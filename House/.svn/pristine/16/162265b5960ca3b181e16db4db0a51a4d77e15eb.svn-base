<%@ Page Title="" Language="C#" MasterPageFile="~/QY/QY.Master" AutoEventWireup="true" CodeBehind="qyAddClient.aspx.cs" Inherits="Cargo.QY.qyAddClient" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="mui-content">
        <form id="formAddress">
            <input type="hidden" id="ClientID" value="<%=client.ClientID %>" />
            <div class="mui-input-group" style="font-size: 14px;">
                <div class="mui-input-row">
                    <label>公司名称：</label>
                    <input type="text" id="ClientName" value="<%=client.ClientName %>" style="font-size: 14px;" class="mui-input-clear" placeholder="请输入公司名称" />
                </div>
                <div class="mui-input-row">
                    <label>客户姓名：</label>
                    <input type="text" id="Boss" value="<%=client.Boss %>" style="font-size: 14px;" class="mui-input-clear" placeholder="请输入客户姓名（必填）" />
                </div>
                <div class="mui-input-row">
                    <label>手机号码：</label>
                    <input type="text" id="Cellphone" value="<%=client.Cellphone %>" style="font-size: 14px;" class="mui-input-clear" placeholder="请输入手机号码（必填）" />
                </div>
                <div class="mui-input-row">
                    <label>公司电话：</label>
                    <input type="text" id="Telephone" value="<%=client.Telephone %>" style="font-size: 14px;" class="mui-input-clear" placeholder="请输入公司电话" />
                </div>
                <div class="mui-input-row">
                    <label>详细地址：</label>
                    <input type="text" id="Address" value="<%=client.Address %>" style="font-size: 14px;" class="mui-input-clear" placeholder="请输入详细地址（必填）" />
                </div>
            </div>
            <div class="title">
                &nbsp;
            </div>
            <button id="btnSubmit" type="button" class="mui-btn mui-btn-success btnsize_default mui-btn-block">确定</button>
        </form>
    </div>
    <script type="text/javascript">
        mui.init({
            swipeBack: true //启用右滑关闭功能
        });

        $('#btnSubmit').on('click', function () {
            if ($("#Boss").val() == "") {
                mui.alert('请输入客户姓名', '系统提示', function () {
                    $('#Boss').focus();
                });
                return false;
            }
            if ($("#Cellphone").val() == "") {
                mui.alert('请输入手机号码', '系统提示', function () {
                    $('#Cellphone').focus();
                });
                return false;
            }
            var phone = $("#Cellphone").val();
            if (!phone.match(/^1[3|4|5|7|8|6|9][0-9]{9}$/)) {
                mui.alert('请输入正确的手机号码', '系统提示', function () {
                    $('#Cellphone').focus();
                });
                return false;
            }
            if ($("#Address").val() == "") {
                mui.alert('请输入收件人详细地址', '系统提示', function () {
                    $('#Address').focus();
                });
                return false;
            }
            mui.showLoading("正在加载中..", "div"); //加载文字和类型，plus环境中类型为div时强制以div方式显示  

            var str = {
                "ClientName": $('#ClientName').val(), "Boss": $('#Boss').val(), "Cellphone": $('#Cellphone').val(), "Telephone": $('#Telephone').val(), "Address": $('#Address').val()
            }
            var json = JSON.stringify([str])
            $.ajax({
                url: 'qyServices.aspx?method=qyAddClient&ClientID=' + $('#ClientID').val(),
                type: 'post', dataType: 'json', data: { data: json },
                success: function (text) {
                    mui.hideLoading();//隐藏后的回调函数  
                    if (text.Result == true) {
                        mui.toast('保存成功！');
                        window.location.href = "qyClientManager.aspx";
                    }
                    else {
                        mui.toast('添加失败 失败原因：' + text.Message);
                    }
                }
            });

        });




    </script>
</asp:Content>
