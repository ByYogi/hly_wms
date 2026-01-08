<%@ Page Title="轮胎库存查询" Language="C#" MasterPageFile="~/QY/QY.Master" AutoEventWireup="true" CodeBehind="TypeStockQuery.aspx.cs" Inherits="Cargo.QY.TypeStockQuery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../JS/MUI/css/mui.picker.css" rel="stylesheet" />
    <link href="../JS/MUI/css/mui.poppicker.css" rel="stylesheet" />
    <script src="../JS/MUI/js/showLoading.js"></script>
    <style>
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="offCanvasWrapper" class="mui-off-canvas-wrap mui-draggable">
        <!--侧滑菜单部分-->
        <aside id="offCanvasSide" class="mui-off-canvas-left">
            <div id="offCanvasSideScroll" class="mui-scroll-wrapper">
                <div class="mui-scroll">
                    <div class="mui-input-row" style="color: white;">
                        <label>规格：</label>
                        <input type="text" id="Specs" class="mui-input-clear" placeholder="请输入规格">
                    </div>
                    <div class="mui-input-row" style="color: white;">
                        <label>型号：</label>
                        <input type="text" id="Model" class="mui-input-clear" placeholder="请输入型号">
                    </div>
                    <div class="mui-input-row" style="color: white;">
                        <label>花纹：</label>
                        <input type="text" id="Figure" class="mui-input-clear" placeholder="请输入花纹">
                    </div>
                    <%--   <div class="mui-input-row" style="color: white;">
                        <label>批次：</label>
                        <input type="text" id="Batch" class="mui-input-clear" placeholder="请输入批次">
                    </div>--%>
                    <button id='showHousePicker' class="mui-btn mui-btn-block" type='button'>所属仓库</button>
                    <button id='showBatchPicker' class="mui-btn mui-btn-block" type='button'>年周周期</button>
                    <button id='showCityPicker' class="mui-btn mui-btn-block" type='button'>产品类型</button>
                    <button id="offCanvasHide" type="button" class="mui-btn mui-btn-danger mui-btn-block">确定</button>

                    <input type="text" id="HouseID" class="mui-input-clear" style="visibility: hidden">
                    <input type="text" id="BatchYear" class="mui-input-clear" style="visibility: hidden">
                    <input type="text" id="TypeID" class="mui-input-clear" style="visibility: hidden">
                </div>
            </div>
        </aside>

        <%--<header class="mui-bar mui-bar-nav">
        <a class="mui-action-back mui-icon mui-icon-left-nav mui-pull-left"></a>
        <h1 class="mui-title">轮胎库存查询</h1>
    </header>--%>

        <div class="mui-inner-wrap">
            <header class="mui-bar mui-bar-nav">
                <a href="#offCanvasSide" class="mui-icon mui-action-menu mui-icon-bars mui-pull-left"></a>
                <%-- <a class="mui-action-back mui-btn mui-btn-link mui-pull-right">关闭</a>--%>
                <h1 class="mui-title">轮胎库存查询</h1>
            </header>
            <div id="offCanvasContentScroll" class="mui-content mui-scroll-wrapper">
                <div class="mui-scroll">
                    <div class="mui-content-padded" style="margin: 0px 0px 50px 0px;">
                        <%--  <div class="mui-input-row mui-search" style="margin-bottom: -10px; margin-top: 3px;">
                            <input type="search" id="searchInput" onkeyup="enterSearch(event)" class="mui-input-clear" placeholder="请输入规格和花纹">
                        </div>--%>
                        <div id="divStock"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <nav class="mui-bar mui-bar-tab">
        <a class="mui-tab-item  mui-active" href="../QY/MyWorkFloor.aspx">
            <span class="mui-icon mui-icon-home-filled"></span>
            <span style="font-size: 11px; display: block; overflow: hidden; text-overflow: ellipsis;">我的工作台</span>
        </a>
        <a class="mui-tab-item" href="../QY/qyShoppingCart.aspx">
            <span class="mui-icon-extra mui-icon-extra-cart"><span id="gwc" style="font-size: 10px; line-height: 1.4; position: absolute; top: 2px; margin-left: 1px; padding: 1px 5px; color: #fff; background: red; border-radius: 100px; display: inline-block;">0</span></span>
            <span style="font-size: 11px; display: block; overflow: hidden; text-overflow: ellipsis;">购物车</span>
        </a>
        <a class="mui-tab-item" href="../QY/qyQueryMyOrder.aspx">
            <span class="mui-icon mui-icon-bars"></span>
            <span style="font-size: 11px; display: block; overflow: hidden; text-overflow: ellipsis;">我的订单</span>
        </a>
    </nav>
    <script src="../JS/MUI/js/mui.picker.js"></script>
    <script src="../JS/MUI/js/mui.poppicker.js"></script>
    <script src="../JS/MUI/js/TryeBrand.js"></script>
    <script src="../JS/MUI/js/TryeBrand2.js"></script>
    <script type="text/javascript">
        (function ($, doc) {
            //所属仓库下拉列表
            var housePicker = new $.PopPicker();
            housePicker.setData(<%=hStr%>);
            var showHousePickerButton = doc.getElementById('showHousePicker');
            showHousePickerButton.addEventListener('tap', function (event) {
                housePicker.show(function (items) {
                    showHousePickerButton.textContent = items[0].text;
                    doc.getElementById('HouseID').value = items[0].value;
                    //返回 false 可以阻止选择框的关闭
                    //return false;
                });
            }, false);
            //周期
            var batchPicker = new $.PopPicker();
            batchPicker.setData([{
                value: '26',
                text: '2026年'
            }, {
                value: '25',
                text: '2025年'
            }, {
                value: '24',
                text: '2024年'
            }, {
                value: '23',
                text: '2023年'
            }, {
                value: '22',
                text: '2022年'
            }, {
                value: '21',
                text: '2021年'
            }, {
                value: '20',
                text: '2020年'
            }, {
                value: '19',
                text: '2019年'
            }, {
                value: '18',
                text: '2018年'
            }, {
                value: '17',
                text: '2017年'
            }]);
            var showBatchPickerButton = doc.getElementById('showBatchPicker');
            showBatchPickerButton.addEventListener('tap', function (event) {
                batchPicker.show(function (items) {
                    showBatchPickerButton.textContent = items[0].text;
                    doc.getElementById('BatchYear').value = items[0].value;
                    //返回 false 可以阻止选择框的关闭
                    //return false;
                });
            }, false);

            var cityPicker = new $.PopPicker({
                layer: 2
            });
            if (<%=QyUserInfo.RoleID%> == 42) {
                cityPicker.setData(maPaiProductTypeData);
            } else {
                cityPicker.setData(cityData);
            }
            var showCityPickerButton = doc.getElementById('showCityPicker');
            showCityPickerButton.addEventListener('tap', function (event) {
                cityPicker.show(function (items) {
                    showCityPickerButton.textContent = items[1].text;
                    doc.getElementById('TypeID').value = items[1].value;
                    //$('#showCityPicker').html(items[0].text + " " + items[1].text);
                    //返回 false 可以阻止选择框的关闭
                    //return false;
                });
            }, false);
        })(mui, document);
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
            mui.showLoading("正在加载中..", "div"); //加载文字和类型，plus环境中类型为div时强制以div方式显示  
            $.ajax({
                url: 'qyServices.aspx?method=QueryTypeStock',
                dataType: 'json', //服务器返回json格式数据
                cache: false, data: { Specs: $('#Specs').val(), Model: $('#Model').val(), Figure: $('#Figure').val(), BatchYear: $('#BatchYear').val(), TypeID: $('#TypeID').val(), HouseID: $('#HouseID').val(), IsShowStock: '0' },
                success: function (text) {
                    var RoleID = "<%=QyUserInfo.RoleID%>";
                    mui.hideLoading();//隐藏后的回调函数  
                    //$('#divStock').html(text);
                    var s = text;
                    var str = "<ul class=\"mui-table-view mui-table-view-chevron\">";
                    for (var j = 0; j < s.length; j++) {
                        if (RoleID == 42 && s[j].TypeID == "34" && s[j].Source == "26") { continue; }
                        if (RoleID == 42 && s[j].TypeID != "34" && s[j].TypeID != "345") { continue }
                        if (RoleID == 132 && s[j].TypeID != "39" && s[j].TypeID != "66") { continue }
                        str += "<li class='mui-table-view-cell' style='padding-right: 5px; padding-left: 5px;'>";
                        if (s[j].TypeID == "9") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/ykhm.png'>";
                        } else if (s[j].TypeID == "18") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/plst.png'>";
                        } else if (s[j].TypeID == "27") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/wanli.jpg'>";
                        } else if (s[j].TypeID == "34") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/mapai.jpg'>";
                        } else if (s[j].TypeID == "36") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/mql.png'>";
                        } else if (s[j].TypeID == "66") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/gupo.png'>";
                        } else if (s[j].TypeID == "31") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/hantai.jpg'>";
                        } else if (s[j].TypeID == "171") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/damai.png'>";
                        } else if (s[j].TypeID == "178") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/luji.jpg'>";
                        } else if (s[j].TypeID == "157") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/gitong.jpg'>";
                        } else if (s[j].TypeID == "180") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/wokaitu.png'>";
                        } else if (s[j].TypeID == "345") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/weijing.jpg'>";
                        } else if (s[j].TypeID == "338") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/chifeng.jpg'>";
                        } else if (s[j].TypeID == "344") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/xidatong.png'>";
                        } else if (s[j].TypeID == "98") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/linglong.jpg'>";
                        } else if (s[j].TypeID == "165") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/haoyun.jpg'>";
                        } else if (s[j].TypeID == "149") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/majisi.jpg'>";
                        } else if (s[j].TypeID == "159") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/zhengxin.jpg'>";
                        } else {
                            str += "<div style='width:62px;height:62px;float:left;background-color: #63b4ef;text-align: center;font-size: 18px;color: white;border-radius: 10px; '><span style='line-height: 60px;'>" + s[j].TypeName + "</span></div>";
                            //str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/wdbt.png'>";
                        }
                        str += "  <div style='width: 3%; min-height: 60px;' class=\" mui-pull-left\"></div>" +
                            "  <div style='font-size: 12px;'>" + s[j].Specs + "--" + s[j].Figure + "--" + s[j].Model + "--" + s[j].LoadIndex + s[j].SpeedLevel + "</div>" +
                            " <div class='mui-pull-right' style='font-size: 13px;'><button data-piece=" + s[j].Piece + " id=" + s[j].ID + " onclick='addGWC(this)' type='button' class='mui-btn mui-btn-success mui-icon mui-icon-plus'>购物车</button></div>" +
                            " <div style='font-size: 12px;'>" + s[j].FirstAreaName + "--";
                        //" <div style='font-size: 12px;'>" + s[j].FirstAreaName + "--" + s[j].AreaName + "--";
                        str += s[j].Batch + "</div> ";
                        //if (s[j].HouseID == "9" || s[j].HouseID == "44" || s[j].HouseID == "45" || s[j].HouseID == "64" || s[j].HouseID == "83" || s[j].HouseID == "84" || s[j].HouseID == "89") {
                        //    str += s[j].Batch + "</div> ";
                        //} else {
                        //    str += s[j].BatchYear + "年</div> ";
                        //}
                        if (s[j].HouseID == "64") {
                            //粤信广通显示成本价
                            str += " <div style='font-size: 13px; font-weight: bold; color: #FD6C24;'>存：" + s[j].Piece + "条&nbsp;&nbsp;批/本：" + s[j].TradePrice + "/" + s[j].CostPrice + "元</div></li>";
                        }
                        else {
                            if (s[j].TypeID == "34" && s[j].Source == "26") {
                                //马牌OES
                                str += " <div style='font-size: 13px; font-weight: bold; color: #FD6C24;'>存：" + s[j].Piece + "条&nbsp;&nbsp;售价：" + s[j].TradePrice + "元(OES)</div></li>";
                            } else {
                                str += " <div style='font-size: 13px; font-weight: bold; color: #FD6C24;'>存：" + s[j].Piece + "条&nbsp;门：" + s[j].TradePrice + "元&nbsp;云仓：" + s[j].SalePrice + "元</div></li>";
                            }
                        }
                    }
                    str += "</ul>";
                    $("#divStock").html(str);
                }
            });

            offCanvasWrapper.offCanvas('close');
        });
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

        if (localStorage.getItem("CART") != null) {
            var cart = localStorage.getItem("CART");
            var cartjson = JSON.parse(cart);
            var carcount = 0;
            for (var i = 0; i < cartjson.length; i++) {
                carcount += Number(cartjson[i].PC);
            }
            document.getElementById("gwc").innerHTML = carcount;
        }


        function addGWC(ee) {
            //e.detail.gesture.preventDefault(); //修复iOS 8.x平台存在的bug，使用plus.nativeUI.prompt会造成输入法闪一下又没了
            var btnArray = ['取消', '确定'];
            mui.prompt('', '', '请输入条数和价格，以/隔开', btnArray, function (e) {
                if (e.index == 1) {
                    var oldPiece = Number($('#gwc').html());
                    var pie = 0, money = 0;
                    var s = e.value.split('/');
                    if (s.length == 1) {
                        pie = s[0];
                    }
                    if (s.length == 2) {
                        pie = s[0];
                        money = s[1];
                    }
                    var oldP = Number($(ee).attr("data-piece"));
                    if (pie > oldP) {
                        mui.alert('库存件数不足 在库：' + oldP + '条', '提示');
                        return;
                    }
                    if (localStorage.getItem("CART") != null) {
                        var id = $(ee).attr("id");
                        var isexist = 0;
                        var jsoncat = JSON.parse(localStorage.getItem("CART"));
                        for (var i = 0; i < jsoncat.length; i++) {
                            if (id == jsoncat[i].ID) {
                                isexist = 1;
                                jsoncat[i].PC = Number(jsoncat[i].PC) + Number(pie);
                            }
                        }
                        if (isexist == 1) {
                            localStorage.setItem("CART", JSON.stringify(jsoncat));
                        } else {
                            var oldjson = localStorage.getItem("CART").split(']')[0] + ',';
                            var addjson = { ID: $(ee).attr("id"), PC: pie, PRICE: money };
                            var newjson = oldjson + JSON.stringify(addjson) + "]";
                            localStorage.setItem("CART", newjson);
                        }
                    } else {
                        var json = [{ ID: $(ee).attr("id"), PC: pie, PRICE: money }];
                        localStorage.setItem("CART", JSON.stringify(json));
                    }
                    var result = oldPiece + Number(pie);
                    $('#gwc').html(result);
                    //info.innerText = e.value;
                } else {
                    //info.innerText = '你点了取消按钮';
                }
            })
        }

        function enterSearch(e) {
            if (e.keyCode == 13) {
                var searchKey = $('#searchInput').val();
                $.ajax({
                    url: 'qyServices.aspx?method=QueryTypeStock',
                    cache: false, data: { key: searchKey },
                    success: function (text) {
                        $('#divStock').html(text);
                        //var ldl = document.getElementById("divStock");
                        //ldl.innerHTML = text;
                    }
                });
            }
        }
        mui('body').on('tap', 'a', function () { document.location.href = this.href; });

    </script>
</asp:Content>
