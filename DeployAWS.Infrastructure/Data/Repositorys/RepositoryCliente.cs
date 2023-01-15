using DeployAWS.Domain.Core.Interfaces.Repositorys;
using DeployAWS.Domain.Entitys;

namespace DeployAWS.Infrastructure.Data.Repositorys
{
    public class RepositoryCliente : RepositoryBase<Cliente>, IRepositoryCliente
    {
        private readonly AppDbContext _appDbContext;

        public RepositoryCliente(AppDbContext appDbContext)
            : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}