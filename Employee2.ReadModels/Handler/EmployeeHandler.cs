using MM.ES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee2.ReadModels.Handler
{
    public class EmployeeHandler : IHandleEvent<NewEmployee>, IHandleEvent<UpdateName>, IHandleEvent<UpdateRole>, IHandleEvent<FireEmployee>, IHandleEvent<ResignEmployee>
    {
        private readonly IReadModelSession session;

        public EmployeeHandler(IReadModelSession session)
        {
            this.session = session;
        }
        //public Employee LogInEmployee(string name)
        //{
        //    var emp = session.Load<Employee>(name);
        //    return emp;
        //}
        public void Handle(NewEmployee e)
        {
            var book = new Employee
            {
                Id = e.Id,
                Name = e.Name,
                role = e.role,
                workingBranch = e.workingBranch,
            };
            session.Store(book);
        }
        public void Handle(UpdateName e)
        {
            var emp = session.Load<Employee>(e.Id);
            emp.Name = e.NewName;
        }
        public void Handle(UpdateRole e)
        {
            var emp = session.Load<Employee>(e.Id);
            emp.role = e.NewRole;
        }
        public void Handle(FireEmployee e)
        {
            var emp = session.Load<Employee>(e.Id);
            emp.role = Role.Fired;
        }
        public void Handle(ResignEmployee e)
        {
            var emp = session.Load<Employee>(e.Id);
            emp.role = Role.Resigned;
        }
    }

}
