
using System.Linq.Expressions;

namespace Domain.Repository
{
    public interface IRepositoryBase<T> where T : class
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


        IQueryable<TResult> GetCombo<TResult>(Expression<Func<T, TResult>> projection);
        IQueryable<T> Get(Expression<Func<T, bool>> where);
        IQueryable<T> GetAsNoTracking(Expression<Func<T, bool>> where);
        ICollection<T> Get(Expression<Func<T, bool>> where, string field, int skip, int take);
        Task AddAsync(ICollection<T> commands);
        Task AddAsync(T command);
        //void Update(T command);
        void Delete(Expression<Func<T, bool>> where);
        //void Delete(T entity);
        void Add(ICollection<T> commands);
        void Add(T command);
        
    }
}
