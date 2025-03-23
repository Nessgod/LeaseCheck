using System;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System.Web.UI;

public partial class View_Root_Mantenedores_Perfiles_Perfiles : System.Web.UI.Page
{
    private PerfilesController perfilesController = new PerfilesController();

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_perfil.Ver;
        LeaseCheck.Token.SecurityManagerVer(ver);
        #endregion

        if (!IsPostBack)
        {
            Grid.AddSelectColumn();
            Grid.AddColumn("PER_ID", "", "2%", Align: HorizontalAlign.Center);
            Grid.AddColumn("PER_ID", "ID", "5%", Align: HorizontalAlign.Center, HeaderAlign: HorizontalAlign.Center);
            Grid.AddColumn("PER_NOMBRE", "PERFIL", "33%", HorizontalAlign.Left);
            Grid.AddColumn("PER_DESCRIPCION", "DESCRIPCION", "50%", HorizontalAlign.Left);
            Grid.AddColumn("TPP_NOMBRE", "TIPO", "50%", HorizontalAlign.Left);
            Grid.AddCheckboxColumn("PER_HABILITADO", "ESTADO", "10%");
        }

        Tools.tools.RegisterPostBackScript(Grid);
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        CargaGrid();
        udPanel.Update();
        LinkButton LnkDescargarPlantilla = (LinkButton)Grid.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("LnkDescargarPlantilla");
      
       ScriptManager.GetCurrent(Page).RegisterPostBackControl(LnkDescargarPlantilla);
    }

    protected void CargaGrid()
    {
        Perfiles perfil = new Perfiles();
        if (wucFiltro.Filtro() != "") perfil.per_descripcion = wucFiltro.Filtro();
        
        Grid.DataSource = perfilesController.ListoPerfiles(perfil);
        Grid.DataBind();
    }

    protected void Grid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
        {
            if (((e.Item) is GridDataItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                string id = item.GetDataKeyValue("PER_ID").ToString();

                string query = Server.UrlEncode(Tools.Crypto.Encrypt("Id=" + id));

                HyperLink Editar = new HyperLink();
                Editar.ID = "lnkEditar" + id;
                Editar.CssClass = "icono_Editar";
                Editar.NavigateUrl = "javascript:void(0)";
                Editar.Attributes.Add("onclick", "abrirPerfil('" + query + "')");

                GridDataItem DataItem = e.Item as GridDataItem;
                
                TableCell PER_ID = DataItem["PER_ID"];
                PER_ID.Controls.Add(Editar);

                TableCell PER_HABILITADO = DataItem["PER_HABILITADO"];

                
            }
        }
    }

    protected void lnkEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            if (Grid.SelectedIndexes.Count == 0)
            {
                Tools.tools.ClientAlert("Debe seleccionar al menos un Perfil.", "alerta");
            }
            else
            {
                Respuesta respuesta = new Respuesta();

                foreach (string item in Grid.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = Grid.MasterTableView.DataKeyValues[Int32.Parse(item)];
                    int id = Int32.Parse(value["PER_ID"].ToString());

                    Perfiles registro = new Perfiles();
                    registro.per_id = id;

                    respuesta = perfilesController.DeletePerfil(registro);
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
            Tools.tools.ClientAlert(ex.ToString(), "error");
        }
    }

    protected void LnkGenerar_Click(object SENDER, EventArgs e)
    {
        Perfiles perfil = new Perfiles();
        if (wucFiltro.Filtro() != "") perfil.per_descripcion = wucFiltro.Filtro();
        
        perfilesController.ReporteInforme(perfil);
    }
}