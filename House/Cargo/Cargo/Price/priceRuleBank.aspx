<%@ Page Title="促销规则管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="priceRuleBank.aspx.cs" Inherits="Cargo.Price.priceRuleBank" %>

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
            $("#Specs").combobox({
                //相当于html >> select >> onChange事件  
                onChange: function () {
                    var value = $('#Specs').combobox('getValue');
                    if (value == null || value == "") {
                        $("#Figure").combobox('options').required = true;
                        $("#Figure").combobox('textbox').validatebox('options').required = true;
                        $("#Figure").combobox('validate');
                        $("#SBatchYear").combobox('options').required = true;
                        $("#SBatchYear").combobox('textbox').validatebox('options').required = true;
                        $("#SBatchYear").combobox('validate');
                        $("#SBatchWeek").combobox('options').required = true;
                        $("#SBatchWeek").combobox('textbox').validatebox('options').required = true;
                        $("#SBatchWeek").combobox('validate');
                        $("#EBatchYear").combobox('options').required = true;
                        $("#EBatchYear").combobox('textbox').validatebox('options').required = true;
                        $("#EBatchYear").combobox('validate');
                        $("#EBatchWeek").combobox('options').required = true;
                        $("#EBatchWeek").combobox('textbox').validatebox('options').required = true;
                        $("#EBatchWeek").combobox('validate');
                        $('#Figure').combobox('clear');
                        var url = '../Price/priceApi.aspx?method=QueryAllFigureToTypeID&typeid=' + $('#ASID').combobox('getValue');
                        $('#Figure').combobox('reload', url);
                    } else {
                        $("#Figure").combobox('options').required = false;
                        $("#Figure").combobox('textbox').validatebox('options').required = false;
                        $("#Figure").combobox('validate');
                        $("#SBatchYear").combobox('options').required = false;
                        $("#SBatchYear").combobox('textbox').validatebox('options').required = false;
                        $("#SBatchYear").combobox('validate');
                        $("#SBatchWeek").combobox('options').required = false;
                        $("#SBatchWeek").combobox('textbox').validatebox('options').required = false;
                        $("#SBatchWeek").combobox('validate');
                        $("#EBatchYear").combobox('options').required = false;
                        $("#EBatchYear").combobox('textbox').validatebox('options').required = false;
                        $("#EBatchYear").combobox('validate');
                        $("#EBatchWeek").combobox('options').required = false;
                        $("#EBatchWeek").combobox('textbox').validatebox('options').required = false;
                        $("#EBatchWeek").combobox('validate');
                    }
                }
            });
            $("#SBatchYear").combobox({
                //相当于html >> select >> onChange事件  
                onChange: function () {
                    var value = $('#SBatchYear').combobox('getValue');
                    if (value == null || value == "") {
                        $("#Figure").combobox('options').required = true;
                        $("#Figure").combobox('textbox').validatebox('options').required = true;
                        $("#Figure").combobox('validate');
                        $("#Specs").combobox('options').required = true;
                        $("#Specs").combobox('textbox').validatebox('options').required = true;
                        $("#Specs").combobox('validate');
                    } else {
                        $("#Figure").combobox('options').required = false;
                        $("#Figure").combobox('textbox').validatebox('options').required = false;
                        $("#Figure").combobox('validate');
                        $("#Specs").combobox('options').required = false;
                        $("#Specs").combobox('textbox').validatebox('options').required = false;
                        $("#Specs").combobox('validate');
                    }
                }
            });
            $("#Figure").combobox({
                //相当于html >> select >> onChange事件  
                onChange: function () {
                    var value = $('#Figure').combobox('getValue');
                    if (value == null || value == "") {
                        $("#Specs").combobox('options').required = true;
                        $("#Specs").combobox('textbox').validatebox('options').required = true;
                        $("#Specs").combobox('validate');
                        $("#SBatchYear").combobox('options').required = true;
                        $("#SBatchYear").combobox('textbox').validatebox('options').required = true;
                        $("#SBatchYear").combobox('validate');
                        $("#SBatchWeek").combobox('options').required = true;
                        $("#SBatchWeek").combobox('textbox').validatebox('options').required = true;
                        $("#SBatchWeek").combobox('validate');
                        $("#EBatchYear").combobox('options').required = true;
                        $("#EBatchYear").combobox('textbox').validatebox('options').required = true;
                        $("#EBatchYear").combobox('validate');
                        $("#EBatchWeek").combobox('options').required = true;
                        $("#EBatchWeek").combobox('textbox').validatebox('options').required = true;
                        $("#EBatchWeek").combobox('validate');
                    } else {
                        $("#Specs").combobox('options').required = false;
                        $("#Specs").combobox('textbox').validatebox('options').required = false;
                        $("#Specs").combobox('validate');
                        $("#SBatchYear").combobox('options').required = false;
                        $("#SBatchYear").combobox('textbox').validatebox('options').required = false;
                        $("#SBatchYear").combobox('validate');
                        $("#SBatchWeek").combobox('options').required = false;
                        $("#SBatchWeek").combobox('textbox').validatebox('options').required = false;
                        $("#SBatchWeek").combobox('validate');
                        $("#EBatchYear").combobox('options').required = false;
                        $("#EBatchYear").combobox('textbox').validatebox('options').required = false;
                        $("#EBatchYear").combobox('validate');
                        $("#EBatchWeek").combobox('options').required = false;
                        $("#EBatchWeek").combobox('textbox').validatebox('options').required = false;
                        $("#EBatchWeek").combobox('validate');
                    }
                }
            });
            $('#FullEntry').numberbox({
                onChange: function () {
                    $('#CutEntry').numberbox({
                        min: 0,
                        max: parseInt($('#FullEntry').val()),
                        precision: 0
                    });
                }
            });
            $("#AStartDate").datebox({
                onSelect: function (beginDate) {
                    $('#AEndDate').datebox().datebox('calendar').calendar({
                        validator: function (date) {
                            var d1 = new Date(beginDate.getFullYear(), beginDate.getMonth(), beginDate.getDate());
                            //var d2 = new Date(beginDate.getFullYear(), beginDate.getMonth(), beginDate.getDate());
                            //d2.setDate(d1.getDate());
                            return d1 <= date; //&& date<=d2;
                        }
                    });
                }
            });
            $("#AEndDate").datebox({
                onSelect: function (beginDate) {
                    var beginTime = $('#AStartDate').datebox('getValue');
                    var endTime = $('#AEndDate').datebox('getValue');
                    if (beginTime != "" && endTime != "") {
                        var aDate, oDate1, oDate2, days;
                        aDate = beginTime.split("-");
                        oDate1 = new Date(aDate[0], aDate[1], aDate[2]);
                        aDate = endTime.split("-");
                        oDate2 = new Date(aDate[0], aDate[1], aDate[2]);
                        days = parseInt(Math.abs(oDate1 - oDate2) / 1000 / 60 / 60 / 24);
                        $("#DateNumLab").html("&nbsp;&nbsp;优惠时长" + (parseInt(days) + parseInt(1)) + "天");
                    }
                }
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
                    //轮胎规格
                    $('#ASID').combobox({
                        onSelect: function (fai) {
                            $('#Specs').combobox('clear');
                            var url = '../Price/priceApi.aspx?method=QueryAllSpesc&typeid=' + fai.TypeID + '&houseID=' + $("#HouseID").combobox('getValue');
                            $('#Specs').combobox('reload', url);
                            $('#Figure').combobox('clear');
                            var url = '../Price/priceApi.aspx?method=QueryAllFigureToTypeID&typeid=' + fai.TypeID + '&houseID=' + $("#HouseID").combobox('getValue');
                            $('#Figure').combobox('reload', url);
                            //轮胎花纹
                            $('#Specs').combobox({
                                onSelect: function (sp) {
                                    $('#Figure').combobox('clear');
                                    var url = '../Price/priceApi.aspx?method=QueryAllFigure&typeid=' + fai.TypeID + '&Specs=' + sp.Specs + '&houseID=' + $("#HouseID").combobox('getValue');
                                    $('#Figure').combobox('reload', url);
                                    $('#LoadIndex').combobox('clear');
                                    var url = '../Price/priceApi.aspx?method=QueryAllLoadIndex&typeid=' + fai.TypeID + '&Specs=' + sp.Specs + '&houseID=' + $("#HouseID").combobox('getValue');
                                    $('#LoadIndex').combobox('reload', url);
                                    $('#SpeedLevel').combobox('clear');
                                    var url = '../Price/priceApi.aspx?method=QueryAllSpeedLevel&typeid=' + fai.TypeID + '&Specs=' + sp.Specs + '&houseID=' + $("#HouseID").combobox('getValue');
                                    $('#SpeedLevel').combobox('reload', url);
                                }
                            });
                        }
                    });
                }
            });
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
                title: '适用仓库', field: 'HouseName', width: '60px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '适用品牌', field: 'TypeName', width: '80px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '适用规格', field: 'Specs', width: '80px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            if (HID == "47") {
                columns.push({
                    title: '适用长和编码', field: 'Figure', width: '80px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            } else {
                columns.push({
                    title: '适用花纹', field: 'Figure', width: '80px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                });
            }
            columns.push({
                title: '适用载重', field: 'LoadIndex', width: '80px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '适用速度', field: 'SpeedLevel', width: '80px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '适用周期', field: 'Batch', width: '80px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '适用客户', field: 'SuitClientNum', width: '180px', formatter: function (value) {
                    return "<span title='" + value + "'>" + value + "</span>";
                }
            });
            columns.push({
                title: '满额享受', field: 'MeetLimit', width: '80px', formatter: function (value) {
                    if (value == 0) {
                        return "<span title='否'>否</span>";
                    } else {
                        return "<span title='是'>是</span>";
                    }
                }
            });
            columns.push({
                title: '常规促销', field: 'Regular', width: '80px', formatter: function (value) {
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
            var datenow = new Date();
            $('#StartDate').datebox('setValue', getLastDayFormatDate(datenow));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));

            //客户姓名
            $('#SuitClientNum').combobox({
                valueField: 'ClientNum', textField: 'Boss', AddField: 'PinyinName', delay: '10',
                url: '../Client/clientApi.aspx?method=AutoCompleteClient',
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                }
            });

            $("input[name=IsSuperPosition]").click(function () { showCont(); });
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'priceApi.aspx?method=QueryRuleBankData';
            $('#dg').datagrid('load', {
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue'),
                HID: $("#HID").combobox('getValue'),
                Title: $('#Title').val()
            });
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
                $('#dlg').dialog('open').dialog('setTitle', '修改产品基础价格');
                $('#ASID').combobox('clear');
                $('#Specs').combobox('clear');
                $('#Figure').combobox('clear');
                $('#SBatchYear').combobox('clear');
                $('#SBatchWeek').combobox('clear');
                $('#EBatchYear').combobox('clear');
                $('#EBatchWeek').combobox('clear');
                $('#FullEntry').next(".numberbox").hide();
                $('#Rule2').next(".combo").hide();
                $('#Rule3').next(".combo").hide();
                $('#CutEntry').next(".numberbox").hide();
                $('#Rule4').next(".combo").hide();
                $('#RuleTr1').show();
                $('#Rule5').removeAttr('checked');
                $('#Hrule').val(0);
                $('#RuleName').textbox('setValue', rows[0].RuleName);
                $('#ID').val(rows[0].ID);

                $('#AEndDate').datebox().datebox('calendar').calendar({
                    validator: function (date) {
                        var d1 = new Date(rows[0].StartDate);
                        return d1 <= date;
                    }
                });

                switch (rows[0].RuleType) {
                    case "0"://满n元减n元	
                        var str = rows[0].Title;
                        $("#FullEntry").numberbox('setValue', str.substring(str.indexOf("满") + 1, str.indexOf("减")));
                        $("#CutEntry").numberbox('setValue', str.substring(str.indexOf("减") + 1, str.indexOf("元")));
                        $('#Rule1').combobox('setValue', 0);
                        $('#FullEntry').next(".numberbox").show();
                        $("#FullEntry").numberbox('options').required = true;
                        $("#FullEntry").numberbox('textbox').validatebox('options').required = true;
                        $("#FullEntry").numberbox('validate');

                        $('#Rule2').combobox('setValue', 0);
                        $('#Rule2').next(".combo").show();
                        $("#Rule2").combobox('options').required = true;
                        $("#Rule2").combobox('textbox').validatebox('options').required = true;
                        $("#Rule2").combobox('validate');
                        $("#Rule2").combobox("readonly", false);

                        $('#Rule3').combobox('setValue', 1);
                        $('#Rule3').next(".combo").show();
                        $("#Rule3").combobox("readonly", true);

                        $('#Rule4').combobox('setValue', 0);
                        $('#Rule4').next(".combo").show();
                        $("#Rule4").combobox("readonly", true);

                        $('#CutEntry').next(".numberbox").show();
                        $("#CutEntry").numberbox('options').required = true;
                        $("#CutEntry").numberbox('textbox').validatebox('options').required = true;
                        $("#CutEntry").numberbox('validate');
                        break;
                    case "1"://满n条送n条轮胎	
                        var str = rows[0].Title;
                        $("#FullEntry").numberbox('setValue', str.substring(str.indexOf("满") + 1, str.indexOf("条")));
                        $("#CutEntry").numberbox('setValue', str.substring(str.indexOf("送") + 1, str.lastIndexOf("条")));
                        $('#Rule1').combobox('setValue', 0);
                        $('#FullEntry').next(".numberbox").show();
                        $("#FullEntry").numberbox('options').required = true;
                        $("#FullEntry").numberbox('textbox').validatebox('options').required = true;
                        $("#FullEntry").numberbox('validate');

                        $('#Rule2').combobox('setValue', 1);
                        $('#Rule2').next(".combo").show();
                        $("#Rule2").combobox('options').required = true;
                        $("#Rule2").combobox('textbox').validatebox('options').required = true;
                        $("#Rule2").combobox('validate');
                        $("#Rule2").combobox("readonly", false);

                        $('#Rule3').combobox('setValue', 0);
                        $('#Rule3').next(".combo").show();
                        $("#Rule3").combobox("readonly", true);

                        $('#Rule4').combobox('setValue', 1);
                        $('#Rule4').next(".combo").show();
                        $("#Rule4").combobox("readonly", true);

                        $('#CutEntry').next(".numberbox").show();
                        $("#CutEntry").numberbox('options').required = true;
                        $("#CutEntry").numberbox('textbox').validatebox('options').required = true;
                        $("#CutEntry").numberbox('validate');
                        break;
                    case "2"://打n折轮胎
                        var str = rows[0].Title;
                        $("#FullEntry").numberbox('setValue', str.substring(str.indexOf("轮胎") + 2, str.indexOf("折")));
                        $('#Rule1').combobox('setValue', 2);
                        $('#FullEntry').next(".numberbox").show();
                        $("#FullEntry").numberbox('options').required = true;
                        $("#FullEntry").numberbox('textbox').validatebox('options').required = true;
                        $("#FullEntry").numberbox('validate');
                        break;
                    case "4"://限购n条
                        var str = rows[0].Title;
                        $("#FullEntry").numberbox('setValue', str.substring(str.indexOf("限购") + 2, str.indexOf("条")));
                        $('#Rule1').combobox('setValue', 1);
                        $('#FullEntry').next(".numberbox").show();
                        $("#FullEntry").numberbox('options').required = true;
                        $("#FullEntry").numberbox('textbox').validatebox('options').required = true;
                        $("#FullEntry").numberbox('validate');

                        $('#Rule2').combobox('setValue', 1);
                        $('#Rule2').next(".combo").show();
                        $("#Rule2").combobox('options').required = true;
                        $("#Rule2").combobox('textbox').validatebox('options').required = true;
                        $("#Rule2").combobox('validate');
                        $("#Rule2").combobox("readonly", true);
                        break;
                    case "5"://APP商城首单送100元优惠券
                        $('#RuleTr1').hide();
                        $('#Rule5').prop('checked', true);
                        $('#Hrule').val(1);
                        break;
                    case "6"://直减n元
                        var str = rows[0].Title;
                        $("#FullEntry").numberbox('setValue', str.substring(str.indexOf("直减") + 2, str.indexOf("元")));
                        $('#Rule1').combobox('setValue', 3);
                        $('#FullEntry').next(".numberbox").show();
                        $("#FullEntry").numberbox('options').required = true;
                        $("#FullEntry").numberbox('textbox').validatebox('options').required = true;
                        $("#FullEntry").numberbox('validate');

                        $('#Rule2').combobox('setValue', 0);
                        $('#Rule2').next(".combo").show();
                        $("#Rule2").combobox('options').required = true;
                        $("#Rule2").combobox('textbox').validatebox('options').required = true;
                        $("#Rule2").combobox('validate');
                        $("#Rule2").combobox("readonly", true);
                        break;

                }

                $('#APID').combobox({
                    url: '../Product/productApi.aspx?method=QueryALLOneProductType', valueField: 'TypeID', textField: 'TypeName',
                });
                $.ajax({
                    url: "../Product/productApi.aspx?method=QueryParentID&TypeID=" + rows[0].TypeID,
                    cache: false,
                    async: false,
                    success: function (text) {
                        if (text.Result == null) {
                            $('#APID').combobox('setValue', text);
                        }
                    }
                });
                $('#ASID').combobox({
                    url: '../Product/productApi.aspx?method=QueryALLOneProductType&PID=' + $('#APID').combobox('getValue'), valueField: 'TypeID', textField: 'TypeName', AddField: 'PinyinName',
                    filter: function (q, row) {
                        var opts = $(this).combobox('options');
                        return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                    },
                });
                $('#ASID').combobox('setValue', rows[0].TypeID);
                var arr = rows[0].SuitClientNum.split(',');
                var valueArr = new Array();
                for (var i = 0; i < arr.length; i++) {
                    valueArr.push(arr[i]);
                }
                $('#SuitClientNum').combobox("setValues", valueArr);
                if (rows[0].MeetLimit == "1") { $('#MeetLimit').prop('checked', true); } else { $('#MeetLimit').prop('checked', false); }
                if (rows[0].Regular == "1") { $('#Regular').prop('checked', true); } else { $('#Regular').prop('checked', false); }
                //轮胎花纹
                $('#Specs').combobox({
                    onSelect: function (sp) {
                        var url = '../Price/priceApi.aspx?method=QueryAllFigure&typeid=' + $('#ASID').combobox('getValue') + '&Specs=' + sp.Specs + '&houseID=' + $("#HouseID").combobox('getValue');
                        $('#Figure').combobox('reload', url);
                        var url = '../Price/priceApi.aspx?method=QueryAllLoadIndex&typeid=' + $('#ASID').combobox('getValue') + '&Specs=' + sp.Specs + '&houseID=' + $("#HouseID").combobox('getValue');
                        $('#LoadIndex').combobox('reload', url);
                        var url = '../Price/priceApi.aspx?method=QueryAllSpeedLevel&typeid=' + $('#ASID').combobox('getValue') + '&Specs=' + sp.Specs + '&houseID=' + $("#HouseID").combobox('getValue');
                        $('#SpeedLevel').combobox('reload', url);
                    }
                });
                //所在仓库
                $('#HouseID').combobox({
                    url: '../House/houseApi.aspx?method=CargoPermisionHouse', valueField: 'HouseID', textField: 'Name',
                });
                $('#HouseID').combobox('setValue', rows[0].HouseID);
                let startDate = rows[0].StartDate.split('T');
                let endDate = rows[0].EndDate.split('T');
                $('#AStartDate').datebox('setValue', startDate[0]);
                $('#AStartDate').datebox('setText', startDate[0]);
                $('#AEndDate').datebox('setValue', endDate[0]);
                $('#AEndDate').datebox('setText', endDate[0]);
                var url = '../Price/priceApi.aspx?method=QueryAllSpesc&typeid=' + rows[0].TypeID;
                $('#Specs').combobox('reload', url);
                if (rows[0].Specs != null) {
                    $('#Specs').combobox('setValue', rows[0].Specs);
                }
                if (rows[0].Figure != null) {
                    $('#Figure').combobox('setValue', rows[0].Figure);
                }
                if (rows[0].LoadIndex != null) {
                    $('#LoadIndex').combobox('setValue', rows[0].LoadIndex);
                }
                if (rows[0].SpeedLevel != null) {
                    $('#SpeedLevel').combobox('setValue', rows[0].SpeedLevel);
                }
                if (rows[0].StartBatch != 0) {
                    $('#SBatchYear').combobox('setValue', (rows[0].StartBatch + "").substr(0, 2));
                    $('#SBatchWeek').combobox('setValue', (rows[0].StartBatch + "").substr(2, 2));
                    $('#EBatchYear').combobox('setValue', (rows[0].EndBatch + "").substr(0, 2));
                    $('#EBatchWeek').combobox('setValue', (rows[0].EndBatch + "").substr(2, 2));
                }

                var beginTime = $('#AStartDate').datebox('getValue');
                var endTime = $('#AEndDate').datebox('getValue');
                if (beginTime != "" && endTime != "") {
                    var aDate, oDate1, oDate2, days;
                    aDate = beginTime.split("-");
                    oDate1 = new Date(aDate[0], aDate[1], aDate[2]);
                    aDate = endTime.split("-");
                    oDate2 = new Date(aDate[0], aDate[1], aDate[2]);
                    days = parseInt(Math.abs(oDate1 - oDate2) / 1000 / 60 / 60 / 24);
                    $("#DateNumLab").html("&nbsp;&nbsp;优惠时长" + (parseInt(days) + parseInt(1)) + "天");
                }

               
            }
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
                    <input id="HID" class="easyui-combobox" data-options="url:'../House/houseApi.aspx?method=CargoPermisionHouse',method:'get',valueField:'HouseID',textField:'Name'" style="width: 100px;" />
                </td>
                <td style="text-align: right;">有效期间:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px"></td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()">
            &nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" iconcls="icon-edit"
                plain="false" onclick="editItem()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
                    iconcls="icon-cut" plain="false" onclick="DelItem()">&nbsp;删&nbsp;除&nbsp;</a>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 700px; height: 450px; padding: 1px 1px"
        closed="true" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" id="Hrule" name="Hrule" />
            <input type="hidden" id="ID" name="ID" />

            <div id="saPanel">
                <table>
                    <tr>
                        <td style="text-align: right;">促销条件：</td>
                        <td>
                            <input type="checkbox" name="MeetLimit" id="MeetLimit" value="1" />
                            <label id="MeetLimitLab" for="MeetLimit">满额享受</label>
                            <input type="checkbox" name="Regular" id="Regular" value="1" />
                            <label id="RegularLab" for="Regular">常规促销</label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">规则名称：</td>
                        <td>
                            <input name="RuleName" id="RuleName" class="easyui-textbox" data-options="prompt:'可为空，显示在规则下方',required:false" style="width: 300px;"/>
                        </td>
                    </tr>
                    <tr id="RuleTr1">
                        <td style="text-align: right;">促销规则：</td>
                        <td>
                            <select class="easyui-combobox" id="Rule1" name="Rule1" style="width: 60px;" panelheight="auto" editable="false" data-options="onSelect:onRuleChanged">
                                <option value="0">满</option>
                                <option value="3">直减</option>
                                <option value="1">限购</option>
                                <option value="2">打折</option>
                            </select>

                            <input name="FullEntry" id="FullEntry" data-options="min:0,precision:0" class="easyui-numberbox" style="width: 80px;" />

                            <select class="easyui-combobox" id="Rule2" name="Rule2" style="width: 60px;" editable="false" panelheight="auto" data-options="onSelect:onRule2Changed">
                                <option value="0">元</option>
                                <option value="1">条</option>
                            </select>
                            <select class="easyui-combobox" id="Rule3" name="Rule3" style="width: 60px;" editable="false" panelheight="auto">
                                <option value="0">送</option>
                                <option value="1">减</option>
                            </select>
                            <input name="CutEntry" id="CutEntry" data-options="min:0,precision:0" class="easyui-numberbox" style="width: 80px;" />
                            <select class="easyui-combobox" id="Rule4" name="Rule4" style="width: 60px;" editable="false" panelheight="auto">
                                <option value="0">元</option>
                                <option value="1">条</option>
                            </select>
                        </td>
                    </tr>
                    <%--<tr>
                        <td style="text-align: right;">促销规则2：</td>
                        <td>APP商城首单送100元优惠券(1张30元、2张20元、2张10元、2张5元)  
                            <input type="checkbox" name="Rule5" id="Rule5" /></td>
                    </tr>--%>
                    <tr>
                        <td style="text-align: right;">有效期间：</td>
                        <td>
                            <input id="AStartDate" name="StartDate" class="easyui-datebox" style="width: 100px" data-options="required:true" />~
                    <input id="AEndDate" name="EndDate" class="easyui-datebox" style="width: 100px" data-options="required:true" /><label id="DateNumLab" style="text-align: left;"></label></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">适用仓库：</td>
                        <td>
                            <input id="HouseID" name="HouseID" class="easyui-combobox" style="width: 300px;" data-options="multiple:false,required:true" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">适用品牌：</td>
                        <td>一级：<input id="APID" class="easyui-combobox" style="width: 100px;"
                            panelheight="auto" data-options="required:true" />&nbsp;&nbsp;二级：<input id="ASID" name="TypeID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'TypeID',textField:'TypeName',required:true" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">适用规格：</td>
                        <td>
                            <input id="Specs" name="Specs" class="easyui-combobox" style="width: 300px;" data-options="valueField:'Specs',textField:'Specs',required:true" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">适用花纹：</td>
                        <td>
                            <input id="Figure" name="Figure" class="easyui-combobox" style="width: 300px;" data-options="valueField:'Figure',textField:'Figure',required:true" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">适用载重：</td>
                        <td>
                            <input id="LoadIndex" name="LoadIndex" class="easyui-combobox" style="width: 300px;" data-options="valueField:'LoadIndex',textField:'LoadIndex',required:false" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">适用速度：</td>
                        <td>
                            <input id="SpeedLevel" name="SpeedLevel" class="easyui-combobox" style="width: 300px;" data-options="valueField:'SpeedLevel',textField:'SpeedLevel',required:false" /></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">适用周期：</td>
                        <td>从<select class="easyui-combobox" id="SBatchYear" name="SBatchYear" style="width: 50px;" panelheight="auto" data-options="required:true">
                            <option value="24">24</option>
                            <option value="23">23</option>
                            <option value="22">22</option>
                            <option value="21">21</option>
                            <option value="20">20</option>
                            <option value="19">19</option>
                            <option value="18">18</option>
                        </select>年<input class="easyui-combobox" id="SBatchWeek" name="SBatchWeek" data-options="url:'../Data/TyreBatch.json',method:'get',valueField:'id',textField:'text',required:true" style="width: 50px;">周-------<select class="easyui-combobox" id="EBatchYear" name="EBatchYear" style="width: 50px;" panelheight="auto" data-options="required:true">
                             <option value="24">24</option>
                            <option value="23">23</option>
                                <option value="22">22</option>
                                <option value="21">21</option>
                                <option value="20">20</option>
                                <option value="19">19</option>
                                <option value="18">18</option>
                            </select>年<input class="easyui-combobox" id="EBatchWeek" name="EBatchWeek" data-options="url:'../Data/TyreBatch.json',method:'get',valueField:'id',textField:'text',required:true" style="width: 50px;">周
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">适用客户：</td>
                        <td>
                            <input name="SuitClientNum" id="SuitClientNum" style="width: 300px;" class="easyui-combobox" data-options="valueField:'ClientNum',textField:'Boss',editable:true,required:false,multiple:true" />
                        </td>
                    </tr>
                    <tr>
    <td style="text-align: right;">是否叠加使用:</td>
    <td>
        <input name="IsSuperPosition" id="IsSuperPosition0" type="radio" value="0" /><label for="IsSuperPosition0" style="font-size: 15px;">不可叠加</label>
        <input name="IsSuperPosition" id="IsSuperPosition1" type="radio" value="1" /><label for="IsSuperPosition1" style="font-size: 15px;">可叠加使用</label>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <input type="checkbox" id="IsFollowQuantity" name="IsFollowQuantity" value="0" />
        <label for="IsFollowQuantity">限制一条使用一张</label>
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
        function showCont() {
            switch ($("input[name=IsSuperPosition]:checked").attr("id")) {
                case "IsSuperPosition0":
                    $('#IsFollowQuantity').prop('disabled', true);
                    $('#IsFollowQuantity').val('0');
                    break;
                case "IsSuperPosition1":
                    $('#IsFollowQuantity').prop('disabled', false);
                    break;
                default:
                    break;
            }
        }

        //保存规则
        function saveRuleBank() {
            //促销规则1
            var AStartDate = $('#AStartDate').datebox('getValue');
            if (AStartDate == undefined || AStartDate == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择有效期间！', 'warning');
                $('#AStartDate').textbox('textbox').focus();
                return;
            }
            var AEndDate = $('#AEndDate').datebox('getValue');
            if (AEndDate == undefined || AEndDate == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择有效期间！', 'warning');
                $('#AEndDate').textbox('textbox').focus();
                return;
            }
            var hid = $("#HouseID").combobox('getValue');
            if (hid == undefined || hid == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择适用仓库！', 'warning');
                $('#HouseID').textbox('textbox').focus();
                return;
            }
            var ck = $("#Rule5").prop('checked');
            if (ck == true) {

            } else {

            }
            $('#fm').form('submit', {
                url: 'priceApi.aspx?method=saveRuleBank',
                onSubmit: function () {
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        if ($("#ID").val() != "") {
                            $('#dlg').dialog('close');
                        }
                        dosearch();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }
        //新增规则
        function addItem() {
            $('#dlg').dialog('open').dialog('setTitle', '新增促销规则');
            $("#Specs").combobox('options').required = true;
            $("#Specs").combobox('textbox').validatebox('options').required = true;
            $("#Specs").combobox('validate');
            $("#Figure").combobox('options').required = true;
            $("#Figure").combobox('textbox').validatebox('options').required = true;
            $("#Figure").combobox('validate');
            $("#SBatchYear").combobox('options').required = true;
            $("#SBatchYear").combobox('textbox').validatebox('options').required = true;
            $("#SBatchYear").combobox('validate');
            $("#SBatchWeek").combobox('options').required = true;
            $("#SBatchWeek").combobox('textbox').validatebox('options').required = true;
            $("#SBatchWeek").combobox('validate');
            $("#EBatchYear").combobox('options').required = true;
            $("#EBatchYear").combobox('textbox').validatebox('options').required = true;
            $("#EBatchYear").combobox('validate');
            $("#EBatchWeek").combobox('options').required = true;
            $("#EBatchWeek").combobox('textbox').validatebox('options').required = true;
            $("#EBatchWeek").combobox('validate');

            $('#fm').form('clear');
            $('#RuleTr1').show();
            $('#FullEntry').next(".numberbox").hide();
            $('#Rule2').next(".combo").hide();
            $('#Rule3').next(".combo").hide();
            $('#CutEntry').next(".numberbox").hide();
            $('#Rule4').next(".combo").hide();
            $('#Hrule').val(0);
            //所在仓库
            $('#HouseID').combobox({
                url: '../House/houseApi.aspx?method=CargoPermisionHouse', valueField: 'HouseID', textField: 'Name',
            });

            $('#HouseID').combobox('textbox').bind('focus', function () { $('#HouseID').combobox('showPanel'); });
        }
        $("#Rule5").change(function () {
            var ck = $("#Rule5").prop('checked');
            if (ck == true) {
                //$('#FullEntry').numberbox('clear');
                //$('#CutEntry').numberbox('clear');
                //$('#Rule1').combobox('clear');
                //$('#Rule2').combobox('clear');
                //$('#Rule3').combobox('clear');
                //$('#Rule4').combobox('clear');
                $('#RuleTr1').hide();
                $('#Hrule').val(1);
            } else {
                //$('#FullEntry').numberbox('clear');
                //$('#CutEntry').numberbox('clear');
                //$('#Rule1').combobox('clear');
                //$('#Rule2').combobox('clear');
                //$('#Rule3').combobox('clear');
                //$('#Rule4').combobox('clear');
                $('#RuleTr1').show();
                $('#Hrule').val(0);
            }
        });
        //
        function onRuleChanged(item) {
            if (item) {
                //$('#FullEntry').numberbox('clear');
                //$('#CutEntry').numberbox('clear');
                //$('#Rule2').combobox('clear');
                //$('#Rule3').combobox('clear');
                //$('#Rule4').combobox('clear');
                $('#FullEntry').next(".numberbox").hide();
                $('#Rule2').next(".combo").hide();
                $('#Rule3').next(".combo").hide();
                $('#CutEntry').next(".numberbox").hide();
                $('#Rule4').next(".combo").hide();
                switch (item.value) {
                    case "0"://满减
                        $('#FullEntry').next(".numberbox").show();
                        $("#FullEntry").numberbox('options').required = true;
                        $("#FullEntry").numberbox('textbox').validatebox('options').required = true;
                        $("#FullEntry").numberbox('validate');

                        $('#Rule2').next(".combo").show();
                        $("#Rule2").combobox('options').required = true;
                        $("#Rule2").combobox('textbox').validatebox('options').required = true;
                        $("#Rule2").combobox('validate');
                        $('#Rule2').combobox('clear');
                        $("#Rule2").combobox("readonly", false);
                        break;
                    case "1"://限购
                        $('#FullEntry').next(".numberbox").show();
                        $('#Rule2').next(".combo").show();
                        $('#Rule2').combobox('setValue', 1);
                        $("#Rule2").combobox("readonly", true);
                        break;
                    case "2"://打折
                        $('#FullEntry').next(".numberbox").show();
                        break;
                    case "3"://直减
                        $('#FullEntry').next(".numberbox").show();
                        $('#Rule2').next(".combo").show();
                        $('#Rule2').combobox('setValue', 0);
                        $("#Rule2").combobox("readonly", true);
                        break;

                }
            }
        }
        function onRule2Changed(item) {
            if (item) {
                switch (item.value) {
                    case "0"://元
                        $('#Rule3').combobox('setValue', 1);
                        $('#Rule4').combobox('setValue', 0);
                        break;
                    case "1"://条
                        $('#Rule3').combobox('setValue', 0);
                        $('#Rule4').combobox('setValue', 1);
                        break;
                }
                $('#Rule3').next(".combo").show();
                $("#Rule3").combobox("readonly", true);
                $('#Rule4').next(".combo").show();
                $("#Rule4").combobox("readonly", true);
                $('#CutEntry').next(".numberbox").show();
                $("#CutEntry").numberbox('options').required = true;
                $("#CutEntry").numberbox('textbox').validatebox('options').required = true;
                $("#CutEntry").numberbox('validate');
            }
        }
        //删除规则
        function DelItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定删除该规则？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'priceApi.aspx?method=DelRuleBank',
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

    </script>
</asp:Content>
