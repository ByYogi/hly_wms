<%@ Page Title="客户档案" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CustomerProfileManager.aspx.cs" Inherits="Cargo.Client.CustomerProfileManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

      <!-- Element UI CSS -->
<%--  <link rel="stylesheet" href="https://unpkg.com/element-ui/lib/theme-chalk/index.css"/>--%>
    <link href="../JS/NewVue/element-ui/css/index.css" rel="stylesheet" />
  
  <!-- 自定义样式 -->
  <link rel="stylesheet" href="../JS/CustomerProfileManager/css/variables.css"/>
  <link rel="stylesheet" href="../JS/CustomerProfileManager/css/main.css"/>
  <link rel="stylesheet" href="../JS/CustomerProfileManager/css/components.css"/>
  <link rel="stylesheet" href="../JS/CustomerProfileManager/css/responsive.css"/>
  <link rel="stylesheet" href="../JS/CustomerProfileManager/css/ranking.css"/>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <div id="app">
  <!-- 页面容器 -->
  <div class="page-container">

      <!-- 在新标签页打开按钮 - 独占一行 -->
      <div v-if="isInIframe" class="new-tab-button-section">
        <el-button
          icon="el-icon-link"
          size="small"
          type="primary"
          plain
          class="open-new-tab-btn standalone-btn"
          @click="openInNewTab">
          在新标签页打开
        </el-button>
      </div>

      <!-- KPI指标区 -->
      <div class="kpi-section">
        <!-- 加载状态：显示6个骨架屏 -->
        <template v-if="kpiLoading">
          <kpi-card
            v-for="n in 6"
            :key="'skeleton-' + n"
            :loading="true"
            title=""
            value=""
            :color="['primary', 'success', 'warning', 'info', 'danger', 'primary'][n-1]">
          </kpi-card>
        </template>
        
        <!-- 正常状态：显示实际数据 -->
        <template v-else>
          <kpi-card
            v-for="kpi in kpiData"
            :key="kpi.id"
            :title="kpi.title"
            :value="kpi.value"
            :subtitle="kpi.subtitle"
            :trend="kpi.trend"
            :trend-value="kpi.trendValue"
            :icon="kpi.icon"
            :color="kpi.color"
            :loading="false">
          </kpi-card>
        </template>
      </div>

      <!-- 图表分析区 -->
      <div class="chart-section">
        <div class="chart-container">
          <div class="chart-left">
            <chart-panel
              title="客户分布统计"
              chart-type="mixed"
              :data="chartData"
              @refresh="refreshChartData"
              @period-change="handleChartPeriodChange">
            </chart-panel>
          </div>
          <div class="chart-right">
            <ranking-panel
              :data-source="rankingConfig"
              :initial-data="initialRankingData"
              @refresh="handleRankingRefresh"
              @tab-change="handleRankingTabChange">
            </ranking-panel>
          </div>
        </div>
      </div>

      <!-- 仓库详细区 -->
      <div class="warehouse-section">
        <div class="warehouse-header">
          <h3 class="warehouse-title">仓库详细</h3>
          <div class="warehouse-actions">
            <el-button icon="el-icon-refresh" @click="refreshWarehouseData">
              刷新
            </el-button>
          </div>
        </div>
        
        <warehouse-table
          :data="warehouseData"
          :loading="warehouseLoading"
          @view-detail="handleViewWarehouseDetail"
          @refresh="refreshWarehouseData">
        </warehouse-table>
      </div>

      <!-- 筛选条件区 -->
      <div class="filter-section">
        <!-- <div class="filter-header">
          <h3 class="filter-title">筛选条件</h3>
        </div>
         -->
        <!-- 客户筛选区域 -->
        <customer-filter-bar
          :filters="customerFilters"
          @filter-change="handleCustomerFilterChange"
          @search="handleCustomerSearch"
          @refresh="handleCustomerFilterRefresh"
          @export="handleCustomerFilterExport">
        </customer-filter-bar>
      </div>

      <!-- 数据表格区 -->
      <div class="table-section">
        <div class="table-header">
          <h3 class="table-title">客户列表</h3>
          <div class="table-actions">
            <!-- <el-button type="primary" icon="el-icon-plus" @click="handleAddCustomer">
              新增客户
            </el-button> -->
            <el-button icon="el-icon-refresh" @click="refreshTableData">
              刷新
            </el-button>
          </div>
        </div>
        
        <data-table
          :columns="tableColumns"
          :data="tableData"
          v-loading="tableLoading"
          :pagination="pagination"
          @edit="handleEditCustomer"
          @delete="handleDeleteCustomer"
          @sort-change="handleSortChange"
          @page-change="handlePageChange"
          @page-size-change="handlePageSizeChange"
          @row-dblclick="handleRowDoubleClick">
        </data-table>
      </div>
    </div>
 

  <!-- 客户编辑对话框 -->
  <el-dialog
    :title="dialogTitle"
    :visible.sync="dialogVisible"
    width="600px"
    @close="resetForm">
    <el-form
      ref="customerForm"
      :model="customerForm"
      :rules="formRules"
      label-width="100px">
      <el-form-item label="客户名称" prop="name">
        <el-input v-model="customerForm.name" placeholder="请输入客户名称"></el-input>
      </el-form-item>
      <el-form-item label="联系人" prop="contact">
        <el-input v-model="customerForm.contact" placeholder="请输入联系人"></el-input>
      </el-form-item>
      <el-form-item label="联系电话" prop="phone">
        <el-input v-model="customerForm.phone" placeholder="请输入联系电话"></el-input>
      </el-form-item>
      <el-form-item label="邮箱地址" prop="email">
        <el-input v-model="customerForm.email" placeholder="请输入邮箱地址"></el-input>
      </el-form-item>
      <el-form-item label="客户状态" prop="status">
        <el-select v-model="customerForm.status" placeholder="请选择状态">
          <el-option label="活跃" value="活跃"></el-option>
          <el-option label="待激活" value="待激活"></el-option>
          <el-option label="已停用" value="已停用"></el-option>
        </el-select>
      </el-form-item>
      <el-form-item label="备注信息">
        <el-input
          v-model="customerForm.remark"
          type="textarea"
          :rows="3"
          placeholder="请输入备注信息">
        </el-input>
      </el-form-item>
    </el-form>
    <div slot="footer" class="dialog-footer">
      <el-button @click="dialogVisible = false">取消</el-button>
      <el-button type="primary" @click="saveCustomer" :loading="saveLoading">
        {{ isEdit ? '更新' : '保存' }}
      </el-button>
    </div>
  </el-dialog>
