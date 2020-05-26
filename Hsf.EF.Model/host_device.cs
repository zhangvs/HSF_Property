namespace Hsf.EF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("hsf.host_device")]
    public partial class host_device
    {
        [StringLength(50)]
        public string Id { get; set; }

        [Required]
        [StringLength(20)]
        public string deviceid { get; set; }

        [StringLength(50)]
        public string chinaname { get; set; }

        public int? classfid { get; set; }

        [StringLength(50)]
        public string devtype { get; set; }

        [StringLength(50)]
        public string devip { get; set; }

        [Required]
        [StringLength(50)]
        public string devmac { get; set; }

        [StringLength(20)]
        public string devport { get; set; }

        [StringLength(50)]
        public string cachekey { get; set; }

        [Required]
        [StringLength(50)]
        public string devposition { get; set; }

        [StringLength(50)]
        public string devchannel { get; set; }

        [StringLength(50)]
        public string imageid { get; set; }

        [StringLength(50)]
        public string userid { get; set; }

        [StringLength(50)]
        public string powvalue { get; set; }

        [StringLength(50)]
        public string devstate { get; set; }

        [StringLength(50)]
        public string devstate1 { get; set; }

        [StringLength(50)]
        public string devstate2 { get; set; }

        [StringLength(50)]
        public string account { get; set; }

        [StringLength(50)]
        public string mac { get; set; }

        [StringLength(50)]
        public string createuser { get; set; }

        public DateTime? createtime { get; set; }

        [StringLength(50)]
        public string modifiyuser { get; set; }

        public DateTime? modifiytime { get; set; }

        public int? deletemark { get; set; }
    }
}
