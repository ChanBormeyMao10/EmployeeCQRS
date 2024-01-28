using System;
using System.Linq;
using Raven.Abstractions.Indexing;
using Raven.Client;
using Raven.Client.Indexes;


namespace Employee2.ReadModels.RavenConfig
{
    public class EmployeeEvent_SetupIndex : AbstractIndexCreationTask<Employee2.ReadModels.Employee, EmployeeEvent_SetupIndex.IndexEntry>
    {
        public class IndexEntry
        {
            public Guid Id;
            public string name;
            public Role role;
        }
        public EmployeeEvent_SetupIndex()
        {
            Map = emps => from e in emps
                          select new IndexEntry()
                          {
                              Id = e.Id,
                              name = e.Name,
                              role = e.role

                          };
            Index(x => x.Id, FieldIndexing.Analyzed);
        }
    }
}
