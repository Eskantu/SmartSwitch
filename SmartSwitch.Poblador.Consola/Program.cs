﻿using SmartSwitch.COMMON.Entidades;
using SmartSwitch.COMMON.Interfaces;
using SmartSwitch.DAL.API;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartSwitch.Poblador.Consola
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Iniciando poblador...");
            IGenericRepository<Usuarios> usuario = new GenericRepository<Usuarios>();
            IGenericRepository<Home> home = new GenericRepository<Home>();
            IGenericRepository<Rooms> room = new GenericRepository<Rooms>();
            IGenericRepository<Sensores> sensor = new GenericRepository<Sensores>();
            IGenericRepository<DatosSensados> datos = new GenericRepository<DatosSensados>();
            Console.WriteLine("Creando usuario");
          Usuarios user=  usuario.Create(new Usuarios { ApMaterno = "Cantu", ApPaterno = "Escalante", EsAdmin = true, Nombre = "Mario", Password = "esklante98", UserName = "eskantu",FechaHoraCreacion=DateTime.Now , UrlFoto= "data:image/jpeg;base64,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" });


            Console.WriteLine("Creacion de home");
            Home homeCreate = home.Create(new Home() { Nombre = "My Home", Direccion = "Demo", IdUsuario = user.Id });
            for (int i = 0; i < 6; i++)
            {
                room.Create(new Rooms() { IdHome = homeCreate.Id, Nombre = $"Demo-{i}", FechaHoraCreacion=DateTime.Now, UrlImagen= "light.png" });
            }
            Console.WriteLine("Creando Sensores");
            sensor.Create(new Sensores() {FechaHoraCreacion=DateTime.Now, Nombre = "DHT11", TipoDeDatoSensado = "Humedad" });
            sensor.Create(new Sensores() {FechaHoraCreacion=DateTime.Now, Nombre = "DHT11", TipoDeDatoSensado = "Temperatura" });
            sensor.Create(new Sensores() {FechaHoraCreacion=DateTime.Now, Nombre = "Foto resistencia", TipoDeDatoSensado = "Luminocidad" });
            sensor.Create(new Sensores() {FechaHoraCreacion=DateTime.Now, Nombre = "Switch", TipoDeDatoSensado = "Energia" });
            Random random = new Random(DateTime.Now.Millisecond);
            List<Sensores> s = sensor.Read.ToList();
            List<Rooms> r = room.Read.ToList();
            Console.WriteLine("Creando Datos sensados");
            foreach (Rooms item in r)
            {
                foreach (Sensores sensores in s)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        datos.Create(new DatosSensados() { FechaHoraCreacion = new DateTime(random.Next(2000, 2020), random.Next(1, 12), random.Next(1, 20)), IdRoom=item.Id, IdSensor=sensores.Id, ValorSensado= sensores.Nombre== "Switch"?random.Next(0,1).ToString(): random.Next(1, 100).ToString() });
                    }
                }
            }
            Console.WriteLine("Poblador finalizado");
        }
    }
}
