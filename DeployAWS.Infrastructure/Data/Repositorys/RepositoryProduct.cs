using DeployAWS.Domain.Core.Interfaces.Repositorys;

namespace DeployAWS.Infrastructure.Data.Repositorys
{
    public class RepositoryProduct : RepositoryBaseNoSQL, IRepositoryProduct
    {
        private readonly AppDbNoSQLContext _appDbNoSQLContext;

        public RepositoryProduct(AppDbNoSQLContext appDbNoSQLContext) : base(appDbNoSQLContext) =>
            _appDbNoSQLContext = appDbNoSQLContext;
    }
}