using System;
using System.Web.UI;
using CursoSM.LogicaNegocio;
using System.Web.Script.Serialization;

namespace WebApp1
{
    public partial class getdata : System.Web.UI.Page
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
                CtrlUsuario negocio = new CtrlUsuario();
                int totalUsuarios = negocio.ContarUsuarios();

                if (totalUsuarios >= 0)
                {
                    retVal = "Total de usuarios: " + totalUsuarios;
                }
                else
                {
                    retVal = "Error al contar usuarios.";
                }

                System.Console.WriteLine(retVal);
            }
            catch (Exception ex)
            {
                retVal = "Error: " + ex.Message;
                System.Console.WriteLine("Error al cargar los datos: " + ex.Message);
            }
        }
    }
}
