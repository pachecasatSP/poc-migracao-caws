<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="PerfilOperador.aspx.cs" Inherits="Presenta.CA.Site.Pages.Relatorio.PerfilOperador" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src='<%= ResolveClientUrl("~/Scripts/ca-relat.js") %>' type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            configPage();
            exibirNotificacoes();
            carregarDdlSistema();
            configChangeLog();
        });
        function configPage() {
            setGrandMenuId(3);
            setMenuHover(2);
            setPageName('Relatório - Perfil x Operador');
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="hf">
        <asp:HiddenField ID="hdfUpdate" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfInsert" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfDelete" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfError" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfText1" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfText2" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfIdSistema" runat="server" EnableViewState="true" Value="-1" />
        <asp:HiddenField ID="hdfIdAplicativo" runat="server" EnableViewState="true" Value="-1" />
    </div>
    <div style="height: 60px;">
        <div style="width: 20%; margin-top: 10px; float:left">
            <fieldset>
                <legend>Sistema</legend>
                <asp:DropDownList Width="100%" ID="ddlSistema" runat="server" AutoPostBack="false">
                </asp:DropDownList>
            </fieldset>
        </div>
        <div style="width: 20%; margin-top: 10px; float:left">
            <fieldset>
                <legend>Aplicação</legend>
                <asp:DropDownList Width="100%" ID="ddlAplicativo" runat="server" AutoPostBack="false">
                </asp:DropDownList>
            </fieldset>
        </div>
    </div>
    <div>
        <div style="width: 40%; text-align:center; margin-top: 10px;">            
            <asp:Button ID="btnImprimir" Text="Imprimir" runat="server" CssClass="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" OnClick="btnImprimir_Click" />
        </div>
    </div>
    <div style="height: 10px;"></div>
    <br />
    <br />
    <asp:Label ID="lblResultado" runat="server" Text="Não há registros que satisfaçam a seleção efetuada." Visible="false" CssClass="blue-font-bold"></asp:Label>
    <rsweb:reportviewer runat="server" width="100%" height="90%" id="reportViewer"></rsweb:reportviewer>
</asp:Content>
