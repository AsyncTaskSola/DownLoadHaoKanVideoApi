using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DownLoadHaoKanVideoAPI.Entity;

namespace DownLoadHaoKanVideoAPI.Interface
{
    public interface IEmployeeRepository
    {
        void AddEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
