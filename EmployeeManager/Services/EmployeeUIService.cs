using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using EmployeeManager.Models;
using EmployeeManager.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using EmployeeManager.Views;

namespace EmployeeManager.Services
{
    /// <summary>
    ///  Introduced to handle UI requirements. The purpose is to avoid dependency of other Views/ ViewModels in the MainWindow ViewModel. 
    ///  EmployeeUIService provides methods to activate Add and Edit windows.
    /// </summary>
    internal class EmployeeUIService:IEmployeeUIService
    {
        private IServiceProvider _serviceProvider;
        public EmployeeUIService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void ActivateAdddNewEmployeeWindow()
        {
            Window addEmployeeWindow = _serviceProvider.GetRequiredService<EmployeeAdd>();
            AddEmployeeViewModel addEmployeeViewModel = _serviceProvider.GetRequiredService<AddEmployeeViewModel>();
            addEmployeeWindow.DataContext = addEmployeeViewModel;
            addEmployeeWindow.ShowDialog();
        }

        public void ActivateEditEmployeeWindow(IEmployee employee)
        {
            Window  editEmployeeWindow = _serviceProvider.GetRequiredService<EmployeeEdit>();
            EditEmployeeViewModel editEmployeeViewModel= _serviceProvider.GetRequiredService<EditEmployeeViewModel>();
            editEmployeeViewModel.SelectedEmployee = employee;
            editEmployeeWindow.DataContext = editEmployeeViewModel;
            editEmployeeWindow.ShowDialog();
        }
    }
}
