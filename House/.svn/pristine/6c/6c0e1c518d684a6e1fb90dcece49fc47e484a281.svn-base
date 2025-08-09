<%@ Page Title="预录单管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PreOrderManager.aspx.cs" Inherits="Cargo.Order.PreOrderManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/Rotate/jQueryRotate.2.2.js" type="text/javascript"></script>
    <style type="text/css">
        .commTblStyle_8 th { border: 1px solid rgb(205, 205, 205); text-align: center; color: rgb(255, 255, 255); line-height: 28px; background-color: rgb(15, 114, 171); }

        .commTblStyle_8 tr.BlankRow td { line-height: 10px; }

        .commTblStyle_8 tr td { border: 1px solid rgb(205, 205, 205); text-align: center; line-height: 20px; }

            .commTblStyle_8 tr td.left { text-align: right; padding-right: 10px; font-weight: bold; white-space: nowrap; background-color: rgb(239, 239, 239); }

            .commTblStyle_8 tr td.right { text-align: left; padding-left: 10px; }

        .commTblStyle_8 .whiteback { background-color: rgb(255, 255, 255); }
    </style>
    <script src="../JS/easy/js/datagrid-groupview.js" type="text/javascript"></script>
    <%--<script src="../JS/Date/CheckActivX.js" type="text/javascript"></script>--%>
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
                      title: '订单号', field: 'OrderNo', width: '90px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '出发站', field: 'Dep', width: '50px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '到达站', field: 'Dest', width: '50px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '件数', field: 'Piece', width: '35px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '收入', field: 'TransportFee', width: '50px', align: 'right', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  //{
                  //    title: '保险费用', field: 'InsuranceFee', width: '50px', align: 'right', formatter: function (value) {
                  //        return "<span title='" + value + "'>" + value + "</span>";
                  //    }
                  //},
                  {
                      title: '合计', field: 'TotalCharge', width: '60px', align: 'right', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '付款人', field: 'PayClientName', width: '55px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '客户名称', field: 'AcceptUnit', width: '120px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '收货人', field: 'AcceptPeople', width: '55px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '联系手机', field: 'AcceptCellphone', width: '90px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '收货地址', field: 'AcceptAddress', width: '120px', formatter: function (value) {
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
                  {
                      title: '订单状态', field: 'AwbStatus', width: '60px',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "<span title='已下单'>已下单</span>"; }
                          else if (val == "1") { return "<span title='出库中'>出库中</span>"; }
                          else if (val == "2") { return "<span title='已出库'>已出库</span>"; }
                          else if (val == "3") { return "<span title='运输在途'>运输在途</span>"; }
                          else if (val == "4") { return "<span title='已到达'>已到达</span>"; }
                          else if (val == "5") { return "<span title='已签收'>已签收</span>"; }
                          else if (val == "6") { return "<span title='已拣货'>已拣货</span>"; }
                          else if (val == "7") { return "<span title='正在配送'>正在配送</span>"; }
                          else { return ""; }
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
                      title: '物流公司单号', field: 'LogisAwbNo', width: '90px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '结算状态', field: 'CheckStatus', width: '60px', formatter: function (val, row, index) {
                          if (val == "0") { return "<span title='未结算'>未结算</span>"; }
                          else if (val == "1") { return "<span title='已结算'>已结算</span>"; }
                          else if (val == "2") { return "<span title='未结清'>未结清</span>"; }
                          else { return ""; }
                      }
                  },
                  {
                      title: '物流配送费用', field: 'DeliveryFee', width: '80px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '物流公司名称', field: 'LogisticName', width: '90px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '订单类型', field: 'ThrowGood', width: '60px',
                      formatter: function (val, row, index) {
                          if (val == "5") { return "<span title='长和订单'>长和订单</span>"; }
                          else if (val == "6") { return "<span title='商贸订单'>商贸订单</span>"; }
                          else if (val == "10") { return "<span title='祺航订单'>祺航订单</span>"; }
                          else if (val == "7") { return "<span title='其它订单'>其它订单</span>"; }
                          else { return ""; }
                      }
                  },
                  {
                      title: '订单状态', field: 'IsMakeSure', width: '60px',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "<span title='未确认'>未确认</span>"; }
                          else if (val == "1") { return "<span title='已确认'>已确认</span>"; }
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
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getNowFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#Dep').combobox('textbox').bind('focus', function () { $('#Dep').combobox('showPanel'); });
            $('#Dest').combobox('textbox').bind('focus', function () { $('#Dest').combobox('showPanel'); });
            $('#AOrderType').combobox('textbox').bind('focus', function () { $('#AOrderType').combobox('showPanel'); });
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#ASaleManID').combobox('clear');
                    var url = '../Order/orderApi.aspx?method=QueryUserByDepCode&houseID=' + rec.HouseID;
                    $('#ASaleManID').combobox('reload', url);
                    $('#PID').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    $('#PID').combobox('reload', url);
                }
            });
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            $('#ASaleManID').combobox('clear');
            var url = '../Order/orderApi.aspx?method=QueryUserByDepCode&houseID=<%=UserInfor.HouseID%>';
            $('#ASaleManID').combobox('reload', url);
            $('#PID').combobox('clear');
            var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=<%=UserInfor.HouseID%>';
            $('#PID').combobox('reload', url);

            //一级产品
            $('#APID').combobox({
                url: '../Product/productApi.aspx?method=QueryALLOneProductType', valueField: 'TypeID', textField: 'TypeName',
                onSelect: function (rec) {
                    $('#ASID').combobox('clear');
                    var url = '../Product/productApi.aspx?method=QueryALLOneProductType&PID=' + rec.TypeID;
                    $('#ASID').combobox('reload', url);
                }
            });
            $('#APID').combobox('textbox').bind('focus', function () { $('#APID').combobox('showPanel'); });
            $('#ASID').combobox('textbox').bind('focus', function () { $('#ASID').combobox('showPanel'); });
            $('#ThrowGood').combobox('textbox').bind('focus', function () { $('#ThrowGood').combobox('showPanel'); });
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            $('#PID').combobox('textbox').bind('focus', function () { $('#PID').combobox('showPanel'); });
            $('#ASaleManID').combobox('textbox').bind('focus', function () { $('#ASaleManID').combobox('showPanel'); });
            var value2 = 0
            $("#simg").rotate({ bind: { click: function () { value2 += 90; $(this).rotate({ animateTo: value2 }) } } });
            //所在仓库
            $('#ThrowID').combobox({ url: '../House/houseApi.aspx?method=QueryDLTHouse', valueField: 'HouseID', textField: 'Name' });
            //付款人
            $('#APayClientNum').combobox({ valueField: 'ClientNum', textField: 'Boss', delay: '10', url: '../Client/clientApi.aspx?method=AutoCompleteClient' });

            //动态设置下拉框的属性
            var data = [];
            data.push({ "text": "全部", "id": -1 });
            data.push({ "text": "长和订单", "id": 5 });
            data.push({ "text": "商贸订单", "id": 6 });
            data.push({ "text": "祺航订单", "id": 10 });
            data.push({ "text": "其它订单", "id": 7 });
            $("#ThrowGood").combobox("loadData", data);
            $("#ThrowGood").combobox("setValue", -1);
        });

        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=QueryPreOrderInfo';
            $('#dg').datagrid('load', {
                OrderNo: $('#AOrderNo').val(),
                LogisAwbNo: $('#LogisAwbNo').val(),
                AcceptPeople: $("#AcceptPeople").val(),
                Piece: $("#Piece").val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                FinanceSecondCheck: '-1',
                CheckOutType: '',
                ThrowGood: $("#ThrowGood").combobox('getValue'),
                OrderType: $("#AOrderType").combobox('getValue'),
                AwbStatus: $("#AAwbStatus").combobox('getValue'),
                Dep: $("#Dep").combobox('getText'),
                Dest: $("#Dest").combobox('getText'),
                HouseID: $("#AHouseID").combobox('getValue'),//仓库ID
                SaleManID: $('#ASaleManID').combobox('getValue'),
                CreateAwb: $('#ACreateAwb').textbox('getValue'),
                //FirstID: $("#PID").combobox('getValue'),//父ID
                OutHouseName: $("#PID").combobox('getText'),
                AcceptUnit: $('#AcceptUnit').val(),
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
                <td style="text-align: right;">出发站:
                </td>
                <td>
                    <input id="Dep" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../systempage/sysService.aspx?method=QueryAllCity'" /><!-- panelheight="auto"-->
                </td>
                <td style="text-align: right;">区域大仓:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;" />
                </td>
                <td style="text-align: right;">物流单号:
                </td>
                <td>
                    <input id="LogisAwbNo" class="easyui-textbox" data-options="prompt:'请输入物流单号'" style="width: 100px">
                </td>
                <td style="text-align: right;">订单类型:
                </td>
                <td>
                    <input class="easyui-combobox" id="ThrowGood" style="width: 80px;" panelheight="auto" data-options="valueField:'id',textField:'text'">
                </td>
                <td style="text-align: right;">开单时间:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">收货人:
                </td>
                <td>
                    <input id="AcceptPeople" class="easyui-textbox" data-options="prompt:'收货单位/人'" style="width: 100px;" />
                </td>
                <td style="text-align: right;">到达站:
                </td>
                <td>
                    <input id="Dest" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../systempage/sysService.aspx?method=QueryAllCity',multiple:true" />
                </td>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="PID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'AreaID',textField:'Name'"
                        panelheight="auto" />
                </td>
                <td style="text-align: right;">订单状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="AAwbStatus" style="width: 100px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">已下单</option>
                        <option value="6">已拣货</option>
                        <option value="1">出库中</option>
                        <option value="2">已出库</option>
                        <option value="3">已装车</option>
                        <%-- <option value="4">已到达</option>--%>
                        <option value="5">已签收</option>
                    </select>
                </td>

                <td style="text-align: right;">下单方式:
                </td>
                <td>
                    <select class="easyui-combobox" id="AOrderType" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">电脑下单</option>
                        <option value="1">企业号下单</option>
                        <option value="2">商城下单</option>
                        <option value="3">APP下单</option>
                        <option value="4">小程序下单</option>
                    </select>
                </td>

                <td style="text-align: right;">订单件数:
                </td>
                <td>
                    <input id="Piece" class="easyui-textbox" data-options="prompt:'请输入件数'" style="width: 80px">
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">客户名称:
                </td>
                <td>
                    <input id="AcceptUnit" class="easyui-textbox" data-options="prompt:'请输入客户名称'" style="width: 100px">
                </td>
                <td style="text-align: right;">开单员:
                </td>
                <td>
                    <input id="ACreateAwb" class="easyui-textbox" data-options="prompt:'请输入开单员'" style="width: 100px">
                </td>
                <td style="text-align: right;">业务员:
                </td>
                <td>
                    <input id="ASaleManID" class="easyui-combobox" style="width: 100px;"
                        data-options="valueField: 'LoginName',textField: 'UserName'" />
                </td>
                <td style="text-align: right;">一级产品:
                </td>
                <td>
                    <input id="APID" class="easyui-combobox" style="width: 100px;"
                        panelheight="auto" />
                </td>
                <td style="text-align: right;">二级产品:
                </td>
                <td>
                    <input id="ASID" class="easyui-combobox" style="width: 80px;" data-options="valueField:'TypeID',textField:'TypeName'" />
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="dgtoolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cut" id="delete" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" id="delete" plain="false" onclick="Confirm()">&nbsp;确认订单&nbsp;</a>&nbsp;&nbsp;
    </div>
    <input type="hidden" id="DisplayNum" />
    <input type="hidden" id="DisplayPiece" />
    <div id="dlgOrder" class="easyui-dialog" style="width: 1080px; height: 540px;" closed="true"
        closable="false" buttons="#dlgOrder-buttons">
        <form id="fmDep" method="post">
            <input type="hidden" name="SaleManName" id="SaleManName" />
            <input type="hidden" name="SaleCellPhone" id="SaleCellPhone" />
            <input type="hidden" name="HouseCode" id="HouseCode" />
            <input type="hidden" name="HouseID" id="HouseID" />
            <input type="hidden" name="ONum" id="ONum" />
            <input type="hidden" name="OutNum" id="OutNum" />
            <input type="hidden" name="OrderID" id="OrderID" />
            <input type="hidden" name="OrderNo" id="OrderNo" />
            <input type="hidden" name="ClientNum" id="ClientNum" />
            <input type="hidden" name="TrafficType" />
            <input type="hidden" id="HiddenClientNum" />
            <input type="hidden" id="HiddenAcceptPeople" />
            <input type="hidden" id="HiddenLimitID" />
            <input type="hidden" id="HiddenLimitTitle" />
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
                            <input name="AcceptAddress" id="AAcceptAddress" style="width: 100px;" class="easyui-textbox" />
                        </td>
                        <td style="text-align: right;">电话:
                        </td>
                        <td>
                            <input name="AcceptTelephone" id="AAcceptTelephone" class="easyui-textbox"
                                style="width: 90px;" />
                        </td>
                        <td style="text-align: right;">手机:
                        </td>
                        <td>
                            <input name="AcceptCellphone" id="AAcceptCellphone" class="easyui-textbox" data-options="required:false" style="width: 90px;" />
                        </td>

                    </tr>

                    <tr>
                        <td style="text-align: right;">销售费用:
                        </td>
                        <td>
                            <input name="TransportFee" id="TransportFee" data-options="min:0,precision:2,required:true" class="easyui-numberbox"
                                style="width: 60px;" />
                        </td>
                        <td style="text-align: right;">送货费用:
                        </td>
                        <td>
                            <input name="TransitFee" id="TransitFee" data-options="min:0,precision:2" class="easyui-numberbox"
                                style="width: 60px;" />
                        </td>
                        <td style="text-align: right; display: none"></td>
                        <td style="display: none"></td>
                        <td style="text-align: right; display: none">其它费用:
                        </td>
                        <td style="display: none">
                            <input name="OtherFee" id="OtherFee" class="easyui-numberbox" data-options="min:0,precision:2"
                                style="width: 80px;" />
                        </td>
                        <td style="text-align: right; color: #f53333">报销费用:
                        </td>
                        <td>
                            <input name="InsuranceFee" id="InsuranceFee" data-options="min:0,precision:2" class="easyui-numberbox"
                                style="width: 100px;" />
                        </td>
                        <td style="text-align: right; display: none">回扣:
                        </td>
                        <td style="display: none">
                            <input name="Rebate" id="Rebate" data-options="min:0,precision:2" class="easyui-numberbox"
                                style="width: 90px;" />
                        </td>
                        <td style="text-align: right;">费用合计:
                        </td>
                        <td>
                            <input name="TotalCharge" id="TotalCharge" class="easyui-numberbox" data-options="min:0,precision:2,required:true"
                                style="width: 80px;" />
                        </td>
                        <td style="text-align: right;">付款方式:
                        </td>
                        <td>
                            <select class="easyui-combobox" id="CheckOutType" name="CheckOutType" style="width: 60px;" panelheight="auto" editable="false">
                                <option value="2">月结</option>
                                <option value="0">现付</option>
                                <option value="4">代收</option>
                            </select>
                        </td>
                        <td style="text-align: right;">物流:
                        </td>
                        <td>
                            <input name="LogisID" id="ALogisID" class="easyui-combobox" style="width: 90px;" data-options="panelHeight:'200px',valueField:'ID',textField:'LogisticName',url:'../systempage/sysService.aspx?method=QueryAllLogistic'"
                                panelheight="auto" />
                        </td>
                        <td style="text-align: right;">物流费:
                        </td>
                        <td>
                            <input name="DeliveryFee" id="DeliveryFee" data-options="min:0,precision:2" class="easyui-numberbox"
                                style="width: 90px;" />
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">总件数:
                        </td>
                        <td>
                            <input name="Piece" id="APiece" class="easyui-numberbox" data-options="min:0,precision:0,required:true"
                                style="width: 60px;" readonly="true" />
                        </td>
                        <td style="text-align: right;">开单员:
                        </td>
                        <td>
                            <input name="CreateAwb" id="CreateAwb" class="easyui-textbox" readonly="true" style="width: 60px;" />
                        </td>
                        <td style="text-align: right;">开单时间:
                        </td>
                        <td colspan="3">
                            <input name="CreateDate" id="CreateDate" class="easyui-datetimebox" data-options="formatter:AllDateTime"
                                readonly="true" style="width: 150px;" />
                        </td>
                        <td style="text-align: right;">业务员:
                        </td>
                        <td>
                            <input name="SaleManID" id="SaleManID" class="easyui-combobox" style="width: 100px;"
                                data-options="url: 'orderApi.aspx?method=QueryUserByDepCode',valueField: 'LoginName',textField: 'UserName', onSelect: onSaleManIDChanged," />
                        </td>
                        <td style="text-align: right;">单号:
                        </td>
                        <td>
                            <input name="LogisAwbNo" id="ALogisAwbNo" class="easyui-textbox" style="width: 90px;" />
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" rowspan="2">备注:
                        </td>
                        <td colspan="7" rowspan="2">
                            <textarea name="Remark" id="ARemark" rows="3" style="width: 450px; resize: none"></textarea>
                        </td>
                        <td style="text-align: right;">付款人:</td>
                        <td>
                            <input name="PayClientNum" id="APayClientNum" style="width: 100px;" class="easyui-combobox" /></td>

                        <td style="text-align: right;">仓库:</td>
                        <td>
                            <input id="ThrowID" class="easyui-combobox" style="width: 90px;" /></td>
                        <td style="text-align: right;"></td>
                        <td colspan="2"></td>
                    </tr>
                    <tr>
                        <td colspan="6" style="text-align: left;" id="OrderModel">
                            <input class="FTS" type="checkbox" name="ThrowGood" id="ChangHe" value="5" />
                            <label class="FTS" for="ChangHe">长和订单</label>
                            <input class="FTS" type="checkbox" name="ThrowGood" id="ShangMao" value="6" />
                            <label class="FTS" for="ShangMao">商贸订单</label>
                            <input class="FTS" type="checkbox" name="ThrowGood" id="qihang" value="10" />
                            <label class="FTS" for="qihang">祺航订单</label>
                            <input class="FTS" type="checkbox" name="ThrowGood" id="QiTa" value="7" />
                            <label class="FTS" for="QiTa">其它订单</label>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
        <table>
            <tr>
                <td>
                    <input type="hidden" id="dgSaveAwbStatus" />
                    <input type="hidden" id="dgSaveoldPiece" />
                    <table id="dgSave" class="easyui-datagrid">
                    </table>
                </td>
                <td>
                    <table id="outDg" class="easyui-datagrid">
                    </table>
                </td>
            </tr>
        </table>
        <div id="toolbar">
            名称:<input id="AProductName" class="easyui-textbox" data-options="prompt:'请输入产品名称'" style="width: 110px">
            区域大仓:
            <input id="AHID" class="easyui-combobox" style="width: 100px;" readonly="readonly" data-options="required:true"
                panelheight="auto" />
            <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="queryInCargoProduct()">查询</a>
            <br />
            编码:<input id="AGoodsCode" class="easyui-textbox" data-options="prompt:'请输入富添盛编码'" style="width: 110px">
            所在仓库: 
            <input id="HID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'AreaID',textField:'Name',required:true"
                panelheight="auto" />

        </div>

    </div>
    <div id="dlgOrder-buttons">
        <table style="width: 100%">
            <tr>
                <td>
                    <a href="#" class="easyui-linkbutton" iconcls="icon-basket_put" plain="false" onclick="plup()" id="up">&nbsp;拉上订单&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="SaveOrderUpdate()" id="save">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" id="payprint" iconcls="icon-printer" onclick="prePrint()">&nbsp;打印发货单&nbsp;</a>&nbsp;&nbsp;
                    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closeDlg()">&nbsp;关&nbsp;闭&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>

    <!--Begin 出库操作-->

    <div id="dlgOutCargo" class="easyui-dialog" style="width: 350px; height: 200px; padding: 5px 5px"
        closed="true" buttons="#dlgOutCargo-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" id="InPiece" />
            <input type="hidden" id="InIndex" />
            <table>
                <tr>
                    <td style="text-align: right;">拉上订单数量:
                    </td>
                    <td>
                        <input name="Numbers" id="Numbers" class="easyui-numberbox" data-options="min:0,precision:0"
                            style="width: 200px;">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">业务员价格：</td>
                    <td>
                        <input name="ActSalePrice" id="ActSalePrice" class="easyui-numberbox" data-options="min:0,precision:2"
                            style="width: 200px;">
                    </td>
                </tr>
            </table>
            <div id="lblRule" style="display: none;"></div>
        </form>
    </div>
    <div id="dlgOutCargo-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="outOK()">&nbsp;确&nbsp;定&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgOutCargo').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <!--End 出库操作-->
   

    <object id="LODOP_OB" title="dd" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA"
        width="0px" height="0px">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0px" height="0px"></embed>
    </object>
    <script type="text/javascript">
        //复选框只能选中一个
        $(function () {
            $('#OrderModel').find('input[type=checkbox]').bind('click', function () {
                var id = $(this).attr("id");
                if (this.checked) {
                    $("#OrderModel").find('input[type=checkbox]').not(this).attr("checked", false);
                }
            });
        })


        //删除订单信息
        function DelItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            for (var i = 0; i < rows.length; i++) {
                if (rows[i].AwbStatus != 0) {
                    if ("<%=UserInfor.LoginName%>" != "1000" && "<%=UserInfor.LoginName%>" != "2076") {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', rows[i].OrderNo + '已出库无法删除！', 'warning'); return;
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
        //确认订单
        function Confirm() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要确认的订单！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定操作？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在提交中...' });
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'orderApi.aspx?method=PreOrderConfirm',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确认成功！', 'info');
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
        //保存订单
        function SaveOrderUpdate() {
            var row = $('#dgSave').datagrid('getRows');
            if (row.length <= 0) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单列表中没有数据', 'warning'); return; }

            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定保存？', function (r) {
                if (r) {
                    var json = JSON.stringify(row);
                    $.messager.progress({ msg: '请稍后,正在保存中...' });

                    $('#fmDep').form('submit', {
                        url: 'orderApi.aspx?method=updatePreOrderData',
                        onSubmit: function (param) {
                            param.TranHouse = $('#ThrowID').combobox('getText');
                            param.PayClientName = $('#APayClientNum').combobox('getText');
                            param.submitData = json;
                            var trd = $(this).form('enableValidation').form('validate');
                            return trd;
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
                            } else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                            }
                        }
                    })
                }
            });
        }
        //拉下订单
        function pldown() {
            var rows = $('#dgSave').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要拉下订单的数据！', 'warning');
                return;
            }
            var copyRows = [];
            var tCharge = $('#TransportFee').numberbox('getValue') == null || $('#TransportFee').numberbox('getValue') == "" ? 0 : Number($('#TransportFee').numberbox('getValue'));
            for (var i = 0, l = rows.length; i < l; i++) {
                var row = rows[i];
                copyRows.push(row);
                var p = $('#APiece').val() == "" || isNaN($('#APiece').val()) ? 0 : Number($('#APiece').val());
                var pt = p - Number(row.Piece);
                $('#APiece').numberbox('setValue', Number(pt));
                var SalePrice = Number(row.ActSalePrice);//销售价
                var NC = SalePrice * Number(row.Piece);
                $('#TransportFee').numberbox('setValue', tCharge - NC);

                var index = $('#outDg').datagrid('getRowIndex', copyRows[i].ID);
                if (index >= 0) {

                    var Trow = $("#outDg").datagrid('getData').rows[index];
                    Trow.Piece = Trow.InPiece;
                    $('#outDg').datagrid('updateRow', { index: index, row: Trow });
                } else {
                    //$('#outDg').datagrid('updateRow', { index: 1, row: row });
                }
            }
            for (var i = 0; i < copyRows.length; i++) {
                var index = $('#dgSave').datagrid('getRowIndex', copyRows[i]);
                $('#dgSave').datagrid('deleteRow', index);
            }
        }
        //新增出库数据
        function outOK() {
            var row = $('#outDg').datagrid('getSelected');
            var Total = Number(row.Piece);
            var SalePrice = $('#ActSalePrice').numberbox('getValue');// Number(row.SalePrice);//销售价
            //对产品进行价格管控
            if (SalePrice == "0.00" || SalePrice == '' || SalePrice == undefined) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请输入业务员销售价格！', 'warning');
                return;
            }
            
            var OutCargoID;
            var dgS = $('#dgSave').datagrid('getRows');
            for (var i = 0; i < dgS.length; i++) {
                OutCargoID = dgS[i].OutCargoID;
                if (dgS[i].GoodsCode == row.GoodsCode) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '该产品已在订单上，请直接修改数量操作！', 'warning');
                    return;
                }
            }
            var tCharge = $('#TransportFee').numberbox('getValue') == null || $('#TransportFee').numberbox('getValue') == "" ? 0 : Number($('#TransportFee').numberbox('getValue'));
            var Aindex = $('#InIndex').val();
            var indexD = $('#dgSave').datagrid('getRowIndex', row.ID);
            if (indexD < 0) {
                row.Piece = Number($('#Numbers').numberbox('getValue'));
                row.ActSalePrice = SalePrice;
                row.OutCargoID = OutCargoID;
                row.OrderNo = $('#OrderNo').val();
                var json = JSON.stringify([row])
                //$('#dgSave').datagrid('appendRow', row);
                var p = $('#APiece').val() == "" || isNaN($('#APiece').val()) ? 0 : Number($('#APiece').val());
                var pt = p + Number($('#Numbers').numberbox('getValue'));
                $('#APiece').numberbox('setValue', Number(pt));
                var NC = SalePrice * Number($('#Numbers').numberbox('getValue'));
                $('#TransportFee').numberbox('setValue', tCharge + NC);
                $('#dlgOutCargo').dialog('close');
                if (Total != Number($('#Numbers').numberbox('getValue'))) {
                    row.Piece = Total - Number($('#Numbers').numberbox('getValue'));
                } else {
                    row.Piece = 0;
                }
                $('#outDg').datagrid('updateRow', { index: Aindex, row: row });


                $.messager.progress({ msg: '请稍后,拉上订单中...' });
                $.ajax({
                    url: 'orderApi.aspx?method=DrawUpPreOrder',
                    type: 'post', dataType: 'json', data: { data: json },
                    success: function (text) {
                        $.messager.progress("close");
                        if (text.Result == true) {
                            //刷新列表
                            $('#dgSave').datagrid('clearSelections');
                            var gridOpts = $('#dgSave').datagrid('options');
                            gridOpts.url = 'orderApi.aspx?method=QueryPreOrderByOrderNo';
                            $('#dgSave').datagrid('load', {
                                OrderNo: $('#OrderNo').val()
                            });
                        }
                        else {
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                        }
                    }
                });
            } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单列表已存在该产品，请先删除再添加！', 'warning'); }
        }
        ///拉上订单
        function plup() {
            var row = $('#outDg').datagrid('getSelected');

<%--            var sp = "<%=UserInfor.SpecialCreateAwb%>";
            if (sp == "0" || sp == '' || sp == undefined) {
                //没有特殊下单权限，需要验证先进先出
                var rows = $('#outDg').datagrid('getRows');
                for (var i = 0; i < rows.length; i++) {
                    var rw = rows[i];
                    if (rw.Specs == row.Specs && rw.Figure == row.Figure && rw.Model == row.Model && rw.BatchYear == row.BatchYear) {
                        if (row.BatchWeek > rw.BatchWeek && rw.Piece > 0) {
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请优先出库周期早的产品！', 'warning');
                            return;
                        }
                    }
                }
            }--%>

            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要拉上订单的数据！', 'warning');
                return;
            }
            if (Number(row.TypeParentID) == 1 && Number(row.SalePrice) <= 0) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请先录入销售价格', 'warning');
                return;
            }
            if (row) {
                var clientNum = $('#ClientNum').val();
                $('#dlgOutCargo').dialog('open').dialog('setTitle', '拉上  ' + row.ProductName + ' 规格：' + row.Specs);
                $('#InPiece').val(row.Piece);
                $('#InIndex').val($('#outDg').datagrid('getRowIndex', row));
                $('#Numbers').numberbox('setValue', '');
                $('#ActSalePrice').numberbox('setValue', row.SalePrice);

            }
        }
        //查询在库产品
        function queryInCargoProduct() {
            if ($("#AHID").combobox('getValue') == undefined || $("#AHID").combobox('getValue') == '') {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择区域大仓！', 'warning');
                return;
            }
            if ($("#HID").combobox('getValue') == undefined || $("#HID").combobox('getValue') == '') {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择所在仓库！', 'warning');
                return;
            }
            var checkVal = 0;
            $("#OrderModel :checkbox").each(function (i) {
                var isCheck = $(this).prop("checked");
                if ('checked' == isCheck || isCheck) {
                    checkVal = $(this).val();
                    return true;
                }
            });
            if (checkVal == 0) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择订单类型！', 'warning');
                return;
            }
            $('#outDg').datagrid('clearSelections');
            var gridOpts = $('#outDg').datagrid('options');
            gridOpts.url = 'orderApi.aspx?method=PreQueryALLProduct';
            $('#outDg').datagrid('load', {
                ProductName: $('#AProductName').val(),
                GoodsCode: $('#AGoodsCode').val(),
                HAID: $("#HID").combobox('getValue'),
                PriceType: checkVal,
                HouseID: $("#AHID").combobox('getValue')//仓库ID
            });
        }
        //双击显示订单详细界面 
        function editItemByID(Did) {
            $('#dgSave').datagrid('loadData', { total: 0, rows: [] });
            IsModifyOrder = false;
            editIndex = undefined;
            var row = $("#dg").datagrid('getData').rows[Did];
            rowHouseID = row.HouseID;
            if (row) {
                $('#dgSaveAwbStatus').val(row.AwbStatus);
                $('#dlgOrder').dialog('open').dialog('setTitle', '修改订单：' + row.OrderNo);
                $('#fmDep').form('clear');
                $('#HiddenAcceptPeople').val(row.AcceptPeople);
                $('#HiddenClientNum').val(row.ClientNum);
                //客户姓名
                $('#AAcceptPeople').combobox({
                    valueField: 'Boss',
                    textField: 'Boss',
                    delay: '10',
                    url: '../Client/clientApi.aspx?method=AutoCompleteClient',
                    onSelect: onAcceptAddressChanged,
                    required: true
                });
                bindMethod();
                row.CreateDate = AllDateTime(row.CreateDate);
                $('#fmDep').form('load', row);
                $('#ThrowID').combobox('setText', row.TranHouse);
                $('#ThrwG').prop('checked', false);
                $('#Tran').prop('checked', false);
                $('#DaiFa').prop('checked', false);
                if (row.ThrowGood == "1") { $('#ThrwG').prop('checked', true); } else if (row.ThrowGood == "2") { $('#Tran').prop('checked', true); } else if (row.ThrowGood == "3") { $('#DaiFa').prop('checked', true); }
                if (row.IsPrintPrice == "1") { $('#IsPrintPrice').prop('checked', true); } else { $('#IsPrintPrice').prop('checked', false); }
                $('#up').show();
                $('#save').show();
                $('#payprint').show();
                $('#pickprint').show();
                var columns = [];
                columns.push({ title: '', field: 'ID', checkbox: true, width: '30px' });

                if (row.ModifyPriceStatus == "1") {
                    //灰掉按钮
                    $('#up').hide();
                    $('#save').hide();
                    $('#payprint').hide();
                    $('#pickprint').hide();
                    columns.push({ title: '出库件数', field: 'Piece', width: '60px' });
                    columns.push({ title: '业务员价', field: 'ActSalePrice', width: '60px' });
                } else {
                    columns.push({ title: '出库件数', field: 'Piece', width: '60px', editor: { type: 'numberbox' } });
                    columns.push({ title: '业务员价', field: 'ActSalePrice', width: '60px', editor: { type: 'numberbox', options: { precision: 2 } } });
                }
                columns.push({
                    title: '产品名称', field: 'ProductName', width: '110px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '富添盛编码', field: 'GoodsCode', width: '70px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '商贸编码', field: 'Model', width: '110px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '长和编码', field: 'Figure', width: '110px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '祺航编码', field: 'Size', width: '110px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '批次', field: 'Batch', width: '50px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '产品类型', field: 'TypeName', width: '60px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });

                columns.push({
                    title: '产品ID', field: 'ProductID', width: '60px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '备注', field: 'RuleTitle', width: '60px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                $("#APayClientNum").combobox("setValue", row.PayClientNum);
                showGrid(columns, row.HouseID);

                var gridOpts = $('#dgSave').datagrid('options');
                gridOpts.url = 'orderApi.aspx?method=QueryPreOrderByOrderNo&OrderNo=' + row.OrderNo;
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
        function showGrid(dgSaveCol, houseID) {
            var columns = [];
            columns.push({ title: '', field: 'ID', checkbox: true, width: '30px' });
            columns.push({
                title: '在库件数', field: 'Piece', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品名称', field: 'ProductName', width: '80px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '规格', field: 'Specs', width: '75px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '富添盛编码', field: 'GoodsCode', width: '70px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '商贸编码', field: 'Model', width: '110px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '长和编码', field: 'Figure', width: '110px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '祺航编码', field: 'Size', width: '110px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '批次', field: 'Batch', width: '50px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '销售价', field: 'CostPriceStore', width: '50px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品类型', field: 'TypeName', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '产品ID', field: 'ProductID', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });

            $('#outDg').datagrid({
                width: '450px',
                height: '310px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [columns],
                onDblClickRow: function (index, row) { plup(index); }
            });
            $('#dgSave').datagrid({
                width: '600px',
                height: '310px',
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
                columns: [dgSaveCol],
                onClickCell: onClickCell
            });
        }

        $.extend($.fn.datagrid.methods, {
            editCell: function (jq, param) {
                return jq.each(function () {
                    var fields = $(this).datagrid('getColumnFields');
                    for (var i = 0; i < fields.length; i++) {
                        var col = $(this).datagrid('getColumnOption', fields[i]);
                        col.editor1 = col.editor;
                        if (fields[i] != param.field) {
                            col.editor = null;
                        }
                    }
                    $(this).datagrid('beginEdit', param.index);
                    for (var i = 0; i < fields.length; i++) {
                        var col = $(this).datagrid('getColumnOption', fields[i]);
                        col.editor = col.editor1;
                    }
                });
            }
        });
        var IsModifyOrder = false;
        var editIndex = undefined;
        function endEditing() {
            if (editIndex == undefined) { return true }
            if ($('#dgSave').datagrid('validateRow', editIndex)) {
                var rows = $("#dgSave").datagrid('getData').rows[editIndex];
                var ed = $('#dgSave').datagrid('getEditors', editIndex);
                var cg = ed[0];
                if (cg == undefined) { return false; }
                var sum = 0;
                if (cg.field == "Piece") {
                    //修改件数
                    var oldPiece = Number(rows.Piece);
                    var salePrice = Number(rows.ActSalePrice);
                    var newPiece = Number(cg.target.val());
                    if (oldPiece == newPiece) {
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        return;
                    }
                    if (newPiece < 0) { newPiece = 0; }

                    var row = $('#dgSave').datagrid('getRows').length;
                    var count = 0;
                    for (var i = 0; i < row; i++) {
                        if (i == editIndex) {
                            count = Number(count) + newPiece;
                        } else {
                            count = Number(count) + Number($("#dgSave").datagrid('getData').rows[i].Piece);
                        }
                    }
                    if (count == 0) {
                        rows.Piece = oldPiece;
                        $('#dgSave').datagrid('refreshRow', editIndex);
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单出库总件数必须大于0!', 'info');
                        return;
                    }

                    //修改件数
                    var OrderNo = $('#OrderNo').val();
                    var GoodsCode = rows.GoodsCode;
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    $.ajax({
                        url: 'orderApi.aspx?method=UpdatePreOrderPiece',
                        type: 'post', dataType: 'json', data: { OrderNo: OrderNo, GoodsCode: GoodsCode, oldPiece: oldPiece, newPiece: newPiece },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            $.messager.progress("close");
                            if (text.Result == true) {
                                IsModifyOrder = true;
                                var ModifyPiece = 0, ModifyPrice = 0;
                                //说明是增加了数量
                                if (newPiece > oldPiece) {
                                    ModifyPiece = newPiece - oldPiece;
                                    ModifyPrice = ModifyPiece * salePrice;
                                    var TPiece = Number($('#APiece').numberbox('getValue'));
                                    $('#APiece').numberbox('setValue', Number(TPiece + ModifyPiece));
                                    var TFee = Number($('#TransportFee').numberbox('getValue'));
                                    $('#TransportFee').numberbox('setValue', Number(TFee + ModifyPrice).toFixed(2));
                                    qh();
                                } else {
                                    ModifyPiece = oldPiece - newPiece;
                                    ModifyPrice = ModifyPiece * salePrice;

                                    var TPiece = Number($('#APiece').numberbox('getValue'));
                                    $('#APiece').numberbox('setValue', Number(TPiece - ModifyPiece));
                                    var TFee = Number($('#TransportFee').numberbox('getValue'));
                                    $('#TransportFee').numberbox('setValue', Number(TFee - ModifyPrice).toFixed(2));
                                    qh();
                                }
                                alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                $('#dgSave').datagrid('rejectChanges');
                                editIndex = undefined;
                            }
                        }
                    });

                }
                if (cg.field == "ActSalePrice") {
                    var oldPrice = Number(rows.ActSalePrice);//旧价格
                    var piece = Number(rows.Piece);//新件数
                    var newPrice = Number(cg.target.val());//新价格
                    if (oldPrice == newPrice) {
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        return;
                    }
                    if (newPiece < 0) { newPiece = 0; }

                    var row = $('#dgSave').datagrid('getRows').length;
                    var count = 0;
                    for (var i = 0; i < row; i++) {
                        count = Number(count) + Number($("#dgSave").datagrid('getData').rows[i].Piece);
                    }
                    if (count == 0) {
                        rows.Piece = oldPiece;
                        $('#dgSave').datagrid('refreshRow', editIndex);
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单出库总件数必须大于0!', 'info');
                            return;
                        }

                        var salePrice = Number(rows.SalePrice);
                        var IsModifyPrice = "<%=UserInfor.IsModifyPrice%>";
                    if (IsModifyPrice == undefined || IsModifyPrice == "0") {
                        if (newPrice * 1 < salePrice * 1) {
                            $('#dgSave').datagrid('endEdit', editIndex);
                            editIndex = undefined;
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', "业务员价格不能低于销售价格!", 'warning');
                            return;
                        }
                    }
                    rows.SalePrice = oldPrice;
                    rows.ActSalePrice = cg.target.val();
                    rows.OrderNo = $('#OrderNo').val();
                    var json = JSON.stringify([rows])
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    $.ajax({
                        url: 'orderApi.aspx?method=UpdatePreOrderSalePrice',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                IsModifyOrder = true;
                                var ModifyPiece = 0, ModifyPrice = 0;
                                //说明是增加价格
                                if (newPrice > oldPrice) {
                                    ModifyPrice = newPrice - oldPrice;
                                    ModifyPiece = ModifyPrice * piece;
                                    var TFee = Number($('#TransportFee').numberbox('getValue'));
                                    $('#TransportFee').numberbox('setValue', Number(TFee + ModifyPiece).toFixed(2));
                                    qh();
                                } else {
                                    ModifyPrice = oldPrice - newPrice;
                                    ModifyPiece = ModifyPrice * piece;

                                    var TFee = Number($('#TransportFee').numberbox('getValue'));
                                    $('#TransportFee').numberbox('setValue', Number(TFee - ModifyPiece).toFixed(2));
                                    qh();
                                }
                                alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                $('#dgSave').datagrid('rejectChanges');
                                editIndex = undefined;
                            }
                        }
                    });
                }
                $('#dgSave').datagrid('endEdit', editIndex);
                editIndex = undefined;
                return true;
            } else {
                return false;
            }
        }
        function onClickCell(index, field) {
            if ($('#dgSaveAwbStatus').val() * 1 < 1 && field == "Piece" || field == "ActSalePrice") {
                if (endEditing()) {
                    $('#dgSave').datagrid('selectRow', index)
                        .datagrid('editCell', { index: index, field: field });
                    editIndex = index;
                }
            } else {
                if (editIndex == undefined) { return true }
                var rows = $("#dgSave").datagrid('getData').rows[editIndex];
                var ed = $('#dgSave').datagrid('getEditors', editIndex);
                var cg = ed[0];
                if (cg == undefined) {
                    return true;
                }
                var sum = 0;
                if (cg.field == "Piece") {
                    var oldPiece = Number(rows.Piece);
                    var salePrice = Number(rows.ActSalePrice);
                    var newPiece = Number(cg.target.val());
                    if (oldPiece == newPiece) {
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        return;
                    }
                    if (newPiece < 0) { newPiece = 0; }
                    //修改件数
                    rows.Piece = newPiece;
                    rows.oldPiece = oldPiece;
                    rows.OrderNo = $('#OrderNo').val();

                    var row = $('#dgSave').datagrid('getRows').length;
                    var count = 0;
                    for (var i = 0; i < row; i++) {
                        count = Number(count) + Number($("#dgSave").datagrid('getData').rows[i].Piece);
                    }
                    if (count == 0) {
                        rows.Piece = oldPiece;
                        $('#dgSave').datagrid('refreshRow', index);
                        $('#dgSave').datagrid('endEdit', editIndex);
                        editIndex = undefined;
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单出库总件数必须大于0!', 'info');
                        return;
                    }
                    var OrderNo = $('#OrderNo').val();
                    var GoodsCode = rows.GoodsCode;
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    $.ajax({
                        url: 'orderApi.aspx?method=UpdatePreOrderPiece',
                        type: 'post', dataType: 'json', data: { OrderNo: OrderNo, GoodsCode: GoodsCode, oldPiece: oldPiece, newPiece: newPiece },
                        success: function (text) {
                            $.messager.progress("close");
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                IsModifyOrder = true;
                                var ModifyPiece = 0, ModifyPrice = 0;
                                //说明是增加了数量
                                if (newPiece > oldPiece) {
                                    ModifyPiece = newPiece - oldPiece;
                                    ModifyPrice = ModifyPiece * salePrice;
                                    var TPiece = Number($('#APiece').numberbox('getValue'));
                                    $('#APiece').numberbox('setValue', Number(TPiece + ModifyPiece));
                                    var TFee = Number($('#TransportFee').numberbox('getValue'));
                                    $('#TransportFee').numberbox('setValue', Number(TFee + ModifyPrice).toFixed(2));
                                    qh();
                                } else {

                                    ModifyPiece = oldPiece - newPiece;
                                    ModifyPrice = ModifyPiece * salePrice;

                                    var TPiece = Number($('#APiece').numberbox('getValue'));
                                    $('#APiece').numberbox('setValue', Number(TPiece - ModifyPiece));
                                    var TFee = Number($('#TransportFee').numberbox('getValue'));
                                    $('#TransportFee').numberbox('setValue', Number(TFee - ModifyPrice).toFixed(2));
                                    qh();
                                }
                                alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                                }
                                else {
                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                    $('#dgSave').datagrid('rejectChanges');
                                    editIndex = undefined;
                                }
                            }
                        });

                    }
                    if (cg.field == "ActSalePrice") {
                        //修改销售价
                        var oldPrice = Number(rows.ActSalePrice);//旧价格
                        var piece = Number(rows.Piece);//新件数
                        var newPrice = Number(cg.target.val());//新价格
                        if (oldPrice == newPrice) {
                            $('#dgSave').datagrid('endEdit', editIndex);
                            editIndex = undefined;
                            return;
                        }
                        if (newPiece < 0) { newPiece = 0; }
<%--                        if (count == 0) {
                            rows.Piece = oldPiece;
                            $('#dgSave').datagrid('refreshRow', index);
                            $('#dgSave').datagrid('endEdit', editIndex);
                            editIndex = undefined;
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单出库总件数必须大于0!', 'info');
                        return;
                    }--%>

                    var row = $('#dgSave').datagrid('getRows').length;
                    var count = 0;
                    //for (var i = 0; i < row; i++) {
                    //    count = Number(count) + Number($("#dgSave").datagrid('getData').rows[i].Piece);
                    //}
                    var salePrice = Number(rows.SalePrice);
                    var IsModifyPrice = "<%=UserInfor.IsModifyPrice%>";
                    if (IsModifyPrice == undefined || IsModifyPrice == "0") {
                        if (newPrice * 1 < salePrice * 1) {
                            $('#dgSave').datagrid('endEdit', editIndex);
                            editIndex = undefined;
                            rows.ActSalePrice = oldPrice;
                            $('#dgSave').datagrid('refreshRow', index);
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', "业务员价格不能低于销售价格!", 'warning');
                            return;
                        }
                    }
                    rows.SalePrice = oldPrice;
                    rows.ActSalePrice = cg.target.val();
                    rows.OrderNo = $('#OrderNo').val();
                    var json = JSON.stringify([rows])
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    $.ajax({
                        url: 'orderApi.aspx?method=UpdatePreOrderSalePrice',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                IsModifyOrder = true;
                                var ModifyPiece = 0, ModifyPrice = 0;
                                //说明是增加价格
                                if (newPrice > oldPrice) {
                                    ModifyPrice = newPrice - oldPrice;
                                    ModifyPiece = ModifyPrice * piece;
                                    var TFee = Number($('#TransportFee').numberbox('getValue'));
                                    $('#TransportFee').numberbox('setValue', Number(TFee + ModifyPiece).toFixed(2));
                                    qh();
                                } else {
                                    ModifyPrice = oldPrice - newPrice;
                                    ModifyPiece = ModifyPrice * piece;

                                    var TFee = Number($('#TransportFee').numberbox('getValue'));
                                    $('#TransportFee').numberbox('setValue', Number(TFee - ModifyPiece).toFixed(2));
                                    qh();
                                }
                                alert_autoClose('<%= Cargo.Common.GetSystemNameAndVersion()%>', '修改成功!', 'info');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                                $('#dgSave').datagrid('rejectChanges');
                                editIndex = undefined;
                            }
                        }
                    });
                }

                $('#dgSave').datagrid('endEdit', editIndex);
                editIndex = undefined;
            }
        }
        //绑定费用框
        function bindMethod() {
            $("#TransitFee").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
            $("#TransportFee").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
            //$("#DeliveryFee").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
            $("#OtherFee").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
            $("#InsuranceFee").numberbox({ "onChange": function (newValue, oldValue) { qh(); } });
        }
        function qh() {
            var t = Number($('#TransitFee').numberbox('getValue')) + Number($('#TransportFee').numberbox('getValue')) + Number($('#OtherFee').numberbox('getValue')) - Number($('#InsuranceFee').combogrid('getText'));;
            $('#TotalCharge').numberbox('setValue', Number(t).toFixed(2));
        }
        //业务员选择方法
        function onSaleManIDChanged(item) {
            if (item) {
                $('#SaleManName').val(item.UserName);
                $('#SaleCellPhone').val(item.CellPhone);
            }
        }
        //收货人自动选择方法
        function onAcceptAddressChanged(item) {
            if (item) {
                var AAcceptPeople = $('#AAcceptPeople').combobox('getValue');
                var HiddenAcceptPeople = $('#HiddenAcceptPeople').val();
                if (AAcceptPeople != HiddenAcceptPeople) {
                    var outDgrows = $("#dgSave").datagrid('getData').rows;
                    if (outDgrows.length > 0) {
                        //var LimitType = 0;
                        for (var i = 0; i < outDgrows.length; i++) {
                            $('#AAcceptUnit').textbox('setValue', item.ClientName);
                            $('#AAcceptAddress').textbox('setValue', item.Address);
                            $('#AAcceptTelephone').textbox('setValue', item.Telephone);
                            $('#AAcceptCellphone').textbox('setValue', item.Cellphone);
                            $('#HiddenAcceptPeople').val(item.Boss);
                            $('#ClientNum').val(item.ClientNum);
                            if (HiddenAcceptPeople == $('#APayClientNum').combobox('getText')) {
                                $('#APayClientNum').combobox('setValue', item.ClientNum);
                                $('#APayClientNum').combobox('setText', item.Boss);
                            }

                        }
                    }
                } else {
                    $('#AAcceptUnit').textbox('setValue', item.ClientName);
                    $('#AAcceptAddress').textbox('setValue', item.Address);
                    $('#AAcceptTelephone').textbox('setValue', item.Telephone);
                    $('#AAcceptCellphone').textbox('setValue', item.Cellphone);
                    $('#HiddenAcceptPeople').val(item.Boss);
                    $('#ClientNum').val(item.ClientNum);
                    if (HiddenAcceptPeople == $('#APayClientNum').combobox('getText')) {
                        $('#APayClientNum').combobox('setValue', item.ClientNum);
                        $('#APayClientNum').combobox('setText', item.Boss);
                    }
                }
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
        var LODOP;
        //打印发货单 
        function prePrint() {
            var ThrowGoodId = "";
            $('input[name="ThrowGood"]:checked').each(function () {
                ThrowGoodId = $(this).val();

            });
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            var griddata = $('#dgSave').datagrid('getRows');
            for (var i = 0; i < griddata.length; i++) {
                if (griddata[i].Piece <= 0) {
                    griddata.splice(i, 1);
                }
            }
            if (rowHouseID != 47) {
                var json = JSON.stringify(griddata)
                var returnType = false;
                $.ajax({
                    url: 'orderApi.aspx?method=QueryOrderDataForPrePrint',
                    type: 'post', dataType: 'json', data: { data: json },
                    async: false,
                    success: function (data) {
                        if (data == null || data == undefined || data.length < 1) {
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有可打印的发货单数据！', 'warning');
                            returnType = true;
                            return;
                        } else {
                            griddata = data;
                        }
                    }
                });
            }
            if (returnType) { return; }

            var js = 0, Alltotal = 0, AllPiece = 0; p = 1; pie = 0; total = 0; AllSalePrice = 0;

            //长和送货单
            if (ThrowGoodId == 5) {
                for (var k = 0; k < griddata.length; k++) {
                    AllPiece += Number(griddata[k].Piece) * Number(griddata[k].ActSalePrice);
                    Alltotal += Number(griddata[k].Piece);
                    AllSalePrice += Number(griddata[k].ActSalePrice)
                }
                for (var i = 0; i < griddata.length; i++) {
                    if (i == (p - 1) * 5) {
                        if (p > 1) {
                            LODOP.NewPage();
                        }
                        p++;

                        LODOP.SET_PRINT_PAGESIZE(3, 2100, 30, "");
                        LODOP.ADD_PRINT_TEXT(5, 101, 574, 33, "广州广汽商贸长和汽车科技有限公司   发货单");
                        LODOP.SET_PRINT_STYLEA(0, "FontName", "新宋体");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 19);
                        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                        LODOP.ADD_PRINT_TEXT(43, 437, 89, 25, "发货单号：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.SET_PRINT_STYLEA(0, "FontColor", "#FF0000");
                        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                        LODOP.ADD_PRINT_TEXT(43, 517, 183, 25, $('#OrderNo').val());//发货单号
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(43, 167, 90, 25, "发货日期：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.SET_PRINT_STYLEA(0, "FontColor", "#FF0000");
                        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                        LODOP.ADD_PRINT_TEXT(43, 249, 174, 25, getNowFormatDate($('#CreateDate').datetimebox('getValue')));//开单日期
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(79, 6, 85, 25, "客户名称：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(101, 6, 85, 25, "客户地址：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(124, 6, 85, 25, "收货人：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(79, 91, 675, 25, $('#AAcceptUnit').combobox('getText'));//客户名称
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(101, 91, 675, 25, $('#AAcceptAddress').textbox('getValue'));//客户地址
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(124, 91, 151, 25, $('#AAcceptPeople').textbox('getValue'));//收货人
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(124, 245, 85, 25, "联系电话：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        var sphone = $('#AAcceptCellphone').textbox('getValue');
                        if (sphone == "") {
                            sphone = $('#AAcceptTelephone').textbox('getValue');
                        }
                        LODOP.ADD_PRINT_TEXT(124, 330, 436, 25, sphone);
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_RECT(150, 0, 33, 36, 0, 1);


                        LODOP.ADD_PRINT_TEXT(165, 2, 39, 25, "序号");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_RECT(150, 33, 112, 36, 0, 1);
                        LODOP.ADD_PRINT_TEXT(164, 61, 87, 25, "长和编码");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(165, 225, 40, 25, "品名");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_RECT(150, 145, 183, 36, 0, 1);
                        LODOP.ADD_PRINT_TEXT(163, 364, 70, 25, "规格/型号");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_RECT(150, 328, 126, 36, 0, 1);
                        LODOP.ADD_PRINT_TEXT(153, 544, 63, 33, "对店销售     单价");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);
                        LODOP.ADD_PRINT_RECT(150, 540, 61, 36, 0, 1);
                        LODOP.ADD_PRINT_TEXT(164, 507, 40, 25, "数量");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_RECT(150, 500, 40, 36, 0, 1);
                        LODOP.ADD_PRINT_TEXT(165, 621, 49, 25, "小计");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_RECT(150, 601, 68, 36, 0, 1);
                        LODOP.ADD_PRINT_TEXT(164, 464, 41, 25, "单位");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(165, 706, 49, 25, "备注");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_RECT(150, 668, 102, 36, 0, 1);

                        LODOP.ADD_PRINT_RECT(71, 0, 770, 79, 0, 1);
                        LODOP.ADD_PRINT_LINE(151, 0, 398, 1, 0, 1);
                        LODOP.ADD_PRINT_LINE(149, 770, 398, 771, 0, 1);
                        LODOP.ADD_PRINT_RECT(396, 0, 770, 60, 0, 1);
                        LODOP.ADD_PRINT_RECT(371, 0, 702, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(371, 701, 69, 25, 0, 1);
                        LODOP.ADD_PRINT_TEXT(376, 271, 87, 22, "合计");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);


                        LODOP.ADD_PRINT_TEXT(435, 3, 162, 25, "库存备货人（签名）：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(464, 2, 671, 25, "本单一式四联，第一联（白）留广汽长和，第二联(红)留4S店，第三、四联(黄/绿）留送货单位。");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(435, 401, 167, 25, "收货人（签名/盖章）：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_TEXT(376, 704, 64, 22, Number(AllPiece).toFixed(2));
                        LODOP.ADD_PRINT_RECT(396, 0, 73, 35, 0, 1);
                        LODOP.ADD_PRINT_TEXT(407, 18, 55, 22, "备注：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        LODOP.ADD_PRINT_RECT(396, 73, 697, 35, 0, 1);
                        LODOP.ADD_PRINT_TEXT(398, 75, 688, 22, $('#ARemark').val());
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    }

                    LODOP.ADD_PRINT_RECT(186 + (i - (p - 2) * 5) * 37, 0, 33, 37, 0, 1);
                    LODOP.ADD_PRINT_RECT(186 + (i - (p - 2) * 5) * 37, 33, 112, 37, 0, 1);
                    LODOP.ADD_PRINT_RECT(186 + (i - (p - 2) * 5) * 37, 145, 183, 37, 0, 1);
                    LODOP.ADD_PRINT_RECT(186 + (i - (p - 2) * 5) * 37, 328, 126, 37, 0, 1);
                    LODOP.ADD_PRINT_RECT(186 + (i - (p - 2) * 5) * 37, 454, 46, 37, 0, 1);
                    LODOP.ADD_PRINT_RECT(186 + (i - (p - 2) * 5) * 37, 500, 40, 37, 0, 1);
                    LODOP.ADD_PRINT_RECT(186 + (i - (p - 2) * 5) * 37, 540, 61, 37, 0, 1);
                    LODOP.ADD_PRINT_RECT(186 + (i - (p - 2) * 5) * 37, 601, 68, 37, 0, 1);
                    LODOP.ADD_PRINT_RECT(186 + (i - (p - 2) * 5) * 37, 668, 102, 37, 0, 1);
                    LODOP.ADD_PRINT_TEXT(200 + (i - (p - 2) * 5) * 37, 12, 34, 20, i + 1);
                    LODOP.ADD_PRINT_TEXT(192 + (i - (p - 2) * 5) * 37, 37, 112, 20, griddata[i].Figure);//长和编码
                    LODOP.ADD_PRINT_TEXT(191 + (i - (p - 2) * 5) * 37, 151, 194, 20, griddata[i].ProductName);//品名
                    LODOP.ADD_PRINT_TEXT(191 + (i - (p - 2) * 5) * 37, 332, 135, 20, griddata[i].Specs);//规格
                    LODOP.ADD_PRINT_TEXT(199 + (i - (p - 2) * 5) * 37, 459, 51, 20, griddata[i].Package);//单位
                    LODOP.ADD_PRINT_TEXT(199 + (i - (p - 2) * 5) * 37, 507, 45, 20, Number(griddata[i].Piece));//数量
                    LODOP.ADD_PRINT_TEXT(198 + (i - (p - 2) * 5) * 37, 551, 66, 20, Number(griddata[i].ActSalePrice));//对店销售单价
                    LODOP.ADD_PRINT_TEXT(199 + (i - (p - 2) * 5) * 37, 612, 64, 20, (Number(griddata[i].Piece) * Number(griddata[i].ActSalePrice)).toFixed(2));
                    LODOP.ADD_PRINT_TEXT(191 + (i - (p - 2) * 5) * 37, 671, 106, 20, griddata[i].RuleTitle);

                }
            }
                //商贸送货单
            else if (ThrowGoodId == 6) {
                for (var k = 0; k < griddata.length; k++) {
                    AllPiece += Number(griddata[k].Piece) * Number(griddata[k].ActSalePrice);
                    Alltotal += Number(griddata[k].Piece);
                    AllSalePrice += Number(griddata[k].ActSalePrice)
                }
                for (var i = 0; i < griddata.length; i++) {
                    if (i == (p - 1) * 6) {
                        if (p > 1) {
                            LODOP.NewPage();
                        }
                        p++;
                        LODOP.SET_PRINT_PAGESIZE(3, 2100, 30, "");
                        LODOP.ADD_PRINT_TEXT(5, 126, 534, 33, "广汽商贸-广州市广汽商贸汽车用品有限公司");
                        LODOP.SET_PRINT_STYLEA(0, "FontName", "华文新魏");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 19);
                        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                        LODOP.ADD_PRINT_TEXT(85, 597, 54, 20, "单号：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(85, 642, 123, 20, $('#OrderNo').val());//发货单号
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(85, 441, 53, 20, "日期：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(85, 487, 111, 20, getNowFormatDate($('#CreateDate').datetimebox('getValue')));//开单日期
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_RECT(106, 0, 33, 36, 0, 1);

                        LODOP.ADD_PRINT_TEXT(121, 4, 39, 25, "序号");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_RECT(106, 33, 133, 36, 0, 1);
                        LODOP.ADD_PRINT_TEXT(120, 70, 62, 25, "物料代码");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(120, 221, 62, 25, "物料名称");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_RECT(106, 166, 161, 36, 0, 1);
                        LODOP.ADD_PRINT_TEXT(120, 378, 70, 25, "型号");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_RECT(106, 327, 126, 36, 0, 1);
                        LODOP.ADD_PRINT_TEXT(109, 538, 63, 33, "对销售店  单价");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);
                        LODOP.ADD_PRINT_RECT(106, 536, 55, 36, 0, 1);
                        LODOP.ADD_PRINT_TEXT(120, 506, 40, 25, "数量");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_RECT(106, 499, 37, 36, 0, 1);
                        LODOP.ADD_PRINT_TEXT(121, 598, 49, 25, "总金额");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_RECT(106, 590, 58, 36, 0, 1);
                        LODOP.ADD_PRINT_TEXT(119, 461, 41, 25, "单位");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_RECT(106, 453, 46, 36, 0, 1);
                        LODOP.ADD_PRINT_RECT(106, 647, 123, 36, 0, 1);
                        LODOP.ADD_PRINT_TEXT(121, 694, 49, 25, "备注");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);


                        LODOP.SET_PRINT_PAGESIZE(3, 2100, 30, "");
                        LODOP.ADD_PRINT_RECT(364, 536, 234, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(389, 0, 770, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(364, 166, 333, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(364, 590, 58, 25, 0, 1);
                        LODOP.ADD_PRINT_TEXT(370, 58, 41, 20, "小计");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(372, 593, 64, 22, Number(AllPiece).toFixed(2));
                        LODOP.ADD_PRINT_RECT(364, 499, 37, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(364, 0, 166, 25, 0, 1);
                        LODOP.ADD_PRINT_TEXT(395, 5, 164, 20, "  价税合计（大写/小写）");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_RECT(389, 166, 370, 25, 0, 1);
                        LODOP.ADD_PRINT_TEXT(399, 583, 162, 22, "￥" + Number(AllPiece).toFixed(2));
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(372, 506, 34, 22, Alltotal);
                        LODOP.ADD_PRINT_TEXT(396, 181, 346, 20, digitUppercase(AllPiece));
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);


                        LODOP.ADD_PRINT_LINE(180, 0, 364, 1, 0, 1);
                        LODOP.ADD_PRINT_LINE(180, 770, 364, 771, 0, 1);
                        LODOP.ADD_PRINT_RECT(389, 0, 770, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(364, 166, 333, 25, 0, 1);
                        LODOP.ADD_PRINT_TEXT(370, 58, 41, 20, "小计");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(474, 3, 254, 24, "配送服务商经手人（签字及盖章）：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(455, 3, 671, 25, "一式三联：第一联配送单位留存；第二、三联收货单位留存。");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(474, 433, 250, 24, "收货单位经手人（签字及盖章）：");


                        LODOP.ADD_PRINT_TEXT(38, 339, 90, 28, "送货单");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 18);
                        LODOP.ADD_PRINT_TEXT(65, 4, 90, 20, "配送服务商：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(65, 91, 347, 20, "广州富添盛汽车用品有限公司");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(85, 4, 96, 20, "收货单位：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(85, 91, 352, 20, $('#AAcceptUnit').combobox('getText'));//客户名称
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_RECT(364, 536, 55, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(364, 0, 166, 25, 0, 1);

                        LODOP.ADD_PRINT_RECT(414, 0, 166, 35, 0, 1);
                        LODOP.ADD_PRINT_TEXT(427, 67, 41, 20, "备注");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_RECT(414, 166, 604, 35, 0, 1);
                        LODOP.ADD_PRINT_IMAGE(3, 8, 115, 35, "<img src='../CSS/image/GacBusiness.png'/>");
                        LODOP.SET_PRINT_STYLEA(0, "Stretch", 1);
                        if ($('#ARemark').val() != null || $('#ARemark').val() != "") {
                            LODOP.ADD_PRINT_TEXT(418, 167, 599, 29, $('#ARemark').val());
                        } else {
                            LODOP.ADD_PRINT_TEXT(418, 167, 599, 29, "签字及盖章确认");
                        }
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 9);
                        LODOP.ADD_PRINT_TEXT(396, 181, 346, 20, digitUppercase(AllPiece));

                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    }
                    LODOP.ADD_PRINT_RECT(142 + (i - (p - 2) * 6) * 37, 0, 33, 37, 0, 1);
                    LODOP.ADD_PRINT_RECT(142 + (i - (p - 2) * 6) * 37, 33, 133, 37, 0, 1);
                    LODOP.ADD_PRINT_RECT(142 + (i - (p - 2) * 6) * 37, 166, 161, 37, 0, 1);
                    LODOP.ADD_PRINT_RECT(142 + (i - (p - 2) * 6) * 37, 327, 126, 37, 0, 1);
                    LODOP.ADD_PRINT_RECT(142 + (i - (p - 2) * 6) * 37, 453, 46, 37, 0, 1);
                    LODOP.ADD_PRINT_RECT(142 + (i - (p - 2) * 6) * 37, 499, 37, 37, 0, 1);
                    LODOP.ADD_PRINT_RECT(142 + (i - (p - 2) * 6) * 37, 536, 55, 37, 0, 1);
                    LODOP.ADD_PRINT_RECT(142 + (i - (p - 2) * 6) * 37, 590, 58, 37, 0, 1);
                    LODOP.ADD_PRINT_RECT(142 + (i - (p - 2) * 6) * 37, 647, 123, 37, 0, 1);

                    LODOP.ADD_PRINT_TEXT(153 + (i - (p - 2) * 6) * 37, 6, 34, 20, i + 1);
                    LODOP.ADD_PRINT_TEXT(147 + (i - (p - 2) * 6) * 37, 37, 137, 20, griddata[i].Model);//商贸编码
                    LODOP.ADD_PRINT_TEXT(147 + (i - (p - 2) * 6) * 37, 170, 165, 20, griddata[i].ProductName);//品名
                    LODOP.ADD_PRINT_TEXT(147 + (i - (p - 2) * 6) * 37, 335, 125, 20, griddata[i].Specs);//规格
                    LODOP.ADD_PRINT_TEXT(147 + (i - (p - 2) * 6) * 37, 458, 45, 20, griddata[i].Package);//单位
                    LODOP.ADD_PRINT_TEXT(147 + (i - (p - 2) * 6) * 37, 503, 45, 20, Number(griddata[i].Piece));//数量
                    LODOP.ADD_PRINT_TEXT(146 + (i - (p - 2) * 6) * 37, 540, 61, 20, Number(griddata[i].ActSalePrice));//对店销售单价
                    LODOP.ADD_PRINT_TEXT(146 + (i - (p - 2) * 6) * 37, 594, 64, 20, (Number(griddata[i].Piece) * Number(griddata[i].ActSalePrice)).toFixed(2));
                    LODOP.ADD_PRINT_TEXT(146 + (i - (p - 2) * 6) * 37, 653, 119, 20, griddata[i].RuleTitle);
                }
            }
                //富添盛送货单
            else if (ThrowGoodId == 7) {
                for (var k = 0; k < griddata.length; k++) {
                    AllPiece += Number(griddata[k].Piece) * Number(griddata[k].ActSalePrice);
                    Alltotal += Number(griddata[k].Piece);
                    AllSalePrice += Number(griddata[k].ActSalePrice)
                }
                LODOP.SET_PRINT_PAGESIZE(0, 2100, 2970, "A4");
                LODOP.ADD_PRINT_TEXT(4, 191, 389, 33, "广州富添盛汽车用品有限公司");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 20);
                LODOP.SET_PRINT_STYLEA(0, "FontColor", "#008000");
                LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                LODOP.ADD_PRINT_TEXT(40, 76, 624, 17, "地址：广州市白云区东平北路横岗东街自编1号                   Email：yingzhi_qin@gzfts.com  ");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(58, 76, 624, 17, "电话：（020）87647208                                      传真：（020）87647208");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(79, 32, 85, 20, "客户简称：");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(143, 33, 85, 20, "发货地址：");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(101, 33, 85, 20, "联系人：");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(79, 117, 371, 20, $('#AAcceptUnit').textbox('getValue'));//收货人
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(143, 118, 371, 20, $('#AAcceptAddress').textbox('getValue'));//客户地址
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(101, 118, 151, 20, $('#AAcceptPeople').combobox('getText'));//客户名称
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(122, 33, 85, 20, "联系电话：");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                var sphone = $('#AAcceptCellphone').textbox('getValue');
                if (sphone == "") {
                    sphone = $('#AAcceptTelephone').textbox('getValue');
                }
                LODOP.ADD_PRINT_TEXT(122, 118, 436, 20, sphone);
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(79, 481, 55, 20, "单号：");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(79, 534, 182, 20, $('#OrderNo').val());//发货单号
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(143, 587, 54, 20, "日期：");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(143, 640, 182, 20, getNowFormatDate($('#CreateDate').datetimebox('getValue')));//开单日期
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                LODOP.ADD_PRINT_TEXT(173, 38, 39, 20, "项目");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_RECT(165, 32, 34, 26, 0, 1);
                LODOP.ADD_PRINT_TEXT(173, 140, 40, 20, "货名");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_RECT(165, 66, 177, 26, 0, 1);
                LODOP.ADD_PRINT_TEXT(173, 293, 68, 20, "规格/型号");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_RECT(165, 549, 58, 26, 0, 1);
                LODOP.ADD_PRINT_TEXT(173, 407, 37, 20, "单位");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_RECT(165, 488, 61, 26, 0, 1);
                LODOP.ADD_PRINT_TEXT(173, 453, 35, 20, "数量");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_RECT(165, 243, 155, 26, 0, 1);
                LODOP.ADD_PRINT_TEXT(173, 493, 63, 20, "含税单价");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_RECT(165, 443, 45, 26, 0, 1);
                LODOP.ADD_PRINT_TEXT(173, 565, 37, 20, "小计");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_RECT(165, 398, 45, 26, 0, 1);
                LODOP.ADD_PRINT_TEXT(173, 653, 37, 20, "备注");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_RECT(165, 606, 121, 26, 0, 1);

                for (var i = 0; i < griddata.length; i++) {
                    LODOP.ADD_PRINT_RECT(191 + (i * 35), 32, 34, 35, 0, 1);
                    LODOP.ADD_PRINT_RECT(191 + (i * 35), 66, 177, 35, 0, 1);
                    LODOP.ADD_PRINT_RECT(191 + (i * 35), 243, 155, 35, 0, 1);
                    LODOP.ADD_PRINT_RECT(191 + (i * 35), 398, 45, 35, 0, 1);
                    LODOP.ADD_PRINT_RECT(191 + (i * 35), 443, 45, 35, 0, 1);
                    LODOP.ADD_PRINT_RECT(191 + (i * 35), 488, 61, 35, 0, 1);
                    LODOP.ADD_PRINT_RECT(191 + (i * 35), 549, 58, 35, 0, 1);
                    LODOP.ADD_PRINT_RECT(191 + (i * 35), 606, 121, 35, 0, 1);
                    LODOP.ADD_PRINT_TEXT(199 + (i * 35), 39, 34, 20, i + 1);
                    LODOP.ADD_PRINT_TEXT(194 + (i * 35), 69, 188, 20, griddata[i].ProductName);
                    LODOP.ADD_PRINT_TEXT(194 + (i * 35), 246, 162, 20, griddata[i].Specs);
                    LODOP.ADD_PRINT_TEXT(195 + (i * 35), 403, 51, 20, griddata[i].Package);
                    LODOP.ADD_PRINT_TEXT(199 + (i * 35), 455, 45, 20, Number(griddata[i].Piece));//数量
                    LODOP.ADD_PRINT_TEXT(199 + (i * 35), 492, 71, 20, Number(griddata[i].ActSalePrice));//对店销售单价
                    LODOP.ADD_PRINT_TEXT(199 + (i * 35), 553, 74, 20, (Number(griddata[i].Piece) * Number(griddata[i].ActSalePrice)).toFixed(2));
                    LODOP.ADD_PRINT_TEXT(193 + (i * 35), 612, 119, 20, griddata[i].RuleTitle);
                }
                LODOP.ADD_PRINT_RECT(226 + (griddata.length * 35) - 35, 443, 45, 25, 0, 1);
                LODOP.ADD_PRINT_TEXT(232 + (griddata.length * 35) - 35, 80, 215, 20, "合计TOTAL AMOUNT     ￥RMB");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_RECT(226 + (griddata.length * 35) - 35, 32, 411, 25, 0, 1);
                LODOP.ADD_PRINT_RECT(226 + (griddata.length * 35) - 35, 549, 58, 25, 0, 1);
                LODOP.ADD_PRINT_TEXT(232 + (griddata.length * 35) - 35, 553, 74, 20, AllPiece.toFixed(2));
                LODOP.ADD_PRINT_RECT(226 + (griddata.length * 35) - 35, 606, 121, 25, 0, 1);
                LODOP.ADD_PRINT_TEXT(230 + (griddata.length * 35) - 35, 447, 51, 20, Alltotal);


                LODOP.ADD_PRINT_TEXT(270 + (griddata.length * 35) - 35, 38, 39, 20, "备注");
                LODOP.ADD_PRINT_RECT(251 + (griddata.length * 35) - 35, 32, 39, 52, 0, 1);
                LODOP.ADD_PRINT_RECT(251 + (griddata.length * 35) - 35, 71, 656, 52, 0, 1);
                LODOP.ADD_PRINT_TEXT(253 + (griddata.length * 35) - 35, 80, 651, 45, $('#ARemark').val());
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(309 + (griddata.length * 35) - 35, 35, 70, 20, "制单人：");
                LODOP.ADD_PRINT_TEXT(309 + (griddata.length * 35) - 35, 89, 128, 20, $('#CreateAwb').textbox('getValue'));
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(309 + (griddata.length * 35) - 35, 283, 70, 20, "审核人：");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(309 + (griddata.length * 35) - 35, 509, 75, 20, "客户确认：");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(331 + (griddata.length * 35) - 35, 36, 70, 20, "请注意：");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                LODOP.ADD_PRINT_TEXT(352 + (griddata.length * 35) - 35, 39, 623, 57, "１、贵客户如对上列货品之品质、数量有任何疑问，请于收到货后十天内洽办，否则恕不受理退换货。\r\n２、客户应在收到上述货品后支付广州富添盛汽车用品有限公司货款。\r\n３、客户订货确认签名视为同意上述约定。");
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

            } else if (ThrowGoodId == 10) {
                //祺航送货单                
                for (var k = 0; k < griddata.length; k++) {
                    AllPiece += Number(griddata[k].Piece) * Number(griddata[k].ActSalePrice);
                    Alltotal += Number(griddata[k].Piece);
                    AllSalePrice += Number(griddata[k].ActSalePrice)
                }
                for (var i = 0; i < griddata.length; i++) {
                    if (i == (p - 1) * 5) {
                        if (p > 1) {
                            LODOP.NewPage();
                        }
                        p++;

                        LODOP.SET_PRINT_PAGESIZE(3, 2100, 30, "");
                        LODOP.ADD_PRINT_TEXT(5, 224, 328, 33, "广州祺航汽车科技有限公司");
                        LODOP.SET_PRINT_STYLEA(0, "FontName", "华文新魏");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 19);
                        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                        LODOP.ADD_PRINT_TEXT(85, 597, 54, 20, "单号：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(85, 642, 123, 20, $('#OrderNo').val());//发货单号
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(85, 441, 53, 20, "日期：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(85, 487, 111, 20, getNowFormatDate($('#CreateDate').datetimebox('getValue')));//开单日期
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(65, 4, 70, 20, "供应商：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(65, 70, 347, 20, "广州富添盛汽车用品有限公司");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(85, 4, 76, 20, "收货单位：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        var sphone = $('#AAcceptCellphone').textbox('getValue');
                        if (sphone == "") {
                            sphone = $('#AAcceptTelephone').textbox('getValue');
                        }
                        LODOP.ADD_PRINT_TEXT(85, 70, 377, 20, $('#AAcceptUnit').combobox('getText') + " " + $('#AAcceptPeople').textbox('getValue') + " " + sphone);//收货人
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

                        LODOP.ADD_PRINT_RECT(106, 0, 34, 36, 0, 1);
                        LODOP.ADD_PRINT_TEXT(121, 4, 39, 25, "序号");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_RECT(106, 34, 132, 36, 0, 1);
                        LODOP.ADD_PRINT_TEXT(120, 76, 62, 25, "产品编码");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(120, 239, 62, 25, "产品名称");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_RECT(106, 166, 186, 36, 0, 1);
                        LODOP.ADD_PRINT_TEXT(120, 408, 70, 25, "规格/型号");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_RECT(106, 352, 170, 36, 0, 1);
                        LODOP.ADD_PRINT_RECT(106, 522, 51, 36, 0, 1);
                        LODOP.ADD_PRINT_TEXT(120, 584, 40, 25, "数量");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_RECT(106, 573, 48, 36, 0, 1);
                        LODOP.ADD_PRINT_TEXT(120, 535, 37, 25, "单位");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_RECT(106, 621, 149, 36, 0, 1);
                        LODOP.ADD_PRINT_TEXT(120, 677, 70, 25, "备注");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);

                        LODOP.ADD_PRINT_LINE(180, 0, 364, 1, 0, 1);
                        LODOP.ADD_PRINT_LINE(180, 770, 364, 771, 0, 1);
                        LODOP.ADD_PRINT_RECT(364, 166, 536, 25, 0, 1);
                        LODOP.ADD_PRINT_RECT(364, 702, 68, 25, 0, 1);
                        LODOP.ADD_PRINT_TEXT(370, 58, 41, 20, "合计");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(468, 3, 254, 24, "配送服务商经手人（签字及盖章）：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(442, 3, 671, 25, "一式三联：第一联配送单位留存；第二、三联收货单位留存。");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(468, 433, 250, 24, "收货单位经手人（签字及盖章）：");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(38, 339, 90, 28, "送货单");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 18);
                        LODOP.ADD_PRINT_RECT(364, 0, 166, 25, 0, 1);
                        LODOP.ADD_PRINT_TEXT(372, 713, 65, 22, Number(AllPiece).toFixed(2));
                        LODOP.ADD_PRINT_RECT(388, 0, 166, 45, 0, 1);
                        LODOP.ADD_PRINT_TEXT(412, 67, 41, 20, "备注");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_RECT(388, 166, 604, 45, 0, 1);
                        LODOP.ADD_PRINT_TEXT(392, 167, 599, 40, $('#ARemark').val());
                    }

                    LODOP.ADD_PRINT_RECT(142 + (i - (p - 2) * 5) * 37, 0, 34, 37, 0, 1);
                    LODOP.ADD_PRINT_RECT(142 + (i - (p - 2) * 5) * 37, 34, 132, 37, 0, 1);
                    LODOP.ADD_PRINT_RECT(142 + (i - (p - 2) * 5) * 37, 166, 186, 37, 0, 1);
                    LODOP.ADD_PRINT_RECT(142 + (i - (p - 2) * 5) * 37, 352, 170, 37, 0, 1);
                    LODOP.ADD_PRINT_RECT(142 + (i - (p - 2) * 5) * 37, 522, 51, 37, 0, 1);
                    LODOP.ADD_PRINT_RECT(142 + (i - (p - 2) * 5) * 37, 573, 48, 37, 0, 1);
                    LODOP.ADD_PRINT_RECT(142 + (i - (p - 2) * 5) * 37, 621, 149, 37, 0, 1);
                    LODOP.ADD_PRINT_TEXT(156 + (i - (p - 2) * 5) * 37, 5, 34, 20, i + 1);
                    LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 5) * 37, 43, 122, 20, griddata[i].GoodsCode);
                    LODOP.ADD_PRINT_TEXT(147 + (i - (p - 2) * 5) * 37, 175, 179, 20, griddata[i].ProductName);
                    LODOP.ADD_PRINT_TEXT(147 + (i - (p - 2) * 5) * 37, 358, 166, 20, griddata[i].Specs);
                    LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 5) * 37, 528, 51, 20, griddata[i].Package);
                    LODOP.ADD_PRINT_TEXT(155 + (i - (p - 2) * 5) * 37, 580, 45, 20, Number(griddata[i].Piece));
                    LODOP.ADD_PRINT_TEXT(147 + (i - (p - 2) * 5) * 37, 627, 151, 20, griddata[i].RuleTitle);

                }
            }
            //LODOP.PRINT_DESIGN();
            LODOP.PREVIEW();
        }
        //数字转大写
        function digitUppercase(money) {
            var fraction = ['角', '分'];
            var digit = ['零', '壹', '贰', '叁', '肆', '伍', '陆', '柒', '捌', '玖'];
            var unit = [['元', '万', '亿'], ['', '拾', '佰', '仟']];
            var head = money < 0 ? '欠' : '';
            money = Math.abs(money);
            var s = '';
            for (var i = 0; i < fraction.length; i++) {
                s += (digit[Math.floor(money * 10 * Math.pow(10, i)) % 10] + fraction[i]).replace(/零./, '');
            }
            s = s || '整';
            money = Math.floor(money);
            for (var i = 0; i < unit[0].length && money > 0; i++) {
                var p = '';
                for (var j = 0; j < unit[1].length && money > 0; j++) {
                    p = digit[money % 10] + unit[1][j] + p;
                    money = Math.floor(money / 10);
                }
                s = p.replace(/(零.)*零$/, '').replace(/^$/, '零') + unit[0][i] + s;
            }
            var sum = head + s.replace(/(零.)*零元/, '元').replace(/(零.)+/g, '零').replace(/^整$/, '零元整');
            return sum;
        }
    </script>

</asp:Content>

