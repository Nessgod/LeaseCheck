using LeaseCheck;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

public class ProductoController
{
    public List<Producto> GetProductos(Producto producto = null)
    {
        List<Producto> listado = new List<Producto>();

        if (LeaseCheck.Token.TokenSeguridad())
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = "SEL_PRODUCTOS";
                if (producto.pro_id > 0) cmd.Parameters.AddWithValue("@ID", producto.pro_id);
                if (producto.filtro != "") cmd.Parameters.AddWithValue("@FILTRO", producto.filtro);
                using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                {
                    while (dr.Read())
                    {
                        Producto item = new Producto();

                        item.pro_id = int.Parse(dr["PRO_ID"].ToString());
                        item.pro_producto = dr["PRO_PRODUCTO"].ToString();
                        item.pro_habilitado = Boolean.Parse(dr["pro_habilitado"].ToString());

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

    public Producto GetProducto(Producto filtro)
    {
        Producto item = new Producto();

        if (Token.TokenSeguridad())
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = "SEL_PRODUCTOS";
                cmd.Parameters.AddWithValue("@ID", filtro.pro_id);

                using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                {
                    while (dr.Read())
                    {
                        item.pro_id = int.Parse(dr["PRO_ID"].ToString());
                        item.pro_producto = dr["PRO_PRODUCTO"].ToString();
                        item.pro_habilitado = Boolean.Parse(dr["pro_habilitado"].ToString());
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

    public Respuesta InsertProducto(Producto item)
    {
        Respuesta respuesta = new Respuesta();

        if (Token.TokenSeguridad())
        {
            SqlCommand cmd = null;

            try
            {
                cmd = Conexion.GetCommand("INS_PRODUCTOS");

                cmd.Parameters.AddWithValue("@NOMBRE", item.pro_producto);
                cmd.Parameters.AddWithValue("@HABILITADO", item.pro_habilitado);

                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();

                respuesta.detalle = "Registro creado con éxito.";
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

    public Respuesta UpdateProductos(Producto item)
    {
        Respuesta respuesta = new Respuesta();

        if (Token.TokenSeguridad())
        {
            SqlCommand cmd = null;

            try
            {
                cmd = Conexion.GetCommand("UPD_PRODUCTOS");

                cmd.Parameters.AddWithValue("@ID", item.pro_id);
                cmd.Parameters.AddWithValue("@NOMBRE", item.pro_producto);
                cmd.Parameters.AddWithValue("@HABILITADO", item.pro_habilitado);

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

    public Respuesta DeleteProductos(Producto item)
    {
        Respuesta respuesta = new Respuesta();

        if (Token.TokenSeguridad())
        {
            SqlCommand cmd = null;

            try
            {
                cmd = Conexion.GetCommand("DEL_PRODUCTOS");
                cmd.Parameters.AddWithValue("@ID", item.pro_id);

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