IF EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'AssessmentId' AND Object_ID = Object_ID(N'mstCompany'))
BEGIN
    alter table mstCompany drop constraint FK_mstCompany_mstAssessmentSet_AssessmentId

    alter table mstCompany drop column AssessmentId
END

IF EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'IsBulkSentRequire' AND Object_ID = Object_ID(N'mstHumanResourceRepo'))
BEGIN
    alter table mstHumanResourceRepo drop column IsBulkSentRequire
END

IF EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'IsReportSentToCandidate' AND Object_ID = Object_ID(N'mstHumanResourceRepo'))
BEGIN
    alter table mstHumanResourceRepo drop column IsReportSentToCandidate
END

IF EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'IsReportSentToHr' AND Object_ID = Object_ID(N'mstHumanResourceRepo'))
BEGIN
    alter table mstHumanResourceRepo drop column IsReportSentToHr
END

IF EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'LinkCount' AND Object_ID = Object_ID(N'mstHumanResourceRepo'))
BEGIN
    alter table mstHumanResourceRepo drop column LinkCount
END

IF EXISTS(SELECT 1 FROM sys.columns WHERE NAME = N'HrId' AND Object_ID = Object_ID(N'txnCandidate'))
BEGIN

	alter table txnCandidate add HrId int not null DEFAULT 2

	ALTER TABLE txnCandidate ADD CONSTRAINT FK_TxnCandidate_mstHumanResource_HrId FOREIGN KEY (HrId)
    REFERENCES mstHumanResourceRepo(HrId);
END

IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'mstmailConfigByAssessment'))
BEGIN
			CREATE TABLE [dbo].[mstmailConfigByAssessment](
			[MailConfigId] [int] IDENTITY(1,1) NOT NULL,
			[MailType] [varchar](50) NULL,
			[MailConfigName] [nvarchar](250) NULL,
			[AssessmentId] [int] NULL,
			[MailTemplateId] [int] NULL,
		 CONSTRAINT [PK_mstmailConfigByAssessment_MailConfigId] PRIMARY KEY CLUSTERED 
		(
			[MailConfigId] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]

		ALTER TABLE [dbo].[mstmailConfigByAssessment]  WITH CHECK ADD  CONSTRAINT [FK_mstmailConfigByAssessment_mstAssessmentSet_AssessmentId] FOREIGN KEY([AssessmentId])
		REFERENCES [dbo].[mstAssessmentSet] ([AssessmentId])

		ALTER TABLE [dbo].[mstmailConfigByAssessment] CHECK CONSTRAINT [FK_mstmailConfigByAssessment_mstAssessmentSet_AssessmentId]

		ALTER TABLE [dbo].[mstmailConfigByAssessment]  WITH CHECK ADD  CONSTRAINT [FK_mstmailConfigByAssessment_mstMailTemplate_MailTemplateId] FOREIGN KEY([MailTemplateId])
		REFERENCES [dbo].[mstMailTemplate] ([MailTemplateId])

		ALTER TABLE [dbo].[mstmailConfigByAssessment] CHECK CONSTRAINT [FK_mstmailConfigByAssessment_mstMailTemplate_MailTemplateId]


END






IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'txnCandidateLinkHistory'))
BEGIN
			CREATE TABLE [dbo].[txnCandidateLinkHistory](
				[LinkHisId] [int] IDENTITY(1,1) NOT NULL,
				[TestId] [int] NULL,
				[InitialMailTemplateId] [int] NULL,
				[FinalMailTemplateId] [int] NULL,
				[ReportSendToHr] [bit] NULL,
				[ReportSendToCandidate] [bit] NULL,
			 CONSTRAINT [PK_txnCandidateLinkHistory_LinkHisId] PRIMARY KEY CLUSTERED 
			(
				[LinkHisId] ASC
			)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
			) ON [PRIMARY]

			ALTER TABLE [dbo].[txnCandidateLinkHistory]  WITH CHECK ADD  CONSTRAINT [FK_txnCandidateLinkHistory_mstMailTemplate_FinalMailTemplateId] FOREIGN KEY([FinalMailTemplateId])
			REFERENCES [dbo].[mstMailTemplate] ([MailTemplateId])

			ALTER TABLE [dbo].[txnCandidateLinkHistory] CHECK CONSTRAINT [FK_txnCandidateLinkHistory_mstMailTemplate_FinalMailTemplateId]

			ALTER TABLE [dbo].[txnCandidateLinkHistory]  WITH CHECK ADD  CONSTRAINT [FK_txnCandidateLinkHistory_mstMailTemplate_InitialMailTemplateId] FOREIGN KEY([InitialMailTemplateId])
			REFERENCES [dbo].[mstMailTemplate] ([MailTemplateId])

			ALTER TABLE [dbo].[txnCandidateLinkHistory] CHECK CONSTRAINT [FK_txnCandidateLinkHistory_mstMailTemplate_InitialMailTemplateId]

			ALTER TABLE [dbo].[txnCandidateLinkHistory]  WITH CHECK ADD  CONSTRAINT [FK_txnCandidateLinkHistory_txnUserTestDetails_TestId] FOREIGN KEY([TestId])
			REFERENCES [dbo].[txnUserTestDetails] ([TestId])

			ALTER TABLE [dbo].[txnCandidateLinkHistory] CHECK CONSTRAINT [FK_txnCandidateLinkHistory_txnUserTestDetails_TestId]


