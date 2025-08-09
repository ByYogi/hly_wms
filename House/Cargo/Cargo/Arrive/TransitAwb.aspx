<%@ Page Title="到达运单中转" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TransitAwb.aspx.cs" Inherits="Cargo.Arrive.TransitAwb" %>
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
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 30;
            $('#dg').datagrid({ height: height });
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
        $(document).ready(function() {
            $('#dg').datagrid({
                width: '100%',
                height: '200px',
                title: '在站运单', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ArriveID',
                url: null,
                columns: [[
                  { title: '', field: 'ArriveID', checkbox: true, width: '30px' },
                  { title: '运单号', field: 'AwbNo', width: '70px' },
                  { title: '开单日期', field: 'HandleTime', width: '80px', formatter: DateFormatter },
                  { title: '最迟时效', field: 'LatestTimeLimit', width: '70px' },
                  { title: '客户名称', field: 'ShipperUnit', width: '100px' },
                  { title: '品名', field: 'Goods', width: '70px' },
                  { title: '件数', field: 'Piece', width: '40px' },
                  { title: '分批件数', field: 'AwbPiece', width: '50px' },
                  { title: '重量', field: 'Weight', width: '40px' },
                  { title: '体积', field: 'Volume', width: '40px' },
                  { title: '运输费用', field: 'TransportFee', width: '60px' },
                  { title: '总费用', field: 'TotalCharge', width: '60px' },
                  { title: '出发站', field: 'Dep', width: '50px' },
                  { title: '中间站', field: 'MidDest', width: '50px' },
                  { title: '到达站', field: 'Dest', width: '50px' },
                  { title: '中转站', field: 'Transit', width: '50px' },
                  { title: '回单要求', field: 'ReturnAwb', width: '60px' },
                  { title: '到达日期', field: 'ArriveDate', width: '130px', formatter: DateTimeFormatter },
                  { title: '结款方式', field: 'CheckOutType', width: '60px',
                      formatter: function(val, row, index) {
                          if (val == "0") { return "现付"; } else if (val == "1") { return "回单"; } else if (val == "2") { return "月结"; } else if (val == "3") { return "到付"; } else if (val == "4") { return "代收款"; } else { return ""; }
                      }
                  },
                  { title: '代收款', field: 'CollectMoney', width: '60px' },
                  { title: '录单员', field: 'CreateAwb', width: '60px' },
                  {
                      title: '回单状态', field: 'ReturnStatus', width: '60px',
                      formatter: function(val, row, index) { if (val == "N") { return "回单未录"; } else if (val == "Y") { return "回单已录"; } else { return ""; } }
                  },
                  { title: '回单信息', field: 'ReturnInfo', width: '60px' }
                ]],
                onLoadSuccess: function(data) {
                    //
                },
                onDblClickRow: function(index, row) {
                    up(index, row);
                },
                rowStyler: function(index, row) {
                    if (row.ReturnStatus == "Y") { return "color:#FF0000"; }
                }
            });
            $('#dgManifest').datagrid({
                width: '100%',
                height: '200px',
                title: '中转运单', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ArriveID',
                url: null,
                columns: [[
                  { title: '', field: 'ArriveID', checkbox: true, width: '30px' },
                  { title: '运单号', field: 'AwbNo', width: '70px' },
                  { title: '开单日期', field: 'HandleTime', width: '80px', formatter: DateFormatter },
                  { title: '最迟时效', field: 'LatestTimeLimit', width: '70px' },
                  { title: '客户名称', field: 'ShipperUnit', width: '100px' },
                  { title: '品名', field: 'Goods', width: '70px' },
                  { title: '件数', field: 'Piece', width: '40px' },
                  { title: '分批件数', field: 'AwbPiece', width: '50px' },
                  { title: '重量', field: 'Weight', width: '40px' },
                  { title: '体积', field: 'Volume', width: '40px' },
                  { title: '运输费用', field: 'TransportFee', width: '60px' },
                  { title: '总费用', field: 'TotalCharge', width: '60px' },
                  { title: '出发站', field: 'Dep', width: '50px' },
                  { title: '中间站', field: 'MidDest', width: '50px' },
                  { title: '到达站', field: 'Dest', width: '50px' },
                  { title: '中转站', field: 'Transit', width: '50px' },
                  { title: '回单要求', field: 'ReturnAwb', width: '60px' },
                  { title: '到达日期', field: 'ArriveDate', width: '130px', formatter: DateTimeFormatter },
                  { title: '结款方式', field: 'CheckOutType', width: '60px',
                      formatter: function(val, row, index) {
                          if (val == "0") { return "现付"; } else if (val == "1") { return "回单"; } else if (val == "2") { return "月结"; } else if (val == "3") { return "到付"; } else if (val == "4") { return "代收款"; } else { return ""; }
                      }
                  },
                  { title: '代收款', field: 'CollectMoney', width: '60px' },
                  { title: '录单员', field: 'CreateAwb', width: '60px' },
                  {
                      title: '回单状态', field: 'ReturnStatus', width: '60px',
                      formatter: function(val, row, index) { if (val == "N") { return "回单未录"; } else if (val == "Y") { return "回单已录"; } else { return ""; } }
                  },
                  { title: '回单信息', field: 'ReturnInfo', width: '60px' }
                ]],
                onLoadSuccess: function(data) {
                    //
                },
                onDblClickRow: function(index, row) {
                    down(index, row);
                },
                rowStyler: function(index, row) {
                    if (row.ReturnStatus == "Y") { return "color:#FF0000"; }
                }
            });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#OP_ID').textbox('setValue', '<%=UserInfor.LoginName %>')
            $('#DisplayNum').val(0);
            $('#DisplayPiece').val(0);
            $('#DisplayWeight').val(0);
            $('#DisplayVolume').val(0);
            $('#DisplayMoney').val(0);
            bindMethod();
            $('#Dest').combobox('setValues', '<%=UserInfor.DepCity%>');
            $('#Dest').combobox('disable');
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../Arrive/ArriveApi.aspx?method=QueryArriveDestAwb';
            $('#dg').datagrid('load', {
                AwbNo: $('#AwbNo').val(),
                ShipperUnit: $("#ShipperUnit").val(),
                AcceptPeople: $("#AcceptPeople").val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                Dep: $("#Dep").combobox('getValue'),
                Dest: $("#Dest").combobox('getValue'),
                Transit: $("#Transit").combobox('getValue'),
                CheckOutType: $('#CheckOutType').combobox('getValue')
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
    <%--此div用于在界面未完全加载样式前显示内容--%>
<div id="saPanel" class="easyui-panel" title="" data-options="iconCls:'icon-search'">
        <table>
            <tr>
                <td style="text-align: right;">
                    运单号:
                </td>
                <td>
                    <input id="AwbNo" class="easyui-textbox" data-options="prompt:'请输入运单号'" style="width: 100px">
                </td>
                <td style="text-align: right;">
                    发货:
                </td>
                <td>
                    <input id="ShipperUnit" class="easyui-textbox" data-options="prompt:'发货单位/人'" style="width: 100px;" />
                </td>
                <td style="text-align: right;">
                    收货:
                </td>
                <td>
                    <input id="AcceptPeople" class="easyui-textbox" data-options="prompt:'收货单位/人'" style="width: 100px;" />
                </td>
                <td style="text-align: right;">
                    出发站:
                </td>
                <td>
                    <input id="Dep" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../Arrive/ArriveApi.aspx?method=QueryAllCity'"
                        panelheight="auto" />
                </td>
                <td style="text-align: right;">
                    到达站:
                </td>
                <td>
                    <input id="Dest" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../Arrive/ArriveApi.aspx?method=QueryAllCity',multiple:true"
                         />
                </td>
                <td style="text-align: right;">
                    中转站:
                </td>
                <td>
                    <input id="Transit" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../Arrive/ArriveApi.aspx?method=QueryAllCity'"
                        panelheight="auto" />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询</a>&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-arrow_out" plain="false" onclick="tear()">&nbsp;拆&nbsp;票</a>&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-arrow_in" plain="false" onclick="merge()">&nbsp;合&nbsp;并</a>&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="plManifest()">批量选择</a>&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="plDelete()">批量删除</a>&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-database_go" plain="false" onclick="Delivery()">&nbsp;中&nbsp;转</a>
                </td>
                <td style="text-align: right;">
                    时间:
                </td>
                <td colspan="3">
                    <input id="StartDate" class="easyui-datebox" style="width: 110px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 110px">
                </td>
                <td style="text-align: right;">
                    付款方式:
                </td>
                <td>
                    <input class="easyui-combobox" id="Text1" data-options="url:'../Data/CheckType.json',method:'get',valueField:'id',textField:'text',panelHeight:'auto'"
                        style="width: 60px;">
                </td>
            </tr>
        </table>
    </div>
    <input type="hidden" id="DisplayNum" />
    <input type="hidden" id="DisplayPiece" />
    <input type="hidden" id="DisplayWeight" />
    <input type="hidden" id="DisplayVolume" />
    <input type="hidden" id="DisplayMoney" />
    <table id="dg" class="easyui-datagrid">
    </table>
    <table id="dgManifest" class="easyui-datagrid">
    </table>
    <div id="dlgTear" class="easyui-dialog" style="width: 500px; height: 200px; padding: 10px 10px"
        closed="true" buttons="#dlgTear-buttons">
        <form id="fmTear" class="easyui-form" method="post">
        <input type="hidden" name="AwbID" id="AAwbID" />
        <table>
            <tr>
                <td style="text-align: right;">
                    总件数:
                </td>
                <td>
                    <input id="TotalAwbPiece" readonly="true" class="easyui-numberbox" data-options="min:0,precision:0"
                        style="width: 100px;">
                </td>
                <td style="text-align: right;">
                    总重量:
                </td>
                <td>
                    <input id="TotalAwbWeight" readonly="true" class="easyui-numberbox" data-options="min:0,precision:2"
                        style="width: 100px;">
                </td>
                <td style="text-align: right;">
                    总体积:
                </td>
                <td>
                    <input id="TotalAwbVolume" readonly="true" class="easyui-numberbox" data-options="min:0,precision:2"
                        style="width: 100px;">
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    分批件数:
                </td>
                <td>
                    <input id="TearPiece" name="TearPiece" class="easyui-numberbox" data-options="min:0,precision:0"
                        style="width: 100px;">
                </td>
                <td style="text-align: right;">
                    分批重量:
                </td>
                <td>
                    <input id="TearWeight" readonly="true" class="easyui-numberbox" data-options="min:0,precision:2"
                        style="width: 100px;">
                </td>
                <td style="text-align: right;">
                    分批体积:
                </td>
                <td>
                    <input id="TearVolume" readonly="true" class="easyui-numberbox" data-options="min:0,precision:2"
                        style="width: 100px;">
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    剩余件数:
                </td>
                <td>
                    <input id="OtherPiece" readonly="true" class="easyui-numberbox" data-options="min:0,precision:0"
                        style="width: 100px;">
                </td>
                <td style="text-align: right;">
                    剩余重量:
                </td>
                <td>
                    <input id="OtherWeight" readonly="true" class="easyui-numberbox" data-options="min:0,precision:2"
                        style="width: 100px;">
                </td>
                <td style="text-align: right;">
                    剩余体积:
                </td>
                <td>
                    <input id="OtherVolume" readonly="true" class="easyui-numberbox" data-options="min:0,precision:2"
                        style="width: 100px;">
                </td>
            </tr>
        </table>
        </form>
    </div>
    <div id="dlgTear-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveTear()">保存</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgTear').dialog('close')">取消</a>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 750px; height: 360px; padding: 3px 3px"
        closed="true" closable="false" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
        <input type="hidden" name="Unitphone" id="Unitphone" />
        <input type="hidden" name="UnitBoss" id="UnitBoss" />
        <input type="hidden" name="UnitAddress" id="UnitAddress" />
        <input type="hidden" id="DDest" />
        <table>
            <tr style="height: 35px;">
                <td style="text-align: right; color: Red; font-weight: bold; border-bottom: solid 2px #89B1F1;">
                    运单票数:
                </td>
                <td style="border-bottom: solid 2px #89B1F1;">
                    <input id="TransitPiao" class="easyui-textbox" readonly="true" style="width: 100px;">
                </td>
                <td style="text-align: right; color: Red; font-weight: bold; border-bottom: solid 2px #89B1F1;">
                    运单件数:
                </td>
                <td style="border-bottom: solid 2px #89B1F1;">
                    <input id="TransitPiece" class="easyui-textbox" readonly="true" style="width: 100px;">
                </td>
                <td style="text-align: right; color: Red; font-weight: bold; border-bottom: solid 2px #89B1F1;">
                    运单重量:
                </td>
                <td style="border-bottom: solid 2px #89B1F1;">
                    <input id="TransitWeight" class="easyui-textbox" readonly="true" style="width: 100px;">
                </td>
                <td style="text-align: right; color: Red; font-weight: bold; border-bottom: solid 2px #89B1F1;">
                    运单体积:
                </td>
                <td style="border-bottom: solid 2px #89B1F1;">
                    <input id="TransitVolume" class="easyui-textbox" readonly="true" style="width: 150px;">
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    承运公司:
                </td>
                <td>
                    <input name="CarrierShortName" id="CarrierShortName" class="easyui-combobox" data-options="required:true,url:'../Arrive/ArriveApi.aspx?method=QueryCarrier',method:'get',valueField:'CarrierID',textField:'CarrierShortName',onSelect:carrierChange"
                        style="width: 100px;">
                </td>
                <td style="text-align: right;">
                    联系人:
                </td>
                <td>
                    <input name="Boss" id="Boss" class="easyui-textbox" style="width: 100px;">
                </td>
                <td style="text-align: right;">
                    联系电话:
                </td>
                <td>
                    <input name="Telephone" id="Telephone" class="easyui-textbox" style="width: 100px;">
                </td>
                <td style="text-align: right;">
                    公司地址:
                </td>
                <td>
                    <input name="Address" id="Address" class="easyui-textbox" style="width: 150px;">
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    外协单号:
                </td>
                <td>
                    <input name="AssistNum" id="AssistNum" class="easyui-textbox" style="width: 100px;">
                </td>
                <td style="text-align: right;">
                    付款方式:
                </td>
                <td>
                    <input class="easyui-combobox" name="CheckOutType" id="CheckOutType" data-options="url:'../Data/check.json',method:'get',valueField:'id',textField:'text',panelHeight:'auto'"
                        style="width: 100px;">
                </td>
                <td style="text-align: right;">
                    操作人:
                </td>
                <td>
                    <input name="OP_ID" id="OP_ID" readonly="true" class="easyui-textbox" style="width: 100px;">
                </td>
                <td style="text-align: right;">
                    发车时间:
                </td>
                <td>
                    <input name="StartTime" id="StartTime" class="easyui-datetimebox" data-options="required:true"
                        style="width: 150px;">
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    运输费用:
                </td>
                <td>
                    <input name="TransportFee" id="TransportFee" data-options="min:0,precision:2" class="easyui-numberbox"
                        style="width: 100px;" />
                </td>
                <td style="text-align: right;">
                    预付:
                </td>
                <td>
                    <input name="PrepayFee" id="PrepayFee" data-options="min:0,precision:2" class="easyui-numberbox"
                        style="width: 100px;" />
                </td>
                <td style="text-align: right;">
                    到付:
                </td>
                <td>
                    <input name="ArriveFee" id="ArriveFee" data-options="min:0,precision:2" class="easyui-numberbox"
                        style="width: 100px;" />
                </td>
                <td style="text-align: right;">
                    其它费用:
                </td>
                <td>
                    <input name="OtherFee" id="OtherFee" data-options="min:0,precision:2" class="easyui-numberbox"
                        style="width: 150px;" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    提货费用:
                </td>
                <td>
                    <input name="DeliveryFee" id="DeliveryFee" data-options="min:0,precision:2" class="easyui-numberbox"
                        style="width: 100px;" />
                </td>
                <td style="text-align: right;">
                    送货费:
                </td>
                <td>
                    <input name="SendFee" id="SendFee" data-options="min:0,precision:2" class="easyui-numberbox"
                        style="width: 100px;" />
                </td>
                <td style="text-align: right;">
                    装卸费用:
                </td>
                <td>
                    <input name="HandFee" id="HandFee" data-options="min:0,precision:2" class="easyui-numberbox"
                        style="width: 100px;" />
                </td>
                <td style="text-align: right;">
                    代收款:
                </td>
                <td>
                    <input name="CollectMoney" id="CollectMoney" data-options="min:0,precision:2" class="easyui-numberbox"
                        style="width: 150px;" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    中转件数:
                </td>
                <td>
                    <input name="Piece" id="Piece" data-options="min:0,precision:0" class="easyui-numberbox"
                        style="width: 100px;" />
                </td>
                <td style="text-align: right;">
                    中转重量:
                </td>
                <td>
                    <input name="Weight" id="Weight" data-options="min:0,precision:2" class="easyui-numberbox"
                        style="width: 100px;" />
                </td>
                <td style="text-align: right;">
                    中转体积:
                </td>
                <td>
                    <input name="Volume" id="Volume" data-options="min:0,precision:2" class="easyui-numberbox"
                        style="width: 100px;" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    件数单价:
                </td>
                <td>
                    <input name="PiecePrice" id="PiecePrice" data-options="min:0,precision:2" class="easyui-numberbox"
                        style="width: 100px;" />
                </td>
                <td style="text-align: right;">
                    重量单价:
                </td>
                <td>
                    <input name="WeightPrice" id="WeightPrice" data-options="min:0,precision:3" class="easyui-numberbox"
                        style="width: 100px;" />
                </td>
                <td style="text-align: right;">
                    体积单价:
                </td>
                <td>
                    <input name="VolumePrice" id="VolumePrice" data-options="min:0,precision:3" class="easyui-numberbox"
                        style="width: 100px;" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    备注:
                </td>
                <td colspan="8">
                    <textarea id="Remark" rows="3" name="Remark" style="width: 350px;"></textarea>
                </td>
            </tr>
        </table>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" id="save" onclick="saveItem()">
            &nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp; <a href="#" class="easyui-linkbutton" iconcls="icon-cancel"
                onclick="closeDlg()">&nbsp;取&nbsp;消&nbsp;</a>&nbsp;&nbsp; <a href="#" class="easyui-linkbutton"
                    iconcls="icon-print" onclick="PrePrint()" id="print">&nbsp;打印预览&nbsp;</a>
    </div>
    <object id="LODOP_OB" title="dd" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA"
        width="0" height="0">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0" height="0"></embed>
    </object>

    <script type="text/javascript">
        //合并
        function merge() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows.length <= 1) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择进行合并的运单数据！', 'warning'); return; }
            var json = JSON.stringify(rows)
            $.ajax({
                url: "../Arrive/ArriveApi.aspx?method=TransitMerge",
                type: "post",
                data: { submitData: json },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '合并成功!', 'info');
                        dosearch();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '合并失败：' + result.Message, 'warning');
                    }
                }
            });
        }
        //分批
        function tear() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows.length <= 0) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择进行分批的运单数据！', 'warning'); return; }
            if (rows.length > 1) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '只能选择一条运单数据进行分批！', 'warning'); return; }
            var p = rows[0].AwbPiece;
            var w = rows[0].AwbWeight;
            var v = rows[0].AwbVolume;
            var awbno = rows[0].AwbNo;
            $('#dlgTear').dialog('open').dialog('setTitle', awbno + '运单分批');
            $('#fmTear').form('clear');
            $("#AAwbID").val(rows.AwbID);
            $("#TearPiece").numberbox('setValue', '');
            $("#TotalAwbPiece").numberbox('setValue', p);
            $("#TotalAwbWeight").numberbox('setValue', w);
            $("#TotalAwbVolume").numberbox('setValue', v);
            $("#TearPiece").numberbox({
                "onChange": function (newValue, oldValue) {
                    var tp = newValue; //分批件数
                    if (tp >= p) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '分批件数不能大于总件数！', 'warning');
                    return;
                }
                var op = Number(p) - Number(tp); //剩余件数
                var tw = (Number(w) * Number(tp) / Number(p)).toFixed(2); //分批重量
                var tv = (Number(v) * Number(tp) / Number(p)).toFixed(2); //分批体积
                $("#OtherPiece").numberbox('setValue', op);
                $("#TearWeight").numberbox('setValue', tw);
                $("#TearVolume").numberbox('setValue', tv);
                $("#OtherWeight").numberbox('setValue', (w - tw).toFixed(2));
                $("#OtherVolume").numberbox('setValue', (v - tv).toFixed(2));
            }
            });
        }
        //保存分批
        function saveTear() {
            var row = $('#dg').datagrid('getSelected');
            var json = JSON.stringify([row])
            $('#fmTear').form('submit', {
                url: '../Arrive/ArriveApi.aspx?method=TransitTear&data=' + json,
                contentType: "application/json;charset=utf-8", dataType: "json",
                onSubmit: function (param) {
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '分批成功!', 'info');
                        $('#dlgTear').dialog('close'); 	// close the dialog
                        dosearch();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '分批失败：' + result.Message, 'warning');
                    }
                }
            })
        }
        //新增中转清单
        function up(index, rows) {
            $('#dg').datagrid('deleteRow', index);
            var index = $('#dgManifest').datagrid('getRowIndex', rows.ArriveID);
            if (index < 0) {
                $('#dgManifest').datagrid('appendRow', rows);
                var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
                var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
                var w = $('#DisplayWeight').val() == "" || isNaN($('#DisplayWeight').val()) ? 0 : Number($('#DisplayWeight').val());
                var v = $('#DisplayVolume').val() == "" || isNaN($('#DisplayVolume').val()) ? 0 : Number($('#DisplayVolume').val());
                var m = $('#DisplayMoney').val() == "" || isNaN($('#DisplayMoney').val()) ? 0 : Number($('#DisplayMoney').val());
                n++;
                var pt = p + Number(rows.AwbPiece);
                var wt = w + Number(rows.AwbWeight);
                var vt = v + Number(rows.AwbVolume);
                var mt = m + Number(rows.TotalCharge);
                $('#DisplayNum').val(Number(n));
                $('#DisplayPiece').val(Number(pt));
                $('#DisplayWeight').val(Number(wt).toFixed(2));
                $('#DisplayVolume').val(Number(vt).toFixed(2));
                $('#DisplayMoney').val(Number(mt).toFixed(2));
                var title = "中转运单      已中转：" + n + "票，件数：" + pt + " 件，重量：" + Number(wt).toFixed(2) + " 吨，体积：" + Number(vt).toFixed(2) + " 方，总收入：" + Number(mt).toFixed(2) + " 元";
                $('#dgManifest').datagrid("getPanel").panel("setTitle", title);
            }
        }
        //移除中转清单
        function down(index, rows) {
            $('#dgManifest').datagrid('deleteRow', index);
            var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
            var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
            var w = $('#DisplayWeight').val() == "" || isNaN($('#DisplayWeight').val()) ? 0 : Number($('#DisplayWeight').val());
            var v = $('#DisplayVolume').val() == "" || isNaN($('#DisplayVolume').val()) ? 0 : Number($('#DisplayVolume').val());
            var m = $('#DisplayMoney').val() == "" || isNaN($('#DisplayMoney').val()) ? 0 : Number($('#DisplayMoney').val());
            n--;
            var pt = p - Number(rows.AwbPiece);
            var wt = w - Number(rows.AwbWeight);
            var vt = v - Number(rows.AwbVolume);
            var mt = m - Number(rows.TotalCharge);
            $('#DisplayNum').val(Number(n));
            $('#DisplayPiece').val(Number(pt));
            $('#DisplayWeight').val(Number(wt).toFixed(2));
            $('#DisplayVolume').val(Number(vt).toFixed(2));
            $('#DisplayMoney').val(Number(mt).toFixed(2));
            var title = "中转运单      已中转：" + n + "票，件数：" + pt + " 件，重量：" + Number(wt).toFixed(2) + " 吨，体积：" + Number(vt).toFixed(2) + " 方，总收入：" + Number(mt).toFixed(2) + " 元";
            $('#dgManifest').datagrid("getPanel").panel("setTitle", title);
            var index = $('#dg').datagrid('getRowIndex', rows.ArriveID);
            if (index < 0) {
                $('#dg').datagrid('appendRow', rows);
            }
        }
        //批量选择
        function plManifest() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择数据！', 'warning'); return; }
            var copyRows = [];
            for (var i = 0, l = rows.length; i < l; i++) {
                var row = rows[i];
                copyRows.push(row);
                var index = $('#dgManifest').datagrid('getRowIndex', copyRows[i].ArriveID);
                if (index < 0) {
                    $('#dgManifest').datagrid('appendRow', row);
                    var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
                    var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
                    var w = $('#DisplayWeight').val() == "" || isNaN($('#DisplayWeight').val()) ? 0 : Number($('#DisplayWeight').val());
                    var v = $('#DisplayVolume').val() == "" || isNaN($('#DisplayVolume').val()) ? 0 : Number($('#DisplayVolume').val());
                    var m = $('#DisplayMoney').val() == "" || isNaN($('#DisplayMoney').val()) ? 0 : Number($('#DisplayMoney').val());
                    n++;
                    var pt = p + Number(row.AwbPiece);
                    var wt = w + Number(row.AwbWeight);
                    var vt = v + Number(row.AwbVolume);
                    var mt = m + Number(row.TotalCharge);
                    $('#DisplayNum').val(Number(n));
                    $('#DisplayPiece').val(Number(pt));
                    $('#DisplayWeight').val(Number(wt).toFixed(2));
                    $('#DisplayVolume').val(Number(vt).toFixed(2));
                    $('#DisplayMoney').val(Number(mt).toFixed(2));
                    var title = "中转运单      已中转：" + n + "票，件数：" + pt + " 件，重量：" + Number(wt).toFixed(2) + " 吨，体积：" + Number(vt).toFixed(2) + " 方，总收入：" + Number(mt).toFixed(2) + " 元";
                    $('#dgManifest').datagrid("getPanel").panel("setTitle", title);
                }
            }
            for (var i = 0; i < copyRows.length; i++) {
                var index = $('#dg').datagrid('getRowIndex', copyRows[i]);
                $('#dg').datagrid('deleteRow', index);
            }
        }
        //批量删除
        function plDelete() {
            var rows = $('#dgManifest').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择数据！', 'warning'); return; }
            var copyRows = [];
            for (var i = 0, l = rows.length; i < l; i++) {
                var row = rows[i];
                copyRows.push(row);
                var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
                var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
                var w = $('#DisplayWeight').val() == "" || isNaN($('#DisplayWeight').val()) ? 0 : Number($('#DisplayWeight').val());
                var v = $('#DisplayVolume').val() == "" || isNaN($('#DisplayVolume').val()) ? 0 : Number($('#DisplayVolume').val());
                var m = $('#DisplayMoney').val() == "" || isNaN($('#DisplayMoney').val()) ? 0 : Number($('#DisplayMoney').val());
                n--;
                var pt = p - Number(row.AwbPiece);
                var wt = w - Number(row.AwbWeight);
                var vt = v - Number(row.AwbVolume);
                var mt = m - Number(row.TotalCharge);
                $('#DisplayNum').val(Number(n));
                $('#DisplayPiece').val(Number(pt));
                $('#DisplayWeight').val(Number(wt).toFixed(2));
                $('#DisplayVolume').val(Number(vt).toFixed(2));
                $('#DisplayMoney').val(Number(mt).toFixed(2));
                var title = "中转运单      已中转：" + n + "票，件数：" + pt + " 件，重量：" + Number(wt).toFixed(2) + " 吨，体积：" + Number(vt).toFixed(2) + " 方，总收入：" + Number(mt).toFixed(2) + " 元";
                $('#dgManifest').datagrid("getPanel").panel("setTitle", title);
                var index = $('#dg').datagrid('getRowIndex', copyRows[i].ArriveID);
                if (index < 0) {
                    $('#dg').datagrid('appendRow', row);
                }
            }
            for (var i = 0; i < copyRows.length; i++) {
                var index = $('#dgManifest').datagrid('getRowIndex', copyRows[i]);
                $('#dgManifest').datagrid('deleteRow', index);
            }
        }
        //新增中转
        function Delivery() {
            var row = $('#dgManifest').datagrid('getRows');
            if (row.length <= 0) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要中转的运单！', 'warning'); return; }
            $('#dlg').dialog('open').dialog('setTitle', '新增中转');
            $('#fm').form('clear');
            $('#DDest').val(row[0].Dest);
            var d = new Date();
            $('#StartTime').datetimebox('setValue', AllDateTime(d));
            $('#OP_ID').textbox('setValue', "<%= UserInfor.LoginName%>");
            loadUnit();
            $('#TransitPiao').textbox('setValue', Number($('#DisplayNum').val()));
            $('#TransitPiece').textbox('setValue', Number($('#DisplayPiece').val()));
            $('#TransitWeight').textbox('setValue', Number($('#DisplayWeight').val()).toFixed(2));
            $('#TransitVolume').textbox('setValue', Number($('#DisplayVolume').val()).toFixed(2));

            $('#Piece').textbox('setValue', Number($('#DisplayPiece').val()));
            $('#Weight').textbox('setValue', Number($('#DisplayWeight').val()).toFixed(2));
            $('#Volume').textbox('setValue', Number($('#DisplayVolume').val()).toFixed(2));
        }
        //绑定费用框
        function bindMethod() {
            $("#Piece").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
            $("#PiecePrice").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
            $("#Weight").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
            $("#WeightPrice").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
            $("#Volume").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
            $("#VolumePrice").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
        }
        function qh() {
            var pS = Number($('#Piece').numberbox('getValue')) * Number($('#PiecePrice').numberbox('getValue'));
            var wS = Number($('#Weight').numberbox('getValue')) * Number($('#WeightPrice').numberbox('getValue'));
            var vS = Number($('#Volume').numberbox('getValue')) * Number($('#VolumePrice').numberbox('getValue'));
            var t = 0;
            if (pS >= wS) { t = pS; } else { t = wS; }
            if (vS > t) { t = vS; }
            $('#TransportFee').numberbox('setValue', Number(t).toFixed(2));
        }
        //保存
        function saveItem() {
            var row = $('#dgManifest').datagrid('getRows');
            var json = JSON.stringify(row)
            $('#fm').form('submit', {
                url: '../Arrive/ArriveApi.aspx?method=AddTransit',
                onSubmit: function (param) {
                    param.data = json;
                    var trd = $(this).form('enableValidation').form('validate');
                    if (trd) { $('#save').linkbutton('disable'); }
                    return trd;
                },
                success: function (msg) {
                    $('#save').linkbutton('enable');
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '中转成功，是否打印中转单？', function (r) {
                            if (r) { Print(); }
                        });
                    } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '中转失败：' + result.Message, 'warning'); }
                }
            })
        }
        function closeDlg() {
            $('#dlg').dialog('close');
            $('#dg').datagrid('reload');
            $('#dgManifest').datagrid('loadData', { total: 0, rows: [] });
            $('#DisplayNum').val(0);
            $('#DisplayPiece').val(0);
            $('#DisplayWeight').val(0);
            $('#DisplayVolume').val(0);
            $('#DisplayMoney').val(0);
            var title = "中转运单      已中转：0票，件数：0 件，重量：0吨，体积：0方，总收入：0 元";
            $('#dgManifest').datagrid("getPanel").panel("setTitle", title);
        }
        //承运公司选择后自动赋值
        function carrierChange(item) {
            if (item) {
                $('#Boss').textbox('setValue', item.Boss);
                $('#Telephone').textbox('setValue', item.Telephone);
                $('#Address').textbox('setValue', item.Address);
            }
        }
        function loadUnit() {
            $.ajax({
                url: "../Arrive/ArriveApi.aspx?method=GetUnitByCityCode&id=" + escape($('#DDest').val()),
                cache: false,
                success: function (o) {
                    var result = eval('(' + o + ')');
                    $('#UnitAddress').val(result.Address);
                    $('#UnitBoss').val(result.Boss);
                    $('#Unitphone').val(result.phone);
                }
            });
        }
        var LODOP;
        //打印预览
        function Print() {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            CreateDataBill();
            //LODOP.PREVIEW();
            LODOP.PRINT();
            //LODOP.PRINT_SETUP();
        }
        //打印预览
        function PrePrint() {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            CreateDataBill();
            LODOP.PREVIEW();
            //LODOP.PRINT();
            //LODOP.PRINT_SETUP();
        }
        //打印设置
        function CreateDataBill() {
            LODOP.SET_PRINTER_INDEX(-1);
            LODOP.SET_PRINT_PAGESIZE(1, 2400, 2750, "");
            LODOP.SET_PRINT_STYLE("FontName", "宋体");
            LODOP.SET_PRINT_STYLE("FontSize", 10);
            LODOP.SET_PRINT_STYLE("Bold", 0);
            var bl = "<%=UserInfor.NewLandBelongSystem %>";
            var bt = "新路城配货物中转清单";
            if (bl == "DR") { bt = "鼎融物流有限公司货物中转清单"; }
            else if (bl == "FJ") { bt = "福建新陆程物流货物中转清单"; }
            else if (bl == "YQ") { bt = "云起物流货物中转清单"; }
            else if (bl == "ZY") { bt = "众盈物流货物中转清单"; }
            LODOP.ADD_PRINT_TEXT(10, 270, 700, 30, bt);
            LODOP.SET_PRINT_STYLEA(1, "FontName", "宋体");
            LODOP.SET_PRINT_STYLEA(1, "FontSize", 16);
            LODOP.SET_PRINT_STYLEA(1, "Bold", 1);
            var d = new Date();
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(35, 25, 80, 25, "外协单号:");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(35, 105, 120, 25, $('#AssistNum').textbox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(35, 225, 60, 25, "承运商:");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(35, 285, 100, 25, $('#CarrierShortName').combobox('getText'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(35, 385, 60, 25, "运费:");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(35, 445, 80, 25, $('#TransportFee').textbox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(35, 525, 75, 25, "中转日期:");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(35, 600, 200, 25, AllDateTime(d));

            LODOP.ADD_PRINT_RECT(60, 20, 70, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 22, 70, 20, "发货日期");
            LODOP.ADD_PRINT_RECT(60, 90, 60, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 92, 60, 20, "运单号");
            LODOP.ADD_PRINT_RECT(60, 150, 65, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 152, 65, 20, "收货人");
            LODOP.ADD_PRINT_RECT(60, 215, 230, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 217, 230, 20, "地址");
            LODOP.ADD_PRINT_RECT(60, 445, 60, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 447, 60, 20, "品名");
            LODOP.ADD_PRINT_RECT(60, 505, 40, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 507, 40, 20, "件数");
            LODOP.ADD_PRINT_RECT(60, 545, 40, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 547, 40, 20, "分批");
            LODOP.ADD_PRINT_RECT(60, 585, 45, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 587, 45, 20, "重量");
            LODOP.ADD_PRINT_RECT(60, 630, 45, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 632, 45, 20, "体积");
            LODOP.ADD_PRINT_RECT(60, 675, 50, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 677, 50, 20, "运费");
            LODOP.ADD_PRINT_RECT(60, 725, 50, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 727, 50, 20, "代收款");
            LODOP.ADD_PRINT_RECT(60, 775, 60, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 777, 60, 20, "付款方式");
            LODOP.ADD_PRINT_RECT(60, 835, 40, 20, 0, 1);
            LODOP.ADD_PRINT_TEXT(62, 837, 40, 20, "回单");
            var n = 0;
            var pNum = 0;
            var charge = 0;
            var dsk = 0;
            var hd = 0;
            var fp = 0;
            var griddata = $('#dgManifest').datagrid('getRows');
            for (var i = 0; i < griddata.length + 1; i++) {
                if (i == griddata.length) {
                    LODOP.ADD_PRINT_TEXT(90 + i * 20, 30, 100, 20, "备注：");
                    LODOP.ADD_PRINT_TEXT(90 + i * 20, 130, 700, 20, $('#Remark').val());
                    LODOP.ADD_PRINT_TEXT(110 + i * 20, 30, 100, 20, "负责人:");
                    LODOP.ADD_PRINT_TEXT(110 + i * 20, 130, 700, 20, $('#UnitBoss').val());
                    LODOP.ADD_PRINT_TEXT(130 + i * 20, 30, 100, 20, "联系电话:");
                    LODOP.ADD_PRINT_TEXT(130 + i * 20, 130, 700, 20, $('#Unitphone').val());
                    LODOP.ADD_PRINT_TEXT(150 + i * 20, 30, 100, 20, "地址:");
                    LODOP.ADD_PRINT_TEXT(150 + i * 20, 130, 700, 20, $('#UnitAddress').val());
                    LODOP.ADD_PRINT_TEXT(200 + i * 20, 30, 100, 20, "操作员：");
                    LODOP.ADD_PRINT_TEXT(200 + i * 20, 130, 150, 20, $('#OP_ID').textbox('getValue'));
                    LODOP.ADD_PRINT_TEXT(200 + i * 20, 400, 100, 20, "承运商签字：");
                    break;
                }
                n++;
                LODOP.ADD_PRINT_RECT(80 + i * 20, 20, 70, 20, 0, 1);
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 22, 70, 20, getNowFormatDate(griddata[i].HandleTime));
                LODOP.ADD_PRINT_RECT(80 + i * 20, 90, 60, 20, 0, 1);
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 92, 60, 20, griddata[i].AwbNo);
                LODOP.ADD_PRINT_RECT(80 + i * 20, 150, 65, 20, 0, 1);
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 152, 65, 20, griddata[i].AcceptPeople);
                LODOP.ADD_PRINT_RECT(80 + i * 20, 215, 230, 20, 0, 1);
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 217, 230, 20, griddata[i].AcceptAddress);
                LODOP.ADD_PRINT_RECT(80 + i * 20, 445, 60, 20, 0, 1);
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 447, 60, 20, griddata[i].Goods);
                LODOP.ADD_PRINT_RECT(80 + i * 20, 505, 40, 20, 0, 1);
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 507, 40, 20, griddata[i].Piece);
                LODOP.ADD_PRINT_RECT(80 + i * 20, 545, 40, 20, 0, 1);
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 547, 40, 20, griddata[i].AwbPiece);
                LODOP.ADD_PRINT_RECT(80 + i * 20, 585, 45, 20, 0, 1);
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 587, 45, 20, griddata[i].AwbWeight);
                LODOP.ADD_PRINT_RECT(80 + i * 20, 630, 45, 20, 0, 1);
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 632, 45, 20, griddata[i].AwbVolume);
                LODOP.ADD_PRINT_RECT(80 + i * 20, 675, 50, 20, 0, 1);
                var tp = 0;
                if (griddata[i].CheckOutType == "3") { tp = griddata[i].TransportFee; }
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 677, 50, 20, tp);
                LODOP.ADD_PRINT_RECT(80 + i * 20, 725, 50, 20, 0, 1);
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 727, 50, 20, griddata[i].CollectMoney);
                var fu = onCheckOutType(griddata[i].CheckOutType)
                LODOP.ADD_PRINT_RECT(80 + i * 20, 775, 60, 20, 0, 1);
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 777, 60, 20, fu);
                LODOP.ADD_PRINT_RECT(80 + i * 20, 835, 40, 20, 0, 1);
                LODOP.ADD_PRINT_TEXT(82 + i * 20, 837, 40, 20, griddata[i].ReturnAwb);
            }
        }
        //结款方式
        var CheckOutType = [{ id: 0, text: '现付' }, { id: 1, text: '回单' }, { id: 2, text: '月结' }, { id: 3, text: '到付' }, { id: 4, text: '代收款' }];
        function onCheckOutType(e) {
            for (var i = 0, l = CheckOutType.length; i < l; i++) {
                var g = CheckOutType[i];
                if (g.id == e) return g.text;
            }
            return "";
        }
    </script>
</asp:Content>
