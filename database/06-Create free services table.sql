USE [greenscapes]
GO

/****** Object:  Table [dbo].[FreeServices]    Script Date: 10/11/2016 4:43:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FreeServices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EventTaskListId] [int] NOT NULL,
	[ServiceTime] [datetime] NOT NULL,
 CONSTRAINT [PK_FreeServices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[FreeServices]  WITH CHECK ADD  CONSTRAINT [FK_FreeServices_EventTaskLists] FOREIGN KEY([EventTaskListId])
REFERENCES [dbo].[EventTaskLists] ([Id])
GO

ALTER TABLE [dbo].[FreeServices] CHECK CONSTRAINT [FK_FreeServices_EventTaskLists]
GO


