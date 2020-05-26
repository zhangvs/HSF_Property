namespace Hsf.EF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("hsf.hsf_residential")]
    public partial class hsf_residential
    {
        public int id { get; set; }

        [StringLength(20)]
        public string chinaname { get; set; }

        [StringLength(20)]
        public string residential { get; set; }

        [StringLength(50)]
        public string createuser { get; set; }

        public DateTime? createtime { get; set; }

        [StringLength(50)]
        public string modifiyuser { get; set; }

        public DateTime? modifiytime { get; set; }

        public int? deletemark { get; set; }
    }
}
