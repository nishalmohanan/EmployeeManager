using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Models.Options
{
    internal class ServiceOptions
    {
        public string Service = "Service";
        public string BaseUrl { get; set; }
        public string Token { get; set; }
    }
}
