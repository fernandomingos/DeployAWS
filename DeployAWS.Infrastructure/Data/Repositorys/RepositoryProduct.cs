using DeployAWS.Domain.Core.Interfaces.Repositorys;
using DeployAWS.Domain.Entitys;
using DeployAWS.Infrastructure.Settings.NoSQL;
using Microsoft.Extensions.Options;

namespace DeployAWS.Infrastructure.Data.Repositorys
{
    public class RepositoryProduct : RepositoryBaseNoSQL<Product>, IRepositoryProduct
    {
        private readonly AppDbNoSQLContext _appDbNoSQLContext;

        public RepositoryProduct(IOptions<ProductDatabaseSettings> options) : base(options)
        {
            _appDbNoSQLContext = new AppDbNoSQLContext(options);
        }
    }
}