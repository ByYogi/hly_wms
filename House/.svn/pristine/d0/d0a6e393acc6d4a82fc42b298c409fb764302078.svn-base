<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qyOrderDeliveryPic.aspx.cs" Inherits="Cargo.QY.qyOrderDeliveryPic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>订单签收</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="../Weixin/WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/style.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/jquery-weui.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/weuix.min.css" rel="stylesheet" />
    <style type="text/css">
        .imgParent {
            display: inline;
            padding-right: 2px;
        }

        .delPicAC {
            margin-left: 5px;
            margin-top: -3px;
            padding: 1px 5px;
            font-size: 10px;
            color: #fff;
            background-color: red;
            position: absolute;
            border-radius: 100px;
            display: inline-block;
        }
    </style>
</head>
<body ontouchstart>

    <ul class="weui-payrec">
        <div class="weui-pay-m">
            <li class="weui-pay-order">
                <dl class="weui-pay-line">
                    <dd class="weui-pay-name" style="text-align: center; font-size: 17px;">上传司机提货照片</dd>
                    <dt class="weui-pay-label">订 单 号：</dt>
                    <dd class="weui-pay-e">
                        <asp:Literal ID="ltlOrderNo" runat="server"></asp:Literal></dd>
                </dl>
                <dl class="weui-pay-line">
                    <dt class="weui-pay-label">收 货 人：</dt>
                    <dd class="weui-pay-e">
                        <asp:Literal ID="ltlAccept" runat="server"></asp:Literal>&nbsp;&nbsp;<asp:Literal ID="ltlAcceptCellphone" runat="server"></asp:Literal></dd>
                </dl>
                <%-- <dl class="weui-pay-line">
                    <dt class="weui-pay-label">收货地址：</dt>
                    <dd class="weui-pay-e">
                        <asp:Literal ID="ltlAcceptAddress" runat="server"></asp:Literal></dd>
                </dl>--%>
                <dl class="weui-pay-line">
                    <dt class="weui-pay-label">订单数量：</dt>
                    <dd class="weui-pay-e">
                        <asp:Literal ID="ltlPiece" runat="server"></asp:Literal></dd>
                </dl>
                <%-- <dl class="weui-pay-line">
                    <dt class="weui-pay-label">订单金额：</dt>
                    <dd class="weui-pay-e">
                        <asp:Literal ID="ltlCharge" runat="server"></asp:Literal>&nbsp;&nbsp;元</dd>
                </dl>--%>
                <dl class="weui-pay-line">
                    <dt class="weui-pay-label">开 单 员：</dt>
                    <dd class="weui-pay-e">
                        <asp:Literal ID="ltlCreateAwb" runat="server"></asp:Literal></dd>
                </dl>
                <dl class="weui-pay-line">
                    <dt class="weui-pay-label">业 务 员：</dt>
                    <dd class="weui-pay-e">
                        <asp:Literal ID="ltlSaleMan" runat="server"></asp:Literal></dd>
                </dl>
                <dl class="weui-pay-line">
                    <dt class="weui-pay-label">司机姓名：</dt>
                    <dd class="weui-pay-e">
                        <input class="weui-input" id="DeliveryDriverName" placeholder="请输入司机姓名" type="text" /></dd>
                </dl>
                <dl class="weui-pay-line">
                    <dt class="weui-pay-label">车牌号码：</dt>
                    <dd class="weui-pay-e">
                        <input class="weui-input" id="DriverCarNum" placeholder="请输入车牌号码" type="text" /></dd>
                </dl>
                <dl class="weui-pay-line">
                    <dt class="weui-pay-label">手机号码：</dt>
                    <dd class="weui-pay-e">
                        <input class="weui-input" id="DriverCellphone" placeholder="请输入手机号码" type="text" /></dd>
                </dl>
                <dl class="weui-pay-line">
                    <dt class="weui-pay-label">身份证号：</dt>
                    <dd class="weui-pay-e">
                        <input class="weui-input" id="DriverIDNum" placeholder="请输入身份证号" type="text" /></dd>
                </dl>
                <div class="container-fluid">
                    <div class="weui-uploader__bd">
                        <ul class="weui-uploader__files" id="uploaderFiles">
                            <li class="weui-uploader__file">
                                <div class="weui-uploader__input-box">
                                    <input id="uploaderInput" class="weui-uploader__input" onfocus="this.blur()" onclick="file_upload()" type="text" />上传照片
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </li>
        </div>
    </ul>
    <div class="pay-div">
        <a href="javascript:;" class="weui-btn weui-btn_primary" id="btnSubmit">&nbsp;&nbsp;保&nbsp;&nbsp;存&nbsp;&nbsp;</a>
    </div>
    <script src="../Weixin/WeUI/JS/jquery-2.1.4.js"></script>
    <script src="../Weixin/WeUI/JS/jweixin-1.6.0.js"></script>
    <script src="../Weixin/WeUI/JS/fastclick.js"></script>
    <script src="../Weixin/WeUI/JS/jquery-weui.js"></script>
    <script type="text/javascript">
        $(function () {
            configjssdk();
            $('#DeliveryDriverName').val("<%=order.DeliveryDriverName%>");
            $('#DriverCarNum').val("<%=order.DriverCarNum%>");
            $('#DriverCellphone').val("<%=order.DriverCellphone%>");
            $('#DriverIDNum').val("<%=order.DriverIDNum%>");
            $('#btnSubmit').on('click', function () {
                var ID = "<%=order.OrderID%>";
                if (her.sum <= 0) { $.alert('请上传照片'); return; }
                $.showLoading();
                $.ajax({
                    url: 'qyServices.aspx?method=AddOrderDelivery',
                    type: 'post', dataType: 'json', data: {
                        "OrderID": "<%=order.OrderID%>",
                        "OrderNo": "<%=order.OrderNo%>",
                        "DeliveryDriverName": $('#DeliveryDriverName').val(),
                        "DriverCarNum": $('#DriverCarNum').val(),
                        "DriverCellphone": $('#DriverCellphone').val(),
                        "DriverIDNum": $('#DriverIDNum').val(),
                        "MediaID": her.iid.toString()
                    },
                    success: function (text) {
                        $.hideLoading();
                        if (text.Result == true) {
                            $.alert("保存成功!");
                            window.location.href = "qyLogicTrack.aspx";
                        }
                        else {
                            $.toast('保存失败 失败原因：' + text.Message);
                        }
                    }
                });
            });
        })

        function configjssdk() {
            var weixinUrl = location.href.split('#')[0];//;
            //配置jssdk
            $.ajax({
                url: "qyServices.aspx?method=configJssdk",
                type: "post", dataType: "json", data: { Url: weixinUrl },
                cache: false, ifModified: true, async: false,
                success: function (msg) {
                    var json = eval(msg);
                    var config = {};
                    config.beta = true;
                    config.appId = json.appId;
                    config.nonceStr = json.nonceStr;
                    config.signature = json.signature;
                    config.debug = false;        // 添加你需要的JSSDK的权限
                    config.jsApiList = ['getLocation', 'chooseImage', 'openLocation', 'scanQRCode', 'uploadImage', 'downloadImage', 'getLocalImgData', 'scanQRCode', 'previewImage'];
                    config.timestamp = parseInt(json.timestamp);
                    wx.config(config);
                    wx.ready(function () {
                        //alert("jssdk配置成功");
                        //wx.config(config);
                    });
                    wx.error(function (res) {
                        alert(JSON.stringify(res));
                    });
                }
            })
        }
        // 5.1 拍照、本地选图
        var images = {
            localId: [],
            serverId: []//Media_Id
        };
        var her = {
            sum: 0,
            srcArr: [],
            iid: []//图片在本地服务器中的id  Media_Id
        };
        function file_upload() {
            //configjssdk();
            //拍照或从手机相册中选图接口
            wx.chooseImage({
                count: 3,//设置一次能选择的图片的数量 
                sizeType: ['original', 'compressed'],//指定是原图还是压缩,默认二者都有
                sourceType: ['album', 'camera'],//可以指定来源是相册还是相机,默认二者都有
                success: function (res) {   //微信返回了一个资源对象 
                    //res.localIds 是一个数组　保存了用户一次性选择的所有图片的信息　 　　　　　　　　
                    images.localId = res.localIds;//把图片的路径保存在images[localId]中--图片本地的id信息，用于上传图片到微信浏览器时使用
                    ulLoadToWechat(0); //把这些图片上传到微信服务器  一张一张的上传
                }
            });

        }
        //上传图片到微信
        function ulLoadToWechat(i) {
            length = images.localId.length; //本次要上传所有图片的数量
            wx.uploadImage({
                localId: images.localId[i], //图片在本地的id
                success: function (res) {
                    images.serverId.push(res.serverId);//上传图片到微信成功的回调函数 会返回一个媒体对象MediaID 存储了图片在微信的id
                    //her.pic = wxImgDown(res.serverId);
                    //上传成功后 后台立马把图片从微信服务器上下载下来，返回图片在本地服务器上的信息（具体内容和后台协调：这里返回的信息有图片的url和图片在本地服务器中的id）
                    her.iid.push(res.serverId); //把图片在本地服务器中的id专门保存到一个数组当中
                    her.sum++;
                    if (her.sum <= 3) {
                        creatImg(images.localId[i], res.serverId);//原图  创建img便签  将用户选择的图片显示在页面中
                    } else {
                        alert('只能选择3张图片');
                    }
                    i++;
                    if (i < length) {
                        ulLoadToWechat(i);
                    }

                },
                fail: function (res) {
                    alert(JSON.stringify(res));
                }
            });
        };

        //创建img的方法
        function creatImg(path, id) {
            if (her.sum <= 3) {
                var imgdiv = document.createElement('div');
                imgdiv.className = "imgParent";

                var img = document.createElement('img');
                $(img).attr('data-id', id);//给每个img添加一个data-id属性,值为该图片在数据库中的id
                $(img).attr('width', '77px');
                $(img).attr('height', '77px');
                img.src = path;

                //创建 删除小按钮  定位在了每张图片的左上角
                var delPicA = document.createElement('a');
                delPicA.innerHTML = 'X';
                delPicA.className = "delPicAC";
                $(delPicA).attr('data-id', id);

                $(imgdiv).append(delPicA);

                $(imgdiv).append(img);
                $('#uploaderFiles').append(imgdiv);

            } else {
                alert('最多只能选择3张图片');
            }

            her.srcArr.push(path);
        }
        //用户选好图片后,点击图片进行预览
        $('#uploaderFiles').on('click', 'img', function () {
            //调用预览图片的接口
            wx.previewImage({
                current: this.src,//当前显示图片的http连接
                urls: her.srcArr//需要预览图片的http连接列表
            });
        });
        //用户点击X 删除图片
        $('#uploaderFiles').on('click', 'a', function () {
            var id = $(this).attr('data-id');//每张图片上都有一个自定义属性保存了图片在后台中的id
            if (confirm("确定删除这张图片吗?")) {
                //删除要传后台中img的id 的数组中的数据
                for (var i = 0; i < her.iid.length; i++) {
                    if (her.iid[i] == id) {
                        her.iid.splice(i, 1);
                    }
                }
                her.sum--;
                //删除预览
                $(this).parent().remove();
            }
        });

    </script>
</body>
</html>
