using Microcharts;
using SmartSwitch.BIZ;
using SmartSwitch.COMMON.Entidades;
using SmartSwitch.COMMON.Interfaces;
using SmartSwitch.UI.Movil.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XF.Material.Forms.UI.Dialogs;

namespace SmartSwitch.UI.Movil.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : ContentPage
    {
        List<RoomsModel> roomsModels;
        IGenericManager<DatosSensados> datosSensados;
        IGenericManager<Rooms> rooms;
        IGenericManager<Sensores> sensores;
        public Dashboard()
        {
            Random r = new Random(DateTime.Now.Millisecond);
            InitializeComponent();

            datosSensados = new GenericManager<DatosSensados>();
            sensores = new GenericManager<Sensores>();
            rooms = new GenericManager<Rooms>();
            roomsModels = new List<RoomsModel>();
            foreach (var item in rooms.ObtenerTodos)
            {
                roomsModels.Add(new RoomsModel() { Contacts = $"1/{r.Next(1, 8)}", EnergyConsumption = r.Next(1, 100), ImageRoom = item.UrlImagen, NameRoom = item.Nombre, AllContacts = 10, TempAverage = r.Next(1, 100), Options = new List<string> { "See more", "Configure", "Add device" } });
            }
            Datos.ItemsSource = roomsModels;
            CrearGraficasAsync();

        }

        private async Task CrearGraficasAsync()
        {
            using (await MaterialDialog.Instance.LoadingDialogAsync(message: "Loading data"))
            {
                chartTemp.Chart = null;
                chartHuemdad.Chart = null;
                chartContactos.Chart = null;
                chartEnergy.Chart = null;

                List<Microcharts.Entry> entriesEnergy = new List<Microcharts.Entry>();
                List<Microcharts.Entry> entriesTemp = new List<Microcharts.Entry>();
                List<Microcharts.Entry> entriesHum = new List<Microcharts.Entry>();
                List<Microcharts.Entry> entriesCont = new List<Microcharts.Entry>();
                entriesEnergy.Add(new Microcharts.Entry(0) { ValueLabel = "", Color = SkiaSharp.SKColor.Parse("#FF7100"), Label = "" });
                entriesTemp.Add(new Microcharts.Entry(0) { ValueLabel = "", Color = SkiaSharp.SKColor.Parse("#FF7100"), Label = "" });
                entriesHum.Add(new Microcharts.Entry(0) { ValueLabel = "", Color = SkiaSharp.SKColor.Parse("#FF7100"), Label = "" });
                entriesCont.Add(new Microcharts.Entry(0) { ValueLabel = "", Color = SkiaSharp.SKColor.Parse("#FF7100"), Label = "" });
                List<DatosSensados> datosBase = datosSensados.ObtenerTodos.Where(p => p.FechaHoraCreacion > DateTime.Now.AddMonths(-2)).ToList();
                Sensores idTemp = sensores.ObtenerTodos.Where(p => p.TipoDeDatoSensado == "Temperatura").SingleOrDefault();
                Sensores humedad = sensores.ObtenerTodos.Where(p => p.TipoDeDatoSensado == "Humedad").SingleOrDefault();
                Sensores luminocidad = sensores.ObtenerTodos.Where(p => p.TipoDeDatoSensado == "Luminocidad").SingleOrDefault();
                Sensores energia = sensores.ObtenerTodos.Where(p => p.TipoDeDatoSensado == "Energia").SingleOrDefault();
                foreach (var item in datosBase)
                {
                    if (item.IdSensor == energia.Id)
                    {
                        entriesCont.Add(new Microcharts.Entry(float.Parse(item.ValorSensado)) { Label = item.FechaHoraCreacion.ToShortDateString(), Color = SkiaSharp.SKColor.Parse("#FF7100"), ValueLabel = $"{item.ValorSensado}%" });

                    }
                    if (item.IdSensor == idTemp.Id)
                    {
                        entriesTemp.Add(new Microcharts.Entry(float.Parse(item.ValorSensado)) { Label = item.FechaHoraCreacion.ToShortDateString(), Color = SkiaSharp.SKColor.Parse("#FF7100"), ValueLabel = $"{item.ValorSensado}%" });

                    }
                    if (item.IdSensor == humedad.Id)
                    {
                        entriesHum.Add(new Microcharts.Entry(float.Parse(item.ValorSensado)) { Label = item.FechaHoraCreacion.ToShortDateString(), Color = SkiaSharp.SKColor.Parse("#FF7100"), ValueLabel = $"{item.ValorSensado}%" });


                    }
                    if (item.IdSensor == luminocidad.Id)
                    {
                        entriesEnergy.Add(new Microcharts.Entry(float.Parse(item.ValorSensado)) { Label = item.FechaHoraCreacion.ToShortDateString(), Color = SkiaSharp.SKColor.Parse("#FF7100"), ValueLabel = $"{item.ValorSensado}%" });

                    }
                }
                entriesEnergy.Add(new Microcharts.Entry(0) { ValueLabel = "", Color = SkiaSharp.SKColor.Parse("#FF7100"), Label = "" });
                entriesTemp.Add(new Microcharts.Entry(0) { ValueLabel = "", Color = SkiaSharp.SKColor.Parse("#FF7100"), Label = "" });
                entriesHum.Add(new Microcharts.Entry(0) { ValueLabel = "", Color = SkiaSharp.SKColor.Parse("#FF7100"), Label = "" });
                entriesCont.Add(new Microcharts.Entry(0) { ValueLabel = "", Color = SkiaSharp.SKColor.Parse("#FF7100"), Label = "" });

                chartTemp.Chart = new Microcharts.LineChart() { AnimationDuration = new TimeSpan(0, 0, 0, 5, 0), AnimationProgress = 0.5f, BackgroundColor = SkiaSharp.SKColor.FromHsl(0, 0, 0, 0), Entries = entriesTemp, IsAnimated = true, LineMode = LineMode.Spline };
                chartHuemdad.Chart = new Microcharts.LineChart() { AnimationDuration = new TimeSpan(0, 0, 0, 5, 0), AnimationProgress = 0.5f, BackgroundColor = SkiaSharp.SKColor.FromHsl(0, 0, 0, 0), Entries = entriesHum, IsAnimated = true, LineMode = LineMode.Spline };

                chartContactos.Chart = new Microcharts.LineChart() { AnimationDuration = new TimeSpan(0, 0, 0, 5, 0), AnimationProgress = 0.5f, BackgroundColor = SkiaSharp.SKColor.FromHsl(0, 0, 0, 0), Entries = entriesCont, IsAnimated = true, LineMode = LineMode.Straight };

                chartEnergy.Chart = new Microcharts.LineChart() { AnimationDuration = new TimeSpan(0, 0, 0, 5, 0), AnimationProgress = 0.5f, BackgroundColor = SkiaSharp.SKColor.FromHsl(0, 0, 0, 0), Entries = entriesEnergy, IsAnimated = true, LineMode = LineMode.Spline };
            }

        }

        private void MaterialButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync(true);
        }

        private void MaterialMenuButton_MenuSelectedAsync(object sender, XF.Material.Forms.UI.MenuSelectedEventArgs e)
        {

        }

        private void MaterialCard_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Temp(), true);
        }

        private void Datos_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {

            CrearGraficasAsync();
        }

        private void MaterialCard_Clicked_1(object sender, EventArgs e)
        {
            DisplayAlert("SmartSwitch", "Profile", "Ok");
        }
    }
}