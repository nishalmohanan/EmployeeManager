using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeManager.Enums;

namespace EmployeeManager.Models
{
    internal interface IEmployee
    {
         Int64 Id { get; set; }
         string Name { get; set; }
         string Email { get; set; }
         Gender Gender { get; set; }
         Status Status { get; set; }
    }
    internal class Employee:IEmployee
    {
        public Int64 Id{ get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Gender  Gender{ get; set; }
        public Status Status { get; set; }
    }
}
