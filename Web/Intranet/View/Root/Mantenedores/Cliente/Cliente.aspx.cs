using LeaseCheck.Controller;
using LeaseCheck.Model;
using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class View_Root_Mantenedores_Cliente_Cliente : System.Web.UI.Page
{
    protected int Id
    {
        get { return Convert.ToInt32(ViewState["Id"]); }
        set { ViewState.Add("Id", value); }
    }

    private ClienteController controller = new ClienteController();
    private ClienteDocumentoController controllerClienteDocumento = new ClienteDocumentoController();
    public bool TieneRut { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_cliente.Ver;
        LeaseCheck.Token.SecurityManagerVer(ver);
        #endregion

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

            GridDocumentos.AddSelectColumn();
            //GridDocumentos.AddColumn("cdo_id", "", "2%", Align: HorizontalAlign.Center);
            GridDocumentos.AddColumn("cdo_id", "ID", "5%", HorizontalAlign.Center);
            GridDocumentos.AddTemplateColumn("documentoFisico", "", "DESCARGAR", ItemPosition: HorizontalAlign.Center, Width: "5%");
            GridDocumentos.AddColumn("cdo_descripcion", "DESCRIPCION", "27%", HorizontalAlign.Left);
            GridDocumentos.AddColumn("nombre_usuario_creacion", "USUARIO CREACION", "40%", HorizontalAlign.Left);
            GridDocumentos.AddColumn("cdo_fecha_creacion", "FECHA CREACION", "30%", HorizontalAlign.Left);
            //GridDocumentos.AddColumn("clp_fecha_hasta", "HASTA", "", HorizontalAlign.Left, DataFormat: "{0:dd-MM-yyyy}");

            Grid.AddSelectColumn();
            Grid.AddColumn("clp_id", "", "2%", Align: HorizontalAlign.Center);
            Grid.AddColumn("clp_id", "ID", "", HorizontalAlign.Left);
            Grid.AddColumn("tipo_dato", "TIPO", "", HorizontalAlign.Left);
            Grid.AddColumn("plan_nombre", "NOMBRE", "", HorizontalAlign.Left);
            Grid.AddColumn("clp_fecha_desde", "DESDE", "", HorizontalAlign.Left, DataFormat: "{0:dd-MM-yyyy}");
            Grid.AddColumn("clp_fecha_hasta", "HASTA", "", HorizontalAlign.Left, DataFormat: "{0:dd-MM-yyyy}");
            Grid.AddColumn("estado", "ESTADO", "", HorizontalAlign.Left);
            //Grid.AddColumn("clp_administradores_ilimitados", "ESTADO", "", HorizontalAlign.Left);
            Grid.AddColumn("clp_administradores", "ADMINISTRADORES", "", HorizontalAlign.Left);
            Grid.AddColumn("clp_cantidad", "CANTIDAD", "", HorizontalAlign.Left);
            Grid.AddColumn("clp_valor_plan", "VALOR", "", HorizontalAlign.Left, DataFormat: "{0:N0}");

            gridUsuarios.AddSelectColumn();
            gridUsuarios.AddColumn("usu_id", "", "2%", Align: HorizontalAlign.Center);
            gridUsuarios.AddColumn("nombreCompleto", "NOMBRE COMPLETO", "", HorizontalAlign.Left);
            gridUsuarios.AddColumn("perfil_nombre", "PERFIL", "", HorizontalAlign.Left);
            gridUsuarios.AddCheckboxColumn("usu_habilitado", "HABILITADO", "10%");


            GridInstalaciones.AddSelectColumn();
            GridInstalaciones.AddColumn("cin_id", "", "2%", Align: HorizontalAlign.Center);
            GridInstalaciones.AddColumn("cin_id", "ID", "2%", Align: HorizontalAlign.Center);
            GridInstalaciones.AddColumn("cin_nombre", "NOMBRE", "", HorizontalAlign.Left);
            GridInstalaciones.AddColumn("cin_direccion", "DIRECCIÓN", "", HorizontalAlign.Left);
            GridInstalaciones.AddCheckboxColumn("cin_habilitado", "HABILITADO", "10%");
        }
        Tools.tools.RegisterPostBackScript(GridDocumentos);
        tieneRut();
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        Validaciones();
        CargaComuna();
        CargaDatos();
        udPanel.Update();
        ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnGuardar);
    }

    protected void Validaciones()
    {
        if (Id > 0)
        {
            ragTab.Tabs[1].Visible = true;
            ragTab.Tabs[2].Visible = true;
            ragTab.Tabs[3].Visible = true;
        }
        else
        {
            ragTab.Tabs[1].Visible = false;
            ragTab.Tabs[2].Visible = false;
            ragTab.Tabs[3].Visible = false;
            lblTituloCliente.Text = "Nuevo Cliente";
        }
    }

    public void LoadControls(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            if (sender is RadComboBox2)
            {
          
                RadComboBox2 ctrl = (RadComboBox2)sender;
                switch (ctrl.ID)
                {
                    case "cboPais":

                        ctrl.DataSource = controller.GetPaises();
                        ctrl.DataValueField = "pai_id";
                        ctrl.DataTextField = "pai_nombre";
                        ctrl.SelectedValue = LeaseCheck.Session.Pais();
                        ctrl.DataBind();

                        break;
                }
            }
        }
    }

    protected void CargaDatos()
    {
        if (Id > 0)
        {
            if (!IsPostBack)
            {
                Cliente cliente = new Cliente();
                cliente.cli_id = Id;
                cliente = controller.GetCliente(cliente);

                lblID.Text = cliente.cli_id.ToString();
                txtNombre.Text = cliente.cli_nombre;
                txtGiro.Text = cliente.cli_giro;
                txtRut.Text = cliente.cli_rut.ToString();
                txtDv.Text = cliente.cli_dv;
                txtIdentificador.Text = cliente.cli_identificador;
                txtAlias.Text = cliente.cli_alias;
                cboPais.SelectedValue = cliente.cli_pais.ToString();
                txtDireccion.Text = cliente.cli_direccion;
                txtEmail.Text = cliente.cli_email;
                txtTelefono.Text = cliente.cli_telefono;
                chkDemo.Checked = cliente.cli_es_demo;
                txtContactoNombre.Text = cliente.cli_contacto_nombre;
                txtContactoEmail.Text = cliente.cli_contacto_email;
                txtContactoTelefono.Text = cliente.cli_contacto_telefono;

                Comuna filtro = new Comuna();
                filtro.cmn_pais = cliente.cli_pais;

                cboComuna.Items.Clear();
                cboComuna.DataSource = controller.GetComunas(filtro);
                cboComuna.DataValueField = "cmn_id";
                cboComuna.DataTextField = "cmn_nombre";
                cboComuna.SelectedValue = cliente.cli_comuna.ToString();
                cboComuna.DataBind();

                if (cliente.cli_tiene_rut)
                {
                    rdoSi.Checked = true;
                    pnlRut.Style.Add("display", "");
                    pnlIdentificador.Style.Add("display", "none");
                }
                else
                {
                    rdoNo.Checked = true;
                    pnlIdentificador.Style.Add("display", "");
                    pnlRut.Style.Add("display", "none");
                }

                if (cliente.cli_logo != null)
                {
                    string base64String = Convert.ToBase64String(cliente.cli_logo, 0, cliente.cli_logo.Length);
                    imgLogo.ImageUrl = "data:image/jpeg;base64," + base64String;
                }

                if (bool.Parse(cliente.cli_habilitado.ToString()))
                {
                    rdbSi.Checked = true;
                    rdbNo.Checked = false;
                }
                else
                {
                    rdbSi.Checked = false;
                    rdbNo.Checked = true;
                }
            }

            CargaGrid();
            pnlPlan.Visible = true;

            CargaGridUsuarios();
            pnlUsuarios.Visible = true;

            CargaGridInstalaciones();
            pnlInstalaciones.Visible = true;
        }
    }

    protected void CargaComuna()
    {
        Comuna filtro = new Comuna();
        filtro.cmn_pais = int.Parse(cboPais.SelectedValue);

        cboComuna.Items.Clear();
        cboComuna.DataSource = controller.GetComunas(filtro);
        cboComuna.DataValueField = "cmn_id";
        cboComuna.DataTextField = "cmn_nombre";
        if (filtro.cmn_pais == 1) cboComuna.SelectedValue = "1";
        cboComuna.DataBind();
    }

    protected void CargaGrid()
    {
        ClientePlan item = new ClientePlan();
        item.clp_cliente = Id;
        List<ClientePlan> listado = new List<ClientePlan>();
        listado = controller.GetClientePlanes(item);

        Grid.DataSource = listado;
        Grid.DataBind();

        CalcularTotalValor();

        //Cargar GridClienteDocumento

        ClienteDocumento clienteDocumento = new ClienteDocumento();
        clienteDocumento.cdo_id_cliente = Id;
        GridDocumentos.DataSource = controllerClienteDocumento.GetClienteDocumentos(clienteDocumento);
        GridDocumentos.DataBind();
    }

    private void CalcularTotalValor()
    {
        decimal total = 0;

        foreach (GridDataItem item in Grid.MasterTableView.Items)
        {
            var estado = item["estado"].Text;
            var valorPlan = item["clp_valor_plan"].Text;
            if (estado == "Activo")
            {
                decimal valor = 0;
                if (!string.IsNullOrEmpty(valorPlan))
                {
                    decimal.TryParse(valorPlan, out valor);
                }

                total += valor;
            }
        }

        lblValorTotal.Text = total.ToString("N0");
    }

    protected void CargaGridUsuarios()
    {
        Cliente item = new Cliente();
        item.cli_id = Id;
        gridUsuarios.DataSource = controller.GetClienteUsuarios(item);
        gridUsuarios.DataBind();
    }

    protected void Grid_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
        {
            if (((e.Item) is GridDataItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                string id = item.GetDataKeyValue("clp_id").ToString();
                string tipo_dato = item.GetDataKeyValue("tipo_dato").ToString();
                string query = Server.UrlEncode(Tools.Crypto.Encrypt("Id=" + id + "&tipo_dato=" + tipo_dato));

                HyperLink Editar = new HyperLink();
                Editar.ID = "lnk" + id;
                Editar.CssClass = "icono_Editar";
                Editar.NavigateUrl = "javascript:void(0)";
                Editar.Attributes.Add("onclick", "abrirPlan('" + query + "')");

                GridDataItem DataItem = e.Item as GridDataItem;
                TableCell dca_id = DataItem["clp_id"];
                dca_id.Controls.Add(Editar);
            }
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (!ValidaArchivo()) return;

        try
        {
            Respuesta respuesta = new Respuesta();

            Cliente cliente = new Cliente();
            cliente.cli_id = Id;
            cliente.cli_nombre = txtNombre.Text;
            cliente.cli_giro = txtGiro.Text;

            cliente.cli_alias = txtAlias.Text;
            cliente.cli_pais = int.Parse(cboPais.SelectedValue);
            cliente.cli_comuna = int.Parse(cboComuna.SelectedValue);
            cliente.cli_direccion = txtDireccion.Text;
            cliente.cli_email = txtEmail.Text;
            cliente.cli_telefono = txtTelefono.Text;
            cliente.cli_es_demo = chkDemo.Checked;
            cliente.cli_contacto_nombre = txtContactoNombre.Text;
            cliente.cli_contacto_email = txtContactoEmail.Text;
            cliente.cli_contacto_telefono = txtContactoTelefono.Text;
            cliente.cli_tiene_rut = rdoSi.Checked;


            if (rdoSi.Checked)
            {
                cliente.cli_rut = int.Parse(txtRut.Text);
                cliente.cli_dv = txtDv.Text;
            }
            else
            {
                cliente.cli_identificador = txtIdentificador.Text;
            }

            if (fudFoto.HasFile)
                cliente.cli_logo = LeaseCheck.LeaseCheck.ReducirImagen(fudFoto.FileBytes, 100, 100);

            if (rdbSi.Checked)
                cliente.cli_habilitado = true;

            if (Id > 0)
                respuesta = controller.UpdateCliente(cliente);
            else
            {
                respuesta = controller.InsertCliente(cliente);
                Id = respuesta.codigo;
                CargaDatos();
            }

            if (!respuesta.error)
                Tools.tools.ClientAlert(respuesta.detalle, "ok", true);
            else
                Tools.tools.ClientAlert(respuesta.detalle, "alerta");
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.Message, "error");
        }
    }

    public void tieneRut()
    {

        if (rdoSi.Checked)
        {
            pnlRut.Style.Add("display", "");
            pnlIdentificador.Style.Add("display", "none");
        }
        else
        {
            pnlIdentificador.Style.Add("display", "");
            pnlRut.Style.Add("display", "none");
        }
    }

    protected void lnkEliminar_Click(object sender, EventArgs e)
    {
        try
        {
            if (Grid.SelectedIndexes.Count == 0)
            {
                Tools.tools.ClientAlert("Debe seleccionar al menos un registro.", "alerta");
            }
            else
            {
                Respuesta respuesta = new Respuesta();

                foreach (string item in Grid.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = Grid.MasterTableView.DataKeyValues[Int32.Parse(item)];
                    int id = Int32.Parse(value["clp_id"].ToString());

                    ClientePlan plan = new ClientePlan();
                    plan.clp_id = id;

                    respuesta = controller.DeleteClientePlan(plan);
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

    protected void lnkNuevo_Click(object sender, EventArgs e)
    {
        string query = Server.UrlEncode(Tools.Crypto.Encrypt("Cliente=" + Id));
        Tools.tools.ClientExecute("abrirPlan('" + query + "')");
    }

    protected void lnkNuevoUsuario_Click(object sender, EventArgs e)
    {
        string query = Server.UrlEncode(Tools.Crypto.Encrypt("Cliente=" + Id));
        Tools.tools.ClientExecute("abrirUsuario('" + query + "')");
    }

    protected void lnkEliminarUsuario_Click(object sender, EventArgs e)
    {
        try
        {
            if (gridUsuarios.SelectedIndexes.Count == 0)
            {
                Tools.tools.ClientAlert("Debe seleccionar al menos un registro.", "alerta");
            }
            else
            {
                Respuesta respuesta = new Respuesta();

                foreach (string item in gridUsuarios.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = gridUsuarios.MasterTableView.DataKeyValues[Int32.Parse(item)];
                    int id = Int32.Parse(value["usu_id"].ToString());

                    Usuarios usuario = new Usuarios();
                    usuario.usu_id = id;

                    respuesta = controller.DeleteClienteUsuario(usuario);
                }


                if (!respuesta.error)
                    Tools.tools.ClientAlert(respuesta.detalle, "ok");
                else
                    Tools.tools.ClientAlert(respuesta.detalle, "alerta");

            }
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.ToString(), "error");
        }
    }

    protected void gridUsuarios_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item.ItemType == GridItemType.AlternatingItem | e.Item.ItemType == GridItemType.Item)
        {
            if (((e.Item) is GridDataItem))
            {
                GridDataItem item = e.Item as GridDataItem;
                string id = item.GetDataKeyValue("usu_id").ToString();
                string query = Server.UrlEncode(Tools.Crypto.Encrypt("Id=" + id + "&Cliente=" + Id));

                HyperLink Editar = new HyperLink();
                Editar.ID = "lnk" + id;
                Editar.CssClass = "icono_Editar";
                Editar.NavigateUrl = "javascript:void(0)";
                Editar.Attributes.Add("onclick", "abrirUsuario('" + query + "')");

                GridDataItem DataItem = e.Item as GridDataItem;
                TableCell dca_id = DataItem["usu_id"];
                dca_id.Controls.Add(Editar);
            }
        }
    }

    protected void lnkReset_Click(object sender, EventArgs e)
    {
        try
        {
            if (gridUsuarios.SelectedIndexes.Count == 0)
            {
                Tools.tools.ClientAlert("Debe seleccionar al menos un registro.", "alerta");
            }
            else
            {
                Respuesta respuesta = new Respuesta();

                foreach (string item in gridUsuarios.SelectedIndexes)
                {
                    Telerik.Web.UI.DataKey value = gridUsuarios.MasterTableView.DataKeyValues[Int32.Parse(item)];
                    int id = Int32.Parse(value["usu_id"].ToString());

                    Usuarios usuario = new Usuarios();
                    usuario.usu_id = id;

                    respuesta = controller.ResetPassword(usuario);
                }


                if (!respuesta.error)
                    Tools.tools.ClientAlert(respuesta.detalle, "ok");
                else
                    Tools.tools.ClientAlert(respuesta.detalle, "alerta");

            }
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.ToString(), "error");
        }
    }

    protected bool ValidaArchivo()
    {
        try
        {
            if (fudFoto.HasFile)
            {
                if (Path.GetExtension(fudFoto.FileName).ToUpper() == ".JPG" | Path.GetExtension(fudFoto.FileName).ToUpper() == ".PNG")
                {
                    if (fudFoto.FileBytes.Length > 2097152) //2MB (1 MB = 1048576 bytes)
                    {
                        Tools.tools.ClientAlert("El tamaño del archivo no debe superar los 2MB.", "alerta");
                        return false;
                    }
                }
                else
                {
                    Tools.tools.ClientAlert("Debe subir una imagen JPG o PNG.", "alerta");
                    return false;
                }
            }

            return true;
        }
        catch
        {
            Tools.tools.ClientAlert("No fue posible validar los archivos adjuntos.", "error");
            return false;
        }
    }

    protected void lnkNuevoDocumento_Click(object sender, EventArgs e)
    {
        string query = Server.UrlEncode(Tools.Crypto.Encrypt("IdCliente=" + Id));
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



    protected void CargaGridInstalaciones()
    {
        ClienteInstalacion item = new ClienteInstalacion();
        ClienteInstalacionController controllerInstalacion = new ClienteInstalacionController();
        item.cin_cliente = Id;
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
                string query = Server.UrlEncode(Tools.Crypto.Encrypt("Id=" + id + "&Cliente=" + Id));

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
        string query = Server.UrlEncode(Tools.Crypto.Encrypt("Cliente=" + Id));
        Tools.tools.ClientExecute("abrirInstalacion('" + query + "')");
    }

    protected void lnkEliminarInstalacion_Click(object sender, EventArgs e)
    {

    }
}