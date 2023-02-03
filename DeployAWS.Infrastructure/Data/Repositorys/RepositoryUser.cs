using DeployAWS.Domain.Core.Interfaces.Repositorys;
using DeployAWS.Domain.Entitys;

namespace DeployAWS.Infrastructure.Data.Repositorys
{
    public class RepositoryUser : RepositoryBase<User>, IRepositoryUser
    {
        private readonly AppDbContext _appDbContext;

        public RepositoryUser(AppDbContext appDbContext)
            : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}
