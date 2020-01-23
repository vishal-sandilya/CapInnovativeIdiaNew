using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CapInnovativeIdia.Domain.ViewModels
{
    public class LoginUserViewModel
    {
        [Required(ErrorMessage ="Please eneter your email.")]
        [EmailAddress(ErrorMessage ="Please enter a valid email.")]
        [MaxLength(200,ErrorMessage ="Email address cannot be more than 200 characters.")]
        public string Email { get; set; }
        [Required(ErrorMessage ="Please enter your password.")]
        [MinLength(8,ErrorMessage ="Password should be at least 8 charachers.")]
        [MaxLength(20,ErrorMessage ="Password cannot be more than 20 characters.")]
        public string Password { get; set; }
    }
}
