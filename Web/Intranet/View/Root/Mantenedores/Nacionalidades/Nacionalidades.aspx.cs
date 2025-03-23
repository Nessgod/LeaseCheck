using System;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System.Web.UI;

public partial class View_Root_Mantenedores_Nacionalidades_Nacionalidades : System.Web.UI.Page
{
    NacionalidadesController nacionalidadesController = new NacionalidadesController();

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_nacionalidad.Ver;
        LeaseCheck.Token.SecurityManagerVer(ver);
        #endregion

        if (!IsPostBack)
        {
            Grid.AddSelectColumn();
            Grid.AddColumn("NAC_ID", "", "2%");
            Grid.AddColumn("NAC_ID", "ID", "5%");
            Grid.AddColumn("NAC_NOMBRE", "NACIONALIDAD", "70%");
            Grid.AddColumn("NAC_FECHA_CREACION", "FECHA CREACIÓN", "13%", DataFormat: "{0:dd-MM-yyyy}");
            Grid.AddCheckboxColumn("NAC_HABILITADO", "HABILITADO", "10%");
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
        Grid.DataSource = nacionalidadesController.GetNacionalidades();
        Grid.DataBind();
    }

    protected void Grid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
        {
            if (((e.Item) is GridDataItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                string id = item.GetDataKeyValue("NAC_ID").ToString();
                string query = Server.UrlEncode(Tools.Crypto.Encrypt("Id=" + id));

                HyperLink Editar = new HyperLink();
                Editar.ID = "lnk" + id;
                Editar.CssClass = "icono_Editar";
                Editar.NavigateUrl = "javascript:void(0)";
                Editar.Attributes.Add("onclick", "abrir('" + query + "')");

                GridDataItem DataItem = e.Item as GridDataItem;
                
                TableCell NAC_ID = DataItem["NAC_ID"];
                NAC_ID.Controls.Add(Editar);

                TableCell NAC_HABILITADO = DataItem["NAC_HABILITADO"];

                
            }
        }
    }

    protected void lnkEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            if (Grid.SelectedIndexes.Count == 0)
            {
                Tools.tools.ClientAlert("Debe seleccionar al menos una Nacionalidad.","alerta");                
            }
            else
            {
                Respuesta respuesta = new Respuesta();

                foreach (string item in Grid.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = Grid.MasterTableView.DataKeyValues[Int32.Parse(item)];
                    int id = Int32.Parse(value["NAC_ID"].ToString());

                    Nacionalidades nacionalidad = new Nacionalidades();
                    nacionalidad.nac_id = id;

                    respuesta = nacionalidadesController.DeleteNacionalidad(nacionalidad);
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
        Nacionalidades nacionalidades = new Nacionalidades();
        nacionalidadesController.InformeNacionalidades(nacionalidades);
    }
}