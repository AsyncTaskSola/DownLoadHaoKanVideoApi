using DownLoadHaoKanVideoAPI.Entity;
using DownLoadHaoKanVideoAPI.Interface;

namespace DownLoadHaoKanVideoAPI.Servers
{
    public class EmployeeServers:IEmployeeServers
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeServers(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public void AddEmployee(Employee employee)
        {
            _employeeRepository.AddEmployee(employee);
        }
    }
}
