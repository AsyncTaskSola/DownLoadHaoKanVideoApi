using System;
using System.Linq;
using System.Threading.Tasks;
using DownLoadHaoKanVideoAPI.Dbdata;
using DownLoadHaoKanVideoAPI.Entity;
using DownLoadHaoKanVideoAPI.Interface;
using Microsoft.EntityFrameworkCore;

namespace DownLoadHaoKanVideoAPI.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SampleDBContext _sampleDb;

        public EmployeeRepository(SampleDBContext sampleDb)
        {
            _sampleDb = sampleDb ?? throw new ArgumentException(nameof(sampleDb));
        }
        public void AddEmployee(Employee employee)
        {
            var number = 0;
            if (employee == null) throw new ArgumentNullException(nameof(employee));
            try
            {
                 number = _sampleDb.Emplyees.Max(x => x.id);
            }
            catch (Exception e)
            {
                number= number;
            }
            
            employee.id = number + 1;
            _sampleDb.Emplyees.Add(employee);
            //_sampleDb.Emplyees
            _sampleDb.Entry(employee).State = EntityState.Added;
            _sampleDb.SaveChanges();
        }

        public void UpdateEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
