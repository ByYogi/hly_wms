<%@ Page Title="每日移库报表" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reportMoveContainer.aspx.cs" Inherits="Cargo.Report.reportMoveContainer" %>

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
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - $("div[name='SelectDiv2']").outerHeight(true);
            $('#dg').datagrid({ height: height });
        }
        $(document).ready(function () {
            $('#dg').datagrid({
                width: '100%',
                //height:'500px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                rownumbers: true,
                columns: [[
                  {
                      title: '产品编码', field: 'ProductCode', width: '100px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
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
                      title: '成本价', field: 'CostPrice', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '原货位代码', field: 'OldCCode', width: '90px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '原货位区域', field: 'OldAreaName', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '原所在仓库', field: 'OldHouseName', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '移库数量', field: 'MoveNum', width: '60px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '新货位代码', field: 'NewCCode', width: '90px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '新货位区域', field: 'NewAreaName', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '新仓库', field: 'NewHouseName', width: '80px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '移库状态', field: 'MoveStatus', width: '60px', formatter: function (val, row, index) {
                          if (val == "1") { return "<span title='全部移'>全部移</span>"; } else if (val == "2") { return "<span title='部分移'>部分移</span>"; } else { return ""; }
                      }
                  },
                  {
                      title: '操作人', field: 'UserName', width: '70px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  { title: '移库时间', field: 'OP_DATE', width: '135px', formatter: DateTimeFormatter }
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
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#FirstAreaID').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    $('#FirstAreaID').combobox('reload', url);
                    //一级仓库
                    $('#FirstAreaID').combobox({
                        onSelect: function (fai) {
                            $('#ParentAreaID').combobox('clear');
                            var url = '../House/houseApi.aspx?method=QueryALLArea&hid=' + rec.HouseID + '&pid=' + fai.AreaID;
                            $('#ParentAreaID').combobox('reload', url);
                        }
                    });
                }
            });
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            $('#FirstAreaID').combobox('textbox').bind('focus', function () { $('#FirstAreaID').combobox('showPanel'); });
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            $('#FirstAreaID').combobox('clear');
            var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=<%=UserInfor.HouseID%>';
            $('#FirstAreaID').combobox('reload', url);
            //一级仓库
            $('#FirstAreaID').combobox({
                onSelect: function (fai) {
                    $('#ParentAreaID').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryALLArea&hid=<%=UserInfor.HouseID%>&pid=' + fai.AreaID;
                    $('#ParentAreaID').combobox('reload', url);
                }
            });
            var RoleCName = "<%=UserInfor.RoleCName%>";
            if (RoleCName.indexOf("安泰路斯") >= 0) {
                $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
                $("#AHouseID").combobox("readonly", true);
                $('#AHouseID').combobox('textbox').unbind('focus');
                $('#AHouseID').combobox('textbox').css('background-color', '#e8e8e8');

            }
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'reportApi.aspx?method=QueryMoveContainerReport';
            $('#dg').datagrid('load', {
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                HouseID: $("#AHouseID").combobox('getValue'),//仓库ID
                FirstAreaID: $('#FirstAreaID').combobox('getValue'),
                ParentAreaID: $('#ParentAreaID').combobox('getValue')
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
                            &nbsp;每日业务员报表&nbsp;</a>&nbsp;&nbsp;<a class="easyui-linkbutton" iconcls="icon-chart_bar" style="color: Red;"
                                plain="false" href="../Report/reportMoveContainer.aspx" target="_self">
                            &nbsp;每日移库报表&nbsp;</a>&nbsp;&nbsp;
                    <a class="easyui-linkbutton" iconcls="icon-chart_bar" plain="false" href="../Report/outboundLabelReport.aspx" target="_self">&nbsp;出库标签报表&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'">
        <table>
            <tr>
                <td style="text-align: right;">查询时间:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                        <input id="EndDate" class="easyui-datebox" style="width: 100px">
                </td>
                <td style="text-align: right;">区域大仓:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;"
                        panelheight="auto" />
                </td>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="FirstAreaID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'AreaID',textField:'Name'"
                        panelheight="auto" />
                </td>
                <td style="text-align: right;">一级区域:
                </td>
                <td>
                    <input id="ParentAreaID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'AreaID',textField:'Name'"
                        panelheight="auto" />
                </td>
                <td><a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-application_put"
                    plain="false" onclick="AwbExport()">&nbsp;导&nbsp;出&nbsp;</a>
                    <form runat="server" id="fm1">
                        <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
                    </form>
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>


    <script type="text/javascript">

        //导出数据
        function AwbExport() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click();
        }

    </script>

</asp:Content>
