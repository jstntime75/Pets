CREATE TABLE [dbo].[PetOwner] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (200) NOT NULL,
    [PolicyNumber] CHAR (13)      NULL,
    [PolicyDate]   DATETIME       NULL,
    [CancelDate]   DATE           NULL,
    [CountryId]    TINYINT        NOT NULL,
    CONSTRAINT [PK_PetOwner] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_PetOwner_Country] FOREIGN KEY ([CountryId]) REFERENCES [dbo].[Country] ([Id]),
	CONSTRAINT [AK_PetOwner_PolicyNumber] UNIQUE NONCLUSTERED ([PolicyNumber])
);

