using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace Mailjet.Repositories.Interfaces.Bases
{
    public interface IRepositoryCreate<T>
    {
        T Create(T model);
    }

    public interface IRepositoryCreate<TResult, TResultRaw>


    {
        TResult Create(TResultRaw key);
    }

    public interface IRepositoryCreate<TResult, TKey1, TKey2>

    {
        TResult Create(TKey1 key1, TKey2 key2);
    }

    public interface IRepositoryCreate<TResult, TKey1, TKey2, TKey3>

    {
        TResult Create(TKey1 key1, TKey2 key2, TKey3 key3);
    }

    public interface IRepositoryCreate<TResult, TKey1, TKey2, TKey3, TKey4>

    {
        TResult Create(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4);
    }

    public interface IRepositoryCreate<TResult, TKey1, TKey2, TKey3, TKey4, TKey5>

    {
        TResult Create(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5);
    }

    public interface IRepositoryCreate<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>

    {
        TResult Create(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6);
    }

    public interface IRepositoryCreate<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7>

    {
        TResult Create(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7);
    }

    public interface IRepositoryCreate<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8>

    {
        TResult Create(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8);
    }

    public interface IRepositoryCreate<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9>

    {
        TResult Create(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9);
    }

    public interface IRepositoryCreate<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9, TKey10>

    {
        TResult Create(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9, TKey10 key10);
    }

    public interface IRepositoryCreate<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9, TKey10, TKey11, TKey12>

    {
        TResult Create(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9, TKey10 key10, TKey11 key11, TKey12 key12);
    }

    public interface IRepositoryCreate<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9, TKey10, TKey11, TKey12, TKey13>

    {
        TResult Create(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9, TKey10 key10, TKey11 key11, TKey12 key12, TKey13 key13);
    }

    public interface IRepositoryCreate<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9, TKey10, TKey11, TKey12, TKey13, TKey14>

    {
        TResult Create(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9, TKey10 key10, TKey11 key11, TKey12 key12, TKey14 key14);
    }

    public interface IRepositoryCreate<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9, TKey10, TKey11, TKey12, TKey13, TKey14, TKey15>

    {
        TResult Create(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9, TKey10 key10, TKey11 key11, TKey12 key12, TKey14 key14, TKey15 key15);
    }

    public interface IRepositoryCreate<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9, TKey10, TKey11, TKey12, TKey13, TKey14, TKey15, TKey16>

    {
        TResult Create(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9, TKey10 key10, TKey11 key11, TKey12 key12, TKey14 key14, TKey15 key15, TKey16 key16);
    }

    public interface IRepositoryCreate<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9, TKey10, TKey11, TKey12, TKey13, TKey14, TKey15, TKey16, TKey17>

    {
        TResult Create(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9, TKey10 key10, TKey11 key11, TKey12 key12, TKey14 key14, TKey15 key15, TKey16 key16, TKey17 key17);
    }

    public interface IRepositoryCreate<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9, TKey10, TKey11, TKey12, TKey13, TKey14, TKey15, TKey16, TKey17, TKey18>

    {
        TResult Create(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9, TKey10 key10, TKey11 key11, TKey12 key12, TKey14 key14, TKey15 key15, TKey16 key16, TKey17 key17, TKey18 key18);
    }

    public interface IRepositoryCreate<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9, TKey10, TKey11, TKey12, TKey13, TKey14, TKey15, TKey16, TKey17, TKey18, TKey19>

    {
        TResult Create(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9, TKey10 key10, TKey11 key11, TKey12 key12, TKey14 key14, TKey15 key15, TKey16 key16, TKey17 key17, TKey18 key18, TKey19 key19);
    }

    public interface IRepositoryCreate<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9, TKey10, TKey11, TKey12, TKey13, TKey14, TKey15, TKey16, TKey17, TKey18, TKey19, TKey20>

    {
        TResult Create(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9, TKey10 key10, TKey11 key11, TKey12 key12, TKey14 key14, TKey15 key15, TKey16 key16, TKey17 key17, TKey18 key18, TKey19 key19, TKey20 key20);
    }
}