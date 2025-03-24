using System;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Tools
{

    public static class tools
    {
        #region Alerts
        public static void ClientAlert(string Message)
        {
            Page page = (Page)HttpContext.Current.Handler;
            Message = Message.Replace("\"", "");
            Message = Message.Replace("''", "'");
            Message = Message.Replace("''''", "'");
            Message = Message.Replace("\n", "");
            Message = Message.Replace("\r", "");
            RegisterScriptBlock(page, "alert" + DateTime.Now.ToString(), "alert(\"" + Message + "\");");
        }

        public static void ClientAlert(string mensaje, string tipo, bool cerrar = false)
        {
            mensaje = mensaje.Replace("\"", "").Replace("''", "'").Replace("''''", "'").Replace("'", "").Replace("\n", "").Replace("\r", "");

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("function ClientAlert(mensaje, tipo) {");
            stringBuilder.AppendLine("$(\"#myModal\").remove();");
            stringBuilder.AppendLine("var icono = ''; var botonClase = '';");
            stringBuilder.AppendLine("switch (tipo) {");
            stringBuilder.AppendLine(" case 'error':");
            stringBuilder.AppendLine("     icono = '<div class=\"sa\"><div class=\"sa-error\"><div class=\"sa-error-x\"><div class=\"sa-error-left\"></div><div class=\"sa-error-right\"></div></div><div class=\"sa-error-placeholder\"></div><div class=\"sa-error-fix\"></div></div></div>'; ");
            stringBuilder.AppendLine("     botonClase = 'ButtonSweetError';");
            stringBuilder.AppendLine("     break;");
            stringBuilder.AppendLine(" case 'ok':");
            stringBuilder.AppendLine("     icono = '<div class=\"sa\"><div class=\"sa-success\"><div class=\"sa-success-tip\"></div><div class=\"sa-success-long\"></div><div class=\"sa-success-placeholder\"></div><div class=\"sa-success-fix\"></div></div></div>'; ");
            stringBuilder.AppendLine("     botonClase = 'ButtonSweetOk';");
            stringBuilder.AppendLine("     break;");
            stringBuilder.AppendLine(" case 'alerta':");
            stringBuilder.AppendLine("     icono = '<div class=\"sa\"><div class=\"sa-warning\"><div class=\"sa-warning-body\"></div><div class=\"sa-warning-dot\"></div></div></div>'; ");
            stringBuilder.AppendLine("     botonClase = 'ButtonSweetAlerta';");
            stringBuilder.AppendLine("     break;");
            stringBuilder.AppendLine("}");
            stringBuilder.AppendLine("var modal = '';");
            stringBuilder.AppendLine("modal += '<div class=\"modal fade\" id=\"myModal\" role=\"dialog\" data-backdrop=\"static\" data-keyboard=\"false\">';");
            stringBuilder.AppendLine("modal += '   <div class=\"modal-dialog\">';");
            stringBuilder.AppendLine("modal += '       <div class=\"modal-content\">';");
            stringBuilder.AppendLine("modal += '           <div class=\"modal-header\">';");
            stringBuilder.AppendLine("modal += '               ' + icono + ' ';");
            stringBuilder.AppendLine("modal += '               <h4 class=\"modal-title\"></h4>';");
            stringBuilder.AppendLine("modal += '           </div>';");
            stringBuilder.AppendLine("modal += '           <div class=\"modal-body\">';");
            stringBuilder.AppendLine("modal += '               <div style=\"font-size:14px;\">';");
            stringBuilder.AppendLine("modal += '                   <p>' + mensaje + '</p>';");
            stringBuilder.AppendLine("modal += '               </div>';");
            stringBuilder.AppendLine("modal += '           </div>';");
            stringBuilder.AppendLine("modal += '           <div class=\"modal-footer\">';");
            stringBuilder.AppendLine("modal += '               <button type=\"button\" class=\"' + botonClase + '\" onclick=\"CloseWindows()\">Aceptar</button>';");
            stringBuilder.AppendLine("modal += '           </div>';");
            stringBuilder.AppendLine("modal += '       </div>';");
            stringBuilder.AppendLine("modal += '   </div>';");
            stringBuilder.AppendLine("modal += '</div>';");
            stringBuilder.AppendLine("$(\"body\").append(modal);");
            stringBuilder.AppendLine("$(\"#myModal\").modal();");
            stringBuilder.AppendLine("}");

            stringBuilder.AppendLine("function CloseWindows() {");
            if (cerrar)
            {
                stringBuilder.AppendLine("if (window.closeWindow) window.closeWindow();");
            }
            stringBuilder.AppendLine("$('#myModal').modal('hide');");
            stringBuilder.AppendLine("}");

            Page page = (Page)HttpContext.Current.Handler;
            RegisterScriptBlock(page, "ClientAlert" + DateTime.Now.ToString(), stringBuilder.ToString());
            ClientExecute("ClientAlert('" + mensaje + "','" + tipo + "');");
        }


        #endregion

        public static void ClientClose()
        {
            Page page = (Page)HttpContext.Current.Handler;
            RegisterStartupScript(page, "close", "close();");
        }

        public static void ClientExecute(string Script)
        {
            Page page = (Page)HttpContext.Current.Handler;
            RegisterStartupScript(page, "execute" + DateTime.Now.ToString(), Script);
        }

        public static bool IsStartupScriptRegistered(string key)
        {
            Page page = (Page)HttpContext.Current.Handler;
            return page.ClientScript.IsStartupScriptRegistered(typeof(Page), key);
        }

        public static bool IsStartupScriptRegistered(System.Web.UI.Page Page, string key)
        {
            return Page.ClientScript.IsStartupScriptRegistered(typeof(Page), key);
        }

        public static bool IsClientScriptBlockRegistered(string key)
        {
            Page page = (Page)HttpContext.Current.Handler;
            return page.ClientScript.IsClientScriptBlockRegistered(typeof(Page), key);
        }

        public static bool IsClientScriptBlockRegistered(System.Web.UI.Page Page, string key)
        {
            return Page.ClientScript.IsClientScriptBlockRegistered(typeof(Page), key);
        }

        public static void RegisterStartupScript(string key, string Script)
        {
            Page page = (Page)HttpContext.Current.Handler;
            RegisterStartupScript(page, key, Script);
        }

        public static void RegisterStartupScript(System.Web.UI.Page Page, string key, string Script)
        {
            if (ScriptManager.GetCurrent(Page) != null)
            {
                ScriptManager.RegisterStartupScript(Page, typeof(Page), key, Script, true);
            }
            else
            {
                if (!Page.ClientScript.IsStartupScriptRegistered(typeof(Page), key))
                {
                    Page.ClientScript.RegisterStartupScript(typeof(Page), key, Script, true);
                }
            }

        }

        public static void RegisterScriptBlock(string key, string Script)
        {
            Page page = (Page)HttpContext.Current.Handler;
            RegisterScriptBlock(page, key, Script);
        }

        public static void RegisterScriptBlock(System.Web.UI.Page Page, string key, string Script)
        {
            if (ScriptManager.GetCurrent(Page) != null)
            {
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), key, Script, true);
            }
            else
            {
                if (!Page.ClientScript.IsClientScriptBlockRegistered(typeof(Page), key))
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), key, Script, true);
                }
            }

        }

        public static void RegisterWebResource(Control Ctrl, string ResourceName)
        {
            string url = Ctrl.Page.ClientScript.GetWebResourceUrl(Ctrl.GetType(), ResourceName);
            string key = ResourceName.Replace(".", "_");

            if (ScriptManager.GetCurrent(Ctrl.Page) != null)
            {
                ScriptManager.RegisterClientScriptInclude(Ctrl.Page, Ctrl.Page.GetType(), key, url);
            }
            else
            {
                if (!Ctrl.Page.ClientScript.IsClientScriptIncludeRegistered(key))
                {
                    Ctrl.Page.ClientScript.RegisterClientScriptInclude(key, url);
                }
            }

        }

        public static void RegisterPostBackScript(Control ctrl)
        {
            RegisterPostBackScript(ctrl, "", "refresh");
        }

        public static void RegisterPostBackScript(Control ctrl, string argument, string scriptName)
        {
            if (scriptName.Length == 0)
            {
                throw new ArgumentException("Debe especificar un nombre para la función.");
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("function " + scriptName + "()");
            sb.AppendLine("{");
            sb.AppendLine(ctrl.Page.ClientScript.GetPostBackEventReference(ctrl, argument));
            sb.AppendLine("}");

            RegisterScriptBlock(ctrl.Page, DateTime.Now.ToString(), sb.ToString());

        }


    }

}

