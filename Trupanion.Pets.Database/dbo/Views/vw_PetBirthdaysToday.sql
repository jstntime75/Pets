CREATE VIEW [dbo].[vw_PetBirthdaysToday]
AS 
SELECT        po.Id, 
              po.Name AS PetOwnerName, 
			  po.PolicyNumber, 
			  po.PolicyDate, 
			  p.Name AS PetName, 
			  p.DateOfBirth
FROM          dbo.Pet p 
				INNER JOIN dbo.PetOwner po ON p.PetOwnerId = po.Id
WHERE        ISNULL(po.CancelDate, CAST(GETDATE() AS DATE)) >= CAST(GETDATE() AS DATE) AND (p.DateOfBirth IS NOT NULL) AND (p.DateOfBirth = CAST(GETDATE() AS DATE))