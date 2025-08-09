<%@ Page Title="采购订单确认" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OesOrderConfirm.aspx.cs" Inherits="Cargo.Order.OesOrderConfirm" %>
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
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OrderID',
                url: null,
                toolbar: '#dgtoolbar',
                columns: [[
                    { title: '', field: 'OrderID', checkbox: true, width: '30px' },
                    {
                        title: '出库仓库', field: 'OutHouseName', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '外采订单号', field: 'OrderNo', width: '110px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '出发站', field: 'Dep', width: '70px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '城市', field: 'Dest', width: '70px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '数量', field: 'Piece', width: '50px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '收入', field: 'TransportFee', width: '50px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '合计', field: 'TotalCharge', width: '60px', align: 'right', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '付款人', field: 'PayClientName', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '客户名称', field: 'AcceptUnit', width: '130px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '收货人', field: 'AcceptPeople', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '联系手机', field: 'AcceptCellphone', width: '90px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '收货地址', field: 'AcceptAddress', width: '140px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '业务员', field: 'SaleManName', width: '55px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '开单员ID', field: 'CreateAwbID', width: '60px', hidden: 'true', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    { title: '开单时间', field: 'CreateDate', width: '125px', formatter: DateTimeFormatter },
                    {
                        title: '开单员', field: 'CreateAwb', width: '60px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '结算方式', field: 'CheckOutType', width: '60px',
                        formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='现付'>现付</span>"; }
                            else if (val == "1") { return "<span title='周期'>周期</span>"; }
                            else if (val == "2") { return "<span title='月结'>月结</span>"; }
                            else if (val == "3") { return "<span title='到付'>到付</span>"; }
                            else if (val == "4") { return "<span title='代收'>代收</span>"; }
                            else if (val == "5") { return "<span title='微信付款'>微信付款</span>"; }
                            else if (val == "6") { return "<span title='额度付款'>额度付款</span>"; }
                            else { return ""; }
                        }
                    },
                    {
                        title: '订单状态', field: 'IsMakeSure', width: '60px',
                        formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='未报价'>未报价</span>"; }
                            else if (val == "1") { return "<span title='已确认'>已确认</span>"; }
                            else if (val == "2") { return "<span title='已报价'>已报价</span>"; }
                            else if (val == "3") { return "<span title='已取消'>已取消</span>"; }
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
                },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });

            if (<%=UserInfor.LoginName%>!= "1000" &&<%=UserInfor.LoginName%>!= "2076" &&<%=UserInfor.LoginName%>!= "2409") {
                $('#DelItem').hide();
            }
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
            gridOpts.url = 'orderApi.aspx?method=QueryOesPreOrderInfo&type=confirm';
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
                <td style="text-align: right;">订单状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="AIsMakeSure" name="IsMakeSure" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="2">已报价</option>
                        <option value="0">未报价</option>
                        <option value="1">已确认</option>
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
        <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()" id="DelItem">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
        <%--<a href="#" class="easyui-linkbutton" iconcls="icon-ok" plain="false" onclick="OpenConfirm()">&nbsp;确认订单&nbsp;</a>&nbsp;&nbsp;--%>
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" plain="false" onclick="OpenCancel()">&nbsp;取消订单&nbsp;</a>&nbsp;&nbsp;
    </div>
    <div id="dlgOrder" class="easyui-dialog" style="width: 1080px; height: 540px;" closed="true" closable="false" buttons="#dlgOrder-buttons">
    <form id="fmDep" method="post">
            <input type="hidden" name="SaleManName" id="SaleManName" />
            <input type="hidden" name="SaleCellPhone" id="SaleCellPhone" />
            <input type="hidden" name="ONum" id="ONum" />
            <input type="hidden" name="OutNum" id="OutNum" />
            <input type="hidden" name="OrderID" id="OrderID" />
            <input type="hidden" name="OrderNo" id="OrderNo" />
            <input type="hidden" name="ClientNum" id="ClientNum" />
            <input type="hidden" id="HiddenClientNum" />
            <input type="hidden" id="HiddenAcceptPeople" />
            <input type="hidden" name="HouseID" />
            <div id="saPanel">
                <table style="width: 100%">
                    <tr>
                        <td style="color: Red; font-weight: bolder; text-align: right;">出发站:
                        </td>
                        <td>
                            <input name="Dep" id="ADep" class="easyui-textbox" readonly="readonly" style="width: 60px">
                        </td>
                        <td style="color: Red; font-weight: bolder; text-align: right;">到达站:
                        </td>
                        <td>
                            <input name="Dest" id="ADest" class="easyui-combobox" style="width: 60px;" data-options="valueField:'CityName',textField:'CityName',url:'../systempage/sysService.aspx?method=QueryAllCity',required:false " />
                        </td>
                        <td style="text-align: right;">收货人:
                        </td>
                        <td>
                            <input name="AcceptPeople" id="AAcceptPeople" style="width: 80px;" class="easyui-combobox" />
                        </td>
                        <td style="text-align: right;">公司名称:
                        </td>
                        <td>
                            <input name="AcceptUnit" id="AAcceptUnit" class="easyui-textbox" style="width: 80px;" />
                        </td>

                        <td style="text-align: right;">收货地址:
                        </td>
                        <td>
                            <input name="AcceptAddress" id="AAcceptAddress" style="width: 140px;" class="easyui-textbox" />
                        </td>
                        <td style="text-align: right;">电话:
                        </td>
                        <td>
                            <input name="AcceptTelephone" id="AAcceptTelephone" class="easyui-textbox" style="width: 90px;" />
                        </td>
                        <td style="text-align: right;">手机:
                        </td>
                        <td>
                            <input name="AcceptCellphone" id="AAcceptCellphone" class="easyui-textbox" data-options="required:false" style="width: 90px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">总件数:
                        </td>
                        <td>
                            <input name="Piece" id="APiece" class="easyui-numberbox" data-options="min:0,precision:0,required:true" style="width: 60px;" readonly="true" />
                        </td>
                        <td style="text-align: right;">销售费用:
                        </td>
                        <td>
                            <input name="TransportFee" id="TransportFee" data-options="min:0,precision:2,required:true" class="easyui-numberbox" style="width: 60px;" />
                        </td>
                        <td style="text-align: right;">送货费用:
                        </td>
                        <td>
                            <input name="TransitFee" id="TransitFee" data-options="min:0,precision:2" class="easyui-numberbox" style="width: 80px;" />
                        </td>
                        <td style="text-align: right; display: none"></td>
                        <td style="display: none"></td>
                        <td style="text-align: right; display: none">其它费用:
                        </td>
                        <td style="display: none">
                            <input name="OtherFee" id="OtherFee" class="easyui-numberbox" data-options="min:0,precision:2" style="width: 80px;" />
                        </td>
                        <td style="text-align: right;">费用合计:
                        </td>
                        <td>
                            <input name="TotalCharge" id="TotalCharge" class="easyui-numberbox" data-options="min:0,precision:2,required:true" style="width: 80px;" />
                        </td>
                        <td style="text-align: right;">开单时间:
                        </td>
                        <td>
                            <input name="CreateDate" id="CreateDate" class="easyui-datetimebox" data-options="formatter:AllDateTime" readonly="true" style="width: 140px;" />
                        </td>
                        <td style="text-align: right;">付款方式:
                        </td>
                        <td>
                            <select class="easyui-combobox" id="CheckOutType" name="CheckOutType" style="width: 90px;" panelheight="auto" editable="false">
                                <option value="2">月结</option>
                                <option value="0">现付</option>
                                <option value="4">代收</option>
                            </select>
                        </td>
                        <td style="text-align: right;">业务员:
                        </td>
                        <td>
                            <input name="SaleManID" id="SaleManID" class="easyui-combobox" style="width: 90px;" data-options="url: 'orderApi.aspx?method=QueryUserByDepCode',valueField: 'LoginName',textField: 'UserName', onSelect: onSaleManIDChanged," />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" rowspan="2">备注:
                        </td>
                        <td colspan="7" rowspan="2">
                            <textarea name="Remark" id="ARemark" rows="3" style="width: 450px; resize: none"></textarea>
                        </td>
                        <td style="text-align: right;">所属仓库:
                        </td>
                        <td>
                            <%--<input id="HouseID" name="HouseID" class="easyui-combobox" style="width: 95px;" data-options="required:true" panelheight="auto" />--%>
                            <input name="OutHouseName" id="HouseID" class="easyui-textbox" style="width: 80px;" />
                        </td>
                        <td style="text-align: right;">
                        </td>
                        <td>
                        </td>
                        <td style="text-align: right;" colspan="2">
                            <input type="checkbox" id="IsPrintPrice" name="IsPrintPrice" checked="checked" value="1" />
                            <label for="IsPrintPrice">打印价格</label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">采购商:
                        </td>
                        <td>
                            <input name="PurchaserName" id="PurchaserName" class="easyui-textbox" style="width: 80px;" />
                        </td>
                    </tr>
                </table>
            </div>
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
                    <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="OpenConfirm()" id="save">&nbsp;确认订单</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closeDlg()">&nbsp;关&nbsp;闭&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    
    <div id="Cancel" class="easyui-dialog" style="width: 400px; height: 250px; padding: 0px" closed="true" buttons="#dlg-buttons">
        <form id="fmCancel" class="easyui-form" method="post">
            <table>
                <tr>
                    <td style="text-align: right;">取消原因:
                    </td>
                    <td>
                        <textarea name="PurchaseRemark" id="APurchaseRemark" rows="3" placeholder="请输入取消原因" style="width: 300px;height:100px; resize: none"></textarea>
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="ConfirmCancel()" id="ConfirmCancel">保存</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#Cancel').dialog('close')">取消</a>
    </div>
    <script type="text/javascript">
        function editItemByID(Did) {
            $('#dgSave').datagrid('loadData', { total: 0, rows: [] });
            IsModifyOrder = false;
            editIndex = undefined;
            var row = $("#dg").datagrid('getData').rows[Did];
            rowHouseID = row.HouseID;
            if (row) {
                if (row.IsMakeSure != 2) {
                    $('#save').hide();
                } else {
                    $('#save').show();
                }
                $('#dlgOrder').dialog('open').dialog('setTitle', '查看采购订单：' + row.OrderNo);
                $('#fmDep').form('clear');
                $('#HiddenAcceptPeople').val(row.AcceptPeople);
                $('#HiddenClientNum').val(row.ClientNum);
                row.CreateDate = AllDateTime(row.CreateDate);
                $('#fmDep').form('load', row);
                if (row.IsPrintPrice == "1") { $('#IsPrintPrice').prop('checked', true); } else { $('#IsPrintPrice').prop('checked', false); }
                showGrid();

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
        //显示列表
        function showGrid() {
            $('#dgSave').datagrid({
                width: '1060px',
                height: '338px',
                title: '出库产品', //标题内容
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
                        title: '批次', field: 'Batch', width: '50px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '批次', field: 'Batch', width: '50px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '采购价', field: 'UnitPrice', width: '50px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '销售价', field: 'ConfirmSalePrice', width: '60px', editor: { type: 'numberbox', options: { precision: 2, required: true } }
                    },
                    {
                        title: '采购商', field: 'PurchaserName', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '联系人', field: 'PurchaserBoss', width: '90px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '联系电话', field: 'PurchaserCellphone', width: '90px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '联系地址', field: 'PurchaserAddress', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    }
                ]],
                onClickRow: function (index, data) {
                    if ($("#IsMakeSure").val() == "1") { return; }
                    if (OldeditIndex != index) {
                        NeweditIndex = index;
                        if (endEditing()) {
                            $('#dgSave').datagrid('selectRow', index).datagrid('beginEdit', index);
                            var row = $("#dgSave").datagrid('getData').rows[index];
                        } else {
                            if ($('#dgSave').datagrid('validateRow', OldeditIndex)) {
                                var editors = $('#dgSave').datagrid('getEditors', OldeditIndex);
                                if (editors.length > 0) {
                                    $('#dgSave').datagrid('endEdit', OldeditIndex);
                                }
                            }
                            $('#dgSave').datagrid('cancelEdit', OldeditIndex);
                        }
                        OldeditIndex = index;
                    }
                },
                onClickCell: function (Index, field, value) {
                    if ($("#IsMakeSure").val() == "1") { return; }
                    $('#dgSave').datagrid('selectRow', Index);
                    $('#dgSave').datagrid('beginEdit', Index);
                }
            });
        }
        var IsModifyOrder = false;
        NeweditIndex = undefined;
        OldeditIndex = undefined;
        function endEditing() {
            if (OldeditIndex != undefined) {
                if ($('#dgSave').datagrid('validateRow', OldeditIndex)) {
                    var editors = $('#dgSave').datagrid('getEditors', OldeditIndex);
                    $('#dgSave').datagrid('endEdit', OldeditIndex);
                } else {
                    $('#dgSave').datagrid('cancelEdit', OldeditIndex);
                }
                return true;
            } else {
                return false;
            }
        }
        //业务员选择方法
        function onSaleManIDChanged(item) {
            if (item) {
                $('#SaleManName').val(item.UserName);
                $('#SaleCellPhone').val(item.CellPhone);
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
            var time = 1000;
            var x = 2;  //秒为单位，只接受整数
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
        //删除订单信息
        function DelItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            for (var i = 0; i < rows.length; i++) {
                if (rows[i].IsMakeSure != 0) {
                    if ("<%=UserInfor.LoginName%>" != "1000" && "<%=UserInfor.LoginName%>" != "2076") {
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', rows[i].OrderNo + '已报价无法删除！', 'warning'); return;
                        }
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
        function OpenConfirm() {
            var row = $('#dgSave').datagrid('getRows');
            if (row.length <= 0) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单列表中没有数据', 'warning'); return; }
            $('#dgSave').datagrid('endEdit', OldeditIndex);
            for (var i = 0; i < row.length; i++) {
                if (row[i].ConfirmSalePrice == "0.00" || row[i].ConfirmSalePrice == "0") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请输入产品销售价！', 'warning'); return;
                }
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定操作？', function (r) {
                if (r) {
                    if ($('#dgSave').datagrid('validateRow', OldeditIndex)) {
                        var editors = $('#dgSave').datagrid('getEditors', OldeditIndex);
                        if (editors.length > 0) {
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
        function OpenCancel() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要取消的采购单数据！', 'warning');
                return;
            }
            for (var i = 0; i < rows.length; i++) {
                if (rows[i].IsMakeSure == 1) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', "采购订单" + rows[i].OrderNo + '已取消！', 'warning'); return;
                }
            }
            if (rows) {
                $('#Cancel').dialog('open').dialog('setTitle',  '取消采购单');
                $('#fmCancel').form('clear');
            }
        }
        function ConfirmCancel() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要取消的采购订单！', 'warning');
                return;
            }

            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定取消？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在提交中...' });
                    $('#ConfirmCancel').linkbutton('disable');
                    var json = JSON.stringify(rows);

                    $('#fmCancel').form('submit', {
                        url: 'orderApi.aspx?method=OesPreOrderCancel',
                        contentType: "application/json;charset=utf-8", dataType: "json",
                        onSubmit: function (param) {
                            param.submitData = json;
                            var trd = $(this).form('enableValidation').form('validate');
                            if (trd) { $('#ConfirmCancel').linkbutton('disable'); } else {
                                $.messager.progress("close");
                                $('#ConfirmCancel').linkbutton('enable');
                            }
                            return trd;
                        },
                        success: function (msg) {
                            $.messager.progress("close");
                            $('#btnSConfirmCancelave').linkbutton('enable');
                            var result = eval('(' + msg + ')');
                            if (result.Result) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功！', 'info');
                                location.reload();
                            } else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                            }
                        },
                        error: function (data, status, e) {
                            alert("error");
                        }
                    })
                }
            });

        }
    </script>
</asp:Content>
