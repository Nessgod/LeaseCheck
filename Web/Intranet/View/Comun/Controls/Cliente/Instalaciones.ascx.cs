using LeaseCheck.Controller;
using LeaseCheck.Model;
using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;


public partial class View_Comun_Controls_Cliente_Instalaciones : System.Web.UI.UserControl
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

    public bool TieneRut { get; set; }

    ClienteController controller = new ClienteController();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string[] query = Tools.Crypto.Decrypt(Server.UrlDecode(Request.QueryString["query"].ToString())).Split('&');

            foreach (string arr in query)
            {
                string[] array = arr.ToString().Split('=');
                switch (array[0].ToString())
                {
                    case "IdCliente":
                        IdCliente = Int32.Parse(array[1].ToString());
                        break;
                }
            }

            GridInstalaciones.AddSelectColumn();
            GridInstalaciones.AddColumn("cin_id", "", "2%", Align: HorizontalAlign.Center);
            GridInstalaciones.AddColumn("cin_id", "ID", "2%", Align: HorizontalAlign.Center);
            GridInstalaciones.AddColumn("cin_nombre", "NOMBRE", "", HorizontalAlign.Left);
            GridInstalaciones.AddColumn("cin_direccion", "DIRECCIÓN", "", HorizontalAlign.Left);
            GridInstalaciones.AddCheckboxColumn("cin_habilitado", "HABILITADO", "10%");
            Tools.tools.RegisterPostBackScript(GridInstalaciones);
        }
    }

    public void LoadControls(object sender, System.EventArgs e)
    {

        if (!IsPostBack)
        {
            
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargaDatos();
            udPanel.Update();
        }
        CargaDatos();
        udPanel.Update();

    }

    protected void CargaDatos()
    {
        CargaGridInstalaciones();
    }

    protected void CargaGridInstalaciones()
    {
        ClienteInstalacion item = new ClienteInstalacion();
        ClienteInstalacionController controllerInstalacion = new ClienteInstalacionController();
        item.cin_cliente = IdCliente;
        GridInstalaciones.DataSource = controllerInstalacion.GetClienteInstalaciones(item);
        GridInstalaciones.DataBind();
    }


    protected void gridInstalaciones_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
        {
            if (((e.Item) is GridDataItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                string id = item.GetDataKeyValue("cin_id").ToString();
                string query = Server.UrlEncode(Tools.Crypto.Encrypt("IdClienteInstalacion=" + id + "&Cliente=" + IdCliente));

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
        string query = Server.UrlEncode(Tools.Crypto.Encrypt("Cliente=" + IdCliente));
        Tools.tools.ClientExecute("abrirInstalacion('" + query + "')");
    }

    protected void lnkEliminarInstalacion_Click(object sender, EventArgs e)
    {
        try
        {
            if (GridInstalaciones.SelectedIndexes.Count == 0)
            {
                Tools.tools.ClientAlert("Debe seleccionar al menos un registro.", "alerta");
            }
            else
            {
                Respuesta respuesta = new Respuesta();

                foreach (string item in GridInstalaciones.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = GridInstalaciones.MasterTableView.DataKeyValues[Int32.Parse(item)];
                    int id = Int32.Parse(value["cin_id"].ToString());

                    ClienteInstalacion item1 = new ClienteInstalacion();
                    ClienteInstalacionController controller = new ClienteInstalacionController();
                    item1.cin_id = id;

                    respuesta = controller.DeleteClienteInstalacion(item1);
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

}