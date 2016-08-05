CREATE TABLE [dbo].[Country] (
    [Id]      TINYINT        NOT NULL,
    [Name]    NVARCHAR (100) NOT NULL,
    [IsoCode] CHAR (3)       NOT NULL,
    CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED ([Id] ASC)
);

