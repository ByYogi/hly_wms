<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="category.aspx.cs" Inherits="Cargo.Weixin.category" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>产品分类</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1,maximum-scale=1,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="../JS/MUI/css/mui.min.css" rel="stylesheet" />
    <link href="../JS/MUI/css/app.css" rel="stylesheet" />
    <style>
        .mui-row.mui-fullscreen > [class*="mui-col-"] {
            height: 100%;
        }

        .mui-col-xs-3,
        .mui-control-content {
            overflow-y: auto;
            height: 100%;
        }

        .mui-segmented-control .mui-control-item {
            line-height: 50px;
            width: 100%;
        }

        .mui-segmented-control.mui-segmented-control-inverted .mui-control-item.mui-active {
            background-color: #fff;
        }

        .class-ui-ad {
            margin: 10px 10px 0px 10px;
        }

            .class-ui-ad a img {
                width: 100%;
                height: 100%;
                display: block;
                border: none;
            }

        .class-ui-title {
            text-align: center;
            padding: 14px 0;
        }

            .class-ui-title h3 {
                display: inline-block;
                position: relative;
                height: 12px;
                line-height: 12px;
                font-size: 12px;
                vertical-align: middle;
                padding: 0 .92rem;
                color: #f23030;
            }

                .class-ui-title h3:before {
                    position: absolute;
                    top: 5px;
                    content: '';
                    width: .72rem;
                    height: 1px;
                    background: #ccc;
                    left: 0;
                }

                .class-ui-title h3:after {
                    position: absolute;
                    top: 5px;
                    content: '';
                    width: .72rem;
                    height: 1px;
                    background: #ccc;
                    right: 0;
                }



        /*æ …æ ¼ç±»*/
        .aui-row {
            overflow: hidden;
            margin: 0;
        }

        .aui-row-padded {
            margin-left: -0.125rem;
            margin-right: -0.125rem;
        }

            .aui-row-padded [class*=aui-col-xs-] {
                padding: 0.125rem;
            }

        .aui-col-xs-1, .aui-col-xs-2, .aui-col-xs-3, .aui-col-xs-4, .aui-col-xs-5, .aui-col-xs-6, .aui-col-xs-7, .aui-col-xs-8, .aui-col-xs-9, .aui-col-xs-10, .aui-col-xs-11, .aui-col-5 {
            position: relative;
            float: left;
        }

        .aui-col-xs-12 {
            width: 100%;
            position: relative;
        }

        .aui-col-xs-11 {
            width: 91.66666667%;
        }

        .aui-col-xs-10 {
            width: 83.33333333%;
        }

        .aui-col-xs-9 {
            width: 75%;
        }

        .aui-col-xs-8 {
            width: 66.66666667%;
        }

        .aui-col-xs-7 {
            width: 58.33333333%;
        }

        .aui-col-xs-6 {
            width: 50%;
        }

        .aui-col-xs-5 {
            width: 41.66666667%;
        }

        .aui-col-xs-4 {
            width: 33.33333333%;
            text-align: center;
        }

        .aui-col-xs-3 {
            width: 25%;
        }

        .aui-col-xs-2 {
            width: 16.66666667%;
        }

        .aui-col-xs-1 {
            width: 8.33333333%;
        }

        .aui-col-5 {
            width: 20%;
        }

        .class-ui-text .aui-col-xs-4 a img {
            width: 2.6rem;
            height: 2.6rem;
            margin: 0 auto;
        }

        .class-ui-text .aui-col-xs-4 a p {
            text-align: center;
            color: #999;
            font-size: 12px;
        }
        /*----------------mui.showLoading---------------*/
        .mui-show-loading {
            position: fixed;
            padding: 5px;
            width: 120px;
            min-height: 120px;
            top: 45%;
            left: 50%;
            margin-left: -60px;
            background: rgba(0, 0, 0, 0.6);
            text-align: center;
            border-radius: 5px;
            color: #FFFFFF;
            visibility: hidden;
            margin: 0;
            z-index: 2000;
            -webkit-transition-duration: .2s;
            transition-duration: .2s;
            opacity: 0;
            -webkit-transform: scale(0.9) translate(-50%, -50%);
            transform: scale(0.9) translate(-50%, -50%);
            -webkit-transform-origin: 0 0;
            transform-origin: 0 0;
        }

            .mui-show-loading.loading-visible {
                opacity: 1;
                visibility: visible;
                -webkit-transform: scale(1) translate(-50%, -50%);
                transform: scale(1) translate(-50%, -50%);
            }

            .mui-show-loading .mui-spinner {
                margin-top: 24px;
                width: 36px;
                height: 36px;
            }

            .mui-show-loading .text {
                line-height: 1.6;
                font-family: -apple-system-font,"Helvetica Neue",sans-serif;
                font-size: 14px;
                margin: 10px 0 0;
                color: #fff;
            }

        .mui-show-loading-mask {
            position: fixed;
            z-index: 1000;
            top: 0;
            right: 0;
            left: 0;
            bottom: 0;
        }

        .mui-show-loading-mask-hidden {
            display: none !important;
        }
    </style>
