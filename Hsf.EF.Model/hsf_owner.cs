namespace Hsf.EF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("hsf.hsf_owner")]
    public partial class hsf_owner
    {
        [StringLength(50)]
        public string Id { get; set; }

        [Display(Name = "手机号")]
        [StringLength(20)]
        public string telphone { get; set; }

        [Display(Name = "密码")]
        [StringLength(50)]
        public string password { get; set; }

        [Display(Name = "业主姓名")]
        [StringLength(50)]
        public string ownername { get; set; }

        [Display(Name = "小区名")]
        [StringLength(50)]
        public string chinaname { get; set; }

        [Display(Name = "小区编码")]
        [StringLength(50)]
        public string residential { get; set; }

        [Display(Name = "楼号")]
        [StringLength(10)]
        public string building { get; set; }

        [Display(Name = "单元号")]
        [StringLength(10)]
        public string unit { get; set; }

        [Display(Name = "楼层")]
        [StringLength(10)]
        public string floor { get; set; }

        [Display(Name = "门牌号")]
        [StringLength(10)]
        public string room { get; set; }

        [Display(Name = "主机")]
        [StringLength(50)]
        public string host { get; set; }

        [Display(Name = "Mac")]
        [StringLength(50)]
        public string userid { get; set; }

        [Display(Name = "创建时间")]
        public DateTime? createtime { get; set; }

        public int? deletemark { get; set; }
    }
}
