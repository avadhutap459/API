/****** Object:  StoredProcedure [dbo].[sp_generatelinkbaseonrequiredetails]    Script Date: 12-03-2024 18:11:31 ******/
DROP PROCEDURE [dbo].[sp_generatelinkbaseonrequiredetails]
GO

/****** Object:  StoredProcedure [dbo].[sp_generatelinkbaseonrequiredetails]    Script Date: 12-03-2024 18:11:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_generatelinkbaseonrequiredetails]
	@AssessmentId int,
	@CompanyId int,
	@HrId int,
	@InitialMailId int,
	@FinalMailId int,
	@IsReportSendToHr int,
	@IsReportSendToCandidate int,
	@OutputTestId INT OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	BEGIN TRANSACTION
		BEGIN TRY
			
			DECLARE @USERID INT
			DECLARE @TestId INT

			INSERT INTO [dbo].[txnCandidate](CreatedAt,LastModified,IsOTPRequire,IsActive,AssessmentId,CompanyId,HrId)
									  VALUES(SWITCHOFFSET(SYSDATETIMEOFFSET(),'+05:30'),SWITCHOFFSET(SYSDATETIMEOFFSET(),'+05:30'),
												0,1,@AssessmentId,@CompanyId,@HrId)
			SET @USERID = SCOPE_IDENTITY();

			EXEC [dbo].[Sp_InsertRandomQuestion] @USERID,NULL,NULL,NULL,@AssessmentId,NULL,@TestId OUTPUT

			INSERT INTO [dbo].[txnCandidateLinkHistory](TestId,InitialMailTemplateId,FinalMailTemplateId,ReportSendToHr,ReportSendToCandidate)
												 VALUES(@TestId,@InitialMailId,@FinalMailId,@IsReportSendToHr,@IsReportSendToCandidate)

			SELECT @OutputTestId = @TestId

			SELECT @OutputTestId

			COMMIT TRANSACTION;
		END TRY
		BEGIN CATCH
			
			SELECT ERROR_NUMBER() As ErrorNumber,
				   ERROR_STATE() AS ErrorState,
				   ERROR_SEVERITY() As ErrorSeverity,
				   ERROR_PROCEDURE() As ErrorProcedure,
				   ERROR_LINE() As ErrorLine,
				   ERROR_MESSAGE() As ErrorMessage

			ROLLBACK TRANSACTION;
		END CATCH
END
GO



