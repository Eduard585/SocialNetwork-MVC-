//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comments
    {
        public long ID { get; set; }
        public System.DateTime Date { get; set; }
        public long UserID { get; set; }
        public long PostID { get; set; }
        public string CommentText { get; set; }
    
        public virtual Posts Posts { get; set; }
        public virtual Users Users { get; set; }
    }
}
