using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.ImageEditor;

public partial class View_Clientes_Identidad_ClienteTicket : System.Web.UI.Page
{
    protected int Id
    {
        get { return Convert.ToInt32(ViewState["Id"]); }
        set { ViewState.Add("Id", value); }
    }


    MesaAyudaController controller = new MesaAyudaController();

    protected void Page_Load(object sender, EventArgs e)
    {
        //#region SeguridadPagina
        //MenuPerfil ver = new MenuPerfil();
        //ver.mpe_menu = (int)LeaseCheck.Paginas.menu_cliente.Ver;
        //LeaseCheck.Token.SecurityManagerVer(ver);
        //#endregion

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
                }
            }

            ConfigurarGrid();
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

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargaGrid();
            udPanel.Update();
        }
        CargaGrid();
        udPanel.Update();
    }

    protected void ConfigurarGrid()
    {
        GridMisTickets.AddSelectColumn();
        GridMisTickets.AddColumn("MES_ID", "", "2%");
        GridMisTickets.AddColumn("MES_ID", "N° CONSULTA");
        GridMisTickets.AddColumn("NOMBRE_CLIENTE", "CLIENTE", "10%");
        GridMisTickets.AddColumn("MES_NOMBRE", "NOMBRE", "30%");
        GridMisTickets.AddColumn("MES_MENSAJE", "CONSULTA", "20%");
        GridMisTickets.AddColumn("NOMBRE_CREADOR", "CREADOR", "10%");
        GridMisTickets.AddColumn("PERFIL", "PERFIL", "13%");
        GridMisTickets.AddColumn("MES_FECHA_CREACION", "FECHA CONSULTA", "13%", DataFormat: "{0:dd-MM-yyyy HH:mm}");
        GridMisTickets.AddColumn("FECHA_ULTIMA_RESPUESTA", "FECHA RESPUESTA", "13%", DataFormat: "{0:dd-MM-yyyy HH:mm}");
        GridMisTickets.AddColumn("MES_ESTADO_NOMBRE", "ESTADO", "10%");
        Tools.tools.RegisterPostBackScript(GridMisTickets);
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
        GridMisTickets.DataSource = controller.GetMesaAyuda(mesaAyuda);
        GridMisTickets.DataBind();
    }
    protected void lnkCerrarTicket_Click(object sender, EventArgs e)
    {

    }

    protected void GridMisTickets_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = e.Item as GridDataItem;

                // Corrección del TryParse para C# 4.x
                string fechaTexto = item["FECHA_ULTIMA_RESPUESTA"].Text;
                DateTime fechaUltimaRespuesta;
                if (string.IsNullOrWhiteSpace(fechaTexto) ||
                    fechaTexto == "&nbsp;" ||
                    (DateTime.TryParse(fechaTexto, out fechaUltimaRespuesta) && fechaUltimaRespuesta == DateTime.MinValue))
                {
                    item["FECHA_ULTIMA_RESPUESTA"].Text = "Sin registro";
                }

                // Enlace de edición
                string id = item.GetDataKeyValue("mes_id").ToString();
                string query = Server.UrlEncode(Tools.Crypto.Encrypt("Id=" + id));

                HyperLink Editar = new HyperLink();
                Editar.ID = "lnk" + id;
                Editar.CssClass = "fa fa-search";
                Editar.Style.Add("color", "#6d6d6d");
                Editar.Style.Add("padding-left", "5px");
                Editar.NavigateUrl = "javascript:void(0)";
                Editar.NavigateUrl = "javascript:void(0)";
                Editar.Attributes.Add("onclick", "abrir('" + query + "')");

                TableCell dca_id = item["mes_id"];
                dca_id.Controls.Add(Editar);
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

        controller.ReporteMesaAyuda(mesaayuda);
    }


}