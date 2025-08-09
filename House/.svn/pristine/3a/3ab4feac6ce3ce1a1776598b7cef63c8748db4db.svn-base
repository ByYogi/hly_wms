<%@ Page Title="回单录入操作" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReturnAwbManager.aspx.cs" Inherits="Cargo.Arrive.ReturnAwbManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: false, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ContractNum',
                url: null,
                columns: [[
                  {
                      title: '合同号', field: 'ContractNum', width: '10%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '车牌号', field: 'TruckNum', width: '10%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  { title: '发车时间', field: 'StartTime', width: '10%', formatter: DateTimeFormatter },
                  { title: '预计(实际)到达时间', field: 'PreArriveTime', width: '130px', formatter: DateTimeFormatter },
                  {
                      title: '出发站', field: 'Dep', width: '5%', formatter: function (value) {
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
                      title: '司机', field: 'Driver', width: '10%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '手机号码', field: 'DriverCellPhone', width: '10%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '付款方式', field: 'PayMode', width: '5%',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "<span title='现金'>现金</span>"; } else if (val == "1") { return "<span title='提付'>提付</span>"; } else if (val == "2") { return "<span title='回单'>回单</span>"; } else if (val == "3") { return "<span title='月结'>月结</span>"; } else if (val == "4") { return "<span title='代收款'>代收款</span>"; } else { return ""; }
                      }
                  },
                  {
                      title: '回单状态', field: 'ReturnStatus', width: '10%',
                      formatter: function (val, row, index) { if (val == "N") { return "<span title='回单未录'>回单未录</span>"; } else if (val == "Y") { return "<span title='回单全录'>回单全录</span>"; } else { return ""; } }
                  },
                  {
                      field: 'opt', title: '操作', width: '10%', align: 'left',
                      formatter: function (value, rec, index) {
                          var btn = '<a class="setcls" onclick="editRow(\'' + index + '\')" href="javascript:void(0)">录入回单</a>'; return btn;
                      }
                  }
                ]],
                onLoadSuccess: function (data) {
                    $('.setcls').linkbutton({ text: '录入回单', plain: false, iconCls: 'icon-attach' });
                },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                rowStyler: function (index, row) {
                    if (row.ReturnStatus == "Y") { return "color:#FF0000"; }
                }
            });
            $('#Dest').combobox('textbox').bind('focus', function () { $('#Dest').combobox('showPanel'); });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#StartDate').datebox('textbox').bind('focus', function () { $('#StartDate').datebox('showPanel'); });
            $('#EndDate').datebox('textbox').bind('focus', function () { $('#EndDate').datebox('showPanel'); });
            $('#Dest').combobox('setValues', '<%=UserInfor.DepCity%>');
            $('#Dest').combobox('disable');
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../Arrive/ArriveApi.aspx?method=QueryArriveManifestFromReturnAwb';
            $('#dg').datagrid('load', {
                TruckNum: $('#TruckNum').val(),
                ContractNum: $('#ContractNum').val(),
                AwbNo: $('#AwbNo').val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                Dest: $("#Dest").combobox('getValue')
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

    <div style="background: linear-gradient(to bottom, #eff5ff 0px, #e0ecff 100%) repeat-x scroll 0 0; border-color: #95b8e7; border-style: solid; border-width: 1px 1px 0px 1px;">
        <table>
            <tr>
                <td>
                    <a class="easyui-linkbutton" iconcls="icon-lorry" plain="false" style="color: Red;" href="../Arrive/ReturnAwbManager.aspx" target="_self">&nbsp;按车辆录入回单&nbsp;</a>&nbsp;&nbsp;
                    <a class="easyui-linkbutton" iconcls="icon-application_form" plain="false" href="../Arrive/ReturnByAwb.aspx" target="_self"> &nbsp;按运单录入回单&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    <div id="saPanel" name='SelectDiv1' class="easyui-panel" title="" data-options="iconCls:'icon-search'">
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
                <td style="text-align: right;">运单号码:
                </td>
                <td>
                    <input id="AwbNo" class="easyui-textbox" data-options="prompt:'请输入运单号'" style="width: 100px">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>
                </td>
                <td style="text-align: right;">时间范围:
                </td>
                <td colspan="3">
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px">
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="dgShowData" class="easyui-dialog" style="width: 80%; height: 450px; padding: 3px 3px"
        closed="true" buttons="#dgShowData-buttons">
        <input id="AContractNum" type="hidden" />
        <table id="dgTransit" class="easyui-datagrid">
        </table>
    </div>
    <div id="dgShowData-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" id="save" plain="false" onclick="SaveData()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="closedgShowData()">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>

    <script type="text/javascript">
        //录入回单信息
        function SaveData() {
            editIndex == undefined;
            var rows = $('#dgTransit').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要保存的数据！', 'warning'); return; }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定保存？', function (r) {
                if (r) {
                    var cr = $('#dgTransit').datagrid('getChanges');
                    $('#dgTransit').datagrid('acceptChanges');
                    var rowss = $('#dgTransit').datagrid('getSelections');
                    var json = JSON.stringify(rowss) + "|" + JSON.stringify(cr);
                    $.ajax({
                        url: "../Arrive/ArriveApi.aspx?method=AddReturnAwbInfo&ContractNum=" + $('#AContractNum').val(),
                        type: 'post', dataType: 'json', data: { data: json }, async: false,
                        success: function (text) {
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '录入回单成功!', 'info');
                                closedgShowData();
                            }
                            else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning'); }
                        }
                    });
                }
            });
        }
        //录入回单
        function editRow(Did) {
            editIndex = undefined;
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                editIndex = undefined;
                $('#dgShowData').dialog('open').dialog('setTitle', '司机合同  ' + row.ContractNum + ' 录入回单');
                showDg();
                $('#AAwbNo').val($('#AwbNo').val());
                $('#AContractNum').val(row.ContractNum);
                var dgTransit = $('#dgTransit').datagrid('options');
                dgTransit.url = '../Arrive/ArriveApi.aspx?method=QueryAwbByContractNum&ContractNum=' + row.ContractNum;
            }
        }
        //显示DG
        function showDg() {
            $('#dgTransit').datagrid({
                width: '100%',
                height: '360px',
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
                  { title: '', field: 'AwbID', checkbox: true, width: '30px' },
                  { title: '实际签收时间', field: 'SignTime', width: '140px', formatter: DateTimeFormatter, editor: 'datetimebox' },
                  {
                      title: '回单信息', field: 'ReturnInfo', width: '100px', editor: 'text', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '延误天数', field: 'DelayDay', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '延误原因', field: 'DelayReason', width: '100px', editor: {
                          type: 'combobox',
                          options: { panelHeight: 'auto', valueField: 'id', textField: 'value', data: [{ id: '发运延迟', value: '发运延迟' }, { id: '配送延迟', value: '配送延迟' }, { id: '客户原因', value: '客户原因' }, { id: '其它原因', value: '其它原因' }], required: false, editable: false }
                      }
                  },
                  { title: '实收金额', field: 'ActMoney', width: '60px', editor: { type: 'numberbox', options: { precision: 2 } } },
                  {
                      title: '签收人', field: 'Signer', width: '60px', editor: 'text', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '回单状态', field: 'ReturnStatus', width: '70px',
                      formatter: function (val, row, index) { if (val == "N") { return "<span title='回单未录'>回单未录</span>"; } else if (val == "Y") { return "<span title='回单已录'>回单已录</span>"; } else { return ""; } }
                  },
                  {
                      title: '运单号', field: 'AwbNo', width: '70px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  { title: '开单日期', field: 'HandleTime', width: '80px', formatter: DateFormatter },
                  {
                      title: '客户名称', field: 'ShipperUnit', width: '100px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '运输费用', field: 'TransportFee', width: '70px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '总费用', field: 'TotalCharge', width: '70px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '结款方式', field: 'CheckOutType', width: '60px',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "<span title='现付'>现付</span>"; } else if (val == "1") { return "<span title='回单'>回单</span>"; } else if (val == "2") { return "<span title='月结'>月结</span>"; } else if (val == "3") { return "<span title='到付'>到付</span>"; } else if (val == "4") { return "<span title='代收款'>代收款</span>"; } else { return ""; }
                      }
                  },
                 {
                     title: '出发站', field: 'Dep', width: '50px', formatter: function (value) {
                         if (value != null && value != "") {
                             return "<span title='" + value + "'>" + value + "</span>";
                         }
                     }
                 },
                 {
                     title: '中间站', field: 'MidDest', width: '50px', formatter: function (value) {
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
                     title: '中转站', field: 'Transit', width: '50px', formatter: function (value) {
                         if (value != null && value != "") {
                             return "<span title='" + value + "'>" + value + "</span>";
                         }
                     }
                 },
                 {
                     title: '品名', field: 'Goods', width: '70px', formatter: function (value) {
                         if (value != null && value != "") {
                             return "<span title='" + value + "'>" + value + "</span>";
                         }
                     }
                 },
                 {
                     title: '件数', field: 'Piece', width: '50px', formatter: function (value, row) {
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
                     title: '重量', field: 'AwbWeight', width: '60px', formatter: function (value, row) {
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
                     title: '体积', field: 'AwbVolume', width: '60px', formatter: function (value, row) {
                         if (value != null && value != "") {
                             if (row.AwbVolume != value) {
                                 return "<span title='" + row.AwbVolume + "/" + value + "'>" + row.AwbVolume + "/" + value + "</span>";
                             } else {
                                 return "<span title='" + value + "'>" + value + "</span>";
                             }
                         }
                     }
                 },
                ]],
                onClickCell: onClickCell,
                onLoadSuccess: function (data) {
                    $('#dgTransit').datagrid('clearSelections');
                    var awbno = $('#AwbNo').textbox('getValue');
                    for (var i = 0; i < data.rows.length; i++) {
                        if (awbno == data.rows[i].AwbNo) {
                            $('#dgTransit').datagrid('selectRow', i);
                        }
                    }
                    var trs = $(this).prev().find('div.datagrid-body').find('tr');
                    for (var i = 0; i < trs.length; i++) {
                        for (var j = 1; j < trs[i].cells.length; j++) {
                            var row_html = trs[i].cells[j];
                            var cell_field = $(row_html).attr('field');
                            if (cell_field != 'SignTime' && cell_field != 'ReturnInfo' && cell_field != 'DelayReason' && cell_field != 'ActMoney' && cell_field != 'Signer') {
                                trs[i].cells[j].style.cssText = 'background:#e2e2e2;';
                            }
                        }
                    }
                },
                rowStyler: function (index, row) {
                    if (row.ReturnStatus == "Y") { return "background:#80C8FE;"; }
                    if (row.ReturnStatus == "Y" && row.TrafficType == "2") { return "font-weight:bold;background:#80C8FE;"; }
                    if (row.FP == "1") { return "color:#FF0000;"; }
                    if (row.FP == "1" && row.TrafficType == "2") { return "color:#FF0000;font-weight:bold;"; }
                    if (row.FP == "1" && row.ReturnStatus == "Y") { return "color:#FF0000;background:#80C8FE;"; }
                    if (row.FP == "1" && row.ReturnStatus == "Y" && row.TrafficType == "2") { return "color:#FF0000;background:#80C8FE;font-weight:bold;"; }
                }
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
        var editIndex = undefined;
        function endEditing() {
            if (editIndex == undefined) { return true }
            if ($('#dgTransit').datagrid('validateRow', editIndex)) {
                var rows = $("#dgTransit").datagrid('getData').rows[editIndex];
                var ed = $('#dgTransit').datagrid('getEditors', editIndex);
                var cg = ed[0];
                if (cg.field == "SignTime") {
                    if (CurentTimeHHMM(getDate(cg.target.datetimebox('getValue'))) == "00:00:00") {
                        cg.target.datetimebox('setValue', AllDateTime(new Date()));
                    }
                }
                $('#dgTransit').datagrid('endEdit', editIndex);
                editIndex = undefined;
                return true;
            } else {
                return false;
            }
        }
        function onClickCell(index, field) {
            var row = $("#dgTransit").datagrid('getData').rows[index];
            if (row.ReturnStatus == "N") {
                if (field == "SignTime" || field == "ReturnInfo" || field == "ActMoney" || field == "Signer" || field == "DelayReason") {
                    if (endEditing()) {
                        $('#dgTransit').datagrid('selectRow', index).datagrid('editCell', { index: index, field: field });
                        editIndex = index;
                    }
                }
                else {
                    if (editIndex == undefined) { return true }
                    $('#dgTransit').datagrid('endEdit', editIndex);
                    editIndex = undefined;
                }
            } else {
                if (editIndex == undefined) { return true }
                $('#dgTransit').datagrid('endEdit', editIndex);
                editIndex = undefined;
            }
        }
        //关闭
        function closedgShowData() {
            $('#dgShowData').dialog('close');
            $("#dg").datagrid('reload');
        }
    </script>

</asp:Content>
