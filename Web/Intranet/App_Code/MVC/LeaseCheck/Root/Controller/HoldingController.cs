using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LeaseCheck.Root.Controller
{
    public class HoldingController
    {
        public Holding GetHolding(Holding filtro)
        {
            Holding item = new Holding();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_HOLDING";
                    cmd.Parameters.AddWithValue("@ID", filtro.hol_id);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        if (dr.Read())
                        {
                            item.hol_id = int.Parse(dr["HOL_ID"].ToString());
                            item.hol_nombre = dr["HOL_NOMBRE"].ToString();
                            item.hol_habilitado = bool.Parse(dr["HOL_HABILITADO"].ToString());
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

        public List<Holding> GetHoldings()
        {
            List<Holding> listado = new List<Holding>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_HOLDING";

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Holding item = new Holding();

                            item.hol_id = int.Parse(dr["HOL_ID"].ToString());
                            item.hol_nombre = dr["HOL_NOMBRE"].ToString();
                            item.hol_habilitado = bool.Parse(dr["HOL_HABILITADO"].ToString());

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

        public Respuesta InsertHolding(Holding item)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = null;

                try
                {
                    cmdExecute = Conexion.GetCommand("INS_HOLDING");

                    cmdExecute.Parameters.AddWithValue("@NOMBRE", item.hol_nombre);
                    cmdExecute.Parameters.AddWithValue("@HABILITADO", item.hol_habilitado);
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

        public Respuesta UpdateHolding(Holding item)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                try
                {
                    cmdExecute = Conexion.GetCommand("UPD_HOLDING");

                    cmdExecute.Parameters.AddWithValue("@ID", item.hol_id);
                    cmdExecute.Parameters.AddWithValue("@NOMBRE", item.hol_nombre);
                    cmdExecute.Parameters.AddWithValue("@HABILITADO", item.hol_habilitado);
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

        public Respuesta DeleteHolding(Holding item)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                try
                {
                    cmdExecute = Conexion.GetCommand("DEL_HOLDING");

                    cmdExecute.Parameters.AddWithValue("@ID", item.hol_id);

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

        public List<UsuarioHolding> GetUsuarioHolding(UsuarioHolding item)
        {
            List<UsuarioHolding> listado = new List<UsuarioHolding>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_USUARIO_HOLDING";
                    cmd.Parameters.AddWithValue("@USUARIO", item.uho_usuario);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            item = new UsuarioHolding();

                            item.uho_id = int.Parse(dr["UHO_ID"].ToString());
                            item.uho_usuario = int.Parse(dr["UHO_USUARIO"].ToString());
                            item.uho_holding = int.Parse(dr["UHO_HOLDING"].ToString());
                            item.holding_nombre = dr["HOL_NOMBRE"].ToString();

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

        public List<UsuarioHolding> GetHoldings(UsuarioHolding item)
        {
            List<UsuarioHolding> listado = new List<UsuarioHolding>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_USUARIO_HOLDING_ASOCIA";
                    cmd.Parameters.AddWithValue("@USUARIO", item.uho_usuario);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            item = new UsuarioHolding();

                            item.uho_holding = int.Parse(dr["HOL_ID"].ToString());
                            item.holding_nombre = dr["HOL_NOMBRE"].ToString();

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

        public Respuesta InsertUsuarioHolding(List<UsuarioHolding> listado)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = new SqlCommand();

                try
                {
                    foreach (UsuarioHolding item in listado)
                    {
                        cmdExecute = Conexion.GetCommand("INS_USUARIO_HOLDING");
                        cmdExecute.Parameters.AddWithValue("@ID_USUARIO", item.uho_usuario);
                        cmdExecute.Parameters.AddWithValue("@ID_HOLDING", item.uho_holding);
                        cmdExecute.Parameters.AddWithValue("@USUARIO_CREACION", Session.UsuarioId());

                        cmdExecute.ExecuteNonQuery();
                        cmdExecute.Connection.Close();
                    }

                    respuesta.detalle = "Registro(s) asociado(s) con éxito";
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

        public Respuesta DeleteUsuarioHolding(List<UsuarioHolding> listado)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = new SqlCommand();

                try
                {
                    foreach (UsuarioHolding item in listado)
                    {
                        cmdExecute = Conexion.GetCommand("DEL_USUARIO_HOLDING");
                        cmdExecute.Parameters.AddWithValue("@ID", item.uho_id);
                        cmdExecute.ExecuteNonQuery();
                        cmdExecute.Connection.Close();
                    }

                    respuesta.detalle = "Registro(s) eliminado(s) con éxito";
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