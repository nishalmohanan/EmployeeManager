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
using System.Windows.Controls;

namespace EmployeeManager.ViewModels
{
    internal class AddEmployeeViewModel:ObservableRecipient
    {
        private string _name;
        private string _email;
        private Gender _gender;
        
        public string Name { get { return _name; } set { SetProperty(ref _name, value); } }
        public string Email { get { return _email; } set { SetProperty(ref _email, value); } }
        public Gender Gender { get { return _gender; } set { SetProperty(ref _gender, value); } }
        public ICommand CreateEmployeeCommand { get; set; }


        private readonly IEmployeeManagerState _employeeManagerState;
        private readonly IEmployeeDataRepository _employeeDataRepository;


        public AddEmployeeViewModel(IEmployeeDataRepository employeeDataRepository, IEmployeeManagerState employeeManagerState)
        {
            _employeeDataRepository = employeeDataRepository;
            _employeeManagerState = employeeManagerState;

            CreateEmployeeCommand = new RelayCommand<Window>(async (Window addWindow) =>
            {    
                var result  = await _employeeDataRepository.Create(new Employee() { Name = _name, Email = _email, Gender = _gender, Status = Status.Active });
                if (result != null)
                {
                    employeeManagerState.InvokeEmployeeCreatedEvent(result);
                    addWindow.Close();
                }
            }, (Window  window) => {  
                return  true;
            });


            _gender = Gender.Male;
            _name = "";
            _email = "";
        }



    }
}
