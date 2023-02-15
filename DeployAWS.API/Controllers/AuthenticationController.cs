using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using DeployAWS.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DeployAWS.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthenticationController : ControllerBase
    {

        private readonly IApplicationServiceCustomer _applicationServiceCustomer;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IApplicationServiceCustomer applicationServiceCustomer, IConfiguration configuration
            , ILogger<AuthenticationController> logger)
        {
            _applicationServiceCustomer = applicationServiceCustomer;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Authentication([FromBody] AuthenticationDto authenticationDto)
        {
            try
            {
                _logger.LogInformation($"##### Executando requisição Authentication => AuthenticationController #####");
                var customer = await _applicationServiceCustomer.GetByIdAsync(authenticationDto.Id);

                if (customer == null)
                {
                    _logger.LogInformation($"##### Usuário inválido! #####");
                    return BadRequest(new { Message = "Usuário inválido." });
                }

                var token = ServiceJwtAuth.GenerateToken(customer, _configuration);

                return Ok(new
                {
                    Token = token,
                    Usuario = customer
                });

            }
            catch (Exception ex)
            {
                _logger.LogCritical($"##### Ocorreu um erro durante o processamento da solicitação. Detalhes: {ex.Message} #####");
                return BadRequest(new { Message = "Ocorreu algum erro interno na aplicação, por favor tente novamente." });
            }
        }
    }
}
