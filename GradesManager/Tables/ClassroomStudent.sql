CREATE TABLE [dbo].[ClassroomStudent]
(
	[ID]         BIGINT     IDENTITY (1,1) NOT NULL,
	[Classroom]  BIGINT     NOT NULL,
	[Student]    BIGINT     NOT NULL,
	CONSTRAINT [PK_ClassroomStudent] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_ClassroomStudent_Classroom] FOREIGN KEY ([Classroom]) REFERENCES [dbo].[Classroom] ([ID]),
	CONSTRAINT [FK_ClassroomStudent_Student] FOREIGN KEY ([Student]) REFERENCES [dbo].[Student] ([ID]),
)
