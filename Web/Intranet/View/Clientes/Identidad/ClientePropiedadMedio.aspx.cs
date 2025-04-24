using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Telerik.Web.UI.ImageEditor;

public partial class View_Clientes_Identidad_ClientePropiedadMedio : System.Web.UI.Page
{
    protected int Id
    {
        get { return Convert.ToInt32(ViewState["Id"]); }
        set { ViewState.Add("Id", value); }
    }
    protected int IdPropiedad
    {
        get { return Convert.ToInt32(ViewState["IdPropiedad"]); }
        set { ViewState.Add("IdPropiedad", value); }
    }

    ClientePropiedadController controller = new ClientePropiedadController();

    protected void Page_Load(object sender, EventArgs e)
    {
        //#region SeguridadPagina
        //MenuPerfil ver = new MenuPerfil();
        //ver.mpe_menu = (int)LeaseCheck.Paginas.menu_cliente.Ver;
        //LeaseCheck.Token.SecurityManagerVer(ver);
        //#endregion

        if (!IsPostBack)
        {
            string[] query = Tools.Crypto.Decrypt(Server.UrlDecode(Request.QueryString["query"].ToString())).Split('&');

            foreach (string arr in query)
            {
                string[] array = arr.ToString().Split('=');
                switch (array[0].ToString())
                {
                    case "IdPropiedad":
                        IdPropiedad = Int32.Parse(array[1].ToString());
                        break;

                    case "Id":
                        Id = Int32.Parse(array[1].ToString());
                        break;
                }
            }

        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnGuardar);

        if (!IsPostBack)
        {
            CargarDatos();
        }
    }

    protected void CargarDatos()
    {
        ClientePropiedadMedioBinario binario = new ClientePropiedadMedioBinario();
        ClientePropiedadMedio medio = new ClientePropiedadMedio();
        ClientePropiedadController clientePropiedadController = new ClientePropiedadController();

        medio.cpm_id = Id;

        if (Id > 0)
        {
            // Realiza la consulta primero
            medio = clientePropiedadController.GetClientePropiedadMedio(medio);

            if (medio.cpm_id > 0)
            {
                lblTitulo.Text = "ID:" + medio.cpm_id + " | " + medio.cpm_descripcion;
                txtDescripcion.Text = medio.cpm_descripcion;
                txtLink.Text = medio.cpm_link;



                if (bool.Parse(medio.cpm_imagen.ToString()))
                {
                    esImagenSi.Checked = true;
                    esVideoSi.Checked = false;
                }
                else
                {
                    esImagenSi.Checked = false;
                    esVideoSi.Checked = true;
                }

                if (bool.Parse(medio.cpm_video.ToString()))
                {
                    esVideoSi.Checked = true;
                    esImagenSi.Checked = false;
                }
                else
                {
                    esVideoSi.Checked = false;
                    esImagenSi.Checked = true;
                }
            }
        }
        else
        {
            lblTitulo.Text = "Nuevo medio";
        }


    }
    //Guardar imagen / video
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();
            ClientePropiedadMedio clientePropiedadMedio = new ClientePropiedadMedio();
            ClientePropiedadMedioBinario binario = new ClientePropiedadMedioBinario();

            clientePropiedadMedio.cpm_id = Id;
            clientePropiedadMedio.cpm_id_propiedad = IdPropiedad;
            clientePropiedadMedio.cpm_link = txtLink.Text;
            clientePropiedadMedio.cpm_descripcion = txtDescripcion.Text;
            clientePropiedadMedio.cpm_imagen = esImagenSi.Checked;
            clientePropiedadMedio.cpm_video = esVideoSi.Checked;

            if (Id > 0)
            {
                if (fldImagen.FileBytes != null && fldImagen.FileBytes.Length > 0)
                {
                    binario.cmb_binario = fldImagen.FileBytes;
                    respuesta = controller.UpdateClientePropiedadMedioImagen(clientePropiedadMedio, binario);
                }
                else
                {
                    respuesta = controller.UpdateClientePropiedadMedioVideo(clientePropiedadMedio);
                }
            }
            else
            {
                if (fldImagen.FileBytes != null && fldImagen.FileBytes.Length > 0)
                {
                    binario.cmb_binario = fldImagen.FileBytes;
                    respuesta = controller.InsertClientePropiedadImagen(clientePropiedadMedio, binario);
                }
                else
                {
                    respuesta = controller.InsertClientePropiedadVideo(clientePropiedadMedio, null);
                }
            }

            if (!respuesta.error)
                Tools.tools.ClientAlert(respuesta.detalle, "ok", true);
            else
                Tools.tools.ClientAlert(respuesta.detalle, "alerta");
        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.ToString(), "error");
        }
    }

}