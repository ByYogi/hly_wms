<%@ Page Title="微信商城订单" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wxOrderManager.aspx.cs" Inherits="Cargo.Order.wxOrderManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/easy/js/datagrid-groupview.js" type="text/javascript"></script>
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
        }
        $(window).resize(function () {
            adjustment();
        });
        function adjustment() {
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true);
            $('#dg').datagrid({ height: height });
        }
        $(document).ready(function () {
            $('#dg').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2],
                rownumbers: true,
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    //{ title: '', field: 'ID', checkbox: true, width: '30px' },
                    {
                        title: '订单类型', field: 'OrderType', width: '60px', formatter: function (val, row, index) {
                            if (val == "2") { return "<span title='微信商城'>微信商城</span>"; }
                            else if (val == "3") { return "<span title='APP商城'>APP商城</span>"; }
                            else if (val == "4") { return "<span title='小程序'>小程序</span>"; }
                            else { return "<span title='微信商城'>微信商城</span>"; }
                        }
                    },
                    {
                        title: '订单号', field: 'OrderNo', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    { title: '下单时间', field: 'CreateDate', width: '130px', formatter: DateTimeFormatter },
                    {
                        title: '付款状态', field: 'PayStatus', width: '60px',
                        formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='未付款'>未付款</span>"; }
                            else if (val == "1") { return "<span title='已付款'>已付款</span>"; }
                            else if (val == "2") { return "<span title='申请退款'>申请退款</span>"; }
                            else if (val == "3") { return "<span title='已退款'>已退款</span>"; }
                            else { return ""; }
                        }
                    },
                    {
                        title: '付款方式', field: 'PayWay', width: '60px',
                        formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='微信支付'>微信支付</span>"; }
                            else if (val == "1") { return "<span title='额度支付'>额度支付</span>"; }
                            else if (val == "2") { return "<span title='积分兑换'>积分兑换</span>"; }
                            else { return ""; }
                        }
                    },
                    {
                        title: '订单状态', field: 'OrderStatus', width: '60px',
                        formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='客户下单'>客户下单</span>"; }
                            else if (val == "1") { return "<span title='仓库确认'>仓库确认</span>"; }
                            else if (val == "2") { return "<span title='仓库发货'>仓库发货</span>"; }
                            else if (val == "3") { return "<span title='商品在途'>商品在途</span>"; }
                            else if (val == "4") { return "<span title='客户收货'>客户收货</span>"; }
                            else { return ""; }
                        }
                    },
                    {
                        title: '收货人', field: 'Name', width: '60px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '收货地址', field: 'AcceptAddress', width: '300px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '手机号码', field: 'Cellphone', width: '90px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '数量', field: 'OrderNum', width: '50px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '价格', field: 'OrderPrice', width: '50px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '直减', field: 'CutEntry', width: '50px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '商品名称', field: 'Title', width: '200px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '销售类型', field: 'SaleType', width: '60px', formatter: function (val, row, index) {
                            if (val == "1") { return "<span title='【天】'>【天】</span>"; }
                            else if (val == "3") { return "<span title='【限】'>【限】</span>"; }
                            else if (val == "4") { return "<span title='【积】'>【积】</span>"; }
                            else { return "<span title='正价'>正价</span>"; }
                        }
                    },
                    {
                        title: '商品评价', field: 'GoodEvaluate', width: '70px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '物流评价', field: 'LogisEvaluate', width: '70px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '评价内容', field: 'EvaluateMemo', width: '150px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '备注', field: 'Memo', width: '150px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                ]],
                groupField: 'OrderNo',
                view: groupview,
                rowStyler: function (index, row) {
                    if (row.PayStatus == "2") {
                        return "background-color:#ffeded";
                    }
                },
                groupFormatter: function (value, rows) {
                    if (rows[0].HouseID == "83") {
                        return "订单号：" + value + "- 总数量：" + rows[0].Piece + " - 总金额：" + rows[0].TotalCharge + " - 配送费：" + rows[0].TransitFee + " - 优惠券：" + rows[0].CouponMoney + " - " + rows[0].RuleTitle;
                     
                    } else {
                        if (rows[0].PayWay == "2") {
                            //积分兑换
                            var total = Number(rows[0].Consume) * rows[0].Piece;
                            return "订单号：" + value + "- 总数量：" + rows[0].Piece + " - 消费积分：" + total;
                        } else {
                            if (rows[0].OrderStatus == "0") {
                                return "订单号：" + value + "- 总数量：" + rows[0].Piece + " - 总金额：" + rows[0].TotalCharge + " - 配送费：" + rows[0].TransitFee + " - 优惠券：" + rows[0].CouponMoney + " - " + rows[0].RuleTitle + "&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"javascript:cargoSure(" + value + ");\" class=\"easyui-linkbutton l-btn l-btn-big\" style=\" color:red;padding: 2px;\" >&nbsp;仓库确认&nbsp;</a>&nbsp;&nbsp;<a href=\"javascript:modifyOrderCharge(" + value + "," + rows[0].Piece + "," + rows[0].TotalCharge + ");\" class=\"easyui-linkbutton l-btn l-btn-big\" style=\" color:red;padding: 2px;\" >&nbsp;修改金额&nbsp;</a>&nbsp;&nbsp;<a href=\"javascript:DelWxOrder(" + value + ");\" class=\"easyui-linkbutton l-btn l-btn-big\" style=\" color:red;padding: 2px;\" >&nbsp;删除订单&nbsp;</a>";
                            } else {
                                return "订单号：" + value + "- 总数量：" + rows[0].Piece + " - 总金额：" + rows[0].TotalCharge + " - 配送费：" + rows[0].TransitFee + " - 优惠券：" + rows[0].CouponMoney + " - " + rows[0].RuleTitle;
                            }
                        }
                    }
                }
            });
            var datenow = new Date();
            //$('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#StartDate').datebox('setValue', getNowFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));

            //所在仓库
            $('#HID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });
            $('#HID').combobox('textbox').bind('focus', function () { $('#HID').combobox('showPanel'); });
            $('#HID').combobox('setValue', '<%=UserInfor.HouseID%>');
        });
        //确定删除订单
        function DelWxOrder(orderno) {
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定删除订单？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在删除中...' });
                    $.ajax({
                        url: 'orderApi.aspx?method=DeleteWeixinOrder',
                        type: 'post', dataType: 'json', data: { data: orderno },
                        success: function (text) {
                            $.messager.progress("close");
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单删除成功!', 'info');
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

        //确认订单
        function cargoSure(orderno) {
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定确认订单？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在确认订单中...' });
                    $.ajax({
                        url: 'orderApi.aspx?method=makeSureWxOrder',
                        type: 'post', dataType: 'json', data: { data: orderno },
                        success: function (text) {
                            $.messager.progress("close");
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '仓库确认成功!', 'info');
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
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=QueryWeixinOrderInfo';
            $('#dg').datagrid('load', {
                OrderNo: $('#OrderNo').textbox('getValue'),
                PayStatus: $("#PayStatus").combobox('getValue'),
                PayWay: $("#PayWay").combobox('getValue'),
                HID: $("#HID").combobox('getValue'),
                OrderStatus: $("#OrderStatus").combobox('getValue'),
                Name: $('#Name').textbox('getValue'),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue')
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
                    <input id="OrderNo" class="easyui-textbox" data-options="prompt:'请输入订单号'" style="width: 100px">
                </td>
                <td style="text-align: right;">付款状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="PayStatus" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">未付款</option>
                        <option value="1">已付款</option>
                        <option value="2">申请退款</option>
                        <option value="3">已退款</option>
                    </select>
                </td>

                <td style="text-align: right;">订单状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="OrderStatus" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">客户下单</option>
                        <option value="1">仓库确认</option>
                        <option value="2">仓库发货</option>
                        <option value="3">商品在途</option>
                        <option value="4">客户收货</option>
                    </select>
                </td>

                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="HID" class="easyui-combobox" style="width: 100px;"
                        panelheight="auto" />
                </td>

            </tr>
            <tr>
                <td style="text-align: right;">客户名:
                </td>
                <td>
                    <input id="Name" class="easyui-textbox" data-options="prompt:'请输入客户名'" style="width: 100px">
                </td>
                <td style="text-align: right;">付款方式:
                </td>
                <td>
                    <select class="easyui-combobox" id="PayWay" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">微信支付</option>
                        <option value="1">额度支付</option>
                    </select>
                </td>
                <td style="text-align: right;">下单时间:
                </td>
                <td colspan="3">
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px">【天】:天天特价【限】:限时促销
                </td>
            </tr>

        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<%--<a href="#" class="easyui-linkbutton" iconcls="icon-database_go" plain="false" onclick="moveContainer()">
            &nbsp;移&nbsp;库&nbsp;</a>--%>
    </div>

    <div id="dlg" class="easyui-dialog" style="width: 500px; height: 200px; padding: 2px 2px"
        closed="true" buttons="#dlg-buttons">
        <input type="hidden" id="orderno" />
        <input type="hidden" id="hPiece" />
        <input type="hidden" id="hCharge" />
        <table>
            <tr>
                <td style="text-align: right;">修改为金额:
                </td>
                <td>
                    <input name="TotalCharge" id="TotalCharge" class="easyui-numberbox" data-options="min:0,precision:2" style="width: 150px;">
                </td>
            </tr>

        </table>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">&nbsp;&nbsp;确&nbsp;定&nbsp;&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;&nbsp;取&nbsp;消&nbsp;&nbsp;</a>
    </div>
    <script type="text/javascript">
        function saveItem() {
            var rows = $('#TotalCharge').numberbox('getValue');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请输入修改后的总金额！', 'warning');
                return;
            }
            var piece = Number($('#hPiece').val());
            var charge = Number($('#hCharge').val());
            var orderno = $('#orderno').val();
            if (Number(rows) < charge) {
                var differ = charge - Number(rows);
                if (Number(differ) > piece * 10) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '优惠金额超过定额，请向领导申请！', 'warning');
                    return;
                }
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定修改？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'orderApi.aspx?method=modifyOrderCharge',
                        type: 'post', dataType: 'json', data: { total: rows, orderno: orderno },
                        success: function (text) {
                            $.messager.progress("close");
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
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
        //修改订单金额 
        function modifyOrderCharge(orderno, piece, charge) {
            $('#dlg').dialog('open').dialog('setTitle', '修改订单号：' + orderno + " 总金额");
            $('#orderno').val(orderno);
            $('#hPiece').val(piece);
            $('#hCharge').val(charge);

            $('#TotalCharge').numberbox('clear');
        }
    </script>
</asp:Content>
