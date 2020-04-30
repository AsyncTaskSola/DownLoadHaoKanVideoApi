using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DownLoadHaoKanVideoAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace DownLoadHaoKanVideoAPI.Dbdata
{
    public class SampleDBContext:DbContext
    {
        public SampleDBContext(DbContextOptions<SampleDBContext> options):base(options)
        {

        }
        public DbSet<Employee> Emplyees { get; set; }
    }
}
