<%@ Page Title="库存价格查询" Language="C#" MasterPageFile="~/QY/QY.Master" AutoEventWireup="true" CodeBehind="qyLeaderStockQuery.aspx.cs" Inherits="Cargo.QY.qyLeaderStockQuery" %>

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
                    <button id='showBatchPicker' class="mui-btn mui-btn-block" type='button'>周期批次</button>
                    <button id='showCityPicker' class="mui-btn mui-btn-block" type='button'>产品品牌</button>
                    <button id="offCanvasHide" type="button" class="mui-btn mui-btn-danger mui-btn-block">确定</button>

                    <input type="text" id="BatchYear" class="mui-input-clear" style="visibility: hidden">
                    <input type="text" id="TypeID" class="mui-input-clear" style="visibility: hidden">
                </div>
            </div>
        </aside>
        <div class="mui-inner-wrap">
            <header class="mui-bar mui-bar-nav">
                <a href="#offCanvasSide" class="mui-icon mui-action-menu mui-icon-bars mui-pull-left"></a>
                <h1 class="mui-title">库存价格查询</h1>
            </header>
            <div id="offCanvasContentScroll" class="mui-content mui-scroll-wrapper">
                <div class="mui-scroll">
                    <div class="mui-content-padded" style="margin: 0px">
                        <div id="divStock"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../JS/MUI/js/mui.picker.js"></script>
    <script src="../JS/MUI/js/mui.poppicker.js"></script>
    <script src="../JS/MUI/js/TryeBrand.js"></script>
    <script type="text/javascript">
        (function ($, doc) {
            //普通示例
            var batchPicker = new $.PopPicker();
            batchPicker.setData([{
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
            cityPicker.setData(cityData);
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
                url: 'qyServices.aspx?method=QueryLeaderStock',
                dataType: 'json', //服务器返回json格式数据
                cache: false, data: { Specs: $('#Specs').val(), Model: $('#Model').val(), Figure: $('#Figure').val(), BatchYear: $('#BatchYear').val(), TypeID: $('#TypeID').val() },
                success: function (text) {
                    mui.hideLoading();//隐藏后的回调函数  
                    //$('#divStock').html(text);
                    var s = text;
                    var str = "<ul class=\"mui-table-view mui-table-view-chevron\">";
                    for (var j = 0; j < s.length; j++) {
                        if (s[j].HouseName == undefined) { continue; }
                        str += "<li class=\"mui-table-view-cell\" style=\"padding-right:7px;\"><div class=\"mui-table\"><div class=\"mui-table-cell mui-col-xs-10\"><h5 class=\"mui-ellipsis\" style=\"font-weight:bold;color:#7739da;padding-top:2px;\">所属仓库：" + s[j].HouseName + "&nbsp;&nbsp;&nbsp;品牌：" + s[j].TypeName + "</h5>";
                        str += "<p style=\"font-weight:bold;color:#0e0d0d;\">规格：" + s[j].Specs + "--" + s[j].Figure + "--" + s[j].LoadIndex + s[j].SpeedLevel + "</p>";
                        str += "<p style=\"font-weight:bold;color:#0e0d0d;\">周期：" + s[j].Batch + "&nbsp;&nbsp;销售价：" + s[j].SalePrice + "元&nbsp;&nbsp;成本价：" + s[j].CostPrice + "元</p></div>";
                        str += "<div class=\"mui-table-cell mui-col-xs-2 mui-text-right\"><span class=\"mui-h5\" style=\"color:red;\">" + s[j].Quantity + "条</span>";
                        str += "</div></div></li>";
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
