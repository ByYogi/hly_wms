<%@ Page Title="按进货单付款" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="financeByPurchasePay.aspx.cs" Inherits="Cargo.Finance.financeByPurchasePay" %>
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
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - $("div[name='SelectDiv2']").outerHeight(true) - $("div[name='SelectDiv3']").outerHeight(true);
            $('#dg').datagrid({ height: height });
        }
        $(document).ready(function () {
            $('#dg').datagrid({
                width: '100%',
                height: '400px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OrderID',
                url: null,
                columns: [[
                    { title: '', field: '', checkbox: true, width: '30px' },
                    {
                        title: '进货单号', field: 'OrderID', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    },
                    {
                        title: '订单类型', field: 'TrafficType', width: '60px', formatter: function (val, row, index) { if (val == "0") { return "<span title='入仓单'>入仓单</span>"; } else if (val == "1") { return "<span title='外部单'>外部单</span>"; } else if (val == "2") { return "<span title='退货单'>退货单</span>"; } else if (val == "3") { return "<span title='调货单'>调货单</span>"; } else { return val; } }
                    },
                    {
                        title: '金额', field: 'TotalCharge', width: '60px', align: 'right', formatter: function (value, row) {
                            if (value != null && value != "") {
                                if (row.TrafficType == 2) {
                                    return "-" + value;
                                } else {
                                    return value;
                                }
                            }
                        }
                    },
                    {
                        title: '结算状态', field: 'CheckStatus', width: '60px', formatter: function (val, row, index) { if (val == "0") { return "<span title='未结算'>未结算</span>"; } else if (val == "1") { return "<span title='已结清'>已结清</span>"; } else if (val == "2") { return "<span title='未结清'>未结清</span>"; } else { return ""; } }
                    },
                    {
                        title: '开单人', field: 'CreateAwb', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    },
                    { title: '来货时间', field: 'CreateDate', width: '80px', formatter: DateFormatter },
                    {
                        title: '客户名称', field: 'AcceptUnit', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    },
                    {
                        title: '结算方式', field: 'CheckOutType', width: '60px',
                        formatter: function (val, row, index) { if (val == "0") { return "<span title='现金'>现金</span>"; } else if (val == "1") { return "<span title='现付'>现付</span>"; } else if (val == "2") { return "<span title='周结'>周结</span>"; } else if (val == "3") { return "<span title='半月结'>半月结</span>"; } else if (val == "4") { return "<span title='月结'>月结</span>"; } else { return ""; } }
                    },
                    {
                        title: '来货单号', field: 'OrderID', width: '60px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    }
                ]],
                onSelect: checkRow,
                onUnselect: checkRow,
                onSelectAll: checkRow,
                onUnselectAll: checkRow,
                onCheck: function test() {
                    if (rowIndex != -1) {
                        $('#dg').datagrid('uncheckRow', rowIndex);
                    }
                }
            });
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            $('#CheckStatus').combobox('textbox').bind('focus', function () { $('#CheckStatus').combobox('showPanel'); });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#Cash').numberbox('setValue', '0');
            $('#ReceiveMoney').numberbox('setValue', '0');
            $('#WxMoney').numberbox('setValue', '0');
            $('#AliMoney').numberbox('setValue', '0');

            $('#DisplayTotalPiece').textbox('setValue', '');
            $('#Total').textbox('setValue', '');
            //客户名称查询
            $('#AcceptUnit').combobox({
                url: '../Client/clientApi.aspx?method=AutoCompleteClient', valueField: 'ClientNum', textField: 'Boss', AddField: 'PinyinName',
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                },
            });
            $('#BasicID').combobox({
                url: '../Finance/financeApi.aspx?method=AutoCompleteCard&tk=1',
                valueField: 'BasicID',
                textField: 'Aliases'
            });
            $('#CashID').combobox({
                url: '../Finance/financeApi.aspx?method=AutoCompleteCard&tk=0',
                valueField: 'BasicID',
                textField: 'Aliases',
                onSelect: tkSelect
            });
            $('#WX').combobox({
                url: '../Finance/financeApi.aspx?method=AutoCompleteCard&tk=2',
                valueField: 'BasicID',
                textField: 'Aliases'
            });
            $('#ALIPAY').combobox({
                url: '../Finance/financeApi.aspx?method=AutoCompleteCard&tk=3',
                valueField: 'BasicID',
                textField: 'Aliases'
            });
            $('#BasicID').combobox('textbox').bind('focus', function () { $('#BasicID').combobox('showPanel'); });
            $('#CashID').combobox('textbox').bind('focus', function () { $('#CashID').combobox('showPanel'); });
            $('#WX').combobox('textbox').bind('focus', function () { $('#WX').combobox('showPanel'); });
            $('#ALIPAY').combobox('textbox').bind('focus', function () { $('#ALIPAY').combobox('showPanel'); });
            var rl = "<%=UserInfor.RoleCName%>";
            if (rl == "粤信广通客服角色" || rl == "广通慧采销售角色") {
                $('#btnbyExpensePay').hide();
            }

        });
        function tkSelect(item) { $('#HouseName').val(item.HouseName); }
        var rowIndex = -1;
        function checkRow() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $('#DisplayTotalPiece').textbox('setValue', '0');
                $('#Total').textbox('setValue', '0');
                $("#ClientNum").val("");
                rowIndex = -1;
                return;
            }
            var num = 0;
            var sum = 0;
            for (var i = 0, l = rows.length; i < l; i++) {
                var row = rows[i];
                num++;
                if (row.ClientNum != $("#ClientNum").val() && $("#ClientNum").val() != "") {
                    alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '一次只能操作一个客户！', 'warning');
                    var index = $('#dg').datagrid('getRowIndex', row);
                    $('#dg').datagrid('uncheckRow', index);
                    rowIndex = index;
                    return;
                }
                $("#ClientNum").val(row.ClientNum);
                if (row.CheckStatus == "1") {
                    continue;
                }
                if (Number(row.TotalCharge - row.ReceivedMoney) > 0) {
                    if (row.TrafficType == 2) {
                        sum -= Number(row.TotalCharge - row.ReceivedMoney);
                    } else {
                        sum += Number(row.TotalCharge - row.ReceivedMoney);
                    }
                }
            }
            $('#DisplayTotalPiece').textbox('setValue', num);
            $('#Total').textbox('setValue', sum.toFixed(2));
        }
        //弹出定时关闭的消息框
        function alert_autoClose(title, msg, icon) {
            var interval;
            var time = 1000;
            var x = 1;  //秒为单位，只接受整数
            $.messager.alert(title, msg, icon, function () { });
            interval = setInterval(fun, time);
            function fun() {
                --x;
                if (x == 0) {
                    clearInterval(interval);
                    $(".messager-body").window('close');
                }
            };
        }
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../Finance/financeApi.aspx?method=QueryPurchaseForCash';
            $('#dg').datagrid('load', {
                OrderID: $('#OrderID').val(),
                CreateAwb: $("#CreateAwb").val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                ClientID: $("#AcceptUnit").combobox('getValue'),
                HouseID: $("#AHouseID").combobox('getValue'),//仓库ID
                CheckStatus: $('#CheckStatus').combobox('getValue'),
                FromTO: "6",
                RType: "1"
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
    <div name="SelectDiv3" style="background: linear-gradient(to bottom, #eff5ff 0px, #e0ecff 100%) repeat-x scroll 0 0; border-color: #95b8e7; border-style: solid; border-width: 1px 1px 0px 1px;">
        <table>
            <tr>
                <td>
                    <a id="btnbyExpensePay" class="easyui-linkbutton" iconcls="icon-coins_delete" plain="false" href="../Finance/financeByExpensePay.aspx" target="_self">&nbsp;按报销单付款&nbsp;</a>
                    <a class="easyui-linkbutton" iconcls="icon-coins_delete" plain="false" href="../Finance/financeByLogisticsPay.aspx" target="_self">&nbsp;按物流单付款&nbsp;</a>
                    <a class="easyui-linkbutton" iconcls="icon-coins_delete" plain="false" style="color: Red;" href="../Finance/financeByPurchasePay.aspx" target="_self">&nbsp;按进货单付款&nbsp;</a>
                    <a class="easyui-linkbutton" iconcls="icon-coins_delete" plain="false" href="../Finance/financeByOtherBusinessPay.aspx" target="_self">&nbsp;其他业务付款&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>    
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width:100%">
        <table>
            <tr>
                <td style="text-align: right;">来货单号:
                </td>
                <td>
                    <input id="OrderID" class="easyui-textbox" data-options="prompt:'请输入报销单号，多个用逗号,隔开'" style="width: 250px">
                </td>

                <td style="text-align: right;">开单人:
                </td>
                <td>
                    <input id="CreateAwb" class="easyui-textbox" data-options="prompt:'开单人'" style="width: 80px;" />
                </td>

                <td style="text-align: right;">客户名称:
                </td>
                <td>
                    <input id="AcceptUnit" class="easyui-combobox" style="width: 80px;" />
                </td>
                <td style="text-align: right;">日期范围:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px">
                </td>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 80px;"
                        panelheight="auto" />
                </td>
                <td style="text-align: right;">结算状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="CheckStatus" style="width: 60px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">未结算</option>
                        <option value="1">已结清</option>
                        <option value="2">未结清</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td colspan="12">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" id="undo"
                        iconcls="icon-clear" onclick="reset()"> &nbsp;重&nbsp;置&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div style="height: 150px;">
        <div id="saPanel" name="SelectDiv2" class="easyui-panel" title="" data-options="iconCls:'icon-coins_add',collapsible:false,fit:true"
            style="height: 120px;">
            <form id="fm" class="easyui-form" method="post">
                <input type="hidden" id="HouseName" name="HouseName" />
                <input type="hidden" id="ClientNum" name="ClientNum" />
                <table>
                    <tr>
                        <td style="text-align: right; width: 100px">收款：
                        </td>
                        <td style="width: 200px">
                            <input id="DisplayTotalPiece" name="DisplayTotalPiece" readonly="readonly" class="easyui-textbox"
                                style="width: 60px">
                            份
                        </td>
                        <td style="text-align: right; width: 70px">应付：
                        </td>
                        <td style="width: 150px">
                            <input id="Total" name="Total" class="easyui-textbox" readonly="readonly" style="width: 100px">
                            元
                        </td>
                        <td>备注：
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">现金：
                        </td>
                        <td>
                            <input id="CashID" name="CashID" class="easyui-combobox" style="width: 200px;" panelheight="auto" />
                        </td>
                        <td style="text-align: right;">金额：
                        </td>
                        <td>
                            <input id="Cash" name="Cash" class="easyui-numberbox" data-options="precision:2"
                                style="width: 100px"/>
                        </td>
                        <td rowspan="3">
                            <textarea name="Memo" id="Memo" cols="80" style="height: 60px;" class="mini-textarea"></textarea>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">微信：
                        </td>
                        <td>
                            <input id="WX" name="WX" class="easyui-combobox" style="width: 200px;"
                                panelheight="auto" />
                        </td>
                        <td style="text-align: right;">金额：
                        </td>
                        <td>
                            <input id="WxMoney" name="WxMoney" class="easyui-numberbox" data-options="precision:2"
                                style="width: 100px"/>
                        </td>


                    </tr>
                    <tr>
                        <td style="text-align: right;">银行卡：
                        </td>
                        <td>
                            <input id="BasicID" name="BasicID" class="easyui-combobox" style="width: 200px;"
                                panelheight="auto" />
                        </td>
                        <td style="text-align: right;">金额：
                        </td>
                        <td>
                            <input id="ReceiveMoney" name="ReceiveMoney" class="easyui-numberbox" data-options="precision:2"
                                style="width: 100px"/>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right;">支付宝：
                        </td>
                        <td>
                            <input id="ALIPAY" name="ALIPAY" class="easyui-combobox" style="width: 200px;"
                                panelheight="auto" />
                        </td>
                        <td style="text-align: right;">金额：
                        </td>
                        <td>
                            <input id="AliMoney" name="AliMoney" class="easyui-numberbox" data-options="precision:2"
                                style="width: 100px"/>
                        </td>
                        <td><a href="#" class="easyui-linkbutton" iconcls="icon-cart_put" plain="false" onclick="save()">&nbsp;付&nbsp;款&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-cart_put"
                            plain="false" onclick="over()">&nbsp;已&nbsp;结&nbsp;清&nbsp;</a></td>
                    </tr>
                </table>
            </form>
        </div>
    </div>

    <script type="text/javascript">
        function reset() {
            $('#fm').form('clear');
            $('#dg').datagrid('loadData', { total: 0, rows: [] });
            //$('#dgLay').datagrid('loadData', { total: 0, rows: [] });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#AOrderNo').textbox('setValue', '');
            $('#CheckStatus').combobox('setValue', '-1');
            $('#Cash').numberbox('setValue', '0');
            $('#ReceiveMoney').numberbox('setValue', '0');
            $('#WxMoney').numberbox('setValue', '0');
            $('#AliMoney').numberbox('setValue', '0');
            $('#DisplayTotalPiece').textbox('setValue', '');
            $('#Total').textbox('setValue', '');
        }
        function succ() {
            $('#fm').form('clear');
            $('#dg').datagrid('reload');
            $('#dg').datagrid('unselectAll');
            $('#Cash').numberbox('setValue', '0');
            $('#ReceiveMoney').numberbox('setValue', '0');
            $('#WxMoney').numberbox('setValue', '0');
            $('#AliMoney').numberbox('setValue', '0');
            $('#DisplayTotalPiece').textbox('setValue', '0');
            $('#Total').textbox('setValue', '0');
        }
        //已结清
        function over() {
            var row = $('#dg').datagrid('getSelections');
            if (row == null || row == "" || row.length <= 0) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要付款的数据！', 'warning'); return; }
            if ((Number($('#ReceiveMoney').numberbox('getValue')) + Number($('#Cash').numberbox('getValue')) + Number($('#WxMoney').numberbox('getValue')) + Number($('#AliMoney').numberbox('getValue'))) <= 0) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请输入付款金额！', 'warning');
                return;
            }
            if (Number($('#DisplayTotalPiece').numberbox('getValue')) > 1) {
                if ((Number($('#ReceiveMoney').numberbox('getValue')) + Number($('#Cash').numberbox('getValue')) + Number($('#WxMoney').numberbox('getValue')) + Number($('#AliMoney').numberbox('getValue'))) != Number($('#Total').numberbox('getValue'))) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '实付与应付金额不一致，不允许多票付款', 'warning');
                    return;
                }
            }
            var mo = $('#Memo').val();
            if (mo == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '备注不能为空！', 'warning'); return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定付款？', function (r) {
                if (r) {
                    var trd = $("#fm").form('enableValidation').form('validate');
                    if (trd == false) { return; }
                    var json = JSON.stringify(row)
                    var formjson = $("#fm").serialize();
                    $.ajax({
                        async: false,
                        url: "../Finance/financeApi.aspx?method=SaveCashPay&" + formjson,
                        type: "post",
                        data: { data: json, ty: 6, cStarus: "1" },
                        success: function (msg) {
                            $('#save').linkbutton('enable');
                            var result = eval('(' + msg + ')');
                            if (result.Result) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '付款成功!', 'info'); succ();
                            } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '付款失败：' + result.Message, 'warning'); }
                        }
                    });
                }
            });
        }
        //付款
        function save() {
            var row = $('#dg').datagrid('getSelections');
            if (row == null || row == "" || row.length <= 0) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要付款的数据！', 'warning'); return; }
            <%--if ((Number($('#ReceiveMoney').numberbox('getValue')) + Number($('#Cash').numberbox('getValue')) + Number($('#WxMoney').numberbox('getValue')) + Number($('#AliMoney').numberbox('getValue'))) <= 0) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请输入付款金额！', 'warning');
                return;
            }--%>
            if (Number($('#DisplayTotalPiece').numberbox('getValue')) > 1) {
                if ((Number($('#ReceiveMoney').numberbox('getValue')) + Number($('#Cash').numberbox('getValue')) + Number($('#WxMoney').numberbox('getValue')) + Number($('#AliMoney').numberbox('getValue'))) != Number($('#Total').numberbox('getValue'))) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '实付与应付金额不一致，不允许多票付款', 'warning');
                    return;
                }
            }
            if ((Number($('#ReceiveMoney').numberbox('getValue')) + Number($('#Cash').numberbox('getValue')) + Number($('#WxMoney').numberbox('getValue')) + Number($('#AliMoney').numberbox('getValue'))) > Number($('#Total').numberbox('getValue'))) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '实付大于应付金额，不允许付款', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定付款？', function (r) {
                if (r) {
                    var trd = $("#fm").form('enableValidation').form('validate');
                    if (trd == false) { return; }
                    var json = JSON.stringify(row)
                    var formjson = $("#fm").serialize();
                    $.ajax({
                        async: false,
                        url: "../Finance/financeApi.aspx?method=SaveCashPay&" + formjson,
                        type: "post",
                        data: { data: json, ty: 6, cStarus: "0" },
                        success: function (msg) {
                            $('#save').linkbutton('enable');
                            var result = eval('(' + msg + ')');
                            if (result.Result) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '付款成功!', 'info'); succ();
                            } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '付款失败：' + result.Message, 'warning'); }
                        }
                    });
                }
            });
        }
    </script>
</asp:Content>
