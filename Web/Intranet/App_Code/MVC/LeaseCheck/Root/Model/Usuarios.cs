using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class Usuarios
    {
        public int usu_id { get; set; }
        public string usu_login { get; set; }
        public string usu_password { get; set; }
        public string usu_nombres { get; set; }
        public string usu_apellido_paterno { get; set; }
        public string usu_apellido_materno { get; set; }
        public string usu_correo { get; set; }
        public string usu_fono { get; set; }
        public bool? usu_habilitado { get; set; }
        public byte[] usu_foto { get; set; }
        public string usuario_creacion { get; set; }
        public DateTime usu_fecha_creacion { get; set; }
        public string usu_host_creacion { get; set; }
        public string usuario_act { get; set; }
        public DateTime usu_fecha_act { get; set; }
        public string usu_host_act { get; set; }
        public DateTime? usu_ultimo_login { get; set; }
        public bool devuelve_foto { get; set; }

        public string nombreCompleto { get; set; }

        public string perfiles { get; set; }
        public string filtro { get; set; }
        public string filtro_Cliente { get; set; }
        public string filtro_Pais { get; set; }

        public bool? es_cliente { get; set; }
        public int usu_perfil { get; set; }
        public int usu_pais { get; set; }
        public int cliente { get; set; }
        public string perfil_nombre { get; set; }

        public string cliente_nombre { get; set; }

        public string NOMBRE_COMPLETO { get; set; }

        public int usu_comuna { get; set; }
        public string usu_ciudad { get; set; }
        public string usu_calle { get; set; }
        public string usu_numero_propiedad { get; set; }
        public int usu_genero { get; set; }
        public int usu_profesion { get; set; }
        public int usu_estado_civil { get; set; }
        public string usu_rut { get; set; }
        public int usu_nacionalidad { get; set; }

    }
}