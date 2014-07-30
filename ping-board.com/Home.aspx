<%@ Page Title="" Language="C#" MasterPageFile="~/PingBoard.master" AutoEventWireup="true"
    CodeFile="Home.aspx.cs" Inherits="Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/freewall.js" type="text/javascript"></script>
    <style type="text/css">
        .panel
        {
            width: 250px;
            margin: 5px;
            float: left;
            background: #fff;
            border-radius: 0px;
            position:relative;
            word-break:break-all;
        }
        .panel-header
        {
            width: 250px;
            height: 40px;
        }
       
        .panel-header-left
        {
            float: left;
            width: 240px;
        }
        .p-img
        {
            width: 50px;
            float: left;
            padding: 10px;
        }
        .p-txt
        {
            width: 150px;
            float: left;
            padding: 10px 5px 5px 5px;
        }
        .p-txt span
        {
            font-size: 12px;
            color: #333;
        }
        .panel-mid
        {
            width: 220px;
            padding: 10px;
            font-size: 12px;
        }
        .panel-bottom
        {
            width: 230px;
            padding: 10px;
        }
        .panel-bottom-left
        {
            float: left;
            font-size: 11px;
            color: #333;
            width:30px;
        }
        .panel-bottom-right
        {
            float: right;
            width:180px;
            text-align:right;
        }
        .panel-bottom-right img{ margin-right:4px;}
        .panel-with-img
        {
            width: 250px;
            margin: 5px;
            float: left;
            background: #fff;
            border-radius: 4px;
            position:relative;
        }
        .panel-with-img img
        {
            border-radius: 4px 4px 0px 0px;
        }
        
        .corner-icon
        {
            position: absolute;
            top: 0px;
            right: 0px;
        }
        .datetxt{ font-size:10px !important; color:#27b0cd !important;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="boxpanel" style="display:none;">
        <div class="boxpanel-header">
            <div class="boxpanel-hed-txt1">
                Reply to Tweet : 
            </div>
            <div class="box-close-btn">
                <img src="images/popup-close.png" alt="closebtn" />
            </div>
        </div>
        <div class="boxpanel-bottom">
            <div class="boxpanel-bottom-left">
                Message
            </div>
            <input type="hidden" id="panID" value="0" />
            <div class="boxpanel-bottom-right">
                <asp:TextBox ID="rpMsg" TextMode="MultiLine" CssClass="textbox" runat="server"></asp:TextBox>
            </div>
            <div class="boxpanel-bottom-left">
            </div>
            <div class="boxpanel-bottom-right">
                <span style="cursor:pointer" id="rpBtn" class="bluebtn">Reply</span>
            </div>
        </div>
    </div> 
    <div id="Result" runat="server"></div>
    <input type="hidden" id="TotPost" value="0" runat="server" />
    <div id="freewall" class="right-panel" style="margin-left:-200px">
        <div id="divProducts">
        <%--TWITTER PANEL--%>
        <asp:DataList ID="Data1" GridLines="None" OnItemDataBound="Data1_Bound" RepeatDirection="Horizontal" 
            RepeatLayout="Flow" runat="server">
            <ItemTemplate>
        <div class="panel">
            <input type="hidden" id="cID" runat="server" value='<%# Eval("ContID") %>' />
            <input type="hidden" id="cMsg" runat="server" value="Test Text" />
            <input type="hidden" id="cUID" runat="server" value='<%# Eval("ContUserID") %>' />
            <div id="topData" runat="server"></div>
            <div class="panel-header">
                <div class="panel-header-left">
                    <div class="p-img">
                        <asp:Image ID="ContUserPic" Width="48" Height="48" AlternateText='<%#Eval("ContUserName") %>' ImageUrl='<%#Eval("ContUserPic") %>' runat="server" />
                    </div>
                    <div class="p-txt">
                        <asp:Label ID="ContDate" runat="server" CssClass="datetxt" Text='<%#Eval("ContDate", "{0:dd MMM hh:mm tt}") %>'></asp:Label><br />
                        <b><%#Eval("ContUserName") %></b><br />
                        <span><asp:Label ID="ContUserID" runat="server" Text='<%#Eval("ContUserID") %>'></asp:Label></span>
                    </div>
                    <div class="corner-icon">
                        <asp:Image ID="ContPlatform" AlternateText='<%#Eval("ContPlatform") %>' runat="server" />
                    </div>
                </div>
            </div>
            <div class="panel-mid">
                <%#Eval("ContMsg") %>
                <asp:Label ID="ContLK" runat="server" Visible="false" Text='<%#Eval("ContLike") %>'></asp:Label>
                <asp:Label ID="ContLink" runat="server" Visible="false" Text='<%#Eval("ContLink") %>'></asp:Label>
                <asp:Label ID="ContMsgType" runat="server" Visible="false" Text='<%#Eval("ContMsgType") %>'></asp:Label>
            </div>
            <div class="panel-bottom">
                <div class="panel-bottom-left"><img id="delImg" runat="server" style="cursor:pointer" alt="delete-icon" src="images/delete-box-icon.png" /></div>
                <div id="botLayout" runat="server" class="panel-bottom-right">
                    <div style="float:left; margin:3px 3px 0px 3px;"> <span ID="L1" class="rpTweet" runat="server"><img src="images/share.png" alt="Reply" /></span></div>
                    <div id="botLK" runat="server" class="share__count">0</div>
                    <div style="float:left;margin:3px 5px 0px 5px;"> <span ID="L2" runat="server"><img src="images/retweet2.png" alt="Retweet" /></span></div>
                    <div id="botCM" runat="server" class="share__count"><%#Eval("ContLike") %></div>
                    <div style="float:left;margin:3px 5px 0px 5px;"> <span ID="L3" runat="server"><img src="images/favourite.png" alt="Favourite" /></span></div>
                    <div id="botSH" runat="server" class="share__count"><%#Eval("ContComment") %></div>
                </div>
            </div>
        </div>
            </ItemTemplate>
        </asp:DataList>
            <div style="width:100%;clear:both">&nbsp;</div>
            </div>
        <div id="divProgress" style="text-align:center">
            <img src="images/loading.gif" alt="" />
        </div>
    </div>
        <script type="text/javascript">
            $(document).on('click', '.panel-bottom-left img', function () {
                var id = $(this).attr('id');
                id = id.replace('ContentPlaceHolder1_Data1_delImg_', '');
                var cID = $('#ContentPlaceHolder1_Data1_cID_' + id).val();
                id = $(this).parent().parent().parent().attr('id');
                //alert(cID);
                PageMethods.ExcludeContent(cID);
                $('#' + id).remove();
                wall.fitWidth();
            });
            $('.box-close-btn img').click(function () {
                $('.boxpanel').hide();
            });
            $('.rpTweet').click(function (e) {
                e.preventDefault();
                var id = $(this).attr('id');
                id = id.replace('ContentPlaceHolder1_Data1_L1_', '');
                $('#panID').val(id);
                var cID = $('#ContentPlaceHolder1_Data1_cID_' + id).val();
                var cUID = $('#ContentPlaceHolder1_Data1_cUID_' + id).val();
                $('.boxpanel').show();
                $('.boxpanel-hed-txt1').text('Reply to Tweet : ' + cUID);
            });
            $('#rpBtn').click(function () {
                var txt = $('#ContentPlaceHolder1_rpMsg').val();
                if (txt.trim() != "") {
                    var id = $('#panID').val();
                    var cUID = $('#ContentPlaceHolder1_Data1_cUID_' + id).val();
                    var cID = $('#ContentPlaceHolder1_Data1_cID_' + id).val();
                    $('#ContentPlaceHolder1_Data1_cMsg_' + id).val(txt);
                    //var PubSerUrl = "/PINGBOARD/Home.aspx/";
                    var PubSerUrl = "/Home.aspx/";
                    var userInfo = "{ 'ID': '" + cID + "', 'uID': '" + cUID + "', 'msg': '" + txt + "' }";
                    //alert(userInfo);
                    $.ajax({
                        type: "POST",
                        url: PubSerUrl + "TweetReply",
                        data: userInfo,
                        contentType: "application/json; charset=utf-8",
                        success: function (msg) {
                            alert(msg.d);
                            $('.boxpanel').hide();
                        },
                        error: function () {
                            alert('Got Some Error! Tweet Reply Failed!');
                        }
                    });
                } else {
                    alert("Fill Message Box");
                }
            });
        </script>
    <script type="text/javascript">
        var wall = new freewall("#freewall");
        wall.reset({
            selector: '.panel',
            animate: true,
            cellW: 250,
            cellH: 'auto',
            onResize: function () {
                wall.fitWidth();
            }
        });

        wall.container.find('.panel img').load(function () {
            wall.fitWidth();
        });
    </script>
    <script type="text/javascript">
        var previousProductId = 0;
        var checkVal = 0;
        //alert($(window).height());
        //Max records to display at a time in the grid.
        $(document).ready(function () {
            //initially hide the loading gif
            $("#divProgress").hide();
            //$('#freewall').height($(window).height());
            //Attach function to the scroll event of the div
            $(window).scroll(function () {
                //alert('i am here');
                var scrolltop = $(window).scrollTop();
                var scrollheight = $('#freewall').height();
                var windowheight = $(window).height();
                var scrolloffset = 20;
                if (checkVal == 2) {
                    wall.fitWidth();
                    checkVal = 0;
                }
                //$('#MyboardName').html('Top : ' + scrolltop + ' height : ' + scrollheight + ' : ' + windowheight);
                if (scrollheight <= (windowheight + scrolltop + scrolloffset)) {
                    if (checkVal == 0) {
                        //alert('i am here');
                        BindNewData();
                    }
                }
            });
        });
        function BindNewData() {
            //var lastProductId = $("#ContentPlaceHolder1_Data1 .panel:last").children("td:first").html();
            var maxRecordsToDisplay = $('#ContentPlaceHolder1_TotPost').val();
            var lastProductId = GetRowsCount();
            //get last table row in order to append the new products
            var lastRow = $("#ContentPlaceHolder1_Data1 .panel:last");
            $("#divProgress").show();
            checkVal = 1;
            //Fetch records only when the no. of records displayed in the grid are less than limit.
            //alert(GetRowsCount() + " : " + maxRecordsToDisplay);
            if (GetRowsCount() < maxRecordsToDisplay) {
                //alert('i am here');
                $.post("FetchRecordsHandler.ashx?lastProductId=" + lastProductId, function (data) {
                    if (data != null) {
                        //append new products rows to last row in the gridview.
                        lastRow.after(data);
                        $("#divProgress").hide();
                        checkVal = 2;
                        //$(window).trigger("resize");
                        wall.fitWidth();
                    } else {
                        $("#divProgress").hide();
                        checkVal = 0;
                    }
                });
            }
            else {
                $("#divProgress").hide();
                wall.fitWidth();
                checkVal = 0;
            }
        }

        function GetRowsCount() {
            //Count no. of rows except header row in the grid.
            var rowCount = $('#ContentPlaceHolder1_Data1 .panel').length;
            return rowCount;
        }
    </script>
</asp:Content>
