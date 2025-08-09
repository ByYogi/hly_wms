<%@ Page Title="仓库货位管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="housePositionManager.aspx.cs" Inherits="Cargo.House.housePositionManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                idField: 'ContainerID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                  { title: '', field: 'ContainerID', checkbox: true, width: '2%' },
                  {
                      title: '货位代码', field: 'ContainerCode', width: '10%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '类型', field: 'ContainerType', width: '5%',
                      formatter: function (value) { if (value == "RCK") { return "<span title='货架'>货架</span>"; } else if (value == "ULD") { return "<span title='板'>板</span>"; } else if (value == "CAR") { return "<span title='拖车'>拖车</span>"; } else if (value == "SHL") { return "<span title='空地'>空地</span>"; } else if (value == "VIR") { return "<span title='虚拟'>虚拟</span>"; } else { return ""; } }
                  },
                  {
                      title: '区域大仓', field: 'HouseName', width: '6%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '所在仓库', field: 'FirstAreaName', width: '6%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '一级区域', field: 'ParentName', width: '5%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '二级区域', field: 'AreaName', width: '5%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '预警件数', field: 'WarnPiece', width: '5%', align: 'right', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '最大件数', field: 'MaxPiece', width: '5%', align: 'right', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '最大重量', field: 'MaxWeight', width: '5%', align: 'right', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '最大体积', field: 'MaxVolume', width: '5%', align: 'right', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '优先级', field: 'EmOrder', width: '15%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '备注', field: 'Remark', width: '11%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  { title: '操作时间', field: 'OP_DATE', width: '10%', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });

            //所在仓库
            $('#HID').combobox({
                url: 'houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#PID').combobox('clear');
                    var url = 'houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    $('#PID').combobox('reload', url);
                    //一级仓库
                    $('#PID').combobox({
                        onSelect: function (fai) {
                            $('#SID').combobox('clear');
                            var url = 'houseApi.aspx?method=QueryALLArea&hid=' + rec.HouseID + '&pid=' + fai.AreaID;
                            $('#SID').combobox('reload', url);
                        }
                    });
                }
            });
            //所在仓库
            $('#HouseID').combobox({
                url: 'houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#FirstAreaID').combobox('clear');
                    var url = 'houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    $('#FirstAreaID').combobox('reload', url);
                    //一级货位
                    $('#FirstAreaID').combobox({
                        onSelect: function (fai) {
                            $('#SecondAreaID').combobox('clear');
                            var url = 'houseApi.aspx?method=QueryALLArea&hid=' + rec.HouseID + '&pid=' + fai.AreaID;
                            $('#SecondAreaID').combobox('reload', url);
                            //一级货位
                            $('#SecondAreaID').combobox({
                                onSelect: function (fsai) {
                                    $('#AAreaCode').val(fai.Code);
                                    $('#AreaID').combobox('clear');
                                    var url = 'houseApi.aspx?method=QueryALLArea&hid=' + rec.HouseID + '&pid=' + fsai.AreaID;
                                    $('#AreaID').combobox('reload', url);
                                }
                            });
                        }
                    });
                }
            });
            //一级产品
            $('#ParentID').combobox({
                url: '../Product/productApi.aspx?method=QueryALLOneProductType', valueField: 'TypeID', textField: 'TypeName',
                onSelect: function (rec) {
                    $('#TypeID').combobox('clear');
                    var url = '../Product/productApi.aspx?method=QueryALLOneProductType&PID=' + rec.TypeID;
                    $('#TypeID').combobox('reload', url);
                    //一级货位
                    $('#TypeID').combobox({
                        onSelect: function (fai) {
                            var res = "";
                            var typeRes = $('#TypeID').combobox('getText');//选择的结果进行拆分，按，逗号拆分
                            var typeArr = new Array();
                            typeArr = typeRes.split(',');
                            for (i = 0; i < typeArr.length ; i++) {
                                if (i == 5) { break; }
                                if (i == 0) {
                                    res += "<div><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png />" + typeArr[i] + "</div>";
                                }
                                if (i == 1) {
                                    res += "<div><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png />" + typeArr[i] + "</div>";
                                }
                                if (i == 2) {
                                    res += "<div><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png />" + typeArr[i] + "</div>";
                                }
                                if (i == 3) {
                                    res += "<div><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png />" + typeArr[i] + "</div>";
                                }
                                if (i == 4) {
                                    res += "<div><img src=../CSS/image/color_star.png />" + typeArr[i] + "</div>";
                                }
                            }
                            $('#toal').html(res);
                        },
                        onUnselect: function (ou) {
                            var res = "";
                            var typeRes = $('#TypeID').combobox('getText');//选择的结果进行拆分，按，逗号拆分
                            var typeArr = new Array();
                            typeArr = typeRes.split(',');
                            for (i = 0; i < typeArr.length ; i++) {
                                if (i == 5) { break; }
                                if (i == 0) {
                                    res += "<div><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png />" + typeArr[i] + "</div>";
                                }
                                if (i == 1) {
                                    res += "<div><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png />" + typeArr[i] + "</div>";
                                }
                                if (i == 2) {
                                    res += "<div><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png />" + typeArr[i] + "</div>";
                                }
                                if (i == 3) {
                                    res += "<div><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png />" + typeArr[i] + "</div>";
                                }
                                if (i == 4) {
                                    res += "<div><img src=../CSS/image/color_star.png />" + typeArr[i] + "</div>";
                                }
                            }
                            $('#toal').html(res);
                        }
                    });
                }
            });
            $('#CType').combobox('textbox').bind('focus', function () { $('#CType').combobox('showPanel'); });
            $('#HID').combobox('textbox').bind('focus', function () { $('#HID').combobox('showPanel'); });
            $('#PID').combobox('textbox').bind('focus', function () { $('#PID').combobox('showPanel'); });
            $('#HID').combobox('setValue', '<%=UserInfor.HouseID%>');

        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'houseApi.aspx?method=QueryCargoContainer';
            $('#dg').datagrid('load', {
                ContainerCode: $('#CCode').val(),
                ContainerType: $("#CType").combobox('getValue'),
                FirstID: $("#PID").combobox('getValue'),//父ID
                HID: $("#HID").combobox('getValue'),
                SID: $("#SID").combobox('getValue')
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
                <td style="text-align: right;">货位代码:
                </td>
                <td style="width: 10%">
                    <input id="CCode" class="easyui-textbox" data-options="prompt:'请输入货位代码'" style="width: 100%">
                </td>
                <td style="text-align: right;">货位类型:
                </td>
                <td style="width: 10%">
                    <input class="easyui-combobox" id="CType" data-options="url:'../Data/ContainerType.json',method:'get',valueField:'id',textField:'text',panelHeight:'auto'"
                        style="width: 100%;">
                </td>
                <td style="text-align: right;">区域大仓:
                </td>
                <td style="width: 10%">
                    <input id="HID" class="easyui-combobox" style="width: 100%;" />
                </td>
                <td style="text-align: right;">所属仓库:
                </td>
                <td style="width: 10%">
                    <input id="PID" class="easyui-combobox" style="width: 100%;" data-options="valueField:'AreaID',textField:'Name'"
                        panelheight="auto" />
                </td>
                <td style="text-align: right;">一级区域:
                </td>
                <td style="width: 10%">
                    <input id="SID" class="easyui-combobox" style="width: 100%;" data-options="valueField:'AreaID',textField:'Name'"
                        panelheight="auto" />
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">
            &nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-edit"
                plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
                    iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
                        iconcls="icon-plugin" plain="false" onclick="MakeQR()">&nbsp;打印货位二维码&nbsp;</a>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 600px; height: 400px; padding: 1px 1px"
        closed="true" buttons="#dlg-buttons">
        <div id="saPanel">
            <form id="fm" class="easyui-form" method="post">
                <input type="hidden" name="ContainerID" id="ContainerID" />
                <!--二级货位代码-->
                <input type="hidden" id="ACode" />
                <input type="hidden" id="AAreaCode" />
                <!--货位号-->
                <input type="hidden" id="ContainerNum" name="ContainerNum" />
                <table>
                    <tr>
                        <td style="text-align: right;">货位代码:
                        </td>
                        <td>
                            <input name="ContainerCode" id="ContainerCode" class="easyui-textbox" readonly="true" style="width: 120px;">
                        </td>
                        <td style="text-align: right;">货位类型:
                        </td>
                        <td>
                            <input class="easyui-combobox" id="ContainerType" name="ContainerType" data-options="url:'../Data/ContainerType.json',method:'get',valueField:'id',textField:'text',panelHeight:'auto',required:true,onSelect:fixCCode"
                                style="width: 120px;">
                        </td>
                        <td style="text-align: right;">区域大仓:
                        </td>
                        <td>
                            <input id="HouseID" name="HouseID" class="easyui-combobox" style="width: 120px;" data-options="required:true" panelheight="auto" />
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right;">最大件数:
                        </td>
                        <td>
                            <input name="MaxPiece" id="MaxPiece" class="easyui-numberbox" data-options="min:0,precision:0,required:true"
                                style="width: 120px;" />
                        </td>
                        <td style="text-align: right;">最大重量:
                        </td>
                        <td>
                            <input id="MaxWeight" name="MaxWeight" class="easyui-numberbox" data-options="min:0,precision:2,required:true"
                                style="width: 120px;" />
                        </td>
                        <td style="text-align: right;">所属仓库:
                        </td>
                        <td>
                            <input id="FirstAreaID" name="FirstAreaID" class="easyui-combobox" style="width: 120px;" data-options="valueField:'AreaID',textField:'Name',required:true"
                                panelheight="auto" />
                        </td>

                    </tr>
                    <tr>
                        <td style="text-align: right;">预警件数:
                        </td>
                        <td>
                            <input name="WarnPiece" id="WarnPiece" class="easyui-numberbox" data-options="min:0,precision:0"
                                style="width: 120px;" />
                        </td>
                        <td style="text-align: right;">最大体积:
                        </td>
                        <td>
                            <input id="MaxVolume" name="MaxVolume" class="easyui-numberbox" data-options="min:0,precision:2,required:true"
                                style="width: 120px;" />
                        </td>
                        <td style="text-align: right;">一级区域:
                        </td>
                        <td>
                            <input id="SecondAreaID" name="SecondAreaID" class="easyui-combobox" style="width: 120px;" data-options="valueField:'AreaID',textField:'Name',required:true"
                                panelheight="auto" />
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td style="text-align: right;">二级区域:
                        </td>
                        <td>
                            <input id="AreaID" name="AreaID" class="easyui-combobox" style="width: 120px;" data-options="valueField:'AreaID',textField:'Name',required:true,onSelect:fixContainerCode"
                                panelheight="auto" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">备注:
                        </td>
                        <td colspan="5">
                            <textarea id="Remark" rows="3" cols="5" name="Remark" style="width: 500px;"></textarea>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">适用产品:
                        </td>
                        <td colspan="5">
                            <input id="ParentID" name="ParentID" class="easyui-combobox" style="width: 150px;"
                                panelheight="auto" />

                            <input id="TypeID" name="TypeID" class="easyui-combobox" style="width: 300px;" data-options="valueField:'TypeID',textField:'TypeName',multiple:true"
                                panelheight="auto" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">优先顺序:</td>
                        <td colspan="5">
                            <div id="toal"></div>
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">保存</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">取消</a>
    </div>
    <div id="dlgQRCode" class="easyui-dialog" style="width: 700px; height: 500px; padding: 10px 10px"
        closed="true" buttons="#dlgQRCode-buttons">
        <img id="qrc" src="" width="300px" height="300px" />
        <br />
        <label id="contaiCode" style="font-size: 30px;"></label>
    </div>
    <div id="dlgQRCode-buttons">

        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgQRCode').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <object id="LODOP_OB" title="dd" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA"
        width="0px" height="0px">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0px" height="0px"></embed>
    </object>
    <script type="text/javascript">
        //打印货位二维码
        function MakeQR() {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            <%--            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要操作的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlgQRCode').dialog('open').dialog('setTitle', '打印货位号：' + row.ContainerCode + ' 二维码：');
                $('#qrc').attr('src', "houseApi.aspx?method=MakeQRCode&Code=" + row.ContainerCode);
                $('#contaiCode').html(row.ContainerCode);
            }--%>

            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要打印的数据！', 'warning');
                return;
            }
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            LODOP.PRINT_INITA(0, 25, 850, 500, "打印标签");
            LODOP.SET_PRINT_PAGESIZE(0, 850, 500, "打印标签");

            for (var i = 0; i < rows.length; i++) {
                CreateOnePage(rows[i].ContainerCode)
            }
            LODOP.SET_PREVIEW_WINDOW(0, 0, 0, 0, 0, "");

            var printNum = LODOP.GET_PRINTER_COUNT();
            if (printNum <= 0) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '电脑未安装打印机！', 'warning');
                return;
            }
            var ise = 0;
            //Gprinter  GP-3120TL
            for (var i = 0; i < printNum - 1; i++) {
                var pName = LODOP.Printers.list[i].DriverName;
                if (pName.indexOf("Gprinter") != -1) {
                    LODOP.SET_PRINTER_INDEX(i);
                    ise = 1;
                    break;
                }
            }
            if (ise == 0) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '电脑未安装标签打印机！', 'warning');
                return;
            }

            LODOP.PRINT();
            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '打印成功！', 'info');
            //LODOP.PRINT_DESIGN();
            //LODOP.PREVIEW();
        }
        function CreateOnePage(ContainerCode) {
            LODOP.NewPage();
            LODOP.ADD_PRINT_BARCODE(32, 42, 238, 122, "128A", ContainerCode);
        }

        //组装货位代码
        function fixCCode() {
            var ty = $("#ContainerType").combobox('getValue');
            var fArea = $("#AAreaCode").val();//一级区域代码
            var code = $("#ACode").val();//二级区域代码
            var area = $('#AreaID').combobox('getValue');
            var cnum;
            $.ajax({
                url: "houseApi.aspx?method=ReturnContainerNum&ContainerType=" + ty + "&area=" + area,
                type: "post", async: false,
                success: function (text) {
                    cnum = text;
                }
            });
            $('#ContainerNum').val(cnum);
            //$('#ContainerCode').textbox('setValue', fArea + code + cnum + ty);
            $('#ContainerCode').textbox('setValue', code + cnum + ty);
        }
        //组装货位代码
        function fixContainerCode(item) {
            if (item) {
                $('#ContainerType').combobox('clear');
                $('#ContainerCode').textbox('clear');
                //var ty = $("#ContainerType").combobox('getValue');
                //var cnum = $("#ContainerNum").val();
                //$('#ContainerCode').textbox('setValue', item.Code + cnum + ty);
                $('#ACode').val(item.Code);
            }
        }

        //新增货位信息
        function addItem() {
            $('#dlg').dialog('open').dialog('setTitle', '新增货位信息');
            $('#fm').form('clear');
            $('#toal').html('');
            $('#HouseID').textbox('textbox').focus();
            $('#ContainerType').combobox('textbox').bind('focus', function () { $('#ContainerType').combobox('showPanel'); });
            $('#HouseID').combobox('textbox').bind('focus', function () { $('#HouseID').combobox('showPanel'); });
            $('#FirstAreaID').combobox('textbox').bind('focus', function () { $('#FirstAreaID').combobox('showPanel'); });
            $('#AreaID').combobox('textbox').bind('focus', function () { $('#AreaID').combobox('showPanel'); });
            $('#MaxPiece').numberbox('setValue', 0);
            $('#MaxWeight').numberbox('setValue', 0);
            $('#WarnPiece').numberbox('setValue', 0);
            $('#MaxVolume').numberbox('setValue', 0);
        }
        //修改货位信息
        function editItem() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                $('#toal').html('');
                $('#fm').form('clear');
                $('#dlg').dialog('open').dialog('setTitle', '修改货位信息');

                $('#FirstAreaID').combobox('clear');
                var url = 'houseApi.aspx?method=QueryALLArea&pid=0&hid=' + row.HouseID;
                $('#FirstAreaID').combobox('reload', url);


                $('#SecondAreaID').combobox('clear');
                var url = 'houseApi.aspx?method=QueryALLArea&hid=' + row.HouseID + '&pid=' + row.FirstAreaID;
                $('#SecondAreaID').combobox('reload', url);

                $('#AreaID').combobox('clear');
                var url = 'houseApi.aspx?method=QueryALLArea&hid=' + row.HouseID + '&pid=' + row.ParentID;
                $('#AreaID').combobox('reload', url);

                $('#TypeID').combobox('clear');
                var url = '../Product/productApi.aspx?method=QueryALLOneProductType&PID=1';
                $('#TypeID').combobox('reload', url);

                $('#ContainerID').val(row.ContainerID);
                $('#ContainerNum').val(row.ContainerNum);
                $('#ContainerCode').textbox('setValue', row.ContainerCode);
                $('#ContainerType').combobox('setValue', row.ContainerType);
                $('#MaxPiece').numberbox('setValue', row.MaxPiece);
                $('#MaxWeight').numberbox('setValue', row.MaxWeight);
                $('#WarnPiece').numberbox('setValue', row.WarnPiece);
                $('#MaxVolume').numberbox('setValue', row.MaxVolume);
                $('#Remark').val(row.Remark);
                $('#FirstAreaID').combobox('setValue', row.FirstAreaID);
                $('#SecondAreaID').combobox('setValue', row.ParentID);
                $('#AreaID').combobox('setValue', row.AreaID);
                $('#HouseID').combobox('setValue', row.HouseID);
                $('#TypeID').combobox('setValues', row.EmOrderID);
                $('#HouseID').combobox('textbox').bind('focus', function () { $('#HouseID').combobox('showPanel'); });
                $('#FirstAreaID').combobox('textbox').bind('focus', function () { $('#FirstAreaID').combobox('showPanel'); });
                $('#AreaID').combobox('textbox').bind('focus', function () { $('#AreaID').combobox('showPanel'); });
                $('#ContainerType').combobox('textbox').bind('focus', function () { $('#ContainerType').combobox('showPanel'); });
                if (row.EmOrder != "") {

                    var res = "";
                    var typeArr = new Array();
                    typeArr = row.EmOrder.split(',');
                    for (i = 0; i < typeArr.length ; i++) {
                        if (i == 5) { break; }
                        if (i == 0) {
                            res += "<div><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png />" + typeArr[i] + "</div>";
                        }
                        if (i == 1) {
                            res += "<div><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png />" + typeArr[i] + "</div>";
                        }
                        if (i == 2) {
                            res += "<div><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png />" + typeArr[i] + "</div>";
                        }
                        if (i == 3) {
                            res += "<div><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png />" + typeArr[i] + "</div>";
                        }
                        if (i == 4) {
                            res += "<div><img src=../CSS/image/color_star.png />" + typeArr[i] + "</div>";
                        }
                    }
                    $('#toal').html(res);
                }
            }
        }
        //修改货位信息
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                $('#toal').html('');
                $('#fm').form('clear');
                $('#dlg').dialog('open').dialog('setTitle', '修改货位信息');

                $('#FirstAreaID').combobox('clear');
                var url = 'houseApi.aspx?method=QueryALLArea&pid=0&hid=' + row.HouseID;
                $('#FirstAreaID').combobox('reload', url);


                $('#SecondAreaID').combobox('clear');
                var url = 'houseApi.aspx?method=QueryALLArea&hid=' + row.HouseID + '&pid=' + row.FirstAreaID;
                $('#SecondAreaID').combobox('reload', url);

                $('#AreaID').combobox('clear');
                var url = 'houseApi.aspx?method=QueryALLArea&hid=' + row.HouseID + '&pid=' + row.ParentID;
                $('#AreaID').combobox('reload', url);
                //$('#fm').form('load', row);

                $('#TypeID').combobox('clear');
                var url = '../Product/productApi.aspx?method=QueryALLOneProductType&PID=1';
                $('#TypeID').combobox('reload', url);

                $('#ContainerID').val(row.ContainerID);
                $('#ContainerNum').val(row.ContainerNum);
                $('#ContainerCode').textbox('setValue', row.ContainerCode);
                $('#ContainerType').combobox('setValue', row.ContainerType);
                $('#MaxPiece').numberbox('setValue', row.MaxPiece);
                $('#MaxWeight').numberbox('setValue', row.MaxWeight);
                $('#WarnPiece').numberbox('setValue', row.WarnPiece);
                $('#MaxVolume').numberbox('setValue', row.MaxVolume);
                $('#Remark').val(row.Remark);
                $('#FirstAreaID').combobox('setValue', row.FirstAreaID);
                $('#SecondAreaID').combobox('setValue', row.ParentID);
                $('#AreaID').combobox('setValue', row.AreaID);
                $('#HouseID').combobox('setValue', row.HouseID);
                $('#TypeID').combobox('setValues', row.EmOrderID);
                $('#HouseID').combobox('textbox').bind('focus', function () { $('#HouseID').combobox('showPanel'); });
                $('#FirstAreaID').combobox('textbox').bind('focus', function () { $('#FirstAreaID').combobox('showPanel'); });
                $('#AreaID').combobox('textbox').bind('focus', function () { $('#AreaID').combobox('showPanel'); });
                $('#ContainerType').combobox('textbox').bind('focus', function () { $('#ContainerType').combobox('showPanel'); });

                if (row.EmOrder != "") {

                    var res = "";
                    var typeArr = new Array();
                    typeArr = row.EmOrder.split(',');
                    for (i = 0; i < typeArr.length ; i++) {
                        if (i == 5) { break; }
                        if (i == 0) {
                            res += "<div><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png />" + typeArr[i] + "</div>";
                        }
                        if (i == 1) {
                            res += "<div><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png />" + typeArr[i] + "</div>";
                        }
                        if (i == 2) {
                            res += "<div><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png />" + typeArr[i] + "</div>";
                        }
                        if (i == 3) {
                            res += "<div><img src=../CSS/image/color_star.png /><img src=../CSS/image/color_star.png />" + typeArr[i] + "</div>";
                        }
                        if (i == 4) {
                            res += "<div><img src=../CSS/image/color_star.png />" + typeArr[i] + "</div>";
                        }
                    }
                    $('#toal').html(res);
                }

            }
        }
        //删除货位信息
        function DelItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'houseApi.aspx?method=DelCargoContainer',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
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

        //保存货位信息
        function saveItem() {
            $('#fm').form('submit', {
                url: 'houseApi.aspx?method=SaveCargoContainer',
                onSubmit: function () {
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $('#dlg').dialog('close'); 	// close the dialog
                        dosearch();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
    </script>

</asp:Content>
