<%@ Page Title="购物车" Language="C#" MasterPageFile="~/QY/QY.Master" AutoEventWireup="true" CodeBehind="qyShoppingCart.aspx.cs" Inherits="Cargo.QY.qyShoppingCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/MUI/js/showLoading.js"></script>
    <style type="text/css">
        .shopCart:after, .shopCart .logo:after {
            display: table;
            content: '';
            clear: both;
        }

        .shopCart {
            position: fixed;
            bottom: 0;
            width: 100%;
            height: 50px;
            background: #000000;
            z-index: 1;
        }

            .shopCart div {
                float: left;
            }

            .shopCart .left {
                width: 70%;
                height: 50px;
                background: #fff;
            }

            .shopCart .right {
                width: 30%;
                height: 50px;
                line-height: 50px;
                text-align: center;
                background: #1489EB;
                color: #fff;
            }

            .shopCart .logo {
                position: relative;
                width: 85px;
                height: 64px;
            }

                .shopCart .logo img {
                    position: absolute;
                    top: -15px;
                    left: 15px;
                    width: 70%;
                }

            .shopCart .price {
                line-height: 50px;
                color: #000;
            }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {

            queryShopCommList();//查询超市对应的商品

            mui(".mui-numbox").numbox();//初始化数字输入框
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <header class="mui-bar mui-bar-nav">
        <a class="mui-action-back mui-icon mui-icon-left-nav mui-pull-left"></a>
        <h1 class="mui-title">购物车</h1>
        <span id="delShoppingcart" class="mui-icon mui-icon-trash mui-pull-right" style="color: #fff;"></span>
    </header>
    <div class="mui-content">
        <div id="proList"></div>

        <%--   <ul class="mui-table-view">
            <li class="mui-table-view-cell mui-checkbox mui-left">
                <input name="checkbox" type="checkbox">
                <img class="mui-pull-left" style='height: 62px; width: 73px;' src='../CSS/image/wdbt.png' />
                <div style='width: 3%; min-height: 60px;' class=" mui-pull-left"></div>
                <div style='font-size: 12px;'>225/60R16----AE50----F8440</div>
                <div class="mui-pull-right" style="color: #FD6C24;">
                    <div class="mui-numbox" data-numbox-min='0' style="height: 30px;">
                        <button class="mui-btn mui-btn-numbox-minus" type="button">-</button>
                        <input id="test" class="mui-input-numbox" type="number" value="5" />
                        <button class="mui-btn mui-btn-numbox-plus" type="button">+</button>
                    </div>
                </div>
                <div style='font-size: 12px;'>好来运长沙一号仓--02A区</div>
                <div style='font-size: 12px; font-weight: bold; color: #FD6C24;'>销售价：￥29.9元</div>
            </li>

        </ul>--%>
    </div>
    <div class="shopCart">
        <div class="left">
            <div class="price">
                <div class="mui-input-row mui-checkbox mui-left">
                    <label>全选</label>
                    <input id="checkAll" type="checkbox" style="top: 10px;">
                </div>
                <span>合计:￥</span>
                <span id="total">0</span>
            </div>
        </div>
        <div class="right" id="pay" onclick="goCheck()">去结算</div>
    </div>

    <script type="text/javascript">
        var cart = localStorage.getItem("CART");

        //结算
        function goCheck() {
            window.location.href = "qyOrderSubmit.aspx";
        }

        //查询商品信息
        function queryShopCommList() {
            if (cart == null) { mui.toast('购物车为空'); return; }
            mui.showLoading("查询中..", "div");
            mui.ajax("../QY/qyServices.aspx?method=QueryInHouseProductByID&data=" + cart, {
                async: false, type: 'POST', dataType: 'json', //服务器返回json格式数据
                success: function (obj) {
                    mui.hideLoading();//隐藏后的回调函数  
                    var s = obj;
                    var str = "<ul class=\"mui-table-view\">";
                    for (var j = 0; j < s.length; j++) {
                        str += "<div class=\"mui-table-view-cell mui-checkbox mui-left\">" +
                            "	 ";
                        if (s[j].TypeID == "9") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/ykhm.png'>";
                        } else if (s[j].TypeID == "18") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/plst.png'>";
                        } else if (s[j].TypeID == "27") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/wanli.jpg'>";
                        } else if (s[j].TypeID == "34") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/mapai.jpg'>";
                        } else if (s[j].TypeID == "36") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/mql.png'>";
                        } else if (s[j].TypeID == "66") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/gupo.png'>";
                        } else if (s[j].TypeID == "31") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/hantai.jpg'>";
                        } else if (s[j].TypeID == "171") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/damai.png'>";
                        } else if (s[j].TypeID == "178") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/luji.jpg'>";
                        } else if (s[j].TypeID == "157") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/gitong.jpg'>";
                        } else if (s[j].TypeID == "180") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/wokaitu.png'>";
                        } else if (s[j].TypeID == "345") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/weijing.jpg'>";
                        } else if (s[j].TypeID == "338") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/chifeng.jpg'>";
                        } else if (s[j].TypeID == "344") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/xidatong.png'>";
                        } else if (s[j].TypeID == "98") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/linglong.jpg'>";
                        } else if (s[j].TypeID == "165") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/haoyun.jpg'>";
                        } else if (s[j].TypeID == "149") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/majisi.jpg'>";
                        } else if (s[j].TypeID == "159") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/zhengxin.jpg'>";
                        } else {
                            str += "<div style='width:62px;height:62px;float:left;background-color: #63b4ef;text-align: center;font-size: 18px;color: white;border-radius: 10px; '><span style='line-height: 60px;'>" + s[j].TypeName + "</span></div>";
                           // str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/wdbt.png'>";
                        }
                        str += "  <div style='width: 3%; min-height: 60px;' class=\" mui-pull-left\"></div>" +
                            "  <div style='font-size: 12px;'>" + s[j].Specs + "--" + s[j].Figure + "--" + s[j].Model + "--" + s[j].LoadIndex + s[j].SpeedLevel + "</div>" +
                            "  <div class=\"mui-pull-right\" style=\"color: #FD6C24;\">" +
                            " <div class=\"mui-numbox\" data-numbox-min='0' style=\"height: 30px;\">" +
                            "  <button class=\"mui-btn mui-btn-numbox-minus\" type=\"button\">-</button>" +
                            " <input id=\"test\" class=\"mui-input-numbox\" type=\"number\" value=\"" + s[j].Piece + "\" />" +
                            " <button class=\"mui-btn mui-btn-numbox-plus\" type=\"button\">+</button>" +
                            " </div></div>" +
                            " <div style='font-size: 12px;'>" + s[j].FirstAreaName + "--" + s[j].AreaName + "--" + s[j].BatchYear + "年</div>" +
                            " <div style='font-size: 12px; font-weight: bold; color: #FD6C24;'>销售价：" + s[j].SalePrice + "元</div>" +
                            "<input name=\"checkbox\" data-p=\"" + s[j].SalePrice + "\" value=\"" + s[j].ID + "\" type=\"checkbox\">" +
                            "</div>";
                        // " <div style='font-size: 12px;'>" + s[j].HouseName + "--" + s[j].AreaName + "--" + s[j].Batch + "</div>" +
                    }
                    str += "</ul>";
                    $("#proList").append(str);
                },
                error: function (xhr, type, errorThrown) {
                    mui.toast('缺货');
                    localStorage.removeItem("CART");
                }
            });
        }


        //右上清空购物车
        mui("body").on('tap', '#delShoppingcart', function () {//清空购物车
            var shoppingName = JSON.parse(cart).ID;
            var btnArray = ['取消', '确定'];
            mui.confirm('您确定要清空购物车里的产品？', '', btnArray, function (e) {
                console.log(e.index);//
                if (e.index == 1) {//确定
                    localStorage.removeItem("CART");
                    $("#proList").html("");
                    mui.toast('购物车为空');
                } // else我再想想
            })
        })


        //全选、全不选
        document.getElementById('checkAll').addEventListener('change', function (e) {
            var listBox = $("[name='checkbox']");
            if (e.target.checked) {
                listBox.each(function () {
                    var ele = this;
                    ele.checked = true
                })
                jsTotal();//计算总额

            } else {
                listBox.each(function () {
                    var ele = this;
                    ele.checked = false
                    //ele.removeAttribute('checked');
                })
                $("#total").text("0");
            }
        })

        //数量的变化
        mui("body").on('change', ".mui-input-numbox", function () {
            jsTotal();//算出总额
        })

        //多选框的改变事件
        mui('body').on('change', '[name=checkbox]', function () {
            jsTotal();//算出总额
        });

        //算出总额
        function jsTotal() {
            var listBox = $("[name='checkbox']:checked");
            var total = 0;
            if (listBox.length == 0) {
                $("#total").text(total);
            }
            for (var i = 0; i < listBox.length; i++) {
                var p = listBox.eq(i).attr("data-p");//单价
                var id = listBox.eq(i).val();//产品ID
                var n = listBox.eq(i).parent().find(".mui-input-numbox").val();//数量
                var m = Math.formatFloat(p * n, 2);
                total = Math.formatFloat(total + m, 2);
                upcar(id, n);
            }
            console.log(total);
            $("#total").text(total);
        }

        Math.formatFloat = function (f, digit) {
            var m = Math.pow(10, digit);
            return parseInt(f * m, 10) / m;
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
</asp:Content>
