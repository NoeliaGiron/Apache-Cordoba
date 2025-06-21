using System;
using System.Web.UI;
using CursoSM.LogicaNegocio;
using System.Web.Script.Serialization;

namespace WebApp1
{
    public partial class getLista : System.Web.UI.Page
    {
        public string retVal = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now);
            Response.Cache.SetNoServerCaching();
            Response.Cache.SetNoStore();

            try
            {
                CtrlRestaurante negocio = new CtrlRestaurante();
                var lista = negocio.ListaRestaurantes();

                if (lista != null)
                {
                    var json = new JavaScriptSerializer();
                    retVal = json.Serialize(lista);
                    System.Console.WriteLine("Datos serializados correctamente: " + retVal);
                }
                else
                {
                    retVal = "No se encontraron datos.";
                    System.Console.WriteLine("Lista es null o está vacía.");
                }
            }
            catch (Exception ex)
            {
                retVal = "Error: " + ex.Message;
                System.Console.WriteLine("Error al cargar los datos: " + ex.Message);
            }
        }
    }
}
