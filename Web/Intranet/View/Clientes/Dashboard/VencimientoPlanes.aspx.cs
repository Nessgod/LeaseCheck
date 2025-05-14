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
          
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        DashboardController controller = new DashboardController();
        Dashboard filtro = new Dashboard();

        var a = controller.GetDashComercialPlan(filtro);

        rpt.DataSource = a;
        rpt.DataBind();
     
    }



    protected void rpt_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string cliente_nombre = (string)DataBinder.Eval(e.Item.DataItem, "cliente_nombre");
            string plan_nombre = (string)DataBinder.Eval(e.Item.DataItem, "plan_nombre");
            string cliente_vencimiento = (string)DataBinder.Eval(e.Item.DataItem, "cliente_vencimiento");
            int plan_propiedad_total = (int)DataBinder.Eval(e.Item.DataItem, "plan_propiedad_total");

            Label lblVencimiento = (Label)e.Item.FindControl("lblVencimiento");
            Label lblPlan = (Label)e.Item.FindControl("lblPlan");
            Label lblFecha = (Label)e.Item.FindControl("lblFecha");
            Label lblPropiedades = (Label)e.Item.FindControl("lblPropiedades");

            lblVencimiento.Text = cliente_nombre;
            lblPlan.Text = string.IsNullOrEmpty(plan_nombre) ? "Sin plan asignado" : plan_nombre;

            DateTime fechaVencimiento;
            if (DateTime.TryParse(cliente_vencimiento, out fechaVencimiento))
            {
                lblFecha.Text = fechaVencimiento.ToShortDateString();
            }
            else
            {
                lblFecha.Text = "Sin Fecha";
            }

            lblPropiedades.Text = (plan_propiedad_total == 0) ? "Sin propiedades" : plan_propiedad_total.ToString();

        }
    }

}