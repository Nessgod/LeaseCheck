using System;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class ClientePropiedadMedio
    {
        public int cpm_id { get; set; }
        public int cpm_id_propiedad { get; set; }
        public int? cpm_id_archivo { get; set; }
        public string cpm_link { get; set; }
        public string cpm_descripcion { get; set; }
        public bool cpm_imagen { get; set; }
        public bool cpm_video { get; set; }

    }

    [Serializable]
    public class ClientePropiedadMedioBinario
    {
        public int cmb_id { get; set; }
        public byte[] cmb_binario { get; set; }
        public string DESCRIPCION { get; set; }

    }
}