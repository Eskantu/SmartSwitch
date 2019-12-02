using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSwitch.COMMON.Entidades
{
   public class DatosSensados:BaseDTO
    {
        public string IdRoom { get; set; }
        public string ValorSensado { get; set; }
        public string IdSensor { get; set; }

    }
}
