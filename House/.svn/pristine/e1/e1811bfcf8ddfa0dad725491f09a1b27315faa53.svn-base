<%@ Page Title="汽修合作推广" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AutoCarSpread.aspx.cs" Inherits="Cargo.SecKill.AutoCarSpread" %>


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
            $('#dg').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: 20, //每页多少条
                pageList: [20, 50],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                  { title: '', field: 'ID', checkbox: true, width: '30px' },
                  { title: '登记车主微信名', field: 'wxName', width: '120px' },
                  { title: '登记车牌号码', field: 'CarNum', width: '100px' },
                  { title: '登记车主手机号码', field: 'Cellphone', width: '100px' },
                  {
                      title: '使用状态', field: 'UseStatus', width: '60px',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "未使用"; }
                          else if (val == "1") { return "已使用"; }
                          else { return "未使用"; }
                      }
                  },
                  {
                      title: '汽修公司', field: 'Company', width: '120px',
                      formatter: function (val, row, index) {
                          if (val == "1") { return "广州信达汽修"; }
                          else if (val == "2") { return "长沙迪乐泰汽车美容"; }
                          else if (val == "3") { return "广州车轮馆"; }
                          else { return ""; }
                      }
                  },
                  { title: '上级微信名', field: 'OneWxName', width: '120px' },
                  { title: '登记时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'secKillApi.aspx?method=QueryAutoCarSpread';
            $('#dg').datagrid('load', {
                WxName: $('#WxName').val(),
                CarNum: $("#CarNum").val(),
                Cellphone: $("#Cellphone").val(),
                OneWxName: $("#OneWxName").val(),
                UseStatus: $("#UseStatus").combobox('getValue')
            });
            adjustment();
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
                <td style="text-align: right;">微信名称:
                </td>
                <td>
                    <input id="WxName" class="easyui-textbox" data-options="prompt:'请输入微信名称'" style="width: 80px">
                </td>
                <td style="text-align: right;">车牌号码:
                </td>
                <td>
                    <input id="CarNum" class="easyui-textbox" data-options="prompt:'请输入车牌号码'" style="width: 100px">
                </td>
                <td style="text-align: right;">手机号码:
                </td>
                <td>
                    <input id="Cellphone" class="easyui-textbox" data-options="prompt:'请输入手机号码'" style="width: 100px">
                </td>
                <td style="text-align: right;">上级微信名称:
                </td>
                <td>
                    <input id="OneWxName" class="easyui-textbox" data-options="prompt:'请输入上级微信名称'" style="width: 100px">
                </td>
                <td style="text-align: right;">使用状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="UseStatus" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">未使用</option>
                        <option value="1">已使用</option>
                    </select>
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-lock_add" plain="false" onclick="setUseStatus()">
            &nbsp;设置已使用&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
                iconcls="icon-lock_open" plain="false" onclick="setUnUseStatus()">&nbsp;设置未使用&nbsp;</a>
    </div>

    <script type="text/javascript">

        //设置未使用
        function setUnUseStatus() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要操作的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定设置未使用？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'secKillApi.aspx?method=setUnUseStatus',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '设置成功!', 'info');
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
        //设置使用
        function setUseStatus() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要操作的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定设置为已使用？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'secKillApi.aspx?method=setUseStatus',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '设置成功!', 'info');
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
    </script>
</asp:Content>
