using MongoDB.Bson;
using MongoDB.Driver;
using SmartSwitch.COMMON.Entidades;
using SmartSwitch.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SmartSwitch.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseDTO
    {
        private readonly MongoClient _client;
        private readonly IMongoDatabase _db;
        public GenericRepository()
        {
            _client = new MongoClient(new MongoUrl("mongodb://eskantu:esklante98@ds046867.mlab.com:46867/proyectoss?retryWrites=false"));
            //_client = new MongoClient(new MongoUrl("mongodb://localhost:27017/proyectoss"));
            _db = _client.GetDatabase("proyectoss");
        }
        private IMongoCollection<T> Collection() => _db.GetCollection<T>(typeof(T).Name);

        public string Error { get; private set; }

        public IEnumerable<T> Read
        {
            get
            {
                try
                {
                    IEnumerable<T> datos = Collection().AsQueryable();
                    return datos == null ? throw new Exception("Error en la conexión") : datos;
                }
                catch (Exception ex)
                {
                    Error = ex.Message;
                    return null;
                }
            }
        }

        public IEnumerable<T> AddMultiple(IEnumerable<T> entidades)
        {
            throw new NotImplementedException();
        }

        public T Create(T entidad)
        {
            try
            {
                //entidad.FechaHoraCreacion = DateTime.Now;
                entidad.Id = ObjectId.GenerateNewId().ToString();
                Collection().InsertOne(entidad);
                Error = "";
                return entidad;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return null;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                Collection().DeleteOne(entidad => entidad.Id == id);
                Error = "";
                return true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }

        public IEnumerable<string> DeleteMultiple(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> predicado) => Read.Where(predicado.Compile());

        public T SearchById(string id)
        {
            return Read.Where(entidad => entidad.Id == id).SingleOrDefault();
        }

        public bool Update(T entidad)
        {
            throw new NotImplementedException();
        }
    }
}
