using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DotNetCoreProject.Entity.DataContext
{
    public partial class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public int Status { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public string? CreatedUserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeletedDate { get; set; }
        public string? DeletedUserId { get; set; }
    }
}
