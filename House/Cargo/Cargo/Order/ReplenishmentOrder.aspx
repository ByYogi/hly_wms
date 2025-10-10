<%@ Page Title="补货单" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MoveOrderManager.aspx.cs" Inherits="Cargo.Order.MoveOrderManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- <script src="../JS/easy/js/datagrid-cellediting.js" type="text/javascript"></script> --%>
    <style type="text/css">
        .tblBtn{
            letter-spacing: 4px; /* 字间距可调 */
            padding: 0 2px;
        }
        .space{
            display: inline-flex;       /* 横向排列 */
            gap: 8px;           /* 按钮间距统一由 gap 控制 */
            align-items: center;  /* 垂直居中 */
            vertical-align: middle; /* 与其他行内元素对齐，消除多余上间距 */
        }
        .space-lg{
            display: inline-flex;       /* 横向排列 */
            gap: 22px;           /* 按钮间距统一由 gap 控制 */
            align-items: center;  /* 垂直居中 */
            vertical-align: middle; /* 与其他行内元素对齐，消除多余上间距 */
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
            $.ajaxSetup({ async: true });
            $.getJSON("../Client/clientApi.aspx?method=QueryAllUpClientDep", function (data) {
                UpClientDep = data;
            });
        }
        $(window).resize(function () {
            adjustment();
        });
        function adjustment() {
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true);
            $('#dg').datagrid("resize", { height: height });
        }
        $(document).ready(function () {
            $('#dg').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中, 请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                showFooter: true,
                pageSize: 28, //每页多少条
                pageList: [28, 50, 500, 1000, 10000, 100000],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'RplID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    { title: '', field: 'RplID', checkbox: true, width: '30px' },
                    {
                        title: '补货单号', field: 'RplNo', width: '130px'
                    },
                    {
                        title: '缺货仓库', field: 'FromHouseName', width: '100px'
                    },
                    {
                        title: '补货仓库', field: 'HouseName', width: '100px'
                    },
                    {
                        title: '缺货数量', field: 'Piece', width: '100px'
                    },
                    {
                        title: '开单人', field: 'UserName', width: '100px'
                    },
                    {
                        title: '补货单状态', field: 'Status', width: '100px',
                        formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='待处理'>已开单</span>"; }
                            else if (val == "1") { return "<span title='补货中'>补货中</span>"; }
                            else if (val == "2") { return "<span title='已完成'>已完成</span>"; }
                            else { return ""; }
                        }
                    },
                    {
                        title: '补货品牌', field: 'TypeNames', width: '200px'
                    },
                    { title: '备注', field: 'Remark', width: '200px'},
                    { title: '开单时间', field: 'CreateDate', width: '130px', formatter: DateTimeFormatter },
                    // { title: '完成时间', field: 'CompletedDate', width: '130px', formatter: DateTimeFormatter },
                    // { title: '花费天数', field: 'SpendDays', width: '130px'}
                ]],
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                rowStyler: function (index, row) {
                    if (row.MoveStatus == "2") { return "color:#2a83de"; };
                    if (row.MoveStatus == "1" || row.MoveStatus == "3") { return "background-color:#f7e4e0"; };
                    if (row.MoveStatus == "1" || row.MoveStatus == "3" || row.MoveStatus == "4") { return "background-color:#FCF3CF"; };

                },
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { editByID(index); }
            });
            //一级产品
            $('#TypeCate').combobox({
                url: '../Product/productApi.aspx?method=QueryALLOneProductType', valueField: 'TypeID', textField: 'TypeName',
                onSelect: function (rec) {
                    $('#TypeID').combobox('clear');
                    $('#TypeID').combobox({
                        url: '../Product/productApi.aspx?method=QueryALLOneProductType&PID=' + rec.TypeID, valueField: 'TypeID', textField: 'TypeName', AddField: 'PinyinName',
                        filter: function (q, row) {
                            var opts = $(this).combobox('options');
                            return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                        },
                    });
                }
            });

            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            <%-- $('#StartDate').datebox('textbox').bind('focus', function () { $('#StartDate').datebox('showPanel'); });
            $('#EndDate').datebox('textbox').bind('focus', function () { $('#EndDate').datebox('showPanel'); }); --%>

            $('#HouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse', valueField: 'HouseID', textField: 'Name'
            });
            $('#FromHouse').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse', valueField: 'HouseID', textField: 'Name'
            });
            $('#HouseID').combobox('textbox').bind('focus', function () { $('#HouseID').combobox('showPanel'); });
            $('#FromHouse').combobox('textbox').bind('focus', function () { $('#FromHouse').combobox('showPanel'); });
            $('#HouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=QueryRplOrder';
            $('#dg').datagrid('load', {
                RplNo: $('#RplNo').val(),
                HouseID: $("#HouseID").combobox('getValue'),
                FromHouse: $("#FromHouse").combobox('getValue'),
                Status: $("#Status").combobox('getValue'),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                TypeCate: $('#TypeCate').datebox('getValue'),
                TypeID: $('#TypeID').datebox('getValue') 
            });
            console.log(gridOpts.url);
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
                <td style="text-align: right;">补货单号:
                </td>
                <td>
                    <input id="RplNo" class="easyui-textbox" data-options="prompt:'请输入移库单号'" style="width: 120px">
                </td>
                <%-- <td style="text-align: right;">规格:
                </td> --%>
                <%-- <td>
                    <input id="Specs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 120px">
                </td> --%>
                <td style="text-align: right;">补货仓库:
                </td>
                <td>
                    <input id="HouseID" class="easyui-combobox" style="width: 120px;"  />
                </td>
                <td style="text-align: right;">缺货仓库:
                </td>
                <td>
                    <input id="FromHouse" class="easyui-combobox" style="width: 120px;"  />
                </td>
                <td style="text-align: right;">开单时间:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 120px" />~
                    <input id="EndDate" class="easyui-datebox" style="width: 120px" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">补货单状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="Status" style="width: 120px;" >
                        <option value="">全部</option>
                        <option value="0">已开单</option>
                        <option value="1">处理中</option>
                        <option value="2">已完成</option>
                    </select>
                </td>
                <td style="text-align: right;">一级产品:
                </td>
                <td>
                    <input id="TypeCate" class="easyui-combobox" style="width: 120px;"  />
                </td>
                <td style="text-align: right;">二级产品:
                </td>
                <td>
                    <input id="TypeID" class="easyui-combobox" style="width: 120px;" data-options="valueField:'TypeID',textField:'TypeName'" />
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid"></table>
    <div id="toolbar">
    <div class="space-lg">
        <span class="space">
            <a href="#" class="easyui-linkbutton tblBtn" id="btnSearch" iconcls="icon-search" plain="false">查询</a>
        </span>
        <span class="space">
        <!-- <a href="#" class="easyui-linkbutton tblBtn" id="btnAdd" iconcls="icon-add" plain="false" >新增</a> -->
        <!-- <a href="#" class="easyui-linkbutton tblBtn" id="btnEdit" iconcls="icon-edit" plain="false" >修改</a> -->
        <a href="#" class="easyui-linkbutton tblBtn" id="btnCancel" iconcls="icon-cut" plain="false" >作废</a>
        <!-- <a href="#" class="easyui-linkbutton tblBtn" id="btnDel" iconcls="icon-cut" plain="false" >合并补货单</a>
        <a href="#" class="easyui-linkbutton tblBtn" id="btnDel" iconcls="icon-cut" plain="false" >转为移库单</a> -->
        <a href="#" class="easyui-linkbutton tblBtn" id="btnExport" iconcls="icon-application_put" plain="false">导出</a>
        </span>
    </div>
   
        <form runat="server" id="fm1">
            <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
        </form>
    </div>



    <div id="dlgOrder" class="easyui-dialog"  buttons="#dlgOrder-buttons" closable="true">
       
        <table>
            <tr>
                <td>
                    <input type="hidden" id="HiddenMoveStatus" />
                    <table id="dgSave" class="easyui-datagrid">
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="dlgOrder-buttons">
        <table style="width: 100%">
            <tr>
                <td>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgOrder').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>

    <!--Begin 查询产品标签列表-->
    <div id="productTag" class="easyui-dialog" style="width: 1100px; height: 520px; padding: 1px"
        closed="true" buttons="#productTag-buttons">
        <table id="dgTag" class="easyui-datagrid">
        </table>
        <div id="productTag-buttons">

            <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#productTag').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
        </div>
    </div>
    <!--End 查询产品标签列表-->

    <script type="text/javascript">
        //Easyui 初始化
        $(function(){
            $('#dlgOrder').dialog({
                modal: true,
                title: '补货单明细',
                width: 900,
                closed: true
            });

            const columns = [{
                    title: '产品代码', field: 'ProductCode', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '产品名称', field: 'ProductName', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '货品代码', field: 'GoodsCode', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '品牌', field: 'TypeName', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '花纹', field: 'Figure', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '载速', field: 'LISS', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '缺货数量', field: 'Piece', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '完成数量', field: 'DonePiece', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }]
            $('#dgSave').datagrid({
                width: '880px',
                height: '300px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '',
                onClickCell: onClickCell,
                columns: [columns],
                rowStyler: function (index, row) {
                    // if (row.Piece != row.NewPiece) { return "background-color:#f7d9d9"; };
                }
            });
        })
        //统一事件绑定
        $(function() {
            $('#btnCancel').on('click', cancelHandle);
            $('#btnExport').on('click', dataExport);
            $('#btnSearch').on('click', dosearch)
        });
        
        function editHandle() {
            <%-- 
            onMove: function(left, top) {
        var parentWidth = $(window).width();
        var parentHeight = $(window).height();
        var dialogWidth = $(this).d
        
        
        
        ialog('options').width;
        var dialogHeight = $(this).dialog('options').height;

        // 限制水平位置
        if (left < 0) left = 0;
        if (left + dialogWidth > parentWidth) left = parentWidth - dialogWidth;

        // 限制垂直位置
        if (top < 0) top = 0;
        if (top + dialogHeight > parentHeight) top = parentHeight - dialogHeight;

        $(this).dialog('move', left, top);
    } --%>
        }
        function cancelHandle(){
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "" || rows.length == 0) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要作废的数据！', 'warning');
                return;
            }
            var rplIDs = rows.map(x => x.RplID);
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定作废？', function (r) {
                if (r) {
                    $.ajax({
                        url: 'orderApi.aspx?method=CancelRplOrder',
                        type: 'post', 
                        dataType: 'json', 
                        data: { RplIDs: JSON.stringify(rplIDs) },
                        success: function (resps) {
                            if (resps.Success) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '作废成功!', 'info');
                                $('#dg').datagrid('clearSelections');
                                $('#dg').datagrid('reload');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', resps.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }

        
        //导出数据
        function dataExport() {
            var pager  = $('#dg').datagrid('getPager');
            var pageData = pager.pagination('options');
            var pageIndex = pageData.pageNumber;  // 当前页码（从1开始）
            var pageSize = pageData.pageSize;     // 每页大小
            var queryParams = {
                RplNo: $('#RplNo').val(),
                HouseID: $("#HouseID").combobox('getValue'),
                FromHouse: $("#FromHouse").combobox('getValue'),
                Status: $("#Status").combobox('getValue'),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                TypeCate: $('#TypeCate').datebox('getValue'),
                TypeID: $('#TypeID').datebox('getValue'),
                page: pageIndex,
                rows: pageSize
            }

            console.log({queryParams,pageData});
            var queryString = Object.keys(queryParams)
                .map(key => encodeURIComponent(key) + '=' + encodeURIComponent(queryParams[key]))
                .join('&');
            var downloadUrl = 'orderApi.aspx?method=GetRplOrderExcel&' + queryString;
            
            // 创建隐藏的iframe
            var iframe = document.createElement('iframe');
            iframe.style.display = 'none';
            iframe.src = downloadUrl;
            document.body.appendChild(iframe);
            return
        }
        //双击显示订单详细界面 
        function editByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            $("#HiddenMoveStatus").val(row.MoveStatus);
            if (row) {
                $('#dlgOrder').dialog('open').dialog('setTitle', ' 补货明细 ' + row.RplNo).dialog('center');
                
                

                var gridOpts = $('#dgSave').datagrid('options');
                gridOpts.url = 'orderApi.aspx?method=QueryRplOrderGoods&RplID=' + row.RplID;
                $('#dgSave').datagrid('load');
            }
        }

        //删除仓库信息
        function DelItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'orderApi.aspx?method=DelMoveOrder',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
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

        //保存仓库信息
        function saveItem() {
            $('#fm').form('submit', {
                url: 'houseApi.aspx?method=SaveCargoHouse',
                onSubmit: function () {
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $('#dlgOrder').dialog('close'); 	// close the dialog
                        dosearch();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
        var IsModifyOrder = false;
        var editIndex = undefined;
        function endEditing() {
            if (editIndex == undefined) { return true }
            if ($('#dgSave').datagrid('validateRow', editIndex)) {
                var rows = $("#dgSave").datagrid('getData').rows[editIndex];
                var ed = $('#dgSave').datagrid('getEditors', editIndex);
                var cg = ed[0];
                if (cg == undefined) { return false; }
                if (cg.field == "Piece") {
                    //修改件数
                    var oldPiece = Number(rows.Piece);
                    var newPiece = Number(cg.target.val());
                    if (oldPiece == newPiece) {
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        return;
                    }
                    //修改件数
                    rows.Piece = newPiece;
                    rows.oldPiece = oldPiece;
                    var json = JSON.stringify([rows])
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    $.ajax({
                        url: 'orderApi.aspx?method=UpdateMoveOrderPiece',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            $.messager.progress("close");
                            if (text.Result == true) {
                                IsModifyOrder = true;
                                alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                $('#dgSave').datagrid('rejectChanges');
                                editIndex = undefined;
                            }
                        }
                    });
                }
                $('#dgSave').datagrid('endEdit', editIndex);
                editIndex = undefined;
                return true;
            } else {
                return false;
            }
        }
        function onClickCell(index, field) {
            if ($("#HiddenMoveStatus").val() == "1" || $("#HiddenMoveStatus").val() == "2" || $("#HiddenMoveStatus").val() == "4") { return; }
            var rows = $("#dgSave").datagrid('getData').rows[index];
            if (field == "Piece") {
                if (endEditing()) {
                    $('#dgSave').datagrid('selectRow', index)
                        .datagrid('editCell', { index: index, field: field });
                    editIndex = index;
                }
            } else {
                if (editIndex == undefined) { return true }
                rows = $("#dgSave").datagrid('getData').rows[editIndex];
                var ed = $('#dgSave').datagrid('getEditors', editIndex);
                var cg = ed[0];
                if (cg == undefined) {
                    return true;
                }
                var sum = 0;
                if (cg.field == "Piece") {
                    var oldPiece = Number(rows.Piece);
                    var newPiece = Number(cg.target.val());
                    if (oldPiece == newPiece) {
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        return;
                    }
                    //修改件数
                    rows.Piece = newPiece;
                    rows.oldPiece = oldPiece;

                    var json = JSON.stringify([rows])
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    $.ajax({
                        url: 'orderApi.aspx?method=UpdateMoveOrderPiece',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                IsModifyOrder = true;
                                alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                $('#dgSave').datagrid('rejectChanges');
                                editIndex = undefined;
                            }
                        }
                    });
                }
                $('#dgSave').datagrid('endEdit', editIndex);
                editIndex = undefined;
            }
        }


    </script>

</asp:Content>
