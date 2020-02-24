namespace BannerGrabbing.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TPortStatus
    {
        public double ID { get; set; }

        public string PortStatus { get; set; }
    }
}
