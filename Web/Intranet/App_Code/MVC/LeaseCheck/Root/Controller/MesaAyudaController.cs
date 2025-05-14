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
    
        //Inserta mesa ayuda
        public Respuesta InsertMesaAyuda(MesaAyuda mesaAyuda)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            try
            {
                int id = 0;

                cmdExecute = Conexion.GetCommand("INS_MESA_AYUDA");
                cmdExecute.Parameters.AddWithValue("@ID", id).Direction = System.Data.ParameterDirection.Output;
                cmdExecute.Parameters.AddWithValue("@NOMBRE", mesaAyuda.mes_nombre);
                cmdExecute.Parameters.AddWithValue("@MENSAJE", mesaAyuda.mes_mensaje);
                cmdExecute.Parameters.AddWithValue("@MODULO", mesaAyuda.mes_id_modulo);
                cmdExecute.Parameters.AddWithValue("@OTRO_MODULO", mesaAyuda.mes_otro_modulo);
                cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());
                cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());

                cmdExecute.ExecuteNonQuery();
                cmdExecute.Connection.Close();

                id = (int)cmdExecute.Parameters["@ID"].Value;


                respuesta.codigo = id;
                respuesta.detalle = "Consulta enviada con exito.";
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
                cmdExecute.Parameters.AddWithValue("@USUARIO_CIERRE", Session.UsuarioId());

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
                cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());

                using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                {
                    while (dr.Read())
                    {
                        mesaAyuda = new MesaAyuda();

                        mesaAyuda.mes_id = int.Parse(dr["MES_ID"].ToString());
                        mesaAyuda.mes_nombre = dr["MES_NOMBRE"].ToString();
                        mesaAyuda.mes_telefono = dr["MES_TELEFONO"].ToString();
                        mesaAyuda.mes_correo = dr["MES_CORREO"].ToString();
                        mesaAyuda.mes_mensaje = dr["MES_MENSAJE"].ToString();
                        mesaAyuda.mes_estado = int.Parse(dr["MES_ESTADO"].ToString());
                        mesaAyuda.mes_estado_nombre = dr["MES_ESTADO_NOMBRE"].ToString();
                        if (dr["MES_ID_MODULO"].ToString() != "")  mesaAyuda.mes_id_modulo = int.Parse(dr["MES_ID_MODULO"].ToString());
                        if (dr["MES_OTRO_MODULO"].ToString() != "") mesaAyuda.mes_otro_modulo = dr["MES_OTRO_MODULO"].ToString();
                        if (dr["NOMBRE_CLIENTE"].ToString() != "")  mesaAyuda.NOMBRE_CLIENTE = dr["NOMBRE_CLIENTE"].ToString();
                        if (dr["NOMBRE_MODULO"].ToString() != "") mesaAyuda.NOMBRE_MODULO = dr["NOMBRE_MODULO"].ToString();
                        if (dr["NOMBRE_CREADOR"].ToString() != "") mesaAyuda.NOMBRE_CREADOR = dr["NOMBRE_CREADOR"].ToString();
                        if (dr["RESPONSABLE_CIERRE"].ToString() != "") mesaAyuda.RESPONSABLE_CIERRE = dr["RESPONSABLE_CIERRE"].ToString();
                        if (dr["PERFIL"].ToString() != "") mesaAyuda.PERFIL = dr["PERFIL"].ToString();
                        mesaAyuda.mes_fecha_creacion = DateTime.Parse(dr["MES_FECHA_CREACION"].ToString());
                        if (dr["MES_FECHA_CIERRE"].ToString() != "") mesaAyuda.fecha_respuesta = DateTime.Parse(dr["MES_FECHA_CIERRE"].ToString());
                        if (dr["FECHA_RESPUESTA"].ToString() != "") mesaAyuda.FECHA_ULTIMA_RESPUESTA = DateTime.Parse(dr["FECHA_RESPUESTA"].ToString());

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
                        mesaAyuda.mes_telefono = dr["MES_TELEFONO"].ToString();
                        mesaAyuda.mes_correo = dr["MES_CORREO"].ToString();
                        mesaAyuda.mes_mensaje = dr["MES_MENSAJE"].ToString();
                        mesaAyuda.mes_estado = int.Parse(dr["MES_ESTADO"].ToString());
                        mesaAyuda.mes_estado_nombre = dr["MES_ESTADO_NOMBRE"].ToString();
                        mesaAyuda.mes_fecha_creacion = DateTime.Parse(dr["MES_FECHA_CREACION"].ToString());
                        if(dr["MES_FECHA_CIERRE"].ToString() != "") mesaAyuda.mes_fecha_cierre = DateTime.Parse(dr["MES_FECHA_CIERRE"].ToString());
                        mesaAyuda.mes_observacion_cierre = dr["MES_OBSERVACION_CIERRE"].ToString();
                        if (dr["MES_ID_MODULO"].ToString() != "") mesaAyuda.mes_id_modulo = int.Parse(dr["MES_ID_MODULO"].ToString());
                        if (dr["MES_OTRO_MODULO"].ToString() != "") mesaAyuda.mes_otro_modulo = dr["MES_OTRO_MODULO"].ToString();
                        if (dr["NOMBRE_CLIENTE"].ToString() != "") mesaAyuda.NOMBRE_CLIENTE = dr["NOMBRE_CLIENTE"].ToString();
                        if (dr["NOMBRE_MODULO"].ToString() != "") mesaAyuda.NOMBRE_MODULO = dr["NOMBRE_MODULO"].ToString();
                        if (dr["NOMBRE_CREADOR"].ToString() != "") mesaAyuda.NOMBRE_CREADOR = dr["NOMBRE_CREADOR"].ToString();
                        if (dr["RESPONSABLE_CIERRE"].ToString() != "") mesaAyuda.RESPONSABLE_CIERRE = dr["RESPONSABLE_CIERRE"].ToString();

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

        public Respuesta InsertMesaAyudaRespuestas(MesaAyudaRespuesta mesaAyuda)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            try
            {
                cmdExecute = Conexion.GetCommand("INS_MESA_AYUDA_RESPUESTA");
                cmdExecute.Parameters.AddWithValue("@RESPUESTA", mesaAyuda.mer_respuesta);
                cmdExecute.Parameters.AddWithValue("@TIPO", mesaAyuda.mer_tipo_respuesta);
                cmdExecute.Parameters.AddWithValue("@ID_CONSULTA", mesaAyuda.mer_id_mesa_ayuda);
                cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());
                cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());

                cmdExecute.ExecuteNonQuery();
                cmdExecute.Connection.Close();

                respuesta.codigo = -1;
                respuesta.detalle = "Respuesta enviada con exito.";
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

        public List<MesaAyudaRespuesta> GetMesaAyudaRespuestas(MesaAyudaRespuesta respuesta = null)
        {
            List<MesaAyudaRespuesta> respuestas = new List<MesaAyudaRespuesta>();

            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = "SEL_MESA_AYUDA_RESPUESTA";
                if (respuesta.mer_id_mesa_ayuda > 0) cmd.Parameters.AddWithValue("@ID_CONSULTA", respuesta.mer_id_mesa_ayuda);

                using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                {
                    while (dr.Read())
                    {
                        respuesta = new MesaAyudaRespuesta();

                        respuesta.mer_id = int.Parse(dr["MER_ID"].ToString());
                        respuesta.mer_id_mesa_ayuda = int.Parse(dr["MER_ID_MESA_AYUDA"].ToString());
                        respuesta.mer_tipo_respuesta = int.Parse(dr["MER_TIPO_RESPUESTA"].ToString());
                        respuesta.NOMBRE_EJECUTOR = dr["NOMBRE_EJECUTOR"].ToString();
                        respuesta.QUIEN_RESPONDIO = dr["QUIEN_RESPONDIO"].ToString();
                        respuesta.mer_respuesta = dr["MER_RESPUESTA"].ToString();
                        respuesta.FOTO_USUARIO = dr["FOTO_USUARIO"] == DBNull.Value ? null : (byte[])dr["FOTO_USUARIO"];
                        respuesta.NOMBRE_CLIENTE = dr["NOMBRE_CLIENTE"].ToString();
                        respuesta.mer_fecha_creacion = DateTime.Parse(dr["MER_FECHA_CREACION"].ToString());


                        respuestas.Add(respuesta);
                    }
                }

                cmd.Connection.Close();
                cmd.Dispose();

                return respuestas;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                cmd.Dispose();
                return null;
            }
        }

        public List<ModuloSistema> GetModuloSistema(ModuloSistema modulo = null)
        {
            List<ModuloSistema> modulos = new List<ModuloSistema>();

            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = "SEL_MODULO_SISTEMA";

                using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                {
                    while (dr.Read())
                    {
                        modulo = new ModuloSistema();

                        modulo.mod_id = int.Parse(dr["MOD_ID"].ToString());
                        modulo.mod_nombre = dr["MOD_NOMBRE"].ToString();

                        modulos.Add(modulo);
                    }
                }

                cmd.Connection.Close();
                cmd.Dispose();

                return modulos;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                cmd.Dispose();
                return null;
            }
        }



        public void ReporteMesaAyuda(MesaAyuda mesaAyuda)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "RPT_MESA_AYUDA";
            if (mesaAyuda.mes_id > 0) cmd.Parameters.AddWithValue("@MES_ID", mesaAyuda.mes_id);
            if (mesaAyuda.filtro != null) cmd.Parameters.AddWithValue("@FILTRO", mesaAyuda.filtro);
            if (mesaAyuda.mes_estado > 0) cmd.Parameters.AddWithValue("@ESTADO", mesaAyuda.mes_estado);
            if (Session.UsuarioId() != "") cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
            string filename = "INFORME MESA DE AYUDA" + DateTime.Now;
            Tools.Excel.exportExcel(Conexion.GetDataTable(cmd), filename, true);
        }

    }
}