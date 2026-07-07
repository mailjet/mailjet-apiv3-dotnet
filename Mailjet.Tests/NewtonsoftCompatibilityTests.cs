using Mailjet.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace Mailjet.Tests
{
    /// <summary>
    /// Guards against regressions of issue #162.
    ///
    /// Newtonsoft.Json 13.0.4 added a new single-argument <c>JToken.ToString(Formatting)</c>
    /// overload that does not exist in 13.0.0-13.0.3, yet every 13.0.x release ships the same
    /// assembly version (13.0.0.0). If the library binds to that single-arg overload, consumers
    /// whose runtime loads an older 13.0.x throw MissingMethodException. We therefore assert that
    /// no compiled call in Mailjet.Client binds to the single-arg overload; only the long-standing
    /// <c>ToString(Formatting, params JsonConverter[])</c> overload is allowed.
    /// </summary>
    [TestClass]
    public class NewtonsoftCompatibilityTests
    {
        private const string JTokenFullName = "Newtonsoft.Json.Linq.JToken";

        [TestMethod]
        public void MailjetClientAssembly_DoesNotBindTo_SingleArgJTokenToString()
        {
            var assembly = typeof(MailjetClient).Assembly;

            var offendingCallSites = new List<string>();
            bool foundSafeToStringCall = false;

            foreach (var callee in EnumerateCalledMethods(assembly))
            {
                if (!IsJTokenToString(callee.Method))
                {
                    continue;
                }

                var parameters = callee.Method.GetParameters();

                bool isSingleArg = parameters.Length == 1
                    && parameters[0].ParameterType == typeof(Formatting);

                bool isParamsOverload = parameters.Length == 2
                    && parameters[0].ParameterType == typeof(Formatting)
                    && parameters[1].ParameterType == typeof(JsonConverter[]);

                if (isSingleArg)
                {
                    offendingCallSites.Add(callee.DeclaringMethod);
                }
                else if (isParamsOverload)
                {
                    foundSafeToStringCall = true;
                }
            }

            Assert.AreEqual(
                0,
                offendingCallSites.Count,
                "Mailjet.Client binds to the 13.0.4-only JToken.ToString(Formatting) overload (regression of #162). " +
                "Use ToString(Formatting, Array.Empty<JsonConverter>()) instead. Offending members: " +
                string.Join(", ", offendingCallSites));

            Assert.IsTrue(
                foundSafeToStringCall,
                "Expected to find a call to JToken.ToString(Formatting, JsonConverter[]) but none was found. " +
                "The serialization call site may have moved; update this regression test to keep guarding #162.");
        }

        private static bool IsJTokenToString(MethodBase method)
        {
            return method != null
                && method.Name == "ToString"
                && method.DeclaringType != null
                && method.DeclaringType.FullName == JTokenFullName;
        }

        private static IEnumerable<(MethodBase Method, string DeclaringMethod)> EnumerateCalledMethods(Assembly assembly)
        {
            const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;

            // Includes nested compiler-generated async state machines, where the actual
            // ToString call lives after the C# async rewrite.
            foreach (var type in assembly.GetTypes())
            {
                var members = type.GetMethods(flags).Cast<MethodBase>()
                    .Concat(type.GetConstructors(flags));

                foreach (var member in members)
                {
                    MethodBody body;
                    try
                    {
                        body = member.GetMethodBody();
                    }
                    catch
                    {
                        continue;
                    }

                    if (body == null)
                    {
                        continue;
                    }

                    var il = body.GetILAsByteArray();
                    if (il == null)
                    {
                        continue;
                    }

                    foreach (var token in ReadCallTokens(il))
                    {
                        MethodBase resolved = TryResolveMethod(member, token);
                        if (resolved != null)
                        {
                            yield return (resolved, $"{type.FullName}.{member.Name}");
                        }
                    }
                }
            }
        }

        private static MethodBase TryResolveMethod(MethodBase context, int token)
        {
            try
            {
                Type[] typeArgs = context.DeclaringType != null && context.DeclaringType.IsGenericType
                    ? context.DeclaringType.GetGenericArguments()
                    : null;

                Type[] methodArgs = context.IsGenericMethod
                    ? context.GetGenericArguments()
                    : null;

                return context.Module.ResolveMethod(token, typeArgs, methodArgs);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Minimal IL walker that yields the metadata tokens of every call/callvirt instruction.
        /// It steps over each instruction using the operand size for its opcode so operand bytes
        /// are never misread as opcodes.
        /// </summary>
        private static IEnumerable<int> ReadCallTokens(byte[] il)
        {
            var (single, twoByte) = OpCodeMaps.Value;

            int pos = 0;
            while (pos < il.Length)
            {
                OpCode opCode;
                if (il[pos] == 0xFE)
                {
                    if (pos + 1 >= il.Length || !twoByte.TryGetValue(il[pos + 1], out opCode))
                    {
                        yield break;
                    }
                    pos += 2;
                }
                else
                {
                    if (!single.TryGetValue(il[pos], out opCode))
                    {
                        yield break;
                    }
                    pos += 1;
                }

                int operandSize = GetOperandSize(opCode, il, pos);

                if ((opCode == OpCodes.Call || opCode == OpCodes.Callvirt) && pos + 4 <= il.Length)
                {
                    yield return BitConverter.ToInt32(il, pos);
                }

                if (operandSize < 0)
                {
                    yield break;
                }

                pos += operandSize;
            }
        }

        private static int GetOperandSize(OpCode opCode, byte[] il, int operandPos)
        {
            switch (opCode.OperandType)
            {
                case OperandType.InlineNone:
                    return 0;
                case OperandType.ShortInlineBrTarget:
                case OperandType.ShortInlineI:
                case OperandType.ShortInlineVar:
                    return 1;
                case OperandType.InlineVar:
                    return 2;
                case OperandType.InlineBrTarget:
                case OperandType.InlineField:
                case OperandType.InlineI:
                case OperandType.InlineMethod:
                case OperandType.InlineSig:
                case OperandType.InlineString:
                case OperandType.InlineTok:
                case OperandType.InlineType:
                case OperandType.ShortInlineR:
                    return 4;
                case OperandType.InlineI8:
                case OperandType.InlineR:
                    return 8;
                case OperandType.InlineSwitch:
                    if (operandPos + 4 > il.Length)
                    {
                        return -1;
                    }
                    int count = BitConverter.ToInt32(il, operandPos);
                    return 4 + (count * 4);
                default:
                    return -1;
            }
        }

        private static readonly Lazy<(Dictionary<byte, OpCode> Single, Dictionary<byte, OpCode> TwoByte)> OpCodeMaps =
            new Lazy<(Dictionary<byte, OpCode>, Dictionary<byte, OpCode>)>(BuildOpCodeMaps);

        private static (Dictionary<byte, OpCode>, Dictionary<byte, OpCode>) BuildOpCodeMaps()
        {
            var single = new Dictionary<byte, OpCode>();
            var twoByte = new Dictionary<byte, OpCode>();

            foreach (var field in typeof(OpCodes).GetFields(BindingFlags.Public | BindingFlags.Static))
            {
                if (field.FieldType != typeof(OpCode))
                {
                    continue;
                }

                var opCode = (OpCode)field.GetValue(null);
                ushort value = unchecked((ushort)opCode.Value);

                if (opCode.Size == 1)
                {
                    single[(byte)value] = opCode;
                }
                else
                {
                    twoByte[(byte)(value & 0xFF)] = opCode;
                }
            }

            return (single, twoByte);
        }
    }
}
