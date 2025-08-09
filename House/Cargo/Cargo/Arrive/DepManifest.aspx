<%@ Page Title="长运配载操作" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DepManifest.aspx.cs" Inherits="Cargo.Arrive.DepManifest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../JS/Lodop/LodopFuncs.js" type="text/javascript"></script>

    <script type="text/javascript">
        //页面加载时执行
        window.onload = function () {
            adjustment();
        }
        $(window).resize(function () {
            adjustment();
        });
        function adjustment() {
            var height = (Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 30.1) / 2;
            $('#dg').datagrid({ height: height });
            $('#dgManifest').datagrid({ height: height });
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
                title: '在站运单', //标题内容
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
                      title: '运单号', field: 'AwbNo', width: '6%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  { title: '开单日期', field: 'HandleTime', width: '6%', formatter: DateFormatter },
                  {
                      title: '最迟时效', field: 'LatestTimeLimit', width: '6%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '客户名称', field: 'ShipperUnit', width: '10%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '收货人', field: 'AcceptPeople', width: '5%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '结款方式', field: 'CheckOutType', width: '5%',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "<span title='现付'>现付</span>"; } else if (val == "1") { return "<span title='回单'>回单</span>"; } else if (val == "2") { return "<span title='月结'>月结</span>"; } else if (val == "3") { return "<span title='到付'>到付</span>"; } else if (val == "4") { return "<span title='代收款'>代收款</span>"; } else { return ""; }
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
                      title: '重量', field: 'Weight', width: '5%', formatter: function (value, row) {
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
                      title: '体积', field: 'Volume', width: '5%', formatter: function (value, row) {
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
                      title: '附加信息', field: 'Remark', width: '11.5%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  }
                ]],
                onDblClickRow: function (index, row) { up(index, row); },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                rowStyler: function (index, row) {
                    if (row.TrafficType == "2") { return "font-weight:bold;"; }
                    if (row.AwbPiece != row.Piece) { return "background-color:#EBEDE1"; }
                },
                onLoadSuccess: function () {
                    var row = $('#dg').datagrid('getRows');
                    var row1 = $('#dgManifest').datagrid('getRows');
                    if ($('#dg').datagrid('getRows').length < $('#dgManifest').datagrid('getRows').length) {
                        var row1 = $('#dg').datagrid('getRows');
                        var row = $('#dgManifest').datagrid('getRows');
                    }
                    for (var j = 0; j < row.length; j++) {
                        for (var i = 0; i < row1.length; i++) {
                            if (row[j].AwbID == row1[i].AwbID) {
                                $('#dg').datagrid('deleteRow', j);
                            }
                        }
                    }
                }
            });
            $('#dgManifest').datagrid({
                width: '100%',
                title: '配载运单', //标题内容
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
                      title: '运单号', field: 'AwbNo', width: '6%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  { title: '开单日期', field: 'HandleTime', width: '6%', formatter: DateFormatter },
                  {
                      title: '最迟时效', field: 'LatestTimeLimit', width: '6%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '客户名称', field: 'ShipperUnit', width: '10%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '收货人', field: 'AcceptPeople', width: '5%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '结款方式', field: 'CheckOutType', width: '5%',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "<span title='现金'>现金</span>"; } else if (val == "1") { return "<span title='提付'>提付</span>"; } else if (val == "2") { return "<span title='回单'>回单</span>"; } else if (val == "3") { return "<span title='月结'>月结</span>"; } else if (val == "4") { return "<span title='代收款'>代收款</span>"; } else { return ""; }
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
                      title: '重量', field: 'Weight', width: '5%', formatter: function (value, row) {
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
                      title: '体积', field: 'Volume', width: '5%', formatter: function (value, row) {
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
                      title: '附加信息', field: 'Remark', width: '11.5%', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  }
                ]],
                onDblClickRow: function (index, row) { down(index, row); },
                onClickRow: function (index, row) {
                    $('#dgManifest').datagrid('clearSelections');
                    $('#dgManifest').datagrid('selectRow', index);
                },
                rowStyler: function (index, row) {
                    if (row.TrafficType == "2") { return "font-weight:bold;"; }
                    if (row.AwbPiece != row.Piece) { return "background-color:#EBEDE1"; }
                }
            });
            $('#dgDriver').datagrid({
                width: '100%',
                title: '配载运单', //标题内容
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
                      title: '运单号', field: 'AwbNo', width: '95px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  { title: '开单日期', field: 'HandleTime', width: '95px', formatter: DateFormatter },
                  {
                      title: '最迟时效', field: 'LatestTimeLimit', width: '95px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '客户名称', field: 'ShipperUnit', width: '170px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '收货人', field: 'AcceptPeople', width: '95px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '结款方式', field: 'CheckOutType', width: '85px',
                      formatter: function (val, row, index) {
                          if (val == "0") { return "现付"; } else if (val == "1") { return "回单"; } else if (val == "2") { return "月结"; } else if (val == "3") { return "到付"; } else if (val == "4") { return "代收款"; } else { return ""; }
                      }
                  },
                  {
                      title: '出发站', field: 'Dep', width: '85px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '中间站', field: 'MidDest', width: '95px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '到达站', field: 'Dest', width: '85px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '中转站', field: 'Transit', width: '85px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '品名', field: 'Goods', width: '180px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '总费用', field: 'TotalCharge', width: '85px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '件数', field: 'Piece', width: '85px', formatter: function (value, row) {
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
                      title: '重量', field: 'Weight', width: '85px', formatter: function (value, row) {
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
                      title: '体积', field: 'Volume', width: '85px', formatter: function (value, row) {
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
                      title: '附加信息', field: 'Remark', width: '200px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  }
                ]],
                onDblClickRow: function (index, row) { down(index, row); },
                onClickRow: function (index, row) {
                    $('#dgDriver').datagrid('clearSelections');
                    $('#dgDriver').datagrid('selectRow', index);
                },
                rowStyler: function (index, row) {
                    if (row.TrafficType == "2") { return "font-weight:bold;"; }
                    if (row.AwbPiece != row.Piece) { return "background-color:#EBEDE1"; }
                }
            });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#DisplayNum').val(0);
            $('#DisplayPiece').val(0);
            $('#DisplayWeight').val(0);
            $('#DisplayVolume').val(0);
            $('#DisplayMoney').val(0);
            $('#ATotalNum').numberbox('setValue', '');
            $('#ATotalMoney').numberbox('setValue', '');
            $('#AWeight').numberbox('setValue', '');
            $('#AVolume').numberbox('setValue', '');
            $('#OPNAME').textbox('setValue', "<%=UserInfor.UserName %>");
            var bt = "<%=UserInfor.NewLandBelongSystem %>";
            if (bt == "DR") {
                $('#ArrivePay').text("回付");
            } else {
                $('#ArrivePay').text("到付");
            }


            $('#Dep').combobox('textbox').bind('focus', function () { $('#Dep').combobox('showPanel'); });
            $('#StartDate').datetimebox('textbox').bind('focus', function () { $('#StartDate').datetimebox('showPanel'); });
            $('#EndDate').datetimebox('textbox').bind('focus', function () { $('#EndDate').datetimebox('showPanel'); });
            $('#Dest').combobox('textbox').bind('focus', function () { $('#Dest').combobox('showPanel'); });
            $('#TruckNum').combobox('textbox').bind('focus', function () { $('#TruckNum').combobox('showPanel'); });
            $('#StartTime').datetimebox('textbox').bind('focus', function () { $('#StartTime').datetimebox('showPanel'); });
            $('#CardName').combobox('textbox').bind('focus', function () { $('#CardName').combobox('showPanel'); });
            $('#Dep').combobox('setValues', '<%=UserInfor.DepCity%>');
            $('#Dep').combobox('disable');
            $('#ADep').combobox('setValues', '<%=UserInfor.DepCity%>');


            $('#tbDepmanifest').tabs({
                onSelect: function (title, index) {
                    if (index == 0) {
                        var height = (Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 30.1) / 2;
                        $('#dg').datagrid({ height: height });
                        $('#dgManifest').datagrid({ height: height });
                    } else if (index == 1) {
                        var height = (Number($(window).height()) - $("div[name='SelectDiv5']").outerHeight(true) - 30.1);
                        $('#dgDriver').datagrid({ height: height });
                    }
                }
            });
        });

        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            var row = $('#dgManifest').datagrid('getRows');
            var AwbID = null;
            for (var i = 0; i < row.length; i++) {
                if (AwbID != null) {
                    AwbID += "," + row[i].AwbID;
                } else {
                    AwbID = row[i].AwbID;
                }
            }
            gridOpts.url = '../Arrive/ArriveApi.aspx?method=QueryAwbByDest';
            $('#dg').datagrid('load', {
                AwbNo: $('#AwbNo').val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                Dep: $("#Dep").combobox('getValue'),
                AwbIDs: AwbID,
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
    <div id="tbDepmanifest" class="easyui-tabs" data-options="fit:true">
        <div title="长运配载" data-options="iconCls:'icon-table_edit'">
            <div id="saPanel" name="SelectDiv1">
                <table>
                    <tr>
                        <td style="text-align: right;">运单号:
                        </td>
                        <td>
                            <input id="AwbNo" class="easyui-textbox" data-options="prompt:'请输入运单号'" style="width: 100px">
                        </td>
                        <td style="text-align: right;">出发站:
                        </td>
                        <td>
                            <input id="Dep" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../Arrive/ArriveApi.aspx?method=QueryAllCity',multiple:true"
                                panelheight="auto" />
                        </td>
                        <td style="text-align: right;">到达站:
                        </td>
                        <td>
                            <input id="Dest" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../Arrive/ArriveApi.aspx?method=QueryAllCity',multiple:true" />
                        </td>
                        <td style="text-align: right;">时间范围:
                        </td>
                        <td>
                            <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                        <input id="EndDate" class="easyui-datebox" style="width: 100px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="8">
                            <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
                        <a href="#" class="easyui-linkbutton" iconcls="icon-arrow_out" plain="false" onclick="tear()">&nbsp;拆&nbsp;票&nbsp;</a>&nbsp;&nbsp;
                        <a href="#" class="easyui-linkbutton" iconcls="icon-arrow_in" plain="false" onclick="merge()">&nbsp;合&nbsp;并&nbsp;</a>&nbsp;&nbsp;
                        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="plManifest()">&nbsp;批量配载&nbsp;</a>
                        </td>
                    </tr>
                </table>
            </div>
            <input type="hidden" id="DisplayNum" />
            <input type="hidden" id="DisplayPiece" />
            <input type="hidden" id="DisplayWeight" />
            <input type="hidden" id="DisplayVolume" />
            <input type="hidden" id="DisplayMoney" />
            <table id="dg" class="easyui-datagrid">
            </table>
            <table id="dgManifest" class="easyui-datagrid">
            </table>
            <div id="dlg" class="easyui-dialog" style="width: 500px; height: 200px; padding: 10px 10px"
                closed="true" buttons="#dlg-buttons">
                <form id="fm" class="easyui-form" method="post">
                    <input type="hidden" name="AwbID" id="AAwbID" />
                    <table>
                        <tr>
                            <td style="text-align: right;">总件数:
                            </td>
                            <td>
                                <input id="TotalAwbPiece" readonly="true" class="easyui-numberbox" data-options="min:0,precision:0"
                                    style="width: 80px;">
                            </td>
                            <td style="text-align: right;">总重量:
                            </td>
                            <td>
                                <input id="TotalAwbWeight" readonly="true" class="easyui-numberbox" data-options="min:0,precision:3"
                                    style="width: 80px;">
                            </td>
                            <td style="text-align: right;">总体积:
                            </td>
                            <td>
                                <input id="TotalAwbVolume" readonly="true" class="easyui-numberbox" data-options="min:0,precision:3"
                                    style="width: 80px;">
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">分批件数:
                            </td>
                            <td>
                                <input id="TearPiece" name="TearPiece" class="easyui-numberbox" data-options="min:0,precision:0"
                                    style="width: 80px;">
                            </td>
                            <td style="text-align: right;">分批重量:
                            </td>
                            <td>
                                <input id="TearWeight" readonly="true" class="easyui-numberbox" data-options="min:0,precision:3"
                                    style="width: 80px;">
                            </td>
                            <td style="text-align: right;">分批体积:
                            </td>
                            <td>
                                <input id="TearVolume" readonly="true" class="easyui-numberbox" data-options="min:0,precision:3"
                                    style="width: 80px;">
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">剩余件数:
                            </td>
                            <td>
                                <input id="OtherPiece" readonly="true" class="easyui-numberbox" data-options="min:0,precision:0"
                                    style="width: 80px;">
                            </td>
                            <td style="text-align: right;">剩余重量:
                            </td>
                            <td>
                                <input id="OtherWeight" readonly="true" class="easyui-numberbox" data-options="min:0,precision:3"
                                    style="width: 80px;">
                            </td>
                            <td style="text-align: right;">剩余体积:
                            </td>
                            <td>
                                <input id="OtherVolume" readonly="true" class="easyui-numberbox" data-options="min:0,precision:3"
                                    style="width: 80px;">
                            </td>
                        </tr>
                    </table>
                </form>
            </div>
            <div id="dlg-buttons">
                <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveTear()">保存</a>
                <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">取消</a>
            </div>
        </div>
        <div title="司机合同" data-options="iconCls:'icon-lorry'">
            <div id="saPanel" name="SelectDiv5">
                <form id="fmDep" class="easyui-form" method="post">
                    <input name="DepCellPhone" id="DepCellPhone" type="hidden" />
                    <input type="hidden" id="AContractNum" />
                    <table>
                        <tr>
                            <td style="text-align: right;">车牌号码:
                            </td>
                            <td>
                                <input name="TruckNum" id="TruckNum" class="easyui-combobox" style="width: 100px;" data-options="valueField:'TruckNum',textField:'TruckNum',url:'../Arrive/ArriveApi.aspx?method=QueryTruck&TripMark=0',onSelect:truckChange,required:true" />
                            </td>
                            <td style="text-align: right;">车长:
                            </td>
                            <td>
                                <input class="easyui-combobox" name="Length" id="Length" data-options="url:'../Data/CarLength.json',method:'get',valueField:'id',textField:'text',panelHeight:'auto',required:true,editable:false" style="width: 100px;">
                            </td>
                            <td style="text-align: right;">车型:
                            </td>
                            <td>
                                <input class="easyui-combobox" name="Model" id="Model" data-options="url:'../Data/CarModel.json',method:'get',valueField:'id',textField:'text',panelHeight:'auto',required:true,editable:false" style="width: 150px;">
                            </td>
                            <td style="text-align: right;">发车时间:
                            </td>
                            <td>
                                <input id="StartTime" name="StartTime" class="easyui-datetimebox" data-options="required:true" style="width: 150px">
                            </td>
                            <td style="text-align: right;">运行:
                            </td>
                            <td>
                                <input id="PassTime" name="PassTime" class="easyui-numberbox" style="width: 100px">
                            </td>
                            <td style="text-align: right;">调车员:
                            </td>
                            <td>
                                <input id="Dispatcher" name="Dispatcher" class="easyui-textbox" style="width: 100px">
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
                            <td style="text-align: right;">身份证号:
                            </td>
                            <td>
                                <input id="DriverIDNum" name="DriverIDNum" class="easyui-textbox" style="width: 150px">
                            </td>
                            <td style="text-align: right;">地址:
                            </td>
                            <td colspan="1">
                                <input id="DriverIDAddress" name="DriverIDAddress" class="easyui-textbox" style="width: 270px">
                            </td>
                            <td style="text-align: right;">途径:
                            </td>
                            <td>
                                <input id="Transit" name="Transit" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../Arrive/ArriveApi.aspx?method=QueryAllCity',editable:false"
                                    panelheight="auto" />
                            </td>
                            <td style="text-align: right;">操作员:
                            </td>
                            <td>
                                <input id="OPNAME" readonly="true" name="OPNAME" class="easyui-textbox" style="width: 100px">
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">联系人:
                            </td>
                            <td>
                                <input class="easyui-combobox" name="DestPeople" id="DestPeople" data-options="url:'../Arrive/ArriveApi.aspx?method=GetDeptByUnitID',method:'get',valueField:'People',textField:'People',onSelect:onDestPeopleChanged,editable:false" style="width: 100px;">
                            </td>
                            <td style="text-align: right;">手机:
                            </td>
                            <td>
                                <input id="DestCellphone" name="DestCellphone" class="easyui-textbox" style="width: 100px">
                            </td>
                            <td style="text-align: right;">卸货地址:
                            </td>
                            <td>
                                <input id="UnLoadAddress" name="UnLoadAddress" class="easyui-textbox" style="width: 150px">
                            </td>
                            <td style="text-align: right;">起讫:
                            </td>
                            <td colspan="1">
                                <input id="ADep" name="Dep" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../Arrive/ArriveApi.aspx?method=QueryAllCity',onSelect:depSetValue,editable:false,required:true" />→
                        <input id="ADest" name="Dest" class="easyui-combobox" style="width: 100px;" data-options="valueField:'CityName',textField:'CityName',url:'../Arrive/ArriveApi.aspx?method=QueryAllCity',onSelect:destSetValue,editable:false,required:true" />
                            </td>
                            <td style="text-align: right;">体积:
                            </td>
                            <td colspan="1">
                                <input id="AVolume" name="Volume" class="easyui-numberbox" data-options="min:0,precision:2"
                                    style="width: 100px">
                            </td>
                            <td style="text-align: right;">监装人:
                            </td>
                            <td>
                                <input id="Loader" name="Loader" class="easyui-textbox" style="width: 100px">
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">长途运费:
                            </td>
                            <td>
                                <input id="TransportFee" name="TransportFee" class="easyui-numberbox" data-options="min:0,precision:2,required:true"
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
                                    style="width: 150px">
                            </td>
                            <td style="text-align: right;">合计:
                            </td>
                            <td>
                                <input id="ATotalNum" name="TotalNum" class="easyui-numberbox" data-options="min:0,precision:0"
                                    style="width: 100px">票
                        <input id="ATotalMoney" name="TotalMoney" class="easyui-numberbox" data-options="min:0,precision:2"
                            style="width: 100px">元
                            </td>
                            <td style="text-align: right;">重量:
                            </td>
                            <td colspan="1">
                                <input id="AWeight" name="Weight" class="easyui-numberbox" data-options="min:0,precision:2"
                                    style="width: 100px">
                            </td>
                            <td colspan="2"></td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">开户名:
                            </td>
                            <td>
                                <input class="easyui-combobox" name="CardName" id="CardName" data-options="valueField:'CardName',textField:'CardName',panelHeight:'auto',onSelect:onCardNameChanged"
                                    style="width: 100px;">
                            </td>
                            <td style="text-align: right;">开户行:
                            </td>
                            <td colspan="3">
                                <input id="CardBank" name="CardBank" class="easyui-textbox" style="width: 300px">
                            </td>
                            <td style="text-align: right;">卡号:
                            </td>
                            <td colspan="1">
                                <input id="CardNum" name="CardNum" class="easyui-textbox" style="width: 300px">
                            </td>
                            <td colspan="4"></td>
                        </tr>
                        <tr>
                            <td colspan="6">&nbsp;&nbsp;&nbsp;&nbsp;
                                <a href="#" class="easyui-linkbutton" id="btnSave" iconcls="icon-ok" onclick="SaveManifest()">&nbsp;保存配载&nbsp;</a>&nbsp;&nbsp;
                                <a href="#" class="easyui-linkbutton" iconcls="icon-print" onclick="PrePrint()">&nbsp;打印预览&nbsp;</a>&nbsp;&nbsp;
                                <a href="#" class="easyui-linkbutton" iconcls="icon-clear" onclick="cancelManifest()">&nbsp;清&nbsp;空&nbsp;</a>
                            </td>
                            <td style="text-align: right;">备注:
                            </td>
                            <td colspan="9">
                                <textarea name="Remark" id="Remark" cols="80" style="height: 35px;" class="mini-textarea">*小时以内到达档口，奖励100，如果超过*小时还没到达档口，每延迟1小时扣50，如有特殊情况，立刻拍照，跟档口联系人联系！</textarea>
                            </td>
                        </tr>
                    </table>
                </form>
            </div>
            <table id="dgDriver" class="easyui-datagrid">
            </table>
        </div>
    </div>
    <object id="LODOP_OB" title="dd" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA"
        width="0px" height="0px">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0px" height="0px"></embed>
    </object>

    <script type="text/javascript">
        //保存配载
        function SaveManifest() {
            var row = $('#dgDriver').datagrid('getRows');
            if (row.length <= 0) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '配载列表中没有数据', 'warning'); return; }
            var tp = $('#TransportFee').numberbox('getText');
            if (tp == "" || tp < 0) {
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
            $('#fmDep').form('submit', {
                url: "../Arrive/ArriveApi.aspx?method=SaveManifest",
                contentType: "application/json;charset=utf-8", dataType: "json",
                onSubmit: function (param) {
                    param.PTruckNum = $('#TruckNum').combobox('getText');
                    param.PCardName = $('#CardName').combobox('getText');
                    param.submitData = json
                    var trd = $(this).form('enableValidation').form('validate');
                    if (trd) { $('#btnSave').linkbutton('disable'); }
                    return trd;
                },
                success: function (msg) {
                    $('#btnSave').linkbutton('enable');
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $('#AContractNum').val(result.Message); //将配载合同号返回保存在界面上打印用
                        $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '配载成功，是否打印司机合同？', function (r) {
                            if (r) {
                                PrePrint();
                            }
                            location.reload();
                        });
                    } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning'); }
                }
            })
        }
        //清空
        function cancelManifest() {
            $('#fmDep').form('clear');
            $('#dgManifest').datagrid('loadData', { total: 0, rows: [] });
            $('#dgDriver').datagrid('loadData', { total: 0, rows: [] });
            $('#DisplayNum').val('');
            $('#DisplayPiece').val('');
            $('#DisplayWeight').val('');
            $('#DisplayVolume').val('');
            $('#DisplayMoney').val('');
        }
        //合并
        function merge() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows.length <= 1) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择需要合并的在站运单分批数据！', 'warning'); return; }
            var awbNos = new Array();
            $.each(rows, function (i, row) {
                var index = $.inArray(row.AwbNo, awbNos);
                if (index == '-1') {
                    awbNos.push(row.AwbNo);
                }
            });
            if (awbNos.length > 1) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '相同运单号才可进行合并！', 'warning');
                return;
            }
            var json = JSON.stringify(rows)
            $.ajax({
                url: "../Arrive/ArriveApi.aspx?method=merge",
                type: "post",
                data: { submitData: json },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '合并成功!', 'info');
                        dosearch();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '合并失败：' + result.Message, 'warning');
                    }
                }
            });
        }
        //分批
        function tear() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows[0].AwbPiece <= 1) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '总件数不可再分批！', 'warning');
                return;
            }
            if (rows.length <= 0) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择进行分批的运单数据！', 'warning'); return; }
            if (rows.length > 1) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '只能选择一条运单数据进行分批！', 'warning'); return; }
            var p = rows[0].AwbPiece;
            var w = rows[0].AwbWeight;
            var v = rows[0].AwbVolume;
            var awbno = rows[0].AwbNo;
            $('#TotalAwbWeight').numberbox('textbox').css('background-color', '#e8e8e8');
            $('#TotalAwbVolume').numberbox('textbox').css('background-color', '#e8e8e8');
            $('#TearWeight').numberbox('textbox').css('background-color', '#e8e8e8');
            $('#TearVolume').numberbox('textbox').css('background-color', '#e8e8e8');
            $('#OtherWeight').numberbox('textbox').css('background-color', '#e8e8e8');
            $('#OtherVolume').numberbox('textbox').css('background-color', '#e8e8e8');
            $('#TotalAwbPiece').numberbox('textbox').css('background-color', '#e8e8e8');
            $('#OtherPiece').numberbox('textbox').css('background-color', '#e8e8e8');
            $('#dlg').dialog('open').dialog('setTitle', awbno + '运单分批');
            $('#fm').form('clear');
            $("#AAwbID").val(rows.AwbID);
            $("#TearPiece").numberbox('setValue', '');
            $("#TotalAwbPiece").numberbox('setValue', p);
            $("#TotalAwbWeight").numberbox('setValue', w);
            $("#TotalAwbVolume").numberbox('setValue', v);
            $("#TearPiece").numberbox({
                "onChange": function (newValue, oldValue) {
                    var tp = newValue; //分批件数
                    if (tp >= p) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '分批件数必须小于总件数！', 'warning');
                        return;
                    }
                    var op = Number(p) - Number(tp); //剩余件数
                    var tw = (Number(w) * Number(tp) / Number(p)).toFixed(3); //分批重量
                    var tv = (Number(v) * Number(tp) / Number(p)).toFixed(3); //分批体积
                    $("#OtherPiece").numberbox('setValue', op);
                    $("#TearWeight").numberbox('setValue', tw);
                    $("#TearVolume").numberbox('setValue', tv);
                    $("#OtherWeight").numberbox('setValue', (w - tw).toFixed(3));
                    $("#OtherVolume").numberbox('setValue', (v - tv).toFixed(3));
                }
            });
        }
        //保存分批
        function saveTear() {
            var row = $('#dg').datagrid('getSelected');
            $('#fm').form('submit', {
                url: '../Arrive/ArriveApi.aspx?method=Tear&AwbID=' + row.AwbID + "&AwbPiece=" + row.AwbPiece + "&AwbWeight=" + row.AwbWeight + "&AwbVolume=" + row.AwbVolume + "&AwbNo=" + row.AwbNo,
                contentType: "application/json;charset=utf-8",
                onSubmit: function (param) {
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '分批成功!', 'info');
                        $('#dlg').dialog('close'); 	// close the dialog
                        dosearch();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '分批失败：' + result.Message, 'warning');
                    }
                }
            })
        }
        //单个配载
        function up(index, rows) {
            $('#dg').datagrid('deleteRow', index);
            var index = $('#dgManifest').datagrid('getRowIndex', rows.AwbID);
            var indexd = $('#dgDriver').datagrid('getRowIndex', rows.AwbID);
            if (index < 0) {
                $('#dgManifest').datagrid('appendRow', rows);
                $('#dgDriver').datagrid('appendRow', rows);
                var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
                var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
                var w = $('#DisplayWeight').val() == "" || isNaN($('#DisplayWeight').val()) ? 0 : Number($('#DisplayWeight').val());
                var v = $('#DisplayVolume').val() == "" || isNaN($('#DisplayVolume').val()) ? 0 : Number($('#DisplayVolume').val());
                var m = $('#DisplayMoney').val() == "" || isNaN($('#DisplayMoney').val()) ? 0 : Number($('#DisplayMoney').val());
                n++;
                var pt = p + Number(rows.AwbPiece);
                var wt = w + Number(rows.AwbWeight);
                var vt = v + Number(rows.AwbVolume);
                var mt = m + Number(rows.TotalCharge);
                $('#DisplayNum').val(Number(n));
                $('#DisplayPiece').val(Number(pt));
                $('#DisplayWeight').val(Number(wt).toFixed(2));
                $('#DisplayVolume').val(Number(vt).toFixed(2));
                $('#DisplayMoney').val(Number(mt).toFixed(2));
                $('#ATotalNum').numberbox('setValue', Number(n));
                $('#AWeight').numberbox('setValue', Number(wt).toFixed(2));
                $('#AVolume').numberbox('setValue', Number(vt).toFixed(2));
                $('#ATotalMoney').numberbox('setValue', Number(mt).toFixed(2));
                var title = "配载运单      已配载：" + pt + " 件，重量：" + Number(wt).toFixed(2) + " 吨，体积：" + Number(vt).toFixed(2) + " 方，总收入：" + Number(mt).toFixed(2) + " 元";
                $('#dgManifest').datagrid("getPanel").panel("setTitle", title);
                $('#dgDriver').datagrid("getPanel").panel("setTitle", title);
            }
        }
        //移除
        function down(index, rows) {
            $('#dgManifest').datagrid('deleteRow', index);
            $('#dgDriver').datagrid('deleteRow', index);
            var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
            var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
            var w = $('#DisplayWeight').val() == "" || isNaN($('#DisplayWeight').val()) ? 0 : Number($('#DisplayWeight').val());
            var v = $('#DisplayVolume').val() == "" || isNaN($('#DisplayVolume').val()) ? 0 : Number($('#DisplayVolume').val());
            var m = $('#DisplayMoney').val() == "" || isNaN($('#DisplayMoney').val()) ? 0 : Number($('#DisplayMoney').val());
            n--;
            var pt = p - Number(rows.AwbPiece);
            var wt = w - Number(rows.AwbWeight);
            var vt = v - Number(rows.AwbVolume);
            var mt = m - Number(rows.TotalCharge);
            $('#DisplayNum').val(Number(n));
            $('#DisplayPiece').val(Number(pt));
            $('#DisplayWeight').val(Number(wt).toFixed(2));
            $('#DisplayVolume').val(Number(vt).toFixed(2));
            $('#DisplayMoney').val(Number(mt).toFixed(2));
            $('#ATotalNum').numberbox('setValue', Number(n));
            $('#AWeight').numberbox('setValue', Number(wt).toFixed(2));
            $('#AVolume').numberbox('setValue', Number(vt).toFixed(2));
            $('#ATotalMoney').numberbox('setValue', Number(mt).toFixed(2));
            var title = "配载运单      已配载：" + pt + " 件，重量：" + Number(wt).toFixed(2) + " 吨，体积：" + Number(vt).toFixed(2) + " 方，总收入：" + Number(mt).toFixed(2) + " 元";
            $('#dgManifest').datagrid("getPanel").panel("setTitle", title);
            $('#dgDriver').datagrid("getPanel").panel("setTitle", title);
            var index = $('#dg').datagrid('getRowIndex', rows.AwbID);
            if (index < 0) {
                $('#dg').datagrid('appendRow', rows);
            }
        }
        //批量选择
        function plManifest() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择在站运单数据！', 'warning'); return; }
            var copyRows = [];
            for (var i = 0, l = rows.length; i < l; i++) {
                var row = rows[i];
                copyRows.push(row);
                var index = $('#dgManifest').datagrid('getRowIndex', copyRows[i].AwbID);
                var index = $('#dgDriver').datagrid('getRowIndex', copyRows[i].AwbID);
                if (index < 0) {
                    $('#dgManifest').datagrid('appendRow', row);
                    $('#dgDriver').datagrid('appendRow', row);
                    var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
                    var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
                    var w = $('#DisplayWeight').val() == "" || isNaN($('#DisplayWeight').val()) ? 0 : Number($('#DisplayWeight').val());
                    var v = $('#DisplayVolume').val() == "" || isNaN($('#DisplayVolume').val()) ? 0 : Number($('#DisplayVolume').val());
                    var m = $('#DisplayMoney').val() == "" || isNaN($('#DisplayMoney').val()) ? 0 : Number($('#DisplayMoney').val());
                    n++;
                    var pt = p + Number(row.AwbPiece);
                    var wt = w + Number(row.AwbWeight);
                    var vt = v + Number(row.AwbVolume);
                    var mt = m + Number(row.TotalCharge);
                    $('#DisplayNum').val(Number(n));
                    $('#DisplayPiece').val(Number(pt));
                    $('#DisplayWeight').val(Number(wt).toFixed(2));
                    $('#DisplayVolume').val(Number(vt).toFixed(2));
                    $('#DisplayMoney').val(Number(mt).toFixed(2));
                    $('#ATotalNum').numberbox('setValue', Number(n));
                    $('#AWeight').numberbox('setValue', Number(wt).toFixed(2));
                    $('#AVolume').numberbox('setValue', Number(vt).toFixed(2));
                    $('#ATotalMoney').numberbox('setValue', Number(mt).toFixed(2));
                    var title = "配载运单      已配载：" + pt + " 件，重量：" + Number(wt).toFixed(2) + " 吨，体积：" + Number(vt).toFixed(2) + " 方，总收入：" + Number(mt).toFixed(2) + " 元";
                    $('#dgManifest').datagrid("getPanel").panel("setTitle", title);
                    $('#dgDriver').datagrid("getPanel").panel("setTitle", title);
                }
            }
            for (var i = 0; i < copyRows.length; i++) {
                var index = $('#dg').datagrid('getRowIndex', copyRows[i]);
                $('#dg').datagrid('deleteRow', index);
            }
        }
        //司机银行卡选择方法
        function onCardNameChanged(item) {
            if (item) {
                $('#CardBank').textbox('setValue', item.CardBank);
                $('#CardNum').textbox('setValue', item.CardNum);
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
            //$.ajax({
            //    url: "../SystemManager/systemApi.aspx?method=GetUnitByCityCode&id=" + escape(item.CityName),
            //    cache: false,
            //    success: function (text) {
            //        var o = eval('(' + text + ')');
            //        $('#DepCellPhone').val(o.CellPhone);
            //    }
            //});
        }
        //到达站选择后赋值
        function destSetValue(item) {
            $.ajax({
                url: "../Arrive/ArriveApi.aspx?method=GetUnitByCityCode&id=" + escape(item.CityName),
                cache: false,
                success: function (text) {
                    var o = eval('(' + text + ')');
                    $('#DestPeople').combobox('select', o.Boss);
                    if (o.Boss == null) {
                        $("#DestCellphone").textbox('setValue', "");
                        $("#UnLoadAddress").textbox('setValue', "");
                    }
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
            //cancelManifest();
        }
        //打印设置
        function CreateDataBill() {
            LODOP.SET_PRINTER_INDEX(-1);
            LODOP.SET_PRINT_PAGESIZE(1, 2400, 2750, "");
            LODOP.SET_PRINT_STYLE("FontName", "宋体");
            LODOP.SET_PRINT_STYLE("FontSize", 10);
            LODOP.SET_PRINT_STYLE("Bold", 0);
            var bl = "<%=UserInfor.UserName %>";
            var bt = "新路城配货物配载交接表";
            if (bl == "DR") { bt = "鼎融物流有限公司货物配载交接表"; }
            else if (bl == "FJ") { bt = "福建新陆程物流货物配载交接表"; }
            else if (bl == "YQ") { bt = "云起物流货物配载交接表"; }
            else if (bl == "ZY") { bt = "众盈物流货物配载交接表"; }
            LODOP.ADD_PRINT_TEXT(10, 230, 700, 30, bt);
            LODOP.SET_PRINT_STYLEA(1, "FontName", "宋体");
            LODOP.SET_PRINT_STYLEA(1, "FontSize", 16);
            LODOP.SET_PRINT_STYLEA(1, "Bold", 1);

            var cp = $('#DepCellPhone').val();
            if (bl == "DR") { cp = "15815898928" } else { if (cp == "") { cp = "13544320020"; } }

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
            LODOP.ADD_PRINT_TEXT(80, 130, 300, 20, $('#TruckNum').combobox('getText'));
            LODOP.ADD_PRINT_TEXT(80, 350, 100, 20, "出发站：");
            LODOP.ADD_PRINT_TEXT(80, 420, 100, 20, $('#ADep').combobox('getValue'));
            LODOP.ADD_PRINT_TEXT(80, 510, 100, 20, "到达站：");
            LODOP.ADD_PRINT_TEXT(80, 580, 300, 20, $('#ADest').combobox('getValue'));
            LODOP.ADD_PRINT_TEXT(100, 40, 100, 20, "联系方式：");
            LODOP.ADD_PRINT_TEXT(100, 130, 300, 20, $('#DriverCellPhone').textbox('getValue'));
            LODOP.ADD_PRINT_TEXT(100, 350, 100, 20, "联系人：");
            LODOP.ADD_PRINT_TEXT(100, 420, 300, 20, $('#DestPeople').combobox('getText'));
            LODOP.ADD_PRINT_TEXT(120, 40, 100, 20, "身份号码：");
            LODOP.ADD_PRINT_TEXT(120, 130, 300, 20, $('#DriverIDNum').textbox('getValue'));
            LODOP.ADD_PRINT_TEXT(120, 350, 100, 20, "详细地址：");
            LODOP.ADD_PRINT_TEXT(120, 420, 300, 20, $('#UnLoadAddress').textbox('getValue'));
            LODOP.ADD_PRINT_TEXT(140, 40, 100, 20, "身份地址：");
            LODOP.ADD_PRINT_TEXT(140, 130, 300, 20, $('#DriverIDAddress').textbox('getValue'));
            LODOP.ADD_PRINT_TEXT(140, 350, 100, 20, "联系方式：");
            LODOP.ADD_PRINT_TEXT(140, 420, 300, 20, $('#DestCellphone').textbox('getValue'));

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
    </script>

</asp:Content>

