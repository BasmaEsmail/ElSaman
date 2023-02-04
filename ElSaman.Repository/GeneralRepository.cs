using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Models;

namespace ElSaman.Repository
{
    public class GeneralRepository<T> where T : class
    {
        private readonly DBContext dbContext;
        private readonly DbSet<T> Set;
        public GeneralRepository (DBContext _dbContext)
        {
            dbContext = _dbContext;
            Set = dbContext.Set<T>();
        }
        public IQueryable<T> Get(Expression<Func<T,bool>>? Filter = null, string? orderBy = null, bool isAscending = false
                     , int pageIndex = 1, int pageSize = 20
             , params string[] includProps)
        {
            var query = Set.AsQueryable();
            foreach (string prop in includProps)
                query = query.Include(prop);
            if(Filter!=null)
                query = query.Where(Filter);
            if(orderBy!=null)
                query= query.OrderBy(orderBy,isAscending);
            int rowCouunt=query.Count();
            if (rowCouunt < pageSize)
                pageIndex = 1;
            if (pageIndex <= 0)
                pageIndex = 1;
            int ExcludeRowCount=(pageIndex - 1)*pageSize;
            query=query.Skip(ExcludeRowCount).Take(pageSize);
            return query;
        }

        public IQueryable<T> GetList()
        {
            return Set.AsQueryable();
        }


        //Get By ID => Back ID From Database
        public T? GetByID(Expression<Func<T, bool>>? filter = null, int ID = 0)
        {
            var query = Set.AsQueryable();
            if (filter != null)
                query = query.Where(filter);
            return query.FirstOrDefault();
        }
        public EntityEntry<T> Add(T entity) =>
             Set.Add(entity);
        public EntityEntry<T> Update(T entity) =>
             Set.Update(entity);
        public EntityEntry<T> Remove(T entity) =>
            Set.Remove(entity);


    }
}
