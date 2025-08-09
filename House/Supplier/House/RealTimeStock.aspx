<%@ Page Title="实时库存" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RealTimeStock.aspx.cs" Inherits="Supplier.House.RealTimeStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        //页面加载时执行
        window.onload = function () {
            //$.ajaxSetup({ async: true });
            document.body.style.overflow = 'hidden';
            adjustment();
            document.body.style.overflow = 'auto';
        }
        $(window).resize(function () {
            document.body.style.overflow = 'hidden';
            adjustment();
            document.body.style.overflow = 'auto';
        });
        function adjustment() {
            var height = parseInt((Number($(window).height()) - Number($("div[name='SelectDiv1']").outerHeight(true))));
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
            var columns = [];
            columns.push({ title: '', field: 'ProductID', checkbox: true, width: '10%' });
            columns.push({
                title: '所在仓库', field: 'HouseName', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品编码', field: 'ProductCode', width: '9%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '品牌', field: 'TypeName', width: '7%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            }); columns.push({
                title: '规格', field: 'Specs', width: '8%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '花纹', field: 'Figure', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '货品代码', field: 'GoodsCode', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '载速', field: 'LoadIndex', width: '5%', formatter: function (value, row) {
                    return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                }
            });
            columns.push({ title: '批次', field: 'Batch', width: '5%' });
            columns.push({ title: '库存数量', field: 'RealStockNum', width: '5%', align: 'right' });//真实入库数量 关联查询得到
            columns.push({ title: '库存天数', field: 'InHouseDay', width: '5%', align: 'right' });//库存天数取当前时间减入库时间
            columns.push({ title: '进仓价', field: 'InHousePrice', width: '5%', align: 'right' });
            columns.push({ title: '销售价', field: 'SalePrice', width: '5%', align: 'right' });
            columns.push({ title: '进仓单号', field: 'SourceOrderNo', width: '7%', });
            columns.push({ title: '入库时间', field: 'InHouseTimeStr', width: '8%' });
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
                showFooter: true,
                toolbar: '#toolbar',
                columns: [columns],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
            });

          <%--  var ln = "<%=UserInfor.LoginName%>";
            if (ln == "551098" || ln == "542207" || ln == "130453" || ln == "891524") {
                $('#btnPlInhouse').hide();
            } else {
                $('#btnPlInhouse').show();

            }--%>

            //初始加载 
            //var datenow = new Date();
            //$('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            //$('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            //$('#StartDate').datebox('textbox').bind('focus', function () { $('#StartDate').datebox('showPanel'); });
            //$('#EndDate').datebox('textbox').bind('focus', function () { $('#EndDate').datebox('showPanel'); });

            //仓库、品牌(一级产品)
            var topMenuNew = [];
            var SettleHouseID = '<%= UserInfor.SettleHouseID%>'.split(',');
            var SettleHouseName = '<%= UserInfor.SettleHouseName%>'.split(',');
            for (var i = 0; i < SettleHouseID.length; i++) {
                topMenuNew.push({ "text": SettleHouseName[i], "id": SettleHouseID[i] });
            }
            $("#AHouseID").combobox("loadData", topMenuNew);
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            $('#AHouseID').combobox('setValue', "<%= UserInfor.SettleHouseID%>");
            topMenuNew = [];
            var ClientTypeID = '<%= UserInfor.ClientTypeID%>'.split(',');
            var ClientTypeName = '<%= UserInfor.ClientTypeName%>'.split(',');
            for (var i = 0; i < ClientTypeID.length; i++) {
                topMenuNew.push({ "text": ClientTypeName[i], "id": ClientTypeID[i] });
            }
            $("#ASID").combobox("loadData", topMenuNew);
            $('#ASID').combobox('textbox').bind('focus', function () { $('#ASID').combobox('showPanel'); });
        });

        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'houseApi.aspx?method=QueryAllProductStockInfo';
            $('#dg').datagrid('load', {
                SID: $("#ASID").combobox('getValue'),//二级产品 品牌
                HouseID: $("#AHouseID").combobox('getValue'),//仓库ID
                Specs: $('#ASpecs').val(),//规格
                Figure: $('#AFigure').val(),//花纹
                GoodsCode: '',//$('#AGoodsCode').val(),//货品代码
                InHouseDay: $("#InHouseDay").combobox('getValue'),
                ProductCode: $('#AProductCode').val(),//产品编码
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                BatchYear: $('#ABatchYear').combobox('getValue'),
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
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table>
            <tr>
                <td style="text-align: right;">仓库:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 95px;" data-options="valueField:'id',textField:'text',editable:false,required:true" />
                </td>
                <td style="text-align: right;">品牌:
                </td>
                <td>
                    <input id="ASID" class="easyui-combobox" style="width: 95px;" data-options="valueField:'id',textField:'text'" />
                </td>

                <td style="text-align: right;">规格:
                </td>
                <td>
                    <input id="ASpecs" class="easyui-textbox" data-options="prompt:'请输入规格',required:false" style="width: 95px" />
                </td>
                <td style="text-align: right;">花纹:
                </td>
                <td>
                    <input id="AFigure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 95px" />
                </td>
                <%--  <td style="text-align: right;">货品代码:
                </td>
                <td>
                    <input id="AGoodsCode" class="easyui-textbox" data-options="prompt:'请输入货品代码'" style="width: 120px" />
                </td>--%>
                <td style="text-align: right;">年周:
                </td>
                <td>
                    <select class="easyui-combobox" id="ABatchYear" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="25">25年</option>
                        <option value="24">24年</option>
                        <option value="23">23年</option>
                        <option value="22">22年</option>
                        <option value="21">21年</option>
                        <option value="20">20年</option>
                    </select>
                </td>
                <td style="text-align: right;">产品编码:
                </td>
                <td>
                    <input id="AProductCode" class="easyui-textbox" data-options="prompt:'请输入产品编码'" style="width: 95px" />
                </td>
                <td style="text-align: right;">库存天数:
                </td>
                <td>
                    <select class="easyui-combobox" id="InHouseDay" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="60">60天</option>
                        <option value="90">90天</option>
                    </select>
                </td>
                <td style="text-align: right;">入库时间:
                </td>
                <td colspan="3">
                    <input id="StartDate" class="easyui-datebox" style="width: 100px" />~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px" />
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
      <%--  <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="modifyInHousePrice()">&nbsp;修改进仓价&nbsp;</a>&nbsp;&nbsp;--%>
        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="modifySale()" id="btnPlInhouse">&nbsp;修改销售价&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="exportData()">&nbsp;导出&nbsp;</a>&nbsp;&nbsp;
           <form runat="server" id="fm1">
               <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
           </form>
    </div>
    <!--Begin 批量修改销售价-->
    <div id="dlgUpInNum" class="easyui-dialog" style="width: 400px; height: 250px; padding: 5px 5px"
        closed="true" buttons="#dlgUpInNum-buttons">
        <form id="fmUpInNum" class="easyui-form" method="post">
            <table>
                <tr>
                    <td style="text-align: right;">销售价格:
                    </td>
                    <td>
                        <input name="SalePrice" id="SalePrice" class="easyui-numberbox" data-options="min:0,precision:2"
                            style="width: 200px;" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlgUpInNum-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveSalePriceModify()">&nbsp;确&nbsp;定&nbsp;</a>&nbsp;&nbsp;
 <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgUpInNum').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <!--End 批量修改销售价-->


    <script type="text/javascript">
        //修改价格
        function modifyInHousePrice() {

        }

        //导出Excel
        function exportData() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            $.messager.progress({ msg: '请稍后,正在导出中...' });
            $.ajax({
                url: "houseApi.aspx?method=QueryProductStockInfoExport&SID=" + $("#ASID").combobox('getValue') + "&HouseID=" + $("#AHouseID").combobox('getValue') + "&Specs=" + $('#ASpecs').val() + "&Figure=" + $("#AFigure").val() + "&ProductCode=" + $("#AProductCode").val() + "&StartDate=" + $('#StartDate').datebox('getValue') + "&EndDate=" + $('#EndDate').datebox('getValue') + "&InHouseDay=" + $("#InHouseDay").combobox('getValue'),
                success: function (text) {
                    $.messager.progress("close");
                    if (text == "OK") { var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click() }
                    else { $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
            $.messager.progress("close");
        }
        //弹窗提示？-是否批量修改销售价
        function modifySale() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning'); return;
            }
            if (rows) {
                $('#dlgUpInNum').dialog('open').dialog('setTitle', '批量修改产品销售价格');
                $('#SalePrice').numberbox('setValue', rows[0].SalePrice);
            }
        }
        //保存批量修改销售价
        function saveSalePriceModify() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请选择要保存的数据！', 'warning'); return; }
            $.messager.confirm('<%= Supplier.Common.GetSystemNameAndVersion()%>', '确定修改？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    var json = JSON.stringify(rows);
                    $('#fmUpInNum').form('submit', {
                        url: 'houseApi.aspx?method=SaveRealTimeStockSalePrice',
                        contentType: "application/json;charset=utf-8", dataType: "json",
                        onSubmit: function (param) {
                            param.submitData = json;
                            var trd = $(this).form('enableValidation').form('validate');
                            return trd;
                        },
                        success: function (msg) {
                            $.messager.progress("close");
                            var res = eval('(' + msg + ')');
                            if (res.Result) {
                                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                                $('#dg').datagrid('reload');
                                $('#dlgUpInNum').dialog('close');
                            } else {
                                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '修改失败：' + res.Message, 'warning');
                            }
                        }
                    })
                }
            });
        }

    </script>
</asp:Content>
