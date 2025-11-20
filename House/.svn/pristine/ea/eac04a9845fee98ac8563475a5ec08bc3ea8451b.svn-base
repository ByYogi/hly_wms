<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reportCityAgeingSet.aspx.cs" Inherits="Cargo.Report.reportCityAgeingSet" %>

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
            document.body.style.overflow = 'hidden';
            adjustment();
            document.body.style.overflow = 'auto';
        }
        $(window).resize(function () {
            document.body.style.overflow = 'hidden';
            adjustment();
            document.body.style.overflow = 'auto';
        });
        function adjustment() {
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - $("div[name='SelectDiv2']").outerHeight(true);
            $('#dg').datagrid({ height: height });
        }
        $(document).ready(function () {
            setDatagrid();

            //出发省市区三级联动
            $('#VProvince').combobox({
                url: '../House/houseApi.aspx?method=QueryCityData', valueField: 'City', textField: 'City',
                onSelect: function (rec) {
                    $('#VCity').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryCityData&PID=' + rec.ID;
                    $('#VCity').combobox('reload', url);
                }
            });
            //到达省市区三级联动
            $('#DProvince').combobox({
                url: '../House/houseApi.aspx?method=QueryCityData', valueField: 'City', textField: 'City',
                onSelect: function (rec) {
                    $('#DCity').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryCityData&PID=' + rec.ID;
                    $('#DCity').combobox('reload', url);
                }
            });

            $('#DepProvince').combobox({
                url: '../House/houseApi.aspx?method=QueryCityData', valueField: 'City', textField: 'City',
                onSelect: function (rec) {
                    $('#DepCity').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryCityData&PID=' + rec.ID;
                    $('#DepCity').combobox('reload', url);
                }
            });
            $('#DestProvince').combobox({
                url: '../House/houseApi.aspx?method=QueryCityData', valueField: 'City', textField: 'City',
                onSelect: function (rec) {
                    $('#DestCity').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryCityData&PID=' + rec.ID;
                    $('#DestCity').combobox('reload', url);
                }
            });
            $('#DProvince').combobox('textbox').bind('focus', function () { $('#DProvince').combobox('showPanel'); });
            $('#DCity').combobox('textbox').bind('focus', function () { $('#DCity').combobox('showPanel'); });
            $('#VProvince').combobox('textbox').bind('focus', function () { $('#VProvince').combobox('showPanel'); });
            $('#VCity').combobox('textbox').bind('focus', function () { $('#VCity').combobox('showPanel'); });
            $('#DepProvince').combobox('textbox').bind('focus', function () { $('#DepProvince').combobox('showPanel'); });
            $('#DepCity').combobox('textbox').bind('focus', function () { $('#DepCity').combobox('showPanel'); });
            $('#DestProvince').combobox('textbox').bind('focus', function () { $('#DestProvince').combobox('showPanel'); });
            $('#DestCity').combobox('textbox').bind('focus', function () { $('#DestCity').combobox('showPanel'); });

        });
        function setDatagrid() {

            $('#dg').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - $("div[name='SelectDiv2']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - $("div[name='SelectDiv2']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - $("div[name='SelectDiv2']").outerHeight(true) - 58) / 28) * 2],
                fitColumns: true, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'MID',
                url: null,
                toolbar: '#toolbar',
                rownumbers: true,
                showFooter: true,
                columns: [[
                    { title: '出发省', field: 'DepProvince', width: '100px' },
                    { title: '出发城市', field: 'DepCity', width: '100px' },
                    { title: '到达省', field: 'DestProvince', width: '100px' },
                    { title: '到达城市', field: 'DestCity', width: '100px' },
                    { title: '到达时效(小时)', field: 'ArriveAgeHour', width: '90px', align: 'right' },
                    { title: '签收时效(小时)', field: 'SignAgeHour', width: '90px', align: 'right' },
                    { title: '操作时间', field: 'OPDate', width: '130px', formatter: DateTimeFormatter },

                ]],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                rowStyler: function (index, row) { },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });
        }
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'reportApi.aspx?method=QueryReportCityAgeingSet';
            $('#dg').datagrid('load', {
                DepProvince: $("#VProvince").combobox('getValue'),//仓库ID
                DepCity: $("#VCity").combobox('getValue'),
                DestProvince: $("#DProvince").combobox('getValue'),
                DestCity: $("#DCity").combobox('getValue'),
                DelFlag: $("#ADelFlag").combobox('getValue'),
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
    <div name="SelectDiv2" style="background: linear-gradient(to bottom, #eff5ff 0px, #e0ecff 100%) repeat-x scroll 0 0; border-color: #95b8e7; border-style: solid; border-width: 1px 1px 0px 1px;">
        <table>
            <tr>
                <td>
                    <a class="easyui-linkbutton" iconcls="icon-chart_bar" plain="false" href="../Report/reportOutTime.aspx" target="_self">&nbsp;时效统计&nbsp;</a>&nbsp;&nbsp;
                    <a id="saleManReport" class="easyui-linkbutton" iconcls="icon-chart_bar" plain="false" href="../Report/reportAgeingDetail.aspx" target="_self">&nbsp;时效明细&nbsp;</a>&nbsp;&nbsp;
                    <a id="cityAgeingSet" class="easyui-linkbutton" style="color: Red;" iconcls="icon-chart_bar" plain="false" href="../Report/reportCityAgeingSet.aspx" target="_self">&nbsp;时效设置&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table>
            <tr>

                <td style="text-align: right;">出发省:</td>
                <td>
                    <input id="VProvince" class="easyui-combobox" style="width: 90px;" />
                </td>
                <td style="text-align: right;">出发市:</td>
                <td>
                    <input id="VCity" class="easyui-combobox" style="width: 90px;" data-options="valueField:'City',textField:'City'" />
                </td>

                <td style="text-align: right;">到达省:</td>
                <td>
                    <input id="DProvince" class="easyui-combobox" style="width: 90px;" />
                </td>
                <td style="text-align: right;">到达市:</td>
                <td>
                    <input id="DCity" class="easyui-combobox" style="width: 90px;" data-options="valueField:'City',textField:'City'" />
                </td>

                <td style="text-align: right;">启用状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="ADelFlag" style="width: 80px;" panelheight="auto">
                        <option value="0">启用</option>
                        <option value="1">停用</option>
                        <option value="-1">全部</option>
                    </select>
                </td>
            </tr>
        </table>
    </div>
    <table id="dg">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">&nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
      <%--  <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="AwbExport()">&nbsp;导&nbsp;出&nbsp;</a>
        <form runat="server" id="fm1">
            <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
        </form>--%>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 500px; height: 400px; padding: 0px"
        closed="true" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" name="MID" />
            <div id="saPanel">
                <table>

                    <tr>
                        <td style="text-align: right;">出发省:
                        </td>
                        <td>
                            <input name="DepProvince" id="DepProvince" class="easyui-combobox" data-options="required:true"
                                style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">出发城市:
                        </td>
                        <td>
                            <input name="DepCity" id="DepCity" class="easyui-combobox" data-options="valueField:'City',textField:'City',required:true"
                                style="width: 250px;" />
                        </td>

                    </tr>
                    <tr>
                        <td style="text-align: right;">到达省:
                        </td>
                        <td>
                            <input name="DestProvince" id="DestProvince" class="easyui-combobox" data-options="required:true"
                                style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">到达城市:
                        </td>
                        <td>
                            <input name="DestCity" id="DestCity" class="easyui-combobox" data-options="valueField:'City',textField:'City',required:true"
                                style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">状态标识:
                        </td>
                        <td>
                            <select class="easyui-combobox" id="DelFlag" name="DelFlag" style="width: 250px;"
                                panelheight="auto" required="true">
                                <option value="0">启用</option>
                                <option value="1">停用</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">到达时效:
                        </td>
                        <td>
                            <input name="ArriveAgeHour" class="easyui-numberbox" data-options="min:0,precision:2" style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">签收时效:
                        </td>
                        <td>
                            <input name="SignAgeHour" class="easyui-numberbox" data-options="min:0,precision:2" style="width: 250px;" />
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">&nbsp;&nbsp;保&nbsp;存&nbsp;&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
     <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;&nbsp;取&nbsp;消&nbsp;&nbsp;</a>
    </div>
    <script type="text/javascript">
        //新增
        function addItem() {
            $('#dlg').dialog('open').dialog('setTitle', '新增城市时效配置');
            $('#fm').form('clear');
            $('#DelFlag').combobox('select', '0');
        }

        //修改城市时效配置
        function editItem() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改城市时效配置');
                $('#fm').form('load', row);
                $('#StockShareHouseID').combobox('setValue', row.StockShareHouseID);
            }
        }
        //修改城市时效配置
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改城市时效配置');

                $('#fm').form('load', row);

            }
        }
        //删除
        function DelItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'reportApi.aspx?method=DelReportCityAgeingSet',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                $('#dg').datagrid('reload');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }
        //保存
        function saveItem() {
            $('#fm').form('submit', {
                url: 'reportApi.aspx?method=SaveReportCityAgeingSet',
                onSubmit: function (param) { return $(this).form('enableValidation').form('validate'); },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $('#dlg').dialog('close'); 	// close the dialog
                        dosearch();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
    </script>
</asp:Content>
