// Vue应用主文件
new Vue({
    el: '#app',
    data: {
        // 加载状态
        loading: false,
        kpiLoading: true, // KPI专用加载状态
        tableLoading: false,
        warehouseLoading: false,
        saveLoading: false,

        // iframe检测
        isInIframe: false,

        // KPI数据
        kpiData: [],

        // 图表数据
        chartData: [],

        // 表格数据
        tableData: [],

        // 仓库数据
        warehouseData: [],

        // 排行榜数据配置
        rankingConfig: {
            apiEnabled: true,
            refreshInterval: 300000, // 5分钟自动刷新
            enableAutoRefresh: false, // 默认关闭自动刷新
            limit: 10 // 排行榜显示条数
        },

        // 排行榜初始数据
        initialRankingData: null,

        // 表格配置
        tableColumns: CONSTANTS.TABLE_COLUMNS.CUSTOMER,

        // 分页配置
        pagination: {
            currentPage: 1,
            pageSize: 10,
            total: 0,
            pageSizes: [10, 20, 50, 100]
        },


        // 对话框状态
        dialogVisible: false,
        dialogTitle: '新增客户',
        isEdit: false,

        // 客户表单数据
        customerForm: {
            id: null,
            name: '',
            contact: '',
            phone: '',
            email: '',
            customerType: '',
            status: '活跃',
            remark: ''
        },

        // 表单验证规则
        formRules: {
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
                { pattern: CONSTANTS.REGEX.PHONE, message: '请输入正确的手机号码', trigger: 'blur' }
            ],
            email: [
                { required: true, message: '请输入邮箱地址', trigger: 'blur' },
                { pattern: CONSTANTS.REGEX.EMAIL, message: '请输入正确的邮箱地址', trigger: 'blur' }
            ],
            customerType: [
                { required: true, message: '请选择客户类型', trigger: 'change' }
            ],
            status: [
                { required: true, message: '请选择客户状态', trigger: 'change' }
            ]
        },


        // 筛选条件
        customerFilters: {
            customerName: '',
            customerCode: '',
            phoneNumber: '',
            warehouse: '',
            customerType: '',
            activeLevel: '',
            preferredBrand: '',
            preferredSpec: '',
            valueLevel: ''
        },

    },

    computed: {
        // 可以在这里添加其他计算属性
    },


    async mounted() {
        // 检测是否在iframe中
        this.checkIfInIframe();

        // 初始化应用
        await this.initializeApp();
    },

    methods: {
        /**
         * 检测当前页面是否在iframe中
         */
        checkIfInIframe() {
            try {
                // 方法1：检查window.self !== window.top
                this.isInIframe = window.self !== window.top;

                // 方法2：作为备用检查，防止跨域限制
                if (!this.isInIframe) {
                    try {
                        this.isInIframe = window.frameElement !== null;
                    } catch (e) {
                        // 如果访问frameElement抛出异常，说明可能在跨域iframe中
                        this.isInIframe = true;
                    }
                }

                console.log('iframe检测结果:', this.isInIframe);
            } catch (error) {
                console.error('iframe检测失败:', error);
                // 出错时默认显示按钮，保证功能可用
                this.isInIframe = true;
            }
        },

        /**
         * 初始化应用
         */
        async initializeApp() {
            this.loading = true;

            try {
                // 并行加载数据
                await Promise.all([
                    this.loadKpiData(),
                    this.loadChartData(),
                    this.loadTableData(),
                    this.loadWarehouseData(),
                    this.loadRankingData()
                ]);

                this.$message.success('数据加载完成');
            } catch (error) {
                console.error('初始化失败:', error);
                this.$message.error('数据加载失败，请刷新页面重试');
            } finally {
                this.loading = false;
            }
        },

        /**
         * 加载KPI数据 - 对接.NET后台
         */
        async loadKpiData() {

            // 设置KPI加载状态
            this.kpiLoading = true;

            try {
                // 模拟网络延迟，让用户能看到骨架屏效果
                await new Promise(resolve => setTimeout(resolve, 800));

                // 尝试从.NET后台获取数据
                const response = await API.statistics.kpi();


                if (response.success) {
                    this.kpiData = response.data;
                    this.$message.success('KPI数据加载成功');
                } else {
                    console.warn('⚠️ [APP] API返回失败，使用模拟数据');
                    this.kpiData = MockData.kpiData;
                    this.$message.warning('KPI数据加载失败，使用模拟数据: ' + response.message);
                }

            } catch (error) {
                console.error('❌ [APP] KPI数据加载异常:', error);

                // 显示详细错误信息
                let errorMessage = 'KPI数据加载失败';
                if (error.message) {
                    errorMessage += ': ' + error.message;
                }

                // 使用模拟数据作为后备
                this.kpiData = MockData.kpiData;
                console.log('🔄 [APP] 使用模拟数据作为后备');

                // 显示错误提示
                this.$message.error(errorMessage);

                // 在控制台输出调试信息
                console.group('🔍 [DEBUG] KPI加载失败详情');
                console.log('错误类型:', error.name);
                console.log('错误消息:', error.message);
                console.log('错误堆栈:', error.stack);
                console.log('当前API配置:', CONSTANTS.API);
                console.groupEnd();
            } finally {
                // 确保加载状态被重置
                this.kpiLoading = false;
            }
        },

        /**
         * 加载图表数据
         */
        async loadChartData(period = 'month') {
            try {
                // 使用模拟数据，根据时间周期获取不同数据
                this.chartData = MockData.getChartData(period);

                // 如果需要从API获取数据，可以使用以下代码：
                // const response = await API.statistics.chart(period);
                // if (response.success) {
                //   this.chartData = response.data;
                // }
            } catch (error) {
                console.error('加载图表数据失败:', error);
            }
        },

        /**
         * 加载表格数据
         */
        async loadTableData() {
            this.tableLoading = true;

            try {
                //// 使用模拟数据，应用筛选条件
                //const result = MockData.getPagedCustomers(
                //    this.pagination.currentPage,
                //    this.pagination.pageSize,
                //    this.customerFilters
                //);

                //this.tableData = result.data;
                //this.pagination.total = result.total;
                // 如果需要从API获取数据，可以使用以下代码：
                 const response = await API.customers.list({
                   page: this.pagination.currentPage,
                   pageSize: this.pagination.pageSize,
                   paramData: this.customerFilters
                 });
                console.log('this.customerFilters', response.data.list, response.data.total)
                 if (response.success) {
                   this.tableData = response.data.list;
                   this.pagination.total = response.data.total;
                }
            } catch (error) {
                console.error('加载表格数据失败:', error);
                this.$message.error('加载客户数据失败');
            } finally {
                this.tableLoading = false;
            }
        },

        /**
         * 加载仓库数据
         */
        async loadWarehouseData() {
            this.warehouseLoading = true;

            try {
                // 使用模拟数据
                this.warehouseData = MockData.getWarehouseData();

                // 如果需要从API获取数据，可以使用以下代码：
                // const response = await API.warehouse.list();
                // if (response.success) {
                //   this.warehouseData = response.data;
                // }
            } catch (error) {
                console.error('加载仓库数据失败:', error);
                this.$message.error('加载仓库数据失败');
            } finally {
                this.warehouseLoading = false;
            }
        },

        /**
         * 加载排行榜数据
         */
        async loadRankingData() {

            try {
                // 如果API被禁用，使用默认数据
                if (!this.rankingConfig.apiEnabled) {
                    this.initialRankingData = null; // 让组件使用内置默认数据
                    return;
                }

                // 尝试从API获取排行榜数据
                const [activeResponse, valueResponse, productResponse] = await Promise.allSettled([
                    API.statistics.rankings('GetRankingUserData', { limit: this.rankingConfig.limit }),
                    API.statistics.rankings('GetRankingUserConsumptionData', { limit: this.rankingConfig.limit }),
                    API.statistics.rankings('GetRankingUserHotsellingData', { limit: this.rankingConfig.limit })
                ]);

                // 处理API响应
                const rankingData = {
                    updateTime: new Date().toLocaleString()
                };

                // 处理用户活跃排行数据
                if (activeResponse.status === 'fulfilled' && activeResponse.value.success) {
                    rankingData.userActiveRanking = activeResponse.value.data.rankings || activeResponse.value.data;

                } else {
                    console.warn('⚠️ [APP] 用户活跃排行数据获取失败，将使用默认数据');
                }

                //// 处理用户价值排行数据
                //if (valueResponse.status === 'fulfilled' && valueResponse.value.success) {
                //    rankingData.userValueRanking = valueResponse.value.data.rankings || valueResponse.value.data;
                //    console.log('✅ [APP] 用户价值排行数据获取成功');
                //} else {
                //    console.warn('⚠️ [APP] 用户价值排行数据获取失败，将使用默认数据');
                //}

                //// 处理产品销售排行数据
                //if (productResponse.status === 'fulfilled' && productResponse.value.success) {
                //    rankingData.productRanking = productResponse.value.data.rankings || productResponse.value.data;
                //    console.log('✅ [APP] 产品销售排行数据获取成功');
                //} else {
                //    console.warn('⚠️ [APP] 产品销售排行数据获取失败，将使用默认数据');
                //}

                // 设置初始数据
                this.initialRankingData = rankingData;

            } catch (error) {
                console.error('❌ [APP] 加载排行榜数据失败:', error);
                // 发生错误时，让组件使用默认数据
                this.initialRankingData = null;
            }
        },

        /**
         * 刷新图表数据
         */
        refreshChartData(period = 'month') {
            this.loadChartData(period);
            this.$message.success('图表数据已刷新');
        },

        /**
         * 处理图表时间周期变化
         */
        handleChartPeriodChange(period) {
            this.loadChartData(period);

            // 显示相应的提示信息
            const periodNames = {
                month: '本月',
                quarter: '本季度',
                year: '本年度'
            };
            this.$message.success(`已切换到${periodNames[period]}数据`);
        },

        /**
         * 刷新表格数据
         */
        refreshTableData() {
            this.loadTableData();
        },

        /**
         * 刷新仓库数据
         */
        refreshWarehouseData() {
            this.loadWarehouseData();
            this.$message.success('仓库数据已刷新');
        },

        /**
         * 查看仓库详情
         */
        handleViewWarehouseDetail(warehouse) {
            this.$alert(`
        <div style="text-align: left;">
          <h4>${warehouse.name} - 详细信息</h4>
          <p><strong>位置：</strong>${warehouse.location}</p>
          <p><strong>负责人：</strong>${warehouse.manager}</p>
          <p><strong>联系电话：</strong>${warehouse.phone}</p>
          <p><strong>运营状态：</strong>${warehouse.status}</p>
          <hr style="margin: 15px 0;">
          <p><strong>出库量：</strong>${warehouse.shipmentVolume.toLocaleString()} 件</p>
          <p><strong>注册用户：</strong>${warehouse.registeredUsers.toLocaleString()} 人</p>
          <p><strong>人均出库量：</strong>${warehouse.avgShipmentPerUser}</p>
          <p><strong>成交率：</strong>${warehouse.conversionRate}</p>
        </div>
      `, `${warehouse.name} 详细信息`, {
                confirmButtonText: '确定',
                dangerouslyUseHTMLString: true,
                customClass: 'warehouse-detail-dialog'
            });
        },

        /**
         * 新增客户
         */
        handleAddCustomer() {
            this.dialogTitle = '新增客户';
            this.isEdit = false;
            this.resetForm();
            this.dialogVisible = true;
        },

        /**
         * 编辑客户
         */
        handleEditCustomer(row) {
            this.dialogTitle = '编辑客户';
            this.isEdit = true;
            this.customerForm = { ...row };
            this.dialogVisible = true;
        },

        /**
         * 删除客户
         */
        async handleDeleteCustomer(row) {
            try {
                await this.$confirm('确定要删除这个客户吗？', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'warning'
                });

                // 模拟删除操作
                const index = this.tableData.findIndex(item => item.id === row.id);
                if (index > -1) {
                    this.tableData.splice(index, 1);
                    this.pagination.total--;
                    this.$message.success('删除成功');
                }

                // 实际项目中调用API删除
                // const response = await API.customers.delete(row.id);
                // if (response.success) {
                //   this.$message.success('删除成功');
                //   this.loadTableData();
                // }
            } catch (error) {
                if (error !== 'cancel') {
                    console.error('删除失败:', error);
                    this.$message.error('删除失败');
                }
            }
        },

        /**
         * 处理表格行双击事件
         */
        handleRowDoubleClick(row, column, event) {
            try {
                console.log('双击行数据:', row);
                
                // 检查是否有客户ID
                if (!row.Id) {
                    this.$message.warning('客户ID不存在，无法打开详情页面');
                    return;
                }

                // 构建详情页面URL - 使用正确的相对路径
                const detailUrl = `CustomerProfileManagerDetail.aspx?id=${row.Id}`;
                // 在新窗口中打开详情页面
                const newWindow = window.open(detailUrl, '_blank');
                
                // 检查窗口是否成功打开
                if (!newWindow) {
                    this.$message.error('无法打开新窗口，请检查浏览器弹窗设置');
                    return;
                }
                
                // 显示成功提示
                this.$message.success(`正在打开客户 "${row.name || row.Id}" 的详情页面`);
                
                console.log('双击打开客户详情:', {
                    customerId: row.Id,
                    customerName: row.name,
                    url: detailUrl
                });
                
            } catch (error) {
                console.error('打开客户详情页面失败:', error);
                this.$message.error('打开详情页面失败，请稍后重试');
            }
        },

        /**
         * 排序变化处理
         */
        handleSortChange(sort) {
            console.log('排序变化:', sort);
            // 可以在这里实现排序逻辑
        },

        /**
         * 页面变化处理
         */
        handlePageChange(page) {
            this.pagination.currentPage = page;
            this.loadTableData();
        },

        /**
         * 页面大小变化处理
         */
        handlePageSizeChange(size) {
            this.pagination.pageSize = size;
            this.pagination.currentPage = 1;
            this.loadTableData();
        },

        /**
         * 保存客户
         */
        async saveCustomer() {
            try {
                // 表单验证
                await this.$refs.customerForm.validate();

                this.saveLoading = true;

                if (this.isEdit) {
                    // 编辑模式
                    const index = this.tableData.findIndex(item => item.id === this.customerForm.id);
                    if (index > -1) {
                        this.tableData.splice(index, 1, { ...this.customerForm });
                        this.$message.success('更新成功');
                    }

                    // 实际项目中调用API更新
                    // const response = await API.customers.update(this.customerForm.id, this.customerForm);
                    // if (response.success) {
                    //   this.$message.success('更新成功');
                    //   this.loadTableData();
                    // }
                } else {
                    // 新增模式
                    const newCustomer = {
                        ...this.customerForm,
                        id: Date.now(), // 模拟ID
                        createTime: new Date().toISOString().split('T')[0]
                    };
                    this.tableData.unshift(newCustomer);
                    this.pagination.total++;
                    this.$message.success('新增成功');

                    // 实际项目中调用API创建
                    // const response = await API.customers.create(this.customerForm);
                    // if (response.success) {
                    //   this.$message.success('新增成功');
                    //   this.loadTableData();
                    // }
                }

                this.dialogVisible = false;
            } catch (error) {
                if (error !== false) { // 表单验证失败时error为false
                    console.error('保存失败:', error);
                    this.$message.error('保存失败');
                }
            } finally {
                this.saveLoading = false;
            }
        },

        /**
         * 重置表单
         */
        resetForm() {
            this.customerForm = {
                id: null,
                name: '',
                contact: '',
                phone: '',
                email: '',
                customerType: '',
                status: '活跃',
                remark: ''
            };

            if (this.$refs.customerForm) {
                this.$refs.customerForm.resetFields();
            }
        },


        /**
         * 格式化货币
         */
        formatCurrency(value) {
            return Formatter.formatCurrency(value);
        },

        /**
         * 格式化日期
         */
        formatDate(date) {
            return Formatter.formatDate(date);
        },

        /**
         * 显示消息
         */
        showMessage(message, type = 'info') {
            this.$message({
                message,
                type,
                duration: CONSTANTS.MESSAGE[`${type.toUpperCase()}_DURATION`] || 3000
            });
        },

        /**
         * 处理筛选条件变化
         */
        handleCustomerFilterChange(filters) {
            this.customerFilters = { ...filters };
            this.pagination.currentPage = 1; // 重置到第一页
            this.loadTableData();
        },

        /**
         * 处理筛选搜索
         */
        handleCustomerSearch(filters) {
            this.customerFilters = { ...filters };
            this.pagination.currentPage = 1; // 重置到第一页
            this.loadTableData();
            this.$message.success('搜索完成');
        },

        /**
         * 处理筛选刷新
         */
        handleCustomerFilterRefresh() {
            this.loadTableData();
            this.$message.success('数据已刷新');
        },

        /**
         * 处理筛选导出
         */
        handleCustomerFilterExport(filters) {
            // 获取筛选后的所有数据
            const result = MockData.getPagedCustomers(1, 10000, filters);

            // 模拟导出功能
            const csvContent = this.generateCSV(result.data);
            this.downloadCSV(csvContent, '客户数据导出.csv');

            this.$message.success(`已导出 ${result.total} 条客户数据`);
        },

        /**
         * 生成CSV内容
         */
        generateCSV(data) {
            const headers = [
                'ID', '公司名称', '客户类型', '联系人', '手机号', '所属仓库',
                '最近下单', '近三个月下单数量', '近三个月消费次数', '近三个月营业次数',
                '活跃水平', '偏好品牌', '偏好规格', '价值水平', '最后访问'
            ];

            const csvRows = [headers.join(',')];

            data.forEach(customer => {
                const row = [
                    customer.id,
                    `"${customer.name}"`,
                    customer.customerType,
                    customer.contact,
                    customer.phone,
                    customer.warehouse || '',
                    customer.lastOrder || '',
                    customer.recentThreeMonthsOrders || 0,
                    customer.recentThreeMonthsConsumption || 0,
                    customer.recentThreeMonthsBusiness || 0,
                    customer.activeLevel || '',
                    customer.preferredBrand || '',
                    customer.preferredSpec || '',
                    customer.valueLevel || '',
                    customer.lastAccess || ''
                ];
                csvRows.push(row.join(','));
            });

            return csvRows.join('\n');
        },

        /**
         * 下载CSV文件
         */
        downloadCSV(content, filename) {
            const blob = new Blob(['\uFEFF' + content], { type: 'text/csv;charset=utf-8;' });
            const link = document.createElement('a');

            if (link.download !== undefined) {
                const url = URL.createObjectURL(blob);
                link.setAttribute('href', url);
                link.setAttribute('download', filename);
                link.style.visibility = 'hidden';
                document.body.appendChild(link);
                link.click();
                document.body.removeChild(link);
            }
        },

        /**
         * 在新标签页打开当前页面
         */
        openInNewTab() {
            window.open(window.location.href, '_blank');
            this.$message.success('已在新标签页打开');
        },

        /**
         * 处理排行榜刷新事件
         * @param {object} event 刷新事件对象
         */
        handleRankingRefresh(event) {

            if (event.type === 'all') {
                this.$message.success('排行榜数据已刷新');
            } else {
                const typeNames = {
                    active: '用户活跃排行',
                    value: '用户价值排行',
                    product: '产品销售排行'
                };
                this.$message.success(`${typeNames[event.type]}数据已刷新`);
            }
        },

        /**
         * 处理排行榜标签页切换事件
         * @param {object} event 标签页切换事件对象
         */
        handleRankingTabChange(event) {

            const typeNames = {
                active: '用户活跃排行',
                value: '用户价值排行',
                product: '产品销售排行'
            };

            // 可以在这里添加统计或其他逻辑
            console.log(`用户查看了${typeNames[event.activeTab]}`);
        }
    }
});