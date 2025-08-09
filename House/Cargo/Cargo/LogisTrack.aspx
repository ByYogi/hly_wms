<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogisTrack.aspx.cs" Inherits="Cargo.LogisTrack" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>广汽丰田轮胎订单状态查询</title>
    <script src="JS/easy/js/jquery.min.js" type="text/javascript"></script>
    <script src="JS/easy/js/jquery.easyui.min.js" type="text/javascript"></script>
    <link href="JS/easy/css/default/easyui.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        body {
            color: #666;
            font: 12px/150% Arial,Verdana,"宋体";
            font-family: arial;
        }

        #mohe-kuaidi_new .mh-icon {
            background: url("http://p9.qhimg.com/d/inn/f2e20611/kuaidi_new.png") no-repeat scroll 0 0 rgba(0, 0, 0, 0);
        }

        p, form, ol, ul, li, h3, menu {
            list-style: outside none none;
        }

        #mohe-kuaidi_new .mh-list-wrap .mh-list {
            border-top: 1px solid #eee;
            margin: 0 15px;
            padding: 15px 0;
        }

            #mohe-kuaidi_new .mh-list-wrap .mh-list ul {
                max-height: 255px;
                _height: 255px;
                padding-left: 75px;
                padding-right: 20px;
                --overflow: auto;
                height: 626px;
            }

            #mohe-kuaidi_new .mh-list-wrap .mh-list li {
                position: relative;
                border-bottom: 1px solid #f5f5f5;
                margin-bottom: 8px;
                padding-bottom: 8px;
                color: #666;
            }

                #mohe-kuaidi_new .mh-list-wrap .mh-list li.first {
                    color: #3eaf0e;
                }

                #mohe-kuaidi_new .mh-list-wrap .mh-list li p {
                    line-height: 20px;
                }

                #mohe-kuaidi_new .mh-list-wrap .mh-list li .before {
                    position: absolute;
                    left: -13px;
                    top: 2.2em;
                    height: 82%;
                    width: 0;
                    border-left: 2px solid #ddd;
                }

                #mohe-kuaidi_new .mh-list-wrap .mh-list li .after {
                    position: absolute;
                    left: -16px;
                    top: 1.2em;
                    width: 8px;
                    height: 8px;
                    background: #ddd;
                    border-radius: 6px;
                }

                #mohe-kuaidi_new .mh-list-wrap .mh-list li.first .after {
                    background: #3eaf0e;
                }

        #mohe-kuaidi_new .mh-icon-new {
            position: absolute;
            left: -20px;
            top: 1.5em;
            width: 41px;
            height: 18px;
            margin-left: -41px;
            margin-top: -9px;
            background-position: 0 -58px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#ATagCode").textbox('textbox').css("font-size", "20pt");
            $("#ATagCode").textbox('textbox').css("padding-top", "20px");
        })
        function query() {
            var keyword = $("#ATagCode").val();
            if (keyword == "") {
                $.messager.alert('提示', '请输入发票号码');
            } else {
                $.messager.progress({ msg: '请稍后,查询中...' });
                $.ajax({
                    url: "/Order/orderApi.aspx?method=QueryGFOrderStatus&keyword=" + keyword,
                    dataType: 'json',
                    success: function (text) {
                        $.messager.progress("close");
                        $("#lblStatusTrack").html('');
                        if (text.Result == true) {
                            var ldl = document.getElementById("lblStatusTrack");
                            ldl.innerHTML = text.Message;
                        } else {
                            $.messager.alert('提示', text.Message);
                        }
                    }, error: function (text) {
                        $.messager.progress("close");
                        $("#lblStatusTrack").html('');
                        $.messager.alert('错误', '查询失败');
                    }
                });
            }
        }
    </script>
</head>

<body>
    <form id="form1" runat="server">
        <img src="CSS/image/gflogo.jpg" style="height: 110px; /*display: block; */ margin: auto;" />
    <div class="cantent" style="text-align: center;">
        <p style="color: red; font-size: 20px;">广汽丰田轮胎订单状态查询</p>
        <p></p>
        <input id="ATagCode" class="easyui-textbox" data-options="prompt:'请输入7位发票号码'" style="width: 90%; height: 75px; font-size: 45px;" />
        <p style="width: 60%; margin-left: 20%;" id="Prompt">提示:字母部分不区分大小写.</p>
        <p>
            <a id="query" onclick="query()" href="#" class="easyui-linkbutton" style="width: 60%; height: 60px; border-radius: 5px; background-color: #0080C0; font-size: 25px">&nbsp;&nbsp;查&nbsp;询&nbsp;&nbsp;</a>
        </p>
        <div style="text-align: left;position: absolute;right: 50%;left: 50%;transform: translateX(-50%);width:50%;">
            <div data-mohe-type="kuaidi_new" class="g-mohe" id="mohe-kuaidi_new">
                <div>
                    <div class="mohe-wrap mh-wrap">
                        <div class="mh-cont mh-list-wrap mh-unfold">
                            <div class="mh-list" id="lblStatusTrack">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
