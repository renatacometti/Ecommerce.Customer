﻿using Domain.Common;
using Repository.Context;


namespace Repository.Repository
{
    public abstract class BaseRepository<T> : TransactionRepository where T : BaseEntity
    {
        protected BaseRepository(AppDbContext appDbContext) : base(appDbContext)
        {
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

        // teste de um UPdate 2 que inclui
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


    }
}
