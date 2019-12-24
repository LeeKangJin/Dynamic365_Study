

-- num er 1
SELECT P.new_name
		,CONVERT(CHAR(10),new_dt_exstart,23) AS EXSTART
		,CONVERT(CHAR(10),new_dt_exend,23) AS EXEND
		,CONVERT(CHAR(10),GETDATE(),23) AS TODAY
		,CASE WHEN ROUND(CONVERT(float,(DATEDIFF(dd ,CONVERT(CHAR(10),GETDATE(),12),P.new_dt_exstart))) / DATEDIFF(dd,P.new_dt_exend ,P.new_dt_exstart),2) > 1 THEN 100 ELSE 
		 ROUND(CONVERT(float,(DATEDIFF(dd ,CONVERT(CHAR(10),GETDATE(),12),P.new_dt_exstart))) / DATEDIFF(dd,P.new_dt_exend ,P.new_dt_exstart),2)*100   END AS '목표' 
		,PT.new_d_percentage '실적'
		,CONVERT(CHAR(10),DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE()),0)),23) AS FirstDay
		,CONVERT(CHAR(10),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0)),23) AS LastDay
		,총분류 = 1
		,CASE WHEN PT.new_d_percentage != 0 THEN 1 ELSE 0 END AS 누적진행cnt
		,CASE WHEN (PT.new_d_percentage != 100 AND PT.new_dt_end < CONVERT(CHAR(10),GETDATE(),23) ) THEN 1 ELSE 0 END AS 지연cnt 
FROM new_project AS P JOIN new_projectdetail AS PT ON (P.new_projectId = PT.new_l_project)
WHERE P.CreatedOn Between CONVERT(CHAR(10),DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE()),0)),23)  and CONVERT(CHAR(10),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0)),23)


SELECT CONVERT(CHAR(10),DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE()),0)),23) AS FirstDay
	   ,CONVERT(CHAR(10),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0)),23) AS LastDay
	   ,CONVERT(CHAR(10),GETDATE(),23) AS ToDay



-- number 2
SELECT 
	p_request_item.Value
	,new_txt_request_type_detail1
	,요청cnt = 1
	,CASE WHEN Incident.stateCode = 0 THEN 1 ELSE 0 END AS 미완료cnt
    ,CASE WHEN Incident.stateCode = 1 THEN 1 ELSE 0 END AS 완료cnt
FROM Incident JOIN new_request_type ON(Incident.new_l_request_type = new_request_type.new_request_typeId)
JOIN BusinessUnit ON (Incident.OwningBusinessUnit = BusinessUnit.BusinessUnitId)
LEFT JOIN (SELECT ObjectTypeCode,AttributeValue,AttributeName,[Value] FROM StringMap WHERE AttributeName = 'new_p_report_category' AND ObjectTypeCode = 10051) p_request_item ON (new_request_type.new_p_report_category = p_request_item.AttributeValue)
WHERE Incident.CreatedOn Between CONVERT(CHAR(10),DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE()),0)),23)  and CONVERT(CHAR(10),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0)),23)
AND BusinessUnit.Name = 'IT개발팀'

--number 3

SELECT CASE WHEN task.RegardingObjectTypeCode = 10067 THEN 'IT Support' ELSE '그룹사지원' END AS SupportType 
		,task.RegardingObjectIdName
		,task.Subject
		,task.Description
		,CONVERT(CHAR(10),task.ScheduledStart,23) AS '시작일'
		,CONVERT(CHAR(10),task.ScheduledEnd,23) AS '마침일'
		,CASE WHEN task.ActualDurationMinutes > 1440 THEN task.ActualDurationMinutes / 3 ELSE  task.ActualDurationMinutes  END AS '지원시간'
		,task.OwnerIdName
FROM BusinessUnit
JOIN task ON ( BusinessUnit.BusinessUnitId = task.OwningBusinessUnit)
WHERE Name = 'IT개발팀'
AND task.CreatedOn Between CONVERT(CHAR(10),DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE()),0)),23)  and CONVERT(CHAR(10),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0)),23)
AND (task.RegardingObjectTypeCode ='1' OR  task.RegardingObjectTypeCode  ='10067')
AND task.RegardingObjectIdName != '셀트리온'


SELECT * FROM task