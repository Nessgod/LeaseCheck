using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
public class PlanProducto
{
    public int plp_id { get; set; }
    public int plp_tipo_plan { get; set; }
    public int plp_producto { get; set; }

    public int plp_documento { get; set; }
    public string producto_nombre { get; set; }

    public string documento_nombre { get; set; }

}