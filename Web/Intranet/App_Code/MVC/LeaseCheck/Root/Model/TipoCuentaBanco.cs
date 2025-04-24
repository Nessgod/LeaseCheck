using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class TipoCuentaBanco
    {
        public int tpc_id { get; set; }
        public string tpc_nombre { get; set; }

        public int tpc_banco { get; set; }
    }
}