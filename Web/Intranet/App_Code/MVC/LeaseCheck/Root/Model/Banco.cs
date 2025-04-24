using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class Banco
    {
        public int bnc_id { get; set; }
        public string bnc_nombre { get; set; }

        public string bnc_tipo { get; set; }
    }
}