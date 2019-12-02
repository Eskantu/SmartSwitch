using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSwitch.COMMON.Entidades
{
    public class Usuarios:BaseDTO
    {
        public string Nombre { get; set; }
        public string ApMaterno { get; set; }
        public string ApPaterno { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UrlFoto { get; set; }
        public bool EsAdmin { get; set; }
    }
}
