<%@ Page Title="清货列表" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShortageList.aspx.cs" Inherits="Dealer.ShortageList" %>
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
            //var height = parseInt((Number($(window).height()) - 100) / 2);
            var height = parseInt((Number($(window).height()) - Number($("div[name='SelectDiv1']").outerHeight(true))) / 2);
            $('#dg').datagrid({ height: height - 60 });
            $('#outDg').datagrid({ height: (Number($(window).height()) - 90) - height });
        }
        $(document).ready(function () {
            var columns = [];
            columns.push({ title: '', field: 'ID', checkbox: true, width: '10%' });
            columns.push({
                title: '产品品牌', field: 'TypeName', width: '130px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '货品代码', field: 'GoodsCode', width: '180px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '规格', field: 'Specs', width: '130px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '花纹', field: 'Figure', width: '130px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '载速', field: 'LoadIndex', width: '60px', align: 'right', formatter: function (value, row) {
                    return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                }
            });
            columns.push({
                title: '库存情况', field: 'Piece', width: '60px', align: 'right', styler: function (val, row, index) { return "color:#f5a99a;font-weight:bold;" }, formatter: function (value) {
                    if (value < 0) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    } else {
                        return "<span title='缺货'>缺货</span>";
                    }
                }
            });
            $('#dg').datagrid({
                width: '100%',
                //height: '50%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'SID',
                url: null,
                toolbar: '#toolbar',
                columns: [columns],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { plOutCargo(); },
                rowStyler: function (index, row) {
                }
            });
            columns = [];
            columns.push({ title: '', field: 'SID', checkbox: true, width: '10%' });
            columns.push({
                title: '产品品牌', field: 'TypeName', width: '130px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '货品代码', field: 'GoodsCode', width: '180px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '规格', field: 'Specs', width: '130px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '花纹', field: 'Figure', width: '130px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '载速', field: 'LoadIndex', width: '60px', align: 'right', formatter: function (value, row) {
                    return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                }
            });
            columns.push({
                title: '等货数量', field: 'Piece', width: '60px', align: 'right', styler: function (val, row, index) { return "color:#f5a99a;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            //出库列表
            $('#outDg').datagrid({
                width: '100%',
                //height: '38%',
                title: '缺货物料', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ShortageID',
                url: null,
                toolbar: '',
                columns: [columns],
                onLoadSuccess: function (data) {
                    var rows = $('#outDg').datagrid('getRows');
                    var pt = 0;
                    var n = rows.length;
                    for (var i = 0; i < rows.length; i++) {
                        pt = pt + Number(rows[i].Piece);
                    }
                    $('#DisplayNum').val(Number(n));
                    $('#DisplayPiece').val(Number(pt));
                    $('#APiece').numberbox('setValue', Number(pt));
                    var title = "缺货物料     已拉上：" + n + "个物料号，总数量：" + pt + " 条";
                    $('#outDg').datagrid("getPanel").panel("setTitle", title);
                },
                onDblClickRow: function (index, row) { dblClickDelCargo(index); },
                rowStyler: function (index, row) {

                }
            });

            $('#ASpecs').textbox('textbox').keydown(function (e) {
                if (e.keyCode == 13) {
                    dosearch();
                }
            });
            $('#AFigure').textbox('textbox').keydown(function (e) {
                if (e.keyCode == 13) {
                    dosearch();
                }
            });
            var gridOpts = $('#outDg').datagrid('options');
            gridOpts.url = '../FormService.aspx?method=QueryShortageListByClientNum';
        });

        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../FormService.aspx?method=QueryALLProductSpecPieceData';
            $('#dg').datagrid('load', {
                Specs: $('#ASpecs').val(),
                Figure: $('#AFigure').val(),
                GoodsCode: $('#AGoodsCode').val()
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
                    <input id="ASpecs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 100px" />
                </td>
                <td id="AFigureTd" style="text-align: right;">花纹:
                </td>
                <td>
                    <input id="AFigure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 100px" />
                </td>
                <td id="AGoodsCodeTd" style="text-align: right;">货品代码:
                </td>
                <td>
                    <input id="AGoodsCode" class="easyui-textbox" data-options="prompt:'请输入货品代码'" style="width: 100px"/>
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <table id="outDg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;&nbsp;双击物料信息添加至缺货列表&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label runat="server" ID="UnpaidText" Font-Bold="true" Font-Size="15px" ForeColor="#cd5b5b"></asp:Label>
    </div>
    <!--Begin 出库操作-->

    <div id="dlg" class="easyui-dialog" style="width: 350px; height: 250px; padding: 0px" closed="true" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
            <table>
                <tr>
                    <td style="text-align: right;">拉上列表数量：
                    </td>
                    <td>
                        <input name="Numbers" id="Numbers" class="easyui-numberbox" data-options="min:0,precision:0" style="width: 170px;" type="text" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="outOK()">&nbsp;确&nbsp;定&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <input type="hidden" id="DisplayNum" />
    <input type="hidden" id="DisplayPiece" />
    <script type="text/javascript">
        //删除出库的数据
        function dblClickDelCargo(Did) {
            var row = $("#outDg").datagrid('getData').rows[Did];
            var json = JSON.stringify([row])
            $.messager.progress({ msg: '请稍后,拉上订单中...' });
            $.ajax({
                url: '../FormService.aspx?method=DeleteShortageList',
                type: 'post', dataType: 'json', data: { data: json },
                success: function (text) {
                    $.messager.progress("close");
                    if (text.Result == true) {
                    }
                    else {
                        $('#APiece').numberbox('setValue', Number(p));
                        $('#TransportFee').numberbox('setValue', tCharge);
                        $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                        }
                    }
                });
            var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
            var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
            n--;
            var pt = p - Number(row.Piece);
            $('#DisplayNum').val(Number(n));
            $('#DisplayPiece').val(Number(pt));
            $('#APiece').numberbox('setValue', Number(pt));
            var title = "缺货物料     已拉上：" + n + "个物料号，总数量：" + pt + " 条";
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
                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '请输入拉上列表数量！', 'warning');
                return;
            }
            if ($('#Numbers').val() < 1) {
                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '拉上列表数量必须大于0！', 'warning');
                return;
            }
            row.Piece = $('#Numbers').numberbox('getValue');
            var json = JSON.stringify([row])
            $.messager.progress({ msg: '请稍后,拉上订单中...' });
            $.ajax({
                url: '../FormService.aspx?method=InsertShortageList',
                type: 'post', dataType: 'json', data: { data: json },
                success: function (text) {
                    $.messager.progress("close");
                    if (text.Result == true) {

                    }
                    else {
                        $('#APiece').numberbox('setValue', Number(p));
                        $('#TransportFee').numberbox('setValue', tCharge);
                        $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                        return;
                    }
                }
            });
            $('#outDg').datagrid('appendRow', row);
            var indexD = $('#outDg').datagrid('getRowIndex', row.ID);
            $('#outDg').datagrid('refreshRow', indexD);

            var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
            var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
            n++;
            var pt = p + Number($('#Numbers').numberbox('getValue'));
            var title = "缺货物料     已拉上：" + n + "个物料号，总数：" + pt + " 条";
            $('#DisplayNum').val(Number(n));
            $('#DisplayPiece').val(Number(pt));
            $('#outDg').datagrid("getPanel").panel("setTitle", title);
            closedgShowData();
        }
        //关闭
        function closedgShowData() {
            $('#dlg').dialog('close');
        }
        ///出库
        function plOutCargo() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '请选择要拉上列表的数据！', 'warning');
                return;
            }
            if (row) {
                var dgS = $('#outDg').datagrid('getRows');
                for (var i = 0; i < dgS.length; i++) {
                    if (dgS[i].Specs == row.Specs && dgS[i].Figure == row.Figure && dgS[i].GoodsCode == row.GoodsCode && dgS[i].LoadIndex == row.LoadIndex && dgS[i].SpeedLevel == row.SpeedLevel && dgS[i].TypeName == row.TypeName) {
                        $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '该产品已在缺货列表上！', 'warning');
                        return;
                    }
                }
                $('#dlg').dialog('open').dialog('setTitle', '拉上  ' + row.Specs + ' 花纹：' + row.Figure);
                $('#InPiece').val(row.Piece);
                $('#InIndex').val($('#dg').datagrid('getRowIndex', row));
                $('#Numbers').numberbox('setValue', '');
                $('#Numbers').numberbox().next('span').find('input').focus();
            }
        }
    </script>
</asp:Content>
