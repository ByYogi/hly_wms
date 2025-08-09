<%@ Page Title="新增进货单" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddArrivalOrder.aspx.cs" Inherits="Cargo.Order.AddArrivalOrder" %>

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
            //var height = parseInt((Number($(window).height()) - 100) / 2);
            var height = parseInt((Number($(window).height()) - Number($("div[name='SelectDiv1']").outerHeight(true))) / 2);
            $('#dg').datagrid({ height: height - 20 });
            $('#outDg').datagrid({ height: (Number($(window).height()) - 90) - height - 20 });
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
            var columns = [];
            var HID = "<%=UserInfor.HouseID%>";
            columns.push({ title: '', field: 'SID', checkbox: true, width: '10%' });
            columns.push({
                title: '品牌', field: 'TypeName', width: '8%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '货品代码', field: 'GoodsCode', width: '15%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '规格', field: 'Specs', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '型号', field: 'Model', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            //columns.push({ title: '型号', field: 'Model', hidden: true });
            columns.push({
                title: '花纹', field: 'Figure', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            if (HID == "84") {
                columns.push({
                    title: '型号', field: 'Model', width: '10%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '尺寸', field: 'HubDiameter', width: '5%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '捆包数', field: 'BundleNum', width: '5%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            }
            columns.push({
                title: '载重', field: 'LoadIndex', width: '5%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '速度', field: 'SpeedLevel', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });

            //columns.push({
            //    title: '产地', field: 'Born', width: '5%', formatter: function (value) {
            //        if (value == "0") { return "<span title='国产'>国产</span>"; }
            //        else if (value == "1") { return "<span title='进口'>进口</span>"; }
            //    }
            //});
            columns.push({ title: '产地', field: 'Born', hidden: true });
            //columns.push({
            //    title: '类型', field: 'Assort', width: '5%', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});
            columns.push({ title: '类型', field: 'Assort', hidden: true });
            $('#dg').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'SID',
                url: null,
                toolbar: '#toolbar',
                columns: [columns],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { plOutCargo(row); }
            });

            columns = [];
            columns.push({
                title: '', field: 'SID', checkbox: true, width: '3%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '品牌', field: 'TypeName', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '货品代码', field: 'GoodsCode', width: '20%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '规格', field: 'Specs', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            //columns.push({
            //   title: '型号', field: 'Model', width: '10%', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});
            //columns.push({ title: '型号', field: 'Model', hidden: true });
            columns.push({
                title: '花纹', field: 'Figure', width: '10%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            if (HID == "84") {
                columns.push({
                    title: '型号', field: 'Model', width: '10%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '尺寸', field: 'HubDiameter', width: '5%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
                columns.push({
                    title: '捆包数', field: 'BundleNum', width: '5%', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            }
            columns.push({
                title: '载重', field: 'LoadIndex', width: '5%', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '速度', field: 'SpeedLevel', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            //columns.push({
            //    title: '产地', field: 'Born', width: '5%', formatter: function (value) {
            //        if (value == "0") { return "<span title='国产'>国产</span>"; }
            //        else if (value == "1") { return "<span title='进口'>进口</span>"; }
            //    }
            //});

            columns.push({ title: '产地', field: 'Born', hidden: true });
            //columns.push({
            //    title: '类型', field: 'Assort', width: '5%', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});
            columns.push({ title: '类型', field: 'Assort', hidden: true });
            columns.push({
                title: '批次', field: 'Batch', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '数量', field: 'Piece', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '进货价', field: 'UnitPrice', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '进货总价', field: 'AllUnitPrice', width: '5%', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value, row) {
                    return "<span title='" + accMul(row.Piece, row.UnitPrice) + "'>" + accMul(row.Piece, row.UnitPrice) + "</span>";
                }
            });
            columns.push({
                title: '门店价', field: 'TradePrice', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '销售价', field: 'SalePrice', width: '5%', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '是否含税', field: 'WhetherTax', width: '5%', formatter: function (value) {
                    if (value == "0") { return "<span title='不含税'>不含税</span>"; }
                    else if (value == "1") { return "<span title='含税'>含税</span>"; }
                }
            });
            //出库列表
            $('#outDg').datagrid({
                width: '100%',
                //height: '38%',
                title: '出库产品', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'SID',
                url: null,
                toolbar: '',
                columns: [columns],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { dblClickDelCargo(index); }
            });

            ////客户姓名
            //$('#SAcceptPeople').combobox({
            //    valueField: 'ClientNum', textField: 'Boss', delay: '10',
            //    url: '../Client/clientApi.aspx?method=AutoCompleteClient',
            //    onSelect: onClientChanged,
            //    required: true
            //});
            var IsHeadHouse = "<%= UserInfor.IsHeadHouse%>";
            var HeadHouseID = "<%= UserInfor.HeadHouseID%>";
            var UpClientID = "";
            if (IsHeadHouse == "1" && HeadHouseID == "3480") {
                UpClientID = "8";
            }
            $('#SAcceptPeople').combobox({
                //$('#AcceptPeople').combobox({
                valueField: 'ClientNum', textField: 'Boss', AddField: 'PinyinName', 
                url: '../Client/clientApi.aspx?method=AutoCompleteClient&UpClientID=' + UpClientID,
                onSelect: onClientChanged,
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                },
                required: true
            });
            $('#SAcceptPeople').combobox('textbox').bind('focus', function () { $('#SAcceptPeople').combobox('showPanel'); });
            //一级产品
            $('#ASID').combobox({
                url: '../Product/productApi.aspx?method=QueryALLOneProductType&PID=1', valueField: 'TypeID', textField: 'TypeName', AddField: 'PinyinName',
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                },
            });
            $('#DisplayNum').val(0);
            $('#DisplayPiece').val(0);
            var hous = '<%= UserInfor.HouseName%>';
            $('#CreateAwb').textbox('setValue', '<%= UserInfor.UserName%>');
            var d = new Date();
            $('#CreateDate').datetimebox('setValue', AllDateTime(d));
            $('#AOrderType').combobox({
                onSelect: function (e) {
                    if (e.value == "3") {
                        $('#tdA').show(); $('#tdB').show();
                        $('#LOTPID').combobox({ 'required': true });
                    } else {
                        $('#tdA').hide(); $('#tdB').hide();
                        $('#LOTPID').combobox({ 'required': false });
                    }
                }
            });
            if (HID == "84") {
                $('#AOrderType').combobox('setValue', '0');
                $('#tdA').hide(); $('#tdB').hide();
                $('#LOTPID').combobox({ 'required': false });
            }
            else {
                $('#AOrderType').combobox('setValue', '3');
            }
            $('#AOrderType').combobox('textbox').bind('focus', function () { $('#AOrderType').combobox('showPanel'); });

            $('#LOTPID').combobox({
                url: '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=<%=UserInfor.HouseID%>'
            });
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

            $('#ASID').combobox('textbox').bind('focus', function () { $('#ASID').combobox('showPanel'); });

            $("#TransitFee").numberbox({
                "onChange": function (newValue, oldValue) {
                    $('#TotalCharge').numberbox('setValue', TransportFee + Number($('#TransitFee').numberbox('getValue')) + Number($('#HandFee').numberbox('getValue')));
                }
            });
            $("#HandFee").numberbox({
                "onChange": function (newValue, oldValue) {
                    $('#TotalCharge').numberbox('setValue', TransportFee + Number($('#TransitFee').numberbox('getValue')) + Number($('#HandFee').numberbox('getValue')));
                }
            });
        });
        function accMul(arg1, arg2) {
            var m = 0, s1 = arg1.toString(), s2 = arg2.toString();
            try { m += s1.split(".")[1].length } catch (e) { }
            try { m += s2.split(".")[1].length } catch (e) { }
            return Number(s1.replace(".", "")) * Number(s2.replace(".", "")) / Math.pow(10, m)
        }

        //查询
        function dosearch() {
<%--            if ($("#ASpecs").textbox('getValue') == undefined || $("#ASpecs").textbox('getValue') == '') {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请输入规格！', 'warning');
                return;
            }--%>
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../House/houseApi.aspx?method=QueryALLProductData';
            $('#dg').datagrid('load', {
                Specs: $('#ASpecs').val(),
                Figure: $('#AFigure').val(),
                SID: $("#ASID").combobox('getValue'),//二级产品
                GoodsCode: $("#AGoodsCode").val(),//货品代码
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
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table>
            <tr>
                <td style="text-align: right;">品牌:
                </td>
                <td>
                    <input id="ASID" class="easyui-combobox" style="width: 120px;" data-options="valueField:'TypeID',textField:'TypeName'" />
                </td>
                <td style="text-align: right;">规格:
                </td>
                <td>
                    <input id="ASpecs" class="easyui-textbox" data-options="prompt:'请输入规格',required:false" style="width: 120px" />
                </td>
                <td id="AFigureTd" style="text-align: right;">花纹:
                </td>
                <td>
                    <input id="AFigure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 120px" />
                </td>
                <td style="text-align: right;">货品代码:
                </td>
                <td>
                    <input id="AGoodsCode" class="easyui-textbox" style="width: 160px;" data-options="prompt:'请输入货品代码'" />
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <table id="outDg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="plOutCargo()">&nbsp;添加上订单&nbsp;</a>&nbsp;&nbsp;
                <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删除下订单&nbsp;</a>
    </div>
    <form id="fmDep" class="easyui-form" method="post">
        <input type="hidden" id="DisplayNum" />
        <input type="hidden" id="DisplayPiece" name="DisplayPiece" />
        <input type="hidden" name="ClientNum" id="ClientNum" />
        <input type="hidden" name="AAcceptAddress" id="AAcceptAddress" />
        <input type="hidden" name="AAcceptPeople" id="AAcceptPeople" />
        <input type="hidden" name="AAcceptUnit" id="AAcceptUnit" />
        <input type="hidden" name="AAcceptTelephone" id="AAcceptTelephone" />
        <input type="hidden" name="AAcceptCellphone" id="AAcceptCellphone" />
        <input type="hidden" name="ACheckOutType" id="ACheckOutType" />
        <div id="saPanel">
            <table style="width: 100%">
                <tr>
                    <td style="text-align: right;">订单类型:
                    </td>
                    <td>
                        <select class="easyui-combobox" id="AOrderType" name="OrderType" style="width: 100px;" panelheight="auto" editable="false">
                            <option value="3">调货单</option>
                            <option value="0">订货单</option>
                            <option value="2">退货单</option>
                        </select>
                    </td>
                    <td style="text-align: right;">供应商：</td>
                    <td>
                        <input name="AcceptPeople" id="SAcceptPeople" style="width: 120px;" class="easyui-combobox AcceptPeople" data-options="valueField:'ClientNum',textField:'Boss',editable:true,required:true" />
                    </td>
                    <td style="text-align: right;" hidden="hidden">发货地址：</td>
                    <td hidden="hidden">
                        <input id="AcceptAddress" name="AcceptAddress" style="width: 220px;" data-options="required:false" class="easyui-combobox AcceptPeople" />
                    </td>
                    <td style="text-align: right;" id="tdA">入库仓库:
                    </td>
                    <td id="tdB">
                        <input id="LOTPID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'AreaID',textField:'Name',editable:false"
                            panelheight="auto" />
                    </td>
                    <td style="text-align: right;">进货费用：</td>
                    <td>
                        <input name="TransportFee" id="TransportFee" class="easyui-numberbox" data-options="min:0,precision:2,required:false,editable:false" style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">运输费：</td>
                    <td>
                        <input name="TransitFee" id="TransitFee" class="easyui-numberbox" data-options="min:0,precision:2,required:false" style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">卸货费：</td>
                    <td>
                        <input name="HandFee" id="HandFee" class="easyui-numberbox" data-options="min:0,precision:2,required:false" style="width: 100px;" />
                    </td>
                    <td style="text-align: right;">进货单号：</td>
                    <td>
                        <input name="FacOrderNo" id="FacOrderNo" class="easyui-textbox" style="width: 100px;" />
                    </td>
                    <td style="text-align: right; display: none;">合计费用：</td>
                    <td style="display: none;">
                        <input name="TotalCharge" id="TotalCharge" class="easyui-numberbox" data-options="min:0,precision:2,required:false,editable:false" style="width: 100px;" />
                    </td>
                    <td colspan="8" style="text-align: right;">
                        <a href="#" id="btnSave" class="easyui-linkbutton" iconcls="icon-ok" plain="false" onclick="saveOutCargo()">&nbsp;保&nbsp;存&nbsp;订&nbsp;单</a>&nbsp;&nbsp;
                                <a href="#" class="easyui-linkbutton" id="undo" iconcls="icon-clear" onclick="reset()">&nbsp;重&nbsp;置&nbsp;</a>
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <div id="dlg" class="easyui-dialog" style="width: 400px; height: 300px; padding: 0px"
        closed="true" buttons="#dlg-buttons">
        <div id="saPanel">
            <form id="fm" class="easyui-form" method="post">
                <input type="hidden" id="InPiece" />
                <input type="hidden" id="InIndex" />
                <table>
                    <tr>
                        <td style="text-align: right;">来货数量：
                        </td>
                        <td>
                            <input name="Numbers" id="Numbers" class="easyui-numberbox" data-options="min:0,precision:0,prompt:'请输入来货数量',required:true" style="width: 200px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">来货批次：
                        </td>
                        <td>
                            <input name="Batch" id="Batch" class="easyui-textbox" data-options="prompt:'请输入来货批次',required:true" style="width: 200px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">进货价：</td>
                        <td>
                            <input id="UnitPrice" class="easyui-numberbox" data-options="min:0,precision:2,prompt:'请输入进货价',required:true" style="width: 200px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">门店价：</td>
                        <td>
                            <input id="TradePrice" class="easyui-numberbox" data-options="min:0,precision:2,prompt:'请输入门店价',required:true" style="width: 200px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">销售价：</td>
                        <td>
                            <input id="SalePrice" class="easyui-numberbox" data-options="min:0,precision:2,prompt:'请输入销售价',required:true" style="width: 200px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">是否含税：</td>
                        <td>
                            <select class="easyui-combobox" id="WhetherTax" name="WhetherTax" style="width: 100px;" panelheight="auto" editable="false">
                                <option value="0">不含税</option>
                                <option value="1">含税
                                </option>
                            </select>
                        </td>
                    </tr>
                </table>
                <div id="lblRule">
                </div>
            </form>
        </div>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="outOK()">&nbsp;确&nbsp;定&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>

    <script type="text/javascript">
        //收货人自动选择方法
        function onClientChanged(item) {
            if (item) {
                debugger;
                $('#AAcceptUnit').val(item.ClientName);
                $('#AAcceptAddress').val(item.Address);
                $('#AAcceptTelephone').val(item.Telephone);
                $('#AAcceptCellphone').val(item.Cellphone);
                $('#AAcceptPeople').val(item.Boss);
                $('#ClientNum').val(item.ClientNum);
                $('#ACheckOutType').val(item.CheckOutType);

                //$('#AcceptAddress').combobox({
                //    valueField: 'ADID', textField: 'AcceptPeopleCellphone', delay: '10',
                //    url: '../Client/clientApi.aspx?method=AutoCompleteClientAcceptPeople&ClientNum=' + item.ClientNum,
                //    onLoadSuccess: function () {
                //        //默认选中第一个
                //        var array = $(this).combobox("getData");
                //        for (var item in array[0]) {
                //            if (item == "ADID") {
                //                $(this).combobox('select', array[0][item]);
                //            }
                //        }
                //    },
                //    onSelect: onAcceptAddressChanged
                //});
            }
            $('#AcceptAddress').combobox('textbox').bind('focus', function () { $('#AcceptAddress').combobox('showPanel'); });

        }
        function onAcceptAddressChanged(item) {
            $('#AAcceptUnit').val(item.AcceptCompany);
            $('#AAcceptAddress').val(item.AcceptAddress);
            $('#AAcceptTelephone').val(item.AcceptTelephone);
            $('#AAcceptCellphone').val(item.AcceptCellphone);
            $('#AAcceptPeople').val(item.AcceptPeople);
        }
        ///出库
        function plOutCargo(row) {
            var index = $('#outDg').datagrid('getRowIndex', row.SID);
            if (index >= 0) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '进货单列表已存在该产品，请先删除再添加！', 'warning');
                return;
            }
            var RoleCName = "<%=UserInfor.RoleCName%>";
            if (RoleCName.indexOf("汕头科矿") >= 0) {
                var today = new Date(); // 创建一个表示当前时间的Date对象
                var year = today.getFullYear(); // 获取年份
                var month = (today.getMonth() + 1).toString().padStart(2, '0'); // 月份从0开始计数，所以需要加1
                var day = (today.getDate()).toString().padStart(2, '0'); // 获取日期
                // 将结果输出为字符串格式（YYYYMMDD）
                var dateString = year + month + day;
                $('#Batch').textbox('setValue', dateString);
                $('#Numbers').numberbox('setValue', '');
                $('#SalePrice').numberbox('setValue', '1');
                $('#TradePrice').numberbox('setValue', '1');
                $('#UnitPrice').numberbox('setValue', '1');
            } else {
                $('#Numbers').numberbox('setValue', '');
                $('#Batch').textbox('setValue', '');
                $('#SalePrice').numberbox('setValue', '');
                $('#TradePrice').numberbox('setValue', '');
                $('#UnitPrice').numberbox('setValue', '');
            }
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要拉上订单的数据！', 'warning');
                return;
            }
            $('#dlg').dialog('open').dialog('setTitle', '拉上  ' + row.Specs + ' 花纹：' + row.Figure);
            var myDate = new Date();
            var year = myDate.getFullYear().toString();
            //$("#Batch").numberbox('setValue', year.slice(2, 4));
            var FullDiscountFull = $('#FullDiscountFull').val();
            if (FullDiscountFull != undefined) {
                document.cookie = "FullDiscountFull=; expires=Thu, 01 Jan 1970 00:00:00 GMT";
                document.cookie = "FullDiscountCut=; expires=Thu, 01 Jan 1970 00:00:00 GMT";
                document.cookie = "FullDiscountSum=; expires=Thu, 01 Jan 1970 00:00:00 GMT";
                var ThisRuleContent = $('#FullDiscountCut').val();
                document.cookie = "FullDiscountFull=" + FullDiscountFull;
                document.cookie = "FullDiscountCut=" + ThisRuleContent;
                document.cookie = "FullDiscountSum=" + text.FullDiscountSum;
            }
        }
        TransportFee = 0;
        //新增出库数据
        function outOK() {
            var row = $('#dg').datagrid('getSelected');
            if ($('#Numbers').val() == null || $('#Numbers').val() == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请输入拉上订单数量！', 'warning');
                return;
            }
            if ($('#Numbers').val() < 1) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '拉上订单数量必须大于0！', 'warning');
                return;
            }
<%--            if ($("#Batch").val().length < 4) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请输入完整4位批次！', 'warning');
                return;
            }--%>
            var index = $('#outDg').datagrid('getRowIndex', row.SID);
            if (index < 0) {
                row.Piece = $('#Numbers').numberbox('getValue');
                row.Batch = $('#Batch').textbox('getValue');
                row.SalePrice = $('#SalePrice').numberbox('getValue');
                row.UnitPrice = $('#UnitPrice').numberbox('getValue');
                row.TradePrice = $('#TradePrice').numberbox('getValue');
                row.WhetherTax = $('#WhetherTax').numberbox('getValue');
                //if ($('#TransitFee').numberbox('getValue') != "" || $('#HandFee').numberbox('getValue') != "") {
                //    row.CostPrice = $('#SalePrice').numberbox('getValue');
                //} else {
                //    row.CostPrice = $('#SalePrice').numberbox('getValue');
                //}
                $('#outDg').datagrid('appendRow', row);
                var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
                var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
                n++;
                var pt = p + Number($('#Numbers').numberbox('getValue'));
                if (getCookie("FullDiscountFull") > 0) {
                    var ptNum = (parseInt(Number($('#Numbers').numberbox('getValue'))) + parseInt(getCookie("FullDiscountSum"))) / getCookie("FullDiscountFull");
                    var ptList = (ptNum + "").split(".");
                    if (ptList.length > 1) {
                        fullDiscountCut = ptList[0] * parseInt(getCookie("FullDiscountCut"));
                    } else {
                        fullDiscountCut = ptList[0] * parseInt(getCookie("FullDiscountCut"));
                    }
                }
                $('#DisplayNum').val(Number(n));
                $('#DisplayPiece').val(Number(pt));
                var title = "上订单     已拉上：" + n + "票，总件数：" + pt + " 件";
                $('#outDg').datagrid("getPanel").panel("setTitle", title);
                TransportFee += $('#Numbers').numberbox('getValue') * $('#UnitPrice').numberbox('getValue');
                $('#TransportFee').numberbox('setValue', TransportFee);
                $('#TotalCharge').numberbox('setValue', TransportFee + Number($('#TransitFee').numberbox('getValue')) + Number($('#HandFee').numberbox('getValue')));
                closedgShowData();

            } else {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '进货单列表已存在该产品，请先删除再添加！', 'warning');
            }
        }
        function getCookie(cname) {
            var name = cname + "=";
            var cookie = document.cookie.split(';');
            for (var i = 0; i < cookie.length; i++) {
                var c = cookie[i].trim();
                if (c.indexOf(name) == 0) return c.substring(name.length, c.length);
            }
            return "";
        }
        //关闭
        function closedgShowData() {
            $('#dlg').dialog('close');
        }
        //删除出库的数据
        function dblClickDelCargo(Did) {
            var row = $("#outDg").datagrid('getData').rows[Did];
            var n = $('#DisplayNum').val() == "" || isNaN($('#DisplayNum').val()) ? 0 : Number($('#DisplayNum').val());
            var p = $('#DisplayPiece').val() == "" || isNaN($('#DisplayPiece').val()) ? 0 : Number($('#DisplayPiece').val());
            n--;
            var pt = p - row.Piece;
            $('#DisplayNum').val(Number(n));
            $('#DisplayPiece').val(Number(pt));
            var UnitPrice = Number(row.UnitPrice);//销售价
            TransportFee -= UnitPrice * row.Piece;
            $('#TransportFee').numberbox('setValue', TransportFee);
            $('#TotalCharge').numberbox('setValue', TransportFee + Number($('#TransitFee').numberbox('getValue')) + Number($('#HandFee').numberbox('getValue')));

            var title = "上订单     已拉上：" + n + "票，总件数：" + pt + " 件";
            $('#outDg').datagrid("getPanel").panel("setTitle", title);

            var index = $('#dg').datagrid('getRowIndex', row.SID);
            if (index >= 0) {
                var Trow = $("#dg").datagrid('getData').rows[index];
                Trow.Piece = Trow.InPiece;
                $('#dg').datagrid('updateRow', { index: index, row: Trow });
            }
            var index = $('#outDg').datagrid('getRowIndex', row);
            $('#outDg').datagrid('deleteRow', index);
        }
        //保存订单
        function saveOutCargo() {
            //取消业务员禁用方便后台取值
            var rows = $('#outDg').datagrid('getRows');
            if (rows == null || rows == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要保存的数据！', 'warning'); return; }

            HID = "<%=UserInfor.HouseID%>";
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定保存？', function (r) {
                if (r) {
                    //启用复选框用于后台数据获取
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    $('#btnSave').linkbutton('disable');
                    var json = JSON.stringify(rows);
                    var LOTPID = $('#LOTPID').combobox('getValue');
                    $('#fmDep').form('submit', {
                        url: 'orderApi.aspx?method=SaveArrivalOrderData',
                        contentType: "application/json;charset=utf-8", dataType: "json",
                        onSubmit: function (param) {
                            param.submitData = json;
                            param.LOTPID = LOTPID;
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
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功！', 'info');
                                location.reload();
                            } else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                            }
                        }
                    })
                }
            });
        }
    </script>
</asp:Content>
