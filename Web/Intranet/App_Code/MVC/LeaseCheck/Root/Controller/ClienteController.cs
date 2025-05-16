using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;

namespace LeaseCheck.Root.Controller
{
    public class ClienteController
    {
        public List<Cliente> GetClientesCBO()
        {
            List<Cliente> listado = new List<Cliente>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_CLIENTE";
                    cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Cliente item = new Cliente();

                            item.cli_id = int.Parse(dr["CLI_ID"].ToString());
                            item.cli_nombre = dr["CLI_NOMBRE"].ToString();

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

        public List<Cliente> GetClientes(Cliente filtro)
        {
            List<Cliente> listado = new List<Cliente>();
            Cliente cliente = new Cliente();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_CLIENTE";
                    cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    if (filtro.filtro != "") cmd.Parameters.AddWithValue("@FILTRO", filtro.filtro);
                    if (filtro.filtro_Cliente != "") cmd.Parameters.AddWithValue("@Cliente", filtro.filtro_Cliente);
                    if (filtro.filtro_Pais != "") cmd.Parameters.AddWithValue("@PAIS", filtro.filtro_Pais);



                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Cliente item = new Cliente();

                            item.cli_id = int.Parse(dr["CLI_ID"].ToString());
                            item.cli_nombre = dr["CLI_NOMBRE"].ToString();
                            item.cli_giro = dr["CLI_GIRO"].ToString();
                            item.cli_rut = int.Parse(dr["CLI_RUT"].ToString());
                            item.cli_dv = item.cli_rut.ToString() + "-" + dr["CLI_DV"].ToString();
                            item.cli_habilitado = bool.Parse(dr["CLI_HABILITADO"].ToString());
                            item.cli_alias = dr["CLI_ALIAS"].ToString();
                            item.cli_pais = int.Parse(dr["CLI_PAIS"].ToString());
                            item.cli_comuna = int.Parse(dr["CLI_COMUNA"].ToString());
                            item.cli_direccion = dr["CLI_DIRECCION"].ToString();
                            item.cli_email = dr["CLI_EMAIL"].ToString();
                            item.cli_telefono = dr["CLI_TELEFONO"].ToString();
                            item.cli_es_demo = Boolean.Parse(dr["CLI_ES_DEMO"].ToString());
                            item.cli_contacto_nombre = dr["CLI_CONTACTO_NOMBRE"].ToString();
                            item.cli_contacto_email = dr["CLI_CONTACTO_EMAIL"].ToString();
                            item.cli_contacto_telefono = dr["CLI_CONTACTO_TELEFONO"].ToString();
                            item.cli_rut_completo = dr["CLI_RUT_COMPLETO"].ToString();

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

        public List<Cliente> GetClientesUsuario()
        {
            List<Cliente> listado = new List<Cliente>();
            Cliente cliente = new Cliente();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_CLIENTE";
                    cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());


                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Cliente item = new Cliente();

                            item.cli_id = int.Parse(dr["CLI_ID"].ToString());
                            item.cli_nombre = dr["CLI_NOMBRE"].ToString();
                            item.cli_giro = dr["CLI_GIRO"].ToString();
                            item.cli_rut = int.Parse(dr["CLI_RUT"].ToString());
                            item.cli_dv = item.cli_rut.ToString() + "-" + dr["CLI_DV"].ToString();
                            item.cli_habilitado = bool.Parse(dr["CLI_HABILITADO"].ToString());
                            item.cli_alias = dr["CLI_ALIAS"].ToString();
                            item.cli_pais = int.Parse(dr["CLI_PAIS"].ToString());
                            item.cli_comuna = int.Parse(dr["CLI_COMUNA"].ToString());
                            item.cli_direccion = dr["CLI_DIRECCION"].ToString();
                            item.cli_email = dr["CLI_EMAIL"].ToString();
                            item.cli_telefono = dr["CLI_TELEFONO"].ToString();
                            item.cli_es_demo = Boolean.Parse(dr["CLI_ES_DEMO"].ToString());
                            item.cli_contacto_nombre = dr["CLI_CONTACTO_NOMBRE"].ToString();
                            item.cli_contacto_email = dr["CLI_CONTACTO_EMAIL"].ToString();
                            item.cli_contacto_telefono = dr["CLI_CONTACTO_TELEFONO"].ToString();

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

     
        public Cliente GetCliente(Cliente filtro)
        {
            Cliente item = new Cliente();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_CLIENTE";
                    cmd.Parameters.AddWithValue("@ID", filtro.cli_id);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            item.cli_id = int.Parse(dr["CLI_ID"].ToString());
                            item.cli_nombre = dr["CLI_NOMBRE"].ToString();
                            item.cli_giro = dr["CLI_GIRO"].ToString();
                            item.cli_rut = int.Parse(dr["CLI_RUT"].ToString());
                            item.cli_dv = dr["CLI_DV"].ToString();
                            item.cli_habilitado = bool.Parse(dr["CLI_HABILITADO"].ToString());
                            item.cli_alias = dr["CLI_ALIAS"].ToString();
                            item.cli_pais = int.Parse(dr["CLI_PAIS"].ToString());
                            item.cli_comuna = int.Parse(dr["CLI_COMUNA"].ToString());
                            item.cli_direccion = dr["CLI_DIRECCION"].ToString();
                            item.cli_email = dr["CLI_EMAIL"].ToString();
                            item.cli_telefono = dr["CLI_TELEFONO"].ToString();
                            item.cli_es_demo = Boolean.Parse(dr["CLI_ES_DEMO"].ToString());
                            item.cli_contacto_nombre = dr["CLI_CONTACTO_NOMBRE"].ToString();
                            item.cli_contacto_email = dr["CLI_CONTACTO_EMAIL"].ToString();
                            item.cli_contacto_telefono = dr["CLI_CONTACTO_TELEFONO"].ToString();
                            item.cli_rut_completo = dr["CLI_RUT_COMPLETO"].ToString();
                            item.cli_tiene_rut = bool.Parse(dr["CLI_TIENE_RUT"].ToString());
                            item.cli_identificador = dr["CLI_IDENTIFICADOR"].ToString();
                            if (dr["CLI_LOGO"] != System.DBNull.Value)
                            {
                                item.cli_logo = (byte[])dr["CLI_LOGO"];
                            }
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

        public Cliente GetClienteIdentidad()
        {
            Cliente item = new Cliente();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_CLIENTE";
                    cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                   
                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        if (dr.Read())
                        {
                            item.cli_id = int.Parse(dr["CLI_ID"].ToString());
                            item.cli_nombre = dr["CLI_NOMBRE"].ToString();
                            item.cli_giro = dr["CLI_GIRO"].ToString();
                            item.cli_rut = int.Parse(dr["CLI_RUT"].ToString());
                            item.cli_dv = dr["CLI_DV"].ToString();
                            item.cli_habilitado = bool.Parse(dr["CLI_HABILITADO"].ToString());
                            item.cli_alias = dr["CLI_ALIAS"].ToString();
                            item.cli_pais = int.Parse(dr["CLI_PAIS"].ToString());
                            item.cli_comuna = int.Parse(dr["CLI_COMUNA"].ToString());
                            item.cli_direccion = dr["CLI_DIRECCION"].ToString();
                            item.cli_email = dr["CLI_EMAIL"].ToString();
                            item.cli_telefono = dr["CLI_TELEFONO"].ToString();
                            item.cli_es_demo = Boolean.Parse(dr["CLI_ES_DEMO"].ToString());
                            item.cli_contacto_nombre = dr["CLI_CONTACTO_NOMBRE"].ToString();
                            item.cli_contacto_email = dr["CLI_CONTACTO_EMAIL"].ToString();
                            item.cli_contacto_telefono = dr["CLI_CONTACTO_TELEFONO"].ToString();
                            float comision;
                            if (float.TryParse(dr["CLI_COMISION_VENTA"].ToString(), out comision))
                            {
                                item.cli_comision_venta = comision;
                            }
                            else
                            {
                                item.cli_comision_venta = 0f; // o el valor por defecto que necesites
                            }


                            if (dr["CLI_LOGO"] != System.DBNull.Value)
                            {
                                item.cli_logo = (byte[])dr["CLI_LOGO"];
                            }
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


        public Respuesta InsertCliente(Cliente item)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = null;

                try
                {
                    int id = 0;
                    cmd = Conexion.GetCommand("INS_CLIENTE");
                    cmd.Parameters.AddWithValue("@ID", id).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@NOMBRE", item.cli_nombre);
                    cmd.Parameters.AddWithValue("@GIRO", item.cli_giro);
                    cmd.Parameters.AddWithValue("@HABILITADO", item.cli_habilitado);
                    cmd.Parameters.AddWithValue("@ALIAS", item.cli_alias);
                    cmd.Parameters.AddWithValue("@PAIS", item.cli_pais);
                    cmd.Parameters.AddWithValue("@COMUNA", item.cli_comuna);
                    cmd.Parameters.AddWithValue("@DIRECCION", item.cli_direccion);
                    cmd.Parameters.AddWithValue("@EMAIL", item.cli_email);
                    cmd.Parameters.AddWithValue("@TELEFONO", item.cli_telefono);
                    cmd.Parameters.AddWithValue("@LOGO", item.cli_logo);
                    cmd.Parameters.AddWithValue("@ES_DEMO", item.cli_es_demo);
                    cmd.Parameters.AddWithValue("@CONTACTO_NOMBRE", item.cli_contacto_nombre);
                    cmd.Parameters.AddWithValue("@CONTACTO_EMAIL", item.cli_contacto_email);
                    cmd.Parameters.AddWithValue("@CONTACTO_TELEFONO", item.cli_contacto_telefono);

                    cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());

                    if (item.cli_tiene_rut)
                    {
                        cmd.Parameters.AddWithValue("@RUT", item.cli_rut);
                        cmd.Parameters.AddWithValue("@DV", item.cli_dv);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RUT", 0);
                        cmd.Parameters.AddWithValue("@DV", "");
                        cmd.Parameters.AddWithValue("@IDENTIFICADOR", item.cli_identificador);
                    }

                    // Verificar si el usuario tiene el perfil AdministradorCorredora
                    string[] perfiles = Session.UsuarioPerfil().Split(',');
                    if (perfiles.Contains(Convert.ToInt32(LeaseCheck.Perfiles.AdministradorCorredora).ToString()))
                    {
                        // Insertar valor en cli_comision_venta si el usuario es AdministradorCorredora
                        cmd.Parameters.AddWithValue("@COMISION_VENTA", item.cli_comision_venta);
                    }


                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    cmd.Dispose();

                    id = (int)cmd.Parameters["@ID"].Value;

                    respuesta.codigo = id;
                    respuesta.detalle = "Registro creado con éxito.";
                    respuesta.error = false;
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    respuesta.codigo = -1;
                    respuesta.detalle = ex.Message;
                    respuesta.error = true;
                }
            }

            return respuesta;
        }

        public Respuesta UpdateCliente(Cliente item)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = null;

                try
                {
                    cmd = Conexion.GetCommand("UPD_CLIENTE");

                    cmd.Parameters.AddWithValue("@ID", item.cli_id);
                    cmd.Parameters.AddWithValue("@NOMBRE", item.cli_nombre);
                    cmd.Parameters.AddWithValue("@GIRO", item.cli_giro);
                    cmd.Parameters.AddWithValue("@HABILITADO", item.cli_habilitado);
                    cmd.Parameters.AddWithValue("@ALIAS", item.cli_alias);
                    cmd.Parameters.AddWithValue("@PAIS", item.cli_pais);
                    cmd.Parameters.AddWithValue("@COMUNA", item.cli_comuna);
                    cmd.Parameters.AddWithValue("@DIRECCION", item.cli_direccion);
                    cmd.Parameters.AddWithValue("@EMAIL", item.cli_email);
                    cmd.Parameters.AddWithValue("@TELEFONO", item.cli_telefono);
                    cmd.Parameters.AddWithValue("@LOGO", item.cli_logo);
                    cmd.Parameters.AddWithValue("@ES_DEMO", item.cli_es_demo);
                    cmd.Parameters.AddWithValue("@CONTACTO_NOMBRE", item.cli_contacto_nombre);
                    cmd.Parameters.AddWithValue("@CONTACTO_EMAIL", item.cli_contacto_email);
                    cmd.Parameters.AddWithValue("@CONTACTO_TELEFONO", item.cli_contacto_telefono);

                    cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());

                    if (item.cli_tiene_rut)
                    {
                        cmd.Parameters.AddWithValue("@RUT", item.cli_rut);
                        cmd.Parameters.AddWithValue("@DV", item.cli_dv);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@RUT", 0);
                        cmd.Parameters.AddWithValue("@DV", "");
                        cmd.Parameters.AddWithValue("@IDENTIFICADOR", item.cli_identificador);
                    }

                    // Verificar si el usuario tiene el perfil AdministradorCorredora
                    string[] perfiles = Session.UsuarioPerfil().Split(',');
                    if (perfiles.Contains(Convert.ToInt32(LeaseCheck.Perfiles.AdministradorCorredora).ToString()))
                    {
                        // Insertar valor en cli_comision_venta si el usuario es AdministradorCorredora
                        cmd.Parameters.AddWithValue("@COMISION_VENTA", item.cli_comision_venta);
                    }


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


        public Respuesta UpdateClienteComision(Cliente item)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = null;

                try
                {
                    cmd = Conexion.GetCommand("UPD_CLIENTE_COMISION");

                    cmd.Parameters.AddWithValue("@ID", item.cli_id);
                    cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    cmd.Parameters.AddWithValue("@RUT",item.cli_rut);
                    cmd.Parameters.AddWithValue("@DV", item.cli_dv);
                    cmd.Parameters.AddWithValue("@PAIS", Session.Pais());
                    cmd.Parameters.AddWithValue("@NOMBRE", item.cli_nombre);
                    cmd.Parameters.AddWithValue("@GIRO", item.cli_giro);
                    cmd.Parameters.AddWithValue("@ALIAS", item.cli_alias);
                    cmd.Parameters.AddWithValue("@COMUNA", item.cli_comuna);
                    cmd.Parameters.AddWithValue("@DIRECCION", item.cli_direccion);
                    cmd.Parameters.AddWithValue("@EMAIL", item.cli_email);
                    cmd.Parameters.AddWithValue("@TELEFONO", item.cli_telefono);
                    cmd.Parameters.AddWithValue("@LOGO", item.cli_logo);
                    cmd.Parameters.AddWithValue("@CONTACTO_NOMBRE", item.cli_contacto_nombre);
                    cmd.Parameters.AddWithValue("@CONTACTO_EMAIL", item.cli_contacto_email);
                    cmd.Parameters.AddWithValue("@CONTACTO_TELEFONO", item.cli_contacto_telefono);
                    cmd.Parameters.AddWithValue("@COMISION",item.cli_comision_venta);

                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    cmd.Dispose();

                    respuesta.detalle = "Cliente actualizado con éxito.";
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

        public Respuesta DeleteCliente(Cliente item)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = null;

                try
                {
                    cmd = Conexion.GetCommand("DEL_CLIENTE");
                    cmd.Parameters.AddWithValue("@ID", item.cli_id);

                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    cmd.Dispose();

                    respuesta.detalle = "Registro eliminado con éxito.";
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    respuesta.detalle = "No es posible eliminar al cliente debido a que esta siendo utilizado en otro proceso.";
                    respuesta.error = true;
                }
            }

            return respuesta;
        }

        public void InformeCliente(Cliente fitro)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "RPT_CLIENTE";
            if (fitro.filtro != "") cmd.Parameters.AddWithValue("@FILTRO", fitro.filtro);
            if (fitro.filtro_Cliente != "") cmd.Parameters.AddWithValue("@Cliente", fitro.filtro_Cliente);

            string filename = " INFORME CLIENTES " + DateTime.Now;
            Tools.Excel.exportExcel(Conexion.GetDataTable(cmd), filename, true);
        }

