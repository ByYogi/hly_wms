<%@ Page Title="报销收款监督" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="financeExpenseReceiveMonitor.aspx.cs" Inherits="Cargo.Finance.financeExpenseReceiveMonitor" %>

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
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - $("div[name='SelectDiv2']").outerHeight(true);
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
                idField: 'ExID',
                url: null,
                columns: [[
                  {
                      title: '报销单号', field: 'ExID', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '报销人', field: 'ExName', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  { title: '报销时间', field: 'ExDate', width: '80px', formatter: DateFormatter },
                  {
                      title: '客户名称', field: 'ClientName', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '受款人', field: 'ReceiveName', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '受款账号', field: 'ReceiveNumber', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '付款方式', field: 'ChargeType', width: '60px',
                      formatter: function (val, row, index) { if (val == "0") { return "现金"; } else if (val == "1") { return "银行卡"; } else { return ""; } }
                  },
                  {
                      title: '报销类别', field: 'CostName', width: '150px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '报销金额', field: 'ExCharge', width: '60px', align: 'right', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '审批状态', field: 'Status', width: '80px',
                      formatter: function (val, row, index) { if (val == "0") { return "待审"; } else if (val == "1") { return "主管已审批"; } else if (val == "2") { return "拒审"; } else if (val == "3") { return "结束"; } else if (val == "4") { return "财务已审批"; } else if (val == "5") { return "领导已审批"; } else { return ""; } }
                  },
                  {
                      title: '上一审批人', field: 'UserName', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  { title: '上一审批时间', field: 'CheckTime', width: '120px', formatter: DateTimeFormatter },
                  {
                      title: '应收', field: 'ExCharge', width: '70px', align: 'right', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '已收', field: 'ReceivedMoney', width: '70px', align: 'right', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '未收', field: 'UncollectMoney', width: '70px', align: 'right', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '操作人', field: 'LastUserName', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  { title: '收款时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter },
                  {
                      title: '结算状态', field: 'CheckStatus', width: '60px', formatter: function (val, row, index) { if (val == "0") { return "未结算"; } else if (val == "1") { return "已结清"; } else if (val == "2") { return "未结清"; } else { return ""; } }
                  }
                ]],
                onLoadSuccess: function (data) { }
            });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });
            //客户名称查询
            $('#ClientID').combobox({
                url: '../Client/clientApi.aspx?method=AutoCompleteClient',
                valueField: 'ClientID', textField: 'Boss'
            });
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            $('#CheckStatus').combobox('textbox').bind('focus', function () { $('#CheckStatus').combobox('showPanel'); });
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');

        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../Finance/financeApi.aspx?method=QueryExpenseReciveMonitor';
            $('#dg').datagrid('load', {
                ExID: $('#ExID').val(),
                ExName: $("#ExName").val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                ClientID: $("#ClientID").combobox('getValue'),
                HouseID: $("#AHouseID").combobox('getValue'),//仓库ID
                Status: $("#Status").combobox('getValue'),
                CheckStatus: $('#CheckStatus').combobox('getValue'),
                FromTO: "2",
                RType: "0",
                ExType: "1"
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
                    <a class="easyui-linkbutton" iconcls="icon-monitor_lightning" plain="false"
                        href="../Finance/financeOrderReceiveMonitor.aspx" target="_self">&nbsp;订单收款监督&nbsp;</a>&nbsp;&nbsp; <a class="easyui-linkbutton" iconcls="icon-monitor_lightning" plain="false" style="color: Red;"
                            href="../Finance/financeExpenseReceiveMonitor.aspx" target="_self">&nbsp;报销收款监督&nbsp;</a>&nbsp;&nbsp;<a class="easyui-linkbutton" iconcls="icon-monitor_lightning" plain="false"
                                href="../Finance/financeExpensePayMonitor.aspx" target="_self">&nbsp;报销付款监督&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'">
        <table>
            <tr>
                <td style="text-align: right;">报销单号:
                </td>
                <td>
                    <input id="ExID" class="easyui-textbox" data-options="prompt:'请输入报销单号，多个用逗号,隔开'" style="width: 350px">
                </td>
                <td style="text-align: right;">报销人:
                </td>
                <td>
                    <input id="ExName" class="easyui-textbox" data-options="prompt:'报销人'" style="width: 100px;" />
                </td>

                <td style="text-align: right;">客户名称:
                </td>
                <td>
                    <input id="ClientID" class="easyui-combobox" style="width: 100px;" />
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
                    <select class="easyui-combobox" id="CheckStatus" style="width: 100px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">未结算</option>
                        <option value="1">已结清</option>
                        <option value="2">未结清</option>
                    </select>
                </td>

            </tr>
            <tr>
                <td colspan="6">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-application_put"
                        plain="false" onclick="AwbExport()">&nbsp;导&nbsp;出</a>
                    <form runat="server" id="fm1">
                        <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
                    </form>
                </td>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;"
                        panelheight="auto" />
                </td>
                <td style="text-align: right;">报销状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="Status" style="width: 100px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">待审</option>
                        <option value="1">审批中</option>
                        <option value="2">拒审</option>
                        <option value="3">结束</option>
                    </select>
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>

    <script type="text/javascript">
        //导出数据
        function AwbExport() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            var key = new Array();
            key[0] = $('#ExID').val();
            key[1] = $("#ExName").val();
            key[2] = $('#StartDate').datebox('getValue');
            key[3] = $('#EndDate').datebox('getValue');
            key[4] = $("#ClientID").combobox('getValue');
            key[5] = $("#AHouseID").combobox('getValue');
            key[6] = $('#CheckStatus').combobox('getValue');
            key[7] = $('#Status').combobox('getValue');
            key[8] = "2";
            key[9] = "0";
            key[10] = "1";
            $.ajax({
                url: "../Finance/financeApi.aspx?method=QueryExpenseReciveMonitorForExport&key=" + escape(key.toString()),
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click(); }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }
    </script>

</asp:Content>
