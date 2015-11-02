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
VALUES (1, 'Arborjet Treatment','/templates/servicetickets/arborjet-treatment.html','{"Trees":[],"DefaultTree":{"Kind":"Palm","Type":"","Location":"","ChemicalInjected":false,"ChemcialAmount":0}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (2, 'Commercial Mowing Visit','/templates/servicetickets/commercial-mowing.html','{"Tasks":[], "DefaultTask": {"Location":"", "Mowed":false, "HardtopEdged":false, "BedEdged":false, "BlewOff":false, "DebrisCollected":false, "Comments":""}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (3, 'Commercial Pruning and Weeding Visit','/templates/servicetickets/commercial-pruning.html','{"Tasks":[], "DefaultTask": {"Location":"", "Pruned":false, "Weeded":false, "Sprayed":false, "BlewOff":false, "Comments":""}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (4, 'Commercial Weeding Only Visit','/templates/servicetickets/commercial-weeding.html','{"Tasks":[], "DefaultTask": {"Location":"", "HandPulledWeeds":false, "SprayedWeeds":false, "SprayedCrackWeeds":false, "Comments":""}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (5, 'Commercial Yard Trash Collection Visit','/templates/servicetickets/commercial-trash.html','{"Tasks":[], "DefaultTask": {"Location":"", "YardTrashPresent":false,"Comments":""}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (6, 'Customer Care Visit','/templates/servicetickets/customer-care.html','{}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (7, 'Commercial Complaint Visit','/templates/servicetickets/customer-complaint.html','{}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (8, 'Fertilization/Pest Control Visit','/templates/servicetickets/pest-control.html','{"Tasks":[{"Name":"St Augustine Fert","Location":"", "ProductUsed":"", "QuantityUsed":0, "Prevention":""}
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
], "DefaultTask": {"Name":"","Location":"", "ProductUsed":"", "QuantityUsed":0, "Prevention":""}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (9, 'Irrigation Adjustment','/templates/servicetickets/irrigation-adjustment.html','{"Zones":[], "DefaultZone": {"Number":0,"Adjustment":""}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (10, 'Irrigation Inspection','/templates/servicetickets/irrigation-inspection.html','{
"NoCharges": [],
"DefaultNoCharge":{"ZoneNumber": 1, "Repairs":[{"Name":"No Faults Found", "Value":false},{"Name":"Partial Clog Cleared", "Value":false},{"Name":"Arc/Radius Adjusted", "Value":false}]},
"Charges": [],
"DefaultCharge":{"ZoneNumber": 1, "Repairs":[{"Name":"Head Replaced", "Value":false},{"Name":"Head Straightened", "Value":false},{"Name":"Nozzle Replaced", "Value":false},{"Name":"Break in Pipe Under 2''''", "Value":false}]}
 }');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (11, 'Irrigation Installation','/templates/servicetickets/irrigation-installation.html','{
"Zones": [],
"DefaultZone":{"Number": 1, "Installation": ""},
"Parts": [],
"DefaultPart":{"Name": "", "Charge":0.00}
 }');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (12, 'Irrigation Port of Control Installation','/templates/servicetickets/irrigation-port.html','{
"Clocks": [],
"DefaultClock":{"Reference": "", "Installation": ""},
"Parts": [],
"DefaultPart":{"Name": "", "Charge":0.00},
"Labor": [],
"DefaultLabor":{"Name": "", "Charge":0.00}
 }');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (13, 'Irrigation Quote','/templates/servicetickets/irrigation-quote.html','{
"Zones": [],
"DefaultZone":{"Number": 1, "Repair": ""},
"Parts": [],
"DefaultPart":{"Name": "", "Charge":0.00},
"Labor": [{"Name": "Initial call out and first hour", "Charge":100.00}],
"DefaultLabor":{"Name": "", "Charge":0.00}
 }');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (14, 'Irrigation Repair','/templates/servicetickets/irrigation-repair.html','{
"Zones": [],
"DefaultZone":{"Number": 1, "Repair": ""},
"Parts": [],
"DefaultPart":{"Name": "", "Charge":0.00},
"Labor": [],
"DefaultLabor":{"Name": "", "Charge":0.00}
 }');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (15, 'Landscape Installation or Removal or Repair Visit','/templates/servicetickets/landscape-repair.html','{"Tasks":[], "DefaultTask": {"Location":"", "Plantings":"", "Amount":0}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (16, 'Mulching Visit','/templates/servicetickets/mulching.html','{"Tasks":[], "DefaultTask": {"Location":"", "Size":"", "NumberBags":0}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (17, 'Operations Manager Quality Control Visit','/templates/servicetickets/quality-control.html','{}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (18, 'Pruning of Palm Trees over 15ft','/templates/servicetickets/palm-pruning.html','{"CompletedPalms":[], "RemainingPalms":[], "DefaultPalm": {"Area":"", "Number":1}}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (19, 'Residential/Small Commercial Maintenance Visit','/templates/servicetickets/residential-maintenance.html','{

"Tasks":[],
"DefaultTask": {"Location":"", "Mowed":false, "HardtopEdged":false, "BedEdged":false, "BlewOff":false, "DebrisCollected":false, "Comments":""},
"Plants":[],
"DefaultPlant":{"Location":"", "Plant":"", "Pruned":false, "Comments":""},
"Weeds":[],
"DefaultWeed": {"Location":"", "HandPulled":false, "Sprayed":false, "SprayedCracks":false, "Comments":""},
"Work":[],
"DefaultWork": {"Location":"", "FireAnt":false, "DebrisCollected":false, "BlewOff":false, "Comments":""}
}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (20, 'Sales Visit','/templates/servicetickets/sales-visit.html','{}');
GO

INSERT ServiceTemplates (Id, Name, Url, JsonFields) 
VALUES (21, 'Walkthrough Record','/templates/servicetickets/walk-through.html','{}');
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