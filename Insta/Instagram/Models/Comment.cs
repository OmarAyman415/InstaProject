//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Instagram.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public int CommentId { get; set; }
        public Nullable<int> PostId { get; set; }
        public Nullable<int> UserCommmentedId { get; set; }
        public System.DateTime CommentDate { get; set; }
        public string CommentText { get; set; }
        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
