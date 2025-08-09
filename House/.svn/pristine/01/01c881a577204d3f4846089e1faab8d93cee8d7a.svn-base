<%@ Page Title="采购单报价" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OesOrderManager.aspx.cs" Inherits="Cargo.Order.OesOrderManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/Rotate/jQueryRotate.2.2.js" type="text/javascript"></script>
    <script src="../JS/easy/js/datagrid-groupview.js" type="text/javascript"></script>
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
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true);
            $('#dg').datagrid({ height: height });
        }
        $(document).ready(function () {
            $('#dg').datagrid({
                width: '100%',
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OrderID',
                url: null,
                toolbar: '#dgtoolbar',
                columns: [[
                    { title: '', field: 'OrderID', checkbox: true, width: '30px' },
                    {
                        title: '采购订单号', field: 'OrderNo', width: '110px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '城市', field: 'Dest', width: '110px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '数量', field: 'Piece', width: '110px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '开单员ID', field: 'CreateAwbID', width: '60px', hidden: 'true', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '采购单状态', field: 'IsMakeSure', width: '80px',
                        formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='未报价'>未报价</span>"; }
                            else if (val == "1") { return "<span title='已确认'>已确认</span>"; }
                            else if (val == "2") { return "<span title='已报价'>已报价</span>"; }
                            else if (val == "3") { return "<span title='已取消'>已取消</span>"; }
                            else if (val == "4") { return "<span title='已确价'>已确价</span>"; }
                            else { return ""; }
                        }
                    }
                ]],
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                rowStyler: function (index, row) {
                    if (row.AwbStatus == "5") { return "color:#2a83de"; };
                    if (row.IsMakeSure == "0") { return "background-color:#f8ecca"; };
                    //if (row.IsMakeSure == "1") { return "background-color:#b3ce7e"; };
                    if (row.IsMakeSure == "2") { return "background-color:#CED27D"; };
                    if (row.IsMakeSure == "3") { return "background-color:#CEDCE0"; };
                    if (row.IsMakeSure == "4") { return "background-color:#b3ce7e"; };
                },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });

            var datenow = new Date();
            $('#StartDate').datebox('setValue', getNowFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#Dest').combobox('textbox').bind('focus', function () { $('#Dest').combobox('showPanel'); });
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                }
            });
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });

        });

        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=QueryOesPreOrderInfo';
            $('#dg').datagrid('load', {
                OrderNo: $('#AOrderNo').val(),
                AcceptPeople: '',
                Piece: '',
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                FinanceSecondCheck: '-1',
                CheckOutType: '',
                Dep: '',
                Dest: $("#Dest").combobox('getText'),
                HouseID: $("#AHouseID").combobox('getValue'),//仓库ID
                SaleManID: '',
                CreateAwb: '',
                Purchaser: '',
                IsMakeSure: $("#AIsMakeSure").combobox('getValue'),
                AcceptUnit: '',
                OrderModel: "0",//订单类型
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
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'">
        <table>
            <tr>
                <td style="text-align: right;">订单号:
                </td>
                <td>
                    <input id="AOrderNo" class="easyui-textbox" data-options="prompt:'请输入订单号'" style="width: 100px">
                </td>
                <td style="text-align: right;">区域大仓:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;" />
                </td>
                <td style="text-align: right;">开单时间:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px" />~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">城市:
                </td>
                <td>
                    <input id="Dest" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../systempage/sysService.aspx?method=QueryAllCity',multiple:true" />
                </td>
                <td style="text-align: right;">确认状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="AIsMakeSure" name="IsMakeSure" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="0">未报价</option>
                        <option value="1">已确认</option>
                        <option value="2">已报价</option>
                        <option value="4">已确价</option>
                        <option value="-1">全部</option>
                    </select>
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="dgtoolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <%--<a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()" id="DelItem">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;--%>
        <%--<a href="#" class="easyui-linkbutton" iconcls="icon-ok" plain="false" onclick="OpenConfirm()">&nbsp;确认订单&nbsp;</a>&nbsp;&nbsp;--%>
    </div>
    <div id="dlgOrder" class="easyui-dialog" style="width: 1080px; height: 540px;" closed="true" closable="false" buttons="#dlgOrder-buttons">
        <form id="fmDep" method="post">
            <input type="hidden" name="OrderID" id="OrderID" />
            <input type="hidden" name="OrderNo" id="OrderNo" />
            <input type="hidden" name="HouseID" id="HouseID" />
            <input type="hidden" name="TransitFee" id="TransitFee" />
            <input type="hidden" id="IsMakeSure" />
            <input type="hidden" id="Did" />
        </form>
        <table>
            <tr>
                <td>
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
                    <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="OpenConfirm()" id="confirm">&nbsp;确认订单</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="SaveOrderUpdate()" id="save">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closeDlg()">&nbsp;关&nbsp;闭&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    <!--Begin 确认订单-->
    <div id="dlgConfirm" class="easyui-dialog" style="width: 350px; height: 200px; padding: 5px 5px" closed="true" buttons="#dlgConfirm-buttons">
        <form class="easyui-form" method="post">
            <table>
                <tr>
                    <td style="text-align: right;">外采商家:
                    </td>
                    <td>
                        <input id="CPurchaser" name="Purchaser" class="easyui-combobox" style="width: 150px;" data-options="required:true" panelheight="auto" />
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlgConfirm-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="Confirm()">&nbsp;确&nbsp;定&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgConfirm').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <!--End 确认订单-->

    <object id="LODOP_OB" title="dd" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA"
        width="0px" height="0px">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0px" height="0px"></embed>
    </object>
    <script type="text/javascript">
        function OpenConfirm() {
            var row = $('#dgSave').datagrid('getRows');
            if (row.length <= 0) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单列表中没有数据', 'warning'); return; }
            for (var i = 0; i < row.length; i++) {
                if (row[i].Batch == "" || row[i].UnitPrice == "" || row[i].PurchaserID == "" || row[i].Batch == undefined || row[i].UnitPrice == undefined || row[i].PurchaserID == undefined) {
                    if (row.length <= 0) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请填写所有必填项', 'warning');
                        return;
                    }
                }
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定保存？', function (r) {
                if (r) {
                    if ($('#dgSave').datagrid('validateRow', OldeditIndex)) {
                        var editors = $('#dgSave').datagrid('getEditors', OldeditIndex);
                        if (editors.length > 0) {
                            var zsn = editors[2];
                            var ZName = $(zsn.target).combobox('getText');

                            $('#dgSave').datagrid('getRows')[OldeditIndex]['PurchaserName'] = ZName;
                            $('#dgSave').datagrid('endEdit', OldeditIndex);
                        }
                    }
                    var json = JSON.stringify(row);
                    $.messager.progress({ msg: '请稍后,正在保存中...' });

                    $('#fmDep').form('submit', {
                        url: 'orderApi.aspx?method=OesPreOrderConfirm',
                        onSubmit: function (param) {
                            param.submitData = json;
                        },
                        success: function (msgg) {
                            IsModifyOrder = false;
                            $.messager.progress("close");
                            var result = eval('(' + msgg + ')');
                            if (result.Result) {
                                alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确认成功！', 'info');
                                $('#dg').datagrid('reload');
                                $('#dlgConfirm').dialog('close');
                            } else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    })
                }
            });
        }
        //保存订单
        function SaveOrderUpdate() {
            var dgRow = $("#dg").datagrid('getData').rows[$("#Did").val()];
            var row = $('#dgSave').datagrid('getRows');
            if (row.length <= 0) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单列表中没有数据', 'warning'); return; }
            if ($('#dgSave').datagrid('validateRow', OldeditIndex)) {
                var editors = $('#dgSave').datagrid('getEditors', OldeditIndex);
                if (editors.length > 0) {
                    var zsn = editors[2];
                    var ZName = $(zsn.target).combobox('getText');

                    $('#dgSave').datagrid('getRows')[OldeditIndex]['PurchaserName'] = ZName;
                    $('#dgSave').datagrid('endEdit', OldeditIndex);
                }
            }
            for (var i = 0; i < row.length; i++) {
                if (row[i].Batch == "" || row[i].UnitPrice == "" || row[i].PurchaserID == "" || row[i].Batch == undefined || row[i].UnitPrice == undefined || row[i].PurchaserID == undefined || row[i].UnitPrice == "0.00") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请填写所有必填项', 'warning');
                    return;
                }
                if (dgRow.IsMakeSure == 4 && row[i].UnitPrice > row[i].OldUnitPrice) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '最新采购价不能大于' + row[i].OldUnitPrice + '！', 'warning');
                    return;
                }
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定保存？', function (r) {
                if (r) {
                    var json = JSON.stringify(row);
                    $.messager.progress({ msg: '请稍后,正在保存中...' });

                    $('#fmDep').form('submit', {
                        url: 'orderApi.aspx?method=updateOesPreOrderData&IsMakeSure=' + dgRow.IsMakeSure,
                        onSubmit: function (param) {
                            param.submitData = json;
                            //var trd = $(this).form('enableValidation').form('validate');
                            //return trd;
                        },
                        success: function (msgg) {
                            IsModifyOrder = false;
                            $.messager.progress("close");
                            var result = eval('(' + msgg + ')');
                            if (result.Result) {
                                var dd = result.Message.split('/');
                                $('#ONum').val(dd[0]);
                                $('#OutNum').val(dd[1]);
                                alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功！', 'info');
                                $('#dg').datagrid('reload');
                            } else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                            }
                        }
                    })
                }
            });
        }
        //删除订单信息
        function DelItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            for (var i = 0; i < rows.length; i++) {
                if (rows[i].IsMakeSure != 0) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', rows[i].OrderNo + '已确认采购订单无法删除！', 'warning'); return;
                }
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'orderApi.aspx?method=DelPreOrder',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '删除成功！', 'info');
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
        //双击显示订单详细界面 
        function editItemByID(Did) {
            $('#dgSave').datagrid('loadData', { total: 0, rows: [] });
            IsModifyOrder = false;
            editIndex = undefined;
            $("#Did").val(Did);
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#fmDep').form('load', row);
                $('#dlgOrder').dialog('open').dialog('setTitle', '修改订单：' + row.OrderNo);
                $("#IsMakeSure").val(row.IsMakeSure);
                if (row.IsMakeSure == 1 || row.IsMakeSure == 3) {
                    $('#save').hide();
                } else {
                    $('#save').show();
                }
                if (row.IsMakeSure == 4) {
                    $('#confirm').show();
                } else {
                    $('#confirm').hide();
                }
                $('#dgSave').datagrid({
                    width: '1050px',
                    height: '450px',
                    title: '出库产品', //标题内容
                    loadMsg: '数据加载中请稍候...',
                    autoRowHeight: false, //行高是否自动
                    collapsible: true, //是否可折叠
                    pagination: false, //分页是否显示
                    fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                    singleSelect: true, //设置为 true，则只允许选中一行。
                    checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                    idField: 'ID',
                    url: null,
                    columns: [[
                        { title: '', field: 'ID', checkbox: true, width: '30px' },
                        {
                            title: '数量', field: 'Piece', width: '50px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '规格', field: 'Specs', width: '100px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '花纹', field: 'Figure', width: '100px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '型号', field: 'Model', width: '100px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '货品代码', field: 'GoodsCode', width: '100px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '产品类型', field: 'TypeName', width: '80px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '产地', field: 'Born', width: '50px', formatter: function (value) {
                                if (value == "0") {
                                    return "<span title='国产'>国产</span>";
                                } else if (value == "1") {
                                    return "<span title='进口'>进口</span>";
                                }
                            }
                        },
                        {
                            title: '速度级别', field: 'SpeedLevel', width: '60px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '载重指数', field: 'LoadIndex', width: '60px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        },
                        {
                            title: '旧采购价', field: 'OldUnitPrice', width: '60px', hidden: true
                        },
                        {
                            title: '批次', field: 'Batch', width: '50px', editor: { type: 'numberbox', options: { required: true } }
                        },
                        {
                            title: '采购价', field: 'UnitPrice', width: '60px', editor: { type: 'numberbox', options: { precision: 2, required: true} }
                        },           
                        {
                            title: '选择采购商', field: 'PurchaserID', width: '110px', formatter: function (value, row) {
                                return row.PurchaserName;
                            },
                            editor: {
                                type: 'combobox',
                                options: {
                                    panelHeight: 'auto', valueField: 'PurchaserID', textField: 'PurchaserName', url: '../Client/clientApi.aspx?method=AutoCompletePurchaser', required: true, editable: false
                                }
                            }
                        },
                        {
                            title: '采购商', field: 'PurchaserName', width: '90px', formatter: function (value) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    ]],
                    onClickRow: function (index, data) {
                        if ($("#IsMakeSure").val() == "1" || $("#IsMakeSure").val() == "3") { return; }
                        if (OldeditIndex != index) {
                            NeweditIndex = index;
                            if (endEditing()) {
                                $('#dgSave').datagrid('selectRow', index).datagrid('beginEdit', index);
                                var row = $("#dgSave").datagrid('getData').rows[index];
                            } else {
                                if ($('#dgSave').datagrid('validateRow', OldeditIndex)) {
                                    var editors = $('#dgSave').datagrid('getEditors', OldeditIndex);
                                    if (editors.length > 0) {
                                        var zsn = editors[2];
                                        var ZName = $(zsn.target).combobox('getText');

                                        $('#dgSave').datagrid('getRows')[OldeditIndex]['PurchaserName'] = ZName;
                                        $('#dgSave').datagrid('endEdit', OldeditIndex);
                                    }
                                }
                                $('#dgSave').datagrid('cancelEdit', OldeditIndex);
                            }
                            OldeditIndex = index;
                        }
                    },
                    onClickCell: function (Index, field, value) {
                        if ($("#IsMakeSure").val() == "1" || $("#IsMakeSure").val() == "3") { return; }
                        $('#dgSave').datagrid('selectRow', Index);
                        $('#dgSave').datagrid('beginEdit', Index);
                    }
                });

                var gridOpts = $('#dgSave').datagrid('options');
                gridOpts.url = 'orderApi.aspx?method=QueryOesPreOrderByOrderNo&OrderNo=' + row.OrderNo;
                //所在仓库
                $('#AHID').combobox({
                    url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                    valueField: 'HouseID', textField: 'Name',
                    onSelect: function (rec) {
                        $('#HID').combobox('clear');
                        var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                        $('#HID').combobox('reload', url);
                    }
                });
                $('#AHID').combobox('setValue', '<%=UserInfor.HouseID%>');
                $('#HID').combobox('clear');
                var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=<%=UserInfor.HouseID%>';
                $('#HID').combobox('reload', url);
            }
        }
        var IsModifyOrder = false;
        NeweditIndex = undefined;
        OldeditIndex = undefined;
        function endEditing() {
            if (OldeditIndex != undefined) {
                if ($('#dgSave').datagrid('validateRow', OldeditIndex)) {
                    var editors = $('#dgSave').datagrid('getEditors', OldeditIndex);
                    if (editors.length > 0) {
                        var zsn = editors[2];
                        var ZName = $(zsn.target).combobox('getText');

                        $('#dgSave').datagrid('getRows')[OldeditIndex]['PurchaserName'] = ZName;
                    }
                    var dgRow = $("#dg").datagrid('getData').rows[$("#Did").val()];
                    if (dgRow.IsMakeSure == 4) {
                        var row = $("#dgSave").datagrid('getData').rows[OldeditIndex];
                        if (row.OldUnitPrice != "" && row.OldUnitPrice != undefined) {
                            if ($(editors[1].target).combobox('getText') > row.OldUnitPrice) {
                                $('#dgSave').datagrid('cancelEdit', OldeditIndex);
                                alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '最新采购价不能大于' + row.OldUnitPrice +'！', 'info');
                            }
                        }
                    }
                    $('#dgSave').datagrid('endEdit', OldeditIndex);
                } else {
                    $('#dgSave').datagrid('cancelEdit', OldeditIndex);
                }
                return true;
            } else {
                return false;
            }
        }

        //关闭弹出框
        function closeDlg() {
            if (IsModifyOrder) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请先保存订单再关闭！', 'warning');
                return;
            }
            $('#dlgOrder').dialog('close');
            $('#dg').datagrid('reload');
        }
        //弹出定时关闭的消息框
        function alert_autoClose(title, msg, icon) {
            var interval;
            var time = 3000;
            var x = 1;  //只接受整数
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
