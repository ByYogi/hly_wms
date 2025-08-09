<%@ Page Title="预录单" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PreAddOrder.aspx.cs" Inherits="Cargo.Order.PreAddOrder" %>

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

            //如果是广州、广东、揭阳的用户开放区域大仓选项
            if (HID == "9" || HID == "44" || HID == "45") {
                $('#AHouseID').combobox('enable');
            } else {
                $('#AHouseID').combobox('disable');
            }

            $.ajaxSetup({ async: true });
            $.getJSON("../Product/productApi.aspx?method=QueryAllProductSource", function (data) {
                ProductSource = data;
            })
            //定义全局ThrowGood值用于判断显示价格
            ThrowGoodValue = -1;
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
        $(document).ready(function () {
            var columns = [];
            columns.push({ title: '', field: 'ID', checkbox: true, width: '2%' });
            columns.push({
                title: '产品名称', field: 'ProductName', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '规格', field: 'Specs', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '富添盛编码', field: 'GoodsCode', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '商贸编码', field: 'Model', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '长和编码', field: 'Figure', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '祺航编码', field: 'Size', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '在库件数', field: 'Piece', width: '5%', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '批次', field: 'Batch', width: '5%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });

            //columns.push({
            //    title: '销售价', field: 'SalePrice', width: '5%', align: 'right', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});
            columns.push({
                title: '对客户销售价', field: 'CostPriceStore', width: '5%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在仓库', field: 'FirstAreaName', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品类型', field: 'TypeName', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '门店价', field: 'TradePrice', width: '5%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
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
            columns.push({ title: '入库时间', field: 'InHouseTime', width: '9%', formatter: DateTimeFormatter });

            $('#dg').datagrid({
                width: '100%',
                //height: '50%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2],
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
                title: '', field: 'ID', checkbox: true, width: '2%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品名称', field: 'ProductName', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '规格', field: 'Specs', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '富添盛编码', field: 'GoodsCode', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '商贸编码', field: 'Model', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '长和编码', field: 'Figure', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '祺航编码', field: 'Size', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '出库件数', field: 'Piece', width: '5%', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '批次', field: 'Batch', width: '5%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });

            columns.push({
                title: '销售价', field: 'CostPriceStore', width: '5%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在仓库', field: 'FirstAreaName', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品类型', field: 'TypeName', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '门店价', field: 'TradePrice', width: '5%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
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
                title: '备注', field: 'RuleTitle', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({ title: '入库时间', field: 'InHouseTime', width: '9%', formatter: DateTimeFormatter });
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
                title: '', field: 'ID', checkbox: true, width: '2%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品名称', field: 'ProductName', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '规格', field: 'Specs', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '富添盛编码', field: 'GoodsCode', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '商贸编码', field: 'Model', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '长和编码', field: 'Figure', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '祺航编码', field: 'Size', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '出库件数', field: 'Piece', width: '5%', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '批次', field: 'Batch', width: '5%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });

            columns.push({
                title: '销售价', field: 'CostPriceStore', width: '5%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在仓库', field: 'FirstAreaName', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品类型', field: 'TypeName', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '门店价', field: 'TradePrice', width: '5%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '备注', field: 'RuleTitle', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({ title: '入库时间', field: 'InHouseTime', width: '9%', formatter: DateTimeFormatter });
            //订单列表
            $('#dgSave').datagrid({
                width: '100%',
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
                onDblClickRow: function (index, row) { }
            });
            //所在仓库
            $('#HID').combobox({ url: '../House/houseApi.aspx?method=CargoPermisionHouse', valueField: 'HouseID', textField: 'Name' });

            //一级产品
            $('#APID').combobox({
                url: '../Product/productApi.aspx?method=QueryALLOneProductType', valueField: 'TypeID', textField: 'TypeName',
                onSelect: function (rec) {
                    $('#ASID').combobox('clear');
                    var url = '../Product/productApi.aspx?method=QueryALLOneProductType&PID=' + rec.TypeID;
                    $('#ASID').combobox('reload', url);
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
            //客户姓名
            $('#AAcceptPeople').combobox({
                valueField: 'ClientNum', textField: 'Boss', delay: '10',
                url: '../Client/clientApi.aspx?method=AutoCompleteClient',
                onSelect: onAcceptAddressChanged,
                required: false
            });
            //$('#APayClientNum').combobox({ valueField: 'ClientNum', textField: 'Boss', delay: '10', url: '../Client/clientApi.aspx?method=AutoCompleteClient' });
            bindMethod();
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#HAID').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    $('#HAID').combobox('reload', url);
                }
            });
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            $('#HAID').combobox('clear');
            var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=<%=UserInfor.HouseID%>';
            $('#HAID').combobox('reload', url);
            $('#HAID').combobox('textbox').bind('focus', function () { $('#HAID').combobox('showPanel'); });
            $('#APID').combobox('textbox').bind('focus', function () { $('#APID').combobox('showPanel'); });
            $('#ASID').combobox('textbox').bind('focus', function () { $('#ASID').combobox('showPanel'); });
            $('#HAID').combobox('setValue', '2839');

        });

        //查询
        function dosearch() {
            if ($("#AHouseID").combobox('getValue') == undefined || $("#AHouseID").combobox('getValue') == '') {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择区域大仓！', 'warning');
                return;
            }
            if ($("#HAID").combobox('getValue') == undefined || $("#HAID").combobox('getValue') == '') {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择所在仓库！', 'warning');
                return;
            }
            var checkVal = 0;
            $("#toolbar :checkbox").each(function (i) {
                var isCheck = $(this).prop("checked");
                if ('checked' == isCheck || isCheck) {
                    checkVal = $(this).val();
                    return true;
                }
            });
            if (checkVal == 0) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择订单类型！', 'warning');
                return;
            }
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=PreQueryALLProduct';
            $('#dg').datagrid('load', {
                GoodsCode: $('#AGoodsCode').val(),
                PID: $("#APID").combobox('getValue'),//一级产品
                SID: $("#ASID").combobox('getValue'),//二级产品
                ProductName: $('#AProductName').val(),
                HAID: $("#HAID").combobox('getValue'),
                PriceType: checkVal,
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
    <div id="tbDepmanifest" class="easyui-tabs" data-options="fit:true">
        <div title="货物查询" data-options="iconCls:'icon-search'">

            <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
                <table>
                    <tr>
                        <td style="text-align: right;">产品名称:
                        </td>
                        <td>
                            <input id="AProductName" class="easyui-textbox" data-options="prompt:'请输入产品名称'" style="width: 120px">
                        </td>
                        <td id="AGoodsCodeTd" style="text-align: right;">富添盛编码:
                        </td>
                        <td>
                            <input id="AGoodsCode" class="easyui-textbox" data-options="prompt:'请输入富添盛编码'" style="width: 120px">
                        </td>
                        <td style="text-align: right;">一级产品:
                        </td>
                        <td>
                            <input id="APID" class="easyui-combobox" style="width: 80px;"
                                panelheight="auto" />
                        </td>
                        <td style="text-align: right;">二级产品:
                        </td>
                        <td>
                            <input id="ASID" class="easyui-combobox" style="width: 80px;" data-options="valueField:'TypeID',textField:'TypeName'" />
                        </td>
                        <td style="text-align: right;">区域大仓:
                        </td>
                        <td>
                            <input id="AHouseID" class="easyui-combobox" style="width: 100px;" data-options="required:true"
                                panelheight="auto" />
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
            <input type="hidden" id="DisplayPiece" />
            <table id="dg" class="easyui-datagrid">
            </table>
            <table id="outDg" class="easyui-datagrid">
            </table>
            <div id="toolbar">
                <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="plOutCargo()">
            &nbsp;添加上订单&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
                iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删除下订单&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;订单类型:&nbsp;<input class="OrderModel" type="checkbox" name="ThrowGood" id="changhe" value="5" /><label class="OrderModel" for="changhe">长和订单</label><input class="OrderModel" type="checkbox" name="ThrowGood" id="shangmao" value="6" /><label class="OrderModel" for="shangmao">商贸订单</label><input class="OrderModel" type="checkbox" name="ThrowGood" id="qihang" value="10" /><label class="OrderModel" for="qihang">祺航订单</label><input class="OrderModel" type="checkbox" name="ThrowGood" id="qita" value="7" /><label class="OrderModel" for="qita">其它订单</label>
            </div>
            <!--Begin 出库操作-->

            <div id="dlg" class="easyui-dialog" style="width: 420px; height: 200px; padding: 2px 2px"
                closed="true" buttons="#dlg-buttons">
                <form id="fm" class="easyui-form" method="post">
                    <input type="hidden" id="InPiece" />
                    <input type="hidden" id="InIndex" />
                    <table>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">拉上订单数量：
                            </td>
                            <td>
                                <input name="Numbers" id="Numbers" class="easyui-numberbox" data-options="min:0,precision:0"
                                    style="width: 200px;">
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">销售价：</td>
                            <td>
                                <input name="ActSalePrice" id="ActSalePrice" class="easyui-numberbox" data-options="min:0,precision:2"
                                    style="width: 200px;">
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">备注：</td>
                            <td>
                                <input name="RuleTitle" id="RuleTitle" class="easyui-textbox" data-options="prompt:'可输入产品订单备注'" style="width: 200px">
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

        </div>
        <div title="保存订单" data-options="iconCls:'icon-page_add'">
            <form id="fmDep" class="easyui-form" method="post">
                <input type="hidden" name="SaleManName" id="SaleManName" />
                <input type="hidden" name="SaleCellPhone" id="SaleCellPhone" />
                <input type="hidden" name="HouseCode" id="HouseCode" />
                <input type="hidden" name="HouseID" id="HouseID" />
                <input type="hidden" name="ONum" id="ONum" />
                <input type="hidden" name="OutNum" id="OutNum" />
                <input type="hidden" name="ClientNum" id="ClientNum" />
                <input type="hidden" id="HiddenAcceptPeople" />
                <input type="hidden" id="HiddenClientNum" />
                <input type="hidden" id="ClientType" name="ClientType" />
                <input type="hidden" id="HiddenThrowGood" name="ThrowGood" />

                <div id="saPanel">
                    <table style="width: 100%">
                        <tr>

                            <td style="color: Red; font-weight: bolder; text-align: right;">出发站:
                            </td>
                            <td>
                                <input name="Dep" id="ADep" class="easyui-textbox" style="width: 80px" data-options="required:false ">
                            </td>
                            <td style="color: Red; font-weight: bolder; text-align: right;">到达站:
                            </td>
                            <td>
                                <input name="Dest" id="ADest" class="easyui-combobox" style="width: 80px;" data-options="valueField:'CityName',textField:'CityName',url:'../systempage/sysService.aspx?method=QueryAllCity',required:false " />
                            </td>
                            <td style="text-align: right;">收货人:
                            </td>
                            <td>
                                <input id="AAcceptPeople" style="width: 110px;" class="easyui-combobox AcceptPeople"/>
                                <input type="hidden" name="AcceptPeople" id="HiddenAAcceptPeople" />
                            </td>
                            <td style="text-align: right;">公司名称:
                            </td>
                            <td>
                                <input name="AcceptUnit" id="AAcceptUnit" class="easyui-textbox" style="width: 100px;"  data-options="required:true "/>
                            </td>

                            <td style="text-align: right;">收货地址:
                            </td>
                            <td>
                                <input name="AcceptAddress" id="AAcceptAddress" style="width: 120px;" class="easyui-textbox" />
                            </td>
                            <td style="text-align: right;">电话:
                            </td>
                            <td>
                                <input name="AcceptTelephone" id="AAcceptTelephone" class="easyui-textbox"
                                    style="width: 100px;" />
                            </td>
                            <td style="text-align: right;">手机:
                            </td>
                            <td>
                                <input name="AcceptCellphone" id="AAcceptCellphone" class="easyui-textbox" data-options="required:false" style="width: 100px;" />
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
                            <td style="text-align: right;">保险费用:
                            </td>
                            <td>
                                <input name="InsuranceFee" id="InsuranceFee" data-options="min:0,precision:2" class="easyui-numberbox"
                                    style="width: 150px;" />
                            </td>
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
                                <select class="easyui-combobox" id="CheckOutType" name="CheckOutType" style="width: 60px;" panelheight="auto" editable="false">
                                    <option value="2">月结</option>
                                    <option value="0">现付</option>
                                    <option value="4">代收</option>
                                </select>
                            </td>
                            <td style="text-align: right;">物流:
                            </td>
                            <td>
                                <input name="LogisID" id="ALogisID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'ID',textField:'LogisticName',url:'../systempage/sysService.aspx?method=QueryAllLogistic'" />
                            </td>
                            <td style="text-align: right;">物流费:
                            </td>
                            <td>
                                <input name="DeliveryFee" id="DeliveryFee" data-options="min:0,precision:2" class="easyui-numberbox"
                                    style="width: 90px;" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">总件数:
                            </td>
                            <td>
                                <input name="Piece" id="APiece" class="easyui-numberbox" data-options="min:0,precision:0,required:true,disabled:true"
                                    style="width: 80px;" />
                            </td>
                            <td style="text-align: right;">开单员:
                            </td>
                            <td>
                                <input name="CreateAwb" id="CreateAwb" class="easyui-textbox" readonly="true" style="width: 80px;" />
                            </td>
                            <td style="text-align: right;">开单时间:
                            </td>
                            <td colspan="3">
                                <input name="CreateDate" id="CreateDate" class="easyui-datetimebox" data-options="formatter:AllDateTime"
                                    readonly="true" style="width: 150px;" />
                            </td>
                            <td style="text-align: right;">业务员:
                            </td>
                            <td>
                                <input name="SaleManID" id="SaleManID" class="easyui-combobox" style="width: 120px;"
                                    data-options="url: 'orderApi.aspx?method=QueryUserByDepCode',valueField: 'LoginName',textField: 'UserName', onSelect: onSaleManIDChanged,required:false" />
                            </td>
                            <td style="text-align: right;"></td>
                            <td></td>
                            <td style="text-align: right;"></td>
                            <td>
                                <%--<input name="PayClientNum" id="APayClientNum" style="width: 100px;" class="easyui-combobox" />--%>

                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">备注:
                            </td>
                            <td colspan="7">
                                <textarea name="Remark" id="ARemark" rows="3" placeholder="请输入订单备注或改价原因" style="width: 100%; height: 25px; resize: none"></textarea>
                            </td>
                            <td colspan="2" style="display: none">
                                <input id="HID" class="easyui-combobox" style="width: 100px;" />
                            </td>
                            <td colspan="6" style="text-align: right;">
                                <a href="#" id="btnSave" class="easyui-linkbutton" iconcls="icon-ok"
                                    plain="false" onclick="saveOutCargo()">&nbsp;保&nbsp;存&nbsp;订&nbsp;单</a>
                                &nbsp;&nbsp;
                                <a href="#" class="easyui-linkbutton" id="undo" iconcls="icon-clear" onclick="reset()">&nbsp;重&nbsp;置&nbsp;</a>
                            </td>
                        </tr>

                        <%--<tr>
                            <td colspan="3" id="OrderModel" class="OrderModel">
                                <input class="OrderModel" type="checkbox" name="ThrowGood" id="changhe" value="5" />
                                <label class="OrderModel" for="changhe">长和订单</label>
                                <input class="OrderModel" type="checkbox" name="ThrowGood" id="shangmao" value="6" />
                                <label class="OrderModel" for="shangmao">商贸订单</label>
                                <input class="OrderModel" type="checkbox" name="ThrowGood" id="qihang" value="10" />
                                <label class="OrderModel" for="qihang">祺航订单</label>
                                <input class="OrderModel" type="checkbox" name="ThrowGood" id="qita" value="7" />
                                <label class="OrderModel" for="qita">其它订单</label>
                            </td>
                            <td colspan="6" style="text-align: right;">
                                <a href="#" id="btnSave" class="easyui-linkbutton" iconcls="icon-ok"
                                    plain="false" onclick="saveOutCargo()">&nbsp;保&nbsp;存&nbsp;订&nbsp;单</a>
                                &nbsp;&nbsp;
                                <a href="#" class="easyui-linkbutton" id="undo" iconcls="icon-clear" onclick="reset()">&nbsp;重&nbsp;置&nbsp;</a>
                            </td>
                        </tr>--%>
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
        $(function () {
            $('#ThrowGood').find('input[type=checkbox]').bind('click', function () {
                var id = $(this).attr("id");
                if (this.checked) {
                    var value = $(this).val();
                    ThrowGoodValue = value;
                    $("#ThrowGood").find('input[type=checkbox]').not(this).attr("checked", false);
                    if (value == 9) {
                        $("#TransportFee").numberbox('setValue', '0.00');
                        $("#TotalCharge").numberbox('setValue', '0.00');
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
        $(function () {
            $('#toolbar').find('input[type=checkbox]').bind('click', function () {
                var id = $(this).val();
                $("#HiddenThrowGood").val(id);
                if (this.checked) {
                    $("#toolbar").find('input[type=checkbox]').not(this).attr("checked", false);
                }
            });
        })
        //$(function () {
        //    $('.OrderModel').find('input[type=checkbox]').bind('click', function () {
        //        var id = $(this).attr("id");
        //        console.log(id);
        //        if (this.checked) {
        //            $(".OrderModel").find('input[type=checkbox]').not(this).attr("checked", false);
        //        }
        //        $("#" + id).prop('checked', true);
        //    });
        //})

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
            $("#InsuranceFee").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
        }
        function qh() {
            var t = Number($('#TransitFee').numberbox('getValue')) + Number($('#TransportFee').numberbox('getValue')) + Number($('#DeliveryFee').numberbox('getValue')) + Number($('#OtherFee').numberbox('getValue')) + Number($('#InsuranceFee').numberbox('getValue'));
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
        //收货人自动选择方法
        function onAcceptAddressChanged(item) {
            if (item) {
                if (item.ClientType == "3") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '该客户是逾期客户，请确认收到货款再发货', 'warning');
                }
                var AAcceptPeople = $('#AAcceptPeople').combobox('getValue');
                $('#ClientType').val(item.ClientType);

                $('#HiddenClientNum').val(item.ClientNum);
                $('#HiddenAcceptPeople').val(item.Boss);
                $('#HiddenAAcceptPeople').val(item.Boss);

                $('#AAcceptPeople').combobox('setValue', item.ClientNum);
                $('#AAcceptPeople').combobox('setText', item.Boss);
                $('#HiddenAAcceptPeople').val(item.Boss);
                $('#AAcceptUnit').textbox('setValue', item.ClientName);//ClientShortName
                $('#AAcceptAddress').textbox('setValue', item.Address);
                $('#AAcceptTelephone').textbox('setValue', item.Telephone);
                $('#AAcceptCellphone').textbox('setValue', item.Cellphone);
                $('#ClientNum').val(item.ClientNum);
                if (item.City != '') {
                    $('#ADest').combobox('setValue', item.City);
                }
            }
        }
        //保存订单
        function saveOutCargo() {
            var rows = $('#dgSave').datagrid('getRows');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要保存的数据！', 'warning'); return; }
            var saleMaanID = $('#SaleManID').combobox('getValue');
            var saleMaanName = $('#SaleManID').combobox('getText');
            <%--if ((saleMaanID == "" || saleMaanID == undefined) && saleMaanName != "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '此业务员无效！', 'warning'); return;
            }
            else if (saleMaanID == "" || saleMaanID == undefined) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择业务员！', 'warning'); return;
            }--%>

            var HID = "<%=UserInfor.HouseID%>";
            if (HID == "47") {
                var len = $("input[class='OrderModel']:checked").length;
                if (len < 1) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择订单类型！', 'warning'); return;
                }
            }
            $('#HouseCode').val(rows[0].HouseCode);
            $('#HouseID').val(rows[0].HouseID);

            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定保存？', function (r) {
                if (r) {
                    //$.messager.progress({ msg: '请稍后,正在保存中...' });
                    //$('#btnSave').linkbutton('disable');
                    var json = JSON.stringify(rows);

                    $('#fmDep').form('submit', {
                        url: 'orderApi.aspx?method=PreSaveOrderData',
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
                                if (dd[3] == "1") {
                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单库存数量不足，预订单下单成功！', 'info', function (fn) {
                                        location.reload();
                                    });
                                } else {
                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '下单成功！', 'info', function (fn) {
                                        location.reload();
                                    });
                                }
                            } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning'); }
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
                var SalePrice = Number(row.CostPriceStore);//销售价
                var NC = SalePrice * (Number(row.InPiece) - Number(row.Piece));
                $("#hiddenTransportFee").val(hiddenTransportFeeVal - NC);
                if (ThrowGoodValue != 9) {
                    $('#TransportFee').numberbox('setValue', tCharge - NC);
                }
                var title = "上订单     已拉上：" + n + "票，总件数：" + pt + " 件";
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
        function test() {
            alert(1);
        }
        var ISM = false;
        //新增出库数据
        function outOK() {
            var SalePrice = $('#ActSalePrice').numberbox('getValue');// Number(row.SalePrice);//销售价
            var row = $('#dg').datagrid('getSelected');
            //对产品进行价格管控
            <%--            if (row.TypeParentID == 1) {
                var sp = "<%=UserInfor.SpecialCreateAwb%>";
                if (sp == "0" || sp == '' || sp == undefined) {
                    //没有特殊下单权限，需要验证先进先出
                    var rows = $('#dg').datagrid('getRows');
                    for (var i = 0; i < rows.length; i++) {
                        var rw = rows[i];
                        if (rw.Specs == row.Specs && rw.Figure == row.Figure && rw.Model == row.Model && rw.BatchYear == row.BatchYear && rw.Source == row.Source) {
                            if (row.BatchWeek > rw.BatchWeek && rw.Piece > 0) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请优先出库周期早的产品！', 'warning');
                                return;
                            }
                        }
                    }
                }
            }--%>
            if (SalePrice == "0.00" || SalePrice == '' || SalePrice == undefined) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请输入业务员销售价格！', 'warning');
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
                row.Piece = $('#Numbers').numberbox('getValue');
                row.ActSalePrice = SalePrice;
                row.RuleTitle = $('#RuleTitle').val();
                $('#outDg').datagrid('appendRow', row);
                $('#dgSave').datagrid('appendRow', row);
                var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
                var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
                n++;
                var pt = p + Number($('#Numbers').numberbox('getValue'));
                $('#DisplayNum').val(Number(n));
                $('#DisplayPiece').val(Number(pt));
                $('#APiece').numberbox('setValue', Number(pt));
                var NC = SalePrice * Number($('#Numbers').numberbox('getValue'));
                $("#hiddenTransportFee").val(hiddenTransportFeeVal + NC);
                if (ThrowGoodValue != 9) {
                    $('#TransportFee').numberbox('setValue', tCharge + NC);
                }
                var title = "上订单     已拉上：" + n + "票，总件数：" + pt + " 件";
                $('#outDg').datagrid("getPanel").panel("setTitle", title);
                $('#dgSave').datagrid("getPanel").panel("setTitle", title);
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
            var SalePrice = Number(row.CostPriceStore);//销售价
            var NC = SalePrice * (Number(row.InPiece) - Number(row.Piece));
            $("#hiddenTransportFee").val(hiddenTransportFeeVal - NC);
            if (ThrowGoodValue != 9) {
                $('#TransportFee').numberbox('setValue', tCharge - NC);
            }
            var title = "上订单     已拉上：" + n + "票，总件数：" + pt + " 件";
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
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要拉上订单的数据！', 'warning');
                return;
            }
            if (Number(row.CostPriceStore) <= 0) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请先录入产品销售价格', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '拉上  ' + row.ProductName + ' 富添盛编码：' + row.GoodsCode);
                $('#InIndex').val($('#dg').datagrid('getRowIndex', row));
                $('#Numbers').numberbox('setValue', '');
                $('#RuleTitle').textbox('setValue', '');
                $('#ActSalePrice').numberbox('setValue', row.CostPriceStore);
                $('#SystemSalePrice').numberbox('setValue', row.CostPriceStore);

                $('#Numbers').numberbox({
                    min: 0,
                    precision: 0
                });
                $('#Numbers').numberbox().next('span').find('input').focus();

            }
        }
        //弹出定时关闭的消息框
        function alert_autoClose(title, msg, icon) {
            var interval;
            var time = 500;
            var x = 1;  //只接受整数
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
            var type = $("input:checkbox[name='ThrowGood']:checked").val();
            if (type == 5) {
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
                LODOP.ADD_PRINT_TEXT(41, 595, 100, 20, $('#AAcceptPeople').combobox('getText'));
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
                LODOP.ADD_PRINT_TEXT(70, 448, 64, 25, "出库件数");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
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


                LODOP.PRINT_DESIGN();
                LODOP.PREVIEW();
                location.reload();
            } else if (ty == 6) {

            } else {

            }

        }
    </script>

</asp:Content>
