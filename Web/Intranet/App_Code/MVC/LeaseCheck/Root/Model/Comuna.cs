using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class Comuna
    {
        public int cmn_id { get; set; }
        public string cmn_nombre { get; set; }
        public int cmn_pais { get; set; }

        public int cmn_region { get; set; }

        public int cmn_provincia { get; set; }
    }
}