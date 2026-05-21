using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class Emp
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpId {get ; set ;}

        [Required]
        public string Name {get ; set ;}

        [Required]
        [EmailAddress]
        public string Email {get ; set;}

        [Required]
        public string Gender {get ; set ;}

        [Required]
        [Phone]
        public string Mobile {get ;set ;}

        
        public decimal Salary{get ;set ;} = 0;

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@@$!%*?&])[A-Za-z\\d@@$!%*?&]{8,}$",ErrorMessage ="Password must be 8+ chars with Upper, Lower, Number, and Special char.")]
        public string Password {get ;set;}

        [Required]
        [Compare("Password")]
        public string ConfirmPassword {get ;set ;}


    }
}