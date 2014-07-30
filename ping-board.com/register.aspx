<%@ Page Title="" Language="C#" MasterPageFile="~/web.master" AutoEventWireup="true" CodeFile="register.aspx.cs" Inherits="register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style type="text/css">
    .redbtn
        {
            background: #25A6E1;
            background: -moz-linear-gradient(top,#25A6E1 0%,#188BC0 100%);
            background: -webkit-gradient(linear,left top,left bottom,color-stop(0%,#25A6E1),color-stop(100%,#188BC0));
            background: -webkit-linear-gradient(top,#25A6E1 0%,#188BC0 100%);
            background: -o-linear-gradient(top,#25A6E1 0%,#188BC0 100%);
            background: -ms-linear-gradient(top,#25A6E1 0%,#188BC0 100%);
            background: linear-gradient(top,#25A6E1 0%,#188BC0 100%);
            filter: progid: DXImageTransform.Microsoft.gradient( startColorstr='#25A6E1',endColorstr='#188BC0',GradientType=0);
            padding: 8px 175px;
            color: #fff;
            font-family:'Helvetica Neue' ,sans-serif;
            font-size: 16px;
            font-weight:bold;
            border-radius: 4px;
            -moz-border-radius: 4px;
            -webkit-border-radius: 4px;
            border: 1px solid #1A87B9;
            margin: 5px;
            cursor:pointer;
        }
 .ragistration-area{ width:1000px; height:800px; padding:10px; margin-left:auto; margin-right:auto; margin-top:100px;}
 .ragistration-left{float:left; width:600px;}
 .ragistration-right{float:right; width:300px;}
 .ragistration-box{}
 .ragistration-header{height:30px; color:#fff; font-size:14px; font-weight:bold; width:450px; padding:10px 5px 0px 5px; background:rgba(9,117,171,1);}
 .ragistration-bottom{width:450px; padding:5px; background:rgba(255,255,255,1);}
 .subheading{margin:10px;}
 input[type="text"], input[type="password"], input[type="email"], input[type="number"], textarea, select {
    border:1px solid #A6A6A6;
    background-color:#FFF;
    color:#333;
    min-height:35.5px;
    line-height:1.25;
    font-size:14px;
    padding:8px 0px 8px 8px;
    display:inline-block;
    outline:0px none;
    vertical-align:middle;
    border-radius:4px;
    max-width:100%;
    box-sizing:border-box;
    transition:box-shadow 2s ease-in 0s;
    margin:5px;
}
.ragistration-panel{
    margin:5px;
    font-size:13px;
     }
.fontsize1{
    font-size:11px;
    }
.colorbox {
    width:320px; 
    padding:10px; 
    margin:0px; 
    height:150px; background:rgba(255,255,255,1);  border-radius:4px; text-align:center;}
.colorbox span{ font-size:14px; margin:10px; font-weight:bold; color:#fa6f2a;}
.colorbox ul{ list-style:none; margin:5px; padding:0px; text-align:center;}
.colorbox ul li{ float:left; margin:5px 15px; font-size:12px;}
.colorbox ul li b{ color:#333;}
.ragistration-panel div{ margin-top:7px; float:right;}

</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="ragistration-area">
<div class="subheading">

</div>

<div class="ragistration-left">
<div class="ragistration-box">
  <div class="ragistration-header">Create Your Account
  </div>
  <div class="ragistration-bottom">
      <asp:TextBox ID="TextBox1" placeholder="Enter Your Email" Width="400px" runat="server"></asp:TextBox>
      <asp:TextBox ID="TextBox2" placeholder="Enter Your Password" Width="400px" runat="server"></asp:TextBox>
      <asp:TextBox ID="TextBox3" placeholder="Enter Your Password Again" Width="400px" runat="server"></asp:TextBox>
  </div>
</div>

<%--<div class="ragistration-box">
  <div class="ragistration-header">
  2. Select Your Plan
  </div>
  <div class="ragistration-bottom">

  <span>Billing Cycle</span>
  <div class="ragistration-panel">
     <div class="annual"></div>  
     <div class="monthly"></div>  
  </div>
  </div>
</div>--%>

<div class="ragistration-box">
  <div class="ragistration-header">
Enter Your Billing Information
  </div>
  <div class="ragistration-bottom">
  <div class="ragistration-panel">
      <asp:RadioButton ID="RadioButton1" Font-Bold="true" runat="server" Text="Pay By Cradit Card" />
   </div>

    <div class="ragistration-panel">
      <asp:TextBox ID="TextBox4" Width="300px"  runat="server"></asp:TextBox>
      <asp:TextBox ID="TextBox5" Width="80px" runat="server"></asp:TextBox>
    </div>

    <div class="ragistration-panel">
      
      <asp:DropDownList runat="server">
        <asp:ListItem>Month</asp:ListItem> 
      </asp:DropDownList>

       <asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem>Year</asp:ListItem> 
      </asp:DropDownList>
     <div> <img src="images/payment.png" alt="Payment" /></div>
   </div>


   <div class="ragistration-panel">

    <asp:RadioButton ID="RadioButton2" Font-Bold="true" Text="Pay Using Paypal" runat="server" />
   </div>

     <div class="ragistration-panel">
       <span>Billing Address:</span>
         <asp:DropDownList ID="DropDownList2" runat="server">
         <asp:ListItem>Select Country</asp:ListItem>
         </asp:DropDownList>
   </div>

    <div class="ragistration-panel">
       <span>State/Province:</span>
         <asp:DropDownList ID="DropDownList3" runat="server">
         <asp:ListItem>Select Country</asp:ListItem>
         </asp:DropDownList>
   </div>

   <div class="ragistration-panel">
       <span>Zip/Postal Code:</span>
       <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
   </div>

   <div class="ragistration-panel">
   <span class="fontsize1">
       <asp:CheckBox ID="CheckBox1" Text="I have read and agree to the Terms of Use" runat="server" /></span>
   </div>
<p>&nbsp;</p>
   <div class="ragistration-panel">
       <asp:Button ID="Button1" runat="server" CssClass="redbtn" Text="Start Now" />

   </div>

<p></p>


  </div>
</div>
</div>


<div class="ragistration-right">

<div class="colorbox">
<span>What's included with your Pro plan?</span>
<br />
<br />
   <ul>
<li><b>4</b> No of Board </li>
<li><b>60 Day</b> Archiving </li>
<li><b>4</b> No of Board </li>
<li><b>60 Day</b> Archiving </li>
<li><b>4</b> No of Board </li>
<li><b>60 Day</b> Archiving </li>
<li><b>4</b> No of Board </li>
<li><b>60 Day</b> Archiving </li>
</ul>


</div>

<br />

<div class="colorbox">
<span>What's included with your Enterprise plan?</span>
<br />
<br />
  <ul>
<li><b>4</b> No of Board </li>
<li><b>60 Day</b> Archiving </li>
<li><b>4</b> No of Board </li>
<li><b>60 Day</b> Archiving </li>
<li><b>4</b> No of Board </li>
<li><b>60 Day</b> Archiving </li>
<li><b>4</b> No of Board </li>
<li><b>60 Day</b> Archiving </li>
</ul>
</div>
</div>

</div>
</asp:Content>

