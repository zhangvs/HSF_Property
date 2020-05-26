namespace Hsf.EF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("hsf.sound_title")]
    public partial class sound_title
    {
        [Key]
        [StringLength(50)]
        public string titleid { get; set; }

        [StringLength(255)]
        public string titletext { get; set; }

        public long? timestamp { get; set; }

        [StringLength(50)]
        public string preid { get; set; }

        [StringLength(50)]
        public string nextid { get; set; }

        public int? sort { get; set; }

        [StringLength(50)]
        public string talkid { get; set; }

        [StringLength(50)]
        public string sender { get; set; }

        [StringLength(50)]
        public string userid { get; set; }

        [StringLength(20)]
        public string sendtype { get; set; }

        [StringLength(20)]
        public string field { get; set; }

        [StringLength(20)]
        public string talkstate { get; set; }

        [StringLength(50)]
        public string ext1 { get; set; }

        [StringLength(50)]
        public string ext2 { get; set; }

        [StringLength(50)]
        public string ext3 { get; set; }
    }
}
