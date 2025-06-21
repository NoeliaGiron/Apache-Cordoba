using System;
using System.Web.UI;
using CursoSM.LogicaNegocio;

namespace WebApp1
{
    public partial class postdata : System.Web.UI.Page
    {
        public string retVal = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now);
            Response.Cache.SetNoServerCaching();
            Response.Cache.SetNoStore();

            string action = Request.Params["action"];
            if (string.IsNullOrEmpty(action))
            {
                retVal = "ERROR: action parameter is missing.";
                return;
            }

            CtrlUsuario negocio = new CtrlUsuario();

            switch (action.ToLower())
            {
                case "buscar":
                    string nombre = Request.Params["nombre"];
                    if (string.IsNullOrEmpty(nombre))
                    {
                        retVal = "ERROR: nombre parameter is missing.";
                    }
                    else
                    {
                        var usuarios = negocio.BuscarUsuario(nombre);
                        if (usuarios != null && usuarios.Count > 0)
                        {
                            var json = new System.Web.Script.Serialization.JavaScriptSerializer();
                            retVal = json.Serialize(usuarios);
                        }
                        else
                        {
                            retVal = "No se encontraron usuarios.";
                        }
                    }
                    break;

                case "registrar":
                    string username = Request.Params["username"];
                    string password = Request.Params["password"];
                    string fullname = Request.Params["fullname"];
                    string celular = Request.Params["celular"];
                    string email = Request.Params["email"];
                    string fechaNacStr = Request.Params["fechaNac"];
                    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(fullname) ||
                        string.IsNullOrEmpty(celular) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(fechaNacStr))
                    {
                        retVal = "ERROR: Missing parameters for registration.";
                    }
                    else
                    {
                        DateTime fechaNac;
                        if (DateTime.TryParse(fechaNacStr, out fechaNac))
                        {
                            int result = negocio.RegistrarUsuario(username, password, fullname, celular, email, fechaNac);
                            if (result == 1)
                            {
                                retVal = "Usuario registrado exitosamente.";
                            }
                            else
                            {
                                retVal = "Error al registrar el usuario.";
                            }
                        }
                        else
                        {
                            retVal = "ERROR: Invalid fechaNac parameter.";
                        }
                    }
                    break;

                default:
                    retVal = "ERROR: Invalid action parameter.";
                    break;
            }
        }
    }
}
