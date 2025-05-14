using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Web.UI.WebControls;

public partial class Portal : System.Web.UI.Page
{
    protected void Page_PreRender(object sender, EventArgs e)
    {
        ClientePropiedadDetallePortalController controller = new ClientePropiedadDetallePortalController();

        var a = controller.GetListadoPropiedadesPortal();

        rptPropiedades.DataSource = a;
        rptPropiedades.DataBind();

    }

    protected void rptPropiedades_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            dynamic item = e.Item.DataItem;

            var imgPropiedad = (Image)e.Item.FindControl("imgPropiedad");
            var lblTitulo = (Label)e.Item.FindControl("lblTitulo");
            var lblPrecio = (Label)e.Item.FindControl("lblPrecio");
            var lblUbicacion = (Label)e.Item.FindControl("lblUbicacion");
            var lblHabitaciones = (Label)e.Item.FindControl("lblHabitaciones");
            var lblBanos = (Label)e.Item.FindControl("lblBanos");
            var lblMetros = (Label)e.Item.FindControl("lblMetros");
            var lblPublicado = (Label)e.Item.FindControl("lblPublicado");
            var lblNombreCorredora = (Label)e.Item.FindControl("lblNombreCorredora");
            var lnkDetalle = (HyperLink)e.Item.FindControl("lnkDetalle");

            if (item.IMAGEN_BINARIA != null && item.IMAGEN_BINARIA.Length > 1)
            {
                string base64String = Convert.ToBase64String(item.IMAGEN_BINARIA);
                imgPropiedad.ImageUrl = "data:image/png;base64," + base64String;
            }
            else
            {
                // Imagen por defecto si viene vacío o es cero
                imgPropiedad.ImageUrl = "~/Imagenes/sin-imagen.png";
            }

            lblTitulo.Text = item.cpd_titulo;
            lblPrecio.Text = string.Format("{0:C0}", item.cpd_valor_venta);
            lblUbicacion.Text = item.UBICACION;
            lblHabitaciones.Text = item.DORMITORIOS.ToString();
            lblBanos.Text = item.BAÑOS.ToString();
            lblMetros.Text = item.SUPERFICIE_TOTAL.ToString();
            DateTime fechaPublicacion = item.cpd_fecha_creacion;
            TimeSpan diferencia = DateTime.Now - fechaPublicacion;

            int dias = (int)diferencia.TotalDays;
            int horas = diferencia.Hours;

            if (dias == 0 && horas == 0)
            {
                lblPublicado.Text = "Publicado hace menos de 1 hora";
            }
            else if (dias == 0)
            {
                lblPublicado.Text = "Publicado hace " + horas + (horas == 1 ? " hora" : " horas");
            }
            else if (horas == 0)
            {
                lblPublicado.Text = "Publicado hace " + dias + (dias == 1 ? " día" : " días");
            }
            else
            {
                lblPublicado.Text = "Publicado hace " + dias + (dias == 1 ? " día y " : " días y ")
                                                  + horas + (horas == 1 ? " hora" : " horas");
            }

            lblNombreCorredora.Text = item.CLIENTE;

            int idPropiedad = item.cpd_id;
            lnkDetalle.NavigateUrl = "DetallePropiedad.aspx?id=" + idPropiedad;

        }
    }

}