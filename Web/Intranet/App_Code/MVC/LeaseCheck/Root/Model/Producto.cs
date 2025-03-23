using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


[Serializable]
public class Producto
{
    public int pro_id { get; set; }
    public string pro_producto { get; set; }
    public bool pro_habilitado { get; set; }
    public string filtro { get; set; }
}