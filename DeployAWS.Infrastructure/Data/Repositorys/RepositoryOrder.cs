using DeployAWS.Domain.Core.Interfaces.Repositorys;
using DeployAWS.Domain.Entitys;

namespace DeployAWS.Infrastructure.Data.Repositorys
{
    public class RepositoryOrder : RepositoryBase<Order>, IRepositoryOrder
    {
        private readonly AppDbContext _appDbContext;
        public RepositoryOrder(AppDbContext appDbContext)
            : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
