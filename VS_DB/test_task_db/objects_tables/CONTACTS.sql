﻿CREATE TABLE [dbo].[CONTACTS]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FIRST_NAME] NVARCHAR(50) NULL, 
    [LAST_NAME] NVARCHAR(50) NULL, 
    [TEL] BIGINT NULL, 
    [EMAIL] NVARCHAR(50) NULL, 
    [COMMENT] NVARCHAR(50) NULL, 
    [CLIENT_ID] INT NOT NULL, 
    CONSTRAINT [FK_CONTACTS_CLIENTS] FOREIGN KEY ([CLIENT_ID]) REFERENCES [CLIENTS]([ID]), 
    CONSTRAINT [CK_CONTACTS_NAMES] CHECK ((FIRST_NAME is not null) or (LAST_NAME is not null)),
    CONSTRAINT [CK_CONTACTS_CONTACT] CHECK ((TEL is not null) or (EMAIL is not null))    

)
