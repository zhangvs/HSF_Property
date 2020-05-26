namespace Hsf.EF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("hsf.baidu_items")]
    public partial class baidu_items
    {
        [Key]
        [StringLength(50)]
        public string wordid { get; set; }

        [StringLength(50)]
        public string titleid { get; set; }

        public int? byte_length { get; set; }

        public int? byte_offset { get; set; }

        [StringLength(10)]
        public string formal { get; set; }

        [StringLength(255)]
        public string item { get; set; }

        [StringLength(20)]
        public string ne { get; set; }

        [StringLength(20)]
        public string pos { get; set; }

        [StringLength(10)]
        public string uri { get; set; }

        [StringLength(10)]
        public string loc_details { get; set; }

        [StringLength(50)]
        public string basic_words { get; set; }

        public int? sort { get; set; }

        public long? timestamp { get; set; }

        [StringLength(50)]
        public string vec { get; set; }

        public int? iskey { get; set; }

        [StringLength(50)]
        public string ext1 { get; set; }

        [StringLength(50)]
        public string ext2 { get; set; }

        [StringLength(50)]
        public string ext3 { get; set; }
    }
}
