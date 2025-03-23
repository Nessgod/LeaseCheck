using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
public class ClienteProductoAdicional
{
    public int cpa_id { get; set; }
    public int cpa_cliente { get; set; }
    public int cpa_producto_adicional { get; set; }
    public DateTime cpa_fecha_desde { get; set; }
    public DateTime cpa_fecha_hasta { get; set; }
    public int cpa_valor_producto_adicional { get; set; }

    public int valor_producto { get; set; }
}