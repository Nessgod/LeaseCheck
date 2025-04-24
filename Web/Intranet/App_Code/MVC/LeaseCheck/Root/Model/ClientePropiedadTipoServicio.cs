using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


[Serializable]
public class ClientePropiedadTipoServicio
{
    public int tsc_id { get; set; }

    public string tsc_nombre { get; set; }

    public int psc_id { get; set; }

    public int psc_producto { get; set; }

    public int psc_servicio { get; set; }
}