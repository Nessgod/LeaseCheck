using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class PreguntaTipo
    {
        public int pti_id { get; set; }
        public string pti_descripcion { get; set; }
    }
}