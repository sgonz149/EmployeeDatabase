using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Models
{
    class EmployeeModel
    {
        public int EmployeeId {get; set;}

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string JobTitle { get; set; }

        public bool IsCommitted { get; set; }
    }
}
