namespace SmartActS.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rank")]
    public partial class Rank
    {
        public int RankId { get; set; }

        [StringLength(50)]
        public string RankCode { get; set; }

        [StringLength(50)]
        public string RankName { get; set; }
    }
}
