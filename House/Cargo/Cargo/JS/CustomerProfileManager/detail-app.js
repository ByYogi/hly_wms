// 客户详情页面Vue应用
new Vue({
    el: '#app',
    data: {
        // 加载状态
        customerInfoLoading: false,
        trendChartLoading: false,
        brandChartLoading: false,
        detailsLoading: false,
        consumptionRankingLoading: false,
        orderRankingLoading: false,
        orderHistoryLoading: false,
        iconRecordsLoading: false,
        saveLoading: false,

        // iframe检测
        isInIframe: false,

        // 客户ID
        customerId: null,

        // 客户基本信息
        customerInfo: {
            id: '',
            name: '',
            customerType: '',
            contact: '',
            phone: '',
            email: '',
            warehouse: '',
            lastOrder: '',
            status: '',
            totalConsumption: 0,
            totalOrders: 0,
            registerDate: '',
            lastVisit: '',
            createTime: '',
            remark: '',
            notes: '',
            noteUpdater: '',
            noteUpdateTime: ''
        },

        // 图表数据
        trendPeriod: 'month',
        trendChart: null,
        brandChart: null,

        // 客户详细信息表格数据
        customerDetails: [],
        filteredCustomerDetails: [],
        searchKeyword: '',
        
        // 客户偏好分页数据
        customerPreferencesPagination: {
            page: 1,
            pageSize: 10,
            total: 0
        },
        paginatedCustomerDetails: [],
        
        // 客户常用内容标签数据
        frequentItems: [],

        // 排行榜数据
        consumptionRanking: [],
        orderRanking: [],

        // 订单历史记录
        orderHistory: [],
        orderDateRange: null,
        orderPagination: {
            page: 1,
            pageSize: 10,
            total: 0
        },

        // 新增标签页相关数据
        activeTab: 'order',
        browseHistory: [],
        searchHistory: [],
        browseHistoryLoading: false,
        searchHistoryLoading: false,
        
        // 筛选相关
        showFilterDialog: false,
        filterForm: {
            dateRange: null,
            brand: '',
            specification: ''
        },
        
        // 当前分页数据（根据活动标签页动态切换）
        currentPagination: {
            page: 1,
            pageSize: 10,
            total: 0
        },

        // 图标记录
        iconRecords: [],

        // 对话框状态
        editDialogVisible: false,
        orderDetailDialogVisible: false,
        preferencesAnalysisDialogVisible: false,
        selectedOrder: null,

        // 编辑表单
        editForm: {
            name: '',
            contact: '',
            phone: '',
            email: '',
            status: '',
            remark: ''
        },

        // 表单验证规则
        editFormRules: {
            name: [
                { required: true, message: '请输入客户名称', trigger: 'blur' },
                { min: 2, max: 50, message: '长度在 2 到 50 个字符', trigger: 'blur' }
            ],
            contact: [
                { required: true, message: '请输入联系人', trigger: 'blur' },
                { min: 2, max: 20, message: '长度在 2 到 20 个字符', trigger: 'blur' }
            ],
            phone: [
                { required: true, message: '请输入联系电话', trigger: 'blur' },
                { pattern: /^1[3-9]\d{9}$/, message: '请输入正确的手机号码', trigger: 'blur' }
            ],
            email: [
                { required: true, message: '请输入邮箱地址', trigger: 'blur' },
                { pattern: /^[^\s@]+@[^\s@]+\.[^\s@]+$/, message: '请输入正确的邮箱地址', trigger: 'blur' }
            ],
            status: [
                { required: true, message: '请选择客户状态', trigger: 'change' }
            ]
        }
    },

    async mounted() {
        // 检测是否在iframe中
        this.checkIfInIframe();

        // 获取客户ID
        this.getCustomerId();

        // 初始化页面
        await this.initializePage();

        // Vue加载完成，显示页面内容
        this.$nextTick(() => {
            const app = document.getElementById('app');
            if (app) {
                app.classList.add('vue-loaded');
                // 移除v-cloak属性（Vue会自动处理，但为了确保兼容性）
                app.removeAttribute('v-cloak');
            }
        });
    },

    methods: {
        /**
         * 检测当前页面是否在iframe中
         */
        checkIfInIframe() {
            try {
                this.isInIframe = window.self !== window.top;
                
                if (!this.isInIframe) {
                    try {
                        this.isInIframe = window.frameElement !== null;
                    } catch (e) {
                        this.isInIframe = true;
                    }
                }
                
                console.log('iframe检测结果:', this.isInIframe);
            } catch (error) {
                console.error('iframe检测失败:', error);
                this.isInIframe = true;
            }
        },

        /**
         * 获取客户ID
         */
        getCustomerId() {
            const urlParams = new URLSearchParams(window.location.search);
            this.customerId = urlParams.get('id') || '103253';
            console.log('当前客户ID:', this.customerId);
        },

        /**
         * 初始化页面
         */
        async initializePage() {
            try {
                // 并行加载数据
                await Promise.all([
                    this.loadCustomerInfo(),
                    this.loadCustomerDetails(),
                    this.loadFrequentItems(),
                    this.loadConsumptionRanking(),
                    this.loadOrderRanking(),
                    this.loadOrderHistory(),
                    this.loadBrowseHistory(),
                    this.loadSearchHistory(),
                    this.loadIconRecords()
                ]);

                // 初始化图表
                this.$nextTick(() => {
                    this.initTrendChart();
                    this.initBrandChart();
                });

                // 初始化当前分页数据
                this.updateCurrentPagination();

                this.$message.success('客户详情加载完成');
            } catch (error) {
                console.error('初始化失败:', error);
                this.$message.error('数据加载失败，请刷新页面重试');
            }
        },

        /**
         * 加载客户基本信息
         */
        async loadCustomerInfo() {
            this.customerInfoLoading = true;
            
            try {
                // 使用模拟数据生成器
                if (window.CustomerDetailMockData) {
                    this.customerInfo = window.CustomerDetailMockData.generateCustomerInfo(this.customerId);
                } else {
                    // 默认数据
                    this.customerInfo = {
                        id: this.customerId,
                        name: '客户信息',
                        customerType: '普通客户',
                        contact: '-',
                        phone: '-',
                        email: '-',
                        warehouse: '-',
                        lastOrder: '-',
                        status: '活跃',
                        totalConsumption: 0,
                        createTime: '-',
                        remark: ''
                    };
                }
                
                console.log('客户信息加载成功:', this.customerInfo);
            } catch (error) {
                console.error('加载客户信息失败:', error);
                this.$message.error('加载客户信息失败');
            } finally {
                this.customerInfoLoading = false;
            }
        },

        /**
         * 加载客户详细信息
         */
        async loadCustomerDetails() {
            this.detailsLoading = true;
            
            try {
                if (window.CustomerDetailMockData) {
                    this.customerDetails = window.CustomerDetailMockData.customerPreferences;
                } else {
                    this.customerDetails = [];
                }
                this.filteredCustomerDetails = [...this.customerDetails];
                this.customerPreferencesPagination.total = this.filteredCustomerDetails.length;
                this.updatePaginatedCustomerDetails();
            } catch (error) {
                console.error('加载客户详细信息失败:', error);
                this.$message.error('加载客户详细信息失败');
            } finally {
                this.detailsLoading = false;
            }
        },

        /**
         * 刷新客户详细信息
         */
        refreshCustomerDetails() {
            this.loadCustomerDetails();
            this.$message.success('客户偏好数据已刷新');
        },

        /**
         * 处理搜索
         */
        handleSearch() {
            if (!this.searchKeyword.trim()) {
                this.filteredCustomerDetails = [...this.customerDetails];
            } else {
                const keyword = this.searchKeyword.toLowerCase();
                this.filteredCustomerDetails = this.customerDetails.filter(item =>
                    item.productName.toLowerCase().includes(keyword) ||
                    item.specification.toLowerCase().includes(keyword) ||
                    item.brand.toLowerCase().includes(keyword)
                );
            }
            
            // 重置分页并更新分页数据
            this.customerPreferencesPagination.page = 1;
            this.customerPreferencesPagination.total = this.filteredCustomerDetails.length;
            this.updatePaginatedCustomerDetails();
        },

        /**
         * 更新分页后的客户详细信息
         */
        updatePaginatedCustomerDetails() {
            const start = (this.customerPreferencesPagination.page - 1) * this.customerPreferencesPagination.pageSize;
            const end = start + this.customerPreferencesPagination.pageSize;
            this.paginatedCustomerDetails = this.filteredCustomerDetails.slice(start, end);
        },

        /**
         * 处理客户偏好分页变化
         */
        handleCustomerPreferencesPageChange(page) {
            this.customerPreferencesPagination.page = page;
            this.updatePaginatedCustomerDetails();
        },

        /**
         * 处理客户偏好分页大小变化
         */
        handleCustomerPreferencesPageSizeChange(pageSize) {
            this.customerPreferencesPagination.pageSize = pageSize;
            this.customerPreferencesPagination.page = 1;
            this.updatePaginatedCustomerDetails();
        },

        /**
         * 查看产品详情
         */
        viewProductDetail(row) {
            this.$message.info(`查看产品：${row.productName} (${row.specification})`);
            // 这里可以添加跳转到产品详情页面的逻辑
        },

        /**
         * 获取规格标签类型
         */
        getSpecificationTagType(specification) {
            // 根据规格返回不同的标签颜色
            if (specification.includes('225/50R18')) return 'primary';
            if (specification.includes('215/')) return 'success';
            if (specification.includes('K127')) return 'warning';
            if (specification.includes('95W')) return 'danger';
            if (specification.includes('康佳森')) return 'info';
            return '';
        },

        /**
         * 获取品牌标签类型
         */
        getBrandTagType(brand) {
            const brandColors = {
                '韩泰': 'primary',
                '规格': 'success',
                '花纹': 'warning',
                '在线': 'info',
                '防爆轮胎': 'danger'
            };
            return brandColors[brand] || '';
        },

        /**
         * 获取库存数量样式类
         */
        getStockCountClass(count) {
            if (count >= 20) return 'stock-high';
            if (count >= 10) return 'stock-medium';
            return 'stock-low';
        },

        /**
         * 加载客户常用内容标签
         */
        async loadFrequentItems() {
            try {
                // 模拟常用内容数据，基于图片中的样式
                this.frequentItems = [
                    { name: '韩泰', count: 26, category: '品牌', type: 'brand' },
                    { name: '225/50R18', count: 22, category: '规格', type: 'specification' },
                    { name: 'K127防爆', count: 18, category: '花纹', type: 'pattern' },
                    { name: '95W', count: 15, category: '载重', type: 'load' },
                    { name: '康佳森', count: 12, category: '品牌', type: 'brand' },
                    { name: 'K3000 plus', count: 10, category: '花纹', type: 'pattern' },
                    { name: '215/50ZR17', count: 8, category: '规格', type: 'specification' },
                    { name: '防爆轮胎', count: 6, category: '类型', type: 'type' }
                ];
            } catch (error) {
                console.error('加载常用内容失败:', error);
                this.$message.error('加载常用内容失败');
            }
        },

        /**
         * 获取常用标签类型
         */
        getFrequentTagType(type) {
            const typeColors = {
                'brand': 'primary',
                'specification': 'success',
                'pattern': 'warning',
                'load': 'danger',
                'type': 'info'
            };
            return typeColors[type] || '';
        },

        /**
         * 处理常用标签点击
         */
        handleFrequentTagClick(item) {
            // 将标签内容设置为搜索关键词
            this.searchKeyword = item.name;
            this.handleSearch();
            this.$message.success(`已搜索：${item.name}`);
        },

        /**
         * 加载消费TOP10排行
         */
        async loadConsumptionRanking() {
            this.consumptionRankingLoading = true;
            
            try {
                if (window.CustomerDetailMockData) {
                    this.consumptionRanking = window.CustomerDetailMockData.consumptionRanking;
                } else {
                    this.consumptionRanking = [];
                }
            } catch (error) {
                console.error('加载消费排行失败:', error);
                this.$message.error('加载消费排行失败');
            } finally {
                this.consumptionRankingLoading = false;
            }
        },

        /**
         * 加载下单TOP10排行
         */
        async loadOrderRanking() {
            this.orderRankingLoading = true;
            
            try {
                if (window.CustomerDetailMockData) {
                    this.orderRanking = window.CustomerDetailMockData.orderRanking;
                } else {
                    this.orderRanking = [];
                }
            } catch (error) {
                console.error('加载下单排行失败:', error);
                this.$message.error('加载下单排行失败');
            } finally {
                this.orderRankingLoading = false;
            }
        },

        /**
         * 加载订单历史记录
         */
        async loadOrderHistory() {
            this.orderHistoryLoading = true;
            
            try {
                if (window.CustomerDetailMockData) {
                    this.orderHistory = window.CustomerDetailMockData.orderHistory;
                    this.orderPagination.total = this.orderHistory.length;
                } else {
                    this.orderHistory = [];
                    this.orderPagination.total = 0;
                }
            } catch (error) {
                console.error('加载订单历史失败:', error);
                this.$message.error('加载订单历史失败');
            } finally {
                this.orderHistoryLoading = false;
            }
        },

        /**
         * 加载图标记录
         */
        async loadIconRecords() {
            this.iconRecordsLoading = true;
            
            try {
                if (window.CustomerDetailMockData) {
                    this.iconRecords = window.CustomerDetailMockData.iconRecords;
                } else {
                    this.iconRecords = [];
                }
            } catch (error) {
                console.error('加载图标记录失败:', error);
                this.$message.error('加载图标记录失败');
            } finally {
                this.iconRecordsLoading = false;
            }
        },

        /**
         * 初始化消费趋势图表
         */
        initTrendChart() {
            const chartDom = document.getElementById('trendChart');
            if (!chartDom) return;

            this.trendChart = echarts.init(chartDom);
            this.updateTrendChart();
        },

        /**
         * 更新消费趋势图表
         */
        updateTrendChart() {
            if (!this.trendChart) return;

            const trendData = window.CustomerDetailMockData ? 
                window.CustomerDetailMockData.trendData[this.trendPeriod] : 
                { categories: [], data: [] };

            const option = {
                title: {
                    text: '消费趋势',
                    left: 'center',
                    textStyle: {
                        fontSize: 14,
                        color: '#333'
                    }
                },
                tooltip: {
                    trigger: 'axis',
                    formatter: function(params) {
                        return params[0].name + '<br/>' +
                               params[0].marker + '消费金额: ¥' + params[0].value.toLocaleString();
                    }
                },
                grid: {
                    left: '3%',
                    right: '4%',
                    bottom: '3%',
                    containLabel: true
                },
                xAxis: {
                    type: 'category',
                    boundaryGap: false,
                    data: trendData.categories
                },
                yAxis: {
                    type: 'value',
                    axisLabel: {
                        formatter: '¥{value}'
                    }
                },
                series: [{
                    name: '消费金额',
                    type: 'line',
                    smooth: true,
                    data: trendData.data,
                    itemStyle: {
                        color: '#409EFF'
                    },
                    areaStyle: {
                        color: {
                            type: 'linear',
                            x: 0,
                            y: 0,
                            x2: 0,
                            y2: 1,
                            colorStops: [{
                                offset: 0, color: 'rgba(64, 158, 255, 0.3)'
                            }, {
                                offset: 1, color: 'rgba(64, 158, 255, 0.1)'
                            }]
                        }
                    }
                }]
            };

            this.trendChart.setOption(option);
        },

        /**
         * 初始化品牌偏好图表
         */
        initBrandChart() {
            const chartDom = document.getElementById('brandChart');
            if (!chartDom) return;

            this.brandChart = echarts.init(chartDom);
            this.updateBrandChart();
        },

        /**
         * 更新品牌偏好图表
         */
        updateBrandChart() {
            if (!this.brandChart) return;

            const brandData = window.CustomerDetailMockData ? 
                window.CustomerDetailMockData.brandData : [];

            const option = {
                title: {
                    text: '品牌偏好',
                    left: 'center',
                    textStyle: {
                        fontSize: 14,
                        color: '#333'
                    }
                },
                tooltip: {
                    trigger: 'item',
                    formatter: '{a} <br/>{b}: {c}% ({d}%)'
                },
                legend: {
                    orient: 'vertical',
                    left: 'left',
                    data: brandData.map(item => item.name)
                },
                series: [{
                    name: '品牌偏好',
                    type: 'pie',
                    radius: '50%',
                    data: brandData,
                    emphasis: {
                        itemStyle: {
                            shadowBlur: 10,
                            shadowOffsetX: 0,
                            shadowColor: 'rgba(0, 0, 0, 0.5)'
                        }
                    }
                }]
            };

            this.brandChart.setOption(option);
        },

        /**
         * 处理趋势图表时间周期变化
         */
        handleTrendPeriodChange(period) {
            this.trendPeriod = period;
            this.updateTrendChart();
            
            const periodNames = {
                week: '近7天',
                month: '近30天',
                quarter: '近3个月',
                year: '近一年'
            };
            this.$message.success(`已切换到${periodNames[period]}数据`);
        },

        /**
         * 刷新品牌图表
         */
        refreshBrandChart() {
            this.brandChartLoading = true;
            setTimeout(() => {
                this.updateBrandChart();
                this.brandChartLoading = false;
                this.$message.success('品牌偏好数据已刷新');
            }, 500);
        },

        /**
         * 刷新客户信息
         */
        refreshCustomerInfo() {
            this.loadCustomerInfo();
            this.$message.success('客户信息已刷新');
        },

        /**
         * 刷新消费排行
         */
        refreshConsumptionRanking() {
            this.loadConsumptionRanking();
            this.$message.success('消费排行已刷新');
        },

        /**
         * 刷新下单排行
         */
        refreshOrderRanking() {
            this.loadOrderRanking();
            this.$message.success('下单排行已刷新');
        },

        /**
         * 刷新订单历史
         */
        refreshOrderHistory() {
            this.loadOrderHistory();
            this.$message.success('订单历史已刷新');
        },

        /**
         * 刷新图标记录
         */
        refreshIconRecords() {
            this.loadIconRecords();
            this.$message.success('相关记录已刷新');
        },

        /**
         * 编辑客户
         */
        editCustomer() {
            this.editForm = {
                name: this.customerInfo.name,
                contact: this.customerInfo.contact,
                phone: this.customerInfo.phone,
                email: this.customerInfo.email,
                status: this.customerInfo.status,
                remark: this.customerInfo.remark
            };
            this.editDialogVisible = true;
        },

        /**
         * 保存客户编辑
         */
        async saveCustomerEdit() {
            try {
                await this.$refs.editForm.validate();
                
                this.saveLoading = true;
                
                // 模拟保存操作
                await new Promise(resolve => setTimeout(resolve, 1000));
                
                // 更新客户信息
                Object.assign(this.customerInfo, this.editForm);
                
                this.editDialogVisible = false;
                this.$message.success('客户信息更新成功');
            } catch (error) {
                if (error !== false) {
                    console.error('保存失败:', error);
                    this.$message.error('保存失败');
                }
            } finally {
                this.saveLoading = false;
            }
        },

        /**
         * 重置编辑表单
         */
        resetEditForm() {
            if (this.$refs.editForm) {
                this.$refs.editForm.resetFields();
            }
        },

        /**
         * 处理订单日期范围变化
         */
        handleOrderDateChange(dateRange) {
            this.orderDateRange = dateRange;
            console.log('订单日期范围变化:', dateRange);
        },

        /**
         * 搜索订单
         */
        searchOrders() {
            this.loadOrderHistory();
            this.$message.success('订单搜索完成');
        },

        /**
         * 处理订单行点击
         */
        handleOrderRowClick(row) {
            console.log('点击订单行:', row);
        },

        /**
         * 查看订单详情
         */
        viewOrderDetail(row) {
            this.selectedOrder = row;
            this.orderDetailDialogVisible = true;
        },

        /**
         * 处理订单分页变化
         */
        handleOrderPageChange(page) {
            this.orderPagination.page = page;
            this.loadOrderHistory();
        },

        /**
         * 处理订单分页大小变化
         */
        handleOrderPageSizeChange(pageSize) {
            this.orderPagination.pageSize = pageSize;
            this.orderPagination.page = 1;
            this.loadOrderHistory();
        },

        /**
         * 处理图标记录点击
         */
        handleIconRecordClick(record) {
            this.$message.info(`点击了${record.title}，共${record.count}条记录`);
        },

        /**
         * 返回客户列表
         */
        goBack() {
            // 检查是否有历史记录
            if (window.history.length > 1) {
                window.history.back();
            } else {
                // 如果没有历史记录，跳转到客户列表页面
                window.location.href = 'CustomerProfileManager.aspx';
            }
        },

        /**
         * 在新标签页打开
         */
        openInNewTab() {
            window.open(window.location.href, '_blank');
            this.$message.success('已在新标签页打开');
        },

        /**
         * 获取客户类型颜色
         */
        getCustomerTypeColor(type) {
            const colorMap = {
                'VIP客户': 'danger',
                '企业客户': 'warning',
                '个人客户': 'info',
                '普通客户': ''
            };
            return colorMap[type] || '';
        },

        /**
         * 获取活跃水平颜色
         */
        getActivityLevelColor(level) {
            const colorMap = {
                '高度活跃': 'success',
                '中等活跃': 'warning',
                '低度活跃': 'info',
                '不活跃': 'danger'
            };
            return colorMap[level] || 'info';
        },

        /**
         * 获取状态颜色
         */
        getStatusColor(status) {
            const colorMap = {
                '活跃': 'success',
                '待激活': 'warning',
                '已停用': 'danger'
            };
            return colorMap[status] || '';
        },

        /**
         * 获取订单状态颜色
         */
        getOrderStatusColor(status) {
            const colorMap = {
                '已完成': 'success',
                '进行中': 'warning',
                '已取消': 'danger',
                '待处理': 'info'
            };
            return colorMap[status] || '';
        },

        /**
         * 格式化货币
         */
        formatCurrency(value) {
            if (typeof value !== 'number') return '0.00';
            return value.toLocaleString('zh-CN', {
                minimumFractionDigits: 2,
                maximumFractionDigits: 2
            });
        },

        /**
         * 格式化日期
         */
        formatDate(date) {
            if (!date) return '-';
            if (typeof date === 'string') return date;
            return new Date(date).toLocaleDateString('zh-CN');
        },

        /**
         * 格式化日期时间
         */
        formatDateTime(datetime) {
            if (!datetime) return '-';
            if (typeof datetime === 'string') return datetime;
            return new Date(datetime).toLocaleString('zh-CN');
        },

        /**
         * 获取客户姓名首字母
         */
        getCustomerInitial(name) {
            if (!name) return '客';
            // 如果是中文，返回第一个字符
            if (/[\u4e00-\u9fa5]/.test(name)) {
                return name.charAt(0);
            }
            // 如果是英文，返回首字母大写
            return name.charAt(0).toUpperCase();
        },

        /**
         * 根据客户姓名获取头像主题
         */
        getAvatarTheme(name) {
            if (!name) return 'theme-blue';
            
            // 根据姓名的第一个字符的Unicode值来决定主题
            const firstChar = name.charAt(0);
            const charCode = firstChar.charCodeAt(0);
            
            // 使用字符编码的模运算来分配主题，确保同一个名字总是得到相同的主题
            const themes = ['theme-blue', 'theme-green', 'theme-orange', 'theme-purple', 'theme-gold'];
            const themeIndex = charCode % themes.length;
            
            return themes[themeIndex];
        },

        /**
         * 判断是否为新客户（注册时间在30天内）
         */
        isNewCustomer(createTime) {
            if (!createTime) return false;
            
            const now = new Date();
            const customerCreateDate = new Date(createTime);
            const daysDiff = Math.floor((now - customerCreateDate) / (1000 * 60 * 60 * 24));
            
            return daysDiff <= 30;
        },

        /**
         * 编辑客户备注
         */
        editNotes() {
            this.$prompt('请输入客户备注', '编辑备注', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                inputType: 'textarea',
                inputValue: this.customerInfo.notes || '',
                inputPlaceholder: '请输入客户备注信息...'
            }).then(({ value }) => {
                this.customerInfo.notes = value;
                this.customerInfo.noteUpdater = '系统管理员';
                this.customerInfo.noteUpdateTime = new Date().toLocaleString('zh-CN');
                this.$message.success('备注更新成功');
            }).catch(() => {
                // 用户取消操作
            });
        },

        /**
         * 显示客户偏好分析弹窗
         */
        editPreferences() {
            this.preferencesAnalysisDialogVisible = true;
        },

        /**
         * 计算消费排行条形图宽度百分比
         */
        getConsumptionBarWidth(amount) {
            if (!this.consumptionRanking || this.consumptionRanking.length === 0) return 0;
            
            // 找到最大消费金额
            const maxAmount = Math.max(...this.consumptionRanking.map(item => item.amount));
            
            // 计算当前金额占最大金额的百分比，最小宽度为5%
            const percentage = maxAmount > 0 ? (amount / maxAmount) * 100 : 0;
            return Math.max(percentage, 5);
        },

        /**
         * 计算下单排行条形图宽度百分比
         */
        getOrderBarWidth(orderCount) {
            if (!this.orderRanking || this.orderRanking.length === 0) return 0;
            
            // 找到最大下单次数
            const maxCount = Math.max(...this.orderRanking.map(item => item.orderCount));
            
            // 计算当前次数占最大次数的百分比，最小宽度为5%
            const percentage = maxCount > 0 ? (orderCount / maxCount) * 100 : 0;
            return Math.max(percentage, 5);
        },

        /**
         * 切换标签页
         */
        switchTab(tab) {
            this.activeTab = tab;
            this.updateCurrentPagination();
            console.log('切换到标签页:', tab);
        },

        /**
         * 更新当前分页数据
         */
        updateCurrentPagination() {
            switch (this.activeTab) {
                case 'order':
                    this.currentPagination = { ...this.orderPagination };
                    break;
                case 'browse':
                    this.currentPagination = {
                        page: 1,
                        pageSize: 10,
                        total: this.browseHistory.length
                    };
                    break;
                case 'search':
                    this.currentPagination = {
                        page: 1,
                        pageSize: 10,
                        total: this.searchHistory.length
                    };
                    break;
            }
        },

        /**
         * 加载浏览记录
         */
        async loadBrowseHistory() {
            this.browseHistoryLoading = true;
            
            try {
                // 模拟浏览记录数据
                this.browseHistory = [
                    {
                        id: 1,
                        browseDate: '2025-11-01',
                        productName: '韩泰',
                        specification: '225/50R18',
                        pattern: 'K127防爆',
                        viewCount: 5,
                        duration: '¥1800'
                    },
                    {
                        id: 2,
                        browseDate: '2025-10-15',
                        productName: '米其林',
                        specification: '215/50ZR17',
                        pattern: 'K3000 plus',
                        viewCount: 3,
                        duration: '¥950'
                    },
                    {
                        id: 3,
                        browseDate: '2025-09-20',
                        productName: '倍耐力',
                        specification: '215/50R17',
                        pattern: 'V551V',
                        viewCount: 4,
                        duration: '¥3200'
                    },
                    {
                        id: 4,
                        browseDate: '2025-08-05',
                        productName: '韩泰',
                        specification: '225/50R18',
                        pattern: 'K127防爆',
                        viewCount: 2,
                        duration: '¥1800'
                    },
                    {
                        id: 5,
                        browseDate: '2025-07-18',
                        productName: '玲珑',
                        specification: '215/60R17',
                        pattern: 'HP010',
                        viewCount: 3,
                        duration: '¥2100'
                    }
                ];
            } catch (error) {
                console.error('加载浏览记录失败:', error);
                this.$message.error('加载浏览记录失败');
            } finally {
                this.browseHistoryLoading = false;
            }
        },

        /**
         * 加载搜索记录
         */
        async loadSearchHistory() {
            this.searchHistoryLoading = true;
            
            try {
                // 模拟搜索记录数据
                this.searchHistory = [
                    {
                        id: 1,
                        searchDate: '2025-11-01',
                        keyword: '韩泰',
                        category: '225/50R18',
                        resultCount: 'K127防爆',
                        searchCount: 2,
                        lastSearch: '¥1800'
                    },
                    {
                        id: 2,
                        searchDate: '2025-10-15',
                        keyword: '米其林',
                        category: '215/50ZR17',
                        resultCount: 'K3000 plus',
                        searchCount: 1,
                        lastSearch: '¥950'
                    },
                    {
                        id: 3,
                        searchDate: '2025-09-20',
                        keyword: '倍耐力',
                        category: '215/50R17',
                        resultCount: 'V551V',
                        searchCount: 4,
                        lastSearch: '¥3200'
                    },
                    {
                        id: 4,
                        searchDate: '2025-08-05',
                        keyword: '韩泰',
                        category: '225/50R18',
                        resultCount: 'K127防爆',
                        searchCount: 2,
                        lastSearch: '¥1800'
                    },
                    {
                        id: 5,
                        searchDate: '2025-07-18',
                        keyword: '玲珑',
                        category: '215/60R17',
                        resultCount: 'HP010',
                        searchCount: 3,
                        lastSearch: '¥2100'
                    }
                ];
            } catch (error) {
                console.error('加载搜索记录失败:', error);
                this.$message.error('加载搜索记录失败');
            } finally {
                this.searchHistoryLoading = false;
            }
        },

        /**
         * 查看浏览详情
         */
        viewBrowseDetail(row) {
            this.$message.info(`查看浏览详情：${row.productName} (${row.specification})`);
        },

        /**
         * 查看搜索详情
         */
        viewSearchDetail(row) {
            this.$message.info(`查看搜索详情：${row.keyword}`);
        },

        /**
         * 处理分页变化（通用）
         */
        handlePageChange(page) {
            switch (this.activeTab) {
                case 'order':
                    this.handleOrderPageChange(page);
                    break;
                case 'browse':
                    // 浏览记录分页逻辑
                    this.currentPagination.page = page;
                    break;
                case 'search':
                    // 搜索记录分页逻辑
                    this.currentPagination.page = page;
                    break;
            }
        },

        /**
         * 处理分页大小变化（通用）
         */
        handlePageSizeChange(pageSize) {
            switch (this.activeTab) {
                case 'order':
                    this.handleOrderPageSizeChange(pageSize);
                    break;
                case 'browse':
                    // 浏览记录分页大小逻辑
                    this.currentPagination.pageSize = pageSize;
                    this.currentPagination.page = 1;
                    break;
                case 'search':
                    // 搜索记录分页大小逻辑
                    this.currentPagination.pageSize = pageSize;
                    this.currentPagination.page = 1;
                    break;
            }
        },

        /**
         * 重置筛选条件
         */
        resetFilter() {
            this.filterForm = {
                dateRange: null,
                brand: '',
                specification: ''
            };
            this.$message.success('筛选条件已重置');
        },

        /**
         * 应用筛选条件
         */
        applyFilter() {
            this.showFilterDialog = false;
            // 根据当前标签页应用不同的筛选逻辑
            switch (this.activeTab) {
                case 'order':
                    this.loadOrderHistory();
                    break;
                case 'browse':
                    this.loadBrowseHistory();
                    break;
                case 'search':
                    this.loadSearchHistory();
                    break;
            }
            this.$message.success('筛选条件已应用');
        }
    },

    // 监听窗口大小变化，重新调整图表
    beforeDestroy() {
        if (this.trendChart) {
            this.trendChart.dispose();
        }
        if (this.brandChart) {
            this.brandChart.dispose();
        }
        
        window.removeEventListener('resize', this.handleResize);
    },

    created() {
        // 监听窗口大小变化
        this.handleResize = () => {
            if (this.trendChart) {
                this.trendChart.resize();
            }
            if (this.brandChart) {
                this.brandChart.resize();
            }
        };
        
        window.addEventListener('resize', this.handleResize);
    }
});