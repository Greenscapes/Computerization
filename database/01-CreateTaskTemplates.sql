
/****** Object:  Table [dbo].[PropertyTasks]    Script Date: 11/5/2015 11:55:30 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TaskTemplates](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
	[EstimatedDuration] [int] NOT NULL,
	[IsFreeService] [bit] NOT NULL
 CONSTRAINT [PK_dbo.TaskTemplates] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

UPDATE ServiceTemplates set Name = 'Customer Complaint Visit' where Name = 'Commercial Complaint Visit'
GO