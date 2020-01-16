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
            //                Entity retrieveTarget = service.Retrieve("task", target.Id, new ColumnSet(true));

            //                DateTime startDate = Convert.ToDateTime(retrieveTarget["scheduledstart"]); // 시작 날짜
            //                DateTime endDate = Convert.ToDateTime(retrieveTarget["scheduledend"]); // 종료 날짜
            //                TimeSpan dateDiff = endDate - startDate; // 차이

            //                int week = Enum.GetValues(typeof(DayOfWeek)).Length;

            //                int startDayOffset = (int)startDate.AddDays(-(startDate.Day - 1)).DayOfWeek;
            //                int startWeekCnt = (startDate.Day + startDayOffset) / week;

            //                startWeekCnt += ((startDate.Day + startDayOffset) % week) > 0 ? 1 : 0; // 시작 날짜의 주차

            //                int endDayOffset = (int)endDate.AddDays(-(endDate.Day - 1)).DayOfWeek;
            //                int endWeekCnt = (endDate.Day + endDayOffset) / week;

            //                endWeekCnt += ((endDate.Day + endDayOffset) % week) > 0 ? 1 : 0; // 종료 날짜의 주차

            //                int startDayCnt = Convert.ToInt32(startDate.DayOfWeek); // 시작 날짜의 요일(숫자)
            //                int endDayCnt = Convert.ToInt32(endDate.DayOfWeek); // 종료 날짜의 요일(숫자)

            //                decimal[] workHourArr = new decimal[5] { 0, 0, 0, 0, 0 }; // 1주일 간의 근무시간

            //                for (int i = startDayCnt - 1; i < endDayCnt; i++)
            //                {
            //                    workHourArr[i] = Convert.ToDecimal(target["actualdurationminutes"]) / (dateDiff.Days + 1);
            //                }

            //                QueryExpression qe = new QueryExpression("new_weekly_report_detail");
            //                qe.ColumnSet.AddColumns("new_i_year", "new_i_month", "new_i_week", "new_txt_subject", "new_d_input_expected_monday", "new_d_input_expected_tuesday", "new_d_input_expected_wednesday", "new_d_input_expected_thursday", "new_d_input_expected_friday");
            //                qe.Criteria.AddFilter(new FilterExpression());
            //                qe.Criteria.Filters[0].AddCondition("new_i_year", ConditionOperator.Equal, startDate.Year); // 년도 비교
            //                qe.Criteria.Filters[0].AddCondition("new_i_month", ConditionOperator.Equal, startDate.Month); // 월 비교
            //                qe.Criteria.Filters[0].AddCondition("new_i_week", ConditionOperator.Equal, i); // 주차 비교
            //                qe.Criteria.Filters[0].AddCondition("new_txt_subject", ConditionOperator.Equal, target["subject"].ToString()); // task의 제목과 detail의 상세 내용 비교
            //                qe.Criteria.Filters[0].AddCondition("new_d_input_expected_monday", ConditionOperator.Equal, workHourArr[0]); // 
            //                qe.Criteria.Filters[0].AddCondition("new_d_input_expected_tuesday", ConditionOperator.Equal, workHourArr[1]); // 
            //                qe.Criteria.Filters[0].AddCondition("new_d_input_expected_wednesday", ConditionOperator.Equal, workHourArr[2]); // 
            //                qe.Criteria.Filters[0].AddCondition("new_d_input_expected_thursday", ConditionOperator.Equal, workHourArr[3]); // 
            //                qe.Criteria.Filters[0].AddCondition("new_d_input_expected_friday", ConditionOperator.Equal, workHourArr[4]); // 
            //                qe.Criteria.Filters[0].FilterOperator = LogicalOperator.And;

            //                EntityCollection ec2 = service.RetrieveMultiple(qe);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new InvalidPluginExecutionException(ex.Message);
            //}