USE [CELLCRM_MSCRM]
GO
/****** Object:  StoredProcedure [dbo].[usp_GetEspReqList]    Script Date: 2019-11-19 ���� 3:19:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		�̰���
-- Create date: 2019-11-15
-- Description:	e-SPOC VPN ��û ����Ʈ ��������
/*
EXEC [usp_GetEspReqList] '',''
EXEC [usp_GetEspReqList] '100000001'
EXEC [usp_GetEspReqList] 'IT Security' ,'��Ʈ��ũ' , 'VPN ������' , '��Ʈ����' ,'2019-07-24','2019-11-15'
EXEC [usp_GetEspReqList] '100000003'
EXEC [usp_GetEspReqList] ''
*/
-- =============================================
ALTER PROCEDURE [dbo].[usp_GetEspReqList]
	 @request_item NVARCHAR(100)
	,@detail_request1 NVARCHAR(100)
	,@detail_request2 NVARCHAR(100)
	,@account NVARCHAR(100)
	,@request_date_form NVARCHAR(100)
	,@request_date_to NVARCHAR(100)


AS
BEGIN
IF OBJECT_ID('tempdb..#APPROVAL1') IS NOT NULL
BEGIN DROP TABLE #APPROVAL1 END
IF OBJECT_ID('tempdb..#APPROVAL2') IS NOT NULL
BEGIN DROP TABLE #APPROVAL2 END
IF OBJECT_ID('tempdb..#new_approval') IS NOT NULL
BEGIN DROP TABLE #new_approval END


-- 2019 11 19 
IF @request_date_to IS NULL
BEGIN SET @request_date_to = CONVERT(NVARCHAR(100), CONVERT(date,GETDATE(),23)) END

IF @request_date_form IS NULL
BEGIN SET @request_date_to = '1900-01-01' END



--SET @request_item = (
--	SELECT AttributeValue FROM StringMap 
--	WHERE AttributeName = 'new_p_request_item' AND ObjectTypeCode = 10051
--	AND [Value] = @request_item
--)

--SELECT @request_date_form

