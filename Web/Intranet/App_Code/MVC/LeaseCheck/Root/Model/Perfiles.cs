using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseCheck.Root.Model
{ 
    [Serializable]
    public class Perfiles
    {
        public int	per_id {get;set;}
        public string per_nombre {get;set;}
        public int per_tipo_perfil { get; set; }
        public string per_tipo_perfil_nombre { get; set; }
        public string per_descripcion {get;set;}
        public bool per_habilitado {get;set;}
        public int per_usuario_creacion {get;set;}
        public DateTime	per_fecha_creacion {get;set;}
        public int per_usuario_act {get;set;}
        public DateTime per_fecha_act { get; set; }

        public string per_filtro { get; set; }

        //Empresa
        public string empresas { get; set; }

        public string tpp_nombre { get; set; }
    }
}
