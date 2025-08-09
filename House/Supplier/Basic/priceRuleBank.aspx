<%@ Page Title="促销规则管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="priceRuleBank.aspx.cs" Inherits="Supplier.Basic.priceRuleBank" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
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
            //所在仓库-查询
            $('#HID').combobox({
                url: '../FormService.aspx?method=CargoPermisionHouse',
                valueField: 'SettleHouseID', textField: 'SettleHouseName'
            });
            $('#HID').combobox('setValue', '<%=UserInfor.SettleHouseID%>');
            //有效时间-查询
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getNowFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#StartDate').datebox('textbox').bind('focus', function () { $('#StartDate').datebox('showPanel'); });
            $('#EndDate').datebox('textbox').bind('focus', function () { $('#EndDate').datebox('showPanel'); });
        }
        function adjustment() {
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true);
            $('#dg').datagrid({ height: height });
        }
        $(document).ready(function () {
            var columns = [];
            var HID = "<%=UserInfor.HouseID%>";

            columns.push({ title: '', field: 'ID', checkbox: true, width: '30px' });
            columns.push({
                title: '规则名称', field: 'RuleName', width: '150px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '规则标题', field: 'Title', width: '150px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '规则类型', field: 'RuleType', width: '80px',
                formatter: function (val, row, index) {
                    if (val == "0") { return "<span title='满减'>满减</span>"; }
                    else if (val == "1") { return "<span title='满送轮胎'>满送轮胎</span>"; }
                    else if (val == "2") { return "<span title='折扣'>折扣</span>"; }
                    else if (val == "3") { return "<span title='满送礼品'>满送礼品</span>"; }
                    else if (val == "4") { return "<span title='限购数量'>限购数量</span>"; }
                    else if (val == "5") { return "<span title='APP首单送'>APP首单送</span>"; }
                    else if (val == "6") { return "<span title='直减n元'>直减n元</span>"; }
                    else if (val == "7") { return "<span title='满送优惠卷'>满送优惠卷</span>"; }
                    else if (val == "8") { return "<span title='满送赠品'>满送赠品</span>"; }
                    else { return ""; }
                }
            });
            columns.push({ title: '开始时间', field: 'StartDate', width: '80px', formatter: DateFormatter });
            columns.push({ title: '结束时间', field: 'EndDate', width: '80px', formatter: DateFormatter });
            columns.push({
                title: '满', field: 'FullEntry', width: '60px', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '减/送', field: 'CutEntry', width: '60px', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '折扣', field: 'SaleEntry', width: '60px', align: 'right', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '适用仓库', field: 'HouseName', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '适用品牌', field: 'TypeName', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '使用品牌', field: 'UseTypeName', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '赠品品牌', field: 'ProductName', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '赠品规格', field: 'Specs', width: '100px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '优惠卷使用时间（天）', field: 'ServiceTime', width: '80px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });

            columns.push({
                title: '发放赠品', field: 'IssueGiveaway', width: '80px', formatter: function (value) {
                    if (value == 0) {
                        return "<span title='否'>否</span>";
                    } else {
                        return "<span title='是'>是</span>";
                    }
                }
            });
            columns.push({
                title: '发放优惠卷', field: 'IssueCoupon', width: '80px', formatter: function (value) {
                    if (value == 0) {
                        return "<span title='否'>否</span>";
                    } else {
                        return "<span title='是'>是</span>";
                    }
                }
            });
            columns.push({
                title: '是否可叠加使用', field: 'IsSuperPosition', width: '80px', formatter: function (value) {
                    if (value == 0) {
                        return "<span title='否'>否</span>";
                    } else {
                        return "<span title='是'>是</span>";
                    }
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
                fitColumns: true, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: false, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [columns],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { editItem(); }
            });
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'BasicApi.aspx?method=QueryRuleBankData';
            $('#dg').datagrid('load', {
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                HID: $("#HID").combobox('getValue'),
                Title: $('#Title').val()
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
                <td style="text-align: right;">规则名称:
                </td>
                <td>
                    <input id="Title" class="easyui-textbox" data-options="prompt:'请输入规则名称'" style="width: 120px">
                </td>
                <td style="text-align: right;">区域大仓:
                </td>
                <td>
                    <input id="HID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'id',textField:'text',editable:true" />
                    <%-- <input id="HID" class="easyui-combobox" data-options="url:'../FormService.aspx?method=CargoPermisionHouse',method:'get',valueField:'id',textField:'text'" style="width: 100px;" />--%>
                </td>
                <td style="text-align: right;">有效期间:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px" />~
                 <input id="EndDate" class="easyui-datebox" style="width: 100px" />
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
    <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">&nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;
    <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;
    <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 700px; height: 400px; padding: 1px 1px"
        closed="true" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" id="Hrule" name="Hrule" />
            <input type="hidden" id="ProductID" name="ProductID" />
            <input type="hidden" id="UseTypeName" name="UseTypeName" />
            <input type="hidden" id="ID" name="ID" />

            <div id="saPanel">
                <table>
                    <tr>
                        <td style="text-align: right;">促销条件：</td>
                        <td>
                            <input type="radio" name="IssueCoupon" value="2" />
                            <label for="IssueCoupon">发放优惠卷</label>
                            <input type="radio" name="IssueCoupon" value="3" />
                            <label for="IssueCoupon">发放赠品</label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">规则名称：</td>
                        <td>
                            <input name="RuleName" id="RuleName" class="easyui-textbox" data-options="prompt:'可为空，显示在规则下方',required:false" style="width: 210px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">促销规则：</td>
                        <td>
                            <select class="easyui-combobox" id="Rule1" name="Rule1" style="width: 60px;" panelheight="auto" editable="false" disabled="true">
                                <option value="0">满</option>
                                <option value="3">直减</option>
                                <option value="1">限购</option>
                                <option value="2">打折</option>
                            </select>

                            <input name="FullEntry" id="FullEntry" data-options="min:0,precision:0,required:true" class="easyui-numberbox" style="width: 80px;" />

                            <select class="easyui-combobox" id="Rule2" name="Rule2" style="width: 60px;" editable="false" panelheight="auto" disabled="true">
                                <option value="0">元</option>
                                <option value="1">条</option>
                            </select>
                            <select class="easyui-combobox" id="Rule3" name="Rule3" style="width: 60px;" editable="false" panelheight="auto" disabled="true">
                                <option value="0">送</option>
                                <option value="1">减</option>
                            </select>
                            <input name="CutEntry" id="CutEntry" data-options="min:0,precision:2,required:true" class="easyui-numberbox" style="width: 80px;" />
                            <select class="easyui-combobox" id="Rule4" name="Rule4" style="width: 60px;" editable="false" panelheight="auto" disabled="true">
                                <option value="0">元</option>
                                <option value="1">条</option>
                                <option value="2">箱</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">有效期间：</td>
                        <td>
                            <input id="AStartDate" name="StartDate" class="easyui-datebox" style="width: 100px" data-options="required:true" />~
                        <input id="AEndDate" name="EndDate" class="easyui-datebox" style="width: 100px" data-options="required:true" />
                            <label id="DateNumLab" style="text-align: left;"></label>

                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">适用仓库：</td>
                        <td>
                            <input id="HouseID" name="HouseID" class="easyui-combobox" style="width: 210px;" data-options="multiple:false,required:true" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">适用品牌：</td>
                        <td>
                            <input id="AProductID" name="AProductID" class="easyui-combobox" style="width: 210px;" data-options="multiple:false,required:true" />
                        </td>
                    </tr>
                    <tr>
    <td style="text-align: right;">使用品牌：</td>
    <td>
        <input id="UseTypeID" name="UseTypeID" class="easyui-combobox" style="width: 210px;" data-options="multiple:false" />
    </td>
</tr>
                    <tr id="APID1">
                        <td style="text-align: right;">赠品品牌：</td>
                        <td>一级：<input id="APID" class="easyui-combobox" style="width: 100px;"
                            panelheight="auto" />&nbsp;&nbsp;二级：<input id="ASID" name="TypeID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'TypeID',textField:'TypeName'" /></td>
                    </tr>
                    <tr id="Specs1">
                        <td style="text-align: right;">赠品规格：</td>
                        <td>
                            <input id="Specs" name="Specs" class="easyui-combobox" style="width: 300px;" data-options="valueField:'Specs',textField:'Specs'" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">可用时间：</td>
                        <td>
                            <input type="radio" name="ServiceTime" value="7" />
                            <label for="ServiceTime">7天</label>
                            <input type="radio" name="ServiceTime" value="15" />
                            <label for="ServiceTime">15天</label>
                            <input type="radio" name="ServiceTime" value="30" />
                            <label for="ServiceTime">30天</label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">可叠加使用：</td>
                        <td>
                            <input type="radio" name="IsSuperPosition" value="0" />
                            <labe for="IsSuperPosition">否</labe>
                            <input type="radio" name="IsSuperPosition" value="1" />
                            <label for="IsSuperPosition">是</label>
                        </td>
                    </tr>

                </table>
            </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" id="save" iconcls="icon-ok" onclick="saveRuleBank()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;
    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <script type="text/javascript">
        //保存规则
        function saveRuleBank() {
            //促销规则1
            var AStartDate = $('#AStartDate').datebox('getValue');
            if (AStartDate == undefined || AStartDate == "") {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请选择有效期间！', 'warning');
                $('#AStartDate').textbox('textbox').focus();
                return;
            }
            var AEndDate = $('#AEndDate').datebox('getValue');
            if (AEndDate == undefined || AEndDate == "") {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请选择有效期间！', 'warning');
                $('#AEndDate').textbox('textbox').focus();
                return;
            }
            var hid = $("#HouseID").combobox('getValue');
            if (hid == undefined || hid == "") {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请选择适用仓库！', 'warning');
                $('#HouseID').textbox('textbox').focus();
                return;
            }
            var apid = $("#AProductID").combobox('getValue');
            console.log("品牌信息", apid)
            if ((apid == undefined || apid == "")) {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请选择适用品牌！', 'warning');
                $('#AProductID').textbox('textbox').focus();
                return;
            }

            <%--var pid = $("#ProductID").combobox('getValue');
            var selectedValue = $('input[name="IssueCoupon"]:checked').val();
            if ((pid == undefined || pid == "") && selectedValue == 3) {
              $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请选择适用赠品品牌信息与规格信息！', 'warning');
                $('#ProductID').textbox('textbox').focus();
                return;
            }--%>



            var UPIDText = $('#UseTypeID').combobox('getText')
            $('#UseTypeName').val(UPIDText);
            //$('#UseTypeName').combobox('setValue', UPIDText);
            console.log(UPIDText);


            $('#fm').form('submit', {
                url: 'BasicApi.aspx?method=saveRuleBank',
                onSubmit: function () {
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        //if ($("#ID").val() != "") {
                            $('#dlg').dialog('close');
                        //}
                        dosearch();
                    } else {
                        $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
        //新增规则
        function addItem() {
            $('#dlg').dialog('open').dialog('setTitle', '新增促销规则');

            $('#fm').form('clear');

            $('#Rule1').combobox('setValue', '0');
            $('#Rule2').combobox('setValue', '1');
            $('#Rule3').combobox('setValue', '0');
            $('#Rule4').combobox('setValue', '0');

            $('input[name="ServiceTime"][value="7"]').prop('checked', true);
            $('input[name="IsSuperPosition"][value="1"]').prop('checked', true);
            $('input[name="IssueCoupon"][value="2"]').prop('checked', true);


            $('#APID').combobox({
                url: 'BasicApi.aspx?method=QueryALLOneProductType', valueField: 'TypeID', textField: 'TypeName',
                onSelect: function (rec) {
                    $('#ASID').combobox('clear');
                    $('#ASID').combobox({
                        url: 'BasicApi.aspx?method=QueryALLOneProductType&PID=' + rec.TypeID, valueField: 'TypeID', textField: 'TypeName', AddField: 'PinyinName',
                        filter: function (q, row) {
                            var opts = $(this).combobox('options');
                            return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                        },
                    });
                    //轮胎规格
                    $('#ASID').combobox({
                        onSelect: function (fai) {
                            $('#Specs').combobox('clear');
                            $('#Specs').combobox({
                                url: 'BasicApi.aspx?method=QueryAllSpesc&typeid=' + fai.TypeID + '&houseID=' + $("#HouseID").combobox('getValue'),
                                onSelect: function (a) {
                                    $('#ProductID').val(a.ProductID);
                                },
                            });
                            //var url = 'BasicApi.aspx?method=QueryAllSpesc&typeid=' + fai.TypeID + '&houseID=' + $("#HouseID").combobox('getValue');
                            //$('#Specs').combobox('reload', url);
                        }
                    });
                }
            });

            //仓库信息
            $('#HouseID').combobox({
                url: '../FormService.aspx?method=CargoPermisionHouse', valueField: 'SettleHouseID', textField: 'SettleHouseName',
                onSelect: function (rec) {
                    // 构建数据数组
                    var clientTypes = rec.ClientTypeID.split(',').map(function (id, index) {
                        return {
                            value: id,
                            text: rec.ClientTypeName.split(',')[index]
                        };
                    });
                    console.log('Selected dt:', clientTypes);
                    // 加载到combobox中
                    $('#AProductID').combobox({
                        valueField: 'value',
                        textField: 'text',
                        data: clientTypes
                    });
                    $('#UseTypeID').combobox({
                        valueField: 'value',
                        textField: 'text',
                        data: clientTypes,
                        multiple: true
                    });
                }
            });

            $('#HouseID').combobox('setValue', '<%=UserInfor.SettleHouseID%>');
            $('#HouseID').combobox('textbox').bind('focus', function () { $('#HouseID').combobox('showPanel'); });
            $('#AProductID').combobox('textbox').bind('focus', function () { $('#AProductID').combobox('showPanel'); });
            $('#UseTypeID').combobox('textbox').bind('focus', function () { $('#UseTypeID').combobox('showPanel'); });

            $('#AStartDate').datebox('textbox').bind('focus', function () { $('#AStartDate').datebox('showPanel'); });
            $('#AEndDate').datebox('textbox').bind('focus', function () { $('#AEndDate').datebox('showPanel'); });

            $('#APID1').hide();
            $('#Specs1').hide();
        }
        //促销规则单选界面更改
        $('input[name="IssueCoupon"]').change(function () {
            // 获取选中的单选按钮的值
            var selectedValue = $(this).val();
            //发放优惠卷
            if (selectedValue == 2) {
                $('#Rule4').combobox('setValue', '0');
                $('#APID1').hide();
                $('#Specs1').hide();
                console.log('发放优惠卷', selectedValue);
            }
            //发放赠品
            if (selectedValue == 3) {
                $('#Rule4').combobox('setValue', '2');
                $('#APID1').show();
                $('#Specs1').show();
                console.log('发放赠品', selectedValue);
            }
        });

        //修改规则
        function editItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (rows.length > 1) {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请选择一条要修改的数据！', 'warning');
                return;
            }
            if (rows[0]) {
                $('#dlg').dialog('open').dialog('setTitle', '修改促销规则');
                $('#ID').val(rows[0].ID);
                //单选
                if (rows[0].IssueCoupon == 1) {
                    $('input[name="IssueCoupon"][value="2"]').prop('checked', true);
                    $('#Rule4').combobox('setValue', '0');
                    $('#APID1').hide();
                    $('#Specs1').hide();
                }
                if (rows[0].IssueGiveaway == 1) {
                    $('input[name="IssueCoupon"][value="3"]').prop('checked', true);
                    $('#Rule4').combobox('setValue', '2');
                    $('#APID1').show();
                    $('#Specs1').show();
                }

                $('#RuleName').textbox('setValue', rows[0].RuleName);
                $('#FullEntry').textbox('setValue', rows[0].FullEntry);
                $('#CutEntry').textbox('setValue', rows[0].CutEntry);
                $('#AStartDate').textbox('setValue', rows[0].StartDate.split('T'));
                $('#AEndDate').textbox('setValue', rows[0].EndDate.split('T'));

                $('input[name="ServiceTime"][value="' + rows[0].ServiceTime +'"]').prop('checked', true);
                $('input[name="IsSuperPosition"][value="' + rows[0].IsSuperPosition + '"]').prop('checked', true);


                $('#APID').combobox({
                    url: 'BasicApi.aspx?method=QueryALLOneProductType', valueField: 'TypeID', textField: 'TypeName',
                    //onLoadSuccess: function (rec) {
                    //    var TypeID = "";
                    //    if (rows[0].ProductID != null) {
                    //        $.ajax({
                    //            url: 'BasicApi.aspx?method=QueryAllTypeIDToProductID&productID=' + rows[0].ProductID,
                    //            type: 'post',
                    //            dataType: 'json',
                    //            success: function (text) {
                    //                console.log(text, "产品信息1")
                    //                TypeID = text[0].TypeID;
                    //                console.log(TypeID, "产品信息2")
                    //                //$('#APID').combobox('setValue', TypeID);
                    //                $('#ASID').combobox('clear');
                    //                $('#ASID').combobox({
                    //                    url: 'BasicApi.aspx?method=QueryALLOneProductType&PID=' + TypeID, valueField: 'TypeID', textField: 'TypeName', AddField: 'PinyinName',
                    //                    filter: function (q, row) {
                    //                        var opts = $(this).combobox('options');
                    //                        return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                    //                    },
                    //                });
                    //                $('#ASID').combobox('setValue', text[0].ProductID);
                    //                //$('#APID').combobox('setValue', text[0].TypeID);
                    //                //$('#ASID').combobox('setValue', text[0].ProductID);
                    //                //$('#Specs').combobox('setValue', text[0].Specs);
                    //            }
                    //        });
                    //        //console.log(TypeID, "产品信息3")
                            

                    //    }

                    //},
                        onSelect: function (rec) {
                            $('#ASID').combobox('clear');
                            $('#ASID').combobox({
                                url: 'BasicApi.aspx?method=QueryALLOneProductType&PID=' + rec.TypeID, valueField: 'TypeID', textField: 'TypeName', AddField: 'PinyinName',
                                filter: function (q, row) {
                                    var opts = $(this).combobox('options');
                                    return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                                },
                            });
                            //轮胎规格
                            $('#ASID').combobox({
                                onSelect: function (fai) {
                                    $('#Specs').combobox('clear');
                                    $('#Specs').combobox({
                                        url: 'BasicApi.aspx?method=QueryAllSpesc&typeid=' + fai.TypeID + '&houseID=' + $("#HouseID").combobox('getValue'),
                                        onSelect: function (a) {
                                            $('#ProductID').val(a.ProductID);
                                        },
                                    });
                                    //var url = 'BasicApi.aspx?method=QueryAllSpesc&typeid=' + fai.TypeID + '&houseID=' + $("#HouseID").combobox('getValue');
                                    //$('#Specs').combobox('reload', url);
                                }
                            });
                        }
                });
                //let TypeID = "";
                //if (rows[0].ProductID != null) {
                   
                //    $.ajax({
                //        url: 'BasicApi.aspx?method=QueryAllTypeIDToProductID&productID=' + rows[0].ProductID,
                //        type: 'post',
                //        dataType: 'json',
                //        success: function (text) {
                //            console.log(text, "产品信息")
                //            TypeID = text[0].TypeID;
                //            console.log(TypeID, "产品信息")
                //            $('#APID').combobox('setValue', TypeID);
                //            //$('#APID').combobox('setValue', text[0].TypeID);
                //            //$('#ASID').combobox('setValue', text[0].ProductID);
                //            //$('#Specs').combobox('setValue', text[0].Specs);
                //        }
                //    });
                //}
                
                //console.log(TypeID, "产品信息11111")
               

                //let apID = "";
                //$('#APID').combobox({
                //    url: 'BasicApi.aspx?method=QueryALLOneProductType', valueField: 'TypeID', textField: 'TypeName',
                //    onSelect: function (rec) {
                //        $('#ASID').combobox('clear');
                //        $('#ASID').combobox({
                //            url: 'BasicApi.aspx?method=QueryALLOneProductType&PID=' + rec.TypeID, valueField: 'TypeID', textField: 'TypeName', AddField: 'PinyinName',
                //            filter: function (q, row) {
                //                var opts = $(this).combobox('options');
                //                return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                //            },
                //        });
                //        //轮胎规格
                //        $('#ASID').combobox({
                //            onSelect: function (fai) {
                //                $('#Specs').combobox('clear');
                //                $('#Specs').combobox({
                //                    url: 'BasicApi.aspx?method=QueryAllSpesc&typeid=' + fai.TypeID + '&houseID=' + $("#HouseID").combobox('getValue'),
                //                    onSelect: function (a) {
                //                        $('#ProductID').val(a.ProductID);
                //                    },
                //                });
                //                //var url = 'BasicApi.aspx?method=QueryAllSpesc&typeid=' + fai.TypeID + '&houseID=' + $("#HouseID").combobox('getValue');
                //                //$('#Specs').combobox('reload', url);
                //            }
                //        });
                //    }
                //});

                //仓库信息
                $('#HouseID').combobox({
                    url: '../FormService.aspx?method=CargoPermisionHouse', valueField: 'SettleHouseID', textField: 'SettleHouseName',
                    onLoadSuccess: function (rec) {
                        console.log(rec, rows[0].HouseID, 1212121212121212)
                        if (rec.length >= 1) {
                            var filteredRec = rec.filter(function (item) {
                                console.log(item.SettleHouseID, 1)
                                return item.SettleHouseID == rows[0].HouseID;
                            });
                            console.log(filteredRec, 1212121212121212)
                            // 构建数据数组
                            var clientTypes = filteredRec[0].ClientTypeID.split(',').map(function (id, index) {
                                return {
                                    value: id,
                                    text: filteredRec[0].ClientTypeName.split(',')[index]
                                };
                            });

                            console.log('Selected dt:', clientTypes);
                            // 加载到combobox中
                            $('#AProductID').combobox({
                                valueField: 'value',
                                textField: 'text',
                                data: clientTypes
                            });
                            $('#AProductID').combobox('setValue', rows[0].TypeID);
                            $('#UseTypeID').combobox({
                                valueField: 'value',
                                textField: 'text',
                                data: clientTypes,
                                multiple: true
                            });
                            $("#UseTypeID").combobox('clear');
                            if (rows[0].UseTypeID.length != 0) {
                            var arr = rows[0].UseTypeID.split(',');
                            var valueArr = new Array();
                            for (var i = 0; i < arr.length; i++) {
                                valueArr.push(arr[i]);
                            }
                            $("#UseTypeID").combobox("setValues", valueArr);
                            }
                        }
                       
                       
                    },
                    onSelect: function (rec) {
                        console.log(211212121212122)
                       
                        // 构建数据数组
                        var clientTypes = rec.ClientTypeID.split(',').map(function (id, index) {
                            return {
                                value: id,
                                text: rec.ClientTypeName.split(',')[index]
                            };
                        });
                        console.log('Selected dt:', clientTypes);
                        // 加载到combobox中
                        $('#AProductID').combobox({
                            valueField: 'value',
                            textField: 'text',
                            data: clientTypes
                        });
                        //$('#UseTypeID').combobox({
                        //    valueField: 'value',
                        //    textField: 'text',
                        //    data: clientTypes,
                        //    multiple: true
                        //});                       
                    }
                });


                
                
                $('#HouseID').combobox('setValue', rows[0].HouseID);
                



                $('#HouseID').combobox('textbox').bind('focus', function () { $('#HouseID').combobox('showPanel'); });
                $('#AProductID').combobox('textbox').bind('focus', function () { $('#AProductID').combobox('showPanel'); });

                $('#AStartDate').datebox('textbox').bind('focus', function () { $('#AStartDate').datebox('showPanel'); });
                $('#AEndDate').datebox('textbox').bind('focus', function () { $('#AEndDate').datebox('showPanel'); });

            }
        }

        //删除规则
        function DelItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Supplier.Common.GetSystemNameAndVersion()%>', '确定删除该规则？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'BasicApi.aspx?method=DelRuleBank',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            console.log(text,1111111111)
                            if (text.Result == true) {
                                $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');

                                //$('#dg').dialog('close');
                         $('#dg').datagrid('reload');
                     }
                     else {
                         $.messager.alert('<%= Supplier.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                     }
                 }
             });
         }
     });
        }
    </script>
</asp:Content>
