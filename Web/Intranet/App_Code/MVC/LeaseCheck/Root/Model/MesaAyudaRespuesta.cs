using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class MesaAyudaRespuesta
    {
        public int mer_id { get; set; }
        public int mer_id_mesa_ayuda { get; set; }

        public string mer_respuesta { get; set; }

        public int mer_tipo_respuesta { get; set; }

        public int mer_usuario_creacion { get; set; }
        
        public DateTime mer_fecha_creacion { get; set; }

        public string NOMBRE_EJECUTOR { get; set; }
        public string QUIEN_RESPONDIO { get; set; }
        public string NOMBRE_CLIENTE { get; set; }

        public byte[] FOTO_USUARIO { get; set; }
    }
}