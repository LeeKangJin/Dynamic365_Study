	SELECT  B.Title AS '서비스 케이스 제목',
			B.new_l_requestName AS '관련 IT 요청서',
			E.AccountIdName AS '그룹사명(고객)',
			(CASE
			D.new_p_request_type
			WHEN '100000000' THEN 'IT 보안 요청서'
			WHEN '100000001' THEN 'IT 자산 요청서'
			WHEN '100000002' THEN '공유폴더 권한 요청서'
			WHEN '100000003' THEN '기타요청'
			END
			) AS '요청서타입',
			D.new_ntxt_purpose_detail AS '요청내용',
			D.new_dt_target_from AS '사용기간 From',
			D.new_dt_target_to AS '사용기간 To',
			D.new_dt_request AS '요청일시',
			D.new_dt_complete AS '완료일시',
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
		   CONCAT(CONCAT(CONCAT(CONCAT(CONCAT( A.new_l_approver1Name ,'-', A.new_dt_approval1)  ,' | ',A. new_l_approver2Name ),'-',A.new_dt_approval2 ),' | ',A.new_l_approver3Name),'-',A.new_dt_approval3) AS '결재라인'
	FROM incident B INNER JOIN new_request D
	ON (B.new_l_request = D.new_requestId ) INNER JOIN (SELECT AttributeValue,[Value], objecttypecode FROM StringMap WHERE AttributeName = 'new_p_classfication_secu' and ObjectTypeCode = 10027) T
	ON (T.AttributeValue = D.new_p_classfication_secu) INNER JOIN new_approval A
	ON (A.new_l_request =D.new_requestId) INNER JOIN Contact E
	ON(B.CustomerId=E.ContactId)  
	WHERE T.Value= 'VPN 사용권한' 


	-- Entity 마다 전역으로 StringMap을 가지고 있음 (Look - Up Filed Key & Value ) 
	-- 여러 Entity에서 쓰일경우 ObjectTypeCode로 나뉘어져 있다 . 
	-- ex 
	SELECT AttributeValue,[Value], objecttypecode FROM StringMap WHERE AttributeName = 'new_p_classfication_secu'  -- and ObjectTypeCode = 10027