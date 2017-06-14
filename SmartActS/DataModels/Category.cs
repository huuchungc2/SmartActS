using System.Web.Mvc;
namespace SmartActS.DataModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
       
        public int CategoryId { get; set; }

        [StringLength(50)]
        public string CategoryCode { get; set; }

        [StringLength(50)]
        public string CategoryName { get; set; }

        public int? ParentId { get; set; }
        [NotMapped]
       // public System.Web.Mvc.SelectList CategoryList { get; set; 
       public List<SelectList> CategoryList { get; set; }
     

    }
}
