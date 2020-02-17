

-- num er 1
SELECT P.new_name
		,CONVERT(CHAR(10),new_dt_exstart,23) AS EXSTART
		,CONVERT(CHAR(10),new_dt_exend,23) AS EXEND
		,CONVERT(CHAR(10),GETDATE(),23) AS TODAY
		,CASE WHEN ROUND(CONVERT(float,(DATEDIFF(dd ,CONVERT(CHAR(10),GETDATE(),12),P.new_dt_exstart))) / DATEDIFF(dd,P.new_dt_exend ,P.new_dt_exstart),2) > 1 THEN 100 ELSE 
		 ROUND(CONVERT(float,(DATEDIFF(dd ,CONVERT(CHAR(10),GETDATE(),12),P.new_dt_exstart))) / DATEDIFF(dd,P.new_dt_exend ,P.new_dt_exstart),2)*100   END AS '��ǥ' 
		,PT.new_d_percentage '����'
		,CONVERT(CHAR(10),DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE()),0)),23) AS FirstDay
		,CONVERT(CHAR(10),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0)),23) AS LastDay
		,�Ѻз� = 1
		,CASE WHEN PT.new_d_percentage != 0 THEN 1 ELSE 0 END AS ��������cnt
		,CASE WHEN (PT.new_d_percentage != 100 AND PT.new_dt_end < CONVERT(CHAR(10),GETDATE(),23) ) THEN 1 ELSE 0 END AS ����cnt 
FROM new_project AS P JOIN new_projectdetail AS PT ON (P.new_projectId = PT.new_l_project)
WHERE P.CreatedOn Between CONVERT(CHAR(10),DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE()),0)),23)  and CONVERT(CHAR(10),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0)),23)


SELECT CONVERT(CHAR(10),DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE()),0)),23) AS FirstDay
	   ,CONVERT(CHAR(10),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0)),23) AS LastDay
	   ,CONVERT(CHAR(10),GETDATE(),23) AS ToDay



-- number 2
SELECT 
	p_request_item.Value
	,new_txt_request_type_detail1
	,��ûcnt = 1
	,CASE WHEN Incident.stateCode = 0 THEN 1 ELSE 0 END AS �̿Ϸ�cnt
    ,CASE WHEN Incident.stateCode = 1 THEN 1 ELSE 0 END AS �Ϸ�cnt
FROM Incident JOIN new_request_type ON(Incident.new_l_request_type = new_request_type.new_request_typeId)
JOIN BusinessUnit ON (Incident.OwningBusinessUnit = BusinessUnit.BusinessUnitId)
LEFT JOIN (SELECT ObjectTypeCode,AttributeValue,AttributeName,[Value] FROM StringMap WHERE AttributeName = 'new_p_report_category' AND ObjectTypeCode = 10051) p_request_item ON (new_request_type.new_p_report_category = p_request_item.AttributeValue)
WHERE Incident.CreatedOn Between CONVERT(CHAR(10),DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE()),0)),23)  and CONVERT(CHAR(10),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0)),23)
AND BusinessUnit.Name = 'IT������'

--number 3

SELECT CASE WHEN task.RegardingObjectTypeCode = 10067 THEN 'IT Support' ELSE '�׷������' END AS SupportType 
		,task.RegardingObjectIdName
		,task.Subject
		,task.Description
		,CONVERT(CHAR(10),task.ScheduledStart,23) AS '������'
		,CONVERT(CHAR(10),task.ScheduledEnd,23) AS '��ħ��'
		,CASE WHEN task.ActualDurationMinutes > 1440 THEN task.ActualDurationMinutes / 3 ELSE  task.ActualDurationMinutes  END AS '�����ð�'
		,task.OwnerIdName
FROM BusinessUnit
JOIN task ON ( BusinessUnit.BusinessUnitId = task.OwningBusinessUnit)
WHERE Name = 'IT������'
AND task.CreatedOn Between CONVERT(CHAR(10),DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE()),0)),23)  and CONVERT(CHAR(10),DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0)),23)
AND (task.RegardingObjectTypeCode ='1' OR  task.RegardingObjectTypeCode  ='10067')
AND task.RegardingObjectIdName != '��Ʈ����'


SELECT * FROM task