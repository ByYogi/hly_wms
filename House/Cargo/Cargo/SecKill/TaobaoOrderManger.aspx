<%@ Page Title="秒杀菌淘宝订单管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TaobaoOrderManger.aspx.cs" Inherits="Cargo.SecKill.TaobaoOrderManger" %>

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
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                  { title: '', field: 'ID', checkbox: true, width: '30px' },
                  { title: '微信名', field: 'wxName', width: '120px' },
                  { title: '宝贝标题', field: 'Title', width: '100px' },
                  { title: '淘宝订单号', field: 'TaobaoID', width: '130px' },
                  { title: '买家昵称', field: 'buyer_nick', width: '80px' },
                  { title: '买家支付宝账号', field: 'buyer_alipay', width: '80px' },
                  { title: '购买数量', field: 'num', width: '60px' },
                  { title: '实付金额', field: 'payment', width: '70px' },
                  //{ title: '商品单价', field: 'price', width: '70px' },
                  { title: '订单总金额', field: 'total_fee', width: '80px' },
                  { title: '返利', field: 'Rebate', width: '70px' },
                  { title: '上级微信', field: 'OneWxName', width: '120px' },
                  { title: '上级提成', field: 'OneMoney', width: '70px' },
                  { title: '上上级微信', field: 'SecendWxName', width: '120px' },
                  { title: '上上级提成', field: 'SecendMoney', width: '70px' },
                  { title: '收货人姓名', field: 'receiver_name', width: '100px' },
                  { title: '手机号码', field: 'receiver_mobile', width: '80px' },
                  { title: '收货人电话', field: 'receiver_phone', width: '80px' },
                  { title: '收货人省份', field: 'receiver_state', width: '80px' },
                  { title: '收货人城市', field: 'receiver_city', width: '80px' },
                  { title: '收货人地区', field: 'receiver_district', width: '80px' },
                  { title: '详细地址', field: 'receiver_address', width: '150px' },
                  { title: '订单创建时间', field: 'created', width: '130px', formatter: DateTimeFormatter },
                  {
                      title: '订单状态', field: 'status', width: '100px',
                  },
                   { title: '物流公司', field: 'logicCompany', width: '100px' },
                  { title: '物流单号', field: 'logicno', width: '100px' },
                  { title: '付款时间', field: 'pay_time', width: '130px', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'secKillApi.aspx?method=QueryTaobaoOrderData';
            $('#dg').datagrid('load', {
                TaobaoID: $('#TaobaoID').val(),
                WxName: $("#WxName").val(),
                buyer_nick: $("#buyer_nick").val(),
                receiver_name: $("#receiver_name").val(),
                receiver_mobile: $("#receiver_mobile").val(),
                status: $("#status").val(),
                buyer_alipay: $("#buyer_alipay").val()
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
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width:100%">
        <table>
            <tr>
                <td style="text-align: right;">微信名:
                </td>
                <td>
                    <input id="WxName" class="easyui-textbox" data-options="prompt:'请输入微信名'" style="width: 80px">
                </td>
                <td style="text-align: right;">淘宝订单号:
                </td>
                <td>
                    <input id="TaobaoID" class="easyui-textbox" data-options="prompt:'请输入淘宝订单号'" style="width: 150px">
                </td>
                <td style="text-align: right;">买家昵称:
                </td>
                <td>
                    <input id="buyer_nick" class="easyui-textbox" data-options="prompt:'请输入买家昵称'" style="width: 100px">
                </td>
                <td style="text-align: right;">收货人姓名:
                </td>
                <td>
                    <input id="receiver_name" class="easyui-textbox" data-options="prompt:'请输入收货人姓名'" style="width: 100px">
                </td>

            </tr>
            <tr>
                <td colspan="2"></td>
                <td style="text-align: right;">支付宝账号:
                </td>
                <td>
                    <input id="buyer_alipay" class="easyui-textbox" data-options="prompt:'请输入支付宝账号'" style="width: 150px">
                </td>
                <td style="text-align: right;">订单状态:
                </td>
                <td>
                    <input id="status" class="easyui-textbox" data-options="prompt:'请输入订单状态'" style="width: 100px">
                </td>
                <td style="text-align: right;">收货人手机:
                </td>
                <td>
                    <input id="receiver_mobile" class="easyui-textbox" data-options="prompt:'请输入收货人手机号码'" style="width: 100px">
                </td>

            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="ImportTaobao()">
            &nbsp;导入淘宝订单&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
                iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 1000px; height: 600px; padding: 2px 2px"
        closed="true" buttons="#dlg-buttons">
        <table id="dgTb" class="easyui-datagrid">
        </table>
        <div id="dgTbtoolbar">
            <input type="file" id="fileT" name="file" style="width: 170px;" /><a href="#" id="btnload" class="easyui-linkbutton" iconcls="icon-out_cargo"
                plain="false" onclick="saveFile()">&nbsp;上传淘宝订单&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-ok"
                    plain="false" onclick="btnSaveData()">&nbsp;保存数据&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-cancel"
                        onclick="closeDlgStatus()">&nbsp;关&nbsp;闭&nbsp;</a>
        </div>
    </div>
    <script src="../JS/easy/js/ajaxfileupload.js"></script>
    <script type="text/javascript">

        //删除仓库信息
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
                        url: 'secKillApi.aspx?method=DelTaobaoOrder',
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
        //保存订单
        function btnSaveData() {
            var rows = $("#dgTb").datagrid('getData').rows;
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要保存的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定保存？', function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在保存中...' });
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'secKillApi.aspx?method=SaveTaobaoOrderData',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            $.messager.progress("close");
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                                //$('#dg').datagrid('reload');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }

        //导入淘宝订单
        function ImportTaobao() {
            $('#dlg').dialog('open').dialog('setTitle', '导入淘宝订单数据');
            showData();
            $('#dgTb').datagrid('loadData', { total: 0, rows: [] });
        }

        //保存上传的文件
        function saveFile() {
            $('#dgTb').datagrid('loadData', { total: 0, rows: [] });
            ajaxFileUpload();
        }

        //文件上传
        function ajaxFileUpload() {
            //$("#loading").ajaxStart(function () { $(this).show(); }).ajaxComplete(function () { $(this).hide(); });
            $.ajaxFileUpload({
                url: 'secKillApi.aspx?method=saveFile',
                secureuri: false,
                fileElementId: 'fileT',
                dataType: 'json',
                //data: { name: 'logan', id: 'id' },
                success: function (data, status) {
                    var d = eval(data.responseText);
                    $('#dgTb').datagrid('loadData', d);
                },
                error: function (data, status, e) {
                    var d = eval(data.responseText);
                    $('#dgTb').datagrid('loadData', d);
                }
            }
            )
            return false;
        }
        //显示DataGrid数据列表
        function showData() {
            $('#dgTb').datagrid({
                width: '100%',
                height: '550px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'TaobaoID',
                url: null,
                toolbar: '#dgTbtoolbar',
                columns: [[
                  { title: '宝贝标题', field: 'Title', width: '100px' },
                    { title: '淘宝订单号', field: 'TaobaoID', width: '130px' },
                  //{ title: '商品图片', field: 'pic_path', width: '80px' },
                  { title: '买家昵称', field: 'buyer_nick', width: '80px' },
                  { title: '买家支付宝账号', field: 'buyer_alipay', width: '80px' },
                  { title: '收货人姓名', field: 'receiver_name', width: '80px' },
                  { title: '手机号码', field: 'receiver_mobile', width: '80px' },
                  { title: '收货人电话', field: 'receiver_phone', width: '80px' },
                  { title: '收货人省份', field: 'receiver_state', width: '80px' },
                  { title: '收货人城市', field: 'receiver_city', width: '80px' },
                  { title: '收货人区域', field: 'receiver_district', width: '80px' },
                  { title: '详细地址', field: 'receiver_address', width: '150px' },
                  { title: '实付金额', field: 'payment', width: '70px' },
                  //{ title: '商品单价', field: 'price', width: '70px' },
                  { title: '购买数量', field: 'num', width: '60px' },
                  { title: '订单总金额', field: 'total_fee', width: '80px' },
                  { title: '订单创建时间', field: 'created', width: '130px', formatter: DateTimeFormatter },
                  { title: '订单状态', field: 'status', width: '100px' },
                  { title: '物流公司', field: 'logicCompany', width: '100px' },
                  { title: '物流单号', field: 'logicno', width: '100px' },
                  { title: '付款时间', field: 'pay_time', width: '130px', formatter: DateTimeFormatter }
                ]]
            });
        }

        //关闭订单状态跟踪弹出框
        function closeDlgStatus() {
            $('#dlg').dialog('close');
        }
    </script>
</asp:Content>
