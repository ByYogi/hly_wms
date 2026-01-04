// 客户筛选栏组件 - 增强版
Vue.component('customer-filter-bar', {
    props: {
        filters: {
            type: Object,
            default: () => ({})
        }
    },
    template: `
    <div class="customer-filter-bar">
      <!-- 第一行筛选控件 -->
      <div class="filter-row">
        <!-- 客户名称 -->
        <div class="filter-item">
          <label class="filter-label">客户名称</label>
          <el-input
            v-model="localFilters.customerName"
            placeholder="请输入客户名称"
            clearable
            @input="handleFilterChange"
            @clear="handleFilterChange">
          </el-input>
        </div>
        
        <!-- 客户编码 -->
        <div class="filter-item">
          <label class="filter-label">客户编码</label>
          <el-input
            v-model="localFilters.customerCode"
            placeholder="请输入客户编码"
            clearable
            @input="handleFilterChange"
            @clear="handleFilterChange">
          </el-input>
        </div>
        
        <!-- 手机号码 -->
        <div class="filter-item">
          <label class="filter-label">手机号码</label>
          <el-input
            v-model="localFilters.phoneNumber"
            placeholder="请输入手机号码"
            clearable
            @input="handleFilterChange"
            @clear="handleFilterChange">
          </el-input>
        </div>
        
        <!-- 所属仓库 -->
        <div class="filter-item">
          <label class="filter-label">所属仓库</label>
          <el-select
            v-model="localFilters.warehouse" filterable
            placeholder="全部"
            clearable
            :loading="warehouseLoading"
            @change="handleFilterChange">
            <el-option label="全部" value="-1"></el-option>
            <el-option
              v-for="warehouse in warehouseOptions"
              :key="warehouse.id"
              :label="warehouse.name"
              :value="warehouse.id">
            </el-option>
          </el-select>
        </div>
      </div>
      
      <!-- 第二行筛选控件 -->
      <div class="filter-row">
        <!-- 客户类型 -->
        <div class="filter-item">
          <label class="filter-label">客户类型</label>
          <el-select
            v-model="localFilters.customerType"
            placeholder="全部"
            clearable
            @change="handleFilterChange">
            <el-option label="全部" value="-1"></el-option>
            <el-option label="普通客户" value="0"></el-option>
            <el-option label="月结客户" value="1"></el-option>
            <el-option label="VIP客户" value="2"></el-option>
            <el-option label="失信客户" value="3"></el-option>
            <el-option label="云仓二批客户" value="4"></el-option>
          </el-select>
        </div>
        
        <!-- 活跃水平 -->
        <div class="filter-item">
          <label class="filter-label">活跃水平</label>
          <el-select
            v-model="localFilters.activeLevel"
            placeholder="全部"
            clearable
            @change="handleFilterChange">
            <el-option label="全部" value="-1"></el-option>
            <el-option label="高活跃" value="3"></el-option>
            <el-option label="中活跃" value="2"></el-option>
            <el-option label="低活跃" value="1"></el-option>
            <el-option label="不活跃" value="0"></el-option>
          </el-select>
        </div>
        
        <!-- 偏好品牌 -->
        <div class="filter-item">
          <label class="filter-label">偏好品牌</label>
          <el-select
            v-model="localFilters.preferredBrand" filterable
            placeholder="全部"
            clearable
            :loading="brandLoading"
            @change="handleFilterChange">
            <el-option label="全部" value="-1"></el-option>
            <el-option
              v-for="brand in brandOptions"
              :key="brand.TypeID"
              :label="brand.TypeName"
              :value="brand.TypeID">
            </el-option>
          </el-select>
        </div>
        
        <!-- 偏好规格 -->
        <div class="filter-item">
          <label class="filter-label">偏好规格</label>
          
           <el-input
            v-model="localFilters.preferredSpec"
            placeholder="请输入偏好规格"
            clearable
            @input="handleFilterChange"
            @clear="handleFilterChange">
          </el-input>
        </div>
      </div>
      
      <!-- 第三行筛选控件 -->
      <div class="filter-row">
        <!-- 价值水平 -->
        <div class="filter-item">
          <label class="filter-label">价值水平</label>
          <el-select
            v-model="localFilters.valueLevel"
            placeholder="全部"
            clearable
            @change="handleFilterChange">
            <el-option label="全部" value="-1"></el-option>
            <el-option label="高价值" value="3"></el-option>
            <el-option label="中价值" value="2"></el-option>
            <el-option label="低价值" value="1"></el-option>
            <el-option label="无价值" value="0"></el-option>
          </el-select>
        </div>
        
        <!-- 操作按钮 -->
        <div class="filter-actions">
          <el-button 
            icon="el-icon-refresh"
            @click="handleRefresh">
            刷新
          </el-button>
          <el-button 
            icon="el-icon-download"
            @click="handleExport">
            导出
          </el-button>
          <el-button 
            type="primary" 
            icon="el-icon-search"
            @click="handleSearch">
            查询
          </el-button>
        </div>
      </div>
    </div>
  `,
    data() {
        return {
            localFilters: {
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
            warehouseOptions: [],
            brandOptions: [],
            warehouseLoading: false,
            brandLoading: false,
        };
    },
    watch: {
        filters: {
            handler(newFilters) {
                this.localFilters = { ...this.localFilters, ...newFilters };
            },
            immediate: true,
            deep: true
        }
    },
    methods: {
        // 筛选条件变化处理
        handleFilterChange() {
            // 防抖处理，避免频繁触发
            clearTimeout(this.filterTimer);
            this.filterTimer = setTimeout(() => {
                this.$emit('filter-change', { ...this.localFilters });
            }, 300);
        },

        // 搜索处理
        handleSearch() {
            this.$emit('search', { ...this.localFilters });
        },

        // 刷新处理
        handleRefresh() {
            this.$emit('refresh');
        },

        // 导出处理
        handleExport() {
            this.$emit('export', { ...this.localFilters });
        },

        // 重置筛选条件
        resetFilters() {
            this.localFilters = {
                customerName: '',
                customerCode: '',
                phoneNumber: '',
                warehouse: '',
                customerType: '',
                activeLevel: '',
                preferredBrand: '',
                preferredSpec: '',
                valueLevel: ''
            };
            this.$emit('filter-change', { ...this.localFilters });
            this.$message.success('筛选条件已重置');
        },

        // 获取筛选条件摘要
        getFilterSummary() {
            const summary = [];

            if (this.localFilters.customerName) {
                summary.push(`客户名称: ${this.localFilters.customerName}`);
            }

            if (this.localFilters.customerCode) {
                summary.push(`客户编码: ${this.localFilters.customerCode}`);
            }

            if (this.localFilters.phoneNumber) {
                summary.push(`手机号码: ${this.localFilters.phoneNumber}`);
            }

            if (this.localFilters.warehouse) {
                summary.push(`所属仓库: ${this.localFilters.warehouse}`);
            }

            if (this.localFilters.customerType) {
                summary.push(`客户类型: ${this.localFilters.customerType}`);
            }

            if (this.localFilters.activeLevel) {
                summary.push(`活跃水平: ${this.localFilters.activeLevel}`);
            }

            if (this.localFilters.preferredBrand) {
                summary.push(`偏好品牌: ${this.localFilters.preferredBrand}`);
            }

            if (this.localFilters.preferredSpec) {
                summary.push(`偏好规格: ${this.localFilters.preferredSpec}`);
            }

            if (this.localFilters.valueLevel) {
                summary.push(`价值水平: ${this.localFilters.valueLevel}`);
            }

            return summary.length > 0 ? summary.join(', ') : '无筛选条件';
        },

        // 验证筛选条件
        validateFilters() {
            // 手机号码验证
            if (this.localFilters.phoneNumber && !CONSTANTS.REGEX.PHONE.test(this.localFilters.phoneNumber)) {
                this.$message.error('请输入正确的手机号码格式');
                return false;
            }

            return true;
        },

        // 获取仓库列表数据
        async loadWarehouseOptions() {
            try {
                this.warehouseLoading = true;

                // 优先从API获取数据
                const response = await API.warehouses.list();

                if (response.success && response.data) {
                    this.warehouseOptions = response.data;
                } else {
                    // 如果API失败，使用模拟数据作为备选
                    console.warn('API获取仓库数据失败，使用模拟数据');
                    this.warehouseOptions = [];
                }

            } catch (error) {
                console.error('获取仓库数据失败:', error);

                // 使用模拟数据作为备选方案
                this.warehouseOptions = [];

                // 显示友好的错误提示
                this.$message.warning('仓库数据加载失败，已使用本地数据');

            } finally {
                this.warehouseLoading = false;
            }
        },

        // 刷新仓库数据
        refreshWarehouseOptions() {
            this.loadWarehouseOptions();
        },

        // 获取品牌列表数据
        async loadBrandOptions() {
            try {
                this.brandLoading = true;

                // 优先从API获取数据
                const response = await API.brands.list();

                if (response.success && response.data) {
                    this.brandOptions = response.data;
                } else {
                    // 如果API失败，使用模拟数据作为备选
                    console.warn('API获取品牌数据失败，使用模拟数据');
                    this.brandOptions = [];
                }

            } catch (error) {
                console.error('获取品牌数据失败:', error);

                // 使用模拟数据作为备选方案
                this.brandOptions = [];

                // 显示友好的错误提示
                this.$message.warning('品牌数据加载失败，已使用本地数据');

            } finally {
                this.brandLoading = false;
            }
        },

        // 刷新仓库数据
        refreshBrandOptions() {
            this.loadBrandOptions();
        }
    },

    // 组件创建时加载仓库数据
    created() {
        this.loadWarehouseOptions();
        this.loadBrandOptions();
    },

    beforeDestroy() {
        if (this.filterTimer) {
            clearTimeout(this.filterTimer);
        }
    }
});