using DeployAWS.Application.Dtos;
using System.Threading.Tasks;

namespace DeployAWS.Application.Interfaces
{
    public interface IApplicationServiceUser
    {
        Task<UserDto> GetAsync(UserDto user);
    }
}
