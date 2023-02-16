using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeployAWS.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OrderController : Controller
    {
        private readonly IApplicationServiceOrder _applicationServiceOrder;
        private readonly IValidator<OrderDto> _validator;


        public OrderController(IApplicationServiceOrder applicationServiceOrder, IValidator<OrderDto> validator)
        {
            _applicationServiceOrder = applicationServiceOrder;
            _validator = validator;
        }

        /// <summary>
        /// Recupera uma lista contendo todos os pedidos disponíveis.
        /// </summary>
        /// <response code="200">Retorna uma lista de pedidos!</response>
        /// <response code="404">Não há conteúdo para ser exibido!</response>
        /// <response code="500">Erro interno de processamento!</response>
        [HttpGet]
        [Authorize(Roles = "admin, client")]
        [ProducesResponseType(typeof(List<OrderDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAsync()
        {
            try
            {
                var result = await _applicationServiceOrder.GetAllAsync();

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
