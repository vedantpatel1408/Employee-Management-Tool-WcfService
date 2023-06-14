using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class GetConfigurationDTO
    {
        public int TotalAttempts { get; set; }
        public int PasswordexpiryDay { get; set; }
    }
}
