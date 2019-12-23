

SELECT new_p_report_category
	,new_txt_request_type_detail1
	,Incident.StateCode
FROM Incident JOIN new_request_type ON(Incident.new_l_request_type = new_request_type.new_request_typeId)
JOIN BusinessUnit ON (Incident.OwningBusinessUnit = BusinessUnit.BusinessUnitId)
WHERE Incident.CreatedOn Between '2019-12-01' and '2019-12-20'
AND BusinessUnit.Name = 'IT°³¹ßÆÀ'


