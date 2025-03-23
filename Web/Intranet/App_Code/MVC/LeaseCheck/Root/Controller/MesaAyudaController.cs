using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using AccesoDatosOfertasLaborales.Model;
using System.Data.SqlClient;
using LeaseCheck.Root.Model;

namespace LeaseCheck.Root.Controller
{
    public class MesaAyudaController
    {
        private SqlCommand cmdExecute = null;
        private string ConectionString = "ConectionStringOfertaLaboral";

        //Inserta mesa ayuda
        public Respuesta InsertMesaAyuda(MesaAyuda mesaAyuda)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            try
            {
                cmdExecute = Conexion.GetCommand("INS_MESA_AYUDA");
                cmdExecute.Parameters.AddWithValue("@NOMBRE", mesaAyuda.mes_nombre);
                cmdExecute.Parameters.AddWithValue("@TELEFONO", mesaAyuda.mes_telefono);
                cmdExecute.Parameters.AddWithValue("@CORREO", mesaAyuda.mes_correo);
                cmdExecute.Parameters.AddWithValue("@MENSAJE", mesaAyuda.mes_mensaje);
                cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());

                cmdExecute.ExecuteNonQuery();
                cmdExecute.Connection.Close();

                respuesta.codigo = -1;
                respuesta.detalle = "Consulta recibida con exito.";
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

        //Inserta mesa ayuda
        public Respuesta UpdateMesaAyuda(MesaAyuda mesaAyuda)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            try
            {
                cmdExecute = Conexion.GetCommand("UPD_MESA_AYUDA_CIERRE");
                cmdExecute.Parameters.AddWithValue("@ID", mesaAyuda.mes_id);
                cmdExecute.Parameters.AddWithValue("@RESPUESTA", mesaAyuda.mes_observacion_cierre);
                cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());

                cmdExecute.ExecuteNonQuery();
                cmdExecute.Connection.Close();

                respuesta.codigo = -1;
                respuesta.detalle = "Consulta cerrada con exito.";
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
        public List<MesaAyuda> GetMesaAyuda(MesaAyuda mesaAyuda = null)
        {
            List<MesaAyuda> mesaAyudas = new List<MesaAyuda>();

            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = "SEL_MESA_AYUDA";
                if (mesaAyuda.mes_id > 0) cmd.Parameters.AddWithValue("@ID", mesaAyuda.mes_id);
                if (mesaAyuda.filtro != null) cmd.Parameters.AddWithValue("@FILTRO", mesaAyuda.filtro);
                if (mesaAyuda.mes_estado > 0) cmd.Parameters.AddWithValue("@ESTADO", mesaAyuda.mes_estado);
                using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                {
                    while (dr.Read())
                    {
                        mesaAyuda = new MesaAyuda();

                        mesaAyuda.mes_id = int.Parse(dr["MES_ID"].ToString());
                        mesaAyuda.mes_nombre = dr["MES_NOMBRE"].ToString();
                        mesaAyuda.mes_telefono = int.Parse(dr["MES_TELEFONO"].ToString());
                        mesaAyuda.mes_correo = dr["MES_CORREO"].ToString();
                        mesaAyuda.mes_mensaje = dr["MES_MENSAJE"].ToString();
                        mesaAyuda.mes_estado = int.Parse(dr["MES_ESTADO"].ToString());
                        mesaAyuda.mes_estado_nombre = dr["MES_ESTADO_NOMBRE"].ToString();
                        mesaAyuda.mes_fecha_creacion = DateTime.Parse(dr["MES_FECHA_CREACION"].ToString());
                        if (dr["MES_FECHA_CIERRE"].ToString() != "") mesaAyuda.fecha_respuesta = DateTime.Parse(dr["MES_FECHA_CIERRE"].ToString());

                        mesaAyudas.Add(mesaAyuda);
                    }
                }

                cmd.Connection.Close();
                cmd.Dispose();

                return mesaAyudas;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                cmd.Dispose();
                return null;
            }
        }


        public MesaAyuda GetMesaAyudaDetalle(MesaAyuda mesaAyuda)
        {

            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = "SEL_MESA_AYUDA";
                cmd.Parameters.AddWithValue("@ID", mesaAyuda.mes_id);
                using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                {
                    while (dr.Read())
                    {
                        mesaAyuda = new MesaAyuda();

                        mesaAyuda.mes_id = int.Parse(dr["MES_ID"].ToString());
                        mesaAyuda.mes_nombre = dr["MES_NOMBRE"].ToString();
                        mesaAyuda.mes_telefono = int.Parse(dr["MES_TELEFONO"].ToString());
                        mesaAyuda.mes_correo = dr["MES_CORREO"].ToString();
                        mesaAyuda.mes_mensaje = dr["MES_MENSAJE"].ToString();
                        mesaAyuda.mes_estado = int.Parse(dr["MES_ESTADO"].ToString());
                        mesaAyuda.mes_estado_nombre = dr["MES_ESTADO_NOMBRE"].ToString();
                        mesaAyuda.mes_fecha_creacion = DateTime.Parse(dr["MES_FECHA_CREACION"].ToString());
                        if(dr["MES_FECHA_CIERRE"].ToString() != "") mesaAyuda.mes_fecha_cierre = DateTime.Parse(dr["MES_FECHA_CIERRE"].ToString());
                        mesaAyuda.mes_observacion_cierre = dr["MES_OBSERVACION_CIERRE"].ToString();
                    }
                }

                cmd.Connection.Close();
                cmd.Dispose();

                return mesaAyuda;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                cmd.Dispose();
                mesaAyuda = null;

                return mesaAyuda;
            }
        }

        public void ReporteMesaAyuda(MesaAyuda mesaAyuda)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "RPT_MESA_AYUDA";
            if (mesaAyuda.mes_id > 0) cmd.Parameters.AddWithValue("@MES_ID", mesaAyuda.mes_id);
            if (mesaAyuda.filtro != null) cmd.Parameters.AddWithValue("@FILTRO", mesaAyuda.filtro);
            if (mesaAyuda.mes_estado > 0) cmd.Parameters.AddWithValue("@ESTADO", mesaAyuda.mes_estado);
            string filename = "INFORME MESA DE AYUDA" + DateTime.Now;
            Tools.Excel.exportExcel(Conexion.GetDataTable(cmd), filename, true);
        }

    }
}