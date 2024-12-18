﻿
namespace Domain.Repository
{
    public interface ICommonRepository<T>: ITransactionRepository where T : class
    {
        Task<T> Create(T value);
        Task<IList<T>> CreateBatch(IList<T> value); //criar lote
        Task<T> Update(T value);
        Task UpdateBatch(IList<T> values); // update Lote
        Task Delete(int id);
        Task Delete(T entity);
        IList<T> GetAll();
        T GetById(int id);
        IList<T> GetByFunc(Func<T, bool> filter);
        int Count(int id);
    }
}
