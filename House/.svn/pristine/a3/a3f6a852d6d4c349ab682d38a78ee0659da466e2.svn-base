<%@ Page Title="进仓订单" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseOrder.aspx.cs" Inherits="Supplier.House.PurchaseOrder" %>

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
            var height = parseInt((Number($(window).height()) - Number($("div[name='SelectDiv1']").outerHeight(true))) - 80);
            $('#dg').datagrid({ height: height - 20 });
            $('#outDg').datagrid({ height: height - 20 });
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
            columns.push({ title: '', field: 'SID', checkbox: true, width: '30px' });
            columns.push({
                title: '品牌', field: 'TypeName', width: '70px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品编码', field: 'ProductCode', width: '120px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '规格', field: 'Specs', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '花纹', field: 'Figure', width: '120px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({ title: '型号', field: 'Model', hidden: true });
            columns.push({
                title: '货品代码', field: 'GoodsCode', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '载速', field: 'LoadIndex', width: '80px', formatter: function (value, row) {
                    return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                }
            });
            columns.push({
                title: '进仓价', field: 'InHousePrice', width: '80px', algin: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });//销售价
            //columns.push({
            //    title: '产地', field: 'Born', width: '5%', formatter: function (value) {
            //        if (value == "0") { return "<span title='国产'>国产</span>"; }
            //        else if (value == "1") { return "<span title='进口'>进口</span>"; }
            //    }
            //});
            columns.push({ title: '产地', field: 'Born', hidden: true });
            columns.push({ title: '类型', field: 'Assort', hidden: true });
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
                onDblClickRow: function (index, row) { plOutCargo(row); }
            });

            columns = [];
            columns.push({
                title: '', field: 'SID', checkbox: true, width: '30px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '品牌', field: 'TypeName', width: '70px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品编码', field: 'ProductCode', width: '120px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '规格', field: 'Specs', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '花纹', field: 'Figure', width: '120px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({ title: '型号', field: 'Model', hidden: true });
            columns.push({
                title: '货品代码', field: 'GoodsCode', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '载速', field: 'LoadIndex', width: '60px', formatter: function (value, row) {
                    return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                }
            });
            //columns.push({
            //    title: '产地', field: 'Born', width: '5%', formatter: function (value) {
            //        if (value == "0") { return "<span title='国产'>国产</span>"; }
            //        else if (value == "1") { return "<span title='进口'>进口</span>"; }
            //    }
            //});
            //columns.push({ title: '产地', field: 'Born', hidden: true });
            //columns.push({ title: '类型', field: 'Assort', hidden: true });
            columns.push({
                title: '批次', field: 'Batch', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '周期年', field: 'BatchYear', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '数量', field: 'Piece', width: '60px', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });

            //columns.push({
            //    title: '单价', field: 'UnitPrice', width: '5%', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});
            columns.push({
                title: '进仓价', field: 'InHousePrice', width: '60px', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '进仓总金额', field: 'AllUnitPrice', width: '80px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value, row) {
                    return "<span title='" + accMul(row.Piece, row.InHousePrice) + "'>" + accMul(row.Piece, row.InHousePrice) + "</span>";
                }
            });
            columns.push({
                title: '小程序价', field: 'SalePrice', width: '80px', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });//原销售价
            //进仓单列表
            $('#outDg').datagrid({
                width: '100%',
                //height: '38%',
                title: '进仓单列表', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'SID',
                url: null,
                toolbar: '',
                columns: [columns],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { dblClickDelCargo(index); }
            });
            $('#DisplayNum').val(0);
            $('#DisplayPiece').val(0);
            var d = new Date();
            $('#CreateDate').datetimebox('setValue', AllDateTime(d));
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
            var topMenuNew = [];
            var SettleHouseID = '<%= UserInfor.SettleHouseID%>'.split(',');
            var SettleHouseName = '<%= UserInfor.SettleHouseName%>'.split(',');
            for (var i = 0; i < SettleHouseID.length; i++) {
                topMenuNew.push({ "text": SettleHouseName[i], "id": SettleHouseID[i] });
            }
            $("#HouseID").combobox("loadData", topMenuNew);
            $('#HouseID').combobox('textbox').bind('focus', function () { $('#HouseID').combobox('showPanel'); });
            $('#HouseID').combobox('setValue', "<%= UserInfor.SettleHouseID%>");
            topMenuNew = [];
            var ClientTypeID = '<%= UserInfor.ClientTypeID%>'.split(',');
            var ClientTypeName = '<%= UserInfor.ClientTypeName%>'.split(',');
            for (var i = 0; i < ClientTypeID.length; i++) {
                topMenuNew.push({ "text": ClientTypeName[i], "id": ClientTypeID[i] });
            }
            $("#ASID").combobox("loadData", topMenuNew);
            $('#ASID').combobox('textbox').bind('focus', function () { $('#ASID').combobox('showPanel'); });
        });
        function accMul(arg1, arg2) {
            var m = 0, s1 = arg1.toString(), s2 = arg2.toString();
            try { m += s1.split(".")[1].length } catch (e) { }
            try { m += s2.split(".")[1].length } catch (e) { }
            return Number(s1.replace(".", "")) * Number(s2.replace(".", "")) / Math.pow(10, m)
        }

        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'houseApi.aspx?method=QueryALLProductData';
            $('#dg').datagrid('load', {
                ProductCode: $('#AProductCode').val(),
                Specs: $('#ASpecs').val(),
                Figure: $('#AFigure').val(),
                SID: $("#ASID").combobox('getValue'),//二级产品
                GoodsCode: $("#AGoodsCode").val()
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
                <td style="text-align: right;">货品代码:
                </td>
                <td>
                    <input id="AGoodsCode" class="easyui-textbox" data-options="prompt:'请输入货品代码'" style="width: 110px" />
                </td>
                <td style="text-align: right;">产品编码:
                </td>
                <td>
                    <input id="AProductCode" class="easyui-textbox" data-options="prompt:'请输入产品编码'" style="width: 120px" />
                </td>
            </tr>
        </table>
    </div>
    <table style="width: 100%">
        <tr>
            <td style="width: 40%; margin: 0px; padding: 0px;">
                <table id="dg" class="easyui-datagrid">
                </table>
            </td>
            <td style="width: 55%; margin: 0px; padding: 0px;">
                <table id="outDg" class="easyui-datagrid">
                </table>
            </td>
        </tr>
    </table>


    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="plOutCargo()">&nbsp;添加上订单&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删除下订单&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-in_cargo" plain="false" onclick="Import()">&nbsp;批量导入进仓单&nbsp;</a>&nbsp;&nbsp;
    </div>
    <form id="fmDep" class="easyui-form" method="post">
        <input type="hidden" id="DisplayNum" />
        <input type="hidden" id="DisplayPiece" name="DisplayPiece" />
        <div id="saPanel">
            <table style="width: 100%">
                <tr>
                    <td style="text-align: right;">开单日期：</td>
                    <td>
                        <input name="CreateDate" id="CreateDate" class="easyui-datetimebox" data-options="formatter:AllDateTime" readonly="true" style="width: 150px;" />
                    </td>
                    <td style="text-align: right;">进仓仓库：</td>
                    <td>
                        <input name="HouseID" id="HouseID" style="width: 120px;" class="easyui-combobox" data-options="valueField:'id',textField:'text',editable:false,required:true" />
                    </td>
                    <td style="text-align: right;">总数量：</td>
                    <td style="">
                        <input name="Total" id="Total" class="easyui-numberbox" data-options="min:0,precision:0,required:false,editable:false,disable:true" style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">总金额：</td>
                    <td style="">
                        <input name="TotalCharge" id="TotalCharge" class="easyui-numberbox" data-options="min:0,precision:2,required:false,editable:false" style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">进仓单号：</td>
                    <td style="">
                        <input name="FacOrderNo" id="FacOrderNo" class="easyui-textbox" style="width: 100px;" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">备注:
                    </td>
                    <td colspan="4">
                        <textarea name="Remark" id="ARemark" rows="3" placeholder="请输入订单备注" style="width: 95%; resize: none"></textarea>
                    </td>
                    <td style="text-align: right;" colspan="5">
                        <a href="#" id="btnSave" class="easyui-linkbutton" iconcls="icon-ok" plain="false" onclick="saveOutCargo()">&nbsp;保&nbsp;存&nbsp;订&nbsp;单</a>&nbsp;&nbsp;
                        <a href="#" class="easyui-linkbutton" id="undo" iconcls="icon-clear" onclick="reset()">&nbsp;重&nbsp;置&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>

                </tr>
            </table>
        </div>
    </form>
    <div id="dlg" class="easyui-dialog" style="width: 400px; height: 300px; padding: 0px"
        closed="true" buttons="#dlg-buttons">
        <div id="saPanel">
            <form id="fm" class="easyui-form" method="post">
                <input type="hidden" id="InPiece" />
                <input type="hidden" id="InIndex" />
                <table>
                    <tr>
                        <td style="text-align: right;">进仓数量：
                        </td>
                        <td>
                            <input name="Numbers" id="Numbers" class="easyui-numberbox" data-options="min:0,precision:0,prompt:'请输入进仓数量',required:true" style="width: 200px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">周期批次：
                        </td>
                        <td>
                            <input name="Batch" id="Batch" class="easyui-textbox" data-options="prompt:'请输入轮胎批次',required:true" style="width: 200px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">周期年份：
                        </td>
                        <td>
                            <input name="BatchYear" id="BatchYear" class="easyui-numberbox" data-options="min:22,precision:0,prompt:'请输入轮胎周期年份',required:true" style="width: 200px;" />
                        </td>
                    </tr>
                    <%-- 单价 --%>
                    <tr>
                        <td style="text-align: right;">单价：</td>
                        <td>
                            <input id="UnitPrice" class="easyui-numberbox" data-options="min:0,precision:2,prompt:'请输入单价'" style="width: 200px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">进仓价：</td>
                        <%-- 成本价 --%>
                        <td>
                            <input id="InHousePrice" class="easyui-numberbox" data-options="min:0,precision:2,prompt:'请输入进仓价',required:true" style="width: 200px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">小程序价：</td>
                        <%-- 销售价 --%>
                        <td>
                            <input id="SalePrice" name="SalePrice" class="easyui-numberbox" data-options="min:0,precision:2,prompt:'请输入小程序价',required:true" readonly="true" style="width: 200px;" />
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="2" style="color: #ad2859; padding-left: 55px;">提示：小程序价=进仓价+进仓价*0.9%</td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="outOK()">&nbsp;确&nbsp;定&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <%--此div用于导入数据--%>
    <div id="dlgImport" class="easyui-dialog" style="width: 1000px; height: 530px; padding: 2px 2px" closed="true" buttons="#dlgImport-buttons">
        <table id="dgImport" class="easyui-datagrid">
        </table>
        <div id="dginporttoolbar">
            <input type="file" id="fileT" name="file" accept=".xls" onchange="saveFile()" style="width: 250px;" />
            <input type="hidden" id="ExistCount" />
            <a href="#" id="btnload" class="easyui-linkbutton" iconcls="icon-out_cargo" plain="false" onclick="saveFile()">&nbsp;重新上传&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-ok" plain="false" onclick="btnSaveData()">&nbsp;导入数据&nbsp;</a>&nbsp;&nbsp;
            <a href="../AFile/供应商进仓单导入模板0727.xls" id="dowload" class="easyui-linkbutton" iconcls="icon-application_put" plain="false">&nbsp;下载模板&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closeDlgStatus()">&nbsp;关&nbsp;闭&nbsp;</a>
        </div>
    </div>
    <%--此div用于导入数据--%>

    <script type="text/javascript">
        function closeDlgStatus() {
            $('#dlgImport').dialog('close');
            $('#dgImport').datagrid('loadData', { total: 0, rows: [] });
            $('#fileT').val("");
        }
        //导入
        function Import() {
            $('#dlgImport').dialog('open').dialog('setTitle', '导入工厂来货订单数据');
            showData();
            $('#dgImport').datagrid('loadData', { total: 0, rows: [] });
        }
        //显示DataGrid数据列表
        function showData() {
            var columns = [];
            var HID = "<%=UserInfor.HouseID%>";
            columns.push({ title: '品牌', field: 'TypeName', width: '70px' });
            columns.push({ title: '产品编码', field: 'ProductCode', width: '120px' });
            columns.push({ title: '规格', field: 'Specs', width: '85px' });
            columns.push({ title: '花纹', field: 'Figure', width: '85px' });
            columns.push({ title: '型号', field: 'Model', width: '85px' });
            columns.push({ title: '货品代码', field: 'GoodsCode', width: '85px' });
            columns.push({ title: '载重', field: 'LoadIndex', width: '50px' });
            columns.push({ title: '速度', field: 'SpeedLevel', width: '50px' });
            columns.push({ title: '周期批次', field: 'Batch', width: '65px' });
            columns.push({ title: '周期年份', field: 'BatchYear', width: '65px' });
            columns.push({ title: '数量', field: 'InPiece', width: '65px' });
            columns.push({ title: '单价', field: 'UnitPrice', width: '65px' });
            columns.push({ title: '进仓价', field: 'InHousePrice', width: '65px' });
            columns.push({ title: '进仓总金额', field: 'TaxCostPrice', width: '65px' });
            columns.push({ title: '小程序价', field: 'SalePrice', width: '65px' });
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
                columns: [columns]
            });
        }
        //保存上传的文件
        function saveFile() {
            var file = $("#fileT").val();
            if (file == null || file == "") {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请选择导入文件!', 'info');
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
                url: 'HouseApi.aspx?method=saveFile',
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
                        $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', message, 'warning');
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
                        $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', message, 'warning');
                    }
                }
            })
            return false;
        }
        //保存数据
        function btnSaveData() {
            var rows = $("#dgImport").datagrid('getData').rows;
            if (rows == null || rows == "") {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请先导入需要保存的数据！', 'warning');
                return;
            }
            var msg = "确定导入？";
            $.messager.confirm('<%= Supplier.Common.GetSystemNameAndVersion()%>', msg, function (r) {
                if (r) {
                    var Total = 0;
                    var TotalCharge = 0;
                    for (var i = 0; i < rows.length; i++) {
                        var row = rows[i];
                        row.Piece = rows[i].InPiece;
                        row.Batch = rows[i].Batch;
                        row.SalePrice = rows[i].SalePrice;
                        row.InHousePrice = rows[i].InHousePrice;
                        row.UnitPrice = rows[i].UnitPrice;
                        $('#outDg').datagrid('appendRow', row);
                        Total += row.Piece;
                        TotalCharge += row.InHousePrice * row.Piece;
                    }
                    TransportFee += TotalCharge;
                    $('#TotalCharge').numberbox('setValue', TransportFee);
                    $('#Total').numberbox('setValue', Number($('#DisplayPiece').val()) + Total);
                    $('#DisplayNum').val(Number(TransportFee));
                    $('#DisplayPiece').val(Number(Number($('#DisplayPiece').val()) + Total));
                    closeDlgStatus();
                }
            });
        }
        //删除下订单
        function DelItem() {
            var deletedData = $('#outDg').datagrid('getChecked');
            if (deletedData == null || deletedData == "") { $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请选择要删除的下订单数据！', 'warning'); return; }
            $.messager.confirm('<%= Supplier.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    //然后通过for循环，从最后一行开始向前遍历，每次遍历，用getRowIndex方法得到该行的索引，然后用deleteRow删除该行
                    for (var i = deletedData.length - 1; i >= 0; i--) {
                        var rowIndex = $('#outDg').datagrid('getRowIndex', deletedData[i]);
                        $('#outDg').datagrid('deleteRow', rowIndex);
                    }
                    $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '删除成功', 'success');
                }
            })
        }
        //输入进仓价后 自动计算小程序价
        $("#InHousePrice").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
        function qh() {
            //小程序价SalePrice=进仓价InHousePrice+进仓价InHousePrice * 0.9%
            var salePrice = Number($('#InHousePrice').numberbox('getValue')) + Number($('#InHousePrice').numberbox('getValue') * 0.009);
            var newSalePrice = Math.ceil(salePrice)
            $('#SalePrice').numberbox('setValue', newSalePrice);
          <%--  var upclient = "<%=UserInfor.UpClientID%>";
            if (upclient != "1") {
                //小程序价SalePrice=进仓价InHousePrice+进仓价InHousePrice * 0.9%
                var salePrice = Number($('#InHousePrice').numberbox('getValue')) + Number($('#InHousePrice').numberbox('getValue') * 0.009);
                var newSalePrice = Math.ceil(salePrice)
                $('#SalePrice').numberbox('setValue', newSalePrice);
            }--%>
        }
        //弹出定时关闭的消息框
        function alert_autoClose(title, msg, icon) {
            var interval;
            var time = 500;
            var x = 2;  //只接受整数
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
        ///出库
        function plOutCargo() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请选择要拉上订单的数据！', 'warning');
                return;
            }
            var index = $('#outDg').datagrid('getRowIndex', row.SID);
            if (index >= 0) {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '进货单列表已存在该产品，请先删除再添加！', 'warning');
                return;
            }
            $('#Numbers').numberbox('setValue', '');
            $('#Batch').textbox('setValue', '');
            $('#SalePrice').numberbox('setValue', '');
            $('#UnitPrice').numberbox('setValue', '');
            $('#InHousePrice').numberbox('setValue', '');
            //$('#InHousePrice').numberbox('setValue', row.SalePrice);
          

            <%--var upclient = "<%=UserInfor.UpClientID%>";
            if (upclient == "1") {
                //公司内部供应商 自动取系统配置的价格数据
                $.ajax({
                    url: "../House/HouseApi.aspx?method=QueryBasicPriceEntity",
                    async: false, data: { ProductCode: row.ProductCode }, type: "post", dataType: 'json',
                    success: function (data) {
                        if (data.InHousePrice <= 0) {
                            $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '该规格无基础价格数据！', 'warning');
                            return;
                        }
                        $('#SalePrice').numberbox('setValue', data.SalePrice);
                        $('#InHousePrice').numberbox('setValue', data.InHousePrice);
                        $('#UnitPrice').numberbox('setValue', data.UnitPrice);

                    }
                })
            }--%>

            $('#dlg').dialog('open').dialog('setTitle', '拉上  ' + row.Specs + ' 花纹：' + row.Figure);
            var myDate = new Date();
            var year = myDate.getFullYear().toString();
            //$("#Batch").numberbox('setValue', year.slice(2, 4));
            var FullDiscountFull = $('#FullDiscountFull').val();
            if (FullDiscountFull != undefined) {
                document.cookie = "FullDiscountFull=; expires=Thu, 01 Jan 1970 00:00:00 GMT";
                document.cookie = "FullDiscountCut=; expires=Thu, 01 Jan 1970 00:00:00 GMT";
                document.cookie = "FullDiscountSum=; expires=Thu, 01 Jan 1970 00:00:00 GMT";
                var ThisRuleContent = $('#FullDiscountCut').val();
                document.cookie = "FullDiscountFull=" + FullDiscountFull;
                document.cookie = "FullDiscountCut=" + ThisRuleContent;
                document.cookie = "FullDiscountSum=" + text.FullDiscountSum;
            }
        }
        TransportFee = 0;
        //新增出库数据
        function outOK() {
            var row = $('#dg').datagrid('getSelected');
            if ($('#Numbers').val() == null || $('#Numbers').val() == "") {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请输入拉上进仓单数量！', 'warning');
                return;
            }
            if ($('#Numbers').val() < 1) {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '拉上进仓单数量必须大于0！', 'warning');
                return;
            }
