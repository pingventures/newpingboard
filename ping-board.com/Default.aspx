<%@ Page Title="" Language="C#" MasterPageFile="~/web.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
    header{ position:fixed;  width:100%; height:80px; top:0px; background:none;}
    body{background:#0d0a0a; font-family:Arial; padding:0px; margin:0px; background-image:url(images/index-bg.png);}
    .error-box{padding:10px; margin-top:0px; font-size:12px; color:red; background:#fff; border-radius:3px; width:230px; text-align:center; font-weight:bold;}
	footer{ background:none !important;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="hastag">
            <img src="images/index-hastag.png" alt="hastag" />
        </div>
        <div class="index-center-area">
            <div class="index-pingboard">
                <img src="images/index-pingboard.png" alt="Ping Board" />
            </div>
            <div class="input-area">
                <asp:TextBox ID="txtBoard" runat="server" placeholder="Create Pingboard Here..."></asp:TextBox>
                <asp:LinkButton ID="Link1" Height="35" OnClick="Link1_Click" CssClass="btn" runat="server">Create</asp:LinkButton>
            </div>
            <div id="errorBox" runat="server" class="error-box">
                <asp:Label ID="Result" runat="server"></asp:Label>
            </div>
        </div>
</asp:Content>

