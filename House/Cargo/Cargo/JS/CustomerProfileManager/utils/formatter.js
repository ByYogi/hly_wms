// 数据格式化工具函数
window.Formatter = {
  
  /**
   * 格式化数字，添加千分位分隔符
   * @param {number} num 数字
   * @param {number} decimals 小数位数
   * @returns {string} 格式化后的字符串
   */
  formatNumber(num, decimals = 0) {
    if (num === null || num === undefined || isNaN(num)) {
      return '-';
    }
    
    return new Intl.NumberFormat('zh-CN', {
      minimumFractionDigits: decimals,
      maximumFractionDigits: decimals
    }).format(num);
  },
  
  /**
   * 格式化货币
   * @param {number} amount 金额
   * @param {string} currency 货币代码
   * @returns {string} 格式化后的货币字符串
   */
  formatCurrency(amount, currency = 'CNY') {
    if (amount === null || amount === undefined || isNaN(amount)) {
      return '-';
    }
    
    return new Intl.NumberFormat('zh-CN', {
      style: 'currency',
      currency: currency
    }).format(amount);
  },
  
  /**
   * 格式化百分比
   * @param {number} value 数值
   * @param {number} decimals 小数位数
   * @returns {string} 格式化后的百分比字符串
   */
  formatPercent(value, decimals = 2) {
    if (value === null || value === undefined || isNaN(value)) {
      return '-';
    }
    
    return new Intl.NumberFormat('zh-CN', {
      style: 'percent',
      minimumFractionDigits: decimals,
      maximumFractionDigits: decimals
    }).format(value / 100);
  },
  
  /**
   * 格式化日期
   * @param {string|Date} date 日期
   * @param {string} format 格式类型
   * @returns {string} 格式化后的日期字符串
   */
  formatDate(date, format = 'date') {
    if (!date) return '-';
    
    const d = new Date(date);
    if (isNaN(d.getTime())) return date;
    
    const options = {
      date: {
        year: 'numeric',
        month: '2-digit',
        day: '2-digit'
      },
      datetime: {
        year: 'numeric',
        month: '2-digit',
        day: '2-digit',
        hour: '2-digit',
        minute: '2-digit'
      },
      time: {
        hour: '2-digit',
        minute: '2-digit',
        second: '2-digit'
      },
      month: {
        year: 'numeric',
        month: 'long'
      },
      year: {
        year: 'numeric'
      }
    };
    
    return d.toLocaleDateString('zh-CN', options[format] || options.date);
  },
  
  /**
   * 格式化相对时间
   * @param {string|Date} date 日期
   * @returns {string} 相对时间字符串
   */
  formatRelativeTime(date) {
    if (!date) return '-';
    
    const d = new Date(date);
    if (isNaN(d.getTime())) return date;
    
    const now = new Date();
    const diff = now - d;
    const seconds = Math.floor(diff / 1000);
    const minutes = Math.floor(seconds / 60);
    const hours = Math.floor(minutes / 60);
    const days = Math.floor(hours / 24);
    const months = Math.floor(days / 30);
    const years = Math.floor(days / 365);
    
    if (years > 0) return `${years}年前`;
    if (months > 0) return `${months}个月前`;
    if (days > 0) return `${days}天前`;
    if (hours > 0) return `${hours}小时前`;
    if (minutes > 0) return `${minutes}分钟前`;
    if (seconds > 30) return `${seconds}秒前`;
    return '刚刚';
  },
  
  /**
   * 格式化文件大小
   * @param {number} bytes 字节数
   * @param {number} decimals 小数位数
   * @returns {string} 格式化后的文件大小
   */
  formatFileSize(bytes, decimals = 2) {
    if (bytes === 0) return '0 B';
    if (!bytes || isNaN(bytes)) return '-';
    
    const k = 1024;
    const sizes = ['B', 'KB', 'MB', 'GB', 'TB'];
    const i = Math.floor(Math.log(bytes) / Math.log(k));
    
    return parseFloat((bytes / Math.pow(k, i)).toFixed(decimals)) + ' ' + sizes[i];
  },
  
  /**
   * 格式化电话号码
   * @param {string} phone 电话号码
   * @returns {string} 格式化后的电话号码
   */
  formatPhone(phone) {
    if (!phone) return '-';
    
    // 移除所有非数字字符
    const cleaned = phone.replace(/\D/g, '');
    
    // 手机号码格式化
    if (cleaned.length === 11 && cleaned.startsWith('1')) {
      return cleaned.replace(/(\d{3})(\d{4})(\d{4})/, '$1-$2-$3');
    }
    
    // 固定电话格式化
    if (cleaned.length >= 7) {
      if (cleaned.length === 7) {
        return cleaned.replace(/(\d{3})(\d{4})/, '$1-$2');
      } else if (cleaned.length === 8) {
        return cleaned.replace(/(\d{4})(\d{4})/, '$1-$2');
      } else if (cleaned.length >= 10) {
        return cleaned.replace(/(\d{3,4})(\d{3,4})(\d{4})/, '$1-$2-$3');
      }
    }
    
    return phone;
  },
  
  /**
   * 格式化身份证号码
   * @param {string} idCard 身份证号码
   * @returns {string} 格式化后的身份证号码
   */
  formatIdCard(idCard) {
    if (!idCard) return '-';
    
    // 脱敏处理，只显示前6位和后4位
    if (idCard.length === 18) {
      return idCard.substring(0, 6) + '********' + idCard.substring(14);
    } else if (idCard.length === 15) {
      return idCard.substring(0, 6) + '*****' + idCard.substring(11);
    }
    
    return idCard;
  },
  
  /**
   * 格式化银行卡号
   * @param {string} cardNumber 银行卡号
   * @returns {string} 格式化后的银行卡号
   */
  formatBankCard(cardNumber) {
    if (!cardNumber) return '-';
    
    // 脱敏处理，只显示后4位
    const cleaned = cardNumber.replace(/\D/g, '');
    if (cleaned.length >= 4) {
      return '**** **** **** ' + cleaned.slice(-4);
    }
    
    return cardNumber;
  },
  
  /**
   * 格式化地址
   * @param {object} address 地址对象
   * @returns {string} 格式化后的地址字符串
   */
  formatAddress(address) {
    if (!address) return '-';
    
    if (typeof address === 'string') {
      return address;
    }
    
    const parts = [];
    if (address.province) parts.push(address.province);
    if (address.city) parts.push(address.city);
    if (address.district) parts.push(address.district);
    if (address.street) parts.push(address.street);
    if (address.detail) parts.push(address.detail);
    
    return parts.join('') || '-';
  },
  
  /**
   * 截断文本
   * @param {string} text 文本
   * @param {number} length 最大长度
   * @param {string} suffix 后缀
   * @returns {string} 截断后的文本
   */
  truncateText(text, length = 50, suffix = '...') {
    if (!text) return '-';
    
    if (text.length <= length) {
      return text;
    }
    
    return text.substring(0, length) + suffix;
  },
  
  /**
   * 高亮关键词
   * @param {string} text 文本
   * @param {string} keyword 关键词
   * @returns {string} 高亮后的HTML字符串
   */
  highlightKeyword(text, keyword) {
    if (!text || !keyword) return text;
    
    const regex = new RegExp(`(${keyword})`, 'gi');
    return text.replace(regex, '<mark>$1</mark>');
  },
  
  /**
   * 格式化状态
   * @param {string} status 状态值
   * @returns {object} 状态对象，包含文本和类型
   */
  formatStatus(status) {
    const statusMap = {
      '活跃': { text: '活跃', type: 'success' },
      '正常': { text: '正常', type: 'success' },
      '启用': { text: '启用', type: 'success' },
      '待激活': { text: '待激活', type: 'warning' },
      '待审核': { text: '待审核', type: 'warning' },
      '处理中': { text: '处理中', type: 'warning' },
      '已停用': { text: '已停用', type: 'danger' },
      '已禁用': { text: '已禁用', type: 'danger' },
      '已删除': { text: '已删除', type: 'danger' },
      '失败': { text: '失败', type: 'danger' }
    };
    
    return statusMap[status] || { text: status, type: 'info' };
  }
};