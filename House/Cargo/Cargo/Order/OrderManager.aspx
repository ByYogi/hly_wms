<%@ Page Title="订单管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderManager.aspx.cs" Inherits="Cargo.Order.OrderManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/Rotate/jQueryRotate.2.2.js" type="text/javascript"></script>
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
            //如果是广州仓库显示公司类型选项
            if (HID == "9") {
                $("[name='BelongHouse']").show();
                $('#BelongHouse').combobox('setValue', 0);
            } else {
                $('#BelongHouse').combobox('setValue', -1);
                $("[name='BelongHouse']").hide();
            }
            $("[name='qDep']").hide();
        }
        $(window).resize(function () {
            adjustment();
        });
        function adjustment() {
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true);
            $('#dg').datagrid({ height: height });
        }
        var RoleCName = "<%=UserInfor.RoleCName%>";
        var HouseName = "<%=UserInfor.HouseName%>";
        $(document).ready(function () {
            // 初始化：隐藏自配送相关输入框
            $("#trDeliveryDriverName").hide();
            $("#trDriverCellphone").hide();
            
            // 为快递公司下拉框添加change事件监听
            $('#OpenExpressName').combobox({
                onChange: function (newValue, oldValue) {
                    if (newValue === "THRID_DELIVERY_TMYCCGZPS") {
                        // 选中"自配送"时显示输入框
                        $("#trDeliveryDriverName").show();
                        $("#trDriverCellphone").show();
                    } else {
                        // 选择其他快递公司时隐藏输入框
                        $("#trDeliveryDriverName").hide();
                        $("#trDriverCellphone").hide();
                        // 清空输入框内容
                        $("#ADeliveryDriverName").textbox("clear");
                        $("#ADriverCellphone").textbox("clear");
                    }
                }
            });
            
            $("td.ASID").hide();
            $("td.APID").hide();
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
                $("td.AOrderType").hide();
                $("td.AOutCargoType").hide();
                $("td.AcceptUnit").hide();
                $("td.ACreateAwb").hide();
                $("td.ASaleManID").hide();

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
                columns.push({ title: '开单时间', field: 'CreateDate', width: '125px', formatter: DateTimeFormatter },
                    {
                        title: '开单员', field: 'CreateAwb', width: '60px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
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
                        title: '业务员', field: 'SaleManName', width: '55px', formatter: function (value) {
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
                    columns.push({ title: '开单时间', field: 'CreateDate', width: '125px', formatter: DateTimeFormatter },
                        {
                            title: '开单员', field: 'CreateAwb', width: '60px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        });
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
                            else if (val == "26") { return "<span title='特价单'>特价单</span>"; }
                            else if (val == "27") { return "<span title='预定单'>预定单</span>"; }
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
                    if (HID != 64) {
                        columns.push({
                            title: '优惠券', field: 'InsuranceFee', width: '50px', align: 'right', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        });
                        columns.push({
                            title: '店代码', field: 'ShopCode', width: '50px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        });
                    }
                    columns.push({
                        title: '客户名称', field: 'PayClientName', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
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
                        title: '业务员', field: 'SaleManName', width: '55px', formatter: function (value) {
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
                    columns.push({ title: '开单时间', field: 'CreateDate', width: '125px', formatter: DateTimeFormatter },
                        {
                            title: '开单员', field: 'CreateAwb', width: '60px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        });
                    if (HID != 64) {
                        columns.push({
                            title: '线路名称', field: 'LineName', width: '90px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        });
                        columns.push({
                            title: '副单号', field: 'HAwbNo', width: '80px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        });
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
                                else if (val == "26") { return "<span title='特价单'>特价单</span>"; }
                                else if (val == "27") { return "<span title='预定单'>预定单</span>"; }
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
                        columns.push({
                            title: '微信订单号', field: 'WXOrderNo', width: '110px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        });
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
                        //columns.push({
                        //    title: '订单时效', field: 'OrderAging', width: '60px', formatter: function (val) {
                        //        if (val != null && val != "") {
                        //            return "<span title='" + val + "小时'>" + val + "小时</span>";
                        //        }
                        //    }
                        //});
                        columns.push({ title: '接单人', field: 'TakeOrderName', width: '50px' });
                        columns.push({ title: '接单时间', field: 'TakeOrderTime', width: '125px', formatter: DateTimeFormatter });
                        columns.push({ title: '发车人', field: 'SendCarName', width: '50px' });
                        columns.push({ title: '发车时间', field: 'SendCarTime', width: '125px', formatter: DateTimeFormatter });
                        columns.push({ title: '签收人', field: 'Signer', width: '50px' });
                        columns.push({ title: '签收时间', field: 'SignTime', width: '125px', formatter: DateTimeFormatter });
                        columns.push({
                            title: '签收延期', field: 'AgingDelayTime', width: '60px', formatter: function (val) {
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
                        columns.push({
                            title: '提货司机', field: 'DeliveryDriverName', width: '80px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        });
                        columns.push({
                            title: '提货车牌号', field: 'DriverCarNum', width: '80px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        });
                        columns.push({
                            title: '手机号码', field: 'DriverCellphone', width: '80px', formatter: function (value) {
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
            frozenColumn.push({
                title: '出库仓库', field: 'OutHouseName', width: '80px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            frozenColumn.push({
                title: '订单号', field: 'OrderNo', width: '100px', formatter: function (value) {
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
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                rowStyler: function (index, row) {

                    //改价申请中...
                    if (row.ModifyPriceStatus == "1") { return "color:#a9a4a4"; };

                    //同城3小时未签收
                    if (row.AwbStatus != "5" && row.Dep == row.Dest && new Date(row.CreateDate) < new Date().setHours(new Date().getHours() - 3)) {
                        return "background-color:#F3F3AA;";
                    };

                    //订单已签收
                    if (row.AwbStatus == "5") {
                        //OutCargoTime  出库完成时间  > CreateDate  订单创建时间当天的最后一秒
                        //&&   （配送物流不为自送和自提)  || ((运单状态为已下单或正在备货)&&配送物流不为自送和自提 && 下单时间<单天时间的零点)
                        if (new Date(row.OutCargoTime).getTime() > new Date(new Date(row.CreateDate).getFullYear() + '-' + (new Date(row.CreateDate).getMonth() + 1) + '-' + new Date(row.CreateDate).getDate() + ' 23:59:59').getTime() && (row.LogisID != 24 && row.LogisID != 46) || (row.AwbStatus == 0 || row.AwbStatus == 1) && (row.LogisID != 24 && row.LogisID != 46) && new Date(row.CreateDate).getTime() < new Date(new Date().getFullYear() + '-' + (new Date().getMonth() + 1) + '-' + new Date().getDate() + ' 00:00:00').getTime()) {
                            //出库延期= 计算从下单时间（row.CreateDate）的当天最后一秒到出库时间（row.OutCargoTime）之间的时间差，并以小时为单位，保留一位小数
                            row.DelayTime = ((Date.parse(new Date(row.OutCargoTime)) - new Date(new Date(new Date(row.CreateDate).getFullYear() + '-' + (new Date(row.CreateDate).getMonth() + 1) + '-' + new Date(row.CreateDate).getDate() + ' 23:59:59'))) / 1000 / 3600).toFixed(1) + "小时";
                            return "background-color:#FAE7E9;color:#2a83de";
                        } else {
                            return "color:#2a83de";
                        }
                    };
                    // 外采订单+延迟推送
                    if (row.TrafficType == "2" && row.PostponeShip == "1") { return "background-color:#ddeac2"; };
                    //外采订单+已推送
                    if (row.TrafficType == "2" && row.PostponeShip == "2") { return "background-color:#b3ce7e"; };
                    //运单状态： 正在备货
                    if (row.AwbStatus == "1") { return "background-color:#FCF3CF"; };
                    //速配单或急送单
                    if (row.ThrowGood == "15" || row.ThrowGood == "17") { return "background-color:#f38f8f"; };
                    //订货单 + 延迟推送
                    if (row.ThrowGood == "21" && row.PostponeShip == "1") { return "background-color:#A2D9CE"; };
                    //订货单+不为延迟推送
                    if (row.ThrowGood == "21" && row.PostponeShip != "1") { return "background-color:#E8F8F5"; };
                    //小程序员单
                    if (row.PostponeShip == "1") { return "background-color:#FFCC66"; };
                    //出库时间的时间戳大于下单时间的当天最后一秒的时间戳
                    //&& （配送物流不为自送和自提)  || (运单状态为已下单或正在备货)&&配送物流不为自送和自提 && 下单时间<单天时间的零点
                    //&&  订单类型不为外部订单
                    if (new Date(row.OutCargoTime).getTime() > new Date(new Date(row.CreateDate).getFullYear() + '-' + (new Date(row.CreateDate).getMonth() + 1) + '-' + new Date(row.CreateDate).getDate() + ' 23:59:59').getTime() && (row.LogisID != 24 && row.LogisID != 46) || (row.AwbStatus == 0 || row.AwbStatus == 1) && (row.LogisID != 24 && row.LogisID != 46) && new Date(row.CreateDate).getTime() < new Date(new Date().getFullYear() + '-' + (new Date().getMonth() + 1) + '-' + new Date().getDate() + ' 00:00:00').getTime() && row.TrafficType != 1) {
                        //有出库时间
                        if (row.OutCargoTime != "0001-01-01T00:00:00") {
                            //出库延期= 计算从下单时间（row.CreateDate）的当天最后一秒到出库时间（row.OutCargoTime）之间的时间差，并以小时为单位，保留一位小数
                            row.DelayTime = ((Date.parse(new Date(row.OutCargoTime)) - new Date(new Date(new Date(row.CreateDate).getFullYear() + '-' + (new Date(row.CreateDate).getMonth() + 1) + '-' + new Date(row.CreateDate).getDate() + ' 23:59:59'))) / 1000 / 3600).toFixed(1) + "小时";
                        }
                        return "background-color:#FAE7E9";
                    };
                    //当前时间的时间戳大于下单时间加上延期后的时间戳
                    if (new Date().getTime() > new Date(new Date(row.CreateDate).setHours(new Date(row.CreateDate).getHours() + row.AgingDelayTime)).getTime()) {
                        return "background-color:#e68585;color:#2a83de";
                    };

                },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });
            var datenow = new Date();
            //$('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#StartDate').datebox('setValue', getNowFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#StartDate').datebox('textbox').bind('focus', function () { $('#StartDate').datebox('showPanel'); });
            $('#EndDate').datebox('textbox').bind('focus', function () { $('#EndDate').datebox('showPanel'); });
            $('#Dep').combobox('textbox').bind('focus', function () { $('#Dep').combobox('showPanel'); });
            $('#Dest').combobox('textbox').bind('focus', function () { $('#Dest').combobox('showPanel'); });
            $('#AOrderType').combobox('textbox').bind('focus', function () { $('#AOrderType').combobox('showPanel'); });
            //$('#CheckOutType').combobox('textbox').bind('focus', function () { $('#CheckOutType').combobox('showPanel'); });
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    var HID = rec.HouseID;
                    //如果是广州仓库显示公司类型选项
                    if (HID == 9) {
                        $("[name='BelongHouse']").show();
                        $('#BelongHouse').combobox('setValue', 0);
                    } else {
                        $('#BelongHouse').combobox('setValue', -1);
                        $("[name='BelongHouse']").hide();
                    }
                    $('#ASaleManID').combobox('clear');
                    var url = '../Order/orderApi.aspx?method=QueryUserByDepCode&houseID=' + rec.HouseID;
                    $('#ASaleManID').combobox('reload', url);

                    $('#PID').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    $('#PID').combobox('reload', url);

                    $('#ALineID').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryCargoLogisLineList&hid=' + rec.HouseID;
                    $('#ALineID').combobox('reload', url);
                }
            });
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');

            $('#ASaleManID').combobox('clear');
            var url = '../Order/orderApi.aspx?method=QueryUserByDepCode&houseID=<%=UserInfor.HouseID%>';
            $('#ASaleManID').combobox('reload', url);

            $('#PID').combobox('clear');
            var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=<%=UserInfor.HouseID%>';
            $('#PID').combobox('reload', url);

            $('#ALineID').combobox('clear');
            var url = '../House/houseApi.aspx?method=QueryCargoLogisLineList&hid=<%=UserInfor.HouseID%>';
            $('#ALineID').combobox('reload', url);

            $('#BusinessID').combobox('clear');
            var Burl = '../Client/clientApi.aspx?method=QueryBusinessIDDefault';
            $('#BusinessID').combobox('reload', Burl);

            //一级产品
            //$('#APID').combobox({
            //    url: '../Product/productApi.aspx?method=QueryALLOneProductType', valueField: 'TypeID', textField: 'TypeName',
            //    onSelect: function (rec) {
            //        $('#ASID').combobox('clear');
            //        $('#ASID').combobox({
            //            url: '../Product/productApi.aspx?method=QueryALLOneProductType&PID=' + rec.TypeID, valueField: 'TypeID', textField: 'TypeName', AddField: 'PinyinName',
            //            filter: function (q, row) {
            //                var opts = $(this).combobox('options');
            //                return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
            //            },
            //        });
            //    }
            //});
            //$('#APID').combobox('textbox').bind('focus', function () { $('#APID').combobox('showPanel'); });
            //$('#ASID').combobox('textbox').bind('focus', function () { $('#ASID').combobox('showPanel'); });
            $('#ThrowGood').combobox('textbox').bind('focus', function () { $('#ThrowGood').combobox('showPanel'); });
            $('#ASaleManID').combobox('textbox').bind('focus', function () { $('#ASaleManID').combobox('showPanel'); });
            var value2 = 0
            $("#simg").rotate({ bind: { click: function () { value2 += 90; $(this).rotate({ animateTo: value2 }) } } });
            //所在仓库
            $('#ThrowID').combobox({ url: '../House/houseApi.aspx?method=QueryDLTHouse', valueField: 'HouseID', textField: 'Name' });

            //动态设置下拉框的属性
            var HID = "<%=UserInfor.HouseID%>";
            var data = [];
            if (HID == "47") {
                data.push({ "text": "全部", "id": -1 });
                data.push({ "text": "长和订单", "id": 5 });
                data.push({ "text": "商贸订单", "id": 6 });
                data.push({ "text": "祺航订单", "id": 10 });
                data.push({ "text": "其它订单", "id": 7 });
                $("#ThrowGood").combobox("loadData", data);
            } else {
                data.push({ "text": "全部", "id": -1 });
                data.push({ "text": "客户单", "id": 0 });
                data.push({ "text": "抛货单", "id": 1 });
                //data.push({ "text": "内部单", "id": 2 });
                //data.push({ "text": "代发单", "id": 3 });
                //data.push({ "text": "周期胎", "id": 4 });
                data.push({ "text": "理赔单", "id": 8 });
                //data.push({ "text": "移库单", "id": 9 });
                //data.push({ "text": "展示单", "id": 11 });
                data.push({ "text": "OES客户单", "id": 12 });
                data.push({ "text": "二批单", "id": 13 });
                //data.push({ "text": "报量单", "id": 14 });
                //data.push({ "text": "速配单", "id": 15 });
                data.push({ "text": "促销单", "id": 16 });
                data.push({ "text": "急送单", "id": 17 });
                //data.push({ "text": "异地单", "id": 18 });
                //data.push({ "text": "来货单", "id": 19 });
                data.push({ "text": "电商单", "id": 20 });
                //data.push({ "text": "订货单", "id": 21 });
                data.push({ "text": "极速达", "id": 22 });
                data.push({ "text": "次日达", "id": 23 });
                data.push({ "text": "渠道订单", "id": 24 });
                data.push({ "text": "退仓单", "id": 25 });
                data.push({ "text": "特价单", "id": 26 });
                data.push({ "text": "预定单", "id": 27 });
                $("#ThrowGood").combobox("loadData", data);
            }
            $("#ThrowGood").combobox("setValue", -1);
            var IHH = "<%=UserInfor.IsHeadHouse%>";
            if (IHH == "1") {
                $('#PID').combobox('setText', '<%=UserInfor.HeadHouseName%>');
                $('#PID').combobox('setValue', '<%=UserInfor.HeadHouseID%>');
                $("#PID").combobox("readonly", true);
                $("#AHouseID").combobox("readonly", true);
                $('#btnLogisNo').hide();
                $('#btnOrderStatus').hide();
                $('#MassPrint').hide();
                $('#btnPick').hide();
                $('#barCode').hide();

                //$('#delete').hide();
                $('#btnMass').hide();
                //$('#btnTag').hide();
                $('#btnExportOrder').hide();
                $('#btnTryeCode').hide();
                $('#btnApprove').hide();
                $('#save').hide();
                $('#up').hide();
            }
            else {
                $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
                $('#PID').combobox('textbox').bind('focus', function () { $('#PID').combobox('showPanel'); });
            }
            if ("<%=UserInfor.IsExportTyre%>" == "1") {
                $('#btnTryeCode').show();
            } else {
                $('#btnTryeCode').hide();
            }
            if (RoleCName.indexOf('谦和智诚业务员') > -1) {
                $('#ACreateAwb').textbox('setValue', '<%=UserInfor.UserName%>');
                $("#ACreateAwb").textbox("readonly", true);
            }
            else if (RoleCName.indexOf("安泰路斯") >= 0) {
                //$('#APID').combobox('setValue', '1');
                //$("#APID").combobox("readonly", true);
                //$('#APID').combobox('textbox').unbind('focus');
                //$('#APID').combobox('textbox').css('background-color', '#e8e8e8');
                //一级产品
                //$('#ASID').combobox('clear');
                //$('#ASID').combobox({
                //    url: '../Product/productApi.aspx?method=QueryALLOneProductType&PID=1', valueField: 'TypeID', textField: 'TypeName', AddField: 'PinyinName',
                //    filter: function (q, row) {
                //        var opts = $(this).combobox('options');
                //        return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                //    },
                //});

                $('#ASID').combobox('setValue', '258');
                $("#ASID").combobox("readonly", true);
                $('#ASID').combobox('textbox').unbind('focus');
                $('#ASID').combobox('textbox').css('background-color', '#e8e8e8');
                $("#AHouseID").combobox("readonly", true);
                $('#AHouseID').combobox('textbox').unbind('focus');
                $('#AHouseID').combobox('textbox').css('background-color', '#e8e8e8');
    <%--            $('#ACreateAwb').textbox('setValue', '<%=UserInfor.UserName%>');
                $("#ACreateAwb").textbox("readonly", true);--%>
                $("td.outDg").hide();
                $('#up').hide();
                $("#ASaleManID").combobox("readonly", true);
                $('#ASaleManID').combobox('textbox').unbind('focus');
                $('#ASaleManID').combobox('textbox').css('background-color', '#e8e8e8');
                $('#ASaleManID').combobox('setValue', 2368);
                $("#ThrowGood").combobox("readonly", true);
                $('#ThrowGood').combobox('textbox').unbind('focus');
                $('#ThrowGood').combobox('textbox').css('background-color', '#e8e8e8');
                $("#AOrderType").combobox("readonly", true);
                $('#AOrderType').combobox('textbox').unbind('focus');
                $('#AOrderType').combobox('textbox').css('background-color', '#e8e8e8');
                $("#AOutCargoType").combobox("readonly", true);
                $('#AOutCargoType').combobox('textbox').unbind('focus');
                $('#AOutCargoType').combobox('textbox').css('background-color', '#e8e8e8');
                $('#btnApprove').hide();
                $('#btnTryeCode').hide();
                $('#btnLogisNo').hide();
                $('#MassPrint').hide();
                $('#barCode').hide();
                $('#btnMass').hide();
            }
            else if (RoleCName.indexOf("汕头科矿") >= 0) {
                $('#save').show();
                $('#up').show();
                $('#btnOrderStatus').show();
                $('#btnExportOrder').show();
                $('#btnLogisNo').show();
                $("#AHouseID").combobox("readonly", true);
                $('#AHouseID').combobox('textbox').unbind('focus');
                $('#AHouseID').combobox('textbox').css('background-color', '#e8e8e8');
                $("#ASaleManID").combobox("readonly", true);
                $('#ASaleManID').combobox('textbox').unbind('focus');
                $('#ASaleManID').combobox('textbox').css('background-color', '#e8e8e8');
                $('#ASaleManID').combobox('setValue', '2940');
                $("#ThrowGood").combobox("readonly", true);
                $('#ThrowGood').combobox('textbox').unbind('focus');
                $('#ThrowGood').combobox('textbox').css('background-color', '#e8e8e8');
                $("#AOrderType").combobox("readonly", true);
                $('#AOrderType').combobox('textbox').unbind('focus');
                $('#AOrderType').combobox('textbox').css('background-color', '#e8e8e8');
                $("#AOutCargoType").combobox("readonly", true);
                $('#AOutCargoType').combobox('textbox').unbind('focus');
                $('#AOutCargoType').combobox('textbox').css('background-color', '#e8e8e8');
            }
        });

        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=QueryOrderInfo';
            $('#dg').datagrid('load', {
                OrderNo: $('#AOrderNo').val(),
                LogisAwbNo: $('#LogisAwbNo').val(),
                AcceptPeople: $("#AcceptPeople").val(),
                TrafficType: $("#ATrafficType").combobox('getValue'),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                FinanceSecondCheck: '-1',
                CheckOutType: '',
                ThrowGood: $("#ThrowGood").combobox('getValue'),
                OrderType: $("#AOrderType").combobox('getValue'),
                OpenOrderSource: $("#OpenOrderSource").combobox('getValue'),
                AwbStatus: $("#AAwbStatus").combobox('getValue'),
                Dep: $("#Dep").combobox('getText'),
                Dest: $("#Dest").combobox('getText'),
                HouseID: $("#AHouseID").combobox('getValue'),//仓库ID
                SaleManID: $('#ASaleManID').combobox('getValue'),
                CreateAwb: $('#ACreateAwb').textbox('getValue'),
                //FirstID: $("#PID").combobox('getValue'),//父ID
                OutHouseName: $("#PID").combobox('getText'),
                AcceptUnit: $('#AcceptUnit').val(),
                OrderModel: "0",//订单类型
                LineID: $("#ALineID").combobox('getValue'),
                HAwbNo: $('#AHAwbNo').val(),
                OutCargoType: $("#AOutCargoType").combobox('getValue'),
                PostponeShip: $("#PostponeShip").combobox('getValue'),
                ShopCode: $("#shopCode").val(),
                WXOrderNo: $("#AWXOrderNo").val(),
                BelongHouse: $('#BelongHouse').combobox('getValue'),//公司类型
                BusinessID: $('#BusinessID').combobox('getValue')
            });
        }

        // 快递公司下拉框智能选择处理
        $(document).ready(function() {
            // 为快递公司下拉框添加失焦事件处理
            //$('#OpenExpressName').combobox({
            //    onHidePanel: function() {
            //        handleExpressNameAutoSelection();
            //    }
            //});
            
            // 绑定输入框失焦事件
            $('#OpenExpressName').combobox('textbox').blur(function() {
                setTimeout(function() {
                    handleExpressNameAutoSelection();
                }, 100);
            });
        });

        // 处理快递公司自动选择逻辑
        function handleExpressNameAutoSelection() {
            var $combo = $('#OpenExpressName');
            var inputText = $combo.combobox('getText').trim();
            var currentValue = $combo.combobox('getValue');
            
            // 如果已经有选中值或输入为空，不处理
            if (currentValue || !inputText) {
                return;
            }
            
            // 获取所有选项
            var data = $combo.combobox('getData');
            var matches = [];
            
            // 查找匹配项
            for (var i = 0; i < data.length; i++) {
                var option = data[i];
                var optionText = option.text || '';
                var optionValue = option.value || '';
                
                // 精确匹配（优先级最高）
                if (optionText === inputText ) {
                    matches = [option];
                    break;
                }
                
                // 包含匹配
                if (optionText.indexOf(inputText) >= 0) {
                    matches.push(option);
                }
            }
            
            // 根据匹配结果处理
            if (matches.length === 1) {
                // 唯一匹配，自动选择
                $combo.combobox('setValue', matches[0].value);
                $combo.combobox('setText', matches[0].text);
                console.log('自动选择快递公司: ' + matches[0].text);
            } else if (matches.length === 0) {
                // 无匹配，清空输入并提示
                $combo.combobox('clear');
                //$.messager.show({
                //    title: '提示',
                //    msg: '未找到匹配的快递公司，请重新选择',
                //    timeout: 3000,
                //    showType: 'slide'
                //});
            } else {
                // 多个匹配，清空输入并提示
                $combo.combobox('clear');
                //var matchNames = matches.map(function(item) { return item.text; }).join('、');
                //$.messager.show({
                //    title: '提示',
                //    msg: '找到多个匹配项：' + matchNames + '，请明确选择',
                //    timeout: 5000,
                //    showType: 'slide'
                //});
                $combo.combobox('setValue', matches[0].value);
                $combo.combobox('setText', matches[0].text);
            }
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
                <td class="ASaleManID" style="text-align: right;">业务员:
                </td>
                <td class="ASaleManID">
                    <input id="ASaleManID" class="easyui-combobox" style="width: 100px;" data-options="valueField: 'LoginName',textField: 'UserName'" />
                </td>

                <td style="text-align: right;">区域大仓:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;" />
                </td>

                <td style="text-align: right;">订单状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="AAwbStatus" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="-1">全部</option>
                        <option value="0">已下单</option>
                        <option value="6">已拣货</option>
                        <option value="1">出库中</option>
                        <option value="2">已出库</option>
                        <option value="3">已装车</option>
                        <%-- <option value="4">已到达</option>--%>
                        <option value="5">已签收</option>
                        <option value="7">异常</option>
                        <option value="-5">未签收</option>
                    </select>
                </td>
                <td class="ThrowGood" style="text-align: right;">订单类型:
                </td>
                <td class="ThrowGood">
                    <input class="easyui-combobox" id="ThrowGood" style="width: 100px;" panelheight="auto" data-options="valueField:'id',textField:'text'" editable="false" />
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

                <td class="PID" style="text-align: right;">所属仓库:
                </td>
                <td class="PID">
                    <input id="PID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'AreaID',textField:'Name'"
                        panelheight="auto" />
                </td>
                <td class="LogisAwbNo" style="text-align: right;">物流单号:
                </td>
                <td class="LogisAwbNo">
                    <input id="LogisAwbNo" class="easyui-textbox" data-options="prompt:'请输入物流单号'" style="width: 100px" />
                </td>


                <td class="AOrderType" style="text-align: right;">下单方式:
                </td>
                <td class="AOrderType">
                    <select class="easyui-combobox" id="AOrderType" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="-1">全部</option>
                        <option value="0">电脑单</option>
                        <option value="1">企业微信单</option>
                        <option value="2">商城下单</option>
                        <option value="3">APP下单</option>
                        <option value="4">小程序单</option>
                    </select>
                </td>

                <td class="ATrafficType" style="text-align: right;">订单模式:
                </td>
                <td class="ATrafficType">
                    <select class="easyui-combobox" id="ATrafficType" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="-1">全部</option>
                        <option value="0">内部订单</option>
                        <option value="1">外采订单</option>
                        <option value="2">采购订单</option>
                    </select>
                </td>

                <td style="text-align: right;">业务名称:
                </td>
                <td>
                    <input id="BusinessID" class="easyui-combobox" style="width: 120px;"
                        data-options="valueField: 'ID',textField: 'DepName'" />
                </td>
            </tr>
            <tr>
                <td class="AcceptUnit" style="text-align: right;">客户名称:
                </td>
                <td class="AcceptUnit">
                    <input id="AcceptUnit" class="easyui-textbox" data-options="prompt:'请输入客户名称'" style="width: 100px" />
                </td>
                <td style="text-align: right;">店代码:
                </td>
                <td>
                    <input id="shopCode" class="easyui-textbox" data-options="prompt:'请输入店代码'" style="width: 100px" />
                </td>
                <td style="text-align: right;">微信订单号:
                </td>
                <td>
                    <input id="AWXOrderNo" class="easyui-textbox" data-options="prompt:'请输入微信商城单号'" style="width: 100px" />
                </td>
                <td class="ACreateAwb" style="text-align: right;">开单员:
                </td>
                <td class="ACreateAwb">
                    <input id="ACreateAwb" class="easyui-textbox" data-options="prompt:'请输入开单员'" style="width: 100px" />
                </td>
                <td class="AHAwbNo" style="text-align: right;">副单号码:
                </td>
                <td class="AHAwbNo">
                    <input id="AHAwbNo" class="easyui-textbox" data-options="prompt:'请输入副单号码(发票号)'" style="width: 100px" />
                </td>
                <td class="LogisLine" style="text-align: right;">运输线路:
                </td>
                <td class="LogisLine">
                    <input id="ALineID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'LineID',textField:'LineName'" />
                </td>

                <td class="APID" style="text-align: right;">一级产品:
                </td>
                <td class="APID">
                    <input id="APID" class="easyui-combobox" style="width: 100px;"
                        panelheight="auto" />
                </td>
                <td class="ASID" style="text-align: right;">二级产品:
                </td>
                <td class="ASID">
                    <input id="ASID" class="easyui-combobox" style="width: 80px;" data-options="valueField:'TypeID',textField:'TypeName'" />
                </td>
                <td name="BelongHouse" style="text-align: right;">公司类型:
                </td>
                <td name="BelongHouse">
                    <select class="easyui-combobox" id="BelongHouse" style="width: 80px;" panelheight="auto">
                        <option value="0">迪乐泰</option>
                        <option value="1">好来运</option>
                    </select>
                </td>
                <td name="BelongHouse" style="text-align: right;">推送情况:
                </td>
                <td name="BelongHouse">
                    <select class="easyui-combobox" id="PostponeShip" style="width: 80px;" panelheight="auto" editable="false">
                        <option value="-1">全部</option>
                        <option value="0">未推送</option>
                        <option value="1">已推送</option>
                    </select>
                </td>
                <td class="AOutCargoType" style="text-align: right;">出库情况:
                </td>
                <td class="AOutCargoType">
                    <select class="easyui-combobox" id="AOutCargoType" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="-1">全部</option>
                        <option value="0">正常</option>
                        <option value="1">延期</option>
                    </select>
                </td>
            </tr>
        </table>
    </div>
    <input type="hidden" id="HiddenHouseID" />
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="dgtoolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="btnLogisNo" class="easyui-linkbutton" iconcls="icon-lorry_add" plain="false" onclick="addLogisNo()">&nbsp;物流信息&nbsp;</a>&nbsp;&nbsp;
         <a href="#" id="btnDeliveryDriver" class="easyui-linkbutton" iconcls="icon-lorry_add" plain="false" onclick="addDeliveryDriver()">&nbsp;提货信息&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="btnOrderStatus" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="updateOrderStatus()">&nbsp;订单跟踪&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" id="MassPrint" iconcls="icon-printer" onclick="massPrintOrder()">&nbsp;打印拣货单&nbsp;</a>&nbsp;&nbsp; 
         <a href="#" id="btnExportOrder" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="BatchExportOrderInfo()">&nbsp;导出订单列表&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cut" id="delete" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" id="barCode" iconcls="icon-printer" onclick="preBarCode()">&nbsp;打印标签&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="btnMass" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="massExportOrder()">&nbsp;导出拣货单&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="btnPick" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="CreateOrderPickPlan()">&nbsp;生成拣货计划&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="btnTag" class="easyui-linkbutton" iconcls="icon-tag_blue" plain="false" onclick="queryTag()">&nbsp;出库标签&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="btnApprove" class="easyui-linkbutton" iconcls="icon-sitemap_color" plain="false" onclick="QuerydlgApproval()">&nbsp;审批流程图&nbsp;</a>&nbsp;&nbsp;
            <a href="#" id="btnTryeCode" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="BatchExportTyreCode()">&nbsp;导出轮胎码&nbsp;</a>&nbsp;&nbsp;    
        <a href="#" id="postpone" class="easyui-linkbutton" iconcls="icon-basket_put" plain="false" onclick="PostponeShip()">&nbsp;发货推送&nbsp;</a>&nbsp;&nbsp;
        <form runat="server" id="fm1">
            <asp:Button ID="btnTagCode" runat="server" Style="display: none;" Text="导出" OnClick="btnTagCode_Click" />
            <asp:Button ID="btnPicekPiece" runat="server" Style="display: none;" Text="导出" OnClick="btnPicekPiece_Click" />
            <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
            <asp:Button ID="btnTyreCode" runat="server" Style="display: none;" Text="导出" OnClick="btnTyreCode_Click" />
            <asp:Button ID="btnOrderInfo" runat="server" Style="display: none;" Text="导出" OnClick="btnOrderInfo_Click" />
        </form>
    </div>
    <input type="hidden" id="DisplayNum" />
    <input type="hidden" id="DisplayPiece" />
    <div id="dlgOrder" class="easyui-dialog" style="width: 90%; height: 600px;" closed="true"
        closable="false" buttons="#dlgOrder-buttons">
        <form id="fmDep" method="post">
            <input type="hidden" name="SaleManName" id="SaleManName" />
            <input type="hidden" name="SaleCellPhone" id="SaleCellPhone" />
            <input type="hidden" name="HouseCode" id="HouseCode" />
            <input type="hidden" name="HouseID" id="HouseID" />
            <input type="hidden" name="ONum" id="ONum" />
            <input type="hidden" name="OutNum" id="OutNum" />
            <input type="hidden" name="OrderID" id="OrderID" />
            <input type="hidden" name="OrderNo" id="OrderNo" />
            <input type="hidden" name="ClientNum" id="ClientNum" />
            <input type="hidden" name="HAwbNo" id="HHAwbNo" />
            <input type="hidden" name="PayClientName" id="PayClientName" />

            <input type="hidden" name="Dep" />
            <input type="hidden" name="TrafficType" />
            <input type="hidden" id="HiddenClientNum" />
            <input type="hidden" id="HiddenAcceptPeople" />
            <input type="hidden" id="HiddenLimitID" />
            <input type="hidden" id="HiddenLimitTitle" />
            <input type="hidden" id="HiddenRemark" />
            <input type="hidden" id="HiddenClientSelectName" name="HiddenClientSelectName" />
            <input type="hidden" id="AInsuranceFee" name="AInsuranceFee" />
            <input type="hidden" id="HiddenPostponeShip" name="PostponeShip" />
            <input type="hidden" id="HiddenOutHouseName" name="HiddenOutHouseName" />

            <div id="saPanel">
                <table style="width: 100%;">
                    <tr>

                        <td class="APayClientNum" style="text-align: right;">客户名称:</td>
                        <td class="APayClientNum">
                            <input name="PayClientNum" id="APayClientNum" style="width: 70px;" class="easyui-combobox" /></td>
                        <td class="APayClientNum" style="text-align: right;">收货人:
                        </td>
                        <td class="APayClientNum">
                            <input name="AcceptPeople" id="AAcceptPeople" style="width: 70px;" class="easyui-combobox" />
                        </td>
                        <td class="AAcceptUnit" style="text-align: right;">公司名称:
                        </td>
                        <td class="AAcceptUnit">
                            <input name="AcceptUnit" id="AAcceptUnit" data-options="disabled:false" class="easyui-textbox" style="width: 125px;" />
                        </td>

                        <td class="AAcceptAddress" style="text-align: right;">收货地址:
                        </td>
                        <td class="AAcceptAddress" colspan="3">
                            <input name="AcceptAddress" id="AAcceptAddress" data-options="disabled:false" style="width: 80%;" class="easyui-textbox" />
                        </td>
                        <td class="AAcceptTelephone" style="text-align: right;">电话号码:
                        </td>
                        <td class="AAcceptTelephone">
                            <input name="AcceptTelephone" id="AAcceptTelephone" data-options="disabled:false" class="easyui-textbox" style="width: 85px;" />
                        </td>
                        <td class="AAcceptCellphone" style="text-align: right;">手机号码:
                        </td>
                        <td class="AAcceptCellphone">
                            <input name="AcceptCellphone" id="AAcceptCellphone" data-options="disabled:false" class="easyui-textbox" style="width: 85px;" />
                        </td>

                    </tr>

                    <tr>
                        <td class="TransportFee" style="text-align: right;">销售费:
                        </td>
                        <td class="TransportFee">
                            <input name="TransportFee" id="TransportFee" data-options="min:0,precision:2" readonly="true" class="easyui-numberbox"
                                style="width: 70px;" />
                        </td>
                        <td class="TransitFee" style="text-align: right;">送货费:
                        </td>
                        <td class="TransitFee">
                            <input name="TransitFee" id="TransitFee" data-options="min:0,precision:2" class="easyui-numberbox"
                                style="width: 70px;" />
                        </td>
                        <td style="text-align: right; display: none"></td>
                        <td style="display: none"></td>
                        <td class="OtherFee" style="text-align: right; display: none">其它费用:
                        </td>
                        <td class="OtherFee" style="display: none">
                            <input name="OtherFee" id="OtherFee" class="easyui-numberbox" data-options="min:0,precision:2"
                                style="width: 80px;" />
                        </td>
                        <td class="InsuranceFee" style="text-align: right; color: #f53333">优惠券:
                        </td>
                        <td class="InsuranceFee">
                            <select name="InsuranceFee" id="InsuranceFee" class="easyui-combogrid" style="width: 125px">
                            </select>
                        </td>
                        <td class="Rebate" style="text-align: right; display: none">回扣:
                        </td>
                        <td class="Rebate" style="display: none">
                            <input name="Rebate" id="Rebate" data-options="min:0,precision:2" class="easyui-numberbox"
                                style="width: 70px;" />
                        </td>
                        <td class="TotalCharge" style="text-align: right;">费用合计:
                        </td>
                        <td class="TotalCharge">
                            <input name="TotalCharge" id="TotalCharge" class="easyui-numberbox" data-options="min:0,precision:2" readonly="true" style="width: 165px;" />
                        </td>
                        <td class="CheckOutType" style="text-align: right;">付款方式:
                        </td>
                        <td class="CheckOutType">
                            <select class="easyui-combobox" id="CheckOutType" name="CheckOutType" style="width: 70px;" data-options="onSelect:CheckOutTypeChange" panelheight="auto" editable="false">
                                <option value="2">月结</option>
                                <option value="0">现付</option>
                                <option value="1">周期</option>
                                <option value="4">代收</option>
                                <option value="7">现金</option>
                                <option value="8">周结</option>
                                <option value="9">半月结</option>
                                <option value="3">货到付款</option>
                                <option value="5">微信</option>
                                <option value="6">额度</option>
                                <option value="10">预付款</option>
                            </select>
                            <input id="PeriodsNum" data-options="min:0,precision:0,required:true" class="easyui-numberbox"
                                style="width: 35px;" />
                            <label id="PeriodsNumLab" style="text-align: left;">天</label>
                        </td>
                        <td class="ALogisID" style="text-align: right;">物流名称:
                        </td>
                        <td class="ALogisID">
                            <input name="LogisID" id="ALogisID" class="easyui-combobox" style="width: 85px;" data-options="panelHeight:'200px',valueField:'ID',textField:'LogisticName',url:'../systempage/sysService.aspx?method=QueryAllLogistic'"
                                panelheight="auto" />
                        </td>
                        <td class="DeliveryFee" style="text-align: right;">物流费用:
                        </td>
                        <td class="DeliveryFee">
                            <input name="DeliveryFee" id="DeliveryFee" data-options="min:0,precision:2" class="easyui-numberbox" style="width: 85px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">总数量:
                        </td>
                        <td>
                            <input name="Piece" id="APiece" class="easyui-numberbox" data-options="min:0,precision:0,required:true" style="width: 70px;" readonly="readonly" />
                        </td>
                        <td style="text-align: right;">返利抵扣:
                        </td>
                        <td>
                            <input name="BateAmount" id="BateAmount" data-options="min:0,precision:2" class="easyui-numberbox" style="width: 70px;" />
                        </td>
                        <td style="text-align: right;">在线支付:
                        </td>
                        <td colspan="1">
                            <input name="OnlinePaidAmount" id="OnlinePaidAmount" readonly disabled data-options="min:0,precision:2" class="easyui-numberbox" style="width: 125px;" />
                        </td>
                        <td class="SaleManID" style="text-align: right;">业务人员:
                        </td>
                        <td class="SaleManID">
                            <input name="SaleManID" id="SaleManID" class="easyui-combobox" style="width: 165px;" data-options="url: 'orderApi.aspx?method=QueryUserByDepCode',valueField: 'LoginName',textField: 'UserName', onSelect: onSaleManIDChanged," />
                        </td>
                        <td class="CollectMoney" style="text-align: right;">代收款项:
                        </td>
                        <td class="CollectMoney">
                            <input name="CollectMoney" id="CollectMoney" data-options="min:0,precision:2" class="easyui-numberbox" style="width: 70px;" />
                        </td>
                        <%--  <td class="TryeClientCode" style="text-align: right;">马牌店代:
                        </td>
                        <td class="TryeClientCode">
                            <input name="TryeClientCode" id="TryeClientCode" class="easyui-textbox" readonly="true" style="width: 80px;" />
                        </td>--%>

                        <td class="ALogisAwbNo" style="text-align: right;">物流单号:
                        </td>
                        <td class="ALogisAwbNo">
                            <input name="LogisAwbNo" id="ALogisAwbNo" class="easyui-textbox" style="width: 85px;" />
                        </td>
                        <td class="ADeliverySettlement" style="text-align: right;">物流结算:
                        </td>
                        <td class="ADeliverySettlement">
                            <select class="easyui-combobox" id="ADeliverySettlement" name="DeliverySettlement" style="width: 85px;" panelheight="auto" editable="false">
                                <option value="-1">&nbsp;</option>
                                <option value="0">现付</option>
                                <option value="1">到付</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                        <td style="text-align: right;" rowspan="2">备注:
                        </td>
                        <td colspan="5" rowspan="2">
                            <textarea name="Remark" id="ARemark" rows="3" style="width: 95%; resize: none"></textarea>
                        </td>
                        <td style="text-align: right;">开单员:
                        </td>
                        <td>
                            <input name="CreateAwb" id="CreateAwb" class="easyui-textbox" readonly="readonly" style="width: 165px;" />
                        </td>
                        <td class="ThrowID" style="text-align: right;">调货仓库:</td>
                        <td class="ThrowID">
                            <input id="ThrowID" class="easyui-combobox" style="width: 70px;" />
                        </td>
                        <td class="Dep" style="text-align: right;">送货方式:
                        </td>
                        <td class="Dep">
                            <select class="easyui-combobox" id="DeliveryType" name="DeliveryType" data-options="required:true" style="width: 85px;" panelheight="auto">
                                <option value="2">普送</option>
                                <option value="0">急送</option>
                                <option value="1">自提</option>
                            </select>
                        </td>
                        <%-- <td class="Dep" style="color: Red; font-weight: bolder; text-align: right;">出发站点:
                        </td>
                        <td class="Dep">
                            <input name="Dep" id="ADep" class="easyui-textbox" readonly="readonly" style="width: 85px" />
                        </td>--%>
                        <td class="Dest" style="color: Red; font-weight: bolder; text-align: right;">到达站点:
                        </td>
                        <td class="Dest">
                            <input name="Dest" id="ADest" class="easyui-combobox" style="width: 85px;" data-options="valueField:'CityName',textField:'CityName',url:'../systempage/sysService.aspx?method=QueryAllCity',editable:true " />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">开单时间:
                        </td>
                        <td colspan="1">
                            <input name="CreateDate" id="CreateDate" class="easyui-datetimebox" data-options="formatter:AllDateTime" readonly="readonly" style="width: 165px;" />
                        </td>
                        <td colspan="4" style="text-align: left;" id="OrderModel">
                            <input class="DLT" type="checkbox" name="ThrowGood" id="ThrwG" value="1" />
                            <label class="DLT" for="ThrwG">抛货单</label>
                            <%--<input class="DLT" type="checkbox" name="ThrowGood" id="HuiYi" value="4" />
                            <label class="DLT" for="HuiYi">周期胎</label>--%>
                            <input class="DLT" type="checkbox" name="ThrowGood" id="LiPei" value="8" />
                            <label class="DLT" for="LiPei">理赔单</label>
                            <%--                         <input class="DLT" type="checkbox" name="ThrowGood" id="Tran" value="2" />
                            <label class="DLT" for="Tran">内部调货单</label>--%>
                            <input class="DLT" type="checkbox" name="ThrowGood" id="SuPei" value="15" />
                            <label class="DLT" for="SuPei" id="SuPeiLab">速配单</label>
                            <input class="DLT" type="checkbox" name="ThrowGood" id="DianShang" value="20" />
                            <label class="DLT" for="DianShang" id="DianShangLab">电商单</label>
                            <input class="ThrowGood" type="checkbox" name="ThrowGood" id="TuiCangDan" value="25" />
                            <label class="ThrowGood" for="TuiCangDan" id="TuiCangDanLab">退仓单</label>
                            <%--                            <input class="DLT" type="checkbox" name="ThrowGood" id="DaiFa" value="3" />
                            <label class="DLT" for="DaiFa">代发单</label>--%>
                            <br />
                            <%-- <input class="DLT" type="checkbox" name="ThrowGood" id="YiKu" value="9" />
                            <label class="DLT" for="YiKu">移库单</label>--%>
                            <input class="DLT" type="checkbox" name="ThrowGood" id="ZiYou" value="13" />
                            <label class="DLT" for="ZiYou">二批单</label>
                            <%--                    <input class="DLT" type="checkbox" name="ThrowGood" id="ZhanShi" value="11" />
                            <label class="DLT" for="ZhanShi">展示单</label>--%>
                            <input class="DLT" type="checkbox" name="ThrowGood" id="YiDi" value="18" />
                            <label class="DLT" for="YiDi">异地单</label>
                            <%--<input class="DLT" type="checkbox" name="ThrowGood" id="Baoliang" value="14" />
                            <label class="DLT" for="Baoliang">报量单</label>--%>
                            <input class="DLT" type="checkbox" name="ThrowGood" id="CuXiao" value="16" />
                            <label class="DLT" for="CuXiao">促销单</label>
                            <input class="DLT" type="checkbox" name="ThrowGood" id="OES" value="12" />
                            <label class="DLT" for="OES">OES客户单</label>
                            <input class="DLT" type="checkbox" name="ThrowGood" id="JiSong" value="17" />
                            <label class="DLT" for="JiSong" id="JiSongLab">急送单</label>
                            <input class="FTS" type="checkbox" name="ThrowGood" id="ChangHe" value="5" />
                            <label class="FTS" for="ChangHe">长和订单</label>
                            <input class="FTS" type="checkbox" name="ThrowGood" id="ShangMao" value="6" />
                            <label class="FTS" for="ShangMao">商贸订单</label>
                            <input class="FTS" type="checkbox" name="ThrowGood" id="qihang" value="10" />
                            <label class="FTS" for="qihang">祺航订单</label>
                            <input class="FTS" type="checkbox" name="ThrowGood" id="QiTa" value="7" />
                            <label class="FTS" for="QiTa">其它订单</label>
                            <input class="FTS" type="checkbox" name="ThrowGood" value="21" style="display: none;" />

                        </td>
                        <td style="text-align: right;">马牌单号:</td>
                        <td>
                            <input id="OpenOrderNo" name="OpenOrderNo" class="easyui-textbox" style="width: 120px;" />
                        </td>
                    </tr>
                </table>
            </div>
        </form>
        <table>
            <tr>
                <td>
                    <input type="hidden" id="dgSaveAwbStatus" />
                    <input type="hidden" id="dgSaveoldPiece" />
                    <table id="dgSave" class="easyui-datagrid">
                    </table>
                </td>
                <td class="outDg">
                    <table id="outDg" class="easyui-datagrid">
                    </table>
                </td>
            </tr>
        </table>
        <div id="toolbar">
            <table>
                <tr>
                    <td class="AASpecs" id="AASpecs" style="text-align: right;">规格:</td>
                    <td class="AASpecs">
                        <input id="ASpecs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 80px">
                    </td>
                    <td class="AHID" style="text-align: right;">区域大仓:</td>
                    <td class="AHID">
                        <input id="AHID" class="easyui-combobox" style="width: 100px;" readonly="readonly" data-options="required:true" panelheight="auto" />
                    </td>
                    <td class="ABelongDepart" style="text-align: right;">归属部门:</td>
                    <td class="ABelongDepart">
                        <input id="ABelongDepart" class="easyui-combobox" style="width: 100px;" data-options="valueField:'ID',textField:'DepName',editable:false" />
                    </td>
                </tr>
                <tr>
                    <td class="AFigure" style="text-align: right;">花纹:</td>
                    <td class="AFigure">
                        <input id="AFigure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 80px" />
                    </td>
                    <td class="HID" style="text-align: right;">所在仓库:</td>
                    <td class="HID">
                        <input id="HID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'AreaID',textField:'Name',required:true" panelheight="auto" />
                    </td>
                    <td class="HID" style="text-align: right;" id="IsQueryLockStock">锁定状态:</td>
                    <td class="HID" id="IsQueryLockStockcombobox">
                        <select class="easyui-combobox" id="AlockStock" style="width: 100%;" panelheight="auto">
                            <option value="-1">全部</option>
                            <option value="0">未锁定</option>
                            <option value="1">已锁定</option>
                        </select>&nbsp;
                    </td>
                    <td><a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="queryInCargoProduct()">查询</a></td>
                </tr>
            </table>
        </div>
    </div>
    <div id="dlgOrder-buttons">
        <table style="width: 100%">
            <tr>
                <td class="IsPrintPrice" align="left">
                    <input type="checkbox" id="IsPrintPrice" name="IsPrintPrice" value="1" />
                    <label for="IsPrintPrice">打印价格</label>
                </td>
                <td>
                    <%--<a href="#" class="easyui-linkbutton" iconcls="icon-basket_remove" plain="false" onclick="pldown()" id="down">&nbsp;拉下订单&nbsp;</a>--%>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-basket_put" plain="false" onclick="plup()" id="up">&nbsp;拉上订单&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="SaveOrderUpdate()" id="save">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" id="payprint" iconcls="icon-printer" onclick="prePrint()">&nbsp;打印发货单&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" id="pickprint" iconcls="icon-printer" onclick="pickUpOrder()">&nbsp;打印拣货单&nbsp;</a>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closeDlg()">&nbsp;关&nbsp;闭&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>

    <!--Begin 出库操作-->

    <div id="dlgOutCargo" class="easyui-dialog" style="width: 350px; height: 200px; padding: 5px 5px"
        closed="true" buttons="#dlgOutCargo-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" id="InPiece" />
            <input type="hidden" id="InIndex" />
            <table>
                <tr>
                    <td style="text-align: right;">拉上订单数量:
                    </td>
                    <td>
                        <input name="Numbers" id="Numbers" class="easyui-numberbox" data-options="min:0,precision:0"
                            style="width: 200px;">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">业务员价格：</td>
                    <td>
                        <input type="hidden" id="RuleType" />
                        <input type="hidden" id="RuleID" />
                        <input type="hidden" id="RuleTitle" />
                        <input name="ActSalePrice" id="ActSalePrice" class="easyui-numberbox" data-options="min:0,precision:2"
                            style="width: 200px;">
                    </td>
                </tr>
            </table>
            <div id="lblRule" style="display: none;"></div>
        </form>
    </div>
    <div id="dlgOutCargo-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="outOK()">&nbsp;确&nbsp;定&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgOutCargo').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <!--End 出库操作-->

    <!--Begin 输入物流单号操作-->
    <div id="dlg" class="easyui-dialog" style="width: 400px; height: 400px; padding: 0px"
        closed="true" buttons="#dlg-buttons">
        <form id="fmLogic" class="easyui-form" method="post">
            <input type="hidden" name="OrderID" id="BOrderID" />
            <input type="hidden" name="OrderNo" id="BOrderNo" />
            <input type="hidden" name="BSaleManName" id="BSaleManName" />
            <input type="hidden" name="BSaleCellPhone" id="BSaleCellPhone" />
            <div id="saPanel">
                <table>
                    <tr>
                        <td style="text-align: right;">物流公司:
                        </td>
                        <td>
                            <input name="LogisID" id="BLogisID" class="easyui-combobox" data-options="panelHeight:'200px',valueField:'ID',textField:'LogisticName',url:'../systempage/sysService.aspx?method=QueryAllLogistic',required:true"
                                panelheight="auto" style="width: 150px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">物流单号:
                        </td>
                        <td>
                            <input name="LogisAwbNo" id="BLogisAwbNo" class="easyui-textbox" style="width: 150px;" />
                        </td>
                    </tr>
                    <tr>

                        <td style="text-align: right;">物流费用:
                        </td>
                        <td>
                            <input name="BTransitFee" id="BTransitFee" data-options="min:0,precision:2" style="width: 150px;" class="easyui-numberbox" />
                        </td>
                    </tr>
                    <tr>

                        <td style="text-align: right;">物流结算:
                        </td>
                        <td>
                            <select class="easyui-combobox" id="BDeliverySettlement" name="BDeliverySettlement" style="width: 150px;" panelheight="auto" editable="false">
                                <option value="-1">&nbsp;</option>
                                <option value="0">现付</option>
                                <option value="1">到付</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">到达站:
                        </td>
                        <td>
                            <input name="Dest" id="BDest" class="easyui-textbox" style="width: 150px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">业务员:</td>
                        <td>
                            <input name="SaleManID" id="BSaleManID" class="easyui-combobox" style="width: 150px;"
                                data-options="url: 'orderApi.aspx?method=QueryUserByDepCode',valueField: 'LoginName',textField: 'UserName', onSelect: onBSaleManIDChanged," />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">副单号:
                        </td>
                        <td>
                            <input name="HAwbNo" id="HAwbNo" class="easyui-textbox" style="width: 150px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">快递公司:
                        </td>
                        <td>
                            <select id="OpenExpressName" name="OpenExpressName" style="width: 150px;" panelheight="300" editable="true">
                                <option value="">&nbsp;</option>
                                <option value="京东快递">京东快递</option>
                                <option value="德邦快递">德邦快递</option>
                                <option value="达达快递">达达快递</option>
                                <option value="跨越速运">跨越速运</option>
                                <option value="BESTQJT">百世快运</option>
                                <option value="ESB">E速宝</option>
                                <option value="LTS">联昊通</option>
                                <option value="DBL">德邦物流</option>
                                <option value="POSTB">邮政快递包裹</option>
                                <option value="XFWL">信丰物流</option>
                                <option value="EYB">邮政电商标快EYB</option>
                                <option value="XB">新邦物流</option>
                                <option value="LB">龙邦速递</option>
                                <option value="BEST">百世物流</option>
                                <option value="HOAU">天地华宇</option>
                                <option value="UAPEX">全一快递</option>
                                <option value="SHQ">华强物流</option>
                                <option value="FEDEX">联邦快递</option>
                                <option value="SURE">速尔快运</option>
                                <option value="FAST">快捷快递</option>
                                <option value="GTO">国通快递</option>
                                <option value="ZTO">中通快递</option>
                                <option value="DBKD">德邦快递(天猫)</option>
                                <option value="UC">优速快递</option>
                                <option value="QFKD">全峰快递</option>
                                <option value="TTKDEX">天天快递</option>
                                <option value="YTO">圆通速递</option>
                                <option value="HTKY">极兔速递</option>
                                <option value="YUNDA">韵达快递</option>
                                <option value="EMS">EMS</option>
                                <option value="STO">申通快递</option>
                                <option value="SF">顺丰速运</option>
                                <option value="ZJS">宅急送</option>
                                <option value="BHWL">保宏物流</option>
                                <option value="MGSD">美国速递</option>
                                <option value="UNIPS">发网</option>
                                <option value="DFH">东方汇</option>
                                <option value="YC">远长</option>
                                <option value="DISTRIBUTOR_13484485">顺心捷达</option>
                                <option value="DISTRIBUTOR_1710055">邮政标准快递</option>
                                <option value="GZLT">飞远配送</option>
                                <option value="WND">WnDirect</option>
                                <option value="DISTRIBUTOR_30292473">大食品商家自配</option>
                                <option value="DISTRIBUTOR_30464910">丰网速运</option>
                                <option value="DISTRIBUTOR_13159132">菜鸟大件-日日顺配</option>
                                <option value="DISTRIBUTOR_13148625">菜鸟大件-中铁配</option>
                                <option value="PKGJWL">派易国际物流77</option>
                                <option value="DISTRIBUTOR_13222803">中通快运</option>
                                <option value="DISTRIBUTOR_30493846">平安达腾飞快递</option>
                                <option value="DISTRIBUTOR_13211725">跨越速运(天猫)</option>
                                <option value="YUD">长发</option>
                                <option value="DTW">大田</option>
                                <option value="CYEXP">长宇</option>
                                <option value="POST">中国邮政</option>
                                <option value="HQSY">环球速运</option>
                                <option value="YTKD">运通快递</option>
                                <option value="ZTKY01">中铁快运</option>
                                <option value="SHWL">盛辉物流</option>
                                <option value="LHT">联昊通速递</option>
                                <option value="CJKD">城际快递</option>
                                <option value="ZY_MZ">168 美中快递</option>
                                <option value="CFWL">春风物流</option>
                                <option value="HF">汇丰物流</option>
                                <option value="CHTWL">诚通物流</option>
                                <option value="CSTD">畅顺通达</option>
                                <option value="CITY100">城市100</option>
                                <option value="BNTWL">奔腾物流</option>
                                <option value="ANEKY">安能物流</option>
                                <option value="AYCA">澳邮专线</option>
                                <option value="BETWL">百腾物流</option>
                                <option value="LJS">立即送</option>
                                <option value="CTG">联合运通</option>
                                <option value="ZY_XDKD">迅达快递</option>
                                <option value="STWL">速腾快递</option>
                                <option value="ALKJWL">阿里跨境电商物流</option>
                                <option value="SFGJ">顺丰国际</option>
                                <option value="JYM">加运美</option>
                                <option value="ETONG">E通速递</option>
                                <option value="ZY_HTONG">华通快运</option>
                                <option value="XIAOBI">晓毕物流</option>
                                <option value="JD">京东快递(天猫)</option>
                                <option value="HTB56">徽托邦物流</option>
                                <option value="YLFWL">一路发物流</option>
                                <option value="ZTWY">中天万运</option>
                                <option value="ST">速通物流</option>
                                <option value="BFAY">八方安运</option>
                                <option value="BDT">八达通</option>
                                <option value="ESDEX">卓志速运</option>
                                <option value="ZY_TZKD">天泽快递</option>
                                <option value="ANNTO">安得物流</option>
                                <option value="MLWL">明亮物流</option>
                                <option value="TJS">特急送</option>
                                <option value="RQ">荣庆物流</option>
                                <option value="MDM">门对门快递</option>
                                <option value="HRWL01">韩润物流</option>
                                <option value="HTWL">鸿泰物流</option>
                                <option value="WJWL">万家物流</option>
                                <option value="ZTWL">中铁物流</option>
                                <option value="HDKD">汇达快递</option>
                                <option value="AJ">安捷快递</option>
                                <option value="AX">安迅物流</option>
                                <option value="YD">韵达速递</option>
                                <option value="YJWL">云聚物流</option>
                                <option value="ZY_XGX">新干线快递</option>
                                <option value="CBO">CBO钏博物流</option>
                                <option value="CNPEX">CNPEX中邮快递</option>
                                <option value="DSWL">D速物流</option>
                                <option value="ZY_ETD">ETD</option>
                                <option value="ZY_TPAK">TrakPak</option>
                                <option value="YZTSY">一站通速运</option>
                                <option value="YTFH">一统飞鸿</option>
                                <option value="STSD">三态速递</option>
                                <option value="SDWL">上大物流</option>
                                <option value="ZYKD">中邮快递</option>
                                <option value="AMAZON">亚马逊物流</option>
                                <option value="JGSD">京广速递</option>
                                <option value="YLSY">亿领速运</option>
                                <option value="ZY_XDSY">信达速运</option>
                                <option value="GSD">共速达</option>
                                <option value="GD">冠达</option>
                                <option value="BJXKY">北极星快运</option>
                                <option value="HXLWL">华夏龙物流</option>
                                <option value="DML">大马鹿</option>
                                <option value="ZY_TTHT">天天海淘</option>
                                <option value="HLYSD">好来运快递</option>
                                <option value="AXD">安鲜达</option>
                                <option value="KBSY">快8速运</option>
                                <option value="KFW">快服务</option>
                                <option value="KTKD">快淘快递</option>
                                <option value="KSDWL">快速递物流</option>
                                <option value="HLWL">恒路物流</option>
                                <option value="CND">承诺达</option>
                                <option value="JGWL">景光物流</option>
                                <option value="ZHN">智汇鸟</option>
                                <option value="TJDGJWL">泰捷达物流</option>
                                <option value="AUODEXPRESS">澳德物流</option>
                                <option value="ABGJ">澳邦国际</option>
                                <option value="BCTWL">百城通物流</option>
                                <option value="BFDF">百福东方</option>
                                <option value="JYSY">精英速运</option>
                                <option value="WPE">维普恩</option>
                                <option value="YF">耀飞快递</option>
                                <option value="LHKD">蓝弧快递</option>
                                <option value="ZY_BYECO">贝易购</option>
                                <option value="XDEXPRESS">迅达速递</option>
                                <option value="TYWL01">通用物流</option>
                                <option value="HTKD">青岛恒通快递</option>
                                <option value="DNWL">丹鸟物流</option>
                                <option value="ZMKM">芝麻开门</option>
                                <option value="KY">跨越速运(KY)</option>
                                <option value="JT">极兔速递</option>
                                <option value="THRID_DELIVERY_TMYCCGZPS">自配送</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">快递单号:
                        </td>
                        <td>
                            <input name="OpenExpressNum" id="OpenExpressNum" class="easyui-textbox" style="width: 150px;" />
                        </td>
                    </tr>
                    <tr id="trDeliveryDriverName">
                        <td style="text-align: right;">自配送联系人:
                        </td>
                        <td>
                            <input name="DeliveryDriverName" id="ADeliveryDriverName" class="easyui-textbox" style="width: 150px;" />
                        </td>
                    </tr>
                    <tr id="trDriverCellphone">
                        <td style="text-align: right;">自配送联系人电话:
                        </td>
                        <td>
                            <input name="DriverCellphone" id="ADriverCellphone" class="easyui-textbox" style="width: 150px;" />
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">&nbsp;&nbsp;保&nbsp;存&nbsp;&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;&nbsp;取&nbsp;消&nbsp;&nbsp;</a>
    </div>
    <!--End 输入物流单号操作-->


    <!--Begin 输入提货信息操作-->
    <div id="dlgDeliveryDriver" class="easyui-dialog" style="width: 500px; height: 400px; padding: 0px" closed="true" buttons="#dlgDeliveryDriver-buttons">
        <form id="fmdlgDeliveryDriver" class="easyui-form" method="post" enctype="multipart/form-data">
            <input type="hidden" name="OrderID" id="DOrderID" />
            <input type="hidden" name="OrderNo" id="DOrderNo" />
            <div id="saPanel">
                <table>
                    <tr>
                        <td style="text-align: right;">提货司机姓名:
                        </td>
                        <td>
                            <input name="DeliveryDriverName" id="DeliveryDriverName" class="easyui-textbox" style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">司机车牌号码:
                        </td>
                        <td>
                            <input name="DriverCarNum" id="DriverCarNum" class="easyui-textbox" style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">司机手机号码:
                        </td>
                        <td>
                            <input name="DriverCellphone" id="DriverCellphone" data-options="min:0,precision:0" style="width: 250px;" class="easyui-numberbox" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">司机身份证号:
                        </td>
                        <td>
                            <input name="DriverIDNum" id="DriverIDNum" class="easyui-textbox" style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <input style="width: 400px; height: 30px;" id="DeliveryPic" name="DeliveryPic" class="easyui-filebox" data-options="prompt:'选择多个照片...',multiple:true,buttonText:'点击选择'" />
                        </td>
                    </tr>
                </table>
            </div>
        </form>
        <table id="dgInsurcePicture" class="easyui-datagrid">
        </table>
    </div>
    <div id="dlgDeliveryDriver-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="savedlgDeliveryDriver()">&nbsp;&nbsp;保&nbsp;存&nbsp;&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgDeliveryDriver').dialog('close')">&nbsp;&nbsp;取&nbsp;消&nbsp;&nbsp;</a>
    </div>
    <!--End 输入物流单号操作-->


    <!--订单状态跟踪-->
    <div id="dlgStatus" class="easyui-dialog" style="width: 900px; height: 700px; padding: 0px"
        closed="true" closable="false" buttons="#dlgStatus-buttons">
        <form id="fmStatus" class="easyui-form" method="post">
            <input type="hidden" name="OrderID" id="SOrderID" />
            <input type="hidden" name="OrderNo" id="SOrderNo" />
            <input type="hidden" name="WXOrderNo" id="SWXOrderNo" />
            <div id="saPanel">
                <table class="mini-toolbar" style="width: 100%;">
                    <tr>
                        <td rowspan="3" style="border-right: 1px solid #909aa6;">订<br />
                            单<br />
                            在<br />
                            途<br />
                            跟<br />
                            踪
                        </td>
                        <td class="OrderStatusTd" align="left">&nbsp;&nbsp;状态：
                        </td>
                        <td align="left" class="OrderStatusTd">
                            <input name="OrderStatus" id="AOrderStatus5" type="radio" value="6"><label for="AOrderStatus5" style="font-size: 14px;">已拣货</label>
                            <input name="OrderStatus" id="AOrderStatus1" type="radio" value="2"><label for="AOrderStatus1" style="font-size: 14px;" id="AOrderStatus1Show">已出库</label>
                            <input name="OrderStatus" id="AOrderStatus2" type="radio" value="3"><label for="AOrderStatus2" style="font-size: 14px;">已装车</label>
                            <%-- <input name="OrderStatus" id="AOrderStatus3" type="radio" value="4"><label for="AOrderStatus3" style="font-size: 14px;">已到达</label>--%>
                            <input name="OrderStatus" id="AOrderStatus4" type="radio" value="5"><label for="AOrderStatus4" style="font-size: 14px;" id="AOrderStatus4Show">已签收</label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2" class="OrderStatusTd">
                            <textarea name="DetailInfo" id="DetailInfo" cols="60" style="height: 20px; width: 95%; resize: none" class="mini-textarea"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <div id="tbDepmanifest" class="easyui-tabs" data-options="fit:true" style="height: 250px; width: 860px">
                                <div title="订单跟踪" id="lblTrack" data-options="iconCls:'icon-page_add'"></div>
                                <div title="好来运跟踪" id="lblTrack2" data-options="iconCls:'icon-page_add'"></div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
        <div id="map" style="width: 100%; height: 63%">
        </div>
    </div>
    <div id="dlgStatus-buttons">
        <a href="#" class="easyui-linkbutton OrderStatusTd" id="saveStatus" iconcls="icon-ok" onclick="saveStatus()">&nbsp;保&nbsp;存&nbsp;</a> <a href="#" class="easyui-linkbutton" iconcls="icon-cancel"
            onclick="closeDlgStatus()">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <!--订单状态跟踪-->
    <div id="dgViewImg" class="easyui-dialog" closed="true" style="width: 1000px; height: 600px; overflow: hidden; display: flex; justify-content: center; align-items: center;">
        <img id="simg" style="max-width: 100%; max-height: 170%;" />
    </div>
    <!--Begin 查询产品标签列表-->
    <div id="productTag" class="easyui-dialog" style="width: 1000px; height: 520px; padding: 2px 2px"
        closed="true" buttons="#productTag-buttons">
        <table id="dgTag" class="easyui-datagrid">
        </table>
        <div id="productTag-buttons">
            <input type="hidden" name="OrderID" id="HOrderID" />
            <a href="#" class="easyui-linkbutton" iconcls="icon-redo" plain="false" onclick="RebackOutTag()" id="RebackOutTag">&nbsp;撤回出库标签&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="ExportTyreCode()" id="ExportTyreCode">&nbsp;导出轮胎码&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#productTag').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
        </div>
    </div>

    <!--订单审批流程图-->
    <div id="dlgApproval" class="easyui-dialog" style="width: 900px; height: 500px; padding: 1px 1px"
        closed="true" closable="false" buttons="#dlgApproval-buttons">
        <div id="lblApproval">
        </div>
    </div>
    <div id="dlgApproval-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel"
            onclick="javascript:$('#dlgApproval').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <!--订单审批流程图-->
    <object id="LODOP_OB" title="dd" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA"
        width="0px" height="0px">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0px" height="0px"></embed>
    </object>
    <script src="../JS/easy/js/ajaxfileupload.js" type="text/javascript"></script>
    <script type="text/javascript">
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
        //打印标签
        function preBarCode() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要打印的数据！', 'warning');
                return;
            }
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            LODOP.PRINT_INITA(0, 25, 850, 500, "");
            LODOP.SET_PRINT_PAGESIZE(0, 850, 500, "");

            var js = 0, Alltotal = 0, AllPiece = 0; p = 1; pie = 0; total = 0;
            for (var k = 0; k < rows.length; k++) {
                pie = Number(rows[k].Piece);
                for (j = 1; j <= pie; j++) {
                    CreateOnePage(rows[k].OrderNo, rows[k].Dep, rows[k].Dest, rows[k].AcceptPeople, pie + "--" + j, rows[k].AcceptCellphone, rows[k].AcceptAddress, rows[k].AcceptUnit, rows[k].HAwbNo, rows[k].PayClientName);
                };
            }

            //LODOP.PREVIEW();
            //LODOP.PRINT_DESIGN();

            LODOP.SET_PREVIEW_WINDOW(0, 0, 0, 0, 0, "");
            var printNum = LODOP.GET_PRINTER_COUNT();
            if (printNum <= 0) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '电脑未安装打印机！', 'warning');
                return;
            }
            var ise = 0;
            //Gprinter  GP-3120TL
            for (var i = 0; i < printNum - 1; i++) {
                var pName = LODOP.Printers.list[i].DriverName;
                if (pName.indexOf("Gprinter") != -1) {
                    LODOP.SET_PRINTER_INDEX(i);
                    ise = 1;
                    break;
                }
            }
            if (ise == 0) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '电脑未安装标签打印机！', 'warning');
                return;
            }
            LODOP.PRINT();
            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '打印任务发送成功！', 'info');
            //LODOP.PRINT_DESIGN();
            //LODOP.PREVIEW();
        }
        function CreateOnePage(orderNo, Dep, Dest, AeecptName, Num, Cellphone, AcceptAddress, AcceptUnit, HAwbNo, PayClientName) {

            LODOP.NewPage();
            LODOP.ADD_PRINT_LINE(50, 12, 49, 303, 0, 1);
            if (HAwbNo != "" && AcceptUnit.indexOf("京东") != -1) {
                LODOP.ADD_PRINT_TEXT(2, 15, 170, 20, "京东单号");
                LODOP.ADD_PRINT_TEXT(18, 15, 170, 45, HAwbNo);
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                if (PayClientName == "京东") {
                    LODOP.ADD_PRINT_IMAGE(1, 197, 40, 48, "<img src='../CSS/image/jd.png'/>");
                    LODOP.SET_PRINT_STYLEA(0, "Stretch", 1);
                }
                LODOP.ADD_PRINT_TEXT(51, 180, 130, 20, orderNo);
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 7);
                LODOP.ADD_PRINT_BARCODE(3, 200, 48, 47, "QRCode", orderNo);
            } else {
                LODOP.ADD_PRINT_TEXT(33, 20, 162, 16, orderNo);
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
                LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                LODOP.ADD_PRINT_TEXT(4, 42, 198, 22, AcceptUnit);
                if (AcceptUnit.length > 10) {
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                } else {
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 13);
                }
                LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);
                LODOP.ADD_PRINT_BARCODE(3, 200, 48, 47, "QRCode", orderNo);
            }

            LODOP.ADD_PRINT_LINE(93, 12, 92, 303, 0, 1);
            LODOP.ADD_PRINT_TEXT(57, 21, 272, 35, Dep + "—" + Dest);
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 22);
            LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(98, 19, 194, 20, "收货人:" + AeecptName);
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(98, 226, 71, 20, Num);
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(141, 19, 45, 16, "地址:");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            if (AcceptAddress.length > 38) {
                LODOP.ADD_PRINT_TEXT(141, 19, 280, 35, "      " + AcceptAddress);
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
            } else {
                LODOP.ADD_PRINT_TEXT(141, 19, 280, 35, "     " + AcceptAddress);
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            }
            LODOP.ADD_PRINT_LINE(118, 14, 117, 303, 0, 1);
            LODOP.ADD_PRINT_TEXT(122, 19, 45, 20, "电话:");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(122, 54, 237, 20, Cellphone);
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);


        }
        //批量导出出库轮胎码
        function BatchExportTyreCode() {
            $.ajax({
                url: "orderApi.aspx?method=QueryOrderTyreCodeForExport&OrderNo=" + $('#AOrderNo').val() + "&LogisAwbNo=" + $('#LogisAwbNo').val() + "&AcceptPeople=" + $("#AcceptPeople").val() + "&Piece=&StartDate=" + $('#StartDate').datebox('getValue') + "&EndDate=" + $('#EndDate').datebox('getValue') + "&FinanceSecondCheck=-1&CheckOutType=''&ThrowGood=" + $("#ThrowGood").combobox('getValue') + "&OrderType=" + $("#AOrderType").combobox('getValue') + "&AwbStatus=" + $("#AAwbStatus").combobox('getValue') + "&Dep=" + $("#Dep").combobox('getText') + "&Dest=" + $("#Dest").combobox('getText') + "&HouseID=" + $("#AHouseID").combobox('getValue') + "&SaleManID=" + $('#ASaleManID').combobox('getValue') + "&CreateAwb=" + $('#ACreateAwb').textbox('getValue') + "&OutHouseName=" + $("#PID").combobox('getText') + "&AcceptUnit=" + encodeURIComponent($('#AcceptUnit').val()) + "&OrderModel=0&BelongHouse=" + $('#BelongHouse').combobox('getValue') + "&OutCargoType=" + $('#AOutCargoType').combobox('getValue') + "&PostponeShip=" + $("#PostponeShip").combobox('getValue'),
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=btnTyreCode.ClientID %>"); obj.click() }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }
        //批量导出订单信息
        function BatchExportOrderInfo() {
            $.ajax({
                url: "orderApi.aspx?method=QueryOrderInfoExport&OrderNo=" + $('#AOrderNo').val() + "&LogisAwbNo=" + $('#LogisAwbNo').val() + "&AcceptPeople=" + $("#AcceptPeople").val() + "&Piece=&StartDate=" + $('#StartDate').datebox('getValue') + "&EndDate=" + $('#EndDate').datebox('getValue') + "&FinanceSecondCheck=-1&CheckOutType=''&ThrowGood=" + $("#ThrowGood").combobox('getValue') + "&OrderType=" + $("#AOrderType").combobox('getValue') + "&AwbStatus=" + $("#AAwbStatus").combobox('getValue') + "&Dep=" + $("#Dep").combobox('getText') + "&Dest=" + $("#Dest").combobox('getText') + "&HouseID=" + $("#AHouseID").combobox('getValue') + "&SaleManID=" + $('#ASaleManID').combobox('getValue') + "&CreateAwb=" + $('#ACreateAwb').textbox('getValue') + "&OutHouseName=" + $("#PID").combobox('getText') + "&AcceptUnit=" + encodeURIComponent($('#AcceptUnit').val()) + "&OrderModel=0&BelongHouse=" + $('#BelongHouse').combobox('getValue') + "&OutCargoType=" + $('#AOutCargoType').combobox('getValue') + "&PostponeShip=" + $("#PostponeShip").combobox('getValue') + "&LineID=" + $("#ALineID").combobox('getValue') + "&ShopCode=" + $("#shopCode").val(),
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=btnOrderInfo.ClientID %>"); obj.click() }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }

        //批量导出拣货单
        function massExportOrder() {
            if ($("#AHouseID").combobox('getValue') == "63") {
                var rows = $('#dg').datagrid('getSelections');
                if (rows == null || rows == "") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要导出的数据！', 'warning');
                    return;
                }
                var OrderNoStr = "";
                for (var i = 0; i < rows.length; i++) {
                    OrderNoStr += "'" + rows[i].OrderNo + "',";
                }
                $.ajax({
                    url: "orderApi.aspx?method=QueryOrderPicekPieceExport&OrderNoStr=" + OrderNoStr,
                    success: function (text) {
                        if (text == "OK") { var obj = document.getElementById("<%=btnPicekPiece.ClientID %>"); obj.click() }
                        else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                    }
                });
            } else {
                var key = new Array();
                key[0] = $('#StartDate').datebox('getValue');
                key[1] = $('#EndDate').datebox('getValue');
                key[2] = $("#AHouseID").combobox('getValue');//区域大仓仓库ID
                key[3] = $("#PID").combobox('getValue');//仓库ID
                key[4] = $('#ASID').combobox('getValue');//产品类型ID
                $.ajax({
                    url: "orderApi.aspx?method=QuerymassExportOrder&key=" + escape(key.toString()),
                    success: function (text) {
                        if (text == "OK") { var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click(); }
                        else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                    }
                });
            }
        }

        //批量打印拣货单
        function massPrintOrder() {

            var hous = '<%= HouseName%>';
            var com = '湖南省湖南狄乐汽车服务有限公司发货单';
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要打印的数据！', 'warning');
                return;
            }
            var json = JSON.stringify(rows)
            $.ajax({
                url: 'orderApi.aspx?method=QueryOrderDataForMassPrint',
                type: 'post', dataType: 'json', data: { data: json },
                success: function (data) {
                    if (data == null || data == undefined || data.length < 1) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据！', 'warning');
                        return;
                    }
                    massPrint(data, rows[0].OutHouseName);
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
        //订单打印
        function massPrint(griddata, OutHouseName) {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            var nowdate = new Date();
            //LODOP.SET_PRINT_PAGESIZE(2, 0, 0, "A4");
            //LODOP.SET_SHOW_MODE("LANDSCAPE_DEFROTATED",1);
            //var com = (nowdate.getMonth() + 1) + "-" + nowdate.getDate() + "日" + griddata[0].ProductName + '仓库拣货单';
            //LODOP.PRINT_INITA(0, 0, 1024, 600, "");
            LODOP.SET_PRINT_PAGESIZE(0, 2100, 2970, "");

            var com = (nowdate.getMonth() + 1) + "-" + nowdate.getDate() + "日" + griddata[0].ProductName + '仓库拣货单';
            LODOP.PRINT_INITA(0, 0, 1024, 600, "");

            if (griddata[0].HouseID == "47") {
                com = (nowdate.getMonth() + 1) + "-" + nowdate.getDate() + "日富添盛仓库拣货单";
                LODOP.ADD_PRINT_TEXT(4, 255, 502, 33, com);
                LODOP.SET_PRINT_STYLEA(0, "FontName", "新宋体");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 19);
                LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                LODOP.ADD_PRINT_RECT(44, 11, 114, 27, 0, 1);
                LODOP.ADD_PRINT_RECT(44, 125, 99, 27, 0, 1);
                LODOP.ADD_PRINT_RECT(44, 224, 62, 27, 0, 1);
                LODOP.ADD_PRINT_RECT(44, 286, 48, 27, 0, 1);
                LODOP.ADD_PRINT_RECT(44, 334, 87, 27, 0, 1);
                LODOP.ADD_PRINT_RECT(44, 421, 72, 27, 0, 1);
                LODOP.ADD_PRINT_RECT(44, 493, 85, 27, 0, 1);
                LODOP.ADD_PRINT_RECT(44, 578, 78, 27, 0, 1);
                LODOP.ADD_PRINT_RECT(44, 655, 113, 27, 0, 1);
                LODOP.ADD_PRINT_TEXT(49, 17, 68, 27, "产品名称");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(49, 132, 58, 27, "规格");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(49, 236, 49, 27, "批次");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(49, 290, 50, 27, "数量");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(49, 337, 69, 27, "货位代码");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(49, 423, 66, 27, "所在区域");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(49, 495, 78, 27, "目的站");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(49, 582, 73, 27, "收件人");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(49, 662, 72, 27, "运单号");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

                var AllPiece = 0;//总数量
                var ltjson = [];
                for (var i = 0; i < griddata.length; i++) {
                    if (i == 0) {
                        ltjson[griddata[i].AreaName + ' ' + griddata[i].ProductName + ' ' + griddata[i].Specs + ' ' + griddata[i].Batch] = Number(griddata[i].Piece);
                    } else {
                        if (ltjson.hasOwnProperty(griddata[i].AreaName + ' ' + griddata[i].ProductName + ' ' + griddata[i].Specs + ' ' + griddata[i].GoodsCode + ' ' + griddata[i].Batch)) {
                            var n = Number(ltjson[griddata[i].AreaName + ' ' + griddata[i].ProductName + ' ' + griddata[i].Specs + ' ' + griddata[i].GoodsCode + ' ' + griddata[i].Batch]);
                            var total = n + Number(griddata[i].Piece);
                            ltjson[griddata[i].AreaName + ' ' + griddata[i].ProductName + ' ' + griddata[i].Specs + ' ' + griddata[i].GoodsCode + ' ' + griddata[i].Batch] = total;
                        } else {
                            ltjson[griddata[i].AreaName + ' ' + griddata[i].ProductName + ' ' + griddata[i].Specs + ' ' + griddata[i].GoodsCode + ' ' + griddata[i].Batch] = Number(griddata[i].Piece);
                        }
                    }
                    AllPiece += Number(griddata[i].Piece);
                    LODOP.ADD_PRINT_RECT(71 + i * 35, 11, 114, 35, 0, 1);
                    LODOP.ADD_PRINT_RECT(71 + i * 35, 125, 99, 35, 0, 1);
                    LODOP.ADD_PRINT_RECT(71 + i * 35, 224, 62, 35, 0, 1);
                    LODOP.ADD_PRINT_RECT(71 + i * 35, 286, 48, 35, 0, 1);
                    LODOP.ADD_PRINT_RECT(71 + i * 35, 334, 87, 35, 0, 1);
                    LODOP.ADD_PRINT_RECT(71 + i * 35, 421, 72, 35, 0, 1);
                    LODOP.ADD_PRINT_RECT(71 + i * 35, 493, 85, 35, 0, 1);
                    LODOP.ADD_PRINT_RECT(71 + i * 35, 578, 78, 35, 0, 1);
                    LODOP.ADD_PRINT_RECT(71 + i * 35, 655, 113, 35, 0, 1);
                    LODOP.ADD_PRINT_TEXT(76 + i * 35, 12, 107, 27, griddata[i].ProductName);
                    LODOP.ADD_PRINT_TEXT(76 + i * 35, 127, 95, 27, griddata[i].Specs);
                    LODOP.ADD_PRINT_TEXT(76 + i * 35, 227, 58, 27, griddata[i].Batch);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    if (griddata[i])
                        LODOP.ADD_PRINT_TEXT(76 + i * 35, 289, 53, 27, griddata[i].Piece);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(76 + i * 35, 339, 90, 27, griddata[i].ContainerCode);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(76 + i * 35, 423, 71, 27, griddata[i].AreaName);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(76 + i * 35, 497, 91, 27, griddata[i].Dest);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(76 + i * 35, 582, 79, 27, griddata[i].AcceptPeople);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(76 + i * 35, 660, 115, 27, griddata[i].LogisAwbNo);

                }

                //汇总
                var n = Number(griddata.length);
                for (var p in ltjson) {
                    n++;
                    LODOP.ADD_PRINT_RECT(60 + n * 35, 112, 590, 35, 0, 1);
                    LODOP.ADD_PRINT_TEXT(67 + n * 35, 116, 580, 35, p);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_RECT(60 + n * 35, 652, 50, 35, 0, 1);
                    LODOP.ADD_PRINT_TEXT(67 + n * 35, 658, 200, 35, ltjson[p]);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                }

                LODOP.ADD_PRINT_RECT(60 + (n + 1) * 35, 112, 590, 35, 0, 1);
                LODOP.ADD_PRINT_TEXT(67 + (n + 1) * 35, 116, 580, 35, '总计：');
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_RECT(60 + (n + 1) * 35, 652, 50, 35, 0, 1);
                LODOP.ADD_PRINT_TEXT(67 + (n + 1) * 35, 658, 200, 35, AllPiece);
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            } else {
                if (griddata[0].HouseID == "93") {
                    com = (nowdate.getMonth() + 1) + "-" + nowdate.getDate() + "日" + OutHouseName + '仓库拣货单';
                }
                LODOP.ADD_PRINT_TEXT(4, 255, 502, 33, com);
                LODOP.SET_PRINT_STYLEA(0, "FontName", "新宋体");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 19);
                LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                if (griddata[0].HouseID == "62") {
                    LODOP.ADD_PRINT_RECT(44, 13, 181, 27, 0, 1);
                    LODOP.ADD_PRINT_RECT(44, 194, 197, 27, 0, 1);
                } else {
                    LODOP.ADD_PRINT_RECT(44, 13, 111, 27, 0, 1);
                    LODOP.ADD_PRINT_RECT(44, 124, 127, 27, 0, 1);
                    LODOP.ADD_PRINT_RECT(44, 251, 141, 27, 0, 1);
                }
                LODOP.ADD_PRINT_RECT(44, 391, 112, 27, 0, 1);
                LODOP.ADD_PRINT_RECT(44, 502, 108, 27, 0, 1);
                LODOP.ADD_PRINT_RECT(44, 609, 105, 27, 0, 1);
                LODOP.ADD_PRINT_RECT(44, 713, 73, 27, 0, 1);
                LODOP.ADD_PRINT_RECT(44, 785, 83, 27, 0, 1);
                LODOP.ADD_PRINT_RECT(44, 867, 100, 27, 0, 1);
                LODOP.ADD_PRINT_RECT(44, 966, 118, 27, 0, 1);
                if (griddata[0].HouseID == "62") {
                    LODOP.ADD_PRINT_TEXT(49, 22, 58, 27, "背番");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(49, 227, 49, 27, "品番");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

                } else {
                    LODOP.ADD_PRINT_TEXT(49, 22, 58, 27, "规格");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(49, 157, 49, 27, "型号");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(49, 269, 50, 27, "花纹");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                }
                LODOP.ADD_PRINT_TEXT(49, 411, 51, 27, "批次");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(49, 532, 55, 27, "数量");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(49, 616, 83, 27, "货位代码");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(49, 716, 73, 27, "所在区域");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(49, 797, 72, 27, "目的站");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(49, 879, 68, 27, "收件人");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(49, 978, 72, 27, "运单号");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

                var AllPiece = 0;//总数量
                var ltjson = [];
                var ltjsonPackageNum = [];
                var ltjsonPackage = [];
                for (var i = 0; i < griddata.length; i++) {
                    if (i == 0) {
                        if (griddata[0].HouseID == "62") {
                            ltjson[griddata[i].AreaName + ' ' + griddata[i].Specs + ' ' + griddata[i].GoodsCode + ' ' + griddata[i].Batch] = Number(griddata[i].Piece);
                            ltjsonPackageNum[griddata[i].AreaName + ' ' + griddata[i].Specs + ' ' + griddata[i].GoodsCode + ' ' + griddata[i].Batch] = Number(griddata[i].PackageNum);
                            ltjsonPackage[griddata[i].AreaName + ' ' + griddata[i].Specs + ' ' + griddata[i].GoodsCode + ' ' + griddata[i].Batch] = griddata[i].Package;
                        } else {
                            ltjson[griddata[i].AreaName + ' ' + griddata[i].Specs + ' ' + griddata[i].Model + ' ' + griddata[i].Figure + ' ' + griddata[i].Batch] = Number(griddata[i].Piece);
                            ltjsonPackageNum[griddata[i].AreaName + ' ' + griddata[i].Specs + ' ' + griddata[i].Model + ' ' + griddata[i].Figure + ' ' + griddata[i].Batch] = Number(griddata[i].PackageNum);
                            ltjsonPackage[griddata[i].AreaName + ' ' + griddata[i].Specs + ' ' + griddata[i].Model + ' ' + griddata[i].Figure + ' ' + griddata[i].Batch] = griddata[i].Package;
                        }
                    } else {
                        if (griddata[0].HouseID == "62") {
                            if (ltjson.hasOwnProperty(griddata[i].AreaName + ' ' + griddata[i].Specs + ' ' + griddata[i].GoodsCode + ' ' + griddata[i].Batch)) {
                                var n = Number(ltjson[griddata[i].AreaName + ' ' + griddata[i].Specs + ' ' + griddata[i].GoodsCode + ' ' + griddata[i].Batch]);
                                var total = n + Number(griddata[i].Piece);
                                ltjson[griddata[i].AreaName + ' ' + griddata[i].Specs + ' ' + griddata[i].GoodsCode + ' ' + griddata[i].Batch] = total;
                                ltjsonPackageNum[griddata[i].AreaName + ' ' + griddata[i].Specs + ' ' + griddata[i].GoodsCode + ' ' + griddata[i].Batch] = Number(griddata[i].PackageNum);
                                ltjsonPackage[griddata[i].AreaName + ' ' + griddata[i].Specs + ' ' + griddata[i].GoodsCode + ' ' + griddata[i].Batch] = griddata[i].Package;
                            } else {
                                ltjson[griddata[i].AreaName + ' ' + griddata[i].Specs + ' ' + griddata[i].Model + ' ' + griddata[i].Batch] = Number(griddata[i].Piece);
                                ltjsonPackageNum[griddata[i].AreaName + ' ' + griddata[i].Specs + ' ' + griddata[i].Model + ' ' + griddata[i].Batch] = Number(griddata[i].PackageNum);
                                ltjsonPackage[griddata[i].AreaName + ' ' + griddata[i].Specs + ' ' + griddata[i].Model + ' ' + griddata[i].Batch] = griddata[i].Package;
                            }
                        } else {
                            if (ltjson.hasOwnProperty(griddata[i].AreaName + ' ' + griddata[i].Specs + ' ' + griddata[i].Model + ' ' + griddata[i].Figure + ' ' + griddata[i].Batch)) {
                                var n = Number(ltjson[griddata[i].AreaName + ' ' + griddata[i].Specs + ' ' + griddata[i].Model + ' ' + griddata[i].Figure + ' ' + griddata[i].Batch]);
                                var total = n + Number(griddata[i].Piece);
                                ltjson[griddata[i].AreaName + ' ' + griddata[i].Specs + ' ' + griddata[i].Model + ' ' + griddata[i].Figure + ' ' + griddata[i].Batch] = total;
                                ltjsonPackageNum[griddata[i].AreaName + ' ' + griddata[i].Specs + ' ' + griddata[i].Model + ' ' + griddata[i].Figure + ' ' + griddata[i].Batch] = Number(griddata[i].PackageNum);
                                ltjsonPackage[griddata[i].AreaName + ' ' + griddata[i].Specs + ' ' + griddata[i].Model + ' ' + griddata[i].Figure + ' ' + griddata[i].Batch] = griddata[i].Package;
                            } else {
                                ltjson[griddata[i].AreaName + ' ' + griddata[i].Specs + ' ' + griddata[i].Model + ' ' + griddata[i].Figure + ' ' + griddata[i].Batch] = Number(griddata[i].Piece);
                                ltjsonPackageNum[griddata[i].AreaName + ' ' + griddata[i].Specs + ' ' + griddata[i].Model + ' ' + griddata[i].Figure + ' ' + griddata[i].Batch] = Number(griddata[i].PackageNum);
                                ltjsonPackage[griddata[i].AreaName + ' ' + griddata[i].Specs + ' ' + griddata[i].Model + ' ' + griddata[i].Figure + ' ' + griddata[i].Batch] = griddata[i].Package;
                            }
                        }
                    }
                    AllPiece += Number(griddata[i].Piece);
                    if (griddata[0].HouseID == "62") {
                        LODOP.ADD_PRINT_RECT(71 + i * 27, 13, 181, 27, 0, 1);
                        LODOP.ADD_PRINT_RECT(71 + i * 27, 194, 197, 27, 0, 1);
                    } else {
                        LODOP.ADD_PRINT_RECT(71 + i * 27, 13, 111, 27, 0, 1);
                        LODOP.ADD_PRINT_RECT(71 + i * 27, 124, 127, 27, 0, 1);
                        LODOP.ADD_PRINT_RECT(71 + i * 27, 251, 141, 27, 0, 1);
                    }
                    LODOP.ADD_PRINT_RECT(71 + i * 27, 391, 112, 27, 0, 1);
                    LODOP.ADD_PRINT_RECT(71 + i * 27, 502, 108, 27, 0, 1);
                    LODOP.ADD_PRINT_RECT(71 + i * 27, 609, 105, 27, 0, 1);
                    LODOP.ADD_PRINT_RECT(71 + i * 27, 713, 73, 27, 0, 1);
                    LODOP.ADD_PRINT_RECT(71 + i * 27, 785, 83, 27, 0, 1);
                    LODOP.ADD_PRINT_RECT(71 + i * 27, 867, 100, 27, 0, 1);
                    LODOP.ADD_PRINT_RECT(71 + i * 27, 966, 118, 27, 0, 1);

                    if (griddata[0].HouseID == "62") {
                        LODOP.ADD_PRINT_TEXT(76 + i * 27, 17, 80, 27, griddata[i].Specs);
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(76 + i * 27, 196, 102, 27, griddata[i].GoodsCode);
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    } else {

                        LODOP.ADD_PRINT_TEXT(76 + i * 27, 17, 80, 27, griddata[i].Specs);
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(76 + i * 27, 126, 102, 27, griddata[i].Model);
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(76 + i * 27, 253, 100, 27, griddata[i].Figure);
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    }
                    LODOP.ADD_PRINT_TEXT(76 + i * 27, 407, 90, 27, griddata[i].Batch);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    var p = Number(griddata[i].Piece);
                    if (griddata[i].TypeParentID == 10 || griddata[i].TypeParentID == 133) {
                        if (griddata[i].PackageNum != 0) {
                            var num = p / griddata[i].PackageNum;
                            var b = (num + "").split(".");
                            if (b.length > 1) {
                                p = p + "/" + b[0] + griddata[i].Package + (p - b[0] * griddata[i].PackageNum);
                            } else {
                                p = p + "/" + b[0] + griddata[i].Package;
                            }
                        }
                    }
                    LODOP.ADD_PRINT_TEXT(76 + i * 27, 510, 90, 27, p);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(76 + i * 27, 616, 100, 27, griddata[i].ContainerCode);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(76 + i * 27, 720, 73, 27, griddata[i].AreaName);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(76 + i * 27, 797, 72, 27, griddata[i].Dest);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(76 + i * 27, 879, 68, 27, griddata[i].AcceptPeople);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(76 + i * 27, 973, 107, 27, griddata[i].LogisAwbNo);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                }
                var hous = '<%= HouseName%>';
                var com = '条';
                if (hous.indexOf('成都') != -1 || hous.indexOf('福州') != -1) {
                    com = '箱';
                }
                if (griddata[0].TypeParentID == 10) {
                    com = '';
                }
                //汇总
                var n = Number(griddata.length);
                for (var p in ltjson) {
                    n++;
                    LODOP.ADD_PRINT_RECT(80 + n * 27, 112, 540, 25, 0, 1);
                    LODOP.ADD_PRINT_TEXT(87 + n * 27, 116, 580, 25, p);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_RECT(80 + n * 27, 652, 100, 25, 0, 1);
                    var piece = ltjson[p];
                    if (ltjsonPackageNum[p] != 0) {
                        var num = ltjson[p] / ltjsonPackageNum[p];
                        var b = (num + "").split(".");
                        if (b.length > 1) {
                            piece = piece + "/" + b[0] + ltjsonPackage[p] + (piece - b[0] * ltjsonPackageNum[p]);
                        } else {
                            piece = piece + "/" + b[0] + ltjsonPackage[p];
                        }
                    }
                    LODOP.ADD_PRINT_TEXT(87 + n * 27, 658, 200, 25, piece);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                }

                LODOP.ADD_PRINT_RECT(80 + (n + 1) * 27, 112, 540, 25, 0, 1);
                LODOP.ADD_PRINT_TEXT(87 + (n + 1) * 27, 116, 580, 25, '总计：');
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_RECT(80 + (n + 1) * 27, 652, 100, 25, 0, 1);
                LODOP.ADD_PRINT_TEXT(87 + (n + 1) * 27, 658, 200, 25, AllPiece + com);
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            }
            //LODOP.PRINT_DESIGN();
            LODOP.PREVIEW();
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
        //修改订单跟踪状态
        function updateOrderStatus() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要查看跟踪状态的数据！', 'info');
                return;
            }
           <%-- if (row.OrderStatus == "5") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单：' + row.OrderNo + '已签收，不能修改', 'info');
                return;
            }--%>
          <%--  $(".OrderStatusTd").hide();
            var ln ="<%=UserInfor.LoginName%>";
            if (ln == "1000" || ln == "2065") {
                $(".OrderStatusTd").show();
            }
            if (row.LogisticName == "自送" || row.LogisticName == "自提") {
                $(".OrderStatusTd").show();
            }--%>
            $('#saveStatus').show();
            var IsModifyOrder = "<%=UserInfor.IsModifyOrder%>";
            if (IsModifyOrder == "0") {
                $('#saveStatus').hide();
            }
            if (row) {
                $('#dlgStatus').dialog('open').dialog('setTitle', '查看订单：' + row.OrderNo + ' 物流跟踪状态');
                $('#fmStatus').form('clear');
                $('#SOrderID').val(row.OrderID);
                $('#SOrderNo').val(row.OrderNo);
                $('#SWXOrderNo').val(row.WXOrderNo);
                $('#lblTrack').empty();
                $('#lblTrack2').empty();
                $.ajax({
                    async: false,
                    url: "orderApi.aspx?method=QueryOrderStatus&OrderNo=" + row.OrderNo + "&HouseID=" + row.HouseID + "&LogisAwbNo=" + row.LogisAwbNo + "&ThrowGood=" + row.ThrowGood + "&BelongHouse=" + row.BelongHouse + "&TrafficType=" + row.TrafficType + "&LogisID=" + row.LogisID,
                    cache: false,
                    dataType: "json",
                    success: function (text) {
                        var ldl = document.getElementById("lblTrack");
                        ldl.innerHTML = text.HtmlStr;
                        var ldl2 = document.getElementById("lblTrack2");
                        ldl2.innerHTML = text.HtmlStr2;
                        if (text.StartLatitude != "" && text.StartLatitude != null && text.StartLongitude != "" && text.StartLongitude != null && text.StartTime != "" && text.StartTime != null && text.EndLatitude != "" && text.EndLatitude != null && text.EndLongitude != "" && text.EndLongitude != null && text.EndTime != "" && text.EndTime != null) {
                            openMap(text.StartLatitude, text.StartLongitude, text.StartTime, text.EndLatitude, text.EndLongitude, text.EndTime, text.Status, text.Distance);
                        } else if (text.StartLatitude != "" && text.StartLatitude != null && text.StartLongitude != "" && text.StartLongitude != null && text.StartTime != "" && text.StartTime != null && text.EndLatitude != "" && text.EndLatitude != null && text.EndLongitude != "" && text.EndLongitude != null) {
                            openMap(text.StartLatitude, text.StartLongitude, text.StartTime, text.EndLatitude, text.EndLongitude, "", text.Status, text.Distance);
                        }
                    }
                });
            }
        }
        function restore() {
            var bounds = new TMap.LatLngBounds();

            //判断标注点是否在范围内
            markerArr.forEach(function (item) {
                //若坐标点不在范围内，扩大bounds范围
                if (bounds.isEmpty() || !bounds.contains(item.position)) {
                    bounds.extend(item.position);
                }
            })
            //设置地图可视范围
            map.fitBounds(bounds, {
                padding: 70 // 自适应边距
            });
        }
        function openMap(StartLat, StartLng, StartTime, EndLat, EndLng, EndTime, Status, Distance) {
            $("#map").css("width", "100%");
            $("#map").css("height", $("#dlgStatus").height() - $("#fmStatus").height() - 30);
            $('#map').append('<input type="button" id="restore" onclick="restore()" value="复位" style="right: 31px;height: 33px;width: 39px;top: 178px;z-index: 10000;background-color: #ffffff;border: none;border-radius: 3px;outline: none;position:absolute" />');
            var center = new TMap.LatLng(StartLat, StartLng);
            start = new TMap.LatLng(StartLat, StartLng);
            end = new TMap.LatLng(EndLat, EndLng);
            startTime = StartTime.substring(5, 16);
            endTime = EndTime == "" ? "" : EndTime.substring(5, 16);
            status = Status;
            distance = Distance;
            speed = parseInt(Distance / 3)
            var Datetime1 = new Date();
            var NewTime = Datetime1.getTime();
            var Datetime2 = new Date(StartTime);
            var OldTime = Datetime2.getTime();
            var Time = NewTime - OldTime;
            hour = parseInt(Time / 1000 / 60);//分钟
            //初始化地图
            map = new TMap.Map('map', {
                center: center,
                zoom: 17.5	//缩放级别
            });

            //WebServiceAPI请求URL（驾车路线规划默认会参考实时路况进行计算）
            var url = "https://apis.map.qq.com/ws/direction/v1/driving/"; //请求路径
            url += "?from=" + StartLat + "," + StartLng; //起点坐标
            url += "&to=" + EndLat + "," + EndLng;  //终点坐标
            url += "&output=jsonp&callback=cb";	//指定JSONP回调函数名，本例为cb
            url += "&key=CMZBZ-64RLS-7FROW-6SNUF-C3P5Z-DGB2J"; //开发key，可在控制台自助创建


            //发起JSONP请求，获取路线规划结果
            jsonp_request(url);
        }
        //浏览器调用WebServiceAPI需要通过Jsonp的方式，此处定义了发送JOSNP请求的函数
        function jsonp_request(url) {
            var script = document.createElement('script');
            script.src = url;
            document.body.appendChild(script);
        }

        //定义请求回调函数，在此拿到计算得到的路线，并进行绘制
        function cb(ret) {
            var coords = ret.result.routes[0].polyline, pl = [], path = [];
            //坐标解压（返回的点串坐标，通过前向差分进行压缩）
            var kr = 1000000;
            for (var i = 2; i < coords.length; i++) {
                coords[i] = Number(coords[i - 2]) + Number(coords[i]) / kr;
            }
            var position = hour < 2160 ? parseInt(coords.length / 2160 * hour) : parseInt(coords.length * 0.7) + hour;
            //将解压后的坐标放入点串数组pl中
            for (var i = 0; i < coords.length; i += 2) {
                if (status == 1) {

                }
                else if (status == 2) {
                    if (endTime == '' && i < position) {
                        path.push(new TMap.LatLng(coords[i], coords[i + 1]));
                    }
                }
                else if (status == 5) {
                    path.push(new TMap.LatLng(coords[i], coords[i + 1]));
                }
                pl.push(new TMap.LatLng(coords[i], coords[i + 1]));
            }
            markerArr = [{
                "id": 'start',
                "styleId": 'start',
                "position": start
            }];
            if (status == 1) {
            }
            else if (status == 2) {
                markerArr.push({
                    "id": 'end',
                    "styleId": 'end2',
                    "position": end
                }, {
                    id: 'car',
                    styleId: 'car-down',
                    position: start,
                });
            }
            else if (status == 5) {
                markerArr.push({
                    "id": 'end',
                    "styleId": 'end',
                    "position": end
                }, {
                    id: 'car',
                    styleId: 'car-down',
                    position: start,
                });
            }

            if (status > 1) {
                display_polyline(pl)//显示路线
            }

            //标记起终点marker
            var marker = new TMap.MultiMarker({
                id: 'marker-layer',
                map: map,
                styles: {
                    'car-down': new TMap.MarkerStyle({
                        'width': 40,
                        'height': 40,
                        'anchor': {
                            x: 20,
                            y: 20,
                        },
                        'faceTo': 'map',
                        'rotate': 180,
                        'src': 'https://mapapi.qq.com/web/lbs/javascriptGL/demo/img/car.png',
                    }),
                    "start": new TMap.MarkerStyle({
                        "width": 30,
                        "height": 45,
                        "anchor": { x: 16, y: 32 },
                        "src": 'https://i.loli.net/2021/03/22/1xa7ErotXd2li8I.png'
                    }),
                    "end": new TMap.MarkerStyle({
                        "width": 30,
                        "height": 45,
                        "anchor": { x: 16, y: 32 },
                        "src": 'https://i.loli.net/2021/03/22/HkaCIm4zFZvLYR5.png'
                    }),
                    "end2": new TMap.MarkerStyle({
                        "width": 30,
                        "height": 45,
                        "anchor": { x: 16, y: 32 },
                        "src": 'https://i.loli.net/2021/03/25/kYFrUsIAKfN9SWn.png'
                    })
                },
                geometries: markerArr
            });
            marker.moveAlong({ 'car': { path, speed: speed } }, { autoRotation: true });

            if (status > 1) {
                //初始化
                var bounds = new TMap.LatLngBounds();

                //判断标注点是否在范围内
                markerArr.forEach(function (item) {
                    //若坐标点不在范围内，扩大bounds范围
                    if (bounds.isEmpty() || !bounds.contains(item.position)) {
                        bounds.extend(item.position);
                    }
                })
                //设置地图可视范围
                map.fitBounds(bounds, {
                    padding: 70 // 自适应边距
                });
            }
            var geometries = [{
                'id': 'label1', //点图形数据的标志信息
                'styleId': 'style', //样式id
                'position': start, //标注点位置
                'content': startTime, //标注文本
                'properties': { //标注点的属性数据
                    'title': 'label'
                }
            }]
            if (status == 5) {
                geometries.push({
                    'id': 'label', //点图形数据的标志信息
                    'styleId': 'style', //样式id
                    'position': end, //标注点位置
                    'content': endTime, //标注文本
                    'properties': { //标注点的属性数据
                        'title': 'label'
                    }
                })
            }

            //初始化label
            var label = new TMap.MultiLabel({
                id: 'label-layer',
                map: map,
                styles: {
                    'style': new TMap.LabelStyle({
                        'color': '#d5371a', //颜色属性
                        'size': 18, //文字大小属性
                        'offset': { x: 0, y: 10 }, //文字偏移属性单位为像素
                        'angle': 0, //文字旋转属性
                        'alignment': 'center', //文字水平对齐属性
                        'verticalAlignment': 'middle' //文字垂直对齐属性
                    })
                },
                geometries: geometries
            });
        }

        function display_polyline(pl) {
            //创建 MultiPolyline显示折线
            var polylineLayer = new TMap.MultiPolyline({
                id: 'polyline-layer', //图层唯一标识
                map: map,//绘制到目标地图
                //折线样式定义
                styles: {
                    'style_blue': new TMap.PolylineStyle({
                        'color': '#3777FF', //线填充色
                        'width': 4, //折线宽度
                        'borderWidth': 5, //边线宽度
                        'borderColor': '#FFF', //边线颜色
                        'lineCap': 'round', //线端头方式
                    })
                },
                //折线数据定义
                geometries: [
                    {
                        'id': 'pl_1',//折线唯一标识，删除时使用
                        'styleId': 'style_blue',//绑定样式名
                        'paths': pl
                    }
                ]
            });
        }
        function destroy() {
            map.destroy();
        }
        function download(img) {
            var simg = img;
            $('#dgViewImg').dialog('open').dialog('setTitle', '预览');
            $("#simg").attr("src", simg);

        }
        //关闭订单状态跟踪弹出框
        function closeDlgStatus() {
            $('#dlgStatus').dialog('close');
            $('#dg').datagrid('reload');
            map.destroy();
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
                                $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功，是否打印发货单？', function (r) {
                                    if (r) { prePrint(); }//打印
                                });
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
        //新增出库数据
        function outOK() {
            var RuleType = $('#RuleType').val();
            var row = $('#outDg').datagrid('getSelected');
            var Total = Number(row.Piece);
            var SalePrice = $('#ActSalePrice').numberbox('getValue');// Number(row.SalePrice);//销售价
            //对轮胎产品进行价格管控
            if (row.TypeParentID == 1) {
                if (SalePrice == "0" || SalePrice == '' || SalePrice == undefined) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请输入业务员销售价格！', 'warning');
                    return;
                }
                if (Number(row.SalePrice) * 1.05 < Number(SalePrice)) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '业务员价高于系统销售价格5%', 'warning');
                }
                var IsModifyPrice = "<%=UserInfor.IsModifyPrice%>";
                if (IsModifyPrice == undefined || IsModifyPrice == "0") {
                    if (Number(SalePrice) < Number(row.SalePrice)) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '业务员价格不能低于销售价格！', 'warning');
                        return;
                    }
                }
            }
            var OutCargoID;
            var dgS = $('#dgSave').datagrid('getRows');
            for (var i = 0; i < dgS.length; i++) {
                OutCargoID = dgS[i].OutCargoID;
                if (dgS[i].ProductID == row.ProductID && dgS[i].ContainerID == row.ContainerID) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '该产品已在订单上，请直接修改数量操作！', 'warning');
                    return;
                }
            }
            var tCharge = $('#TransportFee').numberbox('getValue') == null || $('#TransportFee').numberbox('getValue') == "" ? 0 : Number($('#TransportFee').numberbox('getValue'));
            var Aindex = $('#InIndex').val();
            var indexD = $('#dgSave').datagrid('getRowIndex', row.ID);
            if (indexD < 0) {
                if (RuleType != "" && RuleType != null) {
                    row.RuleType = RuleType;
                    row.RuleID = $('#RuleID').val();
                    row.RuleTitle = $('#RuleTitle').val();
                } else {
                    row.RuleType = "";
                    row.RuleID = "";
                    row.RuleTitle = "";
                }
                row.Piece = Number($('#Numbers').numberbox('getValue'));
                row.ActSalePrice = SalePrice;
                row.OutCargoID = OutCargoID;
                row.OrderNo = $('#OrderNo').val();
                var json = JSON.stringify([row])
                //$('#dgSave').datagrid('appendRow', row);
                var p = $('#APiece').val() == "" || isNaN($('#APiece').val()) ? 0 : Number($('#APiece').val());
                var pt = p + Number($('#Numbers').numberbox('getValue'));
                $('#APiece').numberbox('setValue', Number(pt));
                var NC = SalePrice * Number($('#Numbers').numberbox('getValue'));
                $('#TransportFee').numberbox('setValue', tCharge + NC);
                $('#dlgOutCargo').dialog('close');
                if (Total != Number($('#Numbers').numberbox('getValue'))) {
                    row.Piece = Total - Number($('#Numbers').numberbox('getValue'));
                } else {
                    row.Piece = 0;
                }
                $('#outDg').datagrid('updateRow', { index: Aindex, row: row });


                $.messager.progress({ msg: '请稍后,拉上订单中...' });
                $.ajax({
                    url: 'orderApi.aspx?method=DrawUpOrder',
                    type: 'post', dataType: 'json', data: { data: json },
                    success: function (text) {
                        $.messager.progress("close");
                        if (text.Result == true) {
                            //刷新列表
                            $('#dgSave').datagrid('clearSelections');
                            var gridOpts = $('#dgSave').datagrid('options');
                            gridOpts.url = 'orderApi.aspx?method=QueryOrderByOrderNo';
                            $('#dgSave').datagrid('load', {
                                OrderNo: $('#OrderNo').val()
                            });
                        }
                        else {
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                        }
                    }
                });
            } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单列表已存在该产品，请先删除再添加！', 'warning'); }
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
            $('#dgSave').datagrid('loadData', { total: 0, rows: [] });
            $('#outDg').datagrid('loadData', { total: 0, rows: [] });
            IsModifyOrder = false;
            editIndex = undefined;
            var row = $("#dg").datagrid('getData').rows[Did];
            rowHouseID = row.HouseID;
            //$('#postpone').show();
            if (row) {
                if (row.HouseID == "47") {
                    $(".FTS").show();
                    $(".DLT").hide();
                    $("#AASpecs").html("名称:");
                    $("#AAFigure").html("编码:");
                    $('#ASpecs').textbox({ prompt: '请输入名称' });
                    $('#AFigure').textbox({ prompt: '请输入编码' });
                } else if (row.HouseID == "62") {
                    $("#AASpecs").html("背番:");
                    $('#ASpecs').textbox({ prompt: '请输入背番' });
                    $("td.AFigure").hide();
                    $("td.ABelongDepart").hide();
                    $("td.IsPrintPrice").hide();

                    $(".DLT").hide();
                    $(".FTS").hide();
                    $("td.APayClientNum").hide();
                    $("td.AAcceptUnit").hide();
                    $("td.AAcceptAddress").hide();
                    $("td.AAcceptTelephone").hide();
                    $("td.AAcceptCellphone").hide();
                    $("td.TransportFee").hide();
                    $("td.TransitFee").hide();
                    $("td.OtherFee").hide();
                    $("td.InsuranceFee").hide();
                    $("td.Rebate").hide();
                    $("td.TotalCharge").hide();
                    $("td.CheckOutType").hide();
                    $("td.ALogisID").hide();
                    $("td.DeliveryFee").hide();
                    $("td.TryeClientCode").hide();
                    $("td.SaleManID").hide();
                    $("td.ALogisAwbNo").hide();
                    $("td.ADeliverySettlement").hide();
                    $("td.ThrowID").hide();
                    $("td.Dep").hide();
                    $("td.Dest").hide();

                } else {
                    $(".FTS").hide();
                    $(".DLT").show();
                    $("#AASpecs").html("规格:");
                    $("#AAFigure").html("花纹:");
                    $('#ASpecs').textbox({ prompt: '请输入规格' });
                    $('#AFigure').textbox({ prompt: '请输入花纹' });
                }
                $('#dgSaveAwbStatus').val(row.AwbStatus);
                $('#dlgOrder').dialog('open').dialog('setTitle', '修改订单：' + row.OrderNo);
                $('#fmDep').form('clear');
                $('#PeriodsNum').next(".numberbox").hide();
                $("#PeriodsNumLab").css("visibility", "hidden");
                $("#PeriodsNum").numberbox('options').required = false;
                $("#PeriodsNum").numberbox('textbox').validatebox('options').required = false;
                $("#PeriodsNum").numberbox('validate');
                $('#HiddenAcceptPeople').val(row.AcceptPeople);
                $('#HiddenClientNum').val(row.ClientNum);

                //收货人
                if (row.HouseID == "47") {
                    $('#AAcceptPeople').combobox({
                        valueField: 'ADID', textField: 'AcceptPeople',
                        url: '../Client/clientApi.aspx?method=AutoCompleteClientAcceptPeople&ClientNum=' + row.ClientNum,
                        onSelect: onAcceptAddressChanged,
                        editable: false
                    });
                    $("#ADest").removeAttr("required");
                    $("#AAcceptPeople").removeAttr("required");
                } else {
                    $('#AAcceptPeople').combobox({
                        valueField: 'ADID', textField: 'AcceptPeople',
                        url: '../Client/clientApi.aspx?method=AutoCompleteClientAcceptPeople&ClientNum=' + row.ClientNum,
                        onSelect: onAcceptAddressChanged,
                        required: true,
                        editable: true
                    });
                    $('#ADest').combobox('textbox').validatebox('options').required = true;
                    $('#AAcceptPeople').combobox('textbox').validatebox('options').required = true;
                }
                bindMethod();
                //客户姓名
                //$('#InsuranceFee').combogrid({
                //    method: 'get', panelWidth: '150', idField: 'ID', textField: 'Money',
                //    url: 'orderApi.aspx?method=QueryMyCoupon&ClientNum=' + row.ClientNum,
                //    columns: [[{ field: 'Money', title: '券金额', width: 50 }, { field: 'UseStatus', title: '使用状态', width: 50, formatter: function (val, row, index) { if (val == "0") { return "未使用"; } } }]],
                //    fitColumns: true, onSelect: function (rowIndex, rowData) {
                //        $('#AInsuranceFee').val(Number(rowData.Money));
                //        var t = Number($('#TransitFee').numberbox('getValue')) + Number($('#TransportFee').numberbox('getValue')) + Number($('#OtherFee').numberbox('getValue')) - Number(rowData.Money);
                //        $('#TotalCharge').numberbox('setValue', Number(t).toFixed(2));
                //    }
                //});
                row.CreateDate = AllDateTime(row.CreateDate);
                console.log('row', row)
                $('#fmDep').form('load', row);
                if (row.CheckOutType == 1) {
                    $('#PeriodsNum').next(".numberbox").show();
                    $("#PeriodsNumLab").css("visibility", "visible");
                    $("#PeriodsNum").numberbox('options').required = true;
                    $("#PeriodsNum").numberbox('textbox').validatebox('options').required = true;
                    $("#PeriodsNum").numberbox('validate');
                    if (row.Remark.indexOf("周期") == 0) {
                        $('#HiddenRemark').val(row.Remark.replace(row.Remark.split('，')[0] + "，", ""));
                        $('#PeriodsNum').numberbox('setValue', row.Remark.substring(row.Remark.indexOf("周期") + 2, row.Remark.indexOf("天")));
                    }
                } else {
                    $('#HiddenRemark').val(row.Remark);
                }
                $('#HiddenOutHouseName').val(row.OutHouseName);
                $('#ThrowID').combobox('setText', row.TranHouse);
                $('#ThrwG').prop('checked', false);
                $('#Tran').prop('checked', false);
                $('#DaiFa').prop('checked', false);
                if (row.ThrowGood == "1") { $('#ThrwG').prop('checked', true); }
                else if (row.ThrowGood == "2") { $('#Tran').prop('checked', true); }
                else if (row.ThrowGood == "3") { $('#DaiFa').prop('checked', true); }
                if (row.IsPrintPrice == "1") { $('#IsPrintPrice').prop('checked', true); } else { $('#IsPrintPrice').prop('checked', false); }

                $('#payprint').show();
                $('#pickprint').show();
                var columns = [];
                columns.push({ title: '', field: 'ID', checkbox: true, width: '30px' });

                if (row.ModifyPriceStatus == "1") {
                    //灰掉按钮
                    $('#up').hide();
                    $('#save').hide();
                    //$('#payprint').hide();
                    $('#pickprint').hide();
                }

                if (RoleCName.indexOf("安泰路斯") >= 0) {
                    $(".DLT").hide();
                    $(".FTS").hide();
                    $("#SuPeiLab").show();
                    $("#SuPei").show();
                    $('#up').hide();
                    $("#SaleManID").combobox("readonly", true);
                    $('#SaleManID').combobox('textbox').unbind('focus');
                    $('.ThrowID').hide();
                    $('#pickprint').hide();
                    $('#payprint').hide();
                    $('.ADeliverySettlement').hide();
                    //客户姓名
                    $('#APayClientNum').combobox({
                        //$('#AcceptPeople').combobox({
                        valueField: 'ClientNum', textField: 'Boss',
                        url: '../Client/clientApi.aspx?method=AutoCompleteClient&ClientNums=936982,787337',
                        onSelect: onClientChanged,
                        required: true
                    });
                }
                else if (RoleCName.indexOf("汕头科矿") >= 0) {
                    $(".DLT").hide();
                    $(".FTS").hide();
                    $("td.ThrowID").hide();
                    $("td.Dep").hide();
                    $("td.Dest").hide();
                    $("#SaleManID").combobox("readonly", true);
                    $("#ALogisAwbNo").combobox("readonly", true);
                    $("#ADeliverySettlement").combobox("readonly", true);
                    $("#ALogisID").combobox("readonly", true);
                    $("#DeliveryFee").combobox("readonly", true);
                    //客户姓名
                    $('#APayClientNum').combobox({
                        //$('#AcceptPeople').combobox({
                        valueField: 'ClientNum', textField: 'Boss',
                        url: '../Client/clientApi.aspx?method=AutoCompleteClient&&UpClientID=8',
                        onSelect: onClientChanged,
                        required: true
                    });
                }
                else {
                    //客户名称
                    $('#APayClientNum').combobox({
                        valueField: 'ClientNum', textField: 'Boss',
                        url: '../Client/clientApi.aspx?method=AutoCompleteClient&houseID=' + row.HouseID,
                        onSelect: onClientChanged
                    });
                }
                TrafficType = row.TrafficType;
                if (TrafficType == "2") {
                    alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '采购订单无法修改!', 'info');
                    $("#up").hide();
                    $("#save").hide();
                }
                if (row.HouseID == "47") {
                    if (row.ModifyPriceStatus == "1") {
                        columns.push({ title: '出库数量', field: 'Piece', width: '60px' });
                        columns.push({ title: '业务员价', field: 'ActSalePrice', width: '60px' });
                    } else {
                        columns.push({ title: '出库数量', field: 'Piece', width: '60px', editor: { type: 'numberbox' } });
                        columns.push({ title: '业务员价', field: 'ActSalePrice', width: '60px', editor: { type: 'numberbox', options: { precision: 2 } } });
                    }
                    columns.push({
                        title: '产品名称', field: 'ProductName', width: '110px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({ title: '富添盛编码', field: 'GoodsCode', width: '70px' });
                    columns.push({ title: '商贸编码', field: 'Model', width: '110px' });
                    columns.push({ title: '长和编码', field: 'Figure', width: '110px' });
                    columns.push({ title: '祺航编码', field: 'Size', width: '110px' });
                    columns.push({ title: '批次', field: 'Batch', width: '50px' });
                    columns.push({ title: '品牌', field: 'TypeName', width: '60px' });
                    columns.push({ title: '单位', field: 'Package', width: '60px' });
                    columns.push({ title: '备注', field: 'RuleTitle', width: '100px' });
                } else if (row.HouseID == "62") {
                    columns.push({ title: '出库数量', field: 'Piece', width: '60px' });
                    columns.push({ title: '品番', field: 'GoodsCode', width: '160px' });
                    columns.push({ title: '背番', field: 'Specs', width: '160px' });
                    columns.push({ title: '批次', field: 'Batch', width: '90px' });
                    columns.push({ title: '品牌', field: 'TypeName', width: '120px' });

                } else {
                    if (row.ModifyPriceStatus == "1") {
                        columns.push({ title: '出库数量', field: 'Piece', width: '60px' });
                        columns.push({ title: '业务员价', field: 'ActSalePrice', width: '60px' });
                    } else {
                        columns.push({ title: '出库数量', field: 'Piece', width: '60px', editor: { type: 'numberbox' } });
                        columns.push({ title: '业务员价', field: 'ActSalePrice', width: '60px', editor: { type: 'numberbox', options: { precision: 2 } } });
                    }
                    columns.push({ title: '型号', field: 'Model', width: '60px' });
                    columns.push({ title: '规格', field: 'Specs', width: '80px' });
                    columns.push({ title: '花纹', field: 'Figure', width: '90px' });
                    columns.push({ title: '批次', field: 'Batch', width: '50px' });
                    columns.push({ title: '速级', field: 'SpeedLevel', width: '50px' });
                    columns.push({ title: '载重', field: 'LoadIndex', width: '50px' });
                    columns.push({ title: '品牌', field: 'TypeName', width: '60px' });
                    columns.push({ title: '货品代码', field: 'GoodsCode', width: '60px' });
                    columns.push({ title: '产品来源', field: 'SourceName', width: '70px' });
                    columns.push({ title: '开单品牌', field: 'ShowTypeName', width: '60px' });
                    columns.push({ title: '开单编码', field: 'ShowGoodsCode', width: '70px' });
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
                }
                columns.push({ title: '货位代码', field: 'ContainerCode', width: '80px' });
                columns.push({ title: '所在区域', field: 'AreaName', width: '60px' });
                columns.push({ title: '所在仓库', field: 'FirstAreaName', width: '80px' });
                columns.push({ title: '产品ID', field: 'ProductID', width: '60px' });
                columns.push({ title: '包装', field: 'Package', hidden: true });
                columns.push({ title: '包装数量', field: 'PackageNum', hidden: true });
                columns.push({ title: '优惠规则类型', field: 'RuleType', hidden: true });
                columns.push({ title: '优惠规则ID', field: 'RuleID', hidden: true });
                columns.push({ title: '优惠规则名称', field: 'RuleTitle', hidden: true });
                $("#APayClientNum").combobox("setValue", row.PayClientNum);
                showGrid(columns, row.HouseID);

                var gridOpts = $('#dgSave').datagrid('options');
                gridOpts.url = 'orderApi.aspx?method=QueryOrderByOrderNo&OrderNo=' + row.OrderNo;
                //所在仓库
                $('#AHID').combobox({
                    url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                    valueField: 'HouseID', textField: 'Name',
                    onSelect: function (rec) {
                        $('#HID').combobox('clear');
                        var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                        $('#HID').combobox('reload', url);
                    }
                });
                $('#AHID').combobox('setValue', rowHouseID);
                $('#HID').combobox('clear');
                var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rowHouseID;
                $('#HID').combobox('reload', url);
                if (RoleCName.indexOf("汕头科矿") >= 0) {
                    $('#HID').combobox('setText', '<%=UserInfor.HeadHouseName%>');
                    $('#HID').combobox('setValue', '<%=UserInfor.HeadHouseID%>');

                    $("#HID").combobox("readonly", true);
                    $('#HID').combobox('textbox').unbind('focus');
                }
                //保存限制订单类型为OES的仓库ID
                var arr = [13, 14, 15, 23, 30];
                if ($.inArray(rowHouseID, arr) > -1) {
                    //$('#OES').prop('checked', true);
                    $(".DLT").prop("disabled", true);
                } else {
                    //$('#OES').prop('checked', false);
                    $(".DLT").prop("disabled", false);
                }
            }
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

        $.extend($.fn.datagrid.methods, {
            editCell: function (jq, param) {
                return jq.each(function () {
                    var fields = $(this).datagrid('getColumnFields');
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
        });
        var IsModifyOrder = false;
        var editIndex = undefined;
        function endEditing() {
            if (editIndex == undefined) { return true }
            if ($('#dgSave').datagrid('validateRow', editIndex)) {
                var rows = $("#dgSave").datagrid('getData').rows[editIndex];
                var ed = $('#dgSave').datagrid('getEditors', editIndex);
                var cg = ed[0];
                if (cg == undefined) { return false; }
                var sum = 0;
                if (cg.field == "Piece") {
                    //修改数量
                    var oldPiece = Number(rows.Piece);
                    var salePrice = Number(rows.ActSalePrice);
                    var newPiece = Number(cg.target.val());
                    if (oldPiece == newPiece) {
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        return;
                    }

                    var row = $('#dgSave').datagrid('getRows').length;
                    var count = 0;
                    for (var i = 0; i < row; i++) {
                        if (i == editIndex) {
                            count = Number(count) + newPiece;
                        } else {
                            count = Number(count) + Number($("#dgSave").datagrid('getData').rows[i].Piece);
                        }
                    }
                    if (count == 0) {
                        rows.Piece = oldPiece;
                        $('#dgSave').datagrid('refreshRow', editIndex);
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单出库总数量必须大于0!', 'info');
                        return;
                    }
                    //修改数量
                    rows.Piece = newPiece;
                    rows.oldPiece = oldPiece;
                    rows.OrderNo = $('#OrderNo').val();
                    if (rows.RuleType.indexOf('4') != "-1") {
                        var clientNum = $('#ClientNum').val();
                        $.ajax({
                            url: "orderApi.aspx?method=QueryPriceRuleBankInfoToID&RuleID=" + rows.RuleID + "&HouseID=" + rows.HouseID + "&TypeID=" + rows.TypeID + "&Specs=" + encodeURIComponent(rows.Specs) + "&Figure=" + encodeURIComponent(rows.Figure) + "&Batch=" + rows.Batch + "&ClientNum=" + clientNum + "&OrderNo=" + rows.OrderNo + "&RuleType=4",
                            cache: false,
                            async: false,
                            dataType: "json",
                            success: function (text) {
                                if (text.Result == true) {
                                    if (text.RuleContent < rows.Piece) {
                                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '当前收货人此产品限购' + text.RuleContent + '条', 'warning');
                                        rows.Piece = oldPiece;
                                        rows.oldPiece = oldPiece;
                                        $('#dgSave').datagrid('updateRow', { index: editIndex, row: rows });
                                        return;
                                    } else {
                                        var json = JSON.stringify([rows])
                                        $.messager.progress({ msg: '请稍后,正在保存中...' });
                                        $.ajax({
                                            url: 'orderApi.aspx?method=UpdateOrderPiece',
                                            type: 'post', dataType: 'json', data: { data: json },
                                            success: function (text) {
                                                //var result = eval('(' + msg + ')');
                                                $.messager.progress("close");
                                                if (text.Result == true) {
                                                    IsModifyOrder = true;
                                                    var ModifyPiece = 0, ModifyPrice = 0;
                                                    //说明是增加了数量
                                                    if (newPiece > oldPiece) {
                                                        ModifyPiece = newPiece - oldPiece;
                                                        ModifyPrice = ModifyPiece * salePrice;
                                                        var TPiece = Number($('#APiece').numberbox('getValue'));
                                                        $('#APiece').numberbox('setValue', Number(TPiece + ModifyPiece));
                                                        var TFee = Number($('#TransportFee').numberbox('getValue'));
                                                        $('#TransportFee').numberbox('setValue', Number(TFee + ModifyPrice).toFixed(2));
                                                        qh();
                                                    } else {
                                                        ModifyPiece = oldPiece - newPiece;
                                                        ModifyPrice = ModifyPiece * salePrice;

                                                        var TPiece = Number($('#APiece').numberbox('getValue'));
                                                        $('#APiece').numberbox('setValue', Number(TPiece - ModifyPiece));
                                                        var TFee = Number($('#TransportFee').numberbox('getValue'));
                                                        $('#TransportFee').numberbox('setValue', Number(TFee - ModifyPrice).toFixed(2));
                                                        qh();
                                                    }
                                                    alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                                                }
                                                else {
                                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                                    $('#dgSave').datagrid('rejectChanges');
                                                    editIndex = undefined;
                                                }
                                            }
                                        });
                                    }
                                } else {
                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.RuleContent + '！', 'warning');
                                    rows.Piece = oldPiece;
                                    rows.oldPiece = oldPiece;
                                    return;
                                }
                            }
                        });
                    } else {
                        var json = JSON.stringify([rows])
                        $.messager.progress({ msg: '请稍后,正在保存中...' });
                        $.ajax({
                            url: 'orderApi.aspx?method=UpdateOrderPiece',
                            type: 'post', dataType: 'json', data: { data: json },
                            success: function (text) {
                                //var result = eval('(' + msg + ')');
                                $.messager.progress("close");
                                if (text.Result == true) {
                                    IsModifyOrder = true;
                                    var ModifyPiece = 0, ModifyPrice = 0;
                                    //说明是增加了数量
                                    if (newPiece > oldPiece) {
                                        ModifyPiece = newPiece - oldPiece;
                                        ModifyPrice = ModifyPiece * salePrice;
                                        var TPiece = Number($('#APiece').numberbox('getValue'));
                                        $('#APiece').numberbox('setValue', Number(TPiece + ModifyPiece));
                                        var TFee = Number($('#TransportFee').numberbox('getValue'));
                                        $('#TransportFee').numberbox('setValue', Number(TFee + ModifyPrice).toFixed(2));
                                        qh();
                                    } else {
                                        ModifyPiece = oldPiece - newPiece;
                                        ModifyPrice = ModifyPiece * salePrice;

                                        var TPiece = Number($('#APiece').numberbox('getValue'));
                                        $('#APiece').numberbox('setValue', Number(TPiece - ModifyPiece));
                                        var TFee = Number($('#TransportFee').numberbox('getValue'));
                                        $('#TransportFee').numberbox('setValue', Number(TFee - ModifyPrice).toFixed(2));
                                        qh();
                                    }
                                    alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                                }
                                else {
                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                    $('#dgSave').datagrid('rejectChanges');
                                    editIndex = undefined;
                                }
                            }
                        });
                    }
                }
                if (cg.field == "ActSalePrice") {
                    var oldPrice = Number(rows.ActSalePrice);//旧价格
                    var piece = Number(rows.Piece);//新数量
                    var newPrice = Number(cg.target.val());//新价格
                    if (oldPrice == newPrice) {
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        return;
                    }

                    var row = $('#dgSave').datagrid('getRows').length;
                    var count = 0;
                    for (var i = 0; i < row; i++) {
                        count = Number(count) + Number($("#dgSave").datagrid('getData').rows[i].Piece);
                    }
                    if (count == 0) {
                        rows.Piece = oldPiece;
                        $('#dgSave').datagrid('refreshRow', editIndex);
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单出库总数量必须大于0!', 'info');
                        return;
                    }
                    var salePrice = Number(rows.SalePrice);
                    var IsModifyPrice = "<%=UserInfor.IsModifyPrice%>";
                    if (IsModifyPrice == undefined || IsModifyPrice == "0") {
                        if (rows.houseID == 64) {
                            if (newPrice * 1 < Number(rows.CostPrice) * 1) {
                                $('#dgSave').datagrid('endEdit', editIndex);
                                editIndex = undefined;
                                rows.ActSalePrice = oldPrice;
                                $('#dgSave').datagrid('refreshRow', index);
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', "业务员价格不能低于成本价!", 'warning');
                                return;
                            }
                        } else {
                            if (newPrice * 1 < salePrice * 1) {
                                $('#dgSave').datagrid('endEdit', editIndex);
                                editIndex = undefined;
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', "业务员价格不能低于销售价格!", 'warning');
                                return;
                            }
                        }
                    }
                    rows.SalePrice = oldPrice;
                    rows.ActSalePrice = cg.target.val();
                    rows.OrderNo = $('#OrderNo').val();
                    var json = JSON.stringify([rows])
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    $.ajax({
                        url: 'orderApi.aspx?method=UpdateOrderSalePrice',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                IsModifyOrder = true;
                                var ModifyPiece = 0, ModifyPrice = 0;
                                //说明是增加价格
                                if (newPrice > oldPrice) {
                                    ModifyPrice = newPrice - oldPrice;
                                    ModifyPiece = ModifyPrice * piece;
                                    var TFee = Number($('#TransportFee').numberbox('getValue'));
                                    $('#TransportFee').numberbox('setValue', Number(TFee + ModifyPiece).toFixed(2));
                                    qh();
                                } else {
                                    ModifyPrice = oldPrice - newPrice;
                                    ModifyPiece = ModifyPrice * piece;

                                    var TFee = Number($('#TransportFee').numberbox('getValue'));
                                    $('#TransportFee').numberbox('setValue', Number(TFee - ModifyPiece).toFixed(2));
                                    qh();
                                }
                                alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                $('#dgSave').datagrid('rejectChanges');
                                editIndex = undefined;
                            }
                        }
                    });
                }
                $('#dgSave').datagrid('endEdit', editIndex);
                editIndex = undefined;
                return true;
            } else {
                return false;
            }
        }
        function onClickCell(index, field) {
            var rows = $("#dgSave").datagrid('getData').rows[index];
            if (TrafficType == "2") { return; }
            if ($('#dgSaveAwbStatus').val() * 1 < 1 && field == "Piece" || field == "ActSalePrice") {
                if (endEditing()) {
                    $('#dgSave').datagrid('selectRow', index)
                        .datagrid('editCell', { index: index, field: field });
                    editIndex = index;
                }
            } else {
                if (editIndex == undefined) { return true }
                var ed = $('#dgSave').datagrid('getEditors', editIndex);
                var cg = ed[0];
                if (cg == undefined) {
                    return true;
                }
                var sum = 0;
                if (cg.field == "Piece") {
                    var oldPiece = Number(rows.Piece);
                    var salePrice = Number(rows.ActSalePrice);
                    var newPiece = Number(cg.target.val());
                    if (oldPiece == newPiece) {
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        return;
                    }
                    //修改数量
                    rows.Piece = newPiece;
                    rows.oldPiece = oldPiece;
                    rows.OrderNo = $('#OrderNo').val();

                    var row = $('#dgSave').datagrid('getRows').length;
                    var count = 0;
                    for (var i = 0; i < row; i++) {
                        count = Number(count) + Number($("#dgSave").datagrid('getData').rows[i].Piece);
                    }
                    if (count == 0) {
                        rows.Piece = oldPiece;
                        $('#dgSave').datagrid('refreshRow', index);
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单出库总数量必须大于0!', 'info');
                        return;
                    }
                    if (rows.RuleType.indexOf('4') != "-1") {
                        var clientNum = $('#ClientNum').val();
                        $.ajax({
                            url: "orderApi.aspx?method=QueryPriceRuleBankInfoToID&RuleID=" + rows.RuleID + "&HouseID=" + rows.HouseID + "&TypeID=" + rows.TypeID + "&Specs=" + encodeURIComponent(rows.Specs) + "&Figure=" + encodeURIComponent(rows.Figure) + "&Batch=" + rows.Batch + "&ClientNum=" + clientNum + "&OrderNo=" + $('#OrderNo').val() + "&RuleType=4",
                            cache: false,
                            async: false,
                            dataType: "json",
                            success: function (text) {
                                if (text.Result == true) {
                                    if (text.RuleContent < newPiece) {
                                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '当前收货人此产品限购' + text.RuleContent + '条', 'warning');
                                        rows.Piece = oldPiece;
                                        rows.oldPiece = oldPiece;
                                        //$('#dgSave').datagrid('updateRow', {
                                        //    index: index
                                        //});
                                        $('#dgSave').datagrid('updateRow', { index: index, row: rows });
                                        return;
                                    } else {
                                        var json = JSON.stringify([rows])
                                        $.messager.progress({ msg: '请稍后,正在保存中...' });
                                        $.ajax({
                                            url: 'orderApi.aspx?method=UpdateOrderPiece',
                                            type: 'post', dataType: 'json', data: { data: json },
                                            success: function (text) {
                                                $.messager.progress("close");
                                                //var result = eval('(' + msg + ')');
                                                if (text.Result == true) {
                                                    IsModifyOrder = true;
                                                    var ModifyPiece = 0, ModifyPrice = 0;
                                                    //说明是增加了数量
                                                    if (newPiece > oldPiece) {
                                                        ModifyPiece = newPiece - oldPiece;
                                                        ModifyPrice = ModifyPiece * salePrice;
                                                        var TPiece = Number($('#APiece').numberbox('getValue'));
                                                        $('#APiece').numberbox('setValue', Number(TPiece + ModifyPiece));
                                                        var TFee = Number($('#TransportFee').numberbox('getValue'));
                                                        $('#TransportFee').numberbox('setValue', Number(TFee + ModifyPrice).toFixed(2));
                                                        qh();
                                                    } else {

                                                        ModifyPiece = oldPiece - newPiece;
                                                        ModifyPrice = ModifyPiece * salePrice;

                                                        var TPiece = Number($('#APiece').numberbox('getValue'));
                                                        $('#APiece').numberbox('setValue', Number(TPiece - ModifyPiece));
                                                        var TFee = Number($('#TransportFee').numberbox('getValue'));
                                                        $('#TransportFee').numberbox('setValue', Number(TFee - ModifyPrice).toFixed(2));
                                                        qh();
                                                    }
                                                    alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                                                }
                                                else {
                                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                                    $('#dgSave').datagrid('rejectChanges');
                                                    editIndex = undefined;
                                                }
                                            }
                                        });
                                    }
                                } else {
                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.RuleContent + '！', 'warning');
                                    rows.Piece = oldPiece;
                                    rows.oldPiece = oldPiece;
                                    return;
                                }
                            }
                        });
                    } else {
                        var json = JSON.stringify([rows])
                        $.messager.progress({ msg: '请稍后,正在保存中...' });
                        $.ajax({
                            url: 'orderApi.aspx?method=UpdateOrderPiece',
                            type: 'post', dataType: 'json', data: { data: json },
                            success: function (text) {
                                $.messager.progress("close");
                                //var result = eval('(' + msg + ')');
                                if (text.Result == true) {
                                    IsModifyOrder = true;
                                    var ModifyPiece = 0, ModifyPrice = 0;
                                    //说明是增加了数量
                                    if (newPiece > oldPiece) {
                                        ModifyPiece = newPiece - oldPiece;
                                        ModifyPrice = ModifyPiece * salePrice;
                                        var TPiece = Number($('#APiece').numberbox('getValue'));
                                        $('#APiece').numberbox('setValue', Number(TPiece + ModifyPiece));
                                        var TFee = Number($('#TransportFee').numberbox('getValue'));
                                        $('#TransportFee').numberbox('setValue', Number(TFee + ModifyPrice).toFixed(2));
                                        qh();
                                    } else {

                                        ModifyPiece = oldPiece - newPiece;
                                        ModifyPrice = ModifyPiece * salePrice;

                                        var TPiece = Number($('#APiece').numberbox('getValue'));
                                        $('#APiece').numberbox('setValue', Number(TPiece - ModifyPiece));
                                        var TFee = Number($('#TransportFee').numberbox('getValue'));
                                        $('#TransportFee').numberbox('setValue', Number(TFee - ModifyPrice).toFixed(2));
                                        qh();
                                    }
                                    alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                                }
                                else {
                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                    $('#dgSave').datagrid('rejectChanges');
                                    editIndex = undefined;
                                }
                            }
                        });
                    }
                }
                if (cg.field == "ActSalePrice") {
                    //修改销售价
                    var oldPrice = Number(rows.ActSalePrice);//旧价格
                    var piece = Number(rows.Piece);//新数量
                    var newPrice = Number(cg.target.val());//新价格
                    if (oldPrice == newPrice) {
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        return;
                    }

                    if (count == 0) {
                        rows.Piece = oldPiece;
                        $('#dgSave').datagrid('refreshRow', index);
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单出库总数量必须大于0!', 'info');
                        return;
                    }

                    var row = $('#dgSave').datagrid('getRows').length;
                    var count = 0;
                    for (var i = 0; i < row; i++) {
                        count = Number(count) + Number($("#dgSave").datagrid('getData').rows[i].Piece);
                    }
                    var salePrice = Number(rows.SalePrice);
                    var IsModifyPrice = "<%=UserInfor.IsModifyPrice%>";
                    if (IsModifyPrice == undefined || IsModifyPrice == "0") {
                        if (rows.HouseID == 64) {
                            if (newPrice * 1 < Number(rows.CostPrice) * 1) {
                                $('#dgSave').datagrid('endEdit', editIndex);
                                editIndex = undefined;
                                rows.ActSalePrice = oldPrice;
                                $('#dgSave').datagrid('refreshRow', index);
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', "业务员价格不能低于成本价!", 'warning');
                                return;
                            }
                        } else {
                            if (newPrice * 1 < salePrice * 1) {
                                $('#dgSave').datagrid('endEdit', editIndex);
                                editIndex = undefined;
                                rows.ActSalePrice = oldPrice;
                                $('#dgSave').datagrid('refreshRow', index);
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', "业务员价格不能低于销售价格!", 'warning');
                                return;
                            }
                        }
                    }
                    rows.SalePrice = oldPrice;
                    rows.ActSalePrice = cg.target.val();
                    rows.OrderNo = $('#OrderNo').val();
                    var json = JSON.stringify([rows])
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    $.ajax({
                        url: 'orderApi.aspx?method=UpdateOrderSalePrice',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                IsModifyOrder = true;
                                var ModifyPiece = 0, ModifyPrice = 0;
                                //说明是增加价格
                                if (newPrice > oldPrice) {
                                    ModifyPrice = newPrice - oldPrice;
                                    ModifyPiece = ModifyPrice * piece;
                                    var TFee = Number($('#TransportFee').numberbox('getValue'));
                                    $('#TransportFee').numberbox('setValue', Number(TFee + ModifyPiece).toFixed(2));
                                    qh();
                                } else {
                                    ModifyPrice = oldPrice - newPrice;
                                    ModifyPiece = ModifyPrice * piece;

                                    var TFee = Number($('#TransportFee').numberbox('getValue'));
                                    $('#TransportFee').numberbox('setValue', Number(TFee - ModifyPiece).toFixed(2));
                                    qh();
                                }
                                alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                $('#dgSave').datagrid('rejectChanges');
                                editIndex = undefined;
                            }
                        }
                    });
                }

                $('#dgSave').datagrid('endEdit', editIndex);
                editIndex = undefined;
            }
        }
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
        //业务员选择方法
        function onSaleManIDChanged(item) {
            if (item) {
                $('#SaleManName').val(item.UserName);
                $('#SaleCellPhone').val(item.CellPhone);
            }
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
                        $('#ADeliveryDriverName').textbox('setValue', $.trim(text.DeliveryDriverName));
                        $('#ADriverCellphone').textbox('setValue', $.trim(text.DriverCellphone));

                    }
                });
            }
        }

        //输入提货信息
        function addDeliveryDriver() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要输入提货信息的数据！', 'warning');
                return;
            }
            if (row.CreateAwbID != "<%=UserInfor.LoginName%>") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '只允许开单员上传！', 'warning');
                return;
            }
            if (row) {
                $('#dlgDeliveryDriver').dialog('open').dialog('setTitle', row.OrderNo + ' 输入提货信息');
                $('#fmdlgDeliveryDriver').form('clear');
                $('#DeliveryDriverName').textbox('setValue', row.DeliveryDriverName);
                $('#DriverCarNum').textbox('setValue', row.DriverCarNum);
                $('#DriverCellphone').numberbox('setValue', row.DriverCellphone);
                $('#DriverIDNum').textbox('setValue', row.DriverIDNum);
                $('#DOrderID').val(row.OrderID);
                $('#DOrderNo').val(row.OrderNo);

                showdgInsurcePicture();
                $('#dgInsurcePicture').datagrid('clearSelections');
                var gridOpts = $('#dgInsurcePicture').datagrid('options');
                gridOpts.url = 'orderApi.aspx?method=QueryDeliveryOrderStatus&OrderNo=' + row.OrderNo + '&OrderStatus=10';
            }
        }

        //曾经上传的来货订单照片
        function showdgInsurcePicture() {
            $('#dgInsurcePicture').datagrid({
                width: '100%',
                height: '180px',
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                rownumbers: true, //行号
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                //ctrlSelect:true,
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '',
                columns: [[
                    { title: '', field: 'ID', checkbox: true, width: '30px' },
                    { title: '单据照片', field: 'SignImage', width: '250px', formatter: imgFormatter },
                    //{ title: '证件类型', field: 'FileType', width: '100px', formatter: function (val, row, index) { if (val == "0") { return "身份证正面"; } else if (val == "1") { return "身份证反面"; } else if (val == "2") { return "营业执照"; } else if (val == "3") { return "签订合同"; } else if (val == "4") { return "门头照片"; } else { return ""; } } },
                    { title: '上传时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter }
                ]]
            });

        }
        //图片添加路径  
        function imgFormatter(value, row, index) {
            if ('' != value && null != value) {
                var rvalue = "";
                rvalue += "<img onclick=download(\"" + value + "\") style='width:66px; height:60px;margin-left:1px;' src='" + value + "' title='点击查看图片'/>";
                return rvalue;
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
        var LODOP;
        //打印发货单 prePrint
        function prePrint() {
            $.ajax({
                url: "orderApi.aspx?method=UpdatePrintNum&OrderNo=" + $('#OrderNo').val(),
                cache: false, async: true, dataType: "json",
                success: function (text) { }
            });
            $.ajax({
                url: "orderApi.aspx?method=GetHouseSendTitle&HouseID=" + rowHouseID,
                cache: false, async: false, dataType: "text",
                success: function (text) {
                    sendTitle = text;
                }, error: function () {
                    sendTitle = '<%=SendTitle%>';
                }
            });
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            //debugger
            if (rowHouseID == 64 || rowHouseID == 89) {
                //广通慧采销售发货单
                var oldgriddata = $('#dgSave').datagrid('getRows');
                oldgriddata.sort(function (a, b) {
                    var x = a.Specs.toLowerCase();
                    var y = b.Specs.toLowerCase();
                    if (x < y) { return -1; }
                    if (x > y) { return 1; }
                    return 0;
                });
                oldgriddata.sort(function (a, b) { return a.TypeID - b.TypeID; });
                var griddata = [];
                var js = 0, Alltotal = 0, AllPiece = 0; p = 1; pie = 0; total = 0;
                Alltotal = Number($('#TotalCharge').numberbox('getValue')).toFixed(2);
                if ($('#OpenOrderNo').val()) {
                    //Alltotal = Number($('#TransportFee').numberbox('getValue')).toFixed(2);
                    Alltotal = Number($('#TotalCharge').numberbox('getValue')).toFixed(2);
                }
                for (var k = 0; k < oldgriddata.length; k++) {
                    AllPiece += Number(oldgriddata[k].Piece);
                    if (oldgriddata[k].Piece > 0) {
                        griddata.push(oldgriddata[k]);
                    }
                }

                var ALen = griddata.length;
                var ji = 0;
                for (var i = 0; i < ALen; i++) {
                    if (i == (p - 1) * 5) {
                        if (p > 1) {
                            ji = 0;
                            LODOP.NewPage();
                        }
                        p++;
                        LODOP.SET_PRINT_PAGESIZE(1, 2200, 935, "");
            //LODOP.SET_PRINT_PAGESIZE(3, 2200, 10, "");
            //LODOP.PRINT_INITA(0, 0, 800, 320, "广通公司");
                        //var sendTitle = '<%=SendTitle%>'
                        var nowdate = new Date($('#CreateDate').datetimebox('getValue'));
                        LODOP.ADD_PRINT_TEXT(6, 220, 402, 28, sendTitle);
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 18);
                        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                        LODOP.ADD_PRINT_TEXT(34, 28, 84, 20, "收货单位：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        // LODOP.ADD_PRINT_TEXT(41, 90, 412, 20, $('#AAcceptUnit').val() + " " + $('#AAcceptPeople').val() + " " + $('#AAcceptCellphone').val());//收货单位 收货人 收货电话APayClientNum
                        if ($('#APayClientNum').combobox('getText') == $('#AAcceptPeople').val()) {
                            LODOP.ADD_PRINT_TEXT(34, 105, 412, 20, $('#AAcceptPeople').val() + " " + $('#AAcceptCellphone').val());//收货单位 收货人 收货电话
                        } else {
                            LODOP.ADD_PRINT_TEXT(34, 105, 412, 20, $('#APayClientNum').combobox('getText') + " → " + $('#AAcceptPeople').val() + " " + $('#AAcceptCellphone').val());//收货单位 收货人 收货电话
                        }
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(34, 515, 55, 20, "单号：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        //LODOP.ADD_PRINT_TEXT(34, 564, 200, 20, $('#OrderNo').val());//订单号
                        var OrderNo = $('#OrderNo').val();
                        LODOP.ADD_PRINT_TEXT(34, 564, 200, 20, OrderNo.slice(0, 4) + "-" + OrderNo.slice(4, 6) + "-" + OrderNo.slice(6, 8) + "-" + OrderNo.slice(8, 11));//订单号
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(55, 28, 84, 20, "送货地址：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(55, 105, 410, 20, $('#AAcceptAddress').val());//收货地址
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(55, 515, 54, 20, "时间：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(55, 564, 153, 20, nowdate.getHours() + ":" + nowdate.getMinutes() + ":" + nowdate.getSeconds());//开单时间
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(55, 722, 72, 20, (p - 1) + "/" + (Math.floor((ALen - 1) / 5) + 1));
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                        LODOP.ADD_PRINT_RECT(76, 28, 726, 25, 0, 1);

                        //LODOP.ADD_PRINT_RECT(76, 60, 182, 25, 0, 1);
                        //LODOP.ADD_PRINT_RECT(76, 376, 87, 25, 0, 1);
                        //LODOP.ADD_PRINT_RECT(76, 511, 55, 25, 0, 1);

                        LODOP.ADD_PRINT_LINE(101, 60, 76, 61, 0, 1);
                        LODOP.ADD_PRINT_LINE(101, 242, 76, 243, 0, 1);
                        LODOP.ADD_PRINT_LINE(101, 376, 76, 377, 0, 1);
                        LODOP.ADD_PRINT_LINE(101, 464, 76, 465, 0, 1);
                        LODOP.ADD_PRINT_LINE(101, 512, 76, 513, 0, 1);
                        LODOP.ADD_PRINT_LINE(101, 566, 76, 567, 0, 1);


                        LODOP.ADD_PRINT_TEXT(82, 36, 26, 20, "序");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(82, 101, 150, 20, "商品名称");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(82, 290, 70, 20, "花纹");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(82, 392, 55, 20, "级别");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(82, 472, 50, 20, "单位");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(82, 523, 40, 20, "数量");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        var jck = 4 * 28;
                        if ((ALen - js) % 5 == (ALen - js)) {
                            //说明是最后一页
                            jck = (ALen % 5 - 1) * 28;
                        }
                        //判断是否选中打印价格勾选框
                        if ($('#IsPrintPrice').is(':checked')) {

                            //LODOP.ADD_PRINT_RECT(76, 630, 76, 25, 0, 1);
                            LODOP.ADD_PRINT_LINE(101, 631, 76, 632, 0, 1);
                            LODOP.ADD_PRINT_LINE(101, 706, 76, 707, 0, 1);

                            LODOP.ADD_PRINT_TEXT(82, 574, 41, 20, "单价");
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                            LODOP.ADD_PRINT_TEXT(82, 641, 61, 20, "金额");
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                            LODOP.ADD_PRINT_TEXT(82, 711, 44, 20, "批次");
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                            LODOP.ADD_PRINT_TEXT(135 + jck, 128, 303, 20, atoc(Alltotal));//总金额大写
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            //LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                            LODOP.ADD_PRINT_TEXT(135 + jck, 633, 107, 20, Alltotal);//总金额
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);

                        } else {
                            LODOP.ADD_PRINT_TEXT(82, 574, 60, 20, "批次");
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        }

                        LODOP.ADD_PRINT_RECT(129 + jck, 28, 726, 25, 0, 1);

                        LODOP.ADD_PRINT_LINE(154 + jck, 81, 129 + jck, 82, 0, 1);
                        LODOP.ADD_PRINT_LINE(154 + jck, 512, 129 + jck, 513, 0, 1);
                        LODOP.ADD_PRINT_LINE(154 + jck, 566, 129 + jck, 567, 0, 1);

                        //LODOP.ADD_PRINT_RECT(129 + jck, 81, 430, 25, 0, 1);
                        //LODOP.ADD_PRINT_RECT(129 + jck, 566, 188, 25, 0, 1);


                        LODOP.ADD_PRINT_TEXT(135 + jck, 37, 52, 20, "总计：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        //LODOP.SET_PRINT_STYLEA(0, "Bold", 1);

                        LODOP.ADD_PRINT_TEXT(135 + jck, 525, 57, 20, AllPiece);//总数
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);

                        //LODOP.ADD_PRINT_RECT(154 + jck, 28, 726, 25, 0, 1);

                        //LODOP.ADD_PRINT_LINE(179 + jck, 81, 154 + jck, 82, 0, 1);
                        //LODOP.ADD_PRINT_LINE(179 + jck, 512, 154 + jck, 513, 0, 1);
                        //LODOP.ADD_PRINT_LINE(179 + jck, 614, 154 + jck, 615, 0, 1);

                        //LODOP.ADD_PRINT_RECT(154 + jck, 81, 430, 25, 0, 1);
                        //LODOP.ADD_PRINT_RECT(154 + jck, 614, 140, 25, 0, 1);

                        LODOP.ADD_PRINT_TEXT(158 + jck, 37, 49, 20, "备注");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(158 + jck, 93, 332, 50, $('#ARemark').val());//备注
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(158 + jck, 530, 83, 20, "付款方式：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(158 + jck, 632, 59, 20, $("#CheckOutType").combobox('getText'));//付款方式
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(186 + jck, 43, 100, 20, "");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                        LODOP.ADD_PRINT_TEXT(186 + jck, 530, 80, 20, "未付签收：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                    }
                    js++;
                    total = Number(griddata[i].Piece) * Number(griddata[i].ActSalePrice);

                    LODOP.ADD_PRINT_RECT(101 + ji * 28, 28, 726, 28, 0, 1);

                    //LODOP.ADD_PRINT_RECT(101 + ji * 28, 60, 182, 28, 0, 1);
                    //LODOP.ADD_PRINT_RECT(101 + ji * 28, 376, 87, 28, 0, 1);
                    //LODOP.ADD_PRINT_RECT(101 + ji * 28, 511, 55, 28, 0, 1);

                    LODOP.ADD_PRINT_LINE(129 + ji * 28, 60, 101 + ji * 28, 61, 0, 1);
                    LODOP.ADD_PRINT_LINE(129 + ji * 28, 242, 101 + ji * 28, 243, 0, 1);
                    LODOP.ADD_PRINT_LINE(129 + ji * 28, 376, 101 + ji * 28, 377, 0, 1);
                    LODOP.ADD_PRINT_LINE(129 + ji * 28, 464, 101 + ji * 28, 465, 0, 1);
                    LODOP.ADD_PRINT_LINE(129 + ji * 28, 512, 101 + ji * 28, 513, 0, 1);
                    LODOP.ADD_PRINT_LINE(129 + ji * 28, 566, 101 + ji * 28, 567, 0, 1);

                    LODOP.ADD_PRINT_TEXT(106 + ji * 28, 36, 26, 20, js);
                    //判断是否选中打印价格勾选框
                    if ($('#IsPrintPrice').is(':checked')) {

                        LODOP.ADD_PRINT_LINE(129 + ji * 28, 631, 101 + ji * 28, 632, 0, 1);
                        LODOP.ADD_PRINT_LINE(129 + ji * 28, 706, 101 + ji * 28, 707, 0, 1);

                        //LODOP.ADD_PRINT_RECT(101 + ji * 28, 630, 76, 28, 0, 1);
                        LODOP.ADD_PRINT_TEXT(106 + ji * 28, 576, 80, 20, griddata[i].ActSalePrice);//单价
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        //LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                        LODOP.ADD_PRINT_TEXT(106 + ji * 28, 643, 80, 20, total);//金额
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        //LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                        LODOP.ADD_PRINT_TEXT(106 + ji * 28, 710, 80, 20, griddata[i].Batch);//批次
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        //LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                    } else {
                        LODOP.ADD_PRINT_TEXT(106 + ji * 28, 580, 70, 20, griddata[i].Batch);//批次
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        //LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                    }
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    //LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                    LODOP.ADD_PRINT_TEXT(106 + ji * 28, 65, 300, 20, griddata[i].TypeName + " " + griddata[i].Specs);//品牌加规格
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    //LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                    LODOP.ADD_PRINT_TEXT(106 + ji * 28, 257, 121, 20, griddata[i].Figure);//花纹
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    // LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                    LODOP.ADD_PRINT_TEXT(106 + ji * 28, 380, 77, 20, griddata[i].LoadIndex + griddata[i].SpeedLevel);//载速
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    //LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                    LODOP.ADD_PRINT_TEXT(106 + ji * 28, 472, 49, 20, griddata[i].Package);//条套
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    //LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                    LODOP.ADD_PRINT_TEXT(106 + ji * 28, 527, 46, 20, Number(griddata[i].Piece));//数量
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    //LODOP.SET_PRINT_STYLEA(0, "Bold", 1);

                    ji++;
                }
                LODOP.PREVIEW();

                //LODOP.PRINT();
                //LODOP.PRINT_DESIGN();
            }
            else {
                var ThrowGoodId = "";
                $('input[name="ThrowGood"]:checked').each(function () {
                    ThrowGoodId = $(this).val();

                });
                var griddata = $('#dgSave').datagrid('getRows');
                for (var i = 0; i < griddata.length; i++) {
                    if (griddata[i].Piece <= 0) {
                        griddata.splice(i, 1);
                    }
                }
                var oty = griddata[0].TypeParentID;

                var oth = $('#HiddenOutHouseName').val();
                if (rowHouseID != 47) {
                    var json = JSON.stringify(griddata)
                    var returnType = false;
                    $.ajax({
                        url: 'orderApi.aspx?method=QueryOrderDataForPrePrint',
                        type: 'post', dataType: 'json', data: { data: json },
                        async: false,
                        success: function (data) {
                            if (data == null || data == undefined || data.length < 1) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有可打印的发货单数据！', 'warning');
                                returnType = true;
                                return;
                            } else {
                                griddata = data;
                            }
                        }
                    });
                }
                if (returnType) { return; }
                //debugger;
                var js = 0, Alltotal = 0, AllPiece = 0; p = 1; pie = 0; total = 0; AllSalePrice = 0;

                Alltotal = Number($('#TotalCharge').numberbox('getValue')).toFixed(2);
                if ($('#OpenOrderNo').val()) {
                    Alltotal = Number($('#TotalCharge').numberbox('getValue')).toFixed(2);
                    //Alltotal = Number($('#TransportFee').numberbox('getValue')).toFixed(2);
                }
                for (var k = 0; k < griddata.length; k++) {
                    pie = Number(griddata[k].Piece);
                    total = Number(pie) * Number(griddata[k].ActSalePrice);
                    //Alltotal += total;
                    AllPiece += Number(pie);
                }

                for (var i = 0; i < griddata.length; i++) {
                    console.log(griddata, p)
                    if (i == (p - 1) * 10) {
                        if (p > 1) {
                            LODOP.NewPage();
                        }
                        p++;
                        LODOP.SET_PRINT_PAGESIZE(3, 2100, 30, "");
                        LODOP.ADD_PRINT_RECT(-2, 2, 788, 522, 0, 1);
                        LODOP.ADD_PRINT_LINE(31, 3, 32, 791, 0, 1);
                        LODOP.ADD_PRINT_LINE(58, 3, 57, 791, 0, 1);
                        LODOP.ADD_PRINT_LINE(86, 82, 31, 83, 0, 1);
                        LODOP.ADD_PRINT_LINE(56, 657, 31, 658, 0, 1);
                        LODOP.ADD_PRINT_LINE(87, 3, 86, 791, 0, 1);
                        LODOP.ADD_PRINT_LINE(110, 3, 109, 791, 0, 1);
                        LODOP.ADD_PRINT_LINE(133, 82, 86, 83, 0, 1);
                        LODOP.ADD_PRINT_LINE(134, 3, 133, 791, 0, 1);
                        LODOP.ADD_PRINT_LINE(86, 347, 57, 348, 0, 1);
                        LODOP.ADD_PRINT_LINE(86, 444, 57, 445, 0, 1);
                        LODOP.ADD_PRINT_LINE(56, 575, 31, 576, 0, 1);
                        LODOP.ADD_PRINT_LINE(396, 3, 395, 791, 0, 1);
                        LODOP.ADD_PRINT_LINE(479, 58, 395, 59, 0, 1);
                        LODOP.ADD_PRINT_LINE(453, 190, 434, 190, 0, 1);
                        LODOP.ADD_PRINT_LINE(415, 3, 414, 791, 0, 1);
                        LODOP.ADD_PRINT_LINE(479, 553, 395, 554, 0, 1);
                        LODOP.ADD_PRINT_LINE(520, 648, 395, 649, 0, 1);
                        LODOP.ADD_PRINT_LINE(435, 3, 434, 791, 0, 1);
                        LODOP.ADD_PRINT_LINE(455, 3, 454, 648, 0, 1);
                        LODOP.ADD_PRINT_LINE(480, 3, 479, 648, 0, 1);
                        LODOP.ADD_PRINT_LINE(454, 255, 435, 255, 0, 1);
                        LODOP.ADD_PRINT_LINE(454, 450, 435, 450, 0, 1);
                        LODOP.ADD_PRINT_LINE(86, 575, 57, 576, 0, 1);
                        LODOP.ADD_PRINT_LINE(87, 657, 57, 658, 0, 1);

                //var saleName = $('#SaleManName').val();
                            //var sendTitle = '<%=SendTitle%>'
                        //if (saleName == "邱小彬") { sendTitle = "新陆城配发货单"; }
                        if (oth.indexOf("汕头科矿") >= 0) { sendTitle = "汕头市科矿贸易有限公司送货单"; }
                        LODOP.ADD_PRINT_TEXT(1, 206, 541, 33, sendTitle);
                        LODOP.SET_PRINT_STYLEA(0, "FontName", "新宋体");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 19);
                        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);

                        if ($("#HouseID").val() != "59" && rowHouseID != 83 && rowHouseID != 85 && oth.indexOf("汕头科矿") < 0) {
                            LODOP.ADD_PRINT_IMAGE(-1, 7, 198, 32, "<img src='../CSS/image/dlqff.jpg'/>");
                        }
                        LODOP.SET_PRINT_STYLEA(0, "Stretch", 1);
                        LODOP.ADD_PRINT_TEXT(37, 5, 87, 25, "收货地址：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);
                        LODOP.ADD_PRINT_TEXT(37, 86, 492, 25, $('#AAcceptAddress').textbox('getValue'));
                        if ($('#AAcceptAddress').textbox('getValue').length > 38) {
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                        } else if ($('#AAcceptAddress').textbox('getValue').length > 33) {
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        } else {
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        }
                        LODOP.ADD_PRINT_TEXT(37, 583, 85, 26, "发货日期：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(37, 657, 115, 25, getNowFormatDate($('#CreateDate').datetimebox('getValue')));
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);

                        if (oth.indexOf("汕头科矿") >= 0) {
                            LODOP.ADD_PRINT_TEXT(64, 5, 87, 25, "发货单号：");
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);
                            LODOP.ADD_PRINT_TEXT(64, 86, 254, 25, $('#HHAwbNo').val());
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            LODOP.ADD_PRINT_TEXT(66, 583, 85, 25, "代收款项：");
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            LODOP.ADD_PRINT_TEXT(66, 668, 127, 25, $('#CollectMoney').val());
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        } else {
                            LODOP.ADD_PRINT_TEXT(64, 5, 87, 25, "物流公司：");
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);
                            var ALogisAwbNo = $('#ALogisAwbNo').textbox('getValue');
                            if ($('#ALogisID').combobox('getValue') == "62" || $('#ALogisID').combobox('getValue') == "34") {
                                LODOP.ADD_PRINT_TEXT(64, 86, 254, 25, $('#ALogisID').combobox('getText') + " " + ALogisAwbNo);
                            } else {
                                if ($('#ALogisID').combobox('getText') == "0") {
                                    LODOP.ADD_PRINT_TEXT(64, 86, 254, 25, "");
                                } else {
                                    LODOP.ADD_PRINT_TEXT(64, 86, 254, 25, $('#ALogisID').combobox('getText'));
                                }
                            }
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            LODOP.ADD_PRINT_TEXT(66, 583, 85, 25, "发货单号：");
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            LODOP.ADD_PRINT_TEXT(66, 668, 127, 25, $('#OrderNo').val());
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        }
                        LODOP.ADD_PRINT_TEXT(64, 353, 85, 25, "到达城市：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.SET_PRINT_STYLEA(0, "Alignment", 3);
                        LODOP.ADD_PRINT_TEXT(65, 448, 130, 25, $('#ADest').numberbox('getText'));
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);


                        LODOP.ADD_PRINT_TEXT(90, 7, 85, 25, "收货单位：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);
                        LODOP.ADD_PRINT_TEXT(114, 7, 85, 25, "发货单位：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);

                        var sphone = $('#AAcceptCellphone').textbox('getValue');
                        if (sphone == "") {
                            sphone = $('#AAcceptTelephone').textbox('getValue');
                        }
                        if ($('#AAcceptTelephone').textbox('getValue') != $('#AAcceptCellphone').textbox('getValue')) {
                            sphone = $('#AAcceptCellphone').textbox('getValue') + " " + $('#AAcceptTelephone').textbox('getValue');
                        }
                        if ($('#AAcceptUnit').combobox('getText') == $('#AAcceptPeople').textbox('getValue')) {
                            LODOP.ADD_PRINT_TEXT(90, 86, 705, 25, $('#AAcceptPeople').textbox('getValue') + " " + sphone);//收货人
                        } else {
                            LODOP.ADD_PRINT_TEXT(90, 86, 705, 25, $('#AAcceptUnit').combobox('getText') + " " + $('#AAcceptPeople').textbox('getValue') + " " + sphone);//收货人
                        }
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                        <%--var hous = '<%= HouseName%>';--%>
                        var hous = $("#HouseID").val();
                        var sendUnit = '广州狄乐汽车服务有限公司';
                        var sendPhone = '13822154649';
                        switch (parseInt(hous)) {
                            case 1: sendUnit = '湖南省狄乐汽车服务有限公司'; sendPhone = '13319570206'; break;
                            case 3: sendUnit = '湖北省狄乐汽车服务有限公司'; sendPhone = '17771479223;83092268'; break;
                            case 9: sendUnit = '广州迪乐泰'; sendPhone = '13822154649'; break;
                            case 10: sendUnit = '西安新陆程'; sendPhone = '029-84524648'; break;
                            case 11: sendUnit = '西安新陆程'; sendPhone = '029-84524648'; break;
                            case 12: sendUnit = '梅州新陆程'; sendPhone = '18088857730'; break;
                            case 34: sendUnit = '海南迪乐泰'; sendPhone = '15120882670'; break;
                            case 44: sendUnit = '揭阳迪乐泰'; sendPhone = '13016071328'; break;
                            case 45: sendUnit = '广州迪乐泰'; sendPhone = '19924253620'; break;
                            case 46: sendUnit = '四川迪乐泰'; sendPhone = '19827681201'; break;
                            case 48: sendUnit = '重庆迪乐泰'; sendPhone = '13102384877'; break;
                            case 59: sendUnit = '海南新路城配'; sendPhone = '089866804322'; break;
                            case 82: sendUnit = '狄乐汽服'; sendPhone = '13822154649'; break;
                            case 65: sendUnit = '广州狄乐汽车服务有限公司'; sendPhone = '19924281347'; break;
                            case 100: sendUnit = '湖南省狄乐汽车服务有限公司'; sendPhone = '13319578676'; break;
                        }
                        var cphone = $('#SaleCellPhone').val();
                        if (cphone != "" && cphone != undefined) {
                            sendPhone = cphone;
                        }
                        if (oth.indexOf("汕头科矿") >= 0) {
                            LODOP.ADD_PRINT_TEXT(114, 86, 705, 25, "汕头市科矿贸易有限公司 " + " " + "0754-87294936");//填业务员

                        } else {
                            //if (saleName == "邱小彬") {
                            //    LODOP.ADD_PRINT_TEXT(114, 86, 705, 25, "新陆城配  13802517097，13318794144");//填业务员
                            //} else {
                            //    LODOP.ADD_PRINT_TEXT(114, 86, 705, 25, sendUnit + " " + $('#SaleManName').val() + " " + sendPhone);//填业务员
                            //}
                            LODOP.ADD_PRINT_TEXT(114, 86, 705, 25, sendUnit + " " + $('#SaleManName').val() + " " + sendPhone);//填业务员
                        }
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(135, 40, 54, 24, "品牌");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(135, 141, 71, 24, "商品编码");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        if (oth.indexOf("汕头科矿") >= 0) {
                            LODOP.ADD_PRINT_TEXT(135, 263, 300, 24, "商 品 全 名");
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            LODOP.ADD_PRINT_TEXT(135, 601, 48, 24, "型号");//单价
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            LODOP.ADD_PRINT_TEXT(135, 664, 47, 24, "单位");//数量
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            LODOP.ADD_PRINT_TEXT(135, 724, 53, 24, "数量");//总价
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        } else {
                            if (oty == "35" || oty == "10") {
                                LODOP.ADD_PRINT_TEXT(135, 263, 300, 24, "商品名称");
                                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            } else {
                                LODOP.ADD_PRINT_TEXT(135, 263, 50, 24, "规格");
                                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                                LODOP.ADD_PRINT_TEXT(135, 395, 45, 24, "花纹");
                                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                                LODOP.ADD_PRINT_TEXT(135, 480, 80, 24, "载速");
                                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                                LODOP.ADD_PRINT_TEXT(135, 545, 46, 24, "周期");
                                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            }
                            LODOP.ADD_PRINT_TEXT(135, 601, 48, 24, "单价");
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            LODOP.ADD_PRINT_TEXT(135, 664, 47, 24, "数量");
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            LODOP.ADD_PRINT_TEXT(135, 724, 53, 24, "总价");
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        }
                        LODOP.ADD_PRINT_LINE(153, 3, 152, 791, 0, 1);
                        LODOP.ADD_PRINT_TEXT(397, 16, 54, 25, "总计");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(397, 565, 69, 19, AllPiece);
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                        LODOP.ADD_PRINT_TEXT(416, 13, 61, 25, "总 计");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
                        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                        if (oth.indexOf("汕头科矿") >= 0) {
                            LODOP.ADD_PRINT_TEXT(416, 241, 304, 25, numToChinese(Number(AllPiece)));
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
                            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                        } else {
                            if ($('#IsPrintPrice').is(':checked')) {
                                if (oty == "35" || oty == "10") {
                                    LODOP.ADD_PRINT_TEXT(397, 667, 105, 19, Number(Alltotal) + Number($('#DeliveryFee').numberbox('getText')));
                                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                                    LODOP.ADD_PRINT_TEXT(416, 241, 304, 25, atoc(Number(Alltotal) + Number($('#DeliveryFee').numberbox('getText'))));
                                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
                                    LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                                } else {
                                    if ($("#LiPei").is(":checked")) {
                                        //LODOP.ADD_PRINT_TEXT(397, 667, 105, 19, 0);
                                        //LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                                        //LODOP.ADD_PRINT_TEXT(416, 241, 304, 25, atoc(0));
                                        //LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
                                        //LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                                        LODOP.ADD_PRINT_TEXT(397, 667, 105, 19, Alltotal);
                                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                                        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);

                                        LODOP.ADD_PRINT_TEXT(416, 241, 304, 25, atoc(Alltotal));
                                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
                                        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                                    } else {
                                        LODOP.ADD_PRINT_TEXT(397, 667, 105, 19, Alltotal);
                                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                                        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);

                                        LODOP.ADD_PRINT_TEXT(416, 241, 304, 25, atoc(Alltotal));
                                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
                                        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                                    }
                                }
                            }
                        }
                        LODOP.ADD_PRINT_TEXT(436, 16, 66, 22, "制表：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(436, 84, 100, 19, $('#CreateAwb').textbox('getValue'));
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(436, 200, 61, 22, "司机：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(460, 11, 58, 28, "备 注");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
                        LODOP.ADD_PRINT_TEXT(456, 72, 505, 29, $('#ARemark').val() + " " + $('#HHAwbNo').val());
                        //if ($("#AHouseID").combobox('getValue') == "65") {
                        //    LODOP.ADD_PRINT_TEXT(456, 72, 505, 29, $('#ARemark').val() + " " + $('#HHAwbNo').val());
                        //} else {
                        //    LODOP.ADD_PRINT_TEXT(456, 72, 505, 29, $('#ARemark').val());
                        //}
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                        if ($("#AHouseID").combobox('getText').indexOf("云仓") != -1) {
                            //LODOP.ADD_PRINT_TEXT(437, 466, 93, 22, $("#CheckOutType").combobox('getText'));
                            //LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            //LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                            if ($("#CheckOutType").combobox('getText') == "微信") {
                                if ($("#ALogisID").combobox('getText') == "自提") {
                                    LODOP.ADD_PRINT_TEXT(437, 466, 93, 22, "自提");
                                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                                } else {
                                    LODOP.ADD_PRINT_TEXT(437, 466, 93, 22, "送货费用：");
                                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                                }
                            }
                            else if ($("#CheckOutType").combobox('getText') == "货到付款") {
                                LODOP.ADD_PRINT_TEXT(437, 466, 93, 22, "送货费用：");
                                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                                LODOP.ADD_PRINT_TEXT(416, 563, 100, 20, "货到付款");
                                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                                LODOP.SET_PRINT_STYLEA(0, "Bold", 1);

                            }
                            if (Number($('#TransitFee').numberbox('getText')) > 0) {
                                LODOP.ADD_PRINT_TEXT(437, 584, 78, 22, $('#TransitFee').numberbox('getText'));
                                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            }

                        }
                        else {
                            LODOP.ADD_PRINT_TEXT(437, 466, 93, 22, "物流结算：");
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            if (oty == "35" || oty == "10") {
                                LODOP.ADD_PRINT_TEXT(437, 584, 78, 22, $('#DeliveryFee').numberbox('getText'));
                            } else {
                                LODOP.ADD_PRINT_TEXT(437, 584, 78, 22, $('#ADeliverySettlement').combobox('getText'));
                            }
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                        }
                        if ($('#IsPrintPrice').is(':checked')) {
                            if ($("#AHouseID").combobox('getValue') == "9" || $("#AHouseID").combobox('getValue') == "44" || $("#AHouseID").combobox('getValue') == "45") {
                                if ($("#LiPei").is(":checked")) {
                                    LODOP.ADD_PRINT_TEXT(416, 569, 87, 22, "付款方式：");
                                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                                    LODOP.ADD_PRINT_TEXT(416, 655, 300, 19, $("#CheckOutType").combobox('getText'));
                                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                                } else {
                                    LODOP.ADD_PRINT_TEXT(416, 569, 87, 22, "付款方式：");
                                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                                    LODOP.ADD_PRINT_TEXT(416, 655, 300, 19, $("#CheckOutType").combobox('getText'));
                                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                                }
                            }
                        }
                        var daili = '迪乐泰湖南轮胎总代理';
                        if (hous.indexOf('湖北') != -1) {
                            daili = '迪乐泰湖北轮胎总代理';
                            LODOP.ADD_PRINT_TEXT(436, 656, 100, 19, "17771448223");
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        }
                        if (hous.indexOf('西安') != -1) {
                            daili = '优科豪马轮胎西安新陆程总代理';
                        }
                        if (hous.indexOf('梅州') != -1) { daili = '梅州新陆程'; }
                        if (hous.indexOf('揭阳') != -1) { daili = '揭阳迪乐泰'; }
                        if (hous.indexOf('广东') != -1) { daili = '广州迪乐泰'; }
                        if (hous.indexOf('四川') != -1) { daili = '四川迪乐泰'; }
                        if (hous.indexOf('重庆') != -1) { daili = '重庆迪乐泰'; }
                        if (hous.indexOf('广州') != -1) {
                            daili = '广州迪乐泰轮胎总代理';
                            var addr = $('#AAcceptAddress').textbox('getValue');
                            var sj = "";
                            //if (addr.indexOf('大岭山') > -1 || addr.indexOf('长安') > -1 || addr.indexOf('虎门') > -1 || addr.indexOf('厚街') > -1 || addr.indexOf('南城') > -1 || addr.indexOf('东城') > -1 || addr.indexOf('沙田') > -1 || addr.indexOf('麻涌') > -1) {
                            //    sj = "胡 17776469164";
                            //} else if (addr.indexOf('寮步') > -1 || addr.indexOf('企石') > -1 || addr.indexOf('桥头') > -1 || addr.indexOf('茶山') > -1 || addr.indexOf('石牌') > -1 || addr.indexOf('莞城') > -1 || addr.indexOf('高埗') > -1 || addr.indexOf('万江') > -1 || addr.indexOf('中堂') > -1) {
                            //    sj = "王 13189224412";
                            //} else if (addr.indexOf('塘厦') > -1 || addr.indexOf('凤岗') > -1 || addr.indexOf('清溪') > -1 || addr.indexOf('樟木头') > -1 || addr.indexOf('黄江') > -1 || addr.indexOf('大朗') > -1 || addr.indexOf('常平') > -1 || addr.indexOf('横沥') > -1 || addr.indexOf('东坑') > -1) {
                            //    sj = "陈 13711412142";
                            //}
                            LODOP.ADD_PRINT_TEXT(436, 421, 300, 19, sj);
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        }
                        if (hous.indexOf('海南') != -1) {
                            daili = '海南迪乐泰轮胎总代理';
                        }
                        //LODOP.ADD_PRINT_TEXT(460, 566, 189, 30, daili);
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
                        var beizu = '注：第一联结算联（白）、第二联收货人留存（红）、第三联对账联（黄）';
                        if (hous.indexOf('湖北') != -1) {
                            beizu = '湖北优科豪马轮胎代理迪乐泰公司感谢与您的合作，谢谢！';
                        }
                        LODOP.ADD_PRINT_TEXT(484, 11, 645, 26, beizu);
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        if (oth.indexOf("汕头科矿") >= 0) {
                            LODOP.ADD_PRINT_TEXT(416, 657, 121, 20, "付款扫此二维码");
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                            LODOP.ADD_PRINT_IMAGE(393, 650, 133, 133, "<img src='../CSS/image/stkgskm.png'/>");
                            LODOP.SET_PRINT_STYLEA(0, "Stretch", 2);
                            //LODOP.ADD_PRINT_BARCODE(434, 669, 180, 100, "QRCode", "http://ffpo.cn/dtpp0");
                            //LODOP.SET_PRINT_STYLEA(0, "GroundColor", "#FFFFFF");
                            //LODOP.ADD_PRINT_TEXT(443, 753, 23, 82, "付\r\n\r\n款");
                            //LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            //LODOP.ADD_PRINT_TEXT(443, 657, 23, 82, "扫\r\n\r\n码");
                            //LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);($("#AHouseID").combobox('getValue') == "93" ||
                        } else if ($("#AHouseID").combobox('getValue') == "92") {
                            LODOP.ADD_PRINT_BARCODE(434, 669, 180, 90, "QRCode", $('#OrderNo').val());
                            LODOP.SET_PRINT_STYLEA(0, "GroundColor", "#FFFFFF");
                        }
                        else if ($("#AHouseID").combobox('getText').indexOf("云仓") != -1) {
                            LODOP.ADD_PRINT_IMAGE(416, 650, 133, 133, "<img src='../CSS/image/kjfkm.png'/>");
                            LODOP.SET_PRINT_STYLEA(0, "Stretch", 2);
                        }
                        //if ($('#IsPrintPrice').is(':checked')) {
                        //    //if ($("#CheckOutType").combobox('getValue') == 0 || $("#CheckOutType").combobox('getValue') == 1 || $("#CheckOutType").combobox('getValue') == 4) {
                        //    if (($('#OrderNo').val()).indexOf('GZ') > -1 || ($('#OrderNo').val()).indexOf('DG') > -1 || ($('#OrderNo').val()).indexOf('SZ') > -1 || ($('#OrderNo').val()).indexOf('JY') > -1) {
                        //        if ($("#LiPei").is(":checked")) {

                        //        } else {
                        //            LODOP.ADD_PRINT_BARCODE(435, 676, 190, 80, "QRCode", "http://dlt.neway5.com/Weixin/ScanQrPayOrder.aspx?OrderNo=" + $('#OrderNo').val());
                        //            LODOP.SET_PRINT_STYLEA(0, "GroundColor", "#FFFFFF");
                        //            LODOP.ADD_PRINT_TEXT(443, 753, 23, 82, "付\r\n\r\n款");
                        //            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        //            LODOP.ADD_PRINT_TEXT(443, 657, 23, 82, "扫\r\n\r\n码");
                        //            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        //        }
                        //    }
                        //}
                    }

                    LODOP.ADD_PRINT_LINE(176 + (i - (p - 2) * 10) * 23, 3, 175 + (i - (p - 2) * 10) * 23, 791, 0, 1);
                    LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 25, 134 + (i - (p - 2) * 10) * 23, 26, 0, 1);
                    LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 119, 134 + (i - (p - 2) * 10) * 23, 120, 0, 1);
                    LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 237, 134 + (i - (p - 2) * 10) * 23, 238, 0, 1);
                    if (oth.indexOf("汕头科矿") >= 0) {
                        LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 592, 134 + (i - (p - 2) * 10) * 23, 593, 0, 1);

                    } else {
                        if (oty == "35" || oty == "10") {
                            LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 592, 134 + (i - (p - 2) * 10) * 23, 593, 0, 1);
                        } else {
                            LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 348, 134 + (i - (p - 2) * 10) * 23, 349, 0, 1);
                            LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 468, 134 + (i - (p - 2) * 10) * 23, 469, 0, 1);
                            LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 540, 134 + (i - (p - 2) * 10) * 23, 541, 0, 1);
                            LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 592, 134 + (i - (p - 2) * 10) * 23, 593, 0, 1);
                        }

                    }
                    LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 657, 134 + (i - (p - 2) * 10) * 23, 658, 0, 1);
                    LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 701, 134 + (i - (p - 2) * 10) * 23, 702, 0, 1);

                    js++;
                    pie = Number(griddata[i].Piece);
                    total = Number(pie) * Number(griddata[i].ActSalePrice);

                    LODOP.ADD_PRINT_TEXT(156 + (i - (p - 2) * 10) * 23, 6, 25, 23, js);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                    var gtypename = griddata[i].TypeName;
                    if (griddata[i].ShowTypeName != "" && griddata[i].ShowTypeName != undefined) { gtypename = griddata[i].ShowTypeName; }
                    LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 33, 85, 23, gtypename);//品牌
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    var gcode = griddata[i].GoodsCode;
                    if (griddata[i].ShowGoodsCode != "") { gcode = griddata[i].ShowGoodsCode; }
                    LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 122, 115, 23, gcode);//商品编码
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    if (oth.indexOf("汕头科矿") >= 0) {
                        LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 246, 380, 23, griddata[i].Specs + " " + griddata[i].Figure);//规格 
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 598, 64, 23, griddata[i].Model);//型号
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 666, 48, 23, griddata[i].Package);//单位
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 728, 150, 23, pie);//数量
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    } else {
                        if (oty == "35" || oty == "10") {
                            LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 246, 380, 23, griddata[i].Specs + " " + griddata[i].Model + " " + griddata[i].Figure);//规格 
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        } else {
                            if (griddata[i].TypeParentID != "1") {
                                LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 246, 100, 23, griddata[i].Model + " " + griddata[i].Specs);//规格 
                            }
                            else {
                                LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 246, 100, 23, griddata[i].Specs);//规格 
                            }
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 354, 130, 23, griddata[i].Figure);//花纹
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 480, 60, 23, griddata[i].LoadIndex + griddata[i].SpeedLevel);//速度级别
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                            if ($("#Tran").prop("checked")) {
                                LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 545, 49, 23, griddata[i].Batch);//周期
                            } else {
                                if (griddata[i].TypeParentID == "1") {
                                    LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 545, 49, 23, griddata[i].Batch.substr(2) + "年");//周期
                                }
                            }
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        }
                        if ($('#IsPrintPrice').is(':checked')) {
                            if ($("#LiPei").is(":checked")) {
                                //LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 598, 64, 23, 0);//单价销售价
                                //LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                                LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 598, 64, 23, griddata[i].ActSalePrice);//单价销售价
                                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            } else {
                                LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 598, 64, 23, griddata[i].ActSalePrice);//单价销售价
                                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            }
                        }
                        LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 666, 48, 23, pie);//数量出库数量
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        if ($('#IsPrintPrice').is(':checked')) {
                            if ($("#LiPei").is(":checked")) {
                                //LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 708, 150, 23, 0);//总价
                                //LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                                LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 708, 150, 23, Number(total).toFixed(2));//总价
                                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            } else {
                                LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 708, 150, 23, Number(total).toFixed(2));//总价
                                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                            }
                        }
                    }
                }
                //LODOP.PRINT_DESIGN();
                LODOP.PREVIEW();
            }
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
        //订单打印
        function pickUpOrder() {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            var nowdate = new Date();
            LODOP.SET_PRINT_PAGESIZE(0, 2100, 2970, "A4");

            var griddata = $('#dgSave').datagrid('getRows');
            var com = '<%=PickTitle%>'
            var hous = '<%= HouseName%>';
            com = griddata[0].FirstAreaName + "拣货单";
            var ptypeid = griddata[0].TypeID;
            if (ptypeid == 28) {
                //嘉士多机油 
                LODOP.ADD_PRINT_TEXT(4, 253, 485, 33, com);
                LODOP.SET_PRINT_STYLEA(0, "FontName", "新宋体");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 19);
                LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                LODOP.SET_PRINT_STYLEA(0, "Stretch", 1);
                LODOP.ADD_PRINT_TEXT(41, 12, 70, 20, "订单号：");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(41, 72, 110, 20, $('#OrderNo').val());
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(41, 368, 75, 20, "收货人：");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(41, 418, 105, 20, $('#AAcceptPeople').combobox('getText'));
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(41, 512, 90, 20, "联系电话：");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                var tell = $('#AAcceptCellphone').textbox('getValue');
                if (tell == '' || tell == null) { tell = $('#AAcceptTelephone').textbox('getValue'); }
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_BARCODE(-1, 688, 80, 80, "QRCode", $('#OrderNo').val());
                LODOP.ADD_PRINT_IMAGE(-3, 47, 198, 49, "<img src=\"../CSS/image/dlqff.jpg\"/>");
                LODOP.ADD_PRINT_TEXT(41, 182, 90, 20, "到达站：");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(41, 237, 200, 20, $('#ADest').combobox('getText'));
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_RECT(66, 3, 84, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(66, 87, 291, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(66, 378, 68, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(66, 512, 74, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(66, 586, 96, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(66, 682, 106, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(66, 446, 66, 25, 0, 1);
                LODOP.ADD_PRINT_TEXT(70, 21, 48, 25, "品牌");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(70, 140, 123, 25, "商品名称");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(70, 384, 64, 25, "出库瓶数");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(70, 522, 60, 25, "批次");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(70, 590, 88, 25, "货位代码");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(70, 684, 72, 25, "所在区域");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(70, 454, 82, 20, "出库箱数");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);


                for (var i = 0; i < griddata.length; i++) {
                    js++;
                    var p = Number(griddata[i].Piece);
                    var num = griddata[i].PackageNum == 0 ? "" : p / griddata[i].PackageNum;

                    LODOP.ADD_PRINT_RECT(90 + i * 25, 3, 84, 25, 0, 1);
                    LODOP.ADD_PRINT_RECT(90 + i * 25, 87, 291, 25, 0, 1);
                    LODOP.ADD_PRINT_RECT(90 + i * 25, 378, 68, 25, 0, 1);
                    LODOP.ADD_PRINT_RECT(90 + i * 25, 512, 74, 25, 0, 1);
                    LODOP.ADD_PRINT_RECT(90 + i * 25, 586, 96, 25, 0, 1);
                    LODOP.ADD_PRINT_RECT(90 + i * 25, 682, 106, 25, 0, 1);
                    LODOP.ADD_PRINT_RECT(90 + i * 25, 446, 66, 25, 0, 1);


                    LODOP.ADD_PRINT_TEXT(95 + i * 25, 10, 76, 23, griddata[i].TypeName);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(95 + i * 25, 92, 310, 25, griddata[i].Specs + " " + griddata[i].Model + " " + griddata[i].Figure);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                    LODOP.ADD_PRINT_TEXT(95 + i * 25, 391, 52, 20, p);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

                    LODOP.ADD_PRINT_TEXT(95 + i * 25, 456, 78, 20, num);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(95 + i * 25, 520, 100, 20, griddata[i].Batch);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

                    LODOP.ADD_PRINT_TEXT(95 + i * 25, 590, 106, 20, griddata[i].ContainerCode);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(95 + i * 25, 687, 69, 20, griddata[i].AreaName);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

                }
                LODOP.ADD_PRINT_TEXT(124 + (griddata.length - 1) * 25, 50, 65, 23, "备注：");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(124 + (griddata.length - 1) * 25, 120, 400, 23, $('#ARemark').val());
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                LODOP.ADD_PRINT_TEXT(150 + (griddata.length - 1) * 25, 50, 65, 23, "仓库：");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(150 + (griddata.length - 1) * 25, 337, 100, 20, "制表：");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(150 + (griddata.length - 1) * 25, 574, 200, 20, AllDateTime(nowdate));
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.PREVIEW();
                //LODOP.PRINT_DESIGN();
            } else {
                //var com = '湖南省迪乐泰仓库拣货单';
                if (hous.indexOf('西安') != -1) {
                    com = '西安仓库拣货单';
                    //if (hous.indexOf('西安迪乐泰') != -1) {
                    //    com = '陕西省西安新陆程仓库拣货单';
                    //}
                    LODOP.ADD_PRINT_TEXT(4, 253, 485, 33, com);
                    LODOP.SET_PRINT_STYLEA(0, "FontName", "新宋体");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 19);
                    LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                    LODOP.ADD_PRINT_IMAGE(-3, 47, 198, 49, "");
                    LODOP.SET_PRINT_STYLEA(0, "Stretch", 1);
                    LODOP.ADD_PRINT_TEXT(41, 120, 70, 20, "订单号：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(41, 180, 110, 20, $('#OrderNo').val());
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(41, 290, 90, 20, "所在仓库：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(41, 350, 80, 20, griddata[0].HouseName);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(41, 450, 75, 20, "收货人：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(41, 510, 105, 20, $('#AAcceptPeople').combobox('getText'));
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(41, 565, 90, 20, "物流单号：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    //var tell = $('#AAcceptCellphone').textbox('getValue');
                    //if (tell == '' || tell == null) { tell = $('#AAcceptTelephone').textbox('getValue'); }
                    LODOP.ADD_PRINT_TEXT(41, 638, 200, 20, $('#ALogisAwbNo').textbox('getValue'));
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_RECT(66, 3, 292, 25, 0, 1);
                    LODOP.ADD_PRINT_RECT(66, 294, 147, 25, 0, 1);
                    LODOP.ADD_PRINT_RECT(66, 440, 59, 25, 0, 1);
                    LODOP.ADD_PRINT_RECT(66, 498, 150, 25, 0, 1);
                    LODOP.ADD_PRINT_RECT(66, 647, 84, 25, 0, 1);
                    LODOP.ADD_PRINT_RECT(66, 730, 67, 25, 0, 1);
                    LODOP.ADD_PRINT_TEXT(70, 6, 304, 25, "产品名称");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(70, 298, 142, 25, "货品代码");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(70, 442, 69, 25, "出库数量");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(70, 502, 153, 25, "批次");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(70, 651, 94, 25, "货位代码");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(70, 733, 72, 25, "所在区域");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

                    var js = 0, Alltotal = 0, AllPiece = 0;
                    for (var i = 0; i < griddata.length; i++) {
                        js++;
                        var p = Number(griddata[i].Piece);
                        if (griddata[i].TypeParentID == 10 || griddata[i].TypeID == 43 || griddata[i].TypeParentID == 133) {
                            if (griddata[i].PackageNum != 0) {
                                var num = p / griddata[i].PackageNum;
                                var b = (num + "").split(".");
                                if (b.length > 1) {
                                    p = p + "/" + b[0] + griddata[i].Package + (p - b[0] * griddata[i].PackageNum);
                                } else {
                                    p = p + "/" + b[0] + griddata[i].Package;
                                }
                            }
                        }
                        LODOP.ADD_PRINT_RECT(91 + i * 25, 3, 292, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(91 + i * 25, 294, 147, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(91 + i * 25, 440, 59, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(91 + i * 25, 498, 150, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(91 + i * 25, 647, 84, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(91 + i * 25, 730, 67, 25, 0, 1);

                        LODOP.ADD_PRINT_TEXT(95 + i * 25, 6, 304, 25, griddata[i].ProductName + " " + griddata[i].Figure + " " + griddata[i].Model);
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                        LODOP.ADD_PRINT_TEXT(95 + i * 25, 298, 142, 25, griddata[i].GoodsCode);
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                        LODOP.ADD_PRINT_TEXT(95 + i * 25, 445, 70, 25, p);
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                        LODOP.ADD_PRINT_TEXT(95 + i * 25, 502, 158, 25, griddata[i].Batch);
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                        LODOP.ADD_PRINT_TEXT(95 + i * 25, 651, 94, 25, griddata[i].ContainerCode);
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                        LODOP.ADD_PRINT_TEXT(95 + i * 25, 733, 72, 25, griddata[i].AreaName);
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                    }
                    LODOP.ADD_PRINT_TEXT(149 + (griddata.length - 1) * 25, 50, 102, 23, "备注：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(149 + (griddata.length - 1) * 25, 150, 400, 23, $('#ARemark').val());
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                    LODOP.ADD_PRINT_TEXT(173 + (griddata.length - 1) * 25, 50, 102, 23, "仓库：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(173 + (griddata.length - 1) * 25, 337, 100, 20, "制表：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(173 + (griddata.length - 1) * 25, 574, 200, 20, AllDateTime(nowdate));
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                }
                else if (hous.indexOf('生产') != -1) {

                    LODOP.ADD_PRINT_TEXT(4, 253, 280, 33, com);
                    LODOP.SET_PRINT_STYLEA(0, "FontName", "新宋体");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 19);
                    LODOP.SET_PRINT_STYLEA(0, "Bold", 1);

                    LODOP.ADD_PRINT_TEXT(41, 47, 70, 20, "订单号：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(41, 114, 110, 20, $('#OrderNo').val());
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                    LODOP.ADD_PRINT_TEXT(41, 225, 90, 20, "厂家号：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(41, 289, 141, 20, griddata[0].SourceCode + "-" + griddata[0].SourceName);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                    LODOP.ADD_PRINT_TEXT(41, 434, 105, 20, "广丰单号：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(41, 510, 153, 20, $('#HHAwbNo').val());
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                    LODOP.ADD_PRINT_RECT(66, 3, 134, 25, 0, 1);
                    LODOP.ADD_PRINT_RECT(66, 136, 146, 25, 0, 1);
                    LODOP.ADD_PRINT_RECT(66, 281, 94, 25, 0, 1);
                    LODOP.ADD_PRINT_RECT(66, 374, 70, 25, 0, 1);
                    LODOP.ADD_PRINT_RECT(66, 443, 118, 25, 0, 1);
                    LODOP.ADD_PRINT_RECT(66, 560, 107, 25, 0, 1);

                    LODOP.ADD_PRINT_TEXT(70, 21, 78, 25, "产品名称");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(70, 182, 52, 25, "品番");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(70, 302, 59, 25, "背番");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(70, 381, 64, 25, "出库数量");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(70, 465, 88, 25, "货位代码");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(70, 573, 91, 25, "所在区域");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

                    var js = 0, Alltotal = 0, AllPiece = 0;
                    for (var i = 0; i < griddata.length; i++) {
                        js++;
                        var p = Number(griddata[i].Piece);
                        LODOP.ADD_PRINT_RECT(90 + i * 25, 3, 134, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(90 + i * 25, 136, 146, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(90 + i * 25, 281, 94, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(90 + i * 25, 374, 70, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(90 + i * 25, 443, 118, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(90 + i * 25, 560, 107, 25, 0, 1);
                        LODOP.ADD_PRINT_TEXT(95 + i * 25, 5, 131, 23, griddata[i].ProductName);//产品名称
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(95 + i * 25, 156, 127, 20, griddata[i].GoodsCode);//品番
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(95 + i * 25, 303, 61, 20, griddata[i].Specs);//背番 
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

                        LODOP.ADD_PRINT_TEXT(95 + i * 25, 393, 51, 20, p);//数量出库数量
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

                        LODOP.ADD_PRINT_TEXT(95 + i * 25, 463, 106, 20, griddata[i].ContainerCode);//货位代码
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(95 + i * 25, 573, 88, 20, griddata[i].AreaName);//所在区域
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    }
                    LODOP.ADD_PRINT_TEXT(147 + (griddata.length - 1) * 25, 50, 102, 23, "仓库：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(147 + (griddata.length - 1) * 25, 222, 100, 20, "制表：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(147 + (griddata.length - 1) * 25, 394, 200, 20, AllDateTime(nowdate));
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                }
                else {
                    LODOP.ADD_PRINT_TEXT(4, 253, 485, 33, com);
                    LODOP.SET_PRINT_STYLEA(0, "FontName", "新宋体");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 19);
                    LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                    LODOP.SET_PRINT_STYLEA(0, "Stretch", 1);
                    LODOP.ADD_PRINT_TEXT(41, 12, 70, 20, "订单号：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(41, 72, 110, 20, $('#OrderNo').val());
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(41, 368, 75, 20, "收货人：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(41, 418, 105, 20, $('#AAcceptPeople').combobox('getText'));
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(41, 512, 90, 20, "联系电话：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    var tell = $('#AAcceptCellphone').textbox('getValue');
                    if (tell == '' || tell == null) { tell = $('#AAcceptTelephone').textbox('getValue'); }
                    LODOP.ADD_PRINT_TEXT(41, 575, 117, 20, tell);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                    //富添盛送货单
                    if ($('#HouseID').val() == "47") {
                        LODOP.ADD_PRINT_RECT(66, 3, 170, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(66, 173, 170, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(66, 343, 100, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(66, 443, 64, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(66, 507, 69, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(66, 576, 91, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(66, 666, 105, 25, 0, 1);
                        LODOP.ADD_PRINT_TEXT(70, 21, 78, 20, "产品名称");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(70, 232, 52, 20, "规格");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(70, 348, 77, 20, "富添盛编码");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(70, 448, 64, 20, "出库数量");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(70, 517, 43, 20, "批次");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(70, 580, 68, 20, "货位代码");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(70, 668, 72, 20, "所在区域");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(41, 182, 90, 20, "到达站：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(41, 237, 200, 20, $('#ADest').combobox('getText') + " 物流：" + $('#ALogisID').combobox('getText'));
                        //LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        var js = 0, Alltotal = 0, AllPiece = 0;
                        for (var i = 0; i < griddata.length; i++) {
                            js++;
                            var p = Number(griddata[i].Piece);
                            LODOP.ADD_PRINT_RECT(90 + i * 25, 3, 170, 25, 0, 1);
                            LODOP.ADD_PRINT_RECT(90 + i * 25, 173, 170, 25, 0, 1);
                            LODOP.ADD_PRINT_RECT(90 + i * 25, 343, 100, 25, 0, 1);
                            LODOP.ADD_PRINT_RECT(90 + i * 25, 443, 64, 25, 0, 1);
                            LODOP.ADD_PRINT_RECT(90 + i * 25, 507, 69, 25, 0, 1);
                            LODOP.ADD_PRINT_RECT(90 + i * 25, 576, 91, 25, 0, 1);
                            LODOP.ADD_PRINT_RECT(90 + i * 25, 666, 105, 25, 0, 1);
                            LODOP.ADD_PRINT_TEXT(95 + i * 25, 5, 178, 18, griddata[i].ProductName);//产品名称
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                            LODOP.ADD_PRINT_TEXT(95 + i * 25, 175, 178, 15, griddata[i].Specs);//型号
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                            LODOP.ADD_PRINT_TEXT(95 + i * 25, 347, 90, 15, griddata[i].GoodsCode);//富添盛编码
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                            LODOP.ADD_PRINT_TEXT(95 + i * 25, 450, 51, 15, p);//数量出库数量
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                            LODOP.ADD_PRINT_TEXT(95 + i * 25, 515, 58, 15, griddata[i].Batch);
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                            LODOP.ADD_PRINT_TEXT(95 + i * 25, 580, 106, 15, griddata[i].ContainerCode);//货位代码
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                            LODOP.ADD_PRINT_TEXT(95 + i * 25, 668, 69, 15, griddata[i].AreaName);//所在区域
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        }

                        LODOP.ADD_PRINT_TEXT(139 + (griddata.length - 1) * 25, 50, 57, 23, "备注：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(119 + (griddata.length - 1) * 25, 120, 647, 53, $('#ARemark').val());
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(185 + (griddata.length - 1) * 25, 50, 57, 23, "仓库：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(185 + (griddata.length - 1) * 25, 337, 100, 20, "制表：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(185 + (griddata.length - 1) * 25, 574, 200, 20, AllDateTime(nowdate));
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_RECT(115 + (griddata.length - 1) * 25, 3, 768, 65, 0, 1);

                    } else {
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_BARCODE(-1, 688, 80, 80, "QRCode", $('#OrderNo').val());
                        LODOP.ADD_PRINT_IMAGE(-3, 47, 198, 49, "<img src=\"../CSS/image/dlqff.jpg\"/>");
                        LODOP.ADD_PRINT_RECT(66, 3, 99, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(66, 102, 79, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(66, 181, 99, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(66, 280, 99, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(66, 379, 50, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(66, 429, 83, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(66, 512, 74, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(66, 586, 96, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(66, 682, 106, 25, 0, 1);

                        LODOP.ADD_PRINT_TEXT(70, 21, 78, 25, "产品名称");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(70, 118, 52, 25, "型号");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(70, 202, 65, 25, "规格");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(70, 292, 53, 25, "花纹");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(70, 383, 61, 25, "载速");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(70, 448, 64, 25, "出库数量");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

                        LODOP.ADD_PRINT_TEXT(70, 522, 60, 25, "批次");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(70, 590, 88, 25, "货位代码");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(70, 684, 72, 25, "所在区域");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

                        LODOP.ADD_PRINT_TEXT(41, 182, 90, 20, "到达站：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(41, 237, 200, 20, $('#ADest').combobox('getText'));
                        //LODOP.ADD_PRINT_TEXT(41, 237, 200, 20, $('#ADest').combobox('getText') + " 物流：" + $('#ALogisID').combobox('getText'));
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        var js = 0, Alltotal = 0, AllPiece = 0;
                        for (var i = 0; i < griddata.length; i++) {
                            js++;
                            var p = Number(griddata[i].Piece);
                            if (griddata[i].TypeParentID == 10 || griddata[i].TypeParentID == 133) {
                                if (griddata[i].PackageNum != 0) {
                                    var num = p / griddata[i].PackageNum;
                                    var b = (num + "").split(".");
                                    if (b.length > 1) {
                                        p = p + "/" + b[0] + griddata[i].Package + (p - b[0] * griddata[i].PackageNum);
                                    } else {
                                        p = p + "/" + b[0] + griddata[i].Package;
                                    }
                                }
                            }

                            LODOP.ADD_PRINT_RECT(90 + i * 25, 3, 99, 25, 0, 1);
                            LODOP.ADD_PRINT_RECT(90 + i * 25, 102, 79, 25, 0, 1);
                            LODOP.ADD_PRINT_RECT(90 + i * 25, 181, 99, 25, 0, 1);
                            LODOP.ADD_PRINT_RECT(90 + i * 25, 280, 99, 25, 0, 1);
                            LODOP.ADD_PRINT_RECT(90 + i * 25, 379, 50, 25, 0, 1);
                            LODOP.ADD_PRINT_RECT(90 + i * 25, 429, 83, 25, 0, 1);
                            LODOP.ADD_PRINT_RECT(90 + i * 25, 512, 74, 25, 0, 1);
                            LODOP.ADD_PRINT_RECT(90 + i * 25, 586, 96, 25, 0, 1);
                            LODOP.ADD_PRINT_RECT(90 + i * 25, 682, 106, 25, 0, 1);
                            LODOP.ADD_PRINT_TEXT(95 + i * 25, 5, 111, 23, griddata[i].TypeName);//产品类型
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                            LODOP.ADD_PRINT_TEXT(95 + i * 25, 105, 80, 20, griddata[i].Model);//型号
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                            LODOP.ADD_PRINT_TEXT(95 + i * 25, 185, 94, 20, griddata[i].Specs);//规格 
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                            LODOP.ADD_PRINT_TEXT(95 + i * 25, 286, 110, 20, griddata[i].Figure);//花纹
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                            LODOP.ADD_PRINT_TEXT(95 + i * 25, 386, 82, 20, griddata[i].LoadIndex + griddata[i].SpeedLevel);//速度级别
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                            LODOP.ADD_PRINT_TEXT(95 + i * 25, 435, 83, 20, p);//数量出库数量
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                            LODOP.ADD_PRINT_TEXT(95 + i * 25, 520, 100, 20, griddata[i].Batch);
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                            LODOP.ADD_PRINT_TEXT(95 + i * 25, 590, 106, 20, griddata[i].ContainerCode);//货位代码
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                            LODOP.ADD_PRINT_TEXT(95 + i * 25, 684, 69, 20, griddata[i].AreaName);//所在区域
                            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        }


                        LODOP.ADD_PRINT_TEXT(124 + (griddata.length - 1) * 25, 50, 102, 23, "备注：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(124 + (griddata.length - 1) * 25, 150, 400, 23, $('#ARemark').val());
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                        LODOP.ADD_PRINT_TEXT(150 + (griddata.length - 1) * 25, 50, 102, 23, "仓库：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(150 + (griddata.length - 1) * 25, 337, 100, 20, "制表：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(150 + (griddata.length - 1) * 25, 574, 200, 20, AllDateTime(nowdate));
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                        //LODOP.ADD_PRINT_TEXT(124 + (griddata.length - 1) * 25, 50, 102, 23, "仓库：");
                        //LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        //LODOP.ADD_PRINT_TEXT(124 + (griddata.length - 1) * 25, 337, 100, 20, "制表：");
                        //LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        //LODOP.ADD_PRINT_TEXT(124 + (griddata.length - 1) * 25, 574, 200, 20, AllDateTime(nowdate));
                        //LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    }
                }
                LODOP.PREVIEW();
                //LODOP.PRINT_DESIGN();
            }
            //LODOP.PREVIEW();
        }
    </script>

</asp:Content>
