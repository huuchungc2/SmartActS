//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartActS.DataModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class Response1
    {
        public int ResponseId { get; set; }
        public string ResponseTitle { get; set; }
        public int RequestId { get; set; }
        public int SupplyId { get; set; }
        public Nullable<System.DateTime> ResponseTime { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<decimal> PriceSuggest { get; set; }
        public string Description { get; set; }
        public Nullable<int> FileAttachId { get; set; }
        public int categoryId { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
}
