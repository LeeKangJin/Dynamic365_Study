using System;
using System.ServiceModel;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CellCrmVSSolution1.CellCRMPlugin
{
    public class PreOperationtaskUpdate: PluginBase
    {
        private static readonly object SyncLock = new object();

        public PreOperationtaskUpdate(string unsecure, string secure)
            : base(typeof(PreOperationtaskUpdate))
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
                        if (context.Depth < 2)
                        {
                            if (context.InputParameters["Target"] is Entity)
                            {
                                Entity target = (Entity)context.InputParameters["Target"];
                                Entity retrieveTarget = service.Retrieve("task", target.Id, new ColumnSet("new_l_weekly_report_detail"));
                                Entity report = service.Retrieve("new_l_weekly_report",, new ColumnSet("new_l_weekly_report"));

                                Entity report_detail = new Entity("new_l_weekly_report_detail");

                          


                                service.Update(report_detail);
                            }
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