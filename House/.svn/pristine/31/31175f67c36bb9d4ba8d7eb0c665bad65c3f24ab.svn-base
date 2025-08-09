<%@ Page Title="客户货物跟踪" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AwbStatusTrack.aspx.cs" Inherits="Cargo.Arrive.AwbStatusTrack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<script src="../JS/Date/CheckActivX.js" type="text/javascript"></script>--%>
    <script src="../JS/Lodop/LodopFuncs.js" type="text/javascript"></script>

    <script src="../JS/Rotate/jQueryRotate.2.2.js" type="text/javascript"></script>

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
    </style>
    <%--    <link href="../CSS/road/css/component.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/road/css/default.css" rel="stylesheet" type="text/css" />
    --%>

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
        $(document).ready(function () {
            $('#dg').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'AwbID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                  { title: '', field: 'AwbID', checkbox: true, width: '30px' },
                  {
                      title: '回单照片', field: 'UploadReturnPic', width: '60px',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "未上传"; }
                          else if (val == "1") { return "已上传"; }
                          else { return "未上传"; }
                      }
                  },
                  { title: '运单号', field: 'AwbNo', width: '70px' },
                  { title: '开单日期', field: 'HandleTime', width: '75px', formatter: DateFormatter },
                  { title: '最迟时效', field: 'LatestTimeLimit', width: '75px' },
                  { title: '发货单位', field: 'ShipperUnit', width: '90px' },
                  { title: '发货人', field: 'ShipperName', width: '70px' },
                  { title: '联系方式', field: 'ShipperPhone', width: '100px' },
                  { title: '收货人', field: 'AcceptPeople', width: '70px' },
                  { title: '联系方式', field: 'AcceptPhone', width: '100px' },
                  {
                      title: '运单状态', field: 'TruckFlag', width: '60px',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "在站"; }
                          else if (val == "1") { return "出发"; }
                          else if (val == "2") { return "在途"; }
                          else if (val == "3") { return "到达"; }
                          else if (val == "4") { return "结束"; }
                          else if (val == "5") { return "关注"; }
                          else if (val == "6") { return "送达"; }
                          else if (val == "7") { return "签收"; }
                          else if (val == "8") { return "配送"; }
                          else if (val == "9") { return "中转"; }
                          else if (val == "10") { return "回单发送"; }
                          else if (val == "11") { return "移库"; }
                          else if (val == "12") { return "到达移库"; }
                          else if (val == "13") { return "异常"; }
                          else { return "在站"; }
                      }
                  },
                  { title: '回单信息', field: 'ReturnInfo', width: '60px' },
                  { title: '品名', field: 'Goods', width: '70px' },
                  { title: '总件数', field: 'Piece', width: '50px' },
                  { title: '分批件数', field: 'AwbPiece', width: '60px' },
                  { title: '分批重量', field: 'AwbWeight', width: '60px' },
                  { title: '分批体积', field: 'AwbVolume', width: '60px' },
                  {
                      title: '类型', field: 'TransKind', width: '40px',
                      formatter: function (val, row, index) { if (val == "0") { return "自发"; } else if (val == "1") { return "外协"; } else { return "自发"; } }
                  },
                  { title: '出发站', field: 'Dep', width: '50px' },
                  { title: '中间站', field: 'MidDest', width: '50px' },
                  { title: '到达站', field: 'Dest', width: '50px' },
                  { title: '中转站', field: 'Transit', width: '50px' },
                  {
                      title: '送货方式', field: 'DeliveryType', width: '60px',
                      formatter: function (val, row, index) { if (val == "0") { return "送货"; } else if (val == "1") { return "自提"; } else { return "送货"; } }
                  },
                  { title: '录单员', field: 'CreateAwb', width: '60px' },
                  { title: '配载合同号', field: 'ContractNum', width: '90px' },
                  { title: '车辆牌照', field: 'TruckNum', width: '70px' },
                  { title: '发车时间', field: 'StartTime', width: '130px', formatter: DateTimeFormatter },
                  { title: '实际到达时间', field: 'ActArriveTime', width: '130px', formatter: DateTimeFormatter },
                  { title: '司机', field: 'Driver', width: '70px' },
                  { title: '手机号码', field: 'DriverCellPhone', width: '70px' },
                  { title: '合同费用', field: 'ContractFee', width: '70px' },
                  {
                      title: '结算状态', field: 'CheckStatus', width: '60px',
                      formatter: function (val, row, index) { if (val == "0") { return "未结算"; } else if (val == "1") { return "已结清"; } else if (val == "2") { return "未结清"; } else { return "未结算"; } }
                  },
                  { title: '回单要求', field: 'ReturnAwb', width: '60px' },
                  { title: '到达中转时间', field: 'TTime', width: '130px', formatter: DateTimeFormatter },
                  { title: '承运商', field: 'TShortName', width: '60px' },
                  { title: '承运单号', field: 'TAssistNum', width: '60px' },
                  { title: '联系方式', field: 'TPhone', width: '60px' },
                  { title: '到达配送时间', field: 'DTime', width: '130px', formatter: DateTimeFormatter },
                  { title: '配送司机', field: 'DDrive', width: '60px' },
                  { title: '车牌号', field: 'DTruckNum', width: '60px' },
                  { title: '联系方式', field: 'DPhone', width: '60px' },
                  { title: '签收人', field: 'Signer', width: '50px' },
                  { title: '签收时间', field: 'SignTime', width: '130px', formatter: DateTimeFormatter },
                  { title: '发送时间', field: 'SendReturnAwbDate', width: '130px', formatter: DateTimeFormatter },
                  { title: '确认时间', field: 'ConfirmReturnAwbDate', width: '130px', formatter: DateTimeFormatter }
                ]],
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) {
                    editItemByID(index);
                },
                rowStyler: function (index, row) {
                    if (row.ReturnStatus == "Y") {
                        return "color:#FF0000";
                    }
                    if (row.TrafficType == "2" && row.ReturnStatus == "Y") {
                        return "font-weight:bold;background:#80C8FE;";
                    }
                }
            });
            var value2 = 0
            $("#simg").rotate({ bind: { click: function () { value2 += 90; $(this).rotate({ animateTo: value2 }) } } });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#printAWB').prop('checked', true);
            $('#printCharge').prop('checked', true);
            $('#printBAR').prop('checked', false);
            $('#Dest').combobox('setValues', '<%=UserInfor.DepCity%>');
            $('#Dest').combobox('disable');
        });
        //显示运单的货物信息
        function showGoods() {
            $('#dgAdd').datagrid({
                width: '100%',
                height: '100px',
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
                  { title: '品名', field: 'Goods', width: '120px' },
                  { title: '包装', field: 'Package', width: '90px' },
                  { title: '件数', field: 'Piece', width: '80px' },
                  { title: '单价(元/件)', field: 'PiecePrice', width: '80px' },
                  { title: '重量(吨)', field: 'Weight', width: '80px' },
                  { title: '单价(元/吨)', field: 'WeightPrice', width: '80px' },
                  { title: '体积', field: 'Volume', width: '80px' },
                  { title: '单价(元/立方)', field: 'VolumePrice', width: '80px' },
                  { title: '声明价值', field: 'DeclareValue', width: '90px' }
                ]]
            });
        }
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../Arrive/ArriveApi.aspx?method=QueryClientAwbStatusTrack';
            $('#dg').datagrid('load', {
                AwbNo: $('#AwbNo').val(),
                ShipperUnit: $("#ShipperUnit").val(),
                AcceptPeople: $("#AcceptPeople").val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                ADate: $('#ADate').datebox('getValue'),
                BDate: $('#BDate').datebox('getValue'),
                Goods: $("#Goods").val(),
                Piece: $("#Piece").val(),
                TransKind: $('#TransKind').combobox('getValue'),
                DeliveryType: $('#DeliveryType').combobox('getValue'),
                TruckFlag: $("#TruckFlag").combobox('getValue'),
                HAwbNo: $("#HAwbNo").val(),
                Transit: $('#Transit').textbox('getValue'),
                Dep: $("#Dep").combobox('getValue'),
                MidDest: $("#MidDest").combobox('getValue'),
                Dest: $("#Dest").combobox('getValue'),
                CheckStatus: $("#CheckStatus").combobox('getValue')
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
                <td style="text-align: right;">运单号码:
                </td>
                <td>
                    <input id="AwbNo" class="easyui-textbox" data-options="prompt:'请输入运单号'" style="width: 100px">
                </td>
                <td style="text-align: right;">发货:
                </td>
                <td>
                    <input id="ShipperUnit" class="easyui-textbox" data-options="prompt:'发货单位/人'" style="width: 100px;" />
                </td>
                <td style="text-align: right;">收货:
                </td>
                <td>
                    <input id="AcceptPeople" class="easyui-textbox" data-options="prompt:'收货单位/人'" style="width: 100px;" />
                </td>
                <td style="text-align: right;">受理日期:
                </td>
                <td colspan="3">
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">货物品名:
                </td>
                <td>
                    <input id="Goods" class="easyui-textbox" data-options="prompt:'请输入货物品名'" style="width: 100px;" />
                </td>
                <td style="text-align: right;">运单件数:
                </td>
                <td>
                    <input id="Piece" class="easyui-textbox" data-options="prompt:'请输入件数'" style="width: 100px">
                </td>
                <td style="text-align: right;">运单类型:
                </td>
                <td>
                    <select class="easyui-combobox" id="TransKind" style="width: 100px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">自发</option>
                        <option value="1">外协</option>
                    </select>
                </td>
                <td style="text-align: right;">送货方式:
                </td>
                <td>
                    <select class="easyui-combobox" id="DeliveryType" style="width: 100px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">送货</option>
                        <option value="1">自提</option>
                    </select>
                </td>
                <td style="text-align: right;">运单状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="TruckFlag" style="width: 100px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">在站</option>
                        <option value="1">出发</option>
                        <option value="2">在途</option>
                        <option value="3">到达</option>
                        <option value="4">结束</option>
                        <option value="5">关注</option>
                        <option value="6">送达</option>
                        <option value="7">签收</option>
                        <option value="8">配送</option>
                        <option value="9">中转</option>
                        <option value="10">回单发送</option>
                        <option value="11">移库</option>
                        <option value="12">到达移库</option>
                        <option value="13">异常</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">副单号:
                </td>
                <td>
                    <input id="HAwbNo" class="easyui-textbox" data-options="prompt:'请输入副单号'" style="width: 100px;" />
                </td>
                <td style="text-align: right;">出发站:
                </td>
                <td>
                    <input id="Dep" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../Arrive/ArriveApi.aspx?method=QueryAllCity&City='" />
                </td>
                <td style="text-align: right;">中间站:
                </td>
                <td>
                    <input id="MidDest" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../Arrive/ArriveApi.aspx?method=QueryAllCity&City='"
                        panelheight="auto" />
                </td>
                <td style="text-align: right;">到达站:
                </td>
                <td>
                    <input id="Dest" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../Arrive/ArriveApi.aspx?method=QueryAllCity&City='" />
                </td>
                <td style="text-align: right;">中转站:
                </td>
                <td>
                    <input id="Transit" class="easyui-textbox" style="width: 100px;" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-application_put"
                        plain="false" onclick="AwbExport()">&nbsp;导&nbsp;出</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
                            iconcls="icon-find" plain="false" onclick="track()">详细跟踪信息</a>&nbsp;&nbsp;<a href="#"
                                class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="modifyTrack()">修改中转跟踪</a>红色字体：回单已录，粗体：加急
                    <form runat="server" id="fm1">
                        <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
                    </form>
                    <%--<a href="#" class="easyui-linkbutton" iconcls="icon-find" plain="false"
                            onclick="Road()">路线图</a>--%>
                </td>
                <td style="text-align: right;">结算状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="CheckStatus" style="width: 100px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">未结算</option>
                        <option value="1">已结清</option>
                        <option value="2">未结清</option>
                    </select>
                </td>
                <td style="text-align: right;">开单日期:
                </td>
                <td colspan="3">
                    <input id="ADate" class="easyui-datebox" style="width: 100px">~
                    <input id="BDate" class="easyui-datebox" style="width: 100px">
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="dlg" class="easyui-dialog" style="width: 700px; height: 470px; padding: 10px 10px"
        closed="true" buttons="#dlg-buttons">
        <div id="lblTrack">
        </div>
        <table class="mini-toolbar" style="width: 100%;">
            <tr>
                <td>运单备注：
                </td>
            </tr>
            <tr>
                <form id="fmRemark" class="easyui-form" method="post">
                    <input type="hidden" id="BAwbNo" />
                    <td align="left">
                        <textarea name="DetailInfo" id="DetailInfo" cols="100" style="height: 35px;" class="mini-textarea"></textarea>
                    </td>
                </form>
            </tr>
            <tr>
                <td>
                    <div id="lblAwbMemo">
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" id="save" iconcls="icon-ok" onclick="saveRemark()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp; <a href="#" class="easyui-linkbutton" iconcls="icon-cancel"
            onclick="javascript:$('#dlg').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <div id="dlgRoad" class="easyui-dialog" style="width: 900px; height: 450px; padding: 10px 10px"
        closed="true" buttons="#dlgRoad-buttons">
        <div id="lblRoad">
        </div>
    </div>
    <div id="dlgRoad-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgRoad').dialog('close')">取消</a>
    </div>
    <div id="dlgAwbDetail" class="easyui-dialog" style="width: 900px; height: 480px;"
        closed="true" buttons="#dlgAwbDetail-buttons">
        <form id="fm" class="easyui-form" method="post">
            <table style="width: 100%">
                <tr>
                    <td style="color: Red; font-weight: bolder; text-align: right;">运单号:
                    </td>
                    <td>
                        <input name="AwbNo" id="AAwbNo" class="easyui-textbox" style="width: 100px">
                    </td>
                    <td style="color: Red; font-weight: bolder; text-align: right;">启运站:
                    </td>
                    <td>
                        <input name="Dep" id="ADep" class="easyui-textbox" style="width: 70px;" />
                    </td>
                    <td style="color: Red; font-weight: bolder; text-align: right;">到达站:
                    </td>
                    <td>
                        <input name="Dest" id="ADest" class="easyui-textbox" style="width: 70px;" />
                    </td>
                    <td style="text-align: right;">中转站:
                    </td>
                    <td>
                        <input name="Transit" id="ATransit" class="easyui-textbox" style="width: 70px;" />
                    </td>
                    <td style="color: Red; font-weight: bolder; text-align: right;">受理日期:
                    </td>
                    <td>
                        <input name="HandleTime" id="AHandleTime" class="easyui-datebox" style="width: 100px"
                            data-options="required:true">
                    </td>
                </tr>
                <tr>
                    <td style="color: Red; font-weight: bolder; text-align: right;">托运单位:
                    </td>
                    <td>
                        <input name="ShipperUnit" id="AShipperUnit" class="easyui-textbox" style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">托运人:
                    </td>
                    <td>
                        <input name="ShipperName" id="AShipperName" class="easyui-textbox" style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">地址:
                    </td>
                    <td>
                        <input name="ShipperAddress" id="AShipperAddress" style="width: 170px;" class="easyui-textbox" />
                    </td>
                    <td style="text-align: right;">联系电话:
                    </td>
                    <td>
                        <input name="ShipperTelephone" id="AShipperTelephone" class="easyui-textbox" style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">手机:
                    </td>
                    <td>
                        <input name="ShipperCellphone" id="AShipperCellphone" class="easyui-textbox" style="width: 90px;" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">收货单位:
                    </td>
                    <td>
                        <input name="AcceptUnit" id="AAcceptUnit" class="easyui-textbox" style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">收货人:
                    </td>
                    <td>
                        <input name="AcceptPeople" id="AAcceptPeople" class="easyui-textbox" style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">地址:
                    </td>
                    <td>
                        <input name="AcceptAddress" id="AAcceptAddress" style="width: 170px;" class="easyui-textbox" />
                    </td>
                    <td style="text-align: right;">联系电话:
                    </td>
                    <td>
                        <input name="AcceptTelephone" id="AAcceptTelephone" class="easyui-textbox" style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">手机:
                    </td>
                    <td>
                        <input name="AcceptCellphone" id="AAcceptCellphone" class="easyui-textbox" style="width: 90px;" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%;" colspan="10">客户编号:<input name="ClientNum" id="AClientNum" class="easyui-textbox" style="width: 100px">
                    </td>
                </tr>
            </table>
            <table id="dgAdd" class="easyui-datagrid">
            </table>
            <table style="width: 100%">
                <tr>
                    <td style="text-align: right;">件数:
                    </td>
                    <td>
                        <input name="Piece" id="APiece" class="easyui-numberbox" data-options="min:0,precision:0,required:true"
                            style="width: 80px;" />
                    </td>
                    <td style="text-align: right;">重量:
                    </td>
                    <td>
                        <input id="Weight" name="Weight" class="easyui-numberbox" data-options="min:0,precision:3,required:true"
                            style="width: 80px;" />吨
                    </td>
                    <td style="text-align: right;">体积:
                    </td>
                    <td>
                        <input name="Volume" id="Volume" class="easyui-numberbox" data-options="min:0,precision:3,required:true"
                            style="width: 80px;" />方
                    </td>
                    <td style="text-align: right;">其它费用:
                    </td>
                    <td>
                        <input name="TransitFee" id="TransitFee" class="easyui-numberbox" data-options="min:0,precision:2"
                            style="width: 80px;" />
                    </td>
                    <td style="text-align: right;">运输费用:
                    </td>
                    <td>
                        <input name="TransportFee" id="TransportFee" data-options="min:0,precision:2" class="easyui-numberbox"
                            style="width: 80px;" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">送货费:
                    </td>
                    <td>
                        <input name="DeliverFee" id="DeliverFee" data-options="min:0,precision:2" class="easyui-numberbox"
                            style="width: 80px;" />
                    </td>
                    <td style="text-align: right;">提货费:
                    </td>
                    <td>
                        <input name="OtherFee" id="OtherFee" data-options="min:0,precision:2" class="easyui-numberbox"
                            style="width: 80px;" />
                    </td>
                    <td style="text-align: right;">保费:
                    </td>
                    <td>
                        <input name="InsuranceFee" id="InsuranceFee" data-options="min:0,precision:2" class="easyui-numberbox"
                            style="width: 80px;" />
                    </td>
                    <td style="text-align: right;">合计:
                    </td>
                    <td>
                        <input name="TotalCharge" id="TotalCharge" class="easyui-numberbox" data-options="min:0,precision:2"
                            style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">回扣:
                    </td>
                    <td>
                        <input name="Rebate" data-options="min:0,precision:2" class="easyui-numberbox" style="width: 80px;" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">代收款:
                    </td>
                    <td>
                        <input name="CollectMoney" id="CollectMoney" class="easyui-numberbox" data-options="min:0,precision:2"
                            style="width: 80px;" />元
                    </td>
                    <td style="text-align: right;">回单要求:
                    </td>
                    <td>
                        <input name="ReturnAwb" id="ReturnAwb" class="easyui-numberbox" data-options="min:0,precision:0"
                            style="width: 60px;" />份
                    </td>
                    <td style="text-align: right;">状态:
                    </td>
                    <td>
                        <select class="easyui-combobox" name="DelFlag" style="width: 60px;" readonly="readonly"
                            panelheight="auto">
                            <option value="-1">全部</option>
                            <option value="0">在站</option>
                            <option value="1">出发</option>
                            <option value="2">在途</option>
                            <option value="3">到达</option>
                            <option value="4">结束</option>
                            <option value="5">关注</option>
                            <option value="6">送达</option>
                            <option value="7">签收</option>
                            <option value="8">配送</option>
                            <option value="9">中转</option>
                            <option value="10">回单发送</option>
                            <option value="11">移库</option>
                            <option value="12">到达移库</option>
                            <option value="13">异常</option>
                        </select>
                    </td>
                    <td style="text-align: right;">开单员
                    </td>
                    <td>
                        <input name="CreateAwb" id="CreateAwb" class="easyui-textbox" readonly="true" style="width: 80px;" />
                    </td>
                    <td style="text-align: right;">开单时间
                    </td>
                    <td colspan="3">
                        <input name="CreateDate" id="CreateDate" class="easyui-datetimebox" data-options="formatter:AllDateTime"
                            readonly="true" style="width: 150px;" />
                    </td>
                </tr>
                <tr>
                    <td style="color: Red; font-weight: bolder; text-align: right;">结款方式:
                    </td>
                    <td>
                        <input name="CheckOutType" class="easyui-combobox" id="ACheckOutType" data-options="data: [{id: '0',text: '现付'},{id: '3',text: '到付'},{id: '1',text: '回单'},{id: '2',text: '月结'}],valueField:'id',textField:'text',panelHeight:'auto',required:true"
                            style="width: 80px;">
                    </td>
                    <td style="color: Red; font-weight: bolder; text-align: right;">运输时效
                    </td>
                    <td>
                        <select name="TimeLimit" panelheight="auto" style="width: 80px;" class="easyui-combobox"
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
                    <td style="color: Red; font-weight: bolder; text-align: right;">送货方式:
                    </td>
                    <td colspan="3">
                        <select name="DeliveryType" id="Select1" panelheight="auto" style="width: 80px;"
                            class="easyui-combobox" data-options="required:true">
                            <option value="0">送货</option>
                            <option value="1">自提</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">副单号:
                    </td>
                    <td colspan="4">
                        <textarea name="HAwbNo" id="AHAwbNo" rows="3" style="width: 250px;"></textarea>
                    </td>
                    <td style="text-align: right;">重要提示:
                    </td>
                    <td colspan="4">
                        <textarea name="Remark" id="ARemark" rows="3" style="width: 250px;"></textarea>
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlgAwbDetail-buttons">
        <table>
            <tr>
                <td style="text-align: left; width: 40%">
                    <input type="checkbox" id="printAWB" name="printAWB" value="0"><label for="printAWB">打印运单</label>
                    <input type="checkbox" id="printCharge" name="printAWB" value="1"><label for="printCharge">打印运费</label>
                    <input type="checkbox" id="printBAR" name="printAWB" value="2"><label for="printBAR">打印标签</label>
                </td>
                <td style="text-align: right; width: 50%">
                    <input type="checkbox" id="hlyAwb" name="hlyAwb" value="0"><label for="hlyAwb">好来运单</label>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" id="preprint" iconcls="icon-print" onclick="PrePrint()">&nbsp;打印预览&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-cancel"
                        onclick="javascript:$('#dlgAwbDetail').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    <object id="LODOP_OB" title="dd" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA"
        width="0" height="0">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0" height="0"></embed>
    </object>
    <div id="dgViewImg" class="easyui-dialog" closed="true" style="width: 1000px; height: 500px;">
        <img id="simg" />
    </div>
    <div id="dlgTrack" class="easyui-dialog" style="width: 450px; height: 360px; padding: 3px 3px"
        closed="true" closable="false" buttons="#dlgTrack-buttons">
        <form id="FrmTrack" class="easyui-form" method="post">
            <input type="hidden" name="AwbNo" id="TAwbNo" />
            <input type="hidden" name="ArriveID" />
            <input type="hidden" name="AwbID" id="AAwbID" />
            <input type="hidden" name="Dest" id="TDest" />
            <div id="saPanel">
                <table class="mini-toolbar" style="width: 100%;">
                    <tr>
                        <td rowspan="5" style="border-right: 1px solid #909aa6;">运<br />
                            单<br />
                            在<br />
                            途<br />
                            跟<br />
                            踪
                        </td>
                        <td align="left">&nbsp;&nbsp;状态：
                        </td>
                        <td align="left">
                            <input name="AwbStatus" id="ATruckFlag1" type="radio" value="9"><label for="ATruckFlag1">途中</label>
                            <input name="AwbStatus" id="ATruckFlag2" type="radio" value="6"><label for="ATruckFlag2">送达</label>
                            <input name="AwbStatus" id="ATruckFlag3" type="radio" value="13"><label for="ATruckFlag3">异常</label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">&nbsp;&nbsp;运单所在地:
                        </td>
                        <td align="left">
                            <input name="CurrentLocation" id="CurrentLocation" class="easyui-textbox" data-options="prompt:'请输入运单所在地'">
                        </td>
                    </tr>
                    <tr>
                        <td align="left">&nbsp;&nbsp;预计/实际:
                        </td>
                        <td align="left">
                            <input id="ArriveTime" name="ArriveTime" class="easyui-datetimebox" data-options="required:true"
                                style="width: 150px">到达
                        </td>
                    </tr>
                    <tr>
                        <td align="left" colspan="2">
                            <textarea name="DetailInfo" id="TDetailInfo" cols="60" style="height: 35px;" class="mini-textarea"></textarea>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </div>
    <div id="dlgTrack-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" id="btnTrack" onclick="savetrack()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-cancel"
            onclick="javascript:$('#dlgTrack').dialog('close');">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>

    <script type="text/javascript">
        //保存跟踪
        function savetrack() {
            $('#FrmTrack').form('submit', {
                url: '../Arrive/ArriveApi.aspx?method=SaveAwbTruckStatus',
                onSubmit: function () {
                    var trd = $(this).form('enableValidation').form('validate');
                    if (trd) { $('#btnTrack').linkbutton('disable'); }
                    return trd;
                },
                success: function (msg) {
                    $('#btnTrack').linkbutton('enable');
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
        //修改中转跟踪
        function modifyTrack() {
            var row = $('#dg').datagrid('getSelections');
            if (row == null || row == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning'); return; }
            if (row.length > 1) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择一条数据！', 'warning'); return; }
            if (row) {
                if (Number(row[0].TruckFlag) < 3) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', row[0].AwbNo + '运单未到达', 'warning');
                    return;
                }
                if (row[0].TruckFlag == "4") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', row[0].AwbNo + '运单已结束', 'warning');
                    return;
                }
                if (row[0].TruckFlag == "7") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', row[0].AwbNo + '运单已签收', 'warning');
                    return;
                }
                $('#dlgTrack').dialog('open').dialog('setTitle', '修改运单号：' + row[0].AwbNo + ' 中转跟踪状态');
                $('#FrmTrack').form('clear');
                $('#FrmTrack').form('load', row[0]);
                $("input[name=AwbStatus]").click(function () { showCont(); });
            }
        }
        function showCont() {
            switch ($("input[name=AwbStatus]:checked").attr("id")) {
                case "ATruckFlag2": $("#CurrentLocation").textbox('setValue', $('#TDest').val()); break;
                case "ATruckFlag3": $("#CurrentLocation").textbox('setValue', $('#TDest').val()); break;
                default: $("#CurrentLocation").textbox('setValue', ''); break;
            }
        }
        //保存备注
        function saveRemark() {
            $('#fmRemark').form('submit', {
                url: '../Arrive/ArriveApi.aspx?method=AddAwbRemarkInfo&AwbNo=' + escape($('#BAwbNo').val()),
                onSubmit: function () {
                    var trd = $(this).form('enableValidation').form('validate');
                    return trd;
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $.ajax({
                            async: false,
                            url: "../Arrive/ArriveApi.aspx?method=QueryAwbNoRemarkInfo&id=" + escape($('#BAwbNo').val()),
                            cache: false,
                            success: function (text) {
                                var ldl = document.getElementById("lblAwbMemo");
                                ldl.innerHTML = text;
                            }
                        });
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
        //查看运单信息
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlgAwbDetail').dialog('open').dialog('setTitle', '查看：' + row.AwbNo + '运单信息');
                showGoods();
                $('#dgAdd').datagrid('loadData', { total: 0, rows: [] });
                $.ajax({
                    url: "../Arrive/ArriveApi.aspx?method=GetAwbInfoByAwbNo&id=" + row.AwbNo,
                    cache: false,
                    success: function (text) {
                        var o = eval('(' + text + ')');
                        o.CreateDate = AllDateTime(o.CreateDate);
                        $('#fm').form('load', o);
                        if (o.HLY == "0" || o.HLY == null) {
                            $('#hlyAwb').prop('checked', false);
                        } else {
                            $('#hlyAwb').prop('checked', true);
                        }
                        $('#ACheckOutType').combobox('setValue', o.CheckOutType);
                        for (var i = 0; i < o.AwbGoods.length; i++) {
                            var rows = { Goods: o.AwbGoods[i].Goods, Package: o.AwbGoods[i].Package, Piece: o.AwbGoods[i].Piece, PiecePrice: o.AwbGoods[i].PiecePrice, Weight: o.AwbGoods[i].Weight, WeightPrice: o.AwbGoods[i].WeightPrice, Volume: o.AwbGoods[i].Volume, VolumePrice: o.AwbGoods[i].VolumePrice, DeclareValue: o.AwbGoods[i].DeclareValue, GoodsID: o.AwbGoods[i].GoodsID };
                            $('#dgAdd').datagrid('appendRow', rows);
                            var lastIndex = $('#dgAdd').datagrid('getRows').length - 1;
                            $('#dgAdd').datagrid('beginEdit', i);
                        }
                    }
                });
            }
        }
        function download(img) {
            var simg = "http://120.236.158.136:9101/" + img;
            $('#dgViewImg').dialog('open').dialog('setTitle', '预览');
            $("#simg").attr("src", simg);

        }
        //运单详细跟踪界面
        function track() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '运单号：' + row.AwbNo + '详细跟踪信息');
                $('#BAwbNo').val(row.AwbNo);
                $('#fmRemark').form('clear');
                $.ajax({
                    url: "../Arrive/ArriveApi.aspx?method=QueryAwbTrackInfo&id=" + row.AwbNo + "&aid=" + row.AwbID + "&hawbno=" + row.HAwbNo,
                    cache: false,
                    success: function (text) {
                        var ldl = document.getElementById("lblTrack");
                        ldl.innerHTML = text;
                    }
                });
                $.ajax({
                    async: false,
                    url: "../Arrive/ArriveApi.aspx?method=QueryAwbNoRemarkInfo&id=" + row.AwbNo,
                    cache: false,
                    success: function (text) {
                        var ldl = document.getElementById("lblAwbMemo");
                        ldl.innerHTML = text;
                    }
                });
            }
        }
        //运单路线图
        function Road() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlgRoad').dialog('open').dialog('setTitle', '运单号：' + row.AwbNo + '路线图');
                $.ajax({
                    url: "../Arrive/ArriveApi.aspx?method=QueryAwbRoad&id=" + row.AwbNo + "&aid=" + row.AwbID,
                    cache: false,
                    success: function (text) {
                        var ldl = document.getElementById("lblRoad");
                        ldl.innerHTML = text;
                    }
                });
            }
        }
        //导出数据
        function AwbExport() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning');
                return;
            }
            var key = new Array();
            key[0] = $('#StartDate').datebox('getValue');
            key[1] = $('#EndDate').datebox('getValue');
            key[2] = $("#Dest").combobox('getValue');
            key[3] = $('#AwbNo').val();
            key[4] = $("#ShipperUnit").val();
            key[5] = $("#AcceptPeople").val();
            key[6] = $("#Goods").val();
            key[7] = $("#Piece").val();
            key[8] = $('#DeliveryType').datebox('getValue');
            key[9] = $("#TruckFlag").combobox('getValue');
            key[10] = $('#TransKind').datebox('getValue');
            key[11] = $("#HAwbNo").val();
            key[12] = $("#CheckStatus").combobox('getValue');
            key[13] = $("#Dep").combobox('getValue');
            key[14] = $("#MidDest").combobox('getValue');
            key[15] = $('#ADate').datebox('getValue');
            key[16] = $('#BDate').datebox('getValue');
            key[17] = $("#Transit").val();
            $.ajax({
                url: "../Arrive/ArriveApi.aspx?method=QueryAwbStatusTrackForExport&key=" + escape(key.toString()),
                success: function (text) {
                    if (text == "OK") {
                        var obj = document.getElementById("<%=btnDerived.ClientID %>");
                        obj.click();
                    }
                    else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning');
                    }
                }
            });
        }

        var LODOP;
        //打印预览
        function PrePrint() {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            if ($('#printAWB').is(':checked')) {
                CreateOneFormPage();
                if (LODOP.SET_PRINTER_INDEX(-1)) {
                    LODOP.PREVIEW();
                }
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
            var dest = $('#ADest').textbox('getValue');
            if ($('#ATransit').textbox('getValue') != "") { dest += "(" + $('#ATransit').textbox('getValue') + ")"; }
            LODOP.ADD_PRINT_TEXT(40, 115, 100, 30, $('#ADep').textbox('getValue'));
            LODOP.ADD_PRINT_TEXT(40, 235, 100, 30, dest);
            LODOP.ADD_PRINT_TEXT(40, 395, 100, 30, getNowFormatDate($('#AHandleTime').datebox('getValue')));
            var sp;
            if ($('#AShipperUnit').textbox('getValue') == "") { sp = $('#AShipperName').textbox('getValue'); }
            else if ($('#AShipperName').textbox('getValue') == "") {
                sp = $('#AShipperUnit').textbox('getValue');
            } else { sp = $('#AShipperUnit').textbox('getValue') + "/" + $('#AShipperName').textbox('getValue'); }
            LODOP.ADD_PRINT_TEXT(70, 127, 300, 60, sp);
            var sphone;
            if ($('#AShipperCellphone').textbox('getValue') == "") { sphone = $('#AShipperTelephone').textbox('getValue'); }
            else if ($('#AShipperTelephone').textbox('getValue') == "") {
                sphone = $('#AShipperCellphone').textbox('getValue');
            } else { var sphone = $('#AShipperCellphone').textbox('getValue') + "/" + $('#AShipperTelephone').textbox('getValue'); }
            LODOP.ADD_PRINT_TEXT(90, 127, 300, 60, sphone);

            var ap;
            if ($('#AAcceptUnit').textbox('getValue') == "") { ap = $('#AAcceptPeople').textbox('getValue'); }
            else if ($('#AAcceptPeople').textbox('getValue') == "") { ap = $('#AAcceptUnit').textbox('getValue'); }
            else { var ap = $('#AAcceptUnit').textbox('getValue') + "/" + $('#AAcceptPeople').textbox('getValue'); }
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
            //付款方式内容
            var fu = onCheckOutType($('#ACheckOutType').combobox('getValue')) + Number($('#TotalCharge').numberbox('getValue')) + "元";
            if (pcharge == false) {
                LODOP.ADD_PRINT_TEXTA("FuKuan", 240, 170, 230, 50, onCheckOutType($('#ACheckOutType').combobox('getValue')));
            }
            else {
                LODOP.ADD_PRINT_TEXTA("FuKuan", 240, 170, 230, 50, fu);
                LODOP.SET_PRINT_STYLEA("FuKuan", "FontName", "宋体");
                LODOP.SET_PRINT_STYLEA("FuKuan", "FontSize", 15);
                LODOP.SET_PRINT_STYLEA("FuKuan", "Bold", 1);
            }
            //交货方式内容
            LODOP.ADD_PRINT_TEXTA("Delivery", 240, 550, 130, 50, onDeliveryType($('#DeliveryType').combobox('getValue')));
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
            LODOP.ADD_PRINT_TEXT(285, 160, 670, 40, $('#AHAwbNo').val() + $('#ARemark').val());
            //录单员
            //LODOP.ADD_PRINT_TEXT(365, 710, 180, 30, document.getElementById("UserName").value);
            LODOP.ADD_PRINT_TEXT(365, 710, 180, 30, $('#CreateAwb').textbox('getValue'));
            LODOP.ADD_PRINT_TEXT(385, 630, 300, 30, $('#CreateDate').datetimebox('getValue'));
        };
        //结款方式
        function onCheckOutType(e) {
            var CheckOutTypedd = [{ id: 0, text: '现付' }, { id: 1, text: '回单' }, { id: 2, text: '月结' }, { id: 3, text: '到付' }];
            for (var i = 0, l = CheckOutTypedd.length; i < l; i++) { var g = CheckOutTypedd[i]; if (g.id == e) return g.text; } return "";
        }
        //运输方式
        function onTrafficType(e) {
            var TrafficTypedd = [{ id: 0, text: '普汽' }, { id: 1, text: '包车' }, { id: 2, text: '加急' }, { id: 3, text: '铁路' }];
            for (var i = 0, l = TrafficTypedd.length; i < l; i++) { var g = TrafficTypedd[i]; if (g.id == e) return g.text; } return "";
        }
        //运货方式
        function onDeliveryType(e) {
            var DeliveryTypedd = [{ id: 0, text: '送货' }, { id: 1, text: '自提' }];
            for (var i = 0, l = DeliveryTypedd.length; i < l; i++) { var g = DeliveryTypedd[i]; if (g.id == e) return g.text; } return "";
        }
        //本电脑上连接的所有打印机供选择
        function CreatePrinterList() {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            var iPrinterCount = LODOP.GET_PRINTER_COUNT();
            for (var i = 0; i < iPrinterCount; i++) {
                var printname = LODOP.GET_PRINTER_NAME(i);
                if (printname.indexOf("Argox") >= 0) { return i; }
            };
        };
        //条码打印预览
        function BarCodePreview() {
            var pindex = CreatePrinterList();
            if (Printform.Piece > 0) {
                var pi = $('#APiece').numberbox('getValue');
                for (var i = 0; i < pi; i++) {
                    CreateBarCodePrintPage(pi, i + 1);
                    if (LODOP.SET_PRINTER_INDEX(pindex)) {
                        LODOP.PRINT();
                        //LODOP.PREVIEW();
                    }
                }
            }
        };
        //创建条码打印格式
        function CreateBarCodePrintPage(total, curindex) {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            var TrafficType = "";
            //            if (Printform.TrafficType == "2") {
            //                TrafficType = "【急件】";
            //            }
            LODOP.PRINT_INITA(0, 0, 320, 200, "");
            LODOP.ADD_PRINT_BARCODE(40, 40, 153, 54, "EAN8", $('#AAwbNo').textbox('getValue'));
            LODOP.ADD_PRINT_TEXT(45, 180, 155, 41, "新路城配");
            LODOP.SET_PRINT_STYLEA(0, "FontName", "微软雅黑");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 20);
            LODOP.ADD_PRINT_LINE(100, 10, 100, 320, 0, 2);
            LODOP.ADD_PRINT_TEXT(105, 35, 85, 42, $('#ADep').textbox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontName", "黑体");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 24);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(105, 205, 84, 44, $('#ADest').textbox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontName", "黑体");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 24);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_LINE(145, 10, 145, 320, 0, 2);
            LODOP.ADD_PRINT_TEXT(150, 202, 60, 30, "件数:");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
            LODOP.ADD_PRINT_TEXT(150, 260, 64, 30, total + "-" + curindex);
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
            LODOP.ADD_PRINT_TEXT(150, 30, 88, 30, "收货人:");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
            LODOP.ADD_PRINT_TEXT(150, 84, 120, 30, $('#AAcceptPeople').combobox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
            LODOP.ADD_PRINT_TEXT(172, 30, 158, 25, "广州:020-62196262");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(172, 175, 158, 25, "海口:0898-66804322");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(195, 30, 158, 25, "南宁:0771-6769169");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(195, 175, 158, 25, "湛江:0759-3173550");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_LINE(120, 120, 120, 186, 0, 1);
            LODOP.ADD_PRINT_TEXT(92, 264, 100, 20, TrafficType);
            LODOP.SET_PRINT_STYLEA(0, "FontName", "微软雅黑");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
            LODOP.SET_PRINT_STYLEA(0, "FontColor", "#FF0000");
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
        };
    </script>
</asp:Content>
