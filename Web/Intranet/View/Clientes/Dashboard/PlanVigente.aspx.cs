using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Telerik.Web.UI;
using System.Globalization;
using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;

public partial class View_Clientes_Dashboard_PlanVigente : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DashboardController controller = new DashboardController();

        if (Convert.ToInt32(LeaseCheck.Session.UsuarioPerfil()) == (int)LeaseCheck.LeaseCheck.Perfiles.AdministradorCorredora)
        {
  
            var a = controller.GetEstadisticaCliente();

            string mesActual = DateTime.Now.ToString("MMMM yyyy", new System.Globalization.CultureInfo("es-CL")).ToUpper();
            string mesAnterior = DateTime.Now.AddMonths(-1).ToString("MMMM yyyy", new System.Globalization.CultureInfo("es-CL")).ToUpper();


            lblFechaActual.Text = "Mes Actual: " + mesActual;
            lblFechaActual1.Text = "Mes Actual: " + mesActual;
            lblFechaActual2.Text = "Mes Actual: " + mesActual;
            lblFechaActual3.Text = "Mes Actual: " + mesActual;
            lblFechaActual4.Text = "Mes Actual: " + mesActual;

            lblFechaAnterior.Text = "Mes Anterior: " + mesAnterior;

            lblPlanVigente.InnerText = a.plan_nombre;
            lblInformesDisponibles.Text = a.plan_propiedad_total.ToString();
            lblInformesConsumidos.Text = (a.plan_propiedad_actual).ToString();
            lblCreadasAnterior.Text = (a.plan_propiedad_antigua).ToString();
            if (!string.IsNullOrEmpty(a.productos))
            {
                var partes = a.productos.Split(',');
                var html = "<ul class='lista-productos'>";

                foreach (var p in partes)
                {
                    html += "<li>" + p.Trim() + "</li>";
                }

                html += "</ul>";
                lblProductos.Text = html;
            }
            else
            {
                lblProductos.Text = "<span class='lista-productos'>No hay productos asignados.</span>";
            }


            hdfConsumoAnterior.Value = a.plan_propiedad_antigua.ToString();
            hdfConsumoActual.Value = a.plan_propiedad_actual.ToString();
            hdfConsumoDisponible.Value = a.plan_propiedad_total.ToString();

        }
    }
}