namespace Hsf.EF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("hsf.baidu_terms")]
    public partial class baidu_terms
    {
        [StringLength(50)]
        public string Id { get; set; }

        [StringLength(500)]
        public string Message { get; set; }

        [StringLength(1000)]
        public string Terms { get; set; }

        public DateTime? CreateTime { get; set; }
    }
}
