using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManager.Models;

namespace EmployeeManager.Services
{
    internal interface IEmployeeUIService
    {
        void ActivateAdddNewEmployeeWindow();
        void ActivateEditEmployeeWindow(IEmployee employee);
    }
}
