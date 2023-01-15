using DeployAWS.Application.Dtos;
using DeployAWS.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
            return Ok(applicationServiceCliente.GetAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return Ok(applicationServiceCliente.GetById(id));
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
            catch
            {
                throw;
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
            catch
            {
                throw;
            }
        }

        // DELETE api/values/5
        [HttpDelete()]
        public ActionResult Delete([FromBody] ClienteDto clienteDTO)
        {
            try
            {
                if (clienteDTO == null)
                    return NotFound();

                applicationServiceCliente.Remove(clienteDTO);
                return Ok("Cliente removido com sucesso!");
            }
            catch
            {
                throw;
            }
        }
    }
}