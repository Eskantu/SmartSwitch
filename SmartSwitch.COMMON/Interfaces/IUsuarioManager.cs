using SmartSwitch.COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSwitch.COMMON.Interfaces
{
   public interface IUsuarioManager:IGenericManager<Usuarios>
    {
         Usuarios Login(string userName, string password);
    }
}
