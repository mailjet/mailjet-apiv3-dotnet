using System;
using System.Collections.Generic;



namespace Mailjet.Repositories.Interfaces.Bases
{
    public interface IRepositoryDelete<T>
    {
        T Delete(T item);
    }

    public interface IRepositoryDelete<T, TKey1>
    {
        T Delete(TKey1 item);
    }
    public interface IRepositoryDelete<T, TKey1, TKey2>
    {
        T Delete(TKey1 item, TKey2 item2);
    }
    public interface IRepositoryDelete<T, TKey1, TKey2, TKey3>
    {
        T Delete(TKey1 item, TKey2 item2, TKey3 item3);
    }
    public interface IRepositoryDelete<T, TKey1, TKey2, TKey3, TKey4>
    {
        T Delete(TKey1 item, TKey2 item2, TKey3 item3, TKey4 item4);
    }
    public interface IRepositoryDelete<T, TKey1, TKey2, TKey3, TKey4, TKey5>
    {
        T Delete(TKey1 item, TKey2 item2, TKey3 item3, TKey4 item4, TKey5 item5);
    }
}

