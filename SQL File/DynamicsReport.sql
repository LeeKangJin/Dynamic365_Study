
 DECLARE @STARTDATE DATE, @ENDDATE DATE
 /*주간보고: 일 월 화 수 목 금 토 추출*/
--시작일: 지난주 일요일
IF(@STARTDATE IS NULL)
BEGIN
	SET @STARTDATE = CONVERT(DATE,DATEADD(DAY, (DATEPART(WEEKDAY,GETDATE()) + 6) * -1 ,GETDATE()))
END
	
--종료일: 시작일+6일(=지난주 토요일)
IF(@ENDDATE IS NULL)
BEGIN
	SET @ENDDATE = CONVERT(DATE,DATEADD(DAY, 6, DATEADD(DAY, (DATEPART(WEEKDAY,GETDATE()) + 6) * -1 ,GETDATE())))
END 

SELECT CONVERT(DATE,DATEADD(DAY, (DATEPART(WEEKDAY,GETDATE()) + 6) * -1 ,GETDATE()))

 -- ? 안돌아감 ?
--From, To Default 값 설정
--DECLARE @STARTDATE DATE = '2019-11-01', @ENDDATE DATE = '2019-12-14'

--SET @STARTDATE = (SELECT  DATEADD(wk, DATEDIFF(d, 0, getdate()) / 7 -1 , -1)    AS '저번주 일요일(시작)')
--SET @ENDDATE =  (SELECT  DATEADD(wk, DATEDIFF(d, 0, getdate()) / 7 -1 , 5)     AS '저번주 토요일(끝)'   )

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
                 요청cnt = 1,
                 CASE WHEN I.stateCode = 0 THEN 1 ELSE 0 END AS 진행중cnt,
                 CASE WHEN I.stateCode = 2 THEN 1 ELSE 0 END AS 취소cnt,
                 CASE WHEN I.stateCode = 1 THEN 1 ELSE 0 END AS 해결됨cnt,
                              TimeSpent,
                 ISNULL(CONVERT(float, CASE WHEN CONVERT(float, IR.TimeSpent) > 1440 THEN (CONVERT(float, IR.TimeSpent)/480)  ELSE (CONVERT(float, IR.TimeSpent)/60)   END),0) AS 지원시간,
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

