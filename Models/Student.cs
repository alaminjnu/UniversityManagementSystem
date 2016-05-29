using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystemByReturnNull.Models
{
    public class Student
    {

        public int Id { get; set; }
        public string RegistrationNo { get; set; }

        [Required(ErrorMessage = "Please provide Student Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide Valid Email")]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Please provide Address")]
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Please provide Contact Number")]
        public string ContactNo { get; set; }

        [Required(ErrorMessage = "Please Select the date")]
        public DateTime RegistrationDate { get; set; }

        [Required]
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
    }
}