using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DeployAWS.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ClientesController : ControllerBase
    {

        private readonly IApplicationServiceCliente _applicationServiceCliente;
        private readonly IValidator<ClienteDto> _validator;


        public ClientesController(IApplicationServiceCliente applicationServiceCliente, IValidator<ClienteDto> validator)
        {
            _applicationServiceCliente = applicationServiceCliente;
            _validator = validator;
        }

        /// <summary>
        /// Recupera uma lista contendo todos os clientes disponíveis.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Get()
        /// </remarks>
        /// <response code="200">Retorna uma lista de clientes</response>
        /// <response code="500">Erro interno de processamento</response>
        // GET api/values
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            try
            {
                var result = await _applicationServiceCliente.GetAllAsync();

                return Ok(result);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg);
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException val)
            {
                return BadRequest(val);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Recupera um objeto cliente pelo id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Objeto cliente</returns>
        /// <response code="200">Retorna um cliente</response>
        /// <response code="400">Mensagem de retorno caso o id informado não exista</response>
        /// <response code="500">Erro interno de processamento</response>
        // GET api/values/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            try
            {
                var response = _applicationServiceCliente.GetByIdAsync(id).Result;

                return Ok(response);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg);
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException val)
            {
                return BadRequest(val);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Adiciona um objeto cliente na base de dados.
        /// </summary>
        /// <param name="clienteDTO"></param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     PUT
        ///     {
        ///       "id" = 1,
        ///       "nome" = "Michael",
        ///       "sobrenome" = "Jackson",
        ///       "email" = "michael.jackson@neverland.com"
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Retorna o novo cliente criado</response>
        /// <response code="400">Retorno caso as propriedades informadas não estejam corretas</response>
        /// <response code="500">Erro interno de processamento</response>
        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] ClienteDto clienteDTO)
        {
            try
            {
                if (clienteDTO == null)
                    return NotFound();

                var result = _validator.Validate(clienteDTO);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }

                _applicationServiceCliente.Add(clienteDTO);

                return CreatedAtAction("Get", new { id = clienteDTO.Id}, clienteDTO);
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg);
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException val)
            {
                return BadRequest(val);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Altera um objeto cliente na base de dados.
        /// </summary>
        /// <param name="clienteDTO"></param>
        /// <returns>Status code e mensagem</returns>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     POST
        ///     {
        ///       "id" = 1,
        ///       "nome" = "Michael",
        ///       "sobrenome" = "Jackson",
        ///       "email" = "michael.jackson@neverland.com"
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Retorna objeto do produto atualizado</response>
        /// <response code="400">Retorno caso as propriedades informadas não estejam corretas</response>
        /// <response code="500">Erro interno de processamento</response>
        // PUT api/values/5
        [HttpPut]
        public ActionResult Put([FromBody] ClienteDto clienteDTO)
        {
            try
            {
                if (clienteDTO == null)
                    return NotFound();

                var result = _validator.Validate(clienteDTO);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }

                _applicationServiceCliente.Update(clienteDTO);
                return Ok("Cliente atualizado com sucesso!");
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg);
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException val)
            {
                return BadRequest(val);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        /// <summary>
        /// Remove um objeto cliente na base de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status code e mensagem</returns>
        /// <response code="200">Cliente removido com sucesso</response>
        /// <response code="400">Retorno caso cliente não exista</response>
        /// <response code="500">Erro interno de processamento</response> 
        // DELETE api/values/5
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                if (id == 0)
                    return NotFound();

                var deleted = _applicationServiceCliente.Remove(id);

                if (deleted)
                    return Ok("Cliente removido com sucesso!");
                else
                    return BadRequest("Cliente informado não existe!");
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg);
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException val)
            {
                return BadRequest(val);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}