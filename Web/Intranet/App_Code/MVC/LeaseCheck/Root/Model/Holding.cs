using System;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class Holding
    {
        public int hol_id { get; set; }
        public string hol_nombre { get; set; }
        public bool hol_habilitado { get; set; }

    }
}