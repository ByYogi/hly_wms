<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderStatistics.aspx.cs" Inherits="Cargo.Report.OrderStatistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/easy/js/datagrid-groupview.js" type="text/javascript"></script>
    <script src="../JS/EChart/echarts.js" type="text/javascript"></script>
    <script type="text/javascript">
        require.config({
            paths: {
                echarts: '../JS/EChart'
            }
        });
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
            $('#dg').datagrid({
                width: '100%',
                height:'360px',
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: 10, //每页多少条
                pageList: [10, 50],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OrderID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    { title: '', field: 'OrderID', checkbox: true, width: '30px' },
                    {
                        title: '客户名称', field: 'AcceptUnit', width: '240px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '总订单', field: 'TotalOrders', width: '50px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '总件数', field: 'TotalPiece', width: '50px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '总收入', field: 'TotalTotalCharge', width: '50px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                ]],
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                rowStyler: function (index, row) { },
                onDblClickRow: function (index, row) { }
            });

            var datenow = new Date();
            $('#StartDate').datebox('setValue', getNowFormatDate(datenow.getFullYear().toString() + "-" + (datenow.getMonth() + 1).toString() + "-01"));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));

            $('#HouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });
            var HID = "<%=UserInfor.HouseID%>";
            $("#HouseID").combobox("setValue", HID);
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'reportApi.aspx?method=QueryOrderStatistics';
            $('#dg').datagrid('load', {
                HouseID: $("#HouseID").combobox('getValue'),
                ThrowGood: $("#ThrowGood").combobox('getValue'),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue')
            });
            searchNum();
        }
        var categories = [];
        var TotalOrders = [];
        var TotalPiece = [];
        var TotalTotalCharge = [];
        var charTitle;
        function searchNum() {
            $("#main").empty();
            categories = [];
            TotalOrders = [];
            TotalPiece = [];
            TotalTotalCharge = [];
            $.ajax({
                async: false,
                url: "reportApi.aspx?method=QueryOrderStatisticsChartData&HouseID=" + $("#HouseID").combobox('getValue') + "&ThrowGood=" + $("#ThrowGood").combobox('getValue') + "&StartDate=" + $('#StartDate').datebox('getValue')+"&EndDate="+$('#EndDate').datebox('getValue'),
                cache: false,
                success: function (o) {
                    var data = eval('(' + o + ')');
                    for (var i = 0; i < data.length; i++) {
                        categories.push(data[i].AcceptUnit);
                        TotalPiece.push(data[i].TotalPiece);
                        TotalOrders.push(data[i].TotalOrders);
                        TotalTotalCharge.push(data[i].TotalTotalCharge);
                    }
                }
            });
            //categories.reverse();
            //valueories.reverse();
            setChart();
        }

        function setChart() {
            charTitle = $("#HouseID").combobox('getText') + $('#StartDate').datebox('getValue') + "至" + $('#EndDate').datebox('getValue') + "前十订单统计";
            ab();
            ab2();
        }
        function ab() {
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
                            trigger: 'item'
                        },
                        title: {
                            show: true,
                            text: charTitle,
                            x: 'center',
                            textStyle: {
                                fontSize: '15',
                                fontFamily: '微软雅黑',
                                fontWeight: 'bolder',
                                color: 'red'
                            }
                        },
                        legend: {
                            show: false,
                            data: ['总件数']
                        },
                        toolbox: {
                            show: true,
                            feature: {
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
                                    show: true,
                                    rotate: 60
                                }
                            }
                        ],
                        yAxis: [
                            {
                                type: 'value'
                            }
                        ],
                        series: [
                            {
                                name: '总订单',
                                type: 'bar',
                                itemStyle: {
                                    normal: {                   // 系列级个性化，横向渐变填充
                                        borderRadius: 5,
                                        color: 'rgb(0,139,69)',
                                        label: {
                                            show: true,
                                            textStyle: {
                                                fontSize: '15',
                                                fontFamily: '微软雅黑',
                                                fontWeight: 'bold'
                                            }
                                        }
                                    }
                                },
                                data: TotalOrders
                            },
                            {
                                name: '总件数',
                                type: 'bar',
                                itemStyle: {
                                    normal: {                   // 系列级个性化，横向渐变填充
                                        borderRadius: 5,
                                        color: 'rgba(30,144,255,0.8)',
                                        label: {
                                            show: true,
                                            textStyle: {
                                                fontSize: '15',
                                                fontFamily: '微软雅黑',
                                                fontWeight: 'bold'
                                            }
                                        }
                                    }
                                },
                                data: TotalPiece
                            }
                        ]
                    });
                }
            )
        }
        function ab2() {
            require(
                [
                    'echarts',
                    'echarts/chart/bar',
                    'echarts/chart/line'
                ],
                function (ec) {
                    var myChart = ec.init(document.getElementById('main2'));
                    //图表显示提示信息
                    //myChart.showLoading({ text: "图表数据正在努力加载中..." });

                    myChart.setOption({
                        series: [
                            {
                                name: '访问来源',
                                type: 'pie',    // 设置图表类型为饼图
                                radius: '55%',  // 饼图的半径，外半径为可视区尺寸（容器高宽中较小一项）的 55% 长度。
                                data: [          // 数据数组，name 为数据项名称，value 为数据项值
                                    { value: 235, name: '视频广告' },
                                    { value: 274, name: '联盟广告' },
                                    { value: 310, name: '邮件营销' },
                                    { value: 335, name: '直接访问' },
                                    { value: 400, name: '搜索引擎' }
                                ]
                            }
                        ]
                    })
                    myChart.setOption({
                        tooltip: {
                            show: true,
                            trigger: 'item'
                        },
                        title: {
                            show: true,
                            text: charTitle,
                            x: 'center',
                            textStyle: {
                                fontSize: '15',
                                fontFamily: '微软雅黑',
                                fontWeight: 'bolder',
                                color: 'red'
                            }
                        },
                        legend: {
                            show: false,
                            data: ['总件数']
                        },
                        toolbox: {
                            show: true,
                            feature: {
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
                                    show: true,
                                    rotate: 60
                                }
                            }
                        ],
                        yAxis: [
                            {
                                type: 'value'
                            }
                        ],
                        series: [
                            {
                                name: '总订单',
                                type: 'bar',
                                itemStyle: {
                                    normal: {                   // 系列级个性化，横向渐变填充
                                        borderRadius: 5,
                                        color: 'rgb(0,139,69)',
                                        label: {
                                            show: true,
                                            textStyle: {
                                                fontSize: '15',
                                                fontFamily: '微软雅黑',
                                                fontWeight: 'bold'
                                            }
                                        }
                                    }
                                },
                                data: TotalOrders
                            },
                            {
                                name: '总件数',
                                type: 'bar',
                                itemStyle: {
                                    normal: {                   // 系列级个性化，横向渐变填充
                                        borderRadius: 5,
                                        color: 'rgba(30,144,255,0.8)',
                                        label: {
                                            show: true,
                                            textStyle: {
                                                fontSize: '15',
                                                fontFamily: '微软雅黑',
                                                fontWeight: 'bold'
                                            }
                                        }
                                    }
                                },
                                data: TotalPiece
                            }
                        ]
                    });
                }
            )
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--此div用于在界面未完全加载样式前显示内容--%>
    <div id='Loading' style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 100%; height: 100%; background: white; text-align: center; padding: 5px 10px; display: table;">
        <div style="display: table-cell; vertical-align: middle">
            <h1><font size="9">页面加载中……</font></h1>
        </div>
    </div>
    <%--此div用于在界面未完全加载样式前显示内容--%>
    <%--此div用于显示查询条件--%>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table id="saPanelTab">
            <tr>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="HouseID" name="HouseID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'HouseID',textField:'HouseName',editable:false" />
                </td>
                <td style="text-align: right;">订单类型:
                </td>
                <td>
                    <select class="easyui-combobox" id="ThrowGood" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="0">客户单</option>
                        <option value="12">OES客户单</option>
                        <option value="15">速配单</option>
                    </select>
                </td>
                <td style="text-align: right;">订单时间:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px" />~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px" />
                </td>
            </tr>
        </table>
    </div>
    <%--此div用于显示查询条件--%>
    <%--此div用于显示按钮操作--%>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
    </div>
    <%--此div用于显示按钮操作--%>
    <div style="float: left; width: 50%;">
        <table id="dg" class="easyui-datagrid">
        </table>
    <div style="float: left; width: 100%;">
        <div id="main" style="height: 360px; border: 0px solid #ccc;" />
    </div>
    <div>
        <div id="main2" style="height: 360px; border: 0px solid #ccc;" />
    </div>
    </div>
</asp:Content>
