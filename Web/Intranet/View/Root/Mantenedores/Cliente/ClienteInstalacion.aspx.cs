using LeaseCheck.Clientes.Model;
using LeaseCheck.Controller;
using LeaseCheck.Model;
using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class View_Root_Mantenedores_Cliente_ClienteInstalacion : System.Web.UI.Page
{
    protected int Id
    {
        get { return Convert.ToInt32(ViewState["Id"]); }
        set { ViewState.Add("Id", value); }
    }

    protected string tipo_dato
    {
        get { return Convert.ToString(ViewState["tipo_dato"]); }
        set { ViewState.Add("tipo_dato", value); }
    }

    protected int Cliente
    {
        get { return Convert.ToInt32(ViewState["Cliente"]); }
        set { ViewState.Add("Cliente", value); }
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
                    case "Id":
                        Id = Int32.Parse(array[1].ToString());
                        break;

                    case "Cliente":
                        Cliente = Int32.Parse(array[1].ToString());
                        break;

                    case "tipo_dato":
                        tipo_dato = array[1].ToString();
                        break;
                }
            }
            
            
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (!IsPostBack)
            CargaDatos();

        if (Id == 0)
        {
            lblTitulo.Text = "Identidad de la instalación";


        }
        else
        {

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
                    
                }
            }
        }
    }

    protected void CargaDatos()
    {
        if (Id > 0)
        {
            ClienteInstalacionController clienteInstalacionController = new ClienteInstalacionController();
            ClienteInstalacion clienteInstalacion = new ClienteInstalacion();

            clienteInstalacion.cin_id = Id;
            clienteInstalacion = clienteInstalacionController.GetClienteInstalacion(clienteInstalacion);

            lblId.Text = Id.ToString();
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
            item.cin_id = Id;
            item.cin_cliente = Cliente;
            item.cin_nombre = txtNombre.Text;
            item.cin_descripcion = txtDescripcion.Text;
            item.cin_direccion = txtDireccion.Text;
            item.cin_telefono = txtTelefono.Text;


            if (rdbSi.Checked == true)
                item.cin_habilitado = true;
            else
                item.cin_habilitado = false;

            if (Id > 0)
                respuesta = clienteInstalacionController.UpdateClienteInstalacion(item);
            else
                respuesta = clienteInstalacionController.InsertClienteInstalacion(item);

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

}