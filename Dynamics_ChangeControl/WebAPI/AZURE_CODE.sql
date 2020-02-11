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
EXEC dbo.HR_DATA_INSERT '234513',	'4635356',	'11111',	'������',	'dongseok.oh',	'1010',	'0004',	'1',	'2005-12-26',	'',	'S',	'1994-06-14',	'dsoh@celltrionph.com',	'Y',	'032-850-3897',	'010-2156-7894',	'Y'
- ���� �׷��
EXEC dbo.HR_DATA_INSERT '234513',	'2000',	'23544324',	'������',	'dongseok.oh',	'1010',	'0004',	'1',	'2005-12-26',	'',	'S',	'1994-06-14',	'dsoh@celltrionph.com',	'Y',	'032-850-3897',	'010-2156-7894',	'Y'
- ���� �μ�
EXEC dbo.HR_DATA_INSERT '234513',	'2000',	'11111',	'������',	'dongseok.oh',	'',	'0004',	'1',	'2005-12-26',	'',	'S',	'1994-06-14',	'dsoh@celltrionph.com',	'Y',	'032-850-3897',	'010-2156-7894',	'Y'
- ���� ��å
EXEC dbo.HR_DATA_INSERT '234513',	'2000',	'11111',	'������',	'dongseok.oh',	'1010',	'2353735',	'1',	'2005-12-26',	'',	'S',	'1994-06-14',	'dsoh@celltrionph.com',	'Y',	'032-850-3897',	'010-2156-7894',	'Y'
- ���� ����
EXEC dbo.HR_DATA_INSERT '99999','4000',	'4000',	'���̿���',	'meiwheder','',	'L120',	'2',	'',	'',	'L','',	'meiwheder@celltrion.com',	'',	'','1'
- ���� ���/����


SELECT * FROM [dbo].[GW_BASE_OBJECT_GR]

SELECT * FROM HRTemp_TB     
*/


