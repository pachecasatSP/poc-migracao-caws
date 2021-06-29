<%@ Page Title="" Language="C#" MasterPageFile="~/controle_acesso.master" AutoEventWireup="true" CodeBehind="tipo_senha_tipo_senha_validacao_cadastro.aspx.cs" Inherits="Controle_Acesso.tipo_senha_tipo_senha_validacao_cadastro" %>
<%@ Register src="controls/ucToolbar.ascx" tagname="ucToolbar" tagprefix="uc1" %>
<%@ Register src="controls/ucDropDownList.ascx" tagname="ucTipos" tagprefix="uc2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        //Exibe box se ocorreram erros
        <% if (_Erros != null && _Erros.Length > 0) { %>
        $(document).ready(function(){$(divError).show();});
        <% } else { %>
        $(document).ready(function(){$(divError).hide();});
        <% } %>
    </script>

    <asp:Label ID="lblTitulo" runat="server" Text="..:: Cadastro de Tipo Senha x Validação de Tipo de Senha" class="tituloform" Width="735px"></asp:Label>
    <br /><br />
    <div id="divError" class="error_box" style="display:none;"><%=_Erros%></div>
    <div id="divWarning" class="warning_box" style="display:none;">Inconsist&ecirc;ncias nos campos, verifique !</div>
    <div id="divCad">

    <asp:HiddenField ID="hfOperador" runat="server" />

        <table border="0" cellpadding="2" cellspacing="2" width="100%">
            <tr>
                <td class="labeltitulo"><asp:Label ID="Label5" runat="server" Text="Tipo de Senha:"></asp:Label></td>
                <td><uc2:ucTipos ID="ucTipoSenha" runat="server" Width="100%" ></uc2:ucTipos></td>
            </tr>

            <tr>
                <td class="labeltitulo"><asp:Label ID="Label9" runat="server" Text="Tipo:"></asp:Label></td>
                <td><uc2:ucTipos ID="ucTipoSenhaValidacao" runat="server" Width="150px"></uc2:ucTipos></td>
                <td class="labeltitulo"><asp:Label ID="Label3" runat="server" Text="Sequencial:"></asp:Label></td>
                <td><asp:TextBox ID="txtNuOrdem" runat="server" Width="150px" Enabled="false" ></asp:TextBox></td>
            </tr>

            <tr>
                <td class="labeltitulo"><asp:Label ID="Label4" runat="server" Text="Mínimo:"></asp:Label></td>
                <td><asp:TextBox ID="txtQtMinCaracteres" runat="server" Width="150px"></asp:TextBox></td>
                <td class="labeltitulo"><asp:Label ID="Label1" runat="server" Text="Máximo:"></asp:Label></td>
                <td><asp:TextBox ID="txtQtMaxCaracteres" runat="server" Width="150px"></asp:TextBox></td>
            </tr>

            <tr>
                <td class="labeltitulo"><asp:Label ID="Label2" runat="server" Text="Operador:"></asp:Label></td>
                <td colspan="3"><asp:TextBox ID="txtOperador" runat="server" Width="100%" Enabled="False"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="labeltitulo"><asp:Label ID="lblOperador" runat="server" Text="Login:"></asp:Label></td>
                <td><asp:TextBox ID="txtLogin" runat="server" Width="150px" Enabled="False"></asp:TextBox></td>
                <td class="labeltitulo"><asp:Label ID="lblDataAtualizacao" runat="server" Text="Data Atualização:"></asp:Label></td>
                <td><asp:TextBox ID="txtDhAtualizacao" runat="server" Width="150px" Enabled="False"></asp:TextBox></td>
            </tr>            

        </table>
        <div id="divToolbar" align="right">
            <uc1:uctoolbar ID="ucCAToolbar" runat="server" />
        </div>
    </div>
</asp:Content>
