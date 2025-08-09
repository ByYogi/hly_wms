<%@ Page Title="开销售单" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="addSaleOrder.aspx.cs" Inherits="Dealer.Antyres.addSaleOrder" %>

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
                title: '周期年', field: 'BatchYear', width: '60px', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '库存数量', field: 'Piece', width: '60px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
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
                idField: 'ID',
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
                title: '周期年', field: 'BatchYear', width: '60px', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '订单数量', field: 'Piece', width: '60px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            //出库列表
            $('#outDg').datagrid({
                width: '100%',
                //height: '38%',
                title: '订单物料', //标题内容
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
                onDblClickRow: function (index, row) { dblClickDelCargo(index); },
                rowStyler: function (index, row) {
                }
            });

            $('#AcceptPeople').combobox('textbox').bind('focus', function () { $('#AcceptPeople').combobox('showPanel'); });
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
            $('#AGoodsCode').textbox('textbox').keydown(function (e) {
                if (e.keyCode == 13) {
                    dosearch();
                }
            });
            $('#AcceptPeople').combobox({
                valueField: 'ADID', textField: 'AcceptPeopleCellphone', delay: '10',
                url: 'FormService.aspx?method=AutoCompleteClientAcceptPeople&ClientNum=' + <%=UserInfor.LoginName%>,
                onLoadSuccess: function () {
                    //默认选中第一个
                    var array = $(this).combobox("getData");
                    for (var item in array[0]) {
                        if (item == "ADID") {
                            $(this).combobox('select', array[0][item]);
                        }
                    }
                    //$('#ASpecs').next('span').find('input').focus();
                },
                onSelect: onAcceptAddressChanged
            });
        });

        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'FormService.aspx?method=QueryALLHouseData';
            $('#dg').datagrid('load', {
                Specs: $('#ASpecs').val(),
                Figure: $('#AFigure').val(),
                GoodsCode: $('#AGoodsCode').val(),
                ClientNum: $('#ClientNum').val()
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
                    <input id="AGoodsCode" class="easyui-textbox" data-options="prompt:'请输入货品代码'" style="width: 100px" />
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <table id="outDg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;&nbsp;双击物料信息进行下单&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label runat="server" ID="UnpaidText" Font-Bold="true" Font-Size="15px" ForeColor="#cd5b5b"></asp:Label>
        <input type="hidden" id="RebateMoney" runat="server" clientidmode="Static" />
    </div>
    <!--Begin 出库操作-->

    <div id="dlg" class="easyui-dialog" style="width: 350px; height: 350px; padding: 0px" closed="true" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" id="InPiece" />
            <input type="hidden" id="InIndex" />
            <input type="hidden" id="index" />
            <table>
                <tr>
                    <td style="text-align: right;">拉上订单数量：
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
    <!--End 出库操作-->
    <form id="fmDep" class="easyui-form" method="post">
        <input type="hidden" name="SaleCellPhone" id="SaleCellPhone" />
        <input type="hidden" name="ShopCode" id="ShopCode" />
        <input type="hidden" name="ClientNum" id="ClientNum" />
        <input type="hidden" id="ClientType" name="ClientType" />
        <input type="hidden" name="BelongHouse" id="BelongHouse" />
        <input type="hidden" name="HouseID" id="HouseID" />
        <input type="hidden" name="HiddenClientSelectName" id="HiddenClientSelectName" />
        <input type="hidden" name="AAcceptUnit" id="AAcceptUnit" />
        <input type="hidden" name="AAcceptCellphone" id="AAcceptCellphone" />
        <input type="hidden" name="AAcceptTelephone" id="AAcceptTelephone" />
        <input type="hidden" name="AAcceptPeople" id="AAcceptPeople" />
        <input type="hidden" name="AAcceptCity" id="AAcceptCity" />
        <input type="hidden" id="HiddenLimitID" />
        <input type="hidden" id="HiddenLimitTitle" />

        <div id="saPanel">
            <table style="width: 100%">
                <tr>
                    <td style="text-align: right;">收货人:</td>
                    <td>
                        <input id="AcceptPeople" style="width: 180px;" data-options="required:true" class="easyui-combobox AcceptPeople" />
                    </td>
                    <td style="text-align: right;">收货地址:
                    </td>
                    <td colspan="1">
                        <input name="AcceptAddress" id="AAcceptAddress" data-options="disabled:true" style="width: 350px;" class="easyui-textbox" />
                    </td>
                    <td style="text-align: right;">总数量:
                    </td>
                    <td>
                        <input name="Piece" id="APiece" class="easyui-numberbox" data-options="min:0,precision:0,required:true,disabled:true" style="width: 80px;" />
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">备注:
                    </td>
                    <td colspan="4">
                        <textarea name="Remark" id="ARemark" placeholder="请输入订单备注" style="width: 70%; resize: none"></textarea>
                    </td>
                    <td id="JiSongTd">
                        <input class="ThrowGood" type="checkbox" name="ThrowGood" id="JiSong" value="17" />
                        <label class="ThrowGood" id="JiSongLab" for="JiSong">急送单</label>
                    </td>
                    <td colspan="2" rowspan="2" style="text-align: center;">
                        <a href="#" id="btnSave" class="easyui-linkbutton" iconcls="icon-ok" plain="false" onclick="saveOutCargo()">&nbsp;保&nbsp;存&nbsp;订&nbsp;单</a>
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <script type="text/javascript">
        //保存订单
        function saveOutCargo() {
            //取消业务员禁用方便后台取值
            $('#APiece').combobox('enable');
            $('#AAcceptAddress').combobox('enable');
            var rows = $('#outDg').datagrid('getRows');
            if (rows == null || rows == "") { $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '出库列表为空！', 'warning'); return; }

            //$('#HouseCode').val(rows[0].OrderCode);
            $('#BelongHouse').val(rows[0].BelongHouse);
            $('#HouseID').val(rows[0].HouseID);
            //$('#HouseName').val(rows[0].HouseName);
            $.messager.progress({ msg: '请稍后,正在保存中...' });
            $('#btnSave').linkbutton('disable');
            var json = JSON.stringify(rows);

            $('#fmDep').form('submit', {
                url: 'FormService.aspx?method=saveSimpleOrderData',
                contentType: "application/json;charset=utf-8", dataType: "json",
                onSubmit: function (param) {
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
                        location.reload();
                    } else {
                        $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                        //$('#SaleManID').combobox('disable');
                    }
                }
            })

        }
        //业务员选择方法
        function onSaleManIDChanged(item) {
            if (item) {
                $('#SaleCellPhone').val(item.CellPhone);
            }
        }

        //删除出库的数据
        function dblClickDelCargo(Did) {
            var row = $("#outDg").datagrid('getData').rows[Did];
            var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
            var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
            n--;
            var pt = p - (Number(row.InPiece) - Number(row.Piece));
            $('#DisplayNum').val(Number(n));
            $('#DisplayPiece').val(Number(pt));
            $('#APiece').numberbox('setValue', Number(pt));

            var title = "订单物料     已拉上：" + n + "个物料号，总数量：" + pt + " 条";
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
            $('#dg').datagrid('selectRow', $('#index').val());
            var row = $('#dg').datagrid('getSelected');
            var row2 = row;
            if ($('#Numbers').val() == null || $('#Numbers').val() == "") {
                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '请输入拉上订单数量！', 'warning');
                return;
            }
            if ($('#Numbers').val() < 1) {
                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '拉上订单数量必须大于0！', 'warning');
                return;
            }
            var Total = Number(row.Piece);
            var Aindex = $('#InIndex').val();
            var index = $('#outDg').datagrid('getRowIndex', row.ID);
            if (index < 0) {
                row.Piece = $('#Numbers').numberbox('getValue');
                $('#outDg').datagrid('appendRow', row);
                var indexD = $('#outDg').datagrid('getRowIndex', row.ID);
                $('#outDg').datagrid('refreshRow', indexD);

                var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
                var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
                n++;
                var pt = p + Number($('#Numbers').numberbox('getValue'));

                $('#DisplayNum').val(Number(n));
                $('#DisplayPiece').val(Number(pt));
                $('#APiece').numberbox('setValue', Number(pt));
                var title = "订单物料     已拉上：" + n + "个物料号，总数：" + pt + " 条";
                $('#outDg').datagrid("getPanel").panel("setTitle", title);
                closedgShowData();

                if (Total > Number($('#Numbers').numberbox('getValue'))) {
                    row2.Piece = Total - Number($('#Numbers').numberbox('getValue'));
                } else {
                    row2.Piece = 0;
                }
                $('#dg').datagrid('updateRow', { index: Aindex, row: row2 });
            } else { $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '订单列表已存在该产品，请先删除再添加！', 'warning'); }
        }
        //关闭
        function closedgShowData() {
            $('#dlg').dialog('close');
        }
        ///出库
        function plOutCargo() {
            $('#index').val("");
            var row = $('#dg').datagrid('getSelected');
            $('#index').val($('#dg').datagrid('getRowIndex', row.ID));
            if (row == null || row == "") {
                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '请选择要拉上订单的数据！', 'warning');
                return;
            }
            if (row.Piece == 0) {
                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '库存不足', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '拉上  ' + row.Specs + ' 花纹：' + row.Figure);
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
        function onAcceptAddressChanged(item) {
            $('#AAcceptCity').val(item.AcceptCity);
            $('#AAcceptUnit').val(item.AcceptCompany);
            $('#AAcceptAddress').textbox('setValue', item.AcceptAddress);
            $('#AAcceptTelephone').val(item.AcceptTelephone);
            $('#AAcceptCellphone').val(item.AcceptCellphone);
            $('#AAcceptPeople').val(item.AcceptPeople);
        }

    </script>
</asp:Content>
