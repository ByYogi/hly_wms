<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myOrderSubmit.aspx.cs" Inherits="Cargo.Weixin.myOrderSubmit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>提交订单</title>
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
        <div class="wy-header-title">订单详情</div>
    </header>
    <div class="weui-content">
        <!--客户收货地址-->
        <div class="wy-media-box weui-media-box_text address-select">
            <a href="address.aspx">
                <div class="weui-media-box_appmsg">
                    <div class="weui-media-box__hd proinfo-txt-l" style="width: 20px;">
                        <span class="promotion-label-tit">
                            <img src="WeUI/image/icon_nav_city.png" /></span>
                    </div>
                    <div class="weui-media-box__bd" id="clientAddress">
                        点击选择收货地址
                    </div>
                    <div class="weui-media-box__hd proinfo-txt-l" style="width: 16px;">
                        <div class="weui-cell_access"><span class="weui-cell__ft"></span></div>
                    </div>
                </div>
            </a>
        </div>
        <!--客户收货地址-->
        <!--商品信息-->
        <div class="wy-media-box weui-media-box_text">
            <div class="weui-media-box__bd">
                <div id="proList"></div>

                <%--  <div class="weui-media-box_appmsg ord-pro-list">
                    <div class="weui-media-box__hd">
                        <a href="pro_info.html">
                            <img class="weui-media-box__thumb" src="upload/pro3.jpg" alt=""></a>
                    </div>
                    <div class="weui-media-box__bd">
                        <h1 class="weui-media-box__desc"><a href="pro_info.html" class="ord-pro-link">蓝之蓝蓝色瓶装经典Q7浓香型白酒500ml52度高端纯粮食酒2瓶装包邮</a></h1>
                        <p class="weui-media-box__desc">规格：<span>红色</span>，<span>23</span></p>
                        <div class="clear mg-t-10">
                            <div class="wy-pro-pri fl">¥<em class="num font-15">296.00</em></div>
                            <div class="pro-amount fr"><span class="font-13">数量×<em class="name">1</em></span></div>
                        </div>
                    </div>
                </div>--%>
            </div>
        </div>
        <!--商品信息-->
        <div class="weui-panel">
            <div class="weui-panel__bd">
                <div class="weui-media-box weui-media-box_small-appmsg">
                    <div class="weui-cells">

                        <div class="weui-cell weui-cell_access" style="padding: 3px 10px" id="psfs">
                            <div class="weui-cell__bd weui-cell_primary">
                                <p class="font-14"><span class="mg-r-10">配送方式</span><span class="fr" id="kd">快递</span></p>
                            </div>
                        </div>
                        <div class="weui-cell weui-cell_access" style="padding: 3px 10px" id="wlyf">
                            <div class="weui-cell__bd weui-cell_primary">
                                <p class="font-14"><span class="mg-r-10">物流运费</span><span class="fr txt-color-red" id="hyf"></span></p>
                            </div>
                        </div>
                        <div class="weui-cell weui-cell_access" id="dvLogic" style="padding: 3px 10px">
                            <div class="weui-cell__hd weui-cell_primary">
                                <label for="mobile" class="font-14">物流公司</label>
                            </div>
                            <div class="weui-cell__bd">
                                <input class="weui-input" id="LogicName" type="text" value="<%=WxUserInfo.LogicName %>" style="text-align: right; font-size: 14px;" />
                            </div>
                        </div>
                        <div class="weui-cell" style="padding: 3px 10px">
                            <div class="weui-cell__bd">
                                <textarea class="weui-textarea" id="Memo" placeholder="订单备注" rows="2" style="font-size: 14px;"></textarea>
                            </div>
                        </div>

                        <%--    <a class="weui-cell weui-cell_access" href="money.html">
                            <div class="weui-cell__bd weui-cell_primary">
                                <p class="font-14"><span class="mg-r-10">可用蓝豆</span><span class="sitem-tip"><em class="num">1235</em>个</span></p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>
                        <a class="weui-cell weui-cell_access" href="#">
                            <div class="weui-cell__bd weui-cell_primary">
                                <p class="font-14"><span class="mg-r-10">优惠券</span><span class="sitem-tip"><em class="num">0</em>张可用</span></p>
                            </div>
                            <span class="weui-cell__ft"></span>
                        </a>--%>
                    </div>
                </div>
            </div>
        </div>
        <div class="wy-media-box weui-media-box_text">
            <div class="mg10-0 t-c">总金额：<span class="wy-pro-pri mg-tb-5">¥<em class="num font-20" id="zongji">0.00</em></span></div>
            <div class="mg10-0"><a href="javascript:saveOrder();" class="weui-btn weui-btn_primary" id="saveOr">提交订单</a></div>
        </div>
        <input hidden="hidden" id="piece" type="text" value="" />
        <input hidden="hidden" id="HouseID" type="text" value="" />
    </div>
    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/fastclick.js"></script>

    <script src="WeUI/JS/jquery-weui.js"></script>

    <script src="WeUI/JS/logic-picker.js"></script>
    <script type="text/javascript">
        $("#LogicName").logicPicker({
            title: "选择物流公司",
            showDistrict: false,
            onChange: function (picker, values, displayValues) {
                console.log(values, displayValues);
            }
        });
        function saveOrder() {
            var cartG = localStorage.getItem("ORDER");
            var cartjson = JSON.parse(cartG);
            var ZJ = 0, TJ = 0, XG = 0, PLZJ = 0, PLTJ = 0;
            var HouseID = $("#HouseID").val();
            if (HouseID != "9" && HouseID != "12") {
                for (var i = 0; i < cartjson.length; i++) {
                    if (cartjson[i].TJ == "3" && cartjson[i].TYID != "34" && cartjson[i].TYID != "36") {
                        if (Number(cartjson[i].PC) > 10) {
                            $.toast("特价胎单个规格不能超过10条", 'text');
                            return;
                        }
                        //XG += Number(cartjson[i].PC);
                    }
                    if (cartjson[i].TYID == "9") {
                        if (cartjson[i].TJ == "0") {
                            //正价
                            if (cartjson[i].RO == "REP") {
                                ZJ += Number(cartjson[i].PC);
                            }
                        } else if (cartjson[i].TJ == "1") {
                            TJ += Number(cartjson[i].PC);
                        }
                    } else if (cartjson[i].TYID == "18") {
                        //普利司通
                        if (cartjson[i].TJ == "0") {
                            PLZJ += Number(cartjson[i].PC);
                        } else if (cartjson[i].TJ == "1") {
                            PLTJ += Number(cartjson[i].PC);
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
            var clent = localStorage.getItem("CLIENT");
            if (clent == null) {
                $.toast("请选择收货地址"); return;
            }
            var jsoncat = JSON.parse(clent);
            if (jsoncat.length <= 0) {
                $.toast("请选择收货地址"); return;
            }

            if (cart == null) {
                $.toast("请选择商品"); return;
            }
            var jsoncart = JSON.parse(cart);
            if (jsoncart.length <= 0) {
                $.toast("请选择商品"); return;
            }
            var pi = $("#piece").val();
            if (pi == '') {
                $.toast("请选择商品"); return;
            }
            $.showLoading();
            $('#saveOr').removeAttr('href');//去掉a标签中的href属性
            window.location.href = "payOrder.aspx?totalFee=" + $('#zongji').html() + "&piece=" + pi + "&HouseID=" + HouseID + "&LogicName=" + $('#LogicName').val() + "&Memo=" + $('#Memo').val() + "&cart=" + cart + "&address=" + localStorage.getItem("CLIENT");
        }
        $(function () {
            FastClick.attach(document.body);
            $('#wlyf').hide();
            setLocalStor();
            queryShopCommList();//查询超市对应的商品

        });
        var tj = 0;
        //将缓存里的客户数据取出赋值
        function setLocalStor() {
            var clent = localStorage.getItem("CLIENT");
            if (clent != null) {
                var jsoncat = JSON.parse(clent);
                for (var i = 0; i < jsoncat.length; i++) {
                    $("#clientAddress").html("<h4 class='address-name'><span>" + jsoncat[i].Name + "</span><span>" + jsoncat[i].Cellphone + "</span></h4><div class='address-txt'>" + jsoncat[i].Province + "&nbsp;" + jsoncat[i].City + "&nbsp;" + jsoncat[i].Country + "&nbsp;" + jsoncat[i].Address + "</div>");
                }
            }
        }
        var cart = localStorage.getItem("ORDER");
        //查询缓存里添加进购物车里的商品
        function queryShopCommList() {
            if (cart == null) { $.toast("订单为空"); return; }
            var clent = localStorage.getItem("CLIENT");
            $.ajax("myAPI.aspx?method=QueryInHouseProductByID&data=" + cart + "&addr=" + clent, {
                async: false, type: 'POST', dataType: 'json', //服务器返回json格式数据
                timeout: 15000, //15秒超时
                success: function (obj) {
                    var s = obj; var total = 0; var num = 0; var str = ""; var hid = 0;
                    var wlN = 0;
                    for (var j = 0; j < s.length; j++) {
                        if (s[j].Title == undefined || s[j].Title == '') { continue; }
                        str += "<a href='javascript:delPro(" + s[j].OnSaleID + ");' class='wy-dele'></a><div class='weui-media-box_appmsg ord-pro-list' style='margin-top: 0px;padding-top: 0px;'><div class='weui-media-box__hd'><a href='productInfo.aspx?ID=" + s[j].OnSaleID + "'><img class='weui-media-box__thumb' src='" + s[j].FileName + "'></a></div><div class='weui-media-box__bd'><h1 class='weui-media-box__desc'><a href='productInfo.aspx?ID=" + s[j].OnSaleID + "' class='ord-pro-link'>" + s[j].Title + "</a></h1><p class='weui-media-box__desc'>规格：<span>" + s[j].Specs + "</span>，<span>" + s[j].HubDiameter + "寸</span></p><div class='clear mg-t-3'>";

                        if (s[j].HouseID == 12) {
                            str += "<div class='wy-pro-pri fl'>¥<em class='num font-15'>" + s[j].SalePrice + "</em></div>";
                        } else {
                            if (s[j].SaleType == "1") {
                                //str += "<div class='wy-pro-pri fl'>¥<em class='num font-15'>" + s[j].SalePrice + "【天】</em></div>";
                                str += "<div class='wy-pro-pri fl'>¥<em class='num font-15'>" + s[j].SalePrice + "</em></div>";
                            } else if (s[j].SaleType == "3") {
                                str += "<div class='wy-pro-pri fl'>¥<em class='num font-15'>" + s[j].SalePrice + "</em></div>";
                            } else {
                                //str += "<div class='wy-pro-pri fl'>¥<em class='num font-15'>" + s[j].SalePrice + "</em></div>";
                                if (s[j].TypeID == 9) {
                                    str += "<div class='wy-pro-pri fl'>¥<em class='num font-15'>" + s[j].SalePrice + "&nbsp;&nbsp;&nbsp;" + s[j].Assort + "</em></div>";
                                } else {
                                    str += "<div class='wy-pro-pri fl'>¥<em class='num font-15'>" + s[j].SalePrice + "</em></div>";
                                }
                            }
                        }

                        if (s[j].IsModifyPrice == "1") {
                            //表示缺货
                            str += "<div class='pro-amount fr'><span class='font-14'>数量×<em class='name'>" + s[j].Numbers + "</em><em style='color:red;font-weight:bold;'>&nbsp;&nbsp;缺货&nbsp;&nbsp;库存：" + s[j].OnSaleNum + "条</em></span></div></div></div></div>";
                            var m = Math.formatFloat(s[j].SalePrice * s[j].OnSaleNum, 2);
                            num += Number(s[j].Numbers);
                            total = Math.formatFloat(total + m, 2);
                            $('#saveOr').removeAttr('href');//去掉a标签中的href属性
                            wlN += s[j].OnSaleNum;
                        } else {
                            str += "<div class='pro-amount fr'><span class='font-14'>数量×<em class='name'>" + s[j].Numbers + "</em></span></div></div></div></div>";
                            var m = Math.formatFloat(s[j].SalePrice * s[j].Numbers, 2);
                            num += Number(s[j].Numbers);
                            total = Math.formatFloat(total + m, 2);
                            wlN += s[j].Numbers;
                        }
                        if (s[j].DelieryType == "1") { $('#kd').text("2小时内送达 极速配送"); }
                        $("#HouseID").val(s[j].HouseID);
                        hid = s[j].HouseID;
                    }
                    if (hid == 12 || hid == 9) {
                        $('#dvLogic').hide();
                    }
                    else if (hid == 46) {
                        //四川仓库特殊处理
                        $('#dvLogic').hide();
                        $('#psfs').hide();
                        $('#wlyf').show();
                        var logis = "<%=WxUserInfo.LogisID%>";
                        var logisfee = "<%=WxUserInfo.LogisFee%>";
                        if (logis == "34") {
                            $('#hyf').html("￥" + wlN * Number(logisfee));
                            total = Math.formatFloat(total + wlN * Number(logisfee), 2);
                        } else {
                            $('#hyf').html("到付");
                        }
                    }
                    else {
                        $('#dvLogic').show();
                    }
                    $("#proList").append(str);
                    $("#zongji").text(total);
                    $("#piece").val(num);
                },
                error: function (xhr, type, errorThrown) {
                    $.alert("缺货");
                    localStorage.removeItem("ORDER");
                }
            });
    }
    Math.formatFloat = function (f, digit) {
        var m = Math.pow(10, digit);
        return parseInt(f * m, 10) / m;
    }

    function delPro(id) {
        $.confirm("确定删除此商品?", "确认删除?", function () {
            //$.toast(id);
            var json = [];
            var cart = localStorage.getItem("ORDER");
            var cartjson = JSON.parse(cart);
            if (cartjson.length == 1) {
                localStorage.removeItem("ORDER");
                localStorage.removeItem("CART");
                location.reload();
                return;
            }
            for (var i = 0; i < cartjson.length; i++) {
                if (id != cartjson[i].ID) {
                    json.push(cartjson[i]);
                }
            }
            localStorage.removeItem("ORDER");
            localStorage.removeItem("CART");
            localStorage.setItem("ORDER", JSON.stringify(json));
            localStorage.setItem("CART", JSON.stringify(json));

            var cartG = localStorage.getItem("ORDER");
            var cartjson = JSON.parse(cartG);
            var ZJ = 0, TJ = 0; XG = 0, PLZJ = 0, PLTJ = 0;
            for (var i = 0; i < cartjson.length; i++) {
                if (cartjson[i].TJ == "3" && cartjson[i].TYID != "34" && cartjson[i].TYID != "36") {
                    if (Number(cartjson[i].PC) > 10) {
                        $.toast("特价胎单个规格不能超过10条", 'text');
                        return;
                    }
                }
                if (cartjson[i].TYID != "9") {
                    if (cartjson[i].TJ == "0") {
                        //正价
                        if (cartjson[i].RO == "REP") {
                            ZJ += Number(cartjson[i].PC);
                        }
                    } else if (cartjson[i].TJ == "1") {
                        TJ += Number(cartjson[i].PC);
                    }
                } else if (cartjson[j].TYID == "18") {
                    //普利司通
                    if (cartjson[j].TJ == "0") {
                        PLZJ += Number(cartjson[j].PC);
                    } else if (cartjson[j].TJ == "1") {
                        PLTJ += Number(cartjson[j].PC);
                    }
                }
            }

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
                location.reload();
            });
        }
    </script>
</body>
</html>
