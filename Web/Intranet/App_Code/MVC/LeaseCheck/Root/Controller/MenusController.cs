using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace LeaseCheck.Root.Controller
{
    public class MenusController
    {
        public List<Menus> GetMenus(Menus menu = null)
        {
            List<Menus> menus = new List<Menus>();

            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = "SEL_MENUS";
                //if (menu.mnu_tipo > 0) cmd.Parameters.AddWithValue("@TIPO", menu.mnu_tipo);
                //if (menu.mnu_nivel > 0) cmd.Parameters.AddWithValue("@NIVEL", menu.mnu_nivel);
                //if (menu.mnu_padre > 0) cmd.Parameters.AddWithValue("@PADRE", menu.mnu_padre);

                using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                {
                    while (dr.Read())
                    {
                        menu = new Menus();

                        menu.mnu_id = int.Parse(dr["MNU_ID"].ToString());
                        menu.mnu_nombre = dr["MNU_NOMBRE"].ToString();
                        menu.mnu_descripcion = dr["MNU_DESCRIPCION"].ToString();
                        menu.mnu_nivel = int.Parse(dr["MNU_NIVEL"].ToString());
                        menu.mnu_padre = int.Parse(dr["MNU_PADRE"].ToString());
                        menu.mnu_orden = int.Parse(dr["MNU_ORDEN"].ToString());
                        menu.mnu_link = dr["MNU_LINK"].ToString();
                        menu.mnu_visible = bool.Parse(dr["MNU_VISIBLE"].ToString());
                        if (dr["MNU_ICON"].ToString() != null) menu.mnu_icon = dr["MNU_ICON"].ToString();
                        menus.Add(menu);
                    }
                }

                cmd.Connection.Close();
                cmd.Dispose();

                return menus;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                cmd.Dispose();
                return menus;
            }
        }
    }
}