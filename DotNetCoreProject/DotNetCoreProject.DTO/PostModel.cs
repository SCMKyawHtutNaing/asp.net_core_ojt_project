﻿using System;
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
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Posted User")]
        public string PostedUser { get; set; }
        [Required]
        [Display(Name = "Posted Date")]
        public string PostedDate { get; set; }

        public List<PostViewModel> posts { get; set; }
    }
}
