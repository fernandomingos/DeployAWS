﻿using DeployAWS.Application.Interfaces;
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
    public class LoginController : ControllerBase
    {

        private readonly IApplicationServiceCustomer _applicationServiceCustomer;
        private readonly IConfiguration _configuration;

        public LoginController(IApplicationServiceCustomer applicationServiceCustomer, IConfiguration configuration)
        {
            _applicationServiceCustomer = applicationServiceCustomer;
            _configuration = configuration;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(int id)
        {
            try
            {
                var customerDB = await _applicationServiceCustomer.GetByIdAsync(id);

                if (customerDB == null)
                    return BadRequest(new { Message = "Id inválido." });


                var token = ServiceJwtAuth.GenerateToken(customerDB.Nome, _configuration);

                return Ok(new
                {
                    Token = token,
                    Usuario = customerDB
                });

            }
            catch (Exception)
            {
                return BadRequest(new { Message = "Ocorreu algum erro interno na aplicação, por favor tente novamente." });
            }
        }
    }
}
