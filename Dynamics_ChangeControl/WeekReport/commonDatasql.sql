-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE ZSP_GET_Week_Common



AS
BEGIN
	
DECLARE @date date = '2020-02-19'
DECLARE @Value int = 1
DECLARE @BottomTeamName varchar(MAX) = 'IT������'
DECLARE @TeamMemberName varchar(MAX) = '�̰���'


IF OBJECT_ID('tempdb..#CommonData') IS NOT NULL
BEGIN DROP TABLE #APPROVAL1 END

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
			--, WRD.new_name															AS '����'
			, WRD.new_ntxt_description												AS '����'
			, WRD.new_d_input_expected_monday										AS '��(����)'
			, WRD.new_d_input_real_monday											AS '��(����)'
			, -(WRD.new_d_input_expected_monday - WRD.new_d_input_real_monday)			AS '��(����)'		
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
            --, WRD.new_name															AS '����'
            , WRD.new_ntxt_description												AS '����'
            , WRD.new_d_input_expected_monday										AS '��(����)'
            , WRD.new_d_input_real_monday											AS '��(����)'
			, -(WRD.new_d_input_expected_monday - WRD.new_d_input_real_monday)	        AS '��(����)'
            , WRD.new_d_input_expected_tuesday										AS 'ȭ(����)'
            , WRD.new_d_input_real_tuesday											AS 'ȭ(����)'
			, -(WRD.new_d_input_expected_tuesday - WRD.new_d_input_real_tuesday)       AS 'ȭ(����)'											 
            , WRD.new_d_input_expected_wednesday									AS '��(����)'
            , WRD.new_d_input_real_wednesday										AS '��(����)'
            , -(WRD.new_d_input_expected_wednesday - WRD.new_d_input_real_wednesday)	AS '��(����)'
			, WRD.new_d_input_expected_thursday										AS '��(����)'
            , WRD.new_d_input_real_thursday											AS '��(����)'
            , -(WRD.new_d_input_expected_thursday - WRD.new_d_input_real_thursday)   AS '��(����)'
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

-- Date�� ���ؼ� ���� �ָ� �Ǵ� 
DECLARE @last_date date = DATEADD(dd,-7,@date)

--�ְ���������
IF(@Value = '1')
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
FROM #CommonData CD
WHERE 1=1
AND (@date BETWEEN CONVERT(DATE,-15,CD.���۳�¥) AND CONVERT(DATE,DATEADD(HOUR,33,CD.����¥),23 )
OR	 @last_date BETWEEN CONVERT(DATE, DATEADD(HOUR, -15, CD.���۳�¥ ), 23) AND CONVERT(DATE, DATEADD(HOUR, 33, CD.����¥), 23))

END
----������������


ELSE IF(@Value ='2')

SELECT 
		CD.�����
		,CD.��
		,CD.����
		,CD.��з�
		,CD.�ߺз�
		,CD.�Һз�
		--���� �κ��� �Ǵ��� Ȯ�� �غ� �� 
		,SUM(CD.[�հ�(����)])
		,SUM(CD.[�հ�(����)])
		,SUM(CD.[�հ�(����)])
FROM #CommonData CD		   
GROUP BY CD.����� , CD.��


BEGIN

--END
----������������ 
--ELSE IF(@Value ='3')
--BEGIN

--END

 DROP TABLE #CommonData END

	
	
END
GO