</head>
<body>
    <input style="display: none;" id="ProductTypeID" value="<%=ProductTypeID %>" />
    <div class="mui-content mui-row mui-fullscreen" style="font-size: 14px;">
        <div class="weui-search-bar">
            <a href="javascript:window.history.back(-1); ">
                <img src="WeUI/image/icon-back.png" style="width: 25px; height: 25px; margin-bottom: -5px;" /></a>
            <input type="search" style="box-sizing: content-box; width: 80%; line-height: 2.1; display: inline-block; border: 0; -webkit-appearance: none; appearance: none; border-radius: 10px; font-family: inherit; color: #000000; font-size: 16px; font-weight: normal; padding: 0 2px; margin-bottom: 2px; background-color: #fff; border: 1px solid #2dd1e6; opacity: .9;"
                id='searchInput' placeholder='请输入轮胎规格 例：2055516' onkeyup="enterSearch(event)" />&nbsp;
          <img src="WeUI/image/icon-search.png" style="width: 25px; height: 25px; margin-bottom: -5px;" onclick="QueryPro()" />
        </div>
        <div class="mui-col-xs-2">
            <div id="segmentedControls" class="mui-segmented-control mui-segmented-control-inverted mui-segmented-control-vertical" style="font-size: 12px;">
                <%-- <a class="mui-control-item mui-active" href="#item1" style="line-height: 40px;">热门推荐</a>--%>
                <a class="mui-control-item" id="hf1" href="#item1" style="line-height: 40px;">优科豪马</a>
                <a class="mui-control-item" id="hf4" href="#item4" style="line-height: 40px;">马牌轮胎</a>
                <a class="mui-control-item" id="hf9" href="#item9" style="line-height: 40px;">达迈轮胎</a>
                <a class="mui-control-item" id="hf10" href="#item10" style="line-height: 40px;">沃凯途</a>
                <a class="mui-control-item" id="hf2" href="#item2" style="line-height: 40px;">普利司通</a>
                <a class="mui-control-item" id="hf3" href="#item3" style="line-height: 40px;">万力轮胎</a>
                <a class="mui-control-item" id="hf5" href="#item5" style="line-height: 40px;">美国固铂</a>
                <a class="mui-control-item" id="hf6" href="#item6" style="line-height: 40px;">韩泰轮胎</a>
                <a class="mui-control-item" id="hf7" href="#item7" style="line-height: 40px;">佳通轮胎</a>
                <a class="mui-control-item" id="hf8" href="#item8" style="line-height: 40px;">三角轮胎</a>
            </div>
        </div>
        <div id="segmentedControlContents" class="mui-col-xs-10" style="border-left: 1px solid #c8c7cc; background-color: white;">
            <div id="item1" class="mui-control-content">
                <ul class="mui-table-view" id="ul1">
                </ul>
            </div>
            <div id="item2" class="mui-control-content">
                <ul class="mui-table-view" id="ul2">
                </ul>
            </div>
            <div id="item3" class="mui-control-content">
                <ul class="mui-table-view" id="ul3">
                </ul>
            </div>
            <div id="item4" class="mui-control-content">
                <ul class="mui-table-view" id="ul4">
                </ul>
            </div>
            <div id="item5" class="mui-control-content">
                <ul class="mui-table-view" id="ul5">
                </ul>
            </div>
            <div id="item6" class="mui-control-content">
                <ul class="mui-table-view" id="ul6">
                </ul>
            </div>
            <div id="item7" class="mui-control-content">
                <ul class="mui-table-view" id="ul7">
                </ul>
            </div>
            <div id="item8" class="mui-control-content">
                <ul class="mui-table-view" id="ul8">
                </ul>
            </div>
            <div id="item9" class="mui-control-content">
                <ul class="mui-table-view" id="ul9">
                </ul>
            </div>
            <div id="item10" class="mui-control-content">
                <ul class="mui-table-view" id="ul10">
                </ul>
            </div>
        </div>
    </div>

    <script src=" ../JS/MUI/js/mui.js"></script>
    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/fastclick.js"></script>
    <script src="../JS/MUI/js/showLoading.js"></script>
    <script type="text/javascript">

        mui('body').on('tap', 'a', function () {
            var pid = this.id;
            if (pid == "hf1") {
                $('#ProductTypeID').val(9);
                QueryPro();
            } else if (pid == "hf2") {
                $('#ProductTypeID').val(18);
                QueryPro();
            }
            else if (pid == "hf3") {
                $('#ProductTypeID').val(27);
                QueryPro();
            }
            else if (pid == "hf4") {
                $('#ProductTypeID').val(34);
                QueryPro();
            }
            else if (pid == "hf5") {
                $('#ProductTypeID').val(66);
                QueryPro();
            }
            else if (pid == "hf6") {
                $('#ProductTypeID').val(31);
                QueryPro();
            }
            else if (pid == "hf7") {
                $('#ProductTypeID').val(157);
                QueryPro();
            }
            else if (pid == "hf8") {
                $('#ProductTypeID').val(164);
                QueryPro();
            }
            else if (pid == "hf9") {
                $('#ProductTypeID').val(171);
                QueryPro();
            }
            else if (pid == "hf10") {
                $('#ProductTypeID').val(180);
                QueryPro();
            }
            else {
                location.href = this;
            }
        });
        mui.init({
            swipeBack: true //启用右滑关闭功能
        });
        $(function () {
            var s = $('#ProductTypeID').val();

            if (s == 9) {
                //$('#item1').css("mui-control-content mui-active");
                document.getElementById("item1").className = "mui-control-content mui-active";
                document.getElementById("hf1").className = "mui-control-item mui-active";
            } else if (s == 18) {
                document.getElementById("item2").className = "mui-control-content mui-active";
                document.getElementById("hf2").className = "mui-control-item mui-active";
                //$('#item2').css("mui-control-content mui-active");
            } else if (s == 27) {
                document.getElementById("item3").className = "mui-control-content mui-active";
                document.getElementById("hf3").className = "mui-control-item mui-active";
                //$('#item3').css("mui-control-content mui-active");
            } else if (s == 34) {
                document.getElementById("item4").className = "mui-control-content mui-active";
                document.getElementById("hf4").className = "mui-control-item mui-active";
                //$('#item3').css("mui-control-content mui-active");
            } else if (s == 66) {
                document.getElementById("item5").className = "mui-control-content mui-active";
                document.getElementById("hf5").className = "mui-control-item mui-active";
                //$('#item3').css("mui-control-content mui-active");
            } else if (s == 31) {
                document.getElementById("item6").className = "mui-control-content mui-active";
                document.getElementById("hf6").className = "mui-control-item mui-active";
                //$('#item3').css("mui-control-content mui-active");
            } else if (s == 157) {
                document.getElementById("item7").className = "mui-control-content mui-active";
                document.getElementById("hf7").className = "mui-control-item mui-active";
                //$('#item3').css("mui-control-content mui-active");
            } else if (s == 164) {
                document.getElementById("item8").className = "mui-control-content mui-active";
                document.getElementById("hf8").className = "mui-control-item mui-active";
                //$('#item3').css("mui-control-content mui-active");
            } else if (s == 171) {
                document.getElementById("item9").className = "mui-control-content mui-active";
                document.getElementById("hf9").className = "mui-control-item mui-active";
                //$('#item3').css("mui-control-content mui-active");
            } else if (s == 180) {
                document.getElementById("item10").className = "mui-control-content mui-active";
                document.getElementById("hf10").className = "mui-control-item mui-active";
                //$('#item3').css("mui-control-content mui-active");
            } else {
                document.getElementById("item1").className = "mui-control-content mui-active";
                document.getElementById("hf1").className = "mui-control-item mui-active";
                $('#ProductTypeID').val(9);
            }
            var searchKey = '<%=searchText%>';
            $('#searchInput').val(searchKey);
            var ProductTypeID = $('#ProductTypeID').val();
            //$('.mui-control-content').removeClass("mui-active");
            //$('.mui-control-item').removeClass("mui-active");
            // $('#item6').addClass('mui-active');
            mui.showLoading("数据加载中..", "div"); //加载文字和类型，plus环境中类型为div时强制以div方式显示  
            $.ajax({
                url: 'myAPI.aspx?method=QueryTypeStock',
                cache: false, dataType: 'json', data: { key: searchKey, ProductTypeID: ProductTypeID },
                success: function (text) {
                    mui.hideLoading();//隐藏后的回调函数  
                    var s = text;
                    var str = "";
                    for (var j = 0; j < s.length; j++) {
                        if (s[j].SaleType == "1") { continue; }
                        var figure = '';
                        if (s[j].TypeID == 19 && s[j].Figure.length > 5) {
                            figure = s[j].Figure.substr(s[j].Figure.length - 5, 5);
                        } else { figure = s[j].Figure; }
                        var assort = '';
                        if (s[j].Assort.length > 2) {
                            assort = s[j].Assort.substr(0, 2);
                        }
                        if (s[j].TypeID == 9) {
                            str += "<li class=\"mui-table-view-cell\" style=\"padding: 9px 10px;border-radius: 5px;border-bottom: 1px solid #2dd1e6;\"><a class=\"mui-navigate-right\" href=\"productInfo.aspx?ID=" + s[j].OnSaleID + "\">" + s[j].Specs + "--" + figure + "--" + s[j].Batch + "年" + "--" + assort + "--";
                        } else {
                            str += "<li class=\"mui-table-view-cell\" style=\"padding: 9px 10px;border-radius: 5px;border-bottom: 1px solid #2dd1e6;\"><a class=\"mui-navigate-right\" href=\"productInfo.aspx?ID=" + s[j].OnSaleID + "\">" + s[j].Specs + "--" + figure + "--" + s[j].LoadIndex + s[j].SpeedLevel + "--" + s[j].Batch + "年--";
                        }

                        if (s[j].SaleType == "3" || s[j].SaleType == "1") {
                            str += "<em style='color:red;font-weight:bold;'>" + s[j].SalePrice + "元【特价】</em>";
                        } else {
                            str += "<em style='color:red;font-weight:bold;'>" + s[j].SalePrice + "元</em>";
                        }

                        str += "</a></li>";
                    }
                    if (ProductTypeID == 9) {
                        $('#ul1').html(str);
                    } else if (ProductTypeID == 18)
                    { $('#ul2').html(str); }
                    else if (ProductTypeID == 27)
                    { $('#ul3').html(str); }
                    else if (ProductTypeID == 34)
                    { $('#ul4').html(str); }
                    else if (ProductTypeID == 66)
                    { $('#ul5').html(str); }
                    else if (ProductTypeID == 31)
                    { $('#ul6').html(str); }
                    else if (ProductTypeID == 157)
                    { $('#ul7').html(str); }
                    else if (ProductTypeID == 164)
                    { $('#ul8').html(str); }
                    else if (ProductTypeID == 171)
                    { $('#ul9').html(str); }
                    else if (ProductTypeID == 180)
                    { $('#ul10').html(str); }
                }
            });
        })
        function enterSearch(e) {
            if (e.keyCode == 13) {
                QueryPro();
            }
        }
        function QueryPro() {
            var s = $('#ProductTypeID').val();
            mui.showLoading("数据加载中..", "div"); //加载文字和类型，plus环境中类型为div时强制以div方式显示  
            var searchKey = $('#searchInput').val();
            //$('.mui-control-content').removeClass("mui-active");
            //$('.mui-control-item').removeClass("mui-active");
            // $('#item6').addClass('mui-active');
            var ProductTypeID = $('#ProductTypeID').val();
            $.ajax({
                url: 'myAPI.aspx?method=QueryTypeStock',
                cache: false, dataType: 'json', data: { key: searchKey, ProductTypeID: ProductTypeID },
                success: function (text) {
                    mui.hideLoading();//隐藏后的回调函数  
                    var s = text;
                    var str = "";
                    for (var j = 0; j < s.length; j++) {
                        if (s[j].SaleType == "1") { continue; }
                        // str += "<div class=\"aui-col-xs-4\"><a href=\"productInfo.aspx?ID=" + s[j].OnSaleID + "\"><img src=\" " + s[j].FileName + "\" /><p>" + s[j].Specs + " " + s[j].LoadIndex + s[j].SpeedLevel + " " + s[j].Figure + "</p></a></div>";
                        //<span class=\"mui-badge mui-badge-danger\">热门</span>
                        var figure = '';
                        if (s[j].TypeID == 19 && s[j].Figure.length > 5) {
                            figure = s[j].Figure.substr(s[j].Figure.length - 5, 5);
                        } else { figure = s[j].Figure; }
                        var assort = '';
                        if (s[j].Assort.length > 2) {
                            assort = s[j].Assort.substr(0, 2);
                        }
                        if (s[j].TypeID == 9) {
                            str += "<li class=\"mui-table-view-cell\" style=\"padding: 9px 10px;border-radius: 5px;border-bottom: 1px solid #2dd1e6;\"><a class=\"mui-navigate-right\" href=\"productInfo.aspx?ID=" + s[j].OnSaleID + "\">" + s[j].Specs + "--" + figure + "--" + s[j].Batch + "年" + "--" + assort + "--";
                        } else {
                            str += "<li class=\"mui-table-view-cell\" style=\"padding: 9px 10px;border-radius: 5px;border-bottom: 1px solid #2dd1e6;\"><a class=\"mui-navigate-right\" href=\"productInfo.aspx?ID=" + s[j].OnSaleID + "\">" + s[j].Specs + "--" + figure + "--" + s[j].LoadIndex + s[j].SpeedLevel + "--" + s[j].Batch + "年--";
                        }
                        if (s[j].HouseID == 12) {
                            //梅州揭阳仓库
                            str += "<em style='color:red;font-weight:bold;'>" + s[j].SalePrice + "元</em>";
                        } else {
                            if (s[j].SaleType == "3") {
                                str += "<em style='color:red;font-weight:bold;'>" + s[j].SalePrice + "元【特价】</em>";
                            } else {
                                str += "<em style='color:red;font-weight:bold;'>" + s[j].SalePrice + "元</em>";
                            }
                        }
                        str += "</a></li>";
                    }
                    if (ProductTypeID == 9) {
                        $('#ul1').html(str);
                    } else if (ProductTypeID == 18)
                    { $('#ul2').html(str); }
                    else if (ProductTypeID == 27)
                    { $('#ul3').html(str); }
                    else if (ProductTypeID == 34)
                    { $('#ul4').html(str); }
                    else if (ProductTypeID == 66)
                    { $('#ul5').html(str); }
                    else if (ProductTypeID == 31)
                    { $('#ul6').html(str); }
                    else if (ProductTypeID == 157)
                    { $('#ul7').html(str); }
                    else if (ProductTypeID == 164)
                    { $('#ul8').html(str); }
                    else if (ProductTypeID == 171)
                    { $('#ul9').html(str); }
                    else if (ProductTypeID == 180)
                    { $('#ul10').html(str); }
                }
            });
        }
    </script>
</body>
</html>
