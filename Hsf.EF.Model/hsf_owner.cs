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

        [Display(Name = "�ֻ���")]
        [StringLength(20)]
        public string telphone { get; set; }

        [Display(Name = "����")]
        [StringLength(50)]
        public string password { get; set; }

        [Display(Name = "ҵ������")]
        [StringLength(50)]
        public string ownername { get; set; }

        [Display(Name = "С����")]
        [StringLength(50)]
        public string chinaname { get; set; }

        [Display(Name = "С������")]
        [StringLength(50)]
        public string residential { get; set; }

        [Display(Name = "¥��")]
        [StringLength(10)]
        public string building { get; set; }

        [Display(Name = "��Ԫ��")]
        [StringLength(10)]
        public string unit { get; set; }

        [Display(Name = "¥��")]
        [StringLength(10)]
        public string floor { get; set; }

        [Display(Name = "���ƺ�")]
        [StringLength(10)]
        public string room { get; set; }

        [Display(Name = "����")]
        [StringLength(50)]
        public string host { get; set; }

        [Display(Name = "Mac")]
        [StringLength(50)]
        public string userid { get; set; }

        [Display(Name = "����ʱ��")]
        public DateTime? createtime { get; set; }

        public int? deletemark { get; set; }
    }
}
