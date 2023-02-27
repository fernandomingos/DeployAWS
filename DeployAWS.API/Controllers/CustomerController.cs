using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeployAWS.API.Controllers
{
    [Route("v1/customer")]
    [ApiController]
    [Produces("application/json")]
    public class CustomerController : ControllerBase
    {

        private readonly IApplicationServiceCustomer _applicationServiceCustomer;
        private readonly IValidator<CustomerDto> _validator;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(IApplicationServiceCustomer applicationServiceCustomer, IValidator<CustomerDto> validator,
            ILogger<CustomerController> logger)
        {
            _applicationServiceCustomer = applicationServiceCustomer;
            _validator = validator;
            _logger = logger;
        }

        /// <summary>
        /// Recupera uma lista contendo todos os clientes disponíveis.
        /// </summary>
        /// <response code="200">Retorna uma lista de clientes!</response>
        /// <response code="404">Não há conteúdo para ser exibido!</response>
        /// <response code="500">Erro interno de processamento!</response>
        [HttpGet]
        [Authorize(Roles = "admin, client")]
        [ProducesResponseType(typeof(List<CustomerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAsync()
        {
            try
            {
                _logger.LogInformation("##### Enviando requisição GET => CustomerController #####");
                var result = await _applicationServiceCustomer.GetAllAsync();

                if (result == null)
                {
                    _logger.LogInformation("##### Não há clientes para serem exibidos #####");
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"##### Ocorreu um erro durante o processamento da requisição. Detalhes: {ex.Message} #####");
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Recupera um objeto cliente pelo id.
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <returns>Objeto cliente</returns>
        /// <response code="200">Retorna um cliente</response>
        /// <response code="404">Não há conteúdo para ser exibido!</response>
        /// <response code="500">Erro interno de processamento</response>
        [HttpGet("{id}")]
        [Authorize(Roles = "admin, client")]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAsync(String id)
        {
            try
            {
                _logger.LogInformation($"##### Enviando requisição GetAsync => CustomerController - id: {id} #####");
                var response = await _applicationServiceCustomer.GetByIdAsync(id);

                if (response == null)
                {
                    _logger.LogInformation($"##### Cliente com id: {id} não encontrado! #####");
                    return NotFound();
                }

                return Ok(response);
            }
            catch (ArgumentException arg)
            {
                _logger.LogCritical($"##### Ocorreu um erro durante o processamento da requisição. Detalhes: {arg.Message} #####");
                return BadRequest(arg);
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException val)
            {
                _logger.LogCritical($"##### Ocorreu um erro durante o processamento da requisição. Detalhes: {val.Message} #####");
                return BadRequest(val);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"##### Ocorreu um erro durante o processamento da requisição. Detalhes: {ex.Message} #####");
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Adiciona um objeto cliente na base de dados.
        /// </summary>
        /// <param name="customerDTO">Entidade cliente DTO</param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     POST
        ///     {
	    ///         "userName": "Pele",
	    ///         "firstName": "Edson",
	    ///         "lastName": "Arantes",
	    ///         "password": "3Di5*nP3L#",
	    ///         "emailAddress": "edsonarantes@teste.com",
	    ///         "profile": "client"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Retorna o novo cliente criado</response>
        /// <response code="400">Retorno caso as propriedades informadas não estejam corretas</response>
        /// <response code="404">Não há conteúdo para ser exibido!</response>
        /// <response code="500">Erro interno de processamento</response>
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Post([FromBody] CustomerDto customerDTO)
        {
            try
            {
                _logger.LogInformation($"##### Enviando requisição POST => CustomerController - id: {customerDTO.Id} #####");
                
                if (customerDTO == null)
                {
                    _logger.LogInformation($"##### Cliente com id: {customerDTO.Id} não encontrado! #####");
                    return NotFound();
                }
                    
                customerDTO.AddNewId();
                customerDTO.AddCreateDate();

                var result = _validator.Validate(customerDTO);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors.Select(x => x.ErrorMessage).ToArray());
                }

                var customerDB = _applicationServiceCustomer.GetByIdAsync(customerDTO.Id).Result;

                if (customerDB != null)
                    return NotFound("Cliente informado já existe na base");

                var customer = _applicationServiceCustomer.Add(customerDTO);

                return CreatedAtAction("Get", customer);
            }
            catch (ArgumentException arg)
            {
                _logger.LogCritical($"##### Ocorreu um erro durante o processamento da requisição. Detalhes: {arg.Message} #####");
                return BadRequest(arg);
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException val)
            {
                _logger.LogCritical($"##### Ocorreu um erro durante o processamento da requisição. Detalhes: {val.Message} #####");
                return BadRequest(val);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"##### Ocorreu um erro durante o processamento da requisição. Detalhes: {ex.Message} #####");
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Altera um objeto cliente na base de dados.
        /// </summary>
        /// <param name="customerDTO">Entidade cliente DTO</param>
        /// <returns>Status code e mensagem</returns>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     PUT
        ///     {
	    ///         "userName": "Pele",
	    ///         "firstName": "Edson",
	    ///         "lastName": "Arantes",
	    ///         "password": "3Di5*nP3L#",
	    ///         "emailAddress": "edsonarantes@teste.com",
	    ///         "profile": "client"
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Retorna objeto do cliente atualizado</response>
        /// <response code="400">Retorno caso as propriedades informadas não estejam corretas</response>
        /// <response code="404">Não há conteúdo para ser exibido!</response>
        /// <response code="500">Erro interno de processamento</response>
        [HttpPut]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Put([FromBody] CustomerDto customerDTO)
        {
            try
            {
                _logger.LogInformation($"##### Enviando requisição PUT => CustomerController - id: {customerDTO.Id} #####");
                
                if (customerDTO == null)
                {
                    _logger.LogInformation($"##### Cliente com id: {customerDTO.Id} não encontrado! #####");
                    return NotFound();
                }

                var result = _validator.Validate(customerDTO);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }

                customerDTO.AddModifiedDate();
                _applicationServiceCustomer.Update(customerDTO);

                return Ok("Cliente atualizado com sucesso!");
            }
            catch (ArgumentException arg)
            {
                _logger.LogCritical($"##### Ocorreu um erro durante o processamento da requisição. Detalhes: {arg.Message} #####");
                return BadRequest(arg);
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException val)
            {
                _logger.LogCritical($"##### Ocorreu um erro durante o processamento da requisição. Detalhes: {val.Message} #####");
                return BadRequest(val);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"##### Ocorreu um erro durante o processamento da solirequisiçãocitação. Detalhes: {ex.Message} #####");
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Remove um objeto cliente na base de dados.
        /// </summary>
        /// <param name="id">Identificador do cliente</param>
        /// <returns>Status code e mensagem</returns>
        /// <response code="200">Cliente removido com sucesso</response>
        /// <response code="400">Retorno caso cliente não exista</response>
        /// <response code="404">Não há conteúdo para ser exibido!</response>
        /// <response code="500">Erro interno de processamento</response> 
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Delete(String id)
        {
            try
            {
                _logger.LogInformation($"##### Executando requisição Delete => CustomerController - id: {id}  #####");
                if (string.IsNullOrEmpty(id))
                    return NotFound();

                var deleted = _applicationServiceCustomer.Remove(id);

                if (deleted)
                    return Ok("Cliente removido com sucesso!");
                else
                    return BadRequest("Cliente informado não existe!");
            }
            catch (ArgumentException arg)
            {
                _logger.LogCritical($"##### Ocorreu um erro durante o processamento da requisição. Detalhes: {arg.Message} #####");
                return BadRequest(arg);
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException val)
            {
                _logger.LogCritical($"##### Ocorreu um erro durante o processamento da requisição. Detalhes: {val.Message} #####");
                return BadRequest(val);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"##### Ocorreu um erro durante o processamento da requisição. Detalhes: {ex.Message} #####");
                return StatusCode(500, ex);
            }
        }
    }
}