using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
public class PlanProductoDocumento
{
    public int tdc_id { get; set; }
    public string tdc_nombre { get; set; }

    public int prd_id{ get; set; }
    public int prd_producto { get; set; }

    public int prd_tipo_documento { get; set; }
    
}