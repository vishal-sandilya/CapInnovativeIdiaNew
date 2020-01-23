using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CapInnovativeIdia.Domain.Domains
{
    public class Idia
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please provide idia title.")]
        [MaxLength(200,ErrorMessage ="Title should not be more than 200 characters. ")]
        public string Title { get; set; }
        [Required(ErrorMessage ="Please provide idia description.")]
        [MaxLength(65535, ErrorMessage = "Idia description should not be more than 65535 characters. ")]
        public string IdiaDescription { get; set; }
        [Required(ErrorMessage ="Please provide idia benefits.")]
        [MaxLength(65535, ErrorMessage = "Idia benifit should not be more than 65535 characters. ")]
        public string IdiaBenifit { get; set; }
        [Required(ErrorMessage ="Please select customer.")]
        public int? TeamId { get; set; }
        [Required(ErrorMessage ="Please select idia proposal.")]
        public int? IdiaProposalId { get; set; }
        [Required(ErrorMessage ="Please select idia category.")]
        public int? CategoryId { get; set; }
        public int IdiaStatusId { get; set; }
        public byte IsActive { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ActionBy { get; set; }
        public DateTime? ActionDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
