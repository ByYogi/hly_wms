<%@ Page Title="配送运单管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeliveryAwbManager.aspx.cs" Inherits="Cargo.Arrive.DeliveryAwbManager" %>
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
        $(document).ready(function() {
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
                idField: 'DeliveryID',
                url: null,
                columns: [[
                  { title: '', field: 'DeliveryID', checkbox: true, width: '30px' },
                  { title: '配送合同号', field: 'DeliveryNum', width: '90px' },
                  { title: '车牌号', field: 'TruckNum', width: '70px' },
                  { title: '发车时间', field: 'StartTime', width: '130px', formatter: DateTimeFormatter },
                  { title: '车型', field: 'Model', width: '50px',
                      formatter: function(val, row, index) { if (val == "0") { return "厢车"; } else if (val == "1") { return "高栏"; } else if (val == "2") { return "平板"; } else if (val == "3") { return "冷柜"; } else if (val == "4") { return "面包车"; } else if (val == "5") { return "微型车"; } else { return ""; } }
                  },
                  { title: '车长', field: 'Length', width: '50px' },
                  { title: '运输费用', field: 'TransportFee', width: '60px', align: 'right' },
                  { title: '其它费用', field: 'OtherFee', width: '60px', align: 'right' },
                  { title: '合计', field: 'dd', width: '80px', align: 'right', formatter: function(val, row, index) { return row.TransportFee + row.OtherFee; } },
                  { title: '司机', field: 'Driver', width: '60px' },
                  { title: '手机号码', field: 'DriverCellPhone', width: '90px' },
                  { title: '审核状态', field: 'FinanceFirstCheck', width: '60px',
                      formatter: function(val, row, index) { if (val == "0") { return "待审"; } else if (val == "1") { return "已审"; } else if (val == "2") { return "拒审"; } else { return ""; } }
                  },
                   { title: '备注', field: 'Remark', width: '150px' }
                ]],
                onLoadSuccess: function(data) { },
                onDblClickRow: function(index, row) { editItemByID(index); }
            });
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
            gridOpts.url = '../Arrive/ArriveApi.aspx?method=QueryDeliveryOrder';
            $('#dg').datagrid('load', {
                TruckNum: $('#TruckNum').val(),
                Driver: $('#Driver').val(),
                AwbNo: $('#AwbNo').val(),
                DeliveryNum: $('#DeliveryNum').val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                FinanceFirstCheck: $('#FinanceFirstCheck').combobox('getValue'),
                Dest: $('#Dest').combobox('getValue'),
                OPID: $('#OPID').combobox('getValue')
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
                <td style="text-align: right;">
                    车牌号码:
                </td>
                <td>
                    <input id="TruckNum" class="easyui-textbox" data-options="prompt:'请输入车牌号码'" style="width: 100px">
                </td>
                <td style="text-align: right;">
                    运单号:
                </td>
                <td>
                    <input id="AwbNo" class="easyui-textbox" data-options="prompt:'请输入运单号'" style="width: 100px">
                </td>
                <td style="text-align: right;">
                    配送合同号:
                </td>
                <td>
                    <input id="DeliveryNum" class="easyui-textbox" data-options="prompt:'请输入合同号'" style="width: 100px">
                </td>
                <td style="text-align: right;">
                    司机姓名:
                </td>
                <td>
                    <input id="Driver" class="easyui-textbox" data-options="prompt:'请输入司机姓名'" style="width: 100px">
                </td>
                <td style="text-align: right;">
                    所在站:
                </td>
                <td>
                    <input id="Dest" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../Arrive/ArriveApi.aspx?method=QueryOnlyCity',multiple:true"
                        panelheight="auto" />
                </td>
                <td style="text-align: right;">
                    审核:
                </td>
                <td>
                    <select class="easyui-combobox" id="FinanceFirstCheck" style="width: 100px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">未审</option>
                        <option value="1">已审</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="edit()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-exclamation" plain="false" onclick="cut()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="AwbExport()">&nbsp;导&nbsp;出</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-lock_add" plain="false" onclick="plcheck()">&nbsp;批量审核&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-lock_open" plain="false" onclick="plDeny()">&nbsp;批量未审&nbsp;</a>
                    <form runat="server" id="fm1">
                        <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
                    </form>
                </td>
                <td style="text-align: right;">
                    时间范围:
                </td>
                <td colspan="3">
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px">
                </td>
                <td style="text-align: right;">
                    操作员:
                </td>
                <td>
                    <input id="OPID" class="easyui-combobox" style="width: 100px;" data-options="url: '../Arrive/ArriveApi.aspx?method=QueryALLUser',valueField: 'LoginName',textField: 'UserName'" />
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="dlg" class="easyui-dialog" style="width: 880px; height: 540px;" closed="true"
        closable="false" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
        <input type="hidden" name="DeliveryID" />
        <input type="hidden" name="DeliveryNum" id="ADeliveryNum" />
        <table>
            <tr>
                <td style="text-align: right;">
                    车牌号码:
                </td>
                <td>
                    <input name="TruckNum" id="ATruckNum" class="easyui-combobox" style="width: 100px;"
                        data-options="valueField:'TruckNum',textField:'TruckNum',url:'../Arrive/ArriveApi.aspx?method=QueryTruck',onSelect:truckChange,required:true" />
                </td>
                <td style="text-align: right;">
                    司机姓名:
                </td>
                <td>
                    <input id="ADriver" name="Driver" class="easyui-textbox" style="width: 90px">
                </td>
                <td style="text-align: right;">
                    手机号码:
                </td>
                <td>
                    <input id="DriverCellPhone" name="DriverCellPhone" class="easyui-textbox" style="width: 100px">
                </td>
                <td style="text-align: right;">
                    车长:
                </td>
                <td>
                    <input name="Length" id="Length" class="easyui-textbox" style="width: 80px;">
                </td>
                <td style="text-align: right;">
                    车型:
                </td>
                <td>
                    <input name="Model" id="Model" class="easyui-textbox" style="width: 80px;">
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    发车时间:
                </td>
                <td colspan="3">
                    <input id="StartTime" name="StartTime" class="easyui-datetimebox" data-options="required:true"
                        style="width: 170px">
                </td>
                <td style="text-align: right;">
                    运输费用:
                </td>
                <td>
                    <input id="TransportFee" name="TransportFee" class="easyui-numberbox" data-options="min:0,precision:2"
                        style="width: 70px">
                </td>
                <td style="text-align: right;">
                    其它费用:
                </td>
                <td>
                    <input id="OtherFee" name="OtherFee" class="easyui-numberbox" data-options="min:0,precision:2"
                        style="width: 100px">
                </td>
                <td style="text-align: right;">
                    操作员:
                </td>
                <td>
                    <input id="UserName" readonly="true" name="UserName" class="easyui-textbox" style="width: 80px">
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    备注:
                </td>
                <td colspan="9">
                    <textarea name="Remark" id="Remark" style="height: 30px; width: 500px" class="mini-textarea"></textarea>
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
            <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="awbQuery()">
                查询</a>
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
                $('#dlg').dialog('open').dialog('setTitle', '修改配送合同' + row.DeliveryNum);
                $('#fm').form('clear');
                row.StartTime = AllDateTime(row.StartTime);
                $('#fm').form('load', row);
                var ml = "";
                if (row.Model == "0") { ml = "厢车"; } else if (row.Model == "1") { ml = "高栏"; } else if (row.Model == "2") { ml = "平板"; } else if (row.Model == "3") { ml = "冷柜"; } else if (row.Model == "4") { ml = "面包车"; } else if (row.Model == "5") { ml = "微型车"; }
                $('#Model').textbox('setValue', ml);
                showGrid();
                var gridOpts = $('#dgDriver').datagrid('options');
                gridOpts.url = '../Arrive/ArriveApi.aspx?method=QueryAwbInfoByDeliveryID&id=' + row.DeliveryID + '&mode=0';
                                <%--var Dest = "<%=CityCode %>";--%>
                //                var mOpts = $('#dgManifest').datagrid('options');
                //                mOpts.url = '../Arrive/ArriveApi.aspx?method=QueryArriveDestAwb&AwbNo=&StartDate=&EndDate=&ShipperUnit=&AcceptPeople=&Dest=' + Dest + '&Dep=&CheckOutType=&Transit=';
                if (row.FinanceFirstCheck == "1") {
                    $('#down').linkbutton('disable');
                    $('#up').linkbutton('disable');
                    $('#save').linkbutton('disable');
                } else {
                    $('#down').linkbutton('enable');
                    $('#up').linkbutton('enable');
                    $('#save').linkbutton('enable');
                }
            }
        }
        //修改
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改配送合同' + row.DeliveryNum);
                $('#fm').form('clear');
                row.StartTime = AllDateTime(row.StartTime);
                $('#fm').form('load', row);
                var ml = "";
                if (row.Model == "0") { ml = "厢车"; } else if (row.Model == "1") { ml = "高栏"; } else if (row.Model == "2") { ml = "平板"; } else if (row.Model == "3") { ml = "冷柜"; } else if (row.Model == "4") { ml = "面包车"; } else if (row.Model == "5") { ml = "微型车"; }
                $('#Model').textbox('setValue', ml);
                showGrid();
                var gridOpts = $('#dgDriver').datagrid('options');
                gridOpts.url = '../Arrive/ArriveApi.aspx?method=QueryAwbInfoByDeliveryID&id=' + row.DeliveryID + '&mode=0';
                       <%--         var Dest = "<%=CityCode %>";--%>
                //                var mOpts = $('#dgManifest').datagrid('options');
                //                mOpts.url = '../Arrive/ArriveApi.aspx?method=QueryArriveDestAwb&AwbNo=&StartDate=&EndDate=&ShipperUnit=&AcceptPeople=&Dest=' + Dest + '&Dep=&CheckOutType=&Transit=';
                if (row.FinanceFirstCheck == "1") {
                    $('#down').linkbutton('disable');
                    $('#up').linkbutton('disable');
                    $('#save').linkbutton('disable');
                } else {
                    $('#down').linkbutton('enable');
                    $('#up').linkbutton('enable');
                    $('#save').linkbutton('enable');
                }
            }
        }
        //显示列表
        function showGrid() {
            $('#dgDriver').datagrid({
                width: '410px',
                height: '365px',
                title: '配送运单', //标题内容
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
                  { title: '体积', field: 'Volume', width: '60px' },
                  { title: '分批体积', field: 'AwbVolume', width: '60px' },
                  { title: '运输方式', field: 'TrafficType', width: '60px',
                      formatter: function(val, row, index) { if (val == "0") { return "普汽"; } else if (val == "1") { return "包车"; } else if (val == "2") { return "加急"; } else if (val == "3") { return "铁路"; } else { return ""; } }
                  },
                  { title: '备注', field: 'Remark', width: '80px' }
                ]],
                onLoadSuccess: function(data) {

                },
                onDblClickRow: function(index, row) {
                    down(index, row);
                }
            });
            $('#dgManifest').datagrid({
                width: '440px',
                height: '365px',
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
                  { title: '体积', field: 'Volume', width: '60px' },
                  { title: '分批体积', field: 'AwbVolume', width: '60px' },
                  { title: '运输方式', field: 'TrafficType', width: '60px',
                      formatter: function(val, row, index) { if (val == "0") { return "普汽"; } else if (val == "1") { return "包车"; } else if (val == "2") { return "加急"; } else if (val == "3") { return "铁路"; } else { return ""; } }
                  },
                ]],
                onLoadSuccess: function(data) {

                },
                onDblClickRow: function(index, row) {
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
        //批量审核
        function plcheck() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要审核的数据！', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定审核？', function(r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: '../Arrive/ArriveApi.aspx?method=CheckDelivery',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function(text) {
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
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定修改为未审？', function(r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: '../Arrive/ArriveApi.aspx?method=UnLockDelivery',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function(text) {
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
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '审核状态的配送单不能删除，确定删除？', function(r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: '../Arrive/ArriveApi.aspx?method=DelDelivery',
                        type: 'post',
                        dataType: 'json',
                        data: { data: json },
                        success: function(text) {
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
            key[0] = $('#TruckNum').val();
            key[1] = $("#AwbNo").val();
            key[2] = $("#Driver").val();
            key[3] = $('#StartDate').datebox('getValue');
            key[4] = $('#EndDate').datebox('getValue');
            key[5] = $('#Dest').combobox('getValue');
            key[6] = $("#DeliveryNum").val();

            $.ajax({
                url: "../Arrive/ArriveApi.aspx?method=QueryDeliveryOrderForExport&key=" + escape(key.toString()),
                success: function(text) {
                    if (text == "OK") { var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click(); }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }
        //保存配载
        function SaveManifest() {
            var row = $('#dgDriver').datagrid('getRows');
            if (row.length <= 0) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '配送列表中没有数据', 'warning'); return; }
            var json = JSON.stringify(row)
            $('#fm').form('submit', {
                url: "../Arrive/ArriveApi.aspx?method=UpdateDeliveryOrder",
                contentType: "application/json;charset=utf-8", dataType: "json",
                onSubmit: function(param) {
                    param.PTruckNum = $('#ATruckNum').combobox('getText');
                    param.submitData = json;
                    var trd = $(this).form('enableValidation').form('validate');
                    if (trd) { $('#save').linkbutton('disable'); }
                    return trd;
                },
                success: function(msg) {
                    $('#save').linkbutton('enable');
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改配送单成功', 'warning');
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改失败：' + result.Message, 'warning');
                    }
                }
            })
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
        //车辆选择方法
        function truckChange(item) {
            if (item) {
                $('#ADriver').textbox('setValue', item.Driver);
                $('#DriverCellPhone').textbox('setValue', item.DriverCellPhone);
                $('#DriverIDNum').val(item.DriverIDNum);
                $('#DriverIDAddress').val(item.DriverIDAddress);
                $('#Length').textbox('setValue', item.Length);
                var ml = "";
                if (item.Model == "0") { ml = "厢车"; } else if (item.Model == "1") { ml = "高栏"; } else if (item.Model == "2") { ml = "平板"; } else if (item.Model == "3") { ml = "冷柜"; } else if (item.Model == "4") { ml = "面包车"; } else if (item.Model == "5") { ml = "微型车"; }
                $('#Model').textbox('setValue', ml);
            }
        }
        var LODOP;
        //打印预览
        function Print() {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            CreateDataBill();
            //LODOP.PREVIEW();
            LODOP.PRINT();
            //LODOP.PRINT_SETUP();
            closeDlg();
        }
        //打印预览
        function PrePrint() {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            CreateDataBill();
            LODOP.PREVIEW();
        }
        function strlen(str) {
            var len = 0;
            for (var i = 0; i < str.length; i++) {
                var c = str.charCodeAt(i);
                //单字节加1   
                if ((c >= 0x0001 && c <= 0x007e) || (0xff60 <= c && c <= 0xff9f)) {
                    len++;
                }
                else {
                    len += 2;
                }
            }
            return len;
        }
        //打印设置
        function CreateDataBill() {
            LODOP.SET_PRINTER_INDEX(-1);
            LODOP.SET_PRINT_PAGESIZE(1, 2400, 2750, "");
            LODOP.SET_PRINT_STYLE("FontName", "宋体");
            LODOP.SET_PRINT_STYLE("FontSize", 10);
            LODOP.SET_PRINT_STYLE("Bold", 0);
            var bl = "<%=UserInfor.NewLandBelongSystem %>";
            var bt = "新路城配货物配送单";
            if (bl == "DR") { bt = "鼎融物流有限公司货物配送单"; }
            else if (bl == "FJ") { bt = "福建新陆程物流货物配送单"; }
            else if (bl == "YQ") { bt = "云起物流货物配送单"; }
            else if (bl == "ZY") { bt = "众盈物流货物配送单"; }
            LODOP.ADD_PRINT_TEXT(10, 230, 700, 30, bt);
            LODOP.SET_PRINT_STYLEA(1, "FontName", "宋体");
            LODOP.SET_PRINT_STYLEA(1, "FontSize", 16);
            LODOP.SET_PRINT_STYLEA(1, "Bold", 1);
            var d = new Date();
            var currentDate = d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + d.getDate();
            LODOP.ADD_PRINT_TEXT(35, 21, 59, 25, "合同号:");
            LODOP.ADD_PRINT_TEXT(35, 70, 90, 25, $('#ADeliveryNum').val()); //"GG140526001"
            LODOP.ADD_PRINT_TEXT(35, 150, 44, 25, "车牌:");
            LODOP.ADD_PRINT_TEXT(35, 188, 80, 25, $('#ATruckNum').combobox('getText'));
            LODOP.ADD_PRINT_TEXT(35, 250, 74, 25, "司机:");
            LODOP.ADD_PRINT_TEXT(35, 280, 62, 25, $('#ADriver').textbox('getValue'));
            LODOP.ADD_PRINT_TEXT(35, 341, 64, 25, "手机:");
            LODOP.ADD_PRINT_TEXT(35, 380, 107, 25, $('#DriverCellPhone').textbox('getValue'));
            LODOP.ADD_PRINT_TEXT(35, 486, 43, 25, "运费:");
            LODOP.ADD_PRINT_TEXT(35, 520, 80, 25, $('#TransportFee').textbox('getValue'));
            LODOP.ADD_PRINT_TEXT(35, 570, 80, 25, "其它运费:");
            LODOP.ADD_PRINT_TEXT(35, 630, 100, 25, $('#OtherFee').textbox('getValue'));
            LODOP.ADD_PRINT_TEXT(35, 690, 50, 25, "合计:");
            LODOP.ADD_PRINT_TEXT(35, 730, 100, 25, Number($('#TransportFee').textbox('getValue')) + Number($('#OtherFee').textbox('getValue')));
            LODOP.ADD_PRINT_TEXT(55, 21, 50, 25, "备注:");
            LODOP.ADD_PRINT_TEXT(55, 71, 900, 25, $('#Remark').val());

            LODOP.ADD_PRINT_RECT(80, 20, 80, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(84, 31, 64, 20, "发货日期");
            LODOP.ADD_PRINT_RECT(80, 100, 70, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(84, 107, 54, 20, "运单号");
            LODOP.ADD_PRINT_RECT(80, 170, 90, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(84, 195, 54, 20, "收货人");
            LODOP.ADD_PRINT_RECT(80, 260, 233, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(84, 357, 39, 20, "品名");
            LODOP.ADD_PRINT_RECT(80, 493, 35, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(84, 496, 40, 20, "件数");
            LODOP.ADD_PRINT_RECT(80, 528, 35, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(83, 533, 40, 20, "分批");
            LODOP.ADD_PRINT_RECT(80, 563, 54, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(83, 571, 50, 20, "到付款");
            LODOP.ADD_PRINT_RECT(80, 617, 54, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(83, 623, 50, 20, "代收款");
            LODOP.ADD_PRINT_RECT(80, 671, 60, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(83, 674, 66, 20, "付款方式");
            LODOP.ADD_PRINT_RECT(80, 731, 34, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(83, 735, 35, 20, "回单");
            var n = 0;
            var pNum = 0;
            var charge = 0;
            var dsk = 0;
            var hd = 0;
            var fp = 0;
            var height = 20;
            var test = 100;
            var griddata = $('#dgDriver').datagrid('getRows');
            for (var i = 0; i < griddata.length + 1; i++) {
                if (griddata[i] != undefined) {
                    height = Math.ceil(strlen(griddata[i].Goods) / 25) * 20;
                }
                if (i == griddata.length) {
                    LODOP.ADD_PRINT_RECT(test, 20, 473, 20, 0, 1);
                    LODOP.ADD_PRINT_TEXT(test + 2, 93, 396, 20, "合计：");
                    LODOP.ADD_PRINT_RECT(test, 493, 35, 20, 0, 1);
                    LODOP.ADD_PRINT_TEXT(test + 2, 496, 40, 20, pNum);
                    LODOP.ADD_PRINT_RECT(test, 528, 35, 20, 0, 1);
                    LODOP.ADD_PRINT_TEXT(test + 2, 535, 40, 20, fp);
                    LODOP.ADD_PRINT_RECT(test, 563, 54, 20, 0, 1);
                    LODOP.ADD_PRINT_TEXT(test + 2, 567, 60, 20, charge);
                    LODOP.ADD_PRINT_RECT(test, 617, 54, 20, 0, 1);
                    LODOP.ADD_PRINT_TEXT(test + 2, 620, 60, 20, dsk);
                    LODOP.ADD_PRINT_RECT(test, 671, 60, 20, 0, 1);
                    LODOP.ADD_PRINT_TEXT(test + 2, 674, 66, 20, "");
                    LODOP.ADD_PRINT_RECT(test, 731, 34, 20, 0, 1);
                    LODOP.ADD_PRINT_TEXT(test + 2, 735, 35, 20, hd);

                    LODOP.ADD_PRINT_TEXT(test + 40, 30, 100, 20, "配送日期：");
                    LODOP.ADD_PRINT_TEXT(test + 40, 130, 150, 20, getNowFormatDate(getDate($('#StartTime').datetimebox('getValue'))));
                    LODOP.ADD_PRINT_TEXT(test + 40, 280, 100, 20, "操作员：");
                    LODOP.ADD_PRINT_TEXT(test + 40, 380, 150, 20, $('#UserName').textbox('getValue'));
                    LODOP.ADD_PRINT_TEXT(test + 40, 500, 100, 20, "司机签字：");
                    break;
                }
                n++;
                LODOP.ADD_PRINT_RECT(test, 20, 80, height, 0, 1);
                LODOP.ADD_PRINT_TEXT(test + 2, 26, 79, height, getNowFormatDate(griddata[i].HandleTime));
                LODOP.ADD_PRINT_RECT(test, 100, 70, height, 0, 1);
                LODOP.ADD_PRINT_TEXT(test + 2, 103, 70, height, griddata[i].AwbNo);
                LODOP.ADD_PRINT_RECT(test, 170, 90, height, 0, 1);
                LODOP.ADD_PRINT_TEXT(test + 2, 173, 90, height, griddata[i].AcceptPeople);
                LODOP.ADD_PRINT_RECT(test, 260, 233, height, 0, 1);
                LODOP.ADD_PRINT_TEXT(test + 2, 263, 231, height, griddata[i].Goods);
                LODOP.ADD_PRINT_RECT(test, 493, 35, height, 0, 1);
                LODOP.ADD_PRINT_TEXT(test + 2, 496, 40, height, griddata[i].Piece);
                LODOP.ADD_PRINT_RECT(test, 528, 35, height, 0, 1);
                LODOP.ADD_PRINT_TEXT(test + 2, 535, 40, height, griddata[i].AwbPiece);
                LODOP.ADD_PRINT_RECT(test, 563, 54, height, 0, 1);
                var tp = 0;
                if (griddata[i].CheckOutType == "3") { tp = griddata[i].TransportFee; }
                LODOP.ADD_PRINT_TEXT(test + 2, 567, 60, height, tp);
                LODOP.ADD_PRINT_RECT(test, 617, 54, height, 0, 1);
                LODOP.ADD_PRINT_TEXT(test + 2, 620, 60, height, griddata[i].CollectMoney);
                var fu = onCheckOutType(griddata[i].CheckOutType)
                LODOP.ADD_PRINT_RECT(test, 671, 60, height, 0, 1);
                LODOP.ADD_PRINT_TEXT(test + 2, 674, 66, height, fu);
                LODOP.ADD_PRINT_RECT(test, 731, 34, height, 0, 1);
                LODOP.ADD_PRINT_TEXT(test + 2, 735, 35, height, griddata[i].ReturnAwb);

                pNum += Number(griddata[i].Piece);
                charge += Number(tp);
                dsk += Number(griddata[i].CollectMoney);
                hd += Number(griddata[i].ReturnAwb);
                fp += Number(griddata[i].AwbPiece);
                test = test + height;
            }
            //LODOP.PRINT_DESIGN();
        }
        //结款方式
        function onCheckOutType(e) {
            var CheckOutType = [{ id: 0, text: '现付' }, { id: 1, text: '回单' }, { id: 2, text: '月结' }, { id: 3, text: '到付' }, { id: 4, text: '代收款'}];
            for (var i = 0, l = CheckOutType.length; i < l; i++) {
                var g = CheckOutType[i];
                if (g.id == e) return g.text;
            }
            return "";
        }
    </script>

</asp:Content>
