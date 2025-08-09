<%@ Page Title="上游公司管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="upClientManager.aspx.cs" Inherits="Cargo.Client.upClientManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        //页面加载时执行
        window.onload = function () {
            adjustment();
        };
        $(window).resize(function () {
            adjustment();
        });
        function adjustment() {
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true);
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
        };
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
                fitColumns: true, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                  {
                      title: '', field: 'ID', checkbox: true, width: '30px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '公司全称', field: 'UpClientName', width: '10%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '公司简称', field: 'UpClientShortName', width: '10%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '客户类型', field: 'ClientType', width: '5%', formatter: function (val, row, index) {
                          if (val == "0") { return "<span title='普通客户'>普通客户</span>"; }
                          else if (val == "1") { return "<span title='合同客户'>合同客户</span>"; }
                      }
                  },
                  {
                      title: '公司地址', field: 'Address', width: '15%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '所在仓库', field: 'HouseName', width: '10%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '负责人', field: 'Boss', width: '10%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '手机号码', field: 'Cellphone', width: '10%', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '状态', field: 'DelFlag', width: '10%', formatter: function (val, row, index) {
                          if (val == "0") { return "<span title='启用'>启用</span>"; } else if (val == "1") { return "<span title='注销'>注销</span>"; } else { return ""; }
                      }
                  },
                  { title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { editItem(); },
                rowStyler: function (index, row) {
                }
            });


            $('#dgClientDep').datagrid({
                width: '100%',
                height: '440px',
                title: '', //标题内容
                pagination: true, //分页是否显示
                pageSize: 20, //每页多少条
                pageList: [20, 50],
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#ClientDepbar',
                columns: [[
                  { title: '', field: 'ID', checkbox: true, width: '30px' },
                  { title: '业务名称', field: 'DepName', width: '20%' },
                  { title: '小胎库存结算标准', field: 'SmallStockCheckFee', width: '15%' },
                  { title: '大胎库存结算标准', field: 'BigStockCheckFee', width: '15%' },
                  { title: '操作人员', field: 'OP_Name', width: '20%' },
                  { title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter }
                ]],
                onClickRow: function (index, row) {
                    $('#dgClientDep').datagrid('clearSelections');
                    $('#dgClientDep').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) {
                    editClientDep();
                }
            })
            //省市区三级联动
            $('#eProvince').combobox({
                url: '../House/houseApi.aspx?method=QueryCityData&PID=0',
                valueField: 'ID', textField: 'City',
                onSelect: function (rec) {
                    $('#eCity').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryCityData&PID=' + rec.ID;
                    $('#eCity').combobox('reload', url);
                    //一级仓库
                    $('#eCity').combobox({
                        onSelect: function (fai) {
                            $('#eCountry').combobox('clear');
                            var url = '../House/houseApi.aspx?method=QueryCityData&PID=' + fai.ID;
                            $('#eCountry').combobox('reload', url);
                        }
                    });
                }
            });
            //所在仓库
            $('#HouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });
            //所在仓库
            $('#eHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });
            //列表回车响应查询
            $("#saPanelTab").keydown(function (e) { if (e.keyCode == 13) { dosearch(); } });
            //编辑回车响应保存
            //$("#editTab").keydown(function (e) { if (e.keyCode == 13) { saveItem(); } });

        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'clientApi.aspx?method=QueryUpClientData';
            $('#dg').datagrid('load', {
                UpClientName: $("#UpClientName").val(),
                UpClientShortName: $("#UpClientShortName").val(),
                ClientType: $('#ClientType').combobox('getValue'),
                Boss: $('#Boss').val(),
                Cellphone: $("#Cellphone").val(),
                HouseID: $('#HouseID').combobox('getValue'),
                DelFlag: $('#DelFlag').combobox('getValue')
            });
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--此div用于在界面未完全加载样式前显示内容--%>
    <div id='Loading' style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 100%; height: 100%; background: white; text-align: center; padding: 0px 0px; display: table;">
        <div style="display: table-cell; vertical-align: middle">
            <h1><font size="9">页面加载中……</font></h1>
        </div>
    </div>
    <%--此div用于在界面未完全加载样式前显示内容--%>

    <%--此div用于显示查询条件--%>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table id="saPanelTab">
            <tr>
                <td style="text-align: right;">公司全称:
                </td>
                <td>
                    <input id="UpClientName" class="easyui-textbox" data-options="prompt:'请输入公司全称'" style="width: 100px" />
                </td>
                <td style="text-align: right;">公司简称:
                </td>
                <td>
                    <input id="UpClientShortName" class="easyui-textbox" data-options="prompt:'请输入公司简称'" style="width: 100px" />
                </td>
                <td style="text-align: right;">客户类型:
                </td>
                <td>
                    <select class="easyui-combobox" id="ClientType" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="-1">全部</option>
                        <option value="0">普通客户</option>
                        <option value="1">合同客户</option>
                    </select>
                </td>
                <td style="text-align: right;">负责人:
                </td>
                <td>
                    <input id="Boss" class="easyui-textbox" data-options="prompt:'请输入负责人'" style="width: 100px" />
                </td>
                <td style="text-align: right;">手机号码:
                </td>
                <td>
                    <input id="Cellphone" class="easyui-textbox" data-options="prompt:'请输入手机号码'" style="width: 100px" />
                </td>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="HouseID" name="HouseID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'HouseID',textField:'HouseName',editable:false" />
                </td>
                <td style="text-align: right;">状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="DelFlag" style="width: 80px;" panelheight="auto" editable="false">
                        <option value="0">启用</option>
                        <option value="1">注销</option>
                        <option value="-1">全部</option>
                    </select>
                </td>
            </tr>
        </table>
    </div>
    <%--此div用于显示查询条件--%>
    <table id="dg" class="easyui-datagrid">
    </table>
    <%--此div用于显示按钮操作--%>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">&nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-tag_blue_edit" plain="false" onclick="ClientDep()">&nbsp;业务管理&nbsp;</a>&nbsp;
    </div>
    <%--此div用于显示按钮操作--%>

    <%--此div用于新增/编辑数据--%>
    <div id="dlgedit" class="easyui-dialog" style="width: 600px; height: 365px; padding: 0px 0px" closed="true" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" name="UpClientID" />
            <table id="saPanel">
                <tr>
                    <td style="vertical-align: top">
                        <table>
                            <tr>
                                <td style="text-align: right;">所属仓库:
                                </td>
                                <td>
                                    <input id="eHouseID" name="HouseID" class="easyui-combobox" style="width: 150px;" data-options="valueField:'HouseID',textField:'HouseName',editable:false,required:true" />
                                </td>
                                <td style="text-align: right;">状态标识:
                                </td>
                                <td>
                                    <select class="easyui-combobox" id="eDelFlag" name="DelFlag" style="width: 150px;" panelheight="auto" required="true">
                                        <option value="0">启用</option>
                                        <option value="1">停用</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">公司名称:
                                </td>
                                <td>
                                    <input name="UpClientName" id="eUpClientName" class="easyui-textbox" data-options="min:0,precision:0,required:true,prompt:'请输入公司名称'" style="width: 150px;" validtype="length[0,20]" />
                                </td>
                                <td style="text-align: right;">公司简称:
                                </td>
                                <td>
                                    <input name="UpClientShortName" id="eUpClientShortName" class="easyui-textbox" data-options="min:0,precision:0,required:false,prompt:'请输入公司简称'" style="width: 150px;" validtype="length[0,20]" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">客户类型:
                                </td>
                                <td>
                                    <select class="easyui-combobox" id="eClientType" name="ClientType" style="width: 150px;" panelheight="auto" data-options="required:true" editable="false">
                                        <option value="0">普通客户</option>
                                        <option value="1">合同客户</option>
                                    </select>
                                </td>
                                <td style="text-align: right;">负责人:
                                </td>
                                <td>
                                    <input name="Boss" id="eBoss" class="easyui-textbox" data-options="min:0,precision:0,required:false,prompt:'请输入负责人'" style="width: 150px;" validtype="length[0,20]" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">手机号码:
                                </td>
                                <td>
                                    <input name="Cellphone" id="eCellphone" class="easyui-numberbox" data-options="prompt:'请输入手机号码'" style="width: 120px"/>
                                </td>
                                <td style="text-align: right;">公司电话:
                                </td>
                                <td>
                                    <input name="Telephone" id="eTelephone" class="easyui-textbox" data-options="min:0,precision:0,required:false,prompt:'请输入公司电话'" style="width: 150px;" validtype="length[0,20]" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">公司地址:
                                </td>
                                <td colspan="3">
                                    <input id="eProvince" name="Province" class="easyui-combobox" style="width: 70px;" />&nbsp;省
                                    <input id="eCity" name="City" class="easyui-combobox" style="width: 70px;" data-options="valueField:'ID',textField:'City'" />&nbsp;市
                                    <input id="eCountry" name="Country" class="easyui-combobox" style="width: 70px;" data-options="valueField:'ID',textField:'City'" />&nbsp;区&nbsp;
                                    <input name="Address" id="eAddress" class="easyui-textbox" style="width: 165px;" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">备注:
                                </td>
                                <td colspan="3">
                                    <textarea id="Remark" rows="4" name="Remark" style="width: 440px;"></textarea>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" id="btnSave" iconcls="icon-ok" onclick="saveItem()">&nbsp;保&nbsp;存&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgedit').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <%--此div用于新增/编辑数据--%>

    <%--上游客户部门--%>
    <div id="dlgClientDep" class="easyui-dialog" style="width: 700px; height: 550px; padding: 0px 0px" closed="true" buttons="#dlgClientDep-buttons">
        <div id="saPanel">
            <input type="hidden" name="hUpClientID" id="hUpClientID" />
            <table>
                <tr>
                    <td style="text-align: right;">业务名称:
                    </td>
                    <td>
                        <input id="DepName" class="easyui-numberbox" data-options="prompt:'请输入业务名称'" style="width: 100px" />
                    </td>
                </tr>
            </table>
        </div>
        <table id="dgClientDep" class="easyui-datagrid">
        </table>
        <div id="ClientDepbar">
            <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="QueryClientDep()">&nbsp;查询&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addClientDep()">&nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="editClientDep()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelClientDep()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
        </div>
    </div>
    <div id="dlgClientDep-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgClientDep').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <%--上游客户部门--%>

    <%--此div用于新增/编辑数据--%>
    <div id="dlgeditDep" class="easyui-dialog" style="width: 350px; height: 180px; padding: 0px 0px" closed="true" buttons="#dlgDep-buttons">
        <form id="fmDep" class="easyui-form" method="post">
            <input type="hidden" name="UpClientDepID" id="hUpClientDepID" />
            <table id="saPanel">
                <tr>
                    <td style="vertical-align: top">
                        <table>
                            <tr>
                                <td style="text-align: right;">业务名称:
                                </td>
                                <td>
                                    <input name="DepName" id="eDepName" class="easyui-textbox" data-options="min:0,precision:0,required:true,prompt:'请输入业务名称'" style="width: 150px;" validtype="length[0,20]" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">小胎结算标准:
                                </td>
                                <td>
                                    <input name="SmallStockCheckFee" id="eSmallStockCheckFee" class="easyui-numberbox" data-options="min:0,precision:2,required:false,prompt:'请输入库存结算标准'" style="width: 150px;">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">大胎结算标准:
                                </td>
                                <td>
                                    <input name="BigStockCheckFee" id="eBigStockCheckFee" class="easyui-numberbox" data-options="min:0,precision:2,required:false,prompt:'请输入库存结算标准'" style="width: 150px;">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlgDep-buttons">
        <a href="#" class="easyui-linkbutton" id="btnSave" iconcls="icon-ok" onclick="saveClientDep()">&nbsp;保&nbsp;存&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgeditDep').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <%--此div用于新增/编辑数据--%>
    <script type="text/javascript">
        //新增
        function addItem() {
            $('#dlgedit').form('clear');
            $('#dlgedit').dialog('open').dialog('setTitle', '新增上游客户数据');
            $('#eDelFlag').combobox('select', '0');
            $('#eClientType').combobox('select', '0');
        }
        //修改
        function editItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (rows.length > 1) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择一条要修改的数据！', 'warning');
                return;
            }
            if (rows[0]) {
                $('#fm').form('clear');
                $('#dlgedit').dialog('open').dialog('setTitle', '修改上游客户数据');
                $('#fm').form('load', rows[0]);
            }
        }
        //保存数据
        function saveItem() {
            $.messager.progress({ msg: '请稍后,正在保存中...' });
            $('#fm').form('submit', {
                url: 'clientApi.aspx?method=SaveUpClientData',
                onSubmit: function (param) {
                    param.eProvince = $('#eProvince').combobox('getText');
                    param.eCity = $('#eCity').combobox('getText');
                    param.eCountry = $('#eCountry').combobox('getText');
                    var check = $(this).form('enableValidation').form('validate');
                    if (!check) { $.messager.progress("close"); }
                    return check;
                },
                success: function (msg) {
                    $.messager.progress("close");
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        if (result.Message == "成功") {
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        } else {
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', result.Message, 'info');
                        }
                        $('#dlgedit').dialog('close');
                        dosearch();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
        //删除数据
        function DelItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            };
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'clientApi.aspx?method=DelUpClientData',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                $('#dg').datagrid('reload');
                                $('#dg').datagrid('unselectAll');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        };
        function ClientDep() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择上游客户数据！', 'warning');
                return;
            }
            if (rows.length > 1) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择一条上游客户数据！', 'warning');
                return;
            }
            if (rows[0]) {
                $('#dgClientDep').datagrid('loadData', { total: 0, rows: [] });
                $('#dlgClientDep').dialog('open').dialog('setTitle', '业务管理');
                $('#hUpClientID').val(rows[0].UpClientID);
                QueryClientDep();
            }
        };

        function QueryClientDep() {
            var mOpts = $('#dgClientDep').datagrid('options');
            mOpts.url = 'clientApi.aspx?method=QueryClientDep';
            $('#dgClientDep').datagrid('load', {
                UpClientID: $('#hUpClientID').val(),
                DepName: $('#DepName').val()
            });
        }
        //新增上游客户
        function addClientDep() {
            $('#dlgeditDep').form('clear');
            $('#dlgeditDep').dialog('open').dialog('setTitle', '业务数据');
        };
        //修改
        function editClientDep() {
            var rows = $('#dgClientDep').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (rows.length > 1) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择一条要修改的数据！', 'warning');
                return;
            }
            if (rows[0]) {
                $('#dlgeditDep').form('clear');
                $('#dlgeditDep').dialog('open').dialog('setTitle', '业务数据');
                $('#dlgeditDep').form('load', rows[0]);
                $('#hUpClientDepID').val(rows[0].ID);
            }
        }
        //保存数据
        function saveClientDep() {
            $.messager.progress({ msg: '请稍后,正在保存中...' });
            $('#fmDep').form('submit', {
                url: 'clientApi.aspx?method=SaveUpClientDepData',
                onSubmit: function (param) {
                    param.eUpClientID = $('#hUpClientID').val();
                    var check = $(this).form('enableValidation').form('validate');
                    if (!check) { $.messager.progress("close"); }
                    return check;
                },
                success: function (msg) {
                    $.messager.progress("close");
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        if (result.Message == "成功") {
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        } else {
                            $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', result.Message, 'info');
                        }
                        $('#dlgeditDep').dialog('close');
                        QueryClientDep();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
        //删除数据
        function DelClientDep() {
            var rows = $('#dgClientDep').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            };
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'clientApi.aspx?method=DelUpClientDepData',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                $('#dgClientDep').datagrid('reload');
                                $('#dgClientDep').datagrid('unselectAll');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        };
    </script>
</asp:Content>
