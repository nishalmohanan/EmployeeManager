using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using EmployeeManager.Enums;
using System.Windows.Input;
using EmployeeManager.Repositories;
using EmployeeManager.Services;
using EmployeeManager.Models;
using System.Windows;

namespace EmployeeManager.ViewModels
{
    internal class EditEmployeeViewModel : ObservableRecipient
    {
        private IEmployee _selectedEmployee;
        public IEmployee SelectedEmployee { 
                get {
                    return _selectedEmployee; 
                }
                set {
                SetProperty(ref _selectedEmployee, value);
               } 
        }
        public ICommand UpdateCommand { get; set; }

        private readonly IEmployeeDataRepository _employeeDataRepository;
        private readonly IEmployeeManagerState _employeeManagerState;


        public EditEmployeeViewModel(IEmployeeDataRepository employeeDataRepository, IEmployeeManagerState employeeManagerState)
        {
            _employeeDataRepository = employeeDataRepository;
            _employeeManagerState = employeeManagerState;
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            UpdateCommand = new RelayCommand<Window>(async (Window editWindow) =>
            {
                if (_selectedEmployee != null)
                {
                    await _employeeDataRepository.Update(_selectedEmployee);
                    _employeeManagerState.InvokeEmployeeUpdatedEvent(_selectedEmployee);
                }
                editWindow.Close();
            }, (Window window) => { return true; });
        }
    }
}
