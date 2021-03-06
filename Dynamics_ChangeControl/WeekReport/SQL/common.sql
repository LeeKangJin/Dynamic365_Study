IF OBJECT_ID('tempdb..#CommonData') IS NOT NULL
BEGIN DROP TABLE #APPROVAL1 END

DECLARE @date date = '2020-02-19'
DECLARE @BottomTeamName varchar(MAX) = 'IT개발팀'
DECLARE @TeamMemberName varchar(MAX) = '이강진'



SELECT * INTO #CommonData
FROM (
	(
SELECT		  WRD.OwnerIdName															AS '담당자'
			, SU.BusinessUnitIdName													AS '부서'
			, '업무 Master'															AS '구분'
			, CASE WHEN WRD.new_p_check = 100000000 THEN '진행중' ELSE '완료' END	AS '완료여부'
			, WM3.new_l_work_master1Name											AS '대분류'
			, WM3.new_l_work_master2Name											AS '중분류'
			, WM3.new_name															AS '소분류'
			--, WRD.new_name															AS '내용'
			, WRD.new_ntxt_description												AS '내용'
			, WRD.new_d_input_expected_monday										AS '월(예상)'
			, WRD.new_d_input_real_monday											AS '월(실적)'
			, -(WRD.new_d_input_expected_monday - WRD.new_d_input_real_monday)			AS '월(차이)'		
			, WRD.new_d_input_expected_tuesday										AS '화(예상)'
			, WRD.new_d_input_real_tuesday											AS '화(실적)'
			, -(WRD.new_d_input_expected_tuesday - WRD.new_d_input_real_tuesday)		AS '화(차이)'
			, WRD.new_d_input_expected_wednesday									AS '수(예상)'
			, WRD.new_d_input_real_wednesday										AS '수(실적)'
			, -(WRD.new_d_input_expected_wednesday - WRD.new_d_input_real_wednesday)	AS '수(차이)' 
			, WRD.new_d_input_expected_thursday										AS '목(예상)'
			, WRD.new_d_input_real_thursday											AS '목(실적)'
			, -(WRD.new_d_input_expected_thursday - WRD.new_d_input_real_thursday)	AS '목(차이)' 
			, WRD.new_d_input_expected_friday										AS '금(예상)'
			, WRD.new_d_input_real_friday											AS '금(실적)'
			, -(WRD.new_d_input_expected_friday - WRD.new_d_input_real_friday)		AS '금(차이)' 
			, WRD.new_d_expected_sum												AS '합계(예상)'
			, WRD.new_d_real_sum													AS '합계(실적)'
			, -(WRD.new_d_expected_sum - WRD.new_d_real_sum)						AS '합계(차이)'
			, WR.new_name														    AS '주차'
			
			, ISNULL(WR.new_txt_remark,' ')  AS '비고'
FROM		new_weekly_report_detail	WRD
INNER JOIN	new_work_master3			WM3	ON WRD.new_l_work_master3 = WM3.new_work_master3Id
INNER JOIN	new_weekly_report			WR  ON WR.new_weekly_reportId = WRD.new_l_weekly_report
INNER JOIN	systemuser					SU  ON SU.SystemUserId = WRD.OwnerId
INNER JOIN	new_week					W   ON WR.new_l_week = W.new_weekId
WHERE		1=1
AND			WRD.new_p_division = 100000000
AND		    SU.BusinessUnitIdName = @BottomTeamName
AND			(@TeamMemberName = '' OR WRD.OwnerIdName = @TeamMemberName)
-- 시간 하루씩 +-
AND			(@date BETWEEN CONVERT(DATE, DATEADD(HOUR, -15, W.new_dt_start), 23) AND CONVERT(DATE, DATEADD(HOUR, 33, W.new_dt_end), 23)
OR	         @last_date BETWEEN CONVERT(DATE, DATEADD(HOUR, -15, W.new_dt_start), 23) AND CONVERT(DATE, DATEADD(HOUR, 33, W.new_dt_end), 23))
AND         SU.new_p_reportyn = 1

UNION ALL

-- 프로젝트 (구분 0 ) 
SELECT		WRD.OwnerIdName AS '담당자'
			, CASE WHEN @date BETWEEN CONVERT(DATE, DATEADD(HOUR, 9, W.new_dt_start), 23) AND CONVERT(DATE, DATEADD(HOUR, 9, W.new_dt_end), 23) THEN '이번주' ELSE '지난주' END AS '주'
			, SU.BusinessUnitIdName AS '부서'
            , '프로젝트' AS '구분'
			, CASE WHEN WRD.new_p_check = 100000000 THEN '진행중' ELSE '완료' END	AS '완료여부'
            , CASE WHEN P.new_p_actiontype = 100000000 THEN '직접' ELSE '지원' END	AS '대분류' -- 100000000 직접 100000001 지원
            , PD.new_l_projectName													AS '중분류'
            , PD.new_name															AS '소분류'
            --, WRD.new_name															AS '내용'
            , WRD.new_ntxt_description												AS '내용'
            , WRD.new_d_input_expected_monday										AS '월(예상)'
            , WRD.new_d_input_real_monday											AS '월(실적)'
			, -(WRD.new_d_input_expected_monday - WRD.new_d_input_real_monday)	        AS '월(차이)'
            , WRD.new_d_input_expected_tuesday										AS '화(예상)'
            , WRD.new_d_input_real_tuesday											AS '화(실적)'
			, -(WRD.new_d_input_expected_tuesday - WRD.new_d_input_real_tuesday)       AS '화(차이)'											 
            , WRD.new_d_input_expected_wednesday									AS '수(예상)'
            , WRD.new_d_input_real_wednesday										AS '수(실적)'
            , -(WRD.new_d_input_expected_wednesday - WRD.new_d_input_real_wednesday)	AS '수(차이)'
			, WRD.new_d_input_expected_thursday										AS '목(예상)'
            , WRD.new_d_input_real_thursday											AS '목(실적)'
            , -(WRD.new_d_input_expected_thursday - WRD.new_d_input_real_thursday)   AS '목(차이)'
			, WRD.new_d_input_expected_friday										AS '금(예상)'
            , WRD.new_d_input_real_friday											AS '금(실적)'
            , -(WRD.new_d_input_expected_friday -  WRD.new_d_input_real_friday)     AS '금(차이)'
			, WRD.new_d_expected_sum												AS '합계(예상)'
            , WRD.new_d_real_sum													AS '합계(실적)'
			, -(WRD.new_d_expected_sum - WRD.new_d_real_sum)                        AS '합계(차이)'
			, Case when WRD.new_d_expected_sum = 0 and WRD.new_d_real_sum = 0 then 0
			when  WRD.new_d_expected_sum = 0 and WRD.new_d_real_sum != 0 then WRD.new_d_expected_sum else
			CAST(CAST(WRD.new_d_real_sum as float) / CAST(WRD.new_d_expected_sum as float)  AS float) end
			AS '달성율(%)'
			, WR.new_name															AS '주차'
			,ISNULL(WR.new_txt_remark,' ')  AS '비고'
FROM		new_weekly_report_detail	WRD
INNER JOIN	new_projectdetail			PD	ON WRD.new_l_related_project_detail = PD.new_projectdetailId
INNER JOIN	new_project					P	ON PD.new_l_project = P.new_projectId
INNER JOIN	new_weekly_report			WR  ON WR.new_weekly_reportId = WRD.new_l_weekly_report
INNER JOIN  systemuser					SU  ON SU.SystemUserId = WRD.OwnerId
INNER JOIN  new_week					W   ON WR.new_l_week = W.new_weekId




) T




IF OBJECT_ID('tempdb..#CommonData') IS NOT NULL
BEGIN DROP TABLE #APPROVAL1 END