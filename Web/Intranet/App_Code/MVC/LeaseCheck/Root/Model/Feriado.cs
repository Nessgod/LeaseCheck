using System;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class Feriado
    {
        public int frd_id { get; set; }
        public int frd_pais { get; set; }
        public string frd_descripcion { get; set; }
        public DateTime frd_fecha_feriados { get; set; }
        public string pai_nombre { get; set; }

        public DateTime? desde { get; set; }
        public DateTime? hasta { get; set; }

    }
}