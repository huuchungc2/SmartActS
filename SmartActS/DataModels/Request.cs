namespace SmartActS.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Request")]
    public partial class Request
    {
        public int RequestId { get; set; }

        [StringLength(50)]
        public string RequestCode { get; set; }

        public int CategoryId { get; set; }

        public int CustomerId { get; set; }

        public int? Status { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? DurationExpired { get; set; }

        [Column(TypeName = "money")]
        public decimal? FromBudget { get; set; }

        [Column(TypeName = "money")]
        public decimal? ToBudget { get; set; }

        public int? RequireResponse { get; set; }

        [StringLength(1000)]
        public string ShippingAddress { get; set; }

        public int? LocationSupplyId { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public bool? BestTime { get; set; }

        public bool? BestSupply { get; set; }

        public bool? BestPrice { get; set; }

        [StringLength(100)]
        public string RequestTitle { get; set; }

        public int? FileAttrachId { get; set; }
    }
}
