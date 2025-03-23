using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class View_Clientes_Identidad_Cliente : System.Web.UI.Page
{
    private ClienteController controller = new ClienteController();

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
        CargaDatos();
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
                Cliente cliente = new Cliente();
                cliente = controller.GetClienteIdentidad();

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

                if (cliente.cli_es_demo)
                    chkDemo.Text = "Si";
                else
                    chkDemo.Text = "No";

                Comuna filtro = new Comuna();
                filtro.cmn_pais = cliente.cli_pais;

                cboComuna.Items.Clear();
                cboComuna.DataSource = controller.GetComunas(filtro);
                cboComuna.DataValueField = "cmn_id";
                cboComuna.DataTextField = "cmn_nombre";
                cboComuna.SelectedValue = cliente.cli_comuna.ToString();
                cboComuna.DataBind();

                if (cliente.cli_logo != null)
                {
                    string base64String = Convert.ToBase64String(cliente.cli_logo, 0, cliente.cli_logo.Length);
                    imgLogo.ImageUrl = "data:image/jpeg;base64," + base64String;
                }
            }
        }

        //txtNombre.Enabled = false;
        //txtGiro.Enabled = false;
        //txtRut.Enabled = false;
        //txtDv.Enabled = false;
        //txtAlias.Enabled = false;
        //cboPais.Enabled = false;
        //cboComuna.Enabled = false;
        //txtDireccion.Enabled = false;
        //txtEmail.Enabled = false;
        //txtTelefono.Enabled = false;
        //txtContactoNombre.Enabled = false;
        //txtContactoEmail.Enabled = false;
        //txtContactoTelefono.Enabled = false;
        //chkDemo.Enabled = false;

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
    }
}