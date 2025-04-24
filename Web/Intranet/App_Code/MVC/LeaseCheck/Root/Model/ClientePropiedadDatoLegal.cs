using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


[Serializable]
public class ClientePropiedadDatoLegal
{
    public int cdl_id { get; set; }
    public int cdl_id_propiedad { get; set; }
    public int cdl_id_propietario { get; set; }
    public string cdl_fajas { get; set; }
    public string cdl_numero_inscripcion { get; set; }
    public int cdl_anio_inscripcion { get; set; }
    public string cdl_numero_manzana { get; set; }
    public string cdl_numero_sitio { get; set; }
    public string cdl_conjunto_habitacional { get; set; }
    public bool cdl_inventario { get; set; }
    public int cdl_copia_llaves { get; set; }

}

