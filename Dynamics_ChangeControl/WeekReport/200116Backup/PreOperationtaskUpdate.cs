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
                                Entity task = service.Retrieve("task", target.Id, new ColumnSet("new_l_weekly_report_detail"));

                                Entity report_detail = new Entity("new_l_weekly_report_detail");

                                if (task.Contains("new_l_weekly_report")) {
                                    report_detail.Id = ((EntityReference)task["new_l_weekly_report"]).Id;
                                }

                                Entity report = service.Retrieve("new_weekly_report", ((EntityReference)report_detail["new_l_weekly_report"]).Id, new ColumnSet("new_dt_standard"));

                                if (!report_detail.Contains("new_l_weekly_report"))
                                {
                                    throw new InvalidPluginExecutionException("�ش� ���� Detail�� �ְ� ���� Master�� ������ ���� �ʾҽ��ϴ�. ");
                                }


                                DateTime start = new DateTime();
                                DateTime end = new DateTime();
                                int timeDiff = -1;


                                if (task.Contains("scheduledstart")) {
                                    start = (DateTime)task["scheduledstart"];
                                }
                                if (task.Contains("scheduledend")) {
                                    end = (DateTime) task["scheduledend"];
                                }


                                if (!start.Equals(end)) {
                                    throw new InvalidPluginExecutionException("������¥�� �� ���ڰ� �ٸ��ϴ�.");
                                }

                                string[] expectName = { "new_d_input_expected_monday", "new_d_input_expected_tuesday", "new_d_input_expected_wednesday", "new_d_input_expected_thursday", "new_d_input_expected_friday" };
                                string[] actualName = { "new_d_input_real_monday", "new_d_input_real_tuesday", "new_d_input_real_wednesday", "new_d_input_real_thursday", "new_d_input_real_friday" };


                                //���س�¥ �̿� ���� �������� ����.
                                if (report.Contains("new_dt_standard")) {
                                    //���� ���� int ��ȯ �Ǵ��� Ȯ��
                                    timeDiff = (start - ((DateTime)report["new_dt_standard"])).Days;
                                }

                                else {
                                    throw new InvalidPluginExecutionException("�ְ� ������ ������¥�� �����ϴ�.");
                                }


                                //target �̿� report_detail ����

                                if (target.Contains("subject"))
                                {
                                    report_detail["new_name"] = target["subject"];
                                }


                                if (target.Contains("description"))
                                {
                                    report_detail["new_txt_subject"] = target["description"];
                                    
                                }

                                if (target.Contains("new_l_weekly_report"))
                                {
                                    throw new InvalidPluginExecutionException("Ȱ������ �ְ����������� ���� �� �� �����ϴ�.");
                                }

                                if (target.Contains("new_l_weekly_report_detail"))
                                {
                                    throw new InvalidPluginExecutionException("Ȱ������ �ְ����� ���� �󼼸� ���� �� �� �����ϴ�.");


                                }



                                if (target.Contains("expectminutes")) {
                                    report_detail[expectName[timeDiff]] = target["expectminutes"]; 
                                }

                                if (target.Contains("actualdurationminutes")) {
                                    report_detail[actualName[timeDiff]] = target["actualdurationminutes"];
                                }

                               

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