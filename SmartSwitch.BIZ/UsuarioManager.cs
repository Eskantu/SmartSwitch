using SmartSwitch.COMMON.Entidades;
using SmartSwitch.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSwitch.BIZ
{
    public class UsuarioManager:GenericManager<Usuarios>,IUsuarioManager
    {
       
        public Usuarios Login(string userName, string password)
        {
            if (genericRepository.Query(usuario=>usuario.UserName==userName && usuario.Password==password) is Usuarios usuario1)
            {
                return usuario1;
            }
            return null;
        }
    }
}
