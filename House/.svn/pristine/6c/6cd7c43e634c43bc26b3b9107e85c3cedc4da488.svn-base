<%@ Page Title="考勤管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="qyCheckin.aspx.cs" Inherits="Cargo.QY.qyCheckin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">

        //页面加载执行查询
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
                fitColumns: true, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'ID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                  { title: '', field: 'ID', checkbox: true, width: '30px' },
                  {
                      title: '姓名', field: 'UserName', width: '80px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '所属部门', field: 'DepartmentName', width: '95px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '所属规则', field: 'GroupName', width: '100px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '打卡类型', field: 'CheckinType', width: '90px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '异常类型', field: 'ExceptionType', width: '180px', formatter: function (value) {
                          if (value == null || value == "") {
                              return "<span title='无异常'>无异常</span>";
                          } else {
                              return "<span title='" + value + "'>" + value + "</span>";
                          }
                      }
                  },
                  {
                      title: '打卡日期', field: 'CheckinTime', width: '90px', formatter: function (value, row, index) {
                          row.time = value;
                          return "<span title='" + value.split("T")[0] + "'>" + value.split("T")[0] + "</span>";
                      }
                  },
                  {
                      title: '打卡时间', field: 'time', width: '90px', formatter: function (value) {
                          return "<span title='" + value.split("T")[1] + "'>" + value.split("T")[1] + "</span>";
                      }
                  },
                  {
                      title: '规则时间', field: 'SchCheckinTime', width: '90px', formatter: function (value) {
                          return "<span title='" + value.split("T")[1] + "'>" + value.split("T")[1] + "</span>";
                      }
                  },
                  {
                      title: '打卡地点', field: 'LocationDetail', width: '250px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: 'WiFi名称', field: 'WifiName', width: '150px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '备注', field: 'Notes', width: '300px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '附件', field: 'MediaPath', width: '50px', formatter: function (value) {
                          if (value != null && value != "") {
                              return "<img onclick=download(\"" + value + "\") style='width:30px; height:30px;margin-left:3px;' src='" + window.location.href.substring(0, window.location.href.indexOf(window.location.pathname)) + "//CheckinImage//" + value + "' title='点击查看图片'/>";
                          } else {
                              return "";
                          }
                      }
                  },
                  { title: '打卡经度', field: 'Lat', hidden: true },
                  { title: '打卡纬度', field: 'Lng', hidden: true },
                  {
                      field: 'opt', title: '位置', width: '50px', align: 'left',
                      formatter: function (value, row, index) {
                          if (value == undefined && row.ExceptionType != "未打卡") {
                              var btn = '<a class="editcls" iconcls="icon-search" onclick="openMap(\'' + row.Lat + '\',\'' + row.Lng + '\')" href="javascript:void(0)">地图</a>';
                              return btn;
                          }
                      }
                  }
                ]],
                onLoadSuccess: function (data) {
                    var trs = $(this).prev().find('div.datagrid-body').find('tr');
                    for (var i = 0; i < trs.length; i++) {
                        if ($(trs[i].cells[4]).find('div').text() == "上班打卡" && $(trs[i].cells[5]).find('div').text().indexOf("时间异常") >= 0 && $(trs[i].cells[7]).find('div').text().substring(0, 5) > $(trs[i].cells[8]).find('div').text().substring(0, 5)) {
                            trs[i].cells[4].style.cssText = 'background:#ffc1c9;';
                            trs[i].cells[7].style.cssText = 'background:#ffc1c9;';
                        }
                        if ($(trs[i].cells[4]).find('div').text() == "下班打卡" && $(trs[i].cells[5]).find('div').text().indexOf("时间异常") >= 0 && $(trs[i].cells[7]).find('div').text().substring(0, 5) < $(trs[i].cells[8]).find('div').text().substring(0, 5)) {
                            trs[i].cells[4].style.cssText = 'background:#ffc1c9;';
                            trs[i].cells[7].style.cssText = 'background:#ffc1c9;';
                        }
                        if ($(trs[i].cells[5]).find('div').text() == "未打卡") {
                            trs[i].cells[7].textContent = ""
                            trs[i].cells[7].style.cssText = 'background:#ffe9b7;';
                        }
                    }
                },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                rowStyler: function (index, row) {
                    if (row.ExceptionType == "未打卡") {
                        //return "background-color:#ffe9b7";
                    }
                },
                onDblClickRow: function (index, row) { editItem(); },
            });

            var datenow = new Date();
            $('#StartDate').datebox('setValue', FunGetDateStr(3));
            $('#EndDate').datebox('setValue', getNowFormatDate(datenow));
        });
        function FunGetDateStr(p_count) {
            var dd = new Date();
            dd.setDate(dd.getDate() - p_count);//获取p_count天前的日期
            var y = dd.getFullYear();
            var m = dd.getMonth() - 1;//获取当前月份的日期
            if( m <10){
                m = '0'+m;
            }
            var d = dd.getDate();
            if( d <10){
                d = '0'+d;
            }
            return y + "-" + m + "-" + d;
        }
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'wxQYServices.aspx?method=QueryCheckinData';
            $('#dg').datagrid('load', {
                CheckinType: $('#CheckinType').combobox('getValue'),
                ExceptionType: $('#ExceptionType').combobox('getValue'),
                UserName: $("#UserName").val(),
                StartDate: $('#StartDate').datebox('getValue'),
                EndDate: $('#EndDate').datebox('getValue')
            });
        }
        function openMap(Lat, Lng) {
            $('#map').dialog('open').dialog('setTitle', '打卡地图');
            //定义地图中心点坐标
            var center = new TMap.LatLng(Lat / 1000000, Lng / 1000000)
            //定义全局map变量，调用 TMap.Map() 构造函数创建地图
            map = new TMap.Map(document.getElementById('map'), {
                center: center,//设置地图中心点坐标
                zoom: 18,   //设置地图缩放级别
                pitch: 20,  //设置俯仰角
                rotation: 0    //设置地图旋转角度
            });
            //初始化marker
            var marker = new TMap.MultiMarker({
                id: "marker-layer", //图层id
                map: map,
                styles: { //点标注的相关样式
                    "marker": new TMap.MarkerStyle({
                        "width": 25,
                        "height": 35,
                        "anchor": { x: 16, y: 32 },
                        "src": "https://mapapi.qq.com/web/lbs/javascriptGL/demo/img/markerDefault.png"
                    })
                },
                geometries: [{ //点标注数据数组
                    "id": "demo",
                    "styleId": "marker",
                    "position": new TMap.LatLng(Lat / 1000000, Lng / 1000000),
                    "properties": {
                        "title": "marker"
                    }
                }]
            });
        }
        function destroy() {
            map.destroy();
        }

        //查询汇总
        function summary() {
            var dd = new Date();
            var y = dd.getFullYear();
            var m = dd.getMonth() + 1;
            if (m < 10) (m = "0" + m);
            $('#ReportTime').datebox('setValue', y + "-" + m);
            var day = new Date().getDate() - 1;
            var mulCol = [];
            var bt = [
                {
                    title: '部门', field: 'DepartmentName', rowspan: 2, width: '80px', formatter: function (value) {
                        return "<span title='" + value + "'>" + value + "</span>";
                    }
                },
                { title: '概括', colspan: 5, width: '300px' },
                { title: '加班情况(时长)', colspan: 3, width: '300px' },
                { title: '异常情况', colspan: 9, width: '500px' },
                { title: '假勤情况', colspan: 12, width: '500px' }
            ];
            var rc = [
                    {
                        title: '应打卡天数', field: 'WorkDays', rowspan: 1, width: '70px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '正常天数', field: 'RegularDays', rowspan: 1, width: '60px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '异常天数', field: 'ExceptDays', rowspan: 1, width: '60px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '标准工作时长', field: 'StandardWorkSec', rowspan: 1, width: '80px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + (Number(value) / 3600).toFixed(0) + "小时'>" + (Number(value) / 3600).toFixed(0) + "小时</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '实际工作时长', field: 'RegularWorkSec', width: '80px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + (Number(value) / 3600).toFixed(1) + "小时'>" + (Number(value) / 3600).toFixed(1) + "小时</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '工作日加班', field: 'WorkdayOverSec', rowspan: 1, width: '80px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + (Number(value) / 3600).toFixed(1) + "小时'>" + (Number(value) / 3600).toFixed(1) + "小时</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '节假日加班', field: 'HolidaysOverSec', rowspan: 1, width: '80px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + (Number(value) / 3600).toFixed(1) + "小时'>" + (Number(value) / 3600).toFixed(1) + "小时</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '休息日加班', field: 'RestdaysOverSec', rowspan: 1, width: '80px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + (Number(value) / 3600).toFixed(1) + "小时'>" + (Number(value) / 3600).toFixed(1) + "小时</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '迟到次数', field: 'LateCount', rowspan: 1, width: '70px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '迟到时长', field: 'LateDuration', rowspan: 1, width: '70px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + Number(value) / 60 + "分钟'>" + Number(value) / 60 + "分钟</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '早退次数', field: 'LeaveEarlyCount', rowspan: 1, width: '70px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '早退时长', field: 'LeaveEarlyDuration', rowspan: 1, width: '70px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + Number(value) / 60 + "分钟'>" + Number(value) / 60 + "分钟</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '缺卡次数', field: 'AbsenceCount', rowspan: 1, width: '70px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '旷工次数', field: 'AbsenteeismCount', rowspan: 1, width: '70px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '旷工时长', field: 'AbsenteeismDuration', rowspan: 1, width: '70px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + (Number(value) / 3600).toFixed(0) + "小时'>" + (Number(value) / 3600).toFixed(0) + "小时</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '地点异常', field: 'LocationAbnormalCount', rowspan: 1, width: '70px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '设备异常', field: 'EquipmentAbnormalCount', rowspan: 1, width: '70px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '补卡次数', field: 'CardReplacementCount', rowspan: 1, width: '80px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '外勤次数', field: 'FieldCount', rowspan: 1, width: '80px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '外出', field: 'EgressCount', rowspan: 1, width: '80px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '出差', field: 'BusinessCount', rowspan: 1, width: '80px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '年假', field: 'AnnualLeaveCount', rowspan: 1, width: '80px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '事假', field: 'CompassionateLeaveCount', rowspan: 1, width: '80px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '病假', field: 'SickLeaveCount', rowspan: 1, width: '80px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '调休假', field: 'CompensatoryLeaveCount', rowspan: 1, width: '80px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '婚假', field: 'MarriageHolidayCount', rowspan: 1, width: '80px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '产假', field: 'MaternityLeaveCount', rowspan: 1, width: '80px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '陪产假', field: 'PaternityLeaveCount', rowspan: 1, width: '80px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    },
                    {
                        title: '其他', field: 'OtherLeaveCount', rowspan: 1, width: '80px', formatter: function (value) {
                            if (value != 0) {
                                return "<span title='" + value + "'>" + value + "</span>";
                            } else {
                                return "<span title='--'>--</span>";
                            }
                        }
                    }
            ];
            var tit = { title: '月概括', colspan: day };
            for (var i = 1; i <= day; i++) {
                var info = {
                    title: i, field: 'info'+ i.toString(), width: '80px', formatter: function (value) {
                        if (value != "" && value != null) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        } else {
                            return "<span title='--'>--</span>";
                        }
                    }
                };
                rc.push(info);
            }
            bt.push(tit);
            mulCol.push(bt);
            mulCol.push(rc);


            $('#Summarydlg').dialog('open').dialog('setTitle', '查询汇总数据');
            $('#dgSummary').datagrid('clearSelections');
            $('#dgSummary').datagrid('loadData', []);

            $('#dgSummary').datagrid({
                width: '100%',
                height: '95%',
                title: '', //标题内容
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                pagination: false, //分页是否显示
                rownumbers: true,//显示序号
                pageSize: 12, //每页多少条
                pageList: [12, 20],
                fitColumns: true, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: true, //设置为 true，则只允许选中一行。
                checkOnSelect: false, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                url: null,
                toolbar: '#dgsummarybar',
                frozenColumns: [[
                  {
                      title: '姓名', field: 'Name', width: '70px', formatter: function (value) {
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
                onDblClickRow: function (index, row) { }
            });
            dosearchSummary();
        }
        //查询汇总数据
        function dosearchSummary() {
            $('#dgSummary').datagrid('clearSelections');
            var gridOpts = $('#dgSummary').datagrid('options');
            gridOpts.url = 'wxQYServices.aspx?method=QueryCheckinReportData';
            $('#dgSummary').datagrid('load', {
                ReportTime: $("#ReportTime").datebox('getValue').replace('-',''),
                Name: $("#RecordName").val(),
                UserID: $("#RecordNameID").val()
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id='Loading' style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 100%; height: 100%; background: white; text-align: center; padding: 5px 10px; display: table;">
        <div style="display: table-cell; vertical-align: middle">
            <h1><font size="9">页面加载中……</font></h1>
        </div>
    </div>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table id="saPanelTab">
            <tr>
                <td style="text-align: right;">打卡类型:
                </td>
                <td>
                    <select class="easyui-combobox" id="CheckinType" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="全部">全部</option>
                        <option value="上班打卡">上班打卡</option>
                        <option value="下班打卡">下班打卡</option>
                        <option value="外出打卡">外出打卡</option>
                    </select>
                </td>
                <td style="text-align: right;">异常类型:
                </td>
                <td>
                    <select class="easyui-combobox" id="ExceptionType" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="全部">全部</option>
                        <option value="未打卡">未打卡</option>
                        <option value="时间异常">时间异常</option>
                        <option value="地点异常">地点异常</option>
                        <option value="wifi异常">wifi异常</option>
                        <option value="非常用设备">非常用设备</option>
                    </select>
                </td>
                <td style="text-align: right;">姓名:
                </td>
                <td>
                    <input id="UserName" class="easyui-textbox" data-options="prompt:'请输入姓名'" style="width: 100px">
                </td>
                <td style="text-align: right;">打卡时间:
                </td>
                <td>
                    <input id="StartDate" class="easyui-datebox" style="width: 100px">~
                    <input id="EndDate" class="easyui-datebox" style="width: 100px">
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="summary()">&nbsp;汇总查询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-arrow_refresh" plain="false" onclick="SyncCheckin(1)">同步当日考勤</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-arrow_refresh" plain="false" onclick="SyncCheckin(0)">同步本月考勤</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-arrow_refresh" plain="false" onclick="SyncCheckinDayReport()">同步当日汇总</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-arrow_refresh" plain="false" onclick="SyncCheckinMonthlyReport()">同步本月汇总</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="AwbExport()">&nbsp;导&nbsp;出&nbsp;</a>
        <form runat="server" id="fm1">
            <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
            <asp:Button ID="btnReportDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnReportDerived_Click" />
        </form>
    </div>
    <%--显示打卡地图--%>
    <div id="map" class="easyui-dialog" style="width: 50%; height: 500px; padding: 5px 5px" closed="true" buttons="#map-buttons">
    </div>
    <div id="map-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="CloseMap()">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <%--显示打卡地图结束--%>
    <%--显示汇总--%>
    <div id="Summarydlg" class="easyui-dialog" style="width: 85%; height: 650px; padding: 2px 2px" closed="true" buttons="#summary-buttons">
        <div class="easyui-panel" title="" data-options="iconCls:'icon-search'">
            <table id="PriceBasicTab">
                <tr>
                    <td style="text-align: right;">汇总月份:
                    </td>
                    <td>
                    <input name="Date" id="ReportTime" class="easyui-datebox" data-options="required:true" style="width: 100px" editable="false" />
                    </td>
                    <td style="text-align: right;">姓名:
                    </td>
                    <td>
                        <input id="RecordName" class="easyui-textbox" data-options="prompt:'请输入姓名'" style="width: 100px">
                    </td>
                    <td style="text-align: right;">用户ID:
                    </td>
                    <td>
                        <input id="RecordNameID" class="easyui-textbox" data-options="prompt:'请输入用户ID'" style="width: 100px">
                    </td>
                </tr>
            </table>
        </div>
        <table id="dgSummary" class="easyui-datagrid">
        </table>
        <div id="dgsummarybar">
            <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearchSummary()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="AwbReportExport()">&nbsp;导&nbsp;出&nbsp;</a>
            <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#Summarydlg').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
        </div>
    </div>
    <div id="dgViewImg" class="easyui-dialog" closed="true">
        <img id="simg" style="width: 400px; height: 650px;" />
    </div>
    <%--显示汇总结束--%>
    <%--<script type="text/javascript" charset="utf-8" src="https://map.qq.com/api/js?v=2.exp&key=OB4BZ-D4W3U-B7VVO-4PJWW-6TKDJ-WPB77"></script>--%>
    <script charset="utf-8" src="https://map.qq.com/api/gljs?v=1.exp&key=OB4BZ-D4W3U-B7VVO-4PJWW-6TKDJ-WPB77"></script>
    <script type="text/javascript">
        function CloseMap() {
            destroy();
            $('#map').dialog('close');
        }
        function download(img) {
            var simg = window.location.href.substring(0, window.location.href.indexOf(window.location.pathname)) + "//CheckinImage//" + img;
            $('#dgViewImg').dialog('open').dialog('setTitle', '预览');
            $("#simg").attr("src", simg);

        }
        $('#simg').load(function () {
            // 加载完成    
            //setImgWidthHeight();
        });

        function setImgWidthHeight(){
            var maxwidth = 400;
            var maxheight = 650;
            var img = new Image();
            img.src = $("#simg").attr("src");
            // 初始化高度和宽度
            $("#simg").width(img.width);
            $("#simg").height(img.height);
            //高度和宽度设置
            //alert(img.width+" "+img.height);
            if (img.width > maxwidth) {
                $("#simg").width(maxwidth);
                // 再判断高度
                var i = maxwidth/img.width;
                var ih = i*img.height; // 计算高度的缩放比例
                if(ih>maxheight){
                    // 如果走到这里则图片会被拉伸
                    $("#simg").height(maxheight);
                }else{
                    $("#simg").height(ih);
                }        
            }else{
                if (img.height > maxheight) {
                    $("#simg").height(maxheight);
                    // 再判断宽度
                    var i = maxheight/img.height;
                    var iw = i*img.width;
                    if(iw>maxwidth){
                        $("#simg").width(maxwidth);
                    }else{
                        $("#simg").width(iw);
                    }        
                }
            }
        }
        //同步考勤
        function SyncCheckin(type) {
            var rows = $('#dg').datagrid('getSelections');
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', "确认同步当日考勤数据？", function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在同步中...' });
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'wxQYServices.aspx?method=SyncCheckin&type=' + type,
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
                    $.messager.progress("close");
                    dosearch();
                }
            });
        }
        //同步当日汇总
        function SyncCheckinDayReport() {
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', "确认同步当日汇总数据？", function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在同步中...' });
                    //var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'wxQYServices.aspx?method=SyncCheckinDayReport',
                        type: 'post', dataType: 'json',
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
                    $.messager.progress("close");
                    dosearch();
                }
            });
        }
        //同步本月汇总
        function SyncCheckinMonthlyReport() {
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', "确认同步本月汇总数据？", function (r) {
                if (r) {
                    $.messager.progress({ msg: '请稍后,正在同步中...' });
                    //var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'wxQYServices.aspx?method=SyncCheckinReport',
                        type: 'post', dataType: 'json',
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
                    $.messager.progress("close");
                    dosearch();
                }
            });
        }
        //导出数据
        function AwbExport() {
            var rows = $('#dg').datagrid('getData').rows;
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要导出的数据！', 'warning');
                return;
            }
            var MoveNoStr = "";
            for (var i = 0; i < rows.length; i++) {
                MoveNoStr += rows[i].MoveNo + ",";
            }
            var json = JSON.stringify(MoveNoStr)
            $.ajax({
                url: 'wxQYServices.aspx?method=QueryCheckinForExport',
                data: { data: MoveNoStr },
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click(); }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }//导出数据
        function AwbReportExport() {
            var rows = $('#dgSummary').datagrid('getData').rows;
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要导出的数据！', 'warning');
                return;
            }
            var MoveNoStr = "";
            for (var i = 0; i < rows.length; i++) {
                MoveNoStr += rows[i].MoveNo + ",";
            }
            var json = JSON.stringify(MoveNoStr)
            $.ajax({
                url: 'wxQYServices.aspx?method=QueryCheckinReportForExport',
                data: { data: MoveNoStr },
                success: function (text) {
                    if (text == "OK") { var obj = document.getElementById("<%=btnReportDerived.ClientID %>"); obj.click(); }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }
        $(function () {
            $('#ReportTime').datebox({
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
                                if (month < 10) {
                                    month = "0" + month;
                                }
                                //隐藏日期对象                     
                                $('#ReportTime').datebox('hidePanel').datebox('setValue', year + '-' + month);
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

                    return d.getFullYear() + '-' + currentMonthStr;
                }
            });
            //日期选择对象
            var p = $('#ReportTime').datebox('panel'),
            //日期选择对象中月份
            btds = false,
            //显示月份层的触发控件
            span = p.find('span.calendar-text');
        });
    </script>
</asp:Content>
