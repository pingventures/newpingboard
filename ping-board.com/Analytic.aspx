<%@ Page Title="" Language="C#" MasterPageFile="~/PingBoard.master" AutoEventWireup="true"
    CodeFile="Analytic.aspx.cs" Inherits="Analytic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="js/Chart.js" type="text/javascript"></script>
    <script src="js/raphael.js" type="text/javascript"></script>
    <script src="js/jquery.js" type="text/javascript"></script>
    <script src="js/init.js" type="text/javascript"></script>
    <link href="css/default.css" rel="stylesheet" />     
    <style type="text/css">
        
        body
        {
            background: #fff !important;
        }
        .analytic-area
        {
            margin: 10px 10px 10px 40px;
        }
        .analytic-panel
        {
            width:45%;
            margin: 10px 10px 10px 10px;
            padding: 0px;
            float: left;
            text-align:center;
            height:500px;
            background:#f3f3f3;
            
        }
        .analytic-panel .h{
            background: #3ab3b1;
            padding: 10px;
            font-size: 14px;
            color: #fff;
        }
        .analytic-panel span{
            font-size: 46px;
            font-weight:bold;
            
        }
        .analytic-panel table{ border:0px; width:100%; margin:0px; padding:0px; font-size:13px; color:#fff;}
        .analytic-panel table tr{ border:0px solid #333; background:#949494; margin:0px;}
        .analytic-panel table tr td{ border:1px solid #fff; padding:5px;}
        
        .chart-name{ width:200px; margin:10px 0px 10px 130px;}
        .chart-name-panel{ width:200px; height:25px;}
        .chart-color{ background:#F7464A; width:15px; height:15px; float:left;}
         .chart-color1{ background:#46BFBD; width:15px; height:15px; float:left;}
          .chart-color2{ background:#FDB45C; width:15px; height:15px; float:left;}
        .chart-text{  text-align:left; font-size:12px; color:#000; font-weight:bold; float:left; margin-left:6px;}
        .sm-txt{ font-size:12px !important;
                 }
        .color1{color:#eb3a3e !important;}
        .color2{ color:#3ab3b1 !important;}
        .color3{ color:#f1a850 !important;}
        .spinPie{ position:relative; text-align:center; top:40%; margin-top:-43.5px}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin: 40px 0px 0px 40px; height: 100%;">
        <div class="analytic-panel">
            <div class="h">Total Number</div>
            <div id="content">
                <div id="diagram"></div>
                <div class="legend">
                    <div class="skills">
                        <ul>
                            <li class="jq">Total Post</li>
                            <li class="css">Unique User</li>
                            <li class="html">Reach</li>
                            <li class="sql">Impressions</li>
                        </ul>
                    </div>

                </div>
            </div>

            <div class="get">
                <div class="arc">
                    <span class="text">Total Post</span>
                    <input type="hidden" class="mainval" id="totalpost" value="90" runat="server" />
                    <input type="hidden" class="percent" id="Hidden1" value="95" runat="server" />
                    <input type="hidden" class="color" value="#f7464a" />
                </div>
                <div class="arc">
                    <span class="text">Unique User</span>
                    <input type="hidden" class="mainval" id="totuni" value="5" runat="server" />
                    <input type="hidden" class="percent" id="Hidden2" value="75" runat="server" />
                    <input type="hidden" class="color" value="#fdb45c" />
                </div>
                <div class="arc">
                    <span class="text">Reach</span>
                    <input type="hidden" class="mainval" id="totreach" value="80" runat="server" />
                    <input type="hidden" class="percent" id="Hidden3" value="90" runat="server" />
                    <input type="hidden" class="color" value="#46bfbd" />
                </div>
                <div class="arc">
                    <span class="text">Impressions</span>
                    <input type="hidden" class="mainval" id="impession" value="45" runat="server" />
                    <input type="hidden" class="percent" id="Hidden4" value="60" runat="server" />
                    <input type="hidden" class="color" value="#0975ab" />
                </div>
            </div>
        </div>
        <script type="text/javascript">
            PageMethods.canvas4(onSucess);
            function onSucess(res) {
                var spitnet = res.split('-');
                //alert(spitnet[0]);
                $('#ContentPlaceHolder1_totuni').val(spitnet[0]);
                //text(spitnet[0]);
                $('#ContentPlaceHolder1_totalpost').val(spitnet[1]);
            }
        </script>
        <%--Other HashTag Trending--%>
        <div class="analytic-panel" style="overflow: hidden; height: 500px;">
            <div class="h">Other # Tag Use</div>
            <table id="AnotherTags" runat="server" width="200" border="1"></table>
        </div>
        <script type="text/javascript">
            PageMethods.AnoHash(onSucess);
            function onSucess(res) {
                $('#ContentPlaceHolder1_AnotherTags').html(res);
            }
        </script>

        <%--SPlit By Network--%>
        <div class="analytic-panel">
            <div class="h">Split By Network</div>
            <input type="hidden" id="plat1" runat="server" value="40" />
            <input type="hidden" id="plat2" runat="server" value="30" />
            <input type="hidden" id="plat3" runat="server" value="30" />
            <canvas id="canvas1" height="350" width="350"></canvas>

            <div class="chart-name">
                <div class="chart-name-panel">
                    <div class="chart-color"></div>
                    <div class="chart-text">Twitter :
                        <asp:Label ID="plat1l" CssClass="sm-txt color1" runat="server" Text="2134"></asp:Label></div>
                </div>
                <div class="chart-name-panel">
                    <div class="chart-color1"></div>
                    <div class="chart-text">Facebook :
                        <asp:Label ID="plat2l" CssClass="sm-txt color2" runat="server" Text="2134"></asp:Label></div>
                </div>
                <div class="chart-name-panel">
                    <div class="chart-color2"></div>
                    <div class="chart-text">Instagram :
                        <asp:Label ID="plat3l" CssClass="sm-txt color3" runat="server" Text="2134"></asp:Label></div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            PageMethods.canvas1(onSucess);
            function onSucess(res) {
                var spitnet = res.split('-');
                $('#ContentPlaceHolder1_plat1l').text(spitnet[0]);
                $('#ContentPlaceHolder1_plat2l').text(spitnet[1]);
                $('#ContentPlaceHolder1_plat3l').text(spitnet[2]);

                var p1 = parseInt(spitnet[0]);
                var p2 = parseInt(spitnet[1]);
                var p3 = parseInt(spitnet[2]);

                var doughnutData = [
                    {
                        value: p1,
                        color: "#F7464A"
                    },
                    {
                        value: p2,
                        color: "#46BFBD"
                    },
                    {
                        value: p3,
                        color: "#FDB45C"
                    }
                ];
                var myDoughnut = new Chart(document.getElementById("canvas1").getContext("2d")).Doughnut(doughnutData, { scaleShowValues: false });
            }
        </script>

        <%--SPlit By Type--%>
        <div class="analytic-panel">
            <div class="h">Images Vs Video Vs Text</div>
            <%--<div id="imgPie" class="spinPie">
                <img src="images/loading.gif" alt="Loading" /></div>--%>
            <input type="hidden" id="fm1" runat="server" value="40" />
            <input type="hidden" id="fm2" runat="server" value="30" />
            <input type="hidden" id="fm3" runat="server" value="30" />
            <input type="hidden" id="chartgp" runat="server" value="30" />
            <canvas id="canvas3" height="350" width="350"></canvas>
            <div class="chart-name">
                <div class="chart-name-panel">
                    <div class="chart-color"></div>
                    <div class="chart-text">Images :
                        <asp:Label ID="fm1l" CssClass="sm-txt color1" runat="server" Text="0"></asp:Label></div>
                </div>
                <div class="chart-name-panel">
                    <div class="chart-color1"></div>
                    <div class="chart-text">Video :
                        <asp:Label ID="fm2l" CssClass="sm-txt color2" runat="server" Text="0"></asp:Label></div>
                </div>
                <div class="chart-name-panel">
                    <div class="chart-color2"></div>
                    <div class="chart-text">Text :
                        <asp:Label ID="fm3l" CssClass="sm-txt color3" runat="server" Text="0"></asp:Label></div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            PageMethods.canvas2(onSucess);
            function onSucess(res) {
                var spitnet = res.split('-');
                $('#ContentPlaceHolder1_fm1l').text(spitnet[0]);
                $('#ContentPlaceHolder1_fm2l').text(spitnet[1]);
                $('#ContentPlaceHolder1_fm3l').text(spitnet[2]);

                var p1 = parseInt(spitnet[0]);
                var p2 = parseInt(spitnet[1]);
                var p3 = parseInt(spitnet[2]);
                //alert(spitnet);
                //var p1 = parseInt($('#ContentPlaceHolder1_fm1').val());
                //var p2 = parseInt($('#ContentPlaceHolder1_fm2').val());
                //var p3 = parseInt($('#ContentPlaceHolder1_fm3').val());
                //$('#imgPie').hide();
                var doughnutData = [
                    {
                        value: p1,
                        color: "#F7464A"
                    },
                    {
                        value: p2,
                        color: "#46BFBD"
                    },
                    {
                        value: p3,
                        color: "#FDB45C"
                    }

                ];
                var myDoughnut = new Chart(document.getElementById("canvas3").getContext("2d")).Doughnut(doughnutData, { scaleShowValues: false });
            }
        </script>
        <%--Split By Unique User--%>
        <div class="analytic-panel">
            <div class="h">Split By Unique User</div>
            <input type="hidden" id="TwitterUniUser" runat="server" value="40" />
            <input type="hidden" id="FbUniUser" runat="server" value="30" />
            <input type="hidden" id="InstUniUser" runat="server" value="30" />
            <canvas id="canvas4" height="350" width="350"></canvas>

            <div class="chart-name">
                <div class="chart-name-panel">
                    <div class="chart-color"></div>
                    <div class="chart-text">Twitter :
                        <asp:Label ID="TwitterUniUserl" CssClass="sm-txt color1" runat="server" Text="2134"></asp:Label></div>
                </div>
                <div class="chart-name-panel">
                    <div class="chart-color1"></div>
                    <div class="chart-text">Facebook :
                        <asp:Label ID="FbUniUserl" CssClass="sm-txt color2" runat="server" Text="2134"></asp:Label></div>
                </div>
                <div class="chart-name-panel">
                    <div class="chart-color2"></div>
                    <div class="chart-text">Instagram :
                        <asp:Label ID="InstUniUserl" CssClass="sm-txt color3" runat="server" Text="2134"></asp:Label></div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            PageMethods.canvas3(onSucess);
            function onSucess(res) {
                var spitnet = res.split('-');
                $('#ContentPlaceHolder1_TwitterUniUserl').text(spitnet[0]);
                $('#ContentPlaceHolder1_FbUniUserl').text(spitnet[1]);
                $('#ContentPlaceHolder1_InstUniUserl').text(spitnet[2]);

                var u1 = parseInt(spitnet[0]);
                var u2 = parseInt(spitnet[1]);
                var u3 = parseInt(spitnet[2]);
                //var u1 = parseInt($('#ContentPlaceHolder1_TwitterUniUser').val());
                //var u1 = parseInt($('#ContentPlaceHolder1_TwitterUniUser').val());
                //var u2 = parseInt($('#ContentPlaceHolder1_FbUniUser').val());
                //var u3 = parseInt($('#ContentPlaceHolder1_InstUniUser').val());

                var doughnutData = [
                    {
                        value: u1,
                        color: "#F7464A"
                    },
                    {
                        value: u2,
                        color: "#46BFBD"
                    },
                    {
                        value: u3,
                        color: "#FDB45C"
                    }
                ];
                var myDoughnut = new Chart(document.getElementById("canvas4").getContext("2d")).Doughnut(doughnutData, { scaleShowValues: false });
            }
        </script>

        <%--SentiMent Analysis IN TWITTER--%>
        <div class="analytic-panel">
            <div class="h">
                Sentiment Analysis In Twitter
            </div>
            <canvas id="canvas6" height="410" width="500"></canvas>
            <div id="twlineLegend">
                <asp:HiddenField ID="twitterpost" runat="server" />
                <asp:HiddenField ID="twitterneg" runat="server" />
                <asp:HiddenField ID="twitterneu" runat="server" />
            </div>
            <div class="chart-name" style="width: 450px !important; margin: 10px 10px 0px 130px !important; height: 40px;">
                <div class="chart-name-panel" style="float: left; width: 90px !important;">
                    <div class="chart-color2"></div>
                    <div class="chart-text">Positive</div>
                </div>
                <div class="chart-name-panel" style="float: left; width: 90px !important;">
                    <div class="chart-color1"></div>
                    <div class="chart-text">Neutral</div>
                </div>
                <div class="chart-name-panel" style="float: left; width: 90px !important;">
                    <div class="chart-color"></div>
                    <div class="chart-text">Negative</div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            PageMethods.positive("Twitter", onSucess);
            function onSucess(post) {
                PageMethods.negative("Twitter", onSucess);
                function onSucess(neg) {
                    PageMethods.neutral("Twitter", onSucess);
                    function onSucess(neu) {
                        var totpost = post; //$('#ContentPlaceHolder1_twitterpost').val();                
                        var postspit = totpost.split(',');
                        var lb;
                        var labelpost = [], datapost = [];
                        var dates = [];
                        for (var i = 0; i < postspit.length; i++) {
                            lb = postspit[i].split('-');
                            datapost.push(parseInt(lb[0]));
                            labelpost.push(lb[1]);
                        }
                        totpost = neg;//$('#ContentPlaceHolder1_twitterneg').val();
                        postspit = totpost.split(',');
                        var labelneg = [], dataneg = [];
                        for (var i = 0; i < postspit.length; i++) {
                            lb = postspit[i].split('-');
                            dataneg.push(parseInt(lb[0]));
                            labelneg.push(lb[1]);
                        }
                        totpost = neu;// $('#ContentPlaceHolder1_twitterneu').val();
                        postspit = totpost.split(',');
                        var labelneu = [], dataneu = [];
                        for (var i = 0; i < postspit.length; i++) {
                            lb = postspit[i].split('-');
                            dataneu.push(parseInt(lb[0]));
                            labelneu.push(lb[1]);
                        }
                        if (isNaN(dataneg)) {
                            dataneg[0] = 0;
                        }
                        if (labelpost.length >= labelneg.length && labelpost.length >= labelneu.length) {
                            dates = labelpost;
                        }
                        else if (labelneu.length >= labelpost.length && labelneu.length >= labelneg.length) {
                            dates = labelneu;
                        }
                        else {
                            dates = labelneg;
                        }
                        //if (dates.length > datapost.length) {
                        //    datapost[datapost.length] = 0;
                        //}
                        //if (dates.length > dataneu.length) {
                        //    dataneu[dataneu.length] = 0;
                        //}
                        //if (dates.length > dataneg.length) {
                        //    dataneg[dataneg.length] = 0;
                        //}
                        // alert(dataneu + "-" + dataneg + "-" + datapost);
                        // alert(labelneu + "-" + labelneg + "_" + labelpost);

                        var lineChartData = {
                            labels: dates,
                            datasets: [

                            {
                                fillColor: "rgba(253,180,92,0.5)",
                                strokeColor: "rgba(253,180,92,1)",
                                pointColor: "rgba(220,220,220,1)",
                                pointStrokeColor: "#fff",
                                data: datapost,
                                title: "Positive"
                            },
                            {
                                fillColor: "rgba(70,191,189,0.5)",
                                strokeColor: "rgba(70,191,189,1)",
                                pointColor: "rgba(151,187,205,1)",
                                pointStrokeColor: "#fff",
                                data: dataneu,
                                title: "Nutral"
                            },
                            {
                                fillColor: "rgba(247,70,74,0.5)",
                                strokeColor: "rgba(247,70,74,1)",
                                pointColor: "rgba(151,187,205,1)",
                                pointStrokeColor: "#fff",
                                data: dataneg,
                                title: "Negative"
                            }
                            ]
                        }
                        var myLine = new Chart(document.getElementById("canvas6").getContext("2d")).Line(lineChartData, { showLegend: false });
                    }
                }
            }
        </script>
        <%--SentiMent Analysis IN FaceBook--%>
        <div class="analytic-panel">
            <div class="h">Sentiment Analysis In Facebook</div>
            <canvas id="canvas7" height="410" width="500"></canvas>
            <div id="fblineLegend">
                <asp:HiddenField ID="fbpost" runat="server" />
                <asp:HiddenField ID="fbneg" runat="server" />
                <asp:HiddenField ID="fbneu" runat="server" />
            </div>
            <div class="chart-name" style="width: 450px !important; margin: 10px 10px 0px 130px !important; height: 40px;">
                <div class="chart-name-panel" style="float: left; width: 90px !important;">
                    <div class="chart-color2"></div>
                    <div class="chart-text">Positive</div>
                </div>
                <div class="chart-name-panel" style="float: left; width: 90px !important;">
                    <div class="chart-color1"></div>
                    <div class="chart-text">Neutral</div>
                </div>
                <div class="chart-name-panel" style="float: left; width: 90px !important;">
                    <div class="chart-color"></div>
                    <div class="chart-text">Negative</div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            PageMethods.positive("Facebook", onSucess);
            function onSucess(post) {
                PageMethods.negative("Facebook", onSucess);
                function onSucess(neg) {
                    PageMethods.neutral("Facebook", onSucess);
                    function onSucess(neu) {
                        //alert(post);
                        var totpost = post; //$('#ContentPlaceHolder1_twitterpost').val();
                        var postspit = totpost.split(',');
                        var lb;
                        var labelpost = [], datapost = [];
                        var dates = [];
                        for (var i = 0; i < postspit.length; i++) {
                            lb = postspit[i].split('-');
                            datapost.push(parseInt(lb[0]));
                            labelpost.push(lb[1]);
                        }
                        totpost = neg;//$('#ContentPlaceHolder1_twitterneg').val();
                        postspit = totpost.split(',');
                        var labelneg = [], dataneg = [];
                        for (var i = 0; i < postspit.length; i++) {
                            lb = postspit[i].split('-');
                            dataneg.push(parseInt(lb[0]));
                            labelneg.push(lb[1]);
                        }
                        totpost = neu;// $('#ContentPlaceHolder1_twitterneu').val();
                        postspit = totpost.split(',');
                        var labelneu = [], dataneu = [];
                        for (var i = 0; i < postspit.length; i++) {
                            lb = postspit[i].split('-');
                            dataneu.push(parseInt(lb[0]));
                            labelneu.push(lb[1]);
                        }
                        if (isNaN(dataneg)) {
                            dataneg[0] = 0;
                        }
                        if (labelpost.length >= labelneg.length && labelpost.length >= labelneu.length) {
                            dates = labelpost;
                        }
                        else if (labelneu.length >= labelpost.length && labelneu.length >= labelneg.length) {
                            dates = labelneu;
                        }
                        else {
                            dates = labelneg;
                        }
                        //if (dates.length > datapost.length) {
                        //    datapost[datapost.length] = 0;
                        //}
                        //if (dates.length > dataneu.length) {
                        //    dataneu[dataneu.length] = 0;
                        //}
                        //if (dates.length > dataneg.length) {
                        //    dataneg[dataneg.length] = 0;
                        //}
                        // alert(dataneu + "-" + dataneg + "-" + datapost);
                        // alert(labelneu + "-" + labelneg + "_" + labelpost);

                        var lineChartData = {
                            labels: dates,
                            datasets: [

                            {
                                fillColor: "rgba(253,180,92,0.5)",
                                strokeColor: "rgba(253,180,92,1)",
                                pointColor: "rgba(220,220,220,1)",
                                pointStrokeColor: "#fff",
                                data: datapost,
                                title: "Positive"
                            },
                            {
                                fillColor: "rgba(70,191,189,0.5)",
                                strokeColor: "rgba(70,191,189,1)",
                                pointColor: "rgba(151,187,205,1)",
                                pointStrokeColor: "#fff",
                                data: dataneu,
                                title: "Nutral"
                            },
                            {
                                fillColor: "rgba(247,70,74,0.5)",
                                strokeColor: "rgba(247,70,74,1)",
                                pointColor: "rgba(151,187,205,1)",
                                pointStrokeColor: "#fff",
                                data: dataneg,
                                title: "Negative"
                            }
                            ]
                        }
                        var myLine = new Chart(document.getElementById("canvas7").getContext("2d")).Line(lineChartData, { showLegend: false });
                    }
                }
            }
        </script>
        <%--SentiMent Analysis IN Instagram--%>
        <div class="analytic-panel">
            <div class="h">Sentiment Analysis In Instagram</div>
            <canvas id="canvas8" height="410" width="500"></canvas>
            <div id="Div1">
                <asp:HiddenField ID="inpost" runat="server" />
                <asp:HiddenField ID="inneg" runat="server" />
                <asp:HiddenField ID="inneu" runat="server" />
            </div>
            <div class="chart-name" style="width: 450px !important; margin: 10px 10px 0px 130px !important; height: 40px;">
                <div class="chart-name-panel" style="float: left; width: 90px !important;">
                    <div class="chart-color2"></div>
                    <div class="chart-text">Positive</div>
                </div>
                <div class="chart-name-panel" style="float: left; width: 90px !important;">
                    <div class="chart-color1"></div>
                    <div class="chart-text">Neutral</div>
                </div>
                <div class="chart-name-panel" style="float: left; width: 90px !important;">
                    <div class="chart-color"></div>
                    <div class="chart-text">Negative</div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            PageMethods.positive("Instagram", onSucess);
            function onSucess(post) {
                PageMethods.negative("Instagram", onSucess);
                function onSucess(neg) {
                    PageMethods.neutral("Instagram", onSucess);
                    function onSucess(neu) {
                        // alert(post);
                        var totpost = post; //$('#ContentPlaceHolder1_twitterpost').val();
                        var postspit = totpost.split(',');
                        var lb;
                        var labelpost = [], datapost = [];
                        var dates = [];
                        for (var i = 0; i < postspit.length; i++) {
                            lb = postspit[i].split('-');
                            datapost.push(parseInt(lb[0]));
                            labelpost.push(lb[1]);
                        }
                        totpost = neg;//$('#ContentPlaceHolder1_twitterneg').val();
                        postspit = totpost.split(',');
                        var labelneg = [], dataneg = [];
                        for (var i = 0; i < postspit.length; i++) {
                            lb = postspit[i].split('-');
                            dataneg.push(parseInt(lb[0]));
                            labelneg.push(lb[1]);
                        }
                        totpost = neu;// $('#ContentPlaceHolder1_twitterneu').val();
                        postspit = totpost.split(',');
                        var labelneu = [], dataneu = [];
                        for (var i = 0; i < postspit.length; i++) {
                            lb = postspit[i].split('-');
                            dataneu.push(parseInt(lb[0]));
                            labelneu.push(lb[1]);
                        }
                        if (isNaN(dataneg)) {
                            dataneg[0] = 0;
                        }
                        if (labelpost.length >= labelneg.length && labelpost.length >= labelneu.length) {
                            dates = labelpost;
                        }
                        else if (labelneu.length >= labelpost.length && labelneu.length >= labelneg.length) {
                            dates = labelneu;
                        }
                        else {
                            dates = labelneg;
                        }
                        //if (dates.length > datapost.length) {
                        //    datapost[datapost.length] = 0;
                        //}
                        //if (dates.length > dataneu.length) {
                        //    dataneu[dataneu.length] = 0;
                        //}
                        //if (dates.length > dataneg.length) {
                        //    dataneg[dataneg.length] = 0;
                        //}
                        // alert(dataneu + "-" + dataneg + "-" + datapost);
                        // alert(labelneu + "-" + labelneg + "_" + labelpost);

                        var lineChartData = {
                            labels: dates,
                            datasets: [

                            {
                                fillColor: "rgba(253,180,92,0.5)",
                                strokeColor: "rgba(253,180,92,1)",
                                pointColor: "rgba(220,220,220,1)",
                                pointStrokeColor: "#fff",
                                data: datapost,
                                title: "Positive"
                            },
                            {
                                fillColor: "rgba(70,191,189,0.5)",
                                strokeColor: "rgba(70,191,189,1)",
                                pointColor: "rgba(151,187,205,1)",
                                pointStrokeColor: "#fff",
                                data: dataneu,
                                title: "Nutral"
                            },
                            {
                                fillColor: "rgba(247,70,74,0.5)",
                                strokeColor: "rgba(247,70,74,1)",
                                pointColor: "rgba(151,187,205,1)",
                                pointStrokeColor: "#fff",
                                data: dataneg,
                                title: "Negative"
                            }
                            ]
                        }
                        var myLine = new Chart(document.getElementById("canvas8").getContext("2d")).Line(lineChartData, { showLegend: false });
                    }
                }
            }
        </script>
        <%--Post Volume Per Day--%>
        <div class="analytic-panel" style="width: 91.5%;">
            <div class="h">
                Post Volume Per Day
            </div>
            <canvas id="canvas2" height="450" width="1100"></canvas>
        </div>
        <script type="text/javascript">
            PageMethods.chartgrp(onSucess);
            function onSucess(res) {
                var totpost = res;//$('#ContentPlaceHolder1_chartgp').val();
                var postspit = totpost.split(',');
                var lb;
                var labelpost = [], datapost = [];

                for (var i = 0; i < postspit.length; i++) {
                    lb = postspit[i].split('-');
                    datapost.push(parseInt(lb[0]));
                    labelpost.push(lb[1]);
                }
                var lineChartData = {
                    labels: labelpost,
                    datasets: [
                            {
                                fillColor: "rgba(151,187,205,0.5)",
                                strokeColor: "rgba(151,187,205,1)",
                                pointColor: "rgba(151,187,205,1)",
                                pointStrokeColor: "#fff",
                                data: datapost
                            }
                    ]
                }
                var myLine = new Chart(document.getElementById("canvas2").getContext("2d")).Line(lineChartData);
            }

        </script>
        <%--Top 10 Post--%>
        <div class="analytic-panel" style="width: 91.5%;">
            <div class="h">Top 10 Post</div>

        </div>
    </div>
</asp:Content>
