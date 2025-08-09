<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Cargo.Weixin.Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>商品搜索</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1,maximum-scale=1,user-scalable=no" />
    <meta name="apple-mobile-web-app-capable" content="yes" />
    <meta name="apple-mobile-web-app-status-bar-style" content="black" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="../JS/MUI/css/mui.min.css" rel="stylesheet" />
    <link href="WeUI/CSS/weui.min.css" rel="stylesheet" />
    <style type="text/css">
        .wy-Module-tit {
            padding: 8px 10px;
            line-height: 18px;
            position: absolute;
            left: 0;
            top: 0;
        }

            .wy-Module-tit span {
                font-size: 12px;
                font-weight: bold;
                color: #333;
                padding-left: 5px;
                line-height: 18px;
            }

                .wy-Module-tit span:after {
                    position: absolute;
                    left: 0;
                    top: 9px;
                    height: 16px;
                    width: 3px;
                    background: #e21323;
                    content: "";
                }

        .wy-Module-con {
            padding: 35px 10px;
            line-height: 18px;
            position: absolute;
            left: 0;
            top: 0;
        }

        .wy-sperc {
            background-color: white;
            border-radius: 8px;
            font-size: 13px;
            text-align: center;
            margin: 5px;
            padding: 5px;
            height: 30px;
            width: 80px;
            float: left;
        }
    </style>
</head>
<body>
    <div class="weui-search-bar" style="height: 40px;">
        <a href="mall.aspx">
            <img src="WeUI/image/footer01.png" style="width: 25px; height: 25px;margin-bottom:-5px;" /></a>&nbsp;
        <input type="search" style="box-sizing: content-box; width: 97%; height: 25px; display: inline-block; border: 0; -webkit-appearance: none; appearance: none; border-radius: 10px; font-family: inherit; color: #000000; font-size: 13px; font-weight: normal; padding: 0 2px; margin-bottom: 2px; background-color: #fff; border: 1px solid #2dd1e6; opacity: .9;"
            id='searchInput' placeholder='请输入轮胎规格 例：2055516' onkeyup="enterSearch(event)" />&nbsp;
        <img src="WeUI/image/icon-search.png" style="width: 25px; height: 25px;" onclick="search()" />
    </div>
    <div style="margin: 0; position: relative;">
        <div class="wy-Module-tit ">
            <span>热门搜索</span>
        </div>
        <div class="wy-Module-con">
            <a href="category.aspx?ProductTypeID=9&searchText=2055516">
                <div class="wy-sperc">2055516</div>
            </a>
              <a href="category.aspx?ProductTypeID=9&searchText=2156017">
                <div class="wy-sperc">2156017</div>
            </a>
              <a href="category.aspx?ProductTypeID=9&searchText=2056016">
                <div class="wy-sperc">2056016</div>
            </a>
              <a href="category.aspx?ProductTypeID=9&searchText=2155017">
                <div class="wy-sperc">2155017</div>
            </a>
              <a href="category.aspx?ProductTypeID=9&searchText=2256017">
                <div class="wy-sperc">2256017</div>
            </a>
              <a href="category.aspx?ProductTypeID=9&searchText=2256517">
                <div class="wy-sperc">2256517</div>
            </a>
              <a href="category.aspx?ProductTypeID=9&searchText=1657013">
                <div class="wy-sperc">1657013</div>
            </a>

        </div>
    </div>

    <div class="weui-footer weui-footer_fixed-bottom">
        <p class="weui-footer__text">Copyright &copy; 迪乐泰轮胎 客服电话：<a href="tel:13265180164">13265180164</a> </p>
    </div>
    <script src="WeUI/JS/jquery-2.1.4.js"></script>

    <script type="text/javascript">
        function enterSearch(e) {
            if (e.keyCode == 13) {
                window.location.href = "category.aspx?ProductTypeID=9&searchText=" + escape($('#searchInput').val());
            }
        }
        function search() {
            window.location.href = "category.aspx?ProductTypeID=9&searchText=" + escape($('#searchInput').val());
        }
    </script>
</body>
</html>
