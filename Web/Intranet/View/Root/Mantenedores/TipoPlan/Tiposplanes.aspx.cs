using System;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Web.Services;
using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System.Web.UI;

public partial class View_Mantenedores_TipoPlan_Tiposplanes : System.Web.UI.Page
{
    private TiposdePlanesController controller = new TiposdePlanesController();

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_tipo_plan.Ver;
        LeaseCheck.Token.SecurityManagerVer(ver);
        #endregion

        if (!IsPostBack)
        {
            Grid.AddSelectColumn();
            Grid.AddColumn("tpl_id", "", "2%", Align: HorizontalAlign.Center);
            Grid.AddColumn("tpl_id", "ID", "5%", Align: HorizontalAlign.Center, HeaderAlign: HorizontalAlign.Center);
            Grid.AddColumn("tpl_nombre", "PLAN", "33%", HorizontalAlign.Left);
            Grid.AddColumn("tpl_cantidad_documento", "CANTIDAD DOCUMENTO", "15%", HorizontalAlign.Left, DataFormat: "{0:N0}");
            Grid.AddColumn("tpl_cantidad_propiedad", "CANTIDAD PROPIEDAD", "15%", HorizontalAlign.Left, DataFormat: "{0:N0}");
            Grid.AddColumn("tpl_cantidad_lead", "CANTIDAD LEAD", "15%", HorizontalAlign.Left, DataFormat: "{0:N0}");
            Grid.AddColumn("tpl_valor_plan", "VALOR DE PLAN ($)", "15%", HorizontalAlign.Left, DataFormat: "{0:N0}");
            Grid.AddCheckboxColumn("tpl_habilitado", "HABILITADO");
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

    protected  void CargaGrid()
    {
        TipoPlan tipoPlan = new TipoPlan();

        if (wucFiltro.Filtro() != "") tipoPlan.filtro = wucFiltro.Filtro();
        Grid.DataSource = controller.GetTiposPlanes(tipoPlan);
        Grid.DataBind();
    }

    protected void Grid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
        {
            if (((e.Item) is GridDataItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                string id = item.GetDataKeyValue("tpl_id").ToString();
                string query = Server.UrlEncode(Tools.Crypto.Encrypt("Id=" + id));

                HyperLink Editar = new HyperLink();
                Editar.ID = "lnk" + id;
                Editar.CssClass = "icono_Editar";
                Editar.NavigateUrl = "javascript:void(0)";
                Editar.Attributes.Add("onclick", "abrir('" + query + "')");

                GridDataItem DataItem = e.Item as GridDataItem;
                TableCell dca_id = DataItem["tpl_id"];
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
            }
            else
            {
                Respuesta respuesta = new Respuesta();

                foreach (string item in Grid.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = Grid.MasterTableView.DataKeyValues[Int32.Parse(item)];
                    int id = Int32.Parse(value["tpl_id"].ToString());

                    TipoPlan registro = new TipoPlan();
                    registro.tpl_id = id;

                    respuesta = controller.DeleteTipoPlan(registro);
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
        TipoPlan tipoplanes = new TipoPlan();
        if (wucFiltro.Filtro() != "") tipoplanes.filtro = wucFiltro.Filtro();

        controller.InformeTipoPlan(tipoplanes);
    }
}