using System;
using System.Collections;
using System.Linq;
using CursoSM.AccesoDatos;

namespace CursoSM.LogicaNegocio
{
    public class CtrlRestaurante
    {
        RESERVAMEEntities DBcontext = new RESERVAMEEntities();

        /**Lista Restaurantes**/
        public IList ListaRestaurantes()
        {
            try
            {
                Console.WriteLine("Iniciando la consulta de la lista de restaurantes...");

                // Verificar si el contexto tiene datos
                var hasData = DBcontext.RESTAURANTE.Any();
                Console.WriteLine("Tiene datos: " + hasData);

                if (!hasData)
                {
                    Console.WriteLine("No hay datos en la tabla RESTAURANTE.");
                    return null;
                }

                var query = (from c in DBcontext.RESTAURANTE
                             orderby c.cNombre
                             select new
                             {
                                 cNombre = c.cNombre,
                                 cDireccion = c.cDireccion,
                                 cTelefono = c.cTelefono
                             }).Take(10).ToList();

                // Verificar el resultado de la consulta
                if (query != null && query.Count > 0)
                {
                    Console.WriteLine("Datos encontrados: " + query.Count);
                    return query;
                }
                else
                {
                    Console.WriteLine("No se encontraron datos.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }
    }
}
