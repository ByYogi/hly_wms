// 仓库详细表格组件
Vue.component('warehouse-table', {
  props: {
    data: {
      type: Array,
      default: () => []
    },
    loading: {
      type: Boolean,
      default: false
    }
  },
  template: `
    <div class="warehouse-table">
      <el-table
        :data="data"
        :loading="loading"
        stripe
        style="width: 100%"
        @row-click="handleRowClick"
        ref="warehouseTable">
        
        <!-- 序号列 -->
        <el-table-column
          type="index"
          label="序号"
          width="80"
          align="center">
        </el-table-column>
        
        <!-- 仓库名称 -->
        <el-table-column
          prop="name"
          label="仓库名称"
          align="left"
          show-overflow-tooltip>
          <template slot-scope="scope">
            <span class="warehouse-name">{{ scope.row.name }}</span>
          </template>
        </el-table-column>
        
        <!-- 出库量 -->
        <el-table-column
          prop="shipmentVolume"
          label="出库量"
          sortable>
          <template slot-scope="scope">
            <span class="shipment-volume">{{ formatNumber(scope.row.shipmentVolume) }}</span>
          </template>
        </el-table-column>
        
        <!-- 注册人数 -->
        <el-table-column
          prop="registeredUsers"
          label="注册人数"
          sortable>
          <template slot-scope="scope">
            <span class="registered-users">{{ formatNumber(scope.row.registeredUsers) }}</span>
          </template>
        </el-table-column>
        
        <!-- 人均出库量 -->
        <el-table-column
          prop="avgShipmentPerUser"
          label="人均出库量"
          sortable>
          <template slot-scope="scope">
            <span class="avg-shipment">{{ scope.row.avgShipmentPerUser }}</span>
          </template>
        </el-table-column>
        
        <!-- 成交率 -->
        <el-table-column
          prop="conversionRate"
          label="成交率"
          sortable>
          <template slot-scope="scope">
            <span class="conversion-rate" :class="getConversionRateClass(scope.row.conversionRate)">
              {{ scope.row.conversionRate }}
            </span>
          </template>
        </el-table-column>
        
        <!-- 操作列 -->
        <el-table-column
          label="操作">
          <template slot-scope="scope">
            <el-button 
              size="mini" 
              type="text" 
              class="view-detail-btn"
              @click.stop="handleViewDetail(scope.row)">
              查看详情
            </el-button>
          </template>
        </el-table-column>
      </el-table>
      
      <!-- 空状态 -->
      <div v-if="!loading && (!data || data.length === 0)" class="empty-state">
        <i class="el-icon-house empty-state__icon"></i>
        <p class="empty-state__text">暂无仓库数据</p>
      </div>
    </div>
  `,
  methods: {
    // 格式化数字
    formatNumber(value) {
      if (value === null || value === undefined) return '-';
      return new Intl.NumberFormat('zh-CN').format(value);
    },
    
    // 获取成交率样式类
    getConversionRateClass(rate) {
      if (!rate) return '';
      
      const numRate = parseFloat(rate.replace('%', ''));
      if (numRate >= 70) {
        return 'rate-high';
      } else if (numRate >= 60) {
        return 'rate-medium';
      } else {
        return 'rate-low';
      }
    },
    
    // 行点击处理
    handleRowClick(row, column, event) {
      this.$emit('row-click', row, column, event);
    },
    
    // 查看详情处理
    handleViewDetail(row) {
      this.$emit('view-detail', row);
      //this.$message.info(`查看 ${row.name} 的详细信息`);
    },
    
    // 刷新表格
    refresh() {
      this.$emit('refresh');
    }
  }
});