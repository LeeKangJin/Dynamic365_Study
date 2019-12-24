
-- num er 1
SELECT new_project.new_name
		,CONVERT(CHAR(10),new_dt_exstart,23)
		,CONVERT(CHAR(10),new_dt_exend,23)
		,CONVERT(CHAR(10),new_projectdetail.new_dt_start,23)
		,CONVERT(CHAR(10),new_projectdetail.new_dt_end,23)
		,CONVERT(CHAR(10),GETDATE(),23)
		,�Ѻз� = 1
		,CASE WHEN new_projectdetail.new_d_percentage != 0 THEN 1 ELSE 0 END AS ��������cnt
		,CASE WHEN (new_projectdetail.new_d_percentage != 100 AND new_projectdetail.new_dt_end < CONVERT(CHAR(10),GETDATE(),23) ) THEN 1 ELSE 0 END AS ����cnt 
FROM new_project JOIN new_projectdetail ON (new_project.new_projectId = new_projectdetail.new_l_project)
WHERE new_project.CreatedOn Between '2019-12-01' and '2019-12-20'


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
WHERE Incident.CreatedOn Between '2019-12-01' and '2019-12-20'
AND BusinessUnit.Name = 'IT������'

--number 3

SELECT task.Subject, task.Description,task.ActualStart
,CASE WHEN task.RegardingObjectTypeCode = 10067 THEN 'IT Support' ELSE '�׷������' END AS SupportType FROM BusinessUnit
JOIN task ON ( BusinessUnit.BusinessUnitId = task.OwningBusinessUnit)
WHERE Name = 'IT������'
AND task.CreatedOn Between '2019-12-01' and '2019-12-20'
AND (task.RegardingObjectTypeCode ='1' OR  task.RegardingObjectTypeCode  ='10067')
AND task.RegardingObjectIdName != '��Ʈ����'

