using SmartSwitch.COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SmartSwitch.COMMON.Interfaces
{
    public interface IGenericManager<T> where T : BaseDTO
    {
        /// <summary>
        /// Proporciona información sobre el error ocurrido en alguna de las operaciones
        /// </summary>
        string Error { get; }

        /// <summary>
        /// Crea una entidad de tipo 
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns>Si se pudo crear o no la entidad</returns>
        T Crear(T entidad);

        /// <summary>
        /// Obtiene todos los registros de la tabla
        /// </summary>
        IEnumerable<T> ObtenerTodos { get; }

        /// <summary>
        /// Actualizar un registro en la tabla en base a la propiedad id
        /// </summary>
        /// <param name="entidad">Entidad ya modificada, el id debe existir en la tabla para modificarse</param>
        /// <returns>Confirmación de la actualización</returns>
        bool Actualizar(T entidad);

        /// <summary>
        /// Elimina una entidad en la base de datos de acuerdo al id relacionado
        /// </summary>
        /// <param name="id">Id de la entidad a eliminar</param>
        /// <returns>Confirmación de la eliminación</returns>
        bool Eliminar(string id);

        /// <summary>
        /// Obtiene una entidad en base a su Id
        /// </summary>
        /// <param name="id">Id de la entidad a obtener</param>
        /// <returns>Entidad completa que le corresponde el Id proporcionado</returns>
        T BuscarPorID(string id);

        /// <summary>
        /// Realiza una consulta personalizada a la tabla
        /// </summary>
        /// <param name="prediacado">Expresión Lambda que define la consulta</param>
        /// <returns>Conjunto de entidades que cumplen con la consulta</returns>
        IEnumerable<T> Query(Expression<Func<T, bool>> predicado);

        /// <summary>
        /// Agrega múltiples entidades en la Base de datos
        /// </summary>
        /// <param name="entidades">Entidades a agregar</param>
        /// <returns>Entidades no Agregadas</returns>
        IEnumerable<T> CrearVarios(IEnumerable<T> entidades);
        /// <summary>
        /// Elimina multiples entidaes
        /// </summary>
        /// <param name="ids">Id's de entidades a eliminar</param>
        /// <returns>Id's no eliminadas</returns>
        IEnumerable<string> EliminarVarios(IEnumerable<string> ids);
    }
}
