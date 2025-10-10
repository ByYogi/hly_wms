<%@ Page Title="开补货单" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="AddRplOrder.aspx.cs" Inherits="Cargo.Order.addSaleOrder" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        
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
                $.getJSON("../Product/productApi.aspx?method=QueryAllProductSource", function (data) {
                    ProductSource = data;
                })
            }
            $(window).resize(function () {
                adjustment();
            });
            function adjustment() {
                //var height = parseInt((Number($(window).height()) - 100) / 2);
                var height = parseInt((Number($(window).height()) - Number($("div[name='SelectDiv1']").outerHeight(true))) / 2);
                $('#dg').datagrid("resize",{ height: height - 60 });
                $('#outDg').datagrid("resize", { height: (Number($(window).height()) - 90) - height });
            }
            let houseOptionsOrg = null; // 保存大仓列表的请求结果
            $(document).ready(function () {
                var userID = "<%=Ln%>";
                var columns = [];
                $.get("../House/houseApi.aspx?method=CargoPermisionHouse", function(response) {
                    houseOptionsOrg = JSON.parse(response); // 将返回的数据保存到变量中
                    
                    $('#ReqHouseOpts').combobox({
                        data: houseOptionsOrg,
                        valueField: 'HouseID', textField: 'Name',
                    });

                    $('#updtOOSHouseOpts').combobox({
                        data: [{HouseID: -1, Name: '全部'}, ...houseOptionsOrg],
                        valueField: 'HouseID', textField: 'Name',
                    }).combobox('setValue', -1);
                    
                    //所在仓库
                    $('#AHouseID').combobox({
                        data: houseOptionsOrg,
                        valueField: 'HouseID', textField: 'Name',
                        onSelect: function (rec) {
                            $('#HAID').combobox('clear');
                            var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                            $('#HAID').combobox('reload', url);
                        }
                    }).combobox('setValue', '<%=UserInfor.HouseID%>');
                });
                columns.push({ title: '', field: 'OOSID', checkbox: true, width: '10%' });
                columns.push({
                    title: '产品代码', field: 'ProductCode', width: '8%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '产品名称', field: 'ProductName', width: '8%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '产品品牌', field: 'TypeName', width: '6%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '规格', field: 'Specs', width: '7%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '型号', field: 'Model', width: '7%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '花纹', field: 'Figure', width: '3%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '载速', field: 'LoadIndex', width: '3%', align: 'right', formatter: function (value, row) {
                        return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                    }
                });
                columns.push({
                    title: '批次', field: 'Batch', width: '5%', align: 'right', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '最小库存数', field: 'MinStock', width: '5%', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '最大库存数', field: 'MaxStock', width: '5%', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '在库数量', field: 'CurStock', width: '5%', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '在途数量', field: 'InTransitStock', width: '5%', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '补货中数量', field: 'RestockingPiece', width: '5%', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '缺货数量', field: 'Piece', width: '5%', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '所在仓库', field: 'HouseName', width: '8%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '上级仓库', field: 'ParentHouseName', width: '8%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '更新时间', field: 'UpdateDate', width: '8%', formatter: DateTimeFormatter,
                });
                $('#dg').datagrid({
                    width: '100%',
                    title: '', //标题内容
                    loadMsg: '数据加载中请稍候...',
                    autoRowHeight: false, //行高是否自动
                    collapsible: true, //是否可折叠
                    pagination: false, //分页是否显示
                    fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                    singleSelect: true, //设置为 true，则只允许选中一行。
                    checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                    idField: 'OOSID',
                    url: null,
                    toolbar: '#toolbar',
                    columns: [columns],
                    onLoadSuccess: function (data) { },
                    onClickRow: function (index, row) {
                        $('#dg').datagrid('clearSelections');
                        $('#dg').datagrid('selectRow', index);
                    },
                    onDblClickRow: function (index, row) { pullRowData(); }
                });
                columns = [];
                
                
                columns.push({ title: '', field: 'OOSID', checkbox: true, width: '10%' });
                columns.push({
                    title: '产品代码', field: 'ProductCode', width: '8%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '产品名称', field: 'ProductName', width: '8%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '产品品牌', field: 'TypeName', width: '6%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '缺货数量', field: 'Piece', width: '5%', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '所在仓库', field: 'HouseName', width: '8%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '上级仓库', field: 'ParentHouseName', width: '8%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                //出库列表
                $('#outDg').datagrid({
                    width: '100%',
                    title: '补货产品', //标题内容
                    loadMsg: '数据加载中请稍候...',
                    autoRowHeight: false, //行高是否自动
                    collapsible: true, //是否可折叠
                    pagination: false, //分页是否显示
                    fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                    singleSelect: false, //设置为 true，则只允许选中一行。
                    checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                    idField: 'OOSID',
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
                        $('#ASID').combobox({
                            url: '../Product/productApi.aspx?method=QueryALLOneProductType&PID=' + rec.TypeID, valueField: 'TypeID', textField: 'TypeName', AddField: 'PinyinName',
                            filter: function (q, row) {
                                var opts = $(this).combobox('options');
                                return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                            },
                        });
                    }
                });
                $('#APID').combobox('setValue', '1');
                $('#ASID').combobox('clear');
                $('#ASID').combobox({
                    url: '../Product/productApi.aspx?method=QueryALLOneProductType&PID=1', valueField: 'TypeID', textField: 'TypeName', AddField: 'PinyinName',
                    filter: function (q, row) {
                        var opts = $(this).combobox('options');
                        return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                    },
                });
                
                
                $('#HAID').combobox('clear');
                var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=<%=UserInfor.HouseID%>';
                //$('#HAID').combobox('reload', url);
                $('#HAID').combobox({
                    'url': url, onLoadSuccess: function () {
                        //默认选中第一个
                        var array = $(this).combobox("getData");
                        for (var item in array[0]) {
                            if (item == "AreaID") {
                                $(this).combobox('select', array[0][item]);
                            }
                        }
                    }
                });

                $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
                $('#HAID').combobox('textbox').bind('focus', function () { $('#HAID').combobox('showPanel'); });
                //客户姓名
                $('#SAcceptPeople').combobox({
                    //$('#AcceptPeople').combobox({
                    valueField: 'ClientNum', textField: 'Boss', AddField: 'PinyinName', delay: '10',
                    url: '../Client/clientApi.aspx?method=AutoCompleteClient',
                    onSelect: onClientChanged,
                    filter: function (q, row) {
                        var opts = $(this).combobox('options');
                        return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                    },
                    required: true
                });
                $('#ADep').textbox('setValue', '<%= UserInfor.DepCity%>');
                $('#ADest').combobox('textbox').bind('focus', function () { $('#ADest').combobox('showPanel'); });
                $('#APID').combobox('textbox').bind('focus', function () { $('#APID').combobox('showPanel'); });
                $('#ASID').combobox('textbox').bind('focus', function () { $('#ASID').combobox('showPanel'); });
                $('#CheckOutType').combobox('textbox').bind('focus', function () { $('#CheckOutType').combobox('showPanel'); });
                $('#SaleManID').combobox('textbox').bind('focus', function () { $('#SaleManID').combobox('showPanel'); });

                $('#SAcceptPeople').combobox('textbox').bind('focus', function () { $('#SAcceptPeople').combobox('showPanel'); });
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
            });

            //查询
            function dosearch() {
                $('#dg').datagrid('clearSelections');
                var gridOpts = $('#dg').datagrid('options');
                gridOpts.url = '../Order/orderApi.aspx?method=QueryOutOfStocks';
                $('#dg').datagrid('load', {
                    Specs: $('#ASpecs').val(),
                    Figure: $('#AFigure').val(),
                    TypeCate: $("#APID").combobox('getValue'),//一级产品
                    TypeID: $("#ASID").combobox('getValue'),//二级产品
                    AreaID: $("#HAID").combobox('getValue'),
                    HouseID: $("#AHouseID").combobox('getValue')//仓库ID
                });
            }
            //更新缺货数据
            function UpdateOutOfStocks() {
                 var url = '../Order/orderApi.aspx?method=UpdtAllOutOfStocks';
            }
        </script>
    </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div id='Loading'
            style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 98%; height: 100%; background: white; text-align: center; padding: 5px 10px; display: table;">
            <div style="display: table-cell; vertical-align: middle">
                <h1>
                    <font size="9">页面加载中……</font>
                </h1>
            </div>
        </div>
        <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'"
            style="width: 100%">
            <table>
                <tr>
                    <td style="text-align: right;">规格:
                    </td>
                    <td>
                        <input id="ASpecs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 100px" />
                    </td>
                    <td style="text-align: right;">一级产品:
                    </td>
                    <td>
                        <input id="APID" class="easyui-combobox" style="width: 100px;"  />
                    </td>
                    <td style="text-align: right;">区域大仓:
                    </td>
                    <td>
                        <input id="AHouseID" class="easyui-combobox" style="width: 100px;" data-options="required:true"
                             />
                    </td>
                </tr>
                <tr>
                    <td id="AFigureTd" style="text-align: right;">花纹:
                    </td>
                    <td>
                        <input id="AFigure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 100px" />
                    </td>
                    <td style="text-align: right;">二级产品:
                    </td>
                    <td>
                        <input id="ASID" class="easyui-combobox" style="width: 100px;"
                            data-options="valueField:'TypeID',textField:'TypeName'" />
                    </td>


                    <td style="text-align: right;">所在仓库:
                    </td>
                    <td>
                        <input id="HAID" class="easyui-combobox" style="width: 100px;"
                            data-options="valueField:'AreaID',textField:'Name',required:true"  />
                    </td>

                </tr>
                <tr>
            </table>
        </div>
        <table id="dg" class="easyui-datagrid">
        </table>
        <table id="outDg" class="easyui-datagrid">
        </table>
        <div id="toolbar" class="space">
        <span class="space">
            <a href="#" class="easyui-linkbutton tblBtn" iconcls="icon-search" plain="false"
                onclick="dosearch()">查询</a>
                </span>
        <span class="space">
            <a href="#" class="easyui-linkbutton" iconcls="icon-reload" plain="false"
                onclick="openUpdteOOSDlg()">更新缺货数据</a>
                </span>
        </div>
        
        <div id="dlg" class="easyui-dialog" style="width: 350px; height: 230px; padding: 0px" closed="true"
            buttons="#dlg-buttons">
            <form id="fm" class="easyui-form" method="post">
                <input type="hidden" id="InPiece" />
                <input type="hidden" id="InIndex" />
                <table>
                    <tr>
                        <td style="text-align: right;">补货数量：
                        </td>
                        <td>
                            <input name="RplQty" id="RplQty" class="easyui-numberbox" data-options="min:0,precision:0"
                                style="width: 170px;" type="text" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
        <div id="dlg-buttons">
            <a href="#" class="easyui-linkbutton" iconcls="icon-ok"
                onclick="outOK()">&nbsp;确&nbsp;定&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-cancel"
                onclick="javascript:$('#dlg').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
        </div>
        
        <div id="updteOOSDlg" class="easyui-dialog" style="width: 350px; height: 230px; padding: 0px" closed="true"
            buttons="#updateOOSDlg-buttons" title="更新仓库缺货数据">
            <form id="fm" class="easyui-form" method="post">
                <input type="hidden" id="InPiece" />
                <input type="hidden" id="InIndex" />
                <table>
                    <tr>
                        <td style="text-align: right;">仓库：
                        </td>
                        <td>
                        <input id="updtOOSHouseOpts" class="easyui-combobox"  style="width: 200px;" data-options="required:true"
                            />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
        <div id="updateOOSDlg-buttons">
            <a href="#" class="easyui-linkbutton tblBtn" iconcls="icon-ok"
                onclick="updtOOS_ok()">更新</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton tblBtn" iconcls="icon-cancel"
                onclick="javascript:$('#updteOOSDlg').dialog('close')">取消</a>
        </div>

        <form id="fmDep" class="easyui-form" method="post">
            <div id="saPanel">
                <table style="width: 100%">
                    <tr>
                        
                        <td style="text-align: right;">请求仓库:
                        </td>
                        <td style="width: 300px;">
                        <input id="ReqHouseOpts" class="easyui-combobox"  style="width: 200px;" data-options="required:true"
                            />
                        </td>
                        
                        <td style="text-align: right;">备注:
                        </td>
                        <td >
                        <input id="RemarkTxt" class="easyui-textbox" style="width:200px;height:60px"  style="width: 180px;" data-options="multiline:true"
                            />
                        </td>   
                        <td >

                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: right; "><a href="#" id="btnPreSave"
                                class="easyui-linkbutton" iconcls="icon-compress" plain="false" style="margin: 12px;"
                                onclick="saveData()">保&nbsp;存&nbsp;补&nbsp;货&nbsp;单</a></td>
                                
                        <td style="width: 30px;">

                        </td>
                    </tr>
                    <tr style="height: 30px;">

                    </tr>
                </table>
            </div>
        </form>
        <script type="text/javascript">
            function openUpdteOOSDlg(){
                $('#updteOOSDlg').dialog('open');
            }
            function updtOOS_ok(){
                var houseID = $('#updtOOSHouseOpts').combobox('getValue');
                console.log("house id", houseID);
                $.ajax({
                    type: "GET",
                    url: "orderApi.aspx?method=UpdtOOSByHouse",
                    data: { HouseID: houseID },
                    success: function (rsps) {
                        var rsps  = JSON.parse(rsps);
                        if (rsps.Success) {
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '更新成功！', 'info');
                            reset();
                            $('#updteOOSDlg').dialog('close');
                        }
                    }
                })
            }
            //保存
            function saveData() {
                var rows = $('#outDg').datagrid('getRows');
                if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '补货列表为空！', 'warning'); return; }

                $.messager.progress({ msg: '请稍后,正在保存中...' });
                $('#btnSave').linkbutton('disable');

                var ReqHouseID = $('#ReqHouseOpts').combobox('getValue');
                var Remark = $('#RemarkTxt').textbox('getValue'); 
                var RowsJson = JSON.stringify(rows);
                $('#fmDep').form('submit', {
                    url: 'orderApi.aspx?method=AddRplOrder',
                    contentType: "application/json;charset=utf-8", dataType: "json",
                    onSubmit: function (param) {
                        param.HouseID = ReqHouseID;
                        param.Remark = Remark;
                        param.Rows = RowsJson;
                        return;
                    },
                    success: function (resps) {
                        $.messager.progress("close");
                        $('#btnSave').linkbutton('enable');
                        resps = JSON.parse(resps);
                        console.log(resps)
                        if (resps.Success) {
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功', 'warning');
                            reset();
                        } else {
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + resps.Message, 'warning');
                        }
                    }
                })
            }
            function reset() {
                $('#outDg').datagrid('loadData', { total: 0, rows: [] });
                $('#ReqHouseOpts').combobox('setValue', '');
                $('#RplQty').numberbox('setValue',0);
                $('#RemarkTxt').textbox('setValue', '')
                var title = "";
                $('#outDg').datagrid("getPanel").panel("setTitle", title);
                dosearch();
            }
            //业务员选择方法
            function onSaleManIDChanged(item) {
                if (item) {
                    $('#SaleManName').val(item.UserName);
                    $('#SaleCellPhone').val(item.CellPhone);
                }
            }

            //删除出库的数据
            function dblClickDelCargo(index) {
                $('#outDg').datagrid('deleteRow', index);
            }
            //新增出库数据
            function outOK() {
                var row = $('#dg').datagrid('getSelected');
                if ($('#RplQty').val() == null || $('#RplQty').val() == "") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请输入补货数量！', 'warning');
                    return;
                }
                if ($('#RplQty').val() < 1) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '补货数量必须大于0！', 'warning');
                    return;
                }
                var index = $('#outDg').datagrid('getRowIndex', row.OOSID);
                if (index < 0) {
                    const newPiece = $('#RplQty').numberbox('getValue');
                    $('#outDg').datagrid('appendRow',  {...row, Piece: newPiece} );
                    $('#ReqHouseOpts').combobox('setValue', row.ParentHouse);
                } else {
                     $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单列表已存在该产品，请先删除再添加！', 'warning'); 
                }
                closedgShowData();
            }
            //关闭
            function closedgShowData() {
                $('#dlg').dialog('close');
            }
            //添加行数据
            function pullRowData() {
                $("#ActSalePrice").numberbox('enable');

                var row = $('#dg').datagrid('getSelected');
                if (row == null || row == "") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要补货的数据！', 'warning');
                    return;
                }
                
                if (row) {
                    $('#dlg').dialog('open').dialog('setTitle', '添加补货  ' + row.ProductCode);
                    $('#RplQty').numberbox('setValue', row.Piece);

                    if (row.SalePriceClient != undefined && row.SalePriceClient != "") {
                        $('#ActSalePrice').numberbox('setValue', row.SalePriceClient);
                    } else {
                        $('#ActSalePrice').numberbox('setValue', row.TradePrice);
                    }
                    $('#RplQty').numberbox().next('span').find('input').focus();
                }
            }
            
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
            function onAcceptAddressChanged(item) {
                $('#AAcceptUnit').val(item.AcceptCompany);
                $('#AAcceptAddress').textbox('setValue', item.AcceptAddress);
                $('#AAcceptTelephone').val(item.AcceptTelephone);
                $('#AAcceptCellphone').val(item.AcceptCellphone);
                $('#AAcceptPeople').val(item.AcceptPeople);
            }
            //业务编号赋值
            function onAcceptBusinessID(item) {
                $('#BusinessID').val(item.ID);
            }
            //收货人自动选择方法
            function onClientChanged(item) {
                $("#HiddenClientSelectName").val(item.Boss);
                if (item) {
                    $('#AcceptPeople').combobox({
                        valueField: 'ADID', textField: 'AcceptPeopleCellphone', delay: '10',
                        url: '../Client/clientApi.aspx?method=AutoCompleteClientAcceptPeople&ClientNum=' + item.ClientNum,
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
                    $('#ABusinessID').combobox({
                        valueField: 'ID', textField: 'DepName', delay: '10',
                        url: '../Client/clientApi.aspx?method=AutoBusinessID&ClientID=' + item.ClientID,
                        onLoadSuccess: function () {
                            //默认选中第一个
                            var array = $(this).combobox("getData");
                            for (var item in array[0]) {
                                if (item == "ID") {
                                    $(this).combobox('select', array[0][item]);
                                }
                            }
                        },
                        onSelect: onAcceptBusinessID
                    });
                    $('#ClientType').val(item.ClientType);
                    $('#CheckOutType').combobox('setValue', item.CheckOutType);
                    //$('#HiddenClientNum').val(item.ClientNum);
                    if (item.UserID != null && item.UserID != "") {
                        $('#SaleManID').combobox('setValue', item.UserID);
                        if (item.UserName != null && item.UserName != "") {
                            $('#SaleManName').val(item.UserName);
                        }
                    } else {
                        $('#SaleManID').combobox('enable');
                        $('#SaleManID').combobox('setValue', '');
                    }
                    $('#SAcceptPeople').combobox('setValue', item.ClientNum);
                    $('#SAcceptPeople').combobox('setText', item.Boss);
                    $('#ClientNum').val(item.ClientNum);
                    $('#ShopCode').val(item.ShopCode);
                    if (item.City != '') {
                        $('#ADest').combobox('setValue', item.City);
                    }
                }
            }


        </script>
    </asp:Content>