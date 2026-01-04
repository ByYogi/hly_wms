// 数据表格组件
Vue.component('data-table', {
    props: {
        columns: {
            type: Array,
            required: true
        },
        data: {
            type: Array,
            default: () => []
        },
        loading: {
            type: Boolean,
            default: false
        },
        pagination: {
            type: Object,
            default: () => ({
                currentPage: 1,
                pageSize: 20,
                total: 0,
                pageSizes: [10, 20, 50, 100]
            })
        },
        selectable: {
            type: Boolean,
            default: false
        }
    },
    template: `
    <div class="data-table">
      <!-- 调试信息 -->
      <div v-if="false" style="background: #f0f0f0; padding: 10px; margin-bottom: 10px; font-size: 12px;">
        <p><strong>调试信息:</strong></p>
        <p>数据条数: {{ data ? data.length : 0 }}</p>
        <p>列数: {{ columns ? columns.length : 0 }}</p>
        <p>第一行数据: {{ data && data[0] ? JSON.stringify(data[0]) : '无数据' }}</p>
      </div>
      
      <el-table
        :data="data"
        :loading="loading"
        stripe
        style="width: 100%"
        @sort-change="handleSortChange"
        @row-click="handleRowClick"
        @row-dblclick="handleRowDoubleClick"
        ref="dataTable">
        
        <!-- 序号列 -->
        <el-table-column
          type="index"
          label="序号"
          width="60"
          align="center"
          :index="getIndex">
        </el-table-column>
        
        <!-- 数据列 - 重构版本 -->
        <template v-for="column in columns">
          <!-- 状态标签列 -->
          <el-table-column
            v-if="column.type === 'status'"
            :key="column.prop"
            :prop="column.prop"
            :label="column.label"
            :width="column.width"
            :min-width="column.minWidth"
            :sortable="column.sortable"
            :align="column.align || 'left'"
            :class-name="column.className"
            :show-overflow-tooltip="column.showOverflowTooltip !== false">
            <template slot-scope="scope">
              <!-- 活跃水平字段使用数值范围逻辑 -->
              <template v-if="column.prop === 'activeLevel'">
                <el-tag
                  :type="getElementTypeFromClass(getActiveLevelStatus(scope.row[column.prop]).class)"
                  size="small">
                  {{ getActiveLevelStatus(scope.row[column.prop]).title }}
                </el-tag>
              </template>
                   <!--价值水平字段使用数值范围逻辑 -->
              <template v-else-if="column.prop === 'valueLevel'">
                <el-tag
                  :type="getElementTypeFromClass(getvalueLevelStatus(scope.row[column.prop]).class)"
                  size="small">
                  {{ getvalueLevelStatus(scope.row[column.prop]).title }}
                </el-tag>
              </template>
              <!-- 其他状态字段使用原有逻辑 -->
              <template v-else>
                <el-tag
                  :type="getStatusType(scope.row[column.prop])"
                  size="small">
                  {{ scope.row[column.prop] }}
                </el-tag>
              </template>
            </template>
          </el-table-column>
          
          <!-- 日期列 -->
          <el-table-column
            v-else-if="column.type === 'date'"
            :key="column.prop"
            :prop="column.prop"
            :label="column.label"
            :width="column.width"
            :min-width="column.minWidth"
            :sortable="column.sortable"
            :align="column.align || 'left'"
            :class-name="column.className"
            :show-overflow-tooltip="column.showOverflowTooltip !== false">
            <template slot-scope="scope">
              {{ formatDate(scope.row[column.prop]) }}
            </template>
          </el-table-column>
          
          <!-- 日期时间列 -->
          <el-table-column
            v-else-if="column.type === 'datetime'"
            :key="column.prop"
            :prop="column.prop"
            :label="column.label"
            :width="column.width"
            :min-width="column.minWidth"
            :sortable="column.sortable"
            :align="column.align || 'left'"
            :class-name="column.className"
            :show-overflow-tooltip="column.showOverflowTooltip !== false">
            <template slot-scope="scope">
              {{ formatDateTime(scope.row[column.prop]) }}
            </template>
          </el-table-column>
          
          <!-- 货币列 -->
          <el-table-column
            v-else-if="column.type === 'currency'"
            :key="column.prop"
            :prop="column.prop"
            :label="column.label"
            :width="column.width"
            :min-width="column.minWidth"
            :sortable="column.sortable"
            :align="column.align || 'left'"
            :class-name="column.className"
            :show-overflow-tooltip="column.showOverflowTooltip !== false">
            <template slot-scope="scope">
              {{ formatCurrency(scope.row[column.prop]) }}
            </template>
          </el-table-column>
          
          <!-- 操作列 -->
          <el-table-column
            v-else-if="column.type === 'actions'"
            :key="column.prop"
            :label="column.label"
            :width="column.width"
            :min-width="column.minWidth"
            :align="column.align || 'left'"
            :class-name="column.className">
            <template slot-scope="scope">
              <div class="table-actions">
                <el-button
                  size="mini"
                  type="primary"
                  icon="el-icon-edit"
                  @click.stop="handleEdit(scope.row)">
                  编辑
                </el-button>
                <el-button
                  size="mini"
                  type="danger"
                  icon="el-icon-delete"
                  @click.stop="handleDelete(scope.row)">
                  删除
                </el-button>
                <el-dropdown
                  v-if="column.moreActions"
                  @command="handleMoreAction($event, scope.row)"
                  trigger="click">
                  <el-button size="mini" type="text">
                    更多<i class="el-icon-arrow-down el-icon--right"></i>
                  </el-button>
                  <el-dropdown-menu slot="dropdown">
                    <el-dropdown-item
                      v-for="action in column.moreActions"
                      :key="action.command"
                      :command="action.command">
                      <i :class="action.icon"></i> {{ action.label }}
                    </el-dropdown-item>
                  </el-dropdown-menu>
                </el-dropdown>
              </div>
            </template>
          </el-table-column>
          
          <!-- 自定义列 -->
          <el-table-column
            v-else-if="column.type === 'custom'"
            :key="column.prop"
            :prop="column.prop"
            :label="column.label"
            :width="column.width"
            :min-width="column.minWidth"
            :sortable="column.sortable"
            :align="column.align || 'left'"
            :class-name="column.className"
            :show-overflow-tooltip="column.showOverflowTooltip !== false">
            <template slot-scope="scope">
              <slot
                :name="column.slot"
                :row="scope.row"
                :column="column"
                :index="scope.$index">
                {{ scope.row[column.prop] }}
              </slot>
            </template>
          </el-table-column>
          
          <!-- 默认列 - 关键修复！ -->
          <el-table-column
            v-else
            :key="column.prop"
            :prop="column.prop"
            :label="column.label"
            :width="column.width"
            :min-width="column.minWidth"
            :sortable="column.sortable"
            :formatter="column.formatter"
            :align="column.align || 'left'"
            :class-name="column.className"
            :show-overflow-tooltip="column.showOverflowTooltip !== false">
          </el-table-column>
        </template>
      </el-table>
      
      <!-- 分页组件 -->
      <div class="table-pagination" v-if="pagination && pagination.total > 0">
        <el-pagination
          :current-page="pagination.currentPage"
          :page-sizes="pagination.pageSizes"
          :page-size="pagination.pageSize"
          :total="pagination.total"
          layout="total, sizes, prev, pager, next, jumper"
          @size-change="handleSizeChange"
          @current-change="handleCurrentChange">
        </el-pagination>
      </div>
      
      <!-- 空状态 -->
      <div v-if="!loading && (!data || data.length === 0)" class="empty-state">
        <i class="el-icon-document empty-state__icon"></i>
        <p class="empty-state__text">暂无数据</p>
      </div>
    </div>
  `,
    data() {
        return {};
    },
    methods: {
        // 获取序号
        getIndex(index) {
            const { currentPage, pageSize } = this.pagination;
            return (currentPage - 1) * pageSize + index + 1;
        },

        // 获取状态类型
        getStatusType(status) {
            const statusMap = {
                '活跃': 'success',
                '正常': 'success',
                '启用': 'success',
                '待激活': 'warning',
                '待审核': 'warning',
                '处理中': 'warning',
                '已停用': 'danger',
                '已禁用': 'danger',
                '已删除': 'danger',
                '失败': 'danger'
            };
            return statusMap[status] || 'info';
        },

        // 获取活跃水平状态（基于数值范围）
        getActiveLevelStatus(value) {
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
            const config = statusConfig.find(item =>
                activeNum >= item.min && activeNum <= item.max
            );
            return config || { title: '未知', class: 'el-tag--info' };
        },
        // 获取价值水平状态（基于数值范围）
        getvalueLevelStatus(value) {
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
            const config = statusConfig.find(item =>
                activeNum >= item.min && activeNum <= item.max
            );
            return config || { title: '未知', class: 'el-tag--info' };
        },

        // 将自定义class转换为Element UI的type
        getElementTypeFromClass(className) {
            const classMap = {
                'el-tag--gray': 'info',
                'el-tag--yellow': 'warning',
                'el-tag--blue': 'primary',
                'el-tag--green': 'success',
                'el-tag--info': 'info'
            };
            return classMap[className] || 'info';
        },

        // 格式化日期
        formatDate(date) {
            if (!date) return '-';

            const d = new Date(date);
            if (isNaN(d.getTime())) return date;

            return d.toLocaleDateString('zh-CN', {
                year: 'numeric',
                month: '2-digit',
                day: '2-digit'
            });
        },

        // 格式化日期时间
        formatDateTime(datetime) {
            if (!datetime) return '-';

            const d = new Date(datetime);
            if (isNaN(d.getTime())) return datetime;

            return d.toLocaleString('zh-CN', {
                year: 'numeric',
                month: '2-digit',
                day: '2-digit',
                hour: '2-digit',
                minute: '2-digit'
            });
        },

        // 格式化货币
        formatCurrency(amount) {
            if (amount === null || amount === undefined) return '-';

            return new Intl.NumberFormat('zh-CN', {
                style: 'currency',
                currency: 'CNY'
            }).format(amount);
        },


        // 排序变化处理
        handleSortChange(sort) {
            this.$emit('sort-change', sort);
        },

        // 行点击处理
        handleRowClick(row, column, event) {
            this.$emit('row-click', row, column, event);
        },

        // 行双击处理
        handleRowDoubleClick(row, column, event) {
            this.$emit('row-dblclick', row, column, event);
        },

        // 编辑处理
        handleEdit(row) {
            this.$emit('edit', row);
        },

        // 删除处理
        handleDelete(row) {
            this.$confirm('确定要删除这条记录吗？', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning'
            }).then(() => {
                this.$emit('delete', row);
            }).catch(() => {
                // 用户取消删除
            });
        },

        // 更多操作处理
        handleMoreAction(command, row) {
            this.$emit('more-action', { command, row });
        },

        // 页面大小变化处理
        handleSizeChange(size) {
            this.$emit('page-size-change', size);
        },

        // 当前页变化处理
        handleCurrentChange(page) {
            this.$emit('page-change', page);
        },


        // 设置当前行
        setCurrentRow(row) {
            this.$refs.dataTable.setCurrentRow(row);
        },

        // 刷新表格
        refresh() {
            this.$emit('refresh');
        },

        // 导出数据
        exportData() {
            this.$emit('export', this.data);
        }
    }
});