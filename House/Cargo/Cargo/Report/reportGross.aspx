<%@ Page Title="毛利报表统计" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reportGross.aspx.cs" Inherits="Cargo.Report.reportGross" %>

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
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58 - 28) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58 - 28) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58 -28) / 28) * 2],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OrderNo',
                url: null,
                rownumbers: false,
                toolbar: '#dgtoolbar',
                showFooter: true,
                columns: [[
                  { title: '开单日期', field: 'CreateDate', width: '75px', formatter: DateFormatter },
                  {
                      title: '订单号', field: 'OrderNo', width: '90px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '订单类型', field: 'ThrowGood', width: '60px', formatter: function (val, row, index) {
                          if (val == "0") { return "<span title='客户单'>客户单</span>"; } else if (val == "1") { return "<span title='抛货单'>抛货单</span>"; } else if (val == "2") { return "<span title='调货单'>调货单</span>"; } else if (val == "3") { return "<span title='代发单'>代发单</span>"; } else { return ""; }
                      }
                  },
                  {
                      title: '产品名称', field: 'ProductName', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '产品类型', field: 'TypeName', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '销售价', field: 'ActSalePrice', width: '60px', align: 'right', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '成本价', field: 'CostPrice', width: '60px', align: 'right', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '毛利', field: 'Gross', width: '60px', align: 'right', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '数量', field: 'Piece', width: '40px', align: 'right', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '总毛利', field: 'TotalGross', width: '60px', align: 'right', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '花纹', field: 'Figure', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '型号', field: 'Model', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '批次', field: 'Batch', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  //{ title: '批次', field: 'BatchYear', width: '60px', formatter: function (val, row, index) { return val + "年" } },
                  {
                      title: '目的站', field: 'Dest', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '货品代码', field: 'GoodsCode', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '客户编码', field: 'ClientNum', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '客户名称', field: 'ClientName', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '客户单位', field: 'AcceptUnit', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '收货人', field: 'AcceptPeople', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '业务员', field: 'SaleManName', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  //{ title: '手机号码', field: 'AcceptCellphone', width: '80px' },
                  {
                      title: '所在货位', field: 'ContainerCode', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '所在区域', field: 'AreaName', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '所在仓库', field: 'FirstAreaName', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '下单方式', field: 'OrderType', width: '60px', formatter: function (val, row, index) {
                          if (val == "0") { return "<span title='电脑下单'>电脑下单</span>"; } else if (val == "1") { return "<span title='企业号下单'>企业号下单</span>"; } else if (val == "2") { return "<span title='商城订单'>商城订单</span>"; } else if (val == "4") { return "<span title='小程序下单'>小程序下单</span>"; } else { return "<span title='电脑下单'>电脑下单</span>"; }
                      }
                  },
                  {
                      title: '调货(代发)仓库', field: 'TranHouse', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  }
                ]],
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) {
                    editItemByID(index);
                }
            });

            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#SaleManID').combobox('clear');
                    var url = '../Order/orderApi.aspx?method=QueryUserByDepCode&houseID=' + rec.HouseID;
                    $('#SaleManID').combobox('reload', url);
                    $('#PID').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    $('#PID').combobox('reload', url);
                }
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
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            $('#SaleManID').combobox('textbox').bind('focus', function () { $('#SaleManID').combobox('showPanel'); });
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            $('#SaleManID').combobox('clear');
            var url = '../Order/orderApi.aspx?method=QueryUserByDepCode&houseID=<%=UserInfor.HouseID%>';
            $('#SaleManID').combobox('reload', url);
            $('#PID').combobox('clear');
            var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=<%=UserInfor.HouseID%>';
            $('#PID').combobox('reload', url);

        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'reportApi.aspx?method=QueryGrossReport';
            $('#dg').datagrid('load', {
                Specs: $('#ASpecs').val(),
                AcceptPeople: $('#AAcceptPeople').val(),
                PID: $("#APID").combobox('getValue'),//一级产品
                SID: $("#ASID").combobox('getValue'),//二级产品
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                HouseID: $("#AHouseID").combobox('getValues').toString(),//仓库ID
                OrderType: $("#AOrderType").combobox('getValue'),
                ThrowGood: $("#AThrowGood").combobox('getValue'),
                PID: $("#PID").combobox('getValue'),
                SaleManID: $('#SaleManID').combobox('getValue')
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
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width:100%">
        <table>
            <tr>
                <td style="text-align: right;">查询时间:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                        <input id="EndDate" class="easyui-datebox" style="width: 100px">
                </td>
                <td style="text-align: right;">订单类型:
                </td>
                <td>
                    <select class="easyui-combobox" id="AThrowGood" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">客户单</option>
                        <option value="1">抛货单</option>
                        <option value="2">调货单</option>
                        <option value="3">代发单</option>
                    </select>
                </td>
                <td style="text-align: right;">区域大仓:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;" panelheight="auto" data-options="multiple:true" />
                </td>
                <td style="text-align: right;">规格:
                </td>
                <td>
                    <input id="ASpecs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 100px">
                </td>

                <td style="text-align: right;">一级产品:
                </td>
                <td>
                    <input id="APID" class="easyui-combobox" style="width: 80px;"
                        panelheight="auto" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">客户名称:
                </td>
                <td>
                    <input id="AAcceptPeople" class="easyui-textbox" data-options="prompt:'请输入客户名称'" style="width: 100px;" />
                </td>
                <td style="text-align: right;">下单方式:
                </td>
                <td>
                    <select class="easyui-combobox" id="AOrderType" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">电脑下单</option>
                        <option value="1">企业号下单</option>
                        <option value="2">商城下单</option>
                        <option value="4">小程序下单</option>
                    </select>
                </td>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="PID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'AreaID',textField:'Name'" panelheight="auto" />
                </td>
                <td style="text-align: right;">业务员:
                </td>
                <td>
                    <input name="SaleManID" id="SaleManID" class="easyui-combobox" style="width: 100px;"
                        data-options="valueField: 'LoginName',textField: 'UserName'" />
                </td>
                <td style="text-align: right;">二级产品:
                </td>
                <td>
                    <input id="ASID" class="easyui-combobox" style="width: 80px;" data-options="valueField:'TypeID',textField:'TypeName'"
                        panelheight="auto" />
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="dgtoolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-application_put"
            plain="false" onclick="AwbExport()">&nbsp;导&nbsp;出&nbsp;</a>
        <form runat="server" id="fm1">
            <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
        </form>
    </div>

    <script type="text/javascript">

        //导出数据
        function AwbExport() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            var key = new Array();
            key[0] = $('#StartDate').datebox('getValue');
            key[1] = $('#EndDate').datebox('getValue');
            key[2] = $('#SaleManID').combobox('getValue');
            key[3] = $("#AHouseID").combobox('getValue');//仓库ID
            key[4] = $("#PID").combobox('getValue');//销售人员ID
            key[5] = $('#ASpecs').val();
            key[6] = $('#AAcceptPeople').val();
            key[7] = $("#ASID").combobox('getValue');
            key[8] = $("#AOrderType").combobox('getValue');
            key[9] = $("#AThrowGood").combobox('getValue');
            $.ajax({
                url: "reportApi.aspx?method=QueryGrossReportForExport&key=" + escape(key.toString()),
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click(); }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }

    </script>

</asp:Content>
