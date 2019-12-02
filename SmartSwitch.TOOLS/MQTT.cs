using OpenNETCF.MQTT;
using System;
using System.Threading;

namespace SmartSwitch.TOOLS
{
    public class MQTT
    {
        public string IdDispositivo { get; private set; }
        public string TemaPublush { get; private set; }
        public string TemaSubscription { get; private set; }
        private readonly MQTTClient clienteMqtt;
        public event EventHandler<string> MensajeRecibido;
        public event EventHandler<string> Desconectado;
        public MQTT(string idDispositivo, string temaPublish, string temaSubcription)
        {
            clienteMqtt = new MQTTClient("broker.mqtt-dashboard.com",1883);
            IdDispositivo = idDispositivo;
            TemaPublush = temaPublish;
            TemaSubscription = temaSubcription;
            clienteMqtt.Connect(idDispositivo);
            while (!clienteMqtt.IsConnected)
            {
                Thread.Sleep(1000);
            }
            clienteMqtt.MessageReceived += ClienteMqtt_MessageReceived;
            clienteMqtt.Disconnected += ClienteMqtt_Disconnected;
            clienteMqtt.Subscriptions.Add(new Subscription(temaSubcription));
        }

        private void ClienteMqtt_Disconnected(object sender, EventArgs e)
        {
            Desconectado(this, "Conexion perdida");
        }

        private void ClienteMqtt_MessageReceived(string topic, QoS qos, byte[] payload)
        {
            MensajeRecibido(this, System.Text.Encoding.UTF8.GetString(payload));
        }

        public bool Publicar(string mensaje)
        {
            if (clienteMqtt.IsConnected)
            {
                clienteMqtt.Publish(TemaPublush, mensaje, QoS.FireAndForget, false);
                return true;
            }
            else
            {
                
                return false;
            }
        }

    }
}
