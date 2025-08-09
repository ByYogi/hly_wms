<%@ Page Title="库存查询" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OuthouseManager.aspx.cs" Inherits="Dealer.OuthouseManager" %>

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
        }
        $(window).resize(function () {
            adjustment();
        });
        function adjustment() {
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true);
            $('#dg').datagrid({ height: height });
        }
        $(document).ready(function () {
            var ln = "<%=UserInfor.LoginName%>";
            var columns = [];
            columns.push({
                title: '产品品牌', field: 'TypeName', width: '130px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            columns.push({
                title: '货品代码', field: 'GoodsCode', width: '180px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            columns.push({
                title: '规格', field: 'Specs', width: '130px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            columns.push({
                title: '花纹', field: 'Figure', width: '130px', formatter: function (value) {
                    if (value != null && value != "") {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                }
            });
            columns.push({
                title: '载速', field: 'LoadIndex', width: '60px', align: 'right', formatter: function (value, row) {
                    return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                }
            });
            columns.push({
                title: '批次', field: 'BatchYear', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '库存情况', field: 'Piece', width: '60px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    if (value < 9) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    } else if (value > 8 && value < 21) {
                        return "<span title='紧张'>紧张</span>";
                    } else {
                        return "<span title='充足'>充足</span>";
                    }
                }
            });
            if (ln != "732951") {
                columns.push({
                    title: '销售价', field: 'SalePrice', width: '60px', align: 'right', formatter: function (value) {
                        if (value != null && value != "") {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                });
            }


            $('#dg').datagrid({
                width: 'auto',
                //height: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                pageSize: 20, //每页多少条
                pageList: [20, 50],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                showFooter: false,
                toolbar: '#toolbar',
                columns: [columns],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) {
                },
                rowStyler: function (index, row) {
                    if (row.RuleType == "1") { return "background-color:#f5a99a"; };
                    if (row.RuleType == "2") { return "background-color:#e1bd7f"; };
                    if (row.RuleType == "3") { return "background-color:#D4EFDF"; };
                }
            });
            $('#ASpecs').textbox('textbox').keydown(function (e) {
                if (e.keyCode == 13) {
                    dosearch();
                }
            });
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../FormService.aspx?method=QueryALLHouseData';
            $('#dg').datagrid('load', {
                Specs: $('#ASpecs').val(),
                Figure: $('#AFigure').val(),
                GoodsCode: $('#AGoodsCode').val()
            });
        }
        //弹出定时关闭的消息框
        function alert_autoClose(title, msg, icon) {
            var interval;
            var time = 1000;
            var x = 1;  //秒为单位，只接受整数
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
                <td id="ASpecsTd" style="text-align: right;">规格:
                </td>
                <td>
                    <input id="ASpecs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 80px" />
                </td>
                <td class="AFigureTd" id="AFigureTd" style="text-align: right;">花纹:
                </td>
                <td class="AFigureTd">
                    <input id="AFigure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 80px" />
                </td>
                <td id="AGoodsCodeTd" style="text-align: right;">货品代码:
                </td>
                <td>
                    <input id="AGoodsCode" class="easyui-textbox" data-options="prompt:'请输入货品代码'" style="width: 100px" />
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <%--  <table id="outDg" class="easyui-datagrid">
    </table>--%>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
    </div>


    <script type="text/javascript">
</script>

</asp:Content>
