using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemByReturnNull.Models
{
    public class CourseAssignToTeacher
    {
        public int Id { get; set; }

        [DisplayName("Department")]
        [Required]
        public int DepartmentId { get; set; }
        
        [DisplayName("Teacher")]
        [Required]
        public int TeacherId { get; set; }

        
        [Required(ErrorMessage = "Credit Must be a Non Negative Value")]
        [DisplayName("Credit To be Taken")]
        public float CreditToBeTaken { get; set; }
        
        [Required]
        [DisplayName("Remaining Credit")]
        public float CreditTaken { get; set; }

        [Required]
        [DisplayName("Course Code")]
        public int CourseId { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public float Credit { get; set; }
        
        public bool Status { get; set; }
    }
}