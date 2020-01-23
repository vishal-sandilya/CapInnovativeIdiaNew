using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CapInnovativeIdia.Domain.Domains
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please provide first name.")]
        [MaxLength(200,ErrorMessage ="First name should not be more than 200 characters.")]
        public string FirstName { get; set; }
        [Required (ErrorMessage ="Please provide last name.")]
        [MaxLength(200, ErrorMessage = "Last name should not be more than 200 characters.")]
        public string LastName { get; set; }
        [Required (ErrorMessage ="Please provide email.")]
        [MaxLength(200, ErrorMessage = "Email should not be more than 200 characters.")]
        [EmailAddress(ErrorMessage ="Please provide a valid email.")]
        public string Email { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage ="Please select gender.")]
        public string Gender { get; set; }
        [Required(ErrorMessage ="Please select team.")]
        public int TeamId { get; set; }
        public byte IsActive { get; set; }
        public byte IsVarified { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
