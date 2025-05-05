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

        }

        ConfigurarGrid();
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargaGrid();
        }
        CargaGrid();
    }

    protected void ConfigurarGrid()
    {
        GridMisTickets.AddSelectColumn();
        GridMisTickets.AddColumn("mes_id", "");
        GridMisTickets.AddColumn("mes_nombre", "NOMBRE");
        GridMisTickets.AddColumn("NOMBRE_MODULO", "MODULO");
        GridMisTickets.AddColumn("NOMBRE_CREADOR", "CREADOR");
        GridMisTickets.AddColumn("MES_FECHA_CREACION", "FECHA CREACION");
        GridMisTickets.AddColumn("MES_ESTADO_NOMBRE", "ESTADO");
        Tools.tools.RegisterPostBackScript(GridMisTickets);
    }


    protected void CargaGrid()
    {
        MesaAyuda mesaAyuda = new MesaAyuda();
        var Tickets = controller.GetMesaAyuda(mesaAyuda);

        // Asignamos a la grilla
        GridMisTickets.DataSource = Tickets;
        GridMisTickets.DataBind();
    }


    protected void lnkCerrarTicket_Click(object sender, EventArgs e)
    {

    }

    protected void GridMisTickets_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
        {
            if (((e.Item) is GridDataItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                string id = item.GetDataKeyValue("mes_id").ToString();
                string query = Server.UrlEncode(Tools.Crypto.Encrypt("Id=" + id));

                HyperLink Editar = new HyperLink();
                Editar.ID = "lnk" + id;
                Editar.CssClass = "icono_Editar";
                Editar.NavigateUrl = "javascript:void(0)";
                Editar.Attributes.Add("onclick", "abrir('" + query + "')");

                GridDataItem DataItem = e.Item as GridDataItem;
                TableCell dca_id = DataItem["mes_id"];
                dca_id.Controls.Add(Editar);
            }
        }
    }
}