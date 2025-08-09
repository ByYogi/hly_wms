<%@ Page Title="拖拽移库管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DropMoveHouseManager.aspx.cs" Inherits="Cargo.House.DropMoveHouseManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .drag {
            font-size: 13px;
            width: 100px;
            height: 110px;
            padding: 2px 10px 10px 10px;
            margin: 5px;
            border: 1px solid #ccc;
            background: #c2d7f7;
            float: left;
            -moz-border-radius: 5px 5px 0 0;
            -webkit-border-radius: 5px 5px 0 0;
            border-radius: 5px 5px 0 0;
        }

        .dp {
            opacity: 0.5;
            filter: alpha(opacity=50);
        }

        .over {
            background: #FBEC88;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {

            //所在仓库
            $('#HID').combobox({
                url: 'houseApi.aspx?method=CargoPermisionHouse',
                valueField: 'HouseID', textField: 'Name',
                onSelect: function (rec) {
                    $('#PID').combobox('clear');
                    var url = 'houseApi.aspx?method=QueryALLArea&pid=-1&hid=' + rec.HouseID;
                    $('#PID').combobox('reload', url);
                }
            });
            //一级产品
            $('#APID').combobox({
                url: '../Product/productApi.aspx?method=QueryALLOneProductType', valueField: 'TypeID', textField: 'TypeName',
                onSelect: function (rec) {
                    $('#ASID').combobox('clear');
                    var url = '../Product/productApi.aspx?method=QueryALLOneProductType&PID=' + rec.TypeID;
                    $('#ASID').combobox('reload', url);
                }
            });
        });

        //查询
        function dosearch() {
            $('#source').html('');
            $.ajax({
                url: 'houseApi.aspx?method=QueryInHouseData', async: false,
                type: 'post', dataType: 'json', data: { ContainerCode: $('#CCode').textbox('getValue'), Specs: $('#ASpecs').textbox('getValue'), Model: $('#AModel').val(), Figure: $('#AFigure').val(), APID: $("#APID").combobox('getValue'), ASID: $("#ASID").combobox('getValue'), ContainerType: $("#CType").combobox('getValue'), PID: $("#PID").combobox('getValue'), HID: $("#HID").combobox('getValue'), page: 1, rows: 1000 },
                success: function (text) {
                    if (text.total > 0) {
                        for (var i = 0; i < text.rows.length; i++) {
                            $('#source').append("<div class='drag' id=" + text.rows[i].ID + " ContainerID=" + text.rows[i].ContainerID + " TypeID=" + text.rows[i].TypeID + " ProductID=" + text.rows[i].ProductID + " Piece=" + text.rows[i].Piece + ">" + text.rows[i].ContainerCode + "<br />" + text.rows[i].ProductName + "<br />" + text.rows[i].Specs + "<br />" + text.rows[i].Model + "<span style='padding-left: 20px;color: red;'>" + text.rows[i].Piece + "</span><br />" + text.rows[i].Figure + "<br />" + text.rows[i].Batch + "<br />" + text.rows[i].HouseName + "&nbsp;&nbsp;" + text.rows[i].InPiece + "</div>");
                        }
                    }
                }
            });
            $('.drag').draggable({
                proxy: 'clone',
                revert: true,
                cursor: 'auto',
                onStartDrag: function () {
                    $(this).draggable('options').cursor = 'not-allowed';
                    $(this).draggable('proxy').addClass('dp');
                },
                onStopDrag: function () {
                    $(this).draggable('options').cursor = 'auto';
                }
            });
        }
        //查询目标货位
        function searchAim() {
            if ($('#AimCode').textbox('getValue') == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '目标货位不能为空！', 'warning'); return;
            }
            $('#target').html("<h1 id='aim'>目标货位：</h1>");
            $.ajax({
                url: 'houseApi.aspx?method=QueryContainerEntityByCode', async: false,
                type: 'post', dataType: 'json', data: { ContainerCode: $('#AimCode').textbox('getValue') },
                success: function (text) {
                    if (text.ContainerCode == null || text.ContainerCode == '') {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '目标货位不存在！', 'warning');
                    } else {
                        $('#aim').html("目标货位：" + text.ContainerCode + "   可拉件数：" + Number(text.allowPiece));
                        $('#aim').attr('code', text.ContainerCode);
                        $('#aim').attr('houseID', text.HouseID);
                    }
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="saPanel" class="easyui-panel" title="" data-options="iconCls:'icon-search'">
        <table>
            <tr>
                <td style="text-align: right;">规格:
                </td>
                <td>
                    <input id="ASpecs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 100px">
                </td>
                <td style="text-align: right;">型号:
                </td>
                <td>
                    <input id="AModel" class="easyui-textbox" data-options="prompt:'请输入产品型号'" style="width: 100px">
                </td>

                <td style="text-align: right;">一级产品:
                </td>
                <td>
                    <input id="APID" class="easyui-combobox" style="width: 100px;"
                        panelheight="auto" />
                </td>
                <td style="text-align: right;">货位代码:
                </td>
                <td>
                    <input id="CCode" class="easyui-textbox" data-options="prompt:'请输入货位代码'" style="width: 100px">
                </td>
                <td style="text-align: right;">所在仓库:
                </td>
                <td>
                    <input id="HID" class="easyui-combobox" style="width: 100px;" panelheight="auto" />
                </td>

                <td style="text-align: right;">目标货位:
                </td>
                <td>
                    <input id="AimCode" class="easyui-textbox" data-options="prompt:'请输入目标货位代码'" style="width: 100px">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查询移库货位&nbsp;</a>
                </td>
                <td style="text-align: right;">花纹:
                </td>
                <td>
                    <input id="AFigure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 100px">
                </td>

                <td style="text-align: right;">二级产品:
                </td>
                <td>
                    <input id="ASID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'TypeID',textField:'TypeName'"
                        panelheight="auto" />
                </td>


                <td style="text-align: right;">货位类型:
                </td>
                <td>
                    <input class="easyui-combobox" id="CType" data-options="url:'../Data/ContainerType.json',method:'get',valueField:'id',textField:'text',panelHeight:'auto'"
                        style="width: 100px;">
                </td>
                <td style="text-align: right;">所在区域:
                </td>
                <td>
                    <input id="PID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'AreaID',textField:'Name'"
                        panelheight="auto" />
                </td>
                <td colspan="2"><a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="searchAim()">查询目标货位</a>&nbsp;&nbsp;<a href="InhouseManager.aspx" class="easyui-linkbutton" iconcls="icon-arrow_turn_left" plain="false">&nbsp;返回&nbsp;</a></td>
            </tr>
        </table>
    </div>
    <div id="source" style="border: 1px solid #ccc; width: 50%; min-height: 400px; height: 100%; float: left; margin: 5px;">
    </div>
    <div id="target" style="border: 1px solid #ccc; width: 47%; min-height: 400px; height: 100%; float: left; margin: 5px;">
    </div>
    <script type="text/javascript">
        $(function () {

            $('#target').droppable({
                onDragEnter: function (e, source) {
                    if ($('#aim').attr('code') == undefined || $('#aim').attr('code') == '') {
                        return;
                    }
                    $(source).draggable('options').cursor = 'auto';
                    //$(source).draggable('proxy').css('border', '1px solid red');
                    //$(this).addClass('over');
                },
                onDragLeave: function (e, source) {
                    if ($('#aim').attr('code') == undefined || $('#aim').attr('code') == '') {
                        return;
                    }
                    $(source).draggable('options').cursor = 'not-allowed';
                    //$(source).draggable('proxy').css('border', '1px solid #ccc');
                    //$(this).removeClass('over');
                },
                onDrop: function (e, source) {
                    if ($('#aim').attr('code') == undefined || $('#aim').attr('code') == '') {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请先查询目标货位！', 'warning');
                        return;
                    }
                    if (source.parentNode.id != "target") {
                        var isbool = false;
                        //执行移库的方法
                        var OldID = source.id;
                        var ContainerID = source.getAttribute("containerid");
                        var TypeID = source.getAttribute("typeid");
                        var ProductID = source.getAttribute("productid");
                        var Piece = source.getAttribute("piece");
                        var newContainerCode = $('#aim').attr('code');
                        var HouseID = $('#aim').attr('houseID');
                        $.ajax({
                            url: 'houseApi.aspx?method=SaveMoveContainerDrop', async: false,
                            type: 'post', dataType: 'json', data: { OldID: OldID, ContainerID: ContainerID, TypeID: TypeID, ProductID: ProductID, Piece: Piece, newContainerCode: newContainerCode, HouseID: HouseID },
                            success: function (result) {
                                //var result = eval('(' + msg + ')');
                                if (result.Result) {
                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '移库成功!', 'info');
                                    isbool = true;
                                } else {
                                    $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '移库失败：' + result.Message, 'warning');
                                }
                            }
                        });
                        if (isbool) {

                            $(this).append(source)
                            $(this).removeClass('over');
                        }
                    }

                    //var name = $(source).find('p:eq(0)').html();
                    //var price = $(source).find('p:eq(1)').html();
                    //addProduct(name, parseFloat(price));
                }
            });
        });
    </script>
</asp:Content>
