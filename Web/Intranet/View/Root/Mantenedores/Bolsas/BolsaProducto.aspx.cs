using LeaseCheck.Root.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class View_Root_Mantenedores_Bolsas_BolsaProducto : System.Web.UI.Page
{
    protected int IdBolsa
    {
        get { return Convert.ToInt32(ViewState["IdBolsa"]); }
        set { ViewState.Add("IdBolsa", value); }
    }
    private BolsaController controller = new BolsaController();
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

                    case "IdBolsa":
                        IdBolsa = Int32.Parse(array[1].ToString());
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
                        BolsaController bolsaController = new BolsaController();
                        BolsaProducto filtro = new BolsaProducto();
                        filtro.blp_bolsa = IdBolsa;

                        ctrl.EmptyMessage = "Seleccione...";
                        ctrl.DataSource = controller.GetListadoAgregar(filtro);
                        ctrl.DataTextField = "producto_nombre";
                        ctrl.DataValueField = "blp_producto";
                        ctrl.DataBind();

                        break;
                }
            }
        }
    }
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        Respuesta respuesta = new Respuesta();
        BolsaProducto bolsaProducto = new BolsaProducto();
        bolsaProducto.blp_bolsa = IdBolsa;
        bolsaProducto.blp_producto = int.Parse(cboProducto.SelectedValue);

        respuesta = controller.InsertBolsaProducto(bolsaProducto);

        if (!respuesta.error)
            Tools.tools.ClientAlert(respuesta.detalle, "ok", true);
        else
            Tools.tools.ClientAlert(respuesta.detalle, "alerta");
    }
}