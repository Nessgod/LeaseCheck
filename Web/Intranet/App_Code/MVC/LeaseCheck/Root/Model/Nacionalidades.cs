using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class Nacionalidades
    {
		public int nac_id { get; set; }
		public string nac_nombre { get; set; }		
		public bool nac_habilitado { get; set; }
		public int nac_usuario_creacion { get; set; }
		public DateTime nac_fecha_creacion { get; set; }
		public int nac_usuario_act { get; set; }
		public DateTime nac_fecha_act { get; set; }
	}
}