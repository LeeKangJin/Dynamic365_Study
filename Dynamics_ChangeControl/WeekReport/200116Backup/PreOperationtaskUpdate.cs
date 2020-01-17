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

                                if (!report_detail.Contain("new_l_weekly_report"))
                                {
                                    throw new InvalidPluginExecutionException("해당 업무 Detail이 주간 업무 Master와 연결이 되지 않았습니다. ");
                                }

                                Entity report = service.Retrieve("new_weekly_report", ((EntityReference)report_detail["new_l_weekly_report"]).Id, new ColumnSet("new_dt_standard"));

                                

                                Entity report_detail = new Entity("new_l_weekly_report_detail");
                                DateTime start = new DateTime();
                                DateTime end = new DateTime();
                                int timeDiff = -1;


                                if (task.Contain("scheduledstart")) {
                                    start = task["scheduledstart"];
                                }
                                if (task.Contain("scheduledend")) {
                                    end = task["scheduledend"];
                                }


                                if (!start.Equals(end)) {
                                    throw new InvalidPluginExecutionException("시작일짜와 끝 일자가 다릅니다.");
                                }

                                string[] expectName = { "new_d_input_expected_monday", "new_d_input_expected_tuesday", "new_d_input_expected_wednesday", "new_d_input_expected_thursday", "new_d_input_expected_friday" };
                                string[] actualName = { "new_d_input_real_monday", "new_d_input_real_tuesday", "new_d_input_real_wednesday", "new_d_input_real_thursday", "new_d_input_real_friday" };


                                //기준날짜 이용 무슨 요일인지 구함.
                                if (report.Contain("new_dt_standard")) {
                                    //요일 빼서 int 반환 되는지 확인
                                    timeDiff = (start - ((DateTime)report["new_dt_standard"])).Days;
                                }

                                else {
                                    throw new InvalidPluginExecutionException("주간 보고의 기준일짜가 없습니다.");
                                }


                                //target 이용 report_detail 세팅

                                if (target.Contain("subject"))
                                {
                                    report_detail["new_name"] = target["subject"];
                                }


                                if (target.Contain("description"))
                                {
                                    report_detail["new_txt_subject"] = target["description"];
                                    
                                }

                                if (target.Contain("new_l_weekly_report"))
                                {
                                    throw new InvalidPluginExecutionException("활동에서 주간업무보고를 변경 할 수 없습니다.");
                                }

                                if (target.Contain("new_l_weekly_report_detail"))
                                {
                                    throw new InvalidPluginExecutionException("활동에서 주간업무 보고 상세를 변경 할 수 없습니다.");


                                }



                                if (target.Contain("expectminutes")) {
                                    report_detail[expectName[timeDiff]] = target["expectminutes"]; 
                                }

                                if (target.Contain("actualdurationminutes")) {
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