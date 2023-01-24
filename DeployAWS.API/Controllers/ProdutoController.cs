using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DeployAWS.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Produces("applicaion/json")]
    public class ProdutosController : ControllerBase
    {
        private readonly IApplicationServiceProduto _applicationServiceProduto;
        private readonly IValidator<ProdutoDto> _validator;

        public ProdutosController(IApplicationServiceProduto applicationServiceProduto, IValidator<ProdutoDto> validator)
        {
            _applicationServiceProduto = applicationServiceProduto;
            _validator = validator;
        }

        /// <summary>
        /// Recupera uma lista contendo todos os produtos disponíveis.
        /// </summary>
        /// <returns>Lista de objetos cliente</returns>
        /// <remarks>
        /// Get()
        /// </remarks>
        /// <response code="200">Retorna uma lista de produtos</response>
        /// <response code="500">Erro interno de processamento</response>
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            try
            {
                return Ok(_applicationServiceProduto.GetAll());
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
        /// <response code="400">Mensagem de retorno caso o id informado não exista</response>
        /// <response code="500">Erro interno de processamento</response>
        // GET api/values/5\
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            try
            {
                return Ok(_applicationServiceProduto.GetById(id));
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
        /// <param name="produtoDTO"></param>
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
        /// <response code="201">Retorna o novo cliente criado</response>
        /// <response code="400">Retorno caso as propriedades informadas não estejam corretas</response>
        /// <response code="500">Erro interno de processamento</response>
        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] ProdutoDto produtoDTO)
        {
            try
            {
                if (produtoDTO == null)
                    return NotFound();

                var result = _validator.Validate(produtoDTO);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }

                _applicationServiceProduto.Add(produtoDTO);

                return Ok("O produto foi cadastrado com sucesso");
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
        /// <param name="produtoDTO"></param>
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
        public ActionResult Put([FromBody] ProdutoDto produtoDTO)
        {
            try
            {
                if (produtoDTO == null)
                    return NotFound();

                var result = _validator.Validate(produtoDTO);

                if (!result.IsValid) 
                {
                    return BadRequest(result.Errors);
                }

                _applicationServiceProduto.Update(produtoDTO);

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
        // DELETE api/values/5
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                if (id == 0)
                    return NotFound();

                var deleted = _applicationServiceProduto.Remove(id);

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