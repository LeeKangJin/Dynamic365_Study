	SELECT  B.Title AS '���� ���̽� ����',
			B.new_l_requestName AS '���� IT ��û��',
			E.AccountIdName AS '�׷���(��)',
			(CASE
			D.new_p_request_type
			WHEN '100000000' THEN 'IT ���� ��û��'
			WHEN '100000001' THEN 'IT �ڻ� ��û��'
			WHEN '100000002' THEN '�������� ���� ��û��'
			WHEN '100000003' THEN '��Ÿ��û'
			END
			) AS '��û��Ÿ��',
			D.new_ntxt_purpose_detail AS '��û����',
			D.new_dt_target_from AS '���Ⱓ From',
			D.new_dt_target_to AS '���Ⱓ To',
			D.new_dt_request AS '��û�Ͻ�',
			D.new_dt_complete AS '�Ϸ��Ͻ�',
			E.FullName AS '��',
			E.new_txt_companynum AS '���',
			B.OwnerIdName AS '�����',
			E.new_l_departmentName AS '�Ҽ���',
		   (CASE B.StateCode 
			WHEN '0' THEN 'Ȱ��'
			WHEN '1' THEN '�ذ��'
			END)AS '����',
		   B.ModifiedOn AS '������ ��¥',
		   B.CreatedOn AS '���糯¥',
		   CONCAT(CONCAT(CONCAT(CONCAT(CONCAT( A.new_l_approver1Name ,'-', A.new_dt_approval1)  ,' | ',A. new_l_approver2Name ),'-',A.new_dt_approval2 ),' | ',A.new_l_approver3Name),'-',A.new_dt_approval3) AS '�������'
	FROM incident B INNER JOIN new_request D
	ON (B.new_l_request = D.new_requestId ) INNER JOIN (SELECT AttributeValue,[Value], objecttypecode FROM StringMap WHERE AttributeName = 'new_p_classfication_secu' and ObjectTypeCode = 10027) T
	ON (T.AttributeValue = D.new_p_classfication_secu) INNER JOIN new_approval A
	ON (A.new_l_request =D.new_requestId) INNER JOIN Contact E
	ON(B.CustomerId=E.ContactId)  
	WHERE T.Value= 'VPN ������' 


	-- Entity ���� �������� StringMap�� ������ ���� (Look - Up Filed Key & Value ) 
	-- ���� Entity���� ���ϰ�� ObjectTypeCode�� �������� �ִ� . 
	-- ex 
	SELECT AttributeValue,[Value], objecttypecode FROM StringMap WHERE AttributeName = 'new_p_classfication_secu'  -- and ObjectTypeCode = 10027