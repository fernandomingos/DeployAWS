using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using DeployAWS.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DeployAWS.API.Controllers
{
    [Route("v1/account")]
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

        /// <summary>
        /// Recupera objeto login pelo username e password.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Retorna um objeto de autenticação</response>
        /// <response code="404">Não há conteúdo para ser exibido</response>
        /// <response code="500">Erro interno de processamento</response>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authentication([FromBody] LoginDto login)
        {
            try
            {
                _logger.LogInformation($"##### Executando requisição Authentication => AuthenticationController #####");
                var customerDto = await _applicationServiceCustomer.PostLoginAsync(login);

                if (customerDto == null)
                {
                    _logger.LogInformation($"##### Usuário inválido! #####");
                    return BadRequest(new { Message = "Usuário inválido." });
                }

                var token = ServiceJwtAuth.GenerateToken(customerDto, _configuration);

                return Ok(new
                {
                    Token = token,
                    Usuario = customerDto
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
