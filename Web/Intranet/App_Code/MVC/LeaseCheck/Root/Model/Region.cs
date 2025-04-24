using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class Region
    {
        public int rgn_id { get; set; }
        public string rgn_nombre { get; set; }

        public int rgn_pais { get; set; }
    }
}