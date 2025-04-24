using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.IO;
using System.Web.UI;
using Telerik.Web.UI;


public partial class View_Comun_Controls_Cliente_Identidad : System.Web.UI.UserControl
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
        }
        tieneRut();
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
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargaDatos();
            CargaComuna();
        }
        CargaDatos();
        CargaComuna();
        ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnGuardar);

    }

    protected void CargaComuna()
    {
        Comuna filtro = new Comuna();
        filtro.cmn_pais = int.Parse(cboPais.SelectedValue);

        cboComuna.Items.Clear();
        cboComuna.DataSource = controller.GetComunas(filtro);
        cboComuna.DataValueField = "cmn_id";
        cboComuna.DataTextField = "cmn_nombre";
        cboComuna.DataBind();
    }

    protected void CargaDatos()
    {
        if (IdCliente > 0)
        {
            if (!IsPostBack)
            {
                Cliente cliente = new Cliente();
                cliente.cli_id = IdCliente;
                cliente = controller.GetCliente(cliente);

                lblID.Text = cliente.cli_id.ToString();
                txtNombre.Text = cliente.cli_nombre;
                txtGiro.Text = cliente.cli_giro;
                txtRut.Text = cliente.cli_rut.ToString();
                txtDv.Text = cliente.cli_dv;
                txtIdentificador.Text = cliente.cli_identificador;
                txtAlias.Text = cliente.cli_alias;
                cboPais.SelectedValue = cliente.cli_pais.ToString();
                cboComuna.SelectedValue = cliente.cli_comuna.ToString();
                txtDireccion.Text = cliente.cli_direccion;
                txtEmail.Text = cliente.cli_email;
                txtTelefono.Text = cliente.cli_telefono;
                chkDemo.Checked = cliente.cli_es_demo;
                txtContactoNombre.Text = cliente.cli_contacto_nombre;
                txtContactoEmail.Text = cliente.cli_contacto_email;
                txtContactoTelefono.Text = cliente.cli_contacto_telefono;

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

        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (!ValidaArchivo()) return;

        try
        {
            Respuesta respuesta = new Respuesta();

            Cliente cliente = new Cliente();
            cliente.cli_id = IdCliente;
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

            if (IdCliente > 0)
                respuesta = controller.UpdateCliente(cliente);
            else
            {
                respuesta = controller.InsertCliente(cliente);
                IdCliente = respuesta.codigo;
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


}