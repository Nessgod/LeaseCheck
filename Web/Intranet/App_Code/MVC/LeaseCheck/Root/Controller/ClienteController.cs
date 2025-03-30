using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LeaseCheck.Root.Controller
{
    public class ClienteController
    {
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
                            item.clp_cantidad = int.Parse(dr["CLP_CANTIDAD"].ToString());
                            item.clp_administradores = int.Parse(dr["CLP_ADMINISTRADORES"].ToString());
                            item.clp_administradores_ilimitados = bool.Parse(dr["CLP_ADMINISTRADORES_ILIMITADOS"].ToString());
                            item.clp_valor_plan = int.Parse(dr["CLP_VALOR_PLAN"].ToString());

                            item.plan_nombre = dr["PLAN_NOMBRE"].ToString();
                            item.plan_informes = int.Parse(dr["PLAN_INFORMES"].ToString());
                            item.plan_administradores = int.Parse(dr["PLAN_ADMINISTRADORES"].ToString());
                            item.plan_ilimitados = bool.Parse(dr["PLAN_ILIMITADOS"].ToString());
                            item.tipo_dato = dr["TIPO"].ToString();
                            item.estado = dr["ESTADO"].ToString();

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

                            item.clp_valor_plan = int.Parse(dr["CLP_VALOR_PLAN"].ToString());

                            item.plan_nombre = dr["PLAN_NOMBRE"].ToString();
                            item.plan_informes = int.Parse(dr["PLAN_INFORMES"].ToString());
                            item.plan_administradores = int.Parse(dr["PLAN_ADMINISTRADORES"].ToString());
                            item.plan_ilimitados = bool.Parse(dr["PLAN_ILIMITADOS"].ToString());

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
                            item.clp_cantidad = int.Parse(dr["CLP_CANTIDAD"].ToString());
                            item.clp_administradores = int.Parse(dr["CLP_ADMINISTRADORES"].ToString());
                            item.clp_administradores_ilimitados = bool.Parse(dr["CLP_ADMINISTRADORES_ILIMITADOS"].ToString());
                            item.clp_valor_plan = int.Parse(dr["CLP_VALOR_PLAN"].ToString());
                            item.plan_informes = int.Parse(dr["PLAN_INFORMES"].ToString());
                            item.plan_administradores = int.Parse(dr["PLAN_ADMINISTRADORES"].ToString());
                            item.plan_ilimitados = bool.Parse(dr["PLAN_ILIMITADOS"].ToString());

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
                    cmd.Parameters.AddWithValue("@CANTIDAD", item.clp_cantidad);
                    cmd.Parameters.AddWithValue("@ADIMINISTRADORES", item.clp_administradores);
                    cmd.Parameters.AddWithValue("@ILIMITADOS", item.clp_administradores_ilimitados);
                    cmd.Parameters.AddWithValue("@VALOR_PLAN", item.clp_valor_plan);

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
                    cmd.Parameters.AddWithValue("@CANTIDAD", item.clp_cantidad);
                    cmd.Parameters.AddWithValue("@ADMINISTRADORES", item.clp_administradores);
                    cmd.Parameters.AddWithValue("@ILIMITADOS", item.clp_administradores_ilimitados);
                    cmd.Parameters.AddWithValue("@VALOR_PLAN", item.clp_valor_plan);

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
                    cmd.Parameters.AddWithValue("@PAIS", filtro.cmn_pais);

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
                            item.tpl_cantidad_informes = int.Parse(dr["TPL_CANTIDAD_INFORMES"].ToString());
                            item.tpl_cantidad_administradores = int.Parse(dr["TPL_CANTIDAD_ADMINISTRADORES"].ToString());
                            item.tpl_administradores_ilimitados = bool.Parse(dr["TPL_ADMINISTRADORES_ILIMITADOS"].ToString());

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

      
        // usuarios
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
                    respuesta.detalle = "Password reseteado con éxito.";
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

        // reasignacion clientes

        public List<Usuarios> GetUsuarioReasignacion()
        {
            List<Usuarios> usuarios = new List<Usuarios>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();
                try
                {
                    cmd.CommandText = "SEL_USUARIOS_REASIGNACION_CLIENTE";

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Usuarios usuario = new Usuarios();

                            usuario.usu_id = int.Parse(dr["USU_ID"].ToString());
                            usuario.usu_nombres = dr["USU_NOMBRE"].ToString();
                            usuario.usu_apellido_paterno = dr["USU_APELLIDO_PATERNO"].ToString();
                            usuario.usu_apellido_materno = dr["USU_APELLIDO_MATERNO"].ToString();
                            usuario.nombreCompleto = usuario.usu_nombres + " " + usuario.usu_apellido_paterno + " " + usuario.usu_apellido_materno;

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

        public List<Cliente> GetClienteReasignacion(Cliente filtro)
        {
            List<Cliente> listado = new List<Cliente>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_CLIENTE_REASIGNACION";
                    cmd.Parameters.AddWithValue("@USUARIO", filtro.cli_usuario);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Cliente item = new Cliente();

                            item.cli_id = int.Parse(dr["CLI_ID"].ToString());
                            item.cli_rut_completo = dr["CLI_RUT_COMPLETO"].ToString();
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

        public List<Cliente> GetClienteReasignacionAsociar(Cliente filtro)
        {
            List<Cliente> listado = new List<Cliente>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_CLIENTE_REASIGNACION_ASOCIAR";
                    cmd.Parameters.AddWithValue("@USUARIO", filtro.cli_usuario);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Cliente item = new Cliente();

                            item.cli_id = int.Parse(dr["CLI_ID"].ToString());
                            item.cli_rut_completo = dr["CLI_RUT_COMPLETO"].ToString();
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

        public Respuesta UpdateClienteReasignacion(Cliente item)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = null;

                try
                {
                    cmd = Conexion.GetCommand("UPD_CLIENTE_REASIGNACION");
                    cmd.Parameters.AddWithValue("@USUARIO", item.cli_usuario);
                    cmd.Parameters.AddWithValue("@CLIENTE", item.cli_id);

                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    cmd.Dispose();

                    respuesta.detalle = "Registro asociado con éxito.";
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