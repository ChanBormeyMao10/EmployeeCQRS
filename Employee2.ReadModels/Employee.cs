using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee2.ReadModels
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Role role { get; set; }
        public Branches workingBranch { get; set; }
        public Employee() { }
    }
}
