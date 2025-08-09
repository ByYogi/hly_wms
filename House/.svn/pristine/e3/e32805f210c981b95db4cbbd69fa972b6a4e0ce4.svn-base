<%@ Page Title="打卡规则" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckinRule.aspx.cs" Inherits="Cargo.QY.CheckinRule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /*body {
            height: 100%;
            margin: 0px;
            padding: 0px;
        }*/


        #suggestionList li .item_info {
            font-size: 12px;
            color: grey;
        }

        #suggestionList li a:hover:not(.header) {
            background-color: #eee;
        }
    </style>
    <script type="text/javascript">
        //页面加载时执行
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
                  {
                      title: '', field: 'ID', checkbox: true, width: '30px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '规则名称', field: 'RuleName', width: '0px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '规则类型', field: 'RuleType', width: '65px', formatter: function (val, row, index) {
                          if (val == "1") { return "<span title='固定上下班'>固定上下班</span>"; }
                          else if (val == "2") { return "<span title='按班次上下班'>按班次上下班</span>"; }
                          else if (val == "3") { return "<span title='自由上下班'>自由上下班</span>"; }
                      }
                  },
                  {
                      title: '打卡位置', field: 'CheckinLocation', width: '80px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  {
                      title: '打卡范围', field: 'CheckinScope', width: '80px', formatter: function (value) {
                          return "<span title='" + value + "'>" + value + "</span>";
                      }
                  },
                  { title: '操作时间', field: 'OP_DATE', width: '130px', formatter: DateTimeFormatter }
                ]],
                onLoadSuccess: function (data) { },
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) { Edit(); },
                rowStyler: function (index, row) {
                    if (row.RuleType == "1") { return "background-color:#b1e2e2"; }
                    else if (row.RuleType == "2") { return "background-color:#e1f4f4"; }
                    else if (row.RuleType == "3") { return "background-color:#ffdeb3"; }
                }
            });
            //列表回车响应查询
            $("#saPanelTab").keydown(function (e) { if (e.keyCode == 13) { dosearch(); } });
            //编辑回车响应保存
            //$("#editTab").keydown(function (e) { if (e.keyCode == 13) { saveItem(); } });
            $('#RuleType').combobox('textbox').bind('focus', function () { $('#RuleType').combobox('showPanel'); });
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = 'wxQYServices.aspx?method=QueryCheckinRule';
            $('#dg').datagrid('load', {
                RuleName: $("#RuleName").val(),
                RuleType: $('#RuleType').combobox('getValue'),
                CheckinLocation:$("#CheckinLocation").val()
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--此div用于在界面未完全加载样式前显示内容--%>
    <div id='Loading' style="position: absolute; z-index: 1000; top: 0px; left: 0px; width: 100%; height: 100%; background: white; text-align: center; padding: 5px 10px; display: table;">
        <div style="display: table-cell; vertical-align: middle">
            <h1><font size="9">页面加载中……</font></h1>
        </div>
    </div>
    <%--此div用于在界面未完全加载样式前显示内容--%>
    <%--此div用于显示查询条件--%>
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table id="saPanelTab">
            <tr>
                <td style="text-align: right;">规则名称:
                </td>
                <td>
                    <input id="RuleName" class="easyui-textbox" data-options="prompt:'请输入规则名称'" style="width: 100px">
                </td>
                <td style="text-align: right;">规则类型:
                </td>
                <td>
                    <select class="easyui-combobox" id="RuleType" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="-1">全部</option>
                        <option value="1">固定上下班</option>
                        <option value="2">按班次上下班</option>
                        <option value="3">自由上下班</option>
                    </select>
                </td>
                <td style="text-align: right;">打卡地点:
                </td>
                <td>
                    <input id="CheckinLocation" class="easyui-textbox" data-options="prompt:'请输入打卡地点'" style="width: 100px">
                </td>
                <td style="text-align: right;">规则状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="DelFlag" style="width: 100px;" panelheight="auto" editable="false">
                        <option value="-1">全部</option>
                        <option value="0">启用</option>
                        <option value="1">禁用</option>
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
        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="Add()">&nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="Edit()">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="Del()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
    </div>
    <%--此div用于显示按钮操作--%>
    <%--此div用于新增/编辑数据--%>
    <div id="dlgedit" class="easyui-dialog" style="width: 600px; height: 365px; padding: 5px 5px" closed="true" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post">
            <input type="hidden" name="RuleID" />
            <input type="hidden" name="RuleTimeID" />
            <input type="hidden" name="RulePersonnelID" />
            <table id="saPanel">
                <tr>
                    <td style="vertical-align: top">
                        <table>
                            <tr>
                                <td style="text-align: right;">规则名称:
                                </td>
                                <td colspan="3">
                                    <input name="RuleName" id="eRuleName" class="easyui-textbox" data-options="min:0,precision:0,required:true" style="width: 200px;" validtype="length[0,100]">
                                </td>
                                <td style="text-align: right;">规则类型:
                                </td>
                                <td colspan="3">
                                    <select class="easyui-combobox" id="eRuleType" name="RuleType" style="width: 200px;" panelheight="auto" data-options="required:true" editable="false">
                                        <option value="1">固定上下班</option>
                                        <option value="2">按班次上下班</option>
                                        <option value="3">自由上下班</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">打卡位置:
                                </td>
                                <td colspan="3">
                                    <input type="hidden" id="eCheckinCoordinate" name="CheckinCoordinate" />
                                    <input id="eCheckinLocation" name="CheckinLocation" class="easyui-textbox" style="width: 150px; background-color: #ebebeb;" data-options="editable:false,required:true" />&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" style="height: 23px;" onclick="SelectLocation()">&nbsp;设&nbsp;置&nbsp;</a>
                                </td>
                                <td style="text-align: right;">打卡范围:
                                </td>
                                <td colspan="3">
                                    <input name="CheckinScope" id="eCheckinScope" class="easyui-numberspinner" value="300" data-options="increment:100,min:100,max:2000" style="width: 200px;" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">打卡人员:
                                </td>
                                <td colspan="3">
                                    <input id="ePersonnel" name="Personnel" class="easyui-combotree" data-options="url:'wxQYServices.aspx?method=QueryAllOrganizeAndUser',multiple:'true',editable:false,required:true" style="width: 200px;" />
                                </td>
                                <td style="text-align: right;">圈外打卡:
                                </td>
                                <td colspan="3">
                                    <select class="easyui-combobox" id="eScopeOuterType" style="width: 200px;" panelheight="auto" data-options="required:true" editable="false">
                                        <option value="1">记录为地点异常</option>
                                        <option value="2">记录为正常外勤</option>
                                        <option value="3">不允许打卡</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">工作时间:
                                </td>
                                <td colspan="3">
                                    <input name="ToWorkTime" id="eToWorkTime" class="easyui-timespinner" style="width: 80px;" required="required" data-options="min:'00:00',showSeconds:false">
                                    —
                                    <input name="OffWorkTime" id="eOffWorkTime" class="easyui-timespinner" style="width: 80px;" required="required" data-options="showSeconds:false">
                                </td>
                                <td style="text-align: right;">上班星期:
                                </td>
                                <td colspan="3">
                                    <input type="checkbox" id="Mon" name="WorkWeek" value="1" />
                                    <input type="checkbox" id="Tues" name="WorkWeek" value="2" />
                                    <input type="checkbox" id="Wed" name="WorkWeek" value="3" />
                                    <input type="checkbox" id="Thur" name="WorkWeek" value="4" />
                                    <input type="checkbox" id="Fri" name="WorkWeek" value="5" />
                                    <input type="checkbox" id="Sat" name="WorkWeek" value="6" />
                                    <input type="checkbox" id="Sun" name="WorkWeek" value="7" />
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">上班时段:
                                </td>
                                <td colspan="3">
                                    <input name="ToWorkStartTime" id="eToWorkStartTime" class="easyui-timespinner" style="width: 80px;" required="required" data-options="min:'00:00',showSeconds:false">
                                    —
                                    <input name="ToWorkEndTime" id="eToWorkEndTime" class="easyui-timespinner" style="width: 80px;" required="required" data-options="showSeconds:false">
                                </td>
                                <td style="text-align: right;">下班时段:
                                </td>
                                <td colspan="3">
                                    <input name="OffWorkStartTime" id="eOffWorkStartTime" class="easyui-timespinner" style="width: 80px;" required="required" data-options="min:'00:00',showSeconds:false">
                                    —
                                    <input name="OffWorkEndTime" id="eOffWorkEndTime" class="easyui-timespinner" style="width: 80px;" required="required" data-options="showSeconds:false">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">允许补卡:
                                </td>
                                <td colspan="3">
                                    <select class="easyui-combobox" id="eAdditionalType" name="AdditionalType" style="width: 200px;" panelheight="auto" data-options="required:true,onSelect:function(item){if(item.value==1){$('#eAdditionalTime').combobox('enable');$('#eAdditionalCount').combobox('enable');$('#eAdditionalTime').textbox('textbox').css('background','#ffffff');$('#eAdditionalCount').textbox('textbox').css('background','#ffffff')}else{$('#eAdditionalTime').combobox('disable');$('#eAdditionalCount').combobox('disable');$('#eAdditionalTime').textbox('textbox').css('background','#ebebeb');$('#eAdditionalCount').textbox('textbox').css('background','#ebebeb')}}" editable="false">
                                        <option value="0">不允许</option>
                                        <option value="1">允许</option>
                                    </select>
                                </td>
                                <td style="text-align: right;">补卡时限:
                                </td>
                                <td>
                                    <input name="AdditionalTime" id="eAdditionalTime" class="easyui-numberspinner" data-options="increment:1,min:0,max:365" style="width: 50px;" />&nbsp;&nbsp;天
                                </td>
                                <td style="text-align: right;">次数:
                                </td>
                                <td>
                                    <input name="AdditionalCount" id="eAdditionalCount" class="easyui-numberspinner" data-options="increment:1,min:0,max:365" style="width: 50px;" />&nbsp;&nbsp;次
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">休息时间:
                                </td>
                                <td colspan="3">
                                    <select class="easyui-combobox" id="eBreakTimeType" name="BreakTimeType" style="width: 200px;" panelheight="auto" data-options="required:true,onSelect:function(item){if(item.value==1){$('#eBreakStartTime').combobox('enable');$('#eBreakFinishTime').combobox('enable');$('#eBreakStartTime').textbox('textbox').css('background','#ffffff');$('#eBreakFinishTime').textbox('textbox').css('background','#ffffff')}else{$('#eBreakStartTime').combobox('disable');$('#eBreakFinishTime').combobox('disable');$('#eBreakStartTime').textbox('textbox').css('background','#ebebeb');$('#eBreakFinishTime').textbox('textbox').css('background','#ebebeb')}}" editable="false">
                                        <option value="0">关</option>
                                        <option value="1">开</option>
                                    </select>
                                </td>
                                <td style="text-align: right;">开始结束:
                                </td>
                                <td colspan="3">
                                    <input name="BreakStartTime" id="eBreakStartTime" class="easyui-timespinner" style="width: 80px;" required="required" data-options="min:'00:00',showSeconds:false">
                                    —
                                    <input name="BreakFinishTime" id="eBreakFinishTime" class="easyui-timespinner" style="width: 80px;" required="required" data-options="showSeconds:false">
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right;">弹性工作:
                                </td>
                                <td colspan="3">
                                    <input type="hidden" id="eFlexibleWorkType" name="FlexibleWorkType" />
                                    <input type="hidden" id="eAllowTime" name="AllowTime" />
                                    <input type="hidden" id="eArrival" name="Arrival" />
                                    <input id="FlexibleWork" name="CheckinLocation" class="easyui-textbox" style="width: 150px; background-color: #ebebeb;" data-options="editable:false,required:true" />&nbsp;&nbsp;<a href="#" class="easyui-linkbutton" style="height: 23px;" onclick="javascript:$('#dlgFlexibleWork').dialog('open').dialog('setTitle','编辑弹性工作');$('#AllowLateTime').hide();$('#AllowLeaveTime').hide();$('#LeaveLateTime').hide();$('#ArriveLateTime').hide();">&nbsp;设&nbsp;置&nbsp;</a>
                                </td>
                                <td style="text-align: right;">下班不打卡:
                                </td>
                                <td colspan="3">
                                    <select class="easyui-combobox" id="eOffWorkCheckinType" style="width: 200px;" panelheight="auto" data-options="required:true" editable="false">
                                        <option value="0">不需要</option>
                                        <option value="1">需要</option>
                                    </select>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" id="btnSave" iconcls="icon-ok" onclick="Save()">&nbsp;保&nbsp;存&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgedit').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <%--此div用于新增/编辑数据--%>
    <%--此div用于选择位置坐标--%>
    <div id="dlgMap" class="easyui-dialog" style="width: 70%; height: 650px; padding: 5px 5px" closed="true" buttons="#dlgMap-buttons">
        <div id="container" style="width: 100%; height: 100%;"></div>
        <div class="easyui-panel" style="position: absolute; background: #FFF; padding: 5px; z-index: 9999; top: 30px; left: 10px;">
            <b style="font-size: 11px; font-weight: inherit;">请输入考勤地点：</b><input id="keyword" class="easyui-textbox" style="width: 200px;" validtype="length[0,100]" onchange="getSuggestions()"><a href="#" class="easyui-linkbutton" onclick="getSuggestions()">&nbsp;搜&nbsp;索&nbsp;</a>
            <ul id="suggestionList" style="list-style-type: none; padding: 0; margin: 0;">
            </ul>
        </div>
    </div>
    <div id="dlgMap-buttons">
        <a href="#" class="easyui-linkbutton" id="btnSave" iconcls="icon-ok" onclick="dlgMapSelect()">&nbsp;选&nbsp;择&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgMap').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <%--此div用于选择位置坐标--%>
    <%--此div用于设置弹性工作--%>
    <div id="dlgFlexibleWork" class="easyui-dialog" style="width: 330px; height: 165px; padding: 5px 5px" closed="true" buttons="#dlgFlexibleWork-buttons">
        <table id="saPanel">
            <tr>
                <td style="text-align: right;">弹性工作:
                </td>
                <td>
                    <select class="easyui-combobox" id="FlexibleWorkType" name="BreakTimeType" style="width: 200px;" panelheight="auto" data-options="required:true,onSelect: function(item){if(item.value==1){$('#AllowLateTime').show();$('#AllowLeaveTime').show();$('#LeaveLateTime').hide();$('#ArriveLateTime').hide();$('#eAllowLateTime').textbox('setValue', 10);$('#eAllowLeaveTime').textbox('setValue', 10);}else if(item.value==2){$('#AllowLateTime').hide();$('#AllowLeaveTime').hide();$('#LeaveLateTime').show();$('#ArriveLateTime').show();$('#eLeaveLateTime').textbox('setValue', 30);$('#eArriveLateTime').textbox('setValue', 30);}else{$('#AllowLateTime').hide();$('#AllowLeaveTime').hide();$('#LeaveLateTime').hide();$('#ArriveLateTime').hide()}}" editable="false">
                        <option value="0">不设置</option>
                        <option value="1">允许迟到早退</option>
                        <option value="2">允许早到早走，晚到晚走</option>
                    </select>
                </td>
            </tr>
            <tr id="AllowLateTime">
                <td style="text-align: right;">允许迟到:
                </td>
                <td>
                    <input name="AllowLateTime" id="eAllowLateTime" class="easyui-numberspinner" data-options="increment:5,min:5,max:360" style="width: 200px;" />
                </td>
            </tr>
            <tr id="AllowLeaveTime">
                <td style="text-align: right;">允许早退:
                </td>
                <td>
                    <input name="AllowLeaveTime" id="eAllowLeaveTime" class="easyui-numberspinner" data-options="increment:5,min:5,max:360" style="width: 200px;" />
                </td>
            </tr>
            <tr id="LeaveLateTime">
                <td style="text-align: right;">晚走时长:
                </td>
                <td>
                    <input name="LeaveLateTime" id="eLeaveLateTime" class="easyui-numberspinner" data-options="increment:5,min:5,max:360" style="width: 200px;" />
                </td>
            </tr>
            <tr id="ArriveLateTime">
                <td style="text-align: right;">晚到时长:
                </td>
                <td>
                    <input name="ArriveLateTime" id="eArriveLateTime" class="easyui-numberspinner" data-options="increment:5,min:5,max:360" style="width: 200px;" />
                </td>
            </tr>
        </table>
    </div>
    <div id="dlgFlexibleWork-buttons">
        <a href="#" class="easyui-linkbutton" id="btnSave" iconcls="icon-ok" onclick="dlgFlexibleWorkSelect()">&nbsp;确&nbsp;定&nbsp;</a>
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgFlexibleWork').dialog('close')">&nbsp;取&nbsp;消&nbsp;</a>
    </div>
    <%--此div用于设置弹性工作--%>
    <script charset="utf-8" src="https://map.qq.com/api/gljs?v=1.exp&libraries=service&key=CMZBZ-64RLS-7FROW-6SNUF-C3P5Z-DGB2J"></script>
    <%--<script charset="utf-8" src="https://map.qq.com/api/gljs?v=1.exp&libraries=service&key=OB4BZ-D4W3U-B7VVO-4PJWW-6TKDJ-WPB77"></script>--%>
    <script type="text/javascript">
        //选择位置
        function dlgMapSelect(){
            $('#dlgMap').dialog('close');
            //$('#eCheckinLocation').textbox('setValue', '');
            $("#eCheckinCoordinate").val(tempCheckinCoordinate);
            $('#eCheckinLocation').textbox('setValue', tempCheckinLocation);
        }
        //选择弹性工作
        function dlgFlexibleWorkSelect(){
            $('#dlgFlexibleWork').dialog('close');
            $('#FlexibleWork').textbox('setValue', $('#FlexibleWorkType').combobox('getText'));
            var FlexibleWorkType=$('#FlexibleWorkType').combobox('getValue');
            $('#eFlexibleWorkType').val(FlexibleWorkType);
            if(FlexibleWorkType==1){
                $('#eAllowTime').val($('#eAllowLateTime').numberspinner('getValue'));
                $('#eArrival').val($('#eAllowLeaveTime').numberspinner('getValue'));
            }else if(FlexibleWorkType==2){
                $('#eAllowTime').val($('#eLeaveLateTime').numberspinner('getValue'));
                $('#eArrival').val($('#eArriveLateTime').numberspinner('getValue'));
            }else{
                $('#eAllowTime').val('');
                $('#eArrival').val('');
            }
        }
        
        //新增
        function Add() {
            $('#dlgedit').form('clear');
            $('#dlgedit').dialog('open').dialog('setTitle', '新增打卡规则');
            $('#eRuleType').combobox('setValue', '1');
            $('#eCheckinScope').textbox('setValue', 300);
            $('#eToWorkTime').timespinner('setValue', '09:00');
            $('#eOffWorkTime').timespinner('setValue', '17:00');
            $('#eToWorkStartTime').timespinner('setValue', '04:00');
            $('#eToWorkEndTime').timespinner('setValue', '16:59');
            $('#eOffWorkStartTime').timespinner('setValue', '09:01');
            $('#eOffWorkEndTime').timespinner('setValue', '03:59');
            $('#Mon').prop('checked', true);
            $('#Tues').prop('checked', true);
            $('#Wed').prop('checked', true);
            $('#Thur').prop('checked', true);
            $('#Fri').prop('checked', true);
            $('#eScopeOuterType').combobox('setValue', '1');
            $('#eAdditionalType').combobox('setValue', '1');
            $('#eAdditionalTime').textbox('setValue', 60);
            $('#eAdditionalCount').textbox('setValue', 6);
            $('#eBreakTimeType').combobox('setValue', '0');
            $('#eBreakStartTime').timespinner('setValue', '12:00');
            $('#eBreakFinishTime').timespinner('setValue', '13:30');
            $('#FlexibleWork').textbox('setValue', '不设置');
            $('#eOffWorkCheckinType').combobox('setValue', '1');
            $('#FlexibleWork').textbox('textbox').css('background','#ebebeb')
            $('#eBreakStartTime').combobox('disable');
            $('#eBreakFinishTime').combobox('disable');
            $('#eBreakStartTime').textbox('textbox').css('background','#ebebeb');
            $('#eBreakFinishTime').textbox('textbox').css('background','#ebebeb');
            $('#eFlexibleWorkType').val('0');
        }
        //保存
        function Save() {
            $('#eBreakStartTime').combobox('enable');
            $('#eBreakFinishTime').combobox('enable');
            $('#eLeaveLateTime').combobox('enable');
            $('#eArriveLateTime').combobox('enable');
            $('#eAdditionalTime').combobox('enable');
            $('#eAdditionalCount').combobox('enable');
            $.messager.progress({ msg: '请稍后,正在保存中...' });
            $('#fm').form('submit', {
                url: 'wxQYServices.aspx?method=SaveCheckinRule',
                onSubmit: function () {
                    var check = $(this).form('enableValidation').form('validate');
                    if (!check) { $.messager.progress("close"); }
                    return check;
                },
                success: function (msg) {
                    $.messager.progress("close");
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
        //修改
        function Edit() {
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
                $('#dlgedit').dialog('open').dialog('setTitle', '修改打卡规则');
                $('#fm').form('load', rows[0]);
            }
        }
        //删除数据
        function Del() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            for (var i = 0; i < rows.length; i++) {
                if (rows[i].InCargoStatus == 1) {
                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '所选数据中包含已入库数据！', 'warning'); return;
                }
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'wxQYServices.aspx?method=DeleteCheckinRule',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                $('#dg').datagrid('clearSelections');
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
        $('#keyword').textbox({
            onChange: function (n, o) {
                console.log($('#keyword').textbox('getValue'));
                getSuggestions();
            }
        });
        //设置打卡地点
        function SelectLocation() {
            $('#dlgMap').dialog('open').dialog('setTitle', '新增打卡规则');
        }
        var map = new TMap.Map('container', {
            zoom: 16,
            center: new TMap.LatLng(23.25701, 113.318586),
        });
        var suggestionList = [];
        var search = new TMap.service.Search({ pageSize: 10 }); // 新建一个地点搜索类
        var suggest = new TMap.service.Suggestion({
            // 新建一个关键字输入提示类
            pageSize: 10, // 返回结果每页条目数
            //region: '北京', // 限制城市范围
            //regionFix: true, // 搜索无结果时是否固定在当前城市
        });
        var markers = new TMap.MultiMarker({
            map: map,
            geometries: [],
        });
        var infoWindowList = Array(10);
        function getSuggestions() {
            // 使用者在搜索框中输入文字时触发
            var suggestionListContainer = document.getElementById('suggestionList');
            suggestionListContainer.innerHTML = '';
            //var keyword = document.getElementById('keyword').value;
            var keyword = $('#keyword').textbox('getValue');
            if (keyword) {
                suggest
                  .getSuggestions({ keyword: keyword, location: map.getCenter() })
                  .then((result) => {
                      // 以当前所输入关键字获取输入提示
                      suggestionListContainer.innerHTML = '';
                suggestionList = result.data;
                suggestionList.forEach((item, index) => {
                    suggestionListContainer.innerHTML += `<li><a href="#" style="margin-top: -1px;background-color: #f6f6f6;text-decoration: none;font-size: 18px;color: black;display: block; " onclick="setSuggestion(${index})">${item.title}<span class="item_info">${item.address}</span></a></li>`;
        });
        })
      .catch((error) => {
          console.log(error);
        });
        }
        }
        var circle;
        var tempCheckinCoordinate;
        var tempCheckinLocation;
        function setSuggestion(index) {
            // 点击输入提示后，于地图中用点标记绘制该地点，并显示信息窗体，包含其名称、地址等信息
            infoWindowList.forEach((infoWindow) => {
                infoWindow.close();
        });
        tempCheckinCoordinate=suggestionList[index].location.lat+","+suggestionList[index].location.lng;
        tempCheckinLocation=suggestionList[index].address;
        if (circle) {
            circle.setMap(null);
        }
        //创建圆形覆盖物
        circle = new TMap.MultiCircle({ 
            map,
            styles: { // 设置圆形样式
                'circle': new TMap.CircleStyle({
                    'color': 'rgba(41,91,255,0.16)',
                    'showBorder': true,
                    'borderColor': 'rgba(41,91,255,1)',
                    'borderWidth': 2,
                }),
            },
            geometries: [{
                styleId: 'circle',
                center: suggestionList[index].location, //圆形中心点坐标 
                radius: parseInt($('#eCheckinScope').numberspinner('getValue')),	//半径（单位：米）
            }],
        });		
        infoWindowList.length = 0;
        $('#keyword').textbox('setValue', suggestionList[index].title);
        //document.getElementById('keyword').value = suggestionList[index].title;
        document.getElementById('suggestionList').innerHTML = '';
        markers.setGeometries([]);
        markers.updateGeometries([
          {
              id: '0', // 点标注数据数组
              position: suggestionList[index].location,
          },
        ]);
        var infoWindow = new TMap.InfoWindow({
            map: map,
            position: suggestionList[index].location,
            content: `<h3>${suggestionList[index].title}</h3><p>地址：${suggestionList[index].address}</p>`,
            offset: { x: 0, y: -50 },
        });
  infoWindowList.push(infoWindow);
  map.setCenter(suggestionList[index].location);
  markers.on('click', (e) => {
    infoWindowList[Number(e.geometry.id)].open();
        });
        }

function searchByKeyword() {
    // 关键字搜索功能
  infoWindowList.forEach((infoWindow) => {
    infoWindow.close();
    });
  infoWindowList.length = 0;
  markers.setGeometries([]);
  search
    .searchRectangle({
                                                  //keyword: document.getElementById('keyword').value,
                                                      keyword:$('#keyword').textbox('getValue'),
                                                      bounds: map.getBounds(),
                                                  })
    .then((result) => {
      result.data.forEach((item, index) => {
        var geometries = markers.getGeometries();
        var infoWindow = new TMap.InfoWindow({
                                                  map: map,
                                                  position: item.location,
                                                  content: `<h3>${item.title}</h3><p>地址：${item.address}</p><p>电话：${item.tel}</p>`,
        offset: { x: 0, y: -50 },
        });
        infoWindow.close();
        infoWindowList[index] = infoWindow;
        geometries.push({
                                                  id: String(index),
                                                  position: item.location,
                                              });
        markers.updateGeometries(geometries);
        markers.on('click', (e) => {
          infoWindowList[Number(e.geometry.id)].open();
        });
        });
        });
        }
    </script>
</asp:Content>
