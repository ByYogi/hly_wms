<%@ Page Title="每日出入库报表" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reportDaily.aspx.cs" Inherits="Cargo.Report.reportDaily" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            $.ajaxSetup({ async: true });
            $.getJSON("../Client/clientApi.aspx?method=QueryAllUpClientDep", function (data) {
                UpClientDep = data;
            });
            var url = '../Client/clientApi.aspx?method=QueryAllUpClientDep&type=1&houseID=<%=UserInfor.HouseID%>';
            $('#BelongDepart').combobox('reload', url);
            $('#BelongDepart').combobox('select', '-1');
            var HID = "<%=UserInfor.HouseID%>";
            //如果是广州仓库显示公司类型选项
            if (HID == "9") {
                $("[name='BelongHouse']").show();
                $("#BelongHouseLab").html("&nbsp;&nbsp;所属公司:");
                $('#BelongHouse').next(".combo").show();
                $('#BelongHouse').combobox('setValue', 0);
            } else {
                $('#BelongHouse').combobox('setValue', -1);
                $("[name='BelongHouse']").hide();
                $('#BelongHouse').next(".combo").hide();
            }
        }
        $(window).resize(function () {
            adjustment();
        });
        function adjustment() {
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - $("div[name='SelectDiv2']").outerHeight(true);
            $('#dgIn').datagrid({ height: height });
            $('#dgOut').datagrid({ height: height });
        }
        $(document).ready(function () {
            var columns = [];
            var HID = '<%=UserInfor.HouseID%>';
            if (HID == "47") {
                $("#openHub").hide();
                $("#reportMoveContainer").hide();
                $("#saleManReport").hide();
            }
            $('#<%=hiddenHouseID.ClientID%>').val(HID);
            columns.push({ title: '入库时间', field: 'InHouseTime', width: '80px', formatter: DateFormatter });
            columns.push({
                title: '入库单号', field: 'InCargoID', width: '80px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            columns.push({
                title: '入库数量', field: 'Piece', width: '60px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            if (HID != "64") {
                columns.push({
                    title: '产品编码', field: 'ProductCode', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '产品名称', field: 'ProductName', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
            }
            if (HID == "47") {
                columns.push({
                    title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '品牌', field: 'TypeName', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '富添盛编码', field: 'GoodsCode', width: '110px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '商贸编码', field: 'Model', width: '110px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '长和编码', field: 'Figure', width: '110px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '祺航编码', field: 'Size', width: '110px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '批次', field: 'Batch', width: '45px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '成本价', field: 'TradePrice', width: '50px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '入库类型', field: 'InHouseType', width: '100px',
                    formatter: function (val, row, index) {
                        if (val == "0") { return "<span title='正常进货入库'>正常进货入库</span>"; }
                        else if (val == "1") { return "<span title='退货入库'>退货入库</span>"; }
                        else if (val == "2") { return "<span title='订单删除入库'>订单删除入库</span>"; }
                        else if (val == "3") { return "<span title='拉下订单入库'>拉下订单入库</span>"; }
                        else if (val == "4") { return "<span title='复盘入库'>复盘入库</span>"; }
                        else if (val == "5") { return "<span title='移库入库'>移库入库</span>"; }
                        else { return "<span title='正常进货入库'>正常进货入库</span>"; }
                    }
                });
            } else if (HID == "62") {
                $("#ASpecsTd").html("背番:");
                $('#ASpecs').textbox({ prompt: '请输入背番' });
                $("td.BelongDepart").hide();
                $("td.AModelTd").hide();
                $("td.AFigure").hide();
                $("#saleManReport").hide();
                $("#reportMoveContainer").hide();
                $("#openHub").hide();

                columns.push({
                    title: '品番', field: 'GoodsCode', width: '100px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '背番', field: 'Specs', width: '70px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '批次', field: 'Batch', width: '45px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '产品来源', field: 'Source', width: '100px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
            }
            else {
                columns.push({
                    title: '品牌', field: 'TypeName', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '货品代码', field: 'GoodsCode', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '型号', field: 'Model', width: '60px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '花纹', field: 'Figure', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '载速', field: 'LoadIndex', width: '45px', formatter: function (value, row) {
                        if (value != null && value != "") {
                            return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                        }
                    }
                });
                //columns.push({
                //    title: '速度级别', field: 'SpeedLevel', width: '60px', formatter: function (value) {
                //        if (value != null && value != "") {
                //            return "<span title='" + value + "'>" + value + "</span>";
                //        }
                //    }
                //});


                columns.push({
                    title: '批次', field: 'Batch', width: '45px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '单价', field: 'UnitPrice', width: '40px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '销售价', field: 'SalePrice', width: '40px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                if (HID == "64") {
                    if (<%=UserInfor.LoginName%>== 2421 || <%=UserInfor.LoginName%>== 2422 || <%=UserInfor.LoginName%>== 2076) {
                        columns.push({
                            title: '供应商', field: 'Supplier', width: '100px', formatter: function (value) {
                                if (value != null && value != "") {
                                    return "<span title='" + value + "'>" + value + "</span>";
                                }
                            }
                        });
                    }
                } else {
                    columns.push({
                        title: '供应商', field: 'Supplier', width: '100px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '产品来源', field: 'Source', width: '100px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
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
                            else if (val == "4") { return "<span title='极速达'>极速达</span>"; }
                            else if (val == "5") { return "<span title='次日达'>次日达</span>"; }
                            else { return ""; }
                        }
                    });
                }
                columns.push({
                    title: '入库类型', field: 'InHouseType', width: '100px',
                    formatter: function (val, row, index) {
                        if (val == "0") { return "<span title='正常进货入库'>正常进货入库</span>"; }
                        else if (val == "1") { return "<span title='退货入库'>退货入库</span>"; }
                        else if (val == "2") { return "<span title='订单删除入库'>订单删除入库</span>"; }
                        else if (val == "3") { return "<span title='拉下订单入库'>拉下订单入库</span>"; }
                        else if (val == "4") { return "<span title='复盘入库'>复盘入库</span>"; }
                        else if (val == "5") { return "<span title='移库入库'>移库入库</span>"; }
                        else { return "<span title='正常进货入库'>正常进货入库</span>"; }
                    }
                });
            }
            columns.push({
                title: '入库货位', field: 'ContainerCode', width: '90px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            columns.push({
                title: '入库区域', field: 'AreaName', width: '60px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            columns.push({
                title: '入库仓库', field: 'FirstAreaName', width: '100px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            columns.push({
                title: '来货单号', field: 'SourceOrderNo', width: '80px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            columns.push({
                title: '产品ID', field: 'ProductID', width: '50px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            $('#dgIn').datagrid({
                width: '100%',
                //height: '480px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: false, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                rownumbers: true,
                showFooter: true,
                toolbar: '',
                columns: [columns]
            });
            columns = [];
            columns.push({
                title: '出库仓库', field: 'FirstAreaName', width: '80px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            columns.push({ title: '出库时间', field: 'OutHouseTime', width: '80px', formatter: DateFormatter });
            if (HID == "47") {
                columns.push({
                    title: '出库数量', field: 'Piece', width: '60px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '客户名称', field: 'AcceptUnit', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '产品编码', field: 'ProductCode', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '产品名称', field: 'ProductName', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '品牌', field: 'TypeName', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '富添盛编码', field: 'GoodsCode', width: '110px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '商贸编码', field: 'Model', width: '110px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '长和编码', field: 'Figure', width: '110px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '祺航编码', field: 'Size', width: '110px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '批次', field: 'Batch', width: '45px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '成本价', field: 'CostPriceStore', width: '50px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '入手价', field: 'SalePriceClient', width: '50px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '对店价', field: 'CostPriceStore', width: '50px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
            }
            else if (HID == "62") {
                $("#ASpecsTd").html("背番:");
                $('#ASpecs').textbox({ prompt: '请输入背番' });
                $("td.BelongDepart").hide();
                $("td.AModelTd").hide();
                $("td.AFigure").hide();

                columns.push({
                    title: '出库单号', field: 'OutCargoID', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '出库数量', field: 'Piece', width: '60px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '品牌', field: 'TypeName', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '品番', field: 'GoodsCode', width: '100px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '背番', field: 'Specs', width: '70px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '批次', field: 'Batch', width: '45px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '产品来源', field: 'Source', width: '100px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });

            } else {
                columns.push({
                    title: '出库单号', field: 'OutCargoID', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });

                columns.push({
                    title: '出库数量', field: 'Piece', width: '60px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '品牌', field: 'TypeName', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '产品编码', field: 'ProductCode', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                if (HID != "64") {
                    columns.push({
                        title: '产品名称', field: 'ProductName', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                }
              
                if (HID != "64") {
                    columns.push({
                        title: '型号', field: 'Model', width: '60px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                }
                columns.push({
                    title: '货品代码', field: 'GoodsCode', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '花纹', field: 'Figure', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '载速', field: 'LoadIndex', width: '45px', formatter: function (value, row) {
                        if (value != null && value != "") {
                            return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                        }
                    }
                });
                //columns.push({
                //    title: '速度级别', field: 'SpeedLevel', width: '60px', formatter: function (value) {
                //        if (value != null && value != "") {
                //            return "<span title='" + value + "'>" + value + "</span>";
                //        }
                //    }
                //});
                columns.push({
                    title: '批次', field: 'Batch', width: '60px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '销售价', field: 'SalePrice', width: '50px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                if (HID == "64") {
                    if (<%=UserInfor.LoginName%>== 2421 || <%=UserInfor.LoginName%>== 2422 || <%=UserInfor.LoginName%>== 2076) {
                        columns.push({
                            title: '供应商', field: 'Supplier', width: '100px', formatter: function (value) {
                                if (value != null && value != "") {
                                    return "<span title='" + value + "'>" + value + "</span>";
                                }
                            }
                        });
                    }
                } else {
                    columns.push({
                        title: '供应商', field: 'Supplier', width: '100px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '产品来源', field: 'Source', width: '100px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
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
                            else if (val == "4") { return "<span title='极速达'>极速达</span>"; }
                            else if (val == "5") { return "<span title='次日达'>次日达</span>"; }
                            else { return ""; }
                        }
                    });
                }
            }
            columns.push({
                title: '客户名称', field: 'AcceptUnit', width: '100px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            columns.push({
                title: '订单号', field: 'OrderNo', width: '90px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            //columns.push({
            //    title: '订单类型', field: 'OrderModel', width: '60px',
            //    formatter: function (val, row, index) {
            //        if (val == 0) { return "销售单"; }
            //        else if (val == 1) { return "<span title='退货单'>退货单</span>"; }
            //    }
            //});
            columns.push({
                title: '订单状态', field: 'AwbStatus', width: '60px',
                formatter: function (val, row, index) {
                    if (val == "0") { return "已下单"; }
                    else if (val == "1") { return "<span title='正在备货'>正在备货</span>"; }
                    else if (val == "2") { return "<span title='已出库'>已出库</span>"; }
                    else if (val == "3") { return "<span title='运输在途'>运输在途</span>"; }
                    else if (val == "4") { return "<span title='已到达'>已到达</span>"; }
                    else if (val == "5") { return "<span title='已签收'>已签收</span>"; }
                    else if (val == "6") { return "<span title='已拣货'>已拣货</span>"; }
                    else { return ""; }
                }
            });
            columns.push({
                title: '出库货位', field: 'ContainerCode', width: '80px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            columns.push({
                title: '出库区域', field: 'AreaName', width: '60px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
           
            columns.push({
                title: '产品ID', field: 'ProductID', width: '50px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            $('#dgOut').datagrid({
                width: '100%',
                //height: '480px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: false, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OutCargoID',
                rownumbers: true,
                showFooter: true,
                toolbar: '',
                columns: [columns],
                rowStyler: function (index, row) {
                    if (row.OrderModel == "1") { return "background-color:#e5e6e5"; };
                }
            });
            columns = [];
            columns.push({ title: '所属仓库', field: 'FirstAreaName' });
            columns.push({ title: '品牌', field: 'TypeName' });
            columns.push({ title: '规格', field: 'Specs' });
            columns.push({ title: '花纹', field: 'Figure' });
            columns.push({ title: '载速', field: 'LoadIndex', formatter: function (value, row) { return value + row.SpeedLevel; } });
            columns.push({ title: '货品代码', field: 'GoodsCode' });
            columns.push({ title: '出库数量', field: 'Piece', align: 'right' });
            //columns.push({ title: '移库数量', field: 'MoveNum', align: 'right' });
            columns.push({ title: '库存数量', field: 'FlatRatio', align: 'right' });
            columns.push({ title: '安全库存', field: 'Numbers', align: 'right' });
            columns.push({ title: '补货数量', field: 'TreadWidth', align: 'right' });
            $('#dgStock').datagrid({
                width: '100%',
                //height: '480px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: false, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'Specs',
                rownumbers: true,
                showFooter: true,
                toolbar: '',
                columns: [columns],
            });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getNowFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#StartDate').datebox('textbox').bind('focus', function () { $('#StartDate').datebox('showPanel'); });
            $('#EndDate').datebox('textbox').bind('focus', function () { $('#EndDate').datebox('showPanel'); });
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#PID').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    $('#PID').combobox('reload', url);
                    var HID = rec.HouseID;
                    //如果是广州仓库显示公司类型选项
                    if (HID == "9") {
                        $("[name='BelongHouse']").show();
                        $("#BelongHouseLab").html("&nbsp;&nbsp;所属公司:");
                        $('#BelongHouse').next(".combo").show();
                        $('#BelongHouse').combobox('setValue', 0);
                    } else {
                        $('#BelongHouse').combobox('setValue', -1);
                        $("[name='BelongHouse']").hide();
                        $('#BelongHouse').next(".combo").hide();

                        $('#behoID').hide();
                    }
                    var url = '../Client/clientApi.aspx?method=QueryAllUpClientDep&type=1&houseID=' + rec.HouseID;
                    $('#BelongDepart').combobox('reload', url);
                    $('#BelongDepart').combobox('select', '-1');
                }
            });
            //客户姓名
            $('#AClientName').combobox({
                valueField: 'ClientNum', textField: 'ClientShortName',
                url: '../Client/clientApi.aspx?method=AutoCompleteClient'
            });
            $('#AClientName').combobox('textbox').bind('focus', function () { $('#AClientName').combobox('showPanel'); });
            $('#PID').combobox('clear');
            var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=<%=UserInfor.HouseID%>';
            $('#PID').combobox('reload', url);
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
            var IHH = "<%=UserInfor.IsHeadHouse%>";
            if (IHH == "1") {
                $('#PID').combobox('setText', '<%=UserInfor.HeadHouseName%>');
                $('#PID').combobox('setValue', '<%=UserInfor.HeadHouseID%>');
                $("#PID").combobox("readonly", true);
                $("#AHouseID").combobox("readonly", true);
                $('#openHub').hide();
                $("#reportMoveContainer").hide();
                $("#saleManReport").hide();
            } else {
                $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
                $('#PID').combobox('textbox').bind('focus', function () { $('#PID').combobox('showPanel'); });
            }
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            //else {
            //    $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            //}
            var LNM = "<%=UserInfor.LoginName%>";
            if (LNM == "2250") {
                $('#APID').combobox('setValue', '1');
                $("#APID").combobox("readonly", true);
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
                $("#openHub").hide();
                $("#saleManReport").hide();
                $("#reportMoveContainer").hide();

            }
            $('#BusinessID').combobox('clear');
            var Burl = '../Client/clientApi.aspx?method=QueryBusinessIDDefault';
            $('#BusinessID').combobox('reload', Burl);
            //else {
            //    $("#openHub").show();
            //    $("#saleManReport").show();
            //    $("#reportMoveContainer").show();
            //    $('#APID').combobox('textbox').bind('focus', function () { $('#APID').combobox('showPanel'); });
            //    $('#ASID').combobox('textbox').bind('focus', function () { $('#ASID').combobox('showPanel'); });
            //}
            RoleCName = "<%=UserInfor.RoleCName%>";
            if (RoleCName.indexOf("安泰路斯") >= 0) {
                $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
                $("#AHouseID").combobox("readonly", true);
                $('#AHouseID').combobox('textbox').unbind('focus');
                $('#AHouseID').combobox('textbox').css('background-color', '#e8e8e8');

                $('#APID').combobox('setValue', '1');
                $("#APID").combobox("readonly", true);
                $('#APID').combobox('textbox').unbind('focus');
                $('#APID').combobox('textbox').css('background-color', '#e8e8e8');
                //一级产品
                $('#ASID').combobox('clear');
                var url = '../Product/productApi.aspx?method=QueryALLOneProductType&PID=1';
                $('#ASID').combobox('reload', url);

                $('#ASID').combobox('setValue', '258');
                $("#ASID").combobox("readonly", true);
                $('#ASID').combobox('textbox').unbind('focus');
                $('#ASID').combobox('textbox').css('background-color', '#e8e8e8');
                $('#openHub').hide();
                $("#BelongDepart").combobox("readonly", true);
                $('#BelongDepart').combobox('textbox').unbind('focus');
                $('#BelongDepart').combobox('textbox').css('background-color', '#e8e8e8');
                $("#saleManReport").hide();
                $("#reportMoveContainer").hide();
            }
            else if (RoleCName.indexOf("汕头科矿") >= 0) {
                $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
                $("#AHouseID").combobox("readonly", true);
                $('#AHouseID').combobox('textbox').unbind('focus');
                $('#AHouseID').combobox('textbox').css('background-color', '#e8e8e8');

                $('#PID').combobox('setText', '<%=UserInfor.HeadHouseName%>');
                $('#PID').combobox('setValue', '<%=UserInfor.HeadHouseID%>');
                $("#PID").combobox("readonly", true);
                $('#PID').combobox('textbox').unbind('focus');
                $('#PID').combobox('textbox').css('background-color', '#e8e8e8');

                $('#APID').combobox('setValue', '1');
                $("#APID").combobox("readonly", true);
                $('#APID').combobox('textbox').unbind('focus');
                $('#APID').combobox('textbox').css('background-color', '#e8e8e8');
                //一级产品
                $('#ASID').combobox('clear');
                var themecombo2 = [{ 'TypeName': '樱花', 'TypeID': '540' }, { 'TypeName': '台翔', 'TypeID': '541' }, { 'TypeName': '赛为', 'TypeID': '542' }, { 'TypeName': '衡钻', 'TypeID': '543' }, { 'TypeName': '蒲公英', 'TypeID': '565' }];
                $("#ASID").combobox("loadData", themecombo2);
                $('#ASID').combobox('textbox').unbind('focus');
                $('#openHub').hide();
                $("#BelongDepart").combobox("readonly", true);
                $('#BelongDepart').combobox('textbox').unbind('focus');
                $('#BelongDepart').combobox('textbox').css('background-color', '#e8e8e8');
                $("#saleManReport").hide();
                $("#reportMoveContainer").hide();
                $('#behoID').hide();
                //客户姓名
                $('#AClientName').combobox({
                    valueField: 'ClientNum', textField: 'ClientShortName',
                    url: '../Client/clientApi.aspx?method=AutoCompleteClient&UpClientID=8'
                });
                $('#AClientName').combobox('textbox').bind('focus', function () { $('#AClientName').combobox('showPanel'); });
            }
        });
        //每日入库报表查询
        function inQuery() {
            var columns = [];
            var type = 0;
            if ($('#AHouseID').combobox('getValue') != $('#HiddenHouseID').val()) {
                $('#HiddenHouseID').val($('#AHouseID').combobox('getValue'));
                columns.push({ title: '入库时间', field: 'InHouseTime', width: '80px', formatter: DateFormatter });
                columns.push({
                    title: '入库单号', field: 'InCargoID', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '入库数量', field: 'Piece', width: '60px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                if ($('#AHouseID').combobox('getValue') != "64") {
                    columns.push({
                        title: '产品编码', field: 'ProductCode', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '产品名称', field: 'ProductName', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                }
                if ($('#AHouseID').combobox('getValue') == "47") {
                    $("#openHub").hide();
                    $("#reportMoveContainer").hide();
                    $("#saleManReport").hide();
                    columns.push({
                        title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '品牌', field: 'TypeName', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '富添盛编码', field: 'GoodsCode', width: '110px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '商贸编码', field: 'Model', width: '110px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '长和编码', field: 'Figure', width: '110px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '祺航编码', field: 'Size', width: '110px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '批次', field: 'Batch', width: '45px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '成本价', field: 'TradePrice', width: '50px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '入库类型', field: 'InHouseType', width: '100px',
                        formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='正常进货入库'>正常进货入库</span>"; }
                            else if (val == "1") { return "<span title='退货入库'>退货入库</span>"; }
                            else if (val == "2") { return "<span title='订单删除入库'>订单删除入库</span>"; }
                            else if (val == "3") { return "<span title='拉下订单入库'>拉下订单入库</span>"; }
                            else if (val == "4") { return "<span title='复盘入库'>复盘入库</span>"; }
                            else if (val == "5") { return "<span title='移库入库'>移库入库</span>"; }
                            else { return "<span title='正常进货入库'>正常进货入库</span>"; }
                        }
                    });
                } else if ($('#AHouseID').combobox('getValue') == "62") {
                    $("#ASpecsTd").html("背番:");
                    $('#ASpecs').textbox({ prompt: '请输入背番' });
                    $("td.BelongDepart").hide();
                    $("td.AModelTd").hide();
                    $("td.AFigure").hide();

                    columns.push({
                        title: '品番', field: 'GoodsCode', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '背番', field: 'Specs', width: '70px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '批次', field: 'Batch', width: '45px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '产品来源', field: 'Source', width: '100px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                } else {
                    var IHH = "<%=UserInfor.IsHeadHouse%>";
                    if (IHH == "1") {
                        $('#PID').combobox('setText', '<%=UserInfor.HeadHouseName%>');
                        $('#PID').combobox('setValue', '<%=UserInfor.HeadHouseID%>');
                        $("#PID").combobox("readonly", true);
                        $("#AHouseID").combobox("readonly", true);
                        $('#openHub').hide();
                        $("#reportMoveContainer").hide();
                        $("#saleManReport").hide();
                    }
                    //else {
                    //    $("#openHub").show();
                    //    $("#reportMoveContainer").show();
                    //    $("#saleManReport").show();
                    //}

                    columns.push({
                        title: '品牌', field: 'TypeName', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '货品代码', field: 'GoodsCode', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '型号', field: 'Model', width: '60px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '花纹', field: 'Figure', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '载速', field: 'LoadIndex', width: '45px', formatter: function (value, row) {
                            if (value != null && value != "") {
                                return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                            }
                        }
                    });
                    //columns.push({
                    //    title: '速级', field: 'SpeedLevel', width: '40px', formatter: function (value) {
                    //        if (value != null && value != "") {
                    //            return "<span title='" + value + "'>" + value + "</span>";
                    //        }
                    //    }
                    //});


                    columns.push({
                        title: '批次', field: 'Batch', width: '45px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '单价', field: 'UnitPrice', width: '40px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '销售价', field: 'SalePrice', width: '40px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    if ($('#AHouseID').combobox('getValue') == 64 || RoleCName.indexOf("汕头科矿") >= 0) {
                        columns.push({
                            title: '供应商', field: 'Supplier', width: '8%', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        });
                    } else {
                        columns.push({
                            title: '供应商', field: 'Supplier', width: '8%', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        });
                        columns.push({
                            title: '产品来源', field: 'Source', width: '8%', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        });
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
                                else if (val == "4") { return "<span title='极速达'>极速达</span>"; }
                                else if (val == "5") { return "<span title='次日达'>次日达</span>"; }
                                else { return ""; }
                            }
                        });
                    }
                    columns.push({
                        title: '入库类型', field: 'InHouseType', width: '100px',
                        formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='正常进货入库'>正常进货入库</span>"; }
                            else if (val == "1") { return "<span title='退货入库'>退货入库</span>"; }
                            else if (val == "2") { return "<span title='订单删除入库'>订单删除入库</span>"; }
                            else if (val == "3") { return "<span title='拉下订单入库'>拉下订单入库</span>"; }
                            else if (val == "4") { return "<span title='复盘入库'>复盘入库</span>"; }
                            else if (val == "5") { return "<span title='移库入库'>移库入库</span>"; }
                            else { return "<span title='正常进货入库'>正常进货入库</span>"; }
                        }
                    });
                }
                columns.push({
                    title: '入库货位', field: 'ContainerCode', width: '90px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '入库区域', field: 'AreaName', width: '60px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '入库仓库', field: 'FirstAreaName', width: '100px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '来货单号', field: 'SourceOrderNo', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '产品ID', field: 'ProductID', width: '50px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                type = 1;
            }
            $('#dg1').show();
            $('#dg2').hide();
            $('#dg3').hide();
            $('#<%=hiddenID.ClientID%>').val('0');
            $('#<%=hiddenHouseID.ClientID%>').val($("#AHouseID").combobox('getValue'));

            var gridOpts = $('#dgIn').datagrid('options');
            gridOpts.url = 'reportApi.aspx?method=QueryInDailyReport';
            $('#dgIn').datagrid('load', {
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                InHouseType: $("#AInHouseType").combobox('getValue'),
                PID: $("#PID").combobox('getValue'),
                APID: $("#APID").combobox('getValue'),//一级产品
                ASID: $("#ASID").combobox('getValue'),//二级产品
                Specs: $('#ASpecs').textbox('getValue'),
                GoodsCode: $('#AGoodsCode').textbox('getValue'),
                SourceOrderNo: $('#ASourceOrderNo').textbox('getValue'),
                Figure: $('#AFigure').val(),
                ProductName: $("#AProductName").textbox('getValue'),//产品名称
                BelongHouse: $('#BelongHouse').combobox('getValue'),//公司类型
                BelongDepart: $('#BelongDepart').combobox('getValue'),//所属部门
                HouseID: $("#AHouseID").combobox('getValue')//仓库ID
            });
            if (type == 1) {
                $('#dgIn').datagrid({
                    columns: [columns]
                })
            }
            var LNM = "<%=UserInfor.LoginName%>";
            if (LNM == "2250") {
                $('#APID').combobox('setValue', '1');
                $("#APID").combobox("readonly", true);
                //一级产品
                $('#ASID').combobox('clear');
                var url = '../Product/productApi.aspx?method=QueryALLOneProductType&PID=1';
                $('#ASID').combobox('reload', url);

                $('#ASID').combobox('setValue', '258');
                $("#ASID").combobox("readonly", true);
                $("#openHub").hide();
                $("#saleManReport").hide();
                $("#reportMoveContainer").hide();

            }
            //else {
            //    $("#openHub").show();
            //    $("#saleManReport").show();
            //    $("#reportMoveContainer").show();
            //    $('#APID').combobox('textbox').bind('focus', function () { $('#APID').combobox('showPanel'); });
            //    $('#ASID').combobox('textbox').bind('focus', function () { $('#ASID').combobox('showPanel'); });
            //}
        }
        //每日出库报表查询
        function outQuery() {
            var columns = [];
            var type = 0;
            if ($('#AHouseID').combobox('getValue') != $('#HiddenHouseID').val()) {
                $('#HiddenHouseID').val($('#AHouseID').combobox('getValue'));
                columns.push({
                    title: '出库仓库', field: 'FirstAreaName', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({ title: '出库时间', field: 'OutHouseTime', width: '80px', formatter: DateFormatter });
                if ($('#AHouseID').combobox('getValue') == "47") {
                    $("#openHub").hide();
                    $("#reportMoveContainer").hide();
                    $("#saleManReport").hide();
                    columns.push({
                        title: '出库数量', field: 'Piece', width: '60px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '客户名称', field: 'AcceptUnit', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '产品编码', field: 'ProductCode', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '产品名称', field: 'ProductName', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '品牌', field: 'TypeName', width: '80px', formatter: function (value) {
                            0
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '富添盛编码', field: 'GoodsCode', width: '110px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '商贸编码', field: 'Model', width: '110px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '长和编码', field: 'Figure', width: '110px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '祺航编码', field: 'Size', width: '110px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '批次', field: 'Batch', width: '45px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '成本价', field: 'CostPriceStore', width: '50px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '入手价', field: 'SalePriceClient', width: '50px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '对店价', field: 'CostPriceStore', width: '50px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '销售价', field: 'SalePrice', width: '50px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                } else if ($('#AHouseID').combobox('getValue') == "62") {
                    $("#ASpecsTd").html("背番:");
                    $('#ASpecs').textbox({ prompt: '请输入背番' });
                    $("td.BelongDepart").hide();
                    $("td.AModelTd").hide();
                    $("td.AFigure").hide();

                    columns.push({
                        title: '出库单号', field: 'OutCargoID', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '出库数量', field: 'Piece', width: '60px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '产品编码', field: 'ProductCode', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '产品名称', field: 'ProductName', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '品牌', field: 'TypeName', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '品番', field: 'GoodsCode', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '背番', field: 'Specs', width: '70px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '批次', field: 'Batch', width: '45px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '产品来源', field: 'Source', width: '100px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                } else {
                    var IHH = "<%=UserInfor.IsHeadHouse%>";
                    if (IHH == "1") {
                        $('#PID').combobox('setText', '<%=UserInfor.HeadHouseName%>');
                        $('#PID').combobox('setValue', '<%=UserInfor.HeadHouseID%>');
                        $("#PID").combobox("readonly", true);
                        $("#AHouseID").combobox("readonly", true);
                        $('#openHub').hide();
                        $("#reportMoveContainer").hide();
                        $("#saleManReport").hide();
                    }
                    //else {
                    //    $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
                    //    $("#openHub").show();
                    //    $("#reportMoveContainer").show();
                    //}
                    columns.push({
                        title: '出库单号', field: 'OutCargoID', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    if ($('#AHouseID').combobox('getValue') == "64") {
                        columns.push({
                            title: '客户名称', field: 'AcceptUnit', width: '80px', formatter: function (value) {
                                if (value != null && value != "") {
                                    return "<span title='" + value + "'>" + value + "</span>";
                                }
                            }
                        });
                    }
                    columns.push({
                        title: '出库数量', field: 'Piece', width: '60px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '品牌', field: 'TypeName', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    if ($('#AHouseID').combobox('getValue') != "64") {
                        columns.push({
                            title: '产品编码', field: 'ProductCode', width: '80px', formatter: function (value) {
                                if (value != null && value != "") {
                                    return "<span title='" + value + "'>" + value + "</span>";
                                }
                            }
                        });
                        columns.push({
                            title: '产品名称', field: 'ProductName', width: '80px', formatter: function (value) {
                                if (value != null && value != "") {
                                    return "<span title='" + value + "'>" + value + "</span>";
                                }
                            }
                        });
                    }
                  
                    columns.push({
                        title: '货品代码', field: 'GoodsCode', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    if ($('#AHouseID').combobox('getValue') != "64") {
                        columns.push({
                            title: '型号', field: 'Model', width: '60px', formatter: function (value) {
                                if (value != null && value != "") {
                                    return "<span title='" + value + "'>" + value + "</span>";
                                }
                            }
                        });
                    }
                    columns.push({
                        title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '花纹', field: 'Figure', width: '80px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '载速', field: 'LoadIndex', width: '45px', formatter: function (value, row) {
                            if (value != null && value != "") {
                                return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                            }
                        }
                    });
                    //columns.push({
                    //    title: '速度级别', field: 'SpeedLevel', width: '60px', formatter: function (value) {
                    //        if (value != null && value != "") {
                    //            return "<span title='" + value + "'>" + value + "</span>";
                    //        }
                    //    }
                    //});


                    columns.push({
                        title: '批次', field: 'Batch', width: '60px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    columns.push({
                        title: '销售价', field: 'SalePrice', width: '50px', formatter: function (value) {
                            if (value != null && value != "") {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    });
                    if ($('#AHouseID').combobox('getValue') == 64 || RoleCName.indexOf("汕头科矿") >= 0) {
                        columns.push({
                            title: '供应商', field: 'Supplier', width: '100px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        });
                    } else {
                        columns.push({
                            title: '供应商', field: 'Supplier', width: '100px', formatter: function (value) {
                                if (value != null && value != "") {
                                    return "<span title='" + value + "'>" + value + "</span>";
                                }
                            }
                        });
                        columns.push({
                            title: '产品来源', field: 'Source', width: '8%', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        });
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
                                else if (val == "4") { return "<span title='极速达'>极速达</span>"; }
                                else if (val == "5") { return "<span title='次日达'>次日达</span>"; }
                                else { return ""; }
                            }
                        });
                    }

                }
                columns.push({
                    title: '客户名称', field: 'AcceptUnit', width: '100px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '订单号', field: 'OrderNo', width: '90px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '订单状态', field: 'AwbStatus', width: '60px',
                    formatter: function (val, row, index) {
                        if (val == "0") { return "已下单"; }
                        else if (val == "1") { return "<span title='正在备货'>正在备货</span>"; }
                        else if (val == "2") { return "<span title='已出库'>已出库</span>"; }
                        else if (val == "3") { return "<span title='运输在途'>运输在途</span>"; }
                        else if (val == "4") { return "<span title='已到达'>已到达</span>"; }
                        else if (val == "5") { return "<span title='已签收'>已签收</span>"; }
                        else if (val == "6") { return "<span title='已拣货'>已拣货</span>"; }
                        else { return ""; }
                    }
                });
                columns.push({
                    title: '出库货位', field: 'ContainerCode', width: '80px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                columns.push({
                    title: '出库区域', field: 'AreaName', width: '60px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
             
                columns.push({
                    title: '产品ID', field: 'ProductID', width: '50px', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
                type = 1;
            }

            $('#dg2').show();
            $('#dg1').hide();
            $('#dg3').hide();
            $('#<%=hiddenID.ClientID%>').val('1');

            var gridOpts = $('#dgOut').datagrid('options');
            gridOpts.url = 'reportApi.aspx?method=QueryOutDailyReport';
            $('#dgOut').datagrid('load', {
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                AwbStatus: $("#AwbStatus").combobox('getValue'),
                OrderType: $("#AOrderType").combobox('getValue'),
                PID: $("#PID").combobox('getValue'),
                APID: $("#APID").combobox('getValue'),//一级产品
                ASID: $("#ASID").combobox('getValue'),//二级产品
                Specs: $('#ASpecs').textbox('getValue'),
                GoodsCode: $('#AGoodsCode').textbox('getValue'),
                Figure: $('#AFigure').val(),
                ProductName: $("#AProductName").textbox('getValue'),//产品名称
                BelongHouse: $('#BelongHouse').combobox('getValue'),//公司类型
                BelongDepart: $('#BelongDepart').combobox('getValue'),//所属部门
                TrafficType: $("#ATrafficType").combobox('getValue'),
                ClientNum: $("#AClientName").combobox('getValue'),
                HouseID: $("#AHouseID").combobox('getValue'),//仓库ID
                BusinessID: $('#BusinessID').combobox('getValue'),
            });
            if (type == 1) {
                $('#dgOut').datagrid({
                    columns: [columns]
                })
            }
            var LNM = "<%=UserInfor.LoginName%>";
            if (LNM == "2250") {
                $('#APID').combobox('setValue', '1');
                $("#APID").combobox("readonly", true);
                //一级产品
                $('#ASID').combobox('clear');
                var url = '../Product/productApi.aspx?method=QueryALLOneProductType&PID=1';
                $('#ASID').combobox('reload', url);

                $('#ASID').combobox('setValue', '258');
                $("#ASID").combobox("readonly", true);
                $("#openHub").hide();
                $("#saleManReport").hide();
                $("#reportMoveContainer").hide();

            }
            //else {
            //    $("#openHub").show();
            //    $("#saleManReport").show();
            //    $("#reportMoveContainer").show();
            //    $('#APID').combobox('textbox').bind('focus', function () { $('#APID').combobox('showPanel'); });
            //    $('#ASID').combobox('textbox').bind('focus', function () { $('#ASID').combobox('showPanel'); });
            //}
        }

        //预警库存查询
        function StockQuery() {
            $('#dg3').show();
            $('#dg1').hide();
            $('#dg2').hide();
            $('#<%=hiddenID.ClientID%>').val('2');

            if ($("#AHouseID").combobox('getValue') == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择区域大仓！', 'warning'); return;
            }

            var gridOpts = $('#dgStock').datagrid('options');
            gridOpts.url = 'reportApi.aspx?method=QueryStockDailyReport';
            $('#dgStock').datagrid('load', {
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                AwbStatus: $("#AwbStatus").combobox('getValue'),
                PID: $("#PID").combobox('getValue'),
                APID: $("#APID").combobox('getValue'),//一级产品
                ASID: $("#ASID").combobox('getValue'),//二级产品
                Specs: $('#ASpecs').textbox('getValue'),
                Figure: $('#AFigure').val(),
                ProductName: $("#AProductName").textbox('getValue'),//产品名称
                Meridian: "1",//Meridian=0 查询所有的，Meridian=1查询库存小于预警库存的
                BelongDepart: "-1",//$('#BelongDepart').combobox('getValue'),//所属部门
                HouseID: $("#AHouseID").combobox('getValue')//仓库ID
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
    <div name="SelectDiv2" style="background: linear-gradient(to bottom, #eff5ff 0px, #e0ecff 100%) repeat-x scroll 0 0; border-color: #95b8e7; border-style: solid; border-width: 1px 1px 0px 1px;">
        <table>
            <tr>
                <td>
                    <a class="easyui-linkbutton" style="color: Red;" iconcls="icon-chart_bar" plain="false" href="../Report/reportDaily.aspx" target="_self">&nbsp;每日仓库报表&nbsp;</a>&nbsp;&nbsp;
                    <a id="saleManReport" class="easyui-linkbutton" iconcls="icon-chart_bar" plain="false" href="../Report/saleManReport.aspx" target="_self">&nbsp;每日业务员报表&nbsp;</a>&nbsp;&nbsp;
                    <a class="easyui-linkbutton" iconcls="icon-chart_bar" plain="false" href="../Report/reportMoveContainer.aspx" id="reportMoveContainer" target="_self">&nbsp;每日移库报表&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <input type="hidden" id="HiddenHouseID" />
        <table>
            <tr>
                <td style="text-align: right;">查询时间:
                </td>
                <td colspan="0">
                    <input id="StartDate" class="easyui-datebox" style="width: 100px" />~
                        <input id="EndDate" class="easyui-datebox" style="width: 100px" />
                </td>

                <td style="text-align: right;">产品名称:
                </td>
                <td>
                    <input id="AProductName" class="easyui-textbox" data-options="prompt:'请输入产品名称'" style="width: 80px" />
                </td>
                <td style="text-align: right;">区域大仓:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;" />
                </td>
                <td id="ASpecsTd" style="text-align: right;">规格:
                </td>
                <td>
                    <input id="ASpecs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 80px" />
                </td>
                <td style="text-align: right;">货品代码:
                </td>
                <td>
                    <input id="AGoodsCode" class="easyui-textbox" data-options="prompt:'请输入货品代码'" style="width: 80px" />
                </td>
                <td style="text-align: right;">一级产品:
                </td>
                <td>
                    <input id="APID" class="easyui-combobox" style="width: 80px;"
                        panelheight="auto" />
                </td>
                <td style="text-align: right;">下单方式:
                </td>
                <td>
                    <select class="easyui-combobox" id="AOrderType" style="width: 80px;" panelheight="auto" editable="false">
                        <option value="-1">全部</option>
                        <option value="0">电脑单</option>
                        <option value="1">企业微信单</option>
                        <option value="2">微信商城单</option>
                        <option value="3">APP单</option>
                        <option value="4">小程序单</option>
                    </select>
                </td>
                <td style="text-align: right;">订单状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="AwbStatus" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">已下单</option>
                        <%--<option value="6">已拣货</option>--%>
                        <option value="2">已出库</option>
                        <%-- <option value="3">运输在途</option>
                        <option value="4">已到达</option>
                        <option value="5">已签收</option>--%>
                    </select>
                </td>

                <td class="BelongDepart" style="text-align: right;">所属部门:
                </td>
                <td class="BelongDepart">
                    <input id="BelongDepart" class="easyui-combobox" style="width: 80px;" data-options="valueField:'ID',textField:'DepName',editable:false" />
                    <%--<select class="easyui-combobox" id="BelongDepart" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="-1">全部</option>
                        <option value="0">RE渠道销售部</option>
                        <option value="1">OE渠道销售部</option>
                    </select>--%>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="inQuery()">&nbsp;入库报表&nbsp;</a>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="outQuery()">&nbsp;出库报表&nbsp;</a>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="StockQuery()">&nbsp;补货查询&nbsp;</a>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="AwbExport()">&nbsp;导&nbsp;出&nbsp;</a>
                    <%--  <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="openHub()" id="openHub">&nbsp;查询平均尺寸&nbsp;</a>--%>
                    <form runat="server" id="fm1">
                        <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" /><asp:HiddenField ID="hiddenID" runat="server" Value="0" />
                        <asp:HiddenField ID="hiddenHouseID" runat="server" Value="0" />
                    </form>
                </td>


                <td style="text-align: right;">客户名称:
                </td>
                <td>
                    <input id="AClientName" style="width: 80px;" class="easyui-combobox" data-options="editable:true,required:false" />
                </td>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="PID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'AreaID',textField:'Name'" panelheight="auto" />
                </td>
                <td class="AFigure" style="text-align: right;">花纹:
                </td>
                <td class="AFigure">
                    <input id="AFigure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 80px" />
                </td>
                <td style="text-align: right;">来货单号:
                </td>
                <td>
                    <input id="ASourceOrderNo" class="easyui-textbox" data-options="prompt:'请输入来货单号'" style="width: 80px" />
                </td>
                <td style="text-align: right;">二级产品:
                </td>
                <td>
                    <input id="ASID" class="easyui-combobox" style="width: 80px;" data-options="valueField:'TypeID',textField:'TypeName'" />
                </td>
                <td style="text-align: right;">入库类型:
                </td>
                <td>
                    <select class="easyui-combobox" id="AInHouseType" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">正常进货入库</option>
                        <option value="1">退货入库</option>
                        <option value="2">订单删除入库</option>
                        <option value="3">拉下订单入库</option>
                        <option value="4">复盘入库</option>
                        <option value="5">移库入库</option>
                    </select>
                </td>

                <td class="ATrafficType" style="text-align: right;">订单类型:
                </td>
                <td class="ATrafficType">
                    <select class="easyui-combobox" id="ATrafficType" style="width: 80px;" panelheight="auto" editable="false">
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
                <td colspan="2" id="behoID">
                    <label name="BelongHouse" id="BelongHouseLab" style="text-align: left;"></label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <select class="easyui-combobox" id="BelongHouse" style="width: 65px;" panelheight="auto">
                        <option value="0">迪乐泰</option>
                        <option value="1">好来运</option>
                    </select>
                </td>
            </tr>
        </table>
    </div>
    <!--Begin 销售轮胎平均尺寸-->
    <div id="productHub" class="easyui-dialog" style="width: 800px; height: 430px; padding: 2px 2px"
        closed="true" buttons="#productHub-buttons">
        <div id="saPanel">
            <table>
                <tr>
                    <td style="text-align: right;">查询时间:
                    </td>
                    <td>
                        <input id="HStartDate" class="easyui-datebox" style="width: 100px" />~
                        <input id="HEndDate" class="easyui-datebox" style="width: 100px" />
                    </td>
                    <td style="text-align: right;">区域大仓:
                    </td>
                    <td>
                        <input id="HHouseID" class="easyui-combobox" style="width: 80px;" panelheight="auto" />
                    </td>
                    <td style="text-align: right;">一级产品:
                    </td>
                    <td>
                        <input id="HPID" class="easyui-combobox" style="width: 80px;"
                            panelheight="auto" />
                    </td>
                    <td style="text-align: right;">二级产品:
                    </td>
                    <td>
                        <input id="HSID" class="easyui-combobox" style="width: 80px;" data-options="valueField:'TypeID',textField:'TypeName'" />
                    </td>
                </tr>
            </table>
        </div>
        <table id="dgHub" class="easyui-datagrid">
        </table>
        <div id="dgtoolbar"><a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="QueryHub()">&nbsp;查&nbsp;询&nbsp;</a></div>
        <div id="productHub-buttons">
            <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#productHub').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
        </div>
    </div>
    <!--End 销售轮胎平均尺寸-->

    <div id="dg1">
        <table id="dgIn">
        </table>
    </div>
    <div id="dg2">
        <table id="dgOut">
        </table>
    </div>
    <div id="dg3">
        <table id="dgStock">
        </table>
    </div>
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

        //查询轮胎平均尺寸
        function QueryHub() {
            var gridOpts = $('#dgHub').datagrid('options');
            gridOpts.url = 'reportApi.aspx?method=QueryAveraHub';
            $('#dgHub').datagrid('load', {
                StartDate: $('#HStartDate').datebox('getValue'),
                EndDate: $('#HEndDate').datebox('getValue'),
                APID: $("#HPID").combobox('getValue'),//一级产品
                ASID: $("#HSID").combobox('getValue'),//二级产品
                HouseID: $("#HHouseID").combobox('getValue')//仓库ID
            });
        };
        function openHub() {
            $('#productHub').dialog('open').dialog('setTitle', '统计轮胎销售平均尺寸');
            var datenow = new Date();
            $('#HStartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#HEndDate').datebox('setValue', getNowFormatDate(datenow));
            //所在仓库
            $('#HHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });
            $('#HHouseID').combobox('textbox').bind('focus', function () { $('#HHouseID').combobox('showPanel'); });
            $('#HHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            //一级产品
            $('#HPID').combobox({
                url: '../Product/productApi.aspx?method=QueryALLOneProductType', valueField: 'TypeID', textField: 'TypeName',
                onSelect: function (rec) {
                    $('#HSID').combobox('clear');
                    var url = '../Product/productApi.aspx?method=QueryALLOneProductType&PID=' + rec.TypeID;
                    $('#HSID').combobox('reload', url);
                }
            });
            $('#HPID').combobox('textbox').bind('focus', function () { $('#HPID').combobox('showPanel'); });
            $('#HSID').combobox('textbox').bind('focus', function () { $('#HSID').combobox('showPanel'); });
            showGrid();
            //var gridOpts = $('#dgHub').datagrid('options');
            //gridOpts.url = 'orderApi.aspx?method=QueryTagByOrderNo&OrderNo=' + row.OrderNo;

        }
        //标签数据列表
        function showGrid() {
            $('#dgHub').datagrid({
                width: '100%',
                height: '300px',
                title: '', //标题内容
                rownumbers: false,
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'HubDiameter',
                url: null,
                toolbar: '#dgtoolbar',
                showFooter: true,
                columns: [[
                    //{ title: '', field: '', checkbox: true, width: '30px' },
                    { title: '区域仓库', field: 'HouseName', width: '80px' },
                    { title: '开始时间', field: 'StartDate', width: '100px', formatter: DateFormatter },
                    { title: '结束时间', field: 'EndDate', width: '100px', formatter: DateFormatter },
                    { title: '轮胎尺寸', field: 'Specs', width: '80px' },
                    { title: '销售数量', field: 'Model', width: '80px' }
                ]],
                onClickRow: function (index, row) {
                    $('#dgHub').datagrid('clearSelections');
                    $('#dgHub').datagrid('selectRow', index);
                }
            });
        }


        //导出数据
        function AwbExport() {
            if ($('#<%=hiddenID.ClientID%>').val() == '0') {
                var row = $("#dgIn").datagrid('getData').rows[0];
                if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            } else if ($('#<%=hiddenID.ClientID%>').val() == '1') {
                var row = $("#dgOut").datagrid('getData').rows[0];
                if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            } else {
                var row = $("#dgStock").datagrid('getData').rows[0];
                if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            }
            var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click();
        }

    </script>

</asp:Content>
