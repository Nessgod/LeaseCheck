using System;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class TipoPlan
    {
        public int tpl_id { get; set; }
        public string tpl_nombre { get; set; }
        public int tpl_cantidad_informes { get; set; }
        public int tpl_cantidad_administradores { get; set; } 
        public bool tpl_administradores_ilimitados { get; set; }
        public int tpl_valor_plan { get; set; }
        
        public bool tpl_habilitado { get; set; }
        public DateTime tpl_fecha_creacion { get; set; }
        public string filtro { get; set; }
    }
}