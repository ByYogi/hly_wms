<%@ Page Title="客户账单管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="clientBillManager.aspx.cs" Inherits="Cargo.Client.clientBillManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/Rotate/jQueryRotate.2.2.js" type="text/javascript"></script>
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
                idField: 'AccountID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                  { title: '', field: '', checkbox: true, width: '5%' },
                    {
                        title: '所属仓库', field: 'HouseName', width: '6%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                  {
                      title: '账单号', field: 'AccountID', width: '8%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '账单名称', field: 'AccountTitle', width: '12%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  { title: '创建日期', field: 'CreateDate', width: '6%', formatter: DateFormatter },
                {
                    title: '客户名称', field: 'ClientName', width: '5%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                },
                  {
                      title: '账单金额(元)', field: 'Total', align: 'right', width: '6%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  //{
                  //    title: '税费', field: 'TaxFee', width: '7%', formatter: function (value) {
                  //        return "<span title='" + value + "'>" + value + "</span>";
                  //    }
                  //},
                  //{
                  //    title: '其它费用', field: 'OtherFee', width: '7%', formatter: function (value) {
                  //        return "<span title='" + value + "'>" + value + "</span>";
                  //    }
                  //},
                    {
                        title: '备注', field: 'Memo', width: '15%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    //{
                    //    title: '下一审核人', field: 'NextCheckName', width: '5%', formatter: function (value) {
                    //        return "<span title='" + value + "'>" + value + "</span>";
                    //    }
                    //},
                   {
                       title: '审核状态', field: 'Status', width: '5%',
                       formatter: function (value) {
                           if (value == "0") { return "<span title='待审'>待审</span>"; }
                           else if (value == "1") { return "<span title='已审'>已审</span>"; }
                           else if (value == "2") { return "<span title='拒审'>拒审</span>"; }
                           else if (value == "3") { return "<span title='结束'>结束</span>"; }
                       }
                   },
                   {
                       title: '签字确认', field: 'ElecSign', width: '5%',
                       formatter: function (val, row, index) {
                           if (val == "0") { return "未签字"; }
                           else if (val == "1") { return "已签字"; }
                           else { return "未签字"; }
                       }
                   },
                   { title: '签字时间', field: 'ElecSignDate', width: '10%', formatter: DateTimeFormatter },
                   {
                       title: '签字照片', field: 'ElecSignImg', width: '6%', formatter: imgFormatter
                   },
                  {
                      title: '结算状态', field: 'CheckStatus', width: '5%',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "未结算"; }
                          else if (val == "1") { return "已结清"; }
                          else if (val == "2") { return "未结清"; }
                          else { return "未结算"; }
                      }
                  },
                  { title: '操作时间', field: 'OPDATE', width: '10%', formatter: DateTimeFormatter }
                ]],
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });

            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#AClientName').combobox('clear');
                    $('#AClientName').combobox({
                        url: '../Client/clientApi.aspx?method=AutoCompleteClient&houseID=' + rec.HouseID, valueField: 'ClientID', textField: 'Boss', AddField: 'PinyinName',
                        filter: function (q, row) {
                            var opts = $(this).combobox('options');
                            return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                        },
                    });

                }
            });
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            $('#AClientName').combobox('textbox').bind('focus', function () { $('#AClientName').combobox('showPanel'); });
            $('#AStatus').combobox('textbox').bind('focus', function () { $('#AStatus').combobox('showPanel'); });
            $('#ACheckStatus').combobox('textbox').bind('focus', function () { $('#ACheckStatus').combobox('showPanel'); });
            var value2 = 0
            $("#simg").rotate({ bind: { click: function () { value2 += 90; $(this).rotate({ animateTo: value2 }) } } });
        });
        //图片添加路径  
        function imgFormatter(value, row, index) {
            if ('' != value && null != value) {
                var rvalue = "";
                rvalue += "<img onclick=downView(\"" + value + "\") style='width:66px; height:60px;margin-left:1px;' src='../Weixin/UploadFile/" + value + "' title='点击查看图片'/>";
                return rvalue;
            }
        }
        function downView(img) {
            var simg = img;
            $('#dgViewImg').dialog('open').dialog('setTitle', '预览');
            $("#simg").attr("src", "../Weixin/UploadFile/" + simg);
        }
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'clientApi.aspx?method=QueryBillManager';
            $('#dg').datagrid('load', {
                AccountID: $('#AAccountID').val(),
                HouseID: '',//仓库ID
                ClientID: $("#AClientName").combobox('getValue'),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                Status: $("#AStatus").combobox('getValue'),
                ElecSign: $("#AElecSign").combobox('getValue'),
                CheckStatus: $("#ACheckStatus").combobox('getValue')
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
                <td style="text-align: right;">账单号:
                </td>
                <td>
                    <input id="AAccountID" class="easyui-textbox" data-options="prompt:'请输入账单号'" style="width: 100px">
                </td>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 80px;" />
                </td>
                <td style="text-align: right;">客户名称:
                </td>
                <td>
                    <input id="AClientName" style="width: 100px;" data-options="valueField: 'ClientID', textField: 'Boss'" class="easyui-combobox" />
                </td>
                <td style="text-align: right;">审核状态:
                </td>
                <td style="width: 10%">
                    <select class="easyui-combobox" id="AStatus" style="width: 60px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">待审</option>
                        <option value="1">已审</option>
                        <option value="2">拒审</option>
                        <option value="3">结束</option>
                    </select>
                </td>
                 <td style="text-align: right;">签字状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="AElecSign" style="width: 70px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">未签字</option>
                        <option value="1">已签字</option>
                    </select>
                </td>
                <td style="text-align: right;">结算状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="ACheckStatus" style="width: 70px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">未结算</option>
                        <option value="1">已结清</option>
                        <option value="2">未结清</option>
                    </select>
                </td>
                <td style="text-align: right;">创建日期:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px">
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">
            &nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
                iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
                    iconcls="icon-cart_put" plain="false" onclick="PayAcceptMoney()">&nbsp;客户应收应付&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="BillExport()" id="btnExport">&nbsp;导&nbsp;出&nbsp;</a>
          <form runat="server" id="fm1">
            <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
        </form>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 1000px; height: 600px; padding: 0px"
        closed="true" buttons="#dlg-buttons">
        <div id="saPanel">
            <table>
                <tr>
                    <td style="text-align: right;">订单号:
                    </td>
                    <td>
                        <input id="AOrderNo" class="easyui-textbox" data-options="prompt:'请输入订单号'" style="width: 120px">
                    </td>
                    <td style="text-align: right;">所属仓库:
                    </td>
                    <td>
                        <input id="BHouseID" class="easyui-combobox" style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">客户名称:
                    </td>
                    <td>
                        <input id="PayClientNum" style="width: 100px;" data-options="valueField: 'ClientNum', textField: 'Boss'" class="easyui-combobox" />
                    </td>
                    <td style="text-align: right;">结算状态:
                    </td>
                    <td>
                        <select class="easyui-combobox" id="BCheckStatus" style="width: 70px;" panelheight="auto">
                            <option value="-1">全部</option>
                            <option value="0">未结算</option>
                            <option value="1">已结清</option>
                            <option value="2">未结清</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td colspan="4"><a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="QueryOrderAccount()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="plAddOrder()">
            &nbsp;批量添加&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" id="undo"
                iconcls="icon-clear" onclick="reset()"> &nbsp;重&nbsp;置&nbsp;</a></td>
                    <td style="text-align: right;">日期范围:
                    </td>
                    <td colspan="3">
                        <input id="BStartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="BEndDate" class="easyui-datebox" style="width: 100px">
                    </td>
                </tr>
            </table>
        </div>
        <table>
            <tr>
                <td>
                    <table id="dgSave" class="easyui-datagrid">
                    </table>
                </td>
                <td>
                    <table id="dgAccount" class="easyui-datagrid">
                    </table>
                </td>
            </tr>
        </table>
        <div id="saPanel">
            <form id="fm" class="easyui-form" method="post">
                <input type="hidden" name="AccountID" id="AccountID" />
                <input type="hidden" name="ClientID" id="AClientID" />
                <input type="hidden" name="ClientNum" id="AClientNum" />
                <input type="hidden" name="ClientName" id="ClientName" />
                <input type="hidden" name="HouseID" id="HouseID" />

                <table>
                    <tr>
                        <td style="text-align: right; width: 70px">账单金额：
                        </td>
                        <td>
                            <input id="Total" name="Total" class="easyui-textbox" readonly="readonly" style="width: 100px">
                            元
                        </td>
                        <td style="text-align: right; width: 70px">账单名称：
                        </td>
                        <td>
                            <input id="AccountTitle" name="AccountTitle" class="easyui-textbox" style="width: 300px"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 70px">账单备注：
                        </td>
                        <td colspan="3">
                            <textarea name="Memo" id="Memo" cols="70" style="height: 50px;" class="mini-textarea"></textarea>
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveOrderAccount()">&nbsp;&nbsp;保&nbsp;存&nbsp;&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;&nbsp;取&nbsp;消&nbsp;&nbsp;</a>
    </div>
    <div id="dgViewImg" class="easyui-dialog" closed="true" style="width: 1000px; height: 600px; overflow: hidden; display: flex; justify-content: center; align-items: center;">
        <img id="simg" style="max-width: 100%; max-height: 170%;" />
    </div>

    <div id="dlgPay" class="easyui-dialog" style="width: 800px; height: 500px; padding: 0px"
        closed="true" buttons="#dlgPay-buttons">
        <table id="dgPay" class="easyui-datagrid">
        </table>
    </div>
    <div id="dlgPay-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgPay').dialog('close')">&nbsp;&nbsp;关&nbsp;闭&nbsp;&nbsp;</a>
    </div>
    <script type="text/javascript">

        //导出数据
        function BillExport() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            var key = new Array();
            key[0] = $('#AAccountID').val();
            key[1] = $("#AHouseID").combobox('getValue');
            key[2] = $("#AClientName").val();
            key[3] = $('#StartDate').datebox('getValue');
            key[4] = $('#EndDate').datebox('getValue');
            key[5] = $("#AStatus").combobox('getValue');
            key[6] = $("#AElecSign").combobox('getValue');
            key[7] = $("#ACheckStatus").combobox('getValue');
            
            $.ajax({
                url: "clientApi.aspx?method=QueryBillManagerForExport&key=" + escape(key.toString()),
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click(); }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }
        function PayAcceptMoney() {
            //$('#dgPay').datagrid('loadData', { total: 0, rows: [] });
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要查询的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlgPay').dialog('open').dialog('setTitle', '查看客户：' + row.ClientName + '应收应付记录');
                showdgPay();
                var gridOpts = $('#dgPay').datagrid('options');
                gridOpts.url = '../Client/clientApi.aspx?method=QueryClientAccountPay&ClientNum=' + row.ClientNum;
            }
        }
        function showdgPay() {
            $('#dgPay').datagrid({
                width: '100%',
                height: '420px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                pageSize: 20, //每页多少条
                pageList: [20, 50],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'AccountID',
                url: null,
                columns: [[

                {
                    title: '账单月', field: 'AccountDate', width: '100px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                },
                  {
                      title: '账单名称', field: 'AccountTitle', width: '200px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '账单金额(元)', field: 'Total', align: 'right', width: '100px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '返利(元)', field: 'RebateMoney', align: 'right', width: '100px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '来款(元)', field: 'IncomeMoney', align: 'right', width: '100px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '剩余欠款(元)', field: 'PreReceiveMoney', align: 'right', width: '100px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '结算状态', field: 'CheckStatus', width: '80px',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "未结算"; }
                          else if (val == "1") { return "已结清"; }
                          else if (val == "2") { return "未结清"; }
                          else { return "未结算"; }
                      }
                  }
                ]]
            });
        }
        //保存账单数据
        function saveOrderAccount() {
            var row = $('#dgAccount').datagrid('getRows');
            if (row.length <= 0) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '账单列表中没有数据', 'warning'); return; }
            var trd = $("#fm").form('enableValidation').form('validate');
            if (trd == false) { return; }
            var json = JSON.stringify(row)
            var formjson = $("#fm").serialize();
            $.ajax({
                async: false,
                url: "clientApi.aspx?method=saveOrderAccount&" + formjson,
                type: "post", data: { submitData: json },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info'); $('#dlg').dialog('close'); dosearch();
                    } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning'); }
                }
            });
        }
        //批量添加
        function plAddOrder() {
            var rows = $('#dgSave').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择数据！', 'warning'); return; }
            var sum = Number($('#Total').textbox('getValue'));
            //var RebateMoney = Number($('#RebateMoney').textbox('getValue'));
            //var ComeMoney = Number($('#ComeMoney').textbox('getValue'));
            //if (isNaN(RebateMoney)) { RebateMoney = 0; }
            if (isNaN(sum)) { sum = 0; }
            //if (isNaN(ComeMoney)) { ComeMoney = 0; }
            var copyRows = [];
            for (var i = 0, l = rows.length; i < l; i++) {
                var row = rows[i];
                //if (row.CheckStatus == "1") { continue; }
                copyRows.push(row);
                var indexd = $('#dgAccount').datagrid('getRowIndex', copyRows[i].AwbID);
                if (indexd < 0) {
                    $('#dgAccount').datagrid('appendRow', row);
                    if (Number(row.TotalCharge) > 0) {
                        if (row.OrderModel == "1") {
                            sum -= Number(row.TotalCharge );
                        } else {
                            sum += Number(row.TotalCharge);
                        }
                    }
                }
            }
            for (var i = 0; i < copyRows.length; i++) {
                var index = $('#dgSave').datagrid('getRowIndex', copyRows[i]);
                $('#dgSave').datagrid('deleteRow', index);
            }
            $('#Total').textbox('setValue', Number(sum).toFixed(2))
            //var AT = 0;
            //AT = sum - RebateMoney - ComeMoney;
            //$('#ATotal').textbox('setValue', Number(AT).toFixed(2))
        }
        //运单新增的重置
        function reset() {
            $("#AOrderNo").textbox('setValue', '');
            $("#BHouseID").combobox('setValue', '');
            $("#PayClientNum").combobox('setValue', '');
            $("#BCheckStatus").combobox('setValue', '-1');
            var datenow = new Date();
            $('#BStartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#BEndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#fm').form('clear');

            $('#dgSave').datagrid('loadData', { total: 0, rows: [] });
            $('#dgAccount').datagrid('loadData', { total: 0, rows: [] });
            $("#Total").textbox('setValue', "0");
            //$("#ATotal").textbox('setValue', "0");
            //$("#RebateMoney").textbox('setValue', "0");
            //$("#ComeMoney").textbox('setValue', "0");
        }
        function QueryOrderAccount() {
            $('#dgSave').datagrid('clearSelections');
            var gridOpts = $('#dgSave').datagrid('options');
            gridOpts.url = '../Finance/financeApi.aspx?method=QueryOrderForCash';
            $('#dgSave').datagrid('load', {
                OrderNo: $('#AOrderNo').val(),
                StartDate: $('#BStartDate').datebox('getValue'),
                EndDate: $('#BEndDate').datebox('getValue'),
                Dest: "",//$("#Dest").combobox('getValue'),
                PayClientNum: $("#PayClientNum").combobox('getValue'),
                //PayClientName: $("#PayClientNum").combobox('getText'),
                HouseID: $("#BHouseID").combobox('getValue'),//仓库ID
                CheckStatus: $('#BCheckStatus').combobox('getValue')
            });
        }
        //新增账单信息
        function addItem() {
            reset();
            $('#dgSave').datagrid('loadData', { total: 0, rows: [] });
            $('#dgAccount').datagrid('loadData', { total: 0, rows: [] });
            $('#dlg').dialog('open').dialog('setTitle', '新增账单信息');
            $('#fm').form('clear');
            var datenow = new Date();
            $('#BStartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#BEndDate').datebox('setValue', getNowFormatDate(datenow));
            showGrid();
            //$('#AccountTitle').textbox('setValue', "");
            //$('#BHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');

            //所在仓库
            $('#BHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $("#HouseID").val(rec.HouseID);
                    $('#PayClientNum').combobox('clear');
                    var url = '../Client/clientApi.aspx?method=AutoCompleteClient&houseID=' + rec.HouseID;
                    $('#PayClientNum').combobox('reload', url);
                    //一级仓库
                    $('#PayClientNum').combobox({
                        onSelect: function (fai) {
                            $("#AClientID").val(fai.ClientID);
                            $("#AClientNum").val(fai.ClientNum);
                            $("#ClientName").val(fai.Boss);

                            //$('#RebateMoney').textbox('setValue', Number(fai.RebateMoney).toFixed(2))
                            $('#AccountTitle').textbox('setValue', fai.Boss + datenow.getFullYear() + "年" + datenow.getMonth() + "月" + "账单");

                        }
                    });
                }
            });
            $("#HouseID").val("<%=UserInfor.HouseID%>");
            $('#PayClientNum').combobox('clear');
            var url = '../Client/clientApi.aspx?method=AutoCompleteClient&houseID=<%=UserInfor.HouseID%>';
            $('#PayClientNum').combobox('reload', url);
        }
        //修改账单信息
        function editItemByID(Did) {
            $('#dgSave').datagrid('loadData', { total: 0, rows: [] });
            $('#dgAccount').datagrid('loadData', { total: 0, rows: [] });
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改账单号：' + row.AccountID);
                $('#fm').form('load', row);
                var datenow = new Date();
                $('#BStartDate').datebox('setValue', getLastDayFormatDate(datenow));
                $('#BEndDate').datebox('setValue', getNowFormatDate(datenow));
                showGrid();
                //所在仓库
                $('#BHouseID').combobox({
                    url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                    valueField: 'HouseID', textField: 'Name',
                    onSelect: function (rec) {
                        $("#HouseID").val(rec.HouseID);
                        $('#PayClientNum').combobox('clear');
                        var url = '../Client/clientApi.aspx?method=AutoCompleteClient&houseID=' + rec.HouseID;
                        $('#PayClientNum').combobox('reload', url);
                        //一级仓库
                        $('#PayClientNum').combobox({
                            onSelect: function (fai) {
                                $("#AClientID").val(fai.ClientID);
                                $("#AClientNum").val(fai.ClientNum);
                                $("#ClientName").val(fai.Boss);

                                //$('#RebateMoney').textbox('setValue', Number(fai.RebateMoney).toFixed(2))
                                $('#AccountTitle').textbox('setValue', fai.Boss + datenow.getFullYear() + "年" + datenow.getMonth() + "月" + "账单");

                            }
                        });
                    }
                });
                var gridOpts = $('#dgAccount').datagrid('options');
                gridOpts.url = 'clientApi.aspx?method=QueryOrderByAccountNo&AccountNo=' + row.AccountID;
            }
        }
        //显示列表
        function showGrid() {
            $('#dgSave').datagrid({
                width: '470px',
                height: '360px',
                title: '订单列表', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OrderID',
                url: null,
                columns: [[
                  { title: '', field: 'OrderID', checkbox: true, width: '30px' },
                  //{
                  //    title: '收入', field: 'TransportFee', width: '60px', align: 'right',
                  //    formatter: function (val, row, index) {
                  //        if (row.OrderModel == "1") {
                  //            return "-" + val;
                  //        } else { return val; }
                  //    }
                  //},
                  //{
                  //    title: '配送费', field: 'TransitFee', width: '60px', align: 'right', formatter: function (value) {
                  //        if (value != null && value != "") {
                  //            return "<span title='" + value + "'>" + value + "</span>";
                  //        }
                  //    }
                  //},
                  {
                      title: '总收入', field: 'TotalCharge', width: '70px', align: 'right',
                      formatter: function (val, row, index) {
                          if (row.OrderModel == "1") {
                              return "-" + val;
                          } else { return val; }
                      }
                  },
                  {
                      title: '结算状态', field: 'CheckStatus', width: '60px', formatter: function (val, row, index) { if (val == "0") { return "<span title='未结算'>未结算</span>"; } else if (val == "1") { return "<span title='已结清'>已结清</span>"; } else if (val == "2") { return "<span title='未结清'>未结清</span>"; } else { return ""; } }
                  },
                  {
                      title: '订单号', field: 'OrderNo', width: '90px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '订单类型', field: 'OrderModel', width: '60px', formatter: function (val, row, index) {
                          if (val == "0") { return "<span title='订货单'>订货单</span>"; } else if (val == "1") { return "<span title='退货单'>退货单</span>"; } else { return ""; }
                      }
                  },
                  {
                      title: '件数', field: 'Piece', width: '50px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  { title: '开单时间', field: 'CreateDate', width: '130px', formatter: DateTimeFormatter },
                  {
                      title: '客户名称', field: 'PayClientName', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '公司名称', field: 'AcceptUnit', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '收货人', field: 'AcceptPeople', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '手机号码', field: 'AcceptCellphone', width: '90px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '收货地址', field: 'AcceptAddress', width: '120px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '到达站', field: 'Dest', width: '50px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '业务员', field: 'SaleManName', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '开单员', field: 'CreateAwb', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '订单状态', field: 'AwbStatus', width: '60px',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "<span title='已下单'>已下单</span>"; }
                          else if (val == "1") { return "<span title='正在备货'>正在备货</span>"; }
                          else if (val == "2") { return "<span title='已出库'>已出库</span>"; }
                          else if (val == "3") { return "<span title='运输在途'>运输在途</span>"; }
                          else if (val == "4") { return "<span title='已到达'>已到达</span>"; }
                          else if (val == "5") { return "<span title='已签收'>已签收</span>"; }
                          else { return ""; }
                      }
                  },
                  {
                      title: '订单类型', field: 'OrderType', width: '60px', formatter: function (val, row, index) {
                          if (val == "0") { return "<span title='电脑单'>电脑单</span>"; } else if (val == "1") { return "<span title='企业微信单'>企业微信单</span>"; } else if (val == "2") { return "<span title='微信商城单'>微信商城单</span>"; } else if (val == "3") { return "<span title='APP单'>APP单</span>"; } else if (val == "4") { return "<span title='小程序单'>小程序单</span>"; } else { return "<span title='电脑单'>电脑单</span>"; }
                      }
                  },
                  //{
                  //    title: '商城订单号', field: 'WXOrderNo', width: '110px', formatter: function (value) {
                  //        if (value != null && value != "") {
                  //            return "<span title='" + value + "'>" + value + "</span>";
                  //        }
                  //    }
                  //},
                  //{
                  //    title: '付款方式', field: 'PayWay', width: '60px', formatter: function (val, row, index) {
                  //        if (val == "0") { return "<span title='微信付款'>微信付款</span>"; } else if (val == "1") { return "<span title='额度付款'>额度付款</span>"; } else if (val == "2") { return "<span title='积分兑换'>积分兑换</span>"; } else { return ""; }
                  //    }
                  //},
                  //{
                  //    title: '支付订单号', field: 'WXPayOrderNo', width: '190px', formatter: function (value) {
                  //        if (value != null && value != "") {
                  //            return "<span title='" + value + "'>" + value + "</span>";
                  //        }
                  //    }
                  //},
                  { title: '已收款', field: 'ReceivedMoney', hidden: true }
                ]],
                onDblClickRow: function (index, row) { up(index, row); }
            });
            $('#dgAccount').datagrid({
                width: '500px',
                height: '360px',
                title: '账单列表', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OrderID',
                url: null,
                columns: [[
                  //{
                  //    title: '收入', field: 'TransportFee', width: '60px', align: 'right',
                  //    formatter: function (val, row, index) {
                  //        if (row.OrderModel == "1") {
                  //            return "-" + val;
                  //        } else { return val; }
                  //    }
                  //},
                  //{
                  //    title: '配送费', field: 'TransitFee', width: '60px', align: 'right', formatter: function (value) {
                  //        if (value != null && value != "") {
                  //            return "<span title='" + value + "'>" + value + "</span>";
                  //        }
                  //    }
                  //},
                  {
                      title: '总合计', field: 'TotalCharge', width: '70px', align: 'right',
                      formatter: function (val, row, index) {
                          if (row.OrderModel == "1") {
                              return "-" + val;
                          } else { return val; }
                      }
                  },
                  {
                      title: '结算状态', field: 'CheckStatus', width: '60px', formatter: function (val, row, index) { if (val == "0") { return "<span title='未结算'>未结算</span>"; } else if (val == "1") { return "<span title='已结清'>已结清</span>"; } else if (val == "2") { return "<span title='未结清'>未结清</span>"; } else { return ""; } }
                  },
                  {
                      title: '订单号', field: 'OrderNo', width: '90px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '订单类型', field: 'OrderModel', width: '60px', formatter: function (val, row, index) {
                          if (val == "0") { return "<span title='订货单'>订货单</span>"; } else if (val == "1") { return "<span title='退货单'>退货单</span>"; } else { return ""; }
                      }
                  },
                  {
                      title: '件数', field: 'Piece', width: '50px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  { title: '开单时间', field: 'CreateDate', width: '130px', formatter: DateTimeFormatter },
                  {
                      title: '客户名称', field: 'PayClientName', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '公司名称', field: 'AcceptUnit', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '收货人', field: 'AcceptPeople', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '手机号码', field: 'AcceptCellphone', width: '90px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '收货地址', field: 'AcceptAddress', width: '120px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '到达站', field: 'Dest', width: '50px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '业务员', field: 'SaleManName', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '开单员', field: 'CreateAwb', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '订单状态', field: 'AwbStatus', width: '60px',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "<span title='已下单'>已下单</span>"; }
                          else if (val == "1") { return "<span title='正在备货'>正在备货</span>"; }
                          else if (val == "2") { return "<span title='已出库'>已出库</span>"; }
                          else if (val == "3") { return "<span title='运输在途'>运输在途</span>"; }
                          else if (val == "4") { return "<span title='已到达'>已到达</span>"; }
                          else if (val == "5") { return "<span title='已签收'>已签收</span>"; }
                          else { return ""; }
                      }
                  },
                  {
                      title: '订单类型', field: 'OrderType', width: '60px', formatter: function (val, row, index) {
                          if (val == "0") { return "<span title='电脑单'>电脑单</span>"; } else if (val == "1") { return "<span title='企业微信单'>企业微信单</span>"; } else if (val == "2") { return "<span title='微信商城单'>微信商城单</span>"; } else if (val == "3") { return "<span title='APP单'>APP单</span>"; } else if (val == "4") { return "<span title='小程序单'>小程序单</span>"; } else { return "<span title='电脑单'>电脑单</span>"; }
                      }
                  },
                  //{
                  //    title: '商城订单号', field: 'WXOrderNo', width: '110px', formatter: function (value) {
                  //        if (value != null && value != "") {
                  //            return "<span title='" + value + "'>" + value + "</span>";
                  //        }
                  //    }
                  //},
                  //{
                  //    title: '付款方式', field: 'PayWay', width: '60px', formatter: function (val, row, index) {
                  //        if (val == "0") { return "<span title='微信付款'>微信付款</span>"; } else if (val == "1") { return "<span title='额度付款'>额度付款</span>"; } else if (val == "2") { return "<span title='积分兑换'>积分兑换</span>"; } else { return ""; }
                  //    }
                  //},
                  //{
                  //    title: '支付订单号', field: 'WXPayOrderNo', width: '190px', formatter: function (value) {
                  //        if (value != null && value != "") {
                  //            return "<span title='" + value + "'>" + value + "</span>";
                  //        }
                  //    }
                  //},
                  { title: '已收款', field: 'ReceivedMoney', hidden: true }
                ]],
                onDblClickRow: function (index, row) { down(index, row); }
            })
        }
        //新增账单数据
        function up(index, rows) {
            //if (rows.CheckStatus == "1") { return; }
            $('#dgSave').datagrid('deleteRow', index);
            var indexd = $('#dgAccount').datagrid('getRowIndex', rows.OrderID);
            if (indexd < 0) {
                $('#dgAccount').datagrid('appendRow', rows);
                var sum = Number($('#Total').textbox('getValue'));
                //var rebate = Number($('#RebateMoney').textbox('getValue'));
                //var ComeMoney = Number($('#ComeMoney').textbox('getValue'));
                if (isNaN(sum)) { sum = 0; }
                //if (Number(rows.TotalCharge - rows.ReceivedMoney) > 0) {
                //    if (rows.OrderModel == "1") {
                //        sum -= Number(rows.TotalCharge - rows.ReceivedMoney);
                //    } else {
                //        sum += Number(rows.TotalCharge - rows.ReceivedMoney);
                //    }
                //}
                if (Number(rows.TotalCharge) > 0) {
                    if (rows.OrderModel == "1") {
                        sum -= Number(rows.TotalCharge);
                    } else {
                        sum += Number(rows.TotalCharge);
                    }
                }
                $('#Total').textbox('setValue', Number(sum).toFixed(2))
                //var AT = 0;
                //AT = sum - rebate - ComeMoney;
                //$('#ATotal').textbox('setValue', Number(AT).toFixed(2))
            }
        }
        //移除账单数据
        function down(index, rows) {
            $('#dgAccount').datagrid('deleteRow', index);
            var sum = Number($('#Total').textbox('getValue'));
            //var Asum = Number($('#ATotal').textbox('getValue'));
            if (isNaN(sum)) { sum = 0; }
            if (Number(rows.TotalCharge) > 0) {
                if (rows.OrderModel == "1") {
                    sum += Number(rows.TotalCharge);
                    //Asum += Number(rows.TotalCharge - rows.ReceivedMoney);
                } else {
                    sum -= Number(rows.TotalCharge);
                    //Asum -= Number(rows.TotalCharge - rows.ReceivedMoney);
                }
            }
            $('#Total').textbox('setValue', Number(sum).toFixed(2))
            //$('#ATotal').textbox('setValue', Number(Asum).toFixed(2))
            var indexd = $('#dgSave').datagrid('getRowIndex', rows.OrderID);
            if (indexd < 0) {
                $('#dgSave').datagrid('appendRow', rows);
            }
        }
        //删除账单信息
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
                        url: 'clientApi.aspx?method=DelCargoAccount',
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

    </script>

</asp:Content>
