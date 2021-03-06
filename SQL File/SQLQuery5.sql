USE [COVI_SYNCDATA]
GO
/****** Object:  StoredProcedure [dbo].[HR_DATA_SELECT]    Script Date: 2019-12-20 오후 3:37:38 ******/
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
	@UR_Code VARCHAR(50) -- 사번
	,@DN_Code VARCHAR(50) --  그룹사 코드 
	,@DisplayName_KR NVARCHAR(50) -- 사용자 이름
	

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;



	SELECT UR_Code
		,DN_Code			-- 회사 코드
		,GR_Code			-- 사번
		,LogonID			-- 로그인 아이디
		,EmpNo				-- 사번
		,DisplayName		-- 사용자 이름
		,ExDisplayName		-- 사용자 영어이름
		,JobTitleCode		-- 직책 코드
		,JobLevelCode		-- 직급 코드
		,SortKey			-- 우선 순위
		,EnterDate			-- 입사 일자
		,RetireDate			-- 퇴사 일자
		,BirthDiv			-- 양력/음력
		,BirthDate			-- 생년 월일
		,ExternalMailAddress-- 외부 메일 주소
		,PhoneNumberInter	-- 내선번호
		,AD_Mobile			-- 휴대폰
		,EX_PrimaryMail		-- 주메일 주소
	FROM OrgPerson
	WHERE 1=1
	AND UR_Code LIKE '%'+ @UR_Code +'%'
	AND DisplayName LIKE '%' + @DisplayName_KR +'%'


	SELECT * FROM OrgPerson

END
