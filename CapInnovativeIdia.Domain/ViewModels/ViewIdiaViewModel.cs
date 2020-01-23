using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CapInnovativeIdia.Domain.ViewModels
{
    public class ViewIdiaViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string IdiaDescription { get; set; }
        public string IdiaBenifit { get; set; }
        public string Customer { get; set; }
        public string IdiaCategory { get; set; }
        public string IdiaProposal { get; set; }
        public string IdiaStatus { get; set; }
        public int IdiaStatuId { get; set; }
        public int? ActionBy { get; set; }
        public DateTime? ActionDate { get; set; }
        public string CreatedBy { get; set; }
        public int CreatedUserid { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
