<%@ Page Title="微信企业号用户管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="qyUser.aspx.cs" Inherits="Cargo.QY.qyUser" %>

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
                pageSize: 20, //每页多少条
                pageList: [20, 50],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'UserID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    { title: '', field: '', checkbox: true, width: '30px' },
                    {
                        title: '系统ID', field: 'UserID', width: '60px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '微信名', field: 'WxName', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: 'OPENID', field: 'OpenID', width: '210px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '手机号码', field: 'CellPhone', width: '90px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '主部门', field: 'MainDepartmentName', width: '100px', formatter: function (value) {
                            if (value != null) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            }
                        }
                    },
                    {
                        title: '所属部门', field: 'DepartName', width: '200px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    { title: '所属仓库ID', field: 'HouseID', width: '80px', hidden: true },
                    {
                        title: '所属仓库', field: 'HouseName', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '审批仓库', field: 'CheckHouseName', width: '200px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '审批角色', field: 'CheckRole', width: '200px', formatter: function (val, row, index) {
                            if (val != null) {
                                var checkRoles = val.split(',');
                                var CheckRoleName = "";
                                for (var i = 0; i < checkRoles.length; i++) {
                                    if (checkRoles[i] == "1") { CheckRoleName += "内勤主管,"; } else if (checkRoles[i] == "2") { CheckRoleName += "财务主管,"; } else if (checkRoles[i] == "3") { CheckRoleName += "分公司领导,"; } else if (checkRoles[i] == "4") { CheckRoleName += "总经理,"; } else if (checkRoles[i] == "9") { CheckRoleName += "OE部门经理,"; } else if (checkRoles[i] == "5") { CheckRoleName += "董事长特助,"; } else if (checkRoles[i] == "6") { CheckRoleName += "董事长,"; } else if (checkRoles[i] == "7") { CheckRoleName += "分公司财务,"; } else if (checkRoles[i] == "10") { CheckRoleName += "OE财务主管,"; } else if (checkRoles[i] == "11") { CheckRoleName += "OE总经理,"; } else { CheckRoleName += ","; }
                                }
                                CheckRoleName = CheckRoleName.substr(0, CheckRoleName.length - 1);
                                return CheckRoleName;
                            }
                        }
                    },
                    { title: '标签', field: 'TagName', width: '150px' },
                    //{
                    //    title: '性别', field: 'Gender', width: '60px',
                    //    formatter: function (val, row, index) {
                    //        if (val == "0") { return "男"; }
                    //        else if (val == "1") { return "女"; }
                    //        else { return "未确定"; }
                    //    }
                    //},
                    //{ title: '邮箱', field: 'Email', width: '80px' },
                    //{ title: '微信ID', field: 'weixinID', width: '80px' },
                    { title: '头像', field: 'AvatarBig', width: '50px', formatter: imgFormatter }
                ]],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });
            $('#Depart').combobox('textbox').bind('focus', function () { $('#Depart').combobox('showPanel'); });
            //$('#Department').combobox('textbox').bind('focus', function () { $('#Department').combobox('showPanel'); });

            //所属部门查询条件响应删除键清空选项
            $("#Depart").combotree('textbox').bind('keydown', function (e) { if (e.keyCode == 8) { $('#Depart').combotree("clear"); } })

            $('#CheckHouseID').combobox('reload', '../House/houseApi.aspx?method=CargoPermisionHouse');

        });
        //图片添加路径  
        function imgFormatter(value, row, index) {
            if ('' != value && null != value) {
                var rvalue = "";
                rvalue += "<img onclick=download(\"" + value + "\") style='width:30px; height:30px;margin-left:3px;' src='" + value + "' title='点击查看图片'/>";
                return rvalue;
            }
        }
        function download(img) {
            var simg = img;
            $('#dgViewImg').dialog('open').dialog('setTitle', '预览');
            $("#simg").attr("src", simg);

        }
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'wxQYServices.aspx?method=queryUser';
            $('#dg').datagrid('load', {
                WxName: $('#WxName').textbox('getValue'),
                Depart: $("#Depart").combobox('getValue')
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
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table>
            <tr>
                <td style="text-align: right;">微信名称:
                </td>
                <td>
                    <input id="WxName" class="easyui-textbox" data-options="prompt:'请输入微信名称'" style="width: 150px">
                </td>
                <td style="text-align: right;">所属部门:
                </td>
                <td>
                    <%--<input id="Depart" class="easyui-combobox" style="width: 150px;"
                        panelheight="auto" data-options=" url: 'wxQYServices.aspx?method=QueryDepartList',
                valueField: 'Id', textField: 'Name'" />--%>
                    <input id="Depart" class="easyui-combotree" data-options="url:'wxQYServices.aspx?method=QueryAllOrganize',editable:false" style="width: 200px;" />
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
                        iconcls="icon-arrow_refresh" plain="false" onclick="SyncUser()">&nbsp;同&nbsp;步&nbsp;</a>&nbsp;&nbsp;<%--<a href="#" class="easyui-linkbutton"
                            iconcls="icon-arrow_refresh" plain="false" onclick="send()">&nbsp;推送&nbsp;</a>--%>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 400px; height: 350px; padding: 5px 5px"
        closed="true" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" name="OldTag" id="OldTag" />
            <table>
                <tr>
                    <td style="text-align: right;">系统ID:
                    </td>
                    <td>
                        <input name="UserID" class="easyui-textbox" data-options="prompt:'请输入系统ID',required:true"
                            style="width: 250px;">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">微信名称:
                    </td>
                    <td>
                        <input name="WxName" class="easyui-textbox" data-options="prompt:'请输入微信名字',required:true"
                            style="width: 250px;">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">所属部门:
                    </td>
                    <td>
                        <%--<input name="Department" class="easyui-combobox" style="width: 250px;" data-options=" url: 'wxQYServices.aspx?method=QueryDepartList',valueField: 'Id', textField: 'Name',required:true" panelheight="auto" />--%>
                        <input name="Department" id="Department" class="easyui-combotree" data-options="url:'wxQYServices.aspx?method=QueryAllOrganize',multiple:'true',editable:false,required:true" style="width: 250px;" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">手机号码:
                    </td>
                    <td>
                        <input name="CellPhone" class="easyui-textbox" data-options="prompt:'请输入手机号码',required:true"
                            style="width: 250px;">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">标签:
                    </td>
                    <td>
                        <input name="Tag" class="easyui-combobox" style="width: 250px;" data-options=" url: 'wxQYServices.aspx?method=QueryTagList',valueField: 'Id', textField: 'Name',multiple:true "
                            panelheight="auto" />
                    </td>
                </tr>
                <%-- <tr>
                    <td style="text-align: right;">邮箱地址:
                    </td>
                    <td>
                        <input name="Email" class="easyui-textbox" data-options="prompt:'请输入邮箱地址'"
                            style="width: 250px;">
                    </td>
                </tr>--%>
                <tr>
                    <td style="text-align: right;">职位名称:
                    </td>
                    <td>
                        <input name="Position" class="easyui-textbox" data-options="prompt:'请输入职位名称'"
                            style="width: 250px;">
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">所属仓库:
                    </td>
                    <td>
                        <input id="HouseID" name="HouseID" class="easyui-combobox" style="width: 250px;" data-options="valueField:'HouseID',textField:'HouseName',editable:false" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right;">审批角色:
                    </td>
                    <td>
                        <select name="CheckRole" class="easyui-combobox" id="CheckRole" multiple="true" style="width: 250px;" panelheight="auto">
                            <option value="7">分公司财务</option>
                            <option value="3">分公司领导</option>
                            <option value="1">内勤主管</option>
                            <option value="2">财务主管</option>
                            <option value="10">OE财务主管</option>
                            <option value="9">OE部门经理</option>
                            <option value="4">总经理</option>
                            <option value="11">OE总经理</option>
                            <option value="5">董事长特助</option>
                            <option value="6">董事长</option>
                        </select>
                    </td>
                </tr>

                <tr>
                    <td style="text-align: right;">审批仓库:
                    </td>
                    <td>
                        <input name="CheckHouseID" class="easyui-combobox" id="CheckHouseID" multiple="true" style="width: 250px;" panelheight="auto" data-options="valueField:'HouseID',textField:'Name'"/>
                        <input name="CheckHouseName" id="CheckHouseName" type="hidden"/>
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()">&nbsp;&nbsp;保&nbsp;存&nbsp;&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;&nbsp;取&nbsp;消&nbsp;&nbsp;</a>
    </div>
    <div id="dgViewImg" class="easyui-dialog" closed="true">
        <img id="simg" style="width: 250px; height: 250px;" />
    </div>
    <script type="text/javascript">
        //推送消息
        function send() {
            $.ajax({
                url: 'wxQYServices.aspx?method=SendInfo',
                type: 'post', dataType: 'json', data: {},
                success: function (text) {
                    //var result = eval('(' + msg + ')');
                    if (text.Result == true) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '推送成功!', 'info');
                        dosearch();
                    }
                    else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                    }
                }
            });
        }
        $("#CheckHouseID").combobox({
            onChange: function () {
                var selectedValues = $('#CheckHouseID').combobox('getValues');
                console.log(selectedValues)
                var selectedTexts = '';
                for (var i = 0; i < selectedValues.length; i++) {
                    var data = $('#CheckHouseID').combobox('getData');
                    console.log(data)
                    for (var j = 0; j < data.length; j++) {
                        if (selectedValues[i] == data[j].HouseID) {
                            selectedTexts += data[j].Name + ', ';
                            break;
                        }
                    }
                }
                //alert(selectedTexts.slice(0, -2))
                $("#CheckHouseName").val(selectedTexts.slice(0, -2));
                //alert($("#CheckHouseName").val())
            }
        })
        //同步通讯录
        function SyncUser() {
            var rows = $('#dg').datagrid('getSelections');
            var msg = "确定同步？";
            if (rows == null || rows == "") {
                <%--$.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要同步的数据！', 'warning');
                return;--%>
                msg = "确认同步所有人员？";
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', msg, function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在同步中...' });
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'wxQYServices.aspx?method=SyncUser',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '同步成功!', 'info');
                                dosearch();
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }
        //新增用户信息
        function addItem() {
            $('#dlg').dialog('open').dialog('setTitle', '新增微信企业用户');
            $('#fm').form('clear');

        }
        //修改微信企业用户信息
        function editItem() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                var houseUrl = '../Order/orderApi.aspx?method=QueryALLHouse&type=edit';
                $('#HouseID').combobox('reload', houseUrl);
                $('#dlg').dialog('open').dialog('setTitle', '修改微信企业用户');
                $('#fm').form('load', row);
                $('#OldTag').val(row.Tag);
                var arr = row.Department.split(',');
                var valueArr = new Array();
                for (var i = 0; i < arr.length; i++) {
                    valueArr.push(arr[i]);
                }
                $("#ProductLineId").combotree("setValues", valueArr);
            }
        }
        //修改用户信息
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            if (row) {
                var houseUrl = '../Order/orderApi.aspx?method=QueryALLHouse&type=edit';
                $('#HouseID').combobox('reload', houseUrl);
                $('#dlg').dialog('open').dialog('setTitle', '修改微信企业用户');
                $('#fm').form('load', row);
                $('#OldTag').val(row.Tag);
                var arr = row.Department.split(',');
                var valueArr = new Array();
                for (var i = 0; i < arr.length; i++) {
                    valueArr.push(arr[i]);
                }
                $("#ProductLineId").combotree("setValues", valueArr);
            }
        }
        //删除用户信息
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
                        url: 'wxQYServices.aspx?method=DelQYUser',
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

        //保存信息
        function saveItem() {
            $('#fm').form('submit', {
                url: 'wxQYServices.aspx?method=SaveQYUser',
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
