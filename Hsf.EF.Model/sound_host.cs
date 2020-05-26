namespace Hsf.EF.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("hsf.sound_host")]
    public partial class sound_host
    {
        [StringLength(50)]
        public string id { get; set; }

        [StringLength(50)]
        public string chinaname { get; set; }

        [StringLength(10)]
        public string classfid { get; set; }

        [StringLength(20)]
        public string deviceid { get; set; }

        [StringLength(10)]
        public string devip { get; set; }

        [StringLength(20)]
        public string devmac { get; set; }

        [StringLength(10)]
        public string devport { get; set; }

        [StringLength(50)]
        public string devposition { get; set; }

        [StringLength(20)]
        public string devregcode { get; set; }

        [StringLength(20)]
        public string devtype { get; set; }

        [StringLength(20)]
        public string imageid { get; set; }

        public int? lgsort { get; set; }

        [StringLength(50)]
        public string userid { get; set; }

        [StringLength(10)]
        public string devstate { get; set; }

        [StringLength(10)]
        public string devstate1 { get; set; }

        [StringLength(10)]
        public string devstate2 { get; set; }

        [StringLength(50)]
        public string token { get; set; }

        public int? playstate { get; set; }

        public int? deletemark { get; set; }

        public DateTime? createtime { get; set; }

        public DateTime? modifiytime { get; set; }
    }
}
