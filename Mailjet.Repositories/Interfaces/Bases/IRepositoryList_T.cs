using System;
using System.Collections.Generic;

namespace Mailjet.Repositories.Interfaces.Bases
{
    public interface IRepositoryList<T>
    {
        IList<T> List();
    }
    public interface IRepositoryList<TResult, TKey>
    {
        IList<TResult> List(TKey key);
    }

    public interface IRepositoryList<TResult, TKey1, TKey2>
    {
        IList<TResult> List(TKey1 key1, TKey2 key2);
    }

    public interface IRepositoryList<TResult, TKey1, TKey2, TKey3>
    {
        IList<TResult> List(TKey1 key1, TKey2 key2, TKey3 key3);
    }

    public interface IRepositoryList<TResult, TKey1, TKey2, TKey3, TKey4>
    {
        IList<TResult> List(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4);
    }

    public interface IRepositoryList<TResult, TKey1, TKey2, TKey3, TKey4, TKey5>
    {
        IList<TResult> List(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5);
    }

    public interface IRepositoryList<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>
    {
        IList<TResult> List(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6);
    }

    public interface IRepositoryList<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7>
    {
        IList<TResult> List(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7);
    }

    public interface IRepositoryList<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8>
    {
        IList<TResult> List(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8);
    }

    public interface IRepositoryList<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9>
    {
        IList<TResult> List(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9);
    }
}

