using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeaseCheck.Root.Model;
using System.Data.SqlClient;

namespace LeaseCheck.Root.Controller
{
    public class UsuarioPerfilesController
    {
        //GetUsuarioPerfiles
        public List<UsuarioPerfiles> GetUsuarioPerfiles(UsuarioPerfiles usuarioPerfiles)
        {
            List<UsuarioPerfiles> listaUsuarioPerfil = new List<UsuarioPerfiles>();
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SEL_USUARIO_PERFILES";
            cmd.Parameters.AddWithValue("@ID_USUARIO", usuarioPerfiles.upe_usuario);
            cmd.Parameters.AddWithValue("@LEFT", usuarioPerfiles.left);

            using (SqlDataReader dr = Conexion.GetDataReader(cmd))
            {
                while (dr.Read())
                {
                    UsuarioPerfiles usuarioPerfil = new UsuarioPerfiles();
                    usuarioPerfil.upe_perfil = int.Parse(dr["PER_ID"].ToString());
                    usuarioPerfil.perfilNombre = dr["PER_NOMBRE"].ToString();
                    usuarioPerfil.upe_id = int.Parse(dr["UPE_ID"].ToString());

                    usuarioPerfil.upe_usuario = usuarioPerfiles.upe_usuario;
                    
                    Perfiles perfil = new Perfiles();

                    listaUsuarioPerfil.Add(usuarioPerfil);
                }
            }
            

            return listaUsuarioPerfil;
        }
      
        //Inserto UsuarioPerfil
        public Respuesta InsertUsuarioPerfil(UsuarioPerfiles usuarioPerfil)
        {
            SqlCommand cmdExecute = new SqlCommand();

            //Creo el objeto respuesta
            Respuesta respuesta = new Respuesta();

            try
            {

                //inserto UsuarioPerfil en la base de datos
                int id = 0;
                cmdExecute = Conexion.GetCommand("INS_USUARIO_PERFILES");
                cmdExecute.Parameters.AddWithValue("@ID", id).Direction = System.Data.ParameterDirection.Output;
                cmdExecute.Parameters.AddWithValue("@ID_USUARIO", usuarioPerfil.upe_usuario);
                cmdExecute.Parameters.AddWithValue("@IDS_PERFILES", usuarioPerfil.ids_perfiles);
                cmdExecute.Parameters.AddWithValue("@ESTADO", usuarioPerfil.upe_estado);
                cmdExecute.Parameters.AddWithValue("@USUARIO", Session.UsuarioId());
                cmdExecute.Parameters.AddWithValue("@HOST", Session.RemoteHost());

                cmdExecute.ExecuteNonQuery();
                cmdExecute.Connection.Close();

                //Recupero el id del archivo insertado
                id = (int)cmdExecute.Parameters["@ID"].Value;

                respuesta.codigo = id;
                respuesta.error = false;
                respuesta.detalle = "Perfil asociado con éxito.";

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

        //public string GetPermisosPerfiles(string perfiles)
        //{
        //    List<int> listaPermisos = new List<int>();
        //    string permisos = "";

        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandText = "SEL_PERMISOS";
        //    cmd.Parameters.AddWithValue("@PERFILES", perfiles);

        //    using (SqlDataReader dr = Conexion.GetDataReader(cmd))
        //    {
        //        while (dr.Read())
        //        {
        //            permisos = dr["PERMISOS"].ToString();
        //        }
        //    }
        //    return permisos;
        //}
    }

}