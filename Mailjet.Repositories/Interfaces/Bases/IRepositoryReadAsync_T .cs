using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Interfaces.Bases
{
    public interface IRepositoryReadAsync<TResult>
    {
        Task<TResult> ReadAsync();
    }

    public interface IRepositoryReadAsync<TResult, TKey>
    {
        Task<TResult> ReadAsync(TKey key);
    }

    public interface IRepositoryReadAsync<TResult, TKey1, TKey2>
    {
        Task<TResult> ReadAsync(TKey1 key1, TKey2 key2);
    }

    public interface IRepositoryReadAsync<TResult, TKey1, TKey2, TKey3>
    {
        Task<TResult> ReadAsync(TKey1 key1, TKey2 key2, TKey3 key3);
    }

    public interface IRepositoryReadAsync<TResult, TKey1, TKey2, TKey3, TKey4>
    {
        Task<TResult> ReadAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4);
    }

    public interface IRepositoryReadAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5>
    {
        Task<TResult> ReadAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5);
    }

    public interface IRepositoryReadAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>
    {
        Task<TResult> ReadAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6);
    }

    public interface IRepositoryReadAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7>
    {
        Task<TResult> ReadAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7);
    }

    public interface IRepositoryReadAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8>
    {
        Task<TResult> ReadAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8);
    }

    public interface IRepositoryReadAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9>
    {
        Task<TResult> ReadAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9);
    }

}

