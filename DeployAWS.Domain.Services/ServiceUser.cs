using DeployAWS.Domain.Core.Interfaces.Repositorys;
using DeployAWS.Domain.Core.Interfaces.Services;
using DeployAWS.Domain.Entitys;

namespace DeployAWS.Domain.Services
{
    public class ServiceUser : ServiceBase<User>, IServiceUser
    {
        private readonly IRepositoryUser _repositoryUser;

        public ServiceUser(IRepositoryUser repositoryUser) : base(repositoryUser)
        {
            _repositoryUser = repositoryUser;
        }
    }
}