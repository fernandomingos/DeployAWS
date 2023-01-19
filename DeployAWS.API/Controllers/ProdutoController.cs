using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DeployAWS.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IApplicationServiceProduto applicationServiceProduto;

        public ProdutosController(IApplicationServiceProduto applicationServiceProduto)
        {
            this.applicationServiceProduto = applicationServiceProduto;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            try
            {
                return Ok(applicationServiceProduto.GetAll());
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg);
            }
            catch (ValidationException val)
            {
                return BadRequest(val);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET api/values/5\
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            try
            {
                return Ok(applicationServiceProduto.GetById(id));
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg);
            }
            catch (ValidationException val)
            {
                return BadRequest(val);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] ProdutoDto produtoDTO)
        {
            try
            {
                if (produtoDTO == null)
                    return NotFound();


                applicationServiceProduto.Add(produtoDTO);
                return Ok("O produto foi cadastrado com sucesso");
            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg);
            }
            catch (ValidationException val)
            {
                return BadRequest(val);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // PUT api/values/5
        [HttpPut]
        public ActionResult Put([FromBody] ProdutoDto produtoDTO)
        {

            try
            {
                if (produtoDTO == null)
                    return NotFound();

                applicationServiceProduto.Update(produtoDTO);
                return Ok("O produto foi atualizado com sucesso!");

            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg);
            }
            catch (ValidationException val)
            {
                return BadRequest(val);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                if (id == 0)
                    return NotFound();

                var deleted = applicationServiceProduto.Remove(id);

                if (deleted)
                    return Ok("Cliente removido com sucesso!");
                else
                    return BadRequest("Cliente informado não existe!");

            }
            catch (ArgumentException arg)
            {
                return BadRequest(arg);
            }
            catch (ValidationException val)
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