﻿CREATE TABLE [dbo].[CLIENTS_PRODUCTS_LINK]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CLIENT_ID] INT NOT NULL, 
    [PRODUCT_ID] INT NOT NULL, 
    CONSTRAINT [FK_CLIENTS_PRODUCTS_LINK_CLIENTS] FOREIGN KEY ([CLIENT_ID]) REFERENCES [CLIENTS]([ID]),
    CONSTRAINT [FK_CLIENTS_PRODUCTS_LINK_PRODUCTS] FOREIGN KEY ([PRODUCT_ID]) REFERENCES [PRODUCTS]([ID]),
	
)
