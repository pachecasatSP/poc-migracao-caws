<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Perfil.aspx.cs" Inherits="Presenta.CA.Site.Pages.Cadastro.Perfil" %>
<%@ Register Src="~/UserControls/PerfilUC.ascx" TagName="PerfiljqGrid" TagPrefix="pflGrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            configPage();
            exibirNotificacoes();
            configPagePermissions();
        });
        function configPagePermissions() {
            if (GetElemByClientID("hdfReadOnly").val() == '1') {
                setTimeout('$("#PerfisGrid").jqGridHideAdd().jqGridHideEdit().jqGridHideDel();', 100);
            }
        }
        function configPage() {
            setGrandMenuId(1);
            setMenuHover(3);
            setPageName('Cadastro - Perfil');
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
        <asp:HiddenField ID="hdfReadOnly" runat="server" EnableViewState="true" />
    </div>
    <div>
        <div style="padding: 5px;">
            <pflGrid:PerfiljqGrid ID="perfilGrid" runat="server" />
        </div>
    </div>
</asp:Content>
