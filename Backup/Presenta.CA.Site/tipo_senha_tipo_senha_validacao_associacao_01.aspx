<%@ Page Title="" Language="C#" MasterPageFile="~/controle_acesso.master" AutoEventWireup="true" CodeBehind="tipo_senha_tipo_senha_validacao_associacao_01.aspx.cs" Inherits="Controle_Acesso.tipo_senha_tipo_senha_validacao_associacao" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        //Exibe box se ocorreram erros
        <% if (_Erros != null && _Erros.Length > 0) { %>
        $(document).ready(function(){$(divError).show();});
        <% } else { %>
        $(document).ready(function(){$(divError).hide();});
        <% } %>
    </script>

    <asp:Label ID="lblTitulo" runat="server" Text="..:: Associação de Tipo Senha x Validação de Tipo de Senha" class="tituloform" Width="735px"></asp:Label>
    <br /><br />
    <div id="divError" class="error_box" style="display:none;"><%=_Erros%></div>
    <div id="divWarning" class="warning_box" style="display:none;">Inconsist&ecirc;ncias nos campos, verifique !</div>
    <div id="divCad">

    <asp:HiddenField ID="hfOperador" runat="server" />

        <table border="0" cellpadding="2" cellspacing="2" width="100%">

            <tr>
                <td class="labeltitulo"><asp:Label ID="Label3" runat="server" Text="Tipo Senha:  "></asp:Label>
                <asp:DropDownList ID="ddlTipoSenha" runat="server" 
                        AutoPostBack="True" 
                        onselectedindexchanged="ddlTipoSenha_SelectedIndexChanged" /></td>
            </tr>

            <tr>
                
                <td valign = "top">
                    <asp:GridView ID="gvValidacaoTipoSenhaOut" runat="server" Width="350px" 
                        AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" ForeColor="Black" 
                        GridLines="Vertical" AllowPaging="True" CellSpacing="1" PageSize="15" 
                        onrowcommand="gvValidacaoTipoSenhaOut_RowCommand" >
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <%--<asp:TemplateField Visible="false" >
                                <ItemTemplate >
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("idperfil") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:BoundField DataField="idtiposenhavalidacao" HeaderText="id" />
                            <asp:BoundField DataField="dstiposenhavalidacao" HeaderText="Validação Tipo Senha Não Associada" />
                            <asp:ButtonField ButtonType="Button" HeaderText="Adicionar" CommandName="Select" Text="-->" />
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#236B8E" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#236B8E" Font-Bold="True" ForeColor="White" />
                        <PagerSettings Mode="NumericFirstLast" />
                        <PagerStyle BackColor="#236B8E" ForeColor="White" HorizontalAlign="Right" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <EmptyDataTemplate>
                            <p>Não existem dados.</p>
                        </EmptyDataTemplate>
                    </asp:GridView>                
                </td>

                <td valign = "top">
                    <asp:GridView ID="gvValidacaoTipoSenhaIn" runat="server" Width="350px" 
                        AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                        BorderStyle="None" BorderWidth="1px" CellPadding="3" ForeColor="Black" 
                        GridLines="Vertical" AllowPaging="True" CellSpacing="1" PageSize="15" 
                        onrowcommand="gvValidacaoTipoSenhaIn_RowCommand" >
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:ButtonField ButtonType="Button" HeaderText="Remover" CommandName="Select" Text="<--" />
                            <asp:BoundField DataField="idtiposenhavalidacao" HeaderText="id" />
                            <asp:HyperLinkField DataTextField="dstiposenhavalidacao" HeaderText="Validação Tipo Senha Associada"  DataNavigateUrlFields="idtiposenhavalidacao, idtiposenha" DataNavigateUrlFormatString="tipo_senha_tipo_senha_validacao_cadastro.aspx?validacaotiposenha={0}&tiposenha={1}" />
                        </Columns>
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#236B8E" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#236B8E" Font-Bold="True" ForeColor="White" />
                        <PagerSettings Mode="NumericFirstLast" />
                        <PagerStyle BackColor="#236B8E" ForeColor="White" HorizontalAlign="Right" />
                        <RowStyle BackColor="#EFF3FB" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />                        
                    </asp:GridView>
                </td>

            </tr>
            
        </table>
    </div>
</asp:Content>
