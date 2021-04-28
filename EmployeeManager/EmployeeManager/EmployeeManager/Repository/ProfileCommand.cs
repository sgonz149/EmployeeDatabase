using Dapper;
using EmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Repository
{
    class ProfileCommand
    {
        private string _connectionString;

        //constructor takes in connection string
        public ProfileCommand(string connectionString)
        {
            //map to existing connection string
            _connectionString = connectionString;

        }

        public IList<ProfileModel> GetList()
        {
            List<ProfileModel> profiles = new List<ProfileModel>();

            var sql = "Profile_GetList";

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                profiles = connection.Query<ProfileModel>(sql).ToList();
            }

            foreach (var profile in profiles)
            {
                profile.IsCommitted = true;
            }
            return profiles;
        }

        //our upsert stored procedure
        public void Upsert(ProfileModel profileModel)
        {
            var sql = "Employee_Upsert";
            var employeeId = System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();

            //user defined table
            var dataTable = new DataTable();

            dataTable.Columns.Add("ProfilesId", typeof(int));
            dataTable.Columns.Add("EmployeeId", typeof(int));
            dataTable.Columns.Add("CompanyId", typeof(int));
            dataTable.Rows.Add(profileModel.ProfilesId, profileModel.EmployeeId, profileModel.CompanyId);

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Execute(sql, new { @EmployeeType = dataTable.AsTableValuedParameter("EmployeeType")}, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
