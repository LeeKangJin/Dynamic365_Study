USE [CELLCRMDEV_MSCRM]
GO
/****** Object:  StoredProcedure [dbo].[ZSP_GET_Month_Report]    Script Date: 2020-02-26 ���� 3:51:12 ******/
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
	 
-- EXEC [dbo].[ZSP_GET_Core_Report] '2', '20-02-03' ,'IT������',''
AS
BEGIN
	SET NOCOUNT ON;
	
	-- To Check.
	--�������� Parameter�� ������
	--�Ʒ��� ���� Default 2�� ������ ���ν��� CAll�� ����. 


	EXEC [dbo].[ZSP_GET_Core_Report] '2', @date ,@BottomTeamName,@TeamMemberName


END



