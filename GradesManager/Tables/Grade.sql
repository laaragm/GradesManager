CREATE TABLE [dbo].[Grade]
(
	[ID]                  BIGINT          IDENTITY (1,1) NOT NULL,
	[Student]             BIGINT          NOT NULL,
	[Discipline]          BIGINT          NOT NULL,
	[Classroom]           BIGINT          NOT NULL,
	[TotalValue]          DECIMAL(5,2)    NOT NULL,
	[ObtainedValue]       DECIMAL(5,2)    NOT NULL,
	[Creation]            DATETIME        NULL,
	[Exclusion]           DATETIME        NULL,
	CONSTRAINT [PK_Grade] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_Grade_Student] FOREIGN KEY ([Student]) REFERENCES [dbo].[Student] ([ID]),
	CONSTRAINT [FK_Grade_Discipline] FOREIGN KEY ([Discipline]) REFERENCES [dbo].[Discipline] ([ID]),
	CONSTRAINT [FK_Grade_Classroom] FOREIGN KEY ([Classroom]) REFERENCES [dbo].[Classroom] ([ID]),
)
