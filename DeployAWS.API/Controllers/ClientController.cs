﻿using DeployAWS.Application.Dtos;
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
        /// <response code="200">Retorna uma lista de clientes!</response>
        /// <response code="404">Não há conteúdo para ser exibido!</response>
        /// <response code="500">Erro interno de processamento!</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<ClientDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAsync()
        {
            try
            {
                var result = await _applicationServiceClient.GetAllAsync();

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
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
        [HttpGet("{id:int}")]
        [Authorize]
        [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAsync(int id)
        {
            try
            {
                var response = await _applicationServiceClient.GetByIdAsync(id);

                if (response == null)
                    return NotFound();

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
        /// <param name="clientDTO">Entidade cliente DTO</param>
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
        /// <response code="404">Não há conteúdo para ser exibido!</response>
        /// <response code="500">Erro interno de processamento</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ClientDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

                var clientDB = _applicationServiceClient.GetByIdAsync(clientDTO.Id).Result;

                if (clientDB != null)
                    return NotFound("Cliente informado já existe na base");

                var client = _applicationServiceClient.Add(clientDTO);

                return CreatedAtAction("Get", client);
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
        /// <param name="clientDTO">Entidade cliente DTO</param>
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
        /// <response code="404">Não há conteúdo para ser exibido!</response>
        /// <response code="500">Erro interno de processamento</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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

                var clientDB = _applicationServiceClient.GetByIdAsync(clientDTO.Id).Result;

                if (clientDB == null)
                    return BadRequest("Cliente informado não existe na base!");

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
        /// <param name="id">Identificador do cliente</param>
        /// <returns>Status code e mensagem</returns>
        /// <response code="200">Cliente removido com sucesso</response>
        /// <response code="400">Retorno caso cliente não exista</response>
        /// <response code="404">Não há conteúdo para ser exibido!</response>
        /// <response code="500">Erro interno de processamento</response> 
        [HttpDelete("{id:int}")]
        [Authorize]
        [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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