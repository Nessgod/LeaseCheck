using System;
using System.Collections.Generic;

namespace LeaseCheck.Model
{
    [Serializable]
    public class ClienteInstalacion
    {
        public int cin_id { get; set; }
        public int cin_cliente{ get; set; }
        public string cin_nombre { get; set; }
        public string cin_descripcion { get; set; }
        public string cin_direccion { get; set; }

        public string cin_telefono { get; set; }
        public bool cin_habilitado { get; set; }
        public int cin_usuario_creacion { get; set; }
        public DateTime cin_fecha_creacion { get; set; }
        public int cin_usuario_actualizacion { get; set; }
        public DateTime cin_fecha_actualizacion{ get; set; }

        public string filtro { get; set; }
        public int usuario { get; set; }


    }
}