/*
- 담당자 -> SSRS 처리? 

-> 상태(진행중/완료) : 실적 입력시 완료 치는 코드 옵션으로 추가 

- SELECT 추가

- 팀 추가 

-> %정보 -> 대리님 결정 필요 

*/



-- 업무 마스터 (구분 1)
SELECT					WRD.CreatedByName AS '담당자'
                       ,'업무 Master' AS '구분'
					   ,WRD.new_p_check AS '완료여부'
                       ,WM3.new_l_work_master1Name   AS '대분류'
                       ,WM3.new_l_work_master2Name   AS '중분류'
                       ,WM3.new_name   AS '소분류'
                       ,WRD.new_name AS '제목'
                       , WRD.new_ntxt_description AS '내용'
                       ,WRD.new_d_input_expected_monday										AS '월(예상)'
                       ,WRD.new_d_input_real_monday											AS '월(실적)'
					   ,WRD.new_d_input_expected_monday - WRD.new_d_input_real_monday		AS '월(차이)'		
                       ,WRD.new_d_input_expected_tuesday									AS '화(예상)'
                       ,WRD.new_d_input_real_tuesday										AS '화(실적)'
					   ,WRD.new_d_input_expected_tuesday - WRD.new_d_input_real_tuesday     AS '화(차이)'
                       ,WRD.new_d_input_expected_wednesday									AS '수(예상)'
                       ,WRD.new_d_input_real_wednesday										AS '수(실적)'
                       ,WRD.new_d_input_expected_wednesday - WRD.new_d_input_real_wednesday AS '수(차이)' 
					   ,WRD.new_d_input_expected_thursday									AS '목(예상)'
                       ,WRD.new_d_input_real_thursday										AS '목(실적)'
                       ,WRD.new_d_input_expected_thursday - WRD.new_d_input_real_thursday   AS '목(차이)' 
					   ,WRD.new_d_input_expected_friday										AS '금(예상)'
                       ,WRD.new_d_input_real_friday											AS '금(실적)'
                       ,WRD.new_d_input_expected_friday - WRD.new_d_input_real_friday       AS '금(차이)' 
					   ,WRD.new_d_expected_sum												AS '합계(예상)'
                       ,WRD.new_d_real_sum													AS '합계(실적)'
					   ,WRD.new_d_expected_sum - WRD.new_d_real_sum                         AS '합계(차이)'
					   ,WR.new_name														    AS '주차'
FROM            new_weekly_report_detail WRD
INNER JOIN      new_work_master3  WM3    ON WRD.new_l_work_master3 = WM3.new_work_master3Id
INNER JOIN      new_weekly_report WR     ON WR.new_weekly_reportId = WRD.new_l_weekly_report
WHERE   WRD.new_p_division = 100000000


UNION ALL

-- 프로젝트 (구분 0 ) 
SELECT         WRD.CreatedByName AS '담당자'
                       , '프로젝트' AS '구분'
                       ,CASE WHEN P.new_p_actiontype = 100000000 THEN '직접' ELSE '지원' END AS '대분류' -- 100000000 직접 100000001 지원
                       ,PD.new_l_projectName AS '중분류'
                       ,PD.new_name AS '소분류'
                       , WRD.new_name AS '제목'
                       , WRD.new_ntxt_description AS '내용'
                       , WRD.new_d_input_expected_monday										 AS '월(예상)'
                       , WRD.new_d_input_real_monday											 AS '월(실적)'
					   , WRD.new_d_input_expected_monday - WRD.new_d_input_real_monday	         AS '월(차이)'
                       , WRD.new_d_input_expected_tuesday										 AS '화(예상)'
                       , WRD.new_d_input_real_tuesday											 AS '화(실적)'
					   , WRD.new_d_input_expected_tuesday - WRD.new_d_input_real_tuesday         AS '화(차이)'											 
                       , WRD.new_d_input_expected_wednesday										 AS '수(예상)'
                       , WRD.new_d_input_real_wednesday											 AS '수(실적)'
                       , WRD.new_d_input_expected_wednesday - WRD.new_d_input_real_wednesday     AS '수(차이)'
					   , WRD.new_d_input_expected_thursday										 AS '목(예상)'
                       , WRD.new_d_input_real_thursday											 AS '목(실적)'
                       , WRD.new_d_input_expected_thursday - WRD.new_d_input_real_thursday       AS '목(차이)'
					   , WRD.new_d_input_expected_friday										 AS '금(예상)'
                       , WRD.new_d_input_real_friday											 AS '금(실적)'
                       , WRD.new_d_input_expected_friday -  WRD.new_d_input_real_friday          AS '금(차이)'
					   , WRD.new_d_expected_sum													 AS '합계(예상)'
                       , WRD.new_d_real_sum													     AS '합계(실적)'
					   , WRD.new_d_expected_sum - WRD.new_d_real_sum                             AS '합계(차이)'
					   , WR.new_name															 AS '주차'
FROM           new_weekly_report_detail WRD
INNER JOIN new_projectdetail PD ON WRD.new_l_related_project_detail = PD.new_projectdetailId
INNER JOIN new_project P ON  PD.new_l_project = P.new_projectId
INNER JOIN new_weekly_report WR     ON WR.new_weekly_reportId = WRD.new_l_weekly_report

WHERE   WRD.new_p_division = 100000001
