using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class View_Clientes_Dashboard_VencimientoPlanes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDesde.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            txtHasta.Value = (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1)).AddMonths(1).AddDays(-1);
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        DashboardController controller = new DashboardController();
        Dashboard filtro = new Dashboard();

        //if (Convert.ToInt32(LeaseCheck.Session.UsuarioPerfil()) == (int)LeaseCheck.LeaseCheck.Perfiles.AdminComercial ||
        //     Convert.ToInt32(LeaseCheck.Session.UsuarioPerfil()) == (int)LeaseCheck.LeaseCheck.Perfiles.Comercial)
        //{

        filtro.filtro_desde = txtDesde.Value.Value;
        filtro.filtro_hasta = txtHasta.Value.Value;

        if (wucFiltro.Filtro() != null) filtro.plan_nombre = wucFiltro.Filtro();
        //if (!string.IsNullOrEmpty(cboCliente.SelectedValue)) filtro.cliente_id = int.Parse(cboCliente.SelectedValue);

        hdfCargo1.Value = string.Empty;
        hdfCantidad1.Value = string.Empty;
        //lblVencimiento1.Text = string.Empty;
        //lblPlan1.Text = string.Empty;
        //lblFecha1.Text = string.Empty;

        hdfCargo2.Value = string.Empty;
        hdfCantidad2.Value = string.Empty;
        //lblVencimiento2.Text = string.Empty;
        //lblPlan2.Text = string.Empty;
        //lblFecha2.Text = string.Empty;

        hdfCargo3.Value = string.Empty;
        hdfCantidad3.Value = string.Empty;
        //lblVencimiento3.Text = string.Empty;
        //lblPlan3.Text = string.Empty;
        //lblFecha3.Text = string.Empty;

        var a = controller.GetDashComercialPlan(filtro);

        rpt.DataSource = a;
        rpt.DataBind();

        if (a.Count > 0)
        {
            hdfCargo1.Value = a[0].cliente_nombre;
            hdfCantidad1.Value = a[0].plan_informes_antiguos.ToString();

            //lblVencimiento1.Text = a[0].cliente_nombre;
            //lblPlan1.Text = a[0].plan_nombre;
            //lblFecha1.Text = a[0].cliente_vencimiento;
        }

        if (a.Count > 1)
        {
            hdfCargo2.Value = a[1].cliente_nombre;
            hdfCantidad2.Value = a[1].plan_informes_antiguos.ToString();

            //lblVencimiento2.Text = a[1].cliente_nombre;
            //lblPlan2.Text = a[1].plan_nombre;
            //lblFecha2.Text = a[1].cliente_vencimiento;
        }

        if (a.Count > 2)
        {
            hdfCargo3.Value = a[2].cargo_nombre;
            hdfCantidad3.Value = a[2].plan_informes_antiguos.ToString();

            //lblVencimiento3.Text = a[2].cliente_nombre;
            //lblPlan3.Text = a[2].plan_nombre;
            //lblFecha3.Text = a[2].cliente_vencimiento;
        }
        //}
    }



    protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string cliente_nombre = (string)DataBinder.Eval(e.Item.DataItem, "cliente_nombre");
            string plan_nombre = (string)DataBinder.Eval(e.Item.DataItem, "plan_nombre");
            string cliente_vencimiento = (string)DataBinder.Eval(e.Item.DataItem, "cliente_vencimiento");

            Label lblVencimiento = (Label)e.Item.FindControl("lblVencimiento");
            Label lblPlan = (Label)e.Item.FindControl("lblPlan");
            Label lblFecha = (Label)e.Item.FindControl("lblFecha");

            lblVencimiento.Text = cliente_nombre;
            lblPlan.Text = plan_nombre;
            lblFecha.Text = DateTime.Parse(cliente_vencimiento).ToShortDateString();
        }
    }
}