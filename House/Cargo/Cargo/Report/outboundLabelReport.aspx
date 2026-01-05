<%@ Page Title="出库标签报表" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="outboundLabelReport.aspx.cs" Inherits="Cargo.Report.outboundLabelReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .tblBtn {
            letter-spacing: 4px;
            /* 字间距可调 */
            padding: 0 2px;
        }

        .space {
            display: inline-flex;
            /* 横向排列 */
            gap: 8px;
            /* 按钮间距统一由 gap 控制 */
            align-items: center;
            /* 垂直居中 */
            vertical-align: middle;
            /* 与其他行内元素对齐，消除多余上间距 */
        }

        .space-lg {
            display: inline-flex;
            /* 横向排列 */
            gap: 22px;
            /* 按钮间距统一由 gap 控制 */
            align-items: center;
            /* 垂直居中 */
            vertical-align: middle;
            /* 与其他行内元素对齐，消除多余上间距 */
        }
    </style>
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
        var UpClientDep;
        window.onload = function () {
            adjustment();
            $.ajaxSetup({ async: true });
            $.getJSON("../Client/clientApi.aspx?method=QueryAllUpClientDep", function (data) {
                UpClientDep = data;
            });
        }
        $(window).resize(function () {
            adjustment();
        });
        function adjustment() {
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - $("div[name='SelectDiv2']").outerHeight(true);
            $('#dg').datagrid({ height: height });
        }
        $(document).ready(function () {
            $('#dg').datagrid({
                width: '100%',
                title: '',
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false,
                collapsible: true,
                pagination: true,
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28),
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2, 100, 200, 500],
                fitColumns: false,
                singleSelect: false,
                checkOnSelect: true,
                idField: 'TagCode',
                url: null,
                rownumbers: true,
                toolbar: '#toolbar',
                columns: [[
                  {
                      title: '仓库', field: 'HouseName', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  { title: '出库时间', field: 'OutCargoTime', width: '135px', formatter: DateTimeFormatter },
                  {
                      title: '标签号', field: 'TagCode', width: '100px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '出库人', field: 'OutCargoOperID', width: '70px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '订单号', field: 'OrderNo', width: '120px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '品牌', field: 'TypeName', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '产品编码', field: 'ProductCode', width: '100px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '产品名称', field: 'ProductName', width: '100px', formatter: function (value) {
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
                      title: '规格', field: 'Specs', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '花纹', field: 'Figure', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '载速', field: 'LoadSpeed', width: '60px', formatter: function (value) {
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
                  {
                      title: '商品码', field: 'GoodsCode', width: '100px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '供应商', field: 'Supplier', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '来源', field: 'SourceName', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '库区', field: 'AreaName', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '分区', field: 'SubAreaName', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '货架', field: 'SectionName', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '货位号', field: 'ContainerCode', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '归属部门', field: 'BelongDepart', width: '70px', formatter: function (value) {
                          return "<span title='" + GetUpClientDepName(value) + "'>" + GetUpClientDepName(value) + "</span>";
                      }
                  },
                  {
                      title: '规格类型', field: 'SpecsType', width: '70px', formatter: function (val, row, index) {
                          if (val == "0") { return "<span title='分配规格'>分配规格</span>"; }
                          else if (val == "1") { return "<span title='普通规格'>普通规格</span>"; }
                          else if (val == "2") { return "<span title='特价分配规格'>特价分配规格</span>"; }
                          else if (val == "3") { return "<span title='手工单'>手工单</span>"; }
                          else if (val == "4") { return "<span title='极速达'>极速达</span>"; }
                          else if (val == "5") { return "<span title='次日达'>次日达</span>"; }
                          else { return ""; }
                      }
                  },
                  {
                      title: '品类', field: 'CategoryName', width: '80px', formatter: function (value) {
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
                onDblClickRow: function (index, row) { }
            });

            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            //所在仓库
            $('#HouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#AreaID').combobox('clear');
                    $('#SubAreaID').combobox('clear');
                    $('#SectionID').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    $('#AreaID').combobox('reload', url);
                }
            });
            $('#HouseID').combobox('textbox').bind('focus', function () { $('#HouseID').combobox('showPanel'); });
            $('#HouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            $('#AreaID').combobox('clear');
            var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=<%=UserInfor.HouseID%>';
            $('#AreaID').combobox('reload', url);
            //库区
            $('#AreaID').combobox({
                valueField: 'AreaID', textField: 'Name',
                onSelect: function (rec) {
                    $('#SubAreaID').combobox('clear');
                    $('#SectionID').combobox('clear');
                    var hid = $('#HouseID').combobox('getValue');
                    var url = '../House/houseApi.aspx?method=QueryALLArea&hid=' + hid + '&pid=' + rec.AreaID;
                    $('#SubAreaID').combobox('reload', url);
                }
            });
            //分区
            $('#SubAreaID').combobox({
                valueField: 'AreaID', textField: 'Name',
                onSelect: function (rec) {
                    $('#SectionID').combobox('clear');
                    var hid = $('#HouseID').combobox('getValue');
                    var url = '../House/houseApi.aspx?method=QueryALLArea&hid=' + hid + '&pid=' + rec.AreaID;
                    $('#SectionID').combobox('reload', url);
                }
            });
            //货架
            $('#SectionID').combobox({
                valueField: 'AreaID', textField: 'Name'
            });
            //一级产品（品类）
            $('#CategoryID').combobox({
                url: '../Product/productApi.aspx?method=QueryALLOneProductType',
                valueField: 'TypeID', textField: 'TypeName',
                onSelect: function (rec) {
                    $('#TypeID').combobox('clear');
                    var url = '../Product/productApi.aspx?method=QueryALLOneProductType&PID=' + rec.TypeID;
                    $('#TypeID').combobox('reload', url);
                }
            });
            $('#CategoryID').combobox('textbox').bind('focus', function () { $('#CategoryID').combobox('showPanel'); });
            //二级产品（品牌）
            $('#TypeID').combobox({
                valueField: 'TypeID', textField: 'TypeName'
            });
            $('#TypeID').combobox('textbox').bind('focus', function () { $('#TypeID').combobox('showPanel'); });

            var RoleCName = "<%=UserInfor.RoleCName%>";
            if (RoleCName.indexOf("安泰路斯") >= 0) {
                $('#HouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
                $("#HouseID").combobox("readonly", true);
                $('#HouseID').combobox('textbox').unbind('focus');
                $('#HouseID').combobox('textbox').css('background-color', '#e8e8e8');
            }
        });

        //统一事件绑定
        $(function () {
            $('#btnExport').on('click', AwbExport);
            $('#btnSearch').on('click', dosearch);
        });

        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'reportApi.aspx?method=QueryOutboundLabelReport';
            $('#dg').datagrid('load', {
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                HouseID: $("#HouseID").combobox('getValue'),
                AreaID: $('#AreaID').combobox('getValue'),
                SubAreaID: $('#SubAreaID').combobox('getValue'),
                SectionID: $('#SectionID').combobox('getValue'),
                CategoryID: $('#CategoryID').combobox('getValue'),
                TypeID: $('#TypeID').combobox('getValue')
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
    <div name="SelectDiv2" style="background: linear-gradient(to bottom, #eff5ff 0px, #e0ecff 100%) repeat-x scroll 0 0; border-color: #95b8e7; border-style: solid; border-width: 1px 1px 0px 1px;">
        <table>
            <tr>
                <td>
                    <a class="easyui-linkbutton" iconcls="icon-chart_bar" plain="false" href="../Report/reportDaily.aspx"
                        target="_self">&nbsp;每日仓库报表&nbsp;</a>&nbsp;&nbsp;<a class="easyui-linkbutton" iconcls="icon-chart_bar" plain="false" href="../Report/saleManReport.aspx" target="_self">
                            &nbsp;每日业务员报表&nbsp;</a>&nbsp;&nbsp;<a class="easyui-linkbutton" iconcls="icon-chart_bar"
                                plain="false" href="../Report/reportMoveContainer.aspx" target="_self">
                            &nbsp;每日移库报表&nbsp;</a>&nbsp;&nbsp;
                    <a class="easyui-linkbutton" iconcls="icon-chart_bar" style="color: Red;" plain="false" href="../Report/outboundLabelReport.aspx" target="_self">&nbsp;出库标签报表&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table>
            <colgroup>
                <col width="80">
                <col width="240">

                <col width="80">
                <col width="140">

                <col width="80">
                <col width="140">

                <col width="80">
                <col width="140">

                <col width="80">
                <col width="140">

                <col width="auto">
            </colgroup>
            <tr>
                <td style="text-align: right;">出库时间:</td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px" /> ~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px" />
                </td>

                <td style="text-align: right;">仓库:</td>
                <td>
                    <input id="HouseID" class="easyui-combobox" style="width: 100px" panelHeight="300" />
                </td>

                <td style="text-align: right;">库区:</td>
                <td>
                    <input id="AreaID" class="easyui-combobox" style="width: 100px" data-options="valueField:'AreaID',textField:'Name'" panelHeight="300" />
                </td>

                <td style="text-align: right;">分区:</td>
                <td>
                    <input id="SubAreaID" class="easyui-combobox" style="width: 100px" data-options="valueField:'AreaID',textField:'Name'" panelHeight="300" />
                </td>

                <td style="text-align: right;">货架:</td>
                <td>
                    <input id="SectionID" class="easyui-combobox" style="width: 100px" data-options="valueField:'AreaID',textField:'Name'" panelHeight="300" />
                </td>

                <td></td>
            </tr>

            <tr>
                <td style="text-align: right;">品类:</td>
                <td>
                    <input id="CategoryID" class="easyui-combobox" style="width: 100px" panelHeight="300" />
                </td>

                <td style="text-align: right;">品牌:</td>
                <td>
                    <input id="TypeID" class="easyui-combobox" style="width: 100px" panelHeight="300" />
                </td>

                <td></td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid"></table>
    <div id="toolbar">
        <div class="space-lg">
            <span class="space">
                <a href="#" class="easyui-linkbutton tblBtn" id="btnSearch" iconcls="icon-search" plain="false">查询</a>
            </span>
            <span class="space">
                <a href="#" class="easyui-linkbutton tblBtn" id="btnExport" iconcls="icon-application_put" plain="false">导出</a>
            </span>
        </div>
    </div>


    <script type="text/javascript">

        //导出数据
        function AwbExport() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }

            var params = {
                method: 'ExportOutboundLabelReport',
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                HouseID: $("#HouseID").combobox('getValue'),
                AreaID: $('#AreaID').combobox('getValue'),
                SubAreaID: $('#SubAreaID').combobox('getValue'),
                SectionID: $('#SectionID').combobox('getValue'),
                CategoryID: $('#CategoryID').combobox('getValue'),
                TypeID: $('#TypeID').combobox('getValue')
            };

            var queryString = Object.keys(params).map(function (key) {
                return encodeURIComponent(key) + '=' + encodeURIComponent(params[key] || '');
            }).join('&');

            window.location.href = 'reportApi.aspx?' + queryString;
        }

        //获取归属部门名称
        function GetUpClientDepName(value) {
            var name = '';
            if (value != "") {
                for (var i = 0; i < UpClientDep.length; i++) {
                    if (UpClientDep[i].ID == value) {
                        name = UpClientDep[i].DepName;
                        break;
                    }
                }
            }
            return name;
        }

    </script>

</asp:Content>
