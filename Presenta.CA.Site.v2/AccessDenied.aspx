<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="AccessDenied.aspx.cs" Inherits="Presenta.CA.Site.AccessDenied" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            setPageName('Acesso negado!');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div style="float:left;">
            <img src="Images/access_denied.png" />
        </div>
        <div style="float:left;margin-top:50px;font-size:larger;font-weight:bolder;color:Red;">
            <p >
                Você não possui acesso à essa página e/ou funcionalidade!
            </p>
        </div>
    </div>
</asp:Content>
