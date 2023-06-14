using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Entities.Models
{
    public partial class Employee
    {
        public Employee()
        {
            PasswordExpiry = new HashSet<PasswordExpiry>();
        }

        public long EmpId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime? Dob { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? Attempts { get; set; }
        public int TotalAttempts { get; set; }
        public bool? Status { get; set; }
        public bool? IsLocked { get; set; }

        public virtual ICollection<PasswordExpiry> PasswordExpiry { get; set; }
    }
}
