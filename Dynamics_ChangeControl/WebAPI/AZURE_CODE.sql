USE [CelltrionAzureDB]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*
-- =============================================
Author : Lee Kang Jin
Date : 2020 - 02 - 06
Description : Insert API Celltrion.
-- =============================================

                         1			2		3			5			6				7		8		9		10				11	12		13              14						15		16				17					18           
EXEC dbo.HR_DATA_INSERT '234513',	'4635356',	'11111',	'오동석',	'dongseok.oh',	'1010',	'0004',	'1',	'2005-12-26',	'',	'S',	'1994-06-14',	'dsoh@celltrionph.com',	'Y',	'032-850-3897',	'010-2156-7894',	'Y'
- 없는 그룹사
EXEC dbo.HR_DATA_INSERT '234513',	'2000',	'23544324',	'오동석',	'dongseok.oh',	'1010',	'0004',	'1',	'2005-12-26',	'',	'S',	'1994-06-14',	'dsoh@celltrionph.com',	'Y',	'032-850-3897',	'010-2156-7894',	'Y'
- 없는 부서
EXEC dbo.HR_DATA_INSERT '234513',	'2000',	'11111',	'오동석',	'dongseok.oh',	'',	'0004',	'1',	'2005-12-26',	'',	'S',	'1994-06-14',	'dsoh@celltrionph.com',	'Y',	'032-850-3897',	'010-2156-7894',	'Y'
- 없는 직책
EXEC dbo.HR_DATA_INSERT '234513',	'2000',	'11111',	'오동석',	'dongseok.oh',	'1010',	'2353735',	'1',	'2005-12-26',	'',	'S',	'1994-06-14',	'dsoh@celltrionph.com',	'Y',	'032-850-3897',	'010-2156-7894',	'Y'
- 없는 직급
EXEC dbo.HR_DATA_INSERT '99999','4000',	'4000',	'메이웨더',	'meiwheder','',	'L120',	'2',	'',	'',	'L','',	'meiwheder@celltrion.com',	'',	'','1'
- 없는 양력/음력


SELECT * FROM [dbo].[GW_BASE_OBJECT_GR]

SELECT * FROM HRTemp_TB     
*/


CREATE PROCEDURE [dbo].[HR_DATA_INSERT]
		/*주석추가*/
        @UR_Code VARCHAR(50),				-- 1.사번 :              
        @DN_Code VARCHAR(50),				-- 2.그룹사코드
        @GR_Code VARCHAR(50),				-- 3.소속 부서 코드 
        @DisplayName_KR NVARCHAR(100) ,		-- 5.사용자 한글 이름 
		@DisplayName_EN NVARCHAR(100) ,		-- 6.사용자 영어 이름
		@JobTitleCode VARCHAR(50),			-- 7.직책 코드
        @JobLevelCode VARCHAR(50),			-- 8.직급 코드
        @SortKey VARCHAR(200),				-- 9.우선 순위
        @EnterDate VARCHAR(10),				-- 10.입사일자
        @RetireDate VARCHAR(10),			-- 11.퇴사 일자
        @BirthDiv VARCHAR(10) ,				-- 12.양력/음력
        @BirthDate VARCHAR(10),				-- 13.생년 월일
        @MailAddress VARCHAR(100),			-- 14.외부 메일 주소 
        @PhoneNumberInter VARCHAR(50),		-- 16.내선 번호
        @AD_Mobile VARCHAR(50),				-- 17.휴대폰
		@Personal_Info_YN CHAR(1)			-- 18.개인 정보 동의 여부 (Y/N)
        --@ProcessYN CHAR(1),
        --@CreateOn Datetime,
        --@ProcessComplete Datetime
	
        
        
AS
BEGIN
        -- SET NOCOUNT ON added to prevent extra result sets from
        -- interfering with SELECT statements.
        SET NOCOUNT ON;

        DECLARE @RESULT Int = 1 , @MSG NVARCHAR(300) = ''

		select * FROM [AZURE].[CelltrionAzureDB].[dbo].[GW_BASE_OBJECT_GR]

        -- 소속회사 검사 
        IF((SELECT COUNT(*) 
			FROM [AZURE].[CelltrionAzureDB].[dbo].[GW_BASE_OBJECT_GR]AS COMPANY
			WHERE COMPANY.DN_Code   = @DN_Code
			AND   COMPANY.GroupType = 'Company' 
			) = 0) 
        BEGIN
                 SET @MSG = '소속회사가 잘못 입력 되었습니다.'
                 SET @RESULT = 0
        END


        -- 소속 부서 검사 
        IF(	@RESULT != 0 
		    AND
			(SELECT COUNT(*) FROM [AZURE].[CelltrionAzureDB].[dbo].[GW_BASE_OBJECT_GR] AS TEAM
            WHERE TEAM.DN_Code = @DN_Code
            AND  TEAM.GR_Code = @GR_Code
			AND TEAM.GROUP_TYPE = 'Dept'
            ) = 0)
        BEGIN
                 SET @MSG = '소속부서가 잘못 입력 되었습니다.'
                 SET @RESULT = 0
        END

        --JOB LEVEL 검사 
        IF(
			@RESULT != 0
		    AND
			((SELECT COUNT(*) FROM [AZURE].[CelltrionAzureDB].[dbo].[GW_BASE_OBJECT_GR] AS JOBLEVEL
            WHERE JOBLEVEL.DN_Code = @DN_Code
			AND   JOBLEVEL.GroupType = 'JobLevel'
            AND   JOBLEVEL.GR_Code LIKE @JobLevelCode +'_%'
              ) = 0) AND @JobLevelCode != '') 
        BEGIN
                 SET @MSG = '입력한 직급 코드가 잘못 되었습니다.'
                 SET @RESULT = 0
        END
        -- JOB TITLE 검사
      


        IF(
			@RESULT != 0
		    AND
			((SELECT COUNT(*) FROM [AZURE].[CelltrionAzureDB].[dbo].[GW_BASE_OBJECT_GR] AS JOBTITLE
             WHERE JOBTITLE.DN_Code = @DN_Code
			 AND JOBTITLE.GroupType = 'JobTitle'
            AND JOBTITLE.GR_Code LIKE @JobTitleCode +'_%'
            ) = 0) AND @JobTitleCode != '') 
        BEGIN
                 SET @MSG = '입력한 직책 코드가 잘못 되었습니다.'
                 SET @RESULT = 0
        END

		IF((@BirthDiv != 'S' AND @BirthDiv != 'R') AND @BirthDiv != '')
		BEGIN
				 SET @MSG = '생일 양력/음력 표기가 잘못 되었습니다.'
				 SET @RESULT = 0
		END

		IF(@Personal_Info_YN != '1' )
		BEGIN
				 SET @MSG = '개인정보 동의를 하지 않았습니다.'
				 SET @RESULT = 0
		END


        IF(@RESULT = 1) 
        BEGIN
		

   INSERT INTO [AZURE].[CelltrionAzureDB].dbo.HRTemp_TB(
		 UR_Code 
        ,DN_Code			-- 사번
        ,GR_Code			-- 소속 부서 코드
        ,LogonID			-- 로그인 아이디
		,EmpNo              -- 직원 번호
        ,DisplayName		-- 사용자 한글 이름
        ,ExDisplayName		-- 사용자 다국어 이름
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
        ,ProcessYN			-- Temp To GW 여부 
        ,CreateOn			-- 생성 일자
        ,ProcessComplete	-- 완료 일자
		,Personal_Info_YN	-- 개인정보 동의 여부 
   )
   VALUES(
         @UR_Code 
        ,@DN_Code 
        ,@GR_Code 
        ,@UR_Code
		,@UR_Code 
        ,@DisplayName_KR
		,(@DisplayName_KR +';'+ @DisplayName_EN +';;;;;;;') 
		,@JobTitleCode 
        ,@JobLevelCode
        ,@SortKey
        ,@EnterDate 
        ,@RetireDate 
        ,@BirthDiv 
        ,@BirthDate 
        ,@MailAddress 
        ,@PhoneNumberInter
        ,@AD_Mobile 
        ,@MailAddress 
        ,0
        ,GETDATE()
        ,NULL
		,@Personal_Info_YN
   )
   SET @MSG = 'DB Insert 성공 하였습니다.' 
   END

		
        SELECT @RESULT AS RESULT, @MSG AS MSG, @@ROWCOUNT AS ROWNUMBER

END


