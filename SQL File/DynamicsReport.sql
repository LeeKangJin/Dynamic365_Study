
SELECT         C.AccountIdName, I.StateCode , IR.TimeSpent, R.new_p_request_item, R.new_txt_request_type_detail1, I.CreatedByName, I.OwnerIdName, D.new_l_parent_departmentName
FROM           Incident               AS I 
LEFT JOIN       incidentResolution      AS IR   ON I.IncidentId = IR.IncidentId 
LEFT JOIN       Contact                        AS C    ON I.ContactId = C.ContactId 
LEFT JOIN       new_request_type AS R    ON I.new_l_request_type = R.new_request_typeId
LEFT JOIN       StringMap                      AS S    ON R.new_p_request_item = S.AttributeValue
LEFT JOIN       new_department          AS D    ON I.Id = D.OwnerId

WHERE 1=1
AND (S.ObjectTypeCode = '112' OR S.ObjectTypeCode = '10051')
AND (S.AttributeName = 'statecode' OR S.AttributeName = 'new_p_request_item')
AND I.CreatedOn BETWEEN '2019-12-08' AND '2019-12-14'

SELECT * FROM Incident WHERE CreatedOn BETWEEN '2019-12-15' AND '2019-12-17'

SELECT * FROM Contact
