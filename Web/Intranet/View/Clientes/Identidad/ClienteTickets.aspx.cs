using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class View_Clientes_Identidad_ClienteTickets : System.Web.UI.Page
{
    MesaAyudaController controller = new MesaAyudaController();
    MesaAyuda mesaAyuda = new MesaAyuda();
    public int Cliente
    {
        get { return Convert.ToInt32(ViewState["Cliente"]); }
        set { ViewState.Add("Cliente", value); }
    }

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
            CargarModuloSistema(cboModuloSistema);
            CargaDatos();
            udPanel.Update();
        }

        CargaDatos();
        CargarModuloSistema(cboModuloSistema);
        udPanel.Update();
    }


    private void CargarModuloSistema(RadComboBox cbo)
    {
        if (cbo.Items.Count == 0)
        {
            var modulos = controller.GetModuloSistema();
            if (modulos.Count > 0)
            {
                cbo.AppendDataBoundItems = true;
                cbo.Items.Add(new RadComboBoxItem("Seleccione...", ""));
            }
            cbo.DataSource = modulos;
            cbo.DataValueField = "mod_id";
            cbo.DataTextField = "mod_nombre";
            cbo.DataBind();
        }
    }

    protected void CargaDatos()
    {
        if (bool.Parse(LeaseCheck.Session.Usuario_Es_Cliente()))
        {
            if (!IsPostBack)
            {

            }
        }
    }

    protected void btnEnviarConsulta_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();

            mesaAyuda.mes_nombre = txtNombreMesaAyuda.Text;
            mesaAyuda.mes_mensaje = txtMensaje.Text;
            mesaAyuda.mes_id_modulo = int.Parse(cboModuloSistema.SelectedValue.ToString());
            mesaAyuda.mes_otro_modulo = txtOtroModulo.Text;

            respuesta = controller.InsertMesaAyuda(mesaAyuda);
            int numeroConsulta = respuesta.codigo;

            if (!respuesta.error)
            {
                Tools.tools.ClientAlert(respuesta.detalle + "N° Consulta: " + numeroConsulta, "ok");
                Limpiar();
                udPanel.Update();

            }
            else
            {
                Tools.tools.ClientAlert(respuesta.detalle, "error");
            }
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.Message, "error");
        }
    }


    protected void Limpiar()
    {
        txtNombreMesaAyuda.Text = "";
        cboModuloSistema.SelectedIndex = 0;
        txtOtroModulo.Text = "";
        txtMensaje.Text = "";
    }

    protected void lnkAbrirTickets_Click(object sender, EventArgs e)
    {
        string query = Server.UrlEncode(Tools.Crypto.Encrypt("Cliente=" + 0));
        Tools.tools.ClientExecute("abrir('" + query + "')");
    }

}