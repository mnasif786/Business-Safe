

CREATE NONCLUSTERED INDEX [_dta_index_Task_33_1269579561__K19_K22_K1_K11_K6_K13_K21_K16_K2_K23_K24_K12_3_4_5_7_8_9_10_14_15_17_18_20_25_26_27_28] ON [dbo].[Task] 
(
	[FollowingTaskId] ASC,
	[RiskAssessmentReviewId] ASC,
	[Id] ASC,
	[TaskAssignedToId] ASC,
	[Deleted] ASC,
	[TaskStatusId] ASC,
	[Discriminator] ASC,
	[TaskCategoryId] ASC,
	[MultiHazardRiskAssessmentHazardId] ASC,
	[HazardousSubstanceRiskAssessmentId] ASC,
	[SignificantFindingId] ASC,
	[TaskCompletionDueDate] ASC
)
INCLUDE ( [Title],
[Description],
[Reference],
[CreatedOn],
[CreatedBy],
[LastModifiedOn],
[LastModifiedBy],
[TaskCompletedDate],
[TaskCompletedComments],
[TaskReoccurringTypeId],
[TaskReoccurringEndDate],
[OriginalTaskId],
[TaskGuid],
[SendTaskNotification],
[SendTaskCompletedNotification],
[SendTaskOverdueNotification]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE NONCLUSTERED INDEX [_dta_index_Task_33_1269579561__K6_K21_K11_K19_K1_K13_K22_K16_K2_K23_K24_K12_3_4_5_7_8_9_10_14_15_17_18_20_25_26_27_28] ON [dbo].[Task] 
(
	[Deleted] ASC,
	[Discriminator] ASC,
	[TaskAssignedToId] ASC,
	[FollowingTaskId] ASC,
	[Id] ASC,
	[TaskStatusId] ASC,
	[RiskAssessmentReviewId] ASC,
	[TaskCategoryId] ASC,
	[MultiHazardRiskAssessmentHazardId] ASC,
	[HazardousSubstanceRiskAssessmentId] ASC,
	[SignificantFindingId] ASC,
	[TaskCompletionDueDate] ASC
)
INCLUDE ( [Title],
[Description],
[Reference],
[CreatedOn],
[CreatedBy],
[LastModifiedOn],
[LastModifiedBy],
[TaskCompletedDate],
[TaskCompletedComments],
[TaskReoccurringTypeId],
[TaskReoccurringEndDate],
[OriginalTaskId],
[TaskGuid],
[SendTaskNotification],
[SendTaskCompletedNotification],
[SendTaskOverdueNotification]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE NONCLUSTERED INDEX [_dta_index_Task_33_1269579561__K21_K22_K1_K19_K11_K6_K13_K16_K2_K23_K24_K12_3_4_5_7_8_9_10_14_15_17_18_20_25_26_27_28] ON [dbo].[Task] 
(
	[Discriminator] ASC,
	[RiskAssessmentReviewId] ASC,
	[Id] ASC,
	[FollowingTaskId] ASC,
	[TaskAssignedToId] ASC,
	[Deleted] ASC,
	[TaskStatusId] ASC,
	[TaskCategoryId] ASC,
	[MultiHazardRiskAssessmentHazardId] ASC,
	[HazardousSubstanceRiskAssessmentId] ASC,
	[SignificantFindingId] ASC,
	[TaskCompletionDueDate] ASC
)
INCLUDE ( [Title],
[Description],
[Reference],
[CreatedOn],
[CreatedBy],
[LastModifiedOn],
[LastModifiedBy],
[TaskCompletedDate],
[TaskCompletedComments],
[TaskReoccurringTypeId],
[TaskReoccurringEndDate],
[OriginalTaskId],
[TaskGuid],
[SendTaskNotification],
[SendTaskCompletedNotification],
[SendTaskOverdueNotification]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE STATISTICS [_dta_stat_1269579561_1_6_21_11] ON [dbo].[Task]([Id], [Deleted], [Discriminator], [TaskAssignedToId])
go

CREATE STATISTICS [_dta_stat_1269579561_12_6_21_11] ON [dbo].[Task]([TaskCompletionDueDate], [Deleted], [Discriminator], [TaskAssignedToId])
go

CREATE STATISTICS [_dta_stat_1269579561_22_6_21_11_19] ON [dbo].[Task]([RiskAssessmentReviewId], [Deleted], [Discriminator], [TaskAssignedToId], [FollowingTaskId])
go

CREATE STATISTICS [_dta_stat_1269579561_13_22_1_19_11] ON [dbo].[Task]([TaskStatusId], [RiskAssessmentReviewId], [Id], [FollowingTaskId], [TaskAssignedToId])
go

CREATE STATISTICS [_dta_stat_1269579561_16_6_21_11_19_1] ON [dbo].[Task]([TaskCategoryId], [Deleted], [Discriminator], [TaskAssignedToId], [FollowingTaskId], [Id])
go

CREATE STATISTICS [_dta_stat_1269579561_2_6_21_11_19_1] ON [dbo].[Task]([MultiHazardRiskAssessmentHazardId], [Deleted], [Discriminator], [TaskAssignedToId], [FollowingTaskId], [Id])
go

CREATE STATISTICS [_dta_stat_1269579561_24_6_21_11_19_1] ON [dbo].[Task]([SignificantFindingId], [Deleted], [Discriminator], [TaskAssignedToId], [FollowingTaskId], [Id])
go

CREATE STATISTICS [_dta_stat_1269579561_23_6_21_11_19_1] ON [dbo].[Task]([HazardousSubstanceRiskAssessmentId], [Deleted], [Discriminator], [TaskAssignedToId], [FollowingTaskId], [Id])
go

CREATE STATISTICS [_dta_stat_1269579561_6_21_11_19_1_13_16] ON [dbo].[Task]([Deleted], [Discriminator], [TaskAssignedToId], [FollowingTaskId], [Id], [TaskStatusId], [TaskCategoryId])
go

CREATE STATISTICS [_dta_stat_1269579561_22_1_19_11_16_6_13] ON [dbo].[Task]([RiskAssessmentReviewId], [Id], [FollowingTaskId], [TaskAssignedToId], [TaskCategoryId], [Deleted], [TaskStatusId])
go

CREATE STATISTICS [_dta_stat_1269579561_6_21_11_19_1_13_2_22] ON [dbo].[Task]([Deleted], [Discriminator], [TaskAssignedToId], [FollowingTaskId], [Id], [TaskStatusId], [MultiHazardRiskAssessmentHazardId], [RiskAssessmentReviewId])
go

CREATE STATISTICS [_dta_stat_1269579561_6_21_11_19_1_13_23_22_16] ON [dbo].[Task]([Deleted], [Discriminator], [TaskAssignedToId], [FollowingTaskId], [Id], [TaskStatusId], [HazardousSubstanceRiskAssessmentId], [RiskAssessmentReviewId], [TaskCategoryId])
go

CREATE STATISTICS [_dta_stat_1269579561_11_6_13_21_22_1_19_16_12] ON [dbo].[Task]([TaskAssignedToId], [Deleted], [TaskStatusId], [Discriminator], [RiskAssessmentReviewId], [Id], [FollowingTaskId], [TaskCategoryId], [TaskCompletionDueDate])
go

CREATE STATISTICS [_dta_stat_1269579561_22_1_19_11_16_2_23_24_6_13] ON [dbo].[Task]([RiskAssessmentReviewId], [Id], [FollowingTaskId], [TaskAssignedToId], [TaskCategoryId], [MultiHazardRiskAssessmentHazardId], [HazardousSubstanceRiskAssessmentId], [SignificantFindingId], [Deleted], [TaskStatusId])
go

CREATE STATISTICS [_dta_stat_1269579561_6_21_11_19_1_13_24_22_16_2_23_12] ON [dbo].[Task]([Deleted], [Discriminator], [TaskAssignedToId], [FollowingTaskId], [Id], [TaskStatusId], [SignificantFindingId], [RiskAssessmentReviewId], [TaskCategoryId], [MultiHazardRiskAssessmentHazardId], [HazardousSubstanceRiskAssessmentId], [TaskCompletionDueDate])
go

CREATE NONCLUSTERED INDEX [_dta_index_Employee_33_933578364__K32_K1_2_3_4_5_6_7_8_9_10_11_12_13_14_15_16_17_18_19_20_21_22_23_24_25_26_27_28_29_30_31] ON [dbo].[Employee] 
(
	[ClientId] ASC,
	[Id] ASC
)
INCLUDE ( [Forename],
[Surname],
[Title],
[PreviousSurname],
[MiddleName],
[DateOfBirth],
[NationalityId],
[Sex],
[HasDisability],
[DisabilityDescription],
[NINumber],
[DrivingLicenseNumber],
[DrivingLicenseExpirationDate],
[WorkVisaNumber],
[WorkVisaExpirationDate],
[PPSNumber],
[PassportNumber],
[HasCompanyVehicle],
[CompanyVehicleRegistration],
[CompanyVehicleTypeId],
[SiteId],
[OrganisationalUnitId],
[JobTitle],
[EmploymentStatusId],
[Deleted],
[CreatedOn],
[CreatedBy],
[LastModifiedOn],
[LastModifiedBy],
[EmployeeReference]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE STATISTICS [_dta_stat_933578364_1_32] ON [dbo].[Employee]([Id], [ClientId])
go

CREATE NONCLUSTERED INDEX [_dta_index_RiskAssessment_33_194099732__K1_K6_K11_2_3_4_5_7_8_9_10_13_14] ON [dbo].[RiskAssessment] 
(
	[Id] ASC,
	[Deleted] ASC,
	[SiteId] ASC
)
INCLUDE ( [Title],
[Reference],
[AssessmentDate],
[ClientId],
[CreatedOn],
[CreatedBy],
[LastModifiedOn],
[LastModifiedBy],
[StatusId],
[RiskAssessorId]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE STATISTICS [_dta_stat_194099732_6_1] ON [dbo].[RiskAssessment]([Deleted], [Id])
go

CREATE STATISTICS [_dta_stat_194099732_1_11] ON [dbo].[RiskAssessment]([Id], [SiteId])
go

CREATE STATISTICS [_dta_stat_194099732_11_6_1] ON [dbo].[RiskAssessment]([SiteId], [Deleted], [Id])
go

CREATE NONCLUSTERED INDEX [_dta_index_User_33_709577566__K2_1_3_6_7_8_9_10_11_12_13_15] ON [dbo].[User] 
(
	[EmployeeId] ASC
)
INCLUDE ( [UserId],
[RoleId],
[Deleted],
[CreatedOn],
[CreatedBy],
[LastModifiedOn],
[LastModifiedBy],
[ClientId],
[SiteId],
[IsActivated],
[DateDeleted]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE NONCLUSTERED INDEX [_dta_index_SignificantFinding_33_1890105774__K2_K1_3_4_5_6_7] ON [dbo].[SignificantFinding] 
(
	[FireAnswerId] ASC,
	[Id] ASC
)
INCLUDE ( [CreatedBy],
[CreatedOn],
[LastModifiedBy],
[LastModifiedOn],
[Deleted]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE STATISTICS [_dta_stat_1890105774_1_2] ON [dbo].[SignificantFinding]([Id], [FireAnswerId])
go

CREATE NONCLUSTERED INDEX [_dta_index_FireRiskAssessment_33_1586104691__K1_2_3_4_5_6_7_8_9_10_11_12] ON [dbo].[FireRiskAssessment] 
(
	[Id] ASC
)
INCLUDE ( [PersonAppointed],
[PremisesProvidesSleepingAccommodation],
[PremisesProvidesSleepingAccommodationConfirmed],
[Location],
[BuildingUse],
[ElectricityEmergencyShutOff],
[GasEmergencyShutOff],
[WaterEmergencyShutOff],
[OtherEmergencyShutOff],
[NumberOfFloors],
[NumberOfPeople]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
go

CREATE STATISTICS [_dta_stat_1445580188_2_1] ON [dbo].[RiskAssessmentReview]([RiskAssessmentId], [Id])
go

CREATE STATISTICS [_dta_stat_1189579276_1_2] ON [dbo].[MultiHazardRiskAssessmentHazard]([Id], [RiskAssessmentId])
go

CREATE STATISTICS [_dta_stat_1509580416_2_1] ON [dbo].[Document]([DocumentLibraryId], [Id])
go

CREATE STATISTICS [_dta_stat_1266103551_1_13] ON [dbo].[Answer]([Id], [FireRiskAssessmentChecklistId])
go

