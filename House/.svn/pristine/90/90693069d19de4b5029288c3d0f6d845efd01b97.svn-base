<%@ Page Title="新增退货" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="addReturnOrder.aspx.cs" Inherits="Dealer.Antyres.addReturnOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/easy/js/datagrid-groupview.js" type="text/javascript"></script>
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
            var height = parseInt((Number($(window).height()) - Number($("div[name='SelectDiv1']").outerHeight(true))) / 2);
            $('#dg').datagrid({ height: height-15 });
            $('#dgReturn').datagrid({ height: (Number($(window).height()) - 90) - height - 75 });
        }
        $(document).ready(function () {
            $('#dg').datagrid({
                width: '100%',
                //height: '300px',
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                rownumbers: true,
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OrderID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                  {
                      title: '订单号', field: 'OrderNo', width: '90px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '出库数量', field: 'Piece', width: '60px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '货品代码', field: 'GoodsCode', width: '60px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '花纹', field: 'Figure', width: '100px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                    },
                    {
                        title: '载速', field: 'LoadIndex', width: '60px', align: 'right', formatter: function (value, row) {
                            return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                        }
                    },
                  {
                      title: '周期年', field: 'BatchYear', width: '50px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '产品品牌', field: 'TypeName', width: '60px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  }, 
                  {
                      title: '收货人', field: 'AcceptPeople', width: '70px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '联系手机', field: 'AcceptCellphone', width: '90px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  { title: '开单时间', field: 'CreateDate', width: '130px', formatter: DateTimeFormatter }
                ]],
                groupField: 'OrderNo',
                view: groupview,
                groupFormatter: function (value, rows) {
                    return value + ' - 总件数：' + rows[0].TotalPiece ;
                },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { dblClickOutCargo(index); }
            });
            //退货数据
            $('#dgReturn').datagrid({
                width: '100%',
                //height: '200px',
                title: '退货产品', //标题内容
                loadMsg: '数据加载中请稍候...',
                rownumbers: true,
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OrderID',
                url: null,
                toolbar: '',
                columns: [[
                  {
                      title: '订单号', field: 'OrderNo', width: '90px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '退货条数', field: 'Piece', width: '60px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '货品代码', field: 'GoodsCode', width: '60px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '花纹', field: 'Figure', width: '100px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                    },
                    {
                        title: '载速', field: 'LoadIndex', width: '60px', align: 'right', formatter: function (value, row) {
                            return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                        }
                    },
                  {
                      title: '周期年', field: 'BatchYear', width: '50px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '产品品牌', field: 'TypeName', width: '60px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '收货人', field: 'AcceptPeople', width: '70px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '联系手机', field: 'AcceptCellphone', width: '90px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  { title: '开单时间', field: 'CreateDate', width: '130px', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { dblClickDelCargo(index); }
            });
            $('#DisplayNum').val(0);
            $('#DisplayPiece').val(0);
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
        });

        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'FormService.aspx?method=QueryOrderInfoForReturn';
            $('#dg').datagrid('load', {
                OrderNo: $('#AOrderNo').val(),
                AcceptPeople: $("#AcceptPeople").val(),
                Piece: $("#Piece").val(),
                Specs: $("#Specs").val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                CheckOutType: '',
                OrderModel: "0"//订单类型
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
                <td style="text-align: right;">订单号:
                </td>
                <td>
                    <input id="AOrderNo" class="easyui-textbox" data-options="prompt:'请输入订单号'" style="width: 100px"/>
                </td>

                <td style="text-align: right;">收货:
                </td>
                <td>
                    <input id="AcceptPeople" class="easyui-textbox" data-options="prompt:'收货单位/人'" style="width: 100px;" />
                </td>
                <td style="text-align: right;">规格:
                </td>
                <td>
                    <input id="Specs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 100px"/>
                </td>
                <td style="text-align: right;">数量:
                </td>
                <td>
                    <input id="Piece" class="easyui-textbox" data-options="prompt:'请输入数量'" style="width: 100px"/>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">开单时间:
                </td>
                <td colspan="3">
                    <input id="StartDate" class="easyui-datebox" style="width: 100px"/>~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px"/>
                </td>
            </tr>
        </table>
    </div>
    <input type="hidden" id="DisplayNum" />
    <input type="hidden" id="DisplayPiece" />
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>
    </div>
    <table id="dgReturn" class="easyui-datagrid">
    </table>
    <!--Begin 退货操作-->

    <div id="dlg" class="easyui-dialog" style="width: 350px; height: 200px; padding: 5px 5px" closed="true" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" id="InPiece" />
            <input type="hidden" id="InIndex" />
            <table>
                <tr>
                    <td style="text-align: right;">退货订单数量:
                    </td>
                    <td>
                        <input name="Numbers" id="Numbers" class="easyui-numberbox" data-options="min:0,precision:0" style="width: 200px;">
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="outOK()">&nbsp;确&nbsp;定&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <!--End 退货操作-->
    <div id="saPanel" class="easyui-panel" title="" style="width: 100%; height: 120px;" data-options="footer:'#ft'">
        <form id="fmDep" class="easyui-form" method="post">
            <input type="hidden" name="SaleManID" id="SaleManID" />
            <input type="hidden" name="HouseCode" id="HouseCode" />
            <input type="hidden" name="HouseID" id="HouseID" />
            <input type="hidden" name="ONum" id="ONum" />
            <input type="hidden" name="OutNum" id="OutNum" />
            <input type="hidden" name="OrderNo" id="OrderNo" />
            <input type="hidden" name="ReturnHouse" id="ReturnHouse" />
            <table style="width: 100%">
                <tr>
                    <td style="text-align: right;">退货数量:
                    </td>
                    <td>
                        <input name="Piece" id="APiece" class="easyui-numberbox" data-options="min:0,precision:0" style="width: 60px;" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;" rowspan="2">备注:
                    </td>
                    <td colspan="9" rowspan="2">
                        <textarea name="Remark" id="ARemark" data-options="required:true" rows="3" style="width: 500px;"></textarea>
                    </td>

                </tr>
            </table>
        </form>
    </div>
    <div id="ft" style="padding: 1px; text-align: right;">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveReturn()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="reset()">&nbsp;重&nbsp;置&nbsp;</a>
    </div>
    <script type="text/javascript">
        //保存退货单
        function saveReturn() {
            var rows = $('#dgReturn').datagrid('getRows');
            if (rows == null || rows == "") { $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '请选择要保存的数据！', 'warning'); return; }
            $.messager.confirm('<%= Dealer.Common.GetSystemNameAndVersion()%>', '确定保存？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    var json = JSON.stringify(rows);
                    $('#fmDep').form('submit', {
                        url: 'FormService.aspx?method=saveReturn',
                        contentType: "application/json;charset=utf-8", dataType: "json",
                        onSubmit: function (param) {
                            param.submitData = json;
                            var trd = $(this).form('enableValidation').form('validate');
                            return trd;
                        },
                        success: function (msg) {
                            $.messager.progress("close");
                            var result = eval('(' + msg + ')');
                            if (result.Result) {
                                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '保存成功！', 'info');
                                location.reload();
                            } else {
                                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                            }
                        }
                    })
                }
            });
        }
        //重置
        function reset() {
            $('#fmDep').form('clear');
            $('#dg').datagrid('loadData', { total: 0, rows: [] });
            $('#dgReturn').datagrid('loadData', { total: 0, rows: [] });

            $('#DisplayNum').val(0);
            $('#DisplayPiece').val(0);
            var title = "";
            $('#dgReturn').datagrid("getPanel").panel("setTitle", title);

        }
        ///退货
        function dblClickOutCargo(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row.Piece == 0) {
                $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '退货件数为0', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '订单：' + row.OrderNo + ' 规格：' + row.Specs + '退货');
                $('#InPiece').val(row.Piece);
                $('#InIndex').val($('#dg').datagrid('getRowIndex', row));
                $('#Numbers').numberbox('setValue', '');
                $('#Numbers').numberbox({
                    min: 0,
                    max: row.Piece,
                    precision: 0
                });
            }
        }

        //新增出库数据
        function outOK() {
            var row = $('#dg').datagrid('getSelected');
            $('#SaleManName').textbox('setValue', row.SaleManName);
            $('#SaleManID').val(row.SaleManID);
            $('#OrderNo').val(row.OrderNo);
            $('#HouseID').val(row.HouseID);
            $('#HouseCode').val(row.HouseCode);
            var index = $('#dgReturn').datagrid('getRowIndex', row.ID);
            if (index < 0) {
                row.Piece = $('#Numbers').numberbox('getValue');
                $('#dgReturn').datagrid('appendRow', row);
                var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
                var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
                n++;
                var pt = p + Number($('#Numbers').numberbox('getValue'));
                $('#DisplayNum').val(Number(n));
                $('#DisplayPiece').val(Number(pt));
                $('#APiece').numberbox('setValue', Number(pt));
                var title = "退货产品  已退：" + n + "票，总件数：" + pt + " 件";
                $('#dgReturn').datagrid("getPanel").panel("setTitle", title);
                closedgShowData();
            } else { $.messager.alert('<%= Dealer.Common.GetSystemNameAndVersion()%>', '退货列表已存在该产品，请先删除再添加！', 'warning'); }
        }
        //关闭
        function closedgShowData() {
            $('#dlg').dialog('close');
        }
        //删除退货的数据
        function dblClickDelCargo(Did) {
            var row = $("#dgReturn").datagrid('getData').rows[Did];
            var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
            var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
            n--;
            var pt = p - Number(row.Piece);
            $('#DisplayNum').val(Number(n));
            $('#DisplayPiece').val(Number(pt));
            $('#APiece').numberbox('setValue', Number(pt));
            var title = "退货产品  已退：" + n + "票，总件数：" + pt + " 件";
            $('#dgReturn').datagrid("getPanel").panel("setTitle", title);

            var index = $('#dgReturn').datagrid('getRowIndex', row);
            $('#dgReturn').datagrid('deleteRow', index);
        }

    </script>
</asp:Content>
