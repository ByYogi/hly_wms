<%@ Page Title="我的日报" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DailyReports.aspx.cs" Inherits="Cargo.systempage.DailyReports" %>

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
                editor = K.create('#Content', {
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
            $('#dgApproval').datagrid({ height: height });
        }
        $(document).ready(function () {
            $('#dg').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: 20, //每页多少条
                pageList: [20, 50],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    { title: '', field: 'ID', checkbox: true, width: '10%' },
                    {
                        title: '日报标题', field: 'Title', width: '30%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '报告人', field: 'UserName', width: '10%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '可批复人员', field: 'UserReportName', width: '20%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    { title: '操作时间', field: 'OP_DATE', width: '10%', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { editItemByID(index, 0); }
            });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));

            $('#dgApproval').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: 20, //每页多少条
                pageList: [20, 50],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbarApproval',
                columns: [[
                    { title: '', field: 'ID', checkbox: true, width: '10%' },
                    {
                        title: '日报标题', field: 'Title', width: '30%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '报告人', field: 'UserName', width: '10%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '可批复人员', field: 'UserReportName', width: '20%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    { title: '操作时间', field: 'OP_DATE', width: '10%', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) {
                    editItemByID(index, 1);
                }
            });

        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'sysService.aspx?method=QueryCargoDailyReports';
            $('#dg').datagrid('load', {
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                Title: $('#ATitle').val(),
            });

            $('#dgApproval').datagrid('clearSelections');
            var gridOpts = $('#dgApproval').datagrid('options');
            gridOpts.url = 'sysService.aspx?method=QueryCanDailyReports&IsReply=0';
            $('#dgApproval').datagrid('load', {
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                Title: $('#ATitle').val(),
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
    <div id="tbDepmanifest" class="easyui-tabs" data-options="fit:true">
        <div title="我的日报" data-options="iconCls:'icon-search'">
            <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
                <table>
                    <tr>
                        <td style="text-align: right;">日报标题:
                        </td>
                        <td style="width: 10%">
                            <input id="ATitle" class="easyui-textbox" data-options="prompt:'请输入日报标题'" style="width: 100px">
                        </td>
                        <td style="text-align: right;">操作时间:
                        </td>
                        <td>
                            <input id="StartDate" class="easyui-datebox" style="width: 100px">
                            ~
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
            </div>
            <div id="dlg" class="easyui-dialog" style="width: 800px; height: 600px; padding: 0px;"
                closed="true" buttons="#dlg-buttons">
                <div id="saPanel">
                    <form id="fm" class="easyui-form" method="post">
                        <input type="hidden" id="ID" name="ID" />
                        <input type="hidden" id="UserReportName" name="UserReportName" />
                        <table id="satable">
                            <tr>

                                <td style="text-align: right;">日报标题:
                                </td>
                                <td>
                                    <input name="Title" id="Title" class="easyui-textbox" data-options="prompt:'请输入日报标题',required:true"
                                        style="width: 400px;">
                                </td>
                            </tr>

                            <tr>
                                <td style="text-align: right;">日报内容:
                                </td>
                                <td colspan="3">
                                    <textarea id="Content" name="Content" cols="100" rows="8" style="width: 680px; height: 330px; visibility: hidden;"></textarea>
                                </td>
                            </tr>
                            <tr>

                                <td style="text-align: right;">可批复人员:
                                </td>
                                <td>
                                    <input name="LoginReportName" id="LoginReportName" class="easyui-textbox" data-options="prompt:'请输入选择可复批人员'" style="width: 400px;">
                                </td>
                            </tr>
                        </table>

                    </form>
                </div>
            </div>
            <div id="dlg-buttons">
                <div id="isReportContent" style="width: 100%; background: #fafafa; display: flex; justify-content: space-between; padding: 10px 0px;">
                    <div style="width: 68px;">批复:</div>
                    <div>
                        <textarea name="ReportContent" id="ReportContent" rows="3" placeholder="请输入日报批复内容" style="width: 600px; resize: none"></textarea>
                    </div>
                    <div><a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveReportContent()">&nbsp;提&nbsp;交&nbsp;</a></div>
                </div>
                <div id="isbutton">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;
                <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
                </div>
            </div>

        </div>

        <div title="可批复的日报" data-options="iconCls:'icon-page_add'">
            <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%;">
                <table>
                    <tr>
                        <td style="text-align: right;">日报标题:
                        </td>
                        <td style="width: 10%">
                            <input id="ATitle" class="easyui-textbox" data-options="prompt:'请输入日报标题'" style="width: 100px">
                        </td>
                        <td style="text-align: right;">操作时间:
                        </td>
                        <td>
                            <input id="StartDate" class="easyui-datebox" style="width: 100px">
                            ~
                <input id="EndDate" class="easyui-datebox" style="width: 100px">
                        </td>
                    </tr>
                </table>
            </div>
            <table id="dgApproval" class="easyui-datagrid">
            </table>
            <div id="toolbarApproval">
                <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        //新增日报信息
        function addItem() {
            $('#dlg').dialog('open').dialog('setTitle', '新增日报信息');
            $('#fm').form('clear');

            $('#isReportContent').hide();
            $('#isbutton').show();

            $('#LoginReportName').combobox({
                url: 'sysService.aspx?method=QueryUserReportComments', valueField: 'LoginName', textField: 'UserName', multiple: true
            });

            $('#LoginReportName').combobox('textbox').bind('focus', function () { $('#LoginReportName').combobox('showPanel'); });

            let currentDate = new Date();
            let formattedDate = currentDate.toLocaleString('zh-CN', {
                year: 'numeric',
                month: '2-digit',
                day: '2-digit'
            }).replace(/\//g, '-');
            let UserName = '<%=UserInfor.UserName%>';
            let Title = ` ${formattedDate}${UserName}的日报`;
            $('#Title').textbox('setValue', Title);
            // 获取表格元素
            let table = document.getElementById('satable');

            // 清空具有特定类名的行
            let rowsToRemove = table.querySelectorAll('.dynamic-row');
            rowsToRemove.forEach(function (row) {
                row.parentNode.removeChild(row);
            });
        }
        //修改日报信息
        function editItem() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改日报信息');
                $('#fm').form('clear');
                $('#fm').form('load', row);

                $('#LoginReportName').combobox({
                    url: 'sysService.aspx?method=QueryUserReportComments', valueField: 'LoginName', textField: 'UserName', multiple: true
                });
                $("#LoginReportName").combobox('clear');
                if (row.LoginReportName.length != 0) {
                    var arr = row.LoginReportName.split(',');
                    var valueArr = new Array();
                    for (var i = 0; i < arr.length; i++) {
                        valueArr.push(arr[i]);
                    }
                    $("#LoginReportName").combobox("setValues", valueArr);

                }
                //日报内容
                $.ajax({
                    async: false, cache: false, dataType: "json",
                    url: "sysService.aspx?method=QueryDailyReportsByID&ID=" + row.ID,
                    success: function (text) {
                        editor.html(text.Content);
                    }
                });

                //批复列表
                ReportComment(row.ID);

            }
        }
        //修改日报信息
        function editItemByID(Did, IsReportComment) {
            var row = $("#dg").datagrid('getData').rows[Did];
            $('#isReportContent').hide();
            $('#isbutton').show();

            if (IsReportComment == 1) {
                row = $("#dgApproval").datagrid('getData').rows[Did];
                $('#isReportContent').show();
                $('#isbutton').hide();
            }


            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改日报信息');
                $('#fm').form('clear');
                $('#fm').form('load', row);

                //console.log()
                $('#LoginReportName').combobox({
                    url: 'sysService.aspx?method=QueryUserReportComments', valueField: 'LoginName', textField: 'UserName', multiple: true
                });

                $("#LoginReportName").combobox('clear');
                if (row.LoginReportName.length != 0) {
                    var arr = row.LoginReportName.split(',');
                    var valueArr = new Array();
                    for (var i = 0; i < arr.length; i++) {
                        valueArr.push(arr[i]);
                    }
                    $("#LoginReportName").combobox("setValues", valueArr);

                }

                //日报内容
                $.ajax({
                    async: false, cache: false, dataType: "json",
                    url: "sysService.aspx?method=QueryDailyReportsByID&ID=" + row.ID,
                    success: function (text) {
                        editor.html(text.Content);
                    }
                });

                //批复列表
                ReportComment(row.ID);
            }
        }

        //获取批复信息
        function ReportComment(Did) {
            $.ajax({
                url: "sysService.aspx?method=QueryReportComments&ID=" + Did,
                success: function (jsonString) {

                    // 获取表格元素
                    let table = document.getElementById('satable');

                    // 清空具有特定类名的行
                    let rowsToRemove = table.querySelectorAll('.dynamic-row');
                    rowsToRemove.forEach(function (row) {
                        row.parentNode.removeChild(row);
                    });
                    if (jsonString.length != 0) {
                        let text = JSON.parse(jsonString);
                        for (var i = 0; i < text.length; i++) {

                            // 创建新行
                            let newRow = document.createElement('tr');
                            newRow.classList.add('dynamic-row'); // 添加类名以标识这是动态添加的行

                            // 创建第一个单元格（<td>）
                            let newCell1 = document.createElement('td');
                            newCell1.style.textAlign = "right";
                            newCell1.textContent = "批复内容: ";
                            newRow.appendChild(newCell1);

                            // 创建第二个单元格（<td>）
                            let newCell2 = document.createElement('td');
                            newCell2.innerHTML = `
<div style="color: #9C9C9C; font-size: 12px">${text[i].UserName} ${new Date(text[i].OP_DATE).toLocaleString('zh-CN', { hour12: false })}  <a onclick='DelCargoReportCommentsByReportId(${text[i].ID})'">删除</a> </div>
<text>${text[i].Content}<text>
`;

                            newRow.appendChild(newCell2);

                            // 将新行添加到表格
                            table.appendChild(newRow);
                            // 动态绑定点击事件
                            console.log(text[i], "1111111111111")

                            //document.getElementById('deleteLink' + text[i].ID).addEventListener('click', function () {
                            //    DelCargoReportCommentsByReportId(text[i].ID); // 假设 text[i].ReportId 是您需要传递给函数的参数
                            //});
                        }
                    }
                }
            });
        }

        //删除日报信息
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
                        url: 'sysService.aspx?method=DelCargoDailyReports',
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
        //删除批复信息
        function DelCargoReportCommentsByReportId(RDid) {
            let Did = $('#ID').val();
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    $.ajax({
                        url: 'sysService.aspx?method=DelCargoReportCommentsByReportId&ID=' + RDid + '&Report_id=' + Did,
                        success: function (msg) {
                            var result = eval('(' + msg + ')');
                            if (result.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                //批复列表
                                ReportComment(Did)
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', result.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }

        //保存日报信息
        function saveItem() {
            var UPIDText = $('#LoginReportName').combobox('getText')
            $('#UserReportName').val(UPIDText);
            $('#fm').form('submit', {
                url: 'sysService.aspx?method=SaveCargoDailyReports',
                contentType: "application/json;charset=utf-8", dataType: "json",
                onSubmit: function (param) {
                    param.editor = escape(editor.html());
                    var trd = $(this).form('enableValidation').form('validate');
                    if (!trd) { $.messager.progress("close"); }
                    return trd;
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $('#dlg').dialog('close'); 	// close the dialog
                        editor.html('');
                        dosearch();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }

        //提交批复
        function saveReportContent() {
            let reportContent = $('#ReportContent').val();
            let Did = $('#ID').val();
            $.ajax({
                url: "sysService.aspx?method=AddReportComments&ID=" + Did + "&ReportContent=" + reportContent,
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $('#ReportContent').val('');
                        //批复列表
                        ReportComment(Did)
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            });
        }

    </script>

</asp:Content>
