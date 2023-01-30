using DeployAWS.Application.Interfaces;
using DeployAWS.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace DeployAWS.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IApplicationServiceClient _applicationServiceClient;
        private readonly IConfiguration _configuration;

        public AuthController(IApplicationServiceClient applicationServiceClient, IConfiguration configuration)
        {
            _applicationServiceClient = applicationServiceClient;
            _configuration = configuration;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Auth([FromBody]int id)
        {
            try
            {
                var clientDB = await _applicationServiceClient.GetByIdAsync(id);

                if (clientDB == null)
                    return BadRequest(new { Message = "Email inválido." });


                var token = ServiceJwtAuth.GenerateToken(clientDB.Nome, _configuration);

                return Ok(new
                {
                    Token = token,
                    Usuario = clientDB
                });

            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Ocorreu algum erro interno na aplicação, por favor tente novamente." });
            }
        }
    }
}
