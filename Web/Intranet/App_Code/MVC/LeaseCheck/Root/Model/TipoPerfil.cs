using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LeaseCheck.Root.Model
{ 
    [Serializable]
    public class TipoPerfil
    {
        public int	tpp_id {get;set;}
        public string tpp_nombre {get;set;}
        public bool tpp_habilitado {get;set;}
      
    }
}
