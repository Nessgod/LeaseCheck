using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
public class BolsaProducto
{
    public int blp_id { get; set; }
    public int blp_bolsa { get; set; }
    public int blp_producto { get; set; }
    public string producto_nombre { get; set; }
}