namespace Hsf.EF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("hsf.host_user")]
    public partial class host_user
    {
        [Key]
        [StringLength(50)]
        public string userid { get; set; }

        [StringLength(50)]
        public string username { get; set; }

        [StringLength(50)]
        public string userpass { get; set; }

        [StringLength(255)]
        public string userimg { get; set; }

        [StringLength(255)]
        public string userdevice { get; set; }

        public int? deletemark { get; set; }

        public DateTime? createtime { get; set; }

        public DateTime? modifiytime { get; set; }
    }
}
