using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class Bolsa
    {
        public int bls_id { get; set; }
        public string bls_nombre { get; set; }
        public int bls_cantidad { get; set; }
        public int bls_valor_plan { get; set; }
        public bool bls_habilitado { get; set; }
        public string filtro { get; set; }
        public int bls_cantidad_administradores { get; set; }
        public bool bls_administradores_ilimitados { get; set; }
    }
}