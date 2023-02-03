using DeployAWS.Domain.Core.Interfaces.Repositorys;
using DeployAWS.Domain.Entitys;

namespace DeployAWS.Infrastructure.Data.Repositorys
{
    public class RepositoryCustomer : RepositoryBase<Customer>, IRepositoryCustomer
    {
        private readonly AppDbContext _appDbContext;

        public RepositoryCustomer(AppDbContext appDbContext)
            : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}