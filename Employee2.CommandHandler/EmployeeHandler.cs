using CRG.ES;
using MM.ES;
using Employee2.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee2.CommandHandler
{
    public class EmployeeHandler : IHandleCommand<NewEmployee>, IHandleCommand<UpdateName>, IHandleCommand<UpdateRole>, IHandleCommand<FireEmployee>, IHandleCommand<ResignEmployee>
    {
        private readonly IRepository repository;
        public EmployeeHandler(IRepository repository)
        {
            this.repository = repository;
        }

        public CommandResult Handle(NewEmployee c)
        {
            var employee = new Employee(c);
            return new CommandResult(repository.Save(employee));
        }
        public CommandResult Handle(UpdateName c)
        {
            var employee = repository.GetById<Employee>(c.Id);
            employee.UpdateName(c);
            return new CommandResult(repository.Save(employee));
        }
        public CommandResult Handle(UpdateRole c)
        {
            var employee = repository.GetById<Employee>(c.Id);
            employee.UpdateRole(c);
            return new CommandResult(repository.Save(employee));
        }
        public CommandResult Handle(FireEmployee c)
        {
            var employee = repository.GetById<Employee>(c.Id);
            employee.FireEmployee(c);
            return new CommandResult(repository.Save(employee));
        }
        public CommandResult Handle(ResignEmployee c)
        {
            var employee = repository.GetById<Employee>(c.Id);
            employee.ResignEmployee(c);
            return new CommandResult(repository.Save(employee));
        }
    }
}
