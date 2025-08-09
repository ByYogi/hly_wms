<%@ Page Title="车辆在途跟踪" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CarStatusTrack.aspx.cs" Inherits="Cargo.Arrive.CarStatusTrack" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/Rotate/jQueryRotate.2.2.js" type="text/javascript"></script>

    <script type="text/javascript">
        //页面加载时执行
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
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ContractNum',
                url: null,
                columns: [[
                  { title: '', field: '', checkbox: true, width: '30px' },
                  {
                      title: '合同号', field: 'ContractNum', width: '12%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '车牌号', field: 'TruckNum', width: '11%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  { title: '发车时间', field: 'StartTime', width: '10%', formatter: DateTimeFormatter },
                  { title: '预计(实际)到达时间', field: 'PreArriveTime', width: '10%', formatter: DateTimeFormatter },
                  {
                      title: '出发站', field: 'Dep', width: '6%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '到达站', field: 'Dest', width: '6%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '中转站', field: 'Transit', width: '6%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '司机', field: 'Driver', width: '6%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '手机号码', field: 'DriverCellPhone', width: '6%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '付款方式', field: 'PayMode', width: '6%',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "现金"; } else if (val == "1") { return "<span title='提付'>提付</span>"; } else if (val == "2") { return "<span title='回单'>回单</span>"; } else if (val == "3") { return "<span title='月结'>月结</span>"; } else if (val == "4") { return "<span title='代收款'>代收款</span>"; } else { return ""; }
                      }
                  },
                  {
                      title: '车辆状态', field: 'TruckFlag', width: '5%',
                      formatter: function (val, row, index) { if (val == "0") { return "<span title='在站'>在站</span>"; } else if (val == "1") { return "<span title='出发'>出发</span>"; } else if (val == "2") { return "<span title='在途'>在途</span>"; } else if (val == "3") { return "<span title='到达'>到达</span>"; } else if (val == "4") { return "<span title='结束'>结束</span>"; } else if (val == "5") { return "<span title='关注'>关注</span>"; } else { return ""; } }
                  },
                 { title: '合同预览', field: 'ContractURL', width: '10%', formatter: imgFormatter },
                  { title: '合同上传状态', field: 'ContractStatus', width: '5%', formatter: function (val, row, index) { if (val == "0") { return "<span title='未上传'>未上传</span>"; } else if (val == "1") { return "<span title='已上传'>已上传</span>"; } else { return ""; } } }
                ]],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });
            var value2 = 0
            $("#simg").rotate({ bind: { click: function () { value2 += 90; $(this).rotate({ animateTo: value2 }) } } });
            $("input[name=TruckFlag]").click(function () { showCont(); });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#StartDate').datebox('textbox').bind('focus', function () { $('#StartDate').datebox('showPanel'); });
            $('#EndDate').datebox('textbox').bind('focus', function () { $('#EndDate').datebox('showPanel'); });

            $('#Dest').combobox('textbox').bind('focus', function () { $('#Dest').combobox('showPanel'); });
            $('#TruckFlag').combobox('textbox').bind('focus', function () { $('#TruckFlag').combobox('showPanel'); });
            $('#ATruckNum').combobox('textbox').bind('focus', function () { $('#ATruckNum').combobox('showPanel'); });
            $('#Length').combobox('textbox').bind('focus', function () { $('#Length').combobox('showPanel'); });
            $('#Model').combobox('textbox').bind('focus', function () { $('#Model').combobox('showPanel'); });
            $('#StartTime').combobox('textbox').bind('focus', function () { $('#StartTime').combobox('showPanel'); });
            $('#DestPeople').combobox('textbox').bind('focus', function () { $('#DestPeople').combobox('showPanel'); });
            $('#ADep').combobox('textbox').bind('focus', function () { $('#ADep').combobox('showPanel'); });
            $('#Text1').combobox('textbox').bind('focus', function () { $('#Text1').combobox('showPanel'); });
            $('#Dest').combobox('setValues', '<%=UserInfor.DepCity%>');
            $('#Dest').combobox('disable');

        });
        //图片添加路径  
        function imgFormatter(value, row, index) {
            if ('' != value && null != value) {
                var rvalue = "";
                rvalue += "<img onclick=download(\"" + value + "\") style='width:66px; height:60px;margin-left:3px;' src='../" + value + "' title='点击查看图片'/>";
                return rvalue;
            }
        }
        function download(img) {
            var simg = "../" + img;
            $('#dgViewImg').dialog('open').dialog('setTitle', '预览');
            $("#simg").attr("src", simg);

        }
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../Arrive/ArriveApi.aspx?method=QueryCarStatusTrack';
            $('#dg').datagrid('load', {
                TruckNum: $('#TruckNum').val(),
                ContractNum: $('#ContractNum').val(),
                AwbNo: '',
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                Dest: $("#Dest").combobox('getValue'),
                TruckFlag: $("#TruckFlag").combobox('getValue')
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

    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'">
        <table>
            <tr>
                <td style="text-align: right;">车牌号码:
                </td>
                <td>
                    <input id="TruckNum" class="easyui-textbox" data-options="prompt:'请输入车牌号码'" style="width: 100px">
                </td>
                <td style="text-align: right;">合同号:
                </td>
                <td>
                    <input id="ContractNum" class="easyui-textbox" data-options="prompt:'请输入合同号'" style="width: 100px">
                </td>
                <td style="text-align: right;">到达站:
                </td>
                <td>
                    <input id="Dest" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../Arrive/ArriveApi.aspx?method=QueryAllCity'" />
                </td>
                <td style="text-align: right;">车辆状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="TruckFlag" style="width: 100px;" panelheight="auto" data-options="editable:false">
                        <option value="-1">全部</option>
                        <option value="1">出发</option>
                        <option value="2">在途</option>
                        <option value="3">到达</option>
                        <option value="4">结束</option>
                        <option value="5">关注</option>
                    </select>
                </td>
                <td style="text-align: right;">时间范围:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px">
                </td>
            </tr>
            <tr>
                <td colspan="11">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-edit"
                        plain="false" onclick="edit()">修改车辆跟踪状态</a>
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="dlg" class="easyui-dialog" style="width: 450px; height: 370px; padding: 3px 3px" closed="true" closable="false" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" name="ContractNum" id="AContractNum" />
            <input type="hidden" name="TruckNum" />
            <input type="hidden" name="Dest" id="ADest" />
            <div id="saPanel">
                <table class="mini-toolbar" style="width: 100%;">
                    <tr>
                        <td rowspan="5" style="border-right: 1px solid #909aa6;">车<br />
                            辆<br />
                            在<br />
                            途<br />
                            跟<br />
                            踪
                        </td>
                        <td align="left">&nbsp;&nbsp;状态：
                        </td>
                        <td align="left">
                            <input name="TruckFlag" id="ATruckFlag1" type="radio" checked="checked" value="2"/><label for="ATruckFlag1">途中</label>
                            <input name="TruckFlag" id="ATruckFlag2" type="radio" value="3"/><label for="ATruckFlag2">到达</label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">&nbsp;&nbsp;车辆所在地:
                        </td>
                        <td align="left">
                            <input name="CurrentLocation" id="CurrentLocation" class="easyui-textbox" data-options="prompt:'请输入车辆所在地'">
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
                            <textarea name="DetailInfo" id="DetailInfo" cols="60" style="height: 35px;" class="mini-textarea"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <textarea name="txtDetailInfo" id="txtDetailInfo" readonly="true" cols="60" style="height: 140px;"
                                class="mini-textarea"></textarea>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" id="save" iconcls="icon-ok" onclick="saveItem()">&nbsp;保&nbsp;存&nbsp;</a> <a href="#" class="easyui-linkbutton" iconcls="icon-cancel"
            onclick="closeDlg()">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <div id="dgShowData" class="easyui-dialog" style="width: 80%; height: 90%; padding: 3px 3px"
        closed="true" buttons="#dgShowData-buttons">
        <div id="tabs1" class="easyui-tabs" data-options="fit:true">
            <div title="总收入" data-options="iconCls:'icon-coins'">
                <form id="fmDep" class="easyui-form" method="post">
                    <input name="ContractNum" id="BContractNum" type="hidden" />
                    <div id="saPanel">
                        <table>
                            <tr>
                                <td style="text-align: right;">车牌号码:
                                </td>
                                <td>
                                    <input name="TruckNum" id="ATruckNum" class="easyui-combobox" style="width: 100px;"
                                        data-options="valueField:'TruckNum',textField:'TruckNum',url:'../Arrive/ArriveApi.aspx?method=QueryTruck'" />
                                </td>
                                <td style="text-align: right;">车长:
                                </td>
                                <td>
                                    <input class="easyui-combobox" name="Length" id="Length" data-options="url:'../Data/CarLength.json',method:'get',valueField:'id',textField:'text',panelHeight:'auto'"
                                        style="width: 100px;">
                                </td>
                                <td style="text-align: right;">车型:
                                </td>
                                <td>
                                    <input class="easyui-combobox" name="Model" id="Model" data-options="url:'../Data/CarModel.json',method:'get',valueField:'id',textField:'text',panelHeight:'auto'"
                                        style="width: 100px;">
                                </td>
                                <td style="text-align: right;">发车时间:
                                </td>
                                <td>
                                    <input id="StartTime" name="StartTime" class="easyui-datetimebox" style="width: 150px">
                                </td>
                                <td style="text-align: right;">运行:
                                </td>
                                <td>
                                    <input id="PassTime" name="PassTime" class="easyui-numberbox" style="width: 100px">
                                </td>
                                <td style="text-align: right;">监装人:
                                </td>
                                <td>
                                    <input id="Loader" name="Loader" class="easyui-textbox" style="width: 100px">
                                </td>
                                <td style="text-align: right;">操作员:
                                </td>
                                <td>
                                    <input id="Manifester" readonly="true" name="Manifester" class="easyui-textbox" style="width: 100px">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">司机姓名:
                                </td>
                                <td>
                                    <input id="Driver" name="Driver" class="easyui-textbox" style="width: 100px">
                                </td>
                                <td style="text-align: right;">手机:
                                </td>
                                <td colspan="3">
                                    <input id="DriverCellPhone" name="DriverCellPhone" class="easyui-textbox" style="width: 100px">
                                </td>
                                <td style="text-align: right;">身份证号:
                                </td>
                                <td>
                                    <input id="DriverIDNum" name="DriverIDNum" class="easyui-textbox" style="width: 150px">
                                </td>
                                <td style="text-align: right;">地址:
                                </td>
                                <td colspan="5">
                                    <input id="DriverIDAddress" name="DriverIDAddress" class="easyui-textbox" style="width: 250px">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">联系人:
                                </td>
                                <td>
                                    <input class="easyui-combobox" name="DestPeople" id="DestPeople" data-options="url:'../Arrive/ArriveApi.aspx?method=GetDeptByUnitID',method:'get',valueField:'People',textField:'People'"
                                        style="width: 100px;">
                                </td>
                                <td style="text-align: right;">手机:
                                </td>
                                <td colspan="3">
                                    <input id="DestCellphone" name="DestCellphone" class="easyui-textbox" style="width: 100px">
                                </td>
                                <td style="text-align: right;">卸货地址:
                                </td>
                                <td>
                                    <input id="UnLoadAddress" name="UnLoadAddress" class="easyui-textbox" style="width: 150px">
                                </td>
                                <td style="text-align: right;">起讫:
                                </td>
                                <td colspan="3">
                                    <input id="ADep" name="Dep" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../Arrive/ArriveApi.aspx?method=QueryAllCity'"
                                        panelheight="auto" />→
                            <input id="Text1" name="Dest" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../Arrive/ArriveApi.aspx?method=QueryAllCity'"
                                panelheight="auto" />
                                </td>
                                <td style="text-align: right;">途径:
                                </td>
                                <td>
                                    <input id="Transit" name="Transit" class="easyui-textbox" style="width: 100px;" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">长途运费:
                                </td>
                                <td>
                                    <input id="TransportFee" name="TransportFee" class="easyui-numberbox" data-options="min:0,precision:2"
                                        style="width: 100px">
                                </td>
                                <td style="text-align: right;">预付:
                                </td>
                                <td>
                                    <input id="PrepayFee" name="PrepayFee" class="easyui-numberbox" data-options="min:0,precision:2"
                                        style="width: 100px">
                                </td>
                                <td style="text-align: right;">到付:
                                </td>
                                <td>
                                    <input id="ArriveFee" name="ArriveFee" class="easyui-numberbox" data-options="min:0,precision:2"
                                        style="width: 100px">
                                </td>
                                <td style="text-align: right;">合计:
                                </td>
                                <td colspan="3">
                                    <input id="ATotalNum" name="TotalNum" class="easyui-numberbox" data-options="min:0,precision:0"
                                        style="width: 50px">&nbsp;票&nbsp;&nbsp;
                            <input id="ATotalMoney" name="TotalMoney" class="easyui-numberbox" data-options="min:0,precision:2"
                                style="width: 50px">&nbsp;元
                                </td>
                                <td style="text-align: right;">重量:
                                </td>
                                <td>
                                    <input id="AWeight" name="Weight" class="easyui-numberbox" data-options="min:0,precision:2"
                                        style="width: 100px">
                                </td>
                                <td style="text-align: right;">体积:
                                </td>
                                <td>
                                    <input id="AVolume" name="Volume" class="easyui-numberbox" data-options="min:0,precision:2"
                                        style="width: 100px">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">备注:
                                </td>
                                <td colspan="7">
                                    <textarea name="Remark" id="Remark" cols="60" style="height: 35px;" class="mini-textarea"></textarea>
                                </td>
                            </tr>
                        </table>
                    </div>
                </form>
                <table id="dgDriver" class="easyui-datagrid">
                </table>
            </div>
        </div>
    </div>
    <div id="dgShowData-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false"
            onclick="AwbExport()">&nbsp;导&nbsp;出&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="#"
                class="easyui-linkbutton" iconcls="icon-cancel" onclick="closedgShowData()">&nbsp;关&nbsp;闭&nbsp;</a>
        <form runat="server" id="fm1">
            <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
        </form>
    </div>
    <div id="dgViewImg" class="easyui-dialog" closed="true" style="width: 1000px; height: 500px;">
        <img id="simg" />
    </div>
    <script type="text/javascript">
        //查看合同信息
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dgShowData').dialog('open').dialog('setTitle', '查看司机合同 ' + row.ContractNum);
                $('#fmDep').form('clear');
                row.StartTime = AllDateTime(row.StartTime);
                $('#fmDep').form('load', row);
                showDg();
                var gridOpts = $('#dgDriver').datagrid('options');
                gridOpts.url = '../Arrive/ArriveApi.aspx?method=QueryAwbByContractNum&ContractNum=' + row.ContractNum;
            }
        }
        //导出数据
        function AwbExport() {
            var row = $("#dgDriver").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            $.ajax({
                url: "../Arrive/ArriveApi.aspx?method=QueryAwbByContractNumForExport&key=" + escape($('#BContractNum').val()),
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
        //显示DG
        function showDg() {
            $('#dgDriver').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                rownumbers: true,
                idField: 'AwbID',
                url: null,
                columns: [[
                  {
                      title: '运单号', field: 'AwbNo', width: '10%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  { title: '开单日期', field: 'HandleTime', width: '10%', formatter: DateFormatter },
                  {
                      title: '客户名称', field: 'ShipperUnit', width: '10%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '出发站', field: 'Dep', width: '5%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '中间站', field: 'MidDest', width: '5%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '到达站', field: 'Dest', width: '5%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '中转站', field: 'Transit', width: '5%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '品名', field: 'Goods', width: '10%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '总费用', field: 'TotalCharge', width: '5%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '件数', field: 'Piece', width: '5%', formatter: function (value, row) {
                          if (value != null && value != "") {
                              if (row.AwbPiece != value) {
                                  return "<span title='" + row.AwbPiece + "/" + value + "'>" + row.AwbPiece + "/" + value + "</span>";
                              } else {
                                  return "<span title='" + value + "'>" + value + "</span>";
                              }
                          }
                      }
                  },
                  {
                      title: '重量', field: 'AwbWeight', width: '5%', formatter: function (value, row) {
                          if (value != null && value != "") {
                              if (row.AwbWeight != value) {
                                  return "<span title='" + row.AwbWeight + "/" + value + "'>" + row.AwbWeight + "/" + value + "</span>";
                              } else {
                                  return "<span title='" + value + "'>" + value + "</span>";
                              }
                          }
                      }
                  },
                  {
                      title: '体积', field: 'AwbVolume', width: '5%', formatter: function (value, row) {
                          if (value != null && value != "") {
                              if (row.AwbVolume != value) {
                                  return "<span title='" + row.AwbVolume + "/" + value + "'>" + row.AwbVolume + "/" + value + "</span>";
                              } else {
                                  return "<span title='" + value + "'>" + value + "</span>";
                              }
                          }
                      }
                  },
                  {
                      title: '附加信息', field: 'Remark', width: '10%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  }
                ]],
                onLoadSuccess: function (data) {
                    var tN = 0, tM = 0;
                    for (var i = 0; i < data.rows.length; i++) {
                        tM += data.rows[i].SumCharge;
                    }
                    tN = data.total;
                    $('#ATotalNum').numberbox('setValue', Number(tN));
                    $('#ATotalMoney').numberbox('setValue', Number(tM).toFixed(2));
                }
            });
        }
        //关闭
        function closedgShowData() {
            $('#dgShowData').dialog('close');
        }
        //关闭
        function closeDlg() {
            $('#dlg').dialog('close');
            $('#dg').datagrid('reload');
        }
        function showCont() {
            switch ($("input[name=TruckFlag]:checked").attr("id")) {
                case "ATruckFlag2": $("#CurrentLocation").textbox('setValue', $('#ADest').val()); break;
                default: $("#CurrentLocation").textbox('setValue', ''); break;
            }
        }
        //修改
        function edit() {
            var row = $('#dg').datagrid('getSelections');
            if (row == null || row == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning'); return; }
            if (row.length > 1) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择一条数据！', 'warning'); return; }
            if (row) {
                if (row[0].TruckFlag == "0") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', row[0].ContractNum + '状态在站，请重新查询后再修改', 'warning');
                    return;
                }
                if (row[0].TruckFlag == "4") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', row[0].ContractNum + '状态已结束，不能修改', 'warning');
                    return;
                }
                if (row[0].TruckFlag == "3") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', row[0].ContractNum + '已经到达，不能修改', 'warning');
                    return;
                }
                var re = true;
                $.ajax({
                    async: false,
                    url: "../Arrive/ArriveApi.aspx?method=QueryDepManifestByNum&id=" + row[0].ContractNum,
                    cache: false,
                    success: function (o) {
                        if (o.TruckFlag == "0") {
                            re = false;
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', row[0].ContractNum + '状态在站，请重新查询后再修改', 'warning');
                            return;
                        }
                        if (o.TruckFlag == "4") {
                            re = false;
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', row[0].ContractNum + '状态已结束，不能修改', 'warning');
                            return;
                        }
                        if (o.TruckFlag == "3") {
                            re = false;
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', row[0].ContractNum + '已经到达，不能修改', 'warning');
                            return;
                        }
                    }
                });
                if (re) {
                    $('#dlg').dialog('open').dialog('setTitle', '修改合同号：' + row[0].ContractNum + '跟踪状态');
                    $('#fm').form('clear');
                    $('#fm').form('load', row[0]);
                    $.ajax({
                        async: false,
                        url: "../Arrive/ArriveApi.aspx?method=QueryTruckStatusTrack&id=" + row[0].ContractNum,
                        cache: false,
                        success: function (text) { $('#txtDetailInfo').val(text); }
                    });
                }
            }
        }
        //保存
        function saveItem() {
            $('#fm').form('submit', {
                url: '../Arrive/ArriveApi.aspx?method=SaveTruckStatusTrack',
                onSubmit: function () {
                    var trd = $(this).form('enableValidation').form('validate');
                    if (trd) { $('#save').linkbutton('disable'); }
                    return trd;
                },
                success: function (msg) {
                    $('#save').linkbutton('enable');
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '更新成功!', 'info');
                        $.ajax({
                            async: false,
                            url: "../Arrive/ArriveApi.aspx?method=QueryTruckStatusTrack&id=" + escape($('#AContractNum').val()),
                            cache: false,
                            success: function (text) { $('#txtDetailInfo').val(text); }
                        });
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }

    </script>

</asp:Content>


