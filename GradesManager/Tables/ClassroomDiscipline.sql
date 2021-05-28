CREATE TABLE [dbo].[ClassroomDiscipline]
(
	[ID]         BIGINT        IDENTITY (1,1) NOT NULL,
	[Classroom]  BIGINT        NOT NULL,
	[Discipline] BIGINT        NOT NULL,
	[Teacher]    NVARCHAR(MAX) NOT NULL,
	CONSTRAINT [PK_ClassroomDiscipline] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_ClassroomDiscipline_Classroom] FOREIGN KEY ([Classroom]) REFERENCES [dbo].[Classroom] ([ID]),
	CONSTRAINT [FK_ClassroomDiscipline_Discipline] FOREIGN KEY ([Discipline]) REFERENCES [dbo].[Discipline] ([ID]),
)
