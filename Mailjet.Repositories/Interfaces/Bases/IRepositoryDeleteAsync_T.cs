using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Mailjet.Repositories.Interfaces.Bases
{
    public interface IRepositoryDeleteAsync<T>
    {
        Task<T> DeleteAsync(T item);
    }

    public interface IRepositoryDeleteAsync<T, TKey1>
    {
        Task<T> DeleteAsync(TKey1 item);
    }
    public interface IRepositoryDeleteAsync<T, TKey1, TKey2>
    {
        Task<T> DeleteAsync(TKey1 item, TKey2 item2);
    }
    public interface IRepositoryDeleteAsync<T, TKey1, TKey2, TKey3>
    {
        Task<T> DeleteAsync(TKey1 item, TKey2 item2, TKey3 item3);
    }
    public interface IRepositoryDeleteAsync<T, TKey1, TKey2, TKey3, TKey4>
    {
        Task<T> DeleteAsync(TKey1 item, TKey2 item2, TKey3 item3, TKey4 item4);
    }
    public interface IRepositoryDeleteAsync<T, TKey1, TKey2, TKey3, TKey4, TKey5>
    {
        Task<T> DeleteAsync(TKey1 item, TKey2 item2, TKey3 item3, TKey4 item4, TKey5 item5);
    }
}

