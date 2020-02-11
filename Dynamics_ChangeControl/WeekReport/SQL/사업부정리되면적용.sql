--사업부 정리 필요 ( 오데이터가 있음 ) 
SELECT '' AS 'Value', '전체' AS 'Label'
UNION ALL
SELECT DISTINCT ParentBusinessUnitId ,ParentBusinessUnitIdName FROM BusinessUnit
WHERE 1=1

DECLARE @parID NVARCHAR(MAX)

SELECT '' AS 'Value', '전체' AS 'Label'
UNION ALL
SELECT DISTINCT Name, Name FROM BusinessUnit 
WHERE 1=1
AND ParentBusinessUnitId = @parID

