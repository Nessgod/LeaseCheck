using LeaseCheck.Root.Controller;
using LeaseCheck.Root.Model;
using System;
using System.Web;

public partial class Master_Default : System.Web.UI.MasterPage
{
    PaisesController paisesController = new PaisesController();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!LeaseCheck.Token.TokenSeguridad())
        {
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (LeaseCheck.Token.TokenSeguridad())
        {
            if (LeaseCheck.Session.UsuarioFoto() != null)
            {
                string base64String = LeaseCheck.Session.UsuarioFoto();

                this.imgUsuario.ImageUrl = "data:image/jpeg;base64," + base64String;

                this.imgUsuarioLateral.ImageUrl = "data:image/jpeg;base64," + base64String;
                
            }
            else
            {
                this.imgUsuario.ImageUrl = ResolveUrl("~/Imagen/usuario-de-perfil.png");
                this.imgUsuarioLateral.ImageUrl = ResolveUrl("~/Imagen/usuario-de-perfil.png");
            }
            seleccionarPais();
        }
    }

    protected void seleccionarPais()
    {
        try
        {
            //Valido el perfil del usuario si no es root o admin tomo el pais que tiene el usuario
            #region Validaciones
            if (LeaseCheck.Session.UsuarioPerfil() != "1" && LeaseCheck.Session.UsuarioPerfil() != "2")
            {
                LeaseCheck.Session.Pais(LeaseCheck.Session.PaisBase());
            }
            #endregion


            Paises paises = new Paises();
            paises.pai_id = int.Parse(LeaseCheck.Session.Pais());
            paises = paisesController.GetPaisLogin(paises);


        }
        catch (Exception ex)
        {
            Tools.tools.ClientAlert(ex.Message, "error", true);

        }
    }

    protected void lnkCerrarSession_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Session.RemoveAll();

        Response.Redirect("~/Login.aspx");
    }
}