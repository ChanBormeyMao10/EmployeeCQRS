using Employee2;
using Employee2.ReadModels;
using Employee2.ReadModels.Handler;
using Employee2.ReadModelTests;
using MM.ES;
using NUnit.Framework;
using System;

namespace Employee2.ReadModelTests
{
    [TestFixture]
    public class EmployeeHandlerTest
    {
        private IReadModelSession session;
        private EmployeeHandler handler;
        private Guid empId;
        private Employee emp;

        [TearDown]
        public void Teardown()
        { }
        [SetUp]
        public void Setup()
        {
            session = Global.ReadModelSession;
            handler = new EmployeeHandler(session);
            empId = Guid.NewGuid();
            emp = new Employee
            {
                Id = empId,
            };
            session.Store(emp);
        }
        [Test]
        public void NewEmployeeTest()
        {
            //Arrange
            var e = new NewEmployee
            {
                Id = Guid.NewGuid(),
                Name = "Emp1",
                role = Role.Manager
            };
            //Action
            handler.Handle(e);
            var exp = session.Load<Employee>(e.Id);

            //Assert
            Assert.AreEqual(e.Id, exp.Id);
            Assert.AreEqual(e.Name, exp.Name);
            Assert.AreEqual(e.role, exp.role);
        }
        [Test]
        public void UpdateNameTest()
        {
            //Arrange
            session.Store(emp);

            var e = new UpdateName()
            {
                Id = empId,
                NewName = "Emp11",

            };

            //Action
            handler.Handle(e);
            var exp = session.Load<Employee>(empId);

            //Assert
            Assert.NotNull(exp);
            Assert.AreEqual(e.NewName, exp.Name);
        }
        [Test]
        public void UpdateRoleTest()
        {
            //Arrange
            session.Store(emp);

            var e = new UpdateRole()
            {
                Id = empId,
                NewRole = Role.Supervisor

            };

            //Action
            handler.Handle(e);
            var exp = session.Load<Employee>(empId);

            //Assert
            Assert.NotNull(exp);
            Assert.AreEqual(e.NewRole, exp.role);
        }

        [Test]
        public void FireEmployee()
        {
            //Arrange
            session.Store(emp);

            var e = new FireEmployee()
            {
                Id = empId,
            };

            //Action
            handler.Handle(e);
            var exp = session.Load<Employee>(empId);

            //Assert
            Assert.NotNull(exp);
            Assert.AreEqual(exp.role, Role.Fired);
        }

        [Test]
        public void ResignEmployee()
        {
            //Arrange
            session.Store(emp);

            var e = new ResignEmployee()
            {
                Id = empId,
            };

            //Action
            handler.Handle(e);
            var exp = session.Load<Employee>(empId);

            //Assert
            Assert.NotNull(exp);
            Assert.AreEqual(exp.role, Role.Resigned);
        }

    }
}
