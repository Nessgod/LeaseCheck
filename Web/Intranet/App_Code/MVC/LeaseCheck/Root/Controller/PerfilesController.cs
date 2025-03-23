using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LeaseCheck.Root.Model;

namespace LeaseCheck.Root.Controller
{
    public class PerfilesController
    {
        public List<Perfiles> ListoPerfiles(Perfiles perfil)
        {
            List<Perfiles> perfiles = new List<Perfiles>();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = "SEL_PERFILES";
                cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                if (perfil.per_descripcion != "") cmd.Parameters.AddWithValue("@FILTRO", perfil.per_descripcion);

                using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                {
                    while (dr.Read())
                    {
                        perfil = new Perfiles();

                        perfil.per_id = int.Parse(dr["PER_ID"].ToString());
                        perfil.per_nombre = dr["PER_NOMBRE"].ToString();
                        if(!string.IsNullOrEmpty(dr["PER_TIPO"].ToString())) perfil.per_tipo_perfil = int.Parse(dr["PER_TIPO"].ToString());
                        perfil.per_descripcion = dr["PER_DESCRIPCION"].ToString();
                        perfil.per_habilitado = bool.Parse(dr["PER_HABILITADO"].ToString());
                        perfil.tpp_nombre = dr["TPP_NOMBRE"].ToString();

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
        
        public Perfiles GetPerfiles(Perfiles perfil)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = "SEL_PERFILES";
                cmd.Parameters.AddWithValue("@ID", perfil.per_id);


                using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                {
                    if (dr.Read())
                    {
                        perfil = new Perfiles();

                        perfil.per_id = int.Parse(dr["PER_ID"].ToString());
                        perfil.per_nombre = dr["PER_NOMBRE"].ToString();
                        if (!string.IsNullOrEmpty(dr["PER_TIPO"].ToString())) perfil.per_tipo_perfil = int.Parse(dr["PER_TIPO"].ToString());
                        perfil.per_descripcion = dr["PER_DESCRIPCION"].ToString();
                        perfil.per_habilitado = bool.Parse(dr["PER_HABILITADO"].ToString());
                        perfil.tpp_nombre = dr["TPP_NOMBRE"].ToString();
                    }
                }

                cmd.Connection.Close();
                cmd.Dispose();

                return perfil;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                cmd.Dispose();

                return null;
            }
        }

        public List<Cliente> GetCliente()
        {
            List<Cliente> listado = new List<Cliente>();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = "SEL_CLIENTE";

                using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                {
                    while (dr.Read())
                    {
                        Cliente item = new Cliente();

                        item.cli_id = int.Parse(dr["CLI_ID"].ToString());
                        item.cli_nombre = dr["CLI_NOMBRE"].ToString();

                        listado.Add(item);
                    }
                }

                cmd.Connection.Close();
                cmd.Dispose();

                return listado;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                cmd.Dispose();

                return null;
            }
        }

        public Respuesta InsertItem(Perfiles perfil)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            try
            {
                int id = 0;
                cmdExecute = Conexion.GetCommand("INS_PERFIL");
                cmdExecute.Parameters.AddWithValue("@ID", id).Direction = System.Data.ParameterDirection.Output;
                cmdExecute.Parameters.AddWithValue("@NOMBRE", perfil.per_nombre);
                cmdExecute.Parameters.AddWithValue("@TIPO_PERFIL", perfil.per_tipo_perfil);
                cmdExecute.Parameters.AddWithValue("@DESCRIPCION", perfil.per_descripcion);
                cmdExecute.Parameters.AddWithValue("@HABILITADO", perfil.per_habilitado);
                cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());

                //if (!string.IsNullOrEmpty(perfil.empresas)) cmdExecute.Parameters.AddWithValue("@EMPRESAS", perfil.empresas);

                cmdExecute.ExecuteNonQuery();
                cmdExecute.Connection.Close();

                if (cmdExecute.Parameters["@ID"].Value.ToString() != "")
                    id = (int)cmdExecute.Parameters["@ID"].Value;

                respuesta.codigo = id;
                respuesta.detalle = "Registro insertado con exito.";
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

            return respuesta;
        }

