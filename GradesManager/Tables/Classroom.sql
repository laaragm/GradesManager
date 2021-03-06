CREATE TABLE [dbo].[Classroom]
(
	[ID]        BIGINT          IDENTITY (1,1) NOT NULL,
	[School]    BIGINT          NOT NULL,
	[Level]     BIGINT          NOT NULL,
	[Name]      NVARCHAR(MAX)   NULL,
	[Year]      BIGINT          NULL,
	[Creation]  DATETIME        NULL,
	[Exclusion] DATETIME        NULL,
	CONSTRAINT [PK_Classroom] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_Classroom_School] FOREIGN KEY ([School]) REFERENCES [dbo].[School] ([ID]),
)
