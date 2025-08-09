<%@ Page Title="开销售单" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="addSaleOrder.aspx.cs" Inherits="Cargo.Order.addSaleOrder" %>

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
            $.ajaxSetup({ async: true });
            $.getJSON("../Product/productApi.aspx?method=QueryAllProductSource", function (data) {
                ProductSource = data;
            })
        }
        $(window).resize(function () {
            adjustment();
        });
        function adjustment() {
            //var height = parseInt((Number($(window).height()) - 100) / 2);
            var height = parseInt((Number($(window).height()) - Number($("div[name='SelectDiv1']").outerHeight(true))) / 2);
            $('#dg').datagrid({ height: height - 60 });
            $('#outDg').datagrid({ height: (Number($(window).height()) - 90) - height });
        }
        $(document).ready(function () {
            var userID = "<%=Ln%>";
            var columns = [];
            columns.push({ title: '', field: 'ID', checkbox: true, width: '10%' });
            columns.push({
                title: '产品品牌', field: 'TypeName', width: '6%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '规格', field: 'Specs', width: '7%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '花纹', field: 'Figure', width: '8%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '载速', field: 'LoadIndex', width: '5%', align: 'right', formatter: function (value, row) {
                    return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                }
            });
            //columns.push({
            //    title: '速度', field: 'SpeedLevel', width: '3%', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});
            columns.push({
                title: '批次', field: 'Batch', width: '5%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            //columns.push({
            //    title: '型号', field: 'Model', width: '7%', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});
            columns.push({
                title: '在库数量', field: 'Piece', width: '5%', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '销售价', field: 'SalePrice', width: '4%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '门店价', field: 'TradePrice', width: '4%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
          
            //if (userID == "2421" || userID == "2422" || userID == "2076") {
               
            //    columns.push({
            //        title: '成本价', field: 'CostPrice', width: '4%', align: 'right', formatter: function (value) {
            //            return "<span title='" + value + "'>" + value + "</span>";
            //        }
            //    });
            //}
            columns.push({
                title: '上一销售价', field: 'SalePriceClient', width: '5%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '销售单号', field: 'OrderNo', width: '8%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
           
            columns.push({
                title: '进货价', field: 'UnitPrice', width: '4%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({ title: '入库时间', field: 'InHouseTime', width: '130px', formatter: DateTimeFormatter });
            columns.push({
                title: '货位代码', field: 'ContainerCode', width: '8%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在区域', field: 'AreaName', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在仓库', field: 'FirstAreaName', width: '8%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '供应商', field: 'Supplier', width: '8%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            $('#dg').datagrid({
                width: '100%',
                //height: '50%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [columns],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { plOutCargo(); }
            });
            columns = [];
            columns.push({ title: '', field: 'ID', checkbox: true, width: '10%' });
            columns.push({
                title: '产品品牌', field: 'TypeName', width: '6%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '规格', field: 'Specs', width: '7%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '花纹', field: 'Figure', width: '8%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '载速', field: 'LoadIndex', width: '5%', align: 'right', formatter: function (value, row) {
                    return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                }
            });
            //columns.push({
            //    title: '速度', field: 'SpeedLevel', width: '3%', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});
            columns.push({
                title: '批次', field: 'Batch', width: '5%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            //columns.push({
            //    title: '型号', field: 'Model', width: '7%', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});
            columns.push({
                title: '出库数量', field: 'Piece', width: '5%', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '销售价', field: 'ActSalePrice', width: '5%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            if (userID == "2421" || userID == "2422" || userID == "2076") {
                columns.push({
                    title: '进货价', field: 'UnitPrice', width: '4%', align: 'right', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '成本价', field: 'CostPrice', width: '5%', align: 'right', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '供应商', field: 'Supplier', width: '8%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            }

            columns.push({
                title: '货位代码', field: 'ContainerCode', width: '8%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在区域', field: 'AreaName', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '所在仓库', field: 'FirstAreaName', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({ title: '入库时间', field: 'InHouseTime', width: '130px', formatter: DateTimeFormatter });
            //出库列表
            $('#outDg').datagrid({
                width: '100%',
                //height: '38%',
                title: '出库产品', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '',
                columns: [columns],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { dblClickDelCargo(index); }
            });
            //一级产品
            $('#APID').combobox({
                url: '../Product/productApi.aspx?method=QueryALLOneProductType', valueField: 'TypeID', textField: 'TypeName',
                onSelect: function (rec) {
                    $('#ASID').combobox('clear');
                    $('#ASID').combobox({
                        url: '../Product/productApi.aspx?method=QueryALLOneProductType&PID=' + rec.TypeID, valueField: 'TypeID', textField: 'TypeName', AddField: 'PinyinName',
                        filter: function (q, row) {
                            var opts = $(this).combobox('options');
                            return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                        },
                    });
                }
            });
            $('#APID').combobox('setValue', '1');
            $('#ASID').combobox('clear');
            $('#ASID').combobox({
                url: '../Product/productApi.aspx?method=QueryALLOneProductType&PID=1', valueField: 'TypeID', textField: 'TypeName', AddField: 'PinyinName',
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                },
            });
            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#HAID').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    $('#HAID').combobox('reload', url);
                }
            });
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
            $('#HAID').combobox('clear');
            var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=<%=UserInfor.HouseID%>';
            //$('#HAID').combobox('reload', url);
            $('#HAID').combobox({
                'url': url, onLoadSuccess: function () {
                    //默认选中第一个
                    var array = $(this).combobox("getData");
                    for (var item in array[0]) {
                        if (item == "AreaID") {
                            $(this).combobox('select', array[0][item]);
                        }
                    }
                }
            });

            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            $('#HAID').combobox('textbox').bind('focus', function () { $('#HAID').combobox('showPanel'); });
            //客户姓名
            $('#SAcceptPeople').combobox({
                //$('#AcceptPeople').combobox({
                valueField: 'ClientNum', textField: 'Boss', AddField: 'PinyinName', delay: '10',
                url: '../Client/clientApi.aspx?method=AutoCompleteClient',
                onSelect: onClientChanged,
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                },
                required: true
            });
            $('#ADep').textbox('setValue', '<%= UserInfor.DepCity%>');
            $('#ADest').combobox('textbox').bind('focus', function () { $('#ADest').combobox('showPanel'); });
            $('#APID').combobox('textbox').bind('focus', function () { $('#APID').combobox('showPanel'); });
            $('#ASID').combobox('textbox').bind('focus', function () { $('#ASID').combobox('showPanel'); });
            $('#CheckOutType').combobox('textbox').bind('focus', function () { $('#CheckOutType').combobox('showPanel'); });
            $('#SaleManID').combobox('textbox').bind('focus', function () { $('#SaleManID').combobox('showPanel'); });

            $('#SAcceptPeople').combobox('textbox').bind('focus', function () { $('#SAcceptPeople').combobox('showPanel'); });
            $('#AcceptPeople').combobox('textbox').bind('focus', function () { $('#AcceptPeople').combobox('showPanel'); });
            $('#ASpecs').textbox('textbox').keydown(function (e) {
                if (e.keyCode == 13) {
                    dosearch();
                }
            });
            $('#AFigure').textbox('textbox').keydown(function (e) {
                if (e.keyCode == 13) {
                    dosearch();
                }
            });
        });

        //查询
        function dosearch() {
            <%--if ($("#SAcceptPeople").combobox('getValue') == undefined || $("#SAcceptPeople").combobox('getValue') == '') {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择收货人！', 'warning');
                return;
            }--%>
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../House/houseApi.aspx?method=QueryALLHouseData';
            $('#dg').datagrid('load', {
                Specs: $('#ASpecs').val(),
                Figure: $('#AFigure').val(),
                //Model: $('#AModel').val(),
                //GoodsCode: $('#AGoodsCode').val(),
                PID: $("#APID").combobox('getValue'),//一级产品
                SID: $("#ASID").combobox('getValue'),//二级产品
                //TreadWidth: $('#ATreadWidth').val(),
                //ProductName: $('#AProductName').val(),
                //FlatRatio: $('#AFlatRatio').val(),
                // BatchYear: $('#ABatchYear').combobox('getValue'),
                //HubDiameter: $('#AHubDiameter').val(),
                //LoadIndex: $('#ALoadIndex').val(),
                //Company: $("#ACompany").combobox('getValue'),
                HAID: $("#HAID").combobox('getValue'),
                //BelongDepart: $("#ABelongDepart").combobox('getValue'),
                // SpeedLevel: $("#ASpeedLevel").combobox('getValue'),
                ClientNum: $('#ClientNum').val(),
                //ADID: $("#AcceptPeople").combobox('getValue'),
                HouseID: $("#AHouseID").combobox('getValue')//仓库ID
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
                <td style="text-align: right;">客户:</td>
                <td>
                    <input id="SAcceptPeople" style="width: 120px;" class="easyui-combobox AcceptPeople" data-options="valueField:'ClientNum',textField:'Boss',editable:true" /></td>
                <td style="text-align: right;">规格:
                </td>
                <td>
                    <input id="ASpecs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 100px" />
                </td>
                <td style="text-align: right;">一级产品:
                </td>
                <td>
                    <input id="APID" class="easyui-combobox" style="width: 100px;" panelheight="auto" />
                </td>
                <td style="text-align: right;">区域大仓:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;" data-options="required:true" panelheight="auto" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">收货人:</td>
                <td>
                    <input id="AcceptPeople" style="width: 120px;" data-options="required:true" class="easyui-combobox AcceptPeople" /></td>
                <td id="AFigureTd" style="text-align: right;">花纹:
                </td>
                <td>
                    <input id="AFigure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 100px" />
                </td>
                <td style="text-align: right;">二级产品:
                </td>
                <td>
                    <input id="ASID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'TypeID',textField:'TypeName'" />
                </td>


                <td style="text-align: right;">所在仓库:
                </td>
                <td>
                    <input id="HAID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'AreaID',textField:'Name',required:true" panelheight="auto" />
                </td>

            </tr>
            <tr>
    <td style="text-align: right;">业务名称:</td>
    <td>
        <input id="ABusinessID" style="width: 120px;" data-options="required:true" class="easyui-combobox AcceptPeople" /></td>
</tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <table id="outDg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>
    </div>
    <!--Begin 出库操作-->

    <div id="dlg" class="easyui-dialog" style="width: 350px; height: 230px; padding: 0px"
        closed="true" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" id="InPiece" />
            <input type="hidden" id="InIndex" />
            <table>
                <tr>
                    <td style="text-align: right;">拉上订单数量：
                    </td>
                    <td>
                        <input name="Numbers" id="Numbers" class="easyui-numberbox" data-options="min:0,precision:0"
                            style="width: 170px;" type="text" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">业务员价格：</td>
                    <td>
                        <input name="ActSalePrice" id="ActSalePrice" class="easyui-numberbox" data-options="min:0,precision:2"
                            style="width: 170px;" />
                    </td>
                </tr>
                <%--  <tr>
                    <td style="text-align: right;">系统销售价：</td>
                    <td>
                        <input id="SystemSalePrice" class="easyui-numberbox" data-options="min:0,precision:2"
                            style="width: 200px;" readonly="readonly">
                    </td>
                </tr>--%>
                <tr>
                    <td colspan="2"><span style="color: #d27a7a; font-weight: bold;">注意：业务员价格低于成本价,保存订单自动提交改价申请审批！</span></td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="outOK()">&nbsp;确&nbsp;定&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <input type="hidden" id="DisplayNum" />
    <input type="hidden" id="DisplayPiece" />
    <!--End 出库操作-->
    <form id="fmDep" class="easyui-form" method="post">
        <input type="hidden" name="SaleManName" id="SaleManName" />
        <input type="hidden" name="SaleCellPhone" id="SaleCellPhone" />
        <input type="hidden" name="ShopCode" id="ShopCode" />
        <input type="hidden" name="ClientNum" id="ClientNum" />
        <input type="hidden" name="BusinessID" id="BusinessID" />
        <input type="hidden" id="ClientType" name="ClientType" />
        <input type="hidden" name="BelongHouse" id="BelongHouse" />
        <input type="hidden" name="HouseID" id="HouseID" />
        <input type="hidden" name="HiddenClientSelectName" id="HiddenClientSelectName" />
        <input type="hidden" name="AAcceptUnit" id="AAcceptUnit" />
        <input type="hidden" name="AAcceptCellphone" id="AAcceptCellphone" />
        <input type="hidden" name="AAcceptTelephone" id="AAcceptTelephone" />
        <input type="hidden" name="AAcceptPeople" id="AAcceptPeople" />

        <div id="saPanel">
            <table style="width: 100%">
                <tr>
                    <td style="text-align: right;">出发站:
                    </td>
                    <td>
                        <input name="Dep" id="ADep" class="easyui-textbox" style="width: 70px" data-options="required:true ">
                    </td>
                    <td style="color: Red; text-align: right;">到达站:
                    </td>
                    <td>
                        <input name="Dest" id="ADest" class="easyui-combobox" style="width: 70px;" data-options="valueField:'CityName',textField:'CityName',url:'../systempage/sysService.aspx?method=QueryAllCity',required:true " />
                    </td>


                    <td style="text-align: right;">总件数:
                    </td>
                    <td>
                        <input name="Piece" id="APiece" class="easyui-numberbox" data-options="min:0,precision:0,required:true,disabled:true"
                            style="width: 80px;" />
                    </td>
                    <td style="text-align: right;">总收入:
                    </td>
                    <td>
                        <input name="TotalCharge" id="TotalCharge" class="easyui-numberbox" data-options="min:0,precision:2,disabled:true"
                            style="width: 100px;" />
                        <input type="hidden" id="hiddenTotalCharge" />
                    </td>
                    <td>
                        <input class="ThrowGood" type="checkbox" name="ThrowGood" id="ThrwG" value="1" />
                        <label class="ThrowGood" for="ThrwG">抛货单</label>
                    </td>
                    <td style="text-align: right;">付款方式:
                    </td>
                    <td>
                        <select class="easyui-combobox" id="CheckOutType" name="CheckOutType" style="width: 80px;" panelheight="auto">
                            <option value="7">现金</option>
                            <option value="0">现付</option>
                            <option value="8">周结</option>
                            <option value="9">半月结</option>
                            <option value="2">月结</option>
                            <option value="1">周期</option>
                            <option value="3">到付</option>
                            <%--<option value="1">周期</option>
                            <option value="4">代收</option>--%>
                        </select>

                    </td>
                    <td style="text-align: right;">业务员:
                    </td>
                    <td>
                        <input name="SaleManID" id="SaleManID" class="easyui-combobox" style="width: 80px;" data-options="url: 'orderApi.aspx?method=QueryUserByDepCode',valueField: 'LoginName',textField: 'UserName', onSelect: onSaleManIDChanged,required:true" />
                    </td>

                </tr>

                <tr>
                    <td style="text-align: right;">收货地址:
                    </td>
                    <td colspan="3">
                        <input name="AcceptAddress" id="AAcceptAddress" data-options="disabled:true" style="width: 300px;" class="easyui-textbox" />
                    </td>
                    <td style="text-align: right;">备注:
                    </td>
                    <td colspan="3">
                        <textarea name="Remark" id="ARemark" placeholder="请输入订单备注" style="width: 300px; resize: none"></textarea>
                    </td>
                    <td>
                        <input type="checkbox" id="IsPrintPrice" name="IsPrintPrice" checked="checked" value="1" />
                        <label for="IsPrintPrice">打印价格</label>
                    </td>
                    <td colspan="2" style="text-align: center;"><a href="#" id="btnPreSave" class="easyui-linkbutton" iconcls="icon-compress"
                        plain="false" onclick="savePreOutCargo()">&nbsp;预&nbsp;览&nbsp;保&nbsp;存</a></td>
                    <td colspan="2" style="text-align: center;"><a href="#" id="btnSave" class="easyui-linkbutton" iconcls="icon-print"
                        plain="false" onclick="saveOutCargo()">&nbsp;打&nbsp;印&nbsp;保&nbsp;存</a></td>
                </tr>
            </table>
        </div>
    </form>
    <object id="LODOP_OB" title="dd" classid="clsid:2105C259-1E0C-4534-8141-A753534CB4CA"
        width="0px" height="0px">
        <embed id="LODOP_EM" type="application/x-print-lodop" width="0px" height="0px"></embed>
    </object>
    <script type="text/javascript">

        //预览保存
        function savePreOutCargo() {
            //取消业务员禁用方便后台取值
            $('#SaleManID').combobox('enable');
            $('#APiece').combobox('enable');
            $('#TotalCharge').combobox('enable');
            $('#AAcceptAddress').combobox('enable');

            var rows = $('#outDg').datagrid('getRows');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '出库列表为空！', 'warning'); return; }

            var saleMaanID = $('#SaleManID').combobox('getValue');
            var saleMaanName = $('#SaleManID').combobox('getText');
            if ((saleMaanID == "" || saleMaanID == undefined) && saleMaanName != "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '此业务员无效！', 'warning'); return;
            }
            else if (saleMaanID == "" || saleMaanID == undefined) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择业务员！', 'warning'); return;
            }

            //$('#HouseCode').val(rows[0].OrderCode);
            $('#BelongHouse').val(rows[0].BelongHouse);
            $('#HouseID').val(rows[0].HouseID);
            //$('#HouseName').val(rows[0].HouseName);
            $.messager.progress({ msg: '请稍后,正在保存中...' });
            $('#btnSave').linkbutton('disable');
            var json = JSON.stringify(rows);

            $('#fmDep').form('submit', {
                url: 'orderApi.aspx?method=saveSimpleOrderData',
                contentType: "application/json;charset=utf-8", dataType: "json",
                onSubmit: function (param) {
                    param.ADest = $('#ADest').combobox('getText');
                    param.submitData = json;
                    var trd = $(this).form('enableValidation').form('validate');
                    if (trd) { $('#btnSave').linkbutton('disable'); } else {
                        $.messager.progress("close");
                        $('#btnSave').linkbutton('enable');
                    }
                    return trd;
                },
                success: function (msg) {
                    $.messager.progress("close");
                    $('#btnSave').linkbutton('enable');
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        var dd = result.Message.split('/');
                        //$('#ONum').val(dd[0]);
                        //$('#OutNum').val(dd[1]);
                        prePrint(dd[0]); //直接打印发货单
                        //LODOP.PRINT_DESIGN();
                        LODOP.PREVIEW();//预览
                        location.reload();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                        //$('#SaleManID').combobox('disable');
                    }
                }
            })
        }
        //保存订单
        function saveOutCargo() {
            //取消业务员禁用方便后台取值
            $('#SaleManID').combobox('enable');
            $('#APiece').combobox('enable');
            $('#TotalCharge').combobox('enable');
            $('#AAcceptAddress').combobox('enable');

            var rows = $('#outDg').datagrid('getRows');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '出库列表为空！', 'warning'); return; }

            var saleMaanID = $('#SaleManID').combobox('getValue');
            var saleMaanName = $('#SaleManID').combobox('getText');
            if ((saleMaanID == "" || saleMaanID == undefined) && saleMaanName != "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '此业务员无效！', 'warning'); return;
            }
            else if (saleMaanID == "" || saleMaanID == undefined) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择业务员！', 'warning'); return;
            }

            //$('#HouseCode').val(rows[0].OrderCode);
            $('#BelongHouse').val(rows[0].BelongHouse);
            $('#HouseID').val(rows[0].HouseID);
            //$('#HouseName').val(rows[0].HouseName);
            $.messager.progress({ msg: '请稍后,正在保存中...' });
            $('#btnSave').linkbutton('disable');
            var json = JSON.stringify(rows);

            $('#fmDep').form('submit', {
                url: 'orderApi.aspx?method=saveSimpleOrderData',
                contentType: "application/json;charset=utf-8", dataType: "json",
                onSubmit: function (param) {
                    param.ADest = $('#ADest').combobox('getText');
                    param.submitData = json;
                    var trd = $(this).form('enableValidation').form('validate');
                    if (trd) { $('#btnSave').linkbutton('disable'); } else {
                        $.messager.progress("close");
                        $('#btnSave').linkbutton('enable');
                    }
                    return trd;
                },
                success: function (msg) {
                    $.messager.progress("close");
                    $('#btnSave').linkbutton('enable');
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        var dd = result.Message.split('/');
                        //$('#ONum').val(dd[0]);
                        //$('#OutNum').val(dd[1]);
                        prePrint(dd[0]); //直接打印发货单
                        LODOP.PRINT();//直接打印
                        location.reload();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                        //$('#SaleManID').combobox('disable');
                    }
                }
            })

        }
        function reset() {
            $('#fmDep').form('clear');
            $('#outDg').datagrid('loadData', { total: 0, rows: [] });
            $('#DisplayNum').val(0);
            $('#DisplayPiece').val(0);
            $('#ADep').textbox('setValue', '<%= UserInfor.DepCity%>');
            var title = "";
            $('#outDg').datagrid("getPanel").panel("setTitle", title);
        }
        //业务员选择方法
        function onSaleManIDChanged(item) {
            if (item) {
                $('#SaleManName').val(item.UserName);
                $('#SaleCellPhone').val(item.CellPhone);
            }
        }

        //删除出库的数据
        function dblClickDelCargo(Did) {
            var row = $("#outDg").datagrid('getData').rows[Did];
            var tCharge = $('#TotalCharge').numberbox('getValue') == null || $('#TotalCharge').numberbox('getValue') == "" ? 0 : Number($('#TotalCharge').numberbox('getValue'));
            var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
            var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
            n--;
            var pt = p - (Number(row.InPiece) - Number(row.Piece));
            $('#DisplayNum').val(Number(n));
            $('#DisplayPiece').val(Number(pt));
            $('#APiece').numberbox('setValue', Number(pt));
            var SalePrice = Number(row.ActSalePrice);//销售价
            var NC = SalePrice * (Number(row.InPiece) - Number(row.Piece));

            $('#TotalCharge').numberbox('setValue', tCharge - NC);

            var title = "上订单     已拉上：" + n + "票，总数量：" + pt + " 条";
            $('#outDg').datagrid("getPanel").panel("setTitle", title);
            var index = $('#dg').datagrid('getRowIndex', row.ID);
            if (index >= 0) {
                var Trow = $("#dg").datagrid('getData').rows[index];
                Trow.Piece = Trow.InPiece;
                $('#dg').datagrid('updateRow', { index: index, row: Trow });
            }
            var index = $('#outDg').datagrid('getRowIndex', row);
            $('#outDg').datagrid('deleteRow', index);
        }
        //新增出库数据
        function outOK() {
            $("#ActSalePrice").numberbox('enable');
            var SalePrice = $('#ActSalePrice').numberbox('getValue');// Number(row.SalePrice);//销售价
            var row = $('#dg').datagrid('getSelected');
            //对轮胎产品进行价格管控
            if (row.TypeParentID == 1) {
                if (SalePrice == "0" || SalePrice == '' || SalePrice == undefined) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请输入业务员销售价格！', 'warning');
                    return;
                }
            }
            if ($('#Numbers').val() == null || $('#Numbers').val() == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请输入拉上订单数量！', 'warning');
                return;
            }
            if ($('#Numbers').val() < 1) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '拉上订单数量必须大于0！', 'warning');
                return;
            }
            if (SalePrice < Number(row.CostPrice)) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '销售价低于成本价会提交领导审批，保存后先不要打印销售单！', 'warning');
            }
            var Total = Number(row.Piece);
            var tCharge = $('#TotalCharge').numberbox('getValue') == null || $('#TotalCharge').numberbox('getValue') == "" ? 0 : Number($('#TotalCharge').numberbox('getValue'));
            var Aindex = $('#InIndex').val();
            var index = $('#outDg').datagrid('getRowIndex', row.ID);
            if (index < 0) {
                row.Piece = $('#Numbers').numberbox('getValue');
                row.ActSalePrice = SalePrice;
                $('#outDg').datagrid('appendRow', row);
                var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
                var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
                n++;
                var pt = p + Number($('#Numbers').numberbox('getValue'));

                $('#DisplayNum').val(Number(n));
                $('#DisplayPiece').val(Number(pt));
                $('#APiece').numberbox('setValue', Number(pt));
                var NC = SalePrice * Number($('#Numbers').numberbox('getValue'));
                $('#TotalCharge').numberbox('setValue', tCharge + NC);
                var title = "上订单     已拉上：" + n + "票，总数：" + pt + " 条";
                $('#outDg').datagrid("getPanel").panel("setTitle", title);
                closedgShowData();

                if (Total > Number($('#Numbers').numberbox('getValue'))) {
                    row.Piece = Total - Number($('#Numbers').numberbox('getValue'));
                } else {
                    row.Piece = 0;
                }
                $('#dg').datagrid('updateRow', { index: Aindex, row: row });
            } else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '订单列表已存在该产品，请先删除再添加！', 'warning'); }
        }
        //关闭
        function closedgShowData() {
            $('#dlg').dialog('close');
        }
        ///出库
        function plOutCargo() {
            $("#ActSalePrice").numberbox('enable');

            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要拉上订单的数据！', 'warning');
                return;
            }
            if (row.Piece == 0) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '在库件数为0', 'warning');
                return;
            }
            if (row.TypeParentID == 1) {
                var sp = "<%=UserInfor.SpecialCreateAwb%>";
                if (sp == '0' || sp == '' || sp == undefined) {
                    //没有特殊下单权限，需要验证先进先出
                    var rows = $('#dg').datagrid('getRows');
                    for (var i = 0; i < rows.length; i++) {
                        var rw = rows[i];
                        /* if (rw.Specs == row.Specs && rw.Figure == row.Figure && rw.Source == row.Source) {*/
                        if (rw.Specs == row.Specs && rw.Figure == row.Figure && rw.LoadIndex == row.LoadIndex && rw.SpeedLevel == row.SpeedLevel) {
                            if (rw.BatchYear < row.BatchYear) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请优先出库周期早的轮胎！', 'warning');
                                return;
                            }
                            if (rw.BatchYear == row.BatchYear) {
                                if (row.BatchWeek > rw.BatchWeek && rw.Piece > 0) {
                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请优先出库周期早的轮胎！', 'warning');
                                    return;
                                }
                            }

                        }
                    }
                }
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '拉上  ' + row.ProductName + ' 型号：' + row.Model + ' 不得超过：' + row.Piece + '件');
                $('#InPiece').val(row.Piece);
                $('#InIndex').val($('#dg').datagrid('getRowIndex', row));
                $('#Numbers').numberbox('setValue', '');

                if (row.SalePriceClient != undefined && row.SalePriceClient != "") {
                    $('#ActSalePrice').numberbox('setValue', row.SalePriceClient);
                } else {
                    $('#ActSalePrice').numberbox('setValue', row.TradePrice);
                }
                //$('#SystemSalePrice').numberbox('setValue', row.SalePrice);

                $('#Numbers').numberbox({
                    min: 0,
                    max: row.Piece,
                    precision: 0
                });
                $('#Numbers').numberbox().next('span').find('input').focus();

            }
        }
        //根据来源ID获取来源名称
        function GetSourceName(value) {
            var name = '';
            for (var i = 0; i < ProductSource.length; i++) {
                if (ProductSource[i].Source == value) {
                    name = ProductSource[i].SourceName;
                    break;
                }
            }
            return name;
        }
        function onAcceptAddressChanged(item) {
            $('#AAcceptUnit').val(item.AcceptCompany);
            $('#AAcceptAddress').textbox('setValue', item.AcceptAddress);
            $('#AAcceptTelephone').val(item.AcceptTelephone);
            $('#AAcceptCellphone').val(item.AcceptCellphone);
            $('#AAcceptPeople').val(item.AcceptPeople);
        }
        //业务编号赋值
        function onAcceptBusinessID(item) {
            $('#BusinessID').val(item.ID);
        }
        //收货人自动选择方法
        function onClientChanged(item) {
            $("#HiddenClientSelectName").val(item.Boss);
            if (item) {
                $('#AcceptPeople').combobox({
                    valueField: 'ADID', textField: 'AcceptPeopleCellphone', delay: '10',
                    url: '../Client/clientApi.aspx?method=AutoCompleteClientAcceptPeople&ClientNum=' + item.ClientNum,
                    onLoadSuccess: function () {
                        //默认选中第一个
                        var array = $(this).combobox("getData");
                        for (var item in array[0]) {
                            if (item == "ADID") {
                                $(this).combobox('select', array[0][item]);
                            }
                        }
                        //$('#ASpecs').next('span').find('input').focus();
                    },
                    onSelect: onAcceptAddressChanged
                });
                $('#ABusinessID').combobox({
                    valueField: 'ID', textField: 'DepName', delay: '10',
                    url: '../Client/clientApi.aspx?method=AutoBusinessID&ClientID=' + item.ClientID,
                    onLoadSuccess: function () {
                        //默认选中第一个
                        var array = $(this).combobox("getData");
                        for (var item in array[0]) {
                            if (item == "ID") {
                                $(this).combobox('select', array[0][item]);
                            }
                        }
                    },
                    onSelect: onAcceptBusinessID
                });
                $('#ClientType').val(item.ClientType);
                $('#CheckOutType').combobox('setValue', item.CheckOutType);
                //$('#HiddenClientNum').val(item.ClientNum);
                if (item.UserID != null && item.UserID != "") {
                    $('#SaleManID').combobox('setValue', item.UserID);
                    if (item.UserName != null && item.UserName != "") {
                        $('#SaleManName').val(item.UserName);
                    }
                } else {
                    $('#SaleManID').combobox('enable');
                    $('#SaleManID').combobox('setValue', '');
                }
                $('#SAcceptPeople').combobox('setValue', item.ClientNum);
                $('#SAcceptPeople').combobox('setText', item.Boss);
                $('#ClientNum').val(item.ClientNum);
                $('#ShopCode').val(item.ShopCode);
                if (item.City != '') {
                    $('#ADest').combobox('setValue', item.City);
                }
            }
        }

        var LODOP;
        //打印发货单 
        function prePrint(OrderNo) {
            LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            var griddata = $('#outDg').datagrid('getRows');
            griddata.sort(function (a, b) {
                var x = a.Specs.toLowerCase();
                var y = b.Specs.toLowerCase();
                if (x < y) { return -1; }
                if (x > y) { return 1; }
                return 0;
            });
            griddata.sort(function (a, b) { return a.TypeID - b.TypeID; });
            var js = 0, Alltotal = 0, AllPiece = 0; p = 1; pie = 0; total = 0;

            Alltotal = Number($('#TotalCharge').numberbox('getValue')).toFixed(2);
            for (var k = 0; k < griddata.length; k++) {
                AllPiece += Number(griddata[k].InPiece - griddata[k].Piece);
            }
            var ALen = griddata.length;
            var ji = 0;
            for (var i = 0; i < ALen; i++) {
                if (i == (p - 1) * 5) {
                    if (p > 1) {
                        ji = 0;
                        LODOP.NewPage();
                    }
                    p++;
                    LODOP.SET_PRINT_PAGESIZE(1, 2200, 935, "");
                    //LODOP.PRINT_INITA(0, 0, 800, 320, "广通公司");
                    var nowdate = new Date();
                    var sendTitle = '<%=SendTitle%>'
                    LODOP.ADD_PRINT_TEXT(6, 290, 302, 28, sendTitle);
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 18);
                    LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                    LODOP.ADD_PRINT_TEXT(34, 28, 84, 20, "收货单位：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    
                    if ($('#SAcceptPeople').combobox('getText') == $('#AAcceptPeople').val()) {
                        LODOP.ADD_PRINT_TEXT(34, 105, 412, 20, $('#AAcceptPeople').val() + " " + $('#AAcceptCellphone').val());//收货单位 收货人 收货电话
                    } else {
                        LODOP.ADD_PRINT_TEXT(34, 105, 412, 20, $('#SAcceptPeople').combobox('getText') + " → " + $('#AAcceptPeople').val() + " " + $('#AAcceptCellphone').val());//收货单位 收货人 收货电话
                    }
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(34, 515, 55, 20, "单号：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(34, 564, 200, 20, OrderNo.slice(0, 4) + "-" + OrderNo.slice(4, 6) + "-" + OrderNo.slice(6, 8) + "-" + OrderNo.slice(8, 11));//订单号
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(55, 28, 84, 20, "送货地址：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(55, 90, 410, 20, $('#AAcceptAddress').val());//收货地址
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(55, 515, 54, 20, "时间：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(55, 564, 153, 20, nowdate.getHours() + ":" + nowdate.getMinutes() + ":" + nowdate.getSeconds());//开单时间
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.ADD_PRINT_TEXT(55, 722, 72, 20, (p - 1) + "/" + (Math.floor((ALen - 1) / 5) + 1));
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);

                    LODOP.ADD_PRINT_RECT(76, 28, 726, 25, 0, 1);

                    LODOP.ADD_PRINT_LINE(101, 60, 76, 61, 0, 1);
                    LODOP.ADD_PRINT_LINE(101, 242, 76, 243, 0, 1);
                    LODOP.ADD_PRINT_LINE(101, 376, 76, 377, 0, 1);
                    LODOP.ADD_PRINT_LINE(101, 464, 76, 465, 0, 1);
                    LODOP.ADD_PRINT_LINE(101, 512, 76, 513, 0, 1);
                    LODOP.ADD_PRINT_LINE(101, 566, 76, 567, 0, 1);

                    LODOP.ADD_PRINT_TEXT(82, 36, 26, 20, "序");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(82, 101, 117, 20, "商品名称");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(82, 290, 70, 20, "花纹");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(82, 392, 55, 20, "级别");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(82, 472, 50, 20, "单位");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(82, 523, 40, 20, "数量");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    var jck = 4 * 28;
                    if ((ALen - js) % 5 == (ALen - js)) {
                        //说明是最后一页
                        jck = (ALen % 5 - 1) * 28;
                    }
                    //判断是否选中打印价格勾选框
                    if ($('#IsPrintPrice').is(':checked')) {
                        //LODOP.ADD_PRINT_RECT(76, 630, 76, 25, 0, 1);
                        LODOP.ADD_PRINT_LINE(101, 631, 76, 632, 0, 1);
                        LODOP.ADD_PRINT_LINE(101, 706, 76, 707, 0, 1);
                        LODOP.ADD_PRINT_TEXT(82, 574, 41, 20, "单价");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(82, 641, 61, 20, "金额");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(82, 711, 44, 20, "批次");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.ADD_PRINT_TEXT(135 + jck, 128, 303, 20, atoc(Alltotal));//总金额大写
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                        //LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                        LODOP.ADD_PRINT_TEXT(135 + jck, 633, 107, 20, Alltotal);//总金额
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                        LODOP.SET_PRINT_STYLEA(0, "Bold", 1);

                    } else {
                        LODOP.ADD_PRINT_TEXT(82, 574, 60, 20, "批次");
                        LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    }

                    LODOP.ADD_PRINT_RECT(129 + jck, 28, 726, 25, 0, 1);
                    //LODOP.ADD_PRINT_RECT(129 + jck, 81, 430, 25, 0, 1);
                    //LODOP.ADD_PRINT_RECT(129 + jck, 566, 188, 25, 0, 1);
                    LODOP.ADD_PRINT_LINE(154 + jck, 81, 129 + jck, 82, 0, 1);
                    LODOP.ADD_PRINT_LINE(154 + jck, 512, 129 + jck, 513, 0, 1);
                    LODOP.ADD_PRINT_LINE(154 + jck, 566, 129 + jck, 567, 0, 1);

                    LODOP.ADD_PRINT_TEXT(135 + jck, 37, 52, 20, "总计：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    //LODOP.SET_PRINT_STYLEA(0, "Bold", 1);

                    LODOP.ADD_PRINT_TEXT(135 + jck, 525, 57, 20, AllPiece);//总数
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                    //判断是否选中打印价格勾选框
                    //if ($('#IsPrintPrice').is(':checked')) {

                    //}

                    //LODOP.ADD_PRINT_RECT(154 + jck, 28, 726, 25, 0, 1);
                    //LODOP.ADD_PRINT_RECT(154 + jck, 81, 430, 25, 0, 1);
                    //LODOP.ADD_PRINT_RECT(154 + jck, 614, 140, 25, 0, 1);
                    //LODOP.ADD_PRINT_LINE(179 + jck, 81, 154 + jck, 82, 0, 1);
                    //LODOP.ADD_PRINT_LINE(179 + jck, 512, 154 + jck, 513, 0, 1);
                    //LODOP.ADD_PRINT_LINE(179 + jck, 614, 154 + jck, 615, 0, 1);

                    LODOP.ADD_PRINT_TEXT(158 + jck, 37, 49, 20, "备注");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(158 + jck, 93, 332, 50, $('#ARemark').val());//备注
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(158 + jck, 530, 83, 20, "付款方式：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(158 + jck, 632, 59, 20, $("#CheckOutType").combobox('getText'));//付款方式
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.ADD_PRINT_TEXT(186 + jck, 530, 80, 20, "未付签收：");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                    LODOP.ADD_PRINT_TEXT(186 + jck, 43, 100, 20, "");
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 10);
                    LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                }
                js++;
                total = (Number(griddata[i].InPiece) - Number(griddata[i].Piece)) * Number(griddata[i].ActSalePrice);

                LODOP.ADD_PRINT_RECT(101 + ji * 28, 28, 726, 28, 0, 1);
                LODOP.ADD_PRINT_LINE(129 + ji * 28, 60, 101 + ji * 28, 61, 0, 1);
                LODOP.ADD_PRINT_LINE(129 + ji * 28, 242, 101 + ji * 28, 243, 0, 1);
                LODOP.ADD_PRINT_LINE(129 + ji * 28, 376, 101 + ji * 28, 377, 0, 1);
                LODOP.ADD_PRINT_LINE(129 + ji * 28, 464, 101 + ji * 28, 465, 0, 1);
                LODOP.ADD_PRINT_LINE(129 + ji * 28, 512, 101 + ji * 28, 513, 0, 1);
                LODOP.ADD_PRINT_LINE(129 + ji * 28, 566, 101 + ji * 28, 567, 0, 1);
                //LODOP.ADD_PRINT_RECT(101 + ji * 28, 60, 182, 28, 0, 1);
                //LODOP.ADD_PRINT_RECT(101 + ji * 28, 376, 87, 28, 0, 1);
                //LODOP.ADD_PRINT_RECT(101 + ji * 28, 511, 55, 28, 0, 1);

                LODOP.ADD_PRINT_TEXT(106 + ji * 28, 36, 26, 20, js);
                //判断是否选中打印价格勾选框
                if ($('#IsPrintPrice').is(':checked')) {
                    // LODOP.ADD_PRINT_RECT(101 + ji * 28, 630, 76, 28, 0, 1);
                    LODOP.ADD_PRINT_LINE(129 + ji * 28, 631, 101 + ji * 28, 632, 0, 1);
                    LODOP.ADD_PRINT_LINE(129 + ji * 28, 706, 101 + ji * 28, 707, 0, 1);
                    LODOP.ADD_PRINT_TEXT(106 + ji * 28, 576, 70, 20, griddata[i].ActSalePrice);//单价
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                    LODOP.ADD_PRINT_TEXT(106 + ji * 28, 643, 55, 20, total);//金额
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    //LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                    LODOP.ADD_PRINT_TEXT(106 + ji * 28, 710, 51, 20, griddata[i].Batch);//批次
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    //LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                } else {
                    LODOP.ADD_PRINT_TEXT(106 + ji * 28, 580, 70, 20, griddata[i].Batch);//批次
                    LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                    //LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                }
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                //LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                LODOP.ADD_PRINT_TEXT(106 + ji * 28, 65, 187, 20, griddata[i].TypeName + " " + griddata[i].Specs);//品牌加规格
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                //LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                LODOP.ADD_PRINT_TEXT(106 + ji * 28, 257, 121, 20, griddata[i].Figure);//花纹
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                // LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                LODOP.ADD_PRINT_TEXT(106 + ji * 28, 380, 77, 20, griddata[i].LoadIndex + griddata[i].SpeedLevel);//载速
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                //LODOP.SET_PRINT_STYLEA(0, "Bold", 1);
                LODOP.ADD_PRINT_TEXT(106 + ji * 28, 472, 49, 20, griddata[i].Package);//条套
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                LODOP.ADD_PRINT_TEXT(106 + ji * 28, 527, 46, 20, Number(griddata[i].InPiece) - Number(griddata[i].Piece));//数量
                LODOP.SET_PRINT_STYLEA(0, "FontSize", 11);
                //LODOP.SET_PRINT_STYLEA(0, "Bold", 1);

                ji++;
            }
        }

    </script>
</asp:Content>