CREATE PROCEDURE [dbo].[HR_DATA_INSERT]
		/*�ּ��߰�*/
        @UR_Code VARCHAR(50),				-- 1.��� :              
        @DN_Code VARCHAR(50),				-- 2.�׷���ڵ�
        @GR_Code VARCHAR(50),				-- 3.�Ҽ� �μ� �ڵ� 
        @DisplayName_KR NVARCHAR(100) ,		-- 5.����� �ѱ� �̸� 
		@DisplayName_EN NVARCHAR(100) ,		-- 6.����� ���� �̸�
		@JobTitleCode VARCHAR(50),			-- 7.��å �ڵ�
        @JobLevelCode VARCHAR(50),			-- 8.���� �ڵ�
        @SortKey VARCHAR(200),				-- 9.�켱 ����
        @EnterDate VARCHAR(10),				-- 10.�Ի�����
        @RetireDate VARCHAR(10),			-- 11.��� ����
        @BirthDiv VARCHAR(10) ,				-- 12.���/����
        @BirthDate VARCHAR(10),				-- 13.���� ����
        @MailAddress VARCHAR(100),			-- 14.�ܺ� ���� �ּ� 
        @PhoneNumberInter VARCHAR(50),		-- 16.���� ��ȣ
        @AD_Mobile VARCHAR(50),				-- 17.�޴���
		@Personal_Info_YN CHAR(1)			-- 18.���� ���� ���� ���� (Y/N)
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

        -- �Ҽ�ȸ�� �˻� 
        IF((SELECT COUNT(*) 
			FROM [AZURE].[CelltrionAzureDB].[dbo].[GW_BASE_OBJECT_GR]AS COMPANY
			WHERE COMPANY.DN_Code   = @DN_Code
			AND   COMPANY.GroupType = 'Company' 
			) = 0) 
        BEGIN
                 SET @MSG = '�Ҽ�ȸ�簡 �߸� �Է� �Ǿ����ϴ�.'
                 SET @RESULT = 0
        END


        -- �Ҽ� �μ� �˻� 
        IF(	@RESULT != 0 
		    AND
			(SELECT COUNT(*) FROM [AZURE].[CelltrionAzureDB].[dbo].[GW_BASE_OBJECT_GR] AS TEAM
            WHERE TEAM.DN_Code = @DN_Code
            AND  TEAM.GR_Code = @GR_Code
			AND TEAM.GROUP_TYPE = 'Dept'
            ) = 0)
        BEGIN
                 SET @MSG = '�ҼӺμ��� �߸� �Է� �Ǿ����ϴ�.'
                 SET @RESULT = 0
        END

        --JOB LEVEL �˻� 
        IF(
			@RESULT != 0
		    AND
			((SELECT COUNT(*) FROM [AZURE].[CelltrionAzureDB].[dbo].[GW_BASE_OBJECT_GR] AS JOBLEVEL
            WHERE JOBLEVEL.DN_Code = @DN_Code
			AND   JOBLEVEL.GroupType = 'JobLevel'
            AND   JOBLEVEL.GR_Code LIKE @JobLevelCode +'_%'
              ) = 0) AND @JobLevelCode != '') 
        BEGIN
                 SET @MSG = '�Է��� ���� �ڵ尡 �߸� �Ǿ����ϴ�.'
                 SET @RESULT = 0
        END
        -- JOB TITLE �˻�
      


        IF(
			@RESULT != 0
		    AND
			((SELECT COUNT(*) FROM [AZURE].[CelltrionAzureDB].[dbo].[GW_BASE_OBJECT_GR] AS JOBTITLE
             WHERE JOBTITLE.DN_Code = @DN_Code
			 AND JOBTITLE.GroupType = 'JobTitle'
            AND JOBTITLE.GR_Code LIKE @JobTitleCode +'_%'
            ) = 0) AND @JobTitleCode != '') 
        BEGIN
                 SET @MSG = '�Է��� ��å �ڵ尡 �߸� �Ǿ����ϴ�.'
                 SET @RESULT = 0
        END

		IF((@BirthDiv != 'S' AND @BirthDiv != 'R') AND @BirthDiv != '')
		BEGIN
				 SET @MSG = '���� ���/���� ǥ�Ⱑ �߸� �Ǿ����ϴ�.'
				 SET @RESULT = 0
		END

		IF(@Personal_Info_YN != '1' )
		BEGIN
				 SET @MSG = '�������� ���Ǹ� ���� �ʾҽ��ϴ�.'
				 SET @RESULT = 0
		END


        IF(@RESULT = 1) 
        BEGIN
		

   INSERT INTO [AZURE].[CelltrionAzureDB].dbo.HRTemp_TB(
		 UR_Code 
        ,DN_Code			-- ���
        ,GR_Code			-- �Ҽ� �μ� �ڵ�
        ,LogonID			-- �α��� ���̵�
		,EmpNo              -- ���� ��ȣ
        ,DisplayName		-- ����� �ѱ� �̸�
        ,ExDisplayName		-- ����� �ٱ��� �̸�
        ,JobTitleCode		-- ��å �ڵ�
        ,JobLevelCode		-- ���� �ڵ�
        ,SortKey			-- �켱 ����
        ,EnterDate			-- �Ի� ����
        ,RetireDate			-- ��� ����
        ,BirthDiv			-- ���/����
        ,BirthDate			-- ���� ����
        ,ExternalMailAddress-- �ܺ� ���� �ּ�
        ,PhoneNumberInter	-- ������ȣ
        ,AD_Mobile			-- �޴���
        ,EX_PrimaryMail		-- �ָ��� �ּ�
        ,ProcessYN			-- Temp To GW ���� 
        ,CreateOn			-- ���� ����
        ,ProcessComplete	-- �Ϸ� ����
		,Personal_Info_YN	-- �������� ���� ���� 
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
   SET @MSG = 'DB Insert ���� �Ͽ����ϴ�.' 
   END

		
        SELECT @RESULT AS RESULT, @MSG AS MSG, @@ROWCOUNT AS ROWNUMBER

END


