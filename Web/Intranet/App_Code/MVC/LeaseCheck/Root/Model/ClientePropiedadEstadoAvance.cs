using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


[Serializable]
public class ClientePropiedadEstadoAvance
{
    public int cea_id { get; set; }
    public int cea_id_propiedad { get; set; }
    public int cea_id_estado { get; set; }

    public int cea_usuario_creacion { get; set; }
    public DateTime cea_fecha_creacion { get; set; }

    public string NOMBRE_ESTADO { get; set; }

    public string NOMBRE_COMPLETO { get; set; }

    public string OBSERVACION_ESTADO { get; set; }
}


