using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownLoadHaoKanVideoAPI.Entity
{
    public class Employee
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public sbyte? Gender { get; set; }//1.男  2.女
        public string Password { get; set; }
        public sbyte? Status { get; set; }
    }
}
