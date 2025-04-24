using System;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class ClientePlan
    {
        public int clp_id { get; set; }
        public int clp_cliente { get; set; }
        public int clp_tipo_plan { get; set; }
        public DateTime clp_fecha_desde { get; set; }
        public DateTime clp_fecha_hasta { get; set; }

        public int valor_plan { get; set; }
        public string plan_nombre { get; set; }
        public int plan_documento { get; set; }
        public int plan_propiedad { get; set; }
        public int plan_lead { get; set; }
        public string tipo_dato { get; set; }
        public string producto_nombre { get; set; }
        public string estado { get; set; }
    }
}