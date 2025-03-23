using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


[Serializable]
public class ProductoAdicional
{
    public int pra_id { get; set; }
    public string pra_nombre { get; set; }
    public int pra_valor_producto { get; set; }
    public bool pra_habilitado { get; set; }
    public string pra_detalle { get; set; }
    public string filtro { get; set; }
}