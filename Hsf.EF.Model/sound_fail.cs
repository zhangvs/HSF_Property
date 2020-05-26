namespace Hsf.EF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("hsf.sound_fail")]
    public partial class sound_fail
    {
        [Key]
        [StringLength(50)]
        public string sessionId { get; set; }

        [StringLength(50)]
        public string deviceId { get; set; }

        [StringLength(10)]
        public string actionId { get; set; }

        [StringLength(50)]
        public string token { get; set; }

        [StringLength(250)]
        public string questions { get; set; }

        [StringLength(50)]
        public string IPAddress { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}
