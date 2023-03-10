using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace DeployAWS.API.Controllers
{
    [Route("v1/order")]
    [ApiController]
    [Produces("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly IApplicationServiceOrder _applicationServiceOrder;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IApplicationServiceOrder applicationServiceOrder, ILogger<OrderController> logger)
        {
            _applicationServiceOrder = applicationServiceOrder;
            _logger = logger;
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
        public ActionResult Get()
        {
            try
            {
                _logger.LogInformation("##### Enviando requisição Get => OrderController #####");
                var result = _applicationServiceOrder.Get();

                if (result == null)
                {
                    _logger.LogInformation("##### Não há pedidos para serem exibidos #####");
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Uma exceção ocorreu durante a execução da API GET => Order {ex}");
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Envia uma solicitação de compra
        /// </summary>
        /// <response code="200">Retorna uma lista de pedidos!</response>
        /// <response code="404">Não há conteúdo para ser exibido!</response>
        /// <response code="500">Erro interno de processamento!</response>
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(String), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Post([FromBody] OrderDto orderDto)
        {
            try
            {
                _logger.LogInformation("##### Enviando requisição Post => OrderController #####");
                _applicationServiceOrder.Add(orderDto);

                return Accepted("Pedido enviado!");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Uma exceção ocorreu durante requisição Post => Order {ex}");
                return StatusCode(500, ex);
            }
        }
    }
}
