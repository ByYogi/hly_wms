<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WxDailyReport.aspx.cs" Inherits="Cargo.Weixin.WxDailyReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>迪乐泰微信商城报表统计</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <script src="../JS/easy/js/jquery.min.js"></script>
    <script src="../JS/EChart/echarts.js"></script>
    <style type="text/css">
        .monthTotal {
            width: 100%;
            padding: 0px;
            font-size: 11px;
            text-align: center;
        }

            .monthTotal table {
                width: 100%;
                border: 1px solid #ccc;
                padding: 0px;
            }

                .monthTotal table td {
                    border-color: #ccc;
                    border-style: dotted;
                    border-width: 0 1px 1px 0;
                }
    </style>
    <script type="text/javascript">
        var categories = ['湖南仓库', '湖北仓库', '海南仓库', '广州仓库', '西北仓库', '梅州仓库', '揭阳仓库'];
        var numList = [];//客户数
        var orderList = [];//订单数
        var moneyList = [];//轮胎数
        var chargeList = [];//销售总金额
        var chargeBar = [];//总金额饼图
        var charTitle, chargeTitle, barTitle;
        var mStatisxAxis = [];//月统计的xAxis
        var mStatisTyre = [];//月统计销售轮胎数
        var mStatisCharge = [];//月统计销售总金额
        var wStatisxAxis = [];//周统计的xAxis
        var wStatisTyre = [];//周统计销售轮胎数
        var wStatisCharge = [];//周统计销售总金额
        $(document).ready(function () {
            var nowDate = new Date();
            var m = Number(nowDate.getMonth() + 1);
            $.ajax({
                url: 'myAPI.aspx?method=QueryWeixinMalReportData',
                type: 'post', dataType: 'json', data: { StartDate: nowDate.getFullYear() + '-' + m + "-01", EndDate: nowDate.getFullYear() + '-' + m + "-" + nowDate.getDate() },
                success: function (data) {
                    var cdate;
                    var dat = new Date();
                    var cprofit = "<table><tr><td colspan=5 style='text-align:left; font-Size:18px;font-Family: 微软雅黑; font-Weight: bolder;color:red'>" + dat.getFullYear() + "年" + (dat.getMonth() + 1) + "月销售情况表</td></tr>";
                    for (var i = 0; i < data.footer.length; i++) {
                        numList.push(data.footer[i].HNOrderClientNum);
                        numList.push(data.footer[i].HBOrderClientNum);
                        numList.push(data.footer[i].HAOrderClientNum);
                        numList.push(data.footer[i].GZOrderClientNum);
                        numList.push(data.footer[i].XBOrderClientNum);
                        numList.push(data.footer[i].MZOrderClientNum);
                        numList.push(data.footer[i].JYOrderClientNum);
                        orderList.push(data.footer[i].HNOrderNum);
                        orderList.push(data.footer[i].HBOrderNum);
                        orderList.push(data.footer[i].HAOrderNum);
                        orderList.push(data.footer[i].GZOrderNum);
                        orderList.push(data.footer[i].XBOrderNum);
                        orderList.push(data.footer[i].MZOrderNum);
                        orderList.push(data.footer[i].JYOrderNum);
                        moneyList.push(data.footer[i].HNSaleTyreNum);
                        moneyList.push(data.footer[i].HBSaleTyreNum);
                        moneyList.push(data.footer[i].HASaleTyreNum);
                        moneyList.push(data.footer[i].GZSaleTyreNum);
                        moneyList.push(data.footer[i].XBSaleTyreNum);
                        moneyList.push(data.footer[i].MZSaleTyreNum);
                        moneyList.push(data.footer[i].JYSaleTyreNum);
                        chargeList.push(data.footer[i].HNSaleTotalCharge);
                        chargeList.push(data.footer[i].HBSaleTotalCharge);
                        chargeList.push(data.footer[i].HASaleTotalCharge);
                        chargeList.push(data.footer[i].GZSaleTotalCharge);
                        chargeList.push(data.footer[i].XBSaleTotalCharge);
                        chargeList.push(data.footer[i].MZSaleTotalCharge);
                        chargeList.push(data.footer[i].JYSaleTotalCharge);
                        var total = Number(data.footer[i].HNSaleTotalCharge + data.footer[i].HBSaleTotalCharge + data.footer[i].HASaleTotalCharge + data.footer[i].GZSaleTotalCharge + data.footer[i].XBSaleTotalCharge + data.footer[i].MZSaleTotalCharge + data.footer[i].JYSaleTotalCharge);
                        chargeBar.push({ value: data.footer[i].HNSaleTotalCharge, name: '湖南仓库' });
                        chargeBar.push({ value: data.footer[i].HBSaleTotalCharge, name: '湖北仓库' });
                        chargeBar.push({ value: data.footer[i].HASaleTotalCharge, name: '海南仓库' });
                        chargeBar.push({ value: data.footer[i].GZSaleTotalCharge, name: '广州仓库' });
                        chargeBar.push({ value: data.footer[i].XBSaleTotalCharge, name: '西北仓库' });
                        chargeBar.push({ value: data.footer[i].MZSaleTotalCharge, name: '梅州仓库' });
                        chargeBar.push({ value: data.footer[i].JYSaleTotalCharge, name: '揭阳仓库' });
                        cprofit += "<tr style='font-weight:bold;'><td></td><td>客户数</td><td>订单数</td><td>轮胎数</td><td>总金额(元)</td></tr>";
                        cprofit += "<tr><td>湖南仓库</td><td>" + data.footer[i].HNOrderClientNum + "</td><td>" + data.footer[i].HNOrderNum + "</td><td>" + data.footer[i].HNSaleTyreNum + "</td><td>" + data.footer[i].HNSaleTotalCharge + "</td></tr>";
                        cprofit += "<tr><td>湖北仓库</td><td>" + data.footer[i].HBOrderClientNum + "</td><td>" + data.footer[i].HBOrderNum + "</td><td>" + data.footer[i].HBSaleTyreNum + "</td><td>" + data.footer[i].HBSaleTotalCharge + "</td></tr>";
                        cprofit += "<tr><td>海南仓库</td><td>" + data.footer[i].HAOrderClientNum + "</td><td>" + data.footer[i].HAOrderNum + "</td><td>" + data.footer[i].HASaleTyreNum + "</td><td>" + data.footer[i].HASaleTotalCharge + "</td></tr>";
                        cprofit += "<tr><td>广州仓库</td><td>" + data.footer[i].GZOrderClientNum + "</td><td>" + data.footer[i].GZOrderNum + "</td><td>" + data.footer[i].GZSaleTyreNum + "</td><td>" + data.footer[i].GZSaleTotalCharge + "</td></tr>";
                        cprofit += "<tr><td>西北仓库</td><td>" + data.footer[i].XBOrderClientNum + "</td><td>" + data.footer[i].XBOrderNum + "</td><td>" + data.footer[i].XBSaleTyreNum + "</td><td>" + data.footer[i].XBSaleTotalCharge + "</td></tr>";
                        cprofit += "<tr><td>梅州仓库</td><td>" + data.footer[i].MZOrderClientNum + "</td><td>" + data.footer[i].MZOrderNum + "</td><td>" + data.footer[i].MZSaleTyreNum + "</td><td>" + data.footer[i].MZSaleTotalCharge + "</td></tr>";
                        cprofit += "<tr><td>揭阳仓库</td><td>" + data.footer[i].JYOrderClientNum + "</td><td>" + data.footer[i].JYOrderNum + "</td><td>" + data.footer[i].JYSaleTyreNum + "</td><td>" + data.footer[i].JYSaleTotalCharge + "</td></tr>";
                        cprofit += "<tr style='font-weight:bold;color:red;'><td>汇总：</td><td>" + data.footer[i].DayOrderClientNum + "</td><td>" + data.footer[i].DayOrderNum + "</td><td>" + data.footer[i].DaySaleTyreNum + "</td><td>" + data.footer[i].DaySaleTotalCharge + "</td></tr>";
                        cprofit += "</table>";
                    }
                    $('#monthTotal').html(cprofit)
                    charTitle = dat.getFullYear() + "年" + (dat.getMonth() + 1) + "月各仓库销售轮胎报表";
                    chargeTitle = dat.getFullYear() + "年" + (dat.getMonth() + 1) + "月各仓库销售金额报表";
                    barTitle = dat.getFullYear() + "年" + (dat.getMonth() + 1) + "月各仓库销售金额比例报表";
                    //categories.reverse();
                    //orderList.reverse();
                    //numList.reverse();
                    //moneyList.reverse();
                    setChart();
                }
            });
            //月周统计
            $.ajax({
                url: 'myAPI.aspx?method=QueryWeixinMallStatisData',
                type: 'post', dataType: 'json', data: {  },
                success: function (data) {
                    for (var i = 0; i < data.monthStatis.length; i++) {
                        mStatisxAxis.push(data.monthStatis[i].ReportDate);
                        mStatisTyre.push(data.monthStatis[i].DaySaleTyreNum);
                        mStatisCharge.push(data.monthStatis[i].DaySaleTotalCharge);
                    }
                    for (var i = 0; i < data.weekStatis.length; i++) {
                        wStatisxAxis.push(data.weekStatis[i].ReportDate);
                        wStatisTyre.push(data.weekStatis[i].DaySaleTyreNum);
                        wStatisCharge.push(data.weekStatis[i].DaySaleTotalCharge);
                    }
                    setMonthChart();
                }
            });
        });

        function setMonthChart() {
            require(['echarts', 'echarts/chart/bar', 'echarts/chart/line'],
             function (ec) {
                 var monthStatisChart = ec.init(document.getElementById('monthStatisChart'));
                 monthStatisChart.setOption({
                     tooltip: { show: true, trigger: 'axis', axisPointer: { type: 'cross' } },
                     title: {
                         show: true, text: '月销售数据统计图', x: 'left',
                         textStyle: { fontSize: '18', fontFamily: '微软雅黑', fontWeight: 'bolder', color: 'red' }
                     },
                     legend: { show: true, x: 'center', y: 'bottom', data: ['销售轮胎数', '销售总金额'] },
                     toolbox: {
                         show: true, feature: { restore: { show: true }, saveAsImage: { show: true } }
                     },
                     grid: { x: '50px', y: '50px', x2: '70px', y2: '60px', containLabel: true },
                     calculable: true,
                     xAxis: [{ type: 'category', axisTick: { alignWithLabel: true }, data: mStatisxAxis }],
                     yAxis: [{ type: 'value', name: '销售轮胎数', position: 'left', axisLabel: { formatter: '{value}条' } },
                         {
                             type: 'value', name: '销售总金额', position: 'right', axisLabel: { formatter: '{value}元' }
                         }],
                     series: [{
                         name: '销售轮胎数', type: 'bar', data: mStatisTyre,
                         itemStyle: { normal: { label: { show: true, position: 'top' } } }
                     }, {
                         name: '销售总金额', type: 'line', yAxisIndex: 1, data: mStatisCharge,
                         itemStyle: { normal: { label: { show: true, position: 'bottom' } } }
                     }]
                 });
                 //周统计聚合图
                 var WeekStatisChart = ec.init(document.getElementById('WeekStatisChart'));
                 WeekStatisChart.setOption({
                     tooltip: { show: true, trigger: 'axis', axisPointer: { type: 'cross' } },
                     title: {
                         show: true, text: '周销售数据统计图', x: 'left',
                         textStyle: { fontSize: '18', fontFamily: '微软雅黑', fontWeight: 'bolder', color: 'red' }
                     },
                     legend: { show: true, x: 'center', y: 'bottom', data: ['销售轮胎数', '销售总金额'] },
                     toolbox: {
                         show: true, feature: { restore: { show: true }, saveAsImage: { show: true } }
                     },
                     grid: { x: '50px', y: '50px', x2: '70px', y2: '60px', containLabel: true },
                     calculable: true,
                     xAxis: [{ type: 'category', axisTick: { alignWithLabel: true }, data: wStatisxAxis }],
                     yAxis: [{ type: 'value', name: '销售轮胎数', position: 'left', axisLabel: { formatter: '{value}条' } },
                         {
                             type: 'value', name: '销售总金额', position: 'right', axisLabel: { formatter: '{value}元' }
                         }],
                     series: [{
                         name: '销售轮胎数', type: 'bar', data: wStatisTyre,
                         itemStyle: { normal: { label: { show: true, position: 'top' } } }
                     }, {
                         name: '销售总金额', type: 'line', yAxisIndex: 1, data: wStatisCharge,
                         itemStyle: { normal: { label: { show: true, position: 'bottom' } } }
                     }]
                 });
             });
        }

        function setChart() {
            require(['echarts', 'echarts/chart/bar', 'echarts/chart/line', 'echarts/chart/pie'],
            function (ec) {
                //金额柱状图
                var chargeChart = ec.init(document.getElementById('chargeChart'));
                chargeChart.setOption({
                    tooltip: { show: true, trigger: 'axis' },
                    title: {
                        show: true, text: chargeTitle, x: 'left',
                        textStyle: { fontSize: '18', fontFamily: '微软雅黑', fontWeight: 'bolder', color: 'red' }
                    },
                    legend: { show: true, x: 'center', y: 'bottom', data: ['销售总金额'] },
                    toolbox: {
                        show: true, feature: {
                            //mark: { show: true },
                            //dataView: { show: true, readOnly: false },
                            magicType: { show: true, type: ['line', 'bar'] },
                            //restore: { show: true },
                            //saveAsImage: { show: true }
                        }
                    },
                    grid: { x: '0px', y: '50px', x2: '1px', y2: '60px', containLabel: true },
                    calculable: true,
                    xAxis: [{ type: 'category', data: categories }],
                    yAxis: [{ type: 'value' }],
                    series: [
                    {
                        name: '销售总金额', type: 'bar', data: chargeList,
                        itemStyle: { normal: { label: { show: true, position: 'top' } } }
                    }
                    ]
                });
                //图表显示提示信息
                //myChart.showLoading({ text: "图表数据正在努力加载中..." });
                var myChart = ec.init(document.getElementById('tryeChart'));
                myChart.setOption({
                    tooltip: { show: true, trigger: 'axis' },
                    title: {
                        show: true, text: charTitle, x: 'left',
                        textStyle: { fontSize: '18', fontFamily: '微软雅黑', fontWeight: 'bolder', color: 'red' }
                    },
                    legend: { show: true, x: 'center', y: 'bottom', data: ['客户数', '订单数', '轮胎数'] },
                    toolbox: {
                        show: true, feature: {
                            //mark: { show: true },
                            //dataView: { show: true, readOnly: false },
                            magicType: { show: true, type: ['line', 'bar'] },
                            //restore: { show: true },
                            //saveAsImage: { show: true }
                        }
                    },
                    grid: { x: '0px', y: '50px', x2: '1px', y2: '60px', containLabel: true },
                    calculable: true,
                    xAxis: [{ type: 'category', data: categories }],
                    yAxis: [{ type: 'value' }],
                    series: [
                    {
                        name: '客户数', type: 'bar', data: numList,
                        itemStyle: { normal: { label: { show: true, position: 'top' } } }
                    },
                    {
                        name: '订单数', type: 'bar', data: orderList,
                        itemStyle: {
                            normal: {
                                label: {
                                    show: true, //开启显示
                                    position: 'top', //在上方显示
                                    //textStyle: { //数值样式
                                    //    color: 'black',
                                    //    fontSize: 14
                                    //}
                                }
                            }
                        }
                    },
                    {
                        name: '轮胎数', type: 'bar', data: moneyList,
                        itemStyle: {
                            normal: {
                                label: {
                                    show: true, //开启显示
                                    position: 'top', //在上方显示
                                    //textStyle: { //数值样式
                                    //    color: 'black',
                                    //    fontSize: 14
                                    //}
                                }
                            }
                        }
                    }
                    ]
                });
                //金额饼图
                var housePie = ec.init(document.getElementById('housePie'));
                housePie.setOption({
                    tooltip: { show: true, trigger: 'item', formatter: '{a} <br/>{b} : {c} ({d}%)' },
                    title: {
                        show: true, text: barTitle, x: 'left',
                        textStyle: { fontSize: '18', fontFamily: '微软雅黑', fontWeight: 'bolder', color: 'red' }
                    },
                    legend: { show: true, x: 'left', y: 'bottom', data: categories },
                    series: [
                    {
                        name: '仓库', type: 'pie', radius: '60%', center: ['50%', '50%'], data: chargeBar,
                        itemStyle: {
                            normal: {
                                label: {
                                    show: true,
                                    formatter: '{d}%'
                                },
                                labelLine: { show: true }
                            }
                        }
                    }
                    ]
                });

            })
        }
    </script>
</head>
<body>
    <div id="monthTotal" class="monthTotal"></div>
    <div id="tryeChart" style="height: 300px; border: 0px solid #ccc; width: 100%; margin: 0px; padding: 0px;"></div>
    <div id="chargeChart" style="height: 300px; border: 0px solid #ccc; width: 100%; margin: 0px; padding: 0px;"></div>
    <div id="housePie" style="height: 300px; border: 0px solid #ccc; width: 100%; margin: 0px; padding: 0px;"></div>
    <div id="monthStatis" class="monthTotal"></div>
    <div id="WeekStatisChart" style="height: 300px; border: 0px solid #ccc; width: 100%; margin: 0px; padding: 0px;"></div>
    <div id="monthStatisChart" style="height: 300px; border: 0px solid #ccc; width: 100%; margin: 0px; padding: 0px;"></div>
    <script type="text/javascript">
        require.config({
            paths: {
                echarts: '../JS/EChart'
            }
        });
    </script>
</body>
</html>
