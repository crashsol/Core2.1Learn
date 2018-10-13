using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DYBus.Models
{
     public  class BusRunTimeInfo
    {
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 所属路线
        /// </summary>
        public string RoadNum { get; set; } 

        /// <summary>
        /// 公交车编号 
        /// </summary>
        public string BusCarNo { get; set; }

        /// <summary>
        /// 行驶的方向
        /// 1 左边  2 右边
        /// </summary>
        public string BusRunDirection { get; set; } 


        /// <summary>
        /// 公交车状态
        /// 1 到站
        /// 0 正在行驶
        /// </summary>
        public string BusStatus { get; set; }


        /// <summary>
        /// 上一站位置编号
        /// </summary>
        public string BeforeStationNo { get; set; }

        /// <summary>
        /// 驶向下一站的车站编号
        /// </summary>
        public string CurrentStationNo { get; set; } 


        /// <summary>
        /// 是否在站台停站
        /// 1 是
        /// 0 否
        /// </summary>
        public string IsBusStop { get; set; }  

        public DateTime CreateTime { get; set; } = DateTime.Now;

    }
}
