<%@ Page Title="我的审批" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyCheck.aspx.cs" Inherits="Cargo.systempage.MyCheck" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .commTblStyle_8 {
        }

            .commTblStyle_8 th {
                border: 1px solid rgb(205, 205, 205);
                text-align: center;
                color: rgb(255, 255, 255);
                line-height: 28px;
                background-color: rgb(15, 114, 171);
            }

            .commTblStyle_8 tr {
            }

                .commTblStyle_8 tr.BlankRow td {
                    line-height: 10px;
                }

                .commTblStyle_8 tr td {
                    border: 1px solid rgb(205, 205, 205);
                    text-align: center;
                    line-height: 20px;
                }

                    .commTblStyle_8 tr td.left {
                        text-align: right;
                        padding-right: 10px;
                        font-weight: bold;
                        white-space: nowrap;
                        background-color: rgb(239, 239, 239);
                    }

                    .commTblStyle_8 tr td.right {
                        text-align: left;
                        padding-left: 10px;
                    }

            .commTblStyle_8 .whiteback {
                background-color: rgb(255, 255, 255);
            }

        /*流程图样式*/
        .processBar {
            float: left;
            width: 100px;
            margin-top: 15px;
        }

            .processBar .bar {
                background: rgb(230, 224, 236);
                height: 3px;
                position: relative;
                width: 100px;
                margin-left: 10px;
            }

            .processBar .b-select {
                background: rgb(96, 72, 124);
            }

            .processBar .bar .c-step {
                position: absolute;
                width: 8px;
                height: 8px;
                border-radius: 50%;
                background: rgb(230, 224, 236);
                left: -12px;
                top: 50%;
                margin-top: -4px;
            }

            .processBar .bar .c-select {
                width: 10px;
                height: 10px;
                margin: -5px -1px;
                background: rgb(96, 72, 124);
            }

        .main-hide {
            position: absolute;
            top: -9999px;
            left: -9999px;
        }

        .poetry {
            color: rgb(41, 41, 41);
            font-family: KaiTi_GB2312, KaiTi, STKaiti;
            font-size: 16px;
            background-color: transparent;
            font-weight: bold;
            font-style: normal;
            text-decoration: none;
        }

        button {
            width: 80px;
            line-height: 30px;
            font-size: 11px;
            color: rgb(116, 42, 149);
            text-align: center;
            border-radius: 6px;
            border: 1px solid #e2e2e2;
            cursor: pointer;
            background-color: #fff;
            outline: none;
        }

            button:hover {
                border: 1px solid rgb(179, 161, 200);
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
        $(document).ready(function () {
            $("#dg").datagrid({
                width: '100%',
                //height: '320px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                  { title: '', field: '', checkbox: true, width: '30px' },
                  { title: '申请单号', field: 'ID', width: '60px' },
                  { title: '申请标题', field: 'Title', width: '200px' },
                  { title: '申请人', field: 'ApplyName', width: '60px' },
                  { title: '申请内容', field: 'Memo', width: '300px' },
                  {
                      title: '审批状态', field: 'ApplyStatus', width: '60px', formatter: function (val, row, index) {
                          if (val == "0") { return "待审"; }
                          else if (val == "1") { return "审批中"; }
                          else if (val == "2") { return "拒审"; }
                          else if (val == "3") { return "结束"; }
                      }
                  },
                  { title: '上一审批人', field: 'CurrName', width: '100px' },
                  { title: '上一审批时间', field: 'CheckTime', width: '130px', formatter: DateTimeFormatter },
                  //{ title: '当前审批人', field: 'NextCheckName', width: '100px' },
                  {
                      title: '申请类型', field: 'ApplyType', width: '80px', formatter: function (val, row, index) {
                          if (val == "1") { return "油卡充值"; }
                          else if (val == "2") { return "轮胎外调"; }
                      }
                  },
                 { title: '申请时间', field: 'ApplyDate', width: '130px', formatter: DateTimeFormatter }
                ]],
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'sysService.aspx?method=QueryMyCheck';
            $('#dg').datagrid('load', {
                Title: $('#ATitle').val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                ApplyType: $("#AApplyType").combobox('getValue'),
                ApplyStatus: $("#AApplyStatus").combobox('getValue')
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
    <div id="saPanel" class="easyui-panel" title="" data-options="iconCls:'icon-search'">
        <table>
            <tr>
                <td style="text-align: right; width: 60px;">申请标题:
                </td>
                <td>
                    <input id="ATitle" class="easyui-textbox" data-options="prompt:'请输入申请标题'" style="width: 100px">
                </td>
                <td style="text-align: right; width: 60px;">申请时间:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px">
                </td>

                <td style="text-align: right; width: 60px;">申请类型:
                </td>
                <td>
                    <select class="easyui-combobox" id="AApplyType" style="width: 100px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="2">轮胎外调</option>
                    </select>
                </td>
                <td style="text-align: right; width: 60px;">审批状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="AApplyStatus" style="width: 70px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">待审</option>
                        <option value="1">审批中</option>
                        <option value="2">拒审</option>
                        <option value="3">结束</option>
                    </select>
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-lock_add"
            plain="false" onclick="plcheck()">&nbsp;审&nbsp;批&nbsp;</a>&nbsp;&nbsp;<a
                href="#" class="easyui-linkbutton" iconcls="icon-sitemap_color" plain="false"
                onclick="Proc()">&nbsp;审批流程图&nbsp;</a>
    </div>
    <div id="dlgProc" class="easyui-dialog" style="width: 800px; height: 470px; padding: 10px 10px"
        closed="true" buttons="#dlgProc-buttons">
        <div id="lblTrack">
        </div>
    </div>
    <div id="dlgProc-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgProc').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <div id="dlgDeny" class="easyui-dialog" style="width: 1000px; height: 600px;"
        closed="true" buttons="#dlgDeny-buttons">
        <input type="hidden" id="AID" />
        <div id="saPanel">
            <table>
                <tr id="appNR">
                </tr>
                <tr>
                    <td style="text-align: right;">申请备注:</td>
                    <td colspan="7"><span id="AMemo"></span></td>
                </tr>
                <tr>
                    <td style="text-align: right; color: red;">审批意见:
                    </td>
                    <td colspan="7">
                        <textarea id="DenyReason" rows="3" name="DenyReason" style="width: 800px;"></textarea>
                    </td>
                </tr>
            </table>
        </div>
        <table id="dgCheckAwb" class="easyui-datagrid">
        </table>
        <div id="File" class="easyui-panel" title="申请文件列表" style="height: 100px;">
            <div id="dvFile"></div>
        </div>
        <div id="Route" class="easyui-panel" title="审批流程" style="height: 160px;">
            <div id="dvRoute">
            </div>
        </div>
    </div>
    <div id="dlgDeny-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="setCheck()">&nbsp;&nbsp;通过审批&nbsp;&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-no" onclick="setNoCheck()">&nbsp;拒&nbsp;&nbsp;审&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgDeny').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <script type="text/javascript">
        //审批
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlgDeny').dialog('open').dialog('setTitle', '申请审批');
                //$('#fm').form('load', row);
                $('#AID').val(row.ID);
                $('#AMemo').html(row.Memo);
                if (row.ApplyType == "4") {
                    //加班申请
                    $('#File').hide();
                    $('#appNR').html("<td style=\"text-align: right;\">申请人员:</td><td>" + row.ApplyName + "</td><td style=\"text-align: right;\">申请日期:</td><td>" + DateTimeFormatter(row.ApplyDate) + "</td><td style=\"text-align: right;\">加班时间:</td><td>" + DateTimeFormatter(row.OStartTime) + "---" + DateTimeFormatter(row.OEndTime) + "</td><td style=\"text-align: right;\">加班时长:</td><td>" + row.OTime + "小时</td>");
                } else if (row.ApplyType == "2") {
                    //轮胎外调
                    $('#File').show();
                    $('#appNR').html("<td style=\"text-align: right;\">申请人员:</td><td>" + row.ApplyName + "</td><td style=\"text-align: right;\">申请日期:</td><td>" + DateTimeFormatter(row.ApplyDate) + "</td><td style=\"text-align: right;\">外调仓库:</td><td>" + row.ThrowHouseName + "</td><td style=\"text-align: right;\">客户姓名:</td><td>" + row.ClientName + "</td>");

                    ShowDG();
                    //$('#dgCheckAwb').datagrid('load', row.RelateList);
                    var gridOpts = $('#dgCheckAwb').datagrid('options');
                    gridOpts.url = 'sysService.aspx?method=QueryApproveRelateList&ApproveID=' + row.ID + '&ApplyType=' + row.ApplyType;
                    var fl = "";
                    if (row.FileList.length > 0) {
                        for (var i = 0; i < row.FileList.length; i++) {
                            fl += "<a href=../" + row.FileList[i].FileName + ">" + row.FileList[i].FileName + "</a>&nbsp;&nbsp;&nbsp;&nbsp;";
                        }
                    }
                    $('#dvFile').html(fl);
                }
                //$('#AApplyName').html(row.ApplyName);
                //$('#AApplyDate').html(DateTimeFormatter(row.ApplyDate));
                //$('#AThrowHouseName').html(row.ThrowHouseName);
                //$('#AClientName').html(row.ClientName);
                $.ajax({
                    url: "sysService.aspx?method=QueryApproveRout&id=" + row.ID + "&ApproveType=" + row.ApplyType,
                    cache: false,
                    success: function (text) {
                        var ldl = document.getElementById("dvRoute");
                        ldl.innerHTML = text;
                    }
                });
            }
        }
        //显示列表
        function ShowDG() {
            var columns = [];
            //columns.push({ title: '', field: 'ID', checkbox: true, width: '30px' });
            columns.push({ title: '外调件数', field: 'ThrowNum', width: '60px', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" } });
            columns.push({ title: '外调价格', field: 'ThrowCharge', width: '60px', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" } });
            columns.push({ title: '销售价', field: 'SalePrice', width: '60px' });
            columns.push({ title: '规格', field: 'Specs', width: '80px' });
            columns.push({ title: '花纹', field: 'Figure', width: '100px' });
            columns.push({ title: '批次', field: 'Batch', width: '50px' });
            columns.push({ title: '型号', field: 'Model', width: '60px' });
            columns.push({ title: '载重指数', field: 'LoadIndex', width: '60px' });
            columns.push({ title: '速度级别', field: 'SpeedLevel', width: '60px' });
            columns.push({ title: '货位代码', field: 'ContainerCode', width: '80px' });
            columns.push({ title: '所在区域', field: 'AreaName', width: '60px' });
            columns.push({ title: '所在仓库', field: 'HouseName', width: '80px' });
            columns.push({ title: '产品类型', field: 'TypeName', width: '60px' });
            //columns.push({ title: '产品名称', field: 'ProductName', width: '100px' });
            //columns.push({ title: '入库时间', field: 'InHouseTime', width: '130px', formatter: DateTimeFormatter });
            $('#dgCheckAwb').datagrid({
                width: '100%',
                height: '150px',
                enableEditing: true,
                title: '外调轮胎明细', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                rownumbers: true,
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '',
                columns: [columns]
            });
        }
        //拒审
        function setNoCheck() {
            var ID = $('#AID').val();
            var DenyReason = $('#DenyReason').val();
            if (DenyReason == undefined || DenyReason == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请填写审批意见！', 'warning'); return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定拒审？', function (r) {
                if (r) {
                    $.ajax({
                        url: "sysService.aspx?method=setApplicationCheck&ID=" + ID + "&ty=1&reason=" + escape(DenyReason),
                        type: 'post', dataType: 'json', data: {},
                        success: function (text) {
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '拒审成功!', 'info');
                                $('#dlgDeny').dialog('close'); 	// close the dialog
                                reload();
                            }
                            else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning'); }
                        }
                    });
                }
            });
        }
        //审批或拒审
        function setCheck() {
            var ID = $('#AID').val();
            var DenyReason = $('#DenyReason').val();
            if (DenyReason == undefined || DenyReason == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请填写审批意见！', 'warning'); return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定通过审批？', function (r) {
                if (r) {
                    $.ajax({
                        url: "sysService.aspx?method=setApplicationCheck&ID=" + ID + "&ty=0&reason=" + escape(DenyReason),
                        type: 'post', dataType: 'json', data: {},
                        success: function (text) {
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '通过审批成功!', 'info');
                                $('#dlgDeny').dialog('close'); 	// close the dialog
                                reload();
                            }
                            else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning'); }
                        }
                    });
                }
            });
        }
        function reload() {
            $('#dg').datagrid('reload');
            $('#dg').datagrid('clearSelections');
        }
        //审批
        function plcheck() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要审批的数据！', 'warning'); return; }
            if (row) {
                $('#dlgDeny').dialog('open').dialog('setTitle', '申请审批');
                //$('#fm').form('load', row);
                $('#AID').val(row.ID);
                $('#AMemo').html(row.Memo);
                if (row.ApplyType == "4") {
                    //加班申请
                    $('#File').hide();
                    $('#appNR').html("<td style=\"text-align: right;\">申请人员:</td><td>" + row.ApplyName + "</td><td style=\"text-align: right;\">申请日期:</td><td>" + DateTimeFormatter(row.ApplyDate) + "</td><td style=\"text-align: right;\">加班时间:</td><td>" + DateTimeFormatter(row.OStartTime) + "---" + DateTimeFormatter(row.OEndTime) + "</td><td style=\"text-align: right;\">加班时长:</td><td>" + row.OTime + "小时</td>");
                } else if (row.ApplyType == "2") {
                    //轮胎外调
                    $('#File').show();
                    $('#appNR').html("<td style=\"text-align: right;\">申请人员:</td><td>" + row.ApplyName + "</td><td style=\"text-align: right;\">申请日期:</td><td>" + DateTimeFormatter(row.ApplyDate) + "</td><td style=\"text-align: right;\">外调仓库:</td><td>" + row.ThrowHouseName + "</td><td style=\"text-align: right;\">客户姓名:</td><td>" + row.ClientName + "</td>");

                    ShowDG();
                    //$('#dgCheckAwb').datagrid('load', row.RelateList);
                    var gridOpts = $('#dgCheckAwb').datagrid('options');
                    gridOpts.url = 'sysService.aspx?method=QueryApproveRelateList&ApproveID=' + row.ID + '&ApplyType=' + row.ApplyType;
                    var fl = "";
                    if (row.FileList.length > 0) {
                        for (var i = 0; i < row.FileList.length; i++) {
                            fl += "<a href=../" + row.FileList[i].FileName + ">" + row.FileList[i].FileName + "</a>&nbsp;&nbsp;&nbsp;&nbsp;";
                        }
                    }
                    $('#dvFile').html(fl);
                }
                //$('#AApplyName').html(row.ApplyName);
                //$('#AApplyDate').html(DateTimeFormatter(row.ApplyDate));
                //$('#AThrowHouseName').html(row.ThrowHouseName);
                //$('#AClientName').html(row.ClientName);
                $.ajax({
                    url: "sysService.aspx?method=QueryApproveRout&id=" + row.ID + "&ApproveType=" + row.ApplyType,
                    cache: false,
                    success: function (text) {
                        var ldl = document.getElementById("dvRoute");
                        ldl.innerHTML = text;
                    }
                });
            }
        }
        //审批流程图
        function Proc() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要查看的数据！', 'warning'); return; }
            if (row) {
                $('#dlgProc').dialog('open').dialog('setTitle', '查看申请单：' + row.ID + "的审批流程图");
                $.ajax({
                    url: "../Finance/financeApi.aspx?method=QueryExpenseRout&id=" + row.ID + "&ApproveType=" + row.ApplyType + "&ApproveID=" + row.AppSetID + "&applyID=" + row.ApplyID + "&ApplyName=" + row.ApplyName + "&HouseID=" + row.HouseID,
                    cache: false,
                    success: function (text) {
                        var ldl = document.getElementById("lblTrack");
                        ldl.innerHTML = text;
                    }
                });
            }
        }
    </script>

</asp:Content>
