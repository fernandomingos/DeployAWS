using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeployAWS.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        private readonly IApplicationServiceProduct _applicationServiceProduct;
        private readonly IValidator<ProductDto> _validator;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IApplicationServiceProduct applicationServiceProduct, IValidator<ProductDto> validator,
            ILogger<ProductController> logger)
        {
            _applicationServiceProduct = applicationServiceProduct;
            _validator = validator;
            _logger = logger;
        }

        /// <summary>
        /// Recupera uma lista contendo todos os produtos disponíveis.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Retorna uma lista de produtos</response>
        /// <response code="404">Não há conteúdo para ser exibido!</response>
        /// <response code="500">Erro interno de processamento</response>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<CustomerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAsync()
        {
            try
            {
                _logger.LogInformation("##### Enviando requisição GetAsync => ProductController #####");
                var result = await _applicationServiceProduct.GetAllAsync();

                return Ok(result.ToList());
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
        /// Recupera um objeto produto pelo id.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Retorna um produto</response>
        /// <response code="404">Não há conteúdo para ser exibido</response>
        /// <response code="500">Erro interno de processamento</response>
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> GetAsync(string id)
        {
            try
            {
                _logger.LogInformation($"##### Enviando requisição GetAsync => ProductController - id: {id} #####");
                var response = await _applicationServiceProduct.GetByIdAsync(id);

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
        /// Adiciona um objeto produto na base de dados.
        /// </summary>
        /// <param name="productDTO"></param>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     POST
        ///     {
        ///       "id" = 1,
        ///       "nome" = "Caixa de papelão",
        ///       "valor" = 10,00
        ///     }
        /// 
        /// </remarks>
        /// <response code="201">Retorna o novo produto criado</response>
        /// <response code="400">Retorno caso as propriedades informadas não estejam corretas</response>
        /// <response code="404">Não há conteúdo para ser exibido</response>
        /// <response code="500">Erro interno de processamento</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Post([FromBody] ProductDto productDTO)
        {
            try
            {
                _logger.LogInformation($"##### Enviando requisição Post => ProductController - id: {productDTO.Id} #####");

                if (productDTO == null)
                {
                    _logger.LogInformation($"##### Produto com id: {productDTO.Id} não encontrado! #####");
                    return NotFound();
                }

                var result = _validator.Validate(productDTO);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }

                _applicationServiceProduct.CreateAsync(productDTO);

                return Ok(productDTO);
                //return Ok("O produto foi cadastrado com sucesso");
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
        /// Altera um objeto produto na base de dados.
        /// </summary>
        /// <param name="productDTO"></param>
        /// <returns>Status code e mensagem</returns>
        /// <remarks>
        /// Exemplo de requisição:
        /// 
        ///     PUT
        ///     {
        ///       "id" = 1,
        ///       "nome" = "Caixa de papelão",
        ///       "valor" = 10,00
        ///     }
        /// 
        /// </remarks>
        /// <response code="200">Retorna objeto do produto atualizado</response>
        /// <response code="400">Retorno caso as propriedades informadas não estejam corretas</response>
        /// <response code="500">Erro interno de processamento</response>
        [HttpPut]
        public ActionResult Put([FromBody] ProductDto productDTO)
        {
            try
            {
                _logger.LogInformation($"##### Enviando requisição Put => ProductController - id: {productDTO.Id} #####");

                if (productDTO == null)
                {
                    _logger.LogInformation($"##### Produto com id: {productDTO.Id} não encontrado! #####");
                    return NotFound();
                }

                var result = _validator.Validate(productDTO);

                if (!result.IsValid) 
                {
                    return BadRequest(result.Errors);
                }

                _applicationServiceProduct.Update(productDTO);

                return Ok("O produto foi atualizado com sucesso!");
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
        /// Remove um objeto produto na base de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status code e mensagem</returns>
        /// <response code="200">Produto removido com sucesso</response>
        /// <response code="400">Retorno caso produto não exista</response>
        /// <response code="500">Erro interno de processamento</response> 
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult Delete(string id)
        {
            try
            {
                _logger.LogInformation($"##### Enviando requisição Delete => ProductController - id: {id} #####");

                if (id == "")
                {
                    _logger.LogInformation($"##### Produto com id: {id} não encontrado! #####");
                    return NotFound();
                }

                var deleted = _applicationServiceProduct.Remove(id);

                if (deleted)
                    return Ok("Produto removido com sucesso!");
                else
                    return BadRequest("Id do produto informado não existe!");

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