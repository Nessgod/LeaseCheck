using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LeaseCheck.Root.Controller
{
    public class DashboardController
    {

        public Dashboard GetEstadisticaCliente(Dashboard filtro = null)
        {
            Dashboard item = new Dashboard();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_DASHBOARD_CLIENTE";
                    cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        if (dr.Read())
                        {
                            item.plan_propiedad_total = int.Parse(dr["TOTAL_DISPONIBLES"].ToString());
                            item.plan_propiedad_actual = int.Parse(dr["CANTIDAD_MES_ACTUAL"].ToString());
                            item.plan_propiedad_antigua = int.Parse(dr["CANTIDAD_ANTIGUA"].ToString());
                            item.plan_nombre = dr["NOMBRE_PLAN"].ToString();
                            item.productos = dr["PRODUCTOS"].ToString();
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

        public List<Dashboard> GetDashComercial(Dashboard filtro = null)
        {
            List<Dashboard> listado = new List<Dashboard>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_DASHBOARD_COMERCIAL_DETALLE";
                    cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Dashboard item = new Dashboard();

                            item.cliente_id = int.Parse(dr["ID_CLIENTE"].ToString());
                            item.cliente_nombre = dr["CLIENTE"].ToString();
                            item.cliente_vencimiento = dr["VENCIMIENTO"].ToString();
                            item.cliente_total_plan = int.Parse(dr["TOTAL_PLAN"].ToString());
                          

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

        public List<Dashboard> GetEstadisticaComercial(Dashboard filtro = null)
        {
            List<Dashboard> listado = new List<Dashboard>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_DASHBOARD_COMERCIAL";
                    cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Dashboard item = new Dashboard();

                            item.plan_cantidad = int.Parse(dr["PLAN_CANTIDAD"].ToString());
                            item.plan_nombre = dr["PLAN_NOMBRE"].ToString();

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

   
        public List<Dashboard> GetDashComercialPlan(Dashboard filtro = null)
        {
            List<Dashboard> listado = new List<Dashboard>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_DASHBOARD_COMERCIAL_PLANES";
                    cmd.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    if (!string.IsNullOrEmpty(filtro.plan_nombre)) cmd.Parameters.AddWithValue("@FILTRO", filtro.plan_nombre);
                    if (filtro.cliente_id > 0) cmd.Parameters.AddWithValue("@CLIENTE", filtro.cliente_id);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Dashboard item = new Dashboard();

                            item.plan_nombre = dr["PLAN_NOMBRE"].ToString();
                            item.cliente_nombre = dr["CLIENTE"].ToString();
                            item.cliente_vencimiento = dr["VENCIMIENTO"].ToString();
                            item.plan_propiedad_total = int.Parse(dr["PROPIEDADES_CREADAS"].ToString());

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
    }
}