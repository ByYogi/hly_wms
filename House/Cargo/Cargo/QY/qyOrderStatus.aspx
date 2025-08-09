<%@ Page Title="订单状态跟踪" Language="C#" MasterPageFile="~/QY/QY.Master" AutoEventWireup="true" CodeBehind="qyOrderStatus.aspx.cs" Inherits="Cargo.QY.qyOrderStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .mui-bar ~ .mui-content .mui-fullscreen {
            top: 44px;
            height: auto;
        }

        .mui-pull-top-tips {
            position: absolute;
            top: -20px;
            left: 50%;
            margin-left: -25px;
            width: 40px;
            height: 40px;
            border-radius: 100%;
            z-index: 1;
        }

        .mui-bar ~ .mui-pull-top-tips {
            top: 24px;
        }

        .mui-pull-top-wrapper {
            width: 42px;
            height: 42px;
            display: block;
            text-align: center;
            background-color: #efeff4;
            border: 1px solid #ddd;
            border-radius: 25px;
            background-clip: padding-box;
            box-shadow: 0 4px 10px #bbb;
            overflow: hidden;
        }

        .mui-pull-top-tips.mui-transitioning {
            -webkit-transition-duration: 200ms;
            transition-duration: 200ms;
        }

        .mui-pull-top-tips .mui-pull-loading {
            /*-webkit-backface-visibility: hidden;
				-webkit-transition-duration: 400ms;
				transition-duration: 400ms;*/
            margin: 0;
        }

        .mui-pull-top-wrapper .mui-icon,
        .mui-pull-top-wrapper .mui-spinner {
            margin-top: 7px;
        }

            .mui-pull-top-wrapper .mui-icon.mui-reverse {
                /*-webkit-transform: rotate(180deg) translateZ(0);*/
            }

        .mui-pull-bottom-tips {
            text-align: center;
            background-color: #efeff4;
            font-size: 15px;
            line-height: 40px;
            color: #777;
        }

        .mui-pull-top-canvas {
            overflow: hidden;
            background-color: #fafafa;
            border-radius: 40px;
            box-shadow: 0 4px 10px #bbb;
            width: 40px;
            height: 40px;
            margin: 0 auto;
        }

            .mui-pull-top-canvas canvas {
                width: 40px;
            }

        .mui-slider-indicator.mui-segmented-control {
            background-color: #efeff4;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--<header class="mui-bar mui-bar-nav">
        <a class="mui-action-back mui-icon mui-icon-left-nav mui-pull-left"></a>
        <h1 class="mui-title">轮胎库存查询</h1>
    </header>--%>

    <div class="mui-content">
        <div id="slider" class="mui-slider mui-fullscreen">
            <div id="sliderSegmentedControl" class="mui-slider-indicator mui-segmented-control mui-segmented-control-inverted">
                <div class="mui-scroll" style="position: fixed;">
                    <a class="mui-control-item mui-active" href="#item1mobile">已下单
                    </a>
                    <a class="mui-control-item" href="#item3mobile">全部订单
                    </a>
                </div>
            </div>
            <div class="mui-slider-group">

                <div id="item1mobile" class="mui-slider-item mui-control-content mui-active">
                    <div id="scroll1" class="mui-scroll-wrapper">
                        <div class="mui-scroll">
                            <ul class="mui-table-view" id="DL">
                            </ul>
                        </div>
                    </div>
                </div>

                <div id="item3mobile" class="mui-slider-item mui-control-content">
                    <div id="scroll3" class="mui-scroll-wrapper">
                        <div class="mui-scroll">
                            <ul class="mui-table-view" id="ALL">
                            </ul>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <script src="../JS/MUI/js/mui.pullToRefresh.js"></script>
    <script src="../JS/MUI/js/mui.pullToRefresh.material.js"></script>
    <script>
        mui.init({
            swipeBack: false, //关闭左滑关闭功能
            gestureConfig: {
                longtap: true //默认为false
            }
        });
        //查当天的订单
        function QueryCurDayOrder() {
            var listView = document.getElementById('DL');
            listView.appendChild(createFragment(listView, 0));

            var listAllView = document.getElementById('ALL');
            listAllView.appendChild(createFragment(listAllView, 1));
        }

        var createFragment = function (ul, index) {
            var length = ul.querySelectorAll('li').length;
            var Pg = Math.round(length / 10) + 1;
            var fragment = document.createDocumentFragment();
            if (index == 0) {
                var dt = new Date();
                $.ajax({
                    async: false, type: 'POST', dataType: 'json', //服务器返回json格式数据
                    url: 'qyServices.aspx?method=QueryCurDayOrder',
                    cache: false, data: { status: "0", StartDate: addDate(dt, -2), EndDate: dt.toDateString(), page: Pg, rows: 10 },
                    success: function (obj) {
                        var s = obj;
                        for (var j = 0; j < s.length; j++) {
                            var li = document.createElement('li');
                            li.className = 'mui-table-view-cell';
                            li.id = s[j].OrderID;
                            li.innerHTML = "<div style='font-size: 14px;'>" + s[j].OrderNo + "&nbsp;&nbsp;&nbsp;&nbsp;" + s[j].Dep + "&nbsp;&nbsp;&nbsp;&nbsp;" + s[j].Dest + "</div>" +
                                " <div style='font-size: 14px;'>" + s[j].AcceptPeople + "&nbsp;&nbsp;" + s[j].AcceptCellphone + "&nbsp;&nbsp;" + s[j].AcceptAddress + "</div>" +
                                " <div style='font-size: 14px;'>件数：" + s[j].Piece + "&nbsp;&nbsp;&nbsp;&nbsp;业务员：" + s[j].SaleManName + "</div>";
                            li.addEventListener('longtap', function (event) {
                                var btnArray = ['否', '是'];
                                var oID = this.getAttribute("id");
                                mui.confirm('修改订单为 已发运 ？', '操作提示', btnArray, function (e) {
                                    if (e.index == 1) {
                                        $.ajax({
                                            url: 'qyServices.aspx?method=UpdateOrderStatus',
                                            cache: false, data: { OrderID: oID, status: "3" },
                                            success: function (text) {
                                                document.location.reload()
                                            }
                                        });
                                    }
                                })
                            });
                            fragment.appendChild(li);
                        }
                    }
                });
            } else {
                $.ajax({
                    async: false, type: 'POST', dataType: 'json', //服务器返回json格式数据
                    url: 'qyServices.aspx?method=QueryCurDayOrder',
                    cache: false, data: { status: "", page: Pg, rows: 10 },
                    success: function (obj) {
                        var s = obj;
                        for (var j = 0; j < s.length; j++) {
                            var li = document.createElement('li');
                            li.className = 'mui-table-view-cell';
                            li.id = s[j].OrderNo;
                            var rig;
                            if (s[j].AwbStatus == "0") {
                                rig = "<div class='mui-pull-right' style='font-weight:bolder ;font-size: 14px;color:#00CD66'>已下单</div>";
                            } else if (s[j].AwbStatus == "2") {
                                rig = "<div class='mui-pull-right' style='font-weight:bolder ;font-size: 14px;color:#FFC125'>已出库</div>";
                            } else if (s[j].AwbStatus == "3") {
                                rig = "<div class='mui-pull-right' style='font-weight:bolder ;font-size: 14px;color:#53868B'>运输在途</div>";
                            } else if (s[j].AwbStatus == "4") {
                                rig = "<div class='mui-pull-right' style='font-weight:bolder ;font-size: 14px;color:#FF0000'>已到达</div>";
                            }
                            li.innerHTML = "<div style='font-size: 14px;'>" + s[j].OrderNo + "&nbsp;&nbsp;&nbsp;&nbsp;" + s[j].Dep + "&nbsp;&nbsp;&nbsp;&nbsp;" + s[j].Dest + "</div>" + rig +
                                " <div style='font-size: 14px;'>" + s[j].AcceptPeople + "&nbsp;&nbsp;" + s[j].AcceptCellphone + "&nbsp;&nbsp;" + s[j].AcceptAddress + "</div>" +
                                " <div style='font-size: 14px;'>件数：" + s[j].Piece + "&nbsp;&nbsp;&nbsp;&nbsp;业务员：" + s[j].SaleManName + "</div>";
                            fragment.appendChild(li);
                        }
                    }
                });
            }
            return fragment;
        };
        (function ($) {
            QueryCurDayOrder();
            //阻尼系数
            var deceleration = mui.os.ios ? 0.003 : 0.0009;
            $('.mui-scroll-wrapper').scroll({
                bounce: false,
                indicators: true, //是否显示滚动条
                deceleration: deceleration
            });
            $.ready(function () {
                //循环初始化所有下拉刷新，上拉加载。
                $.each(document.querySelectorAll('.mui-slider-group .mui-scroll'), function (index, pullRefreshEl) {
                    $(pullRefreshEl).pullToRefresh({
                        //down: {
                        //    callback: function () {
                        //        var self = this;
                        //        setTimeout(function () {
                        //            var ul = self.element.querySelector('.mui-table-view');
                        //            ul.insertBefore(createFragment(ul, index, 4, true), ul.firstChild);
                        //            self.endPullDownToRefresh();
                        //        }, 1000);
                        //    }
                        //},
                        up: {
                            callback: function () {
                                var self = this;
                                setTimeout(function () {
                                    var ul = self.element.querySelector('.mui-table-view');
                                    ul.appendChild(createFragment(ul, index));
                                    self.endPullUpToRefresh();
                                }, 1000);
                            }
                        }
                    });
                });

            });

        })(mui);
        // 日期月份/天的显示，如果是1位数，则在前面加上'0'
        function getFormatDate(arg) {
            if (arg == undefined || arg == '') {
                return '';
            }

            var re = arg + '';
            if (re.length < 2) {
                re = '0' + re;
            }

            return re;
        }
        function addDate(date, days) {
            if (days == undefined || days == '') {
                days = 1;
            }
            var date = new Date(date);
            date.setDate(date.getDate() + days);
            var month = date.getMonth() + 1;
            var day = date.getDate();
            return date.getFullYear() + '-' + getFormatDate(month) + '-' + getFormatDate(day);
        }
    </script>
</asp:Content>
