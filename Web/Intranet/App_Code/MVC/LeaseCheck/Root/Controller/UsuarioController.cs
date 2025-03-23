using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using LeaseCheck.Root.Model;

namespace LeaseCheck.Root.Controller
{
    public class UsuarioController
    {
        public Respuesta GetUsuarioLogin(Usuarios usuario)
        {
            Respuesta respuesta = new Respuesta();

            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = "SEL_USUARIOS";
                cmd.Parameters.AddWithValue("@LOGIN", usuario.usu_login);
                cmd.Parameters.AddWithValue("@DEVUELVE_FOTO", true);

                using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                {
                    if (dr.Read())
                    {
                        if (dr["USU_PASSWORD"].ToString() == usuario.usu_password)
                        {
                            if (dr["USU_ID"].ToString() == "1")
                            {
                                System.Web.Security.FormsAuthentication.SetAuthCookie(usuario.usu_login, false);

                                //llena las variables de session 
                                HttpContext.Current.Session["usu_id"] = dr["USU_ID"].ToString();
                                HttpContext.Current.Session["usu_login"] = dr["USU_LOGIN"].ToString();
                                HttpContext.Current.Session["usu_password"] = dr["USU_PASSWORD"].ToString();
                                HttpContext.Current.Session["usu_nombre"] = dr["USU_NOMBRE"].ToString();
                                HttpContext.Current.Session["usu_apellido_paterno"] = dr["USU_APELLIDO_PATERNO"].ToString();
                                HttpContext.Current.Session["usu_apellido_materno"] = dr["USU_APELLIDO_MATERNO"].ToString();
                                HttpContext.Current.Session["usu_correo"] = dr["USU_CORREO"].ToString();
                                HttpContext.Current.Session["usu_fono"] = dr["USU_FONO"].ToString();
                                HttpContext.Current.Session["usu_habilitado"] = dr["USU_HABILITADO"].ToString();
                                HttpContext.Current.Session["usu_perfil"] = dr["ID_PERFILES"].ToString();

                                HttpContext.Current.Session["usu_es_cliente"] = dr["USU_ES_CLIENTE"].ToString();
                                usuario.usu_pais = int.Parse(dr["USU_PAIS"].ToString());
                                HttpContext.Current.Session["pais_base"] = dr["USU_PAIS"].ToString();

                                if (dr["USU_FOTO"].ToString() != "")
                                {
                                    byte[] byteFoto = (byte[])dr["USU_FOTO"];
                                    HttpContext.Current.Session["usu_foto"] = Convert.ToBase64String(byteFoto, 0, byteFoto.Length);
                                }

                                respuesta.codigo = int.Parse(dr["USU_ID"].ToString());
                                respuesta.detalle = dr["USU_NOMBRE"].ToString() + " " + dr["USU_APELLIDO_PATERNO"].ToString() + " " + dr["USU_APELLIDO_MATERNO"].ToString();
                                respuesta.error = false;
                            }
                            else
                            { 
                                if (dr["USU_HABILITADO"].ToString() == "True")
                                {
                                    if (dr["CLI_HABILITADO"].ToString() == "True")
                                    {
                                        System.Web.Security.FormsAuthentication.SetAuthCookie(usuario.usu_login, false);

                                        //llena las variables de session 
                                        HttpContext.Current.Session["usu_id"] = dr["USU_ID"].ToString();
                                        HttpContext.Current.Session["usu_login"] = dr["USU_LOGIN"].ToString();
                                        HttpContext.Current.Session["usu_password"] = dr["USU_PASSWORD"].ToString();
                                        HttpContext.Current.Session["usu_nombre"] = dr["USU_NOMBRE"].ToString();
                                        HttpContext.Current.Session["usu_apellido_paterno"] = dr["USU_APELLIDO_PATERNO"].ToString();
                                        HttpContext.Current.Session["usu_apellido_materno"] = dr["USU_APELLIDO_MATERNO"].ToString();
                                        HttpContext.Current.Session["usu_correo"] = dr["USU_CORREO"].ToString();
                                        HttpContext.Current.Session["usu_fono"] = dr["USU_FONO"].ToString();
                                        HttpContext.Current.Session["usu_habilitado"] = dr["USU_HABILITADO"].ToString();
                                        HttpContext.Current.Session["usu_perfil"] = dr["ID_PERFILES"].ToString();

                                        HttpContext.Current.Session["usu_es_cliente"] = dr["USU_ES_CLIENTE"].ToString();
                                        usuario.usu_pais = int.Parse(dr["USU_PAIS"].ToString());
                                        HttpContext.Current.Session["pais_base"] = dr["USU_PAIS"].ToString();

                                        if (dr["USU_FOTO"].ToString() != "")
                                        {
                                            byte[] byteFoto = (byte[])dr["USU_FOTO"];
                                            HttpContext.Current.Session["usu_foto"] = Convert.ToBase64String(byteFoto, 0, byteFoto.Length);
                                        }

                                        respuesta.codigo = int.Parse(dr["USU_ID"].ToString());
                                        respuesta.detalle = dr["USU_NOMBRE"].ToString() + " " + dr["USU_APELLIDO_PATERNO"].ToString() + " " + dr["USU_APELLIDO_MATERNO"].ToString();
                                        respuesta.error = false;

                                    }
                                    else
                                    {
                                        respuesta.codigo = 0;
                                        respuesta.error = true;

                                        if (dr["CLI_ES_DEMO"].ToString() == "True")
                                            respuesta.detalle = "Cliente DEMO Inactivo";
                                        else
                                            respuesta.detalle = "Cliente Inactivo";
                                    }
                                }
                                else
                                {
                                    respuesta.codigo = 0;
                                    respuesta.detalle = "Su cuenta '" + usuario.usu_login + "' está deshabilitada. Comuniquese con el administrador del sitio.";
                                    respuesta.error = true;
                                } 
                            }
                        }
                        else
                        {
                            respuesta.codigo = 0;
                            respuesta.detalle = "Contraseña invalida";
                            respuesta.error = true;
                        }
                    }
                    else
                    {
                        respuesta.codigo = 0;
                        respuesta.detalle = "Cuenta No Existe";
                        respuesta.error = true;
                    }
                }

                cmd.Connection.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                cmd.Dispose();

                respuesta.error = true;
                respuesta.detalle = ex.Message;
            }

