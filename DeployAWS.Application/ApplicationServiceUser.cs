using AutoMapper;
using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using DeployAWS.Domain.Core.Interfaces.Services;
using DeployAWS.Domain.Entitys;
using System.Threading.Tasks;

namespace DeployAWS.Application
{
    public class ApplicationServiceUser : IApplicationServiceUser
    {
        private readonly IServiceUser _serviceUser;
        private readonly IMapper _mapper;

        public ApplicationServiceUser(IServiceUser serviceUser
                                        , IMapper mapper)
        {
            _serviceUser = serviceUser;
            _mapper = mapper;
        }

        public async Task<UserDto> GetAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var userDB = await _serviceUser.GetAsync(user);
            return _mapper.Map<UserDto>(userDB);             
        }
    }
}
