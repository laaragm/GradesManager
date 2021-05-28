CREATE TABLE [dbo].[School]
(
	[ID]                  BIGINT          IDENTITY (1,1) NOT NULL,
	[Name]                NVARCHAR(MAX)   NOT NULL,
	[Owner]               NVARCHAR(MAX)   NOT NULL,
	[Principal]           NVARCHAR(MAX)   NOT NULL,
	[Address]             NVARCHAR(MAX)   NOT NULL,
	[PhoneNumber]         NVARCHAR(MAX)   NOT NULL,
	[CNPJ]                NVARCHAR(MAX)   NOT NULL,
	[Creation]            DATETIME        NULL,
	[Exclusion]           DATETIME        NULL,
	CONSTRAINT [PK_School] PRIMARY KEY CLUSTERED ([ID] ASC),
)
