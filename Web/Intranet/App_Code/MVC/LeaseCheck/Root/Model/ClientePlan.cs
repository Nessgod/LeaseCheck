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
        public int clp_cantidad { get; set; }
        public int clp_administradores { get; set; }
        public bool clp_administradores_ilimitados{ get; set; }
        public int clp_valor_plan { get; set; }

        public string plan_nombre { get; set; }
        public int plan_informes { get; set; }
        public int plan_administradores { get; set; }
        public bool plan_ilimitados { get; set; }
        public string tipo_dato { get; set; }
        public string producto_nombre { get; set; }
        public string estado { get; set; }
    }
}