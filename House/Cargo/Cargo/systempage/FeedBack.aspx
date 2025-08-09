<%@ Page Title="慧采云仓客户回访" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FeedBack.aspx.cs" Inherits="Cargo.systempage.FeedBack" %>

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
                idField: 'FID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    { title: '', field: 'FID', checkbox: true, width: '3%' },
                    {
                        title: '所属仓库', field: 'HouseName', width: '5%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '门店名称', field: 'CompanyName', width: '5%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '客户姓名', field: 'Name', width: '5%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },

                    {
                        title: '回访人', field: 'FeedBackName', width: '5%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    { title: '回访时间', field: 'FeedBackTime', width: '8%', formatter: DateTimeFormatter },
                    {
                        title: '结果类型', field: 'ResultType', width: '5%',
                        formatter: function (val, row, index) {
                            if (val == "0") { return ""; }
                            else if (val == "1") { return "成功下单"; }
                            else if (val == "2") { return "无库存"; }
                            else if (val == "3") { return "价格高"; }
                            else if (val == "4") { return "配送时效"; }
                            else if (val == "5") { return "周期年份问题"; }
                            else if (val == "6") { return "跟车主没达成交易"; }
                            else if (val == "7") { return "了解价格"; }
                            else if (val == "8") { return "选择其他品牌"; }

                        }
                    },

                    {
                        title: '回访结果', field: 'FeedBackResult', width: '30%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },

                    { title: '操作时间', field: 'OPDATE', width: '8%', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { }//editItemByID(index);
            });
            //所在仓库
            $('#HID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
            });

            $('#HID').combobox('textbox').bind('focus', function () { $('#HID').combobox('showPanel'); });

        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'sysService.aspx?method=QueryCargoFeedBack';
            $('#dg').datagrid('load', {
                CompanyName: $('#CompanyName').val(),
                FeedBackName: $("#FeedBackName").val(),
                HID: $("#HID").combobox('getValue'),
                ResultType: $("#ResultType").combobox('getValue'),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
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
                <td style="text-align: right;">所属仓库:
                </td>
                <td style="width: 10%">
                    <input id="HID" class="easyui-combobox" style="width: 100%;" />
                </td>
                <td style="text-align: right;">门店名称:
                </td>
                <td style="width: 10%">
                    <input id="CompanyName" class="easyui-textbox" data-options="prompt:'请输入门店名称'" style="width: 100%" />
                </td>

                <td style="text-align: right;">回访人:
                </td>
                <td style="width: 10%">
                    <input id="FeedBackName" class="easyui-textbox" data-options="prompt:'请输入回访人'" style="width: 100%" />
                </td>

                <td style="text-align: right;">结果状态:
                </td>
                <td style="width: 10%">
                    <select class="easyui-combobox" id="ResultType" style="width: 100%;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="1">成功下单</option>
                        <option value="2">无库存</option>
                        <option value="3">价格高</option>
                        <option value="4">配送时效</option>
                        <option value="5">周期年份问题</option>
                        <option value="6">跟车主没达成交易</option>
                        <option value="7">了解价格</option>
                        <option value="8">选择其他品牌</option>
                    </select>
                </td>
                <td style="text-align: right;">回访时间:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px" />~
     <input id="EndDate" class="easyui-datebox" style="width: 100px" />
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="ExportShopOrderImport()" id="btnExportShopOrderImport">&nbsp;导&nbsp;出&nbsp;</a><%--&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">
            &nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-edit"
                plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
                    iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>--%>
        <form runat="server" id="fm1">
            <asp:Button ID="ShopOrderImport" runat="server" Style="display: none;" Text="导出" OnClick="btnShopOrder_Click" />
        </form>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 980px; height: 600px; padding: 0px"
        closed="true" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" name="HouseID" />
            <div id="saPanel">
                <table>
                    <tr>
                        <td style="text-align: right;">仓库名称:
                        </td>
                        <td>
                            <input name="Name" class="easyui-textbox" data-options="prompt:'请输入仓库名称',required:true"
                                style="width: 250px;" />
                        </td>

                        <td style="text-align: right;">仓库代码:
                        </td>
                        <td>
                            <input name="HouseCode" class="easyui-textbox" data-options="prompt:'请输入仓库代码',required:true"
                                style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">仓库负责人:
                        </td>
                        <td>
                            <input name="Person" class="easyui-textbox" data-options="prompt:'请输入仓库负责人',required:true"
                                style="width: 250px;" />
                        </td>

                        <td style="text-align: right;">仓库类型:
                        </td>
                        <td>
                            <select class="easyui-combobox" name="BelongHouse" id="eBelongHouse" editable="false" style="width: 250px;"
                                panelheight="auto" required="true">
                                <option value="0">迪乐泰</option>
                                <option value="1">好来运</option>
                                <option value="2">富添盛</option>
                                <option value="3">科技公司</option>
                                <option value="4">一二三轮胎</option>
                                <option value="5">采购商</option>
                                <option value="6">云仓</option>
                            </select>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right;">负责人手机:
                        </td>
                        <td>
                            <input name="Cellphone" class="easyui-textbox" style="width: 250px;" />
                        </td>

                        <td style="text-align: right;">发货城市:
                        </td>
                        <td>
                            <input name="DepCity" id="eDepCity" class="easyui-textbox" data-options="prompt:'请输入发货城市',required:true"
                                style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">仓库部门:
                        </td>
                        <td>
                            <input name="CargoDepart" id="eCargoDepart" class="easyui-textbox" data-options="prompt:'请输入仓库部门',required:true"
                                style="width: 250px;" />
                        </td>

                        <td style="text-align: right;">OES仓库部门:
                        </td>
                        <td>
                            <input name="OESCargoDepart" id="eOESCargoDepart" class="easyui-textbox" data-options="prompt:'请输入OES仓库部门',required:true"
                                style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">发货单表头:
                        </td>
                        <td>
                            <input name="SendTitle" class="easyui-textbox" style="width: 250px;" />
                        </td>

                        <td style="text-align: right;">拣货单表头:
                        </td>
                        <td>
                            <input name="PickTitle" class="easyui-textbox" style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">配送区域:
                        </td>
                        <td>
                            <input name="DeliveryArea" class="easyui-combobox" style="width: 250px;"
                                data-options="url:'houseApi.aspx?method=QueryCityData',valueField:'City',textField:'City',multiple:true" />
                        </td>
                        <td style="text-align: right;">状态标识:
                        </td>
                        <td>
                            <select class="easyui-combobox" id="DelFlag" name="DelFlag" style="width: 250px;"
                                panelheight="auto" required="true">
                                <option value="0">启用</option>
                                <option value="1">停用</option>
                            </select>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right;">开始营业:
                        </td>
                        <td>
                            <input name="StartBusHours" class="easyui-textbox" style="width: 250px;" />
                        </td>

                        <td style="text-align: right;">结束营业:
                        </td>
                        <td>
                            <input name="EndBusHours" class="easyui-textbox" style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">运费标准:
                        </td>
                        <td colspan="3">一条：
                            <input name="LogisFee" class="easyui-textbox" style="width: 100px;" />
                            两条：
                            <input name="TwoLogisFee" class="easyui-textbox" style="width: 100px;" />
                            三条及以上：
                            <input name="ThreeLogisFee" class="easyui-textbox" style="width: 100px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">超期天数:
                        </td>
                        <td>
                            <input name="OverDayNum" class="easyui-numberbox" data-options="min:0,precision:0" style="width: 250px;" />
                        </td>

                        <td style="text-align: right;">超期单价:
                        </td>
                        <td>
                            <input name="OverDueUnitPrice" class="easyui-numberbox" data-options="min:0,precision:2" style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">企微标签:
                        </td>
                        <td>
                            <input name="HCYCOrderPushTagID" class="easyui-textbox" style="width: 250px;" />
                        </td>
                        <td style="text-align: right;">次日达运费:
                        </td>
                        <td>
                            <input name="NextDayLogisFee" class="easyui-textbox" style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">仓库地址:
                        </td>
                        <td colspan="3">
                            <input name="Address" id="Address" class="easyui-textbox" data-options="prompt:'请输入仓库地址'"
                                style="width: 700px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">入驻品牌:
                        </td>
                        <td colspan="3">
                            <textarea id="OperaBrand" rows="4" name="OperaBrand" style="width: 700px;"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">仓库备注:
                        </td>
                        <td colspan="3">
                            <textarea id="Remark" rows="4" name="Remark" style="width: 700px;"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">慧采云仓:</td>
                        <td colspan="3">
                            <input type="checkbox" class="easyui-checkbox" id="IsCanRush" name="IsCanRush" onclick="checkbox(this)" />
                            <label for="IsCanRush">急送</label>&nbsp;&nbsp;
                            <input type="checkbox" class="easyui-checkbox" id="IsCanPickUp" name="IsCanPickUp" onclick="checkbox(this)" />
                            <label for="IsCanPickUp">自提</label>&nbsp;&nbsp;
                            <input type="checkbox" class="easyui-checkbox" id="IsCanNextDay" name="IsCanNextDay" onclick="checkbox(this)" />
                            <label for="IsCanNextDay">次日达</label>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">&nbsp;&nbsp;保&nbsp;存&nbsp;&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;&nbsp;取&nbsp;消&nbsp;&nbsp;</a>
    </div>

    <script type="text/javascript">
        function ExportShopOrderImport() {
            $.ajax({
                url: "sysService.aspx?method=QueryCargoFeedBackImport&HouseID=" + $('#HID').combobox('getValue') + "&ResultType=" + $('#ResultType').combobox('getValue') + "&CompanyName=" + $("#CompanyName").val() + "&FeedBackName=" + $("#FeedBackName").val() + "&StartDate=" + $('#StartDate').datebox('getValue') + "&EndDate=" + $('#EndDate').datebox('getValue'),
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=ShopOrderImport.ClientID %>"); obj.click() }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }
        function checkbox(obj) {
            if (obj.checked) {
                obj.value = 0;
            } else {
                obj.value = 1;
            }
        }
        //新增仓库信息
        function addItem() {
            $('#dlg').dialog('open').dialog('setTitle', '新增仓库信息');
            $('#fm').form('clear');
            $('#DelFlag').combobox('select', '0');
        }
        //修改仓库信息
        function editItem() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改仓库信息');
                var IsCanRush = document.getElementById("IsCanRush");
                if ($("#IsCanRush").prop('checked', 'checked')) { IsCanRush.value = 0; } else { IsCanRush.value = 1; }
                var IsCanPickUp = document.getElementById("IsCanPickUp");
                if ($("#IsCanPickUp").prop('checked', 'checked')) { IsCanPickUp.value = 0; } else { IsCanPickUp.value = 1; }
                var IsCanNextDay = document.getElementById("IsCanNextDay");
                if ($("#IsCanNextDay").prop('checked', 'checked')) { IsCanNextDay.value = 0; } else { IsCanNextDay.value = 1; }

                $('#fm').form('load', row);
                if (row.IsCanRush == "0") { $("#IsCanRush").prop('checked', 'checked'); }
                if (row.IsCanPickUp == "0") { $("#IsCanPickUp").prop('checked', 'checked'); }
                if (row.IsCanNextDay == "0") { $("#IsCanNextDay").prop('checked', 'checked'); }
            }
        }
        //修改仓库信息
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改仓库信息');
                var IsCanRush = document.getElementById("IsCanRush");
                if ($("#IsCanRush").prop('checked', 'checked')) { IsCanRush.value = 0; } else { IsCanRush.value = 1; }
                var IsCanPickUp = document.getElementById("IsCanPickUp");
                if ($("#IsCanPickUp").prop('checked', 'checked')) { IsCanPickUp.value = 0; } else { IsCanPickUp.value = 1; }
                var IsCanNextDay = document.getElementById("IsCanNextDay");
                if ($("#IsCanNextDay").prop('checked', 'checked')) { IsCanNextDay.value = 0; } else { IsCanNextDay.value = 1; }
                $('#fm').form('load', row);

                if (row.IsCanRush == "0") { $("#IsCanRush").prop('checked', 'checked'); }
                if (row.IsCanPickUp == "0") { $("#IsCanPickUp").prop('checked', 'checked'); }
                if (row.IsCanNextDay == "0") { $("#IsCanNextDay").prop('checked', 'checked'); }
            }
        }
        //删除仓库信息
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
                        url: 'houseApi.aspx?method=DelCargoHouse',
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

        //保存仓库信息
        function saveItem() {
            $('#fm').form('submit', {
                url: 'houseApi.aspx?method=SaveCargoHouse',
                onSubmit: function () {
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $('#dlg').dialog('close'); 	// close the dialog
                        dosearch();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
    </script>

</asp:Content>
