using Caliburn.Micro;
using EmployeeManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeManager
{
    //stating this is where the application will startup
    class Startup: BootstrapperBase
    {
        public Startup()
        {
            //configuration required for framework
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            //for viewmodel, when app starts use mainviewmodel
            DisplayRootViewFor<MainViewModel>();
        }
    }
}
