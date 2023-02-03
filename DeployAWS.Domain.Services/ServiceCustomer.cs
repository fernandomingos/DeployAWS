using DeployAWS.Domain.Core.Interfaces.Repositorys;
using DeployAWS.Domain.Core.Interfaces.Services;
using DeployAWS.Domain.Entitys;

namespace DeployAWS.Domain.Services
{
    public class ServiceCustomer : ServiceBase<Customer>, IServiceCustomer
    {
        private readonly IRepositoryCustomer _repositoryCustomer;

        public ServiceCustomer(IRepositoryCustomer repositoryCustomer) : base(repositoryCustomer)
        {
            _repositoryCustomer = repositoryCustomer;
        }
    }
}