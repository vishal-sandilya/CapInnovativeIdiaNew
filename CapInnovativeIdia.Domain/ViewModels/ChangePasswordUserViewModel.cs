using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CapInnovativeIdia.Domain.ViewModels
{
    public class ChangePasswordUserViewModel
    {
        [Required(ErrorMessage ="Please provide your current password.")]
        [MinLength(8,ErrorMessage ="Password should be at least 8 characters.")]
        [MaxLength(20,ErrorMessage ="Password cannot be more than 20 characters")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "Please provide your new password.")]
        [MinLength(8, ErrorMessage = "Password should be at least 8 characters.")]
        [MaxLength(20, ErrorMessage = "Password cannot be more than 20 characters")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Please confirm your new password.")]
        [MinLength(8, ErrorMessage = "Password should be at least 8 characters.")]
        [MaxLength(20, ErrorMessage = "Password cannot be more than 20 characters")]
        [Compare("NewPassword", ErrorMessage ="Password does not match, please provide correct password.")]
        public string ConfirmNewPassword { get; set; }
    }
}
