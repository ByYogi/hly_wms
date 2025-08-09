<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="myService.aspx.cs" Inherits="Cargo.Weixin.myService" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>我的中心</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />
    <meta name="description" content="轮胎 优科豪马 普利司通 马牌" />
    <link href="WeUI/CSS/weui.min.css" rel="stylesheet" />
    <link href="WeUI/CSS/style.css" rel="stylesheet" />
    <link href="WeUI/CSS/jquery-weui.css" rel="stylesheet" />
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
            font-family: -apple-system, BlinkMacSystemFont, "PingFang SC", "Helvetica Neue", Arial, sans-serif;
        }

        body {
            background-color: #f7f7f7;
            color: #333;
            line-height: 1.5;
        }

        .profile-container {
            max-width: 100%;
            padding: 15px;
        }

        .profile-header {
            display: flex;
            align-items: center;
            padding: 20px 15px;
            background-color: #fff;
            border-radius: 12px;
            margin-bottom: 15px;
              box-shadow: 0 2px 10px rgb(0 0 0 / 12%);
        }

        .avatar {
            width: 90px;
            height: 90px;
            border-radius: 50%;
            object-fit: cover;
            margin-right: 15px;
            border: 2px solid #f0f0f0;
        }

        .user-info {
            margin-left: 22px;
        }

            .user-info h2 {
                font-size: 20px;
                margin-bottom: 5px;
                color: #333;
            }

            .user-info p {
                font-size: 14px;
                color: #999;
            }

        .profile-card {
            background-color: #fff;
            border-radius: 12px;
            padding: 15px;
            margin-bottom: 15px;
          box-shadow: 0 2px 10px rgb(68 95 92 / 18%);
            margin: 0 8px 15px 8px;
        }

        .info-item {
            display: flex;
            justify-content: space-between;
            padding: 12px 0;
            border-bottom: 1px solid #f5f5f5;
        }

            .info-item:last-child {
                border-bottom: none;
            }

        .info-label {
            color: #666;
            font-size: 15px;
        }

        .info-value {
            color: #333;
            font-weight: 500;
        }

        .member-tag {
            display: inline-block;
            padding: 2px 8px;
            background-color: #ff6b6b;
            color: white;
            font-size: 12px;
            border-radius: 10px;
            margin-left: 8px;
        }

        .bind-btn {
            width: 100%;
            height: 44px;
            border: none;
            border-radius: 10px;
            background-color: #07C160;
            color: white;
            font-size: 16px;
            font-weight: 500;
            margin-top: 20px;
            cursor: pointer;
            transition: all 0.3s;
            position: absolute; /* 子元素绝对定位 */
            width: 96%;
            height: 40px;
            left: 50%;
            transform: translateX(-50%);
            box-shadow: 0 2px 10px rgb(68 95 92 / 18%);
        }

            .bind-btn:active {
                background-color: #06AD56;
                /*transform: scale(0.98);*/
            }

        button.btn.btn-save, button.btn.btn-cancel {
            border-radius: 20px;
            height: 47px;
        }
    </style>
    <style type="text/css">
        /* 弹窗样式 */
        .modal-overlay {
            position: fixed;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background: rgba(0, 0, 0, 0.7);
            display: flex;
            justify-content: center;
            align-items: center;
            z-index: 1000;
            opacity: 0;
            pointer-events: none;
            transition: opacity 0.4s ease;
            backdrop-filter: blur(5px);
        }

            .modal-overlay.active {
                opacity: 1;
                pointer-events: all;
            }

        .modal {
            background: white;
            border-radius: 20px;
            box-shadow: 0 25px 50px rgba(0, 0, 0, 0.3);
            width: 90%;
            max-width: 500px;
            overflow: hidden;
            transform: translateY(30px);
            opacity: 0;
            transition: all 0.4s ease;
        }

        .modal-overlay.active .modal {
            transform: translateY(0);
            opacity: 1;
        }

        .modal-header {
            padding: 25px 30px;
            background: linear-gradient(to right, #07C160, #2ecc71);
            color: white;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .modal-title {
            font-size: 1.5rem;
            font-weight: 600;
        }

        .close-btn {
            background: rgba(255, 255, 255, 0.2);
            width: 36px;
            height: 36px;
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            cursor: pointer;
            transition: all 0.3s ease;
        }

            .close-btn:hover {
                background: rgba(255, 255, 255, 0.3);
                transform: rotate(90deg);
            }

        .modal-body {
            padding: 30px;
        }

        .form-group {
            margin-bottom: 25px;
        }

        .form-label {
            display: block;
            margin-bottom: 8px;
            font-weight: 500;
            color: #2c3e50;
            font-size: 1.1rem;
        }

        .form-input {
            width: 100%;
            padding: 14px 20px;
            border: 2px solid #e0e0e0;
            border-radius: 12px;
            font-size: 1.1rem;
            transition: all 0.3s ease;
        }

            .form-input:focus {
                border-color: #3498db;
                outline: none;
                box-shadow: 0 0 0 3px rgba(52, 152, 219, 0.2);
            }

        .modal-footer {
            padding: 20px 30px;
            background: #f8f9fa;
            display: flex;
            justify-content: flex-end;
            gap: 15px;
        }

        .btn-cancel {
            background: #e0e0e0;
            color: #7f8c8d;
        }

            .btn-cancel:hover {
                background: #d1d1d1;
            }

        .btn-save {
            background: linear-gradient(to right, #07C160, #2ecc71);
            color: white;
            box-shadow: 0 4px 10px rgba(46, 204, 113, 0.3);
        }

            .btn-save:hover {
                transform: translateY(-2px);
                box-shadow: 0 6px 15px rgba(46, 204, 113, 0.4);
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

        @media (max-width: 600px) {
            .container {
                padding: 20px;
            }

            .user-card {
                flex-direction: column;
                align-items: center;
                text-align: center;
            }

            .avatar {
                margin-right: 0;
                margin-bottom: 20px;
            }

            .detail-item {
                justify-content: center;
            }

            .action-buttons {
                flex-direction: column;
                gap: 15px;
            }

            .btn {
                width: 100%;
            }
        }
    </style>
</head>

<body>
      <div id="app">
      <!-- 顶部导航 -->
      <!-- 用户头像和基本信息 -->
    <div class="profile-header">
      <img :src="DetailData.AvatarSmall"  class="avatar" alt="用户头像">
      <div class="user-info">
        <h2>{{DetailData.wxName}}</h2> 
        <p>ID: {{DetailData.ID}}</p> 
      </div>
    </div>
    
    <!-- 详细信息卡片 -->
    <div class="profile-card">
      <div class="info-item">
        <span class="info-label">供应商编码</span>
        <span class="info-value">{{(IsEmpty(DetailData.ClientNum)?"-":DetailData.ClientNum)}}</span> 
      </div>
      <div class="info-item">
        <span class="info-label">手机号码</span>
        <span class="info-value">{{(IsEmpty(DetailData.Cellphone)?"-":DetailData.Cellphone)}}</span> 
      </div>
      <div class="info-item">
        <span class="info-label">注册时间</span>
        <span class="info-value">{{DateTimeFormatter(DetailData.RegisterDate)}}</span> 
      </div>
      <div class="info-item">
        <span class="info-label">积分</span>
        <span class="info-value">{{DetailData.ConsumerPoint}}</span> 
      </div>
    </div>
    
    <div class="profile-card">
      <div class="info-item">
        <span class="info-label">地址</span>
        <span class="info-value">{{DetailData.Province}}-{{DetailData.City}}-{{DetailData.Country}}</span>
      </div>
      <div class="info-item">
        <span class="info-label">客户地址</span>
        <span class="info-value">{{DetailData.Address  || '暂无'}}</span>
      </div>
<%--      <div class="info-item">
        <span class="info-label">最近登录</span>
        <span class="info-value">{{user.lastLogin}}</span> 
      </div>--%>
    </div>

     <button class="bind-btn" 
          v-if="(DetailData.ClientNum==''||DetailData.ClientNum==null||DetailData.ClientNum=='0') && (DetailData.StorePhone==''||DetailData.StorePhone==null) " 
         @click="openModal">绑定</button>
     <button v-else class="bind-btn"  @click="openModal">修改</button>

<%--      <!-- 底部操作按钮 -->
      <div class="action-buttons">
          <button v-if="(DetailData.ClientNum==''||DetailData.ClientNum==null||DetailData.ClientNum=='0') && (DetailData.StorePhone==''||DetailData.StorePhone==null) " 
              class="btn btn-primary" @click="openModal">绑定</button>
          <button v-else class="btn btn-primary" @click="openModal">修改</button>
      </div>--%>

             <!-- 弹窗 -->
      <div class="modal-overlay" :class="{ active: showModal }" @click.self="closeModal">
          <div class="modal">
              <div class="modal-header">
                  <div class="modal-title">
                      <i class="fas fa-user-cog"></i> 编辑用户资料
                  </div>
                  <div class="close-btn" @click="closeModal">
                      <i class="fas fa-times"></i>
                  </div>
              </div>
              
              <div class="modal-body">
                  <div class="form-group">
                      <label class="form-label">供应商编码</label>
                      <input type="text" class="form-input" v-model="editUser.ClientNum">
                  </div>
                  
                  <div class="form-group">
                      <label class="form-label">电话号码</label>
                      <input type="number" class="form-input" v-model="editUser.Cellphone">
                  </div>
                  
              </div>
              
              <div class="modal-footer">
                  <button class="btn btn-cancel" @click="closeModal">
                      <i class="fas fa-times"></i> 取消
                  </button>
                  <button class="btn btn-save" @click="saveChanges">
                      <i class="fas fa-save"></i> 保存更改
                  </button>
              </div>
          </div>
      </div>

       
      <!-- 通知 -->
      <div class="notification" :class="{ show: showNotification }">
          <i class="fas fa-check-circle"></i> {{ notificationMessage }}
      </div>

  </div>


    <script src="WeUI/JS/jquery-2.1.4.js"></script>
    <script src="WeUI/JS/fastclick.js"></script>
    <script src="WeUI/JS/jquery-weui.js"></script>
    <script src="../JS/vue.js"></script>
    <script type="text/javascript">
        new Vue({
            el: '#app',
            data: {
                // 在实际应用中，这里可以存放动态数据
                openid: '<%= wxUser.wxOpenID%>',
                DetailData: {},
                showModal: false,
                showNotification: false,
                notificationMessage: '',
                editUser: {
                    ClientNum: '',
                    Cellphone: '',
                },
            },
            mounted() {
                this.getData();
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
                            url: '../WeixinPush/wxApi.aspx?method=QueryWxDetailData',
                            type: 'post', dataType: 'json', data: { openid: that.openid },
                            success: function (text) {
                                that.DetailData = text[0];
                                if (that.DetailData.ClientNum == "0" && that.IsEmpty(that.DetailData.Cellphone)) {
                                    that.DetailData.ClientNum = "";
                                }
                            }
                        });
                    } else {
                        that.ShowTitle('未获取到OpenID')
                    }

                },
                openModal() {
                    // 复制用户数据到编辑对象
                    //this.editUser = Object.assign({}, this.DetailData, this.editUser);
                    this.editUser = this.DetailData
                    this.showModal = true;
                },
                closeModal() {
                    this.showModal = false;
                },
                saveChanges() {
                    var that = this;
                    // 保存编辑的数据

                    if (this.IsEmpty(this.editUser.ClientNum) || this.IsEmpty(this.editUser.Cellphone)) {
                        that.ShowTitle('供应商编码或电话不能为空！')
                        return;
                    }
                    if (!this.Regexp(this.editUser.Cellphone)) {
                        that.ShowTitle('请填写有效的电话号码！')
                        return;
                    }
                    this.DetailData = Object.assign({}, this.DetailData, this.editUser)
                    console.log(this.DetailData)
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
                            console.log(result)

                            if (result.Result) {
                                that.ShowTitle(result.Message)
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

                }
            }
        });
    </script>
</body>
</html>
