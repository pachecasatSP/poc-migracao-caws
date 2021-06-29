<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="Sistemas.aspx.cs" Inherits="Presenta.CA.Site.Pages.Relatorio.Sistemas" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Src="~/UserControls/SistemaUC.ascx" TagName="SistemajqGrid" TagPrefix="stmGrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            configPage();
            configBtnImprimir();
            exibirNotificacoes();
            setTimeout('configGridButtons();', 100);
        });
        function configPage() {
            setGrandMenuId(3);
            setMenuHover(3);
            setPageName('Relatório - Sistemas / Aplicativos / Funcionalidades');
        }
        function configGridButtons() {
            $("#SistemasGrid").jqGridHideAdd().jqGridHideEdit().jqGridHideDel();
        }
        function configBtnImprimir() {
            GetElemByClientID('btn-imprimir').button({
                icons: {
                    primary: "ui-icon-print",
                    secondary: "ui-icon-check"
                },
                text: true
            });
            //GetElemByClientID('btnImprimir').click(function () {
            //    var grid = $("#SistemasGrid");
            //    if (grid.jqGrid('getGridParam', 'selrow') !== null) {
            //        debugger;
            //        var selRowId = grid.jqGrid('getGridParam', 'selrow');
            //        var idSistema = grid.jqGrid('getCell', selRowId, 'IdSistema');
            //        GetElemByClientID('hdfIdSistema').val(idSistema);
            //        return true;
            //    }
            //});
        }
        function configPrintPostBack() {
            GetElemByClientID('hdfIsPostBack').val('1');
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
        <asp:HiddenField ID="hdfIdSistema" runat="server" EnableViewState="true" />
        <asp:HiddenField ID="hdfIsPostBack" runat="server" EnableViewState="true" Value="0" />
    </div>
    <div style="padding: 5px;">
        <stmGrid:SistemajqGrid ID="sistemaGrid" runat="server" />
    </div>
    <div class="clear"></div>
    <div style="padding: 5px;margin-top:10px;display:none;">
        <div style="padding:5px;text-align:center;">
            <button id="btn-imprimir">Imprimir</button>
        </div>
    </div>
    <div style="margin-top: 10px;">
            <div style="text-align: center; margin-top: 20px;">
                <asp:Button ID="btnImprimir" Text="Imprimir" runat="server" CssClass="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" OnClientClick="configPrintPostBack();" OnClick="btnImprimir_Click" />
            </div>
        </div>
    <div style="height: 10px;"></div>
    <br />
    <br />
    <asp:Label ID="lblResultado" runat="server" Text="Não há registros que satisfaçam a seleção efetuada." Visible="false" CssClass="blue-font-bold"></asp:Label>
    <rsweb:ReportViewer runat="server" Width="100%" Height="90%" ID="reportViewer"></rsweb:ReportViewer>
</asp:Content>
