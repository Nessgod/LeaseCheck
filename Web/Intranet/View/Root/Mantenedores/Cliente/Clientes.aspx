<%@ Page Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="Clientes.aspx.cs" Inherits="View_Root_Mantenedores_Cliente_Clientes" %>

<%@ Register Src="~/View/Root/Mantenedores/Cliente/Controls/Clientes.ascx" TagPrefix="wuc" TagName="Clientes" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphHeder" runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="chpScript" runat="server">

</asp:Content>

<asp:Content ID="ContentTitulo" ContentPlaceHolderID="cphTitulo" runat="Server">
    Clientes
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphFiltro" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="cphBody" runat="Server">
    
    <wuc:Clientes runat="server" ID="wucClientes" URLNuevoCliente="~/View/Root/Mantenedores/Cliente/Cliente.aspx" />        
   
</asp:Content>