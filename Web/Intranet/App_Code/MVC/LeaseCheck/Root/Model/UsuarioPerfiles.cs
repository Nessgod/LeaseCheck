using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class UsuarioPerfiles
    {
        public int upe_id { get; set; }
		public int upe_usuario { get; set; }
		public int upe_perfil { get; set; }
        public bool upe_estado { get; set; }
        public string perfilNombre { get; set; }
        public string ids_perfiles { get; set; }
        public bool left { get; set; }

        public string perfil_nombre { get; set; }
        public bool perfil_habilitado { get; set; }
    }
}