</div>
          <!-- JavaScript 库 -->
  <script type="text/javascript" src="../JS/NewVue/vue.js"></script>
  <script type="text/javascript" src="../JS/NewVue/element-ui.js"></script>
  <script type="text/javascript" src="../JS/NewVue/echarts.min.js"></script>
  <script type="text/javascript" src="../JS/NewVue/axios.min.js"></script>

  <!-- 工具函数 -->
  <script type="text/javascript" src="../JS/CustomerProfileManager/utils/constants.js"></script>
  <script type="text/javascript" src="../JS/CustomerProfileManager/utils/formatter.js"></script>
  <script type="text/javascript" src="../JS/CustomerProfileManager/utils/api.js"></script>
      <!-- 模拟数据 -->
  <script type="text/javascript">
      // 模拟数据文件
      window.MockData = {

          // KPI数据
          kpiData: [
              {
                  id: 'totalUsers',
                  title: '平台用户总数',
                  value: '168,700',
                  subtitle: '同比增长 12,768',
                  trend: 'up',
                  trendValue: '+12.7%',
                  icon: 'el-icon-user',
                  color: 'success'
              },
              {
                  id: 'vipUsers',
                  title: 'VIP客户数',
                  value: '391',
                  subtitle: '同比增长 12,768',
                  trend: 'up',
                  trendValue: '+8.3%',
                  icon: 'el-icon-star-on',
                  color: 'success'
              },
              {
                  id: 'warehouses',
                  title: '仓库数',
                  value: '13',
                  subtitle: '较上月下降3个',
                  trend: 'down',
                  trendValue: '-3%',
                  icon: 'el-icon-house',
                  color: 'danger'
              },
              {
                  id: 'avgShipment',
                  title: '仓库平均出货量',
                  value: '457',
                  subtitle: '较去年同期 409',
                  trend: 'up',
                  trendValue: '+20%',
                  icon: 'el-icon-truck',
                  color: 'success'
              },
              {
                  id: 'conversionRate',
                  title: '用户注册成交率',
                  value: '35.7%',
                  subtitle: '业绩指标 较上月下降2.5%',
                  trend: 'down',
                  trendValue: '-2.5%',
                  icon: 'el-icon-pie-chart',
                  color: 'danger'
              },
              {
                  id: 'vipConversion',
                  title: 'VIP客户成交数',
                  value: '674',
                  subtitle: '营销指标 较上月减少14%',
                  trend: 'down',
                  trendValue: '-14%',
                  icon: 'el-icon-money',
                  color: 'danger'
              }
          ],

          // 图表数据 - 仓库出货量和注册成交率
          chartData: {
              month: {
                  categories: ['湖北仓库', '汉口云台', '光明云台', '欢乐城云台41BAB', '广通星采', '北京仓库', '上海仓库'],
                  series: [
                      {
                          name: '出货量',
                          type: 'bar',
                          data: [4500, 3800, 5000, 2800, 1800, 3600, 5800],
                          yAxisIndex: 0
                      },
                      {
                          name: '注册成交率(%)',
                          type: 'line',
                          data: [68, 62, 75, 45, 35, 58, 78],
                          yAxisIndex: 1
                      }
                  ]
              },
              quarter: {
                  categories: ['湖北仓库', '汉口云台', '光明云台', '欢乐城云台41BAB', '广通星采', '北京仓库', '上海仓库'],
                  series: [
                      {
                          name: '出货量',
                          type: 'bar',
                          data: [13500, 11400, 15000, 8400, 5400, 10800, 17400],
                          yAxisIndex: 0
                      },
                      {
                          name: '注册成交率(%)',
                          type: 'line',
                          data: [72, 65, 78, 48, 38, 61, 82],
                          yAxisIndex: 1
                      }
                  ]
              },
              year: {
                  categories: ['湖北仓库', '汉口云台', '光明云台', '欢乐城云台41BAB', '广通星采', '北京仓库', '上海仓库'],
                  series: [
                      {
                          name: '出货量',
                          type: 'bar',
                          data: [54000, 45600, 60000, 33600, 21600, 43200, 69600],
                          yAxisIndex: 0
                      },
                      {
                          name: '注册成交率(%)',
                          type: 'line',
                          data: [75, 68, 80, 52, 42, 65, 85],
                          yAxisIndex: 1
                      }
                  ]
              }
          },

          // 获取图表数据的方法
          getChartData(period = 'month') {
              return this.chartData[period] || this.chartData.month;
          },

          // 客户数据
          customerData: [
              {
                  id: 103253,
                  name: '范文龙',
                  customerType: '个人客户',
                  contact: '范文龙',
                  phone: '13802578357',
                  warehouse: '湖北仓库',
                  lastOrder: '2025-10-28',
                  recentThreeMonthsOrders: 0,
                  recentThreeMonthsConsumption: 5,
                  recentThreeMonthsBusiness: 2,
                  activeLevel: '中等',
                  preferredBrand: '米其林',
                  preferredSpec: '225/50R18',
                  valueLevel: '低',
                  lastAccess: '2025-11-01 15:30',
                  email: 'fanwenlong@example.com',
                  status: '活跃',
                  createTime: '2024-01-15',
                  remark: '个人客户'
              },
              {
                  id: 320637,
                  name: '杭州天车车站天街',
                  customerType: '个人客户',
                  contact: '黄思忠',
                  phone: '13886508990',
                  warehouse: '汉口云台',
                  lastOrder: '2025-11-01',
                  recentThreeMonthsOrders: 3,
                  recentThreeMonthsConsumption: 8,
                  recentThreeMonthsBusiness: 12,
                  activeLevel: '高',
                  preferredBrand: '韩泰',
                  preferredSpec: '215/55R17',
                  valueLevel: '高',
                  lastAccess: '2025-11-02 10:15',
                  email: 'huangsz@example.com',
                  status: '活跃',
                  createTime: '2024-02-20',
                  remark: '汽车服务客户'
              },
              {
                  id: 838002,
                  name: '嘉兴达汽车维修部',
                  customerType: '个人客户',
                  contact: '胡明',
                  phone: '13612857701',
                  warehouse: '光明云台',
                  lastOrder: '2025-09-15',
                  recentThreeMonthsOrders: 0,
                  recentThreeMonthsConsumption: 2,
                  recentThreeMonthsBusiness: 3,
                  activeLevel: '低',
                  preferredBrand: '倍耐力',
                  preferredSpec: '235/45R18',
                  valueLevel: '低',
                  lastAccess: '2025-10-28 09:40',
                  email: 'huming@example.com',
                  status: '待激活',
                  createTime: '2024-03-10',
                  remark: '汽车维修客户'
              },
              {
                  id: 473697,
                  name: '岐井丰原车田气修',
                  customerType: '个人客户',
                  contact: '卢金梅',
                  phone: '13815912721',
                  warehouse: '欢乐城云台41BAB',
                  lastOrder: '2025-10-30',
                  recentThreeMonthsOrders: 5,
                  recentThreeMonthsConsumption: 12,
                  recentThreeMonthsBusiness: 18,
                  activeLevel: '高',
                  preferredBrand: '固特异',
                  preferredSpec: '245/40R18',
                  valueLevel: '高',
                  lastAccess: '2025-11-03 08:20',
                  email: 'lujinmei@example.com',
                  status: '活跃',
                  createTime: '2024-04-05',
                  remark: '汽车维修客户'
              },
              {
                  id: 385949,
                  name: '钟山汽车服务中心',
                  customerType: '个人客户',
                  contact: '卢宝明14595601418222883',
                  phone: '13815912721',
                  warehouse: '光明云台',
                  lastOrder: '2025-08-20',
                  recentThreeMonthsOrders: 0,
                  recentThreeMonthsConsumption: 3,
                  recentThreeMonthsBusiness: 4,
                  activeLevel: '低',
                  preferredBrand: '马牌',
                  preferredSpec: '205/60R16',
                  valueLevel: '中等',
                  lastAccess: '2025-10-30 16:15',
                  email: 'lubaoming@example.com',
                  status: '活跃',
                  createTime: '2024-05-12',
                  remark: '汽车服务客户'
              },
              {
                  id: 645260,
                  name: '河池李会安',
                  customerType: '个人客户',
                  contact: '河池李会安',
                  phone: '13407788689',
                  warehouse: '广通星采',
                  lastOrder: '2025-11-02',
                  recentThreeMonthsOrders: 2,
                  recentThreeMonthsConsumption: 7,
                  recentThreeMonthsBusiness: 10,
                  activeLevel: '中等',
                  preferredBrand: '优科豪马',
                  preferredSpec: '225/60R17',
                  valueLevel: '中等',
                  lastAccess: '2025-11-03 09:30',
                  email: 'lihuian@example.com',
                  status: '活跃',
                  createTime: '2024-06-18',
                  remark: '个人客户'
              },
              {
                  id: 635956,
                  name: '宏欣汽车服务中心',
                  customerType: '个人客户',
                  contact: '刘康庭',
                  phone: '13593086160',
                  warehouse: '光明云台',
                  lastOrder: '2025-09-25',
                  recentThreeMonthsOrders: 1,
                  recentThreeMonthsConsumption: 4,
                  recentThreeMonthsBusiness: 5,
                  activeLevel: '中等',
                  preferredBrand: '邓禄普',
                  preferredSpec: '215/65R16',
                  valueLevel: '中等',
                  lastAccess: '2025-10-29 14:20',
                  email: 'liukangting@example.com',
                  status: '活跃',
                  createTime: '2024-07-22',
                  remark: '汽车服务客户'
              },
              {
                  id: 426281,
                  name: '光明区之星汽修店',
                  customerType: '个人客户',
                  contact: '李小莉',
                  phone: '15989301801',
                  warehouse: '光明云台',
                  lastOrder: '2025-10-15',
                  recentThreeMonthsOrders: 1,
                  recentThreeMonthsConsumption: 6,
                  recentThreeMonthsBusiness: 8,
                  activeLevel: '中等',
                  preferredBrand: '玲珑',
                  preferredSpec: '225/55R18',
                  valueLevel: '中等',
                  lastAccess: '2025-11-01 11:45',
                  email: 'lixiaoli@example.com',
                  status: '活跃',
                  createTime: '2024-08-30',
                  remark: '汽修店客户'
              },
              {
                  id: 830746,
                  name: '德宝汽车维修服务',
                  customerType: '个人客户',
                  contact: '江宇霸',
                  phone: '17827179664',
                  warehouse: '光明云台',
                  lastOrder: '2025-06-15',
                  recentThreeMonthsOrders: 0,
                  recentThreeMonthsConsumption: 0,
                  recentThreeMonthsBusiness: 0,
                  activeLevel: '沉睡',
                  preferredBrand: '倍耐力',
                  preferredSpec: '235/45R19',
                  valueLevel: '低',
                  lastAccess: '2025-09-10 11:20',
                  email: 'jiangyuba@example.com',
                  status: '活跃',
                  createTime: '2024-09-15',
                  remark: '汽车维修客户'
              },
          ],

          // 生成更多模拟数据的方法
          generateMoreCustomers(count = 50) {
              const names = [
                  '科技有限公司', '贸易集团', '制造企业', '创新公司', '电商平台',
                  '软件开发', '教育机构', '医疗设备', '物流公司', '海鲜贸易',
                  '火锅连锁', '港务集团', '纺织企业', '机械制造', '化工集团',
                  '旅游公司', '醋业集团', '医药公司', '电商园区', '农业合作社'
              ];

              const cities = [
                  '北京', '上海', '广州', '深圳', '杭州', '成都', '西安', '南京',
                  '青岛', '大连', '重庆', '天津', '苏州', '无锡', '常州', '扬州',
                  '镇江', '泰州', '宿迁', '淮安', '武汉', '长沙', '郑州', '济南'
              ];

              const contacts = [
                  '张经理', '李总监', '王主管', '刘经理', '陈总', '赵工程师',
                  '孙校长', '周医生', '吴经理', '郑老板', '冯店长', '何主任',
                  '沈总经理', '韩工程师', '杨博士', '朱导游', '秦厂长', '尤研究员',
                  '许主管', '史社长', '钱总', '孙经理', '李主任', '王总监'
              ];

              const customerTypes = ['企业客户', '个人客户', 'VIP客户'];
              const statuses = ['活跃', '待激活', '已停用'];

              const additionalCustomers = [];

              for (let i = 0; i < count; i++) {
                  const id = this.customerData.length + i + 1;
                  const cityIndex = Math.floor(Math.random() * cities.length);
                  const nameIndex = Math.floor(Math.random() * names.length);
                  const contactIndex = Math.floor(Math.random() * contacts.length);

                  // 生成随机日期（过去一年内）
                  const randomDate = new Date();
                  randomDate.setDate(randomDate.getDate() - Math.floor(Math.random() * 365));

                  additionalCustomers.push({
                      id,
                      name: `${cities[cityIndex]}${names[nameIndex]}`,
                      contact: contacts[contactIndex],
                      phone: `138${String(Math.floor(Math.random() * 100000000)).padStart(8, '0')}`,
                      email: `contact${id}@example.com`,
                      customerType: customerTypes[Math.floor(Math.random() * customerTypes.length)],
                      status: statuses[Math.floor(Math.random() * statuses.length)],
                      createTime: randomDate.toISOString().split('T')[0],
                      remark: `客户备注信息 ${id}`
                  });
              }

              return additionalCustomers;
          },

          // 获取分页数据
          getPagedCustomers(page = 1, pageSize = 20, filters = {}) {
              let filteredData = [...this.customerData];

              // 应用筛选条件
              if (filters.keyword) {
                  const keyword = filters.keyword.toLowerCase();
                  filteredData = filteredData.filter(customer =>
                      customer.name.toLowerCase().includes(keyword) ||
                      customer.contact.toLowerCase().includes(keyword) ||
                      customer.phone.includes(keyword) ||
                      (customer.email && customer.email.toLowerCase().includes(keyword))
                  );
              }

              // 客户名称筛选
              if (filters.customerName) {
                  const name = filters.customerName.toLowerCase();
                  filteredData = filteredData.filter(customer =>
                      customer.name.toLowerCase().includes(name)
                  );
              }

              // 客户编码筛选（使用ID作为编码）
              if (filters.customerCode) {
                  filteredData = filteredData.filter(customer =>
                      customer.id.toString().includes(filters.customerCode)
                  );
              }

              // 手机号码筛选
              if (filters.phoneNumber) {
                  filteredData = filteredData.filter(customer =>
                      customer.phone.includes(filters.phoneNumber)
                  );
              }

              // 所属仓库筛选
              if (filters.warehouse) {
                  filteredData = filteredData.filter(customer => customer.warehouse === filters.warehouse);
              }

              // 客户类型筛选
              if (filters.customerType) {
                  filteredData = filteredData.filter(customer => customer.customerType === filters.customerType);
              }

              // 活跃水平筛选
              if (filters.activeLevel) {
                  filteredData = filteredData.filter(customer => customer.activeLevel === filters.activeLevel);
              }

              // 偏好品牌筛选
              if (filters.preferredBrand) {
                  filteredData = filteredData.filter(customer => customer.preferredBrand === filters.preferredBrand);
              }

              // 偏好规格筛选
              if (filters.preferredSpec) {
                  filteredData = filteredData.filter(customer => customer.preferredSpec === filters.preferredSpec);
              }

              // 价值水平筛选
              if (filters.valueLevel) {
                  filteredData = filteredData.filter(customer => customer.valueLevel === filters.valueLevel);
              }

              if (filters.status) {
                  filteredData = filteredData.filter(customer => customer.status === filters.status);
              }

              if (filters.dateRange && filters.dateRange.length === 2) {
                  const [startDate, endDate] = filters.dateRange;
                  filteredData = filteredData.filter(customer => {
                      const createTime = new Date(customer.createTime);
                      return createTime >= new Date(startDate) && createTime <= new Date(endDate);
                  });
              }

              // 计算分页
              const total = filteredData.length;
              const startIndex = (page - 1) * pageSize;
              const endIndex = startIndex + pageSize;
              const data = filteredData.slice(startIndex, endIndex);

              return {
                  data,
                  total,
                  page,
                  pageSize,
                  totalPages: Math.ceil(total / pageSize)
              };
          },

          // 获取统计数据
          getStatistics() {
              const total = this.customerData.length;
              const active = this.customerData.filter(c => c.status === '活跃').length;
              const pending = this.customerData.filter(c => c.status === '待激活').length;
              const disabled = this.customerData.filter(c => c.status === '已停用').length;

              return {
                  total,
                  active,
                  pending,
                  disabled,
                  activeRate: ((active / total) * 100).toFixed(1),
                  pendingRate: ((pending / total) * 100).toFixed(1),
                  disabledRate: ((disabled / total) * 100).toFixed(1)
              };
          },

          // 获取客户类型分布
          getCustomerTypeDistribution() {
              const distribution = {};
              this.customerData.forEach(customer => {
                  const type = customer.customerType;
                  distribution[type] = (distribution[type] || 0) + 1;
              });

              return Object.keys(distribution).map(key => ({
                  name: key,
                  value: distribution[key]
              }));
          },

          // 获取月度新增客户趋势
          getMonthlyTrend() {
              const monthlyData = {};

              this.customerData.forEach(customer => {
                  const date = new Date(customer.createTime);
                  const monthKey = `${date.getFullYear()}-${String(date.getMonth() + 1).padStart(2, '0')}`;
                  monthlyData[monthKey] = (monthlyData[monthKey] || 0) + 1;
              });

              return Object.keys(monthlyData)
                  .sort()
                  .slice(-12) // 最近12个月
                  .map(key => ({
                      name: key,
                      value: monthlyData[key]
                  }));
          },

          // 仓库详细数据
          warehouseData: [
              {
                  id: 1,
                  name: '湖北仓库',
                  shipmentVolume: 4562,
                  registeredUsers: 2580,
                  avgShipmentPerUser: 1.77,
                  conversionRate: '68.5%',
                  location: '湖北省武汉市',
                  manager: '张经理',
                  phone: '027-88888888',
                  status: '正常运营'
              },
              {
                  id: 2,
                  name: '汉口云台',
                  shipmentVolume: 3854,
                  registeredUsers: 2200,
                  avgShipmentPerUser: 1.75,
                  conversionRate: '73.3%',
                  location: '湖北省武汉市汉口区',
                  manager: '李主管',
                  phone: '027-77777777',
                  status: '正常运营'
              },
              {
                  id: 3,
                  name: '光明云台',
                  shipmentVolume: 5210,
                  registeredUsers: 3150,
                  avgShipmentPerUser: 1.65,
                  conversionRate: '65.8%',
                  location: '广东省深圳市光明区',
                  manager: '王总监',
                  phone: '0755-66666666',
                  status: '正常运营'
              },
              {
                  id: 4,
                  name: '欢乐城云台41BAB',
                  shipmentVolume: 2987,
                  registeredUsers: 1650,
                  avgShipmentPerUser: 1.81,
                  conversionRate: '75.1%',
                  location: '江苏省南京市',
                  manager: '刘经理',
                  phone: '025-55555555',
                  status: '正常运营'
              },
              {
                  id: 5,
                  name: '广通星采',
                  shipmentVolume: 4123,
                  registeredUsers: 2350,
                  avgShipmentPerUser: 1.76,
                  conversionRate: '70.2%',
                  location: '云南省楚雄州广通县',
                  manager: '陈主任',
                  phone: '0878-44444444',
                  status: '正常运营'
              },
              {
                  id: 6,
                  name: '北京仓库',
                  shipmentVolume: 3578,
                  registeredUsers: 2050,
                  avgShipmentPerUser: 1.74,
                  conversionRate: '69.4%',
                  location: '北京市朝阳区',
                  manager: '赵经理',
                  phone: '010-33333333',
                  status: '正常运营'
              },
              {
                  id: 7,
                  name: '上海仓库',
                  shipmentVolume: 5890,
                  registeredUsers: 3350,
                  avgShipmentPerUser: 1.76,
                  conversionRate: '71.6%',
                  location: '上海市浦东新区',
                  manager: '孙总监',
                  phone: '021-22222222',
                  status: '正常运营'
              }
          ],

          // 获取仓库数据
          getWarehouseData() {
              return this.warehouseData;
          },

          // 获取仓库统计数据
          getWarehouseStatistics() {
              const data = this.warehouseData;
              const totalShipment = data.reduce((sum, item) => sum + item.shipmentVolume, 0);
              const totalUsers = data.reduce((sum, item) => sum + item.registeredUsers, 0);
              const avgConversionRate = data.reduce((sum, item) => {
                  return sum + parseFloat(item.conversionRate.replace('%', ''));
              }, 0) / data.length;

              return {
                  totalWarehouses: data.length,
                  totalShipment,
                  totalUsers,
                  avgShipmentPerWarehouse: Math.round(totalShipment / data.length),
                  avgConversionRate: avgConversionRate.toFixed(1) + '%',
                  topPerformingWarehouse: data.reduce((max, item) => {
                      const currentRate = parseFloat(item.conversionRate.replace('%', ''));
                      const maxRate = parseFloat(max.conversionRate.replace('%', ''));
                      return currentRate > maxRate ? item : max;
                  })
              };
          },

          // 排行榜数据
          rankingData: {
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
          },

          /**
           * 获取排行榜数据
           * @param {string} type 排行榜类型 (active|value|product)
           * @param {object} params 查询参数
           * @returns {object} 排行榜数据
           */
          getRankingData(type, params = {}) {
              const limit = params.limit || 10;
              let data = [];

              switch (type) {
                  case 'active':
                      data = this.rankingData.userActiveRanking.slice(0, limit);
                      break;
                  case 'value':
                      data = this.rankingData.userValueRanking.slice(0, limit);
                      break;
                  case 'product':
                      data = this.rankingData.productRanking.slice(0, limit);
                      break;
                  default:
                      console.warn(`未知的排行榜类型: ${type}`);
                      return {
                          success: false,
                          data: [],
                          message: '未知的排行榜类型'
                      };
              }

              return {
                  success: true,
                  data: {
                      rankings: data,
                      updateTime: new Date().toLocaleString(),
                      total: data.length
                  },
                  message: '获取成功'
              };
          },

          /**
           * 生成随机排行榜数据（用于测试）
           * @param {string} type 排行榜类型
           * @param {number} count 生成数量
           * @returns {array} 排行榜数据
           */
          generateRandomRankingData(type, count = 10) {
              const data = [];

              if (type === 'active') {
                  const names = [
                      '科技有限公司', '贸易集团', '制造企业', '创新公司', '电商平台',
                      '软件开发', '教育机构', '医疗设备', '物流公司', '海鲜贸易'
                  ];

                  for (let i = 0; i < count; i++) {
                      data.push({
                          name: `${names[i % names.length]}${i + 1}`,
                          views: Math.floor(Math.random() * 50) + 1
                      });
                  }

                  // 按浏览次数排序
                  data.sort((a, b) => b.views - a.views);

              } else if (type === 'value') {
                  const names = [
                      '优质客户', '重点合作伙伴', '战略客户', '核心客户', '金牌客户',
                      '钻石客户', '白金客户', '黄金客户', '银牌客户', '铜牌客户'
                  ];

                  for (let i = 0; i < count; i++) {
                      data.push({
                          name: `${names[i % names.length]}${i + 1}`,
                          orders: Math.floor(Math.random() * 100) + 1
                      });
                  }

                  // 按订单数排序
                  data.sort((a, b) => b.orders - a.orders);

              } else if (type === 'product') {
                  const brands = ['博世', '瓦尔塔', '统一', '风帆', '骆驼', '理士', '汤浅', '超威', '天能', '德尔福'];
                  const specs = ['12V 45Ah', '12V 50Ah', '12V 55Ah', '12V 60Ah', '12V 65Ah', '12V 70Ah', '12V 75Ah', '12V 80Ah', '12V 90Ah', '12V 100Ah'];

                  for (let i = 0; i < count; i++) {
                      data.push({
                          code: `P${String(i + 1).padStart(3, '0')}`,
                          brand: brands[i % brands.length],
                          spec: specs[i % specs.length],
                          sales: Math.floor(Math.random() * 200) + 10
                      });
                  }

                  // 按销量排序
                  data.sort((a, b) => b.sales - a.sales);
              }

              return data;
          }
      };

      // 使用图片中的真实数据，不再生成额外的模拟数据
  </script>
         <!-- Vue 组件 -->
  <script type="text/javascript" src="../JS/CustomerProfileManager/components/ChartPanel.js"></script>
  <script type="text/javascript" src="../JS/CustomerProfileManager/components/KpiCard.js"></script>
  <script type="text/javascript" src="../JS/CustomerProfileManager/components/ChartPanel.js"></script>
  <script type="text/javascript" src="../JS/CustomerProfileManager/components/RankingPanel.js"></script>
  <script type="text/javascript" src="../JS/CustomerProfileManager/components/DataTable.js"></script>
  <script type="text/javascript" src="../JS/CustomerProfileManager/components/WarehouseTable.js"></script>
  <script type="text/javascript" src="../JS/CustomerProfileManager/components/CustomerFilterBar.js"></script>

  <!-- Vue 应用 -->
    <script type="text/javascript" src="../JS/CustomerProfileManager/app.js"></script>
</asp:Content>
