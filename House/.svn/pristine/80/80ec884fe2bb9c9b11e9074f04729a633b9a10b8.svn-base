<%@ Page Title="按物流单付款" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="financeByLogisticsPay.aspx.cs" Inherits="Cargo.Finance.financeByLogisticsReceive" %>

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
                idField: 'ID',
                url: null,
                columns: [[
                  { title: '', field: 'ID', checkbox: true, width: '2%' },
                  {
                      title: '账单号', field: 'AccountNo', width: '8%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '账单名称', field: 'Title', width: '8%',formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  { title: '账单时间', field: 'CreateDate', width: '8%', formatter: DateFormatter },
                  {
                      title: '客户名称', field: 'Name', width: '8%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '账单金额', field: 'Total', width: '5%', align: 'right', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          } else {
                              return "<span title='0.00'>0.00</span>";
                          }
                      }
                  },
                  {
                      title: '税费', field: 'TaxFee', width: '5%', align: 'right', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          } else {
                              return "<span title='0.00'>0.00</span>";
                          }
                      }
                  },
                  {
                      title: '其它费用', field: 'OtherFee', width: '5%', align: 'right', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          } else {
                              return "<span title='0.00'>0.00</span>";
                          }
                      }
                  },
                  {
                      title: '备注', field: 'Memo', width: '8%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '审核状态', field: 'ApplyStatus', width: '5%',
                      formatter: function (val, row, index) { if (val == "0") { return "<span title='待审'>待审</span>"; } else if (val == "1") { return "<span title='主管已审批'>主管已审批</span>"; } else if (val == "2") { return "<span title='拒审'>拒审</span>"; } else if (val == "3") { return "<span title='结束'>结束</span>"; } else if (val == "4") { return "<span title='财务已审批'>财务已审批</span>"; } else if (val == "5") { return "<span title='领导已审批'>领导已审批</span>"; } else { return ""; } }
                  },
                  {
                      title: '审核人', field: 'CheckName', width: '8%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  { title: '审核时间', field: 'CheckDate', width: '8%', formatter: DateFormatter },
                  {
                      title: '结算状态', field: 'CheckStatus', width: '5%', formatter: function (val, row, index) { if (val == "0") { return "<span title='未结算'>未结算</span>"; } else if (val == "1") { return "<span title='已结清'>已结清</span>"; } else if (val == "2") { return "<span title='未结清'>未结清</span>"; } else { return ""; } }
                  },
                  { title: '已收款', field: 'ReceivedMoney', hidden: true }
                  //{
                  //    title: '账单类型', field: 'CheckStatus', width: '60px', formatter: function (val, row, index) { if (val == "0") { return "<span title='客户账单'>客户账单</span>"; } else if (val == "1") { return "<span title='物流运输账单'>物流运输账单</span>"; } else { return ""; } }
                  //}
                ]],
                onSelect: checkRow,
                onUnselect: checkRow,
                onSelectAll: checkRow,
                onUnselectAll: checkRow,
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { edit(); }
            });
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
            $('#ClientID').combobox({
                url: '../Client/clientApi.aspx?method=AutoCompleteClient',
                valueField: 'ClientID', textField: 'Boss'
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
        function checkRow() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $('#DisplayTotalPiece').textbox('setValue', '0');
                $('#Total').textbox('setValue', '0');
                return;
            }
            var num = 0;
            var sum = 0;
            for (var i = 0, l = rows.length; i < l; i++) {
                var row = rows[i];
                num++;
                if (row.CheckStatus == "1") {
                    continue;
                }
                if (Number(row.Total + row.TaxFee + row.OtherFee - row.ReceivedMoney) > 0) {
                    sum += Number(row.Total + row.TaxFee + row.OtherFee - row.ReceivedMoney);
                }
                
            }
            $('#DisplayTotalPiece').textbox('setValue', num);
            $('#Total').textbox('setValue', sum.toFixed(2));
        }
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'financeApi.aspx?method=QueryAccountList';
            $('#dg').datagrid('load', {
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                AccountNo: $("#AccountNo").val(),
                OrderNo: $("#OrderNo").val(),
                Name: $("#Name").combobox('getValue'),
                CheckStatus: $("#CheckStatus").combobox('getValue')
            });
        }
        //新增
        function add() {
            $('#dgOrder').datagrid('loadData', { total: 0, rows: [] });
            $('#dgSave').datagrid('loadData', { total: 0, rows: [] });
            $('#dlgOrder').form('clear');
            $('#dlgOrder').dialog('open').dialog('setTitle', '新增账单数据');
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            var datenow = new Date();
            var date = new Date(datenow.getFullYear(), datenow.getMonth(), 1);
            $('#AStartDate').datebox('setValue', date.getFullYear() + '-' + ((date.getMonth() + 1) > 10 ? (date.getMonth() + 1) : '0' + (date.getMonth() + 1)) + '-' + (date.getDate() > 10 ? date.getDate() : '0' + date.getDate()));
            $('#AEndDate').datebox('setValue', getNowFormatDate(datenow));
            showGrid();
            $("#ATitle").textbox("setValue",date.getFullYear() + ((date.getMonth() + 1) > 10 ? (date.getMonth() + 1) : '0' + (date.getMonth() + 1)) + (date.getDate() > 10 ? date.getDate() : '0' + date.getDate()) + "物流账单");
        }
        //编辑
        function edit() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (rows.length > 1) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择一条要修改的数据！', 'warning');
                return;
            } if (rows[0]) {
                $('#dgOrder').datagrid('loadData', { total: 0, rows: [] });
                $('#dgSave').datagrid('loadData', { total: 0, rows: [] });
                $('#dlgOrder').form('clear');
                $('#dlgOrder').dialog('open').dialog('setTitle', '编辑账单数据');
                //所在仓库
                $('#AHouseID').combobox({
                    url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                    valueField: 'HouseID', textField: 'Name'
                });
                $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');

                var datenow = new Date();
                var date = new Date(datenow.getFullYear(), datenow.getMonth(), 1);
                $('#AStartDate').datebox('setValue', date.getFullYear() + '-' + ((date.getMonth() + 1) > 10 ? (date.getMonth() + 1) : '0' + (date.getMonth() + 1)) + '-' + (date.getDate() > 10 ? date.getDate() : '0' + date.getDate()));
                $('#AEndDate').datebox('setValue', getNowFormatDate(datenow));
                showGrid();
                
                var gridOpts = $('#dgSave').datagrid('options');
                gridOpts.url = 'financeApi.aspx?method=QueryOrderList&AccountNo=' + rows[0].AccountNo;
                $("#ATitle").textbox("setValue", rows[0].Title);
                $("#AMemo").val(rows[0].Memo);
                $("#ATotal").numberbox("setValue", rows[0].Total);
                $("#AOtherFee").numberbox("setValue", rows[0].OtherFee);
                $("#ATaxFee").numberbox("setValue", rows[0].TaxFee);
                $('#AName').combobox('setValue', rows[0].Name);
                $("#AAccountNo").val(rows[0].AccountNo);
            }
        }
        function showGrid() {
            var width = $("#dlgOrder").innerWidth() - 10;
            var height = $("#dlgOrder").innerHeight() - 120;
            $('#dgOrder').datagrid({
                width: width / 2,
                height: height,
                title: '订单信息', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OrderID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    { title: '', field: 'OrderID', checkbox: true, width: '2%' },
                    {
                        title: '订单编号', field: 'OrderNo', width: '15%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '客户名称', field: 'AcceptUnit', width: '20%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '运单号', field: 'LogisAwbNo', width: '15%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '物流公司', field: 'LogisticName', width: '15%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '总件数', field: 'Piece', width: '7%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '运输费', field: 'DeliveryFee', width: '7%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '始发站', field: 'Dep', width: '7%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '到达站', field: 'Dest', width: '7%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                ]],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dgOrder').datagrid('clearSelections');
                    $('#dgOrder').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { AddOrder(); },
                rowStyler: function (index, row) { }
            });
            $('#dgSave').datagrid({
                width: width / 2,
                height: height,
                title: '账单信息', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OrderID',
                url: null,
                toolbar: '',
                columns: [[
                    { title: '', field: 'OrderID', checkbox: true, width: '2%' },
                    {
                        title: '订单编号', field: 'OrderNo', width: '15%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '客户名称', field: 'AcceptUnit', width: '20%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '运单号', field: 'LogisAwbNo', width: '15%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '物流公司', field: 'LogisticName', width: '15%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '总件数', field: 'Piece', width: '7%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '运输费', field: 'DeliveryFee', width: '7%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '始发站', field: 'Dep', width: '7%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '到达站', field: 'Dest', width: '7%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                ]],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dgSave').datagrid('clearSelections');
                    $('#dgSave').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { DelOrder(); },
                rowStyler: function (index, row) { }
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
                    <a class="easyui-linkbutton" iconcls="icon-coins_delete" plain="false" href="../Finance/financeByExpensePay.aspx" target="_self" id="btnbyExpensePay">&nbsp;按报销单付款&nbsp;</a>
                    <a class="easyui-linkbutton" iconcls="icon-coins_delete" plain="false" style="color: Red;" href="../Finance/financeByLogisticsPay.aspx" target="_self">&nbsp;按物流单付款&nbsp;</a>
                    <a class="easyui-linkbutton" iconcls="icon-coins_delete" plain="false" href="../Finance/financeByPurchasePay.aspx" target="_self">&nbsp;按进货单付款&nbsp;</a>
                    <a class="easyui-linkbutton" iconcls="icon-coins_delete" plain="false" href="../Finance/financeByOtherBusinessPay.aspx" target="_self">&nbsp;其他业务付款&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table>
            <tr>
                <td style="text-align: right;">账单号:
                </td>
                <td>
                    <input id="AccountNo" class="easyui-textbox" data-options="prompt:'请输入账单号，多个用逗号,隔开'" style="width: 250px">
                </td>
                <td style="text-align: right;">订单号:
                </td>
                <td>
                    <input id="OrderNo" class="easyui-textbox" data-options="prompt:'请输入订单号'" style="width: 100px">
                </td>
                <td style="text-align: right;">客户名称:
                </td>
                <td>
                    <select class="easyui-combobox" id="Name" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="-1">全部</option>
                        <option value="好来运速递">好来运速递</option>
                        <option value="新路程">新路程</option>
                    </select>
                </td>
                <td style="text-align: right;">日期范围:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px">
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
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="add()">&nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="edit()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="del()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
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
                <table>
                    <tr>
                        <td style="text-align: right; width: 100px">付款：
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
                                style="width: 100px">
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
                                style="width: 100px">
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
                                style="width: 100px">
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
                                style="width: 100px">
                        </td>
                        <td><a href="#" class="easyui-linkbutton" iconcls="icon-cart_put" plain="false" onclick="save()">&nbsp;付&nbsp;款&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-cart_put"
                            plain="false" onclick="over()">&nbsp;已&nbsp;结&nbsp;清&nbsp;</a></td>
                    </tr>
                </table>
            </form>
        </div>
    </div>



    <div id="dlgOrder" class="easyui-dialog" style="width: 90%; height: 90%;" closed="true" closable="false" buttons="#dlgOrder-buttons">
        <table>
            <tr>
                <td colspan="4">
                    <table id="dgOrder" class="easyui-datagrid">
                    </table>
                </td>
                <td colspan="4">
                    <table id="dgSave" class="easyui-datagrid">
                    </table>
                </td>
            </tr>
        </table>


        <div id="saPanel" name="SelectDiv2" class="easyui-panel" title="" data-options="iconCls:'icon-coins_add',collapsible:false,fit:true"
            style="height: 120px;">
            <form id="fmSub" class="easyui-form" method="post">
            <input type="hidden" name="AccountNo" id="AAccountNo" />
                <table>
                    <tr>
                        <td style="text-align: left; width: 50%">
                            <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="AddOrder()">&nbsp;添加订单&nbsp;</a>
                        </td>
                        <td style="text-align: left; width: 50%">
                            <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="DelOrder()">&nbsp;移除订单&nbsp;</a>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="text-align: right; width: 80px">客户名称:
                        </td>
                        <td>

                            <select class="easyui-combobox" id="AName" name="Name" style="width: 120px;" panelheight="auto" editable="false" data-options="required:true">
                                <option value="好来运速递">好来运速递</option>
                                <option value="新路程">新路程</option>
                            </select>
                        </td>
                        <td style="text-align: right; width: 80px">账单名称:
                        </td>
                        <td style="width: 200px">
                            <input id="ATitle" name="Title" class="easyui-textbox" data-options="prompt:'请输入账单名称'" style="width: 120px">
                        </td>
                        <td style="text-align: right; width: 80px" rowspan="3">备注:
                        </td>
                        <td rowspan="3">
                            <textarea name="Memo" id="AMemo" cols="80" style="height: 60px;" class="mini-textarea"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 80px">账单金额:
                        </td>
                        <td style="width: 150px">
                            <input id="ATotal" name="Total" class="easyui-numberbox" data-options="min:0,precision:2,required:true" style="width: 120px">
                            元
                        </td>
                        <td style="text-align: right; width: 80px">其它费用:
                        </td>
                        <td>
                            <input id="AOtherFee" name="OtherFee" class="easyui-numberbox" data-options="min:0,precision:2,required:false" style="width: 120px">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 80px">税费:
                        </td>
                        <td>
                            <input id="ATaxFee" name="TaxFee" class="easyui-numberbox" data-options="min:0,precision:2,required:false" style="width: 120px">
                        </td>
                        <td style="text-align: right; width: 80px"></td>
                        <td></td>
                    </tr>
                </table>
            </form>
        </div>
        <div id="toolbar">
            所属仓库:<input id="AHouseID" class="easyui-combobox" style="width: 80px;" panelheight="auto" />&nbsp;开单时间:<input id="AStartDate" class="easyui-datebox" style="width: 100px">~<input id="AEndDate" class="easyui-datebox" style="width: 100px">&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="queryOrder()">查询</a>
        </div>
    </div>
    <div id="dlgOrder-buttons">
        <table style="width: 100%">
            <tr>
                <td>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="SaveAccount()" id="save">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgOrder').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>



    <script type="text/javascript">

        //删除数据
        function del() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'financeApi.aspx?method=DeleteAccountData',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                $('#dg').datagrid('clearSelections');
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
        //保存数据
        function SaveAccount() {
            var rows = $("#dgSave").datagrid('getData').rows;
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请先添加订单物流数据！', 'warning');
                return;
            }
            var msg = "确定保存？";
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', msg, function (r) {
                if (r) {

                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    var json = JSON.stringify(rows)

                    $('#fmSub').form('submit', {
                        url: 'financeApi.aspx?method=SaveAccountData',
                        contentType: "application/json;charset=utf-8", dataType: "json",
                        onSubmit: function (param) {
                            param.data = json;
                        },
                        success: function (text) {
                            $.messager.progress("close");
                            var result = eval('(' + text + ')');
                            if (result) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                                $('#dlgOrder').dialog('close');
                                dosearch();
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    })
                }
            });
        }
        function AddOrder() {
            var rows = $('#dgOrder').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要拉上账单的数据！', 'warning');
                return;
            }
            for (var i = 0; i < rows.length;) {
                var row = rows[0];
                $('#dgSave').datagrid('appendRow', row);
                var index = $('#dgOrder').datagrid('getRowIndex', row);
                var Total = Number($("#ATotal").numberbox('getValue')) + row.DeliveryFee;
                $('#dgOrder').datagrid('deleteRow', index);
                $("#ATotal").numberbox('setValue', Number(Total));
            }
        }
        function DelOrder() {
            var rows = $('#dgSave').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要移除账单的数据！', 'warning');
                return;
            }
            for (var i = 0; i < rows.length;) {
                var row = rows[0];
                $('#dgOrder').datagrid('appendRow', row);
                var index = $('#dgSave').datagrid('getRowIndex', row);
                var Total = Number($("#ATotal").numberbox('getValue')) - row.DeliveryFee;
                $('#dgSave').datagrid('deleteRow', index);
                $("#ATotal").numberbox('setValue', Number(Total));
            }
        }
        var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - $("div[name='SelectDiv2']").outerHeight(true) - $("div[name='SelectDiv3']").outerHeight(true);
        $('#dlgOrder').dialog({
            height: height
        }).dialog("open");
        function queryOrder() {
            $('#dgOrder').datagrid('clearSelections');
            var gridOpts = $('#dgOrder').datagrid('options');
            gridOpts.url = '../Finance/financeApi.aspx?method=QueryOrderList';
            $('#dgOrder').datagrid('load', {
                StartDate: $('#AStartDate').datebox('getValue'),
                EndDate: $('#AEndDate').datebox('getValue'),
                HouseID: $("#AHouseID").combobox('getValue')//仓库ID
            });
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
                        data: { data: json, ty: 4, cStarus: "1" },
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
                        data: { data: json, ty: 4, cStarus: "0" },
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
