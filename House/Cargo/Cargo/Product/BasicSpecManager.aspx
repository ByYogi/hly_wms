<%@ Page Title="基础规格管理" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BasicSpecManager.aspx.cs" Inherits="Cargo.Product.BasicSpecManager" %>

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
                pageSize: Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), //每页多少条
                pageList: [Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28), Math.floor((Number($(window).height()) - $("div[name='SelectDiv1']").outerHeight(true) - 58) / 28) * 2],
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'SID',
                url: null,
                toolbar: '#toolbar',
                columns: [[
                    { title: '', field: 'SID', checkbox: true, width: '30px' },
                    {
                        title: '产品编码', field: 'ProductCode', width: '110px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '品牌', field: 'TypeName', width: '70px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '品牌代码', field: 'BrandCode', width: '60px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '产品名称', field: 'ProductName', width: '300px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '规格', field: 'Specs', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '花纹', field: 'Figure', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '型号', field: 'Model', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '货品代码', field: 'GoodsCode', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '载重', field: 'LoadIndex', width: '70px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '速度', field: 'SpeedLevel', width: '60px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '尺寸', field: 'HubDiameter', width: '60px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '产地', field: 'Born', width: '60px', formatter: function (value) {
                            if (value == "0") { return "<span title='国产'>国产</span>"; }
                            else if (value == "1") { return "<span title='进口'>进口</span>"; } else { return ""; }
                        }
                    },
                    {
                        title: '轮胎类型', field: 'TyreType', width: '70px', formatter: function (value) {
                            if (value == "0") { return "<span title='四季胎'>四季胎</span>"; }
                            else if (value == "1") { return "<span title='冬季胎'>冬季胎</span>"; }
                            else if (value == "2") { return "<span title='内胎'>内胎</span>"; }
                            else if (value == "3") { return "<span title='AGM'>AGM</span>"; }
                            else if (value == "4") { return "<span title='EFB'>EFB</span>"; }
                            else if (value == "5") { return "<span title='普通电池'>普通电池</span>"; }
                            else if (value == "6") { return "<span title='半合成'>半合成</span>"; }
                            else if (value == "7") { return "<span title='全合成'>全合成</span>"; } else { return ""; }
                        }
                    },

                    {
                        title: '规格类型', field: 'CommType', width: '80px', formatter: function (value) {
                            if (value == "0") { return "<span title='普通规格'>普通规格</span>"; }
                            else if (value == "1") { return "<span title='常用规格'>常用规格</span>"; } else { return ""; }
                        }
                    },
                    {
                        title: '所属公司', field: 'UpClientShortName', width: '80px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '制造商', field: 'Manufacturer', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '销售商', field: 'Seller', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '服务商', field: 'Servicer', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },

                    {
                        title: '缩略图', field: 'Thumbnail', width: '60px', formatter: function (value) {
                            if (value != "") {
                                return "<a onclick='showImg(\"" + value + "\")'><img style='width: 27px;' src='" + document.location.origin + value.substr(2) + "'></img></a>";
                            }
                        }
                    },
                    {
                        title: '详情图', field: 'DetailDrawing', width: '60px', formatter: function (value) {
                            if (value != "") {
                                return "<a onclick='showImg(\"" + value + "\")'><img style='width: 27px;' src='" + document.location.origin + value.substr(2) + "'></img></a>";
                            }
                        }
                    },
                    {
                        title: '捆包数量', field: 'BundleNum', width: '70px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '启用状态', field: 'DelFlag', width: '80px',
                        formatter: function (val, row, index) {
                            if (val == "0") { return "启用"; }
                            else if (val == "1") { return "停用"; }
                            else { return "启用"; }
                        }
                    },
                ]],
                onClickRow: function (index, row) {
                    $('#dg').datagrid('clearSelections');
                    $('#dg').datagrid('selectRow', index);
                },
                onLoadSuccess: function (data) { },
                onDblClickRow: function (index, row) { editItemByID(index); }
            });

            $('#dgMapping').datagrid({
                width: '100%',
                height: '560px',
                title: '', //标题内容
                pagination: true, //分页是否显示
                pageSize: 17, //每页多少条
                pageList: [17, 50],
                loadMsg: '数据加载中请稍候...',
                autoRowHeight: false, //行高是否自动
                collapsible: false, //是否可折叠
                fitColumns: false, //设置为 true，则会自动扩大或缩小列的尺寸以适应网格的宽度并且防止水平滚动
                singleSelect: false, //设置为 true，则只允许选中一行。
                checkOnSelect: true, //如果设置为 true，当用户点击某一行时，则会选中/取消选中复选框。如果设置为 false 时，只有当用户点击了复选框时，才会选中/取消选中复选框
                idField: 'MID',
                url: null,
                toolbar: '#MappingBar',
                columns: [[
                    { title: '', field: '', checkbox: true, width: '30px' },
                    { title: '共享编码', field: 'ShareCode', width: '100px' },
                    { title: '产品编码', field: 'ProductCode', width: '100px' },
                    { title: '外部编码', field: 'CassCode', width: '120px' },
                    {
                        title: '外部公司', field: 'CorpClass', width: '60px', formatter: function (val, row, index) {
                            if (val == "1") { return "开思"; } else if (val == "2") { return "广汽丰田"; } else if (val == "3") { return "广汽本田"; } else if (val == "4") { return "狄乐汽服"; } else if (val == "5") { return "一汽丰田"; } else { return ""; }
                        }
                    },
                    {
                        title: '品牌', field: 'TypeName', width: '70px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '规格', field: 'Specs', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '花纹', field: 'Figure', width: '100px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    //{
                    //    title: '型号', field: 'Model', width: '120px', formatter: function (value) {
                    //        return "<span title='" + value + "'>" + value + "</span>";
                    //    }
                    //},
                    {
                        title: '货品代码', field: 'GoodsCode', width: '120px', formatter: function (value) {
                            return "<span title='" + value + "'>" + value + "</span>";
                        }
                    },
                    {
                        title: '载速', field: 'LoadIndex', width: '70px', formatter: function (value, row) {
                            return "<span title='" + value + row.SpeedLevel + "'>" + value + row.SpeedLevel + "</span>";
                        }
                    },
                    //{
                    //    title: '速度', field: 'SpeedLevel', width: '60px', formatter: function (value) {
                    //        return "<span title='" + value + "'>" + value + "</span>";
                    //    }
                    //},
                ]],
                onClickRow: function (index, row) {
                    $('#dgMapping').datagrid('clearSelections');
                    $('#dgMapping').datagrid('selectRow', index);
                },
                onDblClickRow: function (index, row) {
                    editAcceptAddressByID(index);
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
                    $('#APID').combobox('textbox').bind('focus', function () { $('#APID').combobox('showPanel'); });

                }
            });
            //一级产品
            //$('#ASID').combobox({
            //    url: '../Product/productApi.aspx?method=QueryALLOneProductType&PID=1', valueField: 'TypeID', textField: 'TypeName', AddField: 'PinyinName',
            //    filter: function (q, row) {
            //        var opts = $(this).combobox('options');
            //        return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
            //    },
            //});
            //一级产品
            $('#SSID').combobox({
                url: '../Product/productApi.aspx?method=QueryALLOneProductType&PID=1', valueField: 'TypeID', textField: 'TypeName', AddField: 'PinyinName',
                filter: function (q, row) {
                    var opts = $(this).combobox('options');
                    return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
                },
            });
            var pcode; var lt;
            ////一级产品
            //$('#TypeID').combobox({
            //    url: '../Product/productApi.aspx?method=QueryALLOneProductType&PID=1', valueField: 'TypeID', textField: 'TypeName', AddField: 'PinyinName',
            //    filter: function (q, row) {
            //        var opts = $(this).combobox('options');
            //        return row[opts.textField].indexOf(q) >= 0 || row[opts.AddField].indexOf(q) >= 0;
            //    },
            //    onSelect: function (r) {
            //        $.ajax({
            //            url: "../Product/productApi.aspx?method=QueryBrandCode&TypeID=" + r.TypeID, type: "post",
            //            success: function (data) {
            //                $("#BrandCode").textbox('setValue', data);
            //                $('#ProductCode').textbox('setValue', 'LT' + data);
            //                pcode = "LT" + data;
            //            }
            //        })
            //    }
            //});
            //一级产品
            $('#APTID').combobox({
                url: '../Product/productApi.aspx?method=QueryALLOneProductType', valueField: 'TypeID', textField: 'TypeName',
                onSelect: function (rec) {
                    if (rec.BrandCode == "DC") {
                        $('#TDTyreType').html("电池系列:");
                        $('#TDTypeID').html("电池品牌:");
                        $('#lSpecs').html("型号简称:");
                        $('#lFigure').html("电池型号:");
                        $('#TDTreadWidth').html("长:");
                        $('#TDFlatRatio').html("宽:");
                        $('#TDHubDiameter').html("高:");
                        $('#lModel').html("国标型号:");
                        $('#lGoodsCode').html("厂家编码:");
                        $('#ProductUnit').combobox('setValue', '瓶');
                        $('#LoadIndex').textbox('disable');
                        $('#SpeedLevel').combobox('disable');
                        $('#Manufacturer').textbox('disable');
                        $('#Servicer').textbox('disable');
                        $('#Seller').textbox('disable');
                        $('#SellerAddress').textbox('disable');
                        $('#CommType').combobox('disable');
                        $('#THeight').textbox('enable');

                    }
                    else if (rec.BrandCode == "JY") {
                        $('#TDTyreType').html("机油类型:");
                        $('#TDTypeID').html("机油品牌:");
                        $('#lSpecs').html("机油级别:");
                        $('#lFigure').html("机油分类:");
                        $('#TDTreadWidth').html("胎面宽度:");
                        $('#TDFlatRatio').html("扁平比:");
                        $('#TDHubDiameter').html("轮毂尺寸:");
                        $('#lModel').html("机油容量:");
                        $('#lGoodsCode').html("货品代码:");
                        $('#ProductUnit').combobox('setValue', '条');
                        $('#LoadIndex').textbox('enable');
                        $('#SpeedLevel').combobox('enable');
                        $('#Manufacturer').textbox('enable');
                        $('#Servicer').textbox('enable');
                        $('#Seller').textbox('enable');
                        $('#SellerAddress').textbox('enable');
                        $('#CommType').combobox('enable');
                        $('#THeight').textbox('disable');
                        $('#LoadIndex').textbox('setValue', '0');
                        $('#SpeedLevel').combobox('setValue', 'B');
                        $('#ProductUnit').combobox('setValue', '桶');
                        $('#BundleNum').textbox('setValue', '1');

                    }
                    else if (rec.BrandCode == "PJ") {
                        $('#TDTypeID').html("配件品牌:");
                        $('#lGoodsCode').html("零件编码:");
                        $('#TDProductName').html("零件名称:");
                        $('#ProductUnit').combobox('setValue', '');

                        $("#Figure").textbox('options').required = false;
                        $("#Figure").textbox('textbox').validatebox('options').required = false;
                        $("#Figure").textbox('validate');
                        $("#Specs").textbox('options').required = false;
                        $("#Specs").textbox('textbox').validatebox('options').required = false;
                        $("#Specs").textbox('validate');
                        $("#Model").textbox('options').required = false;
                        $("#Model").textbox('textbox').validatebox('options').required = false;
                        $("#Model").textbox('validate');

                        $('#THeight').textbox('disable');

                        $('#lSpecs').hide();
                        $('#lSpecs1').hide();
                        $('#TDTreadWidth').hide();
                        $('#TDTreadWidth1').hide();
                        $('#lFigure').hide();
                        $('#lFigure1').hide();
                        $('#lModel').hide();
                        $('#lModel1').hide();
                        $('#TDFlatRatio').hide();
                        $('#TDFlatRatio1').hide();

                        $('#TDHubDiameter').hide();
                        $('#TDHubDiameter1').hide();

                        $('#TDLoadIndex').hide();
                        $('#TDLoadIndex1').hide();
                        $('#TDServicer').hide();
                        $('#TDServicer1').hide();
                        $('#TDSpeedLevel').hide();
                        $('#TDSpeedLevel1').hide();
                        $('#TDSeller').hide();
                        $('#TDSeller1').hide();
                        $('#TDBorn').hide();
                        $('#TDBorn1').hide();
                        $('#TDSellerAddress').hide();
                        $('#TDSellerAddress1').hide();
                        $('#TDBundleNum').hide();
                        $('#TDBundleNum1').hide();
                        $('#TDCommType').hide();
                        $('#TDCommType1').hide();
                        $('#TDManufacturer').hide();
                        $('#TDManufacturer1').hide();
                        $('#TDTyreType').hide();
                        $('#TDTyreType1').hide();

                    }
                    else if (rec.BrandCode == "YP") {

                        $('#TDTypeID').html("品牌名称:");
                        $('#lGoodsCode').html("用品编码:");
                        $('#TDProductName').html("用品名称:");
                        $('#ProductUnit').combobox('setValue', '');

                        $("#Figure").textbox('options').required = false;
                        $("#Figure").textbox('textbox').validatebox('options').required = false;
                        $("#Figure").textbox('validate');
                        $("#Specs").textbox('options').required = false;
                        $("#Specs").textbox('textbox').validatebox('options').required = false;
                        $("#Specs").textbox('validate');
                        $("#Model").textbox('options').required = false;
                        $("#Model").textbox('textbox').validatebox('options').required = false;
                        $("#Model").textbox('validate');

                        $('#THeight').textbox('disable');

                        $('#lSpecs').hide();
                        $('#lSpecs1').hide();
                        $('#TDTreadWidth').hide();
                        $('#TDTreadWidth1').hide();
                        $('#lFigure').hide();
                        $('#lFigure1').hide();
                        $('#lModel').hide();
                        $('#lModel1').hide();
                        $('#TDFlatRatio').hide();
                        $('#TDFlatRatio1').hide();

                        $('#TDHubDiameter').hide();
                        $('#TDHubDiameter1').hide();

                        $('#TDLoadIndex').hide();
                        $('#TDLoadIndex1').hide();
                        $('#TDServicer').hide();
                        $('#TDServicer1').hide();
                        $('#TDSpeedLevel').hide();
                        $('#TDSpeedLevel1').hide();
                        $('#TDSeller').hide();
                        $('#TDSeller1').hide();
                        $('#TDBorn').hide();
                        $('#TDBorn1').hide();
                        $('#TDSellerAddress').hide();
                        $('#TDSellerAddress1').hide();
                        $('#TDBundleNum').hide();
                        $('#TDBundleNum1').hide();
                        $('#TDCommType').hide();
                        $('#TDCommType1').hide();
                        $('#TDManufacturer').hide();
                        $('#TDManufacturer1').hide();
                        $('#TDTyreType').hide();
                        $('#TDTyreType1').hide();
                    }
                    else {
                        $('#TDTyreType').html("轮胎类型:");
                        $('#TDTypeID').html("轮胎品牌:");
                        $('#lSpecs').html("轮胎规格:");
                        $('#lFigure').html("轮胎花纹:");
                        $('#TDTreadWidth').html("胎面宽度:");
                        $('#TDFlatRatio').html("扁平比:");
                        $('#TDHubDiameter').html("轮毂尺寸:");
                        $('#lModel').html("轮胎型号:");
                        $('#lGoodsCode').html("货品代码:");
                        $('#TDProductName').html("产品名称:");
                        $('#ProductUnit').combobox('setValue', '条');
                        $('#LoadIndex').textbox('enable');
                        $('#SpeedLevel').combobox('enable');
                        $('#Manufacturer').textbox('enable');
                        $('#Servicer').textbox('enable');
                        $('#Seller').textbox('enable');
                        $('#SellerAddress').textbox('enable');
                        $('#CommType').combobox('enable');
                        $('#THeight').textbox('disable');

                        $("#Figure").textbox('options').required = true;
                        $("#Figure").textbox('textbox').validatebox('options').required = true;
                        $("#Figure").textbox('validate');
                        $("#Specs").textbox('options').required = true;
                        $("#Specs").textbox('textbox').validatebox('options').required = true;
                        $("#Specs").textbox('validate');
                        $("#Model").textbox('options').required = true;
                        $("#Model").textbox('textbox').validatebox('options').required = true;
                        $("#Model").textbox('validate');

                        $('#lSpecs').show();
                        $('#lSpecs1').show();
                        $('#TDTreadWidth').show();
                        $('#TDTreadWidth1').show();
                        $('#lFigure').show();
                        $('#lFigure1').show();
                        $('#lModel').show();
                        $('#lModel1').show();
                        $('#TDFlatRatio').show();
                        $('#TDFlatRatio1').show();

                        $('#TDHubDiameter').show();
                        $('#TDHubDiameter1').show();
                        $('#TDLoadIndex').show();
                        $('#TDLoadIndex1').show();
                        $('#TDServicer').show();
                        $('#TDServicer1').show();
                        $('#TDSpeedLevel').show();
                        $('#TDSpeedLevel1').show();
                        $('#TDSeller').show();
                        $('#TDSeller1').show();
                        $('#TDBorn').show();
                        $('#TDBorn1').show();
                        $('#TDSellerAddress').show();
                        $('#TDSellerAddress1').show();
                        $('#TDBundleNum').show();
                        $('#TDBundleNum1').show();
                        $('#TDCommType').show();
                        $('#TDCommType1').show();
                        $('#TDManufacturer').show();
                        $('#TDManufacturer1').show();
                        $('#TDTyreType').show();
                        $('#TDTyreType1').show();
                    }
                    $('#ProductCode').textbox('setValue', rec.BrandCode);
                    $("#BrandCode").textbox('setValue', '');
                    $('#TypeID').combobox('clear');
                    $('#TypeID').combobox({
                        valueField: 'TypeID', textField: 'TypeName', PinyinField: 'PinyinName',
                        url: '../Product/productApi.aspx?method=QueryALLOneProductType&PID=' + rec.TypeID,
                        onSelect: function (r) {
                            $("#BrandCode").textbox('setValue', r.BrandCode);
                            $('#ProductCode').textbox('setValue', rec.BrandCode + r.BrandCode);
                            pcode = rec.BrandCode + r.BrandCode;
                            lt = rec.BrandCode;
                        },
                        filter: function (q, row) {
                            var opts = $(this).combobox('options');
                            return row[opts.textField].indexOf(q) >= 0 || row[opts.PinyinField].indexOf(q) >= 0;
                        }
                    });
                }
            });
            //$("#TypeID").combobox({
            //    onChange: function (r) {
            //        $.ajax({
            //            url: "productApi.aspx?method=QueryBrandCode&TypeID=" + r, type: "post",
            //            success: function (data) {
            //                $("#BrandCode").textbox('setValue', data);
            //                $('#ProductCode').textbox('setValue', 'LT' + data);
            //            }
            //        })
            //    }
            //})
            $('#Specs').textbox('textbox').blur(function (e) {
                if (lt == "LT") {

                    var sp = $('#Specs').textbox('getValue').match(/\d+/g);
                    if (sp != null) {

                        if (sp.length >= 3) {
                            $('#TreadWidth').numberbox('setValue', sp[0]);
                            $('#FlatRatio').numberbox('setValue', sp[1]);
                            $('#HubDiameter').numberbox('setValue', sp[2]);
                            $('#ProductCode').textbox('setValue', pcode + sp[0] + sp[1] + sp[2]);
                        }
                        else if (sp.length < 3) {
                            $('#FlatRatio').numberbox('setValue', 0);
                            $('#TreadWidth').numberbox('setValue', sp[0]);
                            $('#HubDiameter').numberbox('setValue', sp[1]);
                            $('#ProductCode').textbox('setValue', pcode + sp[0] + sp[1]);
                        }
                    }
                }
                else if (lt == "JY") {

                    $('#ProductName').textbox('setValue', $('#TypeID').combobox('getText') + " " + $('#Specs').textbox('getValue') + " " + $('#Model').textbox('getValue') + " " + $('#Figure').textbox('getValue'));

                }
                else if (lt == "DC") {

                    $('#ProductName').textbox('setValue', $('#TypeID').combobox('getText') + " " + $('#Specs').textbox('getValue'));

                }
                //alert(ss);
            });
            $('#Model').textbox('textbox').blur(function (e) {
                if (lt == "JY") {

                    $('#ProductName').textbox('setValue', $('#TypeID').combobox('getText') + " " + $('#Specs').textbox('getValue') + " " + $('#Model').textbox('getValue') + " " + $('#Figure').textbox('getValue'));

                }
            });
            $('#Figure').textbox('textbox').blur(function (e) {
                if (lt == "JY") {

                    $('#ProductName').textbox('setValue', $('#TypeID').combobox('getText') + " " + $('#Specs').textbox('getValue') + " " + $('#Model').textbox('getValue') + " " + $('#Figure').textbox('getValue'));

                }
            });
            $('#APID').combobox('textbox').bind('focus', function () { $('#APID').combobox('showPanel'); });
            $('#ASID').combobox('textbox').bind('focus', function () { $('#ASID').combobox('showPanel'); });
            $('#TypeID').combobox('textbox').bind('focus', function () { $('#TypeID').combobox('showPanel'); });
            $('#SpeedLevel').combobox('textbox').bind('focus', function () { $('#SpeedLevel').combobox('showPanel'); });
            $('#Born').combobox('textbox').bind('focus', function () { $('#Born').combobox('showPanel'); });
            $('#TyreType').combobox('textbox').bind('focus', function () { $('#TyreType').combobox('showPanel'); });
            $('#ASpecs').textbox('textbox').keydown(function (e) {
                if (e.keyCode == 13) {
                    dosearch();
                }
            });
            $('#AFigure').textbox('textbox').keydown(function (e) {
                if (e.keyCode == 13) {
                    dosearch();
                }
            });
            $('#AGoodsCode').textbox('textbox').keydown(function (e) {
                if (e.keyCode == 13) {
                    dosearch();
                }
            });
            var IsModifySpecsData = "<%=UserInfor.IsModifySpecsData%>";
            if (IsModifySpecsData == "0") {
                $("#btnAdd").css('display', 'none')
                $("#btnUpdate").css('display', 'none')
                $("#btnDel").css('display', 'none')
                $("#btnShare").css('display', 'none')
                $("#btnSave").css('display', 'none')

            }
        });
        //查询
        function dosearch() {
            $('#dg').datagrid('clearSelections');
            var gridOpts = $('#dg').datagrid('options');
            gridOpts.url = '../House/houseApi.aspx?method=QueryALLProductPageData';
            $('#dg').datagrid('load', {
                PID: $("#APID").combobox('getValue'),//一级产品
                SID: $('#ASID').combobox('getValue'),
                Specs: $('#ASpecs').val(),
                Figure: $('#AFigure').val(),
                GoodsCode: $('#AGoodsCode').val(),
                ProductCode: $('#AProductCode').val(),
                UpClientID: $('#AUpClientID').combobox('getValue'),
                DelFlag: $("#dFlag").combobox('getValue'),
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
    <div id="saPanel" name="SelectDiv1" class="easyui-panel" title="" data-options="iconCls:'icon-search'" style="width: 100%">
        <table>
            <tr>
                <td style="text-align: right;">大类:
                </td>
                <td>
                    <input id="APID" class="easyui-combobox" style="width: 100px;" panelheight="auto" />
                </td>
                <td style="text-align: right;">规格:
                </td>
                <td>
                    <input id="ASpecs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 100px" />
                </td>
                <td id="AGoodsCodeTd" style="text-align: right;">货品代码:
                </td>
                <td>
                    <input id="AGoodsCode" class="easyui-textbox" data-options="prompt:'请输入货品代码'" style="width: 100px" />
                </td>
                <td style="text-align: right;">所属公司:
                </td>
                <td>
                    <input id="AUpClientID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'UpClientID',textField:'UpClientShortName',url:'../Client/clientApi.aspx?method=QeurylistUpClientData'" panelheight="auto" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">品牌:
                </td>
                <td>
                    <input id="ASID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'TypeID',textField:'TypeName'" />
                </td>
                <td id="AFigureTd" style="text-align: right;">花纹:
                </td>
                <td>
                    <input id="AFigure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 100px" />
                </td>
                <td style="text-align: right;">产品编码:
                </td>
                <td>
                    <input id="AProductCode" class="easyui-textbox" data-options="prompt:'请输入产品编码'" style="width: 100px" />
                </td>
                <td style="text-align: right;">启用状态:
                </td>
                <td>
                    <select class="easyui-combobox" id="dFlag" style="width: 100px;" panelheight="auto">
                        <option value="0">启用</option>
                        <option value="1">停用</option>
                        <option value="-1">全部</option>
                    </select>
                </td>
            </tr>
        </table>
    </div>
    <table id="dg" class="easyui-datagrid">
    </table>
    <div id="toolbar">
        <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="dosearch()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addItem()" id="btnAdd">&nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-edit" plain="false" onclick="editItem()" id="btnUpdate">&nbsp;修&nbsp;改&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelItem()" id="btnDel">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-application_put" plain="false" onclick="AwbExport()">&nbsp;导&nbsp;出&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-connect" plain="false" onclick="ShareCode()" id="btnShare">&nbsp;共享编码&nbsp;</a>&nbsp;&nbsp;<a href="#" id="btnPicture" class="easyui-linkbutton" iconcls="icon-picture_add" plain="false" onclick="addInsurancePic()">&nbsp;批量上传花纹照片&nbsp;</a>&nbsp;&nbsp;<a href="#" class="easyui-linkbutton"
            iconcls="icon-arrow_refresh" plain="false" onclick="SyncSuppier()">&nbsp;同步供应商&nbsp;</a>
        <form runat="server" id="fm1">
            <asp:Button ID="btnDerived" runat="server" Style="display: none;" Text="导出" OnClick="btnDerived_Click" />
        </form>
    </div>
    <div id="dlg" class="easyui-dialog" style="width: 800px; height: 650px; padding: 0px" closed="true" buttons="#dlg-buttons">
        <form id="fm" class="easyui-form" method="post" enctype="multipart/form-data">
            <input type="hidden" name="SID" id="SID" />
            <div id="saPanel">
                <table style="width: 100%">
                    <tr>
                        <td style="text-align: right;">产品编码:
                        </td>
                        <td>
                            <input id="ProductCode" name="ProductCode" class="easyui-textbox" style="width: 200px;" panelheight="auto" />
                        </td>
                        <td style="text-align: right;" id="TDProductName">产品名称:
                        </td>
                        <td>
                            <input id="ProductName" name="ProductName" class="easyui-textbox" style="width: 250px;" panelheight="auto" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">品牌大类:
                        </td>
                        <td>
                            <input id="APTID" class="easyui-combobox" style="width: 200px;" data-options="required:true" panelheight="auto" />
                        </td>
                        <td style="text-align: right;" id="TDTHeight">总高:
                        </td>
                        <td>
                            <input name="THeight" id="THeight" class="easyui-numberbox" data-options="min:0,precision:0"
                                style="width: 200px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" id="TDTypeID">轮胎品牌:
                        </td>
                        <td>
                            <input id="TypeID" name="TypeID" class="easyui-combobox" style="width: 200px;" data-options="valueField:'TypeID',textField:'TypeName',url:'productApi.aspx?method=QueryALLOneProductType&PID=1'" panelheight="auto" />
                        </td>
                        <td style="text-align: right;" id="TDTyreType">轮胎类型:
                        </td>
                        <td id="TDTyreType1">
                            <select class="easyui-combobox" id="TyreType" name="TyreType" style="width: 200px;" panelheight="auto">
                                <option value="0">四季胎</option>
                                <option value="1">冬季胎</option>
                                <option value="2">内胎</option>
                                <option value="3">AGM</option>
                                <option value="4">EFB</option>
                                <option value="5">普通电池</option>
                                <option value="6">半合成</option>
                                <option value="7">全合成</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">品牌代码:
                        </td>
                        <td>
                            <input id="BrandCode" name="BrandCode" class="easyui-textbox" style="width: 200px;" panelheight="auto" />
                        </td>
                        <td style="text-align: right;">适配车型:
                        </td>
                        <td>
                            <input name="CarModel" id="CarModel" class="easyui-textbox" data-options="prompt:'请输入适配车型'" style="width: 200px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" id="lSpecs">轮胎规格:
                        </td>
                        <td id="lSpecs1">
                            <input name="Specs" id="Specs" class="easyui-textbox" data-options="required:true" style="width: 200px;" />
                        </td>
                        <td style="text-align: right;" id="TDTreadWidth">胎面宽度:
                        </td>
                        <td id="TDTreadWidth1">
                            <input name="TreadWidth" id="TreadWidth" class="easyui-numberbox" data-options="min:0,precision:0"
                                style="width: 200px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" id="lFigure">轮胎花纹:
                        </td>
                        <td id="lFigure1">
                            <input name="Figure" id="Figure" class="easyui-textbox" data-options="required:true" style="width: 200px;" />
                        </td>
                        <td style="text-align: right;" id="TDFlatRatio">扁平比:
                        </td>
                        <td id="TDFlatRatio1">
                            <input name="FlatRatio" id="FlatRatio" class="easyui-numberbox" data-options="min:0,precision:0"
                                style="width: 200px;" />
                        </td>

                    </tr>
                    <tr>
                        <td style="text-align: right;" id="lModel">轮胎型号:
                        </td>
                        <td id="lModel1">
                            <input name="Model" id="Model" class="easyui-textbox" data-options="required:true" style="width: 200px;" />
                        </td>
                        <td style="text-align: right;" id="TDHubDiameter">轮毂尺寸:
                        </td>
                        <td id="TDHubDiameter1">
                            <input name="HubDiameter" id="HubDiameter" class="easyui-numberbox" data-options="min:0,precision:0"
                                style="width: 200px;" />
                        </td>

                    </tr>
                    <tr>
                        <td style="text-align: right;" id="lGoodsCode">货品代码:
                        </td>
                        <td>
                            <input name="GoodsCode" id="GoodsCode" class="easyui-textbox" data-options="required:true" style="width: 200px;" />
                        </td>
                        <td style="text-align: right;" id="TDManufacturer">制造商:
                        </td>
                        <td id="TDManufacturer1">
                            <input name="Manufacturer" id="Manufacturer" class="easyui-textbox" style="width: 200px;" />
                        </td>

                    </tr>
                    <tr>
                        <td style="text-align: right;" id="TDLoadIndex">载重指数:
                        </td>
                        <td id="TDLoadIndex1">
                            <input name="LoadIndex" id="LoadIndex" class="easyui-textbox" data-options="min:0,precision:0" style="width: 200px;" />
                        </td>
                        <td style="text-align: right;" id="TDServicer">服务商:
                        </td>
                        <td id="TDServicer1">
                            <input name="Servicer" id="Servicer" class="easyui-textbox" style="width: 200px;" />
                        </td>

                    </tr>
                    <tr>
                        <td style="text-align: right;" id="TDSpeedLevel">速度级别:
                        </td>
                        <td id="TDSpeedLevel1">
                            <input class="easyui-combobox" name="SpeedLevel" id="SpeedLevel" data-options="url:'../Data/TyreSpeedLevel.json',method:'get',valueField:'id',textField:'text'" style="width: 200px;" />
                        </td>
                        <td style="text-align: right;" id="TDSeller">销售商:
                        </td>
                        <td id="TDSeller1">
                            <input name="Seller" id="Seller" class="easyui-textbox" style="width: 200px;" />
                        </td>

                    </tr>
                    <tr>
                        <td style="text-align: right;" id="TDBorn">生产产地:
                        </td>
                        <td id="TDBorn1">
                            <select class="easyui-combobox" id="Born" name="Born" style="width: 200px;" panelheight="auto">
                                <option value="0">国产</option>
                                <option value="1">进口</option>
                            </select>
                        </td>
                        <td style="text-align: right;" id="TDSellerAddress">销售地址:
                        </td>
                        <td id="TDSellerAddress1">
                            <input name="SellerAddress" id="SellerAddress" class="easyui-textbox" style="width: 200px;" />
                        </td>

                    </tr>
                    <tr>
                        <td style="text-align: right;">销售单位:
                        </td>
                        <td>
                            <input class="easyui-combobox" name="ProductUnit" id="ProductUnit" data-options="url:'../Data/Package.json',method:'get',valueField:'id',textField:'text',panelHeight:'auto'"
                                style="width: 200px;" />
                        </td>
                        <td style="text-align: right;">所属公司:
                        </td>
                        <td>
                            <input id="UpClientID" name="UpClientID" class="easyui-combobox" style="width: 200px;" data-options="valueField:'UpClientID',textField:'UpClientShortName',url:'../Client/clientApi.aspx?method=QeurylistUpClientData'" panelheight="auto" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;" id="TDBundleNum">捆包数量:
                        </td>
                        <td id="TDBundleNum1">
                            <input name="BundleNum" id="BundleNum" class="easyui-textbox" style="width: 200px;" />
                        </td>
                        <td style="text-align: right;" id="TDCommType">规格类型:
                        </td>
                        <td id="TDCommType1">
                            <select class="easyui-combobox" id="CommType" name="CommType" style="width: 200px;" panelheight="auto">
                                <option value="0">普通规格</option>
                                <option value="1">常用规格</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">状态标识:
                        </td>
                        <td>
                            <select class="easyui-combobox" id="DelFlag" name="DelFlag" style="width: 200px;"
                                panelheight="auto" required="true">
                                <option value="0">启用</option>
                                <option value="1">停用</option>
                            </select>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">产品小图:
                        </td>
                        <td>
                            <div id="ThumbnailImgdiv">
                                <img id="ThumbnailImg" width="80%" height="100px" />
                            </div>
                            <input style="width: 250px; height: 30px;" id="Thumbnail" name="Thumbnail" class="easyui-filebox" data-options="prompt:'规格缩略小图',buttonText:'点击选择',onChange:change_Thumbnail" />
                        </td>
                  
                        <td style="text-align: right;">详情大图:
                        </td>
                        <td>
                            <div id="DetailDrawingImgdiv">
                                <img id="DetailDrawingImg" width="80%" height="100px" />
                            </div>
                            <input style="width: 250px; height: 30px;" id="DetailDrawing" name="DetailDrawing" class="easyui-filebox" data-options="prompt:'规格详情介绍',buttonText:'点击选择',onChange:change_DetailDrawing" />
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </div>
    <div id="dlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="saveItem()" id="btnSave">&nbsp;&nbsp;保&nbsp;存&nbsp;&nbsp;</a>&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlg').dialog('close')">&nbsp;&nbsp;取&nbsp;消&nbsp;&nbsp;</a>
    </div>
    <%-- 批量上传轮胎照片 --%>
    <div id="dlgUploadPic" class="easyui-dialog" style="width: 500px; height: 450px; padding: 1px 1px" closed="true" buttons="#dlgUploadPic-buttons">
        <form id="fmUploadPic" class="easyui-form" method="post" enctype="multipart/form-data">
            <table>
                <tr>
                    <td>
                        <input style="width: 250px; height: 30px;" id="InsurePic" name="InsurePic" class="easyui-filebox" data-options="prompt:'轮胎花纹照片',buttonText:'点击选择',onChange:change_photo" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="InsureImgdiv">
                            <img id="InsureImg" width="300px" height="300px" />
                        </div>
                    </td>
                </tr>
            </table>
        </form>
    </div>
    <div id="dlgUploadPic-buttons">
        <a href="javascript:void(0)" class="easyui-linkbutton" iconcls="icon-ok" onclick="savePic()">&nbsp;&nbsp;保&nbsp;存&nbsp;&nbsp;</a>&nbsp;&nbsp;
    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgUploadPic').dialog('close')">&nbsp;&nbsp;关&nbsp;闭&nbsp;&nbsp;</a>
    </div>
    <%-- 批量上传轮胎照片 --%>

    <div id="imgdlg" class="easyui-dialog" style="width: 70%; height: 600px; padding: 5px 5px" closed="true" buttons="#imgdlg-buttons">
        <img id="imgdlgImg" width="100%" height="auto" />
    </div>
    <div id="imgdlg-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#imgdlg').dialog('close')">关闭</a>
    </div>

    <div id="dlgShare" class="easyui-dialog" style="width: 1000px; height: 700px; padding: 1px" closed="true" buttons="#dlgShare-buttons">
        <div id="saPanel">
            <table>
                <tr>
                    <td style="text-align: right;">产品编码:
                    </td>
                    <td>
                        <input id="SProductCode" class="easyui-textbox" data-options="prompt:'请输入产品编码'" style="width: 100px" />
                    </td>

                    <td style="text-align: right;">品牌名称:
                    </td>
                    <td>
                        <input id="SSID" class="easyui-combobox" style="width: 100px;" data-options="valueField:'TypeID',textField:'TypeName'" />
                    </td>
                    <td style="text-align: right;">规格:
                    </td>
                    <td>
                        <input id="SSpecs" class="easyui-textbox" data-options="prompt:'请输入规格'" style="width: 100px" />
                    </td>

                    <td style="text-align: right;">货品代码:
                    </td>
                    <td>
                        <input id="SGoodsCode" class="easyui-textbox" data-options="prompt:'请输入货品代码'" style="width: 100px" />
                    </td>

                </tr>
                <tr>
                    <td style="text-align: right;">外部编码:
                    </td>
                    <td>
                        <input id="SCassCode" class="easyui-textbox" data-options="prompt:'请输入外部公司编码'" style="width: 100px" />
                    </td>
                    <td style="text-align: right;">共享编码:
                    </td>
                    <td>
                        <input id="SShareCode" class="easyui-textbox" data-options="prompt:'请输入共享编码'" style="width: 100px" />
                    </td>
                    <td style="text-align: right;">花纹:
                    </td>
                    <td>
                        <input id="SFigure" class="easyui-textbox" data-options="prompt:'请输入花纹'" style="width: 100px" />
                    </td>
                    <td style="text-align: right;">共享公司:
                    </td>
                    <td>
                        <select class="easyui-combobox" id="SCorpClass" style="width: 100px;" panelheight="auto" editable="false">
                            <option value="">全部</option>
                            <option value="1">开思</option>
                            <option value="2">广汽丰田</option>
                            <option value="3">广汽本田</option>
                            <option value="4">狄安祺达</option>
                            <option value="5">一汽丰田</option>
                        </select>
                    </td>
                </tr>
            </table>
        </div>
        <table id="dgMapping" class="easyui-datagrid">
        </table>
        <div id="MappingBar">
            <a href="#" class="easyui-linkbutton" iconcls="icon-search" plain="false" onclick="QueryProductMapping()">&nbsp;查&nbsp;询&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-add" plain="false" onclick="addProductMapping()">&nbsp;新&nbsp;增&nbsp;</a>&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" iconcls="icon-cut" plain="false" onclick="DelProductMapping()">&nbsp;删&nbsp;除&nbsp;</a>&nbsp;&nbsp;
        </div>
    </div>
    <div id="dlgShare-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgShare').dialog('close')">&nbsp;&nbsp;关&nbsp;闭&nbsp;&nbsp;</a>
    </div>

    <!--新增共享编码-->
    <div id="dlgAddShareCode" class="easyui-dialog" style="width: 500px; height: 300px; padding: 0px 0px"
        closed="true" buttons="#dlgAddShareCode-buttons">
        <div id="saPanel">
            <form id="fmAddShareCode" class="easyui-form" method="post">
                <input type="hidden" name="MID" />
                <table>
                    <tr>
                        <td style="text-align: right;">共享编码:
                        </td>
                        <td>
                            <input name="ShareCode" id="ShareCode" class="easyui-textbox" data-options="required:true" style="width: 250px;" />
                        </td>

                    </tr>
                    <tr>
                        <td style="text-align: right;">产品编码:
                        </td>
                        <td>
                            <input name="ProductCode" class="easyui-textbox" data-options="required:true" style="width: 250px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">外部公司:
                        </td>
                        <td>
                            <select class="easyui-combobox" id="CorpClass" name="CorpClass" style="width: 250px;"
                                panelheight="auto">
                                <option value="1">开思</option>
                                <option value="2">广汽丰田</option>
                                <option value="3">广汽本田</option>
                                <option value="4">狄安祺达</option>
                                <option value="5">一汽丰田</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right;">外部编码:
                        </td>
                        <td>
                            <input name="CassCode" id="CassCode" class="easyui-textbox" data-options="required:true" style="width: 250px;">
                        </td>
                    </tr>

                </table>
            </form>
        </div>
    </div>
    <div id="dlgAddShareCode-buttons">
        <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="SaveCargoProductMapping()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;
        <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgAddShareCode').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
    </div>
    <!--新增共享编码-->

    <!--同步基础数据供应商-->
    <div id="dlgSyncSupplier" class="easyui-dialog" style="width: 500px; height: 300px; padding: 0px 0px"
        closed="true" buttons="#dlgSyncSupplier-buttons">
        <div id="saPanel">
            <form id="fmdlgSyncSupplier" class="easyui-form" method="post">
                <input type="hidden" name="SID" id="SyncSID" />
                <input type="hidden" name="ProductCode" id="SyncProductCode" />
                <input type="hidden" name="TypeID" id="SyncTypeID" />
                <input type="hidden" name="SupplierID" id="SyncSupplierID" />
                <table>
                    <tr>
                        <td style="text-align: right;">云仓仓库:
                        </td>
                        <td>
                            <input id="AHouseID" name="HouseID" class="easyui-combobox" data-options="required:true" style="width: 250px;" />
                        </td>

                    </tr>
                    <tr>
                        <td style="text-align: right;">供应商:
                        </td>
                        <td>
                            <input id="AHID" name="SupplierNum" class="easyui-combobox" style="width: 250px;" data-options="valueField:'SupplierNum',textField:'ClientShortName',required:true" panelheight="auto" />
                        </td>
                    </tr>

                </table>
            </form>
        </div>
        <div id="dlgSyncSupplier-buttons">
            <a href="#" class="easyui-linkbutton" iconcls="icon-ok" onclick="SaveSyncSupplier()">&nbsp;保&nbsp;存&nbsp;</a>&nbsp;&nbsp;
    <a href="#" class="easyui-linkbutton" iconcls="icon-cancel" onclick="javascript:$('#dlgSyncSupplier').dialog('close')">&nbsp;关&nbsp;闭&nbsp;</a>
        </div>
    </div>
    <!--同步基础数据供应商-->



    <script type="text/javascript">
        //保存基础数据同步至供应商系统 
        function SaveSyncSupplier() {
            $('#fmdlgSyncSupplier').form('submit', {
                url: 'productApi.aspx?method=SaveSyncSupplier',
                onSubmit: function (param) {
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '同步成功!', 'info');
                        $('#dlgSyncSupplier').dialog('close'); 	// close the dialog
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '同步失败：' + result.Message, 'warning');
                    }
                }
            })

        }

        //同步基础数据供应商
        function SyncSuppier() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要同步的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlgSyncSupplier').dialog('open').dialog('setTitle', '同步基础数据到供应商');
                $('#SyncSID').val(row.SID);
                $('#SyncProductCode').val(row.ProductCode);
                $('#SyncTypeID').val(row.TypeID);

                //选择云仓仓库
                $('#AHouseID').combobox({
                    url: '../House/houseApi.aspx?method=CargoPermisionHouse',
                    valueField: 'HouseID', textField: 'Name',
                    onSelect: function (rec) {
                        $('#AHID').combobox('clear');
                        var url = '../House/houseApi.aspx?method=QueryHouseSupplier&HouseID=' + rec.HouseID + '&TypeID=' + row.TypeID;
                        $('#AHID').combobox('reload', url);
                        $('#AHID').combobox({
                            onSelect: function (fai) {
                                $('#SyncSupplierID').val(fai.SupplierID);
                            }
                        });
                        $('#AHID').combobox('textbox').bind('focus', function () { $('#AHID').combobox('showPanel'); });

                    }
                });

                $('#AHouseID').combobox('textbox').bind('focus', function () { $('#AHouseID').combobox('showPanel'); });

            }
        }

        //保存照片
        function savePic() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要批量上传的数据！', 'warning');
                return;
            }

            var json = JSON.stringify(rows)

            $('#fmUploadPic').form('submit', {
                url: 'productApi.aspx?method=PLSaveTyreFigurePic',
                onSubmit: function (param) {
                    console.log(param)
                    param.DGData = json;
                    var trd = $(this).form('enableValidation').form('validate');
                    return trd;
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $('#dlgUploadPic').dialog('close'); 	// close the dialog
                        dosearch();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            });
        }
        //上传来货单 单据照片
        function addInsurancePic() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要操作的数据！', 'warning');
                return;
            }
            if (row) {
                $('#InsurePic').filebox('clear');
                document.getElementById("InsureImg").setAttribute("src", "");
                $('#dlgUploadPic').dialog('open').dialog('setTitle', '批量上传轮胎花纹照片');
            }
        }

        //导出数据
        function AwbExport() {
            var row = $("#dg").datagrid('getData').rows[0];
            if (row == null) { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '没有数据可以进行导出，请重新查询！', 'warning'); return; }

            $.messager.progress({ msg: '请稍后,正在导出中...' });
            $.ajax({
                url: "productApi.aspx?method=QueryALLProductPageDataExport",
                data: {
                    SID: $('#ASID').combobox('getValue'),
                    Specs: $('#ASpecs').val(),
                    Figure: $('#AFigure').val(),
                    GoodsCode: $('#AGoodsCode').val(),
                    ProductCode: $('#AProductCode').val(),
                    UpClientID: $('#AUpClientID').combobox('getValue'),
                },
                success: function (text) {
                    $.messager.progress("close");
                    if (text == "OK") {
                        var obj = document.getElementById("<%=btnDerived.ClientID %>"); obj.click();
                    }
                    else { $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text, 'warning'); }
                }
            });
        }
        //保存共享编码
        function SaveCargoProductMapping() {
            $('#fmAddShareCode').form('submit', {
                url: 'productApi.aspx?method=SaveCargoProductMapping',
                onSubmit: function (param) {
                    return $(this).form('enableValidation').form('validate');
                },
                success: function (msg) {
                    var result = eval('(' + msg + ')');
                    if (result.Result) {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存成功!', 'info');
                        $('#dlgAddShareCode').dialog('close'); 	// close the dialog
                        QueryProductMapping();
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '保存失败：' + result.Message, 'warning');
                    }
                }
            })
        }

        //新增修改
        function addProductMapping() {
            $('#dlgAddShareCode').dialog('open').dialog('setTitle', '新增共享产品编码');
            $('#fmAddShareCode').form('clear');

        }
        //删除产品映射
        function DelProductMapping() {
            var rows = $('#dgMapping').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确定删除？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: 'productApi.aspx?method=DelCargoProductMapping',
                        type: 'post', dataType: 'json', data: { data: json },
                        success: function (text) {
                            //var result = eval('(' + msg + ')');
                            if (text.Result == true) {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '删除成功!', 'info');
                                $('#dgMapping').datagrid('reload');
                                $('#dgMapping').datagrid('unselectAll');
                            }
                            else {
                                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', text.Message, 'warning');
                            }
                        }
                    });
                }
            });
        }
        //查询共享编码数据
        function QueryProductMapping() {
            var mOpts = $('#dgMapping').datagrid('options');
            mOpts.url = 'productApi.aspx?method=QueryProductMapping';
            $('#dgMapping').datagrid('load', {
                ProductCode: $('#SProductCode').val(),
                Specs: $('#SSpecs').val(),
                GoodsCode: $('#SGoodsCode').val(),
                CassCode: $('#SCassCode').val(),
                ShareCode: $('#SShareCode').val(),
                Figure: $('#SFigure').val(),
                TypeID: $("#SSID").combobox('getValue'),
                SCorpClass: $("#SCorpClass").combobox('getValue')
            });
        }
        //打开共享编码窗口
        function ShareCode() {
            $('#dlgShare').dialog('open').dialog('setTitle', '共享产品编码');
            //$('#fm').form('clear');
        }
        //Specs

        $("#Specs").textbox({
            "onChange": function (newValue, oldValue) {
                //alert(newValue);
            }
        });
        function showImg(url) {
            $('#imgdlg').dialog('open').dialog('setTitle', '查看图片');
            $("#imgdlgImg").attr("src", url);
        }
        function change_photo() {
            PreviewImage($("input[name='InsurePic']")[0], 'InsureImg', 'InsureImgdiv');
        }
        function change_Thumbnail() {
            PreviewImage($("input[name='Thumbnail']")[0], 'ThumbnailImg', 'ThumbnailImgdiv');
        }
        function change_DetailDrawing() {
            PreviewImage($("input[name='DetailDrawing']")[0], 'DetailDrawingImg', 'DetailDrawingImgdiv');
        }
        function PreviewImage(fileObj, imgPreviewId, divPreviewId) {
            if (fileObj.files.length <= 0) {
                return;
            }
            var allowExtention = ".jpg,.bmp,.gif,.png,.pdf,.rar,.zip";//允许上传文件的后缀名document.getElementById("hfAllowPicSuffix").value;  
            var extention = fileObj.value.substring(fileObj.value.lastIndexOf(".") + 1).toLowerCase();
            var browserVersion = window.navigator.userAgent.toUpperCase();
            if (allowExtention.indexOf(extention) > -1) {
                if (fileObj.files) {//HTML5实现预览，兼容chrome、火狐7+等  
                    if (window.FileReader) {
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            document.getElementById(imgPreviewId).setAttribute("src", e.target.result);
                        }
                        reader.readAsDataURL(fileObj.files[0]);
                    } else if (browserVersion.indexOf("SAFARI") > -1) {
                        alert("不支持Safari6.0以下浏览器的图片预览!");
                    }
                } else if (browserVersion.indexOf("MSIE") > -1) {
                    if (browserVersion.indexOf("MSIE 6") > -1) {//ie6  
                        document.getElementById(imgPreviewId).setAttribute("src", fileObj.value);
                    } else {//ie[7-9]  
                        fileObj.select();
                        if (browserVersion.indexOf("MSIE 9") > -1)
                            fileObj.blur();//不加上document.selection.createRange().text在ie9会拒绝访问  
                        var newPreview = document.getElementById(divPreviewId + "New");
                        if (newPreview == null) {
                            newPreview = document.createElement("div");
                            newPreview.setAttribute("id", divPreviewId + "New");
                            newPreview.style.width = document.getElementById(imgPreviewId).width + "px";
                            newPreview.style.height = document.getElementById(imgPreviewId).height + "px";
                            newPreview.style.border = "solid 1px #d2e2e2";
                        }
                        newPreview.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod='scale',src='" + document.selection.createRange().text + "')";
                        var tempDivPreview = document.getElementById(divPreviewId);
                        tempDivPreview.parentNode.insertBefore(newPreview, tempDivPreview);
                        tempDivPreview.style.display = "none";
                    }
                } else if (browserVersion.indexOf("FIREFOX") > -1) {//firefox  
                    var firefoxVersion = parseFloat(browserVersion.toLowerCase().match(/firefox\/([\d.]+)/)[1]);
                    if (firefoxVersion < 7) {//firefox7以下版本  
                        document.getElementById(imgPreviewId).setAttribute("src", fileObj.files[0].getAsDataURL());
                    } else {//firefox7.0+                      
                        document.getElementById(imgPreviewId).setAttribute("src", window.URL.createObjectURL(fileObj.files[0]));
                    }
                } else {
                    document.getElementById(imgPreviewId).setAttribute("src", fileObj.value);
                }
            } else {
                alert("仅支持" + allowExtention + "为后缀名的文件!");
                fileObj.value = "";//清空选中文件  
                if (browserVersion.indexOf("MSIE") > -1) {
                    fileObj.select();
                    document.selection.clear();
                }
                fileObj.outerHTML = fileObj.outerHTML;
            }
        }
        
        function addItem() {
            $('#dlg').dialog('open').dialog('setTitle', '新增产品规格');
            $('#fm').form('clear');
            $('#ProductUnit').combobox('setValue', '条');
            $('#DelFlag').combobox('setValue', '0');
            
            $("#TypeID").combobox("readonly", false);
            $('#APTID').combobox('textbox').bind('focus', function () { $('#APTID').combobox('showPanel'); });
        }
        
        function editItem() {
            var row = $('#dg').datagrid('getSelected');
            if (row == null || row == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要修改的数据！', 'warning');
                return;
            }
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改产品规格');
                $('#fm').form('load', row);
                $('#SID').val(row.SID);
                $("#TypeID").combobox("readonly", true);

                $('#APTID').combobox('setValue', row.ParentID);
            }
        }
        
        function editItemByID(Did) {
            var row = $("#dg").datagrid('getData').rows[Did];
            //console.log(row)
            if (row) {
                $('#dlg').dialog('open').dialog('setTitle', '修改产品规格');
                $('#SID').val(row.SID);

                $('#APTID').combobox('setValue', row.ParentID);

                $('#TypeID').combobox('clear');
                var url = '../Product/productApi.aspx?method=QueryALLOneProductType&PID=' + row.ParentID;
                $('#TypeID').combobox('reload', url);


                $('#fm').form('load', row);

                $("#TypeID").combobox("readonly", true);
                $("#APTID").combobox("readonly", true);
            }
        }
        
        function DelItem() {
            var rows = $('#dg').datagrid('getSelections');
            if (rows == null || rows == "") {
                $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '请选择要删除的数据！', 'warning');
                return;
            }
            $.messager.confirm('<%= Cargo.Common.GetSystemNameAndVersion()%>', '确认删除此产品规格？', function (r) {
                if (r) {
                    var json = JSON.stringify(rows)
                    $.ajax({
                        url: '../House/houseApi.aspx?method=DelCargoProduct',
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

        //保存分类信息
        function saveItem() {
            $('#fm').form('submit', {
                url: '../House/houseApi.aspx?method=SaveProductSpecData',
                onSubmit: function (param) {
                    param.UpClientShortName = $('#UpClientID').combobox('getText')
                    param.TypeName = $('#TypeID').combobox('getText');
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
