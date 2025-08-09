<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qyModifyPrice.aspx.cs" Inherits="Cargo.QY.qyModifyPrice" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>修改订单价格</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="../Weixin/WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/style.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/jquery-weui.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/weuix.min.css" rel="stylesheet" />
</head>
<body ontouchstart>
    <header class="wy-header" style="position: fixed; top: 0; left: 0; right: 0; z-index: 200;">
        <div class="wy-header-title"><%=wxOrder.OrderNo %></div>
    </header>
    <div class="weui-content" style="padding-left: 0px; padding-right: 0px;">
        <ul class="weui-payrec">
            <div class="weui-pay-m">
                <li class="weui-pay-order">
                    <dl class="weui-pay-line">
                        <dt class="weui-pay-label">订 单 号：</dt>
                        <dd class="weui-pay-e" style="font-weight: bold;"><%=wxOrder.OrderNo %></dd>
                    </dl>
                    <dl class="weui-pay-line">
                        <dt class="weui-pay-label">收 货 人：</dt>
                        <dd class="weui-pay-e" style="font-weight: bold;"><%=wxOrder.Name %> <%=wxOrder.Country %> <%=wxOrder.Cellphone %></dd>
                    </dl>
                    <dl class="weui-pay-line">
                        <dt class="weui-pay-label">订单金额：</dt>
                        <dd class="weui-pay-e" style="font-weight: bold; color: #d81e06;">￥<%=wxOrder.TotalCharge %></dd>
                    </dl>
                </li>
            </div>
        </ul>
        <div class="weui-cells weui-cells_form" style="font-size: 12px;">
            <div class="weui-cell" style="padding-left: 0px; padding-right: 10px; padding-top: 5px; padding-bottom: 5px; color: #e16531;">
                <div class="weui-cell__hd" style="width: 60%">
                    <label class="weui-label" style="width: 100%">&nbsp;&nbsp;&nbsp;&nbsp;轮胎规格花纹</label>
                </div>
                <div class="weui-cell__hd" style="width: 10%">
                    数量
                </div>
                <div class="weui-cell__hd" style="width: 15%">
                    系统价
                </div>
                <div class="weui-cell__bd" style="width: 15%">
                    修改价
                </div>
            </div>
            <asp:Literal ID="ltlGoods" runat="server"></asp:Literal>
            <div class="weui-cell">
                <div class="weui-cell__bd">
                    <textarea class="weui-textarea" id="Reason" placeholder="请输入修改价格的原因" rows="2" onkeyup="textarea(this);"></textarea>
                    <div class="weui-textarea-counter"><span>0</span>/<i>100</i></div>
                </div>
                <i class="weui-icon-clear" onclick="cleararea(this)"></i>
            </div>
        </div>
    </div>
    <a href="javascript:SaveModify();" class="weui-btn bg-blue">提交改价申请</a>
    <div style="margin-top: 10px;">
        <a href="javascript:Close();" id="btnClose" class="weui-btn weui-btn_warn">关闭</a>
    </div>
    <script src="../Weixin/WeUI/JS/jquery-2.1.4.js"></script>
    <script src="../Weixin/WeUI/JS/fastclick.js"></script>
    <script src="../Weixin/WeUI/JS/jquery-weui.js"></script>
    <script type="text/javascript">
        function Close() {
            WeixinJSBridge.call('closeWindow');
        }
        function textarea(input) {
            var content = $(input);
            var max = content.next().find('i').text();
            var value = content.val();
            if (value.length > 0) {

                value = value.replace(/\n|\r/gi, "");
                var len = value.length;
                content.next().find('span').text(len);
                if (len > max) {
                    content.next().addClass('f-red');
                } else {
                    content.next().removeClass('f-red');
                }
            }
        }
        function cleararea(obj) {
            $(obj).prev().find('.weui-textarea').val("");
            return false;
        }
        $(function () {
            FastClick.attach(document.body);
        });
        //提交申请
        function SaveModify() {
            //alert(66);
            var ordn = "<%=wxOrder.OrderNo %>";
            var name = "<%=wxOrder.Name %>";
            var cellphone = "<%=wxOrder.Cellphone %>";
            var MPRICE = localStorage.getItem("MPRICE");
            if (MPRICE == null) { $.toast("请修改后提交"); return; }
            $.showLoading();
            $.ajax("qyServices.aspx?method=SaveModify&data=" + MPRICE + "&Reason=" + $('#Reason').val() + "&OrderNo=" + ordn + "&Name=" + name + "&Cellphone=" + cellphone, {
                async: true, type: 'POST', dataType: 'json', //服务器返回json格式数据
                success: function (text) {
                    $.hideLoading();
                    if (text.Result == true) {
                        $.alert("提交申请成功", "操作提示", function () {
                            WeixinJSBridge.call('closeWindow');
                        });
                    }
                    else {
                        $.alert('提交申请失败：' + text.Message);
                    }
                }
            });

        }
        //添加到缓存
        function bindModify(th) {
            var oid = "<%=wxOrder.ID%>";
            var num = th.dataset["num"];//订单数量
            var price = th.dataset["price"];//原价
            if (localStorage.getItem("MPRICE") != null) {

                var cart = localStorage.getItem("MPRICE");
                var cartjson = JSON.parse(cart);
                var isadd = "0";
                for (var i = 0; i < cartjson.length; i++) {
                    if (oid == cartjson[i].OID && th.id == cartjson[i].SID) {
                        cartjson[i].PRICE = th.value;
                        isadd = "1";
                    }
                }
                if (isadd == "0") {
                    var addjson = [{ OID: "<%=wxOrder.ID%>", SID: th.id, PRICE: th.value, Num: num, OldPrice: price }];
                    cartjson.push(addjson);
                    localStorage.setItem("MPRICE", JSON.stringify(cartjson));
                } else {
                    localStorage.setItem("MPRICE", JSON.stringify(cartjson));
                }
            } else {
                var json = [{ OID: "<%=wxOrder.ID%>", SID: th.id, PRICE: th.value, Num: num, OldPrice: price }];
                localStorage.setItem("MPRICE", JSON.stringify(json));
            }
        }
    </script>
</body>
</html>
