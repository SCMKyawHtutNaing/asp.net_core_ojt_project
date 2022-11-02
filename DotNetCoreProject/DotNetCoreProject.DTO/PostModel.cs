using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DotNetCoreProject.DTO
{
    public class PostViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title can't be blank.")]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description can't be blank.")]
        [StringLength(255, ErrorMessage = "255 characters is the maximum allowed.")]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Created User")]
        public string CreatedUser { get; set; }
        [Display(Name = "Created Date")]
        public string CreatedDate { get; set; }
        [Display(Name = "Updated User")]
        public string UpdatedUser { get; set; }
        [Display(Name = "Updated Date")]
        public string UpdatedDate { get; set; }
        public bool Status { get; set; }
        public List<PostViewModel> posts { get; set; }
    }

    public class PostCSVModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
