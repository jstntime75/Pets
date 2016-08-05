SET IDENTITY_INSERT dbo.[PetOwner] ON
INSERT INTO [dbo].[PetOwner] ([Id], [Name], [CountryId], [PolicyNumber], [PolicyDate])
VALUES
(1, N'Justin Southall', 1, 'USA0000000001', '20110916 2:30:24 PM'),
(2, N'Lyndzy Baumgart', 1, 'USA0000000002', '20060618 10:34:09 AM'),
(3, N'Jennifer Southall', 1, 'USA0000000003', '20030228 1:44:55 PM'),

-- for St Helens query
(4, N'Saint1 Helen1', 1, 'USA0000000004', '19800518 2:30:24 PM'),
(5, N'Saint2 Helen2', 1, 'USA0000000005', '19800518 10:34:09 AM')

SET IDENTITY_INSERT dbo.[PetOwner] OFF

SET IDENTITY_INSERT dbo.[Pet] ON
INSERT INTO [dbo].[Pet] ([Id], [PetOwnerId], [BreedId], [Name], [DateOfBirth])
VALUES
(1, 1, 3, N'Klaus', '20110916 2:30:24 PM'),
(2, 2, 4, N'Jack', '20060618 10:34:09 AM'),
(3, 3, 2, N'Patches', '20030228 1:44:55 PM')
SET IDENTITY_INSERT dbo.[Pet] OFF
