// 图表面板组件
Vue.component('chart-panel', {
  props: {
    title: {
      type: String,
      required: true
    },
    chartType: {
      type: String,
      default: 'bar',
      validator: value => ['bar', 'line', 'pie', 'area', 'mixed'].includes(value)
    },
    data: {
      type: [Array, Object],
      default: () => []
    },
    options: {
      type: Object,
      default: () => ({})
    }
  },
  template: `
    <div class="chart-panel">
      <div class="chart-panel__header">
        <h3 class="chart-panel__title">{{ title }}</h3>
        <div class="chart-panel__actions">
          <el-button size="mini" icon="el-icon-refresh" @click="refreshChart">刷新</el-button>
          <el-button size="mini" icon="el-icon-download" @click="downloadChart">导出</el-button>
        </div>
      </div>
      <div class="chart-panel__content">
        <div ref="chart" class="chart-container" v-loading="loading"></div>
      </div>
    </div>
  `,
  data() {
    return {
      chart: null,
      loading: false,
      selectedPeriod: 'month'
    };
  },
  mounted() {
    this.$nextTick(() => {
      this.initChart();
    });
    
    // 监听窗口大小变化
    window.addEventListener('resize', this.handleResize);
  },
  beforeDestroy() {
    if (this.chart) {
      this.chart.dispose();
    }
    window.removeEventListener('resize', this.handleResize);
  },
  watch: {
    data: {
      handler() {
        this.updateChart();
      },
      deep: true
    },
    chartType() {
      this.updateChart();
    }
  },
  methods: {
    initChart() {
      if (!this.$refs.chart) return;
      
      this.chart = echarts.init(this.$refs.chart);
      this.updateChart();
    },
    
    updateChart() {
      if (!this.chart || !this.data) return;
      
      this.loading = true;
      
      setTimeout(() => {
        const option = this.generateChartOption();
        this.chart.setOption(option, true);
        this.loading = false;
      }, 300);
    },
    
    generateChartOption() {
      const baseOption = {
        tooltip: {
          trigger: 'axis',
          backgroundColor: 'rgba(0, 0, 0, 0.8)',
          borderColor: 'transparent',
          textStyle: {
            color: '#fff'
          }
        },
        legend: {
          top: 10,
          textStyle: {
            color: '#666'
          }
        },
        grid: {
          left: '3%',
          right: '4%',
          bottom: '3%',
          containLabel: true
        },
        ...this.options
      };

      switch (this.chartType) {
        case 'bar':
          return this.generateBarChart(baseOption);
        case 'line':
          return this.generateLineChart(baseOption);
        case 'pie':
          return this.generatePieChart(baseOption);
        case 'area':
          return this.generateAreaChart(baseOption);
        case 'mixed':
          return this.generateMixedChart(baseOption);
        default:
          return this.generateBarChart(baseOption);
      }
    },
    
    generateBarChart(baseOption) {
      const categories = this.data.map(item => item.name || item.label);
      const values = this.data.map(item => item.value);
      
      return {
        ...baseOption,
        xAxis: {
          type: 'category',
          data: categories,
          axisLine: {
            lineStyle: {
              color: '#e9ecef'
            }
          },
          axisLabel: {
            color: '#666'
          }
        },
        yAxis: {
          type: 'value',
          axisLine: {
            lineStyle: {
              color: '#e9ecef'
            }
          },
          axisLabel: {
            color: '#666'
          },
          splitLine: {
            lineStyle: {
              color: '#f3f4f6'
            }
          }
        },
        series: [{
          name: this.title,
          type: 'bar',
          data: values,
          itemStyle: {
            color: '#6366F1',
            borderRadius: [4, 4, 0, 0]
          },
          emphasis: {
            itemStyle: {
              color: '#4F46E5'
            }
          }
        }]
      };
    },
    
    generateLineChart(baseOption) {
      const categories = this.data.map(item => item.name || item.label);
      const values = this.data.map(item => item.value);
      
      return {
        ...baseOption,
        xAxis: {
          type: 'category',
          data: categories,
          axisLine: {
            lineStyle: {
              color: '#e9ecef'
            }
          },
          axisLabel: {
            color: '#666'
          }
        },
        yAxis: {
          type: 'value',
          axisLine: {
            lineStyle: {
              color: '#e9ecef'
            }
          },
          axisLabel: {
            color: '#666'
          },
          splitLine: {
            lineStyle: {
              color: '#f3f4f6'
            }
          }
        },
        series: [{
          name: this.title,
          type: 'line',
          data: values,
          smooth: true,
          lineStyle: {
            color: '#6366F1',
            width: 3
          },
          itemStyle: {
            color: '#6366F1'
          },
          areaStyle: {
            color: {
              type: 'linear',
              x: 0,
              y: 0,
              x2: 0,
              y2: 1,
              colorStops: [{
                offset: 0, color: 'rgba(99, 102, 241, 0.3)'
              }, {
                offset: 1, color: 'rgba(99, 102, 241, 0.05)'
              }]
            }
          }
        }]
      };
    },
    
    generatePieChart(baseOption) {
      return {
        ...baseOption,
        tooltip: {
          trigger: 'item',
          formatter: '{a} <br/>{b}: {c} ({d}%)'
        },
        series: [{
          name: this.title,
          type: 'pie',
          radius: ['40%', '70%'],
          center: ['50%', '50%'],
          data: this.data.map(item => ({
            name: item.name || item.label,
            value: item.value
          })),
          itemStyle: {
            borderRadius: 8,
            borderColor: '#fff',
            borderWidth: 2
          },
          label: {
            show: true,
            formatter: '{b}: {d}%'
          },
          emphasis: {
            itemStyle: {
              shadowBlur: 10,
              shadowOffsetX: 0,
              shadowColor: 'rgba(0, 0, 0, 0.5)'
            }
          }
        }]
      };
    },
    
    generateAreaChart(baseOption) {
      return this.generateLineChart(baseOption);
    },
    
    generateMixedChart(baseOption) {
      // 处理新的数据结构
      if (!this.data.categories || !this.data.series) {
        return baseOption;
      }
      
      return {
        ...baseOption,
        tooltip: {
          trigger: 'axis',
          backgroundColor: 'rgba(0, 0, 0, 0.8)',
          borderColor: 'transparent',
          textStyle: {
            color: '#fff'
          },
          formatter: function(params) {
            let result = params[0].name + '<br/>';
            params.forEach(param => {
              const unit = param.seriesName.includes('成交率') ? '%' : '';
              result += `${param.marker} ${param.seriesName}: ${param.value}${unit}<br/>`;
            });
            return result;
          }
        },
        legend: {
          data: this.data.series.map(s => s.name),
          top: 10,
          textStyle: {
            color: '#666'
          }
        },
        xAxis: {
          type: 'category',
          data: this.data.categories,
          axisLine: {
            lineStyle: {
              color: '#e9ecef'
            }
          },
          axisLabel: {
            color: '#666',
            rotate: 0
          }
        },
        yAxis: [
          {
            type: 'value',
            name: '出货量',
            position: 'left',
            axisLine: {
              lineStyle: {
                color: '#e9ecef'
              }
            },
            axisLabel: {
              color: '#666'
            },
            splitLine: {
              lineStyle: {
                color: '#f3f4f6'
              }
            }
          },
          {
            type: 'value',
            name: '成交率(%)',
            position: 'right',
            axisLine: {
              lineStyle: {
                color: '#e9ecef'
              }
            },
            axisLabel: {
              color: '#666',
              formatter: '{value}%'
            },
            splitLine: {
              show: false
            }
          }
        ],
        series: this.data.series.map(seriesItem => {
          if (seriesItem.type === 'bar') {
            return {
              name: seriesItem.name,
              type: 'bar',
              data: seriesItem.data,
              yAxisIndex: seriesItem.yAxisIndex || 0,
              itemStyle: {
                color: '#6366F1',
                borderRadius: [4, 4, 0, 0]
              },
              emphasis: {
                itemStyle: {
                  color: '#4F46E5'
                }
              }
            };
          } else if (seriesItem.type === 'line') {
            return {
              name: seriesItem.name,
              type: 'line',
              data: seriesItem.data,
              yAxisIndex: seriesItem.yAxisIndex || 1,
              smooth: true,
              lineStyle: {
                color: '#10B981',
                width: 3
              },
              itemStyle: {
                color: '#10B981'
              },
              symbol: 'circle',
              symbolSize: 6
            };
          }
          return seriesItem;
        })
      };
    },
    
    refreshChart() {
      // 重置筛选条件为"本月"
      this.selectedPeriod = 'month';
      this.$emit('refresh', this.selectedPeriod);
      this.updateChart();
    },
    
    handlePeriodChange(period) {
      this.$emit('period-change', period);
      this.updateChart();
    },
    
    downloadChart() {
      if (this.chart) {
        const url = this.chart.getDataURL({
          type: 'png',
          backgroundColor: '#fff'
        });
        
        const link = document.createElement('a');
        link.download = `${this.title}_${new Date().getTime()}.png`;
        link.href = url;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
        
        this.$message.success('图表导出成功');
      }
    },
    
    handleResize() {
      if (this.chart) {
        this.chart.resize();
      }
    }
  }
});