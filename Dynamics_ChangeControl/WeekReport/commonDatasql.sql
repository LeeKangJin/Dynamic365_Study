USE [CELLCRMDEV_MSCRM]
GO
/****** Object:  StoredProcedure [dbo].[ZSP_GET_Core_Report]    Script Date: 2020-02-27 ���� 1:44:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[ZSP_GET_Core_Report]
	  @Value int 
	 ,@date date
	 ,@BottomTeamName VARCHAR(MAX)
	 ,@TeamMemberName VARCHAR(MAX)
	 
-- EXEC [dbo].[ZSP_GET_Month_Report] '1', '20-02-03' ,'IT������',''
AS
BEGIN
	SET NOCOUNT ON;

--DECLARE @date date = '2020-02-21'
--DECLARE @Value int = 2
--DECLARE @BottomTeamName varchar(MAX) = 'IT������'
--DECLARE @TeamMemberName varchar(MAX) = '�� ����' --  CRM
--DECLARE @TeamMemberName varchar(MAX) = '# �̰���' -- CRMDEV


IF OBJECT_ID('tempdb..#CommonData') IS NOT NULL
BEGIN DROP TABLE #CommonData END

SELECT * INTO #CommonData
FROM 
(

SELECT		  WRD.OwnerIdName													    AS '�����'
			, SU.BusinessUnitIdName													AS '�μ�'
			, '���� Master'															AS '����'
			, CASE WHEN WRD.new_p_check = 100000000 THEN '������' ELSE '�Ϸ�' END	AS '�ϷῩ��'
			, WM3.new_l_work_master1Name											AS '��з�'
			, WM3.new_l_work_master2Name											AS '�ߺз�'
			, WM3.new_name															AS '�Һз�'
			, WRD.new_ntxt_description												AS '����'
			, WRD.new_d_input_expected_monday										AS '��(����)'
			, WRD.new_d_input_real_monday											AS '��(����)'
			, -(WRD.new_d_input_expected_monday - WRD.new_d_input_real_monday)		AS '��(����)'		
			, WRD.new_d_input_expected_tuesday										AS 'ȭ(����)'
			, WRD.new_d_input_real_tuesday											AS 'ȭ(����)'
			, -(WRD.new_d_input_expected_tuesday - WRD.new_d_input_real_tuesday)		AS 'ȭ(����)'
			, WRD.new_d_input_expected_wednesday									AS '��(����)'
			, WRD.new_d_input_real_wednesday										AS '��(����)'
			, -(WRD.new_d_input_expected_wednesday - WRD.new_d_input_real_wednesday)	AS '��(����)' 
			, WRD.new_d_input_expected_thursday										AS '��(����)'
			, WRD.new_d_input_real_thursday											AS '��(����)'
			, -(WRD.new_d_input_expected_thursday - WRD.new_d_input_real_thursday)	AS '��(����)' 
			, WRD.new_d_input_expected_friday										AS '��(����)'
			, WRD.new_d_input_real_friday											AS '��(����)'
			, -(WRD.new_d_input_expected_friday - WRD.new_d_input_real_friday)		AS '��(����)' 
			, WRD.new_d_expected_sum												AS '�հ�(����)'
			, WRD.new_d_real_sum													AS '�հ�(����)'
			, -(WRD.new_d_expected_sum - WRD.new_d_real_sum)						AS '�հ�(����)'
			, WR.new_name														    AS '����'
			, W.new_dt_start														AS '���۳�¥'
			, W.new_dt_end															AS '����¥'
			, W.new_p_year															AS '��'
			, W.new_p_month															AS '��'
			, W.new_p_week															AS '��'
			,ISNULL(WR.new_txt_remark,' ')											AS '���'
			,WM3.new_txt_code														AS '�����ڵ�'
FROM		new_weekly_report_detail	WRD
INNER JOIN	new_work_master3			WM3	ON WRD.new_l_work_master3 = WM3.new_work_master3Id
INNER JOIN	new_weekly_report			WR  ON WR.new_weekly_reportId = WRD.new_l_weekly_report
INNER JOIN	systemuser					SU  ON SU.SystemUserId = WRD.OwnerId
INNER JOIN	new_week					W   ON WR.new_l_week = W.new_weekId
WHERE		1=1
AND			WRD.new_p_division = 100000000
-- ������ �� , �ְ��������� ���ܿ� �ʵ� ( 1�Ͻ� �ְ����� ���� ��� ) 
AND         SU.new_p_reportyn = 1
AND			(@TeamMemberName = '' OR WRD.OwnerIdName = @TeamMemberName)
AND		    SU.BusinessUnitIdName = @BottomTeamName

UNION ALL

-- ������Ʈ (���� 0 ) 
SELECT		WRD.OwnerIdName AS '�����'
			, SU.BusinessUnitIdName AS '�μ�'
            , '������Ʈ' AS '����'
			, CASE WHEN WRD.new_p_check = 100000000 THEN '������' ELSE '�Ϸ�' END	AS '�ϷῩ��'
            , CASE WHEN P.new_p_actiontype = 100000000 THEN '����' ELSE '����' END	AS '��з�' -- 100000000 ���� 100000001 ����
            , PD.new_l_projectName													AS '�ߺз�'
            , PD.new_name															AS '�Һз�'
            , WRD.new_ntxt_description												AS '����'
            , WRD.new_d_input_expected_monday										AS '��(����)'
            , WRD.new_d_input_real_monday											AS '��(����)'
			, -(WRD.new_d_input_expected_monday - WRD.new_d_input_real_monday)	    AS '��(����)'
            , WRD.new_d_input_expected_tuesday										AS 'ȭ(����)'
            , WRD.new_d_input_real_tuesday											AS 'ȭ(����)'
			, -(WRD.new_d_input_expected_tuesday - WRD.new_d_input_real_tuesday)     AS 'ȭ(����)'											 
            , WRD.new_d_input_expected_wednesday									AS '��(����)'
            , WRD.new_d_input_real_wednesday										AS '��(����)'
            , -(WRD.new_d_input_expected_wednesday - WRD.new_d_input_real_wednesday)	AS '��(����)'
			, WRD.new_d_input_expected_thursday										AS '��(����)'
            , WRD.new_d_input_real_thursday											AS '��(����)'
            , -(WRD.new_d_input_expected_thursday - WRD.new_d_input_real_thursday)  AS '��(����)'
			, WRD.new_d_input_expected_friday										AS '��(����)'
            , WRD.new_d_input_real_friday											AS '��(����)'
            , -(WRD.new_d_input_expected_friday -  WRD.new_d_input_real_friday)     AS '��(����)'
			, WRD.new_d_expected_sum												AS '�հ�(����)'
            , WRD.new_d_real_sum													AS '�հ�(����)'
			, -(WRD.new_d_expected_sum - WRD.new_d_real_sum)                        AS '�հ�(����)'
			, WR.new_name															AS '����'
			, W.new_dt_start														AS '���۳�¥'
			, W.new_dt_end															AS '����¥'
			, W.new_p_year															AS '��'
			, W.new_p_month															AS '��'
			, W.new_p_week															AS '��'
			,ISNULL(WR.new_txt_remark,' ')											AS '���'
			,P.new_txt_code															AS '�����ڵ�'
FROM		new_weekly_report_detail	WRD
INNER JOIN	new_projectdetail			PD	ON WRD.new_l_related_project_detail = PD.new_projectdetailId
INNER JOIN	new_project					P	ON PD.new_l_project = P.new_projectId
INNER JOIN	new_weekly_report			WR  ON WR.new_weekly_reportId = WRD.new_l_weekly_report
INNER JOIN  systemuser					SU  ON SU.SystemUserId = WRD.OwnerId
INNER JOIN  new_week					W   ON WR.new_l_week = W.new_weekId
WHERE		1=1
AND			WRD.new_p_division = 100000001
-- ������ �� , �ְ��������� ���ܿ� �ʵ� ( 1�Ͻ� �ְ����� ���� ��� ) 
AND         SU.new_p_reportyn = 1
AND			(@TeamMemberName = '' OR WRD.OwnerIdName = @TeamMemberName)
AND		    SU.BusinessUnitIdName = @BottomTeamName
) T
ORDER BY �����ڵ�

--DEBugging


-- Date�� ���ؼ� ���� �ָ� �Ǵ� 
DECLARE @last_date date = DATEADD(dd,-7,@date)
DECLARE @year int = YEAR(@date)
DECLARE @month int =MONTH(@date)





--�ְ���������
IF(@Value = 1)
BEGIN
SELECT 
			  CD.�����
			 ,CASE WHEN @date BETWEEN CONVERT(DATE, DATEADD(HOUR, 9, CD.���۳�¥ ), 23) AND CONVERT(DATE, DATEADD(HOUR, 9, CD.����¥), 23) THEN '�̹���' ELSE '������' END AS '��'
			 ,CD.�μ�
			 ,CD.����
			 ,CD.�ϷῩ��
			 ,CD.��з�
             ,CD.�ߺз�
			 ,CD.�Һз�
			 ,CD.����
			 ,CD.[��(����)]
			 ,CD.[��(����)]
			 ,CD.[ȭ(����)]
			 ,CD.[ȭ(����)]
			 ,CD.[��(����)]
			 ,CD.[��(����)]
			 ,CD.[��(����)]
			 ,CD.[��(����)]
			 ,CD.[��(����)]
			 ,CD.[��(����)]
			 ,CD.[�հ�(����)]
			 ,CD.[�հ�(����)]
			 ,CD.[�հ�(����)]
			 ,CD.����
			 ,CD.���
			 ,SUM(CD.[�հ�(����)])
FROM #CommonData CD
GROUP BY CD.�����
HAVING 1=1
AND (@date BETWEEN CONVERT(DATE, DATEADD(HOUR, -15, CD.���۳�¥ ), 23) AND CONVERT(DATE, DATEADD(HOUR, -15, CD.����¥), 23) )
OR	 (@last_date BETWEEN CONVERT(DATE, DATEADD(HOUR, 33, CD.���۳�¥ ), 23) AND CONVERT(DATE, DATEADD(HOUR, 33, CD.����¥), 23))



END
----������������

ELSE IF(@Value = 2)
BEGIN


SELECT
	 DENSE_RANK() OVER (PARTITION BY  CD.����� ORDER BY (SUM(T2.[�հ�(����)]) )  desc) as [RANK]
	,CD.�����            AS '�����'
	,CD.��
	,CD.�� - 100000000 +1 AS '��'
	,CD.����
	,CD.��з�
	,CD.�ߺз�
	,CD.�Һз�
	,SUM(CD.[�հ�(����)]) AS '�հ�(����)'
	,SUM(CD.[�հ�(����)]) AS '�հ�(����)'
	,SUM(CD.[�հ�(����)]) AS '�հ�(����)'
    ,T.[�������հ�(����)] AS '���߰���'
	,CASE WHEN T.[�������հ�(����)] = 0 THEN 0 ELSE (SUM(CD.[�հ�(����)]) ) / (T.[�������հ�(����)]) END AS '���� ����'
	 FROM #CommonData CD
   --JOIN (SELECT AttributeName,[Value],* FROM StringMap WHERE (AttributeName = 'new_p_week' AND ObjectTypeCode = 10091) ON (CD.��= AttributeValue))
   -- ���� �� �հ踦 ���� Join.
   JOIN (
   SELECT 
		CD2.����� AS '�����'
		,SUM(CD2.[�հ�(����)]) AS '�������հ�(����)'
		 FROM #CommonData CD2
		 GROUP BY CD2.��, CD2.�� ,CD2.�����
		 HAVING 1=1
		 -- @year�� OptionSetValue Example --- Lable : Value = 2020 : 100002020
		 AND @year = CD2.��- 100000000 
		 -- @month�� OptionSetValue Example --- Lable : Value = 1 : 1
		 AND @month = CD2.�� 
   )  T ON ( CD.����� = T.����� )
   LEFT JOIN 
   (SELECT
		 CD.�����            AS '�����'
		,CD.��з�			 AS '��з�'
		,CD.�ߺз�           AS '�ߺз�'
		,CD.�Һз�           AS '�Һз�'
		,SUM(CD.[�հ�(����)]) AS '�հ�(����)'

		FROM #CommonData CD
		GROUP BY  CD.��,CD.��, CD.����� ,CD.��з�,CD.�ߺз�,CD.�Һз� 
		HAVING 1=1
		AND @year = CD.��- 100000000 
		AND @month = CD.�� 
		) T2 ON (CD.����� = T2.����� AND CD.�Һз� = T2.�Һз� AND CD.��з� = T2.��з� AND CD.�ߺз� = T2.�ߺз�)
   GROUP BY  CD.��,CD.��, CD.����� ,CD.��,CD.����, CD.��з�, CD.�ߺз�, CD.�Һз� ,T.[�������հ�(����)] 
   HAVING 1=1
   AND @year = CD.��- 100000000 
   AND @month = CD.�� 

    --Lable : Value �ϵ��ڵ��� �����ϰ��� �ϸ� �Ʒ��� String Map�� ����Ұ�. 
    --JOIN ���� ��� Key - Value �����ϴ� �ϰ� ����
    --INNOR JOIN (SELECT AttributeName,[Value] FROM StringMap WHERE (AttributeName = 'new_p_year' AND ObjectTypeCode = 10043) temp ON (new_week.new_p_year= temp.AttributeValue)
   
END
----������������ 
ELSE IF(@Value ='3')
BEGIN
SELECT 
		 CD.�����
		,CD.��
		,CD.����
		,CD.��з�
		,CD.�ߺз�
		,CD.�Һз�
		--���� �κ��� �Ǵ��� Ȯ�� �غ� �� 
		,SUM(CD.[�հ�(����)]) AS '�հ�(����)'
		,SUM(CD.[�հ�(����)]) AS '�հ�(����)'
		,SUM(CD.[�հ�(����)]) AS '�հ�(����)'
FROM #CommonData CD		   
JOIN (
   SELECT 
	CD2.����� AS '�����'
	,SUM(CD2.[�հ�(����)]) AS '�������հ�(����)'
	 FROM #CommonData CD2
	 GROUP BY CD2.��,CD2.�����
     HAVING 1=1
	AND CD2.�� -100000000 = @year  
   )  T ON ( CD.����� = T.�����)
GROUP BY CD.��,CD.����� ,CD.��,CD.����, CD.��з�, CD.�ߺз�, CD.�Һз�
HAVING 1=1
AND CD.�� -100000000 = @year  
AND  CD.�� <=@month 



END

IF OBJECT_ID('tempdb..#CommonData') IS NOT NULL
BEGIN DROP TABLE #CommonData END
END



