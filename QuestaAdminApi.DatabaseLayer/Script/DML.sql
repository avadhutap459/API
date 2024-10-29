INSERT INTO [dbo].[mst_mailConfigByAssessment](MailType,MailConfigName,AssessmentId,MailTemplateId)
Values('Initial','Inital_Qlead',3,5),('Final','Final_Qlead',3,4),
('Initial','Inital_Qtam',4,5),('Final','Final_Qtam',4,4),
('Initial','Inital_StandardReport',5,5),('Final','Final_StandardReport',5,4),
('Initial','Inital_PremiumReport',6,5),('Final','Final_PremiumReport',6,4),
('Initial','Inital_QLeap',9,5),('Final','Final_QLeap',9,4),
('Initial','Inital_QMap',10,6),('Final','Final_QMap',10,4),
('Initial','Inital_Qpre',11,5),('Final','Final_Qpre',11,4)




update mstCompany set IsActive = 0

SET IDENTITY_INSERT [dbo].[mstCompany] ON 
Insert into mstCompany(CompanyId,CompanyName,CreatedAt,CreatedBy,LastModifiedAt,LastModifiedBy,IsActive)
values(21,'Questa Enneagram',GETDATE(),100,GETDATE(),100,1),
(22,'Ummeedhfc',GETDATE(),100,GETDATE(),100,1),
(23,'Home housing finace',GETDATE(),100,GETDATE(),100,1),
(24,'NTPC',GETDATE(),100,GETDATE(),100,1),
(25,'Captial',GETDATE(),100,GETDATE(),100,1),
(26,'Kotak Bank',GETDATE(),100,GETDATE(),100,1),
(27,'Vriddhi Home Finance Limited',GETDATE(),100,GETDATE(),100,1),
(29,'shriram life insurance ltd',GETDATE(),100,GETDATE(),100,1)
SET IDENTITY_INSERT [dbo].[mstCompany] OFF

INSERT INTO [dbo].[txnCampanyMapToAssessment](CompanyId,AssessmentId,CreatedAt,CreatedBy,LastModifiedAt,LastModifiedBy,isactive)
values(21,1,GETDATE(),100,GETDATE(),100,1),
(21,2,GETDATE(),100,GETDATE(),100,1),
(21,3,GETDATE(),100,GETDATE(),100,1),
(21,4,GETDATE(),100,GETDATE(),100,1),
(21,5,GETDATE(),100,GETDATE(),100,1),
(21,6,GETDATE(),100,GETDATE(),100,1),
(21,9,GETDATE(),100,GETDATE(),100,1),
(21,10,GETDATE(),100,GETDATE(),100,1),
(21,11,GETDATE(),100,GETDATE(),100,1),
(22,1,GETDATE(),100,GETDATE(),100,1),
(23,1,GETDATE(),100,GETDATE(),100,1),
(28,1,GETDATE(),100,GETDATE(),100,1),
(24,10,GETDATE(),100,GETDATE(),100,1),
(25,1,GETDATE(),100,GETDATE(),100,1),
(26,1,GETDATE(),100,GETDATE(),100,1),
(27,1,GETDATE(),100,GETDATE(),100,1),
(29,9,GETDATE(),100,GETDATE(),100,1)



SET IDENTITY_INSERT [dbo].[mstHumanResourceRepo] ON 
Insert into mstHumanResourceRepo(HrId,HrName,HrEmail,HrPhoneNumber,IsActive)
Values(21,'Lata Verghese','support@questa.in','9811100811',1),
(22,'Raji Verghese','support@questa.in','9711637185',1),
(23,'Divya Kaul','support@questa.in','9711631148',1),
(24,'Quest Enneagram','support@questa.in','9811100811',1),
(25,'Avadhut Parab','avadhutap459@gmail.com','9619725962',1),
(26,'Beenata Lawrence','beenata.lawrence@ummeedhfc.com','9310998779',1),
(27,'Priya Datta','pdjoshi@ahamhfc.com','9337285600',1),
(28,'Employee Referral','employee.referral@capitalindia.com','9811100811',1)
SET IDENTITY_INSERT [dbo].[mstHumanResourceRepo] OFF 





INSERT INTO [dbo].[txnHrMapToCompanyAndAssessment](HrId,CompanyId,AssessmentId,CreatedAt,CreatedBy,LastModifiedAt,LastModifiedBy,isactive)
Values(21,21,1,GETDATE(),100,GETDATE(),100,1),
(21,21,2,GETDATE(),100,GETDATE(),100,1),
(21,21,3,GETDATE(),100,GETDATE(),100,1),
(21,21,4,GETDATE(),100,GETDATE(),100,1),
(21,21,5,GETDATE(),100,GETDATE(),100,1),
(21,21,6,GETDATE(),100,GETDATE(),100,1),
(21,21,9,GETDATE(),100,GETDATE(),100,1),
(21,21,10,GETDATE(),100,GETDATE(),100,1),
(21,21,11,GETDATE(),100,GETDATE(),100,1),
(22,21,1,GETDATE(),100,GETDATE(),100,1),
(22,21,2,GETDATE(),100,GETDATE(),100,1),
(22,21,3,GETDATE(),100,GETDATE(),100,1),
(22,21,4,GETDATE(),100,GETDATE(),100,1),
(22,21,5,GETDATE(),100,GETDATE(),100,1),
(22,21,6,GETDATE(),100,GETDATE(),100,1),
(22,21,9,GETDATE(),100,GETDATE(),100,1),
(22,21,10,GETDATE(),100,GETDATE(),100,1),
(22,21,11,GETDATE(),100,GETDATE(),100,1),
(23,21,1,GETDATE(),100,GETDATE(),100,1),
(23,21,2,GETDATE(),100,GETDATE(),100,1),
(23,21,3,GETDATE(),100,GETDATE(),100,1),
(23,21,4,GETDATE(),100,GETDATE(),100,1),
(23,21,5,GETDATE(),100,GETDATE(),100,1),
(23,21,6,GETDATE(),100,GETDATE(),100,1),
(23,21,9,GETDATE(),100,GETDATE(),100,1),
(23,21,10,GETDATE(),100,GETDATE(),100,1),
(23,21,11,GETDATE(),100,GETDATE(),100,1),
(25,21,1,GETDATE(),100,GETDATE(),100,1),
(25,21,2,GETDATE(),100,GETDATE(),100,1),
(25,21,3,GETDATE(),100,GETDATE(),100,1),
(25,21,4,GETDATE(),100,GETDATE(),100,1),
(25,21,5,GETDATE(),100,GETDATE(),100,1),
(25,21,6,GETDATE(),100,GETDATE(),100,1),
(25,21,9,GETDATE(),100,GETDATE(),100,1),
(25,21,10,GETDATE(),100,GETDATE(),100,1),
(25,21,11,GETDATE(),100,GETDATE(),100,1),
(26,22,1,GETDATE(),100,GETDATE(),100,1),
(27,23,1,GETDATE(),100,GETDATE(),100,1),
(21,24,10,GETDATE(),100,GETDATE(),100,1),
(28,25,1,GETDATE(),100,GETDATE(),100,1),
(24,26,1,GETDATE(),100,GETDATE(),100,1),
(24,27,1,GETDATE(),100,GETDATE(),100,1),
(24,29,9,GETDATE(),100,GETDATE(),100,1)






Insert into [dbo].[mst_mailConfigByAssessment](MailType,MailConfigName,AssessmentId,MailTemplateId)
Values('Initial','Initial_Qsser_PartA',1,1),
('Final','Final_Qsser_PartA',1,2),
('Initial','Initial_Qsser_PartB',2,1),
('Final','Final_Qsser_PartB',2,2)
