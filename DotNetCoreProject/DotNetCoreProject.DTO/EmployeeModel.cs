using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DotNetCoreProject.DTO
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Designation")]
        public string Designation { get; set; }
        [Required]
        [Display(Name = "Salary")]
        public decimal Salary { get; set; }
        [Required]
        [Display(Name = "Join Date")]
        public DateTime JoiningDate { get; set; }

        public List<EmployeeViewModel> employees { get; set; }
    }
}
