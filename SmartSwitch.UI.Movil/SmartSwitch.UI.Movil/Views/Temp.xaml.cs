using SmartSwitch.BIZ;
using SmartSwitch.COMMON.Entidades;
using SmartSwitch.COMMON.Interfaces;
using SmartSwitch.TOOLS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SmartSwitch.UI.Movil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Temp : ContentPage
    {
        MQTT mqtt;
        public Temp()
        {
            InitializeComponent();
            OnOff.Text = "Apagado";
            mqtt = new MQTT("IoTDevice", "SmartSWitch/publish", "SmartSWitch/Subcription");
            mqtt.Desconectado += Mqtt_Desconectado;
            mqtt.MensajeRecibido += Mqtt_MensajeRecibido;    
        }

        private void Mqtt_MensajeRecibido(object sender, string e)
        {
            
        }

        private void Mqtt_Desconectado(object sender, string e)
        {
            DisplayAlert("MQTT", "Conexion perdida", "Ok");
        }

        private void encender_Toggled(object sender, ToggledEventArgs e)
        {
            OnOff.Text = e.Value ? "Encendido" : "Apagado";
            DisplayAlert("MQTT", mqtt.Publicar("sw1") ? "Comando enviado" : "Error al enviar comando", "Ok");
        }
    }
}