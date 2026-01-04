// 排行榜面板组件
Vue.component('ranking-panel', {
    template: `
    <div class="ranking-panel">
      <div class="ranking-header">
        <h3>排行榜</h3>
        <div class="ranking-actions">
          <el-button
            icon="el-icon-refresh"
            size="mini"
            @click="refreshAllRankings"
            :loading="refreshing">
            刷新
          </el-button>
        </div>
      </div>
      <el-tabs v-model="activeTab" class="ranking-tabs" @tab-click="handleTabClick">
        <el-tab-pane label="用户活跃排行" name="active">
          <div class="ranking-table">
            <div class="table-header">
              <div class="col-rank">排名</div>
              <div class="col-name">用户名称</div>
              <div class="col-value">浏览次数</div>
            </div>
            <div class="table-body" v-loading="loading.active">
              <template v-if="!loading.active && userActiveRanking.length > 0">
                <div
                  v-for="(item, index) in userActiveRanking"
                  :key="index"
                  class="table-row"
                  :class="{ 'top-three': index < 3 }"
                >
                  <div class="col-rank">
                    <span class="rank-number" :class="getRankClass(index)">{{ index + 1 }}</span>
                  </div>
                  <div class="col-name">{{ item.name }}</div>
                  <div class="col-value">{{ item.views }}</div>
                </div>
              </template>
              <div v-else-if="!loading.active && userActiveRanking.length === 0" class="empty-data">
                <i class="el-icon-info"></i>
                <span>暂无数据</span>
              </div>
            </div>
          </div>
        </el-tab-pane>
        
        <el-tab-pane label="用户价值排行" name="value">
          <div class="ranking-table">
            <div class="table-header">
              <div class="col-rank">排名</div>
              <div class="col-name">用户名称</div>
              <div class="col-value">下单条数</div>
            </div>
            <div class="table-body" v-loading="loading.value">
              <template v-if="!loading.value && userValueRanking.length > 0">
                <div
                  v-for="(item, index) in userValueRanking"
                  :key="index"
                  class="table-row"
                  :class="{ 'top-three': index < 3 }"
                >
                  <div class="col-rank">
                    <span class="rank-number" :class="getRankClass(index)">{{ index + 1 }}</span>
                  </div>
                  <div class="col-name">{{ item.name }}</div>
                  <div class="col-value">{{ item.orders }}</div>
                </div>
              </template>
              <div v-else-if="!loading.value && userValueRanking.length === 0" class="empty-data">
                <i class="el-icon-info"></i>
                <span>暂无数据</span>
              </div>
            </div>
          </div>
        </el-tab-pane>
        
        <el-tab-pane label="热销产品排行" name="product">
          <div class="ranking-table">
            <div class="table-header">
              <div class="col-rank">排名</div>
              <div class="col-code">产品编码</div>
              <div class="col-brand">品牌名称</div>
              <div class="col-spec">规格</div>
              <div class="col-sales">销售条数</div>
            </div>
            <div class="table-body" v-loading="loading.product">
              <template v-if="!loading.product && productRanking.length > 0">
                <div
                  v-for="(item, index) in productRanking"
                  :key="index"
                  class="table-row"
                  :class="{ 'top-three': index < 3 }"
                >
                  <div class="col-rank">
                    <span class="rank-number" :class="getRankClass(index)">{{ index + 1 }}</span>
                  </div>
                  <div class="col-code">{{ item.code }}</div>
                  <div class="col-brand">{{ item.brand }}</div>
                  <div class="col-spec">{{ item.spec }}</div>
                  <div class="col-sales">{{ item.sales }}</div>
                </div>
              </template>
              <div v-else-if="!loading.product && productRanking.length === 0" class="empty-data">
                <i class="el-icon-info"></i>
                <span>暂无数据</span>
              </div>
            </div>
          </div>
        </el-tab-pane>
      </el-tabs>
      <div class="update-time">
        数据更新时间：{{ updateTime }}
      </div>
    </div>
  `,
    props: {
        // 数据源配置
        dataSource: {
            type: Object,
            default: () => ({
                apiEnabled: true,
                refreshInterval: 300000, // 5分钟自动刷新
                enableAutoRefresh: false,
                limit: 10 // 排行榜显示条数
            })
        },
        // 初始数据
        initialData: {
            type: Object,
            default: null
        }
    },
    data() {
        return {
            activeTab: 'active',
            updateTime: '数据加载中...',
            refreshing: false,
            autoRefreshTimer: null,

            // 加载状态
            loading: {
                active: false,
                value: false,
                product: false
            },

            // 排行榜数据
            userActiveRanking: [],
            userValueRanking: [],
            productRanking: [],

            // 默认数据（作为后备）
            defaultData: {
                userActiveRanking: [
                    { name: '城君丰商贸有限公司', views: 12 },
                    { name: '杭州天车车站大街', views: 8 },
                    { name: '河北李会安', views: 7 },
                    { name: '光明区之星汽修店', views: 6 },
                    { name: '宏伟汽车服务中心', views: 4 },
                    { name: '德山汽车服务中心', views: 3 },
                    { name: '嘉兴达汽车维修部', views: 2 },
                    { name: '范文龙', views: 5 },
                    { name: '德宝汽车维修服务', views: 0 }
                ],
                userValueRanking: [
                    { name: '城君丰商贸有限公司', orders: 45 },
                    { name: '杭州天车车站大街', orders: 38 },
                    { name: '河北李会安', orders: 32 },
                    { name: '光明区之星汽修店', orders: 28 },
                    { name: '宏伟汽车服务中心', orders: 25 },
                    { name: '德山汽车服务中心', orders: 22 },
                    { name: '嘉兴达汽车维修部', orders: 18 },
                    { name: '范文龙', orders: 15 },
                    { name: '德宝汽车维修服务', orders: 12 }
                ],
                productRanking: [
                    { code: 'P001', brand: '博世', spec: '12V 60Ah', sales: 156 },
                    { code: 'P002', brand: '瓦尔塔', spec: '12V 70Ah', sales: 142 },
                    { code: 'P003', brand: '统一', spec: '12V 55Ah', sales: 128 },
                    { code: 'P004', brand: '风帆', spec: '12V 65Ah', sales: 115 },
                    { code: 'P005', brand: '骆驼', spec: '12V 80Ah', sales: 98 },
                    { code: 'P006', brand: '理士', spec: '12V 45Ah', sales: 87 },
                    { code: 'P007', brand: '汤浅', spec: '12V 75Ah', sales: 76 },
                    { code: 'P008', brand: '超威', spec: '12V 50Ah', sales: 65 },
                    { code: 'P009', brand: '天能', spec: '12V 90Ah', sales: 54 }
                ]
            }
        }
    },
    async mounted() {
        // 使用初始数据或默认数据
        this.initializeData();

        // 加载排行榜数据
        await this.loadAllRankings();

        // 设置自动刷新
        if (this.dataSource.enableAutoRefresh) {
            this.setupAutoRefresh();
        }
    },
    beforeDestroy() {
        // 清理自动刷新定时器
        if (this.autoRefreshTimer) {
            clearInterval(this.autoRefreshTimer);
        }
    },
    methods: {
        /**
         * 初始化数据
         */
        initializeData() {
            if (this.initialData) {
                this.userActiveRanking = this.initialData.userActiveRanking || this.defaultData.userActiveRanking;
                this.userValueRanking = this.initialData.userValueRanking || this.defaultData.userValueRanking;
                this.productRanking = this.initialData.productRanking || this.defaultData.productRanking;
                this.updateTime = this.initialData.updateTime || new Date().toLocaleString();
            } else {
                this.userActiveRanking = [...this.defaultData.userActiveRanking];
                this.userValueRanking = [...this.defaultData.userValueRanking];
                this.productRanking = [...this.defaultData.productRanking];
                this.updateTime = new Date().toLocaleString();
            }
        },

        /**
         * 加载所有排行榜数据
         */
        async loadAllRankings() {
            if (!this.dataSource.apiEnabled) {
                console.log('🔄 [RankingPanel] API已禁用，使用默认数据');
                return;
            }


            try {
                // 并行加载所有排行榜数据
                await Promise.all([
                    this.loadRankingData('GetRankingUserData'),
                    this.loadRankingData('GetRankingUserConsumptionData'),
                    this.loadRankingData('GetRankingUserHotsellingData')
                ]);

                this.updateTime = new Date().toLocaleString();

            } catch (error) {
                console.error('❌ [RankingPanel] 加载排行榜数据失败:', error);
            }
        },

        /**
         * 加载指定类型的排行榜数据
         * @param {string} type 排行榜类型 (active|value|product)
         */
        async loadRankingData(type) {

            // 设置加载状态
            this.$set(this.loading, type, true);

            try {
                // 调用API获取数据
                const response = await API.statistics.rankings(type, {
                    limit: this.dataSource.limit || 10
                });


                if (response.success && response.data) {
                    // 根据类型更新对应的数据
                    this.updateRankingData(type, response.data);
                } else {
                    console.warn(`⚠️ [RankingPanel] ${type}排行榜API返回失败，保持现有数据`);
                }

            } catch (error) {
                console.error(`❌ [RankingPanel] 加载${type}排行榜数据失败:`, error);
                // API失败时保持现有数据，不做任何更改
            } finally {
                // 重置加载状态
                this.$set(this.loading, type, false);
            }
        },

        /**
         * 更新排行榜数据
         * @param {string} type 排行榜类型
         * @param {object} data API返回的数据
         */
        updateRankingData(type, data) {
            try {
                let rankings = [];

                // 处理不同的数据格式
                if (Array.isArray(data)) {
                    rankings = data;
                } else if (data.rankings && Array.isArray(data.rankings)) {
                    rankings = data.rankings;
                } else if (data.list && Array.isArray(data.list)) {
                    rankings = data.list;
                }

                // 根据类型更新对应的数据
                switch (type) {
                    case 'active':
                    case 'GetRankingUserData':
                        this.userActiveRanking = rankings.length > 0 ? rankings : this.defaultData.userActiveRanking;
                        break;
                    case 'value':
                    case 'GetRankingUserConsumptionData':
                        this.userValueRanking = rankings.length > 0 ? rankings : this.defaultData.userValueRanking;
                        break;
                    case 'product':
                    case 'GetRankingUserHotsellingData':
                        this.productRanking = rankings.length > 0 ? rankings : this.defaultData.productRanking;
                        break;
                }

                // 更新时间
                if (data.updateTime) {
                    this.updateTime = data.updateTime;
                }

            } catch (error) {
                console.error(`❌ [RankingPanel] 更新${type}排行榜数据失败:`, error);
            }
        },

        /**
         * 刷新所有排行榜数据
         */
        async refreshAllRankings() {

            this.refreshing = true;

            try {
                await this.loadAllRankings();
                this.$message.success('排行榜数据已刷新');

                // 触发刷新事件
                this.$emit('refresh', {
                    type: 'all',
                    timestamp: new Date().toISOString()
                });

            } catch (error) {
                console.error('❌ [RankingPanel] 刷新失败:', error);
                this.$message.error('刷新排行榜数据失败');
            } finally {
                this.refreshing = false;
            }
        },

        /**
         * 刷新指定类型的排行榜数据
         * @param {string} type 排行榜类型
         */
        async refreshRankingData(type) {

            try {
                await this.loadRankingData(type);

                // 触发刷新事件
                this.$emit('refresh', {
                    type: type,
                    timestamp: new Date().toISOString()
                });

            } catch (error) {
                console.error(`❌ [RankingPanel] 刷新${type}排行榜失败:`, error);
            }
        },

        /**
         * 处理标签页切换
         * @param {object} tab 标签页对象
         */
        handleTabClick(tab) {
            const tabName = tab.name;

            // 如果该标签页数据为空，尝试重新加载
            const isEmpty = this.isRankingEmpty(tabName);
            if (isEmpty && this.dataSource.apiEnabled) {
                this.loadRankingData(tabName);
            }

            // 触发标签页切换事件
            this.$emit('tab-change', {
                activeTab: tabName,
                timestamp: new Date().toISOString()
            });
        },

        /**
         * 检查排行榜数据是否为空
         * @param {string} type 排行榜类型
         * @returns {boolean} 是否为空
         */
        isRankingEmpty(type) {
            switch (type) {
                case 'active':
                    return this.userActiveRanking.length === 0;
                case 'value':
                    return this.userValueRanking.length === 0;
                case 'product':
                    return this.productRanking.length === 0;
                default:
                    return true;
            }
        },

        /**
         * 设置自动刷新
         */
        setupAutoRefresh() {
            if (this.autoRefreshTimer) {
                clearInterval(this.autoRefreshTimer);
            }

            const interval = this.dataSource.refreshInterval || 300000; // 默认5分钟

            this.autoRefreshTimer = setInterval(() => {
                this.loadAllRankings();
            }, interval);
        },

        /**
         * 获取排名样式类
         * @param {number} index 排名索引
         * @returns {string} 样式类名
         */
        getRankClass(index) {
            if (index === 0) return 'rank-first';
            if (index === 1) return 'rank-second';
            if (index === 2) return 'rank-third';
            return '';
        }
    }
});