using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Web.UI.WebControls;

public partial class DetallePropiedad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int idPropiedad;
            if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out idPropiedad))
            {
                CargarDetallePropiedad(idPropiedad);
            }
            else
            {
                Response.Redirect("Portal.aspx");
            }
        }
    }


    private void CargarDetallePropiedad(int id)
    {
        ClientePropiedadDetallePortalController controller = new ClientePropiedadDetallePortalController();
        ClientePropiedadDetallePotal propiedad = controller.GetClientePropiedadPortal(id);

        if (propiedad != null)
        {
            // Datos generales
            lblTituloPropiedad.Text = propiedad.cpd_titulo;
            lblPrecio.Text = string.Format("{0:C0}", propiedad.cpd_valor_venta);
            lblUbicacion.Text = propiedad.UBICACION;
            lblTipoPropiedad.Text = propiedad.TIPO_PROPIEDAD;
            lblTipoEntrega.Text = propiedad.TIPO_ENTREGA;

            // Superficies y distribución
            lblMetrosUtiles.Text = propiedad.SUPERFICIE_UTIL;
            lblMetrosTotales.Text = propiedad.SUPERFICIE_TOTAL;
            lblDormitorios.Text = propiedad.DORMITORIOS.ToString();
            lblBanos.Text = propiedad.BAÑOS.ToString();
            lblPisos.Text = propiedad.PISOS.ToString();

            // Estacionamiento y bodega
            lblEstacionamiento.Text = propiedad.cpd_estacionamiento ? "Sí" : "No";
            lblBodega.Text = propiedad.cpd_bodega ? "Sí" : "No";

            // Características técnicas
            lblTipoPiso.Text = propiedad.cpf_tipo_piso;
            lblTipoVentana.Text = propiedad.cpf_tipo_ventana;
            lblConexionCocina.Text = propiedad.cpf_conexion_cocina;
            lblConexionLavadora.Text = propiedad.cpf_conexion_lavadora;

            // Características adicionales (marcar si están habilitadas)
            lblQuincho.Text = propiedad.cpf_quincho ? "✔ Quincho" : "";
            lblPiscina.Text = propiedad.cpf_piscina ? "✔ Piscina" : "";
            lblCalefaccion.Text = propiedad.cpf_calefaccion ? "✔ Calefacción" : "";
            lblGimnasio.Text = propiedad.cpf_gimnasio ? "✔ Gimnasio" : "";
            lblSalonMultiple.Text = propiedad.cpf_salon_multiple ? "✔ Salón Múltiple" : "";

            // Entorno y publicación
            lblDescripcion.Text = propiedad.cdp_descripcion;
            lblConectividad.Text = propiedad.cdp_conectividad;
            lblCentroComercial.Text = propiedad.cdp_centro_comercial;
            lblSalud.Text = propiedad.cdp_servicio_salud;
            lblEducacion.Text = propiedad.cdp_educacion;
            lblAreasVerdes.Text = propiedad.cdp_area_verde;
            lblRestaurant.Text = propiedad.cdp_restaurant;
            lblSeguridad.Text = propiedad.cdp_seguridad;
            lblTransporte.Text = propiedad.cdp_transporte;

            // Imagen principal
            if (propiedad.IMAGEN_BINARIA != null && propiedad.IMAGEN_BINARIA.Length > 1)
            {
                string base64 = Convert.ToBase64String(propiedad.IMAGEN_BINARIA);
                imgPrincipal.ImageUrl = "data:image/png;base64," + base64;
            }
            else
            {
                imgPrincipal.ImageUrl = "~/Imagenes/sin-imagen.png";
            }

            // Galería adicional (si tienes)
            // rptGaleria.DataSource = propiedad.GALERIA;
            // rptGaleria.DataBind();
        }
        else
        {
            Response.Redirect("Portal.aspx");
        }
    }

}
