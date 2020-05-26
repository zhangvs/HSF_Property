namespace Hsf.EF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("hsf.host_room")]
    public partial class host_room
    {
        [StringLength(50)]
        public string id { get; set; }

        [Required]
        [StringLength(20)]
        public string posid { get; set; }

        [StringLength(20)]
        public string chinaname { get; set; }

        [StringLength(10)]
        public string imageid { get; set; }

        [StringLength(1)]
        public string postype { get; set; }

        [StringLength(50)]
        public string userid { get; set; }

        [StringLength(50)]
        public string Account { get; set; }

        [StringLength(50)]
        public string Mac { get; set; }

        public DateTime? CreateTime { get; set; }

        [StringLength(50)]
        public string CreateUser { get; set; }

        public DateTime? ModifyTime { get; set; }

        [StringLength(50)]
        public string ModifyUser { get; set; }

        public int? DeleteMark { get; set; }
    }
}
