using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EmployeeService.Client
{
    public  class EmployeeResult:IEmployeeResult
    {
        public int code { get; set; }
        public Meta meta { get; set; }
        public IEnumerable<Employee>  data { get; set; }
    }

    public class EmployeeCreatedResult : IEmployeeResult
    {
        public int code { get; set; }
        public Meta meta { get; set; }
        public Employee data { get; set; }
    }


    public class Meta
    {
        public Pagination pagination { get; set; }
    }
    public class Pagination
    {
        public long total { get; set; }
        public long pages { get; set; }
        public int page { get; set; }
        public int limit { get; set; }
    }
    public class Employee
    {
        [JsonProperty("id")]
        public Int64 Id { get; set; }

        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }
        [JsonProperty("email")]
        [Required]
        public string Email { get; set; }
        [JsonProperty("gender")]
        [Required]
        public string Gender { get; set; }
        [JsonProperty("status")]
        [Required]
        public string Status { get; set; }
    }
}
