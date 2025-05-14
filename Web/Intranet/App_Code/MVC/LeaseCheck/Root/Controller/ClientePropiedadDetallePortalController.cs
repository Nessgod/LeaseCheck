using LeaseCheck;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;


public class ClientePropiedadDetallePortalController
{
    public List<ClientePropiedad> GetListadoPropiedadesPortal()
    {
        List<ClientePropiedad> listado = new List<ClientePropiedad>();
        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd.CommandText = "SEL_CLIENTE_PROPIEDAD_PORTAL";

            using (SqlDataReader dr = Conexion.GetDataReader(cmd))
            {
                while (dr.Read())
                {
                    ClientePropiedad item = new ClientePropiedad();
                    item.cpd_id = int.Parse(dr["cpd_id"].ToString());
                    item.cpd_tipo_propiedad = int.Parse(dr["cpd_tipo_propiedad"].ToString());
                    item.cpd_tipo_servicio = int.Parse(dr["cpd_tipo_servicio"].ToString());
                    item.cpd_tipo_entrega = int.Parse(dr["cpd_tipo_entrega"].ToString());
                    item.cpd_cliente = int.Parse(dr["cpd_cliente"].ToString());
                    item.cpd_titulo = dr["cpd_titulo"].ToString();
                    item.cpd_valor_uf = int.Parse(dr["cpd_valor_uf"].ToString());
                    item.cpd_valor_venta = int.Parse(dr["cpd_valor_venta"].ToString());
                    item.cpd_valor_evaluo_fiscal = int.Parse(dr["cpd_valor_evaluo_fiscal"].ToString());
                    item.cpd_usuario_creacion = int.Parse(dr["cpd_usuario_creacion"].ToString());
                    item.cpd_fecha_creacion = DateTime.Parse(dr["cpd_fecha_creacion"].ToString());

                    item.ESTADO = dr["ESTADO"].ToString();
                    item.TIPO_PROPIEDAD = dr["TIPO_PROPIEDAD"].ToString();
                    item.TIPO_SERVICIO = dr["TIPO_SERVICIO"].ToString();
                    item.CLIENTE = dr["CLIENTE"].ToString();
                    item.UBICACION = dr["UBICACION"].ToString();

                    item.SUPERFICIE_UTIL = dr["SUPERFICIE_UTIL"].ToString();
                    item.SUPERFICIE_TOTAL = dr["SUPERFICIE_TOTAL"].ToString();
                    item.DORMITORIOS = int.Parse(dr["DORMITORIOS"].ToString());
                    item.BAÑOS = int.Parse(dr["BAÑOS"].ToString());
                    item.PISOS = int.Parse(dr["PISOS"].ToString());

                    // Binario FRONTIS (puede venir como 0 si era NULL en DB)
                    item.IMAGEN_BINARIA = dr["CMB_BINARIO"] != DBNull.Value ? (byte[])dr["CMB_BINARIO"] : new byte[] { 0 };

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

    public ClientePropiedadDetallePotal GetClientePropiedadPortal(int id)
    {
        ClientePropiedadDetallePotal item = new ClientePropiedadDetallePotal();
        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd.CommandText = "SEL_CLIENTE_PROPIEDAD_PORTAL_DETALLE";
            cmd.Parameters.AddWithValue("@ID", id);

            using (SqlDataReader dr = Conexion.GetDataReader(cmd))
            {
                if (dr.Read())
                {
                    item.cpd_id = dr["cpd_id"] != DBNull.Value ? int.Parse(dr["cpd_id"].ToString()) : 0;
                    item.cpd_cliente = dr["cpd_cliente"] != DBNull.Value ? int.Parse(dr["cpd_cliente"].ToString()) : 0;
                    item.cpd_numero_propiedad = dr["cpd_numero_propiedad"] != DBNull.Value ? dr["cpd_numero_propiedad"].ToString() : "";
                    item.cpd_titulo = dr["cpd_titulo"] != DBNull.Value ? dr["cpd_titulo"].ToString() : "";
                    item.cpd_valor_uf = dr["cpd_valor_uf"] != DBNull.Value ? int.Parse(dr["cpd_valor_uf"].ToString()) : 0;
                    item.cpd_valor_venta = dr["cpd_valor_venta"] != DBNull.Value ? int.Parse(dr["cpd_valor_venta"].ToString()) : 0;
                    item.cpd_valor_evaluo_fiscal = dr["cpd_valor_evaluo_fiscal"] != DBNull.Value ? int.Parse(dr["cpd_valor_evaluo_fiscal"].ToString()) : 0;

                    item.cpd_contribucciones = dr["cpd_contribucciones"] != DBNull.Value ? bool.Parse(dr["cpd_contribucciones"].ToString()) : false;
                    item.cpd_derecho_municipal = dr["cpd_derecho_municipal"] != DBNull.Value ? bool.Parse(dr["cpd_derecho_municipal"].ToString()) : false;
                    item.cpd_estacionamiento = dr["cpd_estacionamiento"] != DBNull.Value ? bool.Parse(dr["cpd_estacionamiento"].ToString()) : false;
                    item.cpd_bodega = dr["cpd_bodega"] != DBNull.Value ? bool.Parse(dr["cpd_bodega"].ToString()) : false;
                    item.cpd_valor_estacionamiento = dr["cpd_valor_estacionamiento"] != DBNull.Value ? int.Parse(dr["cpd_valor_estacionamiento"].ToString()) : 0;
                    item.cpd_valor_bodega = dr["cpd_valor_bodega"] != DBNull.Value ? int.Parse(dr["cpd_valor_bodega"].ToString()) : 0;
                    item.cpd_cantidad_estacionamiento = dr["cpd_cantidad_estacionamiento"] != DBNull.Value ? int.Parse(dr["cpd_cantidad_estacionamiento"].ToString()) : 0;
                    item.cpd_cantidad_bodega = dr["cpd_cantidad_bodega"] != DBNull.Value ? int.Parse(dr["cpd_cantidad_bodega"].ToString()) : 0;

                    item.ESTADO = dr["ESTADO"] != DBNull.Value ? dr["ESTADO"].ToString() : "";
                    item.TIPO_PROPIEDAD = dr["TIPO_PROPIEDAD"] != DBNull.Value ? dr["TIPO_PROPIEDAD"].ToString() : "";
                    item.TIPO_SERVICIO = dr["TIPO_SERVICIO"] != DBNull.Value ? dr["TIPO_SERVICIO"].ToString() : "";
                    item.TIPO_ENTREGA = dr["TIPO_ENTREGA"] != DBNull.Value ? dr["TIPO_ENTREGA"].ToString() : "";
                    item.CLIENTE = dr["CLIENTE"] != DBNull.Value ? dr["CLIENTE"].ToString() : "";
                    item.PAIS = dr["PAIS"] != DBNull.Value ? dr["PAIS"].ToString() : "";
                    item.REGION = dr["REGION"] != DBNull.Value ? dr["REGION"].ToString() : "";
                    item.COMUNA = dr["COMUNA"] != DBNull.Value ? dr["COMUNA"].ToString() : "";
                    item.PROVINCIA = dr["PROVINCIA"] != DBNull.Value ? dr["PROVINCIA"].ToString() : "";

                    item.UBICACION = dr["UBICACION"] != DBNull.Value ? dr["UBICACION"].ToString() : "";
                    item.SUPERFICIE_UTIL = dr["SUPERFICIE_UTIL"] != DBNull.Value ? dr["SUPERFICIE_UTIL"].ToString() : "";
                    item.SUPERFICIE_TOTAL = dr["SUPERFICIE_TOTAL"] != DBNull.Value ? dr["SUPERFICIE_TOTAL"].ToString() : "";
                    item.DORMITORIOS = dr["DORMITORIOS"] != DBNull.Value ? int.Parse(dr["DORMITORIOS"].ToString()) : 0;
                    item.BAÑOS = dr["BAÑOS"] != DBNull.Value ? int.Parse(dr["BAÑOS"].ToString()) : 0;
                    item.PISOS = dr["PISOS"] != DBNull.Value ? int.Parse(dr["PISOS"].ToString()) : 0;

                    item.cpf_tipo_piso = dr["cpf_tipo_piso"] != DBNull.Value ? dr["cpf_tipo_piso"].ToString() : "";
                    item.cpf_tipo_ventana = dr["cpf_tipo_ventana"] != DBNull.Value ? dr["cpf_tipo_ventana"].ToString() : "";
                    item.cpf_conexion_cocina = dr["cpf_conexion_cocina"] != DBNull.Value ? dr["cpf_conexion_cocina"].ToString() : "";
                    item.cpf_conexion_lavadora = dr["cpf_conexion_lavadora"] != DBNull.Value ? dr["cpf_conexion_lavadora"].ToString() : "";

                    item.cpf_quincho = dr["cpf_quincho"] != DBNull.Value ? bool.Parse(dr["cpf_quincho"].ToString()) : false;
                    item.cpf_piscina = dr["cpf_piscina"] != DBNull.Value ? bool.Parse(dr["cpf_piscina"].ToString()) : false;
                    item.cpf_calefaccion = dr["cpf_calefaccion"] != DBNull.Value ? bool.Parse(dr["cpf_calefaccion"].ToString()) : false;
                    item.cpf_salon_multiple = dr["cpf_salon_multiple"] != DBNull.Value ? bool.Parse(dr["cpf_salon_multiple"].ToString()) : false;
                    item.cpf_gimnasio = dr["cpf_gimnasio"] != DBNull.Value ? bool.Parse(dr["cpf_gimnasio"].ToString()) : false;

                    item.cdp_conectividad = dr["cdp_conectividad"] != DBNull.Value ? dr["cdp_conectividad"].ToString() : "";
                    item.cdp_centro_comercial = dr["CDP_CENTRO_COMERCAL"] != DBNull.Value ? dr["CDP_CENTRO_COMERCAL"].ToString() : "";
                    item.cdp_servicio_salud = dr["cdp_servicio_salud"] != DBNull.Value ? dr["cdp_servicio_salud"].ToString() : "";
                    item.cdp_educacion = dr["cdp_educacion"] != DBNull.Value ? dr["cdp_educacion"].ToString() : "";
                    item.cdp_area_verde = dr["cdp_area_verde"] != DBNull.Value ? dr["cdp_area_verde"].ToString() : "";
                    item.cdp_seguridad = dr["cdp_seguridad"] != DBNull.Value ? dr["cdp_seguridad"].ToString() : "";
                    item.cdp_restaurant = dr["cdp_restaurant"] != DBNull.Value ? dr["cdp_restaurant"].ToString() : "";
                    item.cdp_transporte = dr["cdp_transporte"] != DBNull.Value ? dr["cdp_transporte"].ToString() : "";
                    item.cdp_descripcion = dr["cdp_descripcion"] != DBNull.Value ? dr["cdp_descripcion"].ToString() : "";


                    item.IMAGEN_BINARIA = dr["CMB_BINARIO"] != DBNull.Value ? (byte[])dr["CMB_BINARIO"] : null;
                }
            }

            cmd.Connection.Close();
            cmd.Dispose();
        }
        catch (Exception ex)
        {
            if (cmd.Connection != null) cmd.Connection.Close();
            cmd.Dispose();
            item = null;
        }

        return item;
    }

}