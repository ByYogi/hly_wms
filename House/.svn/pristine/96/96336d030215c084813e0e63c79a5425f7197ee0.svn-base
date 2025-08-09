<%@ Page Title="一二三汽车用品产品管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="productAuto.aspx.cs" Inherits="Cargo.Product.productAuto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        //页面加载时执行
        window.onload = function () {
            adjustment();
        }
        $(window).resize(function () {
            adjustment();
        });
        function adjustment() {
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true);
            $('#dg').datagrid({ height: height });
        }
        $(document).ready(function () {
            var columns = [];
            columns.push({
                title: '', field: '', checkbox: true, width: '30px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品ID', field: 'ProductID', width: '50px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '区域大仓', field: 'HouseName', width: '80px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在仓库', field: 'AreaName', width: '80px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品名称', field: 'ProductName', width: '80px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '一级类型', field: 'ParentName', width: '80px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '二级类型', field: 'TypeName', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '货位代码', field: 'ContainerCode', width: '80px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '库存', field: 'RealStockNum', width: '40px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '货品代码', field: 'GoodsCode', width: '90px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '型号', field: 'Model', width: '75px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '规格', field: 'Specs', width: '75px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '花纹', field: 'Figure', width: '95px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '载重', field: 'LoadIndex', width: '40px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '速度', field: 'SpeedLevel', width: '40px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '批次', field: 'Batch', width: '40px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '数量', field: 'Numbers', width: '40px', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '单价', field: 'UnitPrice', width: '50px', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '入库类型', field: 'OperaType', width: '60px', formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='电脑入库'>电脑入库</span>"; }
                    else if (val == "1") { return "<span title='扫描入库'>扫描入库</span>"; } else { return "<span title='电脑入库'>电脑入库</span>"; }
                }
            });
            columns.push({
                title: '入库状态', field: 'InCargoStatus', width: '60px', formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='未入库'>未入库</span>"; }
                    else if (val == "1") { return "<span title='已入库'>已入库</span>"; } else { return "<span title='未入库'>未入库</span>"; }
                }
            });
            columns.push({ title: '入库时间', field: 'InHouseTime', width: '130px', formatter: DateTimeFormatter });
            columns.push({
                title: '产地', field: 'Born', width: '50px', formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='国产'>国产</span>"; }
                    else if (val == "1") { return "<span title='进口'>进口</span>"; } else { return ""; }
                }
            });
            columns.push({
                title: 'OERE', field: 'Assort', width: '50px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '销售价', field: 'SalePrice', width: '50px', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '门店价', field: 'TradePrice', width: '50px', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '来源', field: 'Source', width: '100px', formatter: function (value) {
                    return "<span title='" + GetSourceName(value) + "'>" + GetSourceName(value) + "</span>";
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
                    else { return ""; }
                }
            });
            columns.push({
                title: '来货单号', field: 'SourceOrderNo', width: '80px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '订单归属月', field: 'BelongMonth', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '锁定库存', field: 'IsLockStock', width: '50px', formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='未锁定'>未锁定</span>"; }
                    else if (val == "1") { return "<span title='已锁定'>已锁定</span>"; } else { return ""; }
                }
            });
            columns.push({ title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter });



            $('#dg').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ProductID',
                url: null,
                toolbar: '#toolbar',
                columns: [columns],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { editItemByID(index); },
                rowStyler: function (index, row) {
                    if (row.InCargoStatus == "0") { return "background-color:#D1EEEE"; }
                    if (row.IsLockStock == "1") { return "background-color:#f7e9ee"; }
                    //else if (row.InCargoStatus == "1" && row.ContainerCode == "") { return "background-color:#EBEBEB"; }
                }
            });
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#AHID').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    $('#AHID').combobox('reload', url);
                    var url = '../Client/clientApi.aspx?method=QueryAllUpClientDep&type=1&houseID=' + rec.HouseID;
                    $('#ABelongDepart').combobox('reload', url);
                    $('#ABelongDepart').combobox('select', '-1');
                }
            });
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            $('#AHID').combobox('clear');
            var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=<%=UserInfor.HouseID%>';
            $('#AHID').combobox('reload', url);
            //一级产品
            $('#APID').combobox({
                url: 'productApi.aspx?method=QueryALLOneProductType', valueField: 'TypeID', textField: 'TypeName',
                onSelect: function (rec) {
                    $('#ASID').combobox('clear');
                    var url = 'productApi.aspx?method=QueryALLOneProductType&PID=' + rec.TypeID;
                    $('#ASID').combobox('reload', url);
                }
            });
            //一级产品
            $('#ParentID').combobox({
                url: 'productApi.aspx?method=QueryALLOneProductType', valueField: 'TypeID', textField: 'TypeName', editable: false,
                onSelect: function (rec) {
                    $('#TypeID').combobox('clear');
                    var url = 'productApi.aspx?method=QueryALLOneProductType&PID=' + rec.TypeID;
                    $('#TypeID').combobox('reload', url);
                }
            });
            $('#ASID').combobox('textbox').bind('focus', function () { $('#ASID').combobox('showPanel'); });
            $('#APID').combobox('textbox').bind('focus', function () { $('#APID').combobox('showPanel'); });
            $('#AInCargoStatus').combobox('textbox').bind('focus', function () { $('#AInCargoStatus').combobox('showPanel'); });
            var SourceUrl = '../Product/productApi.aspx?method=QueryAllProductSource';
            $('#ProductSource').combobox('reload', SourceUrl);
            var IHH = "<%=UserInfor.IsHeadHouse%>";
            if (IHH == "1") {
                $('#AHID').combobox('setText', '<%=UserInfor.HeadHouseName%>');
                $('#AHID').combobox('setValue', '<%=UserInfor.HeadHouseID%>');
                $("#AHID").combobox("readonly", true);
                $("#AHouseID").combobox("readonly", true);
                $('#btnAdd').hide();
                $('#btnUpdate').hide();
                $('#btnDel').hide();
                $('#btnCopy').hide();
                $('#btnIn').hide();
                $('#btnLotinIn').hide();
                $('#btnPrint').hide();
                $('#btnCancleIn').hide();
                $('#btnTag').hide();
            } else {
                $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
                $('#AHID').combobox('textbox').bind('focus', function () { $('#AHID').combobox('showPanel'); });

            }
        });
        //查询
        function dosearch() {
            var type = 0;
            var columns = [];
            if ($('#AHouseID').combobox('getValue') != $('#HiddenHouseID').val()) {
                $('#HiddenHouseID').val($('#AHouseID').combobox('getValue'));
                if ($('#AHouseID').combobox('getValue') == "47") {
                    $("#AModelTd").html("商贸编码:");
                    $('#AModel').textbox({ prompt: '请输入商贸编码' });
                    $("#AFigureTd").html("长和编码:");
                    $('#AFigure').textbox({ prompt: '请输入长和编码' });
                    $("#AGoodsCodeTd").html("富添盛编码:");
                    $('#AGoodsCode').textbox({ prompt: '请输入富添盛编码' });
                    //$("#ASpecsTd").html("祺航编码:");
                    //$('#ASpecs').textbox({ prompt: '请输入祺航编码' });
                    $("td.ATagCodeTd").hide();
                    $("td.AFigureTd").hide();
                    $("td.AModel").hide();
                    $("td.ASpeedLevelTd").hide();
                    $("td.ALoadIndexTd").hide();
                    $("td.ABelongDepartTd").hide();
                    columns.push({
                        title: '', field: '', checkbox: true, width: '30px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '产品ID', field: 'ProductID', width: '50px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '区域大仓', field: 'HouseName', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '所在仓库', field: 'AreaName', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '一级类型', field: 'ParentName', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '二级类型', field: 'TypeName', width: '60px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '富添盛编码', field: 'GoodsCode', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '商贸编码', field: 'Model', width: '110px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '长和编码', field: 'Figure', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '产品名称', field: 'ProductName', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '规格', field: 'Specs', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '货位代码', field: 'ContainerCode', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '库存', field: 'RealStockNum', width: '40px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '单位', field: 'Package', width: '40px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '成本价', field: 'TradePrice', width: '60px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '数量', field: 'Numbers', width: '60px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '入库类型', field: 'OperaType', width: '60px', formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='电脑入库'>电脑入库</span>"; }
                            else if (val == "1") { return "<span title='扫描入库'>扫描入库</span>"; } else { return "<span title='电脑入库'>电脑入库</span>"; }
                        }
                    });
                    columns.push({
                        title: '入库状态', field: 'InCargoStatus', width: '60px', formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='未入库'>未入库</span>"; }
                            else if (val == "1") { return "<span title='已入库'>已入库</span>"; } else { return "<span title='未入库'>未入库</span>"; }
                        }
                    });
                    columns.push({ title: '入库时间', field: 'InHouseTime', width: '130px', formatter: DateTimeFormatter });
                    columns.push({ title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter });
                }
                else if (HID == "62") {

                    columns.push({
                        title: '', field: '', checkbox: true, width: '30px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '产品ID', field: 'ProductID', width: '50px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '区域大仓', field: 'HouseName', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '所在仓库', field: 'AreaName', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '产品名称', field: 'ProductName', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '一级类型', field: 'ParentName', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '二级类型', field: 'TypeName', width: '60px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '货位代码', field: 'ContainerCode', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '库存', field: 'RealStockNum', width: '40px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
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
                        title: '批次', field: 'Batch', width: '70px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '数量', field: 'Numbers', width: '40px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '入库类型', field: 'OperaType', width: '60px', formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='电脑入库'>电脑入库</span>"; }
                            else if (val == "1") { return "<span title='扫描入库'>扫描入库</span>"; } else { return "<span title='电脑入库'>电脑入库</span>"; }
                        }
                    });
                    columns.push({
                        title: '入库状态', field: 'InCargoStatus', width: '60px', formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='未入库'>未入库</span>"; }
                            else if (val == "1") { return "<span title='已入库'>已入库</span>"; } else { return "<span title='未入库'>未入库</span>"; }
                        }
                    });
                    columns.push({ title: '入库时间', field: 'InHouseTime', width: '130px', formatter: DateTimeFormatter });
                    columns.push({
                        title: '来源', field: 'Source', width: '100px', formatter: function (value) {
                            return "<span title='" + GetSourceName(value) + "'>" + GetSourceName(value) + "</span>";
                        }
                    });
                    columns.push({
                        title: '来货单号', field: 'SourceOrderNo', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '订单归属月', field: 'BelongMonth', width: '60px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({ title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter });
                } else {
                    $("#AModelTd").html("型号:");
                    $('#AModel').textbox({ prompt: '请输入型号' });
                    $("#AFigureTd").html("花纹:");
                    $('#AFigure').textbox({ prompt: '请输入花纹' });
                    $("#AGoodsCodeTd").html("货品代码:");
                    $('#AGoodsCode').textbox({ prompt: '请输入货品代码' });
                    $("#ASpecsTd").html("规格:");
                    $('#ASpecs').textbox({ prompt: '请输入规格' });
                    $("td.ATagCodeTd").show();
                    $("td.AModel").show();
                    $("td.AFigureTd").show();
                    $("td.ASpeedLevelTd").show();
                    $("td.ALoadIndexTd").show();
                    $("td.ABelongDepartTd").show();
                    //if ($('#AHouseID').combobox('getValue') == "9") {
                    //    $("td.ABelongDepartTd").show();
                    //} else {
                    //    $("td.ABelongDepartTd").hide();
                    //}
                    columns.push({
                        title: '', field: '', checkbox: true, width: '30px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '产品ID', field: 'ProductID', width: '50px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '区域大仓', field: 'HouseName', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '所在仓库', field: 'AreaName', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '产品名称', field: 'ProductName', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '一级类型', field: 'ParentName', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '二级类型', field: 'TypeName', width: '60px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '货位代码', field: 'ContainerCode', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '库存', field: 'RealStockNum', width: '40px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '货品代码', field: 'GoodsCode', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '型号', field: 'Model', width: '60px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '花纹', field: 'Figure', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '载重', field: 'LoadIndex', width: '40px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '速度', field: 'SpeedLevel', width: '40px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '批次', field: 'Batch', width: '40px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '数量', field: 'Numbers', width: '40px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '单价', field: 'UnitPrice', width: '50px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '入库类型', field: 'OperaType', width: '60px', formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='电脑入库'>电脑入库</span>"; }
                            else if (val == "1") { return "<span title='扫描入库'>扫描入库</span>"; } else { return "<span title='电脑入库'>电脑入库</span>"; }
                        }
                    });
                    columns.push({
                        title: '入库状态', field: 'InCargoStatus', width: '60px', formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='未入库'>未入库</span>"; }
                            else if (val == "1") { return "<span title='已入库'>已入库</span>"; } else { return "<span title='未入库'>未入库</span>"; }
                        }
                    });
                    columns.push({ title: '入库时间', field: 'InHouseTime', width: '130px', formatter: DateTimeFormatter });
                    columns.push({
                        title: '产地', field: 'Born', width: '50px', formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='国产'>国产</span>"; }
                            else if (val == "1") { return "<span title='进口'>进口</span>"; } else { return ""; }
                        }
                    });
                    columns.push({
                        title: 'OERE', field: 'Assort', width: '50px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '销售价', field: 'SalePrice', width: '50px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '门店价', field: 'TradePrice', width: '50px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '来源', field: 'Source', width: '100px', formatter: function (value) {
                            return "<span title='" + GetSourceName(value) + "'>" + GetSourceName(value) + "</span>";
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
                            else { return ""; }
                        }
                    });
                    columns.push({
                        title: '来货单号', field: 'SourceOrderNo', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '订单归属月', field: 'BelongMonth', width: '60px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '锁定库存', field: 'IsLockStock', width: '50px', formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='未锁定'>未锁定</span>"; }
                            else if (val == "1") { return "<span title='已锁定'>已锁定</span>"; } else { return ""; }
                        }
                    });
                    columns.push({ title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter });
                }
                type = 1;
            }
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'productApi.aspx?method=QueryProduct';
            $('#dg').datagrid('load', {
                Specs: $('#ASpecs').val(),
                Model: $('#AModel').val(),
                GoodsCode: $('#AGoodsCode').val(),//AMeridian
                PID: $("#APID").combobox('getValue'),//一级产品
                SID: $("#ASID").combobox('getValue'),//二级产品
                ProductName: $('#AProductName').val(),
                Batch: $('#ABatch').val(),
                Figure: $('#AFigure').val(),
                InCargoStatus: $('#AInCargoStatus').combobox('getValue'),
                LoadIndex: $('#ALoadIndex').val(),
                ProductID: $('#ProductID').val(),
                HouseID: $("#AHouseID").combobox('getValue'),//仓库ID
                ProductSource: $("#ProductSource").combobox('getValue'),//仓库ID
                AHID: $("#AHID").combobox('getValue'),//一级仓库
                //AAID: $("#AAID").combobox('getValue'),//一级区域
                //SpeedLevel: $("#ASpeedLevel").combobox('getValue'),
                SourceOrderNo: $("#BSourceOrderNo").val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                BelongDepart: $("#ABelongDepart").combobox('getValue'),//归属部门
                TagCode: $("#ATagCode").val()//轮胎唯一编码
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
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table>
            <tr>
                <td id="ASpecsTd" style="text-align: right;">产品规格:
                </td>
                <td>
                    <input id="ASpecs" class="easyui-textbox" data-options="prompt:'请输入产品规格'" style="width: 80px">
                </td>
                <td class="AModel" id="AModelTd" style="text-align: right;">产品型号:
                </td>
                <td class="AModel">
                    <input id="AModel" class="easyui-textbox" data-options="prompt:'请输入产品型号'" style="width: 80px">
                </td>
                <td style="text-align: right;">产品名称:
                </td>
                <td>
                    <%-- <input id="ATreadWidth" class="easyui-numberbox" data-options="min:0,precision:2" style="width: 100px">--%>
                    <input id="AProductName" class="easyui-textbox" data-options="prompt:'请输入产品名称'" style="width: 100px">
                </td>
                <td class="ProductIDTd" style="text-align: right;">产品ID:
                </td>
                <td class="ProductIDTd">
                    <input id="ProductID" class="easyui-textbox" data-options="prompt:'请输入产品ID'" style="width: 100px">
                </td>
                <td style="text-align: right;">入库状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="AInCargoStatus" style="width: 100px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">未入库</option>
                        <option value="1">已入库</option>
                    </select>
                </td>
                <td style="text-align: right;">一级产品:
                </td>
                <td>
                    <input id="APID" class="easyui-combobox" style="width: 100px;"
                        panelheight="auto" />
                </td>
                <td style="text-align: right;">区域大仓:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;" />
                </td>
            </tr>
            <tr>
                <td class="AFigureTd" id="AFigureTd" style="text-align: right;">花纹:
                </td>
                <td class="AFigureTd">
                    <input id="AFigure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 80px">
                </td>

                <td style="text-align: right;">批次:
                </td>
                <td>
                    <input id="ABatch" class="easyui-textbox" data-options="prompt:'请输入批次'" style="width: 80px">
                </td>
                <td id="AGoodsCodeTd" style="text-align: right;">货品代码:
                </td>
                <td>
                    <input id="AGoodsCode" class="easyui-textbox" data-options="prompt:'请输入货品代码'" style="width: 100px">
                </td>
                <td class="ASpeedLevelTd" style="text-align: right;">来货单号:
                </td>
                <td class="ASpeedLevelTd">
                    <input id="BSourceOrderNo" class="easyui-textbox" data-options="prompt:'请输入来货单号'" style="width: 100px">
                </td>
                <%--  <td class="ASpeedLevelTd" style="text-align: right;">速度级别:
                </td>
                <td class="ASpeedLevelTd">
                    <input class="easyui-combobox" id="ASpeedLevel" data-options="url:'../Data/TyreSpeedLevel.json',method:'get',valueField:'id',textField:'text'"
                        style="width: 100px;">
                </td>--%>
                <td style="text-align: right;">二级产品:
                </td>
                <td>
                    <input id="ASID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'TypeID',textField:'TypeName'" />
                </td>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="AHID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'AreaID',textField:'Name'" panelheight="auto" />
                </td>
                <%-- <td style="text-align: right;">一级区域:
                </td>
                <td>
                    <input id="AAID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'AreaID',textField:'Name'"
                        panelheight="auto" />
                </td>--%>
            </tr>
            <tr>
                <td style="text-align: right;">入库时间:
                </td>
                <td colspan="3">
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px">
                </td>
                <td style="text-align: right;">产品来源:
                </td>
                <td>
                    <input id="ProductSource" name="ProductSource" class="easyui-combobox" style="width: 100px;" data-options="valueField:'Source',textField:'SourceName',editable:false" />
                </td>
                <td colspan="6"></td>
            </tr>
        </table>
    </div>
    <input type="hidden" id="HiddenHouseID" />
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="btnAdd" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">&nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="btnUpdate" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="btnDel" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="btnCopy" class="easyui-linkbutton" iconcls="icon-page_copy" plain="false" onclick="CopyPro()">&nbsp;复&nbsp;制&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="btnIn" class="easyui-linkbutton" iconcls="icon-in_cargo" plain="false" onclick="inCargo()">&nbsp;入&nbsp;库&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="btnLotinIn" class="easyui-linkbutton" iconcls="icon-in_cargo" plain="false" onclick="LotinCargo()">&nbsp;批量入库&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="btnCancleIn" class="easyui-linkbutton" iconcls="icon-out_cargo" plain="false" onclick="cancleCargo()">&nbsp;撤销入库&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="btnPrint" class="easyui-linkbutton" iconcls="icon-print" plain="false" onclick="printCargoAwb()">&nbsp;打印入库单&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="btnTag" class="easyui-linkbutton" iconcls="icon-tag_blue" plain="false" onclick="queryTag()">&nbsp;查看标签&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-lock_add"
            plain="false" onclick="lockStock()">&nbsp;锁定库存&nbsp;</a>

        <table style="display: inline;">
            <tr>
                <td bgcolor="#d1eeee" height="15" width="15"></td>
                <td>未入库</td>
                <td bgcolor="#f7e9ee" height="15" width="15"></td>
                <td>锁定库存</td>
                <%-- <td bgcolor="#CDC5BF" height="15" width="15"></td>
                <td>：表示已出完</td>--%>
            </tr>
        </table>
    </div>
    <!--Begin 新增修改产品操作-->
</asp:Content>
