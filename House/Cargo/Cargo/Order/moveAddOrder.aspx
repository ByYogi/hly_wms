<%@ Page Title="新增移库订单" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="moveAddOrder.aspx.cs" Inherits="Cargo.Order.moveAddOrder" %>

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
            $('#ABelongDepart').combobox('reload', url);
            $('#ABelongDepart').combobox('select', '-1');
        }
        $(window).resize(function () {
            adjustment();
        });
        function adjustment() {
            //var height = parseInt((Number($(window).height()) - 100) / 2);
            var height = parseInt((Number(window.outerHeight) - Number($("#saPanel2").outerHeight(true)) - Number($("div[name='SelectDiv1']").outerHeight(true))) / 2) - 100;
            $('#dg').datagrid({ height: height });
            $('#outDg').datagrid({ height: height });
        }
        $(document).ready(function () {
            var columns = [];

            columns.push({ title: '', field: 'ID', checkbox: true, width: '30px' });
            columns.push({
                title: '产品ID', field: 'ProductID', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品', field: 'TypeName', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '型号', field: 'Model', width: '80px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '花纹', field: 'Figure', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '在库件数', field: 'Piece', width: '60px', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '批次', field: 'Batch', width: '50px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            //{ title: '批次', field: 'BatchYear', width: '80px', formatter: function (val, row, index) { return val + "年" } },
            //columns.push({
            //    title: '速度级别', field: 'SpeedLevel', width: '60px', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});
            columns.push({
                title: '载速', field: 'LoadIndex', width: '60px', formatter: function (value, row) {
                    return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                }
            });
            columns.push({
                title: '门店价', field: 'TradePrice', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '销售价', field: 'SalePrice', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '货位代码', field: 'ContainerCode', width: '90px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在区域', field: 'AreaName', width: '90px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在仓库', field: 'FirstAreaName', width: '80px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '供应商', field: 'Supplier', width: '120px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品名称', field: 'ProductName', width: '120px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品来源', field: 'Source', width: '8%', formatter: function (value) {
                    return "<span title='" + GetSourceName(value) + "'>" + GetSourceName(value) + "</span>";
                }
            });
            columns.push({
                title: '归属部门', field: 'BelongDepart', width: '100px', formatter: function (value) {
                    return "<span title='" + GetUpClientDepName(value) + "'>" + GetUpClientDepName(value) + "</span>";
                }
            });
            //columns.push({ title: '入库时间', field: 'InHouseTime', width: '130px', formatter: DateTimeFormatter });
            columns.push({
                title: '单价', field: 'UnitPrice', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            //columns.push({ title: '数量', field: 'Numbers', width: '60px' });
            //{ title: '成本价', field: 'CostPrice', width: '60px' },
            //columns.push({ title: '门店价', field: 'TradePrice', width: '60px' });
            //columns.push({ title: '入库状态', field: 'InCargoStatus', width: '60px', formatter: function (val, row, index) { if (val == "0") { return "未入库"; } else if (val == "1") { return "已入库"; } else { return "未入库"; } } });
            //{ title: '包装', field: 'Package', width: '50px' },
            //{ title: '包装数量', field: 'PackageNum', width: '80px' },
            //{ title: '包装重量', field: 'PackageWeight', width: '80px' },
            //columns.push({ title: '业务员销售价', field: 'ActSalePrice', hidden: true });
            //columns.push({ title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter });
            $('#dg').datagrid({
                width: '100%',
                height: '200px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
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
                onDblClickRow: function (index, row) { dblClickOutCargo(index); }
            });

            columns = [];

            columns.push({ title: '', field: 'ID', checkbox: true, width: '30px' });
            columns.push({
                title: '产品ID', field: 'ProductID', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品', field: 'TypeName', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '型号', field: 'Model', width: '80px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '花纹', field: 'Figure', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '出库件数', field: 'Piece', width: '60px', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '批次', field: 'Batch', width: '50px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            //{ title: '批次', field: 'BatchYear', width: '80px', formatter: function (val, row, index) { return val + "年" } },
            columns.push({
                title: '载速', field: 'LoadIndex', width: '60px', formatter: function (value, row) {
                    return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                }
            });
            //columns.push({
            //    title: '速度级别', field: 'SpeedLevel', width: '60px', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});
            //columns.push({
            //    title: '载重指数', field: 'LoadIndex', width: '60px', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});
            columns.push({
                title: '门店价' , field: 'TradePrice', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '销售价', field: 'SalePrice', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            //columns.push({ title: '业务员销售价', field: 'ActSalePrice', width: '100px' });
            columns.push({
                title: '货位代码', field: 'ContainerCode', width: '90px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在区域', field: 'AreaName', width: '90px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在仓库', field: 'FirstAreaName', width: '80px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '供应商', field: 'Supplier', width: '120px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品名称', field: 'ProductName', width: '120px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品来源', field: 'Source', width: '8%', formatter: function (value) {
                    return "<span title='" + GetSourceName(value) + "'>" + GetSourceName(value) + "</span>";
                }
            });
            columns.push({
                title: '归属部门', field: 'BelongDepart', width: '100px', formatter: function (value) {
                    return "<span title='" + GetUpClientDepName(value) + "'>" + GetUpClientDepName(value) + "</span>";
                }
            });
            //columns.push({ title: '入库时间', field: 'InHouseTime', width: '130px', formatter: DateTimeFormatter });
            //columns.push({ title: '销售价', field: 'SalePrice', width: '60px' });
            columns.push({
                title: '单价', field: 'UnitPrice', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            //columns.push({ title: '数量', field: 'Numbers', width: '60px' });
            //{ title: '成本价', field: 'CostPrice', width: '60px' },
            //columns.push({ title: '门店价', field: 'TradePrice', width: '60px' });
            //columns.push({ title: '入库状态', field: 'InCargoStatus', width: '60px', formatter: function (val, row, index) { if (val == "0") { return "未入库"; } else if (val == "1") { return "已入库"; } else { return "未入库"; } } });
            //columns.push({ title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter });

            //出库列表
            $('#outDg').datagrid({
                width: '100%',
                height: '200px',
                title: '移库列表', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
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
            $('#DisplayNum').val(0);
            //$('#DisplayPiece').numberbox(0);
            $('#ASpecs').textbox('textbox').keydown(function (e) {
                if (e.keyCode == 13) {
                    dosearch();
                }
            });

            $.ajaxSetup({ async: true });
            $.getJSON("../Product/productApi.aspx?method=QueryAllProductSource", function (data) {
                ProductSource = data;
            })
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
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#HAID').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    $('#HAID').combobox('reload', url);
                    var url = '../Client/clientApi.aspx?method=QueryAllUpClientDep&type=1&houseID=' + rec.HouseID;
                    $('#ABelongDepart').combobox('reload', url);
                    $('#ABelongDepart').combobox('select', '-1');
                }
            });
            $('#HouseID').combobox({
                url: '../House/houseApi.aspx?method=QueryDLTHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#HouseName').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    $('#HouseName').combobox('reload', url);
                }
            });
            $('#tdSaveIncomming').hide();
            if (<%=UserInfor.HouseID%>== "64") {
                $('#tdSaveIncomming').show();
                $("#IsSaveIncomming").prop('checked', 'checked');
                var IsSaveIncomming = document.getElementById("IsSaveIncomming");
                IsSaveIncomming.value = 1;
            }
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            $('#HAID').combobox('clear');
            var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=<%=UserInfor.HouseID%>';
            $('#HAID').combobox('reload', url);
            $('#HAID').combobox('textbox').bind('focus', function () { $('#HAID').combobox('showPanel'); });
            $('#APID').combobox('textbox').bind('focus', function () { $('#APID').combobox('showPanel'); });
            $('#ASID').combobox('textbox').bind('focus', function () { $('#ASID').combobox('showPanel'); });
        });

        //查询
        function dosearch() {
            if ($("#AHouseID").combobox('getValue') == undefined || $("#AHouseID").combobox('getValue') == '') {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择区域大仓！', 'warning');
                return;
            }
            <%--if ($("#HAID").combobox('getValue') == undefined || $("#HAID").combobox('getValue') == '') {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择所在仓库！', 'warning');
                return;
            }--%>

            if ($("#HAID").combobox('options').required) {
                if ($("#HAID").combobox('getValue') == undefined || $("#HAID").combobox('getValue') == '') {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择所在仓库！', 'warning');
                    return;
                }
            }
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
                //ProductName: $('#AProductName').val(),
                ContainerCode: $('#AContainerCode').val(),
                //FlatRatio: $('#AFlatRatio').val(),
                Figure: $('#AFigure').val(),
                BatchYear: $('#ABatchYear').combobox('getValue'),
                //HubDiameter: $('#AHubDiameter').val(),
                LoadIndex: $('#ALoadIndex').val(),
                HAID: $("#HAID").combobox('getValue'),
                BelongDepart: $("#ABelongDepart").combobox('getValue'),//归属部门
                HouseID: $("#AHouseID").combobox('getValue'),//仓库ID
                IsLockStock:0
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
                <td style="text-align: right;">规格:
                </td>
                <td>
                    <input id="ASpecs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 100px">
                </td>
                <td id="AModelTd" style="text-align: right;">型号:
                </td>
                <td>
                    <input id="AModel" class="easyui-textbox" data-options="prompt:'请输入产品型号'" style="width: 100px">
                </td>
                <%--<td style="text-align: right;">产品名称:
                </td>
                <td>
                    <input id="AProductName" class="easyui-textbox" data-options="prompt:'请输入产品名称'" style="width: 100px">
                </td>--%>
                <td style="text-align: right;">货位代码:
                </td>
                <td>
                    <input id="AContainerCode" class="easyui-textbox" data-options="prompt:'请输入货位代码'" style="width: 100px" />
                </td>
                <td style="text-align: right;">一级产品:
                </td>
                <td>
                    <input id="APID" class="easyui-combobox" style="width: 100px;"
                        panelheight="auto" />
                </td>
                <td style="text-align: right;">归属部门:
                </td>
                <td>
                    <input id="ABelongDepart" class="easyui-combobox" style="width: 100px;" data-options="valueField:'ID',textField:'DepName',editable:false" />
                    <%--<select class="easyui-combobox" id="ABelongDepart" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="-1">全部</option>
                        <option value="0">RE渠道销售部</option>
                        <option value="1">OE渠道销售部</option>
                    </select>--%>
                </td>
                <td style="text-align: right;">区域大仓:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;" readonly="readonly" data-options="required:true"
                        panelheight="auto" />
                </td>
            </tr>
            <tr>
                <td id="AFigureTd" style="text-align: right;">花纹:
                </td>
                <td>
                    <input id="AFigure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 100px">
                </td>
                <td style="text-align: right;">年周:
                </td>
                <td>
                    <%--   <input id="ABatch" class="easyui-numberbox" data-options="prompt:'请输入批次'" style="width: 80px">--%>
                    <select class="easyui-combobox" id="ABatchYear" style="width: 100px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="21">21年</option>
                        <option value="20">20年</option>
                        <option value="19">19年</option>
                        <option value="18">18年</option>
                        <option value="17">17年</option>
                    </select>
                </td>
                <td id="AGoodsCodeTd" style="text-align: right;">货品代码:
                </td>
                <td>
                    <input id="AGoodsCode" class="easyui-textbox" data-options="prompt:'请输入货品代码'" style="width: 100px">
                </td>
                <td style="text-align: right;">二级产品:
                </td>
                <td>
                    <input id="ASID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'TypeID',textField:'TypeName'" />
                </td>
                <td class="ALoadIndexTd" style="text-align: right;">载重指数:
                </td>
                <td class="ALoadIndexTd">
                    <input id="ALoadIndex" class="easyui-numberbox" data-options="min:0,precision:2" style="width: 100px">
                </td>
                <td style="text-align: right;">所在仓库:
                </td>
                <td>
                    <input id="HAID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'AreaID',textField:'Name',required:true"
                        panelheight="auto" />
                </td>

            </tr>
        </table>
    </div>
    <input type="hidden" id="DisplayNum" />
    <table id="dg" class="easyui-datagrid">
    </table>
    <table id="outDg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="plOutCargo()">
            &nbsp;拉上移库单&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
                iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;拉下移库单&nbsp;</a>
    </div>
    <!--Begin 出库操作-->
    <div id="saPanel2" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <div id="saPanel">
            <form id="fmDep" class="easyui-form" method="post">
                <table>
                    <tr>
                        <td style="text-align: right;">移库备注:
                        </td>
                        <td>
                            <textarea name="Memo" id="Memo" rows="2" placeholder="请输入移库备注" style="width: 400px; resize: none"></textarea>
                        </td>
                        <td style="text-align: right;">区域大仓:</td>
                        <td>
                            <input id="HouseID" name="HouseID" class="easyui-combobox" style="width: 100px;" data-options="required:true" /></td>
                        <td style="text-align: right;">目标仓库:</td>
                        <td>
                            <input id="HouseName" name="NewAreaID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'AreaID',textField:'Name',required:true,editable:false" /></td>
                        <td style="text-align: right;">物流公司:
                        </td>
                        <td>
                            <input name="LogisID" id="ALogisID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'ID',textField:'LogisticName',url:'../systempage/sysService.aspx?method=QueryAllLogistic'" />
                        </td>
                        <td style="text-align: right;">移库数量:</td>
                        <td>
                            <input id="DisplayPiece" name="MoveNum" class="easyui-numberbox" data-options="min:0,precision:0" style="width: 80px" /></td>
                        <td id="tdSaveIncomming">
                            <input type="checkbox" class="easyui-checkbox" id="IsSaveIncomming" name="IsSaveIncomming" onclick="checkbox(this)" />
                            <label for="IsSaveIncomming">是否自动生成来货单</label></td>
                        <td><a href="#" id="btnSave" class="easyui-linkbutton" iconcls="icon-ok"
                            plain="false" onclick="saveOutCargo()">&nbsp;保&nbsp;存&nbsp;</a>
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 360px; height: 200px; padding: 2px 2px"
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
                            style="width: 200px;">
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="outOK()">&nbsp;确&nbsp;定&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <!--End 出库操作-->

    <script type="text/javascript">
        function checkbox(obj) {
            if (obj.checked) {
                obj.value = 1;
            } else {
                obj.value = 0;
            }
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
        function saveOutCargo() {
            var rows = $('#outDg').datagrid('getRows');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请拉上要移库的数据！', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定保存？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    $('#btnSave').linkbutton('disable');
                    var json = JSON.stringify(rows);

                    $('#fmDep').form('submit', {
                        url: 'orderApi.aspx?method=AddMoveOrderData',
                        contentType: "application/json;charset=utf-8", dataType: "json",
                        onSubmit: function (param) {
                            param.submitData = json;
                            param.HouseName = $('#HouseName').combobox('getText');
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
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                                location.reload();
                            } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning'); }
                        }
                    })
                }
            });
        }
        //删除出库的数据
        function dblClickDelCargo(Did) {
            var row = $("#outDg").datagrid('getData').rows[Did];

            var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
            var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
            n--;
            var pt = p - (Number(row.InPiece) - Number(row.Piece));
            $('#DisplayNum').val(Number(n));
            $('#DisplayPiece').numberbox('setValue', Number(pt));
            var title = "上订单     已拉上：" + n + "票，总件数：" + pt + " 件";
            $('#outDg').datagrid("getPanel").panel("setTitle", title);

            var index = $('#dg').datagrid('getRowIndex', row.ID);
            if (index >= 0) {
                var Trow = $("#dg").datagrid('getData').rows[index];
                Trow.Piece = Trow.InPiece;
                $('#dg').datagrid('updateRow', { index: index, row: Trow });
            }
            var index = $('#outDg').datagrid('getRowIndex', row);
            $('#outDg').datagrid('deleteRow', index);
        }
        //新增出库数据
        function outOK() {
            var row = $('#dg').datagrid('getSelected');
            if ($('#Numbers').val() == null || $('#Numbers').val() == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请输入拉上移库单数量！', 'warning');
                return;
            }
            if ($('#Numbers').val() < 1) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '拉上移库单数量必须大于0！', 'warning');
                return;
            }
            var Total = Number(row.Piece);
            var Aindex = $('#InIndex').val();
            var index = $('#outDg').datagrid('getRowIndex', row.ID);
            if (index < 0) {
                row.Piece = $('#Numbers').numberbox('getValue');
                $('#outDg').datagrid('appendRow', row);
                var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
                var p = $('#DisplayPiece').numberbox('getValue') == "" || isNaN($('#DisplayPiece').numberbox('getValue')) ? 0 : Number($('#DisplayPiece').numberbox('getValue'));
                n++;
                var pt = p + Number($('#Numbers').numberbox('getValue'));
                $('#DisplayNum').val(Number(n));
                $('#DisplayPiece').numberbox('setValue', Number(pt));

                var title = "上订单     已拉上：" + n + "票，总件数：" + pt + " 件";
                $('#outDg').datagrid("getPanel").panel("setTitle", title);
                closedgShowData();
                if (Total != Number($('#Numbers').numberbox('getValue'))) {
                    row.Piece = Total - Number($('#Numbers').numberbox('getValue'));
                } else {
                    row.Piece = 0;
                }
                $('#dg').datagrid('updateRow', { index: Aindex, row: row });
            } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单列表已存在该产品，请先删除再添加！', 'warning'); }
        }
        //关闭
        function closedgShowData() {
            $('#dlg').dialog('close');
        }

        ///出库
        function dblClickOutCargo(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row.Piece == 0) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '在库件数为0', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '拉上' + row.TypeName + ' ' + row.Specs + ' ' + row.Figure + ' 不得超过：' + row.Piece + '条');
                $('#InPiece').val(row.Piece);
                $('#InIndex').val($('#dg').datagrid('getRowIndex', row));
                $('#Numbers').numberbox('setValue', '');
                $('#Numbers').numberbox({
                    min: 0,
                    max: row.Piece,
                    precision: 0
                });
                $('#Numbers').numberbox().next('span').find('input').focus();
            }
        }
    </script>
</asp:Content>
