using DeployAWS.Domain.Core.Interfaces.Repositorys;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DeployAWS.Infrastructure.Data.Repositorys
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        private readonly AppDbContext _appDbContext;

        public RepositoryBase(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Add(TEntity obj)
        {
            try
            {
                _appDbContext.Set<TEntity>().Add(obj);
                _appDbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _appDbContext.Set<TEntity>().ToList();
        }

        public TEntity GetById(int id)
        {
            return _appDbContext.Set<TEntity>().Find(id);
        }

        public void Remove(TEntity obj)
        {
            try
            {
                _appDbContext.Set<TEntity>().Remove(obj);
                _appDbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void Update(TEntity obj)
        {
            try
            {
                _appDbContext.Entry(obj).State = EntityState.Modified;
                _appDbContext.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}