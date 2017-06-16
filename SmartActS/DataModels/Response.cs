namespace SmartActS.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Response")]
    public partial class Response
    {
        public int ResponseId { get; set; }

        [StringLength(100)]
        public string ResponseTitle { get; set; }

        public int RequestId { get; set; }

        public int SupplyId { get; set; }

            [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}")]
            public DateTime? ResponseTime { get; set; }

        public int? Status { get; set; }
        [DisplayFormat(DataFormatString = "{0:C0}")]
        [Column(TypeName = "money")]
        public decimal? PriceSuggest { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        public int? FileAttachId { get; set; }

        public int categoryId { get; set; }
    }
}
