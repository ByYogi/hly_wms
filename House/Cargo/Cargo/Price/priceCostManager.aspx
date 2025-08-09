<%@ Page Title="成本价格管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="priceCostManager.aspx.cs" Inherits="Cargo.Price.priceCostManager" %>

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
            $.getJSON("../Product/productApi.aspx?method=QueryAllProductSource", function (data) {
                ProductSource = data;
            });
            $.ajaxSetup({ async: true });
            $.getJSON("../Client/clientApi.aspx?method=QueryAllUpClientDep", function (data) {
                UpClientDep = data;
            });
            var url = '../Client/clientApi.aspx?method=QueryAllUpClientDep&type=1&houseID=<%=UserInfor.HouseID%>';
            $('#BelongDepart').combobox('reload', url);
            $('#BelongDepart').combobox('select', '-1');
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
            var HID = "<%=UserInfor.HouseID%>";
            $('#HiddenHouseID').val(HID);
            columns.push({ title: '', field: '', checkbox: true, width: '30px' });
            columns.push({ title: '产品ID', field: 'ProductID', width: '45px' });
            if (HID == "47") {
                $("#AGoodsCodeTd").html("富添盛编码:");
                $('#AGoodsCode').textbox({ prompt: '请输入富添盛编码' });
                $("#AFigureTd").html("长和编码:");
                $('#AFigure').textbox({ prompt: '请输入长和编码' });
                columns.push({ title: '产品名称', field: 'ProductName', width: '80px' });
                columns.push({ title: '规格', field: 'Specs', width: '80px' });
                columns.push({ title: '二级类型', field: 'TypeName', width: '60px' });
                columns.push({ title: '货位代码', field: 'ContainerCode', width: '80px' });
                columns.push({ title: '富添盛编码', field: 'GoodsCode', width: '110px' });
                columns.push({ title: '商贸编码', field: 'Model', width: '110px' });
                columns.push({ title: '长和编码', field: 'Figure', width: '110px' });
                columns.push({ title: '祺航编码', field: 'Size', width: '110px' });
                columns.push({ title: '入库数', field: 'Numbers', width: '50px' });
                columns.push({ title: '在库数', field: 'PackageNum', width: '50px' });
                columns.push({ title: '门店价', field: 'TradePrice', width: '50px', align: 'right' });
                columns.push({ title: '批次', field: 'Batch', width: '60px' });
            } else {
                if (HID != "64") {
                    columns.push({ title: '产品名称', field: 'ProductName', width: '80px' });
                    columns.push({ title: '二级类型', field: 'TypeName', width: '60px' });
                } else {
                    columns.push({ title: '品牌', field: 'TypeName', width: '60px' });
                }
                columns.push({ title: '货位代码', field: 'ContainerCode', width: '80px' });
                columns.push({ title: '货品代码', field: 'GoodsCode', width: '80px' });
                columns.push({ title: '型号', field: 'Model', width: '80px' });
                columns.push({ title: '规格', field: 'Specs', width: '80px' });
                columns.push({ title: '花纹', field: 'Figure', width: '80px' });
                columns.push({ title: '载重', field: 'LoadIndex', width: '50px' });
                columns.push({ title: '速度', field: 'SpeedLevel', width: '50px' });
                columns.push({ title: '入库数', field: 'Numbers', width: '50px' });
                columns.push({ title: '在库数', field: 'PackageNum', width: '50px' });

                columns.push({ title: '进仓价', field: 'InHousePrice', width: '50px', align: 'right' });
                columns.push({ title: '成本价', field: 'CostPrice', width: '50px', align: 'right', editor: { type: 'numberbox', options: { precision: 2 } } });
                var isFinal = "<%=UserInfor.IsFinalCostPrice%>";
                if (isFinal == "1") {
                    columns.push({ title: '最终成本价', field: 'FinalCostPrice', width: '70px', align: 'right', editor: { type: 'numberbox', options: { precision: 2 } } });
                }
                columns.push({ title: '含税成本价', field: 'TaxCostPrice', width: '60px', align: 'right', editor: { type: 'numberbox', options: { precision: 2 } } });
                columns.push({ title: '不含税成本价', field: 'NoTaxCostPrice', width: '70px', align: 'right', editor: { type: 'numberbox', options: { precision: 2 } } });
                columns.push({ title: '门店价', field: 'TradePrice', width: '50px', align: 'right' });
                columns.push({ title: '销售价', field: 'SalePrice', width: '50px', align: 'right' });
                columns.push({ title: '单价', field: 'UnitPrice', width: '50px', align: 'right' });
                columns.push({ title: '批次', field: 'Batch', width: '50px' });
                if (HID == "64") {
                    columns.push({ title: '供应商', field: 'Supplier', width: '80px' });
                    columns.push({ title: '进货单号', field: 'PurchaseOrderID', width: '60px' });
                } else {
                    columns.push({ title: '来源', field: 'Source', width: '90px', formatter: function (value) { return GetSourceName(value); } });
                    columns.push({
                        title: '归属部门', field: 'BelongDepart', width: '90px', formatter: function (value) {
                            return "<span title='" + GetUpClientDepName(value) + "'>" + GetUpClientDepName(value) + "</span>";
                        }
                    });
                    columns.push({ title: '工厂订单号', field: 'SourceOrderNo', width: '80px' });
                    columns.push({ title: '订单归属月', field: 'BelongMonth', width: '60px' });
                }
            }
            columns.push({
                title: '入库方式', field: 'InHouseType', width: '65px', formatter: function (val) {
                    if (val == "0") { return "<span title='正常进货入库'>正常进货入库</span>"; } else if (val == "1") { return "<span title='退货入库'>退货入库</span>"; } else if (val == "2") { return "<span title='订单删除入库'>订单删除入库</span>"; } else if (val == "3") { return "<span title='拉下订单入库'>拉下订单入库</span>"; } else if (val == "4") { return "<span title='复盘入库'>复盘入库</span>"; } else if (val == "5") { return "<span title='移库入库'>移库入库</span>"; } else { return val; }
                }
            });
            columns.push({ title: '入库时间', field: 'InHouseTime', width: '130px', formatter: DateTimeFormatter });
            $('#dg').datagrid({
                width: 'auto',
                height: '600px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ProductID',
                url: null,
                toolbar: '#toolbar',
                columns: [columns],
                onClickCell: onClickCell,
                rowStyler: function (index, row) {
                    if (row.IsModifyPrice == "1") { return "background-color:#D1EEEE"; }
                    //else if (row.InCargoStatus == "1" && row.ContainerCode == "") { return "background-color:#EBEBEB"; }
                }
            });
            //所在仓库
            $('#HID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#PID').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    $('#PID').combobox('reload', url);
                    var url = '../Client/clientApi.aspx?method=QueryAllUpClientDep&type=1&houseID=' + rec.HouseID;
                    $('#BelongDepart').combobox('reload', url);
                    $('#BelongDepart').combobox('select', '-1');
                }
            });
            $('#HID').combobox('textbox').bind('focus', function () { $('#HID').combobox('showPanel'); });
            $('#HID').combobox('setValue', '<%=UserInfor.HouseID%>');
            $('#PID').combobox('clear');
            var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=<%=UserInfor.HouseID%>';
            $('#PID').combobox('reload', url);
            var SourceUrl = '../Product/productApi.aspx?method=QueryAllProductSource';
            $('#Source').combobox('reload', SourceUrl);
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
            $('#APID').combobox('textbox').bind('focus', function () { $('#APID').combobox('showPanel'); });
            $('#ASID').combobox('textbox').bind('focus', function () { $('#ASID').combobox('showPanel'); });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            var fc = "<%=UserInfor.IsFinalCostPrice%>";
            $('#pl').hide();
            $('#ExportCostPrice').hide();
            $('#ImportCostPrice').hide();
            if (fc == "1") {
                $('#pl').show();
                $('#ExportCostPrice').show();
                $('#ImportCostPrice').show();
            }
            var SourceUrl = '../Product/productApi.aspx?method=QueryAllProductSource';
            $('#ASource').combobox('reload', SourceUrl);

            $('#Supplier').combobox({
                valueField: 'Boss', textField: 'Boss', AddField: 'PinyinName', delay: '10',
                url: '../Client/clientApi.aspx?method=AutoCompleteClient',
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                }
            });
            $('#Supplier').combobox('textbox').bind('focus', function () { $('#Supplier').combobox('showPanel'); });
        })
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

        var editIndex = undefined;
        function endEditing() {
            if (editIndex == undefined) { return true }
            if ($('#dg').datagrid('validateRow', editIndex)) {
                var rows = $("#dg").datagrid('getData').rows[editIndex];
                var ed = $('#dg').datagrid('getEditors', editIndex);
                var cg = ed[0];
                var sum = 0;
                if (cg.field == "CostPrice") { }
                if (cg.field == "FinalCostPrice") { }
                if (cg.field == "TaxCostPrice") { }
                if (cg.field == "NoTaxCostPrice") { }
                $('#dg').datagrid('endEdit', editIndex);
                editIndex = undefined;
                return true;
            } else { return false; }
        }
        function onClickCell(index, field) {
            if (field == "CostPrice" || field == "FinalCostPrice" || field == "TaxCostPrice" || field == "NoTaxCostPrice" ) {
                if (endEditing()) {
                    $('#dg').datagrid('selectRow', index).datagrid('editCell', { index: index, field: field });
                    editIndex = index;
                }
            }
            else {
                if (editIndex == undefined) { return true }
                var rows = $("#dg").datagrid('getData').rows[editIndex];
                var ed = $('#dg').datagrid('getEditors', editIndex);
                var cg = ed[0];
                var sum = 0;
                if (cg.field == "CostPrice") {
                    $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定修改成本价？', function (r) {
                        if (r) {
                            rows.CostPrice = cg.target.val();
                            rows.SalePrice = Number(cg.oldHtml);
                            var json = JSON.stringify([rows])
                            $.ajax({
                                url: 'priceApi.aspx?method=UpdateProductCostPrice',
                                type: 'post', dataType: 'json', data: { data: json },
                                success: function (text) {
                                    //var result = eval('(' + msg + ')');
                                    if (text.Result == true) {
                                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                                    }
                                    else {
                                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                    }
                                }
                            });
                        }
                        $('#dg').datagrid('reload');
                    });
                }
                else if (cg.field == "FinalCostPrice") {
                    //修改最终成本价
                    $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定修改最终成本价？', function (r) {
                        if (r) {
                            rows.FinalCostPrice = cg.target.val();
                            rows.SalePrice = Number(cg.oldHtml);
                            var json = JSON.stringify([rows])
                            $.ajax({
                                url: 'priceApi.aspx?method=UpdateProductFinalCostPrice',
                                type: 'post', dataType: 'json', data: { data: json },
                                success: function (text) {
                                    if (text.Result == true) {
                                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                                    }
                                    else {
                                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                    }
                                }
                            });
                        }
                        $('#dg').datagrid('reload');
                    });
                }
                else if (cg.field == "TaxCostPrice") {
                    //修改含税成本价
                    $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定修改含税成本价？', function (r) {
                        if (r) {
                            rows.TaxCostPrice = cg.target.val();
                            rows.SalePrice = Number(cg.oldHtml);
                            var json = JSON.stringify([rows])
                            $.ajax({
                                url: 'priceApi.aspx?method=UpdateProductTaxCostPrice',
                                type: 'post', dataType: 'json', data: { data: json },
                                success: function (text) {
                                    if (text.Result == true) {
                                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                                    }
                                    else {
                                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                    }
                                }
                            });
                        }
                        $('#dg').datagrid('reload');
                    });
                }
                else if (cg.field == "NoTaxCostPrice") {
                    $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定修改不含税成本价？', function (r) {
                        if (r) {
                            rows.NoTaxCostPrice = cg.target.val();
                            rows.SalePrice = Number(cg.oldHtml);
                            var json = JSON.stringify([rows])
                            $.ajax({
                                url: 'priceApi.aspx?method=UpdateProductNoTaxCostPrice',
                                type: 'post', dataType: 'json', data: { data: json },
                                success: function (text) {
                                    if (text.Result == true) {
                                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                                    }
                                    else {
                                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                    }
                                }
                            });
                        }
                        $('#dg').datagrid('reload');
                    });
                }
               <%-- else if (cg.field == "InHousePrice") {
                    $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定修改进仓价？', function (r) {
                        if (r) {
                            rows.NoTaxCostPrice = cg.target.val();
                            rows.SalePrice = Number(cg.oldHtml);
                            var json = JSON.stringify([rows])
                            $.ajax({
                                url: 'priceApi.aspx?method=UpdateProductNoTaxCostPrice',
                                type: 'post', dataType: 'json', data: { data: json },
                                success: function (text) {
                                    if (text.Result == true) {
                                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                                    }
                                    else {
                                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                    }
                                }
                            });
                        }
                        $('#dg').datagrid('reload');
                    });
                }--%>
                $('#dg').datagrid('endEdit', editIndex);
                editIndex = undefined;
            }
        }
        //查询
        function dosearch() {
            var type = 0;
            var columns = [];
            if ($('#HID').combobox('getValue') != $('#HiddenHouseID').val()) {
                columns.push({
                    title: '', field: '', checkbox: true, width: '30px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '产品ID', field: 'ProductID', width: '40px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '产品名称', field: 'ProductName', width: '80px', formatter: function (value) {
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
                if ($('#HID').combobox('getValue') == "47") {
                    $("#AGoodsCodeTd").html("富添盛编码:");
                    $('#AGoodsCode').textbox({ prompt: '请输入富添盛编码' });
                    $("#AFigureTd").html("长和编码:");
                    $('#AFigure').textbox({ prompt: '请输入长和编码' });
                    columns.push({
                        title: '富添盛编码', field: 'GoodsCode', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '商贸编码', field: 'Model', width: '60px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '长和编码', field: 'Figure', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                } else {
                    $("#AGoodsCodeTd").html("货品代码:");
                    $('#AGoodsCode').textbox({ prompt: '请输入货品代码' });
                    $("#AFigureTd").html("花纹:");
                    $('#AFigure').textbox({ prompt: '请输入花纹' });
                    columns.push({
                        title: '货品代码', field: 'GoodsCode', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '型号', field: 'Model', width: '80px', formatter: function (value) {
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
                        title: '载重', field: 'LoadIndex', width: '50px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '速度', field: 'SpeedLevel', width: '50px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                }
                columns.push({
                    title: '入库数', field: 'Numbers', width: '50px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '在库数', field: 'PackageNum', width: '50px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({ title: '进仓价', field: 'InHousePrice', width: '50px', align: 'right' });
                columns.push({ title: '成本价', field: 'CostPrice', width: '50px', align: 'right', editor: { type: 'numberbox', options: { precision: 2 } } });
                var isFinal = "<%=UserInfor.IsFinalCostPrice%>";
                if (isFinal == "1") {
                    columns.push({ title: '最终成本价', field: 'FinalCostPrice', width: '70px', align: 'right', editor: { type: 'numberbox', options: { precision: 2 } } });
                }
                columns.push({ title: '含税成本价', field: 'TaxCostPrice', width: '60px', align: 'right', editor: { type: 'numberbox', options: { precision: 2 } } });
                columns.push({ title: '不含税成本价', field: 'NoTaxCostPrice', width: '70px', align: 'right', editor: { type: 'numberbox', options: { precision: 2 } } });
                columns.push({
                    title: '门店价', field: 'TradePrice', width: '50px', align: 'right', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '销售价', field: 'SalePrice', width: '50px', align: 'right', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '单价', field: 'UnitPrice', width: '50px', align: 'right', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '批次', field: 'Batch', width: '50px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                if (HID == "64") {
                    columns.push({ title: '供应商', field: 'Supplier', width: '80px' });
                    columns.push({ title: '进货单号', field: 'PurchaseOrderID', width: '60px' });
                } else {
                    columns.push({ title: '来源', field: 'Source', width: '90px', formatter: function (value) { return GetSourceName(value); } });
                    columns.push({
                        title: '归属部门', field: 'BelongDepart', width: '90px', formatter: function (value) {
                            return "<span title='" + GetUpClientDepName(value) + "'>" + GetUpClientDepName(value) + "</span>";
                        }
                    });
                    columns.push({
                        title: '工厂订单号', field: 'SourceOrderNo', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                    columns.push({
                        title: '订单归属月', field: 'BelongMonth', width: '60px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    });
                }
                columns.push({
                    title: '入库方式', field: 'InHouseType', width: '65px', formatter: function (val) {
                        if (val == "0") { return "<span title='正常进货入库'>正常进货入库</span>"; } else if (val == "1") { return "<span title='退货入库'>退货入库</span>"; } else if (val == "2") { return "<span title='订单删除入库'>订单删除入库</span>"; } else if (val == "3") { return "<span title='拉下订单入库'>拉下订单入库</span>"; } else if (val == "4") { return "<span title='复盘入库'>复盘入库</span>"; } else if (val == "5") { return "<span title='移库入库'>移库入库</span>"; } else { return val; }
                    }
                });
                columns.push({ title: '入库时间', field: 'InHouseTime', width: '130px', formatter: DateTimeFormatter });
                type = 1;
            }
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'priceApi.aspx?method=QueryInHouseProduct';
            $('#dg').datagrid('load', {
                APID: $("#APID").combobox('getValue'),//一级产品
                ASID: $("#ASID").combobox('getValue'),//二级产品
                ProductName: $('#AProductName').val(),
                //ContainerCode: $('#CCode').val(),
                BelongDepart: $("#BelongDepart").combobox('getValue'),
                Specs: $('#ASpecs').textbox('getValue'),
                Model: "",//$('#AModel').val(),
                Figure: $('#AFigure').val(),
                GoodsCode: $('#AGoodsCode').val(),
                //Batch: $('#ABatch').val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                HID: $("#HID").combobox('getValue'),//区域大仓
                FirstID: $("#PID").combobox('getValue'),//所属仓库 
                ProductID: $('#ProductID').val(),
                FinalCostPrice: $('#AFinalCostPrice').val(),
                Source: $('#ASource').combobox('getValue'),
                PurchaseOrderID: $('#PurchaseOrderID').val(),
                Supplier: $('#Supplier').combobox('getValue')
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
                <td style="text-align: right;">一级产品:
                </td>
                <td>
                    <input id="APID" class="easyui-combobox" style="width: 100px;"
                        panelheight="auto" />
                </td>
                <td style="text-align: right;">二级产品:
                </td>
                <td>
                    <input id="ASID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'TypeID',textField:'TypeName'" />
                </td>
                <td style="text-align: right;">产品名称:
                </td>
                <td>
                    <input id="AProductName" class="easyui-textbox" data-options="prompt:'请输入产品名称'" style="width: 100px">
                </td>
                <td style="text-align: right;">规格:
                </td>
                <td>
                    <input id="ASpecs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 100px">
                </td>
                <td style="text-align: right;">产品ID:
                </td>
                <td>
                    <input id="ProductID" class="easyui-textbox" data-options="prompt:'请输入产品ID'" style="width: 100px">
                </td>
                <%--  <td style="text-align: right;">型号:
                </td>
                <td>
                    <input id="AModel" class="easyui-textbox" data-options="prompt:'请输入产品型号'" style="width: 80px">
                </td>--%>
                <td style="text-align: right;">区域大仓:
                </td>
                <td>
                    <input id="HID" class="easyui-combobox" style="width: 100px;" />
                </td>
                <td style="text-align: right;">进货单号:
                </td>
                <td>
                    <input id="PurchaseOrderID" class="easyui-textbox" data-options="prompt:'请输入进货单号'" style="width: 100px">
                </td>
                <td style="text-align: right;">最终成本:
                </td>
                <td>
                    <input id="AFinalCostPrice" class="easyui-numberbox" style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">入库时间:
                </td>
                <td colspan="3">
                    <input id="StartDate" class="easyui-datebox" style="width: 100px" />~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px" />
                </td>
                <td id="AGoodsCodeTd" style="text-align: right;">货品代码:
                </td>
                <td>
                    <input id="AGoodsCode" class="easyui-textbox" data-options="prompt:'请输入货品代码'" style="width: 100px">
                </td>
                <td id="AFigureTd" style="text-align: right;">花纹:
                </td>
                <td>
                    <input id="AFigure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 100px">
                </td>
               <%-- <td style="text-align: right;">批次:
                </td>
                <td>
                    <input id="ABatch" class="easyui-textbox" data-options="prompt:'请输入批次'" style="width: 100px">
                </td>--%>
                <%-- <td style="text-align: right;">货位代码:
                </td>
                <td>
                    <input id="CCode" class="easyui-textbox" data-options="prompt:'请输入货位代码'" style="width: 100px">
                </td>--%>
                <td style="text-align: right;">所属部门:
                </td>
                <td>
                    <%--<input class="easyui-combobox" id="Source" data-options="url:'../Data/ProductSource.json',method:'get',valueField:'id',textField:'text'"
                        style="width: 90px;">--%>
                    <%--<input id="Source" class="easyui-combobox" style="width: 90px;" data-options="valueField:'Source',textField:'SourceName',editable:false,required:true" />--%>
                    <input id="BelongDepart" name="BelongDepart" class="easyui-combobox" style="width: 95px;" data-options="valueField:'ID',textField:'DepName',editable:false" />
                    <%--<select class="easyui-combobox" id="BelongDepart" name="BelongDepart" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="-1">全部</option>
                        <option value="0">RE渠道销售部</option>
                        <option value="1">OE渠道销售部</option>
                    </select>--%>
                </td>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="PID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'AreaID',textField:'Name'" panelheight="auto" /></td>
                <td style="text-align: right;">产品来源:
                </td>
                <td>
                    <input id="ASource" name="Source" class="easyui-combobox" style="width: 100px;" data-options="valueField:'Source',textField:'SourceName',editable:true" />
                </td>
                <td style="text-align: right;">供应商:
                </td>
                <td>
                    <input name="Supplier" id="Supplier" style="width: 100px;" class="easyui-combobox AcceptPeople" data-options="valueField:'ClientNum',textField:'Boss',editable:true" />
                </td>
            </tr>
        </table>
    </div>
    <input type="hidden" id="HiddenHouseID" />
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="modifyCost()">&nbsp;批量修改成本价&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="pl" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="plModifyCost()">&nbsp;批量修改最终成本价&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="plt" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="plModifyTaxCost()">&nbsp;批量修改含税成本价&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="plnt" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="plModifyNoTaxCost()">&nbsp;批量修改不含税成本价&nbsp;</a>&nbsp;&nbsp;
        <a href="#" id="plnt" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="AllocateFreight()">&nbsp;批量分摊运费&nbsp;</a>&nbsp;&nbsp;        
        <a href="#" id="ImportCostPrice" class="easyui-linkbutton" iconcls="icon-in_cargo" plain="false" onclick="Import()">&nbsp;导入修改成本价&nbsp;</a>&nbsp;&nbsp;
        <table style="display: inline;">
            <tr>
                <td bgcolor="#d1eeee" height="15" width="15"></td>
                <td>：表示已修改成本价格</td>
                <%-- <td bgcolor="#CDC5BF" height="15" width="15"></td>
                <td>：表示已出完</td>--%>
            </tr>
        </table>
        <form runat="server" id="fm1">
            <asp:Button ID="btnPrice" runat="server" Style="display: none;" Text="导出" OnClick="btnPrice_Click" />
        </form>
    </div>
    <!--Begin 批量修改成本价-->
    <div id="dlgUpInNum" class="easyui-dialog" style="width: 400px; height: 250px; padding: 5px 5px"
        closed="true" buttons="#dlgUpInNum-buttons">
        <form id="fmUpInNum" class="easyui-form" method="post">
            <table>
                <tr>
                    <td style="text-align: right;">成本价格:
                    </td>
                    <td>
                        <input name="CostPrice" id="CostPrice" class="easyui-numberbox" data-options="min:0,precision:2"
                            style="width: 200px;">
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlgUpInNum-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveCostPriceModify()">&nbsp;确&nbsp;定&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgUpInNum').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <!--End 批量修改成本价-->

    <!--Begin 批量修改最终成本价-->
    <div id="dlgFinalCost" class="easyui-dialog" style="width: 400px; height: 250px; padding: 5px 5px"
        closed="true" buttons="#dlgFinalCost-buttons">
        <form id="fmFinalCost" class="easyui-form" method="post">
            <table>
                <tr>
                    <td style="text-align: right;">最终成本价格:
                    </td>
                    <td>
                        <input name="FinalCostPrice" id="FinalCostPrice" class="easyui-numberbox" data-options="min:0,precision:2"
                            style="width: 200px;">
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlgFinalCost-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveFinalCostPriceModify()">&nbsp;确&nbsp;定&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgFinalCost').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <!--End 批量修改最终成本价-->

    <!--Begin 批量修改含税成本价-->
    <div id="dlgTaxCost" class="easyui-dialog" style="width: 400px; height: 250px; padding: 5px 5px"
        closed="true" buttons="#dlgTaxCost-buttons">
        <form id="fmTaxCost" class="easyui-form" method="post">
            <table>
                <tr>
                    <td style="text-align: right;">含税成本价格:
                    </td>
                    <td>
                        <input name="TaxCostPrice" id="TaxCostPrice" class="easyui-numberbox" data-options="min:0,precision:2"
                            style="width: 200px;">
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlgTaxCost-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveTaxCostPriceModify()">&nbsp;确&nbsp;定&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgTaxCost').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <!--End 批量修改含税成本价-->
    <!--Begin 批量修改不含税成本价-->
    <div id="dlgNoTaxCost" class="easyui-dialog" style="width: 400px; height: 250px; padding: 5px 5px"
        closed="true" buttons="#dlgNoTaxCost-buttons">
        <form id="fmNoTaxCost" class="easyui-form" method="post">
            <table>
                <tr>
                    <td style="text-align: right;">不含税成本价格:
                    </td>
                    <td>
                        <input name="NoTaxCostPrice" id="NoTaxCostPrice" class="easyui-numberbox" data-options="min:0,precision:2"
                            style="width: 200px;">
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlgNoTaxCost-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveNoTaxCostPriceModify()">&nbsp;确&nbsp;定&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgNoTaxCost').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <!--End 批量修改含税成本价-->
    <%--此div用于导入数据--%>
    <div id="dlg" class="easyui-dialog" style="width: 80%; height: 530px; padding: 2px 2px" closed="true" buttons="#dlg-buttons">
        <table id="dgImport" class="easyui-datagrid">
        </table>
        <div id="dginporttoolbar">
            <a href="#" id="ExportCostPrice" class="easyui-linkbutton" iconcls="icon-out_cargo" plain="false" onclick="Export()">&nbsp;导出价格列表&nbsp;</a>&nbsp;&nbsp;
            <input type="file" id="fileT" name="file" accept=".xls,.xlsx" onchange="saveFile()" style="width: 250px;" />
            <input type="hidden" id="ExistCount" />
            <a href="#" id="btnload" class="easyui-linkbutton" iconcls="icon-out_cargo" plain="false" onclick="saveFile()">&nbsp;重新上传&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-ok" plain="false" onclick="btnSaveData()">&nbsp;保存数据&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closeDlgStatus()">&nbsp;关&nbsp;闭&nbsp;</a>
        </div>
    </div>
    <script src="../JS/easy/js/ajaxfileupload.js" type="text/javascript"></script>
    <%--此div用于导入数据--%>
    <!--Begin 分摊运费-->
    <div id="dlgAllocateFreight" class="easyui-dialog" style="width: 400px; height: 250px; padding: 5px 5px"
        closed="true" buttons="#dlgAllocateFreight-buttons">
        <form id="fmAllocateFreight" class="easyui-form" method="post">
            <table>
                <tr>
                    <td style="text-align: right;">运输费用:
                    </td>
                    <td>
                        <input name="Freight" id="Freight" class="easyui-numberbox" data-options="min:0,precision:2" style="width: 200px;">
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlgAllocateFreight-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveAllocateFreight()">&nbsp;确&nbsp;定&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgAllocateFreight').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <!--End 分摊运费-->
    <script type="text/javascript">

        function closeDlgStatus() {
            $('#dlg').dialog('close');
            $('#dgImport').datagrid('loadData', { total: 0, rows: [] });
            $('#fileT').val("");
        }

        function AllocateFreight() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning'); return;
            }
            if (rows) {
                $('#dlgAllocateFreight').dialog('open').dialog('setTitle', '批量分摊运费');
                $('#Freight').numberbox('setValue', '');
            }

        }
        function saveAllocateFreight() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要保存的数据！', 'warning'); return; }
            if ($('#Freight').numberbox('getValue') == "" || $('#Freight').numberbox('getValue') == "0.00") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请输入运输费用！', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定修改？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows);
                    $('#fmAllocateFreight').form('submit', {
                        url: 'priceApi.aspx?method=saveAllocateFreight',
                        contentType: "application/json;charset=utf-8", dataType: "json",
                        onSubmit: function (param) {
                            param.submitData = json;
                            var trd = $(this).form('enableValidation').form('validate');
                            return trd;
                        },
                        success: function (msg) {
                            var result = eval('(' + msg + ')');
                            if (result.Result) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                                $('#dg').datagrid('reload');
                                $('#dlgAllocateFreight').dialog('close');
                            } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改失败：' + result.Message, 'warning'); }
                        }
                    })
                }
            });
        }
        //导入
        function Import() {
            $('#dlg').dialog('open').dialog('setTitle', '导入成本订单数据');
            showData();
            $('#dgImport').datagrid('loadData', { total: 0, rows: [] });
        }
        //显示DataGrid数据列表
        function showData() {
            $('#dgImport').datagrid({
                width: '100%',
                height: '450px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                rownumbers: true,//显示序号
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: true, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: false, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                url: null,
                toolbar: '#dginporttoolbar',
                columns: [[
                    { title: '产品ID', field: 'ProductID', width: '50px' },
                    { title: '产品名称', field: 'ProductName', width: '120px' },
                    { title: '品牌', field: 'TypeName', width: '70px' },
                    { title: '货品代码', field: 'GoodsCode', width: '100px' },
                    { title: '规格', field: 'Specs', width: '90px' },
                    { title: '花纹', field: 'Figure', width: '90px' },
                    { title: '载重', field: 'LoadIndex', width: '35px' },
                    { title: '速度', field: 'SpeedLevel', width: '35px' },
                    { title: '旧成本价', field: 'OldCostPrice', width: '55px' },
                    { title: '新成本价', field: 'CostPrice', width: '55px' },
                    { title: '旧最终成本价', field: 'OldFinalCostPrice', width: '55px' },
                    { title: '新最终成本价', field: 'FinalCostPrice', width: '55px' },
                    { title: '单价', field: 'UnitPrice', width: '55px' },
                    { title: '批次', field: 'Batch', width: '55px' },
                    { title: '产品来源', field: 'Source', width: '85px' },
                    { title: '归属部门', field: 'BelongDepart', width: '85px' },
                    { title: '工厂订单号', field: 'SourceOrderNo', width: '90px' },
                    { title: '订单归属月', field: 'BelongMonth', width: '65px' },
                    { title: '入库时间', field: 'InHouseTime', width: '130px', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) {
                    var trs = $(this).prev().find('div.datagrid-body').find('tr');
                    //行
                    for (var i = 0; i < trs.length; i++) {
                        //行内单元格
                        for (var j = 1; j < trs[i].cells.length; j++) {
                            var row_html = trs[i].cells[j];
                            var cell_field = $(row_html).attr('field');
                            var cell_value = $(row_html).find('div').html();
                            if (cell_field == 'OldCostPrice' || cell_field == 'OldFinalCostPrice') {
                                trs[i].cells[j].style.cssText = 'background:#DCE79E;';
                            } else if (cell_field == 'CostPrice' || cell_field == 'FinalCostPrice') {
                                trs[i].cells[j].style.cssText = 'background:#CAE79E;';
                            }
                        }
                    }
                }
            });
        }
        //保存上传的文件
        function saveFile() {
            var file = $("#fileT").val();
            if (file == null || file == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择导入文件!', 'info');
                return;
            }
            $('#dgImport').datagrid('loadData', { total: 0, rows: [] });
            ajaxFileUpload();
        }

        //文件上传
        function ajaxFileUpload() {
            $('#ExistCount').val('');
            $.messager.progress({ msg: '请稍后,正在导入中...' });
            $.ajaxFileUpload({
                url: 'priceApi.aspx?method=UpCostFile',
                secureuri: false,
                fileElementId: 'fileT',
                dataType: 'json',
                success: function (data) {
                    $.messager.progress("close");
                    var val = JSON.parse(data.responseText);
                    var result = val.Result;
                    if (result == true) {
                        var type = val.Type;
                        if (type == 1) {

                            var reg = new RegExp("\r\n", "g");
                            var message = val.Message.replace(reg, "<br>");
                            $.messager.alert('以下Excel数据有误已跳过导入', message, 'warning');
                        }
                        var value = eval(val.Data);
                        $('#dgImport').datagrid('loadData', value);
                        if (val.ExistCount > 0) {
                            $('#ExistCount').val(val.ExistCount);
                        }
                    }
                    else {
                        var message = val.Message;
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', message, 'warning');
                    }
                },
                error: function (data) {
                    $.messager.progress("close");
                    var val = JSON.parse(data.responseText);
                    var result = val.Result;
                    if (result == true) {
                        var type = val.Type;
                        if (type == 1) {

                            var reg = new RegExp("\r\n", "g");
                            var message = val.Message.replace(reg, "<br>");
                            $.messager.alert('以下Excel数据有误已跳过导入', message, 'warning');
                        }
                        var value = eval(val.Data);
                        $('#dgImport').datagrid('loadData', value);
                        if (val.ExistCount > 0) {
                            $('#ExistCount').val(val.ExistCount);
                        }
                    }
                    else {
                        var message = val.Message;
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', message, 'warning');
                    }
                }
            })
            return false;
        }

        //保存数据
        function btnSaveData() {
            var rows = $("#dgImport").datagrid('getData').rows;
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请先导入需要保存的数据！', 'warning');
                return;
            }
            var msg = "确定保存？";
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', msg, function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'priceApi.aspx?method=SaveImportCostData',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            if (text.Result == true) {
                                if (text.Message == "成功") {
                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                                } else {
                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'info');
                                }
                                $('#dlg').dialog('close');
                                dosearch();
                                $("#fileT").val("");
                                $('#dgImport').datagrid('loadData', { total: 0, rows: [] });
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }
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
        };
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
        //批量修改不含税成本 价
        function plModifyNoTaxCost() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning'); return;
            }
            if (rows) {
                $('#dlgNoTaxCost').dialog('open').dialog('setTitle', '批量修改产品不含税成本价格');
                $('#NoTaxCostPrice').numberbox('setValue', rows[0].NoTaxCostPrice);
            }

        }
        function saveNoTaxCostPriceModify() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要保存的数据！', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定修改？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows);
                    $('#fmNoTaxCost').form('submit', {
                        url: 'priceApi.aspx?method=saveNoTaxCostPriceModify',
                        contentType: "application/json;charset=utf-8", dataType: "json",
                        onSubmit: function (param) {
                            param.submitData = json;
                            var trd = $(this).form('enableValidation').form('validate');
                            return trd;
                        },
                        success: function (msg) {
                            var result = eval('(' + msg + ')');
                            if (result.Result) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                                $('#dg').datagrid('reload');
                                $('#dlgNoTaxCost').dialog('close');
                            } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改失败：' + result.Message, 'warning'); }
                        }
                    })
                }
            });
        }
        //批量修改含税成本价
        function plModifyTaxCost() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning'); return;
            }
            if (rows) {
                $('#dlgTaxCost').dialog('open').dialog('setTitle', '批量修改产品含税成本价格');
                $('#TaxCostPrice').numberbox('setValue', rows[0].TaxCostPrice);
            }
        }
        function saveTaxCostPriceModify() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要保存的数据！', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定修改？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows);
                    $('#fmTaxCost').form('submit', {
                        url: 'priceApi.aspx?method=saveTaxCostPriceModify',
                        contentType: "application/json;charset=utf-8", dataType: "json",
                        onSubmit: function (param) {
                            param.submitData = json;
                            var trd = $(this).form('enableValidation').form('validate');
                            return trd;
                        },
                        success: function (msg) {
                            var result = eval('(' + msg + ')');
                            if (result.Result) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                                $('#dg').datagrid('reload');
                                $('#dlgTaxCost').dialog('close');
                            } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改失败：' + result.Message, 'warning'); }
                        }
                    })
                }
            });
        }
        //批量修改最终成本 价
        function plModifyCost() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning'); return;
            }
            if (rows) {
                $('#dlgFinalCost').dialog('open').dialog('setTitle', '批量修改产品最终成本价格');
                $('#FinalCostPrice').numberbox('setValue', rows[0].FinalCostPrice);
            }
        }
        //批量修改最终成本 价
        function saveFinalCostPriceModify() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要保存的数据！', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定修改？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows);
                    $('#fmFinalCost').form('submit', {
                        url: 'priceApi.aspx?method=saveFinalCostPriceModify',
                        contentType: "application/json;charset=utf-8", dataType: "json",
                        onSubmit: function (param) {
                            param.submitData = json;
                            var trd = $(this).form('enableValidation').form('validate');
                            return trd;
                        },
                        success: function (msg) {
                            var result = eval('(' + msg + ')');
                            if (result.Result) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                                $('#dg').datagrid('reload');
                                $('#dlgFinalCost').dialog('close');
                            } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改失败：' + result.Message, 'warning'); }
                        }
                    })
                }
            });
        }
        //批量修改成本价
        function modifyCost() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning'); return;
            }
            if (rows) {
                $('#dlgUpInNum').dialog('open').dialog('setTitle', '批量修改产品成本价格');
                $('#CostPrice').numberbox('setValue', rows[0].CostPrice);
            }
        }
        //保存批量修改成本价
        function saveCostPriceModify() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要保存的数据！', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定修改？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows);
                    $('#fmUpInNum').form('submit', {
                        url: 'priceApi.aspx?method=saveCostPriceModify',
                        contentType: "application/json;charset=utf-8", dataType: "json",
                        onSubmit: function (param) {
                            param.submitData = json;
                            var trd = $(this).form('enableValidation').form('validate');
                            return trd;
                        },
                        success: function (msg) {
                            var result = eval('(' + msg + ')');
                            if (result.Result) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                                $('#dg').datagrid('reload');
                                $('#dlgUpInNum').dialog('close');
                            } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改失败：' + result.Message, 'warning'); }
                        }
                    })
                }
            });
        }
        function Export() {
            $.ajax({
                url: "priceApi.aspx?method=QueryInHouseProduct&APID=" + $("#APID").combobox('getValue') + "&ASID=" + $("#ASID").combobox('getValue') + "&ProductName=" + $('#AProductName').val() + "&BelongDepart=" + $("#BelongDepart").combobox('getValue') + "&Specs=" + $('#ASpecs').textbox('getValue') + "&Figure=" + $('#AFigure').val() + "&GoodsCode=" + $('#AGoodsCode').val() + "&StartDate=" + $('#StartDate').datebox('getValue') + "&EndDate=" + $('#EndDate').datebox('getValue') + "&HID=" + $("#HID").combobox('getValue') + "&FirstID=" + $("#PID").combobox('getValue') + "&ProductID=" + $('#ProductID').val() + "&FinalCostPrice=" + $('#AFinalCostPrice').val() + "&Source=" + $('#ASource').combobox('getValue') + "&PurchaseOrderID=" + $('#PurchaseOrderID').val() + "&Supplier=" + $('#Supplier').combobox('getValue') + "&Export=true",
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=btnPrice.ClientID %>"); obj.click() }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }
    </script>
</asp:Content>
