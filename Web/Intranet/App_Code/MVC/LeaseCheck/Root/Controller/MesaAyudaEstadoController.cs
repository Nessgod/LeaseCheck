using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using AccesoDatosOfertasLaborales.Model;
using System.Data.SqlClient;
using LeaseCheck.Root.Model;

namespace LeaseCheck.Root.Controller
{
    public class MesaAyudaEstadoController
    {
        private SqlCommand cmdExecute = null;
        private string ConectionString = "ConectionStringOfertaLaboral";

        public List<MesaAyudaEstado> GetMesaAyudaEstado(MesaAyudaEstado mesaAyudaEstado = null)
        {
            List<MesaAyudaEstado> mesaAyudaEstados = new List<MesaAyudaEstado>();

            SqlCommand cmd = new SqlCommand();

            try
            {
                cmd.CommandText = "SEL_MESA_AYUDA_ESTADO";
                using (SqlDataReader dr = Conexion.GetDataReader(cmd))
                {
                    while (dr.Read())
                    {
                        mesaAyudaEstado = new MesaAyudaEstado();

                        mesaAyudaEstado.est_id = int.Parse(dr["EST_ID"].ToString());
                        mesaAyudaEstado.est_nombre = dr["EST_NOMBRE"].ToString();

                        mesaAyudaEstados.Add(mesaAyudaEstado);
                    }
                }

                cmd.Connection.Close();
                cmd.Dispose();

                return mesaAyudaEstados;
            }
            catch (Exception ex)
            {
                cmd.Connection.Close();
                cmd.Dispose();
                return mesaAyudaEstados;
            }
        }

    }
}