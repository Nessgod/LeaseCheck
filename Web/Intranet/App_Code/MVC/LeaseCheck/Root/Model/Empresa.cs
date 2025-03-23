using System;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class Empresa
    {
        public int emp_id { get; set; }
        public string emp_nombre { get; set; }
        public string emp_razon_social { get; set; }
        public int emp_rut { get; set; }
        public string emp_dv { get; set; }
        public int emp_holding { get; set; }
        public bool emp_habilitado { get; set; }

        public string rut_completo { get; set; }
        public string holding_nombre { get; set; }
        public string filtro { get; set; }
    }
}