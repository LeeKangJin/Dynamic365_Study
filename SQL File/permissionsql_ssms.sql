
DECLARE @CURRENT_USER NVARCHAR(50)


SET @CURRENT_USER = User!UserID

SELECT DomainName FROM SystemUserBase
WHERE (DomainName = @CURRENT_USER)

SELECT CURRENT_USER

IF(@CURRENT_USER = 'dbo')
	EXECUTE AS LOGIN = 'celltrion\1907068'
	SET @CURRENT_USER = (SELECT CURRENT_USER)

GO

SELECT DISTINCT B.Name , R.owningbusinessunit FROM Filterednew_weekly_report R
LEFT JOIN BusinessUnit B
ON (R.owningbusinessunit = B.BusinessUnitId)
GO


SELECT Name,BusinessUnitId FROM BusinessUnit
WHERE BusinessUnitId  = 'DE574657-AFFE-E911-80E5-00155D012E07'
GO
-- LOG OUT COMMAND ? (Cahe Buffer를 날리는건 admin 권한이 있어야됨..)