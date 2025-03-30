using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;


public partial class View_Root_Mantenedores_Cliente_Controls_Clientes : System.Web.UI.UserControl
{
    public bool ReadOnly
    {
        get { return Convert.ToBoolean(ViewState["ReadOnly"]); }
        set { ViewState.Add("ReadOnly", value); }
    }
    public int IdCliente
    {
        get { return Convert.ToInt32(ViewState["IdCliente"]); }
        set { ViewState.Add("IdCliente", value); }
    }

    public string URLNuevoCliente
    {
        get { return Convert.ToString(ViewState["URLNuevoCliente"]); }
        set { ViewState.Add("URLNuevoCliente", value); }
    }


    public bool TieneRut { get; set; }

    private ClienteController controller = new ClienteController();

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_cliente.Ver;
        LeaseCheck.Token.SecurityManagerVer(ver);
        #endregion

        if (!IsPostBack)
        {
            Grid.AddSelectColumn();
            Grid.AddColumn("cli_id", "", Align: HorizontalAlign.Center);
            Grid.AddColumn("cli_id", "ID", "", Align: HorizontalAlign.Center);
            Grid.AddColumn("cli_rut_completo", "RUT/IDENTIFICADOR", "", Align: HorizontalAlign.Center, HeaderAlign: HorizontalAlign.Center);
            Grid.AddColumn("cli_nombre", "NOMBRE", "", HorizontalAlign.Left);
            Grid.AddColumn("cli_alias", "ALIAS", "", HorizontalAlign.Left);
            Grid.AddColumn("cli_telefono", "TELÉFONO", "", HorizontalAlign.Left);
            Grid.AddColumn("cli_email", "EMAIL", "", HorizontalAlign.Left);
            Grid.AddCheckboxColumn("cli_es_demo", "ES DEMO O PRUEBAS");
            Grid.AddCheckboxColumn("cli_habilitado", "HABILITADO");
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


                    case "cboCliente":
                        ctrl.AppendDataBoundItems = true;
                        ctrl.Items.Add(new RadComboBoxItem("Todos", ""));

                        ClienteController clienteController = new ClienteController();
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
        LinkButton LnkDescargarPlantilla = (LinkButton)Grid.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("lnkDescargarPlantilla");
        //script que genera el excel 
        //ScriptManager.GetCurrent(Page).RegisterPostBackControl(LnkDescargarPlantilla);
    }

    protected void CargaGrid()
    {
        RadComboBox2 cboCliente = (RadComboBox2)wucFiltro.FindControl("cboCliente");
        Cliente cliente = new Cliente();
        RadComboBox2 cboPais = (RadComboBox2)wucFiltro.FindControl("cboPais");
        if (wucFiltro.Filtro() != "") cliente.filtro = wucFiltro.Filtro();
        //if (cboCliente.SelectedValue != "") cliente.filtro_Cliente = cboCliente.SelectedValue;
        if (cboPais.SelectedValue != "") cliente.filtro_Pais = cboPais.SelectedValue;

        Grid.DataSource = controller.GetClientes(cliente);
        Grid.DataBind();
    }

    protected void Grid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
        {
            if (((e.Item) is GridDataItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                string id = item.GetDataKeyValue("cli_id").ToString();
                string query = Server.UrlEncode(Tools.Crypto.Encrypt("IdCliente=" + id + "&ReadOnly=" + ReadOnly));

                HyperLink Editar = new HyperLink();
                Editar.ID = "lnk" + id;
                Editar.CssClass = "icono_Editar";
                Editar.NavigateUrl = "javascript:void(0)";
                Editar.Attributes.Add("onclick", "abrirClientes('" + query + "')");

                GridDataItem DataItem = e.Item as GridDataItem;
                TableCell dca_id = DataItem["cli_id"];
                dca_id.Controls.Add(Editar);
            }
        }
    }

    protected void lnkEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            if (Grid.SelectedIndexes.Count == 0)
            {
                Tools.tools.ClientAlert("Debe seleccionar al menos un registro.", "alerta");
                CargaGrid();
            }
            else
            {
                Respuesta respuesta = new Respuesta();

                foreach (string item in Grid.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = Grid.MasterTableView.DataKeyValues[Int32.Parse(item)];
                    int id = Int32.Parse(value["cli_id"].ToString());

                    Cliente cliente = new Cliente();
                    cliente.cli_id = id;

                    respuesta = controller.DeleteCliente(cliente);
                }

                if (!respuesta.error)
                    Tools.tools.ClientAlert(respuesta.detalle, "ok");
                else
                    Tools.tools.ClientAlert(respuesta.detalle, "alerta");
            }
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.Message, "error");
        }
    }

    protected void LnkGenerar_Click(object SENDER, EventArgs e)
    {
        Cliente cliente = new Cliente();
        if (wucFiltro.Filtro() != "") cliente.filtro = wucFiltro.Filtro();
        RadComboBox2 cboCliente = (RadComboBox2)wucFiltro.FindControl("cboCliente");
        if (cboCliente.SelectedValue != "") cliente.filtro_Cliente = cboCliente.SelectedValue;

        controller.InformeCliente(cliente);
    }

}