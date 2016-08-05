INSERT INTO dbo.Country (Id, Name, IsoCode)
VALUES 
(1, N'United States of America', 'USA')

INSERT INTO dbo.AnimalType (Id, Name)
VALUES 
(1, 'Dog'), 
(2, 'Cat')

INSERT INTO dbo.Breed (Id, AnimalTypeId, Name)
VALUES 
(1, 1, N'Mixed'),
(2, 2, N'Mixed'),
(3, 1, N'Dachshund'),
(4, 1, N'Beagle')