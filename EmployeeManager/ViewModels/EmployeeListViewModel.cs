using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Windows.Input;

using EmployeeManager.Repositories;
using EmployeeManager.Models;
using EmployeeManager.Services;
using System.Collections.ObjectModel;
using System.Windows;

namespace EmployeeManager.ViewModels
{
    internal class EmployeeListViewModel:ObservableRecipient
    {
        private readonly IEmployeeDataRepository _employeeDataRepository;
        private readonly IEmployeeUIService _employeeUIService;
        private readonly IEmployeeManagerState _employeeManagerState;

        private  ObservableCollection<IEmployee> _employeeList;
        private Pagination? _paginationDetails;
        private IEmployee _selectedEmployee;
        private string _searchText;
        private string _statusText;
        private long _currentPage;

        public IEmployee SelectedEmployee { get { return _selectedEmployee; } set {  SetProperty(ref _selectedEmployee, value); } }
        public Pagination PaginationDetails { get { return _paginationDetails; } set { SetProperty(ref _paginationDetails, value); } }
        public ObservableCollection<IEmployee> EmployeeList { get { return _employeeList; } set { SetProperty(ref _employeeList, value); } }
        public string SearchData { get { return _searchText; } set { SetProperty(ref _searchText, value); } }
        public string StatusText { get { return _statusText; } set { SetProperty(ref _statusText, value); } }
        public long  CurrentPageNumber { get { return _currentPage; } set { SetProperty(ref _currentPage, value); } }

        public ICommand EditCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        public ICommand MoveFirstCommand { get; set; }
        public ICommand MoveLastCommand { get; set; }
        public ICommand MoveNextCommand { get; set; }
        public ICommand MovePrevCommand { get; set; }
        public EmployeeListViewModel(IEmployeeDataRepository employeeDataRepository, IEmployeeUIService employeeUIService, IEmployeeManagerState employeeManagerState)
        {
            _employeeDataRepository = employeeDataRepository;
            _employeeUIService = employeeUIService;
            _employeeManagerState = employeeManagerState;
            InitializeCommands();
            LoadEmployees();
        }

        #region "Commands"
        
        private void  InitializeCommands()
        {
            AddCommand = new RelayCommand(() => {
                _employeeManagerState.EmployeeCreated += _employeeManagerState_EmployeeCreated;
                _employeeUIService.ActivateAdddNewEmployeeWindow();
            }, () => { return true; });


            EditCommand = new RelayCommand(() => {
                _employeeManagerState.EmployeeUpdated += _employeeManagerState_EmployeeUpdated;
                _employeeUIService.ActivateEditEmployeeWindow(SelectedEmployee);
            }, () => { return true; });


            DeleteCommand = new RelayCommand(() => {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation",MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    _employeeManagerState.EmployeeDeleted += _employeeManagerState_EmployeeDeleted;
                    _employeeDataRepository.Delete(SelectedEmployee);
                    _employeeManagerState.InvokeEmployeeDeletedEvent(SelectedEmployee);
                }                
            }, () => { return true; });


            RefreshCommand = new RelayCommand(() => {
                SearchData = "";
                LoadEmployees();

            }, () => { return true; });


            SearchCommand = new RelayCommand(() => {
                SearchEmployees();
            }, () => { return true; });


            MoveFirstCommand = new RelayCommand(() => {
                if (string.IsNullOrWhiteSpace(_searchText))
                    LoadEmployees(1);
                else
                    SearchEmployees(1);
            }, () => { return true; });


            MoveLastCommand = new RelayCommand(() => {
                if (string.IsNullOrWhiteSpace(_searchText))
                    LoadEmployees(_paginationDetails.pages);
                else
                    SearchEmployees(_paginationDetails.pages);
            }, () => { return true; });


            MoveNextCommand = new RelayCommand(() => {
                long nextPage = (_paginationDetails.page + 1) > _paginationDetails.pages ? _paginationDetails.pages : (_paginationDetails.page + 1);
                if (string.IsNullOrWhiteSpace(_searchText))
                    LoadEmployees(nextPage);
                else
                    SearchEmployees(nextPage);
            }, () => { return true; });


            MovePrevCommand = new RelayCommand(() => {
                long prevPage = (_paginationDetails.page - 1) < 1 ? 1: (_paginationDetails.page - 1);
                if (string.IsNullOrWhiteSpace(_searchText))
                    LoadEmployees(prevPage);
                else
                    SearchEmployees(prevPage);                
            }, () => { return true; });

        }

        #endregion
        #region CommandHandlers
        private void _employeeManagerState_EmployeeCreated(IEmployee newEmployee)
        {
            _employeeManagerState.EmployeeCreated -= _employeeManagerState_EmployeeCreated;
            EmployeeList.Add(newEmployee);
            StatusText = "Added employee sucessfully";
            System.Windows.MessageBox.Show(StatusText,"Add Employee");
        }
        private void _employeeManagerState_EmployeeDeleted(IEmployee deletedEmployee)
        {
            _employeeManagerState.EmployeeDeleted -= _employeeManagerState_EmployeeDeleted;
            var oldEmployeeObj = _employeeList.First((emp) => { return emp.Id == deletedEmployee.Id; });
            _employeeList.Remove(oldEmployeeObj);
            StatusText = "Successfully deleted employee.";
            System.Windows.MessageBox.Show(StatusText,"Delete Employee");
        }
        private void _employeeManagerState_EmployeeUpdated(IEmployee updatedEmployee)
        {
            _employeeManagerState.EmployeeUpdated -= _employeeManagerState_EmployeeUpdated;
            var oldEmployeeObj = _employeeList.First((emp) => { return emp.Id == updatedEmployee.Id; });
            StatusText = "Successfully Updated employee.";
            System.Windows.MessageBox.Show(StatusText,"Update Employee");
        }
        #endregion

        #region  "Private Methods"
        private void LoadEmployees( long  pageNumber=1)
        {
            Task.Run(async () =>
            {
                StatusText = "Fetching data....";
                PagedEmployeeSet pagedEmployeeSet  = await _employeeDataRepository.Get(pageNumber);
                PaginationDetails = pagedEmployeeSet.PageInfo;
                CurrentPageNumber = PaginationDetails.page;
                EmployeeList = new ObservableCollection<IEmployee>(pagedEmployeeSet.Employees);
                StatusText = "Loading completed";
            });
        }
        private void SearchEmployees(long pageNumber = 1)
        {
            Task.Run(async () =>
            {
                StatusText = "Searching data....";
                PagedEmployeeSet pagedEmployeeSet = await _employeeDataRepository.Get(SearchData, pageNumber);
                PaginationDetails = pagedEmployeeSet.PageInfo;
                CurrentPageNumber = PaginationDetails.page;
                EmployeeList = new ObservableCollection<IEmployee>(pagedEmployeeSet.Employees);
                StatusText = "Searching  completed.";
            });
        }
        #endregion
    }
}
