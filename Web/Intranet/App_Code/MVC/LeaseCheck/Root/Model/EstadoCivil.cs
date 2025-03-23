using System;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class EstadoCivil
    {
        public int eci_id { get; set; }
        public string eci_nombre { get; set; }
        public bool? eci_habilitado { get; set; }
        public int eci_usuario_creacion { get; set; }
        public DateTime eci_fecha_creacion { get; set; }
        public int eci_usuario_act { get; set; }
        public DateTime eci_fecha_act { get; set; }

    }
}