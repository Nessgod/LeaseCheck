using System;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System.Web.UI;

public partial class View_Root_Mantenedores_Paises_Paises : System.Web.UI.Page
{
    private PaisesController paisesController = new PaisesController();

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_pais.Ver;
        LeaseCheck.Token.SecurityManagerVer(ver);
        #endregion

        if (!IsPostBack)
        {
            Grid.AddSelectColumn();
            Grid.AddColumn("PAI_ID", "", "2%");
            Grid.AddColumn("PAI_ID", "ID", "5%");
            Grid.AddColumn("PAI_NOMBRE", "PAÍS", "70%");
            Grid.AddColumn("PAI_FECHA_CREACION", "FECHA CREACIÓN", "13%", DataFormat: "{0:dd-MM-yyyy}");
            
            Grid.AddCheckboxColumn("PAI_HABILITADO", "HABILITADO");
        }

        Tools.tools.RegisterPostBackScript(Grid);
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        CargaGrid();
        LinkButton LnkDescargarPlantilla = (LinkButton)Grid.MasterTableView.GetItems(GridItemType.CommandItem)[0].FindControl("LnkDescargarPlantilla");
        //script que genera el excel 
        ScriptManager.GetCurrent(Page).RegisterPostBackControl(LnkDescargarPlantilla);
    }

    protected void CargaGrid()
    {
        Grid.DataSource = paisesController.GetPaises();
        Grid.DataBind();
       
    }

    protected void Grid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
        {
            if (((e.Item) is GridDataItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                string id = item.GetDataKeyValue("PAI_ID").ToString();
                string query = Server.UrlEncode(Tools.Crypto.Encrypt("Id=" + id));

                HyperLink Editar = new HyperLink();
                Editar.ID = "lnk" + id;
                Editar.CssClass = "icono_Editar";
                Editar.NavigateUrl = "javascript:void(0)";
                Editar.Attributes.Add("onclick", "abrir('" + query + "')");

                GridDataItem DataItem = e.Item as GridDataItem;
                
                TableCell PAI_ID = DataItem["PAI_ID"];
                PAI_ID.Controls.Add(Editar);

                

              
            }
        }
    }
    protected void LnkGenerar_Click(object SENDER, EventArgs e) 
    {
        Paises paises = new Paises();
        paisesController.InformePaises(paises);

        

    }

    protected void lnkEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            if (Grid.SelectedIndexes.Count == 0)
            {
                Tools.tools.ClientAlert("Debe seleccionar al menos un País.", "alerta");
            }
            else
            {
                Respuesta respuesta = new Respuesta();

                foreach (string item in Grid.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = Grid.MasterTableView.DataKeyValues[Int32.Parse(item)];
                    int id = Int32.Parse(value["PAI_ID"].ToString());

                    Paises paises = new Paises();
                    paises.pai_id = id;

                    respuesta = paisesController.DeletePais(paises);
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
}