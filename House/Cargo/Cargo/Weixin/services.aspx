<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="services.aspx.cs" Inherits="Cargo.Weixin.services" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>联系客服</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="WeUI/CSS/style.css" rel="stylesheet" />
    <link href="WeUI/CSS/jquery-weui.css" rel="stylesheet" />
    <style type="text/css">
        .item-col3 li {
            width: 100%;
            padding: 0px 12px 2px;
            margin: 2px;
            float: left;
            border: 1px solid #e2e4e5;
            position: relative;
        }

            .item-col3 li:hover {
                box-shadow: 0 0 20px rgba(0,198,226,.5);
                border-color: #00c1de;
            }
    </style>
    <!--引用百度地图API-->
    <script type="text/javascript" src="http://api.map.baidu.com/api?v=2.0&ak=sSbxPO24gRRq2xE9T3iF5haP"></script>
</head>
<body ontouchstart>
    <header class="wy-header" style="position: fixed; top: 0; left: 0; right: 0;  z-index: 200;">
        <a href="my.aspx">
            <div class="wy-header-icon-back"><span></span></div>
        </a>
        <div class="wy-header-title">联系客服</div>
    </header>
    <div style="height: 45px"></div>
    <!--百度地图容器-->
    <div style="width: 100%; height: 150px; border: #ccc solid 1px; margin: 2px; font-size: 12px" id="map"></div>

    <ul class="item-col3 y-clear">

        <li onclick="setBaidu(1)">
            <p style="color: #333; font-weight: 700; font-size: 15px;">广州迪乐泰</p>
            <p style="color: #999; font-size: 13px; line-height: 20px;">地址：广东省广州市白云区东平横岗东街</p>
            <p style="color: #999; font-size: 13px; line-height: 20px;">电话：<a href="tel:13265180164" style="color: #ec1515">13265180164</a></p>
            <p style="color: #999; font-size: 13px; line-height: 20px;">联系人：陈小姐</p>
            <div style="width: 40px; height: 40px; position: absolute;  top: 20px; right: 40px;">
                <img src="WeUI/image/services.png" style="display: block; width: 40px; height: 40px;">
            </div>

        </li>

        <li onclick="setBaidu(2)">
            <p style="color: #333; font-weight: 700; font-size: 15px;">湖南长沙前置仓</p>
            <p style="color: #999; font-size: 13px; line-height: 20px;">地址：湖南省长沙市岳麓区麓谷街道延农六期十六栋</p>
            <p style="color: #999; font-size: 13px; line-height: 20px;">电话：<a href="tel:13319578676" style="color: #ec1515">13319578676</a>、<a href="tel:13319570206" style="color: #ec1515">13319570206</a></p>
            <p style="color: #999; font-size: 13px; line-height: 20px;">联系人：张小姐</p>
             <div style="width: 40px; height: 40px; position: absolute;  top: 20px; right: 40px;">
                <img src="WeUI/image/services.png" style="display: block; width: 40px; height: 40px;">
            </div>
        </li>
        <li onclick="setBaidu(3)">
            <p style="color: #333; font-weight: 700; font-size: 15px;">湖北武昌前置仓</p>
            <p style="color: #999; font-size: 13px; line-height: 20px;">地址：湖北省武汉市洪山区白沙洲大道恒钢物流园</p>
            <p style="color: #999; font-size: 13px; line-height: 20px;">电话：<a href="tel:17771479223" style="color: #ec1515">17771479223</a>、<a href="tel:17762399163" style="color: #ec1515">17762399163</a></p>
            <p style="color: #999; font-size: 13px; line-height: 20px;">联系人：李小姐</p>
             <div style="width: 40px; height: 40px; position: absolute;  top: 20px; right: 40px;">
                <img src="WeUI/image/services.png" style="display: block; width: 40px; height: 40px;">
            </div>
        </li>
        <li>
            <p style="color: #333; font-weight: 700; font-size: 15px;">西北前置仓</p>
            <p style="color: #999; font-size: 13px; line-height: 20px;">地址：</p>
            <p style="color: #999; font-size: 13px; line-height: 20px;">电话：<a href="tel:17792329456" style="color: #ec1515">17792329456</a></p>
            <p style="color: #999; font-size: 13px; line-height: 20px;">联系人：文小姐</p>
            <div style="width: 40px; height: 40px; position: absolute;  top: 20px; right: 40px;">
                <img src="WeUI/image/services.png" style="display: block; width: 40px; height: 40px;">
            </div>
        </li>
    </ul>


</body>

<script type="text/javascript">
    function setBaidu(ind) {
        initMap(ind);
    }
    //创建和初始化地图函数：
    function initMap(ind) {
        createMap(ind);//创建地图
        setMapEvent();//设置地图事件
        //addMapControl();//向地图添加控件
        addMapOverlay();//向地图添加覆盖物
    }
    function createMap(ind) {
        map = new BMap.Map("map");
        if (ind == "1") {
            map.centerAndZoom(new BMap.Point(113.325, 23.263124), 17);//广州市迪乐泰
        } else if (ind == "2") {
            map.centerAndZoom(new BMap.Point(112.870461, 28.214055), 17);//长沙岳麓前置仓
        } else if (ind == "3") {
            map.centerAndZoom(new BMap.Point(114.297313, 30.514508), 17);//武昌前置仓
        }
    }
    function setMapEvent() {
        map.enableScrollWheelZoom();
        map.enableKeyboard();
        map.enableDragging();
        map.enableDoubleClickZoom()
    }
    function addClickHandler(target, window) {
        target.addEventListener("click", function () {
            target.openInfoWindow(window);
        });
    }
    function addMapOverlay() {
        var markers = [
          { content: "湖南省长沙市岳麓区前置仓", title: "湖南省长沙市岳麓区前置仓", imageOffset: { width: 0, height: 3 }, position: { lat: 28.21423, lng: 112.870102 } },
          { content: "广州迪乐泰", title: "广州迪乐泰", imageOffset: { width: 0, height: 3 }, position: { lat: 23.263024, lng: 113.324905 } },
            { content: "湖北省武汉市武昌前置仓", title: "湖北省武汉市武昌前置仓", imageOffset: { width: 0, height: 3 }, position: { lat: 30.514508, lng: 114.297313 } }
        ];
        for (var index = 0; index < markers.length; index++) {
            var point = new BMap.Point(markers[index].position.lng, markers[index].position.lat);
            var marker = new BMap.Marker(point, {
                icon: new BMap.Icon("http://api.map.baidu.com/lbsapi/createmap/images/icon.png", new BMap.Size(20, 25), {
                    imageOffset: new BMap.Size(markers[index].imageOffset.width, markers[index].imageOffset.height)
                })
            });
            var label = new BMap.Label(markers[index].title, { offset: new BMap.Size(25, 5) });
            var opts = {
                width: 200,
                title: markers[index].title,
                enableMessage: false
            };
            var infoWindow = new BMap.InfoWindow(markers[index].content, opts);
            marker.setLabel(label);
            addClickHandler(marker, infoWindow);
            map.addOverlay(marker);
        };
    }
    //向地图添加控件
    function addMapControl() {
        var scaleControl = new BMap.ScaleControl({ anchor: BMAP_ANCHOR_BOTTOM_LEFT });
        scaleControl.setUnit(BMAP_UNIT_IMPERIAL);
        map.addControl(scaleControl);
        var navControl = new BMap.NavigationControl({ anchor: BMAP_ANCHOR_TOP_LEFT, type: BMAP_NAVIGATION_CONTROL_LARGE });
        map.addControl(navControl);
    }
    var map;
    initMap(1);
</script>
</html>
