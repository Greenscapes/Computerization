USE [CMS]
GO

/****** Object:  Table [dbo].[CrewMembers]    Script Date: 11/09/2015 15:40:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ServiceMembers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[ServiceTicketId] [int] NOT NULL
 CONSTRAINT [PK_dbo.ServiceMembers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[ServiceMembers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ServiceMembers_dbo.ServiceTickets_ServiceTicketId] FOREIGN KEY([ServiceTicketId])
REFERENCES [dbo].[ServiceTickets] ([Id])
GO

ALTER TABLE [dbo].[ServiceMembers] CHECK CONSTRAINT [FK_dbo.ServiceMembers_dbo.ServiceTickets_ServiceTicketId]
GO

ALTER TABLE [dbo].[ServiceMembers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ServiceMembers_dbo.Employees_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO

ALTER TABLE [dbo].[ServiceMembers] CHECK CONSTRAINT [FK_dbo.ServiceMembers_dbo.Employees_EmployeeId]
GO

ALTER TABLE [dbo].[ServiceTickets] DROP COLUMN Staff
GO


