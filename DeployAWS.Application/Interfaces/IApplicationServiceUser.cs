using DeployAWS.Application.Dtos;
using System.Threading.Tasks;

namespace DeployAWS.Application.Interfaces
{
    public interface IApplicationServiceUser
    {
        Task<CustomerDto> GetByIdAsync(AuthenticationDto authenticationDto);
    }
}
