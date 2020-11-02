﻿CREATE TABLE [dbo].[CLIENTS_MANAGERS_LINK]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [MANAGER_ID] INT NOT NULL, 
    [CLIENT_ID] INT NOT NULL UNIQUE, 
    CONSTRAINT [FK_CLIENTS_MANAGERS_LINK_MANAGERS] FOREIGN KEY ([MANAGER_ID]) REFERENCES [MANAGERS]([ID]), 
    CONSTRAINT [FK_CLIENTS_MANAGERS_LINK_CLIENTS] FOREIGN KEY ([CLIENT_ID]) REFERENCES [CLIENTS]([ID])
)
