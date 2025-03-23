using System;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class UsuarioHolding
    {
        public int uho_id { get; set; }
        public int uho_usuario { get; set; }
        public int uho_holding { get; set; }
        public string holding_nombre { get; set; }
    }
}