using DeployAWS.Domain.Core.Interfaces.Repositorys;
using DeployAWS.Domain.Entitys;

namespace DeployAWS.Infrastructure.Data.Repositorys
{
    public class RepositoryProduto : RepositoryBase<Produto>, IRepositoryProduto
    {
        private readonly AppDbContext _appDbContext;

        public RepositoryProduto(AppDbContext appDbContext)
            : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}