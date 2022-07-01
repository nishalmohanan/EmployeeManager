using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManager.Models;

namespace EmployeeManager.Repositories
{
    internal interface IEmployeeDataRepository
    {
        Task<IEmployee> Create(IEmployee  employee);
        Task<PagedEmployeeSet> Get();
        Task<PagedEmployeeSet> Get(long pageNumber );
        Task<PagedEmployeeSet> Get(string strQuery, long pageNumber);
        Task  Update(IEmployee employee);
        Task  Delete(IEmployee  employee);
    }
}
