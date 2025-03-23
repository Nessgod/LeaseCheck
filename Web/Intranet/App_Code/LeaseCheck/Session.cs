using System.Web;
using System.Web.SessionState;

namespace LeaseCheck
{
    public class Session
    {
        private static HttpSessionState Session1
        {
            get { return HttpContext.Current.Session; }
        }

        public static string UsuarioId()
        {
            if (Session1 != null && Session1["usu_id"] != null)
            {
                return HttpContext.Current.Session["usu_id"].ToString();
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/Login.aspx");
                return "";
            }
        }

        public static string UsuarioLogin()
        {
            if (Session1 != null && Session1["usu_login"] != null)
            {
                return HttpContext.Current.Session["usu_login"].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string UsuarioNombre()
        {
            if (Session1 != null && Session1["usu_Nombre"] != null)
            {
                return HttpContext.Current.Session["usu_Nombre"].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string UsuarioApellidoPaterno()
        {
            if (Session1 != null && Session1["usu_apellido_paterno"] != null)
            {
                return HttpContext.Current.Session["usu_apellido_paterno"].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string UsuarioApellidoMaterno()
        {
            if (Session1 != null && Session1["usu_apellido_materno"] != null)
            {
                return HttpContext.Current.Session["usu_apellido_materno"].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string UsuarioNombreCompleto()
        {
            if (Session1 != null && Session1["usu_Nombre"] != null && Session1["usu_apellido_paterno"] != null && Session1["usu_apellido_materno"] != null)
            {
                return HttpContext.Current.Session["usu_Nombre"].ToString() + " " + HttpContext.Current.Session["usu_apellido_paterno"].ToString() + " " + HttpContext.Current.Session["usu_apellido_materno"].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string UsuarioRut()
        {
            if (Session1 != null && Session1["usu_rut"] != null)
            {
                return HttpContext.Current.Session["usu_rut"].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string UsuarioRutDv()
        {
            if (Session1 != null && Session1["usu_dv"] != null)
            {
                return HttpContext.Current.Session["usu_dv"].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string UsuarioPerfil()
        {
            if (Session1 != null && Session1["usu_perfil"] != null)
            {
                return HttpContext.Current.Session["usu_perfil"].ToString();
            }
            else
            {
                HttpContext.Current.Response.Redirect("~/Login.aspx");
                return "";
            }
        }

        public static string RemoteHost()
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return "";
            }
        }

        public static string UsuarioCorreo()
        {
            if (Session1 != null && Session1["usu_correo"] != null)
            {
                return HttpContext.Current.Session["usu_correo"].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string UsuarioPassword()
        {
            if (Session1 != null && Session1["usu_password"] != null)
            {
                return HttpContext.Current.Session["usu_password"].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string UsuarioFono()
        {
            if (Session1 != null && Session1["usu_fono"] != null)
            {
                return HttpContext.Current.Session["usu_fono"].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string UsuarioArea()
        {
            if (Session1 != null && Session1["usu_area"] != null)
            {
                return HttpContext.Current.Session["usu_area"].ToString();
            }
            else
            {
                return "";
            }
        }

        public static string UsuarioCambioPassword()
        {
            if (Session1 != null && Session1["usu_cambio_password"] != null)
            {
                return HttpContext.Current.Session["usu_cambio_password"].ToString();
            }
            else
            {
                return "false";
            }
        }

        public static string UsuarioFoto()
        {
            if (Session1 != null && Session1["usu_foto"] != null)
            {
                return HttpContext.Current.Session["usu_foto"].ToString();
            }
            else
            {
                return null;
            }
        }

        public static string Pais(string pais = "")
        {           
            if (pais != "")
            {
                HttpContext.Current.Session["Pais"] = pais;
                
            }
            else 
            {
                if (Session1 != null && Session1["Pais"] != null)
                {
                    pais = HttpContext.Current.Session["Pais"].ToString();
                }
                else
                {
                    HttpContext.Current.Response.Redirect("~/Login.aspx");
                }
            }

            return pais;

        }

        public static string PaisBase()
        {
            if (Session1 != null && Session1["pais_base"] != null)
            {
                return HttpContext.Current.Session["pais_base"].ToString();
            }
            else
            {
                return null;
            }
        }

        public static string Usuario_Es_Cliente()
        {
            if (Session1 != null && Session1["usu_es_cliente"] != null)
            {
                return HttpContext.Current.Session["usu_es_cliente"].ToString();
            }
            else
            {
                return null;
            }
        }

    }
}