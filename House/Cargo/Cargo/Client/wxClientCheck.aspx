<%@ Page Title="微信服务号用户管理审核" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="wxClientCheck.aspx.cs" Inherits="Cargo.Client.wxClientCheck" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../JS/Rotate/jQueryRotate.2.2.js" type="text/javascript"></script>
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
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    { title: '', field: 'ID', checkbox: true, width: '30px' },
                    { title: '用户姓名', field: 'Name', width: '80px' },
                    //{ title: '公司名称', field: 'CompanyName', width: '80px' },
                    { title: '微信头像', field: 'AvatarBig', width: '80px', formatter: imgFormatter },
                    { title: '微信名称', field: 'wxName', width: '100px' },
                    { title: '手机号码', field: 'Cellphone', width: '90px' },
                    { title: '所在省', field: 'Province', width: '60px' },
                    { title: '所在市', field: 'City', width: '60px' },
                    { title: '所在区', field: 'Country', width: '60px' },
                    { title: '公司名称', field: 'CompanyName', width: '150px' },
                    { title: '公司地址', field: 'Address', width: '300px' },
                    { title: '营业执照照片', field: 'BusLicenseImg', width: '80px', formatter: imgFormatter },
                    { title: '身份证正面照片', field: 'IDCardImg', width: '80px', formatter: imgFormatter },
                    { title: '身份证反面照片', field: 'IDCardBackImg', width: '80px', formatter: imgFormatter },
                    { title: '注册时间', field: 'RegisterDate', width: '130px', formatter: DateTimeFormatter },
                    { title: '绑定店代码', field: 'ClientNum', width: '60px' },
                    { title: '所属仓库', field: 'HouseName', width: '60px' },
                    { title: '绑定时间', field: 'BindDate', width: '130px', formatter: DateTimeFormatter },
                    { title: '拒审原因', field: 'DenyReason', width: '150px' },
                    {
                        title: '实名认证', field: 'IsCertific', width: '60px', formatter: function (val, row, index) {
                            if (val == "0") { return "否"; }
                            else if (val == "1") { return "是"; }
                            else { return "否"; }
                        }
                    },
                    {
                        title: '性别', field: 'Sex', width: '50px', formatter: function (val, row, index) {
                            if (val == "1") { return "男"; }
                            else if (val == "2") { return "女"; }
                            else { return "未知"; }
                        }
                    },
                    { title: 'OpenID', field: 'wxOpenID', width: '100px' }
                    //{ title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter }
                ]],
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            //所在仓库
            $('#HID').combobox({ url: '../House/houseApi.aspx?method=CargoPermisionHouse', valueField: 'HouseID', textField: 'Name' });
            $('#HID').combobox('textbox').bind('focus', function () { $('#HID').combobox('showPanel'); });
            $('#VHID').combobox({ url: '../House/houseApi.aspx?method=CargoPermisionHouse', valueField: 'HouseID', textField: 'Name' });
            $('#VHID').combobox('textbox').bind('focus', function () { $('#VHID').combobox('showPanel'); });

            
            var value2 = 0
            $("#simg").rotate({ bind: { click: function () { value2 += 90; $(this).rotate({ animateTo: value2 }) } } });

        });
        //图片添加路径  
        function imgFormatter(value, row, index) {
            if ('' != value && null != value) {
                var rvalue = "";
                rvalue += "<img onclick=downView(\"" + value + "\") style='width:66px; height:60px;margin-left:1px;' src='" + value + "' title='点击查看图片'/>";
                return rvalue;
            }
        }
        function downView(img) {
            var simg = img;
            $('#dgViewImg').dialog('open').dialog('setTitle', '预览');
            $("#simg").attr("src", simg);
        }
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'clientApi.aspx?method=QueryWxClientCheckData';
            $('#dg').datagrid('load', {
                Name: $('#QName').val(),
                CompanyName: $("#QCompanyName").val(),
                wxName: $("#QwxName").val(),
                Cellphone: $("#QCellphone").val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                HID: $("#HID").combobox('getValue'),
                ClientNum: $("#QClientNum").val(),
                dFlag: $("#dFlag").combobox('getValue')
            });
            adjustment();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id='Loading' style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 98%; height: 100%; background: white; text-align: center; padding: 5px 10px; display: table;">
        <div style="display: table-cell; vertical-align: middle">
            <h1><font size="9">页面加载中……</font></h1>
        </div>
    </div>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'">
        <table>
            <tr>
                <td style="text-align: right;">用户姓名:
                </td>
                <td>
                    <input id="QName" class="easyui-textbox" data-options="prompt:'请输入用户姓名'" style="width: 100px">
                </td>

                <td style="text-align: right;">微信名称:
                </td>
                <td>
                    <input id="QwxName" class="easyui-textbox" data-options="prompt:'请输入微信名称'" style="width: 100px">
                </td>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="HID" class="easyui-combobox" style="width: 100px;" />
                </td>
                <td style="text-align: right;">注册时间:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">公司名称:
                </td>
                <td>
                    <input id="QCompanyName" class="easyui-textbox" data-options="prompt:'请输入公司名称'" style="width: 100px">
                </td>
                <td style="text-align: right;">手机号码:
                </td>
                <td>
                    <input id="QCellphone" class="easyui-numberbox" data-options="prompt:'请输入手机号码'" style="width: 100px">
                </td>
                <td style="text-align: right;">客户编码:
                </td>
                <td>
                    <input id="QClientNum" class="easyui-numberbox" data-options="prompt:'请输入客户编码'" style="width: 100px">
                </td>
                <td style="text-align: right;">是否绑定:
                </td>
                <td>
                    <select class="easyui-combobox" id="dFlag" style="width: 100px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">未绑定</option>
                        <option value="1">已绑定</option>
                    </select>
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-link" plain="false" onclick="bindClient()">&nbsp;绑定客户编码&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-link_break" plain="false" onclick="UnbindClient()">&nbsp;解绑客户编码&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-lock_open" plain="false" onclick="DenyClient()">&nbsp;拒&nbsp;审&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-application_put"
            plain="false" onclick="AwbExport()">&nbsp;导&nbsp;出&nbsp;</a>
        <form runat="server" id="fm1">
            <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
        </form>
    </div>

    <%-- 绑定店代码 --%>
    <div id="dlgViolate" class="easyui-dialog" style="width: 800px; height: 530px; padding: 0px" closed="true" buttons="#dlgViolate-buttons">
        <input type="hidden" id="VID" />
        <input type="hidden" id="WXName" />
        <input type="hidden" id="wxOpenID" />
        <div id="saPanel" class="easyui-panel" title="">
            <table>
                <tr>
                    <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="VHID" class="easyui-combobox" style="width: 100px;" />
                </td>
                    <td style="text-align: right;">客户姓名:
                    </td>
                    <td>
                        <input id="VName" class="easyui-textbox" style="width: 100px" />
                    </td>
                    <td style="text-align: right;">手机号码:
                    </td>
                    <td>
                        <input id="VCellphone" class="easyui-textbox" style="width: 100px" />
                    </td>

                    <td><a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="QueryViolate()">&nbsp;查询客户数据&nbsp;</a></td>
                </tr>
            </table>
        </div>
        <table id="dgViolate" class="easyui-datagrid">
        </table>
    </div>
    <div id="dlgViolate-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-link" plain="false" onclick="saveBind()">&nbsp;绑&nbsp;定&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgViolate').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <%-- 绑定店代码 --%>

    <%-- 拒审 --%>
    <div id="dlgDeny" class="easyui-dialog" style="width: 400px; height: 200px; padding: 1px 1px" closed="true" buttons="#dlgDeny-buttons">
        <form id="fmDeny" class="easyui-form" method="post">
            <table>
                <tr>
                    <td>拒审原因</td>
                </tr>
                <tr>
                    <td>
                        <textarea id="DenyReason" rows="5" name="DenyReason" style="width: 370px;"></textarea>
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlgDeny-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" plain="false" onclick="saveDeny()">&nbsp;保&nbsp;存&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgDeny').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <%-- 拒审 --%>
    <div id="dgViewImg" class="easyui-dialog" closed="true" style="width: 1000px; height: 600px; overflow: hidden; display: flex; justify-content: center; align-items: center;">
        <img id="simg" style="max-width: 100%; max-height: 170%;" />
    </div>
    <script type="text/javascript">
        //解绑店代码
        function UnbindClient() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要解绑的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定解绑？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'clientApi.aspx?method=saveWxUserUnBind',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '解绑成功!', 'info');
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
        //导出数据
        function AwbExport() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            var key = new Array();
            key[0] = $('#QName').val();
            key[1] = $("#QCompanyName").val();
            key[2] = $("#QwxName").val();
            key[3] = $("#QCellphone").val();
            key[4] = $('#StartDate').datebox('getValue');
            key[5] = $('#EndDate').datebox('getValue');
            key[6] = $("#HID").combobox('getValue');
            key[7] = $("#QClientNum").val();
            key[8] = $("#dFlag").combobox('getValue');

            $.ajax({
                url: "clientApi.aspx?method=QueryWxClientCheckDataForExport&key=" + escape(key.toString()),
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click(); }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }
        //保存拒审的内容
        function saveDeny() {
            var DenyReason = $('#DenyReason').val();
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要拒审的数据！', 'warning'); return; }
            var json = JSON.stringify([row])

            $.ajax({
                url: "clientApi.aspx?method=saveDenyReason&reason=" + escape(DenyReason),
                type: 'post', dataType: 'json', data: { data: json },
                success: function (text) {
                    if (text.Result == true) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '拒审成功!', 'info');
                        $('#dlgDeny').dialog('close'); 	// close the dialog
                        $('#dg').datagrid('reload');
                    }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning'); }
                }
            });
        }

        ///拒审
        function DenyClient() {
            var row = $('#dg').datagrid('getSelected');
            if (row.ClientNum != "0") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '该用户已绑定店代码！', 'warning'); return;
            }
            if (row) {
                $('#dlgDeny').dialog('open').dialog('setTitle', '请填写拒审原因');
                $('#DenyReason').val('');
            }
        }

        ///绑定客户数据
        function saveBind() {
            var row = $('#dgViolate').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要绑定的数据！', 'warning');
                 return;
             }
             $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定绑定？', function (r) {
                 if (r) {
                     var json = JSON.stringify([row])
                     $.ajax({
                         url: 'clientApi.aspx?method=saveWxUserBind&ID=' + escape($('#VID').val()) + '&Name=' + escape($('#WXName').val()) + '&wxOpenID=' + escape($('#wxOpenID').val()),
                         type: 'post', dataType: 'json', data: { data: json },
                         success: function (text) {
                             //var result = eval('(' + msg + ')');
                             if (text.Result == true) {
                                 $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '绑定成功!', 'info');
                                $('#dlgViolate').dialog('close');
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
        //查询客户数据
        function QueryViolate() {
            $('#dgViolate').datagrid('clearSelections');
            var gridOpts = $('#dgViolate').datagrid('options');
            gridOpts.url = 'clientApi.aspx?method=QueryCargoClient';
            $('#dgViolate').datagrid('load', {
                HID: $("#VHID").combobox('getValue'),
                Boss: $("#VName").val(),
                Cellphone: $("#VCellphone").val(),
                DelFlag: '0'
            });
        }

        //打开绑定店代码客户信息
        function bindClient() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要操作的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlgViolate').dialog('open').dialog('setTitle', '绑定客户：' + row.Name + ' 客户编码');
                $('#dgViolate').datagrid('loadData', { total: 0, rows: [] });
                $('#VCellphone').textbox('setValue', row.Cellphone);
                //$('#VName').textbox('setValue', row.Name);
                $('#VID').val(row.ID);
                $('#WXName').val(row.Name);
                $('#wxOpenID').val(row.wxOpenID);

                showdgViolate();
            }
        }

        //显示客户数据列表
        function showdgViolate() {
            $('#dgViolate').datagrid({
                width: '100%',
                height: '410px',
                title: '',
                //iconCls: 'icon-view',//标题图标
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: true, //行高是否自动
                //striped: true, //奇偶行使用不同背景色
                collapsible: false, //是否可折叠
                pagination: true, //分页是否显示
                rownumbers: true, //行号
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                //ctrlSelect:true,
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ClientID',
                url: null,
                columns: [[
                    { title: '', field: 'ClientID', checkbox: true, width: '30px' },
                    { title: '所属仓库', field: 'HouseName', width: '80px' },
                    { title: '客户编码', field: 'ClientNum', width: '70px' },
                    { title: '公司名称', field: 'ClientName', width: '80px' },
                    { title: '公司负责人', field: 'Boss', width: '80px' },
                    { title: '手机号码', field: 'Cellphone', width: '80px' },
                    { title: '公司地址', field: 'Address', width: '100px' },
                    { title: '预收款余额', field: 'PreReceiveMoney', width: '80px', align: 'right' },
                    { title: '返利款余额', field: 'RebateMoney', width: '80px', align: 'right' },
                    { title: '公司电话', field: 'Telephone', width: '100px' },
                    {
                        title: '客户类型', field: 'ClientType', width: '60px', formatter: function (val, row, index) {
                            if (val == "0") { return "普通客户"; }
                            else if (val == "1") { return "合同客户"; }
                            else if (val == "2") { return "VIP客户"; }
                            else { return "普通客户"; }
                        }
                    },
                    {
                        title: '启用状态', field: 'DelFlag', width: '60px', formatter: function (val, row, index) {
                            if (val == "0") { return "启用"; }
                            else if (val == "1") { return "停用"; }
                            else { return "启用"; }
                        }
                    },
                    { title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter }
                ]],
                onClickRow: function (index, row) {
                    $('#dgViolate').datagrid('clearSelections');
                    $('#dgViolate').datagrid('selectRow', index);
                },
            });
        }
    </script>
</asp:Content>
