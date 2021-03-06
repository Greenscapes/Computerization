ALTER TABLE [dbo].[WeeklySchedules] DROP CONSTRAINT [FK_dbo.WeeklySchedules_dbo.DailySchedules_WednesdayScheduleId]
GO
ALTER TABLE [dbo].[WeeklySchedules] DROP CONSTRAINT [FK_dbo.WeeklySchedules_dbo.DailySchedules_TuesdayScheduleId]
GO
ALTER TABLE [dbo].[WeeklySchedules] DROP CONSTRAINT [FK_dbo.WeeklySchedules_dbo.DailySchedules_ThursdayScheduleId]
GO
ALTER TABLE [dbo].[WeeklySchedules] DROP CONSTRAINT [FK_dbo.WeeklySchedules_dbo.DailySchedules_SundayScheduleId]
GO
ALTER TABLE [dbo].[WeeklySchedules] DROP CONSTRAINT [FK_dbo.WeeklySchedules_dbo.DailySchedules_SaturdayScheduleId]
GO
ALTER TABLE [dbo].[WeeklySchedules] DROP CONSTRAINT [FK_dbo.WeeklySchedules_dbo.DailySchedules_MondayScheduleId]
GO
ALTER TABLE [dbo].[WeeklySchedules] DROP CONSTRAINT [FK_dbo.WeeklySchedules_dbo.DailySchedules_FridayScheduleId]
GO
ALTER TABLE [dbo].[ServiceTickets] DROP CONSTRAINT [FK_ServiceTickets_EventTaskLists]
GO
ALTER TABLE [dbo].[ServiceTickets] DROP CONSTRAINT [FK_dbo.ServiceTickets_dbo.ServiceTemplates_ServiceTemplateId]
GO
ALTER TABLE [dbo].[ServiceTickets] DROP CONSTRAINT [FK_dbo.ServiceTickets_dbo.Employees_ApprovedById]
GO
ALTER TABLE [dbo].[PropertyTasks] DROP CONSTRAINT [FK_PropertyTasks_EventTaskLists]
GO
ALTER TABLE [dbo].[PropertyTasks] DROP CONSTRAINT [FK_dbo.PropertyTasks_dbo.PropertyTaskLists_PropertyTaskListId]
GO
ALTER TABLE [dbo].[PropertyTaskLists] DROP CONSTRAINT [FK_dbo.PropertyTaskLists_dbo.PropertyTaskListTypes_PropertyTaskListTypeId]
GO
ALTER TABLE [dbo].[PropertyTaskLists] DROP CONSTRAINT [FK_dbo.PropertyTaskLists_dbo.Properties_PropertyId]
GO
ALTER TABLE [dbo].[PropertyTaskHeaders] DROP CONSTRAINT [FK_dbo.PropertyTaskHeaders_dbo.PropertyTaskListTypes_PropertyTaskListTypeId]
GO
ALTER TABLE [dbo].[PropertyTaskEventNotes] DROP CONSTRAINT [FK_dbo.PropertyTaskEventNotes_dbo.EventSchedules_EventScheduleId]
GO
ALTER TABLE [dbo].[PropertyTaskDetails] DROP CONSTRAINT [FK_dbo.PropertyTaskDetails_dbo.PropertyTasks_PropertyTaskId]
GO
ALTER TABLE [dbo].[PropertyTaskDetails] DROP CONSTRAINT [FK_dbo.PropertyTaskDetails_dbo.PropertyTaskHeaders_PropertyTaskHeaderId]
GO
ALTER TABLE [dbo].[PropertyTaskCrews] DROP CONSTRAINT [FK_dbo.PropertyTaskCrews_dbo.PropertyTasks_PropertyTask_Id]
GO
ALTER TABLE [dbo].[PropertyTaskCrews] DROP CONSTRAINT [FK_dbo.PropertyTaskCrews_dbo.Crews_Crew_Id]
GO
ALTER TABLE [dbo].[EventTaskLists] DROP CONSTRAINT [FK_EventTaskLists_ServiceTemplates]
GO
ALTER TABLE [dbo].[EventTaskLists] DROP CONSTRAINT [FK_EventTaskLists_Properties]
GO
ALTER TABLE [dbo].[EventTaskLists] DROP CONSTRAINT [FK_EventTaskLists_Crews]
GO
ALTER TABLE [dbo].[EventSchedules] DROP CONSTRAINT [FK_EventSchedules_EventTaskLists]
GO
ALTER TABLE [dbo].[CrewTypeEmployees] DROP CONSTRAINT [FK_dbo.CrewTypeEmployees_dbo.Employees_Employee_Id]
GO
ALTER TABLE [dbo].[CrewTypeEmployees] DROP CONSTRAINT [FK_dbo.CrewTypeEmployees_dbo.CrewTypes_CrewType_Id]
GO
ALTER TABLE [dbo].[CrewMembers] DROP CONSTRAINT [FK_dbo.CrewMembers_dbo.Employees_EmployeeId]
GO
ALTER TABLE [dbo].[CrewMembers] DROP CONSTRAINT [FK_dbo.CrewMembers_dbo.Crews_CrewId]
GO
/****** Object:  Table [dbo].[WeeklySchedules]    Script Date: 11/2/2015 11:08:18 AM ******/
DROP TABLE [dbo].[WeeklySchedules]
GO
/****** Object:  Table [dbo].[ServiceTickets]    Script Date: 11/2/2015 11:08:18 AM ******/
DROP TABLE [dbo].[ServiceTickets]
GO
/****** Object:  Table [dbo].[ServiceTemplates]    Script Date: 11/2/2015 11:08:18 AM ******/
DROP TABLE [dbo].[ServiceTemplates]
GO
/****** Object:  Table [dbo].[PropertyTasks]    Script Date: 11/2/2015 11:08:18 AM ******/
DROP TABLE [dbo].[PropertyTasks]
GO
/****** Object:  Table [dbo].[PropertyTaskListTypes]    Script Date: 11/2/2015 11:08:18 AM ******/
DROP TABLE [dbo].[PropertyTaskListTypes]
GO
/****** Object:  Table [dbo].[PropertyTaskLists]    Script Date: 11/2/2015 11:08:18 AM ******/
DROP TABLE [dbo].[PropertyTaskLists]
GO
/****** Object:  Table [dbo].[PropertyTaskHeaders]    Script Date: 11/2/2015 11:08:18 AM ******/
DROP TABLE [dbo].[PropertyTaskHeaders]
GO
/****** Object:  Table [dbo].[PropertyTaskEventNotes]    Script Date: 11/2/2015 11:08:18 AM ******/
DROP TABLE [dbo].[PropertyTaskEventNotes]
GO
/****** Object:  Table [dbo].[PropertyTaskDetails]    Script Date: 11/2/2015 11:08:18 AM ******/
DROP TABLE [dbo].[PropertyTaskDetails]
GO
/****** Object:  Table [dbo].[PropertyTaskCrews]    Script Date: 11/2/2015 11:08:18 AM ******/
DROP TABLE [dbo].[PropertyTaskCrews]
GO
/****** Object:  Table [dbo].[Properties]    Script Date: 11/2/2015 11:08:18 AM ******/
DROP TABLE [dbo].[Properties]
GO
/****** Object:  Table [dbo].[EventTaskLists]    Script Date: 11/2/2015 11:08:18 AM ******/
DROP TABLE [dbo].[EventTaskLists]
GO
/****** Object:  Table [dbo].[EventSchedules]    Script Date: 11/2/2015 11:08:18 AM ******/
DROP TABLE [dbo].[EventSchedules]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 11/2/2015 11:08:18 AM ******/
DROP TABLE [dbo].[Employees]
GO
/****** Object:  Table [dbo].[DailySchedules]    Script Date: 11/2/2015 11:08:18 AM ******/
DROP TABLE [dbo].[DailySchedules]
GO
/****** Object:  Table [dbo].[CrewTypes]    Script Date: 11/2/2015 11:08:18 AM ******/
DROP TABLE [dbo].[CrewTypes]
GO
/****** Object:  Table [dbo].[CrewTypeEmployees]    Script Date: 11/2/2015 11:08:18 AM ******/
DROP TABLE [dbo].[CrewTypeEmployees]
GO
/****** Object:  Table [dbo].[Crews]    Script Date: 11/2/2015 11:08:18 AM ******/
DROP TABLE [dbo].[Crews]
GO
/****** Object:  Table [dbo].[CrewMembers]    Script Date: 11/2/2015 11:08:18 AM ******/
DROP TABLE [dbo].[CrewMembers]
GO
/****** Object:  Table [dbo].[CrewMembers]    Script Date: 11/2/2015 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CrewMembers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[CrewId] [int] NOT NULL,
	[IsCrewLeader] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.CrewMembers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Crews]    Script Date: 11/2/2015 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Crews](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Crews] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CrewTypeEmployees]    Script Date: 11/2/2015 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CrewTypeEmployees](
	[CrewType_Id] [int] NOT NULL,
	[Employee_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.CrewTypeEmployees] PRIMARY KEY CLUSTERED 
(
	[CrewType_Id] ASC,
	[Employee_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CrewTypes]    Script Date: 11/2/2015 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CrewTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.CrewTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DailySchedules]    Script Date: 11/2/2015 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DailySchedules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_dbo.DailySchedules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Employees]    Script Date: 11/2/2015 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](max) NULL,
	[MiddleName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventSchedules]    Script Date: 11/2/2015 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventSchedules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClientTaskId] [uniqueidentifier] NOT NULL,
	[StartTime] [datetime2](7) NOT NULL,
	[EndTime] [datetime2](7) NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[IsAllDay] [bit] NOT NULL,
	[RecurrenceRule] [nvarchar](max) NULL,
	[RecurrenceID] [int] NULL,
	[RecurrenceException] [nvarchar](max) NULL,
	[StartTimezone] [nvarchar](max) NULL,
	[EndTimezone] [nvarchar](max) NULL,
	[Status] [int] NOT NULL,
	[ActualStartTime] [datetime] NULL,
	[ActualEndTime] [datetime] NULL,
	[EventTaskListId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.EventSchedules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventTaskLists]    Script Date: 11/2/2015 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventTaskLists](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PropertyId] [int] NOT NULL,
	[CrewId] [int] NOT NULL,
	[Name] [nvarchar](255) NULL,
	[ServiceTemplateId] [int] NULL,
 CONSTRAINT [PK_EventTaskList] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Properties]    Script Date: 11/2/2015 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Properties](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Address1] [nvarchar](max) NULL,
	[Address2] [nvarchar](max) NULL,
	[City] [nvarchar](max) NULL,
	[State] [nvarchar](max) NULL,
	[PropertyRefNumber] [nvarchar](max) NULL,
	[PropertyType] [int] NOT NULL,
	[NumberOfFreeServiceCalls] [int] NOT NULL,
	[ContractDate] [datetime] NOT NULL,
	[Zip] [nvarchar](11) NULL,
 CONSTRAINT [PK_dbo.Properties] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PropertyTaskCrews]    Script Date: 11/2/2015 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropertyTaskCrews](
	[PropertyTask_Id] [int] NOT NULL,
	[Crew_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.PropertyTaskCrews] PRIMARY KEY CLUSTERED 
(
	[PropertyTask_Id] ASC,
	[Crew_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PropertyTaskDetails]    Script Date: 11/2/2015 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropertyTaskDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PropertyTaskId] [int] NOT NULL,
	[PropertyTaskHeaderId] [int] NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.PropertyTaskDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PropertyTaskEventNotes]    Script Date: 11/2/2015 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropertyTaskEventNotes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[ReviewStatus] [int] NOT NULL,
	[EventScheduleId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.PropertyTaskEventNotes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PropertyTaskHeaders]    Script Date: 11/2/2015 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropertyTaskHeaders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PropertyTaskListTypeId] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.PropertyTaskHeaders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PropertyTaskLists]    Script Date: 11/2/2015 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropertyTaskLists](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PropertyId] [int] NOT NULL,
	[PropertyTaskListTypeId] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.PropertyTaskLists] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PropertyTaskListTypes]    Script Date: 11/2/2015 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropertyTaskListTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.PropertyTaskListTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PropertyTasks]    Script Date: 11/2/2015 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PropertyTasks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PropertyTaskListId] [int] NOT NULL,
	[Location] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
	[EstimatedDuration] [int] NOT NULL,
	[IsFreeService] [bit] NOT NULL,
	[Status] [int] NOT NULL,
	[EventTaskListId] [int] NULL,
 CONSTRAINT [PK_dbo.PropertyTasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ServiceTemplates]    Script Date: 11/2/2015 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceTemplates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Url] [nvarchar](max) NULL,
	[JsonFields] [ntext] NULL,
 CONSTRAINT [PK_dbo.ServiceTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ServiceTickets]    Script Date: 11/2/2015 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceTickets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ServiceTemplateId] [int] NOT NULL,
	[ReferenceNumber] [nvarchar](max) NULL,
	[VisitFromTime] [datetime] NULL,
	[VisitToTime] [datetime] NULL,
	[Staff] [nvarchar](max) NULL,
	[JsonFields] [ntext] NULL,
	[ApprovedById] [int] NULL,
	[ApprovedDate] [datetime] NULL,
	[Condition] [int] NULL,
	[Notes] [ntext] NULL,
	[EventTaskListId] [int] NULL,
	[EventDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.ServiceTickets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WeeklySchedules]    Script Date: 11/2/2015 11:08:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WeeklySchedules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SundayScheduleId] [int] NOT NULL,
	[MondayScheduleId] [int] NOT NULL,
	[TuesdayScheduleId] [int] NOT NULL,
	[WednesdayScheduleId] [int] NOT NULL,
	[ThursdayScheduleId] [int] NOT NULL,
	[FridayScheduleId] [int] NOT NULL,
	[SaturdayScheduleId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.WeeklySchedules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[CrewMembers] ON 

INSERT [dbo].[CrewMembers] ([Id], [EmployeeId], [CrewId], [IsCrewLeader]) VALUES (1, 1, 1, 1)
INSERT [dbo].[CrewMembers] ([Id], [EmployeeId], [CrewId], [IsCrewLeader]) VALUES (2, 2, 3, 0)
SET IDENTITY_INSERT [dbo].[CrewMembers] OFF
SET IDENTITY_INSERT [dbo].[Crews] ON 

INSERT [dbo].[Crews] ([Id], [Name]) VALUES (1, N'Morning Mowing')
INSERT [dbo].[Crews] ([Id], [Name]) VALUES (2, N'Morning Mowing')
INSERT [dbo].[Crews] ([Id], [Name]) VALUES (3, N'Pruning')
SET IDENTITY_INSERT [dbo].[Crews] OFF
INSERT [dbo].[CrewTypeEmployees] ([CrewType_Id], [Employee_Id]) VALUES (1, 1)
INSERT [dbo].[CrewTypeEmployees] ([CrewType_Id], [Employee_Id]) VALUES (2, 1)
INSERT [dbo].[CrewTypeEmployees] ([CrewType_Id], [Employee_Id]) VALUES (5, 2)
SET IDENTITY_INSERT [dbo].[CrewTypes] ON 

INSERT [dbo].[CrewTypes] ([Id], [Name], [Description]) VALUES (1, N'Mowing', N'Mowing')
INSERT [dbo].[CrewTypes] ([Id], [Name], [Description]) VALUES (2, N'Mowing', N'Mowing')
INSERT [dbo].[CrewTypes] ([Id], [Name], [Description]) VALUES (5, N'Test', NULL)
SET IDENTITY_INSERT [dbo].[CrewTypes] OFF
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([Id], [FirstName], [MiddleName], [LastName], [Email], [Phone]) VALUES (1, N'Jeff', NULL, N'Love', NULL, NULL)
INSERT [dbo].[Employees] ([Id], [FirstName], [MiddleName], [LastName], [Email], [Phone]) VALUES (2, N'Nathan', NULL, N'Love', NULL, NULL)
INSERT [dbo].[Employees] ([Id], [FirstName], [MiddleName], [LastName], [Email], [Phone]) VALUES (3, N'Nathan', NULL, N'Love', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Employees] OFF
SET IDENTITY_INSERT [dbo].[EventSchedules] ON 

INSERT [dbo].[EventSchedules] ([Id], [ClientTaskId], [StartTime], [EndTime], [Title], [Description], [IsAllDay], [RecurrenceRule], [RecurrenceID], [RecurrenceException], [StartTimezone], [EndTimezone], [Status], [ActualStartTime], [ActualEndTime], [EventTaskListId]) VALUES (24, N'00000000-0000-0000-0000-000000000000', CAST(0x0700E0349564993A0B AS DateTime2), CAST(0x07001417C668993A0B AS DateTime2), N'Pruning of Love House', N'', 0, N'FREQ=DAILY', NULL, N'', N'America/New_York', N'America/New_York', 0, NULL, NULL, 16)
INSERT [dbo].[EventSchedules] ([Id], [ClientTaskId], [StartTime], [EndTime], [Title], [Description], [IsAllDay], [RecurrenceRule], [RecurrenceID], [RecurrenceException], [StartTimezone], [EndTimezone], [Status], [ActualStartTime], [ActualEndTime], [EventTaskListId]) VALUES (25, N'00000000-0000-0000-0000-000000000000', CAST(0x0700B0BD5875A03A0B AS DateTime2), CAST(0x0700E49F8979A03A0B AS DateTime2), N'Mowing Love Neighbor', N'', 0, N'FREQ=WEEKLY;BYDAY=TU,TH', NULL, N'', N'America/New_York', N'America/New_York', 0, NULL, NULL, 18)
INSERT [dbo].[EventSchedules] ([Id], [ClientTaskId], [StartTime], [EndTime], [Title], [Description], [IsAllDay], [RecurrenceRule], [RecurrenceID], [RecurrenceException], [StartTimezone], [EndTimezone], [Status], [ActualStartTime], [ActualEndTime], [EventTaskListId]) VALUES (26, N'00000000-0000-0000-0000-000000000000', CAST(0x0700B0BD5875A03A0B AS DateTime2), CAST(0x0700E49F8979A03A0B AS DateTime2), N'Pruning Visit', N'', 0, N'FREQ=WEEKLY;BYDAY=TU', NULL, N'', N'America/New_York', N'America/New_York', 0, NULL, NULL, 19)
SET IDENTITY_INSERT [dbo].[EventSchedules] OFF
SET IDENTITY_INSERT [dbo].[EventTaskLists] ON 

INSERT [dbo].[EventTaskLists] ([Id], [PropertyId], [CrewId], [Name], [ServiceTemplateId]) VALUES (1, 1, 1, N'Test Event Task', NULL)
INSERT [dbo].[EventTaskLists] ([Id], [PropertyId], [CrewId], [Name], [ServiceTemplateId]) VALUES (2, 1, 1, N'Test Event Task2', NULL)
INSERT [dbo].[EventTaskLists] ([Id], [PropertyId], [CrewId], [Name], [ServiceTemplateId]) VALUES (3, 1, 1, N'Test Event Task3', NULL)
INSERT [dbo].[EventTaskLists] ([Id], [PropertyId], [CrewId], [Name], [ServiceTemplateId]) VALUES (4, 1, 1, N'Test Event Task4', NULL)
INSERT [dbo].[EventTaskLists] ([Id], [PropertyId], [CrewId], [Name], [ServiceTemplateId]) VALUES (5, 1, 1, N'Test Event Task5', NULL)
INSERT [dbo].[EventTaskLists] ([Id], [PropertyId], [CrewId], [Name], [ServiceTemplateId]) VALUES (6, 1, 1, N'test Tasks', NULL)
INSERT [dbo].[EventTaskLists] ([Id], [PropertyId], [CrewId], [Name], [ServiceTemplateId]) VALUES (7, 1, 1, N'test list 2', NULL)
INSERT [dbo].[EventTaskLists] ([Id], [PropertyId], [CrewId], [Name], [ServiceTemplateId]) VALUES (8, 1, 1, N'fdsafdsf', NULL)
INSERT [dbo].[EventTaskLists] ([Id], [PropertyId], [CrewId], [Name], [ServiceTemplateId]) VALUES (9, 1, 1, N'dsfdasf', NULL)
INSERT [dbo].[EventTaskLists] ([Id], [PropertyId], [CrewId], [Name], [ServiceTemplateId]) VALUES (10, 1, 1, N'dsfdsaf', NULL)
INSERT [dbo].[EventTaskLists] ([Id], [PropertyId], [CrewId], [Name], [ServiceTemplateId]) VALUES (11, 1, 1, N'dsfsdafsdaf', NULL)
INSERT [dbo].[EventTaskLists] ([Id], [PropertyId], [CrewId], [Name], [ServiceTemplateId]) VALUES (12, 1, 1, N'sfdsf', NULL)
INSERT [dbo].[EventTaskLists] ([Id], [PropertyId], [CrewId], [Name], [ServiceTemplateId]) VALUES (13, 1, 1, N'fdsafdsaf', NULL)
INSERT [dbo].[EventTaskLists] ([Id], [PropertyId], [CrewId], [Name], [ServiceTemplateId]) VALUES (14, 1, 1, N'fdsafdsf', NULL)
INSERT [dbo].[EventTaskLists] ([Id], [PropertyId], [CrewId], [Name], [ServiceTemplateId]) VALUES (15, 1, 1, N'sdfdsafsadfds', NULL)
INSERT [dbo].[EventTaskLists] ([Id], [PropertyId], [CrewId], [Name], [ServiceTemplateId]) VALUES (16, 4, 3, N'sdfafadf', NULL)
INSERT [dbo].[EventTaskLists] ([Id], [PropertyId], [CrewId], [Name], [ServiceTemplateId]) VALUES (17, 4, 3, N'Test Event Task', 2)
INSERT [dbo].[EventTaskLists] ([Id], [PropertyId], [CrewId], [Name], [ServiceTemplateId]) VALUES (18, 5, 1, N'Mowing', 2)
INSERT [dbo].[EventTaskLists] ([Id], [PropertyId], [CrewId], [Name], [ServiceTemplateId]) VALUES (19, 5, 3, N'Pruning of Love Neighbor', 3)
SET IDENTITY_INSERT [dbo].[EventTaskLists] OFF
SET IDENTITY_INSERT [dbo].[Properties] ON 

INSERT [dbo].[Properties] ([Id], [Name], [Address1], [Address2], [City], [State], [PropertyRefNumber], [PropertyType], [NumberOfFreeServiceCalls], [ContractDate], [Zip]) VALUES (1, N'WMOA', N'1460 Gleneagles Drive', N'', N'Venice', N'Florida', N'RRSRS22323', 0, 0, CAST(0x0000A53900BE2B29 AS DateTime), N'34292')
INSERT [dbo].[Properties] ([Id], [Name], [Address1], [Address2], [City], [State], [PropertyRefNumber], [PropertyType], [NumberOfFreeServiceCalls], [ContractDate], [Zip]) VALUES (4, N'Love Household', N'12558 Cara Cara Loop', NULL, N'Bradenton', N'FL', N'A000001', 66, 0, CAST(0x0000A52400000000 AS DateTime), N'34212')
INSERT [dbo].[Properties] ([Id], [Name], [Address1], [Address2], [City], [State], [PropertyRefNumber], [PropertyType], [NumberOfFreeServiceCalls], [ContractDate], [Zip]) VALUES (5, N'Love Neighbor', N'12554 Cara Cara Loop', NULL, N'Bradenton', N'FL', N'A00009', 66, 0, CAST(0x0000A54800000000 AS DateTime), N'34212')
SET IDENTITY_INSERT [dbo].[Properties] OFF
INSERT [dbo].[PropertyTaskCrews] ([PropertyTask_Id], [Crew_Id]) VALUES (1, 1)
INSERT [dbo].[PropertyTaskCrews] ([PropertyTask_Id], [Crew_Id]) VALUES (3, 1)
INSERT [dbo].[PropertyTaskCrews] ([PropertyTask_Id], [Crew_Id]) VALUES (4, 1)
INSERT [dbo].[PropertyTaskCrews] ([PropertyTask_Id], [Crew_Id]) VALUES (9, 1)
INSERT [dbo].[PropertyTaskCrews] ([PropertyTask_Id], [Crew_Id]) VALUES (11, 1)
INSERT [dbo].[PropertyTaskCrews] ([PropertyTask_Id], [Crew_Id]) VALUES (12, 1)
INSERT [dbo].[PropertyTaskCrews] ([PropertyTask_Id], [Crew_Id]) VALUES (10, 3)
INSERT [dbo].[PropertyTaskCrews] ([PropertyTask_Id], [Crew_Id]) VALUES (13, 3)
SET IDENTITY_INSERT [dbo].[PropertyTaskDetails] ON 

INSERT [dbo].[PropertyTaskDetails] ([Id], [PropertyTaskId], [PropertyTaskHeaderId], [Value]) VALUES (1, 2, 1, N'St. Augustine Fertilization')
INSERT [dbo].[PropertyTaskDetails] ([Id], [PropertyTaskId], [PropertyTaskHeaderId], [Value]) VALUES (2, 2, 2, N'Spot')
INSERT [dbo].[PropertyTaskDetails] ([Id], [PropertyTaskId], [PropertyTaskHeaderId], [Value]) VALUES (3, 2, 3, N'18-0-6 Atrazine and Surfactant')
INSERT [dbo].[PropertyTaskDetails] ([Id], [PropertyTaskId], [PropertyTaskHeaderId], [Value]) VALUES (4, 3, 1, NULL)
INSERT [dbo].[PropertyTaskDetails] ([Id], [PropertyTaskId], [PropertyTaskHeaderId], [Value]) VALUES (5, 3, 2, NULL)
INSERT [dbo].[PropertyTaskDetails] ([Id], [PropertyTaskId], [PropertyTaskHeaderId], [Value]) VALUES (6, 3, 3, NULL)
INSERT [dbo].[PropertyTaskDetails] ([Id], [PropertyTaskId], [PropertyTaskHeaderId], [Value]) VALUES (7, 4, 1, NULL)
INSERT [dbo].[PropertyTaskDetails] ([Id], [PropertyTaskId], [PropertyTaskHeaderId], [Value]) VALUES (8, 4, 2, NULL)
INSERT [dbo].[PropertyTaskDetails] ([Id], [PropertyTaskId], [PropertyTaskHeaderId], [Value]) VALUES (9, 4, 3, NULL)
SET IDENTITY_INSERT [dbo].[PropertyTaskDetails] OFF
SET IDENTITY_INSERT [dbo].[PropertyTaskHeaders] ON 

INSERT [dbo].[PropertyTaskHeaders] ([Id], [PropertyTaskListTypeId], [Name]) VALUES (1, 1, N'Type')
INSERT [dbo].[PropertyTaskHeaders] ([Id], [PropertyTaskListTypeId], [Name]) VALUES (2, 1, N'Treatment')
INSERT [dbo].[PropertyTaskHeaders] ([Id], [PropertyTaskListTypeId], [Name]) VALUES (3, 1, N'Product')
SET IDENTITY_INSERT [dbo].[PropertyTaskHeaders] OFF
SET IDENTITY_INSERT [dbo].[PropertyTaskLists] ON 

INSERT [dbo].[PropertyTaskLists] ([Id], [PropertyId], [PropertyTaskListTypeId], [Name]) VALUES (1, 1, 1, N'Fertilization')
INSERT [dbo].[PropertyTaskLists] ([Id], [PropertyId], [PropertyTaskListTypeId], [Name]) VALUES (2, 1, 1, N'Feritlize front yard')
INSERT [dbo].[PropertyTaskLists] ([Id], [PropertyId], [PropertyTaskListTypeId], [Name]) VALUES (4, 4, 1, N'Default')
INSERT [dbo].[PropertyTaskLists] ([Id], [PropertyId], [PropertyTaskListTypeId], [Name]) VALUES (5, 5, 1, N'Default')
SET IDENTITY_INSERT [dbo].[PropertyTaskLists] OFF
SET IDENTITY_INSERT [dbo].[PropertyTaskListTypes] ON 

INSERT [dbo].[PropertyTaskListTypes] ([Id], [Name], [Description]) VALUES (1, N'Fertilization', N'Fertilization task list')
SET IDENTITY_INSERT [dbo].[PropertyTaskListTypes] OFF
SET IDENTITY_INSERT [dbo].[PropertyTasks] ON 

INSERT [dbo].[PropertyTasks] ([Id], [PropertyTaskListId], [Location], [Description], [Notes], [EstimatedDuration], [IsFreeService], [Status], [EventTaskListId]) VALUES (1, 1, N'444 GG', NULL, NULL, 30, 0, 0, NULL)
INSERT [dbo].[PropertyTasks] ([Id], [PropertyTaskListId], [Location], [Description], [Notes], [EstimatedDuration], [IsFreeService], [Status], [EventTaskListId]) VALUES (2, 1, N'1447 Gleneagles', NULL, NULL, 30, 0, 0, NULL)
INSERT [dbo].[PropertyTasks] ([Id], [PropertyTaskListId], [Location], [Description], [Notes], [EstimatedDuration], [IsFreeService], [Status], [EventTaskListId]) VALUES (3, 1, N'Corner of Johnson St', N'Mowing', NULL, 30, 0, 0, NULL)
INSERT [dbo].[PropertyTasks] ([Id], [PropertyTaskListId], [Location], [Description], [Notes], [EstimatedDuration], [IsFreeService], [Status], [EventTaskListId]) VALUES (4, 1, N'Front yard', N'Pruning Tree', NULL, 20, 0, 0, 1)
INSERT [dbo].[PropertyTasks] ([Id], [PropertyTaskListId], [Location], [Description], [Notes], [EstimatedDuration], [IsFreeService], [Status], [EventTaskListId]) VALUES (9, 4, N'12558 Cara Cara Loop', N'Mow front yard', NULL, 30, 0, 0, NULL)
INSERT [dbo].[PropertyTasks] ([Id], [PropertyTaskListId], [Location], [Description], [Notes], [EstimatedDuration], [IsFreeService], [Status], [EventTaskListId]) VALUES (10, 4, N'12558 Cara Cara Loop', N'Prune Tree', NULL, 30, 0, 0, 16)
INSERT [dbo].[PropertyTasks] ([Id], [PropertyTaskListId], [Location], [Description], [Notes], [EstimatedDuration], [IsFreeService], [Status], [EventTaskListId]) VALUES (11, 5, N'Love Neighbor', N'Mow Front Yard', NULL, 30, 0, 0, 18)
INSERT [dbo].[PropertyTasks] ([Id], [PropertyTaskListId], [Location], [Description], [Notes], [EstimatedDuration], [IsFreeService], [Status], [EventTaskListId]) VALUES (12, 5, N'Love Neighbor', N'Mow Back yard', NULL, 30, 0, 0, 18)
INSERT [dbo].[PropertyTasks] ([Id], [PropertyTaskListId], [Location], [Description], [Notes], [EstimatedDuration], [IsFreeService], [Status], [EventTaskListId]) VALUES (13, 5, N'Love Neighbor', N'Prune Bushes', NULL, 20, 0, 0, 19)
SET IDENTITY_INSERT [dbo].[PropertyTasks] OFF
SET IDENTITY_INSERT [dbo].[ServiceTemplates] ON 

INSERT [dbo].[ServiceTemplates] ([Id], [Name], [Url], [JsonFields]) VALUES (1, N'Arborjet Treatment', N'/templates/servicetickets/arborjet-treatment.html', N'{"Trees":[],"DefaultTree":{"Kind":"Palm","Type":"","Location":"","ChemicalInjected":false,"ChemcialAmount":0}}')
INSERT [dbo].[ServiceTemplates] ([Id], [Name], [Url], [JsonFields]) VALUES (2, N'Commercial Mowing Visit', N'/templates/servicetickets/commercial-mowing.html', N'{"Tasks":[], "DefaultTask": {"Location":"", "Mowed":false, "HardtopEdged":false, "BedEdged":false, "BlewOff":false, "DebrisCollected":false, "Comments":""}}')
INSERT [dbo].[ServiceTemplates] ([Id], [Name], [Url], [JsonFields]) VALUES (3, N'Commercial Pruning and Weeding Visit', N'/templates/servicetickets/commercial-pruning.html', N'{"Tasks":[], "DefaultTask": {"Location":"", "Pruned":false, "Weeded":false, "Sprayed":false, "BlewOff":false, "Comments":""}}')
INSERT [dbo].[ServiceTemplates] ([Id], [Name], [Url], [JsonFields]) VALUES (4, N'Commercial Weeding Only Visit', N'/templates/servicetickets/commercial-weeding.html', N'{"Tasks":[], "DefaultTask": {"Location":"", "HandPulledWeeds":false, "SprayedWeeds":false, "SprayedCrackWeeds":false, "Comments":""}}')
INSERT [dbo].[ServiceTemplates] ([Id], [Name], [Url], [JsonFields]) VALUES (5, N'Commercial Yard Trash Collection Visit', N'/templates/servicetickets/commercial-trash.html', N'{"Tasks":[], "DefaultTask": {"Location":"", "YardTrashPresent":false,"Comments":""}}')
INSERT [dbo].[ServiceTemplates] ([Id], [Name], [Url], [JsonFields]) VALUES (6, N'Customer Care Visit', N'/templates/servicetickets/customer-care.html', N'{}')
INSERT [dbo].[ServiceTemplates] ([Id], [Name], [Url], [JsonFields]) VALUES (7, N'Commercial Complaint Visit', N'/templates/servicetickets/customer-complaint.html', N'{}')
INSERT [dbo].[ServiceTemplates] ([Id], [Name], [Url], [JsonFields]) VALUES (8, N'Fertilization/Pest Control Visit', N'/templates/servicetickets/pest-control.html', N'{"Tasks":[{"Name":"St Augustine Fert","Location":"", "ProductUsed":"", "QuantityUsed":0, "Prevention":""}
,{"Name":"Bahia Fert","Location":"", "ProductUsed":"", "QuantityUsed":0, "Prevention":""}
,{"Name":"Zosia Fert","Location":"", "ProductUsed":"", "QuantityUsed":0, "Prevention":""}
,{"Name":"Mixed Grass Fert","Location":"", "ProductUsed":"", "QuantityUsed":0, "Prevention":""}
,{"Name":"Citrus Fert","Location":"", "ProductUsed":"", "QuantityUsed":0, "Prevention":""}
,{"Name":"Shrub Fert","Location":"", "ProductUsed":"", "QuantityUsed":0, "Prevention":""}
,{"Name":"Palm Fert","Location":"", "ProductUsed":"", "QuantityUsed":0, "Prevention":""}
,{"Name":"Hearts of Palm","Location":"", "ProductUsed":"", "QuantityUsed":0, "Prevention":""}
,{"Name":"Tree Fert","Location":"", "ProductUsed":"", "QuantityUsed":0, "Prevention":""}
,{"Name":"Fire Ant Treat","Location":"", "ProductUsed":"", "QuantityUsed":0, "Prevention":""}
,{"Name":"Pesticide Turf Treat","Location":"", "ProductUsed":"", "QuantityUsed":0, "Prevention":""}
,{"Name":"Herbicide Turf Treat","Location":"", "ProductUsed":"", "QuantityUsed":0, "Prevention":""}
,{"Name":"Pesticide Shrub Treat","Location":"", "ProductUsed":"", "QuantityUsed":0, "Prevention":""}
,{"Name":"Fungicide Shrub Treat","Location":"", "ProductUsed":"", "QuantityUsed":0, "Prevention":""}
,{"Name":"Fungicide Turf Treat","Location":"", "ProductUsed":"", "QuantityUsed":0, "Prevention":""}
,{"Name":"Flower Treat","Location":"", "ProductUsed":"", "QuantityUsed":0, "Prevention":""}
], "DefaultTask": {"Name":"","Location":"", "ProductUsed":"", "QuantityUsed":0, "Prevention":""}}')
INSERT [dbo].[ServiceTemplates] ([Id], [Name], [Url], [JsonFields]) VALUES (9, N'Irrigation Adjustment', N'/templates/servicetickets/irrigation-adjustment.html', N'{"Zones":[], "DefaultZone": {"Number":0,"Adjustment":""}}')
INSERT [dbo].[ServiceTemplates] ([Id], [Name], [Url], [JsonFields]) VALUES (10, N'Irrigation Inspection', N'/templates/servicetickets/irrigation-inspection.html', N'{
"NoCharges": [],
"DefaultNoCharge":{"ZoneNumber": 1, "Repairs":[{"Name":"No Faults Found", "Value":false},{"Name":"Partial Clog Cleared", "Value":false},{"Name":"Arc/Radius Adjusted", "Value":false}]},
"Charges": [],
"DefaultCharge":{"ZoneNumber": 1, "Repairs":[{"Name":"Head Replaced", "Value":false},{"Name":"Head Straightened", "Value":false},{"Name":"Nozzle Replaced", "Value":false},{"Name":"Break in Pipe Under 2''''", "Value":false}]}
 }')
INSERT [dbo].[ServiceTemplates] ([Id], [Name], [Url], [JsonFields]) VALUES (11, N'Irrigation Installation', N'/templates/servicetickets/irrigation-installation.html', N'{
"Zones": [],
"DefaultZone":{"Number": 1, "Installation": ""},
"Parts": [],
"DefaultPart":{"Name": "", "Charge":0.00}
 }')
INSERT [dbo].[ServiceTemplates] ([Id], [Name], [Url], [JsonFields]) VALUES (12, N'Irrigation Port of Control Installation', N'/templates/servicetickets/irrigation-port.html', N'{
"Clocks": [],
"DefaultClock":{"Reference": "", "Installation": ""},
"Parts": [],
"DefaultPart":{"Name": "", "Charge":0.00},
"Labor": [],
"DefaultLabor":{"Name": "", "Charge":0.00}
 }')
INSERT [dbo].[ServiceTemplates] ([Id], [Name], [Url], [JsonFields]) VALUES (13, N'Irrigation Quote', N'/templates/servicetickets/irrigation-quote.html', N'{
"Zones": [],
"DefaultZone":{"Number": 1, "Repair": ""},
"Parts": [],
"DefaultPart":{"Name": "", "Charge":0.00},
"Labor": [{"Name": "Initial call out and first hour", "Charge":100.00}],
"DefaultLabor":{"Name": "", "Charge":0.00}
 }')
INSERT [dbo].[ServiceTemplates] ([Id], [Name], [Url], [JsonFields]) VALUES (14, N'Irrigation Repair', N'/templates/servicetickets/irrigation-repair.html', N'{
"Zones": [],
"DefaultZone":{"Number": 1, "Repair": ""},
"Parts": [],
"DefaultPart":{"Name": "", "Charge":0.00},
"Labor": [],
"DefaultLabor":{"Name": "", "Charge":0.00}
 }')
INSERT [dbo].[ServiceTemplates] ([Id], [Name], [Url], [JsonFields]) VALUES (15, N'Landscape Installation or Removal or Repair Visit', N'/templates/servicetickets/landscape-repair.html', N'{"Tasks":[], "DefaultTask": {"Location":"", "Plantings":"", "Amount":0}}')
INSERT [dbo].[ServiceTemplates] ([Id], [Name], [Url], [JsonFields]) VALUES (16, N'Mulching Visit', N'/templates/servicetickets/mulching.html', N'{"Tasks":[], "DefaultTask": {"Location":"", "Size":"", "NumberBags":0}}')
INSERT [dbo].[ServiceTemplates] ([Id], [Name], [Url], [JsonFields]) VALUES (17, N'Operations Manager Quality Control Visit', N'/templates/servicetickets/quality-control.html', N'{}')
INSERT [dbo].[ServiceTemplates] ([Id], [Name], [Url], [JsonFields]) VALUES (18, N'Pruning of Palm Trees over 15ft', N'/templates/servicetickets/palm-pruning.html', N'{"CompletedPalms":[], "RemainingPalms":[], "DefaultPalm": {"Area":"", "Number":1}}')
INSERT [dbo].[ServiceTemplates] ([Id], [Name], [Url], [JsonFields]) VALUES (19, N'Residential/Small Commercial Maintenance Visit', N'/templates/servicetickets/residential-maintenance.html', N'{

"Tasks":[],
"DefaultTask": {"Location":"", "Mowed":false, "HardtopEdged":false, "BedEdged":false, "BlewOff":false, "DebrisCollected":false, "Comments":""},
"Plants":[],
"DefaultPlant":{"Location":"", "Plant":"", "Pruned":false, "Comments":""},
"Weeds":[],
"DefaultWeed": {"Location":"", "HandPulled":false, "Sprayed":false, "SprayedCracks":false, "Comments":""},
"Work":[],
"DefaultWork": {"Location":"", "FireAnt":false, "DebrisCollected":false, "BlewOff":false, "Comments":""}
}')
INSERT [dbo].[ServiceTemplates] ([Id], [Name], [Url], [JsonFields]) VALUES (20, N'Sales Visit', N'/templates/servicetickets/sales-visit.html', N'{}')
INSERT [dbo].[ServiceTemplates] ([Id], [Name], [Url], [JsonFields]) VALUES (21, N'Walkthrough Record', N'/templates/servicetickets/walk-through.html', N'{}')
SET IDENTITY_INSERT [dbo].[ServiceTemplates] OFF
ALTER TABLE [dbo].[CrewMembers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CrewMembers_dbo.Crews_CrewId] FOREIGN KEY([CrewId])
REFERENCES [dbo].[Crews] ([Id])
GO
ALTER TABLE [dbo].[CrewMembers] CHECK CONSTRAINT [FK_dbo.CrewMembers_dbo.Crews_CrewId]
GO
ALTER TABLE [dbo].[CrewMembers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CrewMembers_dbo.Employees_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[CrewMembers] CHECK CONSTRAINT [FK_dbo.CrewMembers_dbo.Employees_EmployeeId]
GO
ALTER TABLE [dbo].[CrewTypeEmployees]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CrewTypeEmployees_dbo.CrewTypes_CrewType_Id] FOREIGN KEY([CrewType_Id])
REFERENCES [dbo].[CrewTypes] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CrewTypeEmployees] CHECK CONSTRAINT [FK_dbo.CrewTypeEmployees_dbo.CrewTypes_CrewType_Id]
GO
ALTER TABLE [dbo].[CrewTypeEmployees]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CrewTypeEmployees_dbo.Employees_Employee_Id] FOREIGN KEY([Employee_Id])
REFERENCES [dbo].[Employees] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CrewTypeEmployees] CHECK CONSTRAINT [FK_dbo.CrewTypeEmployees_dbo.Employees_Employee_Id]
GO
ALTER TABLE [dbo].[EventSchedules]  WITH CHECK ADD  CONSTRAINT [FK_EventSchedules_EventTaskLists] FOREIGN KEY([EventTaskListId])
REFERENCES [dbo].[EventTaskLists] ([Id])
GO
ALTER TABLE [dbo].[EventSchedules] CHECK CONSTRAINT [FK_EventSchedules_EventTaskLists]
GO
ALTER TABLE [dbo].[EventTaskLists]  WITH CHECK ADD  CONSTRAINT [FK_EventTaskLists_Crews] FOREIGN KEY([CrewId])
REFERENCES [dbo].[Crews] ([Id])
GO
ALTER TABLE [dbo].[EventTaskLists] CHECK CONSTRAINT [FK_EventTaskLists_Crews]
GO
ALTER TABLE [dbo].[EventTaskLists]  WITH CHECK ADD  CONSTRAINT [FK_EventTaskLists_Properties] FOREIGN KEY([PropertyId])
REFERENCES [dbo].[Properties] ([Id])
GO
ALTER TABLE [dbo].[EventTaskLists] CHECK CONSTRAINT [FK_EventTaskLists_Properties]
GO
ALTER TABLE [dbo].[EventTaskLists]  WITH CHECK ADD  CONSTRAINT [FK_EventTaskLists_ServiceTemplates] FOREIGN KEY([ServiceTemplateId])
REFERENCES [dbo].[ServiceTemplates] ([Id])
GO
ALTER TABLE [dbo].[EventTaskLists] CHECK CONSTRAINT [FK_EventTaskLists_ServiceTemplates]
GO
ALTER TABLE [dbo].[PropertyTaskCrews]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PropertyTaskCrews_dbo.Crews_Crew_Id] FOREIGN KEY([Crew_Id])
REFERENCES [dbo].[Crews] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PropertyTaskCrews] CHECK CONSTRAINT [FK_dbo.PropertyTaskCrews_dbo.Crews_Crew_Id]
GO
ALTER TABLE [dbo].[PropertyTaskCrews]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PropertyTaskCrews_dbo.PropertyTasks_PropertyTask_Id] FOREIGN KEY([PropertyTask_Id])
REFERENCES [dbo].[PropertyTasks] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PropertyTaskCrews] CHECK CONSTRAINT [FK_dbo.PropertyTaskCrews_dbo.PropertyTasks_PropertyTask_Id]
GO
ALTER TABLE [dbo].[PropertyTaskDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PropertyTaskDetails_dbo.PropertyTaskHeaders_PropertyTaskHeaderId] FOREIGN KEY([PropertyTaskHeaderId])
REFERENCES [dbo].[PropertyTaskHeaders] ([Id])
GO
ALTER TABLE [dbo].[PropertyTaskDetails] CHECK CONSTRAINT [FK_dbo.PropertyTaskDetails_dbo.PropertyTaskHeaders_PropertyTaskHeaderId]
GO
ALTER TABLE [dbo].[PropertyTaskDetails]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PropertyTaskDetails_dbo.PropertyTasks_PropertyTaskId] FOREIGN KEY([PropertyTaskId])
REFERENCES [dbo].[PropertyTasks] ([Id])
GO
ALTER TABLE [dbo].[PropertyTaskDetails] CHECK CONSTRAINT [FK_dbo.PropertyTaskDetails_dbo.PropertyTasks_PropertyTaskId]
GO
ALTER TABLE [dbo].[PropertyTaskEventNotes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PropertyTaskEventNotes_dbo.EventSchedules_EventScheduleId] FOREIGN KEY([EventScheduleId])
REFERENCES [dbo].[EventSchedules] ([Id])
GO
ALTER TABLE [dbo].[PropertyTaskEventNotes] CHECK CONSTRAINT [FK_dbo.PropertyTaskEventNotes_dbo.EventSchedules_EventScheduleId]
GO
ALTER TABLE [dbo].[PropertyTaskHeaders]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PropertyTaskHeaders_dbo.PropertyTaskListTypes_PropertyTaskListTypeId] FOREIGN KEY([PropertyTaskListTypeId])
REFERENCES [dbo].[PropertyTaskListTypes] ([Id])
GO
ALTER TABLE [dbo].[PropertyTaskHeaders] CHECK CONSTRAINT [FK_dbo.PropertyTaskHeaders_dbo.PropertyTaskListTypes_PropertyTaskListTypeId]
GO
ALTER TABLE [dbo].[PropertyTaskLists]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PropertyTaskLists_dbo.Properties_PropertyId] FOREIGN KEY([PropertyId])
REFERENCES [dbo].[Properties] ([Id])
GO
ALTER TABLE [dbo].[PropertyTaskLists] CHECK CONSTRAINT [FK_dbo.PropertyTaskLists_dbo.Properties_PropertyId]
GO
ALTER TABLE [dbo].[PropertyTaskLists]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PropertyTaskLists_dbo.PropertyTaskListTypes_PropertyTaskListTypeId] FOREIGN KEY([PropertyTaskListTypeId])
REFERENCES [dbo].[PropertyTaskListTypes] ([Id])
GO
ALTER TABLE [dbo].[PropertyTaskLists] CHECK CONSTRAINT [FK_dbo.PropertyTaskLists_dbo.PropertyTaskListTypes_PropertyTaskListTypeId]
GO
ALTER TABLE [dbo].[PropertyTasks]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PropertyTasks_dbo.PropertyTaskLists_PropertyTaskListId] FOREIGN KEY([PropertyTaskListId])
REFERENCES [dbo].[PropertyTaskLists] ([Id])
GO
ALTER TABLE [dbo].[PropertyTasks] CHECK CONSTRAINT [FK_dbo.PropertyTasks_dbo.PropertyTaskLists_PropertyTaskListId]
GO
ALTER TABLE [dbo].[PropertyTasks]  WITH CHECK ADD  CONSTRAINT [FK_PropertyTasks_EventTaskLists] FOREIGN KEY([EventTaskListId])
REFERENCES [dbo].[EventTaskLists] ([Id])
GO
ALTER TABLE [dbo].[PropertyTasks] CHECK CONSTRAINT [FK_PropertyTasks_EventTaskLists]
GO
ALTER TABLE [dbo].[ServiceTickets]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ServiceTickets_dbo.Employees_ApprovedById] FOREIGN KEY([ApprovedById])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[ServiceTickets] CHECK CONSTRAINT [FK_dbo.ServiceTickets_dbo.Employees_ApprovedById]
GO
ALTER TABLE [dbo].[ServiceTickets]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ServiceTickets_dbo.ServiceTemplates_ServiceTemplateId] FOREIGN KEY([ServiceTemplateId])
REFERENCES [dbo].[ServiceTemplates] ([Id])
GO
ALTER TABLE [dbo].[ServiceTickets] CHECK CONSTRAINT [FK_dbo.ServiceTickets_dbo.ServiceTemplates_ServiceTemplateId]
GO
ALTER TABLE [dbo].[ServiceTickets]  WITH CHECK ADD  CONSTRAINT [FK_ServiceTickets_EventTaskLists] FOREIGN KEY([EventTaskListId])
REFERENCES [dbo].[EventTaskLists] ([Id])
GO
ALTER TABLE [dbo].[ServiceTickets] CHECK CONSTRAINT [FK_ServiceTickets_EventTaskLists]
GO
ALTER TABLE [dbo].[WeeklySchedules]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WeeklySchedules_dbo.DailySchedules_FridayScheduleId] FOREIGN KEY([FridayScheduleId])
REFERENCES [dbo].[DailySchedules] ([Id])
GO
ALTER TABLE [dbo].[WeeklySchedules] CHECK CONSTRAINT [FK_dbo.WeeklySchedules_dbo.DailySchedules_FridayScheduleId]
GO
ALTER TABLE [dbo].[WeeklySchedules]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WeeklySchedules_dbo.DailySchedules_MondayScheduleId] FOREIGN KEY([MondayScheduleId])
REFERENCES [dbo].[DailySchedules] ([Id])
GO
ALTER TABLE [dbo].[WeeklySchedules] CHECK CONSTRAINT [FK_dbo.WeeklySchedules_dbo.DailySchedules_MondayScheduleId]
GO
ALTER TABLE [dbo].[WeeklySchedules]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WeeklySchedules_dbo.DailySchedules_SaturdayScheduleId] FOREIGN KEY([SaturdayScheduleId])
REFERENCES [dbo].[DailySchedules] ([Id])
GO
ALTER TABLE [dbo].[WeeklySchedules] CHECK CONSTRAINT [FK_dbo.WeeklySchedules_dbo.DailySchedules_SaturdayScheduleId]
GO
ALTER TABLE [dbo].[WeeklySchedules]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WeeklySchedules_dbo.DailySchedules_SundayScheduleId] FOREIGN KEY([SundayScheduleId])
REFERENCES [dbo].[DailySchedules] ([Id])
GO
ALTER TABLE [dbo].[WeeklySchedules] CHECK CONSTRAINT [FK_dbo.WeeklySchedules_dbo.DailySchedules_SundayScheduleId]
GO
ALTER TABLE [dbo].[WeeklySchedules]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WeeklySchedules_dbo.DailySchedules_ThursdayScheduleId] FOREIGN KEY([ThursdayScheduleId])
REFERENCES [dbo].[DailySchedules] ([Id])
GO
ALTER TABLE [dbo].[WeeklySchedules] CHECK CONSTRAINT [FK_dbo.WeeklySchedules_dbo.DailySchedules_ThursdayScheduleId]
GO
ALTER TABLE [dbo].[WeeklySchedules]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WeeklySchedules_dbo.DailySchedules_TuesdayScheduleId] FOREIGN KEY([TuesdayScheduleId])
REFERENCES [dbo].[DailySchedules] ([Id])
GO
ALTER TABLE [dbo].[WeeklySchedules] CHECK CONSTRAINT [FK_dbo.WeeklySchedules_dbo.DailySchedules_TuesdayScheduleId]
GO
ALTER TABLE [dbo].[WeeklySchedules]  WITH CHECK ADD  CONSTRAINT [FK_dbo.WeeklySchedules_dbo.DailySchedules_WednesdayScheduleId] FOREIGN KEY([WednesdayScheduleId])
REFERENCES [dbo].[DailySchedules] ([Id])
GO
ALTER TABLE [dbo].[WeeklySchedules] CHECK CONSTRAINT [FK_dbo.WeeklySchedules_dbo.DailySchedules_WednesdayScheduleId]
GO
