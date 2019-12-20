USE [COVI_SYNCDATA]
GO
/****** Object:  StoredProcedure [dbo].[HR_DATA_SELECT]    Script Date: 2019-12-20 오후 1:18:46 ******/
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

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT * FROM OrgAdd_DisJob
END
