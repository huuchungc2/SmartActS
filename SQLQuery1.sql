CREATE TABLE [dbo].[Request] (
    [RequestId]        INT             IDENTITY (1, 1) NOT NULL,
    [RequestCode]      VARCHAR (50)    NULL,
    [CategoryId]       INT             NULL,
    [CustomerId]       INT             NULL,
    [Status]           INT             DEFAULT ((0)) NULL,
    [CreatedDate]      DATETIME        NULL,
    [DurationExpired]  INT             DEFAULT ((1)) NULL,
    [FromBudget]       MONEY           NULL,
    [ToBudget]         MONEY           NULL,
    [RequireResponse]  INT             DEFAULT ((1)) NULL,
    [ShippingAddress]  NVARCHAR (1000) NULL,
    [LocationSupplyId] INT             NULL,
    [Description]      NVARCHAR (1000) NULL,
    [BestTime]         BIT             NULL,
    [BestSupply]       BIT             NULL,
    [BestPrice]        BIT             NULL,
    [RequestTitle] NVARCHAR(100) NULL, 
    PRIMARY KEY CLUSTERED ([RequestId] ASC)
);

