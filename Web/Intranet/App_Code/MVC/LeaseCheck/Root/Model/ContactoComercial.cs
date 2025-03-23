using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[Serializable]
public class ContactoComercial
{
    public string nombre { get; set; }
    public string empresa { get; set; }
    public string cargo { get; set; }
    public string telefono { get; set; }
    public string correo { get; set; }
    public string mensaje { get; set; }
}