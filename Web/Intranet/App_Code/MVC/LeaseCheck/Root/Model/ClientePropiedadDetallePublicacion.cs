using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


[Serializable]
public class ClientePropiedadDetallePublicacion
{
    public int cdp_id { get; set; }
    public int cdp_id_propiedad { get; set; }
  
    public string cdp_conectividad { get; set; }

    public string cdp_centro_comercial { get; set; }

    public string cdp_servicio_salud { get; set; }

    public string cdp_educacion { get; set; }

    public string cdp_area_verde { get; set; }

    public string cdp_seguridad { get; set; }

    public string cdp_restaurant { get; set; }

    public string cdp_transporte { get; set; }
    public string cdp_descripcion { get; set; }

    public int cdp_usuario_creacion { get; set; }

    public int cdp_usuario_act { get; set; }

    public DateTime cdp_fecha_creacion { get; set; }
    public DateTime cdp_fecha_act { get; set; }

















}