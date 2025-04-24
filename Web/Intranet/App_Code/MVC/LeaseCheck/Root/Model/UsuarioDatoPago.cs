using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


[Serializable]
public class UsuarioDatoPago
{
    public int upd_id { get; set; }
    public int upd_id_usuario { get; set; }
    public int upd_banco { get; set; }
    public int upd_tipo_cuenta { get; set; }
    public string upd_banco_otro { get; set; }
    public string upd_rut_cuenta { get; set; }
    public string upd_titular_cuenta { get; set; }
    public string upd_numero_cuenta { get; set; }

}