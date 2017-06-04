namespace SmartActS.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Location")]
    public partial class Location
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LocationId { get; set; }

        [StringLength(50)]
        public string LocationCode { get; set; }

        [StringLength(100)]
        public string LocationName { get; set; }

        public int? PrarentId { get; set; }

        public double? LngTitude { get; set; }

        public double? LatTitude { get; set; }
    }
}
