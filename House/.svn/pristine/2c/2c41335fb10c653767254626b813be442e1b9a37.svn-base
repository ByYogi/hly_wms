<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConsumeShop.aspx.cs" Inherits="Cargo.Weixin.ConsumeShop" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>积分兑换</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="WeUI/CSS/jquery-weui.css" rel="stylesheet" />
    <link href="WeUI/CSS/style.css" rel="stylesheet" />
</head>

<body ontouchstart>
    <!--顶部搜索-->
    <!--主体-->
    <div class="wy-content">
        <div class="category-top">
            <header class='weui-header'>
                <div class="weui-search-bar" id="searchBar">
                    <form class="weui-search-bar__form">
                        <div class="weui-search-bar__box">
                            <i class="weui-icon-search"></i>
                            <input type="search" class="weui-search-bar__input" id="searchInput" placeholder="搜索您想要的商品" />
                            <a href="javascript:" class="weui-icon-clear" id="searchClear"></a>
                        </div>
                        <label class="weui-search-bar__label" id="searchText" style="transform-origin: 0px 0px 0px; opacity: 1; transform: scale(1, 1);">
                            <i class="weui-icon-search"></i>
                            <span>搜索您想要的商品</span>
                        </label>
                    </form>
                    <a href="javascript:" class="weui-search-bar__cancel-btn" id="searchCancel">取消</a>
                </div>
            </header>
        </div>
        <div class="wy-Module" style="margin-top: 50px;">
            <div class="wy-Module-con">
                <ul class="wy-pro-list clear" id="ulGift">
                    <%--  <li>
                        <a href="pro_info.html">
                            <div class="proimg">
                                <img src="wxPic/gubo.jpg" />
                            </div>
                            <div class="protxt">
                                <div class="name">优科豪马迪乐泰秒杀菌</div>
                                <div class="wy-pro-pri"><span>3000</span>&nbsp;积分</div>
                            </div>
                        </a>
                    </li>--%>
                </ul>
            </div>
        </div>
    </div>
    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/fastclick.js"></script>
    <script type="text/javascript">
        $(function () {
            var searchKey = $('#searchInput').val();
            //var ProductTypeID = $('#ProductTypeID').val();
            $.showLoading();
            $.ajax({
                url: 'myAPI.aspx?method=QueryOnSaleShelvesData',
                cache: false, dataType: 'json', data: { key: searchKey, SaleType: "4" },
                success: function (text) {
                    $.hideLoading();
                    var s = text;
                    var str = "";
                    for (var j = 0; j < s.length; j++) {
                        if (s[j].SaleType == "1") { continue; }
                        str += "<li><a href='ConsumeInfo.aspx?ID=" + s[j].ID + "&PID=" + s[j].ProductID + "'><div class='proimg'><img src='" + s[j].FileName + "' /></div><div class='protxt'><div class='name' style='font-size:13px;'>" + s[j].Title + "</div><div class='wy-pro-pri' style='float:left;'><span style='font-size:14px;'>" + s[j].Consume + "</span>&nbsp;积分</div><div style='float:right;font-size:14px;color:red;'>积分兑换</div></div></a></li>";
                    }
                    $('#ulGift').html(str);
                }
            });
        })
        $(function () {
            FastClick.attach(document.body);
        });
    </script>
    <script src="WeUI/JS/jquery-weui.js"></script>

</body>
</html>
