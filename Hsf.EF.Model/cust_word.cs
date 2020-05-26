namespace Hsf.EF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("hsf.cust_word")]
    public partial class cust_word
    {
        [Key]
        [StringLength(50)]
        public string wordid { get; set; }

        [StringLength(50)]
        public string actionid { get; set; }

        [StringLength(50)]
        public string wortdtext { get; set; }

        [StringLength(50)]
        public string wordweigh { get; set; }

        [StringLength(50)]
        public string ext1 { get; set; }

        [StringLength(50)]
        public string ext2 { get; set; }

        [StringLength(50)]
        public string ext3 { get; set; }
    }
}
