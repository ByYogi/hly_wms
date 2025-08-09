<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BigDataView.aspx.cs" Inherits="Cargo.Report.BigDataView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>大数据可视化平台</title>
    <link href="../BigData/css/BigData.css" rel="stylesheet" />
    <link href="../BigData/css/index.css" rel="stylesheet" />
    <link href="../BigData/css/index01.css" rel="stylesheet" />
    <link href="../BigData/css/Security_operation.css" rel="stylesheet" />
    <script src="../BigData/js/jquery.js"></script>
    <link href="../BigData/js/bstable/css/bootstrap-table.css" rel="stylesheet" />
    <link href="../BigData/js/bstable/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../BigData/js/artDialog/skins/default.css" rel="stylesheet" />
    <script src="../BigData/js/laydate.js"></script>
    <script src="../BigData/js/Home_page.js"></script>
    <script src="../JS/Date/ComJs.js"></script>
    <style>

    #mapContainer {
      position: relative;
      height: 100%;
      width: 100%;
    }

    .clusterBubble {
      border-radius: 50%;
      color: #fff;
      font-weight: 500;
      text-align: center;
      opacity: 0.88;
      background-image: linear-gradient(139deg, #4294FF 0%, #295BFF 100%);
      
      box-shadow: 0 1px 3px 0 rgba(0, 0, 0, 0.20);
      position: absolute;
      top: 0px;
      left: 0px;
    }
    </style>
</head>

<body>
    <div class="data_bodey">
        <div class="index_nav">
            <ul style="height: 30px; margin-bottom: 0px;">
                <li class="l_left total_chose_fr nav_active"><a href="http://apphome.hlyex.com/data/">好来运数据</a></li>
                <%-- <li class="l_left total_chose_pl"></li>
                <li class="l_left total_chose_pl"></li>
                <li class="r_right total_chose_pl"></li>
                <li class="r_right total_chose_pl"></li>
                <li class="r_right total_chose_pl"></li>--%>
            </ul>
            <div class="total_chose_box" style="display: none;">
                <div style="height: 32px;"></div>
                <span class="chose_tltle">请选择年份：</span>
                <select class="year_chose">
                    <option>2017</option>
                    <option>2016</option>
                    <option>2015</option>
                    <option>2014</option>
                    <option>2013</option>
                    <option>2012</option>
                </select>
                <span class="chose_tltle">请输入月份：</span>
                <input class="chose_text_in">
                <span class="chose_tltle">请选择区域：</span>
                <select class="year_chose">
                    <option>北京市</option>
                    <option>自贡市</option>
                    <option>攀枝花市</option>
                    <option>泸州市</option>
                    <option>德阳市</option>
                    <option>绵阳市</option>
                    <option>广元市</option>
                    <option>遂宁市</option>
                    <option>内江市</option>
                    <option>乐山市</option>
                    <option>南充市</option>
                    <option>宜宾市</option>
                    <option>广安市</option>
                    <option>达州市</option>
                    <option>巴中市</option>
                    <option>雅安市</option>
                    <option>眉山市</option>
                    <option>资阳市</option>
                    <option>阿坝州</option>
                    <option>甘孜州</option>
                    <option>凉山州</option>
                </select>
                <button class="chose_enter">确定</button>
            </div>
            <div class="clear"></div>
        </div>
        <div class="index_tabs">
            <!--实时监控-->
            <div class="inner" style="height: 109%;">

                <div class="left_cage">
                    <div class="dataAllBorder01 cage_cl" style="margin-top: 9% !important; height: 20%;">
                        <div class="dataAllBorder02" id="cage_Month">
                            <div class="analysis">当月销售额：</div>
                            <div id="CurMonthSale"  ></div>

                            <div class="depart_number_box">
                                <ul class="depart_number_cage" style="margin-top: 20px;">
                                    <li class="depart_name">当月订单：</li>
                                    <li class="depart_number" id="MonthOrderNum"></li>
                                </ul>
                                <ul class="depart_number_cage" style="margin-top: 20px;">
                                    <li class="depart_name">当月销量：</li>
                                    <li class="depart_number" id="MonthSaleNum"></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="dataAllBorder01 cage_cl" style="margin-top: 1.5% !important; height: 30%;">
                        <%-- <div class="dataAllBorder02 video_cage">
                            <img class="video_around video_around_chose" src="video/video.jpg">
                        </div>--%>
                        <div class="dataAllBorder02 over_hide dataAllBorder20" id="typeName">
                        </div>
                    </div>
                    <div class="dataAllBorder01 cage_cl check_decrease" style="height: 30%; position: relative;">
                        <div class="dataAllBorder02 over_hide dataAllBorder20" id="saleManRank">
                        </div>
                    </div>
                    <div class="dataAllBorder01 cage_cl check_decrease" style="height: 15%; position: relative;">
                        <div class="dataAllBorder02 over_hide dataAllBorder20" id="OEsaleManRank">
                        </div>
                    </div>
                </div>

                <div class="center_cage">
                    <div class="dataAllBorder01 cage_cl" style="margin-top: 3.5% !important; height: 62.7%; position: relative;">
                        <div class="dataAllBorder02" style="position: relative; overflow: hidden;">
                            <!--标题栏-->
                            <div class="map_title_box" style="height: 6%">
                                <div class="map_title_innerbox">
                                    <div class="map_title">实时地图</div>
                                </div>
                            </div>
                            <div class="map" id="map"></div>
                        </div>
                    </div>

                    <div class="dataAllBorder01 cage_cl" style="margin-top: 0.6% !important; height: 32.1%;">
                        <div class="dataAllBorder02" id="map_title_innerbox">
                            <div class="map_title_box">
                                <div class="map_title_innerbox">
                                    <div class="map_title" <%-- style="background-image: url(img/second_title.png);"--%>>仓库统计排名</div>
                                </div>
                            </div>
                            <table id="table" style="width: 100%"></table>
                        </div>
                    </div>
                </div>

                <div class="right_cage">
                    <!--顶部切换位置-->
                    <div class="dataAllBorder01 cage_cl" style="margin-top: 9% !important; height: 24%">
                        <div class="dataAllBorder02" id="cage_cl">
                            <div class="analysis">实时销售额：</div>
                            <div id="DaySaleMoney"></div>
                            <div class="depart_number_box">
                                <ul class="depart_number_cage" style="margin-top: 35px;">
                                    <li class="depart_name">实时订单：</li>
                                    <li class="depart_number" id="DayOrderNum"></li>
                                </ul>
                                <ul class="depart_number_cage" style="margin-top: 35px;">
                                    <li class="depart_name">实时销量：</li>
                                    <li class="depart_number" id="DaySaleNum"></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="dataAllBorder01 cage_cl" style="margin-top: 1.5% !important; height: 70%; position: relative;">

                        <div class="dataAllBorder02" style="padding: 1.2%; overflow: hidden">
                            <div class="message_scroll_box" id="scrollOrder">
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <!--实时监控-->
        </div>
    </div>
    <script src="../JS/EChart/echarts.js"></script>
    <%-- <script src="../BigData/js/echarts-all.js"></script>--%>
    <script src="../BigData/js/index.js"></script>
    <script src="../BigData/js/bstable/js/bootstrap.min.js"></script>
    <script src="../BigData/js/bstable/js/bootstrap-table.js"></script>
    <script type="text/javascript" src="../BigData/js/jquery.pagination.js"></script>
    <script src="../BigData/js/bstable/js/bootstrap-table-zh-CN.min.js"></script>
    <script charset="utf-8" src="https://map.qq.com/api/gljs?v=1.exp&key=OB4BZ-D4W3U-B7VVO-4PJWW-6TKDJ-WPB77"></script>

    <script src="../BigData/js/artDialog/artDialog.js"></script>
    <script src="../BigData/js/artDialog/plugins/iframeTools.source.js"></script>
    <script type="text/javascript">
        require.config({
            paths: {
                echarts: '../JS/EChart'
            }
        });
    </script>
    <script>
        var number;
        $(function () {
            if (window.screen.height <= 768) {
                number = 5;
            } else if (window.screen.height > 768 && window.screen.height <= 900) {
                number = 6
            } else if (window.screen.height > 1080) {
                number = 8
            }
        });


        //    翻页模块
        $(".tcdPageCode").createPage({
            pageCount: 5,
            current: 1,
            backFn: function (p) { }
        });
        $(".chemistry_tcdPageCode").createPage({
            pageCount: 4,
            current: 1,
            backFn: function (p) { }
        });
        $(".enterprise_tcdPageCode").createPage({
            pageCount: 4,
            current: 1,
            backFn: function (p) { }
        });
        $(".car_tcdPageCode").createPage({
            pageCount: 4,
            current: 1,
            backFn: function (p) { }
        });

        $(function () {
            $(".tit02Diva a").each(function (index) {
                $(this).on("click", function () {
                    $(".data_map").eq(index).fadeIn().siblings(".data_map").stop().hide();
                    $(this).prev('i').removeClass('i_crlieAction');
                    $(this).siblings('a').prev('i').addClass('i_crlieAction');

                })
            });
            BootstrapTable();
            QueryCurBigDataStatis();//实时统计查询
            QueryDataByTypeName(); //按品牌查询
            QueryALLBigOrderSum();//仓库总的实时统计
            //Echarts();
            $("#fresh_tool").click(function (event) {
                event.stopPropagation();
                cancel();
            })

        });
        function EventClick() {
            $(".check_increase").removeClass("check_increase_act");
            $("#over_hide1").show().siblings().hide();
            $(".check_decrease").show();
            $("#cage_cl").hide();
            $("#map_title_innerbox").hide();
            $("#cage_cl1").show();
            //        $("#over_hide").show();
            $("#map_title_innerbox1").show();
            $(".addition_check_in").hide();
            $("#car_check_in").hide();
            BootstrapTable();
        }
        function cancel(e) {
            $(".check_increase").removeClass("check_increase_act");
            $("#over_hide").show().siblings().hide();
            $(".check_decrease").show();
            $("#cage_cl").show();
            $("#cage_cl1").hide();
            //        $("#cage_cl").show();
            //        $("#over_hide1").show();
            //        $("#map_title_innerbox").show();
            BootstrapTable();
        }
        var OrderList, CreateDateTime = "0001/1/1 0:00:00.000", OrderNo = "";
        //查询订单列表
        function QueryBigDataStatisList() {
            $.ajax({
                async: false, cache: false, dataType: "json",
                url: "BigDataApi.aspx?method=QueryBigDataStatisList&CreateDateTime=" + CreateDateTime + "&OrderNo=" + OrderNo,
                success: function (text) {
                    var s = text;
                    if (s.length <= 0) {
                        //clearInterval(T);    //停止定时
                        return;
                    }
                    var str = "";
                    var orderID = 0;
                    for (var j = 0; j < s.length; j++) {
                        str += "<div class=\"message_scroll\"><div class=\"scroll_top\"><span class=\"scroll_title\">" + s[j].OrderNo + "</span><span class=\"scroll_level\">" + s[j].HouseName + "</span><a class=\"localize\"></a><span class=\"scroll_timer\">" + AllDateTime(s[j].CreateDateTime) + "</span></div><div class=\"msg_cage\"><a class=\"localize_title\">" + s[j].AcceptPeople + "</a></div><div class=\"msg_cage\"><a class=\"localize_msg\">" + s[j].Dep + "---" + s[j].Dest + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + s[j].Piece + "条&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + s[j].TotalCharge + "元</a></div></div>";
                    }

                    if (CreateDateTime == "0001/1/1 0:00:00.000") {
                        OrderList = str + OrderList;
                        $('#scrollOrder').html(OrderList);
                    } else {
                        $(".message_scroll_box").animate({ marginTop: 96 }, 800,
                                function () {
                                    $(".message_scroll_box").css({ marginTop: 0 });    //把顶部的边界清零
                                    $(".message_scroll_box .message_scroll:first").before(str);
                                    var childDiv = $("#scrollOrder").children("div").length;
                                    if (childDiv > 8) {
                                        var interval = setInterval(function () {
                                            $(".message_scroll_box .message_scroll:last").remove();
                                            clearInterval(interval);
                                        }, 1000);
                                    }
                                });
                    }
                    CreateDateTime = s[0].CreateDateTime;
                    OrderNo = s[0].OrderNo;
                }
            });

        }
        //实时统计查询
        function QueryCurBigDataStatis() {
            $.ajax({
                async: false, cache: false, dataType: "json",
                url: "BigDataApi.aspx?method=QueryCurBigDataStatis",
                success: function (text) {
                    $('#MonthOrderNum').html(text[0].MonthOrderNum);
                    $('#MonthSaleNum').html(text[0].MonthSaleNum);
                    $('#DayOrderNum').html(text[0].DayOrderNum);
                    $('#DaySaleNum').html(text[0].DaySaleNum);
                    switch (text[0].DaySaleMoney.toString().length) {
                        case 1:
                            $('#DaySaleMoney').html("<ul class=\"data_show_box\" style=\"width:100%;height: 65px;\"><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString() + "</li></ul>");
                            break;
                        case 2:
                            $('#DaySaleMoney').html("<ul class=\"data_show_box\" style=\"width:100%;height: 65px;\"><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(0, 1) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(1, 2) + "</li></ul>");
                            break;
                        case 3:
                            $('#DaySaleMoney').html("<ul class=\"data_show_box\" style=\"width:100%;height: 65px;\"><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(0, 1) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(1, 2) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(2, 3) + "</li></ul>");
                            break;
                        case 4:
                            $('#DaySaleMoney').html("<ul class=\"data_show_box\" style=\"width:100%;height: 65px;\"><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(0, 1) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(1, 2) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(2, 3) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(3, 4) + "</li></ul>");
                            break;
                        case 5:
                            $('#DaySaleMoney').html("<ul class=\"data_show_box\" style=\"width:100%;height: 65px;\"><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(0, 1) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(1, 2) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(2, 3) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(3, 4) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(4, 5) + "</li></ul>");
                            break;
                        case 6:
                            $('#DaySaleMoney').html("<ul class=\"data_show_box\" style=\"width:100%;height: 65px;\"><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(0, 1) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(1, 2) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(2, 3) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(3, 4) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(4, 5) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(5, 6) + "</li></ul>");
                            break;
                        case 7:
                            $('#DaySaleMoney').html("<ul class=\"data_show_box\" style=\"width:100%;height: 65px;\"><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(0, 1) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(1, 2) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(2, 3) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(3, 4) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(4, 5) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(5, 6) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(6, 7) + "</li></ul>");
                            break;
                        case 8:
                            $('#DaySaleMoney').html("<ul class=\"data_show_box\" style=\"width:100%;height: 65px;\"><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(0, 1) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(1, 2) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(2, 3) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(3, 4) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(4, 5) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(5, 6) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(6, 7) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].DaySaleMoney.toString().substring(7, 8) + "</li></ul>");
                            break;
                        default:
                    }
                    switch (text[0].MonthSaleMoney.toString().length) {
                        case 1:
                            $('#CurMonthSale').html("<ul class=\"data_show_box\" style=\"width:100%;height: 65px;\"><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString() + "</li></ul>");
                            break;
                        case 2:
                            $('#CurMonthSale').html("<ul class=\"data_show_box\" style=\"width:100%;height: 65px;\"><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(0, 1) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(1, 2) + "</li></ul>");
                            break;
                        case 3:
                            $('#CurMonthSale').html("<ul class=\"data_show_box\" style=\"width:100%;height: 65px;\"><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(0, 1) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(1, 2) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(2, 3) + "</li></ul>");
                            break;
                        case 4:
                            $('#CurMonthSale').html("<ul class=\"data_show_box\" style=\"width:100%;height: 65px;\"><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(0, 1) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(1, 2) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(2, 3) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(3, 4) + "</li></ul>");
                            break;
                        case 5:
                            $('#CurMonthSale').html("<ul class=\"data_show_box\" style=\"width:100%;height: 65px;\"><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(0, 1) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(1, 2) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(2, 3) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(3, 4) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(4, 5) + "</li></ul>");
                            break;
                        case 6:
                            $('#CurMonthSale').html("<ul class=\"data_show_box\" style=\"width:100%;height: 65px;\"><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(0, 1) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(1, 2) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(2, 3) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(3, 4) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(4, 5) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(5, 6) + "</li></ul>");
                            break;
                        case 7:
                            $('#CurMonthSale').html("<ul class=\"data_show_box\" style=\"width:100%;height: 65px;\"><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(0, 1) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(1, 2) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(2, 3) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(3, 4) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(4, 5) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(5, 6) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(6, 7) + "</li></ul>");
                            break;
                        case 8:
                            $('#CurMonthSale').html("<ul class=\"data_show_box\" style=\"width:100%;height: 65px;\"><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(0, 1) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(1, 2) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(2, 3) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(3, 4) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(4, 5) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(5, 6) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(6, 7) + "</li><li class=\"data_cage\" style=\"width:12%\">" + text[0].MonthSaleMoney.toString().substring(7, 8) + "</li></ul>");
                            break;
                        default:
                    }

                    //$('#CurMonthSale').html("<ul class=\"data_show_box\" style=\"width:100%;\"><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">0</li><li class=\"data_cage\" style=\"width:12%\">" + nm + "</li><li class=\"data_cage\" style=\"width:12%\">" + nm2 + "</li><li class=\"data_cage\" style=\"width:12%\">2</li></ul>");
                }
            });
        }
        //按品牌查询
        function QueryDataByTypeName() {
            $.ajax({
                async: false, cache: false, dataType: "json",
                url: "BigDataApi.aspx?method=QueryDataByTypeName",
                success: function (text) {
                    var s = text;
                    var str = "<table class=\"table table-bordered\"><thead><tr><th style=\"width: 30%;font-size:16px;\">品牌</th><th style=\"font-size:16px;\">当日销量</th><th style=\"font-size:16px;\">当月销量</th><th style=\"font-size:16px;\">当月销售额</th></tr></thead><tbody>";
                    for (var j = 0; j < s.length; j++) {
                        str += "<tr><td>" + s[j].TypeName + "</td><td>" + s[j].DaySaleNum + "</td><td>" + s[j].MonthSaleNum + "</td><td>" + s[j].MonthSaleMoney + "</td></tr>";
                    }
                    str += "</tbody></table>";
                    $('#typeName').html(str);
                }
            });
            //按业务员销量排名查询
            $.ajax({
                async: false, cache: false, dataType: "json",
                url: "BigDataApi.aspx?method=QueryDataBySaleManRank&ThrowGood=",
                success: function (text) {
                    var s = text;
                    var str = "<table class=\"table table-bordered\"><thead><tr><th style=\"width: 30%;font-size:16px;\">RE业务员</th><th style=\"font-size:16px;\">当日销量</th><th style=\"font-size:16px;\">当月销量</th><th style=\"font-size:16px;\">当月销售额</th></tr></thead><tbody>";
                    for (var j = 0; j < s.length; j++) {
                        str += "<tr><td>" + s[j].SaleManName + "</td><td>" + s[j].DaySaleNum + "</td><td>" + s[j].MonthSaleNum + "</td><td>" + s[j].MonthSaleMoney + "</td></tr>";
                    }
                    str += "</tbody></table>";
                    $('#saleManRank').html(str);
                }
            });
            //OE业务员销量排名
            $.ajax({
                async: false, cache: false, dataType: "json",
                url: "BigDataApi.aspx?method=QueryDataBySaleManRank&ThrowGood=12",
                success: function (text) {
                    var s = text;
                    var str = "<table class=\"table table-bordered\"><thead><tr><th style=\"width: 30%;font-size:16px;\">OE业务员</th><th style=\"font-size:16px;\">当日销量</th><th style=\"font-size:16px;\">当月销量</th><th style=\"font-size:16px;\">当月销售额</th></tr></thead><tbody>";
                    for (var j = 0; j < s.length; j++) {
                        str += "<tr><td>" + s[j].SaleManName + "</td><td>" + s[j].DaySaleNum + "</td><td>" + s[j].MonthSaleNum + "</td><td>" + s[j].MonthSaleMoney + "</td></tr>";
                    }
                    str += "</tbody></table>";
                    $('#OEsaleManRank').html(str);
                }
            });

        }

        //获取云仓地址
        var HouseList = [];
            $.ajax({
                async: false, cache: false, dataType: "json",
                url: "BigDataApi.aspx?method=BigDataQueryCargoHouse",
                success: function (text) {
                    // 清空HouseList数组
                    HouseList.length = 0;
                    // 将text数组的元素添加到HouseList数组中
                    Array.prototype.push.apply(HouseList, text);
                }
            });
        
        console.log(HouseList, "仓库信息");


        //仓库总的实时统计
        function QueryALLBigOrderSum() {
            $.ajax({
                async: false, cache: false, dataType: "json",
                url: "BigDataApi.aspx?method=QueryALLBigOrderSum",
                success: function (text) {
                    for (var i = 0; i < text.length; i++) {

                        var matchingHouses = HouseList.filter(function (house) {
                            return house.Name === text[i].HouseName;
                        });

                        if (matchingHouses.length == 0 ) continue;
                        if (marker.getGeometryById(matchingHouses[0].HouseCode) == null) continue;
                       
                        marker.updateGeometries([
                            {
                                styleId: "marker",
                                id: matchingHouses[0].HouseCode,
                                content: matchingHouses[0].Name + "  " + text[i].SaleNum.toString(),
                                position: new TMap.LatLng(matchingHouses[0].Lat, matchingHouses[0].Lng),
                            }
                        ]);
                    }
                }
            });
        }

        function BootstrapTable() {
            $('#table').bootstrapTable({
                method: "get", striped: true, singleSelect: false,
                dataType: "json", pagination: true, //分页
                pageSize: number, pageNumber: 1, search: false, //显示搜索框
                url: "BigDataApi.aspx?method=QueryALLBigDataStatis",
                contentType: "application/x-www-form-urlencoded",
                queryParams: null,
                columns: [
                    {
                        title: '仓库名称', field: 'HouseName', width: '20%', align: 'center', valign: 'middle'
                    },
                    {
                        title: '订单数', field: 'OrderNum', width: '20%', align: 'center', valign: 'middle'
                    },

                    {
                        title: '销售量', field: 'SaleNum', width: '20%', align: 'center'
                    },
                    {
                        title: '销售金额', field: 'SaleMoney', align: 'center'
                    }
                ]
            });
        }
        var categories = [];
        //var orderList = [];
        var numList = [];
        //var moneyList = [];
        //查询仓库销售数据饼图
        function QueryCargoSaleNumPie() {
            //setChart();
            $.ajax({
                async: false, cache: false, dataType: "json",
                url: "BigDataApi.aspx?method=QueryALLBigDataStatis",
                success: function (text) {
                    for (var i = 0; i < text.length; i++) {
                        if (text[i].HouseName == "湖南仓库" || text[i].HouseName == "湖北仓库" || text[i].HouseName == "广州仓库" || text[i].HouseName == "东莞仓库" || text[i].HouseName == "西安迪乐泰" || text[i].HouseName == "揭阳主仓库" || text[i].HouseName == "四川仓库") {
                            categories.push(text[i].HouseName);
                            // orderList.push(data.rows[i].OrderNum);
                            //numList.push(text[i].SaleNum);
                            numList.push({
                                name: text[i].HouseName,
                                value: text[i].SaleNum
                            });
                        }
                        //moneyList.push(data.rows[i].TotalCharge);
                    }
                    //charTitle = data.rows[0].HouseName + "销售统计表";
                    //categories.reverse();
                    //orderList.reverse();
                    // numList.reverse();
                    //moneyList.reverse();
                    // Echarts();
                    setChart();
                }
            });
        }
        function setChart() {
            require(
        [
            'echarts',
            'echarts/chart/pie'
        ],
        function (ec) {
            var myChart = ec.init(document.getElementById('container_huan'));
            //图表显示提示信息
            //myChart.showLoading({ text: "图表数据正在努力加载中..." });
            var option = {
                tooltip: { trigger: 'item', formatter: '{a} <br/>{b} : {c}' },
                legend: { show: false, type: 'scroll', orient: 'horizontal', right: 0, top: 0, bottom: 0, data: categories },
                series: [
                    {
                        name: '仓库销量', type: 'pie', minShowLabelAngle: 20, radius: '55%', center: ['50%', '45%'], data: numList,
                        emphasis: {
                            itemStyle: {
                                shadowBlur: 10,
                                shadowOffsetX: 0,
                                shadowColor: 'rgba(0, 0, 0, 0.5)'
                            }
                        }
                    }
                ]
            };
            myChart.setOption(option);
        })
        }

        function TimeControl() {
            $(".message_scroll_box").animate({ marginTop: 96 }, 800,
                    function () {
                        $(".message_scroll_box").css({ marginTop: 0 });    //把顶部的边界清零
                        $(".message_scroll_box .message_scroll:first").before($(".message_scroll_box .message_scroll:last"));    //在第一个新闻后面插入最后一个新闻
                    });
        }

        window.setInterval(function () {
            QueryCurBigDataStatis();//实时统计查询
            QueryDataByTypeName(); //按品牌查询
            QueryALLBigOrderSum();//仓库总的实时统计
        }, 10000);

        window.setInterval(function () { $('#table').bootstrapTable('refresh'); }, 300000)
        //window.setInterval(function () { $('#table').bootstrapTable('refresh'); }, 10000)
        window.setInterval(function () { QueryBigDataStatisList(); }, 10000)
    </script>

    <!--轨迹回放时间日期选择-->
    <script>
        !function () {
            //QueryCargoSaleNumPie();
            OID = 0;
            QueryBigDataStatisList();
        }();
    </script>
    <script>
        $('#map').append('<input type="button" id="restore" onclick="restore()" value="复位" style="right: 31px;height: 33px;width: 39px;top: 178px;z-index: 10000;background-color: #ffffff;border: none;border-radius: 3px;outline: none;position:absolute" />');
        //定义地图中心点坐标
        var center = new TMap.LatLng(35.42, 107.40)
        //定义map变量，调用 TMap.Map() 构造函数创建地图
        var map = new TMap.Map(document.getElementById('map'), {
            //center: center,//设置地图中心点坐标
            mapStyleId: 'style2',
            zoom: 4.4,   //设置地图缩放级别
            pitch: 0,  //设置俯仰角
            rotation: 0    //设置地图旋转角度
        });

        //获取云仓地址
        var geometries = [];
        var markerArr = [];
        var cluster = [];
        $.ajax({
            async: false, cache: false, dataType: "json",
            url: "BigDataApi.aspx?method=BigDataQueryCargoHouse",
            success: function (text) {
                // 清空HouseList数组
                geometries.length = 0;
                markerArr.length = 0;
                for (var i = 0; i < text.length; i++) {
                    markerArr.push({
                        position: new TMap.LatLng(text[i].Lat, text[i].Lng),
                        styleId: 'marker',
                        id: text[i].HouseCode,
                        content: text[i].Name,
                        properties: { //标注点的属性数据
                            title: 'label'
                        }
                    });
                    cluster.push({
                        position: new TMap.LatLng(text[i].Lat, text[i].Lng),
                        styleId: 'marker',
                        id: text[i].HouseCode,
                        content: text[i].Name,
                        properties: { //标注点的属性数据
                            title: 'label'
                        }
                    });
                }
            }
        });
       

        // 创建点聚合
        var markerCluster = new TMap.MarkerCluster({
            id: 'cluster',
            map: map,
            enableDefaultStyle: false, // 关闭默认样式
            minimumClusterSize: 3,
            geometries: markerArr,
            zoomOnClick: true,
            gridSize: 60,
            averageCenter: false
        });

        var clusterBubbleList = [];
        var markerGeometries = [];
        var marker = null;

        // 监听聚合簇变化
        markerCluster.on('cluster_changed', function (e) {
            // 销毁旧聚合簇生成的覆盖物
            if (clusterBubbleList.length) {
                clusterBubbleList.forEach(function (item) {
                    item.destroy();
                })
                clusterBubbleList = [];
            }
            markerGeometries = [];

            // 根据新的聚合簇数组生成新的覆盖物和点标记图层
            var clusters = markerCluster.getClusters();
            clusters.forEach(function (item) {
                if (item.geometries.length > 1) {
                    let clusterBubble = new ClusterBubble({
                        map,
                        position: item.center,
                        content: item.geometries.length,
                    });
                    clusterBubble.on('click', () => {
                        map.fitBounds(item.bounds);
                    });
                    clusterBubbleList.push(clusterBubble);
                } else {
                    markerGeometries.push({
                        styleId: 'marker',
                        id: item.geometries[0].id,
                        position: item.center,
                        content: item.geometries[0].content,
                    });
                }
            });

            if (marker) {
                // 已创建过点标记图层，直接更新数据
                marker.setGeometries(markerGeometries);
            } else {
                // 创建点标记图层
                marker = new TMap.MultiMarker({
                    map,
                    styles: {
                        // 点标记样式
                        marker: new TMap.MarkerStyle({
                            width: 15, // 样式宽
                            height: 15, // 样式高
                            color: '#FFA500', // 标注点文本颜色#FFA500
                            size: 15, // 标注点文本文字大小direction: 'bottom', // 标注点文本文字相对于标注点图片的方位
                            offset: { x: 0, y: -20 }, // 标注点文本文字基于direction方位的偏移属性
                            anchor: { x: 10, y: 30 }, // 描点位置
                            //enableRelativeScale: true,
                            angle: 0, //文字旋转属性
                            alignment: 'center', //文字水平对齐属性
                            verticalAlignment: 'middle',//文字垂直对齐属性
                            src:'/upload/marker.png'
                            //src: 'https://mapapi.qq.com/web/lbs/javascriptGL/demo/img/markerDefault.png', // 标记路径
                        }),
                    },
                    geometries: markerGeometries
                });
            }
        });


        // 以下代码为基于DOMOverlay实现聚合点气泡
        function ClusterBubble(options) {
            TMap.DOMOverlay.call(this, options);
        }

        ClusterBubble.prototype = new TMap.DOMOverlay();

        ClusterBubble.prototype.onInit = function (options) {
            this.content = options.content;
            this.position = options.position;
        };

        // 销毁时需要删除监听器
        ClusterBubble.prototype.onDestroy = function () {
            this.dom.removeEventListener('click', this.onClick);
            this.removeAllListeners();
        };

        ClusterBubble.prototype.onClick = function () {
            this.emit('click');
        };

        // 创建气泡DOM元素
        ClusterBubble.prototype.createDOM = function () {
            var dom = document.createElement('div');
            dom.classList.add('clusterBubble');
            dom.innerText = this.content;
            dom.style.cssText = [
                'width: ' + (40 + parseInt(this.content) * 2) + 'px;',
                'height: ' + (40 + parseInt(this.content) * 2) + 'px;',
                'line-height: ' + (40 + parseInt(this.content) * 2) + 'px;',
            ].join(' ');

            // 监听点击事件，实现zoomOnClick
            this.onClick = this.onClick.bind(this);
            // pc端注册click事件，移动端注册touchend事件
            dom.addEventListener('click', this.onClick);
            return dom;
        };

        ClusterBubble.prototype.updateDOM = function () {
            if (!this.map) {
                return;
            }
            // 经纬度坐标转容器像素坐标
            let pixel = this.map.projectToContainer(this.position);

            // 使文本框中心点对齐经纬度坐标点
            let left = pixel.getX() - this.dom.clientWidth / 2 + 'px';
            let top = pixel.getY() - this.dom.clientHeight / 2 + 'px';
            this.dom.style.transform = `translate(${left}, ${top})`;

            this.emit('dom_updated');
        };

        window.ClusterBubble = ClusterBubble;

        //var markerCluster = new TMap.MarkerCluster({
        //    id: 'cluster',
        //    map: map,
        //    enableDefaultStyle: true, // 启用默认样式
        //    minimumClusterSize: 2, // 形成聚合簇的最小个数
        //    geometries: cluster,
        //    zoomOnClick: true, // 点击簇时放大至簇内点分离
        //    gridSize: 60, // 聚合算法的可聚合距离
        //    averageCenter: false, // 每个聚和簇的中心是否应该是聚类中所有标记的平均值
        //    maxZoom: 10, // 采用聚合策略的最大缩放级别
        //    //enableDefaultStyle: false,
        //    styles: {
        //        // 点标记样式
        //        marker: new TMap.MarkerStyle({
        //            width: 20, // 样式宽
        //            height: 30, // 样式高
        //            color: '#FF6347', // 标注点文本颜色#FFA500
        //            size: 15, // 标注点文本文字大小direction: 'bottom', // 标注点文本文字相对于标注点图片的方位
        //            offset: { x: 0, y: -40 }, // 标注点文本文字基于direction方位的偏移属性
        //            anchor: { x: 10, y: 30 }, // 描点位置
        //            //enableRelativeScale: true,
        //            angle: 0, //文字旋转属性
        //            alignment: 'center', //文字水平对齐属性
        //            verticalAlignment: 'middle',//文字垂直对齐属性
        //            //src:'/upload/marker.png'
        //            //src: 'https://mapapi.qq.com/web/lbs/javascriptGL/demo/img/markerDefault.png', // 标记路径
        //        }),
        //    }
        //});
    



        restore();
        function restore() {
            var bounds = new TMap.LatLngBounds();

            //判断标注点是否在范围内
            markerArr.forEach(function (item) {
                //若坐标点不在范围内，扩大bounds范围
                if (bounds.isEmpty() || !bounds.contains(item.position)) {
                    bounds.extend(item.position);
                }
            })
            //设置地图可视范围
            map.fitBounds(bounds, {
                padding: 70 // 自适应边距
            });
        }
    </script>

    

</body>
</html>
