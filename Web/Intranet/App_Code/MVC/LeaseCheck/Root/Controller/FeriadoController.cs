using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LeaseCheck.Root.Controller
{
    public class FeriadoController
    {
        public List<Feriado> GetFeriados(Feriado feriado)
        {
            List<Feriado> feriados = new List<Feriado>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                { 
                    cmd.CommandText = "SEL_FERIADOS";
                    if(feriado.frd_id > 0) cmd.Parameters.AddWithValue("@ID", feriado.frd_id);
                    if (feriado.frd_pais > 0) cmd.Parameters.AddWithValue("@PAIS", feriado.frd_pais);
                    if (feriado.desde != null) cmd.Parameters.AddWithValue("@DESDE", feriado.desde);
                    if (feriado.hasta != null) cmd.Parameters.AddWithValue("@HASTA", feriado.hasta);
                    if (feriado.frd_descripcion != "") cmd.Parameters.AddWithValue("@FILTRO", feriado.frd_descripcion);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            feriado = new Feriado();

                            feriado.frd_id = int.Parse(dr["FRD_ID"].ToString());
                            feriado.frd_descripcion = dr["FRD_DESCRIPCION"].ToString();
                            feriado.frd_fecha_feriados = DateTime.Parse(dr["FRD_FECHA_FERIADOS"].ToString());
                            feriado.pai_nombre = dr["PAI_NOMBRE"].ToString();

                            feriados.Add(feriado);
                        }
                    }

                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    feriados = null;
                }
            }

            return feriados;
        }

        public Feriado GetFeriado(Feriado filtro)
        {
            Feriado item = new Feriado();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {  
                    cmd.CommandText = "SEL_FERIADOS";
                    cmd.Parameters.AddWithValue("@ID", filtro.frd_id);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        if (dr.Read())
                        {
                            item.frd_id = int.Parse(dr["FRD_ID"].ToString());
                            item.frd_pais = int.Parse(dr["FRD_PAIS"].ToString());
                            item.frd_descripcion = dr["FRD_DESCRIPCION"].ToString();
                            item.frd_fecha_feriados = DateTime.Parse(dr["FRD_FECHA_FERIADOS"].ToString());
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

        public Respuesta InsertFeriado(Feriado item)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = null;

                try
                {
                    int id = 0;

                    cmdExecute = Conexion.GetCommand("INS_FERIADOS");
                    cmdExecute.Parameters.AddWithValue("@ID", id).Direction = System.Data.ParameterDirection.Output;
                    cmdExecute.Parameters.AddWithValue("@ID_PAIS", item.frd_pais);
                    cmdExecute.Parameters.AddWithValue("@DESCRIPCION", item.frd_descripcion);
                    cmdExecute.Parameters.AddWithValue("@FECHA_FERIADOS", item.frd_fecha_feriados);
                    cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    //Recupero el id del archivo insertado
                    id = (int)cmdExecute.Parameters["@ID"].Value;

                    respuesta.codigo = id;
                    respuesta.error = false;
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

        public Respuesta UpdateFeriado(Feriado item)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                try
                {
                    cmdExecute = Conexion.GetCommand("UPD_FERIADOS");
                    cmdExecute.Parameters.AddWithValue("@ID", item.frd_id);
                    cmdExecute.Parameters.AddWithValue("@ID_PAIS", item.frd_pais);
                    cmdExecute.Parameters.AddWithValue("@DESCRIPCION", item.frd_descripcion);
                    cmdExecute.Parameters.AddWithValue("@FECHA_FERIADOS", item.frd_fecha_feriados);
                    cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());

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

        public Respuesta DeleteFeriado(Feriado item)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                try
                {
                    cmdExecute = Conexion.GetCommand("DEL_FERIADOS");
                    cmdExecute.Parameters.AddWithValue("@ID", item.frd_id);

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    respuesta.codigo = 0;
                    respuesta.detalle = "Registro de feriado eliminado con éxito.";
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

        public void InformeFeriados(Feriado feriado)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "RPT_FERIADOS";
            if (feriado.frd_id > 0) cmd.Parameters.AddWithValue("@ID", feriado.frd_id);
            if (feriado.frd_pais > 0) cmd.Parameters.AddWithValue("@PAIS", feriado.frd_pais);
            if (feriado.desde != null) cmd.Parameters.AddWithValue("@DESDE", feriado.desde);
            if (feriado.hasta != null) cmd.Parameters.AddWithValue("@HASTA", feriado.hasta);
            if (feriado.frd_descripcion != "") cmd.Parameters.AddWithValue("@FILTRO", feriado.frd_descripcion);
            if (feriado.frd_id >0) cmd.Parameters.AddWithValue("@FILTRO", feriado.frd_id);


            string filename = " INFORME FERIADO " + DateTime.Now;
            Tools.Excel.exportExcel(Conexion.GetDataTable(cmd), filename, true);
        }
    }
}