END


IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'mstUserLogin'))
BEGIN

	
CREATE TABLE [dbo].[mstUserLogin](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[EmailId] [nvarchar](100) NULL,
	[MobileNo] [nvarchar](100) NULL,
	[IsActive] [bit] NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [int] NULL,
	[LastModifiedAt] [datetime] NULL,
	[LastModifiedBy] [int] NULL,
 CONSTRAINT [PK_mstUserLogin] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

END


Alter table [dbo].[mstUserLogin] add RefreshToken Nvarchar(max)
Alter table [dbo].[mstUserLogin] add RefreshTokenExpiryTime DateTime


IF (NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'txnCampanyMapToAssessment'))
BEGIN	
		CREATE TABLE [dbo].[txnCampanyMapToAssessment](
			[CoAsId] [int] IDENTITY(1,1) NOT NULL,
			[CompanyId] [int] NULL,
			[AssessmentId] [int] NULL,
			[CreatedAt] [datetime] NULL,
			[CreatedBy] [varchar](50) NULL,
			[LastModifiedAt] [datetime] NULL,
			[LastModifiedBy] [varchar](50) NULL,
			[isactive] [bit] NULL,
		 CONSTRAINT [PK_txnCampanyMapToAssessment_CoAsId] PRIMARY KEY CLUSTERED 
		(
			[CoAsId] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]

		ALTER TABLE [dbo].[txnCampanyMapToAssessment]  WITH CHECK ADD  CONSTRAINT [FK_txnCampanyMapToAssessment_mstCompany_CompanyId] FOREIGN KEY([CompanyId])
		REFERENCES [dbo].[mstCompany] ([CompanyId])

		ALTER TABLE [dbo].[txnCampanyMapToAssessment] CHECK CONSTRAINT [FK_txnCampanyMapToAssessment_mstCompany_CompanyId]

		ALTER TABLE [dbo].[txnCampanyMapToAssessment]  WITH CHECK ADD  CONSTRAINT [FK_txnCampanyMapToAssessment_mstAssessmentSet_CompanyId] FOREIGN KEY([AssessmentId])
		REFERENCES [dbo].[mstAssessmentSet] ([AssessmentId])

		ALTER TABLE [dbo].[txnCampanyMapToAssessment] CHECK CONSTRAINT [FK_txnCampanyMapToAssessment_mstAssessmentSet_CompanyId]

End




IF (Not EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND  TABLE_NAME = 'txnHrMapToCompanyAndAssessment'))
BEGIN	
		CREATE TABLE [dbo].[txnHrMapToCompanyAndAssessment](
			[HrCoAsId] [int] IDENTITY(1,1) NOT NULL,
			[HrId] [int] NULL,
			[CompanyId] [int] NULL,
			[AssessmentId] [int] NULL,
			[CreatedAt] [datetime] NULL,
			[CreatedBy] [varchar](50) NULL,
			[LastModifiedAt] [datetime] NULL,
			[LastModifiedBy] [varchar](50) NULL,
			[isactive] [bit] NULL,
		 CONSTRAINT [PK_txnHrMapToCompanyAndAssessment_HrCoAsId] PRIMARY KEY CLUSTERED 
		(
			[HrCoAsId] ASC
		)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
		) ON [PRIMARY]

		ALTER TABLE [dbo].[txnHrMapToCompanyAndAssessment]  WITH CHECK ADD  CONSTRAINT [FK_txnHrMapToCompanyAndAssessment_mstCompany_CompanyId] FOREIGN KEY([CompanyId])
		REFERENCES [dbo].[mstCompany] ([CompanyId])

		ALTER TABLE [dbo].[txnHrMapToCompanyAndAssessment] CHECK CONSTRAINT [FK_txnHrMapToCompanyAndAssessment_mstCompany_CompanyId]

		ALTER TABLE [dbo].[txnHrMapToCompanyAndAssessment]  WITH CHECK ADD  CONSTRAINT [FK_txnHrMapToCompanyAndAssessment_mstAssessmentSet_CompanyId] FOREIGN KEY([AssessmentId])
		REFERENCES [dbo].[mstAssessmentSet] ([AssessmentId])

		ALTER TABLE [dbo].[txnHrMapToCompanyAndAssessment] CHECK CONSTRAINT [FK_txnHrMapToCompanyAndAssessment_mstAssessmentSet_CompanyId]


		ALTER TABLE [dbo].[txnHrMapToCompanyAndAssessment]  WITH CHECK ADD  CONSTRAINT [FK_txnHrMapToCompanyAndAssessment_mstHumanResourceRepo_HrId] FOREIGN KEY([HrId])
		REFERENCES [dbo].[mstHumanResourceRepo] ([HrId])

		ALTER TABLE [dbo].[txnHrMapToCompanyAndAssessment] CHECK CONSTRAINT [FK_txnHrMapToCompanyAndAssessment_mstHumanResourceRepo_HrId]

End
