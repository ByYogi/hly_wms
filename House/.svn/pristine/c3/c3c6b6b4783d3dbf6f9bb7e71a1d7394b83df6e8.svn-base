// KPI指标卡片组件
Vue.component('kpi-card', {
    props: {
        title: {
            type: String,
            required: true
        },
        value: {
            type: [String, Number],
            required: true
        },
        subtitle: {
            type: String,
            default: ''
        },
        trend: {
            type: String,
            default: 'neutral',
            validator: value => ['up', 'down', 'neutral'].includes(value)
        },
        trendValue: {
            type: String,
            default: ''
        },
        icon: {
            type: String,
            default: 'el-icon-data-line'
        },
        color: {
            type: String,
            default: 'primary',
            validator: value => ['primary', 'success', 'warning', 'info', 'danger'].includes(value)
        },
        loading: {
            type: Boolean,
            default: false
        }
    },
    template: `
    <div class="kpi-card-wrapper">
      <!-- 骨架屏加载状态 -->
      <transition name="skeleton-fade" mode="out-in">
        <div v-if="loading" class="kpi-skeleton" :class="'kpi-skeleton--' + color" key="skeleton">
          <div class="kpi-skeleton__header">
            <div class="kpi-skeleton__title"></div>
            <div class="kpi-skeleton__icon"></div>
          </div>
          <div class="kpi-skeleton__content">
            <div class="kpi-skeleton__value"></div>
            <div class="kpi-skeleton__subtitle"></div>
            <div class="kpi-skeleton__trend"></div>
          </div>
        </div>
        
        <!-- 正常内容显示 -->
        <div v-else
             class="kpi-card kpi-card--loaded"
             :class="'kpi-card--' + color"
             key="content">
          <div class="kpi-card__header">
            <span class="kpi-card__title">{{ title }}</span>
            <i :class="icon" class="kpi-card__icon"></i>
          </div>
          <div class="kpi-card__content">
            <div class="kpi-card__value">{{ formatValue(value) }}</div>
            <div class="kpi-card__subtitle" v-if="subtitle">{{ subtitle }}</div>
            <div class="kpi-card__trend" :class="'trend--' + trend" v-if="trendValue">
              <i :class="trendIcon"></i>
              <span>{{ trendValue }}</span>
            </div>
          </div>
        </div>
      </transition>
    </div>
  `,
    computed: {
        trendIcon() {
            const icons = {
                up: 'el-icon-top',
                down: 'el-icon-bottom',
                neutral: 'el-icon-minus'
            };
            return icons[this.trend] || icons.neutral;
        }
    },
    methods: {
        formatValue(value) {
            if (typeof value === 'number') {
                // 如果是数字，添加千分位分隔符
                return value.toLocaleString();
            }
            return value;
        }
    }
});