<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucData.ascx.cs" Inherits="Controle_Acesso.ucData" %>
    <script type="text/javascript">
        //Definir máscaras e valores
        jQuery(function ($) {
            var _DtControl = $('#<%=txtData.ClientID%>');
            $(_DtControl).mask('99/99/9999');
        });
    </script>

<asp:TextBox ID="txtData" runat="server" Width="90px" MaxLength="10"></asp:TextBox>
<asp:ImageButton ID="btnCalendario" runat="server" ImageUrl="../images/botao_calendario.gif" Height="16px" Width="16px" ToolTip="Abrir calendário" onclick="btnCalendario_Click" />&nbsp;
<asp:Calendar ID="calendar" Visible="False" runat="server" 
    onselectionchanged="calendar_SelectionChanged" BackColor="White" 
    BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
    Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" 
    Width="200px">
    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
    <NextPrevStyle VerticalAlign="Bottom" />
    <OtherMonthDayStyle ForeColor="#808080" />
    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
    <SelectorStyle BackColor="#CCCCCC" />
    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
    <WeekendDayStyle BackColor="#FFFFCC" />
</asp:Calendar>
