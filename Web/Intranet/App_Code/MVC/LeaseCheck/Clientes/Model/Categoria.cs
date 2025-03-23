using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseCheck.Clientes.Model
{
    [Serializable]
    public class Categoria
    {
        public int ctg_id { get; set; }
        public string ctg_categoria { get; set; }
        public bool ctg_habilitado { get; set; }
        public DateTime ctg_fecha_creacion { get; set; }
        public string filtro { get; set; }
    }
}