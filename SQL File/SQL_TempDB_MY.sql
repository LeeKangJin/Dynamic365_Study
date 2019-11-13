-- View 생성 ( 내부 결재라인)
CREATE VIEW OUTPERMISSION AS 
SELECT   new_l_approver1Name,new_dt_approval1,new_l_approver2Name,new_dt_approval2,new_l_approver3Name,new_dt_approval3,new_l_approver4Name,new_dt_approval4,A.new_l_it_requestName,Title,A.new_l_request_type
FROM new_approval B,Incident A
WHERE B.new_l_it_requestName = A.new_l_it_requestName AND
       A.Title like '%IT보안_네트워크_VPN 사용권한%' AND
	   new_p_approval_type = 100000000 

-- View 생성 ( 외부 결재 라인)
CREATE VIEW INPERMISSION AS 
SELECT   new_l_approver1Name,new_dt_approval1,new_l_approver2Name,new_dt_approval2,new_l_approver3Name,new_dt_approval3,new_l_approver4Name,new_dt_approval4,A.new_l_it_requestName,Title,A.new_l_request_type
FROM new_approval B,Incident A
WHERE B.new_l_it_requestName = A.new_l_it_requestName AND
       A.Title like '%IT보안_네트워크_VPN 사용권한%' AND
	   new_p_approval_type = 100000001


	   
SELECT A.new_l_it_requestName ,
	   ( CASE C.new_p_request_item 
	   WHEN '100000000' THEN 'GMP System'
	   WHEN '100000005' THEN 'SAP ERP'
	   WHEN '100000001' THEN 'NonGMP System'
	   WHEN '100000002' THEN 'IT Security' 
	   WHEN '100000003' THEN 'SystemAdmin'
	   WHEN '100000004' THEN 'IT HelpDesk'
	   END) RequestType,
	   D.new_txt_subject requestname,
	   D.new_ntxt_purpose_detail requestdetail,
	   --(CASE D.new_p_request_status  -- 해당필드가 아님
	   --WHEN '100000000' THEN '신청중'
	   --WHEN '100000001' THEN '결재중'
	   --WHEN '100000002' THEN '결재승인'
	   --WHEN '100000003' THEN '작업진행중'
	   --WHEN '100000004' THEN '작업완료'
	   --WHEN '100000005' THEN '결재반려'
	   --WHEN '100000099' THEN '취소'
	   --END) reqeuststate,
	   -- *TO DO*
	   -- D & F 뽑을 것
	   
	   C.new_txt_request_type_detail1,
	   C.new_txt_request_type_detail2,
	   CONCAT(CONCAT(CONCAT( A.new_l_approver1Name ,'-', A.new_dt_approval1)  ,' | ',A. new_l_approver2Name ),'-',A.new_dt_approval2 ) INPERMISSION , 
	   CONCAT(CONCAT(CONCAT(CONCAT(CONCAT( B.new_l_approver1Name ,'-', B.new_dt_approval1)  ,' | ',B. new_l_approver2Name ),'-',B.new_dt_approval2 ),' | ',B.new_l_approver3Name),'-',B.new_dt_approval3) OUTPERMISSION
FROM INPERMISSION A,
     OUTPERMISSION B JOIN new_request_type C
ON(B.new_l_request_type = C.new_request_typeId) JOIN 
     new_it_request D
ON(B.new_l_it_requestName = D.new_name)
-- *TO DO*
--  JOIN ON ( new_l_it_requestName 으로 ) 
-- 사용자  E
-- 그룹    F

WHERE A.new_l_it_requestName = B.new_l_it_requestName