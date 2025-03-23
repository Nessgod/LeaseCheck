using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseCheck.Clientes.Model
{
    [Serializable]
    public class NivelCargo
    {
        public int nca_id { get; set; }
        public string nca_nombre { get; set; }
        public bool nca_habilitado { get; set; }
    }
}