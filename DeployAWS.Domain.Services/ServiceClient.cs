using DeployAWS.Domain.Core.Interfaces.Repositorys;
using DeployAWS.Domain.Core.Interfaces.Services;
using DeployAWS.Domain.Entitys;

namespace DeployAWS.Domain.Services
{
    public class ServiceClient : ServiceBase<Client>, IServiceClient
    {
        private readonly IRepositoryClient _repositoryClient;

        public ServiceClient(IRepositoryClient repositoryClient) : base(repositoryClient)
        {
            _repositoryClient = repositoryClient;
        }
    }
}