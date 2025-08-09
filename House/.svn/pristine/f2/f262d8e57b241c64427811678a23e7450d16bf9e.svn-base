<%@ Page Title="新增订单" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="addOrder.aspx.cs" Inherits="Cargo.Order.addOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="../JS/Date/CheckActivX.js" type="text/javascript"></script>--%>
    <script src="../JS/Lodop/LodopFuncs.js" type="text/javascript"></script>
    <style type="text/css">
        #box {
            height: 25px;
            width: 100px;
            position: relative;
            cursor: pointer;
            overflow: hidden;
            padding: 2px;
            background-color: #fdfce2;
            border-radius: 5px;
        }

            #box.border {
                border: 1px solid red;
            }

            #box .gou {
                position: absolute;
                width: 24px;
                height: 24px;
                right: 0px;
            }

                #box .gou.on::after {
                    border-color: red;
                }

                #box .gou::after {
                    position: absolute;
                    top: 4px;
                    left: 8px;
                    width: 6px;
                    height: 10px;
                    border-style: solid;
                    border-color: #ccc;
                    border-width: 0 2px 2px 0;
                    transform: rotateZ(45deg);
                    content: "";
                }
    </style>
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
            }
        })
        $.parser.onComplete = function () {
            if (pc) {
                clearTimeout(pc);
            }
            pc = setTimeout(closemask, 10);
            if (IsQueryLockStock == 0) {
                $("#IsQueryLockStock").css('display', 'none')
            }
        }
        //关闭加载中遮罩层
        function closemask() {
            $("#Loading").fadeOut("normal", function () {
                $(this).remove();
            });
        }
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
                        var Remark = $('#ARemark').val();
                        if (Remark != null && Remark != undefined) {
                            Remark = Remark.replace("周期" + oldValue + "天，", "");
                            $('#ARemark').val('周期' + newValue + '天，' + Remark);
                        } else {
                            $('#ARemark').val('周期' + newValue + '天');
                        }
                        $('#HiddenPeriodsNum').val(newValue);
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
            HID = "<%=UserInfor.HouseID%>";

            //如果是广州、广东、揭阳的用户开放区域大仓选项
            //if (HID == "9" || HID == "44" || HID == "45") {
            //    $('#AHouseID').combobox('enable');
            //} else {
            //    $('#AHouseID').combobox('disable');
            //}
            $('#AHouseID').combobox('enable');

            if (HID == "50" || HID == "51" || HID == "52" || HID == "53" || HID == "54") {
                $('#ALogisID').combobox('setValue', 34);
            }

            if (HID == "1") {
                $('#DeliverySettlement').combobox('setValue', 1);
            }

            <%--//头脑不热又不限制产品了
            LoginName="<%= UserInfor.LoginName%>";
            if (LoginName == "1409" || LoginName == "2253") {
                $('#ABelongDepart').combobox('disable');
                $('#ABelongDepart').combobox('setValue', '1');
            } else {
                $('#ABelongDepart').combobox('enable');
            }--%>

            $.ajaxSetup({ async: true });
            $.getJSON("../Product/productApi.aspx?method=QueryAllProductSource", function (data) {
                ProductSource = data;
            })
            //定义全局ThrowGood值用于判断显示价格
            ThrowGoodValue = -1;


            $("#SaleManID").combobox({
                //相当于html >> select >> onChange事件  
                onChange: function (newVal, oldVal) {
                    var data = $('#SaleManID').combobox('getData');
                    for (var i = 0; i < data.length; i++)
                        if (data[i].LoginName == newVal) {
                            $('#SaleManName').val(data[i].UserName);
                            break;
                        } else {
                            $('#SaleManName').val('');
                        }
                }
            });
            $("#ALogisID").combobox({
                //相当于html >> select >> onChange事件  
                onChange: function (newVal, oldVal) {
                    if (newVal == 34 && $('#AHouseID').combobox('getValue') == 9) {
                        $('#PostponeShip').show();
                        $('#PostponeShipLab').show();

                    } else {
                        $("#PostponeShip").attr("checked", false);
                        $('#PostponeShip').hide();
                        $('#PostponeShipLab').hide();
                    }
                }
            });
        }
        $(window).resize(function () {
            adjustment();
        });
        function adjustment() {
            //var height = parseInt((Number($(window).height()) - 100) / 2);
            var height = parseInt((Number($(window).height()) - Number($("div[name='SelectDiv1']").outerHeight(true))) / 2);
            $('#dg').datagrid({ height: height });
            $('#outDg').datagrid({ height: (Number($(window).height()) - 90) - height });
        }
        var RoleCName = "<%=UserInfor.RoleCName%>";
        $(document).ready(function () {
            var columns = [];
            var HID = "<%=UserInfor.HouseID%>";
            columns.push({ title: '', field: 'ID', checkbox: true, width: '10%' });
            columns.push({
                title: '品牌', field: 'TypeName', width: '6%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            if (RoleCName.indexOf("汕头科矿") >= 0) {
                columns.push({
                    title: '货品代码', field: 'GoodsCode', width: '6%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            }
            if (HID == 63) {
                columns.push({
                    title: '产品名称', field: 'GoodsName', width: '12%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            }
            else {
                columns.push({
                    title: '规格', field: 'Specs', width: '7%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '型号', field: 'Model', width: '7%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '花纹', field: 'Figure', width: '8%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            }
            columns.push({
                title: '在库数量', field: 'Piece', width: '5%', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '批次', field: 'Batch', width: '5%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            if (RoleCName.indexOf("汕头科矿") >= 0) {
                columns.push({
                    title: '尺寸', field: 'HubDiameter', width: '3%', align: 'right', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '捆包数', field: 'PackageNum', width: '3%', align: 'right', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            }
            else {
                columns.push({
                    title: '载重', field: 'LoadIndex', width: '3%', align: 'right', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '速度', field: 'SpeedLevel', width: '3%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            }
            columns.push({
                title: '销售价', field: 'SalePrice', width: '4%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '门店价', field: 'TradePrice', width: '4%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            if (HID == 64) {
                columns.push({
                    title: '供应商', field: 'Supplier', width: '8%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            } else {
                if (RoleCName.indexOf("汕头科矿") < 0) {
                    columns.push({
                        title: '产品来源', field: 'Source', width: '8%', formatter: function (value) {
                            return "<span title='" + GetSourceName(value) + "'>" + GetSourceName(value) + "</span>";
                        }
                    });
                }
            }
            columns.push({
                title: '货位代码', field: 'ContainerCode', width: '6%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在区域', field: 'AreaName', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在仓库', field: 'FirstAreaName', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            if (RoleCName.indexOf("汕头科矿") < 0) {
                columns.push({
                    title: '归属部门', field: 'BelongDepart', width: '5%', formatter: function (value) {
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
                if (HID == 45) {
                    columns.push({
                        title: '离客距离', field: 'Distance', width: '5%', formatter: function (value) {
                            if (value != "" && value != null) {
                                return "<span title='" + value / 1000 + "千米'>" + value / 1000 + "千米</span>";
                            } else {
                                return "<span title=''></span>";
                            }
                        }
                    });
                    columns.push({
                        title: '当前耗时', field: 'Duration', width: '5%', formatter: function (value) {
                            if (value != "" && value != null) {
                                return "<span title='" + (value / 60).toFixed(2) + "分钟'>" + (value / 60).toFixed(2) + "分钟</span>";
                            } else {
                                return "<span title=''></span>";
                            }
                        }
                    });
                }
                columns.push({
                    title: '产品名称', field: 'ProductName', width: '10%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            }
            //columns.push({
            //    title: '单价', field: 'UnitPrice', width: '5%', align: 'right', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});
            columns.push({ title: '入库时间', field: 'InHouseTime', width: '130px', formatter: DateTimeFormatter });

            columns.push({ title: '业务员销售价', field: 'ActSalePrice', hidden: true });
            columns.push({ title: '是否优惠', field: 'IsRuleBank', hidden: true });
            columns.push({ title: '优惠类型', field: 'RuleType', hidden: true });
            columns.push({ title: '优惠规则ID', field: 'RuleID', hidden: true });
            columns.push({ title: '优惠规则名称', field: 'RuleTitle', hidden: true });
            $('#dg').datagrid({
                width: '100%',
                //height: '50%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                pageSize: 20, //每页多少条
                pageList: [20, 50],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [columns],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { plOutCargo(); }
            });

            columns = [];
            columns.push({
                title: '', field: 'ID', checkbox: true, width: '3%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '品牌', field: 'TypeName', width: '6%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            if (RoleCName.indexOf("汕头科矿") >= 0) {
                columns.push({
                    title: '货品代码', field: 'GoodsCode', width: '6%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            }
            if (HID == 63) {
                columns.push({
                    title: '产品名称', field: 'GoodsName', width: '12%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            }
            else {
                columns.push({
                    title: '规格', field: 'Specs', width: '7%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '型号', field: 'Model', width: '7%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '花纹', field: 'Figure', width: '8%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            }
            columns.push({
                title: '出库数量', field: 'Piece', width: '5%', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '批次', field: 'Batch', width: '5%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            if (RoleCName.indexOf("汕头科矿") >= 0) {

                columns.push({
                    title: '尺寸', field: 'HubDiameter', width: '3%', align: 'right', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '捆包数', field: 'PackageNum', width: '3%', align: 'right', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            }
            else {
                columns.push({
                    title: '载重', field: 'LoadIndex', width: '3%', align: 'right', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '速度', field: 'SpeedLevel', width: '3%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            }
            columns.push({
                title: '销售价', field: 'ActSalePrice', width: '5%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            if (HID == 64) {
                columns.push({
                    title: '供应商', field: 'Supplier', width: '8%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            } else {
                if (RoleCName.indexOf("汕头科矿") < 0) {
                    columns.push({
                        title: '产品来源', field: 'Source', width: '8%', formatter: function (value) {
                            return "<span title='" + GetSourceName(value) + "'>" + GetSourceName(value) + "</span>";
                        }
                    });
                }
            }
            columns.push({
                title: '货位代码', field: 'ContainerCode', width: '6%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在区域', field: 'AreaName', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在仓库', field: 'FirstAreaName', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            if (RoleCName.indexOf("汕头科矿") < 0) {
                columns.push({
                    title: '归属部门', field: 'BelongDepart', width: '5%', formatter: function (value) {
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
                columns.push({
                    title: '产品名称', field: 'ProductName', width: '10%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            }
            //columns.push({
            //    title: '单价', field: 'UnitPrice', width: '5%', align: 'right', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});

            //出库列表
            $('#outDg').datagrid({
                width: '100%',
                //height: '38%',
                title: '出库产品', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '',
                columns: [columns],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { dblClickDelCargo(index); }
            });

            columns = [];
            columns.push({
                title: '', field: 'ID', checkbox: true, width: '3%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '品牌', field: 'TypeName', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            if (RoleCName.indexOf("汕头科矿") >= 0) {
                columns.push({
                    title: '货品代码', field: 'GoodsCode', width: '6%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            }
            if (HID == 63) {
                columns.push({
                    title: '产品名称', field: 'GoodsName', width: '12%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            }
            else {
                columns.push({
                    title: '规格', field: 'Specs', width: '6%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '型号', field: 'Model', width: '6%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '花纹', field: 'Figure', width: '7%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            }
            columns.push({
                title: '出库数量', field: 'Piece', width: '4%', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '批次', field: 'Batch', width: '3%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            if (RoleCName.indexOf("汕头科矿") >= 0) {

                columns.push({
                    title: '尺寸', field: 'HubDiameter', width: '3%', align: 'right', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '捆包数', field: 'PackageNum', width: '3%', align: 'right', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            }
            else {
                columns.push({
                    title: '载重', field: 'LoadIndex', width: '3%', align: 'right', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '速度', field: 'SpeedLevel', width: '3%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            }
            columns.push({
                title: '销售价', field: 'ActSalePrice', width: '5%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            if (HID == 64) {
                columns.push({
                    title: '供应商', field: 'Supplier', width: '8%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            } else {
                if (RoleCName.indexOf("汕头科矿") < 0) {
                    columns.push({
                        title: '产品来源', field: 'Source', width: '8%', formatter: function (value) {
                            return "<span title='" + GetSourceName(value) + "'>" + GetSourceName(value) + "</span>";
                        }
                    });
                }
            }
            columns.push({
                title: '货位代码', field: 'ContainerCode', width: '6%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在区域', field: 'AreaName', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在仓库', field: 'FirstAreaName', width: '8%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            if (RoleCName.indexOf("汕头科矿") < 0) {
                columns.push({
                    title: '归属部门', field: 'BelongDepart', width: '5%', formatter: function (value) {
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
                columns.push({
                    title: '产品名称', field: 'ProductName', width: '10%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            }
            //columns.push({
            //    title: '单价', field: 'UnitPrice', width: '5%', align: 'right', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});

            //订单列表
            $('#dgSave').datagrid({
                width: '100%',
                title: '出库产品', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '',
                columns: [columns],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { }
            });
            //所在仓库
            $('#HID').combobox({ url: '../House/houseApi.aspx?method=CargoPermisionHouse', valueField: 'HouseID', textField: 'Name' });

            //一级产品
            $('#APID').combobox({
                url: '../Product/productApi.aspx?method=QueryALLOneProductType', valueField: 'TypeID', textField: 'TypeName',
                onSelect: function (rec) {
                    $('#ASID').combobox('clear');
                    $('#ASID').combobox({
                        url: '../Product/productApi.aspx?method=QueryALLOneProductType&PID=' + rec.TypeID, valueField: 'TypeID', textField: 'TypeName', AddField: 'PinyinName',
                        filter: function (q, row) {
                            var opts = $(this).combobox('options');
                            return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                        },
                    });
                }
            });
            //一级产品
            $('#ParentID').combobox({
                url: '../Product/productApi.aspx?method=QueryALLOneProductType', valueField: 'TypeID', textField: 'TypeName',
                onSelect: function (rec) {
                    $('#TypeID').combobox('clear');
                    var url = '../Product/productApi.aspx?method=QueryALLOneProductType&PID=' + rec.TypeID;
                    $('#TypeID').combobox('reload', url);
                }
            });
            $('#DisplayNum').val(0);
            $('#DisplayPiece').val(0);
            var hous = '<%= UserInfor.HouseName%>';
            $('#ADep').textbox('setValue', '<%= UserInfor.DepCity%>');
            $('#ADest').combobox('textbox').bind('focus', function () { $('#ADest').combobox('showPanel'); });
            $('#CreateAwb').textbox('setValue', '<%= Un%>');
            var d = new Date();
            $('#CreateDate').datetimebox('setValue', AllDateTime(d));


            //var GetAcceptPeople;
            //$("#SAcceptPeople").combobox({
            //    onChange: function (newValue, oldValue) {
            //        if (newValue != null & newValue != '' && !isNaN(parseInt(newValue))) { return; }
            //        if (newValue != undefined) {
            //            if (GetAcceptPeople != undefined) {
            //                clearInterval(GetAcceptPeople);
            //                GetAcceptPeople = 0;
            //            }
            //            var boss = $('#SAcceptPeople').combobox('getText');
            //            if (boss != null && boss != "") {
            //                if (boss != $('#HiddenClientSelectName').val()) {
            //                    GetAcceptPeople = setTimeout(function () {
            //                        if (GetAcceptPeople != 0) {
            //                            var url = '../Client/clientApi.aspx?method=AutoCompleteClient&Boss=' + boss;
            //                            $('#SAcceptPeople').combobox('reload', url);
            //                            $('#SAcceptPeople').combobox('setValue', boss);
            //                            clearInterval(GetAcceptPeople);
            //                            GetAcceptPeople = 0;
            //                        }
            //                    }, 700);
            //                }
            //            }
            //        }
            //    },
            //    onSelect: onClientChanged
            //});
            bindMethod();
            $('#ASpecs').textbox('textbox').keydown(function (e) {
                if (e.keyCode == 13) {
                    dosearch();
                }
            });
            $(function () {
                $("#ASpecs").textbox('textbox').bind('keyup', function () {
                    var specs = $("#ASpecs").next().children().val();
                    if (specs != null && specs != "") {
                        $("#HAID").combobox('options').required = false;
                        $("#HAID").combobox('textbox').validatebox('options').required = false;
                        $("#HAID").combobox('validate');
                    } else {
                        $("#HAID").combobox('options').required = true;
                        $("#HAID").combobox('textbox').validatebox('options').required = true;
                        $("#HAID").combobox('validate');
                    }
                });
            })
            $(function () {
                $("#AFigure").textbox('textbox').bind('keyup', function () {
                    var figure = $("#AFigure").next().children().val();
                    if (figure != null && figure != "") {
                        $("#HAID").combobox('options').required = false;
                        $("#HAID").combobox('textbox').validatebox('options').required = false;
                        $("#HAID").combobox('validate');
                    } else {
                        $("#HAID").combobox('options').required = true;
                        $("#HAID").combobox('textbox').validatebox('options').required = true;
                        $("#HAID").combobox('validate');
                    }
                });
            })
            $(function () {
                $("#AModel").textbox('textbox').bind('keyup', function () {
                    var model = $("#AModel").next().children().val();
                    if (model != null && model != "") {
                        $("#HAID").combobox('options').required = false;
                        $("#HAID").combobox('textbox').validatebox('options').required = false;
                        $("#HAID").combobox('validate');
                    } else {
                        $("#HAID").combobox('options').required = true;
                        $("#HAID").combobox('textbox').validatebox('options').required = true;
                        $("#HAID").combobox('validate');
                    }
                });
            })
            $(function () {
                $("#AProductName").textbox('textbox').bind('keyup', function () {
                    var productName = $("#AProductName").next().children().val();
                    if (productName != null && productName != "") {
                        $("#HAID").combobox('options').required = false;
                        $("#HAID").combobox('textbox').validatebox('options').required = false;
                        $("#HAID").combobox('validate');
                    } else {
                        $("#HAID").combobox('options').required = true;
                        $("#HAID").combobox('textbox').validatebox('options').required = true;
                        $("#HAID").combobox('validate');
                    }
                });
            })
            $('#HiddenHouseID').val(HID);
            OldHouseID = 0;
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    if (rec.HouseID == 9) {
                        $('#ACompany').combobox('setValue', 0);
                    } else {
                        $('#ACompany').combobox('setValue', -1);
                    }
                    $('#HAID').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    $('#HAID').combobox('reload', url);
                    if ($('#AHouseID').combobox('getValue') == 9 && $('#ALogisID').combobox('getValue') == 34) {
                        $('#PostponeShip').show();
                        $('#PostponeShipLab').show();

                    } else {
                        $("#PostponeShip").attr("checked", false);
                        $('#PostponeShip').hide();
                        $('#PostponeShipLab').hide();
                    }
                    var url = '../Client/clientApi.aspx?method=QueryAllUpClientDep&type=1&houseID=' + rec.HouseID;
                    $('#ABelongDepart').combobox('reload', url);
                    $('#ABelongDepart').combobox('select', '-1');
                    var HouseIDArr = [9, 44, 45];
                    if (OldHouseID != rec.HouseID) {
                        if ($.inArray(rec.HouseID, HouseIDArr) > -1 && $.inArray(OldHouseID, HouseIDArr) > -1 || OldHouseID == 0 && $.inArray(parseInt(HID), HouseIDArr) > -1 && $.inArray(rec.HouseID, HouseIDArr) > -1) {
                            return;
                        }
                        $('#SAcceptPeople').combobox({
                            valueField: 'ClientNum', textField: 'Boss',
                            url: '../Client/clientApi.aspx?method=AutoCompleteClient&HouseID=' + $("#AHouseID").combobox('getValue'),
                            onSelect: onClientChanged,
                            required: true
                        });
                        $('#SAcceptPeople').combobox('textbox').bind('focus', function () { $('#SAcceptPeople').combobox('showPanel'); });
                        $('#AcceptPeople').combobox('clear'); //清除已选中数据
                        $('#AcceptPeople').combo("panel").empty();//清除面板
                    }
                    //保存限制订单类型为OES的仓库ID
                    var arr = [13, 14, 15, 23, 30];
                    if ($.inArray(rec.HouseID, arr) > -1) {
                        $('#OES').prop('checked', true);
                        $(".ThrowGood").prop("disabled", true);
                        $('#ALogisID').combobox('setValue', 34);
                    } else {
                        $('#OES').prop('checked', false);
                        $(".ThrowGood").prop("disabled", false);
                    }
                    OldHouseID = rec.HouseID;
                }
            });
            //所在仓库
            $('#HAID').combobox({
                onSelect: function (rec) {
                    var HAID = $("#HAID").combobox('getValue');
                    if (HAID == 2843 || HAID == 2872) {
                        $('#ALogisID').combobox('setValue', 62);
                    } else {
                        //$('#ALogisID').combobox('clear');
                    }
                    $.ajax({
                        url: "../House/houseApi.aspx?method=QueryAreaByAreaID&AreaID=" + HAID,
                        cache: false,
                        async: true,
                        dataType: "json",
                        success: function (text) {
                            if (text.OrderDep != null && text.OrderDep != "") {
                                $('#ADep').textbox('setValue', text.OrderDep);
                            } else {
                                $('#ADep').textbox('setValue', '<%= UserInfor.DepCity%>');
                            }
                        }
                    });
                }
            });
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            $('#HAID').combobox('clear');
            var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=<%=UserInfor.HouseID%>';
            if (HID == "64") {
                $('#HAID').combobox({
                    'url': url, onLoadSuccess: function () {
                        //默认选中第一个
                        var array = $(this).combobox("getData");
                        for (var item in array[0]) {
                            if (item == "AreaID") {
                                $(this).combobox('select', array[0][item]);
                            }
                        }
                    }
                });
            } else {
                $('#HAID').combobox('reload', url);
                if (HID == "9") {
                    $('#ACompany').combobox('setValue', 0);
                }
            }

            var IHH = "<%=UserInfor.IsHeadHouse%>";
            if (IHH == "1") {
                $('#HAID').combobox('setText', '<%=UserInfor.HeadHouseName%>');
                $('#HAID').combobox('setValue', '<%=UserInfor.HeadHouseID%>');
                $("#HAID").combobox("readonly", true);
                $("#AHouseID").combobox("readonly", true);
            }
            else {
                $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
                $('#HAID').combobox('textbox').bind('focus', function () { $('#HAID').combobox('showPanel'); });
            }

            $('#APID').combobox('textbox').bind('focus', function () { $('#APID').combobox('showPanel'); });
            $('#ASID').combobox('textbox').bind('focus', function () { $('#ASID').combobox('showPanel'); });

            $("#ThrwG").click(function () {
                if (!this.checked) {
                    var outDgrows = $("#outDg").datagrid('getData').rows;
                    if (outDgrows.length > 0) {
                        for (var i = 0; i < outDgrows.length; i++) {
                            if (outDgrows[i].RuleType + "".indexOf('4') != "-1") {
                                if (parseInt(outDgrows[i].InPiece) - parseInt(outDgrows[i].Piece) > 0) {
                                    $.ajax({
                                        url: "orderApi.aspx?method=QueryPriceRuleBankInfoToID&RuleID=" + outDgrows[i].RuleID + "&HouseID=" + outDgrows[i].HouseID + "&TypeID=" + outDgrows[i].TypeID + "&Specs=" + encodeURIComponent(outDgrows[i].Specs) + "&Figure=" + encodeURIComponent(outDgrows[i].Figure) + "&Batch=" + outDgrows[i].Batch + "&ClientNum=" + $('#HiddenClientNum').val() + "&OrderNo=" + $('#OrderNo').val() + "&RuleType=4",
                                        cache: false,
                                        async: false,
                                        dataType: "json",
                                        success: function (text) {
                                            if (text.Result == true) {
                                                if (text.RuleContent < parseInt(outDgrows[i].InPiece) - parseInt(outDgrows[i].Piece)) {
                                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单中第' + (parseInt(i) + 1) + '条货物限购数量为' + text.RuleContent + '，请拉下订单后再取消抛货单！', 'warning');
                                                    $('#ThrwG').prop("checked", true);

                                                    return;
                                                }
                                            } else {
                                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.RuleContent + '！', 'warning');
                                                $('#AcceptPeople').combobox('setValue', $('#HiddenAcceptPeople').val());
                                                return;
                                            }
                                        }
                                    });
                                }
                            }
                        }
                    }
                }
            })
            //隐藏付款方式周期数
            $('#PeriodsNum').next(".numberbox").hide();
            $('#CollectMoney').next(".numberbox").hide();
            $("#PeriodsNumLab").css("visibility", "hidden");
            $("#CollectMoney").numberbox('options').required = false;
            $("#CollectMoney").numberbox('textbox').validatebox('options').required = false;
            $("#CollectMoney").numberbox('validate');

            $("#PeriodsNum").numberbox('options').required = false;
            $("#PeriodsNum").numberbox('textbox').validatebox('options').required = false;
            $("#PeriodsNum").numberbox('validate');
            $('#PostponeShip').hide();
            $('#PostponeShipLab').hide();

            if (RoleCName.indexOf("安泰路斯") >= 0) {
                $('#APID').combobox('setValue', '1');
                $("#APID").combobox("readonly", true);
                $('#APID').combobox('textbox').unbind('focus');
                $('#APID').combobox('textbox').css('background-color', '#e8e8e8');
                //一级产品
                $('#ASID').combobox('clear');
                $('#ASID').combobox({
                    url: '../Product/productApi.aspx?method=QueryALLOneProductType&PID=1', valueField: 'TypeID', textField: 'TypeName', AddField: 'PinyinName',
                    filter: function (q, row) {
                        var opts = $(this).combobox('options');
                        return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                    },
                });

                $('#ASID').combobox('setValue', '258');
                $("#ASID").combobox("readonly", true);
                $('#ASID').combobox('textbox').unbind('focus');
                $('#ASID').combobox('textbox').css('background-color', '#e8e8e8');
                $("#AHouseID").combobox("readonly", true);
                $('#AHouseID').combobox('textbox').unbind('focus');
                $('#AHouseID').combobox('textbox').css('background-color', '#e8e8e8');

                $(".ThrowGood").hide();
                $("#SuPei").show();
                $("#SuPeiLab").show();
                $(".DeliverySettlement").hide();
                $('#HAID').combobox('setValue', '944');
                $("#HAID").combobox("readonly", true);
                $('#HAID').combobox('textbox').unbind('focus');
                $('#HAID').combobox('textbox').css('background-color', '#e8e8e8');
                $("#ABelongDepart").combobox("readonly", true);
                $('#ABelongDepart').combobox('textbox').unbind('focus');
                $('#ABelongDepart').combobox('textbox').css('background-color', '#e8e8e8');
                $("#ACompany").combobox("readonly", true);
                $('#ACompany').combobox('textbox').unbind('focus');
                $('#ACompany').combobox('textbox').css('background-color', '#e8e8e8');
                $('#ALogisID').combobox('setValue', '34');
                $(".HID").hide();
                $("#SaleManID").combobox("readonly", true);
                $('#SaleManID').combobox('textbox').unbind('focus');
                $('#SaleManID').combobox('textbox').css('background-color', '#e8e8e8');
                $('#TransportFee').combobox('textbox').css('background-color', '#e8e8e8');
                $('#APiece').combobox('textbox').css('background-color', '#e8e8e8');
                $('#CreateAwb').combobox('textbox').css('background-color', '#e8e8e8');
                $('#TotalCharge').combobox('textbox').css('background-color', '#e8e8e8');
                $('#ARemark').attr('placeholder', "请输入订单备注信息");

                //客户姓名
                $('#SAcceptPeople').combobox({
                    //$('#AcceptPeople').combobox({
                    valueField: 'ClientNum', textField: 'Boss',
                    url: '../Client/clientApi.aspx?method=AutoCompleteClient&UserName=安鹏',
                    onSelect: onClientChanged,
                    required: true
                });
            }
            else if (RoleCName.indexOf("汕头科矿") >= 0) {
                //汕头科矿 华南枢纽仓前置仓权限
                $('#APID').combobox('setValue', '1');
                $("#APID").combobox("readonly", true);
                $('#APID').combobox('textbox').unbind('focus');
                $('#APID').combobox('textbox').css('background-color', '#e8e8e8');
                //一级产品
                $('#ASID').combobox('clear');
                var themecombo2 = [{ 'TypeName': '樱花', 'TypeID': '540' }, { 'TypeName': '台翔', 'TypeID': '541' }, { 'TypeName': '赛为', 'TypeID': '542' }, { 'TypeName': '衡钻', 'TypeID': '543' }, { 'TypeName': '蒲公英', 'TypeID': '565' }];
                $("#ASID").combobox("loadData", themecombo2);
                //$('#ASID').combobox('setValue', '540');
                // $("#ASID").combobox("readonly", true);
                $('#ASID').combobox('textbox').unbind('focus');
                //$('#ASID').combobox('textbox').css('background-color', '#e8e8e8');
                $("#AHouseID").combobox("readonly", true);
                $('#AHouseID').combobox('textbox').unbind('focus');
                $('#AHouseID').combobox('textbox').css('background-color', '#e8e8e8');
                var checkbox = document.getElementById("IsPrintPrice");
                // 将复选框设置为非选中状态
                checkbox.checked = false;
                //$("#SaleManID").combobox("readonly", true);
                //$('#SaleManID').combobox('textbox').unbind('focus');
                //$('#SaleManID').combobox('textbox').css('background-color', '#e8e8e8');
                $(".ThrowGood").hide();
                $("#SuPei").hide();
                $("#SuPeiLab").hide();
                $(".DeliverySettlement").hide();
                $("#HAID").combobox("readonly", true);
                $('#HAID').combobox('textbox').unbind('focus');
                $('#HAID').combobox('textbox').css('background-color', '#e8e8e8');
                $("#ABelongDepart").combobox("readonly", true);
                $('#ABelongDepart').combobox('textbox').unbind('focus');
                $('#ABelongDepart').combobox('textbox').css('background-color', '#e8e8e8');
                $("#ACompany").combobox("readonly", true);
                $('#ACompany').combobox('textbox').unbind('focus');
                $('#ACompany').combobox('textbox').css('background-color', '#e8e8e8');
                $('#ALogisID').combobox('setValue', '62');
                $(".HID").hide();

                $('#TransportFee').combobox('textbox').css('background-color', '#e8e8e8');
                $('#APiece').combobox('textbox').css('background-color', '#e8e8e8');
                $('#CreateAwb').combobox('textbox').css('background-color', '#e8e8e8');
                $('#TotalCharge').combobox('textbox').css('background-color', '#e8e8e8');
                $('#ARemark').attr('placeholder', "请输入订单备注信息");
                //$('#SaleManID').combobox('setValue', '2940');

                //客户姓名
                $('#SAcceptPeople').combobox({
                    //$('#AcceptPeople').combobox({
                    valueField: 'ClientNum', textField: 'Boss',
                    url: '../Client/clientApi.aspx?method=AutoCompleteClient&UpClientID=8',
                    onSelect: onClientChanged,
                    required: true
                });
            }
            else {
                //客户姓名
                $('#SAcceptPeople').combobox({
                    //$('#AcceptPeople').combobox({
                    valueField: 'ClientNum', textField: 'Boss', AddField: 'PinyinName',
                    url: '../Client/clientApi.aspx?method=AutoCompleteClient',
                    onSelect: onClientChanged,
                    filter: function (q, row) {
                        var opts = $(this).combobox('options');
                        return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                    },
                    required: true
                });
                $('#DeliveryType').combobox('setValue', '');

            }
            $('#DeliveryType').combobox('textbox').bind('focus', function () { $('#DeliveryType').combobox('showPanel'); });
            $('#SAcceptPeople').combobox('textbox').bind('focus', function () { $('#SAcceptPeople').combobox('showPanel'); });
        });

        //查询
        function dosearch() {
            if ($("#AHouseID").combobox('getValue') == undefined || $("#AHouseID").combobox('getValue') == '') {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择区域大仓！', 'warning');
                return;
            }
            if ($("#HAID").combobox('options').required) {
                if ($("#HAID").combobox('getValue') == undefined || $("#HAID").combobox('getValue') == '') {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择所在仓库！', 'warning');
                    return;
                }
            }

            if ($("#SAcceptPeople").combobox('getValue') == undefined || $("#SAcceptPeople").combobox('getValue') == '') {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择收货人！', 'warning');
                return;
            }
            var type = 0;
            var columns = [];
            columns.push({ title: '', field: 'ID', checkbox: true, width: '10%' });
            columns.push({
                title: '品牌', field: 'TypeName', width: '6%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '规格', field: 'Specs', width: '7%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '型号', field: 'Model', width: '7%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '花纹', field: 'Figure', width: '8%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '在库数量', field: 'Piece', width: '5%', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '批次', field: 'Batch', width: '5%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '载重', field: 'LoadIndex', width: '3%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '速度', field: 'SpeedLevel', width: '3%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '销售价', field: 'SalePrice', width: '5%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '门店价', field: 'TradePrice', width: '4%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            if (HID == 64) {
                columns.push({
                    title: '供应商', field: 'Supplier', width: '8%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            } else {
                columns.push({
                    title: '产品来源', field: 'Source', width: '8%', formatter: function (value) {
                        return "<span title='" + GetSourceName(value) + "'>" + GetSourceName(value) + "</span>";
                    }
                });
            }
            columns.push({
                title: '货位代码', field: 'ContainerCode', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在区域', field: 'AreaName', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在仓库', field: 'FirstAreaName', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '归属部门', field: 'BelongDepart', width: '5%', formatter: function (value) {
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
            if ($("#AHouseID").combobox('getValue') != $('#HiddenHouseID').val()) {
                $('#HiddenHouseID').val($("#AHouseID").combobox('getValue'));
                type = 1;
                if ($("#AHouseID").combobox('getValue') == 45) {
                    columns.push({
                        title: '离客距离', field: 'Distance', width: '5%', formatter: function (value) {
                            if (value != "" && value != null) {
                                return "<span title='" + value / 1000 + "千米'>" + value / 1000 + "千米</span>";
                            } else {
                                return "<span title=''></span>";
                            }
                        }
                    });
                    columns.push({
                        title: '当前耗时', field: 'Duration', width: '5%', formatter: function (value) {
                            if (value != "" && value != null) {
                                return "<span title='" + (value / 60).toFixed(2) + "分钟'>" + (value / 60).toFixed(2) + "分钟</span>";
                            } else {
                                return "<span title=''></span>";
                            }
                        }
                    });
                }
            }
            columns.push({
                title: '产品名称', field: 'ProductName', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            //columns.push({
            //    title: '单价', field: 'UnitPrice', width: '5%', align: 'right', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});

            columns.push({ title: '入库时间', field: 'InHouseTime', width: '130px', formatter: DateTimeFormatter });

            columns.push({ title: '业务员销售价', field: 'ActSalePrice', hidden: true });
            columns.push({ title: '是否优惠', field: 'IsRuleBank', hidden: true });
            columns.push({ title: '优惠类型', field: 'RuleType', hidden: true });
            columns.push({ title: '优惠规则ID', field: 'RuleID', hidden: true });
            columns.push({ title: '优惠规则名称', field: 'RuleTitle', hidden: true });
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../House/houseApi.aspx?method=QueryALLHouseData';
            $('#dg').datagrid('load', {
                Specs: $('#ASpecs').val(),
                Model: $('#AModel').val(),
                GoodsCode: $('#AGoodsCode').val(),
                PID: $("#APID").combobox('getValue'),//一级产品
                SID: $("#ASID").combobox('getValue'),//二级产品
                //TreadWidth: $('#ATreadWidth').val(),
                ProductName: $('#AProductName').val(),
                //FlatRatio: $('#AFlatRatio').val(),
                Figure: $('#AFigure').val(),
                BatchYear: $('#ABatchYear').combobox('getValue'),
                //HubDiameter: $('#AHubDiameter').val(),
                //LoadIndex: $('#ALoadIndex').val(),
                Company: $("#ACompany").combobox('getValue'),
                HAID: $("#HAID").combobox('getValue'),
                BelongDepart: $("#ABelongDepart").combobox('getValue'),
                // SpeedLevel: $("#ASpeedLevel").combobox('getValue'),
                ClientNum: $('#HiddenClientNum').val(),
                ADID: $("#AcceptPeople").combobox('getValue'),
                HouseID: $("#AHouseID").combobox('getValue'),//仓库ID
                IsLockStock: $('#AlockStock').combobox('getValue'),
                IsQueryLockStock: IsQueryLockStock,
                IsShowStock: "0",
            });
            if (type == 1) {
                $('#dg').datagrid({
                    columns: [columns]
                })
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
    <div id="tbDepmanifest" class="easyui-tabs" data-options="fit:true">
        <div title="货物查询" data-options="iconCls:'icon-search'">

            <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
                <table>
                    <tr>
                        <td style="text-align: right;">规格:
                        </td>
                        <td>
                            <input id="ASpecs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 95px" />
                        </td>
                        <td id="AModelTd" style="text-align: right;">型号:
                        </td>
                        <td>
                            <input id="AModel" class="easyui-textbox" data-options="prompt:'请输入产品型号'" style="width: 95px" />
                        </td>
                        <td style="text-align: right;">产品名称:
                        </td>
                        <td>
                            <input id="AProductName" class="easyui-textbox" data-options="prompt:'请输入产品名称'" style="width: 95px" />
                        </td>
                        <td style="text-align: right;">一级产品:
                        </td>
                        <td>
                            <input id="APID" class="easyui-combobox" style="width: 95px;" panelheight="auto" />
                        </td>
                        <td class="ASpeedLevelTd" style="text-align: right;">归属部门:
                        </td>
                        <td class="ASpeedLevelTd">
                            <input id="ABelongDepart" class="easyui-combobox" style="width: 95px;" data-options="valueField:'ID',textField:'DepName',editable:false" />
                            <%--<select class="easyui-combobox" id="ABelongDepart" style="width: 95px;" panelheight="auto" editable="false">
                                <option value="-1">全部</option>
                                <option value="0">RE渠道销售部</option>
                                <option value="1">OE渠道销售部</option>
                            </select>--%>
                        </td>
                        <%-- <td class="ASpeedLevelTd" style="text-align: right;">速度级别:
                        </td>
                        <td class="ASpeedLevelTd">
                            <input class="easyui-combobox" id="ASpeedLevel" data-options="url:'../Data/TyreSpeedLevel.json',method:'get',valueField:'id',textField:'text',panelHeight:'auto'"
                                style="width: 95px;">
                        </td>--%>
                        <td style="text-align: right;">区域大仓:
                        </td>
                        <td>
                            <input id="AHouseID" class="easyui-combobox" style="width: 95px;" data-options="required:true" />
                            <input type="hidden" id="HiddenHouseID" />
                        </td>
                    </tr>
                    <tr>
                        <td id="AFigureTd" style="text-align: right;">花纹:
                        </td>
                        <td>
                            <input id="AFigure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 95px">
                        </td>
                        <td style="text-align: right;">年周:
                        </td>
                        <td>
                            <%--   <input id="ABatch" class="easyui-numberbox" data-options="prompt:'请输入批次'" style="width: 80px">--%>
                            <select class="easyui-combobox" id="ABatchYear" style="width: 95px;" panelheight="auto">
                                <option value="-1">全部</option>
                                <option value="25">25年</option>
                                <option value="24">24年</option>
                                <option value="23">23年</option>
                                <option value="22">22年</option>
                                <option value="21">21年</option>
                                <option value="20">20年</option>
                                <option value="19">19年</option>
                                <option value="18">18年</option>
                            </select>
                        </td>
                        <td id="AGoodsCodeTd" style="text-align: right;">货品代码:
                        </td>
                        <td>
                            <input id="AGoodsCode" class="easyui-textbox" data-options="prompt:'请输入货品代码'" style="width: 95px" />
                        </td>
                        <%-- <td style="text-align: right;">胎面宽度:
                        </td>
                        <td>
                            <input id="ATreadWidth" class="easyui-numberbox" data-options="min:0,precision:2" style="width: 80px">
                        </td>--%>
                        <%--   <td style="text-align: right;">扁平比:
                        </td>
                        <td>
                            <input id="AFlatRatio" class="easyui-numberbox" data-options="min:0,precision:2" style="width: 100px">
                        </td>--%>

                        <td style="text-align: right;">二级产品:
                        </td>
                        <td>
                            <input id="ASID" class="easyui-combobox" style="width: 95px;" data-options="valueField:'TypeID',textField:'TypeName'" />
                        </td>
                        <%--  <td style="text-align: right;">轮毂直径:
                        </td>
                        <td>
                            <input id="AHubDiameter" class="easyui-numberbox" data-options="min:0,precision:2" style="width: 100px">
                        </td>--%>

                        <%--<td class="ALoadIndexTd" style="text-align: right;">载重指数:
                        </td>
                        <td class="ALoadIndexTd">
                            <input id="ALoadIndex" class="easyui-numberbox" data-options="min:0,precision:2" style="width: 95px">
                        </td>--%>
                        <td class="ASpeedLevelTd" style="text-align: right;">归属公司:
                        </td>
                        <td class="ASpeedLevelTd">
                            <select class="easyui-combobox" id="ACompany" style="width: 95px;" panelheight="auto" editable="false">
                                <option value="-1">全部</option>
                                <option value="0">迪乐泰</option>
                                <option value="1">好来运</option>
                                <option value="2">富添盛</option>
                            </select>
                        </td>
                        <td style="text-align: right;">所在仓库:
                        </td>
                        <td>
                            <input id="HAID" class="easyui-combobox" style="width: 95px;" data-options="valueField:'AreaID',textField:'Name',required:true" panelheight="auto" />
                        </td>

                    </tr>
                </table>
            </div>
            <input type="hidden" id="DisplayNum" />
            <input type="hidden" id="DisplayPiece" />
            <table id="dg" class="easyui-datagrid">
            </table>
            <table id="outDg" class="easyui-datagrid">
            </table>
            <div id="toolbar">
                <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="plOutCargo()">
            &nbsp;添加上订单&nbsp;</a>&nbsp;&nbsp;
                <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删除下订单&nbsp;</a>&nbsp;&nbsp;客户名称:&nbsp;<input name="AAcceptPeople" id="SAcceptPeople" style="width: 120px;" class="easyui-combobox AcceptPeople" data-options="valueField:'ClientNum',textField:'Boss',editable:true,required:true" />&nbsp;&nbsp;收货人:&nbsp;<input id="AcceptPeople" name="AcceptPeople" style="width: 220px;" data-options="required:true" class="easyui-combobox AcceptPeople" />&nbsp;&nbsp;业务名称:&nbsp;<input id="ABusinessID" name="ABusinessID" style="width: 220px;" data-options="required:true" class="easyui-combobox AcceptPeople" />
                <span id="IsQueryLockStock">&nbsp;&nbsp;锁定状态:
                    <select class="easyui-combobox" id="AlockStock" style="width: 100%;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">未锁定</option>
                        <option value="1">已锁定</option>
                    </select>&nbsp; </span>
            </div>
            <!--Begin 出库操作-->

            <div id="dlg" class="easyui-dialog" style="width: 420px; height: 400px; padding: 2px 2px"
                closed="true" buttons="#dlg-buttons">
                <form id="fm" class="easyui-form" method="post">
                    <input type="hidden" id="InPiece" />
                    <input type="hidden" id="InIndex" />
                    <table>
                        <tr>
                            <td style="text-align: right;">拉上订单数量：
                            </td>
                            <td>
                                <input name="Numbers" id="Numbers" class="easyui-numberbox" data-options="min:0,precision:0"
                                    style="width: 200px;" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">业务员价格：</td>
                            <td>
                                <input type="hidden" id="RuleType" />
                                <input type="hidden" id="RuleID" />
                                <input type="hidden" id="RuleTitle" />
                                <input type="hidden" id="index" />
                                <input name="ActSalePrice" id="ActSalePrice" class="easyui-numberbox" data-options="min:0,precision:2"
                                    style="width: 200px;" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">门店价：</td>
                            <td>
                                <input id="SystemSalePrice" class="easyui-numberbox" data-options="min:0,precision:2"
                                    style="width: 200px;" readonly="readonly" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3"><span style="color: #d27a7a; font-weight: bold;">注意：未使用优惠业务员价低于系统销售价,保存订单自动提交改价申请审批！</span></td>
                        </tr>
                    </table>
                    <div id="lblRule">
                    </div>
                </form>
            </div>
            <div id="dlg-buttons">
                <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="outOK()">&nbsp;确&nbsp;定&nbsp;</a>
                <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
            </div>
            <!--End 出库操作-->

        </div>
        <div title="保存订单" data-options="iconCls:'icon-page_add'">
            <form id="fmDep" class="easyui-form" method="post">
                <input type="hidden" name="SaleManName" id="SaleManName" />
                <input type="hidden" name="SaleCellPhone" id="SaleCellPhone" />
                <input type="hidden" name="HouseCode" id="HouseCode" />
                <input type="hidden" name="BelongHouse" id="BelongHouse" />
                <input type="hidden" name="HouseID" id="HouseID" />
                <input type="hidden" name="ONum" id="ONum" />
                <input type="hidden" name="OutNum" id="OutNum" />
                <input type="hidden" name="ShopCode" id="ShopCode" />
                <input type="hidden" name="ClientNum" id="ClientNum" />
                <input type="hidden" name="BusinessID" id="BusinessID" />
                <input type="hidden" id="Coupon" name="Coupon" />
                <input type="hidden" id="HiddenAcceptPeople" />
                <input type="hidden" id="HiddenClientNum" />
                <input type="hidden" id="HiddenLimitID" />
                <input type="hidden" id="HiddenLimitTitle" />
                <input type="hidden" id="HiddenRemark" />
                <input type="hidden" id="ClientType" name="ClientType" />
                <input type="hidden" id="HiddenLongitude" />
                <input type="hidden" id="HiddenLatitude" />
                <input type="hidden" id="HiddenClientSelectName" name="HiddenClientSelectName" />
                <input type="hidden" id="HiddenProvince" name="HiddenProvince" />
                <input type="hidden" id="HiddenCity" name="HiddenCity" />


                <div id="saPanel">
                    <table style="width: 100%">
                        <tr>

                            <td style="color: Red; font-weight: bolder; text-align: right;">出发站:
                            </td>
                            <td>
                                <input name="Dep" id="ADep" class="easyui-textbox" style="width: 80px" data-options="required:true " />
                                <%-- <input name="Dep" id="ADep" class="easyui-combobox" style="width: 80px;" data-options="valueField:'CityName',textField:'CityName',url:'../systempage/sysService.aspx?method=QueryAllCity',required:true "
                                    panelheight="auto" />--%>
                            </td>
                            <td style="color: Red; font-weight: bolder; text-align: right;">到达站:
                            </td>
                            <td>
                                <input name="Dest" id="ADest" class="easyui-combobox" style="width: 80px;" data-options="valueField:'CityName',textField:'CityName',url:'../systempage/sysService.aspx?method=QueryAllCity',required:true,editable:true " />
                            </td>
                            <td style="text-align: right;">收货人:
                            </td>
                            <td>
                                <input id="AAcceptPeople" data-options="disabled:true" class="easyui-textbox" style="width: 110px;" />
                                <input type="hidden" id="HiddenAAcceptPeople" />
                            </td>
                            <td style="text-align: right;">公司名称:
                            </td>
                            <td>
                                <input name="AcceptUnit" id="AAcceptUnit" data-options="disabled:true" class="easyui-textbox" style="width: 100px;" />
                            </td>

                            <td style="text-align: right;">收货地址:
                            </td>
                            <td>
                                <input name="AcceptAddress" id="AAcceptAddress" data-options="disabled:true" style="width: 100%;" class="easyui-textbox" />
                            </td>
                            <td style="text-align: right;">电话号码:
                            </td>
                            <td>
                                <input name="AcceptTelephone" id="AAcceptTelephone" data-options="disabled:true" class="easyui-textbox"
                                    style="width: 100px;" />
                            </td>
                            <td style="text-align: right;">手机号码:
                            </td>
                            <td>
                                <input name="AcceptCellphone" id="AAcceptCellphone" class="easyui-textbox" data-options="required:true,disabled:true" style="width: 100px;" />
                            </td>

                        </tr>

                        <tr>
                            <td style="text-align: right;">销售费:
                            </td>
                            <td>
                                <input name="TransportFee" id="TransportFee" data-options="min:0,precision:2,disabled:true" class="easyui-numberbox"
                                    style="width: 80px;" />
                                <input type="hidden" id="hiddenTransportFee" />
                            </td>
                            <td style="text-align: right;">送货费:
                            </td>
                            <td>
                                <input name="TransitFee" id="TransitFee" data-options="min:0,precision:2" class="easyui-numberbox"
                                    style="width: 80px;" />
                            </td>
                            <td style="text-align: right; display: none"></td>
                            <td style="display: none"></td>
                            <td style="text-align: right; display: none">其它费用:
                            </td>
                            <td style="display: none">
                                <input name="OtherFee" id="OtherFee" class="easyui-numberbox" data-options="min:0,precision:2"
                                    style="width: 80px;" />
                            </td>
                            <td style="text-align: right; color: #f53333">优惠券:
                            </td>
                            <td>
                                <select name="InsuranceFee" id="InsuranceFee" class="easyui-combogrid" style="width: 110px">
                                </select>
                            </td>
                            <%--<td style="text-align: right;">保险费用:
                            </td>
                            <td>
                                <input name="InsuranceFee" id="InsuranceFee" data-options="min:0,precision:2" class="easyui-numberbox"
                                    style="width: 150px;" />
                            </td>--%>
                            <td style="text-align: right; display: none">回扣:
                            </td>
                            <td style="display: none">
                                <input name="Rebate" id="Rebate" data-options="min:0,precision:2" class="easyui-numberbox"
                                    style="width: 100px;" />
                            </td>
                            <td style="text-align: right;">费用合计:
                            </td>
                            <td>
                                <input name="TotalCharge" id="TotalCharge" class="easyui-numberbox" data-options="min:0,precision:2,disabled:true"
                                    style="width: 100px;" />
                                <input type="hidden" id="hiddenTotalCharge" />
                            </td>

                            <td style="text-align: right;">付款方式:
                            </td>
                            <td>
                                <select class="easyui-combobox" id="CheckOutType" name="CheckOutType" style="width: 100px;" data-options="onSelect:CheckOutTypeChange" panelheight="auto" editable="false">
                                    <option value="2">月结</option>
                                    <option value="0">现付</option>
                                    <option value="1">周期</option>
                                    <option value="4">代收</option>
                                </select>
                                <input id="PeriodsNum" data-options="min:0,precision:0,required:true" class="easyui-numberbox" style="width: 35px;" />
                                <label id="PeriodsNumLab" style="text-align: left;">天</label>
                                <input id="CollectMoney" name="CollectMoney" data-options="min:0,precision:2,required:true" class="easyui-numberbox" style="width: 70px;" />

                            </td>
                            <td style="text-align: right;">物流公司:
                            </td>
                            <td>
                                <input name="LogisID" id="ALogisID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'ID',textField:'LogisticName',url:'../systempage/sysService.aspx?method=QueryAllLogistic'" />
                            </td>
                            <td style="text-align: right;">物流费用:
                            </td>
                            <td>
                                <input name="DeliveryFee" id="DeliveryFee" data-options="min:0,precision:2" class="easyui-numberbox"
                                    style="width: 100px;" />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">总数量:
                            </td>
                            <td>
                                <input name="Piece" id="APiece" class="easyui-numberbox" data-options="min:0,precision:0,required:true,disabled:true"
                                    style="width: 80px;" />
                            </td>
                            <td style="text-align: right;">开单员:
                            </td>
                            <td>
                                <input name="CreateAwb" id="CreateAwb" class="easyui-textbox" readonly="readonly" style="width: 80px;" />
                            </td>
                            <td style="text-align: right;">开单时间:
                            </td>
                            <td>
                                <input name="CreateDate" id="CreateDate" class="easyui-datetimebox" data-options="formatter:AllDateTime"
                                    readonly="readonly" style="width: 150px;" />
                            </td>
                            <td style="text-align: right;">业务员:
                            </td>
                            <td>
                                <input name="SaleManID" id="SaleManID" class="easyui-combobox" style="width: 100px;" data-options="url: 'orderApi.aspx?method=QueryUserByDepCode',valueField: 'LoginName',textField: 'UserName', onSelect: onSaleManIDChanged,required:true" />
                            </td>
                            <td style="text-align: right;">送货方式:</td>
                            <td>
                                <select class="easyui-combobox" id="DeliveryType" name="DeliveryType" data-options="required:true" style="width: 100px;" panelheight="auto">
                                    <option value="2">普送</option>
                                    <option value="0">急送</option>
                                    <option value="1">自提</option>
                                </select>
                            </td>
                            <td style="text-align: right;">副单号:
                            </td>
                            <td>
                                <input name="HAwbNo" id="HAwbNo" class="easyui-textbox" style="width: 100px;" />
                            </td>
                            <td style="text-align: right;" class="DeliverySettlement">物流结算:</td>
                            <td class="DeliverySettlement">
                                <select class="easyui-combobox" id="DeliverySettlement" name="DeliverySettlement" style="width: 100px;" panelheight="auto" editable="false">
                                    <option value="-1">&nbsp;</option>
                                    <option value="0">现付</option>
                                    <option value="1">到付</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;" rowspan="2">备注:
                            </td>
                            <td colspan="6" rowspan="2">
                                <textarea name="Remark" id="ARemark" rows="3" placeholder="请输入订单备注" style="width: 500px; resize: none"></textarea>
                            </td>
                            <td colspan="3" rowspan="2" id="ThrowGood">
                                <input class="ThrowGood" type="checkbox" name="ThrowGood" id="ThrwG" value="1" />
                                <label class="ThrowGood" for="ThrwG">抛货单</label>
                                <%--<input class="ThrowGood" type="checkbox" name="ThrowGood" id="HuiYi" value="4" />
                                <label class="ThrowGood" for="HuiYi">周期胎</label>--%>
                                <input class="ThrowGood" type="checkbox" name="ThrowGood" id="LiPei" value="8" />
                                <label class="ThrowGood" for="LiPei">理赔单</label>
                                <%--                         <input class="ThrowGood" type="checkbox" name="ThrowGood" id="Tran" value="2" />
                                <label class="ThrowGood" for="Tran">内部调货单</label>--%>
                                <%-- <input class="ThrowGood" type="checkbox" name="ThrowGood" id="SuPei" value="15" />
                                <label class="ThrowGood" for="SuPei" id="SuPeiLab">速配单</label>--%>
                                <input class="ThrowGood" type="checkbox" name="ThrowGood" id="DianShang" value="20" />
                                <label class="ThrowGood" for="DianShang" id="DianShangLab">电商单</label>
                                <input class="ThrowGood" type="checkbox" name="ThrowGood" id="TuiCangDan" value="25" />
                                <label class="ThrowGood" for="TuiCangDan" id="TuiCangDanLab">退仓单</label>
                                <%--<input class="ThrowGood" type="checkbox" name="ThrowGood" id="DaiFa" value="3" />
                                <label class="ThrowGood" for="DaiFa">代发单</label>--%>
                                <br />
                                <br />
                                <%--<input class="ThrowGood" type="checkbox" name="ThrowGood" id="YiKu" value="9" />
                                <label class="ThrowGood" for="YiKu">移库单</label>--%>
                                <input class="ThrowGood" type="checkbox" name="ThrowGood" id="ZiYou" value="13" />
                                <label class="ThrowGood" for="ZiYou">二批单</label>
                                <%--<input class="ThrowGood" type="checkbox" name="ThrowGood" id="ZhanShi" value="11" />
                                <label class="ThrowGood" for="ZhanShi">展示单</label>
                                <input class="ThrowGood" type="checkbox" name="ThrowGood" id="YiDi" value="18" />
                                <label class="ThrowGood" for="YiDi">异地单</label>
                                <input class="ThrowGood" type="checkbox" name="ThrowGood" id="Baoliang" value="14" />
                                <label class="ThrowGood" for="Baoliang">报量单</label>--%>
                                <input class="ThrowGood" type="checkbox" name="ThrowGood" id="CuXiao" value="16" />
                                <label class="ThrowGood" for="CuXiao">促销单</label>
                                <input class="ThrowGood" type="checkbox" name="ThrowGood" id="OES" value="12" />
                                <label class="ThrowGood" for="OES">OES客户单</label>
                                <input class="ThrowGood" type="checkbox" name="ThrowGood" id="JiSong" value="17" />
                                <label class="ThrowGood" id="JiSongLab" for="JiSong">急送单</label>
                            </td>
                            <td class="HID" style="text-align: right;">到货仓库:</td>
                            <td class="HID">
                                <input id="HID" class="easyui-combobox" style="width: 100px;" />
                            </td>
                            <td style="text-align: right;">马牌单号:</td>
                            <td>
                                <input id="OpenOrderNo" name="OpenOrderNo" class="easyui-textbox" style="width: 120px;" />
                            </td>

                        </tr>

                        <tr>
                            <td style="text-align: right;">
                                <input type="checkbox" id="IsPrintPrice" name="IsPrintPrice" checked="checked" value="1" />
                                <label for="IsPrintPrice">打印价格</label></td>
                            <td colspan="5" style="text-align: right;">
                                <input type="checkbox" name="PostponeShip" id="PostponeShip" value="1" />
                                <label id="PostponeShipLab" for="PostponeShip">等通知发货</label>
                                <a href="#" id="btnSave" class="easyui-linkbutton" iconcls="icon-ok"
                                    plain="false" onclick="saveOutCargo()">&nbsp;保&nbsp;存&nbsp;订&nbsp;单</a>
                                &nbsp;&nbsp;
                                <a href="#" class="easyui-linkbutton" id="undo" iconcls="icon-clear" onclick="reset()">&nbsp;重&nbsp;置&nbsp;</a>
                            </td>
                        </tr>
                    </table>
                </div>
            </form>
            <table id="dgSave" class="easyui-datagrid">
            </table>

        </div>
    </div>
    <object id="LODOP_OB" title="dd" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA"
        width="0px" height="0px">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0px" height="0px"></embed>
    </object>
    <script type="text/javascript">
        //根据来源ID获取来源名称
        function GetSourceName(value) {
            var name = '';
            for (var i = 0; i < ProductSource.length; i++) {
                if (ProductSource[i].Source == value) {
                    name = ProductSource[i].SourceName;
                    break;
                }
            }
            return name;
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
                $('#CollectMoney').next(".numberbox").hide();
                $("#CollectMoney").numberbox('options').required = false;
                $("#CollectMoney").numberbox('textbox').validatebox('options').required = false;
                $("#CollectMoney").numberbox('validate');
            }
            else if (item.value == 4) {
                $("#PeriodsNumLab").css("visibility", "hidden");
                $('#PeriodsNum').next(".numberbox").hide();
                $('#CollectMoney').next(".numberbox").show();
                $("#CollectMoney").numberbox('options').required = true;
                $("#CollectMoney").numberbox('textbox').validatebox('options').required = true;
                $("#CollectMoney").numberbox('validate');
            }
            else {
                var Remark = $('#ARemark').val();
                Remark = Remark.replace("周期" + $('#PeriodsNum').val() + "天，", "");
                $('#ARemark').val(Remark);
                $('#PeriodsNum').numberbox('clear');
                $('#CollectMoney').next(".numberbox").hide();
                $('#PeriodsNum').next(".numberbox").hide();
                $("#PeriodsNumLab").css("visibility", "hidden");
                $("#PeriodsNum").numberbox('options').required = false;
                $("#PeriodsNum").numberbox('textbox').validatebox('options').required = false;
                $("#PeriodsNum").numberbox('validate');

                $("#CollectMoney").numberbox('options').required = false;
                $("#CollectMoney").numberbox('textbox').validatebox('options').required = false;
                $("#CollectMoney").numberbox('validate');
            }
        }
        $(function () {
            $('#ThrowGood').find('input[type=checkbox]').bind('click', function () {
                var id = $(this).attr("id");
                $('#DeliveryType').combobox('setValue', '');

                if (this.checked) {
                    var value = $(this).val();
                    ThrowGoodValue = value;
                    $("#ThrowGood").find('input[type=checkbox]').not(this).attr("checked", false);
                    if (value == 9) {
                        $("#TransportFee").numberbox('setValue', '0.00');
                        $("#TotalCharge").numberbox('setValue', '0.00');
                    } else if (value == 12) {
                        $('#ALogisID').combobox('setValue', 34);
                    } else if (value == 17) {
                        $('#DeliveryType').combobox('setValue', '0');
                    } else {
                        $("#TransportFee").numberbox('setValue', $("#hiddenTransportFee").val());
                        $("#TotalCharge").numberbox('setValue', $("#hiddenTotalCharge").val());
                    }
                } else {
                    $("#TransportFee").numberbox('setValue', $("#hiddenTransportFee").val());
                    $("#TotalCharge").numberbox('setValue', $("#hiddenTotalCharge").val());
                }
            });
        })
        $("body").on('click', '.RuleBankCheck', function () {
            var id = $(this).attr("id");
            var title = $(this).attr("value");
            if (this.checked) {
                $("#rule").find('input[type=checkbox]').not(this).attr("checked", false);
                UselRuleBank(id, title);
            } else {
                CancelRuleBank(id, title);
            }
        });

        function UselRuleBank(id, title) {
            var RuleType = $('#RuleType').val();
            var SystemSalePrice = $('#SystemSalePrice').val();

            var ThisRuleType = $('#RuleType' + id).val();
            switch (parseInt(ThisRuleType)) {
                case 0:
                    break;
                case 1:

                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 6:
                    var ThisRuleContent = $('#CutEntry' + id).val();
                    if (RuleType == null || RuleType == "") {
                        $('#RuleType').val(6);
                        $('#RuleID').val(id);
                        $('#RuleTitle').val(title);
                        var price = ($('#ActSalePrice').val() * 1 - ThisRuleContent);
                    } else {
                        if (RuleType.indexOf(',6') < 0) {
                            $('#RuleType').val(RuleType + ',' + 6);
                            $('#RuleID').val($('#RuleID').val() + ',' + id);
                            $('#RuleTitle').val($('#RuleTitle').val() + ',' + title);
                        } else {
                            $('#RuleID').val($('#HiddenLimitID').val() + ',' + id);
                            $('#RuleTitle').val($('#HiddenLimitTitle').val() + ',' + title);
                        }
                        var price = (SystemSalePrice * 1 - ThisRuleContent);
                    }
                    $('#ActSalePrice').numberbox('setValue', price);
                    $("#ActSalePrice").numberbox('disable');
                    break;
                default:
                    break;
            }
        }
        function CancelRuleBank(id, title) {
            var RuleType = $('#RuleType').val();

            var ThisRuleType = $('#RuleType' + id).val();
            switch (parseInt(ThisRuleType)) {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 6:
                    var ThisRuleContent = $('#CutEntry' + id).val();
                    if (RuleType.indexOf(",") < 0) {
                        $('#RuleType').val("");
                        $('#RuleID').val("");
                        $('#RuleTitle').val("");
                        var price = ($('#ActSalePrice').val() * 1 + ThisRuleContent * 1);
                    } else {
                        $('#RuleType').val($('#RuleType').val().replace(',6', ''));
                        $('#RuleID').val($('#RuleID').val().replace(',' + id, ''));
                        $('#RuleTitle').val($('#RuleTitle').val().replace(',' + title, ''));
                        var price = ($('#ActSalePrice').val() * 1 + ThisRuleContent * 1);
                    }
                    $('#ActSalePrice').numberbox('setValue', price);
                    $("#ActSalePrice").numberbox('enable');
                    break;
                default:
                    break;
            }
        }
        function reset() {
            // prePrint();
            $('#fmDep').form('clear');
            $('#dgSave').datagrid('loadData', { total: 0, rows: [] });
            $('#outDg').datagrid('loadData', { total: 0, rows: [] });
            $('#CreateAwb').textbox('setValue', '<%= Un%>');
            var d = new Date();
            $('#CreateDate').datetimebox('setValue', AllDateTime(d));

            $('#DisplayNum').val(0);
            $('#DisplayPiece').val(0);
            $('#ADep').textbox('setValue', '<%= UserInfor.DepCity%>');
            var title = "";
            $('#outDg').datagrid("getPanel").panel("setTitle", title);
            $('#dgSave').datagrid("getPanel").panel("setTitle", title);
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
            //var t = Number($('#TransitFee').numberbox('getValue')) + Number($('#TransportFee').numberbox('getValue')) + Number($('#DeliveryFee').numberbox('getValue')) + Number($('#OtherFee').numberbox('getValue')) + Number($('#InsuranceFee').numberbox('getValue'));
            var t = Number($('#TransitFee').numberbox('getValue')) + Number($('#TransportFee').numberbox('getValue')) + Number($('#OtherFee').numberbox('getValue')) - Number($('#InsuranceFee').combogrid('getText'));
            var hiddenTotalChargeVal = Number($('#TransitFee').numberbox('getValue')) + Number($('#hiddenTransportFee').val()) + Number($('#OtherFee').numberbox('getValue')) - Number($('#InsuranceFee').combogrid('getText'));
            $("#hiddenTotalCharge").val(Number(hiddenTotalChargeVal).toFixed(2));
            if (ThrowGoodValue != 9) {
                $('#TotalCharge').numberbox('setValue', Number(t).toFixed(2));
            }
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
        //业务编号赋值
        function onAcceptBusinessID(item) {
            $('#BusinessID').val(item.ID);
        }
        //收货人自动选择方法
        function onClientChanged(item) {
            $("#HiddenClientSelectName").val(item.Boss);
            if (item) {
                if (item.ClientType == "3") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '该客户是逾期客户，请确认收到货款再发货', 'warning');
                }
                $.ajax({
                    url: '../Client/clientApi.aspx?method=QueryCargoClientInfo&ClientNum=' + item.ClientNum,
                    dataType: "json",
                    success: function (text) {
                        if (text.Result == true) {
                            if (text.Message != 0) {
                                $('#ALogisID').combobox('setValue', text.Message);
                            }
                        }
                    }
                });
                $('#AcceptPeople').combobox({
                    valueField: 'ADID', textField: 'AcceptPeopleCellphone',
                    url: '../Client/clientApi.aspx?method=AutoCompleteClientAcceptPeople&ClientNum=' + item.ClientNum,
                    onLoadSuccess: function () {
                        //默认选中第一个
                        var array = $(this).combobox("getData");
                        for (var item in array[0]) {
                            if (item == "ADID") {
                                $(this).combobox('select', array[0][item]);
                            }
                        }
                    },
                    onSelect: onAcceptAddressChanged
                });
                $('#ABusinessID').combobox({
                    valueField: 'ID', textField: 'DepName',
                    url: '../Client/clientApi.aspx?method=AutoBusinessID&ClientID=' + item.ClientID,
                    onLoadSuccess: function () {
                        //默认选中第一个
                        var array = $(this).combobox("getData");
                        for (var item in array[0]) {
                            if (item == "ID") {
                                $(this).combobox('select', array[0][item]);
                            }
                        }
                    },
                    onSelect: onAcceptBusinessID
                });

                var AAcceptPeople = $('#AcceptPeople').combobox('getValue');
                var SAcceptPeople = $('#SAcceptPeople').combobox('getValue');
                $('#ClientType').val(item.ClientType);

                if (SAcceptPeople != AAcceptPeople) {
                    var outDgrows = $("#outDg").datagrid('getData').rows;
                    if (outDgrows.length > 0) {
                        for (var i = 0; i < outDgrows.length; i++) {
                            if (outDgrows[i].RuleType + "".indexOf('4') != "-1") {
                                var piece = parseInt(outDgrows[i].InPiece) - parseInt(outDgrows[i].Piece);
                                if (piece > 0) {
                                    $.ajax({
                                        url: "orderApi.aspx?method=QueryPriceRuleBankInfoToID&RuleID=" + outDgrows[i].RuleID + "&HouseID=" + outDgrows[i].HouseID + "&TypeID=" + outDgrows[i].TypeID + "&Specs=" + encodeURIComponent(outDgrows[i].Specs) + "&Figure=" + encodeURIComponent(outDgrows[i].Figure) + "&Batch=" + outDgrows[i].Batch + "&ClientNum=" + item.ClientNum + "&OrderNo=" + $('#OrderNo').val() + "&RuleType=4",
                                        cache: false,
                                        async: false,
                                        dataType: "json",
                                        success: function (text) {
                                            if (text.Result == true) {
                                                if (text.RuleContent < parseInt(outDgrows[i].InPiece) - parseInt(outDgrows[i].Piece)) {
                                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '所选收货人订单中第' + (parseInt(i) + 1) + '条货物限购数量为' + text.RuleContent + '，无法修改为此收货人！', 'warning');
                                                    $('#SAcceptPeople').combobox('setValue', $('#HiddenClientNum').val());
                                                    $('#SAcceptPeople').combobox('setText', $('#HiddenAcceptPeople').val());
                                                    $("#HiddenClientSelectName").val($('#HiddenAcceptPeople').val());
                                                    //$('#AcceptPeople').combobox('setValue', $('#HiddenClientNum').val());
                                                    //$('#AcceptPeople').combobox('setText', $('#HiddenAcceptPeople').val());
                                                    //$('#HiddenAAcceptPeople').val($('#HiddenAcceptPeople').val());
                                                    $('#SAcceptPeople').combobox('select', $('#HiddenClientNum').val());

                                                    $('#SAcceptPeople').combobox('setValue', $('#HiddenAcceptPeople').val());
                                                    //$('#AcceptPeople').combobox('setValue', $('#HiddenAcceptPeople').val());
                                                    return;
                                                } else {
                                                    $('#HiddenClientNum').val(item.ClientNum);
                                                    $('#HiddenAcceptPeople').val(item.Boss);
                                                    //$('#HiddenAAcceptPeople').val(item.Boss);

                                                    if (item.UserID != null && item.UserID != "") {
                                                        $('#SaleManID').combobox('setValue', item.UserID);
                                                        if (item.UserName != null && item.UserName != "") {
                                                            $('#SaleManName').val(item.UserName);
                                                        }
                                                        if (HID == 9 || HID == 44 || HID == 45) {
                                                            //$('#SaleManID').combobox('disable');
                                                        } else {
                                                            $('#SaleManID').combobox('enable');
                                                        }
                                                    } else {
                                                        $('#SaleManID').combobox('enable');
                                                        $('#SaleManID').combobox('setValue', '');
                                                    }
                                                    $('#SAcceptPeople').combobox('setValue', item.ClientNum);
                                                    $('#SAcceptPeople').combobox('setText', item.Boss);
                                                    //$('#AcceptPeople').combobox('setValue', item.ClientNum);
                                                    //$('#AcceptPeople').combobox('setText', item.Boss);
                                                    //$('#HiddenAAcceptPeople').val(item.Boss);
                                                    //$('#AAcceptUnit').textbox('setValue', item.ClientName);//ClientShortName
                                                    //$('#AAcceptAddress').textbox('setValue', item.Address);
                                                    //$('#AAcceptTelephone').textbox('setValue', item.Telephone);
                                                    //$('#AAcceptCellphone').textbox('setValue', item.Cellphone);
                                                    $('#ClientNum').val(item.ClientNum);
                                                    $('#ShopCode').val(item.ShopCode);
                                                    $('#HiddenProvince').val(item.Province);
                                                    $('#HiddenCity').val(item.City);
                                                    if (item.City != '') {
                                                        $('#ADest').combobox('setValue', item.City);
                                                    }
                                                    //客户姓名
                                                    $('#InsuranceFee').combogrid({
                                                        method: 'get', panelWidth: '150', idField: 'ID', textField: 'Money',
                                                        url: 'orderApi.aspx?method=QueryMyCoupon&ClientID=' + item.ClientID,
                                                        columns: [[{ field: 'Money', title: '券金额', width: '50px' }, { field: 'UseStatus', title: '使用状态', width: '50px', formatter: function (val, row, index) { if (val == "0") { return "未使用"; } } }]],
                                                        fitColumns: true, onSelect: function (rowIndex, rowData) {
                                                            var t = Number($('#TransitFee').numberbox('getValue')) + Number($('#TransportFee').numberbox('getValue')) + Number($('#OtherFee').numberbox('getValue')) - Number(rowData.Money);
                                                            var hiddenTotalChargeVal = Number($('#TransitFee').numberbox('getValue')) + Number($('#hiddenTransportFee').val()) + Number($('#OtherFee').numberbox('getValue')) - Number(rowData.Money);
                                                            if (ThrowGoodValue != 9) {
                                                                $('#TotalCharge').numberbox('setValue', Number(t).toFixed(2));
                                                            }
                                                            $("#hiddenTotalCharge").val(Number(hiddenTotalChargeVal).toFixed(2));
                                                            $('#Coupon').val(rowData.Money);
                                                        }
                                                    });
                                                }
                                            } else {
                                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.RuleContent + '！', 'warning');
                                                //$('#AcceptPeople').combobox('setValue', $('#HiddenAcceptPeople').val());
                                                return;
                                            }
                                        }
                                    });
                                }
                            } else {
                                $('#HiddenClientNum').val(item.ClientNum);
                                $('#HiddenAcceptPeople').val(item.Boss);
                                //$('#HiddenAAcceptPeople').val(item.Boss);

                                if (item.UserID != null && item.UserID != "") {
                                    $('#SaleManID').combobox('setValue', item.UserID);
                                    if (item.UserName != null && item.UserName != "") {
                                        $('#SaleManName').val(item.UserName);
                                    }
                                    if (HID == 9 || HID == 44 || HID == 45) {
                                        //$('#SaleManID').combobox('disable');
                                    } else {
                                        $('#SaleManID').combobox('enable');
                                    }
                                } else {
                                    $('#SaleManID').combobox('enable');
                                    $('#SaleManID').combobox('setValue', '');
                                }
                                $('#SAcceptPeople').combobox('setValue', item.ClientNum);
                                $('#SAcceptPeople').combobox('setText', item.Boss);
                                //$('#AcceptPeople').combobox('setValue', item.ClientNum);
                                //$('#AcceptPeople').combobox('setText', item.Boss);
                                //$('#HiddenAAcceptPeople').val(item.Boss);
                                //$('#AAcceptUnit').textbox('setValue', item.ClientName);//ClientShortName
                                //$('#AAcceptAddress').textbox('setValue', item.Address);
                                //$('#AAcceptTelephone').textbox('setValue', item.Telephone);
                                //$('#AAcceptCellphone').textbox('setValue', item.Cellphone);
                                $('#ClientNum').val(item.ClientNum);
                                $('#ShopCode').val(item.ShopCode);
                                $('#HiddenProvince').val(item.Province);
                                $('#HiddenCity').val(item.City);
                                if (item.City != '') {
                                    $('#ADest').combobox('setValue', item.City);
                                }
                                //客户姓名
                                $('#InsuranceFee').combogrid({
                                    method: 'get', panelWidth: '150', idField: 'ID', textField: 'Money',
                                    url: 'orderApi.aspx?method=QueryMyCoupon&ClientID=' + item.ClientID,
                                    columns: [[{ field: 'Money', title: '券金额', width: '50px' }, { field: 'UseStatus', title: '使用状态', width: '50px', formatter: function (val, row, index) { if (val == "0") { return "未使用"; } } }]],
                                    fitColumns: true, onSelect: function (rowIndex, rowData) {
                                        var t = Number($('#TransitFee').numberbox('getValue')) + Number($('#TransportFee').numberbox('getValue')) + Number($('#OtherFee').numberbox('getValue')) - Number(rowData.Money);
                                        var hiddenTotalChargeVal = Number($('#TransitFee').numberbox('getValue')) + Number($('#hiddenTransportFee').val()) + Number($('#OtherFee').numberbox('getValue')) - Number(rowData.Money);
                                        if (ThrowGoodValue != 9) {
                                            $('#TotalCharge').numberbox('setValue', Number(t).toFixed(2));
                                        }
                                        $("#hiddenTotalCharge").val(Number(hiddenTotalChargeVal).toFixed(2));
                                        $('#Coupon').val(rowData.Money);
                                    }
                                });
                            }
                        }
                    } else {
                        $('#HiddenClientNum').val(item.ClientNum);
                        $('#HiddenAcceptPeople').val(item.Boss);
                        //$('#HiddenAAcceptPeople').val(item.Boss);

                        if (item.UserID != null && item.UserID != "") {
                            $('#SaleManID').combobox('setValue', item.UserID);
                            if (item.UserName != null && item.UserName != "") {
                                $('#SaleManName').val(item.UserName);
                            }
                            if (HID == 9 || HID == 44 || HID == 45) {
                                //$('#SaleManID').combobox('disable');
                            } else {
                                $('#SaleManID').combobox('enable');
                            }
                        } else {
                            $('#SaleManID').combobox('enable');
                            $('#SaleManID').combobox('setValue', '');
                        }
                        $('#SAcceptPeople').combobox('setValue', item.ClientNum);
                        $('#SAcceptPeople').combobox('setText', item.Boss);
                        //$('#AcceptPeople').combobox('setValue', item.ClientNum);
                        //$('#AcceptPeople').combobox('setText', item.Boss);
                        $('#HiddenAAcceptPeople').val(item.Boss);
                        //$('#AAcceptUnit').textbox('setValue', item.ClientName);//ClientShortName
                        //$('#AAcceptAddress').textbox('setValue', item.Address);
                        //$('#AAcceptTelephone').textbox('setValue', item.Telephone);
                        //$('#AAcceptCellphone').textbox('setValue', item.Cellphone);
                        $('#ClientNum').val(item.ClientNum);
                        $('#ShopCode').val(item.ShopCode);
                        $('#HiddenProvince').val(item.Province);
                        $('#HiddenCity').val(item.City);
                        if (item.City != '') {
                            $('#ADest').combobox('setValue', item.City);
                        }
                        //客户姓名
                        $('#InsuranceFee').combogrid({
                            method: 'get', panelWidth: '150', idField: 'ID', textField: 'Money',
                            url: 'orderApi.aspx?method=QueryMyCoupon&ClientID=' + item.ClientID,
                            columns: [[{ field: 'Money', title: '券金额', width: '50px' }, { field: 'UseStatus', title: '使用状态', width: '50px', formatter: function (val, row, index) { if (val == "0") { return "未使用"; } } }]],
                            fitColumns: true, onSelect: function (rowIndex, rowData) {
                                var t = Number($('#TransitFee').numberbox('getValue')) + Number($('#TransportFee').numberbox('getValue')) + Number($('#OtherFee').numberbox('getValue')) - Number(rowData.Money);
                                var hiddenTotalChargeVal = Number($('#TransitFee').numberbox('getValue')) + Number($('#hiddenTransportFee').val()) + Number($('#OtherFee').numberbox('getValue')) - Number(rowData.Money);
                                if (ThrowGoodValue != 9) {
                                    $('#TotalCharge').numberbox('setValue', Number(t).toFixed(2));
                                }
                                $("#hiddenTotalCharge").val(Number(hiddenTotalChargeVal).toFixed(2));
                                $('#Coupon').val(rowData.Money);
                            }
                        });
                    }
                }
            }
            $('#AcceptPeople').combobox('textbox').bind('focus', function () { $('#AcceptPeople').combobox('showPanel'); });

            if (RoleCName.indexOf("安泰路斯") >= 0) {
                $('#ALogisID').combobox('setValue', 34);
                $("#SaleManID").combobox("readonly", true);
                $('#SaleManID').combobox('textbox').unbind('focus');
                $('#SaleManID').combobox('textbox').css('background-color', '#e8e8e8');
                $('#ADest').combobox('setValue', '西安');
            }
            else if (RoleCName.indexOf("汕头科矿") >= 0) {
                $('#ALogisID').combobox('setValue', 62);
                $('#SaleManID').combobox('setValue', '2940');
                $("#SaleManID").combobox("readonly", true);
                $('#SaleManID').combobox('textbox').css('background-color', '#e8e8e8');
            }
        }
        //保存订单
        function saveOutCargo() {
            //取消业务员禁用方便后台取值
            $('#SaleManID').combobox('enable');
            var rows = $('#dgSave').datagrid('getRows');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要保存的数据！', 'warning'); return; }
            <%--var tdt = 0;
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
            }--%>
            if ($('#Tran').is(':checked')) {
                //调货订单
                var th = $('#HID').combobox('getText');
                if (th == undefined || th == "") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要调货的仓库！', 'warning'); return;
                }
            }
<%--            if ($('#DaiFa').is(':checked')) {
                //代发订单
                var th = $('#HID').combobox('getText');
                if (th == undefined || th == "") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要代发的仓库！', 'warning'); return;
                }
            }--%>
            var saleMaanID = $('#SaleManID').combobox('getValue');
            var saleMaanName = $('#SaleManID').combobox('getText');
            if ((saleMaanID == "" || saleMaanID == undefined) && saleMaanName != "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '此业务员无效！', 'warning'); return;
            }
            else if (saleMaanID == "" || saleMaanID == undefined) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择业务员！', 'warning'); return;
            }

            HID = "<%=UserInfor.HouseID%>";
            $('#HouseCode').val(rows[0].OrderCode);
            $('#BelongHouse').val(rows[0].BelongHouse);

            $('#HouseID').val(rows[0].HouseID);

            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定保存？', function (r) {
                if (r) {
                    //启用复选框用于后台数据获取
                    $(".ThrowGood").prop("disabled", false);
                    $("#AAcceptPeople").textbox('enable');
                    $("#AAcceptUnit").textbox('enable');
                    $("#AAcceptAddress").textbox('enable');
                    $("#AAcceptTelephone").textbox('enable');
                    $("#AAcceptCellphone").textbox('enable');
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    $('#btnSave').linkbutton('disable');
                    var json = JSON.stringify(rows);

                    $('#fmDep').form('submit', {
                        url: 'orderApi.aspx?method=saveOrderData',
                        contentType: "application/json;charset=utf-8", dataType: "json",
                        onSubmit: function (param) {
                            param.AAcceptPeople = $('#AAcceptPeople').combobox('getText');
                            //param.PayClientName = $('#APayClientNum').combobox('getText');
                            param.ADest = $('#ADest').combobox('getText');
                            param.TranHouse = $('#HID').combobox('getText');
                            param.submitData = json;
                            var trd = $(this).form('enableValidation').form('validate');
                            if (trd) { $('#btnSave').linkbutton('disable'); } else {
                                $.messager.progress("close");
                                $('#btnSave').linkbutton('enable');
                            }
                            return trd;
                        },
                        success: function (msg) {
                            $.messager.progress("close");
                            $('#btnSave').linkbutton('enable');
                            var result = eval('(' + msg + ')');
                            if (result.Result) {
                                var dd = result.Message.split('/');
                                $('#ONum').val(dd[0]);
                                $('#OutNum').val(dd[1]);
                                if (dd[2] == "0") {
                                    if (RoleCName.indexOf("安泰路斯") >= 0) {
                                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功！', 'info');
                                        location.reload();
                                    }
                                    else if (RoleCName.indexOf("汕头科矿") >= 0) {
                                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功！', 'info');
                                        location.reload();
                                    }
                                    else {
                                        $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功，是否打印拣货单？', function (r) {
                                            if (r) { prePrint(); }
                                            else { location.reload(); }
                                        });
                                    }
                                } else {
                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单改价申请审批中，请等待！', 'info');
                                    location.reload();
                                }
                            } else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                                //$('#SaleManID').combobox('disable');
                            }
                        }
                    })
                }
            });
        }
        //删除出库的数据
        function DelItem() {
            var rows = $('#outDg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            var copyRows = [];
            var tCharge = $('#TransportFee').numberbox('getValue') == null || $('#TransportFee').numberbox('getValue') == "" ? 0 : Number($('#TransportFee').numberbox('getValue'));
            var hiddenTransportFeeVal = $('#hiddenTransportFee').val() == null || $('#hiddenTransportFee').val() == "" ? 0 : Number($('#hiddenTransportFee').val());
            for (var i = 0, l = rows.length; i < l; i++) {
                var row = rows[i];
                copyRows.push(row);
                var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
                var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
                n--;
                var pt = p - (Number(row.InPiece) - Number(row.Piece));
                $('#DisplayNum').val(Number(n));
                $('#DisplayPiece').val(Number(pt));
                $('#APiece').numberbox('setValue', Number(pt));
                var SalePrice = Number(row.SalePrice);//销售价
                var NC = SalePrice * (Number(row.InPiece) - Number(row.Piece));
                $("#hiddenTransportFee").val(hiddenTransportFeeVal - NC);
                if (ThrowGoodValue != 9) {
                    $('#TransportFee').numberbox('setValue', tCharge - NC);
                }
                var title = "上订单     已拉上：" + n + "票，总数量：" + pt + " 条";
                $('#outDg').datagrid("getPanel").panel("setTitle", title);
                $('#dgSave').datagrid("getPanel").panel("setTitle", title);

                var index = $('#dg').datagrid('getRowIndex', copyRows[i].ID);
                if (index >= 0) {

                    var Trow = $("#dg").datagrid('getData').rows[index];
                    Trow.Piece = Trow.InPiece;
                    $('#dg').datagrid('updateRow', { index: index, row: Trow });
                }
            }
            for (var i = 0; i < copyRows.length; i++) {
                var index = $('#outDg').datagrid('getRowIndex', copyRows[i]);
                $('#outDg').datagrid('deleteRow', index);
                $('#dgSave').datagrid('deleteRow', index);
            }
        }
        var ISM = false;
        //新增出库数据
        function outOK() {
            $("#ActSalePrice").numberbox('enable');
            var SalePrice = $('#ActSalePrice').numberbox('getValue');// Number(row.SalePrice);//销售价
            var SaleNum = $('#Numbers').numberbox('getValue');//销售数量
            var RuleType = $('#RuleType').val();
            $('#dg').datagrid('selectRow', $('#index').val());
            var row = $('#dg').datagrid('getSelected');
            var instock = true; var curStock = 0; var minStock = 0;
            //对轮胎产品进行价格管控
            if (row.TypeParentID == 1) {
                var sp = "<%=UserInfor.SpecialCreateAwb%>";
                if (sp == "0" || sp == '' || sp == undefined) {
                    //没有特殊下单权限，需要验证先进先出
                    var rows = $('#dg').datagrid('getRows');
                    for (var i = 0; i < rows.length; i++) {
                        var rw = rows[i];
                        if (rw.Specs == row.Specs && rw.Figure == row.Figure && rw.Model == row.Model && rw.BatchYear == row.BatchYear && rw.Source == row.Source) {
                            if (row.BatchWeek > rw.BatchWeek && rw.Piece > 0) {
                                if (row.Source == "马牌OES") {
                                    if (rw.Source == row.Source) {
                                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请优先出库周期早的轮胎！', 'warning');
                                        return;
                                    }
                                } else {
                                    if (rw.Source != "马牌OES") {
                                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请优先出库周期早的轮胎！', 'warning');
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
                if (SalePrice == "0" || SalePrice == '' || SalePrice == undefined) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请输入业务员销售价格！', 'warning');
                    return;
                }
                var thgd = $("input[name='ThrowGood']:checked").val();
                if (row.BelongHouse == "6" && row.UpClientID != 1 && row.HouseID != 65 && thgd != 25) {
                    //云仓，非狄乐汽服的库存 狄乐汽服仓库除外
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '不允许开非公司供应商库存', 'warning');
                    return;
                }
                if (ThrowGoodValue != 25 && row.BelongHouse == "6" && row.ProductCode != "") {
                    //退仓单之外的订单，云仓仓库 根据产品编码查询该仓库里的所有库存和最小库存，开单数量
                    $.ajax({
                        url: "../House/houseApi.aspx?method=QueryStockDataByProductCode&HouseID=" + row.HouseID + "&ProductCode=" + encodeURIComponent(row.ProductCode),
                        cache: false, async: false, dataType: "json",
                        success: function (text) {
                            if (text.MinStock != 0) {
                                var snum = 0;
                                var gdat = $('#dgSave').datagrid('getRows');
                                for (var k = 0; k < gdat.length; k++) {
                                    if (row.ProductCode == gdat[k].ProductCode) {
                                        var p = Number(gdat[k].InPiece) - Number(gdat[k].Piece);
                                        snum += p;
                                    }
                                }

                                if (Number(text.CurNum - Number(SaleNum) - snum) < Number(text.MinStock)) {
                                    instock = false;
                                    curStock = text.CurNum; minStock = text.MinStock;
                                }
                            }

                        }
                    });
                }

                if (RoleCName.indexOf("安泰路斯") < 0) {
                    if (RuleType.indexOf("0") < 0 && RuleType.indexOf("2") < 0 && RuleType.indexOf("6") < 0) {
                        if (Number(row.SalePrice) * 1.05 < Number(SalePrice)) {
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '业务员价高于系统销售价5%', 'warning');
                        }
                        if (Number(row.SalePrice) * 0.9 >= Number(SalePrice)) {
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '业务员价低于系统销售价10%', 'warning');
                        }
                        var IsModifyPrice = "<%=UserInfor.IsModifyPrice%>";
                        if (IsModifyPrice == undefined || IsModifyPrice == "0") {
                            if (ThrowGoodValue != 17) {
                                if (Number(SalePrice) < Number(row.SalePrice)) {
                                    //$.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '业务员价格不能低于销售价格！', 'warning');
                                    //return;
                                    ISM = true;
                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '业务员价格低于销售价格，保存后将提交改价申请审批！', 'warning');
                                }
                            }
                        }
                    }
                }
            }
            if (!instock) {

                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '剩余库存不能小于最小库存！' + minStock + '条', 'warning');

                return;
            }
            if ($('#Numbers').val() == null || $('#Numbers').val() == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请输入拉上订单数量！', 'warning');
                return;
            }
            if ($('#Numbers').val() < 1) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '拉上订单数量必须大于0！', 'warning');
                return;
            }

            var Total = Number(row.Piece);
            var tCharge = $('#TransportFee').numberbox('getValue') == null || $('#TransportFee').numberbox('getValue') == "" ? 0 : Number($('#TransportFee').numberbox('getValue'));
            var hiddenTransportFeeVal = $('#hiddenTransportFee').val() == null || $('#hiddenTransportFee').val() == "" ? 0 : Number($('#hiddenTransportFee').val());
            var Aindex = $('#InIndex').val();
            var index = $('#outDg').datagrid('getRowIndex', row.ID);
            var indexD = $('#dgSave').datagrid('getRowIndex', row.ID);
            if (index < 0) {
                if (RuleType != "" && RuleType != null) {
                    row.IsRuleBank = 1;
                    row.RuleType = RuleType;
                    row.RuleID = $('#RuleID').val();
                    row.RuleTitle = $('#RuleTitle').val();

                    if (RuleType.indexOf('1') > -1) {

                    }
                } else {
                    row.IsRuleBank = "";
                    row.RuleType = "";
                    row.RuleID = "";
                    row.RuleTitle = "";
                }
                row.Piece = $('#Numbers').numberbox('getValue');
                row.ActSalePrice = SalePrice;
                $('#outDg').datagrid('appendRow', row);
                $('#dgSave').datagrid('appendRow', row);
                var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
                var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
                n++;
                var pt = p + Number($('#Numbers').numberbox('getValue'));
                var fullDiscountCut = 0;
                if (getCookie("FullDiscountFull") > 0) {
                    var ptNum = (parseInt(Number($('#Numbers').numberbox('getValue'))) + parseInt(getCookie("FullDiscountSum"))) / getCookie("FullDiscountFull");
                    var ptList = (ptNum + "").split(".");
                    if (ptList.length > 1) {
                        fullDiscountCut = ptList[0] * parseInt(getCookie("FullDiscountCut"));
                    } else {
                        fullDiscountCut = ptList[0] * parseInt(getCookie("FullDiscountCut"));
                    }
                }
                if (Number($('#Numbers').numberbox('getValue')) + fullDiscountCut <= Total) {

                } else {

                }
                $('#DisplayNum').val(Number(n));
                $('#DisplayPiece').val(Number(pt));
                $('#APiece').numberbox('setValue', Number(pt));
                var NC = SalePrice * Number($('#Numbers').numberbox('getValue'));
                $("#hiddenTransportFee").val(hiddenTransportFeeVal + NC);
                if (ThrowGoodValue != 9) {
                    $('#TransportFee').numberbox('setValue', tCharge + NC);
                }
                var title = "上订单     已拉上：" + n + "票，总数量：" + pt + " 条";
                $('#outDg').datagrid("getPanel").panel("setTitle", title);
                $('#dgSave').datagrid("getPanel").panel("setTitle", title);
                closedgShowData();

                if (Total > Number($('#Numbers').numberbox('getValue')) + fullDiscountCut) {
                    row.Piece = Total - Number($('#Numbers').numberbox('getValue'));
                } else {
                    row.Piece = 0;
                }

                //if (Total != Number($('#Numbers').numberbox('getValue'))) {
                //    row.Piece = Total - Number($('#Numbers').numberbox('getValue'));
                //} else {
                //    row.Piece = 0;
                //}
                $('#dg').datagrid('updateRow', { index: Aindex, row: row });
            } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单列表已存在该产品，请先删除再添加！', 'warning'); }
        }
        function getCookie(cname) {
            var name = cname + "=";
            var cookie = document.cookie.split(';');
            for (var i = 0; i < cookie.length; i++) {
                var c = cookie[i].trim();
                if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
            }
            return "";
        }
        //关闭
        function closedgShowData() {
            $('#dlg').dialog('close');
        }

        //删除出库的数据
        function dblClickDelCargo(Did) {
            var row = $("#outDg").datagrid('getData').rows[Did];
            var tCharge = $('#TransportFee').numberbox('getValue') == null || $('#TransportFee').numberbox('getValue') == "" ? 0 : Number($('#TransportFee').numberbox('getValue'));
            var hiddenTransportFeeVal = $('#hiddenTransportFee').val() == null || $('#hiddenTransportFee').val() == "" ? 0 : Number($('#hiddenTransportFee').val());
            var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
            var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
            n--;
            var pt = p - (Number(row.InPiece) - Number(row.Piece));
            $('#DisplayNum').val(Number(n));
            $('#DisplayPiece').val(Number(pt));
            $('#APiece').numberbox('setValue', Number(pt));
            var SalePrice = Number(row.SalePrice);//销售价
            var NC = SalePrice * (Number(row.InPiece) - Number(row.Piece));
            $("#hiddenTransportFee").val(hiddenTransportFeeVal - NC);
            if (ThrowGoodValue != 9) {
                $('#TransportFee').numberbox('setValue', tCharge - NC);
            }
            var title = "上订单     已拉上：" + n + "票，总数量：" + pt + " 条";
            $('#outDg').datagrid("getPanel").panel("setTitle", title);
            $('#dgSave').datagrid("getPanel").panel("setTitle", title);

            var index = $('#dg').datagrid('getRowIndex', row.ID);
            if (index >= 0) {
                var Trow = $("#dg").datagrid('getData').rows[index];
                Trow.Piece = Trow.InPiece;
                $('#dg').datagrid('updateRow', { index: index, row: Trow });
            }
            var index = $('#outDg').datagrid('getRowIndex', row);
            $('#outDg').datagrid('deleteRow', index);
            $('#dgSave').datagrid('deleteRow', index);
        }
        ///出库
        function plOutCargo() {
            $("#ActSalePrice").numberbox('enable');
            $('#RuleType').val("");
            $('#RuleID').val("");
            $('#RuleTitle').val("");
            $('#index').val("");
            var row = $('#dg').datagrid('getSelected');
            $('#index').val($('#dg').datagrid('getRowIndex', row.ID));
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要拉上订单的数据！', 'warning');
                return;
            }
            if (row.Piece == 0) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '在库数量为0', 'warning');
                return;
            }
            if (Number(row.TypeParentID) == 1 && Number(row.TradePrice) <= 0 && Number(row.SalePrice) <= 0) {
                if (RoleCName.indexOf("安泰路斯") < 0) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请先录入销售价格', 'warning');
                    return;
                }
            }
            var thgd = $("input[name='ThrowGood']:checked").val();
            if (row.BelongHouse == "6" && row.UpClientID != 1 && row.HouseID != 65 && thgd != 25) {
                //云仓，非狄乐汽服的库存 狄乐汽服仓库除外
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '不允许开非公司供应商库存', 'warning');
                return;
            }
            if (row.TypeParentID == 1) {
                var sp = "<%=UserInfor.SpecialCreateAwb%>";
                if (sp == "0" || sp == '' || sp == undefined) {
                    //没有特殊下单权限，需要验证先进先出
                    var rows = $('#dg').datagrid('getRows');
                    for (var i = 0; i < rows.length; i++) {
                        var rw = rows[i];
                        if (rw.Specs == row.Specs && rw.Figure == row.Figure && rw.Model == row.Model && rw.BatchYear == row.BatchYear && rw.Source == row.Source) {
                            if (row.BatchWeek > rw.BatchWeek && rw.Piece > 0) {
                                if (row.Source == "马牌OES") {
                                    if (rw.Source == row.Source) {
                                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请优先出库周期早的轮胎！', 'warning');
                                        return;
                                    }
                                } else {
                                    if (rw.Source != "马牌OES") {
                                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请优先出库周期早的轮胎！', 'warning');
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (row) {
                var clientNum = $('#SAcceptPeople').combobox('getValue');
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
                                    $('#dlg').dialog('close');
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
                                    $('#dlg').dialog('setTitle', '拉上  ' + row.ProductName + ' 型号：' + row.Model + ' 不得超过：' + quotaNmu + '条');
                                }
                            }
                        }
                        var FullDiscountFull = $('#FullDiscountFull').val();
                        if (FullDiscountFull != undefined) {
                            document.cookie = "FullDiscountFull=; expires=Thu, 01 Jan 1970 00:00:00 GMT";
                            document.cookie = "FullDiscountCut=; expires=Thu, 01 Jan 1970 00:00:00 GMT";
                            document.cookie = "FullDiscountSum=; expires=Thu, 01 Jan 1970 00:00:00 GMT";
                            var RuleID = $('#FullDiscountID').val();
                            var RuleTitle = $('#FullDiscountTitle').val();
                            var RuleType = $('#RuleType').val();
                            var ThisRuleContent = $('#FullDiscountCut').val();
                            document.cookie = "FullDiscountFull=" + FullDiscountFull;
                            document.cookie = "FullDiscountCut=" + ThisRuleContent;
                            document.cookie = "FullDiscountSum=" + text.FullDiscountSum;
                            if (RuleType == null || RuleType == "") {
                                $('#RuleType').val(1);
                                $('#RuleID').val(RuleID);
                                $('#RuleTitle').val(RuleTitle);
                                //var price = ($('#ActSalePrice').val() * 1 - ThisRuleContent);
                            } else {
                                if (RuleType.indexOf(',1') < 0) {
                                    $('#RuleType').val(RuleType + ',' + 1);
                                    $('#RuleID').val($('#RuleID').val() + ',' + RuleID);
                                    $('#RuleTitle').val($('#RuleTitle').val() + ',' + RuleTitle);
                                } else {
                                    $('#RuleID').val($('#HiddenLimitID').val() + ',' + RuleID);
                                    $('#RuleTitle').val($('#HiddenLimitTitle').val() + ',' + RuleTitle);
                                }
                                //var price = (SystemSalePrice * 1 - ThisRuleContent);
                            }
                        }
                    }
                });
                $('#dlg').dialog('open').dialog('setTitle', '拉上  ' + row.ProductName + ' 型号：' + row.Model + ' 不得超过：' + row.Piece + '条');
                $('#InPiece').val(row.Piece);
                $('#InIndex').val($('#dg').datagrid('getRowIndex', row));
                $('#Numbers').numberbox('setValue', '');
                $('#ActSalePrice').numberbox('setValue', row.TradePrice);
                $('#SystemSalePrice').numberbox('setValue', row.TradePrice);

                $('#Numbers').numberbox({
                    min: 0,
                    max: row.Piece,
                    precision: 0
                });
                $('#Numbers').numberbox().next('span').find('input').focus();

            }
        }
        var LODOP;
        //订单打印
        function prePrint() {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            var nowdate = new Date();
            LODOP.SET_PRINT_PAGESIZE(0, 2100, 2970, "A4");
            var griddata = $('#dgSave').datagrid('getRows');
            var hous = '<%= PickTitle%>';
            var HouseName = '<%= HouseName%>';
            if (HouseName.indexOf('广东') != -1) {
                hous = griddata[0].FirstAreaName + "拣货单";
            }

            //var com = '湖南省迪乐泰仓库拣货单';
            //if (hous.indexOf('武汉') != -1) {
            //    com = '湖北省迪乐泰仓库拣货单';
            //}
            //else if (hous.indexOf('西安迪乐泰') != -1) {
            //    com = '西安迪乐泰仓库拣货单';
            //}
            LODOP.ADD_PRINT_TEXT(4, 253, 485, 33, hous);
            LODOP.SET_PRINT_STYLEA(0, "FontName", "新宋体");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 19);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_IMAGE(-3, 47, 198, 49, "<img src=\"../CSS/image/dlqf.jpg\"/>");
            LODOP.SET_PRINT_STYLEA(0, "Stretch", 1);

            LODOP.ADD_PRINT_TEXT(41, 120, 70, 20, "订单号：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(41, 180, 110, 20, $('#ONum').val());
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

            LODOP.ADD_PRINT_TEXT(41, 540, 105, 20, "收货人：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(41, 595, 100, 20, $('#AcceptPeople').combobox('getText'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            var tell = $('#AAcceptCellphone').textbox('getValue');
            if (tell == '' || tell == null) { tell = $('#AAcceptTelephone').textbox('getValue'); }
            LODOP.ADD_PRINT_TEXT(41, 638, 117, 20, tell);
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

            LODOP.ADD_PRINT_RECT(66, 3, 100, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 102, 80, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 181, 100, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 280, 100, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 379, 65, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 443, 70, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 512, 75, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 586, 96, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 681, 107, 25, 0, 1);

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
            LODOP.ADD_PRINT_TEXT(70, 448, 64, 25, "出库数量");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            //LODOP.ADD_PRINT_TEXT(70, 520, 78, 25, "货位代码");
            //LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            //LODOP.ADD_PRINT_TEXT(70, 604, 72, 25, "所在区域");
            //LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            //LODOP.ADD_PRINT_TEXT(70, 690, 78, 25, "所在仓库");
            //LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

            LODOP.ADD_PRINT_TEXT(70, 522, 60, 25, "批次");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(70, 590, 88, 25, "货位代码");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(70, 684, 72, 25, "所在区域");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);


            LODOP.ADD_PRINT_TEXT(41, 280, 90, 20, "到达站：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(41, 340, 300, 20, $('#ADest').combobox('getText') + " 物流：" + $('#ALogisID').combobox('getText'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            var js = 0, Alltotal = 0, AllPiece = 0;
            for (var i = 0; i < griddata.length; i++) {
                js++;
                var p = Number(griddata[i].InPiece) - Number(griddata[i].Piece);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 3, 100, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 102, 80, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 181, 100, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 280, 100, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 379, 65, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 443, 70, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 512, 75, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 586, 96, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 681, 107, 25, 0, 1);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 5, 111, 23, griddata[i].ProductName);//产品名称
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 105, 80, 20, griddata[i].Model);//型号
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 185, 94, 20, griddata[i].Specs);//规格 
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 286, 110, 20, griddata[i].Figure);//花纹
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 386, 82, 20, griddata[i].LoadIndex + griddata[i].SpeedLevel);//速度级别
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 450, 51, 20, p);//数量出库数量
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

                //LODOP.ADD_PRINT_TEXT(95 + i * 25, 520, 106, 20, griddata[i].ContainerCode);//货位代码
                //LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                //LODOP.ADD_PRINT_TEXT(95 + i * 25, 602, 69, 20, griddata[i].AreaName);//所在区域
                //LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                //LODOP.ADD_PRINT_TEXT(95 + i * 25, 688, 121, 20, griddata[i].HouseName);//所在仓库
                //LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

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
            location.reload();

        }
    </script>

</asp:Content>
