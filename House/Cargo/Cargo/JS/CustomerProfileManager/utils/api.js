// API接口封装
window.API = {

    // 基础配置
    config: {
        baseURL: CONSTANTS.API.BASE_URL,
        aspxEndpoint: CONSTANTS.API.ASPX_ENDPOINT,
        timeout: CONSTANTS.API.TIMEOUT,
        retryCount: CONSTANTS.API.RETRY_COUNT
    },

    /**
     * HTTP请求封装
     * @param {object} options 请求配置
     * @returns {Promise} 请求Promise
     */
    async request(options) {
        const defaultOptions = {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
            timeout: this.config.timeout
        };

        const config = { ...defaultOptions, ...options };

        // 构建完整URL
        if (config.url && !config.url.startsWith('http')) {
            config.url = this.config.baseURL + config.url;
        }

        // 添加认证头
        const token = this.getToken();
        if (token) {
            config.headers.Authorization = `Bearer ${token}`;
        }

        try {
            // 使用axios发送请求
            const response = await axios(config);
            return this.handleResponse(response);
        } catch (error) {
            return this.handleError(error);
        }
    },

    /**
     * GET请求
     * @param {string} url 请求URL
     * @param {object} params 查询参数
     * @returns {Promise} 请求Promise
     */
    get(url, params = {}) {
        return this.request({
            method: 'GET',
            url,
            params
        });
    },

    /**
     * POST请求
     * @param {string} url 请求URL
     * @param {object} data 请求数据
     * @returns {Promise} 请求Promise
     */
    post(url, data = {}) {
        return this.request({
            method: 'POST',
            url,
            data
        });
    },

    /**
     * PUT请求
     * @param {string} url 请求URL
     * @param {object} data 请求数据
     * @returns {Promise} 请求Promise
     */
    put(url, data = {}) {
        return this.request({
            method: 'PUT',
            url,
            data
        });
    },

    /**
     * DELETE请求
     * @param {string} url 请求URL
     * @returns {Promise} 请求Promise
     */
    delete(url) {
        return this.request({
            method: 'DELETE',
            url
        });
    },

    /**
     * 处理响应
     * @param {object} response axios响应对象
     * @returns {object} 处理后的响应数据
     */
    handleResponse(response) {
        const { data, status } = response;

        if (status >= 200 && status < 300) {
            return {
                success: true,
                data: data.data || data,
                message: data.message || '操作成功',
                code: data.code || status
            };
        }

        throw new Error(`HTTP ${status}: ${response.statusText}`);
    },

    /**
     * 处理错误
     * @param {object} error 错误对象
     * @returns {object} 处理后的错误信息
     */
    handleError(error) {
        let errorInfo = {
            success: false,
            data: null,
            message: '请求失败',
            code: 'UNKNOWN_ERROR'
        };

        if (error.response) {
            // 服务器响应错误
            const { status, data } = error.response;
            errorInfo.code = status;
            errorInfo.message = data.message || CONSTANTS.ERROR_MESSAGES.SERVER_ERROR;

            switch (status) {
                case 401:
                    errorInfo.code = CONSTANTS.ERROR_CODES.UNAUTHORIZED;
                    errorInfo.message = CONSTANTS.ERROR_MESSAGES.UNAUTHORIZED;
                    this.handleUnauthorized();
                    break;
                case 403:
                    errorInfo.code = CONSTANTS.ERROR_CODES.FORBIDDEN;
                    errorInfo.message = CONSTANTS.ERROR_MESSAGES.FORBIDDEN;
                    break;
                case 404:
                    errorInfo.code = CONSTANTS.ERROR_CODES.NOT_FOUND;
                    errorInfo.message = CONSTANTS.ERROR_MESSAGES.NOT_FOUND;
                    break;
                case 422:
                    errorInfo.code = CONSTANTS.ERROR_CODES.VALIDATION_ERROR;
                    errorInfo.message = data.message || CONSTANTS.ERROR_MESSAGES.VALIDATION_ERROR;
                    break;
                default:
                    errorInfo.message = CONSTANTS.ERROR_MESSAGES.SERVER_ERROR;
            }
        } else if (error.request) {
            // 网络错误
            errorInfo.code = CONSTANTS.ERROR_CODES.NETWORK_ERROR;
            errorInfo.message = CONSTANTS.ERROR_MESSAGES.NETWORK_ERROR;
        } else if (error.code === 'ECONNABORTED') {
            // 超时错误
            errorInfo.code = CONSTANTS.ERROR_CODES.TIMEOUT;
            errorInfo.message = CONSTANTS.ERROR_MESSAGES.TIMEOUT;
        }

        console.error('API Error:', error);
        return errorInfo;
    },

    /**
     * 处理未授权错误
     */
    handleUnauthorized() {
        this.removeToken();
        // 可以在这里添加跳转到登录页面的逻辑
        console.warn('用户未授权，请重新登录');
    },

    /**
     * 获取认证令牌
     * @returns {string|null} 认证令牌
     */
    getToken() {
        return localStorage.getItem('access_token');
    },

    /**
     * 设置认证令牌
     * @param {string} token 认证令牌
     */
    setToken(token) {
        localStorage.setItem('access_token', token);
    },

    /**
     * 移除认证令牌
     */
    removeToken() {
        localStorage.removeItem('access_token');
    },

    // 客户相关接口
    customers: {
        /**
         * 获取客户列表
         * @param {object} params 查询参数
         * @returns {Promise} 客户列表
         */
        list(params = {}) {
            return API.get(`${CONSTANTS.API.BASE_URL + CONSTANTS.API.ASPX_ENDPOINT}?method=getPagedCustomers`, params);
        },

        /**
         * 获取客户详情
         * @param {number} id 客户ID
         * @returns {Promise} 客户详情
         */
        detail(id) {
            return API.get(`/customers/${id}`);
        },

        /**
         * 获取客户详情 - 详情页面专用
         * @param {number} id 客户ID
         * @returns {Promise} 客户详情
         */
        getDetail(id) {
            return API.get(`${CONSTANTS.API.BASE_URL + CONSTANTS.API.ASPX_ENDPOINT}?method=getCustomerDetail&id=${id}`);
        },

        /**
         * 获取客户消费趋势数据
         * @param {number} id 客户ID
         * @param {string} period 时间周期 (week|month|quarter|year)
         * @returns {Promise} 消费趋势数据
         */
        getConsumptionTrends(id, period = 'month') {
            return API.get(`${CONSTANTS.API.BASE_URL + CONSTANTS.API.ASPX_ENDPOINT}?method=getConsumptionTrends&id=${id}&period=${period}`);
        },

        /**
         * 获取客户品牌偏好数据
         * @param {number} id 客户ID
         * @returns {Promise} 品牌偏好数据
         */
        getBrandPreferences(id) {
            return API.get(`${CONSTANTS.API.BASE_URL + CONSTANTS.API.ASPX_ENDPOINT}?method=getBrandPreferences&id=${id}`);
        },

        /**
         * 获取客户订单记录
         * @param {number} id 客户ID
         * @param {object} params 查询参数
         * @returns {Promise} 订单记录
         */
        getOrderHistory(id, params = {}) {
            const queryParams = new URLSearchParams({
                method: 'getOrderHistory',
                id: id,
                ...params
            });
            return API.get(`${CONSTANTS.API.BASE_URL + CONSTANTS.API.ASPX_ENDPOINT}?${queryParams.toString()}`);
        },

        /**
         * 获取客户各类记录
         * @param {number} id 客户ID
         * @param {string} type 记录类型 (all|visit|consult|complaint|return|review|favorite)
         * @returns {Promise} 记录数据
         */
        getRecords(id, type = 'all') {
            return API.get(`${CONSTANTS.API.BASE_URL + CONSTANTS.API.ASPX_ENDPOINT}?method=getCustomerRecords&id=${id}&type=${type}`);
        },

        /**
         * 创建客户
         * @param {object} data 客户数据
         * @returns {Promise} 创建结果
         */
        create(data) {
            return API.post('/customers', data);
        },

        /**
         * 更新客户
         * @param {number} id 客户ID
         * @param {object} data 客户数据
         * @returns {Promise} 更新结果
         */
        update(id, data) {
            return API.put(`/customers/${id}`, data);
        },

        /**
         * 删除客户
         * @param {number} id 客户ID
         * @returns {Promise} 删除结果
         */
        delete(id) {
            return API.delete(`/customers/${id}`);
        },

        /**
         * 批量删除客户
         * @param {array} ids 客户ID数组
         * @returns {Promise} 删除结果
         */
        batchDelete(ids) {
            return API.post('/customers/batch-delete', { ids });
        },

        /**
         * 导出客户数据
         * @param {object} params 导出参数
         * @returns {Promise} 导出结果
         */
        export(params = {}) {
            return API.get('/customers/export', params);
        }
    },

    // 统计数据相关接口
    statistics: {
        /**
         * 获取KPI数据 - 对接.NET后台
         * @returns {Promise} KPI数据
         */
        async kpi() {

            try {
                // 构建.NET后台请求URL
                const url = `${CONSTANTS.API.BASE_URL + CONSTANTS.API.ASPX_ENDPOINT}?method=${CONSTANTS.API.KPI_METHOD}`;

                // 发送请求
                const response = await API.request({
                    method: 'GET',
                    url: url,
                    headers: {
                        'Content-Type': 'application/json',
                        'Accept': 'application/json'
                    }
                });


                // 处理.NET后台响应数据
                return API.processNetResponse(response, 'KPI');

            } catch (error) {
                console.error('❌ [KPI API] 请求失败:', error);
                throw error;
            }
        },

        /**
         * 获取图表数据
         * @param {string} type 图表类型
         * @param {object} params 查询参数
         * @returns {Promise} 图表数据
         */
        chart(type, params = {}) {
            return API.get(`/statistics/chart/${type}`, params);
        },

        /**
         * 获取趋势数据
         * @param {object} params 查询参数
         * @returns {Promise} 趋势数据
         */
        trend(params = {}) {
            return API.get('/statistics/trend', params);
        },

        /**
         * 获取排行榜数据
         * @param {string} type 排行榜类型 (active|value|product)
         * @param {object} params 查询参数
         * @returns {Promise} 排行榜数据
         */
        rankings(type, params = {}) {
            return API.get(`${CONSTANTS.API.BASE_URL + CONSTANTS.API.ASPX_ENDPOINT}?method=${type}`, params);
            //return API.get(`/statistics/rankings/${type}`, params);
        }
    },
    // 仓库相关接口
    warehouses: {
        /**
         * 获取仓库列表
         * @param {object} params 查询参数
         * @returns {Promise} 仓库列表
         */
        list(params = {}) {
            return API.get(`${CONSTANTS.API.ASPX_ENDPOINT}?method=CargoPermisionHouse`, params);
        },

        /**
         * 获取仓库详情
         * @param {number} id 仓库ID
         * @returns {Promise} 仓库详情
         */
        detail(id) {
            return API.get(`/warehouses/${id}`);
        },

        /**
         * 获取仓库统计数据
         * @returns {Promise} 仓库统计数据
         */
        statistics() {
            return API.get('/warehouses/statistics');
        }
    },
    // 品牌相关接口
    brands: {
        /**
         * 获取仓库列表
         * @param {object} params 查询参数
         * @returns {Promise} 仓库列表
         */
        list(params = {}) {
            return API.get(`../Product/productApi.aspx?method=QueryALLOneProductType&PID=1`, params);
        },

        /**
         * 获取仓库详情
         * @param {number} id 仓库ID
         * @returns {Promise} 仓库详情
         */
        detail(id) {
            return API.get(`/warehouses/${id}`);
        },

        /**
         * 获取仓库统计数据
         * @returns {Promise} 仓库统计数据
         */
        statistics() {
            return API.get('/warehouses/statistics');
        }
    },

    // 文件上传相关接口
    upload: {
        /**
         * 上传文件
         * @param {File} file 文件对象
         * @param {function} onProgress 进度回调
         * @returns {Promise} 上传结果
         */
        file(file, onProgress) {
            const formData = new FormData();
            formData.append('file', file);

            return API.request({
                method: 'POST',
                url: '/upload',
                data: formData,
                headers: {
                    'Content-Type': 'multipart/form-data'
                },
                onUploadProgress: (progressEvent) => {
                    if (onProgress) {
                        const percentCompleted = Math.round(
                            (progressEvent.loaded * 100) / progressEvent.total
                        );
                        onProgress(percentCompleted);
                    }
                }
            });
        },

        /**
         * 上传头像
         * @param {File} file 头像文件
         * @returns {Promise} 上传结果
         */
        avatar(file) {
            return this.file(file);
        }
    },

    /**
     * 处理.NET后台响应数据
     * @param {object} response 原始响应
     * @param {string} dataType 数据类型标识
     * @returns {object} 标准化响应
     */
    processNetResponse(response, dataType) {

        try {
            let processedData = response;

            // 如果响应包含data字段，提取数据
            if (response.data) {
                processedData = response.data;
            }

            // 根据数据类型进行特殊处理
            if (dataType === 'KPI') {
                return this.processKpiData(processedData);
            }

            return {
                success: true,
                data: processedData,
                message: '数据获取成功'
            };

        } catch (error) {
            console.error(`❌ [${dataType}] 数据处理失败:`, error);
            return {
                success: false,
                data: null,
                message: '数据处理失败',
                error: error.message
            };
        }
    },

    /**
     * 处理KPI数据格式转换
     * @param {any} rawData .NET后台返回的原始数据
     * @returns {object} 标准化KPI数据
     */
    processKpiData(rawData) {

        try {
            // 如果已经是标准格式的数组，直接返回
            if (Array.isArray(rawData) && rawData.length > 0 && rawData[0].id) {
                return {
                    success: true,
                    data: rawData,
                    message: 'KPI数据获取成功'
                };
            }

            // 如果是.NET后台的特殊格式，进行转换
            let kpiData = [];

            // 示例：假设.NET返回的是对象格式
            if (typeof rawData === 'object' && !Array.isArray(rawData)) {
                // 根据实际.NET后台返回格式进行转换
                // 这里提供一个通用的转换示例
                kpiData = [
                    {
                        id: 'totalUsers',
                        title: '平台用户总数',
                        value: rawData.TotalUsers || rawData.totalUsers || '0',
                        subtitle: rawData.UserGrowth || '数据获取中',
                        trend: rawData.UserTrend || 'neutral',
                        trendValue: rawData.UserTrendValue || '0%',
                        icon: 'el-icon-user',
                        color: 'success'
                    },
                    {
                        id: 'vipUsers',
                        title: 'VIP客户数',
                        value: rawData.VipUsers || rawData.vipUsers || '0',
                        subtitle: rawData.VipGrowth || '数据获取中',
                        trend: rawData.VipTrend || 'neutral',
                        trendValue: rawData.VipTrendValue || '0%',
                        icon: 'el-icon-star-on',
                        color: 'success'
                    },
                    {
                        id: 'warehouses',
                        title: '仓库数',
                        value: rawData.Warehouses || rawData.warehouses || '0',
                        subtitle: rawData.WarehouseChange || '数据获取中',
                        trend: rawData.WarehouseTrend || 'neutral',
                        trendValue: rawData.WarehouseTrendValue || '0%',
                        icon: 'el-icon-house',
                        color: 'info'
                    },
                    {
                        id: 'avgShipment',
                        title: '仓库平均出货量',
                        value: rawData.AvgShipment || rawData.avgShipment || '0',
                        subtitle: rawData.ShipmentChange || '数据获取中',
                        trend: rawData.ShipmentTrend || 'neutral',
                        trendValue: rawData.ShipmentTrendValue || '0%',
                        icon: 'el-icon-truck',
                        color: 'warning'
                    }
                ];
            }


            return {
                success: true,
                data: kpiData,
                message: 'KPI数据转换成功'
            };

        } catch (error) {
            console.error('❌ [KPI] 数据转换失败:', error);

            // 返回默认数据以防止页面崩溃
            return {
                success: false,
                data: [
                    {
                        id: 'error',
                        title: '数据加载失败',
                        value: '请检查网络连接',
                        subtitle: error.message,
                        trend: 'neutral',
                        trendValue: '',
                        icon: 'el-icon-warning',
                        color: 'danger'
                    }
                ],
                message: '数据转换失败，使用默认数据',
                error: error.message
            };
        }
    }
};