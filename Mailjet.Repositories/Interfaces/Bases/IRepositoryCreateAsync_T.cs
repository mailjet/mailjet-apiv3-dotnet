using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mailjet.Repositories.Interfaces.Bases
{
    public interface IRepositoryCreateAsync<T>
    {
        Task<T> CreateAsync(Task<T> model);
    }

    public interface IRepositoryCreateAsync<TResult, TResultRaw>


    {
        Task<TResult> CreateAsync(TResultRaw key);
    }

    public interface IRepositoryCreateAsync<TResult, TKey1, TKey2>

    {
        Task<TResult> CreateAsync(TKey1 key1, TKey2 key2);
    }

    public interface IRepositoryCreateAsync<TResult, TKey1, TKey2, TKey3>

    {
        Task<TResult> CreateAsync(TKey1 key1, TKey2 key2, TKey3 key3);
    }

    public interface IRepositoryCreateAsync<TResult, TKey1, TKey2, TKey3, TKey4>

    {
        Task<TResult> CreateAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4);
    }

    public interface IRepositoryCreateAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5>

    {
        Task<TResult> CreateAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5);
    }

    public interface IRepositoryCreateAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6>

    {
        Task<TResult> CreateAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6);
    }

    public interface IRepositoryCreateAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7>

    {
        Task<TResult> CreateAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7);
    }

    public interface IRepositoryCreateAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8>

    {
        Task<TResult> CreateAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8);
    }

    public interface IRepositoryCreateAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9>

    {
        Task<TResult> CreateAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9);
    }

    public interface IRepositoryCreateAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9, TKey10>

    {
        Task<TResult> CreateAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9, TKey10 key10);
    }

    public interface IRepositoryCreateAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9, TKey10, TKey11, TKey12>

    {
        Task<TResult> CreateAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9, TKey10 key10, TKey11 key11, TKey12 key12);
    }

    public interface IRepositoryCreateAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9, TKey10, TKey11, TKey12, TKey13>

    {
        Task<TResult> CreateAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9, TKey10 key10, TKey11 key11, TKey12 key12, TKey13 key13);
    }

    public interface IRepositoryCreateAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9, TKey10, TKey11, TKey12, TKey13, TKey14>

    {
        Task<TResult> CreateAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9, TKey10 key10, TKey11 key11, TKey12 key12, TKey14 key14);
    }

    public interface IRepositoryCreateAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9, TKey10, TKey11, TKey12, TKey13, TKey14, TKey15>

    {
        Task<TResult> CreateAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9, TKey10 key10, TKey11 key11, TKey12 key12, TKey14 key14, TKey15 key15);
    }

    public interface IRepositoryCreateAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9, TKey10, TKey11, TKey12, TKey13, TKey14, TKey15, TKey16>

    {
        Task<TResult> CreateAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9, TKey10 key10, TKey11 key11, TKey12 key12, TKey14 key14, TKey15 key15, TKey16 key16);
    }

    public interface IRepositoryCreateAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9, TKey10, TKey11, TKey12, TKey13, TKey14, TKey15, TKey16, TKey17>

    {
        Task<TResult> CreateAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9, TKey10 key10, TKey11 key11, TKey12 key12, TKey14 key14, TKey15 key15, TKey16 key16, TKey17 key17);
    }

    public interface IRepositoryCreateAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9, TKey10, TKey11, TKey12, TKey13, TKey14, TKey15, TKey16, TKey17, TKey18>

    {
        Task<TResult> CreateAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9, TKey10 key10, TKey11 key11, TKey12 key12, TKey14 key14, TKey15 key15, TKey16 key16, TKey17 key17, TKey18 key18);
    }

    public interface IRepositoryCreateAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9, TKey10, TKey11, TKey12, TKey13, TKey14, TKey15, TKey16, TKey17, TKey18, TKey19>

    {
        Task<TResult> CreateAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9, TKey10 key10, TKey11 key11, TKey12 key12, TKey14 key14, TKey15 key15, TKey16 key16, TKey17 key17, TKey18 key18, TKey19 key19);
    }

    public interface IRepositoryCreateAsync<TResult, TKey1, TKey2, TKey3, TKey4, TKey5, TKey6, TKey7, TKey8, TKey9, TKey10, TKey11, TKey12, TKey13, TKey14, TKey15, TKey16, TKey17, TKey18, TKey19, TKey20>

    {
        Task<TResult> CreateAsync(TKey1 key1, TKey2 key2, TKey3 key3, TKey4 key4, TKey5 key5, TKey6 key6, TKey7 key7, TKey8 key8, TKey9 key9, TKey10 key10, TKey11 key11, TKey12 key12, TKey14 key14, TKey15 key15, TKey16 key16, TKey17 key17, TKey18 key18, TKey19 key19, TKey20 key20);
    }
}