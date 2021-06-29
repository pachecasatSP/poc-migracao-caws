<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="troca_senha.aspx.cs" Inherits="Controle_Acesso.troca_senha" %>

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
            <div class="logo"><a target="_blank" href="http://www.presenta.com.br"><img src='<%= ResolveClientUrl("~/images/logo_presenta.jpg") %>' width="210" height="40" alt="Presenta Sistemas" title="" border="0" /></a></div>
            <br />

            <asp:HiddenField ID="hfOperador" runat="server" />
            
            <asp:ChangePassword ID="changePasswordCA" runat="server" BackColor="#F7F6F3" 
                BorderColor="#E6E2D8" BorderPadding="10" BorderStyle="Solid" BorderWidth="1px" 
                Font-Names="Verdana" Font-Size="0.9em" ForeColor="#333333" 
                Height="150px" Width="450px" 
                
                CancelButtonText="Cancelar" 
                ChangePasswordButtonText="Trocar Senha" 
                ChangePasswordTitleText="Presenta Sistemas - Controle de Acesso - Troca de Senha" 
                ConfirmNewPasswordLabelText="Confirmar:" 
                ConfirmPasswordRequiredErrorMessage="Confirmação obrigatória" 
                NewPasswordLabelText="Nova Senha:" 
                PasswordLabelText="Senha Atual:" 
                PasswordRequiredErrorMessage="Senha obrigatória" 
                SuccessText="Senha alterada com sucesso." 
                UserNameLabelText="Usuário:" 
                UserNameRequiredErrorMessage="Usuário é obrigatório." 
                DisplayUserName="True"

                ChangePasswordButtonStyle-BackColor="#FFFBFF" 
                ChangePasswordButtonStyle-BorderColor="#CCCCCC" 
                ChangePasswordButtonStyle-BorderStyle="Solid" 
                ChangePasswordButtonStyle-BorderWidth="1px" 
                ChangePasswordButtonStyle-Font-Names="Verdana" 
                ChangePasswordButtonStyle-Font-Size="0.9em" ChangePasswordButtonStyle-ForeColor="#284775"
                CancelButtonStyle-BackColor="#FFFBFF" 
                CancelButtonStyle-BorderColor="#CCCCCC" CancelButtonStyle-BorderStyle="Solid" 
                CancelButtonStyle-BorderWidth="1px" CancelButtonStyle-Font-Names="Verdana" 
                CancelButtonStyle-Font-Size="0.9em" CancelButtonStyle-ForeColor="#284775"
                TitleTextStyle-BackColor="#5D7B9D" TitleTextStyle-Font-Bold="True" 
                TitleTextStyle-Font-Size="0.9em" TitleTextStyle-ForeColor="White" 
                TextBoxStyle-Font-Size="0.9em" 
                onchangedpassword="changePasswordCA_ChangedPassword" 

                NewPasswordRegularExpression="\d{2,5}" onchangingpassword="changePasswordCA_ChangingPassword"
                
                

            >

            </asp:ChangePassword>

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
