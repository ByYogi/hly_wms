<%@ Page Title="预订单管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReserveOrderManager.aspx.cs" Inherits="Cargo.Order.ReserveOrderManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/Rotate/jQueryRotate.2.2.js" type="text/javascript"></script>
    <script src="../JS/easy/js/datagrid-groupview.js" type="text/javascript"></script>
    <style type="text/css">
        .commTblStyle_8 th {
            border: 1px solid rgb(205, 205, 205);
            text-align: center;
            color: rgb(255, 255, 255);
            line-height: 28px;
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

        /*流程图样式*/
        .processBar {
            float: left;
            width: 100px;
            margin-top: 15px;
        }

            .processBar .bar {
                background: rgb(230, 224, 236);
                height: 3px;
                position: relative;
                width: 100px;
                margin-left: 10px;
            }

            .processBar .b-select {
                background: rgb(96, 72, 124);
            }

            .processBar .bar .c-step {
                position: absolute;
                width: 8px;
                height: 8px;
                border-radius: 50%;
                background: rgb(230, 224, 236);
                left: -12px;
                top: 50%;
                margin-top: -4px;
            }

            .processBar .bar .c-select {
                width: 10px;
                height: 10px;
                margin: -5px -1px;
                background: rgb(96, 72, 124);
            }

        .main-hide {
            position: absolute;
            top: -9999px;
            left: -9999px;
        }

        .poetry {
            color: rgb(41, 41, 41);
            font-family: KaiTi_GB2312, KaiTi, STKaiti;
            font-size: 16px;
            background-color: transparent;
            font-weight: bold;
            font-style: normal;
            text-decoration: none;
        }

        button {
            width: 80px;
            line-height: 30px;
            font-size: 11px;
            color: rgb(116, 42, 149);
            text-align: center;
            border-radius: 6px;
            border: 1px solid #e2e2e2;
            cursor: pointer;
            background-color: #fff;
            outline: none;
        }

            button:hover {
                border: 1px solid rgb(179, 161, 200);
            }

        #saPanel2 table td {
            border-color: #ccc;
            border-style: dotted;
            border-width: 0 1px 1px 0;
        }

        #saPanel3 table td {
            border-color: #ccc;
            border-style: dotted;
            border-width: 0 1px 1px 0;
        }

        input[name="AStock"].easyui-checkbox {
            height: 20px;
            width: 20px;
        }
    </style>
    <script src="../JS/easy/js/datagrid-groupview.js" type="text/javascript"></script>
    <%--<script src="../JS/Date/CheckActivX.js" type="text/javascript"></script>--%>
    <script src="../JS/Lodop/LodopFuncs.js" type="text/javascript"></script>
    <script charset="utf-8" src="https://map.qq.com/api/gljs?v=1.exp&key=OB4BZ-D4W3U-B7VVO-4PJWW-6TKDJ-WPB77"></script>
    <script type="text/javascript">
        //页面加载显示遮罩层
        var pc;
        var LogiName =<%=UserInfor.LoginName%>;

        var IsQueryLockStock = null;
        $.ajax({
            url: "../Product/productApi.aspx?method=IsLockWarehouse",
            async: false,
            data: { LogiName: LogiName },
            type: "post",
            success: function (data) {
                var obj;
                if ((typeof data == 'object') && data.constructor == Object) {
                    obj = data;
                } else {
                    obj = eval("(" + data + ")");
                    //alert(obj)
                }
                IsQueryLockStock = obj.userEnt.IsQueryLockStock
                //console.log(obj)
            }
        })


        function closes() {
            $("#Loading").fadeOut("normal", function () {
                $(this).remove();
            });
        }
        $.parser.onComplete = function () {
            if (pc) {
                clearTimeout(pc);
            }
            pc = setTimeout(closes, 10);
        }
        setTimeout(function () {
            var saPanel = document.getElementById('saPanel');
            var bodyWidth = document.body.clientWidth;
            // 输出宽度
            $('#saPanel').width(bodyWidth);
            $('#saPanel').parent('div').css('width', bodyWidth + 'px');

            var saPanel2 = document.getElementById('saPanel2');
            // 输出宽度
            $('#saPanel2').width(bodyWidth);
            $('#saPanel2').parent('div').css('width', bodyWidth + 'px');

            var saPanel3 = document.getElementById('saPanel3');
            // 输出宽度
            $('#saPanel3').width(bodyWidth);
            $('#saPanel3').parent('div').css('width', bodyWidth + 'px');
        }, 100);

        window.onload = function () {

            adjustment();
            $.ajaxSetup({ async: true });
            $.getJSON("../Client/clientApi.aspx?method=QueryAllUpClientDep", function (data) {
                UpClientDep = data;
            });
            var url = '../Client/clientApi.aspx?method=QueryAllUpClientDep&type=1&houseID=<%=UserInfor.HouseID%>';
            $('#ABelongDepart').combobox('reload', url);
            $('#ABelongDepart').combobox('select', '-1');
            $('#PeriodsNum').numberbox({
                onChange: function (newValue, oldValue) {
                    if (newValue != null && newValue != "") {
                        var PeriodsNum = $('#PeriodsNum').val();
                        if (oldValue != null && oldValue != "") {
                            PeriodsNum = oldValue;
                        }
                        var Remark = $('#ARemark').val();
                        if (Remark != null && Remark != undefined) {
                            Remark = Remark.replace("周期" + PeriodsNum + "天，", "");
                            $('#ARemark').val('周期' + newValue + '天，' + Remark);
                        } else {
                            $('#ARemark').val('周期' + newValue + '天');
                        }
                    }
                }
            });

            $("#ARemark").focus(function () {
                $('#ARemark').val($('#HiddenRemark').val());
            });
            $("#ARemark").blur(function () {
                $('#HiddenRemark').val($('#ARemark').val());
                if ($("#CheckOutType").combobox('getValue') == "1" && $('#PeriodsNum').val() != null && $('#PeriodsNum').val() != "") {
                    $('#ARemark').val('周期' + $('#PeriodsNum').val() + '天，' + $('#HiddenRemark').val());
                }
            });
            var HID = "<%=UserInfor.HouseID%>";
            $("[name='qDep']").hide();
        }
        $(window).resize(function () {
            console.log(1)
            adjustment();
        });
        function adjustment() {
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true);
            $('#dg').datagrid({ height: (height * 0.58) });
            $('#dg2').datagrid({ height: (height * 0.4) });
            $('#dg3').datagrid({ height: (height * 0.4) });
            $('#dg4').datagrid({ height: (height * 0.4) });
            $('#dg5').datagrid({ height: (height * 0.4) });
        }
        var RoleCName = "<%=UserInfor.RoleCName%>";
        var HouseName = "<%=UserInfor.HouseName%>";
        $(document).ready(function () {
            var HID = '<%=UserInfor.HouseID%>';
            $('#HiddenHouseID').val(HID);
            var columns = [];
            if (HID == "62") {
                $("td.Dep").hide();
                $("td.LogisAwbNo").hide();
                $("td.ThrowGood").hide();
                $("td.AcceptPeople").hide();
                $("td.Dest").hide();
                $("td.PID").hide();
                $("td.AOutCargoType").hide();
                $("td.AcceptUnit").hide();
                $("td.ACreateAwb").hide();
                $("td.outDg").hide();
                $("#postpone").hide();
                $("#btnExportOrder").hide();
                $("#btnMass").hide();
                $("#btnTryeCode").hide();
                $("#btnApprove").hide();
                //$("#btnTag").hide();
                $("#barCode").hide();
                $("#btnOrderStatus").hide();
                $("#btnLogisNo").hide();
                columns.push({
                    title: '数量', field: 'Piece', width: '55px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });

                columns.push({
                    title: '收货人', field: 'AcceptPeople', width: '125px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '收货地址', field: 'AcceptAddress', width: '250px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '订单状态', field: 'AwbStatus', width: '60px',
                    formatter: function (val, row, index) {
                        if (val == "0") { return "<span title='已下单'>已下单</span>"; }
                        else if (val == "1") { return "<span title='出库中'>出库中</span>"; }
                        else if (val == "2") { return "<span title='已出库'>已出库</span>"; }
                        else if (val == "3") { return "<span title='已发车'>已发车</span>"; }
                        else if (val == "4") { return "<span title='配送中'>配送中</span>"; }
                        else if (val == "5") { return "<span title='已签收'>已签收</span>"; }
                        else if (val == "6") { return "<span title='已拣货'>已拣货</span>"; }
                        else if (val == "7") { return "<span title='配送中'>配送中</span>"; }
                        else if (val == "8") { return "<span title='已接单'>已接单</span>"; }
                        else { return ""; }
                    }
                });

            }
            else {
                if (RoleCName.indexOf("安泰路斯") >= 0) {
                    //columns.push({
                    //    title: '出发站', field: 'Dep', width: '50px', formatter: function (value) {
                    //        return "<span title='" + value + "'>" + value + "</span>";
                    //    }
                    //});
                    columns.push({
                        title: '公司名称', field: 'AcceptUnit', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '收货人', field: 'AcceptPeople', width: '55px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '收货地址', field: 'AcceptAddress', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '开单员ID', field: 'CreateAwbID', width: '60px', hidden: 'true', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '订单状态', field: 'AwbStatus', width: '60px',
                        formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='已下单'>已下单</span>"; }
                            else if (val == "1") { return "<span title='出库中'>出库中</span>"; }
                            else if (val == "2") { return "<span title='已出库'>已出库</span>"; }
                            else if (val == "3") { return "<span title='已发车'>已发车</span>"; }
                            else if (val == "4") { return "<span title='配送中'>配送中</span>"; }
                            else if (val == "5") { return "<span title='已签收'>已签收</span>"; }
                            else if (val == "6") { return "<span title='已拣货'>已拣货</span>"; }
                            else if (val == "7") { return "<span title='配送中'>配送中</span>"; }
                            else if (val == "8") { return "<span title='已接单'>已接单</span>"; }
                            else { return ""; }
                        }
                    });
                    columns.push({ title: '开单时间', field: 'CreateDate', width: '125px', formatter: DateTimeFormatter });
                    columns.push({
                        title: '物流公司单号', field: 'LogisAwbNo', width: '90px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '订单类型', field: 'ThrowGood', width: '60px',
                        formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='客户单'>客户单</span>"; }
                            else if (val == "1") { return "<span title='抛货单'>抛货单</span>"; }
                            //else if (val == "2") { return "<span title='调货单'>调货单</span>"; }
                            //else if (val == "3") { return "<span title='代发单'>代发单</span>"; }
                            //else if (val == "4") { return "<span title='周期胎'>周期胎</span>"; }
                            //else if (val == "5") { return "<span title='长和订单'>长和订单</span>"; }
                            //else if (val == "6") { return "<span title='商贸订单'>商贸订单</span>"; }
                            //else if (val == "10") { return "<span title='祺航订单'>祺航订单</span>"; }
                            //else if (val == "7") { return "<span title='其它订单'>其它订单</span>"; }
                            else if (val == "8") { return "<span title='理赔单'>理赔单</span>"; }
                            //else if (val == "9") { return "<span title='移库单'>移库单</span>"; }
                            //else if (val == "11") { return "<span title='展示单'>展示单</span>"; }
                            else if (val == "12") { return "<span title='OES客户单'>OES客户单</span>"; }
                            else if (val == "13") { return "<span title='二批单 '>二批单</span>"; }
                            //else if (val == "14") { return "<span title='报量单'>报量单</span>"; }
                            //else if (val == "15") { return "<span title='速配单'>速配单</span>"; }
                            else if (val == "16") { return "<span title='促销单'>促销单</span>"; }
                            else if (val == "17") { return "<span title='急送单'>急送单</span>"; }
                            //else if (val == "18") { return "<span title='异地单'>异地单</span>"; }
                            //else if (val == "19") { return "<span title='来货单'>来货单</span>"; }
                            else if (val == "20") { return "<span title='电商单'>电商单</span>"; }
                            //else if (val == "21") { return "<span title='订货单'>订货单</span>"; }
                            else if (val == "22") { return "<span title='极速达'>极速达</span>"; }
                            else if (val == "23") { return "<span title='次日达'>次日达</span>"; }
                            else if (val == "24") { return "<span title='渠道单'>渠道单</span>"; }
                            else if (val == "25") { return "<span title='退仓单'>退仓单</span>"; }
                            else { return ""; }
                        }
                    });

                    columns.push({ title: '出库时间', field: 'OutCargoTime', width: '125px', formatter: DateTimeFormatter });
                    columns.push({
                        title: '出库延期', field: 'DelayTime', width: '60px', formatter: function (val) {
                            if (val != null && val != "") {
                                return "<span title='" + val + "'>" + val + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '物流配送费用', field: 'DeliveryFee', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '物流公司名称', field: 'LogisticName', width: '90px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '下单方式', field: 'OrderType', width: '60px', formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='电脑单'>电脑单</span>"; }
                            else if (val == "1") { return "<span title='企业微信单'>企业微信单</span>"; }
                            else if (val == "2") { return "<span title='微信商城单'>微信商城单</span>"; }
                            else if (val == "3") { return "<span title='APP单'>APP单</span>"; }
                            else if (val == "4") { return "<span title='小程序单'>小程序单</span>"; }
                            else { return "<span title='电脑单'>电脑单</span>"; }
                        }
                    });
                    columns.push({ title: '公司类型', field: 'BelongHouse', hidden: true });
                } else {
                    //columns.push({
                    //    title: '出发站', field: 'Dep', width: '50px', formatter: function (value) {
                    //        return "<span title='" + value + "'>" + value + "</span>";
                    //    }
                    //});
                    columns.push({
                        title: '公司名称', field: 'AcceptUnit', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '收货人', field: 'AcceptPeople', width: '55px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '收货地址', field: 'AcceptAddress', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    //columns.push({
                    //    title: '开单员ID', field: 'CreateAwbID', width: '60px', hidden: 'true', formatter: function (value) {
                    //        return "<span title='" + value + "'>" + value + "</span>";
                    //    }
                    //});
                    columns.push({
                        title: '订单状态', field: 'AwbStatus', width: '60px',
                        formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='已下单'>已下单</span>"; }
                            else if (val == "1") { return "<span title='出库中'>出库中</span>"; }
                            else if (val == "2") { return "<span title='已出库'>已出库</span>"; }
                            else if (val == "3") { return "<span title='已发车'>已发车</span>"; }
                            else if (val == "4") { return "<span title='配送中'>配送中</span>"; }
                            else if (val == "5") { return "<span title='已签收'>已签收</span>"; }
                            else if (val == "6") { return "<span title='已拣货'>已拣货</span>"; }
                            else if (val == "7") { return "<span title='配送中'>配送中</span>"; }
                            else if (val == "8") { return "<span title='已接单'>已接单</span>"; }
                            else { return ""; }
                        }
                    });
                    //columns.push({
                    //    title: '拣货计划', field: 'PickStatus', width: '60px',
                    //    formatter: function (val, row, index) {
                    //        if (val == "0") { return "<span title='未生成'>未生成</span>"; }
                    //        else if (val == "1") { return "<span title='已生成'>已生成</span>"; }
                    //        else { return ""; }
                    //    }
                    //});
                    columns.push({ title: '开单时间', field: 'CreateDate', width: '125px', formatter: DateTimeFormatter });
                    if (HID != 64) {
                        columns.push({
                            title: '物流单号', field: 'LogisAwbNo', width: '80px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        });
                        columns.push({
                            title: '送货方式', field: 'DeliveryType', width: '80px', formatter: function (val, row, index) {
                                if (val == "0") { return "<span title='急送'>急送</span>"; }
                                else if (val == "1") { return "<span title='自提'>自提</span>"; }
                                else if (val == "2") { return "<span title='普送'>普送</span>"; }
                                else { return ""; }
                            }
                        });

                        columns.push({
                            title: '订单类型', field: 'ThrowGood', width: '60px',
                            formatter: function (val, row, index) {
                                if (val == "0") { return "<span title='客户单'>客户单</span>"; }
                                else if (val == "1") { return "<span title='抛货单'>抛货单</span>"; }
                                //else if (val == "2") { return "<span title='调货单'>调货单</span>"; }
                                //else if (val == "3") { return "<span title='代发单'>代发单</span>"; }
                                // else if (val == "4") { return "<span title='周期胎'>周期胎</span>"; }
                                //else if (val == "5") { return "<span title='长和订单'>长和订单</span>"; }
                                //else if (val == "6") { return "<span title='商贸订单'>商贸订单</span>"; }
                                //else if (val == "10") { return "<span title='祺航订单'>祺航订单</span>"; }
                                //else if (val == "7") { return "<span title='其它订单'>其它订单</span>"; }
                                else if (val == "8") { return "<span title='理赔单'>理赔单</span>"; }
                                //else if (val == "9") { return "<span title='移库单'>移库单</span>"; }
                                //else if (val == "11") { return "<span title='展示单'>展示单</span>"; }
                                else if (val == "12") { return "<span title='OES客户单'>OES客户单</span>"; }
                                else if (val == "13") { return "<span title='二批单 '>二批单</span>"; }
                                //else if (val == "14") { return "<span title='报量单'>报量单</span>"; }
                                //else if (val == "15") { return "<span title='速配单'>速配单</span>"; }
                                else if (val == "16") { return "<span title='促销单'>促销单</span>"; }
                                else if (val == "17") { return "<span title='急送单'>急送单</span>"; }
                                //else if (val == "18") { return "<span title='异地单'>异地单</span>"; }
                                //else if (val == "19") { return "<span title='来货单'>来货单</span>"; }
                                else if (val == "20") { return "<span title='电商单'>电商单</span>"; }
                                //else if (val == "21") { return "<span title='订货单'>订货单</span>"; }
                                else if (val == "22") { return "<span title='极速达'>极速达</span>"; }
                                else if (val == "23") { return "<span title='次日达'>次日达</span>"; }
                                else if (val == "24") { return "<span title='渠道订单'>渠道订单</span>"; }
                                else if (val == "25") { return "<span title='退仓单'>退仓单</span>"; }
                                else { return ""; }
                            }
                        });
                        columns.push({
                            title: '下单方式', field: 'OrderType', width: '70px', formatter: function (val, row, index) {
                                if (val == "0") { return "<span title='电脑单'>电脑单</span>"; }
                                else if (val == "1") { return "<span title='企业微信单'>企业微信单</span>"; }
                                else if (val == "2") { return "<span title='微信商城单'>微信商城单</span>"; }
                                else if (val == "3") { return "<span title='APP单'>APP单</span>"; }
                                else if (val == "4") { return "<span title='小程序单'>小程序单</span>"; }
                                else { return "<span title='电脑单'>电脑单</span>"; }
                            }
                        });
                        //columns.push({
                        //    title: '是否采购', field: 'IsPurOrder', width: '120px', formatter: function (val, row, index) {
                        //        if (val == "0") { return "<span title='未采购'>未采购</span>"; }
                        //        else if (val == "1") { return "<span title='采购中'>采购中</span>"; }
                        //        else if (val == "2") { return "<span title='采购完成'>采购完成</span>"; }
                        //        else { return "<span title='未采购'>未采购</span>"; }
                        //    }
                        //});
                        columns.push({
                            title: '微信商城单号', field: 'WXOrderNo', width: '110px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        });
                        //columns.push({
                        //    title: '出库订单号', field: 'OutOrderNo', width: '110px', formatter: function (value) {
                        //        return "<span title='" + value + "'>" + value + "</span>";
                        //    }
                        //});
                        //columns.push({
                        //    title: '付款方式', field: 'PayWay', width: '60px', formatter: function (val, row, index) {
                        //        if (val == "0") { return "<span title='微信付款'>微信付款</span>"; }
                        //        else if (val == "1") { return "<span title='额度付款'>额度付款</span>"; }
                        //        else if (val == "2") { return "<span title='积分兑换'>积分兑换</span>"; }
                        //        else { return ""; }
                        //    }
                        //});
                        columns.push({ title: '支付订单号', field: 'WXPayOrderNo', width: '190px' });
                        columns.push({ title: '出库时间', field: 'OutCargoTime', width: '125px', formatter: DateTimeFormatter });
                        columns.push({
                            title: '出库延期', field: 'DelayTime', width: '60px', formatter: function (val) {
                                if (val != null && val != "") {
                                    return "<span title='" + val + "'>" + val + "</span>";
                                }
                            }
                        });
                    }
                    columns.push({
                        title: '审核状态', field: 'FinanceSecondCheck', width: '60px', formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='未审'>未审</span>"; }
                            else if (val == "1") { return "<span title='已审'>已审</span>"; }
                            else { return ""; }
                        }
                    });
                    columns.push({
                        title: '结算状态', field: 'CheckStatus', width: '60px', formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='未结算'>未结算</span>"; }
                            else if (val == "1") { return "<span title='已结算'>已结算</span>"; }
                            else if (val == "2") { return "<span title='未结清'>未结清</span>"; }
                            else { return ""; }
                        }
                    });
                    //columns.push({ title: '结算时间', field: 'CheckDate', width: '125px', formatter: DateTimeFormatter });
                    if (HID != 64) {
                        columns.push({
                            title: '物流配送费用', field: 'DeliveryFee', width: '80px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        });
                        columns.push({
                            title: '物流公司名称', field: 'LogisticName', width: '90px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        });

                        //columns.push({
                        //    title: '仓库', field: 'TranHouse', width: '60px', formatter: function (value) {
                        //        return "<span title='" + value + "'>" + value + "</span>";
                        //    }
                        //});
                        //columns.push({
                        //    title: '外采订单', field: 'TrafficType', width: '60px',
                        //    formatter: function (val, row, index) {
                        //        if (val == "0") { return "<span title='内部订单'>内部订单</span>"; }
                        //        else if (val == "1") { return "<span title='外采订单'>外采订单</span>"; }
                        //        else if (val == "2") { return "<span title='采购订单'>采购订单</span>"; }
                        //        else { return ""; }
                        //    }
                        //});
                    } else {
                        columns.push({
                            title: '发货打印次数', field: 'PrintNum', width: '80px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        });
                    }
                    columns.push({ title: '公司类型', field: 'BelongHouse', hidden: true });
                }
            }
            var frozenColumn = [];
            frozenColumn.push({ title: '', field: 'OrderID', checkbox: true, width: '30px' });
            //frozenColumn.push({
            //    title: '出库仓库', field: 'OutHouseName', width: '80px', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});
            frozenColumn.push({
                title: '订单号', field: 'OrderNo', width: '120px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            frozenColumn.push({
                title: '出发站', field: 'Dep', width: '50px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            frozenColumn.push({
                title: '到达站', field: 'Dest', width: '50px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });

            frozenColumn.push({
                title: '数量', field: 'Piece', width: '35px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            frozenColumn.push({
                title: '已出数量', field: 'OutPiece', width: '55px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            frozenColumn.push({
                title: '收入', field: 'TransportFee', width: '60px', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            frozenColumn.push({
                title: '合计', field: 'TotalCharge', width: '70px', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            if (HouseName.indexOf("云仓") >= 0 && HID != "65") {
                frozenColumn.push({
                    title: '超期费', field: 'OverDueFee', width: '60px', align: 'right', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            }
            columns.push({
                title: '订单模式', field: 'TrafficType', width: '60px',
                formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='内部订单'>内部订单</span>"; }
                    else if (val == "1") { return "<span title='内部订单'>内部订单</span>"; }
                    else if (val == "2") { return "<span title='外采订单'>外采订单</span>"; }
                    else { return ""; }
                }
            });
            $('#dg').datagrid({
                width: '100%',
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OrderID',
                url: null,
                toolbar: '#dgtoolbar',
                frozenColumns: [frozenColumn],
                columns: [columns],
                onClickRow: function (index, row) {
                    //$('#dg').datagrid('clearSelections');

                    var selectedRows = $('#dg').datagrid('getSelections');

                    var isSelected = selectedRows.some(function (row) {
                        return $('#dg').datagrid('getRowIndex', row) === index;
                    });
                    if (isSelected) {
                        $('#dg').datagrid('selectRow', index);
                    } else {
                        $('#dg').datagrid('selectRow', index);
                    }


                    editItemByID(index)
                },
                rowStyler: function (index, row) { },
                onDblClickRow: function (index, row) {

                }
            });
            $('#dg2').datagrid({
                width: '100%',
                height: '100%',
                title: '', //标题内容
                rownumbers: true,
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                //pageSize: 15, //每页多少条
                //pageList: [15, 30],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'FPID',
                rownumbers: true,
                url: null,
                toolbar: '#toolbDg2',
                columns: [
                    [
                        { title: '', field: 'RowNumber', checkbox: true, width: '30px' },
                        //{
                        //    title: '仓库', field: 'HouseName', width: '80px', formatter: function (value) {
                        //        return "<span title='" + value + "'>" + value + "</span>";
                        //    }
                        //},
                        {
                            title: '件数', field: 'Piece', width: '80px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '已出件数', field: 'GoodsPiece', width: '80px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '库存', field: 'InventoryPiece', width: '80px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '销售价', field: 'ActSalePrice', width: '80px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '产品编码', field: 'ProductCode', width: '110px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '品牌', field: 'TypeName', width: '100px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '型号', field: 'Model', width: '80px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '货品代码', field: 'GoodsCode', width: '80px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '花纹', field: 'Figure', width: '150px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '供应商名称', field: 'SuppClientName', width: '150px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                    ]
                ],
            });
            $('#dg3').datagrid({
                width: '100%',
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'HouseName',
                url: null,
                columns: [
                    [{
                        title: '微信系统支付订单号', field: 'WXPayOrderNo', width: '210px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '支付类型', field: 'PaymentType', width: '70px',
                        formatter: function (val, row, index) {
                            if (val == "0") { return "未支付"; }
                            else if (val == "1") { return "定金"; }
                            else if (val == "2") { return "尾款"; }
                            else if (val == "3") { return "全款"; }
                            else { return "未支付"; }
                        }
                    },
                    {
                        title: '支付金额', field: 'PaymentAmount', width: '70px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '收银宝平台流水', field: 'Trxid', width: '150px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '日期', field: 'OP_DATE', width: '150px', formatter: DateTimeFormatter
                    },
                    ]
                ],
            });
            $('#dg4').datagrid({
                width: '100%',
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'HouseName',
                url: null,
                columns: [
                    [{
                        title: '预订单号', field: 'ReserveOrderNo', width: '130px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '采购单号', field: 'PurOrderNo', width: '130px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    ]
                ],
            });
            $('#dg5').datagrid({
                width: '100%',
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'HouseName',
                url: null,
                columns: [
                    [{
                        title: '预订单号', field: 'OrderNo', width: '130px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '出库单号', field: 'CargoOrderNo', width: '130px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '日期', field: 'OP_DATE', width: '150px', formatter: DateTimeFormatter
                    },
                    ]
                ],
            });
            var datenow = new Date();
            //$('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#StartDate').datebox('setValue', getNowFormatDate(datenow.getFullYear().toString() + "-" + (datenow.getMonth() + 1).toString() + "-01"));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#StartDate').datebox('textbox').bind('focus', function () { $('#StartDate').datebox('showPanel'); });
            $('#EndDate').datebox('textbox').bind('focus', function () { $('#EndDate').datebox('showPanel'); });
            $('#Dep').combobox('textbox').bind('focus', function () { $('#Dep').combobox('showPanel'); });
            $('#Dest').combobox('textbox').bind('focus', function () { $('#Dest').combobox('showPanel'); });
            //$('#CheckOutType').combobox('textbox').bind('focus', function () { $('#CheckOutType').combobox('showPanel'); });
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    var HID = rec.HouseID;

                }
            });
            console.log('--------1')
            <%-- $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');--%>


            var value2 = 0
            //所在仓库
            $('#ThrowID').combobox({ url: '../House/houseApi.aspx?method=QueryDLTHouse', valueField: 'HouseID', textField: 'Name' });

            //动态设置下拉框的属性
            console.log('--------2')
        });

        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=QueryReserveOrder';
            $('#dg').datagrid('load', {
                OrderNo: $('#AOrderNo').val(),
                LogisAwbNo: $('#LogisAwbNo').val(),
                AcceptPeople: $("#AcceptPeople").val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                FinanceSecondCheck: '-1',
                CheckOutType: '',
                OpenOrderSource: $("#OpenOrderSource").combobox('getValue'),
                Dep: $("#Dep").combobox('getText'),
                Dest: $("#Dest").combobox('getText'),
                HouseID: $("#AHouseID").combobox('getValue'),//仓库ID
                AcceptUnit: $('#AcceptUnit').val(),
                OrderModel: "0",//订单类型
                ShopCode: $("#shopCode").val(),
            });
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id='Loading' style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 98%; height: 100%; background: white; text-align: center; padding: 5px 10px; display: table;">
        <div style="display: table-cell; vertical-align: middle">
            <h1><font size="9">页面加载中……</font></h1>
        </div>
    </div>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'">
        <table>
            <tr>
                <td style="text-align: right;">订单号:
                </td>
                <td>
                    <input id="AOrderNo" class="easyui-textbox" data-options="prompt:'请输入订单号'" style="width: 100px" />
                </td>

                <td style="text-align: right;">区域大仓:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;" />
                </td>

                <td style="text-align: right;">开单时间:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px" />~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px" />
                </td>
                <td class="OpenOrderSource" style="text-align: right;">其他订单类型:
                </td>
                <td class="OpenOrderSource">
                    <select class="easyui-combobox" id="OpenOrderSource" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="-1">全部</option>
                        <option value="0">马牌</option>
                        <option value="1">慧采云仓</option>
                        <option value="2">开思</option>
                    </select>
                </td>
                <td class="AcceptUnit" style="text-align: right;">客户名称:
                </td>
                <td class="AcceptUnit">
                    <input id="AcceptUnit" class="easyui-textbox" data-options="prompt:'请输入客户名称'" style="width: 100px" />
                </td>
                <%--                <td style="text-align: right;">付款方式:
                </td>
                <td>
                    <input class="easyui-combobox" id="CheckOutType" data-options="url:'../Data/check.json',method:'get',valueField:'id',textField:'text',panelHeight:'auto'"
                        style="width: 70px;">
                </td>--%>
            </tr>
            <tr>
                <td class="AcceptPeople" style="text-align: right;">收货人:
                </td>
                <td class="AcceptPeople">
                    <input id="AcceptPeople" class="easyui-textbox" data-options="prompt:'收货单位/人'" style="width: 100px;" />
                </td>
                <td class="Dest" style="text-align: right;">到达站:
                </td>
                <td class="Dest">
                    <input id="Dest" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../systempage/sysService.aspx?method=QueryAllCity',multiple:true" />
                </td>
                <td name="qDep" class="Dep" style="text-align: right;">出发站:
                </td>
                <td name="qDep" class="Dep">
                    <input id="Dep" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../systempage/sysService.aspx?method=QueryAllCity'" /><!-- panelheight="auto"-->
                </td>

                <td class="LogisAwbNo" style="text-align: right;">物流单号:
                </td>
                <td class="LogisAwbNo">
                    <input id="LogisAwbNo" class="easyui-textbox" data-options="prompt:'请输入物流单号'" style="width: 210px" />
                </td>

            </tr>
        </table>
    </div>
    <input type="hidden" id="HiddenHouseID" />


    <table style="width: 100%">
        <tr>
            <td colspan="4" style="width: 50%; height: 400px; margin: 0px; padding: 0px;">
                <div style="display: none;">
                    <table id="dghide" class="easyui-datagrid">
                    </table>
                </div>
                <table id="dg" class="easyui-datagrid">
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 33%; height: 200px; margin: 0px; padding: 0px;">
                <table id="dg2" class="easyui-datagrid">
                </table>
            </td>
            <td style="width: 31%; height: 200px; margin: 0px; padding: 0px;">
                <table id="dg3" class="easyui-datagrid">
                </table>
            </td>
            <td style="width: 12%; height: 200px; margin: 0px; padding: 0px;">
                <table id="dg4" class="easyui-datagrid">
                </table>
            </td>
            <td style="width: 15%; height: 200px; margin: 0px; padding: 0px;">
                <table id="dg5" class="easyui-datagrid">
                </table>
            </td>
        </tr>
    </table>
    <div id="toolbDg2">
        <a href="#" class="easyui-linkbutton"
            iconcls="icon-zoom" id="btnQuery"
            plain="false" onclick="QueryStockDetail()">查看库存详情</a>
    </div>


    <div id="dgtoolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <%--<a href="#" class="easyui-linkbutton" iconcls="icon-cut" id="delete" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;--%>
        <a href="#" class="easyui-linkbutton" iconcls="icon-script" id="insert" plain="false" onclick="CreateExternalPurchaseList()">生成采购清单</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-lorry" id="insert2" plain="false" onclick="OutboundOrderV2()">生成出库单</a>&nbsp;&nbsp;
        <form runat="server" id="fm1">
            <asp:Button ID="btnTagCode" runat="server" Style="display: none;" Text="导出" OnClick="btnTagCode_Click" />
            <asp:Button ID="btnPicekPiece" runat="server" Style="display: none;" Text="导出" OnClick="btnPicekPiece_Click" />
            <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
            <asp:Button ID="btnTyreCode" runat="server" Style="display: none;" Text="导出" OnClick="btnTyreCode_Click" />
            <asp:Button ID="btnOrderInfo" runat="server" Style="display: none;" Text="导出" OnClick="btnOrderInfo_Click" />
        </form>
    </div>

    <div id="dlg" class="easyui-dialog" data-options="modal:true" style="width: 1000px; height: 600px; padding: 0px"
        closed="true" buttons="#dlg-buttons">
        <div id="saPanel2">
            <form id="fm" class="easyui-form" method="post">
                <input type="hidden" id="DisplayNum" />
                <input type="hidden" id="DisplayPiece" name="DisplayPiece" />
                <input type="hidden" name="PurchaserName" id="PurchaserName" />
                <input type="hidden" name="PurchaserBoss" id="PurchaserBoss" />
                <input type="hidden" name="PurchaserCellphone" id="PurchaserCellphone" />
                <input type="hidden" name="PurchaserAddress" id="PurchaserAddress" />
                <input type="hidden" name="DeliveryTelephone" id="DeliveryTelephone" />
                <input type="hidden" name="DeliveryBoss" id="DeliveryBoss" />
                <input type="hidden" name="DeliveryAddress" id="DeliveryAddress" />
                <input type="hidden" name="DeliveryCellphone" id="DeliveryCellphone" />
                <input type="hidden" name="PurDepart" id="PurDepart" />
                <table>
                    <tr>
                        <td style="text-align: right;">需求部门:
                        </td>
                        <td>
                            <input id="PurDepartID" name="PurDepartID" disabled readonly class="easyui-combobox" style="width: 200px" data-options="required:true" panelheight="auto" />
                        </td>
                        <td style="text-align: right;">供应商:
                        </td>
                        <td>
                            <input id="PurchaserID" name="PurchaserID" class="easyui-combobox" style="width: 200px;" data-options="required:true" />
                        </td>
                        <td style="text-align: right;">是否含税:
                        </td>
                        <td>
                            <input id="WhetherTax" name="WhetherTax" class="easyui-combobox" style="width: 120px" data-options="required:true" panelheight="auto" editable="false" />
                        </td>
                        <td style="text-align: right;">物权方:
                        </td>
                        <td>
                            <input id="OwnerShip" name="OwnerShip" class="easyui-combobox" style="width: 120px" data-options="required:true" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">采购类型:
                        </td>
                        <td>
                            <input id="PurchaseType" name="PurchaseType" class="easyui-combobox" style="width: 200px" data-options="required:true" panelheight="auto" />
                        </td>
                        <td style="text-align: right;">入库类型:
                        </td>
                        <td>
                            <input id="PurchaseInStoreType" name="PurchaseInStoreType" class="easyui-combobox" style="width: 200px" data-options="required:true" panelheight="auto" />
                        </td>
                        <td style="text-align: right;">审批流程:
                        </td>
                        <td>
                            <input id="ApproveID" name="ApproveID" class="easyui-combobox" style="width: 120px" panelheight="auto" editable="false" />
                        </td>
                        <td style="text-align: right;">业务名称：</td>
                        <td>
                            <input id="BusinessID" name="BusinessID" class="easyui-combobox" style="width: 120px" data-options="required:true" panelheight="auto" editable="false" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">提(发)货地址:
                        </td>
                        <td>
                            <input id="DeliveryName" name="DeliveryName" style="width: 200px;" />
                        </td>
                         <td colspan="2">
                             <span style="margin-left:10px;color:red;">双击单元格再编辑</span>
 </td>
                <%--        <td style="text-align: right;">到货仓库:
                        </td>
                        <td>
                            <input id="InWarehouse" name="InWarehouse" class="easyui-combobox" style="width: 200px" data-options="required:true" />
                        </td>--%>
                        <%--       <td style="text-align: right;">库存不足:
                        </td>
                        <td>
                            <input type="checkbox" class="easyui-checkbox" id="AStock" name="AStock" onclick="StockClick(this)" />
                        </td>--%>
                    </tr>
                </table>
            </form>
            <table id="cgTbl" class="easyui-datagrid">
            </table>
        </div>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="outOK()">&nbsp;确&nbsp;定&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>

    <div id="dlgDetail" class="easyui-dialog" style="width: 930px; height: 400px; padding: 0px"
        closed="true" buttons="#dlgDetail-buttons">
        <div id="saPanel3">
            <table id="detailTbl" class="easyui-datagrid">
            </table>
        </div>
    </div>
    <div id="dlgDetail-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgDetail').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>

    <div id="dlgOutHouse" class="easyui-dialog" data-options="modal:true" style="width: 1000px; height: 600px; padding: 0px"
        closed="true" buttons="#dlg-buttons-out">
        <div id="saPanel2">
            <form id="fmOut" class="easyui-form" method="post">
                <table>
                    <tr>
                        <td style="text-align: right;">出库仓库:
                        </td>
                        <td>
                            <input id="CHouseID" name="HouseID" class="easyui-combobox" style="width: 200px" data-options="required:true" panelheight="auto" />
                        </td>
                    </tr>

                </table>
            </form>
            <table id="ckTbl" class="easyui-datagrid">
            </table>
        </div>
    </div>
    <div id="dlg-buttons-out">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="GenerateOutboundOrder()">&nbsp;确&nbsp;定&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgOutHouse').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>


    <script src="../JS/easy/js/ajaxfileupload.js" type="text/javascript"></script>
    <script type="text/javascript">

        function QueryStockDetail() {

            var row = $('#dg2').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择明细数据！', 'warning');
                return;
            }//控件初始化

            $('#dlgDetail').dialog('open').dialog('setTitle', '库存明细-' + row.ProductCode);

            showStockDetailGrid();
            $('#detailTbl').datagrid('clearSelections');
            var gridOpts = $('#detailTbl').datagrid('options');
            gridOpts.url = '../Order/orderApi.aspx?method=QueryStockDetail';
            $('#detailTbl').datagrid('load', {
                HouseID: row.HouseID,
                TypeID: row.TypeID,
                Specs: row.Specs,
                GoodsCode: row.GoodsCode,
                ProductCode: row.ProductCode,
                Figure: row.Figure,
                SuppClientNum: row.SuppClientNum,
            });
        }
        //根据公司部门ID获取部门名称
        function GetUpClientDepName(value) {
            var name = '';
            if (value != "") {
                for (var i = 0; i < UpClientDep.length; i++) {
                    if (UpClientDep[i].ID == value) {
                        name = UpClientDep[i].DepName;
                        break;
                    }
                }
            }
            return name;
        };
        function CheckOutTypeChange(item) {
            if (item.value == 1) {
                $('#PeriodsNum').next(".numberbox").show();
                $("#PeriodsNumLab").css("visibility", "visible");
                $("#PeriodsNum").numberbox('options').required = true;
                $("#PeriodsNum").numberbox('textbox').validatebox('options').required = true;
                $("#PeriodsNum").numberbox('validate');
            } else {
                var Remark = $('#ARemark').val();
                Remark = Remark.replace("周期" + $('#PeriodsNum').val() + "天，", "");
                $('#ARemark').val(Remark);
                $('#PeriodsNum').numberbox('clear');
                $('#PeriodsNum').next(".numberbox").hide();
                $("#PeriodsNumLab").css("visibility", "hidden");
                $("#PeriodsNum").numberbox('options').required = false;
                $("#PeriodsNum").numberbox('textbox').validatebox('options').required = false;
                $("#PeriodsNum").numberbox('validate');
            }
        }
        //查看审批流程
        function QuerydlgApproval() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要查看的数据！', 'info');
                return;
            }
            if (row) {
                $('#dlgApproval').dialog('open').dialog('setTitle', '查看订单：' + row.OrderNo + ' 的审核流程图');
                $.ajax({
                    url: "orderApi.aspx?method=QueryExpenseRoutAtOrderNo&OrderNo=" + row.OrderNo + "&CreateAwbID=" + row.CreateAwbID + "&CreateAwb=" + row.CreateAwb + "&HouseID=" + row.HouseID,
                    cache: false,
                    success: function (text) {
                        var ldl = document.getElementById("lblApproval");
                        ldl.innerHTML = text;
                    }
                });
            }
        }
        //复选框只能选中一个
        $(function () {
            $('#OrderModel').find('input[type=checkbox]').bind('click', function () {
                var id = $(this).attr("id");
                if (this.checked) {
                    $("#OrderModel").find('input[type=checkbox]').not(this).attr("checked", false);
                }
            });
        })
        //出库标签撤回
        function RebackOutTag() {
            var rows = $('#dgTag').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要撤回的标签数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定撤回？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在撤回中...' });
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'orderApi.aspx?method=RebackOutTag&OrderID=' + $("#HOrderID").val(),
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '撤回成功!', 'info');
                                $('#dgTag').datagrid('reload');
                                $('#dgTag').datagrid('clearSelections');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }

        //导出轮胎码
        function ExportTyreCode() {
            var row = $("#dgTag").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出！', 'warning'); return; }
            var obj = document.getElementById("<%=btnTagCode.ClientID %>"); obj.click();
        }
        //查询产品标签数据列表 
        function queryTag() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要查询标签的订单！', 'warning');
                return;
            }
            if (row) {
                $('#productTag').dialog('open').dialog('setTitle', '查询订单：' + row.OrderNo + '出库标签列表');
                $('#HOrderID').val(row.OrderID);
                showTagGrid();
                $('#dgTag').datagrid('clearSelections');
                var gridOpts = $('#dgTag').datagrid('options');
                gridOpts.url = 'orderApi.aspx?method=QueryTagByOrderNo&OrderNo=' + row.OrderNo;
            }

            if (RoleCName.indexOf("安泰路斯") >= 0) {
                $('#ExportTyreCode').hide();
                $('#RebackOutTag').hide();
            }

            if ("<%=UserInfor.LoginName%>" != "1000" && "<%=UserInfor.LoginName%>" != "2076") {
                $("#RebackOutTag").hide();
            }
        }

        //标签数据列表
        function showTagGrid() {
            $('#dgTag').datagrid({
                width: '100%',
                height: '440px',
                title: '', //标题内容
                rownumbers: true,
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'TagCode',
                url: null,
                columns: [[
                    { title: '', field: '', checkbox: true, width: '30px' },
                    { title: '订单号', field: 'OrderNo', width: '80px' },
                    { title: '轮胎码', field: 'TyreCode', width: '80px' },
                    { title: '标签编码', field: 'TagCode', width: '80px' },
                    { title: '入库时间', field: 'InCargoTime', width: '120px', formatter: DateTimeFormatter },
                    { title: '出库时间', field: 'OutCargoTime', width: '120px', formatter: DateTimeFormatter },
                    { title: '出库人', field: 'OutCargoOperID', width: '60px' },
                    { title: '产品ID', field: 'ProductID', width: '50px' },
                    { title: '规格', field: 'Specs', width: '70px' },
                    { title: '花纹', field: 'Figure', width: '80px' },
                    { title: '批次', field: 'Batch', width: '50px' },
                    { title: '型号', field: 'Model', width: '60px' },
                    { title: '货品代码', field: 'GoodsCode', width: '80px' },
                    { title: '载重指数', field: 'LoadIndex', width: '60px' },
                    { title: '速度级别', field: 'SpeedLevel', width: '60px' },
                    { title: '货位代码', field: 'ContainerCode', width: '80px' },
                    { title: '一级区域', field: 'ParentAreaName', width: '60px' },
                    { title: '二级区域', field: 'AreaName', width: '60px' }
                ]],
                onClickRow: function (index, row) {
                    $('#dgTag').datagrid('clearSelections');
                    $('#dgTag').datagrid('selectRow', index);
                }
            });
        }


        function CreateOrderPickPlan() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要生成的数据！', 'warning');
                return;
            }
            for (var i = 0; i < rows.length; i++) {
                if (rows[i].AwbStatus != 0) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', rows[i].OrderNo + '已出库无法生成！', 'warning'); return;
                }
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定生成拣货单？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在生成中...' });
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'orderApi.aspx?method=CreateOrderPickPlan',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.progress("close");
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '生成成功!', 'info');
                                dosearch();
                            }
                            else {
                                $.messager.progress("close");
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }

        //保存订单跟踪状态
        function saveStatus() {
            $('#fmStatus').form('submit', {
                url: 'orderApi.aspx?method=SaveOrderStatus',
                onSubmit: function () {
                    var trd = $(this).form('enableValidation').form('validate');
                    if (trd) { $('#saveStatus').linkbutton('disable'); }
                    return trd;
                },
                success: function (msg) {
                    $('#saveStatus').linkbutton('enable');
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '更新成功!', 'info');
                        $.ajax({
                            async: false,
                            url: "orderApi.aspx?method=QueryOrderStatus&OrderNo=" + escape($('#SOrderNo').val()),
                            cache: false,
                            success: function (text) {
                                var ldl = document.getElementById("lblTrack");
                                ldl.innerHTML = text;
                            }
                        });
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }


        //删除订单信息
        function DelItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            for (var i = 0; i < rows.length; i++) {
                if (rows[i].AwbStatus != 0) {
                    if (rows[i].Piece != 0) {
                        if ("<%=UserInfor.LoginName%>" != "1000" && "<%=UserInfor.LoginName%>" != "2076") {
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', rows[i].OrderNo + '已出库无法删除！', 'warning'); return;
                        }
                    }
                }
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'orderApi.aspx?method=DelOrder',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                $('#dg').datagrid('reload');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }

        function CreateExternalPurchaseList() {
            var row = $('#dg').datagrid('getSelections');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择预订单数据！', 'warning');
                return;
            }//控件初始化
            row = row.filter(a => !(a.Piece == a.OutPiece))
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择未出库预订单数据！', 'warning');
                return;
            }//控件初始化

            reset()

            cgInit()

            $('#dlg').dialog('open').dialog('setTitle', '生成采购单');

            showProCurementGrid({
                orderNos: JSON.stringify(row.map(a => a.OrderNo)),
                //AStock: $("#AStock").prop('checked') ? 1 : 0,
                AStock: 0,
                SuppClientNum: '551098' //写死
            });
            //$('#cgTbl').datagrid('clearSelections');
            //var gridOpts = $('#cgTbl').datagrid('options');
            ////gridOpts.url = '../Order/orderApi.aspx?method=BatchQueryReserveOrderGoods';
            //$('#cgTbl').datagrid('load', {
            //    orderNos: JSON.stringify(row.map(a => a.OrderNo)),
            //    AStock: $("#AStock").prop('checked')?1:0
            //});
        }
        function OutboundOrderV2() {
            //dlgOutHouse
            var rows = $('#dg').datagrid('getSelected');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要出库的数据！', 'warning');
                return;
            }

            $('#dlgOutHouse').dialog('open').dialog('setTitle', '生成出库单');

            showCKGrid({
                orderNos: JSON.stringify([rows.OrderNo]),
                //AStock: $("#AStock").prop('checked') ? 1 : 0,
                AStock: 0,
                SuppClientNum: '551098' //写死
            });

            //$('#fmOut').form('clear');

            //$('#CHouseID').combobox({
            //    url: '../House/houseApi.aspx?method=CargoPermisionHouse',
            //    valueField: 'HouseID', textField: 'Name',
            //    onSelect: function (rec) {
            //        var HID = rec.HouseID;

            //    }
            //});
            //$('#CHouseID').combobox("setValue", rows[0].HouseID);
        }
        //标签数据列表
        function showCKGrid(item) {
            $('#ckTbl').datagrid({
                width: '990px',
                height: '440px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'HouseName',
                url: '../Order/orderApi.aspx?method=BatchQueryReserveOrderGoods',
                queryParams: item,
                columns: [[
                    {
                        title: '出库仓库', field: 'HouseIDStr', width: '80px',
                        editor: {
                            type: 'combogrid',
                            options: {
                                panelWidth: 500,
                                panelHeight: 300,
                                url: '/api/customers',
                                idField: 'HouseID',
                                textField: 'HouseName',
                                pagination: true,
                                pageSize: 10,
                                pageList: [10, 50, 100],
                                columns: [[
                                    { field: 'HouseName', title: '仓库', width: 80, align: 'center' },
                                    { field: 'Piece', title: '库存', width: 80 },
                                    //{ field: 'contactPerson', title: '联系人', width: 120 },
                                    //{ field: 'phone', title: '电话', width: 150 }
                                ]],
                                fitColumns: true,
                                mode: 'remote',
                                filter: function (q, row) {
                                    var opts = $(this).combogrid('options');
                                    return row[opts.textField].toLowerCase().indexOf(q.toLowerCase()) >= 0;
                                }
                            }
                        },
                        formatter: function (value, row, index) {
                            // 格式化显示客户名称
                            if (value) {
                                // 这里可以通过 value (customerId) 从缓存中获取客户名称
                                // 实际项目中建议缓存客户数据，避免重复请求
                                var customers = $('#orderTable').data('customers') || [];
                                var customer = customers.find(c => c.customerId == value);
                                return customer ? customer.customerName : value;
                            }
                            return '';
                        },
                        styler: function (value, row, index) {
                            return 'background-color:#ffee00;color:red;';
                        }
                    },
                    {
                        title: '件数', field: 'Piece', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '已出件数', field: 'GoodsPiece', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '销售价', field: 'ActSalePrice', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '产品编码', field: 'ProductCode', width: '110px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '品牌', field: 'TypeName', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '型号', field: 'Model', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '货品代码', field: 'GoodsCode', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '花纹', field: 'Figure', width: '150px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                ]],
                onClickRow: function (rowIndex, rowData, rowElement, event) {

                },
                onLoadSuccess: function () {

                    // 缓存客户数据，用于格式化显示
                    $.get('/api/customers', function (customers) {
                        $('#orderTable').data('customers', customers);
                    });
                    
                }
                //, onClickCell: onClickCell2
                , onDblClickCell: beginEdit
                , onAfterEdit: function (index, row, changes) {
                    // 输入框自动消失，表格恢复普通文本
                    if (999 < parseInt(row.ProcureMentPiece)) {
                        $('#cgTbl').datagrid('updateRow', {
                            index: index,
                            row: {
                                ProcureMentPiece: parseInt(row.Piece),
                            }
                        });
                    }
                },
            });
        }
        function OutboundOrder() {
            //dlgOutHouse
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要出库的数据！', 'warning');
                return;
            }
            $('#dlgOutHouse').dialog('open').dialog('setTitle', '生成出库单');

            $('#fmOut').form('clear');

            $('#CHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    var HID = rec.HouseID;

                }
            });
            $('#CHouseID').combobox("setValue", rows[0].HouseID);
        }
        function GenerateOutboundOrder() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要出库的数据！', 'warning');
                return;
            }
            var ids = rows.map(a => a.OrderID).join(',')
            var HouseID = $('#CHouseID').combobox("getValue");
            $.ajax({
                url: 'orderApi.aspx?method=GenerateOutboundOrder',
                type: 'post', dataType: 'json', data: { ids: ids, HouseID: HouseID },
                success: function (text) {
                    $.messager.progress("close");
                    if (text.Result == true) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '操作完成!', 'info');
                        $('#dg').datagrid('reload');
                        $('#dlgOutHouse').dialog('close')
                    }
                    else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                    }
                }
            });
        }

        function StockClick() {
            var row = $('#dg').datagrid('getSelections');
            $('#cgTbl').datagrid('load', {
                orderNos: JSON.stringify(row.map(a => a.OrderNo)),
                //AStock: $("#AStock").prop('checked') ? 1 : 0,
                AStock: 0,
                SuppClientNum: '551098' //写死
            });
        }

        function reset() {
            $('#fmDep').form('clear');
            $('#DisplayNum').val(0);
            $('#DisplayPiece').val(0);
        //
            //$('#ADep').textbox('setValue', '<%= UserInfor.DepCity%>');
        }

        function cgInit() {
            //采购部门
            $('#PurDepartID').combobox({
                url: '../Client/clientApi.aspx?method=QueryCargoClientSupplier&ClientType=4&UpClientID=1', valueField: 'ClientNum', textField: 'ClientName',
                onSelect: function (rec) {
                    $('#PurDepart').val(rec.ClientName);
                }
            });
            //551098
            $('#PurDepartID').combobox("setValue", 551098)
            //采购商
            $('#PurchaserID').combobox({
                url: '../Client/clientApi.aspx?method=AutoCompletePurchaser', valueField: 'PurchaserID', textField: 'PurchaserName',
                onSelect: function (rec) {
                    $('#PurchaserName').val(rec.PurchaserShortName);
                    $('#PurchaserBoss').val(rec.Boss);
                    $('#PurchaserCellphone').val(rec.Cellphone);
                    $('#PurchaserAddress').val(rec.Address);
                    $('#DeliveryName').combogrid({
                        panelWidth: 500,
                        idField: 'DAID',
                        textField: 'DeliveryAddress',
                        url: '../Client/clientApi.aspx?method=QueryDeliveryAddress&page=1&rows=9999&PurchaserID=' + rec.PurchaserID,
                        columns: [[
                            { field: 'DeliveryName', title: '提货单位', width: 120 },
                            { field: 'DeliveryBoss', title: '提货联系人', width: 100 },
                            { field: 'DeliveryCellphone', title: '提货电话', width: 100 },
                            { field: 'DeliveryAddress', title: '提货地址', width: 150 }
                        ]],
                        onSelect: function () {
                            $('#DeliveryBoss').val($('#DeliveryName').combogrid('grid').datagrid('getSelected').DeliveryBoss);
                            $('#DeliveryAddress').val($('#DeliveryName').combogrid('grid').datagrid('getSelected').DeliveryAddress);
                            $('#DeliveryCellphone').val($('#DeliveryName').combogrid('grid').datagrid('getSelected').DeliveryCellphone);
                        }
                    });
                    $('#DeliveryName').combobox('textbox').bind('focus', function () { $('#DeliveryName').combobox('showPanel'); });
                }
            });

            $('#PurchaseType').combobox({
                data: [
                    { value: '0', text: '工厂采购单' },
                    { value: '1', text: '市场采购单' },
                ],
                onSelect: function (record) {
                    $('#ApproveID').combobox('clear');

                    if (record.value == "0") {
                        $("#ApproveID").textbox('options').required = true;
                        $("#ApproveID").textbox('textbox').validatebox('options').required = true;
                        $("#ApproveID").textbox('validate');
                    }
                    else {
                        $("#ApproveID").textbox('options').required = false;
                        $("#ApproveID").textbox('textbox').validatebox('options').required = false;
                        $("#ApproveID").textbox('validate');
                    }
                }
            });

            $('#PurchaseInStoreType').combobox({
                data: [
                    { value: '0', text: '入仓单' },
                    { value: '1', text: '调货单' },
                    //{ value: '2', text: '提送单' },
                ],
            });
            $('#WhetherTax').combobox({
                data: [
                    { value: '1', text: '含税' },
                    { value: '0', text: '不含税' },
                ]
            });
            $('#WhetherTax').combobox('setValue', '1');


            $('#BusinessID').combobox({
                data: [
                    { value: '12', text: '狄乐汽服RE业务' },
                    { value: '13', text: '狄乐汽服OE业务' },
                ],
                value: '12'  // 设置默认选中的值
            });
            $('#BusinessID').combobox('setValue', '12');

            //物权方
            $('#OwnerShip').combobox({
                data: [
                    { "value": "45", "text": "广州狄乐OE" },
                    { "value": "46", "text": "广州狄乐RE" },
                    { "value": "47", "text": "湖北狄乐RE" },
                    { "value": "48", "text": "湖南狄乐RE" },
                    { "value": "1", "text": "昆明云仓" },
                    { "value": "2", "text": "龙华云仓" },
                    { "value": "3", "text": "东平云仓" },
                    { "value": "4", "text": "沙井云仓" },
                    { "value": "5", "text": "星沙云仓" },
                    { "value": "6", "text": "增城云仓" },
                    { "value": "7", "text": "西安云仓" },
                    { "value": "8", "text": "汉口云仓" },
                    { "value": "9", "text": "顺捷云仓" },
                    { "value": "10", "text": "汕头云仓" },
                    { "value": "11", "text": "渭南云仓" },
                    { "value": "12", "text": "北辰云仓" },
                    { "value": "13", "text": "南沙云仓" },
                    { "value": "14", "text": "从化云仓" },
                    { "value": "15", "text": "南海云仓" },
                    { "value": "16", "text": "大兴云仓" },
                    { "value": "17", "text": "经开云仓" },
                    { "value": "18", "text": "香坊云仓" },
                    { "value": "19", "text": "栾城云仓" },
                    { "value": "20", "text": "铁西云仓" },
                    { "value": "21", "text": "济南云仓" },
                    { "value": "22", "text": "太原云仓" },
                    { "value": "23", "text": "衡阳云仓" },
                    { "value": "24", "text": "嘉定云仓" },
                    { "value": "25", "text": "常熟云仓" },
                    { "value": "26", "text": "杭州云仓" },
                    { "value": "27", "text": "南山云仓" },
                    { "value": "28", "text": "双流云仓" },
                    { "value": "29", "text": "江宁云仓" },
                    { "value": "30", "text": "连江云仓" },
                    { "value": "31", "text": "兰州云仓" },
                    { "value": "32", "text": "银川云仓" },
                    { "value": "33", "text": "新疆云仓" },
                    { "value": "34", "text": "南开云仓" },
                    { "value": "35", "text": "兴宁云仓" },
                    { "value": "36", "text": "花都云仓" },
                    { "value": "37", "text": "蔡甸云仓" },
                    { "value": "38", "text": "光明云仓" },
                    { "value": "39", "text": "秀英云仓" },
                    { "value": "40", "text": "贵阳云仓" },
                    { "value": "41", "text": "揭阳云仓" },
                    { "value": "42", "text": "南宁云仓" },
                    { "value": "43", "text": "韶关云仓" },
                    { "value": "44", "text": "肇庆云仓" }
                ],
            });
            $('#ApproveID').combobox({
                data: [
                    { value: '16', text: 'RE采购审批流程' },
                    { value: '17', text: 'OE采购审批流程' },
                ]
            });

            $('#OwnerShip').combobox('setValue', '45');

            //$('#WhetherTax').combobox('setValue', '1');
            //$('#APID').combobox('textbox').bind('focus', function () { $('#APID').combobox('showPanel'); });
            $('#PurchaseType').combobox('clear');
            $('#ApproveID').combobox('clear');
            //$('#PurchaseInStoreType').combobox('clear');
            $('#ApproveID').combobox('textbox').bind('focus', function () { $('#ApproveID').combobox('showPanel'); });
            $('#PurchaserID').combobox('textbox').bind('focus', function () { $('#PurchaserID').combobox('showPanel'); });
            $('#BusinessID').combobox('setValue', '12');

            //所在仓库
            $('#InWarehouse').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    var HID = rec.HouseID;

                }
            });
            $('#InWarehouse').combobox("setValue",);
            //$("#AStock").prop('checked', 'checked');
        }
        //标签数据列表
        function showProCurementGrid(item) {
            $('#cgTbl').datagrid({
                width: '990px',
                height: '440px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'HouseName',
                url: '../Order/orderApi.aspx?method=BatchQueryReserveOrderGoods',
                queryParams: item,
                columns: [[
                        {
                        title: '仓库', field: 'HouseIDStr', width: '80px', 
                        editor: {
                            type: 'combobox',
                            options: {
                                //data: [
                                //    { deptId: 1, deptName: '技术部' },
                                //    { deptId: 2, deptName: '运营部' },
                                //    { deptId: 3, deptName: '市场部' },
                                //    { deptId: 4, deptName: '人事部' },
                                //    { deptId: 5, deptName: '财务部' }
                                //], // 直接调用函数获取模拟数据
                                // 关键：指定后端接口的 URL
                                url: '../House/houseApi.aspx?method=CargoPermisionHouse', 
                                valueField: 'HouseID',
                                textField: 'Name',
                                editable: true,
                                //required: true,
                                panelHeight: 'auto',
                                onLoadSuccess: function () {
                                    console.log('部门选项加载完成');
                                }
                            }
                        },
                        formatter: formatDept,
                        styler: function (value, row, index) {
                            return 'background-color:#ffee00;color:red;';
                        }
                    },
                    {
                        title: '件数', field: 'Piece', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '已出件数', field: 'GoodsPiece', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '库存', field: 'InventoryPiece', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '需采购数', field: 'ProcureMentPiece', width: '80px'
                        , editor: { type: 'numberbox', options: { min: 0, max: 999 } },
                        styler: function (value, row, index) {
                            return 'background-color:#ffee00;color:red;';
                        }
                    },
                    {
                        title: '销售价', field: 'ActSalePrice', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '产品编码', field: 'ProductCode', width: '110px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '品牌', field: 'TypeName', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '型号', field: 'Model', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '货品代码', field: 'GoodsCode', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '花纹', field: 'Figure', width: '150px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '订单号', field: 'OrderNos', width: '250px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                ]],
                onClickRow: function (rowIndex, rowData, rowElement, event) {
                
                },
                onLoadSuccess: function () {
                    console.log('表格数据加载完成');
                    beginEdit()
                }
                //, onClickCell: onClickCell2
                , onDblClickCell: beginEdit
                , onAfterEdit: function (index, row, changes) {
                    // 输入框自动消失，表格恢复普通文本
                    if (999 < parseInt(row.ProcureMentPiece)) {
                        $('#cgTbl').datagrid('updateRow', {
                            index: index,
                            row: {
                                ProcureMentPiece: parseInt(row.Piece),
                            }
                        });
                    }
                },
                onCancelEdit: function (index, row) {
                    //console.log('取消编辑:', index, row);
                }
            });
        }

        $.extend($.fn.datagrid.methods, {
            editCell: function (jq, param) {
                return jq.each(function () {
                    var opts = $(this).datagrid('options');
                    var fields = $(this).datagrid('getColumnFields', true).concat(
                        $(this).datagrid('getColumnFields'));
                    for (var i = 0; i < fields.length; i++) {
                        var col = $(this).datagrid('getColumnOption', fields[i]);
                        col.editor1 = col.editor;
                        if (fields[i] != param.field) {
                            col.editor = null;
                        }
                    }
                    $(this).datagrid('beginEdit', param.index);
                    for (var i = 0; i < fields.length; i++) {
                        var col = $(this).datagrid('getColumnOption', fields[i]);
                        col.editor = col.editor1;
                    }
                });
            }
        })


        var editIndex = undefined;
        function onClickCell2(index, field) {
            if (field =="ProcureMentPiece") {
                if (endEditing()) {
                    $('#cgTbl').datagrid('selectRow', index).datagrid('editCell', {
                        index: index,
                        field: field
                    });
                    editIndex = index;
                    var ed = $(this).datagrid('getEditor', { index: index, field: field });
                    if (ed) {
                        console.log(ed, ed.target.next())
                        // 监听输入变化
                        $(ed.target.next()).numberbox({
                            onChange: function (newValue, oldValue) {
                                // 获取当前行数据
                                var rows = $('#cgTbl').datagrid('getRows');
                                var row = rows[index];
                                if (field === 'ProcureMentPiece') {
                                    if (999 < parseInt(newValue)) {
                                        $('#cgTbl').datagrid('updateRow', {
                                            index: index,
                                            row: {
                                                ProcureMentPiece: parseInt(row.Piece),
                                            }
                                        });
                                    } else {
                                        $('#cgTbl').datagrid('updateRow', {
                                            index: index,
                                            row: {
                                                ProcureMentPiece: parseInt(newValue),
                                            }
                                        });
                                    }
                                }

                                //// 举例：输入数量时，自动计算金额 = 数量 * 单价
                                //if (field === 'Quantity') {
                                //    row.Amount = (parseFloat(newValue) || 0) * (row.Price || 0);

                                //    // 更新整行
                                //    $('#dg').datagrid('updateRow', {
                                //        index: index,
                                //        row: row
                                //    });
                                //}
                            }
                        });
                    }
                }
            }

        }
        //结束编辑 
        function endEditing() {
            if (editIndex == undefined) {
                return true
            }
            if ($('#cgTbl').datagrid('validateRow', editIndex)) {
                $('#cgTbl').datagrid('endEdit', editIndex);
                editIndex = undefined;
                return true;
            } else {
                return false;
            }
        }

        function showStockDetailGrid() {
            $('#detailTbl').datagrid({
                width: '930px',
                height: '310px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'RowNumber',
                url: null,
                columns: [[
                    {
                        title: '产品ID', field: 'ProductID', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '数量', field: 'StockNum', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '产品编码', field: 'ProductCode', width: '110px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '批次', field: 'Batch', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '仓库', field: 'HouseName', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '品牌', field: 'TypeName', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '型号', field: 'Model', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '货品代码', field: 'GoodsCode', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '花纹', field: 'Figure', width: '150px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                ]],
                onClickRow: function (index, row) {

                }
            });
        }

        function PostponeShip() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要推送的数据！', 'warning');
                return;
            }
            for (var i = 0; i < rows.length; i++) {
               <%-- if (rows[i].PostponeShip != 1) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', rows[i].OrderNo + '已推送！', 'warning'); return;
                }--%>
                if (rows[i].ModifyPriceStatus == 1) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', rows[i].OrderNo + '改价申请中！', 'warning'); return;
                }
                if (rows[i].ThrowGood == 21 && rows[i].CheckStatus != 1) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', rows[i].OrderNo + '订货单未结清！', 'warning'); return;
                }
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定推送？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'orderApi.aspx?method=PostponeShip',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单已推送!', 'info');
                                $('#dg').datagrid('reload');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }
        //保存订单
        function SaveOrderUpdate() {
            var row = $('#dgSave').datagrid('getRows');
            if (row.length <= 0) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单列表中没有数据', 'warning'); return; }
            var tdt = 0;
            if ($('#ThrwG').is(':checked')) {
                tdt++;
            }
            if ($('#DaiFa').is(':checked')) {
                tdt++;
            }
            if ($('#Tran').is(':checked')) {
                tdt++;
            }
            if (tdt > 1) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '抛货、调货和代发单只能选择一项！', 'warning');
                return;
            }
            if ($('#Tran').is(':checked')) {
                //调货订单
                var th = $('#ThrowID').combobox('getText');
                if (th == undefined || th == "") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要调货的仓库！', 'warning'); return;
                }
                var HouseName = "<%=HouseName%>";
                if (HouseName == th) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '调货仓库不能是本仓库！', 'warning'); return;
                }
            }
            if ($('#DaiFa').is(':checked')) {
                //代发订单
                var th = $('#ThrowID').combobox('getText');
                if (th == undefined || th == "") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要代发的仓库！', 'warning'); return;
                }
                var HouseName = "<%=HouseName%>";
                if (HouseName == th) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '代发仓库不能是本仓库！', 'warning'); return;
                }
            }
            if ($('#PeriodsNum').val = "" || $('#PeriodsNum').val == null) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '周期天数不能为空！', 'warning'); return;
            }
            var IsPrintPrice = null;
            if ($('#IsPrintPrice').is(':checked')) {
                IsPrintPrice = 1;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定保存？', function (r) {
                if (r) {
                    //启用复选框用于后台数据获取
                    $(".DLT").prop("disabled", false);
                    //$("#AAcceptUnit").textbox('enable');
                    //$("#AAcceptAddress").textbox('enable');
                    //$("#AAcceptTelephone").textbox('enable');
                    //$("#AAcceptCellphone").textbox('enable');
                    var json = JSON.stringify(row);
                    $.messager.progress({ msg: '请稍后,正在保存中...' });

                    $('#fmDep').form('submit', {
                        url: 'orderApi.aspx?method=updateOrderData',
                        onSubmit: function (param) {
                            param.TranHouse = $('#ThrowID').combobox('getText');
                            //param.PayClientName = $('#APayClientNum').combobox('getText');
                            param.IsPrintPrice = IsPrintPrice;
                            param.submitData = json;
                            var trd = $(this).form('enableValidation').form('validate');
                            return trd;
                        },
                        success: function (msgg) {
                            IsModifyOrder = false;
                            $.messager.progress("close");
                            var result = eval('(' + msgg + ')');
                            if (result.Result) {
                                var dd = result.Message.split('/');
                                $('#ONum').val(dd[0]);
                                $('#OutNum').val(dd[1]);
                            } else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                            }
                            //$("#AAcceptUnit").textbox('disable');
                            //$("#AAcceptAddress").textbox('disable');
                            //$("#AAcceptTelephone").textbox('disable');
                            //$("#AAcceptCellphone").textbox('disable');
                        }
                    })
                }
            });
        }
        //拉下订单
        function pldown() {
            var rows = $('#dgSave').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要拉下订单的数据！', 'warning');
                return;
            }
            var copyRows = [];
            var tCharge = $('#TransportFee').numberbox('getValue') == null || $('#TransportFee').numberbox('getValue') == "" ? 0 : Number($('#TransportFee').numberbox('getValue'));
            for (var i = 0, l = rows.length; i < l; i++) {
                var row = rows[i];
                copyRows.push(row);
                var p = $('#APiece').val() == "" || isNaN($('#APiece').val()) ? 0 : Number($('#APiece').val());
                var pt = p - Number(row.Piece);
                $('#APiece').numberbox('setValue', Number(pt));
                var SalePrice = Number(row.ActSalePrice);//销售价
                var NC = SalePrice * Number(row.Piece);
                $('#TransportFee').numberbox('setValue', tCharge - NC);

                var index = $('#outDg').datagrid('getRowIndex', copyRows[i].ID);
                if (index >= 0) {

                    var Trow = $("#outDg").datagrid('getData').rows[index];
                    Trow.Piece = Trow.InPiece;
                    $('#outDg').datagrid('updateRow', { index: index, row: Trow });
                } else {
                    //$('#outDg').datagrid('updateRow', { index: 1, row: row });
                }
            }
            for (var i = 0; i < copyRows.length; i++) {
                var index = $('#dgSave').datagrid('getRowIndex', copyRows[i]);
                $('#dgSave').datagrid('deleteRow', index);
            }
        }
        //新增采购数据
        function outOK() {

            endEdit()
            //beginEdit(false)
       
            var rows = $('#cgTbl').datagrid('getRows');
            var rows_ = $('#dg').datagrid('getSelections');
            var json = JSON.stringify(rows);
            var orders = (rows_.map(a => a.OrderNo).join(','));
            
            
       
            for (var i = 0; i < rows.length; i++) {
                if (parseInt(rows[i].ProcureMentPiece) > 0 && isEmpty(rows[i].HouseIDStr)) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '数据行[' + (i + 1)+']采购数量大于0，仓库则不能为空！', 'warning');
                    return;
                }
                if (parseInt(rows[i].ProcureMentPiece) <= 0 && !isEmpty(rows[i].HouseIDStr)) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '数据行[' + (i + 1) + ']仓库不为空，则采购数量必须大于0！', 'warning');
                      return;
                  }
            }
            rows = rows.filter(a => parseInt(a.ProcureMentPiece) > 0 && !isEmpty(a.HouseIDStr))

            if (rows.length == 0) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有满足条件的数据行，请完善可选项！', 'warning');
                return;
            }

            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定保存？', function (r) {
                if (r) {
                    $('#fm').form('submit', {
                        url: '../Order/orderApi.aspx?method=SaveProcurementBatchData',
                        contentType: "application/json;charset=utf-8",
                        type: 'post', dataType: 'json', data: { data: json },
                        onSubmit: function (param) {
                            param.submitData = json;
                            //param.ReserveOrderNos = orders;
                            //param.NoType = $("#NoType").combobox('getValues').toString();//仓库ID
                            //param.ClientTypeName = $('#ClientTypeID').combobox('getText');
                            //param.SettleHouseName = $('#SettleHouseID').combobox('getText');
                            param.PurDepartID = $("#PurDepartID").combobox("getValue")
                            var trd = $(this).form('enableValidation').form('validate');

                            return trd
                        },
                        success: function (msg) {
                            var result = eval('(' + msg + ')');
                            if (result.Result) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                                $('#dlg').dialog('close')
                                $('#dg').datagrid('reload');
                                //$('#cgTbl').datagrid('reload');
                            } else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                            }
                        }
                    })
                }
            });
        }
        ///拉上订单
        function plup() {
            $('#RuleType').val("");
            $('#RuleID').val("");
            $('#RuleTitle').val("");
            var row = $('#outDg').datagrid('getSelected');

            var sp = "<%=UserInfor.SpecialCreateAwb%>";
            if (sp == "0" || sp == '' || sp == undefined) {
                //没有特殊下单权限，需要验证先进先出
                var rows = $('#outDg').datagrid('getRows');
                for (var i = 0; i < rows.length; i++) {
                    var rw = rows[i];
                    if (rw.Specs == row.Specs && rw.Figure == row.Figure && rw.Model == row.Model && rw.BatchYear == row.BatchYear) {
                        if (row.BatchWeek > rw.BatchWeek && rw.Piece > 0) {
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请优先出库周期早的轮胎！', 'warning');
                            return;
                        }
                    }
                }
            }

            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要拉上订单的数据！', 'warning');
                return;
            }
            if (row.Piece == 0) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '在库数量为0', 'warning');
                return;
            }
            if (Number(row.TypeParentID) == 1 && Number(row.SalePrice) <= 0) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请先录入销售价格', 'warning');
                return;
            }
            if (row) {
                var clientNum = $('#ClientNum').val();
                $.ajax({
                    url: "orderApi.aspx?method=QueryPriceRuleBankInfo&HouseID=" + row.HouseID + "&TypeID=" + row.TypeID + "&Specs=" + encodeURIComponent(row.Specs) + "&Figure=" + encodeURIComponent(row.Figure) + "&Batch=" + row.Batch + "&ClientNum=" + clientNum,
                    cache: false,
                    async: true,
                    dataType: "json",
                    success: function (text) {
                        $("#lblRule").html(text.Html);
                        $.parser.parse($("#lblRule"));
                        if (text.QuotaNum != -1) {
                            var RuleID = $('#LimitID').val();
                            var RuleTitle = $('#LimitTitle').val();
                            $('#RuleType').val(4);
                            $('#RuleID').val(RuleID);
                            $('#RuleTitle').val(RuleTitle);
                            $('#HiddenLimitID').val(RuleID);
                            $('#HiddenLimitTitle').val(RuleTitle);
                            //抛货单不受限购条数限制
                            if (!$('#ThrwG').is(':checked')) {
                                var outDgrows = $("#outDg").datagrid('getData').rows;
                                var num = 0;
                                if (outDgrows.length > 0) {
                                    for (var i = 0; i < outDgrows.length; i++) {
                                        var batch = outDgrows[i].Batch;
                                        if (batch.length == 4) {
                                            var batch1 = batch.substring(0, 2);
                                            var batch2 = batch.substring(2);
                                            batch = batch2 + batch1;
                                        }

                                        var ifstr = 1 == 1;
                                        if (text.QuotaSpecs != "") {
                                            ifstr = ifstr && text.QuotaSpecs == outDgrows[i].Specs;
                                        }
                                        if (text.QuotaFigure != "") {
                                            ifstr = ifstr && text.QuotaFigure == outDgrows[i].Figure;
                                        }
                                        if (text.QuotaStartBatch != -1) {
                                            ifstr = ifstr && text.QuotaStartBatch <= batch;
                                        }
                                        if (text.QuotaEndBatch != -1) {
                                            ifstr = ifstr && text.QuotaEndBatch >= batch;
                                        }
                                        if (ifstr) {
                                            var piece = parseInt(outDgrows[i].InPiece) - parseInt(outDgrows[i].Piece);
                                            num = parseInt(num) + parseInt(piece);
                                        }
                                    }
                                } else {

                                }
                                if (parseInt(num) >= parseInt(text.QuotaNum)) {
                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '此产品达到限购数量', 'warning');
                                    $('#dlgOutCargo').dialog('close');
                                    return;
                                } else {
                                    var quotaNmu = parseInt(text.QuotaNum) - num;
                                    if (row.Piece < quotaNmu) {
                                        quotaNmu = row.Piece;
                                    }
                                    $('#Numbers').numberbox({
                                        min: 0,
                                        max: parseInt(quotaNmu),
                                        precision: 0
                                    });
                                    $('#dlgOutCargo').dialog('setTitle', '拉上  ' + row.ProductName + ' 型号：' + row.Model + ' 不得超过：' + quotaNmu + '件');
                                }
                            }
                        }
                    }
                });
                $('#dlgOutCargo').dialog('open').dialog('setTitle', '拉上  ' + row.ProductName + ' 型号：' + row.Model + ' 不得超过：' + row.Piece + '件');
                $('#InPiece').val(row.Piece);
                $('#InIndex').val($('#outDg').datagrid('getRowIndex', row));
                $('#Numbers').numberbox('setValue', '');
                $('#ActSalePrice').numberbox('setValue', row.SalePrice);

                $('#Numbers').numberbox({
                    min: 0,
                    max: row.Piece,
                    precision: 0
                });
            }
        }
        //查询在库产品
        function queryInCargoProduct() {
            if ($("#AHID").combobox('getValue') == undefined || $("#AHID").combobox('getValue') == '') {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择区域大仓！', 'warning');
                return;
            }
            if ($("#HID").combobox('getValue') == undefined || $("#HID").combobox('getValue') == '') {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择所在仓库！', 'warning');
                return;
            }
            $('#outDg').datagrid('clearSelections');
            var gridOpts = $('#outDg').datagrid('options');
            if (rowHouseID == 47) {
                gridOpts.url = '../House/houseApi.aspx?method=QueryALLHouseData';
                $('#outDg').datagrid('load', {
                    Specs: '',
                    ProductName: $('#ASpecs').val(),
                    GoodsCode: $('#AFigure').val(),
                    HAID: $("#HID").combobox('getValue'),
                    HouseID: $("#AHID").combobox('getValue')//仓库ID
                });
            } else {
                gridOpts.url = '../House/houseApi.aspx?method=QueryALLHouseData';
                $('#outDg').datagrid('load', {
                    Specs: $('#ASpecs').val(),
                    Figure: $('#AFigure').val(),
                    HAID: $("#HID").combobox('getValue'),
                    BelongDepart: $("#ABelongDepart").combobox('getValue'),//归属部门
                    HouseID: $("#AHID").combobox('getValue'),//仓库ID
                    IsLockStock: $('#AlockStock').combobox('getValue'),
                    IsQueryLockStock: IsQueryLockStock,
                });
            }
        }
        //双击显示订单详细界面 
        function editItemByID(Did) {
            $('#dg2').datagrid('loadData', { total: 0, rows: [] });
            $('#dg3').datagrid('loadData', { total: 0, rows: [] });
            $('#dg4').datagrid('loadData', { total: 0, rows: [] });

            var row = $("#dg").datagrid('getData').rows[Did];

            var gridOpts = $('#dg2').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=QueryReserveOrderGoods&OrderNo=' + row.OrderNo;
            $('#dg2').datagrid('load', {
                OrderNo: row.OrderNo,
                SuppClientNum: '551098' //写死
            });

            var gridOpts = $('#dg3').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=QueryReserveOrderPaymentRecord&OrderNo=' + row.OrderNo;
            $('#dg3').datagrid('load', {
                OrderNo: row.OrderNo
            });

            var gridOpts = $('#dg4').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=QueryReserveOrderAndProcureMent&OrderNo=' + row.OrderNo;
            $('#dg4').datagrid('load', {
                OrderNo: row.OrderNo
            });

            var gridOpts = $('#dg5').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=QueryReserveOrderAndOrder&OrderNo=' + row.OrderNo;
            $('#dg5').datagrid('load', {
                OrderNo: row.OrderNo
            });

            //var gridOpts = $('#dg4').datagrid('options');
            //gridOpts.url = 'orderApi.aspx?method=QueryOrderByOrderNo&OrderNo=' + row.OrderNo;

        }
        //显示列表
        function showGrid(dgSaveCol, houseID) {
            var columns = [];
            if (houseID == "47") {
                columns.push({ title: '', field: 'ID', checkbox: true, width: '30px' });
                columns.push({ title: '在库数量', field: 'Piece', width: '60px' });
                columns.push({
                    title: '产品名称', field: 'ProductName', width: '80px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '祺航编码', field: 'Specs', width: '75px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({ title: '富添盛编码', field: 'GoodsCode', width: '70px' });
                columns.push({ title: '商贸编码', field: 'Model', width: '110px' });
                columns.push({ title: '长和编码', field: 'Figure', width: '110px' });
                columns.push({ title: '批次', field: 'Batch', width: '50px' });
                columns.push({ title: '销售价', field: 'SalePrice', width: '50px' });
                columns.push({ title: '品牌', field: 'TypeName', width: '60px' });
                columns.push({ title: '货位代码', field: 'ContainerCode', width: '80px' });
                columns.push({ title: '所在区域', field: 'AreaName', width: '60px' });
                columns.push({ title: '所在仓库', field: 'FirstAreaName', width: '80px' });
                columns.push({ title: '产品ID', field: 'ProductID', width: '60px' });
            } else if (houseID == "62") {
                columns.push({ title: '', field: 'ID', checkbox: true, width: '30px' });
                columns.push({ title: '在库数量', field: 'Piece', width: '60px' });
                columns.push({ title: '品番', field: 'GoodsCode', width: '120px' });
                columns.push({ title: '背番', field: 'Specs', width: '80px' });
                columns.push({ title: '批次', field: 'Batch', width: '80px' });
                columns.push({ title: '品牌', field: 'TypeName', width: '90px' });
                columns.push({ title: '货位代码', field: 'ContainerCode', width: '80px' });
                columns.push({ title: '所在区域', field: 'AreaName', width: '60px' });
                columns.push({ title: '所在仓库', field: 'FirstAreaName', width: '80px' });
                columns.push({ title: '产品ID', field: 'ProductID', width: '60px' });
            } else {
                columns.push({ title: '', field: 'ID', checkbox: true, width: '30px' });
                columns.push({ title: '在库数量', field: 'Piece', width: '60px' });
                columns.push({ title: '型号', field: 'Model', width: '55px' });
                columns.push({ title: '规格', field: 'Specs', width: '75px' });
                columns.push({ title: '花纹', field: 'Figure', width: '70px' });
                columns.push({ title: '批次', field: 'Batch', width: '50px' });
                columns.push({ title: '速度', field: 'SpeedLevel', width: '50px' });
                columns.push({ title: '载重', field: 'LoadIndex', width: '50px' });
                columns.push({ title: '销售价', field: 'SalePrice', width: '50px' });
                columns.push({ title: '品牌', field: 'TypeName', width: '60px' });
                columns.push({ title: '货品代码', field: 'GoodsCode', width: '60px' });
                columns.push({
                    title: '归属部门', field: 'BelongDepart', width: '100px', formatter: function (value) {
                        return "<span title='" + GetUpClientDepName(value) + "'>" + GetUpClientDepName(value) + "</span>";
                    }
                });
                columns.push({
                    title: '规格类型', field: 'SpecsType', width: '65px', formatter: function (val, row, index) {
                        if (val == "0") { return "<span title='分配规格'>分配规格</span>"; }
                        else if (val == "1") { return "<span title='普通规格'>普通规格</span>"; }
                        else if (val == "2") { return "<span title='特价分配规格'>特价分配规格</span>"; }
                        else if (val == "3") { return "<span title='手工单'>手工单</span>"; }
                        else { return ""; }
                    }
                });
                columns.push({ title: '货位代码', field: 'ContainerCode', width: '80px' });
                columns.push({ title: '所在区域', field: 'AreaName', width: '60px' });
                columns.push({ title: '所在仓库', field: 'FirstAreaName', width: '80px' });
                columns.push({ title: '产品ID', field: 'ProductID', width: '60px' });
            }
            $('#outDg').datagrid({
                width: Number($("#dlgOrder").width()) * 0.5 - 5,
                height: '355px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [columns],
                onDblClickRow: function (index, row) { plup(index); }
            });
            var dgSavewidth = Number($("#dlgOrder").width()) * 0.5 - 5
            if (houseID == "62") {
                dgSavewidth = Number($("#dlgOrder").width()) - 10;
            }
            var RoleCName = "<%=UserInfor.RoleCName%>";
            if (RoleCName.indexOf("安泰路斯") >= 0) {
                dgSavewidth = Number($("#dlgOrder").width()) - 10;
            }
            $('#dgSave').datagrid({
                width: dgSavewidth,
                height: '355px',
                title: '出库产品', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '',
                columns: [dgSaveCol],
                onClickCell: onClickCell
            });
        }

        var IsModifyOrder = false;
        //绑定费用框
        function bindMethod() {
            $("#TransitFee").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
            $("#TransportFee").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
            //$("#DeliveryFee").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
            $("#OtherFee").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
            //$("#InsuranceFee").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
        }
        function qh() {
            var t = Number($('#TransitFee').numberbox('getValue')) + Number($('#TransportFee').numberbox('getValue')) + Number($('#OtherFee').numberbox('getValue')) - Number($('#InsuranceFee').combogrid('getText'));;
            $('#TotalCharge').numberbox('setValue', Number(t).toFixed(2));
        }

        function onAcceptAddressChanged(item) {
            $('#AAcceptUnit').textbox('setValue', item.AcceptCompany);
            $('#AAcceptAddress').textbox('setValue', item.AcceptAddress);
            $('#AAcceptTelephone').textbox('setValue', item.AcceptTelephone);
            $('#AAcceptCellphone').textbox('setValue', item.AcceptCellphone);
            $('#AAcceptPeople').textbox('setValue', item.AcceptPeople);
            $('#HiddenAAcceptPeople').val(item.AcceptPeople);
        }
        //收货人自动选择方法
        function onClientChanged(item) {
            $("#HiddenClientSelectName").val(item.Boss);
            $("#PayClientName").val(item.ClientName);
            if (item) {
                if (item.ClientType == "3") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '该客户是逾期客户，请确认收到货款再发货', 'warning');
                }

                $('#AAcceptPeople').combobox({
                    valueField: 'ADID', textField: 'AcceptPeople', delay: '10',
                    url: '../Client/clientApi.aspx?method=AutoCompleteClientAcceptPeople&ClientNum=' + item.ClientNum,
                    onSelect: onAcceptAddressChanged
                });
                var AAcceptPeople = $('#AAcceptPeople').combobox('getValue');
                var HiddenAcceptPeople = $('#HiddenAcceptPeople').val();
                if (AAcceptPeople != HiddenAcceptPeople) {
                    var outDgrows = $("#dgSave").datagrid('getData').rows;
                    if (outDgrows.length > 0) {
                        //var LimitType = 0;
                        for (var i = 0; i < outDgrows.length; i++) {
                            if (outDgrows[i].RuleType.indexOf('4') != "-1") {
                                if (outDgrows[i].Piece > 0) {
                                    $.ajax({
                                        url: "orderApi.aspx?method=QueryPriceRuleBankInfoToID&RuleID=" + outDgrows[i].RuleID + "&HouseID=" + outDgrows[i].HouseID + "&TypeID=" + outDgrows[i].TypeID + "&Specs=" + encodeURIComponent(outDgrows[i].Specs) + "&Figure=" + encodeURIComponent(outDgrows[i].Figure) + "&Batch=" + outDgrows[i].Batch + "&ClientNum=" + item.ClientNum + "&OrderNo=" + $('#OrderNo').val() + "&RuleType=4",
                                        cache: false,
                                        async: false,
                                        dataType: "json",
                                        success: function (text) {
                                            if (text.Result == true) {
                                                if (text.RuleContent < outDgrows[i].Piece) {
                                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '所选收货人订单中第' + (parseInt(i) + 1) + '条货物限购数量为' + text.RuleContent + '，无法修改为此收货人！', 'warning');
                                                    $('#AAcceptPeople').combobox('setValue', $('#HiddenAcceptPeople').val());
                                                    $("#HiddenClientSelectName").val($('#HiddenAcceptPeople').val());
                                                    return;
                                                } else {
                                                    //$('#AAcceptUnit').textbox('setValue', item.ClientName);
                                                    //$('#AAcceptAddress').textbox('setValue', item.Address);
                                                    //$('#AAcceptTelephone').textbox('setValue', item.Telephone);
                                                    //$('#AAcceptCellphone').textbox('setValue', item.Cellphone);
                                                    $('#AAcceptUnit').textbox('setValue', '');
                                                    $('#AAcceptAddress').textbox('setValue', '');
                                                    $('#AAcceptTelephone').textbox('setValue', '');
                                                    $('#AAcceptCellphone').textbox('setValue', '');
                                                    $('#HiddenAcceptPeople').val(item.Boss);
                                                    $('#ClientNum').val(item.ClientNum);
                                                    if (HiddenAcceptPeople == $('#APayClientNum').combobox('getText')) {
                                                        $('#APayClientNum').combobox('setValue', item.ClientNum);
                                                        $('#APayClientNum').combobox('setText', item.Boss);
                                                    }
                                                }
                                            } else {
                                                if (text.RuleType == "-1") {
                                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.RuleContent + '！', 'warning');
                                                    $('#AAcceptPeople').combobox('setValue', $('#HiddenAcceptPeople').val());
                                                    return;
                                                } else {
                                                    //$('#AAcceptUnit').textbox('setValue', item.ClientName);
                                                    //$('#AAcceptAddress').textbox('setValue', item.Address);
                                                    //$('#AAcceptTelephone').textbox('setValue', item.Telephone);
                                                    //$('#AAcceptCellphone').textbox('setValue', item.Cellphone);
                                                    $('#AAcceptUnit').textbox('setValue', '');
                                                    $('#AAcceptAddress').textbox('setValue', '');
                                                    $('#AAcceptTelephone').textbox('setValue', '');
                                                    $('#AAcceptCellphone').textbox('setValue', '');
                                                    $('#HiddenAcceptPeople').val(item.Boss);
                                                    $('#ClientNum').val(item.ClientNum);
                                                    if (HiddenAcceptPeople == $('#APayClientNum').combobox('getText')) {
                                                        $('#APayClientNum').combobox('setValue', item.ClientNum);
                                                        $('#APayClientNum').combobox('setText', item.Boss);
                                                    }
                                                }
                                            }
                                        }
                                    });

                                    //LimitType = 1;
                                    //break;
                                }
                            } else {
                                //$('#AAcceptUnit').textbox('setValue', item.ClientName);
                                //$('#AAcceptAddress').textbox('setValue', item.Address);
                                //$('#AAcceptTelephone').textbox('setValue', item.Telephone);
                                //$('#AAcceptCellphone').textbox('setValue', item.Cellphone);
                                $('#AAcceptUnit').textbox('setValue', '');
                                $('#AAcceptAddress').textbox('setValue', '');
                                $('#AAcceptTelephone').textbox('setValue', '');
                                $('#AAcceptCellphone').textbox('setValue', '');
                                $('#HiddenAcceptPeople').val(item.Boss);
                                $('#ClientNum').val(item.ClientNum);
                                if (HiddenAcceptPeople == $('#APayClientNum').combobox('getText')) {
                                    $('#APayClientNum').combobox('setValue', item.ClientNum);
                                    $('#APayClientNum').combobox('setText', item.Boss);
                                }
                            }
                        }
                    }
                } else {
                    //$('#AAcceptUnit').textbox('setValue', item.ClientName);
                    //$('#AAcceptAddress').textbox('setValue', item.Address);
                    //$('#AAcceptTelephone').textbox('setValue', item.Telephone);
                    //$('#AAcceptCellphone').textbox('setValue', item.Cellphone);
                    $('#HiddenAcceptPeople').val(item.Boss);
                    $('#ClientNum').val(item.ClientNum);
                    if (HiddenAcceptPeople == $('#APayClientNum').combobox('getText')) {
                        $('#APayClientNum').combobox('setValue', item.ClientNum);
                        $('#APayClientNum').combobox('setText', item.Boss);
                    }
                }
            }
        }

        //关闭弹出框
        function closeDlg() {
            if (IsModifyOrder) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请先保存订单再关闭！', 'warning');
                return;
            }
            $('#dlgOrder').dialog('close');
            $('#dg').datagrid('reload');
        }
        //输入物流单号
        function addLogisNo() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要输入物流单号的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', row.OrderNo + ' 输入物流快递单号');
                $('#fmLogic').form('clear');
                $.ajax({
                    url: 'orderApi.aspx?method=QueryOrderInfoByOrderNo&OrderNo=' + row.OrderNo,
                    dataType: "json",
                    success: function (text) {
                        $('#BLogisAwbNo').textbox('setValue', $.trim(text.LogisAwbNo));
                        $('#BOrderID').val(text.OrderID);
                        $('#BOrderNo').val(text.OrderNo);
                        //$('#BDest').val(row.Dest);
                        $('#BDest').textbox('setValue', $.trim(text.Dest));
                        $('#BSaleManID').combobox('setValue', text.SaleManID);
                        $('#BSaleManName').val(text.SaleManName);
                        $('#BSaleCellPhone').val(text.SaleCellPhone);
                        $('#HAwbNo').textbox('setValue', $.trim(text.HAwbNo));

                        if (text.LogisID != 0) {
                            $('#BLogisID').combobox('setValue', text.LogisID);
                        }
                        $('#BTransitFee').numberbox('setValue', text.DeliveryFee);
                        $('#BDeliverySettlement').combobox('setValue', text.DeliverySettlement);
                        $('#OpenExpressName').combobox('setValue', text.OpenExpressName);
                        $('#OpenExpressNum').textbox('setValue', $.trim(text.OpenExpressNum));

                    }
                });
            }
        }

        //保存提货数据
        function savedlgDeliveryDriver() {
            $('#fmdlgDeliveryDriver').form('submit', {
                url: 'orderApi.aspx?method=SaveOrderDeliveryInfo',
                onSubmit: function () {
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $('#dlgDeliveryDriver').dialog('close'); 	// close the dialog
                        dosearch();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }

        //业务员选择方法
        function onBSaleManIDChanged(item) {
            if (item) {
                $('#BSaleManName').val(item.UserName);
                $('#BSaleCellPhone').val(item.CellPhone);
            }
        }
        //保存物流公司单号
        function saveItem() {
            $('#fmLogic').form('submit', {
                url: 'orderApi.aspx?method=UpdateLogisAwbNo',
                onSubmit: function () {
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                        $('#dlg').dialog('close'); 	// close the dialog
                        dosearch();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
        //弹出定时关闭的消息框
        function alert_autoClose(title, msg, icon) {
            var interval;
            var time = 500;
            var x = 2;  //只接受整数
            $.messager.alert(title, msg, icon, function () { });
            interval = setInterval(fun, time);
            function fun() {
                --x;
                if (x == 0) {
                    clearInterval(interval);
                    $(".messager-body").window('close');
                }
            };
        }

        //数字转大写
        function numToChinese(number) {
            const numMap = '零一二三四五六七八九';
            const unitMap = ['', '十', '百', '千', '万', '十万', '百万', '千万', '亿'];

            let result = '';

            const numStr = number.toString();
            const length = numStr.length;

            for (let i = 0; i < length; i++) {
                const num = parseInt(numStr[i]);

                if (num !== 0) {
                    result += numMap[num] + unitMap[length - i - 1];
                } else {
                    const nextNum = parseInt(numStr[i + 1]);

                    if (nextNum !== 0) {
                        result += numMap[num];
                    }
                }
            }

            return result + "条";
        }

        //数字转大写 带元整
        function digitUppercase(money) {
            var fraction = ['角', '分'];
            var digit = ['零', '壹', '贰', '叁', '肆', '伍', '陆', '柒', '捌', '玖'];
            var unit = [['元', '万', '亿'], ['', '拾', '佰', '仟']];
            var head = money < 0 ? '欠' : '';
            money = Math.abs(money);
            var s = '';
            for (var i = 0; i < fraction.length; i++) {
                s += (digit[Math.floor(money * 10 * Math.pow(10, i)) % 10] + fraction[i]).replace(/零./, '');
            }
            s = s || '整';
            money = Math.floor(money);
            for (var i = 0; i < unit[0].length && money > 0; i++) {
                var p = '';
                for (var j = 0; j < unit[1].length && money > 0; j++) {
                    p = digit[money % 10] + unit[1][j] + p;
                    money = Math.floor(money / 10);
                }
                s = p.replace(/(零.)*零$/, '').replace(/^$/, '零') + unit[0][i] + s;
            }
            var sum = head + s.replace(/(零.)*零元/, '元').replace(/(零.)+/g, '零').replace(/^整$/, '零元整');
            return sum;
        }



        // -------------------------- 编辑/保存/取消逻辑 --------------------------
        // 开始编辑（支持选中行或指定行索引）
        function beginEdit(isReload=true) {
            const dg = $('#cgTbl');
            // 结束之前的编辑，避免同时编辑多行
            // 开始编辑指定行
            const rows = $('#cgTbl').datagrid('getRows');
            if (rows.length==0) {
                return;
            }
            cancelEdit(isReload);
            for (var i = 0; i < rows.length; i++) {
                dg.datagrid('beginEdit', i);
            }
            // 聚焦到第一个编辑框（优化体验）
            const editor = dg.datagrid('getEditor', { index: 0, field: 'HouseIDStr' });
            if (editor) editor.target.focus();
        }

        // 取消编辑
        function cancelEdit(isReload) {
            const dg = $('#cgTbl');
            const rows = dg.datagrid('getRows');
            for (var i = 0; i < rows.length; i++) {
                if (isReload) {
                    const index = dg.datagrid('getRowIndex', i);
                    dg.datagrid('cancelEdit', index);
                } else {
                    if (!rows[i].HouseIDStr) {
                        const index = dg.datagrid('getRowIndex', i);
                        dg.datagrid('cancelEdit', index);
                    }
                }
            }
            
        }

        // 保存编辑
        function endEdit() {
            const dg = $('#cgTbl');
            const row = dg.datagrid('getRows');
            for (var i = 0; i < row.length; i++) {
                // 验证当前行数据（必填项、格式等）
                //const isValid = dg.datagrid('validateRow', i);
                //if (!isValid) {
                //    //$.messager.alert('提示', '数据验证失败，请检查必填项和格式！', 'warning');
                //    isOk = i;
                //    return;
                //}

                // 结束编辑并保存数据
                dg.datagrid('endEdit', i);
            }
            

        }
        // 2. 格式化部门列（非编辑状态显示部门名称）
        function formatDept(value, row, index) {
            // 从下拉框组件获取缓存数据，匹配 deptId 对应的名称
            //const editor = $('#cgTbl').datagrid('getEditor', { index: index, field: 'HouseIDStr' });
            var allData = $('#AHouseID').combobox('getData');
            //console.log('editor', allData, value, row, index)
            // 初始加载时下拉框未渲染，用静态映射临时显示
            const deptMap = Object.fromEntries(
                allData.map(item => [item.HouseID, item.Name])
            );
            return deptMap[value] || '';
        }
        function isEmpty(value) {
            // 1. 判断 null 或 undefined
            if (value === null || value === undefined) {
                return true;
            }

            // 2. 判断空字符串 (包括仅包含空白字符的字符串)
            if (typeof value === 'string' && value.trim().length === 0) {
                return true;
            }

            // 3. 判断空数组
            if (Array.isArray(value) && value.length === 0) {
                return true;
            }

            // 4. 判断空对象 (不包括数组、日期、正则等特殊对象)
            // 注意：Array.isArray(value) 已经在前面判断过了，所以这里可以放心使用 typeof
            if (typeof value === 'object' && !Array.isArray(value) && value !== null) {
                // Object.keys() 会返回对象自身的可枚举属性组成的数组
                // 如果这个数组长度为 0，说明对象是空的
                return Object.keys(value).length === 0;
            }

            // 5. 其他情况，如数字 0、布尔值 false 等，通常不被认为是空
            return false;
        }
    </script>

</asp:Content>
