using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class Provincia
    {
        public int pro_id { get; set; }
        public string pro_nombre { get; set; }

        public int pro_region { get; set; }
    }
}