using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;


public partial class View_Comun_Controls_Cliente_Documentos : System.Web.UI.UserControl
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
    private ClienteDocumentoController controllerClienteDocumento = new ClienteDocumentoController();
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

            GridDocumentos.AddSelectColumn();
            GridDocumentos.AddColumn("cdo_id", "ID", "5%", HorizontalAlign.Center);
            GridDocumentos.AddTemplateColumn("documentoFisico", "", "DESCARGAR", ItemPosition: HorizontalAlign.Center, Width: "5%");
            GridDocumentos.AddColumn("cdo_descripcion", "DESCRIPCION", "27%", HorizontalAlign.Left);
            GridDocumentos.AddColumn("nombre_usuario_creacion", "USUARIO CREACION", "40%", HorizontalAlign.Left);
            GridDocumentos.AddColumn("cdo_fecha_creacion", "FECHA CREACION", "30%", HorizontalAlign.Left);
            Tools.tools.RegisterPostBackScript(GridDocumentos);
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



    protected void CargaGrid()
    {
        ClienteDocumento clienteDocumento = new ClienteDocumento();
        clienteDocumento.cdo_id_cliente = IdCliente;
        GridDocumentos.DataSource = controllerClienteDocumento.GetClienteDocumentos(clienteDocumento);
        GridDocumentos.DataBind();
    }
    protected void CargaDatos()
    {
        CargaGrid();
    }

    protected void lnkNuevoDocumento_Click(object sender, EventArgs e)
    {
        string query = Server.UrlEncode(Tools.Crypto.Encrypt("IdCliente=" + IdCliente));
        Tools.tools.ClientExecute("abrirDocumento('" + query + "')");
    }

    protected void lnkEliminarDocumento_Click(object sender, EventArgs e)
    {
        try
        {
            if (GridDocumentos.SelectedIndexes.Count == 0)
            {
                Tools.tools.ClientAlert("Debe seleccionar al menos un registro.", "alerta");
                CargaGrid();
            }
            else
            {
                Respuesta respuesta = new Respuesta();

                foreach (string item in GridDocumentos.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = GridDocumentos.MasterTableView.DataKeyValues[Int32.Parse(item)];
                    int id = Int32.Parse(value["cdo_id"].ToString());

                    ClienteDocumento clienteDocumento = new ClienteDocumento();
                    clienteDocumento.cdo_id = id;

                    respuesta = controllerClienteDocumento.DeleteDocumentos(clienteDocumento);
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

    protected void GridDocumentos_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
        {
            if (((e.Item) is GridDataItem))
            {
                GridDataItem item = e.Item as GridDataItem;

                string cdo_id = "";

                cdo_id = item.GetDataKeyValue("cdo_id").ToString();

                //obtengo el link de descarga
                LinkButton lnkDescarga = (LinkButton)item["documentoFisico"].FindControl("lnkDescarga");
                lnkDescarga.CommandName = cdo_id;
            }
        }
    }

    protected void GridDocumentos_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            GridDataItem item = (e.Item as GridDataItem);

            //Descargar Documento Firmado
            LinkButton lnkDescarga = new LinkButton();
            lnkDescarga.ID = "lnkDescarga";
            lnkDescarga.Text = "&nbsp";
            lnkDescarga.CssClass = "fas fa-download";
            lnkDescarga.Command += new CommandEventHandler(lnkDescarga_Command);
            lnkDescarga.ToolTip = "Descargar Documento";

            item["documentoFisico"].Controls.Add(lnkDescarga);

            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lnkDescarga);
        }
    }

    private void lnkDescarga_Command(Object sender, CommandEventArgs e)
    {
        ClienteDocumento clienteDocumento = new ClienteDocumento();
        ClienteDocumentoController controller = new ClienteDocumentoController();

        clienteDocumento.cdo_id = int.Parse(e.CommandName.ToString());
        clienteDocumento = controller.GetClienteDocumento(clienteDocumento);

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.ContentType = clienteDocumento.arc_contenido;
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + clienteDocumento.arc_nombre_archivo + clienteDocumento.arc_extension);
        HttpContext.Current.Response.BinaryWrite(clienteDocumento.abi_archivo_binario);
        HttpContext.Current.Response.End();

    }



}