
SELECT new_l_requestName FROM Incident  

SELECT new_name  FROM new_it_request  -- 6�� ���� ������ ���� ���̽��� �ش� Database�� ����. 

SELECT * FROM Entity JOIN StringMap ON (Entity.ObjectTypeCode = StringMap.ObjectTypeCode)

SELECT CreatedOn,* FROM Incident
ORDER BY Incident.CreatedOn DESC


SELECT StateCode FROM Incident

SELECT * FROM StringMap WHERE ObjectTypeCode = '112' AND AttributeName = 'statecode'
-- Ȱ�� �ذ�� ��ҵ�



SELECT * FROM Incident
WHERE OwnerIdName ='�� ����'


SELECT * FROM incidentresolution  -- �ذ�� �ð� ã�� enetity

SELECT ParentCustomerIdName,StringMap.Value,TimeSpent FROM Incident AS I LEFT JOIN incidentResolution AS IR
ON(I.IncidentId = IR.IncidentId) LEFT JOIN Contact AS C 
ON(I.OwnerId =C.OwnerId) LEFT JOIN StringMap ON(I.StateCode = StringMap.AttributeValue) 
WHERE 1=1
AND StringMap.ObjectTypeCode = '112'
AND StringMap.AttributeName = 'statecode'
AND I.CreatedOn between '2019-12-01' and '2019-12-07'

SELECT * FROM Incident
SELECT * FROM contact


--  �ش� ��� �Ű� ������ �ѱ�
SELECT * FROM StringMap WHERE ObjectTypeCode = '112' AND AttributeName = 'statecode'
-- Ȱ�� �ذ�� ��ҵ�



-- �ش� SELECT �� VIEW�� ����� COUNT�ؼ� Ȱ���ϱ�..
SELECT ParentCustomerIdName,TimeSpent,I.StateCode FROM Incident AS I LEFT JOIN IncidentResolution AS IR  -- �ذ� �ȉ� ���� �ؾߵǼ�
ON(I.IncidentId = IR.IncidentId) LEFT JOIN Contact AS C
ON(I.ContactId = C.ContactId)
WHERE I.CreatedOn between '2019-12-01' and '2019-12-07'

