CREATE TABLE [dbo].[Question] (
    [ID]            INT       NOT NULL IDENTITY,
    [Numer pytania] CHAR (10) NOT NULL,
    [Pytanie]       TEXT      NOT NULL,
    CONSTRAINT [PK_Question] PRIMARY KEY CLUSTERED ([ID] ASC)
);

