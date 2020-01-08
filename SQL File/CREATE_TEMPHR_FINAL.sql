USE [COVI_SYNCDATA]
GO

/****** Object:  Table [dbo].[HRTemp_TB]    Script Date: 2020-01-07 오전 11:23:47 ******/
DROP TABLE [dbo].[HRTemp_TB]
GO

/****** Object:  Table [dbo].[HRTemp_TB]    Script Date: 2020-01-07 오전 11:23:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HRTemp_TB](
	[UR_Code] [varchar](50) NOT NULL,
	[DN_Code] [varchar](50) NOT NULL,
	[GR_Code] [varchar](50) NOT NULL,
	[LogonID] [varchar](50) NOT NULL,
	[EmpNo] [varchar](50) NULL,
	[DisplayName] [nvarchar](100) NOT NULL,
	-- [DisplayName_EN] [nvarchar](100) NOT NULL,
	[JobPositionCode] [varchar](50) DEFAULT '', -- Sync Field
	[ExDisplayName] [nvarchar](1000) NULL,
	[JobTitleCode] [varchar](50) NULL,
	[JobLevelCode] [varchar](50) NULL,
	[SortKey] [varchar](200) NOT NULL,
	[IsUse][char](1) DEFAULT 'Y',-- Sync Field
	[EnterDate] [varchar](10) NULL,
	[RetireDate] [varchar](10) NULL,
	[BirthDiv] [varchar](10) NULL,
	[BirthDate] [varchar](10) NULL,
	[PhotoPath] [varchar](256) DEFAULT '',-- Sync Field
	[ExternalMailAddress] [varchar](100) NULL,
	[ChargeBusiness][nvarchar](100) DEFAULT '',-- Sync Field
	[PhoneNumberInter] [varchar](50) NULL,
	[AD_IsUse][char](1) DEFAULT 'Y',-- Sync Field
	[AD_PhoneNumber][varchar](50) DEFAULT '',-- Sync Field
	[AD_Fax][varchar](50) DEFAULT '',-- Sync Field
	[AD_ManagerCode][varchar](50) DEFAULT '',-- Sync Field
	[EX_IsUse][char](1) DEFAULT 'Y',-- Sync Field
	[AD_Mobile] [varchar](50) NULL,
	[EX_PrimaryMail] [varchar](50) NULL,
	[EMPLOYEMENT][varchar](10) DEFAULT '재직',-- Sync Field
	[WORK_SITE][varchar](50) DEFAULT '',-- Sync Field
	[JOB][varchar](50) DEFAULT '',-- Sync Field
	[O365_IsUse][char](1) DEFAULT 'Y',-- Sync Field
	[O365_LicenseID][varchar](50) DEFAULT '',-- Sync Field
	[ProcessYN] [char](1) NULL,
	[CreateOn] [date] NULL,
	[ProcessComplete] [date] NULL,
	[Personal_Info_YN] [char](1) NULL,
PRIMARY KEY CLUSTERED 
(
	[UR_Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


