using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSwitch.UI.Movil.Models
{
    public class RoomsModel
    {
        public string ImageRoom { get; set; }
        public string NameRoom { get; set; }
        public float TempAverage { get; set; }
        public float EnergyConsumption { get; set; }
        public int AllContacts { get; set; }
        public int ContactsOn { get; set; }
        public string Contacts { get; set; }
        public List<string> Options { get; set; }
    }
}
