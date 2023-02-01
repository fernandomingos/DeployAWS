using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
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

        public ProductController(IApplicationServiceProduct applicationServiceProduct, IValidator<ProductDto> validator)
        {
            _applicationServiceProduct = applicationServiceProduct;
            _validator = validator;
        }

        /// <summary>
        /// Recupera uma lista contendo todos os produtos disponíveis.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Get()
        /// </remarks>
        /// <response code="200">Retorna uma lista de produtos</response>
        /// <response code="204">Não há conteúdo para ser exibido</response>
        /// <response code="500">Erro interno de processamento</response>
        // GET api/values
        [HttpGet]
        public async Task<ActionResult> GetAsync()
        {
            try
            {
                var result = await _applicationServiceProduct.GetAllAsync();

                return Ok(result.ToList());
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
        /// Recupera um objeto produto pelo id.
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Retorna um produto</response>
        /// <response code="204">Não há conteúdo para ser exibido</response>
        /// <response code="500">Erro interno de processamento</response>
        // GET api/values/5\
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(string id)
        {
            try
            {
                var response = await _applicationServiceProduct.GetByIdAsync(id);

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
        /// <response code="500">Erro interno de processamento</response>
        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] ProductDto productDTO)
        {
            try
            {
                if (productDTO == null)
                    return NotFound();

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
        // PUT api/values/5
        [HttpPut]
        public ActionResult Put([FromBody] ProductDto productDTO)
        {
            try
            {
                if (productDTO == null)
                    return NotFound();

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
        /// Remove um objeto produto na base de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Status code e mensagem</returns>
        /// <response code="200">Produto removido com sucesso</response>
        /// <response code="400">Retorno caso produto não exista</response>
        /// <response code="500">Erro interno de processamento</response> 
        [HttpDelete("{id:string}")]
        public ActionResult Delete(string id)
        {
            try
            {
                if (id == "")
                    return NotFound();

                var deleted = _applicationServiceProduct.Remove(id);

                if (deleted)
                    return Ok("Produto removido com sucesso!");
                else
                    return BadRequest("Id do produto informado não existe!");

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