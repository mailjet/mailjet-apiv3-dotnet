using System;
using System.Collections.Generic;

namespace Mailjet.Repositories.Interfaces.Bases
{
    public interface IRepositoryUpdate<T>
    {
        T Update(T key);
    }

    public interface IRepositoryUpdate<TResult, TKey>
    {
        TResult Update(TKey key);
    }

    public interface IRepositoryUpdate<TResult, TKey, TUpdate>
    {
        TResult Update(TKey key, TUpdate update);
    }

    public interface IRepositoryUpdate<TResult, TKey, TUpdate1, TUpdate2>
    {
        TResult Update(TKey key, TUpdate1 update1, TUpdate2 update2);
    }

    public interface IRepositoryUpdate<TResult, TKey, TUpdate1, TUpdate2, TUpdate3>
    {
        TResult Update(TKey key, TUpdate1 update1, TUpdate2 update2, TUpdate3 update3);
    }

    public interface IRepositoryUpdate<TResult, TKey, TUpdate1, TUpdate2, TUpdate3, TUpdate4>
    {
        TResult Update(TKey key, TUpdate1 update1, TUpdate2 update2, TUpdate3 update3, TUpdate4 update4);
    }

    public interface IRepositoryUpdate<TResult, TKey, TUpdate1, TUpdate2, TUpdate3, TUpdate4, TUpdate5>
    {
        TResult Update(TKey key, TUpdate1 update1, TUpdate2 update2, TUpdate3 update3, TUpdate4 update4, TUpdate5 update5);
    }

    public interface IRepositoryUpdate<TResult, TKey, TUpdate1, TUpdate2, TUpdate3, TUpdate4, TUpdate5, TUpdate6>
    {
        TResult Update(TKey key, TUpdate1 update1, TUpdate2 update2, TUpdate3 update3, TUpdate4 update4, TUpdate5 update5, TUpdate6 update6);
    }

    public interface IRepositoryUpdate<TResult, TKey, TUpdate1, TUpdate2, TUpdate3, TUpdate4, TUpdate5, TUpdate6, TUpdate7>
    {
        TResult Update(TKey key, TUpdate1 update1, TUpdate2 update2, TUpdate3 update3, TUpdate4 update4, TUpdate5 update5, TUpdate6 update6, TUpdate7 update7);
    }

    public interface IRepositoryUpdate<TResult, TKey, TUpdate1, TUpdate2, TUpdate3, TUpdate4, TUpdate5, TUpdate6, TUpdate7, TUpdate8>
    {
        TResult Update(TKey key, TUpdate1 update1, TUpdate2 update2, TUpdate3 update3, TUpdate4 update4, TUpdate5 update5, TUpdate6 update6, TUpdate7 update7, TUpdate8 update8);
    }

    public interface IRepositoryUpdate<TResult, TKey, TUpdate1, TUpdate2, TUpdate3, TUpdate4, TUpdate5, TUpdate6, TUpdate7, TUpdate8, TUpdate9>
    {
        TResult Update(TKey key, TUpdate1 update1, TUpdate2 update2, TUpdate3 update3, TUpdate4 update4, TUpdate5 update5, TUpdate6 update6, TUpdate7 update7, TUpdate8 update8, TUpdate9 update9);
    }

}

