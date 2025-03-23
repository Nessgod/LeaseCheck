using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using LeaseCheck.Root.Model;

namespace LeaseCheck.Root.Controller
{
    public class NacionalidadesController
    {
        //Listo todas las nacionalidades (grilla)
        public List<Nacionalidades> GetNacionalidades()
        {
            List<Nacionalidades> nacionalidades = new List<Nacionalidades>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_NACIONALIDADES";

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Nacionalidades nacionalidad = new Nacionalidades();

                            nacionalidad.nac_id = int.Parse(dr["NAC_ID"].ToString());
                            nacionalidad.nac_nombre = dr["NAC_NOMBRE"].ToString();
                            nacionalidad.nac_fecha_creacion = DateTime.Parse(dr["NAC_FECHA_CREACION"].ToString());
                            nacionalidad.nac_habilitado = bool.Parse(dr["NAC_HABILITADO"].ToString());

                            nacionalidades.Add(nacionalidad);
                        }
                    }
                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    nacionalidades = null;
                }
            }
            return nacionalidades;
        }

        //Listo datos de una nacionalidad
        public Nacionalidades GetNacionalidad(Nacionalidades nacionalidad)
        {
            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_NACIONALIDADES";
                    cmd.Parameters.AddWithValue("@ID", nacionalidad.nac_id);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        if (dr.Read())
                        {
                            nacionalidad = new Nacionalidades();

                            nacionalidad.nac_id = int.Parse(dr["NAC_ID"].ToString());
                            nacionalidad.nac_nombre = dr["NAC_NOMBRE"].ToString();                            
                            nacionalidad.nac_habilitado = bool.Parse(dr["NAC_HABILITADO"].ToString());
                        }
                    }
                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    nacionalidad = null;
                }
            }
            return nacionalidad;
        }

        //Inserto registro
        public Respuesta InsertNacionalidad(Nacionalidades nacionalidad)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = null;

                try
                {
                    int id = 0;

                    cmdExecute = Conexion.GetCommand("INS_NACIONALIDADES");
                    cmdExecute.Parameters.AddWithValue("@ID", id).Direction = System.Data.ParameterDirection.Output;
                    cmdExecute.Parameters.AddWithValue("@NOMBRE", nacionalidad.nac_nombre);
                    cmdExecute.Parameters.AddWithValue("@HABILITADO", nacionalidad.nac_habilitado);
                    cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    id = (int)cmdExecute.Parameters["@ID"].Value;

                    respuesta.codigo = id;
                    respuesta.detalle = "Nacionalidad creada con éxito.";
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

        //Actualizo registro
        public Respuesta UpdateNacionalidad(Nacionalidades nacionalidad)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                try
                {
                    cmdExecute = Conexion.GetCommand("UPD_NACIONALIDADES");
                    cmdExecute.Parameters.AddWithValue("@ID", nacionalidad.nac_id);
                    cmdExecute.Parameters.AddWithValue("@NOMBRE", nacionalidad.nac_nombre);
                    cmdExecute.Parameters.AddWithValue("@HABILITADO", nacionalidad.nac_habilitado);
                    cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    respuesta.codigo = 0;
                    respuesta.detalle = "Nacionalida actualizada con éxito.";
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
        public Respuesta DeleteNacionalidad(Nacionalidades nacionalidad)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                try
                {
                    cmdExecute = Conexion.GetCommand("DEL_NACIONALIDAD");
                    cmdExecute.Parameters.AddWithValue("@ID", nacionalidad.nac_id);

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    respuesta.codigo = 0;
                    respuesta.detalle = "Nacionalidad eliminada con éxito.";
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

        public void InformeNacionalidades(Nacionalidades nacionalidades)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "RPT_NACIONALIDADES";
            if (nacionalidades.nac_id > 0) cmd.Parameters.AddWithValue("@NAC_ID", nacionalidades.nac_id);
            //if (paises.pai_nombre != string.Empty) cmd.Parameters.AddWithValue("@PAI_NOMBRE", paises.pai_nombre);
            //if (paises.pai_fecha_creacion != DateTime.Parse("01-01-0001 0:00:00")) cmd.Parameters.AddWithValue("@PAI_FECHA_CREACION", paises.pai_fecha_creacion.ToShortDateString());
            //if (paises.pai_habilitado ) cmd.Parameters.AddWithValue("@PAI_HABILITADO", paises.pai_habilitado);


            string filename = " INFORME NACIONALIDADES " + DateTime.Now;
            Tools.Excel.exportExcel(Conexion.GetDataTable(cmd), filename, true);
        }
    }
}