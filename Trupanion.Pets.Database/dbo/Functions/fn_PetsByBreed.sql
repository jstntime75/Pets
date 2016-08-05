CREATE FUNCTION [dbo].[fn_PetsByBreed]
(	
	@breed NVARCHAR(40)
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT p.Id
		  ,p.PetOwnerId
		  ,p.BreedId
		  ,p.Name
		  ,p.DateOfBirth
	FROM dbo.Pet p
		JOIN dbo.Breed b ON p.BreedId = b.Id
	WHERE b.Name = @breed
)
