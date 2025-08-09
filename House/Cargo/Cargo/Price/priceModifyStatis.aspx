<%@ Page Title="价格修改记录" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="priceModifyStatis.aspx.cs" Inherits="Cargo.Price.priceModifyStatis" %>

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
                idField: 'MID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    { title: '', field: 'MID', checkbox: true, width: '3%' },
                    {
                        title: '仓库名称', field: 'HouseName', width: '5%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '品牌', field: 'TypeName', width: '5%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '产品编码', field: 'ProductCode', width: '5%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },

                    {
                        title: '规格', field: 'Specs', width: '5%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },

                    {
                        title: '花纹', field: 'Figure', width: '5%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '载速', field: 'LoadIndex', width: '5%', formatter: function (value, row) {
                            return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                        }
                    },
                    {
                        title: '货品代码', field: 'GoodsCode', width: '5%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '修改人姓名', field: 'UserName', width: '5%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '修改前价格', field: 'OldPrice', width: '5%', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '修改后价格', field: 'NewPrice', width: '5%', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '改价类型', field: 'PriceType', width: '5%',
                        formatter: function (val, row, index) {
                            if (val == "0") { return "小程序价"; }
                            else if (val == "1") { return "进仓价"; }
                            else if (val == "2") { return "单价"; }
                            else if (val == "3") { return "门店价"; }
                            else if (val == "4") { return "成本价"; }

                        }
                    },
                    {
                        title: '修改系统', field: 'ModifySource', width: '5%',
                        formatter: function (val, row, index) {
                            if (val == "0") { return "智能仓储系统"; }
                            else if (val == "1") { return "供应商系统"; }

                        }
                    },
                    { title: '修改时间', field: 'OPDate', width: '8%', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { }//editItemByID(index);
            });
            //所在仓库
            $('#HID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
            });

            $('#HID').combobox('textbox').bind('focus', function () { $('#HID').combobox('showPanel'); });

        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'priceApi.aspx?method=QueryPriceModifyData';
            $('#dg').datagrid('load', {
                UserName: $('#UserName').val(),
                Specs: $("#Specs").val(),
                HID: $("#HID").combobox('getValue'),
                PriceType: $("#PriceType").combobox('getValue'),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
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
                <td style="text-align: right;">所属仓库:
                </td>
                <td style="width: 10%">
                    <input id="HID" class="easyui-combobox" style="width: 100%;" />
                </td>
                <td style="text-align: right;">修改人:
                </td>
                <td style="width: 10%">
                    <input id="UserName" class="easyui-textbox" data-options="prompt:'请输入修改人姓名'" style="width: 100%" />
                </td>

                <td style="text-align: right;">规格:
                </td>
                <td style="width: 10%">
                    <input id="Specs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 100%" />
                </td>

                <td style="text-align: right;">改价类型:
                </td>
                <td style="width: 10%">
                    <select class="easyui-combobox" id="PriceType" style="width: 100%;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">小程序价</option>
                        <option value="1">进仓价</option>
                        <option value="2">单价</option>
                        <option value="3">门店价</option>
                        <option value="4">成本价</option>
                    </select>
                </td>
                <td style="text-align: right;">修改时间:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px" />~
     <input id="EndDate" class="easyui-datebox" style="width: 100px" />
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="ExportShopOrderImport()" id="btnExportShopOrderImport">&nbsp;导&nbsp;出&nbsp;</a>
        <form runat="server" id="fm1">
            <asp:Button ID="ShopOrderImport" runat="server" Style="display: none;" Text="导出" OnClick="btnShopOrder_Click" />
        </form>
    </div>

    <script type="text/javascript">
        function ExportShopOrderImport() {
            $.ajax({
                url: "priceApi.aspx?method=QueryPriceModifyDataImport&HouseID=" + $('#HID').combobox('getValue') + "&PriceType=" + $('#PriceType').combobox('getValue') + "&UserName=" + $("#UserName").val() + "&Specs=" + $("#Specs").val() + "&StartDate=" + $('#StartDate').datebox('getValue') + "&EndDate=" + $('#EndDate').datebox('getValue'),
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=ShopOrderImport.ClientID %>"); obj.click() }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }
    </script>

</asp:Content>