<%--            if ($("#Batch").val().length < 4) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请输入完整4位批次！', 'warning');
                return;
            }--%>
            var index = $('#outDg').datagrid('getRowIndex', row.SID);
            if (index < 0) {
                row.Piece = $('#Numbers').numberbox('getValue');
                row.Batch = $('#Batch').textbox('getValue');
                row.BatchYear = $('#BatchYear').numberbox('getValue');
                row.SalePrice = $('#SalePrice').numberbox('getValue');
                row.InHousePrice = $('#InHousePrice').numberbox('getValue');
                row.UnitPrice = $('#UnitPrice').numberbox('getValue');
                $('#outDg').datagrid('appendRow', row);
                var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
                var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
                n++;
                var pt = p + Number($('#Numbers').numberbox('getValue'));
                if (getCookie("FullDiscountFull") > 0) {
                    var ptNum = (parseInt(Number($('#Numbers').numberbox('getValue'))) + parseInt(getCookie("FullDiscountSum"))) / getCookie("FullDiscountFull");
                    var ptList = (ptNum + "").split(".");
                    if (ptList.length > 1) {
                        fullDiscountCut = ptList[0] * parseInt(getCookie("FullDiscountCut"));
                    } else {
                        fullDiscountCut = ptList[0] * parseInt(getCookie("FullDiscountCut"));
                    }
                }
                $('#DisplayNum').val(Number(n));
                $('#DisplayPiece').val(Number(pt));
                var title = "进仓单     已拉上：" + n + "票，总条数：" + pt + " 条";
                $('#outDg').datagrid("getPanel").panel("setTitle", title);
                TransportFee += $('#Numbers').numberbox('getValue') * $('#InHousePrice').numberbox('getValue');
                $('#TransportFee').numberbox('setValue', TransportFee);
                $('#TotalCharge').numberbox('setValue', TransportFee);
                $('#Total').numberbox('setValue', pt);
                closedgShowData();

            } else {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '进货单列表已存在该产品，请先删除再添加！', 'warning');
            }
        }
        function getCookie(cname) {
            var name = cname + "=";
            var cookie = document.cookie.split(';');
            for (var i = 0; i < cookie.length; i++) {
                var c = cookie[i].trim();
                if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
            }
            return "";
        }
        //关闭
        function closedgShowData() {
            $('#dlg').dialog('close');
        }
        //删除出库的数据
        function dblClickDelCargo(Did) {
            var row = $("#outDg").datagrid('getData').rows[Did];
            var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
            var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
            n--;
            var pt = p - row.Piece;
            $('#DisplayNum').val(Number(n));
            $('#DisplayPiece').val(Number(pt));
            var InHousePrice = Number(row.InHousePrice);//销售价
            TransportFee -= InHousePrice * row.Piece;
            $('#TransportFee').numberbox('setValue', TransportFee);
            $('#TotalCharge').numberbox('setValue', TransportFee);
            $('#Total').numberbox('setValue', pt);

            var title = "进仓单     已拉上：" + n + "票，总件数：" + pt + " 件";
            $('#outDg').datagrid("getPanel").panel("setTitle", title);

            var index = $('#dg').datagrid('getRowIndex', row.SID);
            if (index >= 0) {
                var Trow = $("#dg").datagrid('getData').rows[index];
                Trow.Piece = Trow.InPiece;
                $('#dg').datagrid('updateRow', { index: index, row: Trow });
            }
            var index = $('#outDg').datagrid('getRowIndex', row);
            $('#outDg').datagrid('deleteRow', index);
        }
        //保存订单
        function saveOutCargo() {
            //取消业务员禁用方便后台取值
            var rows = $('#outDg').datagrid('getRows');
            if (rows == null || rows == "") { $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请选择要保存的数据！', 'warning'); return; }

            $.messager.confirm('<%= Supplier.Common.GetSystemNameAndVersion()%>', '确定保存？', function (r) {
                if (r) {
                    //启用复选框用于后台数据获取
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    $('#btnSave').linkbutton('disable');
                    var json = JSON.stringify(rows);

                    $('#fmDep').form('submit', {
                        url: 'houseApi.aspx?method=SavePurchaseOrderData',
                        contentType: "application/json;charset=utf-8",
                        type: 'post', dataType: 'json', data: { data: json },
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
                            var text = eval('(' + msg + ')');
                            if (text.Result == true) {
                                alert_autoClose('<%= Supplier.Common.GetSystemNameAndVersion()%>', '保存成功！', 'info');
                                $('#outDg').datagrid('loadData', { total: 0, rows: [] });
                                TransportFee = 0;
                                n = 0;
                                pt = 0;
                                $('#TransportFee').numberbox('setValue', TransportFee);
                                $('#TotalCharge').numberbox('setValue', TransportFee);
                                $('#Total').numberbox('setValue', TransportFee);
                                $('#DisplayPiece').val(0);
                            }
                            else {
                                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    })
                }
            });
        }
    </script>
</asp:Content>
