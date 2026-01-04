// 筛选栏组件
Vue.component('filter-bar', {
  props: {
    filters: {
      type: Object,
      default: () => ({})
    }
  },
  template: `
    <div class="filter-bar">
      <!-- 搜索框 -->
      <div class="filter-bar__item">
        <label class="filter-bar__label">关键词搜索</label>
        <el-input
          v-model="localFilters.keyword"
          placeholder="请输入客户名称、联系人或电话"
          prefix-icon="el-icon-search"
          clearable
          style="width: 280px"
          @input="handleFilterChange"
          @clear="handleFilterChange">
        </el-input>
      </div>
      
      <!-- 状态筛选 -->
      <div class="filter-bar__item">
        <label class="filter-bar__label">客户状态</label>
        <el-select
          v-model="localFilters.status"
          placeholder="请选择状态"
          clearable
          style="width: 150px"
          @change="handleFilterChange">
          <el-option label="全部" value=""></el-option>
          <el-option label="活跃" value="活跃"></el-option>
          <el-option label="待激活" value="待激活"></el-option>
          <el-option label="已停用" value="已停用"></el-option>
        </el-select>
      </div>
      
      <!-- 日期范围 -->
      <div class="filter-bar__item">
        <label class="filter-bar__label">创建时间</label>
        <el-date-picker
          v-model="localFilters.dateRange"
          type="daterange"
          range-separator="至"
          start-placeholder="开始日期"
          end-placeholder="结束日期"
          format="yyyy-MM-dd"
          value-format="yyyy-MM-dd"
          style="width: 240px"
          @change="handleFilterChange">
        </el-date-picker>
      </div>
      
      <!-- 客户类型 -->
      <div class="filter-bar__item">
        <label class="filter-bar__label">客户类型</label>
        <el-select
          v-model="localFilters.customerType"
          placeholder="请选择类型"
          clearable
          style="width: 150px"
          @change="handleFilterChange">
          <el-option label="全部" value=""></el-option>
          <el-option label="企业客户" value="企业客户"></el-option>
          <el-option label="个人客户" value="个人客户"></el-option>
          <el-option label="VIP客户" value="VIP客户"></el-option>
        </el-select>
      </div>
      
      <!-- 操作按钮 -->
      <div class="filter-bar__actions">
        <el-button 
          type="primary" 
          icon="el-icon-search"
          @click="handleSearch">
          搜索
        </el-button>
        <el-button 
          icon="el-icon-refresh-left"
          @click="handleReset">
          重置
        </el-button>
        <el-button 
          icon="el-icon-download"
          @click="handleExport">
          导出
        </el-button>
      </div>
    </div>
  `,
  data() {
    return {
      localFilters: {
        keyword: '',
        status: '',
        dateRange: [],
        customerType: ''
      }
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
    
    // 重置处理
    handleReset() {
      this.localFilters = {
        keyword: '',
        status: '',
        dateRange: [],
        customerType: ''
      };
      this.$emit('filter-change', { ...this.localFilters });
      this.$message.success('筛选条件已重置');
    },
    
    // 导出处理
    handleExport() {
      this.$emit('export', { ...this.localFilters });
    },
    
    // 获取筛选条件摘要
    getFilterSummary() {
      const summary = [];
      
      if (this.localFilters.keyword) {
        summary.push(`关键词: ${this.localFilters.keyword}`);
      }
      
      if (this.localFilters.status) {
        summary.push(`状态: ${this.localFilters.status}`);
      }
      
      if (this.localFilters.customerType) {
        summary.push(`类型: ${this.localFilters.customerType}`);
      }
      
      if (this.localFilters.dateRange && this.localFilters.dateRange.length === 2) {
        summary.push(`时间: ${this.localFilters.dateRange[0]} 至 ${this.localFilters.dateRange[1]}`);
      }
      
      return summary.length > 0 ? summary.join(', ') : '无筛选条件';
    },
    
    // 验证筛选条件
    validateFilters() {
      // 日期范围验证
      if (this.localFilters.dateRange && this.localFilters.dateRange.length === 2) {
        const startDate = new Date(this.localFilters.dateRange[0]);
        const endDate = new Date(this.localFilters.dateRange[1]);
        
        if (startDate > endDate) {
          this.$message.error('开始日期不能大于结束日期');
          return false;
        }
        
        // 检查日期范围是否过大（比如超过1年）
        const daysDiff = (endDate - startDate) / (1000 * 60 * 60 * 24);
        if (daysDiff > 365) {
          this.$message.warning('日期范围过大，建议选择1年以内的时间范围');
        }
      }
      
      return true;
    }
  },
  
  beforeDestroy() {
    if (this.filterTimer) {
      clearTimeout(this.filterTimer);
    }
  }
});