        // cliente plan

        public List<ClientePlan> GetClientePlanes(ClientePlan filtro)
        {
            List<ClientePlan> listado = new List<ClientePlan>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_CLIENTE_PLAN";
                    cmd.Parameters.AddWithValue("@CLIENTE", filtro.clp_cliente);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            ClientePlan item = new ClientePlan();

                            item.clp_id = int.Parse(dr["CLP_ID"].ToString());
                            item.clp_cliente = int.Parse(dr["CLP_CLIENTE"].ToString());
                            item.clp_tipo_plan = int.Parse(dr["CLP_TIPO_PLAN"].ToString());
                            item.clp_fecha_desde = DateTime.Parse(dr["CLP_FECHA_DESDE"].ToString());
                            item.clp_fecha_hasta = DateTime.Parse(dr["CLP_FECHA_HASTA"].ToString());


                            item.plan_nombre = dr["PLAN_NOMBRE"].ToString();
                            item.plan_documento = int.Parse(dr["PLAN_DOCUMENTO"].ToString());
                            item.plan_propiedad = int.Parse(dr["PLAN_PROPIEDAD"].ToString());
                            item.plan_lead = int.Parse(dr["PLAN_LEAD"].ToString());
                            item.tipo_dato = dr["TIPO"].ToString();
                            item.estado = dr["ESTADO"].ToString();
                            item.valor_plan = int.Parse(dr["VALOR_PLAN"].ToString());

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

        public List<ClientePlan> GetClientePlanesIdentidad()
        {
            List<ClientePlan> listado = new List<ClientePlan>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_CLIENTE_PLAN";
                    cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            ClientePlan item = new ClientePlan();

                            item.clp_id = int.Parse(dr["CLP_ID"].ToString());
                            item.clp_cliente = int.Parse(dr["CLP_CLIENTE"].ToString());
                            item.clp_tipo_plan = int.Parse(dr["CLP_TIPO_PLAN"].ToString());
                            item.clp_fecha_desde = DateTime.Parse(dr["CLP_FECHA_DESDE"].ToString());
                            item.clp_fecha_hasta = DateTime.Parse(dr["CLP_FECHA_HASTA"].ToString());

                            item.plan_nombre = dr["PLAN_NOMBRE"].ToString();
                            item.plan_documento = int.Parse(dr["PLAN_DOCUMENTO"].ToString());
                            item.plan_propiedad = int.Parse(dr["PLAN_PROPIEDAD"].ToString());
                            item.plan_lead = int.Parse(dr["PLAN_LEAD"].ToString());
                            item.valor_plan = int.Parse(dr["VALOR_PLAN"].ToString());

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

        public ClientePlan GetClientePlan(ClientePlan filtro)
        {
            ClientePlan item = new ClientePlan();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_CLIENTE_PLAN";
                    cmd.Parameters.AddWithValue("@ID", filtro.clp_id);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            item.clp_id = int.Parse(dr["CLP_ID"].ToString());
                            item.clp_cliente = int.Parse(dr["CLP_CLIENTE"].ToString());
                            item.clp_tipo_plan = int.Parse(dr["CLP_TIPO_PLAN"].ToString());
                            item.clp_fecha_desde = DateTime.Parse(dr["CLP_FECHA_DESDE"].ToString());
                            item.clp_fecha_hasta = DateTime.Parse(dr["CLP_FECHA_HASTA"].ToString());
                            item.plan_documento = int.Parse(dr["PLAN_DOCUMENTO"].ToString());
                            item.plan_propiedad = int.Parse(dr["PLAN_PROPIEDAD"].ToString());
                            item.plan_lead = int.Parse(dr["PLAN_LEAD"].ToString());
                            item.valor_plan = int.Parse(dr["VALOR_PLAN"].ToString());

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

        public Respuesta InsertClientePlan(ClientePlan item)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = null;

                try
                {
                    int id = 0;
                    cmd = Conexion.GetCommand("INS_CLIENTE_PLAN");
                    cmd.Parameters.AddWithValue("@ID", id).Direction = System.Data.ParameterDirection.Output;
                    cmd.Parameters.AddWithValue("@CLIENTE", item.clp_cliente);
                    cmd.Parameters.AddWithValue("@TIPO_PLAN", item.clp_tipo_plan);
                    cmd.Parameters.AddWithValue("@FECHA_DESDE", item.clp_fecha_desde);
                    cmd.Parameters.AddWithValue("@FECHA_HASTA", item.clp_fecha_hasta);
                    cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    cmd.Parameters.AddWithValue("@PAIS", Session.Pais());

                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    cmd.Dispose();

                    id = (int)cmd.Parameters["@ID"].Value;
                    respuesta.codigo = id;
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

        public Respuesta UpdateClientePlan(ClientePlan item)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = null;

                try
                {
                    cmd = Conexion.GetCommand("UPD_CLIENTE_PLAN");

                    cmd.Parameters.AddWithValue("@ID", item.clp_id);
                    cmd.Parameters.AddWithValue("@TIPO_PLAN", item.clp_tipo_plan);
                    cmd.Parameters.AddWithValue("@FECHA_DESDE", item.clp_fecha_desde);
                    cmd.Parameters.AddWithValue("@FECHA_HASTA", item.clp_fecha_hasta);
                    cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    cmd.Parameters.AddWithValue("@PAIS", Session.Pais());

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

        public Respuesta DeleteClientePlan(ClientePlan item)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = null;

                try
                {
                    cmd = Conexion.GetCommand("DEL_CLIENTE_PLAN");
                    cmd.Parameters.AddWithValue("@ID", item.clp_id);

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

        public List<PlanVencimiento> GetClientePlanesVencimiento(PlanVencimiento item)
        {
            List<PlanVencimiento> listado = new List<PlanVencimiento>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_PLANES_POR_VENCER";
                    cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    if (item.filtro != "") cmd.Parameters.AddWithValue("@FILTRO", item.filtro);
                    if (item.filtro_Cliente != "") cmd.Parameters.AddWithValue("@CLIENTE", item.filtro_Cliente);
                    if (item.filtro_Estado != null) cmd.Parameters.AddWithValue("@FILTRO_ESTADO", item.filtro_Estado);
                    if (item.desde != null) cmd.Parameters.AddWithValue("@DESDE", item.desde);
                    if (item.hasta != null) cmd.Parameters.AddWithValue("@HASTA", item.hasta);
                    if (item.filtro_Pais != "") cmd.Parameters.AddWithValue("@PAIS", item.filtro_Pais);


                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            PlanVencimiento itm = new PlanVencimiento();
                            itm.cliente = dr["CLI_NOMBRE"].ToString();
                            itm.telefono = dr["CLI_TELEFONO"].ToString();
                            itm.mail = dr["CLI_EMAIL"].ToString();
                            itm.contacto_nombre = dr["CLI_CONTACTO_NOMBRE"].ToString();
                            itm.contacto_telefono = dr["CLI_CONTACTO_TELEFONO"].ToString();
                            itm.contacto_mail = dr["CLI_CONTACTO_EMAIL"].ToString();
                            itm.habilitado = bool.Parse(dr["CLI_HABILITADO"].ToString());
                            itm.plan = dr["TPL_NOMBRE"].ToString();
                            itm.vigencia_desde = DateTime.Parse(dr["CLP_FECHA_DESDE"].ToString());
                            itm.vigencia_hasta = DateTime.Parse(dr["CLP_FECHA_HASTA"].ToString());

                            listado.Add(itm);
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

        public void InformePlan(PlanVencimiento item)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "RPT_PLAN_POR_VENCER";
            cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
            if (item.filtro != "") cmd.Parameters.AddWithValue("@FILTRO", item.filtro);
            if (item.filtro_Cliente != "") cmd.Parameters.AddWithValue("@CLIENTE", item.filtro_Cliente);
            if (item.filtro_Estado != null) cmd.Parameters.AddWithValue("@FILTRO_ESTADO", item.filtro_Estado);
            if (item.desde != null) cmd.Parameters.AddWithValue("@DESDE", item.desde);
            if (item.hasta != null) cmd.Parameters.AddWithValue("@HASTA", item.hasta);
            if (item.filtro_Pais != "") cmd.Parameters.AddWithValue("@PAIS", item.filtro_Pais);

            string filename = " INFORME PLAN POR VENCER " + DateTime.Now;
            Tools.Excel.exportExcel(Conexion.GetDataTable(cmd), filename, true);
        }

        //cbx
        public List<Paises> GetPaises()
        {
            List<Paises> paises = new List<Paises>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_PAISES";
                    cmd.Parameters.AddWithValue("@HABILITADO", true);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Paises item = new Paises();

                            item.pai_id = int.Parse(dr["PAI_ID"].ToString());
                            item.pai_nombre = dr["PAI_NOMBRE"].ToString();

                            paises.Add(item);
                        }
                    }

                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    paises = null;
                }
            }
            return paises;
        }

        public List<Comuna> GetComunas(Comuna filtro)
        {
            List<Comuna> listado = new List<Comuna>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_COMUNA";
                    if (filtro.cmn_pais > 0) cmd.Parameters.AddWithValue("@PAIS", filtro.cmn_pais);
                    if (filtro.cmn_region > 0) cmd.Parameters.AddWithValue("@REGION", filtro.cmn_region);
                    if (filtro.cmn_provincia > 0) cmd.Parameters.AddWithValue("@PROVINCIA", filtro.cmn_provincia);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Comuna item = new Comuna();

                            item.cmn_id = int.Parse(dr["CMN_ID"].ToString());
                            item.cmn_nombre = dr["CMN_NOMBRE"].ToString();

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

        public List<Region> GetRegiones(Region filtro)
        {
            List<Region> listado = new List<Region>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_REGION";
                    cmd.Parameters.AddWithValue("@PAIS", Session.Pais());

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Region item = new Region();

                            item.rgn_id = int.Parse(dr["RGN_ID"].ToString());
                            item.rgn_pais = int.Parse(dr["RGN_PAIS"].ToString());
                            item.rgn_nombre = dr["RGN_NOMBRE"].ToString();

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

        public List<Provincia> GetProvincias(Provincia filtro)
        {
            List<Provincia> listado = new List<Provincia>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_PROVINCIA";
                    if (filtro.pro_region > 0) cmd.Parameters.AddWithValue("@REGION", filtro.pro_region);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Provincia item = new Provincia();

                            item.pro_id = int.Parse(dr["PRO_ID"].ToString());
                            item.pro_region = int.Parse(dr["PRO_REGION"].ToString());
                            item.pro_nombre = dr["PRO_NOMBRE"].ToString();

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

        public List<Profesion> GetProfesion()
        {
            List<Profesion> listado = new List<Profesion>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_PROFESION";

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Profesion item = new Profesion();

                            item.prf_id = int.Parse(dr["PRF_ID"].ToString());
                            item.prf_nombre = dr["PRF_NOMBRE"].ToString();

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

        public List<Genero> GetGenero()
        {
            List<Genero> listado = new List<Genero>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_GENERO";

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Genero item = new Genero();

                            item.gro_id = int.Parse(dr["GRO_ID"].ToString());
                            item.gro_nombre = dr["GRO_NOMBRE"].ToString();

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

        public List<Banco> GetBanco()
        {
            List<Banco> listado = new List<Banco>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_BANCO";

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Banco item = new Banco();

                            item.bnc_id = int.Parse(dr["BNC_ID"].ToString());
                            item.bnc_nombre = dr["BNC_NOMBRE"].ToString();
                            item.bnc_tipo = dr["bnc_tipo"].ToString();

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

        public List<TipoCuentaBanco> GetCuentaBanco(TipoCuentaBanco filtro)
        {
            List<TipoCuentaBanco> listado = new List<TipoCuentaBanco>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_TIPO_CUENTA_BANCO";
                    if (filtro.tpc_banco > 0) cmd.Parameters.AddWithValue("@BANCO", filtro.tpc_banco);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            TipoCuentaBanco item = new TipoCuentaBanco();

                            item.tpc_id = int.Parse(dr["TPC_ID"].ToString());
                            item.tpc_nombre = dr["TPC_NOMBRE"].ToString();
                            item.tpc_banco = int.Parse(dr["TPC_BANCO"].ToString());

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

        public List<Nacionalidad> GetNacionalidad()
        {
            List<Nacionalidad> listado = new List<Nacionalidad>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_NACIONALIDAD";

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Nacionalidad item = new Nacionalidad();

                            item.nac_id = int.Parse(dr["NAC_ID"].ToString());
                            item.nac_nombre = dr["NAC_NOMBRE"].ToString();

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

        public List<EstadoCivil> GetEstadoCivil()
        {
            List<EstadoCivil> listado = new List<EstadoCivil>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_ESTADO_CIVIL";

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            EstadoCivil item = new EstadoCivil();

                            item.ecl_id = int.Parse(dr["ECL_ID"].ToString());
                            item.ecl_nombre = dr["ECL_NOMBRE"].ToString();

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
        public List<TipoPlan> GetTiposPlanes()
        {
            List<TipoPlan> listado = new List<TipoPlan>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_TIPO_PLAN";
                    cmd.Parameters.AddWithValue("@HABILITADO", true);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            TipoPlan item = new TipoPlan();

                            item.tpl_id = int.Parse(dr["TPL_ID"].ToString());
                            item.tpl_nombre = dr["TPL_NOMBRE"].ToString();
                            item.tpl_valor_plan = int.Parse(dr["TPL_VALOR_PLAN"].ToString());
                            item.tpl_cantidad_documento = int.Parse(dr["TPL_CANTIDAD_DOCUMENTO"].ToString());
                            item.tpl_cantidad_propiedad = int.Parse(dr["TPL_CANTIDAD_PROPIEDAD"].ToString());
                            item.tpl_cantidad_lead = int.Parse(dr["TPL_CANTIDAD_LEAD"].ToString());

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
        public List<ClientePropiedadTipoServicio> GetTipoServicio()
        {
            List<ClientePropiedadTipoServicio> servicios = new List<ClientePropiedadTipoServicio>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_CLIENTE_TIPO_SERVICIO";
                    cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            ClientePropiedadTipoServicio item = new ClientePropiedadTipoServicio();

                            item.tsc_id = int.Parse(dr["TSC_ID"].ToString());
                            item.tsc_nombre = dr["TSC_NOMBRE"].ToString();

                            servicios.Add(item);
                        }
                    }

                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    servicios = null;
                }
            }
            return servicios;
        }

        public List<ClientePropiedadTipo> GetTipoPropiedad()
        {
            List<ClientePropiedadTipo> tipoPropiedad = new List<ClientePropiedadTipo>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_CLIENTE_TIPO_PROPIEDAD";


                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            ClientePropiedadTipo item = new ClientePropiedadTipo();

                            item.tpr_id = int.Parse(dr["TPR_ID"].ToString());
                            item.tpr_nombre = dr["TPR_NOMBRE"].ToString();

                            tipoPropiedad.Add(item);
                        }
                    }

                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    tipoPropiedad = null;
                }
            }
            return tipoPropiedad;
        }
        public List<ClientePropiedadTipoEntrega> GetTipoEntrega()
        {
            List<ClientePropiedadTipoEntrega> entrega = new List<ClientePropiedadTipoEntrega>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_CLIENTE_TIPO_ENTREGA";


                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            ClientePropiedadTipoEntrega item = new ClientePropiedadTipoEntrega();

                            item.cpt_id = int.Parse(dr["CPT_ID"].ToString());
                            item.cpt_nombre = dr["CPT_NOMBRE"].ToString();

                            entrega.Add(item);
                        }
                    }

                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    entrega = null;
                }
            }
            return entrega;
        }
        public ClientePropiedad GetEstadoPropiedad(ClientePropiedad filtro)
        {
            ClientePropiedad estado = new ClientePropiedad();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_CLIENTE_PROPIEDAD_ESTADO";
                    cmd.Parameters.AddWithValue("@ID_PROPIEDAD", filtro.cpd_id);
                    cmd.Parameters.AddWithValue("@TIPO", 1);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            estado.ESTADO = dr["ESTADO"].ToString();

                        }
                    }
                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    estado = null;
                }
            }
            return estado;
        }

        public List<ClientePropiedadEstado> GetEstadosPropiedadNueva()
        {
            List<ClientePropiedadEstado> entrega = new List<ClientePropiedadEstado>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_CLIENTE_PROPIEDAD_ESTADO";
                    cmd.Parameters.AddWithValue("@TIPO", 2);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            ClientePropiedadEstado item = new ClientePropiedadEstado();

                            item.cpe_id = int.Parse(dr["CPE_ID"].ToString());
                            item.cpe_nombre = dr["CPE_NOMBRE"].ToString();

                            entrega.Add(item);
                        }
                    }

                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    entrega = null;
                }
            }
            return entrega;
        }

        public List<ClientePropiedadEstado> GetEstadosPropiedad(string estados)
        {
            List<ClientePropiedadEstado> entrega = new List<ClientePropiedadEstado>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_CLIENTE_PROPIEDAD_ESTADO";
                    cmd.Parameters.AddWithValue("@ESTADOS", estados);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            ClientePropiedadEstado item = new ClientePropiedadEstado();

                            item.cpe_id = int.Parse(dr["CPE_ID"].ToString());
                            item.cpe_nombre = dr["CPE_NOMBRE"].ToString();

                            entrega.Add(item);
                        }
                    }

                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    entrega = null;
                }
            }
            return entrega;
        }
        // usuarios

        public List<Usuarios> GetClienteUsuarioPropietarios()
        {
            List<Usuarios> servicios = new List<Usuarios>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_CLIENTE_USUARIO_PROPIETARIO";
                    cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Usuarios item = new Usuarios();

                            item.usu_id = int.Parse(dr["usu_id"].ToString());
                            item.NOMBRE_COMPLETO = dr["NOMBRE_COMPLETO"].ToString();

                            servicios.Add(item);
                        }
                    }

                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    servicios = null;
                }
            }
            return servicios;
        }

        public List<Usuarios> GetClienteUsuarios(Cliente filtro)
        {
            List<Usuarios> usuarios = new List<Usuarios>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "SEL_USUARIOS";
                    cmd.Parameters.AddWithValue("@ES_CLIENTE", true);
                    cmd.Parameters.AddWithValue("@CLIENTE", filtro.cli_id);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Usuarios usuario = new Usuarios();

                            usuario.usu_id = int.Parse(dr["USU_ID"].ToString());
                            usuario.usu_login = dr["USU_LOGIN"].ToString();
                            usuario.usu_password = dr["USU_PASSWORD"].ToString();
                            usuario.usu_nombres = dr["USU_NOMBRE"].ToString();
                            usuario.usu_apellido_paterno = dr["USU_APELLIDO_PATERNO"].ToString();
                            usuario.usu_apellido_materno = dr["USU_APELLIDO_MATERNO"].ToString();
                            usuario.nombreCompleto = usuario.usu_nombres + " " + usuario.usu_apellido_paterno + " " + usuario.usu_apellido_materno;
                            usuario.usu_correo = dr["USU_CORREO"].ToString();
                            usuario.usu_fono = dr["USU_FONO"].ToString();
                            usuario.usu_habilitado = bool.Parse(dr["USU_HABILITADO"].ToString());

                            usuario.es_cliente = bool.Parse(dr["USU_ES_CLIENTE"].ToString());
                            usuario.usu_perfil = int.Parse(dr["USU_PERFIL"].ToString());
                            usuario.usu_pais = int.Parse(dr["USU_PAIS"].ToString());
                            usuario.perfil_nombre = dr["PERFIL"].ToString();

                            usuarios.Add(usuario);
                        }
                    }


                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    usuarios = null;
                }
            }

            return usuarios;
        }

        public Usuarios GetClienteUsuario(Usuarios usuario)
        {
            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_USUARIOS";
                    cmd.Parameters.AddWithValue("@ID", usuario.usu_id);
                    cmd.Parameters.AddWithValue("@DEVUELVE_FOTO", true);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        if (dr.Read())
                        {
                            usuario = new Usuarios();

                            usuario.usu_id = int.Parse(dr["USU_ID"].ToString());
                            usuario.usu_login = dr["USU_LOGIN"].ToString();
                            usuario.usu_password = dr["USU_PASSWORD"].ToString();
                            usuario.usu_nombres = dr["USU_NOMBRE"].ToString();
                            usuario.usu_apellido_paterno = dr["USU_APELLIDO_PATERNO"].ToString();
                            usuario.usu_apellido_materno = dr["USU_APELLIDO_MATERNO"].ToString();
                            usuario.usu_correo = dr["USU_CORREO"].ToString();
                            usuario.usu_fono = dr["USU_FONO"].ToString();
                            usuario.usu_habilitado = bool.Parse(dr["USU_HABILITADO"].ToString());
                            usuario.usuario_creacion = dr["USUARIO_CREACION"].ToString();
                            usuario.usu_fecha_creacion = DateTime.Parse(dr["USU_FECHA_CREACION"].ToString());
                            usuario.usu_host_creacion = dr["USU_HOST_CREACION"].ToString();
                            usuario.usuario_act = dr["USUARIO_ACT"].ToString();
                            usuario.usu_fecha_act = DateTime.Parse(dr["USU_FECHA_ACT"].ToString());
                            usuario.usu_host_act = dr["USU_HOST_ACT"].ToString();
                            usuario.es_cliente = bool.Parse(dr["USU_ES_CLIENTE"].ToString());
                            usuario.usu_perfil = int.Parse(dr["USU_PERFIL"].ToString());
                            usuario.usu_pais = int.Parse(dr["USU_PAIS"].ToString());
                            usuario.usu_rut = dr["USU_RUT"].ToString();
                            usuario.usu_nacionalidad = dr["USU_NACIONALIDAD"] != null && dr["USU_NACIONALIDAD"].ToString() != "" ? Convert.ToInt32(dr["USU_NACIONALIDAD"]) : 0;

                            usuario.usu_estado_civil = dr["USU_ESTADO_CIVIL"] != null && dr["USU_ESTADO_CIVIL"].ToString() != ""
                                ? Convert.ToInt32(dr["USU_ESTADO_CIVIL"]) : 0;

                            usuario.usu_profesion = dr["USU_PROFESION"] != null && dr["USU_PROFESION"].ToString() != ""
                                ? Convert.ToInt32(dr["USU_PROFESION"]) : 0;

                            usuario.usu_genero = dr["USU_GENERO"] != null && dr["USU_GENERO"].ToString() != ""
                                ? Convert.ToInt32(dr["USU_GENERO"]) : 0;

                            usuario.usu_comuna = dr["USU_COMUNA"] != null && dr["USU_COMUNA"].ToString() != ""
                                ? Convert.ToInt32(dr["USU_COMUNA"]) : 0;

                            usuario.usu_ciudad = dr["USU_CIUDAD"] != null ? dr["USU_CIUDAD"].ToString() : string.Empty;

                            usuario.usu_calle = dr["USU_CALLE"] != null ? dr["USU_CALLE"].ToString() : string.Empty;

                            usuario.usu_numero_propiedad = dr["USU_NUMERO_PROPIEDAD"] != null ? dr["USU_NUMERO_PROPIEDAD"].ToString() : string.Empty;



                            if (dr["USU_ULTIMO_LOGIN"].ToString() != "") usuario.usu_ultimo_login = DateTime.Parse(dr["USU_ULTIMO_LOGIN"].ToString());

                            if (dr["USU_FOTO"].ToString() != "") usuario.usu_foto = (byte[])dr["USU_FOTO"];

                        }
                    }


                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    usuario = null;
                }
            }

            return usuario;
        }

        public List<Usuarios> GetClienteUsuariosIdentidad(Usuarios item)
        {
            List<Usuarios> usuarios = new List<Usuarios>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "SEL_USUARIOS";
                    cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    if (item.filtro != "") cmd.Parameters.AddWithValue("@FILTRO", item.filtro);
                    if (item.perfiles != "") cmd.Parameters.AddWithValue("@PERFIL", item.perfiles);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Usuarios usuario = new Usuarios();

                            usuario.usu_id = int.Parse(dr["USU_ID"].ToString());
                            usuario.usu_login = dr["USU_LOGIN"].ToString();
                            usuario.usu_password = dr["USU_PASSWORD"].ToString();
                            usuario.usu_nombres = dr["USU_NOMBRE"].ToString();
                            usuario.usu_apellido_paterno = dr["USU_APELLIDO_PATERNO"].ToString();
                            usuario.usu_apellido_materno = dr["USU_APELLIDO_MATERNO"].ToString();
                            usuario.nombreCompleto = usuario.usu_nombres + " " + usuario.usu_apellido_paterno + " " + usuario.usu_apellido_materno;
                            usuario.usu_correo = dr["USU_CORREO"].ToString();
                            usuario.usu_fono = dr["USU_FONO"].ToString();
                            usuario.usu_habilitado = bool.Parse(dr["USU_HABILITADO"].ToString());

                            usuario.es_cliente = bool.Parse(dr["USU_ES_CLIENTE"].ToString());
                            usuario.usu_perfil = int.Parse(dr["USU_PERFIL"].ToString());
                            usuario.usu_pais = int.Parse(dr["USU_PAIS"].ToString());
                            usuario.perfil_nombre = dr["PERFIL"].ToString();
                            if(!string.IsNullOrEmpty(dr["NOMBRE_PROPIEDAD"].ToString())) usuario.nombre_propiedad = dr["NOMBRE_PROPIEDAD"].ToString();

                            usuarios.Add(usuario);
                        }
                    }


                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    usuarios = null;
                }
            }

            return usuarios;
        }

        public Usuarios GetUsuarioCliente()
        {
            Usuarios item = new Usuarios();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_USUARIO_CLIENTE";
                    cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        if (dr.Read())
                        {
                            item.cliente = int.Parse(dr["CLIENTE"].ToString());
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

        public Respuesta InsertClienteUsuario(Usuarios usuario)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = null;

                try
                {
                    int id = 0;

                    cmdExecute = Conexion.GetCommand("INS_CLIENTE_USUARIOS");
                    cmdExecute.Parameters.AddWithValue("@ID", id).Direction = System.Data.ParameterDirection.Output;
                    cmdExecute.Parameters.AddWithValue("@LOGIN", usuario.usu_login);
                    cmdExecute.Parameters.AddWithValue("@PASSWORD", usuario.usu_password);
                    cmdExecute.Parameters.AddWithValue("@NOMBRES", usuario.usu_nombres);
                    cmdExecute.Parameters.AddWithValue("@APELLIDO_PATERNO", usuario.usu_apellido_paterno);
                    cmdExecute.Parameters.AddWithValue("@APELLIDO_MATERNO", usuario.usu_apellido_materno);
                    cmdExecute.Parameters.AddWithValue("@CORREO", usuario.usu_correo);
                    cmdExecute.Parameters.AddWithValue("@FONO", usuario.usu_fono);
                    cmdExecute.Parameters.AddWithValue("@FOTO", usuario.usu_foto);
                    cmdExecute.Parameters.AddWithValue("@HABILITADO", usuario.usu_habilitado);
                    cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    cmdExecute.Parameters.AddWithValue("@HOST", Session.RemoteHost());
                    cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());

                    cmdExecute.Parameters.AddWithValue("@PERFIL", usuario.usu_perfil);
                    cmdExecute.Parameters.AddWithValue("@PAIS_COMBOBOX", usuario.usu_pais);

                    cmdExecute.Parameters.AddWithValue("@ES_CLIENTE", true);
                    cmdExecute.Parameters.AddWithValue("@CLIENTE", usuario.cliente);

                    cmdExecute.Parameters.AddWithValue("@COMUNA", usuario.usu_comuna);
                    cmdExecute.Parameters.AddWithValue("@CIUDAD", usuario.usu_ciudad);
                    cmdExecute.Parameters.AddWithValue("@CALLE", usuario.usu_calle);
                    cmdExecute.Parameters.AddWithValue("@NUMERO_PROPIEDAD", usuario.usu_numero_propiedad);
                    cmdExecute.Parameters.AddWithValue("@GENERO", usuario.usu_genero);
                    cmdExecute.Parameters.AddWithValue("@PROFESION", usuario.usu_profesion);
                    cmdExecute.Parameters.AddWithValue("@ESTADO_CIVIL", usuario.usu_estado_civil);
                    cmdExecute.Parameters.AddWithValue("@RUT", usuario.usu_rut);
                    cmdExecute.Parameters.AddWithValue("@NACIONALIDAD", usuario.usu_nacionalidad);



                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();


                    id = (int)cmdExecute.Parameters["@ID"].Value;

                    respuesta.codigo = id;
                    respuesta.detalle = "El usuario fue creado con éxito.";
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

        public Respuesta UpdateClienteUsuario(Usuarios usuario)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                try
                {
                    cmdExecute = Conexion.GetCommand("UPD_USUARIOS");
                    cmdExecute.Parameters.AddWithValue("@ID", usuario.usu_id);
                    cmdExecute.Parameters.AddWithValue("@LOGIN", usuario.usu_login);
                    cmdExecute.Parameters.AddWithValue("@PASSWORD", usuario.usu_password);
                    cmdExecute.Parameters.AddWithValue("@NOMBRES", usuario.usu_nombres);
                    cmdExecute.Parameters.AddWithValue("@APELLIDO_PATERNO", usuario.usu_apellido_paterno);
                    cmdExecute.Parameters.AddWithValue("@APELLIDO_MATERNO", usuario.usu_apellido_materno);
                    cmdExecute.Parameters.AddWithValue("@CORREO", usuario.usu_correo);
                    cmdExecute.Parameters.AddWithValue("@FONO", usuario.usu_fono);
                    cmdExecute.Parameters.AddWithValue("@FOTO", usuario.usu_foto);
                    cmdExecute.Parameters.AddWithValue("@HABILITADO", usuario.usu_habilitado);
                    cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    cmdExecute.Parameters.AddWithValue("@HOST", Session.RemoteHost());
                    cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());
                    cmdExecute.Parameters.AddWithValue("@PERFIL", usuario.usu_perfil);
                    cmdExecute.Parameters.AddWithValue("@PAIS_COMBOBOX", usuario.usu_pais);

                    cmdExecute.Parameters.AddWithValue("@COMUNA", usuario.usu_comuna);
                    cmdExecute.Parameters.AddWithValue("@CIUDAD", usuario.usu_ciudad);
                    cmdExecute.Parameters.AddWithValue("@CALLE", usuario.usu_calle);
                    cmdExecute.Parameters.AddWithValue("@NUMERO_PROPIEDAD", usuario.usu_numero_propiedad);
                    cmdExecute.Parameters.AddWithValue("@GENERO", usuario.usu_genero);
                    cmdExecute.Parameters.AddWithValue("@PROFESION", usuario.usu_profesion);
                    cmdExecute.Parameters.AddWithValue("@ESTADO_CIVIL", usuario.usu_estado_civil);
                    cmdExecute.Parameters.AddWithValue("@RUT", usuario.usu_rut);
                    cmdExecute.Parameters.AddWithValue("@NACIONALIDAD", usuario.usu_nacionalidad);

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    respuesta.codigo = 0;
                    respuesta.detalle = "Usuario actualizado con éxito.";
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

        public Respuesta DeleteClienteUsuario(Usuarios usuario)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                try
                {
                    cmdExecute = Conexion.GetCommand("DEL_CLIENTE_USUARIO");
                    cmdExecute.Parameters.AddWithValue("@ID", usuario.usu_id);

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    respuesta.codigo = 0;
                    respuesta.detalle = "Usuario eliminado con éxito.";
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

        public Respuesta ResetPassword(Usuarios usuario)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                try
                {
                    cmdExecute = Conexion.GetCommand("UPD_USUARIO_RESET_PASSWORD");
                    cmdExecute.Parameters.AddWithValue("@ID", usuario.usu_id);

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    respuesta.codigo = 0;
                    respuesta.detalle = "Password reseteado con éxito. La nueva contraseña provisoria es: 123456";
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

        public List<Perfiles> GetPerfiles()
        {
            List<Perfiles> perfiles = new List<Perfiles>();
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = "SEL_PERFILES";
                cmd.Parameters.AddWithValue("@HABILITADO", true);
                cmd.Parameters.AddWithValue("@TIPO", 2);

                using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                {
                    while (dr.Read())
                    {
                        Perfiles perfil = new Perfiles();

                        perfil.per_id = int.Parse(dr["PER_ID"].ToString());
                        perfil.per_nombre = dr["PER_NOMBRE"].ToString();
                        perfiles.Add(perfil);
                    }
                }

                cmd.Connection.Close();
                cmd.Dispose();

                return perfiles;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                cmd.Dispose();

                return null;
            }
        }


        public UsuarioDatoPago GetUsuarioDatoPago(UsuarioDatoPago usuarioDatoPago)
        {
            UsuarioDatoPago item = new UsuarioDatoPago();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_USUARIO_DATO_PAGO";
                    if (usuarioDatoPago.upd_id > 0) cmd.Parameters.AddWithValue("@@ID", usuarioDatoPago.upd_id);
                    if (usuarioDatoPago.upd_id_usuario > 0) cmd.Parameters.AddWithValue("@USUARIO", usuarioDatoPago.upd_id_usuario);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        if (dr.Read())
                        {

                            item.upd_id = int.Parse(dr["UPD_ID"].ToString());
                            item.upd_id_usuario = int.Parse(dr["UPD_ID_USUARIO"].ToString());
                            item.upd_banco = int.Parse(dr["UPD_BANCO"].ToString());
                            item.upd_banco_otro = dr["UPD_BANCO_OTRO"].ToString();
                            item.upd_rut_cuenta = dr["UPD_RUT_CUENTA"].ToString();
                            item.upd_titular_cuenta = dr["UPD_TITULAR_CUENTA"].ToString();
                            item.upd_tipo_cuenta = int.Parse(dr["UPD_TIPO_CUENTA"].ToString());
                            item.upd_numero_cuenta = dr["UPD_NUMERO_CUENTA"].ToString();

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
        public Respuesta InsertUsuarioDatoPago(UsuarioDatoPago usuario)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = null;

                try
                {
                    int id = 0;

                    cmdExecute = Conexion.GetCommand("INS_CLIENTE_USUARIO_DATO_PAGO");
                    cmdExecute.Parameters.AddWithValue("@ID", id).Direction = System.Data.ParameterDirection.Output;
                    cmdExecute.Parameters.AddWithValue("@USUARIO_CREADO", usuario.upd_id_usuario);
                    cmdExecute.Parameters.AddWithValue("@BANCO", usuario.upd_banco);
                    cmdExecute.Parameters.AddWithValue("@OTRO_BANCO", usuario.upd_banco_otro);
                    cmdExecute.Parameters.AddWithValue("@TIPO_CUENTA", usuario.upd_tipo_cuenta);
                    cmdExecute.Parameters.AddWithValue("@RUT_TITULAR", usuario.upd_rut_cuenta);
                    cmdExecute.Parameters.AddWithValue("@NOMBRE_TITULAR", usuario.upd_titular_cuenta);
                    cmdExecute.Parameters.AddWithValue("@NUMERO_CUENTA", usuario.upd_numero_cuenta);
                    cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();


                    id = (int)cmdExecute.Parameters["@ID"].Value;

                    respuesta.codigo = id;
                    respuesta.detalle = "Los datos de pago de usuario fue creado con éxito.";
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

        public Respuesta UpdateUsuarioDatoPago(UsuarioDatoPago usuario)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                try
                {
                    cmdExecute = Conexion.GetCommand("UPD_USUARIO_DATO_PAGO");
                    cmdExecute.Parameters.AddWithValue("@ID", usuario.upd_id);
                    cmdExecute.Parameters.AddWithValue("@USUARIO_CREADO", usuario.upd_id_usuario);
                    cmdExecute.Parameters.AddWithValue("@BANCO", usuario.upd_banco);
                    cmdExecute.Parameters.AddWithValue("@OTRO_BANCO", usuario.upd_banco_otro);
                    cmdExecute.Parameters.AddWithValue("@TIPO_CUENTA", usuario.upd_tipo_cuenta);
                    cmdExecute.Parameters.AddWithValue("@RUT_TITULAR", usuario.upd_rut_cuenta);
                    cmdExecute.Parameters.AddWithValue("@NOMBRE_TITULAR", usuario.upd_titular_cuenta);
                    cmdExecute.Parameters.AddWithValue("@NUMERO_CUENTA", usuario.upd_numero_cuenta);
                    cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    respuesta.codigo = 0;
                    respuesta.detalle = "Datos del usuario actualizado con éxito.";
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