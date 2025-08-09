//扩展easyui表单的验证
$.extend($.fn.validatebox.defaults.rules, {
    // 判断最小长度
    minLength: {
        validator: function(value, param) {
            return value.length >= param[0];
        },
        message: '最少输入 {0} 个字符。'
    },
    // 判断最大长度
    maxLength: {
        validator: function(value, param) {
            return value.length <= param[0];
        },
        message: '最多输入 {0} 个字符。'
    },
    //判断具体长度
    Onlylength: {
        validator: function(value, param) {
            var len = $.trim(value).length;
            return len == param[0];
        },
        message: "必须输入{0}个字符"
    },

    //验证汉字
    CHS: {
        validator: function(value) {
            return /^[\u0391-\uFFE5]+$/.test(value);
        },
        message: '只能输入汉字'
    },
    // 验证英文
    ENG: {
        validator: function(value) {
            return /^[A-Za-z]+$/i.test(value);
        },
        message: '请输入英文'
    },
    // 验证身份证
    idcard: {
        validator: function(value) {
            return /^\d{15}(\d{2}[A-Za-z0-9])?$/i.test(value);
        },
        message: '身份证号码格式不正确'
    },
    //移动手机号码验证
    mobile: {
        validator: function(value) {
            var reg = /^1[3|4|5|8|9]\d{9}$/;
            return reg.test(value);
        },
        message: '输入手机号码格式不准确'
    },
    //Email验证
    email: {
        validator: function(value) {
            return /^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/.test(value);
        },
        message: '请输入有效的Email'
    },
    //国内邮编验证
    zipcode: {
        validator: function(value) {
            var reg = /^[1-9]\d{5}$/;
            return reg.test(value);
        },
        message: '邮编必须是非0开始的6位数字.'
    },
    // 验证IP地址
    ip: {
        validator: function(value) {
            return /d+.d+.d+.d+/i.test(value);
        },
        message: 'IP地址格式不正确'
    },
    // 验证整数
    integer: {
        validator: function(value) {
            return /^[+]?[0-9]+\d*$/i.test(value);
        },
        message: '请输入整数'
    },
    // 验证是否为小数或整数
    floatOrInt: {
        validator: function(value) {
            return /^\d+(\.\d+)?$/i.test(value);
        },
        message: '请输入数字，并保证格式正确'
    },
   
    //用户账号验证(只能包括 _ 数字 字母) 
    account: {//param的值为[]中值
        validator: function(value, param) {
            if (value.length < param[0] || value.length > param[1]) {
                $.fn.validatebox.defaults.rules.account.message = '用户名长度必须在' + param[0] + '至' + param[1] + '范围';
                return false;
            } else {
                if (!/^[\w]+$/.test(value)) {
                    $.fn.validatebox.defaults.rules.account.message = '用户名只能数字、字母、下划线组成.';
                    return false;
                } else {
                    return true;
                }
            }
        }, message: ''
    }
})