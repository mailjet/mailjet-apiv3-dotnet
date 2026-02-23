using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mailjet.Repositories.Interfaces.Bases
{
    public interface IRepositoryListAsync<T>
    {
        Task<IList<T>> ListAsync();
    }
    public interface IRepositoryListAsync<TResult, TKey>
    {
        Task<IList<TResult>> ListAsync(TKey key);
    }

    public interface IRepositoryListAsync<TResult, TKey1, TKey2>
    {
        Task<IList<TResult>> ListAsync(TKey1 key1, TKey2 key2);
    }

    public interface IRepositoryListAsync<TResult, TKey1, TKey2, TKey3>
    {
        Task<IList<TResult>> ListAsync(TKey1 key1, TKey2 key2, TKey3 key3);
    }

    public interface IRepositoryListAsync<TResult, TKey1, TKey2, TKey3, TKey4>
    {
        Task<IList<TResult>> ListAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4);
    }

    public interface IRepositoryListAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5>
    {
        Task<IList<TResult>> ListAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5);
    }

    public interface IRepositoryListAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>
    {
        Task<IList<TResult>> ListAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6);
    }

    public interface IRepositoryListAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7>
    {
        Task<IList<TResult>> ListAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7);
    }

    public interface IRepositoryListAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8>
    {
        Task<IList<TResult>> ListAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8);
    }

    public interface IRepositoryListAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9>
    {
        Task<IList<TResult>> ListAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9);
    }
}

