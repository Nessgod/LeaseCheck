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
using Telerik.Web.UI.ImageEditor;

public partial class View_Root_Mantenedores_Cliente_ClienteDocumento : System.Web.UI.Page
{
    protected int Id
    {
        get { return Convert.ToInt32(ViewState["Id"]); }
        set { ViewState.Add("Id", value); }
    }
    protected int IdCliente
    {
        get { return Convert.ToInt32(ViewState["IdCliente"]); }
        set { ViewState.Add("IdCliente", value); }
    }

    ClienteDocumentoController controller = new ClienteDocumentoController();
    
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
                    case "IdCliente":
                        IdCliente = Int32.Parse(array[1].ToString());
                        break;
                }
            }

        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnGuardar);
    }

    //Guardar documento
    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();
            ClienteDocumento clienteDocumento = new ClienteDocumento();

            if (fldDocumento.HasFile)
            {
                clienteDocumento.cdo_id_cliente = IdCliente;
                clienteDocumento.arc_nombre_archivo = fldDocumento.FileName;

                clienteDocumento.arc_contenido = fldDocumento.PostedFile.ContentType;
                clienteDocumento.arc_extension = Path.GetExtension(fldDocumento.FileName);
                clienteDocumento.arc_tamano = int.Parse(fldDocumento.FileContent.Length.ToString());
                clienteDocumento.abi_archivo_binario = fldDocumento.FileBytes;

                clienteDocumento.cdo_descripcion = txtDescripcionDocumento.Text;
                respuesta = controller.InsertDocumentos(clienteDocumento);

                if (!respuesta.error)
                    Tools.tools.ClientAlert(respuesta.detalle, "ok", true);
                else
                    Tools.tools.ClientAlert(respuesta.detalle, "alerta");

                //IdDocumentos = respuesta.codigo;
            }
            else
            {
                Tools.tools.ClientAlert("No ha adjuntado Documento");
            }

        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.ToString(), "error");
        }
    }

}