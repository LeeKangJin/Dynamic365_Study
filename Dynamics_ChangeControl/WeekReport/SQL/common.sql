IF OBJECT_ID('tempdb..#CommonData') IS NOT NULL
BEGIN DROP TABLE #APPROVAL1 END

DECLARE @date date = '2020-02-19'
DECLARE @BottomTeamName varchar(MAX) = 'IT������'
DECLARE @TeamMemberName varchar(MAX) = '�̰���'



SELECT * INTO #CommonData
FROM (
	(
SELECT		  WRD.OwnerIdName															AS '�����'
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
			
			, ISNULL(WR.new_txt_remark,' ')  AS '���'
FROM		new_weekly_report_detail	WRD
INNER JOIN	new_work_master3			WM3	ON WRD.new_l_work_master3 = WM3.new_work_master3Id
INNER JOIN	new_weekly_report			WR  ON WR.new_weekly_reportId = WRD.new_l_weekly_report
INNER JOIN	systemuser					SU  ON SU.SystemUserId = WRD.OwnerId
INNER JOIN	new_week					W   ON WR.new_l_week = W.new_weekId
WHERE		1=1
AND			WRD.new_p_division = 100000000
AND		    SU.BusinessUnitIdName = @BottomTeamName
AND			(@TeamMemberName = '' OR WRD.OwnerIdName = @TeamMemberName)
-- �ð� �Ϸ羿 +-
AND			(@date BETWEEN CONVERT(DATE, DATEADD(HOUR, -15, W.new_dt_start), 23) AND CONVERT(DATE, DATEADD(HOUR, 33, W.new_dt_end), 23)
OR	         @last_date BETWEEN CONVERT(DATE, DATEADD(HOUR, -15, W.new_dt_start), 23) AND CONVERT(DATE, DATEADD(HOUR, 33, W.new_dt_end), 23))
AND         SU.new_p_reportyn = 1

UNION ALL

-- ������Ʈ (���� 0 ) 
SELECT		WRD.OwnerIdName AS '�����'
			, CASE WHEN @date BETWEEN CONVERT(DATE, DATEADD(HOUR, 9, W.new_dt_start), 23) AND CONVERT(DATE, DATEADD(HOUR, 9, W.new_dt_end), 23) THEN '�̹���' ELSE '������' END AS '��'
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
			, Case when WRD.new_d_expected_sum = 0 and WRD.new_d_real_sum = 0 then 0
			when  WRD.new_d_expected_sum = 0 and WRD.new_d_real_sum != 0 then WRD.new_d_expected_sum else
			CAST(CAST(WRD.new_d_real_sum as float) / CAST(WRD.new_d_expected_sum as float)  AS float) end
			AS '�޼���(%)'
			, WR.new_name															AS '����'
			,ISNULL(WR.new_txt_remark,' ')  AS '���'
FROM		new_weekly_report_detail	WRD
INNER JOIN	new_projectdetail			PD	ON WRD.new_l_related_project_detail = PD.new_projectdetailId
INNER JOIN	new_project					P	ON PD.new_l_project = P.new_projectId
INNER JOIN	new_weekly_report			WR  ON WR.new_weekly_reportId = WRD.new_l_weekly_report
INNER JOIN  systemuser					SU  ON SU.SystemUserId = WRD.OwnerId
INNER JOIN  new_week					W   ON WR.new_l_week = W.new_weekId




) T




IF OBJECT_ID('tempdb..#CommonData') IS NOT NULL
BEGIN DROP TABLE #APPROVAL1 END