using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSwitch.COMMON.Entidades;
using SmartSwitch.COMMON.Interfaces;
using SmartSwitch.DAL;

namespace SmartSwitch.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensoresController : GenericController<Sensores>
    {
        public SensoresController() : base(new GenericRepository<Sensores>())
        {
        }
    }
}