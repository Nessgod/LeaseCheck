using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using LeaseCheck.Root.Model;

namespace LeaseCheck.Root.Controller
{
    public class PaisesController
    {
        private SqlCommand cmdExecute = null;

   
        //Listo todos los paises (grilla)
        public List<Paises> GetPaises(Paises filtro = null)
        {
            List<Paises> paises = new List<Paises>();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_PAISES";
                    if(filtro != null) if(filtro.pai_habilitado) cmd.Parameters.AddWithValue("@HABILITADO", true);

                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Paises pais = new Paises();

                            pais.pai_id = int.Parse(dr["PAI_ID"].ToString());
                            pais.pai_nombre = dr["PAI_NOMBRE"].ToString();
                            pais.pai_fecha_creacion = DateTime.Parse(dr["PAI_FECHA_CREACION"].ToString());
                            pais.pai_habilitado = bool.Parse(dr["PAI_HABILITADO"].ToString());
                           
                            paises.Add(pais);
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

        public List<Paises> GetPaisesLogin()
        {
            List<Paises> paises = new List<Paises>();

           
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_PAISES";
                    cmd.Parameters.AddWithValue("@HABILITADO", true);

                using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        while (dr.Read())
                        {
                            Paises pais = new Paises();

                            pais.pai_id = int.Parse(dr["PAI_ID"].ToString());
                            pais.pai_nombre = dr["PAI_NOMBRE"].ToString();
                            pais.pai_fecha_creacion = DateTime.Parse(dr["PAI_FECHA_CREACION"].ToString());
                            pais.pai_habilitado = bool.Parse(dr["PAI_HABILITADO"].ToString());
                            if (dr["PAI_IMAGEN"].ToString() != "") pais.pai_imagen = (byte[])dr["PAI_IMAGEN"];
                            if (!string.IsNullOrEmpty(dr["PAI_EXTENSION"].ToString())) pais.pai_extension = dr["PAI_EXTENSION"].ToString();
                            pais.pai_habilitado = bool.Parse(dr["PAI_HABILITADO"].ToString());

                        paises.Add(pais);
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
            
            return paises;
        }
        //Listo datos de un país
        public Paises GetPais(Paises pais)
        {
            if (Token.TokenSeguridad())
            {
                SqlCommand cmd = new SqlCommand();

                try
                {
                    cmd.CommandText = "SEL_PAISES";
                    cmd.Parameters.AddWithValue("@ID", pais.pai_id);
                    
                    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                    {
                        if (dr.Read())
                        {
                            pais = new Paises();

                            pais.pai_id = int.Parse(dr["PAI_ID"].ToString());
                            pais.pai_nombre = dr["PAI_NOMBRE"].ToString();
                            pais.pai_suma_resta = dr["PAI_SUMA_RESTA"].ToString();
                            pais.pai_hora = int.Parse(dr["PAI_HORA"].ToString());
                            pais.pai_habilitado = bool.Parse(dr["PAI_HABILITADO"].ToString());
                            if (dr["PAI_IMAGEN"].ToString() != "") pais.pai_imagen = (byte[])dr["PAI_IMAGEN"];
                            if (!string.IsNullOrEmpty(dr["PAI_NOMBRE_IMAGEN"].ToString()))pais.pai_nombre_imagen = dr["PAI_NOMBRE_IMAGEN"].ToString();
                            if(!string.IsNullOrEmpty(dr["PAI_EXTENSION"].ToString())) pais.pai_extension = dr["PAI_EXTENSION"].ToString();
                        }
                    }
                    cmd.Connection.Close();
                    cmd.Dispose();
                }
                catch (Exception ex)
                {
                    cmd.Connection.Close();
                    cmd.Dispose();

                    pais = null;
                }
            }
            return pais;
        }

        //Listo datos de un país
        public Paises GetPaisLogin(Paises pais)
        {
            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = "SEL_PAISES";
                cmd.Parameters.AddWithValue("@ID", pais.pai_id);

                using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                {
                    if (dr.Read())
                    {
                        pais = new Paises();

                        pais.pai_id = int.Parse(dr["PAI_ID"].ToString());
                        pais.pai_nombre = dr["PAI_NOMBRE"].ToString();
                        pais.pai_suma_resta = dr["PAI_SUMA_RESTA"].ToString();
                        pais.pai_hora = int.Parse(dr["PAI_HORA"].ToString());
                        pais.pai_habilitado = bool.Parse(dr["PAI_HABILITADO"].ToString());
                        if (dr["PAI_IMAGEN"].ToString() != "") pais.pai_imagen = (byte[])dr["PAI_IMAGEN"];
                        if (!string.IsNullOrEmpty(dr["PAI_NOMBRE_IMAGEN"].ToString())) pais.pai_nombre_imagen = dr["PAI_NOMBRE_IMAGEN"].ToString();
                        if (!string.IsNullOrEmpty(dr["PAI_EXTENSION"].ToString())) pais.pai_extension = dr["PAI_EXTENSION"].ToString();
                    }
                }
                cmd.Connection.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                cmd.Dispose();

                pais = null;
            }
            
            return pais;
        }
        //Inserto registro
        public Respuesta InsertPais(Paises pais)
        {
            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                SqlCommand cmdExecute = null;

                try
                {
                    int id = 0;

                    cmdExecute = Conexion.GetCommand("INS_PAISES");
                    cmdExecute.Parameters.AddWithValue("@ID", id).Direction = System.Data.ParameterDirection.Output;                    
                    cmdExecute.Parameters.AddWithValue("@NOMBRE", pais.pai_nombre);
                    cmdExecute.Parameters.AddWithValue("@SUMA_RESTA", pais.pai_suma_resta);
                    cmdExecute.Parameters.AddWithValue("@HORA", pais.pai_hora);
                    cmdExecute.Parameters.AddWithValue("@HABILITADO", pais.pai_habilitado);
                    cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());
                    cmdExecute.Parameters.AddWithValue("@IMAGEN", pais.pai_imagen);
                    cmdExecute.Parameters.AddWithValue("@NOMBRE_IMAGEN", pais.pai_nombre_imagen);
                    cmdExecute.Parameters.AddWithValue("@EXTENSION", pais.pai_extension);
                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    id = (int)cmdExecute.Parameters["@ID"].Value;

                    respuesta.codigo = id;
                    respuesta.detalle = "País creado con éxito.";
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

        //Actualizo registro
        public Respuesta UpdatePais(Paises pais)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                try
                {
                    cmdExecute = Conexion.GetCommand("UPD_PAISES");
                    cmdExecute.Parameters.AddWithValue("@ID", pais.pai_id);
                    cmdExecute.Parameters.AddWithValue("@NOMBRE", pais.pai_nombre);
                    cmdExecute.Parameters.AddWithValue("@SUMA_RESTA", pais.pai_suma_resta);
                    cmdExecute.Parameters.AddWithValue("@HORA", pais.pai_hora);
                    cmdExecute.Parameters.AddWithValue("@HABILITADO", pais.pai_habilitado);
                    cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                    cmdExecute.Parameters.AddWithValue("@PAIS", Session.Pais());
                    cmdExecute.Parameters.AddWithValue("@IMAGEN", pais.pai_imagen);
                    cmdExecute.Parameters.AddWithValue("@NOMBRE_IMAGEN", pais.pai_nombre_imagen);
                    cmdExecute.Parameters.AddWithValue("@EXTENSION", pais.pai_extension);
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

        //Elimino registro(s)
        public Respuesta DeletePais(Paises pais)
        {
            SqlCommand cmdExecute = new SqlCommand();

            Respuesta respuesta = new Respuesta();

            if (Token.TokenSeguridad())
            {
                try
                {
                    cmdExecute = Conexion.GetCommand("DEL_PAIS");
                    cmdExecute.Parameters.AddWithValue("@ID", pais.pai_id);

                    cmdExecute.ExecuteNonQuery();
                    cmdExecute.Connection.Close();

                    respuesta.codigo = 0;
                    respuesta.detalle = "País eliminado con éxito.";
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

        //generar excel
        public void InformePaises(Paises paises) 
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "RPT_PAISES";
            if (paises.pai_id > 0) cmd.Parameters.AddWithValue("@PAI_ID", paises.pai_id);

            string filename = " INFORME PAISES " + DateTime.Now;
            Tools.Excel.exportExcel(Conexion.GetDataTable(cmd), filename, true);
        }
    }
}