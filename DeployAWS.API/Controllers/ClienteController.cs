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
    public class ClientesController : ControllerBase
    {

        private readonly IApplicationServiceCliente applicationServiceCliente;


        public ClientesController(IApplicationServiceCliente applicationServiceCliente)
        {
            this.applicationServiceCliente = applicationServiceCliente;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            try
            {
                return Ok(applicationServiceCliente.GetAll());
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

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            try
            {
                return Ok(applicationServiceCliente.GetById(id));
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
        public ActionResult Post([FromBody] ClienteDto clienteDTO)
        {
            try
            {
                if (clienteDTO == null)
                    return NotFound();

                applicationServiceCliente.Add(clienteDTO);
                return Ok("Cliente cadastrado com sucesso!");
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
        public ActionResult Put([FromBody] ClienteDto clienteDTO)
        {
            try
            {
                if (clienteDTO == null)
                    return NotFound();

                applicationServiceCliente.Update(clienteDTO);
                return Ok("Cliente atualizado com sucesso!");
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

                var deleted = applicationServiceCliente.Remove(id);

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