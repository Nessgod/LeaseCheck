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

        if (Convert.ToInt32(LeaseCheck.Session.UsuarioPerfil()) == (int)LeaseCheck.LeaseCheck.Perfiles.Administrador ||
            Convert.ToInt32(LeaseCheck.Session.UsuarioPerfil()) == (int)LeaseCheck.LeaseCheck.Perfiles.Administrativo ||
            Convert.ToInt32(LeaseCheck.Session.UsuarioPerfil()) == (int)LeaseCheck.LeaseCheck.Perfiles.Psicologo)
        {
  
            var a = controller.GetEstadisticaCliente();

            lblPlanVigente.Text = a.plan_nombre;
            lblInformesDisponibles.Text = a.plan_informes_disponibles.ToString();
            lblInformesConsumidos.Text = (a.plan_informes_mes_actual).ToString();

            //hdfConsumoAnterior.Value = b.plan_informes_antiguos.ToString();
            hdfConsumoActual.Value = a.plan_informes_mes_actual.ToString();
            hdfConsumoDisponible.Value = a.plan_informes_disponibles.ToString();
        }
    }
}