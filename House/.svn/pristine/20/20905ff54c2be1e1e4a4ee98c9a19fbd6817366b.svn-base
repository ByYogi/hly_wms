<%@ Page Title="时效统计详细列表" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reportAgeingDetail.aspx.cs" Inherits="Cargo.Report.reportAgeingDetail" %>


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
            var height = Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - $("div[name='SelectDiv2']").outerHeight(true);
            $('#dg').datagrid({ height: height });
        }
        $(document).ready(function () {
            setDatagrid();
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));

            //所在仓库
            $('#AHouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#PID').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=' + rec.HouseID;
                    $('#PID').combobox('reload', url);

                    $('#ALineID').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryCargoLogisLineList&hid=' + rec.HouseID;
                    $('#ALineID').combobox('reload', url);
                }
            });
            $('#AHouseID').combobox('setValue', '<%=UserInfor.HouseID%>');

            $('#PID').combobox('clear');
            var url = '../House/houseApi.aspx?method=QueryALLArea&pid=0&hid=<%=UserInfor.HouseID%>';
            $('#PID').combobox('reload', url);

            $('#ALineID').combobox('clear');
            var url = '../House/houseApi.aspx?method=QueryCargoLogisLineList&hid=<%=UserInfor.HouseID%>';
            $('#ALineID').combobox('reload', url);

            //省市区三级联动
            $('#VProvince').combobox({
                url: '../House/houseApi.aspx?method=QueryCityData',
                valueField: 'City', textField: 'City',
                onSelect: function (rec) {
                    $('#VCity').combobox('clear');
                    var url = '../House/houseApi.aspx?method=QueryCityData&PID=' + rec.ID;
                    $('#VCity').combobox('reload', url);
                }
            });
            $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });
            $('#PID').combobox('textbox').bind('focus', function () { $('#PID').combobox('showPanel'); });
            $('#ALineID').combobox('textbox').bind('focus', function () { $('#ALineID').combobox('showPanel'); });
            $('#VProvince').combobox('textbox').bind('focus', function () { $('#VProvince').combobox('showPanel'); });
            $('#VCity').combobox('textbox').bind('focus', function () { $('#VCity').combobox('showPanel'); });

        });
        function setDatagrid() {
            var columns = [];
            columns.push({ title: '开单时间', field: 'CreateDate', width: '125px', formatter: DateTimeFormatter });
            columns.push({ title: '出库时间', field: 'OutCargoTime', width: '125px', formatter: DateTimeFormatter });
            columns.push({ title: '出库时效(小时)', field: 'OutCargoAgeing', width: '90px', align: 'right' });
            //columns.push({ title: '接单时间', field: 'TakeOrderTime', width: '125px', formatter: DateTimeFormatter });
            //columns.push({ title: '发车时间', field: 'SendCarTime', width: '125px', formatter: DateTimeFormatter });
            columns.push({ title: '签收时间', field: 'SignTime', width: '125px', formatter: DateTimeFormatter });
            columns.push({ title: '开单签收时效(小时)', field: 'FromCreateToSignAgeing', width: '150px', align: 'right' });
            columns.push({ title: '出库签收时效(小时)', field: 'FromOutCargoToSignAgeing', width: '150px', align: 'right' });
            //columns.push({
            //    title: '签收类型', field: 'AbSignStatus', width: '70px',
            //    formatter: function (val, row, index) {
            //        if (val == "0") { return "<span title='正常签收'>正常签收</span>"; }
            //        else if (val == "1") { return "<span title='超时签收'>超时签收</span>"; }
            //        else { return ""; }
            //    }
            //});
            $('#dg').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: false, //分页是否显示
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - $("div[name='SelectDiv2']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - $("div[name='SelectDiv2']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - $("div[name='SelectDiv2']").outerHeight(true) - 58) / 28) * 2],
                fitColumns: true, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'OrderNo',
                url: null,
                toolbar: '#toolbar',
                rownumbers: true,
                showFooter: true,
                frozenColumns: [[
                    { title: '出库仓库', field: 'OutHouseName', width: '70px' },
                    { title: '订单号', field: 'OrderNo', width: '90px' },
                    //{ title: '大区', field: 'Product', width: '50px' },
                    { title: '省', field: 'Province', width: '50px' },
                    { title: '市', field: 'City', width: '50px' },
                    { title: '店代码', field: 'ShopCode', width: '50px' },
                    { title: '数量', field: 'Piece', width: '40px', align: 'right' },
                    //{ title: '线路', field: 'LineName', width: '60px' },
                    { title: '客户名称', field: 'PayClientName', width: '120px' },
                    {
                        title: '订单状态', field: 'AwbStatus', width: '60px', formatter: function (val, row, index) {
                            if (val == "0") { return "<span title='已下单'>已下单</span>"; }
                            else if (val == "1") { return "<span title='出库中'>出库中</span>"; }
                            else if (val == "2") { return "<span title='已出库'>已出库</span>"; }
                            else if (val == "3") { return "<span title='已发车'>已发车</span>"; }
                            else if (val == "4") { return "<span title='配送中'>配送中</span>"; }
                            else if (val == "5") { return "<span title='已签收'>已签收</span>"; }
                            else if (val == "6") { return "<span title='已拣货'>已拣货</span>"; }
                            else if (val == "7") { return "<span title='配送中'>配送中</span>"; }
                            else if (val == "8") { return "<span title='已接单'>已接单</span>"; }
                            else { return ""; }
                        }
                    },
                    //{ title: '签收照片', field: 'SignImage', width: '120px', formatter: imgFormatter },
                    { title: '签收照片', field: 'SignImage', width: '80px', formatter: checkImage },

                ]],
                columns: [columns],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                rowStyler: function (index, row) {
                    if (row.AwbStatus == "5") {
                        if (row.SignImage == undefined || row.SignImage == "") {
                            return "background-color:#fdecee"; 
                        } 
                    }
                },
                onDblClickRow: function (index, row) { }
            });
        }
        function checkImage(value, row) {
            if (row.AwbStatus == "5") {
                if (value == undefined || value == "") {
                    return "无照片";
                } else {
                    return "有照片";
                }
            }
        }
        //图片添加路径  
        function imgFormatter(value) {
            if ('' != value && null != value) {
                var rvalue = "";
                var delimiter = "|";

                // 使用split方法分割字符串
                var fruits = value.split(delimiter).filter(part => part.trim() !== '');

                // 使用forEach循环处理每个元素
                fruits.forEach((fruit, index) => {
                    rvalue += "<img onclick=downView(\"" + fruit + "\") style='width:66px; height:60px;margin-left:1px;' src='" + fruit + "' title='点击查看图片'/>";
                });

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
            gridOpts.url = 'reportApi.aspx?method=QueryReportAgeingList';
            $('#dg').datagrid('load', {
                HouseID: $("#AHouseID").combobox('getValue'),//仓库ID
                OutHouseName: $("#PID").combobox('getText'),
                LineID: $("#ALineID").combobox('getValue'),
                ShopCode: $("#shopCode").val(),
                Province: $("#VProvince").combobox('getValue'),
                City: $("#VCity").combobox('getValue'),
                //AcceptPeople: $("#AcceptPeople").val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                OrderType: $("#AOrderType").combobox('getValue'),
                //AbSignStatus: $("#AAbSignStatus").combobox('getValue'),

                AwbStatus: $("#AAwbStatus").combobox('getValue'),
                //Dep: $("#Dep").combobox('getText'),
                //Dest: $("#Dest").combobox('getText'),
                //SaleManID: $('#ASaleManID').combobox('getValue'),
                //CreateAwb: $('#ACreateAwb').textbox('getValue'),
                //AcceptUnit: $('#AcceptUnit').val(),
                //OrderModel: "0",//订单类型
                //HAwbNo: $('#AHAwbNo').val()
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
    <div name="SelectDiv2" style="background: linear-gradient(to bottom, #eff5ff 0px, #e0ecff 100%) repeat-x scroll 0 0; border-color: #95b8e7; border-style: solid; border-width: 1px 1px 0px 1px;">
        <table>
            <tr>
                <td>
                    <a class="easyui-linkbutton" iconcls="icon-chart_bar" plain="false" href="../Report/reportOutTime.aspx" target="_self">&nbsp;时效统计&nbsp;</a>&nbsp;&nbsp;
                    <a id="saleManReport" class="easyui-linkbutton" style="color: Red;" iconcls="icon-chart_bar" plain="false" href="../Report/reportAgeingDetail.aspx" target="_self">&nbsp;时效明细&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table>
            <tr>
                <td style="text-align: right;">区域大仓:
                </td>
                <td>
                    <input id="AHouseID" class="easyui-combobox" style="width: 100px;" />
                </td>

                <td style="text-align: right;">省:</td>
                <td>
                    <input id="VProvince" class="easyui-combobox" style="width: 90px;" />
                </td>


                <td style="text-align: right;">店代码:
                </td>
                <td>
                    <input id="shopCode" class="easyui-textbox" data-options="prompt:'请输入店代码'" style="width: 100px" />
                </td>
                <td style="text-align: right;">下单方式:
                </td>
                <td>
                    <select class="easyui-combobox" id="AOrderType" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">电脑下单</option>
                        <option value="2">商城下单</option>
                        <option value="3">APP下单</option>
                        <option value="4">小程序下单</option>
                        <option value="1">企业号下单</option>
                    </select>
                </td>
                <td style="text-align: right;">开单时间:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px" />~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="PID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'AreaID',textField:'Name'"
                        panelheight="auto" />
                </td>
                <td style="text-align: right;">市:</td>
                <td>
                    <input id="VCity" class="easyui-combobox" style="width: 90px;" data-options="valueField:'City',textField:'City'" />
                </td>
                <td style="text-align: right;">运输线路:
                </td>
                <td>
                    <input id="ALineID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'LineID',textField:'LineName'" />
                </td>
                <td style="text-align: right;">订单状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="AAwbStatus" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">未签收</option>
                        <option value="5">已签收</option>
                    </select>
                </td>
                <%-- <td style="text-align: right;">签收类型:
                </td>
                <td>
                    <select class="easyui-combobox" id="AAbSignStatus" style="width: 80px;" panelheight="auto">
                        <option value="-1">全部</option>
                        <option value="0">正常签收</option>
                        <option value="1">超时签收</option>
                    </select>
                </td>--%>
            </tr>
        </table>
    </div>
    <table id="dg">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="AwbExport()">&nbsp;导&nbsp;出&nbsp;</a>
        <form runat="server" id="fm1">
            <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
        </form>
    </div>
    <div id="dgViewImg" class="easyui-dialog" closed="true" style="width: 1000px; height: 600px; overflow: hidden; display: flex; justify-content: center; align-items: center;">
        <img id="simg" style="max-width: 100%; max-height: 170%;" />
    </div>
    <script type="text/javascript">

        //导出数据
        function AwbExport() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            $.messager.progress({ msg: '请稍后,正在导出中...' });
            var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click();
            $.messager.progress("close");
        }
    </script>
</asp:Content>
