using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class View_Clientes_Identidad_ClientePlan : System.Web.UI.Page
{
    protected int Id
    {
        get { return Convert.ToInt32(ViewState["Id"]); }
        set { ViewState.Add("Id", value); }
    }

    protected int Cliente
    {
        get { return Convert.ToInt32(ViewState["Cliente"]); }
        set { ViewState.Add("Cliente", value); }
    }

    private ClienteController controller = new ClienteController();

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_cliente_planes.Ver;
        LeaseCheck.Token.SecurityManagerVer(ver);
        #endregion

        if (!IsPostBack)
        {
            string[] query = Tools.Crypto.Decrypt(Server.UrlDecode(Request.QueryString["query"].ToString())).Split('&');

            foreach (string arr in query)
            {
                string[] array = arr.ToString().Split('=');
                switch (array[0].ToString())
                {
                    case "Id":
                        Id = Int32.Parse(array[1].ToString());
                        break;

                    case "Cliente":
                        Cliente = Int32.Parse(array[1].ToString());
                        break;
                }
            }
            
            Grid.AddColumn("bolsa_nombre", "BOLSA", "", HorizontalAlign.Left);
            Grid.AddColumn("cpb_fecha_creacion", "FECHA CONTRATACION", "", HorizontalAlign.Left);
            Grid.AddColumn("bolsa_cantidad", "CANTIDAD", "", HorizontalAlign.Left);
            Grid.AddColumn("cpb_fecha_hasta", "MAX DURACION", "", HorizontalAlign.Left);
            Grid.AddColumn("bolsa_valor", "VALOR", "", HorizontalAlign.Left, DataFormat: "{0:N0}");
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
            CargaDatos();

        CargaGrid();
    }

    public void LoadControls(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            if (sender is RadComboBox2)
            {
                RadComboBox2 ctrl = (RadComboBox2)sender;
                switch (ctrl.ID)
                {
                    case "cboTipoPlan":
                        ctrl.AppendDataBoundItems = true;
                        ctrl.Items.Add(new RadComboBoxItem("Seleccione", ""));
                        ctrl.DataSource = controller.GetTiposPlanes();
                        ctrl.DataValueField = "tpl_id";
                        ctrl.DataTextField = "tpl_nombre";

                        ctrl.DataBind();
                        break;

                  
                }
            }
        }
    }

    protected void CargaDatos()
    {
        if (Id > 0)
        {
            ClientePlan item = new ClientePlan();
            item.clp_id = Id;
            item = controller.GetClientePlan(item);

            cboTipoPlan.SelectedValue = item.clp_tipo_plan.ToString();
            txtDesde.Value = item.clp_fecha_desde;
            txtHasta.Value = item.clp_fecha_hasta;
      

            TxtValorPlan.Value = item.clp_valor_plan;
   

            pnlBolsa.Visible = true;
        }

        txtDesde.ReadOnly = true;
        txtHasta.ReadOnly = true;
        cboTipoPlan.ReadOnly = true;
        TxtValorPlan.ReadOnly = true;

    }

    protected void Grid_ItemDataBound(object sender, GridItemEventArgs e)
    {

    }

    protected void CargaGrid()
    {
        if (Id > 0)
        {
            ClientePlanBolsa item = new ClientePlanBolsa();
           

            var listado = controller.GetPlanBolsas(item);
            hdfTotalBalsa.Value = listado.Select(x => x.bolsa_valor).Sum().ToString();
            lblTotalBolsa.Text = "$" + Tools.Formato.Miles(listado.Select(x => x.bolsa_valor).Sum().ToString());

            Grid.DataSource = listado;
            Grid.DataBind();

            calculo();
        }
        else
            hdfTotalBalsa.Value = "0";
    }

    protected void calculo()
    {
        int valor_plan = 0;
        int total_bolsa = 0;

        valor_plan = Convert.ToInt32(TxtValorPlan.Value);


        total_bolsa = int.Parse(hdfTotalBalsa.Value);
        lblTotal.Text = "$" + Tools.Formato.Miles((valor_plan + total_bolsa).ToString());
        hdfTotal.Value = (valor_plan + total_bolsa).ToString();
    }
}