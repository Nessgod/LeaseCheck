using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class View_Root_Mantenedores_TipoPlan_ProductoPlan : System.Web.UI.Page
{
    protected int IdPlan
    {
        get { return Convert.ToInt32(ViewState["IdPlan"]); }
        set { ViewState.Add("IdPlan", value); }
    }
    
    private PlanProductoController controller = new PlanProductoController();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string[] query = Tools.Crypto.Decrypt(Server.UrlDecode(Request.QueryString["query"].ToString())).Split('&');

            foreach (string arr in query)
            {
                string[] array = arr.ToString().Split('=');
                switch (array[0].ToString())
                {

                    case "IdPlan":
                        IdPlan = Int32.Parse(array[1].ToString());
                        break;
                }
            }
        }
    }
    
    public void LoadControls(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (sender is RadComboBox2)
            {
                RadComboBox2 ctrl = (RadComboBox2)sender;
                switch (ctrl.ID)
                {
                    case "cboProducto":
                        PlanProductoController planProductoController = new PlanProductoController();
                        PlanProducto filtro = new PlanProducto();
                        filtro.plp_tipo_plan = IdPlan;

                        ctrl.EmptyMessage = "Seleccione...";
                        ctrl.DataSource = planProductoController.GetListadoAgregar(filtro);
                        ctrl.DataTextField = "producto_nombre";
                        ctrl.DataValueField = "plp_producto";
                        ctrl.DataBind();

                        break;

                }
            }
        }
    }
    
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Respuesta respuesta = new Respuesta();
        PlanProducto planProducto = new PlanProducto();
        planProducto.plp_tipo_plan = IdPlan;
        planProducto.plp_producto = int.Parse(cboProducto.SelectedValue);

        respuesta = controller.InsertPlanProducto(planProducto);

        if (!respuesta.error)
            Tools.tools.ClientAlert(respuesta.detalle, "ok", true);
        else
            Tools.tools.ClientAlert(respuesta.detalle, "alerta");
    }
}