using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LeaseCheck.Root.Model;

namespace LeaseCheck.Root.Controller
{
    public class JornadasLaboralesController
    {
        public List<JornadaLaboral> GetJornadasLaborales()
        {
            List<JornadaLaboral> listado = new List<JornadaLaboral>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_JORNADA_LABORAL";

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            JornadaLaboral item = new JornadaLaboral();

                            item.jol_id = int.Parse(dr["JOL_ID"].ToString());
                            item.jol_nombre = dr["JOL_NOMBRE"].ToString();
                            item.jol_habilitado = bool.Parse(dr["JOL_HABILITADO"].ToString());
                            
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

        public JornadaLaboral GetJornadaLaboral(JornadaLaboral filtro)
        {
            JornadaLaboral item = new JornadaLaboral();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_JORNADA_LABORAL";
                     cmd.Parameters.AddWithValue("@ID", filtro.jol_id);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            item.jol_id = int.Parse(dr["JOL_ID"].ToString());
                            item.jol_nombre = dr["JOL_NOMBRE"].ToString();
                            item.jol_habilitado = bool.Parse(dr["JOL_HABILITADO"].ToString());
                        }
                    }

                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    item = null;
                }
            }

            return item;
        }

        public Respuesta InsertJornadaLaboral(JornadaLaboral item)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = null;

                try
                {
                    cmdExecute = Conexion.GetCommand("INS_JORNADA_LABORAL");

                    cmdExecute.Parameters.AddWithValue("@NOMBRE", item.jol_nombre);
                    cmdExecute.Parameters.AddWithValue("@HABILITADO", item.jol_habilitado);
                    cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();
                    
                    respuesta.detalle = "Registro creado con éxito.";
                     
                }
                catch (Exception ex)
                {
                    cmdExecute.Connection.Close();
                    cmdExecute.Dispose();

                    respuesta.detalle = ex.Message;
                    respuesta.error = true;
                }
            }

            return respuesta;
        }

        public Respuesta UpdateJornadaLaboral(JornadaLaboral item)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = null;

                try
                {
                    cmdExecute = Conexion.GetCommand("UPD_JORNADA_LABORAL");

                    cmdExecute.Parameters.AddWithValue("@ID", item.jol_id);
                    cmdExecute.Parameters.AddWithValue("@NOMBRE", item.jol_nombre);
                    cmdExecute.Parameters.AddWithValue("@HABILITADO", item.jol_habilitado);
                    cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();
                    
                    respuesta.detalle = "Registro modificado con éxito";
                }
                catch (Exception ex)
                {
                    cmdExecute.Connection.Close();
                    cmdExecute.Dispose();

                    respuesta.detalle = ex.Message;
                    respuesta.error = true;
                }
            }

            return respuesta;
        }

        public Respuesta DeleteJornadaLaboral(JornadaLaboral item)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = null;

                try
                {
                    cmdExecute = Conexion.GetCommand("DEL_JORNADA_LABORAL");
                    cmdExecute.Parameters.AddWithValue("@ID", item.jol_id);

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    respuesta.detalle = "Registro eliminado con éxito.";
                }
                catch (Exception ex)
                {
                    cmdExecute.Connection.Close();
                    cmdExecute.Dispose();

                    respuesta.detalle = ex.Message;
                    respuesta.error = true;
                }
            }

             return respuesta;
        }
        public void ReporteInformeJornadaLaboral(JornadaLaboral jornadalaboral)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "RPT_JORNADA_LABORAL";
            if (jornadalaboral.jol_id > 0) cmd.Parameters.AddWithValue("@JOL_ID", jornadalaboral.jol_id);

            string filename = " INFORME JORNADA LABORAL " + DateTime.Now;
            Tools.Excel.exportExcel(Conexion.GetDataTable(cmd), filename, true);
        }
    }
}