CREATE TABLE [dbo].[AnimalType]
(
	[Id]   TINYINT             NOT NULL,
	[Name] VARCHAR (20)        NOT NULL,
	CONSTRAINT [PK_AnimalType] PRIMARY KEY CLUSTERED ([Id] ASC),
)
