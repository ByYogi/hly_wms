<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wxSupplierDataDisplay.aspx.cs" Inherits="Cargo.Weixin.wxSupplierDataDisplay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
        <script type="text/javascript" src="../JS/vue.js"></script>
     <script src="../JS/easy/js/jquery.min.1.13.js"></script>
    <style type="text/css">
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        body {
            background: linear-gradient(135deg, #6a11cb 0%, #2575fc 100%);
            min-height: 100vh;
            display: flex;
            justify-content: center;
            align-items: flex-start;
            padding: 20px;
        }

        .container {
            max-width: 940px;
            width: 100%;
            padding: 10px;
        }

        header {
            text-align: center;
            margin-bottom: 40px;
            color: white;
        }

            header h1 {
                font-size: 2.5rem;
                margin-bottom: 10px;
                text-shadow: 0 2px 4px rgba(0,0,0,0.2);
            }

            header p {
                font-size: 2.5rem;
                opacity: 0.9;
            }

        .accordion {
            background-color: white;
            border-radius: 12px;
            box-shadow: 0 15px 30px rgba(0, 0, 0, 0.2);
            overflow: hidden;
        }

        .accordion-item {
            border-bottom: 1px solid #eaeaea;
        }

            .accordion-item:last-child {
                border-bottom: none;
            }

        .accordion-header {
            padding: 20px;
            background-color: #f8f9fa;
            cursor: pointer;
            display: flex;
            justify-content: space-between;
            align-items: center;
            transition: all 0.3s ease;
        }

            .accordion-header:hover {
                background-color: #e9ecef;
            }

            .accordion-header.active {
                background-color: #4a69bd;
                color: white;
            }

        .accordion-title {
            font-size: 3.5rem;
            font-weight: 600;
        }

        .accordion-icon {
            font-size: 1.2rem;
            transition: transform 0.3s ease;
        }

        .accordion-header.active .accordion-icon {
            transform: rotate(180deg);
            color: white;
        }

        .accordion-content {
            /*padding: 0 20px;*/
            max-height: 0;
            overflow: hidden;
            transition: max-height 0.3s ease, padding 0.3s ease;
            line-height: 1.6;
            color: #495057;
            background-color: white;
        }

            .accordion-content p {
                padding: 20px 0;
            }

        .accordion-header.active + .accordion-content {
            /*padding: 0 20px;*/
            max-height: 500px;
        }

        .features {
            display: flex;
            flex-wrap: wrap;
            justify-content: space-around;
            margin-top: 40px;
            color: white;
            text-align: center;
        }

        .feature {
            background: rgba(255, 255, 255, 0.1);
            padding: 20px;
            border-radius: 10px;
            width: 200px;
            margin: 10px;
            backdrop-filter: blur(10px);
        }

            .feature i {
                font-size: 2.5rem;
                margin-bottom: 15px;
                color: #ffd700;
            }

            .feature h3 {
                margin-bottom: 10px;
            }

        footer {
            text-align: center;
            margin-top: 40px;
            color: rgba(255, 255, 255, 0.7);
            font-size: 0.9rem;
        }

        @media (max-width: 768px) {
            .features {
                flex-direction: column;
                align-items: center;
            }

            .feature {
                width: 100%;
                max-width: 300px;
            }

            .DataCount {
                color: #00ffaf;
                margin-left: 6px;
            }
        }
    </style>
     <style type="text/css">
         /* 基础样式 */
         :root {
             --primary-color: #42b983;
             --secondary-color: #f7f7f7;
             --border-color: #eee;
             --font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
         }

         body {
             font-family: var(--font-family);
             background-color: #f5f5f5;
             margin: 0;
             padding: 20px;
         }

         .tabs-container {
             /*max-width: 800px;*/
             /*margin: 0 auto;*/
             border-radius: 8px;
             box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
             background-color: #fff;
             overflow: hidden;
         }

         /* 标签栏样式 */
         .tabs {
             display: flex;
             border-bottom: 1px solid var(--border-color);
             background-color: var(--secondary-color);
             font-weight: bold;
             font-size: 29px;
         }

         .tab {
             padding: 15px 20px;
             cursor: pointer;
             user-select: none;
             transition: all 0.3s ease;
             position: relative;
             color: #333;
         }

             .tab::after {
                 content: '';
                 position: absolute;
                 bottom: 0;
                 left: 0;
                 right: 0;
                 height: 2px;
                 transform: scaleX(0);
                 transform-origin: center;
                 background-color: var(--primary-color);
                 transition: transform 0.3s ease;
             }

             .tab:hover {
                 color: var(--primary-color);
             }

             .tab.active {
                 color: var(--primary-color);
                 font-weight: 600;
             }

                 .tab.active::after {
                     transform: scaleX(1);
                 }

         /* 内容区域样式 */
         .tab-content {
             padding: 20px;
             animation: fadeIn 0.5s ease;
         }

         @keyframes fadeIn {
             from {
                 opacity: 0;
             }

             to {
                 opacity: 1;
             }
         }

         /* 响应式适配 */
         @media (max-width: 600px) {
             .tab {
                 padding: 10px 12px;
                 font-size: 14px;
             }

             .tab-content {
                 padding: 16px;
             }
         }

         .table-bodyV th, td {
             width: 100px;
         }

         table {
             table-layout: fixed;
             border-collapse: collapse; /* 消除表格边框间距 */
             width: 100%;
             border: 1px solid #000; /* 外边框 */
         }

         td, th:nth-child(1) {
             width: 60px;
         }

         td, th:nth-child(2) {
             width: 72px;
         }

         td, th:nth-child(3) {
             width: 78px;
         }

         td, th:nth-child(4) {
             width: 46px;
         }

         td, th:nth-child(5) {
             width: 45px;
         }

         td, th:nth-child(6) {
             width: 52px;
         }

         th, td {
             border: 1px solid #000; /* 单元格边框 */
             padding: 5px; /* 单元格内边距（可根据需要调整） */
             text-align: center;
         }

         i.DataCount {
             font-size: 35px;
         }

         i.thCountClass {
             font-size: 35px;
             color: red;
         }

         table.table-bodyV {
             font-weight: bold;
             font-size: 24px;
         }

         .tdHP {
             word-break: break-all;
             word-break: break-word;
             /*font-size: 18px;*/
         }

         .tdAmount {
             font-weight: bold;
             color: red;
             font-size: 3rem;
         }
     </style>

</head>
<body>
      <div class="container" id="app">
       <header>
           <h1>供应商销售数据展示</h1>
           <p v-if="dataList.length<=0">当日无销售数据</p>
       </header>

       <div class="accordion" id="myAccordion">
           <div class="accordion-item" v-for="(item,index) of dataList">
               <div class="accordion-header">
                   <div class="accordion-title">{{item.SettleHouseName}} 
                       <i class="DataCount">销量条数:{{(item.OrderList.length>0?item.OrderList.length:'0')}}条</i>
                       <i class="DataCount">退货条数:{{(item.ReturnOrderList.length>0?item.ReturnOrderList.length:'0')}}条</i>
                   </div>
                   <div class="accordion-icon"><i class="fas fa-chevron-down"></i></div>
               </div>
               <div class="accordion-content">
                  
                        <div class="tabs-container">
                         <!-- 标签栏 -->
                         <div class="tabs" role="tablist">
                           <div 
                             v-for="(tab, TabIndex) in tabs" :key="TabIndex" 
                             :class="['tab', { active: item.activeIndex == TabIndex }]"
                             @click="activateTab(item,TabIndex,index)"
                             role="tab"
                             :aria-selected="item.activeIndex == TabIndex">
                             {{ tab.title  }}
                           </div>
                         </div>
     
                         <!-- 内容区域 -->
                         <div class="tab-content">
                           <div 
                             v-for="(tab, TabIndex) in tabs" 
                             :key="TabIndex" 
                             v-show="item.activeIndex == TabIndex"
                             role="tabpanel"
                             :id="'tab-' + TabIndex"
                             :aria-labelledby="'tab-' + TabIndex"
                           >
                               <div class="table-content">
                                   <table class="table-bodyV">
                                      <thead>
                                           <tr>
                                             <th>品牌</th>
                                             <th>规格</th>
                                             <th>花纹</th>
                                             <th>载速</th>
                                             <th>批次</th>
                                             <%--<th>货品代码</th>--%>
                                             <th>数量 <i class="thCountClass">({{(item.activeIndex == 0?item.OrderCount:item.ReturnOrderCount)}})</i> </th>
                                             <th>产品代码</th>
                                           </tr>
                                         </thead>
                                         <tbody>
                                           <tr v-show="item.activeIndex == 0" v-for="(item2, Index2) in item.OrderList">
                                             <td align="center">{{item2.TypeName}}</td>
                                             <td class="tdHP" align="center">{{item2.Specs}}</td>
                                             <td class="tdHP" align="center">{{item2.Figure}}</td>
                                             <td align="center">{{item2.LoadIndex}}{{item2.SpeedLevel}}</td>
                                             <td align="center">{{item2.Batch}}</td>
                                             <%--<td class="tdHP" align="center">{{item2.GoodsCode}}</td>--%>
                                             <td class="tdAmount" align="center">{{item2.Piece}}</td>
                                             <td class="tdHP" align="center">{{item2.ProductCode}}</td>
                                           </tr>
                                           <tr v-show="item.activeIndex == 1" v-for="(item2, Index2) in item.ReturnOrderList">
                                             <td align="center">{{item2.TypeName}}</td>
                                              <td class="tdHP" align="center">{{item2.Specs}}</td>
                                              <td class="tdHP" align="center">{{item2.Figure}}</td>
                                              <td align="center">{{item2.LoadIndex}}{{item2.SpeedLevel}}</td>
                                              <td align="center">{{item2.Batch}}</td>
                                              <%--<td class="tdHP" align="center">{{item2.GoodsCode}}</td>--%>
                                              <td class="tdAmount" align="center">{{item2.Piece}}</td>
                                              <td class="tdHP" align="center">{{item2.ProductCode}}</td>
                                           </tr>
                                         </tbody>
                                   </table>
                               </div>
                           </div>

                         </div>

                       </div>

               </div>
           </div>

       </div>

   </div>

   <script type="text/javascript">
       var example1 = new Vue({
           el: '#app',
           data: {
               dataList: [],
               activeIndex: 0,
               tabs: [
                   {
                       title: '销售列表明细',
                   },
                   {
                       title: '退货列表明细',
                   },
               ],
               openid:'<%= wxUser.wxOpenID%>',
               currdate:'<%= CurrDate%>',
           },
           mounted() {
               this.getData();
           },
           methods: {
               getData() {
                   var that = this;
                   if (this.openid) {
                       $.ajax({
                           url: '../WeixinPush/wxApi.aspx?method=QueryServiceOrderData',
                           type: 'post', dataType: 'json', data: { openid: that.openid, currdate: that.currdate },
                           success: function (text) {
                               that.dataList = text;
                               for (var i = 0; i < that.dataList.length; i++) {
                                   that.dataList[i].activeIndex = 0;

                                   let sum2 = 0
                                   that.dataList[i].OrderList.forEach(v => {
                                       sum2 += v.Piece
                                   });

                                   that.dataList[i].OrderCount = sum2

                                   sum2 = 0
                                   that.dataList[i].ReturnOrderList.forEach(v => {
                                       sum2 += v.Piece
                                   });

                                   that.dataList[i].ReturnOrderCount = sum2

                               }
                               setTimeout(function () {
                                   that.Init()
                               }, 300)
                           }
                       });
                   } else {
                       $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '未获取到OpenID！' + this.openid, 'info');
                   }

               },
               Init() {
                   $('.accordion-item:first-child .accordion-header').addClass('active');
                   var dom = $('.accordion-header.active').parent()
                   var dom2 = dom.find('.accordion-content .tabs-container');
                   if (dom2) {
                       dom.find('.accordion-content').css('max-height', dom2.height());
                   } else {
                       dom.find('.accordion-content').css('max-height', '500px');
                   }
                   // 点击标题时切换面板
                   $('.accordion-header').click(function () {
                       // 移除所有活动状态
                       $('.accordion-header').not(this).removeClass('active');
                       $('.accordion-header').not(this).siblings('.accordion-content').css('max-height', '0');

                       // 切换当前面板
                       $(this).toggleClass('active');
                       const content = $(this).siblings('.accordion-content');

                       if ($(this).hasClass('active')) {
                           content.css('max-height', content[0].scrollHeight + 'px');
                       } else {
                           content.css('max-height', '0');
                       }
                   });

                   // 可选：添加键盘支持
                   $(document).on('keydown', '.accordion-header', function (e) {
                       if (e.key === 'Enter' || e.key === ' ') {
                           e.preventDefault();
                           $(this).click();
                       }
                   });
               },
               activateTab: function (item, index, topIndex) {
                   item.activeIndex = index;
                   this.$set(this.dataList, topIndex, item);

                   setTimeout(function () {
                       var dom = $('.accordion-header.active').parent()
                       var dom2 = dom.find('.accordion-content .tabs-container');
                       if (dom2) {
                           dom.find('.accordion-content').css('max-height', dom2.height());
                       } else {
                           dom.find('.accordion-content').css('max-height', '500px');
                       }
                   }, 30)
               },
           }
       })

   </script>
</body>
</html>
