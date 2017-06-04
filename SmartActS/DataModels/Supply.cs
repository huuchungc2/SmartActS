namespace SmartActS.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Supply")]
    public partial class Supply
    {
        public int SupplyId { get; set; }

        [StringLength(50)]
        public string SupplyCode { get; set; }

        [StringLength(100)]
        public string SupplyName { get; set; }

        public int? LocationId { get; set; }

        public double? LngTitude { get; set; }

        public double? Latitude { get; set; }

        public int? CategoryId { get; set; }

        [StringLength(1000)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string MobiPhone { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Website { get; set; }

        public int? RankId { get; set; }

        public int? IsStatus { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }
    }
}
