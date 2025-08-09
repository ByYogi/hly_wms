<%@ Page Title="标点地图" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PunctuationMap.aspx.cs" Inherits="Cargo.Report.PunctuationMap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .box {
            position: absolute;
            left: 10px;
            top: 10px;
            height: 20px;
            z-index: 10000;
            background-color: #fff;
            padding: 10px 10px;
        }

        .btnBox {
            font-size: 15px;
            margin-bottom: 10px;
        }

            .btnBox label {
                margin: 0px 5px;
                cursor: pointer;
            }
    </style>
    <script charset="utf-8" src="https://map.qq.com/api/gljs?v=1.exp&key=OB4BZ-D4W3U-B7VVO-4PJWW-6TKDJ-WPB77"></script>
    <script type="text/javascript">
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="box">
        <div class="btnBox" id="btnBox">
        <%--    <label for="hz">
                <input type="radio" id="hz" value="0" name="type" />
                华中
            </label>
            <label for="hn">
                <input type="radio" id="hn" value="1" name="type" />
                华南
            </label>
            <label for="hb">
                <input type="radio" id="hb" value="2" name="type" />
                华北
            </label>
            <label for="hd">
                <input type="radio" id="hd" value="3" name="type" />
                华东
            </label>
            <label for="db">
                <input type="radio" id="db" value="4" name="type" />
                东北
            </label>
            <label for="xb">
                <input type="radio" id="xb" value="5" name="type" />
                西北
            </label>
            <label for="xn">
                <input type="radio" id="xn" value="6" name="type" />
                西南
            </label>--%>
        </div>
    </div>
    <div>
        <div id="map"></div>
    </div>
    <script type="text/javascript">
        var viewMode = '2D';
        var HouseList = [];
        function changeViewMode() {
            if (viewMode == '2D') {
                map.setViewMode('3D');
                map.setPitch(70);
                viewMode = '3D';
            } else {
                map.setViewMode('2D');
                viewMode = '2D';
            }
        }
        function restore() {
            infoWindow.close();
            markerCluster.setMap(null);
            markerCluster = null;
            //map.easeTo({ zoom: 5.2, rotation: 0, center: MapCenter }, { duration: 2000 });//平滑缩放,旋转到指定级别
            map.setBaseMap({
                type: 'vector',
                features: ['base', 'building2d'],
            });
            var bounds = new TMap.LatLngBounds();

            //判断标注点是否在范围内
            MarkerGeometries.forEach(function (item) {
                //若坐标点不在范围内，扩大bounds范围
                if (bounds.isEmpty() || !bounds.contains(item.position)) {
                    bounds.extend(item.position);
                }
            })
            //设置地图可视范围
            map.fitBounds(bounds, {
                padding: 100 // 自适应边距
            });
        }
        function reset() {
            map.setCenter(new TMap.LatLng(35, 106)); 
            map.zoomTo(5.2);  // 或 map.setZoom(15);
        }
        $('#map').append('<input type="button" id="restore" onclick="restore()" value="复位" style="right: 31px;height: 33px;width: 39px;top: 88px;z-index: 10000;background-color: #ffffff;border: none;border-radius: 3px;outline: none;position:absolute" />');
        $('#map').append('<input type="button" id="reset" onclick="reset()" value="还原" style="right: 31px;height: 33px;width: 39px;top: 128px;z-index: 10000;background-color: #ffffff;border: none;border-radius: 3px;outline: none;position:absolute" />');
        //$('#map').append('<input type="button" id="changeViewMode" onclick="changeViewMode()" value="切换" style="right: 31px;height: 33px;width: 39px;top: 122px;z-index: 10000;background-color: #ffffff;border: none;border-radius: 3px;outline: none;position:absolute" />');
        MapCenter = new TMap.LatLng(35, 106);//设置中心点坐标
        // 初始化地图
        var map = new TMap.Map('map', {
            zoom: 5.2, // 设置地图缩放级别
            //pitch: 30,
            center: MapCenter, // 设置地图中心点坐标
            scaleControl: false, // 隐藏比例尺 
            // mapStyleId: "style1", //个性化样式
            //baseMap: {
            //    type: 'vector',
            //    features: ['base', 'building2d'], // 隐藏矢量文字
            //},
        });
        map.removeControl(TMap.constants.DEFAULT_CONTROL_ID.ZOOM);
        $(function () {
            MarkerGeometries = []
            var dataList = [];
            //获取小程序客户的经纬度地址
            $.ajax({
                url: "reportApi.aspx?method=QueryWxLongitudeAndLatitude", async: false,
                success: function (text) {
                    var list = JSON.parse(text)
                   
                    for (var i = 0; i < list.length; i++) {
                        if (HouseList.filter(a => a.HouseID == list[i].HouseID).length == 0 && list[i].Lng != '' && list[i].Lat !='')
                            HouseList.push({
                                HouseID: list[i].HouseID,
                                HouseName: list[i].HouseName,
                                Lng: list[i].Lng,
                                Lat: list[i].Lat,
                            })

                        dataList.push({
                            styleId: 'small', // 对应中的样式id
                            position: new TMap.LatLng(parseFloat(list[i].Latitude), parseFloat(list[i].Longitude) ), // 标注点位置
                            content: list[i].AcceptCompany, // 标注点文本
                            properties: {
                                title: list[i].HouseName,
                                type: 'branch'
                            },
                        },)
                    }
                    HouseList = HouseList.sort(function (a, b) {
                        return parseInt(b.HouseID) - parseInt(a.HouseID)
                    })
                    for (var i = 0; i < HouseList.length; i++) {
                        dataList.push({
                            styleId: 'big', // 对应中的样式id
                            position: new TMap.LatLng(parseFloat(HouseList[i].Lat), parseFloat(HouseList[i].Lng)), // 标注点位置
                            content: HouseList[i].HouseName, // 标注点文本
                            properties: {
                                title: HouseList[i].HouseName,
                                type: 'main'
                            },
                        },)
                    }
                }
            });
            var htmlStr = '';
            for (var i = 0; i < HouseList.length; i++) {
                var item = HouseList[i];
                htmlStr += `
                 <label for="hz">
                     <input type="radio" id="ID_${item.HouseID}" value="${item.HouseID}" name="Radiotype" >${item.HouseName}</input>
                     
                 </label>
                `
            }
            $("#btnBox").append(htmlStr)
            //dataList.length = 10;
            MarkerGeometries = [...MarkerGeometries, ...dataList]
            console.log(MarkerGeometries)
            // marker
            marker = new TMap.MultiMarker({
                map: map, // 显示Marker图层的底图
                styles: {
                    small: new TMap.MarkerStyle({
                        // 点标注的相关样式
                        width: 23, // 宽度
                        height: 23, // 高度
                        anchor: { x: 17, y: 23 }, // 标注点图片的锚点位置
                        src: '../CSS/image/MapMarker1.svg', // 标注点图片url或base64地址
                        color: '#333', // 标注点文本颜色
                        size: 16, // 标注点文本文字大小
                        direction: 'bottom', // 标注点文本文字相对于标注点图片的方位
                        offset: { x: 0, y: 8 }, // 标注点文本文字基于direction方位的偏移属性
                        strokeColor: '#fff', // 标注点文本描边颜色
                        strokeWidth: 2, // 标注点文本描边宽度
                    }),
                    big: new TMap.MarkerStyle({
                        width: 40,
                        height: 40,
                        anchor: { x: 36, y: 32 },
                        src: '../CSS/image/homemade1.svg',
                        color: '#333',
                        size: 22,
                        direction: 'bottom',
                        strokeColor: '#fff',
                        offset: { x: 0, y: 10 },
                        strokeWidth: 2,
                    }),
                },
                //enableCollision: true, // 开启碰撞  开启后缩放会让标记合并
                geometries: MarkerGeometries,
            });

   

            marker.on("click", eventClick)
            // 点击标记放大 
            //qq.maps.event.addListener(marker, 'click', () => {
            //    map.zoomTo(18);  // 平滑放大
            //    map.panTo(marker.getPosition());  // 居中显示 
            //});
            //初始化
            var bounds = new TMap.LatLngBounds();

            // 创建自定义图层时过滤景点类型 
            const poiLayer = new TMap.PoiLayer({
                map: map,
                styles: {
                    // 隐藏特定类型的POI（如景点）
                    hideTypes: ["scenic", "entertainment"]
                }
            });

            //判断标注点是否在范围内
            MarkerGeometries.forEach(function (item) {
                //若坐标点不在范围内，扩大bounds范围
                if (bounds.isEmpty() || !bounds.contains(item.position)) {
                    bounds.extend(item.position);
                }
            })
            //设置地图可视范围
            map.fitBounds(bounds, {
                padding: 100 // 自适应边距
            });

            // 通过地图样式配置 
            map.setStyle("style://sketchy", {
                features: ["point", "land", "water", "road"],
                hidePoi: true  // 隐藏POI文字标注 
            });


            $('input:radio[name="Radiotype"]').click(function () {
                var thisVal=$(this).val()
                var data = HouseList.filter(a => a.HouseID == thisVal)[0]
                setMapCenter({ lat: data.Lat, lng: data.Lng }); // 调用中心点设置函数
            });
        });
        // 模拟双击事件
        let lastClickTime = 0;
        //监听回调函数（非匿名函数）
        var eventClick = function (evt) {
            //alert(evt.geometry.id);
            //markerID.innerHTML = "markerID:" + evt.geometry.id;
            //position.innerHTML = "当前marker位置：" + evt.geometry.position.toString();
            if (evt.geometry.properties.type == "main") {
                map.setBaseMap({
                    type: 'vector',
                    features: ['base', 'building2d', 'point'/*, 'label'*/],
                });
                map.easeTo({ zoom: 9, rotation: 0, center: evt.geometry.position }, { duration: 2000 });//平滑缩放,旋转到指定级别

                // 创建点聚合实例
                markerCluster = new TMap.MarkerCluster({
                    id: 'cluster',
                    map: map,
                    enableDefaultStyle: true, // 启用默认样式
                    minimumClusterSize: 2, // 形成聚合簇的最小个数
                    geometries: [{ // 点数组
                        position: new TMap.LatLng(23.157013, 113.518508)
                    },
                    {
                        position: new TMap.LatLng(23.357013, 113.418508)
                    },
                    {
                        position: new TMap.LatLng(23.457013, 113.318508)
                    },
                    {
                        position: new TMap.LatLng(23.557013, 113.218508)
                    },
                    {
                        position: new TMap.LatLng(23.657013, 113.118508)
                    },
                    {
                        position: new TMap.LatLng(23.757013, 113.018508)
                    },
                    {
                        position: new TMap.LatLng(23.857013, 113.098508)
                    },
                    {
                        position: new TMap.LatLng(23.957013, 113.088508)
                    },
                    ],
                    zoomOnClick: true, // 点击簇时放大至簇内点分离
                    gridSize: 60, // 聚合算法的可聚合距离
                    averageCenter: false, // 每个聚和簇的中心是否应该是聚类中所有标记的平均值
                    maxZoom: 10 // 采用聚合策略的最大缩放级别
                });
            }
            else {
                //infoWindow.close();//初始关闭信息窗关闭
                ////初始化infoWindow
                //infoWindow = new TMap.InfoWindow({
                //    map: map,
                //    position: new TMap.LatLng(39.984104, 116.307503),
                //    offset: { x: -10, y: -32 } //设置信息窗相对position偏移像素
                //});
                //infoWindow.open(); //打开信息窗
                //infoWindow.setPosition(evt.geometry.position);//设置信息窗位置
                //infoWindow.setContent(evt.geometry.properties.title.toString());//设置信息窗内容
            }

            setMapCenter(evt.geometry.position); // 调用中心点设置函数

   

        }
        function setMapCenter(position) {
            map.setCenter(new TMap.LatLng(position.lat, position.lng));
            map.zoomTo(12);
            //setTimeout(a => {
            //    map.setCenter(new TMap.LatLng(position.lat, position.lng));
            //}, 2300)
            // 可选：添加标记点标识点击位置 
            //new TMap.Marker({
            //    map: map,
            //    position: position
            //});
        }
        //初始化infoWindow
        infoWindow = new TMap.InfoWindow({
            map: map,
            position: new TMap.LatLng(39.984104, 116.307503),
            offset: { x: -10, y: -32 } //设置信息窗相对position偏移像素
        });
        infoWindow.close();//初始关闭信息窗关闭

        ////拼接标记内容
        //var markerArr = [{
        //    "id": "marker1",
        //    "styleId": 'marker',
        //    "position": new TMap.LatLng(39.954104, 116.457503),
        //    //'content': "123", //标注文本
        //    "properties": {
        //        "title": "marker1"
        //    }
        //}, {
        //    "id": "marker2",
        //    "styleId": 'marker',
        //    "position": new TMap.LatLng(39.794104, 116.287503),
        //    //'content': "123", //标注文本
        //    "properties": {
        //        "title": "marker2"
        //    }
        //}, {
        //    "id": "marker3",
        //    "styleId": 'marker',
        //    "position": new TMap.LatLng(39.984104, 116.307503),
        //    //'content': "123", //标注文本
        //    "properties": {
        //        "title": "marker3"
        //    }
        //}

        //];
        ////初始化标记
        //var marker = new TMap.MultiMarker({
        //    id: 'marker-layer',
        //    map: map,
        //    styles: {
        //        "marker": new TMap.MarkerStyle({
        //            "width": 25,
        //            "height": 35,
        //            "anchor": { x: 16, y: 32 },
        //            "src": 'https://mapapi.qq.com/web/lbs/javascriptGL/demo/img/markerDefault.png'
        //        })
        //    },
        //    geometries: markerArr
        //});
        //// 拼接label显示内容
        //var labelGeometries = [{
        //    'id': 'label', //点图形数据的标志信息
        //    'styleId': 'label', //样式id
        //    'position': new TMap.LatLng(39.954104, 116.457503), //标注点位置
        //    'content': "123", //标注文本
        //    'properties': { //标注点的属性数据
        //        'title': 'label'
        //    }
        //}]
        //// 初始化label
        //var label = new TMap.MultiLabel({
        //    id: 'label-layer',
        //    map: map,
        //    styles: {
        //        label: new TMap.LabelStyle({
        //            color: '#3777FF', // 颜色属性
        //            size: 20, // 文字大小属性
        //            offset: { x: -5, y: -40 }, // 文字偏移属性单位为像素
        //            angle: 0, // 文字旋转属性
        //            alignment: 'center', // 文字水平对齐属性
        //            verticalAlignment: 'middle', // 文字垂直对齐属性
        //        }),
        //    },
        //    geometries: labelGeometries,
        //});

        ////初始化
        //var bounds = new TMap.LatLngBounds();

        ////判断标注点是否在范围内
        //markerArr.forEach(function (item) {
        //    //若坐标点不在范围内，扩大bounds范围
        //    if (bounds.isEmpty() || !bounds.contains(item.position)) {
        //        bounds.extend(item.position);
        //    }
        //})
        ////设置地图可视范围
        //map.fitBounds(bounds, {
        //    padding: 100 // 自适应边距
        //});


        ////初始化地图
        //var map = new TMap.Map("map", {
        //    zoom: 5,//设置地图缩放级别
        //    center: center//设置地图中心点坐标
        //});
        //var center = new TMap.LatLng(23.257013, 113.318508);
        //// MultiMarker文档地址：https://lbs.qq.com/webApi/javascriptGL/glDoc/glDocMarker
        //var marker = new TMap.MultiMarker({
        //    map: map,
        //    styles: {
        //        // 点标记样式
        //        marker: new TMap.MarkerStyle({
        //            width: 20, // 样式宽
        //            height: 30, // 样式高
        //            anchor: { x: 10, y: 30 }, // 描点位置
        //            src: https://mapapi.qq.com/web/lbs/javascriptGL/demo/img/markerDefault.png
        //        }),
        //    },
        //    geometries: [
        //        // 点标记数据数组
        //        {
        //            // 标记位置(纬度，经度，高度)
        //            position: center,
        //            id: 'marker',
        //        },
        //    ],
        //});
        //// 初始化label
        //var label = new TMap.MultiLabel({
        //    id: 'label-layer',
        //    map: map,
        //    styles: {
        //        label: new TMap.LabelStyle({
        //            color: '#3777FF', // 颜色属性
        //            size: 20, // 文字大小属性
        //            offset: { x: 0, y: -10 }, // 文字偏移属性单位为像素
        //            angle: 0, // 文字旋转属性
        //            alignment: 'center', // 文字水平对齐属性
        //            verticalAlignment: 'middle', // 文字垂直对齐属性
        //        }),
        //    },
        //    geometries: [
        //        {
        //            id: 'label', // 点图形数据的标志信息
        //            styleId: 'label', // 样式id
        //            position: center, // 标注点位置
        //            content: '狄乐汽服', // 标注文本
        //            properties: {
        //                // 标注点的属性数据
        //                title: 'label',
        //            },
        //        },
        //    ],
        //});
    </script>
</asp:Content>
