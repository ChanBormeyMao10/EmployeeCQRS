using CRG.ES;
using Raven.Client;


namespace Employee2.ReadModels.RavenConfig
{
    public class SetUpIndex : IRequireStartup
    {
        private readonly IDocumentStore store;
        public SetUpIndex(IDocumentStore store)
        {
            this.store = store;
        }
        public void Start()
        {
            new EmployeeEvent_SetupIndex().Execute(store);
        }
    }
}
