<%@ Page Title="" Language="C#" MasterPageFile="~/controle_acesso.master" AutoEventWireup="true" CodeBehind="operador_cadastro.aspx.cs" Inherits="Controle_Acesso.operador_cadastro" %>
<%@ Register src="controls/ucToolbar.ascx" tagname="ucToolbar" tagprefix="uc1" %>
<%@ Register src="controls/ucData.ascx" tagname="ucData" tagprefix="uc2" %>
<%@ Register src="controls/ucDropDownList.ascx" tagname="ucSituacao" tagprefix="uc3" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        //Exibe box se ocorreram erros
        <% if (_Erros != null && _Erros.Length > 0) { %>
        $(document).ready(function(){$(divError).show();});
        <% } else { %>
        $(document).ready(function(){$(divError).hide();});
        <% } %>
    </script>

    <asp:Label ID="lblTitulo" runat="server" Text="..:: Cadastro de Operador" class="tituloform" Width="735px"></asp:Label>
    <br /><br />
    <div id="divError" class="error_box" style="display:none;"><%=_Erros%></div>
    <div id="divWarning" class="warning_box" style="display:none;">Inconsist&ecirc;ncias nos campos, verifique !</div>
    <div id="divCad">

    <asp:HiddenField ID="hfCodigo" runat="server" />
    <asp:HiddenField ID="hfOperador" runat="server" />

        <table border="0" cellpadding="2" cellspacing="2" width="100%">

            <tr>
                <td class="labeltitulo"><asp:Label ID="lblNome" runat="server" Text="Nome Operador:"></asp:Label></td>
                <td colspan="3"><asp:TextBox ID="txtNmOperador" runat="server" MaxLength="45" Width="100%"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="labeltitulo"><asp:Label ID="Label2" runat="server" Text="Operador:"></asp:Label></td>
                <td><asp:TextBox ID="txtCodOperador" runat="server" MaxLength="45" Width="210px"></asp:TextBox></td>
            </tr>

            <tr>
                <td class="labeltitulo"><asp:Label ID="Label3" runat="server" Text="Senha:"></asp:Label></td>
                <td><asp:TextBox ID="txtCrSenha" runat="server" Width="150px" TextMode="Password" ></asp:TextBox></td>
                <td class="labeltitulo"><asp:Label ID="Label9" runat="server" Text="Confirme a Senha:"></asp:Label></td>
                <td><asp:TextBox ID="txtCrConfirmaSenha" runat="server" Width="150px" TextMode="Password" ></asp:TextBox></td>
            </tr>

            <tr>
                <td class="labeltitulo"><asp:Label ID="Label8" runat="server" Text="Tipo Senha:"></asp:Label></td>
                <td><uc3:ucSituacao ID="ucTipoSenha" runat="server" /></td>
                <td class="labeltitulo"><asp:Label ID="lblSituacao" runat="server" Text="Situação:"></asp:Label></td>
                <td><uc3:ucSituacao ID="ucSituacaOperador" runat="server" /></td>
            </tr>

            <tr>
                <td class="labeltitulo"><asp:Label ID="Label4" runat="server" Text="Último Logon:"></asp:Label></td>
                <td><asp:TextBox ID="txtDhUltimoLogin" runat="server" MaxLength="45" Width="210px"></asp:TextBox></td>
                <td class="labeltitulo"><asp:Label ID="Label5" runat="server" Text="Data Senha:"></asp:Label></td>
                <td><asp:TextBox ID="txtDtSenha" runat="server" Width="150px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="labeltitulo"><asp:Label ID="Label6" runat="server" Text="Logon Incorreto:"></asp:Label></td>
                <td><asp:TextBox ID="txtQtLoginIncorreto" runat="server" MaxLength="45" Width="210px"></asp:TextBox></td>
                <td class="labeltitulo"><asp:Label ID="Label1" runat="server" Text="Data Situação:"></asp:Label></td>
                <td><asp:TextBox ID="txtDhSituacao" runat="server" Width="150px"></asp:TextBox></td>
            </tr>

            <tr>
                <td class="labeltitulo"><asp:Label ID="lblEmail" runat="server" Text="Email:"></asp:Label></td>
                <td><asp:TextBox ID="txtDsEmail" runat="server" MaxLength="45" Width="210px"></asp:TextBox></td>
                <td class="labeltitulo"><asp:Label ID="lblDtCadastro" runat="server" Text="Data Cadastro:"></asp:Label></td>
                <td><asp:TextBox ID="txtDtCadastro" runat="server" Width="150px"></asp:TextBox></td>
            </tr>

            <tr>
                <td class="labeltitulo"><asp:Label ID="Label7" runat="server" Text="Operador:"></asp:Label></td>
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
