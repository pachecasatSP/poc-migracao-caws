<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucToolbar.ascx.cs" Inherits="Controle_Acesso.ucToolbar" %>
<div id="layBotoes">
    <asp:ImageButton ID="btnSave" runat="server" ImageUrl="../images/save.png" Height="16px" Width="16px" ToolTip="Gravar dados" OnClientClick="javascript:Processando(true);" onclick="btnSave_Click" />&nbsp;
    <asp:ImageButton ID="btnSaveConfirm" runat="server" ImageUrl="../images/save.png" Height="16px" Width="16px" ToolTip="Gravar dados" OnClientClick="return Confirmar();" onclick="btnSave_Click" />&nbsp;
    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="../images/delete.png" Height="16px" Width="16px" ToolTip="Excluir registro" OnClientClick="return ConfirmarExclusao();" onclick="btnDelete_Click" />&nbsp;
<% 
    if (_ResetVisible)
    {
%>
    <asp:ImageButton ID="btnReset" runat="server" ImageUrl="../images/edit-clear.png" Height="16px" Width="16px" ToolTip="Limpar campos" OnClientClick="javascript:document.forms[0].reset();" />&nbsp;
    <%--<a href="#"><img border="0" id="btnReset" alt="Limpar campos" title="Limpar campos" src="/images/edit-clear.png" height="16" width="16" onclick="javascript:document.forms[0].reset();" /></a>&nbsp;--%>
<%
    }
    else if (!_ResetEnabled)
    {
%>
    <asp:ImageButton ID="btnResetOff" runat="server" ImageUrl="../images/edit-clear.png" Height="16px" Width="16px" ToolTip="Limpar campos"  />&nbsp;
    <%--<img border="0" id="btnResetOff" alt="Limpar campos" title="Limpar campos" src="/images/edit-clear.png" height="16" width="16" />&nbsp;--%>
<%
    }
%>
    <asp:ImageButton ID="btnBack" runat="server" ImageUrl="../images/voltar.gif" Height="16px" Width="16px" ToolTip="Voltar" OnClientClick="javascript:Processando();" onclick="btnBack_Click" CausesValidation="false" />&nbsp;
<%
    if (_BackHTMLVisible)
    {
%>
    <a href="#"><img border="0" id="btnBackHTML" alt="Voltar" title="Voltar" src="/images/voltar.gif" height="16" width="16" onclick="javascript:Processando();history.back(-1);" /></a>&nbsp;
<%
    }
%>
    <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="../images/search.png" Height="16px" Width="16px" OnClientClick="javascript:Processando(true);" onclick="btnSearch_Click" ToolTip="Pesquisar" />&nbsp;
    <asp:ImageButton ID="btnNew" runat="server" ImageUrl="../images/new.gif" Height="16px" Width="16px" OnClientClick="javascript:Processando(true);" onclick="btnNew_Click" ToolTip="Novo registro" />&nbsp;
</div>
<div id="layProcess" style="display:none;">
    <span class="labeltitulo">&nbsp;Processando ... &nbsp;<asp:ImageButton ID="imgpBar" CssClass="captcha_loading" runat="server" ImageUrl="../images/process.gif" /></span>
</div>
<script type="text/javascript">
    function CancelaProcessando() {
        document.getElementById("layBotoes").style.display = "";
        document.getElementById("layProcess").style.display = "none";
    }

    function Processando() {
        document.getElementById("layProcess").style.display = "";
        document.getElementById("layBotoes").style.display = "none";
    }
    
    function Processando(validar) {
        var blnvalid = true;
        document.getElementById("layProcess").style.display = "";
        document.getElementById("layBotoes").style.display = "none";
        if (typeof (Page_ClientValidate) == 'function' && validar) {
            blnvalid = Page_ClientValidate();
        }
        if (!blnvalid) {
            CancelaProcessando();
        }
    }

    function ConfirmarExclusao() {
        Processando();
        if (!confirm('Deseja excluir o registro ?')) {
            CancelaProcessando();
            return false;
        }
    }

    function Confirmar() {
        Processando();
        if (!confirm('Confirma a operação ?')) {
            CancelaProcessando();
            return false;
        }
    }
</script>