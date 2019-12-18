
 DECLARE @STARTDATE DATE, @ENDDATE DATE
 /*�ְ�����: �� �� ȭ �� �� �� �� ����*/
--������: ������ �Ͽ���
IF(@STARTDATE IS NULL)
BEGIN
	SET @STARTDATE = CONVERT(DATE,DATEADD(DAY, (DATEPART(WEEKDAY,GETDATE()) + 6) * -1 ,GETDATE()))
END
	
--������: ������+6��(=������ �����)
IF(@ENDDATE IS NULL)
BEGIN
	SET @ENDDATE = CONVERT(DATE,DATEADD(DAY, 6, DATEADD(DAY, (DATEPART(WEEKDAY,GETDATE()) + 6) * -1 ,GETDATE())))
END 

SELECT CONVERT(DATE,DATEADD(DAY, (DATEPART(WEEKDAY,GETDATE()) + 6) * -1 ,GETDATE()))

 -- ? �ȵ��ư� ?
--From, To Default �� ����
--DECLARE @STARTDATE DATE = '2019-11-01', @ENDDATE DATE = '2019-12-14'

--SET @STARTDATE = (SELECT  DATEADD(wk, DATEDIFF(d, 0, getdate()) / 7 -1 , -1)    AS '������ �Ͽ���(����)')
--SET @ENDDATE =  (SELECT  DATEADD(wk, DATEDIFF(d, 0, getdate()) / 7 -1 , 5)     AS '������ �����(��)'   )

--SELECT @STARTDATE, @ENDDATE

SELECT                     
            A.AccountId,
                 C.AccountIdName, 
                 A.new_txt_gw_code,
                 R.new_p_request_item,
                 p_request_item.Value AS 'new_p_request_item_label',
                              p_request_item.DisplayOrder AS 'new_p_request_item_order',
                 R.new_txt_request_type_detail1,
                 R.new_txt_request_type_detail2,
                 I.OwnerId,
                 I.OwnerIdName,
                 B.Name,
                 ��ûcnt = 1,
                 CASE WHEN I.stateCode = 0 THEN 1 ELSE 0 END AS ������cnt,
                 CASE WHEN I.stateCode = 2 THEN 1 ELSE 0 END AS ���cnt,
                 CASE WHEN I.stateCode = 1 THEN 1 ELSE 0 END AS �ذ��cnt,
                              TimeSpent,
                 ISNULL(CONVERT(float, CASE WHEN CONVERT(float, IR.TimeSpent) > 1440 THEN (CONVERT(float, IR.TimeSpent)/480)  ELSE (CONVERT(float, IR.TimeSpent)/60)   END),0) AS �����ð�,
                 SU.IsDisabled
FROM                         Incident                                               AS I 
LEFT JOIN                    incidentResolution                            AS IR    ON I.IncidentId = IR.IncidentId 
INNER JOIN                    Contact                                    AS C     ON I.CustomerId = C.ContactId 
INNER JOIN                    new_request_type                              AS R     ON I.new_l_request_type = R.new_request_typeId
LEFT JOIN                        (SELECT ObjectTypeCode,AttributeValue,AttributeName,[Value],DisplayOrder FROM StringMap WHERE AttributeName = 'new_p_request_item' AND ObjectTypeCode = 10051) p_request_item ON R.new_p_request_item = p_request_item.AttributeValue
INNER JOIN                    SystemUser                      AS SU    ON I.OwnerId = SU.SystemUserId
INNER JOIN                    Account                         AS A     ON C.AccountId = A.AccountId
INNER JOIN                    BusinessUnit                                     AS B     ON B.BusinessUnitId = I.OwningBusinessUnit
WHERE 1=1
AND SU.IsDisabled = 0
AND DATEADD(Hour,9,I.CreatedOn) BETWEEN @STARTDATE AND @ENDDATE

