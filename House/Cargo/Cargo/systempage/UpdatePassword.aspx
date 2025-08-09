<%@ Page Title="修改密码" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdatePassword.aspx.cs" Inherits="Cargo.systempage.UpdatePassword" %>

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
        $(document).ready(function () {
            var IsAdmin = "<%= IsAdmin%>";
            if (IsAdmin == "False") {
                $('#LoginName').textbox('readonly');
            }
            else {
                $('#LoginName').textbox('readonly', false);
            }
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id='Loading' style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 98%; height: 100%; background: white; text-align: center; padding: 5px 10px; display: table;">
        <div style="display: table-cell; vertical-align: middle">
            <h1><font size="9">页面加载中……</font></h1>
        </div>
    </div>
    <div style="height: 100%">
        <div id="saPanel" class="easyui-panel" title="修改密码" data-options="iconCls:'icon-eye',collapsible:true,fit:true"
            style="height: 100%;">
            <form id="fm" class="easyui-form" method="post">
            <table>
                <tr>
                    <td style="text-align: right;">
                        用户登陆名:
                    </td>
                    <td>
                        <input id="LoginName" name="LoginName" class="easyui-textbox" data-options="prompt:'请输入用户登陆名',required:true,iconCls:'icon-man'"
                            value="<%=UserInfor.LoginName %>" style="width: 200px; height: 35px; padding: 12px"">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        新密码:
                    </td>
                    <td>
                        <input id="Password" name="Password" class="easyui-textbox" type="password" style="width: 200px;
                            height: 35px; padding: 12px" data-options="iconCls:'icon-lock',required:true">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">
                        确认新密码:
                    </td>
                    <td>
                        <input id="NewPwd" name="NewPwd" class="easyui-textbox" type="password" style="width: 200px;
                            height: 35px; padding: 12px" data-options="iconCls:'icon-lock',required:true">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <a href="#" class="easyui-linkbutton" iconcls="icon-save" plain="false" onclick="saveItem()">
                            &nbsp;确&nbsp;定&nbsp;</a>
                    </td>
                </tr>
            </table>
            </form>
        </div>
    </div>

    <script type="text/javascript">
        //修改密码
        function saveItem() {
            $('#fm').form('submit', {
                url: '../systempage/sysService.aspx?method=UpdateUserPwd',
                onSubmit: function () {
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result == true) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改密码成功，请妥善保存密码!', 'info'); } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改密码失败：' + result.Message, 'warning');
                    }
                }
            })
        }
    </script>

</asp:Content>
