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
        [Display(Name = "Posted User")]
        public string PostedUser { get; set; }
        [Display(Name = "Posted Date")]
        public string PostedDate { get; set; }
        public bool Status { get; set; }

        public List<PostViewModel> posts { get; set; }
    }
}
