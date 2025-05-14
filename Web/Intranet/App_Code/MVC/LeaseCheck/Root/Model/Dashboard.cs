using System;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class Dashboard
    {
        public int cargo_id { get; set; }
        public string cargo_nombre { get; set; }
        public int cargo_cantidad_postulantes { get; set; }

        public int plan_informes_disponibles { get; set; }
        public int plan_informes_mes_actual { get; set; }
        public int plan_informes_antiguos { get; set; }

        public int cliente_id { get; set; }
        public string cliente_nombre { get; set; }
        public string cliente_vencimiento { get; set; }
        public int cliente_cantidad_test { get; set; }
        public int cliente_total_plan { get; set; }

        public int plan_cantidad { get; set; }
        public string plan_nombre { get; set; }
        public DateTime filtro_desde { get; set; }
        public DateTime filtro_hasta { get; set; }

        // Dashboard - Cantidad Propiedades
        public int plan_propiedad_actual { get; set; }
        public int plan_propiedad_total { get; set; }
        public int plan_propiedad_antigua { get; set; }
        public string productos { get; set; }
    }
}