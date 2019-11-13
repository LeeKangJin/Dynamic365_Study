

/*

SELECT * FROM Incident I                     -- 서비스케이스

SELECT * FROM new_it_request R        -- IT요청서

SELECT * FROM new_request_type T      -- 요청서타입

SELECT * FROM new_approval A          -- 결재

*/




-- ( 내부 결재라인)

SELECT * INTO #OUTPERMISSION
FROM(
		SELECT   new_l_approver1Name,
		         new_dt_approval1,
				 new_l_approver2Name,
				 new_dt_approval2,
				 new_l_approver3Name,
				 new_dt_approval3,
				 new_l_approver4Name,
				 new_dt_approval4,
				 A.new_l_it_requestName,
				 Title,
				 A.new_l_request_type,
				 A.new_l_it_request,
				 A.CustomerId,
				 A.OwnerIdName,
				 A.StateCode,
				 A.ModifiedOn,
				 A.CreatedOn
		FROM new_approval B,Incident A
		WHERE B.new_l_it_requestName = A.new_l_it_requestName AND
			   A.Title like '%IT보안_네트워크_VPN 사용권한%' AND
			   new_p_approval_type = 100000000 
) OUTPERMISSION
-- ( 외부 결재라인)

SELECT * INTO #INPERMISSION
FROM (
		SELECT   new_l_approver1Name,
				 new_dt_approval1,
				 new_l_approver2Name,
				 new_dt_approval2,
				 new_l_approver3Name,
				 new_dt_approval3,
				 new_l_approver4Name,
				 new_dt_approval4,
				 A.new_l_it_requestName,
				 Title,
				 A.new_l_request_type,
				 A.new_l_it_request,
				 A.CustomerId
		FROM new_approval B,Incident A
		WHERE B.new_l_it_requestName = A.new_l_it_requestName AND
			   A.Title like '%IT보안_네트워크_VPN 사용권한%' AND
			   new_p_approval_type = 100000001
) INPERMISSION



	   

SELECT  
		A.Title AS '서비스 케이스 제목',
		A.new_l_it_requestName AS '관련 IT 요청서',
		E.AccountIdName AS '그룹사명(고객)',
	   ( CASE C.new_p_request_item 
		   WHEN '100000000' THEN 'GMP System'
		   WHEN '100000005' THEN 'SAP ERP'
		   WHEN '100000001' THEN 'NonGMP System'
		   WHEN '100000002' THEN 'IT Security' 
		   WHEN '100000003' THEN 'SystemAdmin'
		   WHEN '100000004' THEN 'IT HelpDesk'
	   END) AS '요청서항목',
	   C.new_txt_request_type_detail1 AS '상세구분1',
	   C.new_txt_request_type_detail2 AS '상세구분2',
	   D.new_txt_subject AS '요청제목',
	   D.new_ntxt_purpose_detail AS '요청내용',
	   D.new_dt_target_from AS '사용기간 From',
	   D.new_dt_target_to AS '사용기간 To',
	   D.new_dt_request AS '요청일시',
	   D.new_dt_complete AS '완료일시',
	   -- *TO DO*
	
	   E.FullName AS '고객',
	   E.new_txt_companynum AS '사번',
	   B.OwnerIdName AS '담당자',
	   E.new_l_departmentName AS '소속팀',
	   (CASE B.StateCode 
	    WHEN '0' THEN '활성'
		WHEN '1' THEN '해결됨'
		END)AS '상태',
	   B.ModifiedOn AS '수정한 날짜',
	   B.CreatedOn AS '만든날짜',
	   CONCAT(CONCAT(CONCAT( A.new_l_approver1Name ,'-', A.new_dt_approval1)  ,' | ',A. new_l_approver2Name ),'-',A.new_dt_approval2 ) INPERMISSION , 
	   CONCAT(CONCAT(CONCAT(CONCAT(CONCAT( B.new_l_approver1Name ,'-', B.new_dt_approval1)  ,' | ',B. new_l_approver2Name ),'-',B.new_dt_approval2 ),' | ',B.new_l_approver3Name),'-',B.new_dt_approval3) OUTPERMISSION 
      --A : 내부결재
	  --B : 외부결재
	  --C : 요청서 타입
	  --D : IT 요청서
	  --E : 고객 

FROM  #INPERMISSION A JOIN  #OUTPERMISSION B 
ON(A.new_l_it_request = B.new_l_it_request)JOIN new_request_type C
ON(B.new_l_request_type = C.new_request_typeId) JOIN  new_it_request D
ON(B.new_l_it_requestName = D.new_name) JOIN Contact E
ON(B.CustomerId=E.ContactId)  


IF OBJECT_ID('tempdb..#OUTPERMISSION') IS NOT NULL
BEGIN DROP TABLE #OUTPERMISSION END


IF OBJECT_ID('tempdb..#INPERMISSION') IS NOT NULL
BEGIN DROP TABLE #INPERMISSION END