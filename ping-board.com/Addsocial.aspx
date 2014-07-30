<%@ Page Title="" Language="C#" MasterPageFile="~/PingBoard.master" AutoEventWireup="true" CodeFile="Addsocial.aspx.cs" Inherits="Addsocial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
.addsocial{ width:98%; height:300px;margin-left:40px;}
.addsocial-hed{ width:98%; height:25px; background:#07405c; color:#fff; font-size:15px; padding:5px; margin-top:10px; }
.addsocial_bot{width:98%; height:250px; padding:10px 0px; color:#333; font-size:15px; font-weight:bold; padding:10px 5px 0px 5px; background:#fff url("images/add-fb.png") no-repeat left bottom;}
.addsocial_bot1{width:98%; height:250px; padding:10px 0px; color:#333; font-size:15px; font-weight:bold; padding:10px 5px 0px 5px; background:#fff url("images/add-tw.png") no-repeat left bottom;}
.addsocial_bot_left{ width:300px; float:left; padding:30px 10px 10px 10px; font-weight:bold;}
.addsocial_bot_right{ width:300px; float:left;padding:20px 10px;}
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="addsocial">
        <div class="addsocial-hed">Add facebook account</div>
        <div class="addsocial_bot">
            <div style="text-align:center"><asp:Label ID="Res" runat="server"></asp:Label></div>
            <div class="addsocial_bot_left">
                Add your facebook account in this board.</div>
            <div class="addsocial_bot_right">
                <asp:Button ID="Button1" OnClick="Button1_Click" runat="server" CssClass="bluebtn" Text="Add account" />
            </div>
            <div class="addsocial_bot_right">
                <asp:DropDownList ID="fbPages" Width="120" Height="25" runat="server"></asp:DropDownList>
                &nbsp; &nbsp;
                 <asp:Button ID="Button3" OnClick="Button3_Click" runat="server" CssClass="bluebtn" Text="Submit Page" />
            </div>
           
        </div>
    </div>
    <div class="addsocial">
        <div class="addsocial-hed">Add twitter account</div>
        <div class="addsocial_bot1">
            <div class="addsocial_bot_left">
                Add your twitter account in this board.</div>
            <div class="addsocial_bot_right">
                <asp:Button ID="Button2" OnClick="Button2_Click" runat="server" CssClass="bluebtn" Text="Add account" />
            </div>
            <div class="addsocial_bot_left">
                <asp:Label ID="twit" runat="server"></asp:Label></div>
        </div>
    </div>
</asp:Content>

