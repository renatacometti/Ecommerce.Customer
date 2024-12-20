using Domain.Common;
using Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Repository.Context;
using System.Linq.Expressions;

namespace Repository.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T>
             where T : BaseEntity
    {

        protected readonly AppDbContext _context;

        public RepositoryBase(AppDbContext context)
        {
            _context = context;
            _context.Database.SetCommandTimeout(180);
        }

        public virtual async Task<T> Create(T value)
        {
            T? response = default(T);

            if (value != null)
            {
                value.CreateDate = DateTime.Now;
                var responseAdd = await _context.Set<T>().AddAsync(value);

                await _context.SaveChangesAsync();

                response = responseAdd.Entity;
            }
            else
            {
                throw new ArgumentNullException("value");
            }

            return response;
        }


        public virtual async Task<IList<T>> CreateBatch(IList<T> value)
        {
            IList<T>? response = new List<T>();

            if (value != null)
            {
                foreach (var item in value)
                {
                    item.CreateDate = DateTime.Now;
                    var responseAdd = await _context.Set<T>().AddAsync(item);
                    response.Add(responseAdd.Entity);
                }

                await _context.SaveChangesAsync();
            }


            return response;
        }

        public virtual async Task UpdateBatch(IList<T> values)
        {
            if (values != null)
            {
                _context.Set<T>().UpdateRange(values);
                await _context.SaveChangesAsync();
            }

        }

        public virtual async Task<T> Update(T value)
        {
            T? response = default(T);

            if (value != null)
            {

                var responseUpdate = _context.Set<T>().Update(value);

                await _context.SaveChangesAsync();

                response = responseUpdate.Entity;
            }

            return response;
        }

        
        public virtual async Task Update2(T value)
        {
            T? response = default(T);

            if (value != null)
            {
                _context.Set<T>().Entry(value).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                await _context.SaveChangesAsync();

            }

        }

        public virtual async Task Delete(int id)
        {
            if (id > 0)
            {
                T? deleteObject = default(T);
                deleteObject.Id = id;
                _context.Set<T>().Remove(deleteObject);

                await _context.SaveChangesAsync();
            }

        }

        public virtual async Task Delete(T entiy)
        {
            _context.Remove<T>(entiy);
            await _context.SaveChangesAsync();

        }

        public IList<T> GetAll()
        {
            return _context.Set<T>().ToList();

        }

        public T GetById(int id)
        {
            return _context.Set<T>().FirstOrDefault(account => account.Id == id);
        }

        public IList<T> GetByFunc(Func<T, bool> filter)
        {
            return _context.Set<T>().Where(filter).ToList();
        }

        public virtual int Count(int id)
        {
            return _context.Set<T>().Count(account => account.Id == id);
        }

        public IQueryable<TResult> GetCombo<TResult>(Expression<Func<T, TResult>> projection)
        {
            var query = _context.Set<T>().AsQueryable();
            return query.Select(projection);
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> where)
        {
            var query = _context.Set<T>().Where(where).AsQueryable();
            return query;
        }

        public IQueryable<T> GetAsNoTracking(Expression<Func<T, bool>> where)
        {
            var query = _context.Set<T>().Where(where).AsNoTracking().AsQueryable();
            return query;
        }

        public ICollection<T> Get(Expression<Func<T, bool>> where, string field, int skip, int take)
        {
            var sort = GetOrderByExpression(field);

            var entities = _context.Set<T>().Where(where).OrderBy(sort).Skip(skip).Take(take).ToList();

            return entities;
        }

        public void Delete(Expression<Func<T, bool>> where)
        {
            var entities = _context.Set<T>().Where(where);
            if (entities.Any())
            {
                _context.Set<T>().RemoveRange(entities);
            }
        }

 


        public void Add(ICollection<T> commands)
        {
            throw new NotImplementedException();
        }

        public void Add(T command)
        {
            throw new NotImplementedException();
        }

        public Func<T, object> GetOrderByExpression(string sortColumn)
        {
            Func<T, object> orderByExpr = null;
            if (!string.IsNullOrEmpty(sortColumn))
            {
                Type sponsorResultType = typeof(T);

                if (sponsorResultType.GetProperties().Any(prop => prop.Name == sortColumn))
                {
                    System.Reflection.PropertyInfo pinfo = sponsorResultType.GetProperty(sortColumn);
                    orderByExpr = data => pinfo.GetValue(data, null);
                }
            }
            return orderByExpr;
        }

        public Task AddAsync(ICollection<T> commands)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(T command)
        {
            throw new NotImplementedException();
        }
    }
}
