using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using DeployAWS.Domain.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
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
        private readonly IValidator<LoginDto> _validatorLogin;
        private readonly IValidator<ChangePasswordDto> _validatorChangePassword;

        public AuthenticationController(IApplicationServiceCustomer applicationServiceCustomer, IConfiguration configuration
            , ILogger<AuthenticationController> logger
            , IValidator<LoginDto> validatorLogin
            , IValidator<ChangePasswordDto> validatorChangePassword)
        {
            _applicationServiceCustomer = applicationServiceCustomer;
            _configuration = configuration;
            _logger = logger;
            _validatorLogin = validatorLogin;
            _validatorChangePassword = validatorChangePassword;
        }

        /// <summary>
        /// Recupera objeto login pelo username e password.
        /// </summary>
        /// <param name="loginDto"></param>
        /// <response code="200">Retorna um objeto de autenticação</response>
        /// <response code="404">Não há conteúdo para ser exibido</response>
        /// <response code="500">Erro interno de processamento</response>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authentication([FromBody] LoginDto loginDto)
        {
            try
            {
                _logger.LogInformation($"##### Executando requisição Authentication => AuthenticationController #####");
                var customerDto = await _applicationServiceCustomer.LoginAsync(loginDto);

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

        /// <summary>
        /// Promove a alteração de senha de acesso
        /// </summary>
        /// <param name="changePasswordDto"></param>
        /// <response code="200">Retorna mensagem de sucesso</response>
        /// <response code="404">Não há conteúdo para ser exibido</response>
        /// <response code="500">Erro interno de processamento</response>
        [HttpPost]
        [Route("change-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordDto changePasswordDto)
        {
            try
            {
                // Validar dados de entrada:
                _logger.LogInformation($"##### Executando requisição ChangePasswordAsync => AuthenticationController #####");
                var result = _validatorChangePassword.Validate(changePasswordDto);

                //Validar se senhas novas são iguais
                if (changePasswordDto.NewPassword != changePasswordDto.ConfirmNewPassword)
                {
                    _logger.LogInformation($"##### Validação de senhas: As senhas digitadas não coincidem! {changePasswordDto.UserName} #####");
                    return BadRequest(new { Message = "As senha digitadas não coincidem" });
                }

                if (!result.IsValid)
                {
                    _logger.LogInformation($"##### Executando requisição ChangePasswordAsync => AuthenticationController #####");
                    return BadRequest(result.Errors);
                }

                // Validar se senha enviada está correta está correta.
                var loginDto = new LoginDto
                {
                    UserName = changePasswordDto.UserName,
                    Password = changePasswordDto.ActualPassword
                };

                _logger.LogInformation($"##### Executando requisição LoginAsync => CustomerController #####");
                var customerDto = await _applicationServiceCustomer.LoginAsync(loginDto);

                if (customerDto == null)
                {
                    _logger.LogInformation($"##### Usuário inválido! #####");
                    return BadRequest(new { Message = "Usuário inválido." });
                }

                //atualizando a entidade com a senha nova
                customerDto.ChangePassword(changePasswordDto.NewPassword);
                customerDto.AddModifiedDate();

                _logger.LogInformation($"##### Executando requisição Update => CustomerController #####");
                _applicationServiceCustomer.Update(customerDto);

                return Ok("Senha alterada com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"##### Ocorreu um erro durante o processamento da solicitação. Detalhes: {ex.Message} #####");
                return BadRequest(new { Message = "Ocorreu algum erro interno na aplicação, por favor tente novamente." });
            }
        }
    }
}
