using LeaseCheck.Model;
using LeaseCheck.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class View_Comun_Controls_Instalaciones_Identidad : System.Web.UI.UserControl
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

    public int IdCliente
    {
        get { return Convert.ToInt32(ViewState["IdCliente"]); }
        set { ViewState.Add("IdCliente", value); }
    }

    public int IdUsuario
    {
        get { return Convert.ToInt32(ViewState["IdUsuario"]); }
        set { ViewState.Add("IdUsuario", value); }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarDatos();
            Bloqueo();
        }

        CargarDatos();
        CargarGridUsuarios();
    }

    protected void CargarDatos()
    {
        if (IdClienteInstalacion > 0)
        {
            ClienteInstalacionController clienteInstalacionController = new ClienteInstalacionController();
            ClienteInstalacion clienteInstalacion = new ClienteInstalacion();

            clienteInstalacion.cin_id = IdClienteInstalacion;
            clienteInstalacion = clienteInstalacionController.GetClienteInstalacion(clienteInstalacion);

            lblId.Text = IdClienteInstalacion.ToString();
            txtNombre.Text = clienteInstalacion.cin_nombre;
            txtDescripcion.Text = clienteInstalacion.cin_descripcion;
            txtDireccion.Text = clienteInstalacion.cin_direccion;

            if (clienteInstalacion.cin_habilitado == false)
            {
                rdbNo.Checked = true;
                rdbSi.Checked = false;
            }
            if (clienteInstalacion.cin_habilitado == true)
            {
                rdbNo.Checked = false;
                rdbSi.Checked = true;
            }
            CargarGridUsuarios();
            pnlGrillUsuarios.Visible = true;
        }
        else
        {
            pnlGrillUsuarios.Visible = false;
            CargarGridVacia();
        }
    }

    protected void Bloqueo()
    {
        txtNombre.ReadOnly = ReadOnly;
        txtDescripcion.ReadOnly = ReadOnly;
        txtDireccion.ReadOnly = ReadOnly;

        rdbSi.Enabled = !ReadOnly;
        rdbNo.Enabled = !ReadOnly;

        btnGuardar.Visible = !ReadOnly;
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();
            ClienteInstalacionController clienteInstalacionController = new ClienteInstalacionController();
            ClienteInstalacion clienteInstalacion = new ClienteInstalacion();

            clienteInstalacion.cin_id = IdClienteInstalacion;
            clienteInstalacion.cin_cliente = IdCliente;
            clienteInstalacion.cin_nombre = txtNombre.Text;
            clienteInstalacion.cin_descripcion = txtDescripcion.Text;
            clienteInstalacion.cin_direccion = txtDireccion.Text;


            if (rdbSi.Checked == true)
                clienteInstalacion.cin_habilitado = true;
            else
                clienteInstalacion.cin_habilitado = false;

            if (IdClienteInstalacion > 0)
            {   
                respuesta = clienteInstalacionController.UpdateClienteInstalacion(clienteInstalacion);
                pnlGrillUsuarios.Visible = true;
                CargarGridUsuarios();
                Tools.tools.ClientAlert(respuesta.detalle, "ok");
            }
            else
            {
                respuesta = clienteInstalacionController.InsertClienteInstalacion(clienteInstalacion);
                IdClienteInstalacion = respuesta.codigo;
                pnlGrillUsuarios.Visible = true;
                CargarGridVacia();
            }

            if (!respuesta.error)
                Tools.tools.ClientAlert(respuesta.detalle, "ok", false);
            else
                Tools.tools.ClientAlert(respuesta.detalle, "alerta");
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.ToString(), "error");
        }
    }


    protected void ConfigurarGrid()
    {
        GridUsuarios.Columns.Clear();
        GridUsuarios.AddSelectColumn();
        GridUsuarios.AddColumn("CIU_ID", "", Width: "5%", Align: HorizontalAlign.Left);
        GridUsuarios.AddColumn("CIU_ID", "ID", Width: "5%", Align: HorizontalAlign.Left);
        GridUsuarios.AddColumn("NOMBRE_COMPLETO", "NOMBRE", Align: HorizontalAlign.Left);
        GridUsuarios.AddColumn("USU_CORREO", "CORREO", Align: HorizontalAlign.Left);
        GridUsuarios.AddCheckboxColumn("CIU_RESPONSABLE", "RESPONSABLE");
        GridUsuarios.AddCheckboxColumn("CIU_HABILITADO", "HABILITADO");

        Tools.tools.RegisterPostBackScript(GridUsuarios);
    }
    protected void CargarGridVacia()
    {
        List<ClienteInstalacionUsuario> items = new List<ClienteInstalacionUsuario>();

        GridUsuarios.DataSource = items;
        GridUsuarios.DataBind();
    }

    protected void CargarGridUsuarios()
    {
        ClienteInstalacionUsuario item = new ClienteInstalacionUsuario();
        ClienteInstalacionusuarioController controller = new ClienteInstalacionusuarioController();
        item.ciu_instalacion = IdClienteInstalacion;

        GridUsuarios.DataSource = controller.GetClienteInstalacionUsuarios(item);
        GridUsuarios.DataBind();
    }

    protected void GridUsuarios_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
        {
            if (((e.Item) is GridDataItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                string id = item.GetDataKeyValue("ciu_id").ToString();

                string query = Server.UrlEncode(Tools.Crypto.Encrypt("Id=" + id + "&IdClienteInstalacion=" + IdClienteInstalacion));

                HyperLink Editar = new HyperLink();
                Editar.ID = "lnkEditar" + id;
                //Editar.Text = "&nbsp";
                Editar.CssClass = "icono_Editar";
                Editar.NavigateUrl = "javascript:void(0)";
                Editar.Attributes.Add("onclick", "abrirAsociarUsuario('" + query + "')");

                GridDataItem DataItem = e.Item as GridDataItem;
                TableCell ciu_id = DataItem["ciu_id"];

                ciu_id.Controls.Add(Editar);
            }
        }
    }

    protected void lnkAñadirUsuario_Click(object sender, EventArgs e)
    {
        string query = Server.UrlEncode(Tools.Crypto.Encrypt("IdCliente=" + IdCliente + "&IdClienteInstalacion=" + IdClienteInstalacion));
        Tools.tools.ClientExecute("abrirAsociarUsuario('" + query + "')");
    }

    protected void lnkEliminarUsuario_Click(object sender, EventArgs e)
    {
        try
        {
            if (GridUsuarios.SelectedIndexes.Count == 0)
            {
                Tools.tools.ClientAlert("Debe seleccionar al menos un registro.");
            }
            else
            {
                Respuesta respuesta = new Respuesta();
                ClienteInstalacionUsuario clienteInstalacionUsuario = new ClienteInstalacionUsuario();
                ClienteInstalacionusuarioController clienteInstalacionUsuarioController = new ClienteInstalacionusuarioController();

                foreach (string item in GridUsuarios.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = GridUsuarios.MasterTableView.DataKeyValues[Int32.Parse(item)];
                    int id = Int32.Parse(value["ciu_id"].ToString());

                    clienteInstalacionUsuario.ciu_id = id;

                    respuesta = clienteInstalacionUsuarioController.DeleteClienteInstalacionUsuario(clienteInstalacionUsuario);
                }
                if (!respuesta.error)
                    Tools.tools.ClientAlert(respuesta.detalle, "ok", false);
                else
                    Tools.tools.ClientAlert(respuesta.detalle, "alerta");
            }
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.Message);
        }
    }



}