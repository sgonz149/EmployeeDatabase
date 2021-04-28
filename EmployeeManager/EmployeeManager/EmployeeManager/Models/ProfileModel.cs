using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Models
{
    class ProfileModel
    {
        public int ProfilesId { get; set; }
        public int EmployeeId { get; set; }

        public int CompanyId { get; set;}

        public bool IsCommitted { get; set; }
    }
}
