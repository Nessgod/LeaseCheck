using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseCheck.Root.Model
{
	[Serializable]
	public class Paises
	{
		public int pai_id { get; set; }
		public string pai_nombre { get; set; }
		public string pai_suma_resta { get; set; }
		public int pai_hora { get; set; }                            
		public bool pai_habilitado { get; set; }
		public int pai_usuario_creacion { get; set; }
		public DateTime pai_fecha_creacion { get; set; }
		public int pai_usuario_act { get; set; }
		public DateTime pai_fecha_act { get; set; }
		public byte[] pai_imagen { get; set; }
		public string pai_nombre_imagen { get; set; }
		public string pai_extension { get; set; }
	}
}