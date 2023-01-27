﻿using DeployAWS.Application.Dtos;
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
    public class ClientController : ControllerBase
    {

        private readonly IApplicationServiceClient _applicationServiceClient;
        private readonly IValidator<ClientDto> _validator;


        public ClientController(IApplicationServiceClient applicationServiceClient, IValidator<ClientDto> validator)
        {
            _applicationServiceClient = applicationServiceClient;
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
        /// <response code="204">Não há conteúdo para ser exibido</response>
        /// <response code="500">Erro interno de processamento</response>
        // GET api/values
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            try
            {
                var result = await _applicationServiceClient.GetAllAsync();

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
        /// <response code="204">Não há conteúdo para ser exibido</response>
        /// <response code="500">Erro interno de processamento</response>
        // GET api/values/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetAsync(int id)
        {
            try
            {
                var response = await _applicationServiceClient.GetByIdAsync(id);

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
        /// <param name="clientDTO"></param>
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
        public ActionResult Post([FromBody] ClientDto clientDTO)
        {
            try
            {
                if (clientDTO == null)
                    return NotFound();

                var result = _validator.Validate(clientDTO);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }

                _applicationServiceClient.Add(clientDTO);

                return CreatedAtAction("Get", new { id = clientDTO.Id}, clientDTO);
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
        /// <param name="clientDTO"></param>
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
        /// <response code="200">Retorna objeto do cliente atualizado</response>
        /// <response code="400">Retorno caso as propriedades informadas não estejam corretas</response>
        /// <response code="500">Erro interno de processamento</response>
        // PUT api/values/5
        [HttpPut]
        public ActionResult Put([FromBody] ClientDto clientDTO)
        {
            try
            {
                if (clientDTO == null)
                    return NotFound();

                var result = _validator.Validate(clientDTO);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }

                _applicationServiceClient.Update(clientDTO);
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

                var deleted = _applicationServiceClient.Remove(id);

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