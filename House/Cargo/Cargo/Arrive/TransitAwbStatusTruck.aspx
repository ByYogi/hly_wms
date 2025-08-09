<%@ Page Title="中转状态跟踪" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TransitAwbStatusTruck.aspx.cs" Inherits="Cargo.Arrive.TransitAwbStatusTruck" %>

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
        $(document).ready(function () {
            $('#dg').datagrid({
                width: '100%',
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ArriveID',
                url: null,
                columns: [[
                  { title: '', field: 'ArriveID', checkbox: true, width: '30px' },
                  { title: '运单号', field: 'AwbNo', width: '70px' },
                  { title: '开单日期', field: 'HandleTime', width: '75px', formatter: DateFormatter },
                  { title: '最迟时效', field: 'LatestTimeLimit', width: '75px', formatter: DateFormatter },
                  { title: '发货单位', field: 'ShipperUnit', width: '90px' },
                  { title: '发货人', field: 'ShipperName', width: '70px' },
                  { title: '联系方式', field: 'ShipperPhone', width: '100px' },
                  { title: '收货人', field: 'AcceptPeople', width: '70px' },
                  { title: '联系方式', field: 'AcceptPhone', width: '100px' },
                  { title: '品名', field: 'Goods', width: '70px' },
                  { title: '总件数', field: 'Piece', width: '50px' },
                  { title: '分批件数', field: 'AwbPiece', width: '60px' },
                  { title: '分批重量', field: 'AwbWeight', width: '60px' },
                  { title: '分批体积', field: 'AwbVolume', width: '60px' },
                  { title: '出发站', field: 'Dep', width: '50px' },
                  { title: '中间站', field: 'MidDest', width: '50px', hidden: true },
                  { title: '到达站', field: 'Dest', width: '50px' },
                  { title: '中转站', field: 'Transit', width: '50px' },
                  {
                      title: '送货方式', field: 'DeliveryType', width: '60px',
                      formatter: function (val, row, index) { if (val == "0") { return "送货"; } else if (val == "1") { return "自提"; } else { return "送货"; } }
                  },
                  { title: '录单员', field: 'CreateAwb', width: '60px' },
//                  { title: '中转时间', field: 'StartTime', width: '130px', formatter: DateTimeFormatter },
//                  { title: '中转单号', field: 'TransitID', width: '60px' },
//                  { title: '承运商', field: 'CarrierShortName', width: '60px' },
//                  { title: '承运单号', field: 'AssistNum', width: '60px' },
//                  { title: '联系方式', field: 'DriverCellPhone', width: '100px' },
                  {
                      title: '运单状态', field: 'TruckFlag', width: '60px',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "在站"; }
                          else if (val == "1") { return "出发"; }
                          else if (val == "2") { return "在途"; }
                          else if (val == "3") { return "到达"; }
                          else if (val == "4") { return "结束"; }
                          else if (val == "5") { return "关注"; }
                          else if (val == "6") { return "送达"; }
                          else if (val == "7") { return "签收"; }
                          else if (val == "8") { return "配送"; }
                          else if (val == "9") { return "中转"; }
                          else if (val == "10") { return "回单发送"; }
                          else if (val == "11") { return "移库"; }
                          else if (val == "13") { return "异常"; }
                          else { return "在站"; }
                      }
                  },
                 { title: '中转/配送时间', field: 'TTime', width: '130px', formatter: DateTimeFormatter },
                  { title: '', field: 'AwbID', hidden: true }
                ]],
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onLoadSuccess: function (data) {

                }
            });
            $("input[name=TruckFlag]").click(function () { showCont(); });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#Dest').combobox('setValues', '<%=UserInfor.DepCity%>');
            $('#Dest').combobox('disable');
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../Arrive/ArriveApi.aspx?method=QueryTransitAwbStatusTrack';
            $('#dg').datagrid('load', {
                AwbNo: $('#AwbNo').val(),
                AssistNum: $('#AssistNum').val(),
                ShipperUnit: $("#ShipperUnit").val(),
                CarrierShortName: $('#CarrierShortName').val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                Dest: $("#Dest").combobox('getValue'),
                AwbStatus: $("#AwbStatus").combobox('getValue')

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
    <div id="saPanel" class="easyui-panel" title="" data-options="iconCls:'icon-search'">
        <table>
            <tr>
                <td style="text-align: right;">运单号:
                </td>
                <td>
                    <input id="AwbNo" class="easyui-textbox" data-options="prompt:'请输入运单号'" style="width: 100px">
                </td>
                <td style="text-align: right;">发货:
                </td>
                <td>
                    <input id="ShipperUnit" class="easyui-textbox" data-options="prompt:'发货单位/人'" style="width: 100px;" />
                </td>
                <td style="text-align: right;">承运商:
                </td>
                <td>
                    <input id="CarrierShortName" class="easyui-textbox" data-options="prompt:'请输入承运商'" style="width: 100px">
                </td>
                <td style="text-align: right;">时间范围:
                </td>
                <td colspan="3">
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="edit()">修改运单跟踪状态</a>
                </td>
                <td style="text-align: right;">承运单号:
                </td>
                <td>
                    <input id="AssistNum" class="easyui-textbox" data-options="prompt:'请输入承运单号'" style="width: 90px">
                </td>
                <td style="text-align: right;">到达站:
                </td>
                <td>
                    <input id="Dest" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../Arrive/ArriveApi.aspx?method=QueryAllCity'" />
                </td>
                <td style="text-align: right;">运单状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="AwbStatus" style="width: 70px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="3">到达</option>
                        <option value="8">配送</option>
                        <option value="9">中转</option>
                        <option value="6">送达</option>
                        <option value="13">异常</option>
                    </select>
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="dlg" class="easyui-dialog" style="width: 450px; height: 360px; padding: 3px 3px"
        closed="true" closable="false" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" name="AwbNo" id="AAwbNo" />
            <input type="hidden" name="ArriveID" />
            <input type="hidden" name="AwbID" id="AAwbID" />
            <input type="hidden" name="Dest" id="ADest" />
            <div id="saPanel">
                <table class="mini-toolbar" style="width: 100%;">
                    <tr>
                        <td rowspan="5" style="border-right: 1px solid #909aa6;">运<br />
                            单<br />
                            在<br />
                            途<br />
                            跟<br />
                            踪
                        </td>
                        <td align="left">&nbsp;&nbsp;状态：
                        </td>
                        <td align="left">
                            <input name="TruckFlag" id="ATruckFlag1" type="radio" value="9"><label for="ATruckFlag1">途中</label>
                            <input name="TruckFlag" id="ATruckFlag2" type="radio" value="6"><label for="ATruckFlag2">送达</label>
                            <input name="TruckFlag" id="ATruckFlag3" type="radio" value="13"><label for="ATruckFlag3">异常</label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">&nbsp;&nbsp;运单所在地:
                        </td>
                        <td align="left">
                            <input name="CurrentLocation" id="CurrentLocation" class="easyui-textbox" data-options="prompt:'请输入运单所在地'">
                        </td>
                    </tr>
                    <tr>
                        <td align="left">&nbsp;&nbsp;预计/实际:
                        </td>
                        <td align="left">
                            <input id="ArriveTime" name="ArriveTime" class="easyui-datetimebox" data-options="required:true"
                                style="width: 150px">到达
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <textarea name="DetailInfo" id="DetailInfo" cols="60" style="height: 35px;" class="mini-textarea"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <textarea name="txtDetailInfo" id="txtDetailInfo" readonly="true" cols="60" style="height: 140px;"
                                class="mini-textarea"></textarea>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" id="save" onclick="saveItem()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-cancel"
            onclick="closeDlg()">&nbsp;取&nbsp;消&nbsp;</a>
    </div>

    <script type="text/javascript">
        //关闭
        function closeDlg() {
            $('#dlg').dialog('close');
            $('#dg').datagrid('reload');
        }
        function showCont() {
            switch ($("input[name=TruckFlag]:checked").attr("id")) {
                case "ATruckFlag2": $("#CurrentLocation").textbox('setValue', $('#ADest').val()); break;
                case "ATruckFlag3": $("#CurrentLocation").textbox('setValue', $('#ADest').val()); break;
                default: $("#CurrentLocation").textbox('setValue', ''); break;
            }
        }
        //修改
        function edit() {
            var row = $('#dg').datagrid('getSelections');
            if (row == null || row == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning'); return; }
            if (row.length > 1) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择一条数据！', 'warning'); return; }
            if (row) {
                if (row[0].TruckFlag == "6") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', row[0].AwbNo + '运单状态为客户，不能修改', 'warning');
                    return;
                }
                if (row[0].TruckFlag == "3") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', row[0].AwbNo + '已经到达，不能修改', 'warning');
                    return;
                }
                $('#dlg').dialog('open').dialog('setTitle', '修改运单号：' + row[0].AwbNo + ' 中转跟踪状态');
                $('#fm').form('clear');
                $('#fm').form('load', row[0]);
                $.ajax({
                    async: false,
                    url: "../Arrive/ArriveApi.aspx?method=QueryAwbStatusTrack&id=" + row[0].AwbNo + "&awbid=" + row[0].AwbID,
                    cache: false,
                    success: function (text) { $('#txtDetailInfo').val(text); }
                });
            }
        }
        //保存
        function saveItem() {
            $('#fm').form('submit', {
                url: '../Arrive/ArriveApi.aspx?method=SaveAwbTruckStatus',
                onSubmit: function () {
                    var trd = $(this).form('enableValidation').form('validate');
                    if (trd) { $('#save').linkbutton('disable'); }
                    return trd;
                },
                success: function (msg) {
                    $('#save').linkbutton('enable');
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '更新成功!', 'info');
                        $.ajax({
                            async: false,
                            url: "../Arrive/ArriveApi.aspx?method=QueryAwbStatusTrack&id=" + escape($('#AAwbNo').val()) + "&awbid=" + escape($('#AAwbID').val()),
                            cache: false,
                            success: function (text) { $('#txtDetailInfo').val(text); }
                        });
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }

    </script>
</asp:Content>
