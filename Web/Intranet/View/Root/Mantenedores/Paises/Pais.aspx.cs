using System;
using System.IO;
using System.Web.UI;
using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;

public partial class View_Root_Mantenedores_Paises_Pais : System.Web.UI.Page
{
    private PaisesController paisesController = new PaisesController();
    Paises pais = new Paises();

    protected int Id
    {
        get { return Convert.ToInt32(ViewState["Id"]); }
        set { ViewState.Add("Id", value); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        #region SeguridadPagina
        MenuPerfil ver = new MenuPerfil();
        ver.mpe_menu = (int)LeaseCheck.Paginas.menu_pais.Ver;
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
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
       CargarDatos();
       ScriptManager.GetCurrent(Page).RegisterPostBackControl(btnGuardar);
    }

    protected void CargarDatos()
    {
        if (Id > 0)
        {
            try
            { 
                pais.pai_id = Id;
                pais = paisesController.GetPais(pais);
                txtNombre.Text = pais.pai_nombre;

                if (pais.pai_suma_resta == "1")
                    rbtDiferencia.SelectedValue = "1";
                else
                    rbtDiferencia.SelectedValue = "2";

                txtHora.Text = pais.pai_hora.ToString();

                if (pais.pai_habilitado)
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
            catch (Exception ex)
            {
                Tools.tools.ClientExecute("Mensaje_False_Detalle_Data('" + ex.Message + "')");
            }
        }
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        try
        {
            Respuesta respuesta = new Respuesta();            

            pais.pai_id = Id;
            pais.pai_nombre = txtNombre.Text;
            pais.pai_suma_resta = rbtDiferencia.SelectedValue;
            pais.pai_hora = int.Parse(txtHora.Text);

            if (rdbSi.Checked)
                pais.pai_habilitado = true;
            else
                pais.pai_habilitado = false;

            if (fldImagen.HasFile) 
            {
                if (Path.GetExtension(fldImagen.FileName).ToUpper() == ".PNG" |
                    Path.GetExtension(fldImagen.FileName).ToUpper() == ".JPG") {
                    pais.pai_imagen = fldImagen.FileBytes;
                    pais.pai_nombre_imagen = fldImagen.FileName;
                    pais.pai_extension = Path.GetExtension(fldImagen.FileName);

                }
               
            }
             
            if (Id > 0)
            {
                respuesta = paisesController.UpdatePais(pais);
            }
            else
            {
                respuesta = paisesController.InsertPais(pais);
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