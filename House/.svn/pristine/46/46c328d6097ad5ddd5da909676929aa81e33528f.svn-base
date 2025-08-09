<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="taobao.aspx.cs" Inherits="Cargo.Weixin.taobao" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>输入淘宝订单号码</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href=" WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="WeUI/CSS/style.css" rel="stylesheet" />
    <link href=" WeUI/CSS/jquery-weui.css" rel="stylesheet" />
    <link href=" WeUI/CSS/weuix.min.css" rel="stylesheet" />
    <script src="WeUI/JS/fastclick.js"></script>
    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/jquery-weui.js"></script>
    <style type="text/css">
        .commTblStyle_8 th {
            border: 1px solid rgb(205, 205, 205);
            text-align: center;
            color: rgb(255, 255, 255);
            line-height: 25px;
            background-color: rgb(15, 114, 171);
        }

        .commTblStyle_8 tr.BlankRow td {
            line-height: 10px;
        }

        .commTblStyle_8 tr td {
            border: 1px solid rgb(205, 205, 205);
            text-align: center;
            line-height: 20px;
        }

            .commTblStyle_8 tr td.left {
                text-align: right;
                padding-right: 10px;
                font-weight: bold;
                white-space: nowrap;
                background-color: rgb(239, 239, 239);
            }

            .commTblStyle_8 tr td.right {
                text-align: left;
                padding-left: 10px;
            }

        .commTblStyle_8 .whiteback {
            background-color: rgb(255, 255, 255);
        }
    </style>
    <script type="text/javascript">
        function isInteger(obj) {
            return obj % 1 === 0
        }
        $(function () {
            $(document).on("click", "#btnSubmit", function () {
                //alert("功能暂时未完成")
                //return;
                var searchKey = $('#search').val();
                if (searchKey == undefined || searchKey == "") {
                    $.toptip('淘宝订单号有误', 'warning');
                    return;
                }
                if (searchKey.length != 18) {
                    $.toptip('淘宝订单号长度有误', 'warning');
                    return;
                }
                if (!isInteger(searchKey)) {
                    $.toptip('淘宝订单号必须是数字', 'warning');
                    return;
                }
                $.showLoading();
                $.ajax({
                    url: 'myAPI.aspx?method=SaveTaobaoOrder',
                    cache: false, dataType: 'json', data: { key: searchKey },
                    success: function (text) {
                        $.hideLoading();
                        if (text.Result == true) {
                            $.toast("保存成功!");
                            window.location.href = "OnlyMy.aspx";
                        }
                        else {
                            $.toast('保存失败 失败原因：' + text.Message);
                        }
                    }

                });
            });

        });

    </script>
</head>
<body ontouchstart>
    <div>&nbsp;</div>
    <div class="weui-search-bar">
        <input type="number" class="search-input" id='search' pattern="[0-9]*" placeholder='请输入您的淘宝订单号' />
    </div>
    <div>&nbsp;</div>
    <asp:Literal ID="ltlParent" runat="server"></asp:Literal>
    <div class="weui-btn-area">
        <a class="weui-btn weui-btn_primary" id="btnSubmit">订单无误，保存</a>
    </div>
</body>
</html>
