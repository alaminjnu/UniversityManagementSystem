using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemByReturnNull.Models
{
    public class Teacher
    {
        public int Id { get; set; }


        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string ContactNo { get; set; }


        [Required]
        [DisplayName("Designation")]
        public int DesignationId { get; set; }

        [DisplayName("Department")]
        [Required]
        public int DepartmentId { get; set; }

        [Range(0.0, Double.MaxValue, ErrorMessage = "Must be Non Negative ")]
        [Required]
        [DisplayName("Credit To be Taken")]
        public float CreditToBeTaken { get; set; }

        [Required]
        [DisplayName("Remaining Credit")]
        public float CreditTaken { get; set; }
    }
}