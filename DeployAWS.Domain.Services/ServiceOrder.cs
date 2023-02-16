using DeployAWS.Domain.Core.Interfaces.Repositorys;
using DeployAWS.Domain.Core.Interfaces.Services;
using DeployAWS.Domain.Entitys;

namespace DeployAWS.Domain.Services
{
    public class ServiceOrder : ServiceBase<Order>, IServiceOrder
    {
        private readonly IRepositoryOrder _repositoryOrder;

        public ServiceOrder(IRepositoryOrder repositoryOrder) : base(repositoryOrder)
        {
            _repositoryOrder = repositoryOrder;
        }
    }
}
