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

public partial class View_Clientes_Identidad_ClientePropiedadMedioVisualizador : System.Web.UI.Page
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

        if (!IsPostBack)
        {
            CargarDatos();
        }
    }

    protected void CargarDatos()
    {
        ClientePropiedadMedio medio = new ClientePropiedadMedio();
        ClientePropiedadController clientePropiedadController = new ClientePropiedadController();

        medio.cpm_id = Id;  

        // Realiza la consulta para obtener los binarios
        ClientePropiedadMedioBinario binario = clientePropiedadController.GetClientePropiedadMedioArchivo(medio);

        if (binario != null && binario.cmb_binario != null)
        {
            lblDescripcion.Text = binario.DESCRIPCION;
            imgPropiedad.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(binario.cmb_binario);
        }
    }


    protected void rptImagenes_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            // Obtener los controles dentro del Repeater
            var imgPropiedad = (Image)e.Item.FindControl("imgPropiedad");
            var lblFecha = (Label)e.Item.FindControl("lblFecha");

            // Verificar si el objeto binario no es nulo
            var binario = (ClientePropiedadMedioBinario)e.Item.DataItem;

           

            // Asignar la fecha si está disponible
            if (!string.IsNullOrEmpty(binario.DESCRIPCION))
            {
                lblFecha.Text = binario.DESCRIPCION;
            }
        }
    }

}