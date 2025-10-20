<%@ Page Title="集采上架" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" ValidateRequest="false" CodeBehind="productReserveShelves.aspx.cs" Inherits="Cargo.Product.productReserveShelves" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../JS/KindEditer/themes/default/default.css" rel="stylesheet" />
    <link href="../JS/KindEditer/plugins/code/prettify.css" rel="stylesheet" />
    <script type="text/javascript" src="../JS/KindEditer/kindeditor-all.js"></script>
    <script type="text/javascript" src="../JS/KindEditer/lang/zh-CN.js"></script>
    <script src="../JS/KindEditer/plugins/code/prettify.js" type="text/javascript"></script>
    <script type="text/javascript">
        var editor;
        $(function () {
            KindEditor.ready(function (K) {
                editor = K.create('#Remark', {
                    cssPath: '../JS/KindEditer/plugins/code/prettify.css',
                    uploadJson: 'upload.ashx',
                    fileManagerJson: '../asp.net/file_manager_json.ashx',
                    allowFileManager: true,
                    afterCreate: function () { }
                });
                prettyPrint();
            });
        });
    </script>
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
            var columns = [];
            var HID = "<%=UserInfor.HouseID%>";
            $('#HiddenHouseID').val(HID);
            columns.push({ title: '', field: 'ID', checkbox: true, width: '30px' });
            //columns.push({
            //    title: '产品ID', field: 'ProductID', width: '60px', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});
            columns.push({
                title: '产品代码', field: 'ProductCode', width: '80px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            //columns.push({
            //    title: '货位代码', field: 'ContainerCode', width: '80px', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});

            columns.push({
                title: '上架数量', field: 'ShelvesNum', width: '60px', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            //columns.push({
            //    title: '在库数量', field: 'Piece', width: '60px', align: 'right', styler: function (val, row, index) { return "color:#12bb1f;font-weight:bold;" }, formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});
            columns.push({
                title: '销售价格', field: 'ProductPrice', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '签约价格', field: 'SigningPrice', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '最低起购', field: 'minPurchase', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '品牌', field: 'TypeName', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });

            //columns.push({
            //    title: '型号', field: 'Model', width: '60px', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});
            columns.push({
                title: '规格', field: 'Specs', width: '90px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '花纹', field: 'Figure', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '载速', field: 'LoadIndex', width: '70px', formatter: function (value, row) {
                    return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                }
            });
            columns.push({
                title: '货品代码', field: 'GoodsCode', width: '120px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });

            //columns.push({
            //    title: '所在仓库', field: 'HouseName', width: '60px', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});
            //columns.push({
            //    title: '一级区域', field: 'FirstAreaName', width: '60px', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});
            //columns.push({
            //    title: '二级区域', field: 'AreaName', width: '60px', formatter: function (value) {
            //        return "<span title='" + value + "'>" + value + "</span>";
            //    }
            //});
            columns.push({
                title: '上架状态', field: 'OnShelves', width: '60px', formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='已上架'>已上架</span>"; }
                    else if (val == "1") { return "<span title='未上架'>未上架</span>"; } else { return ""; }
                }
            });
            columns.push({ title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter });

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
                idField: 'ID',
                url: null,
                showFooter: true,
                toolbar: '#toolbar',
                columns: [columns],
                onLoadSuccess: function (data) {
                    var trs = $(this).prev().find('div.datagrid-body').find('tr');
                    for (var i = 0; i < trs.length; i++) {
                        for (var j = 1; j < trs[i].cells.length; j++) {
                            var row_html = trs[i].cells[j];
                            var cell_field = $(row_html).attr('field');
                            var cell_value = $(row_html).find('div').html();
                            if (cell_field == 'Piece' && cell_value > 0 && cell_value <= 4) {
                                trs[i].cells[j].style.cssText = 'background:#f9e6cd;';
                            }
                            if (cell_field == 'Piece' && cell_value == 0) {
                                trs[i].cells[j].style.cssText = 'background:#dfb3aa;';
                            }
                        }
                    }
                },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                rowStyler: function (index, row) {
                    if (row.OnShelves == "0") {
                        if (row.SaleType == "1") { return "background-color:#EECCFF"; } else if (row.SaleType == "3") {
                            return "background-color:#99ccff";
                        } else { return "background-color:#D1EEEE" }
                    }
                    if (row.Piece <= 4 && row.Piece > 0) {
                        return "background-color:#f9e6cd"
                    }
                    if (row.Piece == 0) {
                        return "background-color:#dfb3aa"
                    }

                    //else if (row.InCargoStatus == "1" && row.ContainerCode == "") { return "background-color:#EBEBEB"; }
                },
                onDblClickRow: function (index, row) { editProduct(index); }
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
            //一级产品
            $('#ParentID').combobox({
                url: '../Product/productApi.aspx?method=QueryALLOneProductType', valueField: 'TypeID', textField: 'TypeName',
                onSelect: function (rec) {
                    $('#TypeID').combobox('clear');
                    var url = 'productApi.aspx?method=QueryALLOneProductType&PID=' + rec.TypeID;
                    $('#TypeID').combobox('reload', url);
                }
            });
            //$('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            $('#APID').combobox('textbox').bind('focus', function () { $('#APID').combobox('showPanel'); });
            $('#ASID').combobox('textbox').bind('focus', function () { $('#ASID').combobox('showPanel'); });
        });

        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'productApi.aspx?method=QueryBasicProdictData';
            $('#dg').datagrid('load', {
                Specs: $('#ASpecs').val(),
                Model: $('#AModel').val(),
                Figure: $('#AFigure').val(),
                PID: $("#APID").combobox('getValue'),//一级产品
                SID: $("#ASID").combobox('getValue'),//二级产品
                GoodsCode: $('#AGoodsCode').val(),//AMeridian
                ProductName: $('#AProductName').val(),
                LoadIndex: $('#ALoadIndex').val(),
                onShelves: $("#onShelves").combobox('getValue'),
                //SaleType: $("#ASaleType").combobox('getValue'),
                //SpeedLevel: $("#ASpeedLevel").combobox('getValue'),
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
                <td id="ASpecsTd" style="text-align: right;">规格:
                </td>
                <td>
                    <input id="ASpecs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 100px" />
                </td>

                <td id="AModelTd" style="text-align: right;">型号:
                </td>
                <td>
                    <input id="AModel" class="easyui-textbox" data-options="prompt:'请输入产品型号'" style="width: 100px" />
                </td>
                <td style="text-align: right;">产品名称:
                </td>
                <td>
                    <input id="AProductName" class="easyui-textbox" data-options="prompt:'请输入产品名称'" style="width: 100px" />
                </td>

                <td style="text-align: right;">一级产品:
                </td>
                <td>
                    <input id="APID" class="easyui-combobox" style="width: 100px;"
                        panelheight="auto" />
                </td>


            </tr>
            <tr>
                <td id="AFigureTd" style="text-align: right;">花纹:
                </td>
                <td>
                    <input id="AFigure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 100px" />
                </td>

                <td style="text-align: right;">货品代码:
                </td>
                <td>
                    <input id="AGoodsCode" class="easyui-textbox" data-options="prompt:'请输入货品代码'" style="width: 100px">
                </td>
                <td style="text-align: right;">上架状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="onShelves" style="width: 100px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">已上架</option>
                        <option value="1">未上架</option>
                        <%--  <option value="2">下架</option>--%>
                    </select>
                </td>
                <td style="text-align: right;">二级产品:
                </td>
                <td>
                    <input id="ASID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'TypeID',textField:'TypeName'" />
                </td>

                <td colspan="2"></td>
            </tr>
        </table>
    </div>
    <input type="hidden" id="HiddenHouseID" />
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cart_remove" plain="false" onclick="shelves()">&nbsp;上&nbsp;架&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cart_put" plain="false" onclick="Unshelves()">&nbsp;下&nbsp;架&nbsp;</a>&nbsp;&nbsp;
        <%--<a href="#" class="easyui-linkbutton" iconcls="icon-out_cargo" plain="false" onclick="BatchShelves()">&nbsp;批量上架&nbsp;</a>&nbsp;&nbsp;--%>
        <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="Export()">&nbsp;导出&nbsp;</a>&nbsp;&nbsp;
        <form runat="server" id="fm1">
            <asp:Button ID="btnExport" runat="server" Style="display: none;" Text="导出" OnClick="btnExport_Click" />
        </form>
        <%--       <table style="display: inline;">
            <tr>
                <td bgcolor="#D1EEEE" height="15" width="15"></td>
                <td>：已上架</td>
                <td bgcolor="#EECCFF" height="15" width="15"></td>
                <td>：配比</td>
                <td bgcolor="#99ccff" height="15" width="15"></td>
                <td>：特价促销</td>
                <td bgcolor="#CDC5BF" height="15" width="15"></td>
                <td>：表示已出完</td>
                        <td bgcolor="#f9e6cd" height="15" width="15"></td>
                <td>：库存预警</td>
                <td bgcolor="#dfb3aa" height="15" width="15"></td>
                <td>：库存为零</td>
            </tr>
        </table>--%>
    </div>
    <%--<textarea id="editor" cols="100" rows="8" style="width: 700px; height: 200px; visibility: hidden;"></textarea>--%>

    <div id="dlg" class="easyui-dialog" style="width: 1000px; height: 570px; padding: 1px 1px"
        closed="true" buttons="#dlg-buttons">
        <div id="saPanel">
            <form id="fm" class="easyui-form" method="post">
                <input type="hidden" name="OnSaleID" />
                <input type="hidden" name="ProductID" />
                <input type="hidden" name="TypeID" />
                <input type="hidden" name="ProductName" />
                <input type="hidden" name="ProductCode" />
                <table>
                    <tr>
                        <td style="text-align: right;">商品名称
                        </td>
                        <td colspan="7">
                            <input name="Title" id="Title" class="easyui-textbox" data-options="prompt:'请输入商品名称',required:true" style="width: 600px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" id="sj">上架数量
                        </td>
                        <td>
                            <input name="OnSaleNum" id="OnSaleNum" class="easyui-numberbox" data-options="prompt:'请输入上架数量',required:true"
                                style="width: 60px;" />
                        </td>



                        <%-- <td style="text-align: right;" rowspan="2">销售类型</td>
                        <td>
                            <input type="checkbox" id="SaleType1" name="SaleType" value="1" /><label for="SaleType1">天天特价</label>
                            <input type="checkbox" id="SaleType3" name="SaleType" value="3" /><label for="SaleType3">限时促销</label>


                        </td>--%>
                        <td style="text-align: right;">商城价格
                        </td>
                        <td>
                            <input name="ProductPrice" id="ProductPrice" class="easyui-numberbox" data-options="min:0,precision:2"
                                style="width: 60px;" />
                        </td>

                        <td style="text-align: right;">签约价格
                        </td>
                        <td>
                            <input name="SigningPrice" id="SigningPrice" class="easyui-numberbox" data-options="min:0,precision:2"
                                style="width: 60px;" />
                        </td>

                        <%--                 <td style="text-align: right;">促销有效期</td>
                        <td>
                            <input id="AdvertStartDate" name="AdvertStartDate" class="easyui-datebox" style="width: 100px" />~
                    <input id="AdvertEndDate" name="AdvertEndDate" class="easyui-datebox" style="width: 100px" />
                        </td>--%>
                    </tr>
                    <tr>
                        <td style="text-align: right; color: gray;">在库数量
                        </td>
                        <td>
                            <input name="Piece" id="Piece" class="easyui-textbox" style="width: 60px;" />
                        </td>
                        <%--  <td>
                            <input type="checkbox" id="SaleType4" name="SaleType" value="4" /><label for="SaleType4">积分兑换</label></td>--%>
                        <td style="text-align: right;" id="jf">积分数量
                        </td>
                        <td>
                            <input name="Consume" id="Consume" class="easyui-numberbox" data-options="prompt:'请输入兑换所需积分数量'"
                                style="width: 60px;" />
                        </td>
                        <td style="text-align: right;" id="qg">最低起购
                        </td>
                        <td>
                            <input name="minPurchase" id="minPurchase" class="easyui-numberbox" data-options="prompt:'请输入最低起购数量'"
                                style="width: 60px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">商品描述:
                        </td>
                        <td colspan="7">
                            <textarea id="Remark" name="Remark" cols="100" rows="8" style="width: 800px; height: 400px; visibility: hidden;"></textarea>
                        </td>
                    </tr>

                </table>
            </form>
        </div>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>


    <div id="Batchdlg" class="easyui-dialog" style="width: 400px; height: 200px; padding: 1px 1px" closed="true" buttons="#Batchdlg-buttons">
        <div id="saPanel">
            <form id="fm" class="easyui-form" method="post">
                <table>
                    <tr>
                        <td style="text-align: right;">销售类型</td>
                        <td>
                            <input type="radio" id="ASaleType1" name="SaleType" value="5" checked /><label for="ASaleType1">预订单</label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">特价率
                        </td>
                        <td>
                            <input name="SpecialRate" id="SpecialRate" class="easyui-numberbox" data-options="prompt:'请输入特价率，比如：0.12', min:0, precision:2" style="width: 160px;" />
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
    <div id="Batchdlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItemBatch1()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#Batchdlg').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <script type="text/javascript">
        //导出
        function Export() {
            var rows = $("#dg").datagrid('getData').rows;
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning');
                return;
            }

            $.messager.progress({ msg: '请稍后,正在导出中...' });
            var Specs = $('#ASpecs').val();
            var Model = $('#AModel').val();
            var Figure = $('#AFigure').val();
            var PID = $("#APID").combobox('getValue');//一级产品
            var SID = $("#ASID").combobox('getValue');//二级产品
            var ProductName = $('#AProductName').val();
            var Batch = $('#ABatch').val();
            var BatchYear = $('#ABatchYear').combobox('getValue');
            var LoadIndex = $('#ALoadIndex').val();
            var onShelves = $("#onShelves").combobox('getValue');
            var SaleType = $("#ASaleType").combobox('getValue');
            var HAID = $("#HAID").combobox('getValue');
            var HouseID = $("#AHouseID").combobox('getValue');//仓库ID
            var Piece = $("#APiece").combobox('getValue');//在库数量类型
            var GoodsCode = $('#AGoodsCode').val();
            $.ajax({
                url: 'productApi.aspx?method=QueryProductShelvesForExport',
                data: { GoodsCode: GoodsCode, Specs: Specs, Model: Model, Figure: Figure, PID: PID, SID: SID, ProductName: ProductName, Batch: Batch, BatchYear: BatchYear, LoadIndex: LoadIndex, onShelves: onShelves, SaleType: SaleType, HAID: HAID, HouseID: HouseID, Piece: Piece },
                success: function (text) {
                    $.messager.progress("close");
                    if (text == "OK") { var obj = document.getElementById("<%=btnExport.ClientID %>"); obj.click(); }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }
        //下架
        function Unshelves() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要下架的商品！', 'warning');
                return;
            }
            for (var i = 0; i < rows.length; i++) {
                rows[i].Memo = "";
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定下架该商品？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'productApi.aspx?method=UnLoadReserveshelves',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '下架成功!', 'info');
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
        //保存
        function saveItem() {
            var onNum = Number($('#OnSaleNum').numberbox('getValue'));

            $.messager.progress({ msg: '请稍后,正在保存中...' });
            $('#fm').form('submit', {
                url: 'productApi.aspx?method=SaveProductReserveShelves',
                contentType: "application/json;charset=utf-8", dataType: "json",
                onSubmit: function (param) {
                    //var str = editor.html();
                    //var imgReg = /<img.*?(?:>|\/>)/gi;
                    //var srcReg = /src=[\'\"]?([^\'\"]*)[\'\"]?/i;
                    //var arr = str.match(imgReg);  // arr 为包含所有img标签的数组
                    //var imglist='';
                    //for (var i = 0; i < arr.length; i++) {
                    //    if (i >= 4) { break; }
                    //    var src = arr[i].match(srcReg);
                    //    //获取图片地址
                    //    console.log('图片地址' + (i + 1) + '：' + src[1]);
                    //    imglist += src[1] + "|";
                    //}
                    //param.imglist = escape(imglist);
                    param.editor = escape(editor.html());
                    var trd = $(this).form('enableValidation').form('validate');
                    if (!trd) { $.messager.progress("close"); }
                    return trd;
                },
                success: function (msg) {
                    $.messager.progress("close");
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '操作成功!', 'info');
                        $('#dlg').dialog('close'); 	// close the dialog
                        editor.html('');
                        dosearch();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '操作失败：' + result.Message, 'warning');
                    }
                }
            })
        }
        //批量上架
        function BatchShelves() {
            //var row = $('#dg').datagrid('getSelected');
            $('#Batchdlg').dialog('open').dialog('setTitle', '集采上架');
            $('#SaleType').textbox('setValue', '0');
        }
        //保存批量上架

        function saveItemBatch1() {
            var rows = $('#dg').datagrid('getSelections');
            var urldata = '&SpecialRate=' + $('#SpecialRate').val();
            urldata += '&ASaleType=' + $('#ASaleType1').val();
            if (rows == null || rows == "") {
                //未选择上架数据
                urldata += '&Specs=' + $('#ASpecs').val();
                urldata += '&Model=' + $('#AModel').val();
                urldata += '&Figure=' + $('#AFigure').val();
                urldata += '&GoodsCode=' + $('#AGoodsCode').val();
                urldata += '&PID=' + $("#APID").combobox('getValue');
                urldata += '&SID=' + $("#ASID").combobox('getValue');
                urldata += '&ProductName=' + $('#AProductName').val();
                urldata += '&LoadIndex=' + $('#ALoadIndex').val();
                urldata += '&onShelves=' + $('#onShelves').numberbox('getValue');
            }

            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定上架？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    var json = JSON.stringify(rows)
                    console.log(urldata, 22222222222)
                    $.ajax({
                        url: 'productApi.aspx?method=SaveBatchProductReserveShelves' + urldata,
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            if (text.Result == true) {
                                if (text.Message == "成功") {
                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                                } else {
                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'info');
                                }
                                $('#Batchdlg').dialog('close');
                                dosearch();

                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        },
                        error: function (text) {
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            $.messager.progress('close');
                        }
                    });
                }
            });


        }

        //上架
        function shelves() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要上架的商品！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '集采上架');
                $('#sj').text("上架数量");
                $('#fm').form('clear');
                $('#fm').form('load', row);
                $('#Piece').textbox('setValue', row.Piece);
                $('#Piece').textbox('disable');
                $('#Title').textbox('setValue', row.TypeName + row.Specs + " " + row.Figure + " " + row.LoadIndex + row.SpeedLevel);
                $('#OnSaleNum').numberbox('setValue', row.Piece);

                $('#ProductPrice').numberbox('setValue', '0');
                $('#SigningPrice').numberbox('setValue', '0');
                $('#minPurchase').numberbox('setValue', '0');

                $.ajax({
                    url: 'productApi.aspx?method=QueryTryePic',
                    type: 'post', dataType: 'json', data: { TypeID: row.TypeID, Figure: row.Figure },
                    success: function (text) {
                        if (text.FileName != undefined && text.FileName != "") {
                            editor.html("<img src=\".." + text.FileName + "\" alt=\"\" />");
                        } else {
                            if (row.TypeID == 9) {
                                editor.html("<img src=\"../upload/image/20191103/20191103103220_8800.jpg\" alt=\"\" />");
                            } else if (row.TypeID == 18) {
                                editor.html("<img src=\"../upload/image/20191030/20191030113730_6995.png\" alt=\"\" />");
                            } else if (row.TypeID == 19) {
                                editor.html("<img src=\"../upload/image/20191030/20191030162429_7685.png\" alt=\"\" />");
                            } else if (row.TypeID == 31) {
                                editor.html("<img src=\"../upload/image/20191103/20191103103220_8800.jpg\" alt=\"\" />");
                            } else if (row.TypeID == 34) {
                                editor.html("<img src=\"../upload/image/20191204/20191204143119_7864.jpg\" alt=\"\" />");
                            } else if (row.TypeID == 66) {
                                editor.html("<img src=\"../upload/image/20191103/20191103153540_1374.jpg\" alt=\"\" />");
                            } else if (row.TypeID == 157) {
                                editor.html("<img src=\"../upload/image/20200730/20200730155130_5798.jpg\" alt=\"\" />");
                            } else if (row.TypeID == 164) {
                                editor.html("<img src=\"../upload/image/20200730/20200730155234_0724.jpg\" alt=\"\" />");
                            }
                        }
                    }
                });
            }
        }

        //修改上架商品
        function editProduct(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row.OnShelves != "0") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '该商品未上架，不允许修改！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改上架商品');
                $('#sj').text("在架数量");
                $('#fm').form('clear');
                $('#fm').form('load', row);
                $('#Piece').textbox('setValue', row.Piece);
                $('#Piece').textbox('disable');
                $('#OnSaleNum').numberbox('setValue', row.ShelvesNum);
                editor.html(row.Memo);
            }
        }
    </script>
</asp:Content>
