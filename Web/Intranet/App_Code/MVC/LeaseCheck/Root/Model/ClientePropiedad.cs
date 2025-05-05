using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


[Serializable]
public class ClientePropiedad
{
    public int cpd_id { get; set; }
    public int cpd_tipo_propiedad { get; set; }
    public int cpd_tipo_servicio { get; set; }
    public int cpd_tipo_entrega { get; set; }
    public DateTime cpd_fecha_entrega { get; set; }
    public string cpd_fecha_entrega_detalle { get; set; }
    public int cpd_estado { get; set; }
    public int cpd_cliente { get; set; }
    public int cpd_pais { get; set; }
    public int cpd_region { get; set; }
    public int cpd_provincia { get; set; }
    public int cpd_comuna { get; set; }
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
    public int cpd_usuario_creacion { get; set; }
    public int cpd_usuario_act { get; set; }
    public DateTime cpd_fecha_creacion { get; set; }
    public DateTime cpd_fecha_act { get; set; }
    public string ESTADO { get; set; }
    public string TIPO_PROPIEDAD { get; set; }
    public string TIPO_SERVICIO { get; set; }
    public string TIPO_ENTREGA { get; set; }    
    public string CLIENTE { get; set; }
    public string PAIS { get; set; }
    public string REGION { get; set; }
    public string COMUNA { get; set; }
    public string PROVINCIA { get; set; }

    public double GANANCIA { get; set; }

    public string OBSERVACION_ESTADO { get; set; }

}