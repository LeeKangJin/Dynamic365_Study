USE [CELLCRMDEV_MSCRM]
GO
/****** Object:  StoredProcedure [dbo].[ZSP_GET_Core_Report]    Script Date: 2020-02-27 오후 1:44:44 ******/
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
	 
-- EXEC [dbo].[ZSP_GET_Month_Report] '1', '20-02-03' ,'IT개발팀',''
AS
BEGIN
	SET NOCOUNT ON;

--DECLARE @date date = '2020-02-21'
--DECLARE @Value int = 2
--DECLARE @BottomTeamName varchar(MAX) = 'IT개발팀'
--DECLARE @TeamMemberName varchar(MAX) = '이 강진' --  CRM
--DECLARE @TeamMemberName varchar(MAX) = '# 이강진' -- CRMDEV


IF OBJECT_ID('tempdb..#CommonData') IS NOT NULL
BEGIN DROP TABLE #CommonData END

SELECT * INTO #CommonData
FROM 
(

SELECT		  WRD.OwnerIdName													    AS '담당자'
			, SU.BusinessUnitIdName													AS '부서'
			, '업무 Master'															AS '구분'
			, CASE WHEN WRD.new_p_check = 100000000 THEN '진행중' ELSE '완료' END	AS '완료여부'
			, WM3.new_l_work_master1Name											AS '대분류'
			, WM3.new_l_work_master2Name											AS '중분류'
			, WM3.new_name															AS '소분류'
			, WRD.new_ntxt_description												AS '내용'
			, WRD.new_d_input_expected_monday										AS '월(예상)'
			, WRD.new_d_input_real_monday											AS '월(실적)'
			, -(WRD.new_d_input_expected_monday - WRD.new_d_input_real_monday)		AS '월(차이)'		
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
			, W.new_dt_start														AS '시작날짜'
			, W.new_dt_end															AS '끝날짜'
			, W.new_p_year															AS '년'
			, W.new_p_month															AS '월'
			, W.new_p_week															AS '주'
			,ISNULL(WR.new_txt_remark,' ')											AS '비고'
			,WM3.new_txt_code														AS '정렬코드'
FROM		new_weekly_report_detail	WRD
INNER JOIN	new_work_master3			WM3	ON WRD.new_l_work_master3 = WM3.new_work_master3Id
INNER JOIN	new_weekly_report			WR  ON WR.new_weekly_reportId = WRD.new_l_weekly_report
INNER JOIN	systemuser					SU  ON SU.SystemUserId = WRD.OwnerId
INNER JOIN	new_week					W   ON WR.new_l_week = W.new_weekId
WHERE		1=1
AND			WRD.new_p_division = 100000000
-- 보직자 등 , 주간업무보고 제외용 필드 ( 1일시 주간업무 보고 사용 ) 
AND         SU.new_p_reportyn = 1
AND			(@TeamMemberName = '' OR WRD.OwnerIdName = @TeamMemberName)
AND		    SU.BusinessUnitIdName = @BottomTeamName

UNION ALL

-- 프로젝트 (구분 0 ) 
SELECT		WRD.OwnerIdName AS '담당자'
			, SU.BusinessUnitIdName AS '부서'
            , '프로젝트' AS '구분'
			, CASE WHEN WRD.new_p_check = 100000000 THEN '진행중' ELSE '완료' END	AS '완료여부'
            , CASE WHEN P.new_p_actiontype = 100000000 THEN '직접' ELSE '지원' END	AS '대분류' -- 100000000 직접 100000001 지원
            , PD.new_l_projectName													AS '중분류'
            , PD.new_name															AS '소분류'
            , WRD.new_ntxt_description												AS '내용'
            , WRD.new_d_input_expected_monday										AS '월(예상)'
            , WRD.new_d_input_real_monday											AS '월(실적)'
			, -(WRD.new_d_input_expected_monday - WRD.new_d_input_real_monday)	    AS '월(차이)'
            , WRD.new_d_input_expected_tuesday										AS '화(예상)'
            , WRD.new_d_input_real_tuesday											AS '화(실적)'
			, -(WRD.new_d_input_expected_tuesday - WRD.new_d_input_real_tuesday)     AS '화(차이)'											 
            , WRD.new_d_input_expected_wednesday									AS '수(예상)'
            , WRD.new_d_input_real_wednesday										AS '수(실적)'
            , -(WRD.new_d_input_expected_wednesday - WRD.new_d_input_real_wednesday)	AS '수(차이)'
			, WRD.new_d_input_expected_thursday										AS '목(예상)'
            , WRD.new_d_input_real_thursday											AS '목(실적)'
            , -(WRD.new_d_input_expected_thursday - WRD.new_d_input_real_thursday)  AS '목(차이)'
			, WRD.new_d_input_expected_friday										AS '금(예상)'
            , WRD.new_d_input_real_friday											AS '금(실적)'
            , -(WRD.new_d_input_expected_friday -  WRD.new_d_input_real_friday)     AS '금(차이)'
			, WRD.new_d_expected_sum												AS '합계(예상)'
            , WRD.new_d_real_sum													AS '합계(실적)'
			, -(WRD.new_d_expected_sum - WRD.new_d_real_sum)                        AS '합계(차이)'
			, WR.new_name															AS '주차'
			, W.new_dt_start														AS '시작날짜'
			, W.new_dt_end															AS '끝날짜'
			, W.new_p_year															AS '년'
			, W.new_p_month															AS '월'
			, W.new_p_week															AS '주'
			,ISNULL(WR.new_txt_remark,' ')											AS '비고'
			,P.new_txt_code															AS '정렬코드'
FROM		new_weekly_report_detail	WRD
INNER JOIN	new_projectdetail			PD	ON WRD.new_l_related_project_detail = PD.new_projectdetailId
INNER JOIN	new_project					P	ON PD.new_l_project = P.new_projectId
INNER JOIN	new_weekly_report			WR  ON WR.new_weekly_reportId = WRD.new_l_weekly_report
INNER JOIN  systemuser					SU  ON SU.SystemUserId = WRD.OwnerId
INNER JOIN  new_week					W   ON WR.new_l_week = W.new_weekId
WHERE		1=1
AND			WRD.new_p_division = 100000001
-- 보직자 등 , 주간업무보고 제외용 필드 ( 1일시 주간업무 보고 사용 ) 
AND         SU.new_p_reportyn = 1
AND			(@TeamMemberName = '' OR WRD.OwnerIdName = @TeamMemberName)
AND		    SU.BusinessUnitIdName = @BottomTeamName
) T
ORDER BY 정렬코드

