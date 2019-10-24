using System.Collections.Generic;

namespace DTOModelDLL.Common
{
    /// <summary>
    /// 方便EChart 前端渲染的数据Model
    /// </summary>
    public class DTO_RenderEChart
    {
        /// <summary>
        /// 
        /// </summary>
        public DTO_RenderEChart()
        {
            x_arr = new List<string>();
            y_set = new List<DTO_RenderEChartItem>();
            Title = "";
        }

        /// <summary>
        /// 标识图表类型，方便映射，名称变更时无需前端调整
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> x_arr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<DTO_RenderEChartItem> y_set { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Title { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class DTO_RenderEChartItem
    {
        /// <summary>
        /// 
        /// </summary>
        public DTO_RenderEChartItem() 
        {
            y_arr = new List<string>();
            ChartType = "line";
            YAxisType = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ID { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string displayName { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string Column { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ChartType { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public int YAxisType { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public List<string> y_arr { get; set; }
    }
}