-- ��û�ڰ��� ����Ʈ ����(�ӽ����̺�)
SELECT * INTO #APPROVAL1
FROM
(
       SELECT
                new_approvalId
              , new_l_it_request
              , new_p_approval_type
              , A.new_p_approval1, A.new_l_approver1Name, A.new_dt_approval1
              , A.new_p_approval2, A.new_l_approver2Name, A.new_dt_approval2
              , A.new_p_approval3, A.new_l_approver3Name, A.new_dt_approval3
              , A.new_p_approval4, A.new_l_approver4Name, A.new_dt_approval4
       FROM new_approval A
       WHERE new_p_approval_type = 100000000 --��û�ڰ���
     --  AND new_l_it_request = 'D9F81BBB-709C-E911-80E5-00155D012E07'
) T
-- ���ΰ��� ����Ʈ ����(�ӽ����̺�)
SELECT * INTO #APPROVAL2
FROM
(
       SELECT
                new_approvalId
              , new_l_it_request
              , new_p_approval_type
              , A.new_p_approval1, A.new_l_approver1Name, A.new_dt_approval1
              , A.new_p_approval2, A.new_l_approver2Name, A.new_dt_approval2
              , A.new_p_approval3, A.new_l_approver3Name, A.new_dt_approval3
       FROM new_approval A
       WHERE new_p_approval_type = 100000001 --���ΰ���
      -- AND new_l_it_request = 'D9F81BBB-709C-E911-80E5-00155D012E07'
) T
-- #APPROVAL1, #APPROVAL2 �����Ͽ� ��ħ
SELECT * INTO #new_approval
FROM
(
       SELECT
              A1.new_l_it_request
			 /*��û�� ���� ����*/
              , ISNULL(A1.new_p_approval1,'') AS '��û�ڰ���_��������å(1��)'
              , ISNULL(A1.new_l_approver1Name,'') AS '��û�ڰ���_������(1��)'
              , ISNULL(A1.new_dt_approval1,'') AS '��û�ڰ���_�����Ͻ�(1��)'
              , ISNULL(A1.new_p_approval2,'') AS '��û�ڰ���_��������å(2��)'
              , ISNULL(A1.new_l_approver2Name,'') AS '��û�ڰ���_������(2��)'
              , ISNULL(A1.new_dt_approval2,'') AS '��û�ڰ���_�����Ͻ�(2��)'
              , ISNULL(A1.new_p_approval3,'') AS '��û�ڰ���_��������å(3��)'
              , ISNULL(A1.new_l_approver3Name,'') AS '��û�ڰ���_������(3��)'
              , ISNULL(A1.new_dt_approval3,'') AS '��û�ڰ���_�����Ͻ�(3��)'
			  , ISNULL(A1.new_p_approval4,'') AS '��û�ڰ���_��������å(4��)'
              , ISNULL(A1.new_l_approver4Name,'') AS '��û�ڰ���_������(4��)'
              , ISNULL(A1.new_dt_approval4,'') AS '��û�ڰ���_�����Ͻ�(4��)'
              /*���ΰ��� ����*/
              , A2.new_p_approval1 AS '���ΰ���_��������å(1��)'
              , A2.new_l_approver1Name AS '���ΰ���_������(1��)'
              , A2.new_dt_approval1 AS '���ΰ���_�����Ͻ�(1��)'
              , A2.new_p_approval2 AS '���ΰ���_��������å(2��)'
              , A2.new_l_approver2Name AS '���ΰ���_������(2��)'
              , A2.new_dt_approval2 AS '���ΰ���_�����Ͻ�(2��)'
              , A2.new_p_approval3 AS '���ΰ���_��������å(3��)'
              , A2.new_l_approver3Name AS '���ΰ���_������(3��)'
              , A2.new_dt_approval3 AS '���ΰ���_�����Ͻ�(3��)'
       FROM #APPROVAL1 A1
       INNER JOIN #APPROVAL2 A2 ON A1.new_l_it_request = A2.new_l_it_request
) T
SELECT
       /*�������̽� �ʵ�*/
          ISNULL(I.Title,'�������') AS '���� ���̽� ����'

       /*IT��û�� �ʵ�*/
        ,ISNULL(R.new_name,'') AS '��û����ȣ'
        ,ISNULL(C.AccountIdName,'')  AS '�׷��'
		,ISNULL(p_request_item.Value,'') AS '��û���׸�'
        ,ISNULL(T.new_txt_request_type_detail1,'') AS '�󼼱���1'
        ,ISNULL(T.new_txt_request_type_detail2,'') AS '�󼼱���2'
        ,ISNULL(R.new_txt_subject,'')   AS '��û����'
        ,ISNULL(R.new_ntxt_purpose_detail,'') AS '��û����'
	    ,ISNULL(CONVERT(CHAR(10), DATEADD(HOUR, 9, R.new_dt_target_from), 23),'') AS '���Ⱓ From'
        ,ISNULL(CONVERT(CHAR(10), DATEADD(HOUR, 9, R.new_dt_target_to  ), 23),'') AS '���Ⱓ To'
		,ISNULL(CONVERT(CHAR(10), DATEADD(HOUR, 9, R.new_dt_request), 23),'') AS '��û�Ͻ�'
		,ISNULL(CONVERT(CHAR(10), DATEADD(HOUR, 9, R.new_dt_complete), 23),'') AS '�Ϸ��Ͻ�'
        ,ISNULL(C.FullName,'') AS '��'
        ,ISNULL(C.new_txt_companynum,'') AS '���'
        ,ISNULL(R.OwnerIdName,'') AS '�����'
        ,ISNULL(C.new_l_departmentName,'') AS '�Ҽ���'
      
        --,R.ModifiedOn AS '������ ��¥'
        --,R.CreatedOn AS '���糯¥'
		 
		--, A.[��û�ڰ���_������(1��)]+'-' +  CONVERT(CHAR(10),DATEADD(HOUR, 9,A.[��û�ڰ���_�����Ͻ�(1��)]),23) 
		--+' | '+A.[��û�ڰ���_������(2��)] + '-' +CONVERT(CHAR(10),DATEADD(HOUR, 9,A.[��û�ڰ���_�����Ͻ�(2��)]),23) 
		--+' | '+A.[��û�ڰ���_������(3��)] + '-' + CONVERT(CHAR(10),DATEADD(HOUR, 9,A.[��û�ڰ���_�����Ͻ�(3��)]),23)
		--+' | '+A.[��û�ڰ���_������(4��)] + '-' + CONVERT(CHAR(10),DATEADD(HOUR, 9,A.[��û�ڰ���_�����Ͻ�(4��)]),23) AS '��û�ڰ���'

		, A.[��û�ڰ���_������(1��)]
		+' | '+A.[��û�ڰ���_������(2��)]
		+' | '+A.[��û�ڰ���_������(3��)]
		+' | '+A.[��û�ڰ���_������(4��)]
		AS '��û�ڰ���'

		--, A.[���ΰ���_������(1��)] + '-' + CONVERT(CHAR(10),DATEADD(HOUR, 9,A.[���ΰ���_�����Ͻ�(1��)]), 23)  
		--+'|' + A.[���ΰ���_������(2��)] +'-' + CONVERT(CHAR(10),DATEADD(HOUR, 9,A.[���ΰ���_�����Ͻ�(2��)]),23) 
		--AS '���ΰ���'
       
	   , A.[���ΰ���_������(1��)]
		+'|' + A.[���ΰ���_������(2��)]
		AS '���ΰ���'
        
		
FROM Incident I
INNER JOIN new_it_request R ON I.new_l_it_request = R.new_it_requestId                     --IT��û��
INNER JOIN new_request_type T ON R.new_l_request_type = T.new_request_typeId       --��û��Ÿ��
INNER JOIN Contact C ON I.CustomerId = C.ContactId                                                        --��û��
INNER JOIN #new_approval A ON R.new_it_requestId = A.new_l_it_request                      --����
LEFT JOIN (SELECT ObjectTypeCode,AttributeValue,AttributeName,[Value] FROM StringMap WHERE AttributeName = 'new_p_request_item' AND ObjectTypeCode = 10051) p_request_item ON T.new_p_request_item = p_request_item.AttributeValue
WHERE 1=1
AND R.new_dt_request >= CONVERT(date,@request_date_form,23)
AND R.new_dt_complete <= CONVERT(date,@request_date_to,23)
AND T.new_p_request_item LIKE '%'+@request_item+'%'
AND T.new_txt_request_type_detail1 LIKE '%' + @detail_request1 + '%' --'��Ʈ��ũ'
AND T.new_txt_request_type_detail2 LIKE '%' + @detail_request2 + '%' --'VPN ������'
AND C.AccountId LIKE '%' + @account + '%'
--AND T.new_name = 'IT����_��Ʈ��ũ_VPN ������'
AND I.StateCode = '1' --�ذ��




IF OBJECT_ID('tempdb..#APPROVAL1') IS NOT NULL
BEGIN DROP TABLE #APPROVAL1 END
IF OBJECT_ID('tempdb..#APPROVAL2') IS NOT NULL
BEGIN DROP TABLE #APPROVAL2 END
IF OBJECT_ID('tempdb..#new_approval') IS NOT NULL
BEGIN DROP TABLE #new_approval END





END
