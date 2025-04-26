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

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DashboardController controller = new DashboardController();

        if (Convert.ToInt32(LeaseCheck.Session.UsuarioPerfil()) == (int)LeaseCheck.LeaseCheck.Perfiles.AdministradorCorredora)
        {
            hdfVistaCliente.Value = "true";
            wucDashboardClientes.Visible = true;
            wucDashboardComercial.Visible = false;

            var a = controller.GetEstadisticaCliente();
      
            //hdfConsumoAnterior.Value = b.plan_informes_antiguos.ToString();
           // hdfConsumoActual.Value = a.plan_informes_mes_actual.ToString();
            //hdfConsumoDisponible.Value = a.plan_informes_disponibles < 0 ? "0" : a.plan_informes_disponibles.ToString();

        }
      
    }
}