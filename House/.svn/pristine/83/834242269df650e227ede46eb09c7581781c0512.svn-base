<%@ Page Title="托运合同录入" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddAwbInfo.aspx.cs" Inherits="Cargo.Arrive.AddAwbInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--<script src="../JS/Date/CheckActivX.js" type="text/javascript"></script>--%>
    <script src="../JS/Lodop/LodopFuncs.js" type="text/javascript"></script>

    <script type="text/javascript">
        //页面加载时执行
        window.onload = function () {
            adjustment();
        }
        $(window).resize(function () {
            adjustment();
        });
        function adjustment() {
            var height = Number($(window).height()) - $("div[name='saPanel1']").outerHeight(true) - $("div[name='saPanel2']").outerHeight(true) - $("div[name='saPanel3']").outerHeight(true);
            $('#dgAdd').datagrid({ height: height });
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
            $('#dgAdd').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'GoodsID',
                url: null,
                columns: [[
                  { title: '品名', field: 'Goods', width: '15%', editor: { type: 'textbox', options: { required: true } } },
                  {
                      title: '包装', field: 'Package', width: '10%',
                      editor: {
                          type: 'combobox',
                          options: { panelHeight: 'auto', valueField: 'id', textField: 'value', data: [{ id: '无', value: '无' }, { id: '纸箱', value: '纸箱' }, { id: '纤袋', value: '纤袋' }, { id: '铁笼', value: '铁笼' }, { id: '木架', value: '木架' }, { id: '托', value: '托' }, { id: '泡沫箱', value: '泡沫箱' }, { id: '桶', value: '桶' }], required: true, editable: false }
                      }
                  },
                  { title: '件数', field: 'Piece', width: '10%', editor: { type: 'numberbox', options: { min: 0, precision: 0, required: true } } },
                  { title: '单价(元/件)', field: 'PiecePrice', width: '10%', editor: { type: 'numberbox', options: { precision: 2 } } },
                  { title: '重量(吨)', field: 'Weight', width: '10%', editor: { type: 'numberbox', options: { precision: 3 } } },
                  { title: '单价(元/吨)', field: 'WeightPrice', width: '10%', editor: { type: 'numberbox', options: { precision: 3 } } },
                  { title: '体积', field: 'Volume', width: '10%', editor: { type: 'numberbox', options: { precision: 3, required: true } } },
                  { title: '单价(元/立方)', field: 'VolumePrice', width: '10%', editor: { type: 'numberbox', options: { precision: 3, required: true } } },
                  {
                      field: 'opt', title: '操作', width: '10%', align: 'left',
                      formatter: function (value, rec, index) { var btn = '<a class="delcls" onclick="delItemByID(\'' + index + '\')" href="javascript:void(0)">删除</a>'; return btn; }
                  }
                ]],
                onLoadSuccess: function (data) { $('.delcls').linkbutton({ text: '删除', plain: true, iconCls: 'icon-cut' }); }
            });
            $('#CreateAwb').textbox('setValue', '<%= UserInfor.UserName%>');
            var d = new Date();
            $('#CreateDate').datetimebox('setValue', AllDateTime(d));
            $('#AHandleTime').datebox('setValue', AllDateTime(d));
            $('#APiece').numberbox('setValue', '0');
            $('#Weight').numberbox('setValue', '0');
            $('#Volume').numberbox('setValue', '0');
            $('input:checkbox[name="printAWB"][value="0"]').prop('checked', true); //打印运单
            $('input:checkbox[name="printAWB"][value="1"]').prop('checked', true); //打印运费
            bindMethod();
            newRow();
            $("input:radio[name=CheckOutType]").attr("checked", false)
            $("input:radio[name=DeliveryType]").attr("checked", false)
            $('#CheckOutType1').hide();
            $('#CheckOutType2').hide();
            $('#CheckOutType0').show();
            $('#CheckOutType3').show();
            $('#TimeLimit').combobox('setValue', '');
            $('#ADep').combobox('textbox').bind('focus', function () { $('#ADep').combobox('showPanel'); });
            $('#ADest').combobox('textbox').bind('focus', function () { $('#ADest').combobox('showPanel'); });
            $('#AShipperUnit').combobox('textbox').bind('focus', function () { $('#AShipperUnit').combobox('showPanel'); });
            $('#AAcceptUnit').combobox('textbox').bind('focus', function () { $('#AAcceptUnit').combobox('showPanel'); });
            $('#AAcceptPeople').combobox('textbox').bind('focus', function () { $('#AAcceptPeople').combobox('showPanel'); });
            $('#AShipperUnit').combobox({
                valueField: 'ClientNum',
                textField: 'ClientName',
                delay: '10',
                url: '../Client/ClientApi.aspx?method=AutoCompleteClient',
                onSelect: shipperChange,
                required: true
            });
            $('#ClerkNoTip').textbox({
                onChange: function (newValue, oldValue) {
                    $.ajax({
                        async: false, type: "post",
                        url: "../SystemManager/systemApi.aspx?method=GetUserNameByClerkNo&cnum=" + newValue,
                        success: function (text) { $('#ClerkNo').textbox('setValue', text) }
                    });
                }
            });


            $('#AShipperUnit').combobox('textbox').bind('focus', function () { $('#AShipperUnit').combobox('showPanel'); });
            $('#TimeLimit').combobox('textbox').bind('focus', function () { $('#TimeLimit').combobox('showPanel'); });
            $('#ADep').combobox('setValues', '<%=UserInfor.DepCity%>');
            $('#ADep').combobox('disable');
            $('#CreateAwb').numberbox('textbox').css('background-color', '#e8e8e8');
            $('#APiece').numberbox('textbox').css('background-color', '#e8e8e8');
            $('#Weight').numberbox('textbox').css('background-color', '#e8e8e8');
            $('#Volume').numberbox('textbox').css('background-color', '#e8e8e8');
            $('#TransportFee').numberbox('textbox').css('background-color', '#e8e8e8');
            $('#TotalCharge').numberbox('textbox').css('background-color', '#e8e8e8');
            $('#CreateAwb').combobox('disable');
            $('#APiece').combobox('disable');
            $('#Weight').combobox('disable');
            $('#Volume').combobox('disable');
            $('#TransportFee').combobox('disable');
            $('#TotalCharge').combobox('disable');
            $('input:radio[name="DeliveryType"][value="0"]').prop('checked', true);
            $('input:radio[name="CheckOutType"][value="0"]').prop('checked', true);
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--此div用于在界面未完全加载样式前显示内容--%>
    <div id='Loading' style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 99%; height: 100%; background: white; text-align: center; padding: 5px 10px; display: table;">
        <div style="display: table-cell; vertical-align: middle">
            <h1><font size="9">页面加载中……</font></h1>
        </div>
    </div>
    <%--此div用于在界面未完全加载样式前显示内容--%>
    <form id="fm" class="easyui-form" method="post">
    <div id="saPanel" name="saPanel1">
        <table style="width: 100%">
            <tr>
                <td style="color: Red; font-weight: bolder; text-align: right;">
                    运单号:
                </td>
                <td>
                    <input name="AwbNo" id="AAwbNo" class="easyui-textbox" style="width: 100px" data-options="required:true,validType:'length[7,8]'">
                </td>
                <td style="color: Red; font-weight: bolder; text-align: right;">
                    起运站:
                </td>
                <td>
                    <input name="Dep" id="ADep" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../Arrive/ArriveApi.aspx?method=QueryAllCity',required:true,editable:false " />
                </td>
                <td style="color: Red; font-weight: bolder; text-align: right;">
                    到达站:
                </td>
                <td>
                    <input name="Dest" id="ADest" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../Arrive/ArriveApi.aspx?method=QueryAllCity',required:true,editable:false " />
                </td>
                <td style="text-align: right;">
                    中转站:
                </td>
                <td>
                    <input name="Transit" id="ATransit" class="easyui-textbox" style="width: 100px;" />
                </td>
                <td style="color: Red; font-weight: bolder; text-align: right;">
                    受理日期:
                </td>
                <td>
                    <input name="HandleTime" id="AHandleTime" class="easyui-datebox" style="width: 100px"
                        data-options="required:true">
                </td>
            </tr>
            <tr>
                <td style="color: Red; font-weight: bolder; text-align: right;">
                    托运单位:
                </td>
                <td>
                    <input name="ShipperUnit" id="AShipperUnit" class="easyui-combobox" style="width: 100px;" />
                </td>
                <td style="text-align: right;">
                    托运人:
                </td>
                <td>
                    <input name="ShipperName" id="AShipperName" class="easyui-textbox" style="width: 100px;"
                        data-options="required:true" />
                </td>
                <td style="text-align: right;">
                    地址:
                </td>
                <td>
                    <input name="ShipperAddress" id="AShipperAddress" style="width: 150px;" class="easyui-textbox" />
                </td>
                <td style="text-align: right;">
                    电话:
                </td>
                <td>
                    <input name="ShipperTelephone" id="AShipperTelephone" class="easyui-textbox" style="width: 100px;" />
                </td>
                <td style="text-align: right;">
                    手机:
                </td>
                <td>
                    <input name="ShipperCellphone" id="AShipperCellphone" class="easyui-textbox" data-options="required:true" style="width: 100px;" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    收货单位:
                </td>
                <td>
                    <input name="AcceptUnit" id="AAcceptUnit" class="easyui-combobox" data-options="valueField:'AcceptCompany',textField:'AcceptCompany',onSelect:onAcceptAddressChanged"
                        style="width: 100px;" />
                </td>
                <td style="text-align: right;">
                    收货人:
                </td>
                <td>
                    <input name="AcceptPeople" id="AAcceptPeople" class="easyui-combobox" data-options="valueField:'AcceptPeople',textField:'AcceptPeople',onSelect:onAcceptPeopleChanged,required:true"
                        style="width: 100px;" />
                </td>
                <td style="text-align: right;">
                    地址:
                </td>
                <td>
                    <input name="AcceptAddress" id="AAcceptAddress" style="width: 150px;" class="easyui-textbox" />
                </td>
                <td style="text-align: right;">
                    电话:
                </td>
                <td>
                    <input name="AcceptTelephone" id="AAcceptTelephone" class="easyui-textbox" style="width: 100px;" />
                </td>
                <td style="text-align: right;">
                    手机:
                </td>
                <td>
                    <input name="AcceptCellphone" id="AAcceptCellphone" class="easyui-textbox" data-options="required:true" style="width: 100px;" />
                </td>
            </tr>
            <tr>
                <td style="width: 100%;" colspan="10">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="newRow()">新增货物品名</a> &nbsp;&nbsp;
                    客户编号：<input name="ClientNum" id="AClientNum" class="easyui-validatebox" data-options="validType:'ClientNum'" style="width: 100px">&nbsp;&nbsp;
                    入库员：<input name="ClerkNo" id="ClerkNoTip" style="width: 50px;" class="easyui-textbox" />&nbsp;
                    <input name="ClerkName" id="ClerkNo" readonly="true" style="width: 70px;" class="easyui-textbox" />
                </td>
            </tr>
        </table>
    </div>
    <table id="dgAdd" class="easyui-datagrid">
    </table>
    <div id="saPanel" name="saPanel2">
        <table style="width: 100%">
            <tr>
                <td style="text-align: right;">
                    件数:
                </td>
                <td>
                    <input name="Piece" id="APiece" class="easyui-numberbox" data-options="min:0,precision:0,required:true" style="width: 100px;" />
                </td>
                <td style="text-align: right;">
                    重量:
                </td>
                <td>
                    <input id="Weight" name="Weight" class="easyui-numberbox" data-options="min:0,precision:3,required:true" style="width: 100px;" />吨
                </td>
                <td style="text-align: right;">
                    体积:
                </td>
                <td>
                    <input name="Volume" id="Volume" class="easyui-numberbox" data-options="min:0,precision:3,required:true" style="width: 100px;" />方
                </td>
                <td style="text-align: right;">
                    其它费用:
                </td>
                <td>
                    <input name="TransitFee" id="TransitFee" class="easyui-numberbox" data-options="min:0,precision:2" style="width: 100px;" />
                </td>
                <td style="text-align: right;">
                    运输费用:
                </td>
                <td>
                    <input name="TransportFee" id="TransportFee" data-options="min:0,precision:2" class="easyui-numberbox" style="width: 100px;" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    送货费:
                </td>
                <td>
                    <input name="DeliverFee" id="DeliverFee" data-options="min:0,precision:2" class="easyui-numberbox" style="width: 100px;" />
                </td>
                <td style="text-align: right;">
                    提货费:
                </td>
                <td>
                    <input name="OtherFee" id="OtherFee" data-options="min:0,precision:2" class="easyui-numberbox" style="width: 100px;" />
                </td>
                <td style="text-align: right;">
                    保费:
                </td>
                <td>
                    <input name="InsuranceFee" id="InsuranceFee" data-options="min:0,precision:2" class="easyui-numberbox" style="width: 100px;" />
                </td>
                <td style="text-align: right;">
                    合计:
                </td>
                <td>
                    <input name="TotalCharge" id="TotalCharge" class="easyui-numberbox" data-options="min:0,precision:2" style="width: 100px;" />
                </td>
                <td style="text-align: right;">
                    回扣:
                </td>
                <td>
                    <input name="Rebate" id="Rebate" data-options="min:0,precision:2" class="easyui-numberbox" style="width: 100px;" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    代收款:
                </td>
                <td>
                    <input name="CollectMoney" id="CollectMoney" class="easyui-numberbox" data-options="min:0,precision:2" style="width: 100px;" />元
                </td>
                <td style="text-align: right;">
                    回单要求:
                </td>
                <td>
                    <input name="ReturnAwb" id="ReturnAwb" class="easyui-numberbox" data-options="min:0,precision:0" style="width: 100px;" />份
                </td>
                <td style="text-align: right;">
                    开单员
                </td>
                <td>
                    <input name="CreateAwb" id="CreateAwb" class="easyui-textbox" readonly="true" style="width: 100px;" />
                </td>
                <td style="text-align: right;">
                    开单时间
                </td>
                <td colspan="5">
                    <input name="CreateDate" id="CreateDate" class="easyui-datetimebox" data-options="formatter:AllDateTime"
                        readonly="true" style="width: 150px;" />
                </td>
            </tr>
            <tr>
                <td style="color: Red; font-weight: bolder; text-align: right;">
                    结款方式:
                </td>
                <td colspan="3">
                    <div id="CheckOutType0" style="display: inline">
                        <input name="CheckOutType" id="cot0" type="radio" value="0" /><label for="cot0" style="font-size: 15px;">现付</label></div>
                    <div id="CheckOutType3" style="display: inline">
                        <input name="CheckOutType" id="cot3" type="radio" value="3" /><label for="cot3" style="font-size: 15px;">到付</label></div>
                    <div id="CheckOutType1" style="display: inline">
                        <input name="CheckOutType" id="cot1" type="radio" value="1" /><label for="cot1" style="font-size: 15px;">回单</label></div>
                    <div id="CheckOutType2" style="display: inline">
                        <input name="CheckOutType" id="cot2" type="radio" value="2" /><label for="cot2" style="font-size: 15px;">月结</label></div>
                </td>
                <td style="color: Red; font-weight: bolder; text-align: right;">
                    运输时效
                </td>
                <td colspan="3">
                    <select name="TimeLimit" id="TimeLimit" panelheight="auto" style="width: 100px;" class="easyui-combobox"
                        data-options="required:true">
                        <option value="1">1天</option>
                        <option value="2">2天</option>
                        <option value="3">3天</option>
                        <option value="4">4天</option>
                        <option value="5">5天</option>
                        <option value="6">6天</option>
                        <option value="7">7天</option>
                        <option value="8">8天</option>
                        <option value="9">其它</option>
                    </select>
                </td>
                <td style="color: Red; font-weight: bolder; text-align: right;">
                    送货方式:
                </td>
                <td>
                    <input name="DeliveryType" type="radio" id="dlt1" value="1" /><label for="dlt1" style="font-size: 15px;">自提</label>
                    <input name="DeliveryType" type="radio" id="dlt0" value="0" /><label for="dlt0" style="font-size: 15px;">送货</label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    副单号:
                </td>
                <td colspan="4">
                    <textarea name="HAwbNo" id="AHAwbNo" rows="3" style="width: 300px;"></textarea>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-application_form_magnify" plain="false" onclick="checkHLY()" style="margin-top: -50px;">选择好来运单</a>
                </td>
                <td style="text-align: right;">
                    重要提示:
                </td>
                <td colspan="4">
                    <textarea name="Remark" id="ARemark" rows="3" style="width: 300px;"></textarea>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <div id="saPanel" name="saPanel3">
        <table>
            <tr>
                <td style="text-align: left; width: 40%">
                    <input type="checkbox" id="printAWB" name="printAWB" value="0"><label for="printAWB">打印运单</label>
                    <input type="checkbox" id="printCharge" name="printAWB" value="1"><label for="printCharge">打印运费</label>
                    <input type="checkbox" id="printBAR" name="printAWB" value="2"><label for="printBAR">打印标签</label>
                </td>
                <td style="text-align: right; width: 50%">
                    <input type="checkbox" id="hlyAwb" name="hlyAwb" value="0"><label for="hlyAwb">好来运单</label>&nbsp;&nbsp;&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" id="save" iconcls="icon-ok" onclick="save()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" id="undo" iconcls="icon-clear" onclick="reset()">&nbsp;重&nbsp;置&nbsp;</a>
                    <%--&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" id="print"
                                    iconcls="icon-printer" onclick="prn1_preview()">&nbsp;打&nbsp;印&nbsp;</a>&nbsp;&nbsp;
                            <a href="#" class="easyui-linkbutton" id="preprint" iconcls="icon-print" onclick="PrePrint()">
                                &nbsp;打印预览&nbsp;</a>&nbsp;&nbsp; <a href="#" class="easyui-linkbutton" iconcls="icon-cancel"
                                    onclick="javascript:$('#dlgAwbDetail').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>--%>
                </td>
            </tr>
        </table>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 800px; height: 500px;" closed="true"
        closable="false" buttons="#dlg-buttons">
        <table id="dgManifest" class="easyui-datagrid">
        </table>
        <div id="toolbar">
            五联单号:
            <input id="QAwbNo" class="easyui-textbox" data-options="prompt:'请输入好来运五联单号'" style="width: 200px">
            时间范围:
            <input id="StartDate" class="easyui-datebox" style="width: 100px">~
            <input id="EndDate" class="easyui-datebox" style="width: 100px">
            <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="awbQuery()">查询</a>
        </div>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="SaveHlyAwb()" id="A1">&nbsp;确&nbsp;定&nbsp;</a>&nbsp;&nbsp; 
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closeDlg()">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <object id="LODOP_OB" title="dd" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" width="0px" height="0px">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0px" height="0px"></embed>
    </object>
    <object id="PSKPrn" classid="clsid:81C07687-3353-4ABA-B108-94BCE81E5CBA" codebase="/PSKPrn.ocx#version=2,0,0,1" width="0" height="0"></object>

    <script type="text/javascript">
        function awbQuery() {
            var mOpts = $('#dgManifest').datagrid('options');
            mOpts.url = '../Arrive/ArriveApi.aspx?method=QueryHlyAwbInfo';
            $('#dgManifest').datagrid('load', {
                HlyFiveNo: $('#QAwbNo').val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
            });
        }
        //保存勾选的好来运单数据
        function SaveHlyAwb() {
            var rows = $('#dgManifest').datagrid('getSelections');
            if (rows == null || rows == "") { closeDlg(); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确  定？', function(r) {
                if (r) {
                    var hawb = '';
                    for (var i = 0; i < rows.length; i++) {
                        hawb += rows[i].HlyFiveNo + ",";
                    }
                    hawb = hawb.substring(0, hawb.length - 1);
                    $('#AHAwbNo').val(hawb);
                }
            });
            closeDlg();
        }
        //弹出好来运运单选择框
        function checkHLY() {
            $('#dlg').dialog('open').dialog('setTitle', '选择好来运单');
            showGrid();
            var gridOpts = $('#dgManifest').datagrid('options');
            gridOpts.url = '../Arrive/ArriveApi.aspx?method=QueryHlyAwbInfo';
        }
        //显示列表
        function showGrid() {
            $('#dgManifest').datagrid({
                width: '100%',
                height: '420px',
                title: '', //标题内容
                rownumbers: true,
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                  { title: '', field: 'ID', checkbox: true, width: '30px' },
                  { title: '清单编号', field: 'HlyAwbNo', width: '60px' },
                  { title: '五联单号', field: 'HlyFiveNo', width: '100px' },
                  { title: '件数', field: 'Piece', width: '40px' },
                  { title: '品名', field: 'Goods', width: '60px' },
                  { title: '出发站', field: 'Dep', width: '40px' },
                  { title: '到达站', field: 'Dest', width: '40px' },
                  { title: '发货人姓名', field: 'ShipperName', width: '80px' },
                  { title: '发货人电话', field: 'ShipperCellphone', width: '80px' },
                  { title: '收货人姓名', field: 'AcceptPeople', width: '80px' },
                  { title: '收货人电话', field: 'AcceptCellphone', width: '80px' },
                  { title: '收货地址', field: 'AcceptAddress', width: '100px' },
                  { title: '子单号', field: 'TagCodeNo', width: '100px' },
                  { title: '是否保存', field: 'SaveStatus', width: '60px', formatter: function(val, row, index) { if (val == "0") { return "未保存"; } else if (val == "1") { return "已保存"; } else { return ""; } } },
                  { title: '上传时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter }
                ]],
                rowStyler: function(index, row) {
                    if (row.SaveStatus == "1") {
                        return "background:#E5E5E5";
                    }
                },
                onLoadSuccess: function(data) {
                    $('#dgManifest').datagrid('clearSelections'); //一定要加上这一句，要不然datagrid会记住之前的选择状态，删除时会出问题  
                    //遍历结果
                    for (var i = 0; i < data.rows.length; i++) {
                        //根据status值使复选框 不可用
                        if (data.rows[i].SaveStatus == "1") {
                            $("input[type='checkbox']")[i + 1].disabled = true;
                        }
                    }
                },
                onClickRow: function(rowIndex, rowData) {
                    //根据status值 单击单选行不可用
                    if (rowData.SaveStatus == "1") {
                        $('#dgManifest').datagrid('unselectRow', rowIndex);
                    }
                },
                onDblClickRow: function(rowIndex, rowData) {
                    //根据status值 双击单选行不可用
                    if (rowData.SaveStatus == "1") {
                        $('#dgManifest').datagrid('unselectRow', rowIndex);
                    }
                },
                onSelectAll: function(rows) {
                    //根据status值  全选时某些行不选中
                    $.each(rows, function(i, n) {
                        if (n.SaveStatus == "1") {
                            $('#dgManifest').datagrid('unselectRow', i);
                        }
                    });
                }

            });
        }
        function closeDlg() {
            $('#dlg').dialog('close');
        }
        function reset() {
            $('#fm').form('clear');
            $('#AAwbNo').textbox('setValue', '100');
            $('#dgAdd').datagrid('loadData', { total: 0, rows: [] });
            $('#CreateAwb').textbox('setValue', '<%= UserInfor.LoginName%>');
            var d = new Date();
            $('#CreateDate').datetimebox('setValue', AllDateTime(d));
            $('#AHandleTime').datebox('setValue', AllDateTime(d));

            $('#APiece').numberbox('setValue', '0');
            $('#Weight').numberbox('setValue', '0');
            $('#Volume').numberbox('setValue', '0');
            $('#DeliverFee').numberbox('setValue', '');
            $('#TransportFee').numberbox('setValue', '');
            $('input:checkbox[name="printAWB"][value="0"]').prop('checked', true); //打印运单
            $('input:checkbox[name="printAWB"][value="1"]').prop('checked', true); //打印运费
        }
        function goSet() {
            var an = $('#AAwbNo').textbox('getValue');
            var jg=Number(an) + 1;
            var awb;
           if(jg<10){awb="000000"+jg.toString();}
           if(jg<100&&jg>=10){awb="00000"+jg.toString();}
            if(jg<1000&&jg>=100){awb="0000"+jg.toString();}
            if(jg<10000&&jg>=1000){awb="000"+jg.toString();}
            if(jg<100000&&jg>=10000){awb="00"+jg.toString();}
            if(jg<1000000&&jg>=100000){awb="0"+jg.toString();}
            if(jg<10000000&&jg>=1000000){awb=jg.toString();}
            if(jg<100000000&&jg>=10000000){awb=jg.toString();}
            $('#AAwbNo').textbox('setValue', awb);
            $('#dgAdd').datagrid('loadData', { total: 0, rows: [] });
            $('#CreateAwb').textbox('setValue', '<%= UserInfor.LoginName%>');
            var d = new Date();
            $('#CreateDate').datetimebox('setValue', AllDateTime(d));
            $('#AHandleTime').datebox('setValue', AllDateTime(d));
            $('#AShipperUnit').combobox('setValue', '');
            $('#AShipperName').textbox('setValue', '');
            $('#AShipperAddress').textbox('setValue', '');
            $('#AShipperTelephone').textbox('setValue', '');
            $('#AShipperCellphone').textbox('setValue', '');

            $('#AAcceptUnit').combobox('setValue', '');
            $('#AAcceptPeople').combobox('setValue', '');
            $('#AAcceptAddress').textbox('setValue', '');
            $('#AAcceptTelephone').textbox('setValue', '');
            $('#AAcceptCellphone').textbox('setValue', '');
            $('#AClientNum').val('');

            $('#ADep').combobox('setValue', '');
            $('#ADest').combobox('setValue', '');
            $('#ATransit').textbox('setValue', '');
            $('#APiece').numberbox('setValue', '0');
            $('#Weight').numberbox('setValue', '0');
            $('#Volume').numberbox('setValue', '0');
            $('#DeliverFee').numberbox('setValue', '');
            $('#TransitFee').numberbox('setValue', '');
            $('#TransportFee').numberbox('setValue', '');
            $('#OtherFee').numberbox('setValue', '');
            $('#InsuranceFee').numberbox('setValue', '');
            $('#TotalCharge').numberbox('setValue', '');
            $('#Rebate').numberbox('setValue', '');
            $('#CollectMoney').numberbox('setValue', '');
            $('#ReturnAwb').numberbox('setValue', '');
            $('#AHAwbNo').val('');
            $('#ARemark').val('');
            $('input:checkbox[name="printAWB"][value="0"]').prop('checked', true); //打印运单
            $('input:checkbox[name="printAWB"][value="1"]').prop('checked', true); //打印运费
            $("input:radio[name=CheckOutType]").attr("checked", false)
            $("input:radio[name=DeliveryType]").attr("checked", false)
            $('#TimeLimit').combobox('setValue', '');
            $('#CheckOutType1').hide();
            $('#CheckOutType2').hide();
            $('#CheckOutType0').show();
            $('#CheckOutType3').show();
            newRow();
        }
        $.extend($.fn.validatebox.defaults.rules, {
            ClientNum: {
                validator: function(value) {
                    var dt = false;
                    $.ajax({
                        async: false,
                        url: "../Arrive/ArriveApi.aspx?method=IsExistClientNum&cnum=" + value,
                        type: "post",
                        success: function(text) {
                            if (text == "0") { $.fn.validatebox.defaults.rules.ClientNum.message = '输入的客户编号不存在'; dt = false; } else { dt = true; }
                        }
                    });
                    if (dt) {
                        $('#CheckOutType1').show();
                        $('#CheckOutType2').show();
                        $('#CheckOutType0').show();
                        $('#CheckOutType3').show();
                    }
                    return dt;
                }, message: '输入的客户编号不存在'
            }
        });
        //新增品名数据
        function newRow() {
            var row = { Piece: '0', PiecePrice: '0', Weight: '0', WeightPrice: '0', Volume: '0', VolumePrice: '0' };
            $('#dgAdd').datagrid('appendRow', row);
            var lastIndex = $('#dgAdd').datagrid('getRows').length - 1;
            $('#dgAdd').datagrid('beginEdit', lastIndex);
            var editors = $('#dgAdd').datagrid('getEditors', lastIndex);
            var pg = editors[1];
            $(pg.target).combobox('textbox').bind('focus', function() { $(pg.target).combobox('showPanel'); });
            var pe = editors[2];
            var pep = editors[3];
            var we = editors[4];
            var wew = editors[5];
            var ve = editors[6];
            var vev = editors[7];
            $(pe.target).numberbox({ onChange: function(newValue, oldValue) {
                var rows = $('#dgAdd').datagrid('getRows');
                var p = 0;
                var pt = 0, wt = 0, vt = 0, tt = 0, mt = 0;
                for (var j = 0; j < rows.length; j++) {
                    var edj = $('#dgAdd').datagrid('getEditors', j);
                    p += Number(edj[2].target.val());
                    pt = Number(edj[2].target.val()) * Number(edj[3].target.val());
                    wt = Number(edj[4].target.val()) * Number(edj[5].target.val());
                    vt = Number(edj[6].target.val()) * Number(edj[7].target.val());
                    if (pt > wt) { mt = pt; } else { mt = wt; } if (vt > mt) { mt = vt; } tt += mt;
                }
                $('#APiece').numberbox('setValue', p);
                $('#TransportFee').numberbox('setValue', tt);
            }
            }); //件数
            $(pep.target).numberbox({ onChange: function(newValue, oldValue) {
                var rows = $('#dgAdd').datagrid('getRows');
                var pt = 0, wt = 0, vt = 0, tt = 0, mt = 0;
                for (var j = 0; j < rows.length; j++) {
                    var edj = $('#dgAdd').datagrid('getEditors', j);
                    pt = Number(edj[2].target.val()) * Number(edj[3].target.val());
                    wt = Number(edj[4].target.val()) * Number(edj[5].target.val());
                    vt = Number(edj[6].target.val()) * Number(edj[7].target.val());
                    if (pt > wt) { mt = pt; } else { mt = wt; } if (vt > mt) { mt = vt; } tt += mt;
                }
                $('#TransportFee').numberbox('setValue', tt);
            }
            }); //件数单价
            $(we.target).numberbox({ onChange: function(newValue, oldValue) {
                var rows = $('#dgAdd').datagrid('getRows');
                var p = 0;
                var pt = 0, wt = 0, vt = 0, tt = 0, mt = 0;
                for (var j = 0; j < rows.length; j++) {
                    var edj = $('#dgAdd').datagrid('getEditors', j);
                    p += Number(edj[4].target.val());
                    pt = Number(edj[2].target.val()) * Number(edj[3].target.val());
                    wt = Number(edj[4].target.val()) * Number(edj[5].target.val());
                    vt = Number(edj[6].target.val()) * Number(edj[7].target.val());
                    if (pt > wt) { mt = pt; } else { mt = wt; } if (vt > mt) { mt = vt; } tt += mt;
                }
                $('#Weight').numberbox('setValue', p);
                $('#TransportFee').numberbox('setValue', tt);
            }
            }); //重量
            $(wew.target).numberbox({ onChange: function(newValue, oldValue) {
                var rows = $('#dgAdd').datagrid('getRows');
                var pt = 0, wt = 0, vt = 0, tt = 0, mt = 0;
                for (var j = 0; j < rows.length; j++) {
                    var edj = $('#dgAdd').datagrid('getEditors', j);
                    pt = Number(edj[2].target.val()) * Number(edj[3].target.val());
                    wt = Number(edj[4].target.val()) * Number(edj[5].target.val());
                    vt = Number(edj[6].target.val()) * Number(edj[7].target.val());
                    if (pt > wt) { mt = pt; } else { mt = wt; } if (vt > mt) { mt = vt; } tt += mt;
                }
                $('#TransportFee').numberbox('setValue', tt);
            }
            }); //重量单价
            $(ve.target).numberbox({ onChange: function(newValue, oldValue) {
                var rows = $('#dgAdd').datagrid('getRows');
                var p = 0;
                var pt = 0, wt = 0, vt = 0, tt = 0, mt = 0;
                for (var j = 0; j < rows.length; j++) {
                    var edj = $('#dgAdd').datagrid('getEditors', j);
                    p += Number(edj[6].target.val());
                    pt = Number(edj[2].target.val()) * Number(edj[3].target.val());
                    wt = Number(edj[4].target.val()) * Number(edj[5].target.val());
                    vt = Number(edj[6].target.val()) * Number(edj[7].target.val());
                    if (pt > wt) { mt = pt; } else { mt = wt; } if (vt > mt) { mt = vt; } tt += mt;
                }
                $('#Volume').numberbox('setValue', p);
                $('#TransportFee').numberbox('setValue', tt);
            }
            }); //体积
            $(vev.target).numberbox({ onChange: function(newValue, oldValue) {
                var rows = $('#dgAdd').datagrid('getRows');
                var pt = 0, wt = 0, vt = 0, tt = 0, mt = 0;
                for (var j = 0; j < rows.length; j++) {
                    var edj = $('#dgAdd').datagrid('getEditors', j);
                    pt = Number(edj[2].target.val()) * Number(edj[3].target.val());
                    wt = Number(edj[4].target.val()) * Number(edj[5].target.val());
                    vt = Number(edj[6].target.val()) * Number(edj[7].target.val());
                    if (pt > wt) { mt = pt; } else { mt = wt; } if (vt > mt) { mt = vt; } tt += mt;
                }
                $('#TransportFee').numberbox('setValue', tt);
            }
            }); //体积单价
        }
        //删除品名数据
        function delItemByID(Did) {
            var dr = $('#dgAdd').datagrid('getRows').length;
            if (dr == 1) {
                $('#dgAdd').datagrid('loadData', { total: 0, rows: [] });
                $('#APiece').numberbox('setValue', 0); $('#Weight').numberbox('setValue', 0); $('#Volume').numberbox('setValue', 0);
                $('#TransportFee').numberbox('setValue', 0);
            } else {
                $('#dgAdd').datagrid('deleteRow', Did);
                var rows = $('#dgAdd').datagrid('getRows');
                var pt = 0, wt = 0, vt = 0, p = 0, w = 0, v = 0, tt = 0, mt = 0;
                for (var j = 0; j < rows.length; j++) {
                    var m;
                    if (j == Did) { m = j + 1; } else { m = j; }
                    var edj = $('#dgAdd').datagrid('getEditors', m);
                    p += Number(edj[2].target.val()); w += Number(edj[4].target.val()); v += Number(edj[6].target.val());
                    pt = Number(edj[2].target.val()) * Number(edj[3].target.val());
                    wt = Number(edj[4].target.val()) * Number(edj[5].target.val());
                    vt = Number(edj[6].target.val()) * Number(edj[7].target.val());
                    if (pt > wt) { mt = pt; } else { mt = wt; } if (vt > mt) { mt = vt; } tt += mt;
                }
                $('#APiece').numberbox('setValue', p); $('#Weight').numberbox('setValue', w); $('#Volume').numberbox('setValue', v);
                $('#TransportFee').numberbox('setValue', tt);
            }
        }
        //绑定费用框
        function bindMethod() {
            $("#TransitFee").numberbox({ "onChange": function(newValue, oldValue) { qh(); } });
            $("#TransportFee").numberbox({ "onChange": function(newValue, oldValue) { qh(); } });
            $("#DeliverFee").numberbox({ "onChange": function(newValue, oldValue) { qh(); } });
            $("#OtherFee").numberbox({ "onChange": function(newValue, oldValue) { qh(); } });
            $("#InsuranceFee").numberbox({ "onChange": function(newValue, oldValue) { qh(); } });
        }
        function qh() {
            var t = Number($('#TransitFee').numberbox('getValue')) + Number($('#TransportFee').numberbox('getValue')) + Number($('#DeliverFee').numberbox('getValue')) + Number($('#OtherFee').numberbox('getValue')) + Number($('#InsuranceFee').numberbox('getValue'));
            $('#TotalCharge').numberbox('setValue', Number(t).toFixed(2));
        }
        //托运单位改变值方法
        function shipperChange(item) {
            if (item) { 
                $('#AShipperName').textbox('setValue', item.Boss);
                $('#AShipperTelephone').textbox('setValue', item.Telephone);
                $('#AShipperCellphone').textbox('setValue', item.Cellphone);
                //$('#AClientNum').val(item.ClientNum);
                
            if(item.ClientName.indexOf('好来运')>-1){
               $('#hlyAwb').prop('checked', true);
            }else{
               $('#hlyAwb').prop('checked', false);
            }
                if (item.ClientNum == "") {
                    $('#CheckOutType0').show();
                    $('#CheckOutType3').show();
                    $('#CheckOutType1').hide();
                    $('#CheckOutType2').hide();
                }
                else {
                    $('#CheckOutType0').show();
                    $('#CheckOutType3').show();
                    $('#CheckOutType1').show();
                    $('#CheckOutType2').show();
                }
                //var url = "../Arrive/ArriveApi.aspx?method=QueryClientAcceptAddress&id=" + item.ClientID;
                var url = "../Client/clientApi.aspx?method=AutoCompleteClientAcceptPeople&ClientNum=" + item.ClientNum;
                $('#AAcceptUnit').combobox('reload', url);
                $('#AAcceptPeople').combobox('reload', url);
                $('#AAcceptUnit').combobox('setValue', '');
                $('#AAcceptPeople').combobox('setValue', '');
            }
        }
        //收货单位自动选择方法
        function onAcceptAddressChanged(item) {
            if (item) {
                $('#AAcceptPeople').combobox('setValue', item.AcceptPeople);
                $('#AAcceptAddress').textbox('setValue', item.AcceptAddress);
                $('#AAcceptTelephone').textbox('setValue', item.AcceptTelephone);
                $('#AAcceptCellphone').textbox('setValue', item.AcceptCellphone);
            }
        }
        //收货人自动选择方法
        function onAcceptPeopleChanged(item) {
            if (item) {
                $('#AAcceptUnit').combobox('setValue', item.AcceptCompany);
                $('#AAcceptAddress').textbox('setValue', item.AcceptAddress);
                $('#AAcceptTelephone').textbox('setValue', item.AcceptTelephone);
                $('#AAcceptCellphone').textbox('setValue', item.AcceptCellphone);
            }
        }
        ///保存新增运单
        function save() {
            var rck = $("input:radio[name='CheckOutType']:checked").val();
            if (rck == '2' || rck == '4') {
                if ($('#AClientNum').val() == '') { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请输入客户编号!', 'info'); return; }
            }
            var pe = Number($('#APiece').textbox('getValue'));
            if (pe <= 0) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '件数不能为小于0!', 'info'); return; }
            var ve = Number($('#Volume').textbox('getValue'));
            if (ve <= 0) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '体积不能为小于0!', 'info'); return; }
            $('#dgAdd').datagrid('acceptChanges')
            var row = $('#dgAdd').datagrid('getRows');
            $('#ADep').combobox('enable');
            var json = JSON.stringify(row)
            $('#fm').form('submit', {
                url: '../Arrive/ArriveApi.aspx?method=SaveAwb',
                contentType: "application/json;charset=utf-8", dataType: "json",
                onSubmit: function(param) {
                    param.AAcceptUnit = $('#AAcceptUnit').combobox('getText');
                    param.AAcceptPeople = $('#AAcceptPeople').combobox('getText');
                    param.AShipperUnit = $('#AShipperUnit').combobox('getText');
                    var hlyAwb = $('#hlyAwb').is(':checked');
                    if (hlyAwb == false) {
                        param.HLY = "0";
                    } else {
                        param.HLY = "1";
                    }
                    param.submitData = json;
                    var trd = $(this).form('enableValidation').form('validate');
                    if (trd) { $('#save').linkbutton('disable'); }
                    return trd;
                },
                success: function(msg) {
                    $('#save').linkbutton('enable');
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '运单新增成功', 'warning');
                        prn1_preview(); //打印
                    } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '运单新增失败：' + result.Message, 'warning'); }
                }
            })
        }
        var LODOP;
        //打印预览
        function PrePrint() {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            if ($('#printAWB').is(':checked')) { CreateOneFormPage(); if (LODOP.SET_PRINTER_INDEX(-1)) { LODOP.PREVIEW(); } }
        };
        //打印
        function prn1_preview() {
            if ($('#printAWB').is(':checked')) {
                CreateOneFormPage(); if (LODOP.SET_PRINTER_INDEX(-1)) { LODOP.PRINT(); }
            }
            if ($('#printBAR').is(':checked')) { BarCodePreview(); }
            goSet();
        };
        //条码打印预览
        function BarCodePreview() {
            var tl;
            if ($('#tl9').is(':checked')) { tl = "" };
            if ($('#tl1').is(':checked')) { tl = getSetDayFormatDate($('#AHandleTime').datebox('getValue'), 1) };
            if ($('#tl2').is(':checked')) { tl = getSetDayFormatDate($('#AHandleTime').datebox('getValue'), 2) };
            if ($('#tl3').is(':checked')) { tl = getSetDayFormatDate($('#AHandleTime').datebox('getValue'), 3) };
            var pie = $('#APiece').numberbox('getValue');
            if (pie == "" || pie == "0") { return; }
            for (var i = 1; i <= pie; i++) {
                if (i == 20) { return; }
                OpenPrinter();
                PSKPrn.PTKDrawTextTrueTypeW(720, 280, 55, 0, "微软雅黑", 3, 400, false, false, false, "A2", "开单日期:");
                PSKPrn.PTKDrawTextTrueTypeW(470, 285, 60, 0, "微软雅黑", 3, 400, false, false, false, "A3", getNowFormatDate($('#AHandleTime').datebox('getValue')));
                PSKPrn.PTKDrawTextTrueTypeW(720, 190, 55, 0, "微软雅黑", 3, 400, false, false, false, "A8", "起  运  站:");
                PSKPrn.PTKDrawTextTrueTypeW(530, 155, 100, 0, "微软雅黑", 3, 900, false, false, false, "A9", $('#ADep').combobox('getValue'));
                PSKPrn.PTKDrawTextTrueTypeW(720, 100, 55, 0, "微软雅黑", 3, 400, false, false, false, "A4", "运  单  号:");
                PSKPrn.PTKDrawTextTrueTypeW(460, 80, 70, 0, "微软雅黑", 3, 400, false, false, false, "A5", $('#AAwbNo').textbox('getValue'));
                PSKPrn.PTKDrawTextTrueTypeW(720, 20, 55, 0, "微软雅黑", 3, 400, false, false, false, "A0", "收  件  人:");
                PSKPrn.PTKDrawTextTrueTypeW(400, 0, 70, 0, "微软雅黑", 3, 400, false, false, false, "A1", $('#AAcceptPeople').combobox('getText'));
                PSKPrn.PTKDrawTextTrueTypeW(250, 280, 55, 0, "微软雅黑", 3, 400, false, false, false, "A14", "到达日期:");
                PSKPrn.PTKDrawTextTrueTypeW(0, 285, 60, 0, "微软雅黑", 3, 400, false, false, false, "A15", tl);
                PSKPrn.PTKDrawTextTrueTypeW(250, 190, 55, 0, "微软雅黑", 3, 400, false, false, false, "A10", "到  达  站:");
                PSKPrn.PTKDrawTextTrueTypeW(70, 155, 100, 0, "微软雅黑", 3, 900, false, false, false, "A11", $('#ADest').combobox('getValue'));
                PSKPrn.PTKDrawTextTrueTypeW(250, 100, 55, 0, "微软雅黑", 3, 400, false, false, false, "A6", "件       数:");
                PSKPrn.PTKDrawTextTrueTypeW(70, 75, 65, 0, "微软雅黑", 3, 400, false, false, false, "A7", i + " / " + pie);
                //打印运单号的二维码
                if (!string.IsNullOrEmpty(entity.AwbNo.Trim())) {
                    PSKPrn.PTKDrawBar2DQR(80, 400, 800, 800, 2, 8, 2, 1, 0, $('#AAwbNo').textbox('getValue'));
                }
                PSKPrn.PTKDrawTextTrueTypeW(550, 400, 60, 0, "微软雅黑", 3, 600, false, false, false, "A12", "新速度  心服务");
                PSKPrn.PTKDrawTextTrueTypeW(400, 450, 100, 0, "微软雅黑", 3, 900, false, false, false, "A13", "新 陆 程 物 流");
                PSKPrn.PTKPrintLabel(1, 1);
                ClosePrinter();
            }
        };
        //打印运单
        function CreateOneFormPage() {
            var pcharge = $('#printCharge').is(':checked');
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            LODOP.SET_PRINT_PAGESIZE(1, 2300, 1300, "");
            LODOP.SET_PRINT_STYLE("FontName", "宋体");
            LODOP.SET_PRINT_STYLE("FontSize", 12);
            LODOP.SET_PRINT_STYLE("Bold", 1);
            var dest = $('#ADest').combobox('getValue');
            if ($('#ATransit').textbox('getValue') != "") {
                dest += "(" + $('#ATransit').textbox('getValue') + ")";
            }
            LODOP.ADD_PRINT_TEXT(40, 115, 100, 30, $('#ADep').combobox('getValue'));
            LODOP.ADD_PRINT_TEXT(40, 235, 120, 30, dest);
            LODOP.ADD_PRINT_TEXT(40, 395, 100, 30, getNowFormatDate($('#AHandleTime').datebox('getValue')));
            var sp;
            if ($('#AShipperUnit').combobox('getText') == "") { sp = $('#AShipperName').textbox('getValue'); }
            else if ($('#AShipperName').textbox('getValue') == "") {
                sp = $('#AShipperUnit').combobox('getText');
            } else { sp = $('#AShipperUnit').combobox('getText') + "/" + $('#AShipperName').textbox('getValue'); }
            LODOP.ADD_PRINT_TEXT(70, 127, 300, 60, sp);
            var sphone;
            if ($('#AShipperCellphone').textbox('getValue') == "") { sphone = $('#AShipperTelephone').textbox('getValue'); }
            else if ($('#AShipperTelephone').textbox('getValue') == "") { sphone = $('#AShipperCellphone').textbox('getValue'); }
            else { var sphone = $('#AShipperCellphone').textbox('getValue') + "/" + $('#AShipperTelephone').textbox('getValue'); }
            LODOP.ADD_PRINT_TEXT(90, 127, 300, 60, sphone);

            var ap;
            if ($('#AAcceptUnit').combobox('getText') == "") { ap = $('#AAcceptPeople').combobox('getText'); }
            else if ($('#AAcceptPeople').combobox('getText') == "") { ap = $('#AAcceptUnit').combobox('getText'); }
            else { var ap = $('#AAcceptUnit').combobox('getText') + "/" + $('#AAcceptPeople').combobox('getText'); }
            LODOP.ADD_PRINT_TEXT(65, 454, 500, 30, ap);
            LODOP.ADD_PRINT_TEXT(83, 454, 500, 30, $('#AAcceptAddress').textbox('getValue'));
            var aphone;
            if ($('#AAcceptCellphone').textbox('getValue') == "") { aphone = $('#AAcceptTelephone').textbox('getValue'); }
            else if ($('#AAcceptTelephone').textbox('getValue') == "") {
                aphone = $('#AAcceptCellphone').textbox('getValue');
            } else { var aphone = $('#AAcceptCellphone').textbox('getValue') + "/" + $('#AAcceptTelephone').textbox('getValue'); }
            LODOP.ADD_PRINT_TEXT(100, 454, 500, 30, aphone);

            var yunFee = Number($('#TransportFee').numberbox('getValue'));
            var griddata = $('#dgAdd').datagrid('getRows');
            var js = 0;
            for (var i = 0; i < griddata.length; i++) {
                js++;
                var p = Number(griddata[i].Piece) * Number(griddata[i].PiecePrice);
                var w = Number(griddata[i].Weight) * Number(griddata[i].WeightPrice);
                var v = Number(griddata[i].Volume) * Number(griddata[i].VolumePrice);

                LODOP.ADD_PRINT_TEXT(150 + i * 30, 50, 90, 30, griddata[i].Goods);
                LODOP.ADD_PRINT_TEXT(150 + i * 30, 120, 50, 30, griddata[i].Piece);
                LODOP.ADD_PRINT_TEXT(150 + i * 30, 170, 100, 30, griddata[i].Package); //包装说明
                LODOP.ADD_PRINT_TEXT(150 + i * 30, 255, 100, 30, griddata[i].Weight); //计费重量
                LODOP.ADD_PRINT_TEXT(150 + i * 30, 330, 100, 30, griddata[i].Volume); //计费体积
                //打印运费勾选框
                if (pcharge == false) {
                    LODOP.ADD_PRINT_TEXT(150 + i * 30, 390, 50, 30, ""); //单价
                    LODOP.ADD_PRINT_TEXT(150 + i * 30, 490, 60, 30, ""); //运费
                    LODOP.ADD_PRINT_TEXT(150 + i * 30, 525, 50, 30, ""); //保价费
                    LODOP.ADD_PRINT_TEXT(150 + i * 30, 575, 50, 30, ""); //提货费
                    LODOP.ADD_PRINT_TEXT(150 + i * 30, 625, 50, 30, ""); //送货费
                    LODOP.ADD_PRINT_TEXT(150 + i * 30, 675, 50, 30, ""); //中转费
                    LODOP.ADD_PRINT_TEXT(150 + i * 30, 710, 50, 30, ""); //其它费用
                    LODOP.ADD_PRINT_TEXT(150 + i * 30, 740, 100, 30, ""); //小计
                    continue;
                }
                if (p >= w && p >= v) {
                    //打印运费勾选框
                    if (pcharge == true) {
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 390, 50, 30, griddata[i].PiecePrice); //单价
                    }
                    LODOP.ADD_PRINT_TEXT(150 + i * 30, 446, 50, 30, griddata[i].DeclareValue); //声明价值
                    if (griddata.length == 1) {
                        p = Number($('#TransportFee').numberbox('getValue'));
                    }
                    LODOP.ADD_PRINT_TEXT(150 + i * 30, 490, 60, 30, p); //运费
                    if (js == 1) {
                        var xiaoji = Number(p) + Number($('#InsuranceFee').numberbox('getValue')) + Number($('#OtherFee').numberbox('getValue')) + Number($('#DeliverFee').numberbox('getValue')) + Number($('#TransitFee').numberbox('getValue'));
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 525, 50, 30, Number($('#InsuranceFee').numberbox('getValue'))); //保价费
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 575, 50, 30, Number($('#OtherFee').numberbox('getValue'))); //提货费
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 625, 50, 30, Number($('#DeliverFee').numberbox('getValue'))); //送货费
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 675, 50, 30, ""); //中转费
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 710, 50, 30, Number($('#TransitFee').numberbox('getValue'))); //其它费用
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 740, 100, 30, xiaoji); //小计
                    }
                    else {
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 525, 50, 30, ""); //保价费
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 575, 50, 30, ""); //提货费
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 625, 50, 30, ""); //送货费
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 675, 50, 30, ""); //中转费
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 710, 50, 30, ""); //其它费用
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 740, 100, 30, p); //小计                    
                    }

                }
                if (w >= p && w >= v) {
                    //打印运费勾选框
                    if (pcharge == true) {
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 390, 50, 30, griddata[i].WeightPrice); //单价
                    }
                    LODOP.ADD_PRINT_TEXT(150 + i * 30, 446, 50, 30, griddata[i].DeclareValue); //声明价值
                    if (griddata.length == 1) {
                        w = Number($('#TransportFee').numberbox('getValue'));
                    }
                    LODOP.ADD_PRINT_TEXT(150 + i * 30, 490, 80, 30, w); //运费
                    if (js == 1) {
                        var xiaoji = Number(w) + Number($('#InsuranceFee').numberbox('getValue')) + Number($('#OtherFee').numberbox('getValue')) + Number($('#DeliverFee').numberbox('getValue')) + Number($('#TransitFee').numberbox('getValue'));
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 525, 50, 30, Number($('#InsuranceFee').numberbox('getValue'))); //保价费
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 575, 50, 30, Number($('#OtherFee').numberbox('getValue'))); //提货费
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 625, 50, 30, Number($('#DeliverFee').numberbox('getValue'))); //送货费
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 675, 50, 30, ""); //中转费
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 710, 50, 30, Number($('#TransitFee').numberbox('getValue'))); //其它费用
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 740, 100, 30, xiaoji); //小计                        
                    }
                    else {
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 525, 50, 30, ""); //保价费
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 575, 50, 30, ""); //提货费
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 625, 50, 30, ""); //送货费
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 675, 50, 30, ""); //中转费
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 710, 50, 30, ""); //其它费用
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 740, 100, 30, w); //小计                         
                    }
                }
                if (v >= w && v >= p) {
                    //打印运费勾选框
                    if (pcharge == true) {
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 390, 50, 30, griddata[i].VolumePrice); //单价
                    }
                    LODOP.ADD_PRINT_TEXT(150 + i * 30, 446, 50, 30, griddata[i].DeclareValue); //声明价值
                    if (griddata.length == 1) {
                        v = Number($('#TransportFee').numberbox('getValue'));
                    }
                    LODOP.ADD_PRINT_TEXT(150 + i * 30, 490, 80, 30, v); //运费
                    if (js == 1) {
                        var xiaoji = Number(v) + Number($('#InsuranceFee').numberbox('getValue')) + Number($('#OtherFee').numberbox('getValue')) + Number($('#DeliverFee').numberbox('getValue')) + Number($('#TransitFee').numberbox('getValue'));
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 525, 50, 30, Number($('#InsuranceFee').numberbox('getValue'))); //保价费
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 575, 50, 30, Number($('#OtherFee').numberbox('getValue'))); //提货费
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 625, 50, 30, Number($('#DeliverFee').numberbox('getValue'))); //送货费
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 675, 50, 30, ""); //中转费
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 710, 50, 30, Number($('#TransitFee').numberbox('getValue'))); //其它费用
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 740, 100, 30, xiaoji); //小计                                       
                    }
                    else {
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 525, 50, 30, ""); //保价费
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 575, 50, 30, ""); //提货费
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 625, 50, 30, ""); //送货费
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 675, 50, 30, ""); //中转费
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 710, 50, 30, ""); //其它费用
                        LODOP.ADD_PRINT_TEXT(150 + i * 30, 740, 100, 30, v); //小计                
                    }
                }
            }
            //回单
            LODOP.ADD_PRINT_TEXT(202, 100, 50, 30, Number($('#ReturnAwb').numberbox('getValue')));
            //合计
            //var hd = Number(Printform.TotalMoney) + Number(Printform.Rebate);
            var hd = Number($('#TotalCharge').numberbox('getValue'));
            var hj = hd + "    " + atoc(hd);
            if (pcharge == false) {
                LODOP.ADD_PRINT_TEXTA("CheckOut", 202, 220, 600, 30, "");
            }
            else {
                LODOP.ADD_PRINT_TEXTA("CheckOut", 202, 220, 600, 30, hj);
                LODOP.SET_PRINT_STYLEA("CheckOut", "FontName", "宋体");
                LODOP.SET_PRINT_STYLEA("CheckOut", "FontSize", 15);
                LODOP.SET_PRINT_STYLEA("CheckOut", "Bold", 1);
            }
            var rck = $("input:radio[name='CheckOutType']:checked").val();
            //付款方式内容
            var fu = onCheckOutType(rck) + Number($('#TotalCharge').numberbox('getValue')) + "元";
            if (pcharge == false) {
                LODOP.ADD_PRINT_TEXTA("FuKuan", 240, 170, 230, 50, onCheckOutType(rck));
            }
            else {
                LODOP.ADD_PRINT_TEXTA("FuKuan", 240, 170, 230, 50, fu);
                LODOP.SET_PRINT_STYLEA("FuKuan", "FontName", "宋体");
                LODOP.SET_PRINT_STYLEA("FuKuan", "FontSize", 15);
                LODOP.SET_PRINT_STYLEA("FuKuan", "Bold", 1);
            }
            var dck = $("input:radio[name='DeliveryType']:checked").val();
            //交货方式内容
            LODOP.ADD_PRINT_TEXTA("Delivery", 240, 550, 130, 50, onDeliveryType(dck));
            LODOP.SET_PRINT_STYLEA("Delivery", "FontName", "宋体");
            LODOP.SET_PRINT_STYLEA("Delivery", "FontSize", 15);
            LODOP.SET_PRINT_STYLEA("Delivery", "Bold", 1);
            //代收货款内容
            LODOP.ADD_PRINT_TEXTA("Collect", 255, 650, 100, 25, Number($('#CollectMoney').numberbox('getValue')));
            LODOP.SET_PRINT_STYLEA("Collect", "FontName", "宋体");
            LODOP.SET_PRINT_STYLEA("Collect", "FontSize", 15);
            LODOP.SET_PRINT_STYLEA("Collect", "Bold", 1);
            //代垫贷款内容
            LODOP.ADD_PRINT_TEXTA("CollectA", 255, 730, 100, 25, "");
            LODOP.SET_PRINT_STYLEA("CollectA", "FontName", "宋体");
            LODOP.SET_PRINT_STYLEA("CollectA", "FontSize", 15);
            LODOP.SET_PRINT_STYLEA("CollectA", "Bold", 1);
            //备注内容
            LODOP.ADD_PRINT_TEXT(285, 160, 670, 40, $('#ARemark').val() + " " + $('#AHAwbNo').val());
            //录单员
            //LODOP.ADD_PRINT_TEXT(365, 710, 180, 30, document.getElementById("UserName").value);
            LODOP.ADD_PRINT_TEXT(365, 710, 180, 30, $('#CreateAwb').textbox('getValue'));
            LODOP.ADD_PRINT_TEXT(385, 630, 300, 30, $('#CreateDate').datetimebox('getValue'));
            LODOP.ADD_PRINT_TEXT(425, 50, 300, 30, "入库员：" + $('#ClerkNo').textbox('getValue'));
        };
        //结款方式
        function onCheckOutType(e) {
            var cott = [{ id: 0, text: '现付' }, { id: 1, text: '回单' }, { id: 2, text: '月结' }, { id: 3, text: '到付'}];
            for (var i = 0, l = cott.length; i < l; i++) { var g = cott[i]; if (g.id == e) return g.text; } return "";
        }
        //运货方式
        function onDeliveryType(e) {
            var DeliveryTypedd = [{ id: 0, text: '送货' }, { id: 1, text: '自提'}];
            for (var i = 0, l = DeliveryTypedd.length; i < l; i++) { var g = DeliveryTypedd[i]; if (g.id == e) return g.text; } return "";
        }
    </script>

</asp:Content>
