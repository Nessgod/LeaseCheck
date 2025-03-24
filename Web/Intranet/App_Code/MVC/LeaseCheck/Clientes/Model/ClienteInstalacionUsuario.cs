using System;
using System.Collections.Generic;

namespace LeaseCheck.Model
{
    [Serializable]
    public class ClienteInstalacionUsuario
    {
        
        public int ciu_id { get; set; }
        public int ciu_instalacion { get; set; }
        public int ciu_usuario { get; set; }
        public bool ciu_responsable { get; set; }
        public bool ciu_habilitado { get; set; }

        public int ciu_usuario_creacion { get; set; }
        public DateTime ciu_fecha_creacion { get; set; }
        public int ciu_usuario_act { get; set; }

        public DateTime ciu_fecha_act { get; set; }

        public int usu_id { get; set; }

        public string NOMBRE_COMPLETO { get; set; }
        public string usu_correo { get; set; }

        public int cli_id { get; set; }



    }
}