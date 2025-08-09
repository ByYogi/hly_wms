<%@ Page Title="来货订单条码" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FactoryOrderBarcode.aspx.cs" Inherits="Cargo.Order.FactoryOrderBarcode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
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
        //页面加载显示遮罩层防止用户看见未加载CSS的页面
        var pc;
        $.parser.onComplete = function () {
            if (pc) {
                clearTimeout(pc);
            }
            pc = setTimeout(closemask, 10);
        }
        //加载完成后关闭遮罩层
        function closemask() {
            $("#Loading").fadeOut("normal", function () {
                $(this).remove();
            });
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
                fitColumns: true, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                  {
                      title: '', field: 'ID', checkbox: true, width: '30px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '船运号', field: 'VehicleNo', width: '10%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '轮胎条码', field: 'Barcode', width: '10%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '规格', field: 'Specs', width: '10%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '花纹', field: 'Figure', width: '10%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '批次', field: 'Batch', width: '5%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '货品代码', field: 'GoodsCode', width: '10%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '载重', field: 'LoadIndex', width: '5%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '速度', field: 'SpeedLevel', width: '5%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  { title: '出库时间', field: 'OutTime', width: '10%', formatter: DateTimeFormatter },
                  { title: '操作时间', field: 'OP_DATE', width: '10%', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
            });

            //列表回车响应查询
            $("#saPanelTab").keydown(function (e) { if (e.keyCode == 13) { dosearch(); } });

            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
        });

        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=QueryBarcodeData';
            $('#dg').datagrid('load', {
                VehicleNo: $('#VehicleNo').val(),
                Specs: $("#Specs").val(),
                Figure: $("#Figure").val(),
                Batch: $("#Batch").val(),
                GoodsCode: $("#GoodsCode").val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $("#EndDate").datebox('getValue')
            });
        }
        //新增
        function addItem() {
            $('#dlgAdd').form('clear');
            $('#dlgAdd').dialog('open').dialog('setTitle', '导入来货订单条码');
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
                <td style="text-align: right;">船运号:
                </td>
                <td>
                    <input id="VehicleNo" class="easyui-textbox" data-options="prompt:'请输入船运号'" style="width: 100px">
                </td>
                <td style="text-align: right;">规格:
                </td>
                <td>
                    <input id="Specs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 100px">
                </td>
                <td style="text-align: right;">花纹:
                </td>
                <td>
                    <input id="Figure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 100px">
                </td>
                <td style="text-align: right;">批次:
                </td>
                <td>
                    <input id="Batch" class="easyui-textbox" data-options="prompt:'请输入批次'" style="width: 100px">
                </td>
                <td style="text-align: right;">货品代码:
                </td>
                <td>
                    <input id="GoodsCode" class="easyui-textbox" data-options="prompt:'请输入货品代码'" style="width: 100px">
                </td>
                <td style="text-align: right;">出库时间:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px">
                </td>
            </tr>
        </table>
    </div>
    <%--此div用于显示查询条件--%>
    <table id="dg" class="easyui-datagrid">
    </table>
    <%--此div用于显示按钮操作--%>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">&nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="Export()">&nbsp;导出轮胎码&nbsp;</a>&nbsp;&nbsp;
        <form runat="server" id="fm1">
            <asp:Button ID="btnBarcode" runat="server" Style="display: none;" Text="导出" OnClick="btnBarcode_Click" />
        </form>
    </div>
    <%--此div用于显示按钮操作--%>

    <!--Begin 导入来货订单条码-->
    <div id="dlgAdd" class="easyui-dialog" style="width: 400px; height: 160px; padding: 2px 2px"
        closed="true" buttons="#dlgAdd-buttons">
        <div id="saPanel" class="easyui-panel" title="">
            <table>
                <tr>
                    <td style="text-align: right;">船运单号:
                    </td>
                    <td>
                        <input id="AVehicleNo" class="easyui-textbox" data-options="prompt:'请输入船运单号',required:true" style="width: 100px">
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="dlgAdd-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="Import()">&nbsp;导&nbsp;入&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgAdd').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <!--Begin 导入来货订单条码-->
    <script type="text/javascript">
        function Import() {
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定要导入当前船运号代码？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在导入中...' });
                    $.ajax({
                        url: 'orderApi.aspx?method=ImportBarcode&VehicleNo=' + $('#AVehicleNo').val(),
                        dataType: 'json', 
                        success: function (text) {
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '导入成功!', 'info');
                                $('#dg').datagrid('clearSelections');
                                $('#dg').datagrid('reload');
                                $('#dlgAdd').dialog('close');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                            $.messager.progress("close");
                        }
                    });
                }
            });
        }

        function Export() {
            $.ajax({
                url: "orderApi.aspx?method=QueryBarcodeDataExport&VehicleNo=" + $('#VehicleNo').val() + "&Specs=" + $('#Specs').val() + "&Figure=" + $("#Figure").val() + "&Batch=" + $("#Batch").val() + "&GoodsCode=" + $("#GoodsCode").val() + "&StartDate=" + $('#StartDate').datebox('getValue') + "&EndDate=" + $('#EndDate').datebox('getValue'),
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=btnBarcode.ClientID %>"); obj.click() }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }
    </script>
</asp:Content>
