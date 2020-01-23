using System;
using System.Collections.Generic;
using System.Text;

namespace CapInnovativeIdia.Domain.Domains
{
    public class IdiaStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte IsActive { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
