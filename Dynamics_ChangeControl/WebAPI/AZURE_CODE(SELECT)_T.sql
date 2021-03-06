USE [CelltrionAzureDB]
GO
/****** Object:  StoredProcedure [dbo].[HR_DATA_SELECT]    Script Date: 2020-02-10 오전 11:16:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<kangjin Lee>
-- Create date: <2019-12-20>
-- Description:	<Select HR data in CellCrm API>
-- =============================================
ALTER PROCEDURE [dbo].[HR_DATA_SELECT]
	@SEARCH_TEXT NVARCHAR(200) -- 검색어
	,@DN_CONDE NVARCHAR(50)

-- EXEC dbo.HR_DATA_SELECT_DEV 'mei', '400'

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		SELECT 
		 UR_Code															 
		,DN_Code									AS  '회사 코드"		 '
		,GR_Code									AS 	'사번			 '
		,LogonID									AS 	'로그인 아이디	 '
		,EmpNo										AS 	'사번			 '
		,DisplayName								AS 	'사용자 이름	 '
		,ExDisplayName								AS 	'사용자 영어이름 '
		,JobTitleCode								AS 	'직책 코드		 '
		,JobLevelCode								AS 	'직급 코드		 '
		,SortKey									AS 	'우선 순위		 '
		,EnterDate									AS 	'입사 일자		 '
		,RetireDate									AS 	'퇴사 일자		 '
		,BirthDiv									AS 	'양력/음력		 '
		,BirthDate									AS 	'생년 월일		 '
		,ExternalMailAddress						AS 	'외부 메일 주소	 '
		,PhoneNumberInter							AS 	'내선번호		 '
		,AD_Mobile									AS 	'휴대폰			 '
		,EX_PrimaryMail								AS  '메일주소'	-- 주메일 주소
		,ProcessYN		
		,Personal_Info_YN											
		,ProcessComplete
	FROM [CelltrionAzureDB].dbo.HRTemp_TB
	WHERE 1=1
	AND UR_Code            LIKE '%' + @SEARCH_TEXT +'%'
	OR DN_Code             LIKE '%' + @SEARCH_TEXT +'%'
	OR GR_Code             LIKE '%' + @SEARCH_TEXT +'%'
	OR LogonID             LIKE '%' + @SEARCH_TEXT +'%'
	OR EmpNo               LIKE '%' + @SEARCH_TEXT +'%'
	OR DisplayName		   LIKE '%' + @SEARCH_TEXT +'%'
	OR ExDisplayName       LIKE '%' + @SEARCH_TEXT +'%'
	OR JobTitleCode		   LIKE '%' + @SEARCH_TEXT +'%'
	OR JobLevelCode		   LIKE '%' + @SEARCH_TEXT +'%'
	OR SortKey			   LIKE '%' + @SEARCH_TEXT +'%'
	OR EnterDate     	   LIKE '%' + @SEARCH_TEXT +'%'
	OR RetireDate		   LIKE '%' + @SEARCH_TEXT +'%'
	OR BirthDiv			   LIKE '%' + @SEARCH_TEXT +'%'
	OR BirthDate		   LIKE '%' + @SEARCH_TEXT +'%'
	OR ExternalMailAddress LIKE '%' + @SEARCH_TEXT +'%'
	OR PhoneNumberInter	   LIKE '%' + @SEARCH_TEXT +'%'
	OR AD_Mobile		   LIKE '%' + @SEARCH_TEXT +'%'
	OR  EX_PrimaryMail     LIKE '%' + @SEARCH_TEXT +'%'
	AND @DN_CONDE = DN_Code
END
