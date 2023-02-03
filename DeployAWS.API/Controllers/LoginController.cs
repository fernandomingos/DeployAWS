using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using DeployAWS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace DeployAWS.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IApplicationServiceUser _applicationServiceUser;
        private readonly IConfiguration _configuration;

        public LoginController(IApplicationServiceUser applicationServiceUser, IConfiguration configuration)
        {
            _applicationServiceUser = applicationServiceUser;
            _configuration = configuration;
        }

        [HttpGet()]
        public async Task<IActionResult> Login([FromBody]UserDto userDto)
        {
            try
            {
                var user = await _applicationServiceUser.GetAsync(userDto);

                if (user == null)
                    return BadRequest(new { Message = "Usuário inválido." });


                var token = ServiceJwtAuth.GenerateToken(user, _configuration);

                return Ok(new
                {
                    Token = token,
                    Usuario = user
                });

            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Ocorreu algum erro interno na aplicação, por favor tente novamente." });
            }
        }
    }
}
