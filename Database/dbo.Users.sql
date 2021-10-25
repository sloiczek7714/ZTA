CREATE TABLE [dbo].[Users] (
    [ID]       INT       NOT NULL IDENTITY,
    [e-mail]   CHAR (25) NOT NULL,
    [password] CHAR (25) NOT NULL, 
    CONSTRAINT [PK_Users] PRIMARY KEY ([ID])
);

