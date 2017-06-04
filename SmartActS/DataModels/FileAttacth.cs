namespace SmartActS.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FileAttacth")]
    public partial class FileAttacth
    {
        [Key]
        public int FileAttachId { get; set; }

        [StringLength(500)]
        public string FileName { get; set; }

        [StringLength(1000)]
        public string FilePath { get; set; }

        public double? FileSize { get; set; }
    }
}
