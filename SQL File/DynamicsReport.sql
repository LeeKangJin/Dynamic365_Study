
SELECT new_l_requestName FROM Incident  

SELECT new_name  FROM new_it_request  -- 6월 이후 부터의 서비스 케이스는 해당 Database에 있음. 

SELECT * FROM Entity JOIN StringMap ON (Entity.ObjectTypeCode = StringMap.ObjectTypeCode)

SELECT CreatedOn,* FROM Incident
ORDER BY Incident.CreatedOn DESC


SELECT StateCode FROM Incident

SELECT * FROM StringMap WHERE ObjectTypeCode = '112' AND AttributeName = 'statecode'
-- 활성 해결됨 취소됨



SELECT * FROM Incident
WHERE OwnerIdName ='이 강진'


SELECT * FROM incidentresolution  -- 해결됨 시간 찾는 enetity

SELECT ParentCustomerIdName,StringMap.Value,TimeSpent FROM Incident AS I LEFT JOIN incidentResolution AS IR
ON(I.IncidentId = IR.IncidentId) LEFT JOIN Contact AS C 
ON(I.OwnerId =C.OwnerId) LEFT JOIN StringMap ON(I.StateCode = StringMap.AttributeValue) 
WHERE 1=1
AND StringMap.ObjectTypeCode = '112'
AND StringMap.AttributeName = 'statecode'
AND I.CreatedOn between '2019-12-01' and '2019-12-07'

SELECT * FROM Incident
SELECT * FROM contact


--  해당 결과 매개 변수로 넘김
SELECT * FROM StringMap WHERE ObjectTypeCode = '112' AND AttributeName = 'statecode'
-- 활성 해결됨 취소됨



-- 해당 SELECT 문 VIEW로 만든뒤 COUNT해서 활용하기..
SELECT ParentCustomerIdName,TimeSpent,I.StateCode FROM Incident AS I LEFT JOIN IncidentResolution AS IR  -- 해결 안됬어도 조인 해야되서
ON(I.IncidentId = IR.IncidentId) LEFT JOIN Contact AS C
ON(I.ContactId = C.ContactId)
WHERE I.CreatedOn between '2019-12-01' and '2019-12-07'

