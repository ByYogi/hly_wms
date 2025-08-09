<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="HLYEagle.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>


    <link href="CSS/default.css" rel="stylesheet" />
    <link href="CSS/default1.css" rel="stylesheet" />
    <link rel="shortcut icon" type="image/x-icon" href="CSS/image/dlt.ico" media="screen" />
    <link href="JS/easy/css/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="JS/easy/css/icon.css" rel="stylesheet" type="text/css" />
    <link href="JS/easy/css/default/accordion.css" rel="stylesheet" />


    <script src="JS/easy/js/jquery.min.js"></script>
    <script src="JS/easy/js/jquery.easyui.min.js"></script>

    <style type="text/css">
        .header {
            background: linear-gradient(to bottom, #eff5ff 0px, #e0ecff 100%) repeat-x scroll 0 0 rgba(0, 0, 0, 0);
        }
    </style>
    <script type="text/javascript">
        var _menus = {};
        $(document).ready(function () {
            _menus = <%=res%>;
        });

    </script>
    <script src="JS/easy/js/easyui-lang-zh_CN.js"></script>
    <script src="JS/lefttree.js"></script>
    <script src="JS/Date/ComJs.js"></script>
</head>
<body class="easyui-layout">
    <div data-options="region:'north',border:false" class="header" style="height: 40px;">
        <div style="display: inline; padding-left: 10px; font-size: 25px; font-weight: bolder; font-family: 微软雅黑,黑体,宋体;">
            <%= HLYEagle.Common.GetSystemNameAndVersion()%>
        </div>
        <div style="display: inline; position: absolute; top: 10px; text-align: right; right: 10px; font-size: 14px;">
            <asp:Literal ID="welcome" runat="server"></asp:Literal>
            <a href="#" class="easyui-linkbutton" iconcls="icon-eye" plain="true" onclick="modifyPwd()">&nbsp;修改密码&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-house" plain="true" onclick="sTab()">&nbsp;首&nbsp;页&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-shield" plain="true" onclick="Quit()">&nbsp;退&nbsp;出&nbsp;</a>
        </div>
    </div>
    <div data-options="region:'west',split:true" style="width: 150px;">
        <div id="wnav" class="easyui-accordion" fit="true" border="false">
        </div>
    </div>
    <div data-options="region:'center'" style="width: 100%; height: 100%">
        <div data-options="region:'center'" style="width: 100%; height: 100%">
            <div id="tt" class="easyui-tabs" data-options="fit:true">
                <div title="首页"></div>
            </div>
        </div>

    </div>

</body>
</html>
