<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Main.Master" AutoEventWireup="true" CodeBehind="IdleTimer.aspx.cs" Inherits="Presenta.CA.Site.IdleTimer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script type="text/javascript" src="./Scripts/jquery-1.4.1.min.js"></script>--%>
    <script type="text/javascript" src="./Scripts/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="./Scripts/jquery-ui-1.8rc1.custom.min.js"></script>
    <link rel="stylesheet" type="text/css" href="./Styles/cupertino/jquery-ui-1.8.23.custom.css">
    <script type="text/javascript" src="./Scripts/jquery.idletimer.js"></script>
    <%--<style type="text/css">
        body {
            font-family: Arial,Geneva,Verdana,sans-serif;
            font-size: 0.8em;
        }
    </style>--%>
    <script type="text/javascript">
        var idleTime = 2000; // number of miliseconds until the user is considered idle
        var initialSessionTimeoutMessage = 'A sessão expirará em <span id="sessionTimeoutCountdown"></span> segundos.<br /><br />Clique em <b>OK</b> para manter a sessão.';
        var sessionTimeoutCountdownId = 'sessionTimeoutCountdown';
        var redirectAfter = 10; // number of seconds to wait before redirecting the user
        var redirectTo = 'Logout.aspx'; // URL to relocate the user to once they have timed out
        var keepAliveURL = 'KeepAlive.aspx'; // URL to call to keep the session alive
        var expiredMessage = 'A sessão expirou.  Você foi deslogado por questões de segurança.'; // message to show user when the countdown reaches 0
        var running = false; // var to check if the countdown is running
        var timer; // reference to the setInterval timer so it can be stopped
        $(document).ready(function() {
            // create the warning window and set autoOpen to false
            var sessionTimeoutWarningDialog = $("#sessionTimeoutWarning");
            $(sessionTimeoutWarningDialog).html(initialSessionTimeoutMessage);
            $(sessionTimeoutWarningDialog).dialog({
                title: 'Aviso de Expiração de Sessão',
                autoOpen: false,    // set this to false so we can manually open it
                closeOnEscape: false,
                draggable: false,
                width: 460,
                minHeight: 50,
                modal: true,
                beforeclose: function() { // bind to beforeclose so if the user clicks on the "X" or escape to close the dialog, it will work too
                    // stop the timer
                    clearInterval(timer);
 
                    // stop countdown
                    running = false;
 
                    // ajax call to keep the server-side session alive
                    $.ajax({
                        url: keepAliveURL,
                        async: false
                    });
                },
                buttons: {
                    OK: function() {
                        // close dialog
                        $(this).dialog('close');
                    }
                },
                resizable: false,
                open: function() {
                    // scrollbar fix for IE
                    $('body').css('overflow','hidden');
                },
                close: function() {
                    // reset overflow
                    $('body').css('overflow','auto');
                }
            }); // end of dialog
 
 
            // start the idle timer
            $.idleTimer(idleTime);
 
            // bind to idleTimer's idle.idleTimer event
            $(document).bind("idle.idleTimer", function(){
                // if the user is idle and a countdown isn't already running
                if($.data(document,'idleTimer') === 'idle' && !running){
                    var counter = redirectAfter;
                    running = true;
 
                    // intialisze timer
                    $('#'+sessionTimeoutCountdownId).html(redirectAfter);
                    // open dialog
                    $(sessionTimeoutWarningDialog).dialog('open');
 
                    // create a timer that runs every second
                    timer = setInterval(function(){
                        counter -= 1;
 
                        // if the counter is 0, redirect the user
                        if(counter === 0) {
                            $(sessionTimeoutWarningDialog).html(expiredMessage);
                            $(sessionTimeoutWarningDialog).dialog('disable');
                            window.location = redirectTo;
                        } else {
                            $('#'+sessionTimeoutCountdownId).html(counter);
                        };
                    }, 1000);
                };
            });
 
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>CA - IdleTimer</h1>
    <p>
        Página Teste!
        <br /><br />
    </p>
    <div id="sessionTimeoutWarning" style="display: none"></div>
</asp:Content>
