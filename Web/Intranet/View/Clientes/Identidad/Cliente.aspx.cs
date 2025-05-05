using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Linq;
using Telerik.Web.UI;
using System.IO;
using System.Web.UI;
public partial class View_Clientes_Identidad_Cliente : System.Web.UI.Page
{
    private ClienteController controller = new ClienteController();
    public int Cliente
    {
        get { return Convert.ToInt32(ViewState["Cliente"]); }
        set { ViewState.Add("Cliente", value); }
    }
    // Cambio Benjamins
    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_cliente_identidad.Ver;
        LeaseCheck.Token.SecurityManagerVer(ver);
        #endregion
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
        if (bool.Parse(LeaseCheck.Session.Usuario_Es_Cliente()))
        {
            if (!IsPostBack)
            {
                CargarInformacionCliente();
            }

            CargarInformacionCliente(); // también se ejecuta en postbacks
        }
    }

    private void CargarInformacionCliente()
    {
        Cliente cliente = controller.GetClienteIdentidad();
        Cliente = cliente.cli_id;
        txtNombre.Text = cliente.cli_nombre;
        txtGiro.Text = cliente.cli_giro;
        txtRut.Text = cliente.cli_rut.ToString();
        txtDv.Text = cliente.cli_dv;
        txtAlias.Text = cliente.cli_alias;
        cboPais.SelectedValue = cliente.cli_pais.ToString();
        txtDireccion.Text = cliente.cli_direccion;
        txtEmail.Text = cliente.cli_email;
        txtTelefono.Text = cliente.cli_telefono;
        txtContactoNombre.Text = cliente.cli_contacto_nombre;
        txtContactoEmail.Text = cliente.cli_contacto_email;
        txtContactoTelefono.Text = cliente.cli_contacto_telefono;

        chkDemo.Text = cliente.cli_es_demo ? "Si" : "No";

        Comuna filtro = new Comuna();
        filtro.cmn_pais = cliente.cli_pais;

        cboComuna.Items.Clear();
        cboComuna.DataSource = controller.GetComunas(filtro);
        cboComuna.DataValueField = "cmn_id";
        cboComuna.DataTextField = "cmn_nombre";
        cboComuna.DataBind();
        cboComuna.SelectedValue = cliente.cli_comuna.ToString();

        if (cliente.cli_logo != null)
        {
            string base64String = Convert.ToBase64String(cliente.cli_logo, 0, cliente.cli_logo.Length);
            imgLogo.ImageUrl = "data:image/jpeg;base64," + base64String;
        }
        else
        {
            imgLogo.ImageUrl = "";
        }

        string[] perfiles = LeaseCheck.Session.UsuarioPerfil().Split(',');
        bool esAdministrador = perfiles.Contains(Convert.ToInt32(LeaseCheck.LeaseCheck.Perfiles.AdministradorCorredora).ToString());

        if (esAdministrador)
        {
            txtComisionVenta.ReadOnly = false;
            txtComisionVenta.Text = cliente.cli_comision_venta.ToString("0.##");
            btnGuardar.Visible = true;
            lblComision.Visible = true;
            txtComisionVenta.Visible = true;
            txtNombre.ReadOnly = false;
            txtGiro.ReadOnly = false;
            txtRut.ReadOnly = false;
            txtDv.ReadOnly = false;
            txtAlias.ReadOnly = false;
            cboPais.ReadOnly = false;
            cboComuna.ReadOnly = false;
            txtDireccion.ReadOnly = false;
            txtEmail.ReadOnly = false;
            txtTelefono.ReadOnly = false;
            txtContactoNombre.ReadOnly = false;
            txtContactoEmail.ReadOnly = false;
            txtContactoTelefono.ReadOnly = false;
            lblID.Visible = false;
            lblEtiquetaId.Visible = false;
        }
        else
        {
            lblID.Visible = false;
            lblComision.Visible = false;
            txtComisionVenta.Visible = false;
            btnGuardar.Visible = false;
            txtNombre.ReadOnly = true;
            txtGiro.ReadOnly = true;
            txtRut.ReadOnly = true;
            txtDv.ReadOnly = true;
            txtAlias.ReadOnly = true;
            cboPais.ReadOnly = true;
            cboComuna.ReadOnly = true;
            txtDireccion.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtTelefono.ReadOnly = true;
            txtContactoNombre.ReadOnly = true;
            txtContactoEmail.ReadOnly = true;
            txtContactoTelefono.ReadOnly = true;
            chkDemo.Enabled = false;
            fudFotoEmpresa.Visible = false;
            lblLogoEmpresa.Visible = false;
            lblEtiquetaId.Visible = false;
        }

        udPanel.Update();
    }


    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (!ValidaArchivo()) return;

        try
        {
            Respuesta respuesta = new Respuesta();
            ClienteController clienteController = new ClienteController();
            Cliente cliente = new Cliente();
            cliente.cli_id = Cliente;

            // Reemplazar coma por punto para evitar errores de conversión
            string valor = txtComisionVenta.Text.Replace(",", ".");
            double comisionVenta;

            if (double.TryParse(valor, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out comisionVenta))
                cliente.cli_comision_venta = comisionVenta;
            else
                Tools.tools.ClientAlert("El valor ingresado en la comisión de venta no tiene un formato válido.", "alerta");

            cliente.cli_nombre = txtNombre.Text;
            cliente.cli_giro = txtGiro.Text;
            cliente.cli_rut = int.Parse(txtRut.Text);
            cliente.cli_dv = txtDv.Text;
            cliente.cli_alias = txtAlias.Text;
            cliente.cli_pais = int.Parse(cboPais.SelectedValue);
            cliente.cli_comuna = int.Parse(cboComuna.SelectedValue);
            cliente.cli_direccion = txtDireccion.Text;
            cliente.cli_email = txtEmail.Text;
            cliente.cli_telefono = txtTelefono.Text;
            cliente.cli_contacto_nombre = txtContactoNombre.Text;
            cliente.cli_contacto_email = txtContactoEmail.Text;
            cliente.cli_contacto_telefono = txtContactoTelefono.Text;

            if (fudFotoEmpresa.HasFile)
                cliente.cli_logo = LeaseCheck.LeaseCheck.ReducirImagen(fudFotoEmpresa.FileBytes, 100, 100);

            if (cliente.cli_id > 0)
            {
                respuesta = clienteController.UpdateClienteComision(cliente);
                Cliente = respuesta.codigo;

                if (!respuesta.error)
                {
                    Tools.tools.ClientAlert(respuesta.detalle, "ok");
                    CargaDatos();
                    udPanel.Update();

                    // Forzar un postback completo
                    ScriptManager.RegisterStartupScript(this, GetType(), "PostbackScript", "__doPostBack();", true);
                }
                else
                {
                    Tools.tools.ClientAlert(respuesta.detalle, "alerta");
                }
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
            if (fudFotoEmpresa.HasFile)
            {
                if (Path.GetExtension(fudFotoEmpresa.FileName).ToUpper() == ".JPG" | Path.GetExtension(fudFotoEmpresa.FileName).ToUpper() == ".PNG")
                {
                    if (fudFotoEmpresa.FileBytes.Length > 2097152) //2MB (1 MB = 1048576 bytes)
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


}