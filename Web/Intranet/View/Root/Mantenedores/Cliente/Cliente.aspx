<%@ Page Language="C#" MasterPageFile="~/Master/Default.master" AutoEventWireup="true" CodeFile="Cliente.aspx.cs" Inherits="View_Root_Mantenedores_Cliente_Cliente" %>

<%@ Register Src="~/View/Root/Mantenedores/Cliente/Controls/Cliente.ascx" TagPrefix="wuc" TagName="Cliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphHeder" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="chpScript" runat="server">
</asp:Content>

<asp:Content ID="ContentTitulo" ContentPlaceHolderID="cphTitulo" runat="Server">
    Cliente
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cphFiltro" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="cphBody" runat="Server">

    <wuc:Cliente runat="server" id="wucCliente" URLVolverCliente="~/View/Root/Mantenedores/Cliente/Clientes.aspx" />
</asp:Content>
