namespace Hsf.EF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("hsf.hsf_outdevice")]
    public partial class hsf_outdevice
    {
        [StringLength(50)]
        public string Id { get; set; }

        [Display(Name = "小区编码")]
        [StringLength(50)]
        public string residential { get; set; }

        [Display(Name = "楼号")]
        [StringLength(10)]
        public string building { get; set; }

        [Display(Name = "单元")]
        [StringLength(10)]
        public string unit { get; set; }

        [Display(Name = "设备id")]
        [StringLength(20)]
        public string deviceid { get; set; }

        [Display(Name = "设备名称")]
        [StringLength(50)]
        public string chinaname { get; set; }
        
        public int? classfid { get; set; }

        [Display(Name = "设备类型")]
        [StringLength(50)]
        public string devtype { get; set; }
        
        [StringLength(50)]
        public string devip { get; set; }
        
        [StringLength(50)]
        public string devmac { get; set; }

        [StringLength(20)]
        public string devport { get; set; }

        [StringLength(50)]
        public string cachekey { get; set; }

        [StringLength(50)]
        public string devposition { get; set; }

        [StringLength(50)]
        public string devchannel { get; set; }

        [StringLength(50)]
        public string imageid { get; set; }

        [StringLength(50)]
        public string userid { get; set; }

        [StringLength(50)]
        public string powvalue { get; set; }

        [StringLength(50)]
        public string devstate { get; set; }

        [StringLength(50)]
        public string devstate1 { get; set; }

        [StringLength(50)]
        public string devstate2 { get; set; }

        [StringLength(50)]
        public string account { get; set; }

        [StringLength(50)]
        public string mac { get; set; }

        [StringLength(50)]
        public string createuser { get; set; }

        [Display(Name = "创建时间")]
        public DateTime? createtime { get; set; }

        [StringLength(50)]
        public string modifiyuser { get; set; }

        public DateTime? modifiytime { get; set; }

        public int? deletemark { get; set; }
    }
}
