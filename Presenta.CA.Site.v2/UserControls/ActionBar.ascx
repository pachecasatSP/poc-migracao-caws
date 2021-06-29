<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ActionBar.ascx.cs" Inherits="Presenta.CA.Site.UserControls.ActionBar" %>
<div class="conteudo_button">
    <div class="toolbarLeft"></div>
    <div class="actionButton">
        <asp:LinkButton runat="server" ID="lkbInserir" CssClass="button" 
            onclick="lkbInserir_Click" >
            <asp:Image ID="imgInserir" imageurl="~/Images/21.png" runat="server" />
            <asp:Label ID="lblInserir" text="Inserir" runat="server" />
        </asp:LinkButton><asp:LinkButton runat="server" ID="lkbAlterar" 
            CssClass="button" onclick="lkbAlterar_Click" >
            <asp:Image ID="imgAlterar" imageurl="~/Images/24.png" runat="server" />
            <asp:Label ID="lblAlterar" text="Alterar" runat="server" />
        </asp:LinkButton><asp:LinkButton runat="server" ID="lkbSalvar" 
            CssClass="button" onclick="lkbSalvar_Click" >
            <asp:Image ID="imgSalvar" imageurl="~/Images/45.png" runat="server" />
            <asp:Label ID="lblSalvar" text="Salvar" runat="server" />
        </asp:LinkButton><asp:LinkButton runat="server" ID="lkbCancelar" 
            CssClass="button" onclick="lkbCancelar_Click" >
            <asp:Image ID="imgCancelar" imageurl="~/Images/cancel.png" runat="server" />
            <asp:Label ID="lblCancelar" text="Cancelar" runat="server" />
        </asp:LinkButton><asp:LinkButton runat="server" ID="lkbExcluir" 
            CssClass="button" onclick="lkbExcluir_Click" >
            <asp:Image ID="imgExcluir" imageurl="~/Images/bin_closed.png" runat="server" />
            <asp:Label ID="lblExcluir" text="Excluir" runat="server" />
        </asp:LinkButton><asp:LinkButton runat="server" ID="lkbImprimir" 
            CssClass="button" onclick="lkbImprimir_Click" >
            <asp:Image ID="imgImprimir" imageurl="~/Images/printer.png" runat="server" />
            <asp:Label ID="lblImprimir" text="Imprimir" runat="server" />
        </asp:LinkButton><asp:LinkButton runat="server" ID="lkbLocalizar" 
            CssClass="button" onclick="lkbLocalizar_Click" >
            <asp:Image ID="imgLocalizar" imageurl="~/Images/find.png" runat="server" />
            <asp:Label ID="lblLocalizar" text="Localizar" runat="server" />
        </asp:LinkButton></div><div class="toolbarRight"></div>
</div>