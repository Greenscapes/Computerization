/****** Object:  Table [dbo].[Service]    Script Date: 10/4/2016 4:15:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Services](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[PropertyService]    Script Date: 10/4/2016 4:15:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PropertyServices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ServiceId] [int] NOT NULL,
	[Propertyid] [int] NOT NULL,
	[EventCount] [int] NOT NULL,
 CONSTRAINT [PK_PropertyService] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PropertyServices] ADD  CONSTRAINT [DF_PropertyService_EventCount]  DEFAULT ((0)) FOR [EventCount]
GO

ALTER TABLE [dbo].[PropertyServices]  WITH CHECK ADD  CONSTRAINT [FK_PropertyService_Service] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([Id])
GO

ALTER TABLE [dbo].[PropertyServices] CHECK CONSTRAINT [FK_PropertyService_Service]
GO

ALTER TABLE [dbo].[PropertyServices]  WITH CHECK ADD  CONSTRAINT [FK_PropertyService_Property] FOREIGN KEY([PropertyId])
REFERENCES [dbo].[Properties] ([Id])
GO

ALTER TABLE [dbo].[PropertyServices] CHECK CONSTRAINT [FK_PropertyService_Property]
GO

ALTER TABLE [dbo].[EventTaskLists] ADD PropertyServiceId int NULL
GO

ALTER TABLE [dbo].[EventTaskLists] ADD NumberServices decimal(18,2) NULL
GO

ALTER TABLE [dbo].[EventTaskLists]  WITH CHECK ADD  CONSTRAINT [FK_EventTaskLists_PropertyService] FOREIGN KEY([PropertyServiceId])
REFERENCES [dbo].[PropertyServices] ([Id])
GO

ALTER TABLE [dbo].[EventTaskLists] CHECK CONSTRAINT [FK_EventTaskLists_PropertyService]
GO