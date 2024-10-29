IF EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'AssessmentId' AND Object_ID = Object_ID(N'mstCompany'))
BEGIN
    alter table mstCompany drop constraint FK_mstCompany_mstAssessmentSet_AssessmentId

    alter table mstCompany drop column AssessmentId
END

IF EXISTS(SELECT 1 FROM sys.columns WHERE Name = N'CompanyId' AND Object_ID = Object_ID(N'mstHumanResourceRepo'))
BEGIN
    ALTER TABLE mstHumanResourceRepo DROP CONSTRAINT FK_mstHumanResourceRepo_mstCompany_CompanyId

    alter table mstHumanResourceRepo drop column CompanyId
END
