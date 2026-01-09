<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="REDataPanel.aspx.cs" Inherits="Cargo.HuiCai.REDataPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>RE数据仪表板</title>
     <link href="../JS/NewVue/element-ui/css/index.css" rel="stylesheet" />
    <link href="../JS/REDataPanel/REDataPanel.css" rel="stylesheet" />
    <!-- CDN引入顺序（必须严格按照指定链接） -->
      <script type="text/javascript" src="../JS/NewVue/vue.js"></script>
   <script type="text/javascript" src="../JS/NewVue/element-ui.js"></script>
  <script type="text/javascript" src="../JS/NewVue/echarts.min.js"></script>
      <script type="text/javascript" src="../JS/NewVue/axios.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="app">
        <el-container>
            <el-main>
                <!-- 标题区域 -->
                <div class="dashboard-header">
                    <div class="dashboard-header-left">
                        <h1>RE业务数据概览</h1>
                        <span class="update-time">数据最后更新：2025-12-31 18:30:45</span>
                    </div>
                    <div class="dashboard-header-right">
                        <!-- 筛选控件区域 -->
                        <div class="filter-controls">
                            <!-- 在新标签页打开按钮（仅在iframe中显示） -->
                            <el-button
                                v-if="isInIFrame"
                                type="primary"
                                icon="el-icon-link"
                                size="small"
                                @click="openInNewTab">
                                在新标签页打开
                            </el-button>
                            
                            <div class="filter-divider" v-if="isInIFrame"></div>
                            
                            <!-- 品牌选择框 -->
                            <div class="filter-item">
                                <span class="filter-label">品牌</span>
                                <el-select
                                    v-model="filterParams.brand"
                                    :multiple="filterParams.brandMultiple"
                                    :collapse-tags="filterParams.brandMultiple"
                                    filterable
                                    clearable
                                    placeholder="请选择品牌"
                                    class="brand-select"  style="width: 170px;"
                                    size="small">
                                    <el-option
                                        v-for="brand in brandOptions"
                                        :key="brand.value"
                                        :label="brand.label"
                                        :value="brand.value">
                                        <span style="float: left">{{ brand.label }}</span>
                                        <span style="float: right; color: #8492a6; font-size: 12px">{{ brand.code }}</span>
                                    </el-option>
                                </el-select>
                                <!-- <el-tooltip content="切换单选/多选模式" placement="top">
                                    <el-button
                                        :type="brandMultiple ? 'primary' : 'default'"
                                        icon="el-icon-menu"
                                        size="mini"
                                        circle
                                        @click="brandMultiple = !brandMultiple; selectedBrand = brandMultiple ? [] : ''">
                                    </el-button>
                                </el-tooltip> -->
                            </div>
                            
                            <div class="filter-divider"></div>
                            
                            <!-- 时间粒度单选按钮组 -->
                            <div class="filter-item">
                                <span class="filter-label">时间粒度</span>
                                <el-radio-group
                                    v-model="filterParams.timeGranularity"
                                    size="small"
                                    class="time-granularity-group"
                                    @change="handleGranularityChange">
                                    <el-radio-button label="day">日</el-radio-button>
                                    <el-radio-button label="month">月</el-radio-button>
                                    <el-radio-button label="year">年</el-radio-button>
                                </el-radio-group>
                            </div>
                            
                            <div class="filter-divider"></div>
                            
                            <!-- 联动的时间范围选择器 -->
                            <div class="filter-item time-range-picker">
                                <span class="filter-label">时间范围</span>
                                <!-- 日期范围选择器（日粒度） -->
                                <el-date-picker
                                    v-if="filterParams.timeGranularity === 'day'"
                                    v-model="filterParams.dateRange"
                                    type="daterange"
                                    align="right"
                                    unlink-panels
                                    range-separator="至"
                                    start-placeholder="开始日期"
                                    end-placeholder="结束日期"
                                    size="small"
                                    :picker-options="dayPickerOptions"
                                    value-format="yyyy-MM-dd" style="width: 300px;"
                                    format="yyyy-MM-dd">
                                </el-date-picker>
                                <!-- 月份范围选择器（月粒度） -->
                                <el-date-picker
                                    v-else-if="filterParams.timeGranularity === 'month'"
                                    v-model="filterParams.monthRange"
                                    type="monthrange"
                                    align="right"
                                    unlink-panels
                                    range-separator="至"
                                    start-placeholder="开始月份"
                                    end-placeholder="结束月份"
                                    size="small"
                                    :picker-options="monthPickerOptions"
                                    value-format="yyyy-MM" style="width: 270px;"
                                    format="yyyy年MM月">
                                </el-date-picker>
                                <!-- 年份范围选择器（年粒度） -->
                                <div v-else class="year-range-picker">
                                    <el-date-picker
                                        v-model="filterParams.yearStart"
                                        type="year"
                                        placeholder="开始年份"
                                        size="small"
                                        :picker-options="yearStartPickerOptions"
                                        value-format="yyyy"
                                        format="yyyy年"
                                        style="width: 120px;">
                                    </el-date-picker>
                                    <span style="margin: 0 8px; color: #606266;">至</span>
                                    <el-date-picker
                                        v-model="filterParams.yearEnd"
                                        type="year"
                                        placeholder="结束年份"
                                        size="small"
                                        :picker-options="yearEndPickerOptions"
                                        value-format="yyyy"
                                        format="yyyy年"
                                        style="width: 120px;">
                                    </el-date-picker>
                                </div>
                            </div>
                        </div>
                        
                        <!-- 查询按钮卡片 -->
                        <div class="query-btn-card">
                            <el-button type="button" icon="el-icon-search" @click="handleQuery">查询</el-button>
                        </div>
                    </div>
                </div>
                
                <!-- 六大指标卡片 - 单行排列 -->
                <div class="metrics-container">
                    <div class="metric-card">
                        <div class="metric-icon blue">
                            <i class="el-icon-s-goods"></i>
                        </div>
                        <div class="metric-value">{{ metricData.totalSalesVolume.toLocaleString() }}</div>
                        <div class="metric-title">全渠道销量</div>
                    </div>
                    <div class="metric-card">
                        <div class="metric-icon orange">
                            <i class="el-icon-box"></i>
                        </div>
                        <div class="metric-value">{{ metricData.inStockInventory.toLocaleString() }}</div>
                        <div class="metric-title">在库库存</div>
                    </div>
                    <div class="metric-card">
                        <div class="metric-icon blue">
                            <i class="el-icon-data-line"></i>
                        </div>
                        <div class="metric-value">{{ metricData.salesRate }}%</div>
                        <div class="metric-title">动销率</div>
                    </div>
                    <div class="metric-card">
                        <div class="metric-icon green">
                            <i class="el-icon-money"></i>
                        </div>
                        <div class="metric-value green">{{ formatCurrency(metricData.totalSalesAmount) }}</div>
                        <div class="metric-title">销售总额</div>
                    </div>
                    <div class="metric-card">
                        <div class="metric-icon red">
                            <i class="el-icon-wallet"></i>
                        </div>
                        <div class="metric-value red">{{ formatCurrency(metricData.unsettledAmount) }}</div>
                        <div class="metric-title">未结算金额</div>
                    </div>
                    <div class="metric-card">
                        <div class="metric-icon purple">
                            <i class="el-icon-pie-chart"></i>
                        </div>
                        <div class="metric-value purple">{{ metricData.grossProfitRate }}%</div>
                        <div class="metric-title">毛利率</div>
                    </div>
                </div>
                
                <!-- 第3行：渠道月销量统计表格 + 渠道月销量统计图表 -->
                <el-row :gutter="20" justify="start">
                    <el-col :span="12">
                        <el-card shadow="hover">
                            <div slot="header">
                                <span class="card-header-title">
                                    <i class="el-icon-s-grid blue"></i>
                                    渠道月销量统计表格
                                </span>
                            </div>
                            <el-table
                                :data="channelTableData"
                                :header-cell-style="{background:'#f5f7fa',color:'#606266'}"
                                height="300">
                                <el-table-column label="渠道" width="140">
                                    <template slot-scope="scope">
                                        <el-tag :type="scope.row.tagType" size="medium">{{ scope.row.channel }}</el-tag>
                                    </template>
                                </el-table-column>
                                <el-table-column prop="month" label="月份" width="90"></el-table-column>
                                <el-table-column prop="sales" label="销量" width="80" align="right"></el-table-column>
                                <el-table-column prop="target" label="月目标" width="80" align="right"></el-table-column>
                                <el-table-column prop="completion" label="月完成率" width="90" align="center"></el-table-column>
                                <el-table-column prop="lastMonth" label="上月销量" width="90" align="right"></el-table-column>
                                <el-table-column prop="monthGrowth" label="环比增长" width="90" align="center"></el-table-column>
                                <el-table-column prop="yearGrowth" label="同比增长" width="90" align="center"></el-table-column>
                                <el-table-column prop="yearTotal" label="年度累计" width="90" align="right"></el-table-column>
                                <el-table-column prop="marketShare" label="市场占比" width="90" align="center"></el-table-column>
                                <el-table-column prop="customers" label="客户数" width="80" align="right"></el-table-column>
                            </el-table>
                        </el-card>
                    </el-col>
                    <el-col :span="12">
                        <el-card shadow="hover">
                            <div slot="header">
                                <span class="card-header-title">
                                    <i class="el-icon-s-data green"></i>
                                    渠道月销量统计图表
                                </span>
                            </div>
                            <div ref="channelChart" class="chart-container" style="height:300px;"></div>
                        </el-card>
                    </el-col>
                </el-row>
                
                <!-- 第4行：各渠道月销量统计 + 仓库月出库量数据 -->
                <el-row :gutter="20" justify="start">
                    <el-col :span="12">
                        <el-card shadow="hover">
                            <div slot="header">
                                <span class="card-header-title">
                                    <i class="el-icon-s-marketing orange"></i>
                                    各渠道月销量统计
                                </span>
                            </div>
                            <div class="table-scroll" style="height:400px;">
                                <el-table
                                    :data="allChannelTableData"
                                    :header-cell-style="{background:'#f5f7fa',color:'#606266'}"
                                    height="400">
                                    <el-table-column prop="rank" label="排名" width="60" align="center"></el-table-column>
                                    <el-table-column label="渠道" width="120">
                                        <template slot-scope="scope">
                                            <el-tag :type="scope.row.tagType" size="small">{{ scope.row.channel }}</el-tag>
                                        </template>
                                    </el-table-column>
                                    <el-table-column prop="sales" label="2025-12销量" width="100" align="right"></el-table-column>
                                    <el-table-column prop="target" label="月目标" width="90" align="right"></el-table-column>
                                    <el-table-column prop="completion" label="月完成率" width="90" align="center"></el-table-column>
                                    <el-table-column prop="lastMonth" label="上月销量" width="90" align="right"></el-table-column>
                                    <el-table-column prop="monthGrowth" label="环比增长" width="90" align="center"></el-table-column>
                                    <el-table-column prop="yearTotal" label="年度累计" width="100" align="right"></el-table-column>
                                    <el-table-column prop="avgPrice" label="平均单价" width="90" align="right"></el-table-column>
                                    <el-table-column prop="revenue" label="销售额" width="100" align="right"></el-table-column>
                                    <el-table-column prop="yearGrowth" label="同比增长" width="90" align="center"></el-table-column>
                                    <el-table-column prop="inventory" label="库存量" width="90" align="right"></el-table-column>
                                    <el-table-column prop="turnoverRate" label="周转率" width="80" align="center"></el-table-column>
                                    <el-table-column prop="customers" label="客户数" width="80" align="right"></el-table-column>
                                    <el-table-column prop="orders" label="订单数" width="80" align="right"></el-table-column>
                                    <el-table-column prop="avgOrder" label="客单价" width="90" align="right"></el-table-column>
                                    <el-table-column prop="marketShare" label="市场占比" width="90" align="center"></el-table-column>
                                </el-table>
                            </div>
                        </el-card>
                    </el-col>
                    <el-col :span="12">
                        <el-card shadow="hover">
                            <div slot="header">
                                <span class="card-header-title">
                                    <i class="el-icon-s-home blue"></i>
                                    仓库月出库量数据
                                </span>
                            </div>
                            <div ref="warehouseChart" class="chart-container" style="height:400px;"></div>
                        </el-card>
                    </el-col>
                </el-row>
                
                <!-- 第5行：日总销量数据 -->
                <el-row :gutter="20" justify="start">
                    <el-col :span="24">
                        <el-card shadow="hover">
                            <div slot="header">
                                <span class="card-header-title">
                                    <i class="el-icon-date green"></i>
                                    日总销量数据
                                </span>
                            </div>
                            <div ref="dailySalesChart" class="chart-container" style="height:400px;"></div>
                        </el-card>
                    </el-col>
                </el-row>
                
                <!-- 第6行：区域销量完成情况表格 + 区域内完成情况图表 -->
                <el-row :gutter="20" justify="start">
                    <el-col :span="12">
                        <el-card shadow="hover">
                            <div slot="header">
                                <span class="card-header-title">
                                    <i class="el-icon-location-outline orange"></i>
                                    区域销量完成情况
                                </span>
                            </div>
                            <el-table
                                :data="regionTableData"
                                :header-cell-style="{background:'#f5f7fa',color:'#606266'}"
                                height="350">
                                <el-table-column prop="rank" label="序号" width="60" align="center"></el-table-column>
                                <el-table-column label="标题" width="120">
                                    <template slot-scope="scope">
                                        <el-tag type="primary" size="small">{{ scope.row.title }}</el-tag>
                                    </template>
                                </el-table-column>
                                <el-table-column label="目标区域" width="90">
                                    <template slot-scope="scope">
                                        <el-tag type="danger" size="small">{{ scope.row.region }}</el-tag>
                                    </template>
                                </el-table-column>
                                <el-table-column prop="target" label="目标销量" width="90" align="right"></el-table-column>
                                <el-table-column prop="actual" label="实际销量" width="90" align="right"></el-table-column>
                                <el-table-column prop="completion" label="完成率" width="90" align="center"></el-table-column>
                                <el-table-column prop="amount" label="销售金额" width="110" align="right"></el-table-column>
                                <el-table-column prop="status" label="状态" width="80" align="center">
                                    <template slot-scope="scope">
                                        <el-tag :type="scope.row.statusType" size="small">{{ scope.row.status }}</el-tag>
                                    </template>
                                </el-table-column>
                            </el-table>
                        </el-card>
                    </el-col>
                    <el-col :span="12">
                        <el-card shadow="hover">
                            <div slot="header">
                                <span class="card-header-title">
                                    <i class="el-icon-s-flag blue"></i>
                                    区域内完成情况
                                </span>
                            </div>
                            <div ref="regionChart" class="chart-container"></div>
                        </el-card>
                    </el-col>
                </el-row>
                
                <!-- 第7行：未结算订单明细 + 各类通未结算金额 -->
                <el-row :gutter="20" justify="start">
                    <el-col :span="12">
                        <el-card shadow="hover">
                            <div slot="header">
                                <span class="card-header-title">
                                    <i class="el-icon-document red"></i>
                                    未结算订单明细
                                </span>
                            </div>
                            <div class="table-scroll" style="height:350px;">
                                <el-table
                                    :data="orderTableData"
                                    :header-cell-style="{background:'#f5f7fa',color:'#606266'}"
                                    height="350">
                                    <el-table-column prop="rank" label="排名" width="70" align="center"></el-table-column>
                                    <el-table-column prop="warehouse" label="出库仓库" width="100"></el-table-column>
                                    <el-table-column prop="orderType" label="订单类型" width="100">
                                        <template slot-scope="scope">
                                            <span :class="{'order-type-red': scope.row.orderType === 'OES客户端'}">{{ scope.row.orderType }}</span>
                                        </template>
                                    </el-table-column>
                                    <el-table-column prop="orderNo" label="订单编号" width="150"></el-table-column>
                                    <el-table-column prop="amount" label="合计金额" width="120" align="right"></el-table-column>
                                    <el-table-column prop="customer" label="客户名称"></el-table-column>
                                </el-table>
                            </div>
                        </el-card>
                    </el-col>
                    <el-col :span="12">
                        <el-card shadow="hover">
                            <div slot="header">
                                <span class="card-header-title">
                                    <i class="el-icon-money red"></i>
                                    各类通未结算金额
                                </span>
                            </div>
                            <div ref="unsettledChart" class="chart-container"></div>
                        </el-card>
                    </el-col>
                </el-row>
                
                <!-- 第8行：渠道毛利率列表 + 渠道毛利分析 -->
                <el-row :gutter="20" justify="start">
                    <el-col :span="12">
                        <el-card shadow="hover">
                            <div slot="header">
                                <span class="card-header-title">
                                    <i class="el-icon-s-order green"></i>
                                    渠道毛利率列表
                                </span>
                            </div>
                            <el-table
                                :data="profitTableData"
                                :header-cell-style="{background:'#f5f7fa',color:'#606266'}"
                                height="350">
                                <el-table-column prop="rank" label="序号" width="70" align="center"></el-table-column>
                                <el-table-column label="销售渠道" width="120">
                                    <template slot-scope="scope">
                                        <el-tag :type="scope.row.tagType" size="small">{{ scope.row.channel }}</el-tag>
                                    </template>
                                </el-table-column>
                                <el-table-column prop="sales" label="销量" width="90" align="right"></el-table-column>
                                <el-table-column prop="revenue" label="销售金额" width="120" align="right"></el-table-column>
                                <el-table-column prop="cost" label="成本金额" width="120" align="right"></el-table-column>
                            </el-table>
                        </el-card>
                    </el-col>
                    <el-col :span="12">
                        <el-card shadow="hover">
                            <div slot="header">
                                <span class="card-header-title">
                                    <i class="el-icon-s-finance green"></i>
                                    渠道毛利分析
                                </span>
                            </div>
                            <div ref="profitChart" class="chart-container"></div>
                        </el-card>
                    </el-col>
                </el-row>
                
                <!-- 第9行：仓库超期库存数 + 超期库存占比 -->
                <el-row :gutter="20" justify="start">
                    <el-col :span="17">
                        <el-card shadow="hover">
                            <div slot="header">
                                <span class="card-header-title">
                                    <i class="el-icon-warning-outline orange"></i>
                                    仓库超期库存数
                                </span>
                            </div>
                            <div ref="overdueInventoryChart" class="chart-container"></div>
                        </el-card>
                    </el-col>
                    <el-col :span="7">
                        <el-card shadow="hover">
                            <div slot="header">
                                <span class="card-header-title">
                                    <i class="el-icon-pie-chart purple"></i>
                                    超期库存占比
                                </span>
                            </div>
                            <div ref="overdueRatioChart" class="chart-container"></div>
                        </el-card>
                    </el-col>
                </el-row>
                
            </el-main>
        </el-container>
    </div>
    
    <script>
        // 初始化Vue应用
        new Vue({
            el: '#app',
            data() {
                return {
                    // 检测是否在iframe中
                    isInIFrame: false,

                    // 查询条件（统一管理）
                    filterParams: {
                        brand: '-1',  // 品牌选择，默认"全部"
                        brandMultiple: false,  // 是否多选模式
                        timeGranularity: 'day',  // 时间粒度：day, month, year
                        dateRange: [],  // 日期范围（日粒度）
                        monthRange: [],  // 月份范围（月粒度）
                        yearStart: '',  // 开始年份（年粒度）
                        yearEnd: ''  // 结束年份（年粒度）
                    },

                    // 六大指标数据（后端返回纯数值，统一放在一个对象中）
                    metricData: {
                        totalSalesVolume: 0,
                        inStockInventory: 0,
                        salesRate: 0,
                        totalSalesAmount: 0,
                        unsettledAmount: 0,
                        grossProfitRate: 0
                    },
                    brandOptions: [],
                    // 日期选择器快捷选项
                    dayPickerOptions: {
                        shortcuts: [{
                            text: '最近一周',
                            onClick(picker) {
                                const end = new Date();
                                const start = new Date();
                                start.setTime(start.getTime() - 3600 * 1000 * 24 * 7);
                                picker.$emit('pick', [start, end]);
                            }
                        }, {
                            text: '最近一个月',
                            onClick(picker) {
                                const end = new Date();
                                const start = new Date();
                                start.setTime(start.getTime() - 3600 * 1000 * 24 * 30);
                                picker.$emit('pick', [start, end]);
                            }
                        }, {
                            text: '最近三个月',
                            onClick(picker) {
                                const end = new Date();
                                const start = new Date();
                                start.setTime(start.getTime() - 3600 * 1000 * 24 * 90);
                                picker.$emit('pick', [start, end]);
                            }
                        }]
                    },
                    // 月份选择器快捷选项
                    monthPickerOptions: {
                        shortcuts: [{
                            text: '本月',
                            onClick(picker) {
                                const start = new Date();
                                const end = new Date();
                                start.setDate(1);
                                picker.$emit('pick', [start, end]);
                            }
                        }, {
                            text: '今年至今',
                            onClick(picker) {
                                const end = new Date();
                                const start = new Date(new Date().getFullYear(), 0);
                                picker.$emit('pick', [start, end]);
                            }
                        }, {
                            text: '最近六个月',
                            onClick(picker) {
                                const end = new Date();
                                const start = new Date();
                                start.setMonth(start.getMonth() - 6);
                                picker.$emit('pick', [start, end]);
                            }
                        }]
                    },

                    // 渠道月销量统计表格数据
                    channelTableData: [
                        { channel: 'OES客户端', tagType: 'danger', month: '2025-12', sales: 2869, target: 2000, completion: '143%', lastMonth: 2456, monthGrowth: '+16.8%', yearGrowth: '+23.5%', yearTotal: 28456, marketShare: '52.3%', customers: 156 },
                        { channel: '客户自产', tagType: '', month: '2025-12', sales: 434, target: 800, completion: '54%', lastMonth: 512, monthGrowth: '-15.2%', yearGrowth: '+8.3%', yearTotal: 5234, marketShare: '9.6%', customers: 42 },
                        { channel: '极速达', tagType: 'warning', month: '2025-12', sales: 2152, target: 4000, completion: '54%', lastMonth: 1987, monthGrowth: '+8.3%', yearGrowth: '+31.2%', yearTotal: 19876, marketShare: '36.5%', customers: 89 },
                        { channel: '友日达', tagType: 'warning', month: '2025-12', sales: 12, target: 100, completion: '12%', lastMonth: 18, monthGrowth: '-33.3%', yearGrowth: '-40.0%', yearTotal: 867, marketShare: '1.6%', customers: 8 }
                    ],

                    // 各渠道月销量统计数据（15行）
                    allChannelTableData: [
                        { rank: 1, channel: '东莞云仓', tagType: 'success', sales: 426, target: 898, completion: '47%', lastMonth: 389, monthGrowth: '+9.5%', yearTotal: 4856, avgPrice: '¥265', revenue: '¥112,890', yearGrowth: '+18.3%', inventory: 1234, turnoverRate: '78%', customers: 89, orders: 156, avgOrder: '¥724', marketShare: '8.9%' },
                        { rank: 2, channel: '加西云仓', tagType: 'success', sales: 398, target: 983, completion: '40%', lastMonth: 412, monthGrowth: '-3.4%', yearTotal: 4523, avgPrice: '¥278', revenue: '¥110,644', yearGrowth: '+15.7%', inventory: 1156, turnoverRate: '82%', customers: 76, orders: 142, avgOrder: '¥779', marketShare: '8.3%' },
                        { rank: 3, channel: '东南云仓', tagType: 'success', sales: 372, target: 917, completion: '41%', lastMonth: 356, monthGrowth: '+4.5%', yearTotal: 4234, avgPrice: '¥252', revenue: '¥93,744', yearGrowth: '+12.4%', inventory: 987, turnoverRate: '85%', customers: 68, orders: 134, avgOrder: '¥699', marketShare: '7.8%' },
                        { rank: 4, channel: '珠海云仓', tagType: 'success', sales: 308, target: 321, completion: '96%', lastMonth: 298, monthGrowth: '+3.4%', yearTotal: 3567, avgPrice: '¥289', revenue: '¥89,012', yearGrowth: '+21.6%', inventory: 845, turnoverRate: '91%', customers: 62, orders: 118, avgOrder: '¥754', marketShare: '6.4%' },
                        { rank: 5, channel: '佛山云仓', tagType: 'success', sales: 282, target: 832, completion: '34%', lastMonth: 267, monthGrowth: '+5.6%', yearTotal: 3198, avgPrice: '¥245', revenue: '¥69,090', yearGrowth: '+9.8%', inventory: 756, turnoverRate: '73%', customers: 54, orders: 98, avgOrder: '¥705', marketShare: '5.9%' },
                        { rank: 6, channel: '友日云仓', tagType: 'success', sales: 229, target: 669, completion: '34%', lastMonth: 245, monthGrowth: '-6.5%', yearTotal: 2789, avgPrice: '¥258', revenue: '¥59,082', yearGrowth: '+6.2%', inventory: 623, turnoverRate: '69%', customers: 47, orders: 87, avgOrder: '¥679', marketShare: '4.8%' },
                        { rank: 7, channel: '南宁云仓', tagType: 'success', sales: 174, target: 549, completion: '32%', lastMonth: 189, monthGrowth: '-7.9%', yearTotal: 2156, avgPrice: '¥234', revenue: '¥40,716', yearGrowth: '+3.5%', inventory: 512, turnoverRate: '65%', customers: 38, orders: 72, avgOrder: '¥565', marketShare: '3.6%' },
                        { rank: 8, channel: '从化云仓', tagType: 'success', sales: 135, target: 618, completion: '22%', lastMonth: 142, monthGrowth: '-4.9%', yearTotal: 1678, avgPrice: '¥267', revenue: '¥36,045', yearGrowth: '+1.8%', inventory: 445, turnoverRate: '58%', customers: 32, orders: 58, avgOrder: '¥621', marketShare: '2.8%' },
                        { rank: 9, channel: '北京云仓', tagType: 'success', sales: 128, target: 1364, completion: '9%', lastMonth: 156, monthGrowth: '-17.9%', yearTotal: 1534, avgPrice: '¥298', revenue: '¥38,144', yearGrowth: '-5.2%', inventory: 389, turnoverRate: '52%', customers: 28, orders: 51, avgOrder: '¥748', marketShare: '2.7%' },
                        { rank: 10, channel: '北海云仓', tagType: 'success', sales: 107, target: 1912, completion: '6%', lastMonth: 98, monthGrowth: '+9.2%', yearTotal: 1289, avgPrice: '¥223', revenue: '¥23,861', yearGrowth: '-2.1%', inventory: 334, turnoverRate: '48%', customers: 24, orders: 45, avgOrder: '¥530', marketShare: '2.2%' },
                        { rank: 11, channel: '柳州云仓', tagType: 'success', sales: 81, target: 1412, completion: '6%', lastMonth: 76, monthGrowth: '+6.6%', yearTotal: 967, avgPrice: '¥241', revenue: '¥19,521', yearGrowth: '-8.3%', inventory: 278, turnoverRate: '44%', customers: 19, orders: 38, avgOrder: '¥514', marketShare: '1.7%' },
                        { rank: 12, channel: '汉云仓', tagType: 'success', sales: 64, target: 1593, completion: '4%', lastMonth: 71, monthGrowth: '-9.9%', yearTotal: 789, avgPrice: '¥256', revenue: '¥16,384', yearGrowth: '-11.5%', inventory: 223, turnoverRate: '41%', customers: 16, orders: 32, avgOrder: '¥512', marketShare: '1.3%' },
                        { rank: 13, channel: '杭州云仓', tagType: 'success', sales: 57, target: 222, completion: '26%', lastMonth: 52, monthGrowth: '+9.6%', yearTotal: 645, avgPrice: '¥273', revenue: '¥15,561', yearGrowth: '+4.7%', inventory: 198, turnoverRate: '55%', customers: 14, orders: 28, avgOrder: '¥556', marketShare: '1.2%' },
                        { rank: 14, channel: '南京云仓', tagType: 'success', sales: 47, target: 1368, completion: '3%', lastMonth: 54, monthGrowth: '-13.0%', yearTotal: 567, avgPrice: '¥289', revenue: '¥13,583', yearGrowth: '-14.2%', inventory: 167, turnoverRate: '38%', customers: 12, orders: 24, avgOrder: '¥566', marketShare: '1.0%' },
                        { rank: 15, channel: '合肥云仓', tagType: 'success', sales: 46, target: 1606, completion: '3%', lastMonth: 49, monthGrowth: '-6.1%', yearTotal: 523, avgPrice: '¥267', revenue: '¥12,282', yearGrowth: '-16.8%', inventory: 145, turnoverRate: '35%', customers: 11, orders: 22, avgOrder: '¥558', marketShare: '0.9%' }
                    ],

                    // 未结算订单明细数据（18行）
                    orderTableData: [
                        { rank: 1, warehouse: '东莞云仓', orderType: 'OES客户端', orderNo: 'DP251220087', amount: '¥15708', customer: '广州半岛汽车供应链有限公司' },
                        { rank: 2, warehouse: '东莞云仓', orderType: 'OES客户端', orderNo: 'TX251220213', amount: '¥7652', customer: '广州半岛汽车供应链有限公司' },
                        { rank: 3, warehouse: '东莞云仓', orderType: 'OES客户端', orderNo: 'DP251220091', amount: '¥13240', customer: '广州半岛汽车供应链有限公司' },
                        { rank: 4, warehouse: '东莞云仓', orderType: 'OES客户端', orderNo: 'DP251220092', amount: '¥11850', customer: '广州半岛汽车供应链有限公司' },
                        { rank: 5, warehouse: '东莞云仓', orderType: 'OES客户端', orderNo: 'DP251220093', amount: '¥10320', customer: '广州半岛汽车供应链有限公司' },
                        { rank: 6, warehouse: '东莞云仓', orderType: 'OES客户端', orderNo: 'DP251220094', amount: '¥9800', customer: '广州半岛汽车供应链有限公司' },
                        { rank: 7, warehouse: '东莞云仓', orderType: 'OES客户端', orderNo: 'DP251220095', amount: '¥8750', customer: '广州半岛汽车供应链有限公司' },
                        { rank: 8, warehouse: '东莞云仓', orderType: 'OES客户端', orderNo: 'DP251220096', amount: '¥7680', customer: '广州半岛汽车供应链有限公司' },
                        { rank: 9, warehouse: '东莞云仓', orderType: 'OES客户端', orderNo: 'DP251220097', amount: '¥6540', customer: '广州半岛汽车供应链有限公司' },
                        { rank: 10, warehouse: '东莞云仓', orderType: 'OES客户端', orderNo: 'DP251220098', amount: '¥5420', customer: '广州半岛汽车供应链有限公司' },
                        { rank: 11, warehouse: '东莞云仓', orderType: '极速达', orderNo: 'JS251220101', amount: '¥8960', customer: '深圳某某汽车销售有限公司' },
                        { rank: 12, warehouse: '东莞云仓', orderType: '极速达', orderNo: 'JS251220102', amount: '¥7340', customer: '深圳某某汽车销售有限公司' },
                        { rank: 13, warehouse: '东莞云仓', orderType: '极速达', orderNo: 'JS251220103', amount: '¥6280', customer: '深圳某某汽车销售有限公司' },
                        { rank: 14, warehouse: '东莞云仓', orderType: '客户自产', orderNo: 'KH251220201', amount: '¥4520', customer: '东莞某某制造企业' },
                        { rank: 15, warehouse: '东莞云仓', orderType: '客户自产', orderNo: 'KH251220202', amount: '¥3890', customer: '东莞某某制造企业' },
                        { rank: 16, warehouse: '东莞云仓', orderType: '友日达', orderNo: 'YR251220301', amount: '¥2150', customer: '佛山某某物流公司' },
                        { rank: 17, warehouse: '加西云仓', orderType: 'OES客户端', orderNo: 'DP251220201', amount: '¥12450', customer: '珠海某某科技股份公司' },
                        { rank: 18, warehouse: '加西云仓', orderType: 'OES客户端', orderNo: 'DP251220202', amount: '¥9870', customer: '珠海某某科技股份公司' }
                    ],

                    // 区域销量完成情况表格数据
                    regionTableData: [
                        { rank: 1, title: '狄安增达(续标)', region: '深圳', target: 500, actual: 405, completion: '81%', amount: '¥107,325', status: '进行中', statusType: 'warning' },
                        { rank: 2, title: '狄安增达(续标)', region: '汕尾', target: 400, actual: 100, completion: '25%', amount: '¥26,500', status: '未完成', statusType: 'danger' },
                        { rank: 3, title: '狄安增达(续标)', region: '揭阳', target: 300, actual: 579, completion: '193%', amount: '¥153,435', status: '已完成', statusType: 'success' },
                        { rank: 4, title: '狄安增达(续标)', region: '潮州', target: 200, actual: 25, completion: '12.5%', amount: '¥6,625', status: '未完成', statusType: 'danger' },
                        { rank: 5, title: '狄安增达(续标)', region: '汕头', target: 100, actual: 661, completion: '661%', amount: '¥175,165', status: '已完成', statusType: 'success' }
                    ],

                    // 渠道毛利率列表数据
                    profitTableData: [
                        { rank: 1, channel: 'OES客户端', tagType: 'danger', sales: 2869, revenue: '¥916117', cost: '¥537170' },
                        { rank: 2, channel: '客户单', tagType: '', sales: 434, revenue: '¥114976', cost: '¥70739' },
                        { rank: 3, channel: '极速达', tagType: 'warning', sales: 2152, revenue: '¥477009', cost: '¥463790' },
                        { rank: 4, channel: '次日达', tagType: 'warning', sales: 12, revenue: '¥2232', cost: '¥2232' }
                    ],

                    // ECharts实例
                    charts: {}
                };
            },

            created() {
                // 检测页面是否在iframe中
                this.isInIFrame = window.self !== window.top;
                // 动态获取品牌数据
                this.fetchBrandOptions();
            },

            mounted() {
                // 初始化所有图表
                this.$nextTick(() => {
                    this.fetchMetricData();
                    this.initChannelChart();
                    this.initWarehouseChart();
                    this.initDailySalesChart();
                    this.initRegionChart();
                    this.initUnsettledChart();
                    this.initProfitChart();
                    this.initOverdueInventoryChart();
                    this.initOverdueRatioChart();
                });
            },

            beforeDestroy() {
                // 销毁所有图表实例
                Object.values(this.charts).forEach(chart => {
                    if (chart) {
                        chart.dispose();
                    }
                });
            },

            computed: {
                // 年份开始选择器选项
                yearStartPickerOptions() {
                    const self = this;
                    return {
                        disabledDate(time) {
                            if (self.filterParams.yearEnd) {
                                return time.getFullYear() > parseInt(self.filterParams.yearEnd);
                            }
                            return false;
                        }
                    };
                },
                // 年份结束选择器选项
                yearEndPickerOptions() {
                    const self = this;
                    return {
                        disabledDate(time) {
                            if (self.filterParams.yearStart) {
                                return time.getFullYear() < parseInt(self.filterParams.yearStart);
                            }
                            return false;
                        }
                    };
                }
            },

            methods: {
                // 动态获取品牌数据
                fetchBrandOptions() {
                    const apiUrl = '../Product/productApi.aspx?method=QueryALLOneProductType&PID=1';
                    axios.get(apiUrl)
                        .then(response => {
                            // 检查返回数据结构，如果是数组直接使用，否则尝试转换
                            let dataList = [];
                            if (Array.isArray(response.data)) {
                                dataList = response.data;
                            } else if (response.data && Array.isArray(response.data.data)) {
                                dataList = response.data.data;
                            } else if (response.data && response.data.list) {
                                dataList = response.data.list;
                            }
                            
                            // 转换数据格式，并在最前面添加"全部"选项
                            const options = dataList.map(item => ({
                                value: item.TypeID,
                                label: item.TypeName,
                                code: String(item.TypeID)
                            }));
                            
                            // 添加"全部"选项在最前面
                            this.brandOptions = [{ value: '-1', label: '全部', code: 'ID' }, ...options];
                            // console.log('品牌数据加载完成:', this.brandOptions);
                        })
                        .catch(error => {
                            console.error('获取品牌数据失败:', error);
                            this.$message.error('获取品牌数据失败');
                        });
                },

                // 处理时间粒度变化
                handleGranularityChange(value) {
                    // 切换粒度时重置对应的时间范围
                    if (value === 'day') {
                        this.filterParams.dateRange = [];
                    } else if (value === 'month') {
                        this.filterParams.monthRange = [];
                    } else if (value === 'year') {
                        this.filterParams.yearStart = '';
                        this.filterParams.yearEnd = '';
                    }
                },

                // 处理查询按钮点击
                handleQuery() {
                    // 获取筛选条件
                    const params = {
                        brand: this.filterParams.brand,
                        timeGranularity: this.filterParams.timeGranularity
                    };

                    // 根据时间粒度获取对应的时间范围
                    if (this.filterParams.timeGranularity === 'day') {
                        params.dateRange = this.filterParams.dateRange;
                    } else if (this.filterParams.timeGranularity === 'month') {
                        params.monthRange = this.filterParams.monthRange;
                    } else if (this.filterParams.timeGranularity === 'year') {
                        params.yearStart = this.filterParams.yearStart;
                        params.yearEnd = this.filterParams.yearEnd;
                    }

                    console.log('查询参数:', params);
                    this.$message.success('查询成功！');

                    // TODO: 在此处添加实际的数据查询逻辑
                    // 例如：调用API获取数据，更新图表等
                    // 模拟后端返回数据并格式化
                    this.fetchMetricData();
                },

                // 模拟从后端获取指标数据
                fetchMetricData() {
                    // 模拟后端返回的纯数值数据
                    const mockData = {
                        totalSalesVolume: Math.floor(Math.random() * 10000) + 5000,
                        inStockInventory: Math.floor(Math.random() * 5000) + 8000,
                        salesRate: Math.floor(Math.random() * 30) + 80,
                        totalSalesAmount: Math.floor(Math.random() * 3000000) + 1000000,
                        unsettledAmount: Math.floor(Math.random() * 1500000) + 500000,
                        grossProfitRate: Math.floor(Math.random() * 20) + 25
                    };

                    // 批量更新 metricData 对象
                    this.metricData = { ...this.metricData, ...mockData };

                    // console.log('更新后的指标数据:', this.metricData);
                },

                // 格式化金额（添加千分位分隔符和人民币符号）
                formatCurrency(value) {
                    if (value === null || value === undefined) {
                        return '¥0';
                    }
                    // 将数值转为字符串并添加千分位分隔符
                    const formatted = value.toLocaleString('zh-CN', {
                        minimumFractionDigits: 0,
                        maximumFractionDigits: 0
                    });
                    return '¥' + formatted;
                },

                // 在新标签页打开当前页面
                openInNewTab() {
                    window.open(window.location.href, '_blank');
                },

                // 渠道月销量统计组合图
                initChannelChart() {
                    const chart = echarts.init(this.$refs.channelChart);
                    this.charts.channelChart = chart;

                    const option = {
                        tooltip: {
                            trigger: 'axis',
                            axisPointer: { type: 'cross' }
                        },
                        legend: {
                            data: ['销量', '完成率'],
                            top: 0
                        },
                        grid: {
                            left: '3%',
                            right: '4%',
                            bottom: '3%',
                            containLabel: true
                        },
                        xAxis: [
                            {
                                type: 'category',
                                data: ['OES客户端', '客户自产', '极速达', '友日达'],
                                axisLabel: { color: '#606266' },
                                axisLine: { lineStyle: { color: '#ddd' } }
                            }
                        ],
                        yAxis: [
                            {
                                type: 'value',
                                name: '销量',
                                axisLabel: { color: '#606266' },
                                axisLine: { lineStyle: { color: '#ddd' } },
                                splitLine: { lineStyle: { color: '#eee' } }
                            },
                            {
                                type: 'value',
                                name: '完成率',
                                min: 0,
                                max: 200,
                                axisLabel: {
                                    color: '#606266',
                                    formatter: '{value}%'
                                },
                                axisLine: { lineStyle: { color: '#ddd' } },
                                splitLine: { show: false }
                            }
                        ],
                        series: [
                            {
                                name: '销量',
                                type: 'bar',
                                barWidth: '60%',
                                data: [2869, 434, 2152, 12],
                                itemStyle: {
                                    color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                                        { offset: 0, color: '#67C23A' },
                                        { offset: 1, color: '#85ce61' }
                                    ]),
                                    borderRadius: [5, 5, 0, 0]
                                },
                                label: {
                                    show: true,
                                    position: 'top',
                                    color: '#606266',
                                    formatter: '{c}'
                                }
                            },
                            {
                                name: '完成率',
                                type: 'line',
                                yAxisIndex: 1,
                                smooth: true,
                                symbol: 'circle',
                                symbolSize: 6,
                                data: [143, 54, 54, 12],
                                itemStyle: { color: '#E6A23C' },
                                label: {
                                    show: true,
                                    position: 'top',
                                    color: '#E6A23C',
                                    formatter: '{c}%'
                                }
                            }
                        ]
                    };

                    chart.setOption(option);
                },

                // 仓库月出库量柱状图
                initWarehouseChart() {
                    const chart = echarts.init(this.$refs.warehouseChart);
                    this.charts.warehouseChart = chart;

                    const option = {
                        tooltip: {
                            trigger: 'axis',
                            axisPointer: { type: 'shadow' }
                        },
                        grid: {
                            left: '3%',
                            right: '4%',
                            bottom: '3%',
                            containLabel: true
                        },
                        xAxis: {
                            type: 'category',
                            data: [
                                '东莞云仓', '加西云仓', '东南云仓', '珠海云仓', '佛山云仓',
                                '友日云仓', '南宁云仓', '从化云仓', '北京云仓', '北海云仓',
                                '柳州云仓', '汉云仓', '杭州云仓', '南京云仓', '合肥云仓',
                                '重庆云仓', '成都云仓', '武汉云仓', '西安云仓', '郑州云仓',
                                '长沙云仓', '沈阳云仓', '大连云仓', '青岛云仓', '济南云仓',
                                '苏州云仓', '无锡云仓', '宁波云仓'
                            ],
                            axisLabel: {
                                color: '#606266',
                                interval: 0,
                                rotate: 30
                            },
                            axisLine: { lineStyle: { color: '#ddd' } }
                        },
                        yAxis: {
                            type: 'value',
                            axisLabel: { color: '#606266' },
                            axisLine: { lineStyle: { color: '#ddd' } },
                            splitLine: { lineStyle: { color: '#eee' } }
                        },
                        series: [
                            {
                                name: '出库量',
                                type: 'bar',
                                barWidth: '60%',
                                data: [65, 71, 69, 51, 82, 70, 129, 118, 47, 57, 77, 81, 72, 49, 56, 269, 109, 90, 108, 77, 71, 98, 76, 87, 72, 84, 83, 109],
                                itemStyle: {
                                    color: function (params) {
                                        // 为每个柱子生成不同的上下渐变颜色
                                        const colorGradients = [
                                            ['#409EFF', '#66b1ff'],
                                            // ['#67C23A', '#85ce61'],
                                            // ['#E6A23C', '#f0c78a'],
                                            // ['#F56C6C', '#f89898'],
                                            // ['#909399', '#b4b8c3'],
                                            // ['#00d4ff', '#33e0ff'],
                                            // ['#19d4a8', '#4de0c4'],
                                            // ['#ff9f43', '#ffbe73'],
                                            // ['#6c5ce7', '#8c7ae6'],
                                            // ['#fd79a8', '#feb1c3'],
                                            // ['#00b894', '#33d9b3'],
                                            // ['#e17055', '#e89373'],
                                            // ['#0984e3', '#33a1f7'],
                                            // ['#00cec9', '#33e0de'],
                                            // ['#d63031', '#e65353'],
                                            // ['#e84393', '#f06ba8'],
                                            // ['#fdcb6e', '#fed885'],
                                            // ['#55efc4', '#7befd6'],
                                            // ['#81ecec', '#a4f2f4'],
                                            // ['#74b9ff', '#99c9ff'],
                                            // ['#a29bfe', '#c4b5fe'],
                                            // ['#ffeaa7', '#fff2c1'],
                                            // ['#fab1a0', '#fccfc6'],
                                            // ['#81ecec', '#a4f2f4'],
                                            // ['#dfe6e9', '#f0f3f5'],
                                            // ['#b2bec3', '#c8d1d6'],
                                            // ['#636e72', '#858b8e']
                                        ];
                                        const colors = colorGradients[params.dataIndex % colorGradients.length];
                                        return new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                                            { offset: 0, color: colors[0] },
                                            { offset: 1, color: colors[1] }
                                        ]);
                                    },
                                    borderRadius: [5, 5, 0, 0]
                                },
                                label: {
                                    show: true,
                                    position: 'top',
                                    color: '#606266',
                                    formatter: '{c}'
                                }
                            }
                        ]
                    };

                    chart.setOption(option);
                },

                // 日总销量数据柱状图
                initDailySalesChart() {
                    const chart = echarts.init(this.$refs.dailySalesChart);
                    this.charts.dailySalesChart = chart;

                    // 生成12月1日到31日的日期数据
                    const days = [];
                    const salesData = [];
                    for (let i = 1; i <= 31; i++) {
                        days.push(`2025-1-${i}`);
                        // 生成随机销量数据，范围在40-270之间
                        const randomSales = Math.floor(Math.random() * (270 - 40 + 1)) + 40;
                        salesData.push(randomSales);
                    }

                    const option = {
                        tooltip: {
                            trigger: 'axis',
                            axisPointer: { type: 'shadow' }
                        },
                        grid: {
                            left: '3%',
                            right: '4%',
                            bottom: '3%',
                            containLabel: true
                        },
                        xAxis: {
                            type: 'category',
                            data: days,
                            axisLabel: {
                                color: '#606266',
                                interval: 0,
                                rotate: 45,
                                fontSize: 10
                            },
                            axisLine: { lineStyle: { color: '#ddd' } }
                        },
                        yAxis: {
                            type: 'value',
                            name: '数量',
                            axisLabel: { color: '#606266' },
                            axisLine: { lineStyle: { color: '#ddd' } },
                            splitLine: { lineStyle: { color: '#eee' } }
                        },
                        series: [
                            {
                                name: '数量',
                                type: 'bar',
                                barWidth: '60%',
                                data: salesData,
                                itemStyle: {
                                    color: function (params) {
                                       // 为每个柱子生成不同的上下渐变颜色
                                        const colorGradients = [
                                            ['#409EFF', '#66b1ff'],
                                            // ['#67C23A', '#85ce61'],
                                            // ['#E6A23C', '#f0c78a'],
                                            // ['#F56C6C', '#f89898'],
                                            // ['#909399', '#b4b8c3'],
                                            // ['#00d4ff', '#33e0ff'],
                                            // ['#19d4a8', '#4de0c4'],
                                            // ['#ff9f43', '#ffbe73'],
                                            // ['#6c5ce7', '#8c7ae6'],
                                            // ['#fd79a8', '#feb1c3'],
                                            // ['#00b894', '#33d9b3'],
                                            // ['#e17055', '#e89373'],
                                            // ['#0984e3', '#33a1f7'],
                                            // ['#00cec9', '#33e0de'],
                                            // ['#d63031', '#e65353'],
                                            // ['#e84393', '#f06ba8'],
                                            // ['#fdcb6e', '#fed885'],
                                            // ['#55efc4', '#7befd6'],
                                            // ['#81ecec', '#a4f2f4'],
                                            // ['#74b9ff', '#99c9ff'],
                                            // ['#a29bfe', '#c4b5fe'],
                                            // ['#ffeaa7', '#fff2c1'],
                                            // ['#fab1a0', '#fccfc6'],
                                            // ['#81ecec', '#a4f2f4'],
                                            // ['#dfe6e9', '#f0f3f5'],
                                            // ['#b2bec3', '#c8d1d6'],
                                            // ['#636e72', '#858b8e']
                                        ];
                                        const colors = colorGradients[params.dataIndex % colorGradients.length];
                                        return new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                                            { offset: 0, color: colors[0] },
                                            { offset: 1, color: colors[1] }
                                        ]);
                                    },
                                    borderRadius: [5, 5, 0, 0]
                                },
                                label: {
                                    show: true,
                                    position: 'top',
                                    color: '#606266',
                                    fontSize: 10,
                                    formatter: '{c}'
                                }
                            }
                        ]
                    };

                    chart.setOption(option);
                },

                // 区域内完成情况组合图
                initRegionChart() {
                    const chart = echarts.init(this.$refs.regionChart);
                    this.charts.regionChart = chart;

                    const regions = ['深圳', '汕尾', '汕头', '梅州', '揭阳', '潮州'];
                    const actualSales = [405, 100, 661, 400, 579, 25];
                    const targetSales = [500, 100, 400, 300, 300, 200];
                    const completionRates = [81, 100, 165.25, 133, 193, 12.5];

                    const option = {
                        tooltip: {
                            trigger: 'axis',
                            axisPointer: { type: 'cross' }
                        },
                        legend: {
                            data: ['完成销量', '完成率', '目标销量'],
                            top: 0
                        },
                        grid: {
                            left: '3%',
                            right: '8%',
                            bottom: '3%',
                            containLabel: true
                        },
                        xAxis: {
                            type: 'category',
                            data: regions,
                            axisLabel: {
                                color: '#606266',
                                fontSize: 12
                            },
                            axisLine: { lineStyle: { color: '#ddd' } }
                        },
                        yAxis: [
                            {
                                type: 'value',
                                name: '销量',
                                position: 'left',
                                axisLabel: {
                                    color: '#606266',
                                    formatter: '{value}'
                                },
                                axisLine: { lineStyle: { color: '#ddd' } },
                                splitLine: { lineStyle: { color: '#eee' } }
                            },
                            {
                                type: 'value',
                                name: '完成率',
                                position: 'right',
                                min: 0,
                                max: 210,
                                axisLabel: {
                                    color: '#606266',
                                    formatter: '{value}%'
                                },
                                axisLine: { lineStyle: { color: '#ddd' } },
                                splitLine: { show: false }
                            }
                        ],
                        series: [
                            {
                                name: '完成销量',
                                type: 'bar',
                                barWidth: '25%',
                                data: actualSales,
                                itemStyle: {
                                    color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                                        { offset: 0, color: '#409EFF' },
                                        { offset: 1, color: '#66b1ff' }
                                    ]),
                                    borderRadius: [5, 5, 0, 0]
                                },
                                label: {
                                    show: true,
                                    position: 'inside',
                                    color: '#fff',
                                    fontSize: 11,
                                    formatter: '{c}'
                                }
                            },
                            {
                                name: '目标销量',
                                type: 'bar',
                                barWidth: '25%',
                                data: targetSales,
                                itemStyle: {
                                    color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                                        { offset: 0, color: '#E6A23C' },
                                        { offset: 1, color: '#f0c78a' }
                                    ]),
                                    borderRadius: [5, 5, 0, 0]
                                },
                                label: {
                                    show: true,
                                    position: 'inside',
                                    color: '#fff',
                                    fontSize: 11,
                                    formatter: '{c}'
                                }
                            },
                            {
                                name: '完成率',
                                type: 'line',
                                yAxisIndex: 1,
                                smooth: true,
                                symbol: 'circle',
                                symbolSize: 8,
                                data: completionRates,
                                itemStyle: {
                                    color: '#67C23A',
                                    borderWidth: 2
                                },
                                lineStyle: {
                                    width: 3,
                                    color: '#67C23A'
                                },
                                label: {
                                    show: true,
                                    position: 'top',
                                    color: '#67C23A',
                                    fontSize: 11,
                                    fontWeight: 'bold',
                                    formatter: '{c}%'
                                }
                            }
                        ]
                    };

                    chart.setOption(option);
                },

                // 各类通未结算金额柱状图
                initUnsettledChart() {
                    const chart = echarts.init(this.$refs.unsettledChart);
                    this.charts.unsettledChart = chart;

                    const option = {
                        tooltip: {
                            trigger: 'axis',
                            axisPointer: { type: 'shadow' },
                            formatter: function (params) {
                                return params[0].name + '<br/>' + params[0].marker + '未结算金额: ¥' + params[0].value.toLocaleString();
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
                            data: ['OES客户端', '极速达', '客户自产', '友日达'],
                            axisLabel: { color: '#606266' },
                            axisLine: { lineStyle: { color: '#ddd' } }
                        },
                        yAxis: {
                            type: 'value',
                            axisLabel: {
                                color: '#606266',
                                formatter: function (value) {
                                    return '¥' + value.toLocaleString();
                                }
                            },
                            axisLine: { lineStyle: { color: '#ddd' } },
                            splitLine: { lineStyle: { color: '#eee' } }
                        },
                        series: [
                            {
                                name: '未结算金额',
                                type: 'bar',
                                barWidth: '60%',
                                data: [465721, 220205, 65196, 2232],
                                itemStyle: {
                                    color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                                        { offset: 0, color: '#F56C6C' },
                                        { offset: 1, color: '#f89898' }
                                    ]),
                                    borderRadius: [5, 5, 0, 0]
                                },
                                label: {
                                    show: true,
                                    position: 'top',
                                    color: '#F56C6C',
                                    formatter: function (params) {
                                        return '¥' + params.value.toLocaleString();
                                    }
                                }
                            }
                        ]
                    };

                    chart.setOption(option);
                },

                // 渠道毛利分析组合图
                initProfitChart() {
                    const chart = echarts.init(this.$refs.profitChart);
                    this.charts.profitChart = chart;

                    const channels = ['OES客户端', '客户单', '极速达', '次日达'];
                    const profits = [378947, 44237, 3219, 0];
                    const profitRates = [71, 63, 0, 0];

                    const option = {
                        tooltip: {
                            trigger: 'axis',
                            axisPointer: { type: 'cross' }
                        },
                        legend: {
                            data: ['毛利额', '毛利率'],
                            top: 0
                        },
                        grid: {
                            left: '3%',
                            right: '8%',
                            bottom: '3%',
                            containLabel: true
                        },
                        xAxis: {
                            type: 'category',
                            data: channels,
                            axisLabel: {
                                color: '#606266',
                                fontSize: 12
                            },
                            axisLine: { lineStyle: { color: '#ddd' } }
                        },
                        yAxis: [
                            {
                                type: 'value',
                                name: '毛利额',
                                position: 'left',
                                axisLabel: {
                                    color: '#606266',
                                    formatter: function (value) {
                                        return '¥' + (value / 1000).toFixed(0) + 'k';
                                    }
                                },
                                axisLine: { lineStyle: { color: '#ddd' } },
                                splitLine: { lineStyle: { color: '#eee' } }
                            },
                            {
                                type: 'value',
                                name: '毛利率',
                                position: 'right',
                                min: 0,
                                max: 80,
                                axisLabel: {
                                    color: '#606266',
                                    formatter: '{value}%'
                                },
                                axisLine: { lineStyle: { color: '#ddd' } },
                                splitLine: { show: false }
                            }
                        ],
                        series: [
                            {
                                name: '毛利额',
                                type: 'bar',
                                barWidth: '50%',
                                data: profits,
                                itemStyle: {
                                    color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                                        { offset: 0, color: '#67C23A' },
                                        { offset: 1, color: '#85ce61' }
                                    ]),
                                    borderRadius: [5, 5, 0, 0]
                                },
                                label: {
                                    show: true,
                                    position: 'top',
                                    color: '#606266',
                                    fontSize: 11,
                                    formatter: function (params) {
                                        return '¥' + params.value.toLocaleString();
                                    }
                                }
                            },
                            {
                                name: '毛利率',
                                type: 'line',
                                yAxisIndex: 1,
                                smooth: true,
                                symbol: 'circle',
                                symbolSize: 8,
                                data: profitRates,
                                itemStyle: {
                                    color: '#67C23A',
                                    borderWidth: 2
                                },
                                lineStyle: {
                                    width: 3,
                                    color: '#67C23A'
                                },
                                label: {
                                    show: true,
                                    position: 'top',
                                    color: '#67C23A',
                                    fontSize: 11,
                                    fontWeight: 'bold',
                                    formatter: '{c}%'
                                }
                            }
                        ]
                    };

                    chart.setOption(option);
                },

                // 仓库超期库存数柱状图
                initOverdueInventoryChart() {
                    const chart = echarts.init(this.$refs.overdueInventoryChart);
                    this.charts.overdueInventoryChart = chart;

                    const warehouses = [
                        '微社云仓', '微群云仓', '汕尾云仓', '从化云仓', '杭州云仓',
                        '南宁云仓', '包头云仓', '汕头云仓', '南沙云仓', '揭阳云仓',
                        '东莞云仓', '湛江云仓', '梅州云仓', '韶关云仓', '清远云仓',
                        '河源云仓'
                    ];
                    const inventoryData = [287, 155, 141, 115, 114, 94, 91, 82, 78, 73, 72, 61, 59, 38, 37, 34];

                    const option = {
                        tooltip: {
                            trigger: 'axis',
                            axisPointer: { type: 'shadow' },
                            formatter: function (params) {
                                return params[0].name + '<br/>' + params[0].marker + '库存数量: ' + params[0].value;
                            }
                        },
                        legend: {
                            data: ['库存数量'],
                            top: 0
                        },
                        grid: {
                            left: '3%',
                            right: '4%',
                            bottom: '3%',
                            containLabel: true
                        },
                        xAxis: {
                            type: 'category',
                            data: warehouses,
                            axisLabel: {
                                color: '#606266',
                                interval: 0,
                                rotate: 45,
                                fontSize: 11
                            },
                            axisLine: { lineStyle: { color: '#ddd' } }
                        },
                        yAxis: {
                            type: 'value',
                            name: '库存数量',
                            axisLabel: { color: '#606266' },
                            axisLine: { lineStyle: { color: '#ddd' } },
                            splitLine: { lineStyle: { color: '#eee' } }
                        },
                        series: [
                            {
                                name: '库存数量',
                                type: 'bar',
                                barWidth: '60%',
                                data: inventoryData,
                                itemStyle: {
                                    color: function (params) {
                                      // 为每个柱子生成不同的上下渐变颜色
                                        const colorGradients = [
                                            ['#409EFF', '#66b1ff'],
                                            // ['#67C23A', '#85ce61'],
                                            // ['#E6A23C', '#f0c78a'],
                                            // ['#F56C6C', '#f89898'],
                                            // ['#909399', '#b4b8c3'],
                                            // ['#00d4ff', '#33e0ff'],
                                            // ['#19d4a8', '#4de0c4'],
                                            // ['#ff9f43', '#ffbe73'],
                                            // ['#6c5ce7', '#8c7ae6'],
                                            // ['#fd79a8', '#feb1c3'],
                                            // ['#00b894', '#33d9b3'],
                                            // ['#e17055', '#e89373'],
                                            // ['#0984e3', '#33a1f7'],
                                            // ['#00cec9', '#33e0de'],
                                            // ['#d63031', '#e65353'],
                                            // ['#e84393', '#f06ba8'],
                                            // ['#fdcb6e', '#fed885'],
                                            // ['#55efc4', '#7befd6'],
                                            // ['#81ecec', '#a4f2f4'],
                                            // ['#74b9ff', '#99c9ff'],
                                            // ['#a29bfe', '#c4b5fe'],
                                            // ['#ffeaa7', '#fff2c1'],
                                            // ['#fab1a0', '#fccfc6'],
                                            // ['#81ecec', '#a4f2f4'],
                                            // ['#dfe6e9', '#f0f3f5'],
                                            // ['#b2bec3', '#c8d1d6'],
                                            // ['#636e72', '#858b8e']
                                        ];
                                        const colors = colorGradients[params.dataIndex % colorGradients.length];
                                        return new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                                            { offset: 0, color: colors[0] },
                                            { offset: 1, color: colors[1] }
                                        ]);
                                    },
                                    borderRadius: [5, 5, 0, 0]
                                },
                                label: {
                                    show: true,
                                    position: 'top',
                                    color: '#606266',
                                    fontSize: 10,
                                    formatter: '{c}'
                                }
                            }
                        ]
                    };

                    chart.setOption(option);
                },

                // 超期库存占比饼图
                initOverdueRatioChart() {
                    const chart = echarts.init(this.$refs.overdueRatioChart);
                    this.charts.overdueRatioChart = chart;

                    const overdueValue = 1556;
                    const normalValue = 8420;
                    const total = overdueValue + normalValue;
                    const overduePercent = ((overdueValue / total) * 100).toFixed(1);
                    const normalPercent = ((normalValue / total) * 100).toFixed(2);

                    const option = {
                        tooltip: {
                            trigger: 'item',
                            backgroundColor: '#fff',
                            borderColor: '#ebeef5',
                            borderWidth: 1,
                            padding: [10, 15],
                            textStyle: {
                                color: '#303133',
                                fontSize: 13
                            },
                            formatter: function (params) {
                                const percent = ((params.value / total) * 100).toFixed(2);
                                const colorBar = '<span style="display:inline-block;width:10px;height:10px;border-radius:2px;background:' + params.color + ';margin-right:8px;"></span>';
                                return '<div style="font-weight:600;color:' + params.color + ';margin-bottom:8px;">' + colorBar + params.name + '</div>' +
                                    '<div style="padding-left:18px;line-height:1.8;">' +
                                    '库存数量: <span style="font-weight:600;">' + params.value + '</span><br/>' +
                                    '占比: <span style="font-weight:600;">' + percent + '%</span>' +
                                    '</div>';
                            }
                        },
                        legend: {
                            orient: 'horizontal',
                            bottom: '5%',
                            left: 'center',
                            itemGap: 30,
                            itemWidth: 12,
                            itemHeight: 12,
                            icon: 'circle',
                            textStyle: {
                                color: '#606266',
                                fontSize: 13
                            },
                            data: ['超期库存', '未超期库存']
                        },
                        graphic: [
                            {
                                type: 'text',
                                left: 'center',
                                top: '38%',
                                style: {
                                    text: overduePercent + '%',
                                    textAlign: 'center',
                                    fill: '#303133',
                                    fontSize: 36,
                                    fontWeight: 'bold'
                                }
                            },
                            {
                                type: 'text',
                                left: 'center',
                                top: '52%',
                                style: {
                                    text: '超期占比',
                                    textAlign: 'center',
                                    fill: '#909399',
                                    fontSize: 14
                                }
                            }
                        ],
                        series: [
                            {
                                name: '超期库存占比',
                                type: 'pie',
                                radius: ['55%', '75%'],
                                center: ['50%', '45%'],
                                avoidLabelOverlap: false,
                                startAngle: 90,
                                itemStyle: {
                                    borderRadius: 0,
                                    borderColor: '#fff',
                                    borderWidth: 3
                                },
                                label: {
                                    show: false
                                },
                                labelLine: {
                                    show: false
                                },
                                emphasis: {
                                    scale: true,
                                    scaleSize: 5
                                },
                                data: [
                                    {
                                        value: overdueValue,
                                        name: '超期库存',
                                        itemStyle: { color: '#909399' }
                                    },
                                    {
                                        value: normalValue,
                                        name: '未超期库存',
                                        itemStyle: { color: '#E6A23C' }
                                    }
                                ]
                            }
                        ]
                    };

                    chart.setOption(option);
                },

                // 窗口大小改变时重绘图表
                resizeCharts() {
                    Object.values(this.charts).forEach(chart => {
                        if (chart) {
                            chart.resize();
                        }
                    });
                }
            }
        });

        // 监听窗口大小变化
        window.addEventListener('resize', function () {
            const app = document.getElementById('app').__vue__;
            if (app && typeof app.resizeCharts === 'function') {
                app.resizeCharts();
            }
        });
    </script>
</asp:Content>