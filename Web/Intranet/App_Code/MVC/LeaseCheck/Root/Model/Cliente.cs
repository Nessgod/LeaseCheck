using System;

namespace LeaseCheck.Root.Model
{
    [Serializable]
    public class Cliente
    {
        public int cli_id { get; set; }
        public string cli_nombre { get; set; }
        public string cli_giro { get; set; }
        public int cli_rut { get; set; }
        public string cli_dv { get; set; }
        public bool cli_habilitado { get; set; }
        public string cli_alias { get; set; }
        public int cli_pais { get; set; }
        public int cli_comuna { get; set; }
        public string cli_direccion { get; set; }
        public string cli_email { get; set; }
        public string cli_telefono { get; set; }
        public byte[] cli_logo { get; set; }
        public bool cli_es_demo { get; set; }
        public string cli_contacto_nombre { get; set; }
        public string cli_contacto_email { get; set; }
        public string cli_contacto_telefono { get; set; }


        public string filtro { get; set; }
        public string filtro_Cliente { get; set; }
        public string filtro_Pais { get; set; }

        public int cli_usuario { get; set; }
        public string cli_rut_completo { get; set; }
        public bool cli_tiene_rut { get; set; }
        public string cli_identificador { get; set; }

        public double cli_comision_venta { get; set; }
    }
}