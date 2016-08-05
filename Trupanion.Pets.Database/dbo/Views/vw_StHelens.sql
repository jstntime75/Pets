CREATE VIEW [dbo].[vw_StHelens]
AS 
SELECT [Date], [Count]
FROM dbo.fn_CountEnrollmentsByDate('19800518')