            return respuesta;
        }

        public List<Usuarios> GetUsuarios(Usuarios usuario = null)
        {
            List<Usuarios> usuarios = new List<Usuarios>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                usuario.usu_habilitado = true;
                try
                {
                    cmd.CommandText = "SEL_USUARIOS";
                    //if (usuario.usu_habilitado != null) cmd.Parameters.AddWithValue("@HABILITADO", usuario.usu_habilitado);
                    if (usuario.usu_id > 0) cmd.Parameters.AddWithValue("@ID", usuario.usu_id);
                    if (usuario.filtro != "" ) cmd.Parameters.AddWithValue("@FILTRO", usuario.filtro);
                    if (usuario.filtro_Cliente != "") cmd.Parameters.AddWithValue("@Cliente", usuario.filtro_Cliente);
                    if (usuario.filtro_Pais != "") cmd.Parameters.AddWithValue("@PAIS", usuario.filtro_Pais);

                    cmd.Parameters.AddWithValue("@ES_CLIENTE", usuario.es_cliente);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            usuario = new Usuarios();

                            usuario.usu_id = int.Parse(dr["USU_ID"].ToString());
                            usuario.usu_login = dr["USU_LOGIN"].ToString();
                            usuario.usu_password = dr["USU_PASSWORD"].ToString();
                            usuario.usu_nombres = dr["USU_NOMBRE"].ToString();
                            usuario.usu_apellido_paterno = dr["USU_APELLIDO_PATERNO"].ToString();
                            usuario.usu_apellido_materno = dr["USU_APELLIDO_MATERNO"].ToString();
                            usuario.nombreCompleto = usuario.usu_nombres + " " + usuario.usu_apellido_paterno + " " + usuario.usu_apellido_materno;
                            usuario.usu_correo = dr["USU_CORREO"].ToString();
                            usuario.usu_fono = dr["USU_FONO"].ToString();
                            usuario.usu_habilitado = bool.Parse(dr["USU_HABILITADO"].ToString());

                            usuario.es_cliente = bool.Parse(dr["USU_ES_CLIENTE"].ToString());
                            usuario.usu_perfil = int.Parse(dr["USU_PERFIL"].ToString());
                            usuario.usu_pais = int.Parse(dr["USU_PAIS"].ToString());
                            usuario.cliente_nombre = dr["CLIENTE"].ToString();

                            usuarios.Add(usuario);
                        }
                    }

                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    usuarios = null;
                }
            }

            return usuarios;
        }

        public Usuarios GetUsuario(Usuarios usuario)
        {
            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_USUARIOS";
                    cmd.Parameters.AddWithValue("@ID", usuario.usu_id);
                    cmd.Parameters.AddWithValue("@DEVUELVE_FOTO", true);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        if (dr.Read())
                        {
                            usuario = new Usuarios();

                            usuario.usu_id = int.Parse(dr["USU_ID"].ToString());
                            usuario.usu_login = dr["USU_LOGIN"].ToString();
                            usuario.usu_password = dr["USU_PASSWORD"].ToString();
                            usuario.usu_nombres = dr["USU_NOMBRE"].ToString();
                            usuario.usu_apellido_paterno = dr["USU_APELLIDO_PATERNO"].ToString();
                            usuario.usu_apellido_materno = dr["USU_APELLIDO_MATERNO"].ToString();
                            usuario.usu_correo = dr["USU_CORREO"].ToString();
                            usuario.usu_fono = dr["USU_FONO"].ToString();
                            usuario.usu_habilitado = bool.Parse(dr["USU_HABILITADO"].ToString());
                            usuario.usuario_creacion = dr["USUARIO_CREACION"].ToString();
                            usuario.usu_fecha_creacion = DateTime.Parse(dr["USU_FECHA_CREACION"].ToString());
                            usuario.usu_host_creacion = dr["USU_HOST_CREACION"].ToString();
                            usuario.usuario_act = dr["USUARIO_ACT"].ToString();
                            usuario.usu_fecha_act = DateTime.Parse(dr["USU_FECHA_ACT"].ToString());
                            usuario.usu_host_act = dr["USU_HOST_ACT"].ToString();
                            usuario.es_cliente = bool.Parse(dr["USU_ES_CLIENTE"].ToString());
                            usuario.usu_perfil = int.Parse(dr["USU_PERFIL"].ToString());
                            usuario.usu_pais = int.Parse(dr["USU_PAIS"].ToString());

                            if (dr["USU_ULTIMO_LOGIN"].ToString() != "") usuario.usu_ultimo_login = DateTime.Parse(dr["USU_ULTIMO_LOGIN"].ToString());

                            if (dr["USU_FOTO"].ToString() != "") usuario.usu_foto = (byte[])dr["USU_FOTO"];

                        }
                    }

                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    usuario = null;
                }
            }

            return usuario;
        }

        public Respuesta InsertUsuario(Usuarios usuario)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = null;

                try
                {
                    int id = 0;

                    cmdExecute = Conexion.GetCommand("INS_USUARIOS");
                    cmdExecute.Parameters.AddWithValue("@ID", id).Direction = System.Data.ParameterDirection.Output;
                    cmdExecute.Parameters.AddWithValue("@LOGIN", usuario.usu_login);
                    cmdExecute.Parameters.AddWithValue("@PASSWORD", usuario.usu_password);
                    cmdExecute.Parameters.AddWithValue("@NOMBRES", usuario.usu_nombres);
                    cmdExecute.Parameters.AddWithValue("@APELLIDO_PATERNO", usuario.usu_apellido_paterno);
                    cmdExecute.Parameters.AddWithValue("@APELLIDO_MATERNO", usuario.usu_apellido_materno);
                    cmdExecute.Parameters.AddWithValue("@CORREO", usuario.usu_correo);
                    cmdExecute.Parameters.AddWithValue("@FONO", usuario.usu_fono);
                    cmdExecute.Parameters.AddWithValue("@FOTO", usuario.usu_foto);
                    cmdExecute.Parameters.AddWithValue("@HABILITADO", usuario.usu_habilitado);
                    cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    cmdExecute.Parameters.AddWithValue("@HOST", Session.RemoteHost());
                    cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());

                    cmdExecute.Parameters.AddWithValue("@ES_CLIENTE", usuario.es_cliente);
                    cmdExecute.Parameters.AddWithValue("@PERFIL", usuario.usu_perfil);
                    cmdExecute.Parameters.AddWithValue("@PAIS_COMBOBOX", usuario.usu_pais);

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                  
                    id = (int)cmdExecute.Parameters["@ID"].Value;

                    respuesta.codigo = id;
                    respuesta.detalle = "El usuario fue creado con éxito.";
                    respuesta.error = false;

                    
                }
                catch (Exception ex)
                {
                    cmdExecute.Connection.Close();
                    cmdExecute.Dispose();
                    respuesta.codigo = -1;
                    respuesta.detalle = ex.Message;
                    respuesta.error = true;
                }
            }

            return respuesta;
        }

        public Respuesta UpdateUsuario(Usuarios usuario)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                try
                {
                    cmdExecute = Conexion.GetCommand("UPD_USUARIOS");
                    cmdExecute.Parameters.AddWithValue("@ID", usuario.usu_id);
                    cmdExecute.Parameters.AddWithValue("@LOGIN", usuario.usu_login);
                    cmdExecute.Parameters.AddWithValue("@PASSWORD", usuario.usu_password);
                    cmdExecute.Parameters.AddWithValue("@NOMBRES", usuario.usu_nombres);
                    cmdExecute.Parameters.AddWithValue("@APELLIDO_PATERNO", usuario.usu_apellido_paterno);
                    cmdExecute.Parameters.AddWithValue("@APELLIDO_MATERNO", usuario.usu_apellido_materno);
                    cmdExecute.Parameters.AddWithValue("@CORREO", usuario.usu_correo);
                    cmdExecute.Parameters.AddWithValue("@FONO", usuario.usu_fono);
                    cmdExecute.Parameters.AddWithValue("@FOTO", usuario.usu_foto);
                    cmdExecute.Parameters.AddWithValue("@HABILITADO", usuario.usu_habilitado);
                    cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    cmdExecute.Parameters.AddWithValue("@HOST", Session.RemoteHost());
                    cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());
                    cmdExecute.Parameters.AddWithValue("@PERFIL", usuario.usu_perfil);
                    cmdExecute.Parameters.AddWithValue("@PAIS_COMBOBOX", usuario.usu_pais);

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    respuesta.codigo = 0;
                    respuesta.detalle = "Usuario actualizado con éxito.";
                    respuesta.error = false;

                }
                catch (Exception ex)
                {
                    cmdExecute.Connection.Close();
                    cmdExecute.Dispose();

                    respuesta.codigo = -1;
                    respuesta.detalle = ex.Message;
                    respuesta.error = true;
                }
            }

            return respuesta;
        }

        public Respuesta DeleteUsuario(Usuarios usuario)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                try
                {
                    cmdExecute = Conexion.GetCommand("DEL_USUARIO");
                    cmdExecute.Parameters.AddWithValue("@ID", usuario.usu_id);

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    respuesta.codigo = 0;
                    respuesta.detalle = "Usuario eliminado con éxito.";
                    respuesta.error = false;

                }
                catch (Exception ex)
                {
                    cmdExecute.Connection.Close();
                    cmdExecute.Dispose();

                    respuesta.codigo = -1;
                    respuesta.detalle = ex.Message;
                    respuesta.error = true;
                }
            }

            return respuesta;
        }

        public Respuesta ResetPassword(Usuarios usuario)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                try
                {
                    cmdExecute = Conexion.GetCommand("UPD_USUARIO_RESET_PASSWORD");
                    cmdExecute.Parameters.AddWithValue("@ID", usuario.usu_id);

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    respuesta.codigo = 0;
                    respuesta.detalle = "Password reseteado con éxito.";
                    respuesta.error = false;

                }
                catch (Exception ex)
                {
                    cmdExecute.Connection.Close();
                    cmdExecute.Dispose();

                    respuesta.codigo = -1;
                    respuesta.detalle = ex.Message;
                    respuesta.error = true;
                }
            }

            return respuesta;
        }

        public List<Perfiles> GetPerfiles(Perfiles filtro = null)
        {
            List<Perfiles> perfiles = new List<Perfiles>();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = "SEL_PERFILES";
                if (filtro != null) if (filtro.per_habilitado) cmd.Parameters.AddWithValue("@HABILITADO", true);

                using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                {
                    while (dr.Read())
                    {
                        Perfiles perfil = new Perfiles();

                        perfil.per_id = int.Parse(dr["PER_ID"].ToString());
                        perfil.per_nombre = dr["PER_NOMBRE"].ToString();
                        if (!string.IsNullOrEmpty(dr["PER_TIPO"].ToString())) perfil.per_tipo_perfil = int.Parse(dr["PER_TIPO"].ToString());
                        perfil.per_descripcion = dr["PER_DESCRIPCION"].ToString();
                        perfil.per_habilitado = bool.Parse(dr["PER_HABILITADO"].ToString());

                        perfiles.Add(perfil);
                    }
                }

                cmd.Connection.Close();
                cmd.Dispose();

                return perfiles;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                cmd.Dispose();

                return null;
            }
        }

        public List<Paises> GetPaises(Paises filtro = null)
        {
            List<Paises> paises = new List<Paises>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_PAISES";
                    if (filtro != null) if (filtro.pai_habilitado) cmd.Parameters.AddWithValue("@HABILITADO", true);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Paises pais = new Paises();

                            pais.pai_id = int.Parse(dr["PAI_ID"].ToString());
                            pais.pai_nombre = dr["PAI_NOMBRE"].ToString();
                            pais.pai_fecha_creacion = DateTime.Parse(dr["PAI_FECHA_CREACION"].ToString());
                            pais.pai_habilitado = bool.Parse(dr["PAI_HABILITADO"].ToString());

                            paises.Add(pais);
                        }
                    }
                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    paises = null;
                }
            }
            return paises;
        }

        public List<Cliente> GetClientes()
        {
            List<Cliente> listado = new List<Cliente>();
            Cliente cliente = new Cliente();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_CLIENTE";
                    cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Cliente item = new Cliente();

                            item.cli_id = int.Parse(dr["CLI_ID"].ToString());
                            item.cli_nombre = dr["CLI_NOMBRE"].ToString();
                            item.cli_giro = dr["CLI_GIRO"].ToString();
                            item.cli_rut = int.Parse(dr["CLI_RUT"].ToString());
                            item.cli_dv = item.cli_rut.ToString() + "-" + dr["CLI_DV"].ToString();
                            item.cli_habilitado = bool.Parse(dr["CLI_HABILITADO"].ToString());
                            item.cli_alias = dr["CLI_ALIAS"].ToString();
                            item.cli_pais = int.Parse(dr["CLI_PAIS"].ToString());
                            item.cli_comuna = int.Parse(dr["CLI_COMUNA"].ToString());
                            item.cli_direccion = dr["CLI_DIRECCION"].ToString();
                            item.cli_email = dr["CLI_EMAIL"].ToString();
                            item.cli_telefono = dr["CLI_TELEFONO"].ToString();
                            item.cli_es_demo = Boolean.Parse(dr["CLI_ES_DEMO"].ToString());
                            item.cli_contacto_nombre = dr["CLI_CONTACTO_NOMBRE"].ToString();
                            item.cli_contacto_email = dr["CLI_CONTACTO_EMAIL"].ToString();
                            item.cli_contacto_telefono = dr["CLI_CONTACTO_TELEFONO"].ToString();

                            listado.Add(item);
                        }
                    }

                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    listado = null;
                }
            }

            return listado;
        }

        public void InformeUsuarios(Usuarios usuario)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "RPT_USUARIOS";

            if (usuario.usu_id > 0) cmd.Parameters.AddWithValue("@ID", usuario.usu_id);
            if (usuario.filtro != "") cmd.Parameters.AddWithValue("@FILTRO", usuario.filtro);
            if (usuario.es_cliente != null) cmd.Parameters.AddWithValue("@ES_CLIENTE", usuario.es_cliente);
            if (usuario.filtro_Cliente != "") cmd.Parameters.AddWithValue("@Cliente", usuario.filtro_Cliente);

            string filename = " INFORME USUARIOS " + DateTime.Now;
            Tools.Excel.exportExcel(Conexion.GetDataTable(cmd), filename, true);
        }

        public Respuesta UpdateMiCuenta(Usuarios usuario)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                try
                {
                    cmdExecute = Conexion.GetCommand("UPD_MICUENTA");
                    cmdExecute.Parameters.AddWithValue("@ID", usuario.usu_id);
                    cmdExecute.Parameters.AddWithValue("@LOGIN", usuario.usu_login);
                    cmdExecute.Parameters.AddWithValue("@PASSWORD", usuario.usu_password);
                    cmdExecute.Parameters.AddWithValue("@NOMBRES", usuario.usu_nombres);
                    cmdExecute.Parameters.AddWithValue("@APELLIDO_PATERNO", usuario.usu_apellido_paterno);
                    cmdExecute.Parameters.AddWithValue("@APELLIDO_MATERNO", usuario.usu_apellido_materno);
                    cmdExecute.Parameters.AddWithValue("@CORREO", usuario.usu_correo);

                    cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    cmdExecute.Parameters.AddWithValue("@HOST", Session.RemoteHost());
                    cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();
                    cmdExecute.Dispose();

                    respuesta.codigo = 0;
                    respuesta.detalle = "Usuario actualizado con éxito.";
                    respuesta.error = false;

                }
                catch (Exception ex)
                {
                    cmdExecute.Connection.Close();
                    cmdExecute.Dispose();

                    respuesta.codigo = -1;
                    respuesta.detalle = ex.Message;
                    respuesta.error = true;
                }
            }

            return respuesta;
        }
    }
}