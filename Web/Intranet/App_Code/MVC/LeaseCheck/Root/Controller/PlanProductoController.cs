using LeaseCheck;
using LeaseCheck.Clientes;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;


public class PlanProductoController
{
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

}