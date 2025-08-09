<%@ Page Title="狄乐汽服周报" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reportDLWeekly.aspx.cs" Inherits="Cargo.Report.reportDLWeekly" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../JS/FloatNavi/base.css" rel="stylesheet" />
    <script src="../JS/FloatNavi/common.js" type="text/javascript"></script>
    <script src="../JS/EChart/chart.umd.js" type="text/javascript"></script>
    <script src="../JS/EChart/chartjs-plugin-datalabels.min.js" type="text/javascript"></script>
    <style type="text/css">
        body {
            margin: 5px;
            background-color: #f1f1f1;
        }

        .Chart ul li {
            float: left;
            /*border: solid 0.1px #00FF00;*/
            width: 100%;
            height: 50%;
            position: relative;
            margin-top: 2%;
        }

        .boxDid {
            float: left;
            height: 50px;
            width: 33%;
        }

        /*#Chart ul li canvas {*/
        /*text-align: center;*/
        /*position: absolute;
                top: 50%;
                left: 50%;
                transform: translate(-50%, -50%);
            }*/

        .dg > div {
            /*text-align: center;*/
            float: left;
            width: 32%;
            height: 100%;
            /*            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);*/
        }

        .divChart {
            float: left;
            width: 32%;
            height: 100%;
            margin-left: 1%;
            /*padding:0px 25% 0px 0px;*/
        }

        .Operate > .Chart ul li > .OperateDg {
            float: left;
            width: 50%;
            height: 100%;
        }

        .arrow {
            border: solid black;
            border-width: 0 3px 3px 0;
            display: inline-block;
            padding: 3px;
        }

        .up {
            transform: rotate(-135deg);
            -webkit-transform: rotate(-135deg);
            border-color: green;
        }

        .down {
            transform: rotate(45deg);
            -webkit-transform: rotate(45deg);
            border-color: red;
        }
    </style>
    <script type="text/javascript">
        $(function () {

            var windowHeight = $(window).height() - 150;
            $(".Chart").css("height", windowHeight)
            RELOAD();

        });
        function RELOAD() {
            $('#dgTop20onsale').datagrid({
                width: '100%',
                height: '100%',
                title: '销售前20名', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: false, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ShopCode',
                rownumbers: true,
                showFooter: true,
                toolbar: '',
                columns: [[
                    { title: '店代码', field: 'ShopCode', width: '70px' },
                    { title: '店名称', field: 'ClientName', width: '120px' },
                    { title: '销售量', field: 'OrderPiece', width: '80px', align: 'right' },
                    {
                        title: '销售占比', field: 'NumSalesproportion', width: '80px', align: 'right', formatter: function (value) {
                            return (value * 100).toFixed(2) + "%";
                        }
                    },
                ]],
                onClickRow: function (index, row) { },
            });
            $('#dgPinCountdown20').datagrid({
                width: '100%',
                height: '100%',
                title: '销售后20名', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: false, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ShopCode',
                rownumbers: true,
                showFooter: true,
                toolbar: '',
                columns: [[
                    { title: '店代码', field: 'ShopCode', width: '70px' },
                    { title: '店名称', field: 'ClientName', width: '120px' },
                    { title: '销售量', field: 'OrderPiece', width: '80px', align: 'right' },
                ]],
                onClickRow: function (index, row) { },
            });

            $('#dgPunctualityRate').datagrid({
                width: '100%',
                height: '100%',
                title: '区域准点率', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: false, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'Specs',
                rownumbers: true,
                showFooter: true,
                toolbar: '',
                columns: [[
                    { title: '区域', field: 'Specs', width: '70px' },
                    {
                        title: '准点率', field: 'PunctualityRate', width: '230px', align: 'center', formatter: function (value) {
                            return (value * 100).toFixed(2) + "%";
                        }
                    },
                ]],
                onClickRow: function (index, row) { },
            });

            $('#dgShopPunctualityRate').datagrid({
                width: '100%',
                height: '100%',
                title: '授权店准点率', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: false, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ShopCode',
                rownumbers: true,
                showFooter: true,
                toolbar: '',
                columns: [[
                    { title: '店代码', field: 'ShopCode', width: '70px' },
                    { title: '店名称', field: 'ShopName', width: '120px', align: 'center' },
                    {
                        title: '准点率', field: 'PunctualityRate', width: '80px', align: 'right', formatter: function (value) {
                            return (value * 100).toFixed(2) + "%";
                        }
                    },
                ]],
                onClickRow: function (index, row) { },
            });
        }
        function doQuery() {
            var gridOpts = $('#dgTop20onsale').datagrid('options');
            gridOpts.url = 'reportApi.aspx?method=QuerySalesVolume';
            $('#dgTop20onsale').datagrid('load', {
                StartDate: $('#AStartDate').datebox('getValue'),
                EndDate: $('#AEndDate').datebox('getValue'),
                around: "0",
                HouseID: $('#AHouseID').combobox('getValue'),
            });

            gridOpts = $('#dgPinCountdown20').datagrid('options');
            gridOpts.url = 'reportApi.aspx?method=QuerySalesVolume';
            $('#dgPinCountdown20').datagrid('load', {
                StartDate: $('#AStartDate').datebox('getValue'),
                EndDate: $('#AEndDate').datebox('getValue'),
                around: "1",
                HouseID: $('#AHouseID').combobox('getValue'),
            });

            gridOpts = $('#dgPunctualityRate').datagrid('options');
            gridOpts.url = 'reportApi.aspx?method=RegionalpunctualityRate';
            $('#dgPunctualityRate').datagrid('load', {
                StartDate: $('#AStartDate').datebox('getValue'),
                EndDate: $('#AEndDate').datebox('getValue'),
                HouseID: $('#AHouseID').combobox('getValue'),
            });
            gridOpts = $('#dgShopPunctualityRate').datagrid('options');
            gridOpts.url = 'reportApi.aspx?method=ShopPunctualityrate';
            $('#dgShopPunctualityRate').datagrid('load', {
                StartDate: $('#AStartDate').datebox('getValue'),
                EndDate: $('#AEndDate').datebox('getValue'),
                HouseID: $('#AHouseID').combobox('getValue'),
            });

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="font-size: 30px; font-weight: bold; background-color: white; padding-left: 20px; padding-bottom: 3px; padding-top: 3px;">
        <span style="font-size: 30px; font-weight: bold; background-color: white; padding-left: 20px; padding-bottom: 3px; padding-top: 3px;" id="title"></span><span id="DateTime"></span>
    </div>
    <div id="saPanel" style="font-weight: bold; background-color: white; padding-bottom: 3px; padding-top: 3px; margin-top: 5px;">
        <table style="text-align: center;">
            <tr>
                <td><span style="font-weight: lighter; font-size: 15px; color: #808080">周订单数(个)</span><br />
                    <br style="line-height: 5px;" />
                    <span style="font-size: 30px; font-weight: bold; color: #1ea7f7;" id="Weeklyorders">0</span></td>

                <td>
                    <div class="boxDid">
                    </div>
                    <div class="boxDid">
                        <span style="font-weight: lighter; font-size: 15px; color: #808080">周销售量(条)</span><br />
                        <span style="font-size: 30px; font-weight: bold; color: #1ea7f7;" id="Weeklysalesvolume">0</span>
                    </div>
                    <div class="boxDid" style="position: relative;" id="OrderNumDiv">
                        <%--                        <i class="up arrow" style="position:relative;top:20px;left:-60px;display:none"></i>
                        <i class="down arrow" style="position:relative;top:34px;left:-73px;display:none"></i>--%>
                        <span style="position: relative; left: -30px; bottom: -35px;" id="OrderNumText"></span>
                    </div>

                </td>


                <td><span style="font-weight: lighter; font-size: 15px; color: #808080">月订单数(个)</span><br />
                    <br style="line-height: 5px;" />
                    <span style="font-size: 30px; font-weight: bold; color: #ed2323;" id="Monthlyorders">0</span></td>
                <td>
                    <div class="boxDid">
                    </div>
                    <div class="boxDid">
                        <span style="font-weight: lighter; font-size: 15px; color: #808080">月销售量(个)</span><br />
                        <span style="font-size: 30px; font-weight: bold; color: #ed2323;" id="Monthsalesvolume">0</span>
                    </div>
                    <div class="boxDid" style="position: relative;" id="MonthOrderNumDiv">
                        <i class="up arrow" style="position: relative; top: 20px; left: -40px; display: none"></i>
                        <i class="down arrow" style="position: relative; top: 34px; left: -53px; display: none"></i>
                        <span style="position: relative; left: -30px; bottom: -35px;" id="MonthOrderNumText"></span>
                    </div>
                </td>
                <td><span style="font-weight: lighter; font-size: 15px; color: #808080">准点率</span><br />
                    <br style="line-height: 5px;" />
                    <span style="font-size: 30px; font-weight: bold; color: #2E8B57;" id="PunctualityRate">0</span></td>
                <td><span style="font-weight: lighter; font-size: 15px; color: #808080">客户总数</span><br />
                    <br style="line-height: 5px;" />
                    <span style="font-size: 30px; font-weight: bold; color: #1cdb14;" id="ClientNum">0</span></td>
            </tr>
        </table>
    </div>
    <div style="height: 100%">
        <div id="ChartStyle" style="display: none" class="CharTable">
            <div class="Chart">
                <ul style="height: 100%">
                    <li>
                        <div class="divChart">
                            <canvas id="Bar" style="display: none;"></canvas>
                        </div>
                        <div class="divChart">
                            <canvas id="Bar2" style="display: none;"></canvas>
                        </div>
                        <div class="divChart">
                            <canvas id="ComboChart" style="display: none;"></canvas>
                        </div>
                    </li>
                    <li>
                        <div class="divChart">
                            <canvas id="DailyBar" style="display: none;"></canvas>
                        </div>
                        <div class="divChart">
                            <canvas id="ProvincialBar" style="display: none;"></canvas>
                        </div>
                        <div class="divChart">
                            <canvas id="BrandBar" style="display: none;"></canvas>
                        </div>
                    </li>
                    <li class="dg">
                        <div class="dgChar">
                            <canvas id="ProvincePie" style="display: none;"></canvas>
                        </div>
                        <div class="dgChar">
                            <canvas id="Pie" style="display: none;"></canvas>
                        </div>
                        <div class="dgChar">
                            <canvas id="TyreTypeChart" style="display: none;"></canvas>
                        </div>
                    </li>
                    <li>
                        <div style="width: 49%; height: 100%;" class="divChart">
                            <canvas id="MonthlySalesStatistics" style="display: none;"></canvas>
                        </div>
                        <div style="width: 49%; height: 100%" class="divChart">
                            <canvas id="MonthlyMoneyStatistics" style="display: none;"></canvas>
                        </div>
                    </li>
                    <li class="dg">
                        <div>
                            <table id="dgTop20onsale">
                            </table>
                        </div>
                        <div>
                            <table id="dgPinCountdown20">
                            </table>
                        </div>
                    </li>

                </ul>
            </div>
        </div>
        <%--//财务统计--%>
        <div style="display: none" class="CharTable">
            <div class="Chart">
                <ul style="height: 100%">
                </ul>
            </div>
        </div>
        <%--//运营统计--%>
        <div style="display: none" class="CharTable Operate">
            <div class="Chart">
                <ul style="height: 100%">
                    <li>
                        <div class="OperateDg">
                            <table id="dgPunctualityRate" width="100%">
                            </table>

                        </div>
                        <div class="OperateDg">
                            <table id="dgShopPunctualityRate" width="100%">
                            </table>
                        </div>
                    </li>
                    <li>
                        <div class="OperateDg">
                            <canvas id="ProvincePunctualityRate" style="display: none;"></canvas>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
        <%--//采购统计--%>
        <div style="display: none" class="CharTable">
            <div class="Chart">
                <ul style="height: 100%">
                </ul>
            </div>
        </div>
    </div>

    <script type="text/javascript">

        var CharTable = $(".CharTable")
        $(CharTable[0]).css("display", "")

        Chart.register(ChartDataLabels);



        var MonthlyMoneyStatistics = document.getElementById('MonthlyMoneyStatistics');
        var MonthlyMoneyStatisticsChart = new Chart(MonthlyMoneyStatistics, {
            data: {
                labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
                datasets: [{
                    label: 'Monthly Sales',
                    data: [10, 45, 49, 78, 65, 34],
                    type: 'line',
                    fill: false,
                    yAxisID: "y_axis_1"
                }, {
                    label: 'Monthly Money',
                    data: [12, 95, 45, 78, 96, 31],
                    type: 'bar',
                    yAxisID: "y_axis_2"
                }]
            },
            options: {
                scales: {
                    y_axis_2: {
                        position: 'right',
                        ticks: {
                            callback: function (value, index, ticks) {
                                return value + '%';
                            }
                        },
                        //min: -50
                    },
                    y_axis_1: {
                        position: 'left',
                    }
                },
                plugins: {
                    datalabels: {
                        color: 'Black',
                        formatter: function (value, context) {
                            if (context.datasetIndex == 1) {
                                return value + '%'; // 将值乘以100并转换为百分比形式
                            }

                        },
                        font: {
                            weight: 'bold',
                            size: 15,
                        },
                        anchor: 'end', // 设置标签的锚点位置（位置相对于数据点）
                        align: 'top', // 设置标签的对齐位置（相对于锚点）
                        //formatter: function (value) {
                        //    return value;
                        //}
                    },
                    legend: {
                        position: 'top',
                    },
                    title: {
                        display: false,
                        text: '每月销售额统计',
                        font: {
                            size: 15
                        }
                    }

                },

            }
        });


        var MonthlySalesStatistics = document.getElementById('MonthlySalesStatistics');
        var MonthlySalesStatisticsChart = new Chart(MonthlySalesStatistics, {
            data: {
                //labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
                datasets: [{
                    label: 'Monthly Sales',
                    data: [10, 45, 49, 78, 65, 34],
                    type: 'line',
                    fill: false,
                    yAxisID: "y_axis_1"
                }, {
                    label: 'Monthly Money',
                    data: [12, 95, 45, 78, 96, 31],
                    type: 'bar',
                    yAxisID: "y_axis_2"
                }]
            },
            options: {
                scales: {
                    y_axis_2: {
                        position: 'right',
                        ticks: {
                            callback: function (value, index, ticks) {
                                return value + '%';
                            }
                        },

                    },
                    y_axis_1: {
                        position: 'left',
                    }
                },
                plugins: {
                    datalabels: {
                        color: 'Black',
                        formatter: function (value, context) {
                            if (context.datasetIndex == 1) {
                                return value + '%'; // 将值乘以100并转换为百分比形式
                            }

                        },
                        font: {
                            weight: 'bold',
                            size: 15,
                        },
                        anchor: 'end', // 设置标签的锚点位置（位置相对于数据点）
                        align: 'top', // 设置标签的对齐位置（相对于锚点）
                        //formatter: function (value) {
                        //    return value;
                        //}
                    },
                    legend: {
                        position: 'top',
                    },
                    title: {
                        display: false,
                        text: '每月销售量统计',
                        font: {
                            size: 15
                        }
                    }

                },

            }
        });









        var ProvincePunctualityRate = document.getElementById('ProvincePunctualityRate');
        var ProvincePunctualityRateChart = new Chart(ProvincePunctualityRate, {
            data: {
                //labels: newDataset.labels,
                datasets: []
            },
            options: {
                scales: {
                    y: {
                        ticks: {
                            callback: function (value, index, values) {
                                return value + '%';
                            }
                        }
                    }
                },
                plugins: {
                    datalabels: {
                        color: 'Black',
                        font: {
                            weight: 'bold',
                            size: 15,
                        },
                        //anchor: 'end', // 设置标签的锚点位置（位置相对于数据点）
                        //align: 'top', // 设置标签的对齐位置（相对于锚点）
                        formatter: function (value) {
                            return value + "%";
                        }
                    },
                    legend: {
                        position: 'top',
                    },
                    title: {
                        display: false,
                        text: '省份准点率统计',
                        font: {
                            size: 15
                        }
                    }

                },
            }
        });



        var TyreType = document.getElementById('TyreTypeChart');
        var TyreTypeChart = new Chart(TyreType, {
            data: {
                datasets: []
            },
            options: {
                scales: {
                    y_axis_2: {
                        position: 'right',
                        ticks: {
                            callback: function (value, index, ticks) {
                                return value + '%';
                            }
                        },
                        min: 0,
                        max: 100
                    },
                    y_axis_1: {
                        position: 'left',
                        ticks: {
                            stepSize: 500 // 设置步长为10
                        },
                    }
                },
                plugins: {
                    datalabels: {
                        color: 'Black',
                        formatter: function (value, context) {
                            if (context.datasetIndex == 1) {
                                return value + '%'; // 将值乘以100并转换为百分比形式
                            }

                        },
                        font: {
                            weight: 'bold',
                            size: 15,
                        },
                        anchor: 'end', // 设置标签的锚点位置（位置相对于数据点）
                        align: 'top', // 设置标签的对齐位置（相对于锚点）
                        //formatter: function (value) {
                        //    return value;
                        //}
                    },
                    legend: {
                        position: 'top',
                    },
                    title: {
                        display: false,
                        text: '轮胎类型统计',
                        font: {
                            size: 15
                        }
                    }

                },
            }
        });




        var ProvincePie = document.getElementById('ProvincePie');
        // 创建饼图
        var ProvincePieChart = new Chart(ProvincePie, {
            type: 'pie',
            data: {
                //labels: ['1', '2', '3'],
                datasets: [{
                    //data: [20, 30, 50],
                    //backgroundColor: ['#FF0000', '#0000FF', '#FFFF00']
                }]
            },
            options: {
                plugins: {
                    legend: {
                        position: 'right',
                    },
                    datalabels: {
                        display: true, // 确保设置为 true 来显示数据标签
                        formatter: function (value, context) {
                            return value + '%'; // 将值乘以100并转换为百分比形式
                        },
                        color: 'Black',
                        font: {
                            weight: 'bold',
                            size: 15,
                        },
                        anchor: 'end', // 设置标签的锚点位置（位置相对于数据点）
                        //align: 'top', // 设置标签的对齐位置（相对于锚点）
                    },
                    title: {
                        display: true,
                        text: '省份销售占比统计',
                        align: 'center',
                        font: {
                            size: 15
                        }
                    }
                }
            }
        });



        var ProvincialBar = document.getElementById('ProvincialBar');
        var ProvincialBarChar = new Chart(ProvincialBar, {
            data: {
                //labels: newDataset.labels,
                datasets: [{
                    type: 'bar', // 指定第一个数据集为柱图
                    //label: '每日销量',
                    //data: newDataset.data,
                    borderWidth: 1
                },]
            },
            options: {
                plugins: {
                    datalabels: {
                        color: 'Black',
                        font: {
                            weight: 'bold',
                            size: 15,
                        },
                        //anchor: 'end', // 设置标签的锚点位置（位置相对于数据点）
                        //align: 'top', // 设置标签的对齐位置（相对于锚点）
                        formatter: function (value) {
                            return value;
                        }
                    },
                    legend: {
                        position: 'top',
                    },
                    title: {
                        display: false,
                        text: '省份销售统计',
                        font: {
                            size: 15
                        }
                    }

                },
            }
        });







        var DailyBar = document.getElementById('DailyBar');
        var DailyBarChar = new Chart(DailyBar, {
            data: {
                type: 'bar',
                //labels: newDataset.labels,
                datasets: [{
                    type: 'bar', // 指定第一个数据集为柱图
                    //label: '每日销量',
                    //data: newDataset.data,
                    borderWidth: 1
                }, {
                    type: 'bar', // 指定第一个数据集为柱图
                    //label: '每日销量',
                    //data: newDataset.data,
                    borderWidth: 1
                }]
            },
            options: {
                plugins: {
                    datalabels: {
                        display: function (context) {
                            if (context.datasetIndex == 0) {
                                return true
                            }
                            else {
                                return false
                            }
                        },
                        color: 'Black',
                        font: {
                            weight: 'bold',
                            size: 15,
                        },
                        //anchor: 'end', // 设置标签的锚点位置（位置相对于数据点）
                        //align: 'top', // 设置标签的对齐位置（相对于锚点）
                        formatter: function (value) {
                            return value;
                        }
                    },
                    legend: {
                        position: 'top',
                    },
                    title: {
                        display: false,
                        text: '每日销售统计',
                        font: {
                            size: 15
                        }
                    }

                },
            }
        });
        var BrandBar = document.getElementById('BrandBar');

        var BrandBarChart = new Chart(BrandBar, {
            data: {
                //labels: newDataset.labels,
                datasets: []

            },
            options: {
                scales: {
                    y_axis_2: {
                        position: 'right',
                        ticks: {
                            callback: function (value, index, ticks) {
                                return value + '%';
                            }
                        },
                    },
                    y_axis_1: {
                        position: 'left',
                    }
                },
                plugins: {
                    legend: {
                        position: 'top',
                    },
                    datalabels: {
                        display: true, // 确保设置为 true 来显示数据标签
                        formatter: function (value, context) {
                            if (context.datasetIndex == 1) {
                                return value + '%'; // 将值乘以100并转换为百分比形式
                            }

                        },
                        color: 'Black',
                        font: {
                            weight: 'bold',
                            size: 15,
                        },
                        //anchor: 'end', // 设置标签的锚点位置（位置相对于数据点）
                        //align: 'top', // 设置标签的对齐位置（相对于锚点）
                    },
                    title: {
                        display: true,
                        text: '品牌销售统计',
                        font: {
                            size: 15
                        }
                    }
                }
            }
        });


        // 获取 canvas 元素
        var ctx = document.getElementById('Pie');
        // 定义数据
        var data = {
            labels: ['1', '2', '3'],
            datasets: [{
                data: [20, 30, 50],
                //backgroundColor: ['#FF0000', '#0000FF', '#FFFF00']
            }]
        };

        // 创建饼图
        var myChart = new Chart(ctx, {
            type: 'pie',
            data: data,
            options: {
                plugins: {
                    legend: {
                        position: 'right',

                    },
                    datalabels: {
                        display: true, // 确保设置为 true 来显示数据标签
                        formatter: function (value, context) {
                            return value + '%'; // 将值乘以100并转换为百分比形式
                        },
                        color: 'Black',
                        font: {
                            weight: 'bold',
                            size: 15,
                        },
                        //anchor: 'end', // 设置标签的锚点位置（位置相对于数据点）
                        //align: 'top', // 设置标签的对齐位置（相对于锚点）
                    },
                    title: {
                        display: true,
                        text: '品牌销售占比统计',
                        align: 'center',
                        font: {
                            size: 15
                        }
                        //position: 'right',
                    },

                },



            }
        });


        var Bar = document.getElementById('Bar');




        // 创建柱状
        var myChart1 = new Chart(Bar, {
            type: 'bar',
            data: {
                //labels: ['红色', '蓝色', '黄色'],
                datasets: []
            },
            options: {
                scales: {
                    y_axis_2: {
                        position: 'right',
                        ticks: {
                            callback: function (value, index, ticks) {
                                return value + '%';
                            }
                        },
                    },
                    y_axis_1: {
                        position: 'left',
                    }
                },
                plugins: {
                    legend: {
                        position: 'top',
                    },
                    datalabels: {
                        display: true, // 确保设置为 true 来显示数据标签
                        formatter: function (value, context) {
                            if (context.datasetIndex == 2) {
                                return value + '%'; // 将值乘以100并转换为百分比形式
                            }

                        },
                        color: 'Black',
                        font: {
                            weight: 'bold',
                            size: 15,
                        },
                        //anchor: 'end', // 设置标签的锚点位置（位置相对于数据点）
                        //align: 'top', // 设置标签的对齐位置（相对于锚点）
                    },
                    title: {
                        display: true,
                        text: '周销售统计',
                        font: {
                            size: 15
                        }
                    }
                }
            }
        });

        var Bar2 = document.getElementById('Bar2');

        // 定义数据


        // 创建柱状
        var myChart2 = new Chart(Bar2, {
            type: 'bar',
            data: {
                //labels: ['红色', '蓝色', '黄色'],
                datasets: [{
                    //label: '柱图',
                    //data: [20, 30, 50],
                    //backgroundColor: ['#FF0000', '#0000FF', '#FFFF00']
                }]
            },
            options: {
                plugins: {
                    legend: {
                        position: 'top',
                    },
                    datalabels: {
                        display: true, // 确保设置为 true 来显示数据标签
                        color: 'Black',
                        font: {
                            weight: 'bold',
                            size: 15,
                        },
                        //anchor: 'end', // 设置标签的锚点位置（位置相对于数据点）
                        //align: 'top', // 设置标签的对齐位置（相对于锚点）
                    },
                    title: {
                        display: true,
                        text: '销售车型统计',
                        font: {
                            size: 15
                        }
                    }
                }
            }
        });


        // 获取 canvas 元素
        var ComboChart = document.getElementById('ComboChart');

        // 定义数据

        // 创建组合图
        var myCharts = new Chart(ComboChart, {
            data: {
                datasets: []
            },
            options: {
                scales: {
                    y_axis_2: {
                        position: 'right',
                        ticks: {
                            callback: function (value, index, ticks) {
                                return value + '%';
                            }
                        },
                        max: 100,
                        min: 0,
                        gridLines: {
                            display: false
                        }
                    },
                    y_axis_1: {
                        position: 'left',
                        ticks: {
                            stepSize: 500 // 设置步长为10
                        },

                    }
                },
                plugins: {
                    title: {
                        display: true,
                        text: '销售区域统计',
                        font: {
                            size: 15
                        }
                    },
                    datalabels: {
                        display: true, // 确保设置为 true 来显示数据标签
                        formatter: function (value, context) {
                            if (context.datasetIndex == 1) {
                                return value + '%'; // 将值乘以100并转换为百分比形式
                            }

                        },

                        color: 'Black',
                        font: {
                            weight: 'bold',
                            size: 15,
                        },
                        anchor: 'end', // 设置标签的锚点位置（位置相对于数据点）
                        align: 'top', // 设置标签的对齐位置（相对于锚点）
                    },
                }

            }
        });


        var saveMessageUrl = "http://www.17sucai.com";//提交留言
        //Dialog icon base path
        function showInfoTip(content, type, onhide) {
            var icons = { success: 'success.png', info: 'info.png' };
            var icon = icons[type] || 'error.png';
            return ds.dialog({
                title: '消息提示',
                content: content,
                onhide: onhide,
                onyes: true,
                icon: icon,
                timeout: 3,
                width: 200
            });
        }


        function formatNewDate(date) {
            var year = date.getFullYear().toString();
            var month = (date.getMonth() + 1).toString().padStart(2, '0');
            var day = date.getDate().toString().padStart(2, '0');
            return year + "-" + month + "-" + day;
        }


        function getCurrentMonthStartAndEndDates(date) {
            // 获取当前月份的年份和月份
            const year = date.getFullYear();
            const month = date.getMonth();

            // 获取当前月份的第一天的日期对象
            const firstDayOfMonth = new Date(year, month, 1);

            // 获取当前月份的最后一天的日期对象（这里使用下个月的第 0 天来获取当前月份的最后一天）
            const lastDayOfMonth = new Date(year, month + 1, 0);

            return { startDate: firstDayOfMonth, endDate: lastDayOfMonth };
        }






        //创建DOM
        var
            quickHTML = '<div class="quick_links_panel"><div id="quick_links" class="quick_links"><a href="#top" class="return_top"><i class="top"></i><span>返回顶部</span></a><a href="#" class="" id="sale"><i class="view"></i><span>销售统计</span></a><a href="#" class="" id="operate"><i class="view"></i><span>运营统计</span></a><a href="#" class="" id="procure"><i class="view"></i><span>采购统计</span></a><a href="#" class="" id="finance"><i class="view"></i><span>财务统计</span></a><a href="#" class="leave_message"><i class="setting"></i><span>切换条件</span></a></div><div class="quick_toggle"><a href="javascript:;" class="toggle" title="展开/收起"></a></div></div><div id="quick_links_pop" class="quick_links_pop hide"></div>',
            quickShell = $(document.createElement('div')).html(quickHTML).addClass('quick_links_wrap'),
            quickLinks = quickShell.find('.quick_links');
        quickPanel = quickLinks.parent();
        quickShell.appendTo('body');
        var AppendHtml = '<form action="./" method="post"><div class= "types"></div><div class="txt"><div name="msg" id="msg">所属仓库:<input id="AHouseID" class="easyui-combobox" style="width: 150px;" panelheight="auto" /><br><br><br>查询时间:<input id="AStartDate" class="easyui-datebox" style="width: 100px" />~<input id="AEndDate" class="easyui-datebox" style="width: 100px" /></div></div><div class="token"><span id="code_img"></span></div><div class="btns"><button type="submit" class="btn"><span>确定</span></button></div></form > '

        //具体数据操作 
        var
            quickPopXHR,
            loadingTmpl = '<div class="loading" style="padding:30px 80px"><i></i><span>Loading...</span></div>',
            popTmpl = '<div class="title"><h3><i></i><\%=title\%></h3></div><div class="pop_panel"><\%=content\%></div><div class="arrow"><i></i></div><div class="fix_bg"></div>',

            quickPop = quickShell.find('#quick_links_pop'),
            quickDataFns = {
                leave_message: {
                    title: '查询条件',
                    content: AppendHtml,
                    init: function (ops) {
                        setTimeout(function () {
                        }, 100);


                        //效验 & 提交数据
                        //debugger;
                        var form = quickPop.find('form');
                        //form.attr("action", "reportApi.aspx?method=QuerySalesVolume");
                        form.bind('submit', function (e) {
                            e.preventDefault();
                            var data = form.serialize();
                            //if (!checkMessageForm()) {
                            //    return false;
                            //}
                            var type = quickPop.find(':radio:checked').val();
                            //debugger;
                            jQuery.ajax({
                                type: 'post',
                                url: "reportApi.aspx?method=Statisticalheader",
                                data: {
                                    StartDate: $('#AStartDate').datebox('getValue'),
                                    EndDate: $('#AEndDate').datebox('getValue'),
                                    HouseID: $('#AHouseID').combobox('getValue'),
                                },
                                dataType: "json",
                                error: function (value) {
                                    ds.dialog.alert('失败');
                                },
                                success: function (value) {
                                    $("#Weeklyorders").text(value.OrderNum);
                                    $("#Weeklysalesvolume").text(value.OrderPiece);
                                    $("#Monthlyorders").text(value.DNOrderNum);
                                    $("#Monthsalesvolume").text(value.DNOrderPiece);
                                    $("#ClientNum").text(value.ClientNum);
                                    $("#PunctualityRate").text((value.PunctualityRate * 100).toFixed(2) + "%");
                                    $("#OrderNumText").text("周单量:" + (parseFloat(value.OrderPiece) / parseFloat(value.OrderNum)).toFixed(0));
                                    $("#OrderNumDiv").css("display", "")
                                    $("#MonthOrderNumText").text("月单量:" + (parseFloat(value.DNOrderPiece) / parseFloat(value.DNOrderNum)).toFixed(0));
                                    $("#MonthOrderNumDiv").css("display", "")

                                    // 获取当前日期
                                    var today = new Date();
                                    const currentDate = new Date($('#AStartDate').datebox('getValue'));

                                    var firstDayOfMonth;
                                    var nextMonth;
                                    var firstDayOfNextMonth;
                                    var lastDayOfMonth;
                                    if (today.getMonth() == currentDate.getMonth()) {
                                        // 获取本月第一天
                                        firstDayOfMonth = new Date(today.getFullYear(), today.getMonth(), 1);
                                        // 获取下个月第一天
                                        nextMonth = (today.getMonth() === 11) ? 0 : today.getMonth() + 1;
                                         firstDayOfNextMonth = new Date(today.getFullYear(), nextMonth, 1);
                                        // 获取本月最后一天
                                        lastDayOfMonth = new Date(firstDayOfNextMonth.getTime() - 1);
                                    }
                                    else {
                                        const { startDate, endDate } = getCurrentMonthStartAndEndDates(currentDate);
                                        firstDayOfMonth = startDate;
                                        lastDayOfMonth = endDate
                                    }

                                    if (formatNewDate(firstDayOfMonth) == $('#AStartDate').datebox('getValue') && formatNewDate(lastDayOfMonth) == $('#AEndDate').datebox('getValue')) {
                                        $("#title").text($('#AHouseID').combobox('getText') + (firstDayOfMonth.getMonth() + 1) + "月月报")
                                    }
                                    else {
                                        $("#title").text($('#AHouseID').combobox('getText') + "第" + value.ReportDate + "周周报")
                                    }
                                    $("#DateTime").text("(" + $('#AStartDate').datebox('getValue') + "--" + $('#AEndDate').datebox('getValue') + ")");
                                    doQuery()
                                }
                            });
                            $.ajax({
                                type: 'post',
                                url: "reportApi.aspx?method=Statistics",
                                data: {
                                    StartDate: $('#AStartDate').datebox('getValue'),
                                    EndDate: $('#AEndDate').datebox('getValue'),
                                    HouseID: $('#AHouseID').combobox('getValue'),
                                },
                                success: function (data) {
                                    var newdata = eval("(" + data + ")")
                                    for (var i = 1; i <= myChart1.data.datasets.length; i++) {
                                        myChart1.data.datasets.pop();
                                    }
                                    myChart1.data.datasets.pop();
                                    myChart1.update();



                                    myChart2.data.datasets.pop();
                                    myChart2.update();

                                    for (var i = 1; i <= myCharts.data.datasets.length; i++) {
                                        myCharts.data.datasets.pop();
                                    }
                                    myCharts.data.datasets.pop();
                                    myCharts.update();

                                    BrandBarChart.data.datasets.pop();
                                    BrandBarChart.data.datasets.pop();
                                    BrandBarChart.update();

                                    myChart.data.datasets.pop();
                                    myChart.update();
                                    const newDataset = {
                                        labels: [],
                                        data: [],
                                        yAxisID: "y_axis_1"
                                    }
                                    const newDataset_2 = {
                                        labels: [],
                                        data: [],
                                        yAxisID: "y_axis_1"
                                    }
                                    const newDataset_3 = {
                                        labels: [],
                                        data: [],
                                        type: "line",
                                        yAxisID: "y_axis_2"
                                    }
                                    var sums = 0;
                                    for (var i = 0; i < newdata[0].length; i++) {
                                        sums += newdata[0][i].OrderPiece
                                    }
                                    //var date = $("#dgClientStatisHubDiameter").datagrid("getRows");
                                    for (var i = 0; i < newdata[0].length; i++) {
                                        if (newdata[0][i].OrderPiece != 0) {
                                            newDataset.labels.push(newdata[0][i].ReportDate + "周")
                                            newDataset.data.push(newdata[0][i].OrderPiece)
                                            newDataset_2.data.push(sums)
                                            newDataset_3.data.push((parseFloat(newdata[0][i].OrderPiece / sums) * 100).toFixed(2))
                                        }
                                    }
                                    myChart1.data.datasets.push(newDataset_2);
                                    myChart1.data.datasets.push(newDataset);

                                    myChart1.data.datasets.push(newDataset_3);
                                    myChart1.data.labels = newDataset.labels;
                                    myChart1.data.datasets[0].label = "月销量";
                                    myChart1.data.datasets[1].label = "周销量";
                                    myChart1.data.datasets[2].label = "占比";
                                    myChart1.options.plugins.title.display = true;
                                    // 重新绘制图表
                                    myChart1.update();
                                    $("#Bar").css("display", "")
                                    const newVehicleModelDataset = {
                                        labels: [],
                                        data: [],
                                    }
                                    for (var i = 0; i < newdata[1].length; i++) {
                                        newVehicleModelDataset.labels.push(newdata[1][i].CarModel)
                                        newVehicleModelDataset.data.push(newdata[1][i].OrderPiece)
                                    }
                                    myChart2.data.datasets.push(newVehicleModelDataset);
                                    myChart2.data.labels = newVehicleModelDataset.labels;
                                    myChart2.data.datasets[0].label = "销售量";
                                    myChart2.options.plugins.title.display = true;
                                    myChart2.update();
                                    $("#Bar2").css("display", "")
                                    const newRegionDataset = {
                                        labels: [],
                                        label: "销售量",
                                        data: [],
                                        type: "bar",
                                        yAxisID: "y_axis_1"
                                    }
                                    const newRegionDataset_2 = {
                                        labels: [],
                                        label: "环比增长率",
                                        data: [],
                                        type: "line",
                                        yAxisID: "y_axis_2"
                                    }
                                    const newRegionDataset_3 = {
                                        labels: [],
                                        label: "销售占比",
                                        data: [],
                                        type: "line",
                                        yAxisID: "y_axis_2"
                                    }
                                    const newRegionDataset_4 = {
                                        labels: [],
                                        label: "月销售量",
                                        data: [],
                                        type: "bar",
                                        yAxisID: "y_axis_1"
                                    }
                                    if (newdata.length > 4) {
                                        for (var i = 0; i < newdata[4].length; i++) {
                                            newRegionDataset.labels.push(newdata[4][i].Area)
                                            newRegionDataset.data.push(newdata[4][i].OrderPiece)
                                            //var newdata4sum = 0;
                                            //for (var u = 0; u < newdata[4].length; u++) {
                                            //    newdata4sum += newdata[4][u].OrderPiece;
                                            //}
                                            //newRegionDataset_3.data.push(parseFloat(newdata[4][i].OrderPiece / newdata4sum * 100).toFixed(2));
                                            //newRegionDataset_4.data.push(newdata4sum);
                                            if (newdata[4][i].DNOrderPiece == 0) {
                                                newRegionDataset_2.data.push(0)
                                            }
                                            else {
                                                newRegionDataset_2.data.push(((newdata[4][i].OrderPiece - newdata[4][i].DNOrderPiece) / newdata[4][i].DNOrderPiece * 100).toFixed(2))
                                            }
                                        }
                                        myCharts.data.datasets.push(newRegionDataset);
                                        myCharts.data.datasets.push(newRegionDataset_2);
                                    }
                                    else {

                                        var newdata4sum = 0;
                                        for (var u = 0; u < newdata[2].length; u++) {
                                            newdata4sum += newdata[2][u].OrderPiece;
                                        }
                                        for (var i = 0; i < newdata[2].length; i++) {
                                            newRegionDataset.labels.push(newdata[2][i].Area)
                                            newRegionDataset.data.push(newdata[2][i].OrderPiece)
                                            newRegionDataset_3.data.push(parseFloat(newdata[2][i].OrderPiece / newdata4sum * 100).toFixed(2));
                                            //newRegionDataset_4.data.push(newdata4sum);
                                        }
                                        myCharts.data.datasets.push(newRegionDataset);

                                    }

                                    myCharts.data.labels = newRegionDataset.labels;
                                    myCharts.data.datasets.push(newRegionDataset_3);
                                    //myCharts.data.datasets.push(newRegionDataset_4);
                                    myCharts.options.plugins.title.display = true;
                                    myCharts.update();
                                    $("#ComboChart").css("display", "")
                                    const newBrandDataset = {
                                        labels: [],
                                        data: [],
                                    }
                                    for (var i = 0; i < newdata[3].length; i++) {
                                        var s = 0;
                                        var sum = 0;
                                        while (s < newdata[3].length) {
                                            sum += newdata[3][s].OrderPiece
                                            s++;
                                        }
                                        newBrandDataset.labels.push(newdata[3][i].TypeName)
                                        newBrandDataset.data.push(((parseFloat(newdata[3][i].OrderPiece) / sum) * 100).toFixed(2))
                                    }
                                    myChart.data.datasets.push(newBrandDataset);
                                    myChart.data.labels = newBrandDataset.labels;
                                    myChart.data.datasets[0].label = "销售占比";
                                    myChart.options.plugins.title.display = true;
                                    myChart.update();
                                    $("#Pie").css("display", "")

                                    const BrandBar = {
                                        labels: [],
                                        data: [],
                                        label: "销售量",
                                        yAxisID: "y_axis_1",
                                        type: "bar",
                                    }
                                    const Brandline = {
                                        data: [],
                                        type: "line",
                                        label: "环比",
                                        yAxisID: "y_axis_2"
                                    }
                                    // 获取当前日期
                                    var today = new Date();
                                    const currentDate = new Date($('#AStartDate').datebox('getValue'));

                                    var firstDayOfMonth;
                                    var nextMonth;
                                    var firstDayOfNextMonth;
                                    var lastDayOfMonth;
                                    if (today.getMonth() == currentDate.getMonth()) {
                                        // 获取本月第一天
                                        firstDayOfMonth = new Date(today.getFullYear(), today.getMonth(), 1);
                                        // 获取下个月第一天
                                        nextMonth = (today.getMonth() === 11) ? 0 : today.getMonth() + 1;
                                        firstDayOfNextMonth = new Date(today.getFullYear(), nextMonth, 1);
                                        // 获取本月最后一天
                                        lastDayOfMonth = new Date(firstDayOfNextMonth.getTime() - 1);
                                    }
                                    else {
                                        const { startDate, endDate } = getCurrentMonthStartAndEndDates(currentDate);
                                        firstDayOfMonth = startDate;
                                        lastDayOfMonth = endDate
                                    }
                                    if (formatNewDate(firstDayOfMonth) == $('#AStartDate').datebox('getValue') && formatNewDate(lastDayOfMonth) == $('#AEndDate').datebox('getValue')) {
                                        for (var i = 0; i < newdata[3].length; i++) {
                                            BrandBar.labels.push(newdata[3][i].TypeName)
                                            BrandBar.data.push(newdata[3][i].OrderPiece)
                                            Brandline.data.push(parseFloat(((newdata[3][i].OrderPiece - newdata[3][i].QYOrderPiece)) / parseFloat(newdata[3][i].QYOrderPiece == 0 ? newdata[3][i].OrderPiece : newdata[3][i].QYOrderPiece) * 100).toFixed(2));
                                        }
                                        BrandBarChart.data.datasets.push(BrandBar);
                                        BrandBarChart.data.datasets.push(Brandline);
                                    }
                                    else {
                                        for (var i = 0; i < newdata[3].length; i++) {
                                            BrandBar.labels.push(newdata[3][i].TypeName)
                                            BrandBar.data.push(newdata[3][i].OrderPiece)
                                        }
                                        BrandBarChart.data.datasets.push(BrandBar);
                                    }
                                    console.log(Brandline);
                                    BrandBarChart.data.labels = BrandBar.labels;
                                    BrandBarChart.options.plugins.title.display = true;
                                    BrandBarChart.update();
                                    $("#BrandBar").css("display", "")

                                }
                            })
                            $.ajax({
                                url: 'reportApi.aspx?method=QueryStatisticalChart',
                                data: {
                                    HouseID: $('#AHouseID').combobox('getValue'),
                                    StartDate: $('#AStartDate').datebox('getValue'),
                                    EndDate: $('#AEndDate').datebox('getValue'),
                                },
                                type: 'post',
                                success: function (text) {
                                    DailyBarChar.data.datasets.pop();
                                    DailyBarChar.data.datasets.pop();
                                    DailyBarChar.update();
                                    const newDataset = {
                                        labels: [],
                                        data: [],
                                    }
                                    var data = eval("(" + text + ")")
                                    for (var i = 0; i < data.length; i++) {
                                        newDataset.labels.push(data[i].ReportDate)
                                        newDataset.data.push(data[i].OrderPiece)
                                    }
                                    const datasets0 = {
                                        label: "销量",
                                        type: "bar",
                                        data: newDataset.data,

                                    }
                                    const datasets1 = {
                                        label: "销量变化",
                                        type: "line",
                                        data: newDataset.data,

                                    }
                                    DailyBarChar.data.datasets.push(datasets0);
                                    DailyBarChar.data.datasets.push(datasets1);
                                    DailyBarChar.data.labels = newDataset.labels;
                                    DailyBarChar.options.plugins.title.display = true;
                                    $("#DailyBar").css("display", "")
                                    DailyBarChar.update();
                                }

                            })

                            $.ajax({
                                url: 'reportApi.aspx?method=QueryProvincialStatistics',
                                data: {
                                    StartDate: $('#AStartDate').datebox('getValue'),
                                    EndDate: $('#AEndDate').datebox('getValue'),
                                    HouseID: $('#AHouseID').combobox('getValue'),
                                },
                                type: 'post',
                                success: function (data) {
                                    var newdata = eval("(" + data + ")")

                                    ProvincePieChart.data.datasets.pop();
                                    ProvincePieChart.update();

                                    ProvincialBarChar.data.datasets.pop();
                                    ProvincialBarChar.update();
                                    const newDataset = {
                                        labels: [],
                                        data: [],
                                    }
                                    const newProvincePie = {
                                        data: [],
                                    }
                                    for (var i = 0; i < newdata.length; i++) {
                                        var s = 0;
                                        var sum = 0;
                                        while (s < newdata.length) {
                                            sum += newdata[s].OrderPiece
                                            s++;
                                        }

                                        newProvincePie.data.push(((parseFloat(newdata[i].OrderPiece) / sum) * 100).toFixed(2))

                                        newDataset.labels.push(newdata[i].Province)
                                        newDataset.data.push(newdata[i].OrderPiece)
                                    }
                                    const datasets0 = {
                                        label: "销售量",
                                        type: "bar",
                                        data: newDataset.data,
                                    }
                                    ProvincialBarChar.data.datasets.push(datasets0);
                                    ProvincialBarChar.data.labels = newDataset.labels;
                                    ProvincialBarChar.options.plugins.title.display = true;
                                    $("#ProvincialBar").css("display", "")
                                    ProvincialBarChar.update();


                                    ProvincePieChart.data.datasets.push(newProvincePie);
                                    ProvincePieChart.data.labels = newDataset.labels;
                                    ProvincePieChart.options.plugins.title.display = true;
                                    $("#ProvincePie").css("display", "")
                                    ProvincePieChart.update();


                                }
                            });
                            $.ajax({
                                url: 'reportApi.aspx?method=QuryTyreType',
                                data: {
                                    StartDate: $('#AStartDate').datebox('getValue'),
                                    EndDate: $('#AEndDate').datebox('getValue'),
                                    HouseID: $('#AHouseID').combobox('getValue'),
                                },
                                type: 'post',
                                success: function (data) {
                                    var newdata = eval("(" + data + ")")
                                    for (var i = 1; i <= TyreTypeChart.data.datasets.length; i++) {
                                        TyreTypeChart.data.datasets.pop();
                                    }
                                    TyreTypeChart.data.datasets.pop();
                                    TyreTypeChart.update();
                                    const datasets0 = {
                                        label: "销量",
                                        type: "bar",
                                        yAxisID: "y_axis_1",
                                        data: [],
                                    }
                                    const datasets1 = {
                                        label: "销售占比",
                                        type: "bar",
                                        yAxisID: "y_axis_2",
                                        data: [],

                                    }
                                    var labels = [];
                                    var sum = 0
                                    for (var t = 0; t < newdata.length; t++) {
                                        sum += newdata[t].OrderPiece
                                    }
                                    for (var i = 0; i < newdata.length; i++) {
                                        labels.push(newdata[i].TyreType == 0 ? "四季胎" : "冬季胎")
                                        datasets0.data.push(newdata[i].OrderPiece)
                                        datasets1.data.push(parseFloat(newdata[i].OrderPiece / sum * 100).toFixed(2))
                                    }
                                    TyreTypeChart.data.datasets.push(datasets0);
                                    TyreTypeChart.data.datasets.push(datasets1);
                                    TyreTypeChart.data.labels = labels;
                                    TyreTypeChart.options.plugins.title.display = true;
                                    $("#TyreTypeChart").css("display", "")
                                    TyreTypeChart.update();


                                }
                            });

                            $.ajax({
                                url: 'reportApi.aspx?method=ProvincePunctualityRate',
                                data: {
                                    StartDate: $('#AStartDate').datebox('getValue'),
                                    EndDate: $('#AEndDate').datebox('getValue'),
                                    HouseID: $('#AHouseID').combobox('getValue'),
                                },
                                type: 'post',
                                success: function (data) {
                                    var newdata = eval("(" + data + ")")
                                    ProvincePunctualityRateChart.data.datasets.pop();
                                    ProvincePunctualityRateChart.update();
                                    const datasets0 = {
                                        label: "准点率",
                                        type: "line",
                                        data: [],
                                    }
                                    var labels = [];
                                    for (var i = 0; i < newdata.length; i++) {
                                        datasets0.data.push((newdata[i].PunctualityRate * 100).toFixed(2))
                                        labels.push(newdata[i].Province)
                                    }
                                    ProvincePunctualityRateChart.data.datasets.push(datasets0);
                                    ProvincePunctualityRateChart.data.labels = labels;
                                    ProvincePunctualityRateChart.options.plugins.title.display = true;
                                    $("#ProvincePunctualityRate").css("display", "")
                                    ProvincePunctualityRateChart.update();

                                }
                            })

                            $.ajax({
                                url: 'reportApi.aspx?method=StatisticalAmountSalesvolume&HouseID=' + $('#AHouseID').combobox('getValue'),
                                type: 'get',
                                success: function (data) {
                                    var newdata = eval("(" + data + ")")
                                    console.log(newdata)
                                    MonthlySalesStatisticsChart.data.datasets.pop();
                                    MonthlySalesStatisticsChart.data.datasets.pop();
                                    MonthlySalesStatisticsChart.update();

                                    MonthlyMoneyStatisticsChart.data.datasets.pop();
                                    MonthlyMoneyStatisticsChart.data.datasets.pop();
                                    MonthlyMoneyStatisticsChart.update();


                                    const newMoneyBar = {
                                        labels: [],
                                        data: [],
                                        yAxisID: "y_axis_1",
                                        label: "金额",
                                        type: "bar"
                                    }
                                    const newMoneyline = {
                                        label: "环比",
                                        data: [],
                                        yAxisID: "y_axis_2",
                                        type: "line"
                                    }
                                    for (var i = 0; i < newdata[0].length; i++) {
                                        newMoneyBar.labels.push(newdata[0][i].DeadTime)
                                        newMoneyBar.data.push(newdata[0][i].OrderCharge)
                                        if (i == 0) {
                                            newMoneyline.data.push(0)
                                        }
                                        else {
                                            newMoneyline.data.push(parseFloat((newdata[0][i].OrderCharge - newdata[0][i - 1].OrderCharge) / newdata[0][i - 1].OrderCharge * 100).toFixed(2))
                                        }
                                    }

                                    MonthlyMoneyStatisticsChart.data.datasets.push(newMoneyBar);
                                    MonthlyMoneyStatisticsChart.data.datasets.push(newMoneyline);
                                    MonthlyMoneyStatisticsChart.data.labels = newMoneyBar.labels;
                                    MonthlyMoneyStatisticsChart.options.plugins.title.display = true;
                                    $("#MonthlyMoneyStatistics").css("display", "")
                                    MonthlyMoneyStatisticsChart.update();


                                    const newSalesBar = {
                                        labels: [],
                                        data: [],
                                        yAxisID: "y_axis_1",
                                        label: "销量",
                                        type: "bar"
                                    }
                                    const newSalesline = {
                                        label: "环比",
                                        data: [],
                                        yAxisID: "y_axis_2",
                                        type: "line"
                                    }


                                    for (var i = 0; i < newdata[1].length; i++) {
                                        newSalesBar.labels.push(newdata[1][i].DeadTime)
                                        newSalesBar.data.push(newdata[1][i].OrderPiece)
                                        if (i == 0) {
                                            newSalesline.data.push(0)
                                        }
                                        else {
                                            newSalesline.data.push(parseFloat((newdata[1][i].OrderPiece - newdata[1][i - 1].OrderPiece) / newdata[1][i - 1].OrderPiece * 100).toFixed(2))
                                        }
                                    }

                                    MonthlySalesStatisticsChart.data.datasets.push(newSalesBar);
                                    MonthlySalesStatisticsChart.data.datasets.push(newSalesline);
                                    MonthlySalesStatisticsChart.data.labels = newSalesBar.labels;
                                    MonthlySalesStatisticsChart.options.plugins.title.display = true;
                                    $("#MonthlySalesStatistics").css("display", "")
                                    MonthlySalesStatisticsChart.update();



                                }
                            })
                        });
                    }
                }
            };

        //showQuickPop
        var
            prevPopType,
            prevTrigger,
            doc = $(document),
            popDisplayed = false,

            hideQuickPop = function (e) {
                //debugger;
                if (e == 1) {
                    var StartDate = $('#AStartDate').datebox('getValue');
                    if (prevTrigger) {
                        prevTrigger.removeClass('current');
                    }
                    prevPopType = '';
                    popDisplayed = false;
                    quickPop.hide();
                }
                //alert(popDisplayed)


            },
            showQuickPop = function (type) {
                if (quickPopXHR && quickPopXHR.abort) {
                    quickPopXHR.abort();
                }
                if (type !== prevPopType) {
                    var fn = quickDataFns[type];
                    quickPop.html(ds.tmpl(popTmpl, fn));
                    fn.init.call(this, fn);
                }
                //debugger;
                doc.unbind('click.quick_links').one('click.quick_links', hideQuickPop);

                quickPop[0].className = 'quick_links_pop quick_' + type;

                popDisplayed = true;
                prevPopType = type;
                quickPop.show();
                var $textbox = $('#AHouseID');
                $textbox.combobox();
                var $textbox = $('#AEndDate');
                $textbox.datebox();
                var $textbox = $('#AStartDate');
                $textbox.datebox();
                var datenow = new Date();
                $('#AStartDate').datebox('setValue', getNowFormatDate(datenow.getFullYear().toString() + "-" + (datenow.getMonth() + 1).toString() + "-01"));
                $('#AEndDate').datebox('setValue', getNowFormatDate(datenow));
                $('#AHouseID').combobox({
                    url: '../House/houseApi.aspx?method=CargoPermisionHouse', valueField: 'HouseID', textField: 'Name'
                });
                $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            };
        quickShell.bind('click.quick_links', function (e) {
            e.stopPropagation();
        });

        //通用事件处理
        var
            view = $(window),
            quickLinkCollapsed = !!ds.getCookie('ql_collapse'),
            getHandlerType = function (className) {
                return className.replace(/current/g, '').replace(/\s+/, '');
            },
            showPopFn = function () {
                //debugger;
                var type = getHandlerType(this.className);
                if (popDisplayed && type === prevPopType) {
                    //debugger;
                    return hideQuickPop(1);
                }
                showQuickPop(this.className);
                if (prevTrigger) {
                    prevTrigger.removeClass('current');
                }
                prevTrigger = $(this).addClass('current');
            },
            quickHandlers = {
                //购物车，最近浏览，商品咨询
                my_qlinks: showPopFn,
                message_list: showPopFn,
                history_list: showPopFn,
                leave_message: showPopFn,
                //返回顶部
                return_top: function () {
                    ds.scrollTo(0, 0);
                    hideReturnTop();
                },
                toggle: function () {
                    quickLinkCollapsed = !quickLinkCollapsed;

                    quickShell[quickLinkCollapsed ? 'addClass' : 'removeClass']('quick_links_min');
                    ds.setCookie('ql_collapse', quickLinkCollapsed ? '1' : '', 30);
                }
            };
        quickShell.delegate('a', 'click', function (e) {
            var type = getHandlerType(this.className);
            if (type && quickHandlers[type]) {
                quickHandlers[type].call(this);
                e.preventDefault();
            }
        });


        //Return top
        var scrollTimer, resizeTimer, minWidth = 1350;

        function resizeHandler() {
            clearTimeout(scrollTimer);
            scrollTimer = setTimeout(checkScroll, 160);
        }
        function checkResize() {
            quickShell[view.width() > 1340 ? 'removeClass' : 'addClass']('quick_links_dockright');
        }
        function scrollHandler() {
            clearTimeout(resizeTimer);
            resizeTimer = setTimeout(checkResize, 160);
        }
        function checkScroll() {
            view.scrollTop() > 100 ? showReturnTop() : hideReturnTop();
        }
        function showReturnTop() {
            quickPanel.addClass('quick_links_allow_gotop');
        }
        function hideReturnTop() {
            quickPanel.removeClass('quick_links_allow_gotop');
        }

        view.bind('scroll.go_top', resizeHandler).bind('resize.quick_links', scrollHandler);
        quickLinkCollapsed && quickShell.addClass('quick_links_min');
        resizeHandler();
        scrollHandler();

        //销售统计
        $("#sale").click(function () {
            for (var i = 0; i < CharTable.length; i++) {
                $(CharTable[i]).css("display", "none")
            }
            $(CharTable[0]).css("display", "")
            RELOAD()
        })
        //财务统计
        $("#finance").click(function () {
            for (var i = 0; i < CharTable.length; i++) {
                $(CharTable[i]).css("display", "none")
            }
            $(CharTable[1]).css("display", "")
            RELOAD()
        })
        //运营统计
        $("#operate").click(function () {
            for (var i = 0; i < CharTable.length; i++) {
                $(CharTable[i]).css("display", "none")
            }
            $(CharTable[2]).css("display", "")
            RELOAD()
        })
        //采购统计
        $("#procure").click(function () {
            for (var i = 0; i < CharTable.length; i++) {
                $(CharTable[i]).css("display", "none")
            }
            $(CharTable[3]).css("display", "")
            RELOAD()
        })

</script>
</asp:Content>
