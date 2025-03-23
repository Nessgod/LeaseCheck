using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LeaseCheck.Root.Model;

namespace LeaseCheck.Root.Controller
{
    public class EstadoCivilesController
    {
        public List<EstadoCivil> GetEstadoCiviles(EstadoCivil estadoCivil)
        {
            List<EstadoCivil> estadoCiviles = new List<EstadoCivil>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_ESTADO_CIVIL";
                    if (estadoCivil.eci_id > 0) cmd.Parameters.AddWithValue("@ID", estadoCivil.eci_id);
                    if (estadoCivil.eci_habilitado != null) cmd.Parameters.AddWithValue("@HABILITADO", estadoCivil.eci_habilitado);
                    if (estadoCivil.eci_nombre != null) cmd.Parameters.AddWithValue("@FILTRO", estadoCivil.eci_nombre);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            EstadoCivil item = new EstadoCivil();

                            item.eci_id = int.Parse(dr["ECI_ID"].ToString());
                            item.eci_nombre = dr["ECI_NOMBRE"].ToString();
                            item.eci_habilitado = bool.Parse(dr["ECI_HABILITADO"].ToString());

                            estadoCiviles.Add(item);
                        }
                    }

                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    estadoCiviles = null;
                }
            }

            return estadoCiviles;
        }

        public EstadoCivil GetEstadoCivil(EstadoCivil estadoCivil)
        {
            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_ESTADO_CIVIL";
                    if (estadoCivil.eci_id > 0) cmd.Parameters.AddWithValue("@ID", estadoCivil.eci_id);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        if (dr.Read())
                        {
                            estadoCivil = new EstadoCivil();

                            estadoCivil.eci_id = int.Parse(dr["ECI_ID"].ToString());
                            estadoCivil.eci_nombre = dr["ECI_NOMBRE"].ToString();
                            estadoCivil.eci_habilitado = bool.Parse(dr["ECI_HABILITADO"].ToString());
                        }
                    }

                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    estadoCivil = null;
                }
            }

            return estadoCivil;
        }

        public Respuesta InsertEstadoCivil(EstadoCivil estadoCivil)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = null;

                try
                {
                    int id = 0;
                    cmdExecute = Conexion.GetCommand("INS_ESTADO_CIVIL");
                    cmdExecute.Parameters.AddWithValue("@ID", id).Direction = System.Data.ParameterDirection.Output;
                    cmdExecute.Parameters.AddWithValue("@NOMBRE", estadoCivil.eci_nombre);
                    cmdExecute.Parameters.AddWithValue("@HABILITADO", estadoCivil.eci_habilitado);
                    cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    //Recupero el id del archivo insertado
                    id = (int)cmdExecute.Parameters["@ID"].Value;

                    respuesta.codigo = id;
                    respuesta.error = false;
                    respuesta.detalle = "Estado Civil creado con éxito.";
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

        public Respuesta UpdateEstadoCivil(EstadoCivil estadoCivil)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = null;

                try
                {

                    cmdExecute = Conexion.GetCommand("UPD_ESTADO_CIVIL");
                    cmdExecute.Parameters.AddWithValue("@ID", estadoCivil.eci_id);
                    cmdExecute.Parameters.AddWithValue("@NOMBRE", estadoCivil.eci_nombre);
                    cmdExecute.Parameters.AddWithValue("@HABILITADO", estadoCivil.eci_habilitado);
                    cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    respuesta.codigo = 0;
                    respuesta.detalle = "Estado Civil actualizado con éxito.";
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

        public Respuesta DeleteEstadoCivil(EstadoCivil estadoCivil)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = null;
                try
                {

                    cmdExecute = Conexion.GetCommand("DEL_ESTADO_CIVIL");
                    cmdExecute.Parameters.AddWithValue("@ID", estadoCivil.eci_id);

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    respuesta.codigo = 0;
                    respuesta.detalle = "Estado Civil eliminado con éxito.";
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

        public void InformeEstadoCivil(EstadoCivil estadoCivil)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "RPT_ESTADO_CIVIL";
            if (estadoCivil.eci_id > 0) cmd.Parameters.AddWithValue("@eci_id", estadoCivil.eci_id);
            
            string filename = " INFORME ESTADO CIVIL " + DateTime.Now;
            Tools.Excel.exportExcel(Conexion.GetDataTable(cmd), filename, true);
        }
    }
}