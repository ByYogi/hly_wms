<%@ Page Title="客户账单审批" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="financeClientAccountCheck.aspx.cs" Inherits="Cargo.Finance.financeClientAccountCheck" %>

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
        $(document).ready(function () {
            $('#dg').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'AccountID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                  { title: '', field: '', checkbox: true, width: '5%' },
                  {
                      field: 'opt', title: '操作', width: '70px', align: 'left',
                      formatter: function (value, row, index) {
                          if (value == undefined) {
                              if (row.Status == "1") {

                                  return '<a class="delcls" onclick="Deny(\'' + index + '\')" href="javascript:void(0)">未审</a>';
                              } else {

                                  return '<a class="editcls" onclick="check(\'' + index + '\')" href="javascript:void(0)">审核</a>';
                              }
                          }
                      }
                  },
                  {
                      title: '审核状态', field: 'Status', width: '5%',
                      formatter: function (value) {
                          if (value == "0") { return "<span title='待审'>待审</span>"; }
                          else if (value == "1") { return "<span title='已审'>已审</span>"; }
                          else if (value == "2") { return "<span title='拒审'>拒审</span>"; }
                          else if (value == "2") { return "<span title='结束'>结束</span>"; }
                      }
                  },
                  {
                      title: '结算状态', field: 'CheckStatus', width: '5%',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "未结算"; }
                          else if (val == "1") { return "已结清"; }
                          else if (val == "2") { return "未结清"; }
                          else { return "未结算"; }
                      }
                  },
                  {
                      title: '账单金额(元)', field: 'Total', align: 'right', width: '6%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                    {
                        title: '所属仓库', field: 'HouseName', width: '6%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                  {
                      title: '账单号', field: 'AccountID', width: '8%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '账单名称', field: 'AccountTitle', width: '12%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  { title: '创建日期', field: 'CreateDate', width: '6%', formatter: DateFormatter },
                {
                    title: '客户名称', field: 'ClientName', width: '5%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                },
                  //{
                  //    title: '税费', field: 'TaxFee', width: '7%', formatter: function (value) {
                  //        return "<span title='" + value + "'>" + value + "</span>";
                  //    }
                  //},
                  //{
                  //    title: '其它费用', field: 'OtherFee', width: '7%', formatter: function (value) {
                  //        return "<span title='" + value + "'>" + value + "</span>";
                  //    }
                  //},
                    {
                        title: '备注', field: 'Memo', width: '15%', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    //{
                    //    title: '下一审核人', field: 'NextCheckName', width: '5%', formatter: function (value) {
                    //        return "<span title='" + value + "'>" + value + "</span>";
                    //    }
                    //},

                  { title: '操作时间', field: 'OPDATE', width: '10%', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) {
                    $('.editcls').linkbutton({ text: '审核', plain: true, iconCls: 'icon-ok' });
                    $('.delcls').linkbutton({ text: '未审', plain: true, iconCls: 'icon-no' });
                },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });

            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#AClientName').combobox('clear');
                    $('#AClientName').combobox({
                        url: '../Client/clientApi.aspx?method=AutoCompleteClient&houseID=' + rec.HouseID, valueField: 'ClientID', textField: 'Boss', AddField: 'PinyinName',
                        filter: function (q, row) {
                            var opts = $(this).combobox('options');
                            return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                        },
                    });

                }
            });
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            $('#AClientName').combobox('textbox').bind('focus', function () { $('#AClientName').combobox('showPanel'); });
            $('#AStatus').combobox('textbox').bind('focus', function () { $('#AStatus').combobox('showPanel'); });
            $('#ACheckStatus').combobox('textbox').bind('focus', function () { $('#ACheckStatus').combobox('showPanel'); });
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../Client/clientApi.aspx?method=QueryBillManager';
            $('#dg').datagrid('load', {
                AccountID: $('#AAccountID').val(),
                HouseID: $("#AHouseID").combobox('getValue'),//仓库ID
                ClientID: $("#AClientName").val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                Status: $("#AStatus").combobox('getValue'),
                CheckStatus: $("#ACheckStatus").combobox('getValue')
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
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table>
            <tr>
                <td style="text-align: right;">账单号:
                </td>
                <td>
                    <input id="AAccountID" class="easyui-textbox" data-options="prompt:'请输入账单号'" style="width: 100px">
                </td>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 80px;" />
                </td>
                <td style="text-align: right;">客户名称:
                </td>
                <td>
                    <input id="AClientName" style="width: 100px;" data-options="valueField: 'ClientID', textField: 'Boss'" class="easyui-combobox" />
                </td>
                <td style="text-align: right;">审核状态:
                </td>
                <td style="width: 10%">
                    <select class="easyui-combobox" id="AStatus" style="width: 60px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">待审</option>
                        <option value="1">已审</option>
                        <option value="2">拒审</option>
                        <option value="3">结束</option>
                    </select>
                </td>
                <td style="text-align: right;">结算状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="ACheckStatus" style="width: 70px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">未结算</option>
                        <option value="1">已结清</option>
                        <option value="2">未结清</option>
                    </select>
                </td>
                <td style="text-align: right;">创建日期:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px">
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-lock_add"
            plain="false" onclick="plcheck()">&nbsp;批量审核</a>&nbsp;&nbsp; <a href="#" class="easyui-linkbutton"
                iconcls="icon-lock_open" plain="false" onclick="plDeny()">&nbsp;批量未审</a>&nbsp;&nbsp;
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 1000px; height: 600px; padding: 0px"
        closed="true" buttons="#dlg-buttons">
        <table id="dgAccount" class="easyui-datagrid">
        </table>
        <div id="saPanel">
            <form id="fm" class="easyui-form" method="post">
                <input type="hidden" name="AccountID" id="AccountID" />
                <input type="hidden" name="ClientID" id="AClientID" />
                <input type="hidden" name="ClientNum" id="AClientNum" />
                <input type="hidden" name="ClientName" id="ClientName" />
                <input type="hidden" name="HouseID" id="HouseID" />
                <table>
                    <tr>
                        <td style="text-align: right; width: 70px">应收金额：
                        </td>
                        <td>
                            <input id="Total" name="Total" class="easyui-textbox" readonly="readonly" style="width: 100px">
                            元
                        </td>
                        <td style="text-align: right; width: 70px">账单名称：
                        </td>
                        <td>
                            <input id="AccountTitle" name="AccountTitle" class="easyui-textbox" style="width: 300px"></td>
                    </tr>
                    <tr>
                        <td style="text-align: right; width: 70px">账单备注：
                        </td>
                        <td colspan="3">
                            <textarea name="Memo" id="Memo" cols="70" style="height: 50px;" class="mini-textarea"></textarea>
                        </td>
                    </tr>

                </table>
            </form>
        </div>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;&nbsp;关&nbsp;闭&nbsp;&nbsp;</a>
    </div>

    <script type="text/javascript">

        //审核
        function check(Did) {
            var rows = $("#dg").datagrid('getData').rows[Did];
            if (rows) {
                if (rows.Status == "1") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', rows.AccountID + '已审核', 'info'); return; }
                $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定审核？', function (r) {
                    if (r) {
                        var json = JSON.stringify([rows])
                        $.ajax({
                            url: '../Finance/financeApi.aspx?method=plCheckAccount',
                            type: 'post', dataType: 'json', data: { data: json },
                            success: function (text) {
                                if (text.Result == true) {
                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '审核成功!', 'info');
                                    reload();
                                } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning'); }
                            }
                        });
                    }
                });
            }
            else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要审核的数据！', 'warning'); }
        }
        //批量审核
        function plcheck() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要审核的数据！', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定审核？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: '../Finance/financeApi.aspx?method=plCheckAccount',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '审核成功!', 'info');
                                reload();
                            }
                            else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning'); }
                        }
                    });
                }
            });
        }
        //未审
        function Deny(Did) {
            var rows = $("#dg").datagrid('getData').rows[Did];
            if (rows) {
                $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定修改为未审？', function (r) {
                    if (r) {
                        var json = JSON.stringify([rows])
                        $.ajax({
                            url: '../Finance/financeApi.aspx?method=plUnCheckAccount',
                            type: 'post', dataType: 'json', data: { data: json },
                            success: function (text) {
                                if (text.Result == true) {
                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '未审成功!', 'info');
                                    reload();
                                } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning'); }
                            }
                        });
                    }
                });
            }
            else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要审核的数据！', 'warning'); }
        }
        //批量未审
        function plDeny() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要未审的数据！', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定修改为未审？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: '../Finance/financeApi.aspx?method=plUnCheckAccount',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '未审成功!', 'info');
                                reload();
                            }
                            else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning'); }
                        }
                    });
                }
            });
        }
        function reload() {
            $('#dg').datagrid('reload');
            $('#dg').datagrid('clearSelections');
        }
        //修改账单信息
        function editItemByID(Did) {
            $('#dgAccount').datagrid('loadData', { total: 0, rows: [] });
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '查看账单号：' + row.AccountID);
                $('#fm').form('load', row);
                showGrid();
                var gridOpts = $('#dgAccount').datagrid('options');
                gridOpts.url = '../Client/clientApi.aspx?method=QueryOrderByAccountNo&AccountNo=' + row.AccountID;
            }
        }
        //显示列表
        function showGrid() {
            $('#dgAccount').datagrid({
                width: '100%',
                height: '430px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OrderID',
                url: null,
                columns: [[
                  {
                      title: '收入', field: 'TotalCharge', width: '70px', align: 'right',
                      formatter: function (val, row, index) {
                          if (row.OrderModel == "1") {
                              return "-" + val;
                          } else { return val; }
                      }
                  },
                  {
                      title: '结算状态', field: 'CheckStatus', width: '60px', formatter: function (val, row, index) { if (val == "0") { return "<span title='未结算'>未结算</span>"; } else if (val == "1") { return "<span title='已结清'>已结清</span>"; } else if (val == "2") { return "<span title='未结清'>未结清</span>"; } else { return ""; } }
                  },
                  {
                      title: '订单号', field: 'OrderNo', width: '90px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '订单类型', field: 'OrderModel', width: '60px', formatter: function (val, row, index) {
                          if (val == "0") { return "<span title='订货单'>订货单</span>"; } else if (val == "1") { return "<span title='退货单'>退货单</span>"; } else { return ""; }
                      }
                  },
                  {
                      title: '件数', field: 'Piece', width: '50px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  { title: '开单时间', field: 'CreateDate', width: '130px', formatter: DateTimeFormatter },
                  {
                      title: '客户名称', field: 'PayClientName', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '公司名称', field: 'AcceptUnit', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '收货人', field: 'AcceptPeople', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '手机号码', field: 'AcceptCellphone', width: '90px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '收货地址', field: 'AcceptAddress', width: '120px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '到达站', field: 'Dest', width: '50px', formatter: function (value) {
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
                  {
                      title: '开单员', field: 'CreateAwb', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '订单状态', field: 'AwbStatus', width: '60px',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "<span title='已下单'>已下单</span>"; }
                          else if (val == "1") { return "<span title='正在备货'>正在备货</span>"; }
                          else if (val == "2") { return "<span title='已出库'>已出库</span>"; }
                          else if (val == "3") { return "<span title='运输在途'>运输在途</span>"; }
                          else if (val == "4") { return "<span title='已到达'>已到达</span>"; }
                          else if (val == "5") { return "<span title='已签收'>已签收</span>"; }
                          else { return ""; }
                      }
                  },
                  {
                      title: '订单类型', field: 'OrderType', width: '60px', formatter: function (val, row, index) {
                          if (val == "0") { return "<span title='电脑单'>电脑单</span>"; } else if (val == "1") { return "<span title='企业微信单'>企业微信单</span>"; } else if (val == "2") { return "<span title='微信商城单'>微信商城单</span>"; } else if (val == "3") { return "<span title='APP单'>APP单</span>"; } else if (val == "4") { return "<span title='小程序单'>小程序单</span>"; } else { return "<span title='电脑单'>电脑单</span>"; }
                      }
                  },
                  //{
                  //    title: '商城订单号', field: 'WXOrderNo', width: '110px', formatter: function (value) {
                  //        if (value != null && value != "") {
                  //            return "<span title='" + value + "'>" + value + "</span>";
                  //        }
                  //    }
                  //},
                  //{
                  //    title: '付款方式', field: 'PayWay', width: '60px', formatter: function (val, row, index) {
                  //        if (val == "0") { return "<span title='微信付款'>微信付款</span>"; } else if (val == "1") { return "<span title='额度付款'>额度付款</span>"; } else if (val == "2") { return "<span title='积分兑换'>积分兑换</span>"; } else { return ""; }
                  //    }
                  //},
                  //{
                  //    title: '支付订单号', field: 'WXPayOrderNo', width: '190px', formatter: function (value) {
                  //        if (value != null && value != "") {
                  //            return "<span title='" + value + "'>" + value + "</span>";
                  //        }
                  //    }
                  //},
                  { title: '已收款', field: 'ReceivedMoney', hidden: true }
                ]],
                onDblClickRow: function (index, row) { }
            })
        }

    </script>

</asp:Content>
