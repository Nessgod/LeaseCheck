using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


[Serializable]
public class ClientePropiedadDetallePotal
{
    public int cpd_id { get; set; }
    public DateTime cpd_fecha_entrega { get; set; }
    public string cpd_fecha_entrega_detalle { get; set; }
    public int cpd_cliente { get; set; }
    public string cpd_calle { get; set; }
    public string cpd_numero_propiedad { get; set; }
    public string cpd_titulo { get; set; }
    public int cpd_valor_uf { get; set; }
    public int cpd_valor_venta { get; set; }
    public int cpd_valor_evaluo_fiscal { get; set; }

    public bool cpd_contribucciones { get; set; }
    public bool cpd_derecho_municipal { get; set; }
    public bool cpd_estacionamiento { get; set; }
    public bool cpd_bodega { get; set; }
    public int cpd_valor_estacionamiento { get; set; }
    public int cpd_valor_bodega { get; set; }
    public int cpd_cantidad_estacionamiento { get; set; }
    public int cpd_cantidad_bodega { get; set; }
    public string ESTADO { get; set; }
    public string TIPO_PROPIEDAD { get; set; }
    public string TIPO_SERVICIO { get; set; }
    public string TIPO_ENTREGA { get; set; }    
    public string CLIENTE { get; set; }
    public string PAIS { get; set; }
    public string REGION { get; set; }
    public string COMUNA { get; set; }
    public string PROVINCIA { get; set; }

    public string OBSERVACION_ESTADO { get; set; }



    // PORTAL

    public int PISOS { get; set; }
    public int BAÑOS { get; set; }
    public int DORMITORIOS { get; set; }
    public string SUPERFICIE_UTIL { get; set; }
    public string SUPERFICIE_TOTAL { get; set; }
    public byte[] IMAGEN_BINARIA { get; set; }
    public string UBICACION { get; set; }

    public string cpf_tipo_piso { get; set; }

    public string cpf_tipo_ventana { get; set; }
    public string cpf_conexion_cocina { get; set; }

    public string cpf_conexion_lavadora { get; set; }

    public bool cpf_quincho { get; set; }

    public bool cpf_piscina { get; set; }

    public bool cpf_calefaccion { get; set; }

    public bool cpf_salon_multiple { get; set; }
    public bool cpf_gimnasio { get; set; }

    public string cdp_conectividad { get; set; }

    public string cdp_centro_comercial { get; set; }

    public string cdp_servicio_salud { get; set; }

    public string cdp_educacion { get; set; }

    public string cdp_area_verde { get; set; }

    public string cdp_seguridad { get; set; }

    public string cdp_restaurant { get; set; }

    public string cdp_transporte { get; set; }
    public string cdp_descripcion { get; set; }

}