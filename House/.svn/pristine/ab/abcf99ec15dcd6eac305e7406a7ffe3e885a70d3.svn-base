<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="shoppingCart.aspx.cs" Inherits="Cargo.Weixin.shoppingCart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>购物车</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="WeUI/CSS/style.css" rel="stylesheet" />
    <link href="WeUI/CSS/jquery-weui.css" rel="stylesheet" />
</head>
<body ontouchstart>
    <!--主体-->
    <header class="wy-header">
        <a href="javascript:history.go(-1)">
            <div class="wy-header-icon-back"><span></span></div>
        </a>
        <div class="wy-header-title">购物车</div>
        <div class="wy-header-right"><a href="javascript:ClearCart()">清空</a></div>
    </header>
    <div class="weui-content">
        <div id="proList"></div>
    </div>
    <!--底部导航-->
    <div class="foot-black"></div>
    <div class="weui-tabbar wy-foot-menu">
        <div class="npd cart-foot-check-item weui-cells_checkbox allselect">
            <label class="weui-cell allsec-well weui-check__label" for="all">
                <div class="weui-cell__hd">
                    <input type="checkbox" class="weui-check" name="all-sec" id="all">
                    <i class="weui-icon-checked"></i>
                </div>
                <div class="weui-cell__bd">
                    <p class="font-14">全选</p>
                </div>
            </label>
        </div>
        <div class="weui-tabbar__item  npd">
            <p class="cart-total-txt">合计：<i>￥</i><em class="num font-16" id="zongji">0.00</em></p>
        </div>
        <a href="javascript:goCheck()" class="red-color npd w-90 t-c">
            <p class="promotion-foot-menu-label">去结算</p>
        </a>
    </div>
    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/jquery-weui.js"></script>
    <script src="WeUI/JS/fastclick.js"></script>
    <script type="text/javascript">
        //去结算
        function goCheck() {
            var zj = Number($("#zongji").html());
            if (zj <= 0) {
                $.toast("请勾选要提交的商品", 'text');
                return;
            }
            var houseID = "<%=WxUserInfo.HouseID%>";
            if (houseID != "9" && houseID != "12") {
                var cartG = localStorage.getItem("CART");
                var cartjson = JSON.parse(cartG);
                var ZJ = 0, TJ = 0; XG = 0, PLZJ = 0, PLTJ = 0;
                var listBox = $("[name='cartpro']:checked");

                for (var i = 0; i < listBox.length; i++) {
                    var id = listBox.eq(i).attr("data-id");//产品ID
                    for (var j = 0; j < cartjson.length; j++) {
                        if (cartjson[i].TJ == "3" && cartjson[i].TYID != "34" && cartjson[i].TYID != "36") {
                            if (Number(cartjson[i].PC) > 10) {
                                $.toast("特价胎单个规格不能超过10条", 'text');
                                return;
                            }
                            //XG += Number(cartjson[i].PC);
                        }
                        if (cartjson[j].TYID == "9") {
                            if (id == cartjson[j].ID) {
                                if (cartjson[j].TJ == "0") {
                                    //正价
                                    if (cartjson[j].RO == "REP") {
                                        ZJ += Number(cartjson[j].PC);
                                    }
                                } else if (cartjson[j].TJ == "1") {
                                    TJ += Number(cartjson[j].PC);
                                }
                            }
                        }
                        else if (cartjson[j].TYID == "18") {
                            //普利司通
                            if (id == cartjson[j].ID) {
                                if (cartjson[j].TJ == "0") {
                                    PLZJ += Number(cartjson[j].PC);
                                } else if (cartjson[j].TJ == "1") {
                                    PLTJ += Number(cartjson[j].PC);
                                }
                            }
                        }
                    }
                }
               <%-- var ym = "<%=TeJiaNum%>";

            if (XG + Number(ym) > 5) {
                $.toast("特价胎当月购买不能超过5条", 'text');
                return;
            }--%>
                var XY = "<%=AllowBuyNum%>";
                if (TJ > ZJ + Number(XY)) {
                    $.toast("优科豪马特价胎数量超过可购买数量" + (ZJ + Number(XY)) + "条", 'text');
                    return;
                }
                var PLXY = "<%=PLAllowBuyNum%>";
                if (PLTJ > PLZJ + Number(PLXY)) {
                    $.toast("普利司通特价胎数量超过可购买数量" + (PLZJ + Number(PLXY)) + "条", 'text');
                    return;
                }
            }
            $.showLoading();
            window.location.href = "myOrderSubmit.aspx";
        }
        $(function () {
            FastClick.attach(document.body);
            queryShopCommList();//查询超市对应的商品

        });
        var cart = localStorage.getItem("CART");
        //查询缓存里添加进购物车里的商品
        function queryShopCommList() {
            if (cart == null) { $.toast("购物车为空"); return; }
            var SaleT = "<%=SaleT%>";
        var clent = localStorage.getItem("CLIENT");
        $.ajax("myAPI.aspx?method=QueryInHouseProductByID&data=" + cart + "&SaleT=" + SaleT, {
            async: false, type: 'POST', dataType: 'json', //服务器返回json格式数据
            timeout: 15000, //15秒超时
            success: function (obj) {
                var s = obj;
                var str = "";
                for (var j = 0; j < s.length; j++) {
                    if (s[j].Title == undefined || s[j].Title == '') {
                        continue;
                    }
                    str += "<div class='weui-panel weui-panel_access' style='margin-top: 0px;'><div class='weui-panel__hd' style='padding-top: 2px; padding-bottom: 2px;'><span><%--广州迪乐泰贸易公司--%>&nbsp;</span><a href='javascript:delPro(" + s[j].OnSaleID + ");' class='wy-dele'></a></div><div class='weui-panel__bd'><div class='weui-media-box_appmsg pd-3'><div class='weui-media-box__hd check-w weui-cells_checkbox'><label class='weui-check__label' for='cart-pto" + s[j].OnSaleID + "'><div class='weui-cell__hd cat-check'><input type='checkbox' class='weui-check' name='cartpro' id='cart-pto" + s[j].OnSaleID + "' data-TypeID='" + s[j].TypeID + "' data-p='" + s[j].SalePrice + "' data-id='" + s[j].OnSaleID + "' data-ty='" + s[j].SaleType + "' data-num='" + s[j].OnSaleNum + "' /><i class='weui-icon-checked'></i></div></label></div><div class='weui-media-box__hd'><a href='productInfo.aspx?ID=" + s[j].OnSaleID + "'><img class='weui-media-box__thumb' src='" + s[j].FileName + "' alt='' /></a></div><div class='weui-media-box__bd'><h1 class='weui-media-box__desc'><a href='productInfo.aspx?ID=" + s[j].OnSaleID + "' class='ord-pro-link'>" + s[j].Title + "</a></h1><p class='weui-media-box__desc'>规格：<span>" + s[j].Specs + "</span>,<span>" + s[j].HubDiameter + "寸</span>,<span>" + s[j].BatchYear + "年</span></p><div class='clear mg-t-3'>";
                    if (s[j].HouseID == 12) {
                        str += "<div class='wy-pro-pri fl'>¥<em class='num font-15'>" + s[j].SalePrice + "</em></div>";
                    } else {
                        if (s[j].SaleType == "1") {
                            str += "<div class='wy-pro-pri fl'>¥<em class='num font-15'>" + s[j].SalePrice + "</em></div>";
                        } else if (s[j].SaleType == "3") {
                            str += "<div class='wy-pro-pri fl'>¥<em class='num font-15'>" + s[j].SalePrice + "</em></div>";
                        } else {
                            if (s[j].TypeID == 9) {
                                str += "<div class='wy-pro-pri fl'>¥<em class='num font-15'>" + s[j].SalePrice + "&nbsp;&nbsp;&nbsp;" + s[j].Assort + "</em></div>";
                            } else {
                                str += "<div class='wy-pro-pri fl'>¥<em class='num font-15'>" + s[j].SalePrice + "</em></div>";
                            }

                        }
                    }
                    str += "<div class='pro-amount fr'><div class='Spinner' id=" + s[j].OnSaleID + "></div></div></div></div></div></div></div>";
                }
                $("#proList").append(str);
                var jsoncat = JSON.parse(localStorage.getItem("CART"));
                for (var j = 0; j < s.length; j++) {
                    for (var i = 0; i < jsoncat.length; i++) {
                        if (s[j].OnSaleID == jsoncat[i].ID) {
                            $("#" + s[j].OnSaleID).Spinner({ value: jsoncat[i].PC, len: 3, max: 999 });
                        }
                    }
                }
            },
            error: function (xhr, type, errorThrown) {
                $.toast("缺货");
                localStorage.removeItem("CART");
            }
        });
    }
    </script>

    <script src="WeUI/JS/jquery.Spinner.js"></script>
    <!---全选按钮-->

    <script type="text/javascript">
        //清空购物车
        function ClearCart() {
            $.confirm("您确定要清空购物车吗?", "清空购物车", function () {
                localStorage.removeItem("CART");
                location.reload();
            });
        }
        function delPro(id) {
            $.confirm("您确定要把此商品从购物车删除吗?", "确认删除?", function () {
                //$.toast(id);
                var json = [];
                var cart = localStorage.getItem("CART");
                var cartjson = JSON.parse(cart);
                if (cartjson.length == 1) {
                    localStorage.removeItem("CART"); location.reload();
                    return;
                }
                for (var i = 0; i < cartjson.length; i++) {
                    if (id != cartjson[i].ID) {
                        json.push(cartjson[i]);
                    }
                }
                localStorage.removeItem("CART");
                localStorage.setItem("CART", JSON.stringify(json));
                location.reload();
            }, function () {
                //取消操作
            });
        }
        $(document).on("click", ".Increase", function () {
            $("input[id=cart-pto" + this.parentElement.id + "]").prop("checked", true);
            jsTotal();
        });
        $(document).on("click", ".DisDe", function () {
            $("input[id=cart-pto" + this.parentElement.id + "]").prop("checked", true);
            jsTotal();
        });
        $(document).on("click", ".Decrease", function () {
            $("input[id=cart-pto" + this.parentElement.id + "]").prop("checked", true);
            jsTotal();
        });

        //多选框的改变事件
        $(document).on('change', '[name=cartpro]', function () {
            jsTotal();//算出总额
        });
        $(document).ready(function () {
            $(".allselect").click(function () {
                if ($(this).find("input[name=all-sec]").prop("checked")) {
                    $("input[name=cartpro]").each(function () {
                        $(this).prop("checked", true);
                    });
                }
                else {
                    $("input[name=cartpro]").each(function () {
                        if ($(this).prop("checked")) {
                            $(this).prop("checked", false);
                        } else {
                            $(this).prop("checked", true);
                        }
                    });
                }
                jsTotal();//计算总额
            });
        });
        //算出总额
        function jsTotal() {
            var listBox = $("[name='cartpro']:checked");
            var total = 0;
            if (listBox.length == 0) {
                $("#total").text(total);
            }
            for (var i = 0; i < listBox.length; i++) {
                var p = listBox.eq(i).attr("data-p");//单价
                var id = listBox.eq(i).attr("data-id");//产品ID
                var n = listBox.eq(i).parent().parent().parent().parent().find(".Amount").val();//数量
                var tnum = listBox.eq(i).attr("data-num");//库存数量
                var ty = listBox.eq(i).attr("data-ty");//分类1特价2全网3特价
                var TypeID = listBox.eq(i).attr("data-TypeID");//分类1特价2全网3特价
                if (ty == "3" && TypeID != '34' && TypeID != '36') {
                    if (Number(n) > 10) {
                        $.toast("特价胎限购10条", 'text');
                        return;
                    }
                    $.ajax({
                        url: 'myAPI.aspx?method=QueryPurchaseNum',
                        type: 'post', dataType: 'text', data: { ID: id, Num: Number(n) }, sysnc: false,
                        success: function (text) {
                            var z = Number(text);
                            if (Number(z) > 10) {
                                $.toast("特价胎单个规格限购10条，您已购买了" + text + "条", 'text');
                                $("#zongji").text(0);
                                return;
                            }
                        }
                    });
                }
                if (Number(n) > Number(tnum)) {
                    //库存不足
                    $.toast("库存不足，库存数：" + tnum + "条", 'text');
                    return;
                }
                var m = Math.formatFloat(p * n, 2);
                total = Math.formatFloat(total + m, 2);
                upcar(id, n);
            }
            setOrderStorage(listBox);
            console.log(total);
            $("#zongji").text(total);
        }
        Math.formatFloat = function (f, digit) {
            var m = Math.pow(10, digit);
            return parseInt(f * m, 10) / m;
        }
        //设置要提交的订单缓存
        function setOrderStorage(listBox) {
            localStorage.removeItem("ORDER");
            var cartG = localStorage.getItem("CART");
            var cartjsonJ = JSON.parse(cartG);
            var cartjson = [];
            for (var i = 0; i < listBox.length; i++) {
                var id = listBox.eq(i).attr("data-id");//产品ID
                for (var j = 0; j < cartjsonJ.length; j++) {
                    if (id == cartjsonJ[j].ID) {
                        cartjson.push(cartjsonJ[j]);
                    }
                }
            }
            localStorage.setItem("ORDER", JSON.stringify(cartjson));
        }
        //修改缓存的件数
        function upcar(id, pc) {
            var jsoncat = JSON.parse(localStorage.getItem("CART"));
            for (var i = 0; i < jsoncat.length; i++) {
                if (id == jsoncat[i].ID) {
                    jsoncat[i].PC = pc;
                }
            }
            localStorage.setItem("CART", JSON.stringify(jsoncat));
        }
    </script>
</body>
</html>
