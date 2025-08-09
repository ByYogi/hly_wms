<%@ Page Title="每日库存统计" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reportDayStockStatis.aspx.cs" Inherits="Cargo.Report.reportDayStockStatis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        window.onload = function () {
            adjustment();
        }
        $(window).resize(function () {
            adjustment();
        });
        function adjustment() {
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - $("div[name='SelectDiv2']").outerHeight(true);
            $('#dg').datagrid({ height: height });
        }
        $(document).ready(function () {
            $('#dg').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'StatisDis',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    {
                        title: '年月', field: 'StatisDis', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '仓库名称', field: 'Name', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '仓库租金', field: 'CurRentMoney', width: '100px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                ]],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#AHID').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    $('#AHID').combobox('reload', url);
                }
            });
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            $('#AHID').combobox('textbox').bind('focus', function () { $('#AHID').combobox('showPanel'); });
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'reportApi.aspx?method=QueryDayStockData';
            $('#dg').datagrid('load', {
                HouseID: $("#AHouseID").combobox('getValue'),
                Name: $("#AHID").combobox('getText'),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue')
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
    <div id="SelectDiv2" name="SelectDiv2" style="background: linear-gradient(to bottom, #eff5ff 0px, #e0ecff 100%) repeat-x scroll 0 0; border-color: #95b8e7; border-style: solid; border-width: 1px 1px 0px 1px;">
        <table>
            <tr>
                <td>
                    <a class="easyui-linkbutton" iconcls="icon-chart_bar" plain="false" href="../Report/ReportStocktaking.aspx"
                        target="_self">&nbsp;仓库盘点&nbsp;</a>&nbsp;&nbsp;<a id="saleManReport" class="easyui-linkbutton" iconcls="icon-chart_bar"
                            plain="false" style="color: Red;" href="../Report/reportDayStockStatis.aspx" target="_self">
                            &nbsp;每日库存统计&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table>
            <tr>
                <td style="text-align: right;">区域大仓:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;" />
                </td>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="AHID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'AreaID',textField:'Name'" panelheight="auto" />
                </td>
                <td style="text-align: right;">查询日期:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" data-options="prompt:'请输入开始月'" style="width: 100px" />~<input id="EndDate" class="easyui-datebox" data-options="prompt:'请输入结束月'" style="width: 100px" />
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>
    </div>
    <!--客户收货地址-->
    <div id="dlgCargoStock" class="easyui-dialog" style="width: 86%; height: 550px; padding: 0px 0px"
        closed="true" buttons="#dlgCargoStock-buttons">
        <table id="dgCargoStock" class="easyui-datagrid">
        </table>
    </div>
    <div id="dlgCargoStock-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="AwbExport()">&nbsp;导&nbsp;出&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgCargoStock').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
        <form runat="server" id="fm1">
            <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
        </form>
    </div>
    <!--客户收货地址-->
    <script type="text/javascript">
        //导出数据
        function AwbExport() {
            var row = $("#dgCargoStock").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            $.messager.progress({ msg: '请稍后,正在导出中...' });
            var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click();
            $.messager.progress("close");

        }
        //查看详细每月库存数据
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                showGrid(row.HouseID);
                $('#dgCargoStock').datagrid('loadData', { total: 0, rows: [] });
                $('#dlgCargoStock').dialog('open').dialog('setTitle', row.Name + row.StatisDis + '仓库库存统计');
                var gridOpts = $('#dgCargoStock').datagrid('options');
                gridOpts.url = 'reportApi.aspx?method=QueryDayStockListData&Name=' + row.Name + '&StartDate=' + row.StatisDis;
            }
        }
        function showGrid(HID) {
            var columns = [];
            columns.push({
                title: '日期时间', field: 'StatisDate', width: '80px', formatter: DateFormatter
            });
            columns.push({
                title: '所属仓库', field: 'Name', width: '90px', formatter: function (value) {
                    if (value != null && value != "") { return "<span title='" + value + "'>" + value + "</span>"; }
                }
            });
            columns.push({
                title: '当日库存', field: 'TotalSum', width: '70px', align: 'right', formatter: function (value) {
                    if (value != null && value != "") { return "<span title='" + value + "'>" + value + "</span>"; }
                }
            });
            if (HID == "59") {
                columns.push({ title: '大胎库存', field: 'BigTyreSum', width: '70px', align: 'right' });
                columns.push({ title: '小胎库存', field: 'SmallTyreSum', width: '70px', align: 'right' });
            } else {
                columns.push({
                    title: '库存价值', field: 'TotalValue', width: '90px', align: 'right', formatter: function (value) {
                        if (value != null && value != "") { return "<span title='" + value + "'>" + value + "</span>"; }
                    }
                });
                columns.push({
                    title: '马牌库存', field: 'MaPaiSum', width: '70px', align: 'right', formatter: function (value) {
                        if (value != null && value != "") { return "<span title='" + value + "'>" + value + "</span>"; }
                    }
                });
                columns.push({
                    title: '库存价值', field: 'MaPaiTotalValue', width: '90px', align: 'right', formatter: function (value) {
                        if (value != null && value != "") { return "<span title='" + value + "'>" + value + "</span>"; }
                    }
                });
                columns.push({
                    title: '优科库存', field: 'YKHMSum', width: '70px', align: 'right', formatter: function (value) {
                        if (value != null && value != "") { return "<span title='" + value + "'>" + value + "</span>"; }
                    }
                });
                columns.push({
                    title: '库存价值', field: 'YKHMTotalValue', width: '90px', align: 'right', formatter: function (value) {
                        if (value != null && value != "") { return "<span title='" + value + "'>" + value + "</span>"; }
                    }
                });
                columns.push({
                    title: '韩泰库存', field: 'HanTaiSum', width: '70px', align: 'right', formatter: function (value) {
                        if (value != null && value != "") { return "<span title='" + value + "'>" + value + "</span>"; }
                    }
                });
                columns.push({
                    title: '库存价值', field: 'HanTaiTotalValue', width: '90px', align: 'right', formatter: function (value) {
                        if (value != null && value != "") { return "<span title='" + value + "'>" + value + "</span>"; }
                    }
                });
                columns.push({
                    title: '固铂库存', field: 'GuBoSum', width: '70px', align: 'right', formatter: function (value) {
                        if (value != null && value != "") { return "<span title='" + value + "'>" + value + "</span>"; }
                    }
                });
                columns.push({
                    title: '库存价值', field: 'GuBoTotalValue', width: '90px', align: 'right', formatter: function (value) {
                        if (value != null && value != "") { return "<span title='" + value + "'>" + value + "</span>"; }
                    }
                });
                columns.push({
                    title: '米其林库存', field: 'MQLSum', width: '70px', align: 'right', formatter: function (value) {
                        if (value != null && value != "") { return "<span title='" + value + "'>" + value + "</span>"; }
                    }
                });
                columns.push({
                    title: '库存价值', field: 'MQLTotalValue', width: '90px', align: 'right', formatter: function (value) {
                        if (value != null && value != "") { return "<span title='" + value + "'>" + value + "</span>"; }
                    }
                });
                columns.push({
                    title: '沃凯途库存', field: 'WKTSum', width: '70px', align: 'right', formatter: function (value) {
                        if (value != null && value != "") { return "<span title='" + value + "'>" + value + "</span>"; }
                    }
                });
                columns.push({
                    title: '库存价值', field: 'WKTTotalValue', width: '90px', align: 'right', formatter: function (value) {
                        if (value != null && value != "") { return "<span title='" + value + "'>" + value + "</span>"; }
                    }
                }); columns.push({
                    title: '其它库存', field: 'QiTaSum', width: '70px', align: 'right', formatter: function (value) {
                        if (value != null && value != "") { return "<span title='" + value + "'>" + value + "</span>"; }
                    }
                });
                columns.push({
                    title: '库存价值', field: 'QiTaTotalValue', width: '90px', align: 'right', formatter: function (value) {
                        if (value != null && value != "") { return "<span title='" + value + "'>" + value + "</span>"; }
                    }
                });
            }
            $('#dgCargoStock').datagrid({
                width: '100%',
                height: '440px',
                title: '', //标题内容
                pagination: false, //分页是否显示
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                rownumbers: true,
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                columns: [columns],
                onClickRow: function (index, row) {
                    $('#dgCargoStock').datagrid('clearSelections');
                    $('#dgCargoStock').datagrid('selectRow', index);
                }
            })
        }
        $(function () {
            $('#StartDate').datebox({
                //显示日趋选择对象后再触发弹出月份层的事件，初始化时没有生成月份层
                onShowPanel: function () {
                    //触发click事件弹出月份层
                    span.trigger('click');
                    if (!tds)
                        //延时触发获取月份对象，因为上面的事件触发和对象生成有时间间隔
                        setTimeout(function () {
                            tds = p.find('div.calendar-menu-month-inner td');
                            tds.click(function (e) {
                                //禁止冒泡执行easyui给月份绑定的事件
                                e.stopPropagation();
                                //得到年份
                                var year = /\d{4}/.exec(span.html())[0],
                                    //月份
                                    //之前是这样的month = parseInt($(this).attr('abbr'), 10) + 1; 
                                    month = parseInt($(this).attr('abbr'), 10);

                                //隐藏日期对象                     
                                $('#StartDate').datebox('hidePanel').datebox('setValue', year + '-' + month);
                            });
                        }, 0);
                },
                //配置parser，返回选择的日期
                parser: function (s) {
                    if (!s) return new Date();
                    var arr = s.split('-');
                    return new Date(parseInt(arr[0], 10), parseInt(arr[1], 10) - 1, 1);
                },
                //配置formatter，只返回年月 之前是这样的d.getFullYear() + '-' +(d.getMonth()); 
                formatter: function (d) {
                    var currentMonth = (d.getMonth() + 1);
                    var currentMonthStr = currentMonth < 10 ? ('0' + currentMonth) : (currentMonth + '');
                    return d.getFullYear() + '-' + currentMonthStr;
                }
            });
            //日期选择对象
            var p = $('#StartDate').datebox('panel'),
                //日期选择对象中月份
                tds = false,
                //显示月份层的触发控件
                span = p.find('span.calendar-text');

        });
        $(function () {
            $('#EndDate').datebox({
                //显示日趋选择对象后再触发弹出月份层的事件，初始化时没有生成月份层
                onShowPanel: function () {
                    //触发click事件弹出月份层
                    span.trigger('click');
                    if (!btds)
                        //延时触发获取月份对象，因为上面的事件触发和对象生成有时间间隔
                        setTimeout(function () {
                            btds = p.find('div.calendar-menu-month-inner td');
                            btds.click(function (e) {
                                //禁止冒泡执行easyui给月份绑定的事件
                                e.stopPropagation();
                                //得到年份
                                var year = /\d{4}/.exec(span.html())[0],
                                    //月份
                                    //之前是这样的month = parseInt($(this).attr('abbr'), 10) + 1; 
                                    month = parseInt($(this).attr('abbr'), 10);

                                //隐藏日期对象                     
                                $('#EndDate').datebox('hidePanel').datebox('setValue', year + '-' + month);
                            });
                        }, 0);
                },
                //配置parser，返回选择的日期
                parser: function (s) {
                    if (!s) return new Date();
                    var arr = s.split('-');
                    return new Date(parseInt(arr[0], 10), parseInt(arr[1], 10) - 1, 1);
                },
                //配置formatter，只返回年月 之前是这样的d.getFullYear() + '-' +(d.getMonth()); 
                formatter: function (d) {
                    var currentMonth = (d.getMonth() + 1);
                    var currentMonthStr = currentMonth < 10 ? ('0' + currentMonth) : (currentMonth + '');
                    return d.getFullYear() + '-' + currentMonthStr;
                }
            });
            //日期选择对象
            var p = $('#EndDate').datebox('panel'),
                //日期选择对象中月份
                btds = false,
                //显示月份层的触发控件
                span = p.find('span.calendar-text');
        });
        //格式化日期
        function myformatter(date) {
            //获取年份
            var y = date.getFullYear();
            //获取月份
            var m = date.getMonth();
            return y + '-' + m;
        }
    </script>

</asp:Content>
