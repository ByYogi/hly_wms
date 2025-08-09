<%@ Page Title="全统计分析" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reportDLTDataStatis.aspx.cs" Inherits="Cargo.Report.reportDLTDataStatis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        ul {
            margin: 0px;
            padding: 0px;
        }

        #eChartImg li {
            list-style: none;
            float: left;
            /*width: 30%;*/
            height: 100%;
            margin: 0px;
            padding: 0px;
        }

            #eChartImg li canvas {
                /*border:1px solid red;*/
                width: 100%;
                /*//height:100%;*/
                /*margin-left:10px;*/
            }
    </style>
    <script src="../JS/EChart/chart.umd.js" type="text/javascript"></script>
    <%--<script src="../JS/EChart/Char.min.js" type="text/javascript"></script>--%>
    <script src="../JS/EChart/chartjs-plugin-datalabels.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        //页面加载显示遮罩层
        var pc;
        $.parser.onComplete = function () {
            if (pc) {
                clearTimeout(pc);
            }
            pc = setTimeout(closemask, 10);
        }
        //关闭加载中遮罩层
        function closemask() {
            $("#Loading").fadeOut("normal", function () {
                $(this).remove();
            });
        }
        //页面加载时执行
        window.onload = function () { adjustment(); }
        $(window).resize(function () { adjustment(); });
        function adjustment() {
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 350;
            //alert(Number($(window).height()))
            //alert($("div[name='SelectDiv1']").outerHeight(true))

            $('#dgClientStatis').datagrid({ height: height });
            $('#dgUrbanStatistics').datagrid({ height: height - 150 });
            //$("#Chart1").css("height", height)
        }
        var StatisticalChart;
        var Statistical;
        var regionPie;
        var regionPieChart;
        $(document).ready(function () {
            setDatagrid();
            var datenow = new Date();
            $('#AStartDate').datebox('setValue', getNowFormatDate(datenow.getFullYear().toString() + "-" + (datenow.getMonth() + 1).toString() + "-01"));
            $('#AEndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#DStartDate').datebox('setValue', getNowFormatDate(datenow.getFullYear().toString() + "-" + (datenow.getMonth() + 1).toString() + "-01"));
            $('#DEndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#CStartDate').datebox('setValue', getNowFormatDate(datenow.getFullYear().toString() + "-" + (datenow.getMonth() + 1).toString() + "-01"));
            $('#CEndDate').datebox('setValue', getNowFormatDate(datenow));

            //一级产品
            $('#APID').combobox({
                url: '../Product/productApi.aspx?method=QueryALLOneProductType', valueField: 'TypeID', textField: 'TypeName',
                onSelect: function (rec) {
                    $('#ASID').combobox('clear');
                    var url = '../Product/productApi.aspx?method=QueryALLOneProductType&PID=' + rec.TypeID;
                    $('#ASID').combobox('reload', url);
                }
            });
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse', valueField: 'HouseID', textField: 'Name'
            });
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            //所在仓库
            $('#CHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse', valueField: 'HouseID', textField: 'Name'
            });
            $('#CHouseID').combobox('textbox').bind('focus', function () { $('#CHouseID').combobox('showPanel'); });
            //所在仓库
            $('#DHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse', valueField: 'HouseID', textField: 'Name'
            });
            $('#DHouseID').combobox('textbox').bind('focus', function () { $('#DHouseID').combobox('showPanel'); });
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            $('#CHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            $('#DHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');

            //省市区三级联动
            $('#CProvince').combobox({
                url: '../House/houseApi.aspx?method=QueryCityData', valueField: 'City', textField: 'City',
                onSelect: function (rec) {
                    $('#CCity').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryCityData&PID=' + rec.ID;
                    $('#CCity').combobox('reload', url);
                }
            });
            regionPie = document.getElementById('regionPie');
            regionPieChart = new Chart(regionPie, {
                type: 'pie',
                data: {
                    //labels: labes,
                    datasets: [{
                        //data: [10, 203, 25, 25, 98],
                        backgroundColor: ['#ff6384', '#36a2eb', '#ffce56', '#4bc0c0', '#9966ff']
                    }]
                },
                options: {
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        datalabels: {
                            display: true, // 确保设置为 true 来显示数据标签
                            formatter: function (value, context) {
                                console.log(value)
                                return value.toFixed(2) + '%'; // 将值乘以100并转换为百分比形式
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
                            display: false,
                            text: '各品牌销售情况'
                        }
                    }
                }
            });
            var num = Number($(window).height()) / 2
            $("#regionPie").css("height", num)
            StatisticalChart = document.getElementById('StatisticalChart1');
            Statistical = new Chart(StatisticalChart, {
                data: {
                    //labels: newDataset.labels,
                    datasets: [{
                        type: 'bar', // 指定第一个数据集为柱图
                        //label: '每日销量',
                        //data: newDataset.data,
                        borderWidth: 1
                    }, {
                        type: 'line', // 指定第二个数据集为折线图
                        label: '',
                        //data: newDataset.data,
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
                                //console.log(context)
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
                            text: '每日销量'
                        }

                    },
                }
            });
        });
        function QueryRegionStatistics() {
            $.ajax({
                url: "reportApi.aspx?method=QueryRegionStatistics",
                type: "post",
                data: {
                    StartDate: $('#AStartDate').datebox('getValue'),
                    EndDate: $('#AEndDate').datebox('getValue'),
                    ThrowGood: $('#ThrwG').is(':checked') ? "1" : "",
                    HouseID: $("#AHouseID").combobox('getValue')
                },
                success: function (data) {
                    regionPieChart.data.datasets.pop();
                    regionPieChart.update();
                    const newDataset = {
                        labels: [],
                        data: [],
                    }
                    //var data = row.rows;
                    console.log(data)
                    var newdata = eval("(" + data + ")")
                    console.log(newdata)
                    debugger;
                    for (var i = 0; i < newdata.length; i++) {
                        newDataset.labels.push(newdata[i].TypeName);
                        newDataset.data.push(newdata[i].NumSalesproportion)
                    }
                    regionPieChart.data.datasets.push(newDataset);
                    //myChart.data.datasets[0].data = datas;
                    regionPieChart.data.labels = newDataset.labels;
                    regionPieChart.options.plugins.title.display = true;
                    // 重新绘制图表
                    regionPieChart.update();
                }

            })

        }
        //按品牌分类查询统计
        function QueryBrandStatis() {
            var gridOpts = $('#dgBrandStatis').treegrid('options');
            gridOpts.url = 'reportApi.aspx?method=QueryBrandStatis';
            $('#dgBrandStatis').treegrid('load', {
                StartDate: $('#AStartDate').datebox('getValue'),
                EndDate: $('#AEndDate').datebox('getValue'),
                HouseID: $("#AHouseID").combobox('getValue'),
                PID: $("#APID").combobox('getValue'),//一级产品
                SID: $("#ASID").combobox('getValue'),//二级产品
                Specs: $('#ASpecs').val(),
                Figure: $('#AFigure').val(),
                ThrowGood: $('#ThrwG').is(':checked') ? "1" : ""
            });
            QueryRegionStatistics();
        }

        function setDatagrid() {
            $('#dgBrandStatis').treegrid({
                width: '100%',
                //height: '550px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                rownumbers: true,
                showFooter: false,
                animate: true,
                collapsible: true,
                idField: 'id',
                treeField: 'HouseName',
                toolbar: '#toolbarBrand',
                columns: [[
                    {
                        title: '所属仓库', field: 'HouseName', width: '180px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '日期', field: 'ReportDate', width: '60px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    },
                    {
                        title: '品牌', field: 'TypeName', width: '60px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    },
                    {
                        title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    },
                    {
                        title: '花纹', field: 'Figure', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    },
                    {
                        title: '轮胎数', field: 'OrderPiece', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '销售金额', field: 'OrderCharge', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '销售占比', field: 'Salesproportion', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    }
                ]]
            });
            $('#dgOrderStatis').datagrid({
                width: '100%',
                //height: '550px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: false, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'HouseName',
                rownumbers: true,
                showFooter: true,
                toolbar: '#toolbarStatis',
                columns: [[
                    { title: '所属仓库', field: 'HouseName', width: '80px', rowspan: 2 },
                    { title: '日期', field: 'ReportDate', width: '60px', rowspan: 2 },
                    { title: '电脑下单', colspan: 4, width: '80px' },
                    { title: '商城下单', colspan: 4, width: '80px' },
                    { title: 'APP下单', colspan: 4, width: '80px' },
                    { title: '企业号下单', colspan: 3, width: '80px' },
                    { title: '汇总', colspan: 3, width: '80px' }
                ],
                [
                    { title: "订单数", field: 'DNOrderNum', rowspan: 1 },
                    { title: "轮胎数", field: 'DNOrderPiece', rowspan: 1 },
                    { title: "订单金额", field: 'DNOrderCharge', rowspan: 1 },
                    { title: "订单占比", field: 'DNScale', rowspan: 1 },
                    { title: "订单数", field: 'SCOrderNum', rowspan: 1 },
                    { title: "轮胎数", field: 'SCOrderPiece', rowspan: 1 },
                    { title: "订单金额(元)", field: 'SCOrderCharge', rowspan: 1 },
                    { title: "订单占比", field: 'SCScale', rowspan: 1 },
                    { title: "订单数", field: 'APPOrderNum', rowspan: 1 },
                    { title: "轮胎数", field: 'APPOrderPiece', rowspan: 1 },
                    { title: "订单金额(元)", field: 'APPOrderCharge', rowspan: 1 },
                    { title: "订单占比", field: 'APPScale', rowspan: 1 },
                    { title: "订单数", field: 'QYOrderNum', rowspan: 1 },
                    { title: "轮胎数", field: 'QYOrderPiece', rowspan: 1 },
                    { title: "订单金额(元)", field: 'QYOrderCharge', rowspan: 1 },
                    { title: "订单数", field: 'OrderNum', rowspan: 1 },
                    { title: "轮胎数", field: 'OrderPiece', rowspan: 1 },
                    { title: "订单金额(元)", field: 'OrderCharge', rowspan: 1 }
                ]
                ]
            });
            $('#dgClientStatis').datagrid({
                width: '100%',
                //height: '600px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: false, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ShopCode',
                rownumbers: true,
                showFooter: true,
                toolbar: '#toolbarClient',
                columns: [[
                    /*{ title: '所属仓库', field: 'HouseName', width: '80px'},*/
                    { title: '区域', field: 'Area', width: '60px' },
                    { title: '所在省', field: 'Province', width: '60px' },
                    { title: '所在市', field: 'City', width: '60px' },
                    { title: '客户名称', field: 'ClientName', width: '150px' },
                    { title: '店代码', field: 'ShopCode', width: '60px' },
                    { title: '销售量', field: 'OrderPiece', width: '70px', align: 'right' },
                ]
                ],
                onClickRow: function (index, row) {
                    $('#dgClientStatis').datagrid('clearSelections');
                    $('#dgClientStatis').datagrid('selectRow', index);

                    var gridOpts = $('#dgClientStatisBrand').treegrid('options');
                    gridOpts.url = 'reportApi.aspx?method=QueryClientStatisByBrand';
                    $('#dgClientStatisBrand').treegrid('load', {
                        StartDate: $('#CStartDate').datebox('getValue'),
                        EndDate: $('#CEndDate').datebox('getValue'),
                        HouseID: $("#CHouseID").combobox('getValue'),
                        ClientNum: row.ClientNum,
                        ThrowGood: $('#ThrwG').is(':checked') ? "1" : ""
                    });
                    var gridOpts = $('#dgClientStatisHubDiameter').datagrid('options');
                    gridOpts.url = 'reportApi.aspx?method=QueryClientStatisByHubDiameter';
                    $('#dgClientStatisHubDiameter').datagrid('load', {
                        StartDate: $('#CStartDate').datebox('getValue'),
                        EndDate: $('#CEndDate').datebox('getValue'),
                        HouseID: $("#CHouseID").combobox('getValue'),
                        ClientNum: row.ClientNum,
                        ThrowGood: $('#ThrwG').is(':checked') ? "1" : ""
                    });
                    var gridOpts = $('#dgClientStatisCartype').datagrid('options');
                    gridOpts.url = 'reportApi.aspx?method=QueryClientStatisCartype';
                    $('#dgClientStatisCartype').datagrid('load', {
                        StartDate: $('#CStartDate').datebox('getValue'),
                        EndDate: $('#CEndDate').datebox('getValue'),
                        HouseID: $("#CHouseID").combobox('getValue'),
                        ClientNum: row.ClientNum,
                        ThrowGood: $('#ThrwG').is(':checked') ? "1" : ""
                    });

                    var gridOpts = $('#dgUrbanStatistics').treegrid('options');
                    gridOpts.url = 'reportApi.aspx?method=QueryUrbanStatistics';
                    $('#dgUrbanStatistics').treegrid('load', {
                        StartDate: $('#CStartDate').datebox('getValue'),
                        EndDate: $('#CEndDate').datebox('getValue'),
                        HouseID: $("#CHouseID").combobox('getValue'),
                        ClientNum: row.ClientNum,
                        ThrowGood: $('#ThrwG').is(':checked') ? "1" : ""
                    });

                },
            });
            //Chart.defaults.set('plugins.datalabels', {
            //    color: '#FE777B'
            //});
            Chart.register(ChartDataLabels);




            var Chartpie2 = document.getElementById('pie2');

            var myChart2 = new Chart(Chartpie2, {
                type: 'pie',
                data: {
                    //labels: labes,
                    datasets: [{
                        //data: datas,
                        //backgroundColor: ['#ff6384', '#36a2eb', '#ffce56', '#4bc0c0', '#9966ff']
                    }]
                },
                options: {
                    plugins: {
                        legend: {
                            position: 'top',
                        },
                        datalabels: {
                            display: true, // 确保设置为 true 来显示数据标签
                            formatter: function (value, context) {
                                console.log(value)
                                return value.toFixed(2) + '%'; // 将值乘以100并转换为百分比形式
                            },
                            color: 'Black',
                            font: {
                                weight: 'bold',
                                size: 15,
                            },
                        },
                        title: {
                            display: false,
                            text: '各省份城市销售情况'
                        }
                    }
                }
            });
            $('#dgUrbanStatistics').treegrid({
                width: '100%',
                height: '600px',
                title: '按省份统计', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                rownumbers: true,
                showFooter: false,
                animate: true,
                collapsible: true,
                idField: 'id',
                treeField: 'Province',
                columns: [[
                    {
                        title: '省份', field: 'Province', width: '180px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '销售数量', field: 'OrderPiece', width: '60px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    },
                    {
                        title: '销售占比', field: 'Salesproportion', width: '60px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    },
                ]],
                onLoadSuccess: function (index, row) {
                    myChart2.data.datasets.pop();
                    myChart2.update();
                    const newDataset = {
                        labels: [],
                        data: [],
                    }
                    var data = row.rows;
                    //console.log(data)
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].state == "closed") {
                            newDataset.labels.push(data[i].Province);
                            newDataset.data.push(data[i].NumSalesproportion)
                        }
                    }
                    myChart2.data.datasets.push(newDataset);
                    //myChart.data.datasets[0].data = datas;
                    myChart2.data.labels = newDataset.labels;
                    myChart2.options.plugins.title.display = true;
                    // 重新绘制图表
                    myChart2.update();
                }
            });


            var chartContainer = document.getElementById('pie');
            var myChart = new Chart(chartContainer, {
                type: 'pie',
                data: {
                    //labels: labes,
                    datasets: [{
                        //data: datas,
                        //backgroundColor: ['#ff6384', '#36a2eb', '#ffce56', '#4bc0c0', '#9966ff']
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
                        },
                        title: {
                            display: false,
                            text: '各品牌销售情况'
                        }
                    }
                }
            });
            $('#dgClientStatisBrand').treegrid({
                width: '100%',
                height: '300px',
                title: '按品牌统计', //标题内容
                loadMsg: '数据加载中请稍候...',
                //rownumbers: true,
                showFooter: false,
                animate: true,
                collapsible: true,
                idField: 'id',
                treeField: 'TypeName',
                toolbar: '',
                columns: [[
                    { title: '品牌', field: 'TypeName', width: '150px' },
                    { title: '规格', field: 'Specs', width: '80px', align: 'right' },
                    { title: '花纹', field: 'Figure', width: '80px', align: 'right' },
                    { title: '销售量', field: 'OrderPiece', width: '80px', align: 'right' },
                ]],
                onClickRow: function (index, row) { },
                onLoadSuccess: function (index, row) {
                    myChart.data.datasets.pop();
                    myChart.update();
                    const newDataset = {
                        labels: [],
                        data: [],
                    }
                    var data = row.rows
                    //console.log(data)
                    for (var i = 0; i < data.length; i++) {
                        if (data[i].state == "closed") {
                            newDataset.labels.push(data[i].TypeName);
                            newDataset.data.push(data[i].OrderPiece)
                        }
                    }
                    myChart.data.datasets.push(newDataset);
                    //myChart.data.datasets[0].data = datas;
                    myChart.data.labels = newDataset.labels;
                    myChart.options.plugins.title.display = true;
                    // 重新绘制图表
                    myChart.update();

                }
            });

            var Chartbar = document.getElementById('bar1');
            var myBarChart = new Chart(Chartbar, {
                type: 'bar',
                data: {
                    //labels: labes,
                    datasets: [{
                        //data: datas,
                        //backgroundColor: ['#ff6384', '#36a2eb', '#ffce56', '#4bc0c0', '#9966ff']
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
                        },
                        title: {
                            display: false,
                            text: '各尺寸销售情况'
                        },
                    },

                }
            });
            $('#dgClientStatisHubDiameter').datagrid({
                width: '100%',
                height: '300px',
                title: '按轮胎尺寸统计', //标题内容
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
                    { title: '尺寸', field: 'Specs', width: '70px' },
                    { title: '销售量', field: 'OrderPiece', width: '80px', align: 'right' },
                ]],
                onClickRow: function (index, row) { },
                onLoadSuccess: function (index, row) {
                    myBarChart.data.datasets.pop();
                    myBarChart.update();
                    const newDataset = {
                        labels: [],
                        data: [],
                    }
                    var date = $("#dgClientStatisHubDiameter").datagrid("getRows");
                    for (var i = 0; i < date.length; i++) {
                        newDataset.labels.push(date[i].Specs + "寸")
                        newDataset.data.push(date[i].OrderPiece)
                    }
                    myBarChart.data.datasets.push(newDataset);
                    //myChart.data.datasets[0].data = datas;
                    myBarChart.data.labels = newDataset.labels;
                    myBarChart.data.datasets[0].label = "销量";
                    myBarChart.options.plugins.title.display = true;
                    // 重新绘制图表
                    myBarChart.update();
                    $("#BarLi1").css("display", "")
                }
            });
            function buildColor() {
                var color = "#" + Math.floor(Math.random() * 0xFFFFFF).toString(16).padStart(6, "0");
                return color;
            }
            var Chartbar2 = document.getElementById('bar2');
            var myBarChart2 = new Chart(Chartbar2, {
                type: 'bar',
                data: {
                    //labels: labes,
                    datasets: [{
                        //data: datas,
                        //backgroundColor: ['#ff6384', '#36a2eb', '#ffce56', '#4bc0c0', '#9966ff']
                    }]
                },
                options: {
                    plugins: [ChartDataLabels],
                    plugins: {
                        datalabels: {
                            display: true, // 确保设置为 true 来显示数据标签
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
                            text: '各轮胎车型销售情况'
                        }

                    },

                }
            });
            $('#dgClientStatisCartype').datagrid({
                width: '100%',
                height: '300px',
                title: '按车型统计', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: false, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'CarModel',
                rownumbers: true,
                showFooter: true,
                toolbar: '',
                columns: [[
                    { title: '车型', field: 'CarModel', width: '70px' },
                    { title: '销售量', field: 'OrderPiece', width: '80px', align: 'right' },
                ]],
                onClickRow: function (index, row) { },
                onLoadSuccess: function (index, rows) {
                    myBarChart2.data.datasets.pop();
                    myBarChart2.update();
                    const newDataset = {
                        labels: [],
                        data: [],
                        backgroundColor: [],
                    }
                    var date = $("#dgClientStatisCartype").datagrid("getRows");
                    //console.log(date)
                    for (var i = 0; i < date.length; i++) {
                        newDataset.labels.push(date[i].CarModel)
                        newDataset.data.push(date[i].OrderPiece)
                        newDataset.backgroundColor.push(buildColor());
                    }
                    myBarChart2.data.datasets.push(newDataset);
                    //myChart.data.datasets[0].data = datas;
                    myBarChart2.data.labels = newDataset.labels;
                    myBarChart2.data.datasets[0].label = "销量";
                    myBarChart2.options.plugins.title.display = true;
                    //myBarChart2.options.plugins.datalabels.display = true;
                    //myBarChart2.options.plugins.datalabels.color = "#ff6384";
                    //console.log(newDataset.backgroundColor)
                    myBarChart2.data.datasets[0].backgroundColor = newDataset.backgroundColor;
                    // 重新绘制图表
                    myBarChart2.update();
                    if (date.length > 0) {
                        $("#BarLi2").css("display", "")
                    }
                    else {
                        $("#BarLi2").css("display", "none")
                    }
                }
            });
        }


        //按订单统计查询方法
        function QueryOrderStatis() {
            if ($('#DStartDate').datebox('getValue') == undefined || $('#DStartDate').datebox('getValue') == '') {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择查询时间', 'warning');
                return;
            }
            if ($('#DEndDate').datebox('getValue') == undefined || $('#DEndDate').datebox('getValue') == '') {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择查询时间', 'warning');
                return;
            }
            var gridOpts = $('#dgOrderStatis').datagrid('options');
            gridOpts.url = 'reportApi.aspx?method=QueryOrderStatis';
            $('#dgOrderStatis').datagrid('load', {
                StartDate: $('#DStartDate').datebox('getValue'),
                EndDate: $('#DEndDate').datebox('getValue'),
                ThrowGood: $('#DThrwG').is(':checked') ? "1" : "",
                HouseID: $("#DHouseID").combobox('getValue')
            });
            $.ajax({
                url: 'reportApi.aspx?method=QueryStatisticalChart',
                data: {
                    StartDate: $('#DStartDate').datebox('getValue'),
                    EndDate: $('#DEndDate').datebox('getValue'),
                    ThrowGood: $('#DThrwG').is(':checked') ? "1" : "",
                    HouseID: $("#DHouseID").combobox('getValue')
                },
                type: 'post',
                success: function (text) {
                    Statistical.data.datasets.pop();
                    Statistical.data.datasets.pop();
                    Statistical.update();
                    const newDataset = {
                        labels: [],
                        data: [],
                    }
                    var data = eval("(" + text + ")")
                    //console.log(data)
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
                    //console.log(Statistical.data)
                    //console.log(Statistical.data.datasets)
                    Statistical.data.datasets.push(datasets0);
                    Statistical.data.datasets.push(datasets1);
                    Statistical.data.labels = newDataset.labels;
                    Statistical.options.plugins.title.display = true;
                    $("#Chart1").css("display", "")
                    Statistical.update();
                }

            })

        }
        //按客户统计查询方法
        function QueryClientStatis() {
            var gridOpts = $('#dgClientStatis').datagrid('options');
            gridOpts.url = 'reportApi.aspx?method=QueryClientStatis';
            $('#dgClientStatis').datagrid('load', {
                StartDate: $('#CStartDate').datebox('getValue'),
                EndDate: $('#CEndDate').datebox('getValue'),
                HouseID: $("#CHouseID").combobox('getValue'),
                Area: $("#AArea").combobox('getValue'),
                Province: $("#CProvince").combobox('getValue'),
                City: $("#CCity").combobox('getValue'),
                ClientName: $('#CName').val(),
                ShopCode: $('#CShopCode').val(),
                ThrowGood: $('#ThrwG').is(':checked') ? "1" : ""
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id='Loading' style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 98%; height: 100%; background: white; text-align: center; padding: 5px 10px; display: table;">
        <div style="display: table-cell; vertical-align: middle">
            <h1><font size="9">页面加载中……</font></h1>
        </div>
    </div>
    <div id="tbDepmanifest" class="easyui-tabs" data-options="fit:true">
        <div title="品牌销量统计" data-options="iconCls:'icon-award_star_bronze_2'">
            <div id="saPanel" class="easyui-panel" title="" data-options="iconCls:'icon-search'">
                <table>
                    <tr>
                        <td style="text-align: right;">区域大仓:
                        </td>
                        <td>
                            <input id="AHouseID" class="easyui-combobox" style="width: 100px;" />
                        </td>
                        <td style="text-align: right;">一级产品:
                        </td>
                        <td>
                            <input id="APID" class="easyui-combobox" style="width: 80px;"
                                panelheight="auto" />
                        </td>
                        <td style="text-align: right;">二级产品:
                        </td>
                        <td>
                            <input id="ASID" class="easyui-combobox" style="width: 80px;" data-options="valueField:'TypeID',textField:'TypeName'"
                                panelheight="auto" />
                        </td>
                        <td style="text-align: right;">规格:
                        </td>
                        <td>
                            <input id="ASpecs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 80px">
                        </td>
                        <td style="text-align: right;">花纹:
                        </td>
                        <td>
                            <input id="AFigure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 80px">
                        </td>
                        <td style="text-align: right;">查询时间:
                        </td>
                        <td>
                            <input id="AStartDate" class="easyui-datebox" style="width: 100px" />~
                        <input id="AEndDate" class="easyui-datebox" style="width: 100px" />
                        </td>
                        <td>
                            <input type="checkbox" id="ThrwG" value="1" />
                            <label for="ThrwG">统计内部单</label></td>
                    </tr>
                </table>
            </div>
            <table id="dgBrandStatis">
            </table>
            <div id="toolbarBrand">
                <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="QueryBrandStatis()">&nbsp;查&nbsp;询&nbsp;</a>
            </div>
            <div>
                <canvas id="regionPie"></canvas>
            </div>

        </div>
        <div title="业务员统计" data-options="iconCls:'icon-telephone'"></div>
        <div title="客户统计" data-options="iconCls:'icon-user'">
            <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search',fit:false" style="margin: 0px">
                <table>
                    <tr>
                        <td style="text-align: right;">区域大仓:
                        </td>
                        <td>
                            <input id="CHouseID" class="easyui-combobox" style="width: 100px;" />
                        </td>
                        <td style="text-align: right;">公司简称:
                        </td>
                        <td>
                            <input id="CName" class="easyui-textbox" style="width: 100px" />
                        </td>
                        <td style="text-align: right;">店代码:
                        </td>
                        <td>
                            <input id="CShopCode" class="easyui-textbox" style="width: 100px" />
                        </td>
                        <td style="text-align: right;">区域:
                        </td>
                        <td>
                            <select class="easyui-combobox" id="AArea" style="width: 80px;" panelheight="auto">
                                <option value="-1">全部</option>
                                <option value="华南1区">华南1区</option>
                                <option value="华南2区">华南2区</option>
                            </select>
                        </td>
                        <td style="text-align: right;">省:</td>
                        <td>
                            <input id="CProvince" class="easyui-combobox" style="width: 90px;" />
                        </td>
                        <td style="text-align: right;">市:</td>
                        <td>
                            <input id="CCity" class="easyui-combobox" style="width: 90px;" data-options="valueField:'City',textField:'City'" /></td>
                        <td style="text-align: right;">查询时间:
                        </td>
                        <td>
                            <input id="CStartDate" class="easyui-datebox" style="width: 100px" />~
                        <input id="CEndDate" class="easyui-datebox" style="width: 100px" />
                        </td>
                        <td>
                            <input type="checkbox" id="CThrwG" value="1" />
                            <label for="CThrwG">统计内部单</label></td>
                    </tr>
                </table>
            </div>
            <div style="float: left; width: 30%;">
                <table id="dgClientStatis" style="width: 100%">
                </table>
            </div>
            <div style="float: left; width: 30%;">
                <table id="dgClientStatisBrand">
                </table>
            </div>
            <div style="float: left; width: 20%; margin: 0px;">
                <table id="dgClientStatisHubDiameter">
                </table>
            </div>
            <div style="float: left; width: 20%;">
                <table id="dgClientStatisCartype">
                </table>
            </div>
            <div style="width: 68%; height: 40%; float: left;">
                <ul id="eChartImg">
                    <li style="width: 30%;">
                        <canvas id="pie2"></canvas>
                    </li>
                    <li style="width: 30%;">
                        <canvas id="pie"></canvas>
                    </li>
                    <li style="display: none; width: 35%; height: 100%;" id="BarLi1">
                        <canvas id="bar1"></canvas>
                    </li>
                    <li style="display: none; width: 35%;" id="BarLi2">
                        <canvas id="bar2"></canvas>
                    </li>
                </ul>
            </div>
            <div style="width: 30%; float: left; position: relative; top: -12%">
                <table id="dgUrbanStatistics">
                </table>
            </div>
            <div id="toolbarClient">
                <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="QueryClientStatis()">&nbsp;查&nbsp;询&nbsp;</a>
            </div>
        </div>
        <div title="订单统计" data-options="iconCls:'icon-report'">
            <div id="saPanel" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
                <table>
                    <tr>
                        <td style="text-align: right;">区域大仓:
                        </td>
                        <td>
                            <input id="DHouseID" class="easyui-combobox" style="width: 100px;" />
                        </td>
                        <td style="text-align: right;">查询时间:
                        </td>
                        <td>
                            <input id="DStartDate" class="easyui-datebox" style="width: 100px" />~
                        <input id="DEndDate" class="easyui-datebox" style="width: 100px" />
                        </td>
                        <td>
                            <input type="checkbox" id="DThrwG" value="1" />
                            <label for="DThrwG">统计内部单</label></td>
                    </tr>
                </table>
            </div>
            <table id="dgOrderStatis">
            </table>
            <div style="width: 80%; height: 400px; display: none" id="Chart1">
                <canvas id="StatisticalChart1"></canvas>
            </div>
            <div id="toolbarStatis">
                <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="QueryOrderStatis()">&nbsp;查&nbsp;询&nbsp;</a>
            </div>
        </div>
    </div>
</asp:Content>
