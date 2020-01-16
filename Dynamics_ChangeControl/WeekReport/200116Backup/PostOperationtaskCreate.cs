using System;
using System.ServiceModel;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace CellCrmVSSolution1.CellCRMPlugin
{
    public class PostOperationtaskCreate : PluginBase
    {
        private static readonly object SyncLock = new object();

        public PostOperationtaskCreate(string unsecure, string secure)
            : base(typeof(PostOperationtaskCreate))
        {
        }

        protected override void ExecuteCrmPlugin(LocalPluginContext localContext)
        {
            if (localContext == null)
            {
                throw new InvalidPluginExecutionException("localContext");
            }

            //try
            //{
            //    IPluginExecutionContext context = localContext.PluginExecutionContext;
            //    IOrganizationService service = localContext.OrganizationService;

            //    if (context.IsInTransaction)
            //    {
            //        lock (SyncLock)
            //        {
            //            if (context.InputParameters["Target"] is Entity)
            //            {
                       
            //                Entity target = (Entity)context.InputParameters["Target"];
            //                Entity retrieve_target = service.Retrieve("task", target.Id, new ColumnSet(true)); // target을 retrieve함


            //              //  throw new InvalidPluginExecutionException(target["subject"].ToString());

            //                //throw new InvalidPluginExecutionException(target["scheduledstart"].ToString() + target["scheduledend"].ToString());

            //                DateTime startDate = Convert.ToDateTime(retrieve_target["scheduledstart"]); // 시작 날짜
            //                DateTime endDate = Convert.ToDateTime(retrieve_target["scheduledend"]); // 종료 날짜
            //                TimeSpan dateDiff = endDate - startDate; // 차이

            //                //throw new InvalidPluginExecutionException(retrieve_target["scheduledstart"].ToString());
            //                int week = Enum.GetValues(typeof(DayOfWeek)).Length;

            //                int startDayOffset = (int)startDate.AddDays(-(startDate.Day - 1)).DayOfWeek;
            //                int startWeekCnt = (startDate.Day + startDayOffset) / week;

            //                startWeekCnt += ((startDate.Day + startDayOffset) % week) > 0 ? 1 : 0; // 시작 날짜의 주차

            //                int endDayOffset = (int)endDate.AddDays(-(endDate.Day - 1)).DayOfWeek;
            //                int endWeekCnt = (endDate.Day + endDayOffset) / week;

            //                endWeekCnt += ((endDate.Day + endDayOffset) % week) > 0 ? 1 : 0; // 종료 날짜의 주차

            //                int startDayCnt = Convert.ToInt32(startDate.DayOfWeek); // 시작 날짜의 요일(숫자)
            //                int endDayCnt = Convert.ToInt32(endDate.DayOfWeek); // 종료 날짜의 요일(숫자)

            //                //decimal[] startArr = new decimal[5] { 0, 0, 0, 0, 0 };
            //                //decimal[] endArr = new decimal[5] { 0, 0, 0, 0, 0 };

            //                //for (int i = startDayCnt - 1; i < 5; i++)
            //                //{
            //                //    startArr[i] = Convert.ToDecimal(target["actualdurationminutes"]) / (dateDiff.Days + 1);
            //                //}

            //                //for (int i = 0; i < endDayCnt; i++)
            //                //{
            //                //    endArr[i] = Convert.ToDecimal(target["actualdurationminutes"]) / (dateDiff.Days + 1);
            //                //}


            //                //throw new InvalidPluginExecutionException(startDayCnt + "_" + endDayCnt + "_" + startWeekCnt + "_" + endWeekCnt + "_" + startDate.ToString() + "_" + endDate.ToString());

            //                decimal[] workHourArr = new decimal[5] { 0, 0, 0, 0, 0 }; // 1주일 간의 근무시간

            //                for (int i = startDayCnt - 1; i < endDayCnt; i++)
            //                {
            //                    workHourArr[i] = Convert.ToDecimal(target["actualdurationminutes"]) / (dateDiff.Days + 1);
            //                }
                            
            //                //추후 task를 i개 만큼 분할하여서 Create.
            //                for (int i = startWeekCnt; i <= endWeekCnt; i++) // 주차별로 돈다
            //                {
            //                    QueryExpression qe = new QueryExpression("new_weekly_report");
            //                    qe.ColumnSet.AddColumns("new_p_year", "new_p_month", "new_p_week", "new_txt_subject");
            //                    qe.Criteria.AddFilter(new FilterExpression());
            //                    qe.Criteria.Filters[0].AddCondition("new_p_year", ConditionOperator.Equal, 100000000 + startDate.Year); // 년도 비교
            //                    qe.Criteria.Filters[0].AddCondition("new_p_month", ConditionOperator.Equal, startDate.Month); // 월 비교
            //                    qe.Criteria.Filters[0].AddCondition("new_p_week", ConditionOperator.Equal, 100000000 + i - 1); // 주차 비교
            //                    qe.Criteria.Filters[0].FilterOperator = LogicalOperator.And;

            //                    EntityCollection ec = service.RetrieveMultiple(qe);

            //                    Guid weeklyReportId = new Guid();

                                

            //                    foreach (var j in ec.Entities)
            //                    {
            //                        weeklyReportId = j.Id;
            //                    }

            //                    if (ec.Entities.Count == 0) // 주간업무보고가 없으면
            //                    {
            //                        Entity newWeeklyReport = new Entity("new_weekly_report"); // 새로운 주간업무보고 생성
            //                        newWeeklyReport["new_p_year"] = new OptionSetValue(100000000 + startDate.Year);// 년 대입
            //                        newWeeklyReport["new_p_month"] = new OptionSetValue(startDate.Month); // 월 대입
            //                        newWeeklyReport["new_p_week"] = new OptionSetValue(100000000 + i - 1); // 주차 대입
                                    
            //                        Entity newWeeklyReportDetail = new Entity("new_weekly_report_detail"); // 새로운 주간업무 상세 생성
                                    
            //                        newWeeklyReportDetail["new_l_weekly_report"] = new EntityReference("new_weekly_report", service.Create(newWeeklyReport)); // 관련 주간업무보고 대입
            //                        newWeeklyReportDetail["new_l_task"] = new EntityReference("task", target.Id); // 관련 작업 대입
            //                        newWeeklyReportDetail["new_txt_subject"] = target["subject"]; // 제목 대입
            //                        newWeeklyReportDetail["new_d_input_expected_monday"] = workHourArr[0]; // 월요일 예상시간 대입
            //                        newWeeklyReportDetail["new_d_input_expected_tuesday"] = workHourArr[1]; // 화요일 예상시간 대입
            //                        newWeeklyReportDetail["new_d_input_expected_wednesday"] = workHourArr[2]; // 수요일 예상시간 대입
            //                        newWeeklyReportDetail["new_d_input_expected_thursday"] = workHourArr[3]; // 목요일 예상시간 대입
            //                        newWeeklyReportDetail["new_d_input_expected_friday"] = workHourArr[4]; // 금요일 예상시간 대입
            //                        newWeeklyReportDetail["new_i_year"] = startDate.Year; // 년 대입
            //                        newWeeklyReportDetail["new_i_month"] = startDate.Month; // 월 대입
            //                        newWeeklyReportDetail["new_i_week"] = i; // 주차 대입

            //                        //if (i == startWeekCnt)
            //                        //{
            //                        //    newWeeklyReportDetail["new_d_input_expected_monday"] = startArr[0];
            //                        //    newWeeklyReportDetail["new_d_input_expected_tuesday"] = startArr[1];
            //                        //    newWeeklyReportDetail["new_d_input_expected_wednesday"] = startArr[2];
            //                        //    newWeeklyReportDetail["new_d_input_expected_thursday"] = startArr[3];
            //                        //    newWeeklyReportDetail["new_d_input_expected_friday"] = startArr[4];
            //                        //}
            //                        //else if (i > startWeekCnt && i < endWeekCnt)
            //                        //{
            //                        //    newWeeklyReportDetail["new_d_input_expected_monday"] = Convert.ToDecimal(target["actualdurationminutes"]) / (dateDiff.Days + 1);
            //                        //    newWeeklyReportDetail["new_d_input_expected_tuesday"] = Convert.ToDecimal(target["actualdurationminutes"]) / (dateDiff.Days + 1);
            //                        //    newWeeklyReportDetail["new_d_input_expected_wednesday"] = Convert.ToDecimal(target["actualdurationminutes"]) / (dateDiff.Days + 1);
            //                        //    newWeeklyReportDetail["new_d_input_expected_thursday"] = Convert.ToDecimal(target["actualdurationminutes"]) / (dateDiff.Days + 1);
            //                        //    newWeeklyReportDetail["new_d_input_expected_friday"] = Convert.ToDecimal(target["actualdurationminutes"]) / (dateDiff.Days + 1);
            //                        //}
            //                        //else
            //                        //{
            //                        //    newWeeklyReportDetail["new_d_input_expected_monday"] = endArr[0];
            //                        //    newWeeklyReportDetail["new_d_input_expected_tuesday"] = endArr[1];
            //                        //    newWeeklyReportDetail["new_d_input_expected_wednesday"] = endArr[2];
            //                        //    newWeeklyReportDetail["new_d_input_expected_thursday"] = endArr[3];
            //                        //    newWeeklyReportDetail["new_d_input_expected_friday"] = endArr[4];
            //                        //}

            //                        service.Create(newWeeklyReportDetail);
            //                    }
            //                    else // 주간업무보고가 있으면
            //                    {
                                  
            //                        // 예외 가능성 하루에 두개
            //                        QueryExpression qe2 = new QueryExpression("new_weekly_report_detail");
            //                        qe2.ColumnSet.AddColumns("new_i_year", "new_i_month", "new_i_week", "new_txt_subject", "new_d_input_expected_monday", "new_d_input_expected_tuesday", "new_d_input_expected_wednesday", "new_d_input_expected_thursday", "new_d_input_expected_friday");
            //                        qe2.Criteria.AddFilter(new FilterExpression());
            //                        //qe2.Criteria.Filters[0].AddCondition("new_i_year", ConditionOperator.Equal, startDate.Year); // 년도 비교
            //                        //qe2.Criteria.Filters[0].AddCondition("new_i_month", ConditionOperator.Equal, startDate.Month); // 월 비교
            //                        //qe2.Criteria.Filters[0].AddCondition("new_i_week", ConditionOperator.Equal, i); // 주차 비교
            //                        //qe2.Criteria.Filters[0].AddCondition("new_txt_subject", ConditionOperator.Equal, target["subject"].ToString()); // task의 제목과 detail의 상세 내용 비교
            //                        qe2.Criteria.Filters[0].AddCondition("new_l_task", ConditionOperator.Equal, target.Id); // task의 제목과 detail의 상세 내용 비교
            //                        //qe2.Criteria.Filters[0].AddCondition("new_d_input_expected_monday", ConditionOperator.Equal, workHourArr[0]); // 
            //                        //qe2.Criteria.Filters[0].AddCondition("new_d_input_expected_tuesday", ConditionOperator.Equal, workHourArr[1]); // 
            //                        //qe2.Criteria.Filters[0].AddCondition("new_d_input_expected_wednesday", ConditionOperator.Equal, workHourArr[2]); // 
            //                        //qe2.Criteria.Filters[0].AddCondition("new_d_input_expected_thursday", ConditionOperator.Equal, workHourArr[3]); // 
            //                        //qe2.Criteria.Filters[0].AddCondition("new_d_input_expected_friday", ConditionOperator.Equal, workHourArr[4]); // 
            //                        qe2.Criteria.Filters[0].FilterOperator = LogicalOperator.And;

                                   


            //                        EntityCollection ec2 = service.RetrieveMultiple(qe2);

            //                        Guid weeklyReportId2 = new Guid();

            //                        foreach (var j in ec2.Entities)
            //                        {
            //                            weeklyReportId2 = j.Id;
            //                        }

            //                        throw new InvalidPluginExecutionException(ec2.Entities.Count.ToString());

            //                        if (ec2.Entities.Count == 0) // 주간업무 상세가 없으면
            //                        {
            //                            //throw new InvalidPluginExecutionException(startDate.Year + "_" + startDate.Month.ToString() + "_" + i + "_" + target["subject"] + "_" + workHourArr[0] + "_" + workHourArr[1] + "_" + workHourArr[2] + "_" + workHourArr[3] + "_" + workHourArr[4] + "_" + ec2.Entities.Count);


            //                            Entity newWeeklyReportDetail = new Entity("new_weekly_report_detail"); // 새로운 주간업무 상세를 만든다

            //                            newWeeklyReportDetail["new_l_task"] = new EntityReference("task", target.Id); // 관련 작업 대입
            //                            newWeeklyReportDetail["new_txt_subject"] = target["subject"]; // 제목 대입
            //                            newWeeklyReportDetail["new_d_input_expected_monday"] = workHourArr[0]; // 월요일 예상시간 대입
            //                            newWeeklyReportDetail["new_d_input_expected_tuesday"] = workHourArr[1]; // 화요일 예상시간 대입
            //                            newWeeklyReportDetail["new_d_input_expected_wednesday"] = workHourArr[2]; // 수요일 예상시간 대입
            //                            newWeeklyReportDetail["new_d_input_expected_thursday"] = workHourArr[3]; // 목요일 예상시간 대입
            //                            newWeeklyReportDetail["new_d_input_expected_friday"] = workHourArr[4]; // 금요일 예상시간 대입
            //                            newWeeklyReportDetail["new_i_year"] = startDate.Year; // 년 대입
            //                            newWeeklyReportDetail["new_i_month"] = startDate.Month; // 월 대입
            //                            newWeeklyReportDetail["new_i_week"] = i; // 주차 대입

            //                            service.Create(newWeeklyReportDetail);
            //                        }

                                
            //                    }
            //                }

            //                context.InputParameters["Target"] = target;
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new InvalidPluginExecutionException(ex.Message);
            //}
        }
    }
}