<%@ Page Title="产品基础价格导入" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="priceBasicImport.aspx.cs" Inherits="Cargo.Price.priceBasicImport" %>

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
        //页面加载执行查询
        window.onload = function () {
            document.body.style.overflow = 'hidden';
            $("#TypeID").combobox("setValue", -1);
            var HID = "<%=UserInfor.HouseID%>";
            $("#HouseID").combobox("setValue", HID);
            //dosearch();

            $("#eTypeID").combobox({
                //相当于html >> select >> onChange事件  
                onChange: function () {
                    var typeID = $('#eTypeID').combobox('getValue');
                    if (typeID == 9) {
                        $("#eBorn").combobox('options').required = true;
                        $("#eBorn").combobox('textbox').validatebox('options').required = true;
                        $("#eBorn").combobox('validate');
                        $("#eAssort").combobox('options').required = true;
                        $("#eAssort").combobox('textbox').validatebox('options').required = true;
                        $("#eAssort").combobox('validate');
                    } else {
                        $("#eBorn").combobox('options').required = false;
                        $("#eBorn").combobox('textbox').validatebox('options').required = false;
                        $("#eBorn").combobox('validate');
                        $("#eAssort").combobox('options').required = false;
                        $("#eAssort").combobox('textbox').validatebox('options').required = false;
                        $("#eAssort").combobox('validate');
                    }
                }
            });
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
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2, 300, 1000],
                fitColumns: true, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'PID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    { title: '', field: 'PID', checkbox: true, width: '30px' },
                    {
                        title: '所属仓库', field: 'HouseName', width: '95px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '产地', field: 'Born', width: '50px', formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='国产'>国产</span>"; } else { return "<span title='进口'>进口</span>"; }
                        }
                    },
                    {
                        title: '品牌', field: 'TypeName', width: '90px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '产品编码', field: 'ProductCode', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '类型', field: 'Assort', width: '45px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '型号', field: 'Model', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '货品代码', field: 'GoodsCode', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '规格', field: 'Specs', width: '90px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    //{ title: '尺寸', field: 'HubDiameter', width: '45px' },
                    {
                        title: '花纹', field: 'Figure', width: '70px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '载速', field: 'LoadIndex', width: '55px', formatter: function (value, row) {
                            return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                        }
                    },
                    //{
                    //    title: '速度', field: 'SpeedLevel', width: '40px', formatter: function (value) {
                    //        return "<span title='" + value + "'>" + value + "</span>";
                    //    }
                    //},
                    {
                        title: '订单年', field: 'ProYear', width: '45px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '订单月', field: 'ProMonth', width: '45px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '季度', field: 'ProQuarter', width: '45px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '单价', field: 'UnitPrice', width: '77px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '进仓价', field: 'InHousePrice', width: '77px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '小程序价', field: 'SalePrice', width: '77px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '门店价', field: 'TradePrice', width: '77px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: 'OE销售价', field: 'OESalePrice', width: '77px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '最终成本价', field: 'FinalCostPrice', width: '77px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '含税成本价', field: 'TaxCostPrice', width: '77px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '不含税成本价', field: 'NoTaxCostPrice', width: '80px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '次日达价', field: 'NextDayPrice', width: '80px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        } },
                    { title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { editItem(); },
            });
            var url = 'priceApi.aspx?method=QueryALLBrandType';
            $('#TypeID').combobox('reload', url);
            var houseUrl = 'priceApi.aspx?method=QueryALLHouse';
            $('#HouseID').combobox('reload', houseUrl);

            $('#HouseID').combobox('textbox').bind('focus', function () { $('#HouseID').combobox('showPanel'); });

            //列表回车响应查询
            $("#saPanelTab").keydown(function (e) { if (e.keyCode == 13) { dosearch(); } });
            //编辑回车响应保存
            //$("#editTab").keydown(function (e) { if (e.keyCode == 13) { saveItem(); } });
            //产地下拉框响应删除键清空选项
            $("#eBorn").combobox('textbox').bind('keydown', function (e) { if (e.keyCode == 8) { ClearCombobox("eBorn"); } })
            //类型下拉框响应删除键清空选项
            $("#eAssort").combobox('textbox').bind('keydown', function (e) { if (e.keyCode == 8) { ClearCombobox("eAssort"); } })

        });

        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'priceApi.aspx?method=QueryPriceBasicData';
            $('#dg').datagrid('load', {
                Born: $('#Born').combobox('getValue'),
                TypeID: $('#TypeID').combobox('getValue'),
                HouseID: $('#HouseID').combobox('getValue'),
                Assort: $('#Assort').combobox('getText'),
                ProQuarter: $('#ProQuarter').combobox('getValue'),
                Model: $('#Model').val(),
                GoodsCode: $("#GoodsCode").val(),
                Specs: $("#Specs").val(),
                //HubDiameter: $("#HubDiameter").val(),
                ProductCode: $("#ProductCode").val(),
                Figure: $("#Figure").val(),
                LoadIndex: $("#LoadIndex").val(),
                SpeedLevel: $("#SpeedLevel").val(),
                ProYear: $("#ProYear").val(),
                ProMonth: $("#ProMonth").val()
            });
        }
        //导入
        function Import() {
            $('#dlg').dialog('open').dialog('setTitle', '导入基础数据');
            showData();
        }
        //修改
        function editItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (rows.length > 1) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择一条要修改的数据！', 'warning');
                return;
            }
            if (rows[0]) {
                if (rows[0]["TypeID"] == 9) {
                    $("#eBorn").combobox('options').required = true;
                    $("#eBorn").combobox('textbox').validatebox('options').required = true;
                    $("#eBorn").combobox('validate');
                    $("#eAssort").combobox('options').required = true;
                    $("#eAssort").combobox('textbox').validatebox('options').required = true;
                    $("#eAssort").combobox('validate');
                } else {
                    $("#eBorn").combobox('options').required = false;
                    $("#eBorn").combobox('textbox').validatebox('options').required = false;
                    $("#eBorn").combobox('validate');
                    $("#eAssort").combobox('options').required = false;
                    $("#eAssort").combobox('textbox').validatebox('options').required = false;
                    $("#eAssort").combobox('validate');
                }
                $("#eHouseID").combobox("readonly", true);
                $("#eTypeID").combobox("readonly", true);
                $('#dlgedit').dialog('open').dialog('setTitle', '修改产品基础价格');
                $('#eBorn').combobox('setValue', rows[0].Born);
                var url = 'priceApi.aspx?method=QueryALLBrandType&type=edit';
                $('#eTypeID').combobox('reload', url);
                var houseUrl = 'priceApi.aspx?method=QueryALLHouse&type=edit';
                $('#eHouseID').combobox('reload', houseUrl);
                $('#eAssort').combobox('setValue', rows[0].Assort);
                $('#fm').form('load', rows[0]);
                var year = rows[0]["ProYear"];
                var month = rows[0]["ProMonth"];
                $('#eProYear').datebox('setValue', year + '-' + month);
                $('#eProYear').datebox('setText', year + '-' + month);
            }
        }
        //新增
        function addItem() {
            $("#eHouseID").combobox("readonly", false);
            $("#eTypeID").combobox("readonly", false);
            $('#dlgedit').form('clear');
            $('#dlgedit').dialog('open').dialog('setTitle', '新增产品基础价格');
            var url = 'priceApi.aspx?method=QueryALLBrandType&type=edit';
            $('#eTypeID').combobox('reload', url);
            var houseUrl = 'priceApi.aspx?method=QueryALLHouse&type=edit';
            $('#eHouseID').combobox('reload', houseUrl);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id='Loading' style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 100%; height: 100%; background: white; text-align: center; padding: 5px 10px; display: table;">
        <div style="display: table-cell; vertical-align: middle">
            <h1><font size="9">页面加载中……</font></h1>
        </div>
    </div>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table id="saPanelTab">
            <tr>
                <td style="text-align: right;">仓库:
                </td>
                <td>
                    <input id="HouseID" name="HouseID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'HouseID',textField:'HouseName',editable:true" />
                </td>
                <td style="text-align: right;">品牌:
                </td>
                <td>
                    <input id="TypeID" name="TypeID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'TypeID',textField:'TypeName',editable:true" />
                </td>
                <td style="text-align: right;">规格:
                </td>
                <td>
                    <input id="Specs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 100px" />
                </td>
                <td style="text-align: right;">货品代码:
                </td>
                <td>
                    <input id="GoodsCode" class="easyui-textbox" data-options="prompt:'请输入货品代码'" style="width: 100px" />
                </td>
                <td style="text-align: right;">型号:
                </td>
                <td>
                    <input id="Model" class="easyui-textbox" data-options="prompt:'请输入型号'" style="width: 100px" />
                </td>

                <td style="text-align: right;">载重指数:
                </td>
                <td>
                    <input id="LoadIndex" class="easyui-textbox" data-options="prompt:'请输入载重指数'" style="width: 100px" />
                </td>
                <td style="text-align: right;">订单年份:
                </td>
                <td>
                    <input id="ProYear" class="easyui-textbox" data-options="prompt:'请输入订单年份'" style="width: 100px" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">产地:
                </td>
                <td>
                    <select class="easyui-combobox" id="Born" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="-1">全部</option>
                        <option value="0">国产</option>
                        <option value="1">进口</option>
                    </select>
                </td>
                <td style="text-align: right;">类型:
                </td>
                <td>
                    <select class="easyui-combobox" id="Assort" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="-1">全部</option>
                        <option value="0">OER</option>
                        <option value="1">REP</option>
                    </select>
                </td>
                <td style="text-align: right;">花纹:
                </td>
                <td>
                    <input id="Figure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 100px" />
                </td>

                <td style="text-align: right;">产品编码:
                </td>
                <td>
                    <input id="ProductCode" class="easyui-textbox" data-options="prompt:'请输入产品编码'" style="width: 100px" />
                </td>
                <td style="text-align: right;">季度:
                </td>
                <td>
                    <select class="easyui-combobox" id="ProQuarter" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="-1">全部</option>
                        <option value="1">1季度</option>
                        <option value="2">2季度</option>
                        <option value="3">3季度</option>
                        <option value="4">4季度</option>
                    </select>
                </td>
                <td style="text-align: right;">速度级别:
                </td>
                <td>
                    <input id="SpeedLevel" class="easyui-textbox" data-options="prompt:'请输入速度级别'" style="width: 100px" />
                </td>
                <td style="text-align: right;">订单月份:
                </td>
                <td>
                    <input id="ProMonth" class="easyui-textbox" data-options="prompt:'请输入订单月份'" style="width: 100px" />
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">&nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-page_copy" plain="false" onclick="Copy()">&nbsp;复&nbsp;制&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-page_copy" plain="false" onclick="CopyAll()">&nbsp;复制全部&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-in_cargo" plain="false" onclick="Import()">&nbsp;导&nbsp;入&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="Synchronize()">&nbsp;同&nbsp;步&nbsp;</a>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 1000px; height: 550px; padding: 2px 2px" closed="true" buttons="#dlg-buttons">
        <table id="dgImport" class="easyui-datagrid">
        </table>
        <div id="dginporttoolbar">
            <input type="file" id="fileT" name="file" accept=".xls,.xlsx" onchange="saveFile()" style="width: 250px;" />
            <input type="hidden" id="ExistCount" />
            <a href="#" id="btnload" class="easyui-linkbutton" iconcls="icon-out_cargo" plain="false" onclick="saveFile()">&nbsp;重新上传&nbsp;</a>&nbsp;&nbsp;
            <a href="../upload/sFile/基础价格导入模板.xls" id="dowload" class="easyui-linkbutton" iconcls="icon-application_put" plain="false">&nbsp;下载模板&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-ok" plain="false" onclick="btnSaveData()">&nbsp;保存数据&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closeDlgStatus()">&nbsp;关&nbsp;闭&nbsp;</a>
        </div>
    </div>
    <!--Begin 复制-->
    <div id="dlgCopy" class="easyui-dialog" style="width: 400px; height: 250px; padding: 0px"
        closed="true" buttons="#dlgCopy-buttons">
        <form id="fmdlgCopy" class="easyui-form" method="post">
            <input type="hidden" name="SingleCopyPID" id="SingleCopyPID" />
            <input type="hidden" name="SingleCopyProYear" id="SingleCopyProYear" />
            <input type="hidden" name="SingleCopyProMonth" id="SingleCopyProMonth" />
            <input type="hidden" name="SingleCopyProQuarter" id="SingleCopyProQuarter" />
            <input type="hidden" name="SingleCopySpecs" id="SingleCopySpecs" />
            <input type="hidden" name="SingleCopyFigure" id="SingleCopyFigure" />
            <input type="hidden" name="SingleCopyGoodsCode" id="SingleCopyGoodsCode" />
            <table>
                <tr>
                    <td colspan="4" style="color: #22284e">
                        <h1 id="information"></h1>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">复制到:
                    </td>
                    <td>
                        <input id="SingleNewHouse" name="SingleNewHouse" class="easyui-combobox" style="width: 150px;" data-options="valueField:'HouseID',textField:'HouseName',editable:false,required:true" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">复制到:
                    </td>
                    <td colspan="3">
                        <input name="SingleCopyNewDate" id="SingleCopyNewProYear" class="easyui-datebox" data-options="required:false" style="width: 100px" editable="false" />&nbsp;&nbsp;&nbsp;第&nbsp;<input name="SingleCopyNewProQuarter" id="SingleCopyNewProQuarter" class="easyui-numberbox" data-options="min:1,max:4,precision:0,required:false" readonly="readonly" style="width: 35px;">&nbsp;季度
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="color: #808080">注意：复制日期为空时默认为原始数据日期</td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlgCopy-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="CopyProduct()">&nbsp;确&nbsp;定&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgCopy').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <!--End 复制-->
    <!--Begin 复制全部-->
    <div id="dlgAllCopy" class="easyui-dialog" style="width: 350px; height: 200px; padding: 5px 5px"
        closed="true" buttons="#dlgAllCopy-buttons">
        <form id="fmdlgAllCopy" class="easyui-form" method="post">
            <table>
                <tr>
                    <td style="text-align: right;">来源仓库:
                    </td>
                    <td>
                        <input id="OldHouse" name="OldHouse" class="easyui-combobox" style="width: 100px;" data-options="valueField:'HouseID',textField:'HouseName',editable:false,required:true" />
                    </td>
                    <td style="text-align: right;">复制到:
                    </td>
                    <td>
                        <input id="NewHouse" name="NewHouse" class="easyui-combobox" style="width: 100px;" data-options="valueField:'HouseID',textField:'HouseName',editable:false,required:true" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">来源日期:
                    </td>
                    <td colspan="3">
                        <input name="CopyOldDate" id="copyOldProYear" class="easyui-datebox" data-options="required:true" style="width: 100px" editable="false" />&nbsp;&nbsp;&nbsp;第&nbsp;<input name="copyOldProQuarter" id="copyOldProQuarter" class="easyui-numberbox" data-options="min:1,max:4,precision:0,required:true" readonly="readonly" style="width: 35px;">&nbsp;季度
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">复制到:
                    </td>
                    <td colspan="3">
                        <input name="CopyNewDate" id="copyNewProYear" class="easyui-datebox" data-options="required:false" style="width: 100px" editable="false" />&nbsp;&nbsp;&nbsp;第&nbsp;<input name="copyNewProQuarter" id="copyNewProQuarter" class="easyui-numberbox" data-options="min:1,max:4,precision:0,required:false" readonly="readonly" style="width: 35px;">&nbsp;季度
                    </td>
                </tr>
                <tr>
                    <td colspan="4" style="color: #808080">注意：复制日期为空时默认为来源日期</td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlgAllCopy-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="CopyAllProduct()">&nbsp;确&nbsp;定&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgAllCopy').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <!--End 复制-->
    <%--编辑--%>
    <div id="dlgedit" class="easyui-dialog" style="width: 700px; height: 400px; padding: 0px"
        closed="true" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" name="PID" />
            <div id="saPanel">
                <table>
                    <tr>
                        <td style="text-align: right;">所属仓库:
                        </td>
                        <td>
                            <input id="eHouseID" name="HouseID" class="easyui-combobox" style="width: 200px;" data-options="valueField:'HouseID',textField:'HouseName',editable:false,required:true" />
                        </td>
                        <td style="text-align: right;">产品编码:
                        </td>
                        <td>
                            <input name="ProductCode" class="easyui-textbox" data-options="required:true" style="width: 200px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">产地:
                        </td>
                        <td>
                            <select class="easyui-combobox" id="eBorn" name="Born" style="width: 200px;" panelheight="auto" editable="false">
                                <option value="0">国产</option>
                                <option value="1">进口</option>
                            </select>
                        </td>
                        <td style="text-align: right;">品牌:
                        </td>
                        <td>
                            <input id="eTypeID" name="TypeID" class="easyui-combobox" style="width: 200px;" data-options="valueField:'TypeID',textField:'TypeName',editable:false,required:true" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">类型:
                        </td>
                        <td>
                            <select class="easyui-combobox" id="eAssort" name="Assort" style="width: 200px;"
                                panelheight="auto" editable="false">
                                <option value="OER">OER</option>
                                <option value="REP">REP</option>
                            </select>
                        </td>
                        <td style="text-align: right;">型号:
                        </td>
                        <td>
                            <input name="Model" id="eModel" class="easyui-textbox" data-options="min:0,precision:0,required:true" style="width: 200px;" validtype="length[0,20]">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">代码:
                        </td>
                        <td>
                            <input name="GoodsCode" id="eGoodsCode" class="easyui-textbox" data-options="min:0,precision:2,required:true" style="width: 200px;" validtype="length[0,20]">
                        </td>
                        <td style="text-align: right;">规格:
                        </td>
                        <td>
                            <input name="Specs" id="eSpecs" class="easyui-textbox" data-options="min:0,precision:2,required:true"
                                style="width: 200px;" validtype="length[0,50]">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">尺寸:
                        </td>
                        <td>
                            <input name="HubDiameter" id="eHubDiameter" class="easyui-textbox" data-options="min:0,precision:2,required:true" style="width: 200px;" validtype="length[0,4]">
                        </td>
                        <td style="text-align: right;">花纹:
                        </td>
                        <td>
                            <input name="Figure" id="eFigure" class="easyui-textbox" data-options="min:0,precision:2,required:true" style="width: 200px;" validtype="length[0,50]">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">载重:
                        </td>
                        <td>
                            <input name="LoadIndex" id="eLoadIndex" class="easyui-textbox" data-options="min:0,precision:0,required:true" style="width: 200px;" validtype="length[0,5]">
                        </td>
                        <td style="text-align: right;">速度:
                        </td>
                        <td>
                            <input name="SpeedLevel" id="eSpeedLevel" class="easyui-textbox" data-options="required:true" validtype="length[0,1]"
                                style="width: 200px;">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">订单日期:
                        </td>
                        <td>
                            <input name="Date" id="eProYear" class="easyui-datebox" data-options="required:true" style="width: 100px" editable="false" />&nbsp;&nbsp;&nbsp;第&nbsp;<input name="ProQuarter" id="eProQuarter" class="easyui-numberbox" data-options="min:1,max:4,precision:0,required:true" readonly="readonly" style="width: 35px;">&nbsp;季度
                        </td>
                        <td style="text-align: right;">云仓类型:
                        </td>
                        <td>
                            <select class="easyui-combobox" id="CloudHouseType" name="CloudHouseType" style="width: 200px;"
                                panelheight="auto" editable="false" data-options="required:true">
                                <option value="0">非云仓</option>
                                <option value="1">云仓</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">单价:
                        </td>
                        <td>
                            <input name="UnitPrice" id="eUnitPrice" class="easyui-numberbox" data-options="min:0,precision:2,required:true" style="width: 200px;" validtype="length[0,12]" />
                        </td>
                        <td style="text-align: right;">进仓价:
                        </td>
                        <td>
                            <input name="InHousePrice" id="InHousePrice" class="easyui-numberbox" data-options="min:0,precision:2,required:true" style="width: 200px;" validtype="length[0,12]" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">门店价:
                        </td>
                        <td>
                            <input name="TradePrice" id="eTradePrice" class="easyui-numberbox" data-options="min:0,precision:2,required:true" style="width: 200px" validtype="length[0,12]" />
                        </td>
                        <td style="text-align: right;">最终成本价:
                        </td>
                        <td>
                            <input name="FinalCostPrice" id="eFinalCostPrice" class="easyui-numberbox" data-options="min:0,precision:2,required:true" style="width: 200px" validtype="length[0,12]" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">含税成本:
                        </td>
                        <td>
                            <input name="TaxCostPrice" id="eTaxCostPrice" class="easyui-numberbox" data-options="min:0,precision:2,required:true" style="width: 200px" validtype="length[0,12]" />
                        </td>
                        <td style="text-align: right;">不含税成本:
                        </td>
                        <td>
                            <input name="NoTaxCostPrice" id="eNoTaxCostPrice" class="easyui-numberbox" data-options="min:0,precision:2,required:true" style="width: 200px" validtype="length[0,12]" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">次日达价:
                        </td>
                        <td>
                            <input name="NextDayPrice" id="eNextDayPrice" class="easyui-numberbox" data-options="min:0,precision:2,required:true" style="width: 200px" validtype="length[0,12]" />
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" id="btnSave" iconcls="icon-ok" onclick="saveItem()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgedit').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <%--编辑结束--%>
    <%--<table id="editTab">
        <tr>
            <td style="vertical-align: top"></td>
        </tr>
    </table>--%>
    <script src="../JS/easy/js/ajaxfileupload.js"></script>
    <script type="text/javascript">
        function ClearCombobox(id) {
            $('#' + id).combobox('clear');//清空选中项
            //$('#' + id).combobox('loadData', {});//清空option选项   
        }
        //导入产品基础价格数据
        function ImportTaobao() {
            $('#dlg').dialog('open').dialog('setTitle', '导入产品基础价格数据');
            showData();
            $('#dgImport').datagrid('loadData', { total: 0, rows: [] });
        }
        //复制数据
        function Copy() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要复制的数据！', 'warning'); return;
            }
            if (rows.length > 1) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择一行数据！', 'warning'); return;
            }
            var row = $('#dg').datagrid('getSelected');
            if (row) {
                $('#dlgCopy').dialog('open').dialog('setTitle', '复制基础价格');
                $('#information').text('仓库：' + row.HouseName + '，规格：' + row.Specs + '，花纹：' + row.Figure + '，货品代码：' + row.GoodsCode + '，订单日期：' + row.ProYear + '-' + row.ProMonth);
                $('#dlgCopy').form('clear');
                var houseUrl = 'priceApi.aspx?method=QueryALLHouse&type=copy';
                $('#SingleNewHouse').combobox('reload', houseUrl);
                $('#SingleCopyPID').val(row.PID);
                $('#SingleCopyProYear').val(row.ProYear);
                $('#SingleCopyProMonth').val(row.ProMonth);
                $('#SingleCopyProQuarter').val(row.ProQuarter);
                $('#SingleCopySpecs').val(row.Specs);
                $('#SingleCopyFigure').val(row.Figure);
                $('#SingleCopyGoodsCode').val(row.GoodsCode);
            }
        }

        //保存复制数据
        function CopyProduct() {
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定保存？', function (r) {
                var msg = "确定保存？";
                $('#fmdlgCopy').form('submit', {
                    url: 'priceApi.aspx?method=SaveCopyPriceBasicData',
                    onSubmit: function () {
                        var check = $(this).form('enableValidation').form('validate');
                        if (!check) { $.messager.progress("close"); }
                        return check;
                    },
                    success: function (msg) {
                        $.messager.progress("close");
                        var result = eval('(' + msg + ')');
                        if (result.Result) {
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '复制成功!', 'info');
                            $('#dlgCopy').dialog('close');
                            dosearch();
                        } else {
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '复制失败：' + result.Message, 'warning');
                        }
                    }
                })
            });
        }
        //复制全部数据
        function CopyAll() {
            $('#dlgAllCopy').dialog('open').dialog('setTitle', '复制基础价格');
            $('#OldHouse').combobox('clear');
            $('#NewHouse').combobox('clear');
            $('#copyProYear').datebox('clear');
            $('#copyProQuarter').numberbox('clear');

            var houseUrl = 'priceApi.aspx?method=QueryALLHouse&type=copy';
            $('#OldHouse').combobox('reload', houseUrl);
            $('#NewHouse').combobox('reload', houseUrl);
        }

        //保存复制全部数据
        function CopyAllProduct() {
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定保存？', function (r) {
                var msg = "确定保存？";
                $('#fmdlgAllCopy').form('submit', {
                    url: 'priceApi.aspx?method=SaveAllCopyPriceBasicData',
                    onSubmit: function () {
                        var check = $(this).form('enableValidation').form('validate');
                        if (!check) { $.messager.progress("close"); }
                        return check;
                    },
                    success: function (msg) {
                        $.messager.progress("close");
                        var result = eval('(' + msg + ')');
                        if (result.Result) {
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '复制成功!', 'info');
                            $('#dlgAllCopy').dialog('close');
                            dosearch();
                        } else {
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '复制失败：' + result.Message, 'warning');
                        }
                    }
                })
            });
        }
        //显示DataGrid数据列表
        function showData() {
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
                columns: [[
                    { title: '所属仓库', field: 'HouseName', width: '70px' },
                    {
                        title: '产地', field: 'Born', width: '40px', formatter: function (val, row, index) {
                            if (val == "0") { return "国产"; } else { return "进口"; }
                        }
                    },
                    { title: '品牌', field: 'TypeName', width: '70px' },
                    { title: '产品编码', field: 'ProductCode', width: '70px' },
                    { title: '类型', field: 'Assort', width: '45px' },
                    { title: '型号', field: 'Model', width: '50px' },
                    { title: '代码', field: 'GoodsCode', width: '50px' },
                    { title: '规格', field: 'Specs', width: '70px' },
                    //{ title: '尺寸', field: 'HubDiameter', width: '35px' },
                    { title: '花纹', field: 'Figure', width: '40px' },
                    { title: '载重', field: 'LoadIndex', width: '35px' },
                    { title: '速度', field: 'SpeedLevel', width: '35px' },
                    { title: '季度', field: 'ProQuarter', width: '45px' },
                    { title: '订单月', field: 'ProMonth', width: '45px' },
                    { title: '单价', field: 'UnitPrice', width: '60px', align: 'right' },
                    { title: '进仓价', field: 'InHousePrice', width: '60px', align: 'right' },
                    { title: '门店价', field: 'TradePrice', width: '60px', align: 'right' },
                    { title: 'OE销售价', field: 'OESalePrice', width: '60px', align: 'right' },
                    { title: '最终成本价', field: 'FinalCostPrice', width: '70px', align: 'right' },
                    { title: '含税成本价', field: 'TaxCostPrice', width: '70px', align: 'right' },
                    { title: '不含税成本价', field: 'NoTaxCostPrice', width: '80px', align: 'right' },
                    { title: '次日达价', field: 'NextDayPrice', width: '80px', align: 'right' },
                    //{
                    //    title: '云仓类型', field: 'CloudHouseType', width: '40px', formatter: function (val, row, index) {
                    //        if (val == "0") { return "非云仓"; } else { return "云仓"; }
                    //    }
                    //}
                ]]
            });
        }
        //订单月变化修改订单季度
        $("#eProYear").numberbox({
            "onChange": function () {
                var month = $('#eProYear').val();
                $('#eProQuarter').numberbox('setValue', parseInt(((month * 1 + 2 * 1) / 3)));
            }
        });
        //保存上传的文件
        function saveFile() {
            var file = $("#fileT").val();
            if (file == null || file == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择导入文件!', 'info');
                return;
            }
            $('#dgImport').datagrid('loadData', { total: 0, rows: [] });
            ajaxFileUpload();
        }

        //文件上传
        function ajaxFileUpload() {
            $('#ExistCount').val('');
            $.ajaxFileUpload({
                url: 'priceApi.aspx?method=saveFile',
                secureuri: false,
                fileElementId: 'fileT',
                dataType: 'json',
                success: function (data) {
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
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', message, 'warning');
                    }
                },
                error: function (data) {
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
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', message, 'warning');
                    }
                }
            })
            return false;
        }

        //保存数据
        function btnSaveData() {
            var rows = $("#dgImport").datagrid('getData').rows;
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请先导入需要保存的数据！', 'warning');
                return;
            }
            var msg = "确定保存？";
            //var existCount = $('#ExistCount').val();
            //if (existCount > 0) {
            //    msg = "当前系统已存在" + existCount + "条本次导入同期数据，保存将覆盖所有数据，确定保存？";
            //}
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', msg, function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    //var json = JSON.stringify(rows)
                    var json = ''
                    $.ajax({
                        url: 'priceApi.aspx?method=SavePriceBasicData',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '导入成功!', 'info');
                                $('#dlg').dialog('close');
                                dosearch();
                                $("#fileT").val("");
                                $('#dgImport').datagrid('loadData', { total: 0, rows: [] });
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }


        //关闭数据导入弹出框
        function closeDlgStatus() {
            $('#dlg').dialog('close');
            $('#dgImport').datagrid('loadData', { total: 0, rows: [] });
            $('#fileT').val("");
        }

        //删除数据
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
                        url: 'priceApi.aspx?method=DelPriceBasic',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                $('#dg').datagrid('clearSelections');
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

        //保存基础价格信息
        function saveItem() {
            $.messager.progress({ msg: '请稍后,正在保存中...' });
            $('#fm').form('submit', {
                url: 'priceApi.aspx?method=SavePriceBasic',
                onSubmit: function () {
                    var check = $(this).form('enableValidation').form('validate');
                    if (!check) { $.messager.progress("close"); }
                    return check;
                },
                success: function (msg) {
                    $.messager.progress("close");
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $('#dlgedit').dialog('close');
                        dosearch();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
        //同步价格信息
        function Synchronize() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要同步的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定同步？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'priceApi.aspx?method=SynchronizePriceBasic',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '同步成功!', 'info');
                                $('#dg').datagrid('clearSelections');
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
        $(function () {
            $('#eProYear').datebox({
                //显示日趋选择对象后再触发弹出月份层的事件，初始化时没有生成月份层
                onShowPanel: function () {
                    //触发click事件弹出月份层
                    span.trigger('click');
                    if (!btds)
                        //延时触发获取月份对象，因为上面的事件触发和对象生成有时间间隔
                        setTimeout(function () {
                            btds = p.find('div.calendar-menu-month-inner td');
                            btds.click(function (e) {
                                //禁止冒泡执行easyui给月份绑定的事件
                                e.stopPropagation();
                                //得到年份
                                var year = /\d{4}/.exec(span.html())[0],
                                    //月份
                                    //之前是这样的month = parseInt($(this).attr('abbr'), 10) + 1; 
                                    month = parseInt($(this).attr('abbr'), 10);

                                //隐藏日期对象                     
                                $('#eProYear').datebox('hidePanel').datebox('setValue', year + '-' + month);
                            });
                        }, 0);
                },
                //配置parser，返回选择的日期
                parser: function (s) {
                    if (!s) return new Date();
                    var arr = s.split('-');
                    return new Date(parseInt(arr[0], 10), parseInt(arr[1], 10) - 1, 1);
                },
                //配置formatter，只返回年月 之前是这样的d.getFullYear() + '-' +(d.getMonth()); 
                formatter: function (d) {
                    var currentMonth = (d.getMonth() + 1);
                    var currentMonthStr = currentMonth < 10 ? ('0' + currentMonth) : (currentMonth + '');

                    $('#eProQuarter').numberbox('setValue', parseInt(((currentMonthStr * 1 + 2 * 1) / 3)));
                    return d.getFullYear() + '-' + currentMonthStr;
                }
            });
            //日期选择对象
            var p = $('#eProYear').datebox('panel'),
                //日期选择对象中月份
                btds = false,
                //显示月份层的触发控件
                span = p.find('span.calendar-text');
        });
        $(function () {
            $('#copyOldProYear').datebox({
                //显示日趋选择对象后再触发弹出月份层的事件，初始化时没有生成月份层
                onShowPanel: function () {
                    //触发click事件弹出月份层
                    span.trigger('click');
                    if (!btds)
                        //延时触发获取月份对象，因为上面的事件触发和对象生成有时间间隔
                        setTimeout(function () {
                            btds = p.find('div.calendar-menu-month-inner td');
                            btds.click(function (e) {
                                //禁止冒泡执行easyui给月份绑定的事件
                                e.stopPropagation();
                                //得到年份
                                var year = /\d{4}/.exec(span.html())[0],
                                    //月份
                                    //之前是这样的month = parseInt($(this).attr('abbr'), 10) + 1; 
                                    month = parseInt($(this).attr('abbr'), 10);

                                //隐藏日期对象                     
                                $('#copyOldProYear').datebox('hidePanel').datebox('setValue', year + '-' + month);
                            });
                        }, 0);
                },
                //配置parser，返回选择的日期
                parser: function (s) {
                    if (!s) return new Date();
                    var arr = s.split('-');
                    return new Date(parseInt(arr[0], 10), parseInt(arr[1], 10) - 1, 1);
                },
                //配置formatter，只返回年月 之前是这样的d.getFullYear() + '-' +(d.getMonth()); 
                formatter: function (d) {
                    var currentMonth = (d.getMonth() + 1);
                    var currentMonthStr = currentMonth < 10 ? ('0' + currentMonth) : (currentMonth + '');

                    $('#copyOldProQuarter').numberbox('setValue', parseInt(((currentMonthStr * 1 + 2 * 1) / 3)));
                    return d.getFullYear() + '-' + currentMonthStr;
                }
            });
            //日期选择对象
            var p = $('#copyOldProYear').datebox('panel'),
                //日期选择对象中月份
                btds = false,
                //显示月份层的触发控件
                span = p.find('span.calendar-text');
        });
        $(function () {
            $('#copyNewProYear').datebox({
                //显示日趋选择对象后再触发弹出月份层的事件，初始化时没有生成月份层
                onShowPanel: function () {
                    //触发click事件弹出月份层
                    span.trigger('click');
                    if (!btds)
                        //延时触发获取月份对象，因为上面的事件触发和对象生成有时间间隔
                        setTimeout(function () {
                            btds = p.find('div.calendar-menu-month-inner td');
                            btds.click(function (e) {
                                //禁止冒泡执行easyui给月份绑定的事件
                                e.stopPropagation();
                                //得到年份
                                var year = /\d{4}/.exec(span.html())[0],
                                    //月份
                                    //之前是这样的month = parseInt($(this).attr('abbr'), 10) + 1; 
                                    month = parseInt($(this).attr('abbr'), 10);

                                //隐藏日期对象                     
                                $('#copyNewProYear').datebox('hidePanel').datebox('setValue', year + '-' + month);
                            });
                        }, 0);
                },
                //配置parser，返回选择的日期
                parser: function (s) {
                    if (!s) return new Date();
                    var arr = s.split('-');
                    return new Date(parseInt(arr[0], 10), parseInt(arr[1], 10) - 1, 1);
                },
                //配置formatter，只返回年月 之前是这样的d.getFullYear() + '-' +(d.getMonth()); 
                formatter: function (d) {
                    var currentMonth = (d.getMonth() + 1);
                    var currentMonthStr = currentMonth < 10 ? ('0' + currentMonth) : (currentMonth + '');

                    $('#copyNewProQuarter').numberbox('setValue', parseInt(((currentMonthStr * 1 + 2 * 1) / 3)));
                    return d.getFullYear() + '-' + currentMonthStr;
                }
            });
            //日期选择对象
            var p = $('#copyNewProYear').datebox('panel'),
                //日期选择对象中月份
                btds = false,
                //显示月份层的触发控件
                span = p.find('span.calendar-text');
        });
        $(function () {
            $('#SingleCopyNewProYear').datebox({
                //显示日趋选择对象后再触发弹出月份层的事件，初始化时没有生成月份层
                onShowPanel: function () {
                    //触发click事件弹出月份层
                    span.trigger('click');
                    if (!btds)
                        //延时触发获取月份对象，因为上面的事件触发和对象生成有时间间隔
                        setTimeout(function () {
                            btds = p.find('div.calendar-menu-month-inner td');
                            btds.click(function (e) {
                                //禁止冒泡执行easyui给月份绑定的事件
                                e.stopPropagation();
                                //得到年份
                                var year = /\d{4}/.exec(span.html())[0],
                                    //月份
                                    //之前是这样的month = parseInt($(this).attr('abbr'), 10) + 1; 
                                    month = parseInt($(this).attr('abbr'), 10);

                                //隐藏日期对象                     
                                $('#SingleCopyNewProYear').datebox('hidePanel').datebox('setValue', year + '-' + month);
                            });
                        }, 0);
                },
                //配置parser，返回选择的日期
                parser: function (s) {
                    if (!s) return new Date();
                    var arr = s.split('-');
                    return new Date(parseInt(arr[0], 10), parseInt(arr[1], 10) - 1, 1);
                },
                //配置formatter，只返回年月 之前是这样的d.getFullYear() + '-' +(d.getMonth()); 
                formatter: function (d) {
                    var currentMonth = (d.getMonth() + 1);
                    var currentMonthStr = currentMonth < 10 ? ('0' + currentMonth) : (currentMonth + '');

                    $('#SingleCopyNewProQuarter').numberbox('setValue', parseInt(((currentMonthStr * 1 + 2 * 1) / 3)));
                    return d.getFullYear() + '-' + currentMonthStr;
                }
            });
            //日期选择对象
            var p = $('#SingleCopyNewProYear').datebox('panel'),
                //日期选择对象中月份
                btds = false,
                //显示月份层的触发控件
                span = p.find('span.calendar-text');
        });
    </script>
</asp:Content>
