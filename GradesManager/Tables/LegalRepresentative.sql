CREATE TABLE [dbo].[LegalRepresentative]
(
	[ID]          BIGINT          IDENTITY (1,1) NOT NULL,
	[Name]        NVARCHAR(MAX)   NOT NULL,
	[PhoneNumber] NVARCHAR(MAX)   NOT NULL,
	[Creation]    DATETIME        NULL,
	[Exclusion]   DATETIME        NULL,
	CONSTRAINT [PK_LegalRepresentative] PRIMARY KEY CLUSTERED ([ID] ASC),
)
