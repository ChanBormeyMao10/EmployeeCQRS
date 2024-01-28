using Employee2.CommandHandler;
using Employee2.Domain;
using Employee2;
using MM.ES.Testing;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using MM.ES;

public class EmployeeDomainTest : AggregateTestFixture<Employee>
{
    [TearDown]
    public void Teardown()
    { }
    private EmployeeHandler commandHandler;
    private Guid Id;
    private Guid Id2;
    private List<IMessage> employeeMessages;
    protected override Dictionary<Guid, IEnumerable<IMessage>> Given()
    {
        Id = Guid.NewGuid();
        Id2 = Guid.NewGuid();
        employeeMessages = new List<IMessage>
                {
                    new NewEmployee
                    {
                        Id= Id,
                        Name= "TestName",
                        role= Role.Manager
                    },

                };

        return new Dictionary<Guid, IEnumerable<IMessage>> { { Id, employeeMessages } };
    }
    protected override void SetupCommandHandler(IRepository repository)
    {
        commandHandler = new EmployeeHandler(repository);
    }
    [Test]
    public void NewEmployee()
    {
        var command = new NewEmployee()
        {
            Id = Guid.NewGuid(),
            Name = "Employee1",
            role = Role.Worker
        };
        Then.Add(command.Id, new IMessage[] { command });
        ProcessCommand(commandHandler, command);
    }
    [Test]
    public void UpdateName()
    {
        var command = new UpdateName()
        {
            Id = Id,
            NewName = "Employee11"
        };
        Then.Add(command.Id, new IMessage[] { command });
        ProcessCommand(commandHandler, command);
    }
    [Test]
    public void UpdateSameName()
    {
        var command = new UpdateName()
        {
            Id = Id,
            NewName = "TestName"
        };
        Then.Add(command.Id, new IMessage[] { command });
        ProcessExceptionCommand(commandHandler, command);
    }

    [Test]
    public void UpdateRole()
    {
        var command = new UpdateRole()
        {
            Id = Id,
            NewRole = Role.Supervisor
        };
        Then.Add(command.Id, new IMessage[] { command });
        ProcessCommand(commandHandler, command);
    }
    [Test]
    public void UpdateSameRole()
    {
        var command = new UpdateRole()
        {
            Id = Id,
            NewRole = Role.Manager
        };
        Then.Add(command.Id, new IMessage[] { command });
        ProcessExceptionCommand(commandHandler, command);

    }

    [Test]
    public void NotManagerUpdateRole()
    {

        var command = new UpdateRole()
        {
            Id = Id,
            NewRole = Role.Worker
        };
        Then.Add(command.Id, new IMessage[] { command });
        ProcessExceptionCommand(commandHandler, command);
    }
    [Test]
    public void FireEmployee()
    {
        var command = new FireEmployee()
        {
            Id = Id,
        };
        Then.Add(command.Id, new IMessage[] { command });
        ProcessCommand(commandHandler, command);
    }
    [Test]
    public void ResignEmployee()
    {
        var command = new ResignEmployee()
        {
            Id = Id,
        };
        Then.Add(command.Id, new IMessage[] { command });
        ProcessCommand(commandHandler, command);
    }
}
