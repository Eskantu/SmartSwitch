using MongoDB.Bson;
using Newtonsoft.Json;
using SmartSwitch.COMMON.Entidades;
using SmartSwitch.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SmartSwitch.DAL.API
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseDTO
    {
        HttpClient httpClient;
        string _apiEntidad;
        public GenericRepository()
        {
            httpClient = new HttpClient();
            _apiEntidad = "api/" + typeof(T).Name.ToLower();
            //httpClient.BaseAddress = new Uri("https://marioescalante.azurewebsites.net/");
            httpClient.BaseAddress = new Uri("https://localhost:44362/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.MaxResponseContentBufferSize = 100000000;
        }
        public string Error { get; private set; }

        public IEnumerable<T> Read => ObtenerDatos().Result;

        public IEnumerable<T> AddMultiple(IEnumerable<T> entidades)
        {
            throw new NotImplementedException();
        }

        public T Create(T entidad)
        {
            return CrearDato(entidad).Result;
        }

        public bool Delete(string id)
        {
            return Borrar(id).Result;
        }

        public IEnumerable<string> DeleteMultiple(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> predicado)
        {
            return Consulta(predicado).Result;

        }


        public T SearchById(string id)
        {
            return BuscarPorID(id).Result;
        }
        private async Task<bool> Borrar(string id)
        {
            HttpResponseMessage message = await httpClient.DeleteAsync(_apiEntidad).ConfigureAwait(false);
            var content = await message.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (message.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<bool>(content);
            }
            else
            {
                Error = content;
                return false;
            }
        }
        private async Task<T> BuscarPorID(string id)
        {
            HttpResponseMessage message = await httpClient.GetAsync(_apiEntidad + "/id").ConfigureAwait(false);
            var content = await message.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (message.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(content);

            }
            else
            {
                Error = content;
                return null;
            }
        }

        public bool Update(T entidad)
        {
            throw new NotImplementedException();
        }

        private async Task<IEnumerable<T>> ObtenerDatos()
        {
            HttpResponseMessage message = await httpClient.GetAsync(_apiEntidad).ConfigureAwait(false);
            var content = await message.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (message.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<IEnumerable<T>>(content);

            }
            else
            {
                Error = content;
                return null;
            }
        }

        private async Task<T> CrearDato(T entidad)
        {
            entidad.FechaHoraCreacion = DateTime.Now;
            entidad.Id = ObjectId.GenerateNewId().ToString();
            string json = JsonConvert.SerializeObject(entidad);
            HttpResponseMessage message = await httpClient.PostAsync(_apiEntidad, new StringContent(json, Encoding.UTF8, "application/json")).ConfigureAwait(false);
            var result = await message.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (message.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(result);
            }
            else
            {
                Error = result;
                return null;
            }


        }

        private async Task<IEnumerable<T>> Consulta(Expression<Func<T, bool>> predicado)
        {
            HttpResponseMessage message = await httpClient.PostAsync(_apiEntidad + "/query", new StringContent(JsonConvert.SerializeObject(predicado), Encoding.UTF8, "application/json")).ConfigureAwait(false);
            var result = await message.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (message.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<IEnumerable<T>>(result);
            }
            else
            {
                Error = result;
                return null;
            }
        }
    }
}
