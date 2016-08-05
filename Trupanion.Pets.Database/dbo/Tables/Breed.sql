CREATE TABLE [dbo].[Breed] (
    [Id]           SMALLINT      NOT NULL,
	[AnimalTypeId] TINYINT       NOT NULL,
    [Name]         NVARCHAR (40) NOT NULL,
    CONSTRAINT [PK_Breed] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_Breed_AnimalType] FOREIGN KEY ([AnimalTypeId]) REFERENCES [dbo].[AnimalType] ([Id])
)
GO

CREATE INDEX [IX_Breed_Name] ON [dbo].[Breed] ([Name])
GO
