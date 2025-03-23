using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class PlanVencimiento
    {
        public string cliente { get; set; }
        public string telefono { get; set; }
        public bool habilitado { get; set; }
        public string mail { get; set; }
        public string contacto_nombre { get; set; }
        public string contacto_telefono { get; set; }
        public string contacto_mail { get; set; }
        public string plan { get; set; }
        public DateTime vigencia_desde { get; set; }
        public DateTime vigencia_hasta { get; set; }

        public string filtro { get; set; }
        public string filtro_Cliente { get; set; }
        public string filtro_Pais { get; set; }
        public bool? filtro_Estado { get; set; }
        public DateTime? desde { get; set; }
        public DateTime? hasta { get; set; }
    }
}