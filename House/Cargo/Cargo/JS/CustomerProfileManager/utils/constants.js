// 常量定义文件
window.CONSTANTS = {
    // API 基础配置
    API: {
        BASE_URL: '../Client/', // 修改为.NET后台路径
        TIMEOUT: 30000,
        RETRY_COUNT: 3,
        // .NET后台特定配置
        ASPX_ENDPOINT: 'clientManagerApi.aspx',
        KPI_METHOD: 'GetClientKpiData'
    },

    // 客户状态
    CUSTOMER_STATUS: {
        ACTIVE: '活跃',
        PENDING: '待激活',
        DISABLED: '已停用'
    },

    // 客户类型
    CUSTOMER_TYPE: {
        ENTERPRISE: '企业客户',
        INDIVIDUAL: '个人客户',
        VIP: 'VIP客户'
    },

    // 分页配置
    PAGINATION: {
        DEFAULT_PAGE_SIZE: 20,
        PAGE_SIZES: [10, 20, 50, 100],
        MAX_PAGE_SIZE: 100
    },

    // 表格列配置
    TABLE_COLUMNS: {
        CUSTOMER: [
            {
                prop: 'Id',
                label: 'ID',
                width: 110,
                align: 'center',
                sortable: true
            },
            {
                prop: 'name',
                label: '公司名称',
                minWidth: 150,
                sortable: true,
                showOverflowTooltip: true
            },
            {
                prop: 'customerType',
                label: '客户类型',
                width: 120,
                align: 'center',
                // 格式化函数：参数分别为 单元格值、当前行数据、行索引
                formatter: (value, row, index) => {
                    // 映射关系：0=活跃，1=高活跃，其他值显示为原值或自定义文本
                    const typeMap = {
                        "0": '普通客户',
                        "1": '月结客户',
                        "2": 'VIP客户',
                        "3": '失信客户',
                        "4": '云仓二批客户',
                    };
                    // 优先从映射表取值，无匹配时显示原值（或改为 '' 显示空）
                    return typeMap[value.customerType] ?? value.customerType;
                }
            },
            {
                prop: 'contact',
                label: '联系人',
                width: 100,
                showOverflowTooltip: true
            },
            {
                prop: 'phone',
                label: '手机号',
                width: 120,
                showOverflowTooltip: true
            },
            {
                prop: 'warehouse',
                label: '所属仓库',
                width: 120,
                align: 'center',
                showOverflowTooltip: true
            },
            {
                prop: 'lastOrder',
                label: '最近下单',
                width: 110,
                align: 'center',
                type: 'date'
            },
            {
                prop: 'recentThreeMonthsOrders',
                label: '近三个月下单数量',
                width: 160,
                align: 'center'
            },
            {
                prop: 'recentThreeMonthsConsumption',
                label: '近三个月浏览次数',
                width: 160,
                align: 'center'
            },
            {
                prop: 'recentThreeMonthsBusiness',
                label: '近三个月查询次数',
                width: 160,
                align: 'center'
            },
            {
                prop: 'activeLevel',
                label: '活跃水平',
                width: 120,
                align: 'center',
                type: 'status',
                formatter: (value, row, index) => {
                    // 1. 处理原始值：非数字转 0
                    const activeNum = Number.isFinite(Number(value)) ? Number(value) : 0;
                    // 2. 状态映射
                    const statusConfig = [
                        { min: 0, max: 0, title: '不活跃', class: 'el-tag--gray' },
                        { min: 1, max: 30, title: '低活跃', class: 'el-tag--yellow' },
                        { min: 31, max: 180, title: '中活跃', class: 'el-tag--blue' },
                        { min: 181, max: Infinity, title: '高活跃', class: 'el-tag--green' }
                    ];
                    // 3. 匹配对应状态
                    const { title, class: tagClass } = statusConfig.find(item =>
                        activeNum >= item.min && activeNum <= item.max
                    );
                    // 4. 返回带样式的标签
                    return title;
                }
            },
            {
                prop: 'preferredBrand',
                label: '偏好品牌',
                width: 150,
                align: 'center',
                showOverflowTooltip: true
            },
            {
                prop: 'preferredSpec',
                label: '偏好规格',
                width: 140,
                align: 'center',
                showOverflowTooltip: true
            },
            {
                prop: 'valueLevel',
                label: '价值水平',
                width: 120,
                align: 'center',
                type: 'status',
                formatter: (value, row, index) => {
                    // 1. 处理原始值：非数字转 0
                    const activeNum = Number.isFinite(Number(value)) ? Number(value) : 0;
                    // 2. 状态映射
                    const statusConfig = [
                        { min: 0, max: 0, title: '无价值', class: 'el-tag--gray' },
                        { min: 1, max: 5, title: '低价值', class: 'el-tag--yellow' },
                        { min: 6, max: 30, title: '中价值', class: 'el-tag--blue' },
                        { min: 31, max: Infinity, title: '高价值', class: 'el-tag--green' }
                    ];
                    // 3. 匹配对应状态
                    const { title, class: tagClass } = statusConfig.find(item =>
                        activeNum >= item.min && activeNum <= item.max
                    );
                    // 4. 返回带样式的标签
                    return title;
                }
            },
            {
                prop: 'lastAccess',
                label: '最后访问',
                width: 170,
                align: 'center',
                showOverflowTooltip: true,
                formatter: (row, column, cellValue) => {
                    if (!cellValue || new Date(cellValue).getFullYear() === 1) {
                        return '-';
                    }
                    const date = new Date(cellValue);
                    const year = date.getFullYear();
                    const month = String(date.getMonth() + 1).padStart(2, '0');
                    const day = String(date.getDate()).padStart(2, '0');
                    const hours = String(date.getHours()).padStart(2, '0');
                    const minutes = String(date.getMinutes()).padStart(2, '0');
                    const seconds = String(date.getSeconds()).padStart(2, '0');

                    return `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`;
                }
            }
        ]
    },

    // KPI 配置
    KPI_CONFIG: [
        {
            id: 'totalCustomers',
            title: '客户总数',
            icon: 'el-icon-user',
            color: 'primary'
        },
        {
            id: 'activeCustomers',
            title: '活跃客户',
            icon: 'el-icon-success',
            color: 'success'
        },
        {
            id: 'newCustomers',
            title: '新增客户',
            icon: 'el-icon-plus',
            color: 'warning'
        },
        {
            id: 'revenue',
            title: '总收入',
            icon: 'el-icon-money',
            color: 'info'
        }
    ],

    // 图表配置
    CHART_CONFIG: {
        COLORS: [
            '#6366F1', '#10B981', '#F59E0B', '#EF4444',
            '#8B5CF6', '#06B6D4', '#84CC16', '#F97316'
        ],
        DEFAULT_HEIGHT: 400,
        ANIMATION_DURATION: 1000
    },

    // 消息提示配置
    MESSAGE: {
        SUCCESS_DURATION: 3000,
        ERROR_DURATION: 5000,
        WARNING_DURATION: 4000
    },

    // 文件上传配置
    UPLOAD: {
        MAX_SIZE: 10 * 1024 * 1024, // 10MB
        ALLOWED_TYPES: ['image/jpeg', 'image/png', 'image/gif', 'application/pdf'],
        CHUNK_SIZE: 1024 * 1024 // 1MB
    },

    // 本地存储键名
    STORAGE_KEYS: {
        USER_PREFERENCES: 'customer_management_preferences',
        FILTER_CACHE: 'customer_management_filters',
        TABLE_SETTINGS: 'customer_management_table_settings'
    },

    // 正则表达式
    REGEX: {
        PHONE: /^1[3-9]\d{9}$/,
        EMAIL: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
        ID_CARD: /^[1-9]\d{5}(18|19|20)\d{2}((0[1-9])|(1[0-2]))(([0-2][1-9])|10|20|30|31)\d{3}[0-9Xx]$/
    },

    // 错误码映射
    ERROR_CODES: {
        NETWORK_ERROR: 'NETWORK_ERROR',
        TIMEOUT: 'TIMEOUT',
        UNAUTHORIZED: 'UNAUTHORIZED',
        FORBIDDEN: 'FORBIDDEN',
        NOT_FOUND: 'NOT_FOUND',
        SERVER_ERROR: 'SERVER_ERROR',
        VALIDATION_ERROR: 'VALIDATION_ERROR'
    },

    // 错误消息
    ERROR_MESSAGES: {
        NETWORK_ERROR: '网络连接失败，请检查网络设置',
        TIMEOUT: '请求超时，请稍后重试',
        UNAUTHORIZED: '未授权访问，请重新登录',
        FORBIDDEN: '权限不足，无法执行此操作',
        NOT_FOUND: '请求的资源不存在',
        SERVER_ERROR: '服务器内部错误，请联系管理员',
        VALIDATION_ERROR: '数据验证失败，请检查输入内容'
    }
};