<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Controle_Acesso.login" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Presenta Sistemas - Controle de Acesso - Login</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div align="center">
            <br /><br /><br /><br /><br />
            <div class="logo"><a target="_blank" href="http://www.presenta.com.br"> <img src='<%= ResolveClientUrl("~/images/logo_presenta.jpg") %>' width="210" height="40" alt="Presenta Sistemas" title="" border="0" /></a></div>
            <br />

            <%--matsu - remover UserName --%>
            <asp:Login ID="loginCA" runat="server" BackColor="#F7F6F3" 
                BorderColor="#E6E2D8" BorderPadding="10" BorderStyle="Solid" BorderWidth="1px" 
                Font-Names="Verdana" Font-Size="0.9em" ForeColor="#333333" 
                Height="150px" Width="350px" 
                
                UserName="admin"

                DestinationPageUrl="default.aspx" 
                DisplayRememberMe="False" 
                FailureText="Login não efetuado. Tente novamente." 
                PasswordLabelText="Senha : " 
                PasswordRequiredErrorMessage="Senha é obrigatório" 
                TitleText="Presenta Sistemas - Controle de Acesso - Login" 
                UserNameLabelText="Usuário : " 
                UserNameRequiredErrorMessage="Usuário é obrigatório"
                onauthenticate="loginCA_Authenticate">
                <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="0.9em" ForeColor="#284775" />
                <TextBoxStyle Font-Size="0.9em" />
                <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="0.9em" ForeColor="White" />
            </asp:Login>

            <asp:ValidationSummary ID="vsCA" runat="server"
                ValidationGroup="loginCA" >
            </asp:ValidationSummary>


        </div>
    </div>

    <%--matsu - remover--%>
    <%--<script type="text/javascript">
        document.forms[0].loginCA$Password.value = "123";
    </script>--%>

    </form>
</body>
</html>
