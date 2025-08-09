<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wxDetail.aspx.cs" Inherits="Cargo.Weixin.wxDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
      <script type="text/javascript" src="../JS/vue.js"></script>
    <script src="../JS/easy/js/jquery.min.1.13.js"></script>
<style type="text/css">
    * {
        box-sizing: border-box;
        margin: 0;
        padding: 0;
        font-family: 'Segoe UI', 'PingFang SC', sans-serif;
    }

    body {
        background: linear-gradient(135deg, #f5f7fa 0%, #e4edf5 100%);
        color: #333;
        line-height: 1.6;
        padding: 15px;
        min-height: 100vh;
    }

    .container {
        max-width: 100%;
        margin: 0 auto;
    }

    /* 头部样式 */
    .header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 20px;
        padding: 15px;
        background: white;
        border-radius: 16px;
        box-shadow: 0 4px 20px rgba(0, 0, 0, 0.05);
    }

    .title {
        font-size: 1.4rem;
        font-weight: 600;
        color: #2c3e50;
    }

    .icon-btn {
        background: transparent;
        border: none;
        font-size: 1.2rem;
        color: #3498db;
        padding: 8px;
        border-radius: 50%;
        transition: all 0.3s;
    }

        .icon-btn:active {
            background-color: #f0f8ff;
            transform: scale(0.95);
        }

    /* 筛选区域 */
    .filters {
        display: flex;
        gap: 10px;
        margin-bottom: 20px;
        flex-wrap: wrap;
    }

    .filter-item {
        flex: 1;
        min-width: 120px;
        position: relative;
    }

    .filter-label {
        display: block;
        margin-bottom: 6px;
        font-size: 0.85rem;
        color: #7f8c8d;
        font-weight: 500;
    }

    .filter-select, .filter-input {
        width: 100%;
        padding: 12px 15px;
        border: 1px solid #e1e5e9;
        border-radius: 12px;
        background: white;
        font-size: 0.95rem;
        color: #2c3e50;
        -webkit-appearance: none;
        appearance: none;
    }

        .filter-select:focus, .filter-input:focus {
            border-color: #3498db;
            outline: none;
            box-shadow: 0 0 0 2px rgba(52, 152, 219, 0.2);
        }

    /* 账单卡片 */
    .bill-card {
        background: white;
        border-radius: 16px;
        margin-bottom: 15px;
        overflow: hidden;
        box-shadow: 0 4px 15px rgba(0, 0, 0, 0.03);
        transition: transform 0.3s ease, box-shadow 0.3s ease;
    }

        .bill-card:active {
            transform: translateY(-2px);
            box-shadow: 0 6px 20px rgba(0, 0, 0, 0.08);
        }

    .card-header {
        display: flex;
        justify-content: space-between;
        align-items: center;
        padding: 16px;
        border-bottom: 1px solid #f0f4f8;
        background: linear-gradient(90deg, #f8fafc 0%, #ffffff 100%);
    }

    .supplier-info {
        display: flex;
        align-items: center;
        gap: 12px;
    }

    .supplier-icon {
        width: 100px;
        height: 140px;
        border-radius: 12px;
        background: linear-gradient(135deg, #3498db 0%, #1abc9c 100%);
        display: flex;
        align-items: center;
        justify-content: center;
        color: white;
        font-weight: bold;
        font-size: 2.9rem;
    }

    .supplier-details h3 {
        font-size: 2.2rem;
        font-weight: 600;
        color: #2c3e50;
        margin-bottom: 4px;
    }

    .supplier-details p {
        font-size: 1.85rem;
        color: #7f8c8d;
        display: flex;
        align-items: center;
        gap: 6px;
    }

    .bill-status {
        padding: 6px 12px;
        border-radius: 50px;
        font-size: 0.8rem;
        font-weight: 500;
    }

    .status-paid {
        background-color: rgba(46, 204, 113, 0.15);
        color: #27ae60;
    }

    .status-pending {
        background-color: rgba(241, 196, 15, 0.15);
        color: #f39c12;
    }

    .status-overdue {
        background-color: rgba(231, 76, 60, 0.15);
        color: #c0392b;
    }

    .card-body {
        /*padding: 16px;*/
    }

    .bill-details {
        display: grid;
        grid-template-columns: repeat(2, 1fr);
        gap: 15px;
        margin-bottom: 7px;
        /*border-bottom: 1px dashed #8b8f95;*/
        padding-bottom: 7px;
    }

    .detail-item h4 {
        font-size: 1.2rem;
        color: #7f8c8d;
        font-weight: 500;
        margin-bottom: 5px;
    }

    .detail-item p {
        font-size: 1.4rem;
        font-weight: 500;
        color: #2c3e50;
    }

    .amount {
        font-size: 1.4rem;
        font-weight: 700;
        color: #3498db;
    }

    .card-footer {
        display: flex;
        justify-content: space-between;
        padding: 12px 16px;
        background-color: #f9fbfd;
        border-top: 1px solid #f0f4f8;
    }

    .action-btn {
        padding: 8px 16px;
        border-radius: 50px;
        font-size: 0.9rem;
        font-weight: 500;
        background: white;
        border: 1px solid #e1e5e9;
        display: flex;
        align-items: center;
        gap: 6px;
        transition: all 0.2s;
    }

        .action-btn:active {
            background-color: #f0f8ff;
            transform: scale(0.97);
        }

    .primary-btn {
        background: linear-gradient(135deg, #3498db 0%, #2980b9 100%);
        color: white;
        border: none;
    }

    /* 空状态 */
    .empty-state {
        text-align: center;
        padding: 40px 20px;
        background: white;
        border-radius: 16px;
        box-shadow: 0 4px 15px rgba(0, 0, 0, 0.03);
    }

    .empty-icon {
        font-size: 3rem;
        color: #bdc3c7;
        margin-bottom: 15px;
    }

    .empty-state h3 {
        margin-bottom: 10px;
        color: #7f8c8d;
    }

    .empty-state p {
        color: #95a5a6;
        margin-bottom: 20px;
    }

    /* 底部导航 */
    .bottom-nav {
        position: fixed;
        bottom: 0;
        left: 0;
        right: 0;
        display: flex;
        background: white;
        box-shadow: 0 -2px 10px rgba(0, 0, 0, 0.05);
        padding: 10px 0;
        z-index: 100;
    }

    .nav-item {
        flex: 1;
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        color: #7f8c8d;
        font-size: 0.75rem;
    }

        .nav-item.active {
            color: #3498db;
        }

    .nav-icon {
        font-size: 1.3rem;
        margin-bottom: 4px;
    }

    /* 响应式调整 */
    @media (min-width: 768px) {
        /*  .container {
            max-width: 700px;
        }*/

        .filters {
            flex-wrap: nowrap;
        }

        .bill-card {
            margin-bottom: 20px;
        }
    }

    .qm {
        position: fixed;
        right: 20px;
        bottom: 20px;
    }
</style>
  <style type="text/css">
      .signature-container {
          max-width: 800px;
          margin: 20px auto;
          padding: 15px;
          border: 1px solid #eee;
          border-radius: 4px;
          box-shadow: 0 2px 5px rgba(0,0,0,0.05);
      }

      canvas {
          display: block;
          background: #f8f8f8;
          border: 1px solid #ddd;
          cursor: crosshair;
      }

      .actions {
          margin-top: 15px;
          text-align: center;
      }

      button {
          padding: 15px 30px;
          margin: 0 5px;
          background: #42b983;
          color: white;
          border: none;
          border-radius: 17px;
          cursor: pointer;
          font-size: 2.5rem;
      }

          button:disabled {
              background: #ccc;
          }

      .preview {
          margin-top: 20px;
          text-align: center;
      }

          .preview img {
              max-width: 100%;
              border: 1px dashed #ddd;
          }

      .outputs {
          margin-top: 20px;
          padding: 10px;
          background: #f9f9f9;
          border-radius: 4px;
      }

          .outputs textarea {
              width: 100%;
              height: 100px;
              padding: 10px;
              margin-top: 5px;
              border: 1px solid #ddd;
              border-radius: 4px;
              font-family: monospace;
          }
  </style>
  <style type="text/css">
      * {
          margin: 0;
          padding: 0;
          box-sizing: border-box;
          -webkit-tap-highlight-color: transparent;
      }

      body {
          font-family: -apple-system, BlinkMacSystemFont, "PingFang SC", sans-serif;
          background: #f5f7fa;
          padding: 15px;
      }

      .signature-container {
          max-width: 100%;
          margin: 0 auto;
      }

      .open-btn {
          display: block;
          padding: 12px 30px;
          margin: 20px auto;
          background: #42b983;
          color: white;
          border: none;
          border-radius: 25px;
          font-size: 16px;
          box-shadow: 0 4px 10px rgba(66, 185, 131, 0.3);
      }

      .signature-modal {
          position: fixed;
          top: 0;
          left: 0;
          width: 100%;
          height: 100%;
          background: white;
          z-index: 1000;
          display: flex;
          flex-direction: column;
          transform: translateY(100%);
          transition: transform 0.3s ease;
      }

          .signature-modal.active {
              transform: translateY(0);
          }

      .canvas-container {
          flex: 1;
          display: flex;
          flex-direction: column;
          overflow: hidden;
      }

      .canvas-header {
          padding: 15px;
          border-bottom: 1px solid #eee;
          display: flex;
          justify-content: space-between;
          align-items: center;
      }

      canvas {
          flex: 1;
          width: 100%;
          touch-action: none;
          background: #f8f8f8;
          border: 1px solid #ddd;
          cursor: crosshair;
      }

      .actions {
          display: flex;
          padding: 15px;
          border-top: 1px solid #eee;
          background: white;
      }

          .actions button {
              flex: 1;
              padding: 12px 10px;
              margin: 0 5px;
              border-radius: 8px;
              border: none;
              font-size: 14px;
              cursor: pointer;
              transition: all 0.2s;
          }

              .actions button:active {
                  transform: scale(0.98);
              }

      .clear-btn {
          background: #fef3f2;
          color: #f04438;
      }

      .save-btn {
          background: #42b983;
          color: white;
      }

      .preview {
          max-width: 10%;
          margin-top: 20px;
          padding: 15px;
          background: white;
          border-radius: 10px;
          box-shadow: 0 2px 12px rgba(0, 0, 0, 0.05);
          position: fixed;
          bottom: 20px;
          right: 20px;
      }

          .preview img {
              max-width: 100%;
              margin-top: 10px;
              border: 1px dashed #eee;
              border-radius: 4px;
          }

      .signature-tips {
          text-align: center;
          color: #999;
          font-size: 14px;
          padding: 10px;
      }

      @media (orientation: landscape) {
          .signature-modal {
              flex-direction: row;
          }

          .canvas-container {
              flex-direction: row;
          }

          canvas {
              height: 100%;
          }
      }


      .modal-img {
          position: fixed;
          top: 0;
          left: 0;
          width: 100%;
          height: 100%;
          background: rgba(0, 0, 0, 0.92);
          display: flex;
          align-items: center;
          justify-content: center;
          z-index: 1001;
      }

          .modal-img img {
              max-width: 90%;
              max-height: 90%;
              border: 2px solid white;
              background: white;
              border-radius: 8px;
          }

      .bill-details {
          padding: 0 21px;
          padding-top: 16px;
      }

      .card-body {
          transition: max-height 0.6s cubic-bezier(0.165, 0.84, 0.44, 1), padding 0.4s ease;
          transform: translateY(-10px);
          transition: max-height 0.6s cubic-bezier(0.165, 0.84, 0.44, 1), opacity 0.4s ease 0.1s, transform 0.4s ease 0.1s; /* 延迟动画效果 */
          /*max-height: 0;*/
      }

      .accordion-icon {
          transform: rotate(180deg);
          position: absolute;
          right: 47px;
          font-size: 55px;
          transition: transform 0.4s cubic-bezier(0.165, 0.84, 0.44, 1);
      }

      .card-header.active .accordion-icon {
          transform: rotate(360deg)
      }

      button.clear-btn, button.save-btn {
          font-size: 3rem;
          /*writing-mode: tb;*/
      }

      .canvas-header h2 {
          font-size: 3rem;
      }
         .notification {
       position: fixed;
       top: 30px;
       left: 50%;
       transform: translateX(-50%);
       background: #2ecc71;
       color: white;
       padding: 15px 30px;
       border-radius: 50px;
       font-weight: 500;
       box-shadow: 0 5px 15px rgba(0, 0, 0, 0.2);
       opacity: 0;
       transition: all 0.4s ease;
       z-index: 2000;
   }

       .notification.show {
           opacity: 1;
           top: 40px;
       }

  </style>
</head>
<body>
     <div id="app">
   <div class="container">
      <!-- 账单列表 -->
      <div v-if="filteredBills.length  > 0">
        <div class="bill-card" v-for="(bill, index) in filteredBills" :key="index">
          <div class="card-header active">
              <div class="accordion-icon">▼</div>
            <div class="supplier-info">
              <div class="supplier-icon">
                {{ bill.AccountTitle.charAt(0)  }}
              </div>
              <div class="supplier-details">
                <h3>{{ bill.AccountTitle  }}</h3>
                <p>
                  <i class="fas fa-file-invoice"></i>
                  {{ bill.AccountNO  }} 
                   <i style="margin-left:20px;"> {{DateTimeFormatter(bill.CreateDate)}}</i>
                   <i style="margin-left:20px;">条数:{{bill.GoodsList.length}}</i>
                </p>
              </div>
            </div>
            <div class="bill-status" :class="'status-' + bill.status"> 
              {{ statusText[bill.Status] }}
            </div>
          </div>
          
          <div class="card-body">
            <div class="bill-details"  v-for="(billV2, indexV2) in bill.GoodsList" :key="indexV2">
              <div class="detail-item">
                <h4>订单号</h4>
                <p>{{ (billV2.OrderNo)  }}</p>
              </div>
<%--              <div class="detail-item">
                <h4>账单日期</h4>
                <p>{{ DateTimeFormatter(bill.CreateDate)  }}</p>
              </div>--%>
         
              <div class="detail-item">
                <h4>账单金额</h4>
                <p class="amount">¥{{ billV2.Total.toFixed(2)  }}</p>
              </div>
            </div>
          </div>
          
        </div>
      </div>
      
    </div>

        <!-- 底部操作按钮 -->
        <div class="action-buttons qm" v-if="IsEmpty(this.signatureImage) && bills.length>0 && IsSign">
            <button class="btn btn-primary" @click="openModal">签名</button>
        </div>


           <!-- 签名弹窗 -->
      <div class="signature-modal" :class="{ active: showModal }">
        <div class="canvas-container">
          <div class="canvas-header">
            <h2>请签名</h2>
            <button @click="showModal = false">关闭</button>
          </div>
          <canvas 
            ref="canvas"
            @mousedown="startDrawing"
            @mousemove="draw"
            @mouseup="stopDrawing"
            @touchstart="handleTouchStart"
            @touchmove="handleTouchMove"
            @touchend="stopDrawing">
          </canvas>
          <%--<p class="signature-tips">提示：横向滑动屏幕可绘制更宽签名</p>--%>
        </div>
        
        <div class="actions">
          <button class="clear-btn" @click="clearCanvas" :disabled="!hasSignature">清除</button>
          <button class="save-btn" @click="saveSignature" :disabled="!hasSignature">保存签名</button>
        </div>
      </div>

          <!-- 签名预览 -->
      <div class="preview" v-if="signatureImage">
        <h3>点击预览</h3>
        <img :src="signatureImage" alt="签名预览"  @click="showImageModal = true"/>
        <%--<p>Base64: {{ signatureBase64.substring(0,  30) }}...</p>--%>
      </div>

         <!-- 图片预览弹窗 -->
      <div class="modal-img" v-if="showImageModal" @click="showImageModal = false">
        <img :src="signatureImage" alt="签名大图">
        <!-- <div class="close-modal">&times;</div> -->
      </div>

         
        <!-- 通知 -->
        <div class="notification" :class="{ show: showNotification }">
            <i class="fas fa-check-circle"></i> {{ notificationMessage }}
        </div>

    </div>
    <script type="text/javascript">
        new Vue({
            el: '#app',
            data: {
                // 在实际应用中，这里可以存放动态数据
                openid:'<%= wxUser.wxOpenID%>',
                AccountNO: '<%= AccountNO%>',
                DetailData: {},
                showModal: false,
                showImageModal: false,
                showNotification: false,
                notificationMessage: '',
                editUser: {
                    ClientNum: '',
                    Cellphone: '',
                },
                isDrawing: false,
                lastX: 0,
                lastY: 0,
                signatureImage: null,
                signatureBase64: '',
                hasSignature: false,
                ctx: null,
                touchIdentifier: null,
                filterDate: 'all',
                filterStatus: 'all',
                statusText: {
                    '0': '通过',
                },
                bills: [],
                IsSign: true,

            },
            watch: {
                showModal(val) {
                    {
                        if (val) {
                            {
                                this.$nextTick(() => {
                                    {
                                        this.initCanvas();
                                    }
                                });
                            }
                        } else {
                            {
                                // 解决移动端滚动锁定的问题
                                document.body.style.overflow = '';
                            }
                        }
                    }
                }
            },
            mounted() {
                //this.ShowTitle('签名上传成功!')
                this.getData();
            },
            computed: {
                filteredBills() {
                    let result = this.bills;

                    return result;
                }
            },
            methods: {
                DateTimeFormatter(val) {
                    if (val == null || val == '') {
                        return ''
                    }
                    var dt = this.formatDate((new Date(val)));
                    return dt
                },
                formatDate(date) {
                    const year = date.getFullYear();
                    const month = this.pad(date.getMonth() + 1);
                    const day = this.pad(date.getDate());
                    const hour = this.pad(date.getHours());
                    const minute = this.pad(date.getMinutes());
                    const second = this.pad(date.getSeconds());
                    const weekday = date.getDay() === 0 ? 7 : date.getDay();

                    return `${year}-${month}-${day} ${hour}:${minute}:${second}`;
                },
                pad(num, length = 2, str = '0') {
                    return num.toString().padStart(length, str);
                },
                getData() {
                    var that = this;
                    if (this.openid) {
                        $.ajax({
                            url: '../WeixinPush/wxApi.aspx?method=QueryWxSupplierBillData',
                            type: 'post', dataType: 'json', data: { AccountNO: that.AccountNO },
                            success: function (text) {
                                that.bills = text;

                                setTimeout(function () {
                                    that.Init();

                                    if (that.bills.length > 0 && !that.IsEmpty(that.bills[0].ElecSignImg)) {
                                        //that.fetchImage(that.bills[0].ElecSignImg)

                                        // 使用示例 
                                        that.getImageBase64('../WeixinPush/wxApi.aspx?method=GetPicture', that.bills[0].ElecSignImg, function (base64) {
                                            var dd = base64.split(':')[1]
                                            //console.log('21312', (base64));
                                            that.signatureImage = base64
                                            // 输出：data:image/jpeg;base64,/9j/4AAQSkZJRgABAQ...
                                        })


                                    }
                                }, 100)

                                if (that.bills.length == 0) {
                                    that.bills = [{
                                        AccountTitle: '-',
                                        AccountNO: '-',
                                        CreateDate: '-',
                                        GoodsList: [],
                                    }]
                                    that.IsSign = false;
                                }
                            }
                        });
                    } else {
                        $.messager.alert('<%= Cargo.Common.GetSystemNameAndVersion()%>', '未获取到OpenID！', 'info');
                    }

                },
                Init() {
                    //card-header
                    var acc = $(".card-header");
                    var i;
                    for (i = 0; i < acc.length; i++) {
                        acc[i].addEventListener("click", function () {
                            var panel = this.nextElementSibling;


                            if (this.classList.length == 2) {
                                panel.style.maxHeight = 0 + "px";
                            } else {
                                panel.style.maxHeight = panel.scrollHeight + "px";
                            }
                            this.classList.toggle("active");
                        });
                    }
                },
                openModal() {

                    this.showModal = true;
                },
                closeModal() {
                    this.showModal = false;
                },
                saveChanges() {
                    var that = this;
                    // 保存编辑的数据

                    if (this.IsEmpty(this.editUser.ClientNum) || this.IsEmpty(this.editUser.Cellphone)) {
                        that.ShowTitle('客户编码或电话不能为空！')
                        return;
                    }
                    if (!this.Regexp(this.editUser.Cellphone)) {
                        that.ShowTitle('请填写有效的电话号码！')
                        return;
                    }
                    this.DetailData = Object.assign({}, this.DetailData, this.editUser)

                    $.ajax({
                        async: false,
                        url: '../WeixinPush/wxApi.aspx?method=UpdateWxStoresData',
                        type: 'post', dataType: 'json',
                        data: {
                            wxOpenID: that.DetailData.wxOpenID,
                            Cellphone: that.DetailData.Cellphone,
                            ClientNum: that.DetailData.ClientNum,
                            UserType: that.DetailData.UserType,
                        },
                        success: function (msg) {
                            var result = (msg)

                            if (result.Result) {
                                that.ShowTitle('绑定成功！')
                            } else {
                                that.ShowTitle('更新失败！msg:' + result.Message)
                            }

                            that.closeModal();
                        }
                    });

                },
                Regexp(phone) {
                    var reg = /^[1](([3][0-9])|([4][0,1,4-9])|([5][0-3,5-9])|([6][2,5,6,7])|([7][0-8])|([8][0-9])|([9][0-3,5-9]))[0-9]{8}$/
                    var str = phone
                    var result = reg.test(str)
                    return result
                },
                ShowTitle(title) {
                    this.notificationMessage = title;
                    this.showNotification = true;

                    // 3秒后隐藏通知
                    setTimeout(() => {
                        this.showNotification = false;
                    }, 3000);
                },
                IsEmpty(value) {
                    // 检查 null/undefined
                    if (value == null) return true;

                    // 检查空字符串/纯空格
                    if (typeof value === 'string' && value.trim() === '') return true;

                    // 检查空数组
                    if (Array.isArray(value) && value.length === 0) return true;

                    // 检查空对象
                    if (value.constructor === Object && Object.keys(value).length === 0) return true;

                    return false;

                },

                // 初始化画布 
                initCanvas() {
                    {
                        const canvas = this.$refs.canvas;
                        this.ctx = canvas.getContext('2d');

                        // 适配高清屏
                        const dpr = window.devicePixelRatio || 1;
                        canvas.width = canvas.clientWidth * dpr;
                        canvas.height = canvas.clientHeight * dpr;
                        this.ctx.scale(dpr, dpr);

                        // 设置画笔 
                        this.ctx.lineWidth = 6;
                        this.ctx.lineCap = 'round';
                        this.ctx.lineJoin = 'round';
                        this.ctx.strokeStyle = '#000000';

                        // 锁定页面滚动
                        document.body.style.overflow = 'hidden';
                    }
                },

                // 触摸开始（移动端专用）
                handleTouchStart(e) {
                    {
                        e.preventDefault();
                        if (e.touches.length === 1) {
                            {
                                this.touchIdentifier = e.touches[0].identifier;
                                this.startDrawing(e);
                            }
                        }
                    }
                },

                // 触摸移动（移动端专用）
                handleTouchMove(e) {
                    {
                        e.preventDefault();
                        if (this.touchIdentifier !== null) {
                            {
                                for (let i = 0; i < e.touches.length; i++) {
                                    {
                                        if (e.touches[i].identifier === this.touchIdentifier) {
                                            {
                                                this.draw({ ...e, touches: [e.touches[i]] });
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                },

                // 开始绘制 
                startDrawing(e) {
                    {
                        this.isDrawing = true;
                        const pos = this.getPosition(e);
                        [this.lastX, this.lastY] = [pos.x, pos.y];
                    }
                },

                // 绘制过程
                draw(e) {
                    {
                        if (!this.isDrawing) return;

                        const pos = this.getPosition(e);

                        // 移动端签名优化：动态调整线宽产生笔锋效果 [2]()
                        const velocity = Math.sqrt(
                            Math.pow(pos.x - this.lastX, 2) +
                            Math.pow(pos.y - this.lastY, 2)
                        );
                        this.ctx.lineWidth = 6;

                        this.ctx.beginPath();
                        this.ctx.moveTo(this.lastX, this.lastY);
                        this.ctx.lineTo(pos.x, pos.y);
                        this.ctx.stroke();

                        [this.lastX, this.lastY] = [pos.x, pos.y];
                        this.hasSignature = true;
                    }
                },

                // 获取坐标位置
                getPosition(e) {
                    const canvas = this.$refs.canvas;
                    const rect = canvas.getBoundingClientRect();

                    let clientX, clientY;
                    if (e.touches && e.touches[0]) {
                        {
                            clientX = e.touches[0].clientX;
                            clientY = e.touches[0].clientY;
                        }
                    } else {
                        {
                            clientX = e.clientX;
                            clientY = e.clientY;
                        }
                    }

                    return {
                        x: clientX - rect.left,
                        y: clientY - rect.top
                    };
                },

                // 停止绘制
                stopDrawing() {
                    this.isDrawing = false;
                    this.touchIdentifier = null;
                },

                // 清除画布
                clearCanvas() {
                    {
                        const canvas = this.$refs.canvas;
                        this.ctx.clearRect(0, 0, canvas.width, canvas.height);
                        this.signatureImage = null;
                        this.signatureBase64 = '';
                        this.hasSignature = false;
                    }
                },
                // 保存签名
                saveSignature() {
                    var that = this;
                    const canvas = that.$refs.canvas;
                    that.signatureBase64 = canvas.toDataURL('image/png');
                    that.signatureImage = that.signatureBase64;
                    that.showModal = false;
                    console.log(that.signatureImage)
                    // 转换为二进制Blob 
                    const binaryData = that.base64ToBlob(that.signatureBase64);
                    console.log(that.signatureBase64)
                    // 上传到服务器 
                    that.uploadSignature(binaryData);

                    // 实际应用中可添加文件保存逻辑
                    //this.saveAsFile(this.signatureBase64); 

                },
                // Base64转Blob 
                base64ToBlob(base64) {
                    const byteString = atob(base64.split(',')[1]);
                    const mimeString = base64.split(',')[0].split(':')[1].split(';')[0];
                    const ab = new ArrayBuffer(byteString.length);
                    const ia = new Uint8Array(ab);

                    for (let i = 0; i < byteString.length; i++) {
                        ia[i] = byteString.charCodeAt(i);
                    }

                    return new Blob([ab], { type: mimeString });
                },
                // AJAX上传二进制数据 
                uploadSignature(blob) {
                    var that = this;
                    that.ShowTitle('正在生成签名文件...')

                    const formData = new FormData();
                    formData.append('signature', blob, 'signature.png');
                    formData.append('AccountNO', this.bills[0].AccountNO); // 可根据需要添加其他参数 

                    const url = '../WeixinPush/wxApi.aspx?method=wxUpFile';

                    const xhr = new XMLHttpRequest();
                    xhr.open('POST', url, true);

                    xhr.upload.onprogress = (e) => {
                        if (e.lengthComputable) {
                            const percent = Math.round((e.loaded / e.total) * 100);
                            //this.statusText = `上传中: ${percent}%`;
                        }
                    };

                    xhr.onload = () => {
                        if (xhr.status === 200) {
                            that.ShowTitle('签名上传成功!')
                            //setTimeout(() => this.statusText = '签名后将自动转为二进制上传', 3000);
                        } else {
                            that.ShowTitle('上传失败，请重试!')
                        }
                    };

                    xhr.onerror = () => {
                        that.ShowTitle('网络错误，上传失败!')
                    };

                    xhr.send(formData);

                    this.getData();
                },
                // 保存为文件（实际应用中启用）
                saveAsFile(dataUrl) {
                    const link = document.createElement('a');
                    link.download = '签名_' + new Date().toISOString().slice(0, 10) + '.png';
                    link.href = dataUrl;
                    link.click();
                },
                getImageBase64(imagePath, pathUrl, callback) {
                    // 创建 XMLHttpRequest 对象 
                    const xhr = new XMLHttpRequest();

                    // 配置请求（ASPX 后端需注意路径处理）
                    xhr.open('POST', imagePath, true);
                    xhr.responseType = 'blob';

                    const formData = new FormData();
                    formData.append('pathUrl', pathUrl); // 可根据需要添加其他参数 

                    xhr.onload = function () {
                        if (xhr.status === 200) {
                            const blob = xhr.response;
                            const reader = new FileReader();

                            // Blob 转 Base64
                            reader.onloadend = function () {
                                const base64data = reader.result;
                                callback(base64data);  // 返回 Base64 数据
                            };
                            var aa = reader.readAsDataURL(blob);
                            console.error(' 图片加载失败:', blob);

                        } else {
                            console.error(' 图片加载失败:', xhr.status);
                        }
                    };

                    xhr.onerror = function () {
                        console.error(' 网络请求错误');
                    };

                    xhr.send(formData);
                },


            }
        });
    </script>
</body>
</html>
