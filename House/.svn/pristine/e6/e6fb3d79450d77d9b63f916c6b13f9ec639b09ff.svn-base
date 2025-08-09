<%@ Page Title="订单提交保存" Language="C#" MasterPageFile="~/QY/QY.Master" AutoEventWireup="true" CodeBehind="qyOrderSubmit.aspx.cs" Inherits="Cargo.QY.qyOrderSubmit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../JS/MUI/css/mui.picker.css" rel="stylesheet" />
    <link href="../JS/MUI/css/mui.poppicker.css" rel="stylesheet" />
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

            .shopCart .price {
                line-height: 50px;
                color: #000;
            }

        .mui-btn {
            font-size: 16px;
            padding: 8px;
            margin: 3px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            queryShopCommList();//查询缓存里对应的产品
            setLocalStor();
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <header class="mui-bar mui-bar-nav">
        <a class="mui-action-back mui-icon mui-icon-left-nav mui-pull-left"></a>
        <h1 class="mui-title">订单提交</h1>
    </header>
    <div class="mui-content">

        <ul class="mui-table-view mui-table-view-chevron" style="margin: 0px 0px 3px 0px;">
            <li class="mui-table-view-cell" style="padding: 4px 5px;" onclick="ChoosToAddress()">
                <div class="mui-pull-left" style="min-height: 60px; color: #808080; margin-left: -8px">
                    <span class="mui-icon mui-icon-location" style="font-size: 32px;"></span>
                </div>
                <div style="font-size: 14px; color: #808080;">
                    <span id="addre"></span><%--湖南省长沙市--%>
                </div>
                <div style="font-size: 14px; color: #808080; margin-right: 15px;" class="mui-pull-right"><span id="city"></span><%--益阳--%></div>
                <div style="font-size: 14px; color: #808080;"><span id="boss"></span><%--刘先生--%></div>
                <div style="font-size: 14px; color: #808080;"><span id="phone"></span><%--13565854857--%></div>
            </li>
        </ul>

        <button id='Dest' class="mui-btn mui-btn-block" type='button'>请点击选择到达站</button>
        <button id='LogisticName' class="mui-btn mui-btn-block" type='button'>请点击选择物流公司</button>

        <div id="proList"></div>
        <input type="text" id="LogisID" class="mui-input-clear" style="visibility: hidden">
        <input type="text" id="ADest" class="mui-input-clear" style="visibility: hidden">
    </div>
    <div class="shopCart">
        <div class="left">
            <div class="price">
                <div class="mui-input-row mui-checkbox mui-left">
                    <span>总计:</span>
                    <span id="total">0</span>&nbsp;元
                </div>
            </div>
        </div>
        <div class="right" id="pay" onclick="goCheck()">提交订单</div>
    </div>
    <script src="../JS/MUI/js/mui.picker.js"></script>
    <script src="../JS/MUI/js/mui.poppicker.js"></script>
    <script src="../JS/MUI/js/city.data.js"></script>
    <script type="text/javascript">
        (function ($, doc) {
            $.init();
            $.ready(function () {
                var cityPicker = new $.PopPicker();

                mui.ajax("../QY/qyServices.aspx?method=QueryAllCity", {
                    async: true, type: 'POST', dataType: 'json', //服务器返回json格式数
                    success: function (obj) {
                        cityPicker.setData(obj);
                    }
                });
                var Dest = doc.getElementById('Dest');
                Dest.addEventListener('tap', function (event) {
                    cityPicker.show(function (items) {
                        Dest.textContent = items[0].text;
                        doc.getElementById('ADest').value = items[0].value;
                    });
                }, false);

                var logicPicker = new $.PopPicker();
                mui.ajax("../QY/qyServices.aspx?method=QueryAllLogistic", {
                    async: false, type: 'POST', dataType: 'json', //服务器返回json格式数据
                    success: function (obj) {
                        logicPicker.setData(obj);
                    }
                });

                var LogisticName = doc.getElementById('LogisticName');
                LogisticName.addEventListener('tap', function (event) {
                    logicPicker.show(function (items) {
                        LogisticName.textContent = items[0].text;
                        doc.getElementById('LogisID').value = items[0].value;
                    });
                }, false);
            });
        })(mui, document);
        var cart = localStorage.getItem("CART");

        //将缓存里的客户数据取出赋值
        function setLocalStor() {
            var clent = localStorage.getItem("CLIENT");
            if (clent != null) {
                var jsoncat = JSON.parse(clent);
                for (var i = 0; i < jsoncat.length; i++) {
                    $("#addre").html(jsoncat[i].Address);
                    $("#city").html(jsoncat[i].City);
                    $("#boss").html(jsoncat[i].Boss);
                    $("#phone").html(jsoncat[i].Cellphone);
                }
            }
        }

        //选择收件人
        function ChoosToAddress() {
            window.location.href = "qyClientAddress.aspx";
        }
        //提交订单
        function goCheck() {
            var clent = localStorage.getItem("CLIENT");
            if (cart == null) { mui.toast('购物车为空'); return; }
            if (clent == null) { mui.toast('收货信息为空'); return; }
            mui.showLoading("提交订单中..", "div");
            mui.ajax("../QY/qyServices.aspx?method=WxQySaveOrder&Orderdata=" + cart + "&Clientdata=" + clent + "&logicID=" + $('#LogisID').val() + "&dest=" + $('#ADest').val(), {
                async: true, type: 'POST', dataType: 'json', //服务器返回json格式数据
                success: function (obj) {
                    mui.hideLoading();//隐藏后的回调函数  
                    mui.toast('订单提交成功');
                    localStorage.removeItem("CART");
                    localStorage.removeItem("CLIENT");
                    localStorage.clear();
                    window.location.href = "qyQueryMyOrder.aspx";
                },
                error: function (xhr, type, errorThrown) {
                    mui.toast('订单提交失败');
                    localStorage.clear();
                }
            });
        }
        //查询商品信息
        function queryShopCommList() {
            if (cart == null) { mui.toast('购物车为空'); return; }
            mui.showLoading("查询中..", "div");
            mui.ajax("../QY/qyServices.aspx?method=QueryInHouseProductByID&data=" + cart, {
                async: true, type: 'POST', dataType: 'json', //服务器返回json格式数据
                success: function (obj) {
                    mui.hideLoading();//隐藏后的回调函数  
                    var s = obj;
                    var total = 0;
                    var str = "<ul class=\"mui-table-view mui-table-view-chevron\"  style='margin: 4px 2px 4px 2px;'>";
                    for (var j = 0; j < s.length; j++) {
                        str += "<li class=\"mui-table-view-cell mui-left\">" + "	 ";
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
                        } else if (s[j].TypeID == "165") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/haoyun.jpg'>";
                        } else if (s[j].TypeID == "149") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/majisi.jpg'>";
                        } else if (s[j].TypeID == "159") {
                            str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/zhengxin.jpg'>";
                        } else {
                            str += "<div style='width:62px;height:62px;float:left;background-color: #63b4ef;text-align: center;font-size: 18px;color: white;border-radius: 10px; '><span style='line-height: 60px;'>" + s[j].TypeName + "</span></div>";
                            //str += " <img class=\" mui-pull-left\" style='height: 62px; width: 73px;' src='../CSS/image/wdbt.png'>";
                        }
                        str += "  <div style='width: 3%; min-height: 60px;' class=\" mui-pull-left\"></div>" +
                            "  <div style='font-size: 12px;'>" + s[j].Specs + "--" + s[j].Figure + "--" + s[j].Model + "--" + s[j].LoadIndex + s[j].SpeedLevel + "</div>" +
                            "  <div class=\"mui-pull-right\" style=\"color: #FD6C24;\">" + s[j].Piece + " 条</div>" +
                            " <div style='font-size: 12px;'>" + s[j].FirstAreaName + "--" + s[j].AreaName + "--" + s[j].BatchYear + "年</div>" +
                            " <div style='font-size: 12px;'>销售价：" + s[j].SalePrice + "元</div>" +
                            //"<input name=\"checkbox\" data-p=\"" + s[j].TradePrice + "\" value=\"" + s[j].ID + "\" type=\"checkbox\">" +
                            "</li>";
                        total += Math.formatFloat(s[j].Piece * s[j].SalePrice, 2);
                    }
                    str += "</ul>";
                    $("#proList").html(str);
                    $("#total").html(total);
                },
                error: function (xhr, type, errorThrown) {
                    mui.toast('缺货');
                    localStorage.clear();
                }
            });
        }
        Math.formatFloat = function (f, digit) {
            var m = Math.pow(10, digit);
            return parseInt(f * m, 10) / m;
        }
    </script>
</asp:Content>
