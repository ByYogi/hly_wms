<%@ Page Title="微信下单" Language="C#" MasterPageFile="~/QY/QY.Master" AutoEventWireup="true" CodeBehind="qyAddOrder.aspx.cs" Inherits="Cargo.QY.qyAddOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <header class="mui-bar mui-bar-nav">
        <a class="mui-action-back mui-icon mui-icon-left-nav mui-pull-left"></a>
        <h1 class="mui-title">微信下单</h1>
    </header>
    <div class="mui-content">
        <div class="mui-input-row mui-search">
            <input type="search" id="searchInput" onkeyup="enterSearch(event)" class="mui-input-clear" placeholder="请输入规格和花纹">
        </div>
        <div id="divStock"></div>
        <ul class="mui-table-view mui-table-view-chevron">
            <li class='mui-table-view-cell' style='padding-right: 5px; padding-left: 5px;'>
                <img class='mui-pull-left' style='height: 62px; width: 73px;' src='../CSS/image/ykhm.png' />
                <div style='width: 3%; min-height: 60px;' class=' mui-pull-left'></div>
                <div style='font-size: 12px;'>225/60R16----AE50----F8440</div>

                <div class="mui-pull-right" style="font-size: 13px;">
                    <button id='promptBtn1' type="button" class="mui-btn mui-btn-success mui-icon mui-icon-plus">加入购物车</button>
                </div>

                <div style='font-size: 12px;'>长沙一号仓--00A区</div>
                <div style='font-size: 14px; font-weight: bold; color: #FD6C24;'>库存：265条</div>
            </li>
            <li class='mui-table-view-cell' style='padding-right: 5px; padding-left: 5px;'>
                <img class='mui-pull-left' style='height: 62px; width: 73px;' src='../CSS/image/ykhm.png' />
                <div style='width: 3%; min-height: 60px;' class=' mui-pull-left'></div>
                <div style='font-size: 12px;'>225/60R16----AE50----F8440</div>

                <div class="mui-pull-right" style="font-size: 13px;">
                    <button id='promptBtn2' type="button" class="mui-btn mui-btn-success mui-icon mui-icon-plus">加入购物车</button>
                </div>

                <div style='font-size: 12px;'>长沙一号仓--00A区</div>
                <div style='font-size: 14px; font-weight: bold; color: #FD6C24;'>库存：265条</div>
            </li>
        </ul>
        <div id="info"></div>
    </div>
    <nav class="mui-bar mui-bar-tab">
        <a class="mui-tab-item" href="#tabbar-with-chat">
            <span class="mui-icon-extra mui-icon-extra-cart"><span id="gwc" style="font-size: 10px; line-height: 1.4; position: absolute; top: 2px; margin-left: 1px; padding: 1px 5px; color: #fff; background: red; border-radius: 100px; display: inline-block;">0</span></span>
            <span style="font-size: 11px; display: block; overflow: hidden; text-overflow: ellipsis;">购物车</span>
        </a>
        <a class="mui-tab-item" href="#tabbar-with-contact">
            <span class="mui-icon mui-icon-bars"></span>
            <span style="font-size: 11px; display: block; overflow: hidden; text-overflow: ellipsis;">我的订单</span>
        </a>
    </nav>
    <script type="text/javascript">
        for (var i = 1; i < 3; i++) {
            document.getElementById("promptBtn" + i).addEventListener('tap', function (e) {
                e.detail.gesture.preventDefault(); //修复iOS 8.x平台存在的bug，使用plus.nativeUI.prompt会造成输入法闪一下又没了
                var btnArray = ['取消', '确定'];
                mui.prompt('', '', '请输入条数', btnArray, function (e) {
                    if (e.index == 1) {
                        var oldPiece = Number($('#gwc').html());
                        var result = oldPiece + Number(e.value);
                        $('#gwc').html(result);
                        //info.innerText = e.value;
                    } else {
                        //info.innerText = '你点了取消按钮';
                    }
                })
            });
        }



        function enterSearch(e) {
            if (e.keyCode == 13) {
                var searchKey = $('#searchInput').val();
                $.ajax({
                    url: 'qyServices.aspx?method=QueryTypeStock',
                    cache: false, data: { key: searchKey },
                    success: function (text) {
                        $('#divStock').html(text);
                        //var ldl = document.getElementById("divStock");
                        //ldl.innerHTML = text;
                    }
                });
            }
        }

    </script>
</asp:Content>
