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
    class CompanyCommand
    {
        //string to connect to database
        private string _connectionString;

        //constructor takes in connection string
        public CompanyCommand(string connectionString)
        {
            //map to existing connection string
            _connectionString = connectionString;

        }

        //get list for company getlist stored procedure
        public IList<CompanyModel> GetList()
        {
            //new list for getlist info
            List<CompanyModel> companies = new List<CompanyModel>();

            var sql = "Company_GetList";

            //sql connection coming from sql client
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                //using dapper query grab the employee models sql(stored procedure stated above)
                //will map to companymodel list
                companies = connection.Query<CompanyModel>(sql).ToList();

            }
            return companies;
        }
    }
}
