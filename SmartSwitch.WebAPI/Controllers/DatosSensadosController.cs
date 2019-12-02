using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSwitch.BIZ;
using SmartSwitch.COMMON.Entidades;
using SmartSwitch.COMMON.Interfaces;

namespace SmartSwitch.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatosSensadosController : ControllerBase
    {
        
        IGenericManager<DatosSensados> datosSensados;
        IGenericManager<Sensores> sensores;
        IUsuarioManager usuarios;
        public DatosSensadosController()
        {
            sensores = new GenericManager<Sensores>();
            datosSensados = new GenericManager<DatosSensados>();
            usuarios = new UsuarioManager();
        }

        [HttpPut]
        public ActionResult Post(string user, string pass, string idSensor,  string idRoom, string valor)
        {
            if (usuarios.Login(user,pass) is Usuarios usuario)
            {
                if (usuario.EsAdmin)
                {

                    DatosSensados u = datosSensados.Crear(new DatosSensados() { IdRoom = idRoom, IdSensor = idSensor, ValorSensado = valor });
                    if (u != null)
                    {
                        return Ok(1);
                    }
                    return BadRequest(datosSensados.Error); 
                }
                return BadRequest("El usuario no tiene permisos para sensar");
            }
            return BadRequest("El usuario no esta dado de alta");
        }

        [HttpGet]
        public IEnumerable<DatosSensados> Get()
        {
            return datosSensados.ObtenerTodos;
        }

        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(string id)
        {
            try
            {
                return Ok(datosSensados.Eliminar(id));
            }
            catch (Exception)
            {
                return BadRequest(datosSensados.Error);
            }
        }
    }
}