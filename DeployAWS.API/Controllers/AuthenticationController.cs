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
    public class AuthenticationController : ControllerBase
    {

        private readonly IApplicationServiceCustomer _applicationServiceCustomer;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IApplicationServiceCustomer applicationServiceCustomer, IConfiguration configuration)
        {
            _applicationServiceCustomer = applicationServiceCustomer;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Authentication([FromBody] AuthenticationDto authenticationDto)
        {
            try
            {
                var customer = await _applicationServiceCustomer.GetByIdAsync(authenticationDto.Id);

                if (customer == null)
                    return BadRequest(new { Message = "Usuário inválido." });


                var token = ServiceJwtAuth.GenerateToken(customer, _configuration);

                return Ok(new
                {
                    Token = token,
                    Usuario = customer
                });

            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Ocorreu algum erro interno na aplicação, por favor tente novamente." });
            }
        }
    }
}
