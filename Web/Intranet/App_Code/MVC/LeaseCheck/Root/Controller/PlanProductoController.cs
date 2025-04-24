using LeaseCheck;
using LeaseCheck.Clientes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


public class PlanProductoController
{
    #region Plan Producto
    public List<PlanProducto> GetListado(PlanProducto planProducto)
    {
        List<PlanProducto> listado = new List<PlanProducto>();
        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd.CommandText = "SEL_PLAN_PRODUCTO";
            cmd.Parameters.AddWithValue("@ID_PLAN", planProducto.plp_tipo_plan);

            using (SqlDataReader dr = Conexion.GetDataReader(cmd))
            {
                while (dr.Read())
                {
                    PlanProducto item = new PlanProducto();

                    item.plp_id = int.Parse(dr["plp_id"].ToString());
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

    public List<PlanProducto> GetListadoProductos(PlanProducto planProducto)
    {
        List<PlanProducto> listado = new List<PlanProducto>();
        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd.CommandText = "SEL_CLIENTE_PLAN_PRODUCTO";
            cmd.Parameters.AddWithValue("@ID_PLAN", planProducto.plp_tipo_plan);

            using (SqlDataReader dr = Conexion.GetDataReader(cmd))
            {
                while (dr.Read())
                {
                    PlanProducto item = new PlanProducto();
                    item.plp_producto = int.Parse(dr["pro_id"].ToString());
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
   
    public List<PlanProducto> GetListadoAgregar(PlanProducto planProducto)
    {
        List<PlanProducto> listado = new List<PlanProducto>();
        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd.CommandText = "SEL_PLAN_PRODUCTO_AGREGAR";
            cmd.Parameters.AddWithValue("@ID_PLAN", planProducto.plp_tipo_plan);

            using (SqlDataReader dr = Conexion.GetDataReader(cmd))
            {
                while (dr.Read())
                {
                    PlanProducto item = new PlanProducto();

                    item.plp_producto = int.Parse(dr["pro_id"].ToString());
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

    public Respuesta InsertPlanProducto(PlanProducto item)
    {
        Respuesta respuesta = new Respuesta();

        if (Token.TokenSeguridad())
        {
            SqlCommand cmd = null;

            try
            {
                cmd = Conexion.GetCommand("INS_PLAN_PRODUCTO");

                cmd.Parameters.AddWithValue("@TIPO_PLAN", item.plp_tipo_plan);
                cmd.Parameters.AddWithValue("@PRODUCTO", item.plp_producto);

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
    public Respuesta DeletePlanProducto(PlanProducto item)
    {
        Respuesta respuesta = new Respuesta();

        if (Token.TokenSeguridad())
        {
            SqlCommand cmd = null;

            try
            {
                cmd = Conexion.GetCommand("DEL_PLAN_PRODUCTO");
                cmd.Parameters.AddWithValue("@ID", item.plp_id);

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

    #endregion

    #region Producto Documento
    public List<PlanProductoDocumento> GetListadoProductosDocumento(PlanProductoDocumento planProducto)
    {
        List<PlanProductoDocumento> listado = new List<PlanProductoDocumento>();
        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd.CommandText = "SEL_PRODUCTO_DOCUMENTO";
            cmd.Parameters.AddWithValue("@ID_PRODUCTO", planProducto.prd_producto);

            using (SqlDataReader dr = Conexion.GetDataReader(cmd))
            {
                while (dr.Read())
                {
                    PlanProductoDocumento item = new PlanProductoDocumento();
                    item.prd_id = int.Parse(dr["prd_id"].ToString());
                    item.prd_producto = int.Parse(dr["prd_producto"].ToString());
                    item.prd_tipo_documento = int.Parse(dr["prd_tipo_documento"].ToString());
                    item.tdc_nombre = dr["tdc_nombre"].ToString();
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

    public List<PlanProductoDocumento> GetListadoAgregarDocumento(PlanProductoDocumento planProductoDocumento)
    {
        List<PlanProductoDocumento> listado = new List<PlanProductoDocumento>();
        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd.CommandText = "SEL_PRODUCTO_DOCUMENTO_AGREGAR";
            cmd.Parameters.AddWithValue("@ID_PRODUCTO", planProductoDocumento.prd_producto);

            using (SqlDataReader dr = Conexion.GetDataReader(cmd))
            {
                while (dr.Read())
                {
                    PlanProductoDocumento item = new PlanProductoDocumento();

                    item.tdc_id = int.Parse(dr["TDC_ID"].ToString());
                    item.tdc_nombre = dr["TDC_NOMBRE"].ToString();
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

    public Respuesta InsertProductoDocumento(PlanProductoDocumento item)
    {
        Respuesta respuesta = new Respuesta();

        if (Token.TokenSeguridad())
        {
            SqlCommand cmd = null;

            try
            {
                cmd = Conexion.GetCommand("INS_PRODUCTO_DOCUMENTO");

                cmd.Parameters.AddWithValue("@PRODUCTO", item.prd_producto);
                cmd.Parameters.AddWithValue("@DOCUMENTO", item.prd_tipo_documento);

                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();

                respuesta.detalle = "Documento agregado con éxito.";
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

    public Respuesta DeleteProductoDocumento(PlanProductoDocumento item)
    {
        Respuesta respuesta = new Respuesta();

        if (Token.TokenSeguridad())
        {
            SqlCommand cmd = null;

            try
            {
                cmd = Conexion.GetCommand("DEL_PRODUCTO_DOCUMENTO");
                cmd.Parameters.AddWithValue("@ID", item.prd_id);

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
    #endregion


    #region Producto Servicio
    public List<ClientePropiedadTipoServicio> GetListadoProductosServicio(ClientePropiedadTipoServicio servicio)
    {
        List<ClientePropiedadTipoServicio> listado = new List<ClientePropiedadTipoServicio>();
        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd.CommandText = "SEL_PRODUCTO_SERVICIO";
            cmd.Parameters.AddWithValue("@ID_PRODUCTO", servicio.psc_producto);

            using (SqlDataReader dr = Conexion.GetDataReader(cmd))
            {
                while (dr.Read())
                {
                    ClientePropiedadTipoServicio item = new ClientePropiedadTipoServicio();
                    item.psc_id = int.Parse(dr["psc_id"].ToString());
                    item.psc_producto = int.Parse(dr["psc_producto"].ToString());
                    item.psc_servicio = int.Parse(dr["psc_servicio"].ToString());
                    item.tsc_nombre = dr["tsc_nombre"].ToString();
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

    public List<ClientePropiedadTipoServicio> GetListadoAgregaServicio(ClientePropiedadTipoServicio servicio)
    {
        List<ClientePropiedadTipoServicio> listado = new List<ClientePropiedadTipoServicio>();
        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd.CommandText = "SEL_PRODUCTO_SERVICIO_AGREGAR";
            cmd.Parameters.AddWithValue("@ID_PRODUCTO", servicio.psc_producto);

            using (SqlDataReader dr = Conexion.GetDataReader(cmd))
            {
                while (dr.Read())
                {
                    ClientePropiedadTipoServicio item = new ClientePropiedadTipoServicio();

                    item.tsc_id = int.Parse(dr["TSC_ID"].ToString());
                    item.tsc_nombre = dr["TSC_NOMBRE"].ToString();
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

    public Respuesta InsertProductoServicio(ClientePropiedadTipoServicio item)
    {
        Respuesta respuesta = new Respuesta();

        if (Token.TokenSeguridad())
        {
            SqlCommand cmd = null;

            try
            {
                cmd = Conexion.GetCommand("INS_PRODUCTO_SERVICIO");

                cmd.Parameters.AddWithValue("@PRODUCTO", item.psc_producto);
                cmd.Parameters.AddWithValue("@SERVICIO", item.psc_servicio);

                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();

                respuesta.detalle = "Servicio agregado con éxito.";
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

    public Respuesta DeleteProductoServicio(ClientePropiedadTipoServicio item)
    {
        Respuesta respuesta = new Respuesta();

        if (Token.TokenSeguridad())
        {
            SqlCommand cmd = null;

            try
            {
                cmd = Conexion.GetCommand("DEL_PRODUCTO_SERVICIO");
                cmd.Parameters.AddWithValue("@ID", item.psc_id);

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
    #endregion
}