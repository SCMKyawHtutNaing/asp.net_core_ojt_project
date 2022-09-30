using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable

namespace DotNetCoreProject.Entity.DataContext
{
    public partial class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        [DefaultValue(0)]
        public int UpdatedUserId { get; set; }
        public DateTime? DeletedDate { get; set; }
        [DefaultValue(0)]
        public int DeletedUserId { get; set; }
    }
}
