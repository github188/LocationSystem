﻿using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using DbModel.Tools;
using Location.TModel.Tools;

namespace DbModel.Location.AreaAndDev
{
    /// <summary>
    /// 设备类型
    /// </summary>
    [DataContract]
    public class DevType
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        [DataMember]
        [Display(Name = "主键Id")]
        public int Id { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        [DataMember]
        [Display(Name = "类型名称")]
        public string TypeName { get; set; }

        /// <summary>
        /// 大类
        /// </summary>
        [DataMember]
        [Display(Name = "大类")]
        public string Class { get; set; }

        /// <summary>
        /// 类型编号
        /// </summary>
        [DataMember]
        [Display(Name = "类型编号")]
        public long TypeCode { get; set; }

        /// <summary>
        /// 前面板
        /// </summary>
        [DataMember]
        [Display(Name = "前面板")]
        public string FrontElevation { get; set; }


        public DevType Clone()
        {
            var copy = this.CloneObjectByBinary();
            return copy;
        }
    }
}