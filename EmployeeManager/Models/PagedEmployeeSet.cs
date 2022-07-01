using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Models
{
    internal class PagedEmployeeSet
    {
        public IEnumerable<IEmployee> Employees { get; set; }
        public Pagination PageInfo { get; set; }
    }

    internal class Pagination
    {
        public long total { get; set; }
        public long pages { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
    }
}
