CREATE TABLE [dbo].[Discipline]
(
	[ID]                  BIGINT          IDENTITY (1,1) NOT NULL,
	[Name]                NVARCHAR(MAX)   NOT NULL,
	[Creation]            DATETIME        NULL,
	[Exclusion]           DATETIME        NULL,
	CONSTRAINT [PK_Discipline] PRIMARY KEY CLUSTERED ([ID] ASC),
)
