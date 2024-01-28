using CRG.ES.CommandProcessor;
using Employee2.ReadModels;
using Employee2;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Employee2.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ICommandProcessingUnit cpu;
        private readonly IDocumentSession session;


        public EmployeeController(IDocumentSession session, ICommandProcessingUnit cpu) : base()
        {
            this.session = session;
            this.cpu = cpu;

        }
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(string name)
        {
            var emp = session.Query<Employee>().First(x => x.Name == name);
            if (emp == null)
            {
                return Content("<html><body><h1>No Permission</h1></body></html>");
            }
            if (emp.role == Role.Manager)
            {
                return RedirectToAction("List", new { id = emp.Id });
            }
            else
            {
                return RedirectToAction("Profile", new { id = emp.Id });
            }
        }

        //[Authorize(Roles = nameof(Role.Manager))]
        public ActionResult List(Guid? Id)
        {
            var emp = session.Load<Employee>(Id);

            if (emp.role == Role.Manager)
            {
                var emps = session.Query<Employee>().ToList();
                ViewBag.ManagerId = emp.Id;
                return View(emps);
            }
            else
            {
                return Content("<html><body><h1>No Permission</h1></body></html>");
            }
        }

        //[Authorize(Roles = nameof(Role.Manager))]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(NewEmployee m)
        {
            cpu.Process(new NewEmployee
            {
                Id = Guid.NewGuid(),
                Name = m.Name,
                role = m.role,
                workingBranch = m.workingBranch,
            });
            return RedirectToAction("LogIn");
        }
        public ActionResult UpdateName(Guid? Id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateName(UpdateName m)
        {
            cpu.Process(new UpdateName
            {
                Id = m.Id,
                NewName = m.NewName,
            });
            return RedirectToAction("LogIn");
        }
        public ActionResult UpdateRole(Guid? Id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult UpdateRole(UpdateRole m)
        {
            cpu.Process(new UpdateRole
            {
                Id = m.Id,
                NewRole = m.NewRole,
            });
            return RedirectToAction("LogIn");
        }
        public ActionResult Profile(Guid? id)
        {
            var emp = session.Load<Employee>(id);
            return View(emp);
        }

    }
}