using System;
using System.Collections;
using System.Linq;
using CursoSM.AccesoDatos;

namespace CursoSM.LogicaNegocio
{
    public class CtrlUsuario
    {
        RESERVAMEEntities DBcontext = new RESERVAMEEntities();

        public string getFullName(string pUsername)
        {
            try
            {
                var query = (from c in DBcontext.USUARIO
                             where c.cUsername.Equals(pUsername)
                             select new
                             {
                                 cFullname = c.cFullname
                             }).FirstOrDefault();
                if (query == null)
                {
                    return "";
                }
                else
                {
                    return query.cFullname;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return null;
            }
        }

        public IList ObtenerUsuarios()
        {
            try
            {
                var query = (from c in DBcontext.USUARIO
                             select new
                             {
                                 cUsername = c.cUsername,
                                 cFullname = c.cFullname,
                                 cCelular = c.cCelular,
                                 cEmail = c.cEmail,
                                 dFechaNac = c.dFechaNac
                             }).ToList();

                System.Console.WriteLine("Total de usuarios encontrados: " + query.Count);

                foreach (var usuario in query)
                {
                    System.Console.WriteLine("Usuario: " + usuario.cUsername + ", Nombre Completo: " + usuario.cFullname);
                }

                return query;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error al obtener usuarios: " + ex.Message);
                return null;
            }
        }

        public int ContarUsuarios()
        {
            try
            {
                return DBcontext.USUARIO.Count();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error al contar usuarios: " + ex.Message);
                return -1;
            }
        }

        public IList BuscarUsuario(string pFullname)
        {
            try
            {
                var query = (from c in DBcontext.USUARIO
                             where c.cFullname.Contains(pFullname)
                             orderby c.cFullname
                             select new
                             {
                                 cUsername = c.cUsername,
                                 cFullname = c.cFullname,
                                 cCelular = c.cCelular,
                                 cEmail = c.cEmail,
                                 dFechaNa = c.dFechaNac,
                             }).Take(10).ToList().Select(x => new
                             {
                                 cUsername = x.cUsername,
                                 cFullname = x.cFullname,
                                 cCelular = x.cCelular,
                                 cEmail = x.cEmail,
                                 dFechaNa = string.Format("{0:dd/MM/yyyy}", x.dFechaNa),
                             }).ToList();
                return query;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw; // Esto permitirá ver el mensaje de error exacto en los registros del servidor.
            }
        }

        public int RegistrarUsuario(string pUsername, string pPassword, string pFullname, string pCelular, string pEmail, DateTime pFechaNac)
        {
            try
            {
                USUARIO query = new USUARIO();
                query.cUsername = pUsername;
                query.cPassword = pPassword;
                query.cFullname = pFullname;
                query.cCelular = pCelular;
                query.cEmail = pEmail;
                query.dFechaNac = pFechaNac;
                DBcontext.USUARIO.Add(query);
                DBcontext.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                return -1;
            }
        }
    }
}
