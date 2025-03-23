using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class View_Clientes_Dashboard_PlanesPorVencer : System.Web.UI.Page
{
    private ClienteController controller = new ClienteController();

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_planes_por_vencer.Ver;
        LeaseCheck.Token.SecurityManagerVer(ver);
        #endregion

        if (!IsPostBack)
        {
            Grid.AddColumn("cliente", "CLIENTE", "", HorizontalAlign.Left);
            Grid.AddColumn("telefono", "TELEFONO", "", HorizontalAlign.Left);
            Grid.AddColumn("mail", "EMAIL", "", HorizontalAlign.Left);
            Grid.AddColumn("contacto_nombre", "CONTACTO", "", HorizontalAlign.Left);
            Grid.AddColumn("contacto_telefono", "TELEFONO CONTACTO", "", HorizontalAlign.Left);
            Grid.AddColumn("contacto_mail", "EMAIL CONTACTO", "", HorizontalAlign.Left);
            Grid.AddCheckboxColumn("habilitado", "HABILITADO", "5%");
            Grid.AddColumn("plan", "PLAN TARIFARIO", "", HorizontalAlign.Left);
            Grid.AddColumn("vigencia_desde", "VIGENCIA DESDE", "", HorizontalAlign.Left, false, "", "{0:dd-MM-yyyy}");
            Grid.AddColumn("vigencia_hasta", "VIGENCIA HASTA", "", HorizontalAlign.Left, false, "", "{0:dd-MM-yyyy}");
        }
    }

    public void LoadControls(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (sender is RadComboBox2)
            {
                RadComboBox2 ctrl = (RadComboBox2)sender;
                ClienteController clienteController = new ClienteController();
                switch (ctrl.ID)
                {


                    case "cboCliente":
                        ctrl.AppendDataBoundItems = true;
                        ctrl.Items.Add(new RadComboBoxItem("Todos", ""));
                
                        ctrl.EmptyMessage = "Todos";
                        ctrl.DataSource = clienteController.GetClientesUsuario().OrderBy(x => x.cli_nombre);
                        ctrl.DataValueField = "cli_id";
                        ctrl.DataTextField = "cli_nombre";

                        ctrl.DataBind();

                        break;

                    case "cboPais":

                        ctrl.AppendDataBoundItems = true;
                        ctrl.Items.Add(new RadComboBoxItem("Todos", ""));
                        ctrl.EmptyMessage = "Todos";
                        Paises filtro2 = new Paises();
                        UsuarioController usuariosController = new UsuarioController();
                        filtro2.pai_habilitado = true;

                        ctrl.DataSource = usuariosController.GetPaises(filtro2);
                        ctrl.DataValueField = "pai_id";
                        ctrl.DataTextField = "pai_nombre";

                        ctrl.DataBind();
                        break;
                }
            }
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        CargaGrid();
        udPanel.Update();

        LinkButton lnkDescargarPlantilla = (LinkButton)Grid.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("lnkDescargarPlantilla");
        ScriptManager.GetCurrent(Page).RegisterPostBackControl(lnkDescargarPlantilla);
    }

    protected void CargaGrid()
    {
        PlanVencimiento item = new PlanVencimiento();
        RadComboBox2 cboTipo = (RadComboBox2)wucFiltro.FindControl("cboTipo");
        WebControls.Calendar txtDesde = (WebControls.Calendar)wucFiltro.FindControl("txtDesde");
        WebControls.Calendar txtHasta = (WebControls.Calendar)wucFiltro.FindControl("txtHasta");
        RadComboBox2 cboPais = (RadComboBox2)wucFiltro.FindControl("cboPais");

        if (cboTipo.SelectedValue != "")
        {
            switch (cboTipo.SelectedValue)
            {
                case "1":
                    item.filtro_Estado = true;
                    break;

                case "0":
                    item.filtro_Estado = false;
                    break;
            }

        }
        RadComboBox2 cboCliente = (RadComboBox2)wucFiltro.FindControl("cboCliente");
        if (cboCliente.SelectedValue != "") item.filtro_Cliente = cboCliente.SelectedValue;
        if (wucFiltro.Filtro() != null) item.filtro = wucFiltro.Filtro();
        if (txtDesde.Text != "") item.desde = DateTime.Parse(txtDesde.Text);
        if (txtHasta.Text != "") item.hasta = DateTime.Parse(txtHasta.Text);
        if (cboPais.SelectedValue != "") item.filtro_Pais = cboPais.SelectedValue;

        Grid.DataSource = controller.GetClientePlanesVencimiento(item);
        Grid.DataBind();
    }
    protected void LnkGenerar_Click(object SENDER, EventArgs e)
    {
        PlanVencimiento item = new PlanVencimiento();
        RadComboBox2 cboTipo = (RadComboBox2)wucFiltro.FindControl("cboTipo");
        WebControls.Calendar txtDesde = (WebControls.Calendar)wucFiltro.FindControl("txtDesde");
        WebControls.Calendar txtHasta = (WebControls.Calendar)wucFiltro.FindControl("txtHasta");
        RadComboBox2 cboPais = (RadComboBox2)wucFiltro.FindControl("cboPais");

        if (cboTipo.SelectedValue != "")
        {
            switch (cboTipo.SelectedValue)
            {
                case "1":
                    item.filtro_Estado = true;
                    break;

                case "0":
                    item.filtro_Estado = false;
                    break;
            }

        }
        RadComboBox2 cboCliente = (RadComboBox2)wucFiltro.FindControl("cboCliente");
        if (cboCliente.SelectedValue != "") item.filtro_Cliente = cboCliente.SelectedValue;
        if (wucFiltro.Filtro() != null) item.filtro = wucFiltro.Filtro();
        if (txtDesde.Text != "") item.desde = DateTime.Parse(txtDesde.Text);
        if (txtHasta.Text != "") item.hasta = DateTime.Parse(txtHasta.Text);
        if (cboPais.SelectedValue != "") item.filtro_Pais = cboPais.SelectedValue;


        controller.InformePlan(item);
    }
}