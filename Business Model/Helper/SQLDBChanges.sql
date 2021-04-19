
CREATE TABLE [dbo].[tbl_todo_task](
	[ToDoTaskID] [uniqueidentifier] Primary Key Default NEWID() NOT NULL,
	[Subject] [nvarchar](150) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Status] [smallint] NOT NULL,
	[AssignedBy] [uniqueidentifier] NULL,
	[AssignedOn] [datetime] NULL,
	[CompletedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[ModifiedDateTime] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
)

ALTER TABle tbl_todo_task DROP COLUMN PercentComplete, AssignedTo


Create table tbl_assigned_mapping
(
AssignedMappingID uniqueidentifier primary key default newid(),
AssignedToID uniqueidentifier NOT NULL,
EntityID uniqueidentifier NOT NULL,
EntityType smallint NOT NULL
)

--Date 10-Nov-2020
ALTER TABLE tbl_todo_task drop column Status, CompletedOn
ALTER TABLE tbl_assigned_mapping ADD Status SMALLINT NOT NULL DEFAULT 0, CompletedOn DateTime
