using System;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class JornadaLaboral
    {
        public int jol_id { get; set; }
        public string jol_nombre { get; set; }
        public bool jol_habilitado { get; set; }
    }
}