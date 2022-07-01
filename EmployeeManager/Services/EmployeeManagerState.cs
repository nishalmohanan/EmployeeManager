using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManager.Models;

namespace EmployeeManager.Services
{
    internal interface IEmployeeManagerState
    {
        event Action<IEmployee> EmployeeCreated;
        event Action<IEmployee> EmployeeUpdated;
        event Action<IEmployee> EmployeeDeleted;

        void InvokeEmployeeCreatedEvent(IEmployee employee);
        void InvokeEmployeeDeletedEvent(IEmployee employee);
        void InvokeEmployeeUpdatedEvent(IEmployee employee);
    }
    /// <summary>
    ///  Defines events that helps  to prevent dependency of other Views/ ViewModels in  the MainWindow ViewModel. 
    ///  The MainWindow ViewModel uses the below events  to handle Add,Edit and Delete of employee.
    /// </summary>
    internal class EmployeeManagerState : IEmployeeManagerState
    {
        object objectLock = new Object();
        private event Action<IEmployee> _employeeCreated;
        private event Action<IEmployee> _employeeUpdated;
        private event Action<IEmployee> _employeeDeleted;
        public event Action<IEmployee> EmployeeCreated
        {
            add
            {
                lock (objectLock)
                {
                    _employeeCreated += value;
                }
                
            }
            remove {
                lock (objectLock)
                {
                    _employeeCreated -= value;
                }
            }
        }
        public event Action<IEmployee> EmployeeUpdated
        {
            add
            {
                lock (objectLock)
                {
                    _employeeUpdated += value;
                }

            }
            remove
            {
                lock (objectLock)
                {
                    _employeeUpdated -= value;
                }
            }
        }
        public event Action<IEmployee> EmployeeDeleted
        {
            add
            {
                lock (objectLock)
                {
                    _employeeDeleted += value;
                }
            }
            remove
            {
                lock (objectLock)
                {
                    _employeeDeleted -= value;
                }
            }
        }

        public void InvokeEmployeeCreatedEvent(IEmployee employee)
        {
            _employeeCreated?.Invoke(employee);
        }
        public void InvokeEmployeeUpdatedEvent(IEmployee employee)
        {
            _employeeUpdated?.Invoke(employee);
        }
        public void InvokeEmployeeDeletedEvent(IEmployee employee)
        {
            _employeeDeleted?.Invoke(employee);
        }
    }
}