        public Respuesta UpdateItem(Perfiles perfil)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            try
            {
                cmdExecute = Conexion.GetCommand("UPD_PERFIL");

                cmdExecute.Parameters.AddWithValue("@ID", perfil.per_id);
                cmdExecute.Parameters.AddWithValue("@NOMBRE", perfil.per_nombre);
                cmdExecute.Parameters.AddWithValue("@TIPO_PERFIL", perfil.per_tipo_perfil);
                cmdExecute.Parameters.AddWithValue("@DESCRIPCION", perfil.per_descripcion);
                cmdExecute.Parameters.AddWithValue("@HABILITADO", perfil.per_habilitado);
                cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());

                //if (!string.IsNullOrEmpty(perfil.empresas)) cmdExecute.Parameters.AddWithValue("@EMPRESAS", perfil.empresas);

                cmdExecute.ExecuteNonQuery();
                cmdExecute.Connection.Close();

                respuesta.detalle = "Registro actualizado con éxito.";
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

            return respuesta;
        }

        public Respuesta DeletePerfil(Perfiles item)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                try
                {
                    cmdExecute = Conexion.GetCommand("DEL_PERFILES");

                    cmdExecute.Parameters.AddWithValue("@ID", item.per_id);

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    respuesta.detalle = "Registro eliminado con éxito.";
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

        //Relacion Usuarios
        public List<UsuarioPerfiles> GetUsuarioPerfiles(UsuarioPerfiles item)
        {
            List<UsuarioPerfiles> listado = new List<UsuarioPerfiles>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_USUARIO_PERFILES";
                    cmd.Parameters.AddWithValue("@USUARIO", item.upe_usuario);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            item = new UsuarioPerfiles();

                            item.upe_perfil = int.Parse(dr["PER_ID"].ToString());
                            item.upe_id = int.Parse(dr["UPE_ID"].ToString());
                            item.perfil_nombre = dr["PER_NOMBRE"].ToString();
                            item.perfil_habilitado = bool.Parse(dr["PER_HABILITADO"].ToString());

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

        public List<UsuarioPerfiles> GetPerfiles(UsuarioPerfiles item)
        {
            List<UsuarioPerfiles> listado = new List<UsuarioPerfiles>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_USUARIO_PERFILES_ASOCIAR";
                    cmd.Parameters.AddWithValue("@USUARIO", item.upe_usuario);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            item = new UsuarioPerfiles();

                            item.upe_perfil = int.Parse(dr["PER_ID"].ToString());
                            item.perfil_nombre = dr["PER_NOMBRE"].ToString();
                            item.perfil_habilitado = bool.Parse(dr["PER_HABILITADO"].ToString());

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

        public Respuesta InsertUsuarioPerfiles(List<UsuarioPerfiles> listado)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = new SqlCommand();

                try
                {
                    foreach (UsuarioPerfiles item in listado)
                    {
                        cmdExecute = Conexion.GetCommand("INS_USUARIO_PERFIL");
                        cmdExecute.Parameters.AddWithValue("@USUARIO", item.upe_usuario);
                        cmdExecute.Parameters.AddWithValue("@PERFIL", item.upe_perfil);

                        cmdExecute.ExecuteNonQuery();
                        cmdExecute.Connection.Close();
                    }

                    respuesta.detalle = "Perfil(es) asociado(s) con éxito";
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

        public Respuesta DeleteUsuarioPerfiles(List<UsuarioPerfiles> listado)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = new SqlCommand();

                try
                {
                    foreach (UsuarioPerfiles item in listado)
                    {
                        cmdExecute = Conexion.GetCommand("DEL_USUARIO_PERFIL");
                        cmdExecute.Parameters.AddWithValue("@ID", item.upe_id);
                        cmdExecute.ExecuteNonQuery();
                        cmdExecute.Connection.Close();
                    }

                    respuesta.detalle = "Perfil(es) asociado(s) con éxito";
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

        public DataTable GetAllPerfilesDataset()
        {
            DataTable dat = new DataTable();
            SqlDataAdapter adap = new SqlDataAdapter("SEL_ALLPERFILESUSER", Conexion.GetConnectionString());
            adap.Fill(dat);
            return dat;


        }

        public void ReporteInforme (Perfiles perfil)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "RPT_PERFILES";
            if (perfil.per_descripcion != "") cmd.Parameters.AddWithValue("@FILTRO", perfil.per_descripcion);

            string filename = " INFORME PERFILES " + DateTime.Now;
            Tools.Excel.exportExcel(Conexion.GetDataTable(cmd), filename, true);
        }
    }
}