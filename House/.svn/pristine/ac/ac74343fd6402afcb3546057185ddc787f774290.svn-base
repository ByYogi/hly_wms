<%@ Page Title="线网关系" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StockFileDownload.aspx.cs" Inherits="Cargo.House.StockFileDownload" %>

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
        //页面加载时执行
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
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    { title: '', field: 'ID', checkbox: true, width: '5%' },
                    {
                        title: '文件名称', field: 'FileName', width: '250px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },

                    { title: '操作时间', field: 'OP_DATE', width: '250px', formatter: DateTimeFormatter },
                    {
                        title: '文件名称', field: 'FileName', width: '250px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                ]],
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });

            var datenow = new Date();
            $('#HouseStartDate').datebox('setValue', getNowFormatDate(datenow.getFullYear().toString() + "-" + (datenow.getMonth() + 1).toString() + "-01"));
            $('#HouseEndDate').datebox('setValue', getNowFormatDate(datenow));
        });
        //查询
        function dosearch() {
            var StartDate = $('#HouseStartDate').datebox('getValue')
            var EndDate = $('#HouseEndDate').datebox('getValue')
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'houseApi.aspx?method=QueryStockFile';
            $('#dg').datagrid('load', {
                FileName: $('#FileName').textbox('getValue'),
                StartDate: StartDate,
                EndDate: EndDate,
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
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table>
            <tr>
                <td style="text-align: right;">文件名称:
                </td>
                <td>
                    <input id="FileName" class="easyui-textbox" style="width: 120px;" data-options="prompt:'请输入文件名称'" />
                </td>
                <td style="text-align: right;">日期:
 </td>
 <td>
     <input id="HouseStartDate" class="easyui-datebox" style="width: 100px" />~<input id="HouseEndDate" class="easyui-datebox" style="width: 100px" />
 </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">
            更&nbsp;&nbsp;新</a>&nbsp;&nbsp;
        <%--<a href="#" class="easyui-linkbutton"
                    iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>--%>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 500px; height: 400px; padding: 0px"
        closed="true" buttons="#dlg-buttons">
        <div id="saPanel">
            <form id="fm" class="easyui-form" method="post">
                <input type="hidden" name="MID" />
                <input type="hidden" name="ClientName" id="ClientName" />
                <input type="hidden" name="ShopCode" id="ShopCode" />
                <table>
                    <tr>
                        <td style="text-align: right;">区域大仓:
                        </td>
                        <td>
                            <input id="HouseID" name="HouseID" class="easyui-combobox" style="width: 250px;" panelheight="auto" />
                        </td>

                    </tr>
                    <tr>
                        <td style="text-align: right;">客户名称:
                        </td>
                        <td>
                            <input name="ClientNum" id="ClientNum" class="easyui-combobox" style="width: 250px;" />
                        </td>
                    </tr>
                    <%--   <tr>
                        <td style="text-align: right;">省份:
                        </td>
                        <td>
                            <input name="Province" id="Province" class="easyui-combobox" style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">城市:
                        </td>
                        <td>
                            <input name="City" id="City" class="easyui-combobox" style="width: 250px;"
                                data-options="valueField:'City',textField:'City'" />
                        </td>
                    </tr>--%>

                    <tr>
                        <td style="text-align: right;">所属仓库:
                        </td>
                        <td>
                            <input id="AreaID" name="AreaID" class="easyui-combobox" style="width: 250px;" data-options="valueField:'AreaID',textField:'Name',required:true"
                                panelheight="auto" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">出库级别:
                        </td>
                        <td>
                            <select class="easyui-combobox" name="OrderLevel" id="OrderLevel" editable="false" style="width: 250px;"
                                panelheight="auto" required="true">
                                <option value="1">一级</option>
                                <option value="2">二级</option>
                                <option value="3">三级</option>
                                <option value="4">四级</option>
                                <option value="5">五级</option>
                            </select>
                        </td>
                    </tr>

                </table>
            </form>
        </div>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">&nbsp;&nbsp;保&nbsp;存&nbsp;&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;&nbsp;取&nbsp;消&nbsp;&nbsp;</a>
    </div>

    <script type="text/javascript">
        //新增线网关系
        function addItem() {
            $('#dlg').dialog('open').dialog('setTitle', '新增线网关系');
            $('#fm').form('clear');
            $('#DelFlag').combobox('select', '0');

            ////省市区三级联动
            //$('#Province').combobox({
            //    url: '../House/houseApi.aspx?method=QueryCityData',
            //    valueField: 'City', textField: 'City',
            //    onSelect: function (rec) {
            //        $('#City').combobox('textbox').bind('focus', function () { $('#City').combobox('showPanel'); });
            //        $('#City').combobox('clear');
            //        var url = '../House/houseApi.aspx?method=QueryCityData&PID=' + rec.ID;
            //        $('#City').combobox('reload', url);
            //    }
            //});
            //区域大仓
            $('#HouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#AreaID').combobox('textbox').bind('focus', function () { $('#AreaID').combobox('showPanel'); });
                    $('#AreaID').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    $('#AreaID').combobox('reload', url);

                    $('#ClientNum').combobox({
                        valueField: 'ClientNum', textField: 'ClientShortName', 
                        url: '../Client/clientApi.aspx?method=AutoCompleteClient&BelongHouse=6',
                        onSelect: onClientChanged,
                        required: true
                    });
                    $('#ClientNum').combobox('textbox').bind('focus', function () { $('#ClientNum').combobox('showPanel'); });
                }
            });
            //$('#Province').combobox('textbox').bind('focus', function () { $('#Province').combobox('showPanel'); });
            $('#HouseID').combobox('textbox').bind('focus', function () { $('#HouseID').combobox('showPanel'); });
        }

        function onClientChanged(item) {
            $("#ClientName").val(item.ClientName);
            $("#ShopCode").val(item.ShopCode);
        }

        //修改线网关系
        function editItem() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改线网关系');
                $('#fm').form('load', row);
            }
        }
        //修改线网关系
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改线网关系');
                $('#fm').form('load', row);

                $('#HouseID').combobox({
                    url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                    valueField: 'HouseID', textField: 'Name',
                    onSelect: function (rec) {
                        $('#AreaID').combobox('textbox').bind('focus', function () { $('#AreaID').combobox('showPanel'); });
                        $('#AreaID').combobox('clear');
                        var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                        $('#AreaID').combobox('reload', url);

                        $('#ClientNum').combobox({
                            valueField: 'ClientNum', textField: 'ClientShortName',
                            url: '../Client/clientApi.aspx?method=AutoCompleteClient&BelongHouse=6',
                            onSelect: onClientChanged,
                            required: true
                        });
                        $('#ClientNum').combobox('textbox').bind('focus', function () { $('#ClientNum').combobox('showPanel'); });
                    }
                });
                var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + row.HouseID;
                $('#AreaID').combobox('reload', url);

                $('#ClientNum').combobox({
                    valueField: 'ClientNum', textField: 'ClientShortName',
                    url: '../Client/clientApi.aspx?method=AutoCompleteClient&BelongHouse=6',
                    onSelect: onClientChanged,
                    required: true
                });

                $('#HouseID').combobox('setValue', row.HouseID);
                $('#AreaID').combobox('setValue', row.AreaID);
                $('#ClientNum').combobox('setValue', row.ClientNum);

            }
        }
        //删除线网关系
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
                        url: 'houseApi.aspx?method=DelLineNetMapp',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
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

        //保存线网关系
        function saveItem() {
            $('#fm').form('submit', {
                url: 'houseApi.aspx?method=SaveLineNetMapp',
                onSubmit: function () {
                    return $(this).form('enableValidation').form('validate');
                },
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
