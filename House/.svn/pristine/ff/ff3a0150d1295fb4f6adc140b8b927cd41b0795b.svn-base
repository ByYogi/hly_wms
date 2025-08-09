<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="carTyreMatch.aspx.cs" Inherits="Cargo.Weixin.carTyreMatch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>车型匹配查询</title>
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
        $(function () {
            $(document).on("click", "#btnCar", function () {
                $.showLoading();
                var searchKey = $('#search').val();
                $.ajax({
                    url: 'myAPI.aspx?method=QueryCarTyreMatch',
                    cache: false, dataType: 'json', data: { key: searchKey },
                    success: function (text) {
                        $.hideLoading();
                        var s = text;
                        var str = "<table class='commTblStyle_8' border='0' width='100%' style='border-spacing:0;border-collapse:collapse;font-size:14px;'><tbody><tr><th>车型</th><th>规格花纹</th><th>轮胎品牌</th></tr>";
                        for (var j = 0; j < s.length; j++) {
                            str += "<tr><td>" + s[j].Car + "</td><td>" + s[j].Spec + "</td><td>" + s[j].Brade + "</td></tr>";
                        }
                        str += "</tbody><table>";
                        $('#divStock').html(str);
                    }
                });
            });

        });

    </script>
</head>
<body ontouchstart>
    <div class="weui-search-bar">
        <input type="search" class="search-input" id='search' placeholder='请输入您的爱车品牌' />
        <button class="weui-btn weui-btn_mini weui-btn_primary" id="btnCar"><i class="icon icon-4"></i></button>
    </div>
    <div id="divStock"></div>
  <%--  <div class="weui-footer">
        <p class="weui-footer__links">
            <a href="mall.aspx" class="weui-footer__link">迪乐泰轮胎商城</a>
        </p>
        <p class="weui-footer__text">Copyright &copy; 迪乐泰轮胎</p>
    </div>--%>
     <div class="weui-footer weui-footer_fixed-bottom">
        <p class="weui-footer__text">Copyright &copy; 迪乐泰轮胎 客服电话：<a href="tel:13265180164">13265180164</a> </p>
    </div>

</body>
</html>
