using LeaseCheck;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;



public class ClientePropiedadController
{
    #region Cliente Propiedad

    public List<ClientePropiedad> GetListadoPropiedades(ClientePropiedad planProducto)
    {
        List<ClientePropiedad> listado = new List<ClientePropiedad>();
        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd.CommandText = "SEL_CLIENTE_PROPIEDAD";

            // Obtener los perfiles del usuario desde la sesión
            string[] perfiles = Session.UsuarioPerfil().Split(',');

            // Verificar si el usuario tiene el perfil de AdministradorCorredora
            if (!perfiles.Contains(Convert.ToInt32(LeaseCheck.LeaseCheck.Perfiles.AdministradorCorredora).ToString()))
            {
                // Si no es AdministradorCorredora, filtrar por el usuario que creó la propiedad
                cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
            }

            using (SqlDataReader dr = Conexion.GetDataReader(cmd))
            {
                while (dr.Read())
                {
                    ClientePropiedad item = new ClientePropiedad();
                    item.cpd_id = int.Parse(dr["cpd_id"].ToString());
                    item.cpd_tipo_propiedad = int.Parse(dr["cpd_tipo_propiedad"].ToString());
                    item.cpd_tipo_servicio = int.Parse(dr["cpd_tipo_servicio"].ToString());
                    item.cpd_tipo_entrega = int.Parse(dr["cpd_tipo_entrega"].ToString());
                    item.cpd_fecha_entrega = DateTime.Parse(dr["cpd_fecha_entrega"].ToString());
                    item.cpd_fecha_entrega_detalle = dr["cpd_fecha_entrega_detalle"].ToString();
                    item.cpd_estado = int.Parse(dr["cpd_estado"].ToString());
                    item.cpd_cliente = int.Parse(dr["cpd_cliente"].ToString());
                    item.cpd_pais = int.Parse(dr["cpd_pais"].ToString());
                    item.cpd_region = int.Parse(dr["cpd_region"].ToString());
                    item.cpd_provincia = int.Parse(dr["cpd_provincia"].ToString());
                    item.cpd_comuna = int.Parse(dr["cpd_comuna"].ToString());
                    item.cpd_calle = dr["cpd_calle"].ToString();
                    item.cpd_numero_propiedad = dr["cpd_numero_propiedad"].ToString();
                    item.cpd_titulo = dr["cpd_titulo"].ToString();
                    item.cpd_valor_uf = int.Parse(dr["cpd_valor_uf"].ToString());
                    item.cpd_valor_venta = int.Parse(dr["cpd_valor_venta"].ToString());
                    item.cpd_valor_evaluo_fiscal = int.Parse(dr["cpd_valor_evaluo_fiscal"].ToString());
                    item.cpd_contribucciones = Boolean.Parse(dr["cpd_contribucciones"].ToString());
                    item.cpd_derecho_municipal = Boolean.Parse(dr["cpd_derecho_municipal"].ToString());
                    item.cpd_bodega = Boolean.Parse(dr["cpd_bodega"].ToString());
                    item.cpd_estacionamiento = Boolean.Parse(dr["cpd_estacionamiento"].ToString());
                    item.cpd_valor_estacionamiento = int.Parse(dr["cpd_valor_estacionamiento"].ToString());
                    item.cpd_valor_bodega = int.Parse(dr["cpd_valor_bodega"].ToString());
                    item.cpd_cantidad_estacionamiento = int.Parse(dr["cpd_cantidad_estacionamiento"].ToString());
                    item.cpd_cantidad_bodega = int.Parse(dr["cpd_cantidad_bodega"].ToString());
                    item.cpd_usuario_creacion = int.Parse(dr["cpd_usuario_creacion"].ToString());
                    item.cpd_usuario_act = int.Parse(dr["cpd_usuario_act"].ToString());
                    item.cpd_fecha_creacion = DateTime.Parse(dr["cpd_fecha_creacion"].ToString());
                    item.cpd_fecha_act = DateTime.Parse(dr["cpd_fecha_act"].ToString());
                    item.ESTADO = dr["ESTADO"].ToString();
                    item.TIPO_PROPIEDAD = dr["TIPO_PROPIEDAD"].ToString();
                    item.PAIS = dr["PAIS"].ToString();
                    item.CLIENTE = dr["CLIENTE"].ToString();
                    item.COMUNA = dr["COMUNA"].ToString();
                    item.REGION = dr["REGION"].ToString();
                    item.PROVINCIA = dr["PROVINCIA"].ToString();
                    item.TIPO_ENTREGA = dr["TIPO_ENTREGA"].ToString();
                    item.TIPO_SERVICIO = dr["TIPO_SERVICIO"].ToString();
                    if (dr["GANANCIA"] != DBNull.Value)
                    {
                        item.GANANCIA = double.Parse(dr["GANANCIA"].ToString());
                    }
                    else
                    {
                        item.GANANCIA = 0.0;  
                    }


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

    public ClientePropiedad GetClientePropiedad(ClientePropiedad clientePropiedad)
    {
        ClientePropiedad item = new ClientePropiedad();

        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd.CommandText = "SEL_CLIENTE_PROPIEDAD";
            if (clientePropiedad.cpd_id > 0) cmd.Parameters.AddWithValue("@ID", clientePropiedad.cpd_id);

            using (SqlDataReader dr = Conexion.GetDataReader(cmd))
            {
                if (dr.Read())
                {
                    item.cpd_id = int.Parse(dr["cpd_id"].ToString());
                    item.cpd_tipo_propiedad = int.Parse(dr["cpd_tipo_propiedad"].ToString());
                    item.cpd_tipo_servicio = int.Parse(dr["cpd_tipo_servicio"].ToString());
                    item.cpd_tipo_entrega = int.Parse(dr["cpd_tipo_entrega"].ToString());
                    item.cpd_fecha_entrega = DateTime.Parse(dr["cpd_fecha_entrega"].ToString());
                    item.cpd_fecha_entrega_detalle = dr["cpd_fecha_entrega_detalle"].ToString();
                    item.cpd_estado = int.Parse(dr["cpd_estado"].ToString());
                    item.cpd_cliente = int.Parse(dr["cpd_cliente"].ToString());
                    item.cpd_pais = int.Parse(dr["cpd_pais"].ToString());
                    item.cpd_region = int.Parse(dr["cpd_region"].ToString());
                    item.cpd_provincia = int.Parse(dr["cpd_provincia"].ToString());
                    item.cpd_comuna = int.Parse(dr["cpd_comuna"].ToString());
                    item.cpd_calle = dr["cpd_calle"].ToString();
                    item.cpd_numero_propiedad = dr["cpd_numero_propiedad"].ToString();
                    item.cpd_titulo = dr["cpd_titulo"].ToString();
                    item.cpd_valor_uf = int.Parse(dr["cpd_valor_uf"].ToString());
                    item.cpd_valor_venta = int.Parse(dr["cpd_valor_venta"].ToString());
                    item.cpd_valor_evaluo_fiscal = int.Parse(dr["cpd_valor_evaluo_fiscal"].ToString());
                    item.cpd_contribucciones = Boolean.Parse(dr["cpd_contribucciones"].ToString());
                    item.cpd_derecho_municipal = Boolean.Parse(dr["cpd_derecho_municipal"].ToString());
                    item.cpd_bodega = Boolean.Parse(dr["cpd_bodega"].ToString());
                    item.cpd_estacionamiento = Boolean.Parse(dr["cpd_estacionamiento"].ToString());
                    item.cpd_valor_estacionamiento = int.Parse(dr["cpd_valor_estacionamiento"].ToString());
                    item.cpd_valor_bodega = int.Parse(dr["cpd_valor_bodega"].ToString());
                    item.cpd_cantidad_estacionamiento = int.Parse(dr["cpd_cantidad_estacionamiento"].ToString());
                    item.cpd_cantidad_bodega = int.Parse(dr["cpd_cantidad_bodega"].ToString());
                    item.cpd_usuario_creacion = int.Parse(dr["cpd_usuario_creacion"].ToString());
                    item.cpd_usuario_act = int.Parse(dr["cpd_usuario_act"].ToString());
                    item.cpd_fecha_creacion = DateTime.Parse(dr["cpd_fecha_creacion"].ToString());
                    item.cpd_fecha_act = DateTime.Parse(dr["cpd_fecha_act"].ToString());
                    item.ESTADO = dr["ESTADO"].ToString();
                    item.TIPO_PROPIEDAD = dr["TIPO_PROPIEDAD"].ToString();
                    item.PAIS = dr["PAIS"].ToString();
                    item.CLIENTE = dr["CLIENTE"].ToString();
                    item.COMUNA = dr["COMUNA"].ToString();
                    item.REGION = dr["REGION"].ToString();
                    item.PROVINCIA = dr["PROVINCIA"].ToString();
                    item.TIPO_ENTREGA = dr["TIPO_ENTREGA"].ToString();
                    item.TIPO_SERVICIO = dr["TIPO_SERVICIO"].ToString();

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

        return item;
    }
    public Respuesta InsertClientePropiedad(ClientePropiedad item)
    {
        Respuesta respuesta = new Respuesta();

        if (Token.TokenSeguridad())
        {
            SqlCommand cmd = null;

            try
            {
                cmd = Conexion.GetCommand("INS_CLIENTE_PROPIEDAD");

                cmd.Parameters.AddWithValue("@ID", item.cpd_id);
                cmd.Parameters.AddWithValue("@TIPO_PROPIEDAD", item.cpd_tipo_propiedad);
                cmd.Parameters.AddWithValue("@TIPO_SERVICIO", item.cpd_tipo_servicio);
                cmd.Parameters.AddWithValue("@TIPO_ENTREGA", item.cpd_tipo_entrega);
                cmd.Parameters.AddWithValue("@FECHA_ENTREGA", item.cpd_fecha_entrega);
                cmd.Parameters.AddWithValue("@FECHA_ENTREGA_DETALLE", item.cpd_fecha_entrega_detalle);
                cmd.Parameters.AddWithValue("@ESTADO", item.cpd_estado);
                cmd.Parameters.AddWithValue("@PAIS", item.cpd_pais);
                cmd.Parameters.AddWithValue("@REGION", item.cpd_region);
                cmd.Parameters.AddWithValue("@PROVINCIA", item.cpd_provincia);
                cmd.Parameters.AddWithValue("@COMUNA", item.cpd_comuna);
                cmd.Parameters.AddWithValue("@CALLE", item.cpd_calle);
                cmd.Parameters.AddWithValue("@NUMERO_PROPIEDAD", item.cpd_numero_propiedad);
                cmd.Parameters.AddWithValue("@TITULO", item.cpd_titulo);
                cmd.Parameters.AddWithValue("@VALOR_UF", item.cpd_valor_uf);
                cmd.Parameters.AddWithValue("@VALOR_VENTA", item.cpd_valor_venta);
                cmd.Parameters.AddWithValue("@VALOR_EVALUO_FISCAL", item.cpd_valor_evaluo_fiscal);
                cmd.Parameters.AddWithValue("@CONTRIBUCCIONES", item.cpd_contribucciones);
                cmd.Parameters.AddWithValue("@DERECHO_MUNICIPAL", item.cpd_derecho_municipal);
                cmd.Parameters.AddWithValue("@ESTACIONAMIENTO", item.cpd_estacionamiento);
                cmd.Parameters.AddWithValue("@BODEGA", item.cpd_bodega);
                cmd.Parameters.AddWithValue("@VALOR_ESTACIONAMIENTO", item.cpd_valor_estacionamiento);
                cmd.Parameters.AddWithValue("@VALOR_BODEGA", item.cpd_valor_bodega);
                cmd.Parameters.AddWithValue("@CANTIDAD_ESTACIONAMIENTO", item.cpd_cantidad_estacionamiento);
                cmd.Parameters.AddWithValue("@CANTIDAD_BODEGA", item.cpd_cantidad_bodega);
                cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());

                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();

                respuesta.detalle = "Propiedad agregado con éxito.";
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
    public Respuesta UpdateClientePropiedad(ClientePropiedad item)
    {
        SqlCommand cmdExecute = new SqlCommand();

        Respuesta respuesta = new Respuesta();

        if (Token.TokenSeguridad())
        {
            try
            {
                cmdExecute = Conexion.GetCommand("UPD_CLIENTE_PROPIEDAD");
                cmdExecute.Parameters.AddWithValue("@ID", item.cpd_id);
                cmdExecute.Parameters.AddWithValue("@TIPO_PROPIEDAD", item.cpd_tipo_propiedad);
                cmdExecute.Parameters.AddWithValue("@TIPO_SERVICIO", item.cpd_tipo_servicio);
                cmdExecute.Parameters.AddWithValue("@TIPO_ENTREGA", item.cpd_tipo_entrega);
                cmdExecute.Parameters.AddWithValue("@FECHA_ENTREGA", item.cpd_fecha_entrega);
                cmdExecute.Parameters.AddWithValue("@FECHA_ENTREGA_DETALLE", item.cpd_fecha_entrega_detalle);
                cmdExecute.Parameters.AddWithValue("@ESTADO", item.cpd_estado);
                cmdExecute.Parameters.AddWithValue("@PAIS", item.cpd_pais);
                cmdExecute.Parameters.AddWithValue("@REGION", item.cpd_region);
                cmdExecute.Parameters.AddWithValue("@PROVINCIA", item.cpd_provincia);
                cmdExecute.Parameters.AddWithValue("@COMUNA", item.cpd_comuna);
                cmdExecute.Parameters.AddWithValue("@CALLE", item.cpd_calle);
                cmdExecute.Parameters.AddWithValue("@NUMERO_PROPIEDAD", item.cpd_numero_propiedad);
                cmdExecute.Parameters.AddWithValue("@TITULO", item.cpd_titulo);
                cmdExecute.Parameters.AddWithValue("@VALOR_UF", item.cpd_valor_uf);
                cmdExecute.Parameters.AddWithValue("@VALOR_VENTA", item.cpd_valor_venta);
                cmdExecute.Parameters.AddWithValue("@VALOR_EVALUO_FISCAL", item.cpd_valor_evaluo_fiscal);
                cmdExecute.Parameters.AddWithValue("@CONTRIBUCCIONES", item.cpd_contribucciones);
                cmdExecute.Parameters.AddWithValue("@DERECHO_MUNICIPAL", item.cpd_derecho_municipal);
                cmdExecute.Parameters.AddWithValue("@ESTACIONAMIENTO", item.cpd_estacionamiento);
                cmdExecute.Parameters.AddWithValue("@BODEGA", item.cpd_bodega);
                cmdExecute.Parameters.AddWithValue("@VALOR_ESTACIONAMIENTO", item.cpd_valor_estacionamiento);
                cmdExecute.Parameters.AddWithValue("@VALOR_BODEGA", item.cpd_valor_bodega);
                cmdExecute.Parameters.AddWithValue("@CANTIDAD_ESTACIONAMIENTO", item.cpd_cantidad_estacionamiento);
                cmdExecute.Parameters.AddWithValue("@CANTIDAD_BODEGA", item.cpd_cantidad_bodega);
                cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());


                cmdExecute.ExecuteNonQuery();
                cmdExecute.Connection.Close();

                respuesta.codigo = 0;
                respuesta.detalle = "Propiedad actualizado con éxito.";
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
    public Respuesta UpdateClientePropiedadEstado(ClientePropiedad item)
    {
        SqlCommand cmdExecute = new SqlCommand();

        Respuesta respuesta = new Respuesta();

        if (Token.TokenSeguridad())
        {
            try
            {
                cmdExecute = Conexion.GetCommand("UPD_CLIENTE_PROPIEDAD_ESTADO");
                cmdExecute.Parameters.AddWithValue("@ID", item.cpd_id);
                cmdExecute.Parameters.AddWithValue("@ESTADO", item.cpd_estado);
                cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());
                cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());


                cmdExecute.ExecuteNonQuery();
                cmdExecute.Connection.Close();

                respuesta.codigo = 0;
                respuesta.detalle = "País actualizado con éxito.";
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
    public Respuesta DeleteClientePropiedad(ClientePropiedad item)
    {
        Respuesta respuesta = new Respuesta();

        if (Token.TokenSeguridad())
        {
            SqlCommand cmd = null;

            try
            {
                cmd = Conexion.GetCommand("DEL_CLIENTE_PROPIEDAD");
                cmd.Parameters.AddWithValue("@ID", item.cpd_id);

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


    public ClientePropiedadDatoLegal GetClientePropiedadDatoLegal(ClientePropiedadDatoLegal clientePropiedad)
    {
        ClientePropiedadDatoLegal item = new ClientePropiedadDatoLegal();

        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd.CommandText = "SEL_CLIENTE_PROPIEDAD_DATO_LEGAL";
            if (clientePropiedad.cdl_id > 0) cmd.Parameters.AddWithValue("@ID", clientePropiedad.cdl_id);
            if (clientePropiedad.cdl_id_propiedad > 0) cmd.Parameters.AddWithValue("@PROPIEDAD", clientePropiedad.cdl_id_propiedad);

            using (SqlDataReader dr = Conexion.GetDataReader(cmd))
            {
                if (dr.Read())
                {
                    item.cdl_id = int.Parse(dr["CDL_ID"].ToString());
                    item.cdl_id_propiedad = int.Parse(dr["CDL_ID_PROPIEDAD"].ToString());
                    item.cdl_id_propietario = int.Parse(dr["CDL_ID_PROPIETARIO"].ToString());
                    item.cdl_rol = dr["CDL_ROL"].ToString();
                    item.cdl_anio_inscripcion = int.Parse(dr["CDL_ANIO_INSCRIPCION"].ToString());
                    item.cdl_copia_llaves = int.Parse(dr["CDL_COPIA_LLAVES"].ToString());
                    item.cdl_numero_inscripcion = dr["CDL_NUMERO_INSCRIPCION"].ToString();
                    item.cdl_numero_manzana = dr["CDL_NUMERO_MANZANA"].ToString();
                    item.cdl_numero_sitio = dr["CDL_NUMERO_SITIO"].ToString();
                    item.cdl_conjunto_habitacional = dr["CDL_CONJUNTO_HABITACIONAL"].ToString();
                    item.cdl_fajas = dr["CDL_FAJAS"].ToString();

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

        return item;
    }
    public Respuesta InsertClientePropiedadDatoLegal(ClientePropiedadDatoLegal item)
    {
        Respuesta respuesta = new Respuesta();

        if (Token.TokenSeguridad())
        {
            SqlCommand cmd = null;

            try
            {
                cmd = Conexion.GetCommand("INS_CLIENTE_PROPIEDAD_DATO_LEGAL");

                cmd.Parameters.AddWithValue("@ID", item.cdl_id);
                cmd.Parameters.AddWithValue("@PROPIEDAD", item.cdl_id_propiedad);
                cmd.Parameters.AddWithValue("@PROPIETARIO", item.cdl_id_propietario);
                cmd.Parameters.AddWithValue("@ROL", item.cdl_rol);
                cmd.Parameters.AddWithValue("@NUMERO_INSCRIPCION", item.cdl_numero_inscripcion);
                cmd.Parameters.AddWithValue("@ANIO", item.cdl_anio_inscripcion);
                cmd.Parameters.AddWithValue("@NUMERO_MANZANA", item.cdl_numero_manzana);
                cmd.Parameters.AddWithValue("@NUMERO_SITIO", item.cdl_numero_sitio);
                cmd.Parameters.AddWithValue("@CONJUNTO_HABITACIONAL", item.cdl_conjunto_habitacional);
                cmd.Parameters.AddWithValue("@INVENTARIO", item.cdl_inventario);
                cmd.Parameters.AddWithValue("@COPIA_LLAVES", item.cdl_copia_llaves);
                cmd.Parameters.AddWithValue("@FAJAS", item.cdl_fajas);
                cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                cmd.Parameters.AddWithValue("@PAIS", Session.Pais());

                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();

                respuesta.detalle = "Propiedad dato legal agregado con éxito.";
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
    public Respuesta UpdateClientePropiedadDatoLegal(ClientePropiedadDatoLegal item)
    {
        SqlCommand cmdExecute = new SqlCommand();

        Respuesta respuesta = new Respuesta();

        if (Token.TokenSeguridad())
        {
            try
            {
                cmdExecute = Conexion.GetCommand("UPD_CLIENTE_PROPIEDAD_DATO_LEGAL");
                cmdExecute.Parameters.AddWithValue("@ID", item.cdl_id);
                cmdExecute.Parameters.AddWithValue("@PROPIEDAD", item.cdl_id_propiedad);
                cmdExecute.Parameters.AddWithValue("@PROPIETARIO", item.cdl_id_propietario);
                cmdExecute.Parameters.AddWithValue("@ROL", item.cdl_rol);
                cmdExecute.Parameters.AddWithValue("@NUMERO_INSCRIPCION", item.cdl_numero_inscripcion);
                cmdExecute.Parameters.AddWithValue("@ANIO", item.cdl_anio_inscripcion);
                cmdExecute.Parameters.AddWithValue("@NUMERO_MANZANA", item.cdl_numero_manzana);
                cmdExecute.Parameters.AddWithValue("@NUMERO_SITIO", item.cdl_numero_sitio);
                cmdExecute.Parameters.AddWithValue("@CONJUNTO_HABITACIONAL", item.cdl_conjunto_habitacional);
                cmdExecute.Parameters.AddWithValue("@INVENTARIO", item.cdl_inventario);
                cmdExecute.Parameters.AddWithValue("@COPIA_LLAVES", item.cdl_copia_llaves);
                cmdExecute.Parameters.AddWithValue("@FAJAS", item.cdl_fajas);
                cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());


                cmdExecute.ExecuteNonQuery();
                cmdExecute.Connection.Close();

                respuesta.codigo = 0;
                respuesta.detalle = "Propiedad dato legal actualizado con éxito.";
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

    public ClientePropiedadFicha GetClientePropiedadFicha(ClientePropiedadFicha clientePropiedadFicha)
    {
        ClientePropiedadFicha item = new ClientePropiedadFicha();

        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd.CommandText = "SEL_CLIENTE_PROPIEDAD_FICHA";
            if (clientePropiedadFicha.cpf_id > 0) cmd.Parameters.AddWithValue("@ID", clientePropiedadFicha.cpf_id);
            if (clientePropiedadFicha.cpf_id_propiedad > 0) cmd.Parameters.AddWithValue("@PROPIEDAD", clientePropiedadFicha.cpf_id_propiedad);

            using (SqlDataReader dr = Conexion.GetDataReader(cmd))
            {
                if (dr.Read())
                {
                    item.cpf_id = int.Parse(dr["CPF_ID"].ToString());
                    item.cpf_id_propiedad = int.Parse(dr["CPF_ID_PROPIEDAD"].ToString());
                    item.cpf_pisos = int.Parse(dr["CPF_PISOS"].ToString());
                    item.cpf_dormitorio = int.Parse(dr["CPF_DORMITORIO"].ToString());
                    item.cpf_baño = int.Parse(dr["CPF_BAÑO"].ToString());
                    item.cpf_ubicacion_piso = int.Parse(dr["cpf_ubicacion_piso"].ToString());
                    item.cpf_conexion_cocina = dr["CPF_CONEXION_COCINA"].ToString();
                    item.cpf_conexion_lavadora = dr["CPF_CONEXION_LAVADORA"].ToString();
                    item.cpf_superficie_total = dr["CPF_SUPERFICIE_TOTAL"].ToString();
                    item.cpf_superficie_util = dr["CPF_SUPERFICIE_UTIL"].ToString();
                    item.cpf_tipo_piso = dr["CPF_TIPO_PISO"].ToString();
                    item.cpf_tipo_ventana = dr["CPF_TIPO_VENTANA"].ToString();
                    item.cpf_calefaccion = Boolean.Parse(dr["CPF_CALEFACCION"].ToString());
                    item.cpf_piscina = Boolean.Parse(dr["CPF_PISCINA"].ToString());
                    item.cpf_gimnasio = Boolean.Parse(dr["CPF_GIMNASIO"].ToString());
                    item.cpf_quincho = Boolean.Parse(dr["CPF_QUINCHO"].ToString());
                    item.cpf_salon_multiple = Boolean.Parse(dr["CPF_SALON_MULTIPLE"].ToString());
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

        return item;
    }

    public Respuesta InsertClientePropiedadFicha(ClientePropiedadFicha item)
    {
        Respuesta respuesta = new Respuesta();

        if (Token.TokenSeguridad())
        {
            SqlCommand cmd = null;

            try
            {
                cmd = Conexion.GetCommand("INS_CLIENTE_PROPIEDAD_FICHA");

                cmd.Parameters.AddWithValue("@ID", item.cpf_id);
                cmd.Parameters.AddWithValue("@PROPIEDAD", item.cpf_id_propiedad);
                cmd.Parameters.AddWithValue("@SUPERFICIE_UTIL", item.cpf_superficie_util);
                cmd.Parameters.AddWithValue("@SUPERFICIE_TOTAL", item.cpf_superficie_total);
                cmd.Parameters.AddWithValue("@DORMITORIO", item.cpf_dormitorio);
                cmd.Parameters.AddWithValue("@PISO", item.cpf_pisos);
                cmd.Parameters.AddWithValue("@BAÑO", item.cpf_baño);
                cmd.Parameters.AddWithValue("@UBICACION_PISO", item.cpf_ubicacion_piso);
                cmd.Parameters.AddWithValue("@TIPO_PISO", item.cpf_tipo_piso);
                cmd.Parameters.AddWithValue("@TIPO_VENTANA", item.cpf_tipo_ventana);
                cmd.Parameters.AddWithValue("@CONEXION_LAVADORA", item.cpf_conexion_lavadora);
                cmd.Parameters.AddWithValue("@CONEXION_COCINA", item.cpf_conexion_cocina);
                cmd.Parameters.AddWithValue("@QUINCHO", item.cpf_quincho);
                cmd.Parameters.AddWithValue("@PISCINA", item.cpf_piscina);
                cmd.Parameters.AddWithValue("@GIMNASIO", item.cpf_gimnasio);
                cmd.Parameters.AddWithValue("@CALEFACCION", item.cpf_calefaccion);
                cmd.Parameters.AddWithValue("@SALON_MULTIPLE", item.cpf_salon_multiple);
                cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                cmd.Parameters.AddWithValue("@PAIS", Session.Pais());


                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();

                respuesta.detalle = "Propiedad ficha agregada con éxito.";
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

    public Respuesta UpdateClientePropiedadFicha(ClientePropiedadFicha item)
    {
        SqlCommand cmdExecute = new SqlCommand();

        Respuesta respuesta = new Respuesta();

        if (Token.TokenSeguridad())
        {
            try
            {
                cmdExecute = Conexion.GetCommand("UPD_CLIENTE_PROPIEDAD_FICHA");
                cmdExecute.Parameters.AddWithValue("@ID", item.cpf_id);
                cmdExecute.Parameters.AddWithValue("@PROPIEDAD", item.cpf_id_propiedad);
                cmdExecute.Parameters.AddWithValue("@SUPERFICIE_UTIL", item.cpf_superficie_util);
                cmdExecute.Parameters.AddWithValue("@SUPERFICIE_TOTAL", item.cpf_superficie_total);
                cmdExecute.Parameters.AddWithValue("@DORMITORIO", item.cpf_dormitorio);
                cmdExecute.Parameters.AddWithValue("@PISO", item.cpf_pisos);
                cmdExecute.Parameters.AddWithValue("@BAÑO", item.cpf_baño);
                cmdExecute.Parameters.AddWithValue("@UBICACION_PISO", item.cpf_ubicacion_piso);
                cmdExecute.Parameters.AddWithValue("@TIPO_PISO", item.cpf_tipo_piso);
                cmdExecute.Parameters.AddWithValue("@TIPO_VENTANA", item.cpf_tipo_ventana);
                cmdExecute.Parameters.AddWithValue("@CONEXION_LAVADORA", item.cpf_conexion_lavadora);
                cmdExecute.Parameters.AddWithValue("@CONEXION_COCINA", item.cpf_conexion_cocina);
                cmdExecute.Parameters.AddWithValue("@QUINCHO", item.cpf_quincho);
                cmdExecute.Parameters.AddWithValue("@PISCINA", item.cpf_piscina);
                cmdExecute.Parameters.AddWithValue("@GIMNASIO", item.cpf_gimnasio);
                cmdExecute.Parameters.AddWithValue("@CALEFACCION", item.cpf_calefaccion);
                cmdExecute.Parameters.AddWithValue("@SALON_MULTIPLE", item.cpf_salon_multiple);
                cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());



                cmdExecute.ExecuteNonQuery();
                cmdExecute.Connection.Close();

                respuesta.codigo = 0;
                respuesta.detalle = "Propiedad ficha actualizada con éxito.";
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



    public Respuesta InsertClientePropiedadDetallePublicacion(ClientePropiedadDetallePublicacion item)
    {
        Respuesta respuesta = new Respuesta();

        if (Token.TokenSeguridad())
        {
            SqlCommand cmd = null;

            try
            {
                cmd = Conexion.GetCommand("INS_CLIENTE_PROPIEDAD_DETALLE_PUBLICACION");

                cmd.Parameters.AddWithValue("@ID", item.cdp_id);
                cmd.Parameters.AddWithValue("@PROPIEDAD", item.cdp_id_propiedad);
                cmd.Parameters.AddWithValue("@CONECTIVIDAD", item.cdp_conectividad);
                cmd.Parameters.AddWithValue("@SEGURIDAD", item.cdp_seguridad);
                cmd.Parameters.AddWithValue("@SERVICIO_SALUD", item.cdp_servicio_salud);
                cmd.Parameters.AddWithValue("@AREA_VERDE", item.cdp_area_verde);
                cmd.Parameters.AddWithValue("@DESCRIPCION", item.cdp_descripcion);
                cmd.Parameters.AddWithValue("@EDUCACION", item.cdp_educacion);
                cmd.Parameters.AddWithValue("@RESTAURANT", item.cdp_restaurant);
                cmd.Parameters.AddWithValue("@CENTRO_COMERCIAL", item.cdp_centro_comercial);
                cmd.Parameters.AddWithValue("@TRANSPORTE", item.cdp_transporte);
                cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                cmd.Parameters.AddWithValue("@PAIS", Session.Pais());


                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();

                respuesta.detalle = "Propiedad detalle publicación agregada con éxito.";
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

    public ClientePropiedadDetallePublicacion GetClientePropiedadDetallePublicacion(ClientePropiedadDetallePublicacion clientePropiedadDetallePublicacion)
    {
        ClientePropiedadDetallePublicacion item = new ClientePropiedadDetallePublicacion();

        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd.CommandText = "SEL_CLIENTE_PROPIEDAD_DETALLE_PUBLICACION";
            if (clientePropiedadDetallePublicacion.cdp_id > 0) cmd.Parameters.AddWithValue("@ID", clientePropiedadDetallePublicacion.cdp_id);
            if (clientePropiedadDetallePublicacion.cdp_id_propiedad > 0) cmd.Parameters.AddWithValue("@PROPIEDAD", clientePropiedadDetallePublicacion.cdp_id_propiedad);

            using (SqlDataReader dr = Conexion.GetDataReader(cmd))
            {
                if (dr.Read())
                {
                    item.cdp_id = int.Parse(dr["CDP_ID"].ToString());
                    item.cdp_id_propiedad = int.Parse(dr["CDP_ID_PROPIEDAD"].ToString());
                    item.cdp_conectividad = dr["CDP_CONECTIVIDAD"].ToString();
                    item.cdp_centro_comercial = dr["CDP_CENTRO_COMERCAL"].ToString();
                    item.cdp_servicio_salud = dr["CDP_SERVICIO_SALUD"].ToString();
                    item.cdp_educacion = dr["CDP_EDUCACION"].ToString();
                    item.cdp_area_verde = dr["CDP_AREA_VERDE"].ToString();
                    item.cdp_restaurant = dr["CDP_RESTAURANT"].ToString();
                    item.cdp_seguridad = dr["CDP_SEGURIDAD"].ToString();
                    item.cdp_transporte = dr["CDP_TRANSPORTE"].ToString();
                    item.cdp_descripcion = dr["CDP_DESCRIPCION"].ToString();
   
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

        return item;
    }

    public Respuesta UpdateClientePropiedadDetallePublicacion(ClientePropiedadDetallePublicacion item)
    {
        SqlCommand cmdExecute = new SqlCommand();

        Respuesta respuesta = new Respuesta();

        if (Token.TokenSeguridad())
        {
            try
            {
                cmdExecute = Conexion.GetCommand("UPD_CLIENTE_PROPIEDAD_DETALLE_PUBLICACION");
                cmdExecute.Parameters.AddWithValue("@ID", item.cdp_id);
                cmdExecute.Parameters.AddWithValue("@PROPIEDAD", item.cdp_id_propiedad);
                cmdExecute.Parameters.AddWithValue("@CONECTIVIDAD", item.cdp_conectividad);
                cmdExecute.Parameters.AddWithValue("@SEGURIDAD", item.cdp_seguridad);
                cmdExecute.Parameters.AddWithValue("@SERVICIO_SALUD", item.cdp_servicio_salud);
                cmdExecute.Parameters.AddWithValue("@AREA_VERDE", item.cdp_area_verde);
                cmdExecute.Parameters.AddWithValue("@DESCRIPCION", item.cdp_descripcion);
                cmdExecute.Parameters.AddWithValue("@EDUCACION", item.cdp_educacion);
                cmdExecute.Parameters.AddWithValue("@RESTAURANT", item.cdp_restaurant);
                cmdExecute.Parameters.AddWithValue("@CENTRO_COMERCIAL", item.cdp_centro_comercial);
                cmdExecute.Parameters.AddWithValue("@TRANSPORTE", item.cdp_transporte);
                cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());



                cmdExecute.ExecuteNonQuery();
                cmdExecute.Connection.Close();

                respuesta.codigo = 0;
                respuesta.detalle = "Propiedad detalle publicación actualizado con éxito.";
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


    public ClientePropiedadMedio GetClientePropiedadMedio(ClientePropiedadMedio medio)
    {
        ClientePropiedadMedio item = new ClientePropiedadMedio();

        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd.CommandText = "SEL_CLIENTE_PROPIEDAD_MEDIO";
            if (medio.cpm_id > 0) cmd.Parameters.AddWithValue("@ID", medio.cpm_id);

            using (SqlDataReader dr = Conexion.GetDataReader(cmd))
            {
                if (dr.Read())
                {
                    item.cpm_id = int.Parse(dr["CPM_ID"].ToString());
                    item.cpm_id_propiedad = int.Parse(dr["CPM_ID_PROPIEDAD"].ToString());
                    item.cpm_id_archivo = dr["CPM_ID_ARCHIVO"] != DBNull.Value ? Convert.ToInt32(dr["CPM_ID_ARCHIVO"]) : 0;
                    item.cpm_link = dr["CPM_LINK"].ToString();
                    item.cpm_descripcion = dr["CPM_DESCRIPCION"].ToString();
                    item.cpm_imagen = Boolean.Parse(dr["CPM_IMAGEN"].ToString());
                    item.cpm_video = Boolean.Parse(dr["CPM_VIDEO"].ToString());
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

        return item;
    }
    public List<ClientePropiedadMedio> GetClientePropiedadMedios(ClientePropiedadMedio filtro)
    {
        List<ClientePropiedadMedio> listado = new List<ClientePropiedadMedio>();

        if (Token.TokenSeguridad())
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = "SEL_CLIENTE_PROPIEDAD_MEDIO";
                if (filtro.cpm_id_propiedad > 0) cmd.Parameters.AddWithValue("@PROPIEDAD", filtro.cpm_id_propiedad);

                using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                {
                    while (dr.Read())
                    {
                        ClientePropiedadMedio item = new ClientePropiedadMedio();

                        item.cpm_id = int.Parse(dr["CPM_ID"].ToString());
                        item.cpm_id_propiedad = int.Parse(dr["CPM_ID_PROPIEDAD"].ToString());
                        item.cpm_id_archivo = dr["CPM_ID_ARCHIVO"] != DBNull.Value ? Convert.ToInt32(dr["CPM_ID_ARCHIVO"]) : 0;
                        item.cpm_link = dr["CPM_LINK"].ToString();
                        item.cpm_descripcion = dr["CPM_DESCRIPCION"].ToString();
                        item.cpm_imagen = Boolean.Parse(dr["CPM_IMAGEN"].ToString());
                        item.cpm_video = Boolean.Parse(dr["CPM_VIDEO"].ToString());

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

    public Respuesta InsertClientePropiedadImagen(ClientePropiedadMedio item, ClientePropiedadMedioBinario binario = null)
    {
        Respuesta respuesta = new Respuesta();

        if (Token.TokenSeguridad())
        {
            SqlCommand cmd = null;

            try
            {
                cmd = Conexion.GetCommand("INS_CLIENTE_PROPIEDAD_MEDIO");

                cmd.Parameters.AddWithValue("@ID", item.cpm_id);
                cmd.Parameters.AddWithValue("@PROPIEDAD", item.cpm_id_propiedad);
                cmd.Parameters.AddWithValue("@LINK", item.cpm_link);
                cmd.Parameters.AddWithValue("@DESCRIPCION", item.cpm_descripcion);
                cmd.Parameters.AddWithValue("@IMAGEN", item.cpm_imagen);
                cmd.Parameters.AddWithValue("@VIDEO", item.cpm_video);
                cmd.Parameters.AddWithValue("@BINARIO", binario.cmb_binario);


                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();

                respuesta.detalle = "Imagen / Video de la propiedad agregado con éxito.";
            }
            catch (Exception ex)
            {
                if (cmd != null)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();
                }

                respuesta.detalle = ex.Message;
                respuesta.error = true;
            }
        }

        return respuesta;
    }

    public Respuesta InsertClientePropiedadVideo(ClientePropiedadMedio item, ClientePropiedadMedioBinario binario = null)
    {
        Respuesta respuesta = new Respuesta();

        if (Token.TokenSeguridad())
        {
            SqlCommand cmd = null;

            try
            {
                cmd = Conexion.GetCommand("INS_CLIENTE_PROPIEDAD_MEDIO");

                cmd.Parameters.AddWithValue("@ID", item.cpm_id);
                cmd.Parameters.AddWithValue("@PROPIEDAD", item.cpm_id_propiedad);
                cmd.Parameters.AddWithValue("@LINK", item.cpm_link);
                cmd.Parameters.AddWithValue("@DESCRIPCION", item.cpm_descripcion);
                cmd.Parameters.AddWithValue("@IMAGEN", item.cpm_imagen);
                cmd.Parameters.AddWithValue("@VIDEO", item.cpm_video);

                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                cmd.Dispose();

                respuesta.detalle = "Imagen / Video de la propiedad agregado con éxito.";
            }
            catch (Exception ex)
            {
                if (cmd != null)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();
                }

                respuesta.detalle = ex.Message;
                respuesta.error = true;
            }
        }

        return respuesta;
    }


    public Respuesta UpdateClientePropiedadMedioImagen(ClientePropiedadMedio item, ClientePropiedadMedioBinario binario = null)
    {
        SqlCommand cmdExecute = new SqlCommand();

        Respuesta respuesta = new Respuesta();

        if (Token.TokenSeguridad())
        {
            try
            {
                cmdExecute = Conexion.GetCommand("UPD_CLIENTE_PROPIEDAD_MEDIO");
                cmdExecute.Parameters.AddWithValue("@ID", item.cpm_id);
                cmdExecute.Parameters.AddWithValue("@PROPIEDAD", item.cpm_id_propiedad);
                cmdExecute.Parameters.AddWithValue("@LINK", item.cpm_link);
                cmdExecute.Parameters.AddWithValue("@DESCRIPCION", item.cpm_descripcion);
                cmdExecute.Parameters.AddWithValue("@IMAGEN", item.cpm_imagen);
                cmdExecute.Parameters.AddWithValue("@VIDEO", item.cpm_video);
                cmdExecute.Parameters.AddWithValue("@BINARIO", binario.cmb_binario);


                cmdExecute.ExecuteNonQuery();
                cmdExecute.Connection.Close();

                respuesta.codigo = 0;
                respuesta.detalle = "Propiedad medio de publicación actualizado con éxito.";
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


    public Respuesta UpdateClientePropiedadMedioVideo(ClientePropiedadMedio item)
    {
        SqlCommand cmdExecute = new SqlCommand();

        Respuesta respuesta = new Respuesta();

        if (Token.TokenSeguridad())
        {
            try
            {
                cmdExecute = Conexion.GetCommand("UPD_CLIENTE_PROPIEDAD_MEDIO");
                cmdExecute.Parameters.AddWithValue("@ID", item.cpm_id);
                cmdExecute.Parameters.AddWithValue("@PROPIEDAD", item.cpm_id_propiedad);
                cmdExecute.Parameters.AddWithValue("@LINK", item.cpm_link);
                cmdExecute.Parameters.AddWithValue("@DESCRIPCION", item.cpm_descripcion);
                cmdExecute.Parameters.AddWithValue("@IMAGEN", item.cpm_imagen);
                cmdExecute.Parameters.AddWithValue("@VIDEO", item.cpm_video);

                cmdExecute.ExecuteNonQuery();
                cmdExecute.Connection.Close();

                respuesta.codigo = 0;
                respuesta.detalle = "Propiedad medio de publicación actualizado con éxito.";
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



    public List<ClientePropiedadMedioBinario> GetClientePropiedadMedioArchivos(ClientePropiedadMedio medio)
    {
        List<ClientePropiedadMedioBinario> binarios = new List<ClientePropiedadMedioBinario>(); // Lista para almacenar los resultados

        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd.CommandText = "SEL_CLIENTE_PROPIEDAD_MEDIO_BINARIO";
            if (medio.cpm_id > 0) cmd.Parameters.AddWithValue("@ID", medio.cpm_id);

            using (SqlDataReader dr = Conexion.GetDataReader(cmd))
            {
                while (dr.Read()) 
                {
                    ClientePropiedadMedioBinario item = new ClientePropiedadMedioBinario();

                    item.cmb_id = int.Parse(dr["CMB_ID"].ToString());

                    if (!string.IsNullOrEmpty(dr["CMB_BINARIO"].ToString())) item.cmb_binario = (byte[])dr["CMB_BINARIO"];
                    if (!string.IsNullOrEmpty(dr["DESCRIPCION"].ToString())) item.DESCRIPCION = dr["DESCRIPCION"].ToString();
                   
                    // Añadimos el objeto a la lista
                    binarios.Add(item);
                }
            }

            cmd.Connection.Close();
            cmd.Dispose();
        }
        catch (Exception ex)
        {
            cmd.Connection.Close();
            cmd.Dispose();

            binarios = null; // En caso de error, retornamos null
        }

        return binarios; // Devolvemos la lista
    }

    public ClientePropiedadMedioBinario GetClientePropiedadMedioArchivo(ClientePropiedadMedio medio)
    {
        ClientePropiedadMedioBinario item = new ClientePropiedadMedioBinario();

        SqlCommand cmd = new SqlCommand();

        try
        {
            cmd.CommandText = "SEL_CLIENTE_PROPIEDAD_MEDIO_BINARIO";
            if (medio.cpm_id > 0) cmd.Parameters.AddWithValue("@ID", medio.cpm_id);

            using (SqlDataReader dr = Conexion.GetDataReader(cmd))
            {
                if (dr.Read())
                {
                    item.cmb_id = int.Parse(dr["CMB_ID"].ToString());
                    if (!string.IsNullOrEmpty(dr["CMB_BINARIO"].ToString())) item.cmb_binario = (byte[])dr["CMB_BINARIO"];
                    if (!string.IsNullOrEmpty(dr["DESCRIPCION"].ToString())) item.DESCRIPCION = dr["DESCRIPCION"].ToString();
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

        return item;
    }

    public Respuesta DeleteClientePropiedadMedio(ClientePropiedadMedio item)
    {
        Respuesta respuesta = new Respuesta();

        if (Token.TokenSeguridad())
        {
            SqlCommand cmd = null;

            try
            {
                cmd = Conexion.GetCommand("DEL_CLIENTE_PROPIEDAD_MEDIO");
                cmd.Parameters.AddWithValue("@ID", item.cpm_id);

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