--DEBugging


-- Date를 통해서 월과 주를 판단 
DECLARE @last_date date = DATEADD(dd,-7,@date)
DECLARE @year int = YEAR(@date)
DECLARE @month int =MONTH(@date)





--주간업무보고
IF(@Value = 1)
BEGIN
SELECT 
			  CD.담당자
			 ,CASE WHEN @date BETWEEN CONVERT(DATE, DATEADD(HOUR, 9, CD.시작날짜 ), 23) AND CONVERT(DATE, DATEADD(HOUR, 9, CD.끝날짜), 23) THEN '이번주' ELSE '지난주' END AS '주'
			 ,CD.부서
			 ,CD.구분
			 ,CD.완료여부
			 ,CD.대분류
             ,CD.중분류
			 ,CD.소분류
			 ,CD.내용
			 ,CD.[월(예상)]
			 ,CD.[월(실적)]
			 ,CD.[화(예상)]
			 ,CD.[화(실적)]
			 ,CD.[수(예상)]
			 ,CD.[수(실적)]
			 ,CD.[목(예상)]
			 ,CD.[목(실적)]
			 ,CD.[금(예상)]
			 ,CD.[금(실적)]
			 ,CD.[합계(예상)]
			 ,CD.[합계(실적)]
			 ,CD.[합계(차이)]
			 ,CD.주차
			 ,CD.비고
			 ,SUM(CD.[합계(실적)])
FROM #CommonData CD
GROUP BY CD.담당자
HAVING 1=1
AND (@date BETWEEN CONVERT(DATE, DATEADD(HOUR, -15, CD.시작날짜 ), 23) AND CONVERT(DATE, DATEADD(HOUR, -15, CD.끝날짜), 23) )
OR	 (@last_date BETWEEN CONVERT(DATE, DATEADD(HOUR, 33, CD.시작날짜 ), 23) AND CONVERT(DATE, DATEADD(HOUR, 33, CD.끝날짜), 23))



END
----월간업무보고

ELSE IF(@Value = 2)
BEGIN


