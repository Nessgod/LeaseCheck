using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LeaseCheck.Model;

namespace LeaseCheck.Controller
{
    public class ClienteInstalacionusuarioController
    {
        private SqlCommand cmdExecute = null;


        //Listo datos de cliente instalacion usuario
        public List<ClienteInstalacionUsuario> GetClienteInstalacionUsuarios(ClienteInstalacionUsuario clienteInstalacionUsuario)
        {
            List<ClienteInstalacionUsuario> clienteInstalacionUsuarios = new List<ClienteInstalacionUsuario>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_CLIENTE_INSTALACION_USUARIO";

                    cmd.Parameters.AddWithValue("@INSTALACION", clienteInstalacionUsuario.ciu_instalacion);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            clienteInstalacionUsuario = new ClienteInstalacionUsuario();

                            clienteInstalacionUsuario.ciu_id = int.Parse(dr["CIU_ID"].ToString());
                            clienteInstalacionUsuario.ciu_instalacion = int.Parse(dr["CIU_INSTALACION"].ToString());
                            clienteInstalacionUsuario.ciu_usuario = int.Parse(dr["CIU_USUARIO"].ToString());
                            clienteInstalacionUsuario.NOMBRE_COMPLETO = dr["NOMBRE_COMPLETO"].ToString();
                            clienteInstalacionUsuario.usu_correo = dr["USU_CORREO"].ToString();
                            clienteInstalacionUsuario.ciu_responsable = bool.Parse(dr["CIU_RESPONSABLE"].ToString());
                            clienteInstalacionUsuario.ciu_habilitado = bool.Parse(dr["CIU_HABILITADO"].ToString());
                            clienteInstalacionUsuario.ciu_usuario_creacion = int.Parse(dr["CIU_USUARIO_CREACION"].ToString());
                            clienteInstalacionUsuario.ciu_fecha_creacion = DateTime.Parse(dr["CIU_FECHA_CREACION"].ToString());
                            clienteInstalacionUsuario.ciu_usuario_act = int.Parse(dr["CIU_USUARIO_ACT"].ToString());
                            clienteInstalacionUsuario.ciu_fecha_act = DateTime.Parse(dr["CIU_FECHA_ACT"].ToString());

                            clienteInstalacionUsuarios.Add(clienteInstalacionUsuario);
                        }
                    }
                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    clienteInstalacionUsuario = null;
                }
            }
            return clienteInstalacionUsuarios;
        }

        //Listo datos de un cliente instalacion usuario
        public ClienteInstalacionUsuario GetClienteInstalacionUsuario(ClienteInstalacionUsuario clienteInstalacionUsuario)
        {
            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_CLIENTE_INSTALACION_USUARIO";
                    cmd.Parameters.AddWithValue("@ID", clienteInstalacionUsuario.ciu_id);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        if (dr.Read())
                        {
                            clienteInstalacionUsuario = new ClienteInstalacionUsuario();


                            clienteInstalacionUsuario.ciu_id = int.Parse(dr["CIU_ID"].ToString());
                            clienteInstalacionUsuario.ciu_instalacion = int.Parse(dr["CIU_INSTALACION"].ToString());
                            clienteInstalacionUsuario.ciu_usuario = int.Parse(dr["CIU_USUARIO"].ToString());
                            clienteInstalacionUsuario.NOMBRE_COMPLETO = dr["NOMBRE_COMPLETO"].ToString();
                            clienteInstalacionUsuario.usu_correo = dr["USU_CORREO"].ToString();
                            clienteInstalacionUsuario.ciu_responsable = bool.Parse(dr["CIU_RESPONSABLE"].ToString());
                            clienteInstalacionUsuario.ciu_habilitado = bool.Parse(dr["CIU_HABILITADO"].ToString());
                            clienteInstalacionUsuario.ciu_usuario_creacion = int.Parse(dr["CIU_USUARIO_CREACION"].ToString());
                            clienteInstalacionUsuario.ciu_fecha_creacion = DateTime.Parse(dr["CIU_FECHA_CREACION"].ToString());
                            clienteInstalacionUsuario.ciu_usuario_act = int.Parse(dr["CIU_USUARIO_ACT"].ToString());
                            clienteInstalacionUsuario.ciu_fecha_act = DateTime.Parse(dr["CIU_FECHA_ACT"].ToString());

                        }
                    }
                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    clienteInstalacionUsuario = null;
                }
            }
            return clienteInstalacionUsuario;
        }

        //Inserto registro
        public Respuesta InsertClienteInstalacionUsuario(ClienteInstalacionUsuario clienteInstalacionUsuario)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = null;

                try
                {
                    int id = 0;

                    cmdExecute = Conexion.GetCommand("INS_CLIENTE_INSTALACION_USUARIO");
                    cmdExecute.Parameters.AddWithValue("@ID", id).Direction = System.Data.ParameterDirection.Output;
                    cmdExecute.Parameters.AddWithValue("@INSTALACION", clienteInstalacionUsuario.ciu_instalacion);
                    cmdExecute.Parameters.AddWithValue("@USUARIO_ASOCIADO", clienteInstalacionUsuario.ciu_usuario_act);
                    cmdExecute.Parameters.AddWithValue("@RESPONSABLE", clienteInstalacionUsuario.ciu_responsable);
                    cmdExecute.Parameters.AddWithValue("@HABILITADO", clienteInstalacionUsuario.ciu_habilitado);
                    cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    id = (int)cmdExecute.Parameters["@ID"].Value;

                    respuesta.codigo = id;
                    respuesta.detalle = "Usuario asociado con éxito.";
                    respuesta.error = false;
                }
                catch (Exception ex)
                {
                    cmdExecute.Connection.Close();
                    respuesta.codigo = -1;
                    respuesta.detalle = ex.Message;
                    respuesta.error = true;
                }
            }
            return respuesta;
        }

        //Actualizo registro
        public Respuesta UpdateClienteInstalacionUsuario(ClienteInstalacionUsuario clienteInstalacionUsuario)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                try
                {
                    SqlCommand cmdExecute = Conexion.GetCommand("UPD_CLIENTE_INSTALACION_USUARIO");
                    cmdExecute.Parameters.AddWithValue("@ID", clienteInstalacionUsuario.ciu_id);
                    cmdExecute.Parameters.AddWithValue("@INSTALACION", clienteInstalacionUsuario.ciu_instalacion);
                    cmdExecute.Parameters.AddWithValue("@USUARIO_ASOCIADO", clienteInstalacionUsuario.ciu_usuario_act);
                    cmdExecute.Parameters.AddWithValue("@RESPONSABLE", clienteInstalacionUsuario.ciu_responsable);
                    cmdExecute.Parameters.AddWithValue("@HABILITADO", clienteInstalacionUsuario.ciu_habilitado);
                    cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    respuesta.codigo = 0;
                    respuesta.detalle = "Usuario asociado actualizado con éxito.";
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

        //Elimino registro(s)
        public Respuesta DeleteClienteInstalacionUsuario(ClienteInstalacionUsuario clienteInstalacionUsuario)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                try
                {
                    SqlCommand cmdExecute = Conexion.GetCommand("DEL_CLIENTE_INSTALACION_USUARIO");
                    cmdExecute.Parameters.AddWithValue("@ID", clienteInstalacionUsuario.ciu_id);

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    respuesta.codigo = 0;
                    respuesta.detalle = "Usuario asociado eliminado con éxito.";
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


        public List<ClienteInstalacionUsuario> GetClienteInstalacionUsuariosAsociar(ClienteInstalacionUsuario clienteInstalacionUsuario)
        {
            List<ClienteInstalacionUsuario> clienteInstalacionUsuarios = new List<ClienteInstalacionUsuario>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_CLIENTE_USUARIO_ASOCIAR";

                    if (clienteInstalacionUsuario.cli_id > 0) cmd.Parameters.AddWithValue("@CLIENTE", clienteInstalacionUsuario.cli_id);
                    if (clienteInstalacionUsuario.ciu_instalacion > 0) cmd.Parameters.AddWithValue("@INSTALACION", clienteInstalacionUsuario.ciu_instalacion);



                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            clienteInstalacionUsuario = new ClienteInstalacionUsuario();

                            clienteInstalacionUsuario.usu_id = int.Parse(dr["USU_ID"].ToString());
                            clienteInstalacionUsuario.NOMBRE_COMPLETO = dr["NOMBRE_COMPLETO"].ToString();
                            clienteInstalacionUsuario.usu_correo = dr["USU_CORREO"].ToString();


                            clienteInstalacionUsuarios.Add(clienteInstalacionUsuario);
                        }
                    }
                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    clienteInstalacionUsuario = null;
                }
            }
            return clienteInstalacionUsuarios;
        }
    }



}