<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Simple.master" AutoEventWireup="true" CodeFile="Producto.aspx.cs" Inherits="View_Root_Mantenedores_Productos_Producto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeder" Runat="Server">
</asp:Content>

<asp:Content ID="ContentScript" ContentPlaceHolderID="chpScript" Runat="Server">
    <script type="text/javascript" language="javascript">
        function getRadWindow() {
            var oWindow = null;
            if (window.radWindow)
                oWindow = window.radWindow;
            else if (window.frameElement.radWindow)
                oWindow = window.frameElement.radWindow;
            return oWindow;
        }

        function closeWindow() {
            var window = getRadWindow();
            if (window.BrowserWindow.refresh) window.BrowserWindow.refresh();
            window.close();
        }

        function abrirDocumento(query) {
            var oWin = $find("<%=rwiDetalle.ClientID %>");
            oWin.setUrl('<%=ResolveUrl("~/View/Root/Mantenedores/Productos/ProductoPlanDocumento.aspx") %>?query=' + query);
            oWin.show();
            bloqueaScroll(false);
        }

        function abrirServicio(query) {
            var oWin = $find("<%=rwiDetalle.ClientID %>");
            oWin.setUrl('<%=ResolveUrl("~/View/Root/Mantenedores/Productos/ProductoPlanServicio.aspx") %>?query=' + query);
            oWin.show();
            bloqueaScroll(false);
        }



        function refresh() {
            __doPostBack("<%=Grid.ClientID %>", '')
        }


        function refresh() {
            __doPostBack("<%=GridServicio.ClientID %>", '')
        }

    </script>
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="cphBody" Runat="Server">
    <rad:RadWindow2 ID="rwiDetalle" runat="server" />
    <asp:UpdatePanel runat="server" ID="udPanel"  UpdateMode="Conditional" >
        <ContentTemplate>
            <div class="SubTitulos">Productos</div>
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
	            <div class="form-group col-lg-2 col-md-2 col-xs-12"> 
		            <label>Nombre(*)</label>
	            </div>
	            <div class="form-group col-lg-10 col-md-10 col-xs-12"> 
		            <WebControls:TextBox2 ID="txtNombre" runat="server" />
                    <asp:CustomValidator ID="CustomValidator1" runat="server" 
                        ControlToValidate="txtNombre" 
                        ValidateEmptyText="true"
                        ClientValidationFunction="validaControl" 
                        ValidationGroup="Identidad" />
	            </div>
            </div>
            
            <div class="row col-lg-12 col-md-12 col-xs-12 ">
	            <div class="form-group col-lg-2 col-md-2 col-xs-12"> 
		            <label>Habilitado(*)</label>
	            </div>
	            <div class="form-group col-lg-10 col-md-10 col-xs-12"> 
		            <asp:RadioButton ID="rdbSi" runat="server" Text="SI" GroupName="Habilitado" />
                    <asp:RadioButton ID="rdbNo" runat="server" Text="NO" GroupName="Habilitado" />
	            </div>
            </div>

            <asp:Panel ID="pnlDocumento" runat="server" Visible="true">
                <div class="SubTitulos">Documentos Asociados</div>
                <rad:RadGrid2 ID="Grid" runat="server">
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="prd_id">
                        <CommandItemTemplate>
                            <div>
                                <asp:LinkButton ID="lnlNuevo" runat="server" Text="Nuevo" CssClass="icono_guardar" OnClick="lnlNuevo_Click" />
                                <asp:LinkButton ID="lnkEliminar" runat="server" Text="Eliminar" CssClass="icono_eliminar" OnClick="lnkEliminar_Click"
                                    OnClientClick="return ConfirSweetAlert(this, '', '¿Esta seguro(a) que desea eliminar los registros seleccionados?');" />
                            </div>
                        </CommandItemTemplate>
                    </MasterTableView>
                    <ClientSettings>
                        <Scrolling AllowScroll="True"  />
                    </ClientSettings>
                </rad:RadGrid2>
                <br />
            </asp:Panel>

              <asp:Panel ID="pnlServicios" runat="server" Visible="true">
                  <div class="SubTitulos">Servicios Asociados</div>
                  <rad:RadGrid2 ID="GridServicio" runat="server">
                      <MasterTableView CommandItemDisplay="Top" DataKeyNames="psc_id">
                          <CommandItemTemplate>
                              <div>
                                  <asp:LinkButton ID="lnlNuevoServicio" runat="server" Text="Nuevo" CssClass="icono_guardar" OnClick="lnlNuevoServicio_Click" />
                                  <asp:LinkButton ID="lnlEliminarServicio" runat="server" Text="Eliminar" CssClass="icono_eliminar" OnClick="lnlEliminarServicio_Click"
                                      OnClientClick="return ConfirSweetAlert(this, '', '¿Esta seguro(a) que desea eliminar los registros seleccionados?');" />
                              </div>
                          </CommandItemTemplate>
                      </MasterTableView>
                      <ClientSettings>
                          <Scrolling AllowScroll="True" ScrollHeight="320" />
                      </ClientSettings>
                  </rad:RadGrid2>
                  <br />
              </asp:Panel>
            <div class="col-lg-12 col-md-12 col-xs-12 form-col-center">
                </br>
                <WebControls:PushButton ID="btnGuardar" runat="server" Text="Guardar" ValidationGroup="Identidad" OnClick="btnGuardar_Click"/>
                <WebControls:PushButton ID="btnCerrar" runat="server" Text="Cerrar" OnClientClick="closeWindow();" CssClass="ButtonCerrar"/>
           </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>