using System;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System.Web.UI;

public partial class View_Root_Mantenedores_MesaAyuda_MesaAyuda : System.Web.UI.Page
{
    private MesaAyudaController mesaAyudaController = new MesaAyudaController();

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_pais.Ver;
        LeaseCheck.Token.SecurityManagerVer(ver);
        #endregion

        if (!IsPostBack)
        {
            //Grid.AddSelectColumn();
            Grid.AddColumn("MES_ID", "", "2%");
            Grid.AddColumn("mes_id", "N° CONSULTA");
            Grid.AddColumn("NOMBRE_CLIENTE", "CLIENTE", "10%");
            Grid.AddColumn("MES_NOMBRE", "NOMBRE", "30%");
            Grid.AddColumn("MES_MENSAJE", "CONSULTA", "20%");
            Grid.AddColumn("NOMBRE_CREADOR", "CREADOR", "10%");
            Grid.AddColumn("PERFIL", "PERFIL", "13%");         
            Grid.AddColumn("MES_FECHA_CREACION", "FECHA CONSULTA", "13%", DataFormat: "{0:dd-MM-yyyy HH:mm}");
            Grid.AddColumn("FECHA_ULTIMA_RESPUESTA", "FECHA RESPUESTA", "13%", DataFormat: "{0:dd-MM-yyyy HH:mm}");
            Grid.AddColumn("MES_ESTADO_NOMBRE", "ESTADO", "10%");
            
        }

        Tools.tools.RegisterPostBackScript(Grid);
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
                    case "cboEstatus":

                        ctrl.AppendDataBoundItems = true;
                        ctrl.Items.Add(new RadComboBoxItem("Seleccione", ""));

                         MesaAyudaEstadoController mesaAyudaEstadoController = new MesaAyudaEstadoController();
                        ctrl.EmptyMessage = "Seleccione";
                        ctrl.DataSource = mesaAyudaEstadoController.GetMesaAyudaEstado();
                        ctrl.DataTextField = "EST_NOMBRE";
                        ctrl.DataValueField = "EST_ID";
                        ctrl.DataBind();

                        break;
                }
            }
        }
    }

    protected void CargaGrid()
    {
        MesaAyuda mesaAyuda = new MesaAyuda();
        RadComboBox2 cboEstatus = (RadComboBox2)wucFiltro.FindControl("cboEstatus");

        if (cboEstatus.SelectedValue != "")
        {
            mesaAyuda.mes_estado = int.Parse(cboEstatus.SelectedValue);
        }

        if (wucFiltro.Filtro() != "") mesaAyuda.filtro = wucFiltro.Filtro();
        Grid.DataSource = mesaAyudaController.GetMesaAyuda(mesaAyuda);
        Grid.DataBind();
    }

    protected void Grid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
        {
            if (((e.Item) is GridDataItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                string id = item.GetDataKeyValue("MES_ID").ToString();
                string estado = item.GetDataKeyValue("MES_ESTADO").ToString();
                string query = Server.UrlEncode(Tools.Crypto.Encrypt("Id=" + id + "&Estado="+estado));

                // Corrección del TryParse para C# 4.x
                string fechaTexto = item["FECHA_ULTIMA_RESPUESTA"].Text;
                DateTime fechaUltimaRespuesta;
                if (string.IsNullOrWhiteSpace(fechaTexto) ||
                    fechaTexto == "&nbsp;" ||
                    (DateTime.TryParse(fechaTexto, out fechaUltimaRespuesta) && fechaUltimaRespuesta == DateTime.MinValue))
                {
                    item["FECHA_ULTIMA_RESPUESTA"].Text = "Sin registro";
                }


                HyperLink Editar = new HyperLink();
                Editar.ID = "lnk" + id;
                Editar.CssClass = "fa fa-search";
                Editar.Style.Add("color", "#6d6d6d");
                Editar.Style.Add("padding-left", "5px");
                Editar.NavigateUrl = "javascript:void(0)";
                Editar.Attributes.Add("onclick", "abrir('" + query + "')");

                GridDataItem DataItem = e.Item as GridDataItem;

                TableCell PAI_ID = DataItem["MES_ID"];
                PAI_ID.Controls.Add(Editar);

            }
        }
    }

    protected void LnkGenerar_Click(object SENDER, EventArgs e)
    {
        MesaAyuda mesaayuda = new MesaAyuda();

        RadComboBox2 cboEstatus = (RadComboBox2)wucFiltro.FindControl("cboEstatus");

        if (cboEstatus.SelectedValue != "")
        {
            mesaayuda.mes_estado = int.Parse(cboEstatus.SelectedValue);
        }

        if (wucFiltro.Filtro() != "") mesaayuda.filtro = wucFiltro.Filtro();

        mesaAyudaController.ReporteMesaAyuda(mesaayuda);
    }
}