using System;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class UsuarioEmpresa
    {
        public int use_id { get; set; }
        public int use_usuario { get; set; }
        public int use_empresa { get; set; }
        public string empresa_rut { get; set; }
        public string empresa_nombre { get; set; }
    }
}