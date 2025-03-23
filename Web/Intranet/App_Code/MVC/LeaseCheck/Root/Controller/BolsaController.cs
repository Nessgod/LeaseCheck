using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LeaseCheck.Root.Controller
{
    public class BolsaController
    {
        public List<Bolsa> GetBolsas(Bolsa bolsa = null)
        {
            List<Bolsa> listado = new List<Bolsa>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_BOLSAS";
                    if (bolsa.bls_id > 0) cmd.Parameters.AddWithValue("@ID", bolsa.bls_id);
                    if (bolsa.filtro != "") cmd.Parameters.AddWithValue("@FILTRO", bolsa.filtro);
                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Bolsa item = new Bolsa();

                            item.bls_id = int.Parse(dr["BLS_ID"].ToString());
                            item.bls_nombre = dr["BLS_NOMBRE"].ToString();
                            item.bls_cantidad = int.Parse(dr["BLS_CANTIDAD"].ToString());
                            item.bls_valor_plan = int.Parse(dr["BLS_VALOR_PLAN"].ToString());
                            item.bls_habilitado = bool.Parse(dr["bls_habilitado"].ToString());
                            item.bls_cantidad_administradores = int.Parse(dr["BLS_CANTIDAD_ADMINISTRADORES"].ToString());
                            item.bls_administradores_ilimitados = bool.Parse(dr["bls_administradores_ilimitados"].ToString());

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

        public Bolsa GetBolsa(Bolsa filtro)
        {
            Bolsa item = new Bolsa();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_BOLSAS";
                    cmd.Parameters.AddWithValue("@ID", filtro.bls_id);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            item.bls_id = int.Parse(dr["BLS_ID"].ToString());
                            item.bls_nombre = dr["BLS_NOMBRE"].ToString();
                            item.bls_cantidad = int.Parse(dr["BLS_CANTIDAD"].ToString());
                            item.bls_valor_plan = int.Parse(dr["BLS_VALOR_PLAN"].ToString());
                            item.bls_habilitado = bool.Parse(dr["bls_habilitado"].ToString());
                            item.bls_cantidad_administradores = int.Parse(dr["bls_cantidad_administradores"].ToString());
                            item.bls_administradores_ilimitados = bool.Parse(dr["bls_administradores_ilimitados"].ToString());
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

        public Respuesta InsertBolsa(Bolsa item)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = null;

                try
                {
                    int id = 0;
                    cmd = Conexion.GetCommand("INS_BOLSAS");
                    cmd.Parameters.AddWithValue("@ID", id).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@NOMBRE", item.bls_nombre);
                    cmd.Parameters.AddWithValue("@CANTIDAD", item.bls_cantidad);
                    cmd.Parameters.AddWithValue("@VALOR_PLAN", item.bls_valor_plan);
                    cmd.Parameters.AddWithValue("@HABILITADO", item.bls_habilitado);
                    cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    cmd.Parameters.AddWithValue("@PAIS", Session.Pais());
                    cmd.Parameters.AddWithValue("@CANTIDAD_ADMINISTRADORES", item.bls_cantidad_administradores);
                    cmd.Parameters.AddWithValue("@ADMINISTRADORES_ILIMITADOS", item.bls_administradores_ilimitados);

                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();

                    id = (int)cmd.Parameters["@ID"].Value;
                    respuesta.detalle = "Registro creado con éxito.";
                    respuesta.codigo = id;

                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    respuesta.detalle = ex.Message;
                    respuesta.error = true;
                }
            }

            return respuesta;
        }

        public Respuesta UpdateBosas(Bolsa item)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = null;

                try
                {
                    cmd = Conexion.GetCommand("UPD_BOLSAS");

                    cmd.Parameters.AddWithValue("@ID", item.bls_id);
                    cmd.Parameters.AddWithValue("@NOMBRE", item.bls_nombre);
                    cmd.Parameters.AddWithValue("@CANTIDAD", item.bls_cantidad);
                    cmd.Parameters.AddWithValue("@VALOR_PLAN", item.bls_valor_plan);
                    cmd.Parameters.AddWithValue("@HABILITADO", item.bls_habilitado);
                    cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    cmd.Parameters.AddWithValue("@PAIS", Session.Pais());
                    cmd.Parameters.AddWithValue("@CANTIDAD_ADMINISTRADORES", item.bls_cantidad_administradores);
                    cmd.Parameters.AddWithValue("@ADMINISTRADORES_ILIMITADOS", item.bls_administradores_ilimitados);

                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    cmd.Dispose();

                    respuesta.detalle = "Registro actualizado con éxito.";
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    respuesta.detalle = ex.Message;
                    respuesta.error = true;
                }
            }

            return respuesta;
        }

        public Respuesta DeleteBolsas(Bolsa item)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = null;

                try
                {
                    cmd = Conexion.GetCommand("DEL_BOLSAS");
                    cmd.Parameters.AddWithValue("@ID", item.bls_id);

                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    cmd.Dispose();

                    respuesta.detalle = "Registro eliminado con éxito.";
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    respuesta.detalle = ex.Message;
                    respuesta.error = true;
                }
            }

             return respuesta;
        }

        public void InformeBolsas(Bolsa bolsa)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "RPT_BOLSAS";
            if (bolsa.bls_id > 0) cmd.Parameters.AddWithValue("@BLS_ID", bolsa.bls_id);
            if (bolsa.filtro != "") cmd.Parameters.AddWithValue("@FILTRO", bolsa.filtro);

            string filename = "INFORME DE BOLSAS " + DateTime.Now;
            Tools.Excel.exportExcel(Conexion.GetDataTable(cmd), filename, true);
        }
        public List<BolsaProducto> GetListado(BolsaProducto bolsaProducto)
        {
            List<BolsaProducto> listado = new List<BolsaProducto>();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = "SEL_BOLSA_PRODUCTO";
                cmd.Parameters.AddWithValue("@ID_BOLSA", bolsaProducto.blp_bolsa);

                using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                {
                    while (dr.Read())
                    {
                        BolsaProducto item = new BolsaProducto();

                        item.blp_id = int.Parse(dr["blp_id"].ToString());
                        item.producto_nombre = dr["pro_producto"].ToString();
                        listado.Add(item);
                    }
                }

                cmd.Connection.Close();
                cmd.Dispose();

                return listado;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                cmd.Dispose();
                return listado;
            }
        }
        public List<BolsaProducto> GetListadoAgregar(BolsaProducto bolsaProducto)
        {
            List<BolsaProducto> listado = new List<BolsaProducto>();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = "SEL_BOLSA_PRODUCTO_AGREGAR";
                cmd.Parameters.AddWithValue("@ID_BOLSA", bolsaProducto.blp_bolsa);

                using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                {
                    while (dr.Read())
                    {
                        BolsaProducto item = new BolsaProducto();

                        item.blp_producto = int.Parse(dr["pro_id"].ToString());
                        item.producto_nombre = dr["pro_producto"].ToString();
                        listado.Add(item);
                    }
                }

                cmd.Connection.Close();
                cmd.Dispose();

                return listado;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                cmd.Dispose();
                return listado;
            }
        }
        public Respuesta InsertBolsaProducto(BolsaProducto item)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = null;

                try
                {
                    cmd = Conexion.GetCommand("INS_BOLSA_PRODUCTO");

                    cmd.Parameters.AddWithValue("@BOLSA", item.blp_bolsa);
                    cmd.Parameters.AddWithValue("@PRODUCTO", item.blp_producto);

                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    cmd.Dispose();

                    respuesta.detalle = "Producto agregado con éxito.";
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    respuesta.detalle = ex.Message;
                    respuesta.error = true;
                }
            }

            return respuesta;
        }
        public Respuesta DeleteBolsaProducto(BolsaProducto item)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = null;

                try
                {
                    cmd = Conexion.GetCommand("DEL_BOLSA_PRODUCTO");
                    cmd.Parameters.AddWithValue("@ID", item.blp_id);

                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    cmd.Dispose();

                    respuesta.detalle = "Registro eliminado con éxito.";
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    respuesta.detalle = ex.Message;
                    respuesta.error = true;
                }
            }

            return respuesta;
        }
    }
}