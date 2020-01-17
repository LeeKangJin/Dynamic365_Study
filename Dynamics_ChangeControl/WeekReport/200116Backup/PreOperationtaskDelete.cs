using System;
using System.ServiceModel;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CellCrmVSSolution1.CellCRMPlugin
{ 
    public class PreOperationtaskDelete: PluginBase
    {
        private static readonly object SyncLock = new object();

        public PreOperationtaskDelete(string unsecure, string secure)
            : base(typeof(PreOperationtaskDelete))
        {
          
        }

        protected override void ExecuteCrmPlugin(LocalPluginContext localContext)
        {
            if (localContext == null)
            {
                throw new InvalidPluginExecutionException("localContext");
            }

            try
            {
                IPluginExecutionContext context = localContext.PluginExecutionContext;
                IOrganizationService service = localContext.OrganizationService;

                if (context.IsInTransaction)
                {
                    lock (SyncLock)
                    {
                        if (context.InputParameters["Target"] is Entity)
                        {
                            Entity retrieve_context = service.Retrieve("new_projectdetail", context.PrimaryEntityId, new ColumnSet(true));

                            Guid retrieve_context_project_id = retrieve_context.GetAttributeValue<EntityReference>("new_l_project").Id;

                            Entity new_project = new Entity("new_weekly_report", ((EntityReference)retrieve_context["new_l_project"]).Id);



                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new InvalidPluginExecutionException(ex.Message);
            }
        }
    }
}
