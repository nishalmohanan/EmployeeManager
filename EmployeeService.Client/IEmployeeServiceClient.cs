using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Client
{
    public interface IEmployeeServiceClient
    {

        Task<IEmployeeResult> Get();
        Task<IEmployeeResult> Get(long pageNumber);
        Task<IEmployeeResult> Create(Employee employee);
        Task<IEmployeeResult> Update(Employee employee);
        Task<IEmployeeResult> Delete(long Id);
        Task<IEmployeeResult> Query(string criteria, long pageNumber);
    }
}
