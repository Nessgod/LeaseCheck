using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LeaseCheck.Root.Model;

namespace LeaseCheck.Root.Controller
{
    public class TiposdePlanesController
    {
        public List<TipoPlan> GetTiposPlanes(TipoPlan tipoPlan = null)
        {
            List<TipoPlan> listado = new List<TipoPlan>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_TIPO_PLAN";
                    if (tipoPlan.tpl_id > 0) cmd.Parameters.AddWithValue("@ID", tipoPlan.tpl_id);
                    if (tipoPlan.filtro != "") cmd.Parameters.AddWithValue("@FILTRO", tipoPlan.filtro);
                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            TipoPlan item = new TipoPlan();

                            item.tpl_id = int.Parse(dr["TPL_ID"].ToString());
                            item.tpl_nombre = dr["TPL_NOMBRE"].ToString();
                            item.tpl_cantidad_informes = int.Parse(dr["TPL_CANTIDAD_INFORMES"].ToString());
                            item.tpl_cantidad_administradores = int.Parse(dr["TPL_CANTIDAD_ADMINISTRADORES"].ToString());
                            item.tpl_valor_plan = int.Parse(dr["TPL_VALOR_PLAN"].ToString());
                            item.tpl_habilitado = bool.Parse(dr["TPL_HABILITADO"].ToString());
                            item.tpl_fecha_creacion = DateTime.Parse(dr["TPL_FECHA_CREACION"].ToString());

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

        public TipoPlan GetTipoplan(TipoPlan filtro)
        {
            TipoPlan item = new TipoPlan();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_TIPO_PLAN";
                    cmd.Parameters.AddWithValue("@ID", filtro.tpl_id);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            item.tpl_id = int.Parse(dr["TPL_ID"].ToString());
                            item.tpl_nombre = dr["TPL_NOMBRE"].ToString();
                            item.tpl_cantidad_informes = int.Parse(dr["TPL_CANTIDAD_INFORMES"].ToString());
                            item.tpl_cantidad_administradores = int.Parse(dr["TPL_CANTIDAD_ADMINISTRADORES"].ToString());
                            item.tpl_administradores_ilimitados = bool.Parse(dr["TPL_ADMINISTRADORES_ILIMITADOS"].ToString());
                            item.tpl_valor_plan = int.Parse(dr["TPL_VALOR_PLAN"].ToString());
                            item.tpl_habilitado = bool.Parse(dr["TPL_HABILITADO"].ToString());
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

        public Respuesta InsertTipoPlan(TipoPlan item)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = null;

                try
                {
                    int id = 0;
                    cmdExecute = Conexion.GetCommand("INS_TIPO_PLAN");
                    cmdExecute.Parameters.AddWithValue("@ID", id).Direction = System.Data.ParameterDirection.Output;
                    cmdExecute.Parameters.AddWithValue("@NOMBRE", item.tpl_nombre);
                    cmdExecute.Parameters.AddWithValue("@CANTIDAD_INFORMES", item.tpl_cantidad_informes);
                    cmdExecute.Parameters.AddWithValue("@CANTIDAD_ADMINISTRADORES", item.tpl_cantidad_administradores);
                    cmdExecute.Parameters.AddWithValue("@ADMINISTRADORES_ILIMITADOS", item.tpl_administradores_ilimitados);
                    cmdExecute.Parameters.AddWithValue("@VALOR_PLAN", item.tpl_valor_plan);
                    cmdExecute.Parameters.AddWithValue("@HABILITADO", item.tpl_habilitado);
                    cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    id = (int)cmdExecute.Parameters["@ID"].Value;
                    respuesta.detalle = "Registro creado con éxito.";
                    respuesta.codigo = id;

                    cmdExecute.Dispose();
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

        public Respuesta UpdateTipoPlan(TipoPlan item)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = null;

                try
                {
                    cmdExecute = Conexion.GetCommand("UPD_TIPO_PLAN");

                    cmdExecute.Parameters.AddWithValue("@ID", item.tpl_id);
                    cmdExecute.Parameters.AddWithValue("@NOMBRE", item.tpl_nombre);
                    cmdExecute.Parameters.AddWithValue("@CANTIDAD_INFORMES", item.tpl_cantidad_informes);
                    cmdExecute.Parameters.AddWithValue("@CANTIDAD_ADMINISTRADORES", item.tpl_cantidad_administradores);
                    cmdExecute.Parameters.AddWithValue("@ADMINISTRADORES_ILIMITADOS", item.tpl_administradores_ilimitados);
                    cmdExecute.Parameters.AddWithValue("@VALOR_PLAN", item.tpl_valor_plan);
                    cmdExecute.Parameters.AddWithValue("@HABILITADO", item.tpl_habilitado);
                    cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());
                    

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();
                    cmdExecute.Dispose();

                    respuesta.detalle = "Registro actualizado con éxito.";
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

        public Respuesta DeleteTipoPlan(TipoPlan item)
        {
            Respuesta respuesta = new Respuesta();
                    
            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = null;

                try
                {
                    cmdExecute = Conexion.GetCommand("DEL_TIPO_PLAN");
                    cmdExecute.Parameters.AddWithValue("@ID", item.tpl_id);

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();
                    cmdExecute.Dispose();

                    respuesta.detalle = "Registros eliminados con éxito.";  
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

        public void InformeTipoPlan(TipoPlan TPlan)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "RPT_TIPO_PLAN";
            if (TPlan.filtro != "") cmd.Parameters.AddWithValue("@FILTRO", TPlan.filtro);


            string filename = "INFORME TIPOS DE PLAN " + DateTime.Now;
            Tools.Excel.exportExcel(Conexion.GetDataTable(cmd), filename, true);
        }
    }
}