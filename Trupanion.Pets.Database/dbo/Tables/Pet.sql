CREATE TABLE [dbo].[Pet] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [PetOwnerId]  INT           NOT NULL,
    [BreedId]     SMALLINT      NOT NULL,
    [Name]        NVARCHAR (40) NOT NULL,
    [DateOfBirth] DATE          NULL,
    CONSTRAINT [PK_Pet] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Pet_PetOwner] FOREIGN KEY ([PetOwnerId]) REFERENCES [dbo].[PetOwner] ([Id]),
    CONSTRAINT [FK_Pet_Breed] FOREIGN KEY ([BreedId]) REFERENCES [dbo].[Breed] ([Id])
);

