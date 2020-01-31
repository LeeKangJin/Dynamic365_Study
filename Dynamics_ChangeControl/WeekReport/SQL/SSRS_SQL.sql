/*
- 업무마스터(소) 의 중/대를 가져올 것
- 주간업무상세 필드추가
-> 상태(진행중/완료)
- SELECT 추가
-> 주차 정보(Ex. 2020년 1월 1주차)
-> 차이
-> %정보
*/

-- 업무 마스터 (구분 1)
SELECT         WRD.CreatedByName AS '담당자'
                       ,'1' AS '구분'
                       ,WM1.new_name   AS '대분류'
                       ,WM2.new_name   AS '중분류'
                       ,WM3.new_name   AS '소분류'
                       ,WRD.new_name AS '제목'
                       , WRD.new_ntxt_description AS '내용'
                       --,'진행중' AS '진행현황'
                       ,WRD.new_d_input_expected_monday         AS '월(예상)'
                       ,WRD.new_d_input_real_monday                    AS '월(실적)'
                       ,WRD.new_d_input_expected_tuesday        AS '화(예상)'
                       ,WRD.new_d_input_real_tuesday                   AS '화(실적)'
                       ,WRD.new_d_input_expected_wednesday              AS '수(예상)'
                       ,WRD.new_d_input_real_wednesday                 AS '수(실적)'
                       ,WRD.new_d_input_expected_thursday               AS '목(예상)'
                       ,WRD.new_d_input_real_thursday                  AS '목(실적)'
                       ,WRD.new_d_input_expected_friday         AS '금(예상)'
                       ,WRD.new_d_input_real_friday                    AS '금(실적)'
                       ,WRD.new_d_expected_sum                                AS '합계(예상)'
                       ,WRD.new_d_real_sum                                           AS '합계(실적)'
FROM           new_weekly_report_detail WRD
INNER JOIN      new_work_master1 WM1    ON WRD.new_l_work_master1 = WM1.new_work_master1Id
INNER JOIN      new_work_master2 WM2    ON WRD.new_l_work_master2 = WM2.new_work_master2Id 
INNER JOIN      new_work_master3 WM3    ON WRD.new_l_work_master3 = WM3.new_work_master3Id
WHERE   WRD.new_p_division = 100000000

UNION ALL

-- 프로젝트 (구분 0 ) 
SELECT         WRD.CreatedByName AS '담당자'
                       , '0' AS '구분'
                       ,CASE WHEN P.new_p_actiontype = 100000000 THEN '직접' ELSE '지원' END AS '대분류' -- 100000000 직접 100000001 지원
                       ,PD.new_l_projectName AS '중분류'
                       ,PD.new_name AS '소분류'
                       , WRD.new_name AS '제목'
                       , WRD.new_ntxt_description AS '내용'
                       --,CASE WHEN PD.new_p_statement = 100000004 THEN '완료' ELSE '진행중' END AS  '진행현황' -- 완료 개시 구현 ..
                       , WRD.new_d_input_expected_monday        AS '월(예상)'
                       , WRD.new_d_input_real_monday                   AS '월(실적)'
                       , WRD.new_d_input_expected_tuesday               AS '화(예상)'
                       , WRD.new_d_input_real_tuesday                  AS '화(실적)'
                       , WRD.new_d_input_expected_wednesday     AS '수(예상)'
                       , WRD.new_d_input_real_wednesday         AS '수(실적)'
                       , WRD.new_d_input_expected_thursday              AS '목(예상)'
                       , WRD.new_d_input_real_thursday                 AS '목(실적)'
                       , WRD.new_d_input_expected_friday        AS '금(예상)'
                       , WRD.new_d_input_real_friday                   AS '금(실적)'
                       , WRD.new_d_expected_sum                        AS '합계(예상)'
                       , WRD.new_d_real_sum                                   AS '합계(실적)'
FROM           new_weekly_report_detail WRD
INNER JOIN new_projectdetail PD ON WRD.new_l_related_project_detail = PD.new_projectdetailId
INNER JOIN  new_project P ON  PD.new_l_project = P.new_projectId
WHERE   WRD.new_p_division = 100000001
