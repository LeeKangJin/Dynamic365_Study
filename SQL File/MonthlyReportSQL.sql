
-- num er 1
SELECT new_project.new_name
		,new_dt_exstart
		,new_dt_exend
		,new_dt_realstart
		,new_dt_realend
		,new_projectdetail.new_d_percentage
FROM new_project JOIN new_projectdetail ON (new_project.new_projectId = new_projectdetail.new_l_project)
WHERE new_project.CreatedOn Between '2019-12-01' and '2019-12-20'


SELECT DATEADD(s,0,DATEADD(mm, DATEDIFF(m,0,GETDATE()),0))
SELECT DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,GETDATE())+1,0))


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
WHERE Incident.CreatedOn Between '2019-12-01' and '2019-12-20'
AND BusinessUnit.Name = 'IT개발팀'

--number 3

SELECT * FROM new_itsupport

SELECT new_l_accountName
	,Description 
	,Gro
	
	
FROM task

SELECT * FROM task
WHERE task.CreatedOn Between '2019-12-01' and '2019-12-20'


--그룹사 지원


SELECT task.Subject, task.Description,task.ActualStart FROM BusinessUnit
JOIN task ON ( BusinessUnit.BusinessUnitId = task.OwningBusinessUnit)
WHERE Name = 'IT개발팀'
AND task.CreatedOn Between '2019-12-01' and '2019-12-20'
AND (task.RegardingObjectTypeCode ='1' OR  task.RegardingObjectTypeCode  ='10067')
AND task.RegardingObjectIdName != '셀트리온'


SELECT * FROM new_itsupport

SELECT * FROM task
WHERE RegardingObjectTypeCode = '10067'