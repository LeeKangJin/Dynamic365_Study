

/*

SELECT * FROM Incident I                     -- �������̽�

SELECT * FROM new_it_request R        -- IT��û��

SELECT * FROM new_request_type T      -- ��û��Ÿ��

SELECT * FROM new_approval A          -- ����

*/




-- ( ���� �������)

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
			   A.Title like '%IT����_��Ʈ��ũ_VPN ������%' AND
			   new_p_approval_type = 100000000 
) OUTPERMISSION
-- ( �ܺ� �������)

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
			   A.Title like '%IT����_��Ʈ��ũ_VPN ������%' AND
			   new_p_approval_type = 100000001
) INPERMISSION



	   

SELECT  
		A.Title AS '���� ���̽� ����',
		A.new_l_it_requestName AS '���� IT ��û��',
		E.AccountIdName AS '�׷���(��)',
	   ( CASE C.new_p_request_item 
		   WHEN '100000000' THEN 'GMP System'
		   WHEN '100000005' THEN 'SAP ERP'
		   WHEN '100000001' THEN 'NonGMP System'
		   WHEN '100000002' THEN 'IT Security' 
		   WHEN '100000003' THEN 'SystemAdmin'
		   WHEN '100000004' THEN 'IT HelpDesk'
	   END) AS '��û���׸�',
	   C.new_txt_request_type_detail1 AS '�󼼱���1',
	   C.new_txt_request_type_detail2 AS '�󼼱���2',
	   D.new_txt_subject AS '��û����',
	   D.new_ntxt_purpose_detail AS '��û����',
	   D.new_dt_target_from AS '���Ⱓ From',
	   D.new_dt_target_to AS '���Ⱓ To',
	   D.new_dt_request AS '��û�Ͻ�',
	   D.new_dt_complete AS '�Ϸ��Ͻ�',
	   -- *TO DO*
	
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
	   CONCAT(CONCAT(CONCAT( A.new_l_approver1Name ,'-', A.new_dt_approval1)  ,' | ',A. new_l_approver2Name ),'-',A.new_dt_approval2 ) INPERMISSION , 
	   CONCAT(CONCAT(CONCAT(CONCAT(CONCAT( B.new_l_approver1Name ,'-', B.new_dt_approval1)  ,' | ',B. new_l_approver2Name ),'-',B.new_dt_approval2 ),' | ',B.new_l_approver3Name),'-',B.new_dt_approval3) OUTPERMISSION 
      --A : ���ΰ���
	  --B : �ܺΰ���
	  --C : ��û�� Ÿ��
	  --D : IT ��û��
	  --E : �� 

FROM  #INPERMISSION A JOIN  #OUTPERMISSION B 
ON(A.new_l_it_request = B.new_l_it_request)JOIN new_request_type C
ON(B.new_l_request_type = C.new_request_typeId) JOIN  new_it_request D
ON(B.new_l_it_requestName = D.new_name) JOIN Contact E
ON(B.CustomerId=E.ContactId)  


IF OBJECT_ID('tempdb..#OUTPERMISSION') IS NOT NULL
BEGIN DROP TABLE #OUTPERMISSION END


IF OBJECT_ID('tempdb..#INPERMISSION') IS NOT NULL
BEGIN DROP TABLE #INPERMISSION END