﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseCheck.Clientes.Model
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

        public bool? es_cliente { get; set; }
        public int usu_perfil { get; set; }
        public int usu_pais { get; set; }
        public int cliente { get; set; }
        public string perfil_nombre { get; set; }
    }
}