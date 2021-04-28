using Caliburn.Micro;
using EmployeeManager.Models;
using EmployeeManager.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.ViewModels
{
    //inherit from screen
    class MainViewModel : Screen
    {
        //adding list into application view

        private BindableCollection<ProfileModel> profiles = new BindableCollection<ProfileModel>();
        private BindableCollection<EmployeeModel> employees = new BindableCollection<EmployeeModel>();
        private BindableCollection<CompanyModel> companies = new BindableCollection<CompanyModel>();
        private readonly string _connectionString = @"Data Source=DESKTOP-P88LT4B;Initial Catalog=EmployeeReport;Integrated Security=True";
        private string appStatus;
        private ProfileModel selectedEmployee;
        private ProfileCommand profileCommand;
        public MainViewModel()
        {
            SelectedProfile = new ProfileModel();
            //pulls models from external source but no logic which collections are for
            try
            {
                profileCommand = new ProfileCommand(_connectionString);
                //list that comes from the database will be added to Emplyees Bindable collection
                Profiles.AddRange(profileCommand.GetList());

                EmployeeCommand employeeCommand = new EmployeeCommand(_connectionString);
                //list that comes from the database will be added to Emplyees Bindable collection
                Employees.AddRange(employeeCommand.GetList());

                CompanyCommand companyCommand = new CompanyCommand(_connectionString);
                //list that comes from the database will be added to Emplyees Bindable collection
                Companies.AddRange(companyCommand.GetList());
            }
            catch (Exception ex)
            {
                AppStatus = ex.Message;
                NotifyOfPropertyChange(() => AppStatus);
            }
        }

        //collection to bind to Employees dropdown itemsource drop downs
        //info our view model gets
        //to get from private variable to bind to viewmodel
        public EmployeeModel SelectedEmployeeProfile
        {
            get
            {
                try
                {
                    var employeesDictionary = employees.ToDictionary(b => b.EmployeeId);

                    if (SelectedProfile != null && employeesDictionary.ContainsKey(SelectedProfile.EmployeeId))
                    {
                        return employeesDictionary[SelectedProfile.EmployeeId];
                    }
                }
                catch (Exception ex)
                {
                    UpdateAppStatus(ex.Message);
                }

                return null;
            }
            set 
            {
                try
                {
                    var selectedProfile = value;

                    SelectedProfile.EmployeeId = selectedProfile.EmployeeId;

                    NotifyOfPropertyChange(() => SelectedProfile);
                }
                catch (Exception ex)
                {
                    UpdateAppStatus(ex.Message);
                }
            }
        }

        public CompanyModel SelectedCompanyProfile
        {
            get
            {
                try
                {
                    var companyDictionary = companies.ToDictionary(b => b.CompanyId);

                    if (SelectedProfile != null && companyDictionary.ContainsKey(SelectedProfile.CompanyId))
                    {
                        return companyDictionary[SelectedProfile.CompanyId];
                    }
                }
                catch (Exception ex)
                {
                    UpdateAppStatus(ex.Message);
                }

                return null;
            }
            set
            {
                try
                {
                    var selectedProfile = value;

                    SelectedProfile.CompanyId = selectedProfile.CompanyId;

                    NotifyOfPropertyChange(() => SelectedProfile);
                }
                catch (Exception ex)
                {
                    UpdateAppStatus(ex.Message);
                }
            }
        }
        public BindableCollection<ProfileModel> Profiles
        {
            get { return profiles; }
            set { profiles = value; }
        }
        public BindableCollection<EmployeeModel> Employees
        {
            get { return employees; }
            set { employees = value; }
        }

        public BindableCollection<CompanyModel> Companies
        {
            get { return companies; }
            set { companies = value; }
        }

        public string AppStatus
        {
            get { return appStatus; }
            set { appStatus = value; }
        }

        public ProfileModel SelectedProfile 
        {
            get 
            {
                return selectedEmployee;
            }
            set 
            {

                selectedEmployee = value;
                NotifyOfPropertyChange(() => SelectedProfile);
                NotifyOfPropertyChange(() => SelectedEmployeeProfile);
                NotifyOfPropertyChange(() => SelectedCompanyProfile);
            }
        
        }

        public void NewProfile()
        {
            try
            {
                SelectedProfile = new ProfileModel();
                UpdateAppStatus("New Profile Created");
            }
            catch (Exception ex)
            {
                UpdateAppStatus(ex.Message);
            }
        }

        public void SaveProfile()
        {
            try
            {
                var profilesDictionary = profiles.ToDictionary(p => p.ProfilesId);

                if (SelectedProfile != null)
                {
                    profileCommand.Upsert(SelectedProfile);
                    profiles.Clear();
                    profiles.AddRange(profileCommand.GetList());
                    UpdateAppStatus("Profile Saved");
                   
                   
                }
            }
            catch (Exception ex)
            {
                UpdateAppStatus(ex.Message);
            }
        }
        private void UpdateAppStatus(string message)
        {

            AppStatus = message;
            NotifyOfPropertyChange(() => AppStatus);
        }
    }
}
