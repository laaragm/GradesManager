CREATE TABLE [dbo].[Student]
(
	[ID]                  BIGINT          IDENTITY (1,1) NOT NULL,
	[Name]                NVARCHAR(MAX)   NOT NULL,
	[LegalRepresentative] BIGINT          NOT NULL,
	[Birthday]            DATETIME        NULL,
	[Address]             NVARCHAR(MAX)   NULL,
	[Creation]            DATETIME        NULL,
	[Exclusion]           DATETIME        NULL,
	CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_Student_LegalRepresentative] FOREIGN KEY ([LegalRepresentative]) REFERENCES [dbo].[LegalRepresentative] ([ID]),
)
