/*
*********************************************************************************************

Run Once Script to create all the tables and pre populate the service templates.

**********************************************************************************************
*/
USE [CMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ServiceTemplates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Url] [nvarchar](max) NULL,
	[JsonFields] [ntext] NULL
	
 CONSTRAINT [PK_dbo.ServiceTemplate] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET IDENTITY_INSERT ServiceTemplates ON

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (1, 'Arborjet Treatment','/templates/servicetickets/arborjet-treatment.html','{"Trees":[],"DefaultTree":{"Kind":"Palm","Type":"Coconut"}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (2, 'Commercial Mowing Visit','/templates/servicetickets/commercial-mowing.html','{"Trees":[],"DefaultTree":{"Kind":"Palm","Type":"Coconut"}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (3, 'Commercial Pruning and Weeding Visit','/templates/servicetickets/commercial-pruning.html','{"Trees":[],"DefaultTree":{"Kind":"Palm","Type":"Coconut"}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (4, 'Commercial Weeding Only Visit','/templates/servicetickets/commercial-weeding.html','{"Trees":[],"DefaultTree":{"Kind":"Palm","Type":"Coconut"}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (5, 'Commercial Yard Trash Collection Visit','/templates/servicetickets/commercial-trash.html','{"Trees":[],"DefaultTree":{"Kind":"Palm","Type":"Coconut"}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (6, 'Customer Care Visit','/templates/servicetickets/customer-care.html','{"Trees":[],"DefaultTree":{"Kind":"Palm","Type":"Coconut"}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (7, 'Commercial Complaint Visit','/templates/servicetickets/commercial-complaint.html','{"Trees":[],"DefaultTree":{"Kind":"Palm","Type":"Coconut"}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (8, 'Fertilization/Pest Control Visit','/templates/servicetickets/pest-control.html','{"Trees":[],"DefaultTree":{"Kind":"Palm","Type":"Coconut"}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (9, 'Irrigation Adjustment','/templates/servicetickets/irrigation-adjustment.html','{"Trees":[],"DefaultTree":{"Kind":"Palm","Type":"Coconut"}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (10, 'Irrigation Inspection','/templates/servicetickets/irrigation-inspection.html','{"Trees":[],"DefaultTree":{"Kind":"Palm","Type":"Coconut"}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (11, 'Irrigation Installation','/templates/servicetickets/irrigation-installation.html','{"Trees":[],"DefaultTree":{"Kind":"Palm","Type":"Coconut"}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (12, 'Irrigation Port of Control Installation','/templates/servicetickets/irrigation-port.html','{"Trees":[],"DefaultTree":{"Kind":"Palm","Type":"Coconut"}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (13, 'Irrigation Quote','/templates/servicetickets/irrigation-quote.html','{"Trees":[],"DefaultTree":{"Kind":"Palm","Type":"Coconut"}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (14, 'Irrigation Repair','/templates/servicetickets/irrigation-repair.html','{"Trees":[],"DefaultTree":{"Kind":"Palm","Type":"Coconut"}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (15, 'Landscape Installation or Removal or Repair Visit','/templates/servicetickets/landscape-repair.html','{"Trees":[],"DefaultTree":{"Kind":"Palm","Type":"Coconut"}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (16, 'Mulching Visit','/templates/servicetickets/mulching.html','{"Trees":[],"DefaultTree":{"Kind":"Palm","Type":"Coconut"}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (17, 'Operations Manager Quality Control Visit','/templates/servicetickets/operations-manager.html','{"Trees":[],"DefaultTree":{"Kind":"Palm","Type":"Coconut"}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (18, 'Pruning of Palm Trees over 15ft','/templates/servicetickets/pruning-palm.html','{"Trees":[],"DefaultTree":{"Kind":"Palm","Type":"Coconut"}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (19, 'Residential/Small Commercial Maintenance Visit','/templates/servicetickets/residential-maintenance.html','{"Trees":[],"DefaultTree":{"Kind":"Palm","Type":"Coconut"}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (20, 'Sales Visit','/templates/servicetickets/sales-visit.html','{"Trees":[],"DefaultTree":{"Kind":"Palm","Type":"Coconut"}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (21, 'Walkthrough Record','/templates/servicetickets/walkthrough-record.html','{"Trees":[],"DefaultTree":{"Kind":"Palm","Type":"Coconut"}}');
GO

SET IDENTITY_INSERT ServiceTemplates OFF

CREATE TABLE [dbo].[ServiceTickets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ServiceTemplateId] [int] NOT NULL,
	[PropertyTaskId] [int] NOT NULL,
	[ReferenceNumber] [nvarchar](max) NULL,
	[VisitFromTime] [datetime] NULL,
	[VisitToTime] [datetime] NULL,
	[Staff] [nvarchar](max) NULL,
	[JsonFields] [ntext] NULL,
	[ApprovedById] [int] NULL,
	[ApprovedDate] [datetime] NULL,
	[Condition] [int] NULL,
	[Notes] ntext NULL,
	CONSTRAINT [PK_dbo.ServiceTickets] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ServiceTickets]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ServiceTickets_dbo.ServiceTemplates_ServiceTemplateId] FOREIGN KEY([ServiceTemplateId])
REFERENCES [dbo].[ServiceTemplates] ([Id])
GO

ALTER TABLE [dbo].[ServiceTickets]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ServiceTickets_dbo.PropertyTasks_PropertyTaskId] FOREIGN KEY([PropertyTaskId])
REFERENCES [dbo].[PropertyTasks] ([Id])
GO

ALTER TABLE [dbo].[ServiceTickets]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ServiceTickets_dbo.Employees_ApprovedById] FOREIGN KEY([ApprovedById])
REFERENCES [dbo].[Employees] ([Id])
GO