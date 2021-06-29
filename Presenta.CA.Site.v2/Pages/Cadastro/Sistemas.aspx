<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Sistemas.aspx.cs" Inherits="Presenta.CA.Site.Pages.Cadastro.Sistemas" %>

<%@ Register Src="~/UserControls/SistemaUC.ascx" TagName="SistemajqGrid" TagPrefix="stmGrid" %>
<%@ Register Src="~/UserControls/AplicativoUC.ascx" TagName="AplicativojqGrid" TagPrefix="appGrid" %>
<%@ Register Src="~/UserControls/FuncionalidadeUC.ascx" TagName="FuncionalidadejqGrid" TagPrefix="fncGrid" %>
<%@ Register Src="~/UserControls/PerfilUC.ascx" TagName="PerfiljqGrid" TagPrefix="pflGrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            configPage();
            exibirNotificacoes();
            setTimeout('configGridButtons();', 100);
        });
        function configPage() {
            setGrandMenuId(1);
            setMenuHover(0);
            setPageName('Cadastro - Sistemas / Aplicativos / Funcionalidades');
        }
        function configGridButtons() {
            $("#SistemasGrid").jqGridHideAdd().jqGridHideEdit().jqGridHideDel();
            $("#AplicativosGrid").jqGridHideAdd().jqGridHideEdit().jqGridHideDel();
            $("#FuncionalidadesGrid").jqGridHideAdd().jqGridHideEdit().jqGridHideDel();
            $("#PerfisGrid").jqGridHideAdd().jqGridHideEdit().jqGridHideDel();
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
    </div>
    <div>
        <div style="padding: 5px;">
            <stmGrid:SistemajqGrid ID="sistemaGrid" runat="server" />
        </div>
        <div style="padding: 5px;">
            <appGrid:AplicativojqGrid ID="aplicativojqGrid" runat="server" />
        </div>
        <div style="padding: 5px;">
            <fncGrid:FuncionalidadejqGrid ID="funcionalidadejqGrid" runat="server" />
        </div>
        <div style="padding: 5px;">
            <pflGrid:PerfiljqGrid ID="perfilGrid" runat="server" />
        </div>
    </div>
</asp:Content>
