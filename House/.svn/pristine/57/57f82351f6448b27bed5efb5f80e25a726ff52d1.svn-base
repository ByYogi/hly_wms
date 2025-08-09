<%@ Page Title="快速简易开单" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="QuickAddOrder.aspx.cs" Inherits="Cargo.Order.QuickAddOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="../JS/Date/CheckActivX.js" type="text/javascript"></script>--%>
    <script src="../JS/Lodop/LodopFuncs.js" type="text/javascript"></script>
    <style type="text/css">
        #box {
            height: 25px;
            width: 100px;
            position: relative;
            cursor: pointer;
            overflow: hidden;
            padding: 2px;
            background-color: #fdfce2;
            border-radius: 5px;
        }

            #box.border {
                border: 1px solid red;
            }

            #box .gou {
                position: absolute;
                width: 24px;
                height: 24px;
                right: 0px;
            }

                #box .gou.on::after {
                    border-color: red;
                }

                #box .gou::after {
                    position: absolute;
                    top: 4px;
                    left: 8px;
                    width: 6px;
                    height: 10px;
                    border-style: solid;
                    border-color: #ccc;
                    border-width: 0 2px 2px 0;
                    transform: rotateZ(45deg);
                    content: "";
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

            var HID = "<%=UserInfor.HouseID%>";

            //如果是广州、广东、揭阳的用户开放区域大仓选项
            if (HID == "9" || HID == "44" || HID == "45") {
                $('#AHouseID').combobox('enable');
            } else {
                $('#AHouseID').combobox('disable');
            }
            $.ajaxSetup({ async: true });
            $.getJSON("../Product/productApi.aspx?method=QueryAllProductSource", function (data) {
                ProductSource = data;
            })
            //定义全局ThrowGood值用于判断显示价格
            ThrowGoodValue = -1;
        }
        $(window).resize(function () {
            adjustment();
        });
        function adjustment() {
            //var height = parseInt((Number($(window).height()) - 100) / 2);
            var height = parseInt((Number($(window).height()) - Number($("div[name='SelectDiv1']").outerHeight(true))) / 2) + 200;
            $('#dg').datagrid({ height: height });
            $('#dgSave').datagrid({ height: height });
        }
        $(document).ready(function () {
            var columns = [];
            columns.push({ title: '', field: 'ID', checkbox: true, width: '30px' });
            columns.push({
                title: '品番', field: 'GoodsCode', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '背番', field: 'Specs', width: '70px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '在库数', field: 'Piece', width: '60px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品来源', field: 'Source', width: '90px', formatter: function (value) {
                    return "<span title='" + GetSourceName(value) + "'>" + GetSourceName(value) + "</span>";
                }
            });
            columns.push({
                title: '货位代码', field: 'ContainerCode', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在区域', field: 'AreaName', width: '90px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在仓库', field: 'FirstAreaName', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品名称', field: 'ProductName', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });

            columns.push({ title: '入库时间', field: 'InHouseTime', width: '130px', formatter: DateTimeFormatter });

            $('#dg').datagrid({
                width: '650px',
                //height: '500px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: true, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [columns],
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { plOutCargo(); }
            });

            columns = [];
            columns.push({
                title: '', field: 'ID', checkbox: true, width: '30px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '品番', field: 'GoodsCode', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '背番', field: 'Specs', width: '70px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '出库数', field: 'Piece', width: '60px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品来源', field: 'Source', width: '90px', formatter: function (value) {
                    return "<span title='" + GetSourceName(value) + "'>" + GetSourceName(value) + "</span>";
                }
            });
            columns.push({
                title: '货位代码', field: 'ContainerCode', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在区域', field: 'AreaName', width: '90px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在仓库', field: 'FirstAreaName', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品名称', field: 'ProductName', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });

            //订单列表
            $('#dgSave').datagrid({
                width: '620px',
                //height: '405px',
                title: '出库列表', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '',
                columns: [columns],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { dblClickDelCargo(index); }
            });
            //一级产品
            $('#APID').combobox({
                url: '../Product/productApi.aspx?method=QueryALLOneProductType', valueField: 'TypeID', textField: 'TypeName',
                onSelect: function (rec) {
                    $('#ASID').combobox('clear');
                    var url = '../Product/productApi.aspx?method=QueryALLOneProductType&PID=' + rec.TypeID;
                    $('#ASID').combobox('reload', url);
                }
            });
            $('#APID').combobox('setValue', '354');
            $('#DisplayNum').val(0);
            $('#DisplayPiece').val(0);
            //客户姓名
            $('#SAcceptPeople').combobox({
                //$('#AcceptPeople').combobox({
                valueField: 'ClientNum', textField: 'Boss', delay: '10',
                url: '../Client/clientApi.aspx?method=AutoCompleteClient',
                onSelect: onClientChanged,
                required: true
            });
            //$('#SAcceptPeople').combobox('setValue', '783644');

            function onAcceptAddressChanged(item) {
                $('#AAcceptUnit').val(item.AcceptCompany);
                $('#AAcceptAddress').val(item.AcceptAddress);
                $('#AAcceptTelephone').val(item.AcceptTelephone);
                $('#AAcceptCellphone').val(item.AcceptCellphone);
                $('#AAcceptPeople').val(item.AcceptPeople);
            }
            //收货人自动选择方法
            function onClientChanged(item) {
                $('#ClientNum').val(item.ClientNum);
                $('#HiddenClientNum').val(item.ClientNum);
                $("#HiddenClientSelectName").val(item.Boss);
                $("#ShopCode").val(item.ShopCode);
                
                $('#ClientType').val(item.ClientType);
                if (item) {
                    $('#AcceptPeople').combobox({
                        valueField: 'ADID', textField: 'AcceptPeople', delay: '10',
                        url: '../Client/clientApi.aspx?method=AutoCompleteClientAcceptPeople&ClientNum=' + item.ClientNum,
                        onSelect: onAcceptAddressChanged
                    });
                }
                $('#AcceptPeople').combobox('textbox').bind('focus', function () { $('#AcceptPeople').combobox('showPanel'); });
            }
            $('#AGoodsCode').textbox('textbox').keydown(function (e) {
                if (e.keyCode == 13) {
                    dosearch();
                }
            });
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#HAID').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    $('#HAID').combobox('reload', url);

                }
            });
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            $('#HAID').combobox('clear');
            var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=<%=UserInfor.HouseID%>';
            $('#HAID').combobox('reload', url);
            $('#HAID').combobox('setValue', '3249');

            $('#APID').combobox('textbox').bind('focus', function () { $('#APID').combobox('showPanel'); });
            $('#ASID').combobox('textbox').bind('focus', function () { $('#ASID').combobox('showPanel'); });

            $('#SAcceptPeople').combobox('textbox').bind('focus', function () { $('#SAcceptPeople').combobox('showPanel'); });
        });

        //查询
        function dosearch() {
            if ($("#AHouseID").combobox('getValue') == undefined || $("#AHouseID").combobox('getValue') == '') {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择区域大仓！', 'warning');
                return;
            }
            if ($("#HAID").combobox('options').required) {
                if ($("#HAID").combobox('getValue') == undefined || $("#HAID").combobox('getValue') == '') {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择所在仓库！', 'warning');
                    return;
                }
            }

            if ($("#SAcceptPeople").combobox('getValue') == undefined || $("#SAcceptPeople").combobox('getValue') == '') {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择客户信息！', 'warning');
                return;
            }
            var type = 0;
            var columns = [];
            columns.push({ title: '', field: 'ID', checkbox: true, width: '30px' });
            columns.push({
                title: '品番', field: 'GoodsCode', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '背番', field: 'Specs', width: '70px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '在库数', field: 'Piece', width: '60px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品来源', field: 'Source', width: '90px', formatter: function (value) {
                    return "<span title='" + GetSourceName(value) + "'>" + GetSourceName(value) + "</span>";
                }
            });
            columns.push({
                title: '货位代码', field: 'ContainerCode', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在区域', field: 'AreaName', width: '90px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在仓库', field: 'FirstAreaName', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品名称', field: 'ProductName', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });

            columns.push({ title: '入库时间', field: 'InHouseTime', width: '130px', formatter: DateTimeFormatter });
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../House/houseApi.aspx?method=QueryALLHouseData';
            $('#dg').datagrid('load', {
                Specs: $('#ASpecs').val(),
                //Model: $('#AModel').val(),
                GoodsCode: $('#AGoodsCode').val(),
                PID: $("#APID").combobox('getValue'),//一级产品
                SID: $("#ASID").combobox('getValue'),//二级产品
                //TreadWidth: $('#ATreadWidth').val(),
                //ProductName: $('#AProductName').val(),
                //FlatRatio: $('#AFlatRatio').val(),
                //Figure: $('#AFigure').val(),
                BatchYear: "-1",//$('#ABatchYear').combobox('getValue'),
                //HubDiameter: $('#AHubDiameter').val(),
                //LoadIndex: $('#ALoadIndex').val(),
                // Company: $("#ACompany").combobox('getValue'),
                HAID: $("#HAID").combobox('getValue'),
                //BelongDepart: $("#ABelongDepart").combobox('getValue'),
                // SpeedLevel: $("#ASpeedLevel").combobox('getValue'),
                ClientNum: $('#HiddenClientNum').val(),
                ADID: $("#AcceptPeople").combobox('getValue'),
                HouseID: $("#AHouseID").combobox('getValue')//仓库ID
            });
            if (type == 1) {
                $('#dg').datagrid({
                    columns: [columns]
                })
            }
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
                <td id="AGoodsCodeTd" style="text-align: right;">品番:
                </td>
                <td>
                    <input id="AGoodsCode" class="easyui-textbox" data-options="prompt:'请输入品番'" style="width: 120px">
                </td>
                <td style="text-align: right;">区域大仓:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;" data-options="required:true" panelheight="auto" />
                </td>
                <td style="text-align: right;">一级产品:
                </td>
                <td>
                    <input id="APID" class="easyui-combobox" style="width: 95px;"
                        panelheight="auto" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">背番:
                </td>
                <td>
                    <input id="ASpecs" class="easyui-textbox" data-options="prompt:'请输入背番'" style="width: 120px">
                </td>

                <td style="text-align: right;">所在仓库:
                </td>
                <td>
                    <input id="HAID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'AreaID',textField:'Name',required:true" panelheight="auto" />
                </td>
                <td style="text-align: right;">二级产品:
                </td>
                <td>
                    <input id="ASID" class="easyui-combobox" style="width: 95px;" data-options="valueField:'TypeID',textField:'TypeName'" />
                </td>
            </tr>
        </table>
    </div>
    <input type="hidden" id="DisplayNum" />
    <input type="hidden" id="DisplayPiece" />

    <form id="fmDep" class="easyui-form" method="post">
        <input type="hidden" name="HouseCode" id="HouseCode" />
        <input type="hidden" name="BelongHouse" id="BelongHouse" />
        <input type="hidden" name="HouseID" id="HouseID" />
        <input type="hidden" name="ONum" id="ONum" />
        <input type="hidden" name="OutNum" id="OutNum" />
        <input type="hidden" name="ShopCode" id="ShopCode" />
        <input type="hidden" name="ClientNum" id="ClientNum" />
        <input type="hidden" id="ClientType" name="ClientType" />
        <input type="hidden" id="AAcceptPeople" name="AAcceptPeople" />
        <input type="hidden" id="AAcceptUnit" name="AcceptUnit" />
        <input type="hidden" id="AAcceptAddress" name="AcceptAddress" />
        <input type="hidden" id="AAcceptTelephone" name="AcceptTelephone" />
        <input type="hidden" id="AAcceptCellphone" name="AcceptCellphone" />
        <input type="hidden" id="HiddenClientNum" name="HiddenClientNum" />
        <input type="hidden" id="HiddenClientSelectName" name="HiddenClientSelectName" />
        <table>
            <tr>
                <td>
                    <table id="dg" class="easyui-datagrid">
                    </table>
                </td>
                <td>
                    <table id="dgSave" class="easyui-datagrid">
                    </table>
                </td>
            </tr>
        </table>
        <div id="saPanel">
            <table>
                <tr>
                    <td>客户名称:</td>
                    <td>
                        <input id="SAcceptPeople" style="width: 120px;" class="easyui-combobox AcceptPeople" data-options="valueField:'ClientNum',textField:'Boss',editable:true,required:true" panelheight="auto" /></td>
                    <td>收货客户:</td>
                    <td>
                        <input id="AcceptPeople"  style="width: 120px;" data-options="required:true" class="easyui-combobox AcceptPeople" panelheight="auto" /></td>
                    <td>关联订单:</td>
                    <td>
                        <input id="HAwbNo" name="HAwbNo" class="easyui-textbox" data-options="prompt:'请输入广丰订单号'" style="width: 120px"></td>
                    <td style="text-align: right;">
                        <a href="#" id="btnSave" class="easyui-linkbutton" iconcls="icon-ok"
                            plain="false" onclick="saveOutCargo()">&nbsp;保&nbsp;存&nbsp;订&nbsp;单</a>
                        &nbsp;&nbsp;
                                <a href="#" class="easyui-linkbutton" id="undo" iconcls="icon-clear" onclick="reset()">&nbsp;重&nbsp;置&nbsp;</a>
                    </td>
                </tr>
            </table>
        </div>
    </form>

    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="plOutCargo()">
            &nbsp;上订单&nbsp;</a>&nbsp;&nbsp;
                <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;下订单&nbsp;</a>
    </div>
    <!--Begin 出库操作-->

    <div id="dlg" class="easyui-dialog" style="width: 420px; height: 200px; padding: 5px"
        closed="true" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" id="InPiece" />
            <input type="hidden" id="InIndex" />
            <div id="saPanel">
                <table>
                    <tr>
                        <td style="text-align: right;">拉上订单数量：
                        </td>
                        <td>
                            <input name="Numbers" id="Numbers" class="easyui-numberbox" data-options="min:0,precision:0"
                                style="width: 200px;">
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="outOK()">&nbsp;确&nbsp;定&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <!--End 出库操作-->

    <object id="LODOP_OB" title="dd" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA"
        width="0px" height="0px">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0px" height="0px"></embed>
    </object>
    <script type="text/javascript">
        //根据来源ID获取来源名称
        function GetSourceName(value) {
            var name = '';
            for (var i = 0; i < ProductSource.length; i++) {
                if (ProductSource[i].Source == value) {
                    name = ProductSource[i].SourceName;
                    break;
                }
            }
            return name;
        }
        function reset() {
            // prePrint();
            $('#fmDep').form('clear');
            $('#dgSave').datagrid('loadData', { total: 0, rows: [] });

            $('#DisplayNum').val(0);
            $('#DisplayPiece').val(0);
            $('#dgSave').datagrid("getPanel").panel("setTitle", "");
        }

        //保存订单
        function saveOutCargo() {
            //取消业务员禁用方便后台取值
            var rows = $('#dgSave').datagrid('getRows');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要保存的数据！', 'warning'); return; }

            $('#HouseCode').val(rows[0].OrderCode);
            $('#BelongHouse').val(rows[0].BelongHouse);

            $('#HouseID').val(rows[0].HouseID);

            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定保存？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    $('#btnSave').linkbutton('disable');
                    var json = JSON.stringify(rows);

                    $('#fmDep').form('submit', {
                        url: 'orderApi.aspx?method=saveOrderData',
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
                                $('#ONum').val(dd[0]);
                                $('#OutNum').val(dd[1]);
                                if (dd[2] == "0") {
                                    $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功，是否打印拣货单？', function (r) {
                                        if (r) { prePrint(); }
                                        else { location.reload(); }
                                    });
                                } else {
                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单改价申请审批中，请等待！', 'info');
                                    location.reload();
                                }
                            } else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                                //$('#SaleManID').combobox('disable');
                            }
                        }
                    })
                }
            });
        }
        //删除出库的数据
        function DelItem() {
            var rows = $('#dgSave').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要拉下订单的数据！', 'warning');
                return;
            }
            var copyRows = [];

            for (var i = 0, l = rows.length; i < l; i++) {
                var row = rows[i];
                copyRows.push(row);
                var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
                var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
                n--;
                var pt = p - (Number(row.InPiece) - Number(row.Piece));
                $('#DisplayNum').val(Number(n));
                $('#DisplayPiece').val(Number(pt));

                var title = "上订单     已拉上：" + n + "票，汇总数：" + pt + " 箱";

                $('#dgSave').datagrid("getPanel").panel("setTitle", title);

                var index = $('#dg').datagrid('getRowIndex', copyRows[i].ID);
                if (index >= 0) {

                    var Trow = $("#dg").datagrid('getData').rows[index];
                    Trow.Piece = Trow.InPiece;
                    $('#dg').datagrid('updateRow', { index: index, row: Trow });
                }
            }
            for (var i = 0; i < copyRows.length; i++) {
                var index = $('#dgSave').datagrid('getRowIndex', copyRows[i]);
                $('#dgSave').datagrid('deleteRow', index);
            }
        }
        var ISM = false;
        //新增出库数据
        function outOK() {
            var row = $('#dg').datagrid('getSelected');
            if ($('#Numbers').val() == null || $('#Numbers').val() == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请输入拉上订单数量！', 'warning');
                return;
            }
            if ($('#Numbers').val() < 1) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '拉上订单数量必须大于0！', 'warning');
                return;
            }

            var Total = Number(row.Piece);

            var Aindex = $('#InIndex').val();
            var index = $('#dgSave').datagrid('getRowIndex', row.ID);
            if (index < 0) {
                row.IsRuleBank = "1";
                row.Piece = $('#Numbers').numberbox('getValue');
                $('#dgSave').datagrid('appendRow', row);
                var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
                var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
                n++;
                var pt = p + Number($('#Numbers').numberbox('getValue'));
                $('#DisplayNum').val(Number(n));
                $('#DisplayPiece').val(Number(pt));

                var title = "上订单     已拉上：" + n + "票，汇总数：" + pt + " 箱";
                $('#dgSave').datagrid("getPanel").panel("setTitle", title);
                closedgShowData();

                if (Total > Number($('#Numbers').numberbox('getValue'))) {
                    row.Piece = Total - Number($('#Numbers').numberbox('getValue'));
                } else {
                    row.Piece = 0;
                }

                $('#dg').datagrid('updateRow', { index: Aindex, row: row });
            } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单列表已存在该产品，请先拉下再添加！', 'warning'); }
        }

        //关闭
        function closedgShowData() {
            $('#dlg').dialog('close');
        }

        //删除出库的数据
        function dblClickDelCargo(Did) {
            var row = $("#dgSave").datagrid('getData').rows[Did];

            var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
            var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
            n--;
            var pt = p - (Number(row.InPiece) - Number(row.Piece));
            $('#DisplayNum').val(Number(n));
            $('#DisplayPiece').val(Number(pt));


            var title = "上订单     已拉上：" + n + "票，总件数：" + pt + " 件";

            $('#dgSave').datagrid("getPanel").panel("setTitle", title);

            var index = $('#dg').datagrid('getRowIndex', row.ID);
            if (index >= 0) {
                var Trow = $("#dg").datagrid('getData').rows[index];
                Trow.Piece = Trow.InPiece;
                $('#dg').datagrid('updateRow', { index: index, row: Trow });
            }
            var index = $('#dgSave').datagrid('getRowIndex', row);

            $('#dgSave').datagrid('deleteRow', index);
        }
        ///出库
        function plOutCargo() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要拉上订单的数据！', 'warning');
                return;
            }
            if (row.Piece == 0) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '在库件数为0', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '拉上品番：' + row.GoodsCode + ' 不得超过：' + row.Piece + '件');
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
        var LODOP;
        //订单打印
        function prePrint() {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            var nowdate = new Date();
            LODOP.SET_PRINT_PAGESIZE(0, 2100, 2970, "A4");
            var griddata = $('#dgSave').datagrid('getRows');
            var hous = '<%= PickTitle%>';
            var HouseName = '<%= HouseName%>';

            LODOP.ADD_PRINT_TEXT(4, 253, 280, 33, hous);
            LODOP.SET_PRINT_STYLEA(0, "FontName", "新宋体");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 19);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_IMAGE(-3, 47, 198, 49, "");
            //LODOP.ADD_PRINT_IMAGE(-3, 47, 198, 49, "<img src=\"../CSS/image/dlt.jpg\"/>");
            LODOP.SET_PRINT_STYLEA(0, "Stretch", 1);

            LODOP.ADD_PRINT_TEXT(41, 47, 70, 20, "订单号：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(41, 114, 110, 20, $('#ONum').val());
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

            LODOP.ADD_PRINT_TEXT(41, 225, 90, 20, "厂家号：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(41, 289, 141, 20, griddata[0].SourceCode + "-" + griddata[0].SourceName);
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

            LODOP.ADD_PRINT_TEXT(41, 434, 105, 20, "广丰单号：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(41, 510, 153, 20, $('#HAwbNo').textbox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

            LODOP.ADD_PRINT_RECT(66, 3, 134, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 136, 146, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 281, 94, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 374, 70, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 443, 118, 25, 0, 1);
            LODOP.ADD_PRINT_RECT(66, 560, 107, 25, 0, 1);

            LODOP.ADD_PRINT_TEXT(70, 21, 78, 25, "产品名称");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(70, 182, 52, 25, "品番");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(70, 302, 59, 25, "背番");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(70, 381, 64, 25, "出库数量");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(70, 465, 88, 25, "货位代码");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(70, 573, 91, 25, "所在区域");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
          
            var js = 0, Alltotal = 0, AllPiece = 0;
            for (var i = 0; i < griddata.length; i++) {
                js++;
                var p = Number(griddata[i].InPiece) - Number(griddata[i].Piece);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 3, 134, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 136, 146, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 281, 94, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 374, 70, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 443, 118, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(90 + i * 25, 560, 107, 25, 0, 1);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 5, 131, 23, griddata[i].ProductName);//产品名称
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 156, 127, 20, griddata[i].GoodsCode);//品番
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 303, 61, 20, griddata[i].Specs);//背番 
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
               
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 393, 51, 20, p);//数量出库件数
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

                LODOP.ADD_PRINT_TEXT(95 + i * 25, 463, 106, 20, griddata[i].ContainerCode);//货位代码
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(95 + i * 25, 573, 88, 20, griddata[i].AreaName);//所在区域
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            }

            //LODOP.ADD_PRINT_TEXT(124 + (griddata.length - 1) * 25, 50, 102, 23, "备注：");
            //LODOP.ADD_PRINT_TEXT(124 + (griddata.length - 1) * 25, 50, 102, 23, "");
            //LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            //LODOP.ADD_PRINT_TEXT(124 + (griddata.length - 1) * 25, 150, 400, 23, "");
            //LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

            LODOP.ADD_PRINT_TEXT(147 + (griddata.length - 1) * 25, 50, 102, 23, "仓库：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(147 + (griddata.length - 1) * 25, 222, 100, 20, "制表：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(147 + (griddata.length - 1) * 25, 394, 200, 20, AllDateTime(nowdate));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);


           // LODOP.PRINT_DESIGN();
            LODOP.PREVIEW();
            location.reload();

        }
    </script>

</asp:Content>
