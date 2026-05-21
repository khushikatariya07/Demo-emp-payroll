using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class Admin
    {

         [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get ; set ;}


         [Required]
        [EmailAddress]
        public string Email {get ; set;}

        [Required]
        public string Password {get ;set;}
    }
}