SELECT
	 DENSE_RANK() OVER (PARTITION BY  CD.담당자 ORDER BY (SUM(T2.[합계(실적)]) )  desc) as [RANK]
	,CD.담당자            AS '담당자'
	,CD.월
	,CD.주 - 100000000 +1 AS '주'
	,CD.구분
	,CD.대분류
	,CD.중분류
	,CD.소분류
	,SUM(CD.[합계(예상)]) AS '합계(예상)'
	,SUM(CD.[합계(실적)]) AS '합계(실적)'
	,SUM(CD.[합계(차이)]) AS '합계(차이)'
    ,T.[월간총합계(실적)] AS '비중계산용'
	,CASE WHEN T.[월간총합계(실적)] = 0 THEN 0 ELSE (SUM(CD.[합계(실적)]) ) / (T.[월간총합계(실적)]) END AS '업무 비중'
	 FROM #CommonData CD
   --JOIN (SELECT AttributeName,[Value],* FROM StringMap WHERE (AttributeName = 'new_p_week' AND ObjectTypeCode = 10091) ON (CD.주= AttributeValue))
   -- 월간 총 합계를 위한 Join.
   JOIN (
   SELECT 
		CD2.담당자 AS '담당자'
		,SUM(CD2.[합계(실적)]) AS '월간총합계(실적)'
		 FROM #CommonData CD2
		 GROUP BY CD2.년, CD2.월 ,CD2.담당자
		 HAVING 1=1
		 -- @year의 OptionSetValue Example --- Lable : Value = 2020 : 100002020
		 AND @year = CD2.년- 100000000 
		 -- @month의 OptionSetValue Example --- Lable : Value = 1 : 1
		 AND @month = CD2.월 
   )  T ON ( CD.담당자 = T.담당자 )
   LEFT JOIN 
   (SELECT
		 CD.담당자            AS '담당자'
		,CD.대분류			 AS '대분류'
		,CD.중분류           AS '중분류'
		,CD.소분류           AS '소분류'
		,SUM(CD.[합계(실적)]) AS '합계(실적)'

		FROM #CommonData CD
		GROUP BY  CD.년,CD.월, CD.담당자 ,CD.대분류,CD.중분류,CD.소분류 
		HAVING 1=1
		AND @year = CD.년- 100000000 
		AND @month = CD.월 
		) T2 ON (CD.담당자 = T2.담당자 AND CD.소분류 = T2.소분류 AND CD.대분류 = T2.대분류 AND CD.중분류 = T2.중분류)
   GROUP BY  CD.년,CD.월, CD.담당자 ,CD.주,CD.구분, CD.대분류, CD.중분류, CD.소분류 ,T.[월간총합계(실적)] 
   HAVING 1=1
   AND @year = CD.년- 100000000 
   AND @month = CD.월 

    --Lable : Value 하드코딩을 방지하고자 하면 아래의 String Map을 사용할것. 
    --JOIN 성능 우려 Key - Value 일정하니 일괄 적용
    --INNOR JOIN (SELECT AttributeName,[Value] FROM StringMap WHERE (AttributeName = 'new_p_year' AND ObjectTypeCode = 10043) temp ON (new_week.new_p_year= temp.AttributeValue)
   
END
----연간업무보고 
ELSE IF(@Value ='3')
BEGIN
SELECT 
		 CD.담당자
		,CD.월
		,CD.구분
		,CD.대분류
		,CD.중분류
		,CD.소분류
		--월로 부분합 되는지 확인 해볼 것 
		,SUM(CD.[합계(예상)]) AS '합계(예상)'
		,SUM(CD.[합계(실적)]) AS '합계(실적)'
		,SUM(CD.[합계(차이)]) AS '합계(차이)'
FROM #CommonData CD		   
JOIN (
   SELECT 
	CD2.담당자 AS '담당자'
	,SUM(CD2.[합계(실적)]) AS '월간총합계(실적)'
	 FROM #CommonData CD2
	 GROUP BY CD2.년,CD2.담당자
     HAVING 1=1
	AND CD2.년 -100000000 = @year  
   )  T ON ( CD.담당자 = T.담당자)
GROUP BY CD.년,CD.담당자 ,CD.월,CD.구분, CD.대분류, CD.중분류, CD.소분류
HAVING 1=1
AND CD.년 -100000000 = @year  
AND  CD.월 <=@month 



END

IF OBJECT_ID('tempdb..#CommonData') IS NOT NULL
BEGIN DROP TABLE #CommonData END
END



