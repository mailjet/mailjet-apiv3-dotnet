using System;
using System.Collections.Generic;

namespace Mailjet.Repositories.Interfaces.Bases
{
    public interface IRepositoryRead<TResult>
    {
        TResult Read();
    }

    public interface IRepositoryRead<TResult, TKey>
    {
        TResult Read(TKey key);
    }

    public interface IRepositoryRead<TResult, TKey1, TKey2>
    {
        TResult Read(TKey1 key1, TKey2 key2);
    }

    public interface IRepositoryRead<TResult, TKey1, TKey2, TKey3>
    {
        TResult Read(TKey1 key1, TKey2 key2, TKey3 key3);
    }

    public interface IRepositoryRead<TResult, TKey1, TKey2, TKey3, TKey4>
    {
        TResult Read(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4);
    }

    public interface IRepositoryRead<TResult, TKey1, TKey2, TKey3, TKey4, TKey5>
    {
        TResult Read(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5);
    }

    public interface IRepositoryRead<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>
    {
        TResult Read(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6);
    }

    public interface IRepositoryRead<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7>
    {
        TResult Read(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7);
    }

    public interface IRepositoryRead<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8>
    {
        TResult Read(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8);
    }

    public interface IRepositoryRead<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9>
    {
        TResult Read(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9);
    }

}

