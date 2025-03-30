using LeaseCheck.Controller;
using LeaseCheck.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class View_Clientes_Instalacion_Instalacion : System.Web.UI.Page
{
    public bool ReadOnly
    {
        get { return Convert.ToBoolean(ViewState["ReadOnly"]); }
        set { ViewState.Add("ReadOnly", value); }
    }

    public int IdClienteInstalacion
    {
        get { return Convert.ToInt32(ViewState["IdClienteInstalacion"]); }
        set { ViewState.Add("IdClienteInstalacion", value); }
    }

    public int Cliente
    {
        get { return Convert.ToInt32(ViewState["Cliente"]); }
        set { ViewState.Add("Cliente", value); }
    }

 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
            
        }

        GridInstalaciones.AddSelectColumn();
        GridInstalaciones.AddColumn("cin_id", "", "2%", Align: HorizontalAlign.Center);
        GridInstalaciones.AddColumn("cin_id", "ID", "2%", Align: HorizontalAlign.Center);
        GridInstalaciones.AddColumn("cin_nombre", "NOMBRE", "", HorizontalAlign.Left);
        GridInstalaciones.AddColumn("cin_direccion", "DIRECCIÓN", "", HorizontalAlign.Left);
        GridInstalaciones.AddCheckboxColumn("cin_habilitado", "HABILITADO", "10%");
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        CargaGridInstalaciones();
    }

    protected void CargaGridInstalaciones()
    {

        if (bool.Parse(LeaseCheck.Session.Usuario_Es_Cliente()))
        {
          
            ClienteInstalacion item = new ClienteInstalacion();
            ClienteInstalacionController controllerInstalacion = new ClienteInstalacionController();

            GridInstalaciones.DataSource = controllerInstalacion.GetClienteInstalaciones(item);
            GridInstalaciones.DataBind();
        }

    }


    protected void gridInstalaciones_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
        {
            if (((e.Item) is GridDataItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                string id = item.GetDataKeyValue("cin_id").ToString();
                string query = Server.UrlEncode(Tools.Crypto.Encrypt("Id=" + id + "&Cliente=" + Cliente));

                HyperLink Editar = new HyperLink();
                Editar.ID = "lnk" + id;
                Editar.CssClass = "icono_Editar";
                Editar.NavigateUrl = "javascript:void(0)";
                Editar.Attributes.Add("onclick", "abrirInstalacion('" + query + "')");

                GridDataItem DataItem = e.Item as GridDataItem;
                TableCell cin_id = DataItem["cin_id"];
                cin_id.Controls.Add(Editar);
            }
        }
    }
    protected void lnkNuevaInstalacion_Click(object sender, EventArgs e)
    {
        string query = Server.UrlEncode(Tools.Crypto.Encrypt("Cliente=" + Cliente));
        Tools.tools.ClientExecute("abrirInstalacion('" + query + "')");
    }

    protected void lnkEliminarInstalacion_Click(object sender, EventArgs e)
    {

    }

}