namespace BannerGrabbing.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TIp")]
    public partial class TIp
    {
        public double ID { get; set; }

        public string IP { get; set; }
    }
}
