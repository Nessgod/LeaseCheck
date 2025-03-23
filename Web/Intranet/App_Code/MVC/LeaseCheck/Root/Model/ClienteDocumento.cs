using System;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class ClienteDocumento
    {
        public int cdo_id { get; set; }
        public int cdo_id_cliente { get; set; }
        public int? cdo_documento { get; set; }
        public string cdo_descripcion { get; set; }

        public int cdo_usuario_creacion { get; set; }
        public DateTime cdo_fecha_creacion { get; set; }

        public int arc_id { get; set; }
        public string arc_nombre_archivo { get; set; }

        public string arc_contenido { get; set; }
        public string arc_extension { get; set; }
        public int arc_tamano { get; set; }
        public int arc_archivo { get; set; }
        public int abi_id { get; set; }   
        public byte[] abi_archivo_binario { get; set; }

        public string nombre_usuario_creacion { get; set; }
    }
}