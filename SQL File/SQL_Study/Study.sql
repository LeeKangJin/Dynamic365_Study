USE [ELOGDEV_MSCRM]
GO
/****** Object:  StoredProcedure [dbo].[ZSP_GET_WORK_REPORT]    Script Date: 2021-01-25 오전 11:54:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[ZSP_GET_WORK_REPORT]
	 --@date date

/*
EXEC [dbo].[ZSP_GET_WORK_REPORT2]  --
*/ 
AS
BEGIN
	SET NOCOUNT ON;

	IF OBJECT_ID('tempdb..#new_logbook_cross') IS NOT NULL
	BEGIN DROP TABLE #new_logbook_cross END

	IF OBJECT_ID('tempdb..#new_logbook_temp') IS NOT NULL
	BEGIN DROP TABLE #new_logbook_temp END

	/***Action Item CROSS APPLY***/
	SELECT * INTO #new_logbook_cross
	FROM
	(
		SELECT 
			new_logbookId
			, new_p_actionitem
			, p_actionitem.Value AS 'new_p_actionitemLabel'
		FROM dbo.new_logbook LB
		CROSS APPLY STRING_SPLIT (REPLACE(REPLACE(new_p_actionitem,'[-1,',''),',-1]',''), ',') CA
		LEFT JOIN  (SELECT ObjectTypeCode,AttributeValue,AttributeName,[Value] FROM StringMap WHERE AttributeName = 'new_p_actionitem' AND ObjectTypeCode = 10012) p_actionitem ON CA.value = p_actionitem.AttributeValue
	) T

	 SELECT * FROM #new_logbook_cross

	/***GROUP BY 하여 Comma 로 구분 ***/
	SELECT * INTO #new_logbook_temp
	FROM
	(
		SELECT new_logbookId, new_p_actionitemLabel = 
			STUFF((SELECT ', ' + new_p_actionitemLabel
				   FROM #new_logbook_cross b 
				   WHERE b.new_logbookId = a.new_logbookId 
				  FOR XML PATH('')), 1, 2, '')
		FROM #new_logbook_cross a
		GROUP BY new_logbookId
	) T

	SELECT * FROM #new_logbook_temp

	SELECT
	LB.new_txt_seq AS 'SEQ',
	LB.new_name AS '로그번호',
	CASE EQ.new_p_plant WHEN 100000001 THEN '1공장' WHEN 100000002 THEN '2공장'  END AS '공장',
	new_l_equipmentName AS '원격장비',
	LB.OwnerIdName AS '담당자',
	LB.new_txt_reqname AS '요청자이름',
	LB.new_txt_reqid AS '요청자사번',
	DATEADD(HH,9,LB.new_dt_start) AS '원격시작시간',
	DATEADD(HH,9,LB.new_dt_end) AS '원격마침시간',
	DATEDIFF(MINUTE,DATEADD(HH,9,LB.new_dt_start),DATEADD(HH,9,LB.new_dt_end)) AS '원격시간(분)',
	CASE WHEN LB.statecode = 0 THEN '완료' ELSE '미완료' END AS '완료여부',
	LB2.new_p_actionitemLabel AS 'ActionItem',
	LB.new_ntxt_reason AS '사유',
	EQ.new_txt_ip AS '장비IP',
	EQ.new_txt_team AS 'TEAM'
	FROM dbo.new_logbook LB
	LEFT JOIN #new_logbook_temp LB2 ON LB.new_logbookId = LB2.new_logbookId
	LEFT JOIN dbo.new_equipment EQ ON LB.new_l_equipment = EQ.new_equipmentId
	WHERE 1=1
	AND DATEADD(HH,9,LB.CreatedOn) <  getdate()
	ORDER BY SEQ DESC

	IF OBJECT_ID('tempdb..#new_logbook') IS NOT NULL
	BEGIN DROP TABLE #new_logbook END

	IF OBJECT_ID('tempdb..#new_logbook_temp') IS NOT NULL
	BEGIN DROP TABLE #new_logbook_temp END
END

