using LeaseCheck.Controller;
using LeaseCheck.Model;
using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.IO;
using System.Web.UI;
using Telerik.Web.UI;


public partial class View_Comun_Controls_Cliente_IdentidadInstalacion : System.Web.UI.UserControl
{
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

    public bool ReadOnly
    {
        get { return Convert.ToBoolean(ViewState["ReadOnly"]); }
        set { ViewState.Add("ReadOnly", value); }
    }

    private ClienteInstalacionController clienteInstalacionController = new ClienteInstalacionController();


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
                    case "Cliente":
                        Cliente = Int32.Parse(array[1].ToString());
                        break;
                    case "IdClienteInstalacion":
                        IdClienteInstalacion = Int32.Parse(array[1].ToString());
                        break;
                }
            }
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
            CargaDatos();

        if (IdClienteInstalacion == 0)
            lblTitulo.Text = "Identidad de la instalación";

        CargaDatos();

    }

    protected void CargaDatos()
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
            txtTelefono.Text = clienteInstalacion.cin_telefono;

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
        }

    }



    //Guardar Instalacion
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();

            ClienteInstalacion item = new ClienteInstalacion();
            item.cin_id = IdClienteInstalacion;
            item.cin_cliente = Cliente;
            item.cin_nombre = txtNombre.Text;
            item.cin_descripcion = txtDescripcion.Text;
            item.cin_direccion = txtDireccion.Text;
            item.cin_telefono = txtTelefono.Text;


            if (rdbSi.Checked == true)
                item.cin_habilitado = true;
            else
                item.cin_habilitado = false;

            if (IdClienteInstalacion > 0)
            {
                respuesta = clienteInstalacionController.UpdateClienteInstalacion(item);
                Tools.tools.ClientAlert(respuesta.detalle, "ok");
            }
            else
                respuesta = clienteInstalacionController.InsertClienteInstalacion(item);
            Tools.tools.ClientAlert(respuesta.detalle, "ok");
            IdClienteInstalacion = respuesta.codigo;
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.Message, "error");
        }
    }




}