using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class ClientePlanBolsa
    {
        public int cpb_id { get; set; }
        public int cpb_cliente { get; set; }

        public int cpb_id_plan { get; set; }
        public int cpb_id_bolsa { get; set; }
        public DateTime? cpb_fecha_desde { get; set; }
        public DateTime? cpb_fecha_hasta { get; set; }
        public int cpb_cantidad { get; set; }
        public int cpb_administradores { get; set; }
        public int cpb_valor_bolsa { get; set; }
        public DateTime? cpb_fecha_creacion { get; set; }
        public string bolsa_nombre { get; set; }
        public int bolsa_cantidad { get; set; }
        public int bolsa_administradores { get; set; }
        public int bolsa_valor { get; set; }
        public string producto_nombre { get; set; }
    }
}