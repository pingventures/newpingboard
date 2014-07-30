<%@ Page Title="" Language="C#" MasterPageFile="~/web.master" AutoEventWireup="true"
    CodeFile="price.aspx.cs" Inherits="price" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .pricing-table
        {
            width: 1000px;
            padding: 20px;
            margin-top: 90px;
            margin-bottom: 30px;
            margin-left: auto;
            margin-right: auto;
        }
        
        .pricing-table td{
            position: relative;
            display: inline-block;
            margin: 0 5px;
            vertical-align: text-top;
        }
        .pricing-table thead .plan td{
            width: 210px;
            height: 42px;
            padding: 15px 0;
            text-align: center;
            -webkit-border-radius: 2px;
            -moz-border-radius: 2px;
            border-radius: 2px;
        }
        .pricing-table thead .plan h2
        {
            font-family: 'Arial Black' , Arial, Helvetica, sans-serif;
            font-weight: bold;
            font-size: 22px;
            text-transform: uppercase;
            line-height: 4px;
        }
        
        .pricing-table thead .plan em
        {
            font-family: Georgia, Arial, Helvetica, sans-serif;
            font-style: italic;
            font-size: 14px;
            line-height: 16px;
        }
        .pricing-table thead .plan .green
        {
            color: #36611e;
            text-shadow: 1px 1px 0px rgba(255,255,255, .2);
            background: url(images/green_pattern.png) repeat-x 0 0;
        }
        
        .pricing-table thead .price .orange
        {
            color: #fafafa;
            text-shadow: 1px 1px 2px rgba(0,0,0, .4);
            background: url(images/orange_pattern.png) repeat-x 0 0;
            margin-top: 65px;
        }
        
        .pricing-table thead .price td
        {
            position: relative;
            width: 210px;
            padding: 25px 0;
            font-family: 'Arial Black' , Arial, Helvetica, sans-serif;
            font-weight: bold;
            text-transform: uppercase;
            text-align: center;
            color: #b6b07c;
            background: #f9f8f1;
            background: -moz-linear-gradient(top,  #f9f8f1 0%, #f4f2e2 100%);
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#f9f8f1), color-stop(100%,#f4f2e2));
            background: -webkit-linear-gradient(top,  #f9f8f1 0%,#f4f2e2 100%);
            background: -o-linear-gradient(top,  #f9f8f1 0%,#f4f2e2 100%);
            background: -ms-linear-gradient(top,  #f9f8f1 0%,#f4f2e2 100%);
            background: linear-gradient(to bottom,  #f9f8f1 0%,#f4f2e2 100%);
        }
        
        .pricing-table thead .price p
        {
            display: table;
            margin: 0 auto;
            font-size: 24px;
            line-height: 60px;
        }
        
        .pricing-table thead .price p span
        {
            font-size: 0.5em;
            display: table-cell;
            vertical-align: middle;
        }
        
        .pricing-table thead .price span
        {
            font-size: 14px;
        }
        .pricing-table thead .price a
        {
            display: block;
            position: absolute;
            top: 41px;
            right: -5px;
            height: 32px;
            padding: 0 10px;
            line-height: 32px;
            font-size: 12px;
            text-decoration: none;
        }
        
        .pricing-table thead .price .green a
        {
            color: #37621f;
            text-shadow: 1px 1px 0px rgba(255,255,255, .2);
            background: #82d344;
            background: -moz-linear-gradient(top,  #82d344 0%, #51af34 100%);
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#82d344), color-stop(100%,#51af34));
            background: -webkit-linear-gradient(top,  #82d344 0%,#51af34 100%);
            background: -o-linear-gradient(top,  #82d344 0%,#51af34 100%);
            background: -ms-linear-gradient(top,  #82d344 0%,#51af34 100%);
            background: linear-gradient(to bottom,  #82d344 0%,#51af34 100%);
        }
        
        .pricing-table thead .price .orange a
        {
            color: #37621f;
            text-shadow: 1px 1px 2px rgba(0,0,0, .3);
            background: #ff8042;
            background: -moz-linear-gradient(top,  #ff8042 0%, #f55a0e 100%);
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#ff8042), color-stop(100%,#f55a0e));
            background: -webkit-linear-gradient(top,  #ff8042 0%,#f55a0e 100%);
            background: -o-linear-gradient(top,  #ff8042 0%,#f55a0e 100%);
            background: -ms-linear-gradient(top,  #ff8042 0%,#f55a0e 100%);
            background: linear-gradient(to bottom,  #ff8042 0%,#f55a0e 100%);
        }
        .pricing-table thead .price .green a:before, .pricing-table thead .price .orange a:before, .pricing-table thead .price .green a:after, .pricing-table thead .price .orange a:after
        {
            display: block;
            position: absolute;
            content: '';
        }
        
        .pricing-table thead .price .green a:before, .pricing-table thead .price .orange a:before
        {
            width: 8px;
            height: 32px;
            top: 0;
            left: -8px;
        }
        
        .pricing-table thead .price .green a:after, .pricing-table thead .price .orange a:after{
            width: 0;
            height: 0;
            bottom: -5px;
            right: 0;
            border-bottom: 5px solid transparent;
        }
        
        .pricing-table thead .price .green a:before{
            background-position: 0 0;
        }
        .pricing-table thead .price .orange a:before
        {
            background-position: 0 -32px;
        }
        .pricing-table thead .price .green a:after
        {
            border-left: 5px solid #1c5d40;
        }
        .pricing-table thead .price .orange a:after
        {
            border-left: 5px solid #88330a;
        }
        .pricing-table tbody tr:first-child td:before
        {
            position: absolute;
            display: block;
            content: '';
            width: 100%;
            top: -25px;
            left: 0;
        }
        
        .pricing-table tbody td
        {
            width: 170px;
            padding-left: 39px;
            line-height: 30px;
            font-size: 14px;
            color: #333;
        }
        
        
        .pricing-table .clock-icon td, .pricing-table .basket-icon td, .pricing-table .star-icon td, .pricing-table .heart-icon td
        {
            background: #ffffff;
        }
        
        .pricing-table .clock-icon td
        {
            background-position: 0 0;
        }
        .pricing-table .basket-icon td
        {
            background-position: 0 -30px;
        }
        .pricing-table .star-icon td
        {
            background-position: 0 -60px;
        }
        .pricing-table .heart-icon td
        {
            background-position: 0 -90px;
        }
        
        
        .pricing-table tfoot td
        {
            width: 190px;
            padding: 25px 10px;
            text-align: center;
            line-height: 18px;
            background: #ffffff;
            font-family: Helvetica, Arial, sans-serif;
            font-size: 13px;
            color: #828282;
            -webkit-border-radius: 0 0 2px 2px;
            -moz-border-radius: 0 0 2px 2px;
            border-radius: 0 0 2px 2px;
            -webkit-box-shadow: 0px 2px 0px #e4e4e4;
            -moz-box-shadow: 0px 2px 0px #e4e4e4;
            box-shadow: 0px 2px 0px #e4e4e4;
        }
        
        .b_txt
        {
            font-weight: bold;
            font-size: 13px !important;
            background: #73c1f0;
        }
        .b_bg
        {
            background: #73c1f0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table class="pricing-table">
        <thead>
            <tr class="plan">
                <td>
                </td>
                <td class="green">
                    <h2>
                        Standard</h2>
                    <em>Only for demo</em>
                </td>
                <td class="green">
                    <h2>
                        Pro</h2>
                    <em>Great for small business</em>
                </td>
                <td class="green">
                    <h2>
                        Enterprise</h2>
                    <em>Great for large business</em>
                </td>
            </tr>
            <tr class="price">
                <td class="orange">
                    <span>Tool Features</span>
                </td>
                <td class="green">
                    <p>
                        <span>INR</span>35,000</p>
                    <span>&nbsp</span> <a href="register.aspx">Buy</a>
                </td>
                <td class="green">
                    <p>
                        <span>INR</span>13,500</p>
                    <span>monthly</span> <a href="register.aspx">Buy</a>
                </td>
                <td class="green">
                    <p>
                        <span>INR</span>25,000</p>
                    <span>monthly</span> <a href="register.aspx">Buy</a>
                </td>
            </tr>
        </thead>
        <tbody>
            <tr class="clock-icon">
                <td class="b_txt b_bg">
                    No. of Boards
                </td>
                <td>
                    Single Board
                </td>
                <td>
                   Upto 3 board/month
                </td>
                <td>
                    Unlimited
                </td>
            </tr>
            <tr class="basket-icon">
                <td class="b_txt b_bg">
                    Multiple Hashtags
                </td>
                <td>
                    No
                </td>
                <td>
                    Advanced Options
                </td>
                <td>
                    Advanced Options
                </td>
            </tr>
            <tr class="star-icon">
                <td class="b_txt b_bg">
                    Updates
                </td>
                <td>
                    5 Minutes
                </td>
                <td>
                    Near real-time
                </td>
                <td>
                    Near real-time
                </td>
            </tr>
            <tr class="heart-icon">
                <td class="b_txt b_bg">
                    Customisation (Brand Logo and masthead)
                </td>
                <td style="height:40px; padding-top:20px;">
                    No
                  
                </td>
                <td style="height:40px; padding-top:20px;">
                    No
                </td>
                <td style="height:40px; padding-top:20px;">
                    Yes
                </td>
            </tr>
            <tr class="heart-icon">
                <td class="b_txt b_bg"> Archiving</td>
                <td>
                    No
                </td>
                <td>
                    60 Days
                </td>
                <td>
                    90 Days
                </td>
            </tr>
            <tr class="clock-icon">
                <td style="color: #fff; background: #fa6f2a; font-weight: bold; font-size: 14px;">
                    Content Management
                </td>
                <td style="color: #fa6f2a; background: #fa6f2a">
                    dd
                </td>
                <td style="color: #fa6f2a; background: #fa6f2a">
                    dd
                </td>
                <td style="color: #fa6f2a; background: #fa6f2a">
                    dd
                </td>
            </tr>
            <tr class="heart-icon">
                <td class="b_txt b_bg">
                    Moderation
                </td>
                <td>
                    No
                </td>
                <td>
                    Yes
                </td>
                <td>
                    Yes
                </td>
            </tr>
            <tr class="heart-icon">
                <td class="b_txt b_bg">
                    Exclude Keywords
                </td>
                <td>
                    No
                </td>
                <td>
                    Yes
                </td>
                <td>
                    Yes
                </td>
            </tr>
            <tr class="heart-icon">
                <td class="b_txt">Engage</td>
                <td>No</td>
                <td>Yes</td>
                <td>Yes</td>
            </tr>
            <tr class="heart-icon">
                <td style="color: #fff; background: #fa6f2a; font-weight: bold; font-size: 14px;">
                    Integration
                </td>
                <td style="color: #fa6f2a; background: #fa6f2a">dd</td>
                <td style="color: #fa6f2a; background: #fa6f2a">
                    dd
                </td>
                <td style="color: #fa6f2a; background: #fa6f2a">
                    dd
                </td>
            </tr>
            <tr class="heart-icon">
                <td class="b_txt">
                    Embed/iframe
                </td>
                <td>No</td>
                <td>Yes</td>
                <td>Yes</td>
            </tr>
            <tr class="heart-icon">
                <td class="b_txt">
                    Analytics
                </td>
                <td>
                    No
                </td>
                <td>
                    Basic
                </td>
                <td>
                    Advanced
                </td>
            </tr>
        </tbody>
        <tfoot>
            <tr>
                <td align="center">
                    All prices are in INR
                </td>
                <td align="center" valign="middle" style="padding: 10px 10px 10px 10px;">
                    <asp:Button ID="Button1" CssClass="action-button shadow animate greenbtn" runat="server"
                        Text="Signup" />
                </td>
                <td align="center" style="padding: 10px 10px;">
                    <asp:Button ID="Button2" runat="server" CssClass="action-button shadow animate greenbtn"
                        Text="Signup" />
                </td>
                <td align="center" style="padding: 10px 10px;">
                    <asp:Button ID="Button3" runat="server" CssClass="action-button shadow animate greenbtn"
                        Text="Signup" />
                </td>
            </tr>
        </tfoot>
    </table>
</asp:Content>
