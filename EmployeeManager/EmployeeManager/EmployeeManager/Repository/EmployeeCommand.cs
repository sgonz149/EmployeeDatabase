using Dapper;
using EmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Repository
{
    class EmployeeCommand
    {
        //string to connect to database
        private string _connectionString;

        //constructor takes in connection string
        public EmployeeCommand(string connectionString)
        {
            //map to existing connection string
            _connectionString = connectionString;
        
        }

        //get list for employee getlist stored procedure
        public IList<EmployeeModel> GetList()
        {
            //new list for getlist info
            List<EmployeeModel> employee = new List<EmployeeModel>();

            var sql = "Employee_GetList";

            //sql connection coming from sql client
            using (SqlConnection connection = new SqlConnection(_connectionString)) 
            {
                //using dapper query grab the employee models sql(stored procedure stated above)
                //will map to employeemodel list
                employee = connection.Query<EmployeeModel>(sql).ToList();
            
            }
                return employee;
        }
    }
}
