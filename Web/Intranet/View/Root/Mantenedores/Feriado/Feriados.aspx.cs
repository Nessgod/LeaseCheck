using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class View_Sistema_Feriado_Feriados : System.Web.UI.Page
{
    FeriadoController controller = new FeriadoController();
    Feriado feriado = new Feriado();

    public int Id
    {
        get { return Convert.ToInt32(ViewState["Id"]); }
        set { ViewState.Add("Id", value); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_feriado.Ver;
        LeaseCheck.Token.SecurityManagerVer(ver);
        #endregion

        if (!IsPostBack)
        {
            Grid.AddSelectColumn();
            Grid.AddColumn("frd_id", "", "2%", Align: HorizontalAlign.Center);
            Grid.AddColumn("frd_id", "ID", "5%", Align: HorizontalAlign.Center, HeaderAlign: HorizontalAlign.Center);
            Grid.AddColumn("frd_fecha_feriados", "FECHA", "23%", HorizontalAlign.Left, false, "", "{0:dd-MM-yyyy}");
            Grid.AddColumn("frd_descripcion", "DESCRIPCIÓN", "50%");
            Grid.AddColumn("pai_nombre", "PAIS", "20%");
            
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        CargaGrid();
        udPanel.Update();
        LinkButton lnkDescargarPlantilla = (LinkButton)Grid.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("lnkDescargarPlantilla");
        //script que genera el excel 
        ScriptManager.GetCurrent(Page).RegisterPostBackControl(lnkDescargarPlantilla);
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
                    case "cboPais":

                        ctrl.AppendDataBoundItems = true;
                        ctrl.Items.Add(new RadComboBoxItem("Todos", ""));

                        PaisesController paisesController = new PaisesController();
                        ctrl.EmptyMessage = "Todos";
                        ctrl.DataSource = paisesController.GetPaises(); ;
                        ctrl.DataTextField = "PAI_NOMBRE";
                        ctrl.DataValueField = "PAI_ID";
                        ctrl.DataBind();

                        break;
                }
            }
        }
    }

    protected void CargaGrid()
    {
        Feriado feriado = new Feriado();
        RadComboBox2 cboPais = (RadComboBox2)wucFiltro.FindControl("cboPais");
        WebControls.Calendar txtDesde = (WebControls.Calendar)wucFiltro.FindControl("txtDesde");
        WebControls.Calendar txtHasta = (WebControls.Calendar)wucFiltro.FindControl("txtHasta");

        if (wucFiltro.Filtro() != "") feriado.frd_descripcion = wucFiltro.Filtro();

        if (cboPais.SelectedValue != "") feriado.frd_pais = int.Parse(cboPais.SelectedValue);
        if (txtDesde.Text != "") feriado.desde = DateTime.Parse(txtDesde.Text);
        if (txtHasta.Text != "") feriado.hasta = DateTime.Parse(txtHasta.Text);

        Grid.DataSource = controller.GetFeriados(feriado);
        Grid.DataBind();
    }
    
    protected void Grid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
        {
            if (((e.Item) is GridDataItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                string id = item.GetDataKeyValue("frd_id").ToString();
                string query = Server.UrlEncode(Tools.Crypto.Encrypt("Id=" + id));

                HyperLink Editar = new HyperLink();
                Editar.ID = "lnk" + id;
                Editar.CssClass = "icono_Editar";
                Editar.NavigateUrl = "javascript:void(0)";
                Editar.Attributes.Add("onclick", "abrir('" + query + "')");

                GridDataItem DataItem = e.Item as GridDataItem;
                TableCell dca_id = DataItem["frd_id"];
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
                Tools.tools.ClientAlert("Debe seleccionar al menos un registro.");
                CargaGrid();
            }
            else
            {
                Respuesta respuesta = new Respuesta();

                foreach (string item in Grid.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = Grid.MasterTableView.DataKeyValues[Int32.Parse(item)];
                    int id = Int32.Parse(value["frd_id"].ToString());

                    Feriado registro = new Feriado();
                    registro.frd_id = id;

                    respuesta = controller.DeleteFeriado(registro);
                }

                if (!respuesta.error)
                    Tools.tools.ClientAlert(respuesta.detalle, "ok");
                else
                    Tools.tools.ClientAlert(respuesta.detalle, "alerta");   
                CargaGrid();
            }
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.Message);
        }
    }
    protected void LnkGenerar_Click(object SENDER, EventArgs e)
    {
        RadComboBox2 cboPais = (RadComboBox2)wucFiltro.FindControl("cboPais");
        WebControls.Calendar txtDesde = (WebControls.Calendar)wucFiltro.FindControl("txtDesde");
        WebControls.Calendar txtHasta = (WebControls.Calendar)wucFiltro.FindControl("txtHasta");

        if (wucFiltro.Filtro() != "") feriado.frd_descripcion = wucFiltro.Filtro();
        if (cboPais.SelectedValue != "") feriado.frd_pais = int.Parse(cboPais.SelectedValue);
        if (txtDesde.Text != "") feriado.desde = DateTime.Parse(txtDesde.Text);
        if (txtHasta.Text != "") feriado.hasta = DateTime.Parse(txtHasta.Text);


        controller.InformeFeriados(feriado);
    }
}