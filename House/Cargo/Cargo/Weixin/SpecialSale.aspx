<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpecialSale.aspx.cs" Inherits="Cargo.Weixin.SpecialSale" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>特价轮胎搭配销售</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌 固铂" />
    <link href="../JS/MUI/css/mui.min.css" rel="stylesheet" />
    <link href="../JS/MUI/css/icons-extra.css" rel="stylesheet" />
    <link href="../JS/MUI/css/app.css" rel="stylesheet" />
    <script src="../JS/MUI/js/mui.js"></script>
    <script src="../JS/easy/js/jquery.min.js" type="text/javascript"></script>
    <link href="../JS/MUI/css/mui.picker.css" rel="stylesheet" />
    <link href="../JS/MUI/css/mui.poppicker.css" rel="stylesheet" />
    <script src="../JS/MUI/js/showLoading.js"></script>

    <style type="text/css">
        .mui-btn {
            font-size: 16px;
            padding: 8px;
            margin: 3px;
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

        .mui-switch:before {
            font-size: 13px;
            position: absolute;
            top: 3px;
            right: 11px;
            content: attr(data-off);
            text-transform: uppercase;
            color: #999;
        }

        .mui-switch.mui-active:before {
            right: auto;
            left: 15px;
            content: attr(data-on);
            color: #fff;
        }
    </style>
</head>

<body ontouchstart>
    <div id="offCanvasWrapper" class="mui-off-canvas-wrap mui-draggable">
        <!--侧滑菜单部分-->
        <aside id="offCanvasSide" class="mui-off-canvas-left">
            <div id="offCanvasSideScroll" class="mui-scroll-wrapper">
                <div class="mui-scroll">

                    <input type="text" id="Specs" style="border-radius: 5px; font-family: inherit; color: #000000; font-size: 16px; font-weight: normal; background-color: #fff; border: 1px solid #000000; width: 100%; text-align: center; margin-top: 5px; margin-left: 1px; margin-right: 5px;" placeholder="请输入轮胎规格 例：2055516 " />

                    <div style="color: #fff; font-size: 14px;">
                        <div style="float: left; margin-right: 10px;">
                            <input id="Ykhm" type="checkbox" />
                            <label for="Ykhm">优科豪马</label>
                        </div>
                        <div style="float: left; margin-right: 10px;">
                            <input id="Plst" type="checkbox" />
                            <label for="Plst">普利司通</label>
                        </div>
                        <div style="float: left; margin-right: 10px;">
                            <input id="Wdbt" type="checkbox" />
                            <label for="Wdbt">万达宝通</label>
                        </div>
                        <div style="float: left; margin-right: 10px;">
                            <input id="Dgmp" type="checkbox" />
                            <label for="Dgmp">德国马牌</label>
                        </div>
                        <div style="float: left; margin-right: 10px;">
                            <input id="Mggb" type="checkbox" />
                            <label for="Mggb">美国固铂</label>
                        </div>
                    </div>
                    <button id="offCanvasHide" type="button" class="mui-btn mui-btn-danger mui-btn-block">确定</button>

                </div>
            </div>
        </aside>

        <div class="mui-inner-wrap">
            <header class="mui-bar mui-bar-nav" style="background: linear-gradient(to bottom, #fff 0%,#efefef 100%);">
                <a href="#offCanvasSide">
                    <img src="WeUI/image/icon_select.png" style="margin-top: 5px; margin-left: -5px; margin-right: 2px;" /></a>
                <h1 class="mui-title" style="color: black;">
                    <asp:Literal ID="ltlSpecial" runat="server"></asp:Literal></h1>

            </header>
            <div id="offCanvasContentScroll" class="mui-content mui-scroll-wrapper" style="background-color: white;">
                <div class="mui-scroll">
                    <div class="mui-content-padded" style="margin: 0px 0px 50px 0px;">
                        <div id="divStock"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../JS/MUI/js/mui.picker.js"></script>
    <script src="../JS/MUI/js/mui.poppicker.js"></script>
    <script type="text/javascript">

        mui.init();
        //侧滑容器父节点
        var offCanvasWrapper = mui('#offCanvasWrapper');
        //主界面容器
        var offCanvasInner = offCanvasWrapper[0].querySelector('.mui-inner-wrap');
        //菜单容器
        var offCanvasSide = document.getElementById("offCanvasSide");
        //移动效果是否为整体移动
        var moveTogether = false;
        //侧滑容器的class列表，增加.mui-slide-in即可实现菜单移动、主界面不动的效果；
        var classList = offCanvasWrapper[0].classList;
        //变换侧滑动画移动效果；
        classList.add('mui-scalable');
        offCanvasWrapper.offCanvas().refresh();
        //主界面‘显示侧滑菜单’按钮的点击事件
        //document.getElementById('offCanvasShow').addEventListener('tap', function () {
        //    offCanvasWrapper.offCanvas('show');
        //});
        //菜单界面，‘关闭侧滑菜单’按钮的点击事件
        document.getElementById('offCanvasHide').addEventListener('tap', function () {
            QueryTypr();
            offCanvasWrapper.offCanvas('close');
        });
        $(function () {
            QueryTypr();
        });
        function QueryTypr() {
            var typeid = '';
            if ($('#Ykhm').prop("checked")) { typeid += 9 + "/"; }
            if ($('#Plst').prop("checked")) { typeid += 18 + "/"; }
            if ($('#Wdbt').prop("checked")) { typeid += 19 + "/"; }
            if ($('#Dgmp').prop("checked")) { typeid += 36 + "/"; }
            if ($('#Mggb').prop("checked")) { typeid += 66 + "/"; }
            mui.showLoading("正在加载中..", "div"); //加载文字和类型，plus环境中类型为div时强制以div方式显示  
            $.ajax({
                url: 'myAPI.aspx?method=QuerySpecialSaleData',
                dataType: 'json', //服务器返回json格式数据
                cache: false, data: { Specs: $('#Specs').val(), TypeID: typeid, SaleType: "1" },
                success: function (text) {
                    mui.hideLoading();//隐藏后的回调函数  
                    //$('#divStock').html(text);
                    var s = text;
                    var str = "<ul class=\"mui-table-view mui-table-view-chevron\">";
                    for (var j = 0; j < s.length; j++) {
                        str += "<li class='mui-table-view-cell' style='padding-right: 5px; padding-left: 5px;'>";
                        str += "<img class=\"mui-pull-left\" style='height: 62px; width: 73px;' src='" + s[j].FileName + "'>";
                        str += "<div style='width: 3%; min-height: 60px;' class=\" mui-pull-left\"></div>" +
                              "<div style='font-size: 13px;'>" + s[j].Title + "</div>" +
                              "<div class='mui-pull-right' style='font-size: 10px;'><button data-piece=" + s[j].Piece + " data-SalePrice=" + s[j].SalePrice + " data-TypeID=" + s[j].TypeID + " id=" + s[j].OnSaleID + " onclick='addGWC(this)' type='button' class='mui-icon-extra mui-icon-extra-cart'><a href='#picture'></a></button></div>" +
                             " <div style='font-size: 13px;'>" + s[j].Specs + "--" + s[j].Figure + "--" + s[j].LoadIndex + s[j].SpeedLevel + "--" + s[j].BatchYear + "年</div> " +
                              " <div style='font-size: 13px;'>数量：" + s[j].Piece + "&nbsp;条&nbsp;&nbsp;特价：<span  style='font-size: 18px;font-weight: bolder;color: #f01f1f;font-style: oblique;'>" + s[j].SalePrice + "元</span></div></li>";
                        //+ "--" + s[j].Model
                    }
                    str += "</ul>";
                    $("#divStock").html(str);
                }
            });
        }
        function addGWC(ee) {
            var id = $(ee).attr("id");//ID
            var num = Number($(ee).attr("data-piece"));//数量
            var oldP = Number($(ee).attr("data-SalePrice"));//价格
            var TypeID = $(ee).attr("data-TypeID");//轮胎类型 
            if (localStorage.getItem("CART") != null) {
                var cart = localStorage.getItem("CART");
                var cartjson = JSON.parse(cart);
                var isadd = "0";
                for (var i = 0; i < cartjson.length; i++) {
                    if (id == cartjson[i].ID) {
                        cartjson[i].PC = Number(cartjson[i].PC) + 1;
                        if (cartjson[i].PC > num) {
                            //mui.alert('超过库存数量', '提示');
                            var btnArrayA = ['否', '是'];
                            mui.confirm('超过库存数量，是否转到购物车', '提示', btnArrayA, function (e) {
                                if (e.index == 1) {
                                    window.location.href = "shoppingCart.aspx?SaleT=0";
                                }
                            });
                            //$('#tipGWC').html("超过库存数量");
                            return;
                        }
                        isadd = "1";
                    }
                }
                if (isadd == "0") {
                    var addjson = { ID: id, PC: 1, PRICE: oldP, TJ: "1", RO: "OER", TYID: TypeID };
                    cartjson.push(addjson);
                    localStorage.setItem("CART", JSON.stringify(cartjson));
                } else {
                    localStorage.setItem("CART", JSON.stringify(cartjson));
                }
            } else {
                var json = [{ ID: id, PC: 1, PRICE: oldP, TJ: "1", RO: "OER", TYID: TypeID }];
                localStorage.setItem("CART", JSON.stringify(json));
            }
            var btnArray = ['否', '是'];
            mui.confirm('添加购物车成功，是否转到购物车', '提示', btnArray, function (e) {
                if (e.index == 1) {
                    window.location.href = "shoppingCart.aspx?SaleT=0";
                }
            });
        }

        //主界面和侧滑菜单界面均支持区域滚动；
        mui('#offCanvasSideScroll').scroll();
        mui('#offCanvasContentScroll').scroll();
        //实现ios平台原生侧滑关闭页面；
        if (mui.os.plus && mui.os.ios) {
            mui.plusReady(function () { //5+ iOS暂时无法屏蔽popGesture时传递touch事件，故该demo直接屏蔽popGesture功能
                plus.webview.currentWebview().setStyle({
                    'popGesture': 'none'
                });
            });
        }
    </script>

</body>
</html>
