using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class Parametros
    {
        public string par_codigo { get; set; }
        public string par_nombre { get; set; }
        public string par_valor { get; set; }

    }
}