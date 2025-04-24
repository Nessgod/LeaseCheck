using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class View_Root_Mantenedores_Productos_ProductoPlanDocumento : System.Web.UI.Page
{
    protected int IdProducto
    {
        get { return Convert.ToInt32(ViewState["IdProducto"]); }
        set { ViewState.Add("IdProducto", value); }
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

                    case "IdProducto":
                        IdProducto = Int32.Parse(array[1].ToString());
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
                    case "cboDocumento":
                        PlanProductoController planProductoController = new PlanProductoController();
                        PlanProductoDocumento filtro = new PlanProductoDocumento();
                        filtro.prd_producto = IdProducto;

                        ctrl.EmptyMessage = "Seleccione...";
                        ctrl.DataSource = planProductoController.GetListadoAgregarDocumento(filtro);
                        ctrl.DataTextField = "tdc_nombre";
                        ctrl.DataValueField = "tdc_id";
                        ctrl.DataBind();

                        break;

                }
            }
        }
    }
    
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Respuesta respuesta = new Respuesta();
        PlanProductoDocumento planProducto = new PlanProductoDocumento();
        planProducto.prd_producto = IdProducto;
        planProducto.prd_tipo_documento = int.Parse(cboDocumento.SelectedValue);

        respuesta = controller.InsertProductoDocumento(planProducto);

        if (!respuesta.error)
            Tools.tools.ClientAlert(respuesta.detalle, "ok", true);
        else
            Tools.tools.ClientAlert(respuesta.detalle, "alerta");
    }
}