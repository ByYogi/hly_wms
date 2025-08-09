<%@ Page Title="中转运单管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TransitAwbManager.aspx.cs" Inherits="Cargo.Arrive.TransitAwbManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="../JS/Date/CheckActivX.js" type="text/javascript"></script>--%>
    <script src="../JS/Lodop/LodopFuncs.js" type="text/javascript"></script>

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
                idField: 'TransitID',
                url: null,
                columns: [[
                  { title: '', field: '', checkbox: true, width: '30px' },
                  { title: '中转单号', field: 'TransitID', width: '90px' },
                  { title: '外协单号', field: 'AssistNum', width: '70px' },
                  { title: '承运公司', field: 'CarrierShortName', width: '100px' },
                  { title: '联系人', field: 'Boss', width: '100px' },
                  { title: '联系电话', field: 'Telephone', width: '100px' },
                  { title: '发车时间', field: 'StartTime', width: '130px', formatter: DateTimeFormatter },
                  { title: '件数', field: 'Piece', width: '60px', align: 'right' },
                  { title: '件数单价', field: 'PiecePrice', width: '60px', align: 'right' },
                  { title: '重量', field: 'Weight', width: '60px', align: 'right' },
                  { title: '重量单价', field: 'WeightPrice', width: '60px', align: 'right' },
                  { title: '体积', field: 'Volume', width: '60px', align: 'right' },
                  { title: '体积单价', field: 'VolumePrice', width: '60px', align: 'right' },
                  { title: '运输费用', field: 'TransportFee', width: '60px', align: 'right' },
                  { title: '其它费用', field: 'OtherFee', width: '60px', align: 'right' },
                  { title: '提货费', field: 'DeliveryFee', width: '60px', align: 'right' },
                  { title: '送货费', field: 'SendFee', width: '60px', align: 'right' },
                  { title: '代收款', field: 'CollectMoney', width: '60px', align: 'right' },
                  { title: '装卸费', field: 'HandFee', width: '60px', align: 'right' },
                  { title: '合计', field: 'TotalFee', width: '60px', align: 'right', formatter: onRange },
                  {
                      title: '审核状态', field: 'FinanceFirstCheck', width: '60px',
                      formatter: function (val, row, index) { if (val == "0") { return "待审"; } else if (val == "1") { return "已审"; } else if (val == "2") { return "拒审"; } else { return ""; } }
                  }
                ]],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#Dest').combobox('setValues', '<%=UserInfor.DepCity%>');
            $('#Dest').combobox('disable');
        });
        //格式化
        function onRange(val, row, index) {
            var rg = row.TransportFee + row.OtherFee + row.DeliveryFee + row.SendFee + row.HandFee;
            return rg;
        }
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../Arrive/ArriveApi.aspx?method=QueryTransitOrder';
            $('#dg').datagrid('load', {
                CarrierShortName: $('#CarrierShortName').textbox('getValue'),
                AssistNum: $('#AssistNum').textbox('getValue'),
                AwbNo: $('#AwbNo').textbox('getValue'),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                Dest: $('#Dest').combobox('getValue'),
                FinanceFirstCheck: $('#FinanceFirstCheck').combobox('getValue')
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
                <td style="text-align: right;">承运公司:
                </td>
                <td>
                    <input id="CarrierShortName" class="easyui-textbox" data-options="prompt:'请输入承运公司'"
                        style="width: 100px">
                </td>
                <td style="text-align: right;">运单号:
                </td>
                <td>
                    <input id="AwbNo" class="easyui-textbox" data-options="prompt:'请输入运单号'" style="width: 100px">
                </td>
                <td style="text-align: right;">外协单号:
                </td>
                <td>
                    <input id="AssistNum" class="easyui-textbox" data-options="prompt:'请输入外协单号'" style="width: 100px">
                </td>
                <td style="text-align: right;">时间范围:
                </td>
                <td colspan="3">
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px">
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="edit()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-exclamation" plain="false" onclick="cut()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="AwbExport()">&nbsp;导&nbsp;出</a> &nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-lock_add" plain="false" onclick="plcheck()">&nbsp;批量审核&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-lock_open" plain="false" onclick="plDeny()">&nbsp;批量未审&nbsp;</a>
                    <form runat="server" id="fm1">
                        <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
                    </form>
                </td>
                <td style="text-align: right;">所在站点:
                </td>
                <td>
                    <input id="Dest" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../Arrive/ArriveApi.aspx?method=QueryOnlyCity',multiple:true"
                        panelheight="auto" />
                </td>
                <td style="text-align: right;">审核:
                </td>
                <td>
                    <select class="easyui-combobox" id="FinanceFirstCheck" style="width: 70px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">未审</option>
                        <option value="1">已审</option>
                    </select>
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="dlg" class="easyui-dialog" style="width: 830px; height: 540px;" closed="true"
        closable="false" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" name="Unitphone" id="Unitphone" />
            <input type="hidden" name="UnitBoss" id="UnitBoss" />
            <input type="hidden" name="UnitAddress" id="UnitAddress" />
            <input type="hidden" name="TransitID" id="TransitID" />
            <input type="hidden" id="DDest" />
            <table>
                <tr>
                    <td style="text-align: right;">承运公司:
                    </td>
                    <td>
                        <input name="CarrierID" id="ACarrierShortName" class="easyui-combobox" data-options="required:true,url:'../Arrive/ArriveApi.aspx?method=QueryCarrier',method:'get',valueField:'CarrierID',textField:'CarrierShortName',onSelect:carrierChange"
                            style="width: 100px;">
                    </td>
                    <td style="text-align: right;">联系人:
                    </td>
                    <td>
                        <input name="Boss" id="Boss" class="easyui-textbox" style="width: 100px;">
                    </td>
                    <td style="text-align: right;">联系电话:
                    </td>
                    <td>
                        <input name="Telephone" id="Telephone" class="easyui-textbox" style="width: 100px;">
                    </td>
                    <td style="text-align: right;">公司地址:
                    </td>
                    <td>
                        <input name="Address" id="Address" class="easyui-textbox" style="width: 230px;">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">外协单号:
                    </td>
                    <td>
                        <input name="AssistNum" id="AAssistNum" class="easyui-textbox" style="width: 100px;">
                    </td>
                    <td style="text-align: right;">付款方式:
                    </td>
                    <td>
                        <input class="easyui-combobox" name="CheckOutType" id="CheckOutType" data-options="url:'../Data/check.json',method:'get',valueField:'id',textField:'text',panelHeight:'auto'"
                            style="width: 100px;">
                    </td>
                    <td style="text-align: right;">操作人:
                    </td>
                    <td>
                        <input name="UserName" id="UserName" readonly="true" class="easyui-textbox" style="width: 100px;">
                    </td>
                    <td style="text-align: right;">发车时间:
                    </td>
                    <td>
                        <input name="StartTime" id="StartTime" class="easyui-datetimebox" data-options="required:true"
                            style="width: 150px;">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">运输费用:
                    </td>
                    <td>
                        <input name="TransportFee" id="TransportFee" data-options="min:0,precision:2" class="easyui-numberbox"
                            style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">预付:
                    </td>
                    <td>
                        <input name="PrepayFee" id="PrepayFee" data-options="min:0,precision:2" class="easyui-numberbox"
                            style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">到付:
                    </td>
                    <td>
                        <input name="ArriveFee" id="ArriveFee" data-options="min:0,precision:2" class="easyui-numberbox"
                            style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">其它费用:
                    </td>
                    <td>
                        <input name="OtherFee" id="OtherFee" data-options="min:0,precision:2" class="easyui-numberbox"
                            style="width: 150px;" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">提货费用:
                    </td>
                    <td>
                        <input name="DeliveryFee" id="DeliveryFee" data-options="min:0,precision:2" class="easyui-numberbox"
                            style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">送货费:
                    </td>
                    <td>
                        <input name="SendFee" id="SendFee" data-options="min:0,precision:2" class="easyui-numberbox"
                            style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">装卸费用:
                    </td>
                    <td>
                        <input name="HandFee" id="HandFee" data-options="min:0,precision:2" class="easyui-numberbox"
                            style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">代收款:
                    </td>
                    <td>
                        <input name="CollectMoney" id="CollectMoney" data-options="min:0,precision:2" class="easyui-numberbox"
                            style="width: 150px;" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">中转件数:
                    </td>
                    <td>
                        <input name="Piece" id="Piece" data-options="min:0,precision:0" class="easyui-numberbox"
                            style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">中转重量:
                    </td>
                    <td>
                        <input name="Weight" id="Weight" data-options="min:0,precision:2" class="easyui-numberbox"
                            style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">中转体积:
                    </td>
                    <td>
                        <input name="Volume" id="Volume" data-options="min:0,precision:2" class="easyui-numberbox"
                            style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">备注:
                    </td>
                    <td rowspan="2">
                        <textarea name="Remark" id="Remark" cols="35" style="height: 40px;" class="mini-textarea"></textarea>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">件数单价:
                    </td>
                    <td>
                        <input name="PiecePrice" id="PiecePrice" data-options="min:0,precision:2" class="easyui-numberbox"
                            style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">重量单价:
                    </td>
                    <td>
                        <input name="WeightPrice" id="WeightPrice" data-options="min:0,precision:3" class="easyui-numberbox"
                            style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">体积单价:
                    </td>
                    <td>
                        <input name="VolumePrice" id="VolumePrice" data-options="min:0,precision:3" class="easyui-numberbox"
                            style="width: 100px;" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <table id="dgDriver" class="easyui-datagrid">
                        </table>
                    </td>
                    <td>
                        <table id="dgManifest" class="easyui-datagrid">
                        </table>
                    </td>
                </tr>
            </table>
            <div id="toolbar">
                运单号:
            <input id="QAwbNo" class="easyui-textbox" data-options="prompt:'请输入运单号'" style="width: 90px">
                <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="awbQuery()">查询</a>
            </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-basket_remove" plain="false"
            onclick="pldown()" id="down">&nbsp;拉下运单&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
                iconcls="icon-basket_put" plain="false" onclick="plup()" id="up">&nbsp;拉上运单&nbsp;</a>&nbsp;&nbsp;<a
                    href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="SaveManifest()"
                    id="save">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
                        iconcls="icon-print" onclick="PrePrint()" id="print">&nbsp;打印预览&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closeDlg()">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <object id="LODOP_OB" title="dd" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA"
        width="0" height="0">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0" height="0"></embed>
    </object>

    <script type="text/javascript">
        function awbQuery() {
            var Dest = "<%=UserInfor.DepCity %>";
            var mOpts = $('#dgManifest').datagrid('options');
            mOpts.url = '../Arrive/ArriveApi.aspx?method=QueryArriveDestAwb';
            $('#dgManifest').datagrid('load', {
                AwbNo: $('#QAwbNo').textbox('getValue'),
                ShipperUnit: '',
                AcceptPeople: '',
                StartDate: '',
                EndDate: '',
                Dep: '',
                Dest: Dest,
                CheckOutType: '',
                Transit: ''
            });
        }
        function closeDlg() {
            $('#dlg').dialog('close');
            $('#dg').datagrid('reload');
        }
        //修改
        function edit() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning'); return; }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改中转合同' + row.TransitID);
                $('#fm').form('clear');
                row.StartTime = AllDateTime(row.StartTime);
                $('#fm').form('load', row);
                $('#DDest').val(row[0].CurCity);
                loadUnit();
                showGrid();
                bindMethod();
                $('#TransportFee').numberbox('setValue', Number(row.TransportFee).toFixed(2));
                var gridOpts = $('#dgDriver').datagrid('options');
                gridOpts.url = '../Arrive/ArriveApi.aspx?method=QueryAwbInfoByTransitID&id=' + row.TransitID;
                if (row.FinanceFirstCheck == "1") {
                    $('#down').linkbutton('disable');
                    $('#up').linkbutton('disable');
                    $('#save').linkbutton('disable');
                    $('#print').linkbutton('disable');
                } else {
                    $('#down').linkbutton('enable');
                    $('#up').linkbutton('enable');
                    $('#save').linkbutton('enable');
                    $('#print').linkbutton('enable');
                }
            }
        }
        //修改
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改中转合同' + row.TransitID);
                $('#fm').form('clear');
                row.StartTime = AllDateTime(row.StartTime);
                $('#fm').form('load', row);
                $('#DDest').val(row.CurCity);
                loadUnit();
                showGrid();
                bindMethod();
                $('#TransportFee').numberbox('setValue', Number(row.TransportFee).toFixed(2));
                var gridOpts = $('#dgDriver').datagrid('options');
                gridOpts.url = '../Arrive/ArriveApi.aspx?method=QueryAwbInfoByTransitID&id=' + row.TransitID;
                if (row.FinanceFirstCheck == "1") {
                    $('#down').linkbutton('disable');
                    $('#up').linkbutton('disable');
                    $('#save').linkbutton('disable');
                    $('#print').linkbutton('disable');
                } else {
                    $('#down').linkbutton('enable');
                    $('#up').linkbutton('enable');
                    $('#save').linkbutton('enable');
                    $('#print').linkbutton('enable');
                }
            }
        }

        //绑定费用框
        function bindMethod() {
            $("#Piece").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
            $("#PiecePrice").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
            $("#Weight").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
            $("#WeightPrice").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
            $("#Volume").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
            $("#VolumePrice").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
        }
        function qh() {
            var pS = Number($('#Piece').numberbox('getValue')) * Number($('#PiecePrice').numberbox('getValue'));
            var wS = Number($('#Weight').numberbox('getValue')) * Number($('#WeightPrice').numberbox('getValue'));
            var vS = Number($('#Volume').numberbox('getValue')) * Number($('#VolumePrice').numberbox('getValue'));
            var t = 0;
            if (pS >= wS) { t = pS; } else { t = wS; }
            if (vS > t) { t = vS; }
            $('#TransportFee').numberbox('setValue', Number(t).toFixed(2));
        }
        //显示列表
        function showGrid() {
            $('#dgDriver').datagrid({
                width: '400px',
                height: '300px',
                title: '中转运单', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ArriveID',
                url: null,
                columns: [[
                  { title: '', field: 'ArriveID', checkbox: true, width: '30px' },
                  { title: '运单号', field: 'AwbNo', width: '70px' },
                  { title: '开单日期', field: 'HandleTime', width: '80px', formatter: DateFormatter },
                  { title: '最迟时效', field: 'LatestTimeLimit', width: '60px' },
                  { title: '发站', field: 'Dep', width: '40px' },
                  { title: '到站', field: 'Dest', width: '40px' },
                  { title: '中转', field: 'Transit', width: '40px' },
                  { title: '客户名称', field: 'ShipperUnit', width: '70px' },
                  { title: '收货客户', field: 'AcceptUnit', width: '70px' },
                  { title: '收货人', field: 'AcceptPeople', width: '70px' },
                  { title: '品名', field: 'Goods', width: '60px' },
                  { title: '总运费', field: 'TotalCharge', width: '60px' },
                  { title: '件数', field: 'Piece', width: '40px' },
                  { title: '分批件数', field: 'AwbPiece', width: '60px' },
                  { title: '分批重量', field: 'AwbWeight', width: '60px' },
                  { title: '分批体积', field: 'AwbVolume', width: '60px' },
                  {
                      title: '运输方式', field: 'TrafficType', width: '60px',
                      formatter: function (val, row, index) { if (val == "0") { return "普汽"; } else if (val == "1") { return "包车"; } else if (val == "2") { return "加急"; } else if (val == "3") { return "铁路"; } else { return ""; } }
                  },
                  { title: '备注', field: 'Remark', width: '80px' }
                ]],
                onLoadSuccess: function (data) {

                },
                onDblClickRow: function (index, row) {
                    down(index, row);
                }
            });
            $('#dgManifest').datagrid({
                width: '400px',
                height: '300px',
                title: '到达运单', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: true, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ArriveID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                  { title: '', field: 'ArriveID', checkbox: true, width: '30px' },
                  { title: '运单号', field: 'AwbNo', width: '70px' },
                  { title: '开单日期', field: 'HandleTime', width: '80px', formatter: DateFormatter },
                  { title: '最迟时效', field: 'LatestTimeLimit', width: '60px' },
                  { title: '发站', field: 'Dep', width: '40px' },
                  { title: '到站', field: 'Dest', width: '40px' },
                  { title: '中转', field: 'Transit', width: '40px' },
                  { title: '客户名称', field: 'ShipperUnit', width: '70px' },
                  { title: '收货客户', field: 'AcceptUnit', width: '70px' },
                  { title: '收货人', field: 'AcceptPeople', width: '70px' },
                  { title: '品名', field: 'Goods', width: '60px' },
                  { title: '总运费', field: 'TotalCharge', width: '60px' },
                  { title: '件数', field: 'Piece', width: '40px' },
                  { title: '分批件数', field: 'AwbPiece', width: '60px' },
                  { title: '分批重量', field: 'AwbWeight', width: '60px' },
                  { title: '分批体积', field: 'AwbVolume', width: '60px' },
                  {
                      title: '运输方式', field: 'TrafficType', width: '60px',
                      formatter: function (val, row, index) { if (val == "0") { return "普汽"; } else if (val == "1") { return "包车"; } else if (val == "2") { return "加急"; } else if (val == "3") { return "铁路"; } else { return ""; } }
                  },
                ]],
                onLoadSuccess: function (data) {

                },
                onDblClickRow: function (index, row) {
                    up(index, row);
                }
            });
            //设置分页控件
            var p = $('#dgManifest').datagrid('getPager');
            $(p).pagination({
                showPageList: false,
                pageSize: 10, //每页显示的记录条数，默认为10 
                pageList: [10], //可以设置每页记录条数的列表 
                beforePageText: '第', //页数文本框前显示的汉字 
                afterPageText: '页    共 {pages} 页'
            });
        }
        //单个配载
        function up(index, rows) {
            $('#dgManifest').datagrid('deleteRow', index);
            var index = $('#dgDriver').datagrid('getRowIndex', rows.ArriveID);
            if (index < 0) { $('#dgDriver').datagrid('appendRow', rows); }
        }
        //移除
        function down(index, rows) {
            $('#dgDriver').datagrid('deleteRow', index);
            var index = $('#dgManifest').datagrid('getRowIndex', rows.ArriveID);
            if (index < 0) { $('#dgManifest').datagrid('appendRow', rows); }
        }
        //批量选择
        function plup() {
            var rows = $('#dgManifest').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择数据！', 'warning'); return; }
            var copyRows = [];
            for (var i = 0, l = rows.length; i < l; i++) {
                var row = rows[i];
                copyRows.push(row);
                var index = $('#dgDriver').datagrid('getRowIndex', copyRows[i].ArriveID);
                if (index < 0) {
                    $('#dgDriver').datagrid('appendRow', row);
                }
            }
            for (var i = 0; i < copyRows.length; i++) {
                var index = $('#dgManifest').datagrid('getRowIndex', copyRows[i]);
                $('#dgManifest').datagrid('deleteRow', index);
            }
        }
        //批量下
        function pldown() {
            var rows = $('#dgDriver').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择数据！', 'warning'); return; }
            var copyRows = [];
            for (var i = 0, l = rows.length; i < l; i++) {
                var row = rows[i];
                copyRows.push(row);
                var index = $('#dgManifest').datagrid('getRowIndex', copyRows[i].ArriveID);
                if (index < 0) {
                    $('#dgManifest').datagrid('appendRow', row);
                }
            }
            for (var i = 0; i < copyRows.length; i++) {
                var index = $('#dgDriver').datagrid('getRowIndex', copyRows[i]);
                $('#dgDriver').datagrid('deleteRow', index);
            }
        }
        //批量审核
        function plcheck() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要审核的数据！', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定审核？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: '../Arrive/ArriveApi.aspx?method=CheckTransit',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '审核成功!', 'info');
                                $('#dg').datagrid('reload');
                            }
                            else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning'); }
                        }
                    });
                }
            });
        }
        //批量未审
        function plDeny() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要未审的数据！', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定修改为未审？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: '../Arrive/ArriveApi.aspx?method=UnLockTransit',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '未审成功!', 'info');
                                $('#dg').datagrid('reload');
                            }
                            else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning'); }
                        }
                    });
                }
            });
        }
        //删除
        function cut() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '审核状态的中转单不能删除，确定删除？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: '../Arrive/ArriveApi.aspx?method=DelTransit',
                        type: 'post',
                        dataType: 'json',
                        data: { data: json },
                        success: function (text) {
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                $('#dg').datagrid('reload');
                            }
                            else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning'); }
                        }
                    });
                }
            });
        }
        //导出数据
        function AwbExport() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            var key = new Array();
            key[0] = $('#CarrierShortName').textbox('getValue'),
            key[1] = $("#AwbNo").textbox('getValue'),
            key[2] = $('#StartDate').datebox('getValue');
            key[3] = $('#EndDate').datebox('getValue');
            key[4] = $('#Dest').combobox('getValue');
            key[5] = $("#AssistNum").textbox('getValue'),
            key[6] = $("#FinanceFirstCheck").combobox('getValue'),
            $.ajax({
                url: "../Arrive/ArriveApi.aspx?method=QueryTransitOrderForExport&key=" + escape(key.toString()),
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click(); }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }
        //保存配载
        function SaveManifest() {
            var row = $('#dgDriver').datagrid('getRows');
            if (row.length <= 0) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '中转列表中没有数据', 'warning'); return; }
            var json = JSON.stringify(row)
            $('#fm').form('submit', {
                url: "../Arrive/ArriveApi.aspx?method=UpdateTransitOrder",
                contentType: "application/json;charset=utf-8", dataType: "json",
                onSubmit: function (param) {
                    param.submitData = json;
                    var trd = $(this).form('enableValidation').form('validate');
                    if (trd) { $('#save').linkbutton('disable'); }
                    return trd;
                },
                success: function (msg) {
                    $('#save').linkbutton('enable');
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改中转单成功', 'info');
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改失败：' + result.Message, 'warning');
                    }
                }
            })
        }
        //承运公司选择后自动赋值
        function carrierChange(item) {
            if (item) {
                $('#Boss').textbox('setValue', item.Boss);
                $('#Telephone').textbox('setValue', item.Telephone);
                $('#Address').textbox('setValue', item.Address);
            }
        }
        function loadUnit() {
            $.ajax({
                url: "../Arrive/ArriveApi.aspx?method=GetUnitByCityCode&id=" + escape($('#DDest').val()),
                cache: false,
                success: function (o) {
                    var result = eval('(' + o + ')');
                    $('#UnitAddress').val(result.Address);
                    $('#UnitBoss').val(result.Boss);
                    $('#Unitphone').val(result.phone);
                }
            });
        }
        var LODOP;
        //打印预览
        function Print() {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            CreateDataBill();
            LODOP.PRINT();
        }
        //打印预览
        function PrePrint() {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            CreateDataBill();
            LODOP.PREVIEW();
        }
        //打印设置
        function CreateDataBill() {
            LODOP.SET_PRINTER_INDEX(-1);
            LODOP.SET_PRINT_PAGESIZE(1, 2400, 2750, "");
            LODOP.SET_PRINT_STYLE("FontName", "宋体");
            LODOP.SET_PRINT_STYLE("FontSize", 9);
            LODOP.SET_PRINT_STYLE("Bold", 0);
            var bl = "<%=UserInfor.NewLandBelongSystem %>";
            var bt = "新路城配货物中转清单";
            if (bl == "DR") { bt = "鼎融物流有限公司货物中转清单"; }
            else if (bl == "FJ") { bt = "福建新陆程物流货物中转清单"; }
            else if (bl == "YQ") { bt = "云起物流货物中转清单"; }
            else if (bl == "ZY") { bt = "众盈物流货物中转清单"; }
            LODOP.ADD_PRINT_TEXT(10, 270, 700, 30, bt);
            LODOP.SET_PRINT_STYLEA(1, "FontName", "宋体");
            LODOP.SET_PRINT_STYLEA(1, "FontSize", 16);
            LODOP.SET_PRINT_STYLEA(1, "Bold", 1);
            var d = new Date();
            LODOP.ADD_PRINT_TEXT(35, 25, 80, 25, "外协单号:");
            LODOP.ADD_PRINT_TEXT(35, 105, 120, 25, $('#AAssistNum').textbox('getValue'));
            LODOP.ADD_PRINT_TEXT(35, 225, 70, 25, "承运商:");
            LODOP.ADD_PRINT_TEXT(35, 285, 100, 25, $('#ACarrierShortName').combobox('getText'));
            LODOP.ADD_PRINT_TEXT(35, 385, 60, 25, "运费:");
            LODOP.ADD_PRINT_TEXT(35, 445, 80, 25, $('#TransportFee').textbox('getValue'));
            LODOP.ADD_PRINT_TEXT(35, 525, 75, 25, "中转日期:");
            LODOP.ADD_PRINT_TEXT(35, 600, 200, 25, AllDateTime(d));

            LODOP.ADD_PRINT_RECT(60, 20, 70, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 22, 70, 20, "发货日期");
            LODOP.ADD_PRINT_RECT(60, 90, 60, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 92, 60, 20, "运单号");
            LODOP.ADD_PRINT_RECT(60, 150, 65, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 152, 65, 20, "收货人");
            LODOP.ADD_PRINT_RECT(60, 215, 230, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 217, 230, 20, "地址");
            LODOP.ADD_PRINT_RECT(60, 445, 60, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 447, 60, 20, "品名");
            LODOP.ADD_PRINT_RECT(60, 505, 40, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 507, 40, 20, "件数");
            LODOP.ADD_PRINT_RECT(60, 545, 40, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 547, 40, 20, "分批");
            LODOP.ADD_PRINT_RECT(60, 585, 45, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 587, 45, 20, "重量");
            LODOP.ADD_PRINT_RECT(60, 630, 45, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 632, 45, 20, "体积");
            LODOP.ADD_PRINT_RECT(60, 675, 50, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 677, 50, 20, "运费");
            LODOP.ADD_PRINT_RECT(60, 725, 50, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 727, 50, 20, "代收款");
            LODOP.ADD_PRINT_RECT(60, 775, 60, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 777, 60, 20, "付款方式");
            LODOP.ADD_PRINT_RECT(60, 835, 40, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 837, 40, 20, "回单");
            var n = 0;
            var pNum = 0;
            var charge = 0;
            var dsk = 0;
            var hd = 0;
            var fp = 0;
            var griddata = $('#dgDriver').datagrid('getRows');
            for (var i = 0; i < griddata.length + 1; i++) {
                if (i == griddata.length) {
                    LODOP.ADD_PRINT_TEXT(90 + i * 20, 30, 100, 20, "备注：");
                    LODOP.ADD_PRINT_TEXT(90 + i * 20, 130, 700, 20, $('#Remark').val());
                    LODOP.ADD_PRINT_TEXT(110 + i * 20, 30, 100, 20, "负责人:");
                    LODOP.ADD_PRINT_TEXT(110 + i * 20, 130, 700, 20, $('#UnitBoss').val());
                    LODOP.ADD_PRINT_TEXT(130 + i * 20, 30, 100, 20, "联系电话:");
                    LODOP.ADD_PRINT_TEXT(130 + i * 20, 130, 700, 20, $('#Unitphone').val());
                    LODOP.ADD_PRINT_TEXT(150 + i * 20, 30, 100, 20, "地址:");
                    LODOP.ADD_PRINT_TEXT(150 + i * 20, 130, 700, 20, $('#UnitAddress').val());

                    LODOP.ADD_PRINT_TEXT(200 + i * 20, 30, 100, 20, "操作员：");
                    LODOP.ADD_PRINT_TEXT(200 + i * 20, 130, 150, 20, $('#UserName').textbox('getValue'));
                    LODOP.ADD_PRINT_TEXT(200 + i * 20, 400, 100, 20, "承运商签字：");
                    break;
                }
                n++;
                LODOP.ADD_PRINT_RECT(80 + i * 20, 20, 70, 20, 0, 1);
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 22, 70, 20, getNowFormatDate(griddata[i].HandleTime));
                LODOP.ADD_PRINT_RECT(80 + i * 20, 90, 60, 20, 0, 1);
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 92, 60, 20, griddata[i].AwbNo);
                LODOP.ADD_PRINT_RECT(80 + i * 20, 150, 65, 20, 0, 1);
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 152, 65, 20, griddata[i].AcceptPeople);
                LODOP.ADD_PRINT_RECT(80 + i * 20, 215, 230, 20, 0, 1);
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 217, 230, 20, griddata[i].AcceptAddress);
                LODOP.ADD_PRINT_RECT(80 + i * 20, 445, 60, 20, 0, 1);
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 447, 60, 20, griddata[i].Goods);
                LODOP.ADD_PRINT_RECT(80 + i * 20, 505, 40, 20, 0, 1);
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 507, 40, 20, griddata[i].Piece);
                LODOP.ADD_PRINT_RECT(80 + i * 20, 545, 40, 20, 0, 1);
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 547, 40, 20, griddata[i].AwbPiece);
                LODOP.ADD_PRINT_RECT(80 + i * 20, 585, 45, 20, 0, 1);
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 587, 45, 20, griddata[i].AwbWeight);
                LODOP.ADD_PRINT_RECT(80 + i * 20, 630, 45, 20, 0, 1);
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 632, 45, 20, griddata[i].AwbVolume);
                LODOP.ADD_PRINT_RECT(80 + i * 20, 675, 50, 20, 0, 1);
                var tp = 0;
                if (griddata[i].CheckOutType == "3") { tp = griddata[i].TransportFee; }
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 677, 50, 20, tp);
                LODOP.ADD_PRINT_RECT(80 + i * 20, 725, 50, 20, 0, 1);
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 727, 50, 20, griddata[i].CollectMoney);
                var fu = onCheckOutType(griddata[i].CheckOutType)
                LODOP.ADD_PRINT_RECT(80 + i * 20, 775, 60, 20, 0, 1);
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 777, 60, 20, fu);
                LODOP.ADD_PRINT_RECT(80 + i * 20, 835, 40, 20, 0, 1);
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 837, 40, 20, griddata[i].ReturnAwb);
            }
        }
        //结款方式
        function onCheckOutType(e) {
            var CheckOutType = [{ id: 0, text: '现付' }, { id: 1, text: '回单' }, { id: 2, text: '月结' }, { id: 3, text: '到付' }, { id: 4, text: '代收款' }];
            for (var i = 0, l = CheckOutType.length; i < l; i++) {
                var g = CheckOutType[i];
                if (g.id == e) return g.text;
            }
            return "";
        }
    </script>
</asp:Content>
