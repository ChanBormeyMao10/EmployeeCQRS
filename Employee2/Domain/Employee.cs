using MM.ES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee2.Domain
{
    public class Employee : AggregateBase
    {
        private Role role;
        private string name;
        public Employee()
        {

        }

        public Employee(NewEmployee msg) : this()
        {
            RaiseEvent(msg);
        }
        public void UpdateName(UpdateName msg)
        {
            if (msg.NewName == name)
            {
                throw new DomainAggregateException(this, "New name can't be the smae as previous name");
            }
            RaiseEvent(msg);
        }
        public void UpdateRole(UpdateRole msg)
        {
            if (msg.NewRole == role)
            {
                throw new DomainAggregateException(this, "New role can't be the smae as previous role");
            }
            if (role != Role.Manager)
            {
                throw new DomainAggregateException(this, "Can't change role unless  manager");
            }
            RaiseEvent(msg);
        }
        public void FireEmployee(FireEmployee msg)
        {

            RaiseEvent(msg);
        }
        public void ResignEmployee(ResignEmployee msg)
        {

            RaiseEvent(msg);
        }
        //public void LogIn(LogIn msg)
        //{

        //    RaiseEvent(msg);
        //}
        //-------------------------------------------------------------------------------------------------------
        private void Apply(NewEmployee e)
        {
            role = e.role;
            Id = e.Id;
            name = e.Name;
        }
        private void Apply(UpdateRole e)
        {
            role = e.NewRole;
            Id = e.Id;

        }
        private void Apply(UpdateName e)
        {
            name = e.NewName;
        }

        private void Apply(FireEmployee e)
        {
            role = Role.Fired;
        }
        private void Apply(ResignEmployee e)
        {
            role = Role.Fired;
        }
        //private void Apply(LogIn e)
        //{

        //}
    }
}
