using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LeaseCheck.Root.Controller
{
    public class EmpresaController
    {
        public Empresa GetEmpresa(Empresa filtro)
        {
            Empresa item = new Empresa();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_EMPRESA";
                    cmd.Parameters.AddWithValue("@ID", filtro.emp_id);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        if (dr.Read())
                        {
                            item.emp_id = int.Parse(dr["EMP_ID"].ToString());
                            item.emp_nombre = dr["EMP_NOMBRE"].ToString();
                            item.emp_razon_social = dr["EMP_RAZON_SOCIAL"].ToString();
                            item.emp_rut = int.Parse(dr["EMP_RUT"].ToString());
                            item.emp_dv = dr["EMP_DV"].ToString();
                            item.emp_holding = int.Parse(dr["EMP_HOLDING"].ToString());
                            item.holding_nombre = dr["HOL_NOMBRE"].ToString();
                            item.emp_habilitado = bool.Parse(dr["EMP_HABILITADO"].ToString());
                        }
                    }

                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    item = null;
                }
            }

            return item;
        }

        public List<Empresa> GetEmpresasToken(Empresa empresa)
        {
            List<Empresa> listado = new List<Empresa>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                { 
                    cmd.CommandText = "SEL_EMPRESAS_TOKEN";
                    if (empresa.filtro != null) cmd.Parameters.AddWithValue("@IDS", empresa.filtro);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Empresa item = new Empresa();

                            item.emp_id = int.Parse(dr["EMP_ID"].ToString());
                            item.emp_razon_social = dr["EMP_RAZON_SOCIAL"].ToString();
                            item.emp_rut = int.Parse(dr["EMP_RUT"].ToString());
                            item.emp_dv = dr["EMP_DV"].ToString();
                            item.rut_completo = dr["RUT_COMPLETO"].ToString();
    
                            listado.Add(item);
                        }
                    }

                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    listado = null;
                }
            }

            return listado;
        }

        public List<Empresa> GetEmpresas()
        {
            List<Empresa> listado = new List<Empresa>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                { 
                    cmd.CommandText = "SEL_EMPRESA";

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Empresa item = new Empresa();

                            item.emp_id = int.Parse(dr["EMP_ID"].ToString());
                            item.emp_nombre = dr["EMP_NOMBRE"].ToString();
                            item.emp_razon_social = dr["EMP_RAZON_SOCIAL"].ToString();
                            item.emp_rut = int.Parse(dr["EMP_RUT"].ToString());
                            item.emp_dv = dr["EMP_DV"].ToString();
                            item.emp_holding = int.Parse(dr["EMP_HOLDING"].ToString());
                            item.emp_habilitado = bool.Parse(dr["EMP_HABILITADO"].ToString());
                            item.holding_nombre = dr["HOL_NOMBRE"].ToString();
                            item.rut_completo = item.emp_rut.ToString() + '-' + item.emp_dv;

                            listado.Add(item);
                        }
                    }

                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception s)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    listado = null;
                }
            }

            return listado;
        }

        public Respuesta InsertEmpresa(Empresa item)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = null;

                try
                {
                    cmdExecute = Conexion.GetCommand("INS_EMPRESA");

                    cmdExecute.Parameters.AddWithValue("@NOMBRE", item.emp_nombre);
                    cmdExecute.Parameters.AddWithValue("@RAZON_SOCIAL", item.emp_razon_social);
                    cmdExecute.Parameters.AddWithValue("@RUT", item.emp_rut);
                    cmdExecute.Parameters.AddWithValue("@DV", item.emp_dv);
                    cmdExecute.Parameters.AddWithValue("@HOLDING", item.emp_holding);
                    cmdExecute.Parameters.AddWithValue("@HABILITADO", item.emp_habilitado);
                    cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    respuesta.detalle = "Registro creado con éxito.";
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

        public Respuesta UpdateEmpresa(Empresa item)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                try
                {
                    cmdExecute = Conexion.GetCommand("UPD_EMPRESA");

                    cmdExecute.Parameters.AddWithValue("@ID", item.emp_id);
                    cmdExecute.Parameters.AddWithValue("@NOMBRE", item.emp_nombre);
                    cmdExecute.Parameters.AddWithValue("@RAZON_SOCIAL", item.emp_razon_social);
                    cmdExecute.Parameters.AddWithValue("@RUT", item.emp_rut);
                    cmdExecute.Parameters.AddWithValue("@DV", item.emp_dv);
                    cmdExecute.Parameters.AddWithValue("@HOLDING", item.emp_holding);
                    cmdExecute.Parameters.AddWithValue("@HABILITADO", item.emp_habilitado);
                    cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    respuesta.detalle = "Registro actualizado con éxito.";
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

        public Respuesta DeleteEmpresa(Empresa item)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                try
                {
                    cmdExecute = Conexion.GetCommand("DEL_EMPRESA");

                    cmdExecute.Parameters.AddWithValue("@ID", item.emp_id);

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

        // Relacion Usuarios

        public List<UsuarioEmpresa> GetUsuarioEmpresas(UsuarioEmpresa item)
        {
            List<UsuarioEmpresa> listado = new List<UsuarioEmpresa>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_USUARIO_EMPRESAS";
                    cmd.Parameters.AddWithValue("@USUARIO", item.use_usuario);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            item = new UsuarioEmpresa();

                            item.use_id = int.Parse(dr["UEM_ID"].ToString());
                            item.use_usuario = int.Parse(dr["UEM_USUARIO"].ToString());
                            item.use_empresa = int.Parse(dr["UEM_EMPRESA"].ToString());
                            item.empresa_rut = dr["EMP_RUT"].ToString();
                            item.empresa_nombre = dr["EMP_RAZON_SOCIAL"].ToString();

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

        public List<UsuarioEmpresa> GetEmpresas(UsuarioEmpresa item)
        {
            List<UsuarioEmpresa> listado = new List<UsuarioEmpresa>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_USUARIO_EMPRESAS_ASOCIA";
                    cmd.Parameters.AddWithValue("@USUARIO", item.use_usuario);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            item = new UsuarioEmpresa();

                            item.use_empresa = int.Parse(dr["EMP_ID"].ToString());
                            item.empresa_rut = dr["EMP_RUT"].ToString();
                            item.empresa_nombre = dr["EMP_RAZON_SOCIAL"].ToString();

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

        public Respuesta InsertUsuarioEmpresa(List<UsuarioEmpresa> listado)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = new SqlCommand();

                try
                {
                    foreach (UsuarioEmpresa item in listado)
                    {
                        cmdExecute = Conexion.GetCommand("INS_USUARIO_EMPRESA");
                        cmdExecute.Parameters.AddWithValue("@ID_USUARIO", item.use_usuario);
                        cmdExecute.Parameters.AddWithValue("@ID_EMPRESA", item.use_empresa);
                        cmdExecute.Parameters.AddWithValue("@USUARIO_CREACION", Session.UsuarioId());

                        cmdExecute.ExecuteNonQuery();
                        cmdExecute.Connection.Close();
                    }

                    respuesta.detalle = "Empresa(s) asociada(s) con éxito";
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

        public Respuesta DeleteUsuarioEmpresa(List<UsuarioEmpresa> listado)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = new SqlCommand();

                try
                {
                    foreach (UsuarioEmpresa item in listado)
                    {
                        cmdExecute = Conexion.GetCommand("DEL_USUARIO_EMPRESA");
                        cmdExecute.Parameters.AddWithValue("@ID", item.use_id);
                        cmdExecute.ExecuteNonQuery();
                        cmdExecute.Connection.Close();
                    }

                    respuesta.detalle = "Empresa(s) eliminada(s) con éxito";
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