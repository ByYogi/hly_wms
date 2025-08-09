<%@ Page Title="应收账款管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="financeReceiveMoneyManager.aspx.cs" Inherits="Cargo.Finance.financeReceiveMoneyManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/easy/js/datagrid-groupview.js" type="text/javascript"></script>
    <%--<script src="../JS/Date/CheckActivX.js" type="text/javascript"></script>--%>
    <script src="../JS/Lodop/LodopFuncs.js" type="text/javascript"></script>
    <script type="text/javascript">
        //页面加载显示遮罩层
        var pc;
        $.parser.onComplete = function () {
            if (pc) {
                clearTimeout(pc);
            }
            pc = setTimeout(closemask, 10);
        }
        //关闭加载中遮罩层
        function closemask() {
            $("#Loading").fadeOut("normal", function () {
                $(this).remove();
            });
        }
        window.onload = function () {
            adjustment();
            var HID = "<%=UserInfor.HouseID%>";
            //如果是广州仓库显示公司类型选项
            if (HID == "9" || HID == "15") {
                $("[name='BelongHouse']").show();
                $('#BelongHouse').combobox('setValue', 0);
            } else {
                $('#BelongHouse').combobox('setValue', -1);
                $("[name='BelongHouse']").hide();
            }
        }
        $(window).resize(function () {
            adjustment();
        });
        function adjustment() {
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true);
            $('#dg').datagrid({ height: height });
        }
        $(document).ready(function () {
            var houseid ="<%=UserInfor.HouseID%>";
            var columns = [];
            columns.push({
                title: '', field: 'OrderID', checkbox: true, width: '30px'
            });
            columns.push({
                field: 'opt', title: '操作', width: '70px', align: 'left',
                formatter: function (value, row, index) {
                    if (value == undefined) {
                        if (row.FinanceSecondCheck == "1") {

                            return '<a class="delcls" onclick="Deny(\'' + index + '\')" href="javascript:void(0)">未审</a>';
                        } else {

                            return '<a class="editcls" onclick="check(\'' + index + '\')" href="javascript:void(0)">审核</a>';
                        }
                    }
                }
            });
            columns.push({
                title: '出库仓库', field: 'HouseName', width: '80px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            columns.push({
                title: '开单时间', field: 'CreateDate', width: '130px', formatter: DateTimeFormatter
            });
            columns.push({
                title: '订单号', field: 'OrderNo', width: '90px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            if (houseid == 93 || houseid == 95 || houseid == 96 || houseid == 97 || houseid == 98 || houseid == 99 || houseid == 100 || houseid == 101 || houseid == 102 || houseid == 103 || houseid == 104 || houseid == 105 || houseid == 106 || houseid == 107 || houseid == 108 || houseid == 109 || houseid == 110 || houseid == 111 || houseid == 112 || houseid == 113 || houseid == 114 || houseid == 115 || houseid == 116 || houseid == 117 || houseid == 118 || houseid == 119 || houseid == 120 || houseid == 121 || houseid == 122 || houseid == 123 || houseid == 124 || houseid == 125 || houseid == 126 || houseid == 127 || houseid == 128 || houseid == 129 || houseid == 130 || houseid == 131 || houseid == 132 || houseid == 133 || houseid == 134 || houseid == 135 || houseid == 91) {
                columns.push({
                    title: '数量', field: 'Piece', width: '40px', align: 'right', formatter: function (val, row) {
                        if (val != null && val != "") {
                            if (row.OrderModel == "1") {
                                return "-" + val;
                            } else { return val; }
                        }
                    }
                });
                columns.push({
                    title: '轮胎收入', field: 'TransportFee', width: '60px', align: 'right',
                    formatter: function (val, row, index) {
                        if (row.OrderModel == "1") {
                            return "-" + val;
                        } else { return val; }
                    }
                });
                columns.push({
                    title: '配送费', field: 'TransitFee', width: '60px', align: 'right', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '平台费', field: 'OtherFee', width: '60px', align: 'right', formatter: function (val, row, index) {
                        if (row.OrderModel == "1") {
                            return "-" + val;
                        } else { return val; }
                    }
                });
                //columns.push({
                //    title: '物流费', field: 'DeliveryFee', width: '60px', align: 'right', formatter: function (value) {
                //        if (value != null && value != "") {
                //            return "<span title='" + value + "'>" + value + "</span>";
                //        }
                //    }
                //});
                columns.push({
                    title: '优惠券', field: 'InsuranceFee', width: '60px', align: 'right', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '超期费', field: 'OverDueFee', width: '60px', align: 'right', formatter: function (val, row, index) {
                        if (row.OrderModel == "1") {
                            return "-" + val;
                        } else { return val; }
                    }
                });
                columns.push({
                    title: '合计', field: 'TotalCharge', width: '70px', align: 'right', formatter: function (val, row, index) {
                        if (row.OrderModel == "1") {
                            return "-" + (val + row.DeliveryFee);
                        } else { return val + row.DeliveryFee; }
                    }
                });
            }
            else {

                columns.push({
                    title: '数量', field: 'Piece', width: '40px', align: 'right', formatter: function (val, row) {
                        if (val != null && val != "") {
                            if (row.OrderModel == "1") {
                                return "-" + val;
                            } else { return val; }
                        }
                    }
                });
                columns.push({
                    title: '收入', field: 'TransportFee', width: '60px', align: 'right',
                    formatter: function (val, row, index) {
                        if (row.OrderModel == "1") {
                            return "-" + val;
                        } else { return val; }
                    }
                });
                columns.push({
                    title: '物流费', field: 'DeliveryFee', width: '60px', align: 'right', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                if (<%=UserInfor.HouseID%>!= 64) {
                    columns.push({
                        title: '优惠券', field: 'InsuranceFee', width: '60px', align: 'right', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                }
                columns.push({
                    title: '合计', field: 'TotalCharge', width: '70px', align: 'right', formatter: function (val, row, index) {
                        if (row.OrderModel == "1") {
                            return "-" + (val + row.DeliveryFee);
                        } else { return val + row.DeliveryFee; }
                    }
                });
            }


            columns.push({
                title: '审核状态', field: 'FinanceSecondCheck', width: '60px',
                formatter: function (val, row, index) { if (val == "0") { return "<span title='未审'>未审</span>"; } else if (val == "1") { return "<span title='已审'>已审</span>"; } else { return ""; } }
            });
            columns.push({
                title: '结算状态', field: 'CheckStatus', width: '60px',
                formatter: function (val, row, index) { if (val == "0") { return "<span title='未结算' style='color: #df5555;'>未结算</span>"; } else if (val == "1") { return "<span title='已结清' style='color: #5dc950;'>已结清</span>"; } else if (val == "2") { return "<span title='未结清' style='color: #fba852;'>未结清</span>"; } else { return ""; } }
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
            columns.push({
                title: '客户名称', field: 'PayClientName', width: '60px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            //columns.push({
            //    title: '公司名称', field: 'AcceptUnit', width: '120px', formatter: function (value) {
            //        if (value != null && value != "") {
            //            return "<span title='" + value + "'>" + value + "</span>";
            //        }
            //    }
            //});
            columns.push({
                title: '收货人', field: 'AcceptPeople', width: '60px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            //columns.push({
            //    title: '联系手机', field: 'AcceptCellphone', width: '90px', formatter: function (value) {
            //        if (value != null && value != "") {
            //            return "<span title='" + value + "'>" + value + "</span>";
            //        }
            //    }
            //});
            //columns.push({
            //    title: '收货地址', field: 'AcceptAddress', width: '120px', formatter: function (value) {
            //        if (value != null && value != "") {
            //            return "<span title='" + value + "'>" + value + "</span>";
            //        }
            //    }
            //});


            columns.push({
                title: '订单状态', field: 'AwbStatus', width: '60px',
                formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='已下单'>已下单</span>"; }
                    else if (val == "1") { return "<span title='出库中'>出库中</span>"; }
                    else if (val == "2") { return "<span title='已出库'>已出库</span>"; }
                    else if (val == "3") { return "<span title='运输在途'>运输在途</span>"; }
                    else if (val == "4") { return "<span title='已到达'>已到达</span>"; }
                    else if (val == "5") { return "<span title='已签收'>已签收</span>"; }
                    else { return ""; }
                }
            });
            columns.push({
                title: '送货方式', field: 'DeliveryType', width: '60px', formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='急送'>急送</span>"; }
                    else if (val == "1") { return "<span title='自提'>自提</span>"; }
                    else if (val == "2") { return "<span title='普送'>普送</span>"; }
                    else { return ""; }
                }
            });
            columns.push({
                title: '下单方式', field: 'OrderType', width: '70px', formatter: function (val, row, index) {
                    if (val != '合计') {
                        if (val == "0") { return "<span title='电脑单'>电脑单</span>"; } else if (val == "1") { return "<span title='企业微信单'>企业微信单</span>"; } else if (val == "2") { return "<span title='微信商城单'>微信商城单</span>"; } else if (val == "3") { return "<span title='APP单'>APP单</span>"; } else if (val == "4") { return "<span title='小程序单'>小程序单</span>"; } else { return "<span title='电脑单'>电脑单</span>"; }
                    }
                }
            });
            columns.push({
                title: '供应商', field: 'SuppClientName', width: '90px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            columns.push({
                title: '优惠券类型', field: 'CouponType', width: '60px', formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='平台券'>平台券</span>"; }
                    else if (val == "1") { return "<span title='供应商券'>供应商券</span>"; }
                    else { return ""; }
                }
            });
            columns.push({
                title: '商城订单号', field: 'WXOrderNo', width: '110px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            columns.push({
                title: '付款方式', field: 'PayWay', width: '60px', formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='微信付款'>微信付款</span>"; } else if (val == "1") { return "<span title='额度付款'>额度付款</span>"; } else { return ""; }
                }
            });
            columns.push({
                title: '支付订单号', field: 'WXPayOrderNo', width: '190px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            <%--if (<%=UserInfor.HouseID%>== 64) {
                columns.push({
                    title: '结算方式', field: 'InsuranceFee', width: '60px', align: 'right', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '结算金额', field: 'InsuranceFee', width: '60px', align: 'right', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '余额', field: 'InsuranceFee', width: '60px', align: 'right', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '结算时间', field: 'CreateDate', width: '130px', formatter: DateTimeFormatter
                });
            }--%>


            //columns.push({
            //    title: '出发站', field: 'Dep', width: '50px', formatter: function (value) {
            //        if (value != null && value != "") {
            //            return "<span title='" + value + "'>" + value + "</span>";
            //        }
            //    }
            //});
            //columns.push({
            //    title: '到达站', field: 'Dest', width: '50px', formatter: function (value) {
            //        if (value != null && value != "") {
            //            return "<span title='" + value + "'>" + value + "</span>";
            //        }
            //    }
            //});
            columns.push({
                title: '业务员', field: 'SaleManName', width: '60px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });

            columns.push({
                title: '订单类型', field: 'OrderModel', width: '60px', formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='订货单'>订货单</span>"; } else if (val == "1") { return "<span title='退货单'>退货单</span>"; } else { return ""; }
                }
            });
            columns.push({
                title: '开单员', field: 'CreateAwb', width: '60px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });


            columns.push({
                title: '物流公司单号', field: 'LogisAwbNo', width: '90px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            columns.push({
                title: '物流配送费用', field: 'TransitFee', width: '80px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            columns.push({
                title: '物流公司名称', field: 'LogisticName', width: '90px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            $('#dg').datagrid({
                width: '100%',
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2, 200, 500],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OrderID',
                url: null,
                showFooter: true,
                toolbar: '#dgtoolbar',
                columns: [columns],
                onLoadSuccess: function (data) {
                    $('.editcls').linkbutton({ text: '审核', plain: true, iconCls: 'icon-ok' });
                    $('.delcls').linkbutton({ text: '未审', plain: true, iconCls: 'icon-no' });
                    $(this).datagrid('resize');
                    $('#dg').datagrid('reloadFooter', [{ opt: '合计', Piece: '', TransportFee: '', OrderType: '合计' }]);
                },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                    if (row.OrderModel == "1") {
                        InsuranceFee = "-" + row.InsuranceFee;
                        DeliveryFee = "-" + row.DeliveryFee;
                        TransportFee = "-" + row.TransportFee;
                        Piece = "-" + row.Piece;
                        TotalCharge = "-" + row.TotalCharge;
                        TransitFee = "-" + row.TransitFee;
                        OtherFee = "-" + row.OtherFee;
                        OverDueFee = "-" + row.OverDueFee;


                    } else {
                        InsuranceFee = row.InsuranceFee;
                        DeliveryFee = row.DeliveryFee;
                        TransportFee = row.TransportFee;
                        Piece = row.Piece;
                        TotalCharge = row.TotalCharge;
                        TransitFee = row.TransitFee;
                        OtherFee = row.OtherFee;
                        OverDueFee = row.OverDueFee;
                    }
                    $('#dg').datagrid('reloadFooter', [{ opt: '合计', OrderNo: '合计', Piece: Piece, TransportFee: TransportFee, TransitFee: TransitFee, OtherFee: OtherFee, OverDueFee: OverDueFee, DeliveryFee: DeliveryFee, InsuranceFee: InsuranceFee, TotalCharge: TotalCharge, OrderType: '合计' }]);
                },
                onDblClickRow: function (index, row) {
                    editItemByID(index);
                },
                onCheck: function (index, row) {
                    var rows = $('#dg').datagrid('getSelections');
                    TransportFee = 0;
                    DeliveryFee = 0;
                    InsuranceFee = 0;
                    Piece = 0;
                    TotalCharge = 0;
                    TransitFee = 0;
                    OtherFee = 0;
                    OverDueFee = 0;
                    for (var i = 0; i < rows.length; i++) {
                        if (rows[i].OrderModel == "1") {
                            InsuranceFee -= rows[i].InsuranceFee;
                            DeliveryFee -= rows[i].DeliveryFee;
                            TransportFee -= rows[i].TransportFee;
                            Piece -= rows[i].Piece;
                            TotalCharge -= rows[i].TotalCharge;
                            TransitFee -= rows[i].TransitFee;
                            OtherFee -= rows[i].OtherFee;
                            OverDueFee -= rows[i].OverDueFee;

                        } else {
                            InsuranceFee += rows[i].InsuranceFee;
                            DeliveryFee += rows[i].DeliveryFee;
                            TransportFee += rows[i].TransportFee;
                            Piece += rows[i].Piece;
                            TotalCharge += rows[i].TotalCharge;
                            TransitFee += rows[i].TransitFee;
                            OtherFee += rows[i].OtherFee;
                            OverDueFee += rows[i].OverDueFee;
                        }
                    }
                    $('#dg').datagrid('reloadFooter', [{ opt: '合计', OrderNo: '合计', Piece: Piece, TransportFee: TransportFee, TransitFee: TransitFee, OtherFee: OtherFee, OverDueFee: OverDueFee, DeliveryFee: DeliveryFee, InsuranceFee: InsuranceFee, TotalCharge: TotalCharge, OrderType: '合计' }]);
                },
                onUncheck: function (index, row) {
                    if (row.OrderModel == "1") {
                        InsuranceFee -= row.InsuranceFee;
                        TransportFee -= row.TransportFee;
                        DeliveryFee -= row.DeliveryFee;
                        Piece -= row.Piece;
                        TotalCharge -= row.TotalCharge;
                        TransitFee -= row.TransitFee;
                        OtherFee -= row.OtherFee;
                        OverDueFee -= row.OverDueFee;
                    } else {
                        InsuranceFee += row.InsuranceFee;
                        TransportFee += row.TransportFee;
                        DeliveryFee += row.DeliveryFee;
                        Piece += row.Piece;
                        TotalCharge += row.TotalCharge;
                        TransitFee += row.TransitFee;
                        OtherFee += row.OtherFee;
                        OverDueFee += row.OverDueFee;
                    }
                    $('#dg').datagrid('reloadFooter', [{ opt: '合计', OrderNo: '合计', Piece: Piece, TransportFee: TransportFee, TransitFee: TransitFee, OtherFee: OtherFee, OverDueFee: OverDueFee, DeliveryFee: DeliveryFee, InsuranceFee: InsuranceFee, TotalCharge: TotalCharge, OrderType: '合计' }]);
                },
                onCheckAll: function (index, row) {
                    var rows = $('#dg').datagrid('getSelections');
                    TransportFee = 0;
                    DeliveryFee = 0;
                    InsuranceFee = 0;
                    Piece = 0;
                    TotalCharge = 0;
                    TransitFee = 0;
                    OtherFee = 0;
                    OverDueFee = 0;
                    for (var i = 0; i < rows.length; i++) {
                        //var DeliveryFee = rows[i].DeliveryFee;
                        if (rows[i].OrderModel == "1") {
                            InsuranceFee -= rows[i].InsuranceFee;
                            DeliveryFee -= rows[i].DeliveryFee;
                            TransportFee -= rows[i].TransportFee;
                            Piece -= rows[i].Piece;
                            TotalCharge -= rows[i].TotalCharge;
                            TransitFee -= rows[i].TransitFee;
                            OtherFee -= rows[i].OtherFee;
                            OverDueFee -= rows[i].OverDueFee;
                        } else {
                            InsuranceFee += rows[i].InsuranceFee;
                            DeliveryFee += rows[i].DeliveryFee;
                            TransportFee += rows[i].TransportFee;
                            Piece += rows[i].Piece;
                            TotalCharge += rows[i].TotalCharge;
                            TransitFee += rows[i].TransitFee;
                            OtherFee += rows[i].OtherFee;
                            OverDueFee += rows[i].OverDueFee;
                        }
                    }
                    $('#dg').datagrid('reloadFooter', [{ opt: '合计', OrderNo: '合计', Piece: Piece, TransportFee: TransportFee, TransitFee: TransitFee, OtherFee: OtherFee, OverDueFee: OverDueFee, DeliveryFee: DeliveryFee, InsuranceFee: InsuranceFee, TotalCharge: TotalCharge, OrderType: '合计' }]);
                },
                onUncheckAll: function (index, row) {
                    var rows = $('#dg').datagrid('getSelections');
                    TransportFee = 0;
                    DeliveryFee = 0;
                    InsuranceFee = 0;
                    Piece = 0;
                    TotalCharge = 0;
                    TransitFee = 0;
                    OtherFee = 0;
                    OverDueFee = 0;
                    for (var i = 0; i < rows.length; i++) {
                        //var DeliveryFee = rows[i].DeliveryFee;
                        if (rows[i].OrderModel == "1") {
                            InsuranceFee -= rows[i].InsuranceFee;
                            DeliveryFee -= rows[i].DeliveryFee;
                            TransportFee -= rows[i].TransportFee;
                            Piece -= rows[i].Piece;
                            TotalCharge -= rows[i].TotalCharge;
                            TransitFee -= rows[i].TransitFee;
                            OtherFee -= rows[i].OtherFee;
                            OverDueFee -= rows[i].OverDueFee;
                        } else {
                            InsuranceFee += rows[i].InsuranceFee;
                            DeliveryFee += rows[i].DeliveryFee;
                            TransportFee += rows[i].TransportFee;
                            Piece += rows[i].Piece;
                            TotalCharge += rows[i].TotalCharge;
                            TransitFee += rows[i].TransitFee;
                            OtherFee += rows[i].OtherFee;
                            OverDueFee += rows[i].OverDueFee;
                        }
                    }
                    $('#dg').datagrid('reloadFooter', [{ opt: '合计', OrderNo: '合计', Piece: Piece, TransportFee: TransportFee, TransportFee, TransitFee: TransitFee, OtherFee: OtherFee, OverDueFee: OverDueFee, DeliveryFee: DeliveryFee, InsuranceFee: InsuranceFee, TotalCharge: TotalCharge, OrderType: '合计' }]);
                },
                rowStyler: function (index, row) {
                    if (row.AwbStatus == "5") { return "color:#2a83de"; };
                    if (row.AwbStatus == "1") { return "background-color:#f8e8e7"; };
                }
            });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#StartDate').datebox('textbox').bind('focus', function () { $('#StartDate').datebox('showPanel'); });
            $('#EndDate').datebox('textbox').bind('focus', function () { $('#EndDate').datebox('showPanel'); });
            //$('#Dep').combobox('textbox').bind('focus', function () { $('#Dep').combobox('showPanel'); });
            //$('#Dest').combobox('textbox').bind('focus', function () { $('#Dest').combobox('showPanel'); });
            $('#AOrderType').combobox('textbox').bind('focus', function () { $('#AOrderType').combobox('showPanel'); });
            //$('#CheckOutType').combobox('textbox').bind('focus', function () { $('#CheckOutType').combobox('showPanel'); });
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    var HID = rec.HouseID;
                    //如果是广州仓库显示公司类型选项
                    if (HID == 9 || HID == 15) {
                        $("[name='BelongHouse']").show();
                        $('#BelongHouse').combobox('setValue', 0);
                    } else {
                        $('#BelongHouse').combobox('setValue', -1);
                        $("[name='BelongHouse']").hide();
                    }
                    $('#ASaleManID').combobox('clear');
                    var url = '../Order/orderApi.aspx?method=QueryUserByDepCode&houseID=' + rec.HouseID;
                    $('#ASaleManID').combobox('reload', url);
                }
            });
            $('#PayClientNum').combobox({
                url: '../Client/clientApi.aspx?method=AutoCompleteClient', valueField: 'ClientNum', textField: 'Boss', AddField: 'PinyinName',
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                },
            });
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            $('#ASaleManID').combobox('textbox').bind('focus', function () { $('#ASaleManID').combobox('showPanel'); });
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            $('#ASaleManID').combobox('clear');
            var url = '../Order/orderApi.aspx?method=QueryUserByDepCode&houseID=<%=UserInfor.HouseID%>';
            $('#ASaleManID').combobox('reload', url);
            $('#ABusinessID').combobox('clear');
            var Burl = '../Client/clientApi.aspx?method=QueryBusinessIDDefault';
            $('#ABusinessID').combobox('reload', Burl);
            

            $('#ASettleHouseID').combobox({
                url: '../Client/clientApi.aspx?method=AutoCompleteClient&SettleHouseID=1', valueField: 'ClientNum', textField: 'ClientShortName', AddField: 'PinyinName',
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                },
            });
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../Order/orderApi.aspx?method=QueryOrderInfo';
            $('#dg').datagrid('load', {
                OrderNo: $('#AOrderNo').val(),
                AcceptPeople: $("#AcceptPeople").val(),
                PayClientNum: $("#PayClientNum").combobox('getValue'),
                //PayClientName: $("#PayClientNum").combobox('getText'),
                Piece: $("#Piece").val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                CheckOutType: '',
                CheckStatus: $("#CheckStatus").combobox('getValue'),
                FinanceSecondCheck: $("#FinanceSecondCheck").combobox('getValue'),
                OrderType: $("#AOrderType").combobox('getValue'),
                Dep: "",//$("#Dep").combobox('getValue'),
                Dest: "",//$("#Dest").combobox('getValue'),
                SuppClientNum: $("#ASettleHouseID").combobox('getValue'),//供应商
                HouseID: $("#AHouseID").combobox('getValue'),//仓库ID
                SaleManID: $('#ASaleManID').combobox('getValue'),
                OrderModel: $("#AOrderModel").combobox('getValue'),//订单类型
                AwbStatus: $("#AAwbStatus").combobox('getValue'),//订单状态
                BelongHouse: $('#BelongHouse').combobox('getValue'),//公司类型
                BusinessID: $('#ABusinessID').combobox('getValue'),//业务名称
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
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table>
            <tr>
                <td style="text-align: right;">订单号:
                </td>
                <td>
                    <input id="AOrderNo" class="easyui-textbox" data-options="prompt:'请输入订单号'" style="width: 100px">
                </td>


                <td style="text-align: right;">客户名:
                </td>
                <td>
                    <input id="PayClientNum" style="width: 100px;" class="easyui-combobox" />
                </td>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;" />
                </td>
                <td style="text-align: right;">下单方式:
                </td>
                <td>
                    <select class="easyui-combobox" id="AOrderType" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">电脑单</option>
                        <option value="2">微信商城单</option>
                        <option value="3">APP单</option>
                        <option value="4">小程序单</option>
                        <option value="1">企业微信单</option>
                    </select>
                </td>

                <td style="text-align: right;">订单状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="AAwbStatus" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">已下单</option>
                        <option value="6">已拣货</option>
                        <option value="1">出库中</option>
                        <option value="2">已出库</option>
                        <option value="3">已装车</option>
                        <%-- <option value="4">已到达</option>--%>
                        <option value="5">已签收</option>
                    </select>
                </td>

                <%--<td style="text-align: right;">出发站:
                </td>
                <td>
                    <input id="Dep" class="easyui-combobox" style="width: 80px;" data-options="valueField:'CityName',textField:'CityName',url:'../systempage/sysService.aspx?method=QueryAllCity',multiple:true" />
                </td>
                <td style="text-align: right;">到达站:
                </td>
                <td>
                    <input id="Dest" class="easyui-combobox" style="width: 80px;" data-options="valueField:'CityName',textField:'CityName',url:'../systempage/sysService.aspx?method=QueryAllCity'" />
                </td>--%>
                <td style="text-align: right;">开单时间:
                </td>
                <td colspan="3">
                    <input id="StartDate" class="easyui-datebox" style="width: 100px" />~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">业务员:
                </td>
                <td>
                    <input name="SaleManID" id="ASaleManID" class="easyui-combobox" style="width: 100px;"
                        data-options="valueField: 'LoginName',textField: 'UserName'" />
                </td>
                <td style="text-align: right;">收货人:
                </td>
                <td>
                    <input id="AcceptPeople" class="easyui-textbox" data-options="prompt:'收货单位/人'" style="width: 100px;" />
                </td>
                <td style="text-align: right;">供应商:
                </td>
                <td>
                    <input id="ASettleHouseID" class="easyui-combobox" style="width: 100px;" />
                </td>
                <td style="text-align: right;">结算状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="CheckStatus" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">未结算</option>
                        <option value="1">已结清</option>
                        <option value="2">未结清</option>
                    </select>
                </td>
                <td style="text-align: right;">审核状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="FinanceSecondCheck" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">未审核</option>
                        <option value="1">已审核</option>
                    </select>
                </td>
                <td style="text-align: right;">订单类型:
                </td>
                <td>
                    <select class="easyui-combobox" id="AOrderModel" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">销售单</option>
                        <option value="1">退货单</option>
                    </select>
                </td>
                  <td style="text-align: right;">业务名称:
                </td>
                <td>
                     <input id="ABusinessID" class="easyui-combobox" style="width: 150px;"
                        data-options="valueField: 'ID',textField: 'DepName'" />
                </td>

                <td name="BelongHouse" style="text-align: right;">公司类型:
                </td>
                <td name="BelongHouse">
                    <select class="easyui-combobox" id="BelongHouse" style="width: 80px;" panelheight="auto">
                        <option value="0">迪乐泰</option>
                        <option value="1">好来运</option>
                    </select>
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="dgtoolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-lock_add"
            plain="false" onclick="plcheck()">&nbsp;批量审核</a>&nbsp;&nbsp; <a href="#" class="easyui-linkbutton"
                iconcls="icon-lock_open" plain="false" onclick="plDeny()">&nbsp;批量未审</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-application_put"
                    plain="false" onclick="AwbExport()">&nbsp;导&nbsp;出</a>
        &nbsp;&nbsp;
                    <form runat="server" id="fm1">
                        <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
                    </form>
    </div>
    <input type="hidden" id="DisplayNum" />
    <input type="hidden" id="DisplayPiece" />
    <div id="dlgOrder" class="easyui-dialog" style="width: 1020px; height: 540px;" closed="true"
        closable="false" buttons="#dlgOrder-buttons">
        <%--<table id="outDg" class="easyui-datagrid">
        </table>--%>
        <form id="fmDep" class="easyui-form" method="post">
            <input type="hidden" name="SaleManName" id="SaleManName" />
            <input type="hidden" name="SaleCellPhone" id="SaleCellPhone" />
            <input type="hidden" name="HouseCode" id="HouseCode" />
            <input type="hidden" name="HouseID" id="HouseID" />
            <input type="hidden" name="ONum" id="ONum" />
            <input type="hidden" name="OutNum" id="OutNum" />
            <input type="hidden" name="OrderID" id="OrderID" />
            <input type="hidden" name="OrderNo" id="OrderNo" />
            <input type="hidden" name="ClientNum" id="ClientNum" />
            <input type="hidden" id="HiddenClientSelectName" name="HiddenClientSelectName" />
            <div id="saPanel">
                <table style="width: 100%">
                    <tr>

                        <td style="color: Red; font-weight: bolder; text-align: right;">出发站:
                        </td>
                        <td>
                            <input name="Dep" id="ADep" class="easyui-textbox" readonly="readonly" style="width: 60px">
                            <%-- <input name="Dep" id="ADep" class="easyui-combobox" style="width: 80px;" data-options="valueField:'CityName',textField:'CityName',url:'../systempage/sysService.aspx?method=QueryAllCity',required:true "
                                    panelheight="auto" />--%>
                        </td>
                        <td style="color: Red; font-weight: bolder; text-align: right;">到达站:
                        </td>
                        <td>
                            <input name="Dest" id="ADest" class="easyui-combobox" style="width: 60px;" data-options="valueField:'CityName',textField:'CityName',url:'../systempage/sysService.aspx?method=QueryAllCity',required:true "
                                panelheight="auto" />
                        </td>
                        <td style="text-align: right;">收货人:
                        </td>
                        <td>
                            <input name="AcceptPeople" id="AAcceptPeople" style="width: 80px;" class="easyui-combobox" />
                        </td>
                        <td style="text-align: right;">客户名称:
                        </td>
                        <td>
                            <input name="AcceptUnit" id="AAcceptUnit" class="easyui-textbox" style="width: 80px;" />
                        </td>

                        <td style="text-align: right;">收货地址:
                        </td>
                        <td>
                            <input name="AcceptAddress" id="AAcceptAddress" style="width: 100px;" class="easyui-textbox" />
                        </td>
                        <td style="text-align: right;">电话:
                        </td>
                        <td>
                            <input name="AcceptTelephone" id="AAcceptTelephone" class="easyui-textbox"
                                style="width: 80px;" />
                        </td>
                        <td style="text-align: right;">手机:
                        </td>
                        <td>
                            <input name="AcceptCellphone" id="AAcceptCellphone" class="easyui-textbox" data-options="required:true" style="width: 100px;" />
                        </td>

                    </tr>

                    <tr>
                        <td style="text-align: right;">销售费用:
                        </td>
                        <td>
                            <input name="TransportFee" id="TransportFee" data-options="min:0,precision:2" class="easyui-numberbox"
                                style="width: 60px;" />
                        </td>
                        <td style="text-align: right;">送货费用:
                        </td>
                        <td>
                            <input name="TransitFee" id="TransitFee" data-options="min:0,precision:2" class="easyui-numberbox" readonly="readonly"
                                style="width: 60px;" />
                        </td>
                        <td style="text-align: right;">提货费用:
                        </td>
                        <td>
                            <input name="DeliveryFee" id="DeliveryFee" data-options="min:0,precision:2" class="easyui-numberbox"
                                style="width: 80px;" />
                        </td>
                        <td style="text-align: right;">其它费用:
                        </td>
                        <td>
                            <input name="OtherFee" id="OtherFee" class="easyui-numberbox" data-options="min:0,precision:2"
                                style="width: 80px;" />
                        </td>
                        <td style="text-align: right; color: #f53333">优惠券:
                        </td>
                        <td>
                            <select name="InsuranceFee" id="InsuranceFee" class="easyui-combogrid" style="width: 100px">
                            </select>
                            <%--  <input name="InsuranceFee" id="InsuranceFee" data-options="min:0,precision:2" class="easyui-numberbox"
                                style="width: 100px;" />--%>
                        </td>
                        <td style="text-align: right;">回扣:
                        </td>
                        <td>
                            <input name="Rebate" id="Rebate" data-options="min:0,precision:2" class="easyui-numberbox"
                                style="width: 80px;" />
                        </td>
                        <td style="text-align: right;">合计:
                        </td>
                        <td>
                            <input name="TotalCharge" id="TotalCharge" class="easyui-numberbox" data-options="min:0,precision:2"
                                style="width: 100px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">总件数:
                        </td>
                        <td>
                            <input name="Piece" id="APiece" class="easyui-numberbox" data-options="min:0,precision:0,required:true"
                                style="width: 60px;" />
                        </td>
                        <%--
                            <td style="color: Red; font-weight: bolder; text-align: right;">结款方式:
                            </td>
                            <td colspan="3">
                                <div id="CheckOutType0" style="display: inline">
                                    <input name="CheckOutType" id="cot0" type="radio" value="0" /><label for="cot0" style="font-size: 15px;">现付</label>
                                </div>
                                <div id="CheckOutType3" style="display: inline">
                                    <input name="CheckOutType" id="cot3" type="radio" value="3" /><label for="cot3" style="font-size: 15px;">到付</label>
                                </div>
                                <div id="CheckOutType1" style="display: inline">
                                    <input name="CheckOutType" id="cot1" type="radio" value="1" /><label for="cot1" style="font-size: 15px;">回单</label>
                                </div>
                                <div id="CheckOutType2" style="display: inline">
                                    <input name="CheckOutType" id="cot2" type="radio" value="2" /><label for="cot2" style="font-size: 15px;">月结</label>
                                </div>
                            </td>
                              <td style="text-align: right;">代收款:
                            </td>
                            <td>
                                <input name="CollectMoney" id="CollectMoney" class="easyui-numberbox" data-options="min:0,precision:2"
                                    style="width: 80px;" />元
                            </td>
                            <td style="text-align: right;">回单要求:
                            </td>
                            <td>
                                <input name="ReturnAwb" id="ReturnAwb" class="easyui-numberbox" data-options="min:0,precision:0"
                                    style="width: 60px;" />份
                            </td>--%>
                        <td style="text-align: right;">开单员:
                        </td>
                        <td>
                            <input name="CreateAwb" id="CreateAwb" class="easyui-textbox" readonly="true" style="width: 60px;" />
                        </td>
                        <td style="text-align: right;">开单时间:
                        </td>
                        <td colspan="3">
                            <input name="CreateDate" id="CreateDate" class="easyui-datetimebox" data-options="formatter:AllDateTime"
                                readonly="true" style="width: 150px;" />
                        </td>
                        <%-- <td style="color: Red; font-weight: bolder; text-align: right;">送货方式:
                            </td>
                            <td>
                                <input name="DeliveryType" type="radio" id="dlt1" value="1" /><label for="dlt1" style="font-size: 15px;">自提</label>
                                <input name="DeliveryType" type="radio" id="dlt0" value="0" /><label for="dlt0" style="font-size: 15px;">送货</label>
                            </td>--%>
                        <td style="text-align: right;">业务员:
                        </td>
                        <td>
                            <input name="SaleManID" id="SaleManID" class="easyui-combobox" style="width: 100px;"
                                data-options="url: '../Order/orderApi.aspx?method=QueryUserByDepCode',valueField: 'LoginName',textField: 'UserName', onSelect: onSaleManIDChanged," />
                        </td>
                        <td style="text-align: right;">物流:
                        </td>
                        <td>
                            <input name="LogisID" id="ALogisID" class="easyui-combobox" style="width: 80px;" data-options="valueField:'ID',textField:'LogisticName',url:'../systempage/sysService.aspx?method=QueryAllLogistic'"
                                panelheight="auto" />
                        </td>
                        <td style="text-align: right;">单号:
                        </td>
                        <td>
                            <input name="LogisAwbNo" id="ALogisAwbNo" class="easyui-textbox" style="width: 100px;" />
                        </td>
                    </tr>
                    <tr>

                        <%--                            <td style="color: Red; font-weight: bolder; text-align: right;">运输时效:
                            </td>
                            <td colspan="3">
                                <select name="TimeLimit" id="TimeLimit" panelheight="auto" style="width: 80px;" class="easyui-combobox"
                                    data-options="required:true">
                                    <option value="1">1天</option>
                                    <option value="2">2天</option>
                                    <option value="3">3天</option>
                                    <option value="4">4天</option>
                                    <option value="5">5天</option>
                                    <option value="6">6天</option>
                                    <option value="7">7天</option>
                                    <option value="8">8天</option>
                                    <option value="9">其它</option>
                                </select>
                            </td>--%>
                    </tr>

                    <tr>
                        <%--<td style="text-align: right;" rowspan="2">副单号:
                            </td>
                            <td colspan="4" rowspan="2">
                                <textarea name="HAwbNo" id="AHAwbNo" rows="3" style="width: 300px;"></textarea>
                            </td>--%>
                        <td style="text-align: right;" rowspan="2">备注:
                        </td>
                        <td colspan="7" rowspan="2">
                            <textarea name="Remark" id="ARemark" rows="3" style="width: 400px;"></textarea>
                        </td>

                    </tr>
                    <tr>

                        <td colspan="4"><%--<a href="#" class="easyui-linkbutton" iconcls="icon-ok"
                            plain="false" onclick="saveOutCargo()">&nbsp;保&nbsp;存&nbsp;订&nbsp;单</a>
                            &nbsp;&nbsp;
                                <a href="#" class="easyui-linkbutton" id="undo" iconcls="icon-clear" onclick="reset()">&nbsp;重&nbsp;置&nbsp;</a>--%>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
        <table>
            <tr>
                <td>
                    <table id="dgSave" class="easyui-datagrid">
                    </table>
                </td>
                <td>
                    <table id="outDg" class="easyui-datagrid">
                    </table>
                </td>
            </tr>
        </table>
        <div id="toolbar">
            规格:
            <input id="ASpecs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 100px" />
            型号:<input id="AModel" class="easyui-textbox" data-options="prompt:'请输入产品型号'" style="width: 100px" />

            <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="queryInCargoProduct()">查询</a>
        </div>

    </div>
    <div id="dlgOrder-buttons">
       <%-- <a href="#" class="easyui-linkbutton"
            iconcls="icon-basket_remove" plain="false" onclick="pldown()" id="down">&nbsp;拉下订单&nbsp;</a>&nbsp;&nbsp;<a
                href="#" class="easyui-linkbutton" iconcls="icon-basket_put" plain="false" onclick="plup()"
                id="up">&nbsp;拉上订单&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-ok"
                    onclick="SaveOrderUpdate()" id="save">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" id="payprint" iconcls="icon-printer" onclick="prePrint()">&nbsp;打印发货单&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" id="pickprint" iconcls="icon-printer" onclick="pickUpOrder()">&nbsp;打印拣货单&nbsp;</a>--%>
        <a href="#" class="easyui-linkbutton"
            iconcls="icon-cancel" onclick="closeDlg()">&nbsp;关&nbsp;闭&nbsp;</a>
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
                        <input name="ActSalePrice" id="ActSalePrice" class="easyui-numberbox" data-options="min:0,precision:0"
                            style="width: 200px;">
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlgOutCargo-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="outOK()">&nbsp;确&nbsp;定&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgOutCargo').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <!--End 出库操作-->
    <object id="LODOP_OB" title="dd" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA"
        width="0px" height="0px">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0px" height="0px"></embed>
    </object>
    <script type="text/javascript">
        //导出数据
        function AwbExport() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            var key = new Array();
            key[0] = $('#OrderNo').val();
            key[1] = $("#AcceptPeople").val();
            key[2] = $('#StartDate').datebox('getValue');
            key[3] = $('#EndDate').datebox('getValue');
            //key[4] = $("#Dest").combobox('getValue');
            key[5] = $("#AHouseID").combobox('getValue');
            key[6] = $('#AOrderType').combobox('getValue');
            key[7] = $('#ASaleManID').combobox('getValue');
            key[8] = $('#Piece').val();
            key[9] = '';
            key[10] = $("#FinanceSecondCheck").combobox('getValue');
            //key[11] = $("#Dep").combobox('getValue');
            key[12] = $('#AOrderModel').combobox('getValue');
            key[13] = $("#PayClientNum").combobox('getValue');
            key[14] = $("#ASettleHouseID").combobox('getValue');//供应商
            key[15] = $("#AAwbStatus").combobox('getValue');
            key[16] = $("#CheckStatus").combobox('getValue');

            $.messager.progress({ msg: '请稍后,正在导出中...' });
            $.ajax({
                url: "../Order/orderApi.aspx?method=QueryOrderInfoForExport&key=" + escape(key.toString()),
                success: function (text) {
                    $.messager.progress("close");
                    if (text == "OK") { var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click(); }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }
        function reload() {
            $('#dg').datagrid('reload');
            $('#dg').datagrid('clearSelections');
        }
        //审核
        function check(Did) {
            var rows = $("#dg").datagrid('getData').rows[Did];
            if (rows) {
                if (rows.FinanceSecondCheck == "1") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', rows.OrderNo + '已审核', 'info'); return; }
                $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定审核？', function (r) {
                    if (r) {
                        var json = JSON.stringify([rows])
                        $.ajax({
                            url: '../Finance/financeApi.aspx?method=plSecondCheckOrder',
                            type: 'post', dataType: 'json', data: { data: json },
                            success: function (text) {
                                if (text.Result == true) {
                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '审核成功!', 'info');
                                    reload();
                                } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning'); }
                            }
                        });
                    }
                });
            }
            else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要审核的数据！', 'warning'); }
        }
        //批量审核
        function plcheck() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要审核的数据！', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定审核？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: '../Finance/financeApi.aspx?method=plSecondCheckOrder',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '审核成功!', 'info');
                                reload();
                            }
                            else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning'); }
                        }
                    });
                }
            });
        }
        //未审
        function Deny(Did) {
            var rows = $("#dg").datagrid('getData').rows[Did];
            if (rows) {
                $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定修改为未审？', function (r) {
                    if (r) {
                        var json = JSON.stringify([rows])
                        $.ajax({
                            url: '../Finance/financeApi.aspx?method=plUnSecondCheckOrder',
                            type: 'post',
                            dataType: 'json',
                            data: { data: json },
                            success: function (text) {
                                if (text.Result == true) {
                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '未审成功!', 'info');
                                    reload();
                                } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning'); }
                            }
                        });
                    }
                });
            }
            else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要审核的数据！', 'warning'); }
        }
        //批量未审
        function plDeny() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要未审的数据！', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定修改为未审？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: '../Finance/financeApi.aspx?method=plUnSecondCheckOrder',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '未审成功!', 'info');
                                reload();
                            }
                            else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning'); }
                        }
                    });
                }
            });
        }
        //保存订单
        function SaveOrderUpdate() {
            var row = $('#dgSave').datagrid('getRows');
            if (row.length <= 0) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单列表中没有数据', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定保存？', function (r) {
                if (r) {
                    var json = JSON.stringify(row);

                    $('#fmDep').form('submit', {
                        url: '../Order/orderApi.aspx?method=updateOrderData',
                        contentType: "application/json;charset=utf-8", dataType: "json",
                        onSubmit: function (param) {
                            param.submitData = json;
                            var trd = $(this).form('enableValidation').form('validate');
                            return trd;
                        },
                        success: function (msg) {
                            var result = eval('(' + msg + ')');
                            if (result.Result) {
                                var dd = result.Message.split('/');
                                $('#ONum').val(dd[0]);
                                $('#OutNum').val(dd[1]);
                                $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功，是否打印发货单？', function (r) {
                                    if (r) { prePrint(); }//打印
                                });
                            } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning'); }
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
            var row = $('#outDg').datagrid('getSelected');
            var Total = Number(row.Piece);
            var SalePrice = $('#ActSalePrice').numberbox('getValue');// Number(row.SalePrice);//销售价
            var tCharge = $('#TransportFee').numberbox('getValue') == null || $('#TransportFee').numberbox('getValue') == "" ? 0 : Number($('#TransportFee').numberbox('getValue'));
            var Aindex = $('#InIndex').val();
            var indexD = $('#dgSave').datagrid('getRowIndex', row.ID);
            if (indexD < 0) {
                row.Piece = $('#Numbers').numberbox('getValue');
                row.ActSalePrice = SalePrice;
                $('#dgSave').datagrid('appendRow', row);
                var p = $('#APiece').val() == "" || isNaN($('#APiece').val()) ? 0 : Number($('#APiece').val());
                var pt = p + Number($('#Numbers').numberbox('getValue'));
                $('#APiece').numberbox('setValue', Number(pt));
                var NC = SalePrice * Number($('#Numbers').numberbox('getValue'));
                $('#TransportFee').numberbox('setValue', tCharge + NC);
                closedgShowData();
                if (Total != Number($('#Numbers').numberbox('getValue'))) {
                    row.Piece = Total - Number($('#Numbers').numberbox('getValue'));
                } else {
                    row.Piece = 0;
                }
                $('#outDg').datagrid('updateRow', { index: Aindex, row: row });
            } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单列表已存在该产品，请先删除再添加！', 'warning'); }
        }
        //关闭
        function closedgShowData() {
            $('#dlgOutCargo').dialog('close');
        }
        ///拉上订单
        function plup() {
            var row = $('#outDg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要拉上订单的数据！', 'warning');
                return;
            }
            if (row.Piece == 0) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '在库件数为0', 'warning');
                return;
            }
            if (row) {
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
            $('#outDg').datagrid('clearSelections');
            var gridOpts = $('#outDg').datagrid('options');
            gridOpts.url = '../House/houseApi.aspx?method=QueryALLHouseData';
            $('#outDg').datagrid('load', {
                Specs: $('#ASpecs').val(),
                Model: $('#AModel').val()
            });
        }
        //双击显示订单详细界面
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlgOrder').dialog('open').dialog('setTitle', '修改订单：' + row.OrderNo);
                $('#fmDep').form('clear');
                //客户姓名
                $('#AAcceptPeople').combobox({
                    valueField: 'Boss',
                    textField: 'Boss',
                    delay: '10',
                    url: '../Client/clientApi.aspx?method=AutoCompleteClient',
                    onSelect: onAcceptAddressChanged,
                    required: true
                });
                bindMethod();
                //客户姓名
                $('#InsuranceFee').combogrid({
                    method: 'get', panelWidth: '150', idField: 'ID', textField: 'Money',
                    url: '../Order/orderApi.aspx?method=QueryMyCoupon&ClientNum=' + row.ClientNum,
                    columns: [[{ field: 'Money', title: '券金额', width: 50 }, { field: 'UseStatus', title: '使用状态', width: 50, formatter: function (val, row, index) { if (val == "0") { return "未使用"; } } }]],
                    fitColumns: true, onSelect: function (rowIndex, rowData) {
                        var t = Number($('#TransitFee').numberbox('getValue')) + Number($('#TransportFee').numberbox('getValue')) + Number($('#DeliveryFee').numberbox('getValue')) + Number($('#OtherFee').numberbox('getValue')) - Number(rowData.Money);
                        $('#TotalCharge').numberbox('setValue', Number(t).toFixed(2));
                    }
                });
                $('#fmDep').form('load', row);
                showGrid();
                if (row.OrderModel == "1") {
                    $('#down').linkbutton('disable');
                    $('#up').linkbutton('disable');
                    $('#save').linkbutton('disable');
                    $('#payprint').linkbutton('disable');
                    $('#pickprint').linkbutton('disable');
                } else {
                    $('#down').linkbutton('enable');
                    $('#up').linkbutton('enable');
                    $('#save').linkbutton('enable');
                    $('#payprint').linkbutton('enable');
                    $('#pickprint').linkbutton('enable');
                }
                var gridOpts = $('#dgSave').datagrid('options');
                gridOpts.url = '../Order/orderApi.aspx?method=QueryOrderByOrderNo&OrderNo=' + row.OrderNo;
            }
        }
        //显示列表
        function showGrid() {
            $('#outDg').datagrid({
                width: '394px',
                height: '310px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    { title: '', field: 'ID', checkbox: true, width: '30px' },
                    { title: '在库件数', field: 'Piece', width: '60px' },
                    { title: '型号', field: 'Model', width: '60px' },
                    { title: '规格', field: 'Specs', width: '80px' },
                    { title: '花纹', field: 'Figure', width: '100px' },
                    { title: '产品名称', field: 'ProductName', width: '100px' },
                    { title: '产品类型', field: 'TypeName', width: '60px' },
                    { title: '所在仓库', field: 'HouseName', width: '80px' },
                    { title: '所在区域', field: 'AreaName', width: '60px' },
                    { title: '货位代码', field: 'ContainerCode', width: '80px' },
                    //{ title: '入库时间', field: 'InHouseTime', width: '130px', formatter: DateTimeFormatter },
                    //{ title: '胎面宽度', field: 'TreadWidth', width: '60px' },
                    //{ title: '扁平比', field: 'FlatRatio', width: '60px' },
                    //{ title: '子午线', field: 'Meridian', width: '60px' },
                    //{ title: '轮毂直径', field: 'HubDiameter', width: '60px' },
                    { title: '批次', field: 'Batch', width: '60px' },
                    { title: '载重指数', field: 'LoadIndex', width: '60px' },
                    { title: '速度级别', field: 'SpeedLevel', width: '60px' },
                    { title: '最高速度', field: 'SpeedMax', width: '60px' },
                    { title: '尺寸', field: 'Size', width: '80px' },
                    { title: '单价', field: 'UnitPrice', width: '60px' },
                    { title: '数量', field: 'Numbers', width: '60px' },
                    { title: '销售价', field: 'SalePrice', width: '60px' },
                    { title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter }
                ]],
                groupField: 'TypeParentName',
                view: groupview,
                groupFormatter: function (value, rows) {
                    return value;
                }
            });
            $('#dgSave').datagrid({
                width: '600px',
                height: '310px',
                title: '出库产品', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '',
                columns: [[
                    { title: '', field: 'ID', checkbox: true, width: '30px' },
                    { title: '出库件数', field: 'Piece', width: '60px' },
                    { title: '业务员价', field: 'ActSalePrice', width: '60px' },
                    { title: '型号', field: 'Model', width: '60px' },
                    { title: '规格', field: 'Specs', width: '80px' },
                    { title: '花纹', field: 'Figure', width: '100px' },
                    { title: '批次', field: 'Batch', width: '50px' },
                    { title: '产品名称', field: 'ProductName', width: '100px' },
                    { title: '产品类型', field: 'TypeName', width: '60px' },
                    { title: '所在仓库', field: 'HouseName', width: '80px' },
                    { title: '所在区域', field: 'AreaName', width: '60px' },
                    { title: '货位代码', field: 'ContainerCode', width: '80px' },
                    //{ title: '入库时间', field: 'InHouseTime', width: '130px', formatter: DateTimeFormatter },
                    { title: '销售价', field: 'SalePrice', hidden: true },
                    //{ title: '胎面宽度', field: 'TreadWidth', width: '60px' },
                    //{ title: '扁平比', field: 'FlatRatio', width: '60px' },
                    //{ title: '子午线', field: 'Meridian', width: '60px' },
                    //{ title: '轮毂直径', field: 'HubDiameter', width: '60px' },
                    { title: '载重指数', field: 'LoadIndex', width: '60px' },
                    { title: '速度级别', field: 'SpeedLevel', width: '60px' },
                    { title: '最高速度', field: 'SpeedMax', width: '60px' },
                    { title: '尺寸', field: 'Size', width: '80px' },
                    { title: '单价', field: 'UnitPrice', width: '60px' },
                    //{ title: '数量', field: 'Numbers', width: '60px' },
                    { title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter }
                ]],
                groupField: 'TypeParentName',
                view: groupview,
                groupFormatter: function (value, rows) {
                    return value;
                }
            });
        }
        //绑定费用框
        function bindMethod() {
            $("#TransitFee").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
            $("#TransportFee").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
            $("#DeliveryFee").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
            $("#OtherFee").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
            //$("#InsuranceFee").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
        }
        function qh() {
            var t = Number($('#TransitFee').numberbox('getValue')) + Number($('#TransportFee').numberbox('getValue')) + Number($('#DeliveryFee').numberbox('getValue')) + Number($('#OtherFee').numberbox('getValue')) - Number($('#InsuranceFee').combogrid('getText'));;
            $('#TotalCharge').numberbox('setValue', Number(t).toFixed(2));
        }
        //业务员选择方法
        function onSaleManIDChanged(item) {
            if (item) {
                $('#SaleManName').val(item.UserName);
                $('#SaleCellPhone').val(item.CellPhone);
            }
        }
        //收货人自动选择方法
        function onAcceptAddressChanged(item) {
            if (item) {
                $("#HiddenClientSelectName").val(item.Boss);
                $('#ClientNum').val(item.ClientNum);
                $('#AAcceptUnit').textbox('setValue', item.ClientName);
                $('#AAcceptAddress').textbox('setValue', item.Address);
                $('#AAcceptTelephone').textbox('setValue', item.Telephone);
                $('#AAcceptCellphone').textbox('setValue', item.Cellphone);
            }
        }
        //重置
        function reset() {
            // prePrint();
            $('#fmDep').form('clear');
            $('#dgSave').datagrid('loadData', { total: 0, rows: [] });
            $('#outDg').datagrid('loadData', { total: 0, rows: [] });

            $('#DisplayNum').val(0);
            $('#DisplayPiece').val(0);
            var title = "";
            $('#outDg').datagrid("getPanel").panel("setTitle", title);
            $('#dgSave').datagrid("getPanel").panel("setTitle", title);
        }
        //关闭弹出框
        function closeDlg() {
            $('#dlgOrder').dialog('close');
            $('#dg').datagrid('reload');
        }

        var LODOP;
        //订单打印
        function prePrint() {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            var griddata = $('#dgSave').datagrid('getRows');
            var js = 0, Alltotal = 0, AllPiece = 0; p = 1; pie = 0; total = 0;
            for (var k = 0; k < griddata.length; k++) {
                pie = Number(griddata[k].Piece);
                total = Number(pie) * Number(griddata[k].ActSalePrice);
                Alltotal += total;
                AllPiece += Number(pie);
            }
            for (var i = 0; i < griddata.length; i++) {
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
                    LODOP.ADD_PRINT_LINE(133, 615, 31, 616, 0, 1);
                    LODOP.ADD_PRINT_LINE(87, 3, 86, 791, 0, 1);
                    LODOP.ADD_PRINT_LINE(110, 3, 109, 791, 0, 1);
                    LODOP.ADD_PRINT_LINE(133, 121, 86, 122, 0, 1);
                    LODOP.ADD_PRINT_LINE(134, 3, 133, 791, 0, 1);
                    LODOP.ADD_PRINT_LINE(134, 247, 57, 248, 0, 1);
                    LODOP.ADD_PRINT_LINE(134, 397, 57, 398, 0, 1);
                    LODOP.ADD_PRINT_LINE(133, 523, 31, 524, 0, 1);

                    LODOP.ADD_PRINT_LINE(396, 3, 395, 791, 0, 1);
                    LODOP.ADD_PRINT_LINE(479, 58, 395, 59, 0, 1);
                    LODOP.ADD_PRINT_LINE(453, 285, 434, 286, 0, 1);
                    LODOP.ADD_PRINT_LINE(415, 3, 414, 791, 0, 1);
                    LODOP.ADD_PRINT_LINE(479, 553, 395, 554, 0, 1);
                    LODOP.ADD_PRINT_LINE(453, 648, 395, 649, 0, 1);
                    LODOP.ADD_PRINT_LINE(435, 3, 434, 791, 0, 1);
                    LODOP.ADD_PRINT_LINE(455, 3, 454, 791, 0, 1);
                    LODOP.ADD_PRINT_LINE(480, 3, 479, 791, 0, 1);
                    LODOP.ADD_PRINT_LINE(454, 384, 435, 385, 0, 1);

                    var hous = '<%= HouseName%>';
                    var com = '湖南省迪乐泰贸易有限公司发货单';
                    if (hous.indexOf('武汉') != -1) {
                        com = '湖北省迪乐泰贸易有限公司发货单';
                    }
                    var hous = '<%= HouseName%>';
                    var sendTitle = '<%=UserInfor.SendTitle%>'
                    LODOP.ADD_PRINT_TEXT(1, 226, 471, 33, sendTitle);
                    LODOP.SET_PRINT_STYLEA(0, "FontName", "新宋体");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 19);
                    LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                    LODOP.ADD_PRINT_IMAGE(-1, 7, 198, 32, "<img src='../CSS/image/dlqf.jpg'/>");
                    LODOP.SET_PRINT_STYLEA(0, "Stretch", 1);
                    LODOP.ADD_PRINT_TEXT(37, 5, 87, 26, "收货地址：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(37, 91, 440, 27, $('#AAcceptAddress').textbox('getValue'));
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(37, 533, 85, 26, "发货日期：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(37, 633, 125, 27, getNowFormatDate($('#CreateDate').datetimebox('getValue')));
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(64, 5, 87, 26, "物流公司：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(64, 91, 136, 27, $('#ALogisID').combobox('getText'));
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(64, 278, 100, 26, "物流费用：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(64, 411, 96, 27, $('#TransitFee').numberbox('getText'));
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                    LODOP.ADD_PRINT_TEXT(64, 533, 85, 26, "发货单号：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(64, 633, 104, 27, $('#OrderNo').val());
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(90, 15, 79, 25, "收货单位");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(90, 143, 56, 25, "收货人");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(90, 279, 75, 25, "联系电话");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(90, 422, 74, 25, "发货单位");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(90, 545, 58, 25, "发货人");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(90, 651, 80, 25, "联系电话");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(112, 10, 100, 25, $('#AAcceptUnit').combobox('getText'));
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(112, 127, 100, 25, $('#AAcceptPeople').textbox('getValue'));//收货人
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    var sphone = $('#AAcceptCellphone').textbox('getValue');
                    if (sphone == "") {
                        sphone = $('#AAcceptTelephone').textbox('getValue');
                    }
                    LODOP.ADD_PRINT_TEXT(112, 263, 130, 25, sphone);//填收货人联系电话
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    var sendUnit = '湖南迪乐泰';
                    var sendPhone = '13319570206';
                    if (hous.indexOf('湖北') != -1) {
                        sendUnit = '湖北迪乐泰';
                        sendPhone = '17771479223;83092268';
                    }
                    if (hous.indexOf('西安') != -1) {
                        sendUnit = '西安新陆程';
                        sendPhone = '029-84524648';
                    }
                    if (hous.indexOf('梅州') != -1) {
                        sendUnit = '梅州新陆程';
                        sendPhone = '18088857730';
                    }
                    if (hous.indexOf('广州') != -1) {
                        sendUnit = '广州迪乐泰';
                        sendPhone = '13687469699';
                    }
                    if (hous.indexOf('海南') != -1) {
                        sendUnit = '海南迪乐泰';
                        sendPhone = '15120882670';
                    }
                    if (hous.indexOf('揭阳') != -1) {
                        sendUnit = '揭阳迪乐泰';
                        sendPhone = '13377790810';
                    }
                    if (hous.indexOf('广东') != -1) {
                        sendUnit = '广州迪乐泰';
                        sendPhone = '13687469699';
                    }
                    if (hous.indexOf('四川') != -1) {
                        sendUnit = '四川迪乐泰';
                        sendPhone = '18122771967';
                    }
                    LODOP.ADD_PRINT_TEXT(112, 419, 94, 25, sendUnit);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(112, 532, 100, 25, $('#SaleManName').val());//填业务员
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(112, 626, 127, 25, sendPhone);//业务员的联系电话$('#SaleCellPhone').val()
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);


                    LODOP.ADD_PRINT_TEXT(135, 40, 54, 24, "品牌");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(135, 141, 71, 24, "代码");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(135, 263, 63, 24, "规格");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(135, 395, 45, 24, "花纹");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(135, 469, 80, 24, "速度级别");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(135, 545, 46, 24, "周期");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(135, 601, 48, 24, "单价");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(135, 664, 47, 24, "数量");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(135, 724, 53, 24, "总价");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                    LODOP.ADD_PRINT_LINE(153, 3, 152, 791, 0, 1);


                    LODOP.ADD_PRINT_TEXT(397, 16, 54, 25, "总计");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(397, 565, 69, 19, AllPiece);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(397, 667, 105, 19, Alltotal);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(416, 13, 61, 25, "总 计");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
                    LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                    LODOP.ADD_PRINT_TEXT(416, 241, 350, 25, atoc(Alltotal));
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
                    LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                    LODOP.ADD_PRINT_TEXT(436, 16, 66, 22, "制表：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(436, 84, 100, 19, $('#CreateAwb').textbox('getValue'));
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(436, 313, 61, 22, "仓库：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(436, 421, 100, 19, "");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(436, 570, 67, 22, "司机：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(436, 660, 100, 22, "");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(460, 11, 58, 28, "备 注");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
                    LODOP.ADD_PRINT_TEXT(456, 72, 357, 29, $('#ARemark').val());
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    var daili = '迪乐泰湖南轮胎总代理';
                    if (hous.indexOf('湖北') != -1) {
                        daili = '迪乐泰湖北轮胎总代理';
                        LODOP.ADD_PRINT_TEXT(436, 656, 100, 19, "17771448223");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    }
                    if (hous.indexOf('西安') != -1) {
                        daili = '优科豪马轮胎西安新陆程总代理';
                    }
                    if (hous.indexOf('梅州') != -1) {
                        daili = '梅州新陆程';
                    }
                    if (hous.indexOf('揭阳') != -1) {
                        daili = '揭阳迪乐泰';
                    }
                    if (hous.indexOf('广东') != -1) {
                        daili = '广州迪乐泰';
                    }
                    if (hous.indexOf('四川') != -1) {
                        daili = '四川迪乐泰';
                    }
                    if (hous.indexOf('广州') != -1) {
                        daili = '广州迪乐泰轮胎总代理';
                        var addr = $('#AAcceptAddress').textbox('getValue');
                        var sj = "";
                        if (addr.indexOf('大岭山') > -1 || addr.indexOf('长安') > -1 || addr.indexOf('虎门') > -1 || addr.indexOf('厚街') > -1 || addr.indexOf('南城') > -1 || addr.indexOf('东城') > -1 || addr.indexOf('沙田') > -1 || addr.indexOf('麻涌') > -1) {
                            sj = "胡 17776469164";
                        } else if (addr.indexOf('寮步') > -1 || addr.indexOf('企石') > -1 || addr.indexOf('桥头') > -1 || addr.indexOf('茶山') > -1 || addr.indexOf('石牌') > -1 || addr.indexOf('莞城') > -1 || addr.indexOf('高埗') > -1 || addr.indexOf('万江') > -1 || addr.indexOf('中堂') > -1) {
                            sj = "蒋 18978375719";
                        } else if (addr.indexOf('塘厦') > -1 || addr.indexOf('凤岗') > -1 || addr.indexOf('清溪') > -1 || addr.indexOf('樟木头') > -1 || addr.indexOf('黄江') > -1 || addr.indexOf('大朗') > -1 || addr.indexOf('常平') > -1 || addr.indexOf('横沥') > -1 || addr.indexOf('东坑') > -1) {
                            sj = "陈 13711412142";
                        }
                        LODOP.ADD_PRINT_TEXT(436, 656, 300, 19, sj);
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    }
                    if (hous.indexOf('海南') != -1) {
                        daili = '海南迪乐泰轮胎总代理';
                    }
                    LODOP.ADD_PRINT_TEXT(460, 566, 189, 30, daili);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
                    var beizu = '注：第一联结算联（白）、第二联收货人留存（红）、第三联对账联（黄）';
                    if (hous.indexOf('湖北') != -1) {
                        beizu = '湖北优科豪马轮胎代理迪乐泰公司感谢于您的合作，谢谢！';
                    }
                    LODOP.ADD_PRINT_TEXT(484, 11, 645, 26, beizu);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                }

                LODOP.ADD_PRINT_LINE(176 + (i - (p - 2) * 10) * 23, 3, 175 + (i - (p - 2) * 10) * 23, 791, 0, 1);
                LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 25, 134 + (i - (p - 2) * 10) * 23, 26, 0, 1);
                LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 119, 134 + (i - (p - 2) * 10) * 23, 120, 0, 1);
                LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 237, 134 + (i - (p - 2) * 10) * 23, 238, 0, 1);
                LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 348, 134 + (i - (p - 2) * 10) * 23, 349, 0, 1);
                LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 468, 134 + (i - (p - 2) * 10) * 23, 469, 0, 1);
                LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 540, 134 + (i - (p - 2) * 10) * 23, 541, 0, 1);
                LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 592, 134 + (i - (p - 2) * 10) * 23, 593, 0, 1);
                LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 657, 134 + (i - (p - 2) * 10) * 23, 658, 0, 1);
                LODOP.ADD_PRINT_LINE(174 + (i - (p - 2) * 10) * 23, 701, 134 + (i - (p - 2) * 10) * 23, 702, 0, 1);

                js++;
                pie = Number(griddata[i].Piece);
                total = Number(pie) * Number(griddata[i].ActSalePrice);

                LODOP.ADD_PRINT_TEXT(156 + (i - (p - 2) * 10) * 23, 6, 25, 23, js);
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 33, 85, 23, griddata[i].TypeName);//品牌
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 122, 115, 23, griddata[i].Model);//型号
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 246, 100, 23, griddata[i].Specs);//规格 
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 354, 130, 23, griddata[i].Figure);//花纹
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 480, 60, 23, griddata[i].LoadIndex + griddata[i].SpeedLevel);//速度级别
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 545, 49, 23, griddata[i].Batch);//周期
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 598, 64, 23, griddata[i].ActSalePrice);//单价销售价
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 666, 48, 23, pie);//数量出库件数
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 10) * 23, 708, 150, 23, total);//总价
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            }
            LODOP.PREVIEW();
            //LODOP.PRINT_DESIGN();
        }
        //订单打印
        function pickUpOrder() {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            var nowdate = new Date();
            LODOP.SET_PRINT_PAGESIZE(0, 2100, 2970, "A4");
            var com = '<%=UserInfor.PickTitle%>'
            LODOP.ADD_PRINT_TEXT(4, 253, 485, 33, com);
            LODOP.SET_PRINT_STYLEA(0, "FontName", "新宋体");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 19);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_IMAGE(-3, 47, 198, 49, "<img src=\"../CSS/image/dlqf.jpg\"/>");
            LODOP.SET_PRINT_STYLEA(0, "Stretch", 1);

            LODOP.ADD_PRINT_TEXT(41, 120, 70, 20, "订单号：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(41, 180, 110, 20, $('#OrderNo').val());
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

            LODOP.ADD_PRINT_TEXT(41, 450, 75, 20, "收货人：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(41, 510, 105, 20, $('#AAcceptPeople').combobox('getText'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(41, 565, 90, 20, "联系电话：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            var tell = $('#AAcceptCellphone').textbox('getValue');
            if (tell == '' || tell == null) { tell = $('#AAcceptTelephone').textbox('getValue'); }
            LODOP.ADD_PRINT_TEXT(41, 638, 117, 20, tell);
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

            LODOP.ADD_PRINT_RECT(66, 3, 99, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 102, 79, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 181, 99, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 280, 99, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 379, 64, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 443, 69, 25, 0, 1);
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
            LODOP.ADD_PRINT_TEXT(70, 383, 61, 25, "速度级别");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(70, 448, 64, 25, "出库件数");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(70, 522, 60, 25, "批次");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(70, 590, 88, 25, "货位代码");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(70, 684, 72, 25, "所在区域");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

            var griddata = $('#dgSave').datagrid('getRows');
            LODOP.ADD_PRINT_TEXT(41, 290, 90, 20, "所在仓库：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(41, 370, 80, 20, griddata[0].HouseName);
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            var js = 0, Alltotal = 0, AllPiece = 0;
            for (var i = 0; i < griddata.length; i++) {
                js++;
                var p = Number(griddata[i].Piece);

                LODOP.ADD_PRINT_RECT(90 + i * 25, 3, 99, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 102, 79, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 181, 99, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 280, 99, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 379, 64, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 443, 69, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 512, 74, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 586, 96, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 682, 106, 25, 0, 1);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 5, 111, 23, griddata[i].ProductName);//产品名称
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 105, 80, 20, griddata[i].Model);//型号
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 185, 94, 20, griddata[i].Specs);//规格 
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 286, 82, 20, griddata[i].Figure);//花纹
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 386, 82, 20, griddata[i].LoadIndex + griddata[i].SpeedLevel);//速度级别
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 450, 51, 20, p);//数量出库件数
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

            LODOP.ADD_PRINT_TEXT(147 + (griddata.length - 1) * 25, 50, 102, 23, "仓库：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(147 + (griddata.length - 1) * 25, 337, 100, 20, "制表：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(147 + (griddata.length - 1) * 25, 574, 200, 20, AllDateTime(nowdate));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);


            LODOP.PREVIEW();
        }
    </script>

</asp:Content>
