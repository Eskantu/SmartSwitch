using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serialize.Linq.Serializers;
using SmartSwitch.COMMON.Entidades;
using SmartSwitch.COMMON.Interfaces;

namespace SmartSwitch.WebAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class GenericController<T> : ControllerBase where T : BaseDTO
    {
        IGenericRepository<T> genericRepository;
        public GenericController(IGenericRepository<T> genericRepository)
        {
            this.genericRepository = genericRepository;
        }
        // GET: api/Generic
        [HttpGet]
        public IEnumerable<T> Get()
        {
            return genericRepository.Read;
        }

        // GET: api/Generic/5
        [HttpGet("{id}")]
        public ActionResult<T> Get(string id)
        {
            try
            {
                return Ok(genericRepository.SearchById(id));
            }
            catch (Exception)
            {
                return BadRequest($"Error al obtener entidad con id {id}");
            }
        }

        // POST: api/Generic Create
        [HttpPost]
        public ActionResult<T> Post([FromBody] string value)
        {
            try
            {
                //if (command == "query")
                {
                    var serializer = new ExpressionSerializer(new Serialize.Linq.Serializers.JsonSerializer());
                    var query = serializer.DeserializeText(value);
                    return Ok(genericRepository.Query((Expression<Func<T, bool>>)query));
                }
               // else
                {
                    return Ok(genericRepository.Create((T)JsonConvert.DeserializeObject(value)));

                }
            }
            catch (Exception)
            {
                return BadRequest($"Erro al realzar  del registro ERROR: {genericRepository.Error}");
            }
        }

        // PUT: api/Generic/5 Update
        [HttpPut("{id}")]
        public ActionResult<bool> Put([FromBody] T value)
        {
            try
            {
                return Ok(genericRepository.Update(value));

            }
            catch (Exception)
            {

                return NotFound($"no se puede edita entidad con id {value.Id}, no existe");
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(string id)
        {
            try
            {
                return Ok(genericRepository.Delete(id));
            }
            catch (Exception)
            {
                return BadRequest(genericRepository.Error);
            }
        }


    }
}
