<%@ Page Title="领导驾驶舱" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LeaderCookpit.aspx.cs" Inherits="Cargo.Report.LeaderCookpit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/easy/js/datagrid-groupview.js" type="text/javascript"></script>
    <script type="text/javascript">
        var categories = [];
        var orderList = [];
        var numList = [];
        var moneyList = [];
        var numListQOQ = [];
        var charTitle;
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
        $(document).ready(function () {
            $('#ByProductType').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                rownumbers: false,
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'TypeName',
                url: null,
                columns: [[
                  { title: '产品分类', field: 'TypeName', width: '100px' },
                  { title: '总销量', field: 'Piece', width: '100px', align: 'right' },
                  { title: '总收入(元)', field: 'TotalCharge', width: '150px', align: 'right' }
                ]],
                groupField: 'HouseName',
                view: groupview,
                groupFormatter: function (value, rows) {
                    return value
                },
                onLoadSuccess: function (data) {
                    var tM = 0; tT = 0;
                    for (var i = 0; i < data.rows.length; i++) {
                        tM += Number(data.rows[i].Piece);
                        tT += Number(data.rows[i].TotalCharge);
                    }
                    $('#TotalPiece').html(Number(tM));
                    $('#TotalCharge').html(Number(tT).toFixed(2));
                },
            });
            $('#dgSaleMan').datagrid({
                width: '100%',
                height: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                rownumbers: true,
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'SaleManName',
                url: null,
                columns: [[
                  { title: '姓名', field: 'SaleManName', width: '100px' },
                  { title: '总销量', field: 'Piece', width: '100px', align: 'right' },
                  { title: '总收入(元)', field: 'TotalCharge', width: '100px', align: 'right' }
                ]],
                groupField: 'HouseName',
                view: groupview,
                groupFormatter: function (value, rows) {
                    return value
                }
            });
            $('#dgClient').datagrid({
                width: '100%',
                height: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                rownumbers: true,
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'AcceptPeople',
                url: null,
                columns: [[
                  { title: '客户姓名', field: 'AcceptPeople', width: '80px' },
                  { title: '总销量', field: 'Piece', width: '80px', align: 'right' },
                  { title: '总收入(元)', field: 'TotalCharge', width: '100px', align: 'right' }
                ]],
                groupField: 'HouseName',
                view: groupview,
                groupFormatter: function (value, rows) {
                    return value
                }
            });
            $('#dgbyMonthRank').datagrid({
                width: '100%',
                height: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                rownumbers: false,
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'HouseName',
                url: null,
                columns: [[
                  { title: '仓库名称', field: 'HouseName', width: '60px' },
                  { title: '日期', field: 'DisplayTime', width: '50px' },
                  { title: '总销量(条)', field: 'Piece', width: '70px', align: 'right' },
                  { title: '总收入(万)', field: 'TotalCharge', width: '70px', align: 'right' }
                ]],
                onLoadSuccess: function (data) {
                    var cdate;
                    var cprofit = 0;
                    console.log(data)
                    for (var i = 0; i < data.rows.length; i++) {
                        categories.push(data.rows[i].DisplayTime);
                        orderList.push(data.rows[i].OrderNum);

                        numList.push(data.rows[i].Piece);   
                        moneyList.push(data.rows[i].TotalCharge);
                    }

                    console.log(numList)

                    charTitle = data.rows[0].HouseName + "销售统计表";
                    categories.reverse();
                    orderList.reverse();
                    numList.reverse();
                    moneyList.reverse();
                    numListQOQ.push(0)
                    for (var i = 0; i < numList.length; i++) {
                        var s = i + 1;
                        if (s < numList.length) {
                            numListQOQ.push((((numList[s] - numList[i]) / numList[i]) * 100).toFixed(2));
                        }
                    }
                    console.log(numList)
                    console.log(numListQOQ)
                    setChart();
                }
            });
            $('#dgbyProductRank').datagrid({
                width: '100%',
                height: '100%',
                title: '每月业务量按产品分类统计', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                rownumbers: false,
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'HouseName',
                url: null,
                columns: [[
                  { title: '仓库名称', field: 'HouseName', width: '60px' },
                  { title: '产品类型', field: 'Title', width: '60px' },
                  { title: '年月', field: 'DisplayTime', width: '50px' },
                  { title: '总销量(条)', field: 'Piece', width: '70px', align: 'right' },
                  { title: '总收入(万)', field: 'TotalCharge', width: '70px', align: 'right' }
                ]]
            });
            $('#dgbySaleManRank').datagrid({
                width: '100%',
                height: '100%',
                title: '每月业务量按业务员统计', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                rownumbers: false,
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'HouseName',
                url: null,
                columns: [[
                  { title: '仓库名称', field: 'HouseName', width: '60px' },
                  { title: '业务员', field: 'Title', width: '60px' },
                  { title: '年月', field: 'DisplayTime', width: '50px' },
                  { title: '总销量(条)', field: 'Piece', width: '70px', align: 'right' },
                  { title: '总收入(万)', field: 'TotalCharge', width: '70px', align: 'right' }
                ]]
            });
            $('#dgbyClientRank').datagrid({
                width: '100%',
                height: '100%',
                title: '每月业务量按客户统计TOP10', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                rownumbers: false,
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'HouseName',
                url: null,
                columns: [[
                  { title: '仓库名称', field: 'HouseName', width: '60px' },
                  { title: '客户姓名', field: 'Title', width: '70px' },
                  { title: '年月', field: 'DisplayTime', width: '50px' },
                  { title: '总销量(条)', field: 'Piece', width: '70px', align: 'right' },
                  { title: '总收入(万)', field: 'TotalCharge', width: '70px', align: 'right' }
                ]]
            });
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            var dat = new Date();
            $('#monthSale').panel({ title: dat.getFullYear() + "年" + (dat.getMonth() + 1) + "月业务量统计" });
            $('#saleManRank').panel({ title: dat.getFullYear() + "年" + (dat.getMonth() + 1) + "月业务员销量排名" });
            $('#clientRank').panel({ title: dat.getFullYear() + "年" + (dat.getMonth() + 1) + "月客户销量排名TOP10" });
            dosearch();
        });
        function setChart() {
            require(
        [
            'echarts',
            'echarts/chart/bar',
            'echarts/chart/line'
        ],
        function (ec) {
            var myChart = ec.init(document.getElementById('main'));
            //图表显示提示信息
            //myChart.showLoading({ text: "图表数据正在努力加载中..." });
            myChart.setOption({
                tooltip: {
                    show: true,
                    trigger: 'axis'//axis
                },
                title: {
                    show: true,
                    text: charTitle,
                    x: 'left',
                    textStyle: {
                        fontSize: '20',
                        fontFamily: '微软雅黑',
                        fontWeight: 'bolder',
                        color: 'red'
                    }
                },
                legend: {
                    show: true,
                    data: ['订单数', '总销量', '总收入(万)','环比增长率']
                },
                toolbox: {
                    show: true,
                    feature: {
                        mark: { show: true },
                        dataView: { show: true, readOnly: false },
                        magicType: { show: true, type: ['line', 'bar'] },
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                calculable: true,
                xAxis: [
                {
                    type: 'category',
                        data: categories,
                        axisLabel: {
                            interval: 0
                        }
                        //axisLabel: {
                        //    interval: 0,
                        //    formatter: function (value,index) {
                        //        console.log(value + ',' + index)
                        //        alert(index)
                        //        if (index ==0) {
                        //            return '{special|' + value + '}';
                        //        } else {
                        //            return value;
                        //        }
                        //    },
                        //}
                    },  
 
                ],
                yAxis: [
                {
                    type: 'value'
                    }, {
                        position: 'right',
                        type: 'value',
                        max: 200,
                        min:0
                    }
                ],
                series: [
                {
                    name: '订单数',
                    type: 'bar',
                    markPoint: {
                        data: [
                            { type: 'max', name: '最大值' },
                            { type: 'min', name: '最小值' }
                        ]
                    },
                    markLine: {
                        data: [
                            { type: 'average', name: '平均值' }
                        ]
                    },
                    data: orderList
                    },
                    {
                        name: '环比',
                        type: 'line',
                        yAxisIndex: 1 , 
                        data: numListQOQ,
                        itemStyle: {
                            normal: {
                                label: {
                                    show: true // 在折线拐点上显示数据
                                },
                            }
                        }
                    },
                {
                    name: '总销量',
                    type: 'bar',
                    markPoint: {
                        data: [
                            { type: 'max', name: '最大销量' },
                            { type: 'min', name: '最小销量' }
                        ]
                    },
                    markLine: {
                        data: [
                            { type: 'average', name: '平均值' }
                        ]
                    },
                    //itemStyle: {
                    //    normal: {                   // 系列级个性化，横向渐变填充
                    //        borderRadius: 5,
                    //        color: 'rgba(30,144,255,0.8)',
                    //        label: {
                    //            show: true,
                    //            textStyle: {
                    //                fontSize: '15',
                    //                fontFamily: '微软雅黑',
                    //                fontWeight: 'bold'
                    //            }
                    //        }
                    //    }
                    //},
                    data: numList
                },
                {
                    name: '总收入(万)',
                    type: 'bar',
                    markPoint: {
                        data: [
                            { type: 'max', name: '最大收入' },
                            { type: 'min', name: '最小收入' }
                        ]
                    },
                    markLine: {
                        data: [
                            { type: 'average', name: '平均值' }
                        ]
                    },
                    //itemStyle: {
                    //    normal: {                   // 系列级个性化，横向渐变填充
                    //        borderRadius: 5,
                    //        color: 'rgba(30,144,255,0.8)',
                    //        label: {
                    //            show: true,
                    //            textStyle: {
                    //                fontSize: '15',
                    //                fontFamily: '微软雅黑',
                    //                fontWeight: 'bold'
                    //            }
                    //        }
                    //    }
                    //},
                    data: moneyList
                }
                ]
            });
        })
        }
        //查询统计
        function dosearch() {
            $('#ByProductType').datagrid('clearSelections');
            var gridOpts = $('#ByProductType').datagrid('options');
            gridOpts.url = 'reportApi.aspx?method=QueryByProductType';
            $('#ByProductType').datagrid('load', {});

            $('#dgSaleMan').datagrid('clearSelections');
            var dgSaleManOpts = $('#dgSaleMan').datagrid('options');
            dgSaleManOpts.url = 'reportApi.aspx?method=QueryBySaleMan';
            $('#dgSaleMan').datagrid('load', {});

            $('#dgClient').datagrid('clearSelections');
            var dgClientOpts = $('#dgClient').datagrid('options');
            dgClientOpts.url = 'reportApi.aspx?method=QueryByClient';
            $('#dgClient').datagrid('load', {});

            //$('#dgbyMonthRank').datagrid('clearSelections');
            //var dgbyMonthRankOpts = $('#dgbyMonthRank').datagrid('options');
            //dgbyMonthRankOpts.url = 'reportApi.aspx?method=QueryByMonthStatis&StatisType=0';
            //$('#dgbyMonthRank').datagrid('load', {});

            //$('#dgbyProductRank').datagrid('clearSelections');
            //var dgbyProductRankOpts = $('#dgbyProductRank').datagrid('options');
            //dgbyProductRankOpts.url = 'reportApi.aspx?method=QueryByMonthStatis&StatisType=1';
            //$('#dgbyProductRank').datagrid('load', {});

            //$('#dgbySaleManRank').datagrid('clearSelections');
            //var dgbySaleManRankOpts = $('#dgbySaleManRank').datagrid('options');
            //dgbySaleManRankOpts.url = 'reportApi.aspx?method=QueryByMonthStatis&StatisType=2';
            //$('#dgbySaleManRank').datagrid('load', {});

            //$('#dgbyClientRank').datagrid('clearSelections');
            //var dgbyClientRankOpts = $('#dgbyClientRank').datagrid('options');
            //dgbyClientRankOpts.url = 'reportApi.aspx?method=QueryByMonthStatis&StatisType=3';
            //$('#dgbyClientRank').datagrid('load', {});

        }
        //按年份查询
        function QueryYear()
        {
            var BatchYear = $('#ABatchYear').combobox('getValue');
            var PID = $("#AHouseID").combobox('getValue');
            categories = [];
            orderList = [];
            numList = [];
            moneyList = [];
            $('#dgbyMonthRank').datagrid('clearSelections');
            var dgbyMonthRankOpts = $('#dgbyMonthRank').datagrid('options');
            dgbyMonthRankOpts.url = 'reportApi.aspx?method=QueryByMonthStatis&StatisType=0&HouseID=' + PID + '&Year=' + BatchYear;
            $('#dgbyMonthRank').datagrid('load', {});

            $('#dgbyProductRank').datagrid('clearSelections');
            var dgbyProductRankOpts = $('#dgbyProductRank').datagrid('options');
            dgbyProductRankOpts.url = 'reportApi.aspx?method=QueryByMonthStatis&StatisType=1&HouseID=' + PID + '&Year=' + BatchYear;
            $('#dgbyProductRank').datagrid('load', {});

            $('#dgbySaleManRank').datagrid('clearSelections');
            var dgbySaleManRankOpts = $('#dgbySaleManRank').datagrid('options');
            dgbySaleManRankOpts.url = 'reportApi.aspx?method=QueryByMonthStatis&StatisType=2&HouseID=' + PID + '&Year=' + BatchYear;
            $('#dgbySaleManRank').datagrid('load', {});

            $('#dgbyClientRank').datagrid('clearSelections');
            var dgbyClientRankOpts = $('#dgbyClientRank').datagrid('options');
            dgbyClientRankOpts.url = 'reportApi.aspx?method=QueryByMonthStatis&StatisType=3&HouseID=' + PID + '&Year=' + BatchYear;
            $('#dgbyClientRank').datagrid('load', {});
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id='Loading' style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 98%; height: 100%; background: white; text-align: center; padding: 5px 10px; display: table;">
        <div style="display: table-cell; vertical-align: middle">
            <h1><font size="9">页面加载中……</font></h1>
        </div>
    </div>

    <div style="float: left; width: 37%;">
        <div id="monthSale" class="easyui-panel" title="" style="height: 300px;"
            data-options="iconCls:'icon-chart_bar',collapsible:true">
            <table>
                <tr>
                    <td style="text-align: left;">总销量：<span id="TotalPiece"></span>&nbsp;&nbsp;条
                    </td>
                    <td>总收入：<span id="TotalCharge"></span>&nbsp;&nbsp;元
                    </td>
                </tr>
            </table>
            <table id="ByProductType" class="easyui-datagrid">
            </table>
        </div>
    </div>
    <div style="float: left; width: 35%;">
        <div id="saleManRank" class="easyui-panel" title="" style="height: 300px;"
            data-options="iconCls:'icon-chart_bar',collapsible:true">
            <table id="dgSaleMan" class="easyui-datagrid">
            </table>
        </div>

    </div>
    <div style="float: right; width: 28%;">
        <div id="clientRank" class="easyui-panel" title="" style="height: 300px;"
            data-options="iconCls:'icon-chart_bar',collapsible:true">
            <table id="dgClient" class="easyui-datagrid">
            </table>
        </div>
    </div>

    <div style="float: left; width: 100%;">
        <table>
            <tr>
                <td style="text-align: right;">查询年份:
                </td>
                <td>
                    <select class="easyui-combobox" id="ABatchYear" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="2023">2023年</option>
                        <option value="2022">2022年</option>
                        <option value="2021">2021年</option>
                        <option value="2020">2020年</option>
                        <option value="2019">2019年</option>
                        <option value="2018">2018年</option>
                    </select>
                </td>
                <td style="text-align: right;">区域大仓:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 80px;"
                        panelheight="auto" />
                </td>

                <td><a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="QueryYear()">&nbsp;查询&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    <div style="float: left; width: 25%;">
        <div id="byMonthRank" class="easyui-panel" title="每月业务量统计" style="height: 250px;"
            data-options="iconCls:'icon-chart_bar',collapsible:true">

            <table id="dgbyMonthRank" class="easyui-datagrid">
            </table>
        </div>
    </div>
    <div style="float: left; width: 25%;">
        <div id="byProductRank" class="easyui-panel" title="" style="height: 250px;"
            data-options="iconCls:'icon-chart_bar',collapsible:true">
            <table id="dgbyProductRank" class="easyui-datagrid">
            </table>
        </div>
    </div>
    <div style="float: left; width: 25%;">
        <div id="bySaleManRank" class="easyui-panel" title="" style="height: 250px;"
            data-options="iconCls:'icon-chart_bar',collapsible:true">
            <table id="dgbySaleManRank" class="easyui-datagrid">
            </table>
        </div>
    </div>
    <div style="float: left; width: 25%;">
        <div id="byClientRank" class="easyui-panel" title="" style="height: 250px;"
            data-options="iconCls:'icon-chart_bar',collapsible:true">
            <table id="dgbyClientRank" class="easyui-datagrid">
            </table>
        </div>
    </div>

    <div style="float: left; width: 100%;">
        <div id="main" style="height: 380px; border: 1px solid #ccc; padding: 10px;"></div>
    </div>

    <script src="../JS/EChart/echarts.js" type="text/javascript"></script>

    <script type="text/javascript">
        require.config({
            paths: {
                echarts: '../JS/EChart'
            }
        });
    </script>
</asp:Content>
