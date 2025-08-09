<%@ Page Language="C#" Title="销售日收入报表" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SaleDayReport.aspx.cs" Inherits="Supplier.Report.SaleDayReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../JS/FloatNavi/base.css" rel="stylesheet" />
<script src="../JS/FloatNavi/common.js" type="text/javascript"></script>
<script src="../JS/EChart/chart.umd.js" type="text/javascript"></script>
<script src="../JS/EChart/chartjs-plugin-datalabels.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('#dgBrandTop30').datagrid({
                width: '100%',
                height: '400px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'TypeName',
                url: null,
                rownumbers: true,
                columns: [[
                    { title: '品牌', field: 'TypeName', width: '110px' },
                    { title: '规格', field: 'Specs', width: '130px' },
                    { title: '花纹', field: 'Figure', width: '150px' },
                    { title: '销售量(条)', field: 'OrderPiece', width: '105px', align: 'right' },
                    { title: '单条价值(元)', field: 'UnitCharge', width: '120px', align: 'right' },
                ]],
                onClickRow: function (index, row) {
                    $('#dgBrandTop30').datagrid('clearSelections');
                    $('#dgBrandTop30').datagrid('selectRow', index);
                },
            });
            $('#dgDayAccessStatis').datagrid({
                width: '100%',
                height: '400px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'TypeName',
                url: null,
                rownumbers: true,
                columns: [[
                    { title: '品牌', field: 'TypeName', width: '110px' },
                    { title: '规格', field: 'Specs', width: '130px' },
                    { title: '花纹', field: 'Figure', width: '120px' },
                    {
                        title: '载速', field: 'LoadIndex', width: '120px', formatter: function (value, row) {
                            return row.LoadIndex + row.SpeedLevel;
                        }
                    },
                    { title: '门店名称', field: 'CompanyName', width: '130px' },
                    { title: '联系人', field: 'Name', width: '120px' },
                    { title: '手机号', field: 'Cellphone', width: '130px' },
                    { title: '浏览时间', field: 'AccessDate', width: '120px', formatter: DateTimeFormatter },
                ]],
                onClickRow: function (index, row) {
                    $('#dgDayAccessStatis').datagrid('clearSelections');
                    $('#dgDayAccessStatis').datagrid('selectRow', index);
                },
            });
            $('#dgSevenDayOrder').datagrid({
                width: '100%',
                height: '400px',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'TypeName',
                url: null,
                rownumbers: true,
                columns: [[
                    { title: '品牌', field: 'TypeName', width: '140px' },
                    { title: '规格', field: 'Specs', width: '140px' },
                    { title: '花纹', field: 'Figure', width: '140px' },
                    {
                        title: '载速', field: 'LoadIndex', width: '140px', formatter: function (value, row) {
                            return row.LoadIndex + row.SpeedLevel;
                        }
                    },
                    { title: '门店名称', field: 'ClientName', width: '160px' },
                    { title: '销量(条)', field: 'OrderPiece', width: '120px', align: 'right' },
                    { title: '下单时间', field: 'CreateDate', width: '120px', formatter: DateFormatter },
                ]],
                onClickRow: function (index, row) {
                    $('#dgSevenDayOrder').datagrid('clearSelections');
                    $('#dgSevenDayOrder').datagrid('selectRow', index);
                },
            });
            $('#dgBrandSKUStatis').datagrid({
                width: '100%',
                height: '400px',
                title: '', //标题内容品牌SKU统计
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'TypeName',
                url: null,
                rownumbers: true,
                columns: [[
                    { title: '品牌', field: 'TypeName', width: '130px' },
                    { title: 'SKU总数(个)', field: 'SKUTotalNum', width: '130px', align: 'right' },
                    { title: '在库SKU(个)', field: 'InCargoSKUNum', width: '130px', align: 'right' },
                    { title: '已售SKU(个)', field: 'SaledSKUNum', width: '130px', align: 'right' },
                    { title: '动销率', field: 'SaledRatio', width: '120px', align: 'right' },
                ]],
                onClickRow: function (index, row) {
                    $('#dgBrandSKUStatis').datagrid('clearSelections');
                    $('#dgBrandSKUStatis').datagrid('selectRow', index);
                },
            });

        });

        //查询
        function dosearch() {
            RELOAD();
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="font-size: 30px; font-weight: bold; background-color: white;">
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'">
        <table id="saPanelTab">
    <tr colspan="5">
        <td style="text-align: right;">仓库:
        </td>
        <td>
            <input id="HouseID" class="easyui-combobox" style="width: 95px;" data-options="valueField:'id',textField:'text',editable:false,required:true" />
        </td>
        <td style="text-align: right;">开单时间:
        </td>
        <td>
            <input id="StartDate" class="easyui-datebox" style="width: 100px" />~
           <input id="EndDate" class="easyui-datebox" style="width: 100px" />
            &nbsp;&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        </td>
    </tr>
</table>
    </div>
</div>
<div id="saPanel" style="font-weight: bold; background-color: white; padding-bottom: 3px; padding-top: 3px; margin-top: 5px;">
                <table style="text-align: center;">

                <tr>
   

    
<td><span style="font-weight: lighter; font-size: 15px; color: #808080">日订单数(个)</span><br />
    <br style="line-height: 5px;" />
    <span style="font-size: 30px; font-weight: bold; color: #ed2323;" id="DayOrderNum">0</span>
</td>

    <td><span style="font-weight: lighter; font-size: 15px; color: #808080">月订单数(个)</span><br />
        <br style="line-height: 5px;" />
        <span style="font-size: 30px; font-weight: bold; color: #ed2323;" id="MonthOrderNum">0</span>
    </td>
    <td>
    <div class="boxDid">
        <span style="font-weight: lighter; font-size: 15px; color: #808080">日销量(条)</span><br />
        <br style="line-height: 5px;" />
        <span style="font-size: 30px; font-weight: bold; color: #ed2323;" id="DayOrderSalesVolume">0</span>
    </div>
</td>
    <td>
        <div class="boxDid">
            <span style="font-weight: lighter; font-size: 15px; color: #808080">月销量(条)</span><br />
            <br style="line-height: 5px;" />
            <span style="font-size: 30px; font-weight: bold; color: #ed2323;" id="MonthOrderSalesVolume">0</span>
        </div>
    </td>
                    <td>
    <span style="font-weight: lighter; font-size: 15px; color: #808080">日销售额(元)</span><br />
    <br style="line-height: 5px;" />
    <span style="font-size: 30px; font-weight: bold; color: #2E8B57;" id="DayOrderRevenue">0</span>
</td>
    <td>
        <span style="font-weight: lighter; font-size: 15px; color: #808080">月销售额(万)</span><br />
        <br style="line-height: 5px;" />
        <span style="font-size: 30px; font-weight: bold; color: #2E8B57;" id="MonthOrderRevenue">0</span>
    </td>
   
    <td><span style="font-weight: lighter; font-size: 15px; color: #808080">总销售额(万)</span><br />
        <br style="line-height: 5px;" />
        <span style="font-size: 30px; font-weight: bold; color: #1cdb14;" id="TotalRevenue">0</span>
        <br style="line-height: 5px;" />
<span style="font-weight: lighter; font-size: 13px; color: #808080">总销量(条):<span style="font-size: 15px; font-weight: bold; color: #2E8B57;" id="TotalNum">0</span></span>



    </td>

</tr>
                 <tr>
             <td>
                 <span style="font-weight: lighter; font-size: 15px; color: #808080;">日访问用户数</span>
                 <br />
                 <br style="line-height: 5px;" />
                 <span style="font-size: 30px; font-weight: bold; color: #1ea7f7;" id="DayUserCount">0</span>
             </td>
             <td>
                 <span style="font-weight: lighter; font-size: 15px; color: #808080">月访问用户数</span>
                 <br />
                 <br style="line-height: 5px;" />
                 <span style="font-size: 30px; font-weight: bold; color: #1ea7f7;" id="MonthUserCount">0</span>
             </td>
             <td><span style="font-weight: lighter; font-size: 15px; color: #808080">日访问门店数(家)</span><br />
                 <br style="line-height: 5px;" />
                 <span style="font-size: 30px; font-weight: bold; color: #ed2323;" id="DayClientCount">0</span>
             </td>
             <td>
                 <div class="boxDid">
                     <span style="font-weight: lighter; font-size: 15px; color: #808080">月访问门店数(家)</span><br />
                     <br style="line-height: 5px;" />
                     <span style="font-size: 30px; font-weight: bold; color: #ed2323;" id="MonthClientCount">0</span>
                 </div>
             </td>
             <td>
                 <span style="font-weight: lighter; font-size: 15px; color: #808080">日浏览数(次)</span><br />
                 <br style="line-height: 5px;" />
                 <span style="font-size: 30px; font-weight: bold; color: #2E8B57;" id="DayClickCount">0</span>
             </td>

             <td>
                 <span style="font-weight: lighter; font-size: 15px; color: #808080">月浏览数(次)</span><br />
                 <br style="line-height: 5px;" />
                 <span style="font-size: 30px; font-weight: bold; color: #1cdb14;" id="MonthClickCount">0</span>

             </td>
             <td><span style="font-weight: lighter; font-size: 15px; color: #808080">总SKU数(个)</span><br />
                 <br style="line-height: 5px;" />
                 <span style="font-size: 30px; font-weight: bold; color: #2E8B57;" id="TotalSKUNum">0</span>
                 <br style="line-height: 5px;" />
                 <span style="font-weight: lighter; font-size: 13px; color: #808080">库存SKU:<span style="font-size: 15px; font-weight: bold; color: #2E8B57;" id="InCargoSKUNum">0</span></span>

             </td>
         </tr>
            </table>


</div>

    <div style="width: 100%; display: flex; align-items: center;">
        <div style="width: 30%;">
            <canvas id="DayOrderNumBar"></canvas>
        </div>
        <div style="width: 40%;">
            <canvas id="DayOrderSalesVolumeBar"></canvas>
        </div>
        <div style="width: 30%;">
            <canvas id="DayOrderRevenueBar"></canvas>
        </div>
    </div>

    <div style="width: 100%; display: flex; align-items: center;">
        <div style="width: 40%;" id="pnl0" class="easyui-panel" title="品牌规格月销售排名" data-options="tools:'#tl0'">
            <table id="dgBrandTop30" class="easyui-datagrid">
            </table>
        </div>
        <div id="tl0">
            <a href="javascript:void(0)" class="icon-reload" onclick="QueryHCYC_BrandSpecsNum()" title="刷新"></a>
        </div>
        <div style="width: 59%;" id="pnl1" class="easyui-panel" title="本月客户浏览数据" data-options="tools:'#tl1'">
            <table id="dgDayAccessStatis" class="easyui-datagrid">
            </table>
        </div>
        <div id="tl1">
            <a href="javascript:void(0)" class="icon-reload" onclick="QueryHCYC_DayAccessNum()" title="刷新"></a>
        </div>
        
    </div>

    <div style="width: 100%; display: flex; align-items: center;">
        <div style="width: 40%;" id="pn13" class="easyui-panel" title="品牌SKU统计" data-options="tools:'#tl3'">
            <table id="dgBrandSKUStatis" class="easyui-datagrid">
            </table>
        </div>
        <div id="tl3">
            <a href="javascript:void(0)" class="icon-reload" onclick="QueryHCYC_BrandSKUData()" title="刷新"></a>
        </div>
        <div style="width: 59%;" id="pnl2" class="easyui-panel" title="本月订单数据" data-options="tools:'#tl2'">
    <table id="dgSevenDayOrder" class="easyui-datagrid">
    </table>
</div>
<div id="tl2">
    <a href="javascript:void(0)" class="icon-reload" onclick="QueryHCYC_SevenDayOrderInfo()" title="刷新"></a>
</div>
    </div>

    <script type="text/javascript">
        $(function () {

            //var windowHeight = $(window).height() - 150;
            //$(".Chart").css("height", windowHeight)

            // 设置EndDate的datebox
            $('#EndDate').datebox({
                onShowPanel: function () {
                    // 禁用当前日期之后的日期
                    var now = new Date();
                    setDisabledDates($(this), now);
                }
            });

            // 设置StartDate的datebox
            $('#StartDate').datebox({
                onShowPanel: function () {
                    // 禁用当前日期之后的日期
                    var now = new Date();
                    setDisabledDates($(this), now);
                }
            });

            var datenow = new Date();

            //时间

            $('#StartDate').datebox('setValue', getNowFormatDate(datenow.getFullYear().toString() + "-" + (datenow.getMonth() + 1).toString() + "-01"));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
            $('#StartDate').datebox('textbox').bind('focus', function () { $('#StartDate').datebox('showPanel'); });
            $('#EndDate').datebox('textbox').bind('focus', function () { $('#EndDate').datebox('showPanel'); });

            //所在仓库
            $('#HouseID').combobox({
                url: '../FormService.aspx?method=CargoPermisionHouse',
                valueField: 'SettleHouseID', textField: 'SettleHouseName'
            });
            $('#HouseID').combobox('setValue', '<%=UserInfor.SettleHouseID%>');

            RELOAD();
        });

        // 公共函数，用于设置禁用日期
        function setDisabledDates(datebox, now) {
            var year = now.getFullYear();
            var month = now.getMonth(); // 月份是从0开始的
            var day = now.getDate();

            datebox.datebox('calendar').calendar({
                validator: function (date) {
                    return date <= new Date(year, month, day);
                }
            });
        }


        //每日销售订单数
        var DayOrderNumBar = document.getElementById('DayOrderNumBar');
        var DayOrderNumBarChart = new Chart(DayOrderNumBar, {
            data: {
                labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
                datasets: [{
                    label: '销售量',
                    data: [12, 95, 45, 78, 96, 31],
                    type: 'bar',
                }]
            },
            options: {
                plugins: {
                    datalabels: {
                        color: 'Black',
                        font: {
                            weight: 'bold',
                            size: 15,
                        },
                        anchor: 'end', // 设置标签的锚点位置（位置相对于数据点）
                        //align: 'top', // 设置标签的对齐位置（相对于锚点）
                        //formatter: function (value) {
                        //    return value;
                        //}
                    },
                    //legend: {
                    //    position: 'top',
                    //},
                    title: {
                        display: true,
                        text: '每日销售订单数',
                        font: {
                            size: 15
                        }
                    }

                },

            }
        });

        //每日订单销售量
        var DayOrderSalesVolumeBar = document.getElementById('DayOrderSalesVolumeBar');
        var DayOrderSalesVolumeBarChart = new Chart(DayOrderSalesVolumeBar, {
            data: {
                labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
                datasets: [{
                    label: '销售量',
                    data: [12, 95, 45, 78, 96, 31],
                    type: 'bar',
                }]
            },
            options: {
                plugins: {
                    datalabels: {
                        color: 'Black',
                        font: {
                            weight: 'bold',
                            size: 15,
                        },
                        anchor: 'end', // 设置标签的锚点位置（位置相对于数据点）
                        //align: 'top', // 设置标签的对齐位置（相对于锚点）
                        //formatter: function (value) {
                        //    return value;
                        //}
                    },
                    //legend: {
                    //    position: 'top',
                    //},
                    title: {
                        display: true,
                        text: '每日订单销售量',
                        font: {
                            size: 15
                        }
                    }

                },

            }
        });

        //每日销售额统计
        var DayOrderRevenueBar = document.getElementById('DayOrderRevenueBar');
        var DayOrderRevenueBarChart = new Chart(DayOrderRevenueBar, {
            data: {
                labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun'],
                datasets: [{
                    label: '销售量',
                    data: [12, 95, 45, 78, 96, 31],
                    type: 'bar',
                }]
            },
            options: {
                plugins: {
                    datalabels: {
                        color: 'Black',
                        font: {
                            weight: 'bold',
                            size: 15,
                        },
                        anchor: 'end', // 设置标签的锚点位置（位置相对于数据点）
                        //align: 'top', // 设置标签的对齐位置（相对于锚点）
                        //formatter: function (value) {
                        //    return value;
                        //}
                    },
                    //legend: {
                    //    position: 'top',
                    //},
                    title: {
                        display: true,
                        text: '每日销售额统计',
                        font: {
                            size: 15
                        }
                    }

                },

            }
        });
        function RELOAD() {
            //用户访问信息
            $.ajax({
                type: 'post',
                url: "reportApi.aspx?method=HCYCAccessStatistics",
                data: {
                    HouseID: $('#HouseID').combobox('getValue'),
                    StartDate: $('#StartDate').datebox('getValue'),
                    EndDate: $('#EndDate').datebox('getValue'),
                },
                success: function (data) {
                    var newData = JSON.parse(data);
                    console.log(newData, "用户访问信息");
                    $("#DayUserCount").text(newData.Users.DayUserCount);
                    $("#MonthUserCount").text(newData.Users.MonthUserCount);
                    $("#DayClientCount").text(newData.Users.DayClientCount);
                    $("#MonthClientCount").text(newData.Users.MonthClientCount);
                    $("#DayClickCount").text(newData.Users.DayClickCount);
                    $("#MonthClickCount").text(newData.Users.MonthClickCount);
                }
            });

            //SKU数据统计
            $.ajax({
                type: 'post',
                url: "reportApi.aspx?method=QueryHCYC_BrandSKUData",
                data: {
                    HouseID: $('#HouseID').combobox('getValue'),
                    StartDate: $('#StartDate').datebox('getValue'),
                    EndDate: $('#EndDate').datebox('getValue'),
                },
                success: function (dt) {
                    var data = eval(dt);
                    $('#dgBrandSKUStatis').datagrid('loadData', data);
                    console.log(data, "SKU数据统计");
                    var TotalSKUNum = 0;
                    var InCargoSKUNum = 0;
                    for (var i = 0; i < data.length; i++) {
                        TotalSKUNum += data[i].SKUTotalNum;
                        InCargoSKUNum += data[i].InCargoSKUNum;
                    }
                    console.log(TotalSKUNum, InCargoSKUNum, "SKU数据统计");
                    $("#TotalSKUNum").text(TotalSKUNum);
                    $("#InCargoSKUNum").text(InCargoSKUNum);
                }
            });

            var StoresalternatingColors = ['rgba(175,238,238,0.5)', 'rgba(64,224,208,0.5)'];
            var DayOrderalternatingColors = ['rgba(255,106,106,0.5)', 'rgba(238,99,99,0.5)'];
            var DayOrderSalesVolumealternatingColors = ['#ff7f50', '#87cefa', '#da70d6', '#32cd32', '#6495ed', '#ff69b4', '#ba55d3', '#cd5c5c', '#ffa500', '#40e0d0', '#1e90ff', '#ff6347', '#7b68ee', '#00fa9a', '#ffd700', '#6699FF', '#ff6666', '#3cb371', '#b8860b', '#30e0e0'];
            var DayOrderRevenuealternatingColors = ['rgba(135,206,250,0.5)', 'rgba(127,255,212,0.5)'];

            //订单数据
            $.ajax({
                type: 'post',
                url: "reportApi.aspx?method=HCYCStatistics",
                data: {
                    HouseID: $('#HouseID').combobox('getValue'),
                    StartDate: $('#StartDate').datebox('getValue'),
                    EndDate: $('#EndDate').datebox('getValue'),
                },
                success: function (data) {

                    var newData = JSON.parse(data);
                    console.log(newData.Order, "订单数据");
                    $("#DayOrderNum").text(newData.Order.DayOrderNum)
                    $("#DayOrderRevenue").text(newData.Order.DayOrderRevenue)
                    $("#DayOrderSalesVolume").text(newData.Order.DayOrderSalesVolume)
                    $("#MonthOrderNum").text(newData.Order.MonthOrderNum)
                    $("#MonthOrderRevenue").text((newData.Order.MonthOrderRevenue / 10000).toFixed(2))
                    $("#MonthOrderSalesVolume").text(newData.Order.MonthOrderSalesVolume)
                    $("#TotalNum").text(newData.Order.TotalNum)
                    $("#TotalRevenue").text((newData.Order.TotalRevenue / 10000).toFixed(2))

                    //每日销售订单数
                    var DayOrderNumlabels = [];
                    var DayOrderNumdatas = {
                        data: [],
                        label: '订单数',
                        type: 'bar',
                        borderColor: [],
                        backgroundColor: [],

                    }
                    //每日订单销售量
                    var DayOrderSalesVolumedatas = {
                        data: [],
                        label: '轮胎条数',
                        type: 'bar',
                        borderColor: [],
                        backgroundColor: [],
                    }
                    //每日销售额统计
                    var DayOrderRevenuedatas = {
                        data: [],
                        label: '销售金额',
                        type: 'bar',
                        borderColor: [],
                        backgroundColor: [],
                    }

                    for (var i = 0; i < newData.Statistics.Order.length; i++) {
                        //每日销售订单数
                        DayOrderNumlabels.push(newData.Statistics.Order[i].Date)
                        DayOrderNumdatas.data.push(newData.Statistics.Order[i].OrderNum)
                        //每日订单销售量
                        DayOrderSalesVolumedatas.data.push(newData.Statistics.Order[i].OrderSalesVolume)
                        //每日销售额统计
                        DayOrderRevenuedatas.data.push(newData.Statistics.Order[i].OrderRevenue)
                    }

                    DayOrderNumdatas.data.forEach((dataset, index) => {
                        //每日销售订单数
                        var colorIndex = index % StoresalternatingColors.length;
                        DayOrderNumdatas.backgroundColor.push(DayOrderalternatingColors[colorIndex]);
                        DayOrderNumdatas.borderColor.push(DayOrderalternatingColors[colorIndex]);

                        //每日订单销售量
                        DayOrderSalesVolumedatas.backgroundColor.push(DayOrderSalesVolumealternatingColors[colorIndex]);
                        DayOrderSalesVolumedatas.borderColor.push(DayOrderSalesVolumealternatingColors[colorIndex]);

                        //每日销售额统计
                        DayOrderRevenuedatas.backgroundColor.push(DayOrderRevenuealternatingColors[colorIndex]);
                        DayOrderRevenuedatas.borderColor.push(DayOrderRevenuealternatingColors[colorIndex]);

                    });

                    //每日销售订单数
                    // 销毁之前的图表

                    DayOrderNumBarChart.data.datasets.pop();
                    DayOrderNumBarChart.data.labels = DayOrderNumlabels;
                    DayOrderNumBarChart.data.datasets.push(DayOrderNumdatas);
                    DayOrderNumBarChart.update();

                    //每日订单销售量

                    DayOrderSalesVolumeBarChart.data.datasets.pop();
                    DayOrderSalesVolumeBarChart.data.labels = DayOrderNumlabels;
                    DayOrderSalesVolumeBarChart.data.datasets.push(DayOrderSalesVolumedatas);
                    DayOrderSalesVolumeBarChart.update();

                    //每日销售额统计
                    DayOrderRevenueBarChart.data.datasets.pop();
                    DayOrderRevenueBarChart.data.labels = DayOrderNumlabels;
                    DayOrderRevenueBarChart.data.datasets.push(DayOrderRevenuedatas);
                    DayOrderRevenueBarChart.update();

                }
            });

            //品牌规格月销售排名
            QueryHCYC_BrandSpecsNum();
            //本月订单数据
            QueryHCYC_SevenDayOrderInfo();
            //本月客户浏览数据
            QueryHCYC_DayAccessNum();
            //品牌SUK统计
            QueryHCYC_BrandSKUData();
        }

        //品牌规格月销售排名
        function QueryHCYC_BrandSpecsNum() {
            $.ajax({
                type: 'post',
                url: "reportApi.aspx?method=QueryHCYC_BrandSpecsNum",
                data: {
                    HouseID: $('#HouseID').combobox('getValue'),
                    StartDate: $('#StartDate').datebox('getValue'),
                    EndDate: $('#EndDate').datebox('getValue'),
                },
                success: function (dt) {
                    var data = eval(dt);
                    $('#dgBrandTop30').datagrid('loadData', data);
                }
            });
        }

        //本月订单数据
        function QueryHCYC_SevenDayOrderInfo() {
            $.ajax({
                type: 'post',
                url: "reportApi.aspx?method=QueryHCYC_SevenDayOrderInfo",
                data: {
                    HouseID: $('#HouseID').combobox('getValue'),
                    StartDate: $('#StartDate').datebox('getValue'),
                    EndDate: $('#EndDate').datebox('getValue'),
                },
                success: function (dt) {
                    var data = eval(dt);
                    $('#dgSevenDayOrder').datagrid('loadData', data);

                }
            });
        }

        //本月客户浏览数据
        function QueryHCYC_DayAccessNum() {
            $.ajax({
                type: 'post',
                url: "reportApi.aspx?method=QueryHCYC_DayAccessNum",
                data: {
                    HouseID: $('#HouseID').combobox('getValue'),
                    StartDate: $('#StartDate').datebox('getValue'),
                    EndDate: $('#EndDate').datebox('getValue'),
                },
                success: function (dt) {
                    var data = eval(dt);
                    $('#dgDayAccessStatis').datagrid('loadData', data);
                }
            });
        }

        //品牌SKU统计
        function QueryHCYC_BrandSKUData() {
            $.ajax({
                type: 'post',
                url: "reportApi.aspx?method=QueryHCYC_BrandSKUData",
                data: {
                    HouseID: $('#HouseID').combobox('getValue'),
                    StartDate: $('#StartDate').datebox('getValue'),
                    EndDate: $('#EndDate').datebox('getValue'),
                },
                success: function (dt) {
                    var data = eval(dt);
                    $('#dgBrandSKUStatis').datagrid('loadData', data);
                }
            });
        }


    </script>
</asp:Content>
