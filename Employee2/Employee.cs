using MM.ES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee2
{
    public enum Role
    {
        Manager,
        Supervisor,
        Worker,
        Fired,
        Resigned,
        NotLogIn
    }

    public enum Branches
    {
        Melbourne,
        Sydney,
        Queenland
    }
    public class NewEmployee : IMessage
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Branches workingBranch { get; set; }
        public Role role { get; set; }
    }
    public class UpdateName : IMessage
    {
        public Guid Id { get; set; }
        public string NewName { get; set; }
    }
    public class UpdateRole : IMessage
    {
        public Guid Id { get; set; }
        public Role NewRole { get; set; }
    }
    public class ResignEmployee : IMessage
    {
        public Guid Id { get; set; }
    }
    public class FireEmployee : IMessage
    {
        public Guid Id { get; set; }
    }
    //public class LogIn : IMessage
    //{ 
    //    public Guid Id { get; set; }
    //}

}
