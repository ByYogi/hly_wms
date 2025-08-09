<%@ Page Title="出库时间统计" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reportOutTime.aspx.cs" Inherits="Cargo.Report.reportOutTime" %>

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
            $('#HouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name'
            });
            $('#HouseID').combobox('textbox').bind('focus', function () { $('#HouseID').combobox('showPanel'); });
            //$('#HouseID').combobox('setValue', '<%=UserInfor.HouseID%>');
        });
        function setDatagrid(mulCol) {
            $('#dg').datagrid({
                width: '100%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: true, //是否可折叠
                pagination: true, //分页是否显示
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
                  {
                      title: '所属仓库', field: 'HouseName', width: '70px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  }
                ]],
                columns: mulCol,
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) {  }
            });
        }
        //查询
        function dosearch() {
            if ($('#StartDate').datebox('getValue') == undefined || $('#StartDate').datebox('getValue') == '') {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择开始月份', 'warning');
                return;
            }
            if ($('#EndDate').datebox('getValue') == undefined || $('#EndDate').datebox('getValue') == '') {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择结束月份', 'warning');
                return;
            }
            var startDate = new Date($('#StartDate').datebox('getValue'));
            var endDate = new Date($('#EndDate').datebox('getValue'));
            var sYear = startDate.getFullYear();
            var eYear = endDate.getFullYear();
            var a = startDate.getMonth() + 1;
            var b = endDate.getMonth() + 1;
            var c = MonthsBetw($('#StartDate').datebox('getValue'), $('#EndDate').datebox('getValue'));
            if (c > 4) {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '只能统计相连四个月内的数据', 'warning');
                return;
            }
            var mulCol = [];
            var bt = [];
            var rc = [];

            for (var i = 1; i <= c; i++) {
                if (a == 12) {
                    var t = sYear + "-" + a;
                    var tit = { title: t, colspan: 4 };
                    var OrderCount = {
                        title: '订单总数', field: 'OrderCount' + i.toString(), width: '80px', formatter: function (value) {
                            if (value != "" && value != null) {
                                return "<span title='" + value + " 单'>" + value + " 单</span>";
                            }
                        }
                    };
                    var PieceCount = {
                        title: '出库总数', field: 'PieceCount' + i.toString(), width: '80px', formatter: function (value) {
                            if (value != "" && value != null) {
                                return "<span title='" + value + " 件'>" + value + " 件</span>";
                            }
                        }
                    };
                    var AverageStartInterval = {
                        title: '平均扫描时效', field: 'AverageStartInterval' + i.toString(), width: '80px', formatter: function (value) {
                            if (value != "" && value != null) {
                                return "<span title='" + value + " 分钟'>" + value + " 分钟</span>";
                            }
                        }
                    };
                    var AverageEndInterval = {
                        title: '平均出库时效', field: 'AverageEndInterval' + i.toString(), width: '80px', formatter: function (value) {
                            if (value != "" && value != null) {
                                return "<span title='" + value + " 分钟'>" + value + " 分钟</span>";
                            }
                        }
                    };
                    bt.push(tit);
                    rc.push(OrderCount);
                    rc.push(PieceCount);
                    rc.push(AverageStartInterval);
                    rc.push(AverageEndInterval);
                    a = 0;
                    sYear = eYear;
                } else {
                    var t = sYear + "-" + a;
                    var tit = { title: t, colspan: 4 };
                    var OrderCount = {
                        title: '订单总数', field: 'OrderCount' + i.toString(), width: '80px', formatter: function (value) {
                            if (value != "" && value != null) {
                                return "<span title='" + value + " 单'>" + value + " 单</span>";
                            }
                        }
                    };
                    var PieceCount = {
                        title: '出库总数', field: 'PieceCount' + i.toString(), width: '80px', formatter: function (value) {
                            if (value != "" && value != null) {
                                return "<span title='" + value + " 件'>" + value + " 件</span>";
                            }
                        }
                    };
                    var AverageStartInterval = {
                        title: '平均扫描时效', field: 'AverageStartInterval' + i.toString(), width: '80px', formatter: function (value) {
                            if (value != "" && value != null) {
                                return "<span title='" + value + " 分钟'>" + value + " 分钟</span>";
                            }
                        }
                    };
                    var AverageEndInterval = {
                        title: '平均出库时效', field: 'AverageEndInterval' + i.toString(), width: '80px', formatter: function (value) {
                            if (value != "" && value != null) {
                                return "<span title='" + value + " 分钟'>" + value + " 分钟</span>";
                            }
                        }
                    };
                    bt.push(tit);
                    rc.push(OrderCount);
                    rc.push(PieceCount);
                    rc.push(AverageStartInterval);
                    rc.push(AverageEndInterval);
                }
                a++;
            }
            mulCol.push(bt);
            mulCol.push(rc);

            setDatagrid(mulCol);
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'reportApi.aspx?method=QueryOrderOutTime&StartDate=' + $('#StartDate').datebox('getValue') + '&EndDate=' + $('#EndDate').datebox('getValue') + '&HouseID=' + $("#HouseID").combobox('getValue');

        }
        //返回两个日期相差的月数
        function MonthsBetw(date1, date2) {
            //用-分成数组
            date1 = date1.split("-");
            date2 = date2.split("-");
            //获取年,月数
            var year1 = parseInt(date1[0]),
            month1 = parseInt(date1[1]),
            year2 = parseInt(date2[0]),
            month2 = parseInt(date2[1]),
            //通过年,月差计算月份差
            months = (year2 - year1) * 12 + (month2 - month1) + 1;
            return months;
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
                    <a class="easyui-linkbutton" style="color: Red;" iconcls="icon-chart_bar" plain="false" href="../Report/reportOutTime.aspx" target="_self">&nbsp;时效统计&nbsp;</a>&nbsp;&nbsp;
                    <a id="saleManReport" class="easyui-linkbutton" iconcls="icon-chart_bar" plain="false" href="../Report/reportAgeingDetail.aspx" target="_self">&nbsp;时效明细&nbsp;</a>
                </td>
            </tr>
        </table>
    </div>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table>
            <tr>
                <td style="text-align: right;">所属仓库:
                </td>
                <td>
                    <input id="HouseID" class="easyui-combobox" style="width: 90px;"
                        panelheight="auto" />
                </td>
                <td style="text-align: right;">日期范围:
                </td>
                <td>
                    <input name="Date" id="StartDate" class="easyui-datebox" data-options="prompt:'请选择开始月份',required:true" style="width: 100px" editable="false" />~<input name="Date" id="EndDate" class="easyui-datebox" data-options="prompt:'请选择开始月份',required:true" style="width: 100px" editable="false" />
                </td>
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
    <!--Begin 查询订单详细出库时间-->
    <div id="OrderDetailedTime" class="easyui-dialog" style="width: 600px; height: 320px; padding: 2px 2px"
        closed="true" buttons="#OrderDetailedTime-buttons">
        <table id="dgOrderDetailedTime" class="easyui-datagrid">
        </table>
        <div id="OrderDetailedTime-buttons">
            <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#OrderDetailedTime').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
        </div>
    </div>
    <!--End 查询产品标签列表-->
    <script src="../JS/easy/js/ajaxfileupload.js"></script>
    <script type="text/javascript">

        //导出数据
        function AwbExport() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }
            var key = new Array();
            key[0] = $('#StartDate').datebox('getValue');
            key[1] = $('#EndDate').datebox('getValue');
            key[2] = $("#HouseID").combobox('getValue');
            $.ajax({
                url: "reportApi.aspx?method=QueryOrderOutTimeForExport&key=" + escape(key.toString()),
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click(); }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }
        //查询订单出库详细时间
        function queryOrderDetailedTime() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要查询订单的数据！', 'warning');
                return;
            }
            if (row) {
                $('#OrderDetailedTime').dialog('open').dialog('setTitle', '查询订单：' + row.OrderNo + '详细出库时效');
                showOrderDetailedTimeGrid();
                $('#dgOrderDetailedTime').datagrid('clearSelections');
                var gridOpts = $('#dgOrderDetailedTime').datagrid('options');
                gridOpts.url = 'reportApi.aspx?method=QueryOrderDetailedTime&OrderNo=' + row.OrderNo;
            }
        }

        //标签数据列表
        function showOrderDetailedTimeGrid() {
            $('#dgOrderDetailedTime').datagrid({
                width: '100%',
                height: '440px',
                title: '', //标题内容
                rownumbers: true,
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                //idField: 'TagCode',
                url: null,
                showFooter: true,
                columns: [[
                  {
                      title: '所属仓库', field: 'HouseName', width: '70px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '订单号', field: 'OrderNo', width: '100px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '数量', field: 'Piece', width: '80px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '订单状态', field: 'AwbStatus', width: '80px', formatter: function (value) {
                          if (value == "0") { return "<span title='已下单'>已下单</span>"; }
                          else if (value == "1") { return "<span title='正在备货'>正在备货</span>"; }
                          else if (value == "2") { return "<span title='已出库'>已出库</span>"; }
                          else if (value == "3") { return "<span title='已装车'>已装车</span>"; }
                          else if (value == "4") { return "<span title='已到达'>已到达</span>"; }
                          else if (value == "5") { return "<span title='配送'>配送</span>"; }
                          else if (value == "6") { return "<span title='已签收'>已签收</span>"; }
                          else if (value == "7") { return "<span title='已拣货'>已拣货</span>"; }
                      }
                  },
                  { title: '开单时间', field: 'CreateDate', width: '130px', formatter: DateTimeFormatter },
                  { title: '开始扫描时间', field: 'StartOutTime', width: '130px', formatter: DateTimeFormatter },
                  {
                      title: '开始出库扫描间隔', field: 'StartOutIntervalTime', width: '100px', formatter: function (value) {
                          return "<span title='" + value + " 分钟'>" + value + " 分钟</span>";
                      }
                  },
                  { title: '最后扫描时间', field: 'EndOutTime', width: '130px', formatter: DateTimeFormatter },
                {
                    title: '最后出库扫描间隔', field: 'EndOutIntervalTime', width: '100px', formatter: function (value) {
                        return "<span title='" + value + " 分钟'>" + value + " 分钟</span>";
                    }
                }
                ]]
            });
        }

        $(function () {
            $('#StartDate').datebox({
                //显示日趋选择对象后再触发弹出月份层的事件，初始化时没有生成月份层
                onShowPanel: function () {
                    //触发click事件弹出月份层
                    span.trigger('click');
                    if (!btds)
                        //延时触发获取月份对象，因为上面的事件触发和对象生成有时间间隔
                        setTimeout(function () {
                            btds = p.find('div.calendar-menu-month-inner td');
                            btds.click(function (e) {
                                //禁止冒泡执行easyui给月份绑定的事件
                                e.stopPropagation();
                                //得到年份
                                var year = /\d{4}/.exec(span.html())[0],
                                //月份
                                //之前是这样的month = parseInt($(this).attr('abbr'), 10) + 1; 
                                month = parseInt($(this).attr('abbr'), 10);

                                //隐藏日期对象                     
                                $('#StartDate').datebox('hidePanel').datebox('setValue', year + '-' + month);
                            });
                        }, 0);
                },
                //配置parser，返回选择的日期
                parser: function (s) {
                    if (!s) return new Date();
                    var arr = s.split('-');
                    return new Date(parseInt(arr[0], 10), parseInt(arr[1], 10) - 1, 1);
                },
                //配置formatter，只返回年月 之前是这样的d.getFullYear() + '-' +(d.getMonth()); 
                formatter: function (d) {
                    var currentMonth = (d.getMonth() + 1);
                    var currentMonthStr = currentMonth < 10 ? ('0' + currentMonth) : (currentMonth + '');
                    //查询时间修改成月
                    var db1 = $('#EndDate');
                    //开始日期和结束日期的设置
                    //styleMonthDate(db1, d.getFullYear() + '-' + currentMonthStr, ['1', '4', '7', '10'], d.getfullYear());
                    //styleMonthDate(db1, d.getFullYear() + '-' + currentMonthStr, [currentMonth,currentMonth + 1, currentMonth + 2, currentMonth + 3], d.getFullYear());
                    styleMonthDate(db1, d.getFullYear() + '-' + currentMonthStr, ["" + currentMonth + "", "" + (currentMonth + 1) + "", "" + (currentMonth + 2) + "", "" + (currentMonth + 3) + ""], d.getFullYear());
                    return d.getFullYear() + '-' + currentMonthStr;
                }
            });
            //日期选择对象
            var p = $('#StartDate').datebox('panel'),
            //日期选择对象中月份
            btds = false,
            //显示月份层的触发控件
            span = p.find('span.calendar-text');
        });

        $(function () {
            $('#EndDate').datebox({
                //显示日趋选择对象后再触发弹出月份层的事件，初始化时没有生成月份层
                onShowPanel: function () {
                    //触发click事件弹出月份层
                    span.trigger('click');
                    if (!btds)
                        //延时触发获取月份对象，因为上面的事件触发和对象生成有时间间隔
                        setTimeout(function () {
                            btds = p.find('div.calendar-menu-month-inner td');
                            btds.click(function (e) {
                                //禁止冒泡执行easyui给月份绑定的事件
                                e.stopPropagation();
                                //得到年份
                                var year = /\d{4}/.exec(span.html())[0],
                                //月份
                                //之前是这样的month = parseInt($(this).attr('abbr'), 10) + 1; 
                                month = parseInt($(this).attr('abbr'), 10);

                                //隐藏日期对象                     
                                $('#EndDate').datebox('hidePanel').datebox('setValue', year + '-' + month);
                            });
                        }, 0);
                }
            });
        });

        function styleMonthDate(db, sdate, mArr, myear) {
            /*
            db --jquery对象
            sdate --'2010-02' ,默认的日期,
            mArr --需要设定的可选月份 元素为字符串类型，['1','4','7','10']
            myear -- 需要设定的年份
            */
            db.datebox({
                onShowPanel: function () { //显示日期选择对象后再触发弹出月份层的事件，初始化时没有生成月份层
                    changeDateColor(0);
                    //fix 1.3.x不选择日期点击其他地方隐藏在弹出日期框显示日期面板
                    if (p.find('div.calendar-menu').is(':hidden'))
                        p.find('div.calendar-menu').show();
                    if (!tds) setTimeout(function () { //延时触发获取月份对象，因为上面的事件触发和对象生成有时间间隔
                        tds = p.find('div.calendar-menu-month-inner td');
                        changeDateColor(0);
                        //绑定年份选择按钮事件
                        p.find(".calendar-menu-prev").bind("click", function () {
                            changeDateColor(-1);
                        });
                        p.find(".calendar-menu-next").bind("click", function () {
                            changeDateColor(1);
                        });
                        tds.click(function (e) {
                            e.stopPropagation(); //禁止冒泡执行easyui给月份绑定的事件
                            var year = /\d{4}/.exec(span.html())[0]//得到年份
                            year = p.find(".calendar-menu-year").val();
                            month = parseInt($(this).attr('abbr')); //月份
                            month = month <= 9 ? '0' + month : month;
                            var da = year + month;
                            if (!$(this).hasClass('calendar-other-month')) {
                                db.datebox('hidePanel').datebox('setValue', year + '-' + month); //设置日期的值
                            }
                        });

                    }, 0);
                    //changeDateColor();
                    //隐藏 标题两侧按钮
                    p.find(".calendar-prevmonth").hide();
                    p.find(".calendar-nextmonth").hide();
                    p.find(".calendar-prevyear").hide();
                    p.find(".calendar-nextyear").hide();
                    //禁止年份输入框编辑
                    p.find(".calendar-menu-year").attr("disabled", "disabled");
                    //解绑年份输入框中任何事件
                    p.find(".calendar-menu-year").unbind();
                },
                parser: function (s) {//配置parser，返回选择的日期
                    if (!s)
                        return new Date();
                    var arr = s.split('-');
                    return new Date(parseInt(arr[0]), parseInt(arr[1]) - 1, 1);
                },
                formatter: function (date) {
                    var y = date.getFullYear();
                    var m = date.getMonth() + 1;
                    return y + "-" + (m < 10 ? ("0" + m) : m);
                },
            });
            //改变样式
            function changeDateColor(i) {
                //先初始化
                p.find('div.calendar-menu-month-inner td').each(function (index, element) {
                    var jqNode = $(element);
                    jqNode.removeClass('calendar-other-month');
                });
                p.find('div.calendar-menu-month-inner td').each(function (index, element) {
                    year = parseInt(p.find(".calendar-menu-year").val()) + i;
                    var jqNode = $(element);
                    if (year <= (myear - 1)) {
                        jqNode.addClass('calendar-other-month');
                    }// else if ((element.abbr ==3 || element.abbr==6 || element.abbr==9 || element.abbr==12) && year == "2019") {
                    else if (($.inArray(element.abbr, mArr) == -1 && year == myear)) {
                        jqNode.addClass('calendar-other-month');
                    }
                    if (year > (myear)) {
                        jqNode.addClass('calendar-other-month');
                    }
                });
            }
            var p = db.datebox('panel'); //日期选择对象
            var tds = false;
            var span = p.find('span.calendar-text'); //显示月份层的触发控件    
            var yearArr = sdate.split('-');
            var year = yearArr[0];
            db.datebox('setValue', sdate);
        }
    </script>
</asp:Content>
