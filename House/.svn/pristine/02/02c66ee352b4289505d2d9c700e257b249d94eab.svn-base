<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BluetoothTest.aspx.cs" Inherits="Cargo.QY.BluetoothTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>蓝牙测试</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <link href="../Weixin/WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/style.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/jquery-weui.css" rel="stylesheet" />
    <link href="../Weixin/WeUI/CSS/weuix.min.css" rel="stylesheet" />
    <script src="JS/jweixin-1.2.0.js"></script>
</head>
<body>
    <audio id="chatAudio">
        <source src="../upload/audio/success.mp3" type="audio/mpeg" />
    </audio>
    <audio id="failAudio">
        <source src="../upload/audio/fail.mp3" type="audio/mpeg" />
    </audio>
    <audio id="InBoundSuc">
        <source src="../upload/audio/InBoundSuc.mp3" type="audio/mpeg" />
    </audio>
    <audio id="InBoundFail">
        <source src="../upload/audio/InBoundFail.mp3" type="audio/mpeg" />
    </audio>
    <audio id="scanQrCode">
        <source src="../upload/audio/scanQrCode.mp3" type="audio/mpeg" />
    </audio>
    <audio id="AV0">
        <source src="../upload/audio/0.mp3" type="audio/mpeg" />
    </audio>
    <audio id="AV1">
        <source src="../upload/audio/1.mp3" type="audio/mpeg" />
    </audio>
    <audio id="AV2">
        <source src="../upload/audio/2.mp3" type="audio/mpeg" />
    </audio>
    <audio id="AV3">
        <source src="../upload/audio/3.mp3" type="audio/mpeg" />
    </audio>
    <audio id="AV4">
        <source src="../upload/audio/4.mp3" type="audio/mpeg" />
    </audio>
    <audio id="AV5">
        <source src="../upload/audio/5.mp3" type="audio/mpeg" />
    </audio>
    <audio id="AV6">
        <source src="../upload/audio/6.mp3" type="audio/mpeg" />
    </audio>
    <audio id="AV7">
        <source src="../upload/audio/7.mp3" type="audio/mpeg" />
    </audio>
    <audio id="AV8">
        <source src="../upload/audio/8.mp3" type="audio/mpeg" />
    </audio>
    <audio id="AV9">
        <source src="../upload/audio/9.mp3" type="audio/mpeg" />
    </audio>
    <div style="text-align: center">
        <button onclick="openadapter()" style="width: 40%; height: 50px">打开适配器</button>
        <button onclick="closeadapter()" style="width: 40%; height: 50px">关闭适配器</button>
        <button onclick="opendiscovery()" style="width: 40%; height: 50px">开始搜索</button>
        <button onclick="closediscovery()" style="width: 40%; height: 50px">关闭搜索</button>
        <button onclick="getdevice()" style="width: 40%; height: 50px">获取设备</button>
        <button onclick="getconnecteddevice()" style="width: 40%; height: 50px">获取已连接设备</button>
        <button onclick="connecteddevice()" style="width: 40%; height: 50px">连接我的设备</button>
        <button onclick="getservice()" style="width: 40%; height: 50px">获取服务</button>
        <button onclick="getcharacteristics()" style="width: 40%; height: 50px">获取特征值</button>
        <button onclick="startread()" style="width: 40%; height: 50px">读取值</button>
        <button onclick="startnotify()" style="width: 40%; height: 50px">开启notify</button>
        <button onclick="startwrite()" style="width: 40%; height: 50px">写数据</button>
        <%--        <a href="javascript:openadapter();" class="weui-btn bg-green" style="width: 40%;">打开适配器</a>
        <a href="javascript:closeadapter();" class="weui-btn bg-green" style="width: 40%;">关闭适配器</a>
        <a href="javascript:opendiscovery();" class="weui-btn bg-green" style="width: 40%;">开始搜索</a>
        <a href="javascript:closediscovery();" class="weui-btn bg-green" style="width: 40%;">关闭搜索</a>
        <a href="javascript:getdevice();" class="weui-btn bg-green" style="width: 40%;">获取设备</a>
        <a href="javascript:getconnecteddevice();" class="weui-btn bg-green" style="width: 40%;">获取已连接设备</a>
        <a href="javascript:connecteddevice();" class="weui-btn bg-green" style="width: 40%;">连接我的设备</a>
        <a href="javascript:getservice();" class="weui-btn bg-green" style="width: 40%;">获取服务</a>
        <a href="javascript:getcharacteristics();" class="weui-btn bg-green" style="width: 40%;">获取特征值</a>
        <a href="javascript:startread();" class="weui-btn bg-green" style="width: 40%;">读取值</a>
        <a href="javascript:startnotify();" class="weui-btn bg-green" style="width: 40%;">开启notify</a>
        <a href="javascript:startwrite();" class="weui-btn bg-green" style="width: 40%;">写数据</a>--%>
    </div>
    <div id="log">
        <p>输出</p>
    </div>

    <script src="../Weixin/WeUI/JS/jquery-2.1.4.js"></script>
    <script src="../Weixin/WeUI/JS/fastclick.js"></script>
    <script src="../Weixin/WeUI/JS/jquery-weui.js"></script>
    <script type="text/javascript">
        $(function () {
            var res = "0521";
            $('#InBoundSuc')[0].play();
            var B1 = Number(res.substring(0, 1));
            var B2 = Number(res.substring(1, 2));
            var B3 = Number(res.substring(2, 3));
            var B4 = Number(res.substring(3, 4));
            setTimeout(function () {
                if (B1 == 0) { $('#AV0')[0].play(); }
                if (B1 == 1) { $('#AV1')[0].play(); }
                if (B1 == 2) { $('#AV2')[0].play(); }
                if (B1 == 3) { $('#AV3')[0].play(); }
                if (B1 == 4) { $('#AV4')[0].play(); }
                if (B1 == 5) { $('#AV5')[0].play(); }
                if (B1 == 6) { $('#AV6')[0].play(); }
                if (B1 == 7) { $('#AV7')[0].play(); }
                if (B1 == 8) { $('#AV8')[0].play(); }
                if (B1 == 9) { $('#AV9')[0].play(); }
            }, 1500);

            setTimeout(function () {
                if (B2 == 0) { $('#AV0')[0].play(); }
                if (B2 == 1) { $('#AV1')[0].play(); }
                if (B2 == 2) { $('#AV2')[0].play(); }
                if (B2 == 3) { $('#AV3')[0].play(); }
                if (B2 == 4) { $('#AV4')[0].play(); }
                if (B2 == 5) { $('#AV5')[0].play(); }
                if (B2 == 6) { $('#AV6')[0].play(); }
                if (B2 == 7) { $('#AV7')[0].play(); }
                if (B2 == 8) { $('#AV8')[0].play(); }
                if (B2 == 9) { $('#AV9')[0].play(); }
            }, 1900);
            setTimeout(function () {
                $('#AV2')[0].play();
            }, 2300);
            setTimeout(function () {
                if (B4 == 0) { $('#AV0')[0].play(); }
                if (B4 == 1) { $('#AV1')[0].play(); }
                if (B4 == 2) { $('#AV2')[0].play(); }
                if (B4 == 3) { $('#AV3')[0].play(); }
            }, 2700);
          
            //configjssdk();
        });

        function configjssdk() {
            var weixinUrl = location.href.split('#')[0];//;
            //配置jssdk
            $.ajax({
                type: "post",
                url: "qyServices.aspx?method=configJssdk",
                dataType: "json",
                data: { Url: weixinUrl },
                cache: false,
                ifModified: true,
                async: false,
                success: function (msg) {
                    var json = eval(msg);
                    var config = {};
                    config.beta = true;
                    config.appId = json.appId;
                    config.nonceStr = json.nonceStr;
                    config.signature = json.signature;
                    config.debug = false;        // 添加你需要的JSSDK的权限
                    config.jsApiList = ['openBluetoothAdapter', 'closeBluetoothAdapter', 'startBluetoothDevicesDiscovery', 'stopBluetoothDevicesDiscovery', 'getBluetoothDevices', 'getConnectedBluetoothDevices', 'getBLEDeviceServices', 'getBLEDeviceCharacteristics', 'readBLECharacteristicValue', 'notifyBLECharacteristicValueChange', 'onBLECharacteristicValueChange', 'writeBLECharacteristicValue'];
                    config.timestamp = parseInt(json.timestamp);
                    wx.config(config);
                    wx.ready(function () {
                        //alert("jssdk配置成功");
                        //console("jssdk配置成功");
                        $("#log").append("<p>jssdk配置成功</p>");
                        //wx.config(config);
                    });
                    wx.error(function (res) {
                        alert(JSON.stringify(res));
                    });
                }
            })
        }

        function openadapter() {
            wx.openBluetoothAdapter({
                success: function (res) {
                    console.log(res, "success")
                    $("#log").append(res + "<p>打开适配器成功</p>");
                },
                fail: function (res) {
                    console.log(res, "fail")
                    $("#log").append(res + "<p>打开适配器失败</p>");
                },
            })
        }
        function closeadapter() {
            wx.closeBluetoothAdapter({
                success: function (res) {
                    console.log(res, "success")
                    $("#log").append(res + "<p>关闭适配器成功</p>");
                },
                fail: function (res) {
                    console.log(res, "fail")
                    $("#log").append(res + "<p>关闭适配器失败</p>");
                },
            })
        }
        function opendiscovery() {
            wx.startBluetoothDevicesDiscovery({
                services: [],
                success: function (res) {
                    console.log(res)
                    $("#log").append(res + "<p>搜索设备成功</p>");
                },

                fail: function (res) {
                    console.log(res, "fail")
                    $("#log").append(res + "<p>搜索设备失败</p>");
                },
            })
        }
        function closediscovery() {
            wx.stopBluetoothDevicesDiscovery({
                success: function (res) {
                    console.log(res)
                    $("#log").append(res + "<p>停止搜索设备成功</p>");
                },
                fail: function (res) {
                    console.log(res, "fail")
                    $("#log").append(res + "<p>停止搜索设备成功</p>");
                },
            })
        }
        function getdevice() {
            //function ab2hex(buffer) {
            //    var hexArr = Array.prototype.map.call(
            //    new Uint8Array(buffer),
            //    function (bit) {
            //        return ('00' + bit.toString(16)).slice(-2)
            //    }
            //    )
            //    return hexArr.join('');
            //}
            wx.getBluetoothDevices({
                success: function (res) {
                    console.log(res)
                    i = 0;
                    while (res.devices[i]) {
                        console.log(i);
                        console.log(res.devices[i].name, res.devices[i].deviceId);
                        //$("#log").append(i);
                        $("#log").append(res.devices[i].name + "|" + res.devices[i].deviceId + "<br />");
                        if (res.devices[i].name == 'Printer_DB80_BLE') {
                            deviceId = res.devices[i].deviceId;
                            console.log(deviceId);
                            $("#log").append(deviceId);
                        }
                        i++;
                    }
                }
            })
        }
        function getconnecteddevice() {
            wx.getConnectedBluetoothDevices({
                success: function (res) {
                    console.log(res)
                    $("#log").append(res + "<p>获取已连接设备成功</p>");
                }
            })
        }
        function connecteddevice() {
            alert(deviceId);
            wx.createBLEConnection({
                deviceId: deviceId,
                success: function (res) {
                    console.log(res);
                    $("#log").append(res + "<p>连接我的设备成功</p>");
                },
                fail: function (res) {
                    $("#log").append(res + "<p>连接我的设备失败</p>");
                },
                complete: function (re) {
                    alert(re);
                }
            })
        }
        function getservice() {
            wx.getBLEDeviceServices({
                deviceId: deviceId,
                success: function (res) {
                    console.log(res.services);
                    $("#log").append(res + "<p>获取服务成功</p>");
                    i = 0;
                    while (res.services[i]) {
                        serviceId[i] = res.services[i].uuid;
                        console.log(serviceId[i]);
                        $("#log").append(serviceId[i]);
                        i++;
                    }
                },
            })
        }
        function getcharacteristics() {
            wx.getBLEDeviceCharacteristics({
                deviceId: deviceId,
                serviceId: serviceId[0],
                success: function (res) {
                    console.log('device getBLEDeviceCharacteristics:', res.characteristics)
                    $("#log").append(res.characteristics + "<p>获取特征值成功</p>");
                }
            })
            wx.getBLEDeviceCharacteristics({
                deviceId: deviceId,
                serviceId: serviceId[1],
                success: function (res) {
                    i = 0;
                    while (res.characteristics[i]) {
                        characteristicId[i] = res.characteristics[i].uuid;
                        console.log(characteristicId[i]);
                        $("#log").append(characteristicId[i]);
                        i++;
                    }
                }
            })
        }
        function startread() {
            wx.readBLECharacteristicValue({
                deviceId: deviceId,
                serviceId: serviceId[1],
                characteristicId: characteristicId[0],
                success: function (res) {
                    console.log('readBLECharacteristicValue:', res.errCode)
                    $("#log").append(res.errCode + "<p>读取值成功</p>");
                }
            })
        }
        function startnotify() {
            wx.notifyBLECharacteristicValueChange({
                state: true,
                deviceId: deviceId,
                serviceId: serviceId[1],
                characteristicId: characteristicId[0],
                success: function (res) {
                    console.log('notifyBLECharacteristicValueChange success', res.errMsg)
                    $("#log").append(res.errMsg + "<p>开启notify成功</p>");
                }
            })
            function ab2hex(buffer) {
                var hexArr = Array.prototype.map.call(
                new Uint8Array(buffer),
                function (bit) {
                    return ('00' + bit.toString(16)).slice(-2)
                }
                )
                return hexArr.join('');
            }
            wx.onBLECharacteristicValueChange(function (res) {
                console.log('characteristic value comed:', ab2hex(res.value))
                $("#log").append(ab2hex(res.value) + "<p>特征值系数改变</p>");
            })
        }
        function startwrite() {
            let buffer = new ArrayBuffer(3)
            let dataView = new DataView(buffer)
            dataView.setUint8(1, 100)
            wx.writeBLECharacteristicValue({
                deviceId: deviceId,
                serviceId: serviceId[1],
                characteristicId: characteristicId[0],
                value: buffer,
                success: function (res) {
                    console.log('writeBLECharacteristicValue success', res.errMsg)
                    $("#log").append(res.errMsg + "<p>写入数据成功</p>");
                }
            })
        }
    </script>
</body>
</html>
