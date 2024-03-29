﻿/*
Deployment script for test_task_db

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "test_task_db"
:setvar DefaultFilePrefix "test_task_db"
:setvar DefaultDataPath ""
:setvar DefaultLogPath ""

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)] COLLATE SQL_Latin1_General_CP1_CI_AS
GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                RECOVERY FULL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_PLANS_PER_QUERY = 200, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
    END


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Creating [dbo].[MANAGERS]...';


GO
CREATE TABLE [dbo].[MANAGERS] (
    [ID]      INT           IDENTITY (1, 1) NOT NULL,
    [NAME]    NVARCHAR (50) NOT NULL,
    [COMMENT] NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creating [dbo].[CLIENTS_PRODUCTS_LINK]...';


GO
CREATE TABLE [dbo].[CLIENTS_PRODUCTS_LINK] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [CLIENT_ID]  INT NOT NULL,
    [PRODUCT_ID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creating [dbo].[CLIENTS]...';


GO
CREATE TABLE [dbo].[CLIENTS] (
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [NAME]         NVARCHAR (50) NOT NULL,
    [PRIOR_CLIENT] BIT           NOT NULL,
    [COMMENT]      NVARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creating [dbo].[CONTACTS]...';


GO
CREATE TABLE [dbo].[CONTACTS] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [FIRST_NAME] NVARCHAR (50) NULL,
    [LAST_NAME]  NVARCHAR (50) NULL,
    [TEL]        BIGINT        NULL,
    [EMAIL]      NVARCHAR (50) NULL,
    [COMMENT]    NVARCHAR (50) NULL,
    [CLIENT_ID]  INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creating [dbo].[PRODUCTS]...';


GO
CREATE TABLE [dbo].[PRODUCTS] (
    [ID]     INT             IDENTITY (1, 1) NOT NULL,
    [NAME]   NVARCHAR (50)   NOT NULL,
    [PRICE]  DECIMAL (18, 2) NOT NULL,
    [SUBSC]  BIT             NOT NULL,
    [PERIOD] INT             NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creating [dbo].[CLIENTS_MANAGERS_LINK]...';


GO
CREATE TABLE [dbo].[CLIENTS_MANAGERS_LINK] (
    [ID]         INT IDENTITY (1, 1) NOT NULL,
    [MANAGER_ID] INT NOT NULL,
    [CLIENT_ID]  INT NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    UNIQUE NONCLUSTERED ([CLIENT_ID] ASC)
);


GO
PRINT N'Creating unnamed constraint on [dbo].[CLIENTS]...';


GO
ALTER TABLE [dbo].[CLIENTS]
    ADD DEFAULT 0 FOR [PRIOR_CLIENT];


GO
PRINT N'Creating [dbo].[FK_CLIENTS_PRODUCTS_LINK_CLIENTS]...';


GO
ALTER TABLE [dbo].[CLIENTS_PRODUCTS_LINK]
    ADD CONSTRAINT [FK_CLIENTS_PRODUCTS_LINK_CLIENTS] FOREIGN KEY ([CLIENT_ID]) REFERENCES [dbo].[CLIENTS] ([ID]);


GO
PRINT N'Creating [dbo].[FK_CLIENTS_PRODUCTS_LINK_PRODUCTS]...';


GO
ALTER TABLE [dbo].[CLIENTS_PRODUCTS_LINK]
    ADD CONSTRAINT [FK_CLIENTS_PRODUCTS_LINK_PRODUCTS] FOREIGN KEY ([PRODUCT_ID]) REFERENCES [dbo].[PRODUCTS] ([ID]);


GO
PRINT N'Creating [dbo].[FK_CONTACTS_CLIENTS]...';


GO
ALTER TABLE [dbo].[CONTACTS]
    ADD CONSTRAINT [FK_CONTACTS_CLIENTS] FOREIGN KEY ([CLIENT_ID]) REFERENCES [dbo].[CLIENTS] ([ID]);


GO
PRINT N'Creating [dbo].[FK_CLIENTS_MANAGERS_LINK_MANAGERS]...';


GO
ALTER TABLE [dbo].[CLIENTS_MANAGERS_LINK]
    ADD CONSTRAINT [FK_CLIENTS_MANAGERS_LINK_MANAGERS] FOREIGN KEY ([MANAGER_ID]) REFERENCES [dbo].[MANAGERS] ([ID]);


GO
PRINT N'Creating [dbo].[FK_CLIENTS_MANAGERS_LINK_CLIENTS]...';


GO
ALTER TABLE [dbo].[CLIENTS_MANAGERS_LINK]
    ADD CONSTRAINT [FK_CLIENTS_MANAGERS_LINK_CLIENTS] FOREIGN KEY ([CLIENT_ID]) REFERENCES [dbo].[CLIENTS] ([ID]);


GO
PRINT N'Creating [dbo].[CK_CONTACTS_NAMES]...';


GO
ALTER TABLE [dbo].[CONTACTS]
    ADD CONSTRAINT [CK_CONTACTS_NAMES] CHECK ((FIRST_NAME is not null) or (LAST_NAME is not null));


GO
PRINT N'Creating [dbo].[CK_CONTACTS_CONTACT]...';


GO
ALTER TABLE [dbo].[CONTACTS]
    ADD CONSTRAINT [CK_CONTACTS_CONTACT] CHECK ((TEL is not null) or (EMAIL is not null));


GO
PRINT N'Creating [dbo].[CK_SUBSC_PERIOD_CONF_Column]...';


GO
ALTER TABLE [dbo].[PRODUCTS]
    ADD CONSTRAINT [CK_SUBSC_PERIOD_CONF_Column] CHECK ([SUBSC]=(1) AND ([PERIOD]=(12) OR [PERIOD]=(3) OR [PERIOD]=(1)) OR ([SUBSC]=(0) AND [PERIOD] IS NULL));


GO
-- Refactoring step to update target server with deployed transaction logs

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'b0f3c4de-19af-486f-ace4-dd6b982423bb')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('b0f3c4de-19af-486f-ace4-dd6b982423bb')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '96ec34d7-12d0-4445-84b1-9cb16c0ad764')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('96ec34d7-12d0-4445-84b1-9cb16c0ad764')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '25a1d51e-23ff-4b31-8f7e-240018280934')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('25a1d51e-23ff-4b31-8f7e-240018280934')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'ff88ac00-9d21-470a-b3d3-cfe8d97f2aa5')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('ff88ac00-9d21-470a-b3d3-cfe8d97f2aa5')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '693ca8f6-143f-4f83-95d4-71622eae94ee')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('693ca8f6-143f-4f83-95d4-71622eae94ee')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '00c11b7c-f414-473e-9ed2-db0d60223b24')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('00c11b7c-f414-473e-9ed2-db0d60223b24')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '53ecf07a-e727-4f0b-963e-388607a7774e')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('53ecf07a-e727-4f0b-963e-388607a7774e')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'd430b377-8dac-4dc3-a532-6454c176417d')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('d430b377-8dac-4dc3-a532-6454c176417d')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'c59e4ae0-ecf0-45fe-8b4a-90b292181a13')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('c59e4ae0-ecf0-45fe-8b4a-90b292181a13')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '17d183d7-ebfd-4989-b643-f23a56f19a42')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('17d183d7-ebfd-4989-b643-f23a56f19a42')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '533306da-28ee-4c55-8964-c19daae9bf7f')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('533306da-28ee-4c55-8964-c19daae9bf7f')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'ef2e1416-aba0-4304-b247-1f125093be65')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('ef2e1416-aba0-4304-b247-1f125093be65')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'c8d21bfd-8bcf-4c3a-97f0-af505268dadc')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('c8d21bfd-8bcf-4c3a-97f0-af505268dadc')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '8d404ac7-8105-4778-a29e-48e6964eb6e0')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('8d404ac7-8105-4778-a29e-48e6964eb6e0')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'cd93badf-bfb3-458d-bb7b-657dde1201dc')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('cd93badf-bfb3-458d-bb7b-657dde1201dc')

GO

GO
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET MULTI_USER 
    WITH ROLLBACK IMMEDIATE;


GO
PRINT N'Update complete.';


GO
