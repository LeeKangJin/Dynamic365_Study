# Pre  vs Post

## Pre Plug -in

 db에 데이터 적재 이전에 플러그인 코드가 실행 ( Pre : 이전에 실행 된다)
 
 '''
 target = contect["target"]
 
 target["ex"]  = a
 
 // 타겟에 적재된 데이터가 플러그인 코드가 끝나고 DB에 적재
 '''
 
 ## Post Plug - in
 
 db 적재 이후 플러그인 코드가 실행
 
 '''
 
 target에 적재 X retrieve를 이용해서 데이터들을 가져와야 할것. 
 
 '''

Retrieve Multiple Template.

                        QueryExpression qe = new QueryExpression
                        {
                            EntityName = "new_sequencer",
                            ColumnSet = new ColumnSet("new_name", "new_number"),
                            Criteria =
                                {
                                    FilterOperator = LogicalOperator.And,
                                    Conditions =
                                    {
                                        new ConditionExpression
                                        {
                                            AttributeName = "new_name",
                                            Operator = ConditionOperator.Equal,
                                            Values = { "request_new" }
                                        }
                                    }
                                }
                        };

                        EntityCollection seqResult = service.RetrieveMultiple(qe);
                        foreach (Entity seqDetail in seqResult.Entities)
                        {
                            seqNum = Convert.ToInt32(seqDetail.Attributes["new_number"]);
                            seqId = seqDetail.Attributes["new_sequencerid"].ToString();
                        }


********************************************************
AttributeName -> Look up 필드 일시
Value는 GUID를 넣어줘야함
********************************************************
