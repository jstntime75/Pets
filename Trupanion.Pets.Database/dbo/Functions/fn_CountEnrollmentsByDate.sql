CREATE FUNCTION [dbo].[fn_CountEnrollmentsByDate]
(
	@filter DATE
)
RETURNS TABLE
AS
RETURN 
(
	SELECT @filter AS [Date], COUNT(*) AS [Count]
	FROM dbo.PetOwner
	WHERE ISNULL(CancelDate, CAST(GETDATE() AS DATE)) >= @filter AND PolicyDate BETWEEN @filter AND DATEADD(DAY, 1, @filter)
)
