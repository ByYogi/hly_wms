<%@ Page Title="客户来款记录" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="financeClientIncomeMoney.aspx.cs" Inherits="Cargo.Finance.financeClientIncomeMoney" %>

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
            $('#dgIncome').datagrid({ height: height });
        }
        $(document).ready(function () {
            $('#dgIncome').datagrid({
                width: '100%',
                title: '', //标题内容
                pagination: true, //分页是否显示
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2],
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'IncomeID',
                url: null,
                toolbar: '#Incomebar',
                columns: [[
                  { title: '', field: 'IncomeID', checkbox: true, width: '30px' },
                  { title: '客户编码', field: 'ClientNum', width: '60px' },
                  { title: '客户姓名', field: 'Boss', width: '60px' },
                  { title: '来款金额', field: 'Money', width: '60px' },
                  { title: '来款时间', field: 'CreateDate', width: '80px', formatter: DateFormatter },
                  { title: '转入账号', field: 'Aliases', width: '100px' },
                  {
                      title: '账号类型', field: 'CardType', width: '60px', formatter: function (val, row, index) {
                          if (val == "0") { return "现金"; }
                          else if (val == "1") { return "银行卡"; }
                          else if (val == "2") { return "微信"; }
                          else if (val == "3") { return "支付宝"; }
                          else { return ""; }
                      }
                  },
                  { title: '开户行', field: 'Bank', width: '80px' },
                  { title: '开户名', field: 'CardName', width: '80px' },
                  { title: '账号', field: 'CardNum', width: '120px' },
                  { title: '操作人', field: 'OP_ID', width: '50px' },
                  { title: '操作时间', field: 'OP_DATE', width: '120px', formatter: DateTimeFormatter }
                ]],
                onClickRow: function (index, row) {
                    $('#dgIncome').datagrid('clearSelections');
                    $('#dgIncome').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { editIncomeByID(index); }
            });
            var datenow = new Date();
            $('#InStartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#InEndDate').datebox('setValue', getNowFormatDate(datenow));
            //所在仓库
            $('#InID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#InBoss').combobox('clear');
                    var url = '../Client/clientApi.aspx?method=AutoCompleteClient&houseID=' + rec.HouseID;
                    $('#InBoss').combobox('reload', url);

                }
            });
            $('#InID').combobox('setValue', '<%=UserInfor.HouseID%>');
            $('#InBoss').combobox('clear');
            var url = '../Client/clientApi.aspx?method=AutoCompleteClient&houseID=<%=UserInfor.HouseID%>';
            $('#InBoss').combobox('reload', url);
            $('#InID').combobox('textbox').bind('focus', function () { $('#InID').combobox('showPanel'); });
            $('#InBoss').combobox('textbox').bind('focus', function () { $('#InBoss').combobox('showPanel'); });
        });
        //查询客户来款记录
        function QueryClientIncome() {
            $('#dgIncome').datagrid('clearSelections');
            var gridOpts = $('#dgIncome').datagrid('options');
            gridOpts.url = '../Client/clientApi.aspx?method=QueryClientIncome';
            $('#dgIncome').datagrid('load', {
                ClientNum: $("#InBoss").combobox('getValue'),
                StartDate: $('#InStartDate').datebox('getValue'),
                EndDate: $('#InEndDate').datebox('getValue'),
                HID: $("#InID").combobox('getValue')
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
                <td>
                    <input id="InID" class="easyui-combobox" style="width: 100px;" />
                </td>
                <td style="text-align: right;">客户姓名:
                </td>
                <td>
                    <input id="InBoss" style="width: 100px;" data-options="valueField: 'ClientNum', textField: 'Boss'" class="easyui-combobox" />
                </td>
                <td style="text-align: right;">来款时间:
                </td>
                <td>
                    <input id="InStartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="InEndDate" class="easyui-datebox" style="width: 100px">
                </td>
            </tr>
        </table>
    </div>
    <table id="dgIncome" class="easyui-datagrid">
    </table>
    <div id="Incomebar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="QueryClientIncome()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addIncome()">
            &nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-edit"
                plain="false" onclick="editIncome()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
                    iconcls="icon-cut" plain="false" onclick="DelIncome()">&nbsp;删&nbsp;除&nbsp;</a>
    </div>
    <div id="dlgAddIncome" class="easyui-dialog" style="width: 600px; height: 400px;"
        closed="true" buttons="#dlgAddIncome-buttons">
        <div id="saPanel" class="easyui-panel" title="" data-options="iconCls:'icon-search'">
            <form id="fmIncome" class="easyui-form" method="post">
                <input name="IncomeID" type="hidden" />
                <input name="ClientNum" type="hidden" id="ClientNum" />
                <table>
                    <tr>
                        <td style="text-align: right;">所属仓库:
                        </td>
                        <td>
                            <input id="HouseID" name="HouseID" class="easyui-combobox" style="width: 200px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">客户姓名:
                        </td>
                        <td>
                            <input id="ClientID" name="ClientID" class="easyui-combobox" data-options="valueField: 'ClientID', textField: 'Boss'" style="width: 200px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">账户类型:
                        </td>
                        <td>
                            <input name="CardType" id="inCardType0" type="radio" value="0"><label for="inCardType0" style="font-size: 15px;">现金</label>
                            <input name="CardType" id="inCardType1" type="radio" value="1"><label for="inCardType1" style="font-size: 15px;">银行卡</label>
                            <input name="CardType" id="inCardType2" type="radio" value="2"><label for="inCardType2" style="font-size: 15px;">微信</label>
                            <input name="CardType" id="inCardType3" type="radio" value="3"><label for="inCardType3" style="font-size: 15px;">支付宝</label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">
                            <div id="zr">
                                来款账号:
                            </div>
                        </td>
                        <td>
                            <input id="BasicID" name="BasicID" class="easyui-combobox" style="width: 200px;"
                                panelheight="auto" data-options="required:true " />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">来款金额:
                        </td>
                        <td>
                            <input id="Money" name="Money" class="easyui-numberbox" data-options="min:0,precision:2,required:true " style="width: 200px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">来款时间:
                        </td>
                        <td>
                            <input id="CreateDate" name="CreateDate" class="easyui-datebox" data-options="required:true " style="width: 200px">
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
    <div id="dlgAddIncome-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveIncomeRecord()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgAddIncome').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>

    <script type="text/javascript">

        //删除客户来款记录
        function DelIncome() {
            var rows = $('#dgIncome').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: '../Client/clientApi.aspx?method=DelClientIncome',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                $('#dgIncome').datagrid('reload');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }
        //保存客户来款记录
        function saveIncomeRecord() {
            $('#fmIncome').form('submit', {
                url: '../Client/clientApi.aspx?method=saveIncomeRecord',
                onSubmit: function (param) {
                    //param.ClientTypeName = $('#TypeID').combobox('getText');
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $('#dlgAddIncome').dialog('close'); 	// close the dialog
                        $('#dgIncome').datagrid('reload');
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
        //新增来款记录
        function addIncome() {
            $('#dlgAddIncome').dialog('open').dialog('setTitle', '新增客户来款数据');
            $('#fmIncome').form('clear');
            //所在仓库
            $('#HouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#ClientID').combobox('clear');
                    var url = '../Client/clientApi.aspx?method=AutoCompleteClient&houseID=' + rec.HouseID;
                    $('#ClientID').combobox('reload', url);
                    //一级仓库
                    $('#ClientID').combobox({
                        onSelect: function (fai) { $("#ClientNum").val(fai.ClientNum); }
                    });
                }
            });
            $("input[name=CardType]").click(function () { inCont(); });
        }
        //修改客户来款信息
        function editIncomeByID(Did) {
            var row = $("#dgIncome").datagrid('getData').rows[Did];
            if (row) {
                $('#fmIncome').form('clear');
                $('#dlgAddIncome').dialog('open').dialog('setTitle', '修改客户来款信息');
                $('#fmIncome').form('load', row);
                //所在仓库
                $('#HouseID').combobox({
                    url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                    valueField: 'HouseID', textField: 'Name',
                    onSelect: function (rec) {
                        $('#ClientID').combobox('clear');
                        var url = '../Client/clientApi.aspx?method=AutoCompleteClient&houseID=' + rec.HouseID;
                        $('#ClientID').combobox('reload', url);
                        //一级仓库
                        $('#ClientID').combobox({
                            onSelect: function (fai) { $("#ClientNum").val(fai.ClientNum); }
                        });
                    }
                });
                $('#HouseID').combobox('setValue', row.HouseID);
                $('#ClientID').combobox('clear');
                var url = '../Client/clientApi.aspx?method=AutoCompleteClient&houseID=' + row.HouseID;
                $('#ClientID').combobox('reload', url);
                $('#ClientID').combobox('setValue', row.ClientID);

                $("input[name=CardType]").click(function () { inCont(); });
                inCont();
                $('#BasicID').combobox('setValue', row.BasicID);
            }
        }

        function inCont() {
            switch ($("input[name=CardType]:checked").attr("id")) {
                case "inCardType0":
                    $("#zr").html("现金仓库:");
                    $('#BasicID').combobox({
                        url: '../Finance/financeApi.aspx?method=AutoCompleteCard&tk=0',
                        valueField: 'BasicID',
                        textField: 'Aliases'
                    });
                    break;
                case "inCardType1":
                    $("#zr").html("转入账号:");
                    $('#BasicID').combobox({
                        url: '../Finance/financeApi.aspx?method=AutoCompleteCard&tk=1',
                        valueField: 'BasicID',
                        textField: 'Aliases'
                    });
                    break;
                case "inCardType2":
                    $("#zr").html("转入微信:");
                    $('#BasicID').combobox({
                        url: '../Finance/financeApi.aspx?method=AutoCompleteCard&tk=2',
                        valueField: 'BasicID',
                        textField: 'Aliases'
                    });
                    break;
                case "inCardType3":
                    $("#zr").html("转入支付宝:");
                    $('#BasicID').combobox({
                        url: '../Finance/financeApi.aspx?method=AutoCompleteCard&tk=3',
                        valueField: 'BasicID',
                        textField: 'Aliases'
                    });
                    break;
                default:
                    break;
            }
        }
        //修改客户信息
        function editIncome() {
            var row = $('#dgIncome').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                $('#fmIncome').form('clear');
                $('#dlgAddIncome').dialog('open').dialog('setTitle', '修改客户来款信息');
                $('#fmIncome').form('load', row);
                //所在仓库
                $('#HouseID').combobox({
                    url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                    valueField: 'HouseID', textField: 'Name',
                    onSelect: function (rec) {
                        $('#ClientID').combobox('clear');
                        var url = '../Client/clientApi.aspx?method=AutoCompleteClient&houseID=' + rec.HouseID;
                        $('#ClientID').combobox('reload', url);
                        //一级仓库
                        $('#ClientID').combobox({
                            onSelect: function (fai) { $("#ClientNum").val(fai.ClientNum); }
                        });
                    }
                });
                $('#HouseID').combobox('setValue', row.HouseID);
                $('#ClientID').combobox('clear');
                var url = '../Client/clientApi.aspx?method=AutoCompleteClient&houseID=' + row.HouseID;
                $('#ClientID').combobox('reload', url);
                $('#ClientID').combobox('setValue', row.ClientID);
                $("input[name=CardType]").click(function () { inCont(); });
                inCont();
                $('#BasicID').combobox('setValue', row.BasicID);
            }
        }
    </script>

</asp:Content>
