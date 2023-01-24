using DeployAWS.Domain.Core.Interfaces.Repositorys;
using DeployAWS.Domain.Core.Interfaces.Services;
using DeployAWS.Domain.Entitys;

namespace DeployAWS.Domain.Services
{
    public class ServiceProduct : ServiceBase<Product>, IServiceProduct
    {
        private readonly IRepositoryProduct _repositoryProduct;

        public ServiceProduct(IRepositoryProduct repositoryProduct) : base(repositoryProduct)
        {
            _repositoryProduct = repositoryProduct;
        }
    }
}