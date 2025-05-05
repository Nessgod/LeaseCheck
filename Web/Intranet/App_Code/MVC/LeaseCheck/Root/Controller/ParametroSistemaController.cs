using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeaseCheck.Root.Model;
using System.Data.SqlClient;

namespace LeaseCheck.Root.Controller
{
    public class ParametroSistemaController
    {
        public Parametros GetParametros(Parametros parametro)
        {

            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = "SEL_PARAMETROS";
                cmd.Parameters.AddWithValue("@CODIGO", parametro.par_codigo);


                using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                {
                    if (dr.Read())
                    {
                        parametro = new Parametros();

                        parametro.par_codigo = dr["PAR_CODIGO"].ToString();
                        parametro.par_nombre = dr["PAR_NOMBRE"].ToString();
                        parametro.par_valor = dr["PAR_VALOR"].ToString();
                    }
                }
                cmd.Connection.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                cmd.Dispose();

                parametro = null;
            }

            return parametro;
        }
    }

}