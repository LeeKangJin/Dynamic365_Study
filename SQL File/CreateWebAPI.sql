USE [COVI_SYNCDATA]
GO

/****** Object:  Table [dbo].[HRTemp_TB]    Script Date: 2019-12-09 오전 11:41:04 ******/
DROP TABLE [dbo].[HRTemp_TB]
GO

/****** Object:  Table [dbo].[HRTemp_TB]    Script Date: 2019-12-09 오전 11:41:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HRTemp_TB](
	[UR_Code] [varchar](50) NOT NULL,
	[DN_Code] [varchar](50) NOT NULL,
	[GR_Code] [varchar](50) NOT NULL,
	[LoginID] [varchar](50) NOT NULL,
	[EmpNo] [varchar](50) NULL,
	[DisplayName_KR] [nvarchar](100) NOT NULL,
	[DisplayName_EN] [nvarchar](100) NOT NULL,
	[ExDisplayName] [nvarchar](1000) NULL,
	[JobPositionCode] [varchar](5) NULL,
	[JobTitleCode] [varchar](50) NULL,
	[JobLevelCode] [varchar](50) NULL,
	[SortKey] [varchar](200) NOT NULL,
	[EnterDate] [varchar](10) NULL,
	[RetireDate] [varchar](10) NULL,
	[BirthDiv] [varchar](10) NULL,
	[BirthDate] [varchar](10) NULL,
	[ExternalMailAddress] [varchar](100) NULL,
	[ChargeBusiness] [nvarchar](100) NULL,
	[PhoneNumberInter] [varchar](50) NULL,
	[AD_Mobile] [varchar](50) NULL,
	[EX_PrimaryMail] [varchar](50) NULL,
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


