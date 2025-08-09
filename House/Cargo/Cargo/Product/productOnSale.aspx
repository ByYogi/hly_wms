<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="productOnSale.aspx.cs" Inherits="Cargo.Product.productOnSale" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>商品上架</title>
    <link href="../JS/KindEditer/themes/default/default.css" rel="stylesheet" />
    <link href="../JS/KindEditer/plugins/code/prettify.css" rel="stylesheet" />
  <script src="../JS/easy/js/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JS/KindEditer/kindeditor-all.js"></script>
    <script type="text/javascript" src="../JS/KindEditer/lang/zh-CN.js"></script>
    <script src="../JS/KindEditer/plugins/code/prettify.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            KindEditor.ready(function (K) {
                var editor1 = K.create('#editor', {
                    cssPath: '../JS/KindEditer/plugins/code/prettify.css',
                    uploadJson: 'upload.ashx',
                    fileManagerJson: '../asp.net/file_manager_json.ashx',
                    allowFileManager: true,
                    afterCreate: function () {}
                });
                prettyPrint();
            });
        });
    </script>
</head>
<body>
    <form id="example" runat="server">
        <textarea id="editor" cols="100" rows="8" style="width:700px;height:200px;visibility:hidden;" runat="server"></textarea>
        <br />
        <asp:Button ID="Button1" runat="server" Text="提交内容" /> (提交快捷键: Ctrl + Enter)
    </form>
</body>
</html>
