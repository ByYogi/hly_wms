<%@ Page Title="基础数据管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="financeBasicDataManager.aspx.cs" Inherits="Cargo.Finance.financeBasicDataManager" %>

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
                idField: 'BasicID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                  { title: '', field: 'BasicID', checkbox: true, width: '30px' },
                  {
                      title: '所属仓库', field: 'HouseName', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '账户类型', field: 'CardType', width: '60px',
                      formatter: function (val, row, index) { if (val == "0") { return "<span title='现金'>现金</span>"; } else if (val == "1") { return "<span title='银行卡'>银行卡</span>"; } else if (val == "2") { return "<span title='微信'>微信</span>"; } else if (val == "3") { return "<span title='支付宝'>支付宝</span>"; } else { return ""; } }
                  },
                  {
                      title: '别名', field: 'Aliases', width: '110px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '开户名', field: 'CardName', width: '100px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '帐号', field: 'CardNum', width: '150px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '开户行', field: 'Bank', width: '110px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '余额', field: 'OverMoney', width: '90px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '状态', field: 'Status', width: '40px',
                      formatter: function (val, row, index) { if (val == "0") { return "<span title='正常'>正常</span>"; } else if (val == "1") { return "<span title='注销'>注销</span>"; } else { return ""; } }
                  },
                  { title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) {

                },
                onDblClickRow: function (index, row) {
                    editItemByID(index);
                }
            });
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            $('#HouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });
            $('#ApplyHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });
            $('#HouseID').combobox('textbox').bind('focus', function () { $('#HouseID').combobox('showPanel'); });
            $('#CardType').combobox('textbox').bind('focus', function () { $('#CardType').combobox('showPanel'); });
            $('#Status').combobox('textbox').bind('focus', function () { $('#Status').combobox('showPanel'); });
            $("input[name=CardType]").click(function () { showCont(); });
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');

        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../Finance/financeApi.aspx?method=QueryFinanceBasicData';
            $('#dg').datagrid('load', {
                HouseID: $("#AHouseID").combobox('getValue'),
                CardType: $("#CardType").combobox('getValue'),
                Status: $("#Status").combobox('getValue')
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
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width:100%">
        <table>
            <tr>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;" panelheight="auto" />
                </td>
                <td style="text-align: right;">账户类型:
                </td>
                <td>
                    <select class="easyui-combobox" id="CardType" style="width: 70px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">现金</option>
                        <option value="1">银行卡</option>
                        <option value="2">微信</option>
                        <option value="3">支付宝</option>
                    </select>
                </td>
                <td style="text-align: right;">账户状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="Status" style="width: 70px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">正常</option>
                        <option value="1">注销</option>
                    </select>
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">&nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-exclamation" plain="false" onclick="logout()">&nbsp;注&nbsp;销&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-arrow_refresh" plain="false" onclick="renew()">&nbsp;恢&nbsp;复&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 400px; height: 380px; padding: 10px 10px"
        closed="true" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" name="BasicID" />
            <input type="hidden" name="OldCardType" id="OldCardType" />
            <input type="hidden" name="OldHouseID" id="OldHouseID" />
            <input type="hidden" name="Status" />
            <table>
                <tr>
                    <td style="text-align: right;">所属仓库:
                    </td>
                    <td>
                        <input id="HouseID" name="HouseID" class="easyui-combobox" style="width: 100px;" panelheight="auto" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">适用仓库:</td>
                    <td>
                        <input id="ApplyHouseID" name="ApplyHouseID" class="easyui-combobox" style="width: 250px;" panelheight="auto" data-options="multiple:true" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">账户类型:
                    </td>
                    <td>
                        <input name="CardType" id="CardType0" type="radio" value="0"/><label for="CardType0" style="font-size: 15px;">现金</label>
                        <input name="CardType" id="CardType1" type="radio" value="1"/><label for="CardType1" style="font-size: 15px;">银行卡</label>
                        <input name="CardType" id="CardType2" type="radio" value="2"/><label for="CardType2" style="font-size: 15px;">微信</label>
                        <input name="CardType" id="CardType3" type="radio" value="3"/><label for="CardType3" style="font-size: 15px;">支付宝</label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">账户别名:
                    </td>
                    <td>
                        <input name="Aliases" class="easyui-textbox" data-options="prompt:'请输入账户别名'" style="width: 250px;"/>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">开户行:
                    </td>
                    <td>
                        <input name="Bank" id="ABank" class="easyui-textbox" data-options="prompt:'请输入开户行'" style="width: 250px;"/>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">开户名:
                    </td>
                    <td>
                        <input name="CardName" id="ACardName" class="easyui-textbox" data-options="prompt:'请输入开户名'" style="width: 250px;"/>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">账号:
                    </td>
                    <td>
                        <input name="CardNum" id="ACardNum" class="easyui-textbox" data-options="prompt:'请输入账号'" style="width: 250px;"/>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">账户余额:
                    </td>
                    <td>
                        <input name="OverMoney" class="easyui-numberbox" data-options="prompt:'请输入账户余额',min:0,precision:2" style="width: 250px;"/>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">备注:
                    </td>
                    <td>
                        <textarea id="Memo" rows="3" name="Memo" style="width: 250px;"></textarea>
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closeDlg()">&nbsp;取&nbsp;消&nbsp;</a>
    </div>

    <script type="text/javascript">
        function closeDlg() {
            $('#dlg').dialog('close');
            $('#dg').datagrid('reload');
        }
        function showCont() {
            switch ($("input[name=CardType]:checked").attr("id")) {
                case "CardType0":
                    $('#ABank').textbox('disable');
                    $('#ACardName').textbox('disable');
                    $('#ACardNum').textbox('disable');
                    break;
                case "CardType2":
                    $('#ABank').textbox('disable');
                    $('#ACardName').textbox('disable');
                    $('#ACardNum').textbox('disable');
                    break;
                case "CardType3":
                    $('#ABank').textbox('disable');
                    $('#ACardName').textbox('disable');
                    $('#ACardNum').textbox('disable');
                    break;
                default:
                    $('#ABank').textbox('enable');
                    $('#ACardName').textbox('enable');
                    $('#ACardNum').textbox('enable');
                    break;
            }
        }
        //新增
        function addItem() {
            $('#dlg').dialog('open').dialog('setTitle', '新增账号信息');
            $('#fm').form('clear');
        }
        //修改
        function editItem() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning'); return; }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改账号信息');
                $('#fm').form('clear');
                $('#fm').form('load', row);
                if (row.CardType == "0") {
                    $('#ABank').textbox('disable');
                    $('#ACardName').textbox('disable');
                    $('#ACardNum').textbox('disable');
                }
                if (row.CardType == "2") {
                    $('#ABank').textbox('disable');
                    $('#ACardName').textbox('disable');
                    $('#ACardNum').textbox('disable');
                }
                if (row.CardType == "3") {
                    $('#ABank').textbox('disable');
                    $('#ACardName').textbox('disable');
                    $('#ACardNum').textbox('disable');
                }
                else {
                    $('#ABank').textbox('enable');
                    $('#ACardName').textbox('enable');
                    $('#ACardNum').textbox('enable');
                }
                $('#OldCardType').val(row.CardType);
                $('#OldHouseID').val(row.HouseID);
            }
        }
        //修改
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改账号信息');
                $('#fm').form('clear');
                $('#fm').form('load', row);
                if (row.CardType == "0") {
                    $('#ABank').textbox('disable');
                    $('#ACardName').textbox('disable');
                    $('#ACardNum').textbox('disable');
                } if (row.CardType == "2") {
                    $('#ABank').textbox('disable');
                    $('#ACardName').textbox('disable');
                    $('#ACardNum').textbox('disable');
                }
                if (row.CardType == "3") {
                    $('#ABank').textbox('disable');
                    $('#ACardName').textbox('disable');
                    $('#ACardNum').textbox('disable');
                }
                else {
                    $('#ABank').textbox('enable');
                    $('#ACardName').textbox('enable');
                    $('#ACardNum').textbox('enable');
                }
                $('#OldCardType').val(row.CardType);
                $('#OldHouseID').val(row.HouseID);
            }
        }
        //注销
        function logout() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要注销的数据！', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定注销？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: '../Finance/financeApi.aspx?method=DelBasicData&ty=0',
                        type: 'post',
                        dataType: 'json',
                        data: { data: json },
                        success: function (text) {
                            if (text.Result == true) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '注销成功!', 'info'); $('#dg').datagrid('reload'); }
                            else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning'); }
                        }
                    });
                }
            });
        }
        //恢复
        function renew() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要恢复的数据！', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定恢复？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: '../Finance/financeApi.aspx?method=DelBasicData&ty=2',
                        type: 'post',
                        dataType: 'json',
                        data: { data: json },
                        success: function (text) {
                            if (text.Result == true) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '恢复成功!', 'info'); $('#dg').datagrid('reload'); }
                            else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning'); }
                        }
                    });
                }
            });
        }
        //删除
        function DelItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '删除后不能恢复，确定删除？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: '../Finance/financeApi.aspx?method=DelBasicData&ty=1',
                        type: 'post',
                        dataType: 'json',
                        data: { data: json },
                        success: function (text) {
                            if (text.Result == true) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info'); $('#dg').datagrid('reload'); }
                            else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning'); }
                        }
                    });
                }
            });
        }

        //保存单位信息
        function saveItem() {
            $('#fm').form('submit', {
                url: '../Finance/financeApi.aspx?method=SaveFinanceBasicData',
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
