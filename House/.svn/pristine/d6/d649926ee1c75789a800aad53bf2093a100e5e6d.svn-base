<%@ Page Title="客户账单管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="suppClientBillManager.aspx.cs" Inherits="Cargo.Client.suppClientBillManager" %>

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
                        title: '账单号', field: 'AccountNO', width: '8%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '供应商名称', field: 'ClientName', width: '100px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    },
                    {
                        title: '所属仓库', field: 'HouseName', width: '6%', formatter: function (value) {
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
                        title: '账单金额(元)', field: 'Total', align: 'right', width: '6%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '优惠卷费用(元)', field: 'InsuranceFee', align: 'right', width: '6%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '配送费(元)', field: 'TransitFee', align: 'right', width: '6%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '物流费(元)', field: 'DeliveryFee', align: 'right', width: '6%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '超期费(元)', field: 'OverDueFee', align: 'right', width: '6%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '其他费用(元)', field: 'OtherFee', align: 'right', width: '6%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '出仓费(元)', field: 'OutStorageFee', align: 'right', width: '6%', formatter: function (value) {
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
                        title: '结算状态', field: 'CheckStatus', width: '5%',
                        formatter: function (val, row, index) {
                            if (val == "0") { return "未结算"; }
                            else if (val == "1") { return "已结清"; }
                            else if (val == "2") { return "未结清"; }
                            else { return "未结算"; }
                        }
                    },
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

                    { title: '操作时间', field: 'OPDATE', width: '10%', formatter: DateTimeFormatter }
                ]],
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) {
                    editItemByID(index);
                },
                onLoadSuccess: function (data) { },
            });

            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            //所在仓库
            $('#AHouseID').combobox({ url: '../House/houseApi.aspx?method=CargoPermisionHouse', valueField: 'HouseID', textField: 'Name' });
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            $('#AClientName').combobox({ url: '../Client/clientApi.aspx?method=AutoCompleteClient&ClientType=4', valueField: 'ClientID', textField: 'ClientName' });
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
            gridOpts.url = 'clientApi.aspx?method=QuerySuppClientBillManager';
            $('#dg').datagrid('load', {
                AccountNO: $('#AAccountNO').val(),
                HouseID: $("#AHouseID").combobox('getValue'),//仓库ID
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
                    <input id="AAccountNO" class="easyui-textbox" data-options="prompt:'请输入账单号'" style="width: 100px">
                </td>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;" />
                </td>
                <td style="text-align: right;">供应商名称:
                </td>
                <td>
                    <input id="AClientName" style="width: 100px;" data-options="valueField: 'ClientID', textField: 'ClientName'" class="easyui-combobox" />
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
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">&nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cart_put" plain="false" onclick="PayAcceptMoney()">&nbsp;一键分账&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="BillExport()" id="btnExport">&nbsp;导&nbsp;出&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-application_get" plain="false" onclick="BillPush()" id="btnBillPush">账单推送</a>
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
                    <td style="text-align: right;">供应商名称:
                    </td>
                    <td>
                        <input id="PayClientNum" style="width: 100px;" data-options="valueField: 'ClientNum', textField: 'ClientName'" class="easyui-combobox" />
                    </td>
                    <%--<td style="text-align: right;">结算状态:
                    </td>
                    <td>
                        <select class="easyui-combobox" id="BCheckStatus" style="width: 70px;" panelheight="auto">
                            <option value="-1">全部</option>
                            <option value="0">未结算</option>
                            <option value="1">已结清</option>
                            <option value="2">未结清</option>
                        </select>
                    </td>--%>
                </tr>
                <tr>
                    <td colspan="4">
                        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="QueryOrderAccount()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
                        <%--<a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="plAddOrder()">&nbsp;批量添加&nbsp;</a>&nbsp;&nbsp;
                        <a href="#" class="easyui-linkbutton" id="undo" iconcls="icon-clear" onclick="reset()"> &nbsp;重&nbsp;置&nbsp;</a>--%>
                    </td>
                    <td style="text-align: right;">日期范围:
                    </td>
                    <td colspan="3">
                        <input id="BStartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="BEndDate" class="easyui-datebox" style="width: 100px">
                    </td>
                </tr>
            </table>
        </div>
        <table id="dgSave" class="easyui-datagrid">
        </table>
        <%--<table>
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
        </table>--%>
        <div id="saPanel5">
            <form id="fm" class="easyui-form" method="post">
                <input type="hidden" name="AccountID" id="AccountID" />
                <input type="hidden" name="ClientID" id="AClientID" />
                <input type="hidden" name="ClientNum" id="AClientNum" />
                <input type="hidden" name="ClientName" id="ClientName" />
                <input type="hidden" name="HouseID" id="HouseID" />

                <table>
                    <%--<tr>
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
                    </tr>--%>
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
    <div id="dlgV2" class="easyui-dialog" style="width: 1000px; height: 400px; padding: 0px"
        closed="true" buttons="#dlg-buttonsV2">
        <div id="saPanelV2">
            <form id="fmV2" class="easyui-form" method="post">
                <input type="hidden" name="AccountIDV2" id="AccountIDV2" />
                <input type="hidden" name="AccountNO" id="AccountNO" />
            </form>
            <table id="dgAccountV2" class="easyui-datagrid"></table>
        </div>
    </div>
    <div id="dlgV3" class="easyui-dialog" style="width: 400px; height: 200px; padding: 0px"
        closed="true" buttons="#dlg-buttonsV3">
        <div id="saPanelV3">
            <form id="fmV3" class="easyui-form" method="post">

                <table style="width: 100%">
                    <tr>

                        <td style="color: Red; font-weight: bolder; text-align: right;">金额:
                        </td>
                        <td>
                            <input name="Money" id="Money" class="easyui-numberbox" data-options="prompt:'请输入金额',required:true,min:0,precision:3" style="width: 100px">
                        </td>
                        <td style="color: Red; font-weight: bolder; text-align: right;">类型:
                        </td>
                        <td>
                            <td align="left">
                                <input name="AwbStatus" id="ATruckFlag1" checked type="radio" value="0"><label for="ATruckFlag1">增</label>
                                <input name="AwbStatus" id="ATruckFlag2" type="radio" value="1"><label for="ATruckFlag2">减</label>
                            </td>
                        </td>

                    </tr>
                    <tr>
                        <td style="text-align: right;">备注:
                        </td>
                        <td colspan="7" rowspan="2">
                            <textarea name="MemoV3" id="MemoV3" rows="5" style="width: 300px;"></textarea>
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
    <div id="dlg-buttonsV2">
        <a href="#" class="easyui-linkbutton" iconcls="icon-money_yen" onclick="ChargeOff()">&nbsp;&nbsp;冲&nbsp;账&nbsp;&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgV2').dialog('close')">&nbsp;&nbsp;取&nbsp;消&nbsp;&nbsp;</a>
    </div>
    <div id="dlg-buttonsV3">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveChargeOff()">&nbsp;&nbsp;保&nbsp;存&nbsp;&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgV3').dialog('close')">&nbsp;&nbsp;取&nbsp;消&nbsp;&nbsp;</a>
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
            var row = $("#dg").datagrid('getData').rows;
            var key = {
                AccountNO: $('#AAccountNO').val(),
                HouseID: $("#AHouseID").combobox('getValue'),//仓库ID
                ClientID: $("#AClientName").combobox('getValue'),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                Status: $("#AStatus").combobox('getValue'),
                ElecSign: $("#AElecSign").combobox('getValue'),
                CheckStatus: $("#ACheckStatus").combobox('getValue')
            }

            $.ajax({
                url: "clientApi.aspx?method=QueryAccountSplittingForExport",
                type: 'post', data: key,
                success: function (text) {
                    console.log(text)
                    if (text == "OK") {
                        var obj = document.getElementById("<%=btnDerived.ClientID %>");
                        obj.click();
                    }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }

        //账单推送
        function BillPush() {
            var rows = $('#dg').datagrid('getSelections');
            console.log(rows)
            if (rows.length == 0) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }

            var json = JSON.stringify(rows)
            $.ajax({
                url: "clientApi.aspx?method=wxBillPush",
                type: 'post', dataType: 'json', data: { submitData: json },
                success: function (text) {
                    if (text.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '账单已推送', 'info');
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                    }
                }
            });
        }
        function PayAcceptMoney() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要分账的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定分账？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'clientApi.aspx?method=suppClientAccountPay',
                        type: 'post', dataType: 'json', data: { submitData: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'info');
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
            var rows = $('#dgSave').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要分账的数据！', 'warning');
                return;
            }
            //var row = $('#dgSave').datagrid('getRows');
            //if (row.length <= 0) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '账单列表中没有数据', 'warning'); return; }
            var trd = $("#fm").form('enableValidation').form('validate');
            if (trd == false) { return; }
            var json = JSON.stringify(rows)
            var formjson = $("#fm").serialize();
            $.ajax({
                async: false,
                url: "clientApi.aspx?method=saveOrderBillAccount&" + formjson,
                type: "post", data: { submitData: json },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', result.Message, 'info'); $('#dlg').dialog('close'); dosearch();
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
                            sum -= Number(row.TotalCharge);
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
            if (!$('#PayClientNum').combobox('getValue')) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择供应商名称', 'warning');
                //event.preventDefault();
                //alert('请输入供应商名称');
            } else {
                var gridOpts = $('#dgSave').datagrid('options');
                gridOpts.url = 'clientApi.aspx?method=QueryOrderForBillCash';
                $('#dgSave').datagrid('load', {
                    OrderNo: $('#AOrderNo').val(),
                    StartDate: $('#BStartDate').datebox('getValue'),
                    EndDate: $('#BEndDate').datebox('getValue'),
                    Dest: "",//$("#Dest").combobox('getValue'),
                    PayClientNum: $("#PayClientNum").combobox('getValue'),
                    //PayClientName: $("#PayClientNum").combobox('getText'),
                    HouseID: $("#BHouseID").combobox('getValue'),//仓库ID
                    //CheckStatus: $('#BCheckStatus').combobox('getValue'),
                    CheckStatus: 1,
                    IsSupplierType: 0,
                    IsHouseType: 0,
                });
            }
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
            showGridAdd();

            //所在仓库
            $('#BHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $("#HouseID").val(rec.HouseID);
                }
            });
            //var url = '../Client/clientApi.aspx?method=AutoCompleteClient&ClientType=4';
            //$('#PayClientNum').combobox('reload', url);
            $("#HouseID").val("<%=UserInfor.HouseID%>");
            var url = '../Client/clientApi.aspx?method=AutoCompleteClient&ClientType=4';
            $('#PayClientNum').combobox('reload', url);
        }
        //修改账单信息
        function editItemByID(Did) {
            //$('#dgSave').datagrid('loadData', { total: 0, rows: [] });
            //$('#dgAccount').datagrid('loadData', { total: 0, rows: [] });
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlgV2').dialog('open').dialog('setTitle', '账单详情：' + row.AccountNO);
                $('#fmV2').form('load', row);

                $("#AccountIDV2").val(row.AccountID)

                showGrid(row)

                var gridOpts = $('#dgAccountV2').datagrid('options');
                gridOpts.url = 'clientApi.aspx?method=QueryBillOrderByAccountGoods&AccountNO=' + row.AccountNO

            }
        }
        //显示列表
        function showGridAdd() {
            $('#dgSave').datagrid({
                width: '100%',
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
                    //{
                    //    title: '总收入', field: 'TotalCharge', width: '70px', align: 'right',
                    //    formatter: function (val, row, index) {
                    //        if (row.OrderModel == "1") {
                    //            return "-" + val;
                    //        } else { return val; }
                    //    }
                    //},
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
                        title: '供应商名称', field: 'ClientName', width: '60px', formatter: function (value) {
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
                        title: '供应商名称', field: 'ClientName', width: '60px', formatter: function (value) {
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
        function showGrid(row) {
            //相同元素ID导致页面不显示
            $('#dgAccountV2').datagrid({
                width: '100%',
                height: '320px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: 'clientApi.aspx?method=QueryBillOrderByAccountGoods&AccountNO=' + row.AccountNO,
                columns: [[
                    { title: '账单号', field: 'AccountNO', width: '90px', align: 'right' },
                    { title: '订单号', field: 'OrderNo', width: '90px', align: 'right' },
                    { title: '金额', field: 'Total', width: '60px', },
                    { title: '优惠卷金额', field: 'InsuranceFee', width: '90px' },
                    { title: '配送费', field: 'TransitFee', width: '60px' },
                    { title: '物流费用', field: 'DeliveryFee', width: '80px' },
                    { title: '仓库超期费', field: 'OverDueFee', width: '80px' },
                    { title: '出仓费', field: 'OutStorageFee', width: '60px' },
                    { title: '退仓费', field: 'StoReleaseFee', width: '80px' },
                    { title: '平台其他费用', field: 'OtherExpensesFee', width: '80px' },
                    {
                        title: '订单类型', field: 'OrderModel', width: '60px', formatter: function (value) {
                            if (value == 0) {
                                return "客户单";
                            } else if (value == 1) {
                                return '退货单';
                            } else {
                                return '';
                            }
                        }
                    },

                ]],
            });
        }
        function ChargeOff() {
            $('#fmV3').form('clear');
            $("#ATruckFlag1").prop('checked', true);
            var AccountID = $("#AccountIDV2").val()
            var AccountNO = $("#AccountNO").val()

            $('#dlgV3').dialog('open').dialog('setTitle', '冲账：' + AccountNO);
        }
        function saveChargeOff() {
            var AccountID = $("#AccountIDV2").val()
            var AccountNO = $("#AccountNO").val()
            var Money = $("#Money").val()
            var MemoV3 = $("#MemoV3").val()
            var AwbStatus = $("input[name=AwbStatus]:checked").attr("value")

            $('#fmV3').form('submit', {
                url: 'clientApi.aspx?method=SaveChargeOff',
                onSubmit: function (param) {
                    param.Total = Money
                    param.OrderModel = AwbStatus
                    param.Memo = MemoV3
                    param.AccountID = AccountID
                    param.AccountNO = AccountNO
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $('#dlgV3').dialog('close'); 	// close the dialog
                        $('#dgAccountV2').datagrid('load', {
                            AccountNO: AccountNO,
                        });
                        dosearch();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
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
                        url: 'clientApi.aspx?method=DelCargoSuppClientAccount',
                        type: 'post', dataType: 'json', data: { submitData: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'info');
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
