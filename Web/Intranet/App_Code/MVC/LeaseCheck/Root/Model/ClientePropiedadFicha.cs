using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


[Serializable]
public class ClientePropiedadFicha
{
    public int cpf_id { get; set; }
    public int cpf_id_propiedad { get; set; }
  
    public string cpf_superficie_util { get; set; }

    public string cpf_superficie_total { get; set; }

    public int cpf_dormitorio { get; set; }
    public int cpf_baño { get; set; }

    public int cpf_pisos { get; set; }

    public int cpf_ubicacion_piso { get; set; }

    public string cpf_tipo_piso { get; set; }

    public string cpf_tipo_ventana { get; set; }
    public string cpf_conexion_cocina { get; set; }

    public string cpf_conexion_lavadora { get; set; }

    public bool cpf_quincho { get; set; }

    public bool cpf_piscina { get; set; }

    public bool cpf_calefaccion { get; set; }

    public bool cpf_salon_multiple { get; set; }
    public bool cpf_gimnasio { get; set; }

    public int cpf_usuario_creacion { get; set; }

    public int cpf_usuario_act { get; set; }

    public DateTime cpf_fecha_creacion { get; set; }
    public DateTime cpf_fecha_act { get; set; }

















}