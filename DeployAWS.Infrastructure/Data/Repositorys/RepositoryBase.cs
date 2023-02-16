using DeployAWS.Domain.Core.Interfaces.Repositorys;
using DeployAWS.Domain.Entitys;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _appDbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetAsync(TEntity obj)
        {
            return await _appDbContext.Set<TEntity>().FindAsync(obj);
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            return await _appDbContext.Set<TEntity>().FindAsync(id);
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

        public async Task<TEntity> PostLoginAsync(Login login)
        {
            try
            {
                var customer = _appDbContext.Customers.FirstOrDefaultAsync(x => x.UserName == login.UserName && x.Password == login.Password).Result;
                var customerEntity = await _appDbContext.Set<TEntity>().FindAsync(customer.Id);
                return customerEntity;
            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
    }
}