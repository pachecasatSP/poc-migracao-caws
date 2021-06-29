<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="PerfilFuncionalidade.aspx.cs" Inherits="Presenta.CA.Site.Pages.Cadastro.PerfilFuncionalidade" %>
<%@ Register Src="~/UserControls/PerfilUC.ascx" TagName="PerfiljqGrid" TagPrefix="pflGrid" %>
<%@ Register Src="~/UserControls/SistemaUC.ascx" TagName="SistemajqGrid" TagPrefix="stmGrid" %>
<%@ Register Src="~/UserControls/AplicativoUC.ascx" TagName="AplicativojqGrid" TagPrefix="appGrid" %>
<%@ Register Src="~/UserControls/FuncionalidadeUC.ascx" TagName="FuncionalidadejqGrid" TagPrefix="fncGrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            configPage();
            exibirNotificacoes();
            //setTimeout('configGridButtons();', 100);
        });
        function configPage() {
            setGrandMenuId(1);
            setMenuHover(1);
            setPageName('Cadastro - Perfil / Funcionalidades');
        }
        function configGridButtons() {
            $("#PerfisGrid").jqGridHideAdd().jqGridHideEdit().jqGridHideDel();
            $("#FuncionalidadesGrid").jqGridHideAdd().jqGridHideEdit().jqGridHideDel();
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
        <asp:HiddenField ID="hdfPerfilFuncionalidade" runat="server" EnableViewState="true" Value="1" />
    </div>
    <div>
        <div style="padding: 5px;">
            <pflGrid:PerfiljqGrid ID="perfilGrid" runat="server" />
        </div>
        <div style="padding: 5px;">
            <stmGrid:SistemajqGrid ID="sistemaGrid" runat="server" />
        </div>
        <div style="padding: 5px;">
            <appGrid:AplicativojqGrid ID="aplicativojqGrid" runat="server" />
        </div>
        <div style="padding: 5px;">
            <fncGrid:FuncionalidadejqGrid ID="funcionalidadejqGrid" runat="server" />
        </div>
    </div>
</asp:Content>
