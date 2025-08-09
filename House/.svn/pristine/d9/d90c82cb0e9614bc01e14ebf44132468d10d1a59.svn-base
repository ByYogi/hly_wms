<%@ Page Title="产品资料" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductInformation.aspx.cs" Inherits="Supplier.Basic.ProductInformation" %>

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
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'SID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    { title: '', field: 'SID', checkbox: true, width: '30px' },
                    {
                        title: '入仓仓库', field: 'HouseName', width: '90px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '品牌', field: 'TypeName', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '产品编码', field: 'ProductCode', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '规格', field: 'Specs', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '花纹', field: 'Figure', width: '200px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '货品代码', field: 'GoodsCode', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '载速', field: 'LoadIndex', width: '90px', formatter: function (value, row) {
                            return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                        }
                    },
                    //{
                    //    title: '速度', field: 'SpeedLevel', width: '70px', formatter: function (value) {
                    //        return "<span title='" + value + "'>" + value + "</span>";
                    //    }
                    //},
                    //{
                    //    title: '产地', field: 'Born', width: '70px', formatter: function (value) {
                    //        if (value == "0") { return "<span title='国产'>国产</span>"; }
                    //        else if (value == "1") { return "<span title='进口'>进口</span>"; } else { return ""; }
                    //    }
                    //},
                    {
                        title: '进仓成本价', field: 'UnitPrice', width: '90px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },

                    { title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { /*editItemPrice(index);*/ }
            });
           <%-- var ln = "<%=UserInfor.LoginName%>";
            if (ln == "551098" || ln == "542207" || ln == "130453" || ln == "891524") {
                $('#btnPlInhouse').hide();
                $('#btnImport').hide();
            } else {
                $('#btnPlInhouse').show();
                $('#btnImport').show();

            }--%>

            //初始加载 仓库、品牌(一级产品)
            var topMenuNew = [];
            var SettleHouseID = '<%= UserInfor.SettleHouseID%>'.split(',');
            var SettleHouseName = '<%= UserInfor.SettleHouseName%>'.split(',');
            for (var i = 0; i < SettleHouseID.length; i++) {
                topMenuNew.push({ "text": SettleHouseName[i], "id": SettleHouseID[i] });
            }
            $("#QHouseID").combobox("loadData", topMenuNew);
            $('#QHouseID').combobox('textbox').bind('focus', function () { $('#QHouseID').combobox('showPanel'); });
            $('#QHouseID').combobox('setValue', "<%= UserInfor.SettleHouseID%>");
            topMenuNew = [];
            var ClientTypeID = '<%= UserInfor.ClientTypeID%>'.split(',');
            var ClientTypeName = '<%= UserInfor.ClientTypeName%>'.split(',');
            for (var i = 0; i < ClientTypeID.length; i++) {
                topMenuNew.push({ "text": ClientTypeName[i], "id": ClientTypeID[i] });
            }
            $("#QBarnId").combobox("loadData", topMenuNew);
            $('#QBarnId').combobox('textbox').bind('focus', function () { $('#QBarnId').combobox('showPanel'); });

            $('#QSpecs').textbox('textbox').keydown(function (e) {
                if (e.keyCode == 13) {
                    dosearch();
                }
            });
            $('#QFigure').textbox('textbox').keydown(function (e) {
                if (e.keyCode == 13) {
                    dosearch();
                }
            });
        });


        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'BasicApi.aspx?method=QueryALLProductPageData';
            $('#dg').datagrid('load', {
                QBarnId: $('#QBarnId').combobox('getValue'),
                QSpecs: $('#QSpecs').val(),
                QFigure: $('#QFigure').val(),
                QProductCode: $('#QProductCode').val(),
                QHouseID: $('#QHouseID').combobox('getValue'),
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--列表显示--%>
    <div id='Loading' style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 98%; height: 100%; background: white; text-align: center; padding: 5px 10px; display: table;">
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
                    <input id="QBarnId" class="easyui-combobox" style="width: 90px;" data-options="valueField:'id',textField:'text'" />
                </td>
                <td style="text-align: right;">规格:
                </td>
                <td>
                    <input id="QSpecs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 100px" />
                </td>
                <td style="text-align: right;">花纹:
                </td>
                <td>
                    <input id="QFigure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 100px" />
                </td>
                <td style="text-align: right;">产品编码:
                </td>
                <td>
                    <input id="QProductCode" class="easyui-textbox" data-options="prompt:'请输入产品编码'" style="width: 120px" />
                </td>

                <td style="text-align: right;">入驻仓库：</td>
                <td>
                    <input name="QHouseID" id="QHouseID" style="width: 100px;" class="easyui-combobox" data-options="valueField:'id',textField:'text',editable:false,required:true" />
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
      <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="editItemPrice()" id="btnPlInhouse">&nbsp;修改进仓价&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-in_cargo" plain="false" onclick="Import()" id="btnImport">&nbsp;批量修改进仓价&nbsp;</a>&nbsp;&nbsp;
          <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="ExportShopOrderImport()" id="btnExportShopOrderImport">&nbsp;导&nbsp;出&nbsp;</a>&nbsp;&nbsp;
           <form runat="server" id="fm1">
               <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
           </form>
    </div>
    <%--此div用于导入数据--%>
    <div id="dlgCost" class="easyui-dialog" style="width: 1000px; height: 530px; padding: 2px 2px" closed="true">
        <table id="dgImport" class="easyui-datagrid">
        </table>
        <div id="dginporttoolbar">
            <input type="file" id="fileT" name="file" accept=".xls" onchange="saveFile()" style="width: 250px;" />
            <input type="hidden" id="ExistCount" />
            <a href="#" id="btnload" class="easyui-linkbutton" iconcls="icon-out_cargo" plain="false" onclick="saveFile()">&nbsp;重新上传&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-ok" plain="false" onclick="btnSaveData()">&nbsp;保存数据&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closeDlgStatus()">&nbsp;关&nbsp;闭&nbsp;</a>
        </div>
    </div>
    <%--此div用于导入数据--%>

    <%--修改单价模态框--%>
    <div id="dlg" class="easyui-dialog" style="width: 370px; height: 300px; padding: 5px 5px" closed="true" buttons="#dlg-buttons">
        <form id="fmEditPrice" class="easyui-form" method="post" enctype="multipart/form-data">
            <input type="hidden" name="SID" id="SID" />
            <input type="hidden" name="HouseID" id="HouseID" />
            <input type="hidden" name="ProductCode" id="ProductCode" />
            <div id="editPanel">
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: right;">进仓价格:
                        </td>
                        <td>
                            <input id="UnitPrice" name="UnitPrice" data-options="min:0,precision:2" style="width: 200px;" class="easyui-numberbox" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">品牌:
                        </td>
                        <td>
                            <input id="TypeID" name="TypeID" class="easyui-combobox" style="width: 200px;" data-options="valueField:'TypeID',textField:'TypeName',url:'basicApi.aspx?method=QueryALLOneProductType&PID=1',disabled:true" panelheight="auto" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" id="lSpecs">规格:
                        </td>
                        <td>
                            <input name="Specs" id="Specs" class="easyui-textbox" data-options="prompt:'请输入规格',required:true,disabled:true" style="width: 200px;" />
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right;" id="lFigure">花纹:
                        </td>
                        <td>
                            <input name="Figure" id="Figure" class="easyui-textbox" data-options="required:true,disabled:true" style="width: 200px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">载重指数:
                        </td>
                        <td>
                            <input name="LoadIndex" id="LoadIndex" class="easyui-textbox" data-options="min:0,precision:0,disabled:true" style="width: 200px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">速度级别:
                        </td>
                        <td>
                            <input class="easyui-combobox" name="SpeedLevel" id="SpeedLevel" data-options="url:'../Data/TyreSpeedLevel.json',method:'get',valueField:'id',textField:'text',disabled:true" style="width: 200px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">产地:
                        </td>
                        <td>
                            <select class="easyui-combobox" id="Born" name="Born" data-options="disabled:true" style="width: 200px;" panelheight="auto">
                                <option value="0">国产</option>
                                <option value="1">进口</option>
                            </select>
                        </td>
                    </tr>

                </table>
            </div>

        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">&nbsp;&nbsp;保&nbsp;存&nbsp;&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;&nbsp;取&nbsp;消&nbsp;&nbsp;</a>
    </div>

    <script type="text/javascript">
        //保存数据
        function btnSaveData() {
            var rows = $("#dgImport").datagrid('getData').rows;
            if (rows == null || rows == "") {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请先导入需要修改的数据！', 'warning');
                return;
            }
            var msg = "确定修改进仓价？";
            $.messager.confirm('<%= Supplier.Common.GetSystemNameAndVersion()%>', msg, function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'BasicApi.aspx?method=SaveSupplierCostData',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            if (text.Result == true) {
                                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                                closeDlgStatus();
                                dosearch();
                            }
                            else {
                                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }
        //关闭数据导入弹出框
        function closeDlgStatus() {
            $('#dlgCost').dialog('close');
            $('#dgImport').datagrid('loadData', { total: 0, rows: [] });
            $('#fileT').val("");
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
                url: 'BasicApi.aspx?method=saveFile',
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
        //导入
        function Import() {
            $('#dlgCost').dialog('open').dialog('setTitle', '批量导入修改进仓价');
            showData();
            $('#dgImport').datagrid('loadData', { total: 0, rows: [] });
        }

        //显示DataGrid数据列表
        function showData() {
            var columns = [];
            columns.push({ title: '品牌', field: 'TypeName', width: '70px' });
            columns.push({ title: '产品编码', field: 'ProductCode', width: '100px' });
            columns.push({ title: '规格', field: 'Specs', width: '100px' });
            columns.push({ title: '花纹', field: 'Figure', width: '120px' });
            columns.push({ title: '载速', field: 'LoadIndex', width: '80px' });
            columns.push({ title: '进仓价', field: 'UnitPrice', width: '80px', align: 'right' });

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

        function ExportShopOrderImport() {
            $.ajax({
                url: "BasicApi.aspx?method=QueryALLProductPageDataExport",
                data: {
                    "QBarnId": $('#QBarnId').combobox('getValue'),
                    "QSpecs": $('#QSpecs').val(),
                    "QFigure": $('#QFigure').val(),
                    "QProductCode": $('#QProductCode').val(),
                    "QHouseID": $('#QHouseID').combobox('getValue')
                },
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click() }
                    else { $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }
        //修改单价
        function editItemPrice(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row == null || row == "") {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改产品进仓价格');
                $('#fmEditPrice').form('load', row);
                $('#HouseID').val(row.HouseID);
                $('#SID').val(row.SID);
                $('#ProductCode').val(row.ProductCode);
            }
        }
        //修改单价
        function editItemPrice() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改产品进仓价格');
                $('#fmEditPrice').form('load', row);
                $('#HouseID').val(row.HouseID);
                $('#SID').val(row.SID);
                $('#ProductCode').val(row.ProductCode);
            }
        }

        //保存单价
        function saveItem() {
            var price = $('#UnitPrice').val();
            if (price == undefined || price == "") {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '进仓价格不能为空!', 'warning');
                return;
            }
            $('#fmEditPrice').form('submit', {
                url: 'basicApi.aspx?method=UpdateSupplierProductPrice',
                onSubmit: function (param) {
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $('#dlg').dialog('close'); 	// close the dialog
                        dosearch();
                    } else {
                        $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }

    </script>
</asp:Content>



