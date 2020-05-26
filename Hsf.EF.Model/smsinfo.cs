namespace Hsf.EF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("hsf.smsinfo")]
    public partial class smsinfo
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string Tel { get; set; }

        [StringLength(10)]
        public string Captcha { get; set; }

        public int? SendCount { get; set; }

        public int? Status { get; set; }

        public int? Type { get; set; }

        public DateTime? CreateTime { get; set; }

        [StringLength(255)]
        public string SmsContent { get; set; }
    }
}
