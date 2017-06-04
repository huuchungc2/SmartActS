namespace SmartActS.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Customer")]
    public partial class Customer
    {
        public int CustomerId { get; set; }

        [StringLength(50)]
        public string CustomerCode { get; set; }

        [StringLength(50)]
        public string CustomerName { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string MobiPhone { get; set; }

        [StringLength(1000)]
        public string Address { get; set; }

        public double? LngTitude { get; set; }

        public double? LatTitude { get; set; }

        public int? LocationId { get; set; }

        public int? IsStatus { get; set; }

        [StringLength(128)]
        public string UserId { get; set; }
    }
}
