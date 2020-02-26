USE [CELLCRMDEV_MSCRM]
GO
/****** Object:  StoredProcedure [dbo].[ZSP_GET_Month_Report]    Script Date: 2020-02-26 오후 3:51:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[ZSP_GET_Month_Report]
	  @date date
	 ,@BottomTeamName VARCHAR(MAX)
	 ,@TeamMemberName VARCHAR(MAX)
	 
-- EXEC [dbo].[ZSP_GET_Core_Report] '2', '20-02-03' ,'IT개발팀',''
AS
BEGIN
	SET NOCOUNT ON;
	
	-- To Check.
	--보고서에서 Parameter를 보낼지
	--아래와 같이 Default 2로 정해진 프로시저 CAll로 할지. 


	EXEC [dbo].[ZSP_GET_Core_Report] '2', @date ,@BottomTeamName,@TeamMemberName


END



