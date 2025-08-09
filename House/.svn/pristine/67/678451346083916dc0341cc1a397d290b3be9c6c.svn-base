<%@ Page Title="微信消息推送" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wxMsgManager.aspx.cs" Inherits="Cargo.WeixinPush.wxMsgManager" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../JS/KindEditer/themes/default/default.css" rel="stylesheet" />
    <link href="../JS/KindEditer/plugins/code/prettify.css" rel="stylesheet" />
    <script type="text/javascript" src="../JS/KindEditer/kindeditor-all.js"></script>
    <script type="text/javascript" src="../JS/KindEditer/lang/zh-CN.js"></script>
    <script src="../JS/KindEditer/plugins/code/prettify.js" type="text/javascript"></script>
    <script type="text/javascript">
        var editor;
        $(function () {
            KindEditor.ready(function (K) {
                editor = K.create('#Memo', {
                    cssPath: '../JS/KindEditer/plugins/code/prettify.css',
                    uploadJson: '../Product/upload.ashx',
                    fileManagerJson: '../asp.net/file_manager_json.ashx',
                    allowFileManager: true,
                    afterCreate: function () { }
                });
                prettyPrint();
            });
        });
    </script>
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
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'MPID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                  { title: '', field: 'MPID', checkbox: true, width: '30px' },
                  { title: '所属仓库', field: 'HouseName', width: '80px' },
                  { title: '消息标题', field: 'MsgTitle', width: '120px' },
                  { title: '消息备注', field: 'MsgContent', width: '120px' },
                  { title: '消息正文', field: 'Memo', width: '120px' },
                  {
                      title: '是否上传', field: 'UploadStatus', width: '60px',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "未上传"; }
                          else if (val == "1") { return "已上传"; }
                          else { return ""; }
                      }
                  },
                  { title: '微信ID', field: 'media_id', width: '120px' },
                  {
                      title: '是否推送', field: 'PushStatus', width: '60px',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "未推送"; }
                          else if (val == "1") { return "已推送"; }
                          else { return ""; }
                      }
                  },
                  { title: '推送时间', field: 'PushDate', width: '130px', formatter: DateTimeFormatter },
                  { title: '操作人', field: 'OP_ID', width: '70px' },
                  { title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#UploadStatus').combobox('textbox').bind('focus', function () { $('#UploadStatus').combobox('showPanel'); });
            $('#PushStatus').combobox('textbox').bind('focus', function () { $('#PushStatus').combobox('showPanel'); });
            //所在仓库
            $('#HID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });
            $('#HID').combobox('textbox').bind('focus', function () { $('#HID').combobox('showPanel'); });
            //所在仓库
            $('#HouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });
            $('#HouseID').combobox('textbox').bind('focus', function () { $('#HouseID').combobox('showPanel'); });
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'houseApi.aspx?method=QueryCargoHouse';
            $('#dg').datagrid('load', {
                Name: $('#Name').val(),
                Person: $("#Person").val(),
                Cellphone: $("#Cellphone").val(),
                dFlag: $("#dFlag").combobox('getValue')
            });
            adjustment();
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
                <td style="text-align: right;">消息标题:
                </td>
                <td>
                    <input id="Name" class="easyui-textbox" data-options="prompt:'请输入消息标题'" style="width: 100px">
                </td>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="HID" class="easyui-combobox" style="width: 100px;"
                        panelheight="auto" />
                </td>
                <td style="text-align: right;">上传状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="UploadStatus" style="width: 70px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">未上传</option>
                        <option value="1">已上传</option>
                    </select>
                </td>
                <td style="text-align: right;">推送状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="PushStatus" style="width: 70px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">未推送</option>
                        <option value="1">已推送</option>
                    </select>
                </td>
                <td style="text-align: right;">时间范围:
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
            &nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-edit"
                plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
                    iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>
        &nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
            iconcls="icon-arrow_refresh_small" plain="false" onclick="UploadWeixin()">&nbsp;上传到微信&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
                iconcls="icon-arrow_out" plain="false" onclick="UploadWeixin()">&nbsp;推送&nbsp;</a>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 920px; height: 500px; padding: 1px 1px"
        closed="true" buttons="#dlg-buttons">
        <div id="saPanel">
            <form id="fm" class="easyui-form" method="post">
                <table>
                    <tr>
                        <td style="text-align: right;">所属仓库:
                        </td>
                        <td>
                            <input id="HouseID" name="HouseID" class="easyui-combobox" style="width: 120px;"
                                panelheight="auto" />
                        </td>
                        <td style="text-align: right;">消息类型:
                        </td>
                        <td>
                            <select class="easyui-combobox" id="AMsgType" name="MsgType" style="width: 120px;"
                                panelheight="auto">
                                <option value="0">图文消息</option>
                                <option value="1">文本消息</option>
                                <option value="2">音频消息</option>
                                <option value="3">图片消息</option>
                                <option value="4">卡券消息</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">消息标题:
                        </td>
                        <td colspan="3">
                            <input name="MsgTitle" class="easyui-textbox" data-options="prompt:'请输入消息标题'"
                                style="width: 500px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">消息备注:
                        </td>
                        <td colspan="3">
                            <input name="MsgContent" class="easyui-textbox" style="width: 500px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">消息正文:
                        </td>
                        <td colspan="3">
                            <textarea id="Memo" name="Memo" cols="100" rows="8" style="width: 800px; height: 300px; visibility: hidden;"></textarea>
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">&nbsp;保&nbsp;存&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>

    <script type="text/javascript">
        //新增仓库信息
        function addItem() {
            $('#dlg').dialog('open').dialog('setTitle', '新增微信消息');
            $('#fm').form('clear');
            editor.html("");
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
                $('#fm').form('load', row);
            }
        }
        //修改仓库信息
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改仓库信息');
                $('#fm').form('load', row);
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
