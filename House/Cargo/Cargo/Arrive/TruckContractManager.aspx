<%@ Page Title="车辆合同管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TruckContractManager.aspx.cs" Inherits="Cargo.Arrive.TruckContractManager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../JS/Lodop/LodopFuncs.js" type="text/javascript"></script>

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
                idField: 'ContractNum',
                url: null,
                columns: [[
                  { title: '', field: '', checkbox: true, width: '30px' },
                  {
                      title: '合同号', field: 'ContractNum', width: '90px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '车牌号', field: 'TruckNum', width: '70px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '司机', field: 'Driver', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '手机号码', field: 'DriverCellPhone', width: '90px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  { title: '发车时间', field: 'StartTime', width: '130px', formatter: DateTimeFormatter },
                  {
                      title: '起讫', field: 'Range', width: '100px', formatter: function (value) {
                          if (value != null && value != "") {
                              var strs = value.split("/");
                              return "<span title='" + strs[0] + "→" + strs[1] + "'>" + strs[0] + "→" + strs[1] + "</span>";
                          }
                      }
                  },
                  {
                      title: '重量', field: 'Weight', width: '50px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '体积', field: 'Volume', width: '50px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '直达体积', field: 'noStopVolume', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '中转体积', field: 'transitVolume', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '直达收入', field: 'noStopFee', width: '60px', align: 'right', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '中转收入', field: 'tranistFee', width: '60px', align: 'right', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '运输费用', field: 'TransportFee', width: '60px', align: 'right', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '预付', field: 'PrepayFee', width: '60px', align: 'right', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '到付', field: 'ArriveFee', width: '60px', align: 'right', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '付款方式', field: 'PayMode', width: '60px',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "<span title='现金'>现金</span>"; } else if (val == "1") { return "<span title='提付'>提付</span>"; } else if (val == "2") { return "<span title='回单'>回单</span>"; } else if (val == "3") { return "<span title='月结'>月结</span>"; } else if (val == "4") { return "<span title='代收款'>代收款</span>"; } else { return ""; }
                      }
                  },
                  {
                      title: '调车员', field: 'Dispatcher', width: '50px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '监装人', field: 'Loader', width: '50px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '配载员', field: 'Manifester', width: '50px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '备注', field: 'Remark', width: '150px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                   { title: '合同预览', field: 'ContractURL', width: '100px', formatter: imgFormatter },
                  { title: '合同上传状态', field: 'ContractStatus', width: '80px', formatter: function (val, row, index) { if (val == "0") { return "<span title='未上传'>未上传</span>"; } else if (val == "1") { return "<span title='已上传'>已上传</span>"; } else { return ""; } } },
                  {
                      title: '审核状态', field: 'DelFlag', width: '60px',
                      formatter: function (val, row, index) { if (val == "0") { return "<span title='未审'>未审</span>"; } else if (val == "1") { return "<span title='已审'>已审</span>"; } else if (val == "2") { return "<span title='拒审'>拒审</span>"; } else { return ""; } }
                  },
                  {
                      title: '合同状态', field: 'CancelFlag', width: '60px',
                      formatter: function (val, row, index) { if (val == "0") { return "<span title='正常'>正常</span>"; } else if (val == "1") { return "<span title='作废'>作废</span>"; } else { return ""; } }
                  },
                  {
                      title: '车辆状态', field: 'TruckFlag', width: '60px',
                      formatter: function (val, row, index) { if (val == "0") { return "<span title='在站'>在站</span>"; } else if (val == "1") { return "<span title='出发'>出发</span>"; } else if (val == "2") { return "<span title='在途'>在途</span>"; } else if (val == "3") { return "<span title='到达'>到达</span>"; } else if (val == "4") { return "<span title='结束'>结束</span>"; } else if (val == "5") { return "<span title='关注'>关注</span>"; } else { return ""; } }
                  }
                ]],
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { edit(index); }
            });
            var value2 = 0
            $("#simg").rotate({ bind: { click: function () { value2 += 90; $(this).rotate({ animateTo: value2 }) } } });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            var bt = "<%=UserInfor.NewLandBelongSystem %>";
            if (bt == "DR") {
                $('#ArrivePay').text("回付");
            } else {
                $('#ArrivePay').text("到付");
            }
            $('#StartDate').datetimebox('textbox').bind('focus', function () { $('#StartDate').datetimebox('showPanel'); });
            $('#EndDate').datetimebox('textbox').bind('focus', function () { $('#EndDate').datetimebox('showPanel'); });
            $('#Dep').combobox('textbox').bind('focus', function () { $('#Dep').combobox('showPanel'); });
            $('#Dest').combobox('textbox').bind('focus', function () { $('#Dest').combobox('showPanel'); });
            $('#dFlag').combobox('textbox').bind('focus', function () { $('#dFlag').combobox('showPanel'); });
            $('#eFlag').combobox('textbox').bind('focus', function () { $('#eFlag').combobox('showPanel'); });
            $('#ATruckNum').combobox('textbox').bind('focus', function () { $('#ATruckNum').combobox('showPanel'); });
            $('#Length').combobox('textbox').bind('focus', function () { $('#Length').combobox('showPanel'); });
            $('#Model').combobox('textbox').bind('focus', function () { $('#Model').combobox('showPanel'); });
            $('#StartTime').combobox('textbox').bind('focus', function () { $('#StartTime').combobox('showPanel'); });
            $('#CardName').combobox('textbox').bind('focus', function () { $('#CardName').combobox('showPanel'); });
            $('#Dep').combobox('setValues', '<%=UserInfor.DepCity%>');
            $('#Dep').combobox('disable');
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
            gridOpts.url = '../Arrive/ArriveApi.aspx?method=QueryDepManifest';
            $('#dg').datagrid('load', {
                TruckNum: $('#TruckNum').val(),
                ContractNum: $('#ContractNum').val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                dFlag: $("#dFlag").combobox('getValue'),
                Dep: $("#Dep").combobox('getValue'),
                Dest: $("#Dest").combobox('getValue'),
                eFlag: $("#eFlag").combobox('getValue')
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
                <td style="text-align: right;">时间范围:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px">
                </td>
                <td style="text-align: right;">出发站:
                </td>
                <td>
                    <input id="Dep" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../Arrive/ArriveApi.aspx?method=QueryAllCity'" panelheight="auto" />
                </td>
                <td style="text-align: right;">审核状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="dFlag" style="width: 100px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">未审</option>
                        <option value="1">已审</option>
                        <option value="2">拒审</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="edit()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-lock_add" plain="false" onclick="plcheck()">&nbsp;批量审核&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-lock_open" plain="false" onclick="plDeny()">&nbsp;批量未审&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-exclamation" plain="false" onclick="cut()">&nbsp;作&nbsp;废&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-arrow_refresh" plain="false" onclick="renew()">&nbsp;启&nbsp;用&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="AwbExport()">&nbsp;导&nbsp;出</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-print" onclick="showPrint()">&nbsp;打印付款申请</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-monitor_go" onclick="uploadHLY()">&nbsp;上传粤东线配载合同</a>
                    <form runat="server" id="fm1">
                        <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
                        <asp:Button ID="Button1" runat="server" Style="display: none;" Text="导出" OnClick="Button1_Click" />
                    </form>
                </td>
                <td style="text-align: right;">到达站:
                </td>
                <td>
                    <input id="Dest" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../Arrive/ArriveApi.aspx?method=QueryAllCity'" />
                </td>
                <td style="text-align: right;">合同状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="eFlag" style="width: 100px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">正常</option>
                        <option value="1">作废</option>
                    </select>
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="dlg" class="easyui-dialog saPanel" style="width: 80%; height: 70%" closed="true" closable="false" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" name="ContractNum" id="AContractNum" />
            <input type="hidden" name="DelFlag" />
            <input type="hidden" name="CancelFlag" />
            <input name="DepCellPhone" id="DepCellPhone" type="hidden" />
            <div id="saPanel" name="saPanel1">
                <table>
                    <tr>
                        <td style="text-align: right;">车牌号码:
                        </td>
                        <td>
                            <input name="TruckNum" id="ATruckNum" class="easyui-combobox" style="width: 100px;"
                                data-options="valueField:'TruckNum',textField:'TruckNum',url:'../Arrive/ArriveApi.aspx?method=QueryTruck',onSelect:truckChange,required:true" />
                        </td>
                        <td style="text-align: right;">车长:
                        </td>
                        <td>
                            <input class="easyui-combobox" name="Length" id="Length" data-options="url:'../Data/CarLength.json',method:'get',valueField:'id',textField:'text',panelHeight:'auto',required:true"
                                style="width: 100px;">
                        </td>
                        <td style="text-align: right;">车型:
                        </td>
                        <td>
                            <input class="easyui-combobox" name="Model" id="Model" data-options="url:'../Data/CarModel.json',method:'get',valueField:'id',textField:'text',panelHeight:'auto',required:true"
                                style="width: 100px;">
                        </td>
                        <td style="text-align: right;">发车时间:
                        </td>
                        <td>
                            <input id="StartTime" name="StartTime" class="easyui-datetimebox" data-options="required:true"
                                style="width: 150px">
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
                        <td>
                            <input id="DriverCellPhone" name="DriverCellPhone" class="easyui-textbox" style="width: 100px">
                        </td>
                        <td style="text-align: right;">运行:
                        </td>
                        <td>
                            <input id="PassTime" name="PassTime" class="easyui-numberbox" style="width: 100px">
                        </td>
                        <td style="text-align: right;">身份证号:
                        </td>
                        <td>
                            <input id="DriverIDNum" name="DriverIDNum" class="easyui-textbox" style="width: 150px">
                        </td>
                        <td style="text-align: right;">地址:
                        </td>
                        <td colspan="5">
                            <input id="DriverIDAddress" name="DriverIDAddress" class="easyui-textbox" style="width: 100%">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">联系人:
                        </td>
                        <td>
                            <input class="easyui-combobox" name="DestPeople" id="DestPeople" data-options="url:'../Arrive/ArriveApi.aspx?method=GetDeptByUnitID',method:'get',valueField:'Person',textField:'Person',onSelect:onDestPeopleChanged,editable:false" style="width: 100px;">
                        </td>
                        <td style="text-align: right;">手机:
                        </td>
                        <td>
                            <input id="DestCellphone" name="DestCellphone" class="easyui-textbox" style="width: 100px">
                        </td>
                        <td style="text-align: right;">途径:
                        </td>
                        <td>
                            <input id="Transit" name="Transit" class="easyui-textbox" style="width: 100px;" />
                        </td>
                        <td style="text-align: right;">卸货地址:
                        </td>
                        <td>
                            <input id="UnLoadAddress" name="UnLoadAddress" class="easyui-textbox" style="width: 150px">
                        </td>
                        <td style="text-align: right;">起讫:
                        </td>
                        <td colspan="3">
                            <input id="ADep" name="Dep" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../Arrive/ArriveApi.aspx?method=QueryAllCity',onSelect:depSetValue" />&nbsp;→&nbsp;
                    <input id="ADest" name="Dest" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../Arrive/ArriveApi.aspx?method=QueryAllCity'" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">运费:
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
                        <td style="text-align: right;" id="ArrivePay">到付:
                        </td>
                        <td>
                            <input id="ArriveFee" name="ArriveFee" class="easyui-numberbox" data-options="min:0,precision:2"
                                style="width: 100px">
                        </td>
                        <td style="text-align: right;">合计:
                        </td>
                        <td>
                            <input id="ATotalNum" name="TotalNum" class="easyui-numberbox" data-options="min:0,precision:0"
                                style="width: 50px">票
                    <input id="ATotalMoney" name="TotalMoney" class="easyui-numberbox" data-options="min:0,precision:2"
                        style="width: 80px">元
                        </td>
                        <td style="text-align: right;">重量:
                        </td>
                        <td>
                            <input id="AWeight" name="Weight" class="easyui-numberbox" data-options="min:0,precision:2"
                                style="width: 100px">
                        </td>
                        <td style="text-align: right;">调车员:
                        </td>
                        <td>
                            <input id="Dispatcher" name="Dispatcher" class="easyui-textbox" style="width: 100px">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">户名:
                        </td>
                        <td>
                            <input class="easyui-combobox" name="CardName" id="CardName" data-options="valueField:'CardName',textField:'CardName',panelHeight:'auto',onSelect:onCardNameChanged"
                                style="width: 100px;">
                        </td>
                        <td style="text-align: right;">户行:
                        </td>
                        <td colspan="3">
                            <input id="CardBank" name="CardBank" class="easyui-textbox" style="width: 100%">
                        </td>
                        <td style="text-align: right;">卡号:
                        </td>
                        <td>
                            <input id="CardNum" name="CardNum" class="easyui-textbox" style="width: 150px">
                        </td>
                        <td style="text-align: right;">体积:
                        </td>
                        <td>
                            <input id="AVolume" name="Volume" class="easyui-numberbox" data-options="min:0,precision:2"
                                style="width: 100px">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">备注:
                        </td>
                        <td colspan="11">
                            <textarea name="Remark" id="Remark" cols="70" style="height: 35px; width: 99%; resize: none" class="mini-textarea"></textarea>
                        </td>
                    </tr>
                </table>
            </div>
            <table>
                <tr>
                    <td>
                        <table id="dgDriver" class="easyui-datagrid">
                        </table>
                    </td>
                    <td>
                        <table id="dgManifest" class="easyui-datagrid">
                        </table>
                    </td>
                </tr>
            </table>
            <div id="toolbar">
                运单号:
            <input id="QAwbNo" class="easyui-textbox" data-options="prompt:'请输入运单号'" style="width: 90px">
                <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="awbQuery()">查询</a>
            </div>
        </form>
    </div>
    <div id="dlg-buttons" name="saPanel2">
        <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="cAwbExport()">&nbsp;导出配载明细表&nbsp;</a>&nbsp;&nbsp; 
        <a href="#" class="easyui-linkbutton" iconcls="icon-basket_remove" plain="false" onclick="pldown()" id="down">&nbsp;拉下运单&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-basket_put" plain="false" onclick="plup()" id="up">&nbsp;拉上运单&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="SaveManifest()" id="save">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-print" onclick="PrePrint()" id="print">&nbsp;打印配载交接表&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-print" onclick="ManifestPrint()" id="printManifest">&nbsp;打印运输合同&nbsp;</a>&nbsp;&nbsp; 
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closeDlg()">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <object id="LODOP_OB" title="dd" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA"
        width="0" height="0">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0" height="0"></embed>
    </object>
    <div id="dlgAwbDetail" class="easyui-dialog" style="width: 760px; height: 400px;"
        closed="true" buttons="#dlgAwbDetail-buttons">
        <table style="width: 100%">
            <tr>
                <td style="text-align: center; font-size: 22px;">
                    <%=UserInfor.NewLandBelongSystemName%>物流有限公司付款申请书
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">申请日期：
                    <input id="NowDate" class="easyui-datebox" style="width: 100px">
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>收款单位:
                </td>
                <td>
                    <input id="PAcceptUnit" class="easyui-textbox" style="width: 300px">
                </td>
                <td>对应单号:
                </td>
                <td>
                    <input id="PAwbNo" class="easyui-textbox" style="width: 300px;" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>户名:
                </td>
                <td>
                    <input id="PCardName" class="easyui-textbox" style="width: 100px">
                </td>
                <td>开户行:
                </td>
                <td>
                    <input id="PCardBank" class="easyui-textbox" style="width: 250px">
                </td>
                <td>账号:
                </td>
                <td>
                    <input id="PCardNum" class="easyui-textbox" style="width: 200px">
                </td>
            </tr>
            <tr>
                <td>应付费用:
                </td>
                <td>
                    <input id="PYingFu" class="easyui-numberbox" data-options="min:0,precision:2" style="width: 100px;" />
                </td>
                <td>大写:
                </td>
                <td>
                    <input id="PYDaXie" class="easyui-textbox" style="width: 250px">
                </td>
                <td>分批支付:
                </td>
                <td>
                    <input name="PFenPi" type="radio" id="dlt0" value="是" /><label for="dlt0" style="font-size: 15px;">是</label>
                    <input name="PFenPi" type="radio" id="dlt1" value="否" /><label for="dlt1" style="font-size: 15px;">否</label>
                </td>
            </tr>
            <tr>
                <td>实际付款:
                </td>
                <td>
                    <input id="PShiJi" class="easyui-numberbox" data-options="min:0,precision:2" style="width: 100px;" />
                </td>
                <td>大写:
                </td>
                <td>
                    <input id="PSDaXie" class="easyui-textbox" style="width: 250px">
                </td>
                <td>手机号码:
                </td>
                <td>
                    <input id="PCellphone" class="easyui-textbox" style="width: 150px">
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>付款原因:
                </td>
                <td colspan="4">
                    <textarea id="PFReason" rows="5" style="width: 600px;"></textarea>
                </td>
            </tr>
        </table>
    </div>
    <div id="dlgAwbDetail-buttons">
        <table>
            <tr>
                <td style="text-align: left; width: 40%"></td>
                <td style="text-align: right; width: 50%">
                    <a href="#" class="easyui-linkbutton" id="payprint" iconcls="icon-printer" onclick="PayPrint()">&nbsp;打&nbsp;印&nbsp;</a>&nbsp;&nbsp; <a href="#" class="easyui-linkbutton" iconcls="icon-cancel"
                        onclick="javascript:$('#dlgAwbDetail').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    <div id="dgViewImg" class="easyui-dialog" closed="true" style="width: 1000px; height: 500px;">
        <img id="simg" />
    </div>

    <script type="text/javascript">
        //上传粤东线配载合同
        function uploadHLY() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要上传的数据！', 'warning'); return; }
            for (var i = 0; i < rows.length; i++) { rows[i].ContractURL = ''; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '将粤东线配载合同上传好来运系统，确定上传？？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: '../Arrive/ArriveApi.aspx?method=uploadHLYDepmanifest',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '上传成功!', 'info');
                                //$('#dg').datagrid('reload');
                            }
                            else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning'); }
                        }
                    });
                }
            });
        }
        //导出数据
        function cAwbExport() {
            var row = $("#dgDriver").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            $.ajax({
                url: "../Arrive/ArriveApi.aspx?method=QueryAwbInfoByContractNumForExport&key=" + escape($('#AContractNum').val()),
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=Button1.ClientID %>"); obj.click(); }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }
        //导出数据
        function AwbExport() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            var key = new Array();
            key[0] = $('#TruckNum').val();
            key[1] = $('#ContractNum').val();
            key[2] = $('#StartDate').datebox('getValue');
            key[3] = $('#EndDate').datebox('getValue');
            key[4] = $("#dFlag").combobox('getValue');
            key[5] = $("#Dest").combobox('getValue');
            key[6] = $("#eFlag").combobox('getValue');

            $.ajax({
                url: "../Arrive/ArriveApi.aspx?method=QueryDepManifestForExport&key=" + escape(key.toString()),
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
        //绑定费用框
        function bindMethod() {
            $("#PYingFu").numberbox({ "onChange": function (newValue, oldValue) { $('#PYDaXie').textbox('setValue', atoc(Number(newValue))); } });
            $("#PShiJi").numberbox({ "onChange": function (newValue, oldValue) { $('#PSDaXie').textbox('setValue', atoc(Number(newValue))); } });
        }
        //显示打印付款申请书
        function showPrint() {
            bindMethod();
            $('#PFReason').val('');
            $('input:radio[name=PFenPi]').attr('checked', false);
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要打印的数据！', 'warning'); return; }
            $('#dlgAwbDetail').dialog('open').dialog('setTitle', '打印付款申请书');
            var datenow = new Date();
            $('#NowDate').datebox('setValue', getNowFormatDate(datenow));
            var tn = ""; var dn = ""; var cn = ""; var cardN = ""; var cardB = ""; var TFee = 0; var contractN = ""; var cell = "";
            for (var i = 0, l = rows.length; i < l; i++) {
                if (i == 0) { tn = rows[i].TruckNum; dn = rows[i].Driver; cn = rows[i].CardNum; cardN = rows[i].CardName; cardB = rows[i].CardBank; cell = rows[i].DriverCellPhone }
                else {
                    // if (tn != rows[i].TruckNum) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '车牌号不同' + rows[i].TruckNum, 'warning'); return; }
                    //if (dn != rows[i].Driver) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '司机姓名不同' + rows[i].TruckNum, 'warning'); return; }
                    if (cn != rows[i].CardNum) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '银行卡号不同' + rows[i].TruckNum, 'warning'); return; }
                }
                TFee += Number(rows[i].TransportFee);
                contractN += rows[i].ContractNum + ",";
            }
            $('#PAcceptUnit').textbox('setValue', tn.replace(/^\s+|\s+$/g, "") + " " + dn);
            $('#PAwbNo').textbox('setValue', contractN);
            $('#PCardName').textbox('setValue', cardN);
            $('#PCardBank').textbox('setValue', cardB);
            $('#PCardNum').textbox('setValue', cn);
            $('#PYingFu').textbox('setValue', TFee);
            $('#PYDaXie').textbox('setValue', atoc(TFee));
            $('#PShiJi').textbox('setValue', TFee);
            $('#PSDaXie').textbox('setValue', atoc(TFee));
            $('#PCellphone').textbox('setValue', cell);
        }
        //司机银行卡选择方法
        function onCardNameChanged(item) {
            if (item) {
                $('#CardBank').textbox('setValue', item.CardBank);
                $('#CardNum').textbox('setValue', item.CardNum);
            }
        }
        function awbQuery() {
            var Dest = "<%=UserInfor.DepCity %>";
            var mOpts = $('#dgManifest').datagrid('options');
            mOpts.url = '../Arrive/ArriveApi.aspx?method=QueryAwbFromUpdate';
            $('#dgManifest').datagrid('load', {
                AwbNo: $('#QAwbNo').val(),
                Dep: Dest,
                Dest: ''
            });
        }
        //格式化
        function onRange(val, row, index) { var rg; var res = val; var strs = res.split("/"); rg = strs[0] + "→" + strs[1]; return rg; }
        function closeDlg() {
            $('#dlg').dialog('close');
            $('#dg').datagrid('reload');
        }
        //修改
        function edit() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (rows.length > 1) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择一条要修改的数据！', 'warning');
                return;
            }
            var row = rows[0];
            if (row) {
                if (row.CancelFlag == "1") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '作废订单无法修改！', 'warning');
                    return;
                }
                $('#dgManifest').datagrid('loadData', []);
                $('#dlg').dialog('open').dialog('setTitle', '修改车辆合同' + row.ContractNum);
                var height = Number($("#dlg").height()) - $("div[name='saPanel1']").outerHeight(true) - $("div[name='saPanel2']").outerHeight(true);
                $('#dgDriver').datagrid({ height: height });
                $('#dgManifest').datagrid({ height: height });
                $('#fm').form('clear');
                $('#CardName').combobox('clear');
                var url = '../Arrive/ArriveApi.aspx?method=GetDriverCardByTruckNum&id=' + row.TruckNum;
                $('#CardName').combobox('reload', url);
                row.StartTime = AllDateTime(row.StartTime);
                $('#fm').form('load', row);
                $('#ADep').combobox('readonly');
                $('#ADest').combobox('readonly');
                $('#ATotalMoney').numberbox('setValue', Number(row.TotalFee).toFixed(2));
                if (row.DelFlag == "1") {
                    $('#down').linkbutton('disable');
                    $('#up').linkbutton('disable');
                    $('#save').linkbutton('disable');
                }
                else {
                    $('#down').linkbutton('enable');
                    $('#up').linkbutton('enable');
                    $('#save').linkbutton('enable');
                }
                if (row.DelFlag == "2") { $('#StartTime').datetimebox('readonly'); } else { $('#StartTime').datetimebox('readonly', false); }
                showGrid();
                var gridOpts = $('#dgDriver').datagrid('options');
                gridOpts.url = '../Arrive/ArriveApi.aspx?method=QueryAwbByContractNum&ContractNum=' + row.ContractNum;
                //<%--                var Dep = "<%=CityCode %>";--%>
                //                var mOpts = $('#dgManifest').datagrid('options');
                //                mOpts.url = '../SystemManager/systemApi.aspx?method=QueryAwbFromUpdate&AwbNo=&Dest=&Dep=' + Dep;
            }
        }
        //显示列表
        function showGrid() {
            $('#dgDriver').datagrid({
                width: Number($("#dlg").width()) * 0.5 - 5,
                title: '配载运单', //标题内容
                rownumbers: true,
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'AwbID',
                url: null,
                columns: [[
                  { title: '', field: 'AwbID', checkbox: true, width: '30px' },
                  {
                      title: '运单号', field: 'AwbNo', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  { title: '开单日期', field: 'HandleTime', width: '75px', formatter: DateFormatter },
                  {
                      title: '最迟时效', field: 'LatestTimeLimit', width: '75px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '发站', field: 'Dep', width: '55px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '到站', field: 'Dest', width: '55px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '中转', field: 'Transit', width: '55px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '客户名称', field: 'ShipperUnit', width: '70px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '品名', field: 'Goods', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '总运费', field: 'TotalCharge', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '件数', field: 'Piece', width: '60px', formatter: function (value, row) {
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
                      title: '重量', field: 'Weight', width: '60px', formatter: function (value, row) {
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
                      title: '体积', field: 'Volume', width: '60px', formatter: function (value, row) {
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
                      title: '备注', field: 'Remark', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  }
                ]],
                onLoadSuccess: function (data) {
                    var tN = 0, tM = 0;
                    //                    for (var i = 0; i < data.rows.length; i++) {
                    //                        tM += data.rows[i].SumCharge;
                    //                    }
                    tN = data.total;
                    $('#ATotalNum').numberbox('setValue', Number(tN));
                },
                onDblClickRow: function (index, row) {
                    down(index, row);
                },
                rowStyler: function (index, row) {
                    if (row.TrafficType == "2") { return "font-weight:bold;"; }
                    if (row.AwbPiece != row.Piece) { return "background-color:#EBEDE1"; }
                }
            });
            $('#dgManifest').datagrid({
                width: Number($("#dlg").width()) * 0.5 - 5,
                title: '在站运单', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: true, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'AwbID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                  { title: '', field: 'AwbID', checkbox: true, width: '30px' },
                  {
                      title: '运单号', field: 'AwbNo', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  { title: '开单日期', field: 'HandleTime', width: '75px', formatter: DateFormatter },
                  {
                      title: '最迟时效', field: 'LatestTimeLimit', width: '75px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '发站', field: 'Dep', width: '55px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '到站', field: 'Dest', width: '55px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '中转', field: 'Transit', width: '55px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '客户名称', field: 'ShipperUnit', width: '70px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '品名', field: 'Goods', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '总运费', field: 'TotalCharge', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '件数', field: 'Piece', width: '60px', formatter: function (value, row) {
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
                      title: '重量', field: 'Weight', width: '60px', formatter: function (value, row) {
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
                      title: '体积', field: 'Volume', width: '60px', formatter: function (value, row) {
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
                      title: '备注', field: 'Remark', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  }
                ]],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) {
                    up(index, row);
                },
                rowStyler: function (index, row) {
                    if (row.TrafficType == "2") { return "font-weight:bold;"; }
                    if (row.AwbPiece != row.Piece) { return "background-color:#EBEDE1"; }
                }
            });

            //设置分页控件
            var p = $('#dgManifest').datagrid('getPager');
            $(p).pagination({
                showPageList: false,
                pageSize: 10, //每页显示的记录条数，默认为10 
                pageList: [10], //可以设置每页记录条数的列表 
                beforePageText: '第', //页数文本框前显示的汉字 
                afterPageText: '页    共 {pages} 页'
            });
        }
        //批量审核
        function plcheck() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要审核的数据！', 'warning'); return; }
            for (var i = 0; i < rows.length; i++) {
                rows[i].ContractURL = '';
                if (rows[i].DelFlag == "1") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '所选合同中' + rows[i].ContractNum + '已审核！', 'warning'); return;
                }
                if (rows[i].CancelFlag == "1") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '所选合同中' + rows[i].ContractNum + '已作废！', 'warning'); return;
                }
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定审核？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: '../Arrive/ArriveApi.aspx?method=plCheckManifest',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '审核成功!', 'info');
                                $('#dg').datagrid('reload');
                            }
                            else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning'); }
                        }
                    });
                }
            });
        }
        //批量未审
        function plDeny() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要未审的数据！', 'warning'); return; }
            for (var i = 0; i < rows.length; i++) {
                rows[i].ContractURL = '';
                if (rows[i].DelFlag == "0") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '所选合同中' + rows[i].ContractNum + '未审核！', 'warning'); return;
                }
                if (rows[i].CancelFlag == "1") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '所选合同中' + rows[i].ContractNum + '已作废！', 'warning'); return;
                }
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定修改为未审？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: '../Arrive/ArriveApi.aspx?method=plunCheckManifest',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '未审成功!', 'info');
                                $('#dg').datagrid('reload');
                            }
                            else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning'); }
                        }
                    });
                }
            });
        }
        //作废
        function cut() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要作废的数据！', 'warning'); return; }
            for (var i = 0; i < rows.length; i++) {
                rows[i].ContractURL = '';
                if (rows[i].CancelFlag == "1") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '所选合同中' + rows[i].ContractNum + '已作废！', 'warning'); return;
                }
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '合同中运单将修改为在站状态，确定作废？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: '../Arrive/ArriveApi.aspx?method=DelManifest',
                        type: 'post',
                        dataType: 'json',
                        data: { data: json },
                        success: function (text) {
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '作废成功!', 'info');
                                $('#dg').datagrid('reload');
                            }
                            else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning'); }
                        }
                    });
                }
            });
        }
        //启用
        function renew() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要启用的数据！', 'warning'); return; }
            for (var i = 0; i < rows.length; i++) {
                rows[i].ContractURL = '';
                if (rows[i].CancelFlag == "0") {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '所选合同中' + rows[i].ContractNum + '已启用！', 'warning'); return;
                }
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '要重新修改拉上运单，确定启用？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: '../Arrive/ArriveApi.aspx?method=RenewManifest',
                        type: 'post',
                        dataType: 'json',
                        data: { data: json },
                        success: function (text) {
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '启用成功!', 'info');
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
        //保存配载
        function SaveManifest() {
            var row = $('#dgDriver').datagrid('getRows');
            if (row.length <= 0) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '配载列表中没有数据', 'warning'); return; }
            var tp = $('#TransportFee').numberbox('getText');
            if (tp == "" || tp <= 0) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '长途运费不能小于0', 'warning'); return;
            }
            var pf = $('#PrepayFee').numberbox('getText');
            var af = $('#ArriveFee').numberbox('getText');
            if (pf == "") { pf = 0; }
            if (af == "") { af = 0; }
            if (Number(tp) != Number(pf) + Number(af)) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '运费和预付到付不一致', 'warning'); return;
            }
            var json = JSON.stringify(row)
            $('#fm').form('submit', {
                url: "../Arrive/ArriveApi.aspx?method=UpdateContractInfo",
                contentType: "application/json;charset=utf-8", dataType: "json",
                onSubmit: function (param) {
                    param.PTruckNum = $('#ATruckNum').combobox('getText');
                    param.PCardName = $('#CardName').combobox('getText');
                    param.submitData = json;
                    var trd = $(this).form('enableValidation').form('validate');
                    if (trd) { $('#save').linkbutton('disable'); }
                    return trd;
                },
                success: function (msg) {
                    $('#save').linkbutton('enable');
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功，是否打印司机合同？', function (r) {
                            if (r) {
                                PrePrint(); //打印
                            }
                            $('#dlg').dialog('close');
                            dosearch();
                        });
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
        //单个配载
        function up(index, rows) {
            $('#dgManifest').datagrid('deleteRow', index);
            var index = $('#dgDriver').datagrid('getRowIndex', rows.AwbID);
            if (index < 0) {
                $('#dgDriver').datagrid('appendRow', rows);
                var p = $('#ATotalNum').numberbox('getValue') == "" ? 0 : Number($('#ATotalNum').numberbox('getValue'));
                var w = $('#AWeight').numberbox('getValue') == "" ? 0 : Number($('#AWeight').numberbox('getValue'));
                var v = $('#AVolume').numberbox('getValue') == "" ? 0 : Number($('#AVolume').numberbox('getValue'));
                var m = $('#ATotalMoney').numberbox('getValue') == "" ? 0 : Number($('#ATotalMoney').numberbox('getValue'));
                p++;
                var wt = w + Number(rows.AwbWeight);
                var vt = v + Number(rows.AwbVolume);
                var mt = m + Number(rows.TotalCharge);
                $('#ATotalNum').numberbox('setValue', Number(p));
                $('#AWeight').numberbox('setValue', Number(wt).toFixed(2));
                $('#AVolume').numberbox('setValue', Number(vt).toFixed(2));
                $('#ATotalMoney').numberbox('setValue', Number(mt).toFixed(2));
            }
        }
        //移除
        function down(index, rows) {
            $('#dgDriver').datagrid('deleteRow', index);
            var p = $('#ATotalNum').numberbox('getValue') == "" ? 0 : Number($('#ATotalNum').numberbox('getValue'));
            var w = $('#AWeight').numberbox('getValue') == "" ? 0 : Number($('#AWeight').numberbox('getValue'));
            var v = $('#AVolume').numberbox('getValue') == "" ? 0 : Number($('#AVolume').numberbox('getValue'));
            var m = $('#ATotalMoney').numberbox('getValue') == "" ? 0 : Number($('#ATotalMoney').numberbox('getValue'));
            p--;
            var wt = w - Number(rows.AwbWeight);
            var vt = v - Number(rows.AwbVolume);
            var mt = m - Number(rows.TotalCharge);
            $('#ATotalNum').numberbox('setValue', Number(p));
            $('#AWeight').numberbox('setValue', Number(wt).toFixed(2));
            $('#AVolume').numberbox('setValue', Number(vt).toFixed(2));
            $('#ATotalMoney').numberbox('setValue', Number(mt).toFixed(2));
            var index = $('#dgManifest').datagrid('getRowIndex', rows.AwbID);
            if (index < 0) {
                $('#dgManifest').datagrid('appendRow', rows);
            }
        }
        //批量选择
        function plup() {
            var rows = $('#dgManifest').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择数据！', 'warning'); return; }
            var copyRows = [];
            for (var i = 0, l = rows.length; i < l; i++) {
                var row = rows[i];
                copyRows.push(row);
                var index = $('#dgDriver').datagrid('getRowIndex', copyRows[i].AwbID);
                if (index < 0) {
                    $('#dgDriver').datagrid('appendRow', row);
                    var p = $('#ATotalNum').numberbox('getValue') == "" ? 0 : Number($('#ATotalNum').numberbox('getValue'));
                    var w = $('#AWeight').numberbox('getValue') == "" ? 0 : Number($('#AWeight').numberbox('getValue'));
                    var v = $('#AVolume').numberbox('getValue') == "" ? 0 : Number($('#AVolume').numberbox('getValue'));
                    var m = $('#ATotalMoney').numberbox('getValue') == "" ? 0 : Number($('#ATotalMoney').numberbox('getValue'));
                    p++;
                    var wt = w + Number(row.AwbWeight);
                    var vt = v + Number(row.AwbVolume);
                    var mt = m + Number(row.TotalCharge);
                    $('#ATotalNum').numberbox('setValue', Number(p));
                    $('#AWeight').numberbox('setValue', Number(wt).toFixed(2));
                    $('#AVolume').numberbox('setValue', Number(vt).toFixed(2));
                    $('#ATotalMoney').numberbox('setValue', Number(mt).toFixed(2));
                }
            }
            for (var i = 0; i < copyRows.length; i++) {
                var index = $('#dgManifest').datagrid('getRowIndex', copyRows[i]);
                $('#dgManifest').datagrid('deleteRow', index);
            }
        }
        //批量下
        function pldown() {
            var rows = $('#dgDriver').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择数据！', 'warning'); return; }
            var copyRows = [];
            for (var i = 0, l = rows.length; i < l; i++) {
                var row = rows[i];
                copyRows.push(row);
                var index = $('#dgManifest').datagrid('getRowIndex', copyRows[i].AwbID);
                if (index < 0) {
                    $('#dgManifest').datagrid('appendRow', row);
                    var p = $('#ATotalNum').numberbox('getValue') == "" ? 0 : Number($('#ATotalNum').numberbox('getValue'));
                    var w = $('#AWeight').numberbox('getValue') == "" ? 0 : Number($('#AWeight').numberbox('getValue'));
                    var v = $('#AVolume').numberbox('getValue') == "" ? 0 : Number($('#AVolume').numberbox('getValue'));
                    var m = $('#ATotalMoney').numberbox('getValue') == "" ? 0 : Number($('#ATotalMoney').numberbox('getValue'));
                    p--;
                    var wt = w - Number(row.AwbWeight);
                    var vt = v - Number(row.AwbVolume);
                    var mt = m - Number(row.TotalCharge);
                    $('#ATotalNum').numberbox('setValue', Number(p));
                    $('#AWeight').numberbox('setValue', Number(wt).toFixed(2));
                    $('#AVolume').numberbox('setValue', Number(vt).toFixed(2));
                    $('#ATotalMoney').numberbox('setValue', Number(mt).toFixed(2));
                }
            }
            for (var i = 0; i < copyRows.length; i++) {
                var index = $('#dgDriver').datagrid('getRowIndex', copyRows[i]);
                $('#dgDriver').datagrid('deleteRow', index);
            }
        }
        //车辆选择方法
        function truckChange(item) {
            if (item) {
                $('#Driver').textbox('setValue', item.Driver);
                $('#DriverCellPhone').textbox('setValue', item.DriverCellPhone);
                $('#DriverIDNum').textbox('setValue', item.DriverIDNum);
                $('#DriverIDAddress').textbox('setValue', item.DriverIDAddress);
                $('#Length').combobox('setValue', item.Length);
                $('#Model').combobox('setValue', item.Model);
                $('#CardBank').textbox('setValue', '')
                $('#CardNum').textbox('setValue', '')
                //加载司机的银行卡号信息
                $('#CardName').combobox('clear');
                var url = '../Arrive/ArriveApi.aspx?method=GetDriverCardByTruckNum&id=' + item.TruckNum;
                $('#CardName').combobox('reload', url);
            }
        }
        //到达站联系人选择后赋值
        function onDestPeopleChanged(item) {
            if (item) {
                $('#DestCellphone').textbox('setValue', item.Cellphone);
                $('#UnLoadAddress').textbox('setValue', item.Address);
            }
        }
        //起始站选择后赋值
        function depSetValue(item) {
            $.ajax({
                url: "../Arrive/ArriveApi.aspx?method=GetUnitByCityCode&id=" + escape(item.CityName),
                cache: false,
                success: function (text) {
                    var o = mini.decode(text);
                    $('#DepCellPhone').val(o.CellPhone);
                }
            });
        }
        var LODOP;
        //打印预览
        function Print() {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            CreateDataBill();
            LODOP.PRINT();
        }
        //打印预览
        function PrePrint() {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            CreateDataBill();
            LODOP.PREVIEW();
        }
        //打印设置
        function CreateDataBill() {
            LODOP.SET_PRINTER_INDEX(-1);
            LODOP.SET_PRINT_PAGESIZE(1, 2400, 2750, "");
            LODOP.SET_PRINT_STYLE("FontName", "宋体");
            LODOP.SET_PRINT_STYLE("FontSize", 10);
            LODOP.SET_PRINT_STYLE("Bold", 0);
            LODOP.ADD_PRINT_TEXT(10, 230, 700, 30, "新路城配货物配载交接表");
            LODOP.SET_PRINT_STYLEA(1, "FontName", "宋体");
            LODOP.SET_PRINT_STYLEA(1, "FontSize", 16);
            LODOP.SET_PRINT_STYLEA(1, "Bold", 1);
            var cp = $('#DepCellPhone').val();
            if (cp == "") { cp = "13544320020"; }
            LODOP.ADD_PRINT_TEXT(43, 20, 800, 20, "注：请驾驶员在行驶过程中保持电话畅通，如有异常请电广州24小时电话：" + cp);
            LODOP.ADD_PRINT_TEXT(60, 40, 100, 20, "司机姓名：");
            LODOP.ADD_PRINT_TEXT(60, 130, 300, 20, $('#Driver').textbox('getValue'));
            LODOP.ADD_PRINT_TEXT(60, 350, 100, 20, "日期：");
            LODOP.ADD_PRINT_TEXT(60, 420, 150, 20, getNowFormatDate(getDate($('#StartTime').datetimebox('getValue'))));
            LODOP.ADD_PRINT_TEXT(60, 510, 100, 20, "发车时间：");
            LODOP.ADD_PRINT_TEXT(60, 580, 150, 20, CurentTimeHHMM(getDate($('#StartTime').datetimebox('getValue'))));
            //打印二维码
            LODOP.ADD_PRINT_BARCODE(25, 650, 200, 100, "QRCode", $('#AContractNum').val());
            LODOP.SET_PRINT_STYLEA(0, "QRCodeVersion", 3);
            LODOP.ADD_PRINT_TEXT(80, 40, 100, 20, "车牌号：");
            LODOP.ADD_PRINT_TEXT(80, 130, 300, 20, $('#ATruckNum').combobox('getText'));
            LODOP.ADD_PRINT_TEXT(80, 350, 100, 20, "出发站：");
            LODOP.ADD_PRINT_TEXT(80, 420, 100, 20, $('#ADep').combobox('getValue'));
            LODOP.ADD_PRINT_TEXT(80, 510, 100, 20, "到达站：");
            LODOP.ADD_PRINT_TEXT(80, 580, 300, 20, $('#ADest').combobox('getValue'));
            LODOP.ADD_PRINT_TEXT(100, 40, 100, 20, "联系方式：");
            LODOP.ADD_PRINT_TEXT(100, 130, 300, 20, $('#DriverCellPhone').textbox('getValue'));
            LODOP.ADD_PRINT_TEXT(100, 350, 100, 20, "联系人：");
            LODOP.ADD_PRINT_TEXT(100, 420, 300, 20, $('#DestPeople').combobox('getValue'));
            LODOP.ADD_PRINT_TEXT(120, 40, 100, 20, "身份证号码：");
            LODOP.ADD_PRINT_TEXT(120, 130, 300, 20, $('#DriverIDNum').textbox('getValue'));
            LODOP.ADD_PRINT_TEXT(120, 350, 100, 20, "详细地址：");
            LODOP.ADD_PRINT_TEXT(120, 420, 300, 20, $('#UnLoadAddress').textbox('getValue'));
            LODOP.ADD_PRINT_TEXT(140, 40, 100, 20, "身份证地址：");
            LODOP.ADD_PRINT_TEXT(140, 130, 300, 20, $('#DriverIDAddress').textbox('getValue'));
            LODOP.ADD_PRINT_TEXT(140, 350, 100, 20, "联系方式：");
            LODOP.ADD_PRINT_TEXT(140, 410, 300, 20, $('#DestCellphone').textbox('getValue'));

            LODOP.ADD_PRINT_RECT(160, 20, 30, 20, 0, 0);
            LODOP.ADD_PRINT_TEXT(162, 22, 30, 20, "序号");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 8);
            LODOP.ADD_PRINT_RECT(160, 50, 65, 20, 0, 0);
            LODOP.ADD_PRINT_TEXT(162, 52, 65, 20, "单号");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_RECT(160, 115, 70, 20, 0, 0);
            LODOP.ADD_PRINT_TEXT(162, 117, 70, 20, "最迟时效");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
            LODOP.ADD_PRINT_RECT(160, 185, 40, 20, 0, 0);
            LODOP.ADD_PRINT_TEXT(162, 187, 40, 20, "件数");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
            LODOP.ADD_PRINT_RECT(160, 225, 40, 20, 0, 0);
            LODOP.ADD_PRINT_TEXT(162, 227, 40, 20, "分批");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
            LODOP.ADD_PRINT_RECT(160, 265, 40, 20, 0, 0);
            LODOP.ADD_PRINT_TEXT(162, 267, 40, 20, "中转");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
            LODOP.ADD_PRINT_RECT(160, 305, 80, 20, 0, 0);
            LODOP.ADD_PRINT_TEXT(162, 307, 80, 20, "客户名称");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
            LODOP.ADD_PRINT_RECT(160, 385, 65, 20, 0, 0);
            LODOP.ADD_PRINT_TEXT(162, 387, 65, 20, "收货人");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
            LODOP.ADD_PRINT_RECT(160, 450, 80, 20, 0, 0);
            LODOP.ADD_PRINT_TEXT(162, 452, 80, 20, "货物名称");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
            LODOP.ADD_PRINT_RECT(160, 530, 40, 20, 0, 0);
            LODOP.ADD_PRINT_TEXT(162, 532, 40, 20, "付款");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
            LODOP.ADD_PRINT_RECT(160, 570, 35, 20, 0, 0);
            LODOP.ADD_PRINT_TEXT(162, 572, 35, 20, "回单");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
            LODOP.ADD_PRINT_RECT(160, 605, 45, 20, 0, 0);
            LODOP.ADD_PRINT_TEXT(162, 607, 45, 20, "方数");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
            LODOP.ADD_PRINT_RECT(160, 650, 45, 20, 0, 0);
            LODOP.ADD_PRINT_TEXT(162, 652, 45, 20, "吨数");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
            LODOP.ADD_PRINT_RECT(160, 695, 50, 20, 0, 0);
            LODOP.ADD_PRINT_TEXT(162, 697, 50, 20, "到付款");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
            LODOP.ADD_PRINT_RECT(160, 745, 50, 20, 0, 0);
            LODOP.ADD_PRINT_TEXT(162, 747, 50, 20, "代收款");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
            LODOP.ADD_PRINT_RECT(160, 795, 150, 20, 0, 0);
            LODOP.ADD_PRINT_TEXT(162, 797, 150, 20, "备注");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
            var n = 0;
            var pNum = 0;
            var griddata = $('#dgDriver').datagrid('getRows');
            for (var i = 0; i < griddata.length + 1; i++) {
                if (i == griddata.length) {
                    LODOP.ADD_PRINT_TEXT(182 + i * 20, 70, 100, 20, "合计：");
                    LODOP.ADD_PRINT_TEXT(182 + i * 20, 185, 100, 20, pNum);
                    LODOP.ADD_PRINT_TEXT(240 + i * 20, 100, 70, 20, "交货人：");
                    LODOP.ADD_PRINT_TEXT(240 + i * 20, 462, 70, 20, "接货人：");
                    break;
                }
                n++;
                LODOP.ADD_PRINT_RECT(180 + i * 20, 20, 30, 20, 0, 0);
                LODOP.ADD_PRINT_TEXT(182 + i * 20, 22, 30, 20, n);
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                LODOP.ADD_PRINT_RECT(180 + i * 20, 50, 65, 20, 0, 0);
                LODOP.ADD_PRINT_TEXT(182 + i * 20, 52, 65, 20, griddata[i].AwbNo);
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_RECT(180 + i * 20, 115, 70, 20, 0, 0);
                LODOP.ADD_PRINT_TEXT(182 + i * 20, 117, 70, 20, griddata[i].LatestTimeLimit);
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                LODOP.ADD_PRINT_RECT(180 + i * 20, 185, 40, 20, 0, 0);
                LODOP.ADD_PRINT_TEXT(182 + i * 20, 187, 40, 20, griddata[i].Piece);
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                LODOP.ADD_PRINT_RECT(180 + i * 20, 225, 40, 20, 0, 0);
                LODOP.ADD_PRINT_TEXT(182 + i * 20, 227, 40, 20, griddata[i].AwbPiece);
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                LODOP.ADD_PRINT_RECT(180 + i * 20, 265, 40, 20, 0, 0);
                LODOP.ADD_PRINT_TEXT(182 + i * 20, 267, 40, 20, griddata[i].Transit);
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                LODOP.ADD_PRINT_RECT(180 + i * 20, 305, 80, 20, 0, 0);
                LODOP.ADD_PRINT_TEXT(182 + i * 20, 307, 80, 20, griddata[i].ShipperUnit); //客户名称
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                LODOP.ADD_PRINT_RECT(180 + i * 20, 385, 65, 20, 0, 0);
                LODOP.ADD_PRINT_TEXT(182 + i * 20, 387, 65, 20, griddata[i].AcceptPeople); //收货人
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                LODOP.ADD_PRINT_RECT(180 + i * 20, 450, 80, 20, 0, 0);
                LODOP.ADD_PRINT_TEXT(182 + i * 20, 452, 80, 20, griddata[i].Goods); //货物名称
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                var fu = onCheckOutType(griddata[i].CheckOutType)
                LODOP.ADD_PRINT_RECT(180 + i * 20, 530, 40, 20, 0, 0);
                LODOP.ADD_PRINT_TEXT(182 + i * 20, 532, 40, 20, fu); //付款方式
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                LODOP.ADD_PRINT_RECT(180 + i * 20, 570, 35, 20, 0, 0);
                LODOP.ADD_PRINT_TEXT(182 + i * 20, 572, 35, 20, griddata[i].ReturnAwb); //回单数
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                LODOP.ADD_PRINT_RECT(180 + i * 20, 605, 45, 20, 0, 0);
                LODOP.ADD_PRINT_TEXT(182 + i * 20, 607, 45, 20, griddata[i].AwbVolume); //方数
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                LODOP.ADD_PRINT_RECT(180 + i * 20, 650, 45, 20, 0, 0);
                LODOP.ADD_PRINT_TEXT(182 + i * 20, 652, 45, 20, griddata[i].AwbWeight); //吨数
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                LODOP.ADD_PRINT_RECT(180 + i * 20, 695, 50, 20, 0, 0);
                if (griddata[i].CheckOutType == "3") {
                    LODOP.ADD_PRINT_TEXT(182 + i * 20, 697, 50, 20, griddata[i].TotalCharge); //到付款
                }
                else {
                    LODOP.ADD_PRINT_TEXT(182 + i * 20, 697, 50, 20, 0); //到付款                
                }
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                LODOP.ADD_PRINT_RECT(180 + i * 20, 745, 50, 20, 0, 0);
                LODOP.ADD_PRINT_TEXT(182 + i * 20, 747, 50, 20, griddata[i].CollectMoney); //代收款
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                LODOP.ADD_PRINT_RECT(180 + i * 20, 795, 150, 20, 0, 0);
                LODOP.ADD_PRINT_TEXT(182 + i * 20, 797, 150, 20, griddata[i].Remark); //备注
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                pNum += Number(griddata[i].AwbPiece);
            }
        }
        //结款方式
        function onCheckOutType(e) {
            var CheckOutType = [{ id: 0, text: '现付' }, { id: 1, text: '回单' }, { id: 2, text: '月结' }, { id: 3, text: '到付' }, { id: 4, text: '代收款' }];
            for (var i = 0, l = CheckOutType.length; i < l; i++) {
                var g = CheckOutType[i];
                if (g.id == e) return g.text;
            }
            return "";
        }
        //打印运输合同
        function ManifestPrint() {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            CreateManifest();
            LODOP.PREVIEW();
        }
        //打印设置
        function CreateManifest() {
            LODOP.SET_PRINTER_INDEX(-1);

            LODOP.PRINT_INITA(0, 0, 1122, 793, "新路城配长运车辆运输合同");
            LODOP.ADD_PRINT_TEXT(10, 360, 398, 30, "新路城配长运车辆运输合同");
            LODOP.SET_PRINT_STYLEA(0, "FontName", "新宋体");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 17);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(54, 13, 60, 25, "甲方：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(54, 463, 59, 25, "乙方：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_RECT(66, 66, 383, 1, 0, 1);
            LODOP.ADD_PRINT_TEXT(50, 76, 259, 20, "新路城配");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_RECT(66, 514, 285, 1, 0, 1);
            LODOP.ADD_PRINT_TEXT(50, 531, 289, 20, $('#Driver').textbox('getValue') + " " + $('#ATruckNum').combobox('getText'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(36, 837, 90, 25, "合同编号：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(36, 913, 170, 25, $('#AContractNum').val());
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(61, 837, 90, 25, "起讫站点：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(61, 913, 170, 25, $('#ADep').combobox('getValue') + " -- " + $('#ADest').combobox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(86, 837, 90, 25, "发车时间：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(86, 913, 170, 25, $('#StartTime').datetimebox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(91, 10, 117, 25, "一、乙方资料");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(118, 19, 90, 20, "驾驶员姓名：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_RECT(128, 90, 494, 1, 0, 1);
            LODOP.ADD_PRINT_TEXT(112, 130, 380, 20, $('#Driver').textbox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(118, 590, 90, 20, "车牌号：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_RECT(128, 664, 426, 1, 0, 1);
            LODOP.ADD_PRINT_TEXT(113, 684, 344, 20, $('#ATruckNum').combobox('getText'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(141, 19, 90, 20, "身份证号码：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_RECT(151, 90, 494, 1, 0, 1);
            LODOP.ADD_PRINT_TEXT(134, 130, 380, 20, $('#DriverIDNum').textbox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(141, 590, 90, 20, "发动机号码：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_RECT(151, 664, 426, 1, 0, 1);
            LODOP.ADD_PRINT_TEXT(164, 19, 90, 20, "身份证地址：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_RECT(174, 90, 494, 1, 0, 1);
            LODOP.ADD_PRINT_TEXT(156, 130, 380, 20, $('#DriverIDAddress').textbox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(164, 590, 90, 20, "车辆吨位：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_RECT(174, 664, 426, 1, 0, 1);
            LODOP.ADD_PRINT_TEXT(187, 19, 90, 20, "驾驶员证号：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_RECT(197, 90, 494, 1, 0, 1);
            LODOP.ADD_PRINT_TEXT(187, 590, 90, 20, "车辆车场：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_RECT(197, 664, 426, 1, 0, 1);
            LODOP.ADD_PRINT_TEXT(209, 10, 506, 25, "二、经双方协商，达成以下合同，乙方为甲方承运货物：  ");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(240, 19, 75, 20, "起点站：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(240, 263, 75, 20, "终点站：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(240, 521, 75, 20, "合计：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(240, 677, 40, 20, "件");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_RECT(250, 75, 150, 1, 0, 1);
            LODOP.ADD_PRINT_RECT(250, 324, 169, 1, 0, 1);
            LODOP.ADD_PRINT_RECT(250, 569, 100, 1, 0, 1);
            LODOP.ADD_PRINT_TEXT(237, 107, 150, 20, $('#ADep').combobox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(237, 346, 150, 20, $('#ADest').combobox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(237, 606, 42, 20, $('#ATotalNum').numberbox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(263, 19, 75, 20, "起点站：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(263, 263, 75, 20, "终点站：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(263, 521, 75, 20, "合计：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(263, 677, 40, 20, "件");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_RECT(273, 75, 150, 1, 0, 1);
            LODOP.ADD_PRINT_RECT(273, 324, 169, 1, 0, 1);
            LODOP.ADD_PRINT_RECT(273, 569, 100, 1, 0, 1);
            LODOP.ADD_PRINT_TEXT(288, 10, 1082, 25, "三、甲方应向乙方支付全部运费：             ，发车时预付：           ，凭回单结算：      ");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(288, 257, 100, 20, $('#TransportFee').numberbox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(288, 482, 100, 20, $('#PrepayFee').numberbox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(288, 700, 100, 20, $('#ArriveFee').numberbox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(313, 10, 1087, 66, "四、乙方在装车时要清点货物，在运输途中要求精心爱护货物，防止肇事、水灾、火灾、雨淋、挤压、油污、磨损、散包、丢失等意外事故发生，如果发生乙方应该按照货物价值赔偿。乙方必须按甲方指定线路运行，必须将货物安全、准时送至卸货地点，不准配货、倒货、转货、如有特殊情况，乙方必须提前告知甲方，否则一切损失由乙方负责赔偿。");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(378, 10, 1087, 45, "五、在运输中甲方不负责乙方一切过桥、过路、修车、吃饭费用，甲方负责提供储运货物证明手续，乙方必须在      小时内将货物安全送达目的地，如果超出，将按照甲方考核方案罚款。");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(421, 10, 1087, 25, "六、本合同一式两份、经双方签字后生效，并承担一些法律责任，货款两清时终止本合同。");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(446, 10, 1087, 25, "七、乙方须将收货方签收的回单必须在10日内送达货快递至甲方公司，邮费由乙方承担，否则迟一天扣100元。超过一个月，回单全程款款单生效。");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(471, 10, 1087, 25, "八、乙方必须每天电话向甲方报告所在位置、车辆及道路状况。否则每缺少一次通话罚款50元，乙方手机必须保持24小时开机。");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_RECT(490, 10, 1087, 115, 0, 0);
            LODOP.ADD_PRINT_RECT(608, 10, 1087, 92, 0, 1);
            LODOP.ADD_PRINT_TEXT(497, 15, 800, 25, "本趟车：          装         卸，   封条号码：");
            LODOP.ADD_PRINT_TEXT(518, 15, 1000, 25, "卸货地址1：                                                                     联系人：                     联系电话：");
            LODOP.ADD_PRINT_TEXT(518, 82, 443, 20, $('#UnLoadAddress').textbox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(518, 583, 100, 20, $('#DestPeople').combobox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(518, 780, 100, 20, $('#DestCellphone').textbox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(541, 15, 1000, 25, "卸货地址2：                                                                     联系人：                     联系电话：");
            LODOP.ADD_PRINT_TEXT(560, 15, 1000, 20, "要求最晚到车时间：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(581, 15, 1000, 20, "特别注意事项：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(581, 114, 966, 20, $('#Remark').val());
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(612, 15, 1000, 20, "汇款信息");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(633, 38, 85, 20, "收款人：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_RECT(642, 90, 743, 1, 0, 1);
            LODOP.ADD_PRINT_TEXT(626, 130, 261, 20, $('#CardName').combobox('getText'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(657, 38, 85, 20, "开户行：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_RECT(666, 90, 743, 1, 0, 1);
            LODOP.ADD_PRINT_TEXT(648, 130, 570, 20, $('#CardBank').textbox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(679, 38, 85, 20, "账号：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_RECT(688, 90, 743, 1, 0, 1);
            LODOP.ADD_PRINT_TEXT(671, 130, 570, 20, $('#CardNum').textbox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(710, 100, 100, 25, "甲方代表：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(735, 100, 100, 25, "日 期：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(710, 343, 100, 25, "公司电话：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.SET_PRINT_STYLEA(0, "Horient", 1);
            LODOP.ADD_PRINT_TEXT(710, 575, 100, 25, "乙方代表：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(710, 816, 100, 25, "公司电话：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(735, 575, 100, 25, "日 期：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
        }

        function PayPrint() {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            var nowdate = new Date();

            LODOP.PRINT_INIT("新路城配付款申请书");
            LODOP.ADD_PRINT_TEXT(0, 206, 480, 35, "新路城配付款申请书");
            LODOP.SET_PRINT_STYLEA(0, "FontName", "新宋体");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 16);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(25, 563, 114, 30, "申请日期：");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
            LODOP.ADD_PRINT_TEXT(25, 641, 142, 30, getNowFormatDate(nowdate));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 12);
            LODOP.ADD_PRINT_RECT(45, 1, 760, 432, 0, 1);
            LODOP.ADD_PRINT_RECT(45, 1, 100, 38, 0, 1);
            LODOP.ADD_PRINT_RECT(45, 100, 255, 38, 0, 1);
            LODOP.ADD_PRINT_RECT(45, 354, 80, 38, 0, 1);
            LODOP.ADD_PRINT_RECT(45, 433, 328, 38, 0, 1);
            LODOP.ADD_PRINT_TEXT(57, 16, 79, 25, "收款单位");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(57, 109, 254, 25, $('#PAcceptUnit').textbox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(57, 364, 85, 25, "对应单号");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(49, 438, 332, 25, $('#PAwbNo').textbox('getValue'));
            LODOP.ADD_PRINT_RECT(82, 1, 100, 38, 0, 1);
            LODOP.ADD_PRINT_TEXT(92, 16, 79, 25, "支付方式");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_RECT(82, 100, 661, 38, 0, 1);
            LODOP.ADD_PRINT_RECT(92, 168, 17, 18, 0, 1);
            LODOP.ADD_PRINT_TEXT(92, 192, 52, 25, "现金");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_RECT(92, 280, 17, 18, 0, 1);
            LODOP.ADD_PRINT_TEXT(92, 305, 52, 25, "汇款");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_RECT(92, 377, 17, 18, 0, 1);
            LODOP.ADD_PRINT_TEXT(92, 408, 57, 25, "油卡");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_RECT(119, 1, 100, 38, 0, 1);
            LODOP.ADD_PRINT_RECT(156, 1, 100, 38, 0, 1);
            LODOP.ADD_PRINT_RECT(193, 1, 100, 38, 0, 1);
            LODOP.ADD_PRINT_RECT(230, 1, 100, 130, 0, 1);
            LODOP.ADD_PRINT_RECT(359, 1, 100, 40, 0, 1);
            LODOP.ADD_PRINT_RECT(398, 1, 100, 40, 0, 1);
            LODOP.ADD_PRINT_TEXT(129, 27, 58, 25, "户名");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(166, 16, 79, 25, "应付费用");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(202, 16, 79, 25, "实际付款");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_RECT(119, 100, 100, 38, 0, 1);
            LODOP.ADD_PRINT_RECT(119, 199, 70, 38, 0, 1);
            LODOP.ADD_PRINT_TEXT(129, 207, 66, 25, "开户行");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_RECT(119, 268, 180, 38, 0, 1);
            LODOP.ADD_PRINT_RECT(119, 447, 60, 38, 0, 1);
            LODOP.ADD_PRINT_RECT(119, 506, 255, 38, 0, 1);
            LODOP.ADD_PRINT_TEXT(129, 460, 53, 25, "账号");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(127, 110, 89, 25, $('#PCardName').textbox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(127, 276, 171, 25, $('#PCardBank').textbox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_TEXT(129, 520, 221, 25, $('#PCardNum').textbox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_RECT(156, 100, 100, 38, 0, 1);
            LODOP.ADD_PRINT_RECT(156, 199, 70, 38, 0, 1);
            LODOP.ADD_PRINT_RECT(156, 268, 239, 38, 0, 1);
            LODOP.ADD_PRINT_RECT(156, 506, 95, 38, 0, 1);
            LODOP.ADD_PRINT_RECT(156, 600, 161, 38, 0, 1);
            LODOP.ADD_PRINT_RECT(193, 100, 100, 38, 0, 1);
            LODOP.ADD_PRINT_RECT(193, 199, 70, 38, 0, 1);
            LODOP.ADD_PRINT_RECT(193, 268, 239, 38, 0, 1);
            LODOP.ADD_PRINT_RECT(193, 506, 95, 38, 0, 1);
            LODOP.ADD_PRINT_RECT(193, 600, 161, 38, 0, 1);
            LODOP.ADD_PRINT_TEXT(166, 215, 48, 25, "大写");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(202, 215, 48, 25, "大写");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(166, 517, 79, 25, "分批支付");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(164, 632, 52, 20, $('input:radio[name=PFenPi]:checked').val());
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(202, 517, 80, 25, "手机号码");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(263, 16, 80, 73, "付款原因及备注");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(236, 104, 655, 122, $('#PFReason').val());
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
            LODOP.ADD_PRINT_RECT(230, 100, 661, 130, 0, 1);
            LODOP.ADD_PRINT_RECT(359, 100, 661, 40, 0, 1);
            LODOP.ADD_PRINT_RECT(398, 100, 361, 40, 0, 1);
            LODOP.ADD_PRINT_TEXT(370, 9, 86, 25, "总经办");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(407, 10, 87, 30, "财务主管");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(166, 105, 100, 20, $('#PYingFu').textbox('getValue')); //应付费用
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(202, 107, 100, 25, $('#PShiJi').textbox('getValue')); //实际付款
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(166, 272, 236, 25, atoc($('#PYingFu').textbox('getValue'))); //大写
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(202, 272, 236, 25, atoc($('#PShiJi').textbox('getValue'))); //大写
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_TEXT(202, 607, 148, 25, $('#PCellphone').textbox('getValue'));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.ADD_PRINT_RECT(398, 460, 79, 40, 0, 1);
            LODOP.ADD_PRINT_TEXT(403, 478, 54, 32, "财务");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_RECT(398, 538, 223, 40, 0, 1);
            LODOP.ADD_PRINT_RECT(437, 1, 100, 40, 0, 1);
            LODOP.ADD_PRINT_TEXT(446, 5, 93, 30, "部门负责人");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_RECT(437, 100, 361, 40, 0, 1);
            LODOP.ADD_PRINT_RECT(437, 460, 79, 40, 0, 1);
            LODOP.ADD_PRINT_TEXT(442, 468, 68, 30, "申请人");
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
            LODOP.ADD_PRINT_TEXT(441, 543, 223, 30, "<%=UserInfor.UserName.Trim() %>" + " " + AllDateTime(nowdate));
            LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
            LODOP.PREVIEW();
        }
    </script>

</asp:Content>
