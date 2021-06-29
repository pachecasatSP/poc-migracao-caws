<%@ Page Title="" Language="C#" MasterPageFile="~/controle_acesso.master" AutoEventWireup="true" CodeBehind="aplicativo_cadastro.aspx.cs" Inherits="Controle_Acesso.aplicativo_cadastro" %>
<%@ Register src="controls/ucToolbar.ascx" tagname="ucToolbar" tagprefix="uc1" %>
<%@ Register src="controls/ucDropDownList.ascx" tagname="ucSistema" tagprefix="uc2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        //Exibe box se ocorreram erros
        <% if (_Erros != null && _Erros.Length > 0) { %>
        $(document).ready(function(){$(divError).show();});
        <% } else { %>
        $(document).ready(function(){$(divError).hide();});
        <% } %>
    </script>

    <asp:Label ID="lblTitulo" runat="server" Text="..:: Cadastro de Aplicativos" class="tituloform" Width="735px"></asp:Label>
    <br /><br />
    <div id="divError" class="error_box" style="display:none;"><%=_Erros%></div>
    <div id="divWarning" class="warning_box" style="display:none;">Inconsist&ecirc;ncias nos campos, verifique !</div>
    <div id="divCad">

    <asp:HiddenField ID="hfCodigo" runat="server" />
    <asp:HiddenField ID="hfOperador" runat="server" />

        <table border="0" cellpadding="2" cellspacing="2" width="100%">
            <tr>
                <td class="labeltitulo"><asp:Label ID="lblSistema" runat="server" Text="Sistema:"></asp:Label></td>
                <td>
                    <uc2:ucsistema ID="ucIDSistema" runat="server" />
                </td>
            </tr>

            <tr>
                <td class="labeltitulo"><asp:Label ID="Label2" runat="server" Text="Aplicativo:"></asp:Label></td>
                <td colspan="3"><asp:TextBox ID="txtDsAplicativo" runat="server" MaxLength="45" 
                        Width="100%" ></asp:TextBox></td>
            </tr>
            
            <tr>
                <td class="labeltitulo"><asp:Label ID="Label3" runat="server" Text="Operador:"></asp:Label></td>
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
