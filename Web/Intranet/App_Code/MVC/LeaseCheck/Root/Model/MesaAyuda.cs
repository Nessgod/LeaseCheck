using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class MesaAyuda
    {
        public int mes_id { get; set; }
        public string mes_nombre { get; set; }
        public string mes_telefono { get; set; }
        public string mes_correo { get; set; }
        public string mes_mensaje { get; set; }
        public int mes_estado { get; set; }
        public string mes_estado_nombre { get; set; }
        public DateTime mes_fecha_creacion { get; set; }
        public int mes_usuario_creacion { get; set; }
        public DateTime mes_fecha_cierre { get; set; }
        public string mes_observacion_cierre { get; set; }

        public int mes_id_cliente { get; set; }
        public int mes_id_modulo { get; set; }
        public string mes_otro_modulo { get; set; }

        public string NOMBRE_CLIENTE { get; set; }
        public string NOMBRE_MODULO { get; set; }
        public string NOMBRE_CREADOR { get; set; }

        public string RESPONSABLE_CIERRE { get; set; }
        public string PERFIL { get; set; }

        public DateTime FECHA_ULTIMA_RESPUESTA { get; set; }
        public string filtro { get; set; }

        public DateTime? fecha_respuesta { get; set; }
    }
}