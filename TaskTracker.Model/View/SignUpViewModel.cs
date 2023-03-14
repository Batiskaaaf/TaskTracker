using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTracker.Model.View
{
    public class SignUpViewModel
    {

        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Phone]
        public required string Number { get; set; }

        [Required]
        [Compare("ConfirmPassword")]
        public required string Password { get; set; }

        [Required]
        public required string ConfirmPassword { get; set; }
    }
}
