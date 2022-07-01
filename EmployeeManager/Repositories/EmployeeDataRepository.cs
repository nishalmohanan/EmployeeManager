using EmployeeManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeService.Client;


namespace EmployeeManager.Repositories
{
    /// <summary>
    ///    Respository built above EmployeeServiceClient library. All services to access/ modify service data are added here.
    /// </summary>
    internal class EmployeeDataRepository : IEmployeeDataRepository
    {
        private List<IEmployee> _employees;
        private IEmployeeServiceClient _employeeServiceClient;
        public EmployeeDataRepository(IEmployeeServiceClient employeeServiceClient)
        {
            _employeeServiceClient = employeeServiceClient;            
        }
        /// <summary>
        ///  Creates a new employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async  Task<IEmployee> Create(IEmployee employee)
        {
            EmployeeCreatedResult  employeeResult = (EmployeeCreatedResult)await _employeeServiceClient.Create(new EmployeeService.Client.Employee()
            {
                Email = employee.Email,
                Name = employee.Name,
                Gender = employee.Gender.ToString(),
                Status= employee.Status.ToString()
            });
            if (employeeResult != null && employeeResult.code ==201)
            {
                employee.Id = employeeResult.data.Id ;
            }
            return employee;
        }
        
        /// <summary>
        /// Deletes employee  
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task  Delete(IEmployee employee)
        {
            await _employeeServiceClient.Delete(employee.Id);
        }
        public async Task<PagedEmployeeSet> Get()
        {
            return await Get(1);
        }
        /// <summary>
        ///  Loads the employees from given page .
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
         public async Task<PagedEmployeeSet> Get(long pageNumber)
        {
            IEnumerable<IEmployee> employees = new List<IEmployee>();
            Models.Pagination paginationDetails = new  Models.Pagination();
            EmployeeResult employeeResult = (EmployeeResult)await _employeeServiceClient.Get(pageNumber);
            if(employeeResult != null)
            {
                employees = employeeResult.data.Select((e) => new Models.Employee()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Gender = (Enums.Gender)Enum.Parse(typeof(Enums.Gender), e.Gender,true),
                    Status = (Enums.Status)Enum.Parse(typeof(Enums.Status), e.Status,true)
                });
                paginationDetails = new  Models.Pagination() { total =  employeeResult.meta.pagination.total, pages =employeeResult.meta.pagination.pages, limit = employeeResult.meta.pagination.limit, page= employeeResult.meta.pagination.page };
            }
            return (new PagedEmployeeSet() {Employees = employees,PageInfo= paginationDetails  });
        }

        /// <summary>
        ///  Retrives employees with given/similar name.
        /// </summary>
        /// <param name="strQuery"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public async Task<PagedEmployeeSet> Get(string strQuery, long pageNumber)
        {
            IEnumerable<IEmployee> employees = new List<IEmployee>();
            Models.Pagination paginationDetails = new Models.Pagination();
            EmployeeResult employeeResult = (EmployeeResult)await _employeeServiceClient.Query(strQuery, pageNumber);
            if (employeeResult != null)
            {
                employees = employeeResult.data.Select((e) => new Models.Employee()
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Gender = (Enums.Gender)Enum.Parse(typeof(Enums.Gender), e.Gender, true),
                    Status = (Enums.Status)Enum.Parse(typeof(Enums.Status), e.Status, true)
                });
                paginationDetails = new Models.Pagination() { total = employeeResult.meta.pagination.total, pages = employeeResult.meta.pagination.pages, limit = employeeResult.meta.pagination.limit, page = employeeResult.meta.pagination.page };
            }
            return (new PagedEmployeeSet() { Employees = employees, PageInfo = paginationDetails });
        }

        /// <summary>
        ///  Update the details of given employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async  Task  Update(IEmployee employee)
        {
            await _employeeServiceClient.Update(new EmployeeService.Client.Employee()
            {
                Id=employee.Id,
                Email = employee.Email,
                Name = employee.Name,
                Gender = employee.Gender.ToString(),
                Status = employee.Status.ToString()
            });
        }
    }
}
