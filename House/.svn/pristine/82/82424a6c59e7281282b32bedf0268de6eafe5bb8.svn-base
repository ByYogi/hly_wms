<%@ Page Title="客户地址管理" Language="C#" MasterPageFile="~/QY/QY.Master" AutoEventWireup="true" CodeBehind="qyClientAddress.aspx.cs" Inherits="Cargo.QY.qyClientAddress" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <header class="mui-bar mui-bar-nav">
        <a class="mui-action-back mui-icon mui-icon-left-nav mui-pull-left"></a>
        <h1 class="mui-title">地址管理</h1>
    </header>
    <div class="mui-content">
        <div class="mui-input-row mui-search" style="margin-bottom: -10px; margin-top: 3px;">
            <input type="search" id="searchInput" onkeyup="enterSearch(event)" class="mui-input-clear" placeholder="请输入客户姓名或手机号码查询">
        </div>
        <div id="divStock"></div>
    </div>
    <script type="text/javascript">
        //绑定客户地址信息
        function inputAddSession(cid) {
            var client = cid.split('/');
            var storC = localStorage.getItem("CLIENT");
            localStorage.removeItem("CLIENT");
            var json = [{ ClientID: client[0], Address: client[1], Boss: client[2], Cellphone: client[3], City: client[4] }];
            localStorage.setItem("CLIENT", JSON.stringify(json));
            window.history.back(-1);
        }

        function enterSearch(e) {
            if (e.keyCode == 13) {
                var searchKey = $('#searchInput').val();
                $.ajax({
                    url: 'qyServices.aspx?method=QueryClientAddress',
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
