namespace Hsf.EF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("hsf.cust_answer")]
    public partial class cust_answer
    {
        [Key]
        [StringLength(50)]
        public string answerid { get; set; }

        [Required]
        [StringLength(50)]
        public string questionid { get; set; }

        [StringLength(250)]
        public string answertext { get; set; }

        [StringLength(50)]
        public string answertype { get; set; }

        [StringLength(50)]
        public string preid { get; set; }

        [StringLength(50)]
        public string nextid { get; set; }

        public int? sort { get; set; }

        [StringLength(50)]
        public string ext1 { get; set; }

        [StringLength(50)]
        public string ext2 { get; set; }

        [StringLength(50)]
        public string ext3 { get; set; }
    }
}
