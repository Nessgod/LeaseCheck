using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;

namespace LeaseCheck.Root.Controller
{
    public class ClienteDocumentoController
    {
        public List<ClienteDocumento> GetClienteDocumentos(ClienteDocumento clienteDocumento)
        {
            List<ClienteDocumento> clienteDocumentos = new List<ClienteDocumento>();
            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_CLIENTE_DOCUMENTOS";
                    if (clienteDocumento.abi_id > 0) cmd.Parameters.AddWithValue("@ID_ARCHIVO_BINARIO", clienteDocumento.abi_id);
                    if (clienteDocumento.arc_id > 0) cmd.Parameters.AddWithValue("@ID_ARCHIVO", clienteDocumento.arc_id);
                    if (clienteDocumento.cdo_id > 0) cmd.Parameters.AddWithValue("@ID_CLIENTE_DOCUMENTO", clienteDocumento.cdo_id);
                    if (clienteDocumento.cdo_id_cliente > 0) cmd.Parameters.AddWithValue("@ID_CLIENTE", clienteDocumento.cdo_id_cliente);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            ClienteDocumento item = new ClienteDocumento();

                            if (dr["CDO_ID"].ToString() != "") item.cdo_id = int.Parse(dr["CDO_ID"].ToString());
                            if (dr["CDO_ID_CLIENTE"].ToString() != "") item.cdo_id_cliente = int.Parse(dr["CDO_ID_CLIENTE"].ToString());
                            if (dr["CDO_DOCUMENTO"].ToString() != "") item.cdo_documento = int.Parse(dr["CDO_DOCUMENTO"].ToString());
                            item.cdo_descripcion = dr["CDO_DESCRIPCION"].ToString();
                            if (dr["CDO_USUARIO_CREACION"].ToString() != "") item.cdo_usuario_creacion = int.Parse(dr["CDO_USUARIO_CREACION"].ToString());
                            item.cdo_fecha_creacion = DateTime.Parse(dr["CDO_FECHA_CREACION"].ToString());

                            if (dr["ARC_ID"].ToString() != "") item.arc_id = int.Parse(dr["ARC_ID"].ToString());
                            item.arc_nombre_archivo = dr["ARC_NOMBRE_ARCHIVO"].ToString();
                            item.arc_contenido = dr["ARC_CONTENIDO"].ToString();
                            item.arc_extension = dr["ARC_EXTENSION"].ToString();
                            if (dr["ARC_TAMANO"].ToString() != "") item.arc_tamano = int.Parse(dr["ARC_TAMANO"].ToString());
                            if (dr["ARC_ARCHIVO"].ToString() != "") item.arc_archivo = int.Parse(dr["ARC_ARCHIVO"].ToString());
                            item.nombre_usuario_creacion = dr["nombre_usuario_creacion"].ToString();

                            clienteDocumentos.Add(item);
                        }
                    }
                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    clienteDocumento = null;
                }
            }

            return clienteDocumentos;
        }

        public ClienteDocumento GetClienteDocumento(ClienteDocumento clienteDocumento)
        {
            ClienteDocumento item = new ClienteDocumento();

            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = "SEL_CLIENTE_DOCUMENTOS_BINARIO";
                if (clienteDocumento.cdo_id > 0) cmd.Parameters.AddWithValue("@ID", clienteDocumento.cdo_id);

                using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                {
                    if (dr.Read())
                    {
                        item.abi_archivo_binario = (byte[])dr["abi_archivo_binario"];

                        if (dr["ARC_ID"].ToString() != "") item.arc_id = int.Parse(dr["ARC_ID"].ToString());
                        item.arc_nombre_archivo = dr["ARC_NOMBRE_ARCHIVO"].ToString();
                        item.arc_contenido = dr["ARC_CONTENIDO"].ToString();
                        item.arc_extension = dr["ARC_EXTENSION"].ToString();
                        if (dr["ARC_TAMANO"].ToString() != "") item.arc_tamano = int.Parse(dr["ARC_TAMANO"].ToString());
                        if (dr["ARC_ARCHIVO"].ToString() != "") item.arc_archivo = int.Parse(dr["ARC_ARCHIVO"].ToString());

                        if (dr["CDO_ID"].ToString() != "") item.cdo_id = int.Parse(dr["CDO_ID"].ToString());
                        if (dr["CDO_ID_CLIENTE"].ToString() != "") item.cdo_id_cliente = int.Parse(dr["CDO_ID_CLIENTE"].ToString());
                        if (dr["CDO_DOCUMENTO"].ToString() != "") item.cdo_documento = int.Parse(dr["CDO_DOCUMENTO"].ToString());
                        item.cdo_descripcion = dr["CDO_DESCRIPCION"].ToString();
                        if (dr["CDO_USUARIO_CREACION"].ToString() != "") item.cdo_usuario_creacion = int.Parse(dr["CDO_USUARIO_CREACION"].ToString());
                        item.cdo_fecha_creacion = DateTime.Parse(dr["CDO_FECHA_CREACION"].ToString());
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

        public Respuesta InsertDocumentos(ClienteDocumento clienteDocumento)
        {
            Respuesta respuesta = new Respuesta();

            SqlCommand cmdExecute = null;

            try
            {
                int id = 0;

                cmdExecute = Conexion.GetCommand("INS_CLIENTE_DOCUMENTO");

                cmdExecute.Parameters.AddWithValue("@ID", id).Direction = System.Data.ParameterDirection.Output;
                cmdExecute.Parameters.AddWithValue("@NOMBRE_ARCHIVO", clienteDocumento.arc_nombre_archivo);
                cmdExecute.Parameters.AddWithValue("@CONTENIDO", clienteDocumento.arc_contenido);
                cmdExecute.Parameters.AddWithValue("@EXTENSION", clienteDocumento.arc_extension);
                cmdExecute.Parameters.AddWithValue("@TAMANO", clienteDocumento.arc_tamano);
                cmdExecute.Parameters.AddWithValue("@ID_CLIENTE", clienteDocumento.cdo_id_cliente);
                cmdExecute.Parameters.AddWithValue("@ARCHIVO_BINARIO", clienteDocumento.abi_archivo_binario);
                cmdExecute.Parameters.AddWithValue("@DESCRIPCION", clienteDocumento.cdo_descripcion);
                cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());

                cmdExecute.ExecuteNonQuery();
                cmdExecute.Connection.Close();

                if (cmdExecute.Parameters["@ID"].Value.ToString() != "")
                    id = (int)cmdExecute.Parameters["@ID"].Value;

                respuesta.codigo = id;
                respuesta.detalle = "Documento guardado con éxito.";
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

        public Respuesta DeleteDocumentos(ClienteDocumento clienteDocumentos)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = new SqlCommand();
                try
                {
                    cmdExecute = Conexion.GetCommand("DEL_CLIENTE_DOCUMENTOS");
                    cmdExecute.Parameters.AddWithValue("@ID", clienteDocumentos.cdo_id);

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    respuesta.codigo = 0;
                    respuesta.detalle = "Documento eliminado con éxito.";
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
    }
}