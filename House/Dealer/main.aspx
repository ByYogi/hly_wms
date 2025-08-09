<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="main.aspx.cs" Inherits="Dealer.main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        <%= Dealer.Common.GetSystemNameAndVersion()%></title>
    <link rel="shortcut icon" type="image/x-icon" href="CSS/image/jh.ico" media="screen" />
    <link href="JS/easy/css/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="JS/easy/css/icon.css" rel="stylesheet" type="text/css" />
    <link href="CSS/default.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .header {
            background: linear-gradient(to bottom, #eff5ff 0px, #e0ecff 100%) repeat-x scroll 0 0 rgba(0, 0, 0, 0);
        }
    </style>

    <script src="JS/easy/js/jquery.min.js" type="text/javascript"></script>

    <script src="JS/easy/js/jquery.easyui.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        var _menus = {};
        $(document).ready(function () {
            _menus = <%=res%>;
        });

    </script>

    <script src="JS/easy/js/easyui-lang-zh_CN.js" type="text/javascript"></script>

    <script src="JS/lefttree.js" type="text/javascript"></script>

    <script src="JS/Date/ComJs.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            width = Number($(window).width())/2;
            height = Number($(window).height())/2;
            $('#dgNotice').datagrid({
                width: width,
                height: height,
                title: '<span style=color:red;font-size:15px;>公告通知</span>',
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: true, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                fitColumns: true, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                showFooter: true,
                toolbar: '#saPanel',
                columns: [[
                    {
                        title: '消息类型', field: 'NoticeType', width: '80px', align: 'center', formatter: function (val) {
                            if (val == "0") { return "<span title='系统公告'>系统公告</span>"; }
                            else if (val == "1") { return "<span title='到货通知'>到货通知</span>"; }
                            else { return ""; }
                        }
                    },
                    {
                        title: '消息标题', field: 'Title', width: '350px', align: 'center', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '阅读状态', field: 'ReadStatus', width: '80px', align: 'center',
                        formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='未读'>未读</span>"; }
                            else if (val == "1") { return "<span title='已读'>已读</span>"; }
                            else { return ""; }
                        }
                    },
                    { title: '消息时间', field: 'OP_DATE', width: '150px', align: 'center', formatter: DateTimeFormatter }
                ]],
                rowStyler: function (index, row) {
                    if (row.ReadStatus == "0") {
                        return "background-color:#F4982C;font-weight: bold;";
                    }
                },
                onClickRow: function (index, row) {
                    editItemByID(index);
                }
            });
            QueryWarnStock()
        });
        //查询
        function QueryWarnStock() {
            $('#dgNotice').datagrid('clearSelections');
            var gridOpts = $('#dgNotice').datagrid('options');
            gridOpts.url = '../FormService.aspx?method=QueryCargoNotice';
            $('#dgNotice').datagrid('load', {
                Title: $('#ATitle').val()
            });
        }
    </script>
</head>
<body class="easyui-layout">
    <div data-options="region:'north',border:false" class="header" style="height: 40px;">
        <div style="display: inline; padding-left: 10px; font-size: 25px; font-weight: bolder; font-family: 微软雅黑,黑体,宋体;">
            <% if(UserInfor.HouseID == 82) { %>锦湖<% } else if(UserInfor.HouseID==10) { %>安泰路斯<% } %> <%= Dealer.Common.GetSystemNameAndVersion()%>
        </div>
        <div style="display: inline; position: absolute; top: 10px; text-align: right; right: 10px; font-size: 14px;">
            <asp:Literal ID="welcome" runat="server"></asp:Literal>
            <a href="#" class="easyui-linkbutton" iconcls="icon-eye" plain="true" onclick="ChangePaw()">&nbsp;修改密码&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-shield" plain="true" onclick="Quit()"> &nbsp;退&nbsp;出&nbsp;</a>
        </div>
    </div>
    <div data-options="region:'west',split:true" style="width: 150px;">
        <div id="wnav" class="easyui-accordion" fit="true" border="false">
        </div>
    </div>
    


    <div data-options="region:'center'" style="width: 100%; height: 100%">
        <div id="tt" class="easyui-tabs" data-options="fit:true">
            <div title="首页">
                <div id="saPanel">
                    <table>
                        <tr>
                            <td style="text-align: right;">标题:
                            </td>
                            <td>
                                <input id="ATitle" class="easyui-textbox" data-options="prompt:'请输入标题'" style="width: 100px" />
                            </td>
                            <td>
                                <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="QueryWarnStock()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
                                <form runat="server" id="fm1">
                                    <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
                                </form>
                            </td>
                        </tr>
                    </table>
                </div>
                <div style="float: left;">
                    <table id="dgNotice" class="easyui-datagrid"></table>
                </div>
            </div>
        </div>
    </div>
    
    <div id="dlg" class="easyui-dialog" style="width: 800px; height: 500px; padding: 0px" closed="true" buttons="#dlg-buttons">
            <div id="memo">
            </div>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close');QueryWarnStock()">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <script type="text/javascript">
        function editItemByID(Did) {            
            var row = $("#dgNotice").datagrid('getData').rows[Did];
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '查看信息');
                $('#fm').form('clear');
                $('#fm').form('load', row);
                $('#HouseID').combobox('disable');
                $.ajax({
                    async: false, cache: false, dataType: "json",
                    url: "../FormService.aspx?method=QueryNoticeByID&ID=" + row.ID,
                    success: function (text) {
                        $("#memo").html(text.Memo);
                        $.parser.parse($("#memo"));
                    }
                });
            }
        }
        <%--//导出数据
        function AwbExport() {
            var row = $("#dgWarnStock").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            var obj = document.getElementById("<%=btnDerived.ClientID %>");
            obj.click();
        }--%>
        function Quit() { window.location = "Default.aspx"; }
        function ChangePaw() { addTab("修改密码", "../ChangePassword.aspx", "icon-eye"); }
    </script>

</body>
</html>
