<%@ Page Title="" Language="C#" MasterPageFile="~/controle_acesso.master" AutoEventWireup="true" CodeBehind="funcionalidade_perfil_filtro.aspx.cs" Inherits="Controle_Acesso.funcionalidade_perfil_filtro" %>
<%@ Register src="controls/ucToolbar.ascx" tagname="ucToolbar" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        //Exibe box se ocorreram erros
        <% if (_Erros != null && _Erros.Length > 0) { %>
        $(document).ready(function(){$(divError).show();});
        <% } else { %>
        $(document).ready(function(){$(divError).hide();});
        <% } %>
    </script>

    <asp:Label ID="lblTitulo" runat="server" Text="..:: Associação de Funcionalidade x Perfil - Filtro" class="tituloform" Width="735px"></asp:Label>
    <br /><br />
    <div id="divError" class="error_box" style="display:none;"><%=_Erros%></div>
    <div id="divWarning" class="warning_box" style="display:none;"><%=_Avisos%></div>
    <div id="divBox" class="valid_box" style="display:none;">Dados gravados com sucesso !</div>
    <div id="divSearch">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td class="labeltitulo" width="10%" align="left">
                    <asp:Label ID="lblFiltroDescricao" runat="server" Text="Descrição:"></asp:Label>
                </td>
                <td width="65%">
                    <asp:TextBox ID="txtFiltroDescricao" runat="server" MaxLength="50" Width="280px"></asp:TextBox>
                </td>
                <td width="25%" align="left">
                    <uc1:uctoolbar ID="ucCAToolbar" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <br />
    <asp:GridView ID="gvPerfilOperador" runat="server" Width="745px" 
        AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
        BorderStyle="None" BorderWidth="1px" CellPadding="3" ForeColor="Black" 
        GridLines="Vertical" AllowPaging="True" CellSpacing="1" PageSize="15" >
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:HyperLinkField DataTextField="dsfuncionalidade" HeaderText="Funcionalidade"  DataNavigateUrlFields="idfuncionalidade, idperfil" DataNavigateUrlFormatString="funcionalidade_perfil_cadastro.aspx?funcionalidade={0}&perfil={1}" />
            <asp:HyperLinkField DataTextField="dsperfil" HeaderText="Perfil" DataNavigateUrlFields="idfuncionalidade, idperfil" DataNavigateUrlFormatString="funcionalidade_perfil_cadastro.aspx?funcionalidade={0}&perfil={1}" />
            <asp:HyperLinkField DataTextField="dssituacaofuncionalidadeperfil" HeaderText="Situação" DataNavigateUrlFields="idfuncionalidade, idperfil" DataNavigateUrlFormatString="funcionalidade_perfil_cadastro.aspx?funcionalidade={0}&perfil={1}" />
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#236B8E" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#236B8E" Font-Bold="True" ForeColor="White" />
        <PagerSettings Mode="NumericFirstLast" />
        <PagerStyle BackColor="#236B8E" ForeColor="White" HorizontalAlign="Right" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    </asp:GridView>
</asp:Content>
