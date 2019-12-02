using SmartSwitch.COMMON.Entidades;
using SmartSwitch.COMMON.Interfaces;
using SmartSwitch.DAL;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SmartSwitch.BIZ
{
    public class GenericManager<T>:IGenericManager<T> where T:BaseDTO
    {
       internal IGenericRepository<T> genericRepository;
        public GenericManager()
        {
            genericRepository = new GenericRepository<T>();
        }

        public string Error { get; private set; }

        public IEnumerable<T> ObtenerTodos => genericRepository.Read;

        public bool Actualizar(T entidad)
        {
            bool r=  genericRepository.Update(entidad);
            Error = genericRepository.Error;
            return r;
        }

        public T BuscarPorID(string id)
        {
            T entidad = genericRepository.SearchById(id);
            Error = genericRepository.Error;
            return entidad;
        }

        public T Crear(T entidad)
        {
            T result = genericRepository.Create(entidad);
            Error = genericRepository.Error;
            return result;

        }

        public IEnumerable<T> CrearVarios(IEnumerable<T> entidades)
        {
            throw new NotImplementedException();
        }

        public bool Eliminar(string id)
        {
            bool result = genericRepository.Delete(id);
            Error = genericRepository.Error;
            return result;
        }

        public IEnumerable<string> EliminarVarios(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Query(Expression<Func<T, bool>> predicado)
        {
            IEnumerable<T> query = genericRepository.Query(predicado);
            Error = genericRepository.Error;
            return query;
        }
    }
}
