
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/03/2017 13:49:20
-- Generated from EDMX file: D:\projects\SmartActS\source\SmartActS\SmartActS\DataModels\SmartActsModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [SmartActS];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_dbo_UserClaims_dbo_Users_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserClaims] DROP CONSTRAINT [FK_dbo_UserClaims_dbo_Users_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_UserLogins_dbo_Users_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserLogin] DROP CONSTRAINT [FK_dbo_UserLogins_dbo_Users_UserId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_UserRoles_dbo_Roles_RoleId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_dbo_UserRoles_dbo_Roles_RoleId];
GO
IF OBJECT_ID(N'[dbo].[FK_dbo_UserRoles_dbo_Users_UserId]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_dbo_UserRoles_dbo_Users_UserId];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Category];
GO
IF OBJECT_ID(N'[dbo].[Customer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customer];
GO
IF OBJECT_ID(N'[dbo].[FileAttacth]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FileAttacth];
GO
IF OBJECT_ID(N'[dbo].[Location]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Location];
GO
IF OBJECT_ID(N'[dbo].[Rank]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Rank];
GO
IF OBJECT_ID(N'[dbo].[Request]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Request];
GO
IF OBJECT_ID(N'[dbo].[Response]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Response];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Supply]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Supply];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[UserClaims]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserClaims];
GO
IF OBJECT_ID(N'[dbo].[UserLogin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserLogin];
GO
IF OBJECT_ID(N'[dbo].[UserRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRole];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [CategoryId] int IDENTITY(1,1) NOT NULL,
    [CategoryCode] varchar(50)  NULL,
    [CategoryName] nvarchar(50)  NULL,
    [ParentId] int  NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [CustomerId] int IDENTITY(1,1) NOT NULL,
    [CustomerCode] varchar(50)  NULL,
    [CustomerName] nvarchar(50)  NULL,
    [Email] varchar(50)  NULL,
    [MobiPhone] varchar(50)  NULL,
    [Address] nvarchar(1000)  NULL,
    [LngTitude] float  NULL,
    [LatTitude] float  NULL,
    [LocationId] int  NULL,
    [IsStatus] int  NULL,
    [UserId] nvarchar(128)  NULL
);
GO

-- Creating table 'FileAttacths'
CREATE TABLE [dbo].[FileAttacths] (
    [FileAttachId] int IDENTITY(1,1) NOT NULL,
    [FileName] nvarchar(500)  NULL,
    [FilePath] nvarchar(1000)  NULL,
    [FileSize] float  NULL
);
GO

-- Creating table 'Locations'
CREATE TABLE [dbo].[Locations] (
    [LocationId] int  NOT NULL,
    [LocationCode] varchar(50)  NULL,
    [LocationName] nvarchar(100)  NULL,
    [PrarentId] int  NULL,
    [LngTitude] float  NULL,
    [LatTitude] float  NULL
);
GO

-- Creating table 'Ranks'
CREATE TABLE [dbo].[Ranks] (
    [RankId] int IDENTITY(1,1) NOT NULL,
    [RankCode] varchar(50)  NULL,
    [RankName] nvarchar(50)  NULL
);
GO

-- Creating table 'Requests'
CREATE TABLE [dbo].[Requests] (
    [RequestId] int IDENTITY(1,1) NOT NULL,
    [RequestCode] varchar(50)  NULL,
    [CategoryId] int  NOT NULL,
    [CustomerId] int  NOT NULL,
    [Status] int  NULL,
    [CreatedDate] datetime  NULL,
    [DurationExpired] int  NULL,
    [FromBudget] decimal(19,4)  NULL,
    [ToBudget] decimal(19,4)  NULL,
    [RequireResponse] int  NULL,
    [ShippingAddress] nvarchar(1000)  NULL,
    [LocationSupplyId] int  NULL,
    [Description] nvarchar(1000)  NULL,
    [BestTime] bit  NULL,
    [BestSupply] bit  NULL,
    [BestPrice] bit  NULL,
    [RequestTitle] nvarchar(100)  NULL,
    [FileAttrachId] int  NULL
);
GO

-- Creating table 'Responses'
CREATE TABLE [dbo].[Responses] (
    [ResponseId] int IDENTITY(1,1) NOT NULL,
    [ResponseTitle] nvarchar(100)  NULL,
    [RequestId] int  NOT NULL,
    [SupplyId] int  NOT NULL,
    [ResponseTime] datetime  NULL,
    [Status] int  NULL,
    [PriceSuggest] decimal(19,4)  NULL,
    [Description] nvarchar(1000)  NULL,
    [FileAttachId] int  NULL,
    [categoryId] int  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [RoleId] nvarchar(128)  NOT NULL,
    [Name] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] nvarchar(128)  NOT NULL,
    [Email] nvarchar(256)  NULL,
    [EmailConfirmed] bit  NOT NULL,
    [PasswordHash] nvarchar(max)  NULL,
    [SecurityStamp] nvarchar(max)  NULL,
    [PhoneNumber] nvarchar(max)  NULL,
    [PhoneNumberConfirmed] bit  NOT NULL,
    [TwoFactorEnabled] bit  NOT NULL,
    [LockoutEndDateUtc] datetime  NULL,
    [LockoutEnabled] bit  NOT NULL,
    [AccessFailedCount] int  NOT NULL,
    [UserName] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'UserClaims'
CREATE TABLE [dbo].[UserClaims] (
    [UserClaimId] int IDENTITY(1,1) NOT NULL,
    [UserId] nvarchar(128)  NOT NULL,
    [ClaimType] nvarchar(max)  NULL,
    [ClaimValue] nvarchar(max)  NULL
);
GO

-- Creating table 'UserLogins'
CREATE TABLE [dbo].[UserLogins] (
    [LoginProvider] nvarchar(128)  NOT NULL,
    [ProviderKey] nvarchar(128)  NOT NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'Supplies'
CREATE TABLE [dbo].[Supplies] (
    [SupplyId] int IDENTITY(1,1) NOT NULL,
    [SupplyCode] varchar(50)  NULL,
    [SupplyName] nvarchar(100)  NULL,
    [LocationId] int  NULL,
    [LngTitude] float  NULL,
    [Latitude] float  NULL,
    [CategoryId] int  NULL,
    [Address] nvarchar(1000)  NULL,
    [Email] varchar(50)  NULL,
    [MobiPhone] varchar(50)  NULL,
    [Phone] varchar(50)  NULL,
    [Website] varchar(50)  NULL,
    [RankId] int  NULL,
    [IsStatus] int  NULL,
    [UserId] nvarchar(128)  NOT NULL
);
GO

-- Creating table 'UserRole'
CREATE TABLE [dbo].[UserRole] (
    [Roles_RoleId] nvarchar(128)  NOT NULL,
    [Users_UserId] nvarchar(128)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CategoryId] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([CategoryId] ASC);
GO

-- Creating primary key on [CustomerId] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([CustomerId] ASC);
GO

-- Creating primary key on [FileAttachId] in table 'FileAttacths'
ALTER TABLE [dbo].[FileAttacths]
ADD CONSTRAINT [PK_FileAttacths]
    PRIMARY KEY CLUSTERED ([FileAttachId] ASC);
GO

-- Creating primary key on [LocationId] in table 'Locations'
ALTER TABLE [dbo].[Locations]
ADD CONSTRAINT [PK_Locations]
    PRIMARY KEY CLUSTERED ([LocationId] ASC);
GO

-- Creating primary key on [RankId] in table 'Ranks'
ALTER TABLE [dbo].[Ranks]
ADD CONSTRAINT [PK_Ranks]
    PRIMARY KEY CLUSTERED ([RankId] ASC);
GO

-- Creating primary key on [RequestId] in table 'Requests'
ALTER TABLE [dbo].[Requests]
ADD CONSTRAINT [PK_Requests]
    PRIMARY KEY CLUSTERED ([RequestId] ASC);
GO

-- Creating primary key on [ResponseId] in table 'Responses'
ALTER TABLE [dbo].[Responses]
ADD CONSTRAINT [PK_Responses]
    PRIMARY KEY CLUSTERED ([ResponseId] ASC);
GO

-- Creating primary key on [RoleId] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([RoleId] ASC);
GO

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- Creating primary key on [UserClaimId] in table 'UserClaims'
ALTER TABLE [dbo].[UserClaims]
ADD CONSTRAINT [PK_UserClaims]
    PRIMARY KEY CLUSTERED ([UserClaimId] ASC);
GO

-- Creating primary key on [LoginProvider], [ProviderKey], [UserId] in table 'UserLogins'
ALTER TABLE [dbo].[UserLogins]
ADD CONSTRAINT [PK_UserLogins]
    PRIMARY KEY CLUSTERED ([LoginProvider], [ProviderKey], [UserId] ASC);
GO

-- Creating primary key on [SupplyId] in table 'Supplies'
ALTER TABLE [dbo].[Supplies]
ADD CONSTRAINT [PK_Supplies]
    PRIMARY KEY CLUSTERED ([SupplyId] ASC);
GO

-- Creating primary key on [Roles_RoleId], [Users_UserId] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [PK_UserRole]
    PRIMARY KEY CLUSTERED ([Roles_RoleId], [Users_UserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'UserClaims'
ALTER TABLE [dbo].[UserClaims]
ADD CONSTRAINT [FK_dbo_UserClaims_dbo_Users_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_UserClaims_dbo_Users_UserId'
CREATE INDEX [IX_FK_dbo_UserClaims_dbo_Users_UserId]
ON [dbo].[UserClaims]
    ([UserId]);
GO

-- Creating foreign key on [UserId] in table 'UserLogins'
ALTER TABLE [dbo].[UserLogins]
ADD CONSTRAINT [FK_dbo_UserLogins_dbo_Users_UserId]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_dbo_UserLogins_dbo_Users_UserId'
CREATE INDEX [IX_FK_dbo_UserLogins_dbo_Users_UserId]
ON [dbo].[UserLogins]
    ([UserId]);
GO

-- Creating foreign key on [Roles_RoleId] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [FK_UserRole_Roles]
    FOREIGN KEY ([Roles_RoleId])
    REFERENCES [dbo].[Roles]
        ([RoleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Users_UserId] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [FK_UserRole_User]
    FOREIGN KEY ([Users_UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRole_User'
CREATE INDEX [IX_FK_UserRole_User]
ON [dbo].[UserRole]
    ([Users_UserId]);
GO

-- Creating foreign key on [RequestId] in table 'Responses'
ALTER TABLE [dbo].[Responses]
ADD CONSTRAINT [FK_RequestResponse]
    FOREIGN KEY ([RequestId])
    REFERENCES [dbo].[Requests]
        ([RequestId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RequestResponse'
CREATE INDEX [IX_FK_RequestResponse]
ON [dbo].[Responses]
    ([RequestId]);
GO

-- Creating foreign key on [LocationSupplyId] in table 'Requests'
ALTER TABLE [dbo].[Requests]
ADD CONSTRAINT [FK_LocationRequest]
    FOREIGN KEY ([LocationSupplyId])
    REFERENCES [dbo].[Locations]
        ([LocationId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LocationRequest'
CREATE INDEX [IX_FK_LocationRequest]
ON [dbo].[Requests]
    ([LocationSupplyId]);
GO

-- Creating foreign key on [CategoryId] in table 'Requests'
ALTER TABLE [dbo].[Requests]
ADD CONSTRAINT [FK_RequestCategory]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Categories]
        ([CategoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RequestCategory'
CREATE INDEX [IX_FK_RequestCategory]
ON [dbo].[Requests]
    ([CategoryId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------