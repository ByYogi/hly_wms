<%@ Page Title="销量统计" Language="C#" MasterPageFile="~/QY/QY.Master" AutoEventWireup="true" CodeBehind="SalesStatistics.aspx.cs" Inherits="Cargo.QY.SalesStatistics" %>

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
                        <label>花纹：</label>
                        <input type="text" id="Figure" class="mui-input-clear" placeholder="请输入花纹">
                    </div>
                    <div class="mui-input-row" style="color: white;">
                        <label>货品代码：</label>
                        <input type="text" id="GoodsCode" class="mui-input-clear" placeholder="请输入货品代码">
                    </div>
                    <button id='showCityPicker' class="mui-btn mui-btn-block" type='button'>产品品牌</button>
                    <button id="offCanvasHide" type="button" class="mui-btn mui-btn-danger mui-btn-block">确定</button>

                    <input type="text" id="TypeID" class="mui-input-clear" style="visibility: hidden">
                </div>
            </div>
        </aside>
        <div class="mui-inner-wrap">
            <header class="mui-bar mui-bar-nav">
                <a href="#offCanvasSide" class="mui-icon mui-action-menu mui-icon-bars mui-pull-left"></a>
                <h1 class="mui-title">销量统计</h1>
            </header>
            <div id="offCanvasContentScroll" class="mui-content mui-scroll-wrapper">
                <div class="mui-scroll">
                    <div class="mui-content-padded" style="margin: 0px">
                        <div id="divStock">
                            <%--<table id="dataTable" style="text-align: center; width: 100%; font-size: 14px; color: #424345;">
                                <tr style="font-size: 16px;font-weight: bold;color: #55524f;height: 30px;">
                                    <td style="width: 24%;"><button type="button" class="mui-btn mui-btn-outlined" style="border: none;color: #60686f;" onclick="sort('house')">所属仓库</button></td>
                                    <td style="width: 24%;"><button type="button" class="mui-btn mui-btn-outlined" style="border: none;color: #60686f;" onclick="sort('type')">品牌</button></td>
                                    <td style="width: 12%;"><button type="button" class="mui-btn mui-btn-outlined" style="border: none;color: #60686f;" onclick="sort(1)">1月</button></td>
                                    <td style="width: 12%;"><button type="button" class="mui-btn mui-btn-outlined" style="border: none;color: #60686f;" onclick="sort(3)">3月</button></td>
                                    <td style="width: 13%;"><button type="button" class="mui-btn mui-btn-outlined" style="border: none;color: #60686f;" onclick="sort(6)">6月</button></td>
                                    <td style="width: 15%;"><button type="button" class="mui-btn mui-btn-outlined" style="border: none;color: #60686f;" onclick="sort(12)">12月</button></td>
                                </tr>
                                <tr style="border-top: 1px solid #c1c1c1;">
                                    <td style="">广东仓库</td>
                                    <td style="border-right: 1px solid #c1c1c1;">优科豪马</td>
                                    <td rowspan="3" style="color: #99CC00;">10000</td>
                                    <td rowspan="3" style="color: #0099CC;">50000</td>
                                    <td rowspan="3" style="color: #FF9966;">100000</td>
                                    <td rowspan="3" style="color: #FF6666;">1500000</td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="border-right: 1px solid #c1c1c1; text-align: left;font-weight: bold;">205/55R16  FRPT6#</td>
                                </tr>
                                <tr style="border-bottom: 1px solid #c1c1c1;">
                                    <td colspan="2" style="border-right: 1px solid #c1c1c1; text-align: left;">03116570000  91V</td>
                                </tr>
                                <tr style="border-top: 1px solid #c1c1c1;">
                                    <td style="">广东仓库</td>
                                    <td style="border-right: 1px solid #c1c1c1;">优科豪马</td>
                                    <td rowspan="3" style="color: #99CC00;">10000</td>
                                    <td rowspan="3" style="color: #0099CC;">50000</td>
                                    <td rowspan="3" style="color: #FF9966;">100000</td>
                                    <td rowspan="3" style="color: #FF6666;">1500000</td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="border-right: 1px solid #c1c1c1; text-align: left;font-weight: bold;">205/55R16  FRPT6#</td>
                                </tr>
                                <tr style="border-bottom: 1px solid #c1c1c1;">
                                    <td colspan="2" style="border-right: 1px solid #c1c1c1; text-align: left;">03116570000  91V</td>
                                </tr>
                                <tr style="border-top: 1px solid #c1c1c1;">
                                    <td style="">广东仓库</td>
                                    <td style="border-right: 1px solid #c1c1c1;">优科豪马</td>
                                    <td rowspan="3" style="color: #99CC00;">10000</td>
                                    <td rowspan="3" style="color: #0099CC;">50000</td>
                                    <td rowspan="3" style="color: #FF9966;">100000</td>
                                    <td rowspan="3" style="color: #FF6666;">1500000</td>
                                </tr>
                                <tr>
                                    <td colspan="2" style="border-right: 1px solid #c1c1c1; text-align: left;font-weight: bold;">205/55R16  FRPT6#</td>
                                </tr>
                                <tr style="border-bottom: 1px solid #c1c1c1;">
                                    <td colspan="2" style="border-right: 1px solid #c1c1c1; text-align: left;">03116570000  91V</td>
                                </tr>
                            </table>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../JS/MUI/js/mui.picker.js"></script>
    <script src="../JS/MUI/js/mui.poppicker.js"></script>
    <script src="../JS/MUI/js/TryeBrand2.js"></script>
    <script type="text/javascript">
        var listItem = document.getElementById('dataTable');
        //然后是写一个函数，也就是列表条目的点击事件。
        var clickEvent = function (i) {
            listItem[i].addEventListener('tap', function (event) {
                console.log(i);
                mui.toast(listItem[i].textContent);
            });
        }
        function sort(type) {
            mui.showLoading("正在加载中..", "div"); //加载文字和类型，plus环境中类型为div时强制以div方式显示  
            $.ajax({
                url: 'qyServices.aspx?method=SaleStatistics&SortType=' + type,
                dataType: 'json', //服务器返回json格式数据
                cache: false, data: { Specs: $('#Specs').val(), Figure: $('#Figure').val(), GoodsCode: $('#GoodsCode').val(), TypeID: $('#TypeID').val() },
                success: function (text) {
                    $("#divStock").html("");
                    mui.hideLoading();//隐藏后的回调函数  
                    //$('#divStock').html(text);
                    var s = text;
                    var str = "<table id='dataTable'style='text-align: center; width: 100%; font-size: 14px; color: #424345;'><tr style='font-size: 16px;font-weight: bold;color: #55524f;height: 30px;'><td style='width: 20%;'><button type='button'class='mui-btn mui-btn-outlined'style='border: none;color: #60686f;'onclick='sort(-1)'>所属仓库</button></td><td style='width: 20%;'><button type='button'class='mui-btn mui-btn-outlined'style='border: none;color: #60686f;'onclick='sort(0)'>品牌</button></td><td style='width: 12%;'><button type='button'class='mui-btn mui-btn-outlined'style='border: none;color: #60686f;'onclick='sort(1)'>1月</button></td><td style='width: 12%;'><button type='button'class='mui-btn mui-btn-outlined'style='border: none;color: #60686f;'onclick='sort(3)'>3月</button></td><td style='width: 13%;'><button type='button'class='mui-btn mui-btn-outlined'style='border: none;color: #60686f;'onclick='sort(6)'>6月</button></td><td style='width: 15%;'><button type='button'class='mui-btn mui-btn-outlined'style='border: none;color: #60686f;'onclick='sort(12)'>12月</button></td></tr>";
                    for (var j = 0; j < s.length; j++) {
                        if (s[j].HouseName == undefined) { continue; }
                        str += "<tr style='border-top: 1px solid #c1c1c1;'><td style=''>" + s[j].HouseName + "</td><td style='border-right: 1px solid #c1c1c1;'>" + s[j].TypeName + "</td><td rowspan='3'style='color: #99CC00;'>" + s[j].a1Piece + "</td><td rowspan='3'style='color: #0099CC;'>" + s[j].a3Piece + "</td><td rowspan='3'style='color: #FF9966;'>" + s[j].a6Piece + "</td><td rowspan='3'style='color: #FF6666;'>" + s[j].a12Piece + "</td></tr><tr><td colspan='2'style='border-right: 1px solid #c1c1c1; text-align: left;font-weight: bold;'>" + s[j].Specs + "&nbsp;&nbsp;" + s[j].Figure + "</td></tr><tr style='border-bottom: 1px solid #c1c1c1;'><td colspan='2'style='border-right: 1px solid #c1c1c1; text-align: left;'>" + s[j].GoodsCode + "&nbsp;&nbsp;" + s[j].LoadIndex + s[j].SpeedLevel + "</td></tr>";
                    }
                    str += "</table>";
                    $("#divStock").html(str);
                }
            });
        }
        (function ($, doc) {
            var cityPicker = new $.PopPicker({
                layer: 2
            });
            cityPicker.setData(ProductTypeData);
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
                url: 'qyServices.aspx?method=SaleStatistics',
                dataType: 'json', //服务器返回json格式数据
                cache: false, data: { Specs: $('#Specs').val(), Figure: $('#Figure').val(), GoodsCode: $('#GoodsCode').val(), TypeID: $('#TypeID').val() },
                success: function (text) {
                    $("#divStock").html("");
                    mui.hideLoading();//隐藏后的回调函数  
                    //$('#divStock').html(text);
                    var s = text;
                    var str = "<table id='dataTable'style='text-align: center; width: 100%; font-size: 14px; color: #424345;'><tr style='font-size: 16px;font-weight: bold;color: #55524f;height: 30px;'><td style='width: 20%;'><button type='button'class='mui-btn mui-btn-outlined'style='border: none;color: #60686f;'onclick='sort(-1)'>所属仓库</button></td><td style='width: 20%;'><button type='button'class='mui-btn mui-btn-outlined'style='border: none;color: #60686f;'onclick='sort(0)'>品牌</button></td><td style='width: 12%;'><button type='button'class='mui-btn mui-btn-outlined'style='border: none;color: #60686f;'onclick='sort(1)'>1月</button></td><td style='width: 12%;'><button type='button'class='mui-btn mui-btn-outlined'style='border: none;color: #60686f;'onclick='sort(3)'>3月</button></td><td style='width: 13%;'><button type='button'class='mui-btn mui-btn-outlined'style='border: none;color: #60686f;'onclick='sort(6)'>6月</button></td><td style='width: 15%;'><button type='button'class='mui-btn mui-btn-outlined'style='border: none;color: #60686f;'onclick='sort(12)'>12月</button></td></tr>";
                    for (var j = 0; j < s.length; j++) {
                        if (s[j].HouseName == undefined) { continue; }
                        str += "<tr style='border-top: 1px solid #c1c1c1;'><td style=''>" + s[j].HouseName + "</td><td style='border-right: 1px solid #c1c1c1;'>" + s[j].TypeName + "</td><td rowspan='3'style='color: #99CC00;'>" + s[j].a1Piece + "</td><td rowspan='3'style='color: #0099CC;'>" + s[j].a3Piece + "</td><td rowspan='3'style='color: #FF9966;'>" + s[j].a6Piece + "</td><td rowspan='3'style='color: #FF6666;'>" + s[j].a12Piece + "</td></tr><tr><td colspan='2'style='border-right: 1px solid #c1c1c1; text-align: left;font-weight: bold;'>" + s[j].Specs + "&nbsp;&nbsp;" + s[j].Figure + "</td></tr><tr style='border-bottom: 1px solid #c1c1c1;'><td colspan='2'style='border-right: 1px solid #c1c1c1; text-align: left;'>" + s[j].GoodsCode + "&nbsp;&nbsp;" + s[j].LoadIndex + s[j].SpeedLevel + "</td></tr>";
                    }
                    str += "</table>";
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
