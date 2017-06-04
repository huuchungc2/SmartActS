USE [SmartActS]
GO

/****** Object: Table [dbo].[Response] Script Date: 6/3/2017 9:26:29 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

DROP TABLE [dbo].[Response];


GO
CREATE TABLE [dbo].[Response] (
    [ResponseId]   INT             IDENTITY (1, 1) NOT NULL,
	ResponseTitle nvarchar(100) not null,
    [RequestId]    INT             NULL,
    [SupplyId]     INT             NULL,
    [ResponseTime] DATETIME        NULL,
    [Status]       INT             NULL,
    [PriceSuggest] MONEY           NULL,
    [Description]  NVARCHAR (1000) NULL,
    [FileAttachId] INT             NULL
);


