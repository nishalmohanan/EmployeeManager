using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;


namespace EmployeeService.Client
{
    /// <summary>
    ///  Client class  that handles wraps all API calls.
    /// </summary>
    public class EmployeeServiceClient: IEmployeeServiceClient
    {
        private readonly  string _baseUrl;
        private readonly string  _authToken;
        private const string USER_RESOURCE_URL = "public-api/users";
        public EmployeeServiceClient(string baseUrl,string authToken)
        {
            _baseUrl = baseUrl;
            _authToken = authToken;            
        }

        public  async Task<IEmployeeResult> Create(Employee  employee)
        {
            IEmployeeResult? employeeResult;
            if (employee== null)
                    throw new ArgumentNullException(nameof(employee));
            if(!(new EmployeeValidator(employee).IsValid())){
                throw new ArgumentException("Invalid payload");
            }
            string payload = JsonConvert.SerializeObject(employee);
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                AddDefaultHeaders(client);
                HttpResponseMessage responseMessage = await client.PostAsync (USER_RESOURCE_URL, new StringContent(payload, System.Text.Encoding.UTF8, "application/json"));
                responseMessage.EnsureSuccessStatusCode();
                string responseContent = await responseMessage.Content.ReadAsStringAsync();
                employeeResult = JsonConvert.DeserializeObject<EmployeeCreatedResult>(responseContent);
            }
            return employeeResult;
        }
        public async Task<IEmployeeResult> Get()
        {
            return (IEmployeeResult?)await Get(1);
        }
        public async  Task<IEmployeeResult> Get(long pageNumber)
        {
            IEmployeeResult? employeeResult;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                string resourceUrl = $"{USER_RESOURCE_URL}?page={pageNumber}";
                AddDefaultHeaders(client);
                HttpResponseMessage responseMessage = await client.GetAsync(resourceUrl);
                responseMessage.EnsureSuccessStatusCode();
                string responseContent = await responseMessage.Content.ReadAsStringAsync();
                employeeResult = JsonConvert.DeserializeObject<EmployeeResult>(responseContent);
            }
            return employeeResult;
        }
        public async Task<IEmployeeResult> Delete(long Id)
        {
            IEmployeeResult? employeeResult;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                AddDefaultHeaders(client);
                HttpResponseMessage responseMessage = await client.DeleteAsync( $"{USER_RESOURCE_URL}/{Id}");
                responseMessage.EnsureSuccessStatusCode();
                string responseContent = await responseMessage.Content.ReadAsStringAsync();
                employeeResult = JsonConvert.DeserializeObject<EmployeeResult>(responseContent);
            }
            return employeeResult;
        }
        public async  Task<IEmployeeResult> Update(Employee employee)
        {
            IEmployeeResult? employeeResult;
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));
            if (!(new EmployeeValidator(employee).IsValid()))
            {
                throw new ArgumentException("Invalid payload");
            }
            string payload = JsonConvert.SerializeObject(employee);
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                AddDefaultHeaders(client);
                string resourceUrl = $"{USER_RESOURCE_URL}/{employee. Id}";
                HttpResponseMessage responseMessage = await client.PutAsync(resourceUrl, new StringContent(payload, System.Text.Encoding.UTF8, "application/json"));
                responseMessage.EnsureSuccessStatusCode();
                string responseContent = await responseMessage.Content.ReadAsStringAsync();
                employeeResult = JsonConvert.DeserializeObject<EmployeeCreatedResult>(responseContent);
            }
            return employeeResult;
        }

        public  async Task<IEmployeeResult> Query(string criteria, long pageNumber)
        {
            IEmployeeResult? employeeResult;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);
                AddDefaultHeaders(client);
                string resourceUrl = $"{USER_RESOURCE_URL}?name={criteria}&page={pageNumber}";
                HttpResponseMessage responseMessage = await client.GetAsync(resourceUrl);
                responseMessage.EnsureSuccessStatusCode();
                string responseContent = await responseMessage.Content.ReadAsStringAsync();
                employeeResult = JsonConvert.DeserializeObject<EmployeeResult>(responseContent);
            }
            return employeeResult;
        }
        private void AddDefaultHeaders(HttpClient client)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _authToken);
        }

        
    }
}