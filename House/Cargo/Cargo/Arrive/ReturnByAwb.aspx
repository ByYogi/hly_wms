<%@ Page Title="按运单录入回单" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReturnByAwb.aspx.cs" Inherits="Cargo.Arrive.ReturnByAwb" %>
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
                var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 30;
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
        $(document).ready(function() {
            $('#dgTransit').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                rownumbers: true,
                idField: 'AwbID',
                url: null,
                columns: [[
                  { title: '', field: 'AwbID', checkbox: true, width: '30px' },
                  { title: '实际签收时间', field: 'SignTime', width: '140px', formatter: DateTimeFormatter, editor: 'datetimebox' },
                  { title: '回单信息', field: 'ReturnInfo', width: '70px', editor: 'text' },
                  { title: '实收金额', field: 'ActMoney', width: '70px', editor: { type: 'numberbox', options: { precision: 2}} },
                  { title: '签收人', field: 'Signer', width: '70px', editor: 'text' },
                  { title: '回单状态', field: 'ReturnStatus', width: '70px',
                      formatter: function(val, row, index) { if (val == "N") { return "回单未录"; } else if (val == "Y") { return "回单已录"; } else { return ""; } }
                  },
                  { title: '运单号', field: 'AwbNo', width: '70px' },
                  { title: '开单日期', field: 'HandleTime', width: '80px', formatter: DateFormatter },
                  { title: '客户名称', field: 'ShipperUnit', width: '100px' },
                  { title: '运输费用', field: 'TransportFee', width: '70px' },
                  { title: '总费用', field: 'TotalCharge', width: '70px' },
                  { title: '结款方式', field: 'CheckOutType', width: '60px',
                      formatter: function(val, row, index) {
                          if (val == "0") { return "现付"; } else if (val == "1") { return "回单"; } else if (val == "2") { return "月结"; } else if (val == "3") { return "到付"; } else if (val == "4") { return "代收款"; } else { return ""; }
                      }
                  },
                 { title: '出发站', field: 'Dep', width: '50px' },
                 { title: '中间站', field: 'MidDest', width: '50px' },
                 { title: '到达站', field: 'Dest', width: '50px' },
                 { title: '中转站', field: 'Transit', width: '50px' },
                 { title: '品名', field: 'Goods', width: '70px' },
                 { title: '总件数', field: 'Piece', width: '50px' },
                 { title: '分批件数', field: 'AwbPiece', width: '60px' },
                 { title: '分批重量', field: 'AwbWeight', width: '60px' },
                 { title: '分批体积', field: 'AwbVolume', width: '60px' },
                ]],
                onClickCell: onClickCell,
                onLoadSuccess: function(data) {
                    $('#dgTransit').datagrid('clearSelections');
                    var awbno = $('#AwbNo').textbox('getValue');
                    for (var i = 0; i < data.rows.length; i++) { if (awbno == data.rows[i].AwbNo) { $('#dgTransit').datagrid('selectRow', i); } }
                }
            });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#Dest').combobox('setValues', '<%=UserInfor.DepCity%>');
            $('#Dest').combobox('disable');
        });
        //查询
        function dosearch() {
            $('#dgTransit').datagrid('clearSelections');
            var gridOpts = $('#dgTransit').datagrid('options');
            gridOpts.url = '../Arrive/ArriveApi.aspx?method=QueryAwbForReturn';
            $('#dgTransit').datagrid('load', {
                AwbNo: $('#AwbNo').textbox('getValue'),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                ShipperUnit: $('#ShipperUnit').textbox('getValue'),
                Dest: $("#Dest").combobox('getValue')
            });
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
        <div style="background: linear-gradient(to bottom, #eff5ff 0px, #e0ecff 100%) repeat-x scroll 0 0;
        border-color: #95b8e7; border-style: solid; border-width: 1px 1px 0px 1px;">
        <table>
            <tr>
                <td>
                    <a class="easyui-linkbutton" iconcls="icon-lorry" plain="false" href="../Arrive/ReturnAwbManager.aspx" target="_self">&nbsp;按车辆录入回单&nbsp;</a>&nbsp;&nbsp;
                    <a class="easyui-linkbutton" iconcls="icon-application_form" plain="false" style="color: Red;" href="../Arrive/ReturnByAwb.aspx" target="_self">&nbsp;按运单录入回单&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    <div id="saPanel" class="easyui-panel" title="" data-options="iconCls:'icon-search'">
        <table>
            <tr>
                <td style="text-align: right;">
                    运单号码:
                </td>
                <td>
                    <input id="AwbNo" class="easyui-textbox" data-options="prompt:'请输入运单号'" style="width: 100px">
                </td>
                <td style="text-align: right;">
                    到达站:
                </td>
                <td>
                    <input id="Dest" class="easyui-combobox" style="width: 70px;" data-options="valueField:'CityName',textField:'CityName',url:'../Arrive/ArriveApi.aspx?method=QueryOnlyCity'"
                     />
                </td>
                <td style="text-align: right;">
                    客户名称:
                </td>
                <td>
                    <input id="ShipperUnit" class="easyui-textbox"  style="width: 100px">
                </td>
                <td style="text-align: right;">
                    时间范围:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px">
                </td>
                <td>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-ok" id="save" plain="false" onclick="SaveData()"> &nbsp;保&nbsp;存&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    <table id="dgTransit" class="easyui-datagrid">
    </table>

    <script type="text/javascript">
        //录入回单信息
        function SaveData() {
            editIndex == undefined;
            var rows = $('#dgTransit').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要保存的数据！', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定保存？', function (r) {
                if (r) {
                    var cr = $('#dgTransit').datagrid('getChanges');
                    $('#dgTransit').datagrid('acceptChanges');
                    var rowss = $('#dgTransit').datagrid('getSelections');
                    var json = JSON.stringify(rowss) + "|" + JSON.stringify(cr);
                    $.ajax({
                        url: "../Arrive/ArriveApi.aspx?method=AddReturnAwbInfo&ContractNum=''",
                        type: 'post', dataType: 'json', data: { data: json }, async: false,
                        success: function (text) {
                            if (text.Result == true) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '录入回单成功!', 'info'); }
                            else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning'); }
                        }
                    });
                }
            });
        }
        $.extend($.fn.datagrid.methods, {
            editCell: function (jq, param) {
                return jq.each(function () {
                    var fields = $(this).datagrid('getColumnFields');
                    for (var i = 0; i < fields.length; i++) {
                        var col = $(this).datagrid('getColumnOption', fields[i]);
                        col.editor1 = col.editor;
                        if (fields[i] != param.field) {
                            col.editor = null;
                        }
                    }
                    $(this).datagrid('beginEdit', param.index);
                    for (var i = 0; i < fields.length; i++) {
                        var col = $(this).datagrid('getColumnOption', fields[i]);
                        col.editor = col.editor1;
                    }
                });
            }
        });
        var editIndex = undefined;
        function endEditing() {
            if (editIndex == undefined) { return true }
            if ($('#dgTransit').datagrid('validateRow', editIndex)) {
                var rows = $("#dgTransit").datagrid('getData').rows[editIndex];
                var ed = $('#dgTransit').datagrid('getEditors', editIndex);
                var cg = ed[0];
                if (cg.field == "SignTime") {
                    if (CurentTimeHHMM(getDate(cg.target.datetimebox('getValue'))) == "00:00:00") {
                        cg.target.datetimebox('setValue', AllDateTime(new Date()));
                    }
                }
                $('#dgTransit').datagrid('endEdit', editIndex);
                editIndex = undefined;
                return true;
            } else {
                return false;
            }
        }
        function onClickCell(index, field) {
            var row = $("#dgTransit").datagrid('getData').rows[index];
            if (row.ReturnStatus == "N") {
                if (field == "SignTime" || field == "ReturnInfo" || field == "ActMoney" || field == "Signer") {
                    if (endEditing()) {
                        $('#dgTransit').datagrid('selectRow', index).datagrid('editCell', { index: index, field: field });
                        editIndex = index;
                    }
                }
                else {
                    if (editIndex == undefined) { return true }
                    $('#dgTransit').datagrid('endEdit', editIndex);
                    editIndex = undefined;
                }
            } else {
                if (editIndex == undefined) { return true }
                $('#dgTransit').datagrid('endEdit', editIndex);
                editIndex = undefined;
            }
        }
    </script>
</asp:Content>
