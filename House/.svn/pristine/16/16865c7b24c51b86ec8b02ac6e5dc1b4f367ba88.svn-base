<%@ Page Title="销售库存表" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reportSaleStock.aspx.cs" Inherits="Cargo.Report.reportSaleStock" %>

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
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true);
            $('#dg').datagrid({ height: height });
        }
        $(document).ready(function () {
            setDatagrid();
            //var datenow = new Date();
            //$('#StartDate').datebox('setValue', getNowFormatDate(datenow));
            //$('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            //一级产品
            $('#APID').combobox({
                url: '../Product/productApi.aspx?method=QueryALLOneProductType', valueField: 'TypeID', textField: 'TypeName',
                onSelect: function (rec) {
                    $('#ASID').combobox('clear');
                    var url = '../Product/productApi.aspx?method=QueryALLOneProductType&PID=' + rec.TypeID;
                    $('#ASID').combobox('reload', url);
                }
            });
            $('#APID').combobox('textbox').bind('focus', function () { $('#APID').combobox('showPanel'); });
            $('#ASID').combobox('textbox').bind('focus', function () { $('#ASID').combobox('showPanel'); });
        });
        //查询
        function dosearch() {
            if ($('#StartDate').datebox('getValue') == undefined || $('#StartDate').datebox('getValue') == '') {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择开始月份', 'warning');
                return;
            }
            if ($('#EndDate').datebox('getValue') == undefined || $('#EndDate').datebox('getValue') == '') {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择结束月份', 'warning');
                return;
            }
            if ($('#ASID').combobox('getValue') == undefined || $('#ASID').combobox('getValue') == '') {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择产品分类', 'warning');
                return;
            }
            var startDate = new Date($('#StartDate').datebox('getValue'));
            var endDate = new Date($('#EndDate').datebox('getValue'));
            var sYear = startDate.getFullYear();
            var eYear = endDate.getFullYear();
            var a = startDate.getMonth() + 1;
            var b = endDate.getMonth() + 1;
            var c = MonthsBetw($('#StartDate').datebox('getValue'), $('#EndDate').datebox('getValue'));
            var mulCol = [];
            var bt = [];
            var rc = [];

            for (var i = 1; i <= c; i++) {
                if (a == 12) {
                    var t = sYear + "-" + a;
                    var tit = { title: t, colspan: 2 };
                    var rcc = { title: '入库', field: 'In' + i.toString(), width: '60px', align: 'right' };
                    var ck = { title: '出库', field: 'Out' + i.toString(), width: '60px', align: 'right' };
                    bt.push(tit);
                    rc.push(rcc);
                    rc.push(ck);
                    a = 0;
                    sYear = eYear;
                }
                else {
                    var t = sYear + "-" + a;
                    var tit = { title: t, colspan: 2 };
                    var rcc = { title: '入库', field: 'In' + i.toString(), width: '60px', align: 'right' };
                    var ck = { title: '出库', field: 'Out' + i.toString(), width: '60px', align: 'right' };
                    bt.push(tit);
                    rc.push(rcc);
                    rc.push(ck);
                }
                a++;
            }
            mulCol.push(bt);
            mulCol.push(rc);
            //var s = [[{ "title": "20195", "colspan": 2 },], [{ title: '入库', field: 'Piece', width: '60px', align: 'right' }, { title: '出库', field: 'ProductName', width: '60px', align: 'right' }]];
            setDatagrid(mulCol);
            $('#dg').datagrid('clearSelections');
            //gridOpts.url = null;
            //$('#dg').datagrid({ 'columns': mulCol });
            var gridOpts = $('#dg').datagrid('options');

            gridOpts.url = 'reportApi.aspx?method=QuerySaleStockData&StartDate=' + $('#StartDate').datebox('getValue') + '&EndDate=' + $('#EndDate').datebox('getValue') + '&Specs=' + $('#ASpecs').val() + '&Figure=' + $('#AFigure').val() + '&PID=' + $("#APID").combobox('getValue') + '&SID=' + $("#ASID").combobox('getValue') + '&HouseID=' + $("#AHouseID").combobox('getValue');
        }
        function setDatagrid(mulCol) {
            var frozenColumns = [];
            frozenColumns.push({
                title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            frozenColumns.push({
                title: '花纹', field: 'Figure', width: '80px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            if ($('#ASID').combobox('getValue') == 9) {
                frozenColumns.push({
                    title: '货品代码', field: 'GoodsCode', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
            }
            frozenColumns.push({
                title: '总入库', field: 'InTotalNum', width: '70px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            frozenColumns.push({
                title: '总出库', field: 'OutTotalNum', width: '70px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            frozenColumns.push({
                title: '出库入库比', field: 'OutInRate', width: '70px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            frozenColumns.push({
                title: '当前库存数', field: 'CurrentNum', width: '70px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            $('#dg').datagrid({
                width: '100%',
                //height: '550px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: false, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                rownumbers: true,
                toolbar: '#toolbar',
                showFooter: true,
                frozenColumns: [frozenColumns],
                columns: mulCol
            });
        }
        //返回两个日期相差的月数
        function MonthsBetw(date1, date2) {
            //用-分成数组
            date1 = date1.split("-");
            date2 = date2.split("-");
            //获取年,月数
            var year1 = parseInt(date1[0]),
            month1 = parseInt(date1[1]),
            year2 = parseInt(date2[0]),
            month2 = parseInt(date2[1]),
            //通过年,月差计算月份差
            months = (year2 - year1) * 12 + (month2 - month1) + 1;
            return months;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id='Loading' style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 98%; height: 100%; background: white; text-align: center; padding: 5px 10px; display: table;">
        <div style="display: table-cell; vertical-align: middle">
            <h1><font size="9">页面加载中……</font></h1>
        </div>
    </div>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width:100%">
        <table>
            <tr>
                <td style="text-align: right;">规格:
                </td>
                <td>
                    <input id="ASpecs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 100px">
                </td>
                <td style="text-align: right;">花纹:
                </td>
                <td>
                    <input id="AFigure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 100px">
                </td>
                <td style="text-align: right;">一级产品:
                </td>
                <td>
                    <input id="APID" class="easyui-combobox" style="width: 100px;" data-options="required:true" />
                </td>
                <td style="text-align: right;">二级产品:
                </td>
                <td>
                    <input id="ASID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'TypeID',textField:'TypeName',required:true" />
                </td>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 90px;"
                        panelheight="auto" />
                </td>
                <td style="text-align: right;">日期范围:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px" data-options="prompt:'请选择开始月份',required:true">~
                        <input id="EndDate" class="easyui-datebox" style="width: 100px" data-options="prompt:'请选择结束月份',required:true">
                </td>
            </tr>
        </table>
    </div>
    <table id="dg">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-application_put"
            plain="false" onclick="AwbExport()">&nbsp;导&nbsp;出&nbsp;</a>
        <form runat="server" id="fm1">
            <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" /><asp:HiddenField ID="hiddenID" runat="server" Value="0" />
        </form>
    </div>
    <script type="text/javascript">
        //导出数据
        function AwbExport() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            var key = new Array();
            key[0] = $('#StartDate').datebox('getValue');
            key[1] = $('#EndDate').datebox('getValue');
            key[2] = $('#ASpecs').val();
            key[3] = $('#AFigure').val();
            key[4] = $("#APID").combobox('getValue');
            key[5] = $("#ASID").combobox('getValue');
            key[6] = $("#AHouseID").combobox('getValue');//仓库ID
            $.ajax({
                url: "reportApi.aspx?method=QuerySaleStockDataForExport&key=" + escape(key.toString()),
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click(); }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
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
