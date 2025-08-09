<%@ Page Title="订货查询" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StockBook.aspx.cs" Inherits="Cargo.House.StockBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
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
        //页面加载显示遮罩层防止用户看见未加载CSS的页面
        var pc;
        $.parser.onComplete = function () {
            if (pc) {
                clearTimeout(pc);
            }
            pc = setTimeout(closemask, 10);
        }
        //加载完成后关闭遮罩层
        function closemask() {
            $("#Loading").fadeOut("normal", function () {
                $(this).remove();
            });
        }

        $(document).ready(function () {
            $('#dg').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: true, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'TypeName',
                url: null,
                toolbar: '#toolbar',
                showFooter: true,
                rownumbers: true,
                columns: [[
                    {
                        title: '仓库', field: 'FirstAreaName', width: '5%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '品牌', field: 'TypeName', width: '5%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '规格', field: 'Specs', width: '8%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '花纹', field: 'Figure', width: '8%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '载速', field: 'LoadIndex', width: '5%', formatter: function (value, row) {
                            return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                        }
                    },
                    //{
                    //    title: '速度', field: 'SpeedLevel', width: '5%', formatter: function (value) {
                    //        return "<span title='" + value + "'>" + value + "</span>";
                    //    }
                    //},
                    {
                        title: '库存数', field: 'InPiece', width: '5%', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    //{
                    //    title: '平均成本价', field: 'CostPrice', width: '5%', formatter: function (value) {
                    //        return "<span title='" + value + "'>" + value + "</span>";
                    //    }
                    //},
                    {
                        title: '平均单价', field: 'UnitPrice', width: '5%', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '上一进货价', field: 'SalePrice', width: '5%', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '供应商', field: 'PurchaserName', width: '5%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    { title: '进货时间', field: 'OP_DATE', width: '10%', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { },
                rowStyler: function (index, row) { }
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
            $('#APID').combobox('setValue', '1');
            $('#ASID').combobox('clear');
            $('#ASID').combobox({
                url: '../Product/productApi.aspx?method=QueryALLOneProductType&PID=1', valueField: 'TypeID', textField: 'TypeName', AddField: 'PinyinName',
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                },
            });
            //区域大仓
            $('#HouseID').combobox({
                url: 'houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#PID').combobox('clear');
                    var url = 'houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    $('#PID').combobox('reload', url);
                }
            });

            $('#HouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            $('#HouseID').combobox('textbox').bind('focus', function () { $('#HouseID').combobox('showPanel'); });
            $('#PID').combobox('textbox').bind('focus', function () { $('#PID').combobox('showPanel'); });
            $('#APID').combobox('textbox').bind('focus', function () { $('#APID').combobox('showPanel'); });
            $('#ASID').combobox('textbox').bind('focus', function () { $('#ASID').combobox('showPanel'); });
            var url = 'houseApi.aspx?method=QueryALLArea&pid=0&hid=<%=UserInfor.HouseID%>';
            $('#PID').combobox('reload', url);
        });

        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'houseApi.aspx?method=QueryStockBookData';
            $('#dg').datagrid('load', {
                HouseID: $('#HouseID').combobox('getValue'),
                PID: $("#PID").combobox('getValue'),
                Specs: $("#Specs").val(),
                Figure: $("#Figure").val(),
                SID: $("#ASID").combobox('getValue')
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--此div用于在界面未完全加载样式前显示内容--%>
    <div id='Loading' style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 100%; height: 100%; background: white; text-align: center; padding: 5px 10px; display: table;">
        <div style="display: table-cell; vertical-align: middle">
            <h1><font size="9">页面加载中……</font></h1>
        </div>
    </div>
    <%--此div用于在界面未完全加载样式前显示内容--%>

    <%--此div用于显示查询条件--%>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table id="saPanelTab">
            <tr>
                <td style="text-align: right;">区域大仓: </td>
                <td>
                    <input id="HouseID" class="easyui-combobox" style="width: 100px;" data-options="required:false" panelheight="auto" />
                </td>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="PID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'AreaID',textField:'Name'"
                        panelheight="auto" />
                </td>
                <td style="text-align: right;">一级产品:
                </td>
                <td>
                    <input id="APID" class="easyui-combobox" style="width: 100px;" panelheight="auto" />
                </td>
                <td style="text-align: right;">二级产品:
                </td>
                <td>
                    <input id="ASID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'TypeID',textField:'TypeName'" />
                </td>
                <td style="text-align: right;">规格:
                </td>
                <td>
                    <input id="Specs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 100px" />
                </td>
                <td style="text-align: right;">花纹:
                </td>
                <td>
                    <input id="Figure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 100px" />
                </td>

            </tr>
        </table>
    </div>
    <%--此div用于显示查询条件--%>
    <table id="dg" class="easyui-datagrid">
    </table>
    <%--此div用于显示按钮操作--%>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="AwbExport()">&nbsp;导&nbsp;出&nbsp;</a>&nbsp;&nbsp;
           <form runat="server" id="fm1">
               <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
           </form>
    </div>
    <%--此div用于显示按钮操作--%>
    <script type="text/javascript">
        //导出数据
        function AwbExport() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            $.messager.progress({ msg: '请稍后,正在导出中...' });
            var obj = document.getElementById("<%=btnDerived.ClientID %>");
            obj.click();
            $.messager.progress("close");
        }
    </script>
</asp